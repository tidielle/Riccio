Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DIPE

#Region "Variabili"
  Public oCleGreg As CLE__GREG
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents pnDipe As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents edFiltro2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFiltro1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFiltro0 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbfiltri2 As NTSInformatica.NTSLabel
  Public WithEvents lbfiltri1 As NTSInformatica.NTSLabel
  Public WithEvents lbfiltri0 As NTSInformatica.NTSLabel
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.pnDipe = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.edFiltro2 = New NTSInformatica.NTSTextBoxStr
    Me.edFiltro1 = New NTSInformatica.NTSTextBoxStr
    Me.edFiltro0 = New NTSInformatica.NTSTextBoxStr
    Me.lbfiltri2 = New NTSInformatica.NTSLabel
    Me.lbfiltri1 = New NTSInformatica.NTSLabel
    Me.lbfiltri0 = New NTSInformatica.NTSLabel
    CType(Me.pnDipe, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDipe.SuspendLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.edFiltro2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFiltro1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFiltro0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnDipe
    '
    Me.pnDipe.AllowDrop = True
    Me.pnDipe.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDipe.Appearance.Options.UseBackColor = True
    Me.pnDipe.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDipe.Controls.Add(Me.pnLeft)
    Me.pnDipe.Controls.Add(Me.cmdAnnulla)
    Me.pnDipe.Controls.Add(Me.cmdOK)
    Me.pnDipe.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDipe.Location = New System.Drawing.Point(-3, -3)
    Me.pnDipe.Name = "pnDipe"
    Me.pnDipe.Size = New System.Drawing.Size(382, 103)
    Me.pnDipe.TabIndex = 0
    Me.pnDipe.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(279, 44)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(90, 29)
    Me.cmdAnnulla.TabIndex = 7
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(279, 15)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(90, 29)
    Me.cmdOK.TabIndex = 6
    Me.cmdOK.Text = "&OK"
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.edFiltro2)
    Me.pnLeft.Controls.Add(Me.edFiltro1)
    Me.pnLeft.Controls.Add(Me.edFiltro0)
    Me.pnLeft.Controls.Add(Me.lbfiltri2)
    Me.pnLeft.Controls.Add(Me.lbfiltri1)
    Me.pnLeft.Controls.Add(Me.lbfiltri0)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Location = New System.Drawing.Point(3, 3)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.Size = New System.Drawing.Size(270, 100)
    Me.pnLeft.TabIndex = 8
    Me.pnLeft.Text = "NtsPanel1"
    '
    'edFiltro2
    '
    Me.edFiltro2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFiltro2.Location = New System.Drawing.Point(116, 66)
    Me.edFiltro2.Name = "edFiltro2"
    Me.edFiltro2.NTSDbField = ""
    Me.edFiltro2.NTSForzaVisZoom = False
    Me.edFiltro2.NTSOldValue = ""
    Me.edFiltro2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFiltro2.Properties.MaxLength = 65536
    Me.edFiltro2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFiltro2.Size = New System.Drawing.Size(135, 20)
    Me.edFiltro2.TabIndex = 11
    '
    'edFiltro1
    '
    Me.edFiltro1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFiltro1.Location = New System.Drawing.Point(116, 40)
    Me.edFiltro1.Name = "edFiltro1"
    Me.edFiltro1.NTSDbField = ""
    Me.edFiltro1.NTSForzaVisZoom = False
    Me.edFiltro1.NTSOldValue = ""
    Me.edFiltro1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFiltro1.Properties.MaxLength = 65536
    Me.edFiltro1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFiltro1.Size = New System.Drawing.Size(135, 20)
    Me.edFiltro1.TabIndex = 10
    '
    'edFiltro0
    '
    Me.edFiltro0.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edFiltro0.Location = New System.Drawing.Point(116, 14)
    Me.edFiltro0.Name = "edFiltro0"
    Me.edFiltro0.NTSDbField = ""
    Me.edFiltro0.NTSForzaVisZoom = False
    Me.edFiltro0.NTSOldValue = ""
    Me.edFiltro0.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFiltro0.Properties.MaxLength = 65536
    Me.edFiltro0.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFiltro0.Size = New System.Drawing.Size(135, 20)
    Me.edFiltro0.TabIndex = 9
    '
    'lbfiltri2
    '
    Me.lbfiltri2.AutoSize = True
    Me.lbfiltri2.BackColor = System.Drawing.Color.Transparent
    Me.lbfiltri2.Location = New System.Drawing.Point(19, 69)
    Me.lbfiltri2.Name = "lbfiltri2"
    Me.lbfiltri2.NTSDbField = ""
    Me.lbfiltri2.Size = New System.Drawing.Size(88, 13)
    Me.lbfiltri2.TabIndex = 8
    Me.lbfiltri2.Text = "Tipo Documento:"
    '
    'lbfiltri1
    '
    Me.lbfiltri1.AutoSize = True
    Me.lbfiltri1.BackColor = System.Drawing.Color.Transparent
    Me.lbfiltri1.Location = New System.Drawing.Point(19, 43)
    Me.lbfiltri1.Name = "lbfiltri1"
    Me.lbfiltri1.NTSDbField = ""
    Me.lbfiltri1.Size = New System.Drawing.Size(84, 13)
    Me.lbfiltri1.TabIndex = 7
    Me.lbfiltri1.Text = "Codice Azienda:"
    '
    'lbfiltri0
    '
    Me.lbfiltri0.AutoSize = True
    Me.lbfiltri0.BackColor = System.Drawing.Color.Transparent
    Me.lbfiltri0.Location = New System.Drawing.Point(19, 17)
    Me.lbfiltri0.Name = "lbfiltri0"
    Me.lbfiltri0.NTSDbField = ""
    Me.lbfiltri0.Size = New System.Drawing.Size(91, 13)
    Me.lbfiltri0.TabIndex = 6
    Me.lbfiltri0.Text = "Nome Operatore:"
    '
    'FRM__DIPE
    '
    Me.ClientSize = New System.Drawing.Size(378, 99)
    Me.Controls.Add(Me.pnDipe)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__DIPE"
    Me.Text = "Inserimento Filtri"
    CType(Me.pnDipe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDipe.ResumeLayout(False)
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.edFiltro2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFiltro1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFiltro0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edFiltro2.NTSSetParam(oMenu, oApp.Tr(Me, 128333079887631152, "Tipo Documento:"), 0)
      edFiltro1.NTSSetParam(oMenu, oApp.Tr(Me, 128333079887475292, "Codice Azienda:"), 0)
      edFiltro0.NTSSetParam(oMenu, oApp.Tr(Me, 128333079887319432, "Nome Operatore:"), 0)

      edFiltro0.NTSSetParamZoom("ZOOMOPERAT")
      edFiltro1.NTSSetParamZoom("ZOOMTABANAZ")
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
  Public Overridable Sub InitEntity(ByRef cleGreg As CLE__GREG)
    oCleGreg = cleGreg
    AddHandler oCleGreg.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__DIPE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edFiltro0.Text = oCleGreg.strGregNomOp
      edFiltro1.Text = oCleGreg.strGregCodAz
      edFiltro2.Text = oCleGreg.strGregTipoDoc

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Try
      oCleGreg.strGregNomOp = (edFiltro0.Text).ToUpper
      oCleGreg.strGregCodAz = (edFiltro1.Text).ToUpper
      oCleGreg.strGregTipoDoc = (edFiltro2.Text).ToUpper
      Me.Close()
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class