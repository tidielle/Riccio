Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DUCO_F
  Public oCallParams As CLE__CLDP
  Public oCleClie As CLE__CLIE
  Public bOk As Boolean = False
  Public bNew As Boolean = False
  Public strCodPcon As String = ""
  Public bNewCodProposto As Boolean = True
  Public bProgrtmp As Integer = 0
  Public bInformLoad As Boolean = True

  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents lbMastro As NTSInformatica.NTSLabel
  Public WithEvents edMastro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMastrolabel As NTSInformatica.NTSLabel
  Public WithEvents edProgr As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbProgr As NTSInformatica.NTSLabel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents ckProvvigioni As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckListini As NTSInformatica.NTSCheckBox
  Public WithEvents ckIndir As NTSInformatica.NTSCheckBox
  Public WithEvents ckCodarfo As NTSInformatica.NTSCheckBox
  Public WithEvents cmdOk As NTSInformatica.NTSButton

  Public Overridable Sub InitializeComponent()
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.ckIndir = New NTSInformatica.NTSCheckBox
    Me.ckCodarfo = New NTSInformatica.NTSCheckBox
    Me.ckProvvigioni = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.ckListini = New NTSInformatica.NTSCheckBox
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.lbMastro = New NTSInformatica.NTSLabel
    Me.edMastro = New NTSInformatica.NTSTextBoxNum
    Me.lbMastrolabel = New NTSInformatica.NTSLabel
    Me.edProgr = New NTSInformatica.NTSTextBoxNum
    Me.lbProgr = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.ckIndir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProgr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.ckIndir)
    Me.pnNuovo.Controls.Add(Me.ckCodarfo)
    Me.pnNuovo.Controls.Add(Me.ckProvvigioni)
    Me.pnNuovo.Controls.Add(Me.ckSconti)
    Me.pnNuovo.Controls.Add(Me.ckListini)
    Me.pnNuovo.Controls.Add(Me.cbTipo)
    Me.pnNuovo.Controls.Add(Me.lbTipo)
    Me.pnNuovo.Controls.Add(Me.lbMastro)
    Me.pnNuovo.Controls.Add(Me.edMastro)
    Me.pnNuovo.Controls.Add(Me.lbMastrolabel)
    Me.pnNuovo.Controls.Add(Me.edProgr)
    Me.pnNuovo.Controls.Add(Me.lbProgr)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNuovo.Location = New System.Drawing.Point(0, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.NTSActiveTrasparency = True
    Me.pnNuovo.Size = New System.Drawing.Size(499, 186)
    Me.pnNuovo.TabIndex = 12
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'ckIndir
    '
    Me.ckIndir.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIndir.EditValue = True
    Me.ckIndir.Location = New System.Drawing.Point(15, 75)
    Me.ckIndir.Name = "ckIndir"
    Me.ckIndir.NTSCheckValue = "S"
    Me.ckIndir.NTSUnCheckValue = "N"
    Me.ckIndir.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIndir.Properties.Appearance.Options.UseBackColor = True
    Me.ckIndir.Properties.Caption = "Duplica indirizzi"
    Me.ckIndir.Size = New System.Drawing.Size(98, 19)
    Me.ckIndir.TabIndex = 668
    '
    'ckCodarfo
    '
    Me.ckCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCodarfo.Location = New System.Drawing.Point(15, 95)
    Me.ckCodarfo.Name = "ckCodarfo"
    Me.ckCodarfo.NTSCheckValue = "S"
    Me.ckCodarfo.NTSUnCheckValue = "N"
    Me.ckCodarfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCodarfo.Properties.Appearance.Options.UseBackColor = True
    Me.ckCodarfo.Properties.Caption = "Duplica codice articolo Cliente/Fornitore"
    Me.ckCodarfo.Size = New System.Drawing.Size(217, 19)
    Me.ckCodarfo.TabIndex = 669
    '
    'ckProvvigioni
    '
    Me.ckProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProvvigioni.EditValue = True
    Me.ckProvvigioni.Location = New System.Drawing.Point(15, 155)
    Me.ckProvvigioni.Name = "ckProvvigioni"
    Me.ckProvvigioni.NTSCheckValue = "S"
    Me.ckProvvigioni.NTSUnCheckValue = "N"
    Me.ckProvvigioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvigioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvigioni.Properties.Caption = "Duplica provvigioni"
    Me.ckProvvigioni.Size = New System.Drawing.Size(114, 19)
    Me.ckProvvigioni.TabIndex = 672
    '
    'ckSconti
    '
    Me.ckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti.EditValue = True
    Me.ckSconti.Location = New System.Drawing.Point(15, 135)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = "N"
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.Caption = "Duplica sconti"
    Me.ckSconti.Size = New System.Drawing.Size(90, 19)
    Me.ckSconti.TabIndex = 671
    '
    'ckListini
    '
    Me.ckListini.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckListini.EditValue = True
    Me.ckListini.Location = New System.Drawing.Point(15, 115)
    Me.ckListini.Name = "ckListini"
    Me.ckListini.NTSCheckValue = "S"
    Me.ckListini.NTSUnCheckValue = "N"
    Me.ckListini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckListini.Properties.Appearance.Options.UseBackColor = True
    Me.ckListini.Properties.Caption = "Duplica listini"
    Me.ckListini.Size = New System.Drawing.Size(90, 19)
    Me.ckListini.TabIndex = 670
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(113, 5)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(83, 20)
    Me.cbTipo.TabIndex = 583
    Me.cbTipo.ValueMember = ""
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(12, 8)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(57, 13)
    Me.lbTipo.TabIndex = 582
    Me.lbTipo.Text = "Tipo conto"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'lbMastro
    '
    Me.lbMastro.BackColor = System.Drawing.Color.Transparent
    Me.lbMastro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMastro.Location = New System.Drawing.Point(202, 27)
    Me.lbMastro.Name = "lbMastro"
    Me.lbMastro.NTSDbField = ""
    Me.lbMastro.Size = New System.Drawing.Size(281, 20)
    Me.lbMastro.TabIndex = 581
    Me.lbMastro.Text = "lbMastro"
    Me.lbMastro.Tooltip = ""
    Me.lbMastro.UseMnemonic = False
    '
    'edMastro
    '
    Me.edMastro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMastro.EditValue = "0"
    Me.edMastro.Location = New System.Drawing.Point(113, 27)
    Me.edMastro.Name = "edMastro"
    Me.edMastro.NTSDbField = ""
    Me.edMastro.NTSFormat = "0"
    Me.edMastro.NTSForzaVisZoom = False
    Me.edMastro.NTSOldValue = "0"
    Me.edMastro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMastro.Properties.Appearance.Options.UseBackColor = True
    Me.edMastro.Properties.Appearance.Options.UseTextOptions = True
    Me.edMastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMastro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMastro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMastro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMastro.Properties.MaxLength = 65536
    Me.edMastro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMastro.Size = New System.Drawing.Size(83, 20)
    Me.edMastro.TabIndex = 7
    '
    'lbMastrolabel
    '
    Me.lbMastrolabel.AutoSize = True
    Me.lbMastrolabel.BackColor = System.Drawing.Color.Transparent
    Me.lbMastrolabel.Location = New System.Drawing.Point(12, 30)
    Me.lbMastrolabel.Name = "lbMastrolabel"
    Me.lbMastrolabel.NTSDbField = ""
    Me.lbMastrolabel.Size = New System.Drawing.Size(75, 13)
    Me.lbMastrolabel.TabIndex = 6
    Me.lbMastrolabel.Text = "Codice mastro"
    Me.lbMastrolabel.Tooltip = ""
    Me.lbMastrolabel.UseMnemonic = False
    '
    'edProgr
    '
    Me.edProgr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProgr.EditValue = "0"
    Me.edProgr.Location = New System.Drawing.Point(113, 49)
    Me.edProgr.Name = "edProgr"
    Me.edProgr.NTSDbField = ""
    Me.edProgr.NTSFormat = "0"
    Me.edProgr.NTSForzaVisZoom = False
    Me.edProgr.NTSOldValue = "0"
    Me.edProgr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProgr.Properties.Appearance.Options.UseBackColor = True
    Me.edProgr.Properties.Appearance.Options.UseTextOptions = True
    Me.edProgr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProgr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProgr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProgr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProgr.Properties.MaxLength = 65536
    Me.edProgr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProgr.Size = New System.Drawing.Size(83, 20)
    Me.edProgr.TabIndex = 0
    '
    'lbProgr
    '
    Me.lbProgr.AutoSize = True
    Me.lbProgr.BackColor = System.Drawing.Color.Transparent
    Me.lbProgr.Location = New System.Drawing.Point(12, 52)
    Me.lbProgr.Name = "lbProgr"
    Me.lbProgr.NTSDbField = ""
    Me.lbProgr.Size = New System.Drawing.Size(69, 13)
    Me.lbProgr.TabIndex = 5
    Me.lbProgr.Text = "Codice conto"
    Me.lbProgr.Tooltip = ""
    Me.lbProgr.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(505, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(131, 26)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(505, 10)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(131, 26)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'FRM__DUCO_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 186)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DUCO_F"
    Me.Text = "NUOVA ANAGRAFICA"
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.ckIndir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProgr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      dttTipo.Rows.Add(New Object() {"C", "Cliente"})
      dttTipo.Rows.Add(New Object() {"F", "Fornitore"})
      dttTipo.AcceptChanges()
      cbTipo.DataSource = dttTipo
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"

      edMastro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572922000, "Mastro"), tabmast)
      edProgr.NTSSetParam(oMenu, oApp.Tr(Me, 128377746573234000, "Progressivo"), "0", 6, 0, 999999)
      cbTipo.NTSSetParam(oApp.Tr(Me, 128365724808870000, "Tipo conto"))
      cbTipo.SelectedValue = oCleClie.strTipoConto

      ckIndir.NTSSetParam(oMenu, oApp.Tr(Me, 129930390547327908, "Duplica indirizzi"), "S", "N")
      ckCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 129930390560219939, "Duplica cod. articolo cliente/forn."), "S", "N")
      ckProvvigioni.NTSSetParam(oMenu, oApp.Tr(Me, 129930390571351967, "Duplica provvigioni"), "S", "N")
      ckSconti.NTSSetParam(oMenu, oApp.Tr(Me, 129930390583076001, "Duplica sconti"), "S", "N")
      ckListini.NTSSetParam(oMenu, oApp.Tr(Me, 129930390594652029, "Duplica listini"), "S", "N")

      edMastro.NTSSetParamZoom("ZOOMTABMAST")

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
  Public Overridable Sub FRM__DUCO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      bInformLoad = True
      AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      strCodPcon = oCleClie.dttAnaz.Rows(0)!tb_azcodpcon.ToString
      lbMastro.Text = ""
      bOk = False

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      cbTipo.SelectedValue = oCleClie.strTipoConto

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bInformLoad = False
    End Try
  End Sub

  Public Overridable Sub FRM__DUCO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If bInformLoad Then Return
      Select Case cbTipo.SelectedValue.ToString
        Case "C"
          edMastro.Text = NTSCInt(oMenu.GetSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoClie", "0", " ", "0")).ToString
        Case "F"
          edMastro.Text = NTSCInt(oMenu.GetSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoForn", "0", " ", "0")).ToString
      End Select
      If NTSCInt(edMastro.Text) <> 0 Then edMastro_Leave(edMastro, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__DUCO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If e.KeyCode = Keys.Enter And NTSCInt(edProgr.Text) <> 0 And edProgr.Text.Length > 6 Then cmdOk_Click(cmdOk, Nothing)

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
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edMastro.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        oParam.strCodPdc = strCodPcon     'passo il piano dei conti
        SetFastZoom(edMastro.Text, oParam) 'gestione dello zoom veloce
        NTSZOOM.strIn = edMastro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edMastro.Text Then edMastro.Text = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edMastro.NTSZoomGest
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validazione conti"
  Public Overridable Sub edMastro_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMastro.Leave
    Dim strTmp As String = ""
    Dim lProgr As Integer = 0
    Try
      If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then bNewCodProposto = True

      If NTSCInt(edMastro.Text) = 0 Then
        lbMastro.Text = ""
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then edProgr.Text = "0"
        Return
      End If

      If oCleClie.TestMastro(NTSCInt(edMastro.Text), strTmp, strCodPcon, cbTipo.SelectedValue.ToString, lProgr) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222143281250, "Codice Mastro contabile non corretto (inesistente o di tipo cliente/fornitore non compatibile)"))
        edMastro.Text = "0"
        lbMastro.Text = ""
        edProgr.Text = "0"
      Else
        lbMastro.Text = strTmp
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then
          edProgr.Text = lProgr.ToString
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub edProgr_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edProgr.Enter
    Try
      bProgrtmp = NTSCInt(edProgr.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edProgr_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edProgr.Leave
    Try
      If bProgrtmp <> NTSCInt(edProgr.Text) Then bNewCodProposto = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedIndexChanged
    Try
      If bInformLoad Then Return
      Select Case cbTipo.SelectedValue.ToString
        Case "C"
          edMastro.Text = NTSCInt(oMenu.GetSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoClie", "0", " ", "0")).ToString
        Case "F"
          edMastro.Text = NTSCInt(oMenu.GetSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoForn", "0", " ", "0")).ToString
      End Select
      If NTSCInt(edMastro.Text) <> 0 Then edMastro_Leave(edMastro, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim lProgr As Integer = 0
    Dim lContoTmp As Integer = 0
    Try
      If NTSCInt(edProgr.Text) <= 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128272203434990000, "Indicare il codice cliente/fornitore"))
        Return
      End If

      If NTSCInt(edProgr.Text) >= oCleClie.lContoProgrMoltip Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128377740413266000, "Progressivo cliente/fornitore di lunghezza superiore a quella consentita dal piano dei conti in uso (il numero deve essere inferiore a |" & oCleClie.lContoProgrMoltip.ToString & "|)"))
        Return
      End If


      If NTSCInt(edMastro.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128377741805722000, "Indicare il mastro a cui deve appartenere il nuovo cliente/fornitore"))
        Return
      End If

      If oCleClie.TestMastro(NTSCInt(edMastro.Text), "", strCodPcon, cbTipo.SelectedValue.ToString, lProgr) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128738065308438000, "Codice Mastro contabile non corretto (inesistente o di tipo cliente/fornitore non compatibile)"))
        Return
      End If

      lContoTmp = (NTSCInt(edMastro.Text) * oCleClie.lContoProgrMoltip) + NTSCInt(edProgr.Text)
      If oMenu.ValCodiceDb(lContoTmp.ToString, DittaCorrente, "ANAGRA", "N") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128388168686454000, "Codice cliente/fornitore '| " & lContoTmp.ToString & " |' già esistente"))
        Return
      End If

      Select Case cbTipo.SelectedValue.ToString
        Case "C"
          oMenu.SaveSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoClie", edMastro.Text, " ", ".S.", ".S.", ".N.")
        Case "F"
          oMenu.SaveSettingBus("BS--CLNV", "RECENT", ".", "MastroNuovoForn", edMastro.Text, " ", ".S.", ".S.", ".N.")
      End Select

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