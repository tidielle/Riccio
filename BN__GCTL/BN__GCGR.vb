Imports System.Windows.Forms
Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GCGR
  Public ctlGrid As NTSGridView = Nothing
  Public oCleGctl As CLE__GCTL
  Public dtcUi As BindingSource = New BindingSource()
  Public strChild As String = ""
  Public strForm As String = ""
  Public strTipoDoc As String = ""
  Public c As String = ""
  Public strParents As String = "" 'elenco dei container del controllo in analisi, a partire da quello che contiene il controllo fino alla form
  Public dttProp As New DataTable
  Public oParam As CLE__CLDP
  Public nRowCopied As Integer = -1

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean

    Try
      oMenu = Menu
      oApp = oMenu.App
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      '---------------------------------
      'creo e attivo l'entity
      oCleGctl = New CLE__GCTL
      bRemoting = Menu.Remoting("BN__GCTL", strRemoteServer, strRemotePort)
      oCleGctl.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

      '---------------------------------
      'inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      AddHandler oCleGctl.RemoteEvent, AddressOf GestisciEventiEntity

      '---------------------------------
      'memorizzo i parametri passati dal child
      ctlGrid = CType(Param.ctlPar1, NTSGridView)
      oParam = Param
      oParam.bPar1 = True 'presumo che far delle modifiche

      '----------------------------------------------
      'cerco il contenitore del controllo per ottenere il nome
      strParents = ""
      Dim fmParent As Control
      If ctlGrid.GridControl.Parent Is Nothing Then
        fmParent = ctlGrid.GridControl
      Else
        fmParent = ctlGrid.GridControl.Parent
        strParents = strParents & fmParent.Name & ","
        While Not fmParent.Parent Is Nothing
          fmParent = fmParent.Parent
          strParents = strParents & fmParent.Name & ","
          Dim strType As String = fmParent.GetType().ToString
          If strType.StartsWith("NTSInformatica.FRM") OrElse strType.StartsWith("NTSInformatica.FRO") Then Exit While
        End While
        strParents = strParents.Substring(0, strParents.Length - 1)
      End If
      strForm = fmParent.Name
      strChild = Param.strPar1
      strTipoDoc = Param.strPar2
      If strTipoDoc = "" Then strTipoDoc = " "

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei controlli
      optOperat.NTSSetParam(oMenu, oApp.Tr(Me, 128230023388867006, "Applica per operatore"), "-1")
      optGruppo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023389023179, "Applica a Gruppo operatori"), "-1")
      edGruppo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023389179352, "Gruppo operatori"), 20)
      edOperat.NTSSetParam(oMenu, oApp.Tr(Me, 128230023389335525, "Operatore"), 20)
      edTipodoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023389491698, "Tipo documento"), 3, False)
      edRowHeight.NTSSetParam(oMenu, oApp.Tr(Me, 130203561816405573, "Altezza righe"), "0", 3, 14, 999)

      edGruppo.NTSSetParamZoom("ZOOMRUOLI")
      edOperat.NTSSetParamZoom("ZOOMOPERAT")

      grvUi.NTSSetParam(oMenu, oApp.Tr(Me, 128230023389647871, "Griglia proprietà"))
      grvUi.NTSAllowInsert = False
      grvUi.NTSAllowDelete = False

      ui_tag.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023389804044, "Sicurezza"), 3)
      ui_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023389960217, "Descrizione colonna"), 30)
      ui_visible.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023390116390, "Visibile"), "-1", "0")
      ui_enable.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023390272563, "Editabile"), "-1", "0")
      ui_order.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023390428736, "Ordinamento in griglia"), "0", 4)
      ui_colwidth.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130203540560198899, "Larghezza della colonna"), "0", 4)
      ui_colname.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023390584909, "Nome della colonna"), 30)
      edRicerca.NTSSetParam(oMenu, oApp.Tr(Me, 129890799004828522, "Ricerca"), 0, True)

      'tolgo l'evidenziazione dalla riga corrente, diversamente non si vedrebbero i colori delle celle selezionate
      grvUi.OptionsSelection.EnableAppearanceFocusedRow = False
      grvUi.Appearance.FocusedRow.Options.UseBackColor = False

      grvUi.AddColumnBackColor("backcolor_ui_descr") 'sempre nella InitControls

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
    Me.grUi = New NTSInformatica.NTSGrid
    Me.grvUi = New NTSInformatica.NTSGridView
    Me.ui_tag = New NTSInformatica.NTSGridColumn
    Me.ui_descr = New NTSInformatica.NTSGridColumn
    Me.ui_visible = New NTSInformatica.NTSGridColumn
    Me.ui_enable = New NTSInformatica.NTSGridColumn
    Me.ui_colwidth = New NTSInformatica.NTSGridColumn
    Me.ui_order = New NTSInformatica.NTSGridColumn
    Me.ui_colname = New NTSInformatica.NTSGridColumn
    Me.xx_color = New NTSInformatica.NTSGridColumn
    Me.cmdPredefinito = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSalva = New NTSInformatica.NTSButton
    Me.fmParameters = New NTSInformatica.NTSGroupBox
    Me.edGruppo = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.edOperat = New NTSInformatica.NTSTextBoxStr
    Me.edTipodoc = New NTSInformatica.NTSTextBoxStr
    Me.optOperat = New NTSInformatica.NTSRadioButton
    Me.optGruppo = New NTSInformatica.NTSRadioButton
    Me.fmDepends = New NTSInformatica.NTSGroupBox
    Me.fmMoveColumn = New NTSInformatica.NTSGroupBox
    Me.lbSpostaNota = New NTSInformatica.NTSLabel
    Me.cmdDown5 = New NTSInformatica.NTSButton
    Me.cmdUp5 = New NTSInformatica.NTSButton
    Me.cmdDown = New NTSInformatica.NTSButton
    Me.cmdUp = New NTSInformatica.NTSButton
    Me.pnCommand = New NTSInformatica.NTSPanel
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.edRowHeight = New NTSInformatica.NTSTextBoxNum
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edRicerca = New NTSInformatica.NTSTextBoxStr
    Me.lbControl = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    CType(Me.grUi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvUi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmParameters, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmParameters.SuspendLayout()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOperat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optOperat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDepends, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDepends.SuspendLayout()
    CType(Me.fmMoveColumn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmMoveColumn.SuspendLayout()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommand.SuspendLayout()
    CType(Me.edRowHeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
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
    'grUi
    '
    Me.grUi.AllowDrop = True
    Me.grUi.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grUi.EmbeddedNavigator.Name = ""
    Me.grUi.Location = New System.Drawing.Point(0, 0)
    Me.grUi.MainView = Me.grvUi
    Me.grUi.Name = "grUi"
    Me.grUi.Size = New System.Drawing.Size(424, 479)
    Me.grUi.TabIndex = 1
    Me.grUi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvUi})
    '
    'grvUi
    '
    Me.grvUi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ui_tag, Me.ui_descr, Me.ui_visible, Me.ui_enable, Me.ui_colwidth, Me.ui_order, Me.ui_colname, Me.xx_color})
    Me.grvUi.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvUi.Enabled = True
    Me.grvUi.GridControl = Me.grUi
    Me.grvUi.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvUi.MinRowHeight = 14
    Me.grvUi.Name = "grvUi"
    Me.grvUi.NTSAllowDelete = False
    Me.grvUi.NTSAllowInsert = False
    Me.grvUi.NTSAllowUpdate = True
    Me.grvUi.NTSMenuContext = Nothing
    Me.grvUi.OptionsCustomization.AllowRowSizing = True
    Me.grvUi.OptionsFilter.AllowFilterEditor = False
    Me.grvUi.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvUi.OptionsNavigation.UseTabKey = False
    Me.grvUi.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvUi.OptionsView.ColumnAutoWidth = False
    Me.grvUi.OptionsView.EnableAppearanceEvenRow = True
    Me.grvUi.OptionsView.ShowGroupPanel = False
    Me.grvUi.RowHeight = 16
    '
    'ui_tag
    '
    Me.ui_tag.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ui_tag.AppearanceCell.Options.UseBackColor = True
    Me.ui_tag.AppearanceCell.Options.UseTextOptions = True
    Me.ui_tag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_tag.Caption = "Sec"
    Me.ui_tag.Enabled = False
    Me.ui_tag.FieldName = "ui_tag"
    Me.ui_tag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_tag.Name = "ui_tag"
    Me.ui_tag.NTSRepositoryComboBox = Nothing
    Me.ui_tag.NTSRepositoryItemCheck = Nothing
    Me.ui_tag.NTSRepositoryItemMemo = Nothing
    Me.ui_tag.NTSRepositoryItemText = Nothing
    Me.ui_tag.OptionsColumn.AllowEdit = False
    Me.ui_tag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_tag.OptionsColumn.ReadOnly = True
    Me.ui_tag.Visible = True
    Me.ui_tag.VisibleIndex = 0
    Me.ui_tag.Width = 35
    '
    'ui_descr
    '
    Me.ui_descr.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ui_descr.AppearanceCell.Options.UseBackColor = True
    Me.ui_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ui_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_descr.Caption = "Intestazione"
    Me.ui_descr.Enabled = False
    Me.ui_descr.FieldName = "ui_descr"
    Me.ui_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_descr.Name = "ui_descr"
    Me.ui_descr.NTSRepositoryComboBox = Nothing
    Me.ui_descr.NTSRepositoryItemCheck = Nothing
    Me.ui_descr.NTSRepositoryItemMemo = Nothing
    Me.ui_descr.NTSRepositoryItemText = Nothing
    Me.ui_descr.OptionsColumn.AllowEdit = False
    Me.ui_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_descr.OptionsColumn.ReadOnly = True
    Me.ui_descr.Visible = True
    Me.ui_descr.VisibleIndex = 1
    Me.ui_descr.Width = 184
    '
    'ui_visible
    '
    Me.ui_visible.AppearanceCell.Options.UseBackColor = True
    Me.ui_visible.AppearanceCell.Options.UseTextOptions = True
    Me.ui_visible.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_visible.Caption = "Visibile"
    Me.ui_visible.Enabled = True
    Me.ui_visible.FieldName = "ui_visible"
    Me.ui_visible.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_visible.Name = "ui_visible"
    Me.ui_visible.NTSRepositoryComboBox = Nothing
    Me.ui_visible.NTSRepositoryItemCheck = Nothing
    Me.ui_visible.NTSRepositoryItemMemo = Nothing
    Me.ui_visible.NTSRepositoryItemText = Nothing
    Me.ui_visible.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_visible.Visible = True
    Me.ui_visible.VisibleIndex = 2
    Me.ui_visible.Width = 60
    '
    'ui_enable
    '
    Me.ui_enable.AppearanceCell.Options.UseBackColor = True
    Me.ui_enable.AppearanceCell.Options.UseTextOptions = True
    Me.ui_enable.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_enable.Caption = "Editabile"
    Me.ui_enable.Enabled = True
    Me.ui_enable.FieldName = "ui_enable"
    Me.ui_enable.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_enable.Name = "ui_enable"
    Me.ui_enable.NTSRepositoryComboBox = Nothing
    Me.ui_enable.NTSRepositoryItemCheck = Nothing
    Me.ui_enable.NTSRepositoryItemMemo = Nothing
    Me.ui_enable.NTSRepositoryItemText = Nothing
    Me.ui_enable.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_enable.Visible = True
    Me.ui_enable.VisibleIndex = 3
    Me.ui_enable.Width = 60
    '
    'ui_colwidth
    '
    Me.ui_colwidth.AppearanceCell.Options.UseBackColor = True
    Me.ui_colwidth.AppearanceCell.Options.UseTextOptions = True
    Me.ui_colwidth.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_colwidth.Caption = "Larghezza"
    Me.ui_colwidth.Enabled = True
    Me.ui_colwidth.FieldName = "ui_colwidth"
    Me.ui_colwidth.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_colwidth.Name = "ui_colwidth"
    Me.ui_colwidth.NTSRepositoryComboBox = Nothing
    Me.ui_colwidth.NTSRepositoryItemCheck = Nothing
    Me.ui_colwidth.NTSRepositoryItemMemo = Nothing
    Me.ui_colwidth.NTSRepositoryItemText = Nothing
    Me.ui_colwidth.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_colwidth.Visible = True
    Me.ui_colwidth.VisibleIndex = 4
    '
    'ui_order
    '
    Me.ui_order.AppearanceCell.Options.UseBackColor = True
    Me.ui_order.AppearanceCell.Options.UseTextOptions = True
    Me.ui_order.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_order.Caption = "Posizione"
    Me.ui_order.Enabled = False
    Me.ui_order.FieldName = "ui_order"
    Me.ui_order.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_order.Name = "ui_order"
    Me.ui_order.NTSRepositoryComboBox = Nothing
    Me.ui_order.NTSRepositoryItemCheck = Nothing
    Me.ui_order.NTSRepositoryItemMemo = Nothing
    Me.ui_order.NTSRepositoryItemText = Nothing
    Me.ui_order.OptionsColumn.AllowEdit = False
    Me.ui_order.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_order.OptionsColumn.ReadOnly = True
    Me.ui_order.Visible = True
    Me.ui_order.VisibleIndex = 5
    '
    'ui_colname
    '
    Me.ui_colname.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ui_colname.AppearanceCell.Options.UseBackColor = True
    Me.ui_colname.AppearanceCell.Options.UseTextOptions = True
    Me.ui_colname.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_colname.Caption = "Nome colonna"
    Me.ui_colname.Enabled = False
    Me.ui_colname.FieldName = "ui_colname"
    Me.ui_colname.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_colname.Name = "ui_colname"
    Me.ui_colname.NTSRepositoryComboBox = Nothing
    Me.ui_colname.NTSRepositoryItemCheck = Nothing
    Me.ui_colname.NTSRepositoryItemMemo = Nothing
    Me.ui_colname.NTSRepositoryItemText = Nothing
    Me.ui_colname.OptionsColumn.AllowEdit = False
    Me.ui_colname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.ui_colname.OptionsColumn.ReadOnly = True
    Me.ui_colname.Visible = True
    Me.ui_colname.VisibleIndex = 6
    '
    'xx_color
    '
    Me.xx_color.AppearanceCell.Options.UseBackColor = True
    Me.xx_color.AppearanceCell.Options.UseTextOptions = True
    Me.xx_color.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_color.Caption = "..."
    Me.xx_color.Enabled = True
    Me.xx_color.FieldName = "xx_color"
    Me.xx_color.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_color.Name = "xx_color"
    Me.xx_color.NTSRepositoryComboBox = Nothing
    Me.xx_color.NTSRepositoryItemCheck = Nothing
    Me.xx_color.NTSRepositoryItemMemo = Nothing
    Me.xx_color.NTSRepositoryItemText = Nothing
    Me.xx_color.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    '
    'cmdPredefinito
    '
    Me.cmdPredefinito.ImageText = ""
    Me.cmdPredefinito.Location = New System.Drawing.Point(14, 5)
    Me.cmdPredefinito.Name = "cmdPredefinito"
    Me.cmdPredefinito.Size = New System.Drawing.Size(109, 24)
    Me.cmdPredefinito.TabIndex = 1
    Me.cmdPredefinito.Text = "&Predefinito"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(14, 31)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(109, 24)
    Me.cmdConferma.TabIndex = 2
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(14, 57)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(109, 24)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSalva
    '
    Me.cmdSalva.ImageText = ""
    Me.cmdSalva.Location = New System.Drawing.Point(14, 83)
    Me.cmdSalva.Name = "cmdSalva"
    Me.cmdSalva.Size = New System.Drawing.Size(109, 24)
    Me.cmdSalva.TabIndex = 4
    Me.cmdSalva.Text = "&Salva senza uscire"
    '
    'fmParameters
    '
    Me.fmParameters.AllowDrop = True
    Me.fmParameters.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmParameters.Appearance.Options.UseBackColor = True
    Me.fmParameters.Controls.Add(Me.edGruppo)
    Me.fmParameters.Controls.Add(Me.NtsLabel3)
    Me.fmParameters.Controls.Add(Me.NtsLabel2)
    Me.fmParameters.Controls.Add(Me.NtsLabel1)
    Me.fmParameters.Controls.Add(Me.edOperat)
    Me.fmParameters.Controls.Add(Me.edTipodoc)
    Me.fmParameters.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmParameters.Location = New System.Drawing.Point(8, 394)
    Me.fmParameters.Name = "fmParameters"
    Me.fmParameters.Size = New System.Drawing.Size(119, 141)
    Me.fmParameters.TabIndex = 6
    Me.fmParameters.Text = "Modifica"
    '
    'edGruppo
    '
    Me.edGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edGruppo.Enabled = False
    Me.edGruppo.Location = New System.Drawing.Point(5, 78)
    Me.edGruppo.Name = "edGruppo"
    Me.edGruppo.NTSDbField = ""
    Me.edGruppo.NTSForzaVisZoom = False
    Me.edGruppo.NTSOldValue = ""
    Me.edGruppo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edGruppo.Properties.Appearance.Options.UseBackColor = True
    Me.edGruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppo.Properties.MaxLength = 65536
    Me.edGruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppo.Size = New System.Drawing.Size(109, 20)
    Me.edGruppo.TabIndex = 1
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(6, 62)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(46, 13)
    Me.NtsLabel3.TabIndex = 4
    Me.NtsLabel3.Text = "Gruppo:"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(6, 23)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(89, 13)
    Me.NtsLabel2.TabIndex = 3
    Me.NtsLabel2.Text = "Nome operatore:"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(6, 110)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(55, 13)
    Me.NtsLabel1.TabIndex = 2
    Me.NtsLabel1.Text = "Tipo doc.:"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'edOperat
    '
    Me.edOperat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOperat.Location = New System.Drawing.Point(5, 39)
    Me.edOperat.Name = "edOperat"
    Me.edOperat.NTSDbField = ""
    Me.edOperat.NTSForzaVisZoom = False
    Me.edOperat.NTSOldValue = ""
    Me.edOperat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edOperat.Properties.Appearance.Options.UseBackColor = True
    Me.edOperat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOperat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOperat.Properties.MaxLength = 65536
    Me.edOperat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOperat.Size = New System.Drawing.Size(109, 20)
    Me.edOperat.TabIndex = 0
    '
    'edTipodoc
    '
    Me.edTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipodoc.Location = New System.Drawing.Point(90, 107)
    Me.edTipodoc.Name = "edTipodoc"
    Me.edTipodoc.NTSDbField = ""
    Me.edTipodoc.NTSForzaVisZoom = False
    Me.edTipodoc.NTSOldValue = ""
    Me.edTipodoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTipodoc.Properties.Appearance.Options.UseBackColor = True
    Me.edTipodoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipodoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipodoc.Properties.MaxLength = 65536
    Me.edTipodoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipodoc.Size = New System.Drawing.Size(24, 20)
    Me.edTipodoc.TabIndex = 2
    '
    'optOperat
    '
    Me.optOperat.Cursor = System.Windows.Forms.Cursors.Default
    Me.optOperat.EditValue = True
    Me.optOperat.Location = New System.Drawing.Point(21, 22)
    Me.optOperat.Name = "optOperat"
    Me.optOperat.NTSCheckValue = "S"
    Me.optOperat.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optOperat.Properties.Appearance.Options.UseBackColor = True
    Me.optOperat.Properties.Caption = "&Operatore"
    Me.optOperat.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optOperat.Size = New System.Drawing.Size(72, 19)
    Me.optOperat.TabIndex = 0
    '
    'optGruppo
    '
    Me.optGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optGruppo.Location = New System.Drawing.Point(21, 45)
    Me.optGruppo.Name = "optGruppo"
    Me.optGruppo.NTSCheckValue = "S"
    Me.optGruppo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optGruppo.Properties.Appearance.Options.UseBackColor = True
    Me.optGruppo.Properties.Caption = "&Gruppo"
    Me.optGruppo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optGruppo.Size = New System.Drawing.Size(60, 19)
    Me.optGruppo.TabIndex = 1
    '
    'fmDepends
    '
    Me.fmDepends.AllowDrop = True
    Me.fmDepends.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDepends.Appearance.Options.UseBackColor = True
    Me.fmDepends.Controls.Add(Me.optOperat)
    Me.fmDepends.Controls.Add(Me.optGruppo)
    Me.fmDepends.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDepends.Location = New System.Drawing.Point(8, 319)
    Me.fmDepends.Name = "fmDepends"
    Me.fmDepends.Size = New System.Drawing.Size(120, 69)
    Me.fmDepends.TabIndex = 5
    Me.fmDepends.Text = "Imposta per"
    '
    'fmMoveColumn
    '
    Me.fmMoveColumn.AllowDrop = True
    Me.fmMoveColumn.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmMoveColumn.Appearance.Options.UseBackColor = True
    Me.fmMoveColumn.Controls.Add(Me.lbSpostaNota)
    Me.fmMoveColumn.Controls.Add(Me.cmdDown5)
    Me.fmMoveColumn.Controls.Add(Me.cmdUp5)
    Me.fmMoveColumn.Controls.Add(Me.cmdDown)
    Me.fmMoveColumn.Controls.Add(Me.cmdUp)
    Me.fmMoveColumn.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmMoveColumn.Location = New System.Drawing.Point(8, 143)
    Me.fmMoveColumn.Name = "fmMoveColumn"
    Me.fmMoveColumn.Size = New System.Drawing.Size(119, 170)
    Me.fmMoveColumn.TabIndex = 7
    Me.fmMoveColumn.Text = "Sposta colonna"
    '
    'lbSpostaNota
    '
    Me.lbSpostaNota.AutoSize = True
    Me.lbSpostaNota.BackColor = System.Drawing.Color.Transparent
    Me.lbSpostaNota.Location = New System.Drawing.Point(6, 102)
    Me.lbSpostaNota.Name = "lbSpostaNota"
    Me.lbSpostaNota.NTSDbField = ""
    Me.lbSpostaNota.Size = New System.Drawing.Size(116, 65)
    Me.lbSpostaNota.TabIndex = 5
    Me.lbSpostaNota.Text = "Altrimenti: doppio click" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "colonna da spostare" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "poi doppio click su" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "colonna dove " & _
        "inserire" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sopra la colonna selez."
    Me.lbSpostaNota.Tooltip = ""
    Me.lbSpostaNota.UseMnemonic = False
    '
    'cmdDown5
    '
    Me.cmdDown5.ImageText = ""
    Me.cmdDown5.Location = New System.Drawing.Point(65, 62)
    Me.cmdDown5.Name = "cmdDown5"
    Me.cmdDown5.Size = New System.Drawing.Size(40, 27)
    Me.cmdDown5.TabIndex = 3
    Me.cmdDown5.Text = "Giù+5"
    '
    'cmdUp5
    '
    Me.cmdUp5.ImageText = ""
    Me.cmdUp5.Location = New System.Drawing.Point(65, 29)
    Me.cmdUp5.Name = "cmdUp5"
    Me.cmdUp5.Size = New System.Drawing.Size(40, 27)
    Me.cmdUp5.TabIndex = 2
    Me.cmdUp5.Text = "Su+5"
    '
    'cmdDown
    '
    Me.cmdDown.ImageText = ""
    Me.cmdDown.Location = New System.Drawing.Point(9, 62)
    Me.cmdDown.Name = "cmdDown"
    Me.cmdDown.Size = New System.Drawing.Size(40, 27)
    Me.cmdDown.TabIndex = 1
    Me.cmdDown.Text = "Giù"
    '
    'cmdUp
    '
    Me.cmdUp.ImageText = ""
    Me.cmdUp.Location = New System.Drawing.Point(9, 29)
    Me.cmdUp.Name = "cmdUp"
    Me.cmdUp.Size = New System.Drawing.Size(40, 27)
    Me.cmdUp.TabIndex = 0
    Me.cmdUp.Text = "Su"
    '
    'pnCommand
    '
    Me.pnCommand.AllowDrop = True
    Me.pnCommand.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommand.Appearance.Options.UseBackColor = True
    Me.pnCommand.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommand.Controls.Add(Me.NtsLabel4)
    Me.pnCommand.Controls.Add(Me.edRowHeight)
    Me.pnCommand.Controls.Add(Me.cmdPredefinito)
    Me.pnCommand.Controls.Add(Me.cmdConferma)
    Me.pnCommand.Controls.Add(Me.fmMoveColumn)
    Me.pnCommand.Controls.Add(Me.cmdAnnulla)
    Me.pnCommand.Controls.Add(Me.fmDepends)
    Me.pnCommand.Controls.Add(Me.cmdSalva)
    Me.pnCommand.Controls.Add(Me.fmParameters)
    Me.pnCommand.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommand.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnCommand.Location = New System.Drawing.Point(424, 0)
    Me.pnCommand.Name = "pnCommand"
    Me.pnCommand.NTSActiveTrasparency = True
    Me.pnCommand.Size = New System.Drawing.Size(135, 541)
    Me.pnCommand.TabIndex = 8
    Me.pnCommand.Text = "NtsPanel2"
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(14, 110)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(69, 26)
    Me.NtsLabel4.TabIndex = 10
    Me.NtsLabel4.Text = "Altezza righe" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(default 18)"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'edRowHeight
    '
    Me.edRowHeight.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRowHeight.Location = New System.Drawing.Point(88, 114)
    Me.edRowHeight.Name = "edRowHeight"
    Me.edRowHeight.NTSDbField = ""
    Me.edRowHeight.NTSFormat = "0"
    Me.edRowHeight.NTSForzaVisZoom = False
    Me.edRowHeight.NTSOldValue = ""
    Me.edRowHeight.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRowHeight.Properties.Appearance.Options.UseBackColor = True
    Me.edRowHeight.Properties.Appearance.Options.UseTextOptions = True
    Me.edRowHeight.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRowHeight.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRowHeight.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRowHeight.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRowHeight.Properties.MaxLength = 65536
    Me.edRowHeight.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRowHeight.Size = New System.Drawing.Size(39, 20)
    Me.edRowHeight.TabIndex = 9
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edRicerca)
    Me.pnTop.Controls.Add(Me.lbControl)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(424, 62)
    Me.pnTop.TabIndex = 9
    Me.pnTop.Text = "NtsPanel2"
    '
    'edRicerca
    '
    Me.edRicerca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRicerca.Location = New System.Drawing.Point(4, 37)
    Me.edRicerca.Name = "edRicerca"
    Me.edRicerca.NTSDbField = ""
    Me.edRicerca.NTSForzaVisZoom = False
    Me.edRicerca.NTSOldValue = ""
    Me.edRicerca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicerca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicerca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicerca.Properties.MaxLength = 65536
    Me.edRicerca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicerca.Size = New System.Drawing.Size(366, 20)
    Me.edRicerca.TabIndex = 2
    '
    'lbControl
    '
    Me.lbControl.BackColor = System.Drawing.Color.Transparent
    Me.lbControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbControl.Location = New System.Drawing.Point(4, 10)
    Me.lbControl.Name = "lbControl"
    Me.lbControl.NTSDbField = ""
    Me.lbControl.Size = New System.Drawing.Size(366, 23)
    Me.lbControl.TabIndex = 1
    Me.lbControl.Text = "Controllo: "
    Me.lbControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbControl.Tooltip = ""
    Me.lbControl.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grUi)
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 62)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(424, 479)
    Me.pnGrid.TabIndex = 10
    Me.pnGrid.Text = "NtsPanel1"
    '
    'FRM__GCGR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(559, 541)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnCommand)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
    Me.Name = "FRM__GCGR"
    Me.NTSLastControlFocussed = Me.grUi
    Me.Text = "PERSONALIZZAZIONE GRIGLIE"
    CType(Me.grUi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvUi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmParameters, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmParameters.ResumeLayout(False)
    Me.fmParameters.PerformLayout()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOperat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optOperat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDepends, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDepends.ResumeLayout(False)
    Me.fmDepends.PerformLayout()
    CType(Me.fmMoveColumn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmMoveColumn.ResumeLayout(False)
    Me.fmMoveColumn.PerformLayout()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommand.ResumeLayout(False)
    Me.pnCommand.PerformLayout()
    CType(Me.edRowHeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRM__GCGR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim strVis As String = "-1"
    Dim strEnab As String = "-1"
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If CBool(oMenu.GetSettingBus("Opzioni", ".", ".", "BloccaPersGriglieGruppiNonZero", "0", " ", "0")) Then
        If Not UserIsAdmin(oApp.User.Gruppo) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130165491301850268, "Operatore non abilitato ad entrare in modifica della configurazione di griglia"))
          Me.Close()
          Return
        End If
      End If

      '----------------------------------------------
      lbControl.Text = oApp.Tr(Me, 130421147320361432, "Controllo: |" & ctlGrid.Name & "|")
      edTipodoc.Text = strTipoDoc
      If strTipoDoc.Trim = "" Then edTipodoc.Enabled = False
      edOperat.Text = oApp.User.Nome
      edGruppo.Text = oApp.User.Gruppo

      edRowHeight.Text = ctlGrid.RowHeight.ToString

      '----------------------------------------------
      'carico le colonne della griglia (scarto le colonne senza intestazione)
      dttProp.Columns.Add("ui_colname", GetType(String))
      dttProp.Columns.Add("ui_descr", GetType(String))
      dttProp.Columns.Add("ui_visible", GetType(String))
      dttProp.Columns.Add("ui_enable", GetType(String))
      dttProp.Columns.Add("ui_tag", GetType(String))
      dttProp.Columns.Add("ui_order", GetType(Integer))
      dttProp.Columns.Add("ui_colwidth", GetType(Integer))
      dttProp.Columns.Add("ui_colwidthOrig", GetType(Integer))

      'gestione della position:
      'quanto una colonna della griglia è non visibile, viene impostata con VisibleIndex = -1
      'nella NTSSetparam della gridview memorizzo AbsoluteIndexSTD = AbsoluteIndex
      'in absoluteindex c'è l'esatto ordinamento di visualizzazione delle colonne da sx, che siano visibili oppure no

      For i = 0 To ctlGrid.Columns.Count - 1
        strVis = "0"
        strEnab = "-1"
        If ctlGrid.Columns(i).Visible Then strVis = "-1"
        If ctlGrid.Columns(i).ReadOnly Then strEnab = "0"

        If ctlGrid.Columns(i).Caption <> "" Then
          dttProp.Rows.Add(New Object() {ctlGrid.Columns(i).Name, _
                                         ctlGrid.Columns(i).Caption, _
                                         strVis, _
                                         strEnab, _
                                         ctlGrid.Columns(i).Tag, _
                                         CType(ctlGrid.Columns(i), NTSGridColumn).AbsoluteIndex, _
                                         CType(ctlGrid.Columns(i), NTSGridColumn).Width, _
                                         CType(ctlGrid.Columns(i), NTSGridColumn).Width})

        End If    'If ctlGest.Columns(i).HeaderText <> "" Then
      Next

      'carico la colonna del colore
      dttProp.Columns.Add("xx_color", GetType(Integer))
      dttProp.Columns.Add("xx_found", GetType(String))
      dttProp.Columns.Add("backcolor_ui_descr", GetType(Integer))

      'adesso devo ricalcolare la posizione delle colonne, visto che ho aggiunto quelli non visibili
      'non devono esserci colonne con ui_order uguale
      Dim dtrT() As DataRow = dttProp.Select("", "ui_order ASC")
      For i = 0 To dttProp.Rows.Count - 1
        dtrT(i)!ui_order = i
        dtrT(i)!xx_color = 0
        dtrT(i)!xx_found = "N"
        dtrT(i)!backcolor_ui_descr = -1
      Next

      dttProp.AcceptChanges()

      '----------------------------------------------
      'imposto il colore della cella per spostare le colonne
      Dim cn As New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, xx_color, Nothing, 0)
      cn.ApplyToRow = True
      cn.Appearance.BackColor = Color.White
      grvUi.FormatConditions.Add(cn)

      cn = New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, xx_color, Nothing, "1")
      cn.ApplyToRow = True
      cn.Appearance.BackColor = Color.FromArgb(136, 255, 136)     'verde
      grvUi.FormatConditions.Add(cn)

      '----------------------------------------------
      'collego la tabella alla griglia
      dtcUi.DataSource = dttProp
      grUi.DataSource = dtcUi

      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

      GctlSetRoules()

      grvUi_NTSFocusedRowChanged(grvUi, Nothing)

      'per praticità allargo la form come l'altezza dello schermo, così sarà più facile spostare le colonne della griglia
      Me.Top = 0
      Me.Height = Screen.PrimaryScreen.WorkingArea.Height

      ui_tag.Enabled = False
      ui_colname.Enabled = False
      ui_order.Enabled = False
      ui_descr.Enabled = False

      'se non sono un amministratore, non posso modificare le griglie di altri
      If UserIsAdmin(oApp.User.Gruppo) = False Then
        fmDepends.Enabled = False
        optOperat.Checked = True
        edOperat.Enabled = False
        edGruppo.Enabled = False
        edOperat.Text = oApp.User.Nome
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.bPar1 = False
    Me.Close()
  End Sub

  Public Overridable Sub cmdSalva_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalva.Click
    '-------------------------------------------------
    'salvo senza uscire
    Try
      If Checkcontrolli() = False Then Return
      Me.Cursor = Cursors.WaitCursor
      If optOperat.Checked Then
        oCleGctl.SalvaCTRL_ALT_F2(edOperat.Text, "", edTipodoc.Text, False, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      Else
        oCleGctl.SalvaCTRL_ALT_F2("", edGruppo.Text, edTipodoc.Text, False, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    '-------------------------------------------------
    'salvo ed esco
    Try
      If Checkcontrolli() = False Then Return
      Me.Cursor = Cursors.WaitCursor
      If optOperat.Checked Then
        oCleGctl.SalvaCTRL_ALT_F2(edOperat.Text, "", edTipodoc.Text, False, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      Else
        oCleGctl.SalvaCTRL_ALT_F2("", edGruppo.Text, edTipodoc.Text, False, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      End If

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdPredefinito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPredefinito.Click
    '-------------------------------------------------
    'cancello tutte le precedenti impostazioni per gruppo / utente
    If Checkcontrolli() = False Then Return

    Dim strMsg As String = oApp.Tr("BUSNET", 127791950157812500, "Confermi l'annullamento di tutte le impostazioni della griglia per ")
    Try
      If optOperat.Checked Then
        strMsg = strMsg & oApp.Tr("BUSNET", 128962653147982955, " l'operatore '|" & edOperat.Text & "|'?")
      Else
        strMsg = strMsg & oApp.Tr("BUSNET", 128962653446428712, " il gruppo operatori '|" & edGruppo.Text & "|'?")
      End If
      If oApp.MsgBoxInfoYesNo_DefYes(strMsg) = Windows.Forms.DialogResult.No Then Return

      If optOperat.Checked Then
        oCleGctl.SalvaCTRL_ALT_F2(edOperat.Text, "", edTipodoc.Text, True, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      Else
        oCleGctl.SalvaCTRL_ALT_F2("", edGruppo.Text, edTipodoc.Text, True, dttProp, strForm, strChild, ctlGrid.Name, strParents, NTSCInt(edRowHeight.Text))
      End If

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edRowHeight_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edRowHeight.Validated
    Try
      'imposto l'altezza della righe
      If NTSCInt(edRowHeight.Text) < 14 Then edRowHeight.Text = "14"
      ctlGrid.RowHeight = NTSCInt(edRowHeight.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub optOperat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOperat.CheckedChanged
    If optOperat.Checked Then
      edGruppo.Enabled = False
      edOperat.Enabled = True
    Else
      edGruppo.Enabled = True
      edOperat.Enabled = False
    End If
  End Sub

  Public Overridable Sub optGruppo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGruppo.CheckedChanged
    If optOperat.Checked Then
      edGruppo.Enabled = False
      edOperat.Enabled = True
    Else
      edGruppo.Enabled = True
      edOperat.Enabled = False
    End If
  End Sub

  Public Overridable Function Checkcontrolli() As Boolean
    If edOperat.Text.Trim = "" And optOperat.Checked Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222139687500, "Attenzione: impostare il nome dell'operatore"))
      Return False
    End If

    If edGruppo.Text.Trim = "" And optGruppo.Checked Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222139843750, "Attenzione: impostare il nome del gruppo operatori"))
      Return False
    End If

    If edTipodoc.Text = "" And edTipodoc.Enabled Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222140000000, "Attenzione: impostare il tipo documento"))
      Return False
    End If

    Return True

  End Function

  Public Overridable Sub grvUi_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvUi.NTSFocusedRowChanged
    If dttProp.Rows.Count = 0 Then Return
    If grvUi.FocusedRowHandle < 0 Then Return

    '--------------------------------------------------------
    'blocco le colonne in base al contenuto di ui_tab
    'V = visible bloccato
    'E = enable bloccato
    With grvUi.NTSGetCurrentDataRow
      If !ui_tag.ToString.IndexOf("E") > -1 Then
        ui_enable.Enabled = False
      Else
        ui_enable.Enabled = True
      End If
      If !ui_tag.ToString.IndexOf("V") > -1 Then
        ui_visible.Enabled = False
      Else
        ui_visible.Enabled = True
      End If
    End With
  End Sub

  Public Overridable Sub grUi_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grUi.MouseDoubleClick
    'sposto le colonne con doppio click: 
    'doppio click su una colonna la seleziono,
    'doppio click sulla stessa colonna la deseleziono
    'doppio click su unaltra colonna dopo averne selezionata una sposto la colonna selez. prima prima di quella su cui ho appena cliccato
    Try
      If grvUi.NTSGetCurrentDataRow Is Nothing Then Return

      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

      If nRowCopied > -1 Then
        If grvUi.FocusedRowHandle = nRowCopied Then
          'annullo riga copiata
          grvUi.NTSGetCurrentDataRow!xx_color = 0
          grvUi.NTSGetCurrentDataRow.EndEdit()
          grvUi.NTSGetCurrentDataRow.EndEdit()
          grUi.Refresh()
          nRowCopied = -1
          Return
        Else
          'sposto la riga 
          grvUi.SetRowCellValue(nRowCopied, "xx_color", 0)
          grvUi.NTSGetCurrentDataRow.EndEdit()
          grvUi.NTSGetCurrentDataRow.EndEdit()
          grUi.Refresh()

          Dim nOldUiOrder As Integer = NTSCInt(grvUi.GetRowCellValue(nRowCopied, ui_order).ToString)
          Dim dtrT() As DataRow
          Dim i As Integer
          Dim nUiOrder As Integer = NTSCInt(grvUi.GetFocusedRowCellValue(ui_order))
          If nUiOrder > nOldUiOrder Then
            'ho spostato la colonna in basso
            dtrT = dttProp.Select("ui_order >= " + nOldUiOrder.ToString + " AND ui_order <= " + nUiOrder.ToString, "ui_order ASC")

            For i = 0 To dtrT.Length - 1
              dtrT(i)!ui_order = NTSCInt(dtrT(i)!ui_order) - 1
            Next
            dtrT(0)!ui_order = nUiOrder
          Else
            'ho spostato la colonna in alto
            dtrT = dttProp.Select("ui_order >= " + nUiOrder.ToString + " AND ui_order <= " + nOldUiOrder.ToString, "ui_order ASC")

            For i = 0 To dtrT.Length - 2
              dtrT(i)!ui_order = NTSCInt(dtrT(i)!ui_order) + 1
            Next
            dtrT(dtrT.Length - 1)!ui_order = nUiOrder

          End If
          nRowCopied = -1
        End If

      Else
        'copia riga
        grvUi.NTSGetCurrentDataRow!xx_color = 1
        grvUi.NTSGetCurrentDataRow.EndEdit()
        grvUi.NTSGetCurrentDataRow.EndEdit()
        grUi.Refresh()
        nRowCopied = grvUi.FocusedRowHandle
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
    Dim oTmp As Object
    Try
      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
      If grvUi.FocusedRowHandle = 0 Then Return

      Dim strDescr As String = grvUi.GetFocusedRowCellValue(ui_descr).ToString
      Dim dtrT() As DataRow
      Dim nPos As Integer = NTSCInt(grvUi.GetFocusedRowCellValue(ui_order))
      dtrT = dttProp.Select("ui_order = " + nPos.ToString + " OR ui_order = " + (nPos - 1).ToString, "ui_order ASC")
      'non cambio l'ordinamento, ma sposto tutti gli altri campi, altrimenti con busnet_is non va (visto che la griglia non ha il sort)
      'dtrT(0)!ui_order = NTSCInt(dtrT(0)!ui_order) + 1
      'dtrT(1)!ui_order = NTSCInt(dtrT(0)!ui_order) - 1
      For i As Integer = 0 To dttProp.Columns.Count - 1
        If dttProp.Columns(i).ColumnName <> "ui_order" Then
          Dim s As String = dttProp.Columns(i).ColumnName
          oTmp = dtrT(0)(dttProp.Columns(i).ColumnName)
          dtrT(0)(dttProp.Columns(i).ColumnName) = dtrT(1)(dttProp.Columns(i).ColumnName)
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
          dtrT(1)(dttProp.Columns(i).ColumnName) = oTmp
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
        End If
      Next
      If strDescr <> grvUi.GetFocusedRowCellValue(ui_descr).ToString Then grvUi.MovePrev()
      grvUi.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
    Dim oTmp As Object
    Try
      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
      If grvUi.FocusedRowHandle = grvUi.RowCount - 1 Then Return

      Dim strDescr As String = grvUi.GetFocusedRowCellValue(ui_descr).ToString
      Dim dtrT() As DataRow
      Dim nPos As Integer = NTSCInt(grvUi.GetFocusedRowCellValue(ui_order))
      dtrT = dttProp.Select("ui_order = " + nPos.ToString + " OR ui_order = " + (nPos + 1).ToString, "ui_order ASC")

      'non cambio l'ordinamento, ma sposto tutti gli altri campi, altrimenti con busnet_is non va (visto che la griglia non ha il sort)
      'dtrT(0)!ui_order = NTSCInt(dtrT(0)!ui_order) + 1
      'dtrT(1)!ui_order = NTSCInt(dtrT(0)!ui_order) - 1
      For i As Integer = 0 To dttProp.Columns.Count - 1
        If dttProp.Columns(i).ColumnName <> "ui_order" Then
          oTmp = dtrT(0)(dttProp.Columns(i).ColumnName)
          dtrT(0)(dttProp.Columns(i).ColumnName) = dtrT(1)(dttProp.Columns(i).ColumnName)
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
          dtrT(1)(dttProp.Columns(i).ColumnName) = oTmp
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
        End If
      Next

      If strDescr <> grvUi.GetFocusedRowCellValue(ui_descr).ToString Then grvUi.MoveNext()
      grvUi.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdUp5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp5.Click
    Dim oTmp As Object
    Try
      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
      If grvUi.FocusedRowHandle = 0 Then Return

      Dim strDescr As String = grvUi.GetFocusedRowCellValue(ui_descr).ToString
      Dim dtrT() As DataRow
      Dim i As Integer
      Dim nPos As Integer = NTSCInt(grvUi.GetFocusedRowCellValue(ui_order))
      dtrT = dttProp.Select("ui_order <= " + nPos.ToString + " AND ui_order >= " + (nPos - 5).ToString, "ui_order ASC")

      For l As Integer = 0 To dttProp.Columns.Count - 1
        If dttProp.Columns(l).ColumnName <> "ui_order" Then
          oTmp = dtrT(dtrT.Length - 1)(dttProp.Columns(l).ColumnName)
          For i = dtrT.Length - 2 To 0 Step -1
            dtrT(i + 1)(dttProp.Columns(l).ColumnName) = dtrT(i)(dttProp.Columns(l).ColumnName)
            dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
          Next
          dtrT(0)(dttProp.Columns(l).ColumnName) = oTmp
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
        End If
      Next

      If strDescr <> grvUi.GetFocusedRowCellValue(ui_descr).ToString Then grvUi.FocusedRowHandle -= (dtrT.Length - 1)
      grvUi.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdDown5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown5.Click
    Dim oTmp As Object
    Try
      grvUi.ClearSorting()
      ui_order.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
      If grvUi.FocusedRowHandle = grvUi.RowCount - 1 Then Return

      Dim strDescr As String = grvUi.GetFocusedRowCellValue(ui_descr).ToString
      Dim dtrT() As DataRow
      Dim i As Integer
      Dim nPos As Integer = NTSCInt(grvUi.GetFocusedRowCellValue(ui_order))
      dtrT = dttProp.Select("ui_order >= " + nPos.ToString + " AND ui_order <= " + (nPos + 5).ToString, "ui_order ASC")

      For l As Integer = 0 To dttProp.Columns.Count - 1
        If dttProp.Columns(l).ColumnName <> "ui_order" Then
          oTmp = dtrT(0)(dttProp.Columns(l).ColumnName)
          For i = 1 To dtrT.Length - 1
            dtrT(i - 1)(dttProp.Columns(l).ColumnName) = dtrT(i)(dttProp.Columns(l).ColumnName)
            dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
          Next
          dtrT(dtrT.Length - 1)(dttProp.Columns(l).ColumnName) = oTmp
          dttProp.AcceptChanges() 'altrimenti su SBC non aggiorna il colore della riga 
        End If
      Next

      If strDescr <> grvUi.GetFocusedRowCellValue(ui_descr).ToString Then grvUi.FocusedRowHandle += (dtrT.Length - 1)
      grvUi.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grUi_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grUi.MouseMove
    If e.Button = Windows.Forms.MouseButtons.Left Then
      Dim i As Integer = 0
    End If
  End Sub

  Public Overridable Sub edRicerca_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edRicerca.TextChanged
    Dim dtrRow As DataRow()
    Dim strFirst As String = ""
    Dim i As Integer
    Try
      'Prima azzero le righe precedentemente impostate
      dtrRow = dttProp.Select("xx_found = 'S'")
      For i = 0 To dtrRow.Length - 1
        dtrRow(i)!xx_found = "N"
        dtrRow(i)!backcolor_ui_descr = -1
      Next

      'Se c'è del testo fa partire la ricerca
      If edRicerca.Text.Trim <> "" Then
        'Filtra i soli campi da evidenziare
        dtrRow = dttProp.Select("ui_colname LIKE " & CStrSQL("%" & edRicerca.Text & "%") & " OR ui_descr LIKE " & CStrSQL("%" & edRicerca.Text & "%"))
        For i = 0 To dtrRow.Length - 1
          If strFirst = "" Then strFirst = NTSCStr(dtrRow(i)!ui_colname)
          dtrRow(i)!xx_found = "S"
          dtrRow(i)!backcolor_ui_descr = Color.Gold.ToArgb
        Next

        'Posiziona il cursore nella prima colonna
        For i = 0 To grvUi.RowCount - 1
          If NTSCStr(grvUi.GetDataRow(i)!ui_colname) = strFirst Then
            grvUi.FocusedRowHandle = i
            Exit For
          End If
        Next
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvSiub_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grvUi.CustomDrawCell
    Try
      'obsoleta
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
