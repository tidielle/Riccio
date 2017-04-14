Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMDBLOMA

#Region "Variabili"
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsLoma As New DataSet
  Public dcLoma As New BindingSource()
  Public dttLoma As DataTable = Nothing
  Public dtrLoma() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public strChiamante As String

  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomin As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ar_inesaur As NTSInformatica.NTSGridColumn
  Public WithEvents ar_ripriord As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar1 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_sublotto As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar3 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_formula As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomax As NTSInformatica.NTSGridColumn
  Public WithEvents ar_perragg As NTSInformatica.NTSGridColumn
  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents grLoma As NTSInformatica.NTSGrid
  Public WithEvents grvLoma As NTSInformatica.NTSGridView
  Public WithEvents cmdZoom As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdMatricole As NTSInformatica.NTSButton
  Public WithEvents cmdLotti As NTSInformatica.NTSButton
  Public WithEvents cmdSpezzaLotto As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents ar_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents cmdAssegnaLotto As NTSInformatica.NTSButton
  Public WithEvents ar_scosic As NTSInformatica.NTSGridColumn

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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGDOCU", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128877106033806429, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGDOCU", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleDocu.lIITTartico = NTSCInt(oCallParams.dPar1)
    oCleDocu.bModificaMateriali = oCallParams.bPar1

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMDBLOMA))
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_scomin = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.ar_desint = New NTSInformatica.NTSGridColumn
    Me.ar_inesaur = New NTSInformatica.NTSGridColumn
    Me.ar_ripriord = New NTSInformatica.NTSGridColumn
    Me.ar_codvar1 = New NTSInformatica.NTSGridColumn
    Me.ar_codvar2 = New NTSInformatica.NTSGridColumn
    Me.ar_sublotto = New NTSInformatica.NTSGridColumn
    Me.ar_codvar3 = New NTSInformatica.NTSGridColumn
    Me.ar_formula = New NTSInformatica.NTSGridColumn
    Me.ar_scomax = New NTSInformatica.NTSGridColumn
    Me.ar_perragg = New NTSInformatica.NTSGridColumn
    Me.ar_scosic = New NTSInformatica.NTSGridColumn
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSpezzaLotto = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdMatricole = New NTSInformatica.NTSButton
    Me.cmdLotti = New NTSInformatica.NTSButton
    Me.cmdZoom = New NTSInformatica.NTSButton
    Me.grLoma = New NTSInformatica.NTSGrid
    Me.grvLoma = New NTSInformatica.NTSGridView
    Me.ar_ubicaz = New NTSInformatica.NTSGridColumn
    Me.cmdAssegnaLotto = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.grLoma, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLoma, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Articolo"
    Me.ar_codart.Enabled = False
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowEdit = False
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsColumn.ReadOnly = True
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 0
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = False
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowEdit = False
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsColumn.ReadOnly = True
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 1
    '
    'ar_scomin
    '
    Me.ar_scomin.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomin.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomin.Caption = "Quant."
    Me.ar_scomin.Enabled = False
    Me.ar_scomin.FieldName = "ar_scomin"
    Me.ar_scomin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomin.Name = "ar_scomin"
    Me.ar_scomin.NTSRepositoryComboBox = Nothing
    Me.ar_scomin.NTSRepositoryItemCheck = Nothing
    Me.ar_scomin.NTSRepositoryItemMemo = Nothing
    Me.ar_scomin.NTSRepositoryItemText = Nothing
    Me.ar_scomin.OptionsColumn.AllowEdit = False
    Me.ar_scomin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomin.OptionsColumn.ReadOnly = True
    Me.ar_scomin.OptionsFilter.AllowFilter = False
    Me.ar_scomin.Visible = True
    Me.ar_scomin.VisibleIndex = 2
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 3
    '
    'ar_desint
    '
    Me.ar_desint.AppearanceCell.Options.UseBackColor = True
    Me.ar_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ar_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_desint.Caption = "Matricola"
    Me.ar_desint.Enabled = True
    Me.ar_desint.FieldName = "ar_desint"
    Me.ar_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_desint.Name = "ar_desint"
    Me.ar_desint.NTSRepositoryComboBox = Nothing
    Me.ar_desint.NTSRepositoryItemCheck = Nothing
    Me.ar_desint.NTSRepositoryItemMemo = Nothing
    Me.ar_desint.NTSRepositoryItemText = Nothing
    Me.ar_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_desint.OptionsFilter.AllowFilter = False
    Me.ar_desint.Visible = True
    Me.ar_desint.VisibleIndex = 4
    '
    'ar_inesaur
    '
    Me.ar_inesaur.AppearanceCell.Options.UseBackColor = True
    Me.ar_inesaur.AppearanceCell.Options.UseTextOptions = True
    Me.ar_inesaur.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_inesaur.Caption = "Rich. lotto"
    Me.ar_inesaur.Enabled = False
    Me.ar_inesaur.FieldName = "ar_inesaur"
    Me.ar_inesaur.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_inesaur.Name = "ar_inesaur"
    Me.ar_inesaur.NTSRepositoryComboBox = Nothing
    Me.ar_inesaur.NTSRepositoryItemCheck = Nothing
    Me.ar_inesaur.NTSRepositoryItemMemo = Nothing
    Me.ar_inesaur.NTSRepositoryItemText = Nothing
    Me.ar_inesaur.OptionsColumn.AllowEdit = False
    Me.ar_inesaur.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_inesaur.OptionsColumn.ReadOnly = True
    Me.ar_inesaur.OptionsFilter.AllowFilter = False
    Me.ar_inesaur.Visible = True
    Me.ar_inesaur.VisibleIndex = 5
    '
    'ar_ripriord
    '
    Me.ar_ripriord.AppearanceCell.Options.UseBackColor = True
    Me.ar_ripriord.AppearanceCell.Options.UseTextOptions = True
    Me.ar_ripriord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_ripriord.Caption = "Rich.Matr."
    Me.ar_ripriord.Enabled = False
    Me.ar_ripriord.FieldName = "ar_ripriord"
    Me.ar_ripriord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_ripriord.Name = "ar_ripriord"
    Me.ar_ripriord.NTSRepositoryComboBox = Nothing
    Me.ar_ripriord.NTSRepositoryItemCheck = Nothing
    Me.ar_ripriord.NTSRepositoryItemMemo = Nothing
    Me.ar_ripriord.NTSRepositoryItemText = Nothing
    Me.ar_ripriord.OptionsColumn.AllowEdit = False
    Me.ar_ripriord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_ripriord.OptionsColumn.ReadOnly = True
    Me.ar_ripriord.OptionsFilter.AllowFilter = False
    Me.ar_ripriord.Visible = True
    Me.ar_ripriord.VisibleIndex = 6
    '
    'ar_codvar1
    '
    Me.ar_codvar1.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar1.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar1.Caption = "Riga Scarico"
    Me.ar_codvar1.Enabled = False
    Me.ar_codvar1.FieldName = "ar_codvar1"
    Me.ar_codvar1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar1.Name = "ar_codvar1"
    Me.ar_codvar1.NTSRepositoryComboBox = Nothing
    Me.ar_codvar1.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar1.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar1.NTSRepositoryItemText = Nothing
    Me.ar_codvar1.OptionsColumn.AllowEdit = False
    Me.ar_codvar1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar1.OptionsColumn.ReadOnly = True
    Me.ar_codvar1.OptionsFilter.AllowFilter = False
    Me.ar_codvar1.Visible = True
    Me.ar_codvar1.VisibleIndex = 7
    '
    'ar_codvar2
    '
    Me.ar_codvar2.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar2.Caption = "Riga"
    Me.ar_codvar2.Enabled = False
    Me.ar_codvar2.FieldName = "ar_codvar2"
    Me.ar_codvar2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar2.Name = "ar_codvar2"
    Me.ar_codvar2.NTSRepositoryComboBox = Nothing
    Me.ar_codvar2.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar2.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar2.NTSRepositoryItemText = Nothing
    Me.ar_codvar2.OptionsColumn.AllowEdit = False
    Me.ar_codvar2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar2.OptionsColumn.ReadOnly = True
    Me.ar_codvar2.OptionsFilter.AllowFilter = False
    Me.ar_codvar2.Visible = True
    Me.ar_codvar2.VisibleIndex = 8
    '
    'ar_sublotto
    '
    Me.ar_sublotto.AppearanceCell.Options.UseBackColor = True
    Me.ar_sublotto.AppearanceCell.Options.UseTextOptions = True
    Me.ar_sublotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_sublotto.Caption = "Magazzino"
    Me.ar_sublotto.Enabled = False
    Me.ar_sublotto.FieldName = "ar_sublotto"
    Me.ar_sublotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_sublotto.Name = "ar_sublotto"
    Me.ar_sublotto.NTSRepositoryComboBox = Nothing
    Me.ar_sublotto.NTSRepositoryItemCheck = Nothing
    Me.ar_sublotto.NTSRepositoryItemMemo = Nothing
    Me.ar_sublotto.NTSRepositoryItemText = Nothing
    Me.ar_sublotto.OptionsColumn.AllowEdit = False
    Me.ar_sublotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_sublotto.OptionsColumn.ReadOnly = True
    Me.ar_sublotto.OptionsFilter.AllowFilter = False
    Me.ar_sublotto.Visible = True
    Me.ar_sublotto.VisibleIndex = 9
    '
    'ar_codvar3
    '
    Me.ar_codvar3.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar3.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar3.Caption = "Fase"
    Me.ar_codvar3.Enabled = False
    Me.ar_codvar3.FieldName = "ar_codvar3"
    Me.ar_codvar3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar3.Name = "ar_codvar3"
    Me.ar_codvar3.NTSRepositoryComboBox = Nothing
    Me.ar_codvar3.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar3.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar3.NTSRepositoryItemText = Nothing
    Me.ar_codvar3.OptionsColumn.AllowEdit = False
    Me.ar_codvar3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar3.OptionsColumn.ReadOnly = True
    Me.ar_codvar3.OptionsFilter.AllowFilter = False
    Me.ar_codvar3.Visible = True
    Me.ar_codvar3.VisibleIndex = 10
    '
    'ar_formula
    '
    Me.ar_formula.AppearanceCell.Options.UseBackColor = True
    Me.ar_formula.AppearanceCell.Options.UseTextOptions = True
    Me.ar_formula.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_formula.Caption = "Descr. fase"
    Me.ar_formula.Enabled = False
    Me.ar_formula.FieldName = "ar_formula"
    Me.ar_formula.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_formula.Name = "ar_formula"
    Me.ar_formula.NTSRepositoryComboBox = Nothing
    Me.ar_formula.NTSRepositoryItemCheck = Nothing
    Me.ar_formula.NTSRepositoryItemMemo = Nothing
    Me.ar_formula.NTSRepositoryItemText = Nothing
    Me.ar_formula.OptionsColumn.AllowEdit = False
    Me.ar_formula.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_formula.OptionsColumn.ReadOnly = True
    Me.ar_formula.OptionsFilter.AllowFilter = False
    Me.ar_formula.Visible = True
    Me.ar_formula.VisibleIndex = 11
    '
    'ar_scomax
    '
    Me.ar_scomax.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomax.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomax.Caption = "Qta Scart."
    Me.ar_scomax.Enabled = False
    Me.ar_scomax.FieldName = "ar_scomax"
    Me.ar_scomax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomax.Name = "ar_scomax"
    Me.ar_scomax.NTSRepositoryComboBox = Nothing
    Me.ar_scomax.NTSRepositoryItemCheck = Nothing
    Me.ar_scomax.NTSRepositoryItemMemo = Nothing
    Me.ar_scomax.NTSRepositoryItemText = Nothing
    Me.ar_scomax.OptionsColumn.AllowEdit = False
    Me.ar_scomax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomax.OptionsColumn.ReadOnly = True
    Me.ar_scomax.OptionsFilter.AllowFilter = False
    '
    'ar_perragg
    '
    Me.ar_perragg.AppearanceCell.Options.UseBackColor = True
    Me.ar_perragg.AppearanceCell.Options.UseTextOptions = True
    Me.ar_perragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_perragg.Caption = "Scarico Prod"
    Me.ar_perragg.Enabled = False
    Me.ar_perragg.FieldName = "ar_perragg"
    Me.ar_perragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_perragg.Name = "ar_perragg"
    Me.ar_perragg.NTSRepositoryComboBox = Nothing
    Me.ar_perragg.NTSRepositoryItemCheck = Nothing
    Me.ar_perragg.NTSRepositoryItemMemo = Nothing
    Me.ar_perragg.NTSRepositoryItemText = Nothing
    Me.ar_perragg.OptionsColumn.AllowEdit = False
    Me.ar_perragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_perragg.OptionsColumn.ReadOnly = True
    Me.ar_perragg.OptionsFilter.AllowFilter = False
    Me.ar_perragg.Visible = True
    Me.ar_perragg.VisibleIndex = 12
    '
    'ar_scosic
    '
    Me.ar_scosic.AppearanceCell.Options.UseBackColor = True
    Me.ar_scosic.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scosic.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scosic.Caption = "Riga cambiata"
    Me.ar_scosic.Enabled = False
    Me.ar_scosic.FieldName = "ar_scosic"
    Me.ar_scosic.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scosic.Name = "ar_scosic"
    Me.ar_scosic.NTSRepositoryComboBox = Nothing
    Me.ar_scosic.NTSRepositoryItemCheck = Nothing
    Me.ar_scosic.NTSRepositoryItemMemo = Nothing
    Me.ar_scosic.NTSRepositoryItemText = Nothing
    Me.ar_scosic.OptionsColumn.AllowEdit = False
    Me.ar_scosic.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scosic.OptionsColumn.ReadOnly = True
    Me.ar_scosic.OptionsFilter.AllowFilter = False
    Me.ar_scosic.Visible = True
    Me.ar_scosic.VisibleIndex = 13
    '
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdAssegnaLotto)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdSpezzaLotto)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdMatricole)
    Me.pnButton.Controls.Add(Me.cmdLotti)
    Me.pnButton.Controls.Add(Me.cmdZoom)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnButton.Location = New System.Drawing.Point(0, 302)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.NTSActiveTrasparency = True
    Me.pnButton.Size = New System.Drawing.Size(695, 64)
    Me.pnButton.TabIndex = 1
    Me.pnButton.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(456, 34)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(100, 26)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSpezzaLotto
    '
    Me.cmdSpezzaLotto.ImagePath = ""
    Me.cmdSpezzaLotto.ImageText = ""
    Me.cmdSpezzaLotto.Location = New System.Drawing.Point(3, 34)
    Me.cmdSpezzaLotto.Name = "cmdSpezzaLotto"
    Me.cmdSpezzaLotto.NTSContextMenu = Nothing
    Me.cmdSpezzaLotto.Size = New System.Drawing.Size(100, 26)
    Me.cmdSpezzaLotto.TabIndex = 4
    Me.cmdSpezzaLotto.Text = "Dividi lotto"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(592, 34)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(100, 26)
    Me.cmdConferma.TabIndex = 3
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdMatricole
    '
    Me.cmdMatricole.ImagePath = ""
    Me.cmdMatricole.ImageText = ""
    Me.cmdMatricole.Location = New System.Drawing.Point(109, 3)
    Me.cmdMatricole.Name = "cmdMatricole"
    Me.cmdMatricole.NTSContextMenu = Nothing
    Me.cmdMatricole.Size = New System.Drawing.Size(100, 26)
    Me.cmdMatricole.TabIndex = 2
    Me.cmdMatricole.Text = "&Matricole aperte"
    '
    'cmdLotti
    '
    Me.cmdLotti.ImagePath = ""
    Me.cmdLotti.ImageText = ""
    Me.cmdLotti.Location = New System.Drawing.Point(109, 34)
    Me.cmdLotti.Name = "cmdLotti"
    Me.cmdLotti.NTSContextMenu = Nothing
    Me.cmdLotti.Size = New System.Drawing.Size(100, 26)
    Me.cmdLotti.TabIndex = 1
    Me.cmdLotti.Text = "&Lotti aperti"
    '
    'cmdZoom
    '
    Me.cmdZoom.ImagePath = ""
    Me.cmdZoom.ImageText = ""
    Me.cmdZoom.Location = New System.Drawing.Point(3, 3)
    Me.cmdZoom.Name = "cmdZoom"
    Me.cmdZoom.NTSContextMenu = Nothing
    Me.cmdZoom.Size = New System.Drawing.Size(100, 26)
    Me.cmdZoom.TabIndex = 0
    Me.cmdZoom.Text = "&Zoom"
    '
    'grLoma
    '
    Me.grLoma.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grLoma.EmbeddedNavigator.Name = ""
    Me.grLoma.Location = New System.Drawing.Point(0, 0)
    Me.grLoma.MainView = Me.grvLoma
    Me.grLoma.Name = "grLoma"
    Me.grLoma.Size = New System.Drawing.Size(695, 302)
    Me.grLoma.TabIndex = 2
    Me.grLoma.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLoma})
    '
    'grvLoma
    '
    Me.grvLoma.ActiveFilterEnabled = False
    Me.grvLoma.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ar_codart, Me.ar_descr, Me.ar_scomin, Me.xx_lottox, Me.ar_desint, Me.ar_inesaur, Me.ar_ripriord, Me.ar_codvar1, Me.ar_codvar2, Me.ar_sublotto, Me.ar_codvar3, Me.ar_formula, Me.ar_scomax, Me.ar_perragg, Me.ar_scosic, Me.ar_ubicaz})
    Me.grvLoma.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvLoma.Enabled = True
    Me.grvLoma.GridControl = Me.grLoma
    Me.grvLoma.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLoma.MinRowHeight = 14
    Me.grvLoma.Name = "grvLoma"
    Me.grvLoma.NTSAllowDelete = True
    Me.grvLoma.NTSAllowInsert = True
    Me.grvLoma.NTSAllowUpdate = True
    Me.grvLoma.NTSMenuContext = Nothing
    Me.grvLoma.OptionsCustomization.AllowRowSizing = True
    Me.grvLoma.OptionsFilter.AllowFilterEditor = False
    Me.grvLoma.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLoma.OptionsNavigation.UseTabKey = False
    Me.grvLoma.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLoma.OptionsView.ColumnAutoWidth = False
    Me.grvLoma.OptionsView.EnableAppearanceEvenRow = True
    Me.grvLoma.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLoma.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLoma.OptionsView.ShowGroupPanel = False
    Me.grvLoma.RowHeight = 16
    '
    'ar_ubicaz
    '
    Me.ar_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.ar_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.ar_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_ubicaz.Caption = "Ubicazione"
    Me.ar_ubicaz.Enabled = True
    Me.ar_ubicaz.FieldName = "ar_ubicaz"
    Me.ar_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_ubicaz.Name = "ar_ubicaz"
    Me.ar_ubicaz.NTSRepositoryComboBox = Nothing
    Me.ar_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.ar_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.ar_ubicaz.NTSRepositoryItemText = Nothing
    Me.ar_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_ubicaz.OptionsFilter.AllowFilter = False
    '
    'cmdAssegnaLotto
    '
    Me.cmdAssegnaLotto.ImagePath = ""
    Me.cmdAssegnaLotto.ImageText = ""
    Me.cmdAssegnaLotto.Location = New System.Drawing.Point(215, 34)
    Me.cmdAssegnaLotto.Name = "cmdAssegnaLotto"
    Me.cmdAssegnaLotto.NTSContextMenu = Nothing
    Me.cmdAssegnaLotto.Size = New System.Drawing.Size(100, 26)
    Me.cmdAssegnaLotto.TabIndex = 6
    Me.cmdAssegnaLotto.Text = "Calcola lotto"
    '
    'FRMDBLOMA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(695, 366)
    Me.Controls.Add(Me.grLoma)
    Me.Controls.Add(Me.pnButton)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpContext = ""
    Me.Name = "FRMDBLOMA"
    Me.Text = "DETTAGLIO LOTTI / MATRICOLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.grLoma, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLoma, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvLoma.NTSSetParam(oMenu, oApp.Tr(Me, 130086828454585675, "Lotti matricole"))

      ar_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202580109794, "Articolo"), CLN__STD.CodartMaxLen, False)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202580266047, "Descrizione"), 40, False)
      ar_scomin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883202580422300, "Quant."), "#,##0.00", 15)
      If oCleDocu.bLottoNew = False Then
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202580578553, "Lotto"), 9, True)
      Else
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129524541334779079, "Lotto"), 50, True)
      End If
      ar_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202580734806, "Matricola"), 40, True)
      ar_inesaur.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128883202580891059, "Rich. lotto"), "S", "N")
      ar_ripriord.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128883202581047312, "Rich.Matr."), "S", "N")
      ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202581203565, "Riga Scarico"), 8, False)
      ar_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202581359818, "Riga"), 8, False)
      ar_sublotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883202581516071, "Magazzino"), "#,##0.00", 15)
      ar_codvar3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202581672324, "Fase"), 4, False)
      ar_formula.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883202581828577, "Descr. fase"), 0, True)
      ar_scomax.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883202581984830, "Qta Scart."), "#,##0.00", 15)
      ar_perragg.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128883202582141083, "Scarico Prod"), "G", "T")
      ar_scosic.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883202582297336, "Riga cambiata"), "#,##0.00", 15)

      ar_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130993942862098772, "Ubicazione"), 18, True)
      ar_ubicaz.NTSSetParamZoom("ZOOMUBICAZ")

      grvLoma.NTSAllowInsert = False
      grvLoma.NTSAllowDelete = False

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

  Public Overridable Sub Apri()
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.LomaApri(DittaCorrente, dsLoma) Then
        Me.Close()
        Return
      End If

      dcLoma.DataSource = Nothing
      dcLoma.DataSource = dsLoma.Tables("TTARTICO")
      grLoma.DataSource = dcLoma

      'se è un carico da prod devo bloccare la modificabilità di articolo e quantità
      'e nascondere le colonne matricola/lotto
      'in caso contrario visualizzare colonne matricola / lotto
      If oCleDocu.bModificaMateriali Then
        GctlSetVisEnab(ar_codart, False)
        GctlSetVisEnab(ar_codvar3, False)
        GctlSetVisEnab(ar_scomin, False)
        GctlSetVisEnab(ar_scomax, False)

        cmdLotti.Visible = False
        cmdSpezzaLotto.Visible = False
        cmdMatricole.Visible = False

        ar_inesaur.Visible = False
        ar_ripriord.Visible = False
        xx_lottox.Visible = False
        ar_desint.Visible = False
        ar_codvar2.Visible = False

        Me.Text = oApp.Tr(Me, 130086829758023175, "Modifica scarichi")
      Else
        cmdZoom.Visible = False
        ar_codart.Enabled = False
        ar_codvar3.Visible = False
        ar_scomin.Enabled = False
        ar_scomax.Enabled = False

        If strChiamante = "BSDBAVE2" Then
          cmdLotti.Visible = False
          cmdSpezzaLotto.Visible = False
          cmdMatricole.Visible = False
        Else
          cmdLotti.Visible = True
          If dsLoma.Tables("TTARTICO").Rows.Count > 0 AndAlso NTSCStr(dsLoma.Tables("TTARTICO").Rows(0)!ar_perragg) = "G" Then
            'riga di scarico a produzione
            GctlSetVisEnab(cmdSpezzaLotto, True)
          Else
            'riga di carico da prod
            cmdSpezzaLotto.Visible = False
          End If
          cmdMatricole.Visible = True
        End If
        GctlSetVisEnab(ar_inesaur, True)
        GctlSetVisEnab(ar_ripriord, True)
        GctlSetVisEnab(xx_lottox, True)
        GctlSetVisEnab(ar_desint, True)
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvLoma.NTSSalvaRigaCorrente(dcLoma, oCleDocu.LomaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleDocu.LomaSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleDocu.LomaRipristina(dcLoma.Position, dcLoma.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMDBLOMA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleDocu.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()
      oCleDocu.bLottoUnivoco = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "LottoUnivoco", "0", " ", "0"))

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub
  Public Overridable Sub FRMDBLOMA_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      grvLoma.FocusedColumn = xx_lottox
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMDBLOMA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLoma.Dispose()
      dsLoma.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMDBLOMA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCallParams.bPar5 Then Return 'Se ho premuto annulla devo uscire senza salvare.

      If Not Salva() Then e.Cancel = True
      If Not TestClosing() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvLoma_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvLoma.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvLoma_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLoma.NTSFocusedRowChanged
    Try
      '-------------------------------------------------
      'se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCStr(grvLoma.GetFocusedRowCellValue(ar_inesaur).ToString) = "S" Then
        GctlSetVisEnab(xx_lottox, False)
      Else
        xx_lottox.Enabled = False
      End If
      If NTSCStr(grvLoma.GetFocusedRowCellValue(ar_ripriord).ToString) = "S" Then
        GctlSetVisEnab(ar_desint, False)
      Else
        ar_desint.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZoom.Click
    Dim oPar As New CLE__CLDP
    Dim frmHla2 As FRMMGHLLO = Nothing
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grvLoma.FocusedColumn.Name.ToLower = "ar_codart" Then
          If strChiamante <> "BSDBAVE2" Then
            NTSZOOM.strIn = NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codart)
            oParam.bVisGriglia = True
            oParam.strTipoArticolo = "N"
            NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
            grvLoma.SetFocusedRowCellValue(ar_codart, NTSZOOM.strIn) 'If NTSZOOM.strIn <> NTSCStr(grvLoma.EditingValue) Then
          Else
            frmHla2 = CType(NTSNewFormModal("FRMMGHLLO"), FRMMGHLLO)
            frmHla2.Init(oMenu, oPar, DittaCorrente)
            frmHla2.InitEntity(oCleDocu)
            frmHla2.ShowDialog()

            If oCleDocu.strCodart <> "" Then
              grvLoma.NTSGetCurrentDataRow()!ar_codart = oCleDocu.strCodart
            End If
          End If

          If grvLoma.FocusedColumn.Name.ToLower = "ar_codvar3" And strChiamante <> "BSDBAVE2" Then
            If Trim(NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codart)) = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128874799801852165, "Indicare l'articolo prima di inserire la fase."))
            End If

            oParam.strTipo = NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codart)
            NTSZOOM.strIn = NTSCStr(grvLoma.EditingValue)
            NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvLoma.EditingValue) Then grvLoma.SetFocusedRowCellValue(ar_codvar3, NTSZOOM.strIn)
            Return
          End If
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHla2 Is Nothing Then frmHla2.Dispose()
      frmHla2 = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdLotti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLotti.Click
    Dim oParam As New CLE__CLDP
    Dim frmHllo As FRMMGHLLO = Nothing
    Try
      If grvLoma.NTSGetCurrentDataRow() Is Nothing Then Return

      grvLoma.NTSAllowInsert = True
      If Not Salva() Then Return
      grvLoma.NTSAllowInsert = False

      If NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_inesaur) = "S" Then
        oCleDocu.strHlloCodart = NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codart)
        oCleDocu.strHlloDesart = NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_descr)
        oCleDocu.nHlloMagaz = NTSCInt(grvLoma.NTSGetCurrentDataRow()!ar_sublotto)
        oCleDocu.nHlloFase = NTSCInt(grvLoma.NTSGetCurrentDataRow()!ar_codvar3)

        oCleDocu.strHlloDesmaga = ""

        oCleDocu.Tabmaga_Validated(NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_sublotto), oCleDocu.strHlloDesmaga)
        frmHllo = CType(NTSNewFormModal("FRMMGHLLO"), FRMMGHLLO)
        frmHllo.strParent = "BNDBLOMA"
        frmHllo.dttRkOut = New DataTable
        frmHllo.dQtaTot = NTSCDec(grvLoma.NTSGetCurrentDataRow()!ar_scomin)
        frmHllo.Init(oMenu, oParam, DittaCorrente)
        frmHllo.InitEntity(oCleDocu)
        frmHllo.ShowDialog()

        If oCleDocu.lHlloLotto = 0 Or oCleDocu.dHlloQuant = 0 Or frmHllo.dttRkOut.Rows.Count = 0 Then Return

        'memorizzo la mia posizione in griglia
        'i = ds.Recordset.AbsolutePosition
        'nCol = griTtartico.Col
        'ChiudiDs()

        'oCleDocu.LomaUpdateTTArtico(NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codvar1))
        If frmHllo.dttRkOut.Rows.Count = 1 Then
          'ho solo fatto doppio click su una riga: prendo su solo il lotto
          grvLoma.NTSGetCurrentDataRow!ar_maxlotto = oCleDocu.lHlloLotto
          If Not Salva() Then Return
        Else
          'selezione di più lotti :aggiorno la riga corrente e ne aggiungo altre
          For i As Integer = 1 To frmHllo.dttRkOut.Rows.Count - 1
            DividiRiga(grvLoma.NTSGetCurrentDataRow, NTSCInt(frmHllo.dttRkOut.Rows(i)!xx_qtaeva))
          Next

          grvLoma.NTSGetCurrentDataRow!ar_maxlotto = NTSCInt(frmHllo.dttRkOut.Rows(0)!xx_lotto)
          grvLoma.NTSGetCurrentDataRow!ar_scomin = NTSCInt(frmHllo.dttRkOut.Rows(0)!xx_qtaeva)
          If Not Salva() Then
            'se non salva è per test lotto univoco!!!
            grvLoma.NTSGetCurrentDataRow!ar_maxlotto = 0
            Salva()
          End If

          'correggo lotto e qta sulle righe aggiunte
          'le nuove righe sono in fondo, per cui parto dalle ultime ad aggiornare i dati
          For i As Integer = 1 To frmHllo.dttRkOut.Rows.Count - 1
            dsLoma.Tables("TTARTICO").Rows(dsLoma.Tables("TTARTICO").Rows.Count - i)!ar_maxlotto = NTSCInt(frmHllo.dttRkOut.Rows(i)!xx_lotto)
            dsLoma.Tables("TTARTICO").Rows(dsLoma.Tables("TTARTICO").Rows.Count - i)!ar_scomin = NTSCInt(frmHllo.dttRkOut.Rows(i)!xx_qtaeva)
            If Not Salva() Then
              'se non salva è per test lotto univoco!!!
              dsLoma.Tables("TTARTICO").Rows(dsLoma.Tables("TTARTICO").Rows.Count - i)!ar_maxlotto = 0
              Salva()
            End If
          Next
        End If


        'aggiorno la griglia per far vedere le colonne lotto modificate e mi riposiziono sulla vecchia riga
        Apri()
        'ds.Recordset.AbsolutePosition = i
        'griTtartico.Col = nCol

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      grvLoma.NTSAllowInsert = False
      frmHllo.dttRkOut.Clear()
      frmHllo.dttRkOut.Dispose()
      frmHllo.dttRkOut = Nothing
      If Not frmHllo Is Nothing Then frmHllo.Dispose()
      frmHllo = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdSpezzaLotto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpezzaLotto.Click
    Dim strTmp As String = ""
    Dim dTmp As Decimal = 0
    Try
      If grvLoma.NTSGetCurrentDataRow() Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130685582445632998, "Posizionarsi sulla riga dell'articolo gestito a lotti da dividere"))
        Return
      End If
      If NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_ripriord) = "N" And NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_inesaur) = "S" Then
        'posso proseguire
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 130685575799458729, "E' possibile dividere solo articoli gestiti a lotti e non gestiti a matricole"))
        Return
      End If

      grvLoma.NTSAllowInsert = True
      If Not Salva() Then Return
      grvLoma.NTSAllowInsert = False

      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 130685582978220580, "Indicare la quantità:"), 0.ToString(oApp.FormatQta), "N", False)
      If Not IsNumeric(strTmp) OrElse NTSCDec(strTmp) = 0 Then Return
      dTmp = NTSCDec(strTmp)
      If dTmp < 0 Or dTmp > NTSCDec(grvLoma.NTSGetCurrentDataRow()!ar_scomin) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130685584780864930, "La quantità deve essere compresa tra 0 e |" & NTSCDec(grvLoma.NTSGetCurrentDataRow()!ar_scomin).ToString(oApp.FormatQta) & "|"))
        Return
      End If
      If dTmp <> NTSCDec(dTmp.ToString(oApp.FormatQta)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130685584780864931, "La quantità indicata ha troppi decimali!"))
        Return
      End If

      DividiRiga(grvLoma.NTSGetCurrentDataRow(), dTmp)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAssegnaLotto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAssegnaLotto.Click
    Try
      For i As Integer = 0 To dsLoma.Tables("TTARTICO").Rows.Count - 1
        With dsLoma.Tables("TTARTICO").Rows(i)
          If NTSCStr(!xx_lottox).Trim <> "" Then Continue For 'Se il lotto è già stato assegnato non devo fare altro
          'Se l'articolo non è gestito a lotti o se non ha un sistema di attribuzione lotto assegnato non devo fare nulla
          Dim dttArtico As New DataTable
          If Not oMenu.ValCodiceDb(NTSCStr(!ar_codart), DittaCorrente, "ARTICO", "S", , dttArtico) Then Continue For 'Non dovrebbe mai capitare
          If NTSCStr(dttArtico.Rows(0)!ar_geslotti) = "N" Then Continue For
          If NTSCStr(dttArtico.Rows(0)!ar_tipscarlotx) = "M" Then Continue For

          Dim dttLotti As DataTable = Nothing
          oCleDocu.Hllo_ProponiLottox(NTSCStr(!ar_codart), NTSCInt(!ar_codvar3), NTSCInt(!ar_sublotto), NTSCDec(!ar_scomin), _
                                      Now.ToShortDateString, dttLotti)

          Dim dtrRiga As DataRow = dsLoma.Tables("TTARTICO").Rows(i)
          For Each dtrLotto As DataRow In dttLotti.Rows
            If NTSCDec(dtrLotto!lp_esist) >= NTSCDec(!ar_scomin) Then
              !xx_lottox = dtrLotto!alo_lotto
            Else
              DividiRiga(dtrRiga, NTSCDec(dtrLotto!lp_esist))
              dtrRiga!xx_lottox = dtrLotto!alo_lotto

              dtrRiga = dsLoma.Tables("TTARTICO").Rows(dsLoma.Tables("TTARTICO").Rows.Count - 1)
            End If
          Next
        End With
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdMatricole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMatricole.Click
    Dim strParam As String
    Dim oPar As New CLE__PATB
    Try
      If grvLoma.NTSGetCurrentDataRow() Is Nothing Then Return

      If NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_ripriord) = "S" Then
        strParam = "BNVEBOLL;"
        strParam = strParam & NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codart) & ";"
        strParam = strParam & NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_sublotto) & ";"
        strParam = strParam & "2;"
        strParam = strParam & "TTSTMATR;"
        strParam = strParam & "TTSTMATS;"
        strParam = strParam & ";;;;;;;;;;;;;"
        strParam = strParam & NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codvar3) & ";"
        strParam = strParam & NTSCStr(grvLoma.NTSGetCurrentDataRow()!ar_codvar3) & ";"
        oPar.strTipo = strParam
        NTSZOOM.ZoomStrIn("ZOOMSCHEDEARTMATR", DittaCorrente, oPar)
        If oPar.strOut.Trim <> "" Then
          grvLoma.NTSGetCurrentDataRow()!ar_desint = oPar.strOut.Split(";"c)(0)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCallParams.bPar5 = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  

  Public Overridable Function TestClosing() As Boolean
    Dim i As Integer
    Try
      '-----------------------------------------------------------
      'devo controllare che lotti e matricole siano stati inseriti tutti, altrimenti non faccio andare avanti
      If oCleDocu.bModificaMateriali = False Then
        For i = 0 To dsLoma.Tables("TTARTICO").Rows.Count - 1
          With dsLoma.Tables("TTARTICO").Rows(i)
            If NTSCStr(!ar_inesaur) = "S" And NTSCStr(!xx_lottox) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128877105988181429, "Su alcune righe dove è richiesto il numero di lotto tale valore non è stato imputato. Articolo |'" & NTSCStr(!ar_codart) & "|'"))
              Return False
            End If

            If NTSCStr(!ar_ripriord) = "S" And (Trim(NTSCStr(!ar_desint)) = "" Or NTSCStr(!ar_desint) = "") Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128877106071306429, "Su alcune righe dove è richiesto il numero di matricola tale valore non è stato imputato. Articolo '|" & NTSCStr(!ar_codart) & "|'"))
              Return False
            End If
          End With
        Next
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Function DividiRiga(ByRef dtrRiga As DataRow, ByVal dQtaNew As Decimal) As Boolean
    Dim dttTmp As New DataTable
    Dim nRiga As Integer = 0
    Dim bTmp As Boolean = False
    Try
      grvLoma.NTSAllowInsert = True
      If Not Salva() Then Return False

      'aggiorno la riga
      dtrRiga!ar_scomin = NTSCDec(dtrRiga!ar_scomin) - dQtaNew
      Salva()

      'inserisco la nuova riga 
      'devo trovare il numero di riga temporaneo da assegnare a quella nuova
      dttTmp = dsLoma.Tables("TTARTICO").Clone
      For Each dtrT As DataRow In dsLoma.Tables("TTARTICO").Rows
        If NTSCInt(dtrT!ar_codvar2) > nRiga Then nRiga = NTSCInt(dtrT!ar_codvar2)
      Next
      nRiga += 1
      dttTmp.ImportRow(dtrRiga)
      dttTmp.Rows(0)!ar_scomin = dQtaNew
      dttTmp.Rows(0)!ar_codvar2 = nRiga.ToString
      dsLoma.Tables("TTARTICO").ImportRow(dttTmp.Rows(0))
      dttTmp.Clear()
      bTmp = oCleDocu.bLottoUnivoco
      oCleDocu.bLottoUnivoco = False
      dsLoma.Tables("TTARTICO").AcceptChanges()
      dsLoma.Tables("TTARTICO").Rows(dsLoma.Tables("TTARTICO").Rows.Count - 1).SetAdded()
      grvLoma.NTSAllowInsert = True
      oCleDocu.bLomaHasChanges = True
      Salva()
      grvLoma.NTSAllowInsert = False
      oCleDocu.bLottoUnivoco = bTmp

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      grvLoma.NTSAllowInsert = False
    End Try
  End Function


End Class