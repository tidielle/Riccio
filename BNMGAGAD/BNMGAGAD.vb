Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGAGAD
  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = bsModExtMGE
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

#Region "Variaribli"
  Public oCleAgad As CLEMGAGAD
  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents edEscomp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbFinoal As NTSInformatica.NTSLabel
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents edFinoal As NTSInformatica.NTSTextBoxData
  Public WithEvents fmDatPrecAgg As NTSInformatica.NTSGroupBox
  Public WithEvents lbDatPrecAgg As NTSInformatica.NTSLabel
  Public WithEvents ckStoricizza As NTSInformatica.NTSCheckBox
  Public WithEvents lbDesescomp As NTSInformatica.NTSLabel
  Public WithEvents lbDtulap As NTSInformatica.NTSLabel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGAGAD))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    Me.edFinoal = New NTSInformatica.NTSTextBoxData
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.lbFinoal = New NTSInformatica.NTSLabel
    Me.ckStoricizza = New NTSInformatica.NTSCheckBox
    Me.fmDatPrecAgg = New NTSInformatica.NTSGroupBox
    Me.lbDtulap = New NTSInformatica.NTSLabel
    Me.lbDatPrecAgg = New NTSInformatica.NTSLabel
    Me.lbDesescomp = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFinoal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStoricizza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatPrecAgg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatPrecAgg.SuspendLayout()
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
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbElabora.Id = 4
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(12, 209)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(46, 13)
    Me.lbStatus.TabIndex = 94
    Me.lbStatus.Text = "lbStatus"
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.Location = New System.Drawing.Point(146, 50)
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
    Me.edEscomp.Size = New System.Drawing.Size(54, 20)
    Me.edEscomp.TabIndex = 95
    '
    'edFinoal
    '
    Me.edFinoal.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFinoal.Location = New System.Drawing.Point(146, 76)
    Me.edFinoal.Name = "edFinoal"
    Me.edFinoal.NTSDbField = ""
    Me.edFinoal.NTSForzaVisZoom = False
    Me.edFinoal.NTSOldValue = ""
    Me.edFinoal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFinoal.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFinoal.Properties.AutoHeight = False
    Me.edFinoal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFinoal.Properties.MaxLength = 65536
    Me.edFinoal.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFinoal.Size = New System.Drawing.Size(83, 20)
    Me.edFinoal.TabIndex = 96
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(12, 53)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(120, 13)
    Me.lbEscomp.TabIndex = 98
    Me.lbEscomp.Text = "Esercizio di competenza"
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'lbFinoal
    '
    Me.lbFinoal.AutoSize = True
    Me.lbFinoal.BackColor = System.Drawing.Color.Transparent
    Me.lbFinoal.Location = New System.Drawing.Point(12, 79)
    Me.lbFinoal.Name = "lbFinoal"
    Me.lbFinoal.NTSDbField = ""
    Me.lbFinoal.Size = New System.Drawing.Size(75, 13)
    Me.lbFinoal.TabIndex = 99
    Me.lbFinoal.Text = "Elabora fino al"
    Me.lbFinoal.Tooltip = ""
    Me.lbFinoal.UseMnemonic = False
    '
    'ckStoricizza
    '
    Me.ckStoricizza.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStoricizza.Location = New System.Drawing.Point(15, 102)
    Me.ckStoricizza.Name = "ckStoricizza"
    Me.ckStoricizza.NTSCheckValue = "S"
    Me.ckStoricizza.NTSUnCheckValue = "N"
    Me.ckStoricizza.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStoricizza.Properties.Appearance.Options.UseBackColor = True
    Me.ckStoricizza.Properties.AutoHeight = False
    Me.ckStoricizza.Properties.Caption = "Storicizza progressivi precedenti"
    Me.ckStoricizza.Size = New System.Drawing.Size(185, 19)
    Me.ckStoricizza.TabIndex = 100
    '
    'fmDatPrecAgg
    '
    Me.fmDatPrecAgg.AllowDrop = True
    Me.fmDatPrecAgg.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatPrecAgg.Appearance.Options.UseBackColor = True
    Me.fmDatPrecAgg.Controls.Add(Me.lbDtulap)
    Me.fmDatPrecAgg.Controls.Add(Me.lbDatPrecAgg)
    Me.fmDatPrecAgg.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatPrecAgg.Location = New System.Drawing.Point(15, 127)
    Me.fmDatPrecAgg.Name = "fmDatPrecAgg"
    Me.fmDatPrecAgg.Size = New System.Drawing.Size(463, 69)
    Me.fmDatPrecAgg.TabIndex = 101
    '
    'lbDtulap
    '
    Me.lbDtulap.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDtulap.BackColor = System.Drawing.Color.Transparent
    Me.lbDtulap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDtulap.Location = New System.Drawing.Point(174, 38)
    Me.lbDtulap.Name = "lbDtulap"
    Me.lbDtulap.NTSDbField = ""
    Me.lbDtulap.Size = New System.Drawing.Size(76, 20)
    Me.lbDtulap.TabIndex = 100
    Me.lbDtulap.Tooltip = ""
    Me.lbDtulap.UseMnemonic = False
    '
    'lbDatPrecAgg
    '
    Me.lbDatPrecAgg.AutoSize = True
    Me.lbDatPrecAgg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatPrecAgg.Location = New System.Drawing.Point(5, 39)
    Me.lbDatPrecAgg.Name = "lbDatPrecAgg"
    Me.lbDatPrecAgg.NTSDbField = ""
    Me.lbDatPrecAgg.Size = New System.Drawing.Size(163, 13)
    Me.lbDatPrecAgg.TabIndex = 99
    Me.lbDatPrecAgg.Text = "Data precedente aggiornamento"
    Me.lbDatPrecAgg.Tooltip = ""
    Me.lbDatPrecAgg.UseMnemonic = False
    '
    'lbDesescomp
    '
    Me.lbDesescomp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesescomp.BackColor = System.Drawing.Color.Transparent
    Me.lbDesescomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesescomp.Location = New System.Drawing.Point(206, 50)
    Me.lbDesescomp.Name = "lbDesescomp"
    Me.lbDesescomp.NTSDbField = ""
    Me.lbDesescomp.Size = New System.Drawing.Size(258, 20)
    Me.lbDesescomp.TabIndex = 102
    Me.lbDesescomp.Tooltip = ""
    Me.lbDesescomp.UseMnemonic = False
    '
    'FRMMGAGAD
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(499, 237)
    Me.Controls.Add(Me.lbDesescomp)
    Me.Controls.Add(Me.fmDatPrecAgg)
    Me.Controls.Add(Me.ckStoricizza)
    Me.Controls.Add(Me.lbFinoal)
    Me.Controls.Add(Me.lbEscomp)
    Me.Controls.Add(Me.edFinoal)
    Me.Controls.Add(Me.edEscomp)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGAGAD"
    Me.Text = "AGGIORNAMENTO PROGRESSIVI DEFINITIVI MAGAZZINO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFinoal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStoricizza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatPrecAgg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatPrecAgg.ResumeLayout(False)
    Me.fmDatPrecAgg.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGAGAD", "BEMGAGAD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128498740238262677, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleAgad = CType(oTmp, CLEMGAGAD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGAGAD", strRemoteServer, strRemotePort)
    AddHandler oCleAgad.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAgad.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli

      ckStoricizza.NTSSetParam(oMenu, oApp.Tr(Me, 128499874849753867, "Storicizza progressivi precedenti"), "S", "N")
      edFinoal.NTSSetParam(oMenu, oApp.Tr(Me, 128499874850221447, "Elabora fino al"), True)
      edEscomp.NTSSetParam(oMenu, oApp.Tr(Me, 128499874850377307, "Esercizio di competenza"), "0", 4, 1, 9999)

      ckStoricizza.Checked = True

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

#Region "Eventi Form"
  Public Overridable Sub FRMMGAGAD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleAgad.strDtineser = IntSetDate("01/01/" & Year(Now))
      oCleAgad.strDtfieser = IntSetDate("31/12/" & Year(Now))

      If oCleAgad.Apri(DittaCorrente) Then
        edEscomp.Text = oCleAgad.strEscompAnaz
        lbDtulap.Text = oCleAgad.strDtulapAnaz
      End If

      lbStatus.Text = oApp.Tr(Me, 129073494541335613, "Nessuna elaborazione in corso...")

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGAGAD_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If edEscomp.Text.Trim = "" Then
        edEscomp.Focus()
      Else
        If edFinoal.Text.Trim = "" Then edFinoal.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRMMGAGAD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If oCleAgad.bElabInCorso Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strMsg As String = ""

    Try
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If edFinoal.Text.Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129968411420645379, "Attenzione!" & vbCrLf & _
          "Data finale obbligatoria."))
        edFinoal.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      lbStatus.Text = oApp.Tr(Me, 130694184680105989, "Controllo coerenza quantità articoli movimentati Taglie & Colori in corso...")
      Me.Refresh()
      If oCleAgad.CheckCoerenzaTaglieQtaTCO(strMsg) = False Then
        Me.Cursor = Cursors.Default
        lbStatus.Text = oApp.Tr(Me, 130694186217938898, "Nessuna elaborazione in corso...")
        Me.Refresh()
        strMsg += vbCrLf & _
          oApp.Tr(Me, 130694187805663477, "Per avere una lista completa degli articoli con taglie/quantità incongruenti" & vbCrLf & _
          "lanciare il programma di 'Controllo coerenza dati'" & vbCrLf & _
          "(Menu --> Utility)" & vbCrLf & _
          "e selezionare i codici:" & vbCrLf & _
          " . '00059'" & vbCrLf & _
          " . '00060'" & vbCrLf & _
          " . '00061'" & vbCrLf & _
          " . '00062'" & vbCrLf & _
          " . '00063'" & vbCrLf & _
          " . '00064'" & vbCrLf & _
          " . '00065'")
        oApp.MsgBoxInfo(strMsg)
        Return
      End If
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 130694186013118305, "Nessuna elaborazione in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128892045318356748, "Procedere con l'AGGIORNAMENTO PROGRESSIVI DEFINITIVI MAGAZZINO?")) = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor

      lbStatus.Text = oApp.Tr(Me, 129073479166057725, "Elaborazione in corso...")

      If oCleAgad.Elabora(ckStoricizza.Checked, lbDtulap.Text, edFinoal.Text, lbDtulap.Text, edEscomp.Text, lbDesescomp.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128244729072214324, "Elaborazione completata regolarmente."))
      End If

      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129073479198715229, "Nessuna elaborazione in corso...")
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129073479320751165, "Nessuna elaborazione in corso...")
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

#Region "Validazione campi"
  Public Overridable Sub edEscomp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edEscomp.Validated
    Dim strTmp As String = ""
    Try
      If oCleAgad Is Nothing Then Return
      If Not oCleAgad.edEscomp_Validated(NTSCInt(edEscomp.Text), strTmp) Then
        edEscomp.Text = oCleAgad.strEscompAnaz
        lbDesescomp.Text = strTmp
      Else
        lbDesescomp.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class

