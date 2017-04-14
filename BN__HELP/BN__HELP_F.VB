Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports mshtml

Public Class FRM__HELP_F
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
  Public oCallParams As CLE__CLDP
#End Region

#Region "Controlli e InitializeComponent"
  Public WithEvents pnTop As NTSPanel
  Public WithEvents cmdPrint As NTSButton
  Public WithEvents cmdHome As NTSButton
  Public WithEvents cmdSuc As NTSButton
  Public WithEvents cmdPrec As NTSButton
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents tsView As NTSInformatica.NTSTabControl
  Public WithEvents TabPageView0 As NTSInformatica.NTSTabPage
  Public WithEvents pnWebFill As NTSInformatica.NTSPanel
  Public WithEvents WebBrows As NTSInformatica.NTSWebBrowser
  Public WithEvents lbFindInPage As System.Windows.Forms.Label

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HELP_F))
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbFindInPage = New System.Windows.Forms.Label
    Me.cmdPrint = New NTSInformatica.NTSButton
    Me.cmdHome = New NTSInformatica.NTSButton
    Me.cmdSuc = New NTSInformatica.NTSButton
    Me.cmdPrec = New NTSInformatica.NTSButton
    Me.pnFill = New NTSInformatica.NTSPanel
    Me.tsView = New NTSInformatica.NTSTabControl
    Me.TabPageView0 = New NTSInformatica.NTSTabPage
    Me.pnWebFill = New NTSInformatica.NTSPanel
    Me.WebBrows = New NTSInformatica.NTSWebBrowser
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
    CType(Me.tsView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsView.SuspendLayout()
    Me.TabPageView0.SuspendLayout()
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbFindInPage)
    Me.pnTop.Controls.Add(Me.cmdPrint)
    Me.pnTop.Controls.Add(Me.cmdHome)
    Me.pnTop.Controls.Add(Me.cmdSuc)
    Me.pnTop.Controls.Add(Me.cmdPrec)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(733, 37)
    Me.pnTop.TabIndex = 2
    '
    'lbFindInPage
    '
    Me.lbFindInPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbFindInPage.AutoSize = True
    Me.lbFindInPage.Location = New System.Drawing.Point(372, 21)
    Me.lbFindInPage.Name = "lbFindInPage"
    Me.lbFindInPage.Size = New System.Drawing.Size(254, 13)
    Me.lbFindInPage.TabIndex = 16
    Me.lbFindInPage.Text = "Per ricercare all'interno della pagina premere Ctrl+F"
    '
    'cmdPrint
    '
    Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
    Me.cmdPrint.ImageText = ""
    Me.cmdPrint.Location = New System.Drawing.Point(117, 1)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(35, 35)
    Me.cmdPrint.TabIndex = 4
    Me.cmdPrint.TabStop = False
    Me.cmdPrint.ToolTip = "Stampa - Lancia la stampa dell'argomento attualmente visualizzato in finestra"
    '
    'cmdHome
    '
    Me.cmdHome.Image = CType(resources.GetObject("cmdHome.Image"), System.Drawing.Image)
    Me.cmdHome.ImageText = ""
    Me.cmdHome.Location = New System.Drawing.Point(3, 1)
    Me.cmdHome.Name = "cmdHome"
    Me.cmdHome.Size = New System.Drawing.Size(35, 35)
    Me.cmdHome.TabIndex = 2
    Me.cmdHome.TabStop = False
    Me.cmdHome.ToolTip = "Homepage"
    '
    'cmdSuc
    '
    Me.cmdSuc.Image = CType(resources.GetObject("cmdSuc.Image"), System.Drawing.Image)
    Me.cmdSuc.ImageText = ""
    Me.cmdSuc.Location = New System.Drawing.Point(79, 1)
    Me.cmdSuc.Name = "cmdSuc"
    Me.cmdSuc.Size = New System.Drawing.Size(35, 35)
    Me.cmdSuc.TabIndex = 1
    Me.cmdSuc.TabStop = False
    Me.cmdSuc.ToolTip = "Successivo"
    '
    'cmdPrec
    '
    Me.cmdPrec.Image = CType(resources.GetObject("cmdPrec.Image"), System.Drawing.Image)
    Me.cmdPrec.ImageText = ""
    Me.cmdPrec.Location = New System.Drawing.Point(41, 1)
    Me.cmdPrec.Name = "cmdPrec"
    Me.cmdPrec.Size = New System.Drawing.Size(35, 35)
    Me.cmdPrec.TabIndex = 0
    Me.cmdPrec.TabStop = False
    Me.cmdPrec.ToolTip = "Precedente"
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.tsView)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(0, 37)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.NTSActiveTrasparency = True
    Me.pnFill.Size = New System.Drawing.Size(733, 429)
    Me.pnFill.TabIndex = 7
    Me.pnFill.Text = "NtsPanel2"
    '
    'tsView
    '
    Me.tsView.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsView.Location = New System.Drawing.Point(0, 0)
    Me.tsView.Name = "tsView"
    Me.tsView.SelectedTabPage = Me.TabPageView0
    Me.tsView.Size = New System.Drawing.Size(733, 429)
    Me.tsView.TabIndex = 1
    Me.tsView.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPageView0})
    Me.tsView.Text = "NtsTabControl1"
    '
    'TabPageView0
    '
    Me.TabPageView0.AllowDrop = True
    Me.TabPageView0.Controls.Add(Me.pnWebFill)
    Me.TabPageView0.Enable = True
    Me.TabPageView0.Name = "TabPageView0"
    Me.TabPageView0.Size = New System.Drawing.Size(724, 399)
    Me.TabPageView0.Text = "FRIENDLY Help"
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
    Me.pnWebFill.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnWebFill.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnWebFill.Name = "pnWebFill"
    Me.pnWebFill.NTSActiveTrasparency = True
    Me.pnWebFill.Size = New System.Drawing.Size(724, 399)
    Me.pnWebFill.TabIndex = 1
    Me.pnWebFill.Text = "NtsPanel2"
    '
    'WebBrows
    '
    Me.WebBrows.Dock = System.Windows.Forms.DockStyle.Fill
    Me.WebBrows.IsWebBrowserContextMenuEnabled = False
    Me.WebBrows.Location = New System.Drawing.Point(0, 0)
    Me.WebBrows.MinimumSize = New System.Drawing.Size(20, 20)
    Me.WebBrows.Name = "WebBrows"
    Me.WebBrows.ScriptErrorsSuppressed = True
    Me.WebBrows.Size = New System.Drawing.Size(724, 399)
    Me.WebBrows.TabIndex = 0
    '
    'FRM__HELP_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(733, 466)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.pnTop)
    Me.Name = "FRM__HELP_F"
    Me.ShowInTaskbar = True
    Me.Text = "FRIENDLY HELP"
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    CType(Me.tsView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsView.ResumeLayout(False)
    Me.TabPageView0.ResumeLayout(False)
    CType(Me.pnWebFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnWebFill.ResumeLayout(False)
    Me.ResumeLayout(False)

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


    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__HELP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()
      NTSKeyUpF1Disable()
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__HELP_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      Dim strPagina As String = ""
      If Not oCallParams Is Nothing Then
        If Not oCallParams.strParam Is Nothing Then
          strPagina = oCallParams.strParam.Replace(".htm", "").Replace("--", "__")
        End If
      End If
      GoPage(strPagina)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub WebBrows_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrows.Navigated
    Try
      cmdSuc.Enabled = True
      cmdPrec.Enabled = True
      If WebBrows.CanGoBack = False Then cmdPrec.Enabled = False
      If WebBrows.CanGoForward = False Then cmdSuc.Enabled = False
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub GoPage(ByVal strPage As String)
    Try
      Select Case strPage.ToLower
        Case "bs__abia"
        Case "bs__abic"
        Case "bs__anaz"
        Case "bs__cate"
        Case "bscgatne"
        Case "bscgauto"
        Case "bscgbice"
        Case "bscgbilc"
        Case "bscgcaes"
        Case "bscgcauc"
        Case "bscgckpa"
        Case "bscgdcst"
        Case "bscgdist"
        Case "bscgestr"
        Case "bscggnef"
        Case "bscggrat"
        Case "bscgiepa"
        Case "bscginso"
        Case "bscglian"
        Case "bscglipe"
        Case "bscgliqi"
        Case "bscgpart"
        Case "bscgpecg"
        Case "bscgprin"
        Case "bscgpriv"
        Case "bscgriel"
        Case "bscgrnpr"
        Case "bscgscad"
        Case "bscgsepn"
        Case "bscgsesp"
        Case "bscgstlg"
        Case "bscgstpn"
        Case "bscgstri"
        Case "bscgstsc"
        Case "bscgvard"
        Case "bs__civa"
        Case "bs__clie"
        Case "bs__cpar"
        Case "bs__cpcl"
        Case "bs__csar"
        Case "bs__cscl"
        Case "bs__ctmp"
        Case "bsdkkons"
        Case "bs__dupa"
        Case "bsdwgend"
        Case "bsdwqvda"
        Case "bsf4gest"
        Case "bs__fldo"
        Case "bs__gcap"
        Case "bs__hlan"
        Case "bsidgeid"
        Case "bsidstid"
        Case "bsieimpo"
        Case "bs__istf"
        Case "bs__istp"
        Case "bs__ling"
        Case "bs__lsel"
        Case "bs__mast"
          'Case "bs__menu"
        Case "bsmgagad"
        Case "bsmgarti"
        Case "bsmgcaar"
        Case "bsmgcaem"
        Case "bsmgcals"
        Case "bsmgcaum"
        Case "bsmgcfam"
        Case "bsmgcmma"
        Case "bsmgdpro"
        Case "bsmgelar"
        Case "bsmgette"
        Case "bsmggbar"
        Case "bsmggmer"
        Case "bsmggsad"
        Case "bsmggsap"
        Case "bsmggsax"
        Case "bsmggsay"
        Case "bsmghlar"
        Case "bsmginvf"
        Case "bsmglist"
        Case "bsmglsar"
        Case "bsmgmaga"
        Case "bsmgpdon"
        Case "bsmgpeac"
        Case "bsmgprom"
        Case "bsmgrcap"
        Case "bsmgschc"
        Case "bsmgsche"
        Case "bsmgsctr"
        Case "bsmgsgme"
        Case "bsmgstbo"
        Case "bsmgstli"
        Case "bsmgstrl"
        Case "bsmgterm"
        Case "bsmgumis"
        Case "bsmgvaar"
        Case "bs__nume"
        Case "bs__numg"
        Case "bsorcaor"
        Case "bsorchiu"
        Case "bsorconf"
        Case "bsorgsor"
        Case "bsorscho"
        Case "bsorsita"
        Case "bsorstor"
        Case "bs__paga"
        Case "bspdgenp"
        Case "bsprcage"
        Case "bsprcapr"
        Case "bsprclpr"
        Case "bsprelmp"
        Case "bsprenas"
        Case "bsprfapa"
        Case "bsprgnpv"
        Case "bsprgspv"
        Case "bsprscpr"
        Case "bsprstpv"
        Case "bs__scop"
        Case "bssmsmel"
        Case "bssmstel"
        Case "bs__sotc"
        Case "bs__spaf"
        Case "bs__spar"
        Case "bs__stat"
        Case "bs__stwo"
        Case "bs__vaan"
        Case "bsveaspe"
        Case "bsvebanc"
        Case "bsveboll"
        Case "bsvebotr"
        Case "bsveclsc"
        Case "bsvecove"
        Case "bsvefadi"
        Case "bsvefdin"
        Case "bsvepeve"
        Case "bsvepnfa"
        Case "bsveport"
        Case "bsvetpbf"
        Case "bsvevalu"
        Case "bsvevett"
        Case "bs__zone"
        Case "bscgnufo"
        Case "bs__cedi"
        Case "bsmgrear"
        Case "bsefspes"
        Case "bsf4gest"
        Case "bscftrib"
        Case Else
          strPage = ""
      End Select

      If strPage = "" Then
        strPage = "http://www.softwarefriendly.com/help/"
      Else
        strPage = "http://www.softwarefriendly.com/faqs/" & strPage & "/"
      End If
      WebBrows.Navigate(strPage)
      Try
        NTSProcessStart("chrome", strPage)
      Catch ex As Exception
        Try
          NTSProcessStart("firefox", strPage)
        Catch ex1 As Exception
          Try
            NTSProcessStart("Iexplore", strPage)
          Catch ex2 As Exception
            NTSProcessStart(strPage, "")
          End Try
        End Try
      End Try
      Me.Close()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

 

  Public Overridable Sub cmdHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHome.Click
    Try
      GoPage("")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdPrec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrec.Click
    Try
      WebBrows.GoBack()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdSuc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSuc.Click
    Try
      WebBrows.GoForward()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Try
      WebBrows.ShowPrintDialog()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


End Class