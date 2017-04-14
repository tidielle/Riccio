#Region "Imports"
Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
#End Region

Public Class CLDPRELMP
  Inherits CLD__BASE
  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strNomeTabella As String, _
                                      ByVal strData As String, _
                                      ByVal bSblocca As Boolean, _
                                      ByVal bMaturaSoloRDTotInc As Boolean, _
                                      ByRef nAgentiElaborati As Integer) As Boolean
    Try
      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return Elabora(strDitta, strNomeTabella, strData, bSblocca, bMaturaSoloRDTotInc, nAgentiElaborati, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strNomeTabella As String, _
                                      ByVal strData As String, _
                                      ByVal bSblocca As Boolean, _
                                      ByVal bMaturaSoloRDTotInc As Boolean, _
                                      ByRef nAgentiElaborati As Integer, _
                                      ByVal bProvvig2 As Boolean) As Boolean

    Dim strSQL As String = ""
    Dim oCldPrgspv As New CLDPRGSPV
    Dim strDesogglog As String

    Dim dTotfatt, dPvProvv, dTotfattnet, dSommaImp, dTotincass, dTmp2, dPerc1 As Decimal
    Dim dttProvvig As New DataTable, dttScaden As New DataTable, dttTabcage As New DataTable
    Dim dOmaggiReali As Decimal = 0   'omaggi effettivi (senza tener conto delle RB/TRATTE nei pagamenti misti)
    Dim dIvaSplitPayment As Decimal = 0 'questa iva deve essere tolta dal tot. documento perchè non presente nelle scadenze
    Dim dttTmp As New DataTable
    Dim bEscludiAccontiInElab As Boolean = CBool(GetSettingBus("BSPRELMP", "OPZIONI", ".", "EscludiAccontiInElab", "0", " ", "0"))

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strNomeTabella, strData, bSblocca, bMaturaSoloRDTotInc, _
                                             nAgentiElaborati, bProvvig2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nAgentiElaborati = NTSCInt(oIn(5))
        Return CBool(oOut)
      End If
      '----------------

      oCldPrgspv.Init(oApp)

      '-----------------------------------------------------------------------------------------
      '--- Serve per l'eventuale scrittura in ACTLOG
      '-----------------------------------------------------------------------------------------
      strDesogglog = oApp.Tr(Me, 128733741050312500, "Elaborazione delle provvigioni maturate") & vbCrLf & vbCrLf & _
                     oApp.Tr(Me, 128733689142968750, " - Agenti elaborati................................:") & " " & vbCrLf
      '-----------------------------------------------------------------------------------------

      'Lettura agenti
      strSQL = "SELECT tb_codcage, tb_descage, tb_tippro, tb_giorni FROM tabcage" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      dttTabcage = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTabcage.Rows.Count = 0 Then Return True

      nAgentiElaborati = 0
      For i As Integer = 0 To dttTabcage.Rows.Count - 1
        'Per ogni agente
        'scarto le provvigioni con segno = 0, ovvero che non devono andare a finire da nessuna parte
        'quelle con pv_origine = 'X' (simulate) e quelle nuove, non gestite fino a net 2011 (che hanno maturazione MANUALE)

        'provvigioni su incassato nuovo sistema: non sono gestite qui, eccetto quelle dove il totale documento è uguale a 0, per cui non c'è scaden!!!
        strSQL = "SELECT * FROM provvig" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pv_codage = " & NTSCInt(dttTabcage.Rows(i)!tb_codcage) & _
                 " AND ((pv_origine NOT IN ('X', 'T', 'Z'" & IIf(bEscludiAccontiInElab = True, ", 'A', 'B'", "").ToString & ")) OR (pv_origine IN ('T') AND pv_totfatt = 0))" & _
                 " AND pv_segno <> 0" & _
                 " AND pv_flag = 'N'"
        If NTSCStr(dttTabcage.Rows(i)!tb_tippro) = "I" Or NTSCStr(dttTabcage.Rows(i)!tb_tippro) = "T" Then
          'se lavoro su incassato o incassato totale non testo la data di scadenza per i pagametni di tipo bonifico e/o rim. diretta, 
          'perchè potrei aver incassato in anticipo rispetto alla scadenza 
          strSQL += " AND ((pv_datscad <= " & CDataSQL(strData) & " AND pv_tippaga < 3) OR pv_tippaga >= 3)"
        Else
          strSQL += " AND pv_datscad <= " & CDataSQL(strData)
        End If
        dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttProvvig.Rows.Count > 0 Then

          For j As Integer = 0 To dttProvvig.Rows.Count - 1
            'Per ogni provvigione dell'agente
            dPvProvv = NTSCDec(dttProvvig.Rows(j)!pv_provv)
            Select Case NTSCStr(dttTabcage.Rows(i)!tb_tippro)
              Case "F" 'fatturato
                dttProvvig.Rows(j)!pv_impvmat = dPvProvv

              Case "M" 'maturato
                If DateAdd("d", NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(dttProvvig.Rows(j)!pv_datscad)) <= NTSCDate(strData) Then
                  If Not NTSCDec(dttProvvig.Rows(j)!pv_impvmat) = dPvProvv Then
                    dttProvvig.Rows(j)!pv_impvmat = dPvProvv
                  End If
                End If

              Case "I" 'incassato (guarda il tipo pagamento)
                If bProvvig2 = False Then

                  If NTSCInt(dttProvvig.Rows(j)!pv_tippaga) < 3 Then
                    If DateAdd("d", NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(dttProvvig.Rows(j)!pv_datscad)) <= NTSCDate(strData) Then
                      If Not NTSCDec(dttProvvig.Rows(j)!pv_impvmat) = dPvProvv Then
                        dttProvvig.Rows(j)!pv_impvmat = dPvProvv
                      End If
                    End If
                  Else
                    'rimessa diretta (guarda se incassato in scaden)
                    'scarto le riba e le tratte: per quel tipo di pagamento il maturato viene calcolato senza guardare scaden
                    dTotfatt = NTSCDec(dttProvvig.Rows(j)!pv_totfatt)
                    dTotfattnet = ArrDbl(NTSCDec(dttProvvig.Rows(j)!pv_totfatt) - NTSCDec(dttProvvig.Rows(j)!pv_totomag), TrovaNdec(0))

                    '--------------------------------------
                    'per ottenere gli omaggi effettivi prendo l'importo degli omaggi dalla riga con tipo pagamento = RB o TRATTA
                    dOmaggiReali = 0
                    If NTSCDec(dttProvvig.Rows(j)!pv_totomag) <> 0 Then
                      strSQL = "SELECT top 1 pv_totomag FROM provvig " & _
                               " WHERE codditt = " & CStrSQL(strDitta) & _
                               " AND pv_codage = " & NTSCInt(dttTabcage.Rows(i)!tb_codcage) & _
                               " AND pv_tipdoc = " & CStrSQL(dttProvvig.Rows(j)!pv_tipdoc) & _
                               " AND pv_anno = " & NTSCInt(dttProvvig.Rows(j)!pv_anno) & _
                               " AND pv_serie = " & CStrSQL(dttProvvig.Rows(j)!pv_serie) & _
                               " AND pv_numdoc = " & NTSCInt(dttProvvig.Rows(j)!pv_numdoc) & _
                               " AND pv_origine = " & CStrSQL(dttProvvig.Rows(j)!pv_origine) & _
                               " AND pv_tippaga < 3"
                      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                      If dttTmp.Rows.Count > 0 Then
                        dOmaggiReali = NTSCDec(dttTmp.Rows(0)!pv_totomag)
                      Else
                        'non ho trovato righe di tipo RB o tgratta: vuol dire che gli omaggi di pv_totomag sono già quelli reali
                        dOmaggiReali = NTSCDec(dttProvvig.Rows(j)!pv_totomag)
                      End If
                      dttTmp.Clear()
                    End If

                    'non considero RB e TRATTE, visto che per loro il sistema di calcolo non si basa su scaden
                    strSQL = "SELECT SUM(sc_importo) as tot FROM scaden" & _
                             " WHERE codditt = " & CStrSQL(strDitta) & _
                             " AND sc_conto = " & NTSCInt(dttProvvig.Rows(j)!pv_conto) & _
                             " AND sc_annpar = " & NTSCInt(dttProvvig.Rows(j)!pv_annpart) & _
                             " AND sc_alfpar = " & CStrSQL(dttProvvig.Rows(j)!pv_alfpart) & _
                             " AND sc_numpar = " & NTSCInt(dttProvvig.Rows(j)!pv_numpart) & _
                             " AND sc_dtsaldato <= " & CDataSQL(strData) & _
                             " AND sc_tippaga > 2"
                    dttScaden = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                    If dttScaden.Rows.Count > 0 Then
                      dTotincass = ArrDbl(NTSCDec(dttScaden.Rows(0)!tot), TrovaNdec(0))
                    Else
                      dTotincass = 0
                    End If
                    dttScaden.Clear()
                    'dall'incassato tolgo gli omaggi reali, visto che pnfa, quando li contabilizza, li tratta come scadenza di tipo 'rimessa diretta'
                    'in questo modo ora nell'incassato totale ci sono solo le reali rimesse dirette

                    If dOmaggiReali <> 0 And dTotincass = 0 Then
                      'se la fattura non è stata incassata ma ci sono omaggi, non devo stornare gli omaggi. proseguendo verrà fuori un maturato = 0
                    Else
                      dTotincass -= dOmaggiReali
                    End If

                    'IVA split payment: visto che questa IVA non è tra le scadenze, non devo tenerne conto neanche nel totale da incassare
                    'L'IVA split payment è data dalla differenza tra totale residuo fattura e somma scadenze
                    'eventuali omaggi sono già nella somma delle scadenze!!!!
                    dIvaSplitPayment = 0
                    strSQL = "SELECT SUM(sc_importo) as tot FROM scaden" & _
                             " WHERE codditt = " & CStrSQL(strDitta) & _
                             " AND sc_conto = " & NTSCInt(dttProvvig.Rows(j)!pv_conto) & _
                             " AND sc_annpar = " & NTSCInt(dttProvvig.Rows(j)!pv_annpart) & _
                             " AND sc_alfpar = " & CStrSQL(dttProvvig.Rows(j)!pv_alfpart) & _
                             " AND sc_numpar = " & NTSCInt(dttProvvig.Rows(j)!pv_numpart)
                    dttScaden = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                    If dttScaden.Rows.Count > 0 AndAlso dttScaden.Rows(0)!tot.Equals(DBNull.Value) = False Then
                      dIvaSplitPayment = NTSCDec(dttProvvig.Rows(j)!pv_totfatt) - ArrDbl(NTSCDec(dttScaden.Rows(0)!tot), TrovaNdec(0))
                      If dIvaSplitPayment < 0 Then dIvaSplitPayment = 0 'non succederà mai, ma per scrupolo ...
                      dTotfattnet -= dIvaSplitPayment
                    Else
                      dIvaSplitPayment = 0
                    End If
                    dttScaden.Clear()

                    'se completato l'incasso della rimessa diretta
                    'mette il rimanente della provvigione come maturato
                    If Math.Abs(dTotincass + dOmaggiReali) >= Math.Abs(NTSCDec(dttProvvig.Rows(j)!pv_totfatt) - dIvaSplitPayment) Then
                      'incasso completato
                      dTmp2 = ArrDbl(NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag), TrovaNdec(0))
                    Else
                      'incasso parziale
                      If bMaturaSoloRDTotInc Or dTotfatt = 0 Or dTotfattnet = 0 Then
                        dTmp2 = 0
                      Else
                        'in caso contrario allora calcola pro rata il rimanente
                        If NTSCDec(dttProvvig.Rows(j)!pv_provv) = 0 Then
                          dPerc1 = 0
                        Else
                          'dPerc1 è la parte già pagata in precedenza
                          dPerc1 = NTSCDec(dttProvvig.Rows(j)!pv_impvpag) / NTSCDec(dttProvvig.Rows(j)!pv_provv)
                        End If
                        ' tiene conto ora degli eventuali omaggi, in totincass ci sono già compresi anche gli omaggi
                        'dSommaImp = (dTotincass - NTSCDec(dttProvvig.Rows(j)!pv_totomag)) - (NTSCDec(dttProvvig.Rows(j)!pv_totfatt) - NTSCDec(dttProvvig.Rows(j)!pv_totomag)) * dPerc1
                        'che è come scrivere ...
                        'dSommaImp = dTotincass - NTSCDec(dttProvvig.Rows(j)!pv_totfatt) * dPerc1


                        'ora in dTtoincass non ci sono più gli omaggi, ovvero nei pagamenti misti le RB o TRATTE
                        'che risulterebbero saldate perchè chiuse con BNCGGNEF
                        'in dTotfattnet invece ci sono anche le RB e le tratte, visto che i calcoli devo farli 
                        'sugli importi che deveono essere trattati per CASSA/RIM DIRETTA

                        'è la quota incassata dall'ultima volta che ho pagato le provvigioni
                        'in pratica tolgo dall'incassato quello che ho già corrisposto all'agente
                        dSommaImp = dTotincass - (dTotfattnet * dPerc1)
                        dTmp2 = dSommaImp / dTotfattnet * dPvProvv
                      End If
                      'Comunque l'importo di provvivione maturata NON può mai essere superiore
                      'alla provvigione da maturare meno quella già maturata
                      'Attenzione ! le note di accredito hanno provv. negative
                      If Math.Abs(dTmp2) > Math.Abs(ArrDbl(NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag), TrovaNdec(0))) Then dTmp2 = ArrDbl(NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag), TrovaNdec(0))
                    End If
                    dTmp2 = ArrDbl(dTmp2, TrovaNdec(0))
                    If Not NTSCDec(dttProvvig.Rows(j)!pv_impvmat) = dTmp2 Then
                      dttProvvig.Rows(j)!pv_impvmat = dTmp2
                    End If
                  End If
                End If    'If bProvvig2 = False Then

              Case "T" 'incassato totale (guarda il tipo pagamento)
                If Not NTSCDec(dttProvvig.Rows(j)!pv_impvmat) = 0 Then dttProvvig.Rows(j)!pv_impvmat = 0
                strSQL = "SELECT MAX(" & _
                         NTSCStr(IIf(NTSCInt(dttProvvig.Rows(j)!pv_tippaga) < 3, "sc_datsca", "sc_dtsaldato")) & _
                         ") as dtsaldato, sc_flsaldato FROM scaden" & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " AND sc_conto = " & NTSCInt(dttProvvig.Rows(j)!pv_conto) & _
                         " AND sc_annpar = " & NTSCInt(dttProvvig.Rows(j)!pv_annpart) & _
                         " AND sc_alfpar = " & CStrSQL(dttProvvig.Rows(j)!pv_alfpart) & _
                         " AND sc_numpar = " & NTSCInt(dttProvvig.Rows(j)!pv_numpart) & _
                         " GROUP BY sc_flsaldato" & _
                         " ORDER BY sc_flsaldato ASC"
                dttScaden = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                If dttScaden.Rows.Count > 0 Then
                  If NTSCStr(dttScaden.Rows(0)!sc_flsaldato) = "S" Then
                    'scadenze tutte saldate, allora alla scadenza più lontana gli aggiungo
                    'tot giorni e se è minore o uguale alla data fine elab aggiorno,
                    'in pratica tutte le scadenze devono risultare saldate con data scad < data fine elaborazione

                    If NTSCInt(dttProvvig.Rows(j)!pv_tippaga) < 3 Then
                      'RB o tratta
                      If Not (DateAdd("d", NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(dttScaden.Rows(0)!dtsaldato)) > NTSCDate(strData)) Then
                        dttProvvig.Rows(j)!pv_impvmat = NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag)
                      End If
                    Else
                      'rimessa diretta
                      If Not (NTSCDate(dttScaden.Rows(0)!dtsaldato) > NTSCDate(strData)) Then
                        dttProvvig.Rows(j)!pv_impvmat = NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag)
                      End If
                    End If    'If NTSCInt(dttProvvig.Rows(j)!pv_tippaga) < 3 Then
                  End If
                End If    'If dttScaden.Rows.Count > 0 Then

                'se la fattura, pur avendo delle provvigioni, ha un totale = 0 maturo tutto
                If NTSCDec(dttProvvig.Rows(j)!pv_totfatt) = 0 Then
                  dttProvvig.Rows(j)!pv_impvmat = NTSCDec(dttProvvig.Rows(j)!pv_provv)
                End If

            End Select

          Next

          For Each dtrT As DataRow In dttProvvig.Rows
            strSQL = "UPDATE provvig SET pv_impvmat = " & CDblSQL(NTSCDec(dtrT!pv_impvmat))
            If NTSCDec(dtrT!pv_impvmat) <> 0 Then
              strSQL += ", pv_datmatu = " & CDataSQL(NTSCDate(strData))
            Else
              strSQL += ", pv_datmatu = null"
            End If
            strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND pv_progr = " & dtrT!pv_progr.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Next

          nAgentiElaborati += 1

          '---------------------------------------------------------------------------------------
          '--- Serve per l'eventuale scrittura in ACTLOG
          '---------------------------------------------------------------------------------------
          strDesogglog = strDesogglog & "".PadLeft(51, " "c) & ". '" & NTSCInt(dttTabcage.Rows(i)!tb_codcage) & "' " & NTSCStr(dttTabcage.Rows(i)!tb_descage) & vbCrLf
          '---------------------------------------------------------------------------------------

        End If

      Next

      '-----------------------------------------------------------------------------------------
      '--- Scrive l'eventuale riga in ACTLOG, se attiva l'opzione di registro relativa
      '-----------------------------------------------------------------------------------------
      strDesogglog &= vbCrLf & oApp.Tr(Me, 128733742207812500, " - Elaborazione provvigioni fino al................:") & " '" & strData & "'" & _
                      vbCrLf & oApp.Tr(Me, 128733742183906250, " - Sblocca provvigioni sospese se scadenze saladate:") & " " & _
                      NTSCStr(IIf(bSblocca, "'" & oApp.Tr(Me, 128733742153750000, "Sì") & "'", _
                                            "'" & oApp.Tr(Me, 128733742243125000, "No") & "'")) & vbCrLf
      ScriviActLog(strDitta, "BSPRELMP", "BSPRELMP", "", "", "M", "E", strDesogglog, False)
      '-----------------------------------------------------------------------------------------

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Sblocca(ByVal strDitta As String, _
                                      ByVal strNomeTabella As String, _
                                      ByRef dsOut As DataSet, _
                                      ByRef strbSblocca As System.Text.StringBuilder, _
                                      ByVal strData As String, _
                                      ByVal bSbloccaSoloFinoaData As Boolean) As Boolean

    Dim strSQL As String = ""
    Dim dttScaden As New DataTable
    Try
      '----------------------------------------------------------------------------------------
      'sblocco le provvigioni sospese se la scadenza risulta saldata (opera solo su INCASSATO)
      strSQL = "SELECT provvig.* FROM provvig INNER JOIN tabcage" & _
               " ON provvig.pv_codage = tabcage.tb_codcage" & _
               " AND provvig.codditt = tabcage.codditt" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND pv_flag = 'I' and (tb_tippro = 'I' or tb_tippro = 'T') "
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella)
      If dsOut.Tables(strNomeTabella).Rows.Count = 0 Then Return True
      SetTableDefaultValueFromDB(strNomeTabella, dsOut)

      For i As Integer = 0 To dsOut.Tables(strNomeTabella).Rows.Count - 1

        If bSbloccaSoloFinoaData Then
          'Leggo scaden con flag_saldato = "S"
          strSQL = "SELECT sc_dtsaldato FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND sc_flsaldato = 'S'" & _
                   " AND sc_conto = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_conto) & _
                   " AND sc_annpar = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_annpart) & _
                   " AND sc_alfpar = " & CStrSQL(dsOut.Tables(strNomeTabella).Rows(i)!pv_alfpart) & _
                   " AND sc_numpar = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_numpart) & _
                   " AND sc_datsca = " & CDataSQL(NTSCDate(dsOut.Tables(strNomeTabella).Rows(i)!pv_datscad)) & _
                   " ORDER BY sc_dtsaldato DESC"
          dttScaden = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          For j As Integer = 0 To dttScaden.Rows.Count - 1
            'Se data saldata = "" allora non sbloccare
            If NTSCStr(dttScaden.Rows(j)!sc_dtsaldato) = "" Then GoTo Prosegui
            'Se data saldata > data riferimento allora non sbloccare
            If NTSCDate(dttScaden.Rows(j)!sc_dtsaldato) > NTSCDate(strData) Then GoTo Prosegui
          Next
        End If

        strSQL = "SELECT TOP 1 * FROM scaden" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_flsaldato = 'N'" & _
                 " AND sc_conto = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_conto) & _
                 " AND sc_annpar = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_annpart) & _
                 " AND sc_alfpar = " & CStrSQL(dsOut.Tables(strNomeTabella).Rows(i)!pv_alfpart) & _
                 " AND sc_numpar = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_numpart) & _
                 " AND sc_datsca = " & CDataSQL(NTSCDate(dsOut.Tables(strNomeTabella).Rows(i)!pv_datscad))

        If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then
          dsOut.Tables(strNomeTabella).Rows(i)!pv_flag = "N"
          strbSblocca.Append(NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_codage) & _
                        " == " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_conto) & _
                        " == " & NTSCStr(dsOut.Tables(strNomeTabella).Rows(i)!pv_tipdoc) & _
                        " - " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_anno) & _
                        " - '" & NTSCStr(dsOut.Tables(strNomeTabella).Rows(i)!pv_serie) & _
                        "' - " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_numdoc) & _
                        " - " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(i)!pv_numrat) & vbCrLf)
        End If

Prosegui:

      Next

      If dsOut.Tables(strNomeTabella).Rows.Count > 0 Then
        Dim oCldPrgspv As New CLDPRGSPV
        oCldPrgspv.Init(oApp)
        If Not oCldPrgspv.ScriviTabellaSemplice(strDitta, strNomeTabella, dsOut.Tables(strNomeTabella), "", "", "") Then Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IncassatoNewCheckScaden(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    'ottengo un datatable contenente i record di provvig disallineati da scaden per importo e/o tipo pagamento
    'per poter successivamente generare un log (indipendentemente dal tipo pagamento)
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT provvig.*, scaden.sc_datreg, scaden.sc_numreg, sc_datsca, sc_tippaga, sc_importo " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
               " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
               " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND tabcage.tb_tippro = 'I'" & _
               " AND pv_origine = 'T'" & _
               " AND pv_segno <> 0" & _
               " AND pv_impvmat = 0" & _
               " AND pv_provv <> 0 " & _
               " AND pv_flag = 'N'" & _
               " AND (sc_tippaga <> pv_tippaga OR sc_importo <> pv_impscad)" & _
               " ORDER BY pv_codage, pv_conto, pv_anno, pv_serie, pv_numdoc, pv_numrat"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IncassatoNewAggMaturatoSuRD(ByVal strDitta As String, ByVal strData As String) As Boolean
    'calcolo il maturato sulle provvigioni su 'incassato nuovo' con sistema di pagamento RD: 
    'verifico il record in scaden: deve avere lo stesso importo e stesso tipo pagamento!!!!
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Try
      strSQL = "UPDATE provvig SET pv_datmatu = " & CDataSQL(NTSCDate(strData)) & ", " & _
               " pv_impvmat = pv_provv " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
               " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
               " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
               " AND provvig.pv_impscad = scaden.sc_importo AND provvig.pv_tippaga = scaden.sc_tippaga" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND tabcage.tb_tippro = 'I'" & _
               " AND scaden.sc_flsaldato = 'S'" & _
               " AND scaden.sc_dtsaldato <= " & CDataSQL(strData) & _
               " AND pv_origine = 'T'" & _
               " AND pv_segno <> 0" & _
               " AND pv_impvmat = 0" & _
               " AND pv_provv <> 0 " & _
               " AND pv_flag = 'N'" & _
               " AND pv_tippaga >= 3"
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IncassatoNewAggDatScadEffSuRB(ByVal strDitta As String, ByVal strData As String, _
                                                            ByVal nCausRagg As Integer) As Boolean
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Try
      'rimetto la data originaria in pv_datscadeff
      'ed azzero i campi di appoggio pv_scflsaldato e pv_dtsaldato
      strSQL = "UPDATE provvig SET pv_datscadeff = pv_datscad, " & _
               " pv_scflsaldato = 'N', pv_dtsaldato = null " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
               " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
               " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
               " AND provvig.pv_impscad = scaden.sc_importo AND provvig.pv_tippaga = scaden.sc_tippaga" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND tabcage.tb_tippro = 'I'" & _
               " AND pv_origine = 'T'" & _
               " AND pv_segno <> 0" & _
               " AND pv_impvmat = 0" & _
               " AND pv_provv <> 0 " & _
               " AND pv_flag = 'N'" & _
               " AND pv_tippaga < 3"
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se c'è un raggruppamento effetti, correggo la data effettiva prendendo 
      'provvig inner join scaden per partita, da scaden sc_rgsaldato prendo prinot con causale raggruppamento effetti
      'NELLA SECONDA SELECT con conto/data/num reg. di prinot mi collego a scaden con sc_datreg, sc_numreg 
      '(ovvero scadenza generata da raggruppam effetti) e ottengo nuova data scadenza 
      '(in ogni reg. di raggruppam effetti c'è sempre una sola nuova scadenza per ogni cli/for)
      If nCausRagg <> 0 Then
        strSQL = "SELECT DISTINCT pv_progr, pv_conto, pn_datreg, pn_numreg" & _
                 " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
                 " AND provvig.pv_codage = tabcage.tb_codcage" & _
                 " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
                 " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
                 " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
                 " AND provvig.pv_impscad = scaden.sc_importo AND provvig.pv_tippaga = scaden.sc_tippaga" & _
                 " INNER JOIN prinot ON scaden.codditt = prinot.codditt AND scaden.sc_dtsaldato = prinot.pn_datreg " & _
                 " AND scaden.sc_rgsaldato = prinot.pn_numreg " & _
                 " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
                 " AND tabcage.tb_tippro = 'I'" & _
                 " AND pv_datscad <= " & CDataSQL(strData) & _
                 " AND pv_origine = 'T'" & _
                 " AND pv_segno <> 0" & _
                 " AND pv_impvmat = 0" & _
                 " AND pv_provv <> 0 " & _
                 " AND pv_flag = 'N'" & _
                 " AND pv_tippaga < 3" & _
                 " AND scaden.sc_flsaldato = 'S'" & _
                 " AND prinot.pn_causale = " & nCausRagg.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        For Each dtrT As DataRow In dttTmp.Rows
          strSQL = "SELECT sc_datsca, sc_importo, sc_flsaldato, sc_dtsaldato, sc_insolu FROM scaden" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND sc_datreg = " & CDataSQL(NTSCDate(dtrT!pn_datreg)) & _
                   " AND sc_numreg = " & NTSCInt(dtrT!pn_numreg).ToString & _
                   " AND sc_conto = " & NTSCInt(dtrT!pv_conto).ToString
          dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp1.Rows.Count > 0 Then
            If NTSCDec(dttTmp1.Rows(0)!sc_importo) > 0 Then
              strSQL = "UPDATE provvig SET pv_datscadeff = " & CDataSQL(NTSCDate(dttTmp1.Rows(0)!sc_datsca))
              'solo se è una scad saldata e non insoluta memorizzo il fatto che è stata incassata e dovrà essere rilevato il maturato
              If NTSCStr(dttTmp1.Rows(0)!sc_flsaldato) = "S" And NTSCStr(dttTmp1.Rows(0)!sc_insolu) = "N" Then
                strSQL += ", pv_scflsaldato = 'S', pv_dtsaldato = " & CDataSQL(NTSCDate(dttTmp1.Rows(0)!sc_dtsaldato))
              End If
              strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND pv_progr = " & dtrT!pv_progr.ToString
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Else
              'la scadenza del raggruppamento è negativa, ovvero le note di accredito erano superiori alle scadenze da fattura: 
              'mantengo la scadenza originaria
            End If
          Else
            'il raggruppamento non ha generato nuove scadenze 
            '(es scadenze di fatture compensate con note di accredito o altre partite a debito)
            'mantengo la scadenza originaria
          End If
          dttTmp1.Clear()
        Next
        dttTmp.Clear()
      End If    'If nCausRagg <> 0 Then

      'ora imposto pv_scflsaldato e pv_dtsaldato per le provvigioni collegate a RB/tratte saldate non collegate a riemissione
      strSQL = "UPDATE provvig SET pv_scflsaldato = 'S', pv_dtsaldato = sc_dtsaldato " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
               " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
               " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
               " AND provvig.pv_impscad = scaden.sc_importo AND provvig.pv_tippaga = scaden.sc_tippaga" & _
               " INNER JOIN prinot ON scaden.codditt = prinot.codditt AND scaden.sc_dtsaldato = prinot.pn_datreg " & _
               " AND scaden.sc_rgsaldato = prinot.pn_numreg " & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND tabcage.tb_tippro = 'I'" & _
               " AND pv_origine = 'T'" & _
               " AND pv_segno <> 0" & _
               " AND pv_impvmat = 0" & _
               " AND pv_provv <> 0 " & _
               " AND pv_flag = 'N'" & _
               " AND pv_tippaga < 3" & _
               " AND scaden.sc_flsaldato = 'S'" & _
               " AND prinot.pn_causale <> " & nCausRagg.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function IncassatoNewAggMaturatoSuRB(ByVal strDitta As String, ByVal strData As String, _
                                                          ByVal bRbNoChiucli As Boolean) As Boolean
    'calcolo il maturato sulle provvigioni su 'incassato nuovo' con sistema di pagamento RB/tratta: 
    'se no nchiudo il cliente, la data di riferimento è la data effettiva dell'incasso, 
    'dviersamente è la data effettiva di scadenza + gg esito RB
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Try
      strSQL = "UPDATE provvig SET pv_datmatu = " & CDataSQL(NTSCDate(strData)) & ", " & _
               " pv_impvmat = pv_provv " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " INNER JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
               " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
               " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
               " AND provvig.pv_impscad = scaden.sc_importo AND provvig.pv_tippaga = scaden.sc_tippaga" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND tabcage.tb_tippro = 'I'" & _
               " AND pv_scflsaldato = 'S'" & _
               " AND pv_origine = 'T'" & _
               " AND pv_segno <> 0" & _
               " AND pv_impvmat = 0" & _
               " AND pv_provv <> 0 " & _
               " AND pv_flag = 'N'" & _
               " AND pv_tippaga < 3"
      If bRbNoChiucli Then
        strSQL += " AND pv_dtsaldato <= " & CDataSQL(strData)
      Else
        strSQL += " AND dateadd(d, tabcage.tb_giorni, pv_datscadeff) <= " & CDataSQL(strData)
      End If
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreElabora_1(ByVal strDitta As String, ByVal strData As String, _
    ByVal bMaturaSoloRDTotInc As Boolean, ByVal bProvvig2 As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim bEscludiAccontiInElab As Boolean = CBool(GetSettingBus("BSPRELMP", "OPZIONI", ".", "EscludiAccontiInElab", "0", " ", "0"))
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim dTotfatt As Decimal = 0
    Dim dTotfattnet As Decimal = 0
    Dim dSommaImp As Decimal = 0
    Dim dTotincass As Decimal = 0
    Dim dTmp2 As Decimal = 0
    Dim dPerc1 As Decimal = 0
    Dim strSQL As String = ""
    Dim dOmaggiReali As Decimal = 0   '--- Omaggi effettivi (senza tener conto delle RB/TRATTE nei pagamenti misti)
    Dim dIvaSplitPayment As Decimal = 0 '--- Questa iva deve essere tolta dal tot. documento perchè non presente nelle scadenze
    Dim dttTabcage As New DataTable
    Dim dttProvvig As New DataTable
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre un DataTable sulla tabella Agenti, sul quale ciclare
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabcage" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " ORDER BY codditt, tb_codcage"
      dttTabcage = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non ci sono agenti, esce
      '--------------------------------------------------------------------------------------------------------------
      If dttTabcage.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dttTabcage.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------
        '--- Utilizza i filtri, su PROVVIG, come per l'elaborazione:
        '--- Per ogni agente scarto le provvigioni con segno = 0,
        '--- ovvero che non devono andare a finire da nessuna parte
        '--- quelle con pv_origine = 'X' (simulate) e quelle nuove, non gestite fino a NET 2011
        '--- (che hanno maturazione MANUALE) provvigioni su incassato nuovo sistema: 
        '--- non sono gestite qui, eccetto quelle dove il totale documento è uguale a 0, per cui non c'è SCADEN
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT provvig.*, tb_descage, tb_giorni" & _
          " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt" & _
                                         " AND provvig.pv_codage = tabcage.tb_codcage" & _
          " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
          " AND pv_codage = " & NTSCInt(dttTabcage.Rows(i)!tb_codcage) & _
          " AND ((pv_origine NOT IN ('X', 'T', 'Z'" & IIf(bEscludiAccontiInElab = True, ", 'A', 'B'", "").ToString & "))" & _
          " OR (pv_origine IN ('T') AND pv_totfatt = 0))" & _
          " AND pv_segno <> 0" & _
          " AND pv_flag = 'N'"
        If NTSCStr(dttTabcage.Rows(i)!tb_tippro) = "I" Or NTSCStr(dttTabcage.Rows(i)!tb_tippro) = "T" Then
          '--------------------------------------------------------------------------------------------------------
          '--- Se lavoro su incassato o incassato totale non testo la data di scadenza 
          '--- per i pagamenti di tipo bonifico e/o rim. diretta, perchè potrei aver incassato in anticipo
          '--- rispetto alla scadenza 
          '--------------------------------------------------------------------------------------------------------
          strSQL += " AND ((pv_datscad <= " & CDataSQL(strData) & " AND pv_tippaga < 3) OR pv_tippaga >= 3)"
        Else
          strSQL += " AND pv_datscad <= " & CDataSQL(strData)
        End If
        dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttProvvig.Rows.Count > 0 Then
          For j = 0 To (dttProvvig.Rows.Count - 1)
            '--------------------------------------------------------------------------------------------------------
            If dttOut.Columns.Count = 0 Then dttOut = dttProvvig.Clone
            '--------------------------------------------------------------------------------------------------------
            With dttProvvig.Rows(j)
              '------------------------------------------------------------------------------------------------------
              '--- Per ogni provvigione dell'agente
              '------------------------------------------------------------------------------------------------------
              Select Case NTSCStr(dttTabcage.Rows(i)!tb_tippro)
                Case "F" '--- Fatturato
                  !pv_impvmat = NTSCDec(!pv_provv)
                Case "M" '--- Maturato
                  If DateAdd(DateInterval.Day, NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(!pv_datscad)) <= NTSCDate(strData) Then
                    If NTSCDec(!pv_impvmat) <> NTSCDec(!pv_provv) Then !pv_impvmat = NTSCDec(!pv_provv)
                  End If
                Case "I" '--- Incassato (guarda il tipo pagamento)
                  If bProvvig2 = False Then
                    If NTSCInt(!pv_tippaga) < 3 Then
                      If DateAdd(DateInterval.Day, NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(!pv_datscad)) <= NTSCDate(strData) Then
                        If NTSCDec(!pv_impvmat) <> NTSCDec(!pv_provv) Then !pv_impvmat = NTSCDec(!pv_provv)
                      End If
                    Else
                      '----------------------------------------------------------------------------------------------
                      '--- Rimessa diretta (guarda se incassato in scaden)
                      '--- scarto le riba e le tratte:
                      '--- per quel tipo di pagamento il maturato viene calcolato senza guardare scaden
                      '----------------------------------------------------------------------------------------------
                      dTotfatt = NTSCDec(!pv_totfatt)
                      dTotfattnet = ArrDbl(NTSCDec(!pv_totfatt) - NTSCDec(!pv_totomag), TrovaNdec(0))
                      '----------------------------------------------------------------------------------------------
                      '--- Per ottenere gli omaggi effettivi prendo l'importo degli omaggi dalla riga con tipo pagamento = RB o TRATTA
                      '----------------------------------------------------------------------------------------------
                      dOmaggiReali = 0
                      If NTSCDec(!pv_totomag) <> 0 Then
                        strSQL = "SELECT TOP 1 pv_totomag FROM provvig " & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND pv_codage = " & NTSCInt(dttTabcage.Rows(i)!tb_codcage) & _
                          " AND pv_tipdoc = " & CStrSQL(!pv_tipdoc) & _
                          " AND pv_anno = " & NTSCInt(!pv_anno) & _
                          " AND pv_serie = " & CStrSQL(!pv_serie) & _
                          " AND pv_numdoc = " & NTSCInt(!pv_numdoc) & _
                          " AND pv_origine = " & CStrSQL(!pv_origine) & _
                          " AND pv_tippaga < 3"
                        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                        If dttTmp.Rows.Count > 0 Then
                          dOmaggiReali = NTSCDec(dttTmp.Rows(0)!pv_totomag)
                        Else
                          '------------------------------------------------------------------------------------------
                          '--- Non ho trovato righe di tipo RB o tgratta: 
                          '--- vuol dire che gli omaggi di pv_totomag sono già quelli reali
                          '------------------------------------------------------------------------------------------
                          dOmaggiReali = NTSCDec(!pv_totomag)
                        End If
                        dttTmp.Clear()
                        dttTmp.Dispose()
                      End If
                      '----------------------------------------------------------------------------------------------
                      '--- Non considero RB e TRATTE, visto che per loro il sistema di calcolo non si basa su scaden
                      '----------------------------------------------------------------------------------------------
                      strSQL = "SELECT SUM(sc_importo) AS tot FROM scaden" & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND sc_conto = " & NTSCInt(!pv_conto) & _
                        " AND sc_annpar = " & NTSCInt(!pv_annpart) & _
                        " AND sc_alfpar = " & CStrSQL(!pv_alfpart) & _
                        " AND sc_numpar = " & NTSCInt(!pv_numpart) & _
                        " AND sc_dtsaldato <= " & CDataSQL(strData) & _
                        " AND sc_tippaga > 2"
                      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                      If dttTmp.Rows.Count > 0 Then
                        dTotincass = ArrDbl(NTSCDec(dttTmp.Rows(0)!tot), TrovaNdec(0))
                      Else
                        dTotincass = 0
                      End If
                      dttTmp.Clear()
                      dttTmp.Dispose()
                      '----------------------------------------------------------------------------------------------
                      '--- Dall'incassato tolgo gli omaggi reali, visto che pnfa, quando li contabilizza, 
                      '--- li tratta come scadenza di tipo 'rimessa diretta'
                      '--- in questo modo ora nell'incassato totale ci sono solo le reali rimesse dirette
                      '----------------------------------------------------------------------------------------------
                      If dOmaggiReali <> 0 And dTotincass = 0 Then
                        '--- Se la fattura non è stata incassata ma ci sono omaggi, 
                        '--- non devo stornare gli omaggi. proseguendo verrà fuori un maturato = 0
                      Else
                        dTotincass -= dOmaggiReali
                      End If
                      '----------------------------------------------------------------------------------------------
                      '--- IVA split payment: visto che questa IVA non è tra le scadenze, non devo tenerne conto neanche nel totale da incassare
                      '--- L'IVA split payment è data dalla differenza tra totale residuo fattura e somma scadenze
                      '--- eventuali omaggi sono già nella somma delle scadenze!!!!
                      '----------------------------------------------------------------------------------------------
                      dIvaSplitPayment = 0
                      strSQL = "SELECT SUM(sc_importo) AS tot FROM scaden" & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND sc_conto = " & NTSCInt(!pv_conto) & _
                        " AND sc_annpar = " & NTSCInt(!pv_annpart) & _
                        " AND sc_alfpar = " & CStrSQL(!pv_alfpart) & _
                        " AND sc_numpar = " & NTSCInt(!pv_numpart)
                      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                      If (dttTmp.Rows.Count > 0) AndAlso (dttTmp.Rows(0)!tot.Equals(DBNull.Value) = False) Then
                        dIvaSplitPayment = NTSCDec(!pv_totfatt) - ArrDbl(NTSCDec(dttTmp.Rows(0)!tot), TrovaNdec(0))
                        If dIvaSplitPayment < 0 Then dIvaSplitPayment = 0
                        dTotfattnet -= dIvaSplitPayment
                      Else
                        dIvaSplitPayment = 0
                      End If
                      dttTmp.Clear()
                      dttTmp.Dispose()
                      '----------------------------------------------------------------------------------------------
                      '--- Se completato l'incasso della rimessa diretta
                      '--- mette il rimanente della provvigione come maturato
                      '----------------------------------------------------------------------------------------------
                      If Math.Abs(dTotincass + dOmaggiReali) >= Math.Abs(NTSCDec(!pv_totfatt) - dIvaSplitPayment) Then
                        '--------------------------------------------------------------------------------------------
                        '--- incasso completato
                        '--------------------------------------------------------------------------------------------
                        dTmp2 = ArrDbl(NTSCDec(!pv_provv) - NTSCDec(!pv_impvpag), TrovaNdec(0))
                      Else
                        '--------------------------------------------------------------------------------------------
                        '--- Incasso parziale
                        '--------------------------------------------------------------------------------------------
                        If (bMaturaSoloRDTotInc = True) Or (dTotfatt = 0) Or (dTotfattnet = 0) Then
                          dTmp2 = 0
                        Else
                          '------------------------------------------------------------------------------------------
                          '--- In caso contrario allora calcola pro rata il rimanente
                          '------------------------------------------------------------------------------------------
                          If NTSCDec(!pv_provv) = 0 Then
                            dPerc1 = 0
                          Else
                            '----------------------------------------------------------------------------------------
                            '--- dPerc1 è la parte già pagata in precedenza
                            '----------------------------------------------------------------------------------------
                            dPerc1 = NTSCDec(!pv_impvpag) / NTSCDec(!pv_provv)
                          End If
                          dSommaImp = dTotincass - (dTotfattnet * dPerc1)
                          dTmp2 = dSommaImp / dTotfattnet * NTSCDec(!pv_provv)
                          '------------------------------------------------------------------------------------------
                        End If
                        '--------------------------------------------------------------------------------------------
                        If Math.Abs(dTmp2) > Math.Abs(ArrDbl(NTSCDec(!pv_provv) - NTSCDec(!pv_impvpag), TrovaNdec(0))) Then dTmp2 = ArrDbl(NTSCDec(dttProvvig.Rows(j)!pv_provv) - NTSCDec(dttProvvig.Rows(j)!pv_impvpag), TrovaNdec(0))
                        '--------------------------------------------------------------------------------------------
                      End If
                      '----------------------------------------------------------------------------------------------
                      dTmp2 = ArrDbl(dTmp2, TrovaNdec(0))
                      If NTSCDec(!pv_impvmat) <> dTmp2 Then !pv_impvmat = dTmp2
                      '----------------------------------------------------------------------------------------------
                    End If
                  End If    'If bProvvig2 = False Then
                Case "T" '--- Incassato totale (guarda il tipo pagamento)
                  If NTSCDec(!pv_impvmat) <> 0 Then !pv_impvmat = 0
                  strSQL = "SELECT MAX(" & NTSCStr(IIf(NTSCInt(!pv_tippaga) < 3, "sc_datsca", "sc_dtsaldato")) & ") AS dtsaldato," & _
                    " sc_flsaldato FROM scaden" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND sc_conto = " & NTSCInt(!pv_conto) & _
                    " AND sc_annpar = " & NTSCInt(!pv_annpart) & _
                    " AND sc_alfpar = " & CStrSQL(!pv_alfpart) & _
                    " AND sc_numpar = " & NTSCInt(!pv_numpart) & _
                    " GROUP BY sc_flsaldato" & _
                    " ORDER BY sc_flsaldato ASC"
                  dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
                  If dttTmp.Rows.Count > 0 Then
                    If NTSCStr(dttTmp.Rows(0)!sc_flsaldato) = "S" Then
                      '----------------------------------------------------------------------------------------------
                      '--- Scadenze tutte saldate, allora alla scadenza più lontana gli aggiungo
                      '--- tot giorni e se è minore o uguale alla data fine elab aggiorno,
                      '--- in pratica tutte le scadenze devono risultare saldate con data scad < data fine elaborazione
                      '----------------------------------------------------------------------------------------------
                      If NTSCInt(!pv_tippaga) < 3 Then
                        '--------------------------------------------------------------------------------------------
                        '--- RB o tratta
                        '--------------------------------------------------------------------------------------------
                        If Not (DateAdd(DateInterval.Day, NTSCInt(dttTabcage.Rows(i)!tb_giorni), NTSCDate(dttTmp.Rows(0)!dtsaldato)) > NTSCDate(strData)) Then
                          !pv_impvmat = NTSCDec(!pv_provv) - NTSCDec(!pv_impvpag)
                        End If
                      Else
                        '--------------------------------------------------------------------------------------------
                        '--- Rimessa diretta
                        '--------------------------------------------------------------------------------------------
                        If (NTSCDate(dttTmp.Rows(0)!dtsaldato) <= NTSCDate(strData)) Then
                          !pv_impvmat = NTSCDec(!pv_provv) - NTSCDec(!pv_impvpag)
                        End If
                      End If '--- If NTSCInt(dttProvvig.Rows(j)!pv_tippaga) < 3 Then
                    End If
                  End If '---- If dttTmp.Rows.Count > 0 Then
                  '--------------------------------------------------------------------------------------------------
              End Select
            End With
            '--------------------------------------------------------------------------------------------------------
            dttProvvig.AcceptChanges()
            dttOut.ImportRow(dttProvvig.Rows(j))
            dttOut.AcceptChanges()
            '--------------------------------------------------------------------------------------------------------
          Next j
        End If 'If dttProvvig.Rows.Count > 0 Then
        '------------------------------------------------------------------------------------------------------------
        dttProvvig.Clear()
        dttProvvig.Dispose()
        '------------------------------------------------------------------------------------------------------------
      Next i
      '--------------------------------------------------------------------------------------------------------------
      dttTabcage.Clear()
      dttTabcage.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se, dopo il controllo pre-elaborazione, non ci sono dati, esce restituendo True
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono records che hanno Maturato in negativo, esce restituendo True
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Select("pv_impvmat < 0").Length = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Dopo l'elaborazione, elimina, dal DataTable, i records che NON hanno Maturato in negativo
      '--------------------------------------------------------------------------------------------------------------
      For i = (dttOut.Rows.Count - 1) To 0 Step -1
        With dttOut.Rows(i)
          If NTSCInt(!pv_impvmat) >= 0 Then .Delete()
        End With
      Next i
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTabcage.Clear() : dttTabcage.Dispose()
      dttProvvig.Clear() : dttProvvig.Dispose()
      dttTmp.Clear() : dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function TestPreElabora_2(ByVal strDitta As String, ByVal strData As String, _
    ByVal bRbNoChiucli As Boolean, ByRef dttProvvig As DataTable) As Boolean
    Dim bOk As Boolean = False
    Dim i As Integer = 0
    Dim nCausRagg As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim dtrP() As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Prima parte di selezione dati su PROVVIG
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT provvig.*, tb_descage, tb_giorni" & _
        " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
                                       " AND provvig.pv_codage = tabcage.tb_codcage" & _
        " INNER JOIN scaden ON provvig.codditt = scaden.codditt" & _
                         " AND provvig.pv_conto = scaden.sc_conto" & _
                         " AND provvig.pv_anno = scaden.sc_annpar" & _
                         " AND provvig.pv_serie = scaden.sc_alfpar" & _
                         " AND provvig.pv_numdoc = scaden.sc_numpar" & _
                         " AND provvig.pv_numrat = scaden.sc_numrata" & _
                         " AND provvig.pv_impscad = scaden.sc_importo" & _
                         " AND provvig.pv_tippaga = scaden.sc_tippaga" & _
        " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
        " AND tabcage.tb_tippro = 'I'" & _
        " AND scaden.sc_flsaldato = 'S'" & _
        " AND scaden.sc_dtsaldato <= " & CDataSQL(strData) & _
        " AND pv_origine = 'T'" & _
        " AND pv_segno <> 0" & _
        " AND pv_impvmat = 0" & _
        " AND pv_provv <> 0 " & _
        " AND pv_flag = 'N'" & _
        " AND pv_tippaga >= 3"
      dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To (dttProvvig.Rows.Count - 1)
        With dttProvvig.Rows(i)
          !pv_datmatu = NTSCDate(strData)
          !pv_impvmat = NTSCDec(!pv_provv)
        End With
      Next i
      dttProvvig.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Seconda parte
      '--------------------------------------------------------------------------------------------------------------
      If ValCodiceDb("1", strDitta, "TABPECG", "N", "", dttTmp) = True Then
        nCausRagg = NTSCInt(dttTmp.Rows(0)!tb_cauraggef)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT provvig.*, tb_descage, tb_giorni" & _
        " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
                                       " AND provvig.pv_codage = tabcage.tb_codcage" & _
        " INNER JOIN scaden ON provvig.codditt = scaden.codditt" & _
                         " AND provvig.pv_conto = scaden.sc_conto" & _
                         " AND provvig.pv_anno = scaden.sc_annpar" & _
                         " AND provvig.pv_serie = scaden.sc_alfpar" & _
                         " AND provvig.pv_numdoc = scaden.sc_numpar" & _
                         " AND provvig.pv_numrat = scaden.sc_numrata" & _
                         " AND provvig.pv_impscad = scaden.sc_importo" & _
                         " AND provvig.pv_tippaga = scaden.sc_tippaga" & _
        " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
        " AND tabcage.tb_tippro = 'I'" & _
        " AND pv_origine = 'T'" & _
        " AND pv_segno <> 0" & _
        " AND pv_impvmat = 0" & _
        " AND pv_provv <> 0 " & _
        " AND pv_flag = 'N'" & _
        " AND pv_tippaga < 3"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        bOk = True
        For i = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            !pv_datscadeff = NTSCDate(!pv_datscad)
            !pv_scflsaldato = "N"
            !pv_dtsaldato = ""
          End With
          dttTmp.AcceptChanges()
        Next i
        For i = 0 To (dttTmp.Rows.Count - 1)
          dttProvvig.ImportRow(dttTmp.Rows(i))
        Next
        dttProvvig.AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dttProvvig.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      If (nCausRagg <> 0) And (bOk = True) Then
        strSQL = "SELECT DISTINCT pv_progr, pv_conto, pn_datreg, pn_numreg" & _
          " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
                                         " AND provvig.pv_codage = tabcage.tb_codcage" & _
          " INNER JOIN scaden ON provvig.codditt = scaden.codditt" & _
                           " AND provvig.pv_conto = scaden.sc_conto" & _
                           " AND provvig.pv_anno = scaden.sc_annpar" & _
                           " AND provvig.pv_serie = scaden.sc_alfpar" & _
                           " AND provvig.pv_numdoc = scaden.sc_numpar" & _
                           " AND provvig.pv_numrat = scaden.sc_numrata" & _
                           " AND provvig.pv_impscad = scaden.sc_importo" & _
                           " AND provvig.pv_tippaga = scaden.sc_tippaga" & _
          " INNER JOIN prinot ON scaden.codditt = prinot.codditt" & _
                           " AND scaden.sc_dtsaldato = prinot.pn_datreg " & _
                           " AND scaden.sc_rgsaldato = prinot.pn_numreg " & _
          " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
          " AND tabcage.tb_tippro = 'I'" & _
          " AND pv_datscad <= " & CDataSQL(strData) & _
          " AND pv_origine = 'T'" & _
          " AND pv_segno <> 0" & _
          " AND pv_impvmat = 0" & _
          " AND pv_provv <> 0 " & _
          " AND pv_flag = 'N'" & _
          " AND pv_tippaga < 3" & _
          " AND scaden.sc_flsaldato = 'S'" & _
          " AND prinot.pn_causale = " & nCausRagg
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrT As DataRow In dttTmp.Rows
          strSQL = "SELECT sc_datsca, sc_importo, sc_flsaldato, sc_dtsaldato, sc_insolu FROM scaden" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND sc_datreg = " & CDataSQL(NTSCDate(dtrT!pn_datreg)) & _
            " AND sc_numreg = " & NTSCInt(dtrT!pn_numreg).ToString & _
            " AND sc_conto = " & NTSCInt(dtrT!pv_conto).ToString & _
            " AND sc_importo > 0"
          dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp1.Rows.Count > 0 Then
            dtrP = dttProvvig.Select("pv_progr = " & dtrT!pv_progr.ToString)
            If dtrP.Length > 0 Then
              dtrP(0)!pv_datscadeff = NTSCDate(dttTmp1.Rows(0)!sc_datsca)
              If NTSCStr(dttTmp1.Rows(0)!sc_flsaldato) = "S" And NTSCStr(dttTmp1.Rows(0)!sc_insolu) = "N" Then
                dtrP(0)!pv_scflsaldato = "S"
                dtrP(0)!pv_dtsaldato = NTSCDate(dttTmp1.Rows(0)!sc_dtsaldato)
              End If
            End If
            dttProvvig.AcceptChanges()
          End If
          dttTmp1.Clear()
          dttTmp1.Dispose()
        Next
        dttTmp.Clear()
        dttTmp.Dispose()
      End If 'If nCausRagg <> 0 Then
      dtrP = dttProvvig.Select("pv_tippaga < 3" & _
                               " AND pv_origine = 'T'" & _
                               " AND pv_segno <> 0" & _
                               " AND pv_impvmat = 0" & _
                               " AND pv_provv <> 0 " & _
                               " AND pv_flag = 'N'" & _
                               " AND pv_tippaga < 3")
      If dtrP.Length > 0 Then
        For i = 0 To (dtrP.Length - 1)
          With dtrP(i)
            strSQL = "SELECT sc_dtsaldato" & _
              " FROM scaden INNER JOIN prinot ON scaden.codditt = prinot.codditt" & _
                                           " AND scaden.sc_dtsaldato = prinot.pn_datreg" & _
                                           " AND scaden.sc_rgsaldato = prinot.pn_numreg" & _
              " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
              " AND sc_flsaldato = 'S'" & _
              " AND pn_causale <> " & nCausRagg.ToString & _
              " AND sc_conto = " & NTSCInt(!pv_conto) & _
              " AND sc_annpar = " & NTSCInt(!pv_anno) & _
              " AND sc_alfpar = " & CStrSQL(!pv_serie) & _
              " AND sc_numpar = " & NTSCInt(!pv_numdoc) & _
              " AND sc_numrata = " & NTSCInt(!pv_numrat) & _
              " AND sc_importo = " & CDblSQL(NTSCDec(!pv_impscad)) & _
              " AND sc_tippaga = " & NTSCInt(!pv_tippaga)
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then
              !pv_scflsaldato = "S"
              !pv_dtsaldato = NTSCDate(dttTmp.Rows(0)!sc_dtsaldato)
            End If
            dttTmp.Clear()
            dttTmp.Dispose()
          End With
        Next i
      End If
      dttProvvig.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Terza parte
      '--------------------------------------------------------------------------------------------------------------
      dtrP = dttProvvig.Select("pv_scflsaldato = 'S'" & _
                               " AND pv_origine = 'T'" & _
                               " AND pv_segno <> 0" & _
                               " AND pv_impvmat = 0" & _
                               " AND pv_provv <> 0 " & _
                               " AND pv_flag = 'N'" & _
                               " AND pv_tippaga < 3" & _
                               IIf(bRbNoChiucli = True, _
                                 " AND pv_dtsaldato <= " & CDataSQL(strData), _
                                 " AND dateadd(d, tabcage.tb_giorni, pv_datscadeff) <= " & CDataSQL(strData)).ToString)
      If dtrP.Length > 0 Then
        For i = 0 To (dtrP.Length - 1)
          With dtrP(i)
            strSQL = "SELECT TOP 1 sc_conto FROM scaden" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sc_conto = " & NTSCInt(!pv_conto) & _
              " AND sc_annpar = " & NTSCInt(!pv_anno) & _
              " AND sc_alfpar = " & CStrSQL(!pv_serie) & _
              " AND sc_numpar = " & NTSCInt(!pv_numdoc) & _
              " AND sc_numrata = " & NTSCInt(!pv_numrat) & _
              " AND sc_importo = " & CDblSQL(NTSCDec(!pv_impscad)) & _
              " AND sc_tippaga = " & NTSCInt(!pv_tippaga)
            If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then
              !pv_datmatu = NTSCDate(strData)
              !pv_impvmat = NTSCDec(!pv_provv)
            End If
          End With
        Next i
      End If
      dttProvvig.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se, dopo il controllo pre-elaborazione, non ci sono dati, esce restituendo True
      '--------------------------------------------------------------------------------------------------------------
      If dttProvvig.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono records che hanno Maturato in negativo, esce restituendo True
      '--------------------------------------------------------------------------------------------------------------
      dtrP = dttProvvig.Select("pv_impvmat < 0")
      If dtrP.Length = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Dopo l'elaborazione, elimina, dal DataTable, i records che hanno Maturato in negativo
      '--------------------------------------------------------------------------------------------------------------
      For i = (dtrP.Length - 1) To 0 Step -1
        If NTSCInt(dtrP(i)!pv_impvmat) > 0 Then dtrP(i).Delete()
      Next i
      dttProvvig.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      If dttProvvig.Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear() : dttTmp.Dispose()
      dttTmp1.Clear() : dttTmp1.Dispose()
    End Try
  End Function

End Class
