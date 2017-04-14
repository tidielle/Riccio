Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports mshtml

Public Class FRM__HREC
  Inherits FRM__CHIL

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Variabili e oggetti"
  Public oPar As CLE__CLDP
  Public docHTML As IHTMLDocument2
#End Region

#Region "Controlli e InitializeComponent"
  Public WithEvents pnLeft As NTSPanel
  Public WithEvents pnFill As NTSPanel
  Public WithEvents WebBrows As NTSInformatica.NTSWebBrowser
  Public WithEvents NtsSplitter1 As NTSInformatica.NTSSplitter
  Private components As System.ComponentModel.IContainer
  Public WithEvents cmdRecupera As NTSInformatica.NTSButton
  Public WithEvents liTrova As NTSInformatica.NTSListBox
  Public WithEvents lbInfo As System.Windows.Forms.Label
  Public WithEvents pnWebFill As NTSInformatica.NTSPanel

  Public Overridable Sub InitializeComponent()
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.liTrova = New NTSInformatica.NTSListBox
    Me.cmdRecupera = New NTSInformatica.NTSButton
    Me.pnFill = New NTSInformatica.NTSPanel
    Me.pnWebFill = New NTSInformatica.NTSPanel
    Me.WebBrows = New NTSInformatica.NTSWebBrowser
    Me.NtsSplitter1 = New NTSInformatica.NTSSplitter
    Me.lbInfo = New System.Windows.Forms.Label
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.liTrova, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
    CType(Me.pnWebFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnWebFill.SuspendLayout()
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
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.lbInfo)
    Me.pnLeft.Controls.Add(Me.liTrova)
    Me.pnLeft.Controls.Add(Me.cmdRecupera)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.Size = New System.Drawing.Size(319, 470)
    Me.pnLeft.TabIndex = 3
    '
    'liTrova
    '
    Me.liTrova.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.liTrova.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTrova.ItemHeight = 14
    Me.liTrova.Location = New System.Drawing.Point(0, 56)
    Me.liTrova.Name = "liTrova"
    Me.liTrova.NTSDbField = ""
    Me.liTrova.Size = New System.Drawing.Size(319, 372)
    Me.liTrova.TabIndex = 8
    '
    'cmdRecupera
    '
    Me.cmdRecupera.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdRecupera.ImageText = ""
    Me.cmdRecupera.Location = New System.Drawing.Point(13, 436)
    Me.cmdRecupera.Name = "cmdRecupera"
    Me.cmdRecupera.Size = New System.Drawing.Size(99, 24)
    Me.cmdRecupera.TabIndex = 5
    Me.cmdRecupera.TabStop = False
    Me.cmdRecupera.Text = "Recupera HTML"
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.pnWebFill)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(322, 0)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.Size = New System.Drawing.Size(574, 470)
    Me.pnFill.TabIndex = 4
    '
    'pnWebFill
    '
    Me.pnWebFill.AllowDrop = True
    Me.pnWebFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnWebFill.Appearance.Options.UseBackColor = True
    Me.pnWebFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnWebFill.Controls.Add(Me.WebBrows)
    Me.pnWebFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnWebFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnWebFill.Location = New System.Drawing.Point(0, 0)
    Me.pnWebFill.Name = "pnWebFill"
    Me.pnWebFill.Size = New System.Drawing.Size(574, 470)
    Me.pnWebFill.TabIndex = 9
    Me.pnWebFill.Text = "NtsPanel1"
    '
    'WebBrows
    '
    Me.WebBrows.Dock = System.Windows.Forms.DockStyle.Fill
    Me.WebBrows.IsWebBrowserContextMenuEnabled = False
    Me.WebBrows.Location = New System.Drawing.Point(0, 0)
    Me.WebBrows.MinimumSize = New System.Drawing.Size(20, 20)
    Me.WebBrows.Name = "WebBrows"
    Me.WebBrows.ScriptErrorsSuppressed = True
    Me.WebBrows.Size = New System.Drawing.Size(574, 470)
    Me.WebBrows.TabIndex = 0
    '
    'NtsSplitter1
    '
    Me.NtsSplitter1.BackColor = System.Drawing.SystemColors.ControlDark
    Me.NtsSplitter1.Location = New System.Drawing.Point(319, 0)
    Me.NtsSplitter1.Name = "NtsSplitter1"
    Me.NtsSplitter1.Size = New System.Drawing.Size(3, 470)
    Me.NtsSplitter1.TabIndex = 1
    Me.NtsSplitter1.TabStop = False
    '
    'lbInfo
    '
    Me.lbInfo.Location = New System.Drawing.Point(12, 9)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.Size = New System.Drawing.Size(295, 39)
    Me.lbInfo.TabIndex = 17
    Me.lbInfo.Text = "Scegliere un elemento dalla lista sotto, la lista è ordinata dal più vecchio al p" & _
        "iù recente, quindi l'ultimo elemento della lista è l'ultimo salvato"
    '
    'FRM__HREC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(896, 470)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.NtsSplitter1)
    Me.Controls.Add(Me.pnLeft)
    Me.Name = "FRM__HREC"
    Me.ShowInTaskbar = True
    Me.Text = "RECUPERA HTML"
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    CType(Me.liTrova, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    CType(Me.pnWebFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnWebFill.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
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

    Return True

  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      liTrova.NTSSetParam(oApp.Tr(Me, 129640269596308594, "Lista risultati"))

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Dim dirInfo As DirectoryInfo
  Dim fileInfoToRec As FileInfo

  Public Overridable Sub FRM__HELP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      InitControls()

      GctlSetRoules()
      cmdRecupera.Enabled = False

      dirInfo = New DirectoryInfo(oApp.AscDir & "\help\")
      fileInfoToRec = New FileInfo(oPar.strPar1)

      For Each fileInfo As FileInfo In dirInfo.GetFiles()
        If fileInfo.FullName.ToUpper.Contains(fileInfoToRec.Name.ToUpper) Then
          liTrova.Items.Add(fileInfo.FullName)
        End If
      Next
      If liTrova.Items.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129640330987480469, "Non ci sono HTML recuperabili."))
        Me.Close()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdRecupera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecupera.Click
    Try
      oPar.bPar1 = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub liTrova_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles liTrova.Click
    Try
      If liTrova.SelectedItem Is Nothing Then Return
      Dim fileRec As New FileInfo(liTrova.SelectedItem.ToString)
      oPar.strPar2 = fileInfoToRec.DirectoryName & "\recupera.htm"
      fileRec.CopyTo(fileInfoToRec.DirectoryName & "\recupera.htm", True)
      WebBrows.Navigate(fileInfoToRec.DirectoryName & "\recupera.htm")
      cmdRecupera.Enabled = True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class
