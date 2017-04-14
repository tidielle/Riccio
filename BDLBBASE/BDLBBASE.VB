Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class CLDLBBASE
  Inherits MarshalByRefObject             'serve per poter gestire il remoting
  'base per tutti i dal
  '----------------------------------------------
  'INIZIALIZZATORI DI TUTTI I DAL
  Public oCldBase As CLD__BASE
  Public oScriptE As INT__SCRIPT = Nothing 'puntatore alla dll sidecar dell'entity che mi ha istanziato
  Public fQuery As StreamWriter

  Private oApp_P As CLE__APP
  Public ReadOnly Property oApp() As CLE__APP
    Get
      Return oApp_P
    End Get
  End Property

  Public CustomClass As Boolean = False             'se TRUE la classe istanziata non è quella standard NTS ma una ereditata

  Private lDbVersionNeededMajor As Integer = 0         'versione minima di database richiesta per poter far funzionare correttamente il child
  Private lDbVersionNeededMinor As Integer = 0         'versione minima di database richiesta per poter far funzionare correttamente il child
  Public Sub SetDbVersionNeeded(ByVal lMajor As Integer, ByVal lMinor As Integer)
    'questa funzione viene chiamata solo dai vari entity che in corso d'anno richiedono un DB modificato per operare
    'CASO PARTICOLARE:
    'ipotizziamo di aver modificato beveboll per richiedere almeno un DB di versione 18.1
    'successivamente modifichiamo bemgdocu per richiedere almeno un DB di versione 18.2
    'all’avvio di beveboll andrà verificata ed acquisita la versione più alta tra tutte le dll ereditate!
    If lDbVersionNeededMajor > lMajor Then Return
    If lDbVersionNeededMajor = lMajor And lDbVersionNeededMinor > lMinor Then Return
    lDbVersionNeededMajor = lMajor
    lDbVersionNeededMinor = lMinor
  End Sub
  Public ReadOnly Property DbVersionNeededMajor() As Integer
    Get
      Return lDbVersionNeededMajor
    End Get
  End Property
  Public ReadOnly Property DbVersionNeededMinor() As Integer
    Get
      Return lDbVersionNeededMinor
    End Get
  End Property

  Public Overridable Sub Init(ByVal Applic As CLE__APP, ByVal cldBase As CLD__BASE)
    oApp_P = Applic
    oCldBase = cldBase

    '------------------------------------
    'verifico se la versione del DB è almeno quella richiesta dalla DLL
    'CASO PARTICOLARE:
    'ipotizziamo di aver modificato beveboll per richiedere almeno un DB di versione 18.1
    'successivamente modifichiamo bemgdocu per richiedere almeno un DB di versione 18.2
    'all’avvio di beveboll andrà verificata ed acquisita la versione più alta tra tutte le dll ereditate!
    If oApp.Db.ServerDB.Trim <> "" Then
      Dim lDBMajor As Integer = 0
      Dim lDBMinor As Integer = 0
      lDBMajor = NTSCInt(oApp.Db.Release.Split("."c)(0))
      lDBMinor = NTSCInt(oApp.Db.Release.Split("."c)(1))

      If Me.lDbVersionNeededMajor < lDBMajor Then
        'posso continuare: dll di versione precedente a quella del DB (es dll net 2011 su net 2012)
      ElseIf Me.lDbVersionNeededMajor = lDBMajor And _
             Me.lDbVersionNeededMinor <= lDBMinor Then
        'posso ancora continuare
      Else
        'non posso continuare: la dll richiede un DB di versione più aggiornata!
        Throw New NTSException(oApp.Tr(Me, 129796687221992703, "ATTENZIONE: la DLL |'" & Me.GetType.ToString & _
                              "'| (o una sua ereditata) richiede un database azienda di versione almeno |'" & _
                              Me.lDbVersionNeededMajor.ToString("00") & "." & Me.lDbVersionNeededMinor.ToString("00") & "'|"))
      End If
    End If

    'test per remoting 
    '.Show(oApp.Db.Tipo + " - " + oApp.Dir + " - " + oApp.User.Nome + " - " + oApp.Db.Nome)
  End Sub

  Public Overridable Function LegNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal bCrea As Boolean, _
                          ByRef dbConn As DbConnection) As Integer
    ' restiuisce il prossimo progressivo tb_numprog, oppure 0 se errore..
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim lProgr As Integer = 0
    Try
      strSQL = "SELECT tb_numprog FROM tabnuma WHERE tb_numtipo = " & CStrSQL(strTipo) & _
              " AND tb_numserie = " & CStrSQL(strSerie) & _
              " AND tb_numcodl = " & lCodl & _
              " AND codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA", Nothing, dbConn)

      If dsTmp.Tables("TABNUMA").Rows.Count > 0 Then
        lProgr = NTSCInt(dsTmp.Tables("TABNUMA").Rows(0)!tb_numprog) + 1
      Else
        If bCrea Then
          strSQL = "INSERT INTO tabnuma (codditt, tb_numtipo, tb_numserie, tb_numcodl, tb_numprog, tb_numprog2, " & _
                   "tb_numdata, tb_numdata2, tb_numflag, tb_numflag2, tb_numval, tb_numval2 ) " & _
                   " VALUES (" & CStrSQL(strDitta) & ", " & _
                   CStrSQL(strTipo) & ", " & _
                   CStrSQL(strSerie) & ", " & _
                   lCodl & ", 0, 0, null, null, null, null, 0, 0)"
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = 1
        End If
      End If

      Return lProgr
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal lNewProg As Integer, ByVal bControlla As Boolean, _
                          ByVal bVisMsg As Boolean, ByRef strReturnMessage As String, _
                          Optional ByVal dbConn As DbConnection = Nothing) As Integer
    'aggiorna il numeratore: 
    'se la numerazione non è stata attivata e non c'è l'opzione per crearla scatena una eccezione
    'se il numeratore è già stato utilizzato scatena un una eccezione

    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim lProgr As Integer = 0

    Try
      strReturnMessage = ""

      strSQL = "SELECT tb_numprog FROM tabnuma WHERE tb_numtipo = " & CStrSQL(strTipo) & _
        " AND tb_numserie = " & CStrSQL(strSerie) & _
        " AND tb_numcodl = " & lCodl & _
        " AND codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA", Nothing, dbConn)

      If dsTmp.Tables("TABNUMA").Rows.Count = 0 Then
        'non c'è il record: devo generare l'errore (se è impostato di dare il messaggio)
        strReturnMessage = oApp.Tr(Me, 127792249782812500, "Attenzione: impossibile aggiornare il numero progressivo." & vbCrLf & _
                           "Numerazione |'" & CLN__STD.TraduciTipoNumerazione(strTipo) & "', serie '" & strSerie & "', cod. " & lCodl & "| mancante.")
      Else
        'c'è il record: devo verificare se il progressivo è già stato usato
        If bControlla Then
          If NTSCInt(dsTmp.Tables("TABNUMA").Rows(0)!tb_numprog) + 1 > lNewProg Then
            lNewProg = NTSCInt(dsTmp.Tables("TABNUMA").Rows(0)!tb_numprog) + 1
            If bVisMsg Then
              strReturnMessage = oApp.Tr(Me, 129100919179511719, "Attenzione: il numero progressivo attribuito in precedenza è già stato " & _
                                 "utilizzato da un altro utente. Il nuovo numero assegnato è |" & lNewProg & "|")
            End If
          End If
          strSQL = "UPDATE TABNUMA SET  tb_numprog = " & lNewProg & _
                   " WHERE tb_numtipo = " & CStrSQL(strTipo) & _
                   " AND tb_numserie = " & CStrSQL(strSerie) & _
                   " AND tb_numcodl= " & lCodl & _
                   " AND codditt = " & CStrSQL(strDitta)
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = lNewProg
        Else
          strSQL = "UPDATE TABNUMA SET  tb_numprog = " & lNewProg & _
                   " WHERE tb_numtipo = " & CStrSQL(strTipo) & _
                   " AND tb_numserie = " & CStrSQL(strSerie) & _
                   " AND tb_numcodl= " & lCodl & _
                   " AND codditt = " & CStrSQL(strDitta)
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = lNewProg
        End If    'If bControlla Then

      End If    'If dsTmp.Tables("TABNUMA").Rows.Count = 0 Then

      Return lNewProg

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DelNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal lProg As Integer, ByRef dbConn As DbConnection) As Boolean
    'se il numero passato in input è uguale al progressivo salvato, diminuisce il progressivo
    'serve per fare in modo che se cancello un documento (ad esempio un ordine fornitore)
    'se è l'ultimo documento fatto il numeratore venga aggiornato in automatico
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT tb_numprog FROM tabnuma WHERE tb_numtipo = " & CStrSQL(strTipo) & _
              " AND tb_numserie = " & CStrSQL(strSerie) & _
              " AND tb_numcodl = " & lCodl & _
              " AND codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA", Nothing, dbConn)
      If dsTmp.Tables("TABNUMA").Rows.Count > 0 Then
        If NTSCInt(dsTmp.Tables("TABNUMA").Rows(0)!tb_numprog) = lProg Then
          strSQL = "UPDATE tabnuma SET tb_numprog = tb_numprog -1 " & _
                  " WHERE tb_numtipo = " & CStrSQL(strTipo) & _
                  " AND tb_numserie = " & CStrSQL(strSerie) & _
                  " AND tb_numcodl = " & lCodl & _
                  " AND codditt = " & CStrSQL(strDitta)
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          If strTipo = "T" Then
            'per i carichi da prod riporto indietro anche il numeratore degli scarichi collegati
            strSQL = "UPDATE tabnuma SET tb_numprog = tb_numprog -1 " & _
                    " WHERE tb_numtipo = 'U'" & _
                    " AND tb_numserie = " & CStrSQL(strSerie) & _
                    " AND tb_numcodl = " & lCodl & _
                    " AND codditt = " & CStrSQL(strDitta)
            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          If strTipo = "H" Then
            'per gli ordini di prod riporto indietro anche il numeratore degli impegni collegati
            strSQL = "UPDATE tabnuma SET tb_numprog = tb_numprog -1 " & _
                    " WHERE tb_numtipo = 'Y'" & _
                    " AND tb_numserie = " & CStrSQL(strSerie) & _
                    " AND tb_numcodl = " & lCodl & _
                    " AND codditt = " & CStrSQL(strDitta)
            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LegNumg(ByVal strTipo As String, ByVal strSerie As String, ByVal lCodl As Integer, _
                          ByVal bCrea As Boolean, ByRef dbConn As DbConnection) As Integer
    ' restiuisce il prossimo progressivo tb_numprog, oppure 0 se errore..
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim lProgr As Integer = 0
    Try
      strSQL = "SELECT * FROM tabnumg WHERE tb_numtipog = " & CStrSQL(strTipo) & _
              " AND tb_numserieg = " & CStrSQL(strSerie) & _
              " AND tb_numcodlg = " & lCodl
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMG", Nothing, dbConn)

      If dsTmp.Tables("TABNUMG").Rows.Count > 0 Then
        lProgr = NTSCInt(dsTmp.Tables("TABNUMG").Rows(0)!tb_numprogg) + 1
      Else
        If bCrea Then
          strSQL = "INSERT INTO TABNUMG (tb_numtipog, tb_numserieg, tb_numcodlg, tb_numprogg) " & _
                   " VALUES (" & _
                   CStrSQL(strTipo) & ", " & _
                   CStrSQL(strSerie) & ", " & _
                   lCodl & ", 0)"
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = 1
        End If
      End If

      Return lProgr
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggNumg(ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal lNewProg As Integer, ByVal bControlla As Boolean, _
                          ByVal bVisMsg As Boolean, ByRef strReturnMessage As String, _
                          Optional ByVal dbConn As DbConnection = Nothing) As Integer
    'aggiorna il numeratore: 
    'se la numerazione non è stata attivata e non c'è l'opzione per crearla scatena una eccezione
    'se il numeratore è già stato utilizzato scatena un una eccezione

    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim lProgr As Integer = 0

    Try
      strReturnMessage = ""

      strSQL = "SELECT * FROM tabnumg WHERE tb_numtipog = " & CStrSQL(strTipo) & _
        " AND tb_numserieg = " & CStrSQL(strSerie) & _
        " AND tb_numcodlg = " & lCodl.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMG", Nothing, dbConn)

      If dsTmp.Tables("TABNUMG").Rows.Count = 0 Then
        'non c'è il record: devo generare l'errore (se è impostato di dare il messaggio)
        strReturnMessage = oApp.Tr(Me, 129100919246396485, "Attenzione: impossibile aggiornare il numero progressivo." & vbCrLf & _
                           "Numerazione |'" & CLN__STD.TraduciTipoNumerazione(strTipo) & "', serie '" & strSerie & "', cod. " & lCodl & "| mancante.")
      Else
        'c'è il record: devo verificare se il progressivo è già stato usato
        If bControlla Then
          If NTSCInt(dsTmp.Tables("TABNUMG").Rows(0)!tb_numprogg) + 1 > lNewProg Then
            lNewProg = NTSCInt(dsTmp.Tables("TABNUMG").Rows(0)!tb_numprogg) + 1
            If bVisMsg Then
              strReturnMessage = oApp.Tr(Me, 127792250102656250, "Attenzione: il numero progressivo attribuito in precedenza è già stato " & _
                                 "utilizzato da un altro utente. Il nuovo numero assegnato è |" & lNewProg & "|")
            End If
          End If
          strSQL = "UPDATE TABNUMG SET tb_numprogg = " & lNewProg & _
                   " WHERE tb_numtipog = " & CStrSQL(strTipo) & _
                   " AND tb_numserieg = " & CStrSQL(strSerie) & _
                   " AND tb_numcodlg = " & lCodl.ToString
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = lNewProg
        Else
          strSQL = "UPDATE TABNUMG SET tb_numprogg = " & lNewProg & _
                   " WHERE tb_numtipog = " & CStrSQL(strTipo) & _
                   " AND tb_numserieg = " & CStrSQL(strSerie) & _
                   " AND tb_numcodlg = " & lCodl
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          lProgr = lNewProg
        End If    'If bControlla Then

      End If    'If dsTmp.Tables("TABNUMA").Rows.Count = 0 Then

      Return lProgr

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                ByVal strNomProp As String, ByVal strOut As String, ByVal strTipoDoc As String, _
                                ByVal strDefault As String) As String
    Try
      Return GetSettingBusDitt(oApp.Ditta, strLiv1, strLiv2, strLiv3, strNomProp, strOut, strTipoDoc, strDefault)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                ByVal strNomProp As String, ByVal strOut As String, ByVal strTipoDoc As String, _
                                ByVal strDefault As String) As String
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim strQuery As String = ""
    Dim strReturn As String = strDefault
    Dim dsOut As New DataSet
    Dim bCache As Boolean = False
    Dim lCount As Integer = 0

    'Dim strDip(0 To 3) As String
    Try
      'esco se non devo leggere da nessun database 
      If oApp.DbAp.Tipo = "" Then Return ""

      'vecchio sistema
      ''Legge le dipendenze della chiave
      'strQuery = "SELECT rd_diput, rd_dipaz, rd_diptdoc FROM REGDIR WHERE upper(rd_liv1) = '" & strLiv1.ToUpper() & _
      '           "' and upper(rd_liv2) = '" & strLiv2.ToUpper() & "' and upper(rd_liv3) = '" & strLiv3.ToUpper() & "'"
      ''ottengo i dati
      'dsOut = oCldBase.OpenRecordset(strQuery, CLE__APP.DBTIPO.DBPRC, "REGDIR")

      'If dsOut.Tables("REGDIR").Rows.Count = 0 Then
      '  Return strReturn
      'Else
      '  If NTSCStr(dsOut.Tables("REGDIR").Rows(0)!rd_diput) = "S" Then
      '    strDip(1) = oApp.User.Nome  'nome operatore
      '  Else
      '    strDip(1) = " "
      '  End If

      '  If NTSCStr(dsOut.Tables("REGDIR").Rows(0)!rd_dipaz) = "S" Then
      '    strDip(2) = strDitta      'codice ditta
      '  Else
      '    strDip(2) = " "
      '  End If
      '  If NTSCStr(dsOut.Tables("REGDIR").Rows(0)!rd_diptdoc) = "S" Then
      '    strDip(3) = strTipoDoc      'tipo documento
      '  Else
      '    strDip(3) = " "
      '  End If
      'End If

      ''Legge la proprietà
      'strQuery = "SELECT rp_valprop FROM REGPROP WHERE upper(rp_liv1) = '" & strLiv1.ToUpper() & _
      '           "' AND upper(rp_liv2) = '" & strLiv2.ToUpper() & "' AND upper(rp_liv3) = '" & _
      '           strLiv3.ToUpper() & "' AND rp_idut = '" & strDip(1) & "' AND rp_idaz = '" & _
      '           strDip(2) & "' AND rp_idtdoc = '" & strDip(3) & "' AND rp_nomprop = '" & strNomProp & "'"
      ''ottengo i dati

      'nuovo sistema
RIPROVA:
      strLiv1 = strLiv1.ToUpper
      strLiv2 = strLiv2.ToUpper
      strLiv3 = strLiv3.ToUpper

      If oApp.DbAp.Tipo = "SQLServer" Then
        strQuery = "bussp_getsettingbus " & CStrSQL(strLiv1) & ", " & _
                   CStrSQL(strLiv2) & ", " & _
                   CStrSQL(strLiv3) & ", " & _
                   CStrSQL(strNomProp) & ", " & _
                   CStrSQL(oApp.User.Nome) & ", " & _
                   CStrSQL(strDitta) & ", " & _
                   CStrSQL(strTipoDoc)
      Else
        strQuery = "SELECT rp_nomprop, rp_valprop " & _
                   " FROM REGDIR INNER JOIN REGPROP ON rd_liv1 = rp_liv1 " & _
                   " AND rd_liv2 = rp_liv2 AND rd_liv3 = rp_liv3 " & _
                   " WHERE upper(rp_liv1) = '" & strLiv1 & "' " & _
                   " AND upper(rp_liv2) = '" & strLiv2 & "' " & _
                   " AND upper(rp_liv3) = '" & strLiv3 & "' " & _
                   " AND rp_idut = CASE WHEN rd_diput = 'S' THEN '" & oApp.User.Nome & "' ELSE ' ' END " & _
                   " AND rp_idaz = CASE WHEN rd_dipaz = 'S' THEN '" & strDitta & "' ELSE ' ' END " & _
                   " AND rp_idtdoc = CASE WHEN rd_diptdoc = 'S' THEN '" & strTipoDoc & "' ELSE ' ' END " & _
                   " AND rp_nomprop = '" & strNomProp & "'"
      End If
      dsOut.Tables.Clear()

      If oApp.bDSShared AndAlso (strLiv1.IndexOf("OPZIONI") > -1 OrElse strLiv2.IndexOf("OPZIONI") > -1) AndAlso oApp.DSShared.Tables.Contains("BS__GREG") Then
        'leggo dalle tabelle chache
        Dim dtrT() As DataRow = oApp.DSShared.Tables("BS__GREG").Select( _
                                " rp_liv1 =  '" & strLiv1 & "' " & _
                                " AND rp_liv2 = '" & strLiv2 & "' " & _
                                " AND rp_liv3 = '" & strLiv3 & "' " & _
                                " AND rp_nomprop = '" & strNomProp & "'")
        If dtrT.Length = 0 Then Return strReturn
        For i As Integer = 0 To dtrT.Length - 1
          If NTSCStr(dtrT(i)!rd_dipaz).ToUpper = "S" And NTSCStr(dtrT(i)!rp_idaz).ToUpper <> strDitta.ToUpper Then Continue For
          If NTSCStr(dtrT(i)!rd_dipaz).ToUpper = "N" And NTSCStr(dtrT(i)!rp_idaz).ToUpper <> " " Then Continue For
          If NTSCStr(dtrT(i)!rd_diptdoc).ToUpper = "S" And NTSCStr(dtrT(i)!rp_idtdoc).ToUpper <> strTipoDoc.ToUpper Then Continue For
          If NTSCStr(dtrT(i)!rd_diptdoc).ToUpper = "N" And NTSCStr(dtrT(i)!rp_idtdoc).ToUpper <> " " Then Continue For

          strReturn = dtrT(0)!rp_valprop.ToString()
          Exit For
        Next
      Else
        Try
          dsOut = oCldBase.OpenRecordset(strQuery, CLE__APP.DBTIPO.DBPRC, "REGPROP")
        Catch ex As Exception
          If ex.ToString.ToLower.Contains("was deadlocked on lock resources") Then
            lCount += 1
            If lCount < 10 Then
              System.Threading.Thread.Sleep(50)
              GoTo riprova
            Else
              Throw ex
            End If
          Else
            Throw ex
          End If
        End Try
        If dsOut.Tables("REGPROP").Rows.Count = 0 Then Return strReturn
        strReturn = dsOut.Tables("REGPROP").Rows(0)!rp_valprop.ToString()
      End If

      Return strReturn

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsOut.Dispose()
    End Try
    Return strReturn
  End Function

  Public Overridable Function GetSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                            ByVal strTipoDoc As String, ByRef dttOut As DataTable) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return GetSettingBusDitt(oApp.Ditta, strLiv1, strLiv2, strLiv3, strTipoDoc, dttOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                                ByVal strTipoDoc As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'esco se non devo leggere da nessun database 
      If oApp.DbAp.Tipo = "" Then Return False

      strSQL = "SELECT rp_nomprop, rp_valprop " & _
               " FROM REGDIR INNER JOIN REGPROP ON rd_liv1 = rp_liv1 " & _
               " AND rd_liv2 = rp_liv2 AND rd_liv3 = rp_liv3 " & _
               " WHERE upper(rp_liv1) = '" & strLiv1.ToUpper() & "' " & _
               " AND upper(rp_liv2) = '" & strLiv2.ToUpper() & "' " & _
               " AND upper(rp_liv3) = '" & strLiv3.ToUpper() & "' " & _
               " AND rp_idut = CASE WHEN rd_diput = 'S' THEN '" & oApp.User.Nome & "' ELSE ' ' END " & _
               " AND rp_idaz = CASE WHEN rd_dipaz = 'S' THEN '" & strDitta & "' ELSE ' ' END " & _
               " AND rp_idtdoc = CASE WHEN rd_diptdoc = 'S' THEN '" & strTipoDoc & "' ELSE ' ' END "
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                               ByVal strNomProp As String, ByVal strIn As String, ByVal strTipoDoc As String, _
                               ByVal strDipNomOp As String, ByVal strDipAz As String, ByVal strDipTipDoc As String) As Boolean
    Try
      Return SaveSettingBusDitt(oApp.Ditta, strLiv1, strLiv2, strLiv3, strNomProp, strIn, strTipoDoc, strDipNomOp, strDipAz, strDipTipDoc)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SaveSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                 ByVal strNomProp As String, ByVal strIn As String, ByVal strTipoDoc As String, _
                                 ByVal strDipNomOp As String, ByVal strDipAz As String, ByVal strDipTipDoc As String) As Boolean
    Dim strSQL As String = ""
    Dim fIsRoot As Boolean = False
    Dim bReturn As Boolean = True
    Dim nLiv As Integer
    Dim nIndice As Integer
    Dim nI As Integer
    Dim nCicla As Integer
    Dim strTemp(4) As String
    Dim strDipPrec(4) As String
    Dim strDip(0 To 3) As String
    Dim strDipCor(4) As String

    Dim dsOut As New DataSet
    Dim i As Integer = 0

    Try
      'esco se non devo leggere da nessun database 
      If oApp.DbAp.Tipo = "" Then Return True

      If strLiv2 <> "." AndAlso strLiv1 = "." Then Return False
      If strLiv3 <> "BUS" AndAlso strLiv3 <> "." AndAlso strLiv2 = "." Then Return False
      If strNomProp.Trim.Length = 0 Then Return False

      If strTipoDoc Is Nothing Or Len(Trim(strTipoDoc)) = 0 Then strTipoDoc = " "
      nLiv = 3
      If strLiv3 = "." Then
        nLiv = 2
        If strLiv2 = "." Then
          nLiv = 1
          If strLiv1 = "." Then
            nLiv = 0
          End If
        End If
      End If

      strTemp(1) = strLiv1
      strTemp(2) = strLiv2
      strTemp(3) = strLiv3
      strDip(1) = strDipNomOp
      strDip(2) = strDipAz
      strDip(3) = strDipTipDoc

      For nIndice = nLiv To 0 Step -1
        For nI = nIndice + 1 To 3
          strTemp(nI) = "."
        Next nI
        strSQL = "SELECT rd_diput, rd_dipaz, rd_diptdoc FROM REGDIR WHERE rd_liv1 = '" & strTemp(1) & _
                 "' AND rd_liv2 = '" & strTemp(2) & "' AND rd_liv3 = '" & strTemp(3) & "'"
        'ottengo i dati
        dsOut.Tables.Clear()  'se ho fatto la fill già una volta devo rimuovere la tabella precedente, altrimenti mantiene i dati di prima 
        dsOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "REGDIR")
        If dsOut.Tables("REGDIR").Rows.Count <> 0 Then
          Exit For
        End If
      Next

      For nI = 1 To 3
        strDipPrec(nI) = NTSCStr(dsOut.Tables("REGDIR").Rows(0).Item(nI - 1))
        strDipCor(nI) = strDipPrec(nI)
      Next nI

      If nIndice = 0 Then
        nIndice = 1
        fIsRoot = True
      Else
        nIndice = nIndice + 1
      End If

      For nCicla = nIndice To nLiv
        If nCicla = 1 Then strTemp(1) = strLiv1
        If nCicla = 2 Then strTemp(2) = strLiv2
        If nCicla = 3 Then strTemp(3) = strLiv3

        strSQL = ""
        For nI = 1 To 3
          If Mid(strDip(nI), nCicla, 1) = "." Then
            strDipCor(nI) = strDipPrec(nI)
          Else
            strDipCor(nI) = Mid(strDip(nI), nCicla, 1)
            strDipPrec(nI) = strDipCor(nI)
          End If
        Next nI
        strSQL = "INSERT INTO REGDIR (rd_liv1, rd_liv2, rd_liv3, rd_diput, rd_dipaz, rd_diptdoc) " & _
                 "VALUES ('" & strTemp(1) & "', '" & strTemp(2) & "', '" & strTemp(3) & "', '" & _
                 strDipCor(1) & "', '" & strDipCor(2) & "', '" & strDipCor(3) & "')"
        'eseguo la query
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      Next
      dsOut.Clear()

      If strDipCor(1) = "N" Then
        strDip(1) = " "
      Else
        strDip(1) = oApp.User.Nome
      End If
      If strDipCor(2) = "N" Then
        strDip(2) = " "
      Else
        strDip(2) = strDitta
      End If
      If strDipCor(3) = "N" Then
        strDip(3) = " "
      Else
        strDip(3) = strTipoDoc
      End If

      'strSQL = "SELECT * FROM REGPROP WHERE rp_liv1 = '" & strTemp(1) & "' AND rp_liv2 = '" & _
      '          strTemp(2) & "' AND rp_liv3 = '" & strTemp(3) & "' AND rp_idut = '" & _
      '          strDip(1) & "' AND rp_idaz = '" & strDip(2) & "' AND rp_idtdoc = '" & _
      '          strDip(3) & "' AND rp_nomprop = '" & strNomProp & "'"
      ''ottengo i dati
      'dsOut.Tables.Clear()  'se ho fatto la fill già una volta devo rimuovere la tabella precedente, altrimenti mantiene i dati di prima 
      'dsOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "REGPROP")

      'If dsOut.Tables("REGPROP").Rows.Count <> 0 Then
      '  strSQL = "UPDATE REGPROP SET rp_valprop = " & CStrSQL(strIn) & _
      '  " WHERE rp_liv1 = '" & strTemp(1) & "' AND rp_liv2 = '" & _
      '  strTemp(2) & "' AND rp_liv3 = '" & strTemp(3) & "' AND rp_idut = '" & _
      '  strDip(1) & "' AND rp_idaz = '" & strDip(2) & "' AND rp_idtdoc = '" & _
      '  strDip(3) & "' AND rp_nomprop = '" & strNomProp & "'"
      'Else
      '  strSQL = "INSERT INTO REGPROP (rp_liv1, rp_liv2, rp_liv3, rp_idut, rp_idaz, rp_idtdoc, rp_nomprop, rp_valprop) " & _
      '  "VALUES ('" & strTemp(1) & "', '" & strTemp(2) & "', '" & _
      '  strTemp(3) & "', '" & strDip(1) & "', '" & strDip(2) & "', '" & _
      '  strDip(3) & "', '" & strNomProp & "', " & CStrSQL(strIn) & ")"
      'End If
      i = 0
RIPROVA:
      strSQL = "DELETE FROM REGPROP WHERE rp_liv1 = '" & strTemp(1) & "' AND rp_liv2 = '" & _
                strTemp(2) & "' AND rp_liv3 = '" & strTemp(3) & "' AND rp_idut = '" & _
                strDip(1) & "' AND rp_idaz = '" & strDip(2) & "' AND rp_idtdoc = '" & _
                strDip(3) & "' AND rp_nomprop = '" & strNomProp & "'"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Try
        'con business social a causa del thread che ogni minuto controlla LastcheckNotizie a volte da errore di chiave duplicata
        'perchè in contemporanea dopo la delete due client cercano di fare la insert.
        'con la tranazioen si pianta tutto, per cui faccio ripetere la uery per 10 volte, poi do errore
        strSQL = "INSERT INTO REGPROP (rp_liv1, rp_liv2, rp_liv3, rp_idut, rp_idaz, rp_idtdoc, rp_nomprop, rp_valprop) " & _
                "VALUES ('" & strTemp(1) & "', '" & strTemp(2) & "', '" & _
                strTemp(3) & "', '" & strDip(1) & "', '" & strDip(2) & "', '" & _
                strDip(3) & "', '" & strNomProp & "', " & CStrSQL(strIn) & ")"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      Catch ex As Exception
        If ex.ToString.ToLower.Contains("violation of primary key") Or ex.ToString.ToLower.Contains("was deadlocked on lock resources") Then
          i += 1
          If i < 10 Then
            System.Threading.Thread.Sleep(50)
            GoTo riprova
          Else
            Throw ex
          End If
        Else
          Throw ex
        End If
      End Try

      'pulisco tutto
      dsOut.Tables.Clear()
      dsOut.Dispose()

      'se gestisco le tabelle cache devo aggiornare la data di aggiornamento tabelle cache, per farle rileggere dai vari client di business
      If oApp.bDSShared And strLiv2.ToUpper.IndexOf("OPZIONI") > -1 Then
        DBCacheAggDBCacheDate("BS__GREG")
      End If

      Return bReturn

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsOut.Dispose()
    End Try
    Return bReturn
  End Function

  Public Overridable Function DBCacheAggDBCacheDate(ByVal strTabella As String) As Boolean
    Try
      'se gestisco le tabelle cache devo aggiornare la data di aggiornamento tabelle cache, per farle rileggere dai vari client di business
      If oApp.bDSShared Then
        If oApp.DSShared.Tables.Contains(strTabella.ToUpper) Then
          SaveSettingBus("Recent", ".", ".", "DBCacheDateUpd", DateTime.Now.ToUniversalTime.ToString, " ", "...", "...", "...")
          'rimuovo in locale la tabella in memoria, così almeno nella mia istanza la modifica al record è subito operativa
          'poi tra 20 secondi verrà ricaricata la tabella in memoria ...
          oApp.DSShared.Tables.Remove(strTabella.ToUpper)
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, ByRef ds As DataSet) As Boolean
    Try
      Return SetTableDefaultValueFromDB(strTableName, strTableName, ds, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, _
                                                         ByVal strDataTableName As String, ByRef ds As DataSet) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTableName, strDataTableName, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return SetTableDefaultValueFromDB(strTableName, strTableName, ds, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, _
                                                       ByVal strDataTableName As String, ByRef ds As DataSet, _
                                                       ByVal bArcproc As Boolean) As Boolean
    'strTableName = nome effettivo della tabella
    'strDataTableName = nome della tabella nel dataset 
    'ds = dataset contenente la tabella su cui impostare i valori di default
    'legge dal database i valori di delfault della tabella passata in input e li applica alla tabella
    'select substring(name, 11, 50) as column_name, substring(text, 2, len(text) - 2) as default_value from sysobjects inner join syscomments on sysobjects.id = syscomments.id where name like 'DF[_]movord[_]%' AND xtype = 'D'
    Dim strSQL As String = ""
    Dim dsDefault As New DataSet
    Dim i As Integer
    Dim strTmp As String = ""
    Dim strColName As String = ""
    Dim strDefValue As String = ""
    Dim dttTmp As New DataTable
    Dim bSQLite As Boolean = False
    Dim oCol As DataColumn

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTableName, strDataTableName, ds, bArcproc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      'se Ã¨ giÃ  stata letta la struttura in precedenza, riciclo
      If oApp.DSTableDefaultValue.Tables.Contains(IIf(bArcproc, "PRC.", "").ToString & strTableName.ToUpper) Then
        dsDefault.Tables.Add(oApp.DSTableDefaultValue.Tables(IIf(bArcproc, "PRC.", "").ToString & strTableName.ToUpper).Copy)
        dsDefault.Tables(IIf(bArcproc, "PRC.", "").ToString & strTableName.ToUpper).TableName = "DEFAULT"
      Else
        If bArcproc Then
          bSQLite = CBool(oApp.DbAp.Tipo <> "SQLServer")
        Else
          bSQLite = CBool(oApp.Db.Tipo <> "SQLServer")
        End If
        If bSQLite Then
          strSQL = "PRAGMA table_info(" & strTableName & ")"
          If bArcproc Then
            dttTmp = oCldBase.OpenRecordset("SELECT * FROM sqlite_master WHERE type='table' AND lower(name)='" & strTableName.ToLower & "'", CLE__APP.DBTIPO.DBPRC)
            Try
              dsDefault = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "DEFAULT")
            Catch
            End Try
          Else
            dttTmp = oCldBase.OpenRecordset("SELECT * FROM sqlite_master WHERE type='table' AND lower(name)='" & strTableName.ToLower & "'", CLE__APP.DBTIPO.DBAZI)
            Try
              dsDefault = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DEFAULT")
            Catch
            End Try
          End If
          strTmp = ""
          If dttTmp.Rows.Count > 0 Then strTmp = dttTmp.Rows(0)!sql.ToString.ToUpper
          dttTmp.Clear()

          If dsDefault.Tables.Count = 0 Then
            'la tabella non Ã¨ presente nel DB: creo il datatable vuoto
            dsDefault.Tables.Add("DEFAULT")
            dsDefault.Tables("DEFAULT").Columns.Add("cid")
            dsDefault.Tables("DEFAULT").Columns.Add("name")
            dsDefault.Tables("DEFAULT").Columns.Add("type")
            dsDefault.Tables("DEFAULT").Columns.Add("notnull")
            dsDefault.Tables("DEFAULT").Columns.Add("dflt_value")
            dsDefault.Tables("DEFAULT").Columns.Add("pk")
          End If
          dsDefault.Tables("DEFAULT").Columns("name").ColumnName = "colonna"
          dsDefault.Tables("DEFAULT").Columns("type").ColumnName = "tipo"
          dsDefault.Tables("DEFAULT").Columns("dflt_value").ColumnName = "valoredefault"
          dsDefault.Tables("DEFAULT").Columns.Add("lunghezza", GetType(Integer))
          dsDefault.Tables("DEFAULT").Columns.Add("counter", GetType(Integer))

          For Each dtrT As DataRow In dsDefault.Tables("DEFAULT").Rows
            dtrT!counter = 0
            If dtrT!tipo.ToString.ToLower.StartsWith("varchar(") Then
              dtrT!lunghezza = dtrT!tipo.ToString.Substring(8, dtrT!tipo.ToString.Length - 9)
              dtrT!tipo = "varchar"
              If NTSCStr(dtrT!valoredefault) <> "" Then dtrT!valoredefault = "(" & NTSCStr(dtrT!valoredefault) & ")"
            Else
              Select Case dtrT!tipo.ToString.ToLower
                Case "text(2147483647)"
                  'campo memo
                  dtrT!tipo = "35"
                  dtrT!lunghezza = 0
                Case "blob"
                  If dtrT!colonna.ToString = "ao_ole" Then dtrT!tipo = "image" Else dtrT!tipo = "timestamp"
                  dtrT!lunghezza = 0
                Case "smallint", "integer", "float", "numeric"
                  If NTSCStr(dtrT!valoredefault) <> "" Then dtrT!valoredefault = "((" & NTSCStr(dtrT!valoredefault) & "))"
                Case "datetime"
                  If NTSCStr(dtrT!valoredefault) <> "" Then dtrT!valoredefault = "(" & NTSCStr(dtrT!valoredefault) & ")"
                Case "text"
                  If NTSCStr(dtrT!valoredefault) <> "" Then dtrT!valoredefault = "(" & NTSCStr(dtrT!valoredefault) & ")"
              End Select
            End If
          Next
          'verifico ed aggiungo il falg di colonna counter
          i = strTmp.IndexOf("AUTOINCREMENT")
          If i > -1 Then
            Dim n As Integer = i - 1
            While strTmp(n) <> Chr(10)
              n -= 1
            End While
            i = n + 2
            While strTmp(i) <> " " And Asc(strTmp(i)) <> 9  'il nome del campo puÃ² finire per " " o per TAB ed essere nel formato lc_progr o [lc_progr]
              i += 1
            End While
            dsDefault.Tables("DEFAULT").Select("colonna = '" & strTmp.Substring(n + 1, i - (n + 1)).Trim.ToLower.Replace("[", "").Replace("]", "") & "'")(0)!counter = 1
            '.devo trovare la colonna autoincrement e settare 'counter' = 1
          End If
          dsDefault.Tables("DEFAULT").AcceptChanges()
        Else
          'da sql server 2005 in su
          strSQL = "SELECT sys.columns.name as colonna, " & _
                     "case when sys.types.name = 'text' then '35' else sys.types.name end as tipo, " & _
                     "sys.columns.max_length as lunghezza, " & _
                     "case when sys.columns.is_identity = 'true' then 1 else 0 end as counter, " & _
                     "OBJECT_DEFINITION(sys.columns.default_object_id) as valoredefault " & _
                     "FROM sys.columns LEFT JOIN sys.types ON sys.columns.system_type_id = sys.types.system_type_id " & _
                     "WHERE sys.columns.object_id = OBJECT_ID('" & strTableName & "') " & _
                     "AND sys.types.name <> 'sysname'"
          If bArcproc Then
            dsDefault = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "DEFAULT")
          Else
            'strSQL = "bussp_gettablestruct '" & strTableName & "'"
            dsDefault = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DEFAULT")
          End If
        End If    'If bSQLite Then

        'memorizzo per la prossima volta
        oApp.DSTableDefaultValue.Tables.Add(dsDefault.Tables("DEFAULT").Copy())
        oApp.DSTableDefaultValue.Tables("DEFAULT").TableName = IIf(bArcproc, "PRC.", "").ToString & strTableName.ToUpper
      End If    'If oApp.DSTableDefaultValue.Tables.Contains(IIf(bArcproc, "PRC.", "").ToString & strTableName) Then


      For i = 0 To dsDefault.Tables("DEFAULT").Rows.Count - 1
        'la colonna interessata potrebbe non essere nel datatable passatomi ...
        strColName = dsDefault.Tables("DEFAULT").Rows(i)("colonna").ToString


        Try
          If Not ds.Tables(strDataTableName).Columns.Contains(strColName) Then Continue For
          oCol = ds.Tables(strDataTableName).Columns(strColName)
          If oCol Is Nothing Then Continue For 'Evito di andare nel catch che fa rallentare la routine

          'imposto il valore di default
          If Not dsDefault.Tables("DEFAULT").Rows(i)!valoredefault.Equals(System.DBNull.Value) Then
            'tolgo le parentesi prima e dopo il valore di default
            strDefValue = dsDefault.Tables("DEFAULT").Rows(i)("valoredefault").ToString
            If EsclusioneDefault(strDefValue) Then
              oCol.ExtendedProperties.Add("NTSDefault", strDefValue)
            Else
              strDefValue = strDefValue.Replace("((", "(").Replace("))", ")")
              strDefValue = strDefValue.Substring(1)
              strDefValue = strDefValue.Substring(0, strDefValue.Length - 1)
              If strDefValue.Substring(0, 1) = "'" Then
                'per le stringhe tolgo gli apici prima e dopo
                strDefValue = strDefValue.Substring(1, strDefValue.Length - 2)
              End If
              If oCol.DataType.ToString = "System.DateTime" Then
                oCol.DefaultValue = strDefValue.Replace("'", "")
              ElseIf oCol.DataType.ToString = "System.Decimal" OrElse _
                     oCol.DataType.ToString = "System.Double" Then
                oCol.DefaultValue = CLN__STD.IntSetNum(strDefValue).Replace("'", """")
              Else
                oCol.DefaultValue = strDefValue.Replace("'", """")
              End If
            End If
          End If

          'colonna contatore
          If CInt(dsDefault.Tables("DEFAULT").Rows(i)("counter")) = 1 Then
            oCol.AutoIncrement = True
          End If

          'imposto la maxlenght, solo per le scringhe(tanto su date e numeri da errore)
          Select Case oCol.DataType.ToString
            Case "System.String"
              If dsDefault.Tables("DEFAULT").Rows(i)("tipo").ToString <> "35" And _
                 NTSCInt(dsDefault.Tables("DEFAULT").Rows(i)("lunghezza")) <> -1 Then   '35 è la colonna MEMO, se la maxlen = -1 è una varchar(max)
                oCol.MaxLength = NTSCInt(dsDefault.Tables("DEFAULT").Rows(i)("lunghezza"))
              End If
          End Select

        Catch ex As Exception

        End Try
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public strEsclusioniDefaultDB() As String = {"newid()"}
  Public Overridable Function EsclusioneDefault(ByVal strDefault As String) As Boolean
    Try
      'Non ho indicato il default, non devo fare altro...
      If strDefault = "" Then Return False

      'Scorre tutte le esclusioni e verifica se è una di quelle
      For Each strNoDef As String In strEsclusioniDefaultDB
        If strDefault.IndexOf(strNoDef, StringComparison.CurrentCultureIgnoreCase) > -1 Then Return True
      Next
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetTableStructure(ByVal strTabella As String, ByVal bArcproc As Boolean, ByRef dttOut As DataTable, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try

      strSQL = "SELECT top 0 * FROM " & strTabella
      If bArcproc Then
        dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, dbConn)
      Else
        dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      dttOut.TableName = strTabella.ToUpper
      If dttOut.Columns.Contains("TS") Then dttOut.Columns.Remove("TS")
      dttOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTableDataDictionary(ByVal strTabella As String, ByVal bArcproc As Boolean, ByRef dttOut As DataTable) As Boolean
    'spostata in BD__BASE, come tutte le funzioni che trattano il database
    Return False
  End Function


  Public Overridable Function IsPerDitta(ByVal bPerDbAzienda As Boolean, ByVal strNomeTabella As String, _
                             Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    'restituisce true se la tabella è per ditta, altrimenti false
    Dim strSQL As String = "SELECT ot_perditta FROM ordertbl where ot_nometab = '" & strNomeTabella & "' "
    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow = Nothing
    Dim bReturn As Boolean = False

    Try
      If bPerDbAzienda Then
        'prima cerco nel datatable in memoria
        dtrT = oApp.Db.dttOrderTbl.Select("ot_nometab = '" & strNomeTabella & "'")
        If dtrT.Length > 0 Then
          If dtrT(0)!ot_perditta.ToString() = "S" Then
            Return True
          Else
            Return False
          End If
        End If
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella, Nothing, dbConn)
      Else
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, strNomeTabella, Nothing, dbConn)
      End If
      If dsTmp.Tables(strNomeTabella).Rows.Count = 0 Then Return False
      If dsTmp.Tables(strNomeTabella).Rows(0)!ot_perditta.ToString() = "S" Then bReturn = True

      Return bReturn

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsTmp.Dispose()
    End Try
    Return bReturn
  End Function

  Public Overridable Function IsDbMultiDitta(ByRef strCoddittaunica As String) As Boolean
    Dim strSQL As String
    Dim dsTmp As DataSet
    Dim bResult As Boolean = False
    Try
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABANAZ") Then
        dsTmp = New DataSet
        dsTmp.Tables.Add(oApp.DSShared.Tables("TABANAZ").Copy)
      Else
        strSQL = "SELECT codditt FROM tabanaz"
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      End If

      If dsTmp.Tables("TABANAZ").Rows.Count <> 0 Then
        If dsTmp.Tables("TABANAZ").Rows.Count > 1 Then
          bResult = True
        Else
          bResult = False
          strCoddittaunica = dsTmp.Tables("TABANAZ").Rows(0)!codditt.ToString
        End If
      Else
        bResult = False
        strCoddittaunica = oApp.Ditta
      End If
      dsTmp.Dispose()

      Return bResult
    Catch ex As Exception
      Throw (New ArgumentException())
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '---------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function ValCodiceDb(ByVal strCodice As String, ByVal strCodditt As String, ByVal strNomeTabella As String, _
                            ByVal strTipoCod As String, Optional ByRef strDescampo As String = "", _
                            Optional ByRef dttTable As DataTable = Nothing, Optional ByVal strFilter1 As String = "", _
                            Optional ByVal strFilter2 As String = "", Optional ByVal strFilter3 As String = "") As Boolean
    Try
      Return ValCodiceDb(strCodice, strCodditt, strNomeTabella, strTipoCod, strDescampo, dttTable, strFilter1, strFilter2, strFilter3, Nothing)
    Catch ex As Exception
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '---------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ValCodiceDb(ByVal strCodice As String, ByVal strCodditt As String, _
                              ByVal strNomeTabella As String, _
                              ByVal strTipoCod As String, ByRef strDescampo As String, _
                              ByRef dttTable As DataTable, ByVal strFilter1 As String, _
                              ByVal strFilter2 As String, ByVal strFilter3 As String, _
                              ByVal dbConn As DbConnection) As Boolean
    'strCodice :    codice che deve essere controllato
    'strCodditt:    nome della ditta dove deve essere validato il campo (se la tabella è per ditta, altrimenti passare "")
    'strNomeTabella:elemento che pilota la validazione 
    'strTipocod:    N = il codice da controllare è di tipo numerico, S = stringa, D = data
    'strDescampo:   valore di ritorno con la descrizione del campo (es per cod. pagamento la descr del pagamento)

    'verifica se il codice passato è presente nel database 

    Dim strSQL As String = ""
    Dim strCampo As String = ""
    Dim strTabella As String = ""
    Dim strDescr As String = ""
    Dim strWhere As String = ""
    Dim bDbPrc As Boolean = False   'se TRUE la query deve essere eseguita su arcproc
    Dim bPerDitta As Boolean
    Dim strCampiChiave As String = ""

    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodice, strCodditt, strNomeTabella, strTipoCod, strDescampo, _
                                             dttTable, strFilter1, strFilter2, strFilter3, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strDescampo = NTSCStr(oIn(4))
        dttTable = CType(oIn(5), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      strDescampo = ""

      If strTipoCod = "N" Then
        If NTSCDec(strCodice) = 0 Then
          strDescr = ""
          If Not dttTable Is Nothing Then
            dttTable = Nothing
            dttTable = New DataTable
          End If

          Return True
        End If
      Else
        If strCodice = "" Then
          strDescr = ""
          If Not dttTable Is Nothing Then
            dttTable = Nothing
            dttTable = New DataTable
          End If
          Return True
        End If
      End If

      Select Case strNomeTabella
        Case "ANAGRACF", "ANAGRAFC"
          'anagraCF accetta sia clienti che fornitori, ma il default è fornitori
          'anagraFC accetta sia clienti che fornitori, ma il default è clienti
          strTabella = "anagra"
          strCampo = "an_conto"
          strDescr = "an_descr1"
          strWhere = "an_tipo <> 'S' AND an_conto = " & CStrSQL(strCodice) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANAGRAC"
          strTabella = "anagra"
          strCampo = "an_conto"
          strDescr = "an_descr1"
          strWhere = "an_tipo = 'C' AND an_conto = " & CStrSQL(strCodice) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANAGRAF"
          strTabella = "anagra"
          strCampo = "an_conto"
          strDescr = "an_descr1"
          strWhere = "an_tipo = 'F' AND an_conto = " & CStrSQL(strCodice) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANAGRAS"
          strTabella = "anagra"
          strCampo = "an_conto"
          strDescr = "an_descr1"
          strWhere = "an_tipo = 'S' AND an_conto = " & CStrSQL(strCodice) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANAGPC"
          strTabella = "anagpc"
          strCampo = "pc_conto"
          strDescr = "pc_descr1"
          strWhere = "pc_conto = " & CStrSQL(strCodice) & " AND pc_codpcon = " & CStrSQL(strFilter1)
        Case "ARTFASI"
          strTabella = "artfasi"
          strCampo = "af_codart"
          strDescr = "af_descr"
          strWhere = "af_fase = " & strCodice & " AND af_codart = " & CStrSQL(strFilter1) & " AND codditt = " & CStrSQL(strCodditt)
        Case "DESTDIV"
          strTabella = "destdiv"
          strCampo = "dd_coddest"
          strDescr = "dd_nomdest"
          strWhere = "dd_coddest = " & strCodice & " AND dd_conto = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "TABSMEL"
          strTabella = "tabsmel"
          strCampo = "tb_numsmel"
          strDescr = "tb_dessmel"
          strWhere = "tb_numsmel = " & strCodice & " AND tb_codsmel = " & CStrSQL(strFilter1) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ABI"
          bDbPrc = True
          strTabella = "abi"
          strCampo = "abiabi"
          strDescr = "abibanc"
          strWhere = "abiabi = " & CStrSQL(strCodice)
        Case "CAB"
          bDbPrc = True
          strTabella = "abicab"
          strCampo = "abccab"
          strDescr = "abccomune"
          strWhere = "abccab = " & CStrSQL(strCodice) & " AND abcabi = " & strFilter1
        Case "TABCLAS"
          strTabella = "tabclas"
          strCampo = "tb_codclas"
          strDescr = "tb_desclas"
          strWhere = "tb_codpcon = " & CStrSQL(strFilter1) & " AND tb_codclas = " & strCodice
        Case "TABMAST"
          strTabella = "tabmast"
          strCampo = "tb_codmast"
          strDescr = "tb_desmast"
          strWhere = "tb_codpcon = " & CStrSQL(strFilter1) & " AND tb_codmast = " & strCodice
        Case "TABPECG"
          strTabella = "tabpecg"
          strCampo = "tb_codpecg"
          strDescr = "tb_codpecg"
          strWhere = "tb_codpecg = " & strCodice
        Case "TABPECE"
          strTabella = "tabpece"
          strCampo = "tb_codpece"
          strDescr = "tb_codpece"
          strWhere = "tb_codpece = " & strCodice
        Case "ANADITAC"
          strTabella = "anaditac"
          strCampo = "codditt"
          strDescr = "codditt"
          strWhere = "codditt = " & CStrSQL(strCodice)
        Case "ANADITACS"
          strTabella = "anaditacs"
          strCampo = "codditt"
          strDescr = "codditt"
          strWhere = "codditt = " & CStrSQL(strCodice)
        Case "ANADITACE"
          strTabella = "anaditace"
          strCampo = "codditt"
          strDescr = "codditt"
          strWhere = "ae_numesco = " & strCodice & " AND codditt = " & CStrSQL(strCodditt)
        Case "TABCAUS"
          strTabella = "tabcaus"
          strCampo = "tb_codcaus"
          strDescr = "tb_descaus"
          strWhere = "tb_codcaus = " & strCodice & " AND tb_anno = " & strFilter1
        Case "TABPRCR"
          strTabella = "tabprcr"
          strCampo = "tb_codprcr"
          strDescr = "tb_dtulrc"
          strWhere = "tb_codprcr = " & strCodice & " AND tb_escomp = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANADITASD"
          strTabella = "anaditasd"
          strCampo = "asd_numprogr"
          strDescr = "asd_desunpro"
          strWhere = "asd_numprogr = " & strCodice & " AND asd_numesco = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANASTO"
          strTabella = "anasto"
          strCampo = "as_codanag"
          strDescr = "as_codanag"
          strWhere = "as_codanag = " & strCodice & _
                     " AND as_datini <= " & CDataSQL(strFilter1) & _
                     " AND as_datfin >= " & CDataSQL(strFilter1)
        Case "OPERAT"
          strTabella = "operat"
          strCampo = "OpNome"
          strDescr = "OpNome"
          strWhere = "OpNome = " & CStrSQL(strCodice)
          bDbPrc = True
        Case "TABD7PERCIP"
          strTabella = "d7percip"
          strCampo = "pe_7codperc"
          strDescr = "codditt"
          strWhere = "pe_7codperc = " & CStrSQL(strCodice) & " AND codditt = " & CStrSQL(strCodditt)
        Case "SUBCOMM"
          strTabella = "subcomm"
          strCampo = "sco_subcommeca"
          strDescr = "sco_descr"
          strWhere = "sco_subcommeca = " & CStrSQL(strCodice) & " AND sco_commeca = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "ARTMAGA"
          strTabella = "artmaga"
          strCampo = "am_codmaga"
          strDescr = "am_codmaga"
          strWhere = "am_codmaga = " & strCodice & " AND am_codart = " & CStrSQL(strFilter1) & " AND am_fase = " & strFilter2 & " AND codditt = " & CStrSQL(strCodditt)
        Case "ARTPROX"
          strTabella = "artprox"
          strCampo = "apx_codart"
          strDescr = "apx_codart"
          strWhere = "apx_codart = " & CStrSQL(strCodice) & " AND apx_fase = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANALOTTI"
          strTabella = "analotti"
          strCampo = "alo_codart"
          strDescr = "alo_codart"
          strWhere = "alo_lotto = " & strCodice & " AND alo_codart = " & CStrSQL(strFilter1) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ANALOTTIX"
          strTabella = "analotti"
          strCampo = "alo_codart"
          strDescr = "alo_codart"
          strWhere = "alo_lottox = " & CStrSQL(strCodice) & " AND alo_codart = " & CStrSQL(strFilter1) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ARTVAL"
          strTabella = "artval"
          strCampo = "ax_codart"
          strDescr = "ax_descr"
          strWhere = "ax_codart = " & CStrSQL(strCodice) & " AND ax_codvalu = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "PROFIL"
          bDbPrc = True
          strTabella = "profil"
          strCampo = "prf_codice"
          strDescr = "prf_nome"
          strWhere = "prf_codice = " & CStrSQL(strCodice)
        Case "TRCFLD"
          bDbPrc = True
          strTabella = "trcfld"
          strCampo = "trc_codice"
          strDescr = "trc_descr"
          strWhere = "trc_codice = " & CStrSQL(strCodice)
        Case "TRCVAL"
          bDbPrc = True
          strTabella = "trcval"
          strCampo = "trv_codice"
          strDescr = "trv_valbus"
          strWhere = "trv_codice = " & CStrSQL(strCodice)
        Case "SCHTBL"
          bDbPrc = True
          strTabella = "schtbl"
          strCampo = "sct_nometab"
          strDescr = "sct_desctab"
          strWhere = "sct_nometab = " & CStrSQL(strCodice)
        Case "SCHFLD"
          bDbPrc = True
          strTabella = "schfld"
          strCampo = "scf_order"
          strDescr = "scf_nomefld"
          strWhere = "scf_nometab = " & CStrSQL(strCodice) & " AND scf_order = " & strFilter1
        Case "MAPTBL"
          bDbPrc = True
          strTabella = "maptbl"
          strCampo = "mpt_codprof"
          strDescr = "mpt_descr"
          strWhere = "mpt_codprof = " & NTSCInt(strCodice) & _
            " AND mpt_nometabbus = " & CStrSQL(strFilter1) & _
            " AND mpt_nometabext = " & CStrSQL(strFilter2)
        Case "TABGRU2"
          strTabella = "tabgru2"
          strCampo = "tb_codgru2"
          strDescr = "tb_desgru2"
          strWhere = "tb_codgru2 = " & NTSCInt(strCodice) & _
            " AND tb_codpcca = " & CStrSQL(strFilter1)
        Case "TABCLC2"
          strTabella = "tabclc2"
          strCampo = "tb_codclc2"
          strDescr = "tb_desclc2"
          strWhere = "tb_codclc2 = " & NTSCInt(strCodice) & _
            " AND tb_codpcca = " & CStrSQL(strFilter1)
        Case "TABMAC2"
          strTabella = "tabmac2"
          strCampo = "tb_codmac2"
          strDescr = "tb_desmac2"
          strWhere = "tb_codmac2 = " & NTSCInt(strCodice) & _
            " AND tb_codpcca = " & CStrSQL(strFilter1)
        Case "ANAGCA2"
          strTabella = "anagca2"
          strCampo = "ac_conto"
          strDescr = "ac_descr1"
          strWhere = "ac_conto = " & NTSCInt(strCodice) & _
            " AND ac_codpcca = " & CStrSQL(strFilter1)
        Case "TABDICV"
          strTabella = "tabdicv"
          strCampo = "tb_coddicv"
          strDescr = "tb_desdicv"
          strWhere = "tb_coddica = " & CStrSQL(strFilter1) & _
                     " AND tb_coddicv = " & CStrSQL(strCodice) & _
                     " AND codditt = " & _
                     " (CASE WHEN (SELECT tb_liv FROM tabdica WHERE tb_coddica = " & _
                     CStrSQL(strFilter1) & ") = 1  THEN ' '" & _
                     " ELSE  " & CStrSQL(strCodditt) & " END)"
        Case "TABDICA"
          strTabella = "tabdica"
          strCampo = "tb_coddica"
          strDescr = "tb_desdica"
          strWhere = "tb_coddica = " & CStrSQL(strCodice)
          If Not strFilter1 Is Nothing AndAlso Not strFilter1.Trim.Length = 0 Then
            strWhere = strWhere & " AND tb_applicaa = " & CStrSQL(strFilter1)
          End If
        Case "TABTCDC"
          strTabella = "tabtcdc"
          strCampo = "tb_codtcdc"
          strDescr = "tb_destcdc"
          strWhere = "tb_codtcdc = " & CStrSQL(strCodice)
          If Not strFilter1 Is Nothing AndAlso Not strFilter1.Trim.Length = 0 Then
            strWhere = strWhere & " AND tb_tipork = " & CStrSQL(strFilter1)
          End If
        Case "CAREVIS"
          strTabella = "carevis"
          strCampo = "rev_rev"
          strDescr = "rev_status"
          strWhere = "rev_rev = " & ntscint(strCodice) & _
                     " AND rev_codgrua = " & ntscint(strFilter1) & _
                     " AND rev_escompg = " & ntscint(strFilter2)
        Case "DWIMPSCE"
          strTabella = "dwimpsce"
          strCampo = "sce_progr"
          strDescr = "sce_descr"
          strWhere = "sce_progr = " & strCodice & " AND sce_codscen = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "TASKS"
          strTabella = "tasks"
          strCampo = "tsk_taskid"
          strDescr = "tsk_descr"
          strWhere = "tsk_taskid = " & strCodice & " AND tsk_commeca = " & strFilter1 & " AND codditt = " & CStrSQL(strCodditt)
        Case "TABPECX"
          strTabella = "tabpecx"
          strCampo = "tb_codpecx"
          strDescr = ""
          strWhere = "codditt = " & CStrSQL(strCodditt)
        Case "COMWBS"
          'non posso selezionare righe di tipo 'totalizzatore'
          strTabella = "comwbs"
          strCampo = "cwb_idwbs"
          strDescr = "cwb_descr"
          strWhere = "cwb_summary = 'N' AND cwb_idwbs = " & strCodice & " AND cwb_commeca = " & strFilter1 & " AND cwb_subcommeca = " & CStrSQL(strFilter2) & " AND codditt = " & CStrSQL(strCodditt)
        Case "ATTPREVC"
          strTabella = "attprevc"
          strCampo = "ap_codattp"
          strDescr = ""
          strWhere = "codditt = " & CStrSQL(strCodditt) & " AND ap_codattp = " & strCodice
        Case "NNMODRICH"
          strTabella = "nnmodrich"
          strCampo = "nnm_codrich"
          strDescr = "nnm_desrich"
          strWhere = "codditt = " & CStrSQL(strCodditt) & " AND nnm_codrich = " & strCodice
        Case "VALVARI"
          strTabella = "valvari"
          strCampo = "vv_valvari"
          strDescr = "vv_desvvar"
          strWhere = "vv_valvari = " & CStrSQL(strCodice) & " AND vv_codvari = " & CStrSQL(strFilter1) & " AND codditt = " & CStrSQL(strCodditt)
        Case Else
          '-------------------------
          'cerco i dati relativi alla tabella 
          If strNomeTabella.Length = 4 Then strNomeTabella = "TAB" + strNomeTabella
          strSQL = "dl_nome = '" & strNomeTabella & "'"
          If strNomeTabella.Substring(0, 3).ToUpper = "TAB" AndAlso strNomeTabella.Length = 7 Then strSQL += " OR dl_nome = 'TAB'"
          dtrT = oApp.DbAp.dttDalconf.Select(strSQL, "dl_nome DESC")
          If dtrT.Length = 0 Then
            Throw (New NTSException(oApp.Tr(Me, 129100919096972657, "La funzione ValCodiceDb non è stata in grado di determinare il tipo di campo da trattare: parametri " & vbCrLf & _
                   "Codice: |" & strCodice & "|, strNomeTabella: |" & strNomeTabella & "|. Forse manca la dichiarazione della tabella in Arcproc.dalconf")))
            Return False
          End If

          strTabella = dtrT(0)!dl_nometab.ToString
          strCampiChiave = dtrT(0)!dl_campichiave.ToString
          strDescr = dtrT(0)!dl_descampo.ToString
          strCampo = dtrT(0)!dl_campichiave.ToString
          strTipoCod = dtrT(0)!dl_tipocampichiave.ToString

          'caso standard di tabelle generiche gestite per ditta
          If strTabella.ToUpper = "TAB" AndAlso strNomeTabella.Length = 7 AndAlso strNomeTabella.Substring(0, 3).ToUpper = "TAB" Then
            strTabella = strNomeTabella
            If strCampiChiave.Trim <> "" Then
              strCampiChiave += strNomeTabella.Substring(3)
              strDescr += strNomeTabella.Substring(3)
              strCampo += strNomeTabella.Substring(3)
            End If
          End If

          bPerDitta = (dtrT(0)!dl_perditta.ToString = "S")
          If dtrT(0)!dl_nometab.ToString.ToUpper = "TAB" Then
            bPerDitta = IsPerDitta(Not bDbPrc, strTabella, dbConn)
          End If
          bDbPrc = (dtrT(0)!dl_db.ToString = "A")

          Select Case strTipoCod.ToUpper()
            Case "N" : strWhere = strCampo + " = " + CDblSQL(strCodice)
            Case "S" : strWhere = strCampo + " = " + CStrSQL(strCodice)
            Case "D" : strWhere = strCampo + " = " + CDataSQL(strCodice)
          End Select

          If bPerDitta Then
            If NTSCStr(strCodditt) <> "" Then
              strWhere += " AND codditt = " & CStrSQL(strCodditt)
            Else
              Throw (New NTSException(oApp.Tr(Me, 127954334714744390, "La funzione ValCodiceDb è stata chiamata avvisando che la tabella è 'per ditta' ma non è stao passato il codice ditta" & vbCrLf & _
                     "NomeTabella: |" & strNomeTabella & "|")))
              Return False
            End If
          End If
      End Select

      If strTabella = "" Then
        Throw (New NTSException(oApp.Tr(Me, 127791221373906250, "La funzione ValCodiceDb non è stata in grado di determinare il tipo di campo da trattare: parametri " & vbCrLf & _
               "Codice: |" & strCodice & "|, strNomeTabella: |" & strNomeTabella & "|")))
        Return False
      End If

      If dttTable Is Nothing Then
        'se non ho passato il datarow leggo solo il campo descrizione
        strSQL = "SELECT " & strDescr & " FROM " & strTabella & " WHERE " & strWhere
      Else
        'se ho passato il datarow restituisco tutto il record
        strSQL = "SELECT * FROM " & strTabella & " WHERE " & strWhere
      End If

      If bDbPrc Then
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, strTabella, Nothing, dbConn)
      Else
        If oApp.bDSShared AndAlso dbConn Is Nothing AndAlso oApp.DSShared.Tables.Contains(strTabella.ToUpper) Then
          dtrT = oApp.DSShared.Tables(strTabella.ToUpper).Select(strWhere)

          If dtrT.Length = 0 Then
            'non ho trovato nessun record
            If Not dttTable Is Nothing Then dttTable = oApp.DSShared.Tables(strTabella.ToUpper).Clone
            Return False
          Else
            'ritorno la descrizione del campo
            If strDescr <> "" Then strDescampo = NTSCStr(dtrT(0)(strDescr))
            If Not dttTable Is Nothing Then
              dttTable = oApp.DSShared.Tables(strTabella.ToUpper).Clone
              dttTable.ImportRow(dtrT(0))
              dttTable.AcceptChanges()
            End If
            Return True
          End If
        Else
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabella, Nothing, dbConn)
        End If
      End If

      If dsTmp.Tables(strTabella).Rows.Count = 0 Then
        'non ho trovato nessun record
        If Not dttTable Is Nothing Then dttTable = dsTmp.Tables(strTabella).Copy()
        Return False
      Else
        'ritorno la descrizione del campo
        If strDescr <> "" Then strDescampo = NTSCStr(dsTmp.Tables(strTabella).Rows(0)(strDescr))
        If Not dttTable Is Nothing Then dttTable = dsTmp.Tables(strTabella).Copy()
        Return True
      End If

      Return True
    Catch ex As Exception
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '---------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                  Optional ByVal strColonnaCounter As String = "") As String
    'ritorna la parte iniziale di una query di insert (quella contenente i campi del database)
    Dim strSQL As String = ""

    Try
      Return GetQueryInsertField(dttTabella, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                ByVal strColonnaCounter As String, ByVal strAliasPrefissoCampo As String) As String
    'ritorna la parte iniziale di una query di insert (quella contenente i campi del database)
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        Return NTSCStr(oOut)
      End If
      '----------------

      Return GetQueryInsertField(dttTabella, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                  ByVal strColonnaCounter As String, ByVal strAliasPrefissoCampo As String, _
                                                  ByRef dttFieldAlias As DataTable) As String
    'ritorna la parte iniziale di una query di insert (quella contenente i campi del database)
    Dim e As Integer
    Dim strSQLBuilder As New System.Text.StringBuilder
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bOk As Boolean = False
    Dim lAliasPrefissoCampoLength As Integer
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, _
                                             dttFieldAlias})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        dttFieldAlias = CType(oIn(4), DataTable)
        Return NTSCStr(oOut)
      End If
      '----------------

      strPrefissoCampo = strPrefissoCampo.ToLower
      strColonnaCounter = strColonnaCounter.ToLower

      If strAliasPrefissoCampo = "" Then
        strAliasPrefissoCampo = strPrefissoCampo
      Else
        strAliasPrefissoCampo = strAliasPrefissoCampo.ToLower
      End If
      lAliasPrefissoCampoLength = strAliasPrefissoCampo.Length

      For e = 0 To dttTabella.Columns.Count - 1
        With dttTabella.Columns(e)
          Dim strColumnName As String = .ColumnName.ToLower
          If strColumnName = "ts" Then
            'scarto
          ElseIf .AutoIncrement() Then
            'scarto
          ElseIf .ExtendedProperties.Count > 0 AndAlso EsclusioneDefault(NTSCStr(.ExtendedProperties("NTSDefault"))) Then
            'scarto
          ElseIf strColumnName = strColonnaCounter Then
            'scarto colonna counter
          ElseIf .ColumnName.Length < lAliasPrefissoCampoLength Then
            'scarto
          ElseIf strColumnName = "instid" Then
            'colonna instid
            strSQLBuilder.Append(strColumnName & ", ")
          ElseIf strColumnName = "codditt" Then
            'colonna codice ditta
            strSQLBuilder.Append(strColumnName & ", ")
          ElseIf strColumnName.Substring(0, lAliasPrefissoCampoLength) = strAliasPrefissoCampo OrElse _
                 strColumnName.Substring(0, 3) = "hh_" OrElse _
                 strColumnName.Substring(0, 3) = "qq_" OrElse _
                 strColumnName.PadRight(5).Substring(0, 5) = "pers_" Then
            bOk = False
            If Not dttFieldAlias Is Nothing Then
              dtrT = dttFieldAlias.Select("datatable = '" & strColumnName & "'")
              If dtrT.Length > 0 Then
                If dtrT(0)!database.ToString.ToLower().Trim <> "" Then
                  strSQLBuilder.Append(dtrT(0)!database.ToString.ToLower() & ", ")
                End If
                bOk = True
              End If
            End If
            If bOk = False Then
              If strColumnName.Substring(0, lAliasPrefissoCampoLength) = strAliasPrefissoCampo Then
                'sostituisco l'alias con il prefisso giusto
                strSQLBuilder.Append(strPrefissoCampo & strColumnName.Substring(lAliasPrefissoCampoLength) & ", ")
              Else
                'mantengo il prefisso originale
                strSQLBuilder.Append(strColumnName & ", ")
              End If
            End If
          End If    'If strColumnName.Substring(2, 1) = "_" And strColumnName <> "ts" Then
        End With
      Next    'For i = 1 To dttRiga.Columns.Count - 1

      strSQL = strSQLBuilder.ToString
      If strSQL.Length > 2 Then strSQL = " ( " & strSQL.Remove(strSQL.Length - 2) & " ) "

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, Optional ByVal strColonnaCounter As String = "") As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim strSQL As String = ""

    Try

      Return GetQueryInsertValue(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String) As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        dtrRiga = CType(oIn(1), DataRow)
        Return NTSCStr(oOut)
      End If
      '----------------

      Return GetQueryInsertValue(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String, ByRef dttFieldAlias As DataTable) As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim e As Integer
    Dim strSQLBuilder As New System.Text.StringBuilder
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bSalta As Boolean = False
    Dim strTabella As String = dttTabella.TableName
    Dim lAliasPrefissoCampoLength As Integer
    Try
      If strTabella = "TESTA" Then
        strTabella = "TESTMAG"
        If strPrefissoCampo = "td_" Then strTabella = "TESTORD"
      ElseIf strTabella = "TESTAC" Then
        strTabella = "TESTMAGC"
      ElseIf strTabella = "CORPO" Then
        strTabella = "MOVMAG"
        If strPrefissoCampo = "mo_" Then strTabella = "MOVORD"
      End If

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, _
                                             strAliasPrefissoCampo, dttFieldAlias})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        dtrRiga = CType(oIn(1), DataRow)
        dttFieldAlias = CType(oIn(5), DataTable)
        Return NTSCStr(oOut)
      End If
      '----------------

      strPrefissoCampo = strPrefissoCampo.ToLower
      strColonnaCounter = strColonnaCounter.ToLower

      If strAliasPrefissoCampo = "" Then
        strAliasPrefissoCampo = strPrefissoCampo
      Else
        strAliasPrefissoCampo = strAliasPrefissoCampo.ToLower
      End If
      lAliasPrefissoCampoLength = strAliasPrefissoCampo.Length

      For e = 0 To dttTabella.Columns.Count - 1
        With dttTabella.Columns(e)
          Dim strColumnName As String = .ColumnName.ToLower
          If strColumnName = "ts" Then
            'scarto
          ElseIf .AutoIncrement() Then
            'scarto
          ElseIf .ExtendedProperties.Count > 0 AndAlso EsclusioneDefault(NTSCStr(.ExtendedProperties("NTSDefault"))) Then
            'scarto
          ElseIf strColumnName = strColonnaCounter Then
            'scarto colonna counter
          ElseIf .ColumnName.Length < lAliasPrefissoCampoLength Then
            'scarto
          ElseIf strColumnName = "instid" Then
            'colonna instid
            strSQLBuilder.Append(CDblSQL(dtrRiga(.ColumnName).ToString()) & ", ")
          ElseIf strColumnName = "codditt" Then
            'colonna codice ditta
            strSQLBuilder.Append(CStrSQL(dtrRiga(.ColumnName).ToString()) + ", ")
          ElseIf strColumnName.Substring(0, lAliasPrefissoCampoLength) = strAliasPrefissoCampo OrElse _
                 strColumnName.Substring(0, 3) = "hh_" OrElse _
                 strColumnName.Substring(0, 3) = "qq_" OrElse _
                 strColumnName.PadRight(5).Substring(0, 5) = "pers_" Then

            bSalta = False
            If Not dttFieldAlias Is Nothing Then
              dtrT = dttFieldAlias.Select("datatable = '" & strColumnName & "'")
              If dtrT.Length > 0 Then
                If dtrT(0)!database.ToString.ToLower().Trim = "" Then
                  bSalta = True
                End If
              End If
            End If

            If bSalta = False Then
              If .AllowDBNull = True AndAlso dtrRiga(.ColumnName).ToString() = "" Then
                strSQLBuilder.Append("null")
              Else
                Select Case .DataType.ToString()
                  Case "System.String"
                    If oApp.DTTNvarchar.Select("tabella = '" & strTabella & "' AND colonna = '" & .ColumnName & "'").Length > 0 Then
                      'campo unicode
                      strSQLBuilder.Append("N" + CStrSQL(dtrRiga(.ColumnName)))
                    Else
                      strSQLBuilder.Append(CStrSQL(dtrRiga(.ColumnName)))
                    End If
                  Case "System.DateTime"
                    If strColumnName.IndexOf("ultagg") > -1 OrElse _
                       strColumnName.IndexOf("_start") > -1 OrElse _
                       strColumnName.IndexOf("_stop") > -1 OrElse _
                       strColumnName.IndexOf("dtor") > -1 OrElse _
                       strColumnName.IndexOf("dataora") > -1 Then
                      'distinguo tra datetime o solo date
                      'start/stop per lavcent-avlavp
                      Dim dtTmp As DateTime = NTSCDate(NTSCStr(dtrRiga(.ColumnName)))
                      If dtTmp.Hour <> 0 OrElse dtTmp.Minute <> 0 OrElse dtTmp.Second <> 0 Then
                        strSQLBuilder.Append(CDataOraSQL(dtrRiga(.ColumnName).ToString()))
                      Else
                        strSQLBuilder.Append(CDataSQL(dtrRiga(.ColumnName).ToString()))
                      End If
                    Else
                      'sempre e solo data 
                      strSQLBuilder.Append(CDataSQL(dtrRiga(.ColumnName).ToString()))
                    End If
                  Case Else
                    'sono sempre numeri
                    strSQLBuilder.Append(CDblSQL(dtrRiga(.ColumnName).ToString()))
                End Select
              End If    'If .AllowDBNull = True And dtrChange(i)(.ColumnName).ToString() = "" Then
              strSQLBuilder.Append(", ")
            End If    'If bSalta = False Then

          End If    'If strColumnName.Substring(2, 1) = "_" And strColumnName <> "ts" Then
        End With
      Next    'For i = 1 To dttRiga.Columns.Count - 1

      strSQL = strSQLBuilder.ToString
      If strSQL.Length > 2 Then strSQL = " ( " & strSQL.remove(strSQL.Length - 2) & " ) "

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, Optional ByVal strColonnaCounter As String = "") As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim strSQL As String = ""

    Try
      Return GetQueryUpdate(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                            ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                            ByVal strAliasPrefissoCampo As String) As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        dtrRiga = CType(oIn(1), DataRow)
        Return NTSCStr(oOut)
      End If
      '----------------

      Return GetQueryUpdate(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String, ByRef dttFieldAlias As DataTable) As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim e As Integer
    Dim strSQLBuilder As New System.Text.StringBuilder
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bOk As Boolean = False
    Dim strColName As String = ""
    Dim strTabella As String = dttTabella.TableName
    Dim lAliasPrefissoCampoLength As Integer
    Try
      If strTabella = "TESTA" Then
        strTabella = "TESTMAG"
        If strPrefissoCampo = "td_" Then strTabella = "TESTORD"
      ElseIf strTabella = "TESTAC" Then
        strTabella = "TESTMAGC"
      ElseIf strTabella = "CORPO" Then
        strTabella = "MOVMAG"
        If strPrefissoCampo = "mo_" Then strTabella = "MOVORD"
      End If

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, _
                                             strAliasPrefissoCampo, dttFieldAlias})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTabella = CType(oIn(0), DataTable)
        dtrRiga = CType(oIn(1), DataRow)
        dttFieldAlias = CType(oIn(5), DataTable)
        Return NTSCStr(oOut)
      End If
      '----------------

      strPrefissoCampo = strPrefissoCampo.ToLower
      strColonnaCounter = strColonnaCounter.ToLower

      If strAliasPrefissoCampo = "" Then
        strAliasPrefissoCampo = strPrefissoCampo
      Else
        strAliasPrefissoCampo = strAliasPrefissoCampo.ToLower
      End If
      lAliasPrefissoCampoLength = strAliasPrefissoCampo.Length

      For e = 0 To dttTabella.Columns.Count - 1
        'scarto le colonne doveil terzo carattere è diverso da "_": da migliorare!!!!!!
        With dttTabella.Columns(e)
          Dim strColumnName As String = .ColumnName.ToLower
          If strColumnName = "ts" Then
            'scarto
          ElseIf .AutoIncrement() Then
            'scarto
          ElseIf .ExtendedProperties.Count > 0 AndAlso EsclusioneDefault(NTSCStr(.ExtendedProperties("NTSDefault"))) Then
            'scarto
          ElseIf strColumnName = strColonnaCounter Then
            'scarto colonna counter
          ElseIf .ColumnName.Length < lAliasPrefissoCampoLength Then
            'scarto
          ElseIf strColumnName = "instid" Then
            'colonna instid
            strSQLBuilder.Append(strColumnName + " = " + CDblSQL(dtrRiga(.ColumnName).ToString()) + ", ")
          ElseIf strColumnName = "codditt" Then
            'colonna codice ditta
            strSQLBuilder.Append(strColumnName + " = " + CStrSQL(dtrRiga(.ColumnName)) + ", ")
          ElseIf strColumnName.Substring(0, lAliasPrefissoCampoLength) = strAliasPrefissoCampo OrElse _
                 strColumnName.Substring(0, 3) = "hh_" OrElse _
                 strColumnName.Substring(0, 3) = "qq_" OrElse _
                 strColumnName.PadRight(5).Substring(0, 5) = "pers_" Then
            strColName = strColumnName
            bOk = False
            If Not dttFieldAlias Is Nothing Then
              dtrT = dttFieldAlias.Select("datatable = '" & strColName & "'")
              If dtrT.Length > 0 Then
                strColName = dtrT(0)!database.ToString.ToLower()
                bOk = True
              End If
            End If
            If bOk = False Then
              If strColumnName.Substring(0, lAliasPrefissoCampoLength) = strAliasPrefissoCampo Then
                'sostituisco l'alias con il prefisso giusto
                strColName = strPrefissoCampo & strColumnName.Substring(lAliasPrefissoCampoLength)
              Else
                'mantengo il prefisso originale
                strColName = strColumnName
              End If
            End If

            If strColName.Trim <> "" Then
              strSQLBuilder.Append(strColName & " = ")
              If .AllowDBNull = True AndAlso dtrRiga(.ColumnName).ToString() = "" Then
                strSQLBuilder.Append("null")
              Else
                Select Case .DataType.ToString()
                  Case "System.String"
                    If oApp.DTTNvarchar.Select("tabella = '" & strTabella & "' AND colonna = '" & .ColumnName & "'").Length > 0 Then
                      'campo unicode
                      strSQLBuilder.Append("N" + CStrSQL(dtrRiga(.ColumnName)))
                    Else
                      strSQLBuilder.Append(CStrSQL(dtrRiga(.ColumnName)))
                    End If
                  Case "System.DateTime"
                    If strColumnName.IndexOf("ultagg") > -1 OrElse _
                       strColumnName.IndexOf("_start") > -1 OrElse _
                       strColumnName.IndexOf("_stop") > -1 OrElse _
                       strColumnName.IndexOf("dtor") > -1 OrElse _
                       strColumnName.IndexOf("dataora") > -1 Then
                      'distinguo tra datetime o solo date
                      'start/stop per lavcent-avlavp
                      Dim dtTmp As DateTime = NTSCDate(NTSCStr(dtrRiga(.ColumnName)))
                      If dtTmp.Hour <> 0 Or dtTmp.Minute <> 0 Or dtTmp.Second <> 0 Then
                        strSQLBuilder.Append(CDataOraSQL(dtrRiga(.ColumnName).ToString()))
                      Else
                        strSQLBuilder.Append(CDataSQL(dtrRiga(.ColumnName).ToString()))
                      End If
                    Else
                      'sempre e solo data 
                      strSQLBuilder.Append(CDataSQL(dtrRiga(.ColumnName).ToString()))
                    End If
                  Case Else
                    'sono sempre numeri
                    strSQLBuilder.Append(CDblSQL(dtrRiga(.ColumnName).ToString()))
                End Select
              End If    'If .AllowDBNull = True And dtrChange(i)(.ColumnName).ToString() = "" Then
              strSQLBuilder.Append(", ")
            End If    'If strColName.Trim <> "" Then
          End If    'If strColumnName.Substring(2, 1) = "_" And strColumnName <> "ts" Then
        End With
      Next    'For e = 0 To dsPaga.Tables("TABPAGA").Columns.Count

      strSQL = strSQLBuilder.ToString
      If strSQL.Length > 2 Then strSQL = strSQL.Remove(strSQL.Length - 2)

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dsOut As DataSet) As Boolean
    Try
      Return LeggiTabellaSemplice(strDitta, strNomeTabella, dsOut, False, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LeggiTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, _
                                                   ByRef dsOut As DataSet, ByVal bSoloCampiChiaveEDescr As Boolean, _
                                                   ByVal strWhere As String) As Boolean
    'si potrebbe fare una lettura dal database di una tabella che ritorna le query, ma rallenterebbe una operazione eseguita un sacco di volte...
    'volendo solo nel caso in cui il 'select case' non trova nulla, si potrebbe leggere dal database...

    'attenzione: non è la legtaberkall, ma solo un modo per evitare di fare tanti DAL specifici per programmini semplici che leggono/scrivono una tabella 
    Dim strSQL As String = ""
    Dim strTabella As String = ""
    Dim strCampiChiave As String = ""
    Dim dtrT() As DataRow
    Dim strPerDitta As String = "S"
    Dim strDescr As String = ""

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strNomeTabella, dsOut, bSoloCampiChiaveEDescr, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        'strDitta = NTSCStr(oIn(0))
        'strNomeTabella = NTSCStr(oIn(1))
        dsOut = CType(oIn(2), DataSet)        'alla funzione è passato ByRef !!!!
        'bSoloCampiChiaveEDescr = CBool(oIn(3))
        'strWhere = NTSCStr(oIn(4))
        Return CBool(oOut)
      End If
      '----------------

      '-------------------------
      'cerco i dati relativi alla tabella 
      strSQL = "dl_nome = '" & strNomeTabella & "'"
      If strNomeTabella.Substring(0, 3).ToUpper = "TAB" And strNomeTabella.Length = 7 Then strSQL += " OR dl_nome = 'TAB'"
      dtrT = oApp.DbAp.dttDalconf.Select(strSQL, "dl_nome DESC")

      If dtrT.Length = 0 Then
        Throw (New NTSException(oApp.Tr(Me, 127791221374218750, _
              "*** Tabella '|" + strNomeTabella.ToUpper + "|' non gestita dalla fuzione BD__BASE.LeggiTabellaSemplice (o non dichiarata in Arcproc.DALCONF)")))
      End If

      strTabella = dtrT(0)!dl_nometab.ToString
      strCampiChiave = dtrT(0)!dl_campichiave.ToString
      strDescr = NTSCStr(dtrT(0)!dl_descampo)
      'caso standard di tabelle generiche gestite per ditta
      If strTabella.ToUpper = "TAB" And strNomeTabella.Length = 7 And strNomeTabella.Substring(0, 3).ToUpper = "TAB" Then
        strTabella = strNomeTabella
        If strCampiChiave.Trim <> "" Then
          strCampiChiave += strNomeTabella.Substring(3)
          strDescr += strNomeTabella.Substring(3).ToLower
        End If
      End If

      '-------------------------
      'creo una indipendenza tra la tabella logica passata dall'entity e il nome effettivo della tabella nel database
      Select Case strNomeTabella.ToUpper
        Case "TABMAGA"
          strSQL = "SELECT tabmaga.*, an_descr1 as xx_magconto, tabmaga1.tb_desmaga as xx_magass, dd_nomdest as xx_coddest, tb_desstab as xx_desstab " & _
                           "FROM (((tabmaga LEFT JOIN tabmaga as tabmaga1 ON tabmaga.codditt = tabmaga1.codditt AND tabmaga.tb_magass = tabmaga1.tb_codmaga) " & _
                           "LEFT JOIN anagra on tabmaga.codditt = anagra.codditt AND tabmaga.tb_magconto = anagra.an_conto) " & _
                           "LEFT JOIN destdiv ON anagra.codditt = destdiv.codditt AND anagra.an_conto = destdiv.dd_conto AND tabmaga.tb_coddest = destdiv.dd_coddest) " & _
                           "LEFT JOIN tabstab ON tabmaga.codditt = tabstab.codditt AND tabmaga.tb_codstab = tabstab.tb_codstab " & _
                           "WHERE tabmaga.codditt = " & CStrSQL(strDitta) & " "
          If strWhere.Trim <> "" Then
            strSQL += " AND " & strWhere.Replace("tb_", "tabmaga.tb_") 'solo per autocompletamento con doppio click
          End If
          strSQL += "ORDER BY tabmaga.tb_codmaga "
        Case "TABPAGA"
          strSQL = "SELECT tabpaga.*, tb_descovg as xx_concassp " & _
                   "FROM tabpaga LEFT JOIN tabcovg ON tabpaga.tb_concassp = tabcovg.tb_codcovg "
          If strWhere.Trim <> "" Then strSQL += " WHERE " & strWhere
          strSQL += "ORDER BY tabpaga.tb_codpaga "
        Case Else
          strPerDitta = dtrT(0)!dl_perditta.ToString
          If dtrT(0)!dl_nometab.ToString.ToLower = "tab" Then
            'tabella generica: verifico in ordertbl se per ditta o no
            strPerDitta = IIf(IsPerDitta(True, strNomeTabella), "S", "N").ToString
          End If

          'caso standard: leggo dal database quanto serve per farmi restituire la query da eseguire
          strSQL = "SELECT * FROM " & strTabella
          If bSoloCampiChiaveEDescr Then
            strSQL = "SELECT " & strCampiChiave.Replace(";", ",") & IIf(strDescr.Trim <> "", ", " & strDescr, "").ToString & " FROM " & strTabella
          End If
          If strPerDitta = "S" Then
            strSQL += " WHERE codditt = " & CStrSQL(strDitta) & IIf(strWhere.Trim <> "", " AND " & strWhere, "").ToString
          Else
            If strWhere.Trim <> "" Then strSQL += " WHERE " & strWhere
          End If
          If strCampiChiave <> "" Then strSQL += " ORDER BY " & strCampiChiave.Replace(";", ",")
      End Select

      '-----------------------------
      'chiedo i dati al database
      dsOut = oCldBase.OpenRecordset(strSQL, CType(IIf(dtrT(0)!dl_db.ToString = "D", CLE__APP.DBTIPO.DBAZI, CLE__APP.DBTIPO.DBPRC), CLE__APP.DBTIPO), strNomeTabella)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dttTable As DataTable, _
                                        ByVal strActLogProg As String, ByVal strActLogNomOggLog As String, _
                                        ByVal strActLogDesLog As String) As Boolean
    Try
      Return ScriviTabellaSemplice(strDitta, strNomeTabella, dttTable, strActLogProg, strActLogNomOggLog, strActLogDesLog, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, _
                                                    ByRef dttTable As DataTable, ByVal strActLogProg As String, _
                                                    ByVal strActLogNomOggLog As String, _
                                                    ByVal strActLogDesLog As String, ByRef dbConn As DbConnection) As Boolean
    'attenzione: non è la legtaberkall, ma solo un modo per evitare di fare tanti DAL specifici per programmini semplici che leggono/scrivono una tabella 
    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    'Dim dsTmp As New DataSet
    Dim dtrT() As DataRow
    Dim strTipoDB As String = "D"       'D = db ditta, A = db arcproc
    Dim i As Integer
    Dim e As Integer

    Dim strTabella As String = ""
    Dim strCampoChiave() As String        'la chiave può essere anche più di una, basta che sia separata da '|'
    Dim strTipoCampoChiave() As String    'tipo di dato del campo chiave (S=stringa, N=numerico, D=data)
    Dim strPrefissoCampo As String = "tb_"
    Dim bPerDitta As Boolean = False
    Dim strActLogSqlWhere As String = ""
    Dim bInDelete As Boolean = False
    Dim strQuery As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strNomeTabella, dttTable, strActLogProg, strActLogNomOggLog, _
                                             strActLogDesLog, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTable = CType(oIn(2), DataTable)
        dbConn = CType(oIn(6), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      '-------------------------
      'cerco i dati relativi alla tabella 
      strSQL = "dl_nome = '" & strNomeTabella & "'"
      If strNomeTabella.Substring(0, 3).ToUpper = "TAB" And strNomeTabella.Length = 7 Then strSQL += " OR dl_nome = 'TAB'"
      dtrT = oApp.DbAp.dttDalconf.Select(strSQL, "dl_nome DESC")
      If dtrT.Length = 0 Then
        Throw (New NTSException(oApp.Tr(Me, 127791221374531250, "Tabella '|" + strNomeTabella.ToUpper + "|' non gestita dalla fuzione BD__BASE.ScriviTabellaSemplice (o non dichiarata in Arcproc.DALCONF)")))
      End If
      strTabella = dtrT(0)!dl_nometab.ToString
      strCampoChiave = dtrT(0)!dl_campichiave.ToString.Split(";"c)
      strTipoCampoChiave = dtrT(0)!dl_tipocampichiave.ToString.Split(";"c)
      strPrefissoCampo = dtrT(0)!dl_prefisso.ToString
      bPerDitta = CBool(IIf(dtrT(0)!dl_perditta.ToString = "S", True, False))

      If dtrT(0)!dl_nometab.ToString.ToLower = "tab" Then
        'tabella generica: verifico in ordertbl se per ditta o no
        bPerDitta = IsPerDitta(True, strNomeTabella)
      End If

      strTipoDB = dtrT(0)!dl_db.ToString

      'caso standard di tabelle generiche gestite per ditta
      If strTabella.ToUpper = "TAB" And strNomeTabella.Length = 7 And strNomeTabella.Substring(0, 3).ToUpper = "TAB" Then
        strTabella = strNomeTabella
        If strCampoChiave.Length = 1 Then
          strCampoChiave(0) += strNomeTabella.Substring(3)
        End If
      End If

      '-----------------------------
      'prima le delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        '---------
        'gestione di actlog
        If strActLogProg <> "" And strCampoChiave.Length > 0 Then
          strActLogSqlWhere = ""
          If bPerDitta Then strActLogSqlWhere = CStrSQL(strDitta)
          For e = 0 To strCampoChiave.Length - 1
            Select Case strTipoCampoChiave(e)
              Case "N" : strActLogSqlWhere += "," & CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
              Case "S" : strActLogSqlWhere += "," & CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
              Case "D" : strActLogSqlWhere += "," & CDataSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
            End Select
          Next
          If bPerDitta = False Then strActLogSqlWhere = strActLogSqlWhere.Substring(1)
          ScriviActLog(strDitta, strActLogProg, strActLogNomOggLog, strNomeTabella, strActLogSqlWhere, "A", "D", _
                      "DELETE: " & strActLogDesLog & " (" & IIf(bPerDitta, strActLogSqlWhere.Substring(strActLogSqlWhere.IndexOf(",") + 1), strActLogSqlWhere).ToString & ")", _
                      CBool(IIf(strTipoDB = "D", False, True)))
        End If
        '---------

        strSQL = "DELETE FROM " + strTabella + " WHERE "
        If bPerDitta Then strSQL = strSQL + strTabella + ".codditt = " & CStrSQL(strDitta) & " AND "
        For e = 0 To strCampoChiave.Length - 1
          strSQL = strSQL & strCampoChiave(e) & " = "
          Select Case strTipoCampoChiave(e)
            Case "N" : strSQL = strSQL & CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
            Case "S" : strSQL = strSQL & CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
            Case "D" : strSQL = strSQL & CDataSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
          End Select
        Next
        If bPerDitta Or strCampoChiave.Length > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 4)
        strQuery = strSQL
        oCldBase.Execute(strQuery, CType(IIf(strTipoDB = "D", CLE__APP.DBTIPO.DBAZI, CLE__APP.DBTIPO.DBPRC), CLE__APP.DBTIPO), dbConn)
        If Not oScriptE Is Nothing Then
          oScriptE.Exec("ScriviTabellaSemplice_DELETE", oApp, Me, CType(dtrChange(i), Object))
        End If
        dtrChange(i).AcceptChanges()
      Next
      bInDelete = False

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO " + strTabella + " " & GetQueryInsertField(dttTable, strPrefissoCampo)
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dttTable, dtrChange(i), strPrefissoCampo)
        strQuery = strSQL & " VALUES " & strSQLVal
        oCldBase.Execute(strQuery, CType(IIf(strTipoDB = "D", CLE__APP.DBTIPO.DBAZI, CLE__APP.DBTIPO.DBPRC), CLE__APP.DBTIPO), dbConn)
        If Not oScriptE Is Nothing Then
          oScriptE.Exec("ScriviTabellaSemplice_INSERT", oApp, Me, CType(dtrChange(i), Object))
        End If
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        '---------
        'gestione di actlog
        If strActLogProg <> "" And strCampoChiave.Length > 0 Then
          strActLogSqlWhere = ""
          If bPerDitta Then strActLogSqlWhere = CStrSQL(strDitta)
          For e = 0 To strCampoChiave.Length - 1
            Select Case strTipoCampoChiave(e)
              Case "N" : strActLogSqlWhere += "," & CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
              Case "S" : strActLogSqlWhere += "," & CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
              Case "D" : strActLogSqlWhere += "," & CDataSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
            End Select
          Next
          If bPerDitta = False Then strActLogSqlWhere = strActLogSqlWhere.Substring(1)
          ScriviActLog(strDitta, strActLogProg, strActLogNomOggLog, strNomeTabella, strActLogSqlWhere, "M", "D", _
                      "UPDATE: " & strActLogDesLog & " (" & IIf(bPerDitta, strActLogSqlWhere.Substring(strActLogSqlWhere.IndexOf(",") + 1), strActLogSqlWhere).ToString & ")", _
                      CBool(IIf(strTipoDB = "D", False, True)))
        End If
        '---------

        strSQL = "UPDATE " + strTabella + " SET " & GetQueryUpdate(dttTable, dtrChange(i), strPrefissoCampo)
        strSQL += " WHERE "
        If (bPerDitta) Then strSQL = strSQL + strTabella + ".codditt = " & CStrSQL(strDitta) & " AND "
        For e = 0 To strCampoChiave.Length - 1
          strSQL = strSQL & strCampoChiave(e) & " = "
          Select Case strTipoCampoChiave(e)
            Case "N" : strSQL = strSQL & CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
            Case "S" : strSQL = strSQL & CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
            Case "D" : strSQL = strSQL & CDataSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString()) & " AND "
          End Select
        Next
        If bPerDitta Or strCampoChiave.Length > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 4)
        strQuery = strSQL
        oCldBase.Execute(strQuery, CType(IIf(strTipoDB = "D", CLE__APP.DBTIPO.DBAZI, CLE__APP.DBTIPO.DBPRC), CLE__APP.DBTIPO), dbConn)
        If Not oScriptE Is Nothing Then
          oScriptE.Exec("ScriviTabellaSemplice_UPDATE", oApp, Me, CType(dtrChange(i), Object))
        End If
        dtrChange(i).AcceptChanges()
      Next

      '---------------------------------------
      'se serve memorizzo che le tabella CACHE devono essere rilette
      If oApp.bDSShared Then
        DBCacheAggDBCacheDate(strNomeTabella.ToUpper)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Try
        'se in fase di cancellaz di una riga si verifica un errore ripristino la riga corrente
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviActLog(ByVal strDitta As String, _
                             ByVal strNomprog As String, ByVal strNomogglog As String, _
                             ByVal strNomtab As String, ByVal strSqlwhere As String, _
                             ByVal strTipoop As String, ByVal strTipoogg As String, _
                             ByVal strDesogglog As String, ByVal fLocale As Boolean) As Boolean

    Dim bOk As Boolean = False
    Dim strDefault As String
    Dim strSQL As String = ""
    Dim strDati As String = ""
    Dim lProgrpadr As Integer = 0
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim n As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Dim strChiave() As String = Nothing
    Dim strValChiave() As String = Nothing
    Dim strDesCampo() As String = Nothing

    Dim strCampiActlog As String = "codditt, al_nomogglog, al_nomtab, al_tipoogglog, al_tabprim, " & _
                                   "al_desogglog, al_data, al_opnome, al_nomprog, al_tipoop, " & _
                                   "al_key, al_dati, al_progrpadr"
    Dim j As Integer
    Dim nPos As Integer

    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla se per il programma chiamante è attiva l'opzione di registro
      '--- per la scrittura del/dei record in ACTLOG
      '--- (se non è attiva esce restituendo TRUE)
      '--- se è False creca nelle OPZIONI generali
      '-----------------------------------------------------------------------------------------
      strDefault = GetSettingBus(strNomprog, "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strDefault = " " Then
        If strTipoogg.ToUpper = "D" Then
          bOk = oApp.ScriviActlogD
        Else
          'verifico se e una scriviactlogE o una scriviactlogL
          If strNomprog = "BS__MENU" And strNomogglog.ToUpper.IndexOf("BS__MENU") > -1 And strDitta = "." And fLocale = True Then
            bOk = True      'scriviactlogL è settata = -1, altrimenti non sari qui
          Else
            bOk = oApp.ScriviActlogE
          End If
        End If
      Else
        bOk = CBool(strDefault)
      End If


      If bOk = False Then Return True

      'caso specifico: tablingp Ã¨ la tabella delle lingue di arcproc!!! da non confondere con tabling del db azienda
      If strNomtab = "TABLINGP" Then strNomtab = "TABLING"

      '-----------------------------------------------
      'cerco nella tabella STRUCTLOG per vedere se nel file di log devo salvare dei valori 
      'presenti nel database prima della chiamata a questa operazione
      strDati = ""
      strSQL = "SELECT * FROM STRUCTLOG" & _
              " WHERE cb_nomogglog = " & CStrSQL(strNomogglog) & _
              " AND cb_nomtab = " & CStrSQL(IIf(strNomtab = "", " ", strNomtab)) & _
              " AND cb_tabprim = 'S'"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      'se è una elaborazione non devo restituire nulla
      If strTipoogg.ToUpper = "E" Then
        dttTmp.Clear()
      Else
        If dttTmp.Rows.Count = 0 And strSqlwhere <> "" Then
          If strNomtab.Trim <> "" Then
            strSQL = "dl_nome = '" & strNomtab & "'"
            If strNomtab.Substring(0, 3).ToUpper = "TAB" And strNomtab.Length = 7 Then strSQL += " OR dl_nome = 'TAB'"
            dtrT = oApp.DbAp.dttDalconf.Select(strSQL, "dl_nome DESC")
            If dtrT.Length > 0 Then
              If NTSCStr(dtrT(0)!dl_perditta) = "S" Then
                strChiave = ("codditt;" & NTSCStr(dtrT(0)!dl_campichiave)).Split(";"c)
              Else
                strChiave = NTSCStr(dtrT(0)!dl_campichiave).Split(";"c)
              End If
              For i = 0 To strChiave.Length - 1
                If strChiave(i) = "tb_cod" Then strChiave(i) = strChiave(i) & strNomtab.Substring(3).ToLower
              Next
              '---------------------------------------------------------------------------------------
              '--- Non fa più la Split ma tratta ogni porzione di stringa, perché se ad esempio
              '--- esiste '123,45' (codice articolo), questa veniva troncata
              '---------------------------------------------------------------------------------------
              j = 0
              nPos = 1
              ReDim strValChiave(39)
              For i = 1 To Len(strSqlwhere)
                If Mid(strSqlwhere, nPos, 1) = "" Then Exit For
                If Mid(strSqlwhere, nPos, 1) = "'" Then
                  Dim nPosi As Integer = InStr((nPos + 1), strSqlwhere, "'")
TrovaFineStringa1:
                  Dim strtmp As String = Mid(strSqlwhere, nPos, (nPosi - (nPos - 1)))
                  strtmp = Mid(strSqlwhere, nPosi, 2)
                  If Mid(strSqlwhere, nPosi, 2) = "''" Then
                    nPosi = InStr((nPosi + 2), strSqlwhere, "'")
                    GoTo TrovaFineStringa1
                  End If
                  strValChiave(j) = Mid(strSqlwhere, nPos, (nPosi - (nPos - 1)))
                  nPos = (nPosi + 2)
                Else
                  If InStr(nPos, strSqlwhere, ",") <> 0 Then
                    strValChiave(j) = Mid(strSqlwhere, nPos, (InStr(nPos + 1, strSqlwhere, ",") - nPos))
                    nPos = (InStr(nPos, strSqlwhere, ",") + 1)
                  Else
                    strValChiave(j) = Mid(strSqlwhere, nPos)
                    nPos = (nPos + Len(strValChiave(j)))
                  End If
                End If
                j = (j + 1)
              Next i
              'strValChiave = NTSCStr(strSqlwhere).Split(CType(",", Char))
            End If
            dttTmp.Rows.Add(dttTmp.NewRow())
          End If
        End If    'If dttTmp.Rows.Count = 0 Then
      End If

      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          If NTSCStr(!cb_sqlselect).Trim <> "" Then
            If NTSCStr(!cb_sqlselect) <> "" And NTSCStr(!cb_descaselect) <> "" And NTSCStr(!cb_sqlkey) <> "" Then
              strChiave = NTSCStr(!cb_sqlkey).Split(CType(",", Char))
              '---------------------------------------------------------------------------------------
              '--- Non fa più la Split ma tratta ogni porzione di stringa, perché se ad esempio
              '--- esiste '123,45' (codice articolo), questa veniva troncata
              '---------------------------------------------------------------------------------------
              j = 0
              nPos = 1
              ReDim strValChiave(39)
              For i = 1 To Len(strSqlwhere)
                If Mid(strSqlwhere, nPos, 1) = "" Then Exit For
                If Mid(strSqlwhere, nPos, 1) = "'" Then
                  Dim nPosi As Integer = InStr((nPos + 1), strSqlwhere, "'")
TrovaFineStringa:
                  Dim strtmp As String = Mid(strSqlwhere, nPos, (nPosi - (nPos - 1)))
                  strtmp = Mid(strSqlwhere, nPosi, 2)
                  If Mid(strSqlwhere, nPosi, 2) = "''" Then
                    nPosi = InStr((nPosi + 2), strSqlwhere, "'")
                    GoTo TrovaFineStringa
                  End If
                  strValChiave(j) = Mid(strSqlwhere, nPos, (nPosi - (nPos - 1)))
                  nPos = (nPosi + 2)
                Else
                  If InStr(nPos, strSqlwhere, ",") <> 0 Then
                    strValChiave(j) = Mid(strSqlwhere, nPos, (InStr(nPos + 1, strSqlwhere, ",") - nPos))
                    nPos = (InStr(nPos, strSqlwhere, ",") + 1)
                  Else
                    strValChiave(j) = Mid(strSqlwhere, nPos)
                    nPos = (nPos + Len(strValChiave(j)))
                  End If
                End If
                j = (j + 1)
              Next i
              'strValChiave = NTSCStr(strSqlwhere).Split(CType(",", Char))
              strDesCampo = NTSCStr(!cb_descaselect).Split(CType(",", Char))
            End If
            If Not strChiave Is Nothing Then
              If strChiave.Length > 0 Then
                strSQL = "SELECT TOP 1 " & NTSCStr(!cb_sqlselect) & " FROM " & !cb_nomtab.ToString & " WHERE "
                For i = 0 To strChiave.Length - 1
                  If strChiave(i) <> "" Then
                    strSQL += IIf(i <> 0, " AND ", "").ToString & strChiave(i).ToString & " = " & strValChiave(i).ToString
                  End If
                Next
              Else
                strSQL = ""
              End If
            Else
              strSQL = ""
            End If
          Else
            If Not strChiave Is Nothing Then
              If strChiave.Length > 0 Then
                strSQL = "SELECT TOP 1 * FROM " & strNomtab & " WHERE "
                For i = 0 To strChiave.Length - 1
                  If strChiave(i) <> "" Then
                    strSQL += IIf(i <> 0, " AND ", "").ToString & strChiave(i).ToString & " = " & strValChiave(i).ToString
                  End If
                Next
              Else
                strsql = ""
              End If
            Else
              strsql = ""
            End If

          End If
        End With    'With dttTmp.Rows(0)
        dttTmp.Clear()

        If strSQL <> "" Then

          If fLocale = False Then
            If oCldBase.IsInTrans Then
              dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, oCldBase.traDb.Connection)
            Else
              dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
          Else
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
          End If

          If dttTmp.Rows.Count > 0 Then
            For i = 0 To dttTmp.Columns.Count - 1
              If strDesCampo Is Nothing Then
                strDati += dttTmp.Columns(i).ColumnName & " = " & NTSCStr(dttTmp.Rows(0)(i)) & "§"
              Else
                If dttTmp.Rows(0)(i) Is Nothing Then
                  strDati += strDesCampo(i).Trim & " = ''§"
                Else
                  If dttTmp.Rows(0)(i).ToString = "" Then
                    strDati += strDesCampo(i).Trim & " = '" & dttTmp.Rows(0)(i).ToString & "'§"
                  Else
                    strDati += strDesCampo(i).Trim & " = " & dttTmp.Rows(0)(i).ToString & "§"
                  End If
                End If
              End If
            Next
          End If
          dttTmp.Clear()
        Else
          dttTmp.Clear()
        End If    'If dttTmp.Rows.Count > 0 Then

      End If    'If strSQL <> "" Then

      '-----------------------------------------------
      'inserisco il record in ACTLOG
      strSQL = "INSERT INTO actlog (" & strCampiActlog & " )" & _
              " VALUES (" & CStrSQL(strDitta) & ", " & _
              CStrSQL(strNomogglog) & ", " & _
              CStrSQL(IIf(strNomtab = "", " ", strNomtab).ToString) & ", " & _
              CStrSQL(strTipoogg) & ", " & _
              "'S', " & _
              CStrSQL(strDesogglog) & ", " & CDataOraSQL(Now.ToString) & ", " & _
              CStrSQL(oApp.User.Nome) & ", " & CStrSQL(strNomprog) & ", " & _
              CStrSQL(strTipoop) & ", " & CStrSQL(strSqlwhere) & ", " & _
              CStrSQL(strDati) & ", 0)"
      If fLocale = False Then
        If oCldBase.IsInTrans Then
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, oCldBase.traDb.Connection)
        Else
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      Else
        If oCldBase.IsInTrans Then
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC, oCldBase.traDb.Connection)
        Else
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If
      End If

      '-----------------------------------------------
      'mi faccio ritornare il progressivo del record appena inserito
      strSQL = "SELECT Max(al_progr) As Progr FROM actlog WHERE codditt = " & CStrSQL(strDitta)
      If fLocale = False Then
        If oCldBase.IsInTrans Then
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, oCldBase.traDb.Connection)
        Else
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      Else
        If oCldBase.IsInTrans Then
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, oCldBase.traDb.Connection)
        Else
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If
      End If
      lProgrpadr = NTSCInt(dttTmp.Rows(0)!Progr)
      dttTmp.Clear()
      '-----------------------------------------------
      'cerco in structlog se devo inserire altre righe in actlog (record collegati al primo)

      strSQL = "SELECT * FROM STRUCTLOG" & _
              " WHERE cb_nomogglog = " & CStrSQL(strNomogglog) & _
              " AND cb_tabprim = 'N' AND not cb_sqlselect is null"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      For e = 0 To dttTmp.Rows.Count - 1
        With dttTmp.Rows(e)
          strDati = ""

          If NTSCStr(!cb_sqlselect) <> "" And NTSCStr(!cb_descaselect) <> "" And NTSCStr(!cb_sqlkey) <> "" Then
            strChiave = NTSCStr(!cb_sqlkey).Split(CType(",", Char))
            '-----------------------------------------------------------------------------------
            j = 0
            nPos = 1
            ReDim strValChiave(39)
            For i = 1 To Len(strSqlwhere)
              If Mid(strSqlwhere, nPos, 1) = "" Then Exit For
              If Mid(strSqlwhere, nPos, 1) = "'" Then
                strValChiave(j) = Mid(strSqlwhere, nPos, (InStr((nPos + 1), strSqlwhere, "'") - (nPos - 1)))
                nPos = (InStr((nPos + 1), strSqlwhere, "'") + 2)
              Else
                If InStr(nPos, strSqlwhere, ",") <> 0 Then
                  strValChiave(j) = Mid(strSqlwhere, nPos, (InStr(nPos, strSqlwhere, ",") - nPos))
                  nPos = (InStr(nPos, strSqlwhere, ",") + 1)
                Else
                  strValChiave(j) = Mid(strSqlwhere, nPos)
                  nPos = (nPos + Len(strValChiave(j)))
                End If
              End If
              j = (j + 1)
            Next i
            'strValChiave = NTSCStr(strSqlwhere).Split(CType(",", Char))
            '-----------------------------------------------------------------------------------
            strDesCampo = NTSCStr(!cb_descaselect).Split(CType(",", Char))
          End If
          If Not strChiave Is Nothing Then
            strSQL = "SELECT " & NTSCStr(!cb_sqlselect) & " FROM " & !cb_nomtab.ToString & " WHERE "
            For i = 0 To strChiave.Length - 1
              If strValChiave(i) <> "" Then
                strSQL += IIf(i <> 0, " AND ", "").ToString & strChiave(i).ToString & " = " & strValChiave(i).ToString
              End If
            Next
          Else
            strSQL = ""
          End If

          If strSQL <> "" Then

            If fLocale = False Then
              If oCldBase.IsInTrans Then
                dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, oCldBase.traDb.Connection)
              Else
                dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
              End If
            Else
              If oCldBase.IsInTrans Then
                dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, oCldBase.traDb.Connection)
              Else
                dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
              End If
            End If

            For n = 0 To dttTmp1.Rows.Count - 1
              With dttTmp1.Rows(n)
                strDati = ""
                For i = 0 To dttTmp1.Columns.Count - 1
                  If dttTmp1.Rows(n)(i) Is Nothing Then
                    strDati += strDesCampo(i).Trim & " = ''§"
                  Else
                    If dttTmp1.Rows(n)(i).ToString = "" Then
                      strDati += strDesCampo(i).Trim & " = '" & dttTmp1.Rows(n)(i).ToString & "'§"
                    Else
                      strDati += strDesCampo(i).Trim & " = " & dttTmp1.Rows(n)(i).ToString & "§"
                    End If
                  End If
                Next
                strSQL = "INSERT INTO actlog (" & strCampiActlog & " )" & _
                        " VALUES (" & CStrSQL(strDitta) & ", " & _
                        CStrSQL(strNomogglog) & ", " & _
                        CStrSQL(NTSCStr(dttTmp.Rows(e)!cb_nomtab)) & ", " & _
                        CStrSQL(strTipoogg) & ", " & _
                        "'N', " & _
                        CStrSQL(strDesogglog) & ", " & CDataOraSQL(Now.ToString) & ", " & _
                        CStrSQL(oApp.User.Nome) & ", " & CStrSQL(strNomprog) & ", " & _
                        CStrSQL(strTipoop) & ", " & CStrSQL(strSqlwhere) & ", " & _
                        CStrSQL(strDati) & ", " & lProgrpadr & ")"
                If fLocale = False Then
                  If oCldBase.IsInTrans Then
                    oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, oCldBase.traDb.Connection)
                  Else
                    oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
                  End If
                Else
                  If oCldBase.IsInTrans Then
                    oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC, oCldBase.traDb.Connection)
                  Else
                    oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
                  End If
                End If

              End With
            Next
            dttTmp1.Clear()


          End If    'If strSQL <> "" Then

        End With    'With dttTmp.Rows(e)
      Next    'For e = 0 To dttTmp.Rows.Count - 1
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaCambioDiOggi(ByVal nCodVal As Integer, ByVal dtDataValidita As String) As Decimal
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim strCambMens As String
    Dim dtDataInizioMese As Date
    Dim dCambio As Decimal
    Try
      strSQL = "SELECT * FROM tabvalu WHERE tb_codvalu = " & nCodVal
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")
      If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
        Return 0
      Else
        If dsTmp.Tables("TABVALU").Rows(0)!tb_ue.ToString = "S" Then
          Return 1
        Else
          strCambMens = dsTmp.Tables("TABVALU").Rows(0)!tb_cambmens.ToString
        End If
      End If

      dsTmp.Tables.Clear()
      If strCambMens = "S" Then
        dtDataInizioMese = NTSCDate(IntSetDate("01/" & _
                                 NTSCDate(dtDataValidita).Month.ToString("00") & "/" & _
                                 NTSCDate(dtDataValidita).Year.ToString()))
        strSQL = "SELECT wx_cambio FROM CAMBI WHERE wx_codvalu = " & nCodVal.ToString & _
                 " AND wx_dtvalid <= " & CDataSQL(dtDataValidita) & _
                 " AND wx_dtvalid >= " & CDataSQL(dtDataInizioMese) & _
                 " ORDER BY wx_dtvalid DESC"
      Else
        strSQL = "SELECT wx_cambio FROM CAMBI WHERE wx_codvalu = " & nCodVal.ToString & _
                 " AND wx_dtvalid = " & CDataSQL(dtDataValidita)
      End If

      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CAMBI")
      If dsTmp.Tables("CAMBI").Rows.Count = 0 Then
        dCambio = 0
      Else
        dCambio = NTSCDec(dsTmp.Tables("CAMBI").Rows(0)!wx_cambio)
      End If

      dsTmp.Tables.Clear()
      dsTmp.Dispose()

      ' ora arrotonda dCambio per avere alla fine un numero di dec. pari a 6 cifre significative

      'ArrDbl()
      'senza il 3° parametro  1.1449, 2  -> 1.14        come con MidpointRounding.ToEven
      '                       1.1450, 2  -> 1.14        come con MidpointRounding.ToEven
      '                       1.1451, 2  -> 1.15        come con MidpointRounding.ToEven
      'con 3° AwayFromZero    1.1449, 2  -> 1.14        come ArrDbl di Bus
      '                       1.1450, 2  -> 1.15        come ArrDbl di Bus
      '                       1.1451, 2  -> 1.15        come ArrDbl di Bus

      Select Case dCambio
        Case Is < NTSCDec(0.001) : Return ArrDbl(dCambio, 9) ' 6 decimali
        Case Is < NTSCDec(0.01) : Return ArrDbl(dCambio, 8) ' 6 decimali
        Case Is < NTSCDec(0.1) : Return ArrDbl(dCambio, 7) ' 6 decimali
        Case Is < NTSCDec(1) : Return ArrDbl(dCambio, 6) ' 6 decimali
        Case Is < NTSCDec(10) : Return ArrDbl(dCambio, 5)
        Case Is < NTSCDec(100) : Return ArrDbl(dCambio, 4)
        Case Is < NTSCDec(1000) : Return ArrDbl(dCambio, 3)
        Case Else : Return ArrDbl(dCambio, 2)
      End Select

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return 0
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AggiornaCambio(ByVal nCodVal As Integer, ByVal dtDataValidita As String, ByVal dCambio As Decimal, _
                            ByVal bSovrascrivi As Boolean)
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim strCambMens As String
    Dim bEsist As Boolean

    Try
      strSQL = "SELECT * FROM tabvalu WHERE tb_codvalu = " & nCodVal
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")
      If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
        Return
      Else
        If dsTmp.Tables("TABVALU").Rows(0)!tb_ue.ToString = "S" Then
          Return
        Else
          strCambMens = dsTmp.Tables("TABVALU").Rows(0)!tb_cambmens.ToString
        End If
      End If

      dsTmp.Tables.Clear()

      If strCambMens = "S" Then
        dtDataValidita = NTSCDate(IntSetDate("01/" & _
                               NTSCDate(dtDataValidita).Month.ToString("00") & "/" & _
                               NTSCDate(dtDataValidita).Year.ToString())).ToShortDateString
      End If

      'Controlla se esiste
      bEsist = False
      strSQL = "SELECT top 1 * FROM CAMBI WHERE wx_codvalu = " & nCodVal & _
               " AND wx_dtvalid = " & CDataSQL(dtDataValidita)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CAMBI")
      If dsTmp.Tables("CAMBI").Rows.Count <> 0 Then bEsist = True
      dsTmp.Tables.Clear()

      If bEsist And bSovrascrivi = False Then Return

      If bEsist Then
        'Se esiste
        strSQL = "UPDATE cambi SET wx_cambio = " & CDblSQL(dCambio) & " WHERE wx_codvalu = " & nCodVal & " AND wx_dtvalid = " & CDataSQL(dtDataValidita)
      Else
        'Se non esiste
        strSQL = "INSERT INTO cambi (wx_codvalu, wx_dtvalid, wx_cambio) VALUES (" & nCodVal & ", " & CDataSQL(dtDataValidita) & ", " & CDblSQL(dCambio) & ")"
      End If
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function AliquotaIva(ByVal nCodiceIva As Integer) As Decimal
    Dim strSQL As String
    Dim dsTmp As DataSet

    Try
      strSQL = "SELECT tb_aliq FROM TABCIVA WHERE tb_codciva = " & nCodiceIva.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count = 0 Then
        Return 0
      Else
        Return NTSCDec(dsTmp.Tables("TMP").Rows(0)!tb_aliq)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetEmailBusUser(ByVal strDitta As String, ByVal strUser As String) As String
    Dim strSQL As String
    Dim dttTmp As New DataTable
    Try
      GetEmailBusUser = ""
      strSQL = "SELECT og_email FROM organig WHERE codditt = " & CStrSQL(strDitta) & " AND og_conto = 0 AND og_codlead = 0 AND og_coperat = " & CStrSQL(strUser)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCStr(dttTmp.Rows(0)!og_email)
      End If

      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  '--------------------------------------------------------------------------
  '  Passato Imponibile e codice CalcolaIva restituisce l'imposta
  '
  '  Par. opzionale: nDec -> Numero decimali richiesto
  '--------------------------------------------------------------------------
  Public Overridable Function CalcolaIva(ByVal dImponibile As Decimal, ByVal CodiceIva As Integer, Optional ByVal nDec As Integer = 2) As Decimal
    Dim bMatem As Boolean = False

    Try
      If nDec = 2 Then
        If oApp.oGvar.strTipoArrotondamentoIvaEuro = "M" Then
          bMatem = True
        End If
      End If
      '
      If bMatem = False Then ' come prima, e come in lire
        CalcolaIva = ArrDblEcc((dImponibile * AliquotaIva(CodiceIva) / 100) - NTSCDec((IIf(dImponibile > 0, 0.0000000001, IIf(dImponibile = 0, 0, 0.0000000001)))), nDec)
      Else ' ora arrotondamento matematico ..
        CalcolaIva = ArrDbl((dImponibile * AliquotaIva(CodiceIva) / 100), nDec)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Scorporo(ByVal dIvaInclusa As Decimal, ByVal nCodiceIva As Integer, ByRef dImponibile As Decimal, ByRef dImposta As Decimal, Optional ByVal nDec As Integer = -1)
    '--------------------------------------------------------------------------
    ' EFFUETTUA LO SCORPORO DELL'IVA
    ' SOLO PER I PREZZI, e gli importi dove si vuole scegliere
    ' il numero dei decimali di arrotondamento dell'imponibile
    '
    '
    ' IN:  dIvaInclusa  --> Importo IVA inclusa
    '      nCodiceIva   --> Codice IVA
    '      (OPZ) nDec   --> Numero decimali
    '
    ' OU:  dImponibile  --> Imponibile
    '      dImposta     --> imposta
    '--------------------------------------------------------------------------
    Dim dAliq As Decimal
    Dim strSQL As String
    Dim dsTmp As DataSet

    Try
      If nDec = -1 Then nDec = oApp.NDecImporti
      strSQL = "SELECT tb_aliq FROM TABCIVA WHERE tb_codciva = " & nCodiceIva.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count = 0 Then
        dAliq = 0
      Else
        dAliq = NTSCDec(dsTmp.Tables("TMP").Rows(0)!tb_aliq)
      End If

      If dAliq = 0 Then
        dImponibile = dIvaInclusa
        dImposta = 0
      Else
        dIvaInclusa = ArrDbl(dIvaInclusa, nDec)
        dImponibile = ArrDbl((dIvaInclusa * 100) / (100 + dAliq), nDec)
        dImposta = dIvaInclusa - dImponibile
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  '--------------------------------------------------------------------------
  ' Converte in lire o euro un importo in valuta estera
  '
  ' IN:  dImpval (imp da convertire )
  '      CodVal -> Codice valuta da trattare
  '      dtdatavalidita -> data della conversione
  '      dCambiout  -> cambio ev. impostato dall'utente (cambi variabili)
  '      bCancel - > true se non trovati i dati per effettuare la conversione
  ' OU:  ConvImpvaluta -> controvalore in lire o euro
  '--------------------------------------------------------------------------
  Public Overridable Function ConvImpValuta(ByVal strDitta As String, ByVal bPrzun As Boolean, ByVal dImpval As Decimal, ByVal nCodVal As Integer, ByVal dtDataValidita As Date, ByVal dCambiout As Decimal, Optional ByRef bCancel As Boolean = False) As Decimal
    'ora accorpa anche la funzione di vb6 'ConvPrezzoUnValuta'
    Dim strSQL As String
    Dim dsTmp As DataSet
    Dim dtrTmp As DataRow

    Dim dImporto As Decimal = 0
    Dim strDividi As String = "D"
    Dim dPerqtav As Decimal = 1

    Try
      If nCodVal = 0 Then
        dImporto = dImpval
      Else

        strSQL = "SELECT * FROM tabvalu WHERE tb_codvalu = " & nCodVal
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")
        If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
          dImporto = 0
        Else

          dtrTmp = dsTmp.Tables("TABVALU").Rows(0)
          '--------------------------------------------------
          'valuta europea
          If dtrTmp!tb_ue.ToString = "S" Then
            If NTSCDec(dtrTmp!tb_cambeuro) > 0 Then
              If bPrzun Then
                dImporto = ArrDbl(dImpval / NTSCDec(dtrTmp!tb_cambeuro), oApp.NDecPrzUn)
              Else
                dImporto = ArrDbl(dImpval / NTSCDec(dtrTmp!tb_cambeuro), 2)
              End If
            Else
              dImporto = 0
              bCancel = True
            End If
          Else
            '--------------------------------------------------
            'valuta non europea

            strDividi = dtrTmp!tb_moltdiv.ToString
            dPerqtav = NTSCDec(dtrTmp!tb_perqtav)
            Err.Clear()

            If dCambiout <> 0 Then
              If strDividi = "D" Then
                If bPrzun Then
                  dImporto = ArrDbl(dImpval / dCambiout, oApp.NDecPrzUn)
                Else
                  dImporto = ArrDbl(dImpval / dCambiout, 2)
                End If
              Else
                If bPrzun Then
                  dImporto = ArrDbl(dImpval * dCambiout, oApp.NDecPrzUn)
                Else
                  dImporto = ArrDbl(dImpval * dCambiout, 2)
                End If
              End If
            Else
              dImporto = 0
            End If
          End If    'If dtrTmp!tb_ue.ToString = "S" Then

        End If    'If dsTmp.Tables("TABVALU").Rows.Count = 0 Then

        dsTmp.Tables.Clear()
        dsTmp.Dispose()

      End If    'If nCodVal = 0 Then

      Return dImporto

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  '--------------------------------------------------------------------------
  ' Converte un importo dato in lire o euro,  in un importo in valuta
  '
  ' IN:  dImportoin (imp da convertire espresso in unità monetaria principale)
  '      nCodVal -> Codice valuta da trattare
  '      dtdatavalidita -> data della conversione
  '      dCambiout  -> cambio ev. impostato dall'utente (cambi variabili)
  '      bCancel - > true se non trovati i dati per effettuare la conversione
  ' OU:  ConvImpEur -> importo in valuta ricavato
  '--------------------------------------------------------------------------
  Public Overridable Function ConvImpEur(ByVal bPrzun As Boolean, ByVal dImportoin As Decimal, ByVal nCodVal As Integer, _
                             ByVal dtDataValidita As DateTime, ByRef dCambiout As Decimal, _
                             Optional ByVal bCancel As Boolean = False) As Decimal
    'ConvImpLireur e ConvPrezzoUnLireur di Business
    Dim strSQL As String
    Dim dsTmp As DataSet
    Dim dtrValu As DataRow

    Dim dCom As Decimal = 0
    Dim dImpval As Decimal = 0
    Dim strDividi As String             ' = true l'importo in valuta deve venir diviso per il cambio, diversamente deve venir moltiplicato
    Dim dPerqtav As Decimal             ' = indica per quante unità di valuta estera  relativo il cambio

    Try
      bCancel = False

      Select Case nCodVal
        Case 0
          dImpval = dImportoin
        Case Else
          '--------------------------------------
          'codici valuta diversi da 0, tutte le altre (compresa la 100=LIT dopo EUR)
          strSQL = "SELECT  * FROM tabvalu WHERE tb_codvalu = " & nCodVal
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")

          If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
            dsTmp.Tables.Clear()
            GoTo fine
          Else
            dtrValu = dsTmp.Tables("TABVALU").Rows(0)
          End If

          '--------------------------------------
          ' trovata la valuta, controlla se valuta europea ...
          ' se si valuta se siamo nel erpiodo transitorio, e se adottiamo lire od euro come
          ' valuta di default o principale
          If dtrValu!tb_ue.ToString = "S" Then ' est 100=lire  oppre franchi, ecc.
            ' caso siamo ancora in lire
            If NTSCInt(dtrValu!tb_cambeuro) > 0 Then
              If bPrzun Then
                dImpval = ArrDbl(dImportoin * NTSCDec(dtrValu!tb_cambeuro), TrovaNdecSuPrzUn(nCodVal)) ' perchè euro
              Else
                dImpval = ArrDbl(dImportoin * NTSCDec(dtrValu!tb_cambeuro), TrovaNdec(nCodVal)) ' perchè euro
              End If
            Else
              dImpval = 0
              bCancel = True
              dCom = 0 'per dargli un valore: dovrebbe avvertire che non ha senso...
            End If
          End If    'If dtrValu!tb_ue.ToString = "S" Then

          '--------------------------------------
          'Per tutte le altre valute , quelle non ue, siamo a cambio flutt., pecio cerca in cambi
          ' tuttavia se dopo 31/12/98 devo passare per il cambio euro (triangolazione)
          If dtrValu!tb_ue.ToString <> "S" Then

            dCom = dCambiout
            strDividi = "D"              'caso standard (ex: euro/dollaro)
            dPerqtav = 1                 'default
            strDividi = dtrValu!tb_moltdiv.ToString
            'Davide - errore overflow
            If dCambiout <> 0 Then
              If strDividi = "D" Then
                If bPrzun Then
                  dImpval = ArrDbl((dImportoin * dCambiout) * dPerqtav, TrovaNdecSuPrzUn(nCodVal)) ' perchè euro
                Else
                  dImpval = ArrDbl((dImportoin * dCambiout) * dPerqtav, TrovaNdec(nCodVal)) ' perchè euro
                End If
              Else
                If bPrzun Then
                  dImpval = ArrDbl((dImportoin / dCambiout) * dPerqtav, TrovaNdecSuPrzUn(nCodVal)) ' perchè euro
                Else
                  dImpval = ArrDbl((dImportoin / dCambiout) * dPerqtav, TrovaNdec(nCodVal)) ' perchè euro
                End If
              End If
            Else
              dImpval = 0
            End If
          End If    'If dtrValu!tb_ue.ToString <> "S" Then

          dsTmp.Tables.Clear()
          dsTmp.Dispose()
      End Select

      Return dImpval
fine:
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaNdec(ByVal nCodvaluta As Integer) As Integer
    Dim strSQL As String
    Dim dsTmp As DataSet
    Dim nDec As Integer = 0

    Try
      '-----------------------------------------------------------
      ' restituisce il nuemro di decimali da usarsi per es. negli arrotondamenti ...
      ' est una copia del metodo pubblico in std.cls (da modificare entrambi se nel caso ...)
      Select Case nCodvaluta
        Case 0
          nDec = 2
        Case 99
          nDec = 2 ' euro, fisse
        Case 100
          nDec = 0 ' lire fisse
        Case Else
          If Not oApp.NdecDaTabvalu Then
            nDec = 2 ' altre valute
          Else
            strSQL = "SELECT tb_ndec FROM tabvalu WHERE tb_codvalu = " & nCodvaluta
            dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")
            If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
              nDec = 2
            Else
              nDec = NTSCInt(dsTmp.Tables("TABVALU").Rows(0)!tb_ndec)
            End If
          End If    ' If Not (gbNdecDaTabvalu) Then
      End Select

      Return nDec

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaNdecSuPrzUn(ByVal nCodvalu As Integer) As Integer
    If nCodvalu = 0 Then
      Return oApp.NDecPrzUn
    Else
      Return oApp.NDecPrzUnVal
    End If
  End Function

  Public Overridable Function TrovaContoCFDaPivaCodfisAnagen(ByVal strDitta As String, ByVal strTipoconto As String, _
                                          ByVal strPiva As String, ByVal strCodfisc As String, _
                                          ByVal lCodAnagen As Integer) As Integer
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim lOut As Integer = 0
    Try

      If strPiva = "" And strCodfisc = "" And lCodAnagen = 0 Then
        Throw (New NTSException(oApp.Tr(Me, 127791221374687500, "Indicare almeno la Partita IVA o il Codice Fiscale o il codice di Anagrafica Generale")))
      End If

      strSQL = "SELECT an_conto FROM anagra " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If strTipoconto <> "" Then strSQL += " AND an_tipo = " & CStrSQL(strTipoconto)
      If strPiva <> "" Then strSQL += " AND an_pariva = " & CStrSQL(strPiva)
      If strCodfisc <> "" Then strSQL += " AND an_codfis = " & CStrSQL(strCodfisc)
      If lCodAnagen <> 0 Then strSQL += " AND an_codanag = " & lCodAnagen

      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGRA")
      If dsTmp.Tables("ANAGRA").Rows.Count > 0 Then lOut = NTSCInt(dsTmp.Tables("ANAGRA").Rows(0)!an_conto)
      dsTmp.Tables.Clear()
      Return lOut

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TrovaContoDaCodcontr(ByVal strCodditt As String, ByVal nCodcovg As Integer) As Integer
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strPianoConti As String = ""
    Dim lOut As Integer = 0
    Try

      If nCodcovg < 1 Or nCodcovg > 9999 Then Return 0

      'Prima cerco il conto tra quelli specifici per la ditta
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABCOVE") Then
        Dim dtrT() As DataRow = oApp.DSShared.Tables("TABCOVE").Select("codditt = " & CStrSQL(strCodditt) & " AND tb_codcove = " & nCodcovg.ToString)
        If dtrT.Length > 0 Then
          Return NTSCInt(dtrT(0)!tb_concove)
        End If
      Else
        strSQL = "SELECT tb_concove FROM tabcove WHERE tb_codcove = " & nCodcovg.ToString & " and codditt = " & CStrSQL(strCodditt)
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCOVE")
        If dsTmp.Tables("TABCOVE").Rows.Count > 0 Then
          lOut = NTSCInt(dsTmp.Tables("TABCOVE").Rows(0)!tb_concove.ToString)
          dsTmp.Tables.Clear()
          Return lOut
        End If
      End If

      'Cerco nel piano dei conti generale
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABANAZ") Then
        strPianoConti = NTSCStr(oApp.DSShared.Tables("TABANAZ").Select("codditt = " & CStrSQL(strCodditt))(0)!tb_azcodpcon)
      Else
        strSQL = "SELECT tb_azcodpcon FROM tabanaz WHERE codditt = " & CStrSQL(strCodditt)
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
        strPianoConti = dsTmp.Tables("TABANAZ").Rows(0)!tb_azcodpcon.ToString
        dsTmp.Tables.Clear()
      End If

      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABCOVP") Then
        Dim dtrT() As DataRow = oApp.DSShared.Tables("TABCOVP").Select("tb_codpcon = " & CStrSQL(strPianoConti) & " AND tb_codcovp = " & nCodcovg.ToString)
        If dtrT.Length > 0 Then
          Return NTSCInt(dtrT(0)!tb_concove)
        End If
      Else
        strSQL = "SELECT tb_concove FROM tabcovp WHERE tb_codcovp = " & nCodcovg.ToString & " and tb_codpcon = " & CStrSQL(strPianoConti)
        dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCOVP")
        If dsTmp.Tables("TABCOVP").Rows.Count > 0 Then
          lOut = NTSCInt(dsTmp.Tables("TABCOVP").Rows(0)!tb_concove.ToString)
          dsTmp.Tables.Clear()
          Return lOut
        End If
      End If

      'non dovrebbe mai arrivare qui ...
      Return 0

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TrovaContoDaContrEConto(ByVal strCodditt As String, ByVal nCodcovg As Integer, ByVal lConto As Integer) As Integer
    Dim lConto1 As Integer = 0
    Try

      If nCodcovg <> 0 Then
        lConto1 = TrovaContoDaCodcontr(strCodditt, nCodcovg)
      End If
      If lConto1 = 0 Then lConto1 = lConto

      Return lConto1

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function LegAggRegcDitt(ByVal strCodditt As String, ByVal strData As String) As Integer
    Return LegAggRegcDitt(strCodditt, strData, Nothing)
  End Function
  Public Overridable Function LegAggRegcDitt(ByVal strCodditt As String, ByVal strData As String, _
                                             ByRef dbConn As DbConnection) As Integer
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim lOut As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodditt, strData, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(2), DbConnection)
        Return NTSCInt(oOut)
      End If
      '----------------
      If CBool(GetSettingBus("OPZIONI", ".", ".", "NumerazioneSuAnno", "0", " ", "0")) Then strData = New Date((NTSCDate(strData)).Year, 1, 1).ToShortDateString

      strSQL = "SELECT * FROM TABREGB Where tb_regbdata = " & CDataSQL(strData) & " and codditt = " & CStrSQL(strCodditt)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABREGB", Nothing, dbConn)
      If dsTmp.Tables("TABREGB").Rows.Count = 0 Then
        lOut = 1
        strSQL = "INSERT INTO TABREGB ( codditt, tb_regbdata, tb_regbnum ) values (" & CStrSQL(strCodditt) & "," & CDataSQL(strData) & ", " & lOut & ")"
      Else
        lOut = NTSCInt(dsTmp.Tables("TABREGB").Rows(0)!tb_regbnum.ToString) + 1
        strSQL = "UPDATE TABREGB SET  tb_regbnum = " & lOut & " Where tb_regbdata = " & CDataSQL(strData) & " and codditt = " & CStrSQL(strCodditt)
      End If
      dsTmp.Tables.Clear()
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return lOut

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function LegAngeDataRkAll(ByVal nValuein As Integer, ByVal dtDataval As Date, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim bDaAnagen As Boolean

    Try

      If dtDataval = Nothing Then
        bDaAnagen = True
      Else
        bDaAnagen = False
      End If

      If bDaAnagen Then
        strSQL = " SELECT * FROM ANAGEN WHERE ag_codanag = " & nValuein.ToString
      Else
        strSQL = " SELECT * FROM ANASTO WHERE as_codanag = " & nValuein.ToString & _
               " AND as_datini <= " & CDataSQL(dtDataval) & _
               " AND as_datfin >= " & CDataSQL(dtDataval)
        'Se la data è antecedente, la data di inserimento iniziale dati ?
        'Se la data è posteriore, la ultima data di validità ? (dovrebbe superare 31/12/2099, molto improbabile)
        'in entrambi i casi il recordset è vuoto (dati non trovati)
      End If
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then
        'se la lettura è stata fatta su anasto e non ho trovato nessun record, rifaccio la lettura prendendo il primo
        If bDaAnagen = False Then
          strSQL = " SELECT TOP 1 * FROM ANASTO WHERE as_codanag = " & nValuein.ToString & " ORDER BY as_datini ASC"
          dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttOut.Rows.Count = 0 Then
            Return False
          End If
        Else
          Return False
        End If
      End If

      If bDaAnagen Then
        dttOut.TableName = "ANAGEN"
      Else
        dttOut.TableName = "ANASTO"
      End If

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetTblInstId(ByVal strNomtab As String, ByVal bArcproc As Boolean) As Integer
    Try
      Return GetTblInstId(strNomtab, bArcproc, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTblInstId(ByVal strNomtab As String, ByVal bArcproc As Boolean, _
                                           ByVal dbConn As DbConnection) As Integer
    Dim strSQL As String = ""
    Dim ds As New DataSet
    Dim lCom As Integer = 0

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strNomtab, bArcproc, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(2), DbConnection)
        Return NTSCInt(oOut)
      End If
      '----------------

      strSQL = "SELECT ot_instid FROM ordertbl WHERE ot_nometab = '" & strNomtab & "' "
      ds = oCldBase.OpenRecordset(strSQL, CType(IIf(bArcproc, CLE__APP.DBTIPO.DBPRC, CLE__APP.DBTIPO.DBAZI), CLE__APP.DBTIPO), "ORDERTBL", Nothing, dbConn)
      If ds.Tables("ORDERTBL").Rows.Count = 0 Then
        ds.Tables.Clear()
        Throw New NTSException(oApp.Tr(Me, 127939842516406250, "GetTblInstID: non è stata trovata la tabella '|" & strNomtab & "|' nella ORDERTBL"))
        Return 0
      End If

      lCom = NTSCInt(ds.Tables("ORDERTBL").Rows(0)!ot_instid.ToString) + 1
      If lCom > 1000000 Then
        lCom = 1
      End If
      ds.Tables.Clear()

      strSQL = "UPDATE ordertbl SET ot_instid = " & lCom & " WHERE ot_nometab = '" & strNomtab & "' "
      oCldBase.Execute(strSQL, CType(IIf(bArcproc, CLE__APP.DBTIPO.DBPRC, CLE__APP.DBTIPO.DBAZI), CLE__APP.DBTIPO), dbConn)

      strSQL = "DELETE  FROM " & strNomtab & " WHERE instid = " & lCom
      oCldBase.Execute(strSQL, CType(IIf(bArcproc, CLE__APP.DBTIPO.DBPRC, CLE__APP.DBTIPO.DBAZI), CLE__APP.DBTIPO), dbConn)

      Return lCom
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ResetTblInstId(ByVal strNomtab As String, ByVal bArcproc As Boolean, ByVal instid As Integer) As Boolean
    Dim strSQL As String = ""
    Dim ds As New DataSet

    Try
      strSQL = "DELETE FROM " & strNomtab & " WHERE instid = " & instid
      oCldBase.Execute(strSQL, CType(IIf(bArcproc, CLE__APP.DBTIPO.DBPRC, CLE__APP.DBTIPO.DBAZI), CLE__APP.DBTIPO))

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function


  Public Overridable Function InvalidaLiqIVA(ByVal strDitta As String, ByVal strDatreg As String, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    'chiamato dai programmi che, in un qualche modo, possono rndere una liquidazione IVA precedentemente elaborata non corretta

    Dim strSQL As String = ""
    Dim dttTmp As DataTable
    Dim nAnno As Integer = 0
    Dim nMese As Integer = 0
    Dim nMeseInput As Integer = 0
    Dim strAtmentri As String = ""
    Try

      nAnno = NTSCDate(strDatreg).Year
      nMeseInput = NTSCDate(strDatreg).Month
      strAtmentri = "M"

      'NON TESTO PIU' SE ATTIVITA' MENSILE O TRIMESTRALE, DIVERSAMENTE SE HO SIA L'UNA CHE L'ALTRA E MODIFICA UNA REG. A LUGIO, INVALIDA SOLO LA TRIMESTRALE DI SETTEMPRE E NON LA MENSILE DI LUGLIO ED AGOSTO
      'leggo da tabatti il tipo di liquidazione usato dalla ditta:
      'questo per poter determinare la data di inizio liquidazione per ottenere la prima liquidazione
      'da cui partire per invalidare tutte le altre

      'strSQL = "SELECT tb_atmentri FROM tabatti " & _
      '         "WHERE codditt = " & CStrSQL(strDitta) & _
      '         " AND tb_anno = " & nAnno.ToString & _
      '         " AND tb_atmentri <> 'M' " & _
      '         " GROUP BY tb_atmentri ORDER BY tb_atmentri ASC"
      'dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      'If dttTmp.Rows.Count > 0 Then strAtmentri = dttTmp.Rows(0)!tb_atmentri.ToString()
      'dttTmp.Clear()

      'Select Case strAtmentri
      '  Case "M"
      '    nMese = nMeseInput       'il mese della liquidazione da invalidare è quello contenete la registrazione che mi ha chiamato
      '  Case "T"
      '    If nMeseInput < 4 Then
      '      nMese = 3
      '    ElseIf nMeseInput > 3 And nMeseInput < 7 Then
      '      nMese = 6
      '    ElseIf nMeseInput > 6 And nMeseInput < 10 Then
      '      nMese = 9
      '    Else
      '      nMese = 12
      '    End If
      '  Case "A"
      '    nMese = 14
      'End Select

      ''verifico se la liquidazione di partenza è già stata stampata in defintivo:
      ''se lo è già non faccio nulla, visto che sarà l'operatore a sapere di dover rifare il tutto
      ''(anche perchè anche se marco la liquidazione con non buona, comunque liqui parte dall'ultima in definitivo,
      ''che deve essere rielaborata a mano)
      'strSQL = "SELECT tb_prodef FROM tablipe " & _
      '         " WHERE codditt = " & CStrSQL(strDitta) & _
      '         " AND tb_lipecodatti <= 0 " & _
      '         " AND tb_lipeanno = " & nAnno & _
      '         " AND tb_lipemese = " & IIf(strAtmentri <> "M", nMese, nMeseInput).ToString & _
      '         " AND tb_prodef = 'P'"
      'dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      'If dttTmp.Rows.Count = 0 Then
      '  dttTmp.Clear()
      '  Return True
      'End If

      'dttTmp.Clear()

      'eseguo la query su attività 0 e -1 dal mese sopra calcolato in poi, e per tutti gli anni a seguire
      'potrei migliorare la query, ma per ora rendo tutte le liquidazioni non buone, indipendentemente dal fatto che
      'la registrazione abbia modificato una liquidazione mensile o trimestrale (in caso di ditte con attività con entrambe le liquidazioni)
      'non tocco l'acconto di dicembre
      strSQL = "UPDATE tablipe SET tb_darielab = 'S' " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_lipecodatti <= 0 " & _
               " AND tb_prodef = 'P' " & _
               " AND tb_lipemese <> 13 " & _
               " AND (tb_lipeanno > " & nAnno.ToString & " OR (tb_lipeanno = " & nAnno.ToString & " AND tb_lipemese >= " & nMeseInput.ToString & "))"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function EsisteBudget(ByVal strCodditt As String, ByVal strTipork As String, _
                                           ByVal lConto As Integer, ByVal nCodcena As Integer, _
                                           ByVal strCodcfam As String, ByVal lCommeca As Integer, _
                                           ByVal strSubcommeca As String, ByVal nEscomp As Integer, _
                                           ByVal bTestBudget As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bTestBudgetSottocommesse As Boolean = False
    Try
      If bTestBudget = False Then Return True ' non controllare mai
      If bTestBudget And strTipork = "C" Then Return True 'caso test solo su centri e linee

      Select Case strTipork
        Case "C" 'commessa
          bTestBudgetSottocommesse = CBool(GetSettingBus("OPZIONI", ".", ".", "TestBudgetSottocommesse", "-1", " ", "-1"))
          strSQL = "select bu_conto from budget where " _
              & " bu_tipork = '" & strTipork & "' and bu_escomp = " & "0" & " and " _
              & " bu_commeca = " & lCommeca.ToString & " and bu_subcommeca = '" & IIf(bTestBudgetSottocommesse = False, " ", bTestBudgetSottocommesse).ToString & "' and " _
              & " bu_codcena = " & "0" & " and bu_codcfam = '" & " " & "' and bu_conto = " & lConto.ToString
        Case "L" 'liea
          strSQL = "select bu_conto from budget where " _
              & " bu_tipork = '" & strTipork & "' and bu_escomp = " & nEscomp.ToString & " and " _
              & " bu_commeca = " & "0" & " and bu_subcommeca = '" & " " & "' and " _
              & " bu_codcena = " & "0" & " and bu_codcfam = '" & strCodcfam & "' and bu_conto = " & lConto.ToString
        Case "E" 'centro
          strSQL = "select bu_conto from budget where " _
              & " bu_tipork = '" & strTipork & "' and bu_escomp = " & nEscomp.ToString & " and " _
              & " bu_commeca = " & "0" & " and bu_subcommeca = '" & " " & "' and " _
              & " bu_codcena = " & nCodcena.ToString & " and bu_codcfam = '" & " " & "' and bu_conto = " & lConto.ToString
      End Select
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        Return False
      End If

      dttTmp.Clear()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RitornaTipoConto(ByVal lConto As Integer, ByVal strDitta As String) As String
    'ritorna se il conto passato è
    'ATTIVITA'(A),
    'PASSIVITA'(P),
    'COSTO(C),
    'RICAVO(R),
    'CONTO D'ORDINE(O),
    'CONTO DI RIEPILOGO(Z)
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strSezbil As String = ""
    Try

      strSQL = "SELECT tb_struttura, tb_sezbil, tb_darave, tb_tipomast, tb_sezbilm, an_cksegno " & _
          "FROM ((((TABPCON INNER JOIN TABGRUC ON tabpcon.tb_codpcon = tabgruc.tb_codpcon) INNER JOIN " & _
          "TABCLAS ON tabpcon.tb_codpcon = tabclas.tb_codpcon AND tabgruc.tb_codgruc = tabclas.tb_gruclas AND " & _
          "tabgruc.tb_codpcon = tabclas.tb_codpcon) INNER JOIN " & _
          "TABMAST ON tabpcon.tb_codpcon = tabmast.tb_codpcon AND tabclas.tb_codclas = tabmast.tb_clasmast AND " & _
          "tabclas.tb_codpcon = tabmast.tb_codpcon) INNER JOIN " & _
          "ANAGRA ON tabmast.tb_codmast = anagra.an_codmast AND tabmast.tb_codpcon = anagra.an_codpcon) INNER JOIN " & _
          "TABANAZ ON anagra.an_codpcon = tabanaz.tb_azcodpcon " & _
          "WHERE anagra.codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto.ToString
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count = 0 Then
        Throw New NTSException(oApp.Tr(Me, 128462517571264000, "Non è stato possibile ottenere il tipo di " & _
                  "appartenenza per il conto |" & lConto & "|. " & vbCrLf & "La seguente query non ha restituito nessun dato " & _
                  "(probabile non corretta impostazione del database)" & vbCrLf & "|" & strSQL & "|"))
        Return "A"
      End If

      If dttTmp.Rows(0)!tb_tipomast.ToString = "C" Then
        dttTmp.Clear()
        Return "C"
      End If

      If dttTmp.Rows(0)!tb_tipomast.ToString = "F" Then
        dttTmp.Clear()
        Return "F"
      End If

      If dttTmp.Rows(0)!tb_struttura.ToString = "B" And dttTmp.Rows(0)!tb_sezbilm.ToString <> "0" Then
        strSezbil = dttTmp.Rows(0)!tb_sezbilm.ToString
      Else
        strSezbil = dttTmp.Rows(0)!tb_sezbil.ToString
      End If

      Select Case strSezbil
        Case "1"
          If dttTmp.Rows(0)!an_cksegno.ToString = "1" Then
            dttTmp.Clear()
            Return "A"
          ElseIf dttTmp.Rows(0)!an_cksegno.ToString = "2" Then
            dttTmp.Clear()
            Return "Q"
          ElseIf dttTmp.Rows(0)!tb_darave.ToString = "D" Then
            dttTmp.Clear()
            Return "A"
          Else
            dttTmp.Clear()
            Return "Q"
          End If
        Case "2"
          If dttTmp.Rows(0)!an_cksegno.ToString = "1" Then
            dttTmp.Clear()
            Return "K"
          ElseIf dttTmp.Rows(0)!an_cksegno.ToString = "2" Then
            dttTmp.Clear()
            Return "R"
          ElseIf dttTmp.Rows(0)!tb_darave.ToString = "D" Then
            dttTmp.Clear()
            Return "K"
          Else
            dttTmp.Clear()
            Return "R"
          End If
        Case "3"
          dttTmp.Clear()
          Return "O"
        Case "4"
          dttTmp.Clear()
          Return "Z"
      End Select

      'se sono qui c'è stato un problema ...
      Throw New NTSException(oApp.Tr(Me, 127985658879531250, "Non è stato possibile ottenere il tipo di appartenenza per il conto |" & lConto & "|"))
      Return "A"

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
      Return ""
    End Try
  End Function

  Public Overridable Function DataInEsercizio(ByVal strDitta As String, ByVal dtData As Date, Optional ByVal nAnno As Integer = -1) As Boolean
    ' CONTROLLA SE UNA DATA E INCLUSA NELL'ANNO ESERCIZIO INDICATO
    '
    ' IN: dtData  -> Data da valutare
    '     nAnno    -> Anno esercizio
    '
    ' OU: True -> La data è esatta
    '     False -> La data è errata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      If nAnno = -1 Then
        strSQL = "SELECT tb_escomp FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        nAnno = NTSCInt(dttTmp.Rows(0)!tb_escomp)
        dttTmp.Clear()
      End If

      strSQL = "SELECT tb_dtineser, tb_dtfieser FROM tabesco WHERE codditt = " & CStrSQL(strDitta) & " AND tb_codesco = " & nAnno.ToString
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        Throw New NTSException(oApp.Tr(Me, 128244772619245574, "Attenzione! In anagrafica ditta non è stato trovato l'esercizio contabile numero |" & nAnno.ToString & "| per la ditta |" & strDitta & "|"))
        Return False
      Else
        If dtData >= NTSCDate(dttTmp.Rows(0)!tb_dtineser) And dtData <= NTSCDate(dttTmp.Rows(0)!tb_dtfieser) Then
          dttTmp.Clear()
          Return True
        End If
      End If

      Return False

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#Region "sincronizzazione ANAGEN/ANAGRA/DESTGEN/TABANAZ/ ..."

  Public Overridable Function AggiornaAnagen(ByVal strDitta As String, ByVal strOrigine As String, _
                                            ByVal lCodanag As Integer, ByVal lConto As Integer, _
                                            Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bAggLeads As Boolean = False
    Try

      '-----------------------------------------------
      'PARTENDO DA anagen /a destgen già aggiornati,
      'aggiornoa/sincronizza ANAGRA / DESTIV, TABANAZ / ANAZUL, LEADS
      strSQL = "SELECT TOP 1 le_conto FROM leads"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then bAggLeads = True
      dttTmp.Clear()
      Select Case strOrigine
        Case "ANAGEN"
          If Not AggiornaAnagraDaAnagen(strOrigine, lCodanag, "", 0, dbConn) Then Return False
          If Not AggiornaTabanazDaAnagen(strOrigine, lCodanag, "", dbConn) Then Return False
          If bAggLeads Then
            If Not AggiornaLeadsDaAnagen(lCodanag, dbConn) Then Return False
          End If
        Case "ANAGRA"
          If Not AggiornaAnagraDaAnagen(strOrigine, lCodanag, strDitta, lConto, dbConn) Then Return False
          If Not AggiornaTabanazDaAnagen(strOrigine, lCodanag, "", dbConn) Then Return False
          If bAggLeads Then
            If Not AggiornaLeadsDaAnagen(lCodanag, dbConn) Then Return False
          End If
        Case "TABANAZ"
          If Not AggiornaAnagraDaAnagen(strOrigine, lCodanag, "", 0, dbConn) Then Return False
          If Not AggiornaTabanazDaAnagen(strOrigine, lCodanag, strDitta, dbConn) Then Return False
          If bAggLeads Then
            If Not AggiornaLeadsDaAnagen(lCodanag, dbConn) Then Return False
          End If
      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaAnagraDaAnagen(ByVal strOrigine As String, ByVal lCodanag As Integer, ByVal strDitta As String, _
                                            ByVal lConto As Integer, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim str1 As String = ""

    Try

      If strOrigine = "ANAGRA" Then
        str1 = " AND (anagra.codditt <> " & CStrSQL(strDitta) & " Or anagra.an_conto <> " & lConto.ToString & ") "
      Else
        str1 = ""
      End If

      '----------------------------
      'UPDATE su ANAGRA da ANAGEN
      strSQL = " UPDATE anagra " & _
               " SET anagra.an_descr1 = anagen.ag_descr1, anagra.an_descr2 = anagen.ag_descr2, anagra.an_indir = anagen.ag_indir, anagra.an_cap = anagen.ag_cap, " & _
               " anagra.an_citta = anagen.ag_citta, anagra.an_prov = anagen.ag_prov, anagra.an_stato = anagen.ag_stato, anagra.an_codfis = anagen.ag_codfis, " & _
               " anagra.an_pariva = anagen.ag_pariva, anagra.an_telef = anagen.ag_telef, anagra.an_faxtlx = anagen.ag_faxtlx, anagra.an_valuta = anagen.ag_valuta, " & _
               " anagra.an_codling = anagen.ag_codling, anagra.an_ultagg = " & CDataOraSQL(DateTime.Now) & ", anagra.an_destin = anagen.ag_destin, anagra.an_destpag = anagen.ag_destpag, " & _
               " anagra.an_note = anagen.ag_note, anagra.an_note2 = anagen.ag_note2, anagra.an_email = anagen.ag_email, anagra.an_website = anagen.ag_website, " & _
               " anagra.an_usaem = anagen.ag_usaem, anagra.an_opnome = " & CStrSQL(oApp.User.Nome) & ", anagra.an_webuid = anagen.ag_webuid, anagra.an_webpwd = anagen.ag_webpwd, " & _
               " anagra.an_sesso = anagen.ag_sesso, anagra.an_datnasc = anagen.ag_datnasc, anagra.an_citnasc = anagen.ag_citnasc, anagra.an_pronasc = anagen.ag_pronasc, " & _
               " anagra.an_stanasc = anagen.ag_stanasc, anagra.an_codfisest = anagen.ag_codfisest, anagra.an_cell = anagen.ag_cell, anagra.an_titolo = anagen.ag_titolo, " & _
               " anagra.an_persfg = anagen.ag_persfg, anagra.an_profes = anagen.ag_profes, anagra.an_condom = anagen.ag_condom, anagra.an_tpsogiva = anagen.ag_tpsogiva, " & _
               " anagra.an_codcomu = anagen.ag_codcomu, anagra.an_destcorr = anagen.ag_destcorr, anagra.an_destsedel = anagen.ag_destsedel, anagra.an_destdomf = anagen.ag_destdomf, " & _
               " anagra.an_destresan = anagen.ag_destresan, anagra.an_siglaric = anagen.ag_siglaric, anagra.an_cognome = anagen.ag_cognome, anagra.an_nome = anagen.ag_nome, " & _
               " anagra.an_codcomn = anagen.ag_codcomn, anagra.an_nazion1 = anagen.ag_nazion1, anagra.an_nazion2 = anagen.ag_nazion2, " & _
               " anagra.an_statofed = anagen.ag_statofed, anagra.an_soggresi = anagen.ag_soggresi, anagra.an_omocodice = anagen.ag_omocodice, " & _
               " anagra.an_estcodiso = anagen.ag_estcodiso, anagra.an_estpariva = anagen.ag_estpariva, anagra.an_codrtac = anagen.ag_codrtac, " & _
               " anagra.an_latitud = anagen.ag_latitud, anagra.an_longitud = anagen.ag_longitud " & _
               " FROM anagra, anagen " & _
               " WHERE anagra.an_codanag = anagen.ag_codanag And anagra.an_codanag = " & lCodanag.ToString & str1
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------
      'sistemo DESTDIV
      strSQL = "UPDATE destdiv SET " & _
               " destdiv.dd_nomdest = destgen.dd_nomdest, destdiv.dd_nomdest2 = destgen.dd_nomdest2, " & _
               " destdiv.dd_inddest = destgen.dd_inddest, destdiv.dd_capdest = destgen.dd_capdest, " & _
               " destdiv.dd_locdest = destgen.dd_locdest, destdiv.dd_prodest = destgen.dd_prodest, " & _
               " destdiv.dd_turno = destgen.dd_turno, destdiv.dd_telef = destgen.dd_telef, " & _
               " destdiv.dd_codfis = destgen.dd_codfis, destdiv.dd_pariva = destgen.dd_pariva, " & _
               " destdiv.dd_faxtlx = destgen.dd_faxtlx, destdiv.dd_email = destgen.dd_email, " & _
               " destdiv.dd_usaem = destgen.dd_usaem, destdiv.dd_stato = destgen.dd_stato, " & _
               " destdiv.dd_codcomu = destgen.dd_codcomu, destdiv.dd_codfisest = destgen.dd_codfisest, " & _
               " destdiv.dd_statofed = destgen.dd_statofed, " & _
               " destdiv.dd_latitud = destgen.dd_latitud, destdiv.dd_longitud = destgen.dd_longitud " & _
               " FROM destdiv, anagra, destgen " & _
               " WHERE destdiv.codditt = anagra.codditt " & _
               " AND destdiv.dd_conto = anagra.an_conto " & _
               " AND anagra.an_codanag = destgen.dd_codanag " & _
               " AND destdiv.dd_coddest = destgen.dd_coddest " & _
               " AND anagra.an_codanag = " & lCodanag
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = " INSERT INTO destdiv (codditt, dd_conto, dd_coddest, dd_nomdest, dd_inddest, " & _
               " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codzona, " & _
               " dd_codfis, dd_pariva, dd_faxtlx, dd_agente, dd_agente2, dd_email, " & _
               " dd_usaem, dd_vett, dd_vett2, dd_nomdest2, dd_stato, dd_codcomu, " & _
               " dd_codfisest, dd_statofed, dd_latitud, dd_longitud)" & _
               " SELECT DISTINCT codditt, an_conto, dd_coddest, dd_nomdest, dd_inddest, dd_capdest, " & _
               " dd_locdest, dd_prodest, dd_turno, dd_telef, 0, dd_codfis, dd_pariva, dd_faxtlx, 0, 0, " & _
               " dd_email, dd_usaem, 0, 0, dd_nomdest2, dd_stato, dd_codcomu, dd_codfisest, dd_statofed, " & _
               " dd_latitud, dd_longitud " & _
               " FROM destgen inner join anagra on destgen.dd_codanag = anagra.an_codanag " & _
               " WHERE dd_codanag = " & lCodanag.ToString & _
               " AND dd_coddest NOT IN (SELECT dd_coddest FROM destdiv WHERE codditt = anagra.codditt AND dd_conto = an_conto AND destdiv.dd_coddest = destgen.dd_coddest)"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaTabanazDaAnagen(ByVal strOrigine As String, ByVal lCodanag As Integer, _
                                            ByVal strDitta As String, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim str1 As String = ""
    Try

      If strOrigine = "TABANAZ" Then
        str1 = " And tabanaz.codditt <> " & CStrSQL(strDitta)
      Else
        str1 = ""
      End If

      strSQL = " UPDATE tabanaz " & _
               " SET tabanaz.tb_azrags1 = anagen.ag_descr1, tabanaz.tb_azrags2 = anagen.ag_descr2, tabanaz.tb_azindir = anagen.ag_indir, tabanaz.tb_azcap = anagen.ag_cap, " & _
               " tabanaz.tb_azcitta = anagen.ag_citta, tabanaz.tb_azprov = anagen.ag_prov, tabanaz.tb_azstato = anagen.ag_stato, tabanaz.tb_azcodf = anagen.ag_codfis, " & _
               " tabanaz.tb_azpiva = anagen.ag_pariva, tabanaz.tb_aztelef = anagen.ag_telef, tabanaz.tb_azfaxtlx = anagen.ag_faxtlx, tabanaz.tb_azultagg = " & CDataOraSQL(DateTime.Now) & ", " & _
               " tabanaz.tb_azemail = anagen.ag_email, tabanaz.tb_azwebsite = anagen.ag_website, " & _
               " tabanaz.tb_azusaem = anagen.ag_usaem, tabanaz.tb_azopnome = " & CStrSQL(oApp.User.Nome) & ", tabanaz.tb_azwebuid = anagen.ag_webuid, tabanaz.tb_azwebpwd = anagen.ag_webpwd, " & _
               " tabanaz.tb_sesso = anagen.ag_sesso, tabanaz.tb_datnasc = anagen.ag_datnasc, tabanaz.tb_locnasc = anagen.ag_citnasc, tabanaz.tb_pronasc = anagen.ag_pronasc, " & _
               " tabanaz.tb_azstanasc = anagen.ag_stanasc, tabanaz.tb_azcodfisest = anagen.ag_codfisest, tabanaz.tb_azcell = anagen.ag_cell, tabanaz.tb_aztitolo = anagen.ag_titolo, " & _
               " tabanaz.tb_azpersfg = anagen.ag_persfg, tabanaz.tb_azprofes = anagen.ag_profes, tabanaz.tb_azcondom = anagen.ag_condom, " & _
               " tabanaz.tb_azcodcomu = anagen.ag_codcomu, tabanaz.tb_uldestcorr = anagen.ag_destcorr, tabanaz.tb_uldestsedel = anagen.ag_destsedel, tabanaz.tb_uldestdomf = anagen.ag_destdomf, " & _
               " tabanaz.tb_uldestresan = anagen.ag_destresan, tabanaz.tb_azsiglaric = anagen.ag_siglaric, tabanaz.tb_azcognome = anagen.ag_cognome, tabanaz.tb_aznome = anagen.ag_nome, " & _
               " tabanaz.tb_azcodcomn = anagen.ag_codcomn, tabanaz.tb_aznazion1 = anagen.ag_nazion1, tabanaz.tb_aznazion2 = anagen.ag_nazion2, " & _
               " tabanaz.tb_azstatofed = anagen.ag_statofed, tabanaz.tb_azsoggresi = anagen.ag_soggresi, tabanaz.tb_azomocodice = anagen.ag_omocodice, " & _
               " tabanaz.tb_azestcodiso = anagen.ag_estcodiso, tabanaz.tb_azestpariva = anagen.ag_estpariva, tabanaz.tb_azcodrtac = anagen.ag_codrtac, " & _
               " tabanaz.tb_latitud = anagen.ag_latitud, tabanaz.tb_longitud = anagen.ag_longitud " & _
               " FROM tabanaz, anagen " & _
               " WHERE tabanaz.tb_azcodanag = anagen.ag_codanag " & _
               " And tabanaz.tb_azcodanag = " & lCodanag.ToString & str1
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------
      'sistemo ANAZUL
      strSQL = "UPDATE anazul SET " & _
               " anazul.ul_nomdest = destgen.dd_nomdest, anazul.ul_nomdest2 = destgen.dd_nomdest2, " & _
               " anazul.ul_inddest = destgen.dd_inddest, anazul.ul_capdest = destgen.dd_capdest, " & _
               " anazul.ul_locdest = destgen.dd_locdest, anazul.ul_prodest = destgen.dd_prodest, " & _
               " anazul.ul_turno = destgen.dd_turno, anazul.ul_telef = destgen.dd_telef, " & _
               " anazul.ul_codfis = destgen.dd_codfis, anazul.ul_pariva = destgen.dd_pariva, " & _
               " anazul.ul_faxtlx = destgen.dd_faxtlx, anazul.ul_email = destgen.dd_email, " & _
               " anazul.ul_usaem = destgen.dd_usaem, anazul.ul_stato = destgen.dd_stato, " & _
               " anazul.ul_codcomu = destgen.dd_codcomu, anazul.ul_codfisest = destgen.dd_codfisest, " & _
               " anazul.ul_statofed = destgen.dd_statofed, " & _
               " anazul.ul_latitud = destgen.dd_latitud, anazul.ul_longitud = destgen.dd_longitud " & _
               " FROM anazul, tabanaz, destgen " & _
               " WHERE anazul.codditt = tabanaz.codditt " & _
               " AND anazul.ul_coddest = destgen.dd_coddest " & _
               " AND tabanaz.tb_azcodanag = destgen.dd_codanag " & _
               " And tabanaz.tb_azcodanag = " & lCodanag
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = " INSERT INTO anazul (codditt, ul_coddest, ul_nomdest, ul_nomdest2, ul_inddest, " & _
               " ul_capdest, ul_locdest, ul_prodest, ul_turno, ul_telef, ul_codfis, ul_pariva, " & _
               " ul_faxtlx, ul_email, ul_usaem, ul_stato, ul_codcomu, ul_codfisest, ul_statofed, " & _
               " ul_latitud, ul_longitud)" & _
               " SELECT DISTINCT codditt, dd_coddest, dd_nomdest, dd_nomdest2, dd_inddest, " & _
               " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis, dd_pariva, " & _
               " dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest, dd_statofed, " & _
               " dd_latitud, dd_longitud " & _
               " FROM destgen INNER JOIN tabanaz on destgen.dd_codanag = tabanaz.tb_azcodanag " & _
               " WHERE dd_codanag = " & lCodanag.ToString & _
               " AND dd_coddest NOT IN (SELECT ul_coddest FROM anazul " & _
               " WHERE anazul.codditt = tabanaz.codditt AND anazul.ul_coddest = destgen.dd_coddest)"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaLeadsDaAnagen(ByVal lCodanag As Integer, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim bGestAnaext As Boolean = False
    Dim i As Integer = 0
    Dim lProg As Integer = 0
    Try
      '----------------------
      'prima cerco le destinazioni diverse non presenti nei leads e le inserisco
      strSQL = "SELECT destdiv.codditt, an_codanag, dd_conto, dd_coddest, an_descr1, le_opinc, le_status " & _
               " FROM (destdiv INNER JOIN anagra ON destdiv.codditt = anagra.codditt AND destdiv.dd_conto = anagra.an_conto) " & _
               " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
               " WHERE an_tipo <> 'S' AND dd_codlead = 0 AND le_coddest = 0 AND an_codanag = " & lCodanag.ToString
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'verifico se gestisco anaext per i leads
      If dttTmp.Rows.Count > 0 Then
        strSQL = "SELECT TOP 1 ax_tipork FROM anaext WHERE ax_tipork = 'L'"
        dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp1.Rows.Count > 0 Then bGestAnaext = True
        dttTmp1.Clear()
      End If

      For i = 0 To dttTmp.Rows.Count - 1
        lProg = LegNuma(NTSCStr(dttTmp.Rows(i)!codditt), "C8", " ", 0, True, dbConn)
        lProg = AggNuma(NTSCStr(dttTmp.Rows(i)!codditt), "C8", " ", 0, lProg, True, True, "", dbConn)

        strSQL = "UPDATE destdiv SET dd_codlead = " & lProg.ToString & _
                 " WHERE codditt = " & CStrSQL(dttTmp.Rows(i)!codditt) & _
                 " AND dd_conto = " & NTSCInt(dttTmp.Rows(i)!dd_conto).ToString & _
                 " AND dd_coddest = " & NTSCInt(dttTmp.Rows(i)!dd_coddest).ToString
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "SELECT opcr_codlead FROM acclead" & _
                  " WHERE codditt = " & CStrSQL(dttTmp.Rows(i)!codditt) & _
                  " AND opcr_codlead = " & lProg
        If oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn).Rows.Count = 0 Then
          strSQL = "INSERT INTO acclead (codditt, opcr_codlead, opcr_opnome, opcr_crmmod, opcr_crmvis)" & _
            " VALUES (" & CStrSQL(dttTmp.Rows(i)!codditt) & ", " & lProg & ", " & _
            CStrSQL(GetSettingBus("BS--CLIE", "OPZIONI", ".", "UtentePredefinitoCrm", "Admin", " ", "Admin")) & "," & _
            " 'S', 'S')"
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strSQL = "INSERT INTO leads (codditt, le_codlead, le_descr1, le_conto, le_coddest, le_opinc, le_status) " & _
                 " VALUES (" & CStrSQL(dttTmp.Rows(i)!codditt.ToString) & ", " & _
                 lProg.ToString & ", " & _
                 CStrSQL(dttTmp.Rows(i)!an_descr1.ToString) & ", " & _
                 NTSCInt(dttTmp.Rows(i)!dd_conto).ToString & ", " & _
                 NTSCInt(dttTmp.Rows(i)!dd_coddest).ToString & ", " & _
                 NTSCInt(dttTmp.Rows(i)!le_opinc).ToString & ", " & _
                 CStrSQL(NTSCStr(dttTmp.Rows(i)!le_status)) & ")"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If bGestAnaext Then
          strSQL = "INSERT INTO anaext (codditt, ax_conto, ax_coddest, ax_codlead, ax_tipork, ax_codart, ax_matric, ax_ultagg, ax_opnome) " & _
                   " VALUES (" & CStrSQL(dttTmp.Rows(i)!codditt.ToString) & ", 0, 0, " & _
                   lProg.ToString & ", 'L', ' ', ' ', getdate(), " & CStrSQL(oApp.User.Nome) & ") "
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Next
      dttTmp.Clear()

      '----------------------
      'ora aggiorno tutto
      strSQL = "UPDATE leads" & _
                " SET le_descr1 = an_descr1," & _
                " le_descr2 = an_descr2," & _
                " le_indir = an_indir," & _
                " le_cap = an_cap," & _
                " le_citta = an_citta," & _
                " le_prov = an_prov," & _
                " le_codfis = an_codfis," & _
                " le_pariva = an_pariva," & _
                " le_telef = an_telef," & _
                " le_faxtlx = an_faxtlx," & _
                " le_ultagg = " & CDataOraSQL(Now) & "," & _
                " le_note = an_note," & _
                " le_stato = an_stato," & _
                " le_note2 = an_note2," & _
                " le_email = an_email," & _
                " le_website = an_website," & _
                " le_usaem = an_usaem," & _
                " le_codling = an_codling," & _
                " le_opnome = " & CStrSQL(oApp.User.Nome) & "," & _
                " le_webuid = an_webuid," & _
                " le_webpwd = an_webpwd," & _
                " le_siglaric = an_siglaric," & _
                " le_cell = an_cell," & _
                " le_titolo = an_titolo," & _
                " le_latitud = an_latitud," & _
                " le_longitud = an_longitud," & _
                " le_servdtua = " & CDataOraSQL(DataGMT) & _
                " FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                " WHERE anagra.an_codanag = " & lCodanag.ToString & _
                " AND anagra.an_tipo = 'C'" & _
                " AND leads.le_coddest = 0" & _
                " AND anagra.an_conto <> 0"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "UPDATE leads" & _
              " SET le_descr1 = case when dd_nomdest is null Then ' ' Else dd_nomdest End," & _
              " le_descr2 = SUBSTRING(dd_nomdest2, 1, 30)," & _
              " le_indir = dd_inddest," & _
              " le_cap = dd_capdest," & _
              " le_citta = dd_locdest," & _
              " le_prov = dd_prodest," & _
              " le_codfis = dd_codfis," & _
              " le_pariva = dd_pariva," & _
              " le_faxtlx = dd_faxtlx," & _
              " le_ultagg = " & CDataOraSQL(Now) & "," & _
              " le_stato = dd_stato," & _
              " le_email = dd_email," & _
              " le_usaem = dd_usaem," & _
              " le_latitud = dd_latitud," & _
              " le_longitud = dd_longitud," & _
              " le_opnome = " & CStrSQL(oApp.User.Nome) & "," & _
              " le_servdtua = " & CDataOraSQL(DataGMT) & _
              " FROM (destdiv INNER JOIN leads ON destdiv.codditt = leads.codditt AND destdiv.dd_conto = leads.le_conto AND destdiv.dd_coddest = leads.le_coddest) " & _
              " INNER JOIN anagra ON anagra.codditt = destdiv.codditt AND anagra.an_conto = destdiv.dd_conto " & _
              " WHERE anagra.an_codanag = " & lCodanag.ToString & _
              " AND anagra.an_tipo = 'C'" & _
              " AND anagra.an_conto <> 0"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function DataGMT() As Date
    '-----------------------------------------------------------------------------------------
    '--- Determina la Data/Ora di Greenwich (GMT) da passare alla data aggiornamento
    '-----------------------------------------------------------------------------------------
    Return My.Computer.Clock.GmtTime
    '-----------------------------------------------------------------------------------------
  End Function

#End Region

#Region "Conto funzionamento"
  Public Overridable Function RiempiTtanconf(ByVal INSTID As Integer, ByVal strFiltrodittegruppo As String) As Boolean
    'Riempio la tabella temporanea per gestire il conto di funzionamento
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      strFiltrodittegruppo = CorreggiFiltroDitteGruppoPerQuery("", strFiltrodittegruppo)

      ResetTblInstId("TTANCONF", False, INSTID)

      '-------------------------------
      'prima inserisco i valori standard
      strSQL = "INSERT INTO ttanconf (instid, codditt, an_codpcon, an_pdcbase, an_conto, " & _
               "an_funzion, an_voceirap, an_varirap, an_pervari, an_indirap, an_indiidd, " & _
               "an_indiiddsit, an_azcom, an_ricmimp, an_ricmpro, an_stseimp, an_stsepro) " & _
               "SELECT " & INSTID.ToString & ", codditt, an_codpcon, "
      strSQL += "CASE WHEN tb_pcprof = 'S' THEN 'Standard-PR' ELSE 'Standard-AZ' END "
      strSQL += ", an_conto, " & _
             "an_funzion, an_voceirap, an_varirap, an_pervari, an_indirap, an_indiidd, " & _
             "an_indiiddsit, an_azcom, an_ricmimp, an_ricmpro, an_stseimp, an_stsepro " & _
             "FROM anagra INNER JOIN tabpcon ON anagra.an_codpcon = tabpcon.tb_codpcon " & _
             "WHERE codditt IN ( " & strFiltrodittegruppo & " ) "
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '-------------------------------
      'adesso aggiorno i valori dal conto di funzionamento
      strSQL = "UPDATE ttanconf SET " & _
              "an_voceirap = CASE WHEN SUBSTRING(an_opzgest, 2, 1) = 'S' THEN pc_voceirap ELSE ttanconf.an_voceirap END, " & _
               "an_varirap = CASE WHEN SUBSTRING(an_opzgest, 2, 1) = 'S' THEN pc_varirap ELSE ttanconf.an_varirap END, " & _
               "an_pervari = CASE WHEN SUBSTRING(an_opzgest, 2, 1) = 'S' THEN pc_pervari ELSE ttanconf.an_pervari END," & _
               "an_indirap = CASE WHEN SUBSTRING(an_opzgest, 2, 1) = 'S' THEN pc_indirap ELSE ttanconf.an_indirap END," & _
               "an_indiidd = CASE WHEN SUBSTRING(an_opzgest, 3, 1) = 'S' THEN pc_indiidd ELSE ttanconf.an_indiidd END," & _
               "an_indiiddsit = CASE WHEN SUBSTRING(an_opzgest, 3, 1) = 'S' THEN pc_indiiddsit ELSE ttanconf.an_indiiddsit END," & _
               "an_azcom = CASE WHEN SUBSTRING(an_opzgest, 6, 1) = 'S' THEN pc_azcom ELSE ttanconf.an_azcom END," & _
               "an_ricmimp = CASE WHEN SUBSTRING(an_opzgest, 4, 1) = 'S' THEN pc_ricmimp ELSE ttanconf.an_ricmimp END, " & _
               "an_ricmpro = CASE WHEN SUBSTRING(an_opzgest, 4, 1) = 'S' THEN pc_ricmpro ELSE ttanconf.an_ricmpro END," & _
               "an_stseimp = CASE WHEN SUBSTRING(an_opzgest, 5, 1) = 'S' THEN pc_stseimp ELSE ttanconf.an_stseimp END," & _
               "an_stsepro = CASE WHEN SUBSTRING(an_opzgest, 5, 1) = 'S'THEN pc_stsepro ELSE ttanconf.an_stsepro END " & _
             "FROM (ttanconf INNER JOIN anagpc ON " & _
             "ttanconf.an_funzion = anagpc.pc_conto AND ttanconf.an_pdcbase = anagpc.pc_codpcon) " & _
             "INNER JOIN anagra ON ttanconf.codditt = anagra.codditt AND ttanconf.an_conto = anagra.an_conto " & _
             "WHERE ttanconf.codditt IN ( " & strFiltrodittegruppo & " ) And " & _
             "ttanconf.an_funzion <> 0 And " & _
             "instid = " & INSTID.ToString
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CorreggiFiltroDitteGruppoPerQuery(ByVal strDitta As String, ByVal strFiltroDitteGruppo As String) As String
    Dim strT() As String
    Dim i As Integer
    Try
      If strFiltroDitteGruppo = "" Then strFiltroDitteGruppo = strDitta

      '----------------------------------------
      'devo mettere i nomi delle ditte tra apici
      strT = strFiltroDitteGruppo.Replace("'", "").Split(CType(",", Char))
      strFiltroDitteGruppo = ""
      For i = 0 To strT.Length - 1
        strFiltroDitteGruppo += "'" & strT(i).Trim & "', "
      Next
      If strFiltroDitteGruppo.Length > 0 Then strFiltroDitteGruppo = strFiltroDitteGruppo.Substring(0, strFiltroDitteGruppo.Length - 2)
      Return strFiltroDitteGruppo
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

#End Region


  Public Overridable Function IsAnagDeletable(ByVal strDitta As String, ByVal lConto As Integer, _
                                              ByRef strMsgOut As String) As Boolean
    'im vb6 IsAnagDeletableDitt
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT top 1 sc_conto FROM scaden WHERE sc_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 131038044146479537, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio Scadenze ed effetti (attivi e passivi)")
        Return False
      End If

      strSQL = "SELECT top 1 pn_conto FROM prinot WHERE pn_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 131038044208416660, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio Prima nota ")
        Return False
      End If

      strSQL = "SELECT top 1 km_conto FROM keymag WHERE km_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310606546762000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio di Magazzino ")
        Return False
      End If

      strSQL = "SELECT top 1 ko_conto FROM keyord WHERE ko_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 131038044330674839, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio degli Ordini ")
        Return False
      End If

      strSQL = "SELECT top 1 km_conto FROM keyprb WHERE km_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310606917262000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio delle Note di prelievo ")
        Return False
      End If

      strSQL = "SELECT top 1 co_conto FROM commess WHERE co_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310607474650000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio delle Commesse ")
        Return False
      End If

      strSQL = "SELECT top 1 sv_conto FROM statven WHERE sv_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310608013006000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio delle Statistiche ")
        Return False
      End If

      strSQL = "SELECT top 1 to_conto FROM testrdo WHERE to_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310608295210000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio delle RDA/RDO ")
        Return False
      End If

      strSQL = "SELECT top 1 ol_conto FROM ordlist WHERE ol_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310608604402000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio delle Proposte d'ordine ")
        Return False
      End If

      strSQL = "SELECT top 1 bvd_conto FROM budvend WHERE bvd_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310608811258000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio dei Budget vendite ")
        Return False
      End If

      strSQL = "SELECT top 1 ik_conto FROM intrac WHERE ik_conto = " & lConto & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128310609367086000, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già utilizzato nell'archivio Intrastat ")
        Return False
      End If

      strSQL = "SELECT TOP 1 op_conto FROM nnchiam WHERE codditt = " & CStrSQL(strDitta) & " AND op_conto = " & lConto.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 129585856823271485, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già presente in 'Gestione Chiamate'.")
        Return False
      End If

      strSQL = "SELECT TOP 1 td_conto FROM testoff " & _
               " INNER JOIN leads ON leads.codditt = testoff.codditt AND td_codlead = le_codlead " & _
               "  WHERE testoff.codditt = " & CStrSQL(strDitta) & _
               "   AND " & lConto & " IN (td_conto, le_conto)"
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 129585857131826172, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già presente in 'Gestione Offerte'.")
        Return False
      End If

      strSQL = "SELECT TOP 1 rl_conto FROM nnmatric WHERE codditt = " & CStrSQL(strDitta) & " AND rl_conto = " & lConto.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 129860375806665350, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già presente in una matricola del Customer Service.")
        Return False
      End If

      strSQL = "SELECT TOP 1 nnc_conto FROM nncontra WHERE codditt = " & CStrSQL(strDitta) & " AND nnc_conto = " & lConto.ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 129860375794793466, "Il cliente/fornitore/sottoconto con codice |" & lConto.ToString & "| non puo' essere cancellato in quanto già presente in un contratto del Customer Service.")
        Return False
      End If

      'GPV Disconnesso: Se esistono dei negozi disconnessi il programma non consente di cancellare un conto perchè questo potrebbe essere già stato sincronizzato!
      strSQL = "SELECT TOP 1 codditt FROM reoffconf WHERE codditt = " & cstrsql(StrDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 130710780710527779, "Non è possibile cancellare clienti/fornitori/sottoconti se è stato configurato il modulo GPV Disconnesso.")
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaLeadDaConto(ByVal strDitta As String, ByVal strTipoconto As String, _
                                             ByVal lConto As Integer, ByVal lCoddest1 As Integer, _
                                             ByVal lModuliDittaDitt As Integer, _
                                             ByVal lModuliExtDittaDitt As Integer) As Integer

    Try
      Return CercaLeadDaConto(strDitta, strTipoconto, lConto, lCoddest1, lModuliDittaDitt, lModuliExtDittaDitt, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaLeadDaConto(ByVal strDitta As String, ByVal strTipoconto As String, _
                                               ByVal lConto As Integer, ByVal lCoddest1 As Integer, _
                                               ByVal lModuliDittaDitt As Integer, _
                                               ByVal lModuliExtDittaDitt As Integer, _
                                               ByVal lModuliSupDittaDitt As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoconto, lConto, lCoddest1, lModuliDittaDitt, _
                                             lModuliExtDittaDitt, lModuliSupDittaDitt})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '----------------

      CercaLeadDaConto = 0

      If CBool((lModuliExtDittaDitt And bsModExtCRM) Or (lModuliDittaDitt And bsModAS)) Or CBool((lModuliSupDittaDitt And bsModSupWCR)) Then
        'ho il modulo: farò la query
      Else
        Return 0
      End If

      strSQL = "SELECT TOP 1 le_codlead FROM leads" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND le_conto = " & lConto & _
                " AND le_coddest = " & lCoddest1
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        CercaLeadDaConto = NTSCInt(dttTmp.Rows(0)!le_codlead)
      End If
      dttTmp.Clear()

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsArtiDeletable(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByRef strMsgOut As String) As Boolean
    'im vb6 IsArtiDeletableDitt
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT top 1 md_codfigli FROM movdis WHERE " & "md_codfigli = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847484023839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto già utilizzato nell'archivio distinta base")
        Return False
      End If

      strSQL = "SELECT top 1 mo_codart FROM movoff WHERE " & "mo_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 130680540850803900, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio offerte")
        Return False
      End If

      strSQL = "SELECT top 1 mo_codart FROM movord WHERE " & "mo_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847573567839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio ordini/impegni/preventivi")
        Return False
      End If

      strSQL = "SELECT top 1 mm_codart FROM movmag WHERE " & "mm_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847609135839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio documenti di magazzino")
        Return False
      End If

      strSQL = "SELECT top 1 mm_codart FROM movprb WHERE " & "mm_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847623175839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio documenti di scarico/carico dimagazzino")
        Return False
      End If

      strSQL = "SELECT top 1 ol_codart FROM ordlist WHERE " & "ol_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847641271839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio proposte d'ordine")
        Return False
      End If

      strSQL = "SELECT top 1 lp_codart FROM listprod WHERE " & "lp_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847656091839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio liste di produzione")
        Return False
      End If

      strSQL = "SELECT top 1 tsk_codart FROM tasks WHERE " & "tsk_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847673875839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio Tasks delle commesse (gest.progetti)")
        Return False
      End If

      strSQL = "SELECT top 1 bud_codart FROM budgetd WHERE " & "bud_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847686667839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio Budget dettagli")
        Return False
      End If

      strSQL = "SELECT top 1 bvd_codart FROM budvend WHERE " & "bvd_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847703359839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio Budget vendite")
        Return False
      End If

      strSQL = "SELECT top 1 sv_codart FROM statven WHERE " & "sv_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847720987839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio Statistiche vendite/acquisti")
        Return False
      End If

      strSQL = "SELECT top 1 nc_codart FROM rappnc WHERE " & "nc_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847737835839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto gia' utilizzato nell'archivio Rapporti di Non Conformità (Q)")
        Return False
      End If

      strSQL = "SELECT top 1 adx_codart FROM artdefx WHERE " & "adx_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847750315839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto presente nei 'Progressivi definitivi articoli'.")
        Return False
      End If

      strSQL = "SELECT top 1 rl_codart FROM nnmatrics WHERE " & "rl_codart = " & CStrSQL(strCodart) & " and codditt = " & CStrSQL(strDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128734847768411839, "L'articolo |" & strCodart & "| non puo' essere cancellato in quanto presente in 'Anagrafica matricole (parco)'.")
        Return False
      End If

      'GPV Disconnesso: Se esistono dei negozi disconnessi il programma non consente di cancellare un conto perchè questo potrebbe essere già stato sincronizzato!
      strSQL = "SELECT TOP 1 codditt FROM reoffconf WHERE codditt = " & cstrsql(StrDitta)
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 131038044387189275, "Non è possibile cancellare gli articoli se è stato configurato il modulo GPV Disconnesso.")
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsCommecaDeletable(ByVal strDitta As String, ByVal lCommeca As Integer, _
                                            ByRef strMsgOut As String) As Boolean
    'im vb6 IsCommecaDeletableDitt
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      '-----------------------------------------------------------------------------------------
      '--- MOVMAG
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mm_commeca FROM movmag WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556866192656250, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Movimenti di Magazzino.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- MOVPRB
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mm_commeca FROM movprb WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556867670312500, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Note di Prelievo.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- MOVORD
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mo_commeca FROM movord WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556869219218750, "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Ordini/Impegni/Preventivi.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- PRIANA
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 pa_commeca FROM priana WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pa_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556869999375000, "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Movimenti di Prina Nota di C.A.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- LOTCDEF
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ld_commeca FROM lotcdef WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ld_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556870930781250, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Progressivi Definitivi per articolo/magazzino/lotto/commessa.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- LISTPROD
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lp_commeca FROM listprod WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lp_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556871674687500, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Liste di Produzione.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- ORDLIST
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ol_commeca FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ol_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556872250781250, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Proposte d'Ordine.")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- TASKS
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tsk_commeca FROM tasks WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556873166093750, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Attività Commesse (TASKS)")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- PREVENT
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 pre_commeca FROM prevent WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pre_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556873720781250, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Preventivi Commesse (PREVENT).")
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- COMOP
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 cop_commeca FROM comop WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cop_commeca = " & lCommeca
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128556874469687500, "Attenzione!" & vbCrLf & _
        "La commessa n°" & lCommeca & " non può essere eliminata" & vbCrLf & _
        "in quanto già utilizzata in:" & vbCrLf & _
        "Operatori Commesse (COMOP).")
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsDestdivDeletable(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal lCoddest As Integer, ByRef strMsgOut As String) As Boolean
    Dim strSQL As String = ""
    Dim strDescr1 As String = ""
    Dim strNomdest As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Preleva la descrizione del conto passato, per l'eventuale messaggio
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT an_descr1 FROM anagra" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strDescr1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Preleva la descrizione della destinazione passato, per l'eventuale messaggio
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT dd_nomdest FROM destdiv" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dd_conto = " & lConto & _
        " AND dd_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strNomdest = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Pallet
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mp_idpall FROM mpallet" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mp_conto = " & lConto & _
        " AND mp_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016529953621824, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Pallet'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Chiamate
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 op_codchia FROM nnchiam" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND op_conto = " & lConto & _
        " AND op_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016534075809324, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Chiamate'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Contratti
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 nnc_numcontr FROM nncontras" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND nnc_conto = " & lConto & _
        " AND nnc_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016534208153074, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Contratti'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Matricole
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 rl_codart FROM nnmatrics" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND rl_conto = " & lConto & _
        " AND rl_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016534811121824, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Matricole'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Prepagati
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 nnp_numprep FROM nnprepag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND nnp_conto = " & lConto & _
        " AND nnp_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016535340028074, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Prepagati/Monte-ore'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Organizzazione
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 og_progr FROM organig" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND og_conto = " & lConto & _
        " AND og_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016536093465574, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Organizzazione'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Magazzini/depositi/terzisti
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codmaga FROM tabmaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_magconto = " & lConto & _
        " AND tb_coddest = " & lCoddest
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016536737528074, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Magazzini/depositi/terzisti'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Documenti
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_tipork FROM testmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_conto = " & lConto & _
        " AND (tm_coddest = " & lCoddest & " OR tm_coddest2 = " & lCoddest & ")"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016537269246824, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Documenti'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Documenti (Note di Prelievo)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_tipork FROM testprb" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_conto = " & lConto & _
        " AND (tm_coddest = " & lCoddest & " OR tm_coddest2 = " & lCoddest & ")"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016537744871824, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Documenti (Note di Prelievo)'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Offerte
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 td_tipork FROM testoff" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND td_conto = " & lConto & _
        " AND (td_coddest = " & lCoddest & " OR td_coddest2 = " & lCoddest & ")"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016538266903074, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Offerte'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Ordini
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 td_tipork FROM testord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND td_conto = " & lConto & _
        " AND (td_coddest = " & lCoddest & " OR td_coddest2 = " & lCoddest & ")"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016538794715574, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Ordini'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Gestione Packing List
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tp_anno FROM testpac" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tp_conto = " & lConto & _
        " AND (tp_coddest = " & lCoddest & " OR tp_coddest2 = " & lCoddest & ")"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsgOut = oApp.Tr(Me, 129016539306873429, "La destinazione '|" & lCoddest.ToString & _
          "|' - |" & strDescr1 & "|" & vbCrLf & _
          "associata al conto '|" & lConto.ToString & "|' - |" & strNomdest & "|" & vbCrLf & _
          "NON può essere eliminata in quanto già presente in: " & vbCrLf & _
          " --> 'Gestione Packing List'.")
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function IsGescommUpdatable(ByVal strDitta As String, ByVal strCodart As String, ByVal bIsRoot As Boolean, _
                                                 ByVal strOldValue As String, ByVal strNewValue As String, ByRef strMsgOut As String) As Boolean
    Dim strSQL As String = ""
    Dim dttArti As New DataTable
    Dim dsTmp As DataSet
    Dim bControllaProposte As Boolean = False
    Dim bControllaListe As Boolean = False
    Dim bControllaOrdini As Boolean = False
    Dim bControllaNote As Boolean = False
    Dim bControllaDocumenti As Boolean = False
    Dim bControllaChiusure As Boolean = False
    Try

      Select Case strOldValue
        Case "N"
          Select Case strNewValue
            Case "O"
              bControllaProposte = True
              bControllaListe = True
              bControllaOrdini = True
            Case "S"
              bControllaProposte = True
              bControllaListe = True
              bControllaOrdini = True
              bControllaNote = True
              bControllaDocumenti = True
              bControllaChiusure = True
          End Select
        Case "O"
          Select Case strNewValue
            Case "N"
              'nessuna verifica da fare
            Case "S"
              bControllaOrdini = True
              bControllaNote = True
              bControllaDocumenti = True
              bControllaChiusure = True
          End Select
        Case "S"
          Select Case strNewValue
            Case "N"
              bControllaOrdini = True
              bControllaNote = True
              bControllaDocumenti = True
              bControllaChiusure = True
            Case "O"
              bControllaOrdini = True
              bControllaNote = True
              bControllaDocumenti = True
              bControllaChiusure = True
          End Select
      End Select

      If bIsRoot Then
        strSQL = " SELECT ar_codart FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ar_codroot = " & CStrSQL(strCodart) & _
                 " ORDER BY ar_codart"
        dttArti = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        strSQL = " SELECT ar_codart FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ar_codart = " & CStrSQL(strCodart)
        dttArti = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      For Each dtrRow As DataRow In dttArti.Rows

        If bControllaProposte Then
          strSQL = " SELECT TOP 1 ol_codart FROM ordlist " & _
                   " WHERE ol_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND ol_commeca = 0 " & _
                   " AND codditt = " & CStrSQL(strDitta)
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128310605165850000, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                        "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                        "in quanto esso è presente nell'archivio delle 'Proposte d'Ordine'," & vbCrLf & _
                                                        "senza indicazione del numero di commessa.")
            Return False
          End If
        End If

        If bControllaListe Then
          strSQL = " SELECT TOP 1 lp_codart FROM listprod " & _
                   " WHERE lp_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND lp_commeca = 0" & _
                   " AND codditt = " & CStrSQL(strDitta)
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128310605899986000, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                        "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                        "in quanto esso è presente nell'archivio delle 'Liste di Produzione'," & vbCrLf & _
                                                        "senza indicazione del numero di commessa.")
            Return False
          End If
        End If

        If bControllaOrdini Then
          strSQL = " SELECT TOP 1 mo_codart FROM movord " & _
                   " WHERE mo_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)
          'Se passaggio da N as O controllo che ci sia la commessa, se c'è siamo a posto, perchè i progressivi correnti non vanno ricostruiti
          If strOldValue = "N" And strNewValue = "O" Then
            strSQL = strSQL & " AND mo_commeca = 0"
          End If
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            If strOldValue = "N" And strNewValue = "O" Then
              strMsgOut = oApp.Tr(Me, 131038044263056179, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                          "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                          "in quanto esso è presente nell'archivio degli 'Ordini/Impegni'," & vbCrLf & _
                                                          "senza indicazione del numero di commessa.")
            Else
              strMsgOut = oApp.Tr(Me, 128310606546762001, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                          "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                          "in quanto esso è presente nell'archivio degli 'Ordini/Impegni'.")
            End If
            Return False
          End If
        End If

        If bControllaNote Then
          strSQL = " SELECT TOP 1 mm_codart FROM movprb " & _
                   " WHERE mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128310606862506000, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                        "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                        "in quanto esso è presente nell'archivio delle 'Note di Prelievo'.")
            Return False
          End If
        End If

        If bControllaDocumenti Then
          strSQL = " SELECT TOP 1 mm_codart FROM movmag " & _
                   " WHERE mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128310606862506002, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                        "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                        "in quanto esso è presente nell'archivio dei 'Documenti di Magazzino'.")
            Return False
          End If
        End If

        If bControllaChiusure Then
          strSQL = " SELECT TOP 1 ld_codart FROM lotcdef " & _
                   " WHERE ld_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)
          dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128310606862506001, "Per l'articolo '" & NTSCStr(dtrRow!ar_codart) & "'," & vbCrLf & _
                                                        "non è possibile cambiare la 'Gestione per commessa', " & vbCrLf & _
                                                        "in quanto esso è presente nell'archivio dei 'Progressivi Definitivi'.")
            Return False
          End If
        End If
      Next

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetOrganig(ByVal strDitta As String, ByVal lProgr As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM organig" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND og_progr = " & lProgr 
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetOrganigFromConto(ByVal strDitta As String, ByVal lConto As Integer, _
                                                  ByVal lCodlead As Integer, ByVal strRuolo As String, _
                                                  ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT organig.* FROM organig " & _
               " WHERE organig.codditt = " & CStrSQL(strDitta)

      If lConto <> 0 Then strSQL += " AND og_conto = " & lConto.ToString
      If lCodlead <> 0 Then strSQL += " AND og_codlead = " & lCodlead.ToString
      If strRuolo <> "" Then
        If CBool(GetSettingBus("OPZIONI", ".", ".", "AttivaRuoliMultipli", "0", ".", "0")) Then
          strSQL += " AND (organig.og_codruaz = " & CStrSQL(strRuolo) & " OR og_progr IN (SELECT og_progr FROM orgaruoli " & _
                                                                                        " WHERE codditt = " & CStrSQL(strDitta) & _
                                                                                        "   AND orgaruoli.og_codruaz = " & CStrSQL(strRuolo) & "))"
        Else
          strSQL += " AND organig.og_codruaz = " & CStrSQL(strRuolo)
        End If
      End If

      strSQL += " AND og_servdeleted = 'N'"

      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrganigDestFromConto(ByVal strDitta As String, ByVal lConto As Integer, _
                                                      ByVal lCodlead As Integer, ByVal strRuolo As String, _
                                                      ByRef dttOut As DataTable, ByVal strCoddest As String) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT organig.* FROM organig " & _
               "WHERE organig.codditt = " & CStrSQL(strDitta)

      If lConto <> 0 Then strSQL += " AND og_conto = " & lConto.ToString
      If lCodlead <> 0 Then strSQL += " AND og_codlead = " & lCodlead.ToString
      If strRuolo <> "" Then
        If CBool(GetSettingBus("OPZIONI", ".", ".", "AttivaRuoliMultipli", "0", ".", "0")) Then
          strSQL += " AND (organig.og_codruaz = " & CStrSQL(strRuolo) & " OR og_progr IN (SELECT og_progr FROM orgaruoli " & _
                                                                                        " WHERE codditt = " & CStrSQL(strDitta) & _
                                                                                        "   AND orgaruoli.og_codruaz = " & CStrSQL(strRuolo) & "))"
        Else
          strSQL += " AND organig.og_codruaz = " & CStrSQL(strRuolo)
        End If
      End If

      strSQL += " AND og_old = 'N' " & _
                " AND og_dtfine >= " & CDataSQL(Now) & _
                " AND og_coddest = " & strCoddest & _
                " ORDER BY organig.og_progr"
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TraduciWhere(ByVal strQuery As String, ByRef strSQL As String) As Boolean
    '-------------------------
    'data una strQuery dove and e or sono rimappati in modo diverso (vedi bnmgarti)
    'aggiunge a strSQL la strQuery tradotta
    Dim strAnd() As String = Nothing
    Dim strOr() As String = Nothing
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim bOpen As Boolean = False
    Dim strSQLBuilder As New System.Text.StringBuilder
    Try
      If strQuery <> "" Then
        'se ho usato il carattere § nel codice articolo o nelle descrfizioni, devo isolarlo
        For i = 0 To strQuery.Length - 1
          If strQuery(i) = "'"c Then bOpen = Not bOpen
          If strQuery(i) = "§"c AndAlso bOpen Then strQuery = strQuery.Substring(0, i) & "▌" & strQuery.Substring(i + 1)
        Next
        strAnd = strQuery.Split("§"c)
        strSQLBuilder.Append(strSQL)
        For i = 0 To strAnd.Length - 1
          strSQLBuilder.Append(" AND (")
          strOr = strAnd(i).Split("|"c)
          strSQLBuilder.Append(strOr(0))
          For e = 1 To strOr.Length - 1
            strSQLBuilder.Append(strOr(e))
            strSQLBuilder.Append(" OR ")
            strSQLBuilder.Append(strOr(0))
            strSQLBuilder.Append(" ")
          Next
          If strOr.Length > 1 Then strSQLBuilder = strSQLBuilder.Remove(strSQLBuilder.Length - (4 + strOr(0).Length), (4 + strOr(0).Length))
          strSQLBuilder.Append(")")
        Next
        strSQL = strSQLBuilder.Replace("▌", "§").ToString
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsDocRetail(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strTabella As String = ""
    Dim strPrefisso As String = ""
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- A seconda del tipo documento passato, determina il nome della tabella da analizzare
      '-----------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A", "B", "C", "D", "E", "F", "I", "J", "K", "L", "M", "N", "P", "S", "T", "U", "W", "Z"
          strTabella = "testmags"
          strPrefisso = "tms"
        Case "#", "$", "H", "O", "Q", "R", "V", "X", "Y"
          strTabella = "testords"
          strPrefisso = "tds"
      End Select

      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT " & strPrefisso & "_tipork FROM " & strTabella & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND " & strPrefisso & "_tipork = " & CStrSQL(strTipork) & _
        " AND " & strPrefisso & "_anno = " & nAnno & _
        " AND " & strPrefisso & "_serie = " & CStrSQL(strSerie) & _
        " AND " & strPrefisso & "_numdoc = " & lNumdoc

      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttOut.Rows.Count > 0 Then
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsDocRetailNew(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strTabella As String = ""
    Dim strPrefisso As String = ""
    Dim strTipo As String = ""
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- A seconda del tipo documento passato, determina il nome della tabella da analizzare
      '-----------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A", "B", "C", "D", "E", "F", "I", "J", "K", "L", "M", "N", "P", "S", "T", "U", "W", "Z"
          strTabella = "testmagc"
          strPrefisso = "tm"
          strTipo = "doc"
        Case "#", "$", "H", "O", "Q", "R", "V", "X", "Y"
          strTabella = "testord"
          strPrefisso = "td"
          strTipo = "ord"
      End Select

      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 " & strPrefisso & "_tipork FROM " & strTabella & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND " & strPrefisso & "_tipork = " & CStrSQL(strTipork) & _
                " AND " & strPrefisso & "_anno = " & nAnno & _
                " AND " & strPrefisso & "_serie = " & CStrSQL(strSerie) & _
                " AND " & strPrefisso & "_num" & strTipo & " = " & lNumdoc & _
                " AND " & strPrefisso & "_retail = 'S'"

      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttOut.Rows.Count > 0 Then
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaUmMis(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    'prendo sia quelle di artico che quelle di tabumis, visto che se in tabumis ho NR e su artico Nr (case-sensitive) 
    'nei documenti / ordini alcune righe non avrebberò l'unità di misura visualizzata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT1() As DataRow = Nothing
    Try

      dttOut.Clear()
      dttOut.Columns.Clear()
      dttOut.Columns.Add("tb_codumis", GetType(String))
      dttOut.CaseSensitive = True

      'prima carico le unità di misura utilizzate sugli articoli
      'LENTISSIMA con oltre 300.000 articoli: bnveboll impiega 2 minuti per eseguire questa query!!!
      'strSQL = "SELECT ar_unmis AS ar_unmis, convert(binary(5), ar_unmis) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND NOT ar_unmis IS NULL GROUP BY ar_unmis, convert(binary(5), ar_unmis) " & _
      '         " UNION " & _
      '         "SELECT ar_unmis2 AS ar_unmis, convert(binary(5), ar_unmis2) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND NOT ar_unmis2 IS NULL GROUP BY ar_unmis2, convert(binary(5), ar_unmis2) " & _
      '         " UNION " & _
      '         "SELECT ar_confez2 AS ar_unmis, convert(binary(5), ar_confez2) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND NOT ar_confez2 IS NULL GROUP BY ar_confez2, convert(binary(5), ar_confez2) " & _
      '         " UNION " & _
      '         "SELECT ar_um4 AS ar_unmis, convert(binary(5), ar_um4) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND NOT ar_um4 IS NULL GROUP BY ar_um4, convert(binary(5), ar_um4) "
      'dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      'For Each dtrT As DataRow In dttTmp.Rows
      '  If NTSCStr(dtrT!ar_unmis) <> "" Then
      '    dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_unmis)))
      '    If dtrT1.Length = 0 Then
      '      dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis)})
      '    Else
      '      If NTSCStr(dtrT1(0)!tb_codumis).Equals(NTSCStr(dtrT!ar_unmis)) = Nothing Then
      '        dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis)})
      '      End If
      '    End If
      '  End If
      'Next

      strSQL = "select distinct " & _
               "ar_unmis COLLATE Latin1_GENERAL_CS_AS as ar_unmis, " & _
               "ar_unmis2 COLLATE Latin1_GENERAL_CS_AS as ar_unmis2, " & _
               "ar_confez2 COLLATE Latin1_GENERAL_CS_AS as ar_confez2, " & _
               "ar_um4 COLLATE Latin1_GENERAL_CS_AS as ar_um4 from artico WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        If NTSCStr(dtrT!ar_unmis) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_unmis)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis)})
          End If
        End If

        If NTSCStr(dtrT!ar_unmis2) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_unmis2)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis2)})
          End If
        End If

        If NTSCStr(dtrT!ar_confez2) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_confez2)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_confez2)})
          End If
        End If

        If NTSCStr(dtrT!ar_um4) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_um4)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_um4)})
          End If
        End If
      Next

      strSQL = "SELECT tb_codumis FROM tabumis WHERE codditt = " & CStrSQL(strDitta) & " GROUP BY tb_codumis"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        If NTSCStr(dtrT!tb_codumis) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!tb_codumis)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!tb_codumis)})
          End If
        End If
      Next

      dttOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function EscompFromDate(ByVal strDitta As String, ByVal dtDate As Date, ByRef nEscomp As Integer, ByRef dttEscomp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabesco.* FROM tabesco " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND " & CDataSQL(dtDate) & " BETWEEN tb_dtineser AND tb_dtfieser "

      dttEscomp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttEscomp.Rows.Count = 0 Then Return False

      nEscomp = NTSCInt(dttEscomp.Rows(0)!tb_codesco)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetBusRelease(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("RELEASE") Then
        dttOut = oApp.DSShared.Tables("RELEASE").Clone
        For Each dtrT As DataRow In oApp.DSShared.Tables("RELEASE").Select("rel_sw = 'BUSINESS'")
          dttOut.ImportRow(dtrT)
        Next
      Else
        strSQL = "SELECT rel_maior, rel_pers FROM release WHERE UPPER(rel_sw) = 'BUSINESS'"
        dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function


  Public Overridable Function DocumentLockAdd(ByVal strDitta As String, ByVal dtControllaConcorrenzaOggettiDataLock As DateTime, _
                                             ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                             ByVal lNumdoc As Integer, ByVal lVers As Integer) As Boolean
    'memorizzo in ttkeys il fatto che il documento è aperto dall'operatore corrente
    'in questo modo se un altro operatore apre lo stesso doc può venir avvisato/bloccato

    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strSessionId As String = ""
    Try
      If oApp.User.Nome.Length > 18 Then Return True 'la tabella non contiene un campo di 20 caratteri ... da migliorare in futuro

      If CLN__STD.IsWebApp Then
        strSessionId = oApp.WebIDMsg
      Else
        strSessionId = System.Diagnostics.Process.GetCurrentProcess.Id.ToString
      End If

      'verifico se il record è già presente:
      'ad esempio perchè ho aperto un ddt e gli ho aggiunto righe d'ordine di orgina già presente in veboll
      'ignoro l'errore
      strSQL = "SELECT TOP 1 * FROM ttkeys " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = -1" & _
               " AND ke_tipork = " & CStrSQL(strTipork) & _
               " AND ke_anno = " & nAnno.ToString & _
               " AND ke_serie = " & CStrSQL(strSerie) & _
               " AND ke_numdoc = " & lNumdoc.ToString & _
               " AND ke_vers = " & lVers.ToString
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      'prima memorizzo che ho aperto il documento corrente
      strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
               " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
               " VALUES (" & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
               CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
               CStrSQL(strTipork) & ", " & nAnno.ToString & ", " & CStrSQL(strSerie) & ", " & _
               lNumdoc.ToString & ", " & lVers.ToString & ", " & strSessionId & ")"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'in base al tipo documento, memorizzo anche eventuali lock su ordini inseriti in ddt e/o note di prelievo 
      'e/o ordini aperti e/o offerte
      Select Case strTipork
        Case "O", "R"
          'aggiungo eventuali ordini aperti
          strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
                   " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
                   " SELECT " & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
                   CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
                   " mo_oatipo, mo_oaanno, mo_oaserie, mo_oanum, 0, " & _
                   strSessionId & _
                   " FROM movord WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mo_tipork = " & CStrSQL(strTipork) & _
                   " AND mo_anno = " & nAnno.ToString & _
                   " AND mo_serie = " & CStrSQL(strSerie) & _
                   " AND mo_numord = " & lNumdoc.ToString & _
                   " AND mo_oanum <> 0 " & _
                   " GROUP BY mo_oatipo, mo_oaanno, mo_oaserie, mo_oanum "
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          If strTipork = "R" Then
            'aggiungo eventuali offerte 
            strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
                     " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
                     " SELECT " & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
                     CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
                     " mo_oqtipo, mo_oqanno, mo_oqserie, mo_oqnum, mo_oqvers, " & _
                     strSessionId & _
                     " FROM movord WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mo_tipork = " & CStrSQL(strTipork) & _
                     " AND mo_anno = " & nAnno.ToString & _
                     " AND mo_serie = " & CStrSQL(strSerie) & _
                     " AND mo_numord = " & lNumdoc.ToString & _
                     " AND mo_oqnum <> 0 " & _
                     " GROUP BY mo_oqtipo, mo_oqanno, mo_oqserie, mo_oqnum, mo_oqvers "
            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If

        Case "A", "B", "C", "E", "F", "I", "J", "L", "M", "N", "S", "T", "U", "Z"
          'documento di magazzino: aggiungo eventuali ordini / impegni
          strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
                   " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
                   " SELECT " & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
                   CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
                   " mm_ortipo, mm_oranno, mm_orserie, mm_ornum, 0, " & _
                   strSessionId & _
                   " FROM movmag WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_ornum <> 0 " & _
                   " GROUP BY mm_ortipo, mm_oranno, mm_orserie, mm_ornum "
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          'ora le note di prelievo
          strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
                   " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
                   " SELECT " & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
                   CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
                   " mm_nptipo, mm_npanno, mm_npserie, mm_npnum, 0, " & _
                   strSessionId & _
                   " FROM movmag WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_npnum <> 0 " & _
                   " GROUP BY mm_nptipo, mm_npanno, mm_npserie, mm_npnum "
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        Case "W"
          'note di prel: aggiungo eventuali ordini / impegni
          strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_datreg, " & _
                   " ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_vers, ke_rigaa) " & _
                   " SELECT " & CStrSQL(strDitta) & ", -1, " & CStrSQL(oApp.User.Nome) & ", " & _
                   CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & ", " & _
                   " mm_ortipo, mm_oranno, mm_orserie, mm_ornum, 0, " & _
                   strSessionId & _
                   " FROM movprb WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_ornum <> 0 " & _
                   " GROUP BY mm_ortipo, mm_oranno, mm_orserie, mm_ornum "
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function DocumentLockRemove(ByVal strDitta As String, ByVal dtControllaConcorrenzaOggettiDataLock As DateTime) As Boolean
    'cancello da ttkeys il fatto che il documento è aperto dall'operatore corrente
    Dim strSQL As String = ""
    Try
      If oApp.User.Nome.Length > 18 Then Return True 'la tabella non contiene un campo di 20 caratteri ... da migliorare in futuro

      strSQL = "DELETE FROM ttkeys " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = -1" & _
               " AND ke_codart = " & CStrSQL(oApp.User.Nome) & _
               " AND ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock)
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DocumentLockCheck(ByVal strDitta As String, ByVal dtControllaConcorrenzaOggettiDataLock As DateTime, _
                                               ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                               ByVal lNumdoc As Integer, ByVal lVers As Integer, ByRef strMsgOut As String) As Boolean
    'verifico se il documento passato in input è loccato da un operatore 
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strMess1 As String = ""
    Dim strMess2 As String = ""
    Dim strIIf1 As String = ""
    Try
      strMsgOut = ""
      strMess1 = oApp.Tr(Me, 130427113595536490, "Versione")
      If oApp.User.Nome.Length > 18 Then Return True 'la tabella non contiene un campo di 20 caratteri ... da migliorare in futuro

      'cancello eventuali record più vecchi di 1 giorno
      strSQL = "DELETE FROM ttkeys " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = -1" & _
               " AND ke_datreg < " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock.AddHours(-24))
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT TOP 1 * FROM ttkeys " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = -1" & _
               " AND ke_tipork = " & CStrSQL(strTipork) & _
               " AND ke_anno = " & nAnno.ToString & _
               " AND ke_serie = " & CStrSQL(strSerie) & _
               " AND ke_numdoc = " & lNumdoc.ToString & _
               " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
               " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")" '" AND ke_vers = " & lVers.ToString & _
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
        strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString

        strMsgOut = oApp.Tr(Me, 129176018401035157, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                    "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                    dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                    "é aperto dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                    NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
        Return True
      End If

      'in base al tipo documento, verifico anche eventuali lock su ordini inseriti in ddt e/o note di prelievo 
      'e/o ordini aperti e/o offerte
      Select Case strTipork
        Case "O", "R"
          'verifico eventuali ordini aperti
          strSQL = "SELECT TOP 1 ttkeys.* FROM ttkeys INNER JOIN movord ON ttkeys.codditt = movord.codditt " & _
                   " AND ttkeys.ke_tipork = movord.mo_oatipo AND ttkeys.ke_anno = movord.mo_oaanno " & _
                   " AND ttkeys.ke_serie = movord.mo_oaserie AND ttkeys.ke_numdoc = movord.mo_oanum " & _
                   " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = -1" & _
                   " AND mo_tipork = " & CStrSQL(strTipork) & _
                   " AND mo_anno = " & nAnno.ToString & _
                   " AND mo_serie = " & CStrSQL(strSerie) & _
                   " AND mo_numord = " & lNumdoc.ToString & _
                   " AND mo_oanum <> 0 " & _
                   " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
                   " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")"
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
            strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString()

            strMsgOut = oApp.Tr(Me, 129176112953476563, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                        "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                        dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                        "é aperto (direttamente o indirettamente) dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                        NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
            Return True
          End If

          If strTipork = "R" Then
            'verifico eventuali offerte 
            strSQL = "SELECT TOP 1 ttkeys.* FROM ttkeys INNER JOIN movord ON ttkeys.codditt = movord.codditt " & _
                     " AND ttkeys.ke_tipork = movord.mo_oqtipo AND ttkeys.ke_anno = movord.mo_oqanno " & _
                     " AND ttkeys.ke_serie = movord.mo_oqserie AND ttkeys.ke_numdoc = movord.mo_oqnum " & _
                     " AND ttkeys.ke_vers = movord.mo_oqvers" & _
                     " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                     " AND instid = -1" & _
                     " AND mo_tipork = " & CStrSQL(strTipork) & _
                     " AND mo_anno = " & nAnno.ToString & _
                     " AND mo_serie = " & CStrSQL(strSerie) & _
                     " AND mo_numord = " & lNumdoc.ToString & _
                     " AND mo_oqnum <> 0 " & _
                     " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
                     " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")"
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then
              strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
              strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString()

              strMsgOut = oApp.Tr(Me, 129176112981308594, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                          "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                          dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                          "é aperto (direttamente o indirettamente) dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                          NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
              Return True
            End If
          End If

        Case "A", "B", "C", "E", "F", "I", "J", "L", "M", "N", "S", "T", "U", "Z"
          'documento di magazzino: verifico eventuali ordini / impegni
          strSQL = "SELECT TOP 1 ttkeys.* FROM ttkeys INNER JOIN movmag ON ttkeys.codditt = movmag.codditt " & _
                   " AND ttkeys.ke_tipork = movmag.mm_ortipo AND ttkeys.ke_anno = movmag.mm_oranno " & _
                   " AND ttkeys.ke_serie = movmag.mm_orserie AND ttkeys.ke_numdoc = movmag.mm_ornum " & _
                   " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = -1" & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_ornum <> 0 " & _
                   " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
                   " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")"
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
            strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString()

            strMsgOut = oApp.Tr(Me, 129176113010029297, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                        "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                        dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                        "é aperto (direttamente o indirettamente) dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                        NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
            Return True
          End If

          'ora le note di prelievo
          strSQL = "SELECT TOP 1 ttkeys.* FROM ttkeys INNER JOIN movmag ON ttkeys.codditt = movmag.codditt " & _
                   " AND ttkeys.ke_tipork = movmag.mm_nptipo AND ttkeys.ke_anno = movmag.mm_npanno " & _
                   " AND ttkeys.ke_serie = movmag.mm_npserie AND ttkeys.ke_numdoc = movmag.mm_npnum " & _
                   " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = -1" & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_npnum <> 0 " & _
                   " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
                   " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")"
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
            strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString()

            strMsgOut = oApp.Tr(Me, 129176113034111328, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                        "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                        dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                        "é aperto (direttamente o indirettamente) dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                        NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
            Return True
          End If

        Case "W"
          'note di prel: verifico eventuali ordini / impegni
          strSQL = "SELECT TOP 1 ttkeys.* FROM ttkeys INNER JOIN movprb ON ttkeys.codditt = movprb.codditt " & _
                   " AND ttkeys.ke_tipork = movprb.mm_ortipo AND ttkeys.ke_anno = movprb.mm_oranno " & _
                   " AND ttkeys.ke_serie = movprb.mm_orserie AND ttkeys.ke_numdoc = movprb.mm_ornum " & _
                   " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
                   " AND instid = -1" & _
                   " AND mm_tipork = " & CStrSQL(strTipork) & _
                   " AND mm_anno = " & nAnno.ToString & _
                   " AND mm_serie = " & CStrSQL(strSerie) & _
                   " AND mm_numdoc = " & lNumdoc.ToString & _
                   " AND mm_ornum <> 0 " & _
                   " AND NOT (ke_datreg = " & CDataOraSQL(dtControllaConcorrenzaOggettiDataLock) & _
                   " AND ke_codart = " & CStrSQL(oApp.User.Nome) & ")"
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strMess2 = ", " & strMess1 & " " & dttTmp.Rows(0)!ke_vers.ToString
            strIIf1 = IIf(NTSCInt(dttTmp.Rows(0)!ke_vers) <> 0, strMess2, "").ToString()

            strMsgOut = oApp.Tr(Me, 129176113060195313, "Attenzione: il documento di tipo '|" & TraduciTipork(dttTmp.Rows(0)!ke_tipork.ToString) & _
                        "|', Anno |" & dttTmp.Rows(0)!ke_anno.ToString & "|, Serie |'" & dttTmp.Rows(0)!ke_serie.ToString & "'|, Numero |" & _
                        dttTmp.Rows(0)!ke_numdoc.ToString & "| |" & strIIf1 & "|" & vbCrLf & _
                        "é aperto (direttamente o indirettamente) dall'operatore '|" & dttTmp.Rows(0)!ke_codart.ToString & "|' dal '|" & _
                        NTSCDate(dttTmp.Rows(0)!ke_datreg).ToString() & "|'")
            Return True
          End If

      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function TraduciTipork(ByVal strTiporkIn As String) As String
    Try
      TraduciTipork = strTiporkIn
      Select Case strTiporkIn
        Case "A" : TraduciTipork = "Fattura Imm. emessa"
        Case "B" : TraduciTipork = "DDT emesso"
        Case "C" : TraduciTipork = "Corrispettivo emesso"
        Case "E" : TraduciTipork = "Nota di Addeb. emessa"
        Case "F" : TraduciTipork = "Ric.Fiscale Emessa"
        Case "I" : TraduciTipork = "Riemissione Ric.Fiscali"
        Case "J" : TraduciTipork = "Nota Accr. ricevuta"
        Case "L" : TraduciTipork = "Fattura Imm. ricevuta"
        Case "M" : TraduciTipork = "DDT ricevuto"
        Case "N" : TraduciTipork = "Nota Accr. emessa"
        Case "S" : TraduciTipork = "Fatt.Ric.Fisc. Emessa"
        Case "T" : TraduciTipork = "Carico da produzione"
        Case "W" : TraduciTipork = "Nota di prelievo"
        Case "Z" : TraduciTipork = "Bolla di mov. interna"
        Case "R" : TraduciTipork = "Impegno cliente"
        Case "O" : TraduciTipork = "Ordine fornitore"
        Case "H" : TraduciTipork = "Ordine di produzione"
        Case "X" : TraduciTipork = "Impegno Trasferimento"
        Case "Q" : TraduciTipork = "Preventivo"
        Case "#" : TraduciTipork = "Impegno di commessa"
        Case "V" : TraduciTipork = "Impegno cliente aperto"
        Case "$" : TraduciTipork = "Ordine fornitore aperto"
        Case "!" : TraduciTipork = "Offerta"
      End Select

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCausaleSplitPaymentFromTpbf(ByVal strDitta As String, ByVal nCodtpbf As Integer) As Boolean
    'verifica se la casuale di CG collegata al tipoBF è di tipo 'spliy 'payment'
    'sono stati messi dei blocchi per cui la causale di tipo 'slit' non può essere messa in tabpeve
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT tb_tipmov FROM tabtpbf INNER JOIN tabcauc ON tabtpbf.tb_codcauc = tabcauc.tb_codcauc " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codtpbf = " & nCodtpbf & _
               " AND tb_tipmov = 'P'"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


#Region "Scrittura di priana2 e pria2comp da TESTMAG-MOVMAG"
  Public Overridable Function ScriviPriana2DaTestmag(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByVal strDatdoc As String, _
                                                   ByRef dbConn As DbConnection, ByVal lIITtkeys As Integer, _
                                                   ByVal bRiscriviSoloScarichieQtaProd As Boolean, _
                                                   ByVal bEscludiLavorazTerziste As Boolean, _
                                                   ByVal bEscludiSpesePiede As Boolean) As Boolean

    'dato il documento passato in input, scrivo le righe di priana2 (sempre 2 per ogni riga di movmag, se causale doppia 4 righe)
    'i test DEVONO già stati eseguiti tutti esattamente come beveboll.TestPreSalvaTestmag_CheckCa2

    '----------------------------
    'come devono essere fatte le registrazioni di CA

    '----------------------------
    'per sapere se movmag.mm_contocontr deve andare in priana2 in DARE oppure AVERE, 
    'mi baso su tabcaum.tb_testci (indipendentemente che sia DARE CG o DARE MG / AVERE CG o AVERE MG), 
    'se ' ' .... non va in ca: c'è un test in tabcaum per non far settare causali di CA se causale nulla
    'deroga per carichi da produzione: mm_concocontr va sempre in dare, anche se tb_testci = A o B
    'per le spese di piede mi baso sul fatto che sia un documento emesso o ricevuto e prendo sempre contropartirte, MAI conti da tabpeve/tabpeac

    '----------------------------
    'vengono portate in CA sempre e solo le righe e spese di piede con contocg che gestisce 
    'la ca e QTA e/o VALORE diversi da 0!! (se il contocg gestisce la CA, la CA deve essere gestita sempre)

    '----------------------------
    'come devono essere fatte le registrazioni di CA
    'a) DDT EMESSO / BOLLA MOVIM. INTERNA
    'causale tabcaca presa da tabcaum
    'valore preso da movmag.mm_valore
    'fatt da emettere (preso da tabcove.tb_concovag)   A    movmag.mm_contocontr
    'per spese di piede
    'causale tabcaca presa da tabtpbf
    'fatt da emettere (preso da tabcove.tb_concovag di controp di tabpeve/peac per quel tipo di spesa) A    sottoconti presi da controp di tabpeve/peac

    'oppure, per reso a fornitore
    'movmag.mm_contocontr   A  fatture da ricevere (preso da tabcove.tb_concovag)

    'b) DDT RICEVUTO
    'come per ddt emessi, ma registraz. del tipo
    'costo    A    fatt. da ricevere (preso da tabcove.tb_concovag)

    'c) carico da produzione
    '   1) riga qta prodotta (unica riga solo in dare):
    'causale da tabpecx che movim. solo la qta
    'qta da movmaga riga 'T'
    'sottoconto 'q.ta prodotta' non gestito a valore preso tabcove.tb_concovag id movmag riga T
    '
    '   2) materiali consumati (tutto da righe di scarico 'U', sia valore che sottoconti di CA)
    'causale tabcaca da tabcaum di riga U
    'valore = mm_valore di movmag riga U
    ''MP consumate x produzione' (movmag.mm_contocontr riga 'U')    A   'consumi di MP' preso tabcove.tb_concovag (è solo un conto di giro)
    '
    '   3) lavorazioni (sia interne che esterne)
    'se produzione da terzista prendo tabcove e contoca di movmag T (tanto c'è una sola riga - per compatibilità con PNFA ed archivi già esistenti) altrimenti prendo tabcove e contoca da lavcent
    'causale tabcaca da tabcaum di riga T
    'valore da lavcent.
    ''lavorazioni interne/esterne assegnazione' (lavcent.lce_contocontr se prod interna, altrikmenti movmag.mm_contocontr)    A   'lavorazioni interne/esterne prodotte' preso tabcove.tb_concovag di lavcent se prod interna, altrimenti da tabcove di movmag.mm_controp riga T(è solo un conto di giro)
    'spese di piede come per DDT ricevuto

    'd) DDT EMESSI/RICEVUTI CON CAUSALE DOPPIA (trasferimenti tra magazzini)
    'su prima riga di keymag, quella di scarico
    'causale tabcaca presa da tabcaum
    'valore preso da movmag.mm_valore
    ''MP c/to transito' preso da tabcove.tb_concovag (no gestione centro,linea,commessa,...    A   'MP trasferimenti' da movmag.mm_contocontr
    'su seconda riga di keymag, quella di carico
    ''MP arrivi / assegnazioni' preso da tabcove.tb_concova2      A 'MP c/to transito' preso da tabcove.tb_concovag (no gestione centro,linea,commessa,...

    'OCCHIO: se documento che muove due magazzini devo inserire in priana2 la riga 2 volte
    'NON SONO GESTITI I DATI IN VALUTA

    'se documento già fatturato non verrà più scritta priana2, visto che il giro è:
    'quando registro DDT ricevuto rilevo 'costi a fatt da ricevere'
    'quando registro la FATT DIFF ricevuta (fadi/fdin) cancello 'costi a fatt da ric'
    'quando porto in contabilità (pnfa) registro la ca2 collegata sia alla cg che alle righe di movmag
    'se cancello la fattura differita devo anche rigenerare le righe di priana2 da movmag

    'NON SI POSSONO GESTIRE IN CA LE FATT IMMEDIATE RICEVUTE, BISOGNA PASSARE SEMPRE PER DDT RIC E FT DIFF RICEVUTE
    'LE FATTURE IMMEDIATE EMESSE ANDRANNO IN CA2 SOLO DA PNFA, VISTO CHE UNA VOLTA EMESSE POSSONO SUBITO ESSERE PORTATE IN CG

    Dim strSQL As String = ""
    Dim lMinProgr As Integer = 0
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Dim lProgr As Integer = 0
    Dim nEscomp As Integer = 0
    Dim i As Integer = 0

    Try
      '----------------------------------------------
      'solo da pnfa devo riscrivere solo le righe U e la qta prodotta, visto che nella fattura ricevuta da fornitore ho solo costo lavorazioni e spese di piede
      If bRiscriviSoloScarichieQtaProd And strTipork <> "T" Then Return True

      '----------------------------------------------
      'scrivo le righe di costo/ricavo prese dal corpo
      'se è un carico da prod devo prendere le righe U
      'in ke_conto metto la contropartita presa da tabcove.tb_concovag, 
      'in ke_coddest il conto (normalmente mm_contocontr ma nei doc che muovono 2 magaz sul maga di carico tabcove.tb_concova2
      'prendo solo le righe con anagra preso da tabcove an_flci <> ' ' o con tabcove dove non è indicato anagra (tb_concove) ma c'à conto ca (tb_concova)
      strSQL = "DELETE FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'esercizio di competenza: prendo escomp con data documento compresa in data ini/fin ...
      strSQL = "SELECT tb_codesco, tb_gestcadp FROM tabesco WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_dtineser <= " & CDataSQL(strDatdoc) & " AND tb_dtfieser >= " & CDataSQL(strDatdoc)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count = 0 Then Return True
      If dttTmp.Rows(0)!tb_gestcadp.ToString = "N" Then Return True
      nEscomp = NTSCInt(dttTmp.Rows(0)!tb_codesco)
      dttTmp.Clear()

      strSQL = "INSERT INTO ttkeys (instid, codditt, ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_riga, " & _
               " ke_vers, ke_lead, ke_conto, ke_coddest, ke_datreg, ke_olprogr, ke_numrata) " & _
               " SELECT " & lIITtkeys & ", movmag.codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, " & _
               " CASE WHEN mm_tipork = 'U' THEN 1 ELSE CASE WHEN tabcaum.tb_testci = 'D' OR tabcaum.tb_testci = 'E' THEN 1 ELSE CASE WHEN tabcaum.tb_testci = 'A' OR tabcaum.tb_testci = 'B' THEN -1 ELSE 0 END END END, tb_codcacadd, " & _
               " tabcove.tb_concovag, " & _
               " CASE WHEN mm_magaz2 = km_magaz AND mm_magaz2 <> 0 AND (tabcaum.tb_testci = 'D' OR tabcaum.tb_testci = 'E') THEN tabcove.tb_concova2 ELSE mm_contocontr END, " & _
               " tm_datdoc, tm_codcli, tabmaga.tb_codstab " & _
               " FROM ((((((" & oCldBase.strJoinTestmagMovmag & ") " & _
               " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork " & _
               " AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie " & _
               " AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga) " & _
               " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum) " & _
               " INNER JOIN tabcaca ON tabcaum.tb_codcacadd = tabcaca.tb_codcaca) " & _
               " INNER JOIN tabmaga ON keymag.codditt = tabmaga.codditt AND keymag.km_magaz = tabmaga.tb_codmaga) " & _
               " INNER JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove) " & _
               " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(IIf(strTipork = "T", "U", strTipork).ToString) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc & _
               " AND tb_codcacadd <> 0 " & _
               " AND tabcaum.tb_testci <> ' ' " & _
               " AND ((tb_concove = 0 AND tb_concova <> 0) OR an_flci <> ' ') " & _
               " AND (movmag.mm_quant <> 0 or mm_valore <> 0)"
      nRec = oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If nRec > 0 Then
        'se ci sono righe da scrivere in priana2, devo calcolare il progressivo da memorizzare poi in priana2
        strSQL = "SELECT min(ke_progr) as progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        lMinProgr = NTSCInt(dttTmp.Rows(0)!progr)
        dttTmp.Clear()

        lProgr = LegNuma(strDitta, "PX", " ", 0, True, dbConn)      'primo progressivo da usare in priana2
        strSQL = "UPDATE tabnuma SET tb_numprog = " & lProgr + nRec & " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = 'PX'"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'inserisco la prima riga di PRIANA2:
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_idwbs, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT movmag.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 " CASE WHEN ke_vers = 1 THEN 1 ELSE 2 END, " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE movmag.mm_codcena END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE movmag.mm_codcfam END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE movmag.mm_commeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE movmag.mm_subcommeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE movmag.mm_pmtaskid END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE movmag.mm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers = 1) OR (tb_aqta = 'S' AND ke_vers = -1) THEN movmag.mm_unmis ELSE ' ' END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers = 1) OR (tb_aqta = 'S' AND ke_vers = -1) THEN movmag.mm_quant * ke_vers ELSE 0 END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers = 1) OR (tb_aqta = 'S' AND ke_vers = -1) THEN movmag.mm_quant ELSE 0 END, " & _
                 " CASE WHEN ke_vers = 1 THEN 'D' ELSE 'A' END, " & _
                 " movmag.mm_valore * ke_vers, movmag.mm_valore, " & _
                 " CASE WHEN ke_vers = 1 THEN 2 ELSE 1 END, 'M', " & _
                 " movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, " & _
                 " ke_datreg, movmag.mm_numdoc, movmag.mm_serie, ke_datreg, 0, 0, " & _
                 " CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_datini ELSE movmag.mm_datini END, " & _
                 " CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_datfin ELSE movmag.mm_datfin END, " & _
                 " 'N', 0, ke_lead, 0, " & _
                 " CASE WHEN ac_richarti = 'N' THEN ' ' ELSE CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_codart ELSE movmag.mm_codart END END, " & _
                 " CASE WHEN ac_richarti = 'N' THEN 0 ELSE CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_fase ELSE movmag.mm_fase END END, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " movmag.mm_ultagg, " & CStrSQL(oApp.User.Nome) & ", " & _
                 " CASE WHEN movmag.mm_tipork = 'T' OR movmag.mm_tipork = 'U' THEN 'T' ELSE null END " & _
                 " FROM (((movmag INNER JOIN ttkeys ON movmag.codditt = ttkeys.codditt " & _
                 " AND movmag.mm_tipork = ttkeys.ke_tipork AND movmag.mm_anno = ttkeys.ke_anno " & _
                 " AND movmag.mm_serie = ttkeys.ke_serie AND movmag.mm_numdoc = ttkeys.ke_numdoc " & _
                 " AND movmag.mm_riga = ttkeys.ke_riga) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_coddest = anagca.ac_conto) " & _
                 " INNER JOIN tabcaca ON ttkeys.ke_lead = tabcaca.tb_codcaca) " & _
                 " LEFT JOIN movmag AS mmT ON movmag.codditt = mmT.codditt AND movmag.mm_prtipo = mmT.mm_tipork " & _
                 " AND movmag.mm_pranno = mmT.mm_anno AND movmag.mm_prserie = mmT.mm_serie " & _
                 " AND movmag.mm_prnum = mmT.mm_numdoc AND movmag.mm_prriga = mmT.mm_riga " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'adesso la seconda riga: la query è come la prima, solo che anagca è in join con 
        'ke_conto (rispettivamente tb_dcontoca o tb_acontoca di tabcaca)
        'e ke_vers (ovvero tb_esist) deve essere moltiplicato per -1 per cambiare sezione dare/avere
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_idwbs, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT movmag.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 " CASE WHEN ke_vers = -1 THEN 1 ELSE 2 END, " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE movmag.mm_codcena END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE movmag.mm_codcfam END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE movmag.mm_commeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE movmag.mm_subcommeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE movmag.mm_pmtaskid END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE movmag.mm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers * -1 = 1) OR (tb_aqta = 'S' AND ke_vers * -1 = -1) THEN movmag.mm_unmis ELSE ' ' END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers * -1 = 1) OR (tb_aqta = 'S' AND ke_vers * -1 = -1) THEN movmag.mm_quant * ke_vers * -1 ELSE 0 END, " & _
                 " CASE WHEN (tb_dqta = 'S' AND ke_vers * -1 = 1) OR (tb_aqta = 'S' AND ke_vers * -1 = -1) THEN movmag.mm_quant ELSE 0 END, " & _
                 " CASE WHEN ke_vers = -1 THEN 'D' ELSE 'A' END, " & _
                 " movmag.mm_valore * ke_vers * -1, movmag.mm_valore, " & _
                 " CASE WHEN ke_vers = -1 THEN 2 ELSE 1 END, 'M', " & _
                 " movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, " & _
                 " ke_datreg, movmag.mm_numdoc, movmag.mm_serie, ke_datreg, 0, 0, " & _
                 " CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_datini ELSE movmag.mm_datini END, " & _
                 " CASE WHEN movmag.mm_tipork = 'U' THEN mmT.mm_datfin ELSE movmag.mm_datfin END, " & _
                 " 'N', 0, ke_lead, 0, " & _
                 " CASE WHEN ac_richarti = 'N' THEN ' ' ELSE movmag.mm_codart END, " & _
                 " CASE WHEN ac_richarti = 'N' THEN 0 ELSE movmag.mm_fase END, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " movmag.mm_ultagg, " & CStrSQL(oApp.User.Nome) & ", " & _
                 " CASE WHEN movmag.mm_tipork = 'T' OR movmag.mm_tipork = 'U' THEN 'T' ELSE null END " & _
                 " FROM (((movmag INNER JOIN ttkeys ON movmag.codditt = ttkeys.codditt " & _
                 " AND movmag.mm_tipork = ttkeys.ke_tipork AND movmag.mm_anno = ttkeys.ke_anno " & _
                 " AND movmag.mm_serie = ttkeys.ke_serie AND movmag.mm_numdoc = ttkeys.ke_numdoc " & _
                 " AND movmag.mm_riga = ttkeys.ke_riga) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_conto = anagca.ac_conto) " & _
                 " INNER JOIN tabcaca ON ttkeys.ke_lead = tabcaca.tb_codcaca) " & _
                 " LEFT JOIN movmag AS mmT ON movmag.codditt = mmT.codditt AND movmag.mm_prtipo = mmT.mm_tipork " & _
                 " AND movmag.mm_pranno = mmT.mm_anno AND movmag.mm_prserie = mmT.mm_serie " & _
                 " AND movmag.mm_prnum = mmT.mm_numdoc AND movmag.mm_prriga = mmT.mm_riga " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '----------------------------------------------
        'se serve devo scrivere ance pria2comp
        strSQL = "bussp_aggpria2comp " & CStrSQL(strDitta) & ", " & lProgr.ToString & ", " & (lProgr + nRec - 1).ToString & ", " & TrovaNdec(0)
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If nRec > 0 Then

      '----------------------------------------------
      'carico da produzione: quantità prodotta e lavorazioni
      If strTipork = "T" Then
        If Not ScriviPriana2DaTestmag_Lavorazioni(strDitta, strTipork, nAnno, strSerie, lNumdoc, dbConn, lIITtkeys, _
                                                  nEscomp, bRiscriviSoloScarichieQtaProd, bEscludiLavorazTerziste) Then Return False
      End If

      '----------------------------------------------
      'scrivo le righe delle spese di piede
      If bRiscriviSoloScarichieQtaProd = False And bEscludiSpesePiede = False Then
        If Not ScriviPriana2DaTestmag_SpesePiede(strDitta, strTipork, nAnno, strSerie, lNumdoc, dbConn, lIITtkeys, nEscomp) Then Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScriviPriana2DaTestmag_Lavorazioni(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByRef dbConn As DbConnection, _
                                                   ByVal lIITtkeys As Integer, ByVal nEscomp As Integer, _
                                                   ByVal bRiscriviSoloScarichieQtaProd As Boolean, _
                                                   ByVal bEscludiLavorazTerziste As Boolean) As Boolean

    'per come deve essere redatta la registrazione vedi note in testa alla routine 'ScriviPriana2DaTestmag'

    Dim strSQL As String = ""
    Dim lMinProgr As Integer = 0
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Dim lProgr As Integer = 0
    Dim i As Integer = 0
    Try
      If bRiscriviSoloScarichieQtaProd Then GoTo QTA_PRODOTTA

      '----------------------------------------------
      'carico da produzione: lavorazioni
      'se produzione da terzista prendo tabcove e contoca di movmag T (tanto c'è una sola riga - per compatibilità con PNFA ed archivi già esistenti) altrimenti prendo tabcove e contoca da lavcent
      strSQL = "DELETE FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'in ke_tipo memorizzo se carico da prod terzista 'T' o interno 'I'
      strSQL = "INSERT INTO ttkeys (instid, codditt, ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_riga, " & _
               " ke_vers, ke_lead, ke_conto, ke_coddest, ke_tipo, ke_datreg, ke_olprogr, ke_numrata) " & _
               " SELECT " & lIITtkeys & ", movmag.codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, " & _
               " lce_rigaa, tb_codcacalv, " & _
               " CASE WHEN tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X' THEN tabcoveT.tb_concovag ELSE tabcove.tb_concovag END, " & _
               " CASE WHEN tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X' THEN mm_contocontr ELSE lce_contocontr END, " & _
               " CASE WHEN tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X' THEN 'T' ELSE 'I' END, tm_datdoc, tm_codcli, tabmaga.tb_codstab " & _
               " FROM ((((((((" & oCldBase.strJoinTestmagMovmag & ") " & _
               " INNER JOIN lavcent ON movmag.codditt = lavcent.codditt AND movmag.mm_tipork = lavcent.lce_tipork " & _
               " AND movmag.mm_anno = lavcent.lce_anno AND movmag.mm_serie = lavcent.lce_serie " & _
               " AND movmag.mm_numdoc = lavcent.lce_numdoc AND movmag.mm_riga = lavcent.lce_riga) " & _
               " INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum) " & _
               " INNER JOIN tabcaca ON tabcaum.tb_codcacalv = tabcaca.tb_codcaca) " & _
               " INNER JOIN tabcove ON lavcent.codditt = tabcove.codditt AND lavcent.lce_controp = tabcove.tb_codcove) " & _
               " INNER JOIN tabcove as tabcoveT ON movmag.codditt = tabcoveT.codditt AND movmag.mm_controp = tabcoveT.tb_codcove) " & _
               " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto) " & _
               " INNER JOIN tabmaga ON testmag.codditt = tabmaga.codditt AND testmag.tm_magimp = tabmaga.tb_codmaga) " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipork) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc & _
               " AND tb_codcacalv <> 0" & _
               " AND ((CASE WHEN tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X' THEN tabcoveT.tb_concove ELSE tabcove.tb_concove END = 0 " & _
               " AND CASE WHEN tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X' THEN tabcoveT.tb_concova ELSE tabcove.tb_concova END <> 0) OR an_flci <> ' ') " & _
               " AND (lce_valore <> 0 OR lce_valmo <> 0 OR lce_qtaes <> 0 OR lce_qtascart <> 0)"
      If bEscludiLavorazTerziste Then
        strSQL += " AND NOT (tabmaga.tb_flclavo = 'F' OR tabmaga.tb_flclavo = 'X')"
      End If
      nRec = oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If nRec > 0 Then
        'se ci sono righe da scrivere in priana2, devo calcolare il progressivo da memorizzare poi in priana2
        strSQL = "SELECT min(ke_progr) as progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        lMinProgr = NTSCInt(dttTmp.Rows(0)!progr)
        dttTmp.Clear()

        lProgr = LegNuma(strDitta, "PX", " ", 0, True, dbConn)      'primo progressivo da usare in priana2
        strSQL = "UPDATE tabnuma SET tb_numprog = " & lProgr + nRec & " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = 'PX'"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'inserisco la prima riga di PRIANA2:
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_idwbs, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT lavcent.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 " 1, " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_codcena ELSE mm_codcena END END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE CASE WHEN ke_tipo = 'I' THEN lce_codcfam ELSE mm_codcfam END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_commeca ELSE mm_commeca END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE CASE WHEN ke_tipo = 'I' THEN lce_subcommeca ELSE mm_subcommeca END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_pmtaskid ELSE mm_pmtaskid END END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE movmag.mm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " CASE WHEN tb_dqta = 'S' THEN ac_unmis ELSE ' ' END, " & _
                 " CASE WHEN tb_dqta = 'S' THEN lce_tempatte + lce_tempese ELSE 0 END, " & _
                 " CASE WHEN tb_dqta = 'S' THEN lce_tempatte + lce_tempese ELSE 0 END, " & _
                 " 'D', lce_valore + lce_valmo, lce_valore + lce_valmo, 2, 'M', " & _
                 " movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, " & _
                 " ke_datreg, movmag.mm_numdoc, movmag.mm_serie, ke_datreg, 0, 0, " & _
                 " movmag.mm_datini, movmag.mm_datfin, 'N', 0, ke_lead, lce_rigaa, " & _
                 " CASE WHEN ac_richarti = 'N' THEN ' ' ELSE movmag.mm_codart END, " & _
                 " CASE WHEN ac_richarti = 'N' THEN 0 ELSE movmag.mm_fase END, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " movmag.mm_ultagg, " & CStrSQL(oApp.User.Nome) & ", 'T' " & _
                 " FROM (((lavcent INNER JOIN ttkeys ON lavcent.codditt = ttkeys.codditt " & _
                 " AND lavcent.lce_tipork = ttkeys.ke_tipork AND lavcent.lce_anno = ttkeys.ke_anno " & _
                 " AND lavcent.lce_serie = ttkeys.ke_serie AND lavcent.lce_numdoc = ttkeys.ke_numdoc " & _
                 " AND lavcent.lce_riga = ttkeys.ke_riga AND lavcent.lce_rigaa = ttkeys.ke_vers) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_coddest = anagca.ac_conto) " & _
                 " INNER JOIN tabcaca ON ttkeys.ke_lead = tabcaca.tb_codcaca) " & _
                 " INNER JOIN movmag ON movmag.codditt = lavcent.codditt AND movmag.mm_tipork = lavcent.lce_tipork " & _
                 " AND movmag.mm_anno = lavcent.lce_anno AND movmag.mm_serie = lavcent.lce_serie " & _
                 " AND movmag.mm_numdoc = lavcent.lce_numdoc AND movmag.mm_riga = lavcent.lce_riga " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'adesso la seconda riga: la query è come la prima, solo che anagca è in join con 
        'ke_conto = tb_acontoca di tabcaca
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_idwbs, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT lavcent.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 " 2, " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_codcena ELSE mm_codcena END END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE CASE WHEN ke_tipo = 'I' THEN lce_codcfam ELSE mm_codcfam END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_commeca ELSE mm_commeca END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE CASE WHEN ke_tipo = 'I' THEN lce_subcommeca ELSE mm_subcommeca END END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE CASE WHEN ke_tipo = 'I' THEN lce_pmtaskid ELSE mm_pmtaskid END END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE movmag.mm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " CASE WHEN tb_aqta = 'S' THEN ac_unmis ELSE ' ' END, " & _
                 " CASE WHEN tb_aqta = 'S' THEN (lce_tempatte + lce_tempese) * -1 ELSE 0 END, " & _
                 " CASE WHEN tb_aqta = 'S' THEN lce_tempatte + lce_tempese ELSE 0 END, " & _
                 " 'A', (lce_valore + lce_valmo) * -1, (lce_valore + lce_valmo), 1, 'M', " & _
                 " movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, " & _
                 " ke_datreg, movmag.mm_numdoc, movmag.mm_serie, ke_datreg, 0, 0, " & _
                 " movmag.mm_datini, movmag.mm_datfin, 'N', 0, ke_lead, lce_rigaa, " & _
                 " CASE WHEN ac_richarti = 'N' THEN ' ' ELSE movmag.mm_codart END, " & _
                 " CASE WHEN ac_richarti = 'N' THEN 0 ELSE movmag.mm_fase END, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " movmag.mm_ultagg, " & CStrSQL(oApp.User.Nome) & ", 'T' " & _
                 " FROM (((lavcent INNER JOIN ttkeys ON lavcent.codditt = ttkeys.codditt " & _
                 " AND lavcent.lce_tipork = ttkeys.ke_tipork AND lavcent.lce_anno = ttkeys.ke_anno " & _
                 " AND lavcent.lce_serie = ttkeys.ke_serie AND lavcent.lce_numdoc = ttkeys.ke_numdoc " & _
                 " AND lavcent.lce_riga = ttkeys.ke_riga AND lavcent.lce_rigaa = ttkeys.ke_vers) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_conto = anagca.ac_conto) " & _
                 " INNER JOIN tabcaca ON ttkeys.ke_lead = tabcaca.tb_codcaca) " & _
                 " INNER JOIN movmag ON movmag.codditt = lavcent.codditt AND movmag.mm_tipork = lavcent.lce_tipork " & _
                 " AND movmag.mm_anno = lavcent.lce_anno AND movmag.mm_serie = lavcent.lce_serie " & _
                 " AND movmag.mm_numdoc  = lavcent.lce_numdoc AND movmag.mm_riga = lavcent.lce_riga " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '----------------------------------------------
        'se serve devo scrivere ance pria2comp
        strSQL = "bussp_aggpria2comp " & CStrSQL(strDitta) & ", " & lProgr.ToString & ", " & (lProgr + nRec - 1).ToString & ", " & TrovaNdec(0)
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If nRec > 0 Then



QTA_PRODOTTA:
      '----------------------------------------------
      'carico da produzione: quantità prodotta: SEMPRE E SOLO UNA RIGA IN DARE
      strSQL = "DELETE FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'ottengo causale e conto di ca da utilizzare
      strSQL = "SELECT tb_cauqtapro FROM tabpecx WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "INSERT INTO ttkeys (instid, codditt, ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_riga, " & _
               " ke_vers, ke_lead, ke_conto, ke_coddest, ke_datreg, ke_olprogr, ke_numrata) " & _
               " SELECT " & lIITtkeys & ", movmag.codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, " & _
               " 0, " & NTSCInt(dttTmp.Rows(0)!tb_cauqtapro) & ", 0, tabcove.tb_concova2, " & _
               " tm_datdoc, tm_codcli, tabmaga.tb_codstab " & _
               " FROM (((((" & oCldBase.strJoinTestmagMovmag & ") " & _
               " INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum) " & _
               " INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) " & _
               " INNER JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove) " & _
               " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto) " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipork) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc & _
               " AND ((tb_concove = 0 AND tb_concova2 <> 0) OR an_flci <> ' ') " & _
               " AND tb_codcacalv <> 0 " & _
               " AND tb_testci <> ' ' " & _
               " AND mm_quant <> 0"
      nRec = oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      dttTmp.Clear()

      If nRec > 0 Then
        'se ci sono righe da scrivere in priana2, devo calcolare il progressivo da memorizzare poi in priana2
        strSQL = "SELECT min(ke_progr) as progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        lMinProgr = NTSCInt(dttTmp.Rows(0)!progr)
        dttTmp.Clear()

        lProgr = LegNuma(strDitta, "PX", " ", 0, True, dbConn)      'primo progressivo da usare in priana2
        strSQL = "UPDATE tabnuma SET tb_numprog = " & lProgr + nRec & " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = 'PX'"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_idwbs, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT movmag.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 " 1, " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE mm_codcena END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE mm_codcfam END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE mm_commeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE mm_subcommeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE mm_pmtaskid END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE mm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " mm_unmis, mm_quant, mm_quant, 'D', 0, 0, 0, 'M', " & _
                 " mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, " & _
                 " ke_datreg, mm_numdoc, mm_serie, ke_datreg, 0, 0, " & _
                 " mm_datini, mm_datfin, 'N', 0, ke_lead, 0, " & _
                 " mm_codart, mm_fase, CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " movmag.mm_ultagg, " & CStrSQL(oApp.User.Nome) & ", 'T' " & _
                 " FROM ((movmag INNER JOIN ttkeys ON movmag.codditt = ttkeys.codditt " & _
                 " AND movmag.mm_tipork = ttkeys.ke_tipork AND movmag.mm_anno = ttkeys.ke_anno " & _
                 " AND movmag.mm_serie = ttkeys.ke_serie AND movmag.mm_numdoc = ttkeys.ke_numdoc " & _
                 " AND movmag.mm_riga = ttkeys.ke_riga) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_coddest = anagca.ac_conto) " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'non è gestito a valore, per cui non ha senso scrivere pria2comp
      End If    'If nRec > 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScriviPriana2DaTestmag_SpesePiede(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByRef dbConn As DbConnection, _
                                                   ByVal lIITtkeys As Integer, ByVal nEscomp As Integer) As Boolean

    'per come deve essere redatta la registrazione vedi note in testa alla routine 'ScriviPriana2DaTestmag'

    'occhio: gli importi di tipo incasso/pagamento, cliente/fornitore generico, abbuoni, omaggi vengono rilevati sempre e solo da pnfa

    Dim strSQL As String = ""
    Dim lMinProgr As Integer = 0
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Dim dttCove As New DataTable
    Dim lProgr As Integer = 0
    Dim i As Integer = 0
    Dim bDocEmesso As Boolean = False
    Dim nCodcove As Integer = 0
    Dim nGiro As Integer = 0

    Dim bUsaAggContropAnagra As Boolean = False      'se TRUE, nell contropartite lette da BNVEPEVE e BNMGPEAC viene aggiunto il campo anagra.an_agcontrop e se il sottocont esiste viene preo quello, altrienti quello senza an_agcontrop
    Dim dsCove As New DataSet
    Dim nAggControp As Integer = 0

    Try
      '----------------------------------------------
      'scrivo le righe delle spese di piede

      'se impostata l'opzione di reg., aggiungo alla contropartita delle spese di piede l'aggiunta contropartita di anagra
      'se in tabcove c'è la contropartita bene, altrimenti uso quella di peve/peac
      bUsaAggContropAnagra = CBool(GetSettingBus("BSVEPNFA", "OPZIONI", ".", "UsaAggContropAnagra ", "0", " ", "0"))
      If bUsaAggContropAnagra Then
        LeggiTabellaSemplice(strDitta, "TABCOVE", dsCove)
        strSQL = "SELECT an_agcontrop " & _
                 " FROM anagra INNER JOIN testmag ON anagra.codditt = testmag.codditt AND anagra.an_conto = testmag.tm_conto" & _
                 " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(strTipork) & _
                 " AND tm_anno = " & nAnno & _
                 " AND tm_serie = " & CStrSQL(strSerie) & _
                 " AND tm_numdoc = " & lNumdoc
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then nAggControp = NTSCInt(dttTmp.Rows(0)!an_agcontrop)
        dttTmp.Clear()
      End If

      If strTipork = "B" Or strTipork = "Z" Then
        bDocEmesso = True
        strSQL = "SELECT * FROM tabpeve WHERE tb_codpeve = 1"
      Else
        'può essere solo M o T
        bDocEmesso = False
        strSQL = "SELECT * FROM tabpeac WHERE tb_codpeac = 1"
      End If

      'ottengo causale e conto di ca da utilizzare
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For nGiro = 0 To 7
        'ripeto il tutto per ogni tipo di spesa
        Select Case nGiro
          Case 0  'spese trasporto
            If bDocEmesso Then
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_contrriac)
            Else
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_acontrriac)
            End If
          Case 1 'spese incasso
            If bDocEmesso Then
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_contrriin)
            Else
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_acontrriin)
            End If
          Case 2  'spese imballo
            If bDocEmesso Then
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_contrimba)
            Else
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_acontrimba)
            End If
          Case 3 : GoTo PASSA   'omaggi                     gestito solo in pnfa
          Case 4 : GoTo PASSA   'abbuoni                    gestito solo in pnfa
          Case 5 : GoTo PASSA   'importo incassato/pagato   gestito solo in pnfa
          Case 6 : GoTo PASSA   'cliente/fornitore          gestito solo in pnfa
          Case 7  'bolli
            If bDocEmesso Then
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_contrribo)
            Else
              nCodcove = NTSCInt(dttTmp.Rows(0)!tb_acontrribo)
            End If
        End Select

        If bUsaAggContropAnagra Then
          nCodcove = ScriviPriana2DaTestmag_CoveAggControp(dsCove, nCodcove, nAggControp)
        End If

        strSQL = "SELECT tb_concove, tb_concova, tb_concovag, isnull(an_flci, ' ') as xx_flci " & _
                 " FROM tabcove LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto " & _
                 " WHERE tabcove.codditt = " & CStrSQL(strDitta) & " AND tb_codcove = " & nCodcove
        dttCove = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'negli entity di veboll/gsor ho già controllato che se c'è un importo da rilevare in CA2 il record in tabcove deve esistere e deve essere compilato correttamente!!!
        If dttCove.Rows.Count > 0 Then
          If (NTSCInt(dttCove.Rows(0)!tb_concove) = 0 And NTSCInt(dttCove.Rows(0)!tb_concova) <> 0) Or NTSCStr(dttCove.Rows(0)!xx_flci).Trim <> "" Then
            strSQL = "INSERT INTO ttkeys (instid, codditt, ke_tipork, ke_anno, ke_serie, ke_numdoc, ke_riga, " & _
                     " ke_vers, ke_lead, ke_conto, ke_coddest, ke_datreg, ke_olprogr, ke_numrata, ke_importo) " & _
                     " SELECT " & lIITtkeys & ", testmag.codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, 0, " & _
                     " 0, tb_codcacadd, " & NTSCInt(dttCove.Rows(0)!tb_concovag) & ", " & NTSCInt(dttCove.Rows(0)!tb_concova) & ", " & _
                     " tm_datdoc, tm_codcli, tabmaga.tb_codstab, "
            Select Case nGiro
              Case 0 : strSQL += " tm_speacc "
              Case 1 : strSQL += " tm_speinc "
              Case 2 : strSQL += " tm_speimb "
              Case 7 : strSQL += " tm_bolli "
            End Select
            strSQL += " FROM ((testmag INNER JOIN tabmaga ON testmag.codditt = tabmaga.codditt AND testmag.tm_magaz = tabmaga.tb_codmaga) " & _
                     " INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf) " & _
                     " INNER JOIN tabcaca ON tabtpbf.tb_codcacadd = tabcaca.tb_codcaca " & _
                     " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                     " AND tm_tipork = " & CStrSQL(strTipork) & _
                     " AND tm_anno = " & nAnno & _
                     " AND tm_serie = " & CStrSQL(strSerie) & _
                     " AND tm_numdoc = " & lNumdoc & _
                     " AND tb_codcacadd <> 0"
            Select Case nGiro
              Case 0 : strSQL += " AND tm_speacc <> 0"
              Case 1 : strSQL += " AND tm_speinc <> 0"
              Case 2 : strSQL += " AND tm_speimb <> 0"
              Case 7 : strSQL += " AND tm_bolli <> 0"
            End Select
            dttCove.Clear()
            nRec += oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If lConto <> 0 Then
        End If    'If dttCove.Rows.Count > 0 Then

PASSA:
      Next    'For nGiro = 0 To 2
      dttTmp.Clear()


      If nRec <> 0 Then
        'se ci sono righe da scrivere in priana2, devo calcolare il progressivo da memorizzare poi in priana2
        strSQL = "SELECT min(ke_progr) as progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITtkeys
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        lMinProgr = NTSCInt(dttTmp.Rows(0)!progr)
        dttTmp.Clear()

        lProgr = LegNuma(strDitta, "PX", " ", 0, True, dbConn)      'primo progressivo da usare in priana2
        strSQL = "UPDATE tabnuma SET tb_numprog = " & lProgr + nRec & " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = 'PX'"
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'inserisco la prima riga di PRIANA2:
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT testmag.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 IIf(bDocEmesso, 2, 1).ToString & " , " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE tm_codcena END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE tm_codcfam END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE tm_commeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE tm_subcommeca END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE tm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " ' ', ' ',  ' ', " & _
                 IIf(bDocEmesso, "'A'", "'D'").ToString & ", " & _
                 " ke_importo * " & IIf(bDocEmesso, "-1", "1").ToString & ", " & _
                 " ke_importo, " & IIf(bDocEmesso, 1, 2).ToString & ", 'M', " & _
                 " tm_tipork, tm_anno, tm_serie, tm_numdoc, 0, " & _
                 " ke_datreg, tm_numdoc, tm_serie, ke_datreg, 0, 0, " & _
                 " ke_datreg, ke_datreg, 'N', 0, ke_lead, 0, ' ', 0, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " tm_ultagg, " & CStrSQL(oApp.User.Nome) & ", " & _
                 " CASE WHEN tm_tipork = 'T' OR tm_tipork = 'U' THEN 'T' ELSE null END " & _
                 " FROM ((testmag INNER JOIN ttkeys ON testmag.codditt = ttkeys.codditt " & _
                 " AND testmag.tm_tipork = ttkeys.ke_tipork AND testmag.tm_anno = ttkeys.ke_anno " & _
                 " AND testmag.tm_serie = ttkeys.ke_serie AND testmag.tm_numdoc = ttkeys.ke_numdoc) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_coddest = anagca.ac_conto) " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------
        'adesso la seconda riga: la query è come la prima, solo che anagca è in join con 
        'ke_conto = tb_acontoca di tabcaca ed invertito i dati di bDocumentoEmesso
        strSQL = "INSERT INTO PRIANA2 (codditt, pa2_progr, pa2_rigarg, pa2_escomp, pa2_conto, pa2_codcena, " & _
                 " pa2_codcfam, pa2_commeca, pa2_subcommeca, pa2_coddivi, pa2_codstab, pa2_unmis, pa2_quant, pa2_quantda, " & _
                 " pa2_darave, pa2_importo, pa2_importoda, pa2_idrigacontrop, pa2_origine, " & _
                 " pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_mmriga, pa2_datdoc, pa2_numdoc, " & _
                 " pa2_alfdoc, pa2_datreg, pa2_numreg, pa2_riga, pa2_datini, pa2_datfin, " & _
                 " pa2_integr, pa2_contocg, pa2_codcaca, pa2_lcerigaa, " & _
                 " pa2_codart, pa2_fase, pa2_codcli, pa2_ultagg, pa2_opnome, pa2_mmtiporkt) " & _
                 "SELECT testmag.codditt, ke_progr - " & lMinProgr & " + " & lProgr & ", " & _
                 IIf(bDocEmesso, 1, 2).ToString & " , " & nEscomp & ", ac_conto, " & _
                 " CASE WHEN ac_richcena = 'N' THEN 0 ELSE tm_codcena END, " & _
                 " CASE WHEN ac_richcfam = 'N' THEN ' ' ELSE tm_codcfam END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN 0 ELSE tm_commeca END, " & _
                 " CASE WHEN ac_richcomm = 'N' THEN ' ' ELSE tm_subcommeca END, " & _
                 " CASE WHEN ac_richdivi = 'N' THEN 0 ELSE tm_coddivi END, " & _
                 " CASE WHEN ac_richstab = 'N' THEN 0 ELSE ke_numrata END, " & _
                 " ' ', ' ',  ' ', " & _
                 IIf(bDocEmesso, "'D'", "'A'").ToString & ", " & _
                 " ke_importo * " & IIf(bDocEmesso, "1", "-1").ToString & ", " & _
                 " ke_importo, " & IIf(bDocEmesso, 2, 1).ToString & ", 'M', " & _
                 " tm_tipork, tm_anno, tm_serie, tm_numdoc, 0, " & _
                 " ke_datreg, tm_numdoc, tm_serie, ke_datreg, 0, 0, " & _
                 " ke_datreg, ke_datreg, 'N', 0, ke_lead, 0, ' ', 0, " & _
                 " CASE WHEN ac_richcli = 'N' THEN 0 ELSE ke_olprogr END, " & _
                 " tm_ultagg, " & CStrSQL(oApp.User.Nome) & ", " & _
                 " CASE WHEN tm_tipork = 'T' OR tm_tipork = 'U' THEN 'T' ELSE null END " & _
                 " FROM ((testmag INNER JOIN ttkeys ON testmag.codditt = ttkeys.codditt " & _
                 " AND testmag.tm_tipork = ttkeys.ke_tipork AND testmag.tm_anno = ttkeys.ke_anno " & _
                 " AND testmag.tm_serie = ttkeys.ke_serie AND testmag.tm_numdoc = ttkeys.ke_numdoc) " & _
                 " INNER JOIN anagca ON ttkeys.codditt = anagca.codditt AND ttkeys.ke_conto = anagca.ac_conto) " & _
                 " WHERE ttkeys.codditt = " & CStrSQL(strDitta) & _
                 " AND ttkeys.instid = " & lIITtkeys
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If nRec <> 0 Then

      'non sono gestite i conti a periodo di competenza economica: non scrivo mai pria2comp

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttCove.Clear()
    End Try
  End Function

  Public Overridable Function ScriviPriana2DaTestmag_CoveAggControp(ByRef dsCove As DataSet, ByVal nCodcove As Integer, ByVal nAggControp As Integer) As Integer
    ScriviPriana2DaTestmag_CoveAggControp = nCodcove
    Try
      If nAggControp = 0 Then Return nCodcove
      If nCodcove = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Rows.Count = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Select("tb_codcove = " & nAggControp + nCodcove).Length > 0 Then Return nAggControp + nCodcove
      Return nCodcove

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------	
    End Try
  End Function
#End Region


  Public Overridable Function AggTotaliBudgetCa2(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nCodgrua As Integer, ByVal nEscompg As Integer, _
                                      ByVal nRev As Integer, ByVal nCodtcdc As Integer, _
                                      ByVal strCoddica As String, ByVal strFiltroBudget As String) As Boolean
    'strFiltroBudget contiene, a seconda della chimata, il numero del centro da elaborare, oppure il cod. famiglia, num commessa, ...
    'questa routine viene chiamata da BDCXBUDG, BDCXRIBA, BECXFORM
    Dim strSQL As String = ""
    Dim strKeys As String = ""
    Dim strKeysC As String = ""
    Dim dttSchema, dttTmp As New DataTable
    Try
      '-----------------------------------------
      'calcolo le colonne di tipo 'totale': ottengo le righe dello schema budget in ordine corretto,
      'poi memorizzo le righe fino al totalizzatore, per ogni totalizzatore faccio una sum con tt_riga in (....) 
      'poi faccio update di riga totalizzatore.
      'il tutto solo sui totali (tt_conto = 0)
      'CALCOLO SOLO I TOTALI DI PREVENTIVO, NON IL CONSUNTIVO. IL CONSUNTIVO VIENE AGGIORNATO DA STAMPA BUDGET/CONSUNTIVI!!!
      strSQL = "SELECT pb_riga, pb_tipovoc, pb_tipoconsu, pb_darave " & _
               " FROM caschebud INNER JOIN tabscbg ON caschebud.pb_codscbg = tabscbg.tb_codscbg " & _
               " WHERE pb_tipork = " & CStrSQL(strTipork) & _
               " AND pb_codgrua = " & nCodgrua & _
               " AND pb_escompg = " & nEscompg & _
               " AND pb_rev = " & nRev & _
               " AND pb_codtcdc = " & nCodtcdc & _
               " AND pb_coddica = " & CStrSQL(strCoddica) & _
               " ORDER BY tb_ordin, pb_ordin, pb_riga"
      dttSchema = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttSchema.Rows
        'CALCOLO I TOTALI A PREVENTIVO
        If NTSCStr(dtrT!pb_tipovoc) <> "T" And NTSCStr(dtrT!pb_tipovoc) <> "S" Then
          strKeys += dtrT!pb_riga.ToString & ", "
        Else
          'ho le chiavi delle righe che devono essere sommate: 
          'calcolo i totali raggruppati per centro/linea/commessa, ... e facco le update
          If strKeys.Trim <> "" Then
            strSQL = "SELECT SUM(CASE WHEN pb_darave = 'A' THEN -1 * bu_val ELSE bu_val END) as bu_val, SUM(bu_qta) as bu_qta, " & _
                     " bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                     " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno " & _
                     " FROM cabudget LEFT JOIN caschebud ON pb_tipork = bu_tipork AND pb_codgrua = bu_codgrua AND pb_riga = bu_riga " & _
                     " AND pb_escompg = bu_escompg AND pb_rev = bu_rev AND pb_codtcdc = bu_codtcdc AND pb_coddica = bu_coddica " & _
                     " WHERE bu_riga IN (" & strKeys.Substring(0, strKeys.Length - 2) & ")" & _
                     " AND bu_tipork = " & CStrSQL(strTipork) & _
                     " AND bu_codgrua = " & nCodgrua & _
                     " AND bu_escompg = " & nEscompg & _
                     " AND bu_rev = " & nRev & _
                     " AND bu_codtcdc = " & nCodtcdc & _
                     " AND bu_coddica = " & CStrSQL(strCoddica)
            If strFiltroBudget <> "" Then
              Select Case strTipork
                Case "E" : strSQL += " AND bu_codcena = " & strFiltroBudget
                Case "L" : strSQL += " AND bu_codcfam = " & CStrSQL(strFiltroBudget)
                Case "C" : strSQL += " AND bu_commeca = " & strFiltroBudget
                Case "D" : strSQL += " AND bu_coddivi = " & strFiltroBudget
                Case "S" : strSQL += " AND bu_codstab = " & strFiltroBudget
                Case "A" : strSQL += " AND bu_codart = " & CStrSQL(strFiltroBudget)
                Case "K" : strSQL += " AND bu_codcli = " & strFiltroBudget
                Case "Y" : strSQL += " AND codditt = " & CStrSQL(strFiltroBudget)
                Case "Z" : strSQL += " AND bu_coddicv = " & CStrSQL(strFiltroBudget)
              End Select
            End If
            strSQL += " GROUP BY bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                     " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno "
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            For Each dtrT1 As DataRow In dttTmp.Rows
              strSQL = "UPDATE cabudget SET " & _
                       "bu_val = CASE WHEN " & CStrSQL(dtrT!pb_darave) & " = 'A' THEN -1 * " & CDblSQL(NTSCDec(dtrT1!bu_val)) & _
                       " ELSE " & CDblSQL(NTSCDec(dtrT1!bu_val)) & " END, " & _
                       "bu_qta = " & CDblSQL(NTSCDec(dtrT1!bu_qta)) & _
                       " WHERE bu_codcena = " & dtrT1!bu_codcena.ToString & _
                       " AND bu_codcfam = " & CStrSQL(dtrT1!bu_codcfam) & _
                       " AND bu_commeca = " & dtrT1!bu_commeca.ToString & _
                       " AND bu_coddivi = " & dtrT1!bu_coddivi.ToString & _
                       " AND bu_codstab = " & dtrT1!bu_codstab.ToString & _
                       " AND bu_codart = " & CStrSQL(dtrT1!bu_codart) & _
                       " AND bu_fase = " & dtrT1!bu_fase.ToString & _
                       " AND bu_codcli = " & dtrT1!bu_codcli.ToString & _
                       " AND codditt = " & CStrSQL(dtrT1!codditt) & _
                       " AND bu_coddica = " & CStrSQL(dtrT1!bu_coddica) & _
                       " AND bu_coddicv = " & CStrSQL(dtrT1!bu_coddicv) & _
                       " AND bu_codgrua = " & dtrT1!bu_codgrua.ToString & _
                       " AND bu_riga = " & dtrT!pb_riga.ToString & _
                       " AND bu_mese = " & dtrT1!bu_mese.ToString & _
                       " AND bu_anno = " & dtrT1!bu_anno.ToString
              oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Next    'For Each dtrT1 As DataRow In dttTmp.Rows
            dttTmp.Clear()
          End If    'If strKeys.Trim <> "" Then
        End If    'If NTSCStr(dtrT!pb_tipoconsu) <> "T" And NTSCStr(dtrT!pb_tipoconsu) <> "S" Then

        'CALCOLO I TOTALI A CONSUNTIVO
        If NTSCStr(dtrT!pb_tipoconsu) <> "T" And NTSCStr(dtrT!pb_tipoconsu) <> "S" Then
          strKeysC += dtrT!pb_riga.ToString & ", "
        Else
          'ho le chiavi delle righe che devono essere sommate: 
          'calcolo i totali raggruppati per centro/linea/commessa, ... e facco le update
          If strKeysC.Trim <> "" Then
            strSQL = "SELECT SUM(CASE WHEN pb_darave = 'A' THEN -1 * bu_valcon ELSE bu_valcon END) as bu_valcon, SUM(bu_qtacon) as bu_qtacon, " & _
                     " bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                     " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno " & _
                     " FROM cabudget LEFT JOIN caschebud ON pb_tipork = bu_tipork AND pb_codgrua = bu_codgrua AND pb_riga = bu_riga " & _
                     " AND pb_escompg = bu_escompg AND pb_rev = bu_rev AND pb_codtcdc = bu_codtcdc AND pb_coddica = bu_coddica " & _
                     " WHERE bu_riga IN (" & strKeysC.Substring(0, strKeysC.Length - 2) & ")" & _
                     " AND bu_tipork = " & CStrSQL(strTipork) & _
                     " AND bu_codgrua = " & nCodgrua & _
                     " AND bu_escompg = " & nEscompg & _
                     " AND bu_rev = " & nRev & _
                     " AND bu_codtcdc = " & nCodtcdc & _
                     " AND bu_coddica = " & CStrSQL(strCoddica)
            If strFiltroBudget <> "" Then
              Select Case strTipork
                Case "E" : strSQL += " AND bu_codcena = " & strFiltroBudget
                Case "L" : strSQL += " AND bu_codcfam = " & CStrSQL(strFiltroBudget)
                Case "C" : strSQL += " AND bu_commeca = " & strFiltroBudget
                Case "D" : strSQL += " AND bu_coddivi = " & strFiltroBudget
                Case "S" : strSQL += " AND bu_codstab = " & strFiltroBudget
                Case "A" : strSQL += " AND bu_codart = " & CStrSQL(strFiltroBudget)
                Case "K" : strSQL += " AND bu_codcli = " & strFiltroBudget
                Case "Y" : strSQL += " AND codditt = " & CStrSQL(strFiltroBudget)
                Case "Z" : strSQL += " AND bu_coddicv = " & CStrSQL(strFiltroBudget)
              End Select
            End If
            strSQL += " GROUP BY bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                     " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno "
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            For Each dtrT1 As DataRow In dttTmp.Rows
              strSQL = "UPDATE cabudget SET " & _
                       "bu_valcon = CASE WHEN " & CStrSQL(dtrT!pb_darave) & " = 'A' THEN -1 * " & CDblSQL(NTSCDec(dtrT1!bu_valcon)) & _
                       " ELSE " & CDblSQL(NTSCDec(dtrT1!bu_valcon)) & " END, " & _
                       "bu_qtacon = " & CDblSQL(NTSCDec(dtrT1!bu_qtacon)) & _
                       " WHERE bu_codcena = " & dtrT1!bu_codcena.ToString & _
                       " AND bu_codcfam = " & CStrSQL(dtrT1!bu_codcfam) & _
                       " AND bu_commeca = " & dtrT1!bu_commeca.ToString & _
                       " AND bu_coddivi = " & dtrT1!bu_coddivi.ToString & _
                       " AND bu_codstab = " & dtrT1!bu_codstab.ToString & _
                       " AND bu_codart = " & CStrSQL(dtrT1!bu_codart) & _
                       " AND bu_fase = " & dtrT1!bu_fase.ToString & _
                       " AND bu_codcli = " & dtrT1!bu_codcli.ToString & _
                       " AND codditt = " & CStrSQL(dtrT1!codditt) & _
                       " AND bu_coddica = " & CStrSQL(dtrT1!bu_coddica) & _
                       " AND bu_coddicv = " & CStrSQL(dtrT1!bu_coddicv) & _
                       " AND bu_codgrua = " & dtrT1!bu_codgrua.ToString & _
                       " AND bu_riga = " & dtrT!pb_riga.ToString & _
                       " AND bu_mese = " & dtrT1!bu_mese.ToString & _
                       " AND bu_anno = " & dtrT1!bu_anno.ToString
              oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Next    'For Each dtrT1 As DataRow In dttTmp.Rows
            dttTmp.Clear()
          End If    'If strKeysC.Trim <> "" Then
        End If    'If NTSCStr(dtrT!pb_tipoconsu) <> "T" And NTSCStr(dtrT!pb_tipoconsu) <> "S" Then

      Next    'For Each dtrT As DataRow In dttSchema.Rows
      dttSchema.Clear()

      '----------------
      'adesso i totali di sezione per dati a PREVENTIVO
      strSQL = "SELECT pb_codscbg, pb_riga, pb_darave " & _
               " FROM caschebud " & _
               " WHERE pb_tipork = " & CStrSQL(strTipork) & _
               " AND pb_codgrua = " & nCodgrua & _
               " AND pb_escompg = " & nEscompg & _
               " AND pb_rev = " & nRev & _
               " AND pb_codtcdc = " & nCodtcdc & _
               " AND pb_coddica = " & CStrSQL(strCoddica) & _
               " AND pb_tipovoc = 'S'" & _
               " ORDER BY pb_codscbg, pb_riga"
      dttSchema = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttSchema.Rows
        strSQL = "SELECT SUM(CASE WHEN pb_darave = 'A' THEN -1 * bu_val ELSE bu_val END) as bu_val, SUM(bu_qta) as bu_qta, " & _
                 " bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                 " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno " & _
                 " FROM cabudget LEFT JOIN caschebud ON pb_tipork = bu_tipork AND pb_codgrua = bu_codgrua AND pb_riga = bu_riga " & _
                 " AND pb_escompg = bu_escompg AND pb_rev = bu_rev AND pb_codtcdc = bu_codtcdc AND pb_coddica = bu_coddica " & _
                 " WHERE pb_tipovoc <> 'S' AND pb_tipovoc <> 'T'" & _
                 " AND bu_tipork = " & CStrSQL(strTipork) & _
                 " AND bu_codgrua = " & nCodgrua & _
                 " AND bu_escompg = " & nEscompg & _
                 " AND bu_rev = " & nRev & _
                 " AND bu_codtcdc = " & nCodtcdc & _
                 " AND bu_coddica = " & CStrSQL(strCoddica) & _
                 " AND pb_codscbg = " & NTSCInt(dtrT!pb_codscbg).ToString
        If strFiltroBudget <> "" Then
          Select Case strTipork
            Case "E" : strSQL += " AND bu_codcena = " & strFiltroBudget
            Case "L" : strSQL += " AND bu_codcfam = " & CStrSQL(strFiltroBudget)
            Case "C" : strSQL += " AND bu_commeca = " & strFiltroBudget
            Case "D" : strSQL += " AND bu_coddivi = " & strFiltroBudget
            Case "S" : strSQL += " AND bu_codstab = " & strFiltroBudget
            Case "A" : strSQL += " AND bu_codart = " & CStrSQL(strFiltroBudget)
            Case "K" : strSQL += " AND bu_codcli = " & strFiltroBudget
            Case "Y" : strSQL += " AND codditt = " & CStrSQL(strFiltroBudget)
            Case "Z" : strSQL += " AND bu_coddicv = " & CStrSQL(strFiltroBudget)
          End Select
        End If
        strSQL += " GROUP BY bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
             " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno "
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrT1 As DataRow In dttTmp.Rows
          strSQL = "UPDATE cabudget SET " & _
                   "bu_val = CASE WHEN " & CStrSQL(dtrT!pb_darave) & " = 'A' THEN -1 * " & CDblSQL(NTSCDec(dtrT1!bu_val)) & _
                   " ELSE " & CDblSQL(NTSCDec(dtrT1!bu_val)) & " END, " & _
                   "bu_qta = " & CDblSQL(NTSCDec(dtrT1!bu_qta)) & _
                   " WHERE bu_codcena = " & dtrT1!bu_codcena.ToString & _
                   " AND bu_codcfam = " & CStrSQL(dtrT1!bu_codcfam) & _
                   " AND bu_commeca = " & dtrT1!bu_commeca.ToString & _
                   " AND bu_coddivi = " & dtrT1!bu_coddivi.ToString & _
                   " AND bu_codstab = " & dtrT1!bu_codstab.ToString & _
                   " AND bu_codart = " & CStrSQL(dtrT1!bu_codart) & _
                   " AND bu_fase = " & dtrT1!bu_fase.ToString & _
                   " AND bu_codcli = " & dtrT1!bu_codcli.ToString & _
                   " AND codditt = " & CStrSQL(dtrT1!codditt) & _
                   " AND bu_coddica = " & CStrSQL(dtrT1!bu_coddica) & _
                   " AND bu_coddicv = " & CStrSQL(dtrT1!bu_coddicv) & _
                   " AND bu_codgrua = " & dtrT1!bu_codgrua.ToString & _
                   " AND bu_riga = " & dtrT!pb_riga.ToString & _
                   " AND bu_mese = " & dtrT1!bu_mese.ToString & _
                   " AND bu_anno = " & dtrT1!bu_anno.ToString
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
        dttTmp.Clear()
      Next    'For Each dtrT As DataRow In dttSchema.Rows
      dttSchema.Clear()

      '----------------
      'adesso i totali di sezione per dati a CONSUNTIVO
      strSQL = "SELECT pb_codscbg, pb_riga, pb_darave " & _
               " FROM caschebud " & _
               " WHERE pb_tipork = " & CStrSQL(strTipork) & _
               " AND pb_codgrua = " & nCodgrua & _
               " AND pb_escompg = " & nEscompg & _
               " AND pb_rev = " & nRev & _
               " AND pb_codtcdc = " & nCodtcdc & _
               " AND pb_coddica = " & CStrSQL(strCoddica) & _
               " AND pb_tipoconsu = 'S'" & _
               " ORDER BY pb_codscbg, pb_riga"
      dttSchema = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttSchema.Rows
        strSQL = "SELECT SUM(CASE WHEN pb_darave = 'A' THEN -1 * bu_val ELSE bu_valcon END) as bu_valcon, SUM(bu_qtacon) as bu_qtacon, " & _
                 " bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
                 " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno " & _
                 " FROM cabudget LEFT JOIN caschebud ON pb_tipork = bu_tipork AND pb_codgrua = bu_codgrua AND pb_riga = bu_riga " & _
                 " AND pb_escompg = bu_escompg AND pb_rev = bu_rev AND pb_codtcdc = bu_codtcdc AND pb_coddica = bu_coddica " & _
                 " WHERE pb_tipoconsu <> 'S' AND pb_tipoconsu <> 'T'" & _
                 " AND bu_tipork = " & CStrSQL(strTipork) & _
                 " AND bu_codgrua = " & nCodgrua & _
                 " AND bu_escompg = " & nEscompg & _
                 " AND bu_rev = " & nRev & _
                 " AND bu_codtcdc = " & nCodtcdc & _
                 " AND bu_coddica = " & CStrSQL(strCoddica) & _
                 " AND pb_codscbg = " & NTSCInt(dtrT!pb_codscbg).ToString
        If strFiltroBudget <> "" Then
          Select Case strTipork
            Case "E" : strSQL += " AND bu_codcena = " & strFiltroBudget
            Case "L" : strSQL += " AND bu_codcfam = " & CStrSQL(strFiltroBudget)
            Case "C" : strSQL += " AND bu_commeca = " & strFiltroBudget
            Case "D" : strSQL += " AND bu_coddivi = " & strFiltroBudget
            Case "S" : strSQL += " AND bu_codstab = " & strFiltroBudget
            Case "A" : strSQL += " AND bu_codart = " & CStrSQL(strFiltroBudget)
            Case "K" : strSQL += " AND bu_codcli = " & strFiltroBudget
            Case "Y" : strSQL += " AND codditt = " & CStrSQL(strFiltroBudget)
            Case "Z" : strSQL += " AND bu_coddicv = " & CStrSQL(strFiltroBudget)
          End Select
        End If
        strSQL += " GROUP BY bu_codcena, bu_codcfam, bu_commeca, bu_coddivi, bu_codstab, " & _
             " bu_codart, bu_fase, bu_codcli, codditt, bu_coddica, bu_coddicv, bu_codgrua, bu_mese, bu_anno "
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrT1 As DataRow In dttTmp.Rows
          strSQL = "UPDATE cabudget SET " & _
                   "bu_valcon = CASE WHEN " & CStrSQL(dtrT!pb_darave) & " = 'A' THEN -1 * " & CDblSQL(NTSCDec(dtrT1!bu_valcon)) & _
                   " ELSE " & CDblSQL(NTSCDec(dtrT1!bu_valcon)) & " END, " & _
                   "bu_qtacon = " & CDblSQL(NTSCDec(dtrT1!bu_qtacon)) & _
                   " WHERE bu_codcena = " & dtrT1!bu_codcena.ToString & _
                   " AND bu_codcfam = " & CStrSQL(dtrT1!bu_codcfam) & _
                   " AND bu_commeca = " & dtrT1!bu_commeca.ToString & _
                   " AND bu_coddivi = " & dtrT1!bu_coddivi.ToString & _
                   " AND bu_codstab = " & dtrT1!bu_codstab.ToString & _
                   " AND bu_codart = " & CStrSQL(dtrT1!bu_codart) & _
                   " AND bu_fase = " & dtrT1!bu_fase.ToString & _
                   " AND bu_codcli = " & dtrT1!bu_codcli.ToString & _
                   " AND codditt = " & CStrSQL(dtrT1!codditt) & _
                   " AND bu_coddica = " & CStrSQL(dtrT1!bu_coddica) & _
                   " AND bu_coddicv = " & CStrSQL(dtrT1!bu_coddicv) & _
                   " AND bu_codgrua = " & dtrT1!bu_codgrua.ToString & _
                   " AND bu_riga = " & dtrT!pb_riga.ToString & _
                   " AND bu_mese = " & dtrT1!bu_mese.ToString & _
                   " AND bu_anno = " & dtrT1!bu_anno.ToString
          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
        dttTmp.Clear()
      Next    'For Each dtrT As DataRow In dttSchema.Rows
      dttSchema.Clear()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaContrda(ByVal strCodditt As String, ByVal strNomprog As String, _
                                           ByVal strTipork As String, ByVal nMagimp As Integer, _
                                           ByVal lConto As Integer, ByVal nCodcent As Integer, _
                                           ByVal strCodart As String, ByVal nCodcaum As Integer, _
                                           ByVal nFase As Integer, ByVal strDatdoc As String, _
                                           ByVal bCollaudiSuDocEmessi As Boolean, _
                                           ByRef nCodtcon As Integer, ByRef nCodpcam As Integer, _
                                           ByRef nCodclaq As Integer, ByRef nCodlivq As Integer, _
                                           ByRef dbConn As DbConnection) As Boolean
    'GESTIONE COLLAUDI: VERIFICA SE L'ARTICOLO DEVE ESSERE COLLAUDATO
    'se restituisce TRUE l'articolo deve essere collaudato

    ' ritorna gli ultimi 4 parametri obblgatori, e poi ritorna true se ha trovatoqualcosa, false in caso contrario
    ' tipork e magimp servono per identificare se trattasi di ricevimento mat,
    ' oppure versamento e su che tipo di magazzino è avvenuta la produzione, interno o estermo
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nCodsgme As Integer = 0
    Try

      CercaContrda = False

      ' trova il cod. sottogruppo
      strSQL = "SELECT ar_sotgru from artico where ar_codart = '" & strCodart & "' " & _
               " AND codditt = " & CStrSQL(strCodditt) & " "
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        nCodsgme = NTSCInt(dttTmp.Rows(0)!ar_sotgru)
      End If
      dttTmp.Clear()

      ' inizia la ricerca
      If strNomprog = "BSVEBOLL" Then
        If strTipork = "T" Then ' carico da produzione : versam. o reso lavorato da terzista (per ora trattati uguali)
          strSQL = "SELECT * from contrda where cd_codart = '" & strCodart & "' and cd_conto = " & lConto & _
                   " and cd_codsgme = 0 and cd_codcent = 0 and cd_fase = " & nFase & " and cd_codcaum = " & nCodcaum & _
                   " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                   " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
            nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
            nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
            nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
            dttTmp.Clear()
            GoTo finito
          Else ' non trovato : cerca per csottogruppo mercologico
            dttTmp.Clear()
            strSQL = "SELECT * from contrda where cd_codart = ' ' and cd_conto = " & lConto & _
                     " and cd_codsgme = " & nCodsgme & " and cd_codcent = 0 and cd_fase = " & nFase & _
                     " and cd_codcaum = " & nCodcaum & _
                     " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                     " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp.Rows.Count > 0 Then
              nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
              nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
              nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
              nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
              dttTmp.Clear()
              GoTo finito
            Else
              dttTmp.Clear()
              Return False
            End If
          End If ' fine T (=vers. da produzione)
        Else ' ddt ricevuto o fatt. immediata da fornitore in acquisto
          If (strTipork = "M") Or (strTipork = "L") Or (strTipork = "Z") Or (bCollaudiSuDocEmessi And (strTipork = "A" Or strTipork = "B")) Then
            strSQL = "SELECT * from contrda where cd_codart = '" & strCodart & "' and cd_conto = " & lConto & _
                     " and cd_codsgme = 0 and cd_codcent = 0 and cd_fase = " & nFase & " and cd_codcaum = " & nCodcaum & _
                     " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                     " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
            dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp.Rows.Count > 0 Then
              nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
              nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
              nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
              nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
              dttTmp.Clear()
              GoTo finito
            Else ' non trovato : cerca per csottogruppo mercologico
              dttTmp.Clear()
              strSQL = "SELECT * from contrda where cd_codart = ' ' and cd_conto = " & lConto & _
                       " and cd_codsgme = " & nCodsgme & " and cd_codcent = 0 and cd_fase = " & nFase & " and cd_codcaum = " & nCodcaum & _
                       " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                       " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
              dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count > 0 Then
                nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
                nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
                nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
                nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
                dttTmp.Clear()
                GoTo finito
              Else
                dttTmp.Clear()
                Return False
              End If
            End If

          Else ' altro tipo doc : nessun test
            Return False
          End If
        End If
      End If
      ' da bsdbavla, invece (dich. di fase interna)
      If strNomprog = "BSDBAVLA" Then
        strSQL = "SELECT * from contrda where cd_codart = '" & strCodart & "' and cd_conto = " & lConto & _
                 " and cd_codsgme = 0 and cd_codcent = " & nCodcent & " and cd_fase = " & nFase & " and cd_codcaum = 0 " & _
                 " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                 " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
          nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
          nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
          nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
          dttTmp.Clear()
          GoTo finito
        Else ' non trovato : cerca per csottogruppo mercologico
          dttTmp.Clear()
          strSQL = "SELECT * from contrda where cd_codart = ' ' and cd_conto = " & lConto & _
                   " and cd_codsgme = " & nCodsgme & " and cd_codcent = " & nCodcent & " and cd_fase = " & nFase & " and cd_codcaum = 0 " & _
                   " and cd_dtinval <= " & CDataSQL(strDatdoc) & " and cd_dtfival >= " & CDataSQL(strDatdoc) & _
                   " AND codditt = " & CStrSQL(strCodditt) & " order by cd_dtinval DESC "
          dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            nCodtcon = NTSCInt(dttTmp.Rows(0)!cd_codtcon)
            nCodpcam = NTSCInt(dttTmp.Rows(0)!cd_codpcam)
            nCodclaq = NTSCInt(dttTmp.Rows(0)!cd_codclaq)
            nCodlivq = NTSCInt(dttTmp.Rows(0)!cd_codlivq)
            dttTmp.Clear()
            GoTo finito
          Else
            dttTmp.Clear()
            Return False
          End If
        End If
      End If ' fine avla
finito:

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

#Region "Project Management"
  Public Overridable Function Caricattproesebappo(ByVal strDitta As String, ByVal lIIttproesebappo As Integer, _
                                                  ByVal lCommeca As Integer, ByVal lTaskid As Integer, _
                                                  ByRef dbConn As DbConnection) As Boolean
    '--- dato commessa task creo copiandoli i record di ttproesec da proeseb, se non ci sono già;
    ' se task di tipo F copia anche i record di proeseb dei fratelli
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Dim strPrevGrup As String = ""
    Dim lUidpadre As Integer = 0

    Try
      Caricattproesebappo = True
      ' priva svuoto l'istanza di tttasks
      ' adesso legge i dati da tornare indietro
      strSQL = " SELECT tsk_uidpadre, tsk_darave, tsk_tipotask, tsk_gestcost,  tsk_prevgrup from TASKS " & _
                 " WHERE tsk_commeca = " & lCommeca & _
                 " and tsk_taskid = " & lTaskid & _
                 " And codditt = " & CStrSQL(strDitta)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        'strDaraveout = snaTMP10!tsk_darave
        'strTipoTask = snaTMP10!tsk_tipotask
        'strGestcost = snaTMP10!tsk_gestcost
        strPrevGrup = NTSCStr(dttTmp.Rows(0)!tsk_prevgrup)
        ' locale
        lUidpadre = NTSCInt(dttTmp.Rows(0)!tsk_uidpadre)
      Else
        Return False
      End If
      dttTmp.Clear()

      If strPrevGrup = "N" Then
        ' adesso carica ttproesec da proeseb ma solo per il task in oggetto
        strSQL = "INSERT INTO TTPROESEC ( codditt, instid,  pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec,  pes_workesec,  pes_salcon, " & _
            " pes_pctworkcomp,  pes_diffwork,  pes_remworkdop,  pes_diffqta,  pes_remqtadop,  pes_costesec,  pes_data,  pes_diffcost,  pes_remcostdop,  pes_note, " & _
            " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie,  pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno,  pes_mmserie,  pes_mmnum,  pes_mmriga,  pes_paprogr,  pes_olprogr, pes_elabor ) "
        strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIIttproesebappo & ",  pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec,  pes_workesec,  pes_salcon, " & _
            " pes_pctworkcomp,  pes_diffwork,  pes_remworkdop,  pes_diffqta,  pes_remqtadop,  pes_costesec,  pes_data,  pes_diffcost,  pes_remcostdop,  pes_note, " & _
            " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie,  pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno,  pes_mmserie,  pes_mmnum,  pes_mmriga,  pes_paprogr,  pes_olprogr, pes_elabor  " & _
            " FROM proeseb WHERE pes_commeca = " & lCommeca & " and pes_taskid = " & lTaskid & " And proeseb.codditt = " & CStrSQL(strDitta) & _
            " And NOT EXISTS (SELECT codditt, pes_commeca, pes_esecid FROM ttproesec " & _
                            " WHERE instid = " & lIIttproesebappo & _
                            " And ttproesec.codditt = " & CStrSQL(strDitta) & _
                            " And pes_commeca = " & lCommeca & _
                            " And ttproesec.codditt = proeseb.codditt " & _
                            " And ttproesec.pes_commeca = proeseb.pes_commeca " & _
                            " And ttproesec.pes_esecid = proeseb.pes_esecid)"
        ' non carica i record già esistenti, così facendo, altrimenti occorre inserire NOT IN (select...) !!!
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        ' se riga figlia di preventivazione di  gruppo (strprevgrup=F), ora copia tutti i record dei task fratelli
      Else ' est un F (cioè figlio di prev di gruppo)

        strSQL = "INSERT INTO TTPROESEC ( codditt, instid, pes_commeca, pes_esecid,  pes_taskid,  pes_qtaesec,  pes_workesec,  pes_salcon, " & _
           " pes_pctworkcomp,  pes_diffwork,  pes_remworkdop,  pes_diffqta,  pes_remqtadop,  pes_costesec,  pes_data,  pes_diffcost,  pes_remcostdop,  pes_note, " & _
           " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie,  pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno,  pes_mmserie,  pes_mmnum,  pes_mmriga,  pes_paprogr,  pes_olprogr, pes_elabor  ) "
        strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIIttproesebappo & ",  pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec,  pes_workesec,  pes_salcon, " & _
           " pes_pctworkcomp,  pes_diffwork,  pes_remworkdop,  pes_diffqta,  pes_remqtadop,  pes_costesec,  pes_data,  pes_diffcost,  pes_remcostdop,  pes_note, " & _
           " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie,  pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno,  pes_mmserie,  pes_mmnum,  pes_mmriga,  pes_paprogr,  pes_olprogr, pes_elabor  " & _
           " FROM PROESEB inner join TASKS ON (TASKS.codditt = PROESEB.codditt) AND (TASKS.tsk_commeca = PROESEB.pes_commeca) And (TASKS.tsk_taskid = PROESEB.pes_taskid) " & _
           " WHERE proeseb.codditt = " & CStrSQL(strDitta) & " And pes_commeca = " & lCommeca & " and tsk_uidpadre = " & lUidpadre & _
           " And NOT EXISTS (SELECT codditt, pes_commeca, pes_esecid FROM ttproesec " & _
                           " WHERE instid = " & lIIttproesebappo & _
                           " and ttproesec.codditt = " & CStrSQL(strDitta) & _
                           " And pes_commeca = " & lCommeca & _
                           " And ttproesec.codditt = proeseb.codditt " & _
                           " And ttproesec.pes_commeca = proeseb.pes_commeca " & _
                           " And ttproesec.pes_esecid = proeseb.pes_esecid)"
        ' anche qui non mette i già presenti, altrimenti fare NOT IN  (SELECT) !!!
        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaPriana(ByVal strDitta As String, ByVal strNometabProesec As String, _
                                                  ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, _
                                                  ByVal lPaprogr As Integer) As Integer
    ' ritorna il numero di esecid da un record di priana, per una determinata commessa, se indicata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    TrovaEsecIdDaPriana = 0
    Try
      strSQL = "SELECT pes_esecid from " & strNometabProesec & " where codditt = " & CStrSQL(strDitta) & " And pes_tipork='P' and pes_paprogr = " & lPaprogr & " "
      If lCommeca <> 0 Then
        strSQL = strSQL & " and pes_commeca = " & lCommeca
      End If
      If lIITtproesec <> 0 Then
        strSQL = strSQL & " and instid = " & lIITtproesec
      End If
      strSQL = strSQL & " order by pes_esecid DESC" ' prende il più recente
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCInt(dttTmp.Rows(0)!pes_esecid)
      End If

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function LegTasksCompleteDao(ByVal strDitta As String, ByVal strNometabProesec As String, _
                                                  ByVal lIITtproesec As Integer, ByVal lIItttasks As Integer, _
                                                  ByVal lCommeca As Integer, ByVal lTaskid As Integer, _
                                                  ByVal lEsecidDaEscludere As Integer, ByVal strFinoal As String, _
                                                  ByVal bControllaBaseCost As Boolean, ByRef strDestask As String, _
                                                  ByRef strDaraveout As String, ByRef strTipoTask As String, _
                                                  ByRef strGestcost As String, ByRef dQtares As Decimal, _
                                                  ByRef dWorkres As Decimal, ByRef dCostores As Decimal, _
                                                  ByRef dBasecost As Decimal, ByRef dBasecostun As Decimal, _
                                                  ByRef dBasework As Decimal, ByRef dBaseqta As Decimal, _
                                                  ByRef strPrevGrup As String, ByVal bDaordini As Boolean, _
                                                  ByRef strRilasciato As String) As Boolean
    '--- dato commessa task crea il record in TTTASKS
    ' lIItttasks instid del TTTASK di appoggio
    ' comme taskid , identificatori del task
    ' eseciddaescludere = id. di proeseb da non considerare
    ' strFinoal , data finale
    Dim strSQL As String = ""
    Dim nRes As Integer = 0
    Dim dRettqta As Decimal, dRettwork As Decimal, dRettcost As Decimal
    Dim dTRettqta As Decimal, dTRettwork As Decimal, dTRettcost As Decimal

    ' lo aggiorna da proeseb e restituisce descrizioni costi e qtabtità residue del task
    Dim lUidpadre As Integer
    Dim dLocbasecost As Decimal
    Dim dLocbaseqta As Decimal, dLocbasework As Decimal
    Dim strGestcostpadre As String
    Dim strControllaBaseCost As String, strDaordini As String

    Dim dttTmp As New DataTable

    Dim nErrcode As Integer = 0

    LegTasksCompleteDao = True

    Try

      If UCase(strNometabProesec) = "TTPROESEC" Then
        'Caso di SQL Server... al termine dei calcoli devo uscire
        If bControllaBaseCost Then strControllaBaseCost = "S" Else strControllaBaseCost = "N"
        If bDaordini Then strDaordini = "S" Else strDaordini = "N"

        'lancio la stored procedure
        strSQL = "bussp_LegTaskComp9 " & _
                lIITtproesec.ToString & ", " & _
                lIItttasks.ToString & ", " & _
                lCommeca.ToString & ", " & _
                lTaskid.ToString & ", " & _
                lEsecidDaEscludere.ToString & ", " & _
                CStrSQL(strControllaBaseCost) & ", " & _
                CStrSQL(strDaordini) & ", " & _
                CStrSQL(strDitta)
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        With dttTmp.Rows(0)
          strDestask = NTSCStr(!DescrTask)
          strDaraveout = NTSCStr(!DareAvereOut)
          strTipoTask = NTSCStr(!TipoTask)
          strGestcost = NTSCStr(!GestioneCosti)
          dQtares = NTSCDec(!QuantRes)
          dWorkres = NTSCDec(!WorkRes)
          dCostores = NTSCDec(!CostoRes)
          dBasecost = NTSCDec(!BaseCost)
          dBasecostun = NTSCDec(!BaseCostun)
          dBasework = NTSCDec(!BaseWork)
          dBaseqta = NTSCDec(!BaseQuant)
          strPrevGrup = NTSCStr(!PreventGrup)
          strRilasciato = NTSCStr(!Rilasciato)
        End With
        dttTmp.Clear()

        Return True
      End If

      'Per sicurezza in caso di errore
      strGestcost = "2"
      strPrevGrup = "N"

      ' priva svuoto l'istanza di tttasks
      oCldBase.ResetTblInstId("TTTASKS", False, lIItttasks)
      '---
      strSQL = "INSERT INTO TTTASKS (codditt, instid , tsk_commeca,  tsk_taskid,  tsk_codcapi,  tsk_descapi,  tsk_codtarg,  tsk_destarg,  tsk_codctap,  tsk_desctap,  tsk_codctas,  tsk_descr,  tsk_posiz, " & _
            "  tsk_livello ,  tsk_uidpadre,  tsk_summary,  tsk_prevgrup,  tsk_progdate,  tsk_startdate,  tsk_finishdate,  tsk_actstart,  tsk_actfinish,  tsk_basestart,  tsk_basefinish,  tsk_ultagg, " & _
            "  tsk_datins,  tsk_opnomeo,  tsk_opnome,  tsk_deadline,  tsk_basecostun,  tsk_baseprzun,  tsk_baseprz,  tsk_work,  tsk_actwork,  tsk_basework,  tsk_remwork,  tsk_qta,  tsk_actqta, " & _
            "  tsk_baseqta,  tsk_remqta,  tsk_flevas,  tsk_cost,  tsk_actcost,  tsk_basecost,  tsk_remcost,  tsk_pctworkcomp,  tsk_prevnum,  tsk_prevrev,  tsk_varid, "
      strSQL = strSQL & " tsk_progprev,  tsk_codforn,  tsk_codinca,  tsk_coddestin,  tsk_success,  tsk_relsucc,  tsk_codttas,  tsk_codart,  tsk_codlavo,  tsk_contoca,  tsk_codcent,  tsk_codcena,  tsk_darave,  tsk_codcfam, " & _
           "   tsk_escompca,  tsk_ump,  tsk_umwork,  tsk_datini,  tsk_datfin,  tsk_data1,  tsk_data2,  tsk_data3,  tsk_data4,  tsk_data5,  tsk_stato,  tsk_note1,  tsk_note2,  tsk_offcost, " & _
           "  tsk_ordcost,  tsk_offflevas,  tsk_ordflevas,  tsk_offremcost,  tsk_ordremcost,  tsk_tipotask,  tsk_gestcost,  tsk_sulcant,  tsk_rdaem,  tsk_genrda,  tsk_ricprev,  tsk_percpz,  tsk_percpr, " & _
           "  tsk_impbase,  tsk_rilasciato,  tsk_codrumod,    tsk_codvcl,  tsk_perqta,   tsk_posiz0, " & _
           "  tsk_uid0,  tsk_posiz1,  tsk_uid1,  tsk_posiz2,  tsk_uid2,  tsk_posiz3,  tsk_uid3, tsk_nlayout, tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcostprg, tsk_orddiffcost, tsk_offcostprg, tsk_offdiffcost, tsk_totcost, tsk_totdiffcost, " & _
           " tsk_ordqta, tsk_ordqtaprg, tsk_orddiffqta, tsk_ordremqta, tsk_ordwork, tsk_ordworkprg, tsk_orddiffwork, tsk_ordremwork, tsk_totqta, tsk_totdiffqta, tsk_totwork, tsk_totdiffwork, tsk_offqta, tsk_offqtaprg, tsk_offdiffqta, tsk_offremqta, tsk_offwork, tsk_offworkprg, tsk_offdiffwork, tsk_offremwork, tsk_ricdur, tsk_dur, tsk_actdur, tsk_remdur, tsk_basedur, tsk_diffdur, tsk_nfigli, tsk_pctordcomp, tsk_totactcost, tsk_totremcost )  "

      'strSQL = strSQL & "  SELECT  " & lIItttasks & "  ,  tsk_commeca,  tsk_taskid,  tsk_codcapi,  tsk_descapi,  tsk_codtarg,  tsk_destarg,  tsk_codctap,  tsk_desctap,  tsk_codctas,  tsk_descr,  tsk_posiz, " & _
      '      "  tsk_livello ,  tsk_uidpadre,  tsk_summary,  tsk_prevgrup,  tsk_progdate,  tsk_startdate,  tsk_finishdate,  tsk_actstart,  tsk_actfinish,  tsk_basestart,  tsk_basefinish,  tsk_ultagg, " & _
      '      "  tsk_datins,  tsk_opnomeo,  tsk_opnome,  tsk_deadline,  tsk_basecostun,  tsk_baseprzun,  tsk_baseprz,  tsk_work,  tsk_actwork,  tsk_basework,  tsk_remwork,  tsk_qta,  tsk_actqta, " & _
      '      "  tsk_baseqta,  tsk_remqta,  tsk_flevas,  tsk_cost,  tsk_actcost,  tsk_basecost,  tsk_remcost,  tsk_pctworkcomp,  tsk_prevnum,  tsk_prevrev,  tsk_varid, "
      'strSQL = strSQL & "  tsk_progprev,  tsk_codforn,  tsk_codinca,  tsk_coddestin,  tsk_success,  tsk_relsucc,  tsk_codttas,  tsk_codart,  tsk_codlavo,  tsk_contoca,  tsk_codcent,  tsk_codcena,  tsk_darave,  tsk_codcfam, " & _
      '     "   tsk_escompca,  tsk_ump,  tsk_umwork,  tsk_datini,  tsk_datfin,  tsk_data1,  tsk_data2,  tsk_data3,  tsk_data4,  tsk_data5,  tsk_stato,  tsk_note1,  tsk_note2,  tsk_offcost, " & _
      '     "  tsk_ordcost,  tsk_offflevas,  tsk_ordflevas,  tsk_offremcost,  tsk_ordremcost,  tsk_tipotask,  tsk_gestcost,  tsk_sulcant,  tsk_rdaem,  tsk_genrda,  tsk_ricprev,  tsk_percpz,  tsk_percpr, " & _
      '     "  tsk_impbase,  tsk_rilasciato,  tsk_codrumod,    tsk_codvcl,  tsk_perqta,   tsk_posiz0, " & _
      '     "  tsk_uid0,  tsk_posiz1,  tsk_uid1,  tsk_posiz2,  tsk_uid2,  tsk_posiz3,  tsk_uid3, tsk_nlayout, tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcostprg, tsk_orddiffcost, tsk_offcostprg, tsk_offdiffcost, tsk_totcost, tsk_totdiffcost "

      ' QUI SOTTO SETTA A ZERO I CAMPI ACT e METTE I REM AL VALORE DEI  PREV perchè si parte dall'inizio...
      ' e mette i FLEVAS a C , idem per i costo ordinato e offerte
      strSQL = strSQL & "  SELECT  " & CStrSQL(strDitta) & ", " & lIItttasks & "  ,  tsk_commeca,  tsk_taskid,  tsk_codcapi,  tsk_descapi,  tsk_codtarg,  tsk_destarg,  tsk_codctap,  tsk_desctap,  tsk_codctas,  tsk_descr,  tsk_posiz, " & _
            "  tsk_livello ,  tsk_uidpadre,  tsk_summary,  tsk_prevgrup,  tsk_progdate,  tsk_startdate,  tsk_finishdate,  tsk_actstart,  tsk_actfinish,  tsk_basestart,  tsk_basefinish,  tsk_ultagg, " & _
            "  tsk_datins,  tsk_opnomeo,  tsk_opnome,  tsk_deadline,  tsk_basecostun,  tsk_baseprzun,  tsk_baseprz,  tsk_basework,  0,  tsk_basework,  tsk_basework,  tsk_baseqta,  0, " & _
            "  tsk_baseqta,  tsk_baseqta,  'C',  tsk_basecost,  0,  tsk_basecost,  tsk_basecost,  0,  tsk_prevnum,  tsk_prevrev,  tsk_varid, "
      strSQL = strSQL & "  tsk_progprev,  tsk_codforn,  tsk_codinca,  tsk_coddestin,  tsk_success,  tsk_relsucc,  tsk_codttas,  tsk_codart,  tsk_codlavo,  tsk_contoca,  tsk_codcent,  tsk_codcena,  tsk_darave,  tsk_codcfam, " & _
           "   tsk_escompca,  tsk_ump,  tsk_umwork,  tsk_datini,  tsk_datfin,  tsk_data1,  tsk_data2,  tsk_data3,  tsk_data4,  tsk_data5,  tsk_stato,  tsk_note1,  tsk_note2,  0, " & _
           "  0,  'C',  'C',  tsk_basecost,  tsk_basecost,  tsk_tipotask,  tsk_gestcost,  tsk_sulcant,  tsk_rdaem,  tsk_genrda,  tsk_ricprev,  tsk_percpz,  tsk_percpr, " & _
           "  tsk_impbase,  tsk_rilasciato,  tsk_codrumod,    tsk_codvcl,  tsk_perqta,   tsk_posiz0, " & _
           "  tsk_uid0,  tsk_posiz1,  tsk_uid1,  tsk_posiz2,  tsk_uid2,  tsk_posiz3,  tsk_uid3, tsk_nlayout, 0, 0, 0, tsk_basecost, 0, tsk_basecost, 0, tsk_basecost, 0, " & _
           "  0, tsk_baseqta, 0, tsk_baseqta, 0, tsk_basework, 0, tsk_basework, tsk_baseqta, 0, tsk_basework, 0, 0, tsk_baseqta, 0, tsk_baseqta, 0, tsk_basework, 0, tsk_basework, tsk_ricdur, tsk_dur, tsk_actdur, tsk_remdur, tsk_basedur, tsk_diffdur, tsk_nfigli, 0, 0, tsk_basecost "
      strSQL = strSQL & " FROM TASKS WHERE tsk_commeca=" & lCommeca & " and tsk_taskid=" & lTaskid & " And codditt = " & CStrSQL(strDitta)
      nRes = oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      If Not (nRes = 1) Then
        Return False
      End If
      dRettqta = 0 : dRettwork = 0 : dRettcost = 0
      ' adesso chiama la routine che la aggiorna dall'inizio
      If Not AggTasksCompleteDao(strDitta, "TTPROESEC", lIITtproesec, "TTTASKS", lIItttasks, lCommeca, lTaskid, lEsecidDaEscludere, True, False, False, IntSetDate("31/12/2099"), Nothing, nErrcode, dRettqta, dRettwork, dRettcost) Then
        Return False
      End If
      ' adesso legge i dati da tornare indietro

      strSQL = " SELECT tsk_uidpadre, tsk_descr, tsk_darave, tsk_tipotask, tsk_gestcost, tsk_remqta, tsk_remwork, tsk_remcost, tsk_ordremqta, tsk_ordremwork, tsk_ordremcost, tsk_basecost, tsk_basecostun, tsk_baseqta, tsk_basework, tsk_prevgrup, tsk_rilasciato from TTTASKS " & _
                 " where codditt = " & CStrSQL(strDitta) & _
                 " And instid = " & lIItttasks & _
                 " and tsk_commeca = " & lCommeca & _
                 " and tsk_taskid = " & lTaskid
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strDestask = NTSCStr(!tsk_descr)
          strDaraveout = NTSCStr(!tsk_darave)
          strTipoTask = NTSCStr(!tsk_tipotask)
          strGestcost = NTSCStr(!tsk_gestcost)
          strRilasciato = NTSCStr(!tsk_rilasciato)
          If bDaordini Then
            dQtares = NTSCDec(!tsk_ordremqta) ' !! da gestire !!!
            dWorkres = NTSCDec(!tsk_ordremwork)
            dCostores = NTSCDec(!tsk_ordremcost)
          Else
            dQtares = NTSCDec(!tsk_remqta)
            dWorkres = NTSCDec(!tsk_remwork)
            dCostores = NTSCDec(!tsk_remcost)
          End If
          dBasecost = NTSCDec(!tsk_basecost) + dRettcost ' qui controllare meglio...
          dBasecostun = NTSCDec(!tsk_basecostun)
          dBasework = NTSCDec(!tsk_basework) + dRettwork
          dBaseqta = NTSCDec(!tsk_baseqta) + dRettqta
          strPrevGrup = NTSCStr(!tsk_prevgrup)
          ' locale
          lUidpadre = NTSCInt(!tsk_uidpadre)
        End With

      Else
        Return False
      End If
      dttTmp.Clear()
      ' se riga figlia di preventivazione di  gruppo (strprevgrup=F), ora aggiorna meglio i campi in uscita XXXRES e XXXBASE
      ' ATTENZIONE ! gestire il caso che il padre abbia un darave <> darave del filio (dei filgli..) NON GESTITO per ora
      ' ATRENZIONE : per ora gestito solo che la riga con prevgrup sia padre DIRETTO del task in oggetto, e non nonno... !!!
      If strPrevGrup = "F" Then ' solo per esecuzione , non per ordini o offerte, per ora...)
        strSQL = " SELECT tsk_gestcost, tsk_basecost, tsk_baseqta, tsk_basework from TASKS " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " And tsk_commeca= " & lCommeca & _
                   " and tsk_taskid=" & lUidpadre & " "
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then
          dttTmp.Clear()
          Return False
        End If
        dLocbasecost = NTSCDec(dttTmp.Rows(0)!tsk_basecost)
        dLocbaseqta = NTSCDec(dttTmp.Rows(0)!tsk_baseqta)
        dLocbasework = NTSCDec(dttTmp.Rows(0)!tsk_basework)
        strGestcostpadre = NTSCStr(dttTmp.Rows(0)!tsk_gestcost)
        dttTmp.Clear()
        ' trovato il padre ora cerca di totalizzare da proeseb tutte le differenze ed eseguiti su tutti i task a lui dipendenti di tipo F
        strSQL = " SELECT sum(pes_costesec * pes_segno) as costesec, sum(pes_diffcost  * pes_segno) as diffcost, sum(pes_qtaesec * pes_segno) as qtaesec, sum(pes_diffqta * pes_segno) as diffqta, sum(pes_workesec * pes_segno) as workesec, sum(pes_diffwork * pes_segno) as diffwork " & _
                   " FROM tasks INNER JOIN (tasks AS tasks1 INNER JOIN " & strNometabProesec & _
                   " ON (tasks1.codditt = " & strNometabProesec & ".codditt) AND (tasks1.tsk_taskid = " & strNometabProesec & ".pes_taskid) AND (tasks1.tsk_commeca = " & strNometabProesec & ".pes_commeca)) " & _
                   " ON (tasks.codditt = tasks1.codditt) And (tasks.tsk_taskid = tasks1.tsk_uidpadre) AND (tasks.tsk_commeca = tasks1.tsk_commeca) " & _
                   " WHERE pes_esecid <> " & lEsecidDaEscludere & _
                   " And tasks.codditt = " & CStrSQL(strDitta) & _
                   " and tasks.tsk_commeca=" & lCommeca & _
                   " and tasks.tsk_taskid=" & lUidpadre & " "
        If bDaordini Then ' solo ordini
          strSQL = strSQL & " and pes_tipork = 'O' "
        End If
        If Not (bDaordini) Then ' esecuzioni vere e proprie
          strSQL = strSQL & " and ( not ( pes_tipork = 'O' ) ) "
        End If
        If lIITtproesec <> 0 Then
          strSQL = strSQL & " and " & strNometabProesec & ".instid = " & lIITtproesec
        End If
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then
          dttTmp.Clear()
          Return False
        End If
        If Not (dttTmp.Rows(0)!COSTESEC.Equals(DBNull.Value)) Then
          ' cambia le variabili ricavate sopra andando per differenze rispetto ai basecost ecc.
          dCostores = dLocbasecost - NTSCDec(dttTmp.Rows(0)!COSTESEC) + NTSCDec(dttTmp.Rows(0)!DIFFCOST) 'ìl costo rimanente sul task padre... tsk_remcost virtuale
          dBasecost = dLocbasecost ' qui controllare meglio...
          If strGestcostpadre = "4" Or strGestcostpadre = "5" Then
            dBasework = dLocbasework
            dWorkres = dLocbasework - NTSCDec(dttTmp.Rows(0)!WORKESEC) + NTSCDec(dttTmp.Rows(0)!DIFFWORK)
          End If
          If strGestcostpadre = "3" Or strGestcostpadre = "5" Then
            dQtares = dLocbaseqta - NTSCDec(dttTmp.Rows(0)!QTAESEC) + NTSCDec(dttTmp.Rows(0)!DIFFQTA) ' potrebbero essere non significativi per questo task..
            dBaseqta = dLocbaseqta
          End If
        Else
          dCostores = dLocbasecost  'ìl costo rimanente sul task padre... tsk_remcost virtuale
          dBasecost = dLocbasecost ' qui controllare meglio...
          If strGestcostpadre = "4" Or strGestcostpadre = "5" Then
            dBasework = dLocbasework
            dWorkres = dLocbasework
          End If
          If strGestcostpadre = "3" Or strGestcostpadre = "5" Then
            dQtares = dLocbaseqta  ' potrebbero essere non significativi per questo task..
            dBaseqta = dLocbaseqta
          End If
        End If
        'dBasecostun = snaTMP10!tsk_basecostun
        dttTmp.Clear()
        'End If
        ' adessoc erco ev. differenze registrate come rettifche manauli del valore base (cost, qta , work)
        '
        strSQL = " SELECT sum(pes_diffcost * pes_segno) as diffcost, sum(pes_diffqta * pes_segno) as diffqta,  sum(pes_diffwork * pes_segno) as diffwork " & _
                   " FROM tasks INNER JOIN (tasks AS tasks1 INNER JOIN " & strNometabProesec & _
                   " ON (tasks1.codditt = " & strNometabProesec & ".codditt) AND (tasks1.tsk_taskid = " & strNometabProesec & ".pes_taskid) AND (tasks1.tsk_commeca = " & strNometabProesec & ".pes_commeca)) " & _
                   " ON (tasks.codditt = tasks1.codditt) AND (tasks.tsk_taskid = tasks1.tsk_uidpadre) AND (tasks.tsk_commeca = tasks1.tsk_commeca) " & _
                   " WHERE pes_esecid <> " & lEsecidDaEscludere & _
                   " and tasks.codditt = " & CStrSQL(strDitta) & _
                   " and tasks.tsk_commeca=" & lCommeca & _
                   " and tasks.tsk_taskid=" & lUidpadre & _
                   " and ( pes_tipork = 'R' or pes_tipork = 'S') "
        If lIITtproesec <> 0 Then
          strSQL = strSQL & " and " & strNometabProesec & ".instid = " & lIITtproesec
        End If
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then
          dttTmp.Clear()
          Return False
        End If
        If Not (dttTmp.Rows(0)!DIFFCOST.Equals(DBNull.Value)) Then
          dTRettcost = NTSCDec(dttTmp.Rows(0)!DIFFCOST)
          dTRettwork = NTSCDec(dttTmp.Rows(0)!DIFFWORK)
          dTRettqta = NTSCDec(dttTmp.Rows(0)!DIFFQTA)
          dBaseqta = dBaseqta + dTRettqta
          dBasework = dBasework + dTRettwork
          dBasecost = dBasecost + dTRettcost
          If (bDaordini) Then ' nell'altro caso ne abbiamo già tenuto conto nella query sopra ... (credo...)
            dQtares = dQtares + dTRettqta
            dWorkres = dWorkres + dTRettwork
            dCostores = dCostores + dTRettcost
          End If
        Else
          ' niente
        End If
        dttTmp.Clear()
      End If ' est un F


      ' se non sono significativi per quel task li azzero ...
      Select Case strGestcost
        Case "1" 'solo pianificazione (IMPOSSIBILE!!!)
          dQtares = 0
          dBaseqta = 0
          dWorkres = 0
          dBasework = 0
          dCostores = 0
          dBasecost = 0

        Case "2" ' solo costo
          dQtares = 0
          dBaseqta = 0
          dWorkres = 0
          dBasework = 0
        Case "3" ' costo e qta
          dWorkres = 0
          dBasework = 0
        Case "4" ' costo e lavoro
          dQtares = 0
          dBaseqta = 0
        Case "5" ' costo qta e lavoro
          ' ci vuole tutto
      End Select
      '---

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function AggTasksCompleteDao(ByVal strDitta As String, ByVal strNomtabin As String, _
                                                  ByVal lIIin As Integer, ByVal strNomtabout As String, _
                                                  ByVal lIIout As Integer, ByVal lCommeca As Integer, _
                                                  ByVal lTaskid As Integer, ByVal lEsecidDaEscludere As Integer, _
                                                  ByVal bDallinizio As Boolean, ByVal bVissforbudg As Boolean, _
                                                  ByVal bAbortsforbudg As Boolean, ByVal strFinoal As String, _
                                                  ByRef dbConn As DbConnection, ByRef nErrcode As Integer, _
                                                  ByRef dRettqta As Decimal, ByRef dRettwork As Decimal, _
                                                  ByRef dRettcost As Decimal) As Boolean
    '--- aggiorna sull atabella out tutti i dati effettuando l'agg
    ' su tutti i rk di proesec (tabella in) relativi a quel task-id
    ' (utilizzato da CIPRIA e altri per aggiornate i dati alla data della registrazione tenedo
    '  conto di proeseb )
    Dim dTRettqta As Decimal, dTRettwork As Decimal, dTRettcost As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      AggTasksCompleteDao = True
      strSQL = " SELECT pes_esecid, pes_tipork, pes_diffqta, pes_diffwork, pes_diffcost , pes_segno  from " & strNomtabin & _
                 " where codditt = " & CStrSQL(strDitta) & _
                 " And pes_commeca = " & lCommeca & _
                 " and pes_taskid = " & lTaskid & _
                 " and pes_data <= " & CDataSQL(strFinoal)
      If UCase(strNomtabin) = "PROESEB" And bDallinizio = False Then
        strSQL = strSQL & " and pes_elabor = 'N' "
      End If
      If lIIin <> 0 Then
        strSQL = strSQL & " and instid = " & lIIin & " "
      End If
      If lEsecidDaEscludere <> 0 Then
        strSQL = strSQL & " and pes_esecid <> " & lEsecidDaEscludere & " "
      End If
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      For Each dtrT As DataRow In dttTmp.Rows
        If Not AggTasksDao(strDitta, strNomtabin, lIIin, strNomtabout, lIIout, lCommeca, NTSCInt(dtrT!pes_esecid), bDallinizio, bVissforbudg, bAbortsforbudg, dbConn, nErrcode) Then
          dttTmp.Clear()
          Return False
        End If
        If dtrT!pes_tipork.ToString = "R" Or dtrT!pes_tipork.ToString = "S" Then ' rettifica manuale sul budget (autorizzato sforamento di spesa)
          dTRettqta = dTRettqta + NTSCDec(dtrT!pes_diffqta) * NTSCDec(dtrT!pes_segno)
          dTRettwork = dTRettwork + NTSCDec(dtrT!pes_diffwork) * NTSCDec(dtrT!pes_segno)
          dTRettcost = dTRettcost + NTSCDec(dtrT!pes_diffcost) * NTSCDec(dtrT!pes_segno)
        End If
      Next
      dttTmp.Clear()

      ' torna indietro i dati delle rettifiche manuali
      dRettqta = dTRettqta
      dRettwork = dTRettwork
      dRettcost = dTRettcost

      Return True


    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function AggTasksDao(ByVal strDitta As String, ByVal strNomtabin As String, ByVal lIIin As Integer, _
                                          ByVal strNomtabout As String, ByVal lIIout As Integer, ByVal lCommeca As Integer, _
                                          ByVal lEsecid As Integer, ByVal bDallinizio As Boolean, _
                                          ByVal bVissforbudg As Boolean, ByVal bAbortsforbudg As Boolean, _
                                          ByRef dbConn As DbConnection, ByRef nErrcode As Integer) As Boolean
    ' data i nomi (e gli instid) delle tabelle in e out , il numero di commessa e l'esecid , aggiorna tasks (o altra tabella temporanea con gli stessi campi)
    ' bVissforbudg se a true vis. e testa anche sformaneti del dato a budget...
    ' babortsforbudg se a true restituisce false anche per sformaneti del dato a budget...
    ' finoal è una data finale
    ' nErrcode è un numero di ritorno per comunicare al chiamante che tipo di errore si è verifato: es. superamento budget
    'USA ADO !!!
    ' tabella in = proesec/proeseb/ttproesec, tabella out = tasks o tttasks

    Dim strSQL As String = ""
    Dim dPctordcomp As Decimal
    Dim dPctworkcomp As Decimal

    Dim dttTas As New DataTable
    Dim dttPes As New DataTable

    Dim nSegno As Integer
    Dim strMemflevas As String
    Try

      strSQL = "SELECT * from " & strNomtabin & " where codditt = " & CStrSQL(strDitta) & " and pes_commeca = " & lCommeca & " and pes_esecid = " & lEsecid
      If lIIin <> 0 Then
        strSQL = strSQL & " and instid = " & lIIin
      End If
      'strSQL = strSQL & " order by
      ' qui distinguere gcn/gdb
      dttPes = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttPes.Rows.Count = 0 Then
        Throw New NTSException(oApp.Tr(Me, 129417430009501953, "record inesistente su  |" & strNomtabin & lCommeca & "/" & lEsecid & "| !  "))
        Return False
      End If

      strSQL = "SELECT * from " & strNomtabout & " where codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lCommeca & " and tsk_taskid = " & dttPes.Rows(0)!pes_taskid.ToString
      If lIIout <> 0 Then
        strSQL = strSQL & " and instid = " & lIIout
      End If
      ' qui distinguere gcn/gdb
      dttTas = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'objStd.ApriAdoRecordset False, strSQL, dttTas, bsOpenKeyset ' attenzione ! in aggiornamento
      If dttTas.Rows.Count = 0 Then
        Throw New NTSException(oApp.Tr(Me, 129417430919970703, "record inesistente su  |" & strNomtabout & lCommeca & "| id. rk esecuzione :| " & lEsecid & "| !  "))
        Return False
      End If
      ' ora aggiorna a seconda dei casi ...
      nSegno = NTSCInt(dttPes.Rows(0)!pes_segno)

      ' _____||||||| INSERIMENTO ||||||______
      If nSegno = 1 Then ' nuovo record o reinserimento dopo modifica
        Select Case dttPes.Rows(0)!pes_tipork.ToString
          'Case "L":  'ordlist
          'dttTas.Rows(0)!tsk_offcost = dttTas.Rows(0)!tsk_offcost + dttPes.Rows(0)!pes_costesec
          'dttTas.Rows(0)!tsk_offflevas = dttPes.Rows(0)!pes_salcon ' flag evasione


          'dttTas.Rows(0)!tsk_offremcost = dttTas.Rows(0)!tsk_offremcost - dttPes.Rows(0)!pes_costesec + dttPes.Rows(0)!pes_diffcost
          'dttTas.Rows(0)!tsk_offcostprg = dttTas.Rows(0)!tsk_offcost + dttTas.Rows(0)!tsk_offremcost
          'dttTas.Rows(0)!tsk_offdiffcost = dttTas.Rows(0)!tsk_offdiffcost + dttPes.Rows(0)!pes_diffcost


          Case "O" ' ordini
            ' campi actual (effettivi)
            dttTas.Rows(0)!tsk_ordcost = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttPes.Rows(0)!pes_costesec)
            dttTas.Rows(0)!tsk_ordwork = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttPes.Rows(0)!pes_workesec)
            dttTas.Rows(0)!tsk_ordqta = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec)

            dttTas.Rows(0)!tsk_ordflevas = dttPes.Rows(0)!pes_salcon ' flag evasione

            ' campi rem
            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_ordremcost = NTSCDec(dttTas.Rows(0)!tsk_ordremcost) - NTSCDec(dttPes.Rows(0)!pes_costesec) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
            End If
            dttTas.Rows(0)!tsk_ordremwork = NTSCDec(dttTas.Rows(0)!tsk_ordremwork) - NTSCDec(dttPes.Rows(0)!pes_workesec) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_ordremqta = NTSCDec(dttTas.Rows(0)!tsk_ordremqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec) + NTSCDec(dttPes.Rows(0)!pes_diffqta)

            ' acmpi current (programmati)
            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_ordcostprg = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttTas.Rows(0)!tsk_ordremcost)
            End If
            dttTas.Rows(0)!tsk_ordworkprg = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttTas.Rows(0)!tsk_ordremwork)
            dttTas.Rows(0)!tsk_ordqtaprg = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttTas.Rows(0)!tsk_ordremqta)

            ' campi diff
            dttTas.Rows(0)!tsk_orddiffcost = NTSCDec(dttTas.Rows(0)!tsk_orddiffcost) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
            dttTas.Rows(0)!tsk_orddiffwork = NTSCDec(dttTas.Rows(0)!tsk_orddiffwork) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_orddiffqta = NTSCDec(dttTas.Rows(0)!tsk_orddiffqta) + NTSCDec(dttPes.Rows(0)!pes_diffqta)



          Case Else ' movmag, priana, lavcent, manuali
            dttTas.Rows(0)!tsk_actwork = NTSCDec(dttTas.Rows(0)!tsk_actwork) + NTSCDec(dttPes.Rows(0)!pes_workesec)
            dttTas.Rows(0)!tsk_actqta = NTSCDec(dttTas.Rows(0)!tsk_actqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec)
            dttTas.Rows(0)!tsk_actcost = NTSCDec(dttTas.Rows(0)!tsk_actcost) + NTSCDec(dttPes.Rows(0)!pes_costesec)
            If dttPes.Rows(0)!pes_tipork.ToString <> "S" Then
              dttTas.Rows(0)!tsk_flevas = dttPes.Rows(0)!pes_salcon ' flag evasione
            End If
            If dttPes.Rows(0)!pes_tipork.ToString = "S" Or dttPes.Rows(0)!pes_tipork.ToString = "R" Then
              dttTas.Rows(0)!tsk_ordflevas = dttPes.Rows(0)!pes_salcon
            End If

            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_remcost = NTSCDec(dttTas.Rows(0)!tsk_remcost) - NTSCDec(dttPes.Rows(0)!pes_costesec) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
            End If
            dttTas.Rows(0)!tsk_remwork = NTSCDec(dttTas.Rows(0)!tsk_remwork) - NTSCDec(dttPes.Rows(0)!pes_workesec) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_remqta = NTSCDec(dttTas.Rows(0)!tsk_remqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec) + NTSCDec(dttPes.Rows(0)!pes_diffqta)
            ' come si vede non usiamo affatto per ora i campi pes_remworkdop, costdop e qtadop relativi
            ' probabilmente (per ora almeno) non servono...

            ' ora sistema i campi current
            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_cost = NTSCDec(dttTas.Rows(0)!tsk_actcost) + NTSCDec(dttTas.Rows(0)!tsk_remcost)
            End If
            dttTas.Rows(0)!tsk_work = NTSCDec(dttTas.Rows(0)!tsk_actwork) + NTSCDec(dttTas.Rows(0)!tsk_remwork)
            dttTas.Rows(0)!tsk_qta = NTSCDec(dttTas.Rows(0)!tsk_actqta) + NTSCDec(dttTas.Rows(0)!tsk_remqta)
            ' ora i campi diff
            dttTas.Rows(0)!tsk_diffcost = NTSCDec(dttTas.Rows(0)!tsk_diffcost) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
            dttTas.Rows(0)!tsk_diffwork = NTSCDec(dttTas.Rows(0)!tsk_diffwork) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_diffqta = NTSCDec(dttTas.Rows(0)!tsk_diffqta) + NTSCDec(dttPes.Rows(0)!pes_diffqta)

            ' ecceszione : se R allora gg. anche i dati ordini (rettifica valore manaule)

            If (dttPes.Rows(0)!pes_tipork.ToString = "R" Or dttPes.Rows(0)!pes_tipork.ToString = "S") And (dttTas.Rows(0)!tsk_tipotask.ToString = "1" Or dttTas.Rows(0)!tsk_tipotask.ToString = "3" Or dttTas.Rows(0)!tsk_tipotask.ToString = "5" Or dttTas.Rows(0)!tsk_tipotask.ToString = "7" Or dttTas.Rows(0)!tsk_tipotask.ToString = "8") Then
              dttTas.Rows(0)!tsk_orddiffcost = NTSCDec(dttTas.Rows(0)!tsk_orddiffcost) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
              dttTas.Rows(0)!tsk_orddiffwork = NTSCDec(dttTas.Rows(0)!tsk_orddiffwork) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
              dttTas.Rows(0)!tsk_orddiffqta = NTSCDec(dttTas.Rows(0)!tsk_orddiffqta) + NTSCDec(dttPes.Rows(0)!pes_diffqta)
              If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
                dttTas.Rows(0)!tsk_ordremcost = NTSCDec(dttTas.Rows(0)!tsk_ordremcost) - NTSCDec(dttPes.Rows(0)!pes_costesec) + NTSCDec(dttPes.Rows(0)!pes_diffcost)
              End If
              dttTas.Rows(0)!tsk_ordremwork = NTSCDec(dttTas.Rows(0)!tsk_ordremwork) - NTSCDec(dttPes.Rows(0)!pes_workesec) + NTSCDec(dttPes.Rows(0)!pes_diffwork)
              dttTas.Rows(0)!tsk_ordremqta = NTSCDec(dttTas.Rows(0)!tsk_ordremqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec) + NTSCDec(dttPes.Rows(0)!pes_diffqta)
              dttTas.Rows(0)!tsk_ordcost = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttPes.Rows(0)!pes_costesec)
              dttTas.Rows(0)!tsk_ordwork = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttPes.Rows(0)!pes_workesec)
              dttTas.Rows(0)!tsk_ordqta = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec)
              ' acmpi current (programmati)
              If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
                dttTas.Rows(0)!tsk_ordcostprg = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttTas.Rows(0)!tsk_ordremcost)
              End If
              dttTas.Rows(0)!tsk_ordworkprg = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttTas.Rows(0)!tsk_ordremwork)
              dttTas.Rows(0)!tsk_ordqtaprg = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttTas.Rows(0)!tsk_ordremqta)
            End If

            ' ora le date , se ...
            If dttTas.Rows(0)!tsk_progdate.ToString = "S" Or dttTas.Rows(0)!tsk_progdate.ToString = "F" Then
              If dttTas.Rows(0)!tsk_actstart.Equals(DBNull.Value) Then
                dttTas.Rows(0)!tsk_actstart = dttPes.Rows(0)!pes_data ' inizio effettvo
              End If
              If dttPes.Rows(0)!pes_salcon.ToString = "S" Then
                dttTas.Rows(0)!tsk_actfinish = dttPes.Rows(0)!pes_data ' fine effettivo
              End If
              ' ora sistema i current (=date programmate)
              If (Not (dttTas.Rows(0)!tsk_actstart.Equals(DBNull.Value))) And (dttTas.Rows(0)!tsk_startdate.Equals(DBNull.Value)) Then
                dttTas.Rows(0)!tsk_startdate = dttTas.Rows(0)!tsk_actstart ' data corrente inizio
              End If
              If (Not (dttTas.Rows(0)!tsk_actfinish.Equals(DBNull.Value))) And (dttTas.Rows(0)!tsk_finishdate.Equals(DBNull.Value)) Then
                dttTas.Rows(0)!tsk_finishdate = dttTas.Rows(0)!tsk_actfinish ' data corrente fine
              Else 'riprogramma automaticamnete la data fine, se vuota
                'If (IsNull(dttTas.Rows(0)!tsk_finishdate)) Then
                If Not (dttTas.Rows(0)!tsk_actstart.Equals(DBNull.Value)) And Not (dttTas.Rows(0)!tsk_basestart.Equals(DBNull.Value)) And Not (dttTas.Rows(0)!tsk_basefinish.Equals(DBNull.Value)) Then
                  dttTas.Rows(0)!tsk_finishdate = NTSCDate(dttTas.Rows(0)!tsk_actstart).AddDays(DateDiff("d", NTSCDate(dttTas.Rows(0)!tsk_basestart), NTSCDate(dttTas.Rows(0)!tsk_basefinish)))
                Else
                  dttTas.Rows(0)!tsk_finishdate = dttTas.Rows(0)!tsk_basefinish
                End If
                'End If
              End If
            End If
        End Select
      End If
      '---||||||||| .... storno ...____||||||||||||||
      If nSegno = -1 Then ' storno
        Select Case dttPes.Rows(0)!pes_tipork.ToString
          'Case "L":  'ordlist
          'dttTas.Rows(0)!tsk_offcost = dttTas.Rows(0)!tsk_offcost - dttPes.Rows(0)!pes_costesec
          'strMemflevas = dttTas.Rows(0)!tsk_offflevas ' salva il valore prima dello storno
          'dttTas.Rows(0)!tsk_offflevas = "C"  ' flag evasione che va sempre a C , in caso di storno

          ' qui forse, se prima era a saldo, o sempre, sarà necessario ricostruire il rem (rimanente) rileggendo le righe di proesec fino alla data ..
          ' da vedere successivamente...

          'dttTas.Rows(0)!tsk_offremcost = dttTas.Rows(0)!tsk_offremcost + dttPes.Rows(0)!pes_costesec - dttPes.Rows(0)!pes_diffcost
          'dttTas.Rows(0)!tsk_offcostprg = dttTas.Rows(0)!tsk_offcost + dttTas.Rows(0)!tsk_offremcost
          'dttTas.Rows(0)!tsk_offdiffcost = dttTas.Rows(0)!tsk_offdiffcost - dttPes.Rows(0)!pes_diffcost


          Case "O" ' ordini
            dttTas.Rows(0)!tsk_ordcost = NTSCDec(dttTas.Rows(0)!tsk_ordcost) - NTSCDec(dttPes.Rows(0)!pes_costesec)
            dttTas.Rows(0)!tsk_ordwork = NTSCDec(dttTas.Rows(0)!tsk_ordwork) - NTSCDec(dttPes.Rows(0)!pes_workesec)
            dttTas.Rows(0)!tsk_ordqta = NTSCDec(dttTas.Rows(0)!tsk_ordqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec)

            strMemflevas = dttTas.Rows(0)!tsk_ordflevas.ToString
            dttTas.Rows(0)!tsk_ordflevas = "C" ' flag evasione

            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_ordremcost = NTSCDec(dttTas.Rows(0)!tsk_ordremcost) + NTSCDec(dttPes.Rows(0)!pes_costesec) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
            End If
            dttTas.Rows(0)!tsk_ordremwork = NTSCDec(dttTas.Rows(0)!tsk_ordremwork) + NTSCDec(dttPes.Rows(0)!pes_workesec) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_ordremqta = NTSCDec(dttTas.Rows(0)!tsk_ordremqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec) - NTSCDec(dttPes.Rows(0)!pes_diffqta)

            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_ordcostprg = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttTas.Rows(0)!tsk_ordremcost)
            End If
            dttTas.Rows(0)!tsk_ordworkprg = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttTas.Rows(0)!tsk_ordremwork)
            dttTas.Rows(0)!tsk_ordqtaprg = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttTas.Rows(0)!tsk_ordremqta)

            dttTas.Rows(0)!tsk_orddiffcost = NTSCDec(dttTas.Rows(0)!tsk_orddiffcost) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
            dttTas.Rows(0)!tsk_orddiffwork = NTSCDec(dttTas.Rows(0)!tsk_orddiffwork) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_orddiffqta = NTSCDec(dttTas.Rows(0)!tsk_orddiffqta) - NTSCDec(dttPes.Rows(0)!pes_diffqta)



          Case Else ' movmag, priana, lavcent, manuali
            dttTas.Rows(0)!tsk_actwork = NTSCDec(dttTas.Rows(0)!tsk_actwork) - NTSCDec(dttPes.Rows(0)!pes_workesec)
            dttTas.Rows(0)!tsk_actqta = NTSCDec(dttTas.Rows(0)!tsk_actqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec)
            dttTas.Rows(0)!tsk_actcost = NTSCDec(dttTas.Rows(0)!tsk_actcost) - NTSCDec(dttPes.Rows(0)!pes_costesec)
            strMemflevas = dttTas.Rows(0)!tsk_flevas.ToString
            dttTas.Rows(0)!tsk_flevas = "C" ' flag evasione
            If dttPes.Rows(0)!pes_tipork.ToString = "S" Or dttPes.Rows(0)!pes_tipork.ToString = "R" Then
              dttTas.Rows(0)!tsk_ordflevas = "C"
            End If

            ' qui forse, se prima era a saldo, o sempre, sarà necessario ricostruire il rem (rimanente) rileggendo le righe di proesec fino alla data ..
            ' da vedere successivamente...

            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_remcost = NTSCDec(dttTas.Rows(0)!tsk_remcost) + NTSCDec(dttPes.Rows(0)!pes_costesec) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
            End If
            dttTas.Rows(0)!tsk_remwork = NTSCDec(dttTas.Rows(0)!tsk_remwork) + NTSCDec(dttPes.Rows(0)!pes_workesec) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_remqta = NTSCDec(dttTas.Rows(0)!tsk_remqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec) - NTSCDec(dttPes.Rows(0)!pes_diffqta)

            ' come si vede non usiamo affatto per ora i campi pes_remworkdop, costdop e qtadop relativi
            ' ora sistema i campi current
            If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
              dttTas.Rows(0)!tsk_cost = NTSCDec(dttTas.Rows(0)!tsk_actcost) + NTSCDec(dttTas.Rows(0)!tsk_remcost)
            End If
            dttTas.Rows(0)!tsk_work = NTSCDec(dttTas.Rows(0)!tsk_actwork) + NTSCDec(dttTas.Rows(0)!tsk_remwork)
            dttTas.Rows(0)!tsk_qta = NTSCDec(dttTas.Rows(0)!tsk_actqta) + NTSCDec(dttTas.Rows(0)!tsk_remqta)
            ' ora i campi diff
            dttTas.Rows(0)!tsk_diffcost = NTSCDec(dttTas.Rows(0)!tsk_diffcost) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
            dttTas.Rows(0)!tsk_diffwork = NTSCDec(dttTas.Rows(0)!tsk_diffwork) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
            dttTas.Rows(0)!tsk_diffqta = NTSCDec(dttTas.Rows(0)!tsk_diffqta) - NTSCDec(dttPes.Rows(0)!pes_diffqta)

            ' eccezione : rettifica manuale inciode anche sugli ordini
            If (dttPes.Rows(0)!pes_tipork.ToString = "R" Or dttPes.Rows(0)!pes_tipork.ToString = "S") And (dttTas.Rows(0)!tsk_tipotask.ToString = "1" Or dttTas.Rows(0)!tsk_tipotask.ToString = "3" Or dttTas.Rows(0)!tsk_tipotask.ToString = "5" Or dttTas.Rows(0)!tsk_tipotask.ToString = "7" Or dttTas.Rows(0)!tsk_tipotask.ToString = "8") Then
              dttTas.Rows(0)!tsk_ordcost = NTSCDec(dttTas.Rows(0)!tsk_ordcost) - NTSCDec(dttPes.Rows(0)!pes_costesec)
              dttTas.Rows(0)!tsk_ordwork = NTSCDec(dttTas.Rows(0)!tsk_ordwork) - NTSCDec(dttPes.Rows(0)!pes_workesec)
              dttTas.Rows(0)!tsk_ordqta = NTSCDec(dttTas.Rows(0)!tsk_ordqta) - NTSCDec(dttPes.Rows(0)!pes_qtaesec)
              If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
                dttTas.Rows(0)!tsk_ordremcost = NTSCDec(dttTas.Rows(0)!tsk_ordremcost) + NTSCDec(dttPes.Rows(0)!pes_costesec) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
              End If
              dttTas.Rows(0)!tsk_ordremwork = NTSCDec(dttTas.Rows(0)!tsk_ordremwork) + NTSCDec(dttPes.Rows(0)!pes_workesec) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
              dttTas.Rows(0)!tsk_ordremqta = NTSCDec(dttTas.Rows(0)!tsk_ordremqta) + NTSCDec(dttPes.Rows(0)!pes_qtaesec) - NTSCDec(dttPes.Rows(0)!pes_diffqta)
              If dttTas.Rows(0)!tsk_prevgrup.ToString <> "F" Then
                dttTas.Rows(0)!tsk_ordcostprg = NTSCDec(dttTas.Rows(0)!tsk_ordcost) + NTSCDec(dttTas.Rows(0)!tsk_ordremcost)
              End If
              dttTas.Rows(0)!tsk_ordworkprg = NTSCDec(dttTas.Rows(0)!tsk_ordwork) + NTSCDec(dttTas.Rows(0)!tsk_ordremwork)
              dttTas.Rows(0)!tsk_ordqtaprg = NTSCDec(dttTas.Rows(0)!tsk_ordqta) + NTSCDec(dttTas.Rows(0)!tsk_ordremqta)

              dttTas.Rows(0)!tsk_orddiffcost = NTSCDec(dttTas.Rows(0)!tsk_orddiffcost) - NTSCDec(dttPes.Rows(0)!pes_diffcost)
              dttTas.Rows(0)!tsk_orddiffwork = NTSCDec(dttTas.Rows(0)!tsk_orddiffwork) - NTSCDec(dttPes.Rows(0)!pes_diffwork)
              dttTas.Rows(0)!tsk_orddiffqta = NTSCDec(dttTas.Rows(0)!tsk_orddiffqta) - NTSCDec(dttPes.Rows(0)!pes_diffqta)
            End If
            ' ora non ripristina le date per ora......
            'If dttTas.Rows(0)!tsk_progdate = "S" Then
            '   If IsNull(dttTas.Rows(0)!tsk_actstart) Then
            '      dttTas.Rows(0)!tsk_actstart = dttPes.Rows(0)!pes_data
            '   End If
            '   If dttPes.Rows(0)!pes_salcon = "S" Then
            '      dttTas.Rows(0)!tsk_actfinish = dttPes.Rows(0)!pes_data
            '   End If
            '   ' ora sistema i current
            '   If Not (IsNull(dttTas.Rows(0)!tsk_actstart)) Then
            '      dttTas.Rows(0)!tsk_startdate = dttTas.Rows(0)!tsk_actstart ' data corrente inizio
            '   End If
            '   If Not (IsNull(dttTas.Rows(0)!tsk_actfinish)) Then
            '      dttTas.Rows(0)!tsk_finishdate = dttTas.Rows(0)!tsk_actfinish ' data corrente fine
            '   Else
            '      If Not (IsNull(dttTas.Rows(0)!tsk_actstart)) And Not (IsNull(dttTas.Rows(0)!tsk_basestart)) And Not (IsNull(dttTas.Rows(0)!tsk_basefinish)) Then
            '        dttTas.Rows(0)!tsk_finishdate = dttTas.Rows(0)!tsk_actstart + DateDiff("d", dttTas.Rows(0)!tsk_basefinish, dttTas.Rows(0)!tsk_basestart)
            '      Else
            '        dttTas.Rows(0)!tsk_finishdate = dttTas.Rows(0)!tsk_basefinish
            '      End If
            '   End If
            ' End If
        End Select

      End If
      ' ora i campi in più in  fondo (totali) e il flag rilasciato..
      ' determina la % di completamento effettiva e su ordini
      If NTSCDec(dttTas.Rows(0)!tsk_cost) <> 0 Then
        dPctworkcomp = NTSCDec(dttTas.Rows(0)!tsk_actcost) / NTSCDec(dttTas.Rows(0)!tsk_cost) * 100 ' (rapporto tra eseguito e programmato)
      Else
        dPctworkcomp = 0
      End If
      If NTSCDec(dttTas.Rows(0)!tsk_ordcostprg) <> 0 Then
        dPctordcomp = NTSCDec(dttTas.Rows(0)!tsk_ordcost) / NTSCDec(dttTas.Rows(0)!tsk_ordcostprg) * 100 ' (rapporto tra eseguito e programmato)
      Else
        dPctordcomp = 0
      End If
      ' aggiorna il campo di tasks
      dttTas.Rows(0)!tsk_pctworkcomp = dPctworkcomp
      dttTas.Rows(0)!tsk_pctordcomp = dPctordcomp


      If (dttTas.Rows(0)!tsk_ordflevas.ToString = "S" Or dttTas.Rows(0)!tsk_ordflevas.ToString = "V") And dttTas.Rows(0)!tsk_flevas.ToString = "C" And NTSCDec(dttTas.Rows(0)!tsk_ordcostprg) <> 0 Then
        dttTas.Rows(0)!tsk_totcost = dttTas.Rows(0)!tsk_ordcostprg 'prevalenza ordini
        dttTas.Rows(0)!tsk_totdiffcost = dttTas.Rows(0)!tsk_orddiffcost
        dttTas.Rows(0)!tsk_totwork = dttTas.Rows(0)!tsk_ordworkprg 'prevalenza ordini
        dttTas.Rows(0)!tsk_totdiffwork = dttTas.Rows(0)!tsk_orddiffwork
        dttTas.Rows(0)!tsk_totqta = dttTas.Rows(0)!tsk_ordqtaprg 'prevalenza ordini
        dttTas.Rows(0)!tsk_totdiffqta = dttTas.Rows(0)!tsk_orddiffqta
        dttTas.Rows(0)!tsk_totactcost = dttTas.Rows(0)!tsk_ordcost 'prevalenza ordini su eseguito
        dttTas.Rows(0)!tsk_totremcost = dttTas.Rows(0)!tsk_ordremcost
      Else
        'If dttTas.Rows(0)!tsk_cost >= dttTas.Rows(0)!tsk_ordcostprg Then 'assunzione preregrina, ma...
        If dPctworkcomp >= dPctordcomp Then ' molto meglio
          dttTas.Rows(0)!tsk_totcost = dttTas.Rows(0)!tsk_cost
          dttTas.Rows(0)!tsk_totdiffcost = dttTas.Rows(0)!tsk_diffcost
          dttTas.Rows(0)!tsk_totwork = dttTas.Rows(0)!tsk_work
          dttTas.Rows(0)!tsk_totdiffwork = dttTas.Rows(0)!tsk_diffwork
          dttTas.Rows(0)!tsk_totqta = dttTas.Rows(0)!tsk_qta
          dttTas.Rows(0)!tsk_totdiffqta = dttTas.Rows(0)!tsk_diffqta
          dttTas.Rows(0)!tsk_totactcost = dttTas.Rows(0)!tsk_actcost 'prevalenza ordini su eseguito
          dttTas.Rows(0)!tsk_totremcost = dttTas.Rows(0)!tsk_remcost

        Else ' gli ordini sono maggiori, com epercentuale di completamento
          dttTas.Rows(0)!tsk_totcost = dttTas.Rows(0)!tsk_ordcostprg
          dttTas.Rows(0)!tsk_totdiffcost = dttTas.Rows(0)!tsk_orddiffcost
          dttTas.Rows(0)!tsk_totwork = dttTas.Rows(0)!tsk_ordworkprg 'prevalenza ordini
          dttTas.Rows(0)!tsk_totdiffwork = dttTas.Rows(0)!tsk_orddiffwork
          dttTas.Rows(0)!tsk_totqta = dttTas.Rows(0)!tsk_ordqtaprg 'prevalenza ordini
          dttTas.Rows(0)!tsk_totdiffqta = dttTas.Rows(0)!tsk_orddiffqta
          dttTas.Rows(0)!tsk_totactcost = dttTas.Rows(0)!tsk_ordcost 'prevalenza ordini su eseguito
          dttTas.Rows(0)!tsk_totremcost = dttTas.Rows(0)!tsk_ordremcost
        End If
      End If
      If dttTas.Rows(0)!tsk_rilasciato.ToString = "N" And dttTas.Rows(0)!tsk_flevas.ToString = "S" Then
        dttTas.Rows(0)!tsk_rilasciato = "S"
      End If
      ' aggiorna tasks, alla fine

      'riverso snaTas nel DB
      strSQL = "UPDATE " & strNomtabout & " SET "
      strSQL += oCldBase.GetQueryUpdate(dttTas, dttTas.Rows(0), "tsk_")
      strSQL += " WHERE  codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lCommeca & " and tsk_taskid = " & dttPes.Rows(0)!pes_taskid.ToString
      If lIIout <> 0 Then strSQL += " AND instid = " & lIIout
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      dttTas.Clear()
      dttPes.Clear()


      ' tutto ok, sembra
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTas.Clear()
      dttPes.Clear()
    End Try
  End Function
  Public Overridable Function LegTaskGr(ByVal strDitta As String, ByVal strCommecain As String, _
                                        ByVal strTaskidin As String, ByVal strNometabin As String, _
                                        ByVal lInstidin As Integer, ByVal nObbl As Boolean, _
                                        ByRef strValueout As String, ByRef strDaraveout As String, _
                                        ByRef strTipoTask As String, ByRef strGestcost As String, _
                                        ByRef strPrevGrup As String, ByRef strSummary As String, _
                                        ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      If (strTaskidin = "0" Or strCommecain = "0") And nObbl = False Then
        strValueout = ""
        strDaraveout = ""
        Return True
      End If
      If (strTaskidin = "" Or strCommecain = "") And nObbl = False Then
        strValueout = ""
        strDaraveout = ""
        Return True
      End If

      If Val(strCommecain) = 0 Or Val(strTaskidin) = 0 Then
        'MsgBox("Inserire numero commessa e task-id diversi da zero", vbExclamation, bsTtlMsg)
        strValueout = "******"
        strDaraveout = ""
        Return True
      End If
      strTaskidin = CStr(Val(strTaskidin))
      strCommecain = CStr(Val(strCommecain))

      strSQL = " SELECT tsk_descr, tsk_darave, tsk_tipotask, tsk_gestcost, tsk_prevgrup, tsk_summary FROM " & strNometabin & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tsk_commeca = " & strCommecain & _
               " AND tsk_taskid = " & strTaskidin & " "
      If lInstidin <> 0 Then
        strSQL = strSQL & " and instid = " & lInstidin & " "
      End If
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count = 0 Then
        strValueout = ""
        strDaraveout = ""
        Return False
      Else
        strValueout = NTSCStr(dttTmp.Rows(0)!tsk_descr)
        strDaraveout = NTSCStr(dttTmp.Rows(0)!tsk_darave)
        strTipoTask = NTSCStr(dttTmp.Rows(0)!tsk_tipotask)
        strGestcost = NTSCStr(dttTmp.Rows(0)!tsk_gestcost)
        strPrevGrup = NTSCStr(dttTmp.Rows(0)!tsk_prevgrup)
        strSummary = NTSCStr(dttTmp.Rows(0)!tsk_summary)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function LegGestCostPadreDao(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskid As Integer) As String
    '--- ritorna la gest. costi del padre del task passato
    ' si suppone che si sappia che il task passato è un prevgrup = 'F' !!!

    ' lo aggiorna da proeseb e restituisce descrizioni costi e qtabtità residue del task
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lUidpadre As Integer = 0

    LegGestCostPadreDao = "2" ' per default

    Try
      strSQL = "SELECT tsk_uidpadre from TASKS where codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lCommeca & " and tsk_taskid = " & lTaskid
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then lUidpadre = NTSCInt(dttTmp.Rows(0)!tsk_uidpadre)
      dttTmp.Clear()

      If lUidpadre <> 0 Then  'Se non è un capitolo
        strSQL = "SELECT tsk_gestcost from TASKS WHERE codditt = " & CStrSQL(strDitta) & " And tsk_commeca=" & lCommeca & " and tsk_taskid=" & lUidpadre
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then LegGestCostPadreDao = NTSCStr(dttTmp.Rows(0)!tsk_gestcost)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function LegTaskRk(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskID As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      If lCommeca = 0 Or lTaskID = 0 Then Return False

      strSQL = "SELECT tsk_taskid FROM TASKS WHERE codditt = " & CStrSQL(strDitta) & _
               " and tsk_commeca = " & lCommeca & " and tsk_taskid = " & lTaskID
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ControllaSalconFratelliDao(ByVal strDitta As String, ByVal strNomtabttpr As String, _
                                              ByVal lIIttproesebappo As Integer, ByVal strNomtabtttasks As String, _
                                              ByVal lIItttasks As Integer, ByVal lCommeca As Integer, _
                                              ByVal lTaskid As Integer, ByVal bDaordini As Boolean, _
                                              ByRef strMess As String, ByRef dbConn As DbConnection) As Boolean
    ' chiamata in before update di record con prevgrup = 'F' and PMSALCON = 'S'
    ' verifica se tutti gli altri fratelòli sono già in situazione salcon = Q in caso contrario restituisce una stringa con l'elenco
    ' dei fratelli non q , e false
    ' in caso contrario True = tutti i fratelli sono Q, in quel momento...
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim lTaskidpadre As Integer = 0
    Dim strMess10 As String = ""
    Dim dRettqta As Decimal, dRettwork As Decimal, dRettcost As Decimal
    Dim nRes As Integer = 0
    Dim nErrcode As Integer = 0
    Try
      strMess = ""

      strSQL = "SELECT tsk_uidpadre FROM tasks WHERE codditt = " & CStrSQL(strDitta) & _
               " And tsk_commeca = " & lCommeca & " and tsk_taskid = " & lTaskid
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        lTaskidpadre = NTSCInt(dttTmp.Rows(0)!tsk_uidpadre)
      End If
      dttTmp.Clear()

      strSQL = "SELECT tsk_taskid FROM tasks WHERE codditt = " & CStrSQL(strDitta) & _
               " And tsk_commeca = " & lCommeca & " and tsk_uidpadre = " & lTaskidpadre & _
               " and tsk_taskid <> " & lTaskid & " ORDER BY tsk_taskid"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        Return True ' nessun fratello
      End If

      For Each dtrT As DataRow In dttTmp.Rows
        dRettqta = 0 : dRettwork = 0 : dRettcost = 0
        ' adesso inserisce nel temporaneo quel record di tasks, vuoto... di valori
        strSQL = "INSERT INTO TTTASKS ( codditt, instid , tsk_commeca, tsk_taskid,  tsk_codcapi,  tsk_descapi,  tsk_codtarg,  tsk_destarg,  tsk_codctap,  tsk_desctap,  tsk_codctas,  tsk_descr,  tsk_posiz, " & _
             "  tsk_livello ,  tsk_uidpadre,  tsk_summary,  tsk_prevgrup,  tsk_progdate,  tsk_startdate,  tsk_finishdate,  tsk_actstart,  tsk_actfinish,  tsk_basestart,  tsk_basefinish,  tsk_ultagg, " & _
             "  tsk_datins,  tsk_opnomeo,  tsk_opnome,  tsk_deadline,  tsk_basecostun,  tsk_baseprzun,  tsk_baseprz,  tsk_work,  tsk_actwork,  tsk_basework,  tsk_remwork,  tsk_qta,  tsk_actqta, " & _
             "  tsk_baseqta,  tsk_remqta,  tsk_flevas,  tsk_cost,  tsk_actcost,  tsk_basecost,  tsk_remcost,  tsk_pctworkcomp,  tsk_prevnum,  tsk_prevrev,  tsk_varid, "
        strSQL = strSQL & " tsk_progprev,  tsk_codforn,  tsk_codinca,  tsk_coddestin,  tsk_success,  tsk_relsucc,  tsk_codttas,  tsk_codart,  tsk_codlavo,  tsk_contoca,  tsk_codcent,  tsk_codcena,  tsk_darave,  tsk_codcfam, " & _
             "   tsk_escompca,  tsk_ump,  tsk_umwork,  tsk_datini,  tsk_datfin,  tsk_data1,  tsk_data2,  tsk_data3,  tsk_data4,  tsk_data5,  tsk_stato,  tsk_note1,  tsk_note2,  tsk_offcost, " & _
             "  tsk_ordcost,  tsk_offflevas,  tsk_ordflevas,  tsk_offremcost,  tsk_ordremcost,  tsk_tipotask,  tsk_gestcost,  tsk_sulcant,  tsk_rdaem,  tsk_genrda,  tsk_ricprev,  tsk_percpz,  tsk_percpr, " & _
             "  tsk_impbase,  tsk_rilasciato,  tsk_codrumod,    tsk_codvcl,  tsk_perqta,   tsk_posiz0, " & _
             "  tsk_uid0,  tsk_posiz1,  tsk_uid1,  tsk_posiz2,  tsk_uid2,  tsk_posiz3,  tsk_uid3, tsk_nlayout, tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcostprg, tsk_orddiffcost, tsk_offcostprg, tsk_offdiffcost, tsk_totcost, tsk_totdiffcost, " & _
             " tsk_ordqta, tsk_ordqtaprg, tsk_orddiffqta, tsk_ordremqta, tsk_ordwork, tsk_ordworkprg, tsk_orddiffwork, tsk_ordremwork, tsk_totqta, tsk_totdiffqta, tsk_totwork, tsk_totdiffwork, tsk_offqta, tsk_offqtaprg, tsk_offdiffqta, tsk_offremqta, tsk_offwork, tsk_offworkprg, tsk_offdiffwork, tsk_offremwork, tsk_ricdur, tsk_dur, tsk_actdur, tsk_remdur, tsk_basedur, tsk_diffdur, tsk_nfigli, tsk_pctordcomp, tsk_totactcost, tsk_totremcost )  "
        strSQL = strSQL & "  SELECT codditt, " & lIItttasks & " , tsk_commeca,  tsk_taskid,  tsk_codcapi,  tsk_descapi,  tsk_codtarg,  tsk_destarg,  tsk_codctap,  tsk_desctap,  tsk_codctas,  tsk_descr,  tsk_posiz, " & _
              "  tsk_livello ,  tsk_uidpadre,  tsk_summary,  tsk_prevgrup,  tsk_progdate,  tsk_startdate,  tsk_finishdate,  tsk_actstart,  tsk_actfinish,  tsk_basestart,  tsk_basefinish,  tsk_ultagg, " & _
              "  tsk_datins,  tsk_opnomeo,  tsk_opnome,  tsk_deadline,  tsk_basecostun,  tsk_baseprzun,  tsk_baseprz,  tsk_basework,  0,  tsk_basework,  tsk_basework,  tsk_baseqta,  0, " & _
              "  tsk_baseqta,  tsk_baseqta,  'C',  tsk_basecost,  0,  tsk_basecost,  tsk_basecost,  0,  tsk_prevnum,  tsk_prevrev,  tsk_varid, "
        strSQL = strSQL & "  tsk_progprev,  tsk_codforn,  tsk_codinca,  tsk_coddestin,  tsk_success,  tsk_relsucc,  tsk_codttas,  tsk_codart,  tsk_codlavo,  tsk_contoca,  tsk_codcent,  tsk_codcena,  tsk_darave,  tsk_codcfam, " & _
             "   tsk_escompca,  tsk_ump,  tsk_umwork,  tsk_datini,  tsk_datfin,  tsk_data1,  tsk_data2,  tsk_data3,  tsk_data4,  tsk_data5,  tsk_stato,  tsk_note1,  tsk_note2,  0, " & _
             "  0,  'C',  'C',  tsk_basecost,  tsk_basecost,  tsk_tipotask,  tsk_gestcost,  tsk_sulcant,  tsk_rdaem,  tsk_genrda,  tsk_ricprev,  tsk_percpz,  tsk_percpr, " & _
             "  tsk_impbase,  tsk_rilasciato,  tsk_codrumod,    tsk_codvcl,  tsk_perqta,   tsk_posiz0, " & _
             "  tsk_uid0,  tsk_posiz1,  tsk_uid1,  tsk_posiz2,  tsk_uid2,  tsk_posiz3,  tsk_uid3, tsk_nlayout, 0, 0, 0, tsk_basecost, 0, tsk_basecost, 0, tsk_basecost, 0, " & _
             "  0, tsk_baseqta, 0, tsk_baseqta, 0, tsk_basework, 0, tsk_basework, tsk_baseqta, 0, tsk_basework, 0, 0, tsk_baseqta, 0, tsk_baseqta, 0, tsk_basework, 0, tsk_basework, tsk_ricdur, tsk_dur, tsk_actdur, tsk_remdur, tsk_basedur, tsk_diffdur, tsk_nfigli, 0, 0, tsk_basecost "
        strSQL = strSQL & " FROM TASKS " & _
             " WHERE tasks.codditt = " & CStrSQL(strDitta) & " and tsk_commeca = " & lCommeca & " and tsk_taskid =" & dtrT!tsk_taskid.ToString & _
             " AND NOT EXISTS (SELECT codditt, tsk_commeca, tsk_taskid FROM tttasks " & _
                                    " WHERE instid = " & lIItttasks & _
                                    " And tttasks.codditt = " & CStrSQL(strDitta) & _
                                    " And tsk_commeca = " & lCommeca & _
                                    " And tsk_taskid = " & dtrT!tsk_taskid.ToString & _
                                    " And tttasks.codditt = tasks.codditt " & _
                                    " And tttasks.tsk_commeca = tasks.tsk_commeca " & _
                                    " And tttasks.tsk_taskid = tasks.tsk_taskid)"
        nRes = oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If Not (nRes = 1 Or nRes = 0) Then ' potrebbe già esserci il record dentro ..
          strMess10 = "errore in inserimento riga di task in tttasks..."
          GoTo esci1
        End If
        ' adesso chiama la routine che la aggiorna dall'inizio
        If Not AggTasksCompleteDao(strDitta, strNomtabttpr, lIIttproesebappo, strNomtabtttasks, lIItttasks, lCommeca, NTSCInt(dtrT!tsk_taskid), 0, True, False, False, IntSetDate("31/12/2099"), dbConn, nErrcode, dRettqta, dRettwork, dRettcost) Then
          strMess10 = "Errore in AggTaskCompleteDao"
          GoTo esci1
        End If
        strSQL = " SELECT tsk_flevas, tsk_ordflevas FROM " & strNomtabtttasks & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " and instid= " & lIItttasks & _
                   " and tsk_commeca = " & lCommeca & _
                   " and tsk_taskid = " & dtrT!tsk_taskid.ToString
        dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp1.Rows.Count > 0 Then
          If bDaordini And (NTSCStr(dttTmp1.Rows(0)!tsk_ordflevas) <> "Q" And NTSCStr(dttTmp1.Rows(0)!tsk_ordflevas) <> "S") Then
            strMess10 = strMess10 & " task n. " & dtrT!tsk_taskid.ToString
          End If
          If Not (bDaordini) And (NTSCStr(dttTmp1.Rows(0)!tsk_flevas) <> "Q" And NTSCStr(dttTmp1.Rows(0)!tsk_flevas) <> "S") Then
            strMess10 = strMess10 & " task n. " & dtrT!tsk_taskid.ToString
          End If
        Else
          ' non può essere !!
        End If
        dttTmp1.Clear()

      Next
      dttTmp.Clear()
esci1:

      If strMess10 <> "" Then
        ControllaSalconFratelliDao = False
        strMess = strMess10
      Else
        ControllaSalconFratelliDao = True
        strMess = ""
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggProesebDaPrianaDao(ByVal strDitta As String, ByVal nOperaz As Integer, _
                                                    ByVal strNomtabTT As String, ByVal lII As Integer, _
                                                    ByVal strNomtabPes As String, ByVal lIIpes As Integer, _
                                                    ByVal lPaprogr As Integer, ByVal dbconn As DbConnection, _
                                                    ByRef dttPriana As DataTable) As Boolean
    '
    'nOperaz = 0 =  inserisci
    '         = 1 = cancella
    '         = 2 = modifica !!!!! (PER ORA GESTITO SOLO 0=INS 1=storno !!!
    ' strNomtabTT è PRIANA oppute TTPRIANA o altro con la stessa struttura, tabella di partenza
    ' lII è l'instid relativo se applicabile
    ' scrive SEMPRE su PROESEB, invece
    'UTILIZZA DAO (chiamato da cipria ecc  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontare i dati di movmag con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    'IN VB6, se la tabella era TTPRIANA come parametro veniva passato l'INSTID di ttpriana
    'in NET, da BNCGPRIN priana è in memoria: passo il datatable

    'OCCHIO: strNomtabPes può essere: PROESEC, PROESEB, TTPROESEC

    Dim strSQL As String = ""
    Dim lEsecid As Integer
    Dim dQtaesec As Decimal, dWorkesec As Decimal, dDiffwork As Decimal, dDiffqta As Decimal
    Dim dRemqtadop As Decimal, dRemworkdop As Decimal
    Dim dCostesec As Decimal, dDiffcost As Decimal, dRemcostdop As Decimal
    Dim dMoltipsegno As Decimal
    Dim strOut As String = "", strDaraveout As String = "", strTipoTask As String = "", strGestcost As String = "", strPrevGrup As String = "", strSummary As String = ""

    Dim dtrPriana As DataRow = Nothing
    Dim dttIn As New DataTable
    Try

      AggProesebDaPrianaDao = False

      'differenza rispetto a vb6: 
      'se mi è stato passato il datatable prendo il dato dal datatable, diversamente faccio la select sulla tabella

      If strNomtabTT = "TTPRIANA" And lII = 0 Then
        Dim dtrT1() As DataRow = dttPriana.Select("pa_pmtaskid <> 0 AND pa_commeca <> 0 AND pa_progr = " & lPaprogr.ToString)
        If dtrT1.Length = 0 Then Return True
        dtrPriana = dtrT1(0)
      Else
        'caso standard: leggo dal database il record
        If strNomtabTT = "PRIANA2" Then
          strSQL = " SELECT * FROM priana2" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND pa2_progr = " & lPaprogr & _
                   " AND pa2_idwbs <> 0 " & _
                   " AND pa2_commeca <> 0 "
        Else
          strSQL = " SELECT * FROM " & strNomtabTT & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND pa_progr = " & lPaprogr & _
                     " AND pa_pmtaskid <> 0 " & _
                     " AND pa_commeca <> 0 "
          If lII <> 0 Then
            strSQL = strSQL & " AND instid = " & lII & " "
          End If
        End If
        dttIn = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbconn)
        If dttIn.Rows.Count = 0 Then
          Return True
          'Throw New NTSException(oApp.Tr(Me, 129418886282460937, " Errore ! record |" & lPaprogr & "| inesistente su |" & strNomtabTT & "| in fase aggiornamento proeseb !!"))
          'Return False
        End If
        If strNomtabTT = "PRIANA2" Then
          dttIn.Columns("pa2_idwbs").ColumnName = "pa_pmtaskid"
          For Each oCol As DataColumn In dttIn.Columns
            If oCol.ColumnName.StartsWith("pa2_") Then oCol.ColumnName = "pa_" & oCol.ColumnName.Substring(4)
          Next
        End If
        dtrPriana = dttIn.Select("pa_progr = " & lPaprogr.ToString)(0)
      End If    'If strNomtabTT = "TTPRIANA" And lII = 0 Then

      'non serve più
      ''---------------------------------------
      ''riprende la routine come in vb6
      'If NTSCInt(dtrPriana!pa_commeca) = 0 Or NTSCInt(dtrPriana!pa_pmtaskid) = 0 Then
      '  'MsgBox " Errore ! Commessa zero o taskid zero in fase aggiornamento proeseb !!", vbExclamation, "Errore"
      '  Return True
      'End If

      ' legge il progressivo di proesec/proeseb dat tabnuma
      lEsecid = oCldBase.LegNuma(strDitta, "PE", " ", NTSCInt(dtrPriana!pa_commeca), True, dbconn)
      lEsecid = oCldBase.AggNuma(strDitta, "PE", " ", NTSCInt(dtrPriana!pa_commeca), lEsecid, False, True, "", dbconn)

      ' adesso determina le variabili da scrivere :

      ' serve perchè pa_pmvaldis e pa_pmqtadis sono moltiplicati * -1 se segno è avere
      If NTSCStr(dtrPriana!pa_darave) = "A" Then
        dMoltipsegno = -1
      Else
        dMoltipsegno = 1
      End If

      If Not LegTaskGr(strDitta, NTSCInt(dtrPriana!pa_commeca).ToString, NTSCInt(dtrPriana!pa_pmtaskid).ToString, _
                       "TASKS", 0, False, strOut, strDaraveout, strTipoTask, strGestcost, strPrevGrup, strSummary, Nothing) Then
        'non ' obbligatorio: non do messaggi
      End If

      If NTSCStr(dtrPriana!pa_origine) = "O" Then
        ' manodopera : gestisce lavoro
        dQtaesec = 0
        If strGestcost = "2" Then
          'Solo costo
          dWorkesec = 0
        Else
          'Costo e lavoro
          dWorkesec = NTSCDec(dtrPriana!pa_quantda)
        End If
        dCostesec = NTSCDec(dtrPriana!pa_importoda)

        dRemqtadop = 0
        dRemworkdop = 0
        dRemcostdop = 0

        dDiffqta = 0
        If strGestcost = "2" Then
          'Solo costo
          dDiffwork = 0
        Else
          'Costo e lavoro
          dDiffwork = NTSCDec(dtrPriana!pa_quantda) - (dMoltipsegno * (NTSCDec(dtrPriana!pa_pmqtadis)))
        End If
        dDiffcost = NTSCDec(dtrPriana!pa_importoda) - (dMoltipsegno * (NTSCDec(dtrPriana!pa_pmvaldis)))
      Else
        ' altro : gestisce quantità
        If strGestcost = "2" Then
          'Solo costo
          dQtaesec = 0
        Else
          'Costo e qta
          dQtaesec = NTSCDec(dtrPriana!pa_quantda)
        End If
        dWorkesec = 0
        dCostesec = NTSCDec(dtrPriana!pa_importoda)

        dRemqtadop = 0
        dRemworkdop = 0
        dRemcostdop = 0
        If strGestcost = "2" Then
          'Solo costo
          dDiffqta = 0
        Else
          'Costo e qta
          dDiffqta = NTSCDec(dtrPriana!pa_quantda) - (dMoltipsegno * (NTSCDec(dtrPriana!pa_pmqtadis)))
        End If
        dDiffwork = 0
        dDiffcost = NTSCDec(dtrPriana!pa_importoda) - (dMoltipsegno * (NTSCDec(dtrPriana!pa_pmvaldis)))
        'dtrPriana!xxo_pmvaldisda
      End If


      ' ora scrive, finalmente
      If lIIpes <> 0 Then
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, instid , "
      Else
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, "
      End If
      strSQL = strSQL & " pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec, " & _
         " pes_workesec,  pes_salcon,  pes_pctworkcomp,  pes_diffwork,  pes_remworkdop, " & _
         " pes_diffqta,  pes_remqtadop,  pes_costesec, pes_data,  pes_diffcost,  pes_remcostdop,  pes_note," & _
         " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie, " & _
         " pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno, pes_mmserie, pes_mmnum,  pes_mmriga, " & _
         " pes_paprogr,  pes_olprogr ) VALUES "
      If lIIpes <> 0 Then
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & ", " & lIIpes & " , "
      Else
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & ", "
      End If
      strSQL = strSQL & "  " & NTSCInt(dtrPriana!pa_commeca) & ", " & lEsecid & ", " & dtrPriana!pa_pmtaskid.ToString & ", " & CDblSQL(dQtaesec) & "," & _
           CDblSQL(dWorkesec) & ",'" & dtrPriana!pa_pmsalcon.ToString & "', 0, " & CDblSQL(dDiffwork) & "," & CDblSQL(dRemworkdop) & "," & _
           CDblSQL(dDiffqta) & "," & CDblSQL(dRemqtadop) & "," & CDblSQL(dCostesec) & "," & CDataSQL(NTSCDate(dtrPriana!pa_datreg)) & "," & CDblSQL(dDiffcost) & "," & CDblSQL(dRemcostdop) & ", LEFT(" & CStrSQL(dtrPriana!pa_note) & ", 255),"
      strSQL = strSQL & CStrSQL(oApp.User.Nome) & "," & IIf(nOperaz = 0, 1, -1).ToString & ", 'P', Null,0,null, " & _
           " 0,0,null,0,null,0,0, " & NTSCInt(dtrPriana!pa_progr).ToString & ", 0 ) "

      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbconn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttIn.Clear()
    End Try
  End Function
  Public Overridable Function AggProeseb(ByVal strChiamante As String, ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                     ByVal strDatreg As String, ByVal lNumreg As Integer, _
                                     ByRef dttPriana As DataTable, ByVal strState As String) As Boolean
    'AGGIORNO PROESEB DEL PROJECT MANAGEMENT (INSERT / UPDATE / DELETE)
    'LA ROUTINE E' CHIAMATA DA BDCGPRIN, BDCGDIST, BDCGINSO, ....
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Dim strIdNew As String = ""     'sono le nuove righe di ttpriana che non c'erano in priana all'apertura della registrazione
    Try
      'se è una delete devo solo cancellare tutto !!!
      If strChiamante = "BSCIPRIA" Then
        'in strDatreg c'è l'elenco dei pa_progr gestiti nella registrazione e
        'in lNumreg c'è lIITTPrianax
      End If

      If strState = "D" Then
        strTmp = "0"
        strIdNew = ""
        GoTo CANCELLA
      End If

      ' cerca i record modificati
AGGIORNA:
      For Each dtrT As DataRow In dttPriana.Rows

        If dttPriana.TableName = "PRIANA2" Then
          'NELLA CADC ci sono sempre 2 righe di CA, ma SOLO in una sola ci può essere il taskid: scarto quella senza task
          If NTSCInt(dtrT!pa_pmtaskid) = 0 Then Continue For
        End If
        strTmp += NTSCInt(dtrT!pa_progr).ToString & ", "   'mi servirà dopo ...

        If dttPriana.TableName = "PRIANA2" Then
          strSQL = "SELECT pa2_progr as pa_progr, pa2_commeca as pa_commeca, pa2_idwbs as pa_pmtaskid, " & _
                   " pa2_quantda as pa_quantda, pa2_importoda as pa_importoda, pa2_pmvaldis as pa_pmvaldis, " & _
                   " pa2_pmsalcon as pa_pmsalcon, pa2_pmqtadis as pa_pmqtadis FROM priana2 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND pa2_progr = " & NTSCInt(dtrT!pa_progr).ToString & _
                   " AND pa2_idwbs <> 0"
        Else
          strSQL = "SELECT pa_progr, pa_commeca, pa_pmtaskid, pa_quantda, pa_importoda, pa_pmvaldis, " & _
                   " pa_pmsalcon, pa_pmqtadis FROM priana " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND pa_progr = " & dtrT!pa_progr.ToString
        End If

        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn) 'ottengo priana di origine con pa_progr = a quello in memoria
        If dttTmp.Rows.Count = 0 Then strIdNew += NTSCInt(dtrT!pa_progr).ToString & ", "

        'se i dati di pm sono cambiati tra record di origine e quello in memoria, storno e riaggiorno PROESEB
        For Each dtrP As DataRow In dttTmp.Rows
          If (Not (NTSCInt(dtrT!pa_commeca) = NTSCInt(dtrP!pa_commeca) And _
                   NTSCInt(dtrT!pa_pmtaskid) = NTSCInt(dtrP!pa_pmtaskid) And _
                   NTSCDec(dtrT!pa_quantda) = NTSCDec(dtrP!pa_quantda) And _
                   NTSCDec(dtrT!pa_importoda) = NTSCDec(dtrP!pa_importoda) And _
                   NTSCDec(dtrT!pa_pmvaldis) = NTSCDec(dtrP!pa_pmvaldis) And _
                   NTSCStr(dtrT!pa_pmsalcon) = NTSCStr(dtrP!pa_pmsalcon) And _
                   NTSCDec(dtrT!pa_pmqtadis) = NTSCDec(dtrP!pa_pmqtadis))) And _
                  (NTSCInt(dtrT!pa_pmtaskid) <> 0 Or NTSCInt(dtrP!pa_pmtaskid) <> 0) Then
            ' ora crea peroeseb storno
            If NTSCInt(dtrP!pa_pmtaskid) <> 0 Then
              AggProesebDaPrianaDao(strDitta, 1, IIf(dttPriana.TableName = "PRIANA2", "PRIANA2", "PRIANA").ToString, 0, "PROESEB", 0, NTSCInt(dtrP!pa_progr), dbConn, Nothing)
            End If
            ' ora crea peroeseb dati nuovi
            If NTSCInt(dtrT!pa_pmtaskid) <> 0 Then
              If strChiamante = "BSCIPRIA" Then
                AggProesebDaPrianaDao(strDitta, 0, "TTPRIANAX", lNumreg, "PROESEB", 0, NTSCInt(dtrT!pa_progr), dbConn, Nothing)
              Else
                'tutti gli altri casi
                AggProesebDaPrianaDao(strDitta, 0, "TTPRIANA", 0, "PROESEB", 0, NTSCInt(dtrT!pa_progr), dbConn, dttPriana)
              End If
            End If
          End If
        Next    'For Each dtrP As DataRow In dttPriaOrig.Rows

        dttTmp.Clear()
      Next    'For Each dtrT As DataRow In dttPriana.Rows
      If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 2)

      '--------------------------------------------
      'ora cancella i record in priana che non ci sono più in ttpriana
      'se dalla registraz. originaria di CG ho cancellato l'unica riga che rimandava alla CA,
      'dttPriana è vuota... devo comunque eseguire la delete 
CANCELLA:

      If strChiamante = "BSCIPRIA" Then
        'in strDatreg c'è l'elenco dei pa_progr gestiti nella registrazione e
        'in lNumreg c'è lIITTPrianax
        strSQL = "SELECT priana.pa_progr FROM priana LEFT JOIN TTPRIANAX ON priana.codditt = ttprianax.codditt and priana.pa_progr = ttprianax.pa_progr" & _
                  " WHERE priana.codditt = " & CStrSQL(strDitta) & _
                  " AND priana.pa_progr IN " & strDatreg & _
                  " AND ttprianax.instid IS NULL " & _
                  " AND priana.pa_pmtaskid <> 0"
      Else
        'tutti gli altri casi
        If dttPriana.TableName = "PRIANA2" Then
          strSQL = "SELECT pa2_progr as pa_progr FROM priana2 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND pa2_idwbs <> 0"

          If strDatreg = "***" Then
            'da bncxpria viene passato solo lNumreg (ed in strDatreg '***') che corrisponde a pa2_progr
            strSQL += " AND pa2_progr = " & lNumreg.ToString
          Else
            'da bncgprin, bncgdist, bncginso
            strSQL += " AND pa2_datreg = " & CDataSQL(strDatreg) & _
                      " AND pa2_numreg = " & lNumreg.ToString
          End If

          If strTmp.Length > 0 Then strSQL += " AND pa2_progr NOT IN (" & strTmp & ")"
        Else
          strSQL = "SELECT pa_progr FROM priana " & _
           " WHERE codditt = " & CStrSQL(strDitta) & _
           " AND pa_pmtaskid <> 0"
          strSQL += " AND pa_datreg = " & CDataSQL(strDatreg) & _
                    " AND pa_numreg = " & lNumreg.ToString
          If strTmp.Length > 0 Then strSQL += " AND pa_progr NOT IN (" & strTmp & ")"
        End If
      End If

      If strDatreg.trim <> "" Then
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      For Each dtrT As DataRow In dttTmp.Rows
        AggProesebDaPrianaDao(strDitta, 1, IIf(dttPriana.TableName = "PRIANA2", "PRIANA2", "PRIANA").ToString, 0, "PROESEB", 0, NTSCInt(dtrT!pa_progr), dbConn, Nothing)
      Next    'For Each dtrT As DataRow In dttPriana.Rows
      dttTmp.Clear()

INSERISCI:
      ' ora inserisce i record nuovi di ttpriana che non c'erano in priana
      If strChiamante = "BSCIPRIA" Then
        'in strDatreg c'è l'elenco dei pa_progr gestiti nella registrazione e
        'in lNumreg c'è lIITTPrianax
        strSQL = "SELECT TTPRIANAX.* FROM priana RIGHT JOIN TTPRIANAX ON priana.codditt = ttprianax.codditt AND priana.pa_progr = ttprianax.pa_progr" & _
                " WHERE TTPRIANAX.codditt = " & CStrSQL(strDitta) & _
                " AND priana.pa_numreg IS NULL" & _
                " AND TTPRIANAX.instid = " & lNumreg
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For Each dtrT As DataRow In dttTmp.Rows
          AggProesebDaPrianaDao(strDitta, 0, "TTPRIANAX", lNumreg, "PROESEB", 0, NTSCInt(dtrT!pa_progr), dbConn, Nothing)
        Next    'For Each dtrT As DataRow In dttPriana.Rows
        dttTmp.Clear()
      Else
        'tutti gli altri casi
        If strIdNew.Length > 0 Then
          strIdNew = strIdNew.Substring(0, strIdNew.Length - 2)
          For Each dtrT As DataRow In dttPriana.Select("pa_pmtaskid <> 0 AND pa_progr IN (" & strIdNew & ")")
            AggProesebDaPrianaDao(strDitta, 0, "TTPRIANA", 0, "PROESEB", 0, NTSCInt(dtrT!pa_progr), dbConn, dttPriana)
          Next
        End If    'If strIdNew.Length > 0 Then
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaMovmag(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer) As Integer
    ' ritorna il numero di esecid da un record di priana, per una determinata commessa, se indicata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    TrovaEsecIdDaMovmag = 0
    Try
      strSQL = "SELECT pes_esecid from " & strNometabProesec & " where codditt = " & CStrSQL(strDitta) & _
               " And pes_tipork='M' and pes_mmtipork = '" & strTipork & "' and  pes_mmanno = " & nAnno & _
               " and pes_mmserie ='" & strSerie & "' and pes_mmnum = " & lNumdoc & _
               "  and pes_mmriga = " & lMmriga & " "
      If lCommeca <> 0 Then
        strSQL = strSQL & " and pes_commeca = " & lCommeca
      End If
      If lIITtproesec <> 0 Then
        strSQL = strSQL & " and instid = " & lIITtproesec
      End If
      strSQL = strSQL & " order by pes_esecid DESC" ' prende il più recente
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!pes_esecid)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaMovord(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer) As Integer
    ' ritorna il numero di esecid da un record di priana, per una determinata commessa, se indicata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    TrovaEsecIdDaMovord = 0
    Try
      strSQL = "SELECT pes_esecid from " & strNometabProesec & " where codditt = " & CStrSQL(strDitta) & _
               " And pes_tipork='O' and pes_ortipork = '" & strTipork & "' and  pes_oranno = " & nAnno & _
               " and pes_orserie ='" & strSerie & "' and pes_ornum = " & lNumdoc & _
               "  and pes_orriga = " & lMmriga & " "
      If lCommeca <> 0 Then
        strSQL = strSQL & " and pes_commeca = " & lCommeca
      End If
      If lIITtproesec <> 0 Then
        strSQL = strSQL & " and instid = " & lIITtproesec
      End If
      strSQL = strSQL & " order by pes_esecid DESC" ' prende il più recente
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!pes_esecid)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaLavcent(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer, ByVal lMmrigaa As Integer) As Integer
    ' ritorna il numero di esecid da un record di priana, per una determinata commessa, se indicata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    TrovaEsecIdDaLavcent = 0
    Try
      strSQL = "SELECT pes_esecid from " & strNometabProesec & " where codditt = " & CStrSQL(strDitta) & _
               " And pes_tipork='L' and pes_mmtipork = '" & strTipork & "' and  pes_mmanno = " & nAnno & _
               " and pes_mmserie ='" & strSerie & "' and pes_mmnum = " & lNumdoc & _
               " and pes_mmriga = " & lMmriga & " and pes_olprogr = " & lMmrigaa
      If lCommeca <> 0 Then
        strSQL = strSQL & " and pes_commeca = " & lCommeca
      End If
      If lIITtproesec <> 0 Then
        strSQL = strSQL & " and instid = " & lIITtproesec
      End If
      strSQL = strSQL & " order by pes_esecid DESC" ' prende il più recente
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!pes_esecid)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function AggProesebDaMovmagDao(ByVal strDitta As String, ByVal nOperaz As Integer, _
                                                    ByVal strNomtabTT As String, ByVal lII As Integer, _
                                                    ByVal strNomtabPes As String, ByVal lIIpes As Integer, _
                                                    ByVal strTipork As String, ByVal nAnno As Integer, _
                                                    ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                    ByVal lRiga As Integer, ByVal strDatdoc As String, _
                                                    ByRef dbConn As DbConnection, _
                                                    ByRef dttMMtrans As DataTable) As Boolean
    ' se lriga = 0 -> tutto il documento
    'UTILIZZA DAO (chiamato da veboll  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontere i dati di movmag con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    'nOperaz = 0 =  inserisci
    '         = 1 = cancella
    '         = 2 = modifica !!!!! (PER ORA GESTITO SOLO 0=INS 1=storno !!!
    ' strNomtabTT è MOVMAG oppute MMTRANS o altro con la stessa struttura, tabella di partenza
    ' lII è l'instid relativo se applicabile
    ' scrive SEMPRE su PROESEB, invece
    'UTILIZZA DAO (chiamato da gsor ecc  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontare i dati di movmag con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    'IN VB6, se la tabella era MMTRANS come parametro veniva passato l'INSTID di mmtrans
    'in NET, da BNVEBOLL mmtrans è in memoria: passo il datatable

    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim lEsecid As Integer
    Dim dQtaesec As Decimal, dWorkesec As Decimal, dDiffwork As Decimal, dDiffqta As Decimal
    Dim dRemqtadop As Decimal, dRemworkdop As Decimal
    Dim dCostesec As Decimal, dDiffcost As Decimal, dRemcostdop As Decimal
    Dim dMoltipsegno As Decimal
    Dim strOut As String = "", strDaraveout As String = "", strTipoTask As String = ""
    Dim strGestcost As String = "", strPrevGrup As String = "", strSummary As String = ""

    Dim dtrMM As DataRow
    Dim strPref As String = "mm_"

    Try
      If strNomtabTT = "MMTRANS" And lII = 0 Then
        dtrMM = dttMMtrans.Select("ec_riga = " & lRiga.ToString)(0)
        strPref = "ec_"
      Else
        'caso standard: leggo dal database il record
        strSQL = " SELECT * FROM " & strNomtabTT & _
           " WHERE codditt = " & CStrSQL(strDitta) & _
           " AND mm_riga = " & lRiga & " "
        If lII = 0 Then ' è lettura da movmag
          strSQL = strSQL & " AND mm_tipork= '" & strTipork & "' AND mm_anno = " & nAnno & _
                            " AND mm_serie ='" & strSerie & "' AND mm_numdoc = " & lNumdoc & "   "
        End If
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          Throw New NTSException(oApp.Tr(Me, 129442300044472656, " Errore ! record |" & lRiga & "| inesistente su |" & strNomtabTT & "| in fase aggiornamento proeseb !!"))
          Return False
        End If
        dtrMM = dttTmp.Rows(0)
      End If    'If strNomtabTT = "TTPRIANA" And lII = 0 Then

      If NTSCInt(dtrMM(strPref & "commeca")) = 0 Or NTSCInt(dtrMM(strPref & "pmtaskid")) = 0 Then
        Return True ' va comuqnue bene lo stesso.....
      End If
      ' legge il progressivo di proesec/proeseb dat tabnuma
      lEsecid = oCldBase.LegNuma(strDitta, "PE", " ", NTSCInt(dtrMM(strPref & "commeca")), True, dbConn)
      lEsecid = oCldBase.AggNuma(strDitta, "PE", " ", NTSCInt(dtrMM(strPref & "commeca")), lEsecid, False, True, "", dbConn)

      ' adesso determina le variabili da scrivere :

      ' serve perchè mo_pmvaldis e mo_pmqtadis sono moltiplicati * -1 se segno è avere
      If (lII <> 0) Then  ' da MMTRANS
        If NTSCStr(dtrMM(strPref & "darave")) = "A" Then
          dMoltipsegno = -1
        Else
          dMoltipsegno = 1
        End If
      End If
      If lII = 0 Then     ' da MOVMAG
        strSQL = "SELECT tb_testci FROM tabcaum WHERE tb_codcaum = " & NTSCInt(dtrMM(strPref & "causale"))
        dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp1.Rows.Count > 0 Then
          Select Case NTSCStr(dttTmp1.Rows(0)!tb_testci)
            Case "A", "B"
              dMoltipsegno = -1
            Case "D", "E"
              dMoltipsegno = 1
          End Select
        Else
          Throw New NTSException(oApp.Tr(Me, 129442304273955078, "Errore in fase aggiornamento proeseb !!! Causale |" & _
                                         dtrMM(strPref & "causale").ToString & "| indicata su |" & strNomtabTT & "| inesistente."))
          Return False
        End If
        dttTmp1.Clear()
      End If

      If Not LegTaskGr(strDitta, NTSCStr(dtrMM(strPref & "commeca")), NTSCStr(dtrMM(strPref & "pmtaskid")), _
                 "TASKS", 0, False, strOut, strDaraveout, strTipoTask, strGestcost, strPrevGrup, strSummary, Nothing) Then
        'non ' obbligatorio: non do messaggi
      End If

      ' altro : gestisce quantità
      If strGestcost = "2" Then
        'Solo costo
        dQtaesec = 0
      Else
        'Costo e qta
        dQtaesec = NTSCDec(dtrMM(strPref & "quant"))
      End If
      dWorkesec = 0
      dCostesec = NTSCDec(dtrMM(strPref & "valore"))

      dRemqtadop = 0
      dRemworkdop = 0
      dRemcostdop = 0

      If strGestcost = "2" Then
        'Solo costo
        dDiffqta = 0
      Else
        'Costo e qta
        dDiffqta = NTSCDec(dtrMM(strPref & "quant")) - (dMoltipsegno * NTSCDec(dtrMM(strPref & "pmqtadis")))
      End If
      dDiffwork = 0
      dDiffcost = NTSCDec(dtrMM(strPref & "valore")) - (dMoltipsegno * NTSCDec(dtrMM(strPref & "pmvaldis")))  ' determina la diff. rilevata

      ' ora scrive, finalmente
      If lIIpes <> 0 Then
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, instid , "
      Else
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, "
      End If
      strSQL = strSQL & " pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec, " & _
         " pes_workesec,  pes_salcon,  pes_pctworkcomp,  pes_diffwork,  pes_remworkdop, " & _
         " pes_diffqta,  pes_remqtadop,  pes_costesec, pes_data,  pes_diffcost,  pes_remcostdop,  pes_note," & _
         " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie, " & _
         " pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno, pes_mmserie, pes_mmnum,  pes_mmriga, " & _
         " pes_paprogr,  pes_olprogr ) VALUES "
      If lIIpes <> 0 Then
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , " & lIIpes & " , "
      Else
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , "
      End If
      strSQL = strSQL & "  " & NTSCStr(dtrMM(strPref & "commeca")) & ", " & lEsecid & ", " & _
                NTSCStr(dtrMM(strPref & "pmtaskid")) & ", " & CDblSQL(dQtaesec) & "," & _
                CDblSQL(dWorkesec) & ",'" & NTSCStr(dtrMM(strPref & "pmsalcon")) & "', 0, " & _
                CDblSQL(dDiffwork) & "," & CDblSQL(dRemworkdop) & "," & CDblSQL(dDiffqta) & "," & _
                CDblSQL(dRemqtadop) & "," & CDblSQL(dCostesec) & "," & CDataSQL(strDatdoc) & "," & _
                CDblSQL(dDiffcost) & "," & CDblSQL(dRemcostdop) & _
                ", LEFT(" & CStrSQL(NTSCStr(dtrMM(strPref & "note"))) & ",255), " & _
                CStrSQL(oApp.User.Nome) & "," & (IIf(nOperaz = 0, 1, -1)).ToString & _
                ", 'M', null,0,null,0,0, '" & strTipork & "'," & nAnno & ",'" & strSerie & "', " & _
                " " & lNumdoc & "," & lRiga & ", 0, 0 ) "
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

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
  Public Overridable Function AggProesebDaMovordDao(ByVal strDitta As String, ByVal nOperaz As Integer, _
                                                    ByVal strNomtabTT As String, ByVal lII As Integer, _
                                                    ByVal strNomtabPes As String, ByVal lIIpes As Integer, _
                                                    ByVal strTipork As String, ByVal nAnno As Integer, _
                                                    ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                    ByVal lRiga As Integer, ByVal strDatdoc As String, _
                                                    ByRef dbConn As DbConnection, _
                                                    ByRef dttMOtrans As DataTable) As Boolean
    ' se lriga = 0 -> tutto il documento
    'UTILIZZA DAO (chiamato da gsor  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontere i dati di movmag con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    'nOperaz = 0 =  inserisci
    '         = 1 = cancella
    '         = 2 = modifica !!!!! (PER ORA GESTITO SOLO 0=INS 1=storno !!!
    ' strNomtabTT è MOVORD oppute MOTRANS o altro con la stessa struttura, tabella di partenza
    ' lII è l'instid relativo se applicabile
    ' scrive SEMPRE su PROESEB, invece
    'UTILIZZA DAO (chiamato da gsor ecc  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontare i dati di movmag con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    'IN VB6, se la tabella era MOTRANS come parametro veniva passato l'INSTID di motrans
    'in NET, da BBORGSOR motrans è in memoria: passo il datatable

    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim lEsecid As Integer
    Dim dQtaesec As Decimal, dWorkesec As Decimal, dDiffwork As Decimal, dDiffqta As Decimal
    Dim dRemqtadop As Decimal, dRemworkdop As Decimal
    Dim dCostesec As Decimal, dDiffcost As Decimal, dRemcostdop As Decimal
    Dim dMoltipsegno As Decimal
    Dim strOut As String = "", strDaraveout As String = "", strTipoTask As String = ""
    Dim strGestcost As String = "", strPrevGrup As String = "", strSummary As String = ""

    Dim dtrMO As DataRow
    Dim strPref As String = "mo_"

    Try
      If strNomtabTT = "MOTRANS" And lII = 0 Then
        dtrMO = dttMOtrans.Select("ec_riga = " & lRiga.ToString)(0)
        strPref = "ec_"
      Else
        'caso standard: leggo dal database il record
        strSQL = " SELECT * FROM " & strNomtabTT & _
           " WHERE codditt = " & CStrSQL(strDitta) & _
           " AND mo_riga = " & lRiga & " "
        If lII = 0 Then ' è lettura da movmag
          strSQL = strSQL & " AND mo_tipork= '" & strTipork & "' AND mo_anno = " & nAnno & _
                            " AND mo_serie ='" & strSerie & "' AND mo_numord = " & lNumdoc & "   "
        End If
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          Throw New NTSException(oApp.Tr(Me, 129446576838876953, " Errore ! record |" & lRiga & "| inesistente su |" & strNomtabTT & "| in fase aggiornamento proeseb !!"))
          Return False
        End If
        dtrMO = dttTmp.Rows(0)
      End If    'If strNomtabTT = "TTPRIANA" And lII = 0 Then

      If NTSCInt(dtrMO(strPref & "commeca")) = 0 Or NTSCInt(dtrMO(strPref & "pmtaskid")) = 0 Then
        Return True ' va comuqnue bene lo stesso.....
      End If
      ' legge il progressivo di proesec/proeseb dat tabnuma
      lEsecid = oCldBase.LegNuma(strDitta, "PE", " ", NTSCInt(dtrMO(strPref & "commeca")), True, dbConn)
      lEsecid = oCldBase.AggNuma(strDitta, "PE", " ", NTSCInt(dtrMO(strPref & "commeca")), lEsecid, False, True, "", dbConn)

      ' adesso determina le variabili da scrivere :

      ' serve perchè mo_pmvaldis e mo_pmqtadis sono moltiplicati * -1 se segno è avere
      If (lII <> 0) Then  ' da MOTRANS
        If NTSCStr(dtrMO(strPref & "darave")) = "A" Then
          dMoltipsegno = -1
        Else
          dMoltipsegno = 1
        End If
      End If
      If lII = 0 Then     ' da MOVORD
        If dtrMO(strPref & "tipork").ToString = "R" Then    'Impegno cliente: Avere
          dMoltipsegno = -1
        Else
          dMoltipsegno = 1
        End If
      End If

      

      If Not LegTaskGr(strDitta, NTSCStr(dtrMO(strPref & "commeca")), NTSCStr(dtrMO(strPref & "pmtaskid")), _
                 "TASKS", 0, False, strOut, strDaraveout, strTipoTask, strGestcost, strPrevGrup, strSummary, Nothing) Then
        'non ' obbligatorio: non do messaggi
      End If

      ' altro : gestisce quantità
      If strGestcost = "2" Then
        'Solo costo
        dQtaesec = 0
      Else
        'Costo e qta
        dQtaesec = NTSCDec(dtrMO(strPref & "quant"))
      End If
      dWorkesec = 0
      dCostesec = NTSCDec(dtrMO(strPref & "valoremm"))

      dRemqtadop = 0
      dRemworkdop = 0
      dRemcostdop = 0

      If strGestcost = "2" Then
        'Solo costo
        dDiffqta = 0
      Else
        'Costo e qta
        dDiffqta = NTSCDec(dtrMO(strPref & "quant")) - (dMoltipsegno * NTSCDec(dtrMO(strPref & "pmqtadis")))
      End If
      dDiffwork = 0
      dDiffcost = NTSCDec(dtrMO(strPref & "valoremm")) - (dMoltipsegno * NTSCDec(dtrMO(strPref & "pmvaldis")))  ' determina la diff. rilevata

      ' ora scrive, finalmente
      If lIIpes <> 0 Then
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, instid , "
      Else
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, "
      End If
      strSQL = strSQL & " pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec, " & _
         " pes_workesec,  pes_salcon,  pes_pctworkcomp,  pes_diffwork,  pes_remworkdop, " & _
         " pes_diffqta,  pes_remqtadop,  pes_costesec, pes_data,  pes_diffcost,  pes_remcostdop,  pes_note," & _
         " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie, " & _
         " pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno, pes_mmserie, pes_mmnum,  pes_mmriga, " & _
         " pes_paprogr,  pes_olprogr ) VALUES "
      If lIIpes <> 0 Then
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , " & lIIpes & " , "
      Else
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , "
      End If
      strSQL = strSQL & "  " & NTSCStr(dtrMO(strPref & "commeca")) & ", " & lEsecid & ", " & _
                NTSCStr(dtrMO(strPref & "pmtaskid")) & ", " & CDblSQL(dQtaesec) & "," & _
                CDblSQL(dWorkesec) & ",'" & NTSCStr(dtrMO(strPref & "pmsalcon")) & "', 0, " & _
                CDblSQL(dDiffwork) & "," & CDblSQL(dRemworkdop) & "," & CDblSQL(dDiffqta) & "," & _
                CDblSQL(dRemqtadop) & "," & CDblSQL(dCostesec) & "," & CDataSQL(strDatdoc) & "," & _
                CDblSQL(dDiffcost) & "," & CDblSQL(dRemcostdop) & _
                ", LEFT(" & CStrSQL(NTSCStr(dtrMO(strPref & "note"))) & ",255), " & _
                CStrSQL(oApp.User.Nome) & "," & (IIf(nOperaz = 0, 1, -1)).ToString & _
                ", 'O', '" & strTipork & "'," & nAnno & ",'" & strSerie & "', " & _
                 " " & lNumdoc & "," & lRiga & ",null,0,null,0,0, " & _
                 " 0, 0 ) "
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

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
  Public Overridable Function AggProesebDaLavcentDao(ByVal strDitta As String, ByVal nOperaz As Integer, _
                                                ByVal strNomtabTT As String, ByVal lII As Integer, _
                                                ByVal strNomtabPes As String, ByVal lIIpes As Integer, _
                                                ByVal strTipork As String, ByVal nAnno As Integer, _
                                                ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                ByVal lRiga As Integer, ByVal lRigaa As Integer, _
                                                ByVal strDatdoc As String, ByRef dbConn As DbConnection, _
                                                ByRef dttMMlavce As DataTable) As Boolean
    ' UTILIZZA DAO (chiamato da dbavla  ... in transazione, ma anche non in transazione)
    ' strnomtabtt e lii usati solo per le modifiche , per poter confrontere i dati di lavcent con quelli del temporaneo e memorizzare solo le differenze in proeseb...

    ' nOperaz = 0 = inserisci
    '         = 1 = cancella
    '         = 2 = modifica !!!!! (PER ORA GESTITO SOLO 0=ins 1=storno !!!

    ' strNomtabTT è LAVCENT oppute MMLAVCE o altro con la stessa struttura, è la tabella di partenza
    ' lII è l'instid relativo, se applicabile
    ' scrive SEMPRE su PROESEB

    'IN VB6, se la tabella era MMlavce come parametro veniva passato l'INSTID di mmlavce
    'in NET, da BNDBAVLA mmlavce è in memoria: passo il datatable

    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim lEsecid As Integer
    Dim dQtaesec As Decimal, dWorkesec As Decimal, dDiffwork As Decimal, dDiffqta As Decimal
    Dim dRemqtadop As Decimal, dRemworkdop As Decimal
    Dim dCostesec As Decimal, dDiffcost As Decimal, dRemcostdop As Decimal
    Dim dMoltipsegno As Decimal
    Dim strOut As String = "", strDaraveout As String = "", strTipoTask As String = ""
    Dim strGestcost As String = "", strPrevGrup As String = "", strSummary As String = ""

    Dim dtrLce As DataRow

    Try
      If strNomtabTT = "MMLAVCE" And lII = 0 Then
        dtrLce = dttMMlavce.Select("lce_riga = " & lRiga.ToString & " AND lce_rigaa = " & lRigaa.ToString)(0)
      Else
        'caso standard: leggo dal database il record
        strSQL = " SELECT * FROM " & strNomtabTT & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND lce_riga = " & lRiga & " " & _
                 " AND lce_rigaa = " & lRigaa & " "
        If lII = 0 Then ' è lettura da movmag
          strSQL = strSQL & " AND lce_tipork= '" & strTipork & "' AND lce_anno = " & nAnno & _
                            " AND lce_serie ='" & strSerie & "' AND lce_numdoc = " & lNumdoc & "   "
        End If
        dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          Throw New NTSException(oApp.Tr(Me, 129449292973154297, " Errore ! record |" & lRiga & "| fase n° |" & lRigaa & "| inesistente su |" & strNomtabTT & "| in fase aggiornamento proeseb !!"))
          Return False
        End If
        dtrLce = dttTmp.Rows(0)
      End If    'If strNomtabTT = "TTPRIANA" And lII = 0 Then

      If NTSCInt(dtrLce("lce_commeca")) = 0 Or NTSCInt(dtrLce("lce_pmtaskid")) = 0 Then
        Return True ' va comuqnue bene lo stesso.....
      End If
      ' legge il progressivo di proesec/proeseb dat tabnuma
      lEsecid = oCldBase.LegNuma(strDitta, "PE", " ", NTSCInt(dtrLce("lce_commeca")), True, dbConn)
      lEsecid = oCldBase.AggNuma(strDitta, "PE", " ", NTSCInt(dtrLce("lce_commeca")), lEsecid, False, True, "", dbConn)

      ' adesso determina le variabili da scrivere :

      dMoltipsegno = 1

      If Not LegTaskGr(strDitta, NTSCStr(dtrLce("lce_commeca")), NTSCStr(dtrLce("lce_pmtaskid")), _
                 "TASKS", 0, False, strOut, strDaraveout, strTipoTask, strGestcost, strPrevGrup, strSummary, Nothing) Then
        'non ' obbligatorio: non do messaggi
      End If

      Select Case strGestcost
        Case "2"
          'Solo costo
          dQtaesec = 0
          dWorkesec = 0
        Case "4"
          'Costo e lavoro
          dQtaesec = 0
          dWorkesec = NTSCDec(dtrLce!lce_tempese)
        Case "5"
          'Costo, lavoro e qta
          dQtaesec = NTSCDec(dtrLce!lce_qtaes)
          dWorkesec = NTSCDec(dtrLce!lce_tempese)
      End Select
      dCostesec = NTSCDec(dtrLce!lce_valore)

      dRemqtadop = 0
      dRemworkdop = 0
      dRemcostdop = 0

      'Si determina la diff. rilevata
      Select Case strGestcost
        Case "2"
          'Solo costo
          dDiffqta = 0
          dDiffwork = 0
        Case "4"
          'Costo e lavoro
          dDiffqta = 0
          dDiffwork = NTSCDec(dtrLce!lce_tempese) - (dMoltipsegno * NTSCDec(dtrLce!lce_pmqtadis))
        Case "5"
          'Costo, lavoro e qta
          dDiffqta = NTSCDec(dtrLce!lce_qtaes) - (dMoltipsegno * NTSCDec(dtrLce!lce_pmqtadisq))
          dDiffwork = NTSCDec(dtrLce!lce_tempese) - (dMoltipsegno * NTSCDec(dtrLce!lce_pmqtadis))
      End Select
      dDiffcost = NTSCDec(dtrLce!lce_valore) - (dMoltipsegno * NTSCDec(dtrLce!lce_pmvaldis))

      ' ora scrive, finalmente
      If lIIpes <> 0 Then
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, instid , "
      Else
        strSQL = "INSERT INTO " & strNomtabPes & " ( codditt, "
      End If
      strSQL = strSQL & " pes_commeca,  pes_esecid,  pes_taskid,  pes_qtaesec, " & _
         " pes_workesec,  pes_salcon,  pes_pctworkcomp,  pes_diffwork,  pes_remworkdop, " & _
         " pes_diffqta,  pes_remqtadop,  pes_costesec, pes_data,  pes_diffcost,  pes_remcostdop,  pes_note," & _
         " pes_opnome,  pes_segno,  pes_tipork,  pes_ortipork,  pes_oranno,  pes_orserie, " & _
         " pes_ornum,  pes_orriga,  pes_mmtipork,  pes_mmanno, pes_mmserie, pes_mmnum,  pes_mmriga, " & _
         " pes_paprogr,  pes_olprogr ) VALUES "
      If lIIpes <> 0 Then
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , " & lIIpes & " , "
      Else
        strSQL = strSQL & " ( " & CStrSQL(strDitta) & " , "
      End If
      strSQL = strSQL & "  " & NTSCStr(dtrLce("lce_commeca")) & ", " & lEsecid & ", " & _
                NTSCStr(dtrLce("lce_pmtaskid")) & ", " & CDblSQL(dQtaesec) & "," & _
                CDblSQL(dWorkesec) & ",'" & NTSCStr(dtrLce("lce_pmsalcon")) & "', 0, " & _
                CDblSQL(dDiffwork) & "," & CDblSQL(dRemworkdop) & "," & CDblSQL(dDiffqta) & "," & _
                CDblSQL(dRemqtadop) & "," & CDblSQL(dCostesec) & "," & CDataSQL(strDatdoc) & "," & _
                CDblSQL(dDiffcost) & "," & CDblSQL(dRemcostdop) & _
                ", LEFT(" & CStrSQL(NTSCStr(dtrLce("lce_note"))) & ",255), " & _
                CStrSQL(oApp.User.Nome) & "," & (IIf(nOperaz = 0, 1, -1)).ToString & _
                ", 'L', null,0,null,0,0, '" & strTipork & "'," & nAnno & ",'" & strSerie & "', " & _
                 " " & lNumdoc & "," & lRiga & ", " & _
                 " 0," & lRigaa & ")"

      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

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
#End Region

#Region "Net 2012 - determinazione lotto numerico da lotto alfanum"
  Public Overridable Function LottoxCreaID(ByVal strDitta As String, ByRef lId As Integer, _
                                          Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    lId = 0
    Try
      strSQL = "SELECT max(alo_lotto) as ID FROM analotti WHERE codditt = " & CStrSQL(strDitta) & _
               " AND alo_lotto < 999999000" 'per evitare di creare un numero più grande di 999999999 se è stato creato il lotto fittizio 999999999
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then lId = NTSCInt(dttTmp.Rows(0)!ID) + 1
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function LottoxGetID(ByVal strDitta As String, ByVal strCodart As String, _
                                          ByVal strLottox As String, ByRef lId As Integer, _
                                          Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    lId = 0
    Try
      strSQL = "SELECT alo_lotto FROM analotti " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND alo_codart = " & CStrSQL(strCodart) & _
              " AND alo_lottox = " & CStrSQL(strLottox)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then lId = NTSCInt(dttTmp.Rows(0)!alo_lotto)
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function LottoxCheckLottoUnivoco(ByVal strDitta As String, ByVal strCodart As String, _
                                                    ByVal strLottox As String, ByRef strCodartOut As String) As Boolean
    'restituisce false ed il cod. lotto in cui il lottox è stato utilizzato
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    LottoxCheckLottoUnivoco = True
    Try
      strCodartOut = ""
      strSQL = "SELECT alo_codart FROM analotti " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND alo_codart <> " & CStrSQL(strCodart) & _
               " AND alo_lottox = " & CStrSQL(strLottox)
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strCodartOut = NTSCStr(dttTmp.Rows(0)!alo_codart)
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
#End Region


  Public Overridable Function ScadenAggDtprevincpag(ByVal strDitta As String, ByVal bScadenec As Boolean, _
                                                    ByVal strDataScadDa As String, ByVal strDataScadA As String, _
                                                    ByVal strDataDocDa As String, ByVal strDataDocA As String, _
                                                    ByVal lAnagenDa As Integer, ByVal lAnagenA As Integer, _
                                                    ByVal lContoDa As Integer, ByVal lContoA As Integer, _
                                                    ByVal strTipoScad As String) As Boolean
    'strTipoScad  = integrativa si/no/entrambe

    'TESORERIA
    'aggiorna la data di incasso prevista a partire dalla data scadenza + giorni di ritardo contenuti in clitpsg o tabrtst o tabstpg
    'può lavorare sia su scaden che su scadenec
    Dim strSQL As String = ""
    Dim strScaden As String = "scaden"
    Try
      'QUERY DI CONTROLLO
      'SELECT sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, 
      'sc_datsca, sc_codstpg, sc_dtprevip, sc_fldatprbl, sc_darave,
      'isnull(clistpg.cts_ggritmed, 0) AS cts_ggritmed, 
      'isnull(tabrtst.tb_ggritmed, 0) AS tb_ggritmed, isnull(tabrtst.tb_ggritmedpag, 0) AS tb_ggritmedpag, 
      'isnull(tabstpg.tb_ggmedinc, 0) AS tb_ggmedinc,isnull(tabstpg.tb_ggmedpag, 0) AS tb_ggmedpag

      'FROM scaden INNER JOIN anagra ON scaden.codditt = anagra.codditt AND scaden.sc_conto = anagra.an_conto
      'LEFT JOIN clistpg ON clistpg.codditt = scaden.codditt AND clistpg.cts_conto = scaden.sc_conto AND clistpg.cts_codstpg = scaden.sc_codstpg
      'LEFT JOIN tabrtst ON tabrtst.codditt = scaden.codditt AND tabrtst.tb_codstpg = scaden.sc_codstpg AND tabrtst.tb_trating = anagra.an_trating
      'LEFT JOIN tabstpg ON scaden.sc_codstpg = tabstpg.tb_codstpg

      'WHERE scaden.codditt = 'PROVA'
      'AND scaden.sc_fldatprbl = 'N'
      'AND scaden.sc_flsaldato = 'N'
      'AND sc_conto = 4010001

      If bScadenec Then strScaden = "scadenec"

      strSQL = "UPDATE " & strScaden & " SET sc_dtprevip = isnull(" & _
                "	CASE WHEN isnull(clistpg.cts_ggritmed, 0) <> 0 THEN " & _
                "		DATEADD(d, clistpg.cts_ggritmed, sc_datsca) " & _
                "	ELSE " & _
                "		CASE WHEN sc_darave = 'D' AND isnull(tabrtst.tb_ggritmed, 0) <> 0 THEN  " & _
                "			DATEADD(d, tabrtst.tb_ggritmed, sc_datsca) " & _
                "		ELSE " & _
                "			CASE WHEN sc_darave = 'A' AND isnull(tabrtst.tb_ggritmedpag, 0) <> 0 THEN " & _
                "				DATEADD(d, tabrtst.tb_ggritmedpag, sc_datsca) " & _
                "			ELSE " & _
                "				CASE WHEN sc_darave = 'D' AND isnull(tabstpg.tb_ggmedinc, 0) <> 0 THEN " & _
                "					DATEADD(d, tabstpg.tb_ggmedinc, sc_datsca) " & _
                "				ELSE " & _
                "					CASE WHEN sc_darave = 'A' AND isnull(tabstpg.tb_ggmedpag, 0) <> 0 THEN " & _
                "						DATEADD(d, tabstpg.tb_ggmedpag, sc_datsca) " & _
                "					ELSE " & _
                "						sc_datsca " & _
                "					END " & _
                "				END " & _
                "			END " & _
                "		END	" & _
                "	END, sc_datsca) " & _
                "FROM " & strScaden & " INNER JOIN anagra ON " & strScaden & ".codditt = anagra.codditt AND " & strScaden & ".sc_conto = anagra.an_conto " & _
                "LEFT JOIN clistpg ON clistpg.codditt = " & strScaden & ".codditt AND clistpg.cts_conto = " & strScaden & ".sc_conto AND clistpg.cts_codstpg = " & strScaden & ".sc_codstpg " & _
                "LEFT JOIN tabrtst ON tabrtst.codditt = " & strScaden & ".codditt AND tabrtst.tb_codstpg = " & strScaden & ".sc_codstpg AND tabrtst.tb_trating = anagra.an_trating " & _
                "LEFT JOIN tabstpg ON " & strScaden & ".sc_codstpg = tabstpg.tb_codstpg " & _
                "WHERE " & strScaden & ".codditt = " & CStrSQL(strDitta) & _
                " AND sc_fldatprbl = 'N' " & _
                " AND sc_flsaldato = 'N' " & _
                " AND sc_conto BETWEEN " & lContoDa & " AND " & lContoA & _
                " AND anagra.an_codanag BETWEEN " & lAnagenDa & " AND " & lAnagenA & _
                " AND sc_datdoc BETWEEN " & CDataSQL(strDataDocDa) & " AND " & CDataSQL(strDataDocA) & _
                " AND sc_datsca BETWEEN " & CDataSQL(strDataScadDa) & " AND " & CDataSQL(strDataScadA)
      If strTipoScad <> "E" Then strSQL += " AND sc_integr = " & CStrSQL(strTipoScad)

      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataAgendaSTD(ByVal strDitta As String, ByVal strDataStart As String, _
                                                ByVal strDataEnd As String, ByVal strBusUser As String, _
                                                ByVal bDaAgenda As Boolean, _
                                                ByRef bDaeseguire As Boolean, ByRef bEseguito As Boolean, _
                                                ByRef bAnnullato As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQL_CS As String = ""
    Dim strSQL_CRM As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Visualizza tutte le attività previste di tutti gli operatori presenti in ORGANIG
      '--- che posseggono un codice operaio con il flag "Addetto al Customer Service" attivo
      '--------------------------------------------------------------------------------------------------------------
      If Not strBusUser.startswith("'"c) Then strBusUser = CStrSQL(strBusUser)
      '--------------------------------------------------------------------------------------------------------------
      strSQL_CS = "SELECT attprevc.codditt, ap_codattp AS ca_codcrac, le_codlead, op_codchia," & _
        "   le_descr1, le_indir, le_cap, le_citta, le_prov, le_stato, le_telef, le_faxtlx, le_cell, le_email," & _
        "   ap_oggetto AS ca_oggetto, '' AS ca_opnomeinc, ap_codlead AS ca_codlead," & _
        "   ap_luogo AS ca_luogo, ap_oraprev AS ca_oraprev, ap_dataprev AS ca_dataprev, ISNULL(apd_quant,0) AS ca_temppr, " & _
        "   ap_dataprev AS cap_dataes, ap_oraprev AS cap_oraes, ISNULL(apd_quant,0) AS cap_tempes," & _
        "   tabtaco.tb_color," & _
        "   CASE WHEN ap_flevas = 'N' THEN 'D' ELSE 'E' END AS cap_status," & _
        "   organig.og_coperat as xx_operat, organig1.og_coperat as xx_operat1, organig2.og_coperat as xx_operat2, " & _
        "   organig3.og_coperat as xx_operat3, organig4.og_coperat as xx_operat4, organig5.og_coperat as xx_operat5, " & _
        "   organig6.og_coperat as xx_operat6, organig7.og_coperat as xx_operat7, organig8.og_coperat as xx_operat8, " & _
        "   ap_luogo, op_codart, ar_descr AS xx_codart, op_oggetto " & _
        " FROM leads INNER JOIN attprevc ON leads.codditt = attprevc.codditt AND leads.le_codlead = attprevc.ap_codlead" & _
        "   INNER JOIN nnchiam ON attprevc.codditt = nnchiam.codditt AND attprevc.ap_codchia = nnchiam.op_codchia" & _
        "   LEFT JOIN organig ON organig.codditt = attprevc.codditt AND organig.og_progr = attprevc.ap_opinc" & _
        "   LEFT JOIN organig as organig1 ON organig1.codditt = attprevc.codditt AND organig1.og_progr = attprevc.ap_opinc1" & _
        "   LEFT JOIN organig as organig2 ON organig2.codditt = attprevc.codditt AND organig2.og_progr = attprevc.ap_opinc2" & _
        "   LEFT JOIN organig as organig3 ON organig3.codditt = attprevc.codditt AND organig3.og_progr = attprevc.ap_opinc3" & _
        "   LEFT JOIN organig as organig4 ON organig4.codditt = attprevc.codditt AND organig4.og_progr = attprevc.ap_opinc4" & _
        "   LEFT JOIN organig as organig5 ON organig5.codditt = attprevc.codditt AND organig5.og_progr = attprevc.ap_opinc5" & _
        "   LEFT JOIN organig as organig6 ON organig6.codditt = attprevc.codditt AND organig6.og_progr = attprevc.ap_opinc6" & _
        "   LEFT JOIN organig as organig7 ON organig7.codditt = attprevc.codditt AND organig7.og_progr = attprevc.ap_opinc7" & _
        "   LEFT JOIN organig as organig8 ON organig8.codditt = attprevc.codditt AND organig8.og_progr = attprevc.ap_opinc8" & _
        "   INNER JOIN tabcope ON tabcope.codditt = organig.codditt AND tabcope.tb_codcope = organig.og_codcope" & _
        "   INNER JOIN tabtaco ON attprevc.codditt = tabtaco.codditt AND attprevc.ap_codtaco = tabtaco.tb_codtaco" & _
        "   LEFT JOIN tabtchi ON tabtchi.codditt = nnchiam.codditt AND op_codtchi = tb_codtchi " & _
        "   LEFT JOIN attprevcd ON attprevcd.apd_codattp = attprevc.ap_codattp AND attprevc.codditt = attprevcd.codditt " & _
        "   LEFT JOIN artico ON artico.codditt = leads.codditt AND op_codart = ar_codart " & _
        " WHERE ISNULL(apd_tiporiga, 'O') = 'O' " & _
        "   AND tb_flcs = 'S'" & _
        "   AND tb_agenda = 'S'" & _
        "   AND ap_dataprev BETWEEN " & CDataSQL(strDataStart) & " AND " & CDataSQL(strDataEnd) & _
        "   AND (organig.og_coperat IN (" & strBusUser & ")" & _
        "        OR organig1.og_coperat IN (" & strBusUser & ")" & _
        "        OR organig2.og_coperat IN (" & strBusUser & ")" & _
        "        OR organig3.og_coperat IN (" & strBusUser & ")" & _
        "        OR organig4.og_coperat IN (" & strBusUser & "))" & _
        "   AND ap_confermato = 'S'" & _
        IIf(bDaeseguire = False, " AND ap_flevas <> 'C'", "").ToString & _
        IIf(bEseguito = False, " AND ap_flevas <> 'S'", "").ToString
      If strDitta <> "" Then strSQL &= " AND attprevc.codditt = " & CStrSQL(strDitta)
      'ap_confermato = 'N' sono attività pianificate ma ancora non diventate esecutive!!! news net 2015

      strSQL_CS = "SELECT tmp.*, ap_note AS ca_note FROM (" & strSQL_CS & ") AS tmp " & _
                  " LEFT JOIN attprevc ON ap_codattp = tmp.ca_codcrac AND tmp.codditt = attprevc.codditt "


      strSQL_CRM = "SELECT cract.codditt, ca_codcrac, le_codlead, 0 AS op_codchia," & _
                   "       le_descr1, le_indir, le_cap, le_citta, le_prov, le_stato, le_telef, le_faxtlx, le_cell, le_email," & _
                   "       ca_oggetto, ca_opnomeinc, ca_codlead," & _
                   "       ca_luogo, ca_oraprev, ca_dataprev, ca_temppr, " & _
                   "       cap_dataes, cap_oraes, cap_tempes," & _
                   "       tb_color," & _
                   "       cap_status," & _
                   "       cap_opcrmincpr as xx_operat, '' as xx_operat1, '' as xx_operat2, '' as xx_operat3, '' as xx_operat4, " & _
                   "       '' as xx_operat5, '' as xx_operat6, '' as xx_operat7, '' as xx_operat8, " & _
                   "       '' AS ap_luogo, '' AS op_codart, '' AS xx_codart, '' AS op_oggetto " & _
                   " FROM cract LEFT JOIN leads on cract.codditt = leads.codditt AND cract.ca_codlead = leads.le_codlead" & _
                   " INNER JOIN cractopp ON cract.codditt = cractopp.codditt AND cract.ca_codcrac = cractopp.cap_codcrac"
      If bDaAgenda = False Then
        strSQL_CRM += " INNER JOIN acccrm ON cractopp.codditt = acccrm.codditt AND cractopp.cap_opcrmincpr = acccrm.opcr_alopnome"
      Else
        If strBusUser <> CStrSQL(oApp.User.Nome) Then
          strSQL_CRM += " INNER JOIN acccrm ON cractopp.codditt = acccrm.codditt AND cractopp.cap_opcrmincpr = acccrm.opcr_alopnome"
        End If
      End If
      strSQL_CRM += " INNER JOIN tabtaco ON cract.codditt = tabtaco.codditt AND cract.ca_codtaco = tabtaco.tb_codtaco" & _
        " WHERE ((cap_status <> 'E' AND ca_dataprev BETWEEN " & CDataSQL(strDataStart) & " AND " & CDataSQL(strDataEnd) & ")" & _
        "     OR (cap_status = 'E' AND cap_dataes BETWEEN " & CDataSQL(strDataStart) & " AND " & CDataSQL(strDataEnd) & ")) " & _
        "   AND tb_agenda = 'S'"
      If bDaAgenda = False Then
        strSQL_CRM += " AND cap_opcrmincpr IN (" & strBusUser & ")" & _
          " AND opcr_opnome = " & CStrSQL(oApp.User.Nome) & _
          " AND opcr_crmvis = 'S'"
      Else
        If strBusUser <> CStrSQL(oApp.User.Nome) Then
          strSQL_CRM += " AND opcr_opnome = " & CStrSQL(oApp.User.Nome) & _
            " AND opcr_crmvis = 'S' " & _
                        " AND cap_opcrmincpr IN (" & strBusUser & ")"
        Else
          strSQL_CRM += " AND cap_opcrmincpr IN (" & strBusUser & ")"
        End If
      End If
      strSQL_CRM += " AND cap_status IN ('.'" & _
        IIf(bDaeseguire = True, ", 'D'", "").ToString & _
        IIf(bEseguito = True, ", 'E'", "").ToString & _
        IIf(bAnnullato = True, ", 'A'", "").ToString & _
        ")" & _
        IIf(strDitta <> "", " AND cract.codditt = " & CStrSQL(strDitta), "").ToString

      strSQL_CRM = "SELECT tmp.*, ca_note FROM (" & strSQL_CRM & ") AS tmp " & _
                   " LEFT JOIN cract ON tmp.ca_codcrac = cract.ca_codcrac AND tmp.codditt = cract.codditt "

      strSQL = strSQL_CS & " UNION ALL " & strSQL_CRM

      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      'For i As Integer = 0 To (dttOut.Rows.Count - 1)
      '  strSQL = "SELECT"
      '  Select Case NTSCInt(dttOut.Rows(i)!op_codchia)
      '    Case 0 : strSQL += " ca_note AS note"
      '    Case Else : strSQL += " ap_note AS note"
      '  End Select
      '  strSQL += " FROM"
      '  Select Case NTSCInt(dttOut.Rows(i)!op_codchia)
      '    Case 0 : strSQL += " cract"
      '    Case Else : strSQL += " attprevc"
      '  End Select
      '  strSQL += " WHERE codditt = " & CStrSQL(dttOut.Rows(i)!codditt)
      '  Select Case NTSCInt(dttOut.Rows(i)!op_codchia)
      '    Case 0
      '      strSQL += " AND ca_codcrac = " & NTSCInt(dttOut.Rows(i)!ca_codcrac) & _
      '        " AND ca_note IS NOT NULL"
      '    Case Else
      '      strSQL += " AND ap_codattp = " & NTSCInt(dttOut.Rows(i)!ca_codcrac) & _
      '        " AND ap_note IS NOT NULL"
      '  End Select
      '  dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '  If dttTmp.Rows.Count <> 0 Then dttOut.Rows(i)!ca_note = NTSCStr(dttTmp.Rows(0)!note)
      '  dttTmp.Clear()
      '  dttTmp.Dispose()
      'Next
      '--------------------------------------------------------------------------------------------------------------
      'customer service: se sono presenti degli operai in supporto, aggiungo dei record nel temporaneo per poterli visualizzare come operai normali
      'inoltre devo anche dividere il tempo totale indicato sull'intervento per il numero dei tecnici che parteciperanno.
      'non devo dividere il tempo per le attrezzature in supporto (xx_operat5 - xx_operat8) perchè l'attrezzatura deve venir impiegata da tutti e non occupa tempo
      Dim nOp As Integer = 1

      For Each dtrT As DataRow In dttOut.Select("xx_operat1 <> '' OR xx_operat2 <> '' OR xx_operat3 <> '' OR xx_operat4 <> '' " & _
                                                 "OR xx_operat5 <> '' OR xx_operat6 <> '' OR xx_operat7 <> '' OR xx_operat8 <> ''")
        nOp = 1
        If NTSCStr(dtrT!xx_operat1) <> "" Then nOp += 1 : If NTSCStr(dtrT!xx_operat2) <> "" Then nOp += 1
        If NTSCStr(dtrT!xx_operat3) <> "" Then nOp += 1 : If NTSCStr(dtrT!xx_operat4) <> "" Then nOp += 1

        dtrT!ca_temppr = ArrDbl(NTSCDec(dtrT!ca_temppr) / nOp, TrovaNdec(0))
        dtrT!cap_tempes = ArrDbl(NTSCDec(dtrT!cap_tempes) / nOp, TrovaNdec(0))

        'riga per primo op supporto
        If NTSCStr(dtrT!xx_operat1).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat1
        End If

        'riga per secondo op supporto
        If NTSCStr(dtrT!xx_operat2).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat2
        End If

        'riga per terzo op supporto
        If NTSCStr(dtrT!xx_operat3).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat3
        End If

        'riga per terzo op supporto
        If NTSCStr(dtrT!xx_operat4).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat4
        End If

        If NTSCStr(dtrT!xx_operat5).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat5
        End If

        If NTSCStr(dtrT!xx_operat6).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat6
        End If

        If NTSCStr(dtrT!xx_operat7).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat7
        End If

        If NTSCStr(dtrT!xx_operat8).Trim <> "" Then
          dttOut.Rows.Add(dttOut.NewRow)
          For Each oCol As DataColumn In dttOut.Columns
            dttOut.Rows(dttOut.Rows.Count - 1)(oCol.ColumnName) = dtrT(oCol.ColumnName)
          Next
          dttOut.Rows(dttOut.Rows.Count - 1)!xx_operat = dtrT!xx_operat8
        End If
      Next

      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RicalcolaCommessa(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                                ByVal lInstidTTprevent As Integer, _
                                                ByVal dCostototalecommessa As Decimal, _
                                                ByVal dPrezzototalecommessa As Decimal, _
                                                ByVal dPrezzoconcordatocommessa As Decimal, _
                                                ByVal dCostoeffettivocommessa As Decimal, _
                                                ByVal dCostorimanentecommessa As Decimal, _
                                                ByVal dCostoprogrammatocommessa As Decimal, _
                                                ByVal dCostodifferenzecommessa As Decimal, _
                                                ByVal dCostototaleeffettivocommessa As Decimal, _
                                                ByVal dCostototalerimanentecommessa As Decimal, _
                                                ByVal dCostototaleprogrammatocommessa As Decimal, _
                                                ByVal dCostototaledifferenzecommessa As Decimal, _
                                                ByVal dCostoindirettototalecommessa As Decimal, _
                                                ByVal dCostoindirettoeffettivocommessa As Decimal, _
                                                ByVal dCostoindirettoprogrammatocommessa As Decimal, _
                                                ByVal dRicavodefinitivoprevistocommessa As Decimal, _
                                                ByVal dRicavodefinitivoeffettivocommessa As Decimal, _
                                                ByVal dRicavodefinitivorimanentecommessa As Decimal, _
                                                ByVal dRicavodefinitivodifferenzecommessa As Decimal, _
                                                ByVal dRicavoaccontoprevistocommessa As Decimal, _
                                                ByVal dRicavoaccontoeffettivocommessa As Decimal, _
                                                ByVal dRicavoaccontorimanentecommessa As Decimal, _
                                                ByVal dRicavoaccontodifferenzecommessa As Decimal, _
                                                ByVal strDitta As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RicalcolaCommessa(lInstidTTTasks0, lComm, lInstidTTprevent, dCostototalecommessa, _
        dPrezzototalecommessa, dPrezzoconcordatocommessa, dCostoeffettivocommessa, dCostorimanentecommessa, _
        dCostoprogrammatocommessa, dCostodifferenzecommessa, dCostototaleeffettivocommessa, _
        dCostototalerimanentecommessa, dCostototaleprogrammatocommessa, dCostototaledifferenzecommessa, _
        dCostoindirettototalecommessa, dCostoindirettoeffettivocommessa, dCostoindirettoprogrammatocommessa, _
        dRicavodefinitivoprevistocommessa, dRicavodefinitivoeffettivocommessa, _
        dRicavodefinitivorimanentecommessa, dRicavodefinitivodifferenzecommessa, _
        dRicavoaccontoprevistocommessa, dRicavoaccontoeffettivocommessa, dRicavoaccontorimanentecommessa, _
        dRicavoaccontodifferenzecommessa, strDitta, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RicalcolaCommessa(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                                ByVal lInstidTTprevent As Integer, _
                                                ByVal dCostototalecommessa As Decimal, _
                                                ByVal dPrezzototalecommessa As Decimal, _
                                                ByVal dPrezzoconcordatocommessa As Decimal, _
                                                ByVal dCostoeffettivocommessa As Decimal, _
                                                ByVal dCostorimanentecommessa As Decimal, _
                                                ByVal dCostoprogrammatocommessa As Decimal, _
                                                ByVal dCostodifferenzecommessa As Decimal, _
                                                ByVal dCostototaleeffettivocommessa As Decimal, _
                                                ByVal dCostototalerimanentecommessa As Decimal, _
                                                ByVal dCostototaleprogrammatocommessa As Decimal, _
                                                ByVal dCostototaledifferenzecommessa As Decimal, _
                                                ByVal dCostoindirettototalecommessa As Decimal, _
                                                ByVal dCostoindirettoeffettivocommessa As Decimal, _
                                                ByVal dCostoindirettoprogrammatocommessa As Decimal, _
                                                ByVal dRicavodefinitivoprevistocommessa As Decimal, _
                                                ByVal dRicavodefinitivoeffettivocommessa As Decimal, _
                                                ByVal dRicavodefinitivorimanentecommessa As Decimal, _
                                                ByVal dRicavodefinitivodifferenzecommessa As Decimal, _
                                                ByVal dRicavoaccontoprevistocommessa As Decimal, _
                                                ByVal dRicavoaccontoeffettivocommessa As Decimal, _
                                                ByVal dRicavoaccontorimanentecommessa As Decimal, _
                                                ByVal dRicavoaccontodifferenzecommessa As Decimal, _
                                                ByVal strDitta As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strTTTASKS As String = "tasks"
    Dim strTTPREVENT As String = "PREVENT"
    Dim dsTmp As DataSet = Nothing
    Dim dCostototale As Decimal = 0
    Dim dPrezzototale As Decimal = 0
    Dim dCostoeffettivo As Decimal = 0
    Dim dCostorimanente As Decimal = 0
    Dim dCostoprogrammato As Decimal = 0
    Dim dDifferenzecosto As Decimal = 0
    Dim dTotalecostoeffettivo As Decimal = 0
    Dim dTotalecostorimanente As Decimal = 0
    Dim dTotalecostoprogrammato As Decimal = 0
    Dim dTotaledifferenzecostoprogrammato As Decimal = 0
    Dim dCostoindirettototale As Decimal = 0
    Dim dCostoindirettoeffettivo As Decimal = 0
    Dim dCostoindirettoprogrammato As Decimal = 0
    Dim dRicavodefinitivoprevisto As Decimal = 0
    Dim dRicavodefinitivoeffettivo As Decimal = 0
    Dim dRicavodefinitivorimanente As Decimal = 0
    Dim dRicavodefinitivodifferenze As Decimal = 0
    Dim dRicavoaccontoprevisto As Decimal = 0
    Dim dRicavoaccontoeffettivo As Decimal = 0
    Dim dRicavoaccontorimanente As Decimal = 0
    Dim dRicavoaccontodifferenze As Decimal = 0
    Dim dRicar As Decimal = 0
    Dim i As Integer = 0
    Try
      '   CLN__STD.CheckInvokeCustomFunction(
      '--------------------------------------------------------------------------------------------------------------
      If lInstidTTTasks0 <> 0 Then strTTTASKS = "tttasks"
      If lInstidTTprevent <> 0 Then strTTPREVENT = "TTPREVENT"
      '--------------------------------------------------------------------------------------------------------------
      AggiornaPadri(lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      dCostototalecommessa = 0 : dPrezzototalecommessa = 0 : dPrezzoconcordatocommessa = 0
      dCostoeffettivocommessa = 0 : dCostorimanentecommessa = 0 : dCostoprogrammatocommessa = 0
      dCostodifferenzecommessa = 0 : dCostototaleeffettivocommessa = 0 : dCostototalerimanentecommessa = 0
      dCostototaleprogrammatocommessa = 0 : dCostototaledifferenzecommessa = 0
      dCostoindirettototalecommessa = 0 : dCostoindirettoeffettivocommessa = 0
      dCostoindirettoprogrammatocommessa = 0 : dRicavodefinitivoprevistocommessa = 0
      dRicavodefinitivoeffettivocommessa = 0 : dRicavodefinitivorimanentecommessa = 0
      dRicavodefinitivodifferenzecommessa = 0 : dRicavoaccontoprevistocommessa = 0
      dRicavoaccontoeffettivocommessa = 0 : dRicavoaccontorimanentecommessa = 0
      dRicavoaccontodifferenzecommessa = 0
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT pre_prevnum, pre_prevrev, pre_varid, pre_przconc, pre_intest, pre_stato" & _
        " FROM " & strTTPREVENT & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pre_commeca = " & lComm & _
        IIf(lInstidTTprevent <> 0, " AND instid = " & lInstidTTprevent, "").ToString & _
        " ORDER BY pre_prevnum, pre_prevrev, pre_varid"
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP", Nothing, dbConn)
      For i = 0 To (dsTmp.Tables("TEMP").Rows.Count - 1)
        With dsTmp.Tables("TEMP").Rows(i)
          dCostototale = 0 : dPrezzototale = 0 : dCostoeffettivo = 0 : dCostorimanente = 0 : dCostoprogrammato = 0
          dDifferenzecosto = 0 : dTotalecostoeffettivo = 0 : dTotalecostorimanente = 0 : dTotalecostoprogrammato = 0
          dTotaledifferenzecostoprogrammato = 0 : dCostoindirettototale = 0
          dCostoindirettoeffettivo = 0 : dCostoindirettoprogrammato = 0
          dRicavodefinitivoprevisto = 0 : dRicavodefinitivoeffettivo = 0
          dRicavodefinitivorimanente = 0 : dRicavodefinitivodifferenze = 0
          dRicavoaccontoprevisto = 0 : dRicavoaccontoeffettivo = 0
          dRicavoaccontorimanente = 0 : dRicavoaccontodifferenze = 0
          If (NTSCStr(!pre_intest) = "I") And (NTSCStr(!pre_stato) = "3" Or NTSCStr(!pre_stato) = "6") Then
            EseguiRicalcolo(NTSCInt(!pre_prevnum), NTSCInt(!pre_prevrev), NTSCInt(!pre_varid), lInstidTTTasks0, _
              lComm, lInstidTTprevent, NTSCDec(!pre_przconc), dCostototale, dPrezzototale, dCostoeffettivo, _
              dCostorimanente, dCostoprogrammato, dDifferenzecosto, dTotalecostoeffettivo, dTotalecostorimanente, _
              dTotalecostoprogrammato, dTotaledifferenzecostoprogrammato, dCostoindirettototale, _
              dCostoindirettoeffettivo, dCostoindirettoprogrammato, dRicavodefinitivoprevisto, _
              dRicavodefinitivoeffettivo, dRicavodefinitivorimanente, dRicavodefinitivodifferenze, _
              dRicavoaccontoprevisto, dRicavoaccontoeffettivo, dRicavoaccontorimanente, dRicavoaccontodifferenze, _
              strDitta, dbConn)
          End If
          dCostototalecommessa = dCostototalecommessa + dCostototale
          dPrezzototalecommessa = dPrezzototalecommessa + dPrezzototale
          dPrezzoconcordatocommessa = dPrezzoconcordatocommessa + NTSCDec(!pre_przconc)
          dCostoeffettivocommessa = dCostoeffettivocommessa + dCostoeffettivo
          dCostorimanentecommessa = dCostorimanentecommessa + dCostorimanente
          dCostoprogrammatocommessa = dCostoprogrammatocommessa + dCostoprogrammato
          dCostodifferenzecommessa = dCostodifferenzecommessa + dDifferenzecosto
          dCostototaleeffettivocommessa = dCostototaleeffettivocommessa + dTotalecostoeffettivo
          dCostototalerimanentecommessa = dCostototalerimanentecommessa + dTotalecostorimanente
          dCostototaleprogrammatocommessa = dCostototaleprogrammatocommessa + dTotalecostoprogrammato
          dCostototaledifferenzecommessa = dCostototaledifferenzecommessa + dTotaledifferenzecostoprogrammato
          dCostoindirettototalecommessa = dCostoindirettototalecommessa + dCostoindirettototale
          dCostoindirettoeffettivocommessa = dCostoindirettoeffettivocommessa + dCostoindirettoeffettivo
          dCostoindirettoprogrammatocommessa = dCostoindirettoprogrammatocommessa + dCostoindirettoprogrammato
          dRicavodefinitivoprevistocommessa = dRicavodefinitivoprevistocommessa + dRicavodefinitivoprevisto
          dRicavodefinitivoeffettivocommessa = dRicavodefinitivoeffettivocommessa + dRicavodefinitivoeffettivo
          dRicavodefinitivorimanentecommessa = dRicavodefinitivorimanentecommessa + dRicavodefinitivorimanente
          dRicavodefinitivodifferenzecommessa = dRicavodefinitivodifferenzecommessa + dRicavodefinitivodifferenze
          dRicavoaccontoprevistocommessa = dRicavoaccontoprevistocommessa + dRicavoaccontoprevisto
          dRicavoaccontoeffettivocommessa = dRicavoaccontoeffettivocommessa + dRicavoaccontoeffettivo
          dRicavoaccontorimanentecommessa = dRicavoaccontorimanentecommessa + dRicavoaccontorimanente
          dRicavoaccontodifferenzecommessa = dRicavoaccontodifferenzecommessa + dRicavoaccontodifferenze
        End With
      Next
      'UPDATE su TTPREVENT
      If dCostototalecommessa = 0 Then
        dRicar = 0
      Else
        dRicar = NTSCDec(((NTSCDec(dPrezzoconcordatocommessa) - NTSCDec(dCostototalecommessa)) / NTSCDec(dCostototalecommessa)) * 100)
      End If
      strSQL = "UPDATE " & strTTPREVENT & _
        " SET pre_costotot = " & CDblSQL(dCostototalecommessa) & "," & _
        " pre_prztot = " & CDblSQL(dPrezzototalecommessa) & "," & _
        " pre_ricar = " & CDblSQL(dRicar) & "," & _
        " pre_actcost = " & CDblSQL(dCostoeffettivocommessa) & "," & _
        " pre_remcost = " & CDblSQL(dCostorimanentecommessa) & "," & _
        " pre_cost = " & CDblSQL(dCostoprogrammatocommessa) & "," & _
        " pre_diffcost = " & CDblSQL(dCostodifferenzecommessa) & "," & _
        " pre_totactcost = " & CDblSQL(dCostototaleeffettivocommessa) & "," & _
        " pre_totremcost = " & CDblSQL(dCostototalerimanentecommessa) & "," & _
        " pre_totcost = " & CDblSQL(dCostototaleprogrammatocommessa) & "," & _
        " pre_totdiffcost = " & CDblSQL(dCostototaledifferenzecommessa) & "," & _
        " pre_basecostind = " & CDblSQL(dCostoindirettototalecommessa) & "," & _
        " pre_actcostind = " & CDblSQL(dCostoindirettoeffettivocommessa) & "," & _
        " pre_costind = " & CDblSQL(dCostoindirettoprogrammatocommessa) & "," & _
        " pre_basefattdef = " & CDblSQL(dRicavodefinitivoprevistocommessa) & "," & _
        " pre_actfattdef = " & CDblSQL(dRicavodefinitivoeffettivocommessa) & "," & _
        " pre_remfattdef = " & CDblSQL(dRicavodefinitivorimanentecommessa) & "," & _
        " pre_difffattdef = " & CDblSQL(dRicavodefinitivodifferenzecommessa) & "," & _
        " pre_basefattacc = " & CDblSQL(dRicavoaccontoprevistocommessa) & "," & _
        " pre_actfattacc = " & CDblSQL(dRicavoaccontoeffettivocommessa) & "," & _
        " pre_remfattacc = " & CDblSQL(dRicavoaccontorimanentecommessa) & "," & _
        " pre_difffattacc = " & CDblSQL(dRicavoaccontodifferenzecommessa) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pre_commeca = " & lComm & _
        " AND pre_prevnum = 999999999" & _
        " AND pre_prevrev = 999" & _
        " AND pre_varid = 999" & _
        IIf(lInstidTTprevent <> 0, " AND instid = " & lInstidTTprevent, "").ToString
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function EseguiRicalcolo(ByVal lPrev As Integer, ByVal nRev As Integer, ByVal nVar As Integer, ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, ByVal lInstidTTprevent As Integer, ByVal dPrezzoconcordato As Decimal, _
                                                ByVal dCostototale As Decimal, ByVal dPrezzototale As Decimal, ByVal dCostoeffettivo As Decimal, ByVal dCostorimanente As Decimal, ByVal dCostoprogrammato As Decimal, _
                                                ByVal dDifferenzecosto As Decimal, ByVal dTotalecostoeffettivo As Decimal, ByVal dTotalecostorimanente As Decimal, ByVal dTotalecostoprogrammato As Decimal, _
                                                ByVal dTotaledifferenzecostoprogrammato As Decimal, ByVal dCostoindirettototale As Decimal, ByVal dCostoindirettoeffettivo As Decimal, ByVal dCostoindirettoprogrammato As Decimal, _
                                                ByVal dRicavodefinitivoprevisto As Decimal, ByVal dRicavodefinitivoeffettivo As Decimal, ByVal dRicavodefinitivorimanente As Decimal, ByVal dRicavodefinitivodifferenze As Decimal, _
                                                ByVal dRicavoaccontoprevisto As Decimal, ByVal dRicavoaccontoeffettivo As Decimal, ByVal dRicavoaccontorimanente As Decimal, ByVal dRicavoaccontodifferenze As Decimal, _
                                                ByVal strDitta As String) As Boolean
    Dim strTTTASKS As String
    Dim strTTPREVENT As String

    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing

    Dim bPrevgrup As Boolean
    Dim bSoloconsuntivo As Boolean
    Dim strLivello As String = ""
    Dim lTaskid As Integer
    Dim dCostoparziale As Decimal
    Dim dRicar As Decimal

    Dim nSegno As Integer
    Dim i As Integer
    Dim j As Integer
    Try
      If lInstidTTTasks0 <> 0 Then
        strTTTASKS = "tttasks"
      Else
        strTTTASKS = "tasks"
      End If
      If lInstidTTprevent <> 0 Then
        strTTPREVENT = "TTPREVENT"
      Else
        strTTPREVENT = "PREVENT"
      End If

      strSQL = " SELECT * FROM " & strTTTASKS & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " And tsk_commeca = " & lComm & " "
      If lInstidTTTasks0 <> 0 Then
        strSQL = strSQL & " and instid = " & lInstidTTTasks0 & " "
      End If
      strSQL = strSQL & " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3 "
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS")

      If Not dsTmp.Tables("TASKS").Rows.Count = 0 Then
        bPrevgrup = False
        bSoloconsuntivo = False

        For i = 0 To dsTmp.Tables("TASKS").Rows.Count - 1
          With dsTmp.Tables("TASKS").Rows(i)
            If (NTSCInt(!tsk_prevnum) = lPrev) And (NTSCInt(!tsk_prevrev) = nRev) And (NTSCInt(!tsk_varid) = nVar) Then
              If bPrevgrup Then
                Select Case strLivello
                  Case "0"
                    If NTSCInt(!tsk_uid0) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                  Case "1"
                    If NTSCInt(!tsk_uid1) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                  Case "2"
                    If NTSCInt(!tsk_uid2) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                End Select
              End If
              If (NTSCStr(!tsk_summary) = "N") Then
                If (NTSCStr(!tsk_ricprev) <> "N" And NTSCStr(!tsk_ricprev) <> "I") Then
                  Select Case NTSCStr(!tsk_livello)
                    Case "0"
                      strSQL = " SELECT * FROM " & strTTTASKS & _
                               " WHERE codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lComm
                      If lInstidTTTasks0 <> 0 Then
                        strSQL = strSQL & " and instid = " & lInstidTTTasks0
                      End If
                      strSQL = strSQL & " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3"
                    Case "1"
                      strSQL = " SELECT * FROM " & strTTTASKS & _
                               " WHERE codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lComm & _
                               " and tsk_uid0 = " & NTSCInt(!tsk_uidpadre) & " and tsk_taskid <> " & NTSCInt(!tsk_uidpadre)
                      If lInstidTTTasks0 <> 0 Then
                        strSQL = strSQL & " and instid = " & lInstidTTTasks0
                      End If
                      strSQL = strSQL & " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3"
                    Case "2"
                      strSQL = " SELECT * FROM " & strTTTASKS & _
                               " WHERE codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lComm & _
                               " and tsk_uid1 = " & NTSCInt(!tsk_uidpadre) & " and tsk_taskid <> " & NTSCInt(!tsk_uidpadre)
                      If lInstidTTTasks0 <> 0 Then
                        strSQL = strSQL & " and instid = " & lInstidTTTasks0
                      End If
                      strSQL = strSQL & " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3"
                    Case "3"
                      strSQL = "SELECT * FROM " & strTTTASKS & _
                               " WHERE codditt = " & CStrSQL(strDitta) & " And tsk_commeca = " & lComm & _
                               " and tsk_uid2 = " & NTSCInt(!tsk_uidpadre) & " and tsk_taskid <> " & NTSCInt(!tsk_uidpadre)
                      If lInstidTTTasks0 <> 0 Then
                        strSQL = strSQL & " and instid = " & lInstidTTTasks0
                      End If
                      strSQL = strSQL & " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3"
                  End Select
                  dsTmp2 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS")
                  dCostoparziale = 0

                  j = 0
                  While (NTSCInt(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) <> NTSCInt(!tsk_taskid))
                    If (NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_summary) = "N") _
                    Or (NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_summary) = "S" _
                    And NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S") Then
                      Select Case NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_darave)
                        Case "D", "I"
                          dCostoparziale = dCostoparziale + NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basecost)
                        Case "A"
                          dCostoparziale = dCostoparziale + ((-1) * NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basecost))
                      End Select
                    End If
                    j = j + 1
                  End While

                  If (NTSCStr(!tsk_ricprev) = "T") Then
                    If dCostototale < 0 Then
                      !tsk_impbase = 0
                    Else
                      !tsk_impbase = dCostototale
                    End If
                  Else
                    If (NTSCStr(!tsk_ricprev) = "P") Then
                      If dCostoparziale < 0 Then dCostoparziale = 0
                      !tsk_impbase = dCostoparziale
                    Else
                      If (NTSCDec(!tsk_percpz) = 0) And (NTSCDec(!tsk_percpr) <> 0) Then
                        If dCostototale < 0 Then
                          !tsk_impbase = 0
                        Else
                          !tsk_impbase = dCostototale
                        End If
                        !tsk_basecost = NTSCDec((NTSCDec(!tsk_impbase) * NTSCDec(!tsk_percpr)) / 100)
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = (-1) * NTSCDec(!tsk_basecost)
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = (-1) * NTSCDec(!tsk_basecost)
                          Case "E"
                            !tsk_actcost = NTSCDec(!tsk_basecost)
                            !tsk_remcost = 0
                            !tsk_cost = NTSCDec(!tsk_actcost)
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = NTSCDec(!tsk_actcost)
                            !tsk_totremcost = 0
                            !tsk_totcost = NTSCDec(!tsk_actcost)
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                      If (NTSCDec(!tsk_percpz) <> 0) And (NTSCDec(!tsk_percpr) = 0) Then
                        If dCostoparziale < 0 Then dCostoparziale = 0
                        !tsk_impbase = dCostoparziale
                        !tsk_basecost = NTSCDec((dCostoparziale * NTSCDec(!tsk_percpz)) / 100)
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = (-1) * NTSCDec(!tsk_basecost)
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = (-1) * NTSCDec(!tsk_basecost)
                          Case "E"
                            !tsk_actcost = NTSCDec(!tsk_basecost)
                            !tsk_remcost = 0
                            !tsk_cost = NTSCDec(!tsk_actcost)
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = NTSCDec(!tsk_actcost)
                            !tsk_totremcost = 0
                            !tsk_totcost = NTSCDec(!tsk_actcost)
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                      If (NTSCDec(!tsk_percpz) = 0) And (NTSCDec(!tsk_percpr) = 0) Then
                        If dCostoparziale < 0 Then dCostoparziale = 0
                        !tsk_impbase = dCostoparziale
                        !tsk_basecost = 0
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = 0
                          Case "E"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                    End If
                  End If
                End If
                '-----
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "A"
                    If NTSCStr(!tsk_darave) = "A" Then
                      nSegno = -1
                    Else
                      nSegno = 1
                    End If
                    If Not bSoloconsuntivo Then
                      dCostototale = dCostototale + (nSegno * NTSCDec(!tsk_basecost))
                      If NTSCStr(!tsk_darave) = "I" Then dCostoindirettototale = dCostoindirettototale + (nSegno * NTSCDec(!tsk_basecost))
                      dPrezzototale = dPrezzototale + (nSegno * NTSCDec(!tsk_baseprz))
                      '---
                      dCostorimanente = dCostorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      dCostoprogrammato = dCostoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                      dTotalecostorimanente = dTotalecostorimanente + (nSegno * NTSCDec(!tsk_totremcost))
                      dTotalecostoprogrammato = dTotalecostoprogrammato + (nSegno * NTSCDec(!tsk_totcost))
                      '---
                    End If
                    dCostoeffettivo = dCostoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                    If NTSCStr(!tsk_darave) = "I" Then dCostoindirettoeffettivo = dCostoindirettoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                    If NTSCStr(!tsk_darave) = "I" Then dCostoindirettoprogrammato = dCostoindirettoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                    dDifferenzecosto = dDifferenzecosto + (nSegno * NTSCDec(!tsk_diffcost))
                    dTotalecostoeffettivo = dTotalecostoeffettivo + (nSegno * NTSCDec(!tsk_totactcost))
                    dTotaledifferenzecostoprogrammato = dTotaledifferenzecostoprogrammato + (nSegno * NTSCDec(!tsk_totdiffcost))
                  Case "R", "T", "S", "U"
                    If NTSCStr(!tsk_darave) = "T" Or NTSCStr(!tsk_darave) = "U" Then
                      nSegno = -1
                    Else
                      nSegno = 1
                    End If
                    If Not bSoloconsuntivo Then
                      If NTSCStr(!tsk_darave) = "R" Or NTSCStr(!tsk_darave) = "T" Then
                        dRicavodefinitivoprevisto = dRicavodefinitivoprevisto + (nSegno * NTSCDec(!tsk_basecost))
                        dRicavodefinitivorimanente = dRicavodefinitivorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      Else
                        dRicavoaccontoprevisto = dRicavoaccontoprevisto + (nSegno * NTSCDec(!tsk_basecost))
                        dRicavoaccontorimanente = dRicavoaccontorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      End If
                    End If
                    If NTSCStr(!tsk_darave) = "R" Or NTSCStr(!tsk_darave) = "T" Then
                      dRicavodefinitivoeffettivo = dRicavodefinitivoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                      dRicavodefinitivodifferenze = dRicavodefinitivodifferenze + (nSegno * NTSCDec(!tsk_diffcost))
                    Else
                      dRicavoaccontoeffettivo = dRicavoaccontoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                      dRicavoaccontodifferenze = dRicavoaccontodifferenze + (nSegno * NTSCDec(!tsk_diffcost))
                    End If
                End Select
                '-----
              Else
                'Se PREVGRUP = "N" oppure "F" niente
                If (NTSCStr(!tsk_prevgrup) = "S") Then
                  dCostototale = dCostototale + NTSCDec(!tsk_basecost)
                  dPrezzototale = dPrezzototale + NTSCDec(!tsk_baseprz)
                  Select Case NTSCStr(!tsk_darave)
                    'Una cartella può avere Segno Dare/Avere di C.A.
                    'D (Costo) oppure R (Ricavo da fattura in definitiva)
                    Case "D"
                      nSegno = 1
                      dCostorimanente = dCostorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      dCostoprogrammato = dCostoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                      dTotalecostorimanente = dTotalecostorimanente + (nSegno * NTSCDec(!tsk_totremcost))
                      dTotalecostoprogrammato = dTotalecostoprogrammato + (nSegno * NTSCDec(!tsk_totcost))
                    Case "R"
                      nSegno = 1
                      dRicavodefinitivorimanente = dRicavodefinitivorimanente + (nSegno * NTSCDec(!tsk_remcost))
                  End Select
                  bPrevgrup = True
                  strLivello = NTSCStr(!tsk_livello)
                  lTaskid = NTSCInt(!tsk_taskid)
                End If
              End If
            End If
          End With
        Next
      End If
      If dCostototale = 0 Then
        dRicar = 0
      Else
        dRicar = NTSCDec(((NTSCDec(dPrezzoconcordato) - NTSCDec(dCostototale)) / NTSCDec(dCostototale)) * 100)
      End If
      'UPDATE su TTPREVENT
      strSQL = " UPDATE " & strTTPREVENT & _
               " SET pre_costotot = " & CDblSQL(dCostototale) & _
               " , pre_prztot = " & CDblSQL(dPrezzototale) & ", pre_ricar = " & CDblSQL(dRicar) & _
               " , pre_actcost = " & CDblSQL(dCostoeffettivo) & ", pre_remcost = " & CDblSQL(dCostorimanente) & _
               " , pre_cost = " & CDblSQL(dCostoprogrammato) & " , pre_diffcost = " & CDblSQL(dDifferenzecosto) & _
               " , pre_totactcost = " & CDblSQL(dTotalecostoeffettivo) & _
               " , pre_totremcost = " & CDblSQL(dTotalecostorimanente) & _
               " , pre_totcost = " & CDblSQL(dTotalecostoprogrammato) & _
               " , pre_totdiffcost = " & CDblSQL(dTotaledifferenzecostoprogrammato) & _
               " , pre_basecostind = " & CDblSQL(dCostoindirettototale) & _
               " , pre_actcostind = " & CDblSQL(dCostoindirettoeffettivo) & _
               " , pre_costind = " & CDblSQL(dCostoindirettoprogrammato) & _
               " , pre_basefattdef = " & CDblSQL(dRicavodefinitivoprevisto) & _
               " , pre_actfattdef = " & CDblSQL(dRicavodefinitivoeffettivo) & _
               " , pre_remfattdef = " & CDblSQL(dRicavodefinitivorimanente) & _
               " , pre_difffattdef = " & CDblSQL(dRicavodefinitivodifferenze) & _
               " , pre_basefattacc = " & CDblSQL(dRicavoaccontoprevisto) & _
               " , pre_actfattacc = " & CDblSQL(dRicavoaccontoeffettivo) & _
               " , pre_remfattacc = " & CDblSQL(dRicavoaccontorimanente) & _
               " , pre_difffattacc = " & CDblSQL(dRicavoaccontodifferenze) & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " And pre_commeca = " & lComm & _
               " And pre_prevnum = " & lPrev & _
               " And pre_prevrev = " & nRev & _
               " And pre_varid = " & nVar
      If lInstidTTprevent <> 0 Then
        strSQL = strSQL & " and instid = " & lInstidTTprevent
      End If
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function EseguiRicalcolo(ByVal lPrev As Integer, ByVal nRev As Integer, ByVal nVar As Integer, _
                                              ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                              ByVal lInstidTTprevent As Integer, ByVal dPrezzoconcordato As Decimal, _
                                              ByVal dCostototale As Decimal, ByVal dPrezzototale As Decimal, _
                                              ByVal dCostoeffettivo As Decimal, ByVal dCostorimanente As Decimal, _
                                              ByVal dCostoprogrammato As Decimal, ByVal dDifferenzecosto As Decimal, _
                                              ByVal dTotalecostoeffettivo As Decimal, _
                                              ByVal dTotalecostorimanente As Decimal, _
                                              ByVal dTotalecostoprogrammato As Decimal, _
                                              ByVal dTotaledifferenzecostoprogrammato As Decimal, _
                                              ByVal dCostoindirettototale As Decimal, _
                                              ByVal dCostoindirettoeffettivo As Decimal, _
                                              ByVal dCostoindirettoprogrammato As Decimal, _
                                              ByVal dRicavodefinitivoprevisto As Decimal, _
                                              ByVal dRicavodefinitivoeffettivo As Decimal, _
                                              ByVal dRicavodefinitivorimanente As Decimal, _
                                              ByVal dRicavodefinitivodifferenze As Decimal, _
                                              ByVal dRicavoaccontoprevisto As Decimal, _
                                              ByVal dRicavoaccontoeffettivo As Decimal, _
                                              ByVal dRicavoaccontorimanente As Decimal, _
                                              ByVal dRicavoaccontodifferenze As Decimal, _
                                              ByVal strDitta As String, ByRef dbConn As DbConnection) As Boolean
    Dim strTTTASKS As String = "tasks"
    Dim strTTPREVENT As String = "PREVENT"

    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing

    Dim bPrevgrup As Boolean = False
    Dim bSoloconsuntivo As Boolean = False
    Dim strLivello As String = ""
    Dim lTaskid As Integer = 0
    Dim dCostoparziale As Decimal = 0
    Dim dRicar As Decimal = 0

    Dim nSegno As Integer = 0
    Dim i As Integer = 0
    Dim j As Integer = 0

    Try
      '   CLN__STD.CheckInvokeCustomFunction(
      '--------------------------------------------------------------------------------------------------------------
      If lInstidTTTasks0 <> 0 Then strTTTASKS = "tttasks"
      If lInstidTTprevent <> 0 Then strTTPREVENT = "TTPREVENT"
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM " & strTTTASKS & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString & _
        " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3, tsk_uid3"
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      If Not dsTmp.Tables("TASKS").Rows.Count = 0 Then
        bPrevgrup = False
        bSoloconsuntivo = False
        For i = 0 To (dsTmp.Tables("TASKS").Rows.Count - 1)
          With dsTmp.Tables("TASKS").Rows(i)
            If (NTSCInt(!tsk_prevnum) = lPrev) And (NTSCInt(!tsk_prevrev) = nRev) And (NTSCInt(!tsk_varid) = nVar) Then
              If bPrevgrup Then
                Select Case strLivello
                  Case "0"
                    If NTSCInt(!tsk_uid0) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                  Case "1"
                    If NTSCInt(!tsk_uid1) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                  Case "2"
                    If NTSCInt(!tsk_uid2) = lTaskid Then
                      bSoloconsuntivo = True
                    Else
                      bSoloconsuntivo = False
                      bPrevgrup = False
                    End If
                End Select
              End If
              If (NTSCStr(!tsk_summary) = "N") Then
                If (NTSCStr(!tsk_ricprev) <> "N" And NTSCStr(!tsk_ricprev) <> "I") Then
                  Select Case NTSCStr(!tsk_livello)
                    Case "0"
                      strSQL = "SELECT * FROM " & strTTTASKS & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND tsk_commeca = " & lComm & _
                        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString & _
                        " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3," & _
                        " tsk_uid3"
                    Case "1"
                      strSQL = "SELECT * FROM " & strTTTASKS & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND tsk_commeca = " & lComm & _
                        " AND tsk_uid0 = " & NTSCInt(!tsk_uidpadre) & _
                        " AND tsk_taskid <> " & NTSCInt(!tsk_uidpadre) & _
                        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString & _
                        " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3," & _
                        " tsk_uid3"
                    Case "2"
                      strSQL = "SELECT * FROM " & strTTTASKS & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND tsk_commeca = " & lComm & _
                        " AND tsk_uid1 = " & NTSCInt(!tsk_uidpadre) & _
                        " AND tsk_taskid <> " & NTSCInt(!tsk_uidpadre) & _
                        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString & _
                        " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3," & _
                        " tsk_uid3"
                    Case "3"
                      strSQL = "SELECT * FROM " & strTTTASKS & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND tsk_commeca = " & lComm & _
                        " AND tsk_uid2 = " & NTSCInt(!tsk_uidpadre) & _
                        " AND tsk_taskid <> " & NTSCInt(!tsk_uidpadre) & _
                        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString & _
                        " ORDER BY tsk_posiz0, tsk_uid0, tsk_posiz1, tsk_uid1, tsk_posiz2, tsk_uid2, tsk_posiz3," & _
                        " tsk_uid3"
                  End Select
                  dsTmp2 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
                  dCostoparziale = 0
                  j = 0
                  While (NTSCInt(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) <> NTSCInt(!tsk_taskid))
                    If (NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_summary) = "N") _
                    Or (NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_summary) = "S" _
                    And NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S") Then
                      Select Case NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_darave)
                        Case "D", "I"
                          dCostoparziale = dCostoparziale + NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basecost)
                        Case "A"
                          dCostoparziale = dCostoparziale + ((-1) * NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basecost))
                      End Select
                    End If
                    j = j + 1
                  End While
                  If (NTSCStr(!tsk_ricprev) = "T") Then
                    If dCostototale < 0 Then
                      !tsk_impbase = 0
                    Else
                      !tsk_impbase = dCostototale
                    End If
                  Else
                    If (NTSCStr(!tsk_ricprev) = "P") Then
                      If dCostoparziale < 0 Then dCostoparziale = 0
                      !tsk_impbase = dCostoparziale
                    Else
                      If (NTSCDec(!tsk_percpz) = 0) And (NTSCDec(!tsk_percpr) <> 0) Then
                        If dCostototale < 0 Then
                          !tsk_impbase = 0
                        Else
                          !tsk_impbase = dCostototale
                        End If
                        !tsk_basecost = NTSCDec((NTSCDec(!tsk_impbase) * NTSCDec(!tsk_percpr)) / 100)
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = (-1) * NTSCDec(!tsk_basecost)
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = (-1) * NTSCDec(!tsk_basecost)
                          Case "E"
                            !tsk_actcost = NTSCDec(!tsk_basecost)
                            !tsk_remcost = 0
                            !tsk_cost = NTSCDec(!tsk_actcost)
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = NTSCDec(!tsk_actcost)
                            !tsk_totremcost = 0
                            !tsk_totcost = NTSCDec(!tsk_actcost)
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                      If (NTSCDec(!tsk_percpz) <> 0) And (NTSCDec(!tsk_percpr) = 0) Then
                        If dCostoparziale < 0 Then dCostoparziale = 0
                        !tsk_impbase = dCostoparziale
                        !tsk_basecost = NTSCDec((dCostoparziale * NTSCDec(!tsk_percpz)) / 100)
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = (-1) * NTSCDec(!tsk_basecost)
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = (-1) * NTSCDec(!tsk_basecost)
                          Case "E"
                            !tsk_actcost = NTSCDec(!tsk_basecost)
                            !tsk_remcost = 0
                            !tsk_cost = NTSCDec(!tsk_actcost)
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = NTSCDec(!tsk_actcost)
                            !tsk_totremcost = 0
                            !tsk_totcost = NTSCDec(!tsk_actcost)
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                      If (NTSCDec(!tsk_percpz) = 0) And (NTSCDec(!tsk_percpr) = 0) Then
                        If dCostoparziale < 0 Then dCostoparziale = 0
                        !tsk_impbase = dCostoparziale
                        !tsk_basecost = 0
                        Select Case NTSCStr(!tsk_ricprev)
                          Case "S"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = 0
                          Case "E"
                            !tsk_actcost = 0
                            !tsk_remcost = 0
                            !tsk_cost = 0
                            !tsk_diffcost = 0
                            '--
                            !tsk_totactcost = 0
                            !tsk_totremcost = 0
                            !tsk_totcost = 0
                            !tsk_totdiffcost = 0
                        End Select
                      End If
                    End If
                  End If
                End If
                '-----
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "A"
                    If NTSCStr(!tsk_darave) = "A" Then
                      nSegno = -1
                    Else
                      nSegno = 1
                    End If
                    If Not bSoloconsuntivo Then
                      dCostototale = dCostototale + (nSegno * NTSCDec(!tsk_basecost))
                      If NTSCStr(!tsk_darave) = "I" Then dCostoindirettototale = dCostoindirettototale + (nSegno * NTSCDec(!tsk_basecost))
                      dPrezzototale = dPrezzototale + (nSegno * NTSCDec(!tsk_baseprz))
                      '---
                      dCostorimanente = dCostorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      dCostoprogrammato = dCostoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                      dTotalecostorimanente = dTotalecostorimanente + (nSegno * NTSCDec(!tsk_totremcost))
                      dTotalecostoprogrammato = dTotalecostoprogrammato + (nSegno * NTSCDec(!tsk_totcost))
                      '---
                    End If
                    dCostoeffettivo = dCostoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                    If NTSCStr(!tsk_darave) = "I" Then dCostoindirettoeffettivo = dCostoindirettoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                    If NTSCStr(!tsk_darave) = "I" Then dCostoindirettoprogrammato = dCostoindirettoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                    dDifferenzecosto = dDifferenzecosto + (nSegno * NTSCDec(!tsk_diffcost))
                    dTotalecostoeffettivo = dTotalecostoeffettivo + (nSegno * NTSCDec(!tsk_totactcost))
                    dTotaledifferenzecostoprogrammato = dTotaledifferenzecostoprogrammato + (nSegno * NTSCDec(!tsk_totdiffcost))
                  Case "R", "T", "S", "U"
                    If NTSCStr(!tsk_darave) = "T" Or NTSCStr(!tsk_darave) = "U" Then
                      nSegno = -1
                    Else
                      nSegno = 1
                    End If
                    If Not bSoloconsuntivo Then
                      If NTSCStr(!tsk_darave) = "R" Or NTSCStr(!tsk_darave) = "T" Then
                        dRicavodefinitivoprevisto = dRicavodefinitivoprevisto + (nSegno * NTSCDec(!tsk_basecost))
                        dRicavodefinitivorimanente = dRicavodefinitivorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      Else
                        dRicavoaccontoprevisto = dRicavoaccontoprevisto + (nSegno * NTSCDec(!tsk_basecost))
                        dRicavoaccontorimanente = dRicavoaccontorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      End If
                    End If
                    If NTSCStr(!tsk_darave) = "R" Or NTSCStr(!tsk_darave) = "T" Then
                      dRicavodefinitivoeffettivo = dRicavodefinitivoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                      dRicavodefinitivodifferenze = dRicavodefinitivodifferenze + (nSegno * NTSCDec(!tsk_diffcost))
                    Else
                      dRicavoaccontoeffettivo = dRicavoaccontoeffettivo + (nSegno * NTSCDec(!tsk_actcost))
                      dRicavoaccontodifferenze = dRicavoaccontodifferenze + (nSegno * NTSCDec(!tsk_diffcost))
                    End If
                End Select
                '-----
              Else
                'Se PREVGRUP = "N" oppure "F" niente
                If (NTSCStr(!tsk_prevgrup) = "S") Then
                  dCostototale = dCostototale + NTSCDec(!tsk_basecost)
                  dPrezzototale = dPrezzototale + NTSCDec(!tsk_baseprz)
                  Select Case NTSCStr(!tsk_darave)
                    'Una cartella può avere Segno Dare/Avere di C.A.
                    'D (Costo) oppure R (Ricavo da fattura in definitiva)
                    Case "D"
                      nSegno = 1
                      dCostorimanente = dCostorimanente + (nSegno * NTSCDec(!tsk_remcost))
                      dCostoprogrammato = dCostoprogrammato + (nSegno * NTSCDec(!tsk_cost))
                      dTotalecostorimanente = dTotalecostorimanente + (nSegno * NTSCDec(!tsk_totremcost))
                      dTotalecostoprogrammato = dTotalecostoprogrammato + (nSegno * NTSCDec(!tsk_totcost))
                    Case "R"
                      nSegno = 1
                      dRicavodefinitivorimanente = dRicavodefinitivorimanente + (nSegno * NTSCDec(!tsk_remcost))
                  End Select
                  bPrevgrup = True
                  strLivello = NTSCStr(!tsk_livello)
                  lTaskid = NTSCInt(!tsk_taskid)
                End If
              End If
            End If
          End With
        Next
      End If
      If dCostototale = 0 Then
        dRicar = 0
      Else
        dRicar = NTSCDec(((NTSCDec(dPrezzoconcordato) - NTSCDec(dCostototale)) / NTSCDec(dCostototale)) * 100)
      End If
      'UPDATE su TTPREVENT
      strSQL = "UPDATE " & strTTPREVENT & _
        " SET pre_costotot = " & CDblSQL(dCostototale) & ", pre_prztot = " & CDblSQL(dPrezzototale) & "," & _
        " pre_ricar = " & CDblSQL(dRicar) & ", pre_actcost = " & CDblSQL(dCostoeffettivo) & "," & _
        " pre_remcost = " & CDblSQL(dCostorimanente) & ", pre_cost = " & CDblSQL(dCostoprogrammato) & "," & _
        " pre_diffcost = " & CDblSQL(dDifferenzecosto) & ", pre_totactcost = " & CDblSQL(dTotalecostoeffettivo) & "," & _
        " pre_totremcost = " & CDblSQL(dTotalecostorimanente) & "," & _
        " pre_totcost = " & CDblSQL(dTotalecostoprogrammato) & "," & _
        " pre_totdiffcost = " & CDblSQL(dTotaledifferenzecostoprogrammato) & "," & _
        " pre_basecostind = " & CDblSQL(dCostoindirettototale) & "," & _
        " pre_actcostind = " & CDblSQL(dCostoindirettoeffettivo) & "," & _
        " pre_costind = " & CDblSQL(dCostoindirettoprogrammato) & "," & _
        " pre_basefattdef = " & CDblSQL(dRicavodefinitivoprevisto) & "," & _
        " pre_actfattdef = " & CDblSQL(dRicavodefinitivoeffettivo) & "," & _
        " pre_remfattdef = " & CDblSQL(dRicavodefinitivorimanente) & "," & _
        " pre_difffattdef = " & CDblSQL(dRicavodefinitivodifferenze) & "," & _
        " pre_basefattacc = " & CDblSQL(dRicavoaccontoprevisto) & "," & _
        " pre_actfattacc = " & CDblSQL(dRicavoaccontoeffettivo) & "," & _
        " pre_remfattacc = " & CDblSQL(dRicavoaccontorimanente) & "," & _
        " pre_difffattacc = " & CDblSQL(dRicavoaccontodifferenze) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pre_commeca = " & lComm & _
        " AND pre_prevnum = " & lPrev & _
        " AND pre_prevrev = " & nRev & _
        " AND pre_varid = " & nVar & _
        IIf(lInstidTTprevent <> 0, " AND instid = " & lInstidTTprevent, "").ToString
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaPadri(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                            ByVal lInstidTTprevent As Integer, ByVal strDitta As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return AggiornaPadri(lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaPadri(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                            ByVal lInstidTTprevent As Integer, ByVal strDitta As String, _
                                            ByRef dbConn As DbConnection) As Boolean
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strTTTASKS As String = "tasks"
    Dim strTTPREVENT As String = "PREVENT"

    Try
      '   CLN__STD.CheckInvokeCustomFunction(
      '--------------------------------------------------------------------------------------------------------------
      If lInstidTTTasks0 <> 0 Then strTTTASKS = "tttasks"
      If lInstidTTprevent <> 0 Then strTTPREVENT = "TTPREVENT"
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tsk_taskid, tsk_prevgrup, tsk_gestcost, tsk_summary, tsk_baseprz, tsk_basecost, tsk_baseqta," & _
        " tsk_basework FROM " & strTTTASKS & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        " AND tsk_uidpadre = 0" & _
        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      For i = 0 To (dsTmp.Tables("TASKS").Rows.Count - 1)
        With dsTmp.Tables("TASKS").Rows(i)
          AggiornaCostiCapitolo(NTSCInt(!tsk_taskid), NTSCStr(!tsk_prevgrup), NTSCStr(!tsk_gestcost), _
            NTSCStr(!tsk_summary), NTSCDec(!tsk_baseprz), NTSCDec(!tsk_basecost), NTSCDec(!tsk_baseqta), _
            NTSCDec(!tsk_basework), lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, dbConn)
        End With
      Next
      strSQL = "SELECT tsk_taskid, tsk_flevas, tsk_ordflevas, tsk_baseprz, tsk_basecost, tsk_baseqta, tsk_basework" & _
        " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
        " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        " AND tsk_uidpadre = 0 " & _
        " AND pre_stato <> '5'" & _
        IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
        IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      For i = 0 To (dsTmp.Tables("TASKS").Rows.Count - 1)
        With dsTmp.Tables("TASKS").Rows(i)
          AggiornaEvasioniCapitolo(NTSCInt(!tsk_taskid), NTSCStr(!tsk_flevas), NTSCStr(!tsk_ordflevas), _
            lInstidTTTasks0, lComm, lInstidTTprevent, False, strDitta, dbConn)
        End With
      Next
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaCostiCapitolo(ByVal lIdTaskPadre As Integer, ByVal strPrevgrup As String, _
                                                    ByVal strGestcost As String, ByVal strSummary As String, _
                                                    ByVal dPbaseprz As Decimal, ByVal dPbasecost As Decimal, _
                                                    ByVal dPbaseqta As Decimal, ByVal dPbasework As Decimal, _
                                                    ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                                    ByVal lInstidTTprevent As Integer, ByVal strDitta As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return AggiornaCostiCapitolo(lIdTaskPadre, strPrevgrup, strGestcost, strSummary, dPbaseprz, dPbasecost, _
        dPbaseqta, dPbasework, lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaCostiCapitolo(ByVal lIdTaskPadre As Integer, ByVal strPrevgrup As String, _
                                                    ByVal strGestcost As String, ByVal strSummary As String, _
                                                    ByVal dPbaseprz As Decimal, ByVal dPbasecost As Decimal, _
                                                    ByVal dPbaseqta As Decimal, ByVal dPbasework As Decimal, _
                                                    ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                                    ByVal lInstidTTprevent As Integer, ByVal strDitta As String, _
                                                    ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    ' previsti
    Dim dbaseprz As Decimal = 0
    Dim dBasecost As Decimal = 0
    Dim dBasework As Decimal = 0
    Dim dBaseqta As Decimal = 0
    ' consuntivi
    Dim dactcost As Decimal = 0      ' effettivi
    Dim dactwork As Decimal = 0
    Dim dactqta As Decimal = 0
    Dim dremcost As Decimal = 0     ' rimanenti
    Dim dremwork As Decimal = 0
    Dim dremqta As Decimal = 0
    Dim dcost As Decimal = 0     ' programmati
    Dim dwork As Decimal = 0
    Dim dQta As Decimal = 0
    Dim dDiffcost As Decimal = 0    ' differenze
    Dim dDiffwork As Decimal = 0
    Dim dDiffqta As Decimal = 0
    ' ordini
    Dim dordcost As Decimal = 0     ' effettivi
    Dim dordwork As Decimal = 0
    Dim dordqta As Decimal = 0
    Dim dordremcost As Decimal = 0   ' rimanenti
    Dim dordremwork As Decimal = 0
    Dim dordremqta As Decimal = 0
    Dim dordcostprg As Decimal = 0  ' programmati
    Dim dordworkprg As Decimal = 0
    Dim dordqtaprg As Decimal = 0
    Dim dorddiffcost As Decimal = 0 ' differenze
    Dim dorddiffwork As Decimal = 0
    Dim dorddiffqta As Decimal = 0
    '-----
    Dim strIIF1 As String = ""
    Dim strIIF2 As String = ""
    Dim dPerc As Decimal = 0
    Dim dPercord As Decimal = 0
    Dim bPrevalenzaconsuntivo As Boolean = False
    'totali
    Dim dTotalecostoeff As Decimal = 0
    Dim dTotalecostorim As Decimal = 0
    Dim dTotalecostoprog As Decimal = 0
    Dim dTotalecostodiff As Decimal = 0
    Dim dTotalequantprog As Decimal = 0
    Dim dTotalequantdiff As Decimal = 0
    Dim dTotalelavoroprog As Decimal = 0
    Dim dTotalelavorodiff As Decimal = 0

    Dim strA As String = ""
    Dim strC As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsTmpbis As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing
    Dim dsTmp2bis As DataSet = Nothing
    Dim dsTmp3 As DataSet = Nothing
    Dim strTTTASKS As String = "tasks"
    Dim strTTPREVENT As String = "PREVENT"

    Dim nSegno As Integer = 0
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim k As Integer = 0
    Dim w As Integer = 0
    Dim y As Integer = 0

    Try
      '   CLN__STD.CheckInvokeCustomFunction(
      '--------------------------------------------------------------------------------------------------------------
      If lInstidTTTasks0 <> 0 Then strTTTASKS = "tttasks"
      If lInstidTTprevent <> 0 Then strTTPREVENT = "TTPREVENT"
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tsk_taskid, tsk_prevgrup, tsk_gestcost, tsk_summary, tsk_baseprz, tsk_basecost, tsk_baseqta," & _
        " tsk_basework FROM " & strTTTASKS & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        " AND tsk_uidpadre = " & lIdTaskPadre & _
        IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      For i = 0 To (dsTmp.Tables("TASKS").Rows.Count - 1)
        strSQL = "SELECT tsk_taskid, tsk_prevgrup, tsk_gestcost, tsk_summary, tsk_baseprz, tsk_basecost," & _
          " tsk_baseqta, tsk_basework" & _
          " FROM " & strTTTASKS & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tsk_commeca = " & lComm & _
          " AND tsk_uidpadre = " & NTSCInt(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
          IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
        dsTmp2 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
        For j = 0 To (dsTmp2.Tables("TASKS").Rows.Count - 1)
          If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
            dbaseprz = 0
            dBasecost = 0
            dBasework = 0
            dBaseqta = 0
          Else ' dal record ...
            dbaseprz = NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_baseprz)
            dBasecost = NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basecost)
            dBasework = NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_basework)
            dBaseqta = NTSCDec(dsTmp2.Tables("TASKS").Rows(j)!tsk_baseqta)
          End If
          dactcost = 0 : dactwork = 0 : dactqta = 0 : dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0
          dwork = 0 : dQta = 0 : dDiffcost = 0 : dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0
          dordqta = 0 : dordremcost = 0 : dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0
          dordqtaprg = 0 : dorddiffcost = 0 : dorddiffwork = 0 : dorddiffqta = 0
          strSQL = "SELECT tsk_gestcost, tsk_darave, tsk_baseprz, tsk_basecost, tsk_actcost, tsk_remcost, tsk_cost," & _
            " tsk_baseqta, tsk_actqta, tsk_remqta, tsk_qta, tsk_basework, tsk_actwork, tsk_remwork, tsk_work," & _
            " tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcost, tsk_ordremcost, tsk_ordcostprg, tsk_ordqta," & _
            " tsk_ordremqta, tsk_ordqtaprg, tsk_ordwork, tsk_ordremwork, tsk_ordworkprg, tsk_orddiffwork," & _
            " tsk_orddiffqta, tsk_orddiffcost" & _
            " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt " & " AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca " & " AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum " & " AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev " & " AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
            " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
            " AND tsk_commeca = " & lComm & _
            " AND tsk_uidpadre = " & NTSCInt(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) & _
            " AND pre_intest = 'I'" & _
            " AND pre_stato IN ('3', '6')" & _
            IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
            IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
          dsTmp3 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
          For k = 0 To (dsTmp3.Tables("TASKS").Rows.Count - 1)
            With dsTmp3.Tables("TASKS").Rows(k)
              Select Case NTSCStr(!tsk_gestcost)
                Case "1"
                  'Tutti i valori a 0:
                  'le somme non cambiamo
                Case "2"
                  'Solo costi
                  Select Case NTSCStr(!tsk_darave)
                    Case "D", "I", "R", "S"
                      'Non è rettifica: * +1
                      nSegno = 1
                    Case "A", "T", "U"
                      'E' rettifica: * -1
                      nSegno = -1
                  End Select
                  If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
                    dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                    dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                  End If
                  dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                  dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                  dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                  dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                  dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                  dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                  dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                  dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                Case "3"
                  'Costi e qta
                  Select Case NTSCStr(!tsk_darave)
                    Case "D", "I", "R", "S"
                      'Non è rettifica: * +1
                      nSegno = 1
                    Case "A", "T", "U"
                      'E' rettifica: * -1
                      nSegno = -1
                  End Select
                  If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
                    dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                    dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                    dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
                  End If
                  dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                  dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
                  dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                  dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
                  dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                  dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
                  dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                  dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                  dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                  dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                  dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                  dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
                  dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
                  dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
                  dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
                  dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
                Case "4"
                  'Costi e lavoro
                  Select Case NTSCStr(!tsk_darave)
                    Case "D", "I", "R", "S"
                      'Non è rettifica: * +1
                      nSegno = 1
                    Case "A", "T", "U"
                      'E' rettifica: * -1
                      nSegno = -1
                  End Select
                  If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
                    dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                    dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                    dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
                  End If
                  dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                  dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
                  dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                  dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
                  dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                  dwork = dwork + (nSegno * NTSCDec(!tsk_work))
                  dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                  dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                  dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                  dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                  dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                  dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
                  dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
                  dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
                  dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
                  dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
                Case "5"
                  'Costi, qta e lavoro
                  Select Case NTSCStr(!tsk_darave)
                    Case "D", "I", "R", "S"
                      'Non è rettifica: * +1
                      nSegno = 1
                    Case "A", "T", "U"
                      'E' rettifica: * -1
                      nSegno = -1
                  End Select
                  If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
                    dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                    dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                    dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
                    dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
                  End If
                  dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                  dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
                  dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
                  dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                  dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
                  dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
                  dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                  dwork = dwork + (nSegno * NTSCDec(!tsk_work))
                  dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
                  dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                  dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                  dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                  dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                  dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                  dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
                  dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
                  dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
                  dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
                  dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
                  dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
                  dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
                  dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
                  dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
                  dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
              End Select
            End With
          Next
          If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_summary) = "S" Then
            'Azzero i campi che non servono
            Select Case NTSCInt(dsTmp2.Tables("TASKS").Rows(j)!tsk_gestcost)
              Case 1
                dbaseprz = 0 : dBasecost = 0 : dBasework = 0 : dBaseqta = 0 : dactcost = 0 : dactwork = 0
                dactqta = 0 : dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0 : dwork = 0 : dQta = 0
                dDiffcost = 0 : dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0 : dordqta = 0
                dordremcost = 0 : dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0
                dordqtaprg = 0 : dorddiffcost = 0 : dorddiffwork = 0 : dorddiffqta = 0
              Case 2
                dBasework = 0 : dBaseqta = 0 : dactwork = 0 : dactqta = 0 : dremwork = 0 : dremqta = 0
                dwork = 0 : dQta = 0 : dDiffwork = 0 : dDiffqta = 0 : dordwork = 0 : dordqta = 0 : dordremwork = 0
                dordremqta = 0 : dordworkprg = 0 : dordqtaprg = 0 : dorddiffwork = 0 : dorddiffqta = 0
                If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S" Then  ' ricalcola
                  dremcost = dBasecost - dactcost + dDiffcost
                  dcost = dactcost + dremcost
                  dordremcost = dBasecost - dordcost + dorddiffcost
                  dordcostprg = dordcost + dordremcost
                End If
              Case 3
                dBasework = 0 : dactwork = 0 : dremwork = 0 : dwork = 0 : dDiffwork = 0 : dordwork = 0
                dordremwork = 0 : dordworkprg = 0 : dorddiffwork = 0
                If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S" Then  ' ricalcola
                  dremcost = dBasecost - dactcost + dDiffcost
                  dcost = dactcost + dremcost
                  dordremcost = dBasecost - dordcost + dorddiffcost
                  dordcostprg = dordcost + dordremcost
                  dremqta = dBaseqta - dactqta + dDiffqta
                  dQta = dactqta + dremqta
                  dordremqta = dBaseqta - dordqta + dorddiffqta
                  dordqtaprg = dordqta + dordremqta
                End If
              Case 4
                dBaseqta = 0 : dactqta = 0 : dremqta = 0 : dQta = 0 : dDiffqta = 0 : dordqta = 0
                dordremqta = 0 : dordqtaprg = 0 : dorddiffqta = 0
                If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S" Then  ' ricalcola
                  dremcost = dBasecost - dactcost + dDiffcost
                  dcost = dactcost + dremcost
                  dordremcost = dBasecost - dordcost + dorddiffcost
                  dordcostprg = dordcost + dordremcost
                  dremwork = dBasework - dactwork + dDiffwork
                  dwork = dactwork + dremwork
                  dordremwork = dBasework - dordwork + dorddiffwork
                  dordworkprg = dordwork + dordremwork
                End If
              Case 5
                If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) = "S" Then  ' ricalcola
                  dremcost = dBasecost - dactcost + dDiffcost
                  dcost = dactcost + dremcost
                  dordremcost = dBasecost - dordcost + dorddiffcost
                  dordcostprg = dordcost + dordremcost
                  dremqta = dBaseqta - dactqta + dDiffqta
                  dQta = dactqta + dremqta
                  dordremqta = dBaseqta - dordqta + dorddiffqta
                  dordqtaprg = dordqta + dordremqta
                  dremwork = dBasework - dactwork + dDiffwork
                  dwork = dactwork + dremwork
                  dordremwork = dBasework - dordwork + dorddiffwork
                  dordworkprg = dordwork + dordremwork
                End If
            End Select
            '---------
            If NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_prevgrup) <> "S" Then
              strA = "tsk_baseprzun = 0, tsk_basecostun = 0, tsk_perqta = 1, tsk_basecost = " & CDblSQL(dBasecost) & ", tsk_basework = " & CDblSQL(dBasework) & ", tsk_baseqta = " & CDblSQL(dBaseqta) & ","
            Else
              strA = ""
            End If
            '---------
            If dcost = 0 Then
              dPerc = 0
            Else
              dPerc = ArrDbl((dactcost / dcost * 100), 2)
            End If
            If dordcostprg = 0 Then
              dPercord = 0
            Else
              dPercord = ArrDbl(dordcost / dordcostprg * 100, 2)
            End If
            '---------
            If dPerc >= dPercord Then
              bPrevalenzaconsuntivo = True
            Else
              bPrevalenzaconsuntivo = False
            End If
            If bPrevalenzaconsuntivo Then
              dTotalecostoeff = dactcost
              dTotalecostorim = dremcost
              dTotalecostoprog = dcost
              dTotalecostodiff = dDiffcost
              dTotalequantprog = dQta
              dTotalequantdiff = dDiffqta
              dTotalelavoroprog = dwork
              dTotalelavorodiff = dDiffwork
            Else
              dTotalecostoeff = dordcost
              dTotalecostorim = dordremcost
              dTotalecostoprog = dordcost
              dTotalecostodiff = dorddiffcost
              dTotalequantprog = dordqta
              dTotalequantdiff = dorddiffqta
              dTotalelavoroprog = dordwork
              dTotalelavorodiff = dorddiffwork
            End If
            '---------
            strIIF1 = "CASE WHEN tsk_gestcost = '1' THEN 0 ELSE " & CDblSQL(dPerc) & " END"
            strIIF2 = "CASE WHEN tsk_gestcost = '1' THEN 0 ELSE " & CDblSQL(dPercord) & " END"
            '---------
            strC = "UPDATE " & strTTTASKS & _
              " SET " & strA & " tsk_actcost = " & CDblSQL(dactcost) & "," & _
              " tsk_actwork = " & CDblSQL(dactwork) & ", tsk_actqta = " & CDblSQL(dactqta) & "," & _
              " tsk_remcost = " & CDblSQL(dremcost) & ", tsk_remwork = " & CDblSQL(dremwork) & "," & _
              " tsk_remqta = " & CDblSQL(dremqta) & ", tsk_cost = " & CDblSQL(dcost) & "," & _
              " tsk_work = " & CDblSQL(dwork) & ", tsk_qta = " & CDblSQL(dQta) & "," & _
              " tsk_diffcost = " & CDblSQL(dDiffcost) & ", tsk_diffwork = " & CDblSQL(dDiffwork) & "," & _
              " tsk_diffqta = " & CDblSQL(dDiffqta) & ", tsk_ordcost = " & CDblSQL(dordcost) & "," & _
              " tsk_ordwork = " & CDblSQL(dordwork) & ", tsk_ordqta = " & CDblSQL(dordqta) & "," & _
              " tsk_ordremcost = " & CDblSQL(dordremcost) & ", tsk_ordremwork = " & CDblSQL(dordremwork) & "," & _
              " tsk_ordremqta = " & CDblSQL(dordremqta) & ", tsk_ordcostprg = " & CDblSQL(dordcostprg) & "," & _
              " tsk_ordworkprg = " & CDblSQL(dordworkprg) & ", tsk_ordqtaprg = " & CDblSQL(dordqtaprg) & "," & _
              " tsk_orddiffcost = " & CDblSQL(dorddiffcost) & ", tsk_orddiffwork = " & CDblSQL(dorddiffwork) & "," & _
              " tsk_orddiffqta = " & CDblSQL(dorddiffqta) & ", tsk_pctworkcomp = " & strIIF1 & "," & _
              " tsk_pctordcomp = " & strIIF2 & ", tsk_totactcost = " & CDblSQL(dTotalecostoeff) & "," & _
              " tsk_totremcost = " & CDblSQL(dTotalecostorim) & ", tsk_totcost = " & CDblSQL(dTotalecostoprog) & "," & _
              " tsk_totdiffcost = " & CDblSQL(dTotalecostodiff) & ", tsk_totqta = " & CDblSQL(dTotalequantprog) & "," & _
              " tsk_totdiffqta = " & CDblSQL(dTotalequantdiff) & ", tsk_totwork = " & CDblSQL(dTotalelavoroprog) & "," & _
              " tsk_totdiffwork = " & CDblSQL(dTotalelavorodiff) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tsk_commeca = " & lComm & _
              " AND tsk_taskid = " & NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) & _
              IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString
            oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
          dbaseprz = 0 : dBasecost = 0 : dBasework = 0 : dBaseqta = 0
        Else ' dal record ...
          dbaseprz = NTSCDec(dsTmp.Tables("TASKS").Rows(i)!tsk_baseprz)
          dBasecost = NTSCDec(dsTmp.Tables("TASKS").Rows(i)!tsk_basecost)
          dBasework = NTSCDec(dsTmp.Tables("TASKS").Rows(i)!tsk_basework)
          dBaseqta = NTSCDec(dsTmp.Tables("TASKS").Rows(i)!tsk_baseqta)
        End If
        dactcost = 0 : dactwork = 0 : dactqta = 0 : dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0
        dwork = 0 : dQta = 0 : dDiffcost = 0 : dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0
        dordqta = 0 : dordremcost = 0 : dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0
        dordqtaprg = 0 : dorddiffcost = 0 : dorddiffwork = 0 : dorddiffqta = 0
        strSQL = "SELECT tsk_gestcost, tsk_darave, tsk_baseprz, tsk_basecost, tsk_actcost, tsk_remcost, tsk_cost," & _
          " tsk_baseqta, tsk_actqta, tsk_remqta, tsk_qta, tsk_basework, tsk_actwork, tsk_remwork, tsk_work," & _
          " tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcost, tsk_ordremcost, tsk_ordcostprg, tsk_ordqta," & _
          " tsk_ordremqta, tsk_ordqtaprg, tsk_ordwork, tsk_ordremwork, tsk_ordworkprg, tsk_orddiffwork," & _
          " tsk_orddiffqta, tsk_orddiffcost" & _
          " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt " & " AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca " & " AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum " & " AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev " & " AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
          " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
          " AND tsk_commeca = " & lComm & _
          " AND tsk_uidpadre = " & NTSCInt(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
          " AND pre_intest = 'I'" & _
          " AND pre_stato IN('3', '6')" & _
          IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
          IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
        dsTmp2bis = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
        For w = 0 To dsTmp2bis.Tables("TASKS").Rows.Count - 1
          With dsTmp2bis.Tables("TASKS").Rows(w)
            Select Case NTSCStr(!tsk_gestcost)
              Case "1"
                'Tutti i valori a 0:
                'le somme non cambiamo
              Case "2"
                'Solo costi
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "R", "S"
                    'Non è rettifica: * +1
                    nSegno = 1
                  Case "A", "T", "U"
                    'E' rettifica: * -1
                    nSegno = -1
                End Select
                If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
                  dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                  dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                End If
                dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
              Case "3"
                'Costi e qta
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "R", "S"
                    'Non è rettifica: * +1
                    nSegno = 1
                  Case "A", "T", "U"
                    'E' rettifica: * -1
                    nSegno = -1
                End Select
                If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
                  dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                  dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                  dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
                End If
                dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
                dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
                dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
                dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
                dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
                dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
                dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
                dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
              Case "4"
                'Costi e lavoro
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "R", "S"
                    'Non è rettifica: * +1
                    nSegno = 1
                  Case "A", "T", "U"
                    'E' rettifica: * -1
                    nSegno = -1
                End Select
                If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
                  dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                  dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                  dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
                End If
                dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
                dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
                dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                dwork = dwork + (nSegno * NTSCDec(!tsk_work))
                dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
                dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
                dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
                dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
                dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
              Case "5"
                'Costi, qta e lavoro
                Select Case NTSCStr(!tsk_darave)
                  Case "D", "I", "R", "S"
                    'Non è rettifica: * +1
                    nSegno = 1
                  Case "A", "T", "U"
                    'E' rettifica: * -1
                    nSegno = -1
                End Select
                If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
                  dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                  dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                  dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
                  dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
                End If
                dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
                dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
                dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
                dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
                dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
                dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
                dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
                dwork = dwork + (nSegno * NTSCDec(!tsk_work))
                dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
                dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
                dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
                dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
                dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
                dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
                dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
                dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
                dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
                dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
                dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
                dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
                dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
                dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
                dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
                dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
            End Select
          End With
        Next
        If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_summary) = "S" Then
          Select Case NTSCInt(dsTmp.Tables("TASKS").Rows(i)!tsk_gestcost)
            Case 1
              dbaseprz = 0 : dBasecost = 0 : dBasework = 0 : dBaseqta = 0 : dactcost = 0 : dactwork = 0
              dactqta = 0 : dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0 : dwork = 0 : dQta = 0
              dDiffcost = 0 : dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0 : dordqta = 0
              dordremcost = 0 : dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0
              dordqtaprg = 0 : dorddiffcost = 0 : dorddiffwork = 0 : dorddiffqta = 0
            Case 2
              dBasework = 0 : dBaseqta = 0 : dactwork = 0 : dactqta = 0 : dremwork = 0 : dremqta = 0 : dwork = 0
              dQta = 0 : dDiffwork = 0 : dDiffqta = 0 : dordwork = 0 : dordqta = 0 : dordremwork = 0
              dordremqta = 0 : dordworkprg = 0 : dordqtaprg = 0 : dorddiffwork = 0 : dorddiffqta = 0
              ' ricalcola i valori rimanenti e programmato
              If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) = "S" Then  ' ricalcola
                dremcost = dBasecost - dactcost + dDiffcost
                dcost = dactcost + dremcost
                dordremcost = dBasecost - dordcost + dorddiffcost
                dordcostprg = dordcost + dordremcost
              End If
            Case 3
              dBasework = 0 : dactwork = 0 : dremwork = 0 : dwork = 0 : dDiffwork = 0 : dordwork = 0
              dordremwork = 0 : dordworkprg = 0 : dorddiffwork = 0
              If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) = "S" Then  ' ricalcola
                dremcost = dBasecost - dactcost + dDiffcost
                dcost = dactcost + dremcost
                dordremcost = dBasecost - dordcost + dorddiffcost
                dordcostprg = dordcost + dordremcost
                dremqta = dBaseqta - dactqta + dDiffqta
                dQta = dactqta + dremqta
                dordremqta = dBaseqta - dordqta + dorddiffqta
                dordqtaprg = dordqta + dordremqta
              End If
            Case 4
              dBaseqta = 0 : dactqta = 0 : dremqta = 0 : dQta = 0 : dDiffqta = 0 : dordqta = 0 : dordremqta = 0
              dordqtaprg = 0 : dorddiffqta = 0
              If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) = "S" Then  ' ricalcola
                dremcost = dBasecost - dactcost + dDiffcost
                dcost = dactcost + dremcost
                dordremcost = dBasecost - dordcost + dorddiffcost
                dordcostprg = dordcost + dordremcost
                dremwork = dBasework - dactwork + dDiffwork
                dwork = dactwork + dremwork
                dordremwork = dBasework - dordwork + dorddiffwork
                dordworkprg = dordwork + dordremwork
              End If
            Case 5
              If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) = "S" Then  ' ricalcola
                dremcost = dBasecost - dactcost + dDiffcost
                dcost = dactcost + dremcost
                dordremcost = dBasecost - dordcost + dorddiffcost
                dordcostprg = dordcost + dordremcost
                dremqta = dBaseqta - dactqta + dDiffqta
                dQta = dactqta + dremqta
                dordremqta = dBaseqta - dordqta + dorddiffqta
                dordqtaprg = dordqta + dordremqta
                dremwork = dBasework - dactwork + dDiffwork
                dwork = dactwork + dremwork
                dordremwork = dBasework - dordwork + dorddiffwork
                dordworkprg = dordwork + dordremwork
              End If
          End Select
          If NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_prevgrup) <> "S" Then
            strA = "tsk_baseprzun = 0, tsk_basecostun = 0, tsk_perqta = 1, tsk_basecost = " & CDblSQL(dBasecost) & ", tsk_basework = " & CDblSQL(dBasework) & ", tsk_baseqta = " & CDblSQL(dBaseqta) & ","
          Else
            strA = ""
          End If
          If dcost = 0 Then
            dPerc = 0
          Else
            dPerc = ArrDbl((dactcost / dcost * 100), 2)
          End If
          If dordcostprg = 0 Then
            dPercord = 0
          Else
            dPercord = ArrDbl(dordcost / dordcostprg * 100, 2)
          End If
          '---------
          If dPerc >= dPercord Then
            bPrevalenzaconsuntivo = True
          Else
            bPrevalenzaconsuntivo = False
          End If
          If bPrevalenzaconsuntivo Then
            dTotalecostoeff = dactcost
            dTotalecostorim = dremcost
            dTotalecostoprog = dcost
            dTotalecostodiff = dDiffcost
            dTotalequantprog = dQta
            dTotalequantdiff = dDiffqta
            dTotalelavoroprog = dwork
            dTotalelavorodiff = dDiffwork
          Else
            dTotalecostoeff = dordcost
            dTotalecostorim = dordremcost
            dTotalecostoprog = dordcost
            dTotalecostodiff = dorddiffcost
            dTotalequantprog = dordqta
            dTotalequantdiff = dorddiffqta
            dTotalelavoroprog = dordwork
            dTotalelavorodiff = dorddiffwork
          End If
          '---------
          strIIF1 = "CASE WHEN tsk_gestcost = '1' THEN 0 ELSE " & CDblSQL(dPerc) & " END"
          strIIF2 = "CASE WHEN tsk_gestcost = '1' THEN 0 ELSE " & CDblSQL(dPercord) & " END"
          strC = "UPDATE " & strTTTASKS & _
            " SET " & strA & " tsk_actcost = " & CDblSQL(dactcost) & ", tsk_actwork = " & CDblSQL(dactwork) & "," & _
            " tsk_actqta = " & CDblSQL(dactqta) & ", tsk_remcost = " & CDblSQL(dremcost) & "," & _
            " tsk_remwork = " & CDblSQL(dremwork) & ", tsk_remqta = " & CDblSQL(dremqta) & "," & _
            " tsk_cost = " & CDblSQL(dcost) & ", tsk_work = " & CDblSQL(dwork) & "," & _
            " tsk_qta = " & CDblSQL(dQta) & ", tsk_diffcost = " & CDblSQL(dDiffcost) & "," & _
            " tsk_diffwork = " & CDblSQL(dDiffwork) & ", tsk_diffqta = " & CDblSQL(dDiffqta) & "," & _
            " tsk_ordcost = " & CDblSQL(dordcost) & ", tsk_ordwork = " & CDblSQL(dordwork) & "," & _
            " tsk_ordqta = " & CDblSQL(dordqta) & ", tsk_ordremcost = " & CDblSQL(dordremcost) & "," & _
            " tsk_ordremwork = " & CDblSQL(dordremwork) & ", tsk_ordremqta = " & CDblSQL(dordremqta) & "," & _
            " tsk_ordcostprg = " & CDblSQL(dordcostprg) & ", tsk_ordworkprg = " & CDblSQL(dordworkprg) & "," & _
            " tsk_ordqtaprg = " & CDblSQL(dordqtaprg) & ", tsk_orddiffcost = " & CDblSQL(dorddiffcost) & "," & _
            " tsk_orddiffwork = " & CDblSQL(dorddiffwork) & ", tsk_orddiffqta = " & CDblSQL(dorddiffqta) & "," & _
            " tsk_pctworkcomp = " & strIIF1 & ", tsk_pctordcomp = " & strIIF2 & "," & _
            " tsk_totactcost = " & CDblSQL(dTotalecostoeff) & ", tsk_totremcost = " & CDblSQL(dTotalecostorim) & "," & _
            " tsk_totcost = " & CDblSQL(dTotalecostoprog) & ", tsk_totdiffcost = " & CDblSQL(dTotalecostodiff) & "," & _
            " tsk_totqta = " & CDblSQL(dTotalequantprog) & ", tsk_totdiffqta = " & CDblSQL(dTotalequantdiff) & "," & _
            " tsk_totwork = " & CDblSQL(dTotalelavoroprog) & ", tsk_totdiffwork = " & CDblSQL(dTotalelavorodiff) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND tsk_commeca = " & lComm & _
            " AND tsk_taskid = " & NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
            IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString
          oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If strPrevgrup <> "S" Then
        dbaseprz = 0 : dBasecost = 0 : dBasework = 0 : dBaseqta = 0
      Else ' dal record ...
        dbaseprz = dPbaseprz   'snatmp2!tsk_baseprz  DA PASSARE NEI PAR:
        dBasecost = dPbasecost 'snatmp2!tsk_basecost
        dBasework = dPbasework 'snatmp2!tsk_basework
        dBaseqta = dPbaseqta   'snatmp2!tsk_baseqta
      End If
      dactcost = 0 : dactwork = 0 : dactqta = 0 : dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0 : dwork = 0
      dQta = 0 : dDiffcost = 0 : dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0 : dordqta = 0
      dordremcost = 0 : dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0 : dordqtaprg = 0
      dorddiffcost = 0 : dorddiffwork = 0 : dorddiffqta = 0
      strSQL = "SELECT tsk_gestcost, tsk_darave, tsk_baseprz, tsk_basecost, tsk_actcost, tsk_remcost, tsk_cost," & _
        " tsk_baseqta, tsk_actqta, tsk_remqta, tsk_qta, tsk_basework, tsk_actwork, tsk_remwork, tsk_work," & _
        " tsk_diffwork, tsk_diffqta, tsk_diffcost, tsk_ordcost, tsk_ordremcost, tsk_ordcostprg, tsk_ordqta," & _
        " tsk_ordremqta, tsk_ordqtaprg, tsk_ordwork, tsk_ordremwork, tsk_ordworkprg, tsk_orddiffwork, tsk_orddiffqta," & _
        " tsk_orddiffcost" & _
        " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
        " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        " AND tsk_uidpadre = " & lIdTaskPadre & _
        " AND pre_intest = 'I'" & _
        " AND pre_stato IN ('3', '6')" & _
        IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
        IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
      dsTmpbis = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      For y = 0 To (dsTmpbis.Tables("TASKS").Rows.Count - 1)
        With dsTmpbis.Tables("TASKS").Rows(y)
          Select Case strGestcost
            Case "1"
              'Tutti i valori a 0:
              'le somme non cambiamo
            Case "2"
              'Solo costi
              Select Case NTSCStr(!tsk_darave)
                Case "D", "I", "R", "S"
                  'Non è rettifica: * +1
                  nSegno = 1
                Case "A", "T", "U"
                  'E' rettifica: * -1
                  nSegno = -1
              End Select
              If strPrevgrup <> "S" Then
                dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
              End If
              dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
              dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
              dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
              dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
              dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
              dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
              dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
              dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
            Case "3"
              'Costi e qta
              Select Case NTSCStr(!tsk_darave)
                Case "D", "I", "R", "S"
                  'Non è rettifica: * +1
                  nSegno = 1
                Case "A", "T", "U"
                  'E' rettifica: * -1
                  nSegno = -1
              End Select
              If strPrevgrup <> "S" Then
                dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
              End If
              dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
              dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
              dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
              dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
              dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
              dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
              dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
              dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
              dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
              dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
              dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
              dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
              dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
              dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
              dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
              dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
            Case "4"
              'Costi e lavoro
              Select Case NTSCStr(!tsk_darave)
                Case "D", "I", "R", "S"
                  'Non è rettifica: * +1
                  nSegno = 1
                Case "A", "T", "U"
                  'E' rettifica: * -1
                  nSegno = -1
              End Select
              If strPrevgrup <> "S" Then
                dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
              End If
              dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
              dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
              dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
              dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
              dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
              dwork = dwork + (nSegno * NTSCDec(!tsk_work))
              dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
              dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
              dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
              dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
              dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
              dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
              dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
              dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
              dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
              dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
            Case "5"
              'Costi, qta e lavoro
              Select Case NTSCStr(!tsk_darave)
                Case "D", "I", "R", "S"
                  'Non è rettifica: * +1
                  nSegno = 1
                Case "A", "T", "U"
                  'E' rettifica: * -1
                  nSegno = -1
              End Select
              If strPrevgrup <> "S" Then
                dbaseprz = dbaseprz + (nSegno * NTSCDec(!tsk_baseprz))
                dBasecost = dBasecost + (nSegno * NTSCDec(!tsk_basecost))
                dBasework = dBasework + (nSegno * NTSCDec(!tsk_basework))
                dBaseqta = dBaseqta + (nSegno * NTSCDec(!tsk_baseqta))
              End If
              dactcost = dactcost + (nSegno * NTSCDec(!tsk_actcost))
              dactwork = dactwork + (nSegno * NTSCDec(!tsk_actwork))
              dactqta = dactqta + (nSegno * NTSCDec(!tsk_actqta))
              dremcost = dremcost + (nSegno * NTSCDec(!tsk_remcost))
              dremwork = dremwork + (nSegno * NTSCDec(!tsk_remwork))
              dremqta = dremqta + (nSegno * NTSCDec(!tsk_remqta))
              dcost = dcost + (nSegno * NTSCDec(!tsk_cost))
              dwork = dwork + (nSegno * NTSCDec(!tsk_work))
              dQta = dQta + (nSegno * NTSCDec(!tsk_qta))
              dDiffcost = dDiffcost + (nSegno * NTSCDec(!tsk_diffcost))
              dordcost = dordcost + (nSegno * NTSCDec(!tsk_ordcost))
              dordremcost = dordremcost + (nSegno * NTSCDec(!tsk_ordremcost))
              dordcostprg = dordcostprg + (nSegno * NTSCDec(!tsk_ordcostprg))
              dorddiffcost = dorddiffcost + (nSegno * NTSCDec(!tsk_orddiffcost))
              dDiffqta = dDiffqta + (nSegno * NTSCDec(!tsk_diffqta))
              dordqta = dordqta + (nSegno * NTSCDec(!tsk_ordqta))
              dordremqta = dordremqta + (nSegno * NTSCDec(!tsk_ordremqta))
              dordqtaprg = dordqtaprg + (nSegno * NTSCDec(!tsk_ordqtaprg))
              dorddiffqta = dorddiffqta + (nSegno * NTSCDec(!tsk_orddiffqta))
              dDiffwork = dDiffwork + (nSegno * NTSCDec(!tsk_diffwork))
              dordwork = dordwork + (nSegno * NTSCDec(!tsk_ordwork))
              dordremwork = dordremwork + (nSegno * NTSCDec(!tsk_ordremwork))
              dordworkprg = dordworkprg + (nSegno * NTSCDec(!tsk_ordworkprg))
              dorddiffwork = dorddiffwork + (nSegno * NTSCDec(!tsk_orddiffwork))
          End Select
        End With
      Next
      If strSummary = "S" Then
        Select Case NTSCInt(strGestcost)
          Case 1
            dbaseprz = 0 : dBasecost = 0 : dBasework = 0 : dBaseqta = 0 : dactcost = 0 : dactwork = 0 : dactqta = 0
            dremcost = 0 : dremwork = 0 : dremqta = 0 : dcost = 0 : dwork = 0 : dQta = 0 : dDiffcost = 0
            dDiffwork = 0 : dDiffqta = 0 : dordcost = 0 : dordwork = 0 : dordqta = 0 : dordremcost = 0
            dordremwork = 0 : dordremqta = 0 : dordcostprg = 0 : dordworkprg = 0 : dordqtaprg = 0 : dorddiffcost = 0
            dorddiffwork = 0 : dorddiffqta = 0
          Case 2
            dBasework = 0 : dBaseqta = 0 : dactwork = 0 : dactqta = 0 : dremwork = 0 : dremqta = 0 : dwork = 0
            dQta = 0 : dDiffwork = 0 : dDiffqta = 0 : dordwork = 0 : dordqta = 0 : dordremqta = 0 : dordremwork = 0
            dordworkprg = 0 : dordqtaprg = 0 : dorddiffwork = 0 : dorddiffqta = 0
            If strPrevgrup = "S" Then  ' ricalcola
              dremcost = dBasecost - dactcost + dDiffcost
              dcost = dactcost + dremcost
              dordremcost = dBasecost - dordcost + dorddiffcost
              dordcostprg = dordcost + dordremcost
            End If
          Case 3
            dBasework = 0 : dactwork = 0 : dremwork = 0 : dwork = 0 : dDiffwork = 0 : dordwork = 0 : dordremwork = 0
            dordworkprg = 0 : dorddiffwork = 0
            If strPrevgrup = "S" Then  ' ricalcola
              dremcost = dBasecost - dactcost + dDiffcost
              dcost = dactcost + dremcost
              dordremcost = dBasecost - dordcost + dorddiffcost
              dordcostprg = dordcost + dordremcost
              dremqta = dBaseqta - dactqta + dDiffqta
              dQta = dactqta + dremqta
              dordremqta = dBaseqta - dordqta + dorddiffqta
              dordqtaprg = dordqta + dordremqta
            End If
          Case 4
            dBaseqta = 0 : dactqta = 0 : dremqta = 0 : dQta = 0 : dDiffqta = 0 : dordqta = 0 : dordremqta = 0
            dordqtaprg = 0 : dorddiffqta = 0
            If strPrevgrup = "S" Then  ' ricalcola
              dremcost = dBasecost - dactcost + dDiffcost
              dcost = dactcost + dremcost
              dordremcost = dBasecost - dordcost + dorddiffcost
              dordcostprg = dordcost + dordremcost
              dremwork = dBasework - dactwork + dDiffwork
              dwork = dactwork + dremwork
              dordremwork = dBasework - dordwork + dorddiffwork
              dordworkprg = dordwork + dordremwork
            End If
          Case 5
            If strPrevgrup = "S" Then  ' ricalcola
              dremcost = dBasecost - dactcost + dDiffcost
              dcost = dactcost + dremcost
              dordremcost = dBasecost - dordcost + dorddiffcost
              dordcostprg = dordcost + dordremcost
              dremqta = dBaseqta - dactqta + dDiffqta
              dQta = dactqta + dremqta
              dordremqta = dBaseqta - dordqta + dorddiffqta
              dordqtaprg = dordqta + dordremqta
              dremwork = dBasework - dactwork + dDiffwork
              dwork = dactwork + dremwork
              dordremwork = dBasework - dordwork + dorddiffwork
              dordworkprg = dordwork + dordremwork
            End If
        End Select
        If strPrevgrup <> "S" Then
          strA = "tsk_baseprzun = 0, tsk_basecostun = 0, tsk_perqta = 1, tsk_basecost = " & CDblSQL(dBasecost) & ", tsk_basework = " & CDblSQL(dBasework) & ", tsk_baseqta = " & CDblSQL(dBaseqta) & ","
        Else
          strA = ""
        End If
        If dcost = 0 Then
          dPerc = 0
        Else
          dPerc = ArrDbl((dactcost / dcost * 100), 2)
        End If
        If dordcostprg = 0 Then
          dPercord = 0
        Else
          dPercord = ArrDbl(dordcost / dordcostprg * 100, 2)
        End If
        '---------
        If dPerc >= dPercord Then
          bPrevalenzaconsuntivo = True
        Else
          bPrevalenzaconsuntivo = False
        End If
        If bPrevalenzaconsuntivo Then
          dTotalecostoeff = dactcost
          dTotalecostorim = dremcost
          dTotalecostoprog = dcost
          dTotalecostodiff = dDiffcost
          dTotalequantprog = dQta
          dTotalequantdiff = dDiffqta
          dTotalelavoroprog = dwork
          dTotalelavorodiff = dDiffwork
        Else
          dTotalecostoeff = dordcost
          dTotalecostorim = dordremcost
          dTotalecostoprog = dordcost
          dTotalecostodiff = dorddiffcost
          dTotalequantprog = dordqta
          dTotalequantdiff = dorddiffqta
          dTotalelavoroprog = dordwork
          dTotalelavorodiff = dorddiffwork
        End If
        '---------
        strIIF1 = "CASE WHEN tsk_gestcost = '1' THEN tsk_pctworkcomp ELSE " & CDblSQL(dPerc) & " END"
        strIIF2 = "CASE WHEN tsk_gestcost = '1' THEN 0 ELSE " & CDblSQL(dPercord) & " END"
        strC = "UPDATE " & strTTTASKS & _
          " SET " & strA & " tsk_actcost = " & CDblSQL(dactcost) & ", tsk_actwork = " & CDblSQL(dactwork) & "," & _
          " tsk_actqta = " & CDblSQL(dactqta) & ", tsk_remcost = " & CDblSQL(dremcost) & "," & _
          " tsk_remwork = " & CDblSQL(dremwork) & ", tsk_remqta = " & CDblSQL(dremqta) & "," & _
          " tsk_cost = " & CDblSQL(dcost) & ", tsk_work = " & CDblSQL(dwork) & "," & _
          " tsk_qta = " & CDblSQL(dQta) & ", tsk_diffcost = " & CDblSQL(dDiffcost) & "," & _
          " tsk_diffwork = " & CDblSQL(dDiffwork) & ", tsk_diffqta = " & CDblSQL(dDiffqta) & "," & _
          " tsk_ordcost = " & CDblSQL(dordcost) & ", tsk_ordwork = " & CDblSQL(dordwork) & "," & _
          " tsk_ordqta = " & CDblSQL(dordqta) & ", tsk_ordremcost = " & CDblSQL(dordremcost) & "," & _
          " tsk_ordremwork = " & CDblSQL(dordremwork) & ", tsk_ordremqta = " & CDblSQL(dordremqta) & "," & _
          " tsk_ordcostprg = " & CDblSQL(dordcostprg) & ", tsk_ordworkprg = " & CDblSQL(dordworkprg) & "," & _
          " tsk_ordqtaprg = " & CDblSQL(dordqtaprg) & ", tsk_orddiffcost = " & CDblSQL(dorddiffcost) & "," & _
          " tsk_orddiffwork = " & CDblSQL(dorddiffwork) & ", tsk_orddiffqta = " & CDblSQL(dorddiffqta) & "," & _
          " tsk_pctworkcomp = " & strIIF1 & ", tsk_pctordcomp = " & strIIF2 & "," & _
          " tsk_totactcost = " & CDblSQL(dTotalecostoeff) & ", tsk_totremcost = " & CDblSQL(dTotalecostorim) & "," & _
          " tsk_totcost = " & CDblSQL(dTotalecostoprog) & ", tsk_totdiffcost = " & CDblSQL(dTotalecostodiff) & "," & _
          " tsk_totqta = " & CDblSQL(dTotalequantprog) & ", tsk_totdiffqta = " & CDblSQL(dTotalequantdiff) & "," & _
          " tsk_totwork = " & CDblSQL(dTotalelavoroprog) & ", tsk_totdiffwork = " & CDblSQL(dTotalelavorodiff) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tsk_commeca = " & lComm & _
          " AND tsk_taskid = " & lIdTaskPadre & _
          IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString
        oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaEvasioniCapitolo(ByVal lIdTaskPadre As Integer, ByVal strFlevas As String, _
                                                       ByVal strOrdFlevas As String, ByVal lInstidTTTasks0 As Integer, _
                                                       ByVal lComm As Integer, ByVal lInstidTTprevent As Integer, _
                                                       ByVal bVisualizzaMessaggi As Boolean, ByVal strDitta As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      AggiornaEvasioniCapitolo(lIdTaskPadre, strFlevas, strOrdFlevas, lInstidTTTasks0, lComm, lInstidTTprevent, _
        bVisualizzaMessaggi, strDitta, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaEvasioniCapitolo(ByVal lIdTaskPadre As Integer, ByVal strFlevas As String, _
                                                       ByVal strOrdFlevas As String, ByVal lInstidTTTasks0 As Integer, _
                                                       ByVal lComm As Integer, ByVal lInstidTTprevent As Integer, _
                                                       ByVal bVisualizzaMessaggi As Boolean, ByVal strDitta As String, _
                                                       ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strC As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing
    Dim dsTmp3 As DataSet = Nothing
    Dim bCompletato2 As Boolean = False
    Dim bCompletato1 As Boolean = False
    Dim bCompletato As Boolean = False
    Dim bMessaggio2 As Boolean = False
    Dim bMessaggio1 As Boolean = False
    Dim bMessaggio As Boolean = False
    Dim bLocked2 As Boolean = False
    Dim bLocked1 As Boolean = False
    Dim bLocked As Boolean = False
    Dim bCompletatoord2 As Boolean = False
    Dim bCompletatoord1 As Boolean = False
    Dim bCompletatoord As Boolean = False
    Dim bMessaggioord2 As Boolean = False
    Dim bMessaggioord1 As Boolean = False
    Dim bMessaggioord As Boolean = False
    Dim bLockedord2 As Boolean = False
    Dim bLockedord1 As Boolean = False
    Dim bLockedord As Boolean = False
    Dim strTTTASKS As String = "tasks"
    Dim strTTPREVENT As String = "PREVENT"
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim k As Integer = 0

    Try
      '   CLN__STD.CheckInvokeCustomFunction(
      '--------------------------------------------------------------------------------------------------------------
      If lInstidTTTasks0 <> 0 Then strTTTASKS = "tttasks"
      If lInstidTTprevent <> 0 Then strTTPREVENT = "TTPREVENT"
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tsk_taskid, tsk_flevas , tsk_ordflevas" & _
        " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
        " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
        " AND tsk_commeca = " & lComm & _
        " AND tsk_uidpadre = " & lIdTaskPadre & _
        " AND pre_stato <> '5'" & _
        IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
        IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
      dsTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
      bCompletato2 = CBool(IIf(strFlevas = "S", True, False))
      bMessaggio2 = CBool(IIf(strFlevas = "Q", True, False))
      bCompletatoord2 = CBool(IIf(strOrdFlevas = "S", True, False))
      bMessaggioord2 = CBool(IIf(strOrdFlevas = "Q", True, False))
      If Not dsTmp.Tables("TASKS").Rows.Count = 0 Then
        bCompletato2 = False
        bMessaggio2 = False
        bLocked2 = False
        bCompletatoord2 = False
        bMessaggioord2 = False
        bLockedord2 = False
        For i = 0 To dsTmp.Tables("TASKS").Rows.Count - 1
          strSQL = "SELECT tsk_taskid, tsk_flevas, tsk_ordflevas " & _
            " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
            " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
            " AND tsk_commeca = " & lComm & _
            " AND tsk_uidpadre = " & NTSCInt(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
            " AND pre_stato <> '5'" & _
            IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
            IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
          dsTmp2 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
          bCompletato1 = CBool(IIf(NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_flevas) = "S", True, False))
          bMessaggio1 = CBool(IIf(NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_flevas) = "Q", True, False))
          bCompletatoord1 = CBool(IIf(NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_ordflevas) = "S", True, False))
          bMessaggioord1 = CBool(IIf(NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_ordflevas) = "Q", True, False))
          If Not dsTmp2.Tables("TASKS").Rows.Count = 0 Then
            bCompletato1 = False
            bMessaggio1 = False
            bLocked1 = False
            bCompletatoord1 = False
            bMessaggioord1 = False
            bLockedord1 = False
            For j = 0 To (dsTmp2.Tables("TASKS").Rows.Count - 1)
              strSQL = "SELECT tsk_flevas , tsk_ordflevas" & _
                " FROM " & strTTTASKS & " INNER JOIN " & strTTPREVENT & " ON " & strTTTASKS & ".codditt = " & strTTPREVENT & ".codditt AND " & strTTTASKS & ".tsk_commeca = " & strTTPREVENT & ".pre_commeca AND " & strTTTASKS & ".tsk_prevnum = " & strTTPREVENT & ".pre_prevnum AND " & strTTTASKS & ".tsk_prevrev = " & strTTPREVENT & ".pre_prevrev AND " & strTTTASKS & ".tsk_varid = " & strTTPREVENT & ".pre_varid" & _
                " WHERE " & strTTTASKS & ".codditt = " & CStrSQL(strDitta) & _
                " AND tsk_commeca = " & lComm & _
                " AND tsk_uidpadre = " & NTSCInt(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) & _
                " AND pre_stato <> '5'" & _
                IIf(lInstidTTTasks0 <> 0, " AND tttasks.instid = " & lInstidTTTasks0, "").ToString & _
                IIf(lInstidTTprevent <> 0, " AND ttprevent.instid = " & lInstidTTprevent, "").ToString
              dsTmp3 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASKS", Nothing, dbConn)
              bCompletato = CBool(IIf(NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_flevas) = "S", True, False))
              bMessaggio = CBool(IIf(NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_flevas) = "Q", True, False))
              bCompletatoord = CBool(IIf(NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_ordflevas) = "S", True, False))
              bMessaggioord = CBool(IIf(NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_ordflevas) = "Q", True, False))
              If Not dsTmp3.Tables("TASKS").Rows.Count = 0 Then
                bCompletato = False
                bMessaggio = False
                bLocked = False
                bCompletatoord = False
                bMessaggioord = False
                bLockedord = False
                For k = 0 To (dsTmp3.Tables("TASKS").Rows.Count - 1)
                  With dsTmp3.Tables("TASKS").Rows(k)
                    If NTSCStr(!tsk_flevas) = "C" Then
                      bCompletato = False
                      bMessaggio = False
                      bLocked = True
                    End If
                    If NTSCStr(!tsk_flevas) = "Q" Then
                      If Not bLocked Then bMessaggio = True
                    End If
                    If NTSCStr(!tsk_flevas) = "S" Then
                      If Not bLocked Then bCompletato = True
                    End If
                    '------
                    If NTSCStr(!tsk_ordflevas) = "C" Then
                      bCompletatoord = False
                      bMessaggioord = False
                      bLockedord = True
                    End If
                    If NTSCStr(!tsk_ordflevas) = "Q" Then
                      If Not bLockedord Then bMessaggioord = True
                    End If
                    If NTSCStr(!tsk_ordflevas) = "S" Then
                      If Not bLockedord Then bCompletatoord = True
                    End If
                  End With
                Next
                If Not bCompletatoord And bMessaggioord Then
                  If bVisualizzaMessaggi Then
                    'MsgBox("ATTENZIONE:" & vbCrLf & _
                    '       "Il Task di Riepilogo '" & CLng(snaTmp2!tsk_taskid) & "' risulta non essere EVASO a livello di ORDINE, pur avendo tutte le Sotto-Attività evase a livello di ordini.", vbExclamation, bsTtlMsg)
                  End If
                End If
                bMessaggioord = False
                If Not bCompletato And bMessaggio Then
                  If bVisualizzaMessaggi Then
                    'MsgBox("ATTENZIONE:" & vbCrLf & _
                    '       "Il Task di Riepilogo '" & CLng(snaTmp2!tsk_taskid) & "' risulta non essere EVASO, pur avendo tutte le Sotto-Attività evase.", vbExclamation, bsTtlMsg)
                  End If
                End If
                bMessaggio = False
                strC = "UPDATE " & strTTTASKS & _
                  " SET tsk_flevas = " & CStrSQL(IIf(bCompletato, "S", "C")) & "," & _
                  " tsk_ordflevas = " & CStrSQL(IIf(bCompletatoord, "S", "C")) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND tsk_commeca = " & lComm & _
                  " AND tsk_taskid = " & NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) & _
                  IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
                oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
                strC = "UPDATE " & strTTTASKS & _
                  " SET tsk_rilasciato = 'S'" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND tsk_commeca = " & lComm & _
                  " AND tsk_taskid = " & NTSCStr(dsTmp2.Tables("TASKS").Rows(j)!tsk_taskid) & _
                  " AND tsk_flevas = 'S'" & _
                  IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
                oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
              End If
              If bCompletato Then
                If Not bLocked1 Then bCompletato1 = True
              Else
                If bMessaggio Then
                  If Not bLocked1 Then bMessaggio1 = True
                Else
                  bCompletato1 = False
                  bMessaggio1 = False
                  bLocked1 = True
                End If
              End If
              If bCompletatoord Then
                If Not bLockedord1 Then bCompletatoord1 = True
              Else
                If bMessaggioord Then
                  If Not bLockedord1 Then bMessaggioord1 = True
                Else
                  bCompletatoord1 = False
                  bMessaggioord1 = False
                  bLockedord1 = True
                End If
              End If
            Next
            If Not bCompletatoord1 And bMessaggioord1 Then
              If bVisualizzaMessaggi Then
                'MsgBox("ATTENZIONE:" & vbCrLf & _
                '       "Il Task di Riepilogo '" & CLng(snaTmp!tsk_taskid) & "' risulta non essere EVASO a livello di ORDINE, pur avendo tutte le Sotto-Attività evase a livello di ordini.", vbExclamation, bsTtlMsg)
              End If
            End If
            bMessaggioord1 = False
            If Not bCompletato1 And bMessaggio1 Then
              If bVisualizzaMessaggi Then
                'MsgBox("ATTENZIONE:" & vbCrLf & _
                '       "Il Task di Riepilogo '" & CLng(snaTmp!tsk_taskid) & "' risulta non essere EVASO, pur avendo tutte le Sotto-Attività evase.", vbExclamation, bsTtlMsg)
              End If
            End If
            bMessaggio1 = False
            strC = "UPDATE " & strTTTASKS & _
              " SET tsk_flevas = " & CStrSQL(IIf(bCompletato1, "S", "C")) & "," & _
              " tsk_ordflevas = " & CStrSQL(IIf(bCompletatoord1, "S", "C")) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tsk_commeca = " & lComm & _
              " AND tsk_taskid = " & NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
              IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
            oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
            strC = "UPDATE " & strTTTASKS & _
              " SET tsk_rilasciato = 'S'" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tsk_commeca = " & lComm & _
              " AND tsk_taskid = " & NTSCStr(dsTmp.Tables("TASKS").Rows(i)!tsk_taskid) & _
              " AND tsk_flevas = 'S'" & _
              IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
            oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
          If bCompletato1 Then
            If Not bLocked2 Then bCompletato2 = True
          Else
            If bMessaggio1 Then
              If Not bLocked2 Then bMessaggio2 = True
            Else
              bCompletato2 = False
              bMessaggio2 = False
              bLocked2 = True
            End If
          End If
          If bCompletatoord1 Then
            If Not bLockedord2 Then bCompletatoord2 = True
          Else
            If bMessaggioord1 Then
              If Not bLockedord2 Then bMessaggioord2 = True
            Else
              bCompletatoord2 = False
              bMessaggioord2 = False
              bLockedord2 = True
            End If
          End If
        Next
        If Not bCompletatoord2 And bMessaggioord2 Then
          If bVisualizzaMessaggi Then
            'MsgBox("ATTENZIONE:" & vbCrLf & _
            '       "Il Task di Riepilogo '" & lIdTaskPadre & "' risulta non essere EVASO a livello di ORDINE, pur avendo tutte le Sotto-Attività evase a livello di ordini.", vbExclamation, bsTtlMsg)
          End If
        End If
        bMessaggioord2 = False
        If Not bCompletato2 And bMessaggio2 Then
          If bVisualizzaMessaggi Then
            'MsgBox("ATTENZIONE:" & vbCrLf & _
            '       "Il Task di Riepilogo '" & lIdTaskPadre & "' risulta non essere EVASO, pur avendo tutte le Sotto-Attività evase.", vbExclamation, bsTtlMsg)
          End If
        End If
        bMessaggio2 = False
        strC = "UPDATE " & strTTTASKS & _
          " SET tsk_flevas = " & CStrSQL(IIf(bCompletato2, "S", "C")) & "," & _
          " tsk_ordflevas = " & CStrSQL(IIf(bCompletatoord2, "S", "C")) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tsk_commeca = " & lComm & _
          " AND tsk_taskid = " & lIdTaskPadre & _
          IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
        oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
        strC = "UPDATE " & strTTTASKS & _
          " SET tsk_rilasciato = 'S'" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tsk_commeca = " & lComm & _
          " AND tsk_taskid = " & lIdTaskPadre & _
          " AND tsk_flevas = 'S'" & _
          IIf(lInstidTTTasks0 <> 0, " AND instid = " & lInstidTTTasks0, "").ToString
        oCldBase.Execute(strC, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "Net@Pro"
  'funzioni contenute in NPAccess.dll per avviare al volo su net@pro l'esecuzione di import/export
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPConnect(ByVal szHost As String, ByVal lPort As Integer) As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPGetError(ByRef lErr As Integer) As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPSetLog(ByRef strFile As String) As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPLogOn(ByVal strUser As String, ByVal strPwd As String) As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPSend(ByVal trxCode As String, ByVal params As String) As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPLogOff() As Integer
  End Function
  <DllImport("NPAccess.dll")> _
  Private Shared Function NPClose() As Integer
  End Function

  Public Overridable Function SalvaOrdineAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef strErr As String, _
                                                 ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strCodBatch As String = DateTime.Now.ToFileTimeUtc.ToString
    Dim strBusID As String = ""
    Dim nCodcent As Integer = 0
    Dim strStatusRow As String = ""    'REL se l'ordine è ancora parzialmente da evadere, else COM
    Dim strOperaz As String = "INS"    'INS per nuovi documenti o evasione ordini, UPD per update
    Dim bOrd As Boolean = False        'TRUE se vengo da BNORGSOR, else da BNVEBOLL

    Dim dttOrd As New DataTable
    Dim dttOrdImp As New DataTable
    Dim dttOrdAtt As New DataTable
    Dim dttOrdAss As New DataTable
    Try
      If strNetProDB = "" Then Return True

      Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString
        'tipirk non gestiti dal mes
        Case "X", "Q", "#", "V", "$" : Return True
        Case "R", "O", "H" : bOrd = True
      End Select

      '----------------------------------
      'inserisco le righe di ordini: se erano documenti già precedentemente passati li marco come update, else insert
      'quelle con qta ancora da evadere saranno in stato di REL, quelle totalmente evase in stato di COM
      If bOrd Then
        dttOrd = ds.Tables("CORPO")
        dttOrdImp = ds.Tables("CORPOIMP")
        dttOrdAtt = ds.Tables("ATTIVIT")
        dttOrdAss = ds.Tables("ASSRIS")
      Else
        'al salvataggio di un documento di magazzino devo aggiornare la situazione degli ordini 
        'dopo aver salvato/cancellato movmag ed aver lanciato la stored procedure che aggiorna movord
        If Not SalvaOrdineAggNetPro_CaricaOrDaMovmag(ds, dbConn, dttOrd, dttOrdImp, dttOrdAtt, dttOrdAss) Then Return False
        If dttOrd.Rows.Count = 0 Then Return True
      End If


      For Each dtrT As DataRow In dttOrd.Rows

        strBusID = NTSCStr(dtrT!ec_tipork.ToString & dtrT!ec_anno.ToString.Substring(2) & _
                            dtrT!ec_serie.ToString & NTSCInt(dtrT!ec_numdoc).ToString("000000000") & _
                            NTSCInt(dtrT!ec_riga).ToString("0000"))

        strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxorders (fbatch, forder, faction, fdescr, fpnumb, fextorder, " & _
                 " fstorage, fcusord, fcntpart, fstatus, ftype, frqqty, fdoneqty, fcredate, frqduedate, fplduedate, finsdate, " & _
                 " fupddate, fupduser, fplstdate, frqstdate)" & _
                 " VALUES (" & CStrSQL(strCodBatch) & ", "
        If NTSCStr(dtrT!ec_netpid) <> "" Then
          'ordine già precedentemente passato (o generato da netpro): passo il suo progr per update
          strSQL += CStrSQL(dtrT!ec_netpid) & ", "
          strOperaz = "'UPD'"
        Else
          'ordine inserito ora da bus: passo un id univoco da utilizzare successivamente per le update
          dtrT!ec_netpid = strBusID
          dtrT!ec_netpstatus = "REL"
          dtrT.AcceptChanges()
          strSQL += CStrSQL(dtrT!ec_netpid) & ", "
          strOperaz = "'INS'"
        End If
        If NTSCStr(dtrT!ec_flevas) = "S" Then
          strStatusRow = "'COM'"
          strOperaz = "'DEL'"
        Else
          strStatusRow = "'REL'"
        End If
        strSQL += strOperaz & ", "
        strSQL += CStrSQL(dtrT!ec_descr) & IIf(NTSCInt(dtrT!ec_fase) = 0, "", "/" & NTSCStr(dtrT!xxo_fase).ToString).ToString & ", " & _
                  CStrSQL(dtrT!ec_codart) & IIf(NTSCInt(dtrT!ec_fase) = 0, "", "." & NTSCInt(dtrT!ec_fase).ToString).ToString & ", " & _
                  CStrSQL(strBusID) & ", " & dtrT!ec_magaz.ToString & ", " & dtrT!ec_commeca.ToString & ", " & _
                  ds.Tables("TESTA").Rows(0)!et_conto.ToString & ", " & strStatusRow & ", "
        Select Case dtrT!ec_tipork.ToString
          Case "R" : strSQL += "'SELL',"
          Case "H", "Y" : strSQL += "'MAKE',"
          Case "O" : strSQL += "'BUY',"
        End Select
        strSQL += CDblSQL(NTSCDec(dtrT!ec_quant)) & ", " & CDblSQL(NTSCDec(dtrT!ec_quaeva)) & ", "
        'If bOrd Then
        '  strSQL += CDataSQL(NTSCDate(ds.Tables("TESTA").Rows(0)!et_datdoc)) & ", "
        'Else
        '  strSQL += CDataSQL(NTSCDate(dtrT!et_datdoc)) & ", "
        'End If
        strSQL += CDataSQL(NTSCDate(dtrT!ec_dtrichini)) & ", "  'nuovo campo
        strSQL += CDataSQL(NTSCDate(dtrT!ec_datcons)) & ", " & CDataSQL(NTSCDate(dtrT!ec_datcons)) & ", " & _
                  CDataSQL(NTSCDate(ds.Tables("TESTA").Rows(0)!et_ultagg)) & ", " & _
                  CDataSQL(NTSCDate(ds.Tables("TESTA").Rows(0)!et_ultagg)) & ", " & _
                  CStrSQL(oApp.User.Nome) & ", " & CDataOraSQL(NTSCDate(dtrT!ec_dtpianini)) & ", " & _
                  CDataOraSQL(NTSCDate(dtrT!ec_dtrichini)) & ")"

        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


        '----------------------------------
        'ora devo passare le lavorazioni e i materiali impegnati
        If dtrT!ec_tipork.ToString <> "H" Then
          'per gli ordini forn e imp cli devo comunque inserire/cancellare un record in fxdemands con articolo e qta = a record di fxorders
          strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxdemands (fbatch, forder, faction, foperatn, fpnumb, " & _
                    " fstorage, fsequence, frqqty, fusedqty)" & _
                    " VALUES (" & CStrSQL(strCodBatch) & ", "
          strSQL += CStrSQL(dtrT!ec_netpid) & ", " & strOperaz & ", "
          strSQL += "0, " & CStrSQL(dtrT!ec_codart) & IIf(NTSCInt(dtrT!ec_fase) = 0, "", "." & NTSCInt(dtrT!ec_fase).ToString).ToString & ", " & _
                    NTSCInt(dtrT!ec_magaz).ToString & ", " & NTSCInt(dtrT!ec_riga).ToString & ", " & CDblSQL(NTSCDec(dtrT!ec_quant)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!ec_quaeva)) & ")"

          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        Else
          '---- LAVORAZIONI ----
          For Each dtrTL As DataRow In dttOrdAtt.Select(" at_tipork = " & CStrSQL(dtrT!ec_tipork) & _
                                                        " AND at_anno = " & dtrT!ec_anno.ToString & _
                                                        " AND at_serie = " & CStrSQL(dtrT!ec_serie) & _
                                                        " AND at_numord = " & dtrT!ec_numdoc.ToString & _
                                                        " AND at_riga = " & dtrT!ec_riga.ToString)

            strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxjobs (fbatch, forder, faction, foperatn, fdescr, " & _
                      " fasswrkcnt, frqqty, fdoneqty, fmacsettime, fmacwrktime, fplstdate, fplduedate)" & _
                      " VALUES (" & CStrSQL(strCodBatch) & ", "
            If NTSCStr(dtrTL!at_netpid) <> "" Then
              'ordine già precedentemente passato (o generato da netpro): passo il suo progr per update
              strSQL += CStrSQL(dtrTL!at_netpid) & ", " & strOperaz & ", "
            Else
              'ordine inserito ora da bus: passo un id univoco da utilizzare successivamente per le update
              If bOrd = False Then GoTo SALTA1
              dtrTL!at_netpid = strBusID
              dtrTL.AcceptChanges()
              strSQL += CStrSQL(dtrTL!at_netpid) & ", " & strOperaz & ", "
            End If
            nCodcent = 0
            Dim dtrTC() As DataRow = dttOrdAss.Select(" as_tipork = " & CStrSQL(dtrT!ec_tipork) & _
                                                      " AND as_anno = " & dtrT!ec_anno.ToString & _
                                                      " AND as_serie = " & CStrSQL(dtrT!ec_serie) & _
                                                      " AND as_numord = " & dtrT!ec_numdoc.ToString & _
                                                      " AND as_riga = " & dtrTL!at_riga.ToString & _
                                                      " AND as_fase = " & dtrTL!at_fase.ToString)
            If dtrTC.Length > 0 Then nCodcent = NTSCInt(dtrTC(0)!as_codcent)
            strSQL += dtrTL!at_fase.ToString & ", " & CStrSQL(dtrTL!xx_codlavo) & ", " & _
                      nCodcent.ToString & ", " & CDblSQL(NTSCDec(dtrTL!at_qtapr)) & ", " & _
                      CDblSQL(NTSCDec(dtrTL!at_qtaes)) & ", " & _
                      CDblSQL(NTSCDec(dtrTL!at_tempattpr)) & ", " & CDblSQL(NTSCDec(dtrTL!at_tempesepr)) & ", " & _
                      CDataOraSQL(NTSCDate(dtrTL!at_dtpianini)) & ", " & CDataOraSQL(NTSCDate(dtrTL!at_dtrichini)) & ")"

            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
SALTA1:
          Next    'For Each dtrT As DataRow In ds.Tables("ATTIVIT").Rows

          '---- MATERIALI CONSUMATI ----
          For Each dtrTM As DataRow In dttOrdImp.Select("ec_tiporkor = " & CStrSQL(dtrT!ec_tipork) & _
                                                        " AND ec_annoor = " & dtrT!ec_anno.ToString & _
                                                        " AND ec_serieor = " & CStrSQL(dtrT!ec_serie) & _
                                                        " AND ec_numordor = " & dtrT!ec_numdoc.ToString & _
                                                        " AND ec_rigaor = " & dtrT!ec_riga.ToString)

            strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxdemands (fbatch, forder, faction, foperatn, fpnumb, " & _
                      " fstorage, fsequence, frqqty, fusedqty, fplduedate)" & _
                      " VALUES (" & CStrSQL(strCodBatch) & ", "
            If NTSCStr(dtrTM!ec_netpid) <> "" Then
              'ordine già precedentemente passato (o generato da netpro): passo il suo progr per update
              strSQL += CStrSQL(dtrTM!ec_netpid) & ", " & strOperaz & ", "
            Else
              'ordine inserito ora da bus: passo un id univoco da utilizzare successivamente per le update
              dtrTM!ec_netpid = strBusID
              dtrTM.AcceptChanges()
              strSQL += CStrSQL(dtrTM!ec_netpid) & ", " & strOperaz & ", "
            End If
            strSQL += "0, " & CStrSQL(dtrTM!ec_codart) & IIf(NTSCInt(dtrTM!ec_fase) = 0, "", "." & NTSCInt(dtrTM!ec_fase).ToString).ToString & ", " & _
                      NTSCInt(dtrTM!ec_magaz).ToString & ", " & NTSCInt(dtrTM!ec_riga).ToString & ", " & CDblSQL(NTSCDec(dtrTM!ec_quant)) & ", " & _
                      CDblSQL(NTSCDec(dtrTM!ec_quaeva)) & ", " & CDataOraSQL(NTSCDate(dtrTM!ec_dtpianfin)) & ")"

            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next    'For Each dtrT As DataRow In ds.Tables("CORPOIMP").Rows

        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then

      Next    'For Each dtrT As DataRow In ds.Tables("CORPO").Rows


      '----------------------------------
      'creo il file semaforo
      strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxbatches (fbatch, faction, ftype, fstatus) " & _
               "VALUES (" & CStrSQL(strCodBatch) & ", 'ALGN', 'ORD', 'REL')"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiamo la socket per fare in modo che Net@Pro esegua subito l'import
      NetProCallSocket(NTSCStr(ds.Tables("TESTA").Rows(0)!codditt), "ORD", strCodBatch)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function SalvaOrdineAggNetPro_CaricaOrDaMovmag(ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                                                    ByRef dttOrd As DataTable, ByRef dttOrdImp As DataTable, _
                                                                    ByRef dttOrdAtt As DataTable, ByRef dttOrdAss As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '---------------
      'CORPO
      strSQL = "SELECT DISTINCT mo_tipork as ec_tipork, mo_anno as ec_anno, mo_serie as ec_serie, mo_descr as ec_descr, " & _
                   " mo_numord as ec_numdoc, mo_riga as ec_riga, mo_netpid as ec_netpid, mo_flevas as ec_flevas, " & _
                   " mo_netpstatus as ec_netpstatus, mo_codart as ec_codart, mo_fase as ec_fase, " & _
                   " mo_commeca as ec_commeca, mo_magaz as ec_magaz, mo_quant as ec_quant, mo_quaeva as ec_quaeva, " & _
                   " mo_datcons as ec_datcons, td_datord as et_datdoc, af_descr as xxo_fase, " & _
                   " mo_dtpianini as ec_dtpianini, mo_dtpianfin as ec_dtpianfin, mo_dtrichini as ec_dtrichini, mo_dtrichfin as ec_dtrichfin " & _
                   " FROM " & oCldBase.strJoinTestordMovord & _
                   " LEFT JOIN movmag ON movmag.codditt = movord.codditt AND movmag.mm_ortipo = movord.mo_tipork" & _
                   " AND movmag.mm_oranno = movord.mo_anno AND movmag.mm_orserie = movord.mo_serie " & _
                   " AND movmag.mm_ornum = movord.mo_numord AND movmag.mm_orriga = movord.mo_riga " & _
                   " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_codart = artfasi.af_codart AND movord.mo_fase = artfasi.af_fase " & _
                   " WHERE movord.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                   " AND mo_netpid <> ''"
      If ds.Tables.Contains("CORPO_DEL") Then
        'sono in cancellazione docum. di magaz ed ho l'elenco degli ordini che erano evasi dal doc. in cancellazione
        If ds.Tables("CORPO_DEL").Rows.Count > 0 Then
          strSQL += " AND ("
          For Each dtrT As DataRow In ds.Tables("CORPO_DEL").Rows
            strSQL += " (mo_tipork = " & CStrSQL(dtrT!mm_ortipo) & _
                     " AND mo_anno = " & dtrT!mm_oranno.ToString & _
                     " AND mo_serie = " & CStrSQL(dtrT!mm_orserie) & _
                     " AND mo_numord = " & dtrT!mm_ornum.ToString & _
                     " AND mo_riga = " & dtrT!mm_orriga.ToString & ") OR "
          Next
          If ds.Tables("CORPO_DEL").Rows.Count > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 3)
          strSQL += ")"
        Else
          strSQL += " AND movord.codditt = '.k'" 'non deve trovare niente, visto che nel documento non sono state cancellate righe
        End If
      Else
        'sono in salva di un doc che evade degli ordini
        'devo leggere i dati direttamente da movord appena aggiornato con la perte evasa da movmag...

        With ds.Tables("TESTA").Rows(0)
          strSQL += " AND mm_tipork = " & CStrSQL(!et_tipork) & _
                   " AND mm_anno = " & !et_anno.ToString & _
                   " AND mm_serie = " & CStrSQL(!et_serie) & _
                   " AND mm_numdoc = " & !et_numdoc.ToString
        End With
      End If    'If ds.Tables.Contains("CORPO_DEL") Then

      dttOrd = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttOrd.Rows.Count = 0 Then Return True

      If dttOrd.Rows(0)!ec_tipork.ToString <> "H" Then Return True


      '---------------
      'CORPOIMP
      strSQL = "SELECT DISTINCT mo_tipork as ec_tipork, mo_anno as ec_anno, mo_serie as ec_serie, mo_descr as ec_descr, " & _
              " mo_numord as ec_numdoc, mo_riga as ec_riga, mo_netpid as ec_netpid, mo_flevas as ec_flevas, " & _
              " mo_netpstatus as ec_netpstatus, mo_codart as ec_codart, mo_fase as ec_fase, " & _
              " mo_commeca as ec_commeca, mo_magaz as ec_magaz, mo_quant as ec_quant, mo_quaeva as ec_quaeva, " & _
              " mo_datcons as ec_datcons, td_datord as et_datdoc, " & _
              " mo_tiporkor as ec_tiporkor, mo_annoor as ec_annoor, mo_serieor as ec_serieor, " & _
              " mo_numordor as ec_numordor, mo_rigaor as ec_rigaor, af_descr as xxo_fase, " & _
              " mo_dtpianini as ec_dtpianini, mo_dtpianfin as ec_dtpianfin, mo_dtrichini as ec_dtrichini, mo_dtrichfin as ec_dtrichfin " & _
              " FROM " & oCldBase.strJoinTestordMovord & _
              " LEFT JOIN movmag ON movmag.codditt = movord.codditt AND movmag.mm_ortipo = movord.mo_tipork" & _
              " AND movmag.mm_oranno = movord.mo_anno AND movmag.mm_orserie = movord.mo_serie " & _
              " AND movmag.mm_ornum = movord.mo_numord AND movmag.mm_orriga = movord.mo_riga " & _
              " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_codart = artfasi.af_codart AND movord.mo_fase = artfasi.af_fase " & _
              " WHERE movord.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
              " AND mo_netpid <> ''"
      If ds.Tables.Contains("CORPO_DEL") AndAlso ds.Tables("CORPO_DEL").Rows.Count > 0 Then
        'sono in cancellazione docum. di magaz ed ho l'elenco degli ordini che erano evasi dal doc. in cancellazione
        If ds.Tables("CORPO_DEL").Rows.Count > 0 Then
          strSQL += " AND ("
          For Each dtrT As DataRow In ds.Tables("CORPO_DEL").Rows
            strSQL += " (mo_tiporkor = 'Y'" & _
                     " AND mo_annoor = " & dtrT!mm_oranno.ToString & _
                     " AND mo_serieor = " & CStrSQL(dtrT!mm_orserie) & _
                     " AND mo_numordor = " & dtrT!mm_ornum.ToString & _
                     " AND mo_rigaor = " & dtrT!mm_orriga.ToString & ") OR "
          Next
          If ds.Tables("CORPO_DEL").Rows.Count > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 3)
          strSQL += ")"
        Else
          strSQL += " AND movord.codditt = '.k'" 'non deve trovare niente, visto che nel documento non sono state cancellate righe
        End If
      Else
        'sono in salva di un doc che evade degli ordini
        'devo leggere i dati direttamente da movord appena aggiornato con la perte evasa da movmag...
        With ds.Tables("TESTA").Rows(0)
          strSQL += " AND mm_tipork = 'U'" & _
                   " AND mm_anno = " & !et_anno.ToString & _
                   " AND mm_serie = " & CStrSQL(!et_serie) & _
                   " AND mm_numdoc = " & !et_numdoc.ToString
        End With
      End If    'If ds.Tables.Contains("CORPO_DEL") Then

      dttOrdImp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------
      'ATTIVIT
      strSQL = "SELECT DISTINCT at_tipork, at_anno, at_serie, at_numord, at_riga, at_fase, at_netpid, " & _
              " at_qtapr, at_qtaes, at_tempattpr, at_tempesepr, tb_deslavo as xx_codlavo, " & _
              " at_dtpianini, at_dtpianfin, at_dtrichini, at_dtrichfin " & _
              " FROM attivit LEFT JOIN tablavo ON attivit.codditt = tablavo.codditt AND attivit.at_codlavo = tablavo.tb_codlavo " & _
              " LEFT JOIN movmag ON movmag.codditt = attivit.codditt AND movmag.mm_ortipo = attivit.at_tipork" & _
              " AND movmag.mm_oranno = attivit.at_anno AND movmag.mm_orserie = attivit.at_serie " & _
              " AND movmag.mm_ornum = attivit.at_numord AND movmag.mm_orriga = attivit.at_riga " & _
              " WHERE attivit.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
              " AND at_netpid <> ''"
      If ds.Tables.Contains("CORPO_DEL") AndAlso ds.Tables("CORPO_DEL").Rows.Count > 0 Then
        'sono in cancellazione docum. di magaz ed ho l'elenco degli ordini che erano evasi dal doc. in cancellazione
        If ds.Tables("CORPO_DEL").Rows.Count > 0 Then
          strSQL += " AND ("
          For Each dtrT As DataRow In ds.Tables("CORPO_DEL").Rows
            strSQL += " (at_tipork = 'H'" & _
                     " AND at_anno = " & dtrT!mm_oranno.ToString & _
                     " AND at_serie = " & CStrSQL(dtrT!mm_orserie) & _
                     " AND at_numord = " & dtrT!mm_ornum.ToString & _
                     " AND at_riga = " & dtrT!mm_orriga.ToString & ") OR "
          Next
          If ds.Tables("CORPO_DEL").Rows.Count > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 3)
          strSQL += ")"
        Else
          strSQL += " AND attivit.codditt = '.k'" 'non deve trovare niente, visto che nel documento non sono state cancellate righe
        End If
      Else
        'sono in salva di un doc che evade degli ordini
        'devo leggere i dati direttamente da movord appena aggiornato con la perte evasa da movmag...
        With ds.Tables("TESTA").Rows(0)
          strSQL += " AND mm_tipork = 'T'" & _
                   " AND mm_anno = " & !et_anno.ToString & _
                   " AND mm_serie = " & CStrSQL(!et_serie) & _
                   " AND mm_numdoc = " & !et_numdoc.ToString
        End With
      End If    'If ds.Tables.Contains("CORPO_DEL") Then

      dttOrdAtt = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------
      'ASSRIS
      strSQL = "SELECT DISTINCT as_tipork, as_anno, as_serie, as_numord, as_riga, as_fase, as_codcent " & _
              " FROM assris " & _
              " LEFT JOIN movmag ON movmag.codditt = assris.codditt AND movmag.mm_ortipo = assris.as_tipork" & _
              " AND movmag.mm_oranno = assris.as_anno AND movmag.mm_orserie = assris.as_serie " & _
              " AND movmag.mm_ornum = assris.as_numord AND movmag.mm_orriga = assris.as_riga " & _
              " WHERE assris.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt)
      If ds.Tables.Contains("CORPO_DEL") AndAlso ds.Tables("CORPO_DEL").Rows.Count > 0 Then
        'sono in cancellazione docum. di magaz ed ho l'elenco degli ordini che erano evasi dal doc. in cancellazione
        If ds.Tables("CORPO_DEL").Rows.Count > 0 Then
          strSQL += " AND ("
          For Each dtrT As DataRow In ds.Tables("CORPO_DEL").Rows
            strSQL += " (as_tipork = 'H'" & _
                     " AND as_anno = " & dtrT!mm_oranno.ToString & _
                     " AND as_serie = " & CStrSQL(dtrT!mm_orserie) & _
                     " AND as_numord = " & dtrT!mm_ornum.ToString & _
                     " AND as_riga = " & dtrT!mm_orriga.ToString & ") OR "
          Next
          If ds.Tables("CORPO_DEL").Rows.Count > 0 Then strSQL = strSQL.Substring(0, strSQL.Length - 3)
          strSQL += ")"
        Else
          strSQL += " AND assris.codditt = '.k'" 'non deve trovare niente, visto che nel documento non sono state cancellate righe
        End If
      Else
        'sono in salva di un doc che evade degli ordini
        'devo leggere i dati direttamente da movord appena aggiornato con la perte evasa da movmag...
        With ds.Tables("TESTA").Rows(0)
          strSQL += " AND mm_tipork = 'T'" & _
                   " AND mm_anno = " & !et_anno.ToString & _
                   " AND mm_serie = " & CStrSQL(!et_serie) & _
                   " AND mm_numdoc = " & !et_numdoc.ToString
        End With
      End If    'If ds.Tables.Contains("CORPO_DEL") Then

      dttOrdAss = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteDocAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef dbConn As DbConnection) As Boolean
    'per tutte le righe di ordine da cancellare aggiorno, se presenti, lo status in fxorders marcandolo come cancellato
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim strTmp As String = ""
    Dim strCodBatch As String = DateTime.Now.ToFileTimeUtc.ToString

    Try
      If strNetProDB = "" Then Return True
      Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString
        'tipirk non gestiti dal mes
        Case "X", "Q", "#", "V", "$" : Return True
      End Select

      'devo ottenere l'elenco delle righe di movord non più presenti nel documento 
      'per avvisare di cancellarle anche sul MES
      If strState <> "D" Then
        For Each dtrT As DataRow In ds.Tables("CORPO").Select("ec_flevas = 'C'")
          strTmp += dtrT!ec_riga.ToString & ","
        Next
        If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
      End If

      With ds.Tables("TESTA").Rows(0)
        strSQL = "SELECT mo_netpid, mo_codart, mo_fase, mo_riga " & _
                 " FROM movord " & _
                 " WHERE codditt = " & CStrSQL(!codditt) & _
                 " AND mo_tipork = " & CStrSQL(!et_tipork) & _
                 " AND mo_anno = " & !et_anno.ToString & _
                 " AND mo_serie = " & CStrSQL(!et_serie) & _
                 " AND mo_numord = " & !et_numdoc.ToString & _
                 " AND mo_netpid <> ''"
        If strTmp.Length > 0 Then strSQL += " AND mo_riga NOT IN (" & strTmp & ")"
      End With
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttTmp.Rows
        strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxorders (fbatch, forder, faction)" & _
                 " VALUES (" & CStrSQL(strCodBatch) & ", "
        strSQL += CStrSQL(dtrT!mo_netpid) & ", 'DEL') "

        oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "H" Then
          'per gli ordini forn e imp cli devo comunque inserire/cancellare un record in fxdemands con articolo e qta = a record di fxorders
          strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxdemands (fbatch, forder, foperatn, fpnumb, fsequence, faction)" & _
                               " VALUES (" & CStrSQL(strCodBatch) & ", "
          strSQL += CStrSQL(dtrT!mo_netpid) & ", 0, " & _
                    CStrSQL(dtrT!mo_codart) & IIf(NTSCInt(dtrT!mo_fase) = 0, "", "." & NTSCInt(dtrT!mo_fase).ToString).ToString & ", " & _
                    NTSCInt(dtrT!mo_riga).ToString & ", 'DEL') "

          oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else

          'ora devo cancellare i materiali
          With ds.Tables("TESTA").Rows(0)
            strSQL = "SELECT mo_riga, mo_netpid, mo_codart, mo_fase " & _
                     " FROM movord " & _
                     " WHERE codditt = " & CStrSQL(!codditt) & _
                     " AND mo_tipork = 'Y'" & _
                     " AND mo_anno = " & !et_anno.ToString & _
                     " AND mo_serie = " & CStrSQL(!et_serie) & _
                     " AND mo_numord = " & !et_numdoc.ToString & _
                     " AND mo_netpid <> ''"
            If strTmp.Length > 0 Then strSQL += " AND mo_rigaor NOT IN (" & strTmp & ")"
          End With

          dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          For Each dtrTM As DataRow In dttTmp1.Rows
            strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxdemands (fbatch, forder, foperatn, fpnumb, fsequence, faction)" & _
                     " VALUES (" & CStrSQL(strCodBatch) & ", "
            strSQL += CStrSQL(dtrTM!mo_netpid) & ", 0, " & _
                      CStrSQL(dtrTM!mo_codart) & IIf(NTSCInt(dtrTM!mo_fase) = 0, "", "." & NTSCInt(dtrTM!mo_fase).ToString).ToString & ", " & _
                      NTSCInt(dtrTM!mo_riga).ToString & ", 'DEL') "

            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next

          'ora devo cancellare le lavorazioni
          With ds.Tables("TESTA").Rows(0)
            strSQL = "SELECT at_netpid, at_fase " & _
                     " FROM attivit " & _
                     " WHERE codditt = " & CStrSQL(!codditt) & _
                     " AND at_tipork = " & CStrSQL(!et_tipork) & _
                     " AND at_anno = " & !et_anno.ToString & _
                     " AND at_serie = " & CStrSQL(!et_serie) & _
                     " AND at_numord = " & !et_numdoc.ToString & _
                     " AND at_netpid <> ''"
            If strTmp.Length > 0 Then strSQL += " AND at_riga NOT IN (" & strTmp & ")"
          End With

          dttTmp1 = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          For Each dtrTL As DataRow In dttTmp1.Rows
            strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxjobs (fbatch, forder, foperatn, faction)" & _
                     " VALUES (" & CStrSQL(strCodBatch) & ", "
            strSQL += CStrSQL(dtrTL!at_netpid) & ", " & dtrTL!at_fase.ToString & ", 'DEL') "

            oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next

        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then

      Next    'For Each dtrT As DataRow In dttTmp.Rows


      '----------------------------------
      'creo il file semaforo
      strSQL = "INSERT INTO " & strNetProDB & ".DBO.fxbatches (fbatch, faction, ftype, fstatus) " & _
               "VALUES (" & CStrSQL(strCodBatch) & ", 'ALGN', 'ORD', 'REL')"
      oCldBase.Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiamo la socket per fare in modo che Net@Pro esegua subito l'import
      NetProCallSocket(NTSCStr(ds.Tables("TESTA").Rows(0)!codditt), "ORD", strCodBatch)

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

  Public Overridable Function GetStatusEXEOrdineNetpro(ByVal strDitta As String, ByVal strTipork As String, _
                                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                                      ByVal lNumord As Integer, ByVal nRiga As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT mo_netpid, mo_netpstatus FROM movord " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipork) & _
               " AND mo_anno = " & nAnno & _
               " AND mo_serie = " & CStrSQL(strSerie) & _
               " AND mo_numord = " & lNumord & _
               " AND mo_riga = " & nRiga & _
               " AND mo_netpstatus = 'EXE'"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function NetProCallSocket(ByVal strDitta As String, ByVal strOperaz As String, ByVal strBatch As String) As Boolean
    Dim nRes As Integer = 0
    Dim lErr As Integer = 0
    Dim strMesUser As String = "BUSINESS"
    Dim strMesPwd As String = "NTS"
    Dim strMesIP As String = ""
    Dim strMesPort As String = "2157"       'è configurata in fdcs.ini sotto la proprietà CLIENTPORT=
    Try
      '----------------------------------
      'avviso tramite socket a Net@Pro di eseguire l'import immediatamente
      'per operare correttamente è necessario che la dll NPAccess.dll sia presente nella dir dele dll di Busnet

      strMesIP = GetSettingBus("Opzioni", ".", ".", "MESSOCKET_" & strDitta, "", " ", "").Trim
      If strMesIP = "" Then Return True
      strMesPort = strMesIP.Split(":"c)(1)
      strMesIP = strMesIP.Split(":"c)(0)

      strMesUser = GetSettingBus("Opzioni", ".", ".", "MESUSER_" & strDitta, "BUSINESS", " ", "BUSINESS").Trim
      strMesPwd = GetSettingBus("Opzioni", ".", ".", "MESPWD_" & strDitta, "NTS", " ", "NTS").Trim
      If strMesUser = "" Then Return True


      nRes = NPConnect(strMesIP, NTSCInt(strMesPort))
      'nRes = NPGetError(lErr)
      If nRes <> 0 Then Return True 'se non ho indicato le credenzili corrette, esco e non avviso che dovrà essere eseguito l'import

      'nRes = NPSetLog("c:\a.log")

      nRes = NPLogOn(strMesUser, strMesPwd)
      'nRes = NPGetError(lErr)
      If nRes <> 0 Then Return True 'se non ho indicato le credenzili corrette, esco e non avviso che dovrà essere eseguito l'import

      'invio di una richiesta asincrona (quindi non aspetto che netpro abbia eseguito l'import)
      If strOperaz = "ORD" Then
        'specifico per l'aggiornamenot degli ordini/proposte d'ordine
        nRes = NPSend("IMPORD", "#BATCH=" & strBatch)
      Else
        'generico per tutte le tabelle
        nRes = NPSend("IMPORT", "#BATCH=" & strBatch)
      End If

      NPLogOff()
      NPClose()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Dichiarazioni di intento"

  Public Overridable Function IsSoggettoconDichiarazioni(ByVal strDittaCorrente As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Try
      strSQL = " SELECT an_numdic, an_numdicp" & _
               " FROM anagra" & _
               " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
               " AND an_conto = " & lConto
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      Else
        If (dttOut.Rows(0)!an_numdic.Equals(DBNull.Value) Or dttOut.Rows(0)!an_numdic.ToString.Trim = "") And _
           (dttOut.Rows(0)!an_numdicp.Equals(DBNull.Value) Or dttOut.Rows(0)!an_numdicp.ToString.Trim = "") Then
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDichint(ByVal strDitta As String, ByVal lConto As Integer, ByVal strTipo As String, ByVal bChiuse As Boolean, ByVal bRevocate As Boolean, ByVal bValide As Boolean, ByVal strDatVal As String, ByRef dsHldi As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      'DICHINT
      strSQL = " SELECT dichint.*, anagra.an_descr1 as xx_descr1 " & _
               " FROM dichint INNER JOIN anagra ON dichint.codditt = anagra.codditt AND dichint.di_conto = anagra.an_conto" & _
               " WHERE dichint.codditt = " & CStrSQL(strDitta)
      'If lConto = 0 Then
      'strSQL += " AND an_tpsogiva = 'D'"
      'Else
      strSQL += " AND di_conto = " & lConto.ToString
      'End If
      If bValide Then
        'Ci potrebbero essere anche le  revocate...
        strSQL += " AND di_datainval <= " & CDataSQL(strDatVal) & " AND di_datascad >= " & CDataSQL(strDatVal)
      End If
      If Not bChiuse Then
        strSQL += " AND di_chiusa = 'N'"
      End If
      'If bRevocate Then
      '  'VOGLIO VEDERE TUTTE LE REVOCATE
      '  strSQL += " AND di_revocata = 'S'"
      'End If
      If strTipo <> "T" Then
        strSQL += " AND di_tipodich = " & CStrSQL(strTipo)
      End If
      strSQL += " ORDER BY di_anno DESC, di_numero DESC"
      dsHldi = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DICHINT")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CollegaMGDI(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Try
      strSQL = " SELECT ac_mgdi FROM anaditac WHERE codditt = " & CStrSQL(strDitta)
      dttOut = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      Else
        If NTSCStr(dttOut.Rows(0)!ac_mgdi) = "N" Then
          Return False
        Else
          Return True
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function LeggiDatiPlafondDich(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, _
                                                  ByVal nNumero As Integer, ByVal bCollega_MG_DI As Boolean, _
                                                  ByRef bNoPlafondDic As Boolean, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = " SELECT dichint.*" & _
               " FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & nNumero
      ds = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DICHINT")

      If ds.Tables("DICHINT").Rows.Count = 0 Then
        bNoPlafondDic = True
        Return True
      End If

      If NTSCStr(ds.Tables("DICHINT").Rows(0)!di_tipodich) = "3" Then
        bNoPlafondDic = True
        Return True
      End If

      bNoPlafondDic = False

      strSQL = " SELECT prinot.*" & _
               " FROM (((prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_contocf = anagra.an_conto)" & _
               " INNER JOIN moviva ON prinot.codditt = moviva.codditt And prinot.pn_datreg = moviva.mi_datreg and prinot.pn_numreg = moviva.mi_numreg And prinot.pn_riga = moviva.mi_riga) " & _
               " INNER JOIN tabciva ON prinot.pn_codiva = tabciva.tb_codciva)" & _
               " LEFT JOIN caucset ON prinot.pn_causale = caucset.cs_codcauc AND prinot.pn_csriga = caucset.cs_riga" & _
               " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
               " AND an_tipo = " & CStrSQL(strTipo) & _
               " AND tb_tipiva = '3'" & _
               " AND pn_dianno = " & nAnno & _
               " AND pn_dinumero = " & nNumero
      ds = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PRINOT", ds)

      If bCollega_MG_DI Then
        strSQL = " SELECT testmag.*" & _
                 " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto " & _
                 " INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND  " & _
                 " testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc " & _
                 " LEFT JOIN testmag AS testmag_1 ON testmag.codditt = testmag_1.codditt AND testmag.tm_tiporkfat = testmag_1.tm_tipork AND testmag.tm_annfat = testmag_1.tm_anno AND  " & _
                 " testmag.tm_alffat = testmag_1.tm_serie And testmag.tm_numfat = testmag_1.tm_numdoc" & _
                 " WHERE testmagc.codditt = " & CStrSQL(strDitta) & _
                 " AND an_tipo = " & CStrSQL(strTipo) & _
                 " AND tm_dianno = " & nAnno & _
                 " AND tm_dinumero = " & nNumero & _
                 " AND testmag.tm_tipork <> 'U'" & _
                 " AND testmag.tm_flfatt = 'N' " & _
                 " AND ( (testmag.tm_flcont = 'N' AND testmag_1.tm_flcont is null) OR " & _
                 "       (testmag_1.tm_flcont = 'N') )"
        ds = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG", ds)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function CheckCoerenzaTaglieQtaTCO(ByVal strDitta As String, ByRef strErr As String) As Boolean
    Try
      CheckCoerenzaTaglieQtaTCOPeriodo(strDitta, New Date(1900, 1, 1), New Date(2099, 12, 31), strErr)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckCoerenzaTaglieQtaTCOPeriodo(ByVal strDitta As String, ByVal dtInizio As Date, ByVal dtFine As Date, _
                                                               ByRef strErr As String) As Boolean
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strErr = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esiste il modulo Taglie & Colori, esce restituendo True
      '--------------------------------------------------------------------------------------------------------------
      If Not CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtTCO) Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart, ar_descr, adt_fase, tb_codtagl, tb_destagl" & _
        " FROM artico INNER JOIN artdefxtc ON artico.codditt = artdefxtc.codditt" & _
                           " AND artico.ar_codart = artdefxtc.adt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codtagl <> 0" & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_lesist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " ORDER BY ar_codart"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strErr = oApp.Tr(Me, 130785708013021476, "Attenzione!" & vbCrLf & _
            "Per l'articolo '" & NTSCStr(!ar_codart) & "|'" & _
            IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr).Trim & "|", "").ToString & vbCrLf & _
            "Fase '|" & NTSCStr(!adt_fase) & "|'" & vbCrLf & _
            "Taglia '|" & NTSCStr(!tb_codtagl) & "|'" & _
            IIf(NTSCStr(!tb_destagl).Trim <> "", " - |" & NTSCStr(!tb_destagl).Trim & "|", "").ToString & vbCrLf & _
            "esistono progressivi definitivi articoli (ARTDEFXTC) su taglie non presenti in tabella scala taglie.")
        End With
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart, ar_descr, adt_magaz, adt_fase, tb_codtagl, tb_destagl" & _
        " FROM artico INNER JOIN artdeftc ON artico.codditt = artdeftc.codditt" & _
                           " AND artico.ar_codart = artdeftc.adt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codtagl <> 0" & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " ORDER BY ar_codart"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strErr += IIf(strErr.Trim <> "", vbCrLf, "").ToString & _
            oApp.Tr(Me, 130785708063804676, "Attenzione!" & vbCrLf & _
            "Per l'articolo '" & NTSCStr(!ar_codart) & "|'" & _
            IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr).Trim & "|", "").ToString & vbCrLf & _
            "Magazzino '|" & NTSCStr(!adt_magaz) & "|'" & vbCrLf & _
            "Fase '|" & NTSCStr(!adt_fase) & "|'" & vbCrLf & _
            "Taglia '|" & NTSCStr(!tb_codtagl) & "|'" & _
            IIf(NTSCStr(!tb_destagl).Trim <> "", " - |" & NTSCStr(!tb_destagl).Trim & "|", "").ToString & vbCrLf & _
            "esistono progressivi definitivi articoli/magazzino (ARTDEFTC) su taglie non presenti in tabella scala taglie.")
        End With
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart, ar_descr, adt_magaz, adt_commeca, adt_lotto, adt_fase, adt_ubicaz," & _
        " tb_codtagl, tb_destagl" & _
        " FROM artico INNER JOIN lotcdeftc ON artico.codditt = lotcdeftc.codditt" & _
                           " AND artico.ar_codart = lotcdeftc.adt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codtagl <> 0" & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " ORDER BY ar_codart"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strErr += IIf(strErr.Trim <> "", vbCrLf, "").ToString & _
            oApp.Tr(Me, 130785708110281070, "Attenzione!" & vbCrLf & _
            "Per l'articolo '" & NTSCStr(!ar_codart) & "|'" & _
            IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr).Trim & "|", "").ToString & vbCrLf & _
            "Magazzino '|" & NTSCStr(!adt_magaz) & "|'" & vbCrLf & _
            "Commessa '|" & NTSCStr(!adt_commeca) & "|'" & vbCrLf & _
            "Lotto '|" & NTSCStr(!adt_lotto) & "|'" & vbCrLf & _
            "Fase '|" & NTSCStr(!adt_fase) & "|'" & vbCrLf & _
            "Ubicazione '|" & NTSCStr(!adt_ubicaz) & "|'" & vbCrLf & _
            "Taglia '|" & NTSCStr(!tb_codtagl) & "|'" & _
            IIf(NTSCStr(!tb_destagl).Trim <> "", " - |" & NTSCStr(!tb_destagl).Trim & "|", "").ToString & vbCrLf & _
            "esistono progressivi definitivi lotti/commesse/ubicazioni (LOTCDEFTC) su taglie non presenti in tabella scala taglie.")
        End With
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart, ar_descr, movmag.mm_anno, movmag.mm_tipork, movmag.mm_serie, movmag.mm_numdoc," & _
               "            movmag.mm_riga, movmag.mm_codart, tabtagl.tb_codtagl, tabtagl.tb_destagl" & _
               " FROM movmag " & _
               "  INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork" & _
               "                     AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie" & _
               "                     AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
               "  INNER JOIN testmag ON movmag.codditt = testmag.codditt AND movmag.mm_tipork = testmag.tm_tipork" & _
               "                     AND movmag.mm_anno = testmag.tm_anno AND movmag.mm_serie = testmag.tm_serie" & _
               "                     AND movmag.mm_numdoc = testmag.tm_numdoc " & _
               "  INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
               "  INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
               " WHERE ar_codtagl <> 0 " & _
               "  AND tm_datdoc BETWEEN " & CDataSQL(dtInizio) & " AND " & CDataSQL(dtFine) & _
               " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND mm_quant" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " ORDER BY movmag.mm_anno, movmag.mm_tipork, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga"
      dttTmp = oCldBase.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strErr += IIf(strErr.Trim <> "", vbCrLf, "").ToString & _
            oApp.Tr(Me, 130785708155360926, "Attenzione!" & vbCrLf & _
            "Per l'articolo '" & NTSCStr(!ar_codart) & "|'" & _
            IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr).Trim & "|", "").ToString & vbCrLf & _
            "|" & DecodificaDocumento(NTSCStr(!mm_tipork)) & "| N°|" & NTSCStr(!mm_numdoc) & IIf(NTSCStr(!mm_serie).Trim <> "", "/", "").ToString & "| del |" & NTSCStr(!mm_anno) & "|" & vbCrLf & _
            "Taglia '|" & NTSCStr(!tb_codtagl) & "|'" & _
            IIf(NTSCStr(!tb_destagl).Trim <> "", " - |" & NTSCStr(!tb_destagl).Trim & "|", "").ToString & vbCrLf & _
            "esistono movimenti di magazzino (MOVMAGTC) su taglie non presenti in tabella scala taglie.")
        End With
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If strErr.Trim <> "" Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function DecodificaDocumento(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      DecodificaDocumento = ""
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A" : Return "Fattura Immediata Emessa"
        Case "B" : Return "D.D.T Emesso"
        Case "C" : Return "Corrispettivo Emesso"
        Case "D" : Return "Fattura Differita Emessa"
        Case "E" : Return "Note di Addebito Emessa"
        Case "F" : Return "Ricevuta Fiscale Emessa"
        Case "I" : Return "Riemissione Ricevuta Fiscale"
        Case "J" : Return "Nota di Accredito Ricevuta"
        Case "K" : Return "Fattura Differita Ricevuta"
        Case "L" : Return "Fattura Immediata Ricevuta"
        Case "M" : Return "D.D.T. Ricevuto"
        Case "N" : Return "Nota di Accredito Emessa"
        Case "P" : Return "Fattura/Ricevuta Fiscale Differita"
        Case "S" : Return "Fattura/Ricevuta Fiscale Emessa"
        Case "T" : Return "Carico da Produzione"
        Case "U" : Return "Scarico a Produzione"
        Case "W" : Return "Nota di Prelievo"
        Case "Z" : Return "Bolla di Movimentazione Interna"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

End Class
