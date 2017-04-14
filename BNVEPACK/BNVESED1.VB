Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESED1
  Public strTipork As String
  Public strAnno As String
  Public oPar As CLE__CLDP
  Public oClePack As CLEVEPACK
  Public dsSeor As DataSet
  Public dcSeor As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

#Region "Variabili"
  Public WithEvents cmdSeleziona As NTSButton
  Public WithEvents cmdAnnulla As NTSButton
  Public WithEvents pnfiltri As NTSInformatica.NTSPanel
  Public WithEvents fmEvaso As NTSInformatica.NTSGroupBox
  Public WithEvents optSi As NTSInformatica.NTSRadioButton
  Public WithEvents optTutti As NTSInformatica.NTSRadioButton
  Public WithEvents optNo As NTSInformatica.NTSRadioButton
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents lbAdatacons As NTSInformatica.NTSLabel
  Public WithEvents lbDadatacons As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edDataA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatConsA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatConsDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edContoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnCommandbutton As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grSeor As NTSInformatica.NTSGrid
  Public WithEvents grvSeor As NTSInformatica.NTSGridView
  Public WithEvents td_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents td_anno As NTSInformatica.NTSGridColumn
  Public WithEvents td_serie As NTSInformatica.NTSGridColumn
  Public WithEvents td_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents td_conto As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn16 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn17 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn18 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn19 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn110 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn111 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn112 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn113 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn114 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents td_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents td_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents td_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents td_confermato As NTSInformatica.NTSGridColumn
  Public WithEvents td_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_destin As NTSInformatica.NTSGridColumn
  Public WithEvents td_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
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
      edDatConsA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023303440375, "A data consegna"), False)
      edDatConsDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023303596548, "Da data consegna"), False)
      edContoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055327380145450, "A codice cliente/fornitore"), tabanagrac)
      edContoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055327456554145, "Da codice cliente/fornitore"), tabanagrac)
      edDataA.NTSSetParam(oMenu, oApp.Tr(Me, 129055327477648570, "A data documento"), False)
      edDataDa.NTSSetParam(oMenu, oApp.Tr(Me, 129055327528431445, "Da data documento"), False)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 129055327603277590, "Serie documento"), CLN__STD.SerieMaxLen, True)
      optTutti.NTSSetParam(oMenu, oApp.Tr(Me, 129055327634216080, ""), "S")
      optNo.NTSSetParam(oMenu, oApp.Tr(Me, 129055327659841900, ""), "S")
      optSi.NTSSetParam(oMenu, oApp.Tr(Me, 129055327682030110, ""), "S")
      edRiferim.NTSSetParam(oMenu, oApp.Tr(Me, 129055327712968600, "Riferimenti"), 255, True)

      grvSeor.NTSSetParam(oMenu, "")

      td_tipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305158278, "Tipo documento"), 0, True)
      td_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305314451, "Anno"), "0", 4, 0, 9999)
      td_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305470624, "Serie"), CLN__STD.SerieMaxLen, True)
      td_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305626797, "Numero"), "0", 9, 0, 999999999)
      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023305782970, "Data ordine"), True)
      td_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305939143, "Codice conto"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023306095316, "Descr. conto"), 0, True)
      td_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023306251489, "Data consegna"), True)
      td_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306407662, "Tot. documento"), "#,##0.00", 13)
      td_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023306563835, "Evaso"), "S", "N")
      td_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023306720008, "Confermato"), "S", "N")
      td_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306876181, "Cod. destin."), "0", 9, 0, 999999999)
      xx_destin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307032354, "Descr. destin."), 0, True)
      td_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023307188527, "Commessa"), "0", 9, 0, 999999999)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307344700, "Descr. commeca"), 0, True)


      Select Case strTipork
        Case "Q", "R", "V"
          edContoDa.NTSSetParamZoom("ZOOMANAGRAC")
          edContoA.NTSSetParamZoom("ZOOMANAGRAC")
        Case Else
          edContoDa.NTSSetParamZoom("ZOOMANAGRAF")
          edContoA.NTSSetParamZoom("ZOOMANAGRAF")
      End Select

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
    Me.edDatConsA = New NTSInformatica.NTSTextBoxData
    Me.edDatConsDa = New NTSInformatica.NTSTextBoxData
    Me.edContoA = New NTSInformatica.NTSTextBoxNum
    Me.edContoDa = New NTSInformatica.NTSTextBoxNum
    Me.edDataA = New NTSInformatica.NTSTextBoxData
    Me.edDataDa = New NTSInformatica.NTSTextBoxData
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.lbAdatacons = New NTSInformatica.NTSLabel
    Me.lbDadatacons = New NTSInformatica.NTSLabel
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.fmEvaso = New NTSInformatica.NTSGroupBox
    Me.optTutti = New NTSInformatica.NTSRadioButton
    Me.optNo = New NTSInformatica.NTSRadioButton
    Me.optSi = New NTSInformatica.NTSRadioButton
    Me.pnCommandbutton = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grSeor = New NTSInformatica.NTSGrid
    Me.grvSeor = New NTSInformatica.NTSGridView
    Me.td_tipork = New NTSInformatica.NTSGridColumn
    Me.td_anno = New NTSInformatica.NTSGridColumn
    Me.td_serie = New NTSInformatica.NTSGridColumn
    Me.td_numord = New NTSInformatica.NTSGridColumn
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.td_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.td_datcons = New NTSInformatica.NTSGridColumn
    Me.td_totdoc = New NTSInformatica.NTSGridColumn
    Me.td_flevas = New NTSInformatica.NTSGridColumn
    Me.td_confermato = New NTSInformatica.NTSGridColumn
    Me.td_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_destin = New NTSInformatica.NTSGridColumn
    Me.td_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnfiltri.SuspendLayout()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatConsA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatConsDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmEvaso, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEvaso.SuspendLayout()
    CType(Me.optTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommandbutton.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grSeor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSeor, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnfiltri.Controls.Add(Me.edDatConsA)
    Me.pnfiltri.Controls.Add(Me.edDatConsDa)
    Me.pnfiltri.Controls.Add(Me.edContoA)
    Me.pnfiltri.Controls.Add(Me.edContoDa)
    Me.pnfiltri.Controls.Add(Me.edDataA)
    Me.pnfiltri.Controls.Add(Me.edDataDa)
    Me.pnfiltri.Controls.Add(Me.lbAdata)
    Me.pnfiltri.Controls.Add(Me.lbAdatacons)
    Me.pnfiltri.Controls.Add(Me.lbDadatacons)
    Me.pnfiltri.Controls.Add(Me.lbDadata)
    Me.pnfiltri.Controls.Add(Me.lbAconto)
    Me.pnfiltri.Controls.Add(Me.lbDaconto)
    Me.pnfiltri.Controls.Add(Me.edSerie)
    Me.pnfiltri.Controls.Add(Me.lbSerie)
    Me.pnfiltri.Controls.Add(Me.fmEvaso)
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
    'edDatConsA
    '
    Me.edDatConsA.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDatConsA.Location = New System.Drawing.Point(251, 59)
    Me.edDatConsA.Name = "edDatConsA"
    Me.edDatConsA.NTSDbField = ""
    Me.edDatConsA.NTSForzaVisZoom = False
    Me.edDatConsA.NTSOldValue = ""
    Me.edDatConsA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatConsA.Properties.Appearance.Options.UseBackColor = True
    Me.edDatConsA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatConsA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatConsA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatConsA.Properties.MaxLength = 65536
    Me.edDatConsA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatConsA.Size = New System.Drawing.Size(84, 20)
    Me.edDatConsA.TabIndex = 48
    '
    'edDatConsDa
    '
    Me.edDatConsDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatConsDa.Location = New System.Drawing.Point(94, 59)
    Me.edDatConsDa.Name = "edDatConsDa"
    Me.edDatConsDa.NTSDbField = ""
    Me.edDatConsDa.NTSForzaVisZoom = False
    Me.edDatConsDa.NTSOldValue = ""
    Me.edDatConsDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatConsDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDatConsDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatConsDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatConsDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatConsDa.Properties.MaxLength = 65536
    Me.edDatConsDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatConsDa.Size = New System.Drawing.Size(84, 20)
    Me.edDatConsDa.TabIndex = 47
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
    'lbAdatacons
    '
    Me.lbAdatacons.AutoSize = True
    Me.lbAdatacons.BackColor = System.Drawing.Color.Transparent
    Me.lbAdatacons.Location = New System.Drawing.Point(183, 62)
    Me.lbAdatacons.Name = "lbAdatacons"
    Me.lbAdatacons.NTSDbField = ""
    Me.lbAdatacons.Size = New System.Drawing.Size(68, 13)
    Me.lbAdatacons.TabIndex = 11
    Me.lbAdatacons.Text = "A data cons."
    Me.lbAdatacons.Tooltip = ""
    Me.lbAdatacons.UseMnemonic = False
    '
    'lbDadatacons
    '
    Me.lbDadatacons.AutoSize = True
    Me.lbDadatacons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatacons.Location = New System.Drawing.Point(12, 62)
    Me.lbDadatacons.Name = "lbDadatacons"
    Me.lbDadatacons.NTSDbField = ""
    Me.lbDadatacons.Size = New System.Drawing.Size(74, 13)
    Me.lbDadatacons.TabIndex = 10
    Me.lbDadatacons.Text = "Da data cons."
    Me.lbDadatacons.Tooltip = ""
    Me.lbDadatacons.UseMnemonic = False
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
    Me.edSerie.Location = New System.Drawing.Point(455, 9)
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
    Me.lbSerie.Location = New System.Drawing.Point(422, 12)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 5
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'fmEvaso
    '
    Me.fmEvaso.AllowDrop = True
    Me.fmEvaso.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEvaso.Appearance.Options.UseBackColor = True
    Me.fmEvaso.Controls.Add(Me.optTutti)
    Me.fmEvaso.Controls.Add(Me.optNo)
    Me.fmEvaso.Controls.Add(Me.optSi)
    Me.fmEvaso.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEvaso.Location = New System.Drawing.Point(342, 34)
    Me.fmEvaso.Name = "fmEvaso"
    Me.fmEvaso.Size = New System.Drawing.Size(159, 68)
    Me.fmEvaso.TabIndex = 4
    Me.fmEvaso.Text = "Evaso"
    '
    'optTutti
    '
    Me.optTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optTutti.EditValue = True
    Me.optTutti.Location = New System.Drawing.Point(101, 31)
    Me.optTutti.Name = "optTutti"
    Me.optTutti.NTSCheckValue = "S"
    Me.optTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optTutti.Properties.Caption = "&Tutti"
    Me.optTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optTutti.Size = New System.Drawing.Size(46, 19)
    Me.optTutti.TabIndex = 2
    '
    'optNo
    '
    Me.optNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNo.Location = New System.Drawing.Point(50, 31)
    Me.optNo.Name = "optNo"
    Me.optNo.NTSCheckValue = "S"
    Me.optNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNo.Properties.Appearance.Options.UseBackColor = True
    Me.optNo.Properties.Caption = "&No"
    Me.optNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNo.Size = New System.Drawing.Size(39, 19)
    Me.optNo.TabIndex = 1
    '
    'optSi
    '
    Me.optSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.optSi.Location = New System.Drawing.Point(5, 31)
    Me.optSi.Name = "optSi"
    Me.optSi.NTSCheckValue = "S"
    Me.optSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSi.Properties.Appearance.Options.UseBackColor = True
    Me.optSi.Properties.Caption = "&Si"
    Me.optSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSi.Size = New System.Drawing.Size(34, 19)
    Me.optSi.TabIndex = 0
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
    Me.pnGrid.Controls.Add(Me.grSeor)
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 113)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(625, 206)
    Me.pnGrid.TabIndex = 51
    '
    'grSeor
    '
    Me.grSeor.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grSeor.EmbeddedNavigator.Name = ""
    Me.grSeor.Location = New System.Drawing.Point(0, 0)
    Me.grSeor.MainView = Me.grvSeor
    Me.grSeor.Name = "grSeor"
    Me.grSeor.Size = New System.Drawing.Size(625, 206)
    Me.grSeor.TabIndex = 6
    Me.grSeor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSeor})
    '
    'grvSeor
    '
    Me.grvSeor.ActiveFilterEnabled = False
    Me.grvSeor.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.td_tipork, Me.td_anno, Me.td_serie, Me.td_numord, Me.td_datord, Me.td_conto, Me.xx_conto, Me.td_datcons, Me.td_totdoc, Me.td_flevas, Me.td_confermato, Me.td_coddest, Me.xx_destin, Me.td_commeca, Me.xx_commeca})
    Me.grvSeor.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSeor.Enabled = False
    Me.grvSeor.GridControl = Me.grSeor
    Me.grvSeor.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvSeor.MinRowHeight = 14
    Me.grvSeor.Name = "grvSeor"
    Me.grvSeor.NTSAllowDelete = False
    Me.grvSeor.NTSAllowInsert = False
    Me.grvSeor.NTSAllowUpdate = False
    Me.grvSeor.NTSMenuContext = Nothing
    Me.grvSeor.OptionsBehavior.Editable = False
    Me.grvSeor.OptionsCustomization.AllowRowSizing = True
    Me.grvSeor.OptionsFilter.AllowFilterEditor = False
    Me.grvSeor.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSeor.OptionsNavigation.UseTabKey = False
    Me.grvSeor.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSeor.OptionsView.ColumnAutoWidth = False
    Me.grvSeor.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSeor.OptionsView.ShowGroupPanel = False
    Me.grvSeor.RowHeight = 16
    '
    'td_tipork
    '
    Me.td_tipork.AppearanceCell.Options.UseBackColor = True
    Me.td_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipork.Caption = "Tipo documento"
    Me.td_tipork.Enabled = True
    Me.td_tipork.FieldName = "td_tipork"
    Me.td_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipork.Name = "td_tipork"
    Me.td_tipork.NTSRepositoryComboBox = Nothing
    Me.td_tipork.NTSRepositoryItemCheck = Nothing
    Me.td_tipork.NTSRepositoryItemMemo = Nothing
    Me.td_tipork.NTSRepositoryItemText = Nothing
    Me.td_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_tipork.Width = 105
    '
    'td_anno
    '
    Me.td_anno.AppearanceCell.Options.UseBackColor = True
    Me.td_anno.AppearanceCell.Options.UseTextOptions = True
    Me.td_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_anno.Caption = "Anno"
    Me.td_anno.Enabled = True
    Me.td_anno.FieldName = "td_anno"
    Me.td_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_anno.Name = "td_anno"
    Me.td_anno.NTSRepositoryComboBox = Nothing
    Me.td_anno.NTSRepositoryItemCheck = Nothing
    Me.td_anno.NTSRepositoryItemMemo = Nothing
    Me.td_anno.NTSRepositoryItemText = Nothing
    Me.td_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_anno.Visible = True
    Me.td_anno.VisibleIndex = 0
    Me.td_anno.Width = 58
    '
    'td_serie
    '
    Me.td_serie.AppearanceCell.Options.UseBackColor = True
    Me.td_serie.AppearanceCell.Options.UseTextOptions = True
    Me.td_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_serie.Caption = "Serie"
    Me.td_serie.Enabled = True
    Me.td_serie.FieldName = "td_serie"
    Me.td_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_serie.Name = "td_serie"
    Me.td_serie.NTSRepositoryComboBox = Nothing
    Me.td_serie.NTSRepositoryItemCheck = Nothing
    Me.td_serie.NTSRepositoryItemMemo = Nothing
    Me.td_serie.NTSRepositoryItemText = Nothing
    Me.td_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_serie.Visible = True
    Me.td_serie.VisibleIndex = 1
    Me.td_serie.Width = 46
    '
    'td_numord
    '
    Me.td_numord.AppearanceCell.Options.UseBackColor = True
    Me.td_numord.AppearanceCell.Options.UseTextOptions = True
    Me.td_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numord.Caption = "Numero"
    Me.td_numord.Enabled = True
    Me.td_numord.FieldName = "td_numord"
    Me.td_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numord.Name = "td_numord"
    Me.td_numord.NTSRepositoryComboBox = Nothing
    Me.td_numord.NTSRepositoryItemCheck = Nothing
    Me.td_numord.NTSRepositoryItemMemo = Nothing
    Me.td_numord.NTSRepositoryItemText = Nothing
    Me.td_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_numord.Visible = True
    Me.td_numord.VisibleIndex = 2
    Me.td_numord.Width = 91
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data ordine"
    Me.td_datord.Enabled = True
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 3
    Me.td_datord.Width = 82
    '
    'td_conto
    '
    Me.td_conto.AppearanceCell.Options.UseBackColor = True
    Me.td_conto.AppearanceCell.Options.UseTextOptions = True
    Me.td_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_conto.Caption = "Codice conto"
    Me.td_conto.Enabled = True
    Me.td_conto.FieldName = "td_conto"
    Me.td_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_conto.Name = "td_conto"
    Me.td_conto.NTSRepositoryComboBox = Nothing
    Me.td_conto.NTSRepositoryItemCheck = Nothing
    Me.td_conto.NTSRepositoryItemMemo = Nothing
    Me.td_conto.NTSRepositoryItemText = Nothing
    Me.td_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_conto.Visible = True
    Me.td_conto.VisibleIndex = 4
    Me.td_conto.Width = 90
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
    Me.xx_conto.VisibleIndex = 5
    Me.xx_conto.Width = 112
    '
    'td_datcons
    '
    Me.td_datcons.AppearanceCell.Options.UseBackColor = True
    Me.td_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.td_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datcons.Caption = "Data consegna"
    Me.td_datcons.Enabled = True
    Me.td_datcons.FieldName = "td_datcons"
    Me.td_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datcons.Name = "td_datcons"
    Me.td_datcons.NTSRepositoryComboBox = Nothing
    Me.td_datcons.NTSRepositoryItemCheck = Nothing
    Me.td_datcons.NTSRepositoryItemMemo = Nothing
    Me.td_datcons.NTSRepositoryItemText = Nothing
    Me.td_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_datcons.Visible = True
    Me.td_datcons.VisibleIndex = 6
    '
    'td_totdoc
    '
    Me.td_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.td_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.td_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totdoc.Caption = "Tot. documento"
    Me.td_totdoc.Enabled = True
    Me.td_totdoc.FieldName = "td_totdoc"
    Me.td_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totdoc.Name = "td_totdoc"
    Me.td_totdoc.NTSRepositoryComboBox = Nothing
    Me.td_totdoc.NTSRepositoryItemCheck = Nothing
    Me.td_totdoc.NTSRepositoryItemMemo = Nothing
    Me.td_totdoc.NTSRepositoryItemText = Nothing
    Me.td_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_totdoc.Visible = True
    Me.td_totdoc.VisibleIndex = 7
    '
    'td_flevas
    '
    Me.td_flevas.AppearanceCell.Options.UseBackColor = True
    Me.td_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.td_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_flevas.Caption = "Evaso"
    Me.td_flevas.Enabled = True
    Me.td_flevas.FieldName = "td_flevas"
    Me.td_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_flevas.Name = "td_flevas"
    Me.td_flevas.NTSRepositoryComboBox = Nothing
    Me.td_flevas.NTSRepositoryItemCheck = Nothing
    Me.td_flevas.NTSRepositoryItemMemo = Nothing
    Me.td_flevas.NTSRepositoryItemText = Nothing
    Me.td_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_flevas.Visible = True
    Me.td_flevas.VisibleIndex = 8
    '
    'td_confermato
    '
    Me.td_confermato.AppearanceCell.Options.UseBackColor = True
    Me.td_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.td_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_confermato.Caption = "Confermato"
    Me.td_confermato.Enabled = True
    Me.td_confermato.FieldName = "td_confermato"
    Me.td_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_confermato.Name = "td_confermato"
    Me.td_confermato.NTSRepositoryComboBox = Nothing
    Me.td_confermato.NTSRepositoryItemCheck = Nothing
    Me.td_confermato.NTSRepositoryItemMemo = Nothing
    Me.td_confermato.NTSRepositoryItemText = Nothing
    Me.td_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_confermato.Visible = True
    Me.td_confermato.VisibleIndex = 9
    '
    'td_coddest
    '
    Me.td_coddest.AppearanceCell.Options.UseBackColor = True
    Me.td_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.td_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_coddest.Caption = "Cod. destin."
    Me.td_coddest.Enabled = True
    Me.td_coddest.FieldName = "td_coddest"
    Me.td_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_coddest.Name = "td_coddest"
    Me.td_coddest.NTSRepositoryComboBox = Nothing
    Me.td_coddest.NTSRepositoryItemCheck = Nothing
    Me.td_coddest.NTSRepositoryItemMemo = Nothing
    Me.td_coddest.NTSRepositoryItemText = Nothing
    Me.td_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_coddest.Visible = True
    Me.td_coddest.VisibleIndex = 10
    '
    'xx_destin
    '
    Me.xx_destin.AppearanceCell.Options.UseBackColor = True
    Me.xx_destin.AppearanceCell.Options.UseTextOptions = True
    Me.xx_destin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_destin.Caption = "Descr. destin."
    Me.xx_destin.Enabled = True
    Me.xx_destin.FieldName = "xx_destin"
    Me.xx_destin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_destin.Name = "xx_destin"
    Me.xx_destin.NTSRepositoryComboBox = Nothing
    Me.xx_destin.NTSRepositoryItemCheck = Nothing
    Me.xx_destin.NTSRepositoryItemMemo = Nothing
    Me.xx_destin.NTSRepositoryItemText = Nothing
    Me.xx_destin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_destin.Visible = True
    Me.xx_destin.VisibleIndex = 11
    '
    'td_commeca
    '
    Me.td_commeca.AppearanceCell.Options.UseBackColor = True
    Me.td_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.td_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_commeca.Caption = "Commessa"
    Me.td_commeca.Enabled = True
    Me.td_commeca.FieldName = "td_commeca"
    Me.td_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_commeca.Name = "td_commeca"
    Me.td_commeca.NTSRepositoryComboBox = Nothing
    Me.td_commeca.NTSRepositoryItemCheck = Nothing
    Me.td_commeca.NTSRepositoryItemMemo = Nothing
    Me.td_commeca.NTSRepositoryItemText = Nothing
    Me.td_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_commeca.Visible = True
    Me.td_commeca.VisibleIndex = 12
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commeca"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 13
    '
    'FRMVESED1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(625, 319)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMVESED1"
    Me.NTSLastControlFocussed = Me.grSeor
    Me.Text = "SELEZIONE ORDINI"
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnfiltri.ResumeLayout(False)
    Me.pnfiltri.PerformLayout()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatConsA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatConsDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmEvaso, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEvaso.ResumeLayout(False)
    Me.fmEvaso.PerformLayout()
    CType(Me.optTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommandbutton.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grSeor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSeor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRMVESED1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Try
      lbAnno.Text = oApp.Tr(Me, 129055327937038270, "Anno: ") & strAnno
      edContoDa.Text = "0"
      edContoA.Text = "999999999"
      edDataDa.Text = IntSetDate("01/01/" & strAnno)
      edDataA.Text = IntSetDate("31/12/2099")
      edDatConsDa.Text = IntSetDate("01/01/" & strAnno)
      edDatConsA.Text = IntSetDate("31/12/2099")
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
    If dsSeor Is Nothing Then Return
    If dsSeor.Tables("SEOR").Rows.Count > 0 Then
      oPar.bPar1 = True
      oPar.strPar2 = grvSeor.GetFocusedRowCellValue(td_serie).ToString()
      oPar.strPar4 = grvSeor.GetFocusedRowCellValue(td_numord).ToString()
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
      If optSi.Checked Then strEvaso = "S"
      If optNo.Checked Then strEvaso = "N"
      Me.Cursor = Cursors.WaitCursor
      dsSeor = oClePack.ZoomSeor(strTipork, CType(strAnno, Integer), edSerie.Text, CType(edContoDa.Text, Integer), _
                                 CType(edContoA.Text, Integer), edDataDa.Text, edDataA.Text, edDatConsDa.Text, _
                                 edDatConsA.Text, strEvaso, edRiferim.Text.Trim, DittaCorrente)
      dcSeor.DataSource = dsSeor.Tables("SEOR")
      dsSeor.AcceptChanges()
      grSeor.DataSource = dcSeor

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvSeor.RowCount > 0 Then grvSeor.Focus()
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

  Public Overridable Sub grvSeor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvSeor.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then If grvSeor.Focused And grvSeor.RowCount > 0 Then cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grSeor_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grSeor.MouseDoubleClick
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
