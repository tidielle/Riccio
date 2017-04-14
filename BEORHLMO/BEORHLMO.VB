Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEORHLMO
  Inherits CLE__BASN

  Public oCldHlmo As CLDORHLMO
  Public dsShared As DataSet
  Public bConsentiEvasSuperioreTC As Boolean = False
  Public bModuloTCO As Boolean = False
  Public strTipoDoc As String = ""

  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORHLMO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldHlmo = CType(MyBase.ocldBase, CLDORHLMO)
    oCldHlmo.Init(oApp)

    Return True
  End Function


  Public Overridable Function GetData(ByRef dttMmtrans As DataTable, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                      ByVal bModTCO As Boolean, _
                                      ByVal lCommeca As Integer, ByVal nApprovvig As Integer, ByVal bConfermato As Boolean, _
                                      ByVal strQuery As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                      ByVal bCalcArticoPadre As Boolean, ByVal bNew As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      Return GetData(dttMmtrans, strTipork, nAnno, strSerie, lNumdoc, bModTCO, lCommeca, nApprovvig, bConfermato, _
                     strQuery, strCodart, nFase, bCalcArticoPadre, bNew, dsOut, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GetData(ByRef dttMmtrans As DataTable, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                      ByVal bModTCO As Boolean, _
                                      ByVal lCommeca As Integer, ByVal nApprovvig As Integer, ByVal bConfermato As Boolean, _
                                      ByVal strQuery As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                      ByVal bCalcArticoPadre As Boolean, ByVal bNew As Boolean, ByRef dsOut As DataSet, _
                                      ByVal bNoFigliKit As Boolean) As Boolean
    Dim bRes As Boolean = False
    Dim dttMm As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim dtrMtra() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strCodart1 As String = ""
    Dim strDesart1 As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttMmtrans, strTipork, nAnno, strSerie, lNumdoc, bModTCO, lCommeca, nApprovvig, bConfermato, _
                                             strQuery, strCodart, nFase, bCalcArticoPadre, bNew, dsOut, bNoFigliKit})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttMmtrans = CType(oIn(0), DataTable)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        dsOut = CType(oIn(14), DataSet)
        Return CBool(oOut)
      End If
      '----------------


      bModuloTCO = bModTCO
      strTipoDoc = strTipork
      bConsentiEvasSuperioreTC = CBool(Val(oCldHlmo.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "ConsentiEvasSuperioreTC", "0", " ", "0"))) 'NON DOCUMENTARE: se abilitata consente di evadere un articolo T&C con quantità per taglia superiore a quelle previste in ordine/nota

      '--------------------------
      'ottengo le righe di ordine ancora inevase
      bRes = oCldHlmo.GetData(strDittaCorrente, strTipork, bModTCO, lCommeca, nApprovvig, _
                              bConfermato, strQuery, strCodart, nFase, dsOut, bNoFigliKit)
      If bRes = False Then Return False
      dsOut.AcceptChanges()

      If bNew = False Then
        '------------------------------
        '1)aggiunge al datatable degli ordini la parte già evasa dal documento precedentemente salvato
        If Not oCldHlmo.GetOrdiniEvasiDaMovmag(strDittaCorrente, strTipork, nAnno, strSerie, lNumdoc, dttMm) Then
          Return False
        End If
        For Each dtrMM As DataRow In dttMm.Rows
          If dtrMM!mm_salcon.ToString = "S" Then
            '1.A)se la riga corrente evade a saldo la riga d'ordine lo fa sparire
            'non fa nulla (non è che può riaprire l'ordine se già chiuso)
            'non lo selezionerebbe poi
          Else
            '1.B)se la riga corrente NON evade a saldo diminuisce la quantità già evasa (mo_quaeva)
            '    e aumenta quantita possibile la evadere (mo_quadaeva)
            '    non si tocca il resiuso (mo_qtares & mo_valres) che rimane statico
            dtrT = dsOut.Tables("MOVORD").Select("mo_tipork = " & CStrSQL(dtrMM!mm_ortipo) & _
                                                " AND mo_anno = " & dtrMM!mm_oranno.ToString & _
                                                " AND mo_serie = " & CStrSQL(dtrMM!mm_orserie) & _
                                                " AND mo_numord = " & dtrMM!mm_ornum.ToString & _
                                                " AND mo_riga = " & dtrMM!mm_orriga.ToString)
            If dtrT.Length > 0 Then
              'Se nota di prelievo tocco il prenotato, altrimenti l'evaso
              If strTipork = "W" Then
                dtrT(0)!mo_quapre = ArrDbl(NTSCDec(dtrT(0)!mo_quapre) - NTSCDec(dtrMM!mm_quant), 3)

              Else
                dtrT(0)!mo_quaeva = ArrDbl(NTSCDec(dtrT(0)!mo_quaeva) - NTSCDec(dtrMM!mm_quant), 3)

                'Se tale riga ha anche i riferiemnti alla nota di prel. li scala il prenotato
                If NTSCInt(dtrMM!mm_npnum) > 0 Then
                  dtrT(0)!mo_quapre = ArrDbl(NTSCDec(dtrT(0)!mo_quapre) + NTSCDec(dtrMM!mm_quant), 3)
                End If
              End If

              dtrT(0)!xx_quadaeva = ArrDbl(NTSCDec(dtrT(0)!xx_quadaeva) + NTSCDec(dtrMM!mm_quant), 3)

            End If    'If dtrT.Length > 0 Then
          End If    'If dtrMM!ec_salcon.ToString = "S" Then
        Next    'For Each dtrMM As DataRow In dttMm.Rows
      End If    'If bNew = False Then

      '--------------------------
      'se chiamato da movmag,
      'Ora inizia ad operare le rettifiche sul temporaneo per rendere "dinamico" lo zoom
      If Not dttMmtrans Is Nothing Then
        If dttMmtrans.Rows.Count > 0 Then
          '--------------------------
          '2)poi storna tutte le righe presenti nel temporaneo (solo le righe agganciate a ordine)
          dtrMtra = dttMmtrans.Select("ec_ornum <> 0", "ec_ortipo, ec_oranno, ec_orserie, ec_ornum, ec_orriga")
          For i = 0 To dtrMtra.Length - 1
            dtrT = dsOut.Tables("MOVORD").Select("mo_tipork = " & CStrSQL(dtrMtra(i)!ec_ortipo) & _
                                                " AND mo_anno = " & dtrMtra(i)!ec_oranno.ToString & _
                                                " AND mo_serie = " & CStrSQL(dtrMtra(i)!ec_orserie) & _
                                                " AND mo_numord = " & dtrMtra(i)!ec_ornum.ToString & _
                                                " AND mo_riga = " & dtrMtra(i)!ec_orriga.ToString)
            If dtrT.Length > 0 Then
              If dtrMtra(i)!ec_salcon.ToString = "S" Then
                '---------------------
                '2.A)se la riga corrente evade a saldo la riga d'ordine lo fa sparire
                dtrT(0).Delete()
              Else
                '------------------
                '2.B)se la riga corrente NON evade a saldo aumenta la quantità già evasa (mo_quaeva)
                '    e diminuisce quantita possibile la evadere (mo_quadaeva)
                '    non si tocca il resiuso (mo_qtares & mo_valres) che rimane statico

                '------------------
                'Se nota di prelievo tocco il prenotato, altrimenti l'evaso
                If strTipork = "W" Then
                  dtrT(0)!mo_quapre = ArrDbl(NTSCDec(dtrT(0)!mo_quapre) + NTSCDec(dtrMtra(i)!ec_quant), 3)
                Else
                  dtrT(0)!mo_quaeva = ArrDbl(NTSCDec(dtrT(0)!mo_quaeva) + NTSCDec(dtrMtra(i)!ec_quant), 3)
                  '---------------
                  'Se tale riga ha anche i riferiemnti alla nota di prel. li scala il prenotato
                  If NTSCInt(dtrMtra(i)!ec_npnum) > 0 Then
                    dtrT(0)!mo_quapre = ArrDbl(NTSCDec(dtrT(0)!mo_quapre) - NTSCDec(dtrMtra(i)!ec_quant), 3)
                  End If
                End If
                dtrT(0)!xx_quadaeva = ArrDbl(NTSCDec(dtrT(0)!xx_quadaeva) - NTSCDec(dtrMtra(i)!ec_quant), 3)
              End If
            End If    'If dtrT.lenght > 0 Then
          Next    'For i = 0 To dtrMtra.Length - 1

          '---------------------
          '2.C)se alla fine la qtà già evasa (mo_quaeva) >= quantità ordinata (mo_quant) elimina la riga
          '    il flag di saldo riga ordine nel temporaneo potrebbe ancora non essere valido
          '    visto che è controllato al salvataggio del documento
          dtrT = dsOut.Tables("MOVORD").Select("mo_quant > 0 " & IIf(strTipork = "W", "AND mo_quaeva + mo_quapre >= mo_quant", "AND mo_quaeva >= mo_quant").ToString)
          For i = 0 To dtrT.Length - 1
            dtrT(i).Delete()
          Next

        End If    'If dttMmtrans.Rows.Count > 0 Then
      End If    'If Not dttMmtrans Is Nothing Then


      '-----------------------
      'compilo/correggo xx_prezzonetto (fino a qui c'è mo_prezzo)
      dsOut.Tables("MOVORD").AcceptChanges()
      For Each dtrT1 As DataRow In dsOut.Tables("MOVORD").Rows
        With dtrT1
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont1)) / 100)
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont2)) / 100)
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont3)) / 100)
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont4)) / 100)
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont5)) / 100)
          !xx_prezzonetto = NTSCDec(!xx_prezzonetto) - ((NTSCDec(!xx_prezzonetto) * NTSCDec(!mo_scont6)) / 100)
          !xx_prezzonetto = ArrDbl(NTSCDec(!xx_prezzonetto), oCldHlmo.TrovaNdec(0))

          'se serve determino l'articolo padre 
          If bCalcArticoPadre And !mo_tipork.ToString = "Y" Then
            oCldHlmo.GetArticoPadre(strDittaCorrente, NTSCInt(!mo_annoor), !mo_serieor.ToString, _
                                    NTSCInt(!mo_numordor), NTSCInt(!mo_rigaor), strCodart1, strDesart1)
            !xx_articopadre = strCodart1
            !xx_desarticopadre = strDesart1
          End If
        End With
      Next

      dsOut.AcceptChanges()

      dsShared = dsOut

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      Try
        RemoveHandler dsShared.Tables("MOVORD").ColumnChanging, AddressOf BeforeColUpdate
        RemoveHandler dsShared.Tables("MOVORD").ColumnChanged, AddressOf AfterColUpdate
      Catch ex As Exception
      End Try

      AddHandler dsShared.Tables("MOVORD").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("MOVORD").ColumnChanged, AddressOf AfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      If e.Column.ColumnName.ToLower.IndexOf("xx_quadaeva") > -1 And e.Column.ColumnName.Length > 11 Then
        'sono le colonne xx_quadaeva del TCO
        BeforeColUpdate_xx_quadaevatco(sender, e)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_xx_quadaeva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCDec(e.ProposedValue) = 0 Then
        e.Row!xx_seleziona = "N"
      Else
        e.Row!xx_seleziona = "S"
      End If

      If NTSCDec(e.ProposedValue) < NTSCDec(e.Row!xx_qtares) Then
        e.Row!xx_flevasdaeva = "C"
      Else
        e.Row!xx_flevasdaeva = "S"
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_xx_quadaevatco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuares As Decimal = 0
    Try
      'la quantità da evadere non può superare la quantità residua
      If Not bConsentiEvasSuperioreTC Then
        dQuares = (NTSCDec(e.Row("mo_quant" & e.Column.ColumnName.Substring(11))) - NTSCDec(e.Row("mo_quaeva" & e.Column.ColumnName.Substring(11))))
        If NTSCDec(e.ProposedValue) > dQuares Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128584288510312500, "ATTENZIONE" & vbCrLf & _
                        "La quantità da evadere non può essere superiore a |" & dQuares.ToString(oApp.FormatQta) & "|.")))
          Return
        End If
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      If e.Column.ColumnName.ToLower.IndexOf("xx_quadaeva") > -1 And e.Column.ColumnName.Length > 11 Then
        'sono le colonne xx_quadaeva del TCO
        AfterColUpdate_xx_quadaevatco(sender, e)
      End If


    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_xx_quadaevatco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuadaeva As Decimal = 0
    Dim i As Integer = 0
    Try
      'ricalcolo il totale dellq qta da evadere
      For i = 1 To 24
        dQuadaeva += NTSCDec(e.Row("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)))
      Next
      If dQuadaeva < 0 Then dQuadaeva = 0
      e.Row!xx_quadaeva = dQuadaeva

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function edCommeca_Validated(ByVal nCod As Integer, ByRef strDesr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDesr = ""
        Return True
      End If

      bOut = oCldHlmo.ValCodiceDb(nCod.ToString, strDittaCorrente, "COMMESS", "N", strDesr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127965883462031250, "Codice commessa |'" & nCod.ToString & "'| inesistente")))
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edAppro_Validated(ByVal nCod As Integer, ByRef strDesr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDesr = ""
        Return True
      End If

      bOut = oCldHlmo.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABAPPR", "N", strDesr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583600469687500, "Codice approvvigionatore |'" & nCod.ToString & "'| inesistente")))
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("MOVORD").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim bResult As Boolean = True
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not TestPreSalva() Then Return False

      dsShared.Tables("MOVORD").AcceptChanges()

      Return bResult
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim bQtaTagliaEccesso As Boolean = False
    Dim nIDTaglia As Integer = 0
    Dim dQuantTmp As Decimal = 0
    Dim dQuaevaTmp As Decimal = 0
    Dim dQuapreTmp As Decimal = 0


    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrTmp = dsShared.Tables("MOVORD").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCDec(dtrTmp(i)!xx_quadaeva) > NTSCDec(dtrTmp(i)!xx_qtares) And dtrTmp(i)!xx_flevasdaeva.ToString = "C" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128584300425781250, "Attenzione!" & vbCrLf & _
                          "E' obbligatorio evadere a saldo la riga" & vbCrLf & _
                          "quando la quantità da evadere supera o è uguale a quella evadibile.")))
          Return False
        End If

        If bModuloTCO Then
          bQtaTagliaEccesso = False
          nIDTaglia = 0
          For n = 1 To 24
            dQuantTmp = NTSCDec(dtrTmp(i)("mo_quant" & n.ToString.PadLeft(2, "0"c)))
            dQuaevaTmp = NTSCDec(dtrTmp(i)("mo_quaeva" & n.ToString.PadLeft(2, "0"c)))
            dQuapreTmp = NTSCDec(dtrTmp(i)("mo_quapre" & n.ToString.PadLeft(2, "0"c)))

            If strTipoDoc = "W" Then
              'nota di prelievo
              If NTSCDec(dtrTmp(i)("xx_quadaeva" & n.ToString.PadLeft(2, "0"c))) > ArrDbl(dQuantTmp - dQuaevaTmp - dQuapreTmp, 3) Then
                bQtaTagliaEccesso = True
                nIDTaglia = n
                Exit For
              End If
            Else
              'normale documento
              If NTSCDec(dtrTmp(i)("xx_quadaeva" & n.ToString.PadLeft(2, "0"c))) > ArrDbl(dQuantTmp - dQuaevaTmp, 3) Then
                bQtaTagliaEccesso = True
                nIDTaglia = n
                Exit For
              End If
            End If
          Next
          If bConsentiEvasSuperioreTC Then
            If bQtaTagliaEccesso And dtrTmp(i)!xx_flevasdaeva.ToString = "C" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129557376434072266, "Attenzione!" & vbCrLf & _
                              "E' obbligatorio evadere a saldo la riga" & vbCrLf & _
                              "quando la quantità da evadere sulla taglia |" & nIDTaglia & _
                              "| supera quella evadibile residua per quella taglia.")))
              Return False
            End If
          End If    'If bConsentiEvasSuperioreTC Then
        End If    'If bModuloTCO Then
      Next

      Return True


      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function EsisteOrdineImpegno(ByVal strTipork As String, ByVal nAnno As Integer, _
    ByVal strSerie As String, ByVal lNumord As Integer, ByVal lRiga As Integer, ByRef lRigaOut As Integer) As Boolean
    Try
      Return oCldHlmo.EsisteOrdineImpegno(strDittaCorrente, strTipork, nAnno, strSerie, lNumord, lRiga, lRigaOut)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function IsDocRetail(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return ocldBase.IsDocRetail(strDitta, strTipork, nAnno, strSerie, lNumdoc)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function IsDocRetailNew(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return ocldBase.IsDocRetailNew(strDitta, strTipork, nAnno, strSerie, lNumdoc)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function


  Public Overridable Function TrovaFigliKit(ByVal dtrRow As DataRow) As DataTable
    Try
      Dim dttMovOrd As DataTable = oCldHlmo.TrovaFigliKit(dtrRow, bModuloTCO, strTipoDoc)
      'Scorre tutte le righe di kit figlio da importare nell'ordine.
      Dim dRapportoAssegnato As Decimal = NTSCDec(dtrRow!xx_quadaeva) / NTSCDec(dtrRow!xx_qtares)
      For Each dtrRiga As DataRow In dttMovOrd.Rows
        dtrRiga!xx_quadaeva = NTSCDec(dtrRiga!xx_quadaeva) * dRapportoAssegnato
        'Se la riga padre ha il flag di evaso, questo va riportato su tutte le righe figlio
        If NTSCStr(dtrRow!xx_flevasdaeva) = "S" Then
          dtrRiga!xx_flevasdaeva = "S"
        Else
          If NTSCDec(dtrRiga!xx_quadaeva) < NTSCDec(dtrRiga!xx_qtares) Then dtrRiga!xx_flevasdaeva = "C" 'Gestione del flag evaso se sto evadendo una quantità minore
        End If
      Next

      Return dttMovOrd
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return Nothing
  End Function
End Class
