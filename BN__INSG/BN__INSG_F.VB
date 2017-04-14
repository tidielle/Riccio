Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__INSG_F
  Public dsInsg As DataSet
  Public dcInsg As BindingSource = New BindingSource

  Public oCleInsg As CLE__INSG
  Public oCallParams As CLE__CLDP

  Public nVecchiaLenSerie As Integer = 0
  Public nSerieEsistente As Integer = 0
  Public nDecSuPrzUn As Integer = 0
  Public nVecchiaLenCodart As Integer = 0
  Public nCodartEsistente As Integer = 0

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = 0
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

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbTb_seriemaxlen As NTSInformatica.NTSLabel
  Public WithEvents edTb_seriemaxlen As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
#End Region

#Region "Inizializzazione"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__INSG", "BE__INSG", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128300150998106752, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleInsg = CType(oTmp, CLE__INSG)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__INSG", strRemoteServer, strRemotePort)
    AddHandler oCleInsg.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleInsg.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__INSG_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edTb_seriemaxlen = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_seriemaxlen = New NTSInformatica.NTSLabel
    Me.lbNumeroDecimali = New NTSInformatica.NTSLabel
    Me.edNumeroDecimali = New NTSInformatica.NTSTextBoxNum
    Me.edTb_maxlencodart = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_maxlencodart = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_seriemaxlen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumeroDecimali.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_maxlencodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 0
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 2
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
    'edTb_seriemaxlen
    '
    Me.edTb_seriemaxlen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_seriemaxlen.EditValue = "1"
    Me.edTb_seriemaxlen.Location = New System.Drawing.Point(330, 41)
    Me.edTb_seriemaxlen.Name = "edTb_seriemaxlen"
    Me.edTb_seriemaxlen.NTSDbField = ""
    Me.edTb_seriemaxlen.NTSFormat = "0"
    Me.edTb_seriemaxlen.NTSForzaVisZoom = False
    Me.edTb_seriemaxlen.NTSOldValue = "1"
    Me.edTb_seriemaxlen.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_seriemaxlen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_seriemaxlen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_seriemaxlen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_seriemaxlen.Properties.AutoHeight = False
    Me.edTb_seriemaxlen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_seriemaxlen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_seriemaxlen.Size = New System.Drawing.Size(25, 20)
    Me.edTb_seriemaxlen.TabIndex = 32
    '
    'lbTb_seriemaxlen
    '
    Me.lbTb_seriemaxlen.AutoSize = True
    Me.lbTb_seriemaxlen.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_seriemaxlen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_seriemaxlen.ForeColor = System.Drawing.Color.Red
    Me.lbTb_seriemaxlen.Location = New System.Drawing.Point(12, 44)
    Me.lbTb_seriemaxlen.Name = "lbTb_seriemaxlen"
    Me.lbTb_seriemaxlen.NTSDbField = ""
    Me.lbTb_seriemaxlen.Size = New System.Drawing.Size(292, 13)
    Me.lbTb_seriemaxlen.TabIndex = 16
    Me.lbTb_seriemaxlen.Text = "Lunghezza serie documenti\ordini\scadenze\ecc..."
    Me.lbTb_seriemaxlen.Tooltip = ""
    Me.lbTb_seriemaxlen.UseMnemonic = False
    '
    'lbNumeroDecimali
    '
    Me.lbNumeroDecimali.AutoSize = True
    Me.lbNumeroDecimali.BackColor = System.Drawing.Color.Transparent
    Me.lbNumeroDecimali.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbNumeroDecimali.ForeColor = System.Drawing.Color.Red
    Me.lbNumeroDecimali.Location = New System.Drawing.Point(12, 88)
    Me.lbNumeroDecimali.Name = "lbNumeroDecimali"
    Me.lbNumeroDecimali.NTSDbField = ""
    Me.lbNumeroDecimali.Size = New System.Drawing.Size(230, 13)
    Me.lbNumeroDecimali.TabIndex = 33
    Me.lbNumeroDecimali.Text = "Numero decimali prezzi/prezzi in valuta"
    Me.lbNumeroDecimali.Tooltip = ""
    Me.lbNumeroDecimali.UseMnemonic = False
    '
    'edNumeroDecimali
    '
    Me.edNumeroDecimali.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumeroDecimali.EditValue = "2"
    Me.edNumeroDecimali.Location = New System.Drawing.Point(330, 85)
    Me.edNumeroDecimali.Name = "edNumeroDecimali"
    Me.edNumeroDecimali.NTSDbField = ""
    Me.edNumeroDecimali.NTSFormat = "0"
    Me.edNumeroDecimali.NTSForzaVisZoom = False
    Me.edNumeroDecimali.NTSOldValue = "2"
    Me.edNumeroDecimali.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumeroDecimali.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumeroDecimali.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumeroDecimali.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumeroDecimali.Properties.AutoHeight = False
    Me.edNumeroDecimali.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumeroDecimali.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumeroDecimali.Size = New System.Drawing.Size(25, 20)
    Me.edNumeroDecimali.TabIndex = 34
    '
    'edTb_maxlencodart
    '
    Me.edTb_maxlencodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_maxlencodart.EditValue = "18"
    Me.edTb_maxlencodart.Location = New System.Drawing.Point(330, 63)
    Me.edTb_maxlencodart.Name = "edTb_maxlencodart"
    Me.edTb_maxlencodart.NTSDbField = ""
    Me.edTb_maxlencodart.NTSFormat = "0"
    Me.edTb_maxlencodart.NTSForzaVisZoom = False
    Me.edTb_maxlencodart.NTSOldValue = "18"
    Me.edTb_maxlencodart.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_maxlencodart.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_maxlencodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_maxlencodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_maxlencodart.Properties.AutoHeight = False
    Me.edTb_maxlencodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_maxlencodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_maxlencodart.Size = New System.Drawing.Size(25, 20)
    Me.edTb_maxlencodart.TabIndex = 36
    '
    'lbTb_maxlencodart
    '
    Me.lbTb_maxlencodart.AutoSize = True
    Me.lbTb_maxlencodart.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_maxlencodart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTb_maxlencodart.ForeColor = System.Drawing.Color.Red
    Me.lbTb_maxlencodart.Location = New System.Drawing.Point(12, 66)
    Me.lbTb_maxlencodart.Name = "lbTb_maxlencodart"
    Me.lbTb_maxlencodart.NTSDbField = ""
    Me.lbTb_maxlencodart.Size = New System.Drawing.Size(152, 13)
    Me.lbTb_maxlencodart.TabIndex = 35
    Me.lbTb_maxlencodart.Text = "Lunghezza codice articolo"
    Me.lbTb_maxlencodart.Tooltip = ""
    Me.lbTb_maxlencodart.UseMnemonic = False
    '
    'FRM__INSG_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(565, 120)
    Me.Controls.Add(Me.edTb_maxlencodart)
    Me.Controls.Add(Me.lbTb_maxlencodart)
    Me.Controls.Add(Me.edNumeroDecimali)
    Me.Controls.Add(Me.lbNumeroDecimali)
    Me.Controls.Add(Me.edTb_seriemaxlen)
    Me.Controls.Add(Me.lbTb_seriemaxlen)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__INSG_F"
    Me.Text = "INIZIALIZZAZIONE DATABASE AZIENDA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_seriemaxlen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumeroDecimali.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_maxlencodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      edTb_seriemaxlen.NTSSetParam(oMenu, oApp.Tr(Me, 130071244621821070, "Lunghezza serie documenti\ordini\scadenze\ecc..."), "0", 1, 1, 3)
      edTb_maxlencodart.NTSSetParam(oMenu, oApp.Tr(Me, 130541237933530044, "Lunghezza codice articolo"), "0", 2, 18, 50)
      edNumeroDecimali.NTSSetParam(oMenu, oApp.Tr(Me, 130372892960140748, "Numero decimali prezzi/prezzi in valuta"), "0", 1, 2, 4)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__INSG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      Apri()
      '--------------------------------------------------------------------------------------------------------------
      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
      nSerieEsistente = nVecchiaLenSerie
      nVecchiaLenCodart = NTSCInt(edTb_maxlencodart.Text)
      nCodartEsistente = nVecchiaLenCodart
      '--------------------------------------------------------------------------------------------------------------
      edNumeroDecimali.Text = oMenu.GetSettingBus("OPZIONI", ".", ".", "nDecSuPrzUn", "4", " ", "4")
      nDecSuPrzUn = NTSCInt(edNumeroDecimali.Text)
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Salva()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128300183072189954, "Ripristinare le modifiche apportate all'inizializzazione database azienda?"))
      Else
        dlgRes = Windows.Forms.DialogResult.Yes
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No : Return
        Case Windows.Forms.DialogResult.Yes
          oCleInsg.Ripristina(dcInsg.Position, dcInsg.Filter)
          edNumeroDecimali.Text = nDecSuPrzUn.ToString
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edTb_seriemaxlen_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_seriemaxlen.ValidatedAndChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If nVecchiaLenSerie > NTSCInt(edTb_seriemaxlen.Text) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se la serie è diventata più piccola deve verificare che non siano state usate serie più grandi
        '--- del nuovo limite di caratteri.
        '------------------------------------------------------------------------------------------------------------
        If Not oCleInsg.VerificaLenSerie(NTSCInt(edTb_seriemaxlen.Text)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130071256573521923, "Attenzione!" & vbCrLf & _
            "Non è possibile usare una serie con lunghezza " & edTb_seriemaxlen.Text & _
            " in quanto è già stata usata in dei documenti\ordini\scadenze\ecc... una serie con lunghezza maggiore"))
          edTb_seriemaxlen.NTSTextDB = nVecchiaLenSerie.ToString()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edTb_maxlencodart_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_maxlencodart.ValidatedAndChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If nVecchiaLenCodart > NTSCInt(edTb_maxlencodart.Text) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se la lunghezza del codice articolo è diventata più piccola deve verificare che non siano stati usati
        '--- codici articoli più grandi del nuovo limite di caratteri.
        '------------------------------------------------------------------------------------------------------------
        If Not oCleInsg.VerificaLenCodart(NTSCInt(edTb_maxlencodart.Text)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130541233414969626, "Attenzione!" & vbCrLf & _
            "Non è possibile indicare una lunghezza codice articolo pari a '|" & edTb_maxlencodart.Text & "|'" & vbCrLf & _
            "in quanto esistono già articoli in Anagrafica Articoli con lunghezza maggiore."))
          edTb_seriemaxlen.NTSTextDB = nVecchiaLenSerie.ToString()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edNumeroDecimali_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edNumeroDecimali.ValidatedAndChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il numero non è cambiato, rispetto a quello indicato nell'opzione di registro, non fa niente
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edNumeroDecimali.Text) = nDecSuPrzUn Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il numero indicato è inferiore a quello indicato nell'opzione di registro,
      '--- avvisa e non ne permette la modifica
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edNumeroDecimali.Text) < nDecSuPrzUn Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130372901518706599, "Attenzione!" & vbCrLf & _
          "Non è possibile indicare un numero di decimali inferiore a '|" & nDecSuPrzUn.ToString & "|'."))
        edNumeroDecimali.Text = nDecSuPrzUn.ToString
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleInsg.bInternazionale = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtINT)
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleInsg.Apri(DittaCorrente, dsInsg) Then Me.Close()
      dcInsg.DataSource = dsInsg.Tables("TABINSG")
      dsInsg.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      If dsInsg.Tables("TABINSG").Rows.Count = 0 Then
        '------------------------------------------------------------------------------------------------------------
        oCleInsg.Nuovo()
        dcInsg.MoveLast()
        '------------------------------------------------------------------------------------------------------------
        Me.GctlApplicaDefaultValue()
        '------------------------------------------------------------------------------------------------------------
        oCleInsg.Salva(False)
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      dcInsg.ResetBindings(False)
      dcInsg.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edTb_seriemaxlen.NTSDbField = "tabinsg.tb_seriemaxlen"
      edTb_maxlencodart.NTSDbField = "tabinsg.tb_maxlencodart"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcInsg, Me)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim bNumeroDecimaliCambiato As Boolean = False
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edNumeroDecimali.Text) < nDecSuPrzUn Then edNumeroDecimali.Text = nDecSuPrzUn.ToString
      If NTSCInt(edNumeroDecimali.Text) > nDecSuPrzUn Then bNumeroDecimaliCambiato = True
      '--------------------------------------------------------------------------------------------------------------
      If oCleInsg.RecordIsChanged Then
        If GctlControllaOutNotEqual() = False Then Return False
        '------------------------------------------------------------------------------------------------------------
        If (edTb_seriemaxlen.Text = nSerieEsistente.ToString) And (bNumeroDecimaliCambiato = False) Then
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128300183335593354, "Salvo l'inizializzazione database azienda?"))
        Else
          If (edTb_seriemaxlen.Text <> nSerieEsistente.ToString) And (bNumeroDecimaliCambiato = True) Then
            dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130372904576372837, "Attenzione!" & vbCrLf & _
              "Si sta variando:" & vbCrLf & _
              " --> la lunghezza della serie (da |" & nSerieEsistente.ToString & "| a |" & edTb_seriemaxlen.Text & "| caratteri)" & vbCrLf & _
              "e non sarà più possibile ritornare all'impostazione precedente una volta creati movimenti." & vbCrLf & _
              " --> il '|" & lbNumeroDecimali.Text & "|' (da |" & nDecSuPrzUn.ToString & "| a |" & edNumeroDecimali.Text & "|)" & vbCrLf & _
              "e non sarà più possibile diminuire tale valore." & vbCrLf & _
              "Salvare l'inizializzazione database azienda?"))
          Else
            If bNumeroDecimaliCambiato = True Then
              If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130372941612244489, "Attenzione!" & vbCrLf & _
                "E' stato modificato il '|" & lbNumeroDecimali.Text & "|'" & vbCrLf & _
                "   (da |" & nDecSuPrzUn.ToString & "| a |" & edNumeroDecimali.Text & "|)" & vbCrLf & _
                "N.B.: Una volta confermato, tale valore non potrà più essere diminuito." & vbCrLf & _
                "Salvare l'inizializzazione database azienda?")) = Windows.Forms.DialogResult.No Then
                bNumeroDecimaliCambiato = False
                edNumeroDecimali.Text = nDecSuPrzUn.ToString
              End If
            Else
              dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130372941942149735, "Attenzione!" & vbCrLf & _
                "Si sta variando la lunghezza della serie" & vbCrLf & _
                "   (da |" & nSerieEsistente.ToString & "| a |" & edTb_seriemaxlen.Text & "| caratteri)" & vbCrLf & _
                "e non sarà più possibile ritornare all'impostazione precedente una volta creati movimenti." & vbCrLf & _
                "Salvare l'inizializzazione database azienda?"))
            End If
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleInsg.Salva(False) Then Return False
          nSerieEsistente = NTSCInt(edTb_seriemaxlen.Text)
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          bNumeroDecimaliCambiato = False
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      Else
        If bNumeroDecimaliCambiato = True Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130372906932166421, "Attenzione!" & vbCrLf & _
            "E' stato modificato il '|" & lbNumeroDecimali.Text & "|'" & vbCrLf & _
            "   (da |" & nDecSuPrzUn.ToString & "| a |" & edNumeroDecimali.Text & "|)" & vbCrLf & _
            "N.B.: Una volta confermato, tale valore non potrà più essere diminuito." & vbCrLf & _
            "Salvare l'inizializzazione database azienda?")) = Windows.Forms.DialogResult.No Then
            bNumeroDecimaliCambiato = False
            edNumeroDecimali.Text = nDecSuPrzUn.ToString
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bNumeroDecimaliCambiato = True Then
        If oCleInsg.AggiornanDecSuPrzNum(NTSCInt(edNumeroDecimali.Text)) = True Then
          nDecSuPrzUn = NTSCInt(edNumeroDecimali.Text)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class