Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESEP1
  Public strTipork As String
  Public strAnno As String
  Public oPar As CLE__CLDP
  Public oClePack As CLEVEPACK
  Public dsSepa As DataSet
  Public dcSepa As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

#Region "Variabili"
  Public WithEvents cmdSeleziona As NTSButton
  Public WithEvents cmdAnnulla As NTSButton
  Public WithEvents pnfiltri As NTSInformatica.NTSPanel
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edDataA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edContoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnCommandbutton As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grSepa As NTSInformatica.NTSGrid
  Public WithEvents grvSepa As NTSInformatica.NTSGridView
  Public WithEvents tp_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tp_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tp_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tp_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tp_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tp_totcoll As NTSInformatica.NTSGridColumn
  Public WithEvents tp_peso As NTSInformatica.NTSGridColumn
  Public WithEvents tp_pesonetto As NTSInformatica.NTSGridColumn
  Public WithEvents tp_note As NTSInformatica.NTSGridColumn
  Public WithEvents edRiferim As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbRiferim As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As System.Windows.Forms.Label
#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      oPar = Param
      strTipork = Param.strPar1
      strAnno = Param.strPar3
      'edSerie.Text = Param.strPar2

      InitControls()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Sub InitEntity(ByRef clePack As CLEVEPACK)
    oClePack = clePack
    AddHandler oClePack.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edContoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055328591746720, "A codice cliente/fornitore"), tabanagrac)
      edContoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055328627529115, "Da codice cliente/fornitore"), tabanagrac)
      edDataA.NTSSetParam(oMenu, oApp.Tr(Me, 129055328646123460, "A data documento"), False)
      edDataDa.NTSSetParam(oMenu, oApp.Tr(Me, 129055328664874060, "Da data documento"), False)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 129055328693937490, "Serie documento"), CLN__STD.SerieMaxLen, True)
      edRiferim.NTSSetParam(oMenu, oApp.Tr(Me, 129055328709562990, "Riferimenti"), 255, True)
      grvSepa.NTSSetParam(oMenu, oApp.Tr(Me, 129055328735032555, "Selezione packing list"))
      tp_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328770033675, "Anno"), "0", 4, 0, 9999)
      tp_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055328799565870, "Serie"), CLN__STD.SerieMaxLen, True)
      tp_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328827848025, "Numero"), "0", 9, 0, 999999999)
      tp_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129055328855817670, "Data Doc."), True)
      tp_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328889881260, "Conto"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055328909413135, "Descr. conto"), 0, True)
      tp_totcoll.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328927694970, "Tot. Colli"), "0", 9, 0, 999999999)
      tp_peso.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328949883180, "Peso lordo"), oApp.FormatQta, 15)
      tp_pesonetto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055328981602945, "Peso netto"), oApp.FormatQta, 15)
      tp_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055328999884780, "Note"), 0, True)

      grvSepa.NTSSetParam(oMenu, "")

      edContoDa.NTSSetParamZoom("ZOOMANAGRAC")
      edContoA.NTSSetParamZoom("ZOOMANAGRAC")

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
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnfiltri = New NTSInformatica.NTSPanel
    Me.lbAnno = New System.Windows.Forms.Label
    Me.edRiferim = New NTSInformatica.NTSTextBoxStr
    Me.lbRiferim = New NTSInformatica.NTSLabel
    Me.edContoA = New NTSInformatica.NTSTextBoxNum
    Me.edContoDa = New NTSInformatica.NTSTextBoxNum
    Me.edDataA = New NTSInformatica.NTSTextBoxData
    Me.edDataDa = New NTSInformatica.NTSTextBoxData
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.pnCommandbutton = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grSepa = New NTSInformatica.NTSGrid
    Me.grvSepa = New NTSInformatica.NTSGridView
    Me.tp_anno = New NTSInformatica.NTSGridColumn
    Me.tp_serie = New NTSInformatica.NTSGridColumn
    Me.tp_numdoc = New NTSInformatica.NTSGridColumn
    Me.tp_datdoc = New NTSInformatica.NTSGridColumn
    Me.tp_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tp_totcoll = New NTSInformatica.NTSGridColumn
    Me.tp_peso = New NTSInformatica.NTSGridColumn
    Me.tp_pesonetto = New NTSInformatica.NTSGridColumn
    Me.tp_note = New NTSInformatica.NTSGridColumn
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnfiltri.SuspendLayout()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommandbutton.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grSepa, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSepa, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(13, 27)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(79, 22)
    Me.cmdSeleziona.TabIndex = 2
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(13, 58)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnfiltri
    '
    Me.pnfiltri.AllowDrop = True
    Me.pnfiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnfiltri.Appearance.Options.UseBackColor = True
    Me.pnfiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnfiltri.Controls.Add(Me.lbAnno)
    Me.pnfiltri.Controls.Add(Me.edRiferim)
    Me.pnfiltri.Controls.Add(Me.lbRiferim)
    Me.pnfiltri.Controls.Add(Me.edContoA)
    Me.pnfiltri.Controls.Add(Me.edContoDa)
    Me.pnfiltri.Controls.Add(Me.edDataA)
    Me.pnfiltri.Controls.Add(Me.edDataDa)
    Me.pnfiltri.Controls.Add(Me.lbAdata)
    Me.pnfiltri.Controls.Add(Me.lbDadata)
    Me.pnfiltri.Controls.Add(Me.lbAconto)
    Me.pnfiltri.Controls.Add(Me.lbDaconto)
    Me.pnfiltri.Controls.Add(Me.edSerie)
    Me.pnfiltri.Controls.Add(Me.lbSerie)
    Me.pnfiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnfiltri.Location = New System.Drawing.Point(3, 0)
    Me.pnfiltri.Name = "pnfiltri"
    Me.pnfiltri.NTSActiveTrasparency = True
    Me.pnfiltri.Size = New System.Drawing.Size(505, 107)
    Me.pnfiltri.TabIndex = 4
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.Location = New System.Drawing.Point(344, 12)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.Size = New System.Drawing.Size(36, 13)
    Me.lbAnno.TabIndex = 54
    Me.lbAnno.Text = "Anno:"
    '
    'edRiferim
    '
    Me.edRiferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRiferim.Location = New System.Drawing.Point(94, 82)
    Me.edRiferim.Name = "edRiferim"
    Me.edRiferim.NTSDbField = ""
    Me.edRiferim.NTSForzaVisZoom = False
    Me.edRiferim.NTSOldValue = ""
    Me.edRiferim.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRiferim.Properties.Appearance.Options.UseBackColor = True
    Me.edRiferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRiferim.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRiferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRiferim.Properties.MaxLength = 65536
    Me.edRiferim.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRiferim.Size = New System.Drawing.Size(241, 20)
    Me.edRiferim.TabIndex = 50
    '
    'lbRiferim
    '
    Me.lbRiferim.AutoSize = True
    Me.lbRiferim.BackColor = System.Drawing.Color.Transparent
    Me.lbRiferim.Location = New System.Drawing.Point(12, 85)
    Me.lbRiferim.Name = "lbRiferim"
    Me.lbRiferim.NTSDbField = ""
    Me.lbRiferim.Size = New System.Drawing.Size(84, 13)
    Me.lbRiferim.TabIndex = 49
    Me.lbRiferim.Text = "Riferimenti (like)"
    Me.lbRiferim.Tooltip = ""
    Me.lbRiferim.UseMnemonic = False
    '
    'edContoA
    '
    Me.edContoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoA.EditValue = "999999999"
    Me.edContoA.Location = New System.Drawing.Point(251, 9)
    Me.edContoA.Name = "edContoA"
    Me.edContoA.NTSDbField = ""
    Me.edContoA.NTSFormat = "0"
    Me.edContoA.NTSForzaVisZoom = False
    Me.edContoA.NTSOldValue = "999999999"
    Me.edContoA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoA.Properties.Appearance.Options.UseBackColor = True
    Me.edContoA.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoA.Properties.MaxLength = 65536
    Me.edContoA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoA.Size = New System.Drawing.Size(84, 20)
    Me.edContoA.TabIndex = 46
    '
    'edContoDa
    '
    Me.edContoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoDa.EditValue = "0"
    Me.edContoDa.Location = New System.Drawing.Point(93, 9)
    Me.edContoDa.Name = "edContoDa"
    Me.edContoDa.NTSDbField = ""
    Me.edContoDa.NTSFormat = "0"
    Me.edContoDa.NTSForzaVisZoom = False
    Me.edContoDa.NTSOldValue = ""
    Me.edContoDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoDa.Properties.Appearance.Options.UseBackColor = True
    Me.edContoDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoDa.Properties.MaxLength = 65536
    Me.edContoDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoDa.Size = New System.Drawing.Size(84, 20)
    Me.edContoDa.TabIndex = 45
    '
    'edDataA
    '
    Me.edDataA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataA.Location = New System.Drawing.Point(251, 34)
    Me.edDataA.Name = "edDataA"
    Me.edDataA.NTSDbField = ""
    Me.edDataA.NTSForzaVisZoom = False
    Me.edDataA.NTSOldValue = ""
    Me.edDataA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataA.Properties.Appearance.Options.UseBackColor = True
    Me.edDataA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataA.Properties.MaxLength = 65536
    Me.edDataA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataA.Size = New System.Drawing.Size(84, 20)
    Me.edDataA.TabIndex = 14
    '
    'edDataDa
    '
    Me.edDataDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDa.Location = New System.Drawing.Point(93, 34)
    Me.edDataDa.Name = "edDataDa"
    Me.edDataDa.NTSDbField = ""
    Me.edDataDa.NTSForzaVisZoom = False
    Me.edDataDa.NTSOldValue = ""
    Me.edDataDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDataDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataDa.Properties.MaxLength = 65536
    Me.edDataDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataDa.Size = New System.Drawing.Size(84, 20)
    Me.edDataDa.TabIndex = 13
    '
    'lbAdata
    '
    Me.lbAdata.AutoSize = True
    Me.lbAdata.BackColor = System.Drawing.Color.Transparent
    Me.lbAdata.Location = New System.Drawing.Point(183, 37)
    Me.lbAdata.Name = "lbAdata"
    Me.lbAdata.NTSDbField = ""
    Me.lbAdata.Size = New System.Drawing.Size(39, 13)
    Me.lbAdata.TabIndex = 12
    Me.lbAdata.Text = "A data"
    Me.lbAdata.Tooltip = ""
    Me.lbAdata.UseMnemonic = False
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(12, 37)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(45, 13)
    Me.lbDadata.TabIndex = 9
    Me.lbDadata.Text = "Da data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'lbAconto
    '
    Me.lbAconto.AutoSize = True
    Me.lbAconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAconto.Location = New System.Drawing.Point(183, 12)
    Me.lbAconto.Name = "lbAconto"
    Me.lbAconto.NTSDbField = ""
    Me.lbAconto.Size = New System.Drawing.Size(44, 13)
    Me.lbAconto.TabIndex = 8
    Me.lbAconto.Text = "A conto"
    Me.lbAconto.Tooltip = ""
    Me.lbAconto.UseMnemonic = False
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(12, 12)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(50, 13)
    Me.lbDaconto.TabIndex = 7
    Me.lbDaconto.Text = "Da conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(457, 9)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerie.Properties.Appearance.Options.UseBackColor = True
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 6
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(424, 12)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 5
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'pnCommandbutton
    '
    Me.pnCommandbutton.AllowDrop = True
    Me.pnCommandbutton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommandbutton.Appearance.Options.UseBackColor = True
    Me.pnCommandbutton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommandbutton.Controls.Add(Me.cmdRicerca)
    Me.pnCommandbutton.Controls.Add(Me.cmdSeleziona)
    Me.pnCommandbutton.Controls.Add(Me.cmdAnnulla)
    Me.pnCommandbutton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommandbutton.Location = New System.Drawing.Point(510, 4)
    Me.pnCommandbutton.Name = "pnCommandbutton"
    Me.pnCommandbutton.NTSActiveTrasparency = True
    Me.pnCommandbutton.Size = New System.Drawing.Size(101, 103)
    Me.pnCommandbutton.TabIndex = 49
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(13, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(79, 22)
    Me.cmdRicerca.TabIndex = 50
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.pnfiltri)
    Me.pnTop.Controls.Add(Me.pnCommandbutton)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(625, 113)
    Me.pnTop.TabIndex = 50
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grSepa)
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 113)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(625, 206)
    Me.pnGrid.TabIndex = 51
    '
    'grSepa
    '
    Me.grSepa.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grSepa.EmbeddedNavigator.Name = ""
    Me.grSepa.Location = New System.Drawing.Point(0, 0)
    Me.grSepa.MainView = Me.grvSepa
    Me.grSepa.Name = "grSepa"
    Me.grSepa.Size = New System.Drawing.Size(625, 206)
    Me.grSepa.TabIndex = 6
    Me.grSepa.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSepa})
    '
    'grvSepa
    '
    Me.grvSepa.ActiveFilterEnabled = False
    Me.grvSepa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tp_anno, Me.tp_serie, Me.tp_numdoc, Me.tp_datdoc, Me.tp_conto, Me.xx_conto, Me.tp_totcoll, Me.tp_peso, Me.tp_pesonetto, Me.tp_note})
    Me.grvSepa.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSepa.Enabled = False
    Me.grvSepa.GridControl = Me.grSepa
    Me.grvSepa.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvSepa.MinRowHeight = 14
    Me.grvSepa.Name = "grvSepa"
    Me.grvSepa.NTSAllowDelete = False
    Me.grvSepa.NTSAllowInsert = False
    Me.grvSepa.NTSAllowUpdate = False
    Me.grvSepa.NTSMenuContext = Nothing
    Me.grvSepa.OptionsBehavior.Editable = False
    Me.grvSepa.OptionsCustomization.AllowRowSizing = True
    Me.grvSepa.OptionsFilter.AllowFilterEditor = False
    Me.grvSepa.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSepa.OptionsNavigation.UseTabKey = False
    Me.grvSepa.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSepa.OptionsView.ColumnAutoWidth = False
    Me.grvSepa.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSepa.OptionsView.ShowGroupPanel = False
    Me.grvSepa.RowHeight = 16
    '
    'tp_anno
    '
    Me.tp_anno.AppearanceCell.Options.UseBackColor = True
    Me.tp_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tp_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_anno.Caption = "Anno"
    Me.tp_anno.Enabled = True
    Me.tp_anno.FieldName = "tp_anno"
    Me.tp_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_anno.Name = "tp_anno"
    Me.tp_anno.NTSRepositoryComboBox = Nothing
    Me.tp_anno.NTSRepositoryItemCheck = Nothing
    Me.tp_anno.NTSRepositoryItemMemo = Nothing
    Me.tp_anno.NTSRepositoryItemText = Nothing
    Me.tp_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_anno.Visible = True
    Me.tp_anno.VisibleIndex = 0
    Me.tp_anno.Width = 58
    '
    'tp_serie
    '
    Me.tp_serie.AppearanceCell.Options.UseBackColor = True
    Me.tp_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tp_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_serie.Caption = "Serie"
    Me.tp_serie.Enabled = True
    Me.tp_serie.FieldName = "tp_serie"
    Me.tp_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_serie.Name = "tp_serie"
    Me.tp_serie.NTSRepositoryComboBox = Nothing
    Me.tp_serie.NTSRepositoryItemCheck = Nothing
    Me.tp_serie.NTSRepositoryItemMemo = Nothing
    Me.tp_serie.NTSRepositoryItemText = Nothing
    Me.tp_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_serie.Visible = True
    Me.tp_serie.VisibleIndex = 1
    Me.tp_serie.Width = 46
    '
    'tp_numdoc
    '
    Me.tp_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tp_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tp_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_numdoc.Caption = "Numero"
    Me.tp_numdoc.Enabled = True
    Me.tp_numdoc.FieldName = "tp_numdoc"
    Me.tp_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_numdoc.Name = "tp_numdoc"
    Me.tp_numdoc.NTSRepositoryComboBox = Nothing
    Me.tp_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tp_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tp_numdoc.NTSRepositoryItemText = Nothing
    Me.tp_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_numdoc.Visible = True
    Me.tp_numdoc.VisibleIndex = 2
    Me.tp_numdoc.Width = 91
    '
    'tp_datdoc
    '
    Me.tp_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tp_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tp_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_datdoc.Caption = "Data Doc."
    Me.tp_datdoc.Enabled = True
    Me.tp_datdoc.FieldName = "tp_datdoc"
    Me.tp_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_datdoc.Name = "tp_datdoc"
    Me.tp_datdoc.NTSRepositoryComboBox = Nothing
    Me.tp_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tp_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tp_datdoc.NTSRepositoryItemText = Nothing
    Me.tp_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_datdoc.Visible = True
    Me.tp_datdoc.VisibleIndex = 5
    '
    'tp_conto
    '
    Me.tp_conto.AppearanceCell.Options.UseBackColor = True
    Me.tp_conto.AppearanceCell.Options.UseTextOptions = True
    Me.tp_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_conto.Caption = "Conto"
    Me.tp_conto.Enabled = True
    Me.tp_conto.FieldName = "tp_conto"
    Me.tp_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_conto.Name = "tp_conto"
    Me.tp_conto.NTSRepositoryComboBox = Nothing
    Me.tp_conto.NTSRepositoryItemCheck = Nothing
    Me.tp_conto.NTSRepositoryItemMemo = Nothing
    Me.tp_conto.NTSRepositoryItemText = Nothing
    Me.tp_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_conto.Visible = True
    Me.tp_conto.VisibleIndex = 3
    Me.tp_conto.Width = 90
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. conto"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 4
    Me.xx_conto.Width = 112
    '
    'tp_totcoll
    '
    Me.tp_totcoll.AppearanceCell.Options.UseBackColor = True
    Me.tp_totcoll.AppearanceCell.Options.UseTextOptions = True
    Me.tp_totcoll.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_totcoll.Caption = "Tot. Colli"
    Me.tp_totcoll.Enabled = True
    Me.tp_totcoll.FieldName = "tp_totcoll"
    Me.tp_totcoll.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_totcoll.Name = "tp_totcoll"
    Me.tp_totcoll.NTSRepositoryComboBox = Nothing
    Me.tp_totcoll.NTSRepositoryItemCheck = Nothing
    Me.tp_totcoll.NTSRepositoryItemMemo = Nothing
    Me.tp_totcoll.NTSRepositoryItemText = Nothing
    Me.tp_totcoll.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_totcoll.Visible = True
    Me.tp_totcoll.VisibleIndex = 6
    '
    'tp_peso
    '
    Me.tp_peso.AppearanceCell.Options.UseBackColor = True
    Me.tp_peso.AppearanceCell.Options.UseTextOptions = True
    Me.tp_peso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_peso.Caption = "Peso lordo"
    Me.tp_peso.Enabled = True
    Me.tp_peso.FieldName = "tp_peso"
    Me.tp_peso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_peso.Name = "tp_peso"
    Me.tp_peso.NTSRepositoryComboBox = Nothing
    Me.tp_peso.NTSRepositoryItemCheck = Nothing
    Me.tp_peso.NTSRepositoryItemMemo = Nothing
    Me.tp_peso.NTSRepositoryItemText = Nothing
    Me.tp_peso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_peso.Visible = True
    Me.tp_peso.VisibleIndex = 7
    '
    'tp_pesonetto
    '
    Me.tp_pesonetto.AppearanceCell.Options.UseBackColor = True
    Me.tp_pesonetto.AppearanceCell.Options.UseTextOptions = True
    Me.tp_pesonetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_pesonetto.Caption = "Peso netto"
    Me.tp_pesonetto.Enabled = True
    Me.tp_pesonetto.FieldName = "tp_pesonetto"
    Me.tp_pesonetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_pesonetto.Name = "tp_pesonetto"
    Me.tp_pesonetto.NTSRepositoryComboBox = Nothing
    Me.tp_pesonetto.NTSRepositoryItemCheck = Nothing
    Me.tp_pesonetto.NTSRepositoryItemMemo = Nothing
    Me.tp_pesonetto.NTSRepositoryItemText = Nothing
    Me.tp_pesonetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_pesonetto.Visible = True
    Me.tp_pesonetto.VisibleIndex = 8
    '
    'tp_note
    '
    Me.tp_note.AppearanceCell.Options.UseBackColor = True
    Me.tp_note.AppearanceCell.Options.UseTextOptions = True
    Me.tp_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tp_note.Caption = "Note"
    Me.tp_note.Enabled = True
    Me.tp_note.FieldName = "tp_note"
    Me.tp_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tp_note.Name = "tp_note"
    Me.tp_note.NTSRepositoryComboBox = Nothing
    Me.tp_note.NTSRepositoryItemCheck = Nothing
    Me.tp_note.NTSRepositoryItemMemo = Nothing
    Me.tp_note.NTSRepositoryItemText = Nothing
    Me.tp_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.tp_note.Visible = True
    Me.tp_note.VisibleIndex = 9
    '
    'FRMVESEP1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(625, 319)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMVESEP1"
    Me.NTSLastControlFocussed = Me.grSepa
    Me.Text = "SELEZIONE ORDINI"
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnfiltri.ResumeLayout(False)
    Me.pnfiltri.PerformLayout()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommandbutton.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grSepa, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSepa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRMORSEP1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Try
      lbAnno.Text = oApp.Tr(Me, 129055329104888140, "Anno: ") & strAnno
      edContoDa.Text = "0"
      edContoA.Text = "999999999"
      edDataDa.Text = IntSetDate("01/01/" & strAnno)
      edDataA.Text = IntSetDate("31/12/2099")
      edSerie.Text = ""

      '-------------------------------
      'applico le impostazioni da GCTL
      GctlTipoDoc = " "
      GctlSetRoules()
      GctlApplicaDefaultValue()

      strSoloSerie = oMenu.GetSettingBus("Bsvepack", "OpzioniDocUt", ".", "SoloSerie", "?", strTipork, "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" Then
        edSerie.Text = strSoloSerie
        edSerie.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    If dsSepa Is Nothing Then Return
    If dsSepa.Tables("SEPA").Rows.Count > 0 Then
      oPar.bPar1 = True
      oPar.strPar2 = grvSepa.GetFocusedRowCellValue(tp_serie).ToString()
      oPar.strPar4 = grvSepa.GetFocusedRowCellValue(tp_numdoc).ToString()
    Else
      oPar.bPar1 = False
    End If
    Me.Close()
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = False
    Me.Close()
  End Sub
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strEvaso As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edContoDa.Text) > NTSCInt(edContoA.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692647278809074, "Attenzione!" & vbCrLf & _
          "Il conto iniziale non può essere superiore a quello finale."))
        edContoDa.Text = "0"
        edContoA.Text = "".PadLeft(9, "9"c)
        edContoDa.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edDataDa.Text) > NTSCDate(edDataA.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692648332648235, "Attenzione!" & vbCrLf & _
          "La data iniziale non può essere superiore a quella finale."))
        edDataDa.Text = IntSetDate("01/01/" & NTSCDate(Now).Year.ToString)
        edDataA.Text = IntSetDate("31/12/2099")
        edDataDa.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      dsSepa = oClePack.ZoomSepa(strTipork, CType(strAnno, Integer), edSerie.Text, CType(edContoDa.Text, Integer), CType(edContoA.Text, Integer), _
                                 edDataDa.Text, edDataA.Text, strEvaso, edRiferim.Text.Trim, DittaCorrente)
      dcSepa.DataSource = dsSepa.Tables("SEPA")
      dsSepa.AcceptChanges()
      grSepa.DataSource = dcSepa

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvSepa.RowCount > 0 Then grvSepa.Focus()
      Me.Cursor = Cursors.Default
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub edContoDa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edContoDa.Validated
    Try
      If edContoDa.Text <> "0" Then edContoA.Text = edContoDa.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvSepa_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvSepa.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then If grvSepa.Focused And grvSepa.RowCount > 0 Then cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grSepa_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grSepa.MouseDoubleClick
    Try
      cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
End Class