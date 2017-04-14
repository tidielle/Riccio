Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SEAN
  Public oCallParams As CLE__CLDP
  Public oCleClie As CLE__CLIE
  Public bOk As Boolean = False
  Public strOut As String = ""

  Public Overridable Sub InitializeComponent()
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.pnApri = New NTSInformatica.NTSPanel
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnApri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnApri.SuspendLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "1"
    Me.edConto.Location = New System.Drawing.Point(88, 45)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = "1"
    Me.edConto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edConto.Properties.Appearance.Options.UseBackColor = True
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(100, 20)
    Me.edConto.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(216, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(216, 8)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(3, 48)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(69, 13)
    Me.lbConto.TabIndex = 5
    Me.lbConto.Text = "Codice conto"
    '
    'pnApri
    '
    Me.pnApri.AllowDrop = True
    Me.pnApri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnApri.Appearance.Options.UseBackColor = True
    Me.pnApri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnApri.Controls.Add(Me.cbTipo)
    Me.pnApri.Controls.Add(Me.lbTipo)
    Me.pnApri.Controls.Add(Me.edConto)
    Me.pnApri.Controls.Add(Me.lbConto)
    Me.pnApri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnApri.Location = New System.Drawing.Point(2, 8)
    Me.pnApri.Name = "pnApri"
    Me.pnApri.Size = New System.Drawing.Size(208, 79)
    Me.pnApri.TabIndex = 9
    Me.pnApri.Text = "NtsPanel1"
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Enabled = False
    Me.cbTipo.Location = New System.Drawing.Point(88, 15)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(100, 20)
    Me.cbTipo.TabIndex = 7
    Me.cbTipo.ValueMember = ""
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(3, 18)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(57, 13)
    Me.lbTipo.TabIndex = 6
    Me.lbTipo.Text = "Tipo conto"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Location = New System.Drawing.Point(216, 65)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(105, 22)
    Me.cmdSeleziona.TabIndex = 10
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'FRM__SEAN
    '
    Me.ClientSize = New System.Drawing.Size(329, 92)
    Me.Controls.Add(Me.cmdSeleziona)
    Me.Controls.Add(Me.pnApri)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SEAN"
    Me.Text = "Apri anagrafica"
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnApri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnApri.ResumeLayout(False)
    Me.pnApri.PerformLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023056530862, "Codice cliente/fornitore"), tabanagra)
      cbTipo.NTSSetParam(oApp.Tr(Me, 128969756634713862, "Tipo conto"))
      cbTipo.SelectedValue = oCleClie.strTipoConto

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



  Public Overridable Sub FRM__SEAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOk = False
      strOut = ""
      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleClie.strTipoConto
      GctlSetRoules()
      cbTipo.SelectedValue = oCleClie.strTipoConto  'con il modulo  internazionale quando esegue la GctlTipoDoc cbTipo viene impostato fisso = 'C'... lo reimposto = a frm__clie
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__SEAN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If e.KeyCode = Keys.Enter And NTSCInt(edConto.Text) <> 0 And edConto.Text.Length > 6 Then cmdOk_Click(cmdOk, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



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

      If edConto.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        SetFastZoom(edConto.Text, oParam) 'gestione dello zoom veloce
        NTSZOOM.strIn = edConto.Text
        oParam.bTipoProposto = False
        oParam.strAlfpar = "BN__CLIE_F5"
        NTSZOOM.ZoomStrIn("ZOOMANAGRA" & cbTipo.SelectedValue.ToUpper, DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then
          edConto.Text = NTSZOOM.strIn
          If oCleClie.bSelCodiceNoApri = False Then
            strOut = ""
            bOk = True
            Me.Close()
          End If
        End If
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

  Public Overridable Sub edConto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edConto.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()
      If e.TipoEvento = "OPEN" Then bNuovo = False
      oTmp.Text = edConto.Text

      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRA" & cbTipo.SelectedValue.ToUpper, DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edConto.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If NTSCInt(edConto.Text) <= 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128969756767373508, "Indicare il codice cliente/fornitore"))
        Return
      End If
      strOut = ""
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



  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oParam As New CLE__PATB
    Try
      edConto.Text = "0"
      oParam.bTipoProposto = False
      oParam.bVisGriglia = False
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMANAGRA" & cbTipo.SelectedValue.ToUpper, DittaCorrente, oParam)

      If NTSZOOM.strIn = "" Then
        'ho fatto annulla
      Else
        edConto.Text = "0"
        strOut = NTSZOOM.strIn
        bOk = True
        Me.Close()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
                                                    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    'modifico la funzione standard dell'autocompletamento per fare in modo che da edConto 
    'vengano visti solo clienti o fornitori
    Try
      If edConto.ContainsFocus Then
        If cbTipo.SelectedValue = "C" Then
          strTabName = "ANAGRA_CLI"
        Else
          strTabName = "ANAGRA_FOR"
        End If
        If oApp.oGvar.bAutoCompleteIgnoraCF Then strTabName = "ANAGRACF"
      End If

      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
