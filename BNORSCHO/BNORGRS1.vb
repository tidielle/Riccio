Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGRS1

#Region "Variabili"
  Public oCleScho As CLEORSCHO
  Public oCallParams As CLE__CLDP
  Public dsGrs1 As DataSet
  Public dcGrs1 As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bNoModal As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrs1 As NTSInformatica.NTSGrid
  Public WithEvents grvGrs1 As NTSInformatica.NTSGridView
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel

  Public WithEvents xx_origine As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datord As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents mo_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_consolidato As NTSInformatica.NTSGridColumn
  Public WithEvents xx_proposto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_disp As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents mo_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents mo_anno As NTSInformatica.NTSGridColumn
  Public WithEvents mo_serie As NTSInformatica.NTSGridColumn
  Public WithEvents mo_numord As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents mo_olprogr As NTSInformatica.NTSGridColumn
  Public WithEvents mo_origine As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codpadr As NTSInformatica.NTSGridColumn
  Public WithEvents mo_despadr As NTSInformatica.NTSGridColumn
  Public WithEvents mo_fasepadr As NTSInformatica.NTSGridColumn
  'per compatibilita con la store procedure
  Public WithEvents mo_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents t1o As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents t2o As NTSInformatica.NTSGridColumn
  Public WithEvents t3o As NTSInformatica.NTSGridColumn
  Public WithEvents t4o As NTSInformatica.NTSGridColumn
  Public WithEvents t5o As NTSInformatica.NTSGridColumn
  Public WithEvents t6o As NTSInformatica.NTSGridColumn
  Public WithEvents t7o As NTSInformatica.NTSGridColumn
  Public WithEvents t8o As NTSInformatica.NTSGridColumn
  Public WithEvents t9o As NTSInformatica.NTSGridColumn
  Public WithEvents t11o As NTSInformatica.NTSGridColumn
  Public WithEvents t12o As NTSInformatica.NTSGridColumn
  Public WithEvents t13o As NTSInformatica.NTSGridColumn
  Public WithEvents t14o As NTSInformatica.NTSGridColumn
  Public WithEvents t15o As NTSInformatica.NTSGridColumn
  Public WithEvents t16o As NTSInformatica.NTSGridColumn
  Public WithEvents t17o As NTSInformatica.NTSGridColumn
  Public WithEvents t18o As NTSInformatica.NTSGridColumn
  Public WithEvents t19o As NTSInformatica.NTSGridColumn
  Public WithEvents t20o As NTSInformatica.NTSGridColumn
  Public WithEvents t21o As NTSInformatica.NTSGridColumn
  Public WithEvents t22o As NTSInformatica.NTSGridColumn
  Public WithEvents t23o As NTSInformatica.NTSGridColumn
  Public WithEvents t24o As NTSInformatica.NTSGridColumn
  Public WithEvents t1p As NTSInformatica.NTSGridColumn
  Public WithEvents t2p As NTSInformatica.NTSGridColumn
  Public WithEvents t3p As NTSInformatica.NTSGridColumn
  Public WithEvents t4p As NTSInformatica.NTSGridColumn
  Public WithEvents t5p As NTSInformatica.NTSGridColumn
  Public WithEvents t6p As NTSInformatica.NTSGridColumn
  Public WithEvents t7p As NTSInformatica.NTSGridColumn
  Public WithEvents t8p As NTSInformatica.NTSGridColumn
  Public WithEvents t9p As NTSInformatica.NTSGridColumn
  Public WithEvents t10p As NTSInformatica.NTSGridColumn
  Public WithEvents t11p As NTSInformatica.NTSGridColumn
  Public WithEvents t12p As NTSInformatica.NTSGridColumn
  Public WithEvents t13p As NTSInformatica.NTSGridColumn
  Public WithEvents t14p As NTSInformatica.NTSGridColumn
  Public WithEvents t15p As NTSInformatica.NTSGridColumn
  Public WithEvents t16p As NTSInformatica.NTSGridColumn
  Public WithEvents t17p As NTSInformatica.NTSGridColumn
  Public WithEvents t18p As NTSInformatica.NTSGridColumn
  Public WithEvents t19p As NTSInformatica.NTSGridColumn
  Public WithEvents t20p As NTSInformatica.NTSGridColumn
  Public WithEvents t21p As NTSInformatica.NTSGridColumn
  Public WithEvents t22p As NTSInformatica.NTSGridColumn
  Public WithEvents t23p As NTSInformatica.NTSGridColumn
  Public WithEvents t24p As NTSInformatica.NTSGridColumn

  Public WithEvents t10o As NTSInformatica.NTSGridColumn
  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbXx_Magaz As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbUnmis As NTSInformatica.NTSLabel
  Public WithEvents lbAdatcons As NTSInformatica.NTSLabel
  Public WithEvents lbDadatcons As NTSInformatica.NTSLabel
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazioneDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazioneMrp As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edUnmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum

  Public WithEvents tlbApriGestione As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiornaOrdine As NTSInformatica.NTSBarButtonItem

  Public WithEvents t1d As NTSInformatica.NTSGridColumn
  Public WithEvents t2d As NTSInformatica.NTSGridColumn
  Public WithEvents t3d As NTSInformatica.NTSGridColumn
  Public WithEvents t4d As NTSInformatica.NTSGridColumn
  Public WithEvents t5d As NTSInformatica.NTSGridColumn
  Public WithEvents t6d As NTSInformatica.NTSGridColumn
  Public WithEvents t7d As NTSInformatica.NTSGridColumn
  Public WithEvents t8d As NTSInformatica.NTSGridColumn
  Public WithEvents t9d As NTSInformatica.NTSGridColumn
  Public WithEvents t10d As NTSInformatica.NTSGridColumn
  Public WithEvents t11d As NTSInformatica.NTSGridColumn
  Public WithEvents t12d As NTSInformatica.NTSGridColumn
  Public WithEvents t13d As NTSInformatica.NTSGridColumn
  Public WithEvents t14d As NTSInformatica.NTSGridColumn
  Public WithEvents t15d As NTSInformatica.NTSGridColumn
  Public WithEvents t16d As NTSInformatica.NTSGridColumn
  Public WithEvents t17d As NTSInformatica.NTSGridColumn
  Public WithEvents t18d As NTSInformatica.NTSGridColumn
  Public WithEvents t19d As NTSInformatica.NTSGridColumn
  Public WithEvents t20d As NTSInformatica.NTSGridColumn
  Public WithEvents t21d As NTSInformatica.NTSGridColumn
  Public WithEvents t22d As NTSInformatica.NTSGridColumn
  Public WithEvents t23d As NTSInformatica.NTSGridColumn
  Public WithEvents t24d As NTSInformatica.NTSGridColumn

  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents mo_magaz As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGRS1))
    Me.grGrs1 = New NTSInformatica.NTSGrid
    Me.grvGrs1 = New NTSInformatica.NTSGridView
    Me.mo_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_origine = New NTSInformatica.NTSGridColumn
    Me.mo_datord = New NTSInformatica.NTSGridColumn
    Me.xx_tipork = New NTSInformatica.NTSGridColumn
    Me.mo_descr1 = New NTSInformatica.NTSGridColumn
    Me.xx_numord = New NTSInformatica.NTSGridColumn
    Me.mo_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_consolidato = New NTSInformatica.NTSGridColumn
    Me.xx_proposto = New NTSInformatica.NTSGridColumn
    Me.xx_disp = New NTSInformatica.NTSGridColumn
    Me.mo_datcons = New NTSInformatica.NTSGridColumn
    Me.mo_tipork = New NTSInformatica.NTSGridColumn
    Me.mo_anno = New NTSInformatica.NTSGridColumn
    Me.mo_serie = New NTSInformatica.NTSGridColumn
    Me.mo_numord = New NTSInformatica.NTSGridColumn
    Me.mo_olprogr = New NTSInformatica.NTSGridColumn
    Me.mo_origine = New NTSInformatica.NTSGridColumn
    Me.mo_codpadr = New NTSInformatica.NTSGridColumn
    Me.mo_despadr = New NTSInformatica.NTSGridColumn
    Me.mo_fasepadr = New NTSInformatica.NTSGridColumn
    Me.t1o = New NTSInformatica.NTSGridColumn
    Me.t1p = New NTSInformatica.NTSGridColumn
    Me.t1d = New NTSInformatica.NTSGridColumn
    Me.t2o = New NTSInformatica.NTSGridColumn
    Me.t2p = New NTSInformatica.NTSGridColumn
    Me.t2d = New NTSInformatica.NTSGridColumn
    Me.t3o = New NTSInformatica.NTSGridColumn
    Me.t3p = New NTSInformatica.NTSGridColumn
    Me.t3d = New NTSInformatica.NTSGridColumn
    Me.t4o = New NTSInformatica.NTSGridColumn
    Me.t4p = New NTSInformatica.NTSGridColumn
    Me.t4d = New NTSInformatica.NTSGridColumn
    Me.t5o = New NTSInformatica.NTSGridColumn
    Me.t5p = New NTSInformatica.NTSGridColumn
    Me.t5d = New NTSInformatica.NTSGridColumn
    Me.t6o = New NTSInformatica.NTSGridColumn
    Me.t6p = New NTSInformatica.NTSGridColumn
    Me.t6d = New NTSInformatica.NTSGridColumn
    Me.t7o = New NTSInformatica.NTSGridColumn
    Me.t7p = New NTSInformatica.NTSGridColumn
    Me.t7d = New NTSInformatica.NTSGridColumn
    Me.t8o = New NTSInformatica.NTSGridColumn
    Me.t8p = New NTSInformatica.NTSGridColumn
    Me.t8d = New NTSInformatica.NTSGridColumn
    Me.t9o = New NTSInformatica.NTSGridColumn
    Me.t9p = New NTSInformatica.NTSGridColumn
    Me.t9d = New NTSInformatica.NTSGridColumn
    Me.t10o = New NTSInformatica.NTSGridColumn
    Me.t10p = New NTSInformatica.NTSGridColumn
    Me.t10d = New NTSInformatica.NTSGridColumn
    Me.t11o = New NTSInformatica.NTSGridColumn
    Me.t11p = New NTSInformatica.NTSGridColumn
    Me.t11d = New NTSInformatica.NTSGridColumn
    Me.t12o = New NTSInformatica.NTSGridColumn
    Me.t12p = New NTSInformatica.NTSGridColumn
    Me.t12d = New NTSInformatica.NTSGridColumn
    Me.t13o = New NTSInformatica.NTSGridColumn
    Me.t13p = New NTSInformatica.NTSGridColumn
    Me.t13d = New NTSInformatica.NTSGridColumn
    Me.t14o = New NTSInformatica.NTSGridColumn
    Me.t14p = New NTSInformatica.NTSGridColumn
    Me.t14d = New NTSInformatica.NTSGridColumn
    Me.t15o = New NTSInformatica.NTSGridColumn
    Me.t15p = New NTSInformatica.NTSGridColumn
    Me.t15d = New NTSInformatica.NTSGridColumn
    Me.t16o = New NTSInformatica.NTSGridColumn
    Me.t16p = New NTSInformatica.NTSGridColumn
    Me.t16d = New NTSInformatica.NTSGridColumn
    Me.t17o = New NTSInformatica.NTSGridColumn
    Me.t17p = New NTSInformatica.NTSGridColumn
    Me.t17d = New NTSInformatica.NTSGridColumn
    Me.t18o = New NTSInformatica.NTSGridColumn
    Me.t18p = New NTSInformatica.NTSGridColumn
    Me.t18d = New NTSInformatica.NTSGridColumn
    Me.t19o = New NTSInformatica.NTSGridColumn
    Me.t19p = New NTSInformatica.NTSGridColumn
    Me.t19d = New NTSInformatica.NTSGridColumn
    Me.t20o = New NTSInformatica.NTSGridColumn
    Me.t20p = New NTSInformatica.NTSGridColumn
    Me.t20d = New NTSInformatica.NTSGridColumn
    Me.t21o = New NTSInformatica.NTSGridColumn
    Me.t21p = New NTSInformatica.NTSGridColumn
    Me.t21d = New NTSInformatica.NTSGridColumn
    Me.t22o = New NTSInformatica.NTSGridColumn
    Me.t22p = New NTSInformatica.NTSGridColumn
    Me.t22d = New NTSInformatica.NTSGridColumn
    Me.t23o = New NTSInformatica.NTSGridColumn
    Me.t23p = New NTSInformatica.NTSGridColumn
    Me.t23d = New NTSInformatica.NTSGridColumn
    Me.t24o = New NTSInformatica.NTSGridColumn
    Me.t24p = New NTSInformatica.NTSGridColumn
    Me.t24d = New NTSInformatica.NTSGridColumn
    Me.mo_tctaglia = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.edUnmis = New NTSInformatica.NTSTextBoxStr
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbUnmis = New NTSInformatica.NTSLabel
    Me.lbAdatcons = New NTSInformatica.NTSLabel
    Me.lbDadatcons = New NTSInformatica.NTSLabel
    Me.lbXx_Magaz = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbApriGestione = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiornaOrdine = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneMrp = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrs1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrs1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grGrs1
    '
    Me.grGrs1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grGrs1.EmbeddedNavigator.Name = ""
    Me.grGrs1.Location = New System.Drawing.Point(0, 0)
    Me.grGrs1.MainView = Me.grvGrs1
    Me.grGrs1.Name = "grGrs1"
    Me.grGrs1.Size = New System.Drawing.Size(660, 340)
    Me.grGrs1.TabIndex = 5
    Me.grGrs1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrs1})
    '
    'grvGrs1
    '
    Me.grvGrs1.ActiveFilterEnabled = False
    '
    'mo_magaz
    '
    Me.mo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_magaz.Caption = "Magazzino"
    Me.mo_magaz.Enabled = True
    Me.mo_magaz.FieldName = "mo_magaz"
    Me.mo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_magaz.Name = "mo_magaz"
    Me.mo_magaz.NTSRepositoryComboBox = Nothing
    Me.mo_magaz.NTSRepositoryItemCheck = Nothing
    Me.mo_magaz.NTSRepositoryItemMemo = Nothing
    Me.mo_magaz.NTSRepositoryItemText = Nothing
    Me.mo_magaz.Visible = True
    Me.mo_magaz.VisibleIndex = 5
    Me.grvGrs1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_origine, Me.mo_datord, Me.xx_tipork, Me.mo_descr1, Me.xx_numord, Me.mo_magaz, Me.mo_commeca, Me.xx_consolidato, Me.xx_proposto, Me.xx_disp, Me.mo_datcons, Me.mo_tipork, Me.mo_anno, Me.mo_serie, Me.mo_numord, Me.mo_olprogr, Me.mo_origine, Me.mo_codpadr, Me.mo_despadr, Me.mo_fasepadr, Me.t1o, Me.t1p, Me.t1d, Me.t2o, Me.t2p, Me.t2d, Me.t3o, Me.t3p, Me.t3d, Me.t4o, Me.t4p, Me.t4d, Me.t5o, Me.t5p, Me.t5d, Me.t6o, Me.t6p, Me.t6d, Me.t7o, Me.t7p, Me.t7d, Me.t8o, Me.t8p, Me.t8d, Me.t9o, Me.t9p, Me.t9d, Me.t10o, Me.t10p, Me.t10d, Me.t11o, Me.t11p, Me.t11d, Me.t12o, Me.t12p, Me.t12d, Me.t13o, Me.t13p, Me.t13d, Me.t14o, Me.t14p, Me.t14d, Me.t15o, Me.t15p, Me.t15d, Me.t16o, Me.t16p, Me.t16d, Me.t17o, Me.t17p, Me.t17d, Me.t18o, Me.t18p, Me.t18d, Me.t19o, Me.t19p, Me.t19d, Me.t20o, Me.t20p, Me.t20d, Me.t21o, Me.t21p, Me.t21d, Me.t22o, Me.t22p, Me.t22d, Me.t23o, Me.t23p, Me.t23d, Me.t24o, Me.t24p, Me.t24d, Me.mo_tctaglia})
    Me.grvGrs1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrs1.Enabled = True
    Me.grvGrs1.GridControl = Me.grGrs1
    Me.grvGrs1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrs1.MinRowHeight = 14
    Me.grvGrs1.Name = "grvGrs1"
    Me.grvGrs1.NTSAllowDelete = True
    Me.grvGrs1.NTSAllowInsert = True
    Me.grvGrs1.NTSAllowUpdate = True
    Me.grvGrs1.NTSMenuContext = Nothing
    Me.grvGrs1.OptionsCustomization.AllowRowSizing = True
    Me.grvGrs1.OptionsFilter.AllowFilterEditor = False
    Me.grvGrs1.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrs1.OptionsNavigation.UseTabKey = False
    Me.grvGrs1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrs1.OptionsView.ColumnAutoWidth = False
    Me.grvGrs1.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrs1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrs1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrs1.OptionsView.ShowGroupPanel = False
    Me.grvGrs1.RowHeight = 16
    '
    'xx_origine
    '
    Me.xx_origine.AppearanceCell.Options.UseBackColor = True
    Me.xx_origine.AppearanceCell.Options.UseTextOptions = True
    Me.xx_origine.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_origine.Caption = "Origine"
    Me.xx_origine.Enabled = True
    Me.xx_origine.FieldName = "xx_origine"
    Me.xx_origine.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_origine.Name = "xx_origine"
    Me.xx_origine.NTSRepositoryComboBox = Nothing
    Me.xx_origine.NTSRepositoryItemCheck = Nothing
    Me.xx_origine.NTSRepositoryItemMemo = Nothing
    Me.xx_origine.NTSRepositoryItemText = Nothing
    Me.xx_origine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_origine.OptionsFilter.AllowFilter = False
    Me.xx_origine.Visible = True
    Me.xx_origine.VisibleIndex = 0
    '
    'mo_datord
    '
    Me.mo_datord.AppearanceCell.Options.UseBackColor = True
    Me.mo_datord.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datord.Caption = "Data Ord."
    Me.mo_datord.Enabled = True
    Me.mo_datord.FieldName = "mo_datord"
    Me.mo_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datord.Name = "mo_datord"
    Me.mo_datord.NTSRepositoryComboBox = Nothing
    Me.mo_datord.NTSRepositoryItemCheck = Nothing
    Me.mo_datord.NTSRepositoryItemMemo = Nothing
    Me.mo_datord.NTSRepositoryItemText = Nothing
    Me.mo_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datord.OptionsFilter.AllowFilter = False
    Me.mo_datord.Visible = True
    Me.mo_datord.VisibleIndex = 1
    '
    'xx_tipork
    '
    Me.xx_tipork.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipork.Caption = "Tipo"
    Me.xx_tipork.Enabled = True
    Me.xx_tipork.FieldName = "xx_tipork"
    Me.xx_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipork.Name = "xx_tipork"
    Me.xx_tipork.NTSRepositoryComboBox = Nothing
    Me.xx_tipork.NTSRepositoryItemCheck = Nothing
    Me.xx_tipork.NTSRepositoryItemMemo = Nothing
    Me.xx_tipork.NTSRepositoryItemText = Nothing
    Me.xx_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipork.OptionsFilter.AllowFilter = False
    Me.xx_tipork.Visible = True
    Me.xx_tipork.VisibleIndex = 2
    '
    'mo_descr1
    '
    Me.mo_descr1.AppearanceCell.Options.UseBackColor = True
    Me.mo_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_descr1.Caption = "Cliente/Fornitore"
    Me.mo_descr1.Enabled = True
    Me.mo_descr1.FieldName = "mo_descr1"
    Me.mo_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_descr1.Name = "mo_descr1"
    Me.mo_descr1.NTSRepositoryComboBox = Nothing
    Me.mo_descr1.NTSRepositoryItemCheck = Nothing
    Me.mo_descr1.NTSRepositoryItemMemo = Nothing
    Me.mo_descr1.NTSRepositoryItemText = Nothing
    Me.mo_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_descr1.OptionsFilter.AllowFilter = False
    Me.mo_descr1.Visible = True
    Me.mo_descr1.VisibleIndex = 3
    '
    'xx_numord
    '
    Me.xx_numord.AppearanceCell.Options.UseBackColor = True
    Me.xx_numord.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numord.Caption = "N°ordine"
    Me.xx_numord.Enabled = True
    Me.xx_numord.FieldName = "xx_numord"
    Me.xx_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numord.Name = "xx_numord"
    Me.xx_numord.NTSRepositoryComboBox = Nothing
    Me.xx_numord.NTSRepositoryItemCheck = Nothing
    Me.xx_numord.NTSRepositoryItemMemo = Nothing
    Me.xx_numord.NTSRepositoryItemText = Nothing
    Me.xx_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numord.OptionsFilter.AllowFilter = False
    Me.xx_numord.Visible = True
    Me.xx_numord.VisibleIndex = 4
    '
    'mo_commeca
    '
    Me.mo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_commeca.Caption = "Commessa"
    Me.mo_commeca.Enabled = True
    Me.mo_commeca.FieldName = "mo_commeca"
    Me.mo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_commeca.Name = "mo_commeca"
    Me.mo_commeca.NTSRepositoryComboBox = Nothing
    Me.mo_commeca.NTSRepositoryItemCheck = Nothing
    Me.mo_commeca.NTSRepositoryItemMemo = Nothing
    Me.mo_commeca.NTSRepositoryItemText = Nothing
    Me.mo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_commeca.OptionsFilter.AllowFilter = False
    Me.mo_commeca.Visible = True
    Me.mo_commeca.VisibleIndex = 6
    '
    'xx_consolidato
    '
    Me.xx_consolidato.AppearanceCell.Options.UseBackColor = True
    Me.xx_consolidato.AppearanceCell.Options.UseTextOptions = True
    Me.xx_consolidato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_consolidato.Caption = "Consolidato"
    Me.xx_consolidato.Enabled = True
    Me.xx_consolidato.FieldName = "xx_consolidato"
    Me.xx_consolidato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_consolidato.Name = "xx_consolidato"
    Me.xx_consolidato.NTSRepositoryComboBox = Nothing
    Me.xx_consolidato.NTSRepositoryItemCheck = Nothing
    Me.xx_consolidato.NTSRepositoryItemMemo = Nothing
    Me.xx_consolidato.NTSRepositoryItemText = Nothing
    Me.xx_consolidato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_consolidato.OptionsFilter.AllowFilter = False
    Me.xx_consolidato.Visible = True
    Me.xx_consolidato.VisibleIndex = 7
    '
    'xx_proposto
    '
    Me.xx_proposto.AppearanceCell.Options.UseBackColor = True
    Me.xx_proposto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_proposto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_proposto.Caption = "Proposto"
    Me.xx_proposto.Enabled = True
    Me.xx_proposto.FieldName = "xx_proposto"
    Me.xx_proposto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_proposto.Name = "xx_proposto"
    Me.xx_proposto.NTSRepositoryComboBox = Nothing
    Me.xx_proposto.NTSRepositoryItemCheck = Nothing
    Me.xx_proposto.NTSRepositoryItemMemo = Nothing
    Me.xx_proposto.NTSRepositoryItemText = Nothing
    Me.xx_proposto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_proposto.OptionsFilter.AllowFilter = False
    Me.xx_proposto.Visible = True
    Me.xx_proposto.VisibleIndex = 8
    '
    'xx_disp
    '
    Me.xx_disp.AppearanceCell.Options.UseBackColor = True
    Me.xx_disp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disp.Caption = "Disponibilità"
    Me.xx_disp.Enabled = True
    Me.xx_disp.FieldName = "xx_disp"
    Me.xx_disp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disp.Name = "xx_disp"
    Me.xx_disp.NTSRepositoryComboBox = Nothing
    Me.xx_disp.NTSRepositoryItemCheck = Nothing
    Me.xx_disp.NTSRepositoryItemMemo = Nothing
    Me.xx_disp.NTSRepositoryItemText = Nothing
    Me.xx_disp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disp.OptionsFilter.AllowFilter = False
    Me.xx_disp.Visible = True
    Me.xx_disp.VisibleIndex = 9
    '
    'mo_datcons
    '
    Me.mo_datcons.AppearanceCell.Options.UseBackColor = True
    Me.mo_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datcons.Caption = "Consegna"
    Me.mo_datcons.Enabled = True
    Me.mo_datcons.FieldName = "mo_datcons"
    Me.mo_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datcons.Name = "mo_datcons"
    Me.mo_datcons.NTSRepositoryComboBox = Nothing
    Me.mo_datcons.NTSRepositoryItemCheck = Nothing
    Me.mo_datcons.NTSRepositoryItemMemo = Nothing
    Me.mo_datcons.NTSRepositoryItemText = Nothing
    Me.mo_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datcons.OptionsFilter.AllowFilter = False
    Me.mo_datcons.Visible = True
    Me.mo_datcons.VisibleIndex = 10
    '
    'mo_tipork
    '
    Me.mo_tipork.AppearanceCell.Options.UseBackColor = True
    Me.mo_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.mo_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_tipork.Caption = "MO_TIPORK"
    Me.mo_tipork.Enabled = True
    Me.mo_tipork.FieldName = "mo_tipork"
    Me.mo_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_tipork.Name = "mo_tipork"
    Me.mo_tipork.NTSRepositoryComboBox = Nothing
    Me.mo_tipork.NTSRepositoryItemCheck = Nothing
    Me.mo_tipork.NTSRepositoryItemMemo = Nothing
    Me.mo_tipork.NTSRepositoryItemText = Nothing
    Me.mo_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_tipork.OptionsFilter.AllowFilter = False
    '
    'mo_anno
    '
    Me.mo_anno.AppearanceCell.Options.UseBackColor = True
    Me.mo_anno.AppearanceCell.Options.UseTextOptions = True
    Me.mo_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_anno.Caption = "MO_ANNO"
    Me.mo_anno.Enabled = True
    Me.mo_anno.FieldName = "mo_anno"
    Me.mo_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_anno.Name = "mo_anno"
    Me.mo_anno.NTSRepositoryComboBox = Nothing
    Me.mo_anno.NTSRepositoryItemCheck = Nothing
    Me.mo_anno.NTSRepositoryItemMemo = Nothing
    Me.mo_anno.NTSRepositoryItemText = Nothing
    Me.mo_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_anno.OptionsFilter.AllowFilter = False
    '
    'mo_serie
    '
    Me.mo_serie.AppearanceCell.Options.UseBackColor = True
    Me.mo_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mo_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_serie.Caption = "MO_SERIE"
    Me.mo_serie.Enabled = True
    Me.mo_serie.FieldName = "mo_serie"
    Me.mo_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_serie.Name = "mo_serie"
    Me.mo_serie.NTSRepositoryComboBox = Nothing
    Me.mo_serie.NTSRepositoryItemCheck = Nothing
    Me.mo_serie.NTSRepositoryItemMemo = Nothing
    Me.mo_serie.NTSRepositoryItemText = Nothing
    Me.mo_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_serie.OptionsFilter.AllowFilter = False
    '
    'mo_numord
    '
    Me.mo_numord.AppearanceCell.Options.UseBackColor = True
    Me.mo_numord.AppearanceCell.Options.UseTextOptions = True
    Me.mo_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_numord.Caption = "MO_NUMORD"
    Me.mo_numord.Enabled = True
    Me.mo_numord.FieldName = "mo_numord"
    Me.mo_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_numord.Name = "mo_numord"
    Me.mo_numord.NTSRepositoryComboBox = Nothing
    Me.mo_numord.NTSRepositoryItemCheck = Nothing
    Me.mo_numord.NTSRepositoryItemMemo = Nothing
    Me.mo_numord.NTSRepositoryItemText = Nothing
    Me.mo_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_numord.OptionsFilter.AllowFilter = False
    '
    'mo_olprogr
    '
    Me.mo_olprogr.AppearanceCell.Options.UseBackColor = True
    Me.mo_olprogr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_olprogr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_olprogr.Caption = "MO_OLPROGR"
    Me.mo_olprogr.Enabled = True
    Me.mo_olprogr.FieldName = "mo_olprogr"
    Me.mo_olprogr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_olprogr.Name = "mo_olprogr"
    Me.mo_olprogr.NTSRepositoryComboBox = Nothing
    Me.mo_olprogr.NTSRepositoryItemCheck = Nothing
    Me.mo_olprogr.NTSRepositoryItemMemo = Nothing
    Me.mo_olprogr.NTSRepositoryItemText = Nothing
    Me.mo_olprogr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_olprogr.OptionsFilter.AllowFilter = False
    '
    'mo_origine
    '
    Me.mo_origine.AppearanceCell.Options.UseBackColor = True
    Me.mo_origine.AppearanceCell.Options.UseTextOptions = True
    Me.mo_origine.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_origine.Caption = "MO_ORIGINE"
    Me.mo_origine.Enabled = True
    Me.mo_origine.FieldName = "mo_origine"
    Me.mo_origine.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_origine.Name = "mo_origine"
    Me.mo_origine.NTSRepositoryComboBox = Nothing
    Me.mo_origine.NTSRepositoryItemCheck = Nothing
    Me.mo_origine.NTSRepositoryItemMemo = Nothing
    Me.mo_origine.NTSRepositoryItemText = Nothing
    Me.mo_origine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_origine.OptionsFilter.AllowFilter = False
    '
    'mo_codpadr
    '
    Me.mo_codpadr.AppearanceCell.Options.UseBackColor = True
    Me.mo_codpadr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codpadr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codpadr.Caption = "Cod. art. padre"
    Me.mo_codpadr.Enabled = True
    Me.mo_codpadr.FieldName = "mo_codpadr"
    Me.mo_codpadr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codpadr.Name = "mo_codpadr"
    Me.mo_codpadr.NTSRepositoryComboBox = Nothing
    Me.mo_codpadr.NTSRepositoryItemCheck = Nothing
    Me.mo_codpadr.NTSRepositoryItemMemo = Nothing
    Me.mo_codpadr.NTSRepositoryItemText = Nothing
    Me.mo_codpadr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codpadr.OptionsFilter.AllowFilter = False
    Me.mo_codpadr.Visible = True
    Me.mo_codpadr.VisibleIndex = 11
    '
    'mo_despadr
    '
    Me.mo_despadr.AppearanceCell.Options.UseBackColor = True
    Me.mo_despadr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_despadr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_despadr.Caption = "Descr. art. padre"
    Me.mo_despadr.Enabled = True
    Me.mo_despadr.FieldName = "mo_despadr"
    Me.mo_despadr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_despadr.Name = "mo_despadr"
    Me.mo_despadr.NTSRepositoryComboBox = Nothing
    Me.mo_despadr.NTSRepositoryItemCheck = Nothing
    Me.mo_despadr.NTSRepositoryItemMemo = Nothing
    Me.mo_despadr.NTSRepositoryItemText = Nothing
    Me.mo_despadr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_despadr.OptionsFilter.AllowFilter = False
    Me.mo_despadr.Visible = True
    Me.mo_despadr.VisibleIndex = 12
    '
    'mo_fasepadr
    '
    Me.mo_fasepadr.AppearanceCell.Options.UseBackColor = True
    Me.mo_fasepadr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_fasepadr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_fasepadr.Caption = "Fase art. padre"
    Me.mo_fasepadr.Enabled = True
    Me.mo_fasepadr.FieldName = "mo_fasepadr"
    Me.mo_fasepadr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_fasepadr.Name = "mo_fasepadr"
    Me.mo_fasepadr.NTSRepositoryComboBox = Nothing
    Me.mo_fasepadr.NTSRepositoryItemCheck = Nothing
    Me.mo_fasepadr.NTSRepositoryItemMemo = Nothing
    Me.mo_fasepadr.NTSRepositoryItemText = Nothing
    Me.mo_fasepadr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_fasepadr.OptionsFilter.AllowFilter = False
    Me.mo_fasepadr.Visible = True
    Me.mo_fasepadr.VisibleIndex = 13
    '
    't1o
    '
    Me.t1o.AppearanceCell.Options.UseBackColor = True
    Me.t1o.AppearanceCell.Options.UseTextOptions = True
    Me.t1o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t1o.Caption = "T1o"
    Me.t1o.Enabled = True
    Me.t1o.FieldName = "t1o"
    Me.t1o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t1o.Name = "t1o"
    Me.t1o.NTSRepositoryComboBox = Nothing
    Me.t1o.NTSRepositoryItemCheck = Nothing
    Me.t1o.NTSRepositoryItemMemo = Nothing
    Me.t1o.NTSRepositoryItemText = Nothing
    Me.t1o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t1o.OptionsFilter.AllowFilter = False
    Me.t1o.Visible = True
    Me.t1o.VisibleIndex = 14
    '
    't1p
    '
    Me.t1p.AppearanceCell.Options.UseBackColor = True
    Me.t1p.AppearanceCell.Options.UseTextOptions = True
    Me.t1p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t1p.Caption = "T1p"
    Me.t1p.Enabled = True
    Me.t1p.FieldName = "t1p"
    Me.t1p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t1p.Name = "t1p"
    Me.t1p.NTSRepositoryComboBox = Nothing
    Me.t1p.NTSRepositoryItemCheck = Nothing
    Me.t1p.NTSRepositoryItemMemo = Nothing
    Me.t1p.NTSRepositoryItemText = Nothing
    Me.t1p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t1p.OptionsFilter.AllowFilter = False
    Me.t1p.Visible = True
    Me.t1p.VisibleIndex = 15
    '
    't1d
    '
    Me.t1d.AppearanceCell.Options.UseBackColor = True
    Me.t1d.AppearanceCell.Options.UseTextOptions = True
    Me.t1d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t1d.Caption = "T1d"
    Me.t1d.Enabled = True
    Me.t1d.FieldName = "t1d"
    Me.t1d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t1d.Name = "t1d"
    Me.t1d.NTSRepositoryComboBox = Nothing
    Me.t1d.NTSRepositoryItemCheck = Nothing
    Me.t1d.NTSRepositoryItemMemo = Nothing
    Me.t1d.NTSRepositoryItemText = Nothing
    Me.t1d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t1d.OptionsFilter.AllowFilter = False
    Me.t1d.Visible = True
    Me.t1d.VisibleIndex = 16
    '
    't2o
    '
    Me.t2o.AppearanceCell.Options.UseBackColor = True
    Me.t2o.AppearanceCell.Options.UseTextOptions = True
    Me.t2o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t2o.Caption = "T2o"
    Me.t2o.Enabled = True
    Me.t2o.FieldName = "t2o"
    Me.t2o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t2o.Name = "t2o"
    Me.t2o.NTSRepositoryComboBox = Nothing
    Me.t2o.NTSRepositoryItemCheck = Nothing
    Me.t2o.NTSRepositoryItemMemo = Nothing
    Me.t2o.NTSRepositoryItemText = Nothing
    Me.t2o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t2o.OptionsFilter.AllowFilter = False
    Me.t2o.Visible = True
    Me.t2o.VisibleIndex = 17
    '
    't2p
    '
    Me.t2p.AppearanceCell.Options.UseBackColor = True
    Me.t2p.AppearanceCell.Options.UseTextOptions = True
    Me.t2p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t2p.Caption = "T2p"
    Me.t2p.Enabled = True
    Me.t2p.FieldName = "t2p"
    Me.t2p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t2p.Name = "t2p"
    Me.t2p.NTSRepositoryComboBox = Nothing
    Me.t2p.NTSRepositoryItemCheck = Nothing
    Me.t2p.NTSRepositoryItemMemo = Nothing
    Me.t2p.NTSRepositoryItemText = Nothing
    Me.t2p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t2p.OptionsFilter.AllowFilter = False
    Me.t2p.Visible = True
    Me.t2p.VisibleIndex = 18
    '
    't2d
    '
    Me.t2d.AppearanceCell.Options.UseBackColor = True
    Me.t2d.AppearanceCell.Options.UseTextOptions = True
    Me.t2d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t2d.Caption = "T2d"
    Me.t2d.Enabled = True
    Me.t2d.FieldName = "t2d"
    Me.t2d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t2d.Name = "t2d"
    Me.t2d.NTSRepositoryComboBox = Nothing
    Me.t2d.NTSRepositoryItemCheck = Nothing
    Me.t2d.NTSRepositoryItemMemo = Nothing
    Me.t2d.NTSRepositoryItemText = Nothing
    Me.t2d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t2d.OptionsFilter.AllowFilter = False
    Me.t2d.Visible = True
    Me.t2d.VisibleIndex = 19
    '
    't3o
    '
    Me.t3o.AppearanceCell.Options.UseBackColor = True
    Me.t3o.AppearanceCell.Options.UseTextOptions = True
    Me.t3o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t3o.Caption = "T3o"
    Me.t3o.Enabled = True
    Me.t3o.FieldName = "t3o"
    Me.t3o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t3o.Name = "t3o"
    Me.t3o.NTSRepositoryComboBox = Nothing
    Me.t3o.NTSRepositoryItemCheck = Nothing
    Me.t3o.NTSRepositoryItemMemo = Nothing
    Me.t3o.NTSRepositoryItemText = Nothing
    Me.t3o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t3o.OptionsFilter.AllowFilter = False
    Me.t3o.Visible = True
    Me.t3o.VisibleIndex = 20
    '
    't3p
    '
    Me.t3p.AppearanceCell.Options.UseBackColor = True
    Me.t3p.AppearanceCell.Options.UseTextOptions = True
    Me.t3p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t3p.Caption = "T3p"
    Me.t3p.Enabled = True
    Me.t3p.FieldName = "t3p"
    Me.t3p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t3p.Name = "t3p"
    Me.t3p.NTSRepositoryComboBox = Nothing
    Me.t3p.NTSRepositoryItemCheck = Nothing
    Me.t3p.NTSRepositoryItemMemo = Nothing
    Me.t3p.NTSRepositoryItemText = Nothing
    Me.t3p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t3p.OptionsFilter.AllowFilter = False
    Me.t3p.Visible = True
    Me.t3p.VisibleIndex = 21
    '
    't3d
    '
    Me.t3d.AppearanceCell.Options.UseBackColor = True
    Me.t3d.AppearanceCell.Options.UseTextOptions = True
    Me.t3d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t3d.Caption = "T3d"
    Me.t3d.Enabled = True
    Me.t3d.FieldName = "t3d"
    Me.t3d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t3d.Name = "t3d"
    Me.t3d.NTSRepositoryComboBox = Nothing
    Me.t3d.NTSRepositoryItemCheck = Nothing
    Me.t3d.NTSRepositoryItemMemo = Nothing
    Me.t3d.NTSRepositoryItemText = Nothing
    Me.t3d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t3d.OptionsFilter.AllowFilter = False
    Me.t3d.Visible = True
    Me.t3d.VisibleIndex = 22
    '
    't4o
    '
    Me.t4o.AppearanceCell.Options.UseBackColor = True
    Me.t4o.AppearanceCell.Options.UseTextOptions = True
    Me.t4o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t4o.Caption = "T4o"
    Me.t4o.Enabled = True
    Me.t4o.FieldName = "t4o"
    Me.t4o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t4o.Name = "t4o"
    Me.t4o.NTSRepositoryComboBox = Nothing
    Me.t4o.NTSRepositoryItemCheck = Nothing
    Me.t4o.NTSRepositoryItemMemo = Nothing
    Me.t4o.NTSRepositoryItemText = Nothing
    Me.t4o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t4o.OptionsFilter.AllowFilter = False
    Me.t4o.Visible = True
    Me.t4o.VisibleIndex = 23
    '
    't4p
    '
    Me.t4p.AppearanceCell.Options.UseBackColor = True
    Me.t4p.AppearanceCell.Options.UseTextOptions = True
    Me.t4p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t4p.Caption = "T4p"
    Me.t4p.Enabled = True
    Me.t4p.FieldName = "t4p"
    Me.t4p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t4p.Name = "t4p"
    Me.t4p.NTSRepositoryComboBox = Nothing
    Me.t4p.NTSRepositoryItemCheck = Nothing
    Me.t4p.NTSRepositoryItemMemo = Nothing
    Me.t4p.NTSRepositoryItemText = Nothing
    Me.t4p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t4p.OptionsFilter.AllowFilter = False
    Me.t4p.Visible = True
    Me.t4p.VisibleIndex = 24
    '
    't4d
    '
    Me.t4d.AppearanceCell.Options.UseBackColor = True
    Me.t4d.AppearanceCell.Options.UseTextOptions = True
    Me.t4d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t4d.Caption = "T4d"
    Me.t4d.Enabled = True
    Me.t4d.FieldName = "t4d"
    Me.t4d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t4d.Name = "t4d"
    Me.t4d.NTSRepositoryComboBox = Nothing
    Me.t4d.NTSRepositoryItemCheck = Nothing
    Me.t4d.NTSRepositoryItemMemo = Nothing
    Me.t4d.NTSRepositoryItemText = Nothing
    Me.t4d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t4d.OptionsFilter.AllowFilter = False
    Me.t4d.Visible = True
    Me.t4d.VisibleIndex = 25
    '
    't5o
    '
    Me.t5o.AppearanceCell.Options.UseBackColor = True
    Me.t5o.AppearanceCell.Options.UseTextOptions = True
    Me.t5o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t5o.Caption = "T5o"
    Me.t5o.Enabled = True
    Me.t5o.FieldName = "t5o"
    Me.t5o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t5o.Name = "t5o"
    Me.t5o.NTSRepositoryComboBox = Nothing
    Me.t5o.NTSRepositoryItemCheck = Nothing
    Me.t5o.NTSRepositoryItemMemo = Nothing
    Me.t5o.NTSRepositoryItemText = Nothing
    Me.t5o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t5o.OptionsFilter.AllowFilter = False
    Me.t5o.Visible = True
    Me.t5o.VisibleIndex = 26
    '
    't5p
    '
    Me.t5p.AppearanceCell.Options.UseBackColor = True
    Me.t5p.AppearanceCell.Options.UseTextOptions = True
    Me.t5p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t5p.Caption = "T5p"
    Me.t5p.Enabled = True
    Me.t5p.FieldName = "t5p"
    Me.t5p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t5p.Name = "t5p"
    Me.t5p.NTSRepositoryComboBox = Nothing
    Me.t5p.NTSRepositoryItemCheck = Nothing
    Me.t5p.NTSRepositoryItemMemo = Nothing
    Me.t5p.NTSRepositoryItemText = Nothing
    Me.t5p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t5p.OptionsFilter.AllowFilter = False
    Me.t5p.Visible = True
    Me.t5p.VisibleIndex = 27
    '
    't5d
    '
    Me.t5d.AppearanceCell.Options.UseBackColor = True
    Me.t5d.AppearanceCell.Options.UseTextOptions = True
    Me.t5d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t5d.Caption = "T5d"
    Me.t5d.Enabled = True
    Me.t5d.FieldName = "t5d"
    Me.t5d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t5d.Name = "t5d"
    Me.t5d.NTSRepositoryComboBox = Nothing
    Me.t5d.NTSRepositoryItemCheck = Nothing
    Me.t5d.NTSRepositoryItemMemo = Nothing
    Me.t5d.NTSRepositoryItemText = Nothing
    Me.t5d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t5d.OptionsFilter.AllowFilter = False
    Me.t5d.Visible = True
    Me.t5d.VisibleIndex = 28
    '
    't6o
    '
    Me.t6o.AppearanceCell.Options.UseBackColor = True
    Me.t6o.AppearanceCell.Options.UseTextOptions = True
    Me.t6o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t6o.Caption = "T6o"
    Me.t6o.Enabled = True
    Me.t6o.FieldName = "t6o"
    Me.t6o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t6o.Name = "t6o"
    Me.t6o.NTSRepositoryComboBox = Nothing
    Me.t6o.NTSRepositoryItemCheck = Nothing
    Me.t6o.NTSRepositoryItemMemo = Nothing
    Me.t6o.NTSRepositoryItemText = Nothing
    Me.t6o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t6o.OptionsFilter.AllowFilter = False
    Me.t6o.Visible = True
    Me.t6o.VisibleIndex = 29
    '
    't6p
    '
    Me.t6p.AppearanceCell.Options.UseBackColor = True
    Me.t6p.AppearanceCell.Options.UseTextOptions = True
    Me.t6p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t6p.Caption = "T6p"
    Me.t6p.Enabled = True
    Me.t6p.FieldName = "t6p"
    Me.t6p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t6p.Name = "t6p"
    Me.t6p.NTSRepositoryComboBox = Nothing
    Me.t6p.NTSRepositoryItemCheck = Nothing
    Me.t6p.NTSRepositoryItemMemo = Nothing
    Me.t6p.NTSRepositoryItemText = Nothing
    Me.t6p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t6p.OptionsFilter.AllowFilter = False
    Me.t6p.Visible = True
    Me.t6p.VisibleIndex = 30
    '
    't6d
    '
    Me.t6d.AppearanceCell.Options.UseBackColor = True
    Me.t6d.AppearanceCell.Options.UseTextOptions = True
    Me.t6d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t6d.Caption = "T6d"
    Me.t6d.Enabled = True
    Me.t6d.FieldName = "t6d"
    Me.t6d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t6d.Name = "t6d"
    Me.t6d.NTSRepositoryComboBox = Nothing
    Me.t6d.NTSRepositoryItemCheck = Nothing
    Me.t6d.NTSRepositoryItemMemo = Nothing
    Me.t6d.NTSRepositoryItemText = Nothing
    Me.t6d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t6d.OptionsFilter.AllowFilter = False
    Me.t6d.Visible = True
    Me.t6d.VisibleIndex = 31
    '
    't7o
    '
    Me.t7o.AppearanceCell.Options.UseBackColor = True
    Me.t7o.AppearanceCell.Options.UseTextOptions = True
    Me.t7o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t7o.Caption = "T7o"
    Me.t7o.Enabled = True
    Me.t7o.FieldName = "t7o"
    Me.t7o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t7o.Name = "t7o"
    Me.t7o.NTSRepositoryComboBox = Nothing
    Me.t7o.NTSRepositoryItemCheck = Nothing
    Me.t7o.NTSRepositoryItemMemo = Nothing
    Me.t7o.NTSRepositoryItemText = Nothing
    Me.t7o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t7o.OptionsFilter.AllowFilter = False
    Me.t7o.Visible = True
    Me.t7o.VisibleIndex = 32
    '
    't7p
    '
    Me.t7p.AppearanceCell.Options.UseBackColor = True
    Me.t7p.AppearanceCell.Options.UseTextOptions = True
    Me.t7p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t7p.Caption = "T7p"
    Me.t7p.Enabled = True
    Me.t7p.FieldName = "t7p"
    Me.t7p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t7p.Name = "t7p"
    Me.t7p.NTSRepositoryComboBox = Nothing
    Me.t7p.NTSRepositoryItemCheck = Nothing
    Me.t7p.NTSRepositoryItemMemo = Nothing
    Me.t7p.NTSRepositoryItemText = Nothing
    Me.t7p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t7p.OptionsFilter.AllowFilter = False
    Me.t7p.Visible = True
    Me.t7p.VisibleIndex = 33
    '
    't7d
    '
    Me.t7d.AppearanceCell.Options.UseBackColor = True
    Me.t7d.AppearanceCell.Options.UseTextOptions = True
    Me.t7d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t7d.Caption = "T7d"
    Me.t7d.Enabled = True
    Me.t7d.FieldName = "t7d"
    Me.t7d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t7d.Name = "t7d"
    Me.t7d.NTSRepositoryComboBox = Nothing
    Me.t7d.NTSRepositoryItemCheck = Nothing
    Me.t7d.NTSRepositoryItemMemo = Nothing
    Me.t7d.NTSRepositoryItemText = Nothing
    Me.t7d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t7d.OptionsFilter.AllowFilter = False
    Me.t7d.Visible = True
    Me.t7d.VisibleIndex = 34
    '
    't8o
    '
    Me.t8o.AppearanceCell.Options.UseBackColor = True
    Me.t8o.AppearanceCell.Options.UseTextOptions = True
    Me.t8o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t8o.Caption = "T8o"
    Me.t8o.Enabled = True
    Me.t8o.FieldName = "t8o"
    Me.t8o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t8o.Name = "t8o"
    Me.t8o.NTSRepositoryComboBox = Nothing
    Me.t8o.NTSRepositoryItemCheck = Nothing
    Me.t8o.NTSRepositoryItemMemo = Nothing
    Me.t8o.NTSRepositoryItemText = Nothing
    Me.t8o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t8o.OptionsFilter.AllowFilter = False
    Me.t8o.Visible = True
    Me.t8o.VisibleIndex = 35
    '
    't8p
    '
    Me.t8p.AppearanceCell.Options.UseBackColor = True
    Me.t8p.AppearanceCell.Options.UseTextOptions = True
    Me.t8p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t8p.Caption = "T8p"
    Me.t8p.Enabled = True
    Me.t8p.FieldName = "t8p"
    Me.t8p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t8p.Name = "t8p"
    Me.t8p.NTSRepositoryComboBox = Nothing
    Me.t8p.NTSRepositoryItemCheck = Nothing
    Me.t8p.NTSRepositoryItemMemo = Nothing
    Me.t8p.NTSRepositoryItemText = Nothing
    Me.t8p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t8p.OptionsFilter.AllowFilter = False
    Me.t8p.Visible = True
    Me.t8p.VisibleIndex = 36
    '
    't8d
    '
    Me.t8d.AppearanceCell.Options.UseBackColor = True
    Me.t8d.AppearanceCell.Options.UseTextOptions = True
    Me.t8d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t8d.Caption = "T8d"
    Me.t8d.Enabled = True
    Me.t8d.FieldName = "t8d"
    Me.t8d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t8d.Name = "t8d"
    Me.t8d.NTSRepositoryComboBox = Nothing
    Me.t8d.NTSRepositoryItemCheck = Nothing
    Me.t8d.NTSRepositoryItemMemo = Nothing
    Me.t8d.NTSRepositoryItemText = Nothing
    Me.t8d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t8d.OptionsFilter.AllowFilter = False
    Me.t8d.Visible = True
    Me.t8d.VisibleIndex = 37
    '
    't9o
    '
    Me.t9o.AppearanceCell.Options.UseBackColor = True
    Me.t9o.AppearanceCell.Options.UseTextOptions = True
    Me.t9o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t9o.Caption = "T9o"
    Me.t9o.Enabled = True
    Me.t9o.FieldName = "t9o"
    Me.t9o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t9o.Name = "t9o"
    Me.t9o.NTSRepositoryComboBox = Nothing
    Me.t9o.NTSRepositoryItemCheck = Nothing
    Me.t9o.NTSRepositoryItemMemo = Nothing
    Me.t9o.NTSRepositoryItemText = Nothing
    Me.t9o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t9o.OptionsFilter.AllowFilter = False
    Me.t9o.Visible = True
    Me.t9o.VisibleIndex = 38
    '
    't9p
    '
    Me.t9p.AppearanceCell.Options.UseBackColor = True
    Me.t9p.AppearanceCell.Options.UseTextOptions = True
    Me.t9p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t9p.Caption = "T9p"
    Me.t9p.Enabled = True
    Me.t9p.FieldName = "t9p"
    Me.t9p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t9p.Name = "t9p"
    Me.t9p.NTSRepositoryComboBox = Nothing
    Me.t9p.NTSRepositoryItemCheck = Nothing
    Me.t9p.NTSRepositoryItemMemo = Nothing
    Me.t9p.NTSRepositoryItemText = Nothing
    Me.t9p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t9p.OptionsFilter.AllowFilter = False
    Me.t9p.Visible = True
    Me.t9p.VisibleIndex = 39
    '
    't9d
    '
    Me.t9d.AppearanceCell.Options.UseBackColor = True
    Me.t9d.AppearanceCell.Options.UseTextOptions = True
    Me.t9d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t9d.Caption = "T9d"
    Me.t9d.Enabled = True
    Me.t9d.FieldName = "t9d"
    Me.t9d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t9d.Name = "t9d"
    Me.t9d.NTSRepositoryComboBox = Nothing
    Me.t9d.NTSRepositoryItemCheck = Nothing
    Me.t9d.NTSRepositoryItemMemo = Nothing
    Me.t9d.NTSRepositoryItemText = Nothing
    Me.t9d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t9d.OptionsFilter.AllowFilter = False
    Me.t9d.Visible = True
    Me.t9d.VisibleIndex = 40
    '
    't10o
    '
    Me.t10o.AppearanceCell.Options.UseBackColor = True
    Me.t10o.AppearanceCell.Options.UseTextOptions = True
    Me.t10o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t10o.Caption = "T10o"
    Me.t10o.Enabled = True
    Me.t10o.FieldName = "t10o"
    Me.t10o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t10o.Name = "t10o"
    Me.t10o.NTSRepositoryComboBox = Nothing
    Me.t10o.NTSRepositoryItemCheck = Nothing
    Me.t10o.NTSRepositoryItemMemo = Nothing
    Me.t10o.NTSRepositoryItemText = Nothing
    Me.t10o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t10o.OptionsFilter.AllowFilter = False
    Me.t10o.Visible = True
    Me.t10o.VisibleIndex = 41
    '
    't10p
    '
    Me.t10p.AppearanceCell.Options.UseBackColor = True
    Me.t10p.AppearanceCell.Options.UseTextOptions = True
    Me.t10p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t10p.Caption = "T10p"
    Me.t10p.Enabled = True
    Me.t10p.FieldName = "t10p"
    Me.t10p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t10p.Name = "t10p"
    Me.t10p.NTSRepositoryComboBox = Nothing
    Me.t10p.NTSRepositoryItemCheck = Nothing
    Me.t10p.NTSRepositoryItemMemo = Nothing
    Me.t10p.NTSRepositoryItemText = Nothing
    Me.t10p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t10p.OptionsFilter.AllowFilter = False
    Me.t10p.Visible = True
    Me.t10p.VisibleIndex = 42
    '
    't10d
    '
    Me.t10d.AppearanceCell.Options.UseBackColor = True
    Me.t10d.AppearanceCell.Options.UseTextOptions = True
    Me.t10d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t10d.Caption = "T10d"
    Me.t10d.Enabled = True
    Me.t10d.FieldName = "t10d"
    Me.t10d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t10d.Name = "t10d"
    Me.t10d.NTSRepositoryComboBox = Nothing
    Me.t10d.NTSRepositoryItemCheck = Nothing
    Me.t10d.NTSRepositoryItemMemo = Nothing
    Me.t10d.NTSRepositoryItemText = Nothing
    Me.t10d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t10d.OptionsFilter.AllowFilter = False
    Me.t10d.Visible = True
    Me.t10d.VisibleIndex = 43
    '
    't11o
    '
    Me.t11o.AppearanceCell.Options.UseBackColor = True
    Me.t11o.AppearanceCell.Options.UseTextOptions = True
    Me.t11o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t11o.Caption = "T11o"
    Me.t11o.Enabled = True
    Me.t11o.FieldName = "t11o"
    Me.t11o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t11o.Name = "t11o"
    Me.t11o.NTSRepositoryComboBox = Nothing
    Me.t11o.NTSRepositoryItemCheck = Nothing
    Me.t11o.NTSRepositoryItemMemo = Nothing
    Me.t11o.NTSRepositoryItemText = Nothing
    Me.t11o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t11o.OptionsFilter.AllowFilter = False
    Me.t11o.Visible = True
    Me.t11o.VisibleIndex = 44
    '
    't11p
    '
    Me.t11p.AppearanceCell.Options.UseBackColor = True
    Me.t11p.AppearanceCell.Options.UseTextOptions = True
    Me.t11p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t11p.Caption = "T11p"
    Me.t11p.Enabled = True
    Me.t11p.FieldName = "t11p"
    Me.t11p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t11p.Name = "t11p"
    Me.t11p.NTSRepositoryComboBox = Nothing
    Me.t11p.NTSRepositoryItemCheck = Nothing
    Me.t11p.NTSRepositoryItemMemo = Nothing
    Me.t11p.NTSRepositoryItemText = Nothing
    Me.t11p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t11p.OptionsFilter.AllowFilter = False
    Me.t11p.Visible = True
    Me.t11p.VisibleIndex = 45
    '
    't11d
    '
    Me.t11d.AppearanceCell.Options.UseBackColor = True
    Me.t11d.AppearanceCell.Options.UseTextOptions = True
    Me.t11d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t11d.Caption = "T11d"
    Me.t11d.Enabled = True
    Me.t11d.FieldName = "t11d"
    Me.t11d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t11d.Name = "t11d"
    Me.t11d.NTSRepositoryComboBox = Nothing
    Me.t11d.NTSRepositoryItemCheck = Nothing
    Me.t11d.NTSRepositoryItemMemo = Nothing
    Me.t11d.NTSRepositoryItemText = Nothing
    Me.t11d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t11d.OptionsFilter.AllowFilter = False
    Me.t11d.Visible = True
    Me.t11d.VisibleIndex = 46
    '
    't12o
    '
    Me.t12o.AppearanceCell.Options.UseBackColor = True
    Me.t12o.AppearanceCell.Options.UseTextOptions = True
    Me.t12o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t12o.Caption = "T12o"
    Me.t12o.Enabled = True
    Me.t12o.FieldName = "t12o"
    Me.t12o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t12o.Name = "t12o"
    Me.t12o.NTSRepositoryComboBox = Nothing
    Me.t12o.NTSRepositoryItemCheck = Nothing
    Me.t12o.NTSRepositoryItemMemo = Nothing
    Me.t12o.NTSRepositoryItemText = Nothing
    Me.t12o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t12o.OptionsFilter.AllowFilter = False
    Me.t12o.Visible = True
    Me.t12o.VisibleIndex = 47
    '
    't12p
    '
    Me.t12p.AppearanceCell.Options.UseBackColor = True
    Me.t12p.AppearanceCell.Options.UseTextOptions = True
    Me.t12p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t12p.Caption = "T12p"
    Me.t12p.Enabled = True
    Me.t12p.FieldName = "t12p"
    Me.t12p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t12p.Name = "t12p"
    Me.t12p.NTSRepositoryComboBox = Nothing
    Me.t12p.NTSRepositoryItemCheck = Nothing
    Me.t12p.NTSRepositoryItemMemo = Nothing
    Me.t12p.NTSRepositoryItemText = Nothing
    Me.t12p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t12p.OptionsFilter.AllowFilter = False
    Me.t12p.Visible = True
    Me.t12p.VisibleIndex = 48
    '
    't12d
    '
    Me.t12d.AppearanceCell.Options.UseBackColor = True
    Me.t12d.AppearanceCell.Options.UseTextOptions = True
    Me.t12d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t12d.Caption = "T12d"
    Me.t12d.Enabled = True
    Me.t12d.FieldName = "t12d"
    Me.t12d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t12d.Name = "t12d"
    Me.t12d.NTSRepositoryComboBox = Nothing
    Me.t12d.NTSRepositoryItemCheck = Nothing
    Me.t12d.NTSRepositoryItemMemo = Nothing
    Me.t12d.NTSRepositoryItemText = Nothing
    Me.t12d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t12d.OptionsFilter.AllowFilter = False
    Me.t12d.Visible = True
    Me.t12d.VisibleIndex = 49
    '
    't13o
    '
    Me.t13o.AppearanceCell.Options.UseBackColor = True
    Me.t13o.AppearanceCell.Options.UseTextOptions = True
    Me.t13o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t13o.Caption = "T13o"
    Me.t13o.Enabled = True
    Me.t13o.FieldName = "t13o"
    Me.t13o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t13o.Name = "t13o"
    Me.t13o.NTSRepositoryComboBox = Nothing
    Me.t13o.NTSRepositoryItemCheck = Nothing
    Me.t13o.NTSRepositoryItemMemo = Nothing
    Me.t13o.NTSRepositoryItemText = Nothing
    Me.t13o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t13o.OptionsFilter.AllowFilter = False
    Me.t13o.Visible = True
    Me.t13o.VisibleIndex = 50
    '
    't13p
    '
    Me.t13p.AppearanceCell.Options.UseBackColor = True
    Me.t13p.AppearanceCell.Options.UseTextOptions = True
    Me.t13p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t13p.Caption = "T13p"
    Me.t13p.Enabled = True
    Me.t13p.FieldName = "t13p"
    Me.t13p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t13p.Name = "t13p"
    Me.t13p.NTSRepositoryComboBox = Nothing
    Me.t13p.NTSRepositoryItemCheck = Nothing
    Me.t13p.NTSRepositoryItemMemo = Nothing
    Me.t13p.NTSRepositoryItemText = Nothing
    Me.t13p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t13p.OptionsFilter.AllowFilter = False
    Me.t13p.Visible = True
    Me.t13p.VisibleIndex = 51
    '
    't13d
    '
    Me.t13d.AppearanceCell.Options.UseBackColor = True
    Me.t13d.AppearanceCell.Options.UseTextOptions = True
    Me.t13d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t13d.Caption = "T13d"
    Me.t13d.Enabled = True
    Me.t13d.FieldName = "t13d"
    Me.t13d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t13d.Name = "t13d"
    Me.t13d.NTSRepositoryComboBox = Nothing
    Me.t13d.NTSRepositoryItemCheck = Nothing
    Me.t13d.NTSRepositoryItemMemo = Nothing
    Me.t13d.NTSRepositoryItemText = Nothing
    Me.t13d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t13d.OptionsFilter.AllowFilter = False
    Me.t13d.Visible = True
    Me.t13d.VisibleIndex = 52
    '
    't14o
    '
    Me.t14o.AppearanceCell.Options.UseBackColor = True
    Me.t14o.AppearanceCell.Options.UseTextOptions = True
    Me.t14o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t14o.Caption = "T14o"
    Me.t14o.Enabled = True
    Me.t14o.FieldName = "t14o"
    Me.t14o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t14o.Name = "t14o"
    Me.t14o.NTSRepositoryComboBox = Nothing
    Me.t14o.NTSRepositoryItemCheck = Nothing
    Me.t14o.NTSRepositoryItemMemo = Nothing
    Me.t14o.NTSRepositoryItemText = Nothing
    Me.t14o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t14o.OptionsFilter.AllowFilter = False
    Me.t14o.Visible = True
    Me.t14o.VisibleIndex = 53
    '
    't14p
    '
    Me.t14p.AppearanceCell.Options.UseBackColor = True
    Me.t14p.AppearanceCell.Options.UseTextOptions = True
    Me.t14p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t14p.Caption = "T14p"
    Me.t14p.Enabled = True
    Me.t14p.FieldName = "t14p"
    Me.t14p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t14p.Name = "t14p"
    Me.t14p.NTSRepositoryComboBox = Nothing
    Me.t14p.NTSRepositoryItemCheck = Nothing
    Me.t14p.NTSRepositoryItemMemo = Nothing
    Me.t14p.NTSRepositoryItemText = Nothing
    Me.t14p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t14p.OptionsFilter.AllowFilter = False
    Me.t14p.Visible = True
    Me.t14p.VisibleIndex = 54
    '
    't14d
    '
    Me.t14d.AppearanceCell.Options.UseBackColor = True
    Me.t14d.AppearanceCell.Options.UseTextOptions = True
    Me.t14d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t14d.Caption = "T14d"
    Me.t14d.Enabled = True
    Me.t14d.FieldName = "t14d"
    Me.t14d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t14d.Name = "t14d"
    Me.t14d.NTSRepositoryComboBox = Nothing
    Me.t14d.NTSRepositoryItemCheck = Nothing
    Me.t14d.NTSRepositoryItemMemo = Nothing
    Me.t14d.NTSRepositoryItemText = Nothing
    Me.t14d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t14d.OptionsFilter.AllowFilter = False
    Me.t14d.Visible = True
    Me.t14d.VisibleIndex = 55
    '
    't15o
    '
    Me.t15o.AppearanceCell.Options.UseBackColor = True
    Me.t15o.AppearanceCell.Options.UseTextOptions = True
    Me.t15o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t15o.Caption = "T15o"
    Me.t15o.Enabled = True
    Me.t15o.FieldName = "t15o"
    Me.t15o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t15o.Name = "t15o"
    Me.t15o.NTSRepositoryComboBox = Nothing
    Me.t15o.NTSRepositoryItemCheck = Nothing
    Me.t15o.NTSRepositoryItemMemo = Nothing
    Me.t15o.NTSRepositoryItemText = Nothing
    Me.t15o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t15o.OptionsFilter.AllowFilter = False
    Me.t15o.Visible = True
    Me.t15o.VisibleIndex = 56
    '
    't15p
    '
    Me.t15p.AppearanceCell.Options.UseBackColor = True
    Me.t15p.AppearanceCell.Options.UseTextOptions = True
    Me.t15p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t15p.Caption = "T15p"
    Me.t15p.Enabled = True
    Me.t15p.FieldName = "t15p"
    Me.t15p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t15p.Name = "t15p"
    Me.t15p.NTSRepositoryComboBox = Nothing
    Me.t15p.NTSRepositoryItemCheck = Nothing
    Me.t15p.NTSRepositoryItemMemo = Nothing
    Me.t15p.NTSRepositoryItemText = Nothing
    Me.t15p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t15p.OptionsFilter.AllowFilter = False
    Me.t15p.Visible = True
    Me.t15p.VisibleIndex = 57
    '
    't15d
    '
    Me.t15d.AppearanceCell.Options.UseBackColor = True
    Me.t15d.AppearanceCell.Options.UseTextOptions = True
    Me.t15d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t15d.Caption = "T15d"
    Me.t15d.Enabled = True
    Me.t15d.FieldName = "t15d"
    Me.t15d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t15d.Name = "t15d"
    Me.t15d.NTSRepositoryComboBox = Nothing
    Me.t15d.NTSRepositoryItemCheck = Nothing
    Me.t15d.NTSRepositoryItemMemo = Nothing
    Me.t15d.NTSRepositoryItemText = Nothing
    Me.t15d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t15d.OptionsFilter.AllowFilter = False
    Me.t15d.Visible = True
    Me.t15d.VisibleIndex = 58
    '
    't16o
    '
    Me.t16o.AppearanceCell.Options.UseBackColor = True
    Me.t16o.AppearanceCell.Options.UseTextOptions = True
    Me.t16o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t16o.Caption = "T16o"
    Me.t16o.Enabled = True
    Me.t16o.FieldName = "t16o"
    Me.t16o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t16o.Name = "t16o"
    Me.t16o.NTSRepositoryComboBox = Nothing
    Me.t16o.NTSRepositoryItemCheck = Nothing
    Me.t16o.NTSRepositoryItemMemo = Nothing
    Me.t16o.NTSRepositoryItemText = Nothing
    Me.t16o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t16o.OptionsFilter.AllowFilter = False
    Me.t16o.Visible = True
    Me.t16o.VisibleIndex = 59
    '
    't16p
    '
    Me.t16p.AppearanceCell.Options.UseBackColor = True
    Me.t16p.AppearanceCell.Options.UseTextOptions = True
    Me.t16p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t16p.Caption = "T16p"
    Me.t16p.Enabled = True
    Me.t16p.FieldName = "t16p"
    Me.t16p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t16p.Name = "t16p"
    Me.t16p.NTSRepositoryComboBox = Nothing
    Me.t16p.NTSRepositoryItemCheck = Nothing
    Me.t16p.NTSRepositoryItemMemo = Nothing
    Me.t16p.NTSRepositoryItemText = Nothing
    Me.t16p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t16p.OptionsFilter.AllowFilter = False
    Me.t16p.Visible = True
    Me.t16p.VisibleIndex = 60
    '
    't16d
    '
    Me.t16d.AppearanceCell.Options.UseBackColor = True
    Me.t16d.AppearanceCell.Options.UseTextOptions = True
    Me.t16d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t16d.Caption = "T16d"
    Me.t16d.Enabled = True
    Me.t16d.FieldName = "t16d"
    Me.t16d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t16d.Name = "t16d"
    Me.t16d.NTSRepositoryComboBox = Nothing
    Me.t16d.NTSRepositoryItemCheck = Nothing
    Me.t16d.NTSRepositoryItemMemo = Nothing
    Me.t16d.NTSRepositoryItemText = Nothing
    Me.t16d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t16d.OptionsFilter.AllowFilter = False
    Me.t16d.Visible = True
    Me.t16d.VisibleIndex = 61
    '
    't17o
    '
    Me.t17o.AppearanceCell.Options.UseBackColor = True
    Me.t17o.AppearanceCell.Options.UseTextOptions = True
    Me.t17o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t17o.Caption = "T17o"
    Me.t17o.Enabled = True
    Me.t17o.FieldName = "t17o"
    Me.t17o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t17o.Name = "t17o"
    Me.t17o.NTSRepositoryComboBox = Nothing
    Me.t17o.NTSRepositoryItemCheck = Nothing
    Me.t17o.NTSRepositoryItemMemo = Nothing
    Me.t17o.NTSRepositoryItemText = Nothing
    Me.t17o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t17o.OptionsFilter.AllowFilter = False
    Me.t17o.Visible = True
    Me.t17o.VisibleIndex = 62
    '
    't17p
    '
    Me.t17p.AppearanceCell.Options.UseBackColor = True
    Me.t17p.AppearanceCell.Options.UseTextOptions = True
    Me.t17p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t17p.Caption = "T17p"
    Me.t17p.Enabled = True
    Me.t17p.FieldName = "t17p"
    Me.t17p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t17p.Name = "t17p"
    Me.t17p.NTSRepositoryComboBox = Nothing
    Me.t17p.NTSRepositoryItemCheck = Nothing
    Me.t17p.NTSRepositoryItemMemo = Nothing
    Me.t17p.NTSRepositoryItemText = Nothing
    Me.t17p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t17p.OptionsFilter.AllowFilter = False
    Me.t17p.Visible = True
    Me.t17p.VisibleIndex = 63
    '
    't17d
    '
    Me.t17d.AppearanceCell.Options.UseBackColor = True
    Me.t17d.AppearanceCell.Options.UseTextOptions = True
    Me.t17d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t17d.Caption = "T17d"
    Me.t17d.Enabled = True
    Me.t17d.FieldName = "t17d"
    Me.t17d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t17d.Name = "t17d"
    Me.t17d.NTSRepositoryComboBox = Nothing
    Me.t17d.NTSRepositoryItemCheck = Nothing
    Me.t17d.NTSRepositoryItemMemo = Nothing
    Me.t17d.NTSRepositoryItemText = Nothing
    Me.t17d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t17d.OptionsFilter.AllowFilter = False
    Me.t17d.Visible = True
    Me.t17d.VisibleIndex = 64
    '
    't18o
    '
    Me.t18o.AppearanceCell.Options.UseBackColor = True
    Me.t18o.AppearanceCell.Options.UseTextOptions = True
    Me.t18o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t18o.Caption = "T18o"
    Me.t18o.Enabled = True
    Me.t18o.FieldName = "t18o"
    Me.t18o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t18o.Name = "t18o"
    Me.t18o.NTSRepositoryComboBox = Nothing
    Me.t18o.NTSRepositoryItemCheck = Nothing
    Me.t18o.NTSRepositoryItemMemo = Nothing
    Me.t18o.NTSRepositoryItemText = Nothing
    Me.t18o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t18o.OptionsFilter.AllowFilter = False
    Me.t18o.Visible = True
    Me.t18o.VisibleIndex = 65
    '
    't18p
    '
    Me.t18p.AppearanceCell.Options.UseBackColor = True
    Me.t18p.AppearanceCell.Options.UseTextOptions = True
    Me.t18p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t18p.Caption = "T18p"
    Me.t18p.Enabled = True
    Me.t18p.FieldName = "t18p"
    Me.t18p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t18p.Name = "t18p"
    Me.t18p.NTSRepositoryComboBox = Nothing
    Me.t18p.NTSRepositoryItemCheck = Nothing
    Me.t18p.NTSRepositoryItemMemo = Nothing
    Me.t18p.NTSRepositoryItemText = Nothing
    Me.t18p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t18p.OptionsFilter.AllowFilter = False
    Me.t18p.Visible = True
    Me.t18p.VisibleIndex = 66
    '
    't18d
    '
    Me.t18d.AppearanceCell.Options.UseBackColor = True
    Me.t18d.AppearanceCell.Options.UseTextOptions = True
    Me.t18d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t18d.Caption = "T18d"
    Me.t18d.Enabled = True
    Me.t18d.FieldName = "t18d"
    Me.t18d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t18d.Name = "t18d"
    Me.t18d.NTSRepositoryComboBox = Nothing
    Me.t18d.NTSRepositoryItemCheck = Nothing
    Me.t18d.NTSRepositoryItemMemo = Nothing
    Me.t18d.NTSRepositoryItemText = Nothing
    Me.t18d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t18d.OptionsFilter.AllowFilter = False
    Me.t18d.Visible = True
    Me.t18d.VisibleIndex = 67
    '
    't19o
    '
    Me.t19o.AppearanceCell.Options.UseBackColor = True
    Me.t19o.AppearanceCell.Options.UseTextOptions = True
    Me.t19o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t19o.Caption = "T19o"
    Me.t19o.Enabled = True
    Me.t19o.FieldName = "t19o"
    Me.t19o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t19o.Name = "t19o"
    Me.t19o.NTSRepositoryComboBox = Nothing
    Me.t19o.NTSRepositoryItemCheck = Nothing
    Me.t19o.NTSRepositoryItemMemo = Nothing
    Me.t19o.NTSRepositoryItemText = Nothing
    Me.t19o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t19o.OptionsFilter.AllowFilter = False
    Me.t19o.Visible = True
    Me.t19o.VisibleIndex = 68
    '
    't19p
    '
    Me.t19p.AppearanceCell.Options.UseBackColor = True
    Me.t19p.AppearanceCell.Options.UseTextOptions = True
    Me.t19p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t19p.Caption = "T19p"
    Me.t19p.Enabled = True
    Me.t19p.FieldName = "t19p"
    Me.t19p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t19p.Name = "t19p"
    Me.t19p.NTSRepositoryComboBox = Nothing
    Me.t19p.NTSRepositoryItemCheck = Nothing
    Me.t19p.NTSRepositoryItemMemo = Nothing
    Me.t19p.NTSRepositoryItemText = Nothing
    Me.t19p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t19p.OptionsFilter.AllowFilter = False
    Me.t19p.Visible = True
    Me.t19p.VisibleIndex = 69
    '
    't19d
    '
    Me.t19d.AppearanceCell.Options.UseBackColor = True
    Me.t19d.AppearanceCell.Options.UseTextOptions = True
    Me.t19d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t19d.Caption = "T19d"
    Me.t19d.Enabled = True
    Me.t19d.FieldName = "t19d"
    Me.t19d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t19d.Name = "t19d"
    Me.t19d.NTSRepositoryComboBox = Nothing
    Me.t19d.NTSRepositoryItemCheck = Nothing
    Me.t19d.NTSRepositoryItemMemo = Nothing
    Me.t19d.NTSRepositoryItemText = Nothing
    Me.t19d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t19d.OptionsFilter.AllowFilter = False
    Me.t19d.Visible = True
    Me.t19d.VisibleIndex = 70
    '
    't20o
    '
    Me.t20o.AppearanceCell.Options.UseBackColor = True
    Me.t20o.AppearanceCell.Options.UseTextOptions = True
    Me.t20o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t20o.Caption = "T20o"
    Me.t20o.Enabled = True
    Me.t20o.FieldName = "t20o"
    Me.t20o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t20o.Name = "t20o"
    Me.t20o.NTSRepositoryComboBox = Nothing
    Me.t20o.NTSRepositoryItemCheck = Nothing
    Me.t20o.NTSRepositoryItemMemo = Nothing
    Me.t20o.NTSRepositoryItemText = Nothing
    Me.t20o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t20o.OptionsFilter.AllowFilter = False
    Me.t20o.Visible = True
    Me.t20o.VisibleIndex = 71
    '
    't20p
    '
    Me.t20p.AppearanceCell.Options.UseBackColor = True
    Me.t20p.AppearanceCell.Options.UseTextOptions = True
    Me.t20p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t20p.Caption = "T20p"
    Me.t20p.Enabled = True
    Me.t20p.FieldName = "t20p"
    Me.t20p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t20p.Name = "t20p"
    Me.t20p.NTSRepositoryComboBox = Nothing
    Me.t20p.NTSRepositoryItemCheck = Nothing
    Me.t20p.NTSRepositoryItemMemo = Nothing
    Me.t20p.NTSRepositoryItemText = Nothing
    Me.t20p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t20p.OptionsFilter.AllowFilter = False
    Me.t20p.Visible = True
    Me.t20p.VisibleIndex = 72
    '
    't20d
    '
    Me.t20d.AppearanceCell.Options.UseBackColor = True
    Me.t20d.AppearanceCell.Options.UseTextOptions = True
    Me.t20d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t20d.Caption = "T20d"
    Me.t20d.Enabled = True
    Me.t20d.FieldName = "t20d"
    Me.t20d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t20d.Name = "t20d"
    Me.t20d.NTSRepositoryComboBox = Nothing
    Me.t20d.NTSRepositoryItemCheck = Nothing
    Me.t20d.NTSRepositoryItemMemo = Nothing
    Me.t20d.NTSRepositoryItemText = Nothing
    Me.t20d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t20d.OptionsFilter.AllowFilter = False
    Me.t20d.Visible = True
    Me.t20d.VisibleIndex = 73
    '
    't21o
    '
    Me.t21o.AppearanceCell.Options.UseBackColor = True
    Me.t21o.AppearanceCell.Options.UseTextOptions = True
    Me.t21o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t21o.Caption = "T21o"
    Me.t21o.Enabled = True
    Me.t21o.FieldName = "t21o"
    Me.t21o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t21o.Name = "t21o"
    Me.t21o.NTSRepositoryComboBox = Nothing
    Me.t21o.NTSRepositoryItemCheck = Nothing
    Me.t21o.NTSRepositoryItemMemo = Nothing
    Me.t21o.NTSRepositoryItemText = Nothing
    Me.t21o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t21o.OptionsFilter.AllowFilter = False
    Me.t21o.Visible = True
    Me.t21o.VisibleIndex = 74
    '
    't21p
    '
    Me.t21p.AppearanceCell.Options.UseBackColor = True
    Me.t21p.AppearanceCell.Options.UseTextOptions = True
    Me.t21p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t21p.Caption = "T21p"
    Me.t21p.Enabled = True
    Me.t21p.FieldName = "t21p"
    Me.t21p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t21p.Name = "t21p"
    Me.t21p.NTSRepositoryComboBox = Nothing
    Me.t21p.NTSRepositoryItemCheck = Nothing
    Me.t21p.NTSRepositoryItemMemo = Nothing
    Me.t21p.NTSRepositoryItemText = Nothing
    Me.t21p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t21p.OptionsFilter.AllowFilter = False
    Me.t21p.Visible = True
    Me.t21p.VisibleIndex = 75
    '
    't21d
    '
    Me.t21d.AppearanceCell.Options.UseBackColor = True
    Me.t21d.AppearanceCell.Options.UseTextOptions = True
    Me.t21d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t21d.Caption = "T21d"
    Me.t21d.Enabled = True
    Me.t21d.FieldName = "t21d"
    Me.t21d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t21d.Name = "t21d"
    Me.t21d.NTSRepositoryComboBox = Nothing
    Me.t21d.NTSRepositoryItemCheck = Nothing
    Me.t21d.NTSRepositoryItemMemo = Nothing
    Me.t21d.NTSRepositoryItemText = Nothing
    Me.t21d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t21d.OptionsFilter.AllowFilter = False
    Me.t21d.Visible = True
    Me.t21d.VisibleIndex = 76
    '
    't22o
    '
    Me.t22o.AppearanceCell.Options.UseBackColor = True
    Me.t22o.AppearanceCell.Options.UseTextOptions = True
    Me.t22o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t22o.Caption = "T22o"
    Me.t22o.Enabled = True
    Me.t22o.FieldName = "t22o"
    Me.t22o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t22o.Name = "t22o"
    Me.t22o.NTSRepositoryComboBox = Nothing
    Me.t22o.NTSRepositoryItemCheck = Nothing
    Me.t22o.NTSRepositoryItemMemo = Nothing
    Me.t22o.NTSRepositoryItemText = Nothing
    Me.t22o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t22o.OptionsFilter.AllowFilter = False
    Me.t22o.Visible = True
    Me.t22o.VisibleIndex = 77
    '
    't22p
    '
    Me.t22p.AppearanceCell.Options.UseBackColor = True
    Me.t22p.AppearanceCell.Options.UseTextOptions = True
    Me.t22p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t22p.Caption = "T22p"
    Me.t22p.Enabled = True
    Me.t22p.FieldName = "t22p"
    Me.t22p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t22p.Name = "t22p"
    Me.t22p.NTSRepositoryComboBox = Nothing
    Me.t22p.NTSRepositoryItemCheck = Nothing
    Me.t22p.NTSRepositoryItemMemo = Nothing
    Me.t22p.NTSRepositoryItemText = Nothing
    Me.t22p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t22p.OptionsFilter.AllowFilter = False
    Me.t22p.Visible = True
    Me.t22p.VisibleIndex = 78
    '
    't22d
    '
    Me.t22d.AppearanceCell.Options.UseBackColor = True
    Me.t22d.AppearanceCell.Options.UseTextOptions = True
    Me.t22d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t22d.Caption = "T22d"
    Me.t22d.Enabled = True
    Me.t22d.FieldName = "t22d"
    Me.t22d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t22d.Name = "t22d"
    Me.t22d.NTSRepositoryComboBox = Nothing
    Me.t22d.NTSRepositoryItemCheck = Nothing
    Me.t22d.NTSRepositoryItemMemo = Nothing
    Me.t22d.NTSRepositoryItemText = Nothing
    Me.t22d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t22d.OptionsFilter.AllowFilter = False
    Me.t22d.Visible = True
    Me.t22d.VisibleIndex = 79
    '
    't23o
    '
    Me.t23o.AppearanceCell.Options.UseBackColor = True
    Me.t23o.AppearanceCell.Options.UseTextOptions = True
    Me.t23o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t23o.Caption = "T23o"
    Me.t23o.Enabled = True
    Me.t23o.FieldName = "t23o"
    Me.t23o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t23o.Name = "t23o"
    Me.t23o.NTSRepositoryComboBox = Nothing
    Me.t23o.NTSRepositoryItemCheck = Nothing
    Me.t23o.NTSRepositoryItemMemo = Nothing
    Me.t23o.NTSRepositoryItemText = Nothing
    Me.t23o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t23o.OptionsFilter.AllowFilter = False
    Me.t23o.Visible = True
    Me.t23o.VisibleIndex = 80
    '
    't23p
    '
    Me.t23p.AppearanceCell.Options.UseBackColor = True
    Me.t23p.AppearanceCell.Options.UseTextOptions = True
    Me.t23p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t23p.Caption = "T23p"
    Me.t23p.Enabled = True
    Me.t23p.FieldName = "t23p"
    Me.t23p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t23p.Name = "t23p"
    Me.t23p.NTSRepositoryComboBox = Nothing
    Me.t23p.NTSRepositoryItemCheck = Nothing
    Me.t23p.NTSRepositoryItemMemo = Nothing
    Me.t23p.NTSRepositoryItemText = Nothing
    Me.t23p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t23p.OptionsFilter.AllowFilter = False
    Me.t23p.Visible = True
    Me.t23p.VisibleIndex = 81
    '
    't23d
    '
    Me.t23d.AppearanceCell.Options.UseBackColor = True
    Me.t23d.AppearanceCell.Options.UseTextOptions = True
    Me.t23d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t23d.Caption = "T23d"
    Me.t23d.Enabled = True
    Me.t23d.FieldName = "t23d"
    Me.t23d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t23d.Name = "t23d"
    Me.t23d.NTSRepositoryComboBox = Nothing
    Me.t23d.NTSRepositoryItemCheck = Nothing
    Me.t23d.NTSRepositoryItemMemo = Nothing
    Me.t23d.NTSRepositoryItemText = Nothing
    Me.t23d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t23d.OptionsFilter.AllowFilter = False
    Me.t23d.Visible = True
    Me.t23d.VisibleIndex = 82
    '
    't24o
    '
    Me.t24o.AppearanceCell.Options.UseBackColor = True
    Me.t24o.AppearanceCell.Options.UseTextOptions = True
    Me.t24o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t24o.Caption = "T24o"
    Me.t24o.Enabled = True
    Me.t24o.FieldName = "t24o"
    Me.t24o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t24o.Name = "t24o"
    Me.t24o.NTSRepositoryComboBox = Nothing
    Me.t24o.NTSRepositoryItemCheck = Nothing
    Me.t24o.NTSRepositoryItemMemo = Nothing
    Me.t24o.NTSRepositoryItemText = Nothing
    Me.t24o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t24o.OptionsFilter.AllowFilter = False
    Me.t24o.Visible = True
    Me.t24o.VisibleIndex = 83
    '
    't24p
    '
    Me.t24p.AppearanceCell.Options.UseBackColor = True
    Me.t24p.AppearanceCell.Options.UseTextOptions = True
    Me.t24p.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t24p.Caption = "T24p"
    Me.t24p.Enabled = True
    Me.t24p.FieldName = "t24p"
    Me.t24p.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t24p.Name = "t24p"
    Me.t24p.NTSRepositoryComboBox = Nothing
    Me.t24p.NTSRepositoryItemCheck = Nothing
    Me.t24p.NTSRepositoryItemMemo = Nothing
    Me.t24p.NTSRepositoryItemText = Nothing
    Me.t24p.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t24p.OptionsFilter.AllowFilter = False
    Me.t24p.Visible = True
    Me.t24p.VisibleIndex = 84
    '
    't24d
    '
    Me.t24d.AppearanceCell.Options.UseBackColor = True
    Me.t24d.AppearanceCell.Options.UseTextOptions = True
    Me.t24d.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.t24d.Caption = "T24d"
    Me.t24d.Enabled = True
    Me.t24d.FieldName = "t24d"
    Me.t24d.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.t24d.Name = "t24d"
    Me.t24d.NTSRepositoryComboBox = Nothing
    Me.t24d.NTSRepositoryItemCheck = Nothing
    Me.t24d.NTSRepositoryItemMemo = Nothing
    Me.t24d.NTSRepositoryItemText = Nothing
    Me.t24d.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.t24d.OptionsFilter.AllowFilter = False
    Me.t24d.Visible = True
    Me.t24d.VisibleIndex = 85
    '
    'mo_tctaglia
    '
    Me.mo_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.mo_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.mo_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_tctaglia.Caption = "Taglia P."
    Me.mo_tctaglia.Enabled = True
    Me.mo_tctaglia.FieldName = "mo_tctaglia"
    Me.mo_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_tctaglia.Name = "mo_tctaglia"
    Me.mo_tctaglia.NTSRepositoryComboBox = Nothing
    Me.mo_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.mo_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.mo_tctaglia.NTSRepositoryItemText = Nothing
    Me.mo_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_tctaglia.OptionsFilter.AllowFilter = False
    Me.mo_tctaglia.Visible = True
    Me.mo_tctaglia.VisibleIndex = 86
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.edUnmis)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.edAdatcons)
    Me.pnTop.Controls.Add(Me.edDadatcons)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbUnmis)
    Me.pnTop.Controls.Add(Me.lbAdatcons)
    Me.pnTop.Controls.Add(Me.lbDadatcons)
    Me.pnTop.Controls.Add(Me.lbXx_Magaz)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 72)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.Location = New System.Drawing.Point(62, 43)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(53, 20)
    Me.edMagaz.TabIndex = 31
    '
    'edUnmis
    '
    Me.edUnmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUnmis.Location = New System.Drawing.Point(594, 24)
    Me.edUnmis.Name = "edUnmis"
    Me.edUnmis.NTSDbField = ""
    Me.edUnmis.NTSForzaVisZoom = False
    Me.edUnmis.NTSOldValue = ""
    Me.edUnmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUnmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUnmis.Properties.AutoHeight = False
    Me.edUnmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUnmis.Properties.MaxLength = 65536
    Me.edUnmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUnmis.Size = New System.Drawing.Size(54, 20)
    Me.edUnmis.TabIndex = 30
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(560, 44)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(88, 20)
    Me.edFase.TabIndex = 29
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(62, 24)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.AutoHeight = False
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(131, 20)
    Me.edArticolo.TabIndex = 26
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatcons.Location = New System.Drawing.Point(560, 4)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.AutoHeight = False
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(88, 20)
    Me.edAdatcons.TabIndex = 25
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatcons.Location = New System.Drawing.Point(405, 4)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.AutoHeight = False
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(88, 20)
    Me.edDadatcons.TabIndex = 24
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(499, 44)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(34, 13)
    Me.lbFase.TabIndex = 18
    Me.lbFase.Text = "Fase:"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbUnmis
    '
    Me.lbUnmis.AutoSize = True
    Me.lbUnmis.BackColor = System.Drawing.Color.Transparent
    Me.lbUnmis.Location = New System.Drawing.Point(499, 24)
    Me.lbUnmis.Name = "lbUnmis"
    Me.lbUnmis.NTSDbField = ""
    Me.lbUnmis.Size = New System.Drawing.Size(77, 13)
    Me.lbUnmis.TabIndex = 16
    Me.lbUnmis.Text = "Unità di misura"
    Me.lbUnmis.Tooltip = ""
    Me.lbUnmis.UseMnemonic = False
    '
    'lbAdatcons
    '
    Me.lbAdatcons.AutoSize = True
    Me.lbAdatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbAdatcons.Location = New System.Drawing.Point(499, 4)
    Me.lbAdatcons.Name = "lbAdatcons"
    Me.lbAdatcons.NTSDbField = ""
    Me.lbAdatcons.Size = New System.Drawing.Size(22, 13)
    Me.lbAdatcons.TabIndex = 14
    Me.lbAdatcons.Text = "al :"
    Me.lbAdatcons.Tooltip = ""
    Me.lbAdatcons.UseMnemonic = False
    '
    'lbDadatcons
    '
    Me.lbDadatcons.AutoSize = True
    Me.lbDadatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatcons.Location = New System.Drawing.Point(343, 4)
    Me.lbDadatcons.Name = "lbDadatcons"
    Me.lbDadatcons.NTSDbField = ""
    Me.lbDadatcons.Size = New System.Drawing.Size(56, 13)
    Me.lbDadatcons.TabIndex = 12
    Me.lbDadatcons.Text = "Cons.dal :"
    Me.lbDadatcons.Tooltip = ""
    Me.lbDadatcons.UseMnemonic = False
    '
    'lbXx_Magaz
    '
    Me.lbXx_Magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Magaz.Location = New System.Drawing.Point(121, 43)
    Me.lbXx_Magaz.Name = "lbXx_Magaz"
    Me.lbXx_Magaz.NTSDbField = ""
    Me.lbXx_Magaz.Size = New System.Drawing.Size(372, 20)
    Me.lbXx_Magaz.TabIndex = 8
    Me.lbXx_Magaz.Tooltip = ""
    Me.lbXx_Magaz.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(7, 44)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(47, 13)
    Me.lbMagaz.TabIndex = 6
    Me.lbMagaz.Text = "Magazz."
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbXx_articolo
    '
    Me.lbXx_articolo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_articolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_articolo.Location = New System.Drawing.Point(199, 23)
    Me.lbXx_articolo.Name = "lbXx_articolo"
    Me.lbXx_articolo.NTSDbField = ""
    Me.lbXx_articolo.Size = New System.Drawing.Size(294, 20)
    Me.lbXx_articolo.TabIndex = 27
    Me.lbXx_articolo.Tooltip = ""
    Me.lbXx_articolo.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(7, 24)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 3
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrs1)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 102)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 340)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbTaglie, Me.tlbNavigazioneDoc, Me.tlbNavigazioneMrp, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbAggiornaOrdine, Me.tlbApriGestione, Me.tlbStrumenti, Me.tlbStampaVideo, Me.tlbStampa, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 21
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriGestione, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiornaOrdine), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneDoc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneMrp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 9
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 10
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 11
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 12
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.GlyphPath = ""
    Me.tlbTaglie.Id = 0
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbApriGestione
    '
    Me.tlbApriGestione.Caption = "ApriGestione"
    Me.tlbApriGestione.Glyph = CType(resources.GetObject("tlbApriGestione.Glyph"), System.Drawing.Image)
    Me.tlbApriGestione.GlyphPath = ""
    Me.tlbApriGestione.Id = 14
    Me.tlbApriGestione.Name = "tlbApriGestione"
    Me.tlbApriGestione.Visible = True
    '
    'tlbAggiornaOrdine
    '
    Me.tlbAggiornaOrdine.Caption = "AggiornaOrdine"
    Me.tlbAggiornaOrdine.Glyph = CType(resources.GetObject("tlbAggiornaOrdine.Glyph"), System.Drawing.Image)
    Me.tlbAggiornaOrdine.GlyphPath = ""
    Me.tlbAggiornaOrdine.Id = 13
    Me.tlbAggiornaOrdine.Name = "tlbAggiornaOrdine"
    Me.tlbAggiornaOrdine.Visible = True
    '
    'tlbNavigazioneDoc
    '
    Me.tlbNavigazioneDoc.Caption = "NavigazioneDoc"
    Me.tlbNavigazioneDoc.Glyph = CType(resources.GetObject("tlbNavigazioneDoc.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneDoc.GlyphPath = ""
    Me.tlbNavigazioneDoc.Id = 4
    Me.tlbNavigazioneDoc.Name = "tlbNavigazioneDoc"
    Me.tlbNavigazioneDoc.Visible = True
    '
    'tlbNavigazioneMrp
    '
    Me.tlbNavigazioneMrp.Caption = "NavigazioneMrp"
    Me.tlbNavigazioneMrp.Glyph = CType(resources.GetObject("tlbNavigazioneMrp.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneMrp.GlyphPath = ""
    Me.tlbNavigazioneMrp.Id = 5
    Me.tlbNavigazioneMrp.Name = "tlbNavigazioneMrp"
    Me.tlbNavigazioneMrp.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 20
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 18
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 8
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMORGRS1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(660, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMORGRS1"
    Me.NTSLastControlFocussed = Me.grGrs1
    Me.Text = "STAMPA / VISUALIZZAZIONE SCHEDE ORDINI PER ARTICOLO PIANIFICAZIONE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrs1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrs1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByVal cleScho As CLEORSCHO)
    oCleScho = cleScho
    AddHandler oCleScho.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbApriGestione.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbAggiornaOrdine.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbNavigazioneDoc.GlyphPath = (oApp.ChildImageDir & "\navigazione.gif")
        tlbNavigazioneMrp.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edMagaz.NTSSetParam(oMenu, oApp.Tr(Me, 129048515416054085, "Magazz."), "0")
      edUnmis.NTSSetParam(oMenu, oApp.Tr(Me, 129048515625121937, "Unità di misura"), 0)
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 129048515650747593, "Fase:"), "0")
      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 129048515669810581, "Articolo"), 0)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129048515693561189, "al :"), True)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129048515714030463, "Cons.dal :"), True)

      grvGrs1.NTSSetParam(oMenu, "Stampa / Visualizzazione Schede Ordini")

      xx_origine.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029657500000, "Origine"), 0, True)
      mo_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128617029657656250, "Data Ord."), True)
      xx_tipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029657812500, "Tipo"), 0)
      mo_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029657968750, "Cliente/Fornitore"), 0, True)
      xx_numord.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029658125000, "N°ordine"), 0, True)
      mo_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029658281251, "Magazzino"), "0", 4, 0, 9999)
      mo_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029658281250, "Commessa"), "0", 9, 0, 999999999)
      xx_consolidato.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029658437500, "Consolidato"), 0, True)
      xx_proposto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029658593750, "Proposto"), 0, True)
      xx_disp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029658750000, "Disponibilità"), "#,##0.00", 15)
      mo_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128617029658906250, "Consegna"), True)
      mo_tipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029659062500, "MO_TIPORK"), 0, True)
      mo_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029659218750, "MO_ANNO"), "0", 4, 0, 9999)
      mo_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029659375000, "MO_SERIE"), CLN__STD.SerieMaxLen, True)
      mo_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029659531250, "MO_NUMORD"), "0", 9, 0, 999999999)
      mo_olprogr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029659687500, "MO_OLPROGR"), "0", 9, 0, 999999999)
      mo_origine.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029659843750, "MO_ORIGINE"), 0, True)
      mo_codpadr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029660000000, "Cod. art. padre"), 0, True)
      mo_despadr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029660156250, "Descr. art. padre"), 0, True)
      mo_fasepadr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029660312500, "Fase art. padre"), "0", 4, 0, 9999)
      t1o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029660468750, "T1o"), "#,##0.00", 15)
      t1p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029660625000, "T1p"), "#,##0.00", 15)
      t1d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029660781250, "T1d"), "#,##0.00", 15)
      t2o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029660937500, "T2o"), "#,##0.00", 15)
      t2p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661093750, "T2p"), "#,##0.00", 15)
      t2d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661250000, "T2d"), "#,##0.00", 15)
      t3o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661406250, "T3o"), "#,##0.00", 15)
      t3p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661562500, "T3p"), "#,##0.00", 15)
      t3d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661718750, "T3d"), "#,##0.00", 15)
      t4o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029661875000, "T4o"), "#,##0.00", 15)
      t4p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662031250, "T4p"), "#,##0.00", 15)
      t4d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662187500, "T4d"), "#,##0.00", 15)
      t5o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662343750, "T5o"), "#,##0.00", 15)
      t5p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662500000, "T5p"), "#,##0.00", 15)
      t5d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662656250, "T5d"), "#,##0.00", 15)
      t6o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662812500, "T6o"), "#,##0.00", 15)
      t6p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029662968750, "T6p"), "#,##0.00", 15)
      t6d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663125000, "T6d"), "#,##0.00", 15)
      t7o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663281250, "T7o"), "#,##0.00", 15)
      t7p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663437500, "T7p"), "#,##0.00", 15)
      t7d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663593750, "T7d"), "#,##0.00", 15)
      t8o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663750000, "T8o"), "#,##0.00", 15)
      t8p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029663906250, "T8p"), "#,##0.00", 15)
      t8d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664062500, "T8d"), "#,##0.00", 15)
      t9o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664218750, "T9o"), "#,##0.00", 15)
      t9p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664375000, "T9p"), "#,##0.00", 15)
      t9d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664531250, "T9d"), "#,##0.00", 15)
      t10o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664687500, "T10o"), "#,##0.00", 15)
      t10p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029664843750, "T10p"), "#,##0.00", 15)
      t10d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665000000, "T10d"), "#,##0.00", 15)
      t11o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665156250, "T11o"), "#,##0.00", 15)
      t11p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665312500, "T11p"), "#,##0.00", 15)
      t11d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665468750, "T11d"), "#,##0.00", 15)
      t12o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665625000, "T12o"), "#,##0.00", 15)
      t12p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665781250, "T12p"), "#,##0.00", 15)
      t12d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029665937500, "T12d"), "#,##0.00", 15)
      t13o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666093750, "T13o"), "#,##0.00", 15)
      t13p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666250000, "T13p"), "#,##0.00", 15)
      t13d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666406250, "T13d"), "#,##0.00", 15)
      t14o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666562500, "T14o"), "#,##0.00", 15)
      t14p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666718750, "T14p"), "#,##0.00", 15)
      t14d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029666875000, "T14d"), "#,##0.00", 15)
      t15o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667031250, "T15o"), "#,##0.00", 15)
      t15p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667187500, "T15p"), "#,##0.00", 15)
      t15d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667343750, "T15d"), "#,##0.00", 15)
      t16o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667500000, "T16o"), "#,##0.00", 15)
      t16p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667656250, "T16p"), "#,##0.00", 15)
      t16d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667812500, "T16d"), "#,##0.00", 15)
      t17o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029667968750, "T17o"), "#,##0.00", 15)
      t17p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668125000, "T17p"), "#,##0.00", 15)
      t17d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668281250, "T17d"), "#,##0.00", 15)
      t18o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668437500, "T18o"), "#,##0.00", 15)
      t18p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668593750, "T18p"), "#,##0.00", 15)
      t18d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668750000, "T18d"), "#,##0.00", 15)
      t19o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029668906250, "T19o"), "#,##0.00", 15)
      t19p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669062500, "T19p"), "#,##0.00", 15)
      t19d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669218750, "T19d"), "#,##0.00", 15)
      t20o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669375000, "T20o"), "#,##0.00", 15)
      t20p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669531250, "T20p"), "#,##0.00", 15)
      t20d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669687500, "T20d"), "#,##0.00", 15)
      t21o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029669843750, "T21o"), "#,##0.00", 15)
      t21p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670000000, "T21p"), "#,##0.00", 15)
      t21d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670156250, "T21d"), "#,##0.00", 15)
      t22o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670312500, "T22o"), "#,##0.00", 15)
      t22p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670468750, "T22p"), "#,##0.00", 15)
      t22d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670625000, "T22d"), "#,##0.00", 15)
      t23o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670781250, "T23o"), "#,##0.00", 15)
      t23p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029670937500, "T23p"), "#,##0.00", 15)
      t23d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029671093750, "T23d"), "#,##0.00", 15)
      t24o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029671250000, "T24o"), "#,##0.00", 15)
      t24p.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029671406250, "T24p"), "#,##0.00", 15)
      t24d.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617029671562500, "T24d"), "#,##0.00", 15)
      mo_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617029671718750, "Taglia P."), 0, True)

      grvGrs1.Enabled = False
      grvGrs1.NTSAllowInsert = False

      edDadatcons.Enabled = False
      edAdatcons.Enabled = False
      edArticolo.Enabled = False
      edUnmis.Enabled = False
      edMagaz.Enabled = False
      edFase.Enabled = False

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
  Public Overridable Sub FRMORGRS1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      edDadatcons.Text = oCleScho.strSch1Dadatcons
      edAdatcons.Text = oCleScho.strSch1Adatcons
      If Not Apri() Then Exit Sub
      ApriRecordset()
      RiempiLabel()
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs1ModTCO = False Then tlbTaglie.Visible = False
      '-----------------------------------------------------------------------------------------
      If Not oCleScho.Grs1GetRelease() Then
        tlbNavigazioneDoc.Visible = True
      Else
        tlbNavigazioneDoc.Visible = False
      End If

      If oCleScho.bGrs1Accorpa Then
        mo_magaz.Visible = True
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If Not oCleScho.bGrs1Accorpa Then
        mo_magaz.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRS1_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRS1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then oMenu.ResetTblInstId("TTSCHO", False, oCleScho.lIITTScho)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORGRS1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrs1.Dispose()
      dsGrs1.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      dcGrs1.MoveFirst()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      dcGrs1.MovePrevious()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      dcGrs1.MoveNext()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      dcGrs1.MoveLast()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsArtico As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie e colori esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs1ModTCO = False Then Exit Sub

      If grvGrs1.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è gestito per taglia, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrs1ModTCO = True Then
        If Not oCleScho.CheckArticotaglie(NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_codart)) Then
          Exit Sub
        End If
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSORSCHO"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_codart) & "|" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_codart) & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_fase) & "|" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_fase) & "|" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_tipork) & ";" & _
               Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_anno), 4) & ";" & _
               NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_serie) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_numord), 9) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrs1.NTSGetCurrentDataRow!mo_riga), 9) & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneDoc.ItemClick
    Dim strParam As String
    Try
      If grvGrs1.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      If (NTSCStr(grvGrs1.NTSGetCurrentDataRow()!MO_ORIGINE) <> "M" And NTSCStr(grvGrs1.NTSGetCurrentDataRow()!MO_ORIGINE) <> "O") Or _
         (NTSCInt(grvGrs1.NTSGetCurrentDataRow()!MO_OLPROGR) <> 0) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611009251875000, "Funzione non disponibile per questo tipo di ordine/impegno."))
        Exit Sub
      End If

      strParam = "APRI;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_tipork), 1) & ";" & _
          Microsoft.VisualBasic.Right("0000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_anno), 4) & ";" & _
          NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_serie).PadLeft(1) & ";" & _
          Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_numord), 9) & ";" & _
          "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_datord).ToShortDateString, 10) & _
          ";000000000;0000;0000; ;000000000;0000;1"

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 129048516302014265, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneMrp.ItemClick
    Dim strParam As String = ""
    Try
      If grvGrs1.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      If NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_origine) <> "O" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_origine) <> "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611040662816496, "Funzione non disponibile per questo tipo di ordine/impegno."))
        Exit Sub
      End If

      Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_origine)
        Case "M"
          strParam = "O;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_tipork), 1) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_anno), 4) & ";" & _
            Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_serie), 1) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_numord), 9) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_riga), 9) & ";" & _
            Microsoft.VisualBasic.Right("".PadLeft(CLN__STD.CodartMaxLen) & NTSCStr(edArticolo.Text), CLN__STD.CodartMaxLen) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(edFase.Text), 4)

          oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", " ", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
        Case "O"
          strParam = "P;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_tipork), 1) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_olprogr), 9) & ";" & _
            Microsoft.VisualBasic.Right("".PadLeft(CLN__STD.CodartMaxLen) & NTSCStr(edArticolo.Text), CLN__STD.CodartMaxLen) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(edFase.Text), 4)

          oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", " ", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
        Case "T"
          strParam = "T;" & Microsoft.VisualBasic.Right("".PadLeft(CLN__STD.CodartMaxLen) & NTSCStr(edArticolo.Text), CLN__STD.CodartMaxLen) & ";" & _
            Microsoft.VisualBasic.Right("0000" & NTSCStr(edFase.Text), 4) & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!Mmo_olprogr), 9) & ";" & _
            Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_numord), 9) & ";"

          oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", " ", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApriGestione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriGestione.ItemClick
    Dim strTipork As String
    Dim nAnno As Integer
    Dim strSerie As String
    Dim lNumord As Integer
    Dim strParam As String
    Dim lOlprogr As Integer
    Try
      If grvGrs1.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      If NTSCStr(grvGrs1.NTSGetCurrentDataRow()!mo_origine) = "Z" Then Exit Sub
      '-----------------------------------------------------------------------------------------
      Select Case NTSCStr(grvGrs1.NTSGetCurrentDataRow()!mo_origine)
        Case "M"
          '-------------------------------------------------------------------------------------
          strTipork = NTSCStr(grvGrs1.NTSGetCurrentDataRow()!mo_tipork)
          nAnno = NTSCInt(grvGrs1.NTSGetCurrentDataRow()!mo_anno)
          strSerie = NTSCStr(grvGrs1.NTSGetCurrentDataRow()!mo_serie)
          lNumord = NTSCInt(grvGrs1.NTSGetCurrentDataRow()!mo_numord)
          '-------------------------------------------------------------------------------------
          If oCleScho.IsDocRetail(DittaCorrente, strTipork, nAnno, strSerie, lNumord) = True Then
            strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
              strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumord), 9) & ";"
            oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 130021095459106656, "Gestione Ordini/Impegni"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
          ElseIf oCleScho.IsDocRetailNew(DittaCorrente, strTipork, nAnno, strSerie, lNumord) = True Then
            strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
              strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumord), 9) & ";"
            oMenu.RunChild("BSREGSRE", "CLSREGSRE", "Gestione retail", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
          Else
            strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("0000" & NTSCStr(nAnno), 4) & ";" & _
              strSerie & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lNumord), 9) & ";"
            oMenu.RunChild("BSORGSOR", "CLSORGSOR", "Gestione Ordini/Impegni", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
          End If
          '-------------------------------------------------------------------------------------
          oCleScho.bApertaGestione = True
          '-------------------------------------------------------------------------------------
        Case "O"
          strTipork = NTSCStr(grvGrs1.NTSGetCurrentDataRow()!mo_tipork)
          lOlprogr = NTSCInt(grvGrs1.NTSGetCurrentDataRow()!mo_olprogr)
          If strTipork = "Y" Then
            oCleScho.Grs1GetOrdList(lOlprogr, strTipork)
          End If

          strParam = "APRI;" & strTipork & ";" & Microsoft.VisualBasic.Right("000000000" & NTSCStr(lOlprogr), 9) & ";"
          oMenu.RunChild("BSORGSOL", "CLSORGSOL", "Proposte d'Ordine", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)
          oCleScho.bApertaGestione = True
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiornaOrdine_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiornaOrdine.ItemClick
    Dim dlgRes As DialogResult
    Try
      '----------------------------------------------------------------------------------
      ' Se non è stata aperta una gestione è inutile che faccia l'aggiornamento
      '----------------------------------------------------------------------------------
      If oCleScho.bApertaGestione = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128610964791718750, "Non è stata aperta nessuna gestione degli ordini." & vbCrLf & "Aggiornamento non possibile."))
        Exit Sub
      End If
      '----------------------------------------------------------------------------------
      ' Chiede conferma dell'aggiornamento
      '----------------------------------------------------------------------------------
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128610966960781250, "Procedere con l'aggiornamento degli ordini eventualmente modificati?"))
      If dlgRes = Windows.Forms.DialogResult.No Then Exit Sub
      '----------------------------------------------------------------------------------
      ' Setta le variabili per la riapertura con gli eventuali valori modificati
      '----------------------------------------------------------------------------------
      oCleScho.bSch1AggiornaOrdine = True
      oCleScho.strSch1Codart = NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_codart)
      oCleScho.nSch1Magaz = NTSCInt(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_magaz)
      oCleScho.nSch1Fase = NTSCInt(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!mo_fase)
      Me.Close()
      '----------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grGrs1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grGrs1.MouseDoubleClick
    Try
      If tlbApriGestione.Enabled And tlbApriGestione.Visible Then tlbApriGestione_ItemClick(tlbApriGestione, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grvGrs1_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGrs1.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleScho Is Nothing Then Return

      dtrT = grvGrs1.NTSGetCurrentDataRow
      '------------------------------------

      Select Case NTSCStr(dtrT!mo_origine)
        Case "M"
          GctlSetVisEnab(tlbApriGestione, False)
          GctlSetVisEnab(tlbAggiornaOrdine, False)
        Case "O"
          GctlSetVisEnab(tlbApriGestione, False)
          GctlSetVisEnab(tlbAggiornaOrdine, False)
        Case "Z"
          tlbApriGestione.Enabled = False
          tlbAggiornaOrdine.Enabled = False
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub SetColonneTaglie()
    Dim strPref As String
    Dim strSuf As String
    Dim strTaglia As String
    Dim strCampoTaglia As String
    Dim strTagliaDescr As String
    Dim bTagliaPresente As Boolean
    Dim i As Integer
    Dim dsTaglie As DataSet = Nothing
    Try
      strPref = "t"

      oCleScho.Grs1GetTaglie(NTSCStr(dsGrs1.Tables("MOVORD").Rows(dcGrs1.Position)!mo_codart), dsTaglie)

      If dsTaglie.Tables("ARTICO").Rows.Count = 0 Then
        oCleScho.bArticoloTC = False
      Else
        oCleScho.bArticoloTC = True
      End If

      ' no strSchoOrdin

      If (oCleScho.strSchoOrdin = "A" Or oCleScho.strSchoOrdin = "C") And oCleScho.bGrs1ModTCO = True And oCleScho.bArticoloTC = True Then
        For i = 1 To 24 'se settata taglia in scala taglie col visibile else invisibile
          strCampoTaglia = "tb_dest" & Format(i, "00")

          If NTSCStr(dsTaglie.Tables("ARTICO").Rows(0)(strCampoTaglia)) <> " " Then
            bTagliaPresente = True
            strTagliaDescr = NTSCStr(dsTaglie.Tables("ARTICO").Rows(0)(strCampoTaglia))
          Else
            bTagliaPresente = False
            strTagliaDescr = ""
          End If

          strSuf = "o"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrs1.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr

          strSuf = "p"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrs1.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr

          strSuf = "d"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrs1.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr
        Next
      Else
        For i = 1 To 24
          strSuf = "o"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = False
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False

          strSuf = "p"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = False
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False

          strSuf = "d"
          strTaglia = strPref & i & strSuf
          grvGrs1.NTSGetColumnByName(strTaglia).Visible = False
          grvGrs1.NTSGetColumnByName(strTaglia).Enabled = False
        Next
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ApriRecordset()
    Try
      SetColonneTaglie()
      ComponiStringa()

      CaricaColonneUnbound()

      dcGrid.DataSource = dsGrid.Tables("MOVORD")
      dsGrid.AcceptChanges()

      grGrs1.DataSource = dcGrid

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiLabel()
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If dsGrs1.Tables("MOVORD").Rows.Count = 0 Then Exit Sub

      edArticolo.Text = NTSCStr(dsGrs1.Tables("MOVORD").Rows(dcGrs1.Position)!mo_codart)
      If Not oCleScho.Grs1lbArticolo_Validated(edArticolo.Text, strTmp, dttTmp) Then
        edArticolo.Text = ""
        edUnmis.Text = ""
      Else
        lbXx_articolo.Text = strTmp & " " & NTSCStr(dttTmp.Rows(0)!ar_desint)
        edUnmis.Text = NTSCStr(dttTmp.Rows(0)!ar_unmis)
      End If

      edFase.Text = NTSCStr(dsGrs1.Tables("MOVORD").Rows(dcGrs1.Position)!mo_fase)
      edMagaz.Text = NTSCStr(dsGrs1.Tables("MOVORD").Rows(dcGrs1.Position)!mo_magaz)
      If Not oCleScho.Grs1lbMagaz_Validated(NTSCInt(edMagaz.Text), strTmp) Then
        lbXx_Magaz.Text = ""
      Else
        lbXx_Magaz.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringa()
    Dim dEsist As Decimal
    Dim ds As DataSet = Nothing
    Try
      If oCleScho.Grs1ComponiStringa(dsGrs1.Tables("MOVORD").Rows(dcGrs1.Position), dsGrid, dEsist) Then
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleScho.Grs1Apri(dsGrs1) Then
        bClose = True
        Return False
      End If

      dcGrs1.DataSource = dsGrs1.Tables("MOVORD")
      dsGrs1.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaColonneUnbound() As Boolean
    Dim i As Integer
    Dim j As Integer
    Try
      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine)
          Case "M" : dsGrid.Tables("MOVORD").Rows(i)!xx_origine = "Ordini/Impegni"
          Case "O" : dsGrid.Tables("MOVORD").Rows(i)!xx_origine = "Proposte d'Ordine"
          Case "Z" : dsGrid.Tables("MOVORD").Rows(i)!xx_origine = "Fabb.primari EMRP"
          Case "T" : dsGrid.Tables("MOVORD").Rows(i)!xx_origine = "Appr. Interni TASK"
          Case "A" : dsGrid.Tables("MOVORD").Rows(i)!xx_origine = "Esistenza"
        End Select
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine)
          Case "M"
            Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_tipork)
              Case "$" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "OFA"
              Case "R" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "IC"
              Case "O" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "OF"
              Case "H" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "OP"
              Case "V" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "ICA"
              Case "Y" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "IP"
              Case "X" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "IT"
              Case "#" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "ICO"
            End Select
          Case "O"
            Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_tipork)
              Case "O" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "POF"
              Case "H" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "POP"
              Case "Y" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "PIP"
              Case "X" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "PIT"
            End Select
          Case "Z" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "FP"
          Case "T" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "AIT"
          Case "A" : dsGrid.Tables("MOVORD").Rows(i)!xx_tipork = "ES."
        End Select
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        Select Case NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine)
          Case "M"
            If NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) = " " Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_numord)
            Else
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) & " / " & NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_numord)
            End If
          Case "O" : dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_olprogr)
          Case "Z"
            If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) = " ") And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_numord) > 0) Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_numord)
            End If
            If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) <> " ") And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_numord) > 0) Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) & " / " & NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_numord)
            End If
            If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_serie) = " ") And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_numord) = 0) And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_progrlp) > 0) Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_progrlp)
            End If
            If Not NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_riferim) = "" Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_riferim) ' numero commessa + numero task
            End If
          Case "T" : dsGrid.Tables("MOVORD").Rows(i)!xx_numord = NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_numord) & " / " & NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_olprogr) ' numero commessa + numero task
          Case Else
            dsGrid.Tables("MOVORD").Rows(i)!xx_numord = ""
        End Select
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "O") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "Z") Then 'prop. d'ordine e fabb. primari
          dsGrid.Tables("MOVORD").Rows(i)!xx_consolidato = ""
        Else
          If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "A") Then
            dsGrid.Tables("MOVORD").Rows(i)!xx_consolidato = Format((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quant)), oApp.FormatQta)
          Else
            If (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_impeg) = 1) And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_ordin) = 0) Then
              dsGrid.Tables("MOVORD").Rows(i)!xx_consolidato = Format(((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quaeva)) * (-1)), oApp.FormatQta)
            Else
              dsGrid.Tables("MOVORD").Rows(i)!xx_consolidato = Format((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quaeva)), oApp.FormatQta)
            End If
          End If
        End If
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "M") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "T") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "A") Then  ' ordini eff. più task
          dsGrid.Tables("MOVORD").Rows(i)!xx_proposto = ""
        Else
          If (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_impeg) = 1) And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_ordin) = 0) Then
            dsGrid.Tables("MOVORD").Rows(i)!xx_proposto = Format((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quant) * (-1)), oApp.FormatQta)
          Else
            dsGrid.Tables("MOVORD").Rows(i)!xx_proposto = Format(NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quant), oApp.FormatQta)
          End If
        End If
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        For j = 1 To 24
          If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "O") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "Z") Then 'prop. d'ordine e fabb. primari
            dsGrid.Tables("MOVORD").Rows(i)("t" & j & "o") = "0"
          Else
            If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "A") Then
              dsGrid.Tables("MOVORD").Rows(i)("t" & j & "o") = Format$((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)("mo_quant" & Format(j, "00")))), oApp.FormatQta)
            Else
              If (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_impeg) = 1) And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_ordin) = 0) Then
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "o") = Format(((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)("mo_quant" & Format(j, "00")))) - NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quaeva) * (-1)), oApp.FormatQta)
              Else
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "o") = Format((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)("mo_quant" & Format(j, "00")))) - NTSCDec(dsGrid.Tables("MOVORD").Rows(i)!mo_quaeva), oApp.FormatQta)
              End If
            End If
          End If
        Next
      Next

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        For j = 1 To 24
          If (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "M") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "T") Or (NTSCStr(dsGrid.Tables("MOVORD").Rows(i)!mo_origine) = "A") Then  ' ordini eff. più task
            dsGrid.Tables("MOVORD").Rows(i)("t" & j & "p") = "0"
          Else
            If (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_impeg) = 1) And (NTSCInt(dsGrid.Tables("MOVORD").Rows(i)!mo_ordin) = 0) Then
              dsGrid.Tables("MOVORD").Rows(i)("t" & j & "p") = Format((NTSCDec(dsGrid.Tables("MOVORD").Rows(i)("mo_quant" & Format(j, "00"))) * (-1)), oApp.FormatQta)
            Else
              dsGrid.Tables("MOVORD").Rows(i)("t" & j & "p") = Format(NTSCDec(dsGrid.Tables("MOVORD").Rows(i)("mo_quant" & Format(j, "00"))), oApp.FormatQta)
            End If
          End If
        Next
      Next
      If oCleScho.bArticoloTC Then
        For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
          For j = 1 To 24
            Select Case j
              Case 1
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC1(i)
              Case 2
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC2(i)
              Case 3
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC3(i)
              Case 4
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC4(i)
              Case 5
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC5(i)
              Case 6
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC6(i)
              Case 7
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC7(i)
              Case 8
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC18(i)
              Case 9
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC9(i)
              Case 10
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC10(i)
              Case 11
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC11(i)
              Case 12
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC12(i)
              Case 13
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC13(i)
              Case 14
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC14(i)
              Case 15
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC15(i)
              Case 16
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC16(i)
              Case 17
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC17(i)
              Case 18
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC18(i)
              Case 19
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC19(i)
              Case 20
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC20(i)
              Case 21
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC21(i)
              Case 22
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC22(i)
              Case 23
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC23(i)
              Case 24
                dsGrid.Tables("MOVORD").Rows(i)("t" & j & "d") = oCleScho.dDispTC24(i)
            End Select
          Next
        Next
      End If

      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        dsGrid.Tables("MOVORD").Rows(i)!xx_disp = oCleScho.dDisp(i)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub StampaGriglia(ByVal nDestinazione As Integer)
    '----------------------------------------------------------------------------------------------------------------
    '--- nDestinazione:
    '----- 0 --> video
    '----- 1 --> stampante
    '----------------------------------------------------------------------------------------------------------------
    Dim strCaption As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCaption = "ARTICOLO: " & edArticolo.Text & " " & lbXx_articolo.Text.ToUpper & vbCrLf & _
        "   MAGAZ: " & edMagaz.Text & " " & lbXx_Magaz.Text.ToUpper
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      If grGrs1.IsPrintingAvailable Then
        grvGrs1.OptionsPrint.AutoWidth = False
        grvGrs1.OptionsPrint.UsePrintStyles = True
        grvGrs1.OptionsPrint.EnableAppearanceEvenRow = True
        grvGrs1.AppearancePrint.EvenRow.BackColor = Color.White
        Dim PrintingSystem1 As New DevExpress.XtraPrinting.PrintingSystem
        Dim PrintableComponentLink1 As New DevExpress.XtraPrinting.PrintableComponentLink
        PrintingSystem1.Links.AddRange(New Object() {PrintableComponentLink1})
        PrintableComponentLink1.Component = grvGrs1.GridControl
        PrintableComponentLink1.PageHeaderFooter = New DevExpress.XtraPrinting.PageHeaderFooter(New DevExpress.XtraPrinting.PageHeaderArea(New String() {strCaption, "", DateTime.Now.ToShortDateString}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near), New DevExpress.XtraPrinting.PageFooterArea(New String() {"", "", "[Page # of Pages #]"}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near))
        PrintableComponentLink1.Margins = New System.Drawing.Printing.Margins(20, 20, 70, 30)
        PrintableComponentLink1.Landscape = True
        PrintableComponentLink1.PaperKind = Printing.PaperKind.A4
        PrintableComponentLink1.PrintingSystem = PrintingSystem1
        PrintableComponentLink1.CreateDocument()
        '------------------------------------------------------------------------------------------------------------
        Select Case nDestinazione
          Case 0 '--- VIDEO
            PrintableComponentLink1.ShowPreview()
          Case 1 '--- STAMPANTE
            Dim pd As New System.Drawing.Printing.PrintDocument()
            PrintableComponentLink1.Print(pd.PrinterSettings.PrinterName)
            pd.Dispose()
        End Select
        '------------------------------------------------------------------------------------------------------------
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 130420257863345797, "Stampa griglia non abilitata. File DevExpress.XtraPrinting non trovato"))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

End Class
