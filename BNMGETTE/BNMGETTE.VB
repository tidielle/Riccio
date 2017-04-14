Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGETTE
#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

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
#End Region
#Region "Variabili"
  Public oCleEtte As CLEMGETTE
  Public oCallParams As CLE__CLDP
  Public dsEtte As DataSet
  Public dcEtte As BindingSource = New BindingSource
  Public lIIstMatr As Integer = -1
  Public lIIstMats As Integer = -1
  Public Const bsDbAzienda As Boolean = False
  Public strWhereArtico As String = ""
  Public bModTCO As Boolean = False
  Public bBD_DaCodarfo As Boolean
  Public nBD_Listino As Integer
  Public bUsaTtstmatr As Boolean
  Public bStpTerm As Boolean
  Public bDometic As Boolean
  Public bNEtichetteBarCode As Boolean
  Public bStampaNegativi As Boolean
  Public bDividiQuantConfez As Boolean
  Public bCreaBarcode As Boolean
  Public lCreaBarcodeTipo As Integer
  Public strWhere As String = ""
  Public bFiltri As Boolean
  '--- Passati da altra form
  Public bDaArticoli As Boolean = False
  Public strArticoli As String = ""
  Public lQuantita As Integer = 0
  Public bDaDocumenti As Boolean = False
  Public strTipoDocu As String = ""
  Public nAnnoDocu As Integer = 0
  Public strSerieDocu As String = ""
  Public lNumDocu As Integer = 0
  Public lRigaDocu As Integer = 0
  Public bDaOrdini As Boolean = False
  '--- Fine passati
  Public bLoaded As Boolean = False
  Public lQtaTot As Integer
  Private components As System.ComponentModel.IContainer

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmOrigine As NTSInformatica.NTSGroupBox
  Public WithEvents cbOrigine As NTSInformatica.NTSComboBox
  Public WithEvents pnDoc As NTSInformatica.NTSPanel
  Public WithEvents edAanno As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaanno As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdata As NTSInformatica.NTSTextBoxData
  Public WithEvents edDadata As NTSInformatica.NTSTextBoxData
  Public WithEvents edAclie As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaclie As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents lbAriga As NTSInformatica.NTSLabel
  Public WithEvents lbDariga As NTSInformatica.NTSLabel
  Public WithEvents lbAnumero As NTSInformatica.NTSLabel
  Public WithEvents lbAserie As NTSInformatica.NTSLabel
  Public WithEvents lbDanumero As NTSInformatica.NTSLabel
  Public WithEvents lbDaserie As NTSInformatica.NTSLabel
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents lbAclie As NTSInformatica.NTSLabel
  Public WithEvents lbAanno As NTSInformatica.NTSLabel
  Public WithEvents lbDaanno As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents lbDaclie As NTSInformatica.NTSLabel
  Public WithEvents lbTipodoc As NTSInformatica.NTSLabel
  Public WithEvents edAriga As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDariga As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnumero As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDanumero As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAserie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDaserie As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnArtico As NTSInformatica.NTSPanel
  Public WithEvents cmdArtico As NTSInformatica.NTSButton
  Public WithEvents pnFiltri As NTSInformatica.NTSPanel
  Public WithEvents lbBarcode As NTSInformatica.NTSLabel
  Public WithEvents lbEtiche As NTSInformatica.NTSLabel
  Public WithEvents cbBarcode As NTSInformatica.NTSComboBox
  Public WithEvents edEtiche As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnLista As NTSInformatica.NTSPanel
  Public WithEvents lbLista As NTSInformatica.NTSLabel
  Public WithEvents edLista As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckNumeti As NTSInformatica.NTSCheckBox
  Public WithEvents lbDesclista As NTSInformatica.NTSLabel
  Public WithEvents lbA As NTSInformatica.NTSLabel
  Public WithEvents lbDa As NTSInformatica.NTSLabel
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents edDatamod As NTSInformatica.NTSTextBoxData
  Public WithEvents edValida As NTSInformatica.NTSTextBoxData
  Public WithEvents edValuta As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValida As NTSInformatica.NTSLabel
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents ckPrezzi As NTSInformatica.NTSCheckBox
  Public WithEvents ckDatamod As NTSInformatica.NTSCheckBox
  Public WithEvents ckValuta As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipostp As NTSInformatica.NTSComboBox
  Public WithEvents lbTipostp As NTSInformatica.NTSLabel
  Public WithEvents cbSolobar As NTSInformatica.NTSComboBox
  Public WithEvents ckBarcode As NTSInformatica.NTSCheckBox
  Public WithEvents ckTermica As NTSInformatica.NTSCheckBox
  Public WithEvents ckArtbar As NTSInformatica.NTSCheckBox
  Public WithEvents opQta As NTSInformatica.NTSRadioButton
  Public WithEvents opColli As NTSInformatica.NTSRadioButton
  Public WithEvents lbNumeti As NTSInformatica.NTSLabel
  Public WithEvents fmStampe As NTSInformatica.NTSGroupBox
  Public WithEvents ckStampe As NTSInformatica.NTSCheckBox
  Public WithEvents ckMisura As NTSInformatica.NTSCheckBox
  Public WithEvents edNumMolt As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPer As NTSInformatica.NTSLabel
  Public WithEvents cbOperazione As NTSInformatica.NTSComboBox
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents edTaglia As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTaglia As NTSInformatica.NTSLabel
  Public WithEvents lbDescvalu As NTSInformatica.NTSLabel
  Public WithEvents lbAvviso As NTSInformatica.NTSLabel
  Public WithEvents lbArtico As NTSInformatica.NTSLabel
  Public WithEvents edArtico As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNEtichettePerTC As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNEtichettePerTC As NTSInformatica.NTSLabel
  Public WithEvents ckPrzNetClasseSconto As NTSInformatica.NTSCheckBox
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
#End Region

#Region "EVENTI DI FORM"
  Public Overridable Sub FRMMGETTE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As DataTable = Nothing
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edAanno.Text = NTSCStr(Now.Year)
      edDaanno.Text = NTSCStr(Now.Year)

      edAdata.Text = IntSetDate("31/12/" + edDaanno.Text)
      edDadata.Text = IntSetDate("01/01/" + edAanno.Text)
      edValida.Text = IntSetDate(NTSCStr(Now))
      edDatamod.Text = edValida.Text

      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then
        bModTCO = True
        lbTaglia.Visible = True
        edTaglia.Visible = True
        lbNEtichettePerTC.Visible = True
        edNEtichettePerTC.Visible = True
      Else
        bModTCO = False
        lbTaglia.Visible = False
        edTaglia.Visible = False
        lbNEtichettePerTC.Visible = False
        edNEtichettePerTC.Visible = False
      End If

      bBD_DaCodarfo = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "BD_DaCodarfo", "0", ".", "0"))  ' NON DOCUMENTARE
      nBD_Listino = NTSCInt(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "BD_Listino", "0", ".", "0"))  ' NON DOCUMENTARE
      bUsaTtstmatr = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "UsaTtstmatr", "0", ".", "0"))
      bStpTerm = CBool(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "UsaStpTermica", "0", ".", "0"))
      bDometic = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "Dometic", "0", ".", "0")) ' non documentare
      bStampaNegativi = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StampaNegativi", "0", ".", "0")) 'se true, nella stampa da documenti di magazzino e da ordini stampa anche le etichette di articoi con quantità negativ a (moltiprila la quantità * -1)

      If bModTCO Then
        bNEtichetteBarCode = False
      Else
        bNEtichetteBarCode = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "NEtichetteBarCode", "0", ".", "0")) ' NON DOCUMENTARE
      End If

      If bStpTerm Then
        ckTermica.Checked = True
      Else
        ckTermica.Checked = False
      End If

      bDividiQuantConfez = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "DividiQuantConfez", "0", ".", "0"))
      bCreaBarcode = CBool(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "CreaBarcode", "0", ".", "0")) 'non documentare
      lCreaBarcodeTipo = NTSCInt(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "nCreaBarcodeTipo", "0", ".", "0")) 'non usato, non documentare

      'Tabella temporanea
      lIIstMats = oMenu.GetTblInstId("TTSTMATS", bsDbAzienda)
      lIIstMatr = oMenu.GetTblInstId("TTSTMATR", bsDbAzienda)

      CaricaCombo()

      'Carica le impostazioni RECENT
      If CBool(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "MoltDiv", "0", ".", "0")) Then
        cbOperazione.SelectedValue = NTSCStr(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "MoltipQualeOpt", "1", ".", "1")) '1 o 2
        If cbOperazione.SelectedValue = "1" Then
          edNumMolt.Text = NTSCStr(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "MoltipValoreMoltip", "1", ".", "1"))
        Else
          edNumMolt.Text = NTSCStr(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "MoltipValoreDividi", "1", ".", "1"))
        End If
      End If
      cbTipostp.SelectedValue = NTSCStr(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "TipoStampa", "0", ".", "0"))

      RiceviParametri()

      If Not bBD_DaCodarfo Then
        cmdArtico.Visible = True
        lbArtico.Visible = False
        edArtico.Visible = False

        'Chiamata con parametri
        If bDaArticoli Then
          cbOrigine.SelectedValue = "A"
          cbOrigine.Enabled = False
          cmdArtico.Enabled = False
          bFiltri = True
          strArticoli = strArticoli.ToUpper
          strWhereArtico = "ar_codart = " & CStrSQL(strArticoli)
          Me.Text = oApp.Tr(Me, 130421091470883002, "Stampa etichette articolo: |" & strArticoli & "|")
          edEtiche.Text = NTSCStr(lQuantita)
        End If

        If bDaDocumenti Then
          cbOrigine.SelectedValue = "M"
          cbOrigine.Enabled = False
          If Not oCleEtte.DaMagazzino(strTipoDocu, nAnnoDocu, strSerieDocu, lNumDocu, dttTmp) Then Me.Close()

          RiempiDatiMagaz(NTSCStr(dttTmp.Rows(0)!tm_datdoc), NTSCStr(dttTmp.Rows(0)!tm_conto))
        End If

        If bDaOrdini Then
          cbOrigine.SelectedValue = "M"
          cbOrigine.Enabled = False
          If Not oCleEtte.DaOrdini(strTipoDocu, nAnnoDocu, strSerieDocu, lNumDocu, dttTmp) Then Me.Close()

          RiempiDatiMagaz(NTSCStr(dttTmp.Rows(0)!td_datord), NTSCStr(dttTmp.Rows(0)!td_conto))
        End If

        If oCleEtte.bDaTc Then
          cbOrigine.SelectedValue = "A"
          cbOrigine.Enabled = False
          cmdArtico.Enabled = False
          strArticoli = strArticoli.ToUpper
          strWhereArtico = "ar_codart = " & CStrSQL(strArticoli)
          Me.Text = oApp.Tr(Me, 130421091748851752, "Stampa etichette taglie colori articolo: |" & strArticoli & "|")
          edEtiche.Text = NTSCStr(lQtaTot)
          edEtiche.Enabled = False
          edTaglia.Enabled = False
        End If
      Else
        cmdArtico.Visible = False
        lbArtico.Visible = True
        edArtico.Visible = True
      End If

      'Carica le impostazioni RECENT
      If UCase$(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "FlagPrezzi", "N", ".", "N")) = "S" Then
        ckPrezzi.Checked = True
        edListino.Text = oMenu.GetSettingBus("Bsmgette", "Recent", ".", "Listino", "1", ".", "1")
        If UCase$(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "FlagAncheValuta", "N", ".", "N")) = "S" Then
          ckValuta.Checked = True
          edValuta.Text = oMenu.GetSettingBus("Bsmgette", "Recent", ".", "Valuta", "0", ".", "0")
        Else
          ckValuta.Checked = False
        End If
        ckPrzNetClasseSconto.Checked = oMenu.GetSettingBus("Bsmgette", "Recent", ".", "SconClCliDaList", "N", ".", "N") = "S"
      Else
        ckPrezzi.Checked = False
      End If
      If UCase$(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "SoloBarcode", "N", ".", "N")) = "S" Then
        ckArtbar.Checked = True
      End If
      If UCase$(oMenu.GetSettingBus("Bsmgette", "Recent", ".", "QuantDaColli", "N", ".", "N")) = "S" Then
        opColli.Checked = True
      Else
        opQta.Checked = True
      End If

      If oMenu.GetSettingBus("OPZIONI", ".", ".", "SconClCliDaList", "N", " ", "N") = "N" Then
        ckPrzNetClasseSconto.Visible = False
        ckPrzNetClasseSconto.Checked = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      '' Acquisisce InstID
      'lInstID = oMenu.GetTblInstId("TTSTMATR", bsDbAzienda)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGETTE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      'Salva le ultime scelte
      oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "FlagPrezzi", NTSCStr(IIf(ckPrezzi.Checked, "S", "N")), " ", "NS.", "...", "...")
      If ckPrezzi.Checked Then
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "Listino", edListino.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "FlagAncheValuta", NTSCStr(IIf(ckValuta.Checked, "S", "N")), " ", "NS.", "...", "...")
        If ckValuta.Checked Then
          oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "Valuta", edValuta.Text, " ", "NS.", "...", "...")
        End If
      End If
      oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "SoloBarcode", NTSCStr(IIf(ckArtbar.Checked, "S", "N")), " ", "NS.", "...", "...")
      If opColli.Checked Then
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "QuantDaColli", "S", " ", "NS.", "...", "...")
      Else
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "QuantDaColli", "N", " ", "NS.", "...", "...")
      End If
      oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltDiv", NTSCStr(IIf(ckStampe.Checked, "-1", "0")), " ", "NS.", "...", "...")
      If ckStampe.Checked Then
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltipQualeOpt", cbOperazione.SelectedValue, " ", "NS.", "...", "...")
        If cbOperazione.SelectedValue = "1" Then
          oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltipValoreMoltip", edNumMolt.Text, " ", "NS.", "...", "...")
          oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltipValoreDividi", "0", " ", "NS.", "...", "...")
        Else
          oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltipValoreDividi", edNumMolt.Text, " ", "NS.", "...", "...")
          oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "MoltipValoreMoltip", "0", " ", "NS.", "...", "...")
        End If
      End If

      If ckTermica.Checked Then
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "UsaStpTermica", "-1", " ", "NS.", "...", "...")
      Else
        oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "UsaStpTermica", "0", " ", "NS.", "...", "...")
      End If

      oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "SconClCliDaList", NTSCStr(IIf(ckPrzNetClasseSconto.Checked, "S", "N")), " ", "NS.", "...", "...")

      oMenu.SaveSettingBus("Bsmgette", "Recent", ".", "TipoStampa", cbTipostp.SelectedValue, " ", "NS.", "...", "...")

      oMenu.ResetTblInstId("TTSTMATR", bsDbAzienda, lIIstMatr)
      oMenu.ResetTblInstId("TTSTMATS", bsDbAzienda, lIIstMats)

      dcEtte.Dispose()
      dsEtte.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGETTE", "BEMGETTE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleEtte = CType(oTmp, CLEMGETTE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGETTE", strRemoteServer, strRemotePort)
    AddHandler oCleEtte.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleEtte.Init(oApp, NTSScript, oMenu.oCleComm, "TABANAZ", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGETTE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmOrigine = New NTSInformatica.NTSGroupBox
    Me.pnArtico = New NTSInformatica.NTSPanel
    Me.edArtico = New NTSInformatica.NTSTextBoxStr
    Me.lbArtico = New NTSInformatica.NTSLabel
    Me.cmdArtico = New NTSInformatica.NTSButton
    Me.pnLista = New NTSInformatica.NTSPanel
    Me.ckNumeti = New NTSInformatica.NTSCheckBox
    Me.lbDesclista = New NTSInformatica.NTSLabel
    Me.lbLista = New NTSInformatica.NTSLabel
    Me.edLista = New NTSInformatica.NTSTextBoxNum
    Me.pnFiltri = New NTSInformatica.NTSPanel
    Me.edTaglia = New NTSInformatica.NTSTextBoxStr
    Me.lbTaglia = New NTSInformatica.NTSLabel
    Me.lbBarcode = New NTSInformatica.NTSLabel
    Me.lbEtiche = New NTSInformatica.NTSLabel
    Me.cbBarcode = New NTSInformatica.NTSComboBox
    Me.edEtiche = New NTSInformatica.NTSTextBoxNum
    Me.pnDoc = New NTSInformatica.NTSPanel
    Me.fmStampe = New NTSInformatica.NTSGroupBox
    Me.ckMisura = New NTSInformatica.NTSCheckBox
    Me.edNumMolt = New NTSInformatica.NTSTextBoxNum
    Me.lbPer = New NTSInformatica.NTSLabel
    Me.cbOperazione = New NTSInformatica.NTSComboBox
    Me.ckStampe = New NTSInformatica.NTSCheckBox
    Me.opQta = New NTSInformatica.NTSRadioButton
    Me.opColli = New NTSInformatica.NTSRadioButton
    Me.lbNumeti = New NTSInformatica.NTSLabel
    Me.lbA = New NTSInformatica.NTSLabel
    Me.lbDa = New NTSInformatica.NTSLabel
    Me.lbAriga = New NTSInformatica.NTSLabel
    Me.lbDariga = New NTSInformatica.NTSLabel
    Me.lbAnumero = New NTSInformatica.NTSLabel
    Me.lbAserie = New NTSInformatica.NTSLabel
    Me.lbDanumero = New NTSInformatica.NTSLabel
    Me.lbDaserie = New NTSInformatica.NTSLabel
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.lbAclie = New NTSInformatica.NTSLabel
    Me.lbAanno = New NTSInformatica.NTSLabel
    Me.lbDaanno = New NTSInformatica.NTSLabel
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbDaclie = New NTSInformatica.NTSLabel
    Me.lbTipodoc = New NTSInformatica.NTSLabel
    Me.edAriga = New NTSInformatica.NTSTextBoxNum
    Me.edDariga = New NTSInformatica.NTSTextBoxNum
    Me.edAnumero = New NTSInformatica.NTSTextBoxNum
    Me.edDanumero = New NTSInformatica.NTSTextBoxNum
    Me.edAserie = New NTSInformatica.NTSTextBoxStr
    Me.edDaserie = New NTSInformatica.NTSTextBoxStr
    Me.edAanno = New NTSInformatica.NTSTextBoxNum
    Me.edDaanno = New NTSInformatica.NTSTextBoxNum
    Me.edAdata = New NTSInformatica.NTSTextBoxData
    Me.edDadata = New NTSInformatica.NTSTextBoxData
    Me.edAclie = New NTSInformatica.NTSTextBoxNum
    Me.edDaclie = New NTSInformatica.NTSTextBoxNum
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.cbOrigine = New NTSInformatica.NTSComboBox
    Me.fmPrezzi = New NTSInformatica.NTSGroupBox
    Me.lbDescvalu = New NTSInformatica.NTSLabel
    Me.ckPrzNetClasseSconto = New NTSInformatica.NTSCheckBox
    Me.ckDatamod = New NTSInformatica.NTSCheckBox
    Me.ckValuta = New NTSInformatica.NTSCheckBox
    Me.edDatamod = New NTSInformatica.NTSTextBoxData
    Me.edValida = New NTSInformatica.NTSTextBoxData
    Me.edValuta = New NTSInformatica.NTSTextBoxNum
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.lbValida = New NTSInformatica.NTSLabel
    Me.lbListino = New NTSInformatica.NTSLabel
    Me.ckPrezzi = New NTSInformatica.NTSCheckBox
    Me.ckArtbar = New NTSInformatica.NTSCheckBox
    Me.ckTermica = New NTSInformatica.NTSCheckBox
    Me.ckBarcode = New NTSInformatica.NTSCheckBox
    Me.cbSolobar = New NTSInformatica.NTSComboBox
    Me.lbTipostp = New NTSInformatica.NTSLabel
    Me.cbTipostp = New NTSInformatica.NTSComboBox
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.lbAvviso = New NTSInformatica.NTSLabel
    Me.edNEtichettePerTC = New NTSInformatica.NTSTextBoxNum
    Me.lbNEtichettePerTC = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOrigine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrigine.SuspendLayout()
    CType(Me.pnArtico, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnArtico.SuspendLayout()
    CType(Me.edArtico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLista, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLista.SuspendLayout()
    CType(Me.ckNumeti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLista.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri.SuspendLayout()
    CType(Me.edTaglia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEtiche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDoc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDoc.SuspendLayout()
    CType(Me.fmStampe, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStampe.SuspendLayout()
    CType(Me.ckMisura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumMolt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOperazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStampe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opQta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opColli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAriga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDariga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAserie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaserie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAanno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaanno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAclie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaclie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOrigine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzi.SuspendLayout()
    CType(Me.ckPrzNetClasseSconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDatamod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckValuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatamod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckArtbar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTermica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSolobar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipostp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNEtichettePerTC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 7
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 0
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 1
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 6
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'fmOrigine
    '
    Me.fmOrigine.AllowDrop = True
    Me.fmOrigine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrigine.Appearance.Options.UseBackColor = True
    Me.fmOrigine.Controls.Add(Me.pnArtico)
    Me.fmOrigine.Controls.Add(Me.pnLista)
    Me.fmOrigine.Controls.Add(Me.pnFiltri)
    Me.fmOrigine.Controls.Add(Me.pnDoc)
    Me.fmOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOrigine.Location = New System.Drawing.Point(12, 62)
    Me.fmOrigine.Name = "fmOrigine"
    Me.fmOrigine.Size = New System.Drawing.Size(477, 235)
    Me.fmOrigine.TabIndex = 4
    Me.fmOrigine.Text = "Filtri"
    '
    'pnArtico
    '
    Me.pnArtico.AllowDrop = True
    Me.pnArtico.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnArtico.Appearance.Options.UseBackColor = True
    Me.pnArtico.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnArtico.Controls.Add(Me.edArtico)
    Me.pnArtico.Controls.Add(Me.lbArtico)
    Me.pnArtico.Controls.Add(Me.cmdArtico)
    Me.pnArtico.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnArtico.Location = New System.Drawing.Point(6, 23)
    Me.pnArtico.Name = "pnArtico"
    Me.pnArtico.NTSActiveTrasparency = True
    Me.pnArtico.Size = New System.Drawing.Size(466, 54)
    Me.pnArtico.TabIndex = 1
    Me.pnArtico.Text = "NtsPanel1"
    '
    'edArtico
    '
    Me.edArtico.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtico.EditValue = ""
    Me.edArtico.Location = New System.Drawing.Point(229, 7)
    Me.edArtico.Name = "edArtico"
    Me.edArtico.NTSDbField = ""
    Me.edArtico.NTSForzaVisZoom = False
    Me.edArtico.NTSOldValue = ""
    Me.edArtico.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtico.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtico.Properties.AutoHeight = False
    Me.edArtico.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtico.Properties.MaxLength = 65536
    Me.edArtico.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtico.Size = New System.Drawing.Size(87, 20)
    Me.edArtico.TabIndex = 2
    Me.edArtico.TextStr = ""
    '
    'lbArtico
    '
    Me.lbArtico.AutoSize = True
    Me.lbArtico.BackColor = System.Drawing.Color.Transparent
    Me.lbArtico.Location = New System.Drawing.Point(133, 10)
    Me.lbArtico.Name = "lbArtico"
    Me.lbArtico.NTSDbField = ""
    Me.lbArtico.Size = New System.Drawing.Size(47, 13)
    Me.lbArtico.TabIndex = 1
    Me.lbArtico.Text = "Articolo:"
    Me.lbArtico.Tooltip = ""
    Me.lbArtico.UseMnemonic = False
    '
    'cmdArtico
    '
    Me.cmdArtico.ImagePath = ""
    Me.cmdArtico.ImageText = ""
    Me.cmdArtico.Location = New System.Drawing.Point(136, 7)
    Me.cmdArtico.Name = "cmdArtico"
    Me.cmdArtico.NTSContextMenu = Nothing
    Me.cmdArtico.Size = New System.Drawing.Size(180, 35)
    Me.cmdArtico.TabIndex = 0
    Me.cmdArtico.Text = "Seleziona articoli..."
    '
    'pnLista
    '
    Me.pnLista.AllowDrop = True
    Me.pnLista.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLista.Appearance.Options.UseBackColor = True
    Me.pnLista.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLista.Controls.Add(Me.ckNumeti)
    Me.pnLista.Controls.Add(Me.lbDesclista)
    Me.pnLista.Controls.Add(Me.lbLista)
    Me.pnLista.Controls.Add(Me.edLista)
    Me.pnLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLista.Location = New System.Drawing.Point(6, 23)
    Me.pnLista.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnLista.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnLista.Name = "pnLista"
    Me.pnLista.NTSActiveTrasparency = True
    Me.pnLista.Size = New System.Drawing.Size(466, 54)
    Me.pnLista.TabIndex = 3
    '
    'ckNumeti
    '
    Me.ckNumeti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNumeti.Location = New System.Drawing.Point(51, 29)
    Me.ckNumeti.Name = "ckNumeti"
    Me.ckNumeti.NTSCheckValue = "S"
    Me.ckNumeti.NTSUnCheckValue = "N"
    Me.ckNumeti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNumeti.Properties.Appearance.Options.UseBackColor = True
    Me.ckNumeti.Properties.AutoHeight = False
    Me.ckNumeti.Properties.Caption = "Utilizza numero di etichette della lista selezionata articoli (se presente)"
    Me.ckNumeti.Size = New System.Drawing.Size(359, 19)
    Me.ckNumeti.TabIndex = 3
    '
    'lbDesclista
    '
    Me.lbDesclista.BackColor = System.Drawing.Color.Transparent
    Me.lbDesclista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesclista.Location = New System.Drawing.Point(205, 3)
    Me.lbDesclista.Name = "lbDesclista"
    Me.lbDesclista.NTSDbField = ""
    Me.lbDesclista.Size = New System.Drawing.Size(205, 20)
    Me.lbDesclista.TabIndex = 2
    Me.lbDesclista.Tooltip = ""
    Me.lbDesclista.UseMnemonic = False
    '
    'lbLista
    '
    Me.lbLista.AutoSize = True
    Me.lbLista.BackColor = System.Drawing.Color.Transparent
    Me.lbLista.Location = New System.Drawing.Point(48, 6)
    Me.lbLista.Name = "lbLista"
    Me.lbLista.NTSDbField = ""
    Me.lbLista.Size = New System.Drawing.Size(86, 13)
    Me.lbLista.TabIndex = 1
    Me.lbLista.Text = "Lista selezionata"
    Me.lbLista.Tooltip = ""
    Me.lbLista.UseMnemonic = False
    '
    'edLista
    '
    Me.edLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLista.EditValue = "0"
    Me.edLista.Location = New System.Drawing.Point(146, 3)
    Me.edLista.Name = "edLista"
    Me.edLista.NTSDbField = ""
    Me.edLista.NTSFormat = "0"
    Me.edLista.NTSForzaVisZoom = False
    Me.edLista.NTSOldValue = ""
    Me.edLista.Properties.Appearance.Options.UseTextOptions = True
    Me.edLista.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLista.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLista.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLista.Properties.AutoHeight = False
    Me.edLista.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLista.Properties.MaxLength = 65536
    Me.edLista.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLista.Size = New System.Drawing.Size(53, 20)
    Me.edLista.TabIndex = 0
    Me.edLista.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edLista.TextInt = 0
    '
    'pnFiltri
    '
    Me.pnFiltri.AllowDrop = True
    Me.pnFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri.Appearance.Options.UseBackColor = True
    Me.pnFiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri.Controls.Add(Me.edTaglia)
    Me.pnFiltri.Controls.Add(Me.lbTaglia)
    Me.pnFiltri.Controls.Add(Me.lbBarcode)
    Me.pnFiltri.Controls.Add(Me.lbEtiche)
    Me.pnFiltri.Controls.Add(Me.cbBarcode)
    Me.pnFiltri.Controls.Add(Me.edEtiche)
    Me.pnFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri.Location = New System.Drawing.Point(6, 84)
    Me.pnFiltri.Name = "pnFiltri"
    Me.pnFiltri.NTSActiveTrasparency = True
    Me.pnFiltri.Size = New System.Drawing.Size(466, 89)
    Me.pnFiltri.TabIndex = 2
    Me.pnFiltri.Text = "NtsPanel1"
    '
    'edTaglia
    '
    Me.edTaglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTaglia.EditValue = ""
    Me.edTaglia.Location = New System.Drawing.Point(229, 56)
    Me.edTaglia.Name = "edTaglia"
    Me.edTaglia.NTSDbField = ""
    Me.edTaglia.NTSForzaVisZoom = False
    Me.edTaglia.NTSOldValue = ""
    Me.edTaglia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTaglia.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTaglia.Properties.AutoHeight = False
    Me.edTaglia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTaglia.Properties.MaxLength = 65536
    Me.edTaglia.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTaglia.Size = New System.Drawing.Size(87, 20)
    Me.edTaglia.TabIndex = 5
    Me.edTaglia.TextStr = ""
    '
    'lbTaglia
    '
    Me.lbTaglia.AutoSize = True
    Me.lbTaglia.BackColor = System.Drawing.Color.Transparent
    Me.lbTaglia.Location = New System.Drawing.Point(133, 59)
    Me.lbTaglia.Name = "lbTaglia"
    Me.lbTaglia.NTSDbField = ""
    Me.lbTaglia.Size = New System.Drawing.Size(35, 13)
    Me.lbTaglia.TabIndex = 4
    Me.lbTaglia.Text = "Taglia"
    Me.lbTaglia.Tooltip = ""
    Me.lbTaglia.UseMnemonic = False
    '
    'lbBarcode
    '
    Me.lbBarcode.AutoSize = True
    Me.lbBarcode.BackColor = System.Drawing.Color.Transparent
    Me.lbBarcode.Location = New System.Drawing.Point(133, 33)
    Me.lbBarcode.Name = "lbBarcode"
    Me.lbBarcode.NTSDbField = ""
    Me.lbBarcode.Size = New System.Drawing.Size(72, 13)
    Me.lbBarcode.TabIndex = 3
    Me.lbBarcode.Text = "U.M barcode:"
    Me.lbBarcode.Tooltip = ""
    Me.lbBarcode.UseMnemonic = False
    '
    'lbEtiche
    '
    Me.lbEtiche.AutoSize = True
    Me.lbEtiche.BackColor = System.Drawing.Color.Transparent
    Me.lbEtiche.Location = New System.Drawing.Point(133, 6)
    Me.lbEtiche.Name = "lbEtiche"
    Me.lbEtiche.NTSDbField = ""
    Me.lbEtiche.Size = New System.Drawing.Size(90, 13)
    Me.lbEtiche.TabIndex = 2
    Me.lbEtiche.Text = "Numero Etichette"
    Me.lbEtiche.Tooltip = ""
    Me.lbEtiche.UseMnemonic = False
    '
    'cbBarcode
    '
    Me.cbBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbBarcode.DataSource = Nothing
    Me.cbBarcode.DisplayMember = ""
    Me.cbBarcode.Location = New System.Drawing.Point(229, 30)
    Me.cbBarcode.Name = "cbBarcode"
    Me.cbBarcode.NTSDbField = ""
    Me.cbBarcode.Properties.AutoHeight = False
    Me.cbBarcode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbBarcode.Properties.DropDownRows = 30
    Me.cbBarcode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbBarcode.SelectedValue = ""
    Me.cbBarcode.Size = New System.Drawing.Size(87, 20)
    Me.cbBarcode.TabIndex = 1
    Me.cbBarcode.ValueMember = ""
    '
    'edEtiche
    '
    Me.edEtiche.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEtiche.EditValue = "1"
    Me.edEtiche.Location = New System.Drawing.Point(229, 3)
    Me.edEtiche.Name = "edEtiche"
    Me.edEtiche.NTSDbField = ""
    Me.edEtiche.NTSFormat = "0"
    Me.edEtiche.NTSForzaVisZoom = False
    Me.edEtiche.NTSOldValue = "1"
    Me.edEtiche.Properties.Appearance.Options.UseTextOptions = True
    Me.edEtiche.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEtiche.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEtiche.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEtiche.Properties.AutoHeight = False
    Me.edEtiche.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEtiche.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEtiche.Size = New System.Drawing.Size(87, 20)
    Me.edEtiche.TabIndex = 0
    Me.edEtiche.Tag = ""
    Me.edEtiche.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edEtiche.TextInt = 1
    '
    'pnDoc
    '
    Me.pnDoc.AllowDrop = True
    Me.pnDoc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDoc.Appearance.Options.UseBackColor = True
    Me.pnDoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDoc.Controls.Add(Me.fmStampe)
    Me.pnDoc.Controls.Add(Me.opQta)
    Me.pnDoc.Controls.Add(Me.opColli)
    Me.pnDoc.Controls.Add(Me.lbNumeti)
    Me.pnDoc.Controls.Add(Me.lbA)
    Me.pnDoc.Controls.Add(Me.lbDa)
    Me.pnDoc.Controls.Add(Me.lbAriga)
    Me.pnDoc.Controls.Add(Me.lbDariga)
    Me.pnDoc.Controls.Add(Me.lbAnumero)
    Me.pnDoc.Controls.Add(Me.lbAserie)
    Me.pnDoc.Controls.Add(Me.lbDanumero)
    Me.pnDoc.Controls.Add(Me.lbDaserie)
    Me.pnDoc.Controls.Add(Me.lbAdata)
    Me.pnDoc.Controls.Add(Me.lbAclie)
    Me.pnDoc.Controls.Add(Me.lbAanno)
    Me.pnDoc.Controls.Add(Me.lbDaanno)
    Me.pnDoc.Controls.Add(Me.lbDadata)
    Me.pnDoc.Controls.Add(Me.lbDaclie)
    Me.pnDoc.Controls.Add(Me.lbTipodoc)
    Me.pnDoc.Controls.Add(Me.edAriga)
    Me.pnDoc.Controls.Add(Me.edDariga)
    Me.pnDoc.Controls.Add(Me.edAnumero)
    Me.pnDoc.Controls.Add(Me.edDanumero)
    Me.pnDoc.Controls.Add(Me.edAserie)
    Me.pnDoc.Controls.Add(Me.edDaserie)
    Me.pnDoc.Controls.Add(Me.edAanno)
    Me.pnDoc.Controls.Add(Me.edDaanno)
    Me.pnDoc.Controls.Add(Me.edAdata)
    Me.pnDoc.Controls.Add(Me.edDadata)
    Me.pnDoc.Controls.Add(Me.edAclie)
    Me.pnDoc.Controls.Add(Me.edDaclie)
    Me.pnDoc.Controls.Add(Me.cbTipodoc)
    Me.pnDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDoc.Location = New System.Drawing.Point(6, 23)
    Me.pnDoc.Name = "pnDoc"
    Me.pnDoc.NTSActiveTrasparency = True
    Me.pnDoc.Size = New System.Drawing.Size(466, 207)
    Me.pnDoc.TabIndex = 3
    Me.pnDoc.Visible = False
    '
    'fmStampe
    '
    Me.fmStampe.AllowDrop = True
    Me.fmStampe.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStampe.Appearance.Options.UseBackColor = True
    Me.fmStampe.Controls.Add(Me.ckMisura)
    Me.fmStampe.Controls.Add(Me.edNumMolt)
    Me.fmStampe.Controls.Add(Me.lbPer)
    Me.fmStampe.Controls.Add(Me.cbOperazione)
    Me.fmStampe.Controls.Add(Me.ckStampe)
    Me.fmStampe.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStampe.Location = New System.Drawing.Point(6, 156)
    Me.fmStampe.Name = "fmStampe"
    Me.fmStampe.Size = New System.Drawing.Size(452, 48)
    Me.fmStampe.TabIndex = 31
    '
    'ckMisura
    '
    Me.ckMisura.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMisura.Enabled = False
    Me.ckMisura.Location = New System.Drawing.Point(199, 24)
    Me.ckMisura.Name = "ckMisura"
    Me.ckMisura.NTSCheckValue = "S"
    Me.ckMisura.NTSUnCheckValue = "N"
    Me.ckMisura.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMisura.Properties.Appearance.Options.UseBackColor = True
    Me.ckMisura.Properties.AutoHeight = False
    Me.ckMisura.Properties.Caption = "Rileva moltip./divisore da Misura 1 dell'articolo"
    Me.ckMisura.Size = New System.Drawing.Size(243, 19)
    Me.ckMisura.TabIndex = 4
    '
    'edNumMolt
    '
    Me.edNumMolt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumMolt.EditValue = "1"
    Me.edNumMolt.Enabled = False
    Me.edNumMolt.Location = New System.Drawing.Point(140, 23)
    Me.edNumMolt.Name = "edNumMolt"
    Me.edNumMolt.NTSDbField = ""
    Me.edNumMolt.NTSFormat = "0"
    Me.edNumMolt.NTSForzaVisZoom = False
    Me.edNumMolt.NTSOldValue = "1"
    Me.edNumMolt.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumMolt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumMolt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumMolt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumMolt.Properties.AutoHeight = False
    Me.edNumMolt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumMolt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumMolt.Size = New System.Drawing.Size(52, 20)
    Me.edNumMolt.TabIndex = 3
    Me.edNumMolt.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edNumMolt.TextInt = 1
    '
    'lbPer
    '
    Me.lbPer.AutoSize = True
    Me.lbPer.BackColor = System.Drawing.Color.Transparent
    Me.lbPer.Location = New System.Drawing.Point(111, 26)
    Me.lbPer.Name = "lbPer"
    Me.lbPer.NTSDbField = ""
    Me.lbPer.Size = New System.Drawing.Size(23, 13)
    Me.lbPer.TabIndex = 2
    Me.lbPer.Text = "per"
    Me.lbPer.Tooltip = ""
    Me.lbPer.UseMnemonic = False
    '
    'cbOperazione
    '
    Me.cbOperazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOperazione.DataSource = Nothing
    Me.cbOperazione.DisplayMember = ""
    Me.cbOperazione.Enabled = False
    Me.cbOperazione.Location = New System.Drawing.Point(5, 23)
    Me.cbOperazione.Name = "cbOperazione"
    Me.cbOperazione.NTSDbField = ""
    Me.cbOperazione.Properties.AutoHeight = False
    Me.cbOperazione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOperazione.Properties.DropDownRows = 30
    Me.cbOperazione.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOperazione.SelectedValue = ""
    Me.cbOperazione.Size = New System.Drawing.Size(100, 20)
    Me.cbOperazione.TabIndex = 1
    Me.cbOperazione.ValueMember = ""
    '
    'ckStampe
    '
    Me.ckStampe.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStampe.Location = New System.Drawing.Point(6, 0)
    Me.ckStampe.Name = "ckStampe"
    Me.ckStampe.NTSCheckValue = "S"
    Me.ckStampe.NTSUnCheckValue = "N"
    Me.ckStampe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStampe.Properties.Appearance.Options.UseBackColor = True
    Me.ckStampe.Properties.AutoHeight = False
    Me.ckStampe.Properties.Caption = "Modifica numero etichette da stampare"
    Me.ckStampe.Size = New System.Drawing.Size(210, 19)
    Me.ckStampe.TabIndex = 0
    '
    'opQta
    '
    Me.opQta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opQta.EditValue = True
    Me.opQta.Location = New System.Drawing.Point(257, 136)
    Me.opQta.Name = "opQta"
    Me.opQta.NTSCheckValue = "S"
    Me.opQta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opQta.Properties.Appearance.Options.UseBackColor = True
    Me.opQta.Properties.AutoHeight = False
    Me.opQta.Properties.Caption = "Colonna Quantità"
    Me.opQta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opQta.Size = New System.Drawing.Size(113, 19)
    Me.opQta.TabIndex = 30
    '
    'opColli
    '
    Me.opColli.Cursor = System.Windows.Forms.Cursors.Default
    Me.opColli.Location = New System.Drawing.Point(147, 136)
    Me.opColli.Name = "opColli"
    Me.opColli.NTSCheckValue = "S"
    Me.opColli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opColli.Properties.Appearance.Options.UseBackColor = True
    Me.opColli.Properties.AutoHeight = False
    Me.opColli.Properties.Caption = "Colonna Colli"
    Me.opColli.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opColli.Size = New System.Drawing.Size(94, 19)
    Me.opColli.TabIndex = 29
    '
    'lbNumeti
    '
    Me.lbNumeti.AutoSize = True
    Me.lbNumeti.BackColor = System.Drawing.Color.Transparent
    Me.lbNumeti.Location = New System.Drawing.Point(5, 139)
    Me.lbNumeti.Name = "lbNumeti"
    Me.lbNumeti.NTSDbField = ""
    Me.lbNumeti.Size = New System.Drawing.Size(134, 13)
    Me.lbNumeti.TabIndex = 28
    Me.lbNumeti.Text = "Numero etichette uguale a"
    Me.lbNumeti.Tooltip = ""
    Me.lbNumeti.UseMnemonic = False
    '
    'lbA
    '
    Me.lbA.AutoSize = True
    Me.lbA.BackColor = System.Drawing.Color.Transparent
    Me.lbA.Location = New System.Drawing.Point(3, 113)
    Me.lbA.Name = "lbA"
    Me.lbA.NTSDbField = ""
    Me.lbA.Size = New System.Drawing.Size(47, 13)
    Me.lbA.TabIndex = 27
    Me.lbA.Text = "A:   --->"
    Me.lbA.Tooltip = ""
    Me.lbA.UseMnemonic = False
    '
    'lbDa
    '
    Me.lbDa.AutoSize = True
    Me.lbDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDa.Location = New System.Drawing.Point(3, 87)
    Me.lbDa.Name = "lbDa"
    Me.lbDa.NTSDbField = ""
    Me.lbDa.Size = New System.Drawing.Size(47, 13)
    Me.lbDa.TabIndex = 26
    Me.lbDa.Text = "Da: --->"
    Me.lbDa.Tooltip = ""
    Me.lbDa.UseMnemonic = False
    '
    'lbAriga
    '
    Me.lbAriga.AutoSize = True
    Me.lbAriga.BackColor = System.Drawing.Color.Transparent
    Me.lbAriga.Location = New System.Drawing.Point(349, 113)
    Me.lbAriga.Name = "lbAriga"
    Me.lbAriga.NTSDbField = ""
    Me.lbAriga.Size = New System.Drawing.Size(25, 13)
    Me.lbAriga.TabIndex = 25
    Me.lbAriga.Text = "riga"
    Me.lbAriga.Tooltip = ""
    Me.lbAriga.UseMnemonic = False
    '
    'lbDariga
    '
    Me.lbDariga.AutoSize = True
    Me.lbDariga.BackColor = System.Drawing.Color.Transparent
    Me.lbDariga.Location = New System.Drawing.Point(349, 87)
    Me.lbDariga.Name = "lbDariga"
    Me.lbDariga.NTSDbField = ""
    Me.lbDariga.Size = New System.Drawing.Size(25, 13)
    Me.lbDariga.TabIndex = 24
    Me.lbDariga.Text = "riga"
    Me.lbDariga.Tooltip = ""
    Me.lbDariga.UseMnemonic = False
    '
    'lbAnumero
    '
    Me.lbAnumero.AutoSize = True
    Me.lbAnumero.BackColor = System.Drawing.Color.Transparent
    Me.lbAnumero.Location = New System.Drawing.Point(219, 113)
    Me.lbAnumero.Name = "lbAnumero"
    Me.lbAnumero.NTSDbField = ""
    Me.lbAnumero.Size = New System.Drawing.Size(43, 13)
    Me.lbAnumero.TabIndex = 23
    Me.lbAnumero.Text = "numero"
    Me.lbAnumero.Tooltip = ""
    Me.lbAnumero.UseMnemonic = False
    '
    'lbAserie
    '
    Me.lbAserie.AutoSize = True
    Me.lbAserie.BackColor = System.Drawing.Color.Transparent
    Me.lbAserie.Location = New System.Drawing.Point(136, 113)
    Me.lbAserie.Name = "lbAserie"
    Me.lbAserie.NTSDbField = ""
    Me.lbAserie.Size = New System.Drawing.Size(30, 13)
    Me.lbAserie.TabIndex = 22
    Me.lbAserie.Text = "serie"
    Me.lbAserie.Tooltip = ""
    Me.lbAserie.UseMnemonic = False
    '
    'lbDanumero
    '
    Me.lbDanumero.AutoSize = True
    Me.lbDanumero.BackColor = System.Drawing.Color.Transparent
    Me.lbDanumero.Location = New System.Drawing.Point(219, 87)
    Me.lbDanumero.Name = "lbDanumero"
    Me.lbDanumero.NTSDbField = ""
    Me.lbDanumero.Size = New System.Drawing.Size(43, 13)
    Me.lbDanumero.TabIndex = 21
    Me.lbDanumero.Text = "numero"
    Me.lbDanumero.Tooltip = ""
    Me.lbDanumero.UseMnemonic = False
    '
    'lbDaserie
    '
    Me.lbDaserie.AutoSize = True
    Me.lbDaserie.BackColor = System.Drawing.Color.Transparent
    Me.lbDaserie.Location = New System.Drawing.Point(136, 87)
    Me.lbDaserie.Name = "lbDaserie"
    Me.lbDaserie.NTSDbField = ""
    Me.lbDaserie.Size = New System.Drawing.Size(30, 13)
    Me.lbDaserie.TabIndex = 20
    Me.lbDaserie.Text = "serie"
    Me.lbDaserie.Tooltip = ""
    Me.lbDaserie.UseMnemonic = False
    '
    'lbAdata
    '
    Me.lbAdata.AutoSize = True
    Me.lbAdata.BackColor = System.Drawing.Color.Transparent
    Me.lbAdata.Location = New System.Drawing.Point(202, 60)
    Me.lbAdata.Name = "lbAdata"
    Me.lbAdata.NTSDbField = ""
    Me.lbAdata.Size = New System.Drawing.Size(48, 13)
    Me.lbAdata.TabIndex = 19
    Me.lbAdata.Text = "alla data"
    Me.lbAdata.Tooltip = ""
    Me.lbAdata.UseMnemonic = False
    '
    'lbAclie
    '
    Me.lbAclie.AutoSize = True
    Me.lbAclie.BackColor = System.Drawing.Color.Transparent
    Me.lbAclie.Location = New System.Drawing.Point(202, 33)
    Me.lbAclie.Name = "lbAclie"
    Me.lbAclie.NTSDbField = ""
    Me.lbAclie.Size = New System.Drawing.Size(49, 13)
    Me.lbAclie.TabIndex = 18
    Me.lbAclie.Text = "al cliente"
    Me.lbAclie.Tooltip = ""
    Me.lbAclie.UseMnemonic = False
    '
    'lbAanno
    '
    Me.lbAanno.AutoSize = True
    Me.lbAanno.BackColor = System.Drawing.Color.Transparent
    Me.lbAanno.Location = New System.Drawing.Point(55, 113)
    Me.lbAanno.Name = "lbAanno"
    Me.lbAanno.NTSDbField = ""
    Me.lbAanno.Size = New System.Drawing.Size(31, 13)
    Me.lbAanno.TabIndex = 17
    Me.lbAanno.Text = "anno"
    Me.lbAanno.Tooltip = ""
    Me.lbAanno.UseMnemonic = False
    '
    'lbDaanno
    '
    Me.lbDaanno.AutoSize = True
    Me.lbDaanno.BackColor = System.Drawing.Color.Transparent
    Me.lbDaanno.Location = New System.Drawing.Point(55, 87)
    Me.lbDaanno.Name = "lbDaanno"
    Me.lbDaanno.NTSDbField = ""
    Me.lbDaanno.Size = New System.Drawing.Size(31, 13)
    Me.lbDaanno.TabIndex = 16
    Me.lbDaanno.Text = "anno"
    Me.lbDaanno.Tooltip = ""
    Me.lbDaanno.UseMnemonic = False
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(3, 60)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(55, 13)
    Me.lbDadata.TabIndex = 15
    Me.lbDadata.Text = "Dalla data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'lbDaclie
    '
    Me.lbDaclie.AutoSize = True
    Me.lbDaclie.BackColor = System.Drawing.Color.Transparent
    Me.lbDaclie.Location = New System.Drawing.Point(3, 33)
    Me.lbDaclie.Name = "lbDaclie"
    Me.lbDaclie.NTSDbField = ""
    Me.lbDaclie.Size = New System.Drawing.Size(56, 13)
    Me.lbDaclie.TabIndex = 14
    Me.lbDaclie.Text = "Dal cliente"
    Me.lbDaclie.Tooltip = ""
    Me.lbDaclie.UseMnemonic = False
    '
    'lbTipodoc
    '
    Me.lbTipodoc.AutoSize = True
    Me.lbTipodoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTipodoc.Location = New System.Drawing.Point(3, 6)
    Me.lbTipodoc.Name = "lbTipodoc"
    Me.lbTipodoc.NTSDbField = ""
    Me.lbTipodoc.Size = New System.Drawing.Size(83, 13)
    Me.lbTipodoc.TabIndex = 13
    Me.lbTipodoc.Text = "Tipo documento"
    Me.lbTipodoc.Tooltip = ""
    Me.lbTipodoc.UseMnemonic = False
    '
    'edAriga
    '
    Me.edAriga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAriga.EditValue = "999999999"
    Me.edAriga.Location = New System.Drawing.Point(376, 110)
    Me.edAriga.Name = "edAriga"
    Me.edAriga.NTSDbField = ""
    Me.edAriga.NTSFormat = "0"
    Me.edAriga.NTSForzaVisZoom = False
    Me.edAriga.NTSOldValue = "999999999"
    Me.edAriga.Properties.Appearance.Options.UseTextOptions = True
    Me.edAriga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAriga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAriga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAriga.Properties.AutoHeight = False
    Me.edAriga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAriga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAriga.Size = New System.Drawing.Size(82, 20)
    Me.edAriga.TabIndex = 12
    Me.edAriga.TextDec = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.edAriga.TextInt = 999999999
    '
    'edDariga
    '
    Me.edDariga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDariga.EditValue = "0"
    Me.edDariga.Location = New System.Drawing.Point(376, 84)
    Me.edDariga.Name = "edDariga"
    Me.edDariga.NTSDbField = ""
    Me.edDariga.NTSFormat = "0"
    Me.edDariga.NTSForzaVisZoom = False
    Me.edDariga.NTSOldValue = ""
    Me.edDariga.Properties.Appearance.Options.UseTextOptions = True
    Me.edDariga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDariga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDariga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDariga.Properties.AutoHeight = False
    Me.edDariga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDariga.Properties.MaxLength = 65536
    Me.edDariga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDariga.Size = New System.Drawing.Size(82, 20)
    Me.edDariga.TabIndex = 11
    Me.edDariga.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDariga.TextInt = 0
    '
    'edAnumero
    '
    Me.edAnumero.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAnumero.EditValue = "999999999"
    Me.edAnumero.Location = New System.Drawing.Point(263, 110)
    Me.edAnumero.Name = "edAnumero"
    Me.edAnumero.NTSDbField = ""
    Me.edAnumero.NTSFormat = "0"
    Me.edAnumero.NTSForzaVisZoom = False
    Me.edAnumero.NTSOldValue = "999999999"
    Me.edAnumero.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumero.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumero.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnumero.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnumero.Properties.AutoHeight = False
    Me.edAnumero.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnumero.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnumero.Size = New System.Drawing.Size(82, 20)
    Me.edAnumero.TabIndex = 10
    Me.edAnumero.TextDec = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.edAnumero.TextInt = 999999999
    '
    'edDanumero
    '
    Me.edDanumero.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumero.EditValue = "0"
    Me.edDanumero.Location = New System.Drawing.Point(263, 84)
    Me.edDanumero.Name = "edDanumero"
    Me.edDanumero.NTSDbField = ""
    Me.edDanumero.NTSFormat = "0"
    Me.edDanumero.NTSForzaVisZoom = False
    Me.edDanumero.NTSOldValue = ""
    Me.edDanumero.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumero.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumero.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDanumero.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDanumero.Properties.AutoHeight = False
    Me.edDanumero.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumero.Properties.MaxLength = 65536
    Me.edDanumero.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumero.Size = New System.Drawing.Size(82, 20)
    Me.edDanumero.TabIndex = 9
    Me.edDanumero.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDanumero.TextInt = 0
    '
    'edAserie
    '
    Me.edAserie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAserie.EditValue = "z"
    Me.edAserie.Location = New System.Drawing.Point(170, 110)
    Me.edAserie.Name = "edAserie"
    Me.edAserie.NTSDbField = ""
    Me.edAserie.NTSForzaVisZoom = False
    Me.edAserie.NTSOldValue = "z"
    Me.edAserie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAserie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAserie.Properties.AutoHeight = False
    Me.edAserie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAserie.Properties.MaxLength = 65536
    Me.edAserie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAserie.Size = New System.Drawing.Size(45, 20)
    Me.edAserie.TabIndex = 8
    Me.edAserie.Tag = ""
    Me.edAserie.TextStr = "z"
    '
    'edDaserie
    '
    Me.edDaserie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaserie.EditValue = ""
    Me.edDaserie.Location = New System.Drawing.Point(170, 84)
    Me.edDaserie.Name = "edDaserie"
    Me.edDaserie.NTSDbField = ""
    Me.edDaserie.NTSForzaVisZoom = False
    Me.edDaserie.NTSOldValue = ""
    Me.edDaserie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaserie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaserie.Properties.AutoHeight = False
    Me.edDaserie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaserie.Properties.MaxLength = 65536
    Me.edDaserie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaserie.Size = New System.Drawing.Size(45, 20)
    Me.edDaserie.TabIndex = 7
    Me.edDaserie.Tag = ""
    Me.edDaserie.TextStr = ""
    '
    'edAanno
    '
    Me.edAanno.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAanno.EditValue = "1900"
    Me.edAanno.Location = New System.Drawing.Point(89, 110)
    Me.edAanno.Name = "edAanno"
    Me.edAanno.NTSDbField = ""
    Me.edAanno.NTSFormat = "0"
    Me.edAanno.NTSForzaVisZoom = False
    Me.edAanno.NTSOldValue = "1900"
    Me.edAanno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAanno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAanno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAanno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAanno.Properties.AutoHeight = False
    Me.edAanno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAanno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAanno.Size = New System.Drawing.Size(43, 20)
    Me.edAanno.TabIndex = 6
    Me.edAanno.TextDec = New Decimal(New Integer() {1900, 0, 0, 0})
    Me.edAanno.TextInt = 1900
    '
    'edDaanno
    '
    Me.edDaanno.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDaanno.EditValue = "1900"
    Me.edDaanno.Location = New System.Drawing.Point(89, 84)
    Me.edDaanno.Name = "edDaanno"
    Me.edDaanno.NTSDbField = ""
    Me.edDaanno.NTSFormat = "0"
    Me.edDaanno.NTSForzaVisZoom = False
    Me.edDaanno.NTSOldValue = "1900"
    Me.edDaanno.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaanno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaanno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaanno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaanno.Properties.AutoHeight = False
    Me.edDaanno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaanno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaanno.Size = New System.Drawing.Size(43, 20)
    Me.edDaanno.TabIndex = 5
    Me.edDaanno.TextDec = New Decimal(New Integer() {1900, 0, 0, 0})
    Me.edDaanno.TextInt = 1900
    '
    'edAdata
    '
    Me.edAdata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdata.EditValue = "01/01/1900"
    Me.edAdata.Location = New System.Drawing.Point(257, 57)
    Me.edAdata.Name = "edAdata"
    Me.edAdata.NTSDbField = ""
    Me.edAdata.NTSForzaVisZoom = False
    Me.edAdata.NTSOldValue = ""
    Me.edAdata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdata.Properties.AutoHeight = False
    Me.edAdata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdata.Properties.MaxLength = 65536
    Me.edAdata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdata.Size = New System.Drawing.Size(82, 20)
    Me.edAdata.TabIndex = 4
    Me.edAdata.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edDadata
    '
    Me.edDadata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadata.EditValue = "01/01/1900"
    Me.edDadata.Location = New System.Drawing.Point(92, 57)
    Me.edDadata.Name = "edDadata"
    Me.edDadata.NTSDbField = ""
    Me.edDadata.NTSForzaVisZoom = False
    Me.edDadata.NTSOldValue = ""
    Me.edDadata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadata.Properties.AutoHeight = False
    Me.edDadata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadata.Properties.MaxLength = 65536
    Me.edDadata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadata.Size = New System.Drawing.Size(82, 20)
    Me.edDadata.TabIndex = 3
    Me.edDadata.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edAclie
    '
    Me.edAclie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAclie.EditValue = "999999999"
    Me.edAclie.Location = New System.Drawing.Point(257, 30)
    Me.edAclie.Name = "edAclie"
    Me.edAclie.NTSDbField = ""
    Me.edAclie.NTSFormat = "0"
    Me.edAclie.NTSForzaVisZoom = False
    Me.edAclie.NTSOldValue = "999999999"
    Me.edAclie.Properties.Appearance.Options.UseTextOptions = True
    Me.edAclie.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAclie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAclie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAclie.Properties.AutoHeight = False
    Me.edAclie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAclie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAclie.Size = New System.Drawing.Size(82, 20)
    Me.edAclie.TabIndex = 2
    Me.edAclie.TextDec = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.edAclie.TextInt = 999999999
    '
    'edDaclie
    '
    Me.edDaclie.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDaclie.EditValue = "0"
    Me.edDaclie.Location = New System.Drawing.Point(92, 30)
    Me.edDaclie.Name = "edDaclie"
    Me.edDaclie.NTSDbField = ""
    Me.edDaclie.NTSFormat = "0"
    Me.edDaclie.NTSForzaVisZoom = False
    Me.edDaclie.NTSOldValue = ""
    Me.edDaclie.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaclie.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaclie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaclie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaclie.Properties.AutoHeight = False
    Me.edDaclie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaclie.Properties.MaxLength = 65536
    Me.edDaclie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaclie.Size = New System.Drawing.Size(82, 20)
    Me.edDaclie.TabIndex = 1
    Me.edDaclie.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDaclie.TextInt = 0
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Location = New System.Drawing.Point(92, 3)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.AutoHeight = False
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(247, 20)
    Me.cbTipodoc.TabIndex = 0
    Me.cbTipodoc.ValueMember = ""
    '
    'cbOrigine
    '
    Me.cbOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOrigine.DataSource = Nothing
    Me.cbOrigine.DisplayMember = ""
    Me.cbOrigine.Location = New System.Drawing.Point(87, 36)
    Me.cbOrigine.Name = "cbOrigine"
    Me.cbOrigine.NTSDbField = ""
    Me.cbOrigine.Properties.AutoHeight = False
    Me.cbOrigine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOrigine.Properties.DropDownRows = 30
    Me.cbOrigine.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOrigine.SelectedValue = ""
    Me.cbOrigine.Size = New System.Drawing.Size(172, 20)
    Me.cbOrigine.TabIndex = 0
    Me.cbOrigine.ValueMember = ""
    '
    'fmPrezzi
    '
    Me.fmPrezzi.AllowDrop = True
    Me.fmPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzi.Appearance.Options.UseBackColor = True
    Me.fmPrezzi.Controls.Add(Me.lbDescvalu)
    Me.fmPrezzi.Controls.Add(Me.ckPrzNetClasseSconto)
    Me.fmPrezzi.Controls.Add(Me.ckDatamod)
    Me.fmPrezzi.Controls.Add(Me.ckValuta)
    Me.fmPrezzi.Controls.Add(Me.edDatamod)
    Me.fmPrezzi.Controls.Add(Me.edValida)
    Me.fmPrezzi.Controls.Add(Me.edValuta)
    Me.fmPrezzi.Controls.Add(Me.edListino)
    Me.fmPrezzi.Controls.Add(Me.lbValida)
    Me.fmPrezzi.Controls.Add(Me.lbListino)
    Me.fmPrezzi.Controls.Add(Me.ckPrezzi)
    Me.fmPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzi.Location = New System.Drawing.Point(12, 303)
    Me.fmPrezzi.Name = "fmPrezzi"
    Me.fmPrezzi.Size = New System.Drawing.Size(477, 104)
    Me.fmPrezzi.TabIndex = 5
    '
    'lbDescvalu
    '
    Me.lbDescvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbDescvalu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescvalu.Location = New System.Drawing.Point(160, 51)
    Me.lbDescvalu.Name = "lbDescvalu"
    Me.lbDescvalu.NTSDbField = ""
    Me.lbDescvalu.Size = New System.Drawing.Size(294, 20)
    Me.lbDescvalu.TabIndex = 9
    Me.lbDescvalu.Tooltip = ""
    Me.lbDescvalu.UseMnemonic = False
    '
    'ckPrzNetClasseSconto
    '
    Me.ckPrzNetClasseSconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrzNetClasseSconto.Enabled = False
    Me.ckPrzNetClasseSconto.Location = New System.Drawing.Point(252, 26)
    Me.ckPrzNetClasseSconto.Name = "ckPrzNetClasseSconto"
    Me.ckPrzNetClasseSconto.NTSCheckValue = "S"
    Me.ckPrzNetClasseSconto.NTSUnCheckValue = "N"
    Me.ckPrzNetClasseSconto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrzNetClasseSconto.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrzNetClasseSconto.Properties.AutoHeight = False
    Me.ckPrzNetClasseSconto.Properties.Caption = "Prezzo al netto degli sconti per classe"
    Me.ckPrzNetClasseSconto.Size = New System.Drawing.Size(212, 19)
    Me.ckPrzNetClasseSconto.TabIndex = 8
    '
    'ckDatamod
    '
    Me.ckDatamod.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDatamod.Enabled = False
    Me.ckDatamod.Location = New System.Drawing.Point(5, 76)
    Me.ckDatamod.Name = "ckDatamod"
    Me.ckDatamod.NTSCheckValue = "S"
    Me.ckDatamod.NTSUnCheckValue = "N"
    Me.ckDatamod.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDatamod.Properties.Appearance.Options.UseBackColor = True
    Me.ckDatamod.Properties.AutoHeight = False
    Me.ckDatamod.Properties.Caption = "Stampa solo articoli dove il prezzo di listino è stato modifcato dal"
    Me.ckDatamod.Size = New System.Drawing.Size(335, 19)
    Me.ckDatamod.TabIndex = 8
    '
    'ckValuta
    '
    Me.ckValuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckValuta.Enabled = False
    Me.ckValuta.Location = New System.Drawing.Point(5, 52)
    Me.ckValuta.Name = "ckValuta"
    Me.ckValuta.NTSCheckValue = "S"
    Me.ckValuta.NTSUnCheckValue = "N"
    Me.ckValuta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckValuta.Properties.Appearance.Options.UseBackColor = True
    Me.ckValuta.Properties.AutoHeight = False
    Me.ckValuta.Properties.Caption = "Anche in valuta"
    Me.ckValuta.Size = New System.Drawing.Size(96, 19)
    Me.ckValuta.TabIndex = 7
    '
    'edDatamod
    '
    Me.edDatamod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatamod.EditValue = "01/01/1900"
    Me.edDatamod.Enabled = False
    Me.edDatamod.Location = New System.Drawing.Point(354, 75)
    Me.edDatamod.Name = "edDatamod"
    Me.edDatamod.NTSDbField = ""
    Me.edDatamod.NTSForzaVisZoom = False
    Me.edDatamod.NTSOldValue = ""
    Me.edDatamod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatamod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatamod.Properties.AutoHeight = False
    Me.edDatamod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatamod.Properties.MaxLength = 65536
    Me.edDatamod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatamod.Size = New System.Drawing.Size(100, 20)
    Me.edDatamod.TabIndex = 6
    Me.edDatamod.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edValida
    '
    Me.edValida.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValida.EditValue = "01/01/1900"
    Me.edValida.Enabled = False
    Me.edValida.Location = New System.Drawing.Point(160, 25)
    Me.edValida.Name = "edValida"
    Me.edValida.NTSDbField = ""
    Me.edValida.NTSForzaVisZoom = False
    Me.edValida.NTSOldValue = ""
    Me.edValida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValida.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValida.Properties.AutoHeight = False
    Me.edValida.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValida.Properties.MaxLength = 65536
    Me.edValida.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValida.Size = New System.Drawing.Size(86, 20)
    Me.edValida.TabIndex = 5
    Me.edValida.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edValuta
    '
    Me.edValuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValuta.EditValue = "0"
    Me.edValuta.Enabled = False
    Me.edValuta.Location = New System.Drawing.Point(107, 51)
    Me.edValuta.Name = "edValuta"
    Me.edValuta.NTSDbField = ""
    Me.edValuta.NTSFormat = "0"
    Me.edValuta.NTSForzaVisZoom = False
    Me.edValuta.NTSOldValue = ""
    Me.edValuta.Properties.Appearance.Options.UseTextOptions = True
    Me.edValuta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValuta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValuta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValuta.Properties.AutoHeight = False
    Me.edValuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValuta.Properties.MaxLength = 65536
    Me.edValuta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValuta.Size = New System.Drawing.Size(53, 20)
    Me.edValuta.TabIndex = 4
    Me.edValuta.Tag = ""
    Me.edValuta.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edValuta.TextInt = 0
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edListino.EditValue = "1"
    Me.edListino.Enabled = False
    Me.edListino.Location = New System.Drawing.Point(47, 25)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = "1"
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(54, 20)
    Me.edListino.TabIndex = 3
    Me.edListino.Tag = ""
    Me.edListino.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edListino.TextInt = 1
    '
    'lbValida
    '
    Me.lbValida.AutoSize = True
    Me.lbValida.BackColor = System.Drawing.Color.Transparent
    Me.lbValida.Location = New System.Drawing.Point(117, 28)
    Me.lbValida.Name = "lbValida"
    Me.lbValida.NTSDbField = ""
    Me.lbValida.Size = New System.Drawing.Size(41, 13)
    Me.lbValida.TabIndex = 2
    Me.lbValida.Text = "Validità"
    Me.lbValida.Tooltip = ""
    Me.lbValida.UseMnemonic = False
    '
    'lbListino
    '
    Me.lbListino.AutoSize = True
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(5, 28)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(37, 13)
    Me.lbListino.TabIndex = 1
    Me.lbListino.Text = "Listino"
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'ckPrezzi
    '
    Me.ckPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrezzi.Location = New System.Drawing.Point(6, 0)
    Me.ckPrezzi.Name = "ckPrezzi"
    Me.ckPrezzi.NTSCheckValue = "S"
    Me.ckPrezzi.NTSUnCheckValue = "N"
    Me.ckPrezzi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezzi.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezzi.Properties.AutoHeight = False
    Me.ckPrezzi.Properties.Caption = "Stampa prezzi:"
    Me.ckPrezzi.Size = New System.Drawing.Size(118, 19)
    Me.ckPrezzi.TabIndex = 0
    '
    'ckArtbar
    '
    Me.ckArtbar.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArtbar.Location = New System.Drawing.Point(17, 411)
    Me.ckArtbar.Name = "ckArtbar"
    Me.ckArtbar.NTSCheckValue = "S"
    Me.ckArtbar.NTSUnCheckValue = "N"
    Me.ckArtbar.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtbar.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtbar.Properties.AutoHeight = False
    Me.ckArtbar.Properties.Caption = "Considera solo articoli che possiedono barcode"
    Me.ckArtbar.Size = New System.Drawing.Size(252, 19)
    Me.ckArtbar.TabIndex = 6
    '
    'ckTermica
    '
    Me.ckTermica.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckTermica.Location = New System.Drawing.Point(17, 436)
    Me.ckTermica.Name = "ckTermica"
    Me.ckTermica.NTSCheckValue = "S"
    Me.ckTermica.NTSUnCheckValue = "N"
    Me.ckTermica.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTermica.Properties.Appearance.Options.UseBackColor = True
    Me.ckTermica.Properties.AutoHeight = False
    Me.ckTermica.Properties.Caption = "Usa caratteristiche specifiche della stampante termica (solo in stampa su carta)"
    Me.ckTermica.Size = New System.Drawing.Size(411, 19)
    Me.ckTermica.TabIndex = 7
    '
    'ckBarcode
    '
    Me.ckBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBarcode.Enabled = False
    Me.ckBarcode.Location = New System.Drawing.Point(276, 411)
    Me.ckBarcode.Name = "ckBarcode"
    Me.ckBarcode.NTSCheckValue = "S"
    Me.ckBarcode.NTSUnCheckValue = "N"
    Me.ckBarcode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBarcode.Properties.Appearance.Options.UseBackColor = True
    Me.ckBarcode.Properties.AutoHeight = False
    Me.ckBarcode.Properties.Caption = "Solo barcode:"
    Me.ckBarcode.Size = New System.Drawing.Size(93, 19)
    Me.ckBarcode.TabIndex = 8
    '
    'cbSolobar
    '
    Me.cbSolobar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSolobar.DataSource = Nothing
    Me.cbSolobar.DisplayMember = ""
    Me.cbSolobar.Enabled = False
    Me.cbSolobar.Location = New System.Drawing.Point(366, 410)
    Me.cbSolobar.Name = "cbSolobar"
    Me.cbSolobar.NTSDbField = ""
    Me.cbSolobar.Properties.AutoHeight = False
    Me.cbSolobar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSolobar.Properties.DropDownRows = 30
    Me.cbSolobar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSolobar.SelectedValue = ""
    Me.cbSolobar.Size = New System.Drawing.Size(100, 20)
    Me.cbSolobar.TabIndex = 9
    Me.cbSolobar.ValueMember = ""
    '
    'lbTipostp
    '
    Me.lbTipostp.AutoSize = True
    Me.lbTipostp.BackColor = System.Drawing.Color.Transparent
    Me.lbTipostp.Location = New System.Drawing.Point(314, 39)
    Me.lbTipostp.Name = "lbTipostp"
    Me.lbTipostp.NTSDbField = ""
    Me.lbTipostp.Size = New System.Drawing.Size(69, 13)
    Me.lbTipostp.TabIndex = 10
    Me.lbTipostp.Text = "Tipo stampa:"
    Me.lbTipostp.Tooltip = ""
    Me.lbTipostp.UseMnemonic = False
    '
    'cbTipostp
    '
    Me.cbTipostp.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipostp.DataSource = Nothing
    Me.cbTipostp.DisplayMember = ""
    Me.cbTipostp.Location = New System.Drawing.Point(397, 36)
    Me.cbTipostp.Name = "cbTipostp"
    Me.cbTipostp.NTSDbField = ""
    Me.cbTipostp.Properties.AutoHeight = False
    Me.cbTipostp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipostp.Properties.DropDownRows = 30
    Me.cbTipostp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipostp.SelectedValue = ""
    Me.cbTipostp.Size = New System.Drawing.Size(87, 20)
    Me.cbTipostp.TabIndex = 11
    Me.cbTipostp.ValueMember = ""
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(15, 39)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(66, 13)
    Me.NtsLabel2.TabIndex = 12
    Me.NtsLabel2.Text = "Origine dati:"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'lbAvviso
    '
    Me.lbAvviso.BackColor = System.Drawing.Color.Transparent
    Me.lbAvviso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAvviso.Location = New System.Drawing.Point(12, 485)
    Me.lbAvviso.Name = "lbAvviso"
    Me.lbAvviso.NTSDbField = ""
    Me.lbAvviso.Size = New System.Drawing.Size(477, 17)
    Me.lbAvviso.TabIndex = 13
    Me.lbAvviso.Text = "Nessuna operazione in corso."
    Me.lbAvviso.Tooltip = ""
    Me.lbAvviso.UseMnemonic = False
    '
    'edNEtichettePerTC
    '
    Me.edNEtichettePerTC.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNEtichettePerTC.EditValue = "0"
    Me.edNEtichettePerTC.Location = New System.Drawing.Point(366, 461)
    Me.edNEtichettePerTC.Name = "edNEtichettePerTC"
    Me.edNEtichettePerTC.NTSDbField = ""
    Me.edNEtichettePerTC.NTSFormat = "0"
    Me.edNEtichettePerTC.NTSForzaVisZoom = False
    Me.edNEtichettePerTC.NTSOldValue = "0"
    Me.edNEtichettePerTC.Properties.Appearance.Options.UseTextOptions = True
    Me.edNEtichettePerTC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNEtichettePerTC.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNEtichettePerTC.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNEtichettePerTC.Properties.AutoHeight = False
    Me.edNEtichettePerTC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNEtichettePerTC.Properties.MaxLength = 65536
    Me.edNEtichettePerTC.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNEtichettePerTC.Size = New System.Drawing.Size(54, 20)
    Me.edNEtichettePerTC.TabIndex = 15
    Me.edNEtichettePerTC.Tag = ""
    Me.edNEtichettePerTC.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edNEtichettePerTC.TextInt = 0
    '
    'lbNEtichettePerTC
    '
    Me.lbNEtichettePerTC.AutoSize = True
    Me.lbNEtichettePerTC.BackColor = System.Drawing.Color.Transparent
    Me.lbNEtichettePerTC.Location = New System.Drawing.Point(17, 464)
    Me.lbNEtichettePerTC.Name = "lbNEtichettePerTC"
    Me.lbNEtichettePerTC.NTSDbField = ""
    Me.lbNEtichettePerTC.Size = New System.Drawing.Size(312, 13)
    Me.lbNEtichettePerTC.TabIndex = 14
    Me.lbNEtichettePerTC.Text = "Numero etichette aggiuntive per articoli gestiti a 'Taglie & Colori'"
    Me.lbNEtichettePerTC.Tooltip = ""
    Me.lbNEtichettePerTC.UseMnemonic = False
    '
    'FRMMGETTE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(506, 515)
    Me.Controls.Add(Me.edNEtichettePerTC)
    Me.Controls.Add(Me.lbNEtichettePerTC)
    Me.Controls.Add(Me.lbAvviso)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.cbOrigine)
    Me.Controls.Add(Me.cbTipostp)
    Me.Controls.Add(Me.lbTipostp)
    Me.Controls.Add(Me.cbSolobar)
    Me.Controls.Add(Me.ckBarcode)
    Me.Controls.Add(Me.ckTermica)
    Me.Controls.Add(Me.ckArtbar)
    Me.Controls.Add(Me.fmPrezzi)
    Me.Controls.Add(Me.fmOrigine)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGETTE"
    Me.Text = "STAMPA ETICHETTE ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOrigine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrigine.ResumeLayout(False)
    CType(Me.pnArtico, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnArtico.ResumeLayout(False)
    Me.pnArtico.PerformLayout()
    CType(Me.edArtico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLista, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLista.ResumeLayout(False)
    Me.pnLista.PerformLayout()
    CType(Me.ckNumeti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLista.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri.ResumeLayout(False)
    Me.pnFiltri.PerformLayout()
    CType(Me.edTaglia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEtiche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDoc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDoc.ResumeLayout(False)
    Me.pnDoc.PerformLayout()
    CType(Me.fmStampe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStampe.ResumeLayout(False)
    Me.fmStampe.PerformLayout()
    CType(Me.ckMisura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumMolt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOperazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStampe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opQta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opColli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAriga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDariga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAserie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaserie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAanno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaanno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAclie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaclie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOrigine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzi.ResumeLayout(False)
    Me.fmPrezzi.PerformLayout()
    CType(Me.ckDatamod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckValuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatamod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckArtbar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTermica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSolobar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipostp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNEtichettePerTC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      cbOrigine.NTSSetParam(oApp.Tr(Me, 128661395876406250, "Origine dati"))
      cbTipostp.NTSSetParam(oApp.Tr(Me, 128661395876562500, "Tipo stampa"))
      cbSolobar.NTSSetParam(oApp.Tr(Me, 128661395876875000, "Solo barcode"))
      ckBarcode.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877031250, "Solo barcode:"), "S", "N")
      ckTermica.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877187500, "Usa caratteristiche specifiche della stampate termica (solo in stampa su carta)"), "S", "N")
      ckArtbar.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877343750, "Considera solo articoli che possiedono barcode"), "S", "N")
      ckDatamod.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877500000, "Stampa solo articoli dove il prezzo di listino è stato modifcato dal"), "S", "N")
      ckValuta.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877656250, "Anche in valuta"), "S", "N")
      edDatamod.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877812500, "Data modifica"), False)
      edValida.NTSSetParam(oMenu, oApp.Tr(Me, 128661395877968750, "Validità"), False)
      edValuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128661395878125000, "Anche in valuta"), tabvalu)
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128661395878281250, "Listino"), tablist)
      ckPrezzi.NTSSetParam(oMenu, oApp.Tr(Me, 128661395878750000, "Stampa prezzi:"), "S", "N")
      ckNumeti.NTSSetParam(oMenu, oApp.Tr(Me, 128873747451955405, "Utilizza numero di etichette della lista selezionata articoli (se presente)"), "S", "N")
      edLista.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128661395879218750, "Lista selezionata articoli"), tablsar)
      opColli.NTSSetParam(oMenu, oApp.Tr(Me, 128661395879375000, "Colonna Colli"), "C")
      opQta.NTSSetParam(oMenu, oApp.Tr(Me, 128661395879531250, "Colonna Quantità"), "Q")
      cbOperazione.NTSSetParam(oApp.Tr(Me, 128661395879687500, "Moltiplica/Dividi"))
      edNumMolt.NTSSetParam(oMenu, oApp.Tr(Me, 128661395880000000, "Moltiplica/Dividi per"), "0", 4, 1, 9999)
      ckMisura.NTSSetParam(oMenu, oApp.Tr(Me, 128661395880156250, "Rileva moltip./divisore dalla Misura 1 dell'articolo"), "S", "N")
      ckStampe.NTSSetParam(oMenu, oApp.Tr(Me, 128661395880312500, "Modifica numero etichette da stampare"), "S", "N")
      edAserie.NTSSetParam(oMenu, oApp.Tr(Me, 128661395882656250, "A serie"), CLN__STD.SerieMaxLen)
      edDaserie.NTSSetParam(oMenu, oApp.Tr(Me, 128661395882812500, "Da serie"), CLN__STD.SerieMaxLen)
      edDanumero.NTSSetParam(oMenu, oApp.Tr(Me, 128661395882968750, "Da numero"), "0", 9, 0, 999999999)
      edAnumero.NTSSetParam(oMenu, oApp.Tr(Me, 128661395883125000, "A numero"), "0", 9, 0, 999999999)
      edDariga.NTSSetParam(oMenu, oApp.Tr(Me, 128661395883281250, "Da riga"), "0", 9, 0, 999999999)
      edAriga.NTSSetParam(oMenu, oApp.Tr(Me, 128661395883437500, "A riga"), "0", 9, 0, 999999999)
      edAanno.NTSSetParam(oMenu, oApp.Tr(Me, 128661395883593750, "A anno"), "0", 4, 1900, 2099)
      edDaanno.NTSSetParam(oMenu, oApp.Tr(Me, 128661395883750000, "Da anno"), "0", 4, 1900, 2099)
      edDaclie.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128661395883906250, "Da cliente"), tabanagrac)
      edAclie.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128661395884062500, "A cliente"), tabanagrac)
      edDadata.NTSSetParam(oMenu, oApp.Tr(Me, 128661395884218750, "Da data"), False)
      edAdata.NTSSetParam(oMenu, oApp.Tr(Me, 128661395884375000, "A data"), False)
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 128661395884531250, "Tipo documento"))
      cbBarcode.NTSSetParam(oApp.Tr(Me, 128661395885000000, "Solo barcode"))
      edEtiche.NTSSetParam(oMenu, oApp.Tr(Me, 128661395885156250, "Numero etichette"), "0", 5, 1, 10000)
      edTaglia.NTSSetParam(oMenu, oApp.Tr(Me, 128661974142343750, "Taglia"), 5, True)
      edArtico.NTSSetParam(oMenu, oApp.Tr(Me, 128680112815061402, "Articolo"), 1000)
      edNEtichettePerTC.NTSSetParam(oMenu, oApp.Tr(Me, 130138783130489250, "Numero etichette aggiuntive per articoli gestiti a 'Taglie & Colori'"), "0", 3, 0, 100)

      edAserie.Text = "".PadRight(CLN__STD.SerieMaxLen, "z"c)
      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Toolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim x As Integer
    Dim strNomRpt As String = ""
    Dim strKey As String = ""
    Dim lIIstMatrTmp As Integer
    Dim bDocMagazz As Boolean = False
    Dim bDaDoc As Boolean = False
    Dim strCode As String = ""
    Dim strCodartList As String = ""
    Dim strCodart As String = ""
    Dim bAvvisare As Boolean = True
    Dim dttOut As New DataTable
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim frmEtba As FRMMGETBA = Nothing
    Dim nNEtichettePerTC As Integer = 0

    Try
      ' Valido l'ultimo dato inserito
      Me.ValidaLastControl()

      If Not ControlliAggiuntivi() Then Return

      oMenu.ResetTblInstId("TTSTMATS", bsDbAzienda, lIIstMats)
      oMenu.ResetTblInstId("TTSTMATR", bsDbAzienda, lIIstMatr)

      lIIstMatrTmp = oMenu.GetTblInstId("TTSTMATR", bsDbAzienda)
      '--- Controlla barcode mancanti

      '-----------------------------------------------------------------------------------------
      '--- Riempie la tabella temporanea
      Windows.Forms.Cursor.Current = Cursors.WaitCursor
      If Not oCleEtte.LeggiTable(cbOrigine.SelectedValue, bBD_DaCodarfo, edArtico.Text, strArticoli, strWhereArtico, NTSCInt(edLista.Text), cbTipodoc.SelectedValue, _
                                 bCreaBarcode, NTSCInt(edDaclie.Text), NTSCInt(edAclie.Text), NTSCDate(edDadata.Text), NTSCDate(edAdata.Text), _
                                 NTSCInt(edDaanno.Text), NTSCInt(edAanno.Text), edDaserie.Text, edAserie.Text, NTSCInt(edDanumero.Text), _
                                 NTSCInt(edAnumero.Text), NTSCInt(edDariga.Text), NTSCInt(edAriga.Text), opColli.Checked, dttOut, dttTmp, dttTmp2, _
                                 bDocMagazz, bDaDoc, strCode, strCodartList, strCodart, bAvvisare) Then Return

      '-----------------------------------------------------------------------------------------
      '--- Esecuzione della finestra sui Barcode
      '-----------------------------------------------------------------------------------------
      'Controllo se esistono dei codici a barre
      If bNEtichetteBarCode Then
        For i = 0 To dttOut.Rows.Count - 1
          dttTmp = Nothing
          If Not oCleEtte.SelectBarcode(ckBarcode.Checked, cbSolobar.SelectedValue, NTSCStr(dttOut.Rows(i)!Codart), dttTmp) Then Return

          If dttTmp.Rows.Count <> 0 Then
            If Not oCleEtte.SelectTTstmatr(lIIstMatrTmp, NTSCStr(dttOut.Rows(i)!Codart), dttTmp2) Then Return

            If dttTmp2.Rows.Count = 0 Then
              If dttTmp.Rows.Count > 1 Then
                For x = 0 To dttTmp.Rows.Count - 1
                  If Not oCleEtte.InsertBarcodeTTstmatr(lIIstMatrTmp, NTSCStr(dttOut.Rows(i)!Codart), NTSCStr(dttTmp.Rows(x)!bc_code), _
                                                        NTSCDec(dttTmp.Rows(x)!bc_quant), NTSCStr(dttTmp.Rows(x)!bc_unmis)) Then Return
                Next

                frmEtba = CType(NTSNewFormModal("FRMMGETBA"), FRMMGETBA)
                If Not frmEtba.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
                frmEtba.lIIstMatrTmp = lIIstMatrTmp
                frmEtba.InitEntity(oCleEtte)
                frmEtba.strCodart = NTSCStr(dttOut.Rows(i)!Codart)
                frmEtba.ShowDialog(Me)

                If Not frmEtba.bOk Then Return
              End If
            End If
          End If
        Next
      End If

      Me.Refresh()

      If edNEtichettePerTC.Visible = True Then nNEtichettePerTC = NTSCInt(edNEtichettePerTC.Text)

      If Not oCleEtte.RiempiTmpTable(ckPrezzi.Checked, ckDatamod.Checked, NTSCDate(edDatamod.Text), ckBarcode.Checked, cbSolobar.SelectedValue, _
                                     cbBarcode.SelectedValue, ckArtbar.Checked, cbOrigine.SelectedValue, bBD_DaCodarfo, NTSCInt(edLista.Text), opColli.Checked, _
                                     bNEtichetteBarCode, ckValuta.Checked, NTSCInt(edListino.Text), NTSCDate(edValida.Text), NTSCInt(edValuta.Text), _
                                     ckStampe.Checked, cbOperazione.SelectedValue, ckMisura.Checked, NTSCInt(edNumMolt.Text), bDividiQuantConfez, _
                                     lIIstMatr, lIIstMats, bUsaTtstmatr, NTSCInt(edEtiche.Text), nBD_Listino, bModTCO, edTaglia.Text, lIIstMatrTmp, _
                                     bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, bDaDoc, strCode, _
                                     strCodartList, strCodart, bAvvisare, nNEtichettePerTC, ckNumeti.Checked, ckPrzNetClasseSconto.Checked) Then Return

      'oMenu.ResetTblInstId("TTSTMATR", bsDbAzienda, lIIstMatrTmp)
      '-------------------------------------------------------------------------------------------
      If ckTermica.Checked And nDestin = 1 Then
        If bDometic Then
          StampaDirettaDometic()
        Else
          '--- Stampo direttamente sulla stampante termica senza usare Crystal report
          StampaDiretta()
        End If
        Return
      End If
        '-----------------------------------------------------------------------------------------
        '--- determina il nome del report da stampare e la cartella relativa del registro
        '-----------------------------------------------------------------------------------------
        SettaNomeReport(strKey, strNomRpt)
        '-----------------------------------------------------------------------------------------
        '--- Lancia le stampe
        '-----------------------------------------------------------------------------------------
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGETTE", strKey, " ", 0, nDestin, strNomRpt, False, "Stampa etichette", False)
        '-----------------------------------------------------------------------------------------
        '--- Se la stampa è stata annullata esce
        '-----------------------------------------------------------------------------------------
        If nPjob Is Nothing Then Return
        '-----------------------------------------------------------------------------------------
        '--- Formula di selezione
        '-----------------------------------------------------------------------------------------
        If bUsaTtstmatr And cbOrigine.SelectedValue = "M" Then
          strCrpe = "{artico.codditt} = '" & DittaCorrente & "'" & _
                    " And {ttstmatr.codditt} = '" & DittaCorrente & "'" & _
                    " And {ttstmatr.instid} = " & lIIstMatr
        Else
          strCrpe = "{artico.codditt} = '" & DittaCorrente & "'" & _
                    " And {ttstmats.codditt} = '" & DittaCorrente & "'" & _
                    " And {ttstmats.instid} = " & lIIstMats
        End If

        '--------------------------------------------------
        'lancio tutti gli eventuali reports (gestisce già il multireport)
        For i = 1 To UBound(CType(nPjob, Array), 2)
        If i = 1 Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STAMPAPREZZI", "'" & NTSCStr(IIf(ckPrezzi.Checked, "S", "N")) & "'")
          If ckPrezzi.Checked Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAVAL", "'" & edValida.Text & "'")
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO", edListino.Text)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STAMPAPREZZIVAL", "'" & NTSCStr(IIf(ckValuta.Checked, "S", "N")) & "'")
            If ckValuta.Checked Then
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CODVALU", edValuta.Text)
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESVALU", "'" & lbDescvalu.Text & "'")
            End If
          End If
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strCrpe)
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Else
          If Trim$(NTSCStr(CType(nPjob, Array).GetValue(2, i))) <> "" Then
            'RecordSelectionFormula
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STAMPAPREZZI", "'" & NTSCStr(IIf(ckPrezzi.Checked, "S", "N")) & "'")
          If ckPrezzi.Checked Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAVAL", edValida.Text)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO", edListino.Text)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STAMPAPREZZIVAL", "'" & NTSCStr(IIf(ckValuta.Checked, "S", "N")) & "'")
            If ckValuta.Checked Then
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CODVALU", edValuta.Text)
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESVALU", "'" & lbDescvalu.Text & "'")
            End If
          End If
          For j = 3 To 12
            'FormulaJ
            If Trim$(NTSCStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
              nRis = oMenu.PESetFormula(CInt(CType(nPjob, Array).GetValue(0, i)), NTSCStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(j + 10, i))))
            End If
          Next j
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        End If
        Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmEtba Is Nothing Then frmEtba.Dispose()
      frmEtba = Nothing
      Windows.Forms.Cursor.Current = Cursors.Default
      lbAvviso.Text = oApp.Tr(Me, 130421093076039252, "Nessuna operazione in corso.")
    End Try
  End Sub
  Public Overridable Sub SettaNomeReport(ByRef strRptKey As String, ByRef strNomeRpt As String)
    Try
      '-----------------------------------------------------------------------------------------
      If bUsaTtstmatr And cbOrigine.SelectedValue = "M" Then
        '--- Dati di riga movimento in etichetta
        If cbTipostp.SelectedValue = "0" Then
          '--- Monopista
          If Not ckStampe.Checked Then
            strRptKey = "Reports3"
            strNomeRpt = "BSMGETT3.RPT"
          Else
            If cbOperazione.SelectedValue = "1" Then
              '--- Moltiplicatore
              strRptKey = "Reports9"
              strNomeRpt = "BSMGETT9.RPT"
            Else
              '--- Divisore
              strRptKey = "Reports10"
              strNomeRpt = "BSMGETT10.RPT"
            End If
          End If
        Else
          '--- Multipista
          If Not ckStampe.Checked Then
            strRptKey = "Reports4"
            strNomeRpt = "BSMGETT4.RPT"
          Else
            If cbOperazione.SelectedValue = "1" Then
              '--- Moltiplicatore
              strRptKey = "Reports11"
              strNomeRpt = "BSMGETT11.RPT"
            Else
              '--- Divisore
              strRptKey = "Reports12"
              strNomeRpt = "BSMGETT12.RPT"
            End If
          End If
        End If
      Else
        '--- Solo dati di articolo in etichetta
        If cbTipostp.SelectedValue = "0" Then
          '--- Monopista
          If Not ckStampe.Checked Then
            strRptKey = "Reports1"
            strNomeRpt = "BSMGETTE.RPT"
          Else
            If cbOperazione.SelectedValue = "1" Then
              '--- Moltiplicatore
              strRptKey = "Reports5"
              strNomeRpt = "BSMGETT5.RPT"
            Else
              '--- Divisore
              strRptKey = "Reports6"
              strNomeRpt = "BSMGETT6.RPT"
            End If
          End If
        Else
          '--- Multipista
          If Not ckStampe.Checked Then
            strRptKey = "Reports2"
            strNomeRpt = "BSMGETT1.RPT"
          Else
            If cbOperazione.SelectedValue = "1" Then
              '--- Mmoltiplicatore
              strRptKey = "Reports7"
              strNomeRpt = "BSMGETT7.RPT"
            Else
              '--- Divisore
              strRptKey = "Reports8"
              strNomeRpt = "BSMGETT8.RPT"
            End If
          End If
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttOrigine As New DataTable()
    Dim dttBarcode As New DataTable()
    Dim dttSolobar As New DataTable()
    Dim dttTipostp As New DataTable()
    Dim dttTipodoc As New DataTable()
    Dim dttOperazione As New DataTable()
    Try
      dttOrigine.Columns.Add("cod", GetType(String))
      dttOrigine.Columns.Add("val", GetType(String))
      dttOrigine.Rows.Add(New Object() {"A", "Selezione articoli"})
      dttOrigine.Rows.Add(New Object() {"L", "Lista selezionata articoli"})
      If Not bBD_DaCodarfo Then dttOrigine.Rows.Add(New Object() {"M", "Ordini/Documenti di magazzino"})
      dttOrigine.AcceptChanges()
      cbOrigine.DataSource = dttOrigine
      cbOrigine.ValueMember = "cod"
      cbOrigine.DisplayMember = "val"

      dttBarcode.Columns.Add("cod", GetType(String))
      dttBarcode.Columns.Add("val", GetType(String))
      dttBarcode.Rows.Add(New Object() {"P", "Principale"})
      dttBarcode.Rows.Add(New Object() {"C", "Confezione"})
      dttBarcode.Rows.Add(New Object() {"S", "Secondaria"})
      dttBarcode.AcceptChanges()
      cbBarcode.DataSource = dttBarcode
      cbBarcode.ValueMember = "cod"
      cbBarcode.DisplayMember = "val"

      dttTipodoc.Columns.Add("cod", GetType(String))
      dttTipodoc.Columns.Add("val", GetType(String))
      dttTipodoc.Rows.Add(New Object() {"A", "Fatture imm. emesse"})
      dttTipodoc.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipodoc.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipodoc.Rows.Add(New Object() {"D", "Fatture diff. emesse"})
      dttTipodoc.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipodoc.Rows.Add(New Object() {"F", "Ric. fiscale emessa"})
      dttTipodoc.Rows.Add(New Object() {"I", "Riemissione ric. fiscali"})
      dttTipodoc.Rows.Add(New Object() {"J", "Note accr. ricevute"})
      dttTipodoc.Rows.Add(New Object() {"K", "Fatture diff. ricevute"})
      dttTipodoc.Rows.Add(New Object() {"L", "Fatture imm. ricevute"})
      dttTipodoc.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipodoc.Rows.Add(New Object() {"N", "Note accr. emesse"})
      dttTipodoc.Rows.Add(New Object() {"P", "Fatt. Ric. Fisc. differita"})
      dttTipodoc.Rows.Add(New Object() {"S", "Fatt. Ric. Fisc. emessa"})
      dttTipodoc.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipodoc.Rows.Add(New Object() {"U", "Scarico da produzione"})
      dttTipodoc.Rows.Add(New Object() {"Z", "Bolle di mov. interna"})
      dttTipodoc.Rows.Add(New Object() {"W", "Note di prelievo"})
      dttTipodoc.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipodoc.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipodoc.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipodoc.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipodoc.Rows.Add(New Object() {"Q", "Prventivo"})
      dttTipodoc.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipodoc.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipodoc.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipodoc.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      dttTipodoc.AcceptChanges()
      cbTipodoc.DataSource = dttTipodoc
      cbTipodoc.ValueMember = "cod"
      cbTipodoc.DisplayMember = "val"

      dttTipostp.Columns.Add("cod", GetType(String))
      dttTipostp.Columns.Add("val", GetType(String))
      dttTipostp.Rows.Add(New Object() {"0", "A una pista"})
      dttTipostp.Rows.Add(New Object() {"1", "A più piste"})
      dttTipostp.AcceptChanges()
      cbTipostp.DataSource = dttTipostp
      cbTipostp.ValueMember = "cod"
      cbTipostp.DisplayMember = "val"

      dttOperazione.Columns.Add("cod", GetType(String))
      dttOperazione.Columns.Add("val", GetType(String))
      dttOperazione.Rows.Add(New Object() {"1", "Moltiplica"})
      dttOperazione.Rows.Add(New Object() {"2", "Dividi"})
      dttOperazione.AcceptChanges()
      cbOperazione.DataSource = dttOperazione
      cbOperazione.ValueMember = "cod"
      cbOperazione.DisplayMember = "val"

      dttSolobar.Columns.Add("cod", GetType(String))
      dttSolobar.Columns.Add("val", GetType(String))
      dttSolobar.Rows.Add("E", "Ean 13")
      dttSolobar.Rows.Add("8", "Ean 8")
      dttSolobar.Rows.Add("C", "Code 39")
      dttSolobar.Rows.Add("2", "2 su 5 inter.")
      dttSolobar.Rows.Add("I", "ISBN")
      dttSolobar.Rows.Add("F", "Farmac.")
      dttSolobar.Rows.Add("A", "Altro industr.")
      dttSolobar.Rows.Add("U", "UPC")
      dttSolobar.AcceptChanges()
      cbSolobar.DataSource = dttSolobar
      cbSolobar.ValueMember = "cod"
      cbSolobar.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Validated/Checked/Click"
  Public Overridable Sub cbOrigine_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrigine.EditValueChanged
    Try
      Select Case cbOrigine.SelectedValue
        Case "A"
          VisArticolo(True)
          VisLista(False)
          VisFiltri(True)
          VisMagazzino(False)
        Case "L"
          VisArticolo(False)
          VisLista(True)
          VisFiltri(True)
          VisMagazzino(False)
        Case "M"
          VisArticolo(False)
          VisLista(False)
          VisFiltri(False)
          VisMagazzino(True)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckStampe_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckStampe.CheckedChanged
    Try
      cbOperazione.Enabled = Not cbOperazione.Enabled
      edNumMolt.Enabled = Not edNumMolt.Enabled
      ckMisura.Enabled = Not ckMisura.Enabled
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckPrezzi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPrezzi.CheckedChanged
    Try
      edListino.Enabled = Not edListino.Enabled
      edValida.Enabled = Not edValida.Enabled
      ckValuta.Enabled = Not ckValuta.Enabled
      ckValuta.Checked = False
      ckDatamod.Enabled = Not ckDatamod.Enabled
      ckDatamod.Checked = False
      ckPrzNetClasseSconto.Enabled = ckDatamod.Enabled
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckValuta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckValuta.CheckedChanged
    Try
      edValuta.Enabled = Not edValuta.Enabled
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDatamod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDatamod.CheckedChanged
    Try
      edDatamod.Enabled = Not edDatamod.Enabled
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckArtbar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckArtbar.CheckedChanged
    Try
      ckBarcode.Enabled = Not ckBarcode.Enabled
      ckBarcode.Checked = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBarcode.CheckedChanged
    Try
      cbSolobar.Enabled = Not cbSolobar.Enabled
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edLista_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLista.Validated
    Try
      If Not oCleEtte.ValidaCodice(edLista.Text, "N", "TABLSAR", lbDesclista.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128662009224375000, "Codice lista selezionata articoli inesistente"))
        edLista.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValuta_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edValuta.Validated
    Try
      If Not oCleEtte.ValidaCodice(edValuta.Text, "N", "TABVALU", lbDescvalu.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128662009808437500, "Codice valuta inesistente"))
        edValuta.Text = "0"
        lbDescvalu.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDaclie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaclie.Validated
    Try
      If NTSCInt(edDaclie.Text) > NTSCInt(edAclie.Text) Then
        edDaclie.Text = "0"
        oApp.MsgBoxErr(oApp.Tr(Me, 128662027642656250, "'Da cliente' deve essere inferiore o uguale a 'A cliente'"))
      End If

      If Not NTSCInt(edDaclie.Text) = 0 Then
        edAclie.Text = edDaclie.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAclie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAclie.Validated
    Try
      If NTSCInt(edDaclie.Text) > NTSCInt(edAclie.Text) Then
        edAclie.Text = "999999999"
        oApp.MsgBoxErr(oApp.Tr(Me, 128873747568355695, "'Da cliente' deve essere inferiore o uguale ad 'A cliente'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDadata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDadata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAdata.Text) Then
        edDadata.Text = IntSetDate("01/01/" + NTSCStr(Now.Year))
        oApp.MsgBoxErr(oApp.Tr(Me, 128662028961406250, "'Da data' deve essere precedente o uguale ad 'A data'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAdata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAdata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAdata.Text) Then
        edAdata.Text = IntSetDate("31/12/" + NTSCStr(Now.Year))
        oApp.MsgBoxErr(oApp.Tr(Me, 128873747664444525, "'Da data' deve essere precedente o uguale ad 'A data'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaanno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaanno.Validated
    Try
      If NTSCInt(edDaanno.Text) > NTSCInt(edAanno.Text) Then
        edDaanno.Text = "1900"
        oApp.MsgBoxErr(oApp.Tr(Me, 128662043726093750, "'Da anno' deve essere precedente o uguale ad 'A anno'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAanno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAanno.Validated
    Try
      If NTSCInt(edDaanno.Text) > NTSCInt(edAanno.Text) Then
        edAanno.Text = "2099"
        oApp.MsgBoxErr(oApp.Tr(Me, 128873747772876473, "'Da anno' deve essere precedente o uguale ad 'A anno'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaserie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaserie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edDaserie.Text, False)
      If strTmp <> edDaserie.Text Then edDaserie.Text = strTmp

      If edDaserie.Text > edAserie.Text Then
        edDaserie.Text = " "
        oApp.MsgBoxErr(oApp.Tr(Me, 128662045694375000, "'Da serie' deve essere minore o uguale ad 'A serie'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAserie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAserie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edAserie.Text, False)
      If strTmp <> edAserie.Text Then edAserie.Text = strTmp

      If edDaserie.Text > edAserie.Text Then
        edAserie.Text = "z"
        oApp.MsgBoxErr(oApp.Tr(Me, 128873747873965047, "'Da serie' deve essere minore o uguale ad 'A serie'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDanumero_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDanumero.Validated
    Try
      If edDaserie.Text.Equals(edAserie.Text) Then
        If NTSCInt(edDanumero.Text) > NTSCInt(edAnumero.Text) Then
          edDanumero.Text = "0"
          oApp.MsgBoxErr(oApp.Tr(Me, 128662061962187500, "'Da numero' deve essere minore o uguale ad 'A numero', se 'Da serie' è uguale ad 'A serie'"))
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAnumero_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnumero.Validated
    Try
      If edDaserie.Text.Equals(edAserie.Text) Then
        If NTSCInt(edDanumero.Text) > NTSCInt(edAnumero.Text) Then
          edAnumero.Text = "999999999"
          oApp.MsgBoxErr(oApp.Tr(Me, 128873747957085791, "'Da numero' deve essere minore o uguale ad 'A numero', se 'Da serie' è uguale ad 'A serie'"))
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDariga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDariga.Validated
    Try
      If NTSCInt(edDariga.Text) > NTSCInt(edAriga.Text) Then
        edDariga.Text = "0"
        oApp.MsgBoxErr(oApp.Tr(Me, 128662063536093750, "'Da riga' deve essere minore o uguale ad 'A riga'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAriga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAriga.Validated
    Try
      If NTSCInt(edDariga.Text) > NTSCInt(edAriga.Text) Then
        edAriga.Text = "999999999"
        oApp.MsgBoxErr(oApp.Tr(Me, 128873748047081183, "'Da riga' deve essere minore o uguale ad 'A riga'"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdArtico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtico.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      strWhereArtico = oPar.strOut.Trim
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub VisArticolo(ByVal bValue As Boolean)
    Try
      pnArtico.Visible = bValue
      If Not bBD_DaCodarfo Then
        cmdArtico.Visible = bValue
      Else
        edArtico.Visible = bValue
        lbArtico.Visible = bValue
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub VisLista(ByVal bValue As Boolean)
    Try
      pnLista.Visible = bValue
      lbLista.Visible = bValue
      edLista.Visible = bValue
      lbDesclista.Visible = bValue
      ckNumeti.Visible = bValue
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub VisFiltri(ByVal bValue As Boolean)
    Try
      pnFiltri.Visible = bValue
      lbEtiche.Visible = bValue
      edEtiche.Visible = bValue
      lbBarcode.Visible = bValue
      cbBarcode.Visible = bValue
      If bModTCO Then
        lbTaglia.Visible = bValue
        edTaglia.Visible = bValue
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub VisMagazzino(ByVal bValue As Boolean)
    Try
      pnDoc.Visible = bValue
      lbTipodoc.Visible = bValue
      cbTipodoc.Visible = bValue
      lbDaclie.Visible = bValue
      edDaclie.Visible = bValue
      lbAclie.Visible = bValue
      edAclie.Visible = bValue
      lbDadata.Visible = bValue
      edDadata.Visible = bValue
      lbAdata.Visible = bValue
      edAdata.Visible = bValue
      lbAserie.Visible = bValue
      edAserie.Visible = bValue
      lbDaserie.Visible = bValue
      edDaserie.Visible = bValue
      lbDanumero.Visible = bValue
      edDanumero.Visible = bValue
      lbAnumero.Visible = bValue
      edAnumero.Visible = bValue
      lbDariga.Visible = bValue
      edDariga.Visible = bValue
      lbAriga.Visible = bValue
      edAriga.Visible = bValue
      lbNumeti.Visible = bValue
      opColli.Visible = bValue
      opQta.Visible = bValue
      fmStampe.Visible = bValue
      cbOperazione.Visible = bValue
      lbPer.Visible = bValue
      edNumMolt.Visible = bValue
      CkMisura.Visible = bValue
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiDatiMagaz(ByVal strData As String, ByVal strConto As String)
    Try
      edDadata.Text = strData 'NTSCStr(dttTmp.Rows(0)!tm_datdoc)
      edAdata.Text = strData 'NTSCStr(dttTmp.Rows(0)!tm_datdoc)
      edDaclie.Text = strConto ' NTSCStr(dttTmp.Rows(0)!tm_conto)
      edAclie.Text = strConto 'NTSCStr(dttTmp.Rows(0)!tm_conto)
      '----------------------------------------------------------------
      cbTipodoc.SelectedValue = strTipoDocu ' da controllare se va bene
      '----------------------------------------------------------------
      edDaanno.Text = NTSCStr(nAnnoDocu)
      edAanno.Text = NTSCStr(nAnnoDocu)
      edDaserie.Text = NTSCStr(strSerieDocu)
      edAserie.Text = NTSCStr(strSerieDocu)
      edDanumero.Text = NTSCStr(lNumDocu)
      edAnumero.Text = NTSCStr(lNumDocu)
      If lRigaDocu > 0 Then
        edDariga.Text = NTSCStr(lRigaDocu)
        edAriga.Text = NTSCStr(lRigaDocu)
      Else
        edDariga.Text = "0"
        edAriga.Text = "999999999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim strTmp As String = ""

    If Me.TopMost Then
      'vedi il caso: bncgpart chiama da stampa griglia bncgprin che in apertura reg chiede pwd 
      Me.TopMost = False
      Application.DoEvents()
    End If

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Dim oCtrl As Control = Me.NTSFindControlFocused(Me)

    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY
    ''' e.RetValue = "" NO! Altrimenti non funzionano i valori di ritorno delle ThrowRemoteEvent con ThMsg.MSGBOX_XXXXX
    If e.Message <> "" Then
      Select Case e.TipoEvento
        Case "LABEL"
          lbAvviso.Text = e.Message
          lbAvviso.Refresh()
      End Select
    End If
  End Sub

  Public Overridable Sub StampaDirettaDometic()
    Dim oPort As New CLE__PORT
      
    Dim strSTPName As String
    Dim strSTPReset As String
    Dim strSTPAsciiEnter As String
    Dim strSTPTermEtichetta As String
    Dim strIdentificativoPezzo As String

    Dim strEnter() As String
    Dim i As Integer

    Dim dttDometic As New DataTable
    Dim strCodart As String
    Dim strCodalt As String, strDesart As String
    Dim nCount As Integer
    Dim lNrEtichette As Integer
    Dim dttArti As New DataTable
    Try
      strSTPName = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermNome", "", ".", ""))
      strSTPAsciiEnter = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermAsciiEnter", "", ".", ""))
      strSTPTermEtichetta = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "STPTermEtichetta", "", ".", ""))
      strSTPReset = ""

      If strSTPName = "" Or strSTPTermEtichetta = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128677754664062500, "Impossibile stampare: non tutte le opzioni di registro necessarie sono state settate!"))
        Return
      Else
        strEnter = Split(strSTPAsciiEnter, ";")
      End If

      'inizio la stampa

      i = oPort.OpenPort(strSTPName)

      i = oPort.StartDocPort(Me.Text, vbNullString, vbNullString)
      oPort.StartPagePort()

      If Not oCleEtte.DatiTermicaDometic(cbOrigine.SelectedValue, bUsaTtstmatr, lIIstMatr, lIIstMats, dttDometic) Then Return
      'elaboro i dati

      nCount = 1
      If dttDometic.Rows.Count <> 0 Then lNrEtichette = NTSCInt(dttDometic.Rows(0)!nrec)
      Do While lNrEtichette <> 0
        If Not oCleEtte.LeggiArtico(NTSCStr(dttDometic.Rows(0)!tt_codart), dttArti) Then Return

        strCodalt = NTSCStr(dttArti.Rows(0)!ar_codalt)
        If NTSCStr(dttArti.Rows(0)!ar_descr).Length > 35 Then
          strDesart = Microsoft.VisualBasic.Left(NTSCStr(dttArti.Rows(0)!ar_descr).PadLeft(35), 35)
        Else
          strDesart = NTSCStr(dttArti.Rows(0)!ar_descr)
        End If

        strCodart = NTSCStr(dttDometic.Rows(0)!tt_codart)

        'Dato il testo per l'etichetta sostituisco i marcatori con i rispettivi valori
        'IDPZ = H + aaaa + s + nnnnnn + rr + zzzz + llll
        strIdentificativoPezzo = "H" & Microsoft.VisualBasic.Right(NTSCStr(dttDometic.Rows(0)!tt_anno).PadLeft(4, "0"c), 4) & _
                                       Microsoft.VisualBasic.Right(NTSCStr(dttDometic.Rows(0)!tt_serie).PadLeft(1), 1) & _
                                       Microsoft.VisualBasic.Right(NTSCStr(dttDometic.Rows(0)!tt_numdoc).PadLeft(6, "0"c), 6) & _
                                       Microsoft.VisualBasic.Right(NTSCStr(dttDometic.Rows(0)!tt_riga).PadLeft(2, "0"c), 2) & _
                                       Microsoft.VisualBasic.Right(NTSCStr(nCount).PadLeft(4, "0"c), 4) & _
                                       Microsoft.VisualBasic.Right(strCodalt.PadLeft(4), 4)

        strSTPTermEtichetta = Replace(strSTPTermEtichetta, "[@CODART]", strCodart)
        strSTPTermEtichetta = Replace(strSTPTermEtichetta, "[@CODALT]", strCodalt)
        strSTPTermEtichetta = Replace(strSTPTermEtichetta, "[@DESCR]", strDesart)
        strSTPTermEtichetta = Replace(strSTPTermEtichetta, "[@IDPZ]", strIdentificativoPezzo)

        oPort.WritePort("""" & strSTPTermEtichetta & """")

        nCount += 1
        lNrEtichette -= 1
      Loop

      'chiudo la porta
      oPort.EndDocPort()
      oPort.ClosePort()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub StampaDiretta()
    Dim oPort As New CLE__PORT
    Dim strSTPName As String
    Dim strSTPSetStd As String
    Dim strSTPSetBarcode As String
    Dim strSTPSetCopie As String
    Dim strSTPReset As String
    Dim strSTPAsciiEnter As String
    Dim strSTPSetTesto As String
    Dim strSTPTestoCosa As String
    Dim strSTPSetTesto2 As String
    Dim strSTPTestoCosa2 As String
    Dim strSTPSetTesto3 As String
    Dim strSTPTestoCosa3 As String
    Dim strEnter() As String
    Dim lSTPTestoMaxLen As Integer
    Dim lSTPTestoMaxLen2 As Integer
    Dim lSTPTestoMaxLen3 As Integer
    Dim bSTPTestoAnteponiEuro As Boolean
    Dim strTesto As String
    Dim strTesto2 As String
    Dim strTesto3 As String
    Dim dttTermica As New DataTable
    Dim i As Integer
    Dim x As Integer
    Dim strMsg As String = ""

    Try
      strSTPName = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermNome", "", ".", ""))
      strSTPSetStd = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetStd", "", ".", ""))
      strSTPSetBarcode = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetBarcode", "", ".", ""))
      strSTPSetCopie = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetCopie", "", ".", ""))
      strSTPReset = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermNReset", "", ".", ""))
      strSTPAsciiEnter = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermAsciiEnter", "", ".", ""))
      strSTPSetTesto = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetTesto", "", ".", ""))
      strSTPTestoCosa = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermTestoCosa", "D", ".", "D"))
      strSTPSetTesto2 = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetTesto2", "", ".", ""))
      strSTPTestoCosa2 = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermTestoCosa2", "D", ".", "D"))
      strSTPSetTesto3 = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermSetTesto3", "", ".", ""))
      strSTPTestoCosa3 = Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermTestoCosa3", "D", ".", "D"))
      lSTPTestoMaxLen = NTSCInt(Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "STPTestoMaxLen", "0", "0", "0")))
      lSTPTestoMaxLen2 = NTSCInt(Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "STPTestoMaxLen2", "0", "0", "0")))
      lSTPTestoMaxLen3 = NTSCInt(Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "STPTestoMaxLen3", "0", "0", "0")))
      bSTPTestoAnteponiEuro = CBool(Val(Trim(oMenu.GetSettingBus("Bsmgette", "Opzioni", ".", "StpTermTestoAnteponiEuro", "0", "0", "0"))))

      If strSTPName = "" Or strSTPSetStd = "" Or strSTPSetTesto = "" Or strSTPSetBarcode = "" Or _
         strSTPSetCopie = "" Or strSTPReset = "" Or strSTPAsciiEnter = "" Or strSTPTestoCosa = "" Then
        strMsg = oApp.Tr(Me, 130153515480200916, "ATTENZIONE!" & vbCrLf & vbCrLf & _
          "Le seguenti opzioni di Registro, necessarie per la stampa diretta, NON sono state impostate:")
        If strSTPName = "" Then strMsg += oApp.Tr(Me, 130153517501936751, vbCrLf & " . StpTermNome")
        If strSTPSetStd = "" Then strMsg += oApp.Tr(Me, 130153518007216911, vbCrLf & " . StpTermSetStd")
        If strSTPSetTesto = "" Then strMsg += oApp.Tr(Me, 130153518023622111, vbCrLf & " . StpTermSetTesto")
        If strSTPSetBarcode = "" Then strMsg += oApp.Tr(Me, 130153518039197286, vbCrLf & " . StpTermSetBarcode")
        If strSTPSetCopie = "" Then strMsg += oApp.Tr(Me, 130153518058951881, vbCrLf & " . StpTermSetCopie")
        If strSTPReset = "" Then strMsg += oApp.Tr(Me, 130153518076060161, vbCrLf & " . StpTermNReset")
        If strSTPAsciiEnter = "" Then strMsg += oApp.Tr(Me, 130153518094232826, vbCrLf & " . StpTermAsciiEnter")
        If strSTPTestoCosa = "" Then strMsg += oApp.Tr(Me, 130153518109651761, vbCrLf & " . StpTermTestoCosa")
        strMsg += oApp.Tr(Me, 130153519030715856, vbCrLf & vbCrLf & "Impossibile continuare.")
        oApp.MsgBoxErr(strMsg)
        Return
      Else
        strEnter = Split(strSTPAsciiEnter, ";")
      End If


      oPort.OpenPort(strSTPName)
      oPort.StartDocPort(Me.Text, vbNullString, vbNullString)
      oPort.StartPagePort()

      If Not oCleEtte.DatiTermica(bUsaTtstmatr, cbOrigine.SelectedValue, lIIstMatr, lIIstMats, dttTermica) Then Return

      For x = 0 To dttTermica.Rows.Count - 1
        strTesto = "."
        strTesto2 = "."
        strTesto3 = "."
        Select Case UCase(strSTPTestoCosa)
          Case "A" : strTesto = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "B" : strTesto = PrezzoCriptato(NTSCDec(dttTermica.Rows(x)!tt_valore), NTSCDec(dttTermica.Rows(x)!tt_valcar), NTSCStr(dttTermica.Rows(x)!tt_codart))
          Case "C" : strTesto = NTSCStr(dttTermica.Rows(x)!tt_codart)
          Case "D" : strTesto = NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "E" : strTesto = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "F" : strTesto = NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "P" : strTesto = NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case Else : strTesto = strSTPTestoCosa
        End Select
        Select Case UCase(strSTPTestoCosa2)
          Case "A" : strTesto2 = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "B" : strTesto2 = PrezzoCriptato(NTSCDec(dttTermica.Rows(x)!tt_valore), NTSCDec(dttTermica.Rows(x)!tt_valcar), NTSCStr(dttTermica.Rows(x)!tt_codart))
          Case "C" : strTesto2 = NTSCStr(dttTermica.Rows(x)!tt_codart)
          Case "D" : strTesto2 = NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "E" : strTesto2 = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "F" : strTesto2 = NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "P" : strTesto2 = NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case Else : strTesto2 = strSTPTestoCosa2
        End Select
        Select Case UCase(strSTPTestoCosa3)
          Case "A" : strTesto3 = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "B" : strTesto3 = PrezzoCriptato(NTSCDec(dttTermica.Rows(x)!tt_valore), NTSCDec(dttTermica.Rows(x)!tt_valcar), NTSCStr(dttTermica.Rows(x)!tt_codart))
          Case "C" : strTesto3 = NTSCStr(dttTermica.Rows(x)!tt_codart)
          Case "D" : strTesto3 = NTSCStr(dttTermica.Rows(x)!ar_descr)
          Case "E" : strTesto3 = NTSCStr(dttTermica.Rows(x)!tt_codart) & " " & NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "F" : strTesto3 = NTSCStr(dttTermica.Rows(x)!ar_descr) & " " & NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case "P" : strTesto3 = NTSCStr(IIf(bSTPTestoAnteponiEuro, "€ ", "")) & Format(dttTermica.Rows(x)!tt_valore, oApp.FormatImporti)
          Case Else : strTesto3 = strSTPTestoCosa3
        End Select

        oPort.WritePort(strSTPReset)
        For i = 0 To UBound(strEnter)
          oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
        Next

        oPort.WritePort(strSTPSetStd)
        For i = 0 To UBound(strEnter)
          oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
        Next

        'invio il testo sopra al testo 1
        If strSTPSetTesto <> "" Then
          oPort.WritePort(strSTPSetTesto)
          If lSTPTestoMaxLen <> 0 Then strTesto = Microsoft.VisualBasic.Left(strTesto.PadLeft(lSTPTestoMaxLen), lSTPTestoMaxLen)
          oPort.WritePort("""" & strTesto & """")
          For i = 0 To UBound(strEnter)
            oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
          Next
        End If
        'invio il testo sopra al testo 2
        If strSTPSetTesto2 <> "" Then
          oPort.WritePort(strSTPSetTesto2)
          If lSTPTestoMaxLen2 <> 0 Then strTesto2 = Microsoft.VisualBasic.Left(strTesto2.PadLeft(lSTPTestoMaxLen2), lSTPTestoMaxLen2)
          oPort.WritePort("""" & strTesto2 & """")
          For i = 0 To UBound(strEnter)
            oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
          Next
        End If
        'invio il testo sopra al testo 3
        If strSTPSetTesto3 <> "" Then
          oPort.WritePort(strSTPSetTesto3)
          If lSTPTestoMaxLen3 <> 0 Then strTesto3 = Microsoft.VisualBasic.Left(strTesto3.PadLeft(lSTPTestoMaxLen3), lSTPTestoMaxLen3)
          oPort.WritePort("""" & strTesto3 & """")
          For i = 0 To UBound(strEnter)
            oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
          Next
        End If

        'invio il numero barcode
        oPort.WritePort(strSTPSetBarcode)
        oPort.WritePort("""" & NTSCStr(dttTermica.Rows(x)!tt_matric) & """")
        For i = 0 To UBound(strEnter)
          oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
        Next

        'imposto il numero di copie
        oPort.WritePort(strSTPSetCopie & NTSCStr(dttTermica.Rows(x)!nrec))
        For i = 0 To UBound(strEnter)
          oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
        Next

        'pulisco il buffer
        oPort.WritePort(strSTPReset)
        For i = 0 To UBound(strEnter)
          oPort.WritePort(Chr(NTSCInt(Val(strEnter(i)))))
        Next
      Next

      oPort.EndDocPort()
      oPort.ClosePort()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function PrezzoCriptato(ByVal dValore As Decimal, ByVal dValcar As Decimal, ByVal strCodart As String) As String
    Dim strValore As String
    Dim strValcar As String
    Dim Prezzo As String
    Try
      'compone il prezzo in questo modo:
      'codice articolo (colonna note di codarfo) + / + prezzo in lire invertito + / + centesimi di euro + . + prezzo in euro senza centesimi
      Prezzo = strCodart & "/00.0"
      strValore = Format(dValore, "###0.00")
      strValcar = Format(dValcar, "###0.00")
      If dValcar = 0 Then
        If Len(strValore) = 3 Then
          Prezzo = strCodart & "/" & Mid(strValore, 3, 1) & Mid(strValore, 4, 1) & "." & Mid(strValore, 1, 1)
        Else
          If Len(strValore) = 4 Then
            Prezzo = strCodart & "/" & Mid(strValore, 3, 1) & Mid(strValore, 4, 1) & "." & Mid(strValore, 1, 1)
          Else
            If Len(strValore) = 5 Then
              Prezzo = strCodart & "/" & Mid(strValore, 4, 1) & Mid(strValore, 5, 1) & "." & Mid(strValore, 1, 1) & Mid(strValore, 2, 1)
            Else
              If Len(strValore) = 6 Then
                Prezzo = strCodart & "/" & Mid(strValore, 5, 1) & Mid(strValore, 6, 1) & "." & Mid(strValore, 1, 1) & Mid(strValore, 2, 1) & Mid(strValore, 3, 1)
              End If     'If Len(strValore) = 6 Then
            End If    'If Len(strValore) = 5 Then
          End If    'If Len(strValore) = 4 Then
        End If 'If len(strvalore)=3 then
      Else

        If Len(strValore) = 3 Then
          Prezzo = strCodart & "/" & Mid(strValore, 3, 1) & Mid(strValore, 4, 1) & "." & Mid(strValore, 1, 1)
        Else
          If Len(strValore) = 4 Then
            Prezzo = strCodart & "/" & Mid(strValore, 3, 1) & Mid(strValore, 4, 1) & "." & Mid(strValore, 1, 1)
          Else
            If Len(strValore) = 5 Then
              Prezzo = strCodart & "/" & Mid(strValore, 4, 1) & Mid(strValore, 5, 1) & "." & Mid(strValore, 1, 1) & Mid(strValore, 2, 1)
            Else
              If Len(strValore) = 6 Then
                Prezzo = strCodart & "/" & Mid(strValore, 5, 1) & Mid(strValore, 6, 1) & "." & Mid(strValore, 1, 1) & Mid(strValore, 2, 1) & Mid(strValore, 3, 1)
              End If
            End If
          End If
        End If

        If Len(strValcar) = 3 Then
          Prezzo = Prezzo & "/" & Mid(strValcar, 3, 1) & Mid(strValcar, 4, 1) & "." & Mid(strValcar, 1, 1)
        Else
          If Len(strValcar) = 4 Then
            Prezzo = Prezzo & "/" & Mid(strValcar, 3, 1) & Mid(strValcar, 4, 1) & "." & Mid(strValcar, 1, 1)
          Else
            If Len(strValcar) = 5 Then
              Prezzo = Prezzo & "/" & Mid(strValcar, 4, 1) & Mid(strValcar, 5, 1) & "." & Mid(strValcar, 1, 1) & Mid(strValcar, 2, 1)
            Else
              If Len(strValcar) = 6 Then
                Prezzo = Prezzo & "/" & Mid(strValcar, 5, 1) & Mid(strValcar, 6, 1) & "." & Mid(strValcar, 1, 1) & Mid(strValcar, 2, 1) & Mid(strValcar, 3, 1)
              End If
            End If
          End If
        End If
      End If

      Return Prezzo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function ControlliAggiuntivi() As Boolean
    Try
      If ckValuta.Checked And edValuta.Text = "0" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128678578560545631, "Il numero inserito nel campo 'Valuta' deve essere compreso tra 1 e 9999"))
        Return False
      End If

      If ckStampe.Checked And cbOperazione.SelectedValue = "1" And edNumMolt.Text = "0" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128678579659913595, "Non è possibile dividere il numero di etichette per 0"))
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub RiceviParametri()
    Dim strTmp() As String
    Dim i As Integer
    Try
      bDaArticoli = False
      bDaDocumenti = False
      bDaOrdini = False
      oCleEtte.bDaTc = False
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 3).ToUpper = "ART" Then
          'Direttamente un codice articolo
          '----------------------------------------------------------
          'Formato accettato:
          ' ART; (header fisso 3 caratteri + ;)
          ' Codice articolo; (fisso 18 caratteri allin. a sx + ;) -no: ora è lungoquanto la maxlen del cod articolo
          ' Quantità; (fisso 9 caratteri allin. a dx + ;)
          '----------------------------------------------------------
          bDaArticoli = True
          bDaDocumenti = False
          bDaOrdini = False
          oCleEtte.bDaTc = False
          strArticoli = Mid(oCallParams.strParam, 5, CLN__STD.CodartMaxLen)
          lQuantita = NTSCInt(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 6, 9))
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 3).ToUpper = "DOC" Then
          'Direttamente un documento
          '----------------------------------------------------------
          'Formato accettato:
          ' DOC; (header fisso 3 caratteri + ;)
          ' Tipo doc. (fisso 1 carattere + ;)
          ' Anno doc. (fisso 4 caratteri + ;)
          ' Serie doc. (fisso 1 carattere + ;)
          ' Numero doc. (fisso 9 carattere + ;)
          ' Riga doc. (fisso 9 carattere + ;) 0 = tutte le righe
          '----------------------------------------------------------
          bDaArticoli = False
          bDaDocumenti = True
          bDaOrdini = False
          oCleEtte.bDaTc = False
          Dim strParts() As String = oCallParams.strParam.Split(";"c)
          strTipoDocu = strParts(1)
          nAnnoDocu = NTSCInt(strParts(2))
          strSerieDocu = strParts(3)
          lNumDocu = NTSCInt(strParts(4))
          lRigaDocu = NTSCInt(strParts(5))
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 3).ToUpper = "ORD" Then
          'Direttamente un documento
          '----------------------------------------------------------
          'Formato accettato:
          ' DOC; (header fisso 3 caratteri + ;)
          ' Tipo doc. (fisso 1 carattere + ;)
          ' Anno doc. (fisso 4 caratteri + ;)
          ' Serie doc. (fisso 1 carattere + ;)
          ' Numero doc. (fisso 9 carattere + ;)
          ' Riga doc. (fisso 9 carattere + ;) 0 = tutte le righe
          '----------------------------------------------------------
          bDaArticoli = False
          bDaDocumenti = False
          bDaOrdini = True
          oCleEtte.bDaTc = False
          Dim strParts() As String = oCallParams.strParam.Split(";"c)
          strTipoDocu = strParts(1)
          nAnnoDocu = NTSCInt(strParts(2))
          strSerieDocu = strParts(3)
          lNumDocu = NTSCInt(strParts(4))
          lRigaDocu = NTSCInt(strParts(5))
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 3).ToUpper = "ATC" Then
          '----------------------------------------------------------
          'ATC;articolo;taglia1;qta1;taglia2;qta2;.........;taglia24;qta24
          '----------------------------------------------------------
          bDaArticoli = False
          bDaDocumenti = False
          bDaOrdini = False
          oCleEtte.bDaTc = True
          strTmp = oCallParams.strParam.Split(";"c)
          strArticoli = strTmp(1)

          oCleEtte.dttTc = New DataTable
          oCleEtte.dttTc.Columns.Add("xx_tagl")
          oCleEtte.dttTc.Columns.Add("xx_qta")
          oCleEtte.dttTc.AcceptChanges()
          lQtaTot = 0
          For i = 2 To strTmp.Length - 1
            If i Mod 2 = 0 Then
              oCleEtte.dttTc.Rows.Add()
              oCleEtte.dttTc.Rows(oCleEtte.dttTc.Rows.Count - 1)!xx_tagl = strTmp(i)
            Else
              oCleEtte.dttTc.Rows(oCleEtte.dttTc.Rows.Count - 1)!xx_qta = strTmp(i)
              lQtaTot = lQtaTot + NTSCInt(strTmp(i))
            End If
          Next
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class