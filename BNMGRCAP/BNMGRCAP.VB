#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGRCAP

#Region "Moduli"
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
#End Region

#Region "Variabili"
  Public oCleRcap As CLEMGRCAP
  Public dsRcap As DataSet
  Public dcRcap As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents pnRcap As NTSInformatica.NTSPanel
  Public WithEvents ckRicreakeymag As NTSInformatica.NTSCheckBox
  Public WithEvents fmInformazioni As NTSInformatica.NTSGroupBox
  Public WithEvents lbDesescomp As NTSInformatica.NTSLabel
  Public WithEvents lbDtulap As NTSInformatica.NTSLabel
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents edDtulap As NTSInformatica.NTSTextBoxData
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbGeneraBub As NTSInformatica.NTSBarButtonItem
  'Private components As System.ComponentModel.IContainer
  'Private components As System.ComponentModel.IContainer
  'Private components As System.ComponentModel.IContainer
  Public WithEvents edEscomp As NTSInformatica.NTSTextBoxNum
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
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGRCAP", "BEMGRCAP", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleRcap = CType(objTmp, CLEMGRCAP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGRCAP", strRemoteServer, strRemotePort)
    AddHandler oCleRcap.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleRcap.Init(oApp, NTSScript, oMenu.oCleComm, "tabanaz", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGRCAP))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnRcap = New NTSInformatica.NTSPanel
    Me.ckRicreakeymag = New NTSInformatica.NTSCheckBox
    Me.fmInformazioni = New NTSInformatica.NTSGroupBox
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    Me.edDtulap = New NTSInformatica.NTSTextBoxData
    Me.lbDtulap = New NTSInformatica.NTSLabel
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.lbDesescomp = New NTSInformatica.NTSLabel
    Me.tlbGeneraBub = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRcap, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRcap.SuspendLayout()
    CType(Me.ckRicreakeymag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmInformazioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmInformazioni.SuspendLayout()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtulap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(30, 56)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbGeneraBub})
    Me.NtsBarManager1.MaxItemId = 7
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraBub)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnRcap
    '
    Me.pnRcap.AllowDrop = True
    Me.pnRcap.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRcap.Appearance.Options.UseBackColor = True
    Me.pnRcap.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRcap.Controls.Add(Me.ckRicreakeymag)
    Me.pnRcap.Controls.Add(Me.fmInformazioni)
    Me.pnRcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRcap.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnRcap.Location = New System.Drawing.Point(0, 30)
    Me.pnRcap.Name = "pnRcap"
    Me.pnRcap.NTSActiveTrasparency = True
    Me.pnRcap.Size = New System.Drawing.Size(444, 138)
    Me.pnRcap.TabIndex = 0
    Me.pnRcap.Text = "NtsPanel1"
    '
    'ckRicreakeymag
    '
    Me.ckRicreakeymag.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRicreakeymag.Location = New System.Drawing.Point(15, 108)
    Me.ckRicreakeymag.Name = "ckRicreakeymag"
    Me.ckRicreakeymag.NTSCheckValue = "S"
    Me.ckRicreakeymag.NTSUnCheckValue = "N"
    Me.ckRicreakeymag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRicreakeymag.Properties.Appearance.Options.UseBackColor = True
    Me.ckRicreakeymag.Properties.AutoHeight = False
    Me.ckRicreakeymag.Properties.Caption = "&Ricrea tabelle Keymag, Keyord, Keyprb"
    Me.ckRicreakeymag.Size = New System.Drawing.Size(220, 18)
    Me.ckRicreakeymag.TabIndex = 122
    '
    'fmInformazioni
    '
    Me.fmInformazioni.AllowDrop = True
    Me.fmInformazioni.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmInformazioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmInformazioni.Appearance.Options.UseBackColor = True
    Me.fmInformazioni.Controls.Add(Me.edEscomp)
    Me.fmInformazioni.Controls.Add(Me.edDtulap)
    Me.fmInformazioni.Controls.Add(Me.lbDtulap)
    Me.fmInformazioni.Controls.Add(Me.lbEscomp)
    Me.fmInformazioni.Controls.Add(Me.lbDesescomp)
    Me.fmInformazioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmInformazioni.Location = New System.Drawing.Point(7, 6)
    Me.fmInformazioni.Name = "fmInformazioni"
    Me.fmInformazioni.Size = New System.Drawing.Size(429, 94)
    Me.fmInformazioni.TabIndex = 121
    Me.fmInformazioni.Text = "Informazioni:"
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.Enabled = False
    Me.edEscomp.Location = New System.Drawing.Point(103, 32)
    Me.edEscomp.Name = "edEscomp"
    Me.edEscomp.NTSDbField = ""
    Me.edEscomp.NTSFormat = "0"
    Me.edEscomp.NTSForzaVisZoom = False
    Me.edEscomp.NTSOldValue = ""
    Me.edEscomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscomp.Properties.AutoHeight = False
    Me.edEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscomp.Properties.MaxLength = 65536
    Me.edEscomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscomp.Size = New System.Drawing.Size(73, 20)
    Me.edEscomp.TabIndex = 125
    '
    'edDtulap
    '
    Me.edDtulap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtulap.Enabled = False
    Me.edDtulap.Location = New System.Drawing.Point(245, 64)
    Me.edDtulap.Name = "edDtulap"
    Me.edDtulap.NTSDbField = ""
    Me.edDtulap.NTSForzaVisZoom = False
    Me.edDtulap.NTSOldValue = ""
    Me.edDtulap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtulap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtulap.Properties.AutoHeight = False
    Me.edDtulap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtulap.Properties.MaxLength = 65536
    Me.edDtulap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtulap.Size = New System.Drawing.Size(172, 20)
    Me.edDtulap.TabIndex = 124
    '
    'lbDtulap
    '
    Me.lbDtulap.AutoSize = True
    Me.lbDtulap.BackColor = System.Drawing.Color.Transparent
    Me.lbDtulap.Location = New System.Drawing.Point(5, 67)
    Me.lbDtulap.Name = "lbDtulap"
    Me.lbDtulap.NTSDbField = ""
    Me.lbDtulap.Size = New System.Drawing.Size(234, 13)
    Me.lbDtulap.TabIndex = 123
    Me.lbDtulap.Text = "Data ultimo aggiornamento progressivi definitivi"
    Me.lbDtulap.Tooltip = ""
    Me.lbDtulap.UseMnemonic = False
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(5, 35)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(92, 13)
    Me.lbEscomp.TabIndex = 122
    Me.lbEscomp.Text = "Esercizio corrente"
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'lbDesescomp
    '
    Me.lbDesescomp.BackColor = System.Drawing.Color.Transparent
    Me.lbDesescomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesescomp.Location = New System.Drawing.Point(182, 32)
    Me.lbDesescomp.Name = "lbDesescomp"
    Me.lbDesescomp.NTSDbField = ""
    Me.lbDesescomp.Size = New System.Drawing.Size(235, 20)
    Me.lbDesescomp.TabIndex = 117
    Me.lbDesescomp.Tooltip = ""
    Me.lbDesescomp.UseMnemonic = False
    '
    'tlbGeneraBub
    '
    Me.tlbGeneraBub.Caption = "Crea file per la schedulazione"
    Me.tlbGeneraBub.GlyphPath = ""
    Me.tlbGeneraBub.Id = 6
    Me.tlbGeneraBub.Name = "tlbGeneraBub"
    Me.tlbGeneraBub.Visible = True
    '
    'FRMMGRCAP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(444, 168)
    Me.Controls.Add(Me.pnRcap)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMMGRCAP"
    Me.Text = "RICOSTRUZIONE PROGRESSIVI DI MAGAZZINO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRcap, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRcap.ResumeLayout(False)
    Me.pnRcap.PerformLayout()
    CType(Me.ckRicreakeymag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmInformazioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmInformazioni.ResumeLayout(False)
    Me.fmInformazioni.PerformLayout()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtulap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub LoadImage()
    Try
      tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      edEscomp.NTSSetParam(oMenu, oApp.Tr(Me, 128551604191718750, "Esercizio di competenza"), "0", 4, 1, 9999)
      ckRicreakeymag.NTSSetParam(oMenu, oApp.Tr(Me, 128499874849753867, "Ricrea tabelle Keymag, Keyord, Keyprb"), "S", "N")
      edDtulap.NTSSetParam(oMenu, oApp.Tr(Me, 128499874850221447, "Data ultimo aggiornamento progressivi definitivi"), False)

      edEscomp.NTSSetParamZoom("")
      edDtulap.NTSSetParamZoom("")

      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente
      'collegati, li scollego
      NTSFormClearDataBinding(Me)
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli
      edEscomp.NTSDbField = "tabanaz.tb_escomp"
      lbDesescomp.NTSDbField = "tabanaz.tb_desesco"
      '-------------------------------------------------
      'Aggancio al dataset i vari controlli
      NTSFormAddDataBinding(dcRcap, Me)
      '-------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGRCAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      InitControls()

      If oCleRcap.Apri(DittaCorrente, dsRcap) Then
        dcRcap.DataSource = dsRcap.Tables("tabanaz")
        dsRcap.AcceptChanges()

        Bindcontrols()

        If NTSCStr(dsRcap.Tables("tabanaz").Rows(0)!tb_dtulap) = "" Then
          edDtulap.Text = NTSCStr(NTSCDate("01/01/1900"))
        Else
          edDtulap.Text = NTSCStr(NTSCDate(dsRcap.Tables("tabanaz").Rows(0)!tb_dtulap))
        End If

        GctlSetRoules()

      Else
        Me.Close()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGRCAP_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim strT() As String = Nothing
    Try
      '-------------------------------------------------
      'se sono stato chiamato in modalità batch:
      'esempio di riga di comando:
      'mirto . pr14 business BNMGRCAP /B c:\bus2012\asc\bnmgrcap.bub
      If oApp.Batch And oApp.AvvioProgrammaParametri.Trim <> "" Then
        'Me.Visible = False
        Me.Top = -10000
        Me.Left = -10000

        'imposto il flag di ricostruzione keymag dal file.bub
        'modello del file:
        'ckKeymag=0
        Dim r1 As New System.IO.StreamReader(oApp.AvvioProgrammaParametri)
        Do While Not r1.EndOfStream
          strT = r1.ReadLine().Split("="c)
          Select Case strT(0).ToLower
            Case "ckkeymag" : ckRicreakeymag.Checked = CBool(IIf(strT(1) = "-1", True, False))
          End Select
        Loop
        r1.Close()

        'lancio l'elaborazione. i parametri di avvio verranno presi dal file BNVEPNFA.BUB, poi esco
        oCleRcap.Elabora(ckRicreakeymag.Checked, edDtulap.Text)
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMMGRCAP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    
  End Sub
  Public Overridable Sub FRMMGRCAP_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleRcap.bElabInCorso Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGRCAP_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcRcap.Dispose()
      If Not dsRcap Is Nothing Then dsRcap.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbGeneraBub_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraBub.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim strTipork1 As String = ""
    Try
      '--------------------------------------------
      'scrittura del file per la schedulazione BNVEPNFA.BUB
      'ckKeymag=0
      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130917308022872889, "Funzionalità abilitata solo per utenti amministratori"))
        Return
      End If
      Me.ValidaLastControl()

      If System.IO.File.Exists(oApp.AscDir & "\BNMGRCAP.BUB") Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129681712893159239, "Esiste già un file con nome |" & oApp.AscDir & "\BNMGRCAP.BUB" & "|: sovrascriverlo?")) = Windows.Forms.DialogResult.No Then Return
      End If
      Dim w1 As New System.IO.StreamWriter(oApp.AscDir & "\BNMGRCAP.BUB", False)

      w1.WriteLine("ckKeymag=" & IIf(ckRicreakeymag.Checked, "-1", "0").ToString)

      w1.Flush()
      w1.Close()
      oApp.MsgBoxInfo(oApp.Tr(Me, 128744371685129000, "Creato file |" & oApp.AscDir & "\BNMGRCAP.BUB" & "| correttamente"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlb_Elabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      If oCleRcap.Elabora(ckRicreakeymag.Checked, edDtulap.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128551610678125000, "Elaborazione completata regolarmente."))
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128551610937343750, "Elaborazione non riuscita."))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

End Class
