Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLTB

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleHltb As CLE__HLTB
  Public dsHltb As New DataSet
  Public dtcHltb As BindingSource = New BindingSource()
  Public strNomeZoom As String     'stringa contenente il tipo di zoom lanciato 'ZOOMTABPAGA, ZOOMTABMAGA, ...
  Public strNomeCampoCod As String = "tb_codice"
  Public bSelectIfOneRow As Boolean = False 'se true, dopo aver lanciato la ricerca se viene restituito un solo record viene subito selezionato

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, _
                                 ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", _
                                 Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    Dim strTmp As String = ""
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    strNomeZoom = NomeZoom
    If Ditta <> "" Then DittaCorrente = Ditta
    oParam = Param

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HLTB", "BE__HLTB", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222144843750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHltb = CType(oTmp, CLE__HLTB)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HLTB", strRemoteServer, strRemotePort)
    oCleHltb.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHltb.RemoteEvent, AddressOf GestisciEventiEntity

    If strNomeZoom.Length = 11 And strNomeZoom.Substring(0, 7) = "ZOOMTAB" And _
                                   strNomeZoom <> "ZOOMTABCAUC" And _
                                   strNomeZoom <> "ZOOMDESTDIV" And _
                                   strNomeZoom <> "ZOOMANAGPC" And _
                                   strNomeZoom <> "ZOOMTABANAZ" And _
                                   strNomeZoom <> "ZOOMTABSMEL" And _
                                   strNomeZoom <> "ZOOMTABPROC" And _
                                   strNomeZoom <> "ZOOMTABCOVE" And _
                                   strNomeZoom <> "ZOOMTABATTI" And _
                                   strNomeZoom <> "ZOOMTABDICV" Then
      strNomeCampoCod = "tb_cod" & strNomeZoom.Substring(7, 4).ToLower
    End If

    '---------------------------------
    'determino la caption della form
    Me.Text = "Zoom"
    If strNomeZoom.Substring(0, 7).ToUpper = "ZOOMTAB" Then
      strTmp = oCleHltb.GetDescrTabella(strNomeZoom.Substring(4))
    End If
    If strTmp <> "" Then
      Me.Text += " - " + strTmp
    Else
      Select Case strNomeZoom
        Case "ZOOMARTCLAS" : Me.Text += " - Classificazioni Articoli" : Exit Select
        Case "ZOOMCOMUNI" : Me.Text += " - Comuni" : Exit Select
        Case "ZOOMABI" : Me.Text += " - Codici ABI" : Exit Select
        Case "ZOOMDESTDIV" : Me.Text += " - Destinazioni diverse conto " & Param.lContoCF.ToString()
        Case "ZOOMARTFASI" : Me.Text += " - Fasi articolo " & Param.strTipo
        Case "ZOOMTABPCON" : Me.Text += " - Piani dei conti" : Exit Select
        Case "ZOOMTABMAST" : Me.Text += " - Mastri di contabilità" : Exit Select
        Case "ZOOMTABANAZ" : Me.Text += " - Ditte"
        Case "ZOOMANAGPC" : Me.Text += " - Sottoconti PDC"
        Case "ZOOMTABESCO" : Me.Text += " - Esercizi contabili" : Exit Select
        Case "ZOOMSUBCOMM" : Me.Text += " - Sottocommesse (Comm." & oParam.lCommessa.ToString & ")" : Exit Select
        Case "ZOOMANAGCA" : Me.Text += " - Sottoconti CA" : Exit Select
        Case "ZOOMANALINK" : Me.Text += " - Sottoconti CA specifici per conto " & oParam.lContoCF.ToString : Exit Select
        Case "ZOOMBUDGETCADACONTO"
          Select Case oParam.strTipo
            Case "E" : Me.Text += " Budget - Centri CA" : Exit Select
            Case "L" : Me.Text += " Budget - Linee CA" : Exit Select
            Case "C" : Me.Text += " Budget - Commesse " : Exit Select
            Case "S" : Me.Text += " Budget - Sottocommesse (Comm." & oParam.lCommessa.ToString & ")" : Exit Select
          End Select
        Case "ZOOMTARIC" : Me.Text += " - Codici nomenclatura combinata"
        Case "ZOOMNOMPROP" : Me.Text += " - Proprietà"
        Case "ZOOMVALPROP" : Me.Text += " - Valori proprietà"
        Case "ZOOMLEADS" : Me.Text += " - Leads"
        Case "ZOOMTABCAMP" : Me.Text += " - Campagne commerciali"
        Case "ZOOMTABLINGP" : Me.Text += " - Lingue operatori"
        Case "ZOOMTABCVUO" : Me.Text += " - Codici vuoti"
        Case "ZOOMDISTBAS" : Me.Text += " - Distinta base"
        Case "ZOOMRUOLI" : Me.Text += " - Ruoli"
        Case "ZOOMOPERAT" : Me.Text += " - Operatori"
        Case "ZOOMAZIENDE" : Me.Text += " - Aziende"
        Case "ZOOMTIPIALERT" : Me.Text += " - Tipi Alert"
        Case "ZOOMTABPROC" : Me.Text += " - Procedure Import/Export"
        Case "ZOOMTABSTAB" : Me.Text += " - Stabilimenti"
        Case "ZOOMVALVARI" : Me.Text += " - Varianti"
        Case "ZOOMVALVARIESPL" : Me.Text += " - Varianti ""esplose""" & _
                                            " (articolo:  " & oParam.strCodPdc & ")"
        Case "ZOOMVALVARIESPLTC" : Me.Text += " - Varianti ""esplose""" & _
                                            " (articolo:  " & oParam.strCodPdc & ")"
        Case "ZOOMTABSTAB" : Me.Text += " - Gruppi logistici"
        Case "ZOOMCHIPERS" : Me.Text += " - Valori Chiavi di percorso Configuratore"
        Case "ZOOMTASK" : Me.Text += " - Id Wbs"
        Case "AUTOCOMPLETE" : Me.Text += " - Autocompletamento"
        Case "ZOOMMODRICH" : Me.Text += " - Distinte ricambi per modello"
        Case "ZOOMPARSTAG" : Me.Text += " - Stampe Parametriche"
        Case Else
          Me.Text += " - " & strNomeZoom
      End Select
    End If

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edDescr.NTSSetParam(oMenu, oApp.Tr(Me, 128230023468515236, "Descrizione"), 255)
      ckOttimistico.NTSSetParam(oMenu, oApp.Tr(Me, 128230023468671409, "Ottimistico"), "S", "N")

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023468827582, "Griglia Zoom"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      tb_codice.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023468983755, "Codice"), 20, False)
      tb_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023469139928, "Descrizione"), 0, True, True)
      grvZoom.Enabled = False

      If strNomeZoom = "ZOOMABI" Then grvZoom.AddColumnBackColor("backcolor_row")

      '-------------------------------------------------
      'setto il recent
      ckOttimistico.Checked = CBool(oMenu.GetSettingBus("BN__HLTB", "RECENT", ".", "Ottimistico_" & strNomeZoom, "0", " ", "0"))

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

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.ckIgnoraEsclusioni = New NTSInformatica.NTSCheckBox
    Me.cmdEsclusioni = New NTSInformatica.NTSButton
    Me.ckOttimistico = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdGestione = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.edDescr = New NTSInformatica.NTSTextBoxStr
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.tb_codice = New NTSInformatica.NTSGridColumn
    Me.tb_descr = New NTSInformatica.NTSGridColumn
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckIgnoraEsclusioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.ckIgnoraEsclusioni)
    Me.pnAction.Controls.Add(Me.cmdEsclusioni)
    Me.pnAction.Controls.Add(Me.ckOttimistico)
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdGestione)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Controls.Add(Me.cmdRicerca)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(311, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(110, 340)
    Me.pnAction.TabIndex = 2
    '
    'ckIgnoraEsclusioni
    '
    Me.ckIgnoraEsclusioni.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ckIgnoraEsclusioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnoraEsclusioni.Location = New System.Drawing.Point(10, 318)
    Me.ckIgnoraEsclusioni.Name = "ckIgnoraEsclusioni"
    Me.ckIgnoraEsclusioni.NTSCheckValue = "S"
    Me.ckIgnoraEsclusioni.NTSUnCheckValue = "N"
    Me.ckIgnoraEsclusioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnoraEsclusioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnoraEsclusioni.Properties.Caption = "Ignora esclus."
    Me.ckIgnoraEsclusioni.Size = New System.Drawing.Size(97, 19)
    Me.ckIgnoraEsclusioni.TabIndex = 6
    '
    'cmdEsclusioni
    '
    Me.cmdEsclusioni.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsclusioni.ImageText = ""
    Me.cmdEsclusioni.Location = New System.Drawing.Point(10, 289)
    Me.cmdEsclusioni.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdEsclusioni.Name = "cmdEsclusioni"
    Me.cmdEsclusioni.Size = New System.Drawing.Size(88, 25)
    Me.cmdEsclusioni.TabIndex = 5
    Me.cmdEsclusioni.Text = "Esclusioni"
    '
    'ckOttimistico
    '
    Me.ckOttimistico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOttimistico.Location = New System.Drawing.Point(10, 144)
    Me.ckOttimistico.Name = "ckOttimistico"
    Me.ckOttimistico.NTSCheckValue = "S"
    Me.ckOttimistico.NTSUnCheckValue = "N"
    Me.ckOttimistico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOttimistico.Properties.Appearance.Options.UseBackColor = True
    Me.ckOttimistico.Properties.Caption = "&Ottimistico"
    Me.ckOttimistico.Size = New System.Drawing.Size(97, 19)
    Me.ckOttimistico.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(10, 95)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(88, 25)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdGestione
    '
    Me.cmdGestione.ImageText = ""
    Me.cmdGestione.Location = New System.Drawing.Point(10, 68)
    Me.cmdGestione.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdGestione.Name = "cmdGestione"
    Me.cmdGestione.Size = New System.Drawing.Size(88, 25)
    Me.cmdGestione.TabIndex = 2
    Me.cmdGestione.Text = "&Gestione"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(10, 40)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(88, 25)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(10, 12)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(88, 25)
    Me.cmdRicerca.TabIndex = 0
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.edDescr)
    Me.pnDescr.Controls.Add(Me.lbDescr)
    Me.pnDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(311, 65)
    Me.pnDescr.TabIndex = 1
    '
    'edDescr
    '
    Me.edDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr.Location = New System.Drawing.Point(11, 31)
    Me.edDescr.Name = "edDescr"
    Me.edDescr.NTSDbField = ""
    Me.edDescr.NTSForzaVisZoom = False
    Me.edDescr.NTSOldValue = ""
    Me.edDescr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescr.Properties.Appearance.Options.UseBackColor = True
    Me.edDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr.Properties.MaxLength = 65536
    Me.edDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr.Size = New System.Drawing.Size(300, 20)
    Me.edDescr.TabIndex = 1
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Location = New System.Drawing.Point(10, 10)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(61, 13)
    Me.lbDescr.TabIndex = 1
    Me.lbDescr.Text = "Descrizione"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 65)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(311, 275)
    Me.grZoom.TabIndex = 3
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codice, Me.tb_descr})
    Me.grvZoom.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvZoom.Enabled = True
    Me.grvZoom.GridControl = Me.grZoom
    Me.grvZoom.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvZoom.MinRowHeight = 14
    Me.grvZoom.Name = "grvZoom"
    Me.grvZoom.NTSAllowDelete = True
    Me.grvZoom.NTSAllowInsert = True
    Me.grvZoom.NTSAllowUpdate = True
    Me.grvZoom.NTSMenuContext = Nothing
    Me.grvZoom.OptionsCustomization.AllowRowSizing = True
    Me.grvZoom.OptionsFilter.AllowFilterEditor = False
    Me.grvZoom.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvZoom.OptionsNavigation.UseTabKey = False
    Me.grvZoom.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvZoom.OptionsView.ColumnAutoWidth = False
    Me.grvZoom.OptionsView.EnableAppearanceEvenRow = True
    Me.grvZoom.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvZoom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvZoom.OptionsView.ShowGroupPanel = False
    Me.grvZoom.RowHeight = 16
    '
    'tb_codice
    '
    Me.tb_codice.AppearanceCell.Options.UseBackColor = True
    Me.tb_codice.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codice.Caption = "Codice"
    Me.tb_codice.Enabled = True
    Me.tb_codice.FieldName = "tb_codice"
    Me.tb_codice.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codice.Name = "tb_codice"
    Me.tb_codice.NTSRepositoryComboBox = Nothing
    Me.tb_codice.NTSRepositoryItemCheck = Nothing
    Me.tb_codice.NTSRepositoryItemMemo = Nothing
    Me.tb_codice.NTSRepositoryItemText = Nothing
    Me.tb_codice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codice.OptionsFilter.AllowFilter = False
    Me.tb_codice.Visible = True
    Me.tb_codice.VisibleIndex = 0
    Me.tb_codice.Width = 94
    '
    'tb_descr
    '
    Me.tb_descr.AppearanceCell.Options.UseBackColor = True
    Me.tb_descr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descr.Caption = "Descrizione"
    Me.tb_descr.Enabled = True
    Me.tb_descr.FieldName = "tb_descr"
    Me.tb_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descr.Name = "tb_descr"
    Me.tb_descr.NTSRepositoryComboBox = Nothing
    Me.tb_descr.NTSRepositoryItemCheck = Nothing
    Me.tb_descr.NTSRepositoryItemMemo = Nothing
    Me.tb_descr.NTSRepositoryItemText = Nothing
    Me.tb_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descr.OptionsFilter.AllowFilter = False
    Me.tb_descr.Visible = True
    Me.tb_descr.VisibleIndex = 1
    Me.tb_descr.Width = 300
    '
    'FRM__HLTB
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(421, 340)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Controls.Add(Me.pnAction)
    Me.Name = "FRM__HLTB"
    Me.NTSLastControlFocussed = Me.grZoom
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckIgnoraEsclusioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    Me.pnDescr.PerformLayout()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "FORM"
  Public Overridable Sub FRM__HLTB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'se richiesto dal child se la ricerca trova un solo risultato lo seleziona subito.
      bSelectIfOneRow = oParam.bFlag1
      edDescr.Text = oParam.strDescr

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If strNomeZoom = "AUTOCOMPLETE" Then
        'autocompletamento da busnet_is
        cmdGestione.Visible = False
        ckOttimistico.Visible = False
        ckOttimistico.Checked = False
      Else
        'disabilito il comando 'gestione' se non abilitato
        If NTSZOOM.GetNomeProgForGest(strNomeZoom).Trim = "" Then cmdGestione.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLTB_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '-------------------------------------------------
      If strNomeZoom.PadRight(7).Substring(0, 7).ToUpper = "ZOOMTAB" Then
        'gestisco le esclusioni
      Else
        cmdEsclusioni.Visible = False
        ckIgnoraEsclusioni.Checked = True
        ckIgnoraEsclusioni.Visible = False
      End If
      '-------------------------------------------------
      'carico la griglia
      cmdRicerca_Click(Me, Nothing)
      bSelectIfOneRow = False     'dopo la prima ricerca lanciata dal clienti con la ',' finale faccio in modo che il flag 'ottimistico' torni a funzionare

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__HLTB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    If e.KeyCode = Keys.Enter Then
      If grvZoom.Focused And grvZoom.RowCount > 0 Then
        e.Handled = True
        cmdSeleziona_Click(Me, Nothing)
      End If
      If edDescr.Focused Then
        cmdRicerca_Click(Me, Nothing)

        '---------------------------------------------------
        'faccio in modo che la pressione dell'enter non scateni l'emulazione del tasto TAB
        Me.NTSKeyDownEnterNotEmulateTabNow()
        'SendKeys.Send("+{TAB}")   'devo risalire di un tab, visto che prima di arrivare in questa routine è stata chiamata la KeyDown di FRM__CHIL che ha spostato il focus nel controllo successivo ...
      End If

      Return
    End If

    If e.KeyValue > 40 And e.KeyValue < 112 And e.Alt = False And e.Control = False And e.Shift = False And grvZoom.Focused = True Then
      edDescr.Focus()
      edDescr.SelectAll()
      If e.KeyValue >= 96 And e.KeyValue <= 105 Then      'tastiera numerica
        edDescr.Text = e.KeyCode.ToString.Substring(6)
      ElseIf e.KeyValue >= 48 And e.KeyValue <= 57 Then   'numeri sopra a tastiera alfanumerica
        edDescr.Text = e.KeyCode.ToString.Substring(1)
      Else
        edDescr.Text = e.KeyCode.ToString
      End If
      edDescr.SelectionStart = edDescr.Text.Length
      e.Handled = True        'se non mettoquesta riga dopo aver compilato edDescr intercetta nuovamente la KeyDown e fa partire subito la ricerca ...
    End If
  End Sub

  Public Overridable Sub FRM__HLTB_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    If ckOttimistico.Checked Then
      oMenu.SaveSettingBus("BN__HLTB", "RECENT", ".", "Ottimistico_" & strNomeZoom, "-1", " ", "NS.", "...", "...")
    Else
      oMenu.SaveSettingBus("BN__HLTB", "RECENT", ".", "Ottimistico_" & strNomeZoom, "0", " ", "NS.", "...", "...")
    End If
  End Sub
#End Region

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdGestione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGestione.Click
    Dim strServer As String = ""
    Dim strNomeDll As String = ""
    Dim strPrgGest As String = ""
    Dim strParam As String = ""
    Dim dttTmp As New DataTable

    Try
      '---------------------------------
      'ottengo il nome del programma per la gestione
      strPrgGest = NTSZOOM.GetNomeProgForGest(strNomeZoom)
      If strPrgGest = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222145156250, "Funzione GESTIONE non abilitata per il programma '|" & strNomeZoom & "|'"))
        Return
      End If

      '--------------------------
      Select Case strNomeZoom
        Case "ZOOMANAGPC"
          If grvZoom.RowCount = 0 Then
            strParam = "NUOV;" & oParam.strCodPdc.PadRight(12) & "0"
          Else
            strParam = "APRI;" & oParam.strCodPdc.PadRight(12) & ";" & NTSCInt(grvZoom.GetFocusedRowCellDisplayText("tb_codice")).ToString("000000000")
          End If
        Case "ZOOMVALVARIESPL", "ZOOMVALVARIESPLTC"
          If grvZoom.RowCount = 0 Then
            strParam = "NUOV;" & "0"
          Else
            strParam = "APRI;" & oParam.strCodPdc & grvZoom.GetFocusedRowCellDisplayText("tb_codice")
          End If
        Case "ZOOMARTFASI"
          'devo aprire BNMGARTI o BNTCARTV passando la fase
          'prima devo capire se l'articolo è TC o normale
          oMenu.ValCodiceDb(oParam.strTipo, DittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" Then
              If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
                'articolo TCO
                strPrgGest = "BNTCARTV"
              Else
                'articolo NORMALE
                strPrgGest = "BNMGARTI"
              End If
              If grvZoom.RowCount = 0 Then
                strParam = "APRI_F;" & oParam.strTipo & ";0"
              Else
                strParam = "APRI_F;" & oParam.strTipo & ";" & grvZoom.GetFocusedRowCellDisplayText("tb_codice")
              End If
            Else
              oApp.MsgBoxErr(oApp.Tr(Me, 130371020168544968, "Articolo '|" & oParam.strTipo & "|' non gestito a fasi"))
              Return
            End If
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 130371019394968969, "Articolo '|" & oParam.strTipo & "|' non trovato in anagrafica articoli"))
            Return
          End If

        Case Else
          If grvZoom.RowCount = 0 Then
            strParam = "NUOV;" & "0"
          Else
            strParam = "APRI;" & grvZoom.GetFocusedRowCellDisplayText("tb_codice")
          End If
      End Select

      '--------------------------
      'verifico se programma child è COM o NET
      If strPrgGest.Substring(0, 3).ToUpper <> "CLS" Then
        'QUI PASSA SOLO PER I Child NET MAI SCTRITTI IN vb6
        strNomeDll = strPrgGest
        strPrgGest = "FRM" & strPrgGest.Substring(2)
        strServer = "NTSInformatica"

        oMenu.RunChild(strServer, strPrgGest, "", DittaCorrente, "", strNomeDll, Nothing, strParam, True, True)
      Else
        'Child COM
        strServer = strPrgGest.Replace("CLS", "BS")
        oMenu.RunChild(strServer, strPrgGest, "", DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      If edDescr.Text.Trim <> "" Or grvZoom.RowCount > 0 Then cmdRicerca_Click(cmdRicerca, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
      If strNomeZoom = "ZOOMARTCLAS" Then
        oParam.strOut = NTSCStr(grvZoom.NTSGetCurrentDataRow!tb_codice1)
      Else
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("tb_codice")
      End If
    End If
    Me.Close()
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim i As Integer

    Try
      dsHltb.Clear()
      If ckOttimistico.Checked = True And bSelectIfOneRow = False Then edDescr.Text = "*" & edDescr.Text & "*"
      If edDescr.Text = "**" Then edDescr.Text = ""
      edDescr.Text = edDescr.Text.Replace("**", "*")

      If strNomeZoom = "AUTOCOMPLETE" Then
        'autocompletamento da busnet_is: mi è stato passato già il dataset
        'parametri passati da BN__CHIL nella routine IS_ShowAutocomplete
        'Dim oFiltriTb As New CLE__PATB
        'oFiltriTb.strDescr = strValue
        'oFiltriTb.strTipo = strTabName
        'oFiltriTb.bTipoProposto = bIsCrmUser
        NTSGetDataAutocompletamento(oParam.strTipo, IIf(edDescr.Text = "", "-.-", edDescr.Text).ToString, oParam.bTipoProposto, dsHltb)
        dsHltb.Tables(0).Columns("tb_cod").ColumnName = "tb_codice"
        dsHltb.Tables(0).Columns("tb_desc").ColumnName = "tb_descr"
        dtcHltb.DataSource = dsHltb.Tables(0)
        bSelectIfOneRow = False
      Else
        'caso standard
        oCleHltb.Apri(strNomeZoom, dsHltb, DittaCorrente, edDescr.Text.Replace("?", "_").Replace("*", "%"), _
              oParam.strCodPdc, oParam.nAnno, oParam.nEscomp, oParam.lContoCF, oParam.lCommessa, _
              oParam.strTipo, ckIgnoraEsclusioni.Checked)
        dtcHltb.DataSource = dsHltb.Tables(strNomeZoom)
      End If

      dsHltb.AcceptChanges()
      grZoom.DataSource = dtcHltb

      'allineamento della colonna codice è automatico: a sx se non è numerico, diversamente a dx
      If IsColumnsInteger(dsHltb.Tables(0).Columns("tb_codice")) Then
        Dim lColWidth As Integer = tb_codice.Width
        tb_codice.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130966363711484361, "Codice"), "0", 999999999)
        tb_codice.Width = lColWidth
      ElseIf IsColumnsDecimal(dsHltb.Tables(0).Columns("tb_codice")) Then
        Dim lColWidth As Integer = tb_codice.Width
        tb_codice.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130966365012742288, "Codice"), oApp.FormatQta, 999999999)
        tb_codice.Width = lColWidth
      End If

      'mi sposto sul valore passato in input dal campo (solo la prima volta, quando lo zoom viene chiamato dal child)
      If oParam.strIn <> "" And oParam.strIn <> "0" Then
        For i = 0 To dtcHltb.List.Count - 1
          If CType(dtcHltb.Item(i), DataRowView)(strNomeCampoCod).ToString = oParam.strIn Then
            dtcHltb.Position = i
            grvZoom.FocusedColumn = tb_codice
            Exit For
          End If
        Next
        oParam.strIn = ""
      End If

      'se è impostato di selezionare subito l'unica riga restituita lo faccio
      If bSelectIfOneRow And grvZoom.RowCount = 1 Then
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("tb_codice")
        Me.Close()
      End If

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If oApp.bTouchScreen = False And CLN__STD.IsBis = False Then
        'in modalità douch da errore non spiegato ...
        If grvZoom.RowCount > 0 Then grvZoom.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdEsclusioni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsclusioni.Click
    Dim frmEscl As FRM__ESCL = Nothing
    Try

      oCleHltb.strTabname = strNomeZoom.Substring(4)
      frmEscl = CType(NTSNewFormModal("FRM__ESCL"), FRM__ESCL)
      If Not frmEscl.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmEscl.InitEntity(oCleHltb)
      frmEscl.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      'senza le due righe sotto la form non viene scaricata dalla memoria
      If Not frmEscl Is Nothing Then frmEscl.Dispose()
      frmEscl = Nothing
    End Try
  End Sub

  Public Overridable Sub ckIgnoraEsclusioni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckIgnoraEsclusioni.CheckedChanged
    Try
      cmdRicerca_Click(cmdRicerca, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub


  Public Overridable Function IsColumnsInteger(ByVal oCol As DataColumn) As Boolean
    Try
      If oCol Is Nothing Then Return False

      Dim oTypes As Type() = {GetType(Byte), GetType(Int16), GetType(Int32), GetType(Int64), GetType(SByte), GetType(UInt16), GetType(UInt32), GetType(UInt64)}

      For Each oType As Type In oTypes
        If oCol.DataType.ToString = oType.ToString Then Return True
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    Return False
  End Function
  Public Overridable Function IsColumnsDecimal(ByVal oCol As DataColumn) As Boolean
    Try
      If oCol Is Nothing Then Return False

      Dim oTypes As Type() = {GetType(Decimal), GetType(Double), GetType(Single)}

      For Each oType As Type In oTypes
        If oCol.DataType.ToString = oType.ToString Then Return True
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    Return False
  End Function
End Class
