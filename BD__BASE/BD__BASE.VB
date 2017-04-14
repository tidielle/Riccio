Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System.Windows.Forms

Public Class CLD__BASE
  Inherits MarshalByRefObject             'serve per poter gestire il remoting
  'base per tutti i dal
  '----------------------------------------------
  'INIZIALIZZATORI DI TUTTI I DAL
  Public oLbBase As Object = Nothing      'puntatore a BDLBBASE
  Public oScriptE As INT__SCRIPT = Nothing 'puntatore alla dll sidecar dell'entity che mi ha istanziato
  Dim oApp_P As CLE__APP
  Dim fQuery As StreamWriter
  Dim bInTrans As Boolean = False         'se true sono dentro ad una transazione
  Public traDb As DbTransaction = Nothing 'se <> nothing contiene la transazione aperta

  Public strJoinTestordMovord As String = " testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord "
  Public strJoinTestmagMovmag As String = " testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc "
  Public strSelectPriana2 As String = "SELECT priana2.*, ac_descr1 as xx_conto, tb_descena as xx_codcena, tb_descfam as xx_codcfam, " & _
                                     " tb_desdivi as xx_coddivi, tb_desstab as xx_codstab, tb_desvalu as xx_codvalu, " & _
                                     " anagras.an_descr1 as xx_contocg, tb_descaca as xx_codcaca, ar_descr as xx_codart, af_descr as xx_fase, " & _
                                     " anagrac.an_descr1 as xx_codcli, co_descr1 as xx_commeca, ac_accperi as xx_accperi, " & _
                                     " ac_richcena as xx_richcena, ac_richcomm as xx_richcomm, ac_richcfam as xx_richcfam, " & _
                                     " ac_richdivi as xx_richdivi, ac_richstab as xx_richstab, ac_richarti as xx_richarti, ac_richcli as xx_richcli, " & _
                                     " CASE WHEN isnull(tsk_descr, '') = '' THEN cwb_descr ELSE tsk_descr END as xx_idwbs, " & _
                                     " 0.0 as xx_pmqtares, 0.0 as xx_pmqtarim, 0.0 as xx_pmvalres, 0.0 as xx_pmvalrim, " & _
                                     " 0.0 as xx_pmqtadisda, 0.0 as xx_pmvaldisda, 0.0 as xx_pmtemprim, '1' as xx_gestcost, 'N' as xx_prevgrup  " & _
                                     " FROM priana2 LEFT JOIN anagca ON priana2.codditt = anagca.codditt AND priana2.pa2_conto = anagca.ac_conto " & _
                                     " LEFT JOIN tabcaca ON priana2.pa2_codcaca = tabcaca.tb_codcaca " & _
                                     " LEFT JOIN commess ON priana2.codditt = commess.codditt AND priana2.pa2_commeca = commess.co_comme " & _
                                     " LEFT JOIN tabcena ON priana2.codditt = tabcena.codditt AND priana2.pa2_codcena = tabcena.tb_codcena " & _
                                     " LEFT JOIN tabcfam ON priana2.codditt = tabcfam.codditt AND priana2.pa2_codcfam = tabcfam.tb_codcfam " & _
                                     " LEFT JOIN tabdivi ON priana2.codditt = tabdivi.codditt AND priana2.pa2_coddivi = tabdivi.tb_coddivi " & _
                                     " LEFT JOIN tabstab ON priana2.codditt = tabstab.codditt AND priana2.pa2_codstab = tabstab.tb_codstab " & _
                                     " LEFT JOIN tabvalu ON priana2.pa2_codvalu = tabvalu.tb_codvalu " & _
                                     " LEFT JOIN anagra as anagras ON priana2.codditt = anagras.codditt AND priana2.pa2_contocg = anagras.an_conto " & _
                                     " LEFT JOIN anagra as anagrac ON priana2.codditt = anagrac.codditt AND priana2.pa2_codcli = anagras.an_conto " & _
                                     " LEFT JOIN artico ON priana2.codditt = artico.codditt AND priana2.pa2_codart = artico.ar_codart " & _
                                     " LEFT JOIN artfasi ON priana2.codditt = artfasi.codditt AND priana2.pa2_codart = artfasi.af_codart AND priana2.pa2_fase = artfasi.af_fase " & _
                                     " LEFT JOIN comwbs ON priana2.codditt = comwbs.codditt AND priana2.pa2_commeca = comwbs.cwb_commeca AND priana2.pa2_subcommeca = comwbs.cwb_subcommeca AND priana2.pa2_idwbs = comwbs.cwb_idwbs " & _
                                     " LEFT JOIN tasks ON priana2.codditt = tasks.codditt AND priana2.pa2_commeca = tasks.tsk_commeca and priana2.pa2_idwbs = tasks.tsk_taskid "
  Public SET_TRANSACTION_ISOLATION_LEVEL_READ_UNCOMMITTED As Boolean = False  'non documentare: se settata = true dopo aver istanziato il DAL le select vengono fatte con l'apposita opzione e non danno errore se la tabella su cui opera la select è bloccata da una transazione

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


  Public Overridable Function GetFactory(ByVal tipoDb As CLE__APP.DBTIPO) As DbProviderFactory
    Select Case tipoDb
      Case CLE__APP.DBTIPO.DBAZI
        If oApp_P.Db.Tipo = "SQLServer" Then
          Return DbProviderFactories.GetFactory("System.Data.SqlClient")
        Else
          Return DbProviderFactories.GetFactory(oApp_P.Db.Tipo)
        End If
      Case CLE__APP.DBTIPO.DBPRC
        If oApp_P.DbAp.Tipo = "SQLServer" Then
          Return DbProviderFactories.GetFactory("System.Data.SqlClient")
        Else
          Return DbProviderFactories.GetFactory(oApp_P.DbAp.Tipo)
        End If
    End Select
    Return Nothing
  End Function

  Public ReadOnly Property IsInTrans() As Boolean
    Get
      Return bInTrans
    End Get
  End Property

  Public Overridable Function ApriTrans(ByVal dbConn As DbConnection) As Boolean
    Try
      traDb = dbConn.BeginTransaction()
      bInTrans = True
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ChiudiTrans() As Boolean
    Try
      traDb.Commit()
      bInTrans = False
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function AnnullaTrans() As Boolean
    Try
      traDb.Rollback()
      bInTrans = False
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public ReadOnly Property oApp() As CLE__APP
    Get
      Return oApp_P
    End Get
  End Property


  Public Function LegInstDitt(ByVal strcodditt As String, ByRef dttInsg As DataTable, ByRef strError As String) As Boolean
    'NON PORTARE NELLA BDLBBASE!!!!!!!
    Dim strSQL As String
    Dim dsTmp As New DataSet
    Dim i As Integer = 0
    Dim strNomeCampo As String = ""

    Try
      dttInsg = Nothing

      'verifico se ci sono record in tabanaz
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABANAZ") Then
        If oApp.DSShared.Tables("TABANAZ").Rows.Count = 0 Then
          strError = oApp.Tr(Me, 129222946102802734, "Attenzione! Nel database non è presente alcuna ditta. Crearne una il prima possibile.")
          Return False
        End If
      Else
        strSQL = "SELECT count(*) as NREC FROM tabanaz"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
        If dsTmp.Tables("TABANAZ").Rows.Count = 0 Then
          strError = oApp.Tr(Me, 127792167990781250, "Attenzione! Nel database non è presente alcuna ditta. Crearne una il prima possibile.")
          Return False
        End If
      End If

      dsTmp.Tables.Clear()
      If oApp.bDSShared And oApp.DSShared.Tables.Contains("TABINST") Then
        'leggo da tabelle in cache
        dsTmp = New DataSet
        dsTmp.Tables.Add(oApp.DSShared.Tables("TABINST").Copy)
        For Each dtrT As DataRow In dsTmp.Tables("TABINST").Rows
          If dtrT!codditt.ToString.ToUpper <> strcodditt Then dtrT.Delete()
        Next
        dsTmp.AcceptChanges()
      Else
        strSQL = " SELECT * FROM TABINST " & " WHERE codditt = " & CStrSQL(strcodditt)
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABINST")
      End If

      If oApp.bDSShared And oApp.DSShared.Tables.Contains("ANAZMOD") Then
        'leggo da tabelle in cache
        dsTmp.Tables.Add(oApp.DSShared.Tables("ANAZMOD").Copy)
        For Each dtrT As DataRow In dsTmp.Tables("ANAZMOD").Rows
          If dtrT!codditt.ToString.ToUpper <> strcodditt Then dtrT.Delete()
        Next
        dsTmp.AcceptChanges()
      Else
        strSQL = " SELECT * FROM ANAZMOD " & _
                 " WHERE codditt = " & CStrSQL(strcodditt) & _
                 " ORDER BY am_modulo "
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAZMOD", dsTmp)
      End If

      If dsTmp.Tables("TABINST").Rows.Count = 0 Or dsTmp.Tables("ANAZMOD").Rows.Count = 0 Then
        strError = oApp.Tr(Me, 127792168431250000, "Attenzione! Ditta '|" & UCase(strcodditt) & "|' inesistente oppure non inizializzata!")
        Return False
      Else
        If dsTmp.Tables("ANAZMOD").Rows.Count = 0 Then
          strError = oApp.Tr(Me, 127792168473281250, "Attenzione! Ditta '|" & UCase(strcodditt) & "|' inesistente oppure non inizializzata!")
          Return False
        Else
          If dsTmp.Tables("ANAZMOD").Rows.Count < 60 Then
            strError = oApp.Tr(Me, 127792168507968750, "Attenzione! L'inizializzazione della ditta '|" & UCase(strcodditt) & "|' è incompleta!")
            Return False
          End If
        End If

        dttInsg = New DataTable()
        dttInsg.Columns.Add("val", GetType(String))
        dttInsg.Columns.Add("cod", GetType(String))

        'Le righe di ANAZMOD sono sicuramente 60, possiamo procedere
        '-----------------------------------------------------------
        'Partiamo dalle informazioni di TABINST
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!codditt.ToString, "codditt"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_codinst.ToString, "tb_codinst"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_denom.ToString, "tb_denom"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_1.ToString, "tb_opz_1"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_2.ToString, "tb_opz_2"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_3.ToString, "tb_opz_3"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_4.ToString, "tb_opz_4"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_5.ToString, "tb_opz_5"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_6.ToString, "tb_opz_6"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_7.ToString, "tb_opz_7"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_8.ToString, "tb_opz_8"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_9.ToString, "tb_opz_9"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_10.ToString, "tb_opz_10"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_11.ToString, "tb_opz_11"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_12.ToString, "tb_opz_12"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_13.ToString, "tb_opz_13"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_14.ToString, "tb_opz_14"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_15.ToString, "tb_opz_15"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_16.ToString, "tb_opz_16"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_17.ToString, "tb_opz_17"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_18.ToString, "tb_opz_18"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_19.ToString, "tb_opz_19"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_20.ToString, "tb_opz_20"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_21.ToString, "tb_opz_21"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_22.ToString, "tb_opz_22"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_23.ToString, "tb_opz_23"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz_24.ToString, "tb_opz_24"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfeffe.ToString, "tb_nfeffe"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfcorr.ToString, "tb_nfcorr"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfmenu.ToString, "tb_nfmenu"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfboll.ToString, "tb_nfboll"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffata.ToString, "tb_nffata"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffatd.ToString, "tb_nffatd"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfordi.ToString, "tb_nfordi"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfrapp.ToString, "tb_nfrapp"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfpcas.ToString, "tb_nfpcas"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfofax.ToString, "tb_nfofax"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_1.ToString, "tb_opz2_1"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_2.ToString, "tb_opz2_2"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_3.ToString, "tb_opz2_3"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_4.ToString, "tb_opz2_4"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_5.ToString, "tb_opz2_5"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_6.ToString, "tb_opz2_6"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_7.ToString, "tb_opz2_7"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_8.ToString, "tb_opz2_8"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_9.ToString, "tb_opz2_9"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_opz2_10.ToString, "tb_opz2_10"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffati.ToString, "tb_nffati"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfimpe.ToString, "tb_nfimpe"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprev.ToString, "tb_nfprev"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprb.ToString, "tb_nfprb"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfcorrv.ToString, "tb_nfcorrv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfbollv.ToString, "tb_nfbollv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffatav.ToString, "tb_nffatav"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffatdv.ToString, "tb_nffatdv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfordiv.ToString, "tb_nfordiv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffativ.ToString, "tb_nffativ"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfimpev.ToString, "tb_nfimpev"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprevv.ToString, "tb_nfprevv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprbv.ToString, "tb_nfprbv"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfcorric.ToString, "tb_nfcorric"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfbollic.ToString, "tb_nfbollic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffataic.ToString, "tb_nffataic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffatdic.ToString, "tb_nffatdic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfordiic.ToString, "tb_nfordiic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nffatiic.ToString, "tb_nffatiic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfimpeic.ToString, "tb_nfimpeic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprevic.ToString, "tb_nfprevic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nfprbic.ToString, "tb_nfprbic"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_nflogo.ToString, "tb_nflogo"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_xlscee.ToString, "tb_xlscee"})
        dttInsg.Rows.Add(New Object() {dsTmp.Tables("TABINST").Rows(0)!tb_xlsric.ToString, "tb_xlsric"})

        'Continuiamo con le informazioni di ANAZMOD
        i = 0
        While i < dsTmp.Tables("ANAZMOD").Rows.Count
          Select Case NTSCInt(dsTmp.Tables("ANAZMOD").Rows(i)!am_modulo)
            Case 1 : strNomeCampo = "tb_mod_01"
            Case 2 : strNomeCampo = "tb_mod_02"
            Case 3 : strNomeCampo = "tb_mod_03"
            Case 4 : strNomeCampo = "tb_mod_04"
            Case 5 : strNomeCampo = "tb_mod_05"
            Case 6 : strNomeCampo = "tb_mod_06"
            Case 7 : strNomeCampo = "tb_mod_07"
            Case 8 : strNomeCampo = "tb_mod_08"
            Case 9 : strNomeCampo = "tb_mod_09"
            Case 10 : strNomeCampo = "tb_mod_10"
            Case 11 : strNomeCampo = "tb_mod_11"
            Case 12 : strNomeCampo = "tb_mod_12"
            Case 13 : strNomeCampo = "tb_mod_13"
            Case 14 : strNomeCampo = "tb_mod_14"
            Case 15 : strNomeCampo = "tb_mod_15"
            Case 16 : strNomeCampo = "tb_mod2_01"
            Case 17 : strNomeCampo = "tb_mod2_02"
            Case 18 : strNomeCampo = "tb_mod2_03"
            Case 19 : strNomeCampo = "tb_mod2_04"
            Case 20 : strNomeCampo = "tb_mod2_05"
            Case 21 : strNomeCampo = "tb_mod2_06"
            Case 22 : strNomeCampo = "tb_mod2_07"
            Case 23 : strNomeCampo = "tb_mod2_08"
            Case 24 : strNomeCampo = "tb_mod2_09"
            Case 25 : strNomeCampo = "tb_mod2_10"
            Case 26 : strNomeCampo = "tb_mod2_11"
            Case 27 : strNomeCampo = "tb_mod2_12"
            Case 28 : strNomeCampo = "tb_mod2_13"
            Case 29 : strNomeCampo = "tb_mod2_14"
            Case 30 : strNomeCampo = "tb_mod2_15"
            Case 31 : strNomeCampo = "tb_mod3_01"
            Case 32 : strNomeCampo = "tb_mod3_02"
            Case 33 : strNomeCampo = "tb_mod3_03"
            Case 34 : strNomeCampo = "tb_mod3_04"
            Case 35 : strNomeCampo = "tb_mod3_05"
            Case 36 : strNomeCampo = "tb_mod3_06"
            Case 37 : strNomeCampo = "tb_mod3_07"
            Case 38 : strNomeCampo = "tb_mod3_08"
            Case 39 : strNomeCampo = "tb_mod3_09"
            Case 40 : strNomeCampo = "tb_mod3_10"
            Case 41 : strNomeCampo = "tb_mod3_11"
            Case 42 : strNomeCampo = "tb_mod3_12"
            Case 43 : strNomeCampo = "tb_mod3_13"
            Case 44 : strNomeCampo = "tb_mod3_14"
            Case 45 : strNomeCampo = "tb_mod3_15"
            Case 46 : strNomeCampo = "tb_mod4_01"
            Case 47 : strNomeCampo = "tb_mod4_02"
            Case 48 : strNomeCampo = "tb_mod4_03"
            Case 49 : strNomeCampo = "tb_mod4_04"
            Case 50 : strNomeCampo = "tb_mod4_05"
            Case 51 : strNomeCampo = "tb_mod4_06"
            Case 52 : strNomeCampo = "tb_mod4_07"
            Case 53 : strNomeCampo = "tb_mod4_08"
            Case 54 : strNomeCampo = "tb_mod4_09"
            Case 55 : strNomeCampo = "tb_mod4_10"
            Case 56 : strNomeCampo = "tb_mod4_11"
            Case 57 : strNomeCampo = "tb_mod4_12"
            Case 58 : strNomeCampo = "tb_mod4_13"
            Case 59 : strNomeCampo = "tb_mod4_14"
            Case 60 : strNomeCampo = "tb_mod4_15"
            Case 101 To 130 : strNomeCampo = "tb_modsup_" & (NTSCInt(dsTmp.Tables("ANAZMOD").Rows(i)!am_modulo) - 100)
            Case 131 To 160 : strNomeCampo = "tb_modsupext_" & (NTSCInt(dsTmp.Tables("ANAZMOD").Rows(i)!am_modulo) - 130)
            Case 201 To 230 : strNomeCampo = "tb_modptn_" & (NTSCInt(dsTmp.Tables("ANAZMOD").Rows(i)!am_modulo) - 200)
            Case 231 To 260 : strNomeCampo = "tb_modptnext_" & (NTSCInt(dsTmp.Tables("ANAZMOD").Rows(i)!am_modulo) - 230)
          End Select
          dttInsg.Rows.Add(New Object() {dsTmp.Tables("ANAZMOD").Rows(i)!am_abilit.ToString, strNomeCampo})
          i = i + 1
        End While
        dttInsg.AcceptChanges()
      End If    'If dsTmp.Tables("TABINST").Rows.Count = 0 Or dsTmp.Tables("ANAZMOD").Rows.Count = 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Sub ScriviFile(ByVal strQuery As String, ByVal dbTipo As CLE__APP.DBTIPO, ByVal strNomeTabella As String)
    Dim nRetray As Integer = 0
    Try
      If oApp.bWriteQueryInLogFile = False Then
        'scrivo in memoria
        Dim strLog As System.Text.StringBuilder
        If dbTipo = CLE__APP.DBTIPO.DBPRC Then
          strLog = oApp.strQueryLogFilePrc
        Else
          strLog = oApp.strQueryLogFile
        End If

        If strLog.Length > 1000000 Then strLog.Remove(0, 100000) 'Quando si supera il milione di carattaeri, libera i primi 100'000 per occupare meno spazio in memoria
        If Not strQuery.StartsWith("          TEMPO IMPIEGATO", StringComparison.CurrentCultureIgnoreCase) Then
          strLog.Append(DateTime.Now)
          strLog.Append(":")
          strLog.Append(oApp_P.User.Nome)
          strLog.Append(": ")
          strLog.Append(strNomeTabella)
          strLog.Append(": ")
        End If
        strLog.AppendLine(strQuery)
        Return
      End If

      Dim strNomeFile As String
      Dim f1 As FileInfo
      If dbTipo = CLE__APP.DBTIPO.DBPRC Then
        strNomeFile = oApp_P.Dir & "\QueryPrc_" & oApp.User.Nome & ".log"
      Else
        strNomeFile = oApp_P.Dir & "\QueryAz_" & oApp.User.Nome & ".log"
      End If

      Try
        f1 = New FileInfo(strNomeFile)
        If f1.Exists Then
          If f1.Length > 1000000 Then f1.Delete()
        End If
      Catch ex As Exception
        'non gestisco l'errore: potrebbe essere il caso di terminal server dove più utenti scrivono sullo stesso file ...
      End Try

      '-------------------------------------------------
      'il file potrebbe essere aperto dal thread secondario o da un'altra sessione di busnet 
      'aperta sullo stesso pc: se da errore di file in uso riprovo per un altro paio di volte
RIPROVA:
      Try
        fQuery = New StreamWriter(strNomeFile, True)
      Catch ex As Exception
        If nRetray < 20 Then
          nRetray += 1
          If nRetray = 18 Then System.Threading.Thread.Sleep(1000)
          GoTo riprova
        Else
          Return
          'Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
        End If
      End Try
      If Not fQuery Is Nothing Then
        Try
          fQuery.Write(IIf(Microsoft.VisualBasic.Left(strQuery, 25) <> "          TEMPO IMPIEGATO", DateTime.Now.ToString() + ": " + oApp_P.User.Nome + ": " + strNomeTabella + ": ", "").ToString + strQuery & vbCrLf)
          fQuery.Flush()
          fQuery.Close()
        Catch ex As Exception
          'non gestisco l'errore: potrebbe essere il caso di terminal server dove più utenti scrivono sullo stesso file ...
          Dim str As String = ex.Message
        End Try
        fQuery = Nothing
      End If
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Sub

  Public Overridable Sub Init(ByVal Applic As CLE__APP)
    oApp_P = Applic

    '------------------------------------
    'test per remoting 
    '.Show(oApp.Db.Tipo + " - " + oApp_P.Dir + " - " + oApp.User.Nome + " - " + oApp.Db.Nome)

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


    '------------------------------------
    'inizializzo la dll libreria
    Dim strErr As String = ""
    If CLN__STD.NTSIstanziaDll(Applic.ServerDir, Applic.NetDir, "BD__BASE", "BDLBBASE", oLbBase, strErr, False, "", "") = False Then
      Throw New Exception(strErr)
    Else
      oLbBase.Init(oApp, Me)
    End If
  End Sub

  Public Property oScriptEntity() As INT__SCRIPT
    Get
      Return oScriptE
    End Get
    Set(ByVal value As INT__SCRIPT)
      oScriptE = value
      oLbBase.oScriptE = value
    End Set
  End Property

  Public Overridable Function LegNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                        ByVal lCodl As Integer, ByVal bCrea As Boolean) As Integer
    Try
      Return LegNuma(strDitta, strTipo, strSerie, lCodl, bCrea, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function LegNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal bCrea As Boolean, _
                          ByRef dbConn As DbConnection) As Integer
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipo, strSerie, lCodl, bCrea, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(5), DbConnection)
        Return NTSCInt(oOut)
      End If
      '----------------

      Return oLbBase.LegNuma(strDitta, strTipo, strSerie, lCodl, bCrea, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DelNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                        ByVal lCodl As Integer, ByVal lProg As Integer, ByRef dbConn As DbConnection) As Boolean
    Try
      Return oLbBase.DelNuma(strDitta, strTipo, strSerie, lCodl, lProg, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggNuma(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal lNewProg As Integer, ByVal bControlla As Boolean, _
                          ByVal bVisMsg As Boolean, ByRef strReturnMessage As String, _
                          Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Try
      Return oLbBase.AggNuma(strDitta, strTipo, strSerie, lCodl, lNewProg, bControlla, bVisMsg, strReturnMessage, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LegNumg(ByVal strTipo As String, ByVal strSerie As String, ByVal lCodl As Integer, _
                          ByVal bCrea As Boolean) As Integer
    Try
      Return LegNumg(strTipo, strSerie, lCodl, bCrea, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LegNumg(ByVal strTipo As String, ByVal strSerie As String, ByVal lCodl As Integer, _
                          ByVal bCrea As Boolean, ByRef dbConn As DbConnection) As Integer

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipo, strSerie, lCodl, bCrea, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(4), DbConnection)
        Return NTSCInt(oOut)
      End If
      '----------------

      Return oLbBase.LegNumg(strTipo, strSerie, lCodl, bCrea, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggNumg(ByVal strTipo As String, ByVal strSerie As String, _
                          ByVal lCodl As Integer, ByVal lNewProg As Integer, ByVal bControlla As Boolean, _
                          ByVal bVisMsg As Boolean, ByRef strReturnMessage As String, _
                          Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Try
      Return oLbBase.AggNumg(strTipo, strSerie, lCodl, lNewProg, bControlla, bVisMsg, strReturnMessage, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                ByVal strNomProp As String, ByVal strOut As String, ByVal strTipoDoc As String, _
                                ByVal strDefault As String) As String
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return GetSettingBusDitt(oApp_P.Ditta, strLiv1, strLiv2, strLiv3, strNomProp, strOut, strTipoDoc, strDefault)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                ByVal strNomProp As String, ByVal strOut As String, ByVal strTipoDoc As String, _
                                ByVal strDefault As String) As String
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return oLbBase.GetSettingBusDitt(strDitta, strLiv1, strLiv2, strLiv3, strNomProp, strOut, strTipoDoc, strDefault)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                            ByVal strTipoDoc As String, ByRef dttOut As DataTable) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return oLbBase.GetSettingBusDitt(oApp_P.Ditta, strLiv1, strLiv2, strLiv3, strTipoDoc, dttOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                                ByVal strTipoDoc As String, ByRef dttOut As DataTable) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return oLbBase.GetSettingBusDitt(strDitta, strLiv1, strLiv2, strLiv3, strTipoDoc, strTipoDoc)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveSettingBus(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                               ByVal strNomProp As String, ByVal strIn As String, ByVal strTipoDoc As String, _
                               ByVal strDipNomOp As String, ByVal strDipAz As String, ByVal strDipTipDoc As String) As Boolean
    Try
      Return SaveSettingBusDitt(oApp_P.Ditta, strLiv1, strLiv2, strLiv3, strNomProp, strIn, strTipoDoc, strDipNomOp, strDipAz, strDipTipDoc)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SaveSettingBusDitt(ByVal strDitta As String, ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, _
                                 ByVal strNomProp As String, ByVal strIn As String, ByVal strTipoDoc As String, _
                                 ByVal strDipNomOp As String, ByVal strDipAz As String, ByVal strDipTipDoc As String) As Boolean
    Try
      Return oLbBase.SaveSettingBusDitt(strDitta, strLiv1, strLiv2, strLiv3, strNomProp, strIn, strTipoDoc, strDipNomOp, strDipAz, strDipTipDoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestConnessione(ByVal tipoDb As CLE__APP.DBTIPO, ByRef dMillisecond As Decimal) As Boolean
    Return TestConnessione(tipoDb, dMillisecond, "")
  End Function

  Public Overridable Function TestConnessione(ByVal tipoDb As CLE__APP.DBTIPO, ByRef dMillisecond As Decimal, ByRef strDescr As String) As Boolean
    Dim dtUno As DateTime = Nothing
    Dim dtDue As DateTime = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim factory As DbProviderFactory = Nothing
    Dim dttTmp As New DataTable
    Dim strSQL As String = ""
    Dim strSqlVersion As String = ""
    Try
      strDescr = ""

      dMillisecond = -1

      dtUno = DateTime.Now
      dbConn = ApriDB_P(tipoDb, factory)
      dtDue = DateTime.Now
      dMillisecond = dtDue.Subtract(dtUno).TotalMilliseconds

      strSQL = "SELECT sys.objects.name as table_name, value " & _
              " FROM sys.objects LEFT JOIN sys.extended_properties ON sys.objects.object_id = sys.extended_properties.major_id " & _
              " AND sys.objects.parent_object_id = sys.extended_properties.minor_id " & _
              " AND sys.extended_properties.name = 'MS_Description'" & _
              " WHERE sys.objects.type = 'U' AND is_ms_shipped = 0 AND sys.objects.name = 'release'"

      dttTmp = OpenRecordset(strSQL, tipoDb)

      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!value).Trim = "Release" Then strDescr = "Desint."
      End If

      If oApp.DBKeepAlive = False Then
        dbConn.Close()
        dbConn = Nothing
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDB(ByVal tipoDb As CLE__APP.DBTIPO) As DbConnection
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return ApriDB_P(tipoDb)
    Catch ex As Exception
      Throw (New ArgumentException())
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '---------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function ApriDB(ByVal tipoDb As CLE__APP.DBTIPO, ByRef FactoryOut As DbProviderFactory) As DbConnection
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return ApriDB_P(tipoDb, FactoryOut)
    Catch ex As Exception
      Throw (New ArgumentException())
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '---------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Function ApriDB_P(ByVal tipoDb As CLE__APP.DBTIPO, Optional ByRef FactoryOut As DbProviderFactory = Nothing) As DbConnection
    '---------------------------------------------------------
    'apro il database
    '---------------------------------------------------------
    'ATENZIONE: SE IL DATABASE NON SI APRE POTREBBE ESSERE DOVUTO AL FATTO CHE NEL FILE hosts
    'L'INDIRIZZO IP DEL SERVER NON E' CORRETTO: provare a fare un ping per vedere se al nome del 
    'server SQL corrisponde il giusto indirizzo IP
    Dim dbConn As DbConnection = Nothing
    'Dim factory As DbProviderFactory = Nothing
    Dim strDbTipo As String = ""
    Try
      If oApp.DBKeepAlive Then  'riciclo, se presente, la connessione precedentemente aperta
        Select Case tipoDb
          Case CLE__APP.DBTIPO.DBAZI
            If Not oApp_P.Db.dbConn Is Nothing Then
              If oApp_P.Db.dbConn.State <> ConnectionState.Closed Then
                'factory = GetFactory(tipoDb)
                'FactoryOut = factory
                dbConn = oApp_P.Db.dbConn
                Return dbConn
              Else
                oApp_P.Db.dbConn = Nothing
              End If
            End If
          Case CLE__APP.DBTIPO.DBPRC
            If Not oApp_P.DbAp.dbConn Is Nothing Then
              If oApp_P.DbAp.dbConn.State <> ConnectionState.Closed Then
                'factory = GetFactory(tipoDb)
                'FactoryOut = factory
                dbConn = oApp_P.DbAp.dbConn
                Return dbConn
              Else
                oApp_P.DbAp.dbConn = Nothing
              End If
            End If
        End Select
      End If

      'factory = GetFactory(tipoDb)
      'FactoryOut = factory
      'dbConn = factory.CreateConnection()
      'factory = Nothing

      If tipoDb = CLE__APP.DBTIPO.DBAZI Then
        strDbTipo = oApp_P.Db.Tipo
      Else
        strDbTipo = oApp_P.DbAp.Tipo
      End If
      If strDbTipo = "SQLServer" Then
        dbConn = New SqlClient.SqlConnection
      Else
        'esempio di stringa di connessione a SQLite
        If IOS Or ANDROID Then
          'Data Source=prova2013.db;Version=3;New=False;Compress=True;
          dbConn = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll").GetType("Mono.Data.Sqlite.SqliteConnection", True, True)), DbConnection)
        Else
          'Data Source=c:\biz2013\test\prova2013.db;Version=3;New=False;Compress=True;
          dbConn = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll").GetType("System.Data.SQLite.SQLiteConnection", True, True)), DbConnection)
        End If
      End If

      If tipoDb = CLE__APP.DBTIPO.DBAZI Then
        dbConn.ConnectionString = oApp_P.Db.Connect
      Else
        dbConn.ConnectionString = oApp_P.DbAp.Connect
      End If

      dbConn.Open()

      If strDbTipo <> "SQLServer" Then
        Execute("PRAGMA synchronous = 1;", tipoDb, dbConn)
      End If

      If oApp.DBKeepAlive Then    'memorizzo la connessione per query future
        Select Case tipoDb
          Case CLE__APP.DBTIPO.DBAZI : oApp_P.Db.dbConn = dbConn
          Case CLE__APP.DBTIPO.DBPRC : oApp_P.DbAp.dbConn = dbConn
        End Select
      End If

      Return dbConn

    Catch ex As Exception
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '---------------------------------------------------------
      Return Nothing
    End Try
  End Function

  Public Overridable Sub ChiudiDB()
    If oApp.Db.dbConn Is Nothing Then Return
    Try
      oApp.Db.dbConn.Close()
    Catch ex As Exception
    End Try
    oApp.Db.dbConn = Nothing
  End Sub


  Public Overridable Function OpenRecordset(ByVal strQuery As String, ByVal tipoDb As CLE__APP.DBTIPO, Optional ByVal dbConn As DbConnection = Nothing) As DataTable
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim dsTmp As New DataSet
    Try
      OpenRecordset_P(strQuery, tipoDb, "Tab1", dsTmp, dbConn)
      Return dsTmp.Tables(0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return New DataTable
    Finally
      dsTmp.Dispose()
    End Try
  End Function
  Public Overridable Function OpenRecordset(ByVal strQuery As String, ByVal tipoDb As CLE__APP.DBTIPO, ByVal strNomeTabella As String, _
                                  Optional ByRef dsOut As DataSet = Nothing, Optional ByVal dbConn As DbConnection = Nothing) As DataSet
    '   CLN__STD.CheckInvokeCustomFunction(
    Try
      Return OpenRecordset_P(strQuery, tipoDb, strNomeTabella, dsOut, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return New DataSet
    End Try
  End Function
  Private Function OpenRecordset_P(ByVal strQuery As String, ByVal tipoDb As CLE__APP.DBTIPO, ByVal strNomeTabella As String, _
                           Optional ByRef dsOut As DataSet = Nothing, Optional ByVal dbConn As DbConnection = Nothing) As DataSet
    'strQuery: query che deve essere eseguita per ritornare il dataset
    'tipoDb: tipo di database su cui deve essere eseguita la query (azienda, arcproc)
    'strNomeTabella: nome da assegnare alla nuova tabella nel dataset 
    'dsOut: se non viene passato ritorna un dataset semplice, nuovo. Se viene passato aggiunge la nuova tabella al dataset passato
    'dbConn: se impostata non viene aperto e chisuo il database, ma vien utilizzata la connessione che deve essere già aperta

    Dim factory As DbProviderFactory = Nothing
    Dim dbCmd As DbCommand = Nothing
    Dim dbAdapt As DbDataAdapter = Nothing
    If dsOut Is Nothing Then dsOut = New DataSet
    Dim bConn As Boolean = True
    Dim i As Integer = 0
    Dim dtTmp As DateTime
    Dim dtTmpEx As DateTime
    Dim strDbTipo As String = ""

    '---------------------------------------------------------
    'in base al tipo di database lancio la query che mi restituirà un datatable
    '---------------------------------------------------------
    Try
      If Not dbConn Is Nothing Then bConn = False
      ScriviFile(strQuery, tipoDb, strNomeTabella)
      dtTmp = DateTime.Now

      'apro il database
      If dbConn Is Nothing Then
        dbConn = ApriDB_P(tipoDb, factory)
      Else
        'factory = GetFactory(tipoDb)
      End If
      dtTmpEx = DateTime.Now
      If tipoDb = CLE__APP.DBTIPO.DBAZI Then
        strDbTipo = oApp_P.Db.Tipo
      Else
        strDbTipo = oApp_P.DbAp.Tipo
      End If
      If strDbTipo = "SQLServer" Then
        strQuery = ParseSqlUnicode(strQuery, tipoDb = CLE__APP.DBTIPO.DBPRC)
        dbCmd = New SqlClient.SqlCommand
      Else
        If IOS Or ANDROID Then
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll").GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
        Else
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll").GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
        End If
      End If
      dbCmd.Connection = dbConn

      If IsInTrans And bConn = False Then dbCmd.Transaction = traDb

      Select Case tipoDb
        Case CLE__APP.DBTIPO.DBAZI
          dbCmd.CommandTimeout = oApp.Db.QueryTimeOut
        Case CLE__APP.DBTIPO.DBPRC
          dbCmd.CommandTimeout = oApp.DbAp.QueryTimeOut
      End Select

      If strDbTipo = "SQLServer" Then
        If SET_TRANSACTION_ISOLATION_LEVEL_READ_UNCOMMITTED Then
          dbCmd.CommandText = "SET TRANSACTION ISOLATION LEVEL read uncommitted"
          dbCmd.ExecuteNonQuery()
        End If
        dbCmd.CommandText = strQuery
        dbAdapt = New SqlClient.SqlDataAdapter
      Else
        strQuery = CLN__STD.ParseSql(strQuery, 1)
        dbCmd.CommandText = strQuery
        If IOS Or ANDROID Then
          dbAdapt = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll").GetType("Mono.Data.Sqlite.SqliteDataAdapter", True, True)), DbDataAdapter)
        Else
          dbAdapt = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll").GetType("System.Data.SQLite.SQLiteDataAdapter", True, True)), DbDataAdapter)
        End If
      End If
      dbAdapt.SelectCommand = dbCmd

      'ottengo i dati
      If ANDROID Then
        'su android la funzione .FILL da errore perchè la libreria in C presente sul dispositivo è stata 
        'compilata senza la direttiva SQLITE_ENABLE_COLUMN_METADATA
        'devo per forza utilizzare un datareader e creare poi il datatable a mano!!!!!
        If dsOut.Tables.Contains(strNomeTabella) Then dsOut.Tables.Remove(strNomeTabella)
        Dim dttTmp As New DataTable
        FillDatatable(dbAdapt.SelectCommand, dttTmp)
        dsOut.Tables.Add(dttTmp)
      Else
        dbAdapt.Fill(dsOut)
      End If

      dsOut.Tables(dsOut.Tables.Count - 1).TableName = strNomeTabella
      'chiudo il database
      If bConn And oApp.DBKeepAlive = False Then dbConn.Close()

      Dim dtTmp1 As DateTime = DateTime.Now
      Dim lMillisec As Integer = dtTmp1.Subtract(dtTmpEx).TotalMilliseconds
      Dim lMillisecEx As Integer = dtTmpEx.Subtract(dtTmp).TotalMilliseconds
      Dim sbBuilder As New System.Text.StringBuilder(150)
      If lMillisec > 250 OrElse lMillisecEx > 250 Then sbBuilder.Append("*** ")
      sbBuilder.Append("          TEMPO IMPIEGATO apertura connessione/esecuzione query (in millisec): ")
      sbBuilder.Append(lMillisecEx)
      sbBuilder.Append("/")
      sbBuilder.Append(lMillisec)
      sbBuilder.Append(" - Record prelevati: ")
      sbBuilder.Append(dsOut.Tables(dsOut.Tables.Count - 1).Rows.Count)
      ScriviFile(sbBuilder.ToString, tipoDb, strNomeTabella)
      sbBuilder.Length = 0
      '------------------------------------
      'traduco i nomi dell'intestazioni delle colonne tutto in minuscolo.
      'diversamente se devo collegare la tabella ad una griglia ed i nomi colonne sono diversi (case sensitive) 
      'con i FieldName delle colonne della griglia la colonna rimane vuota
      For i = 0 To dsOut.Tables(strNomeTabella).Columns.Count - 1
        dsOut.Tables(strNomeTabella).Columns(i).ColumnName = dsOut.Tables(strNomeTabella).Columns(i).ColumnName.ToLower
      Next

      Return dsOut
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return New DataSet
    Finally
      If bConn Then bConn = Nothing
      factory = Nothing
      Try
        dbCmd.Dispose()
      Catch
      End Try
      dbCmd = Nothing
      Try
        dbAdapt.Dispose()
      Catch
      End Try
      dbAdapt = Nothing
    End Try
  End Function
  Private Sub FillDatatable(ByVal command As System.Data.IDbCommand, ByVal dttOut As System.Data.DataTable)
    Dim i As Integer = 0
    Try
      Dim reader = command.ExecuteReader()
      Dim len = reader.FieldCount

      ' Create the DataTable columns
      For i = 0 To len - 1
        dttOut.Columns.Add(reader.GetName(i), reader.GetFieldType(i))
      Next

      dttOut.BeginLoadData()

      ' Add data rows
      While reader.Read()
        dttOut.Rows.Add(dttOut.NewRow)
        For i = 0 To len - 1
          dttOut.Rows(dttOut.Rows.Count - 1)(i) = reader(i)
        Next
      End While

      dttOut.EndLoadData()

      reader.Close()
      reader.Dispose()
    Catch ex As Exception
      Throw ex
    End Try
  End Sub


  Public Overridable Function Execute(ByVal strQuery As String, ByVal tipoDb As CLE__APP.DBTIPO, Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Dim nOk As Integer = 0
    Try
      nOk = Execute_P(strQuery, tipoDb, dbConn)
      If nOk <> -1 Then
        If Not oScriptE Is Nothing Then
          If strQuery.ToUpper.IndexOf("ACTLOG") = -1 Then
            'tipo trigger
            oScriptE.Exec("Execute", oApp, strQuery, dbConn)
            'Dim strace As New StackTrace(1, True)
            'System.Diagnostics.Debug
            'Dim frame As StackFrame = Nothing
            'Dim strTmp As String = strace.ToString
            'Dim strT() As String = strTmp.Split(vbCrLf)
            'strTmp = ""
            'For i As Integer = 0 To strT.Length - 1
            '  If strT(i).ToUpper.IndexOf("NTSINFORMATICA") > -1 Then
            '    frame = strace.GetFrame(i)
            '    If strT(i).IndexOf("NTSInformatica.MsgBoxNew") = -1 Then
            '      strTmp += strT(i) & vbCrLf ' & strace.GetFrame(i).GetFileLineNumber.ToString
            '    End If
            '    'Else
            '    'strTmp += ""
            '  End If
            'Next
          End If
        End If
      End If

      Return nOk
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return -1
    End Try
  End Function
  Private Function Execute_P(ByVal strQuery As String, ByVal tipoDb As CLE__APP.DBTIPO, Optional ByVal dbConn As DbConnection = Nothing) As Integer
    'restituisce -1 se non ha aggiornato nulla
    'se c'è un errore scatena una eccezione
    Dim factory As DbProviderFactory = Nothing
    Dim dbCmd As DbCommand = Nothing
    Dim bConn As Boolean = True
    Dim lResult As Integer
    Dim dtTmp As DateTime
    Dim strDbTipo As String = ""

    '---------------------------------------------------------
    'in base al tipo di database lancio la query di insert/update/delete
    '---------------------------------------------------------
    Try
      If Not dbConn Is Nothing Then bConn = False
      ScriviFile(strQuery, tipoDb, "Execute")
      dtTmp = DateTime.Now

      'apro il database
      If dbConn Is Nothing Then
        dbConn = ApriDB_P(tipoDb, factory)
      Else
        'factory = GetFactory(tipoDb)
      End If
      If tipoDb = CLE__APP.DBTIPO.DBAZI Then
        strDbTipo = oApp_P.Db.Tipo
      Else
        strDbTipo = oApp_P.DbAp.Tipo
      End If
      If strDbTipo = "SQLServer" Then
        strQuery = ParseSqlUnicode(strQuery, tipoDb = CLE__APP.DBTIPO.DBPRC)
        dbCmd = New SqlClient.SqlCommand
      Else
        If IOS Or ANDROID Then
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll").GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
        Else
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll").GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
        End If
      End If

      dbCmd.Connection = dbConn
      If IsInTrans Then dbCmd.Transaction = traDb
      Select Case tipoDb
        Case CLE__APP.DBTIPO.DBAZI
          dbCmd.CommandTimeout = oApp.Db.QueryTimeOut
        Case CLE__APP.DBTIPO.DBPRC
          dbCmd.CommandTimeout = oApp.DbAp.QueryTimeOut
      End Select

      If strDbTipo = "SQLServer" Then
        If SET_TRANSACTION_ISOLATION_LEVEL_READ_UNCOMMITTED Then
          dbCmd.CommandText = "SET TRANSACTION ISOLATION LEVEL read uncommitted"
          dbCmd.ExecuteNonQuery()
        End If
        dbCmd.CommandText = strQuery
      Else
        strQuery = CLN__STD.ParseSql(strQuery, 1)
        dbCmd.CommandText = strQuery
      End If

      lResult = dbCmd.ExecuteNonQuery()

      'chiudo il database
      If bConn And oApp.DBKeepAlive = False Then dbConn.Close()

      Dim dtTmp1 As DateTime = DateTime.Now
      Dim lMillisec As Integer = dtTmp1.Subtract(dtTmp).TotalMilliseconds
      ScriviFile("          TEMPO IMPIEGATO (in millisec): " & lMillisec.ToString & " - Record trattati: " & lResult.ToString, tipoDb, "Execute")

      Return lResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return -1
    Finally
      If bConn Then bConn = Nothing
      factory = Nothing
      dbCmd = Nothing
    End Try
  End Function

  Public Overridable Function ExecuteSP(ByRef sqlCmd As DbCommand, ByVal tipoDb As CLE__APP.DBTIPO, Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Dim nOk As Integer = 0
    Try

      nOk = ExecuteSP_P(sqlCmd, tipoDb, dbConn)
      If nOk <> -1 Then
        If Not oScriptE Is Nothing Then
          'tipo trigger
          oScriptE.Exec("ExecuteSP", oApp, sqlCmd, dbConn)
        End If
      End If

      Return nOk
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return -1
    End Try
  End Function
  Private Function ExecuteSP_P(ByRef sqlCmd As DbCommand, ByVal tipoDb As CLE__APP.DBTIPO, Optional ByVal dbConn As DbConnection = Nothing) As Integer
    'restituisce -1 se non ha aggiornato nulla
    'se c'è un errore scatena una eccezione
    Dim factory As DbProviderFactory = Nothing
    Dim lResult As Integer
    Dim strTmp As String = ""
    Dim i As Integer
    Dim bConn As Boolean = True
    Dim dtTmp As DateTime

    '---------------------------------------------------------
    'in base al tipo di database lancio la stored procedure
    '---------------------------------------------------------
    Try
      If Not dbConn Is Nothing Then bConn = False

      '---------------------------------------------------------
      'perparo il messaggio da salvare ne file di log
      '---------------------------------------------------------
      strTmp = sqlCmd.CommandText
      For i = 0 To sqlCmd.Parameters.Count - 1
        strTmp = strTmp & vbCrLf & "                    " & sqlCmd.Parameters(i).ParameterName & " = '" & sqlCmd.Parameters(i).Value & "'"
      Next

      ScriviFile(strTmp, tipoDb, "ExecuteSP")
      dtTmp = DateTime.Now

      'apro il database
      If dbConn Is Nothing Then
        dbConn = ApriDB_P(tipoDb, factory)
      Else
        factory = GetFactory(tipoDb)
      End If
      sqlCmd.Connection = dbConn
      If IsInTrans Then sqlCmd.Transaction = traDb

      Select Case tipoDb
        Case CLE__APP.DBTIPO.DBAZI
          sqlCmd.CommandTimeout = oApp.Db.QueryTimeOut
        Case CLE__APP.DBTIPO.DBPRC
          sqlCmd.CommandTimeout = oApp.DbAp.QueryTimeOut
      End Select

      lResult = sqlCmd.ExecuteNonQuery()
      'chiudo il database
      If bConn And oApp.DBKeepAlive = False Then dbConn.Close()

      Dim dtTmp1 As DateTime = DateTime.Now
      Dim lMillisec As Integer = dtTmp1.Subtract(dtTmp).TotalMilliseconds
      ScriviFile("          TEMPO IMPIEGATO (in millisec): " & lMillisec.ToString, tipoDb, "ExecuteSP")

      Return lResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strTmp, oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
      Return -1
    Finally
      If bConn Then bConn = Nothing
      factory = Nothing
    End Try
  End Function
  Public Overridable Function CreateParam(ByVal factory As DbProviderFactory, ByVal strParamName As String, _
                              ByVal oParamType As DbType, ByVal nParamLen As Integer, _
                              ByVal oParamValue As Object) As DbParameter
    CreateParam = factory.CreateParameter
    CreateParam.ParameterName = strParamName
    CreateParam.DbType = oParamType
    CreateParam.Size = nParamLen
    CreateParam.Value = oParamValue
  End Function

  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, ByRef ds As DataSet) As Boolean
    Try
      Return SetTableDefaultValueFromDB(strTableName, strTableName, ds, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, _
                                                         ByVal strDataTableName As String, ByRef ds As DataSet) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTableName, strTableName, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return SetTableDefaultValueFromDB(strTableName, strDataTableName, ds, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SetTableDefaultValueFromDB(ByVal strTableName As String, _
                                                         ByVal strDataTableName As String, _
                                                         ByRef ds As DataSet, ByVal bArcproc As Boolean) As Boolean
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

      Return oLbBase.SetTableDefaultValueFromDB(strTableName, strDataTableName, ds, bArcproc)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetTableStructure(ByVal strTabella As String, ByVal bArcproc As Boolean, ByRef dttOut As DataTable, Optional ByVal dbConn As DbConnection = Nothing) As Boolean

    Try
      Return oLbBase.GetTableStructure(strTabella, bArcproc, dttOut, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetTableDataDictionary(ByVal strTabella As String, ByVal bArcproc As Boolean, ByRef dttOut As DataTable) As Boolean
    'restituisce la struttura della tabella passata in input:
    'colonne, chiave primaria, ....
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim drRead As DbDataReader = Nothing
    Dim dbCmd As DbCommand = Nothing
    Dim factory As DbProviderFactory = Nothing
    Dim strDbTipo As String = ""

    Try
      strSQL = "SELECT TOP 0 * FROM " & strTabella
      If bArcproc = False Then
        dbConn = CType(ApriDB(CLE__APP.DBTIPO.DBAZI, factory), DbConnection)
        strDbTipo = oApp_P.Db.Tipo
      Else
        dbConn = CType(ApriDB(CLE__APP.DBTIPO.DBPRC, factory), DbConnection)
        strDbTipo = oApp_P.DbAp.Tipo
      End If

      If strDbTipo = "SQLServer" Then
        dbCmd = New SqlClient.SqlCommand
      Else
        If IOS Or ANDROID Then
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll").GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
        Else
          dbCmd = CType(Activator.CreateInstance(System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll").GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
        End If
        strSQL = CLN__STD.ParseSql(strSQL, 1)
      End If
      dbCmd.Connection = dbConn
      dbCmd.CommandText = strSQL

      If bArcproc = False Then
        dbCmd.CommandTimeout = oApp.Db.QueryTimeOut
      Else
        dbCmd.CommandTimeout = oApp.DbAp.QueryTimeOut
      End If

      drRead = dbCmd.ExecuteReader(CommandBehavior.KeyInfo)
      dttOut = drRead.GetSchemaTable()
      dttOut.TableName = "TABLE_STRUCT"
      dbConn.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function IsPerDitta(ByVal bPerDbAzienda As Boolean, ByVal strNomeTabella As String, _
                             Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.IsPerDitta(bPerDbAzienda, strNomeTabella, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsDbMultiDitta(ByRef strCoddittaunica As String) As Boolean
    Try
      Return oLbBase.IsDbMultiDitta(strCoddittaunica)
    Catch ex As Exception
      Throw (New ArgumentException())
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
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
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '---------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ValCodiceDb(ByVal strCodice As String, ByVal strCodditt As String, _
                              ByVal strNomeTabella As String, _
                              ByVal strTipoCod As String, ByRef strDescampo As String, _
                              ByRef dttTable As DataTable, ByVal strFilter1 As String, _
                              ByVal strFilter2 As String, ByVal strFilter3 As String, _
                              ByVal dbConn As DbConnection) As Boolean
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

      Return oLbBase.ValCodiceDb(strCodice, strCodditt, strNomeTabella, strTipoCod, strDescampo, dttTable, strFilter1, strFilter2, strFilter3, dbConn)
    Catch ex As Exception
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '---------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                  Optional ByVal strColonnaCounter As String = "") As String
    Try
      Return oLbBase.GetQueryInsertField(dttTabella, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                  ByVal strColonnaCounter As String, ByVal strAliasPrefissoCampo As String) As String
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

      Return oLbBase.GetQueryInsertField(dttTabella, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertField(ByRef dttTabella As DataTable, ByVal strPrefissoCampo As String, _
                                                  ByVal strColonnaCounter As String, ByVal strAliasPrefissoCampo As String, _
                                                  ByRef dttFieldAlias As DataTable) As String
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

      Return oLbBase.GetQueryInsertField(dttTabella, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, dttFieldAlias)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, Optional ByVal strColonnaCounter As String = "") As String
    Try
      Return oLbBase.GetQueryInsertValue(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String) As String
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

      Return oLbBase.GetQueryInsertValue(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryInsertValue(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String, ByRef dttFieldAlias As DataTable) As String
    Try

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

      Return oLbBase.GetQueryInsertValue(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, dttFieldAlias)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                  ByVal strPrefissoCampo As String, Optional ByVal strColonnaCounter As String = "") As String

    Try
      Return oLbBase.GetQueryUpdate(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, "", Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                              ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                              ByVal strAliasPrefissoCampo As String) As String
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

      Return oLbBase.GetQueryUpdate(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryUpdate(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                            ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                            ByVal strAliasPrefissoCampo As String, ByRef dttFieldAlias As DataTable) As String
    Try

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

      Return oLbBase.GetQueryUpdate(dttTabella, dtrRiga, strPrefissoCampo, strColonnaCounter, strAliasPrefissoCampo, dttFieldAlias)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dsOut As DataSet) As Boolean
    Try
      Return LeggiTabellaSemplice(strDitta, strNomeTabella, dsOut, False, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LeggiTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, _
                                                   ByRef dsOut As DataSet, ByVal bSoloCampiChiaveEDescr As Boolean, _
                                                   ByVal strWhere As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strNomeTabella, dsOut, bSoloCampiChiaveEDescr, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return oLbBase.LeggiTabellaSemplice(strDitta, strNomeTabella, dsOut, bSoloCampiChiaveEDescr, strWhere)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dttTable As DataTable, _
                                        ByVal strActLogProg As String, ByVal strActLogNomOggLog As String, _
                                        ByVal strActLogDesLog As String) As Boolean
    Try
      Return oLbBase.ScriviTabellaSemplice(strDitta, strNomeTabella, dttTable, strActLogProg, strActLogNomOggLog, strActLogDesLog)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, _
                                                    ByRef dttTable As DataTable, ByVal strActLogProg As String, _
                                                    ByVal strActLogNomOggLog As String, _
                                                    ByVal strActLogDesLog As String, ByRef dbConn As DbConnection) As Boolean
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

      Return oLbBase.ScriviTabellaSemplice(strDitta, strNomeTabella, dttTable, strActLogProg, strActLogNomOggLog, strActLogDesLog, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviActLog(ByVal strDitta As String, _
                             ByVal strNomprog As String, ByVal strNomogglog As String, _
                             ByVal strNomtab As String, ByVal strSqlwhere As String, _
                             ByVal strTipoop As String, ByVal strTipoogg As String, _
                             ByVal strDesogglog As String, ByVal fLocale As Boolean) As Boolean
    Try
      Return oLbBase.ScriviActLog(strDitta, strNomprog, strNomogglog, strNomtab, strSqlwhere, strTipoop, strTipoogg, strDesogglog, fLocale)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaCambioDiOggi(ByVal nCodVal As Integer, ByVal dtDataValidita As String) As Decimal
    Try
      Return oLbBase.CercaCambioDiOggi(nCodVal, dtDataValidita)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AggiornaCambio(ByVal nCodVal As Integer, ByVal dtDataValidita As String, ByVal dCambio As Decimal, _
                            ByVal bSovrascrivi As Boolean)
    Try
      oLbBase.AggiornaCambio(nCodVal, dtDataValidita, dCambio, bSovrascrivi)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function AliquotaIva(ByVal nCodiceIva As Integer) As Decimal
    Try
      Return oLbBase.AliquotaIva(nCodiceIva)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetEmailBusUser(ByVal strDitta As String, ByVal strUser As String) As String
    Try
      GetEmailBusUser = ""
      Return oLbBase.GetEmailBusUser(strDitta, strUser)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaIva(ByVal dImponibile As Decimal, ByVal CodiceIva As Integer, Optional ByVal nDec As Integer = 2) As Decimal
    Try
      Return oLbBase.CalcolaIva(dImponibile, CodiceIva, nDec)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Scorporo(ByVal dIvaInclusa As Decimal, ByVal nCodiceIva As Integer, ByRef dImponibile As Decimal, ByRef dImposta As Decimal, Optional ByVal nDec As Integer = -1)
    Try
      oLbBase.Scorporo(dIvaInclusa, nCodiceIva, dImponibile, dImposta, nDec)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ConvImpValuta(ByVal strDitta As String, ByVal bPrzun As Boolean, ByVal dImpval As Decimal, _
                                            ByVal nCodVal As Integer, ByVal dtDataValidita As Date, _
                                            ByVal dCambiout As Decimal, Optional ByRef bCancel As Boolean = False) As Decimal
    Try
      Return oLbBase.ConvImpValuta(strDitta, bPrzun, dImpval, nCodVal, dtDataValidita, dCambiout, bCancel)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvImpEur(ByVal bPrzun As Boolean, ByVal dImportoin As Decimal, ByVal nCodVal As Integer, _
                             ByVal dtDataValidita As DateTime, ByRef dCambiout As Decimal, _
                             Optional ByVal bCancel As Boolean = False) As Decimal
    Try
      Return oLbBase.ConvImpEur(bPrzun, dImportoin, nCodVal, dtDataValidita, dCambiout, bCancel)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaNdec(ByVal nCodvaluta As Integer) As Integer
    Try
      Return oLbBase.TrovaNdec(nCodvaluta)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaNdecSuPrzUn(ByVal nCodvalu As Integer) As Integer
    Return oLbBase.TrovaNdecSuPrzUn(nCodvalu)
  End Function

  Public Overridable Function TrovaContoCFDaPivaCodfisAnagen(ByVal strDitta As String, ByVal strTipoconto As String, _
                                          ByVal strPiva As String, ByVal strCodfisc As String, _
                                          ByVal lCodAnagen As Integer) As Integer

    Try
      Return oLbBase.TrovaContoCFDaPivaCodfisAnagen(strDitta, strTipoconto, strPiva, strCodfisc, lCodAnagen)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TrovaContoDaCodcontr(ByVal strCodditt As String, ByVal nCodcovg As Integer) As Integer
    Try
      Return oLbBase.TrovaContoDaCodcontr(strCodditt, nCodcovg)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TrovaContoDaContrEConto(ByVal strCodditt As String, ByVal nCodcovg As Integer, ByVal lConto As Integer) As Integer
    Try
      Return oLbBase.TrovaContoDaContrEConto(strCodditt, nCodcovg, lConto)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function LegAggRegcDitt(ByVal strCodditt As String, ByVal strData As String) As Integer
    Return LegAggRegcDitt(strCodditt, strData, Nothing)
  End Function
  Public Overridable Function LegAggRegcDitt(ByVal strCodditt As String, ByVal strData As String, _
                                             ByRef dbConn As DbConnection) As Integer
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

      Return oLbBase.LegAggRegcDitt(strCodditt, strData, dbConn)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function LegAngeDataRkAll(ByVal nValuein As Integer, ByVal dtDataval As Date, ByRef dttOut As DataTable) As Boolean
    Try
      Return oLbBase.LegAngeDataRkAll(nValuein, dtDataval, dttOut)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetTblInstId(ByVal strNomtab As String, ByVal bArcproc As Boolean) As Integer
    Try
      Return oLbBase.GetTblInstId(strNomtab, bArcproc, Nothing)
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

      Return oLbBase.GetTblInstId(strNomtab, bArcproc, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ResetTblInstId(ByVal strNomtab As String, ByVal bArcproc As Boolean, ByVal instid As Integer) As Boolean
    Try
      Return oLbBase.ResetTblInstId(strNomtab, bArcproc, instid)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function


  Public Overridable Function InvalidaLiqIVA(ByVal strDitta As String, ByVal strDatreg As String, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.InvalidaLiqIVA(strDitta, strDatreg, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function EsisteBudget(ByVal strCodditt As String, ByVal strTipork As String, _
                                           ByVal lConto As Integer, ByVal nCodcena As Integer, _
                                           ByVal strCodcfam As String, ByVal lCommeca As Integer, _
                                           ByVal strSubcommeca As String, ByVal nEscomp As Integer, _
                                           ByVal bTestBudget As Boolean) As Boolean
    Try
      Return oLbBase.EsisteBudget(strCodditt, strTipork, lConto, nCodcena, strCodcfam, lCommeca, strSubcommeca, nEscomp, bTestBudget)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RitornaTipoConto(ByVal lConto As Integer, ByVal strDitta As String) As String
    Try
      Return oLbBase.RitornaTipoConto(lConto, strDitta)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
      Return ""
    End Try
  End Function

  Public Overridable Function DataInEsercizio(ByVal strDitta As String, ByVal dtData As Date, Optional ByVal nAnno As Integer = -1) As Boolean
    Try
      Return oLbBase.DataInEsercizio(strDitta, dtData, nAnno)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#Region "sincronizzazione ANAGEN/ANAGRA/DESTGEN/TABANAZ/ ..."

  Public Overridable Function AggiornaAnagen(ByVal strDitta As String, ByVal strOrigine As String, _
                                            ByVal lCodanag As Integer, ByVal lConto As Integer, _
                                            Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.AggiornaAnagen(strDitta, strOrigine, lCodanag, lConto, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaAnagraDaAnagen(ByVal strOrigine As String, ByVal lCodanag As Integer, ByVal strDitta As String, _
                                            ByVal lConto As Integer, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.AggiornaAnagraDaAnagen(strOrigine, lCodanag, strDitta, lConto, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaTabanazDaAnagen(ByVal strOrigine As String, ByVal lCodanag As Integer, _
                                            ByVal strDitta As String, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.AggiornaTabanazDaAnagen(strOrigine, lCodanag, strDitta, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaLeadsDaAnagen(ByVal lCodanag As Integer, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.AggiornaLeadsDaAnagen(lCodanag, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
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
    Try
      Return oLbBase.RiempiTtanconf(INSTID, strFiltrodittegruppo)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CorreggiFiltroDitteGruppoPerQuery(ByVal strDitta As String, ByVal strFiltroDitteGruppo As String) As String
    Try
      Return oLbBase.CorreggiFiltroDitteGruppoPerQuery(strDitta, strFiltroDitteGruppo)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

  Public Overridable Function IsDestdivDeletable(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal lCoddest As Integer, ByRef strMsgOut As String) As Boolean

    Try
      Return oLbBase.IsDestdivDeletable(strDitta, lConto, lCoddest, strMsgOut)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function IsAnagDeletable(ByVal strDitta As String, ByVal lConto As Integer, _
                                              ByRef strMsgOut As String) As Boolean
    Try
      Return oLbBase.IsAnagDeletable(strDitta, lConto, strMsgOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsGescommUpdatable(ByVal strDitta As String, ByVal strCodart As String, ByVal bIsRoot As Boolean, _
                                                 ByVal strOldValue As String, ByVal strNewValue As String, ByRef strMsgOut As String) As Boolean
    Try
      Return oLbBase.IsGescommUpdatable(strDitta, strCodart, bIsRoot, strOldValue, strNewValue, strMsgOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaLeadDaConto(ByVal strDitta As String, ByVal strTipoconto As String, _
                                               ByVal lConto As Integer, ByVal lCoddest1 As Integer, _
                                               ByVal lModuliDittaDitt As Integer, _
                                               ByVal lModuliExtDittaDitt As Integer) As Integer
    Try
      Return oLbBase.CercaLeadDaConto(strDitta, strTipoconto, lConto, lCoddest1, lModuliDittaDitt, lModuliExtDittaDitt)

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
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoconto, lConto, lCoddest1, lModuliDittaDitt, _
                                             lModuliExtDittaDitt, lModuliSupDittaDitt})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '----------------

      Return oLbBase.CercaLeadDaConto(strDitta, strTipoconto, lConto, lCoddest1, lModuliDittaDitt, lModuliExtDittaDitt, lModuliSupDittaDitt)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsArtiDeletable(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByRef strMsgOut As String) As Boolean
    Try
      Return oLbBase.IsArtiDeletable(strDitta, strCodart, strMsgOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsCommecaDeletable(ByVal strDitta As String, ByVal lCommeca As Integer, _
                                            ByRef strMsgOut As String) As Boolean
    Try
      Return oLbBase.IsCommecaDeletable(strDitta, lCommeca, strMsgOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrganig(ByVal strDitta As String, ByVal lProgr As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oLbBase.GetOrganig(strDitta, lProgr, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetOrganigFromConto(ByVal strDitta As String, ByVal lConto As Integer, ByVal lCodlead As Integer, _
                                                  ByVal strRuolo As String, ByRef dttOut As DataTable) As Boolean
    Try
      Return oLbBase.GetOrganigFromConto(strDitta, lConto, lCodlead, strRuolo, dttOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrganigDestFromConto(ByVal strDitta As String, ByVal lConto As Integer, ByVal lCodlead As Integer, _
                                                      ByVal strRuolo As String, ByRef dttOut As DataTable, ByVal strCoddest As String) As Boolean
    Try
      Return oLbBase.GetOrganigDestFromConto(strDitta, lConto, lCodlead, strRuolo, dttOut, strCoddest)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TraduciWhere(ByVal strQuery As String, ByRef strSQL As String) As Boolean
    '-------------------------
    'data una strQuery dove and e or sono rimappati in modo diverso (vedi bnmgarti)
    'aggiunge a strSQL la strQuery tradotta
    Try

      Return oLbBase.TraduciWhere(strQuery, strSQL)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsDocRetail(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oLbBase.IsDocRetail(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsDocRetailNew(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oLbBase.IsDocRetailNew(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaUmMis(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim strSQL As String = ""
    Try
      Return oLbBase.CaricaUmMis(strDitta, dttOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EscompFromDate(ByVal strDitta As String, ByVal dtDate As Date, ByRef nEscomp As Integer, ByRef dttEscomp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Return oLbBase.EscompFromDate(strDitta, dtDate, nEscomp, dttEscomp)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function SubQueryFiltroLead_Private(ByVal strTipo As String, ByVal strDitta As String, ByVal strBusUser As String) As String
    'restituisce i lead abilitati in visualizzazione per l'operatore con cui ci si è loggati in business
    'per opportunità o offerte deve aggiungere la join con testoff o opportun
    SubQueryFiltroLead_Private = ""
    Dim strSQL As String = ""
    Try
      If strDitta = "" Then
        strSQL = " (SELECT DISTINCT leads1.codditt + '^' + cast(leads1.le_codlead as varchar(9)) "
      Else
        strSQL = " (SELECT DISTINCT leads1.le_codlead "
      End If
      strSQL += " FROM (((leads as leads1 INNER JOIN acclead AS acclead1 ON leads1.codditt = acclead1.codditt AND leads1.le_codlead = acclead1.opcr_codlead) " & _
               " INNER JOIN acccrm AS acccrm1 ON acclead1.opcr_opnome = acccrm1.opcr_alopnome AND acclead1.codditt = acccrm1.codditt) " & _
               " INNER JOIN accdito AS accdito1 ON acccrm1.codditt = accdito1.codditt AND acccrm1.opcr_opnome = accdito1.opdi_opnome)"

      strSQL += " WHERE acccrm1.opcr_opnome = " & CStrSQL(strBusUser) & _
                " AND accdito1.opdi_abilit = 'S' "
      If strDitta <> "" Then strSQL += " AND leads1.codditt = " & CStrSQL(strDitta)

      If strTipo = "V" Then
        strSQL += " AND accdito1.opdi_crmvis = 'S' " & _
                 " AND acclead1.opcr_crmvis = 'S' " & _
                 " AND acccrm1.opcr_crmvis = 'S' "
      End If
      If strTipo = "M" Then
        strSQL += " AND accdito1.opdi_crmmod = 'S' " & _
                 " AND acclead1.opcr_crmmod = 'S' " & _
                 " AND acccrm1.opcr_crmmod = 'S' "
      End If

      strSQL += ") "

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SubQueryFiltroLeadVis(ByVal strDitta As String) As String
    'restituisce i lead abilitati in visualizzazione per l'operatore con cui ci si è loggati in business
    SubQueryFiltroLeadVis = ""
    Dim strSQL As String = ""
    Try
      strSQL = SubQueryFiltroLead_Private("V", strDitta, oApp.User.Nome)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SubQueryFiltroLeadVis(ByVal strDitta As String, ByVal strBusUser As String) As String
    'restituisce i lead abilitati in visualizzazione per l'operatore con cui ci si è loggati in business
    SubQueryFiltroLeadVis = ""
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strBusUser})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      If strBusUser = "" Then strBusUser = oApp.User.Nome
      strSQL = SubQueryFiltroLead_Private("V", strDitta, strBusUser)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SubQueryFiltroLeadMod(ByVal strDitta As String) As String
    'restituisce i lead abilitati in visualizzazione per l'operatore con cui ci si è loggati in business
    SubQueryFiltroLeadMod = ""
    Dim strSQL As String = ""
    Try
      strSQL = SubQueryFiltroLead_Private("M", strDitta, oApp.User.Nome)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SubQueryFiltroLeadMod(ByVal strDitta As String, ByVal strBusUser As String) As String
    'restituisce i lead abilitati in visualizzazione per l'operatore con cui ci si è loggati in business
    SubQueryFiltroLeadMod = ""
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strBusUser})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      If strBusUser = "" Then strBusUser = oApp.User.Nome
      strSQL = SubQueryFiltroLead_Private("M", strDitta, strBusUser)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetBusRelease(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Return oLbBase.GetBusRelease(dttOut)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function DocumentLockAdd(ByVal strDitta As String, ByVal dtControllaConcorrenzaOggettiDataLock As DateTime, _
                                         ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                         ByVal lNumdoc As Integer, ByVal lVers As Integer) As Boolean

    Dim strSQL As String = ""
    Try
      Return oLbBase.DocumentLockAdd(strDitta, dtControllaConcorrenzaOggettiDataLock, strTipork, nAnno, strSerie, lNumdoc, lVers)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DocumentLockRemove(ByVal strDitta As String, ByVal dtControllaConcorrenzaOggettiDataLock As DateTime) As Boolean
    Dim strSQL As String = ""
    Try
      Return oLbBase.DocumentLockRemove(strDitta, dtControllaConcorrenzaOggettiDataLock)

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
    Try
      Return oLbBase.DocumentLockCheck(strDitta, dtControllaConcorrenzaOggettiDataLock, strTipork, nAnno, strSerie, lNumdoc, lVers, strMsgOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function DBCacheAggDBCacheDate(ByVal strTabella As String) As Boolean
    Try
      Return oLbBase.DBCacheAggDBCacheDate(strTabella)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Scrittura di priana2 e pria2cop da TESTMAG-MOVMAG"
  Public Overridable Function ScriviPriana2DaTestmag(ByVal strDitta As String, ByVal strTipork As String, _
                                                 ByVal nAnno As Integer, ByVal strSerie As String, _
                                                 ByVal lNumdoc As Integer, ByVal strDatdoc As String, _
                                                 ByRef dbConn As DbConnection, ByVal lIITtkeys As Integer, _
                                                 ByVal bRiscriviSoloScarichieQtaProd As Boolean) As Boolean
    Try
      Return oLbBase.ScriviPriana2DaTestmag(strDitta, strTipork, nAnno, strSerie, lNumdoc, strDatdoc, dbConn, lIITtkeys, bRiscriviSoloScarichieQtaProd, False, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviPriana2DaTestmag(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByVal strDatdoc As String, _
                                                   ByRef dbConn As DbConnection, ByVal lIITtkeys As Integer, _
                                                   ByVal bRiscriviSoloScarichieQtaProd As Boolean, _
                                                   ByVal bEscludiLavorazTerziste As Boolean, _
                                                   ByVal bEscludiSpesePiede As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, lNumdoc, strDatdoc, dbConn, _
                                             lIITtkeys, bRiscriviSoloScarichieQtaProd, bEscludiLavorazTerziste, _
                                             bEscludiSpesePiede})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(6), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      Return oLbBase.ScriviPriana2DaTestmag(strDitta, strTipork, nAnno, strSerie, lNumdoc, strDatdoc, dbConn, lIITtkeys, bRiscriviSoloScarichieQtaProd, bEscludiLavorazTerziste, bEscludiSpesePiede)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviPriana2DaTestmag_Lavorazioni(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByRef dbConn As DbConnection, _
                                                   ByVal lIITtkeys As Integer, ByVal nEscomp As Integer, _
                                                   ByVal bRiscriviSoloScarichieQtaProd As Boolean, _
                                                   ByVal bEscludiLavorazTerziste As Boolean) As Boolean
    Try
      Return oLbBase.ScriviPriana2DaTestmag_Lavorazioni(strDitta, strTipork, nAnno, strSerie, lNumdoc, dbConn, lIITtkeys, nEscomp, bRiscriviSoloScarichieQtaProd, bEscludiLavorazTerziste)


    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviPriana2DaTestmag_SpesePiede(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal nAnno As Integer, ByVal strSerie As String, _
                                                   ByVal lNumdoc As Integer, ByRef dbConn As DbConnection, _
                                                   ByVal lIITtkeys As Integer, ByVal nEscomp As Integer) As Boolean
    Try
      Return oLbBase.ScriviPriana2DaTestmag_SpesePiede(strDitta, strTipork, nAnno, strSerie, lNumdoc, dbConn, lIITtkeys, nEscomp)

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
    '-----------------------------------------
    'calcolo le colonne di tipo 'totale': ottengo le righe dello schema budget in ordine corretto,
    'poi memorizzo le righe fino al totalizzatore, per ogni totalizzatore faccio una sum con tt_riga in (....) 
    'poi faccio update di riga totalizzatore.
    'il tutto solo sui totali (tt_conto = 0)
    Try
      Return oLbBase.AggTotaliBudgetCa2(strDitta, strTipork, nCodgrua, nEscompg, nRev, nCodtcdc, strCoddica, strFiltroBudget)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaContrda(ByVal strCodditt As String, ByVal strNomprog As String, _
                                         ByVal strTipork As String, ByVal nMagimp As Integer, _
                                         ByVal lConto As Long, ByVal nCodcent As Integer, _
                                         ByVal strCodart As String, ByVal nCodcaum As Integer, _
                                         ByVal nFase As Integer, ByVal strDatdoc As String, _
                                         ByVal bCollaudiSuDocEmessi As Boolean, _
                                         ByRef nCodtcon As Integer, ByRef nCodpcam As Integer, _
                                         ByRef nCodclaq As Integer, ByRef nCodlivq As Integer, _
                                         Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    'GESTIONE COLLAUDI: VERIFICA SE L'ARTICOLO DEVE ESSERE COLLAUDATO
    'se restituisce TRUE l'articolo deve essere collaudato

    ' ritorna gli ultimi 4 parametri obblgatori, e poi ritorna true se ha trovatoqualcosa, false in caso contrario
    ' tipork e magimp servono per identificare se trattasi di ricevimento mat,
    ' oppure versamento e su che tipo di magazzino è avvenuta la produzione, interno o estermo
    Try
      Return oLbBase.CercaContrda(strCodditt, strNomprog, strTipork, nMagimp, lConto, nCodcent, _
                                  strCodart, nCodcaum, nFase, strDatdoc, bCollaudiSuDocEmessi, _
                                  nCodtcon, nCodpcam, nCodclaq, nCodlivq, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Project Management"
  Public Overridable Function Caricattproesebappo(ByVal strDitta As String, ByVal lIIttproesebappo As Integer, _
                                                  ByVal lCommeca As Integer, ByVal lTaskid As Integer, _
                                                  ByRef dbConn As DbConnection) As Boolean
    '--- dato commessa task creo copiandoli i record di ttproesec da proeseb, se non ci sono già;
    ' se task di tipo F copia anche i record di proeseb dei fratelli
    Try
      Return oLbBase.Caricattproesebappo(strDitta, lIIttproesebappo, lCommeca, lTaskid, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaPriana(ByVal strDitta As String, ByVal strNometabProesec As String, _
                                                 ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, _
                                                 ByVal lPaprogr As Integer) As Integer
    Try
      Return oLbBase.TrovaEsecIdDaPriana(strDitta, strNometabProesec, lIITtproesec, lCommeca, lPaprogr)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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
                                                  ByRef strPrevGrup As String, Optional ByVal bDaordini As Boolean = False, _
                                                  Optional ByRef strRilasciato As String = "") As Boolean
    Try

      Return oLbBase.LegTasksCompleteDao(strDitta, strNometabProesec, lIITtproesec, lIItttasks, lCommeca, lTaskid, _
                                         lEsecidDaEscludere, strFinoal, bControllaBaseCost, strDestask, _
                                         strDaraveout, strTipoTask, strGestcost, dQtares, dWorkres, dCostores, _
                                         dBasecost, dBasecostun, dBasework, dBaseqta, strPrevGrup, bDaordini, _
                                         strRilasciato)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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
    Try

      Return oLbBase.AggTasksCompleteDao(strDitta, strNomtabin, lIIin, strNomtabout, lIIout, lCommeca, _
                                         lTaskid, lEsecidDaEscludere, bDallinizio, bVissforbudg, _
                                         bAbortsforbudg, strFinoal, dbConn, nErrcode, dRettqta, dRettwork, _
                                         dRettcost)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggTasksDao(ByVal strDitta As String, ByVal strNomtabin As String, ByVal lIIin As Integer, _
                                          ByVal strNomtabout As String, ByVal lIIout As Integer, ByVal lCommeca As Integer, _
                                          ByVal lEsecid As Integer, ByVal bDallinizio As Boolean, _
                                          ByVal bVissforbudg As Boolean, ByVal bAbortsforbudg As Boolean, _
                                          ByRef dbConn As DbConnection, ByVal nErrcode As Integer) As Boolean
    Try
      Return oLbBase.AggTasksDao(strDitta, strNomtabin, lIIin, strNomtabout, lIIout, lCommeca, _
                                 lEsecid, bDallinizio, bVissforbudg, bAbortsforbudg, dbConn, nErrcode)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LegTaskGr(ByVal strDitta As String, ByVal strCommecain As String, _
                                        ByVal strTaskidin As String, ByVal strNometabin As String, _
                                        ByVal lInstidin As Integer, ByVal nObbl As Boolean, _
                                        ByRef strValueout As String, ByRef strDaraveout As String, _
                                        ByRef strTipoTask As String, ByRef strGestcost As String, _
                                        ByRef strPrevGrup As String, ByRef strSummary As String, _
                                        ByRef dbConn As DbConnection) As Boolean
    Try
      Return oLbBase.LegTaskGr(strDitta, strCommecain, strTaskidin, strNometabin, lInstidin, nObbl, _
                               strValueout, strDaraveout, strTipoTask, strGestcost, strPrevGrup, _
                               strSummary, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LegGestCostPadreDao(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskid As Integer) As String
    LegGestCostPadreDao = "2"
    Try
      Return oLbBase.LegGestCostPadreDao(strDitta, lCommeca, lTaskid)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LegTaskRk(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskID As Integer) As Boolean
    Try
      Return oLbBase.LegTaskRk(strDitta, lCommeca, lTaskID)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ControllaSalconFratelliDao(ByVal strDitta As String, ByVal strNomtabttpr As String, _
                                              ByVal lIIttproesebappo As Integer, ByVal strNomtabtttasks As String, _
                                              ByVal lIItttasks As Integer, ByVal lCommeca As Integer, _
                                              ByVal lTaskid As Integer, ByVal bDaordini As Boolean, _
                                              ByRef strMess As String, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try

      Return oLbBase.ControllaSalconFratelliDao(strDitta, strNomtabttpr, lIIttproesebappo, strNomtabtttasks, _
                                               lIItttasks, lCommeca, lTaskid, bDaordini, strMess, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggProesebDaPrianaDao(ByVal strDitta As String, ByVal nOperaz As Integer, _
                                                    ByVal strNomtabTT As String, ByVal lII As Integer, _
                                                    ByVal strNomtabPes As String, ByVal lIIpes As Integer, _
                                                    ByVal lPaprogr As Integer, ByVal dbconn As DbConnection, _
                                                    ByRef dttPriana As DataTable) As Boolean
    Try

      Return oLbBase.AggProesebDaPrianaDao(strDitta, nOperaz, strNomtabTT, lII, strNomtabPes, lIIpes, _
                                           lPaprogr, dbconn, dttPriana)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggProeseb(ByVal strChiamante As String, ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                   ByVal strDatreg As String, ByVal lNumreg As Integer, _
                                   ByRef dttPriana As DataTable, ByVal strState As String) As Boolean
    Try
      Return oLbBase.AggProeseb(strChiamante, dbConn, strDitta, strDatreg, lNumreg, dttPriana, strState)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaMovmag(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer) As Integer
    Try
      Return oLbBase.TrovaEsecIdDaMovmag(strDitta, strNometabProesec, lIITtproesec, lCommeca, _
                                         strTipork, nAnno, strSerie, lNumdoc, lMmriga)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaMovord(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer) As Integer
    Try
      Return oLbBase.TrovaEsecIdDaMovord(strDitta, strNometabProesec, lIITtproesec, lCommeca, _
                                         strTipork, nAnno, strSerie, lNumdoc, lMmriga)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaEsecIdDaLavcent(ByVal strDitta As String, ByVal strNometabProesec As String, ByVal lIITtproesec As Integer, ByVal lCommeca As Integer, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lMmriga As Integer, ByVal lMmrigaa As Integer) As Integer
    Try
      Return oLbBase.TrovaEsecIdDaLavcent(strDitta, strNometabProesec, lIITtproesec, lCommeca, _
                                         strTipork, nAnno, strSerie, lNumdoc, lMmriga, lMmrigaa)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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
    Try
      Return oLbBase.AggProesebDaMovmagDao(strDitta, nOperaz, strNomtabTT, lII, strNomtabPes, lIIpes, _
                                           strTipork, nAnno, strSerie, lNumdoc, lRiga, strDatdoc, _
                                           dbConn, dttMMtrans)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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
    Try
      Return oLbBase.AggProesebDaMovordDao(strDitta, nOperaz, strNomtabTT, lII, strNomtabPes, lIIpes, _
                                           strTipork, nAnno, strSerie, lNumdoc, lRiga, strDatdoc, _
                                           dbConn, dttMOtrans)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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
    Try
      Return oLbBase.AggProesebDaLavcentDao(strDitta, nOperaz, strNomtabTT, lII, strNomtabPes, lIIpes, _
                                           strTipork, nAnno, strSerie, lNumdoc, lRiga, lRigaa, strDatdoc, _
                                           dbConn, dttMMlavce)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Net 2012 - determinazione lotto numerico da lotto alfanum"
  Public Overridable Function LottoxCreaID(ByVal strDitta As String, ByRef lId As Integer, _
                                          Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.LottoxCreaID(strDitta, lId, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LottoxGetID(ByVal strDitta As String, ByVal strCodart As String, _
                                          ByVal strLottox As String, ByRef lId As Integer, _
                                          Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      Return oLbBase.LottoxGetID(strDitta, strCodart, strLottox, lId, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LottoxCheckLottoUnivoco(ByVal strDitta As String, ByVal strCodart As String, _
                                                      ByVal strLottox As String, ByRef strCodartOut As String) As Boolean
    'restituisce false ed il cod. lotto in cui il lottox è stato utilizzato
    LottoxCheckLottoUnivoco = True
    Try
      Return oLbBase.LottoxCheckLottoUnivoco(strDitta, strCodart, strLottox, strCodartOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function ScadenAggDtprevincpag(ByVal strDitta As String, ByVal bScadenec As Boolean, _
                                                  ByVal strDatascadDa As String, ByVal strDatascadA As String, _
                                                  ByVal strDataDocDa As String, ByVal strDataDocA As String, _
                                                  ByVal lAnagenDa As Integer, ByVal lAnagenA As Integer, _
                                                  ByVal lContoDa As Integer, ByVal lContoA As Integer, _
                                                  ByVal strTipoScad As String) As Boolean
    Try
      'strTipoScad  = integrativa si/no/entrambe

      'TESORERIA
      'aggiorna la data di incasso prevista a partire dalla data scadenza + giorni di ritardo contenuti in clitpsg o tabrtst o tabstpg
      'può lavorare sia su scaden che su scadenec

      Return oLbBase.ScadenAggDtprevincpag(strDitta, bScadenec, strDatascadDa, strDatascadA, strDataDocDa, _
                                           strDataDocA, lAnagenDa, lAnagenA, lContoDa, lContoA, strTipoScad)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataAgendaSTD(ByVal strDitta As String, ByVal strDataStart As String, _
                                               ByVal strDataEnd As String, ByVal strBusUser As String, _
                                               ByVal bDaAgenda As Boolean, _
                                               ByRef bDaeseguire As Boolean, ByRef bEseguito As Boolean, _
                                               ByRef bAnnullato As Boolean, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oLbBase.GetDataAgendaSTD(strDitta, strDataStart, strDataEnd, strBusUser, bDaAgenda, _
        bDaeseguire, bEseguito, bAnnullato, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RicalcolaCommessa(ByVal lInstidTTTasks0 As Integer, _
                                                ByVal lComm As Integer, _
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
      RicalcolaCommessa(lInstidTTTasks0, lComm, lInstidTTprevent, dCostototalecommessa, _
        dPrezzototalecommessa, dPrezzoconcordatocommessa, dCostoeffettivocommessa, dCostorimanentecommessa, _
        dCostoprogrammatocommessa, dCostodifferenzecommessa, dCostototaleeffettivocommessa, _
        dCostototalerimanentecommessa, dCostototaleprogrammatocommessa, dCostototaledifferenzecommessa, _
        dCostoindirettototalecommessa, dCostoindirettoeffettivocommessa, dCostoindirettoprogrammatocommessa, _
        dRicavodefinitivoprevistocommessa, dRicavodefinitivoeffettivocommessa, dRicavodefinitivorimanentecommessa, _
        dRicavodefinitivodifferenzecommessa, dRicavoaccontoprevistocommessa, dRicavoaccontoeffettivocommessa, _
        dRicavoaccontorimanentecommessa, dRicavoaccontodifferenzecommessa, strDitta, Nothing)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function RicalcolaCommessa(ByVal lInstidTTTasks0 As Integer, _
                                                ByVal lComm As Integer, _
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
    Try
      Return oLbBase.RicalcolaCommessa(lInstidTTTasks0, lComm, lInstidTTprevent, dCostototalecommessa, _
        dPrezzototalecommessa, dPrezzoconcordatocommessa, dCostoeffettivocommessa, dCostorimanentecommessa, _
        dCostoprogrammatocommessa, dCostodifferenzecommessa, dCostototaleeffettivocommessa, _
        dCostototalerimanentecommessa, dCostototaleprogrammatocommessa, dCostototaledifferenzecommessa, _
        dCostoindirettototalecommessa, dCostoindirettoeffettivocommessa, dCostoindirettoprogrammatocommessa, _
        dRicavodefinitivoprevistocommessa, dRicavodefinitivoeffettivocommessa, dRicavodefinitivorimanentecommessa, _
        dRicavodefinitivodifferenzecommessa, dRicavoaccontoprevistocommessa, dRicavoaccontoeffettivocommessa, _
        dRicavoaccontorimanentecommessa, dRicavoaccontodifferenzecommessa, strDitta, dbConn)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function AggiornaPadri(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                            ByVal lInstidTTprevent As Integer, ByVal strDitta As String) As Boolean
    Try
      oLbBase.AggiornaPadri(lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, Nothing)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function AggiornaPadri(ByVal lInstidTTTasks0 As Integer, ByVal lComm As Integer, _
                                            ByVal lInstidTTprevent As Integer, ByVal strDitta As String, _
                                            ByRef dbConn As DbConnection) As Boolean
    Try
      Return oLbBase.AggiornaPadri(lInstidTTTasks0, lComm, lInstidTTprevent, strDitta, dbConn)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Function ParseSqlUnicode(ByVal strIn As String, ByVal bArcproc As Boolean) As String
    ParseSqlUnicode = strIn
    Try
      If bArcproc Then
        If oApp.DbAp.SQLUnicode = False Then Return strIn
      Else
        If oApp.Db.SQLUnicode = False Then Return strIn
      End If
      Dim nStart As Integer = 0
      Dim l As Integer = 0
      Dim n As Integer = -1
      Dim nLastUnicode As Integer = -1

RIPARTI:
      For n = nStart To strIn.Length - 1
        'converto la stringa in per nvarchar
        'use the AscW() and ChrW() functions:
        If AscW(strIn(n)) > 127 Then
          'trovato carattere unicode/asci esteso: torno indietro fino a trovare l'apice di apertura della stringa
          If n = nLastUnicode + 1 Then
            'sono all'interno di una stringa unicode ed ho già messo la N processando il primo carattere unicode: vado avanti
            nLastUnicode = n
            Continue For
          End If

          nLastUnicode = n
          l = n - 1
CERCA:
          While strIn(l) <> "'"c
            l -= 1
          End While
          If l > 0 AndAlso strIn(l - 1) = "'"c Then
            'è un doppio apice che nella query verrà tradotto come apice semplice
            l -= 2
            GoTo CERCA
          End If

          'ho trovato l'inizio della stringa: antepongo la N (se non già fatto in precedenza)
          If strIn(l - 1) <> "N"c Then
            strIn = strIn.Substring(0, l) & "N" & strIn.Substring(l)
            nStart = n + 1
            GoTo RIPARTI
          Else
            Continue For
          End If
        End If
      Next

      Return strIn

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

#Region "Net@Pro"
  Public Overridable Function SalvaOrdineAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef strErr As String, _
                                                 ByRef dbConn As DbConnection) As Boolean
    Try
      Return oLbBase.SalvaOrdineAggNetPro(ds, strState, strNetProDB, strErr, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaOrdineAggNetPro_CaricaOrDaMovmag(ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                                                    ByRef dttOrd As DataTable, ByRef dttOrdImp As DataTable, _
                                                                    ByRef dttOrdAtt As DataTable, ByRef dttOrdAss As DataTable) As Boolean
    Try
      Return oLbBase.SalvaOrdineAggNetPro_CaricaOrDaMovmag(ds, dbConn, dttOrd, dttOrdImp, dttOrdAtt, dttOrdAss)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteDocAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef dbConn As DbConnection) As Boolean
    Try
      Return oLbBase.DeleteDocAggNetPro(ds, strState, strNetProDB, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetStatusEXEOrdineNetpro(ByVal strDitta As String, ByVal strTipork As String, _
                                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                                      ByVal lNumord As Integer, ByVal nRiga As Integer) As Boolean
    Try
      Return oLbBase.GetStatusEXEOrdineNetpro(strDitta, strTipork, nAnno, strSerie, lNumord, nRiga)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function NetProCallSocket(ByVal strDitta As String, ByVal strOperaz As String, ByVal strBatch As String) As Boolean
    Try
      Return oLbBase.NetProCallSocket(strDitta, strOperaz, strBatch)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region


  Public Overridable Function RitornaConfigurazioneEmailDef(ByVal strTipo As String, ByVal strTipoRk As String, ByVal strSerie As String) As DataTable
    Dim strSQL As String = ""
    Try
      'la serie '*' e il tipo 'E' ha meno priorità rispetto ai dati specifici.
      strSQL = "SELECT TOP 1 * FROM emaildef " & _
               " WHERE em_tipo IN ('E', " & CStrSQL(strTipo) & ")" & _
               "   AND em_tipork = " & CStrSQL(strTipoRk) & _
               "   AND em_serie IN ('*', " & CStrSQL(strSerie) & ")" & _
               " ORDER BY CASE WHEN em_serie = '*' THEN 1 ELSE 0 END, CASE WHEN em_tipo = 'E' THEN 1 ELSE 0 END "

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

#Region "Dichiarazioni di intento"

  Public Overridable Function IsSoggettoconDichiarazioni(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Try
      Return oLbBase.IsSoggettoconDichiarazioni(strDitta, lConto)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDichint(ByVal strDitta As String, ByVal lConto As Integer, ByVal strTipo As String, ByVal bChiuse As Boolean, ByVal bRevocate As Boolean, ByVal bValide As Boolean, ByVal strDatVal As String, ByRef dsHldi As DataSet) As Boolean
    Try
      Return oLbBase.GetDichint(strDitta, lConto, strTipo, bChiuse, bRevocate, bValide, strDatVal, dsHldi)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CollegaMGDI(ByVal strDitta As String) As Boolean
    Try
      Return oLbBase.CollegaMGDI(strDitta)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function LeggiDatiPlafondDich(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, _
                                                  ByVal nNumero As Integer, ByVal bCollega_MG_DI As Boolean, _
                                                  ByRef bNoPlafondDic As Boolean, ByRef ds As DataSet) As Boolean
    Try
      Return oLbBase.LeggiDatiPlafondDich(strDitta, strTipo, nAnno, nNumero, bCollega_MG_DI, _
                                          bNoPlafondDic, ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function CheckCausaleSplitPaymentFromTpbf(ByVal strDitta As String, ByVal nCodtpbf As Integer) As Boolean
    Try
      Return oLbBase.CheckCausaleSplitPaymentFromTpbf(strDitta, nCodtpbf)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCoerenzaTaglieQtaTCO(ByVal strDitta As String, ByRef strErr As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oLbBase.CheckCoerenzaTaglieQtaTCO(strDitta, strErr)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function CheckCoerenzaTaglieQtaTCOPeriodo(ByVal strDitta As String, ByVal dtInizio As Date, ByVal dtFine As Date, _
                                                              ByRef strErr As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oLbBase.CheckCoerenzaTaglieQtaTCOPeriodo(strDitta, dtInizio, dtFine, strErr)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp_P.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CheckInitGlobali() As Boolean
    'Controlla che i campi destinazioni predefinite in inizializzazioni globali siano valorizzati
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lDestdomf As Integer
    Dim lDestsedel As Integer
    Dim lDestresan As Integer
    Dim lDestcorr As Integer
    Try
      ValCodiceDb("1", "", "TABINSG", "N", "", dttTmp)
      lDestdomf = NTSCInt(dttTmp.Rows(0)!tb_destdomf)
      lDestsedel = NTSCInt(dttTmp.Rows(0)!tb_destsedel)
      lDestresan = NTSCInt(dttTmp.Rows(0)!tb_destresan)
      lDestcorr = NTSCInt(dttTmp.Rows(0)!tb_destcorr)

      If lDestdomf = 0 Or lDestsedel = 0 Or lDestresan = 0 Or lDestcorr = 0 Then
        Return False
      Else
        Return True
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

End Class
