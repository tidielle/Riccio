Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGADOC

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public oCleElar As CLEMGELAR

  Public strTestata As String
  Public strRighe As String

  Public WithEvents pnAdoc As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbTipork As NTSInformatica.NTSLabel
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents lbNumdocfin As NTSInformatica.NTSLabel
  Public WithEvents edNumdocfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumdocini As NTSInformatica.NTSLabel
  Public WithEvents edNumdocini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSeriefin As NTSInformatica.NTSLabel
  Public WithEvents lbSerieini As NTSInformatica.NTSLabel
  Public WithEvents edSeriefin As NTSInformatica.NTSTextBoxStr
  Public WithEvents edSerieini As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

  Public Overridable Sub InitializeComponent()
    Me.pnAdoc = New NTSInformatica.NTSPanel
    Me.lbNumdocfin = New NTSInformatica.NTSLabel
    Me.edNumdocfin = New NTSInformatica.NTSTextBoxNum
    Me.lbNumdocini = New NTSInformatica.NTSLabel
    Me.edNumdocini = New NTSInformatica.NTSTextBoxNum
    Me.lbSeriefin = New NTSInformatica.NTSLabel
    Me.lbSerieini = New NTSInformatica.NTSLabel
    Me.edSeriefin = New NTSInformatica.NTSTextBoxStr
    Me.edSerieini = New NTSInformatica.NTSTextBoxStr
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.pnAdoc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAdoc.SuspendLayout()
    CType(Me.edNumdocfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdocini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriefin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerieini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'pnAdoc
    '
    Me.pnAdoc.AllowDrop = True
    Me.pnAdoc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAdoc.Appearance.Options.UseBackColor = True
    Me.pnAdoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAdoc.Controls.Add(Me.lbNumdocfin)
    Me.pnAdoc.Controls.Add(Me.edNumdocfin)
    Me.pnAdoc.Controls.Add(Me.lbNumdocini)
    Me.pnAdoc.Controls.Add(Me.edNumdocini)
    Me.pnAdoc.Controls.Add(Me.lbSeriefin)
    Me.pnAdoc.Controls.Add(Me.lbSerieini)
    Me.pnAdoc.Controls.Add(Me.edSeriefin)
    Me.pnAdoc.Controls.Add(Me.edSerieini)
    Me.pnAdoc.Controls.Add(Me.lbAnno)
    Me.pnAdoc.Controls.Add(Me.cbTipork)
    Me.pnAdoc.Controls.Add(Me.edAnno)
    Me.pnAdoc.Controls.Add(Me.lbTipork)
    Me.pnAdoc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnAdoc.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnAdoc.Location = New System.Drawing.Point(0, 0)
    Me.pnAdoc.Name = "pnAdoc"
    Me.pnAdoc.Size = New System.Drawing.Size(424, 121)
    Me.pnAdoc.TabIndex = 12
    Me.pnAdoc.Text = "NtsPanel1"
    '
    'lbNumdocfin
    '
    Me.lbNumdocfin.AutoSize = True
    Me.lbNumdocfin.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdocfin.Location = New System.Drawing.Point(202, 88)
    Me.lbNumdocfin.Name = "lbNumdocfin"
    Me.lbNumdocfin.NTSDbField = ""
    Me.lbNumdocfin.Size = New System.Drawing.Size(52, 13)
    Me.lbNumdocfin.TabIndex = 593
    Me.lbNumdocfin.Text = "a numero"
    '
    'edNumdocfin
    '
    Me.edNumdocfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocfin.Location = New System.Drawing.Point(260, 85)
    Me.edNumdocfin.Name = "edNumdocfin"
    Me.edNumdocfin.NTSDbField = ""
    Me.edNumdocfin.NTSFormat = "0"
    Me.edNumdocfin.NTSForzaVisZoom = False
    Me.edNumdocfin.NTSOldValue = ""
    Me.edNumdocfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocfin.Properties.MaxLength = 65536
    Me.edNumdocfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocfin.Size = New System.Drawing.Size(93, 20)
    Me.edNumdocfin.TabIndex = 592
    '
    'lbNumdocini
    '
    Me.lbNumdocini.AutoSize = True
    Me.lbNumdocini.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdocini.Location = New System.Drawing.Point(12, 88)
    Me.lbNumdocini.Name = "lbNumdocini"
    Me.lbNumdocini.NTSDbField = ""
    Me.lbNumdocini.Size = New System.Drawing.Size(59, 13)
    Me.lbNumdocini.TabIndex = 591
    Me.lbNumdocini.Text = "Da numero"
    '
    'edNumdocini
    '
    Me.edNumdocini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocini.Location = New System.Drawing.Point(84, 85)
    Me.edNumdocini.Name = "edNumdocini"
    Me.edNumdocini.NTSDbField = ""
    Me.edNumdocini.NTSFormat = "0"
    Me.edNumdocini.NTSForzaVisZoom = False
    Me.edNumdocini.NTSOldValue = ""
    Me.edNumdocini.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocini.Properties.MaxLength = 65536
    Me.edNumdocini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocini.Size = New System.Drawing.Size(93, 20)
    Me.edNumdocini.TabIndex = 590
    '
    'lbSeriefin
    '
    Me.lbSeriefin.AutoSize = True
    Me.lbSeriefin.BackColor = System.Drawing.Color.Transparent
    Me.lbSeriefin.Location = New System.Drawing.Point(202, 62)
    Me.lbSeriefin.Name = "lbSeriefin"
    Me.lbSeriefin.NTSDbField = ""
    Me.lbSeriefin.Size = New System.Drawing.Size(39, 13)
    Me.lbSeriefin.TabIndex = 589
    Me.lbSeriefin.Text = "a serie"
    '
    'lbSerieini
    '
    Me.lbSerieini.AutoSize = True
    Me.lbSerieini.BackColor = System.Drawing.Color.Transparent
    Me.lbSerieini.Location = New System.Drawing.Point(12, 62)
    Me.lbSerieini.Name = "lbSerieini"
    Me.lbSerieini.NTSDbField = ""
    Me.lbSerieini.Size = New System.Drawing.Size(46, 13)
    Me.lbSerieini.TabIndex = 588
    Me.lbSerieini.Text = "Da serie"
    '
    'edSeriefin
    '
    Me.edSeriefin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriefin.Location = New System.Drawing.Point(260, 59)
    Me.edSeriefin.Name = "edSeriefin"
    Me.edSeriefin.NTSDbField = ""
    Me.edSeriefin.NTSForzaVisZoom = False
    Me.edSeriefin.NTSOldValue = ""
    Me.edSeriefin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriefin.Properties.MaxLength = 65536
    Me.edSeriefin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriefin.Size = New System.Drawing.Size(45, 20)
    Me.edSeriefin.TabIndex = 587
    '
    'edSerieini
    '
    Me.edSerieini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerieini.Location = New System.Drawing.Point(84, 59)
    Me.edSerieini.Name = "edSerieini"
    Me.edSerieini.NTSDbField = ""
    Me.edSerieini.NTSForzaVisZoom = False
    Me.edSerieini.NTSOldValue = ""
    Me.edSerieini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerieini.Properties.MaxLength = 65536
    Me.edSerieini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerieini.Size = New System.Drawing.Size(45, 20)
    Me.edSerieini.TabIndex = 586
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 36)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 585
    Me.lbAnno.Text = "Anno"
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(84, 7)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(182, 20)
    Me.cbTipork.TabIndex = 584
    Me.cbTipork.ValueMember = ""
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(84, 33)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(72, 20)
    Me.edAnno.TabIndex = 583
    '
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Location = New System.Drawing.Point(12, 10)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(27, 13)
    Me.lbTipork.TabIndex = 582
    Me.lbTipork.Text = "Tipo"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(430, 35)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(430, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'FRMMGADOC
    '
    Me.ClientSize = New System.Drawing.Size(547, 121)
    Me.Controls.Add(Me.pnAdoc)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGADOC"
    Me.Text = " Da serie"
    CType(Me.pnAdoc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAdoc.ResumeLayout(False)
    Me.pnAdoc.PerformLayout()
    CType(Me.edNumdocfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdocini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriefin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerieini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByVal cleElar As CLEMGELAR)
    oCleElar = cleElar
    AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edNumdocfin.NTSSetParam(oMenu, oApp.Tr(Me, 128686239057707687, "a numero"), "0", 9, 0, 999999999)
      edNumdocini.NTSSetParam(oMenu, oApp.Tr(Me, 128686239058019903, "Da numero"), "0", 9, 0, 999999999)
      edSeriefin.NTSSetParam(oMenu, oApp.Tr(Me, 128686239058488227, "a serie"), CLN__STD.SerieMaxLen, False)
      edSerieini.NTSSetParam(oMenu, oApp.Tr(Me, 128686239058644335, "Da serie"), CLN__STD.SerieMaxLen, False)
      cbTipork.NTSSetParam(oApp.Tr(Me, 128686239058956551, "Tipo"))
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128686239059112659, "Anno"), "0", 4, 1900, 2099)

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

#Region "Eventi di Form"
  Public Overridable Sub FRMMGADOC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      CaricaCombo()

      oCleElar.bAdocAnnullato = False

      '----------------------------------------------------
      cbTipork.SelectedValue = oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Tipork", "A", " ", "A")
      edAnno.Text = NTSCStr(oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Anno", NTSCStr(Year(Now)), " ", NTSCStr(Year(Now))))
      edSerieini.Text = oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Serieini", " ", " ", " ")

      If SerieMaxLen = 1 Then
        edSeriefin.Text = oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Seriefin", "Z", " ", "Z")
      ElseIf SerieMaxLen = 2 Then
        edSeriefin.Text = oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Seriefin", "ZZ", " ", "ZZ")
      Else
        edSeriefin.Text = oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Seriefin", "ZZZ", " ", "ZZZ")
      End If
      edNumdocini.Text = NTSCStr(oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Numdocini", "0", " ", "0"))
      edNumdocfin.Text = NTSCStr(oMenu.GetSettingBus("BSMGADOC", "RECENT", ".", "Numdocfin", "999999999", " ", "999999999"))

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGADOC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
#End Region

#Region "Zoom / ALT+F3 / ALT+F3"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
      NTSCallStandardZoom()

      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validazione serie"
  Public Overridable Sub edSerieini_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edSerieini.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerieini.Text, False)
      If strTmp <> edSerieini.Text Then edSerieini.Text = strTmp

      edSerieini.Text = UCase(edSerieini.Text)
      If edSerieini.Text = "" Then edSerieini.Text = " "
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSeriefin_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edSeriefin.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriefin.Text, False)
      If strTmp <> edSeriefin.Text Then edSeriefin.Text = strTmp

      edSeriefin.Text = UCase(edSeriefin.Text)
      If edSeriefin.Text = "" Then edSeriefin.Text = " "
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTipork As New DataTable()

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"A", "Fatture immediate emesse"})
      dttTipork.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipork.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipork.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipork.Rows.Add(New Object() {"F", "Ricevute fiscali emesse"})
      dttTipork.Rows.Add(New Object() {"I", "Riemissione ricevute fiscali"})
      dttTipork.Rows.Add(New Object() {"J", "Note di accredito ricevute"})
      dttTipork.Rows.Add(New Object() {"L", "Fatture immediate ricevute"})
      dttTipork.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipork.Rows.Add(New Object() {"N", "Note di accredito emesse"})
      dttTipork.Rows.Add(New Object() {"S", "Fatture/ricevute fiscali emesse"})
      If CLN__STD.FRIENDLY = False Then
        dttTipork.Rows.Add(New Object() {"T", "Carichi da produzione"})
        dttTipork.Rows.Add(New Object() {"U", "Scarichi a produzione"})
        dttTipork.Rows.Add(New Object() {"W", "Note di prelievo"})
      End If
      dttTipork.Rows.Add(New Object() {"Z", "Bolle di movimentazione interna"})
      dttTipork.AcceptChanges()
      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '-----------------------------------------------------------------------------------------
      If Not CheckSelection() Then Exit Sub
      '-----------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Tipork", NTSCStr(cbTipork.SelectedValue), " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Anno", edAnno.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Serieini", edSerieini.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Seriefin", edSeriefin.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Numdocini", edNumdocini.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGADOC", "RECENT", ".", "Numdocfin", edNumdocfin.Text, " ", "NS.", "...", "...")
      '-----------------------------------------------------------------------------------------
      strTestata = NTSCStr(IIf(NTSCStr(cbTipork.SelectedValue) = "W", "testprb", "testmag"))
      strRighe = NTSCStr(IIf(NTSCStr(cbTipork.SelectedValue) = "W", "movprb", "movmag"))
      '-----------------------------------------------------------------------------------------
      oCleElar.ComponiQueryMovmag(cbTipork.SelectedValue, edAnno.Text, edSerieini.Text, edSeriefin.Text, _
                                  edNumdocini.Text, edNumdocfin.Text, strTestata, strRighe)
      '-----------------------------------------------------------------------------------------
      oCleElar.bAdocAnnullato = True
      Me.Close()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleElar.bAdocAnnullato = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CheckSelection() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      If NTSCInt(edNumdocini.Text) > NTSCInt(edNumdocfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128686257641743537, "Il numero documento iniziale non può superare quello finale."))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      strTestata = NTSCStr(IIf(NTSCStr(cbTipork.SelectedValue) = "W", "testprb", "testmag"))
      '-----------------------------------------------------------------------------------------
      oCleElar.CheckMovimenti(cbTipork.SelectedValue, edAnno.Text, edSerieini.Text, edSeriefin.Text, _
                              edNumdocini.Text, edNumdocfin.Text, strTestata, strRighe, dsTmp)

      If dsTmp.Tables("TESTMAG").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128686257320623210, "Attenzione!" & vbCrLf & _
          "Non esistono documenti di magazzino con le caratteristiche richieste " & _
          "dai quali selezionare gli articoli per l'elaborazione."))
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class