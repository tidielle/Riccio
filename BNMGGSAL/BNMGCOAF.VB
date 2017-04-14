Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCOAF
  Public oCleGsal As CLEMGGSAL
  Public oCallParams As CLE__CLDP
  Public bAnnullato As Boolean = True

#Region "VARIBILI"
  Private components As System.ComponentModel.IContainer
  Public WithEvents cmdCodarfo As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents pbCoaf As NTSInformatica.NTSPanel
  Public WithEvents lbXx_fase As NTSInformatica.NTSLabel
  Public WithEvents edAl_fase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edAl_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAl_fase As NTSInformatica.NTSLabel
  Public WithEvents lbAl_codart As NTSInformatica.NTSLabel
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

  Public Overridable Sub InitEntity(ByRef cleGsal As CLEMGGSAL)
    oCleGsal = cleGsal
    AddHandler oCleGsal.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.cmdCodarfo = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    Me.pbCoaf = New NTSInformatica.NTSPanel
    Me.lbXx_fase = New NTSInformatica.NTSLabel
    Me.edAl_fase = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edAl_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbAl_fase = New NTSInformatica.NTSLabel
    Me.lbAl_codart = New NTSInformatica.NTSLabel
    CType(Me.pbCoaf, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbCoaf.SuspendLayout()
    CType(Me.edAl_fase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAl_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdCodarfo
    '
    Me.cmdCodarfo.ImageText = ""
    Me.cmdCodarfo.Location = New System.Drawing.Point(529, 75)
    Me.cmdCodarfo.Name = "cmdCodarfo"
    Me.cmdCodarfo.Size = New System.Drawing.Size(129, 26)
    Me.cmdCodarfo.TabIndex = 35
    Me.cmdCodarfo.Text = "Cod. Art./Cli.-For."
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(529, 43)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(129, 26)
    Me.cmdAnnulla.TabIndex = 34
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.ImageText = ""
    Me.cmdOK.Location = New System.Drawing.Point(529, 12)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(129, 26)
    Me.cmdOK.TabIndex = 33
    Me.cmdOK.Text = "Conferma"
    '
    'pbCoaf
    '
    Me.pbCoaf.AllowDrop = True
    Me.pbCoaf.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbCoaf.Appearance.Options.UseBackColor = True
    Me.pbCoaf.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbCoaf.Controls.Add(Me.lbXx_fase)
    Me.pbCoaf.Controls.Add(Me.edAl_fase)
    Me.pbCoaf.Controls.Add(Me.lbXx_codart)
    Me.pbCoaf.Controls.Add(Me.edAl_codart)
    Me.pbCoaf.Controls.Add(Me.lbAl_fase)
    Me.pbCoaf.Controls.Add(Me.lbAl_codart)
    Me.pbCoaf.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbCoaf.Location = New System.Drawing.Point(3, 11)
    Me.pbCoaf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pbCoaf.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pbCoaf.Name = "pbCoaf"
    Me.pbCoaf.NTSActiveTrasparency = True
    Me.pbCoaf.Size = New System.Drawing.Size(520, 73)
    Me.pbCoaf.TabIndex = 42
    Me.pbCoaf.Text = "NtsPanel1"
    '
    'lbXx_fase
    '
    Me.lbXx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_fase.Location = New System.Drawing.Point(124, 38)
    Me.lbXx_fase.Name = "lbXx_fase"
    Me.lbXx_fase.NTSDbField = ""
    Me.lbXx_fase.Size = New System.Drawing.Size(387, 20)
    Me.lbXx_fase.TabIndex = 47
    Me.lbXx_fase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_fase.Tooltip = ""
    Me.lbXx_fase.UseMnemonic = False
    '
    'edAl_fase
    '
    Me.edAl_fase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAl_fase.Location = New System.Drawing.Point(56, 38)
    Me.edAl_fase.Name = "edAl_fase"
    Me.edAl_fase.NTSDbField = ""
    Me.edAl_fase.NTSFormat = "0"
    Me.edAl_fase.NTSForzaVisZoom = True
    Me.edAl_fase.NTSOldValue = ""
    Me.edAl_fase.Properties.Appearance.Options.UseTextOptions = True
    Me.edAl_fase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAl_fase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAl_fase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAl_fase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAl_fase.Properties.MaxLength = 65536
    Me.edAl_fase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAl_fase.Size = New System.Drawing.Size(62, 20)
    Me.edAl_fase.TabIndex = 44
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(238, 13)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(273, 20)
    Me.lbXx_codart.TabIndex = 46
    Me.lbXx_codart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codart.Tooltip = ""
    Me.lbXx_codart.UseMnemonic = False
    '
    'edAl_codart
    '
    Me.edAl_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAl_codart.Location = New System.Drawing.Point(56, 13)
    Me.edAl_codart.Name = "edAl_codart"
    Me.edAl_codart.NTSDbField = ""
    Me.edAl_codart.NTSForzaVisZoom = True
    Me.edAl_codart.NTSOldValue = ""
    Me.edAl_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAl_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAl_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAl_codart.Properties.MaxLength = 65536
    Me.edAl_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAl_codart.Size = New System.Drawing.Size(176, 20)
    Me.edAl_codart.TabIndex = 42
    '
    'lbAl_fase
    '
    Me.lbAl_fase.AutoSize = True
    Me.lbAl_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbAl_fase.Location = New System.Drawing.Point(7, 42)
    Me.lbAl_fase.Name = "lbAl_fase"
    Me.lbAl_fase.NTSDbField = ""
    Me.lbAl_fase.Size = New System.Drawing.Size(30, 13)
    Me.lbAl_fase.TabIndex = 45
    Me.lbAl_fase.Text = "Fase"
    Me.lbAl_fase.Tooltip = ""
    Me.lbAl_fase.UseMnemonic = False
    '
    'lbAl_codart
    '
    Me.lbAl_codart.AutoSize = True
    Me.lbAl_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAl_codart.Location = New System.Drawing.Point(7, 17)
    Me.lbAl_codart.Name = "lbAl_codart"
    Me.lbAl_codart.NTSDbField = ""
    Me.lbAl_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbAl_codart.TabIndex = 43
    Me.lbAl_codart.Text = "Articolo"
    Me.lbAl_codart.Tooltip = ""
    Me.lbAl_codart.UseMnemonic = False
    '
    'FRMMGCOAF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(670, 123)
    Me.Controls.Add(Me.pbCoaf)
    Me.Controls.Add(Me.cmdCodarfo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOK)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGCOAF"
    Me.Text = ""
    CType(Me.pbCoaf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbCoaf.ResumeLayout(False)
    Me.pbCoaf.PerformLayout()
    CType(Me.edAl_fase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAl_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edAl_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128559406511664568, "Articolo"), tabartico, True)
      edAl_fase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128559406652900460, "Fase"), tabartfasi)
      edAl_fase.ArtiPerFase = edAl_codart

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

#Region "FORM"
  Public Overridable Sub FRMMGCOAF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edAl_fase.Text = "0"

      GctlSetRoules()

      edAl_codart.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGCOAF_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

#Region "CONTROLLI"
  Public Overridable Sub edAl_fase_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs)
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAl_codart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAl_codart.Validated
    Dim bEsiste As Boolean = False
    Try
      '----------------------------------------------------------------------------------
      edAl_codart.Text = edAl_codart.Text.ToUpper
      '----------------------------------------------------------------------------------
      If (edAl_codart.Text.Trim = "") Or (edAl_codart.Text.Trim = ".") Then
        lbXx_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
      oCleGsal.DesCodart(DittaCorrente, edAl_codart.Text, lbXx_codart.Text, bEsiste)
      '----------------------------------------------------------------------------------
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128560210589377855, "Codice articolo inesistente."))
        edAl_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAl_fase_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAl_fase.Validated
    Dim bEsiste As Boolean = False
    Try
      If ((edAl_codart.Text.Trim = "") Or (edAl_codart.Text.Trim = ".")) And _
         (NTSCInt(edAl_fase.Text) <> 0) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128560075653768760, "Indicare un codice articolo valido prima di passare alla selezione della fase"))
        Return
      End If
      oCleGsal.DesFase(DittaCorrente, edAl_codart.Text, NTSCInt(edAl_fase.Text), lbXx_fase.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128560251879071440, "Codice fase inesistente per l'articolo indicato."))
        edAl_fase.Text = "0"
        Return
      End If
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdCodarfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodarfo.Click
    Dim fmSecf As FRMMGSEC1 = Nothing
    Try
      Dim strCodarfo As String = ""

      fmSecf = CType(NTSNewFormModal("FRMMGSEC1"), FRMMGSEC1)
      fmSecf.Init(oMenu, Nothing, DittaCorrente)
      fmSecf.ShowDialog(Me)
      If fmSecf.bAnnullato = True Then Return

      strCodarfo = NTSCStr(fmSecf.edCodarfo.Text)

      oCleGsal.DecodificaCodarfo(DittaCorrente, strCodarfo, edAl_codart.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmSecf Is Nothing Then fmSecf.Dispose()
      fmSecf = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim bGesfasi As Boolean = False
    If (edAl_codart.Text.Trim = "") Or (edAl_codart.Text.Trim = ".") Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128560007619319760, "Indicare un codice articolo valido."))
      Return
    End If
    oCleGsal.ArticoloAFasi(DittaCorrente, edAl_codart.Text.Trim, bGesfasi)
    Select Case bGesfasi
      Case True
        If NTSCInt(edAl_fase.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128980854169150540, "Codice articolo gestito a fasi." & vbCrLf & _
            "Indicare una fase valida."))
          Return
        End If
      Case False
        If NTSCInt(edAl_fase.Text) <> 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128980854191650540, "Codice articolo NON gestito a fasi." & vbCrLf & _
            "Indicare la fase a zero."))
          Return
        End If
    End Select
    bAnnullato = False
    Me.Close()
  End Sub
#End Region

#Region "FUNZIONI"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edAl_fase.Focused Then
        If (edAl_codart.Text.Trim = "") Or (edAl_codart.Text.Trim = ".") Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128559992562719656, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edAl_fase.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edAl_fase.Text)
        oParam.strTipo = edAl_codart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAl_fase.Text Then edAl_fase.NTSTextDB = NTSZOOM.strIn
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
#End Region

End Class