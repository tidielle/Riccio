Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEOL
  Private components As System.ComponentModel.IContainer

  Public oCallParams As CLE__CLDP
  Public oCleSeol As CLEORSEOL = Nothing
  Public strWhereArtico As String = ""

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORSEOL", "BEORSEOL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128617301390000000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSeol = CType(oTmp, CLEORSEOL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORSEOL", strRemoteServer, strRemotePort)
    AddHandler oCleSeol.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSeol.Init(oApp, oScript, oMenu.oCleComm, "TABZONE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      ckOpInterni.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513125000, "Ordini interni di produzione"), "S", "N")
      opIT.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513281250, "Impegni di trasferimento"), "IT")
      opOP.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513437500, "Ordini di produzione"), "OP")
      opOF.NTSSetParam(oMenu, oApp.Tr(Me, 128849573148586000, "Ordini a fornitori"), "OF")
      ckGenerato.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513593750, "Generato"), "S", "N")
      ckEmRDO.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513750000, "Emissione RDO"), "S", "N")
      ckEmRDA.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513906250, "Emissione RDA"), "S", "N")
      ckAppRDA.NTSSetParam(oMenu, oApp.Tr(Me, 128849573206930000, "Approvazione RDA"), "S", "N")
      ckConfermato.NTSSetParam(oMenu, oApp.Tr(Me, 128607644514062500, "Confermato/app.RDO"), "S", "N")
      ckCongelato.NTSSetParam(oMenu, oApp.Tr(Me, 128607644514218750, "Congelato"), "S", "N")
      ckEmOrdine.NTSSetParam(oMenu, oApp.Tr(Me, 128849573277286000, "Emesso Ordine"), "S", "N")
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128849573296162000, "Conto cliente/fornitore"), tabanagraf)
      edMagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128849573308954000, "MAgazzino"), tabmaga)
      edCommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128849573322058000, "Commessa"), tabcommess)
      edDatordDa.NTSSetParam(oMenu, oApp.Tr(Me, 128849573333602000, "Data massima ordine DA"), False)
      edDatconsA.NTSSetParam(oMenu, oApp.Tr(Me, 128849573345458000, "Data consegna A"), False)
      edDatconsDa.NTSSetParam(oMenu, oApp.Tr(Me, 128849573357938000, "Data consegna DA"), False)
      edDatordA.NTSSetParam(oMenu, oApp.Tr(Me, 128849573370106000, "Data massima ordine A"), False)

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

  Public Overridable Sub InitializeComponent()
    Me.fmTipo = New NTSInformatica.NTSGroupBox
    Me.ckOpInterni = New NTSInformatica.NTSCheckBox
    Me.opIT = New NTSInformatica.NTSRadioButton
    Me.opOP = New NTSInformatica.NTSRadioButton
    Me.opOF = New NTSInformatica.NTSRadioButton
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.edDatconsDa = New NTSInformatica.NTSTextBoxData
    Me.edDatordA = New NTSInformatica.NTSTextBoxData
    Me.lbContoLabel = New NTSInformatica.NTSLabel
    Me.lbCommecaLabel = New NTSInformatica.NTSLabel
    Me.lbMagazLabel = New NTSInformatica.NTSLabel
    Me.lbDatcons = New NTSInformatica.NTSLabel
    Me.lbDatord = New NTSInformatica.NTSLabel
    Me.edDatconsA = New NTSInformatica.NTSTextBoxData
    Me.edDatordDa = New NTSInformatica.NTSTextBoxData
    Me.fmStato = New NTSInformatica.NTSGroupBox
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.ckGenerato = New NTSInformatica.NTSCheckBox
    Me.ckEmRDO = New NTSInformatica.NTSCheckBox
    Me.ckEmRDA = New NTSInformatica.NTSCheckBox
    Me.ckAppRDA = New NTSInformatica.NTSCheckBox
    Me.ckConfermato = New NTSInformatica.NTSCheckBox
    Me.ckCongelato = New NTSInformatica.NTSCheckBox
    Me.ckEmOrdine = New NTSInformatica.NTSCheckBox
    Me.pnSx = New NTSInformatica.NTSPanel
    Me.fmOrdinamento = New NTSInformatica.NTSGroupBox
    Me.opCentro = New NTSInformatica.NTSRadioButton
    Me.opLineaFam = New NTSInformatica.NTSRadioButton
    Me.opMagaz = New NTSInformatica.NTSRadioButton
    Me.opDatcons = New NTSInformatica.NTSRadioButton
    Me.opDesForn = New NTSInformatica.NTSRadioButton
    Me.opCodForn = New NTSInformatica.NTSRadioButton
    Me.opDesArtFase = New NTSInformatica.NTSRadioButton
    Me.opCodArtFase = New NTSInformatica.NTSRadioButton
    Me.opProg = New NTSInformatica.NTSRadioButton
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipo.SuspendLayout()
    CType(Me.ckOpInterni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opIT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStato.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.ckGenerato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEmRDO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEmRDA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAppRDA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConfermato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCongelato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEmOrdine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSx.SuspendLayout()
    CType(Me.fmOrdinamento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrdinamento.SuspendLayout()
    CType(Me.opCentro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opLineaFam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDesForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDesArtFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodArtFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opProg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'fmTipo
    '
    Me.fmTipo.AllowDrop = True
    Me.fmTipo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipo.Appearance.Options.UseBackColor = True
    Me.fmTipo.Controls.Add(Me.ckOpInterni)
    Me.fmTipo.Controls.Add(Me.opIT)
    Me.fmTipo.Controls.Add(Me.opOP)
    Me.fmTipo.Controls.Add(Me.opOF)
    Me.fmTipo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.fmTipo.Location = New System.Drawing.Point(12, 3)
    Me.fmTipo.Name = "fmTipo"
    Me.fmTipo.Size = New System.Drawing.Size(352, 101)
    Me.fmTipo.TabIndex = 0
    Me.fmTipo.Text = "Tipo ordine"
    '
    'ckOpInterni
    '
    Me.ckOpInterni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpInterni.Location = New System.Drawing.Point(150, 49)
    Me.ckOpInterni.Name = "ckOpInterni"
    Me.ckOpInterni.NTSCheckValue = "S"
    Me.ckOpInterni.NTSUnCheckValue = "N"
    Me.ckOpInterni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpInterni.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpInterni.Properties.AutoHeight = False
    Me.ckOpInterni.Properties.Caption = "Solo Ordini &interni di produzione"
    Me.ckOpInterni.Size = New System.Drawing.Size(187, 19)
    Me.ckOpInterni.TabIndex = 3
    '
    'opIT
    '
    Me.opIT.Cursor = System.Windows.Forms.Cursors.Default
    Me.opIT.Location = New System.Drawing.Point(6, 74)
    Me.opIT.Name = "opIT"
    Me.opIT.NTSCheckValue = "S"
    Me.opIT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opIT.Properties.Appearance.Options.UseBackColor = True
    Me.opIT.Properties.AutoHeight = False
    Me.opIT.Properties.Caption = "&Impegni di trasferimento"
    Me.opIT.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opIT.Size = New System.Drawing.Size(140, 19)
    Me.opIT.TabIndex = 2
    '
    'opOP
    '
    Me.opOP.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOP.Location = New System.Drawing.Point(6, 49)
    Me.opOP.Name = "opOP"
    Me.opOP.NTSCheckValue = "S"
    Me.opOP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOP.Properties.Appearance.Options.UseBackColor = True
    Me.opOP.Properties.AutoHeight = False
    Me.opOP.Properties.Caption = "O&rdini di produzione"
    Me.opOP.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOP.Size = New System.Drawing.Size(123, 19)
    Me.opOP.TabIndex = 1
    '
    'opOF
    '
    Me.opOF.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOF.EditValue = True
    Me.opOF.Location = New System.Drawing.Point(6, 24)
    Me.opOF.Name = "opOF"
    Me.opOF.NTSCheckValue = "S"
    Me.opOF.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOF.Properties.Appearance.Options.UseBackColor = True
    Me.opOF.Properties.AutoHeight = False
    Me.opOF.Properties.Caption = "&Ordini a fornitori"
    Me.opOF.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOF.Size = New System.Drawing.Size(107, 19)
    Me.opOF.TabIndex = 0
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(162, 110)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(100, 20)
    Me.edConto.TabIndex = 1
    Me.edConto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edConto.TextInt = 0
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.EditValue = "0"
    Me.edCommeca.Location = New System.Drawing.Point(162, 136)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.AutoHeight = False
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(100, 20)
    Me.edCommeca.TabIndex = 2
    Me.edCommeca.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCommeca.TextInt = 0
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.EditValue = "0"
    Me.edMagaz.Location = New System.Drawing.Point(162, 162)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(100, 20)
    Me.edMagaz.TabIndex = 3
    Me.edMagaz.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edMagaz.TextInt = 0
    '
    'edDatconsDa
    '
    Me.edDatconsDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsDa.EditValue = "01/01/1900"
    Me.edDatconsDa.Location = New System.Drawing.Point(162, 188)
    Me.edDatconsDa.Name = "edDatconsDa"
    Me.edDatconsDa.NTSDbField = ""
    Me.edDatconsDa.NTSForzaVisZoom = False
    Me.edDatconsDa.NTSOldValue = ""
    Me.edDatconsDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsDa.Properties.AutoHeight = False
    Me.edDatconsDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsDa.Properties.MaxLength = 65536
    Me.edDatconsDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsDa.Size = New System.Drawing.Size(100, 20)
    Me.edDatconsDa.TabIndex = 4
    Me.edDatconsDa.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edDatordA
    '
    Me.edDatordA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordA.EditValue = "31/12/2099"
    Me.edDatordA.Location = New System.Drawing.Point(268, 214)
    Me.edDatordA.Name = "edDatordA"
    Me.edDatordA.NTSDbField = ""
    Me.edDatordA.NTSForzaVisZoom = False
    Me.edDatordA.NTSOldValue = ""
    Me.edDatordA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordA.Properties.AutoHeight = False
    Me.edDatordA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordA.Properties.MaxLength = 65536
    Me.edDatordA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordA.Size = New System.Drawing.Size(96, 20)
    Me.edDatordA.TabIndex = 7
    Me.edDatordA.TextData = New Date(2099, 12, 31, 0, 0, 0, 0)
    '
    'lbContoLabel
    '
    Me.lbContoLabel.AutoSize = True
    Me.lbContoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbContoLabel.Location = New System.Drawing.Point(15, 113)
    Me.lbContoLabel.Name = "lbContoLabel"
    Me.lbContoLabel.NTSDbField = ""
    Me.lbContoLabel.Size = New System.Drawing.Size(86, 13)
    Me.lbContoLabel.TabIndex = 8
    Me.lbContoLabel.Text = "Codice Fornitore"
    Me.lbContoLabel.Tooltip = ""
    Me.lbContoLabel.UseMnemonic = False
    '
    'lbCommecaLabel
    '
    Me.lbCommecaLabel.AutoSize = True
    Me.lbCommecaLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaLabel.Location = New System.Drawing.Point(15, 139)
    Me.lbCommecaLabel.Name = "lbCommecaLabel"
    Me.lbCommecaLabel.NTSDbField = ""
    Me.lbCommecaLabel.Size = New System.Drawing.Size(113, 13)
    Me.lbCommecaLabel.TabIndex = 9
    Me.lbCommecaLabel.Text = "Commessa (0 = tutte)"
    Me.lbCommecaLabel.Tooltip = ""
    Me.lbCommecaLabel.UseMnemonic = False
    '
    'lbMagazLabel
    '
    Me.lbMagazLabel.AutoSize = True
    Me.lbMagazLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbMagazLabel.Location = New System.Drawing.Point(15, 165)
    Me.lbMagazLabel.Name = "lbMagazLabel"
    Me.lbMagazLabel.NTSDbField = ""
    Me.lbMagazLabel.Size = New System.Drawing.Size(108, 13)
    Me.lbMagazLabel.TabIndex = 10
    Me.lbMagazLabel.Text = "Magazzino (0 = tutti)"
    Me.lbMagazLabel.Tooltip = ""
    Me.lbMagazLabel.UseMnemonic = False
    '
    'lbDatcons
    '
    Me.lbDatcons.AutoSize = True
    Me.lbDatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDatcons.Location = New System.Drawing.Point(15, 191)
    Me.lbDatcons.Name = "lbDatcons"
    Me.lbDatcons.NTSDbField = ""
    Me.lbDatcons.Size = New System.Drawing.Size(113, 13)
    Me.lbDatcons.TabIndex = 11
    Me.lbDatcons.Text = "Data consegna DA / A"
    Me.lbDatcons.Tooltip = ""
    Me.lbDatcons.UseMnemonic = False
    '
    'lbDatord
    '
    Me.lbDatord.AutoSize = True
    Me.lbDatord.BackColor = System.Drawing.Color.Transparent
    Me.lbDatord.Location = New System.Drawing.Point(15, 217)
    Me.lbDatord.Name = "lbDatord"
    Me.lbDatord.NTSDbField = ""
    Me.lbDatord.Size = New System.Drawing.Size(140, 13)
    Me.lbDatord.TabIndex = 12
    Me.lbDatord.Text = "Data massima ordine DA / A"
    Me.lbDatord.Tooltip = ""
    Me.lbDatord.UseMnemonic = False
    '
    'edDatconsA
    '
    Me.edDatconsA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsA.EditValue = "31/12/2099"
    Me.edDatconsA.Location = New System.Drawing.Point(268, 188)
    Me.edDatconsA.Name = "edDatconsA"
    Me.edDatconsA.NTSDbField = ""
    Me.edDatconsA.NTSForzaVisZoom = False
    Me.edDatconsA.NTSOldValue = ""
    Me.edDatconsA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsA.Properties.AutoHeight = False
    Me.edDatconsA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsA.Properties.MaxLength = 65536
    Me.edDatconsA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsA.Size = New System.Drawing.Size(96, 20)
    Me.edDatconsA.TabIndex = 13
    Me.edDatconsA.TextData = New Date(2099, 12, 31, 0, 0, 0, 0)
    '
    'edDatordDa
    '
    Me.edDatordDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordDa.EditValue = "01/01/1900"
    Me.edDatordDa.Location = New System.Drawing.Point(162, 214)
    Me.edDatordDa.Name = "edDatordDa"
    Me.edDatordDa.NTSDbField = ""
    Me.edDatordDa.NTSForzaVisZoom = False
    Me.edDatordDa.NTSOldValue = ""
    Me.edDatordDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordDa.Properties.AutoHeight = False
    Me.edDatordDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordDa.Properties.MaxLength = 65536
    Me.edDatordDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordDa.Size = New System.Drawing.Size(100, 20)
    Me.edDatordDa.TabIndex = 14
    Me.edDatordDa.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'fmStato
    '
    Me.fmStato.AllowDrop = True
    Me.fmStato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStato.Appearance.Options.UseBackColor = True
    Me.fmStato.Controls.Add(Me.NtsPanel1)
    Me.fmStato.Controls.Add(Me.ckConfermato)
    Me.fmStato.Controls.Add(Me.ckCongelato)
    Me.fmStato.Controls.Add(Me.ckEmOrdine)
    Me.fmStato.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStato.Location = New System.Drawing.Point(12, 241)
    Me.fmStato.Name = "fmStato"
    Me.fmStato.Size = New System.Drawing.Size(352, 127)
    Me.fmStato.TabIndex = 15
    Me.fmStato.Text = "Stato riga"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.ckGenerato)
    Me.NtsPanel1.Controls.Add(Me.ckEmRDO)
    Me.NtsPanel1.Controls.Add(Me.ckEmRDA)
    Me.NtsPanel1.Controls.Add(Me.ckAppRDA)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(6, 23)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(139, 99)
    Me.NtsPanel1.TabIndex = 11
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'ckGenerato
    '
    Me.ckGenerato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGenerato.EditValue = True
    Me.ckGenerato.Location = New System.Drawing.Point(-1, 0)
    Me.ckGenerato.Name = "ckGenerato"
    Me.ckGenerato.NTSCheckValue = "S"
    Me.ckGenerato.NTSUnCheckValue = "N"
    Me.ckGenerato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGenerato.Properties.Appearance.Options.UseBackColor = True
    Me.ckGenerato.Properties.AutoHeight = False
    Me.ckGenerato.Properties.Caption = "Generato"
    Me.ckGenerato.Size = New System.Drawing.Size(71, 19)
    Me.ckGenerato.TabIndex = 4
    '
    'ckEmRDO
    '
    Me.ckEmRDO.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEmRDO.EditValue = True
    Me.ckEmRDO.Location = New System.Drawing.Point(0, 75)
    Me.ckEmRDO.Name = "ckEmRDO"
    Me.ckEmRDO.NTSCheckValue = "S"
    Me.ckEmRDO.NTSUnCheckValue = "N"
    Me.ckEmRDO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEmRDO.Properties.Appearance.Options.UseBackColor = True
    Me.ckEmRDO.Properties.AutoHeight = False
    Me.ckEmRDO.Properties.Caption = "Emissione RDO"
    Me.ckEmRDO.Size = New System.Drawing.Size(105, 19)
    Me.ckEmRDO.TabIndex = 10
    '
    'ckEmRDA
    '
    Me.ckEmRDA.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckEmRDA.EditValue = True
    Me.ckEmRDA.Location = New System.Drawing.Point(0, 25)
    Me.ckEmRDA.Name = "ckEmRDA"
    Me.ckEmRDA.NTSCheckValue = "S"
    Me.ckEmRDA.NTSUnCheckValue = "N"
    Me.ckEmRDA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEmRDA.Properties.Appearance.Options.UseBackColor = True
    Me.ckEmRDA.Properties.AutoHeight = False
    Me.ckEmRDA.Properties.Caption = "Emissione RDA"
    Me.ckEmRDA.Size = New System.Drawing.Size(94, 19)
    Me.ckEmRDA.TabIndex = 5
    '
    'ckAppRDA
    '
    Me.ckAppRDA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAppRDA.EditValue = True
    Me.ckAppRDA.Location = New System.Drawing.Point(0, 50)
    Me.ckAppRDA.Name = "ckAppRDA"
    Me.ckAppRDA.NTSCheckValue = "S"
    Me.ckAppRDA.NTSUnCheckValue = "N"
    Me.ckAppRDA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAppRDA.Properties.Appearance.Options.UseBackColor = True
    Me.ckAppRDA.Properties.AutoHeight = False
    Me.ckAppRDA.Properties.Caption = "Approvazione RDA"
    Me.ckAppRDA.Size = New System.Drawing.Size(123, 19)
    Me.ckAppRDA.TabIndex = 6
    '
    'ckConfermato
    '
    Me.ckConfermato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConfermato.EditValue = True
    Me.ckConfermato.Location = New System.Drawing.Point(150, 23)
    Me.ckConfermato.Name = "ckConfermato"
    Me.ckConfermato.NTSCheckValue = "S"
    Me.ckConfermato.NTSUnCheckValue = "N"
    Me.ckConfermato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConfermato.Properties.Appearance.Options.UseBackColor = True
    Me.ckConfermato.Properties.AutoHeight = False
    Me.ckConfermato.Properties.Caption = "Confermato/app.RDO"
    Me.ckConfermato.Size = New System.Drawing.Size(132, 19)
    Me.ckConfermato.TabIndex = 7
    '
    'ckCongelato
    '
    Me.ckCongelato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCongelato.EditValue = True
    Me.ckCongelato.Location = New System.Drawing.Point(150, 48)
    Me.ckCongelato.Name = "ckCongelato"
    Me.ckCongelato.NTSCheckValue = "S"
    Me.ckCongelato.NTSUnCheckValue = "N"
    Me.ckCongelato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCongelato.Properties.Appearance.Options.UseBackColor = True
    Me.ckCongelato.Properties.AutoHeight = False
    Me.ckCongelato.Properties.Caption = "Congelato"
    Me.ckCongelato.Size = New System.Drawing.Size(77, 19)
    Me.ckCongelato.TabIndex = 9
    '
    'ckEmOrdine
    '
    Me.ckEmOrdine.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEmOrdine.EditValue = True
    Me.ckEmOrdine.Location = New System.Drawing.Point(150, 73)
    Me.ckEmOrdine.Name = "ckEmOrdine"
    Me.ckEmOrdine.NTSCheckValue = "S"
    Me.ckEmOrdine.NTSUnCheckValue = "N"
    Me.ckEmOrdine.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEmOrdine.Properties.Appearance.Options.UseBackColor = True
    Me.ckEmOrdine.Properties.AutoHeight = False
    Me.ckEmOrdine.Properties.Caption = "Emesso Ordine"
    Me.ckEmOrdine.Size = New System.Drawing.Size(100, 19)
    Me.ckEmOrdine.TabIndex = 8
    '
    'pnSx
    '
    Me.pnSx.AllowDrop = True
    Me.pnSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSx.Appearance.Options.UseBackColor = True
    Me.pnSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSx.Controls.Add(Me.fmOrdinamento)
    Me.pnSx.Controls.Add(Me.cmdEsci)
    Me.pnSx.Controls.Add(Me.cmdConferma)
    Me.pnSx.Controls.Add(Me.cmdSeleziona)
    Me.pnSx.Controls.Add(Me.lbMagaz)
    Me.pnSx.Controls.Add(Me.lbCommeca)
    Me.pnSx.Controls.Add(Me.lbConto)
    Me.pnSx.Controls.Add(Me.fmTipo)
    Me.pnSx.Controls.Add(Me.fmStato)
    Me.pnSx.Controls.Add(Me.edConto)
    Me.pnSx.Controls.Add(Me.edDatordDa)
    Me.pnSx.Controls.Add(Me.edCommeca)
    Me.pnSx.Controls.Add(Me.edDatconsA)
    Me.pnSx.Controls.Add(Me.edMagaz)
    Me.pnSx.Controls.Add(Me.lbDatord)
    Me.pnSx.Controls.Add(Me.edDatconsDa)
    Me.pnSx.Controls.Add(Me.lbDatcons)
    Me.pnSx.Controls.Add(Me.edDatordA)
    Me.pnSx.Controls.Add(Me.lbMagazLabel)
    Me.pnSx.Controls.Add(Me.lbContoLabel)
    Me.pnSx.Controls.Add(Me.lbCommecaLabel)
    Me.pnSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSx.Location = New System.Drawing.Point(0, 0)
    Me.pnSx.Name = "pnSx"
    Me.pnSx.NTSActiveTrasparency = True
    Me.pnSx.Size = New System.Drawing.Size(498, 492)
    Me.pnSx.TabIndex = 16
    Me.pnSx.Text = "NtsPanel1"
    '
    'fmOrdinamento
    '
    Me.fmOrdinamento.AllowDrop = True
    Me.fmOrdinamento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrdinamento.Appearance.Options.UseBackColor = True
    Me.fmOrdinamento.Controls.Add(Me.opCentro)
    Me.fmOrdinamento.Controls.Add(Me.opLineaFam)
    Me.fmOrdinamento.Controls.Add(Me.opMagaz)
    Me.fmOrdinamento.Controls.Add(Me.opDatcons)
    Me.fmOrdinamento.Controls.Add(Me.opDesForn)
    Me.fmOrdinamento.Controls.Add(Me.opCodForn)
    Me.fmOrdinamento.Controls.Add(Me.opDesArtFase)
    Me.fmOrdinamento.Controls.Add(Me.opCodArtFase)
    Me.fmOrdinamento.Controls.Add(Me.opProg)
    Me.fmOrdinamento.Location = New System.Drawing.Point(12, 374)
    Me.fmOrdinamento.Name = "fmOrdinamento"
    Me.fmOrdinamento.Size = New System.Drawing.Size(352, 100)
    Me.fmOrdinamento.TabIndex = 22
    Me.fmOrdinamento.Text = "Ordinamento"
    '
    'opCentro
    '
    Me.opCentro.Location = New System.Drawing.Point(244, 73)
    Me.opCentro.Name = "opCentro"
    Me.opCentro.NTSCheckValue = "S"
    Me.opCentro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCentro.Properties.Appearance.Options.UseBackColor = True
    Me.opCentro.Properties.AutoHeight = False
    Me.opCentro.Properties.Caption = "Centro"
    Me.opCentro.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCentro.Size = New System.Drawing.Size(64, 19)
    Me.opCentro.TabIndex = 8
    '
    'opLineaFam
    '
    Me.opLineaFam.Location = New System.Drawing.Point(244, 48)
    Me.opLineaFam.Name = "opLineaFam"
    Me.opLineaFam.NTSCheckValue = "S"
    Me.opLineaFam.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opLineaFam.Properties.Appearance.Options.UseBackColor = True
    Me.opLineaFam.Properties.AutoHeight = False
    Me.opLineaFam.Properties.Caption = "Linea/famiglia"
    Me.opLineaFam.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opLineaFam.Size = New System.Drawing.Size(93, 19)
    Me.opLineaFam.TabIndex = 7
    '
    'opMagaz
    '
    Me.opMagaz.Location = New System.Drawing.Point(244, 23)
    Me.opMagaz.Name = "opMagaz"
    Me.opMagaz.NTSCheckValue = "S"
    Me.opMagaz.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagaz.Properties.Appearance.Options.UseBackColor = True
    Me.opMagaz.Properties.AutoHeight = False
    Me.opMagaz.Properties.Caption = "Magazzino"
    Me.opMagaz.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagaz.Size = New System.Drawing.Size(79, 19)
    Me.opMagaz.TabIndex = 6
    '
    'opDatcons
    '
    Me.opDatcons.Location = New System.Drawing.Point(134, 73)
    Me.opDatcons.Name = "opDatcons"
    Me.opDatcons.NTSCheckValue = "S"
    Me.opDatcons.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDatcons.Properties.Appearance.Options.UseBackColor = True
    Me.opDatcons.Properties.AutoHeight = False
    Me.opDatcons.Properties.Caption = "Data consegna"
    Me.opDatcons.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDatcons.Size = New System.Drawing.Size(102, 19)
    Me.opDatcons.TabIndex = 5
    '
    'opDesForn
    '
    Me.opDesForn.Location = New System.Drawing.Point(134, 48)
    Me.opDesForn.Name = "opDesForn"
    Me.opDesForn.NTSCheckValue = "S"
    Me.opDesForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDesForn.Properties.Appearance.Options.UseBackColor = True
    Me.opDesForn.Properties.AutoHeight = False
    Me.opDesForn.Properties.Caption = "Descr. fornitore"
    Me.opDesForn.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDesForn.Size = New System.Drawing.Size(102, 19)
    Me.opDesForn.TabIndex = 4
    '
    'opCodForn
    '
    Me.opCodForn.Location = New System.Drawing.Point(134, 23)
    Me.opCodForn.Name = "opCodForn"
    Me.opCodForn.NTSCheckValue = "S"
    Me.opCodForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodForn.Properties.Appearance.Options.UseBackColor = True
    Me.opCodForn.Properties.AutoHeight = False
    Me.opCodForn.Properties.Caption = "Cod. Fornitore"
    Me.opCodForn.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodForn.Size = New System.Drawing.Size(93, 19)
    Me.opCodForn.TabIndex = 3
    '
    'opDesArtFase
    '
    Me.opDesArtFase.Location = New System.Drawing.Point(6, 73)
    Me.opDesArtFase.Name = "opDesArtFase"
    Me.opDesArtFase.NTSCheckValue = "S"
    Me.opDesArtFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDesArtFase.Properties.Appearance.Options.UseBackColor = True
    Me.opDesArtFase.Properties.AutoHeight = False
    Me.opDesArtFase.Properties.Caption = "Descr. articolo fase"
    Me.opDesArtFase.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDesArtFase.Size = New System.Drawing.Size(123, 19)
    Me.opDesArtFase.TabIndex = 2
    '
    'opCodArtFase
    '
    Me.opCodArtFase.Location = New System.Drawing.Point(6, 48)
    Me.opCodArtFase.Name = "opCodArtFase"
    Me.opCodArtFase.NTSCheckValue = "S"
    Me.opCodArtFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodArtFase.Properties.Appearance.Options.UseBackColor = True
    Me.opCodArtFase.Properties.AutoHeight = False
    Me.opCodArtFase.Properties.Caption = "Cod. articolo fase"
    Me.opCodArtFase.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodArtFase.Size = New System.Drawing.Size(110, 19)
    Me.opCodArtFase.TabIndex = 1
    '
    'opProg
    '
    Me.opProg.EditValue = True
    Me.opProg.Location = New System.Drawing.Point(6, 23)
    Me.opProg.Name = "opProg"
    Me.opProg.NTSCheckValue = "S"
    Me.opProg.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opProg.Properties.Appearance.Options.UseBackColor = True
    Me.opProg.Properties.AutoHeight = False
    Me.opProg.Properties.Caption = "Progressivo"
    Me.opProg.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opProg.Size = New System.Drawing.Size(83, 19)
    Me.opProg.TabIndex = 0
    '
    'cmdEsci
    '
    Me.cmdEsci.ImagePath = ""
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(370, 346)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(119, 26)
    Me.cmdEsci.TabIndex = 21
    Me.cmdEsci.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(370, 314)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(119, 26)
    Me.cmdConferma.TabIndex = 20
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(370, 241)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(119, 26)
    Me.cmdSeleziona.TabIndex = 19
    Me.cmdSeleziona.Text = "&Seleziona Articoli"
    '
    'lbMagaz
    '
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMagaz.Location = New System.Drawing.Point(268, 162)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(221, 20)
    Me.lbMagaz.TabIndex = 18
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommeca.Location = New System.Drawing.Point(268, 136)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(221, 20)
    Me.lbCommeca.TabIndex = 17
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(268, 110)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(221, 20)
    Me.lbConto.TabIndex = 16
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'FRMORSEOL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(499, 492)
    Me.Controls.Add(Me.pnSx)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORSEOL"
    Me.Text = "SELEZIONE PROPOSTE D'ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipo.ResumeLayout(False)
    Me.fmTipo.PerformLayout()
    CType(Me.ckOpInterni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opIT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStato.ResumeLayout(False)
    Me.fmStato.PerformLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.ckGenerato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEmRDO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEmRDA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAppRDA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConfermato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCongelato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEmOrdine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSx.ResumeLayout(False)
    Me.pnSx.PerformLayout()
    CType(Me.fmOrdinamento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrdinamento.ResumeLayout(False)
    Me.fmOrdinamento.PerformLayout()
    CType(Me.opCentro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opLineaFam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDesForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDesArtFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodArtFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opProg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRMORSEOL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Dim strTmp As String
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) Then ckConfermato.Text = oApp.Tr(Me, 128849573470982000, "Approvato RDO")

      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) And oCallParams.strPar2 <> "BNORGSOL:SELDOCU" Then

        ckGenerato.Checked = False
        ckAppRDA.Checked = False
        ckCongelato.Checked = False
        ckEmOrdine.Checked = False
        ckEmRDA.Checked = False
        ckEmRDO.Checked = False

        ckGenerato.Enabled = False
        ckAppRDA.Enabled = False
        ckCongelato.Enabled = False
        ckEmOrdine.Enabled = False
        ckEmRDA.Enabled = False
        ckEmRDO.Enabled = False
        ckConfermato.Enabled = False
      Else
        ckOpInterni.Checked = False
        ckOpInterni.Enabled = False
        ckOpInterni.Visible = False
      End If

      opOP_CheckedChanged(opOP, Nothing)

      If oCallParams.strPar2 = "BNORGSOL:SELDOCU" Then
        strTmp = oMenu.GetSettingBus("BSORSEOL", "RECENT", ".", "Ordinamento", "0", ".", "0")

        If strTmp = "0" Then
          opProg.Checked = True
        ElseIf strTmp = "1" Then
          opCodArtFase.Checked = True
        ElseIf strTmp = "2" Then
          opDesArtFase.Checked = True
        ElseIf strTmp = "3" Then
          opCodForn.Checked = True
        ElseIf strTmp = "4" Then
          opDesForn.Checked = True
        ElseIf strTmp = "5" Then
          opDatcons.Checked = True
        ElseIf strTmp = "6" Then
          opMagaz.Checked = True
        ElseIf strTmp = "7" Then
          opLineaFam.Checked = True
        ElseIf strTmp = "8" Then
          opCentro.Checked = True
        End If
      Else
        fmOrdinamento.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "CommandButton"
  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      oCallParams.strParam = ""     'ho annullato
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim bGsol As Boolean
    Try
      If oCallParams.strPar2 = "BNORGSOL:SELDOCU" Then
        bGsol = True
      Else
        bGsol = False
      End If

      'tipo documento da generare
      oCallParams.strParam = IIf(opOF.Checked, "O", IIf(opOP.Checked, "H", "X")).ToString
      oCallParams.strPar1 = strWhereArtico
      oCallParams.bPar1 = ckOpInterni.Checked
      oCallParams.dPar1 = NTSCInt(edConto.Text)
      oCallParams.dPar2 = NTSCInt(edCommeca.Text)
      oCallParams.dPar3 = NTSCInt(edMagaz.Text)
      oCallParams.strPar2 = NTSCDate(edDatconsDa.Text).ToShortDateString
      oCallParams.strPar3 = NTSCDate(edDatconsA.Text).ToShortDateString
      oCallParams.strPar4 = NTSCDate(edDatordDa.Text).ToShortDateString
      oCallParams.strPar5 = NTSCDate(edDatordA.Text).ToShortDateString
      oCallParams.strParam += IIf(ckGenerato.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckConfermato.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckEmRDA.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckAppRDA.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckEmRDO.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckCongelato.Checked, "S", "N").ToString
      oCallParams.strParam += IIf(ckEmOrdine.Checked, "S", "N").ToString

      If bGsol Then
        If opProg.Checked Then
          oCallParams.strParam += "0"
        ElseIf opCodArtFase.Checked Then
          oCallParams.strParam += "1"
        ElseIf opDesArtFase.Checked Then
          oCallParams.strParam += "2"
        ElseIf opCodForn.Checked Then
          oCallParams.strParam += "3"
        ElseIf opDesForn.Checked Then
          oCallParams.strParam += "4"
        ElseIf opDatcons.Checked Then
          oCallParams.strParam += "5"
        ElseIf opMagaz.Checked Then
          oCallParams.strParam += "6"
        ElseIf opLineaFam.Checked Then
          oCallParams.strParam += "7"
        ElseIf opCentro.Checked Then
          oCallParams.strParam += "8"
        End If

        oMenu.SaveSettingBus("BSORSEOL", "RECENT", ".", "Ordinamento", Microsoft.VisualBasic.Right(oCallParams.strParam, 1), " ", "NS.", "...", "...")
      End If

      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
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
#End Region

  Public Overridable Sub edMagaz_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMagaz.Validated
    Dim strTmp As String = ""
    Try
      If oCleSeol Is Nothing Then Return

      If Not oCleSeol.edMagaz_Validated(NTSCInt(edMagaz.Text), strTmp) Then
        edMagaz.Text = NTSCStr(edMagaz.OldEditValue)
      Else
        lbMagaz.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCommeca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommeca.Validated
    Dim strTmp As String = ""
    Try
      If oCleSeol Is Nothing Then Return

      If Not oCleSeol.edCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
        edCommeca.Text = NTSCStr(edCommeca.OldEditValue)
      Else
        lbCommeca.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strTmp As String = ""
    Try
      If oCleSeol Is Nothing Then Return

      If Not oCleSeol.edConto_Validated(NTSCInt(edConto.Text), strTmp) Then
        edConto.Text = NTSCStr(edConto.OldEditValue)
      Else
        lbConto.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opOP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opOP.CheckedChanged
    Try
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) And oCallParams.strPar2 <> "BNORGSOL:SELDOCU" Then
        If opOP.Checked Then
          GctlSetVisEnab(ckOpInterni, False)
        Else
          ckOpInterni.Enabled = False
          ckOpInterni.Checked = False
        End If
      Else
        ckOpInterni.Checked = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opIT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opIT.CheckedChanged
    Try
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) And oCallParams.strPar2 <> "BNORGSOL:SELDOCU" Then
        If opIT.Checked Then
          GctlSetVisEnab(ckGenerato, False)
          GctlSetVisEnab(ckAppRDA, False)
          GctlSetVisEnab(ckCongelato, False)
          GctlSetVisEnab(ckEmOrdine, False)
          GctlSetVisEnab(ckEmRDA, False)
          GctlSetVisEnab(ckEmRDO, False)
          GctlSetVisEnab(ckConfermato, False)

          ckGenerato.Checked = True
          ckAppRDA.Checked = True
          ckCongelato.Checked = True
          ckEmOrdine.Checked = True
          ckEmRDA.Checked = True
          ckEmRDO.Checked = True
        Else
          ckGenerato.Checked = False
          ckAppRDA.Checked = False
          ckCongelato.Checked = False
          ckEmOrdine.Checked = False
          ckEmRDA.Checked = False
          ckEmRDO.Checked = False

          ckGenerato.Enabled = False
          ckAppRDA.Enabled = False
          ckCongelato.Enabled = False
          ckEmOrdine.Enabled = False
          ckEmRDA.Enabled = False
          ckEmRDO.Enabled = False
          ckConfermato.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckOpInterni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpInterni.CheckedChanged
    Try
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) And oCallParams.strPar2 <> "BNORGSOL:SELDOCU" Then
        If ckOpInterni.Checked And opOP.Checked Then
          GctlSetVisEnab(ckGenerato, False)
          GctlSetVisEnab(ckAppRDA, False)
          GctlSetVisEnab(ckCongelato, False)
          GctlSetVisEnab(ckEmOrdine, False)
          GctlSetVisEnab(ckEmRDA, False)
          GctlSetVisEnab(ckEmRDO, False)
          GctlSetVisEnab(ckConfermato, False)

          ckGenerato.Checked = True
          ckAppRDA.Checked = True
          ckCongelato.Checked = True
          ckEmOrdine.Checked = True
          ckEmRDA.Checked = True
          ckEmRDO.Checked = True
        Else
          ckGenerato.Checked = False
          ckAppRDA.Checked = False
          ckCongelato.Checked = False
          ckEmOrdine.Checked = False
          ckEmRDA.Checked = False
          ckEmRDO.Checked = False

          ckGenerato.Enabled = False
          ckAppRDA.Enabled = False
          ckCongelato.Enabled = False
          ckEmOrdine.Enabled = False
          ckEmRDA.Enabled = False
          ckEmRDO.Enabled = False
          ckConfermato.Enabled = False
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edConto.ContainsFocus Then strTabName = "ANAGRA_FOR"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
End Class
