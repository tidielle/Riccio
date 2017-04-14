Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLCO

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleHlco As CLE__HLCO
  Public dsHlco As New DataSet
  Public dtcHlco As BindingSource = New BindingSource()

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, _
                                 ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", _
                                 Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean

    oMenu = Menu
    oApp = oMenu.App
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    If Ditta <> "" Then DittaCorrente = Ditta
    oParam = Param
    InitializeComponent()

    Me.MinimumSize = Me.Size


    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HLCO", "BE__HLCO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222142187500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlco = CType(oTmp, CLE__HLCO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HLTB", strRemoteServer, strRemotePort)
    oCleHlco.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlco.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAccperi As New DataTable
    Try
      dttAccperi.Columns.Add("cod", GetType(String))
      dttAccperi.Columns.Add("val", GetType(String))
      dttAccperi.Rows.Add(New Object() {"N", "(Nessuna)"})
      dttAccperi.Rows.Add(New Object() {"S", "Periodo di compet. econ."})
      dttAccperi.Rows.Add(New Object() {"D", "Data compet. Econ."})
      dttAccperi.Rows.Add(New Object() {"I", "Data compet. Iva"})
      dttAccperi.Rows.Add(New Object() {"V", "Data Valuta"})
      dttAccperi.AcceptChanges()


      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edFax.NTSSetParam(oMenu, oApp.Tr(Me, 128230023413386167, "Fax"), 18, True)
      edTelef.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414323205, "Telefono"), 18, True)
      edEmail.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414479378, "E-mail"), 100, True)
      edComune.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414635551, "Comune"), 28, True)
      edNome.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414791724, "Sigla di ricerca"), 30, True)
      edCognome.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415416416, "Ragione sociale / Descrizione"), 30, True)
      ckOttimistico.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415572589, "Ottimistico"), "S", "N")
      edStato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023417602838, "Codice stato estero"), tabstat, True)
      edProvincia.NTSSetParam(oMenu, oApp.Tr(Me, 128230023418227530, "Provincia DA"), 2, True)
      edCap.NTSSetParam(oMenu, oApp.Tr(Me, 128230023419633087, "Codice CAP DA"), 9, True)
      cbStatus.NTSSetParam(oApp.Tr(Me, 130465402698908371, "Status Commerciale"))
      edDataNasc.NTSSetParam(oMenu, oApp.Tr(Me, 130465402699064902, "Data nascita"), True)
      edTwitter.NTSSetParam(oMenu, oApp.Tr(Me, 130465402699221429, "Twitter"), 0)
      edFacebook.NTSSetParam(oMenu, oApp.Tr(Me, 130465402699377964, "Facebook"), 0)
      edCellulare.NTSSetParam(oMenu, oApp.Tr(Me, 130465402702665152, "Cellulare"), 0)

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia Contatti"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      co_titolo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023422600374, "Codice"), "0", 9)
      co_descont.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023422756547, "Descrizione"), 50, True)
      co_descont2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023422912720, "Descrizione 2"), 50, True)
      co_indir.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423068893, "Città"), 50, True)
      co_cap.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423225066, "Telefono"), 50, True)
      co_citta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423381239, "Fax"), 50, True)
      co_prov.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423537412, "Partita IVA"), 20, True)
      co_stato.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423693585, "Codice Fiscale"), 20, True)
      co_datnasc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423849758, "Contatto"), 50, True)
      co_fbuser.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129248615509326172, "Gestione contabilità analitica"), "1", " ")
      co_twitteruser.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129248615974765625, "Richiedi date"), dttAccperi, "val", "cod")
      co_emailpers.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465402684037736, "E-mail"), 0, True)
      co_telefpers.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465402684194272, "Telefono"), 0, True)
      co_cellpers.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465402684350803, "Cellulare"), 0, True)
      co_faxpers.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465402684507329, "FAX"), 0, True)
      xx_codstco.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465402685916125, "Status Commerciale"), 0, True)
      grvZoom.Enabled = False
      '-------------------------------------------------
      'setto il recent
      ckOttimistico.Checked = CBool(oMenu.GetSettingBus("BN__HLCO", "RECENT", ".", "Ottimistico", "-1", " ", "-1"))

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


  Public Overridable Function CaricaCombo() As Boolean
    Dim dttStatus As DataTable = Nothing
    Try
      dttStatus = oCleHlco.CaricaStatusCommercialiCombo()

      cbStatus.DataSource = dttStatus
      cbStatus.DisplayMember = "val"
      cbStatus.ValueMember = "cod"

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub InitializeComponent()
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.pnTab1Pan1 = New NTSInformatica.NTSPanel
    Me.cbStatus = New NTSInformatica.NTSComboBox
    Me.edDataNasc = New NTSInformatica.NTSTextBoxData
    Me.edTwitter = New NTSInformatica.NTSTextBoxStr
    Me.edFacebook = New NTSInformatica.NTSTextBoxStr
    Me.lbTwitter = New NTSInformatica.NTSLabel
    Me.lbFacebook = New NTSInformatica.NTSLabel
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.lbDataNascita = New NTSInformatica.NTSLabel
    Me.edCognome = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel8 = New NTSInformatica.NTSLabel
    Me.edEmail = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.edComune = New NTSInformatica.NTSTextBoxStr
    Me.edNome = New NTSInformatica.NTSTextBoxStr
    Me.edStato = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel22 = New NTSInformatica.NTSLabel
    Me.edCap = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel14 = New NTSInformatica.NTSLabel
    Me.edProvincia = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel13 = New NTSInformatica.NTSLabel
    Me.edFax = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel7 = New NTSInformatica.NTSLabel
    Me.lbCell = New NTSInformatica.NTSLabel
    Me.NtsLabel6 = New NTSInformatica.NTSLabel
    Me.edCellulare = New NTSInformatica.NTSTextBoxStr
    Me.edTelef = New NTSInformatica.NTSTextBoxStr
    Me.lbNome = New NTSInformatica.NTSLabel
    Me.lbCognome = New NTSInformatica.NTSLabel
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.ckOttimistico = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.co_titolo = New NTSInformatica.NTSGridColumn
    Me.co_descont = New NTSInformatica.NTSGridColumn
    Me.co_descont2 = New NTSInformatica.NTSGridColumn
    Me.co_emailpers = New NTSInformatica.NTSGridColumn
    Me.co_telefpers = New NTSInformatica.NTSGridColumn
    Me.co_cellpers = New NTSInformatica.NTSGridColumn
    Me.co_faxpers = New NTSInformatica.NTSGridColumn
    Me.co_indir = New NTSInformatica.NTSGridColumn
    Me.co_cap = New NTSInformatica.NTSGridColumn
    Me.co_citta = New NTSInformatica.NTSGridColumn
    Me.co_prov = New NTSInformatica.NTSGridColumn
    Me.co_stato = New NTSInformatica.NTSGridColumn
    Me.co_datnasc = New NTSInformatica.NTSGridColumn
    Me.co_fbuser = New NTSInformatica.NTSGridColumn
    Me.co_twitteruser = New NTSInformatica.NTSGridColumn
    Me.xx_codstco = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1Pan1.SuspendLayout()
    CType(Me.cbStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataNasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTwitter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFacebook.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCognome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edComune.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edStato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvincia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCellulare.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTelef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.pnTab1Pan1)
    Me.pnDescr.Controls.Add(Me.pnAction)
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(738, 154)
    Me.pnDescr.TabIndex = 1
    '
    'pnTab1Pan1
    '
    Me.pnTab1Pan1.AllowDrop = True
    Me.pnTab1Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab1Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1Pan1.Controls.Add(Me.cbStatus)
    Me.pnTab1Pan1.Controls.Add(Me.edDataNasc)
    Me.pnTab1Pan1.Controls.Add(Me.edTwitter)
    Me.pnTab1Pan1.Controls.Add(Me.edFacebook)
    Me.pnTab1Pan1.Controls.Add(Me.lbTwitter)
    Me.pnTab1Pan1.Controls.Add(Me.lbFacebook)
    Me.pnTab1Pan1.Controls.Add(Me.lbStatus)
    Me.pnTab1Pan1.Controls.Add(Me.lbDataNascita)
    Me.pnTab1Pan1.Controls.Add(Me.edCognome)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel8)
    Me.pnTab1Pan1.Controls.Add(Me.edEmail)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel5)
    Me.pnTab1Pan1.Controls.Add(Me.edComune)
    Me.pnTab1Pan1.Controls.Add(Me.edNome)
    Me.pnTab1Pan1.Controls.Add(Me.edStato)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel22)
    Me.pnTab1Pan1.Controls.Add(Me.edCap)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel14)
    Me.pnTab1Pan1.Controls.Add(Me.edProvincia)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel13)
    Me.pnTab1Pan1.Controls.Add(Me.edFax)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel7)
    Me.pnTab1Pan1.Controls.Add(Me.lbCell)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel6)
    Me.pnTab1Pan1.Controls.Add(Me.edCellulare)
    Me.pnTab1Pan1.Controls.Add(Me.edTelef)
    Me.pnTab1Pan1.Controls.Add(Me.lbNome)
    Me.pnTab1Pan1.Controls.Add(Me.lbCognome)
    Me.pnTab1Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1Pan1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTab1Pan1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab1Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab1Pan1.Name = "pnTab1Pan1"
    Me.pnTab1Pan1.NTSActiveTrasparency = True
    Me.pnTab1Pan1.Size = New System.Drawing.Size(628, 154)
    Me.pnTab1Pan1.TabIndex = 2
    '
    'cbStatus
    '
    Me.cbStatus.DataSource = Nothing
    Me.cbStatus.DisplayMember = ""
    Me.cbStatus.Location = New System.Drawing.Point(68, 30)
    Me.cbStatus.Name = "cbStatus"
    Me.cbStatus.NTSDbField = ""
    Me.cbStatus.Properties.AutoHeight = False
    Me.cbStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbStatus.Properties.DropDownRows = 30
    Me.cbStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbStatus.SelectedValue = ""
    Me.cbStatus.Size = New System.Drawing.Size(249, 20)
    Me.cbStatus.TabIndex = 67
    Me.cbStatus.ValueMember = ""
    '
    'edDataNasc
    '
    Me.edDataNasc.EditValue = ""
    Me.edDataNasc.Location = New System.Drawing.Point(382, 30)
    Me.edDataNasc.Name = "edDataNasc"
    Me.edDataNasc.NTSDbField = ""
    Me.edDataNasc.NTSForzaVisZoom = False
    Me.edDataNasc.NTSOldValue = ""
    Me.edDataNasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataNasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataNasc.Properties.AutoHeight = False
    Me.edDataNasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataNasc.Properties.MaxLength = 65536
    Me.edDataNasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataNasc.Size = New System.Drawing.Size(80, 20)
    Me.edDataNasc.TabIndex = 66
    '
    'edTwitter
    '
    Me.edTwitter.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTwitter.Location = New System.Drawing.Point(382, 127)
    Me.edTwitter.Name = "edTwitter"
    Me.edTwitter.NTSDbField = ""
    Me.edTwitter.NTSForzaVisZoom = False
    Me.edTwitter.NTSOldValue = ""
    Me.edTwitter.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTwitter.Properties.Appearance.Options.UseBackColor = True
    Me.edTwitter.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTwitter.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTwitter.Properties.AutoHeight = False
    Me.edTwitter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTwitter.Properties.MaxLength = 65536
    Me.edTwitter.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTwitter.Size = New System.Drawing.Size(240, 20)
    Me.edTwitter.TabIndex = 65
    '
    'edFacebook
    '
    Me.edFacebook.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFacebook.Location = New System.Drawing.Point(68, 127)
    Me.edFacebook.Name = "edFacebook"
    Me.edFacebook.NTSDbField = ""
    Me.edFacebook.NTSForzaVisZoom = False
    Me.edFacebook.NTSOldValue = ""
    Me.edFacebook.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFacebook.Properties.Appearance.Options.UseBackColor = True
    Me.edFacebook.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFacebook.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFacebook.Properties.AutoHeight = False
    Me.edFacebook.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFacebook.Properties.MaxLength = 65536
    Me.edFacebook.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFacebook.Size = New System.Drawing.Size(249, 20)
    Me.edFacebook.TabIndex = 64
    '
    'lbTwitter
    '
    Me.lbTwitter.AutoSize = True
    Me.lbTwitter.BackColor = System.Drawing.Color.Transparent
    Me.lbTwitter.Location = New System.Drawing.Point(326, 130)
    Me.lbTwitter.Name = "lbTwitter"
    Me.lbTwitter.NTSDbField = ""
    Me.lbTwitter.Size = New System.Drawing.Size(41, 13)
    Me.lbTwitter.TabIndex = 63
    Me.lbTwitter.Text = "Twitter"
    Me.lbTwitter.Tooltip = ""
    Me.lbTwitter.UseMnemonic = False
    '
    'lbFacebook
    '
    Me.lbFacebook.AutoSize = True
    Me.lbFacebook.BackColor = System.Drawing.Color.Transparent
    Me.lbFacebook.Location = New System.Drawing.Point(10, 130)
    Me.lbFacebook.Name = "lbFacebook"
    Me.lbFacebook.NTSDbField = ""
    Me.lbFacebook.Size = New System.Drawing.Size(53, 13)
    Me.lbFacebook.TabIndex = 63
    Me.lbFacebook.Text = "Facebook"
    Me.lbFacebook.Tooltip = ""
    Me.lbFacebook.UseMnemonic = False
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(10, 33)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(38, 13)
    Me.lbStatus.TabIndex = 63
    Me.lbStatus.Text = "Status"
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'lbDataNascita
    '
    Me.lbDataNascita.AutoSize = True
    Me.lbDataNascita.BackColor = System.Drawing.Color.Transparent
    Me.lbDataNascita.Location = New System.Drawing.Point(326, 33)
    Me.lbDataNascita.Name = "lbDataNascita"
    Me.lbDataNascita.NTSDbField = ""
    Me.lbDataNascita.Size = New System.Drawing.Size(37, 13)
    Me.lbDataNascita.TabIndex = 63
    Me.lbDataNascita.Text = "Nato il"
    Me.lbDataNascita.Tooltip = ""
    Me.lbDataNascita.UseMnemonic = False
    '
    'edCognome
    '
    Me.edCognome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCognome.Location = New System.Drawing.Point(68, 6)
    Me.edCognome.Name = "edCognome"
    Me.edCognome.NTSDbField = ""
    Me.edCognome.NTSForzaVisZoom = False
    Me.edCognome.NTSOldValue = ""
    Me.edCognome.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCognome.Properties.Appearance.Options.UseBackColor = True
    Me.edCognome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCognome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCognome.Properties.AutoHeight = False
    Me.edCognome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCognome.Properties.MaxLength = 65536
    Me.edCognome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCognome.Size = New System.Drawing.Size(249, 20)
    Me.edCognome.TabIndex = 14
    '
    'NtsLabel8
    '
    Me.NtsLabel8.AutoSize = True
    Me.NtsLabel8.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel8.Location = New System.Drawing.Point(10, 59)
    Me.NtsLabel8.Name = "NtsLabel8"
    Me.NtsLabel8.NTSDbField = ""
    Me.NtsLabel8.Size = New System.Drawing.Size(35, 13)
    Me.NtsLabel8.TabIndex = 13
    Me.NtsLabel8.Text = "E-mail"
    Me.NtsLabel8.Tooltip = ""
    Me.NtsLabel8.UseMnemonic = False
    '
    'edEmail
    '
    Me.edEmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEmail.Location = New System.Drawing.Point(68, 54)
    Me.edEmail.Name = "edEmail"
    Me.edEmail.NTSDbField = ""
    Me.edEmail.NTSForzaVisZoom = False
    Me.edEmail.NTSOldValue = ""
    Me.edEmail.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEmail.Properties.Appearance.Options.UseBackColor = True
    Me.edEmail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEmail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEmail.Properties.AutoHeight = False
    Me.edEmail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEmail.Properties.MaxLength = 65536
    Me.edEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEmail.Size = New System.Drawing.Size(249, 20)
    Me.edEmail.TabIndex = 12
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(144, 106)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(30, 13)
    Me.NtsLabel5.TabIndex = 11
    Me.NtsLabel5.Text = "Città"
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'edComune
    '
    Me.edComune.Cursor = System.Windows.Forms.Cursors.Default
    Me.edComune.Location = New System.Drawing.Point(180, 103)
    Me.edComune.Name = "edComune"
    Me.edComune.NTSDbField = ""
    Me.edComune.NTSForzaVisZoom = False
    Me.edComune.NTSOldValue = ""
    Me.edComune.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edComune.Properties.Appearance.Options.UseBackColor = True
    Me.edComune.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edComune.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edComune.Properties.AutoHeight = False
    Me.edComune.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edComune.Properties.MaxLength = 65536
    Me.edComune.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edComune.Size = New System.Drawing.Size(137, 20)
    Me.edComune.TabIndex = 10
    '
    'edNome
    '
    Me.edNome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNome.Location = New System.Drawing.Point(382, 6)
    Me.edNome.Name = "edNome"
    Me.edNome.NTSDbField = ""
    Me.edNome.NTSForzaVisZoom = False
    Me.edNome.NTSOldValue = ""
    Me.edNome.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNome.Properties.Appearance.Options.UseBackColor = True
    Me.edNome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNome.Properties.AutoHeight = False
    Me.edNome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNome.Properties.MaxLength = 65536
    Me.edNome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNome.Size = New System.Drawing.Size(240, 20)
    Me.edNome.TabIndex = 9
    '
    'edStato
    '
    Me.edStato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edStato.Location = New System.Drawing.Point(542, 103)
    Me.edStato.Name = "edStato"
    Me.edStato.NTSDbField = ""
    Me.edStato.NTSForzaVisZoom = False
    Me.edStato.NTSOldValue = ""
    Me.edStato.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edStato.Properties.Appearance.Options.UseBackColor = True
    Me.edStato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edStato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edStato.Properties.AutoHeight = False
    Me.edStato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edStato.Properties.MaxLength = 65536
    Me.edStato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edStato.Size = New System.Drawing.Size(80, 20)
    Me.edStato.TabIndex = 62
    '
    'NtsLabel22
    '
    Me.NtsLabel22.AutoSize = True
    Me.NtsLabel22.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel22.Location = New System.Drawing.Point(499, 106)
    Me.NtsLabel22.Name = "NtsLabel22"
    Me.NtsLabel22.NTSDbField = ""
    Me.NtsLabel22.Size = New System.Drawing.Size(33, 13)
    Me.NtsLabel22.TabIndex = 61
    Me.NtsLabel22.Text = "Stato"
    Me.NtsLabel22.Tooltip = ""
    Me.NtsLabel22.UseMnemonic = False
    '
    'edCap
    '
    Me.edCap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCap.Location = New System.Drawing.Point(68, 103)
    Me.edCap.Name = "edCap"
    Me.edCap.NTSDbField = ""
    Me.edCap.NTSForzaVisZoom = False
    Me.edCap.NTSOldValue = ""
    Me.edCap.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCap.Properties.Appearance.Options.UseBackColor = True
    Me.edCap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCap.Properties.AutoHeight = False
    Me.edCap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCap.Properties.MaxLength = 65536
    Me.edCap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCap.Size = New System.Drawing.Size(69, 20)
    Me.edCap.TabIndex = 27
    '
    'NtsLabel14
    '
    Me.NtsLabel14.AutoSize = True
    Me.NtsLabel14.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel14.Location = New System.Drawing.Point(10, 106)
    Me.NtsLabel14.Name = "NtsLabel14"
    Me.NtsLabel14.NTSDbField = ""
    Me.NtsLabel14.Size = New System.Drawing.Size(26, 13)
    Me.NtsLabel14.TabIndex = 26
    Me.NtsLabel14.Text = "Cap"
    Me.NtsLabel14.Tooltip = ""
    Me.NtsLabel14.UseMnemonic = False
    '
    'edProvincia
    '
    Me.edProvincia.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvincia.Location = New System.Drawing.Point(382, 103)
    Me.edProvincia.Name = "edProvincia"
    Me.edProvincia.NTSDbField = ""
    Me.edProvincia.NTSForzaVisZoom = False
    Me.edProvincia.NTSOldValue = ""
    Me.edProvincia.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvincia.Properties.Appearance.Options.UseBackColor = True
    Me.edProvincia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvincia.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvincia.Properties.AutoHeight = False
    Me.edProvincia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvincia.Properties.MaxLength = 65536
    Me.edProvincia.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvincia.Size = New System.Drawing.Size(59, 20)
    Me.edProvincia.TabIndex = 25
    '
    'NtsLabel13
    '
    Me.NtsLabel13.AutoSize = True
    Me.NtsLabel13.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel13.Location = New System.Drawing.Point(326, 106)
    Me.NtsLabel13.Name = "NtsLabel13"
    Me.NtsLabel13.NTSDbField = ""
    Me.NtsLabel13.Size = New System.Drawing.Size(50, 13)
    Me.NtsLabel13.TabIndex = 24
    Me.NtsLabel13.Text = "Provincia"
    Me.NtsLabel13.Tooltip = ""
    Me.NtsLabel13.UseMnemonic = False
    '
    'edFax
    '
    Me.edFax.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFax.Location = New System.Drawing.Point(382, 54)
    Me.edFax.Name = "edFax"
    Me.edFax.NTSDbField = ""
    Me.edFax.NTSForzaVisZoom = False
    Me.edFax.NTSOldValue = ""
    Me.edFax.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFax.Properties.Appearance.Options.UseBackColor = True
    Me.edFax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFax.Properties.AutoHeight = False
    Me.edFax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFax.Properties.MaxLength = 65536
    Me.edFax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFax.Size = New System.Drawing.Size(240, 20)
    Me.edFax.TabIndex = 15
    '
    'NtsLabel7
    '
    Me.NtsLabel7.AutoSize = True
    Me.NtsLabel7.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel7.Location = New System.Drawing.Point(326, 57)
    Me.NtsLabel7.Name = "NtsLabel7"
    Me.NtsLabel7.NTSDbField = ""
    Me.NtsLabel7.Size = New System.Drawing.Size(25, 13)
    Me.NtsLabel7.TabIndex = 14
    Me.NtsLabel7.Text = "Fax"
    Me.NtsLabel7.Tooltip = ""
    Me.NtsLabel7.UseMnemonic = False
    '
    'lbCell
    '
    Me.lbCell.AutoSize = True
    Me.lbCell.BackColor = System.Drawing.Color.Transparent
    Me.lbCell.Location = New System.Drawing.Point(326, 82)
    Me.lbCell.Name = "lbCell"
    Me.lbCell.NTSDbField = ""
    Me.lbCell.Size = New System.Drawing.Size(48, 13)
    Me.lbCell.TabIndex = 13
    Me.lbCell.Text = "Cellulare"
    Me.lbCell.Tooltip = ""
    Me.lbCell.UseMnemonic = False
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(10, 82)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSDbField = ""
    Me.NtsLabel6.Size = New System.Drawing.Size(49, 13)
    Me.NtsLabel6.TabIndex = 13
    Me.NtsLabel6.Text = "Telefono"
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'edCellulare
    '
    Me.edCellulare.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCellulare.Location = New System.Drawing.Point(382, 79)
    Me.edCellulare.Name = "edCellulare"
    Me.edCellulare.NTSDbField = ""
    Me.edCellulare.NTSForzaVisZoom = False
    Me.edCellulare.NTSOldValue = ""
    Me.edCellulare.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCellulare.Properties.Appearance.Options.UseBackColor = True
    Me.edCellulare.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCellulare.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCellulare.Properties.AutoHeight = False
    Me.edCellulare.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCellulare.Properties.MaxLength = 65536
    Me.edCellulare.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCellulare.Size = New System.Drawing.Size(240, 20)
    Me.edCellulare.TabIndex = 12
    '
    'edTelef
    '
    Me.edTelef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTelef.Location = New System.Drawing.Point(68, 79)
    Me.edTelef.Name = "edTelef"
    Me.edTelef.NTSDbField = ""
    Me.edTelef.NTSForzaVisZoom = False
    Me.edTelef.NTSOldValue = ""
    Me.edTelef.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTelef.Properties.Appearance.Options.UseBackColor = True
    Me.edTelef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTelef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTelef.Properties.AutoHeight = False
    Me.edTelef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTelef.Properties.MaxLength = 65536
    Me.edTelef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTelef.Size = New System.Drawing.Size(249, 20)
    Me.edTelef.TabIndex = 12
    '
    'lbNome
    '
    Me.lbNome.AutoSize = True
    Me.lbNome.BackColor = System.Drawing.Color.Transparent
    Me.lbNome.Location = New System.Drawing.Point(326, 9)
    Me.lbNome.Name = "lbNome"
    Me.lbNome.NTSDbField = ""
    Me.lbNome.Size = New System.Drawing.Size(34, 13)
    Me.lbNome.TabIndex = 2
    Me.lbNome.Text = "Nome"
    Me.lbNome.Tooltip = ""
    Me.lbNome.UseMnemonic = False
    '
    'lbCognome
    '
    Me.lbCognome.AutoSize = True
    Me.lbCognome.BackColor = System.Drawing.Color.Transparent
    Me.lbCognome.Location = New System.Drawing.Point(10, 9)
    Me.lbCognome.Name = "lbCognome"
    Me.lbCognome.NTSDbField = ""
    Me.lbCognome.Size = New System.Drawing.Size(52, 13)
    Me.lbCognome.TabIndex = 1
    Me.lbCognome.Text = "Cognome"
    Me.lbCognome.Tooltip = ""
    Me.lbCognome.UseMnemonic = False
    '
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.ckOttimistico)
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Controls.Add(Me.cmdRicerca)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(628, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(110, 154)
    Me.pnAction.TabIndex = 3
    '
    'ckOttimistico
    '
    Me.ckOttimistico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOttimistico.Location = New System.Drawing.Point(6, 69)
    Me.ckOttimistico.Name = "ckOttimistico"
    Me.ckOttimistico.NTSCheckValue = "S"
    Me.ckOttimistico.NTSUnCheckValue = "N"
    Me.ckOttimistico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOttimistico.Properties.Appearance.Options.UseBackColor = True
    Me.ckOttimistico.Properties.AutoHeight = False
    Me.ckOttimistico.Properties.Caption = "&Ottimistico"
    Me.ckOttimistico.Size = New System.Drawing.Size(74, 19)
    Me.ckOttimistico.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 125)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(97, 24)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 37)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(97, 24)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 8)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(97, 24)
    Me.cmdRicerca.TabIndex = 0
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 154)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(738, 289)
    Me.grZoom.TabIndex = 2
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.co_titolo, Me.co_descont, Me.co_descont2, Me.co_emailpers, Me.co_telefpers, Me.co_cellpers, Me.co_faxpers, Me.co_indir, Me.co_cap, Me.co_citta, Me.co_prov, Me.co_stato, Me.co_datnasc, Me.co_fbuser, Me.co_twitteruser, Me.xx_codstco})
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
    'co_titolo
    '
    Me.co_titolo.AppearanceCell.Options.UseBackColor = True
    Me.co_titolo.AppearanceCell.Options.UseTextOptions = True
    Me.co_titolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_titolo.Caption = "Titolo"
    Me.co_titolo.Enabled = True
    Me.co_titolo.FieldName = "co_titolo"
    Me.co_titolo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_titolo.Name = "co_titolo"
    Me.co_titolo.NTSRepositoryComboBox = Nothing
    Me.co_titolo.NTSRepositoryItemCheck = Nothing
    Me.co_titolo.NTSRepositoryItemMemo = Nothing
    Me.co_titolo.NTSRepositoryItemText = Nothing
    Me.co_titolo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_titolo.OptionsFilter.AllowFilter = False
    Me.co_titolo.Visible = True
    Me.co_titolo.VisibleIndex = 0
    Me.co_titolo.Width = 67
    '
    'co_descont
    '
    Me.co_descont.AppearanceCell.Options.UseBackColor = True
    Me.co_descont.AppearanceCell.Options.UseTextOptions = True
    Me.co_descont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_descont.Caption = "Cognome"
    Me.co_descont.Enabled = True
    Me.co_descont.FieldName = "co_descont"
    Me.co_descont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_descont.Name = "co_descont"
    Me.co_descont.NTSRepositoryComboBox = Nothing
    Me.co_descont.NTSRepositoryItemCheck = Nothing
    Me.co_descont.NTSRepositoryItemMemo = Nothing
    Me.co_descont.NTSRepositoryItemText = Nothing
    Me.co_descont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_descont.OptionsFilter.AllowFilter = False
    Me.co_descont.Visible = True
    Me.co_descont.VisibleIndex = 1
    Me.co_descont.Width = 220
    '
    'co_descont2
    '
    Me.co_descont2.AppearanceCell.Options.UseBackColor = True
    Me.co_descont2.AppearanceCell.Options.UseTextOptions = True
    Me.co_descont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_descont2.Caption = "Nome"
    Me.co_descont2.Enabled = True
    Me.co_descont2.FieldName = "co_descont2"
    Me.co_descont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_descont2.Name = "co_descont2"
    Me.co_descont2.NTSRepositoryComboBox = Nothing
    Me.co_descont2.NTSRepositoryItemCheck = Nothing
    Me.co_descont2.NTSRepositoryItemMemo = Nothing
    Me.co_descont2.NTSRepositoryItemText = Nothing
    Me.co_descont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_descont2.OptionsFilter.AllowFilter = False
    Me.co_descont2.Visible = True
    Me.co_descont2.VisibleIndex = 2
    Me.co_descont2.Width = 177
    '
    'co_emailpers
    '
    Me.co_emailpers.AppearanceCell.Options.UseBackColor = True
    Me.co_emailpers.AppearanceCell.Options.UseTextOptions = True
    Me.co_emailpers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_emailpers.Caption = "E-mail"
    Me.co_emailpers.Enabled = True
    Me.co_emailpers.FieldName = "co_emailpers"
    Me.co_emailpers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_emailpers.Name = "co_emailpers"
    Me.co_emailpers.NTSRepositoryComboBox = Nothing
    Me.co_emailpers.NTSRepositoryItemCheck = Nothing
    Me.co_emailpers.NTSRepositoryItemMemo = Nothing
    Me.co_emailpers.NTSRepositoryItemText = Nothing
    Me.co_emailpers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_emailpers.OptionsFilter.AllowFilter = False
    Me.co_emailpers.Visible = True
    Me.co_emailpers.VisibleIndex = 13
    '
    'co_telefpers
    '
    Me.co_telefpers.AppearanceCell.Options.UseBackColor = True
    Me.co_telefpers.AppearanceCell.Options.UseTextOptions = True
    Me.co_telefpers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_telefpers.Caption = "Telefono"
    Me.co_telefpers.Enabled = True
    Me.co_telefpers.FieldName = "co_telefpers"
    Me.co_telefpers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_telefpers.Name = "co_telefpers"
    Me.co_telefpers.NTSRepositoryComboBox = Nothing
    Me.co_telefpers.NTSRepositoryItemCheck = Nothing
    Me.co_telefpers.NTSRepositoryItemMemo = Nothing
    Me.co_telefpers.NTSRepositoryItemText = Nothing
    Me.co_telefpers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_telefpers.OptionsFilter.AllowFilter = False
    Me.co_telefpers.Visible = True
    Me.co_telefpers.VisibleIndex = 11
    '
    'co_cellpers
    '
    Me.co_cellpers.AppearanceCell.Options.UseBackColor = True
    Me.co_cellpers.AppearanceCell.Options.UseTextOptions = True
    Me.co_cellpers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_cellpers.Caption = "Cellulare"
    Me.co_cellpers.Enabled = True
    Me.co_cellpers.FieldName = "co_cellpers"
    Me.co_cellpers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_cellpers.Name = "co_cellpers"
    Me.co_cellpers.NTSRepositoryComboBox = Nothing
    Me.co_cellpers.NTSRepositoryItemCheck = Nothing
    Me.co_cellpers.NTSRepositoryItemMemo = Nothing
    Me.co_cellpers.NTSRepositoryItemText = Nothing
    Me.co_cellpers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_cellpers.OptionsFilter.AllowFilter = False
    Me.co_cellpers.Visible = True
    Me.co_cellpers.VisibleIndex = 15
    '
    'co_faxpers
    '
    Me.co_faxpers.AppearanceCell.Options.UseBackColor = True
    Me.co_faxpers.AppearanceCell.Options.UseTextOptions = True
    Me.co_faxpers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_faxpers.Caption = "FAX"
    Me.co_faxpers.Enabled = True
    Me.co_faxpers.FieldName = "co_faxpers"
    Me.co_faxpers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_faxpers.Name = "co_faxpers"
    Me.co_faxpers.NTSRepositoryComboBox = Nothing
    Me.co_faxpers.NTSRepositoryItemCheck = Nothing
    Me.co_faxpers.NTSRepositoryItemMemo = Nothing
    Me.co_faxpers.NTSRepositoryItemText = Nothing
    Me.co_faxpers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_faxpers.OptionsFilter.AllowFilter = False
    Me.co_faxpers.Visible = True
    Me.co_faxpers.VisibleIndex = 12
    '
    'co_indir
    '
    Me.co_indir.AppearanceCell.Options.UseBackColor = True
    Me.co_indir.AppearanceCell.Options.UseTextOptions = True
    Me.co_indir.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_indir.Caption = "Indirizzo"
    Me.co_indir.Enabled = True
    Me.co_indir.FieldName = "co_indir"
    Me.co_indir.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_indir.Name = "co_indir"
    Me.co_indir.NTSRepositoryComboBox = Nothing
    Me.co_indir.NTSRepositoryItemCheck = Nothing
    Me.co_indir.NTSRepositoryItemMemo = Nothing
    Me.co_indir.NTSRepositoryItemText = Nothing
    Me.co_indir.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_indir.OptionsFilter.AllowFilter = False
    Me.co_indir.Visible = True
    Me.co_indir.VisibleIndex = 3
    Me.co_indir.Width = 126
    '
    'co_cap
    '
    Me.co_cap.AppearanceCell.Options.UseBackColor = True
    Me.co_cap.AppearanceCell.Options.UseTextOptions = True
    Me.co_cap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_cap.Caption = "CAP"
    Me.co_cap.Enabled = True
    Me.co_cap.FieldName = "co_cap"
    Me.co_cap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_cap.Name = "co_cap"
    Me.co_cap.NTSRepositoryComboBox = Nothing
    Me.co_cap.NTSRepositoryItemCheck = Nothing
    Me.co_cap.NTSRepositoryItemMemo = Nothing
    Me.co_cap.NTSRepositoryItemText = Nothing
    Me.co_cap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_cap.OptionsFilter.AllowFilter = False
    Me.co_cap.Visible = True
    Me.co_cap.VisibleIndex = 4
    Me.co_cap.Width = 119
    '
    'co_citta
    '
    Me.co_citta.AppearanceCell.Options.UseBackColor = True
    Me.co_citta.AppearanceCell.Options.UseTextOptions = True
    Me.co_citta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_citta.Caption = "Città"
    Me.co_citta.Enabled = True
    Me.co_citta.FieldName = "co_citta"
    Me.co_citta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_citta.Name = "co_citta"
    Me.co_citta.NTSRepositoryComboBox = Nothing
    Me.co_citta.NTSRepositoryItemCheck = Nothing
    Me.co_citta.NTSRepositoryItemMemo = Nothing
    Me.co_citta.NTSRepositoryItemText = Nothing
    Me.co_citta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_citta.OptionsFilter.AllowFilter = False
    Me.co_citta.Visible = True
    Me.co_citta.VisibleIndex = 5
    Me.co_citta.Width = 134
    '
    'co_prov
    '
    Me.co_prov.AppearanceCell.Options.UseBackColor = True
    Me.co_prov.AppearanceCell.Options.UseTextOptions = True
    Me.co_prov.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_prov.Caption = "Provincia"
    Me.co_prov.Enabled = True
    Me.co_prov.FieldName = "co_prov"
    Me.co_prov.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_prov.Name = "co_prov"
    Me.co_prov.NTSRepositoryComboBox = Nothing
    Me.co_prov.NTSRepositoryItemCheck = Nothing
    Me.co_prov.NTSRepositoryItemMemo = Nothing
    Me.co_prov.NTSRepositoryItemText = Nothing
    Me.co_prov.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_prov.OptionsFilter.AllowFilter = False
    Me.co_prov.Visible = True
    Me.co_prov.VisibleIndex = 6
    Me.co_prov.Width = 120
    '
    'co_stato
    '
    Me.co_stato.AppearanceCell.Options.UseBackColor = True
    Me.co_stato.AppearanceCell.Options.UseTextOptions = True
    Me.co_stato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_stato.Caption = "Stato"
    Me.co_stato.Enabled = True
    Me.co_stato.FieldName = "co_stato"
    Me.co_stato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_stato.Name = "co_stato"
    Me.co_stato.NTSRepositoryComboBox = Nothing
    Me.co_stato.NTSRepositoryItemCheck = Nothing
    Me.co_stato.NTSRepositoryItemMemo = Nothing
    Me.co_stato.NTSRepositoryItemText = Nothing
    Me.co_stato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_stato.OptionsFilter.AllowFilter = False
    Me.co_stato.Visible = True
    Me.co_stato.VisibleIndex = 7
    Me.co_stato.Width = 115
    '
    'co_datnasc
    '
    Me.co_datnasc.AppearanceCell.Options.UseBackColor = True
    Me.co_datnasc.AppearanceCell.Options.UseTextOptions = True
    Me.co_datnasc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_datnasc.Caption = "Data Nascita"
    Me.co_datnasc.Enabled = True
    Me.co_datnasc.FieldName = "co_datnasc"
    Me.co_datnasc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_datnasc.Name = "co_datnasc"
    Me.co_datnasc.NTSRepositoryComboBox = Nothing
    Me.co_datnasc.NTSRepositoryItemCheck = Nothing
    Me.co_datnasc.NTSRepositoryItemMemo = Nothing
    Me.co_datnasc.NTSRepositoryItemText = Nothing
    Me.co_datnasc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_datnasc.OptionsFilter.AllowFilter = False
    Me.co_datnasc.Visible = True
    Me.co_datnasc.VisibleIndex = 8
    Me.co_datnasc.Width = 146
    '
    'co_fbuser
    '
    Me.co_fbuser.AppearanceCell.Options.UseBackColor = True
    Me.co_fbuser.AppearanceCell.Options.UseTextOptions = True
    Me.co_fbuser.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_fbuser.Caption = "Facebook"
    Me.co_fbuser.Enabled = True
    Me.co_fbuser.FieldName = "co_fbuser"
    Me.co_fbuser.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_fbuser.Name = "co_fbuser"
    Me.co_fbuser.NTSRepositoryComboBox = Nothing
    Me.co_fbuser.NTSRepositoryItemCheck = Nothing
    Me.co_fbuser.NTSRepositoryItemMemo = Nothing
    Me.co_fbuser.NTSRepositoryItemText = Nothing
    Me.co_fbuser.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_fbuser.OptionsFilter.AllowFilter = False
    Me.co_fbuser.Visible = True
    Me.co_fbuser.VisibleIndex = 9
    '
    'co_twitteruser
    '
    Me.co_twitteruser.AppearanceCell.Options.UseBackColor = True
    Me.co_twitteruser.AppearanceCell.Options.UseTextOptions = True
    Me.co_twitteruser.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.co_twitteruser.Caption = "Twitter"
    Me.co_twitteruser.Enabled = True
    Me.co_twitteruser.FieldName = "co_twitteruser"
    Me.co_twitteruser.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.co_twitteruser.Name = "co_twitteruser"
    Me.co_twitteruser.NTSRepositoryComboBox = Nothing
    Me.co_twitteruser.NTSRepositoryItemCheck = Nothing
    Me.co_twitteruser.NTSRepositoryItemMemo = Nothing
    Me.co_twitteruser.NTSRepositoryItemText = Nothing
    Me.co_twitteruser.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.co_twitteruser.OptionsFilter.AllowFilter = False
    Me.co_twitteruser.Visible = True
    Me.co_twitteruser.VisibleIndex = 10
    '
    'xx_codstco
    '
    Me.xx_codstco.AppearanceCell.Options.UseBackColor = True
    Me.xx_codstco.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codstco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codstco.Caption = "Status Commerciale"
    Me.xx_codstco.Enabled = True
    Me.xx_codstco.FieldName = "xx_codstco"
    Me.xx_codstco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codstco.Name = "xx_codstco"
    Me.xx_codstco.NTSRepositoryComboBox = Nothing
    Me.xx_codstco.NTSRepositoryItemCheck = Nothing
    Me.xx_codstco.NTSRepositoryItemMemo = Nothing
    Me.xx_codstco.NTSRepositoryItemText = Nothing
    Me.xx_codstco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codstco.OptionsFilter.AllowFilter = False
    Me.xx_codstco.Visible = True
    Me.xx_codstco.VisibleIndex = 14
    '
    'FRM__HLCO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.ClientSize = New System.Drawing.Size(738, 443)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Name = "FRM__HLCO"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM CONTATTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1Pan1.ResumeLayout(False)
    Me.pnTab1Pan1.PerformLayout()
    CType(Me.cbStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataNasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTwitter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFacebook.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCognome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edComune.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edStato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvincia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCellulare.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTelef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Eventi di form"
  Public Overridable Sub FRM__HLCO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      CaricaCombo()
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
      GctlApplicaDefaultValue()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLCO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.F5 Then NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__HLCO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    If ckOttimistico.Checked Then
      oMenu.SaveSettingBus("BN__HLCO", "RECENT", ".", "Ottimistico", "-1", " ", "NS.", "...", "...")
    Else
      oMenu.SaveSettingBus("BN__HLCO", "RECENT", ".", "Ottimistico", "0", " ", "NS.", "...", "...")
    End If
  End Sub
#End Region

#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strQuery As String = ""
    Try
      Me.ValidaLastControl()

      '------------------------------------------------
      'ATTENZIONE: nel TAG di ogni controllo deve essere inserito l'operatore che deve essere utilizzato nella query 
      If edCognome.Text <> "" Then
        If edCognome.Text.Contains(".") = False And edCognome.Text.Contains(" ") = False Then
          strQuery += "REPLACE(REPLACE(co_descont, ' ', ''), '.', '') like " & CampoTesto(edCognome.Text, True) & "§"
        Else
          strQuery += "co_descont like " & CampoTesto(edCognome.Text, True) & "§"
        End If
      End If
      If edNome.Text <> "" Then
        If edNome.Text.Contains(".") = False And edNome.Text.Contains(" ") = False Then
          strQuery += "REPLACE(REPLACE(co_descont2, ' ', ''), '.', '') like " & CampoTesto(edNome.Text, True) & "§"
        Else
          strQuery += "co_descont2 like " & CampoTesto(edNome.Text, True) & "§"
        End If
      End If
      If edEmail.Text <> "" Then strQuery += "co_emailpers like " & CampoTesto(edEmail.Text, True) & "§"
      If edComune.Text <> "" Then strQuery += "co_citta like " & CampoTesto(edComune.Text, True) & "§"
      If edTelef.Text <> "" Then strQuery += "co_telefpers like " & CampoTesto(edTelef.Text, True) & "§"
      If edFax.Text <> "" Then strQuery += "co_faxpers like " & CampoTesto(edFax.Text, True) & "§"
      If edProvincia.Text <> "" Then strQuery += "co_prov = " & CampoTesto(edProvincia.Text) & "§"
      If edCap.Text <> "" Then strQuery += "co_cap = " & CampoTesto(edCap.Text) & "§"
      If edStato.Text <> "" Then strQuery += "co_stato = " & CampoTesto(edStato.Text) & "§"
      If edCellulare.Text <> "" Then strQuery += "co_cellpers = " & CampoTesto(edCellulare.Text, True) & "§"
      If NTSCInt(cbStatus.SelectedValue) <> 0 Then strQuery += "co_codstco = " & cbStatus.SelectedValue & "§"
      If edFacebook.Text <> "" Then strQuery += "co_fbuser like " & CampoTesto(edFacebook.Text, True) & "§"
      If edTwitter.Text <> "" Then strQuery += "co_twitteruser like " & CampoTesto(edTwitter.Text, True) & "§"
      If NTSCDate(edDataNasc.Text) <> New Date(1900, 1, 1) Then strQuery += "co_datnasc = " & CDataSQL(edDataNasc.Text) & "§"

      If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)

      '------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor

      oCleHlco.GetDataContatti(DittaCorrente, strQuery, dsHlco)

      dtcHlco.DataSource = dsHlco.Tables("CONTATTI")
      dsHlco.AcceptChanges()
      grZoom.DataSource = dtcHlco

      '------------------------------------------------
      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valoreù
      If grvZoom.RowCount > 0 Then
        grvZoom.Focus()
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 127791222142968750, "La ricerca non ha restituito nessun risultato"))
        edCognome.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If grvZoom.NTSGetCurrentDataRow Is Nothing Then Return

      oParam.strOut = NTSCStr(grvZoom.NTSGetCurrentDataRow()!co_progr)

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then strTesto = strTesto.Substring(0, strTesto.Length - 1)
      End If
      If ckOttimistico.Checked Then
        strOut = CStrSQL(bFil & strTesto & bFil)
      Else
        strOut = CStrSQL(strTesto & bFil)
      End If
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    Try
      cmdSeleziona_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
