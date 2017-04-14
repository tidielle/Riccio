Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__SBSL
  Inherits CLE__BASE

  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public oCldSbsl As CLD__SBSL

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__SBSL"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldSbsl = CType(MyBase.ocldBase, CLD__SBSL)
    oCldSbsl.Init(oApp)
    Return True
  End Function

  Public Overridable Shadows Function Apri(ByVal strDitta As String, ByVal strDtStart As String, _
                                           ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                           ByVal strUser As String, ByVal strTipoEventi As String, _
                                           ByRef dsOut As DataSet) As Boolean

    Try
      Return Apri(strDitta, strDtStart, strDtEnd, strDeviceName, strUser, strTipoEventi, dsOut, True)
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
  Public Overridable Shadows Function Apri(ByVal strDitta As String, ByVal strDtStart As String, _
                                           ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                           ByVal strUser As String, ByVal strTipoEventi As String, _
                                           ByRef dsOut As DataSet, ByVal bFormLoad As Boolean) As Boolean
    'se in uno stesso giorno sono presenti più connessioni/disconnessioni ... mantengo record separati
    'strTipoEventi : T = tutti, P = programmi, A = accessi

    'C = connesso
    'C1 = disconnesso da connessione aperta giorni precedenti 'campo non presente sul DB'
    'C2 = connesso da giorni precedenti che non si è disconnesso nel giorno 'campo non presente sul DB'
    'D = disconnesso
    'S = sospensione
    'R = riconnessione
    'A = avvio child
    'A1 = chiuso child avviato giorni precedenti 'campo non presente sul DB' 
    'A2 = child avviato da giorni precedenti che non si è chiuso nel giorno 'campo non presente sul DB'
    'X = chiusura child

    Dim dtrT1() As DataRow = Nothing
    Dim nRec As Integer = 0
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim strFilter As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDtStart, strDtEnd, strDeviceName, strUser, strTipoEventi, dsOut, bFormLoad})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(6), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      If oCldSbsl.Apri_CountRk(strDitta, strDtStart, strDtEnd, strDeviceName, strUser, strTipoEventi) > 2000 Then
        If bFormLoad Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130923382516106927, _
                           "Attenzione: la ricerca ha restituito più di 2000 record. Verranno prelevati solo i primi 2000 record." & vbCrLf & _
                           "Per prelevare più record, utilizzare il comando 'Apri'")))
        Else
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130923382516106928, "Attenzione: la ricerca ha restituito più di 2000 record. Proseguire (l'attesa potrebbe essere lunga)?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
      End If

      If Not oCldSbsl.Apri(strDitta, strDtStart, strDtEnd, strDeviceName, strUser, strTipoEventi, dsOut) Then Return False

      'calcolo il tempo dall'evento a fine giornata
      'se l'evento si è verificato oggi, calcolo fino alla data/ora corrente
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Rows
        dtrT!xx_data = NTSCDate(NTSCDate(dtrT!sb_ultagg).ToShortDateString) 'solo la data
        If NTSCDate(dtrT!sb_ultagg).ToShortDateString <> DateTime.Now.ToShortDateString Then
          dtrT!xx_durata = NTSCDate(NTSCDate(dtrT!sb_ultagg).ToShortDateString & " 23:59:59").Subtract(NTSCDate(dtrT!sb_ultagg)).TotalSeconds
        Else
          dtrT!xx_durata = DateTime.Now.Subtract(NTSCDate(dtrT!sb_ultagg)).TotalSeconds
        End If
      Next

      'ora devo calcolare i tempi e raggruppare per giorno
      'per ogni connessione, calcolo il tempo dalal connessione fino a fine giornata
      'se dopo la connessione si è verificata una disconnessione, ricalcolo il tempo 
      'togliendo la parte dalla disconness fino a fine giornata
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Select("sb_evento IN('C', 'R', 'A')", "sb_ultagg")

        'filtri per la ricerca di record collegati
        strFilter = " AND sb_devicename = " & CStrSQL(dtrT!sb_devicename) & _
                    " AND sb_idsession = " & dtrT!sb_idsession.ToString & _
                    " AND sb_porta = " & dtrT!sb_porta.ToString & _
                    " AND sb_profilo = " & CStrSQL(dtrT!sb_profilo.ToString) & _
                    " AND sb_db = " & CStrSQL(dtrT!sb_db.ToString) & _
                    " AND sb_ditta = " & CStrSQL(dtrT!sb_ditta.ToString) & _
                    " AND sb_user = " & CStrSQL(dtrT!sb_user.ToString) & _
                    " AND sb_child = " & CStrSQL(dtrT!sb_child.ToString) & _
                    " AND sb_ultagg > " & CDataOraSQL(NTSCDate(dtrT!sb_ultagg))

        If NTSCStr(dtrT!sb_evento) = "A" Then
          dtrT!xx_evento = "Programma"
          'cerco la chisura del programma
          dtrT1 = dsOut.Tables("SBSLOG").Select("sb_evento = 'X' " & strFilter, _
                                                "sb_ultagg", DataViewRowState.CurrentRows)
        Else
          dtrT!xx_evento = "Connesso"
          'cerco le sospensioni/disconnessioni
          dtrT1 = dsOut.Tables("SBSLOG").Select("sb_evento IN('D', 'S') " & strFilter, _
                                                "sb_ultagg", DataViewRowState.CurrentRows)
        End If

        If dtrT1.Length > 0 AndAlso NTSCDate(dtrT1(0)!sb_ultagg).ToShortDateString = NTSCDate(dtrT!sb_ultagg).ToShortDateString Then
          'mi sono disconnesso nello stesso giorno: cancello la disconnessione e memorizzo la durata del tempo di connessione
          dtrT!xx_durata = NTSCDec(dtrT!xx_durata) - NTSCDec(dtrT1(0)!xx_durata)
          dtrT!xx_disconn = "S" 'memorizzo che a fronte di una connessione c'è stata una disconnessione in giornata
          dtrT1(0).Delete()
        End If
      Next
      dsOut.Tables("SBSLOG").AcceptChanges()


      'se sono rimaste delle disconnessioni, vuol dire che il giorno prima non mi ero disconnesso:
      'converto il record da disconnesso a connessione da gg precedente
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Select("sb_evento IN ('D', 'S', 'X')")
        If NTSCStr(dtrT!sb_evento) = "X" Then
          dtrT!xx_evento = "chiuso da avvio di GG prec."
          dtrT!sb_evento = "A1"
        Else
          dtrT!xx_evento = "Disconnesso da conn. di GG prec."
          dtrT!sb_evento = "C1"
        End If

        'tempo da inizio giornata
        dtrT!xx_disconn = "S"
        dtrT!xx_durata = NTSCDate(dtrT!sb_ultagg).Subtract(NTSCDate(NTSCDate(dtrT!sb_ultagg).ToShortDateString)).TotalSeconds
      Next

      'se nei gg prec avevo delle connessioni senza disconnessione, devo aggiungere i record di 'connesso'
      'fino alla data di disconnessione
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Select("sb_evento IN ('C', 'R', 'A') AND xx_disconn = 'N'")
        'cerco se c'è stata uan disconnessione

        'filtri per la ricerca di record collegati
        strFilter = " AND sb_devicename = " & CStrSQL(dtrT!sb_devicename) & _
                    " AND sb_idsession = " & dtrT!sb_idsession.ToString & _
                    " AND sb_porta = " & dtrT!sb_porta.ToString & _
                    " AND sb_profilo = " & CStrSQL(dtrT!sb_profilo.ToString) & _
                    " AND sb_db = " & CStrSQL(dtrT!sb_db.ToString) & _
                    " AND sb_ditta = " & CStrSQL(dtrT!sb_ditta.ToString) & _
                    " AND sb_user = " & CStrSQL(dtrT!sb_user.ToString) & _
                    " AND sb_child = " & CStrSQL(dtrT!sb_child.ToString) & _
                    " AND sb_ultagg > " & CDataOraSQL(NTSCDate(dtrT!sb_ultagg))


        If NTSCStr(dtrT!sb_evento) = "A" Then
          dtrT1 = dsOut.Tables("SBSLOG").Select("sb_evento = 'A1' " & strFilter, _
                                                "sb_ultagg", DataViewRowState.CurrentRows)
        Else
          dtrT1 = dsOut.Tables("SBSLOG").Select("sb_evento = 'C1' " & strFilter, _
                                                "sb_ultagg", DataViewRowState.CurrentRows)
        End If

        If dtrT1.Length > 0 Then
          'devo aggiungere i giorni tra connessione e disconnessione
          nRec = NTSCInt(NTSCDate(dtrT1(0)!sb_ultagg).Subtract(NTSCDate(dtrT!sb_ultagg)).TotalDays) - 1
        Else
          'sono a tutt'oggi connesso
          nRec = NTSCInt(DateTime.Now.Subtract(NTSCDate(dtrT!sb_ultagg)).TotalDays)
        End If

        For i = 1 To nRec
          Dim dtrN As DataRow = dsOut.Tables("SBSLOG").NewRow
          For Each oCol As DataColumn In dsOut.Tables("SBSLOG").Columns
            dtrN(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next

          If NTSCStr(dtrT!sb_evento) = "A" Then
            dtrT!xx_evento = "Programma continua"
            dtrT!sb_evento = "A2"
          Else
            dtrT!xx_evento = "Connesso continua"
            dtrT!sb_evento = "C2"
          End If

          dtrT!xx_data = NTSCDate(dtrT!xx_data).AddDays(1)
          dtrT!sb_ultagg = NTSCDate(dtrT!xx_data).AddDays(1)
          If NTSCDate(dtrT!xx_data).ToShortDateString = DateTime.Now.ToShortDateString Then
            'tempo da inizio giornata
            dtrT!xx_durata = DateTime.Now.Subtract(NTSCDate(DateTime.Now.ToShortDateString)).TotalSeconds
          Else
            dtrT!xx_durata = 24 * 3600
          End If
          dtrT!xx_disconn = "N"
          dsOut.Tables("SBSLOG").Rows.Add(dtrN)
        Next
      Next


      'cancello tutti i giorni che sono al di fuori dal range che avevo chiesto
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Select("sb_ultagg < " & CDataSQL(strDtStart))
        dtrT.Delete()
      Next
      dsOut.Tables("SBSLOG").AcceptChanges()

      'converto i tempi di xx_durata da secondi in ore,minutisecondi
      For Each dtrT As DataRow In dsOut.Tables("SBSLOG").Select("xx_durata <> 0")
        If NTSCDec(dtrT!xx_durata) < 0 Then dtrT!xx_durata = 0
        dtrT!xx_durata = NTSCDec(dtrT!xx_durata) / 3600
        dtrT!xx_durata = ConvOra100Ora60(NTSCDec(dtrT!xx_durata))
      Next
      dsOut.Tables("SBSLOG").AcceptChanges()

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


  Public Overridable Shadows Function Cancella(ByVal strDitta As String, ByVal strDtStart As String, _
                                           ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                           ByVal strUser As String, ByVal strTipoEventi As String) As Boolean
    Try
      Return oCldSbsl.Cancella(strDitta, strDtStart, strDtEnd, strDeviceName, strUser, strTipoEventi)

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


End Class
