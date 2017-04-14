Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSCHC
#Region "Moduli"
  Private Moduli_P As Integer = bsModVE + bsModMG
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
  Public oCleSchc As CLEMGSCHC
  Public oCallParams As CLE__CLDP
  Public dsSchc As New DataSet
  Public dcSchc As New BindingSource
  Public dsTestata As New DataSet
  Public dcTestata As New BindingSource
  Public dttCampi As New DataTable
  Public dsCorpo As New DataSet
  Public dcCorpo As New BindingSource
  Public dttCorpocampi As New DataTable
  Public Const bsDbAzienda As Boolean = False
  Public strWhereArtico As String = ""
  Public bModExtTCO As Boolean
  Public bModuloCrm As Boolean
  Public bIsCRMUser As Boolean
  Public bAmm As Boolean
  Public strAccvis As String
  Public strRegvis As String
  Public lCodorgaOperat As Integer
  Public bFiltri As Boolean = True
  Public bDaGest As Boolean = False
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
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents tsSchc As NTSInformatica.NTSTabControl
  Public WithEvents tsSchc1 As NTSInformatica.NTSTabPage
  Public WithEvents tsSchc2 As NTSInformatica.NTSTabPage
  Public WithEvents lbAlmaga As NTSInformatica.NTSLabel
  Public WithEvents lbAlconto As NTSInformatica.NTSLabel
  Public WithEvents lbDacomme As NTSInformatica.NTSLabel
  Public WithEvents lbDamaga As NTSInformatica.NTSLabel
  Public WithEvents lbClifor As NTSInformatica.NTSLabel
  Public WithEvents lbAldata As NTSInformatica.NTSLabel
  Public WithEvents lbAgente2 As NTSInformatica.NTSLabel
  Public WithEvents lbAlcomme As NTSInformatica.NTSLabel
  Public WithEvents lbAgente As NTSInformatica.NTSLabel
  Public WithEvents lbCausa As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents edDadata As NTSInformatica.NTSTextBoxData
  Public WithEvents edAldata As NTSInformatica.NTSTextBoxData
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClifor As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgente2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCausa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDacomme As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlcomme As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlmaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDamaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents ckTipodoc As NTSInformatica.NTSCheckBox
  Public WithEvents fmClifor As NTSInformatica.NTSGroupBox
  Public WithEvents opFornitori As NTSInformatica.NTSRadioButton
  Public WithEvents opClienti As NTSInformatica.NTSRadioButton
  Public WithEvents NtsButton1 As NTSInformatica.NTSButton
  Public WithEvents lbDesclifor As NTSInformatica.NTSLabel
  Public WithEvents lbDesagente2 As NTSInformatica.NTSLabel
  Public WithEvents lbDesagente As NTSInformatica.NTSLabel
  Public WithEvents lbDescausa As NTSInformatica.NTSLabel
  Public WithEvents pnPrincipale As NTSInformatica.NTSPanel
  Public WithEvents tsSchc3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTestata As NTSInformatica.NTSPanel
  Public WithEvents pnCorpo As NTSInformatica.NTSPanel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents cmdTestLock As NTSInformatica.NTSButton
  Public WithEvents grTestata As NTSInformatica.NTSGrid
  Public WithEvents grvTestata As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents grCorpo As NTSInformatica.NTSGrid
  Public WithEvents grvCorpo As NTSInformatica.NTSGridView
  Public WithEvents xx_corponome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_corpovalda As NTSInformatica.NTSGridColumn
  Public WithEvents cmdCorpoLock As NTSInformatica.NTSButton
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents tlbNoModal As NTSInformatica.NTSBarMenuItem
  Public WithEvents xx_corpovala As NTSInformatica.NTSGridColumn
#End Region

#Region "EVENTI DI FORM"
  Public Overridable Sub FRMMGSCHC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttRelease As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.GetBusRelease(dttRelease) = True Then
        If dttRelease.Rows.Count <> 0 Then
          With dttRelease.Rows(0)
            If NTSCInt(!rel_maior) >= 15 Or (NTSCInt(!rel_maior) = 14 And NTSCStr(!rel_pers) = "a") Then oCleSchc.bIs15 = True
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      bModuloCrm = False
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then bModuloCrm = True
      '--------------------------------------------------------------------------------------------------------------
      If bModuloCrm Then
        bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, "", strRegvis, "")
        If bIsCRMUser Then
          lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente)
          If lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128644940096875000, "Attenzione!" & vbCrLf & _
              "L'operatore '|" & oApp.User.Nome & "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
              "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCrm = True) And (bIsCRMUser = True) And (oCleSchc.bIs15 = True) Then
        oCleSchc.lIITtsubqcrm = oMenu.GetTblInstId("TTSUBQCRM", False)
        oCleSchc.nCodcageAccdito = oCleSchc.RitornaCodcageAccdito(bModuloCrm, bIsCRMUser)
      End If
      '--------------------------------------------------------------------------------------------------------------

      oCleSchc.bModExtTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      oCleSchc.bModuloCrm = bModuloCrm
      oCleSchc.bIsCRMUser = bIsCRMUser
      oCleSchc.bAmm = bAmm
      oCleSchc.strAccvis = strAccvis
      oCleSchc.strRegvis = strRegvis
      oCleSchc.lCodorgaOperat = lCodorgaOperat

      '--- FILTRI TESTATA ---
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("testmag", dttCampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()

      CreaDatatableTestata()

      dcTestata.DataSource = dsTestata.Tables("FILTRI1")
      dsTestata.AcceptChanges()
      grTestata.DataSource = dcTestata

      '--- FILTRI CORPO ---
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("movmag", dttCorpocampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCorpocampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCorpocampi.AcceptChanges()

      CreaDatatableCorpo()

      dcCorpo.DataSource = dsCorpo.Tables("FILTRI1")
      dsCorpo.AcceptChanges()
      grCorpo.DataSource = dcCorpo

      'predispongo i controlli
      InitControls()

      edAldata.Text = IntSetDate("31/12/" + NTSCStr(Now.Year))
      edDadata.Text = IntSetDate("01/01/" + NTSCStr(Now.Year))

      oCleSchc.bControlloSuEsistenza = CBool(oMenu.GetSettingBus("Bsmgschc", "Opzioni", ".", "ControlloSuEsistenza", "0", " ", "0"))

      CaricaCombo()

      edSerie.Enabled = False
      ckSerie.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      tlbNoModal.Checked = CBool(oMenu.GetSettingBus("BSMGSCHC", "RECENT", ".", "ChildNoModal", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          If Mid(oCallParams.strParam, 1, 1) = "C" Then opClienti.Checked = True Else opFornitori.Checked = True
          If NTSCInt(Mid(oCallParams.strParam, 3, 9)) <> 0 Then
            edDaconto.Text = NTSCInt(Mid(oCallParams.strParam, 3, 9)).ToString
          End If
          If NTSCInt(Mid(oCallParams.strParam, 33, 9)) <> 0 Then
            edAlconto.Text = NTSCInt(Mid(oCallParams.strParam, 13, 9)).ToString
          End If
          cbTipodoc.SelectedValue = Mid(oCallParams.strParam, 23, 1)
          If Trim(Mid(oCallParams.strParam, 25, 10)) <> "" Then
            edDadata.Text = Trim(Mid(oCallParams.strParam, 25, 10))
          End If
          If Trim(Mid(oCallParams.strParam, 36, 10)) <> "" Then
            edAldata.Text = Trim(Mid(oCallParams.strParam, 36, 10))
          End If
          bFiltri = CBool(Mid(oCallParams.strParam, 47, 1))
          bDaGest = True
          ckTipodoc.Checked = False

          If bFiltri = False Then
            StampaGriglia()
            Me.Close()
            Return
          End If
        End If
      End If


    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      If Not dsTestata Is Nothing Then
        If Not dsTestata.Tables("FILTRI1") Is Nothing Then
          '-------------------------------------------------
          'salvo il recent
          Dim strTmp As String = ""
          Dim i As Integer = 0
          dsTestata.Tables("FILTRI1").AcceptChanges()
          For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
            strTmp += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BNMGSCHC", "RECENT", ".", "FiltriTestata", strTmp, " ", "NS.", "NS.", "...")
        End If
        dsTestata.Dispose()
        dcTestata.Dispose()
      End If

      If Not dsCorpo Is Nothing Then
        If Not dsCorpo.Tables("FILTRI1") Is Nothing Then
          '-------------------------------------------------
          'salvo il recent
          Dim strTmp As String = ""
          Dim i As Integer = 0
          dsCorpo.Tables("FILTRI1").AcceptChanges()
          For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
            strTmp += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & ";"
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BNMGSCHC", "RECENT", ".", "FiltriCorpo", strTmp, " ", "NS.", "NS.", "...")
        End If
        dsCorpo.Dispose()
        dcCorpo.Dispose()
      End If


      dcSchc.Dispose()
      dsSchc.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSMGSCHC", "RECENT", ".", "ChildNoModal", IIf(tlbNoModal.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSCHC", "BEMGSCHC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSchc = CType(oTmp, CLEMGSCHC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSCHC", strRemoteServer, strRemotePort)
    AddHandler oCleSchc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSchc.Init(oApp, NTSScript, oMenu.oCleComm, "TABANAZ", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSCHC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbNoModal = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tsSchc = New NTSInformatica.NTSTabControl
    Me.tsSchc1 = New NTSInformatica.NTSTabPage
    Me.pnPrincipale = New NTSInformatica.NTSPanel
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbDesclifor = New NTSInformatica.NTSLabel
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.lbDesagente2 = New NTSInformatica.NTSLabel
    Me.lbCausa = New NTSInformatica.NTSLabel
    Me.lbDesagente = New NTSInformatica.NTSLabel
    Me.lbAgente = New NTSInformatica.NTSLabel
    Me.lbDescausa = New NTSInformatica.NTSLabel
    Me.lbAlcomme = New NTSInformatica.NTSLabel
    Me.fmClifor = New NTSInformatica.NTSGroupBox
    Me.opFornitori = New NTSInformatica.NTSRadioButton
    Me.opClienti = New NTSInformatica.NTSRadioButton
    Me.lbAgente2 = New NTSInformatica.NTSLabel
    Me.NtsButton1 = New NTSInformatica.NTSButton
    Me.lbAldata = New NTSInformatica.NTSLabel
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.lbClifor = New NTSInformatica.NTSLabel
    Me.ckTipodoc = New NTSInformatica.NTSCheckBox
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbDamaga = New NTSInformatica.NTSLabel
    Me.edClifor = New NTSInformatica.NTSTextBoxNum
    Me.lbDacomme = New NTSInformatica.NTSLabel
    Me.edAgente2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAlconto = New NTSInformatica.NTSLabel
    Me.edAgente = New NTSInformatica.NTSTextBoxNum
    Me.lbAlmaga = New NTSInformatica.NTSLabel
    Me.edCausa = New NTSInformatica.NTSTextBoxNum
    Me.edAldata = New NTSInformatica.NTSTextBoxData
    Me.edDacomme = New NTSInformatica.NTSTextBoxNum
    Me.edDadata = New NTSInformatica.NTSTextBoxData
    Me.edAlcomme = New NTSInformatica.NTSTextBoxNum
    Me.edDamaga = New NTSInformatica.NTSTextBoxNum
    Me.edAlconto = New NTSInformatica.NTSTextBoxNum
    Me.edAlmaga = New NTSInformatica.NTSTextBoxNum
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.tsSchc2 = New NTSInformatica.NTSTabPage
    Me.pnTestata = New NTSInformatica.NTSPanel
    Me.cmdTestLock = New NTSInformatica.NTSButton
    Me.grTestata = New NTSInformatica.NTSGrid
    Me.grvTestata = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.tsSchc3 = New NTSInformatica.NTSTabPage
    Me.pnCorpo = New NTSInformatica.NTSPanel
    Me.cmdCorpoLock = New NTSInformatica.NTSButton
    Me.grCorpo = New NTSInformatica.NTSGrid
    Me.grvCorpo = New NTSInformatica.NTSGridView
    Me.xx_corponome = New NTSInformatica.NTSGridColumn
    Me.xx_corpovalda = New NTSInformatica.NTSGridColumn
    Me.xx_corpovala = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsSchc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSchc.SuspendLayout()
    Me.tsSchc1.SuspendLayout()
    CType(Me.pnPrincipale, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPrincipale.SuspendLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmClifor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmClifor.SuspendLayout()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClifor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgente2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCausa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAldata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacomme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlcomme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSchc2.SuspendLayout()
    CType(Me.pnTestata, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestata.SuspendLayout()
    CType(Me.grTestata, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTestata, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSchc3.SuspendLayout()
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCorpo.SuspendLayout()
    CType(Me.grCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbNoModal})
    Me.NtsBarManager1.MaxItemId = 9
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStampa.Id = 0
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 1
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 7
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoModal), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbNoModal
    '
    Me.tlbNoModal.Caption = "Apri griglia in modalità NON modale"
    Me.tlbNoModal.Id = 8
    Me.tlbNoModal.Name = "tlbNoModal"
    Me.tlbNoModal.NTSIsCheckBox = True
    Me.tlbNoModal.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 6
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tsSchc
    '
    Me.tsSchc.Location = New System.Drawing.Point(9, 36)
    Me.tsSchc.Name = "tsSchc"
    Me.tsSchc.SelectedTabPage = Me.tsSchc1
    Me.tsSchc.Size = New System.Drawing.Size(454, 275)
    Me.tsSchc.TabIndex = 4
    Me.tsSchc.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tsSchc1, Me.tsSchc2, Me.tsSchc3})
    '
    'tsSchc1
    '
    Me.tsSchc1.AllowDrop = True
    Me.tsSchc1.Controls.Add(Me.pnPrincipale)
    Me.tsSchc1.Enable = True
    Me.tsSchc1.Name = "tsSchc1"
    Me.tsSchc1.Size = New System.Drawing.Size(445, 245)
    Me.tsSchc1.Text = "1 - Principale"
    '
    'pnPrincipale
    '
    Me.pnPrincipale.AllowDrop = True
    Me.pnPrincipale.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPrincipale.Appearance.Options.UseBackColor = True
    Me.pnPrincipale.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPrincipale.Controls.Add(Me.ckSerie)
    Me.pnPrincipale.Controls.Add(Me.lbDadata)
    Me.pnPrincipale.Controls.Add(Me.lbDesclifor)
    Me.pnPrincipale.Controls.Add(Me.lbDaconto)
    Me.pnPrincipale.Controls.Add(Me.lbDesagente2)
    Me.pnPrincipale.Controls.Add(Me.lbCausa)
    Me.pnPrincipale.Controls.Add(Me.lbDesagente)
    Me.pnPrincipale.Controls.Add(Me.lbAgente)
    Me.pnPrincipale.Controls.Add(Me.lbDescausa)
    Me.pnPrincipale.Controls.Add(Me.lbAlcomme)
    Me.pnPrincipale.Controls.Add(Me.fmClifor)
    Me.pnPrincipale.Controls.Add(Me.lbAgente2)
    Me.pnPrincipale.Controls.Add(Me.NtsButton1)
    Me.pnPrincipale.Controls.Add(Me.lbAldata)
    Me.pnPrincipale.Controls.Add(Me.cbTipodoc)
    Me.pnPrincipale.Controls.Add(Me.lbClifor)
    Me.pnPrincipale.Controls.Add(Me.ckTipodoc)
    Me.pnPrincipale.Controls.Add(Me.edSerie)
    Me.pnPrincipale.Controls.Add(Me.lbDamaga)
    Me.pnPrincipale.Controls.Add(Me.edClifor)
    Me.pnPrincipale.Controls.Add(Me.lbDacomme)
    Me.pnPrincipale.Controls.Add(Me.edAgente2)
    Me.pnPrincipale.Controls.Add(Me.lbAlconto)
    Me.pnPrincipale.Controls.Add(Me.edAgente)
    Me.pnPrincipale.Controls.Add(Me.lbAlmaga)
    Me.pnPrincipale.Controls.Add(Me.edCausa)
    Me.pnPrincipale.Controls.Add(Me.edAldata)
    Me.pnPrincipale.Controls.Add(Me.edDacomme)
    Me.pnPrincipale.Controls.Add(Me.edDadata)
    Me.pnPrincipale.Controls.Add(Me.edAlcomme)
    Me.pnPrincipale.Controls.Add(Me.edDamaga)
    Me.pnPrincipale.Controls.Add(Me.edAlconto)
    Me.pnPrincipale.Controls.Add(Me.edAlmaga)
    Me.pnPrincipale.Controls.Add(Me.edDaconto)
    Me.pnPrincipale.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPrincipale.Location = New System.Drawing.Point(3, 0)
    Me.pnPrincipale.Name = "pnPrincipale"
    Me.pnPrincipale.NTSActiveTrasparency = True
    Me.pnPrincipale.Size = New System.Drawing.Size(440, 249)
    Me.pnPrincipale.TabIndex = 34
    Me.pnPrincipale.Text = "NtsPanel1"
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(251, 89)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(45, 19)
    Me.ckSerie.TabIndex = 42
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(3, 13)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(55, 13)
    Me.lbDadata.TabIndex = 0
    Me.lbDadata.Text = "Dalla data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'lbDesclifor
    '
    Me.lbDesclifor.BackColor = System.Drawing.Color.Transparent
    Me.lbDesclifor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesclifor.Location = New System.Drawing.Point(171, 218)
    Me.lbDesclifor.Name = "lbDesclifor"
    Me.lbDesclifor.NTSDbField = ""
    Me.lbDesclifor.Size = New System.Drawing.Size(152, 20)
    Me.lbDesclifor.TabIndex = 33
    Me.lbDesclifor.Tooltip = ""
    Me.lbDesclifor.UseMnemonic = False
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(3, 65)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(52, 13)
    Me.lbDaconto.TabIndex = 1
    Me.lbDaconto.Text = "Dal conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'lbDesagente2
    '
    Me.lbDesagente2.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagente2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagente2.Location = New System.Drawing.Point(171, 192)
    Me.lbDesagente2.Name = "lbDesagente2"
    Me.lbDesagente2.NTSDbField = ""
    Me.lbDesagente2.Size = New System.Drawing.Size(152, 20)
    Me.lbDesagente2.TabIndex = 32
    Me.lbDesagente2.Tooltip = ""
    Me.lbDesagente2.UseMnemonic = False
    '
    'lbCausa
    '
    Me.lbCausa.AutoSize = True
    Me.lbCausa.BackColor = System.Drawing.Color.Transparent
    Me.lbCausa.Location = New System.Drawing.Point(3, 143)
    Me.lbCausa.Name = "lbCausa"
    Me.lbCausa.NTSDbField = ""
    Me.lbCausa.Size = New System.Drawing.Size(45, 13)
    Me.lbCausa.TabIndex = 2
    Me.lbCausa.Text = "Causale"
    Me.lbCausa.Tooltip = ""
    Me.lbCausa.UseMnemonic = False
    '
    'lbDesagente
    '
    Me.lbDesagente.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagente.Location = New System.Drawing.Point(171, 166)
    Me.lbDesagente.Name = "lbDesagente"
    Me.lbDesagente.NTSDbField = ""
    Me.lbDesagente.Size = New System.Drawing.Size(152, 20)
    Me.lbDesagente.TabIndex = 31
    Me.lbDesagente.Tooltip = ""
    Me.lbDesagente.UseMnemonic = False
    '
    'lbAgente
    '
    Me.lbAgente.AutoSize = True
    Me.lbAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbAgente.Location = New System.Drawing.Point(3, 169)
    Me.lbAgente.Name = "lbAgente"
    Me.lbAgente.NTSDbField = ""
    Me.lbAgente.Size = New System.Drawing.Size(76, 13)
    Me.lbAgente.TabIndex = 3
    Me.lbAgente.Text = "Codice agente"
    Me.lbAgente.Tooltip = ""
    Me.lbAgente.UseMnemonic = False
    '
    'lbDescausa
    '
    Me.lbDescausa.BackColor = System.Drawing.Color.Transparent
    Me.lbDescausa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescausa.Location = New System.Drawing.Point(171, 140)
    Me.lbDescausa.Name = "lbDescausa"
    Me.lbDescausa.NTSDbField = ""
    Me.lbDescausa.Size = New System.Drawing.Size(152, 20)
    Me.lbDescausa.TabIndex = 30
    Me.lbDescausa.Tooltip = ""
    Me.lbDescausa.UseMnemonic = False
    '
    'lbAlcomme
    '
    Me.lbAlcomme.AutoSize = True
    Me.lbAlcomme.BackColor = System.Drawing.Color.Transparent
    Me.lbAlcomme.Location = New System.Drawing.Point(248, 117)
    Me.lbAlcomme.Name = "lbAlcomme"
    Me.lbAlcomme.NTSDbField = ""
    Me.lbAlcomme.Size = New System.Drawing.Size(75, 13)
    Me.lbAlcomme.TabIndex = 4
    Me.lbAlcomme.Text = "alla commessa"
    Me.lbAlcomme.Tooltip = ""
    Me.lbAlcomme.UseMnemonic = False
    '
    'fmClifor
    '
    Me.fmClifor.AllowDrop = True
    Me.fmClifor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmClifor.Appearance.Options.UseBackColor = True
    Me.fmClifor.Controls.Add(Me.opFornitori)
    Me.fmClifor.Controls.Add(Me.opClienti)
    Me.fmClifor.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmClifor.Location = New System.Drawing.Point(329, 166)
    Me.fmClifor.Name = "fmClifor"
    Me.fmClifor.Size = New System.Drawing.Size(100, 72)
    Me.fmClifor.TabIndex = 29
    '
    'opFornitori
    '
    Me.opFornitori.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFornitori.Location = New System.Drawing.Point(5, 48)
    Me.opFornitori.Name = "opFornitori"
    Me.opFornitori.NTSCheckValue = "S"
    Me.opFornitori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFornitori.Properties.Appearance.Options.UseBackColor = True
    Me.opFornitori.Properties.AutoHeight = False
    Me.opFornitori.Properties.Caption = "Fornitori"
    Me.opFornitori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFornitori.Size = New System.Drawing.Size(77, 19)
    Me.opFornitori.TabIndex = 1
    '
    'opClienti
    '
    Me.opClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opClienti.EditValue = True
    Me.opClienti.Location = New System.Drawing.Point(5, 23)
    Me.opClienti.Name = "opClienti"
    Me.opClienti.NTSCheckValue = "S"
    Me.opClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opClienti.Properties.AutoHeight = False
    Me.opClienti.Properties.Caption = "Clienti"
    Me.opClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opClienti.Size = New System.Drawing.Size(77, 19)
    Me.opClienti.TabIndex = 0
    '
    'lbAgente2
    '
    Me.lbAgente2.AutoSize = True
    Me.lbAgente2.BackColor = System.Drawing.Color.Transparent
    Me.lbAgente2.Location = New System.Drawing.Point(3, 195)
    Me.lbAgente2.Name = "lbAgente2"
    Me.lbAgente2.NTSDbField = ""
    Me.lbAgente2.Size = New System.Drawing.Size(85, 13)
    Me.lbAgente2.TabIndex = 5
    Me.lbAgente2.Text = "Codice agente 2"
    Me.lbAgente2.Tooltip = ""
    Me.lbAgente2.UseMnemonic = False
    '
    'NtsButton1
    '
    Me.NtsButton1.ImageText = ""
    Me.NtsButton1.Location = New System.Drawing.Point(329, 140)
    Me.NtsButton1.Name = "NtsButton1"
    Me.NtsButton1.NTSContextMenu = Nothing
    Me.NtsButton1.Size = New System.Drawing.Size(100, 23)
    Me.NtsButton1.TabIndex = 28
    Me.NtsButton1.Text = "Seleziona articoli"
    '
    'lbAldata
    '
    Me.lbAldata.AutoSize = True
    Me.lbAldata.BackColor = System.Drawing.Color.Transparent
    Me.lbAldata.Location = New System.Drawing.Point(248, 13)
    Me.lbAldata.Name = "lbAldata"
    Me.lbAldata.NTSDbField = ""
    Me.lbAldata.Size = New System.Drawing.Size(48, 13)
    Me.lbAldata.TabIndex = 6
    Me.lbAldata.Text = "alla data"
    Me.lbAldata.Tooltip = ""
    Me.lbAldata.UseMnemonic = False
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Enabled = False
    Me.cbTipodoc.Location = New System.Drawing.Point(111, 88)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.AutoHeight = False
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(131, 20)
    Me.cbTipodoc.TabIndex = 27
    Me.cbTipodoc.ValueMember = ""
    '
    'lbClifor
    '
    Me.lbClifor.AutoSize = True
    Me.lbClifor.BackColor = System.Drawing.Color.Transparent
    Me.lbClifor.Location = New System.Drawing.Point(3, 221)
    Me.lbClifor.Name = "lbClifor"
    Me.lbClifor.NTSDbField = ""
    Me.lbClifor.Size = New System.Drawing.Size(88, 13)
    Me.lbClifor.TabIndex = 7
    Me.lbClifor.Text = "Categoria Cli/For"
    Me.lbClifor.Tooltip = ""
    Me.lbClifor.UseMnemonic = False
    '
    'ckTipodoc
    '
    Me.ckTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipodoc.Location = New System.Drawing.Point(6, 89)
    Me.ckTipodoc.Name = "ckTipodoc"
    Me.ckTipodoc.NTSCheckValue = "S"
    Me.ckTipodoc.NTSUnCheckValue = "N"
    Me.ckTipodoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipodoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipodoc.Properties.AutoHeight = False
    Me.ckTipodoc.Properties.Caption = "Tipo documento"
    Me.ckTipodoc.Size = New System.Drawing.Size(107, 19)
    Me.ckTipodoc.TabIndex = 26
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(329, 88)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 25
    '
    'lbDamaga
    '
    Me.lbDamaga.AutoSize = True
    Me.lbDamaga.BackColor = System.Drawing.Color.Transparent
    Me.lbDamaga.Location = New System.Drawing.Point(3, 39)
    Me.lbDamaga.Name = "lbDamaga"
    Me.lbDamaga.NTSDbField = ""
    Me.lbDamaga.Size = New System.Drawing.Size(75, 13)
    Me.lbDamaga.TabIndex = 9
    Me.lbDamaga.Text = "Dal magazzino"
    Me.lbDamaga.Tooltip = ""
    Me.lbDamaga.UseMnemonic = False
    '
    'edClifor
    '
    Me.edClifor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClifor.EditValue = "0"
    Me.edClifor.Location = New System.Drawing.Point(111, 218)
    Me.edClifor.Name = "edClifor"
    Me.edClifor.NTSDbField = ""
    Me.edClifor.NTSFormat = "0"
    Me.edClifor.NTSForzaVisZoom = False
    Me.edClifor.NTSOldValue = ""
    Me.edClifor.Properties.Appearance.Options.UseTextOptions = True
    Me.edClifor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClifor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClifor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClifor.Properties.AutoHeight = False
    Me.edClifor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClifor.Properties.MaxLength = 65536
    Me.edClifor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClifor.Size = New System.Drawing.Size(54, 20)
    Me.edClifor.TabIndex = 24
    '
    'lbDacomme
    '
    Me.lbDacomme.AutoSize = True
    Me.lbDacomme.BackColor = System.Drawing.Color.Transparent
    Me.lbDacomme.Location = New System.Drawing.Point(3, 117)
    Me.lbDacomme.Name = "lbDacomme"
    Me.lbDacomme.NTSDbField = ""
    Me.lbDacomme.Size = New System.Drawing.Size(82, 13)
    Me.lbDacomme.TabIndex = 10
    Me.lbDacomme.Text = "Dalla commessa"
    Me.lbDacomme.Tooltip = ""
    Me.lbDacomme.UseMnemonic = False
    '
    'edAgente2
    '
    Me.edAgente2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgente2.EditValue = "0"
    Me.edAgente2.Location = New System.Drawing.Point(111, 192)
    Me.edAgente2.Name = "edAgente2"
    Me.edAgente2.NTSDbField = ""
    Me.edAgente2.NTSFormat = "0"
    Me.edAgente2.NTSForzaVisZoom = False
    Me.edAgente2.NTSOldValue = ""
    Me.edAgente2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgente2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgente2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgente2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgente2.Properties.AutoHeight = False
    Me.edAgente2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgente2.Properties.MaxLength = 65536
    Me.edAgente2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgente2.Size = New System.Drawing.Size(54, 20)
    Me.edAgente2.TabIndex = 23
    '
    'lbAlconto
    '
    Me.lbAlconto.AutoSize = True
    Me.lbAlconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAlconto.Location = New System.Drawing.Point(248, 65)
    Me.lbAlconto.Name = "lbAlconto"
    Me.lbAlconto.NTSDbField = ""
    Me.lbAlconto.Size = New System.Drawing.Size(45, 13)
    Me.lbAlconto.TabIndex = 11
    Me.lbAlconto.Text = "al conto"
    Me.lbAlconto.Tooltip = ""
    Me.lbAlconto.UseMnemonic = False
    '
    'edAgente
    '
    Me.edAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgente.EditValue = "0"
    Me.edAgente.Location = New System.Drawing.Point(111, 166)
    Me.edAgente.Name = "edAgente"
    Me.edAgente.NTSDbField = ""
    Me.edAgente.NTSFormat = "0"
    Me.edAgente.NTSForzaVisZoom = False
    Me.edAgente.NTSOldValue = ""
    Me.edAgente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgente.Properties.AutoHeight = False
    Me.edAgente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgente.Properties.MaxLength = 65536
    Me.edAgente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgente.Size = New System.Drawing.Size(54, 20)
    Me.edAgente.TabIndex = 22
    '
    'lbAlmaga
    '
    Me.lbAlmaga.AutoSize = True
    Me.lbAlmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbAlmaga.Location = New System.Drawing.Point(248, 39)
    Me.lbAlmaga.Name = "lbAlmaga"
    Me.lbAlmaga.NTSDbField = ""
    Me.lbAlmaga.Size = New System.Drawing.Size(68, 13)
    Me.lbAlmaga.TabIndex = 12
    Me.lbAlmaga.Text = "al magazzino"
    Me.lbAlmaga.Tooltip = ""
    Me.lbAlmaga.UseMnemonic = False
    '
    'edCausa
    '
    Me.edCausa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCausa.EditValue = "0"
    Me.edCausa.Location = New System.Drawing.Point(111, 140)
    Me.edCausa.Name = "edCausa"
    Me.edCausa.NTSDbField = ""
    Me.edCausa.NTSFormat = "0"
    Me.edCausa.NTSForzaVisZoom = False
    Me.edCausa.NTSOldValue = ""
    Me.edCausa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCausa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCausa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCausa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCausa.Properties.AutoHeight = False
    Me.edCausa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCausa.Properties.MaxLength = 65536
    Me.edCausa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCausa.Size = New System.Drawing.Size(54, 20)
    Me.edCausa.TabIndex = 21
    '
    'edAldata
    '
    Me.edAldata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAldata.Location = New System.Drawing.Point(329, 10)
    Me.edAldata.Name = "edAldata"
    Me.edAldata.NTSDbField = ""
    Me.edAldata.NTSForzaVisZoom = False
    Me.edAldata.NTSOldValue = ""
    Me.edAldata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAldata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAldata.Properties.AutoHeight = False
    Me.edAldata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAldata.Properties.MaxLength = 65536
    Me.edAldata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAldata.Size = New System.Drawing.Size(100, 20)
    Me.edAldata.TabIndex = 13
    '
    'edDacomme
    '
    Me.edDacomme.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacomme.EditValue = "0"
    Me.edDacomme.Location = New System.Drawing.Point(111, 114)
    Me.edDacomme.Name = "edDacomme"
    Me.edDacomme.NTSDbField = ""
    Me.edDacomme.NTSFormat = "0"
    Me.edDacomme.NTSForzaVisZoom = False
    Me.edDacomme.NTSOldValue = ""
    Me.edDacomme.Properties.Appearance.Options.UseTextOptions = True
    Me.edDacomme.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDacomme.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDacomme.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDacomme.Properties.AutoHeight = False
    Me.edDacomme.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDacomme.Properties.MaxLength = 65536
    Me.edDacomme.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDacomme.Size = New System.Drawing.Size(100, 20)
    Me.edDacomme.TabIndex = 20
    '
    'edDadata
    '
    Me.edDadata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadata.Location = New System.Drawing.Point(111, 10)
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
    Me.edDadata.Size = New System.Drawing.Size(100, 20)
    Me.edDadata.TabIndex = 14
    '
    'edAlcomme
    '
    Me.edAlcomme.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlcomme.EditValue = "999999999"
    Me.edAlcomme.Location = New System.Drawing.Point(329, 114)
    Me.edAlcomme.Name = "edAlcomme"
    Me.edAlcomme.NTSDbField = ""
    Me.edAlcomme.NTSFormat = "0"
    Me.edAlcomme.NTSForzaVisZoom = False
    Me.edAlcomme.NTSOldValue = "999999999"
    Me.edAlcomme.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlcomme.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlcomme.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlcomme.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlcomme.Properties.AutoHeight = False
    Me.edAlcomme.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlcomme.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlcomme.Size = New System.Drawing.Size(100, 20)
    Me.edAlcomme.TabIndex = 19
    '
    'edDamaga
    '
    Me.edDamaga.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDamaga.EditValue = "0"
    Me.edDamaga.Location = New System.Drawing.Point(111, 36)
    Me.edDamaga.Name = "edDamaga"
    Me.edDamaga.NTSDbField = ""
    Me.edDamaga.NTSFormat = "0"
    Me.edDamaga.NTSForzaVisZoom = False
    Me.edDamaga.NTSOldValue = ""
    Me.edDamaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edDamaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDamaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamaga.Properties.AutoHeight = False
    Me.edDamaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamaga.Properties.MaxLength = 65536
    Me.edDamaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamaga.Size = New System.Drawing.Size(54, 20)
    Me.edDamaga.TabIndex = 15
    '
    'edAlconto
    '
    Me.edAlconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlconto.EditValue = "999999999"
    Me.edAlconto.Location = New System.Drawing.Point(329, 62)
    Me.edAlconto.Name = "edAlconto"
    Me.edAlconto.NTSDbField = ""
    Me.edAlconto.NTSFormat = "0"
    Me.edAlconto.NTSForzaVisZoom = False
    Me.edAlconto.NTSOldValue = "999999999"
    Me.edAlconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlconto.Properties.AutoHeight = False
    Me.edAlconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlconto.Size = New System.Drawing.Size(100, 20)
    Me.edAlconto.TabIndex = 18
    '
    'edAlmaga
    '
    Me.edAlmaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlmaga.EditValue = "9999"
    Me.edAlmaga.Location = New System.Drawing.Point(329, 36)
    Me.edAlmaga.Name = "edAlmaga"
    Me.edAlmaga.NTSDbField = ""
    Me.edAlmaga.NTSFormat = "0"
    Me.edAlmaga.NTSForzaVisZoom = False
    Me.edAlmaga.NTSOldValue = "9999"
    Me.edAlmaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlmaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlmaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlmaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlmaga.Properties.AutoHeight = False
    Me.edAlmaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlmaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlmaga.Size = New System.Drawing.Size(54, 20)
    Me.edAlmaga.TabIndex = 16
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(111, 62)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.AutoHeight = False
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(100, 20)
    Me.edDaconto.TabIndex = 17
    '
    'tsSchc2
    '
    Me.tsSchc2.AllowDrop = True
    Me.tsSchc2.Controls.Add(Me.pnTestata)
    Me.tsSchc2.Enable = True
    Me.tsSchc2.Name = "tsSchc2"
    Me.tsSchc2.Size = New System.Drawing.Size(445, 245)
    Me.tsSchc2.Text = "2 - Altri filtri testata"
    '
    'pnTestata
    '
    Me.pnTestata.AllowDrop = True
    Me.pnTestata.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestata.Appearance.Options.UseBackColor = True
    Me.pnTestata.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestata.Controls.Add(Me.cmdTestLock)
    Me.pnTestata.Controls.Add(Me.grTestata)
    Me.pnTestata.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestata.Location = New System.Drawing.Point(3, 3)
    Me.pnTestata.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTestata.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTestata.Name = "pnTestata"
    Me.pnTestata.NTSActiveTrasparency = True
    Me.pnTestata.Size = New System.Drawing.Size(442, 248)
    Me.pnTestata.TabIndex = 0
    Me.pnTestata.Text = "NtsPanel2"
    '
    'cmdTestLock
    '
    Me.cmdTestLock.ImageText = ""
    Me.cmdTestLock.Location = New System.Drawing.Point(331, 218)
    Me.cmdTestLock.Name = "cmdTestLock"
    Me.cmdTestLock.NTSContextMenu = Nothing
    Me.cmdTestLock.Size = New System.Drawing.Size(108, 23)
    Me.cmdTestLock.TabIndex = 7
    Me.cmdTestLock.Text = "Blocca/sblocca filtri"
    '
    'grTestata
    '
    Me.grTestata.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grTestata.EmbeddedNavigator.Name = ""
    Me.grTestata.Location = New System.Drawing.Point(0, 0)
    Me.grTestata.MainView = Me.grvTestata
    Me.grTestata.Name = "grTestata"
    Me.grTestata.Size = New System.Drawing.Size(439, 212)
    Me.grTestata.TabIndex = 6
    Me.grTestata.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTestata})
    '
    'grvTestata
    '
    Me.grvTestata.ActiveFilterEnabled = False
    Me.grvTestata.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_valoreda, Me.xx_valorea})
    Me.grvTestata.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTestata.Enabled = True
    Me.grvTestata.GridControl = Me.grTestata
    Me.grvTestata.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTestata.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvTestata.MinRowHeight = 14
    Me.grvTestata.Name = "grvTestata"
    Me.grvTestata.NTSAllowDelete = False
    Me.grvTestata.NTSAllowInsert = False
    Me.grvTestata.NTSAllowUpdate = True
    Me.grvTestata.NTSMenuContext = Nothing
    Me.grvTestata.OptionsCustomization.AllowRowSizing = True
    Me.grvTestata.OptionsFilter.AllowFilterEditor = False
    Me.grvTestata.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTestata.OptionsNavigation.UseTabKey = False
    Me.grvTestata.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTestata.OptionsView.ColumnAutoWidth = False
    Me.grvTestata.OptionsView.EnableAppearanceEvenRow = True
    Me.grvTestata.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTestata.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTestata.OptionsView.ShowGroupPanel = False
    Me.grvTestata.RowHeight = 16
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Caption = "Filtro"
    Me.xx_nome.Enabled = False
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowEdit = False
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_nome.OptionsColumn.ReadOnly = True
    Me.xx_nome.Visible = True
    Me.xx_nome.VisibleIndex = 0
    Me.xx_nome.Width = 120
    '
    'xx_valoreda
    '
    Me.xx_valoreda.AppearanceCell.Options.UseBackColor = True
    Me.xx_valoreda.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valoreda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valoreda.Caption = "Valore DA"
    Me.xx_valoreda.Enabled = True
    Me.xx_valoreda.FieldName = "xx_valoreda"
    Me.xx_valoreda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valoreda.Name = "xx_valoreda"
    Me.xx_valoreda.NTSRepositoryComboBox = Nothing
    Me.xx_valoreda.NTSRepositoryItemCheck = Nothing
    Me.xx_valoreda.NTSRepositoryItemMemo = Nothing
    Me.xx_valoreda.NTSRepositoryItemText = Nothing
    Me.xx_valoreda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_valoreda.Visible = True
    Me.xx_valoreda.VisibleIndex = 1
    Me.xx_valoreda.Width = 166
    '
    'xx_valorea
    '
    Me.xx_valorea.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea.Caption = "Valore A"
    Me.xx_valorea.Enabled = True
    Me.xx_valorea.FieldName = "xx_valorea"
    Me.xx_valorea.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea.Name = "xx_valorea"
    Me.xx_valorea.NTSRepositoryComboBox = Nothing
    Me.xx_valorea.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea.NTSRepositoryItemText = Nothing
    Me.xx_valorea.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_valorea.Visible = True
    Me.xx_valorea.VisibleIndex = 2
    Me.xx_valorea.Width = 300
    '
    'tsSchc3
    '
    Me.tsSchc3.AllowDrop = True
    Me.tsSchc3.Controls.Add(Me.pnCorpo)
    Me.tsSchc3.Enable = True
    Me.tsSchc3.Name = "tsSchc3"
    Me.tsSchc3.Size = New System.Drawing.Size(445, 245)
    Me.tsSchc3.Text = "3 - Altri filtri corpo"
    '
    'pnCorpo
    '
    Me.pnCorpo.AllowDrop = True
    Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCorpo.Appearance.Options.UseBackColor = True
    Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCorpo.Controls.Add(Me.cmdCorpoLock)
    Me.pnCorpo.Controls.Add(Me.grCorpo)
    Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCorpo.Location = New System.Drawing.Point(0, 3)
    Me.pnCorpo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCorpo.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCorpo.Name = "pnCorpo"
    Me.pnCorpo.NTSActiveTrasparency = True
    Me.pnCorpo.Size = New System.Drawing.Size(445, 248)
    Me.pnCorpo.TabIndex = 0
    Me.pnCorpo.Text = "NtsPanel1"
    '
    'cmdCorpoLock
    '
    Me.cmdCorpoLock.ImageText = ""
    Me.cmdCorpoLock.Location = New System.Drawing.Point(334, 218)
    Me.cmdCorpoLock.Name = "cmdCorpoLock"
    Me.cmdCorpoLock.NTSContextMenu = Nothing
    Me.cmdCorpoLock.Size = New System.Drawing.Size(108, 23)
    Me.cmdCorpoLock.TabIndex = 8
    Me.cmdCorpoLock.Text = "Blocca/sblocca filtri"
    '
    'grCorpo
    '
    Me.grCorpo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grCorpo.EmbeddedNavigator.Name = ""
    Me.grCorpo.Location = New System.Drawing.Point(3, 0)
    Me.grCorpo.MainView = Me.grvCorpo
    Me.grCorpo.Name = "grCorpo"
    Me.grCorpo.Size = New System.Drawing.Size(439, 212)
    Me.grCorpo.TabIndex = 6
    Me.grCorpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCorpo})
    '
    'grvCorpo
    '
    Me.grvCorpo.ActiveFilterEnabled = False
    Me.grvCorpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_corponome, Me.xx_corpovalda, Me.xx_corpovala})
    Me.grvCorpo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCorpo.Enabled = True
    Me.grvCorpo.GridControl = Me.grCorpo
    Me.grvCorpo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCorpo.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvCorpo.MinRowHeight = 14
    Me.grvCorpo.Name = "grvCorpo"
    Me.grvCorpo.NTSAllowDelete = False
    Me.grvCorpo.NTSAllowInsert = False
    Me.grvCorpo.NTSAllowUpdate = True
    Me.grvCorpo.NTSMenuContext = Nothing
    Me.grvCorpo.OptionsCustomization.AllowRowSizing = True
    Me.grvCorpo.OptionsFilter.AllowFilterEditor = False
    Me.grvCorpo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCorpo.OptionsNavigation.UseTabKey = False
    Me.grvCorpo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCorpo.OptionsView.ColumnAutoWidth = False
    Me.grvCorpo.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCorpo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCorpo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCorpo.OptionsView.ShowGroupPanel = False
    Me.grvCorpo.RowHeight = 16
    '
    'xx_corponome
    '
    Me.xx_corponome.AppearanceCell.Options.UseBackColor = True
    Me.xx_corponome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_corponome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_corponome.Caption = "Filtro"
    Me.xx_corponome.Enabled = False
    Me.xx_corponome.FieldName = "xx_corponome"
    Me.xx_corponome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_corponome.Name = "xx_corponome"
    Me.xx_corponome.NTSRepositoryComboBox = Nothing
    Me.xx_corponome.NTSRepositoryItemCheck = Nothing
    Me.xx_corponome.NTSRepositoryItemMemo = Nothing
    Me.xx_corponome.NTSRepositoryItemText = Nothing
    Me.xx_corponome.OptionsColumn.AllowEdit = False
    Me.xx_corponome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_corponome.OptionsColumn.ReadOnly = True
    Me.xx_corponome.Visible = True
    Me.xx_corponome.VisibleIndex = 0
    Me.xx_corponome.Width = 120
    '
    'xx_corpovalda
    '
    Me.xx_corpovalda.AppearanceCell.Options.UseBackColor = True
    Me.xx_corpovalda.AppearanceCell.Options.UseTextOptions = True
    Me.xx_corpovalda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_corpovalda.Caption = "Valore DA"
    Me.xx_corpovalda.Enabled = True
    Me.xx_corpovalda.FieldName = "xx_corpovalda"
    Me.xx_corpovalda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_corpovalda.Name = "xx_corpovalda"
    Me.xx_corpovalda.NTSRepositoryComboBox = Nothing
    Me.xx_corpovalda.NTSRepositoryItemCheck = Nothing
    Me.xx_corpovalda.NTSRepositoryItemMemo = Nothing
    Me.xx_corpovalda.NTSRepositoryItemText = Nothing
    Me.xx_corpovalda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_corpovalda.Visible = True
    Me.xx_corpovalda.VisibleIndex = 1
    Me.xx_corpovalda.Width = 166
    '
    'xx_corpovala
    '
    Me.xx_corpovala.AppearanceCell.Options.UseBackColor = True
    Me.xx_corpovala.AppearanceCell.Options.UseTextOptions = True
    Me.xx_corpovala.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_corpovala.Caption = "Valore A"
    Me.xx_corpovala.Enabled = True
    Me.xx_corpovala.FieldName = "xx_corpovala"
    Me.xx_corpovala.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_corpovala.Name = "xx_corpovala"
    Me.xx_corpovala.NTSRepositoryComboBox = Nothing
    Me.xx_corpovala.NTSRepositoryItemCheck = Nothing
    Me.xx_corpovala.NTSRepositoryItemMemo = Nothing
    Me.xx_corpovala.NTSRepositoryItemText = Nothing
    Me.xx_corpovala.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_corpovala.Visible = True
    Me.xx_corpovala.VisibleIndex = 2
    Me.xx_corpovala.Width = 300
    '
    'FRMMGSCHC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(466, 316)
    Me.Controls.Add(Me.tsSchc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSCHC"
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE CLIENTI/FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsSchc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSchc.ResumeLayout(False)
    Me.tsSchc1.ResumeLayout(False)
    CType(Me.pnPrincipale, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPrincipale.ResumeLayout(False)
    Me.pnPrincipale.PerformLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmClifor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmClifor.ResumeLayout(False)
    Me.fmClifor.PerformLayout()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClifor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgente2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCausa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAldata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacomme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlcomme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSchc2.ResumeLayout(False)
    CType(Me.pnTestata, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestata.ResumeLayout(False)
    CType(Me.grTestata, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTestata, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSchc3.ResumeLayout(False)
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCorpo.ResumeLayout(False)
    CType(Me.grCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      opFornitori.NTSSetParam(oMenu, oApp.Tr(Me, 128644695983750000, "Fornitori"), "F")
      opClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128644695983906250, "Clienti"), "C")
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 128644695984062500, "Tipo documento"))
      ckTipodoc.NTSSetParam(oMenu, oApp.Tr(Me, 128644695984218750, "Tipo documento"), "S", "N")
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128644695984375000, "Serie"), CLN__STD.SerieMaxLen)
      edClifor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695984531250, "Categoria Cli/For"), tabcate)
      edAgente2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695984687500, "Codice agente 2"), tabcage)
      edAgente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695984843750, "Codice agente"), tabcage)
      edCausa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985000000, "Causale"), tabcaum)
      edDacomme.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985156250, "Da commessa"), tabcommess)
      edAlcomme.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985312500, "Alla commessa"), tabcommess)
      edAlconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985468750, "Al conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985625000, "Dal conto"), tabanagrac)
      edAlmaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985781250, "Al magazzino"), tabmaga)
      edDamaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985937500, "Dal magazzino"), tabmaga)
      edDadata.NTSSetParam(oMenu, oApp.Tr(Me, 128644695986093750, "Dalla data"), False)
      edAldata.NTSSetParam(oMenu, oApp.Tr(Me, 128644695986250000, "Alla data"), False)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")

      grvTestata.NTSSetParam(oMenu, oApp.Tr(Me, 128653615587343750, "Altri filtri testata"))
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128823852909011386, "Nome filtro"), dttCampi, "xx_nome", "cb_nomcampo")
      xx_valoreda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128823853074169750, "Valore filtro DA"), 0)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128823853712615422, "Valore filtro A"), 0)
      xx_valoreda.NTSSetParamZoom("__")
      xx_valorea.NTSSetParamZoom("__")

      grvCorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128655057939062500, "Altri filtri corpo"))
      xx_corponome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128823852951043174, "Nome filtro"), dttCorpocampi, "xx_nome", "cb_nomcampo")
      xx_corpovalda.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128823853136983054, "Valore filtro DA"), 0)
      xx_corpovala.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128823853737303238, "Valore filtro A"), 0)
      xx_corpovalda.NTSSetParamZoom("__")
      xx_corpovala.NTSSetParamZoom("__")

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
    Dim oParam As New CLE__PATB
    Dim strTipo As String
    Dim strTmp As String = ""
    Try
      If edDaconto.Focused Then
        If opClienti.Checked Then
          strTipo = "C"
        Else
          strTipo = "F"
        End If
        SetFastZoom(edDaconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edDaconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipo
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edDaconto.Text Then edDaconto.NTSTextDB = NTSZOOM.strIn
      ElseIf edAlconto.Focused Then
        If opClienti.Checked Then
          strTipo = "C"
        Else
          strTipo = "F"
        End If
        SetFastZoom(edAlconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edAlconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipo
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAlconto.Text Then edAlconto.NTSTextDB = NTSZOOM.strIn

      ElseIf grTestata.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        If NTSCStr(grvTestata.NTSGetCurrentDataRow!xx_nome) = "." Then Return 'sono su una colonna N.A.
        If grvTestata.FocusedColumn.Name = "xx_valoreda" Then
          strTmp = NTSCStr(grvTestata.NTSGetCurrentDataRow!xx_valoreda)
        Else
          strTmp = NTSCStr(grvTestata.NTSGetCurrentDataRow!xx_valorea)
        End If
        ApriZoomTabellaTestata(strTmp, NTSCStr(grvTestata.NTSGetCurrentDataRow!xx_nome))

      ElseIf grCorpo.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        If NTSCStr(grvCorpo.NTSGetCurrentDataRow!xx_corponome) = "." Then Return 'sono su una colonna N.A.
        If grvCorpo.FocusedColumn.Name = "xx_corpovalda" Then
          strTmp = NTSCStr(grvCorpo.NTSGetCurrentDataRow!xx_corpovalda)
        Else
          strTmp = NTSCStr(grvCorpo.NTSGetCurrentDataRow!xx_corpovala)
        End If
        ApriZoomTabellaCorpo(strTmp, NTSCStr(grvCorpo.NTSGetCurrentDataRow!xx_corponome))
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

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

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      StampaGriglia()
      If bDaGest = True Then
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validate/Changed"
  Public Overridable Sub edDadata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDadata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAldata.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823853829491918, "'Dalla data' deve essere precedente o uguale a 'Alla data'"))
        edDadata.Text = "01/01/" + NTSCStr(Now.Year)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAldata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAldata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAldata.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644734760000000, "'Dalla data' deve essere precedente o uguale a 'Alla data'"))
        edAldata.Text = "01/01/" + NTSCStr(Now.Year)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDamaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDamaga.Validated
    Try
      If NTSCInt(edDamaga.Text) > NTSCInt(edAlmaga.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823853961524858, "'Dal magazzino' deve essere inferiore o uguale a 'Al magazzino'"))
        edDamaga.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAlmaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlmaga.Validated
    Try
      If NTSCInt(edDamaga.Text) > NTSCInt(edAlmaga.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644735708281250, "'Dal magazzino' deve essere inferiore o uguale a 'Al magazzino'"))
        edAlmaga.Text = "9999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If NTSCInt(edDaconto.Text) > NTSCInt(edAlconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823854044338418, "'Dal conto' deve essere inferiore o uguale a 'Al conto'"))
        edDaconto.Text = "0"
      End If

      If edDaconto.Text <> "0" Then edAlconto.Text = edDaconto.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAlconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlconto.Validated
    Try
      If NTSCInt(edDaconto.Text) > NTSCInt(edAlconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644736937968750, "'Dal conto' deve essere inferiore o uguale a 'Al conto'"))
        edAlconto.Text = "999999999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDacomme_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacomme.Validated
    Try
      If NTSCInt(edDacomme.Text) > NTSCInt(edAlcomme.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823854119339378, "'Dalla commessa' deve essere inferiore o uguale a 'Alla commessa'"))
        edDacomme.Text = "0"
      End If

      If edDacomme.Text <> "0" Then edAlcomme.Text = edDacomme.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAlcomme_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlcomme.Validated
    Try
      If NTSCInt(edDacomme.Text) > NTSCInt(edAlcomme.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644736988906250, "'Dalla commessa' deve essere inferiore o uguale a 'Alla commessa'"))
        edAlcomme.Text = "999999999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCausa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCausa.Validated
    Try
      If edCausa.Text = "0" Then
        lbDescausa.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edCausa.Text, "TABCAUM", "N", lbDescausa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644765277812500, "La causale inserita è inesistente"))
        edCausa.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAgente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAgente.Validated
    Try
      If edAgente.Text = "0" Then
        lbDesagente.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edAgente.Text, "TABCAGE", "N", lbDesagente.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644765834218750, "Il codice agente inserito è inesistente"))
        edAgente.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAgente2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAgente2.Validated
    Try
      If edAgente2.Text = "0" Then
        lbDesagente2.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edAgente2.Text, "TABCAGE", "N", lbDesagente2.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644766122343750, "Il codice agente 2 inserito è inesistente"))
        edAgente2.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edClifor_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edClifor.Validated
    Try
      If edClifor.Text = "0" Then
        lbDesclifor.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edClifor.Text, "TABCATE", "N", lbDesclifor.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644766395468750, "La categoria Cli/For inserita è inesistente"))
        edClifor.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTipodoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTipodoc.CheckedChanged
    Try
      If ckTipodoc.Checked Then
        cbTipodoc.Enabled = True
      Else
        cbTipodoc.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Testata Estesi"
  Public Overridable Function CheckTestata() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome) = "." Then
          dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda = ""
          dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea = ""
        Else
          If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584505571093750, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128823854194809094, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584506055937500, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503331406250, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503236718750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsTestata.Tables("FILTRI1").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CreaDatatableTestata() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsTestata.Tables.Add("FILTRI1")
      dsTestata.Tables("FILTRI1").Columns.Add("xx_nome", GetType(String))
      dsTestata.Tables("FILTRI1").Columns.Add("xx_valoreda", GetType(String))
      dsTestata.Tables("FILTRI1").Columns.Add("xx_valorea", GetType(String))
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsTestata.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dsTestata.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNMGSCHC", "RECENT", ".", "FiltriTestata", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsTestata.Tables("FILTRI1").Rows(i)!xx_nome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsTestata.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ComponiWhereTestataEstesi(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQueryTestata As String = ""
    Try

      If bCrystal Then
        For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CDblSQL(NTSCDec(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda))
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CDblSQL(NTSCDec(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea))
              Case 8
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & ConvDataRpt(NTSCDate(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda).ToShortDateString)
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & ConvDataRpt(NTSCDate(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea).ToShortDateString)
              Case Else
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CampoTesto(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda), False)
                strQueryTestata += " AND {" & dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CampoTesto(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea), False)
            End Select
          End If
        Next    'For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsTestata.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDblSQL(NTSCDec(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDblSQL(NTSCDec(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case 8
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDataSQL(NTSCDate(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDataSQL(NTSCDate(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case Else
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CampoTesto(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valoreda), False) & "§"
                strQueryTestata += dsTestata.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CampoTesto(NTSCStr(dsTestata.Tables("FILTRI1").Rows(i)!xx_valorea), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsTestata.Tables("FILTRI1").Rows.Count - 1
        If strQueryTestata.Length > 0 Then strQueryTestata = strQueryTestata.Substring(0, strQueryTestata.Length - 1)
      End If


      Return strQueryTestata

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Sub ApriZoomTabellaTestata(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try

      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = "TESTMAG"
        oParam.strDescr = strCampo
        NTSZOOM.strIn = NTSCStr(grvTestata.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvTestata.EditingValue) Then grvTestata.SetFocusedValue(NTSZOOM.strIn)
      Else
        SetFastZoom(NTSCStr(grvTestata.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvTestata.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvTestata.EditingValue) Then grvTestata.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdTestLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestLock.Click
    Try
      xx_nome.Enabled = Not xx_nome.Enabled
      grvTestata.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Corpo Estesi"
  Public Overridable Function CheckCorpo() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome) = "." Then
          dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda = ""
          dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala = ""
        Else
          If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda) <> "" Then
            dtrT = dttCorpocampi.Select("cb_nomcampo = " & CStrSQL(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome))
            If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128655069955000000, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128655070012187500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128655070041406250, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala) <> "" Then
            dtrT = dttCorpocampi.Select("cb_nomcampo = " & CStrSQL(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome))
            If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128655070065000000, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128655070097968750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128655070121093750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_corponome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsCorpo.Tables("FILTRI1").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CreaDatatableCorpo() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsCorpo.Tables.Add("FILTRI1")
      dsCorpo.Tables("FILTRI1").Columns.Add("xx_corponome", GetType(String))
      dsCorpo.Tables("FILTRI1").Columns.Add("xx_corpovalda", GetType(String))
      dsCorpo.Tables("FILTRI1").Columns.Add("xx_corpovala", GetType(String))
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCorpocampi.Select("cb_nomcampo = '.'") : dsCorpo.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dsCorpo.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNMGSCHC", "RECENT", ".", "FiltriCorpo", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsCorpo.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ComponiWhereCorpoEstesi(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQueryCorpo As String = ""
    Try

      If bCrystal Then
        For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome).Trim <> "." And NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda).Trim <> "" And NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala).Trim <> "" Then
            dtrT = dttCorpocampi.Select("cb_nomcampo = " & CStrSQL(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} >= " & CDblSQL(NTSCDec(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda))
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} <= " & CDblSQL(NTSCDec(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala))
              Case 8
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} >= " & ConvDataRpt(NTSCDate(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda).ToShortDateString)
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} <= " & ConvDataRpt(NTSCDate(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala).ToShortDateString)
              Case Else
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} >= " & CampoTesto(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda), False)
                strQueryCorpo += " AND {" & dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & "} <= " & CampoTesto(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala), False)
            End Select
          End If
        Next    'For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome).Trim <> "." And NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda).Trim <> "" And NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala).Trim <> "" Then
            dtrT = dttCorpocampi.Select("cb_nomcampo = " & CStrSQL(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " >= " & CDblSQL(NTSCDec(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda)) & "§"
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " <= " & CDblSQL(NTSCDec(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala)) & "§"
              Case 8
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " >= " & CDataSQL(NTSCDate(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda)) & "§"
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " <= " & CDataSQL(NTSCDate(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala)) & "§"
              Case Else
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " >= " & CampoTesto(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovalda), False) & "§"
                strQueryCorpo += dsCorpo.Tables("FILTRI1").Rows(i)!xx_corponome.ToString & " <= " & CampoTesto(NTSCStr(dsCorpo.Tables("FILTRI1").Rows(i)!xx_corpovala), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsCorpo.Tables("FILTRI1").Rows.Count - 1
        If strQueryCorpo.Length > 0 Then strQueryCorpo = strQueryCorpo.Substring(0, strQueryCorpo.Length - 1)
      End If


      Return strQueryCorpo

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Sub ApriZoomTabellaCorpo(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try

      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128655070404375000, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = "MOVMAG"
        oParam.strDescr = strCampo
        NTSZOOM.strIn = NTSCStr(grvCorpo.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCorpo.EditingValue) Then grvCorpo.SetFocusedValue(NTSZOOM.strIn)
      Else
        SetFastZoom(NTSCStr(grvCorpo.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvCorpo.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCorpo.EditingValue) Then grvCorpo.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdCorpoLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCorpoLock.Click
    Try
      xx_corponome.Enabled = Not xx_corponome.Enabled
      grvCorpo.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      strOut = CStrSQL(strTesto & bFil)
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttTipo As New DataTable()
    Try
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"A", "Fatture Imm. emesse"})
      dttTipo.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipo.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      'dttTipo.Rows.Add(New Object() {"D", "Fatture Diff. emesse"})
      dttTipo.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipo.Rows.Add(New Object() {"F", "Ric. Fiscale emessa"})
      dttTipo.Rows.Add(New Object() {"I", "Riemissione Ric. Fiscali"})
      dttTipo.Rows.Add(New Object() {"J", "Note Accr. ricevute"})
      'dttTipo.Rows.Add(New Object() {"K", "Fatture Diff. ricevute"})
      dttTipo.Rows.Add(New Object() {"L", "Fatture Imm. ricevute"})
      dttTipo.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipo.Rows.Add(New Object() {"N", "Note Accr. emessa"})
      'dttTipo.Rows.Add(New Object() {"P", "Fatt. Ric. Fisc. Differita"})
      dttTipo.Rows.Add(New Object() {"S", "Fatt. Ric. Fisc. emessa"})
      dttTipo.Rows.Add(New Object() {"T", "Carico da prod."})
      dttTipo.Rows.Add(New Object() {"U", "Scarico da prod."})
      dttTipo.Rows.Add(New Object() {"Z", "Bolle mov. interna"})
      dttTipo.AcceptChanges()

      cbTipodoc.DataSource = dttTipo
      cbTipodoc.DisplayMember = "val"
      cbTipodoc.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ComponiFormula(ByVal bTipodoc As Boolean, ByVal strTipodoc As String, _
    ByVal bClienti As Boolean, ByVal dtDadata As Date, ByVal dtAldata As Date, ByVal lDamaga As Integer, _
    ByVal lAlmaga As Integer, ByVal lDaconto As Integer, ByVal lAlconto As Integer, ByVal lDacomme As Integer, _
    ByVal lAlcomme As Integer, ByVal strSerie As String, ByVal lCausa As Integer, ByVal lAgente As Integer, _
    ByVal lAgente2 As Integer, ByVal lClifor As Integer, ByRef strWhere As String) As Boolean
    Dim strTipo As String
    Dim strTestata As String
    Dim strCorpo As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bClienti = True Then strTipo = "C" Else strTipo = "F"
      '--------------------------------------------------------------------------------------------------------------
      strWhere = "{KEYMAG.km_aammgg} In Date(" & dtDadata.Year & "," & dtDadata.Month.ToString("00") & "," & dtDadata.Day.ToString("00") & ")" & _
        " To Date(" & dtAldata.Year & "," & dtAldata.Month & "," & dtAldata.Day & ")" & _
        " And {KEYMAG.km_magaz} In " & lDamaga & " To " & lAlmaga & _
        " And {KEYMAG.km_conto} In " & lDaconto & " To " & lAlconto & _
        " And {KEYMAG.km_commeca} In " & lDacomme & " To " & lAlcomme & _
        " And {ANAGRA.an_tipo} = " & CStrSQL(strTipo) & _
        " And {KEYMAG.codditt} = " & CStrSQL(DittaCorrente)
      If bTipodoc = True Then strWhere &= " And {KEYMAG.km_tipork} = '" & strTipodoc & "'"
      If strSerie <> "" Then strWhere &= " And {KEYMAG.km_serie} = '" & strSerie & "'"
      If lCausa <> 0 Then strWhere &= " And {KEYMAG.km_causale} = " & lCausa
      If lAgente <> 0 Then strWhere &= " And {TESTMAG.tm_codagen} = " & lAgente
      If lAgente2 <> 0 Then strWhere &= " And {TESTMAG.tm_codagen2} = " & lAgente2
      If lClifor <> 0 Then strWhere &= " And {ANAGRA.an_categ} = " & lClifor
      '--------------------------------------------------------------------------------------------------------------
      strTestata = ComponiWhereTestataEstesi(True)
      strCorpo = ComponiWhereCorpoEstesi(True)
      '--------------------------------------------------------------------------------------------------------------
      If strWhereArtico <> "" Then oCleSchc.TraduciWhereReport(strWhereArtico, "ARTICO", strWhere)
      If strTestata <> "" Then strWhere &= strTestata
      If strCorpo <> "" Then strWhere &= strCorpo
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCrm = True) And (bIsCRMUser = True) Then
        If oCleSchc.bIs15 = False Then
          strWhere &= " And ("
          If strAccvis <> "T" Then
            strWhere &= "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
            Select Case strAccvis
              Case "P" : strWhere &= " And {leads.le_opinc} = " & lCodorgaOperat
              Case "C" : strWhere &= " And {leads.le_opinc} In [" & strRegvis & "]"
            End Select
            strWhere &= ")"
          Else
            strWhere &= " {anagra.an_tipo} = 'C'"
          End If
          If bAmm = False Then
            strWhere &= " And {anagra.an_tipo} <> 'F'"
          Else
            strWhere &= " Or {anagra.an_tipo} <> 'C'"
          End If
          strWhere &= ")"
        Else
          If oCleSchc.nCodcageAccdito <> 0 Then
            strWhere &= " And ({TESTMAG.tm_codagen} = " & oCleSchc.nCodcageAccdito & " Or {TESTMAG.tm_codagen2} = " & oCleSchc.nCodcageAccdito & ")"
          End If
          RiempiTmpTable()
          strWhere &= " And {TTSUBQCRM.instid} = " & oCleSchc.lIITtsubqcrm
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub PassaDatiBE(ByVal bTipodoc As Boolean, ByVal strTipodoc As String, ByVal bClienti As Boolean, _
                                       ByVal dtDadata As Date, ByVal dtAldata As Date, ByVal lDamaga As Integer, ByVal lAlmaga As Integer, _
                                       ByVal lDaconto As Integer, ByVal lAlconto As Integer, ByVal lDacomme As Integer, _
                                       ByVal lAlcomme As Integer, ByVal strSerie As String, ByVal lCausa As Integer, ByVal lAgente As Integer, _
                                       ByVal lAgente2 As Integer, ByVal lClifor As Integer, ByVal strWhereArtico As String)
    Try
      oCleSchc.bTipodoc = bTipodoc
      oCleSchc.strTipodoc = strTipodoc
      oCleSchc.dtDadata = dtDadata
      oCleSchc.dtAldata = dtAldata
      oCleSchc.lDamaga = lDamaga
      oCleSchc.lAlmaga = lAlmaga
      oCleSchc.lDaconto = lDaconto
      oCleSchc.lAlconto = lAlconto
      oCleSchc.lDacomme = lDacomme
      oCleSchc.lAlcomme = lAlcomme
      oCleSchc.strSerie = strSerie
      oCleSchc.lCausa = lCausa
      oCleSchc.lAgente = lAgente
      oCleSchc.lAgente2 = lAgente2
      oCleSchc.lClifor = lClifor
      oCleSchc.strWhereArtico = strWhereArtico
      oCleSchc.strWhereTestata = ComponiWhereTestataEstesi(False)
      oCleSchc.strWhereCorpo = ComponiWhereCorpoEstesi(False)
      If bClienti Then
        oCleSchc.strTipo = "C"
      Else
        oCleSchc.strTipo = "F"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not ComponiFormula(ckTipodoc.Checked, cbTipodoc.SelectedValue, opClienti.Checked, NTSCDate(edDadata.Text), _
        NTSCDate(edAldata.Text), NTSCInt(edDamaga.Text), NTSCInt(edAlmaga.Text), NTSCInt(edDaconto.Text), _
        NTSCInt(edAlconto.Text), NTSCInt(edDacomme.Text), NTSCInt(edAlcomme.Text), edSerie.Text, _
        NTSCInt(edCausa.Text), NTSCInt(edAgente.Text), NTSCInt(edAgente2.Text), NTSCInt(edClifor.Text), strCrpe) Then Return
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHC", "Reports1", " ", 0, nDestin, "BSMGSCHC.RPT", False, "Stampa/Visualizzazione schede clienti/fornitori", False)
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATA", "'Dal : " & edDadata.Text & " al : " & edAldata.Text & "'")
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        For j = 3 To 12
          If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
          End If
        Next j
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub StampaGriglia()
    Dim frmGrcf As FRMMGGRCF = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      PassaDatiBE(ckTipodoc.Checked, cbTipodoc.SelectedValue, opClienti.Checked, NTSCDate(edDadata.Text), _
        NTSCDate(edAldata.Text), NTSCInt(edDamaga.Text), NTSCInt(edAlmaga.Text), NTSCInt(edDaconto.Text), _
        NTSCInt(edAlconto.Text), NTSCInt(edDacomme.Text), NTSCInt(edAlcomme.Text), edSerie.Text, _
        NTSCInt(edCausa.Text), NTSCInt(edAgente.Text), NTSCInt(edAgente2.Text), NTSCInt(edClifor.Text), _
        strWhereArtico)
      '--------------------------------------------------------------------------------------------------------------
      frmGrcf = CType(NTSNewFormModal("FRMMGGRCF"), FRMMGGRCF)
      frmGrcf.Init(oMenu, Nothing, DittaCorrente)
      frmGrcf.InitEntity(oCleSchc)
      frmGrcf.strGrcfWhereCRM = oCleSchc.strWhereCRM
      If Not oCallParams Is Nothing Then
        frmGrcf.bNoModal = False
      Else
        frmGrcf.bNoModal = tlbNoModal.Checked
      End If
      If frmGrcf.bNoModal Then
        frmGrcf.Show()
      Else
        frmGrcf.ShowDialog()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmGrcf Is Nothing Then
        If frmGrcf.bNoModal = False Then
          frmGrcf.Dispose()
          frmGrcf = Nothing
        End If
      End If
    End Try
  End Sub

  Public Overridable Sub cmdSelartico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NtsButton1.Click
    Dim oPar As CLE__PATB = New CLE__PATB
    Try
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False Then strWhereArtico = oPar.strOut.Trim
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        edSerie.Text = " "
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If ckSerie.Checked And edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      If (bModuloCrm = False) Or (bIsCRMUser = False) Or (oCleSchc.bIs15 = False) Then Return True
      oMenu.ResetTblInstId("TTSUBQCRM", False, oCleSchc.lIITtsubqcrm)
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTmpTable() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.strWhereCRM = ""
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.RiempiTmpTable(bModuloCrm, bIsCRMUser)
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.WhereCRM()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
End Class