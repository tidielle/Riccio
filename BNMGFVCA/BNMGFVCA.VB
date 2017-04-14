Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGFVCA
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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

#Region "VARIABILI"
  Public oCleFvca As CLEMGFVCA
  Public oCallParams As CLE__CLDP

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmVqta As NTSInformatica.NTSGroupBox
  Public WithEvents opAr_gescomm As NTSInformatica.NTSRadioButton
  Public WithEvents cbAr_gescommIni As NTSInformatica.NTSComboBox
  Public WithEvents cbAr_gescommFin As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gescommFin As NTSInformatica.NTSLabel
  Public WithEvents lbAr_gescommIni As NTSInformatica.NTSLabel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCommecaPredef As NTSInformatica.NTSLabel

  Private components As System.ComponentModel.IContainer

  Public dttTmp As New DataTable
  Public dttTmp2 As New DataTable
  Public strMessaggio As String = ""

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGFVCA", "BEMGFVCA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128570465788163478, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleFvca = CType(oTmp, CLEMGFVCA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGFVCA", strRemoteServer, strRemotePort)
    AddHandler oCleFvca.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleFvca.Init(oApp, oScript, oMenu.oCleComm, "ARTICO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGFVCA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmVqta = New NTSInformatica.NTSGroupBox
    Me.cbAr_gescommFin = New NTSInformatica.NTSComboBox
    Me.lbAr_gescommFin = New NTSInformatica.NTSLabel
    Me.lbAr_gescommIni = New NTSInformatica.NTSLabel
    Me.cbAr_gescommIni = New NTSInformatica.NTSComboBox
    Me.opAr_gescomm = New NTSInformatica.NTSRadioButton
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbCommecaPredef = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmVqta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmVqta.SuspendLayout()
    CType(Me.cbAr_gescommFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gescommIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 1
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
    'fmVqta
    '
    Me.fmVqta.AllowDrop = True
    Me.fmVqta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmVqta.Appearance.Options.UseBackColor = True
    Me.fmVqta.Controls.Add(Me.cbAr_gescommFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gescommFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gescommIni)
    Me.fmVqta.Controls.Add(Me.cbAr_gescommIni)
    Me.fmVqta.Controls.Add(Me.opAr_gescomm)
    Me.fmVqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmVqta.Location = New System.Drawing.Point(12, 63)
    Me.fmVqta.Name = "fmVqta"
    Me.fmVqta.Size = New System.Drawing.Size(622, 56)
    Me.fmVqta.TabIndex = 4
    Me.fmVqta.Text = "Campi da modificare:"
    '
    'cbAr_gescommFin
    '
    Me.cbAr_gescommFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescommFin.DataSource = Nothing
    Me.cbAr_gescommFin.DisplayMember = ""
    Me.cbAr_gescommFin.Enabled = False
    Me.cbAr_gescommFin.Location = New System.Drawing.Point(431, 26)
    Me.cbAr_gescommFin.Name = "cbAr_gescommFin"
    Me.cbAr_gescommFin.NTSDbField = ""
    Me.cbAr_gescommFin.Properties.AutoHeight = False
    Me.cbAr_gescommFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescommFin.Properties.DropDownRows = 30
    Me.cbAr_gescommFin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescommFin.SelectedValue = ""
    Me.cbAr_gescommFin.Size = New System.Drawing.Size(176, 20)
    Me.cbAr_gescommFin.TabIndex = 109
    Me.cbAr_gescommFin.ValueMember = ""
    '
    'lbAr_gescommFin
    '
    Me.lbAr_gescommFin.AutoSize = True
    Me.lbAr_gescommFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescommFin.Enabled = False
    Me.lbAr_gescommFin.Location = New System.Drawing.Point(412, 29)
    Me.lbAr_gescommFin.Name = "lbAr_gescommFin"
    Me.lbAr_gescommFin.NTSDbField = ""
    Me.lbAr_gescommFin.Size = New System.Drawing.Size(13, 13)
    Me.lbAr_gescommFin.TabIndex = 108
    Me.lbAr_gescommFin.Text = "a"
    Me.lbAr_gescommFin.Tooltip = ""
    Me.lbAr_gescommFin.UseMnemonic = False
    '
    'lbAr_gescommIni
    '
    Me.lbAr_gescommIni.AutoSize = True
    Me.lbAr_gescommIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescommIni.Enabled = False
    Me.lbAr_gescommIni.Location = New System.Drawing.Point(204, 29)
    Me.lbAr_gescommIni.Name = "lbAr_gescommIni"
    Me.lbAr_gescommIni.NTSDbField = ""
    Me.lbAr_gescommIni.Size = New System.Drawing.Size(20, 13)
    Me.lbAr_gescommIni.TabIndex = 107
    Me.lbAr_gescommIni.Text = "Da"
    Me.lbAr_gescommIni.Tooltip = ""
    Me.lbAr_gescommIni.UseMnemonic = False
    '
    'cbAr_gescommIni
    '
    Me.cbAr_gescommIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescommIni.DataSource = Nothing
    Me.cbAr_gescommIni.DisplayMember = ""
    Me.cbAr_gescommIni.Enabled = False
    Me.cbAr_gescommIni.Location = New System.Drawing.Point(230, 26)
    Me.cbAr_gescommIni.Name = "cbAr_gescommIni"
    Me.cbAr_gescommIni.NTSDbField = ""
    Me.cbAr_gescommIni.Properties.AutoHeight = False
    Me.cbAr_gescommIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescommIni.Properties.DropDownRows = 30
    Me.cbAr_gescommIni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescommIni.SelectedValue = ""
    Me.cbAr_gescommIni.Size = New System.Drawing.Size(176, 20)
    Me.cbAr_gescommIni.TabIndex = 106
    Me.cbAr_gescommIni.ValueMember = ""
    '
    'opAr_gescomm
    '
    Me.opAr_gescomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_gescomm.EditValue = True
    Me.opAr_gescomm.Location = New System.Drawing.Point(14, 27)
    Me.opAr_gescomm.Name = "opAr_gescomm"
    Me.opAr_gescomm.NTSCheckValue = "S"
    Me.opAr_gescomm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_gescomm.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_gescomm.Properties.AutoHeight = False
    Me.opAr_gescomm.Properties.Caption = "Gestione per Commessa"
    Me.opAr_gescomm.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_gescomm.Size = New System.Drawing.Size(145, 19)
    Me.opAr_gescomm.TabIndex = 3
    '
    'lbDescr
    '
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbDescr.Location = New System.Drawing.Point(12, 151)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(622, 22)
    Me.lbDescr.TabIndex = 114
    Me.lbDescr.Text = "Nessuna elaborazione in corso."
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(12, 40)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(77, 13)
    Me.lbCodart.TabIndex = 592
    Me.lbCodart.Text = "Codice articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.EditValue = ""
    Me.edCodart.Location = New System.Drawing.Point(109, 37)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.AutoHeight = False
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(182, 20)
    Me.edCodart.TabIndex = 593
    '
    'lbCommecaPredef
    '
    Me.lbCommecaPredef.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaPredef.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbCommecaPredef.Location = New System.Drawing.Point(12, 125)
    Me.lbCommecaPredef.Name = "lbCommecaPredef"
    Me.lbCommecaPredef.NTSDbField = ""
    Me.lbCommecaPredef.Size = New System.Drawing.Size(622, 26)
    Me.lbCommecaPredef.TabIndex = 594
    Me.lbCommecaPredef.Text = "Commessa stock (magazzino):   NON INDICATA"
    Me.lbCommecaPredef.Tooltip = ""
    Me.lbCommecaPredef.UseMnemonic = False
    '
    'FRMMGFVCA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(647, 174)
    Me.Controls.Add(Me.lbCommecaPredef)
    Me.Controls.Add(Me.lbCodart)
    Me.Controls.Add(Me.edCodart)
    Me.Controls.Add(Me.lbDescr)
    Me.Controls.Add(Me.fmVqta)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGFVCA"
    Me.Text = "FORZA VARIAZIONE CAMPI ARTICOLO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmVqta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmVqta.ResumeLayout(False)
    Me.fmVqta.PerformLayout()
    CType(Me.cbAr_gescommFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gescommIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '----------------------------------------------------------------------------------
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128553203421875805, "Codice articolo"), tabartico, True)
      opAr_gescomm.NTSSetParam(oMenu, oApp.Tr(Me, 128570568434977562, "Gestione per Commessa"), "N")
      cbAr_gescommIni.NTSSetParam(oApp.Tr(Me, 128570572568991736, "Valore da sostituire"))
      cbAr_gescommFin.NTSSetParam(oApp.Tr(Me, 128570572912094776, "Nuovo valore da inserire"))
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "FORM"

  Public Overridable Sub FRMMGFVCA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      CaricaCombo()
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      SettaControlli()
      '----------------------------------------------------------------------------------
      oCleFvca.LeggiRegistro()
      '----------------------------------------------------------------------------------
      If oCleFvca.nCommessaPredef <> 0 Then
        lbCommecaPredef.Text = "Commessa stock (magazzino):   " & oCleFvca.nCommessaPredef
      End If
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGFVCA_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If oCleFvca.nCommessaPredef <= 0 Or oCleFvca.nCommessaPredef > 999999999 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128570623140672046, "ATTENZIONE" & vbCrLf & _
                                                       "Non è stato impostato correttamente il numero della commessa " & vbCrLf & _
                                                       "da utilizzarsi come COMMESSA STOCK (MAGAZZINO)." & vbCrLf & _
                                                       "[Opzione di registro: BSDBEMRP\OPZIONI\CommessaPredefinita]" & vbCrLf & _
                                                       "Il programma al momento non è pertanto utilizzabile."))
        Me.Close()
      End If
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGFVQCA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If oCleFvca.bElabInCorso Then e.Cancel = True
  End Sub

#End Region

#Region "TOOLBAR"

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      Elabora()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

  Public Overridable Sub edCodart_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Dim strError As String = ""
    Try

      If edCodart.Text <> NTSCStr(edCodart.OldEditValue) Then
        If Trim(edCodart.Text) <> "" Then
          If Not oCleFvca.edCodart_Validated(NTSCStr(edCodart.Text), strError, dttTmp, dttTmp2) Then
            oApp.MsgBoxErr(strError)
            edCodart.Text = NTSCStr(edCodart.OldEditValue)
          Else
            SettaControlli()
          End If
        Else
          edCodart.Text = ""
          SettaControlli()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbAr_gescommFin_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAr_gescommFin.SelectedValueChanged
    Try
      Select Case cbAr_gescommIni.SelectedValue
        Case "N"
          Select Case cbAr_gescommFin.SelectedValue
            Case "N"
              'non possibile
            Case "O"
              oCleFvca.bRicostruisci = False
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "- PROPOSTE D'ORDINE" & vbCrLf & _
                       "- LISTE DI PRODUZIONE" & vbCrLf & _
                       "- ORDINI" & vbCrLf & _
                       "Si conferma di voler procedere?"
            Case "S"
              oCleFvca.bRicostruisci = True
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "- PROPOSTE D'ORDINE" & vbCrLf & _
                       "- LISTE DI PRODUZIONE" & vbCrLf & _
                       "- ORDINI" & vbCrLf & _
                       "- NOTE DI PRELIEVO" & vbCrLf & _
                       "- DOCUMENTI" & vbCrLf & _
                       "- PROGRESSIVI DEFINITIVI" & vbCrLf & _
                       "Si conferma di voler procedere?"
          End Select
        Case "O"
          Select Case cbAr_gescommFin.SelectedValue
            Case "N"
              oCleFvca.bRicostruisci = False
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "Si conferma di voler procedere?"
            Case "O"
              'non possibile
            Case "S"
              oCleFvca.bRicostruisci = True
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "- ORDINI" & vbCrLf & _
                       "- NOTE DI PRELIEVO" & vbCrLf & _
                       "- DOCUMENTI" & vbCrLf & _
                       "- PROGRESSIVI DEFINITIVI" & vbCrLf & _
                       "Si conferma di voler procedere?"
          End Select
        Case "S"
          Select Case cbAr_gescommFin.SelectedValue
            Case "N"
              oCleFvca.bRicostruisci = True
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "- ORDINI" & vbCrLf & _
                       "- NOTE DI PRELIEVO" & vbCrLf & _
                       "- DOCUMENTI" & vbCrLf & _
                       "- PROGRESSIVI DEFINITIVI" & vbCrLf & _
                       "Si conferma di voler procedere?"
            Case "O"
              oCleFvca.bRicostruisci = True
              strMessaggio = "ATTENZIONE" & vbCrLf & _
                       "L'elaborazione richiede che NESSUN OPERATORE stia lavorando su:" & vbCrLf & _
                       "- ANAGRAFICA ARTICOLI" & vbCrLf & _
                       "- ORDINI" & vbCrLf & _
                       "- NOTE DI PRELIEVO" & vbCrLf & _
                       "- DOCUMENTI" & vbCrLf & _
                       "- PROGRESSIVI DEFINITIVI" & vbCrLf & _
                       "Si conferma di voler procedere?"
            Case "S"
              'non possibile
          End Select
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      '----------------------------------------------------------------------------------
      Dim dttGescommIni As New DataTable()
      dttGescommIni.Columns.Add("cod", GetType(String))
      dttGescommIni.Columns.Add("val", GetType(String))
      dttGescommIni.Rows.Add(New Object() {"N", "No"})
      dttGescommIni.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttGescommIni.Rows.Add(New Object() {"S", "Ordini/Impegni/Magazzino"})
      dttGescommIni.AcceptChanges()
      cbAr_gescommIni.DataSource = dttGescommIni
      cbAr_gescommIni.ValueMember = "cod"
      cbAr_gescommIni.DisplayMember = "val"
      cbAr_gescommIni.SelectedValue = "N"
      '----------------------------------------------------------------------------------
      Dim dttGescommFin As New DataTable()
      dttGescommFin.Columns.Add("cod", GetType(String))
      dttGescommFin.Columns.Add("val", GetType(String))
      dttGescommFin.Rows.Add(New Object() {"N", "No"})
      dttGescommFin.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttGescommFin.Rows.Add(New Object() {"S", "Ordini/Impegni/Magazzino"})
      dttGescommFin.AcceptChanges()
      cbAr_gescommFin.DataSource = dttGescommFin
      cbAr_gescommFin.ValueMember = "cod"
      cbAr_gescommFin.DisplayMember = "val"
      cbAr_gescommFin.SelectedValue = "N"
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Elabora() As Boolean
    Dim strMsg As String = ""
    Try

      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '----------------------------------------------------------------------------------

      '----------------------------------------------------------------------------------
      '--- Controlla se è stato selezionato l'articolo
      '----------------------------------------------------------------------------------
      If edCodart.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128570623140672046, "Cod. Articolo obbligatorio."))
        Return False
      End If

      '----------------------------------------------------------------------------------
      '--- Controlla la validità dei dati impostati in maschera
      '----------------------------------------------------------------------------------
      If (opAr_gescomm.Checked = True) And (cbAr_gescommIni.SelectedValue = cbAr_gescommFin.SelectedValue) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128570623140672046, "ATTENZIONE" & vbCrLf & _
                                                       "Il valore da sostituire nel campo 'Gestione per commessa'" & vbCrLf & _
                                                       "è uguale al nuovo valore da sostituire." & vbCrLf & _
                                                       "Elaborazione priva di significato."))
        Return False
      End If
      '----------------------------------------------------------------------------------
      '--- Confermo la elaborazione
      '----------------------------------------------------------------------------------    
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128570623140672046, strMessaggio)) = DialogResult.No Then
        Return False
      End If
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '----------------------------------------------------------------------------------
      strMsg = oApp.Tr(Me, 129060371399624831, "Modifica del campo ")
      If opAr_gescomm.Checked = True Then strMsg += oApp.Tr(Me, 129060371449627071, "'Gestione per commessa'")
      strMsg += oApp.Tr(Me, 129082851447595382, " in corso...")
      lbDescr.Text = strMsg

      '----------------------------------------------------------------------------------
      '--- Chiamo il metodo dell'Entity
      '----------------------------------------------------------------------------------
      If oCleFvca.Elabora(dttTmp2, opAr_gescomm.Checked, cbAr_gescommIni.SelectedValue, cbAr_gescommFin.SelectedValue) = True Then
        '----------------------------------------------------------------------------------
        lbDescr.Text = oApp.Tr(Me, 129082851842925592, "Nessuna elaborazione in corso.")
        '----------------------------------------------------------------------------------
        Me.Cursor = Cursors.Default
        '----------------------------------------------------------------------------------
        oApp.MsgBoxInfo(oApp.Tr(Me, 128570634002314900, "Elaborazione terminata correttamente."))
      Else
        Return False
      End If

      Return True

    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    Finally
      '----------------------------------------------------------------------------------
      edCodart.Text = ""
      '----------------------------------------------------------------------------------
      lbDescr.Text = oApp.Tr(Me, 129082851842925592, "Nessuna elaborazione in corso.")
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      'If oCleFvca.nLogCount > 0 Then
      '  If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129356689615767552, "ATTENZIONE" & vbCrLf & _
      '                                                                 "Esistono dei messaggi nel file di log." & vbCrLf & _
      '                                                                 "Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
      '    NTSProcessStart("notepad", oCleFvca.LogFileName)
      '  End If
      'End If
    End Try
  End Function

  Public Overridable Sub SettaControlli()
    Try
      '----------------------------------------------------------------------------------
      If edCodart.Text <> "" Then
        GctlSetVisEnab(cbAr_gescommFin, False)
        cbAr_gescommIni.SelectedValue = NTSCStr(dttTmp.Rows(0)!ar_gescomm)
        cbAr_gescommFin.SelectedIndex = -1
        cbAr_gescommFin.SelectedValue = "N"
        cbAr_gescommFin.Focus()
      Else
        cbAr_gescommFin.Enabled = False
        cbAr_gescommIni.SelectedValue = "N"
        cbAr_gescommFin.SelectedIndex = -1
        cbAr_gescommFin.SelectedValue = "N"
        edCodart.Focus()
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

End Class
