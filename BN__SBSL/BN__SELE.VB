Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SELE
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean = False
  Public strChiamante As String = ""

  Public Overridable Sub InitializeComponent()
    Me.edUser = New NTSInformatica.NTSTextBoxStr
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.edDeviceName = New NTSInformatica.NTSTextBoxStr
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbUser = New NTSInformatica.NTSLabel
    Me.lbDeviceName = New NTSInformatica.NTSLabel
    Me.lbTipopag = New NTSInformatica.NTSLabel
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.edDatScadA = New NTSInformatica.NTSTextBoxData
    Me.edDatScadDa = New NTSInformatica.NTSTextBoxData
    Me.lbDatScadA = New NTSInformatica.NTSLabel
    Me.lbDatScadDa = New NTSInformatica.NTSLabel
    CType(Me.edUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDeviceName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edDatScadA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatScadDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'edUser
    '
    Me.edUser.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edUser.EditValue = ""
    Me.edUser.Location = New System.Drawing.Point(100, 53)
    Me.edUser.Name = "edUser"
    Me.edUser.NTSDbField = ""
    Me.edUser.NTSForzaVisZoom = False
    Me.edUser.NTSOldValue = ""
    Me.edUser.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edUser.Properties.Appearance.Options.UseBackColor = True
    Me.edUser.Properties.Appearance.Options.UseTextOptions = True
    Me.edUser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUser.Properties.AutoHeight = False
    Me.edUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUser.Properties.MaxLength = 9
    Me.edUser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUser.Size = New System.Drawing.Size(120, 20)
    Me.edUser.TabIndex = 8
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(100, 1)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(120, 20)
    Me.cbTipo.TabIndex = 0
    Me.cbTipo.ValueMember = ""
    '
    'edDeviceName
    '
    Me.edDeviceName.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDeviceName.EditValue = ""
    Me.edDeviceName.Location = New System.Drawing.Point(100, 27)
    Me.edDeviceName.Name = "edDeviceName"
    Me.edDeviceName.NTSDbField = ""
    Me.edDeviceName.NTSForzaVisZoom = False
    Me.edDeviceName.NTSOldValue = ""
    Me.edDeviceName.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDeviceName.Properties.Appearance.Options.UseBackColor = True
    Me.edDeviceName.Properties.Appearance.Options.UseTextOptions = True
    Me.edDeviceName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDeviceName.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDeviceName.Properties.AutoHeight = False
    Me.edDeviceName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDeviceName.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDeviceName.Size = New System.Drawing.Size(120, 20)
    Me.edDeviceName.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(238, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(238, 8)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbUser
    '
    Me.lbUser.AutoSize = True
    Me.lbUser.BackColor = System.Drawing.Color.Transparent
    Me.lbUser.Location = New System.Drawing.Point(3, 56)
    Me.lbUser.Name = "lbUser"
    Me.lbUser.NTSDbField = ""
    Me.lbUser.Size = New System.Drawing.Size(69, 13)
    Me.lbUser.TabIndex = 3
    Me.lbUser.Text = "Nome utente"
    Me.lbUser.Tooltip = ""
    Me.lbUser.UseMnemonic = False
    '
    'lbDeviceName
    '
    Me.lbDeviceName.AutoSize = True
    Me.lbDeviceName.BackColor = System.Drawing.Color.Transparent
    Me.lbDeviceName.Location = New System.Drawing.Point(3, 30)
    Me.lbDeviceName.Name = "lbDeviceName"
    Me.lbDeviceName.NTSDbField = ""
    Me.lbDeviceName.Size = New System.Drawing.Size(87, 13)
    Me.lbDeviceName.TabIndex = 5
    Me.lbDeviceName.Text = "Nome dispositivo"
    Me.lbDeviceName.Tooltip = ""
    Me.lbDeviceName.UseMnemonic = False
    '
    'lbTipopag
    '
    Me.lbTipopag.AutoSize = True
    Me.lbTipopag.BackColor = System.Drawing.Color.Transparent
    Me.lbTipopag.Location = New System.Drawing.Point(3, 1)
    Me.lbTipopag.Name = "lbTipopag"
    Me.lbTipopag.NTSDbField = ""
    Me.lbTipopag.Size = New System.Drawing.Size(64, 13)
    Me.lbTipopag.TabIndex = 6
    Me.lbTipopag.Text = "Tipo evento"
    Me.lbTipopag.Tooltip = ""
    Me.lbTipopag.UseMnemonic = False
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.edDatScadA)
    Me.NtsPanel1.Controls.Add(Me.edDatScadDa)
    Me.NtsPanel1.Controls.Add(Me.lbDatScadA)
    Me.NtsPanel1.Controls.Add(Me.lbDatScadDa)
    Me.NtsPanel1.Controls.Add(Me.lbTipopag)
    Me.NtsPanel1.Controls.Add(Me.edDeviceName)
    Me.NtsPanel1.Controls.Add(Me.lbDeviceName)
    Me.NtsPanel1.Controls.Add(Me.cbTipo)
    Me.NtsPanel1.Controls.Add(Me.lbUser)
    Me.NtsPanel1.Controls.Add(Me.edUser)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(2, 8)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(230, 134)
    Me.NtsPanel1.TabIndex = 9
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'edDatScadA
    '
    Me.edDatScadA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatScadA.EditValue = "31/12/2099"
    Me.edDatScadA.Location = New System.Drawing.Point(100, 105)
    Me.edDatScadA.Name = "edDatScadA"
    Me.edDatScadA.NTSDbField = ""
    Me.edDatScadA.NTSForzaVisZoom = False
    Me.edDatScadA.NTSOldValue = "09/07/2006"
    Me.edDatScadA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatScadA.Properties.Appearance.Options.UseBackColor = True
    Me.edDatScadA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatScadA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatScadA.Properties.AutoHeight = False
    Me.edDatScadA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatScadA.Properties.MaxLength = 65536
    Me.edDatScadA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatScadA.Size = New System.Drawing.Size(120, 20)
    Me.edDatScadA.TabIndex = 61
    '
    'edDatScadDa
    '
    Me.edDatScadDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatScadDa.EditValue = "01/01/1900"
    Me.edDatScadDa.Location = New System.Drawing.Point(100, 79)
    Me.edDatScadDa.Name = "edDatScadDa"
    Me.edDatScadDa.NTSDbField = ""
    Me.edDatScadDa.NTSForzaVisZoom = False
    Me.edDatScadDa.NTSOldValue = "09/07/2006"
    Me.edDatScadDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatScadDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDatScadDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatScadDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatScadDa.Properties.AutoHeight = False
    Me.edDatScadDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatScadDa.Properties.MaxLength = 65536
    Me.edDatScadDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatScadDa.Size = New System.Drawing.Size(120, 20)
    Me.edDatScadDa.TabIndex = 60
    '
    'lbDatScadA
    '
    Me.lbDatScadA.AutoSize = True
    Me.lbDatScadA.BackColor = System.Drawing.Color.Transparent
    Me.lbDatScadA.Location = New System.Drawing.Point(3, 108)
    Me.lbDatScadA.Name = "lbDatScadA"
    Me.lbDatScadA.NTSDbField = ""
    Me.lbDatScadA.Size = New System.Drawing.Size(39, 13)
    Me.lbDatScadA.TabIndex = 10
    Me.lbDatScadA.Text = "A data"
    Me.lbDatScadA.Tooltip = ""
    Me.lbDatScadA.UseMnemonic = False
    '
    'lbDatScadDa
    '
    Me.lbDatScadDa.AutoSize = True
    Me.lbDatScadDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDatScadDa.Location = New System.Drawing.Point(3, 86)
    Me.lbDatScadDa.Name = "lbDatScadDa"
    Me.lbDatScadDa.NTSDbField = ""
    Me.lbDatScadDa.Size = New System.Drawing.Size(45, 13)
    Me.lbDatScadDa.TabIndex = 9
    Me.lbDatScadDa.Text = "Da data"
    Me.lbDatScadDa.Tooltip = ""
    Me.lbDatScadDa.UseMnemonic = False
    '
    'FRM__SELE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(346, 142)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SELE"
    Me.Text = "SELEZIONA"
    CType(Me.edUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDeviceName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edDatScadA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatScadDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

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

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"T", "Tutti"})
      dttTipo.Rows.Add(New Object() {"P", "Programmi"})
      dttTipo.Rows.Add(New Object() {"A", "Accessi"})
      dttTipo.AcceptChanges()

      cbTipo.DataSource = dttTipo
      cbTipo.DisplayMember = "val"
      cbTipo.ValueMember = "cod"

      cbTipo.NTSSetParam(oApp.Tr(Me, 128230023056687035, "Tipo evento"))
      edDeviceName.NTSSetParam(oMenu, oApp.Tr(Me, 128230023056530862, "Nome device"), 50, True)
      edUser.NTSSetParam(oMenu, oApp.Tr(Me, 128230023056843208, "Utente"), 20, True)
      edDatScadDa.NTSSetParam(oMenu, oApp.Tr(Me, 129431265983623047, "Da data"), False)
      edDatScadA.NTSSetParam(oMenu, oApp.Tr(Me, 129431266153623047, "A data"), False)

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub FRM__SELE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()

      edDatScadDa.Text = DateTime.Now.AddDays(-7).ToShortDateString
      edDatScadA.Text = DateTime.Now.ToShortDateString

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try

      bOk = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
