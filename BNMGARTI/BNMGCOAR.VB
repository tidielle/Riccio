Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCOAR

#Region "Variabili"
  Public oCleArti As CLEMGARTI
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public bCoarAnnullato As Boolean
  Public bCoarDuplica As Boolean

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdCodarfo As NTSInformatica.NTSButton
  Public WithEvents edCodbar As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodbar As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents cbDaModello As NTSInformatica.NTSComboBox
  Public WithEvents lbDaModello As NTSInformatica.NTSLabel
  Public WithEvents lbInfoModello As NTSInformatica.NTSLabel
  Public WithEvents pnCoar As NTSInformatica.NTSPanel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnCoar = New NTSInformatica.NTSPanel
    Me.cbDaModello = New NTSInformatica.NTSComboBox
    Me.lbDaModello = New NTSInformatica.NTSLabel
    Me.edCodbar = New NTSInformatica.NTSTextBoxStr
    Me.lbCodbar = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.lbInfoModello = New NTSInformatica.NTSLabel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdCodarfo = New NTSInformatica.NTSButton
    CType(Me.pnCoar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCoar.SuspendLayout()
    CType(Me.cbDaModello.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodbar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(359, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(175, 26)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(359, 6)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(175, 26)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Ok"
    '
    'pnCoar
    '
    Me.pnCoar.AllowDrop = True
    Me.pnCoar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCoar.Appearance.Options.UseBackColor = True
    Me.pnCoar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCoar.Controls.Add(Me.cbDaModello)
    Me.pnCoar.Controls.Add(Me.lbDaModello)
    Me.pnCoar.Controls.Add(Me.edCodbar)
    Me.pnCoar.Controls.Add(Me.lbCodbar)
    Me.pnCoar.Controls.Add(Me.edCodart)
    Me.pnCoar.Controls.Add(Me.lbCodart)
    Me.pnCoar.Controls.Add(Me.lbInfoModello)
    Me.pnCoar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCoar.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCoar.Location = New System.Drawing.Point(0, 0)
    Me.pnCoar.Name = "pnCoar"
    Me.pnCoar.NTSActiveTrasparency = True
    Me.pnCoar.Size = New System.Drawing.Size(356, 133)
    Me.pnCoar.TabIndex = 22
    Me.pnCoar.Text = "NtsPanel1"
    '
    'cbDaModello
    '
    Me.cbDaModello.DataSource = Nothing
    Me.cbDaModello.DisplayMember = ""
    Me.cbDaModello.Location = New System.Drawing.Point(118, 15)
    Me.cbDaModello.Name = "cbDaModello"
    Me.cbDaModello.NTSDbField = ""
    Me.cbDaModello.Properties.AutoHeight = False
    Me.cbDaModello.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDaModello.Properties.DropDownRows = 30
    Me.cbDaModello.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDaModello.SelectedValue = ""
    Me.cbDaModello.Size = New System.Drawing.Size(216, 20)
    Me.cbDaModello.TabIndex = 5
    Me.cbDaModello.ValueMember = ""
    '
    'lbDaModello
    '
    Me.lbDaModello.AutoSize = True
    Me.lbDaModello.Location = New System.Drawing.Point(11, 18)
    Me.lbDaModello.Name = "lbDaModello"
    Me.lbDaModello.Size = New System.Drawing.Size(98, 13)
    Me.lbDaModello.TabIndex = 4
    Me.lbDaModello.Text = "Genera dal modello"
    '
    'edCodbar
    '
    Me.edCodbar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodbar.Location = New System.Drawing.Point(118, 73)
    Me.edCodbar.Name = "edCodbar"
    Me.edCodbar.NTSDbField = ""
    Me.edCodbar.NTSForzaVisZoom = False
    Me.edCodbar.NTSOldValue = ""
    Me.edCodbar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodbar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodbar.Properties.AutoHeight = False
    Me.edCodbar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodbar.Properties.MaxLength = 65536
    Me.edCodbar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodbar.Size = New System.Drawing.Size(216, 20)
    Me.edCodbar.TabIndex = 3
    '
    'lbCodbar
    '
    Me.lbCodbar.AutoSize = True
    Me.lbCodbar.BackColor = System.Drawing.Color.Transparent
    Me.lbCodbar.Location = New System.Drawing.Point(11, 76)
    Me.lbCodbar.Name = "lbCodbar"
    Me.lbCodbar.NTSDbField = ""
    Me.lbCodbar.Size = New System.Drawing.Size(77, 13)
    Me.lbCodbar.TabIndex = 2
    Me.lbCodbar.Text = "Codice a barre"
    Me.lbCodbar.Tooltip = ""
    Me.lbCodbar.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(118, 44)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.AutoHeight = False
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(216, 20)
    Me.edCodart.TabIndex = 1
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(11, 47)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(77, 13)
    Me.lbCodart.TabIndex = 0
    Me.lbCodart.Text = "Codice articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'lbInfoModello
    '
    Me.lbInfoModello.Location = New System.Drawing.Point(11, 11)
    Me.lbInfoModello.Name = "lbInfoModello"
    Me.lbInfoModello.Size = New System.Drawing.Size(323, 30)
    Me.lbInfoModello.TabIndex = 4
    Me.lbInfoModello.Text = "La funzione 'Genera dal modello' viene abilitata creando degli articoli con il co" & _
        "dice che inizia con 'MODELLO_'"
    Me.lbInfoModello.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(359, 70)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(175, 26)
    Me.cmdSeleziona.TabIndex = 23
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdCodarfo
    '
    Me.cmdCodarfo.ImageText = ""
    Me.cmdCodarfo.Location = New System.Drawing.Point(359, 102)
    Me.cmdCodarfo.Name = "cmdCodarfo"
    Me.cmdCodarfo.Size = New System.Drawing.Size(175, 26)
    Me.cmdCodarfo.TabIndex = 24
    Me.cmdCodarfo.Text = "&Codice articolo cliente/fornitore"
    '
    'FRMMGCOAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(543, 133)
    Me.Controls.Add(Me.cmdCodarfo)
    Me.Controls.Add(Me.cmdSeleziona)
    Me.Controls.Add(Me.pnCoar)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRMMGCOAR"
    Me.Text = "APRI SCHEDA ARTICOLO"
    CType(Me.pnCoar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCoar.ResumeLayout(False)
    Me.pnCoar.PerformLayout()
    CType(Me.cbDaModello.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodbar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCodbar.NTSSetParam(oMenu, oApp.Tr(Me, 128553203421564263, "Codice a barre"), 255, True)
      edCodart.NTSSetParam(oMenu, oApp.Tr(Me, 128553203421875805, "Codice articolo"), CLN__STD.CodartMaxLen, True)

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
  Public Overridable Sub InitEntity(ByRef cleArti As CLEMGARTI)
    oCleArti = cleArti
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "MGCTFOVIS:"
            'visualizzo il child per cercare in catalogo fornitori
            oMenu.RunChild("BSMGCTFO", "CLSMGCTFO", oApp.Tr(Me, 128553381221728522, "Catalogo fornitori"), DittaCorrente, "", "", Nothing, "APRI;" & _
              strTmp(i).Substring(10, 30) & ";" & _
              strTmp(i).Substring(40, 9) & ";" & _
              strTmp(i).Substring(49, 4), True, True)
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      If oCleArti.nCoarEditmode = 1 And Trim(edCodbar.Text) <> "" And Trim(edCodart.Text) <> "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128553132286662259, "Attenzione non e' possibile inserire contemporanemante sia il codice articolo " & vbCrLf & _
          "che il codice a barre! Inserire solo uno dei due codici!"))
        edCodart.Text = ""
        edCodbar.Text = ""
        Exit Sub
      End If
      If Trim(edCodbar.Text) <> "" Then
        If Not CheckCodbar() Then
          edCodbar.Text = ""
          Exit Sub
        End If
        edCodart.Text = oCleArti.strCodBarArt
      End If

      edCodart.Text = UCase(edCodart.Text)
      If Not CheckCodart() Then Exit Sub

      If Not oCleArti.CheckArticoloBarcode(edCodart.Text, edCodbar.Text) Then
        Exit Sub
      Else
        oCleArti.strCodart = edCodart.Text
        oCleArti.strModello = cbDaModello.SelectedValue
        bCoarAnnullato = False
        Me.Close()
      End If

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bCoarAnnullato = True
    Me.Close()
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleArti.strWhereFiar = oPar.strOut.Trim

      bCoarAnnullato = False
      Me.Close()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdCodarfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodarfo.Click
    Dim oParam As New CLE__CLDP
    Dim frmSecf As FRMMGSECF = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      frmSecf = CType(NTSNewFormModal("FRMMGSECF"), FRMMGSECF)
      frmSecf.strSecfCodarfo = ""

      frmSecf.Init(oMenu, oParam, DittaCorrente)
      frmSecf.InitEntity(oCleArti)
      frmSecf.ShowDialog()

      If frmSecf.bSecfAnnullato = True Then Exit Sub

      oCleArti.GetArticoCodarfo(frmSecf.strSecfCodarfo, dsTmp)

      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128552520253990585, "Codice Articolo Cliente/Fornitore inesistente."))
      Else
        edCodart.Text = NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!caf_codart)
      End If

      edCodart.Focus()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSecf Is Nothing Then frmSecf.Dispose()
      frmSecf = Nothing
    End Try
  End Sub

  Public Overridable Sub FRMMGCOAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

  Public Overridable Sub FRMMGCOAR_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If edCodart.Focused And e.KeyCode = Keys.Return Then
        If edCodart.Text.PadRight(1).Substring(edCodart.Text.PadRight(1).Length - 1, 1) = "," Then
          Zoom()
          e.Handled = True
        Else
          If Not Me.frmAuto.bSelezionato Then
            If (edCodart.Text.Trim = "") And (edCodbar.Text.Trim = "") Then Return
            cmdOk.Focus()
            cmdOk_Click(sender, e)
          Else
            Me.frmAuto.bSelezionato = False
          End If
        End If
      ElseIf edCodbar.Focused And edCodbar.Text.Trim.Length <> 0 And e.KeyCode = Keys.Enter Then
        If CheckCodbar() Then
          cmdOk.Focus()
          cmdOk_Click(sender, e)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGCOAR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Me.NTSDisableEnterComeTab() 'altrimenti su 'nuovo' + invio in mgarti si posiziona su 'descrizione invece che su 'cod. alternativo', come invece succede se si seleziona OK con il mouse

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bCoarAnnullato = True

      oCleArti.strGeneraCodice = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodice", "0", " ", "0")
      oCleArti.nLungRoot = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
      oCleArti.nLungExt = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
      oCleArti.bFocusCodArtCodBar = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "FocusCodArtCodBar", "0", " ", "0"))
      oCleArti.bCoarArticoloGenerato = False
      oCleArti.lCoarProgr = 0
      oCleArti.strCoarRoot = ""

      lbDaModello.Visible = False
      cbDaModello.Visible = False
      lbInfoModello.Visible = False
      Select Case oCleArti.nCoarEditmode
        Case 1
          GctlSetVisEnab(cmdCodarfo, False)
          edCodart.NTSSetParamZoom("ZOOMARTICO")
          If Not bCoarDuplica Then GctlSetVisEnab(cmdSeleziona, False)
          Me.Text = oApp.Tr(Me, 130385775494516653, "Apri Scheda Articolo")
          GctlSetVisEnab(edCodbar, False)
        Case 2
          cmdCodarfo.Enabled = False
          cmdSeleziona.Enabled = False
          Me.Text = oApp.Tr(Me, 130385775576704153, "Nuova Scheda Articolo")
          edCodbar.Enabled = False
          Dim dttModelli As DataTable = oCleArti.ListaArticoliModello()
          If dttModelli.Rows.Count = 0 Then
            lbInfoModello.Visible = True
          Else
            lbDaModello.Visible = True
            cbDaModello.Visible = True
            cbDaModello.DataSource = dttModelli
            cbDaModello.ValueMember = "cod"
            cbDaModello.DisplayMember = "val"
            cbDaModello.SelectedValue = " "
          End If
      End Select

      'Attenzione, la scritta "MODELLO_" non deve essere tradotta.
      lbInfoModello.Text = oApp.Tr(Me, 130417042731483823, "La funzione 'Genera dal modello' viene abilitata creando degli articoli " & _
                                                           "con il codice che inizia con '|" & "MODELLO_" & "|'")

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
  Public Overridable Sub FRMMGCOAR_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      edCodart.Focus()
      If oCleArti.nCoarEditmode = 1 Then
        If oCleArti.bFocusCodArtCodBar Then edCodbar.Focus()
      End If
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Zoom()
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edCodart Is NTSLastControlFocussed() Or edCodart.Focused Then
        Select Case oCleArti.nCoarEditmode
          Case 1
            SetFastZoom(edCodart.Text, oParam)    'abilito la gestione dello zoom veloce
            NTSZOOM.strIn = edCodart.Text
            oParam.bVisGriglia = True
            oParam.strTipoArticolo = "N"
            oParam.strAlfpar = "BNMGARTI_F5"
            oParam.strCodPdc = "N"
            NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edCodart.Text Then
              edCodart.NTSTextDB = NTSZOOM.strIn
              If oCleArti.bSelCodiceNoApri = False Then
                oCleArti.strCodart = edCodart.Text
                bCoarAnnullato = False
                Me.Close()
              End If
            End If
        End Select
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodart_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Try
      If Not CheckCodart() Then Exit Sub
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodbar_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodbar.Validated
    Try
      If Trim(edCodbar.Text) <> "" Then
        If Not CheckCodbar() Then Exit Sub
      End If
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckCodart() As Boolean
    Try
      Return oCleArti.CheckCodart(edCodart.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCodbar() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCleArti.CheckCodbar(edCodbar.Text, dsTmp)

      If dsTmp.Tables("BARCODE").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128552544800880234, "Non esiste il codice a barre inserito! "))
        Return False
      Else
        oCleArti.strCodBarArt = NTSCStr(dsTmp.Tables("BARCODE").Rows(0)!bc_codart)
      End If

      Return True

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class