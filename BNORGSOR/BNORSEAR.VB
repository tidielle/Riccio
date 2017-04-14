Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEAR

#Region "Variabili"
  Public oCleGsor As CLEORGSOR
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer


  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents lbColli As NTSInformatica.NTSLabel
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edColli As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbUnmis As NTSInformatica.NTSComboBox
  Public WithEvents edDatavalDB As NTSInformatica.NTSTextBoxData
  Public WithEvents pnCoar As NTSInformatica.NTSPanel

  Public bOk As Boolean = False
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnCoar = New NTSInformatica.NTSPanel
    Me.edDatavalDB = New NTSInformatica.NTSTextBoxData
    Me.cbUnmis = New NTSInformatica.NTSComboBox
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edColli = New NTSInformatica.NTSTextBoxNum
    Me.lbColli = New NTSInformatica.NTSLabel
    Me.lbData = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbCodart = New NTSInformatica.NTSLabel
    CType(Me.pnCoar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCoar.SuspendLayout()
    CType(Me.edDatavalDB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbUnmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edColli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(279, 35)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(93, 24)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(279, 9)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(93, 24)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Ok"
    '
    'pnCoar
    '
    Me.pnCoar.AllowDrop = True
    Me.pnCoar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCoar.Appearance.Options.UseBackColor = True
    Me.pnCoar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCoar.Controls.Add(Me.edDatavalDB)
    Me.pnCoar.Controls.Add(Me.cbUnmis)
    Me.pnCoar.Controls.Add(Me.edFase)
    Me.pnCoar.Controls.Add(Me.edColli)
    Me.pnCoar.Controls.Add(Me.lbColli)
    Me.pnCoar.Controls.Add(Me.lbData)
    Me.pnCoar.Controls.Add(Me.lbFase)
    Me.pnCoar.Controls.Add(Me.edCodart)
    Me.pnCoar.Controls.Add(Me.lbCodart)
    Me.pnCoar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCoar.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCoar.Location = New System.Drawing.Point(0, 0)
    Me.pnCoar.Name = "pnCoar"
    Me.pnCoar.Size = New System.Drawing.Size(263, 115)
    Me.pnCoar.TabIndex = 22
    Me.pnCoar.Text = "NtsPanel1"
    '
    'edDatavalDB
    '
    Me.edDatavalDB.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatavalDB.EditValue = "01/01/2011"
    Me.edDatavalDB.Location = New System.Drawing.Point(107, 61)
    Me.edDatavalDB.Name = "edDatavalDB"
    Me.edDatavalDB.NTSDbField = ""
    Me.edDatavalDB.NTSForzaVisZoom = False
    Me.edDatavalDB.NTSOldValue = ""
    Me.edDatavalDB.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatavalDB.Properties.Appearance.Options.UseBackColor = True
    Me.edDatavalDB.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatavalDB.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatavalDB.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatavalDB.Properties.MaxLength = 65536
    Me.edDatavalDB.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatavalDB.Size = New System.Drawing.Size(128, 20)
    Me.edDatavalDB.TabIndex = 601
    '
    'cbUnmis
    '
    Me.cbUnmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbUnmis.DataSource = Nothing
    Me.cbUnmis.DisplayMember = ""
    Me.cbUnmis.Location = New System.Drawing.Point(107, 87)
    Me.cbUnmis.Name = "cbUnmis"
    Me.cbUnmis.NTSDbField = ""
    Me.cbUnmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbUnmis.Properties.DropDownRows = 30
    Me.cbUnmis.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbUnmis.SelectedValue = ""
    Me.cbUnmis.Size = New System.Drawing.Size(61, 20)
    Me.cbUnmis.TabIndex = 600
    Me.cbUnmis.ValueMember = ""
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.EditValue = "0"
    Me.edFase.Location = New System.Drawing.Point(107, 35)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(61, 20)
    Me.edFase.TabIndex = 511
    '
    'edColli
    '
    Me.edColli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edColli.EditValue = "0"
    Me.edColli.Location = New System.Drawing.Point(174, 87)
    Me.edColli.Name = "edColli"
    Me.edColli.NTSDbField = ""
    Me.edColli.NTSFormat = "0"
    Me.edColli.NTSForzaVisZoom = False
    Me.edColli.NTSOldValue = ""
    Me.edColli.Properties.Appearance.Options.UseTextOptions = True
    Me.edColli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edColli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edColli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edColli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edColli.Properties.MaxLength = 65536
    Me.edColli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edColli.Size = New System.Drawing.Size(61, 20)
    Me.edColli.TabIndex = 510
    '
    'lbColli
    '
    Me.lbColli.AutoSize = True
    Me.lbColli.BackColor = System.Drawing.Color.Transparent
    Me.lbColli.Location = New System.Drawing.Point(12, 90)
    Me.lbColli.Name = "lbColli"
    Me.lbColli.NTSDbField = ""
    Me.lbColli.Size = New System.Drawing.Size(26, 13)
    Me.lbColli.TabIndex = 4
    Me.lbColli.Text = "Colli"
    Me.lbColli.Tooltip = ""
    Me.lbColli.UseMnemonic = False
    '
    'lbData
    '
    Me.lbData.AutoSize = True
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.Location = New System.Drawing.Point(12, 64)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(83, 13)
    Me.lbData.TabIndex = 3
    Me.lbData.Text = "Data validità DB"
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(12, 38)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(68, 13)
    Me.lbFase.TabIndex = 2
    Me.lbFase.Text = "Fase articolo"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(107, 9)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(128, 20)
    Me.edCodart.TabIndex = 1
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(12, 12)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(77, 13)
    Me.lbCodart.TabIndex = 0
    Me.lbCodart.Text = "Codice articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'FRMORSEAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(383, 115)
    Me.Controls.Add(Me.pnCoar)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORSEAR"
    Me.Text = "SELEZIONA DB"
    CType(Me.pnCoar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCoar.ResumeLayout(False)
    Me.pnCoar.PerformLayout()
    CType(Me.edDatavalDB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbUnmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edColli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR)
    oCleGsor = cleGsor
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try


      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128553203421875805, "Codice articolo"), tabartico, True)
      edFase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129502948888681641, "Fase articolo"), tabartfasi)
      edFase.ArtiPerFase = edCodart
      edDatavalDB.NTSSetParam(oMenu, oApp.Tr(Me, 129502958353564453, "Data validità DB"), False)
      cbUnmis.NTSSetParam(oApp.Tr(Me, 129502949856738281, "Unità di misura colli"))
      edColli.NTSSetParam(oMenu, oApp.Tr(Me, 129502949355468750, "Colli"), oApp.FormatQta)

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
  Public Overridable Sub FRMORSEAR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()
      GctlSetRoules()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMORSEAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      Me.ValidaLastControl()
      If edCodart.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129502955432968750, "Indicare il codice distinta base"))
        Return
      End If

      If NTSCDec(edColli.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129503477652626953, "Indicare una quantità diversa da 0"))
        Return
      End If

      bOk = True
      Me.Close()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bOk = False
    Me.Close()
  End Sub

  Public Overridable Sub Zoom()
    Try
      If edFase.ContainsFocus Then
        '------------------------------------
        'zoom fasi articoli
        Dim oParam As New CLE__PATB
        NTSZOOM.strIn = edFase.Text
        oParam.strTipo = edCodart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edFase.Text Then edFase.Text = NTSZOOM.strIn
      Else
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodart_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodart.Leave
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim dttUm As New DataTable
    Try
      If edCodart.Text.Trim = "" Then Return

      If Not oMenu.ValCodiceDb(edCodart.Text.Trim, DittaCorrente, "ARTICO", "S") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129502955971601563, "Codice distinta base inesistente"))
        edCodart.Text = ""
        Return
      End If

      strTmp = oCleGsor.GetArticoUnMis(edCodart.Text)
      strT = strTmp.Split("§"c)
      dttUm.Columns.Add("tb_codumis", GetType(String))
      For i As Integer = 0 To strT.Length - 1
        dttUm.Rows.Add(New Object() {strT(i)})
      Next
      dttUm.AcceptChanges()

      cbUnmis.DataSource = dttUm
      cbUnmis.ValueMember = "tb_codumis"
      cbUnmis.DisplayMember = "tb_codumis"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class