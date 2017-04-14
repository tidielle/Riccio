Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEDDTS
  Public bOk As Boolean = False
  Public oCleFadi As CLEVEFADI
  Public dcDdts As BindingSource = New BindingSource()

  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grDdts As NTSInformatica.NTSGrid
  Public WithEvents grvDdts As NTSInformatica.NTSGridView
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdDeseleziona As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents tm_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tm_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents tm_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tm_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents tm_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents tm_vistato As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tm_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpbf As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents cmdDelete As NTSInformatica.NTSButton
  Public WithEvents tm_codagen As NTSInformatica.NTSGridColumn
  Public WithEvents tm_codagen2 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_codese As NTSInformatica.NTSGridColumn
  Public WithEvents tm_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_scopag As NTSInformatica.NTSGridColumn
  Public WithEvents tm_valuta As NTSInformatica.NTSGridColumn
  Public WithEvents tm_cambio As NTSInformatica.NTSGridColumn
  Public WithEvents tm_abi As NTSInformatica.NTSGridColumn
  Public WithEvents tm_cab As NTSInformatica.NTSGridColumn
  Public WithEvents tm_banc1 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_banc2 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_codbanc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codbanc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_scorpo As NTSInformatica.NTSGridColumn
  Public WithEvents tm_flspinc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_flbolli As NTSInformatica.NTSGridColumn
  Public WithEvents tm_pagato As NTSInformatica.NTSGridColumn
  Public WithEvents tm_pagato2 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_contfatt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_contfatt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codagen As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codagen2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codese As NTSInformatica.NTSGridColumn
  Public WithEvents tm_fattsep As NTSInformatica.NTSGridColumn
  Public WithEvents tm_dianno As NTSInformatica.NTSGridColumn
  Public WithEvents tm_dinumero As NTSInformatica.NTSGridColumn
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton


  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If

    If Ditta <> "" Then DittaCorrente = Ditta

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Me.MinimizeBox = False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      Dim dttTipoRk As New DataTable
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"A", "Fatture immediate emesse"})
      dttTipoRk.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipoRk.Rows.Add(New Object() {"D", "Fatture differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"i", "Riemissione ricevute fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Note di accredito ricevute"})
      dttTipoRk.Rows.Add(New Object() {"(", "Note di accredito differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fatture immediate ricevute"})
      dttTipoRk.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipoRk.Rows.Add(New Object() {"K", "Fatture differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"P", "Fatture/ricevute fiscali differite"})
      dttTipoRk.Rows.Add(New Object() {"N", "Note di accredito emesse"})
      dttTipoRk.Rows.Add(New Object() {"£", "Note di accredito differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatture/ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carichi da produzione"})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarichi a produzione"})
      dttTipoRk.Rows.Add(New Object() {"W", "Note di prelievo"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolle di movimentazione interna"})
      dttTipoRk.Rows.Add(New Object() {"1", "Fattura acconto parcellaz."})

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvDdts.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia testata documenti"))
      grvDdts.NTSAllowDelete = False
      grvDdts.NTSAllowUpdate = True
      grvDdts.NTSAllowInsert = False
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589568045625000, "Seleziona"), "S", "N")
      tm_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568058750000, "Conto"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568070625000, "Descr. Conto"), 0, True)
      tm_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128589568097812500, "Tipo "), dttTipoRk, "val", "cod")
      tm_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568110312500, "Anno"), "0", 4, 0, 9999)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568122656250, "Serie"), CLN__STD.SerieMaxLen, True)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568138437500, "Numero"), "0", 9, 0, 999999999)
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589568149531250, "Data doc."), True)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568204375000, "Riferimenti"), 0, True)
      tm_tipobf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568249218750, "Tipo bolla/fattura"), tabtpbf)
      xx_codtpbf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568261718750, "Descr. tipo bolla/fattura"), 0, True)
      tm_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568309375000, "Totale documento"), oApp.FormatImporti, 0)
      tm_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568337968750, "Cod. destinaz."), "0", 9, 0, 999999999)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568358125000, "Descr. destinaz."), 0, True)
      tm_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568410937500, "Cod. pagamento"), tabpaga)
      xx_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568428281250, "Descr. pagamento"), 0, True)
      tm_vistato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589568439375000, "Vistato"), "S", "N")
      tm_codagen.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130003812770637458, "Agente"), tabcage)
      xx_codagen.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812770793713, "Descr. agente"), 0, True)
      tm_codagen2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130003812770949955, "Agente 2"), tabcage)
      xx_codagen2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812771106206, "Descr. agente 2"), 0, True)
      tm_codese.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130003812771262465, "Cod. esenz. IVA"), tabciva)
      xx_codese.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812771418716, "Descr. esenz. IVA"), 0, True)
      tm_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812771574975, "Sc. 1"), "#,##0.00", 15)
      tm_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812771731221, "Sc. 2"), "#,##0.00", 15)
      tm_scopag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812771887476, "Sc. pag."), "#,##0.00", 15)
      tm_valuta.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130003812772043731, "Valuta"), tabvalu)
      tm_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772199986, "Cambio"), "#,##0.000000000", 15)
      tm_abi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772356241, "ABI"), "0", 9, 0, 999999999)
      tm_cab.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772512496, "CAB"), "0", 9, 0, 999999999)
      tm_banc1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812772668751, "Banca"), 0, True)
      tm_banc2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812772825019, "Filiale"), 0, True)
      tm_codbanc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772981274, "Cod. ns. banca"), "0", 4, 0, 9999)
      xx_codbanc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812773137534, "Descr. ns. banca"), 0, True)
      tm_scorpo.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130003812773293806, "Scorporo"), "S", "N")
      tm_flspinc.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130003812773450048, "Add. spese inc."), "S", "N")
      tm_flbolli.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130003812773606311, "Add. bolli"), "S", "N")
      tm_pagato.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812773762566, "Pagato"), oApp.FormatImporti, 15)
      tm_pagato2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812773918817, "Pagato 2"), oApp.FormatImporti, 15)
      tm_contfatt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812774075068, "Conto fatturaz."), "0", 9, 0, 999999999)
      xx_contfatt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130003812774231327, "Descr. conto fatturaz."), 0, True)
      tm_fattsep.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130003818360234282, "Fatturare a parte"), "S", "N")
      tm_dianno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772356242, "Anno dichiarazione"), "0", 4, 0, 2099)
      tm_dinumero.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003812772512497, "Numero dichiarazione"), "0", 9, 0, 999999999)

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

  Private Sub InitializeComponent()
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdDelete = New NTSInformatica.NTSButton
    Me.cmdDeseleziona = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grDdts = New NTSInformatica.NTSGrid
    Me.grvDdts = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.tm_tipork = New NTSInformatica.NTSGridColumn
    Me.tm_anno = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tm_totdoc = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.tm_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_codtpbf = New NTSInformatica.NTSGridColumn
    Me.tm_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.tm_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.tm_vistato = New NTSInformatica.NTSGridColumn
    Me.tm_codagen = New NTSInformatica.NTSGridColumn
    Me.xx_codagen = New NTSInformatica.NTSGridColumn
    Me.tm_codagen2 = New NTSInformatica.NTSGridColumn
    Me.xx_codagen2 = New NTSInformatica.NTSGridColumn
    Me.tm_codese = New NTSInformatica.NTSGridColumn
    Me.xx_codese = New NTSInformatica.NTSGridColumn
    Me.tm_scont1 = New NTSInformatica.NTSGridColumn
    Me.tm_scont2 = New NTSInformatica.NTSGridColumn
    Me.tm_scopag = New NTSInformatica.NTSGridColumn
    Me.tm_valuta = New NTSInformatica.NTSGridColumn
    Me.tm_cambio = New NTSInformatica.NTSGridColumn
    Me.tm_abi = New NTSInformatica.NTSGridColumn
    Me.tm_cab = New NTSInformatica.NTSGridColumn
    Me.tm_banc1 = New NTSInformatica.NTSGridColumn
    Me.tm_banc2 = New NTSInformatica.NTSGridColumn
    Me.tm_codbanc = New NTSInformatica.NTSGridColumn
    Me.xx_codbanc = New NTSInformatica.NTSGridColumn
    Me.tm_scorpo = New NTSInformatica.NTSGridColumn
    Me.tm_flspinc = New NTSInformatica.NTSGridColumn
    Me.tm_flbolli = New NTSInformatica.NTSGridColumn
    Me.tm_pagato = New NTSInformatica.NTSGridColumn
    Me.tm_pagato2 = New NTSInformatica.NTSGridColumn
    Me.tm_contfatt = New NTSInformatica.NTSGridColumn
    Me.xx_contfatt = New NTSInformatica.NTSGridColumn
    Me.tm_fattsep = New NTSInformatica.NTSGridColumn
    Me.tm_dianno = New NTSInformatica.NTSGridColumn
    Me.tm_dinumero = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grDdts, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDdts, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdDelete)
    Me.pnButton.Controls.Add(Me.cmdDeseleziona)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdSeleziona)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(598, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.NTSActiveTrasparency = True
    Me.pnButton.Size = New System.Drawing.Size(113, 316)
    Me.pnButton.TabIndex = 2
    Me.pnButton.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 38)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 24)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdDelete
    '
    Me.cmdDelete.ImagePath = ""
    Me.cmdDelete.ImageText = ""
    Me.cmdDelete.Location = New System.Drawing.Point(6, 163)
    Me.cmdDelete.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.NTSContextMenu = Nothing
    Me.cmdDelete.Size = New System.Drawing.Size(98, 24)
    Me.cmdDelete.TabIndex = 13
    Me.cmdDelete.Text = "Cancella righe"
    '
    'cmdDeseleziona
    '
    Me.cmdDeseleziona.ImagePath = ""
    Me.cmdDeseleziona.ImageText = ""
    Me.cmdDeseleziona.Location = New System.Drawing.Point(6, 112)
    Me.cmdDeseleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdDeseleziona.Name = "cmdDeseleziona"
    Me.cmdDeseleziona.NTSContextMenu = Nothing
    Me.cmdDeseleziona.Size = New System.Drawing.Size(98, 24)
    Me.cmdDeseleziona.TabIndex = 12
    Me.cmdDeseleziona.Text = "&Deseleziona tutto"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 10)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(98, 24)
    Me.cmdConferma.TabIndex = 11
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 85)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(98, 24)
    Me.cmdSeleziona.TabIndex = 8
    Me.cmdSeleziona.Text = "&Seleziona tutto"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grDdts)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(598, 316)
    Me.pnGrid.TabIndex = 3
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grDdts
    '
    Me.grDdts.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDdts.EmbeddedNavigator.Name = ""
    Me.grDdts.Location = New System.Drawing.Point(0, 0)
    Me.grDdts.MainView = Me.grvDdts
    Me.grDdts.Name = "grDdts"
    Me.grDdts.Size = New System.Drawing.Size(598, 316)
    Me.grDdts.TabIndex = 0
    Me.grDdts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDdts})
    '
    'grvDdts
    '
    Me.grvDdts.ActiveFilterEnabled = False
    Me.grvDdts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.tm_tipork, Me.tm_anno, Me.tm_serie, Me.tm_numdoc, Me.tm_datdoc, Me.tm_conto, Me.xx_conto, Me.tm_totdoc, Me.tm_riferim, Me.tm_tipobf, Me.xx_codtpbf, Me.tm_coddest, Me.xx_coddest, Me.tm_codpaga, Me.xx_codpaga, Me.tm_vistato, Me.tm_codagen, Me.xx_codagen, Me.tm_codagen2, Me.xx_codagen2, Me.tm_codese, Me.xx_codese, Me.tm_scont1, Me.tm_scont2, Me.tm_scopag, Me.tm_valuta, Me.tm_cambio, Me.tm_abi, Me.tm_cab, Me.tm_banc1, Me.tm_banc2, Me.tm_codbanc, Me.xx_codbanc, Me.tm_scorpo, Me.tm_flspinc, Me.tm_flbolli, Me.tm_pagato, Me.tm_pagato2, Me.tm_contfatt, Me.xx_contfatt, Me.tm_fattsep, Me.tm_dianno, Me.tm_dinumero})
    Me.grvDdts.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvDdts.Enabled = True
    Me.grvDdts.GridControl = Me.grDdts
    Me.grvDdts.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDdts.MinRowHeight = 14
    Me.grvDdts.Name = "grvDdts"
    Me.grvDdts.NTSAllowDelete = True
    Me.grvDdts.NTSAllowInsert = True
    Me.grvDdts.NTSAllowUpdate = True
    Me.grvDdts.NTSMenuContext = Nothing
    Me.grvDdts.OptionsCustomization.AllowRowSizing = True
    Me.grvDdts.OptionsFilter.AllowFilterEditor = False
    Me.grvDdts.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDdts.OptionsNavigation.UseTabKey = False
    Me.grvDdts.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDdts.OptionsView.ColumnAutoWidth = False
    Me.grvDdts.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDdts.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDdts.OptionsView.ShowGroupPanel = False
    Me.grvDdts.RowHeight = 14
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    '
    'tm_tipork
    '
    Me.tm_tipork.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipork.Caption = "Tipo"
    Me.tm_tipork.Enabled = False
    Me.tm_tipork.FieldName = "tm_tipork"
    Me.tm_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipork.Name = "tm_tipork"
    Me.tm_tipork.NTSRepositoryComboBox = Nothing
    Me.tm_tipork.NTSRepositoryItemCheck = Nothing
    Me.tm_tipork.NTSRepositoryItemMemo = Nothing
    Me.tm_tipork.NTSRepositoryItemText = Nothing
    Me.tm_tipork.OptionsColumn.AllowEdit = False
    Me.tm_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipork.OptionsColumn.ReadOnly = True
    Me.tm_tipork.OptionsFilter.AllowFilter = False
    Me.tm_tipork.Visible = True
    Me.tm_tipork.VisibleIndex = 1
    '
    'tm_anno
    '
    Me.tm_anno.AppearanceCell.Options.UseBackColor = True
    Me.tm_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tm_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_anno.Caption = "Anno"
    Me.tm_anno.Enabled = False
    Me.tm_anno.FieldName = "tm_anno"
    Me.tm_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_anno.Name = "tm_anno"
    Me.tm_anno.NTSRepositoryComboBox = Nothing
    Me.tm_anno.NTSRepositoryItemCheck = Nothing
    Me.tm_anno.NTSRepositoryItemMemo = Nothing
    Me.tm_anno.NTSRepositoryItemText = Nothing
    Me.tm_anno.OptionsColumn.AllowEdit = False
    Me.tm_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_anno.OptionsColumn.ReadOnly = True
    Me.tm_anno.OptionsFilter.AllowFilter = False
    Me.tm_anno.Visible = True
    Me.tm_anno.VisibleIndex = 2
    '
    'tm_serie
    '
    Me.tm_serie.AppearanceCell.Options.UseBackColor = True
    Me.tm_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tm_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_serie.Caption = "Serie"
    Me.tm_serie.Enabled = False
    Me.tm_serie.FieldName = "tm_serie"
    Me.tm_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_serie.Name = "tm_serie"
    Me.tm_serie.NTSRepositoryComboBox = Nothing
    Me.tm_serie.NTSRepositoryItemCheck = Nothing
    Me.tm_serie.NTSRepositoryItemMemo = Nothing
    Me.tm_serie.NTSRepositoryItemText = Nothing
    Me.tm_serie.OptionsColumn.AllowEdit = False
    Me.tm_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_serie.OptionsColumn.ReadOnly = True
    Me.tm_serie.OptionsFilter.AllowFilter = False
    Me.tm_serie.Visible = True
    Me.tm_serie.VisibleIndex = 3
    '
    'tm_numdoc
    '
    Me.tm_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numdoc.Caption = "Numero"
    Me.tm_numdoc.Enabled = False
    Me.tm_numdoc.FieldName = "tm_numdoc"
    Me.tm_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numdoc.Name = "tm_numdoc"
    Me.tm_numdoc.NTSRepositoryComboBox = Nothing
    Me.tm_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_numdoc.NTSRepositoryItemText = Nothing
    Me.tm_numdoc.OptionsColumn.AllowEdit = False
    Me.tm_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numdoc.OptionsColumn.ReadOnly = True
    Me.tm_numdoc.OptionsFilter.AllowFilter = False
    Me.tm_numdoc.Visible = True
    Me.tm_numdoc.VisibleIndex = 4
    '
    'tm_datdoc
    '
    Me.tm_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datdoc.Caption = "Data doc."
    Me.tm_datdoc.Enabled = False
    Me.tm_datdoc.FieldName = "tm_datdoc"
    Me.tm_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datdoc.Name = "tm_datdoc"
    Me.tm_datdoc.NTSRepositoryComboBox = Nothing
    Me.tm_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_datdoc.NTSRepositoryItemText = Nothing
    Me.tm_datdoc.OptionsColumn.AllowEdit = False
    Me.tm_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datdoc.OptionsColumn.ReadOnly = True
    Me.tm_datdoc.OptionsFilter.AllowFilter = False
    Me.tm_datdoc.Visible = True
    Me.tm_datdoc.VisibleIndex = 5
    '
    'tm_conto
    '
    Me.tm_conto.AppearanceCell.Options.UseBackColor = True
    Me.tm_conto.AppearanceCell.Options.UseTextOptions = True
    Me.tm_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_conto.Caption = "Conto"
    Me.tm_conto.Enabled = False
    Me.tm_conto.FieldName = "tm_conto"
    Me.tm_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_conto.Name = "tm_conto"
    Me.tm_conto.NTSRepositoryComboBox = Nothing
    Me.tm_conto.NTSRepositoryItemCheck = Nothing
    Me.tm_conto.NTSRepositoryItemMemo = Nothing
    Me.tm_conto.NTSRepositoryItemText = Nothing
    Me.tm_conto.OptionsColumn.AllowEdit = False
    Me.tm_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_conto.OptionsColumn.ReadOnly = True
    Me.tm_conto.OptionsFilter.AllowFilter = False
    Me.tm_conto.Visible = True
    Me.tm_conto.VisibleIndex = 6
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. Conto"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 7
    '
    'tm_totdoc
    '
    Me.tm_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totdoc.Caption = "Totale documento"
    Me.tm_totdoc.Enabled = False
    Me.tm_totdoc.FieldName = "tm_totdoc"
    Me.tm_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totdoc.Name = "tm_totdoc"
    Me.tm_totdoc.NTSRepositoryComboBox = Nothing
    Me.tm_totdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_totdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_totdoc.NTSRepositoryItemText = Nothing
    Me.tm_totdoc.OptionsColumn.AllowEdit = False
    Me.tm_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totdoc.OptionsColumn.ReadOnly = True
    Me.tm_totdoc.OptionsFilter.AllowFilter = False
    Me.tm_totdoc.Visible = True
    Me.tm_totdoc.VisibleIndex = 8
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = False
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowEdit = False
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsColumn.ReadOnly = True
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 9
    '
    'tm_tipobf
    '
    Me.tm_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipobf.Caption = "Tipo BF"
    Me.tm_tipobf.Enabled = False
    Me.tm_tipobf.FieldName = "tm_tipobf"
    Me.tm_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipobf.Name = "tm_tipobf"
    Me.tm_tipobf.NTSRepositoryComboBox = Nothing
    Me.tm_tipobf.NTSRepositoryItemCheck = Nothing
    Me.tm_tipobf.NTSRepositoryItemMemo = Nothing
    Me.tm_tipobf.NTSRepositoryItemText = Nothing
    Me.tm_tipobf.OptionsColumn.AllowEdit = False
    Me.tm_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipobf.OptionsColumn.ReadOnly = True
    Me.tm_tipobf.OptionsFilter.AllowFilter = False
    Me.tm_tipobf.Visible = True
    Me.tm_tipobf.VisibleIndex = 10
    '
    'xx_codtpbf
    '
    Me.xx_codtpbf.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpbf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpbf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtpbf.Caption = "Descr. tipo BF"
    Me.xx_codtpbf.Enabled = False
    Me.xx_codtpbf.FieldName = "xx_codtpbf"
    Me.xx_codtpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpbf.Name = "xx_codtpbf"
    Me.xx_codtpbf.NTSRepositoryComboBox = Nothing
    Me.xx_codtpbf.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpbf.NTSRepositoryItemMemo = Nothing
    Me.xx_codtpbf.NTSRepositoryItemText = Nothing
    Me.xx_codtpbf.OptionsColumn.AllowEdit = False
    Me.xx_codtpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpbf.OptionsColumn.ReadOnly = True
    Me.xx_codtpbf.OptionsFilter.AllowFilter = False
    Me.xx_codtpbf.Visible = True
    Me.xx_codtpbf.VisibleIndex = 11
    '
    'tm_coddest
    '
    Me.tm_coddest.AppearanceCell.Options.UseBackColor = True
    Me.tm_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.tm_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_coddest.Caption = "Cod. destinaz."
    Me.tm_coddest.Enabled = False
    Me.tm_coddest.FieldName = "tm_coddest"
    Me.tm_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_coddest.Name = "tm_coddest"
    Me.tm_coddest.NTSRepositoryComboBox = Nothing
    Me.tm_coddest.NTSRepositoryItemCheck = Nothing
    Me.tm_coddest.NTSRepositoryItemMemo = Nothing
    Me.tm_coddest.NTSRepositoryItemText = Nothing
    Me.tm_coddest.OptionsColumn.AllowEdit = False
    Me.tm_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_coddest.OptionsColumn.ReadOnly = True
    Me.tm_coddest.OptionsFilter.AllowFilter = False
    Me.tm_coddest.Visible = True
    Me.tm_coddest.VisibleIndex = 12
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destinaz."
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 13
    '
    'tm_codpaga
    '
    Me.tm_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.tm_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codpaga.Caption = "Cod. pagamento"
    Me.tm_codpaga.Enabled = False
    Me.tm_codpaga.FieldName = "tm_codpaga"
    Me.tm_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codpaga.Name = "tm_codpaga"
    Me.tm_codpaga.NTSRepositoryComboBox = Nothing
    Me.tm_codpaga.NTSRepositoryItemCheck = Nothing
    Me.tm_codpaga.NTSRepositoryItemMemo = Nothing
    Me.tm_codpaga.NTSRepositoryItemText = Nothing
    Me.tm_codpaga.OptionsColumn.AllowEdit = False
    Me.tm_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codpaga.OptionsColumn.ReadOnly = True
    Me.tm_codpaga.OptionsFilter.AllowFilter = False
    Me.tm_codpaga.Visible = True
    Me.tm_codpaga.VisibleIndex = 14
    '
    'xx_codpaga
    '
    Me.xx_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codpaga.Caption = "Descr. pagamento"
    Me.xx_codpaga.Enabled = False
    Me.xx_codpaga.FieldName = "xx_codpaga"
    Me.xx_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpaga.Name = "xx_codpaga"
    Me.xx_codpaga.NTSRepositoryComboBox = Nothing
    Me.xx_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codpaga.NTSRepositoryItemMemo = Nothing
    Me.xx_codpaga.NTSRepositoryItemText = Nothing
    Me.xx_codpaga.OptionsColumn.AllowEdit = False
    Me.xx_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpaga.OptionsColumn.ReadOnly = True
    Me.xx_codpaga.OptionsFilter.AllowFilter = False
    Me.xx_codpaga.Visible = True
    Me.xx_codpaga.VisibleIndex = 15
    '
    'tm_vistato
    '
    Me.tm_vistato.AppearanceCell.Options.UseBackColor = True
    Me.tm_vistato.AppearanceCell.Options.UseTextOptions = True
    Me.tm_vistato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_vistato.Caption = "Confermato"
    Me.tm_vistato.Enabled = False
    Me.tm_vistato.FieldName = "tm_vistato"
    Me.tm_vistato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_vistato.Name = "tm_vistato"
    Me.tm_vistato.NTSRepositoryComboBox = Nothing
    Me.tm_vistato.NTSRepositoryItemCheck = Nothing
    Me.tm_vistato.NTSRepositoryItemMemo = Nothing
    Me.tm_vistato.NTSRepositoryItemText = Nothing
    Me.tm_vistato.OptionsColumn.AllowEdit = False
    Me.tm_vistato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_vistato.OptionsColumn.ReadOnly = True
    Me.tm_vistato.OptionsFilter.AllowFilter = False
    Me.tm_vistato.Visible = True
    Me.tm_vistato.VisibleIndex = 16
    '
    'tm_codagen
    '
    Me.tm_codagen.AppearanceCell.Options.UseBackColor = True
    Me.tm_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codagen.Caption = "Agente"
    Me.tm_codagen.Enabled = False
    Me.tm_codagen.FieldName = "tm_codagen"
    Me.tm_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codagen.Name = "tm_codagen"
    Me.tm_codagen.NTSRepositoryComboBox = Nothing
    Me.tm_codagen.NTSRepositoryItemCheck = Nothing
    Me.tm_codagen.NTSRepositoryItemMemo = Nothing
    Me.tm_codagen.NTSRepositoryItemText = Nothing
    Me.tm_codagen.OptionsColumn.AllowEdit = False
    Me.tm_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codagen.OptionsColumn.ReadOnly = True
    Me.tm_codagen.OptionsFilter.AllowFilter = False
    Me.tm_codagen.Visible = True
    Me.tm_codagen.VisibleIndex = 17
    '
    'xx_codagen
    '
    Me.xx_codagen.AppearanceCell.Options.UseBackColor = True
    Me.xx_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codagen.Caption = "Descr. agente"
    Me.xx_codagen.Enabled = False
    Me.xx_codagen.FieldName = "xx_codagen"
    Me.xx_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codagen.Name = "xx_codagen"
    Me.xx_codagen.NTSRepositoryComboBox = Nothing
    Me.xx_codagen.NTSRepositoryItemCheck = Nothing
    Me.xx_codagen.NTSRepositoryItemMemo = Nothing
    Me.xx_codagen.NTSRepositoryItemText = Nothing
    Me.xx_codagen.OptionsColumn.AllowEdit = False
    Me.xx_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codagen.OptionsColumn.ReadOnly = True
    Me.xx_codagen.OptionsFilter.AllowFilter = False
    Me.xx_codagen.Visible = True
    Me.xx_codagen.VisibleIndex = 18
    '
    'tm_codagen2
    '
    Me.tm_codagen2.AppearanceCell.Options.UseBackColor = True
    Me.tm_codagen2.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codagen2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codagen2.Caption = "Agente 2"
    Me.tm_codagen2.Enabled = False
    Me.tm_codagen2.FieldName = "tm_codagen2"
    Me.tm_codagen2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codagen2.Name = "tm_codagen2"
    Me.tm_codagen2.NTSRepositoryComboBox = Nothing
    Me.tm_codagen2.NTSRepositoryItemCheck = Nothing
    Me.tm_codagen2.NTSRepositoryItemMemo = Nothing
    Me.tm_codagen2.NTSRepositoryItemText = Nothing
    Me.tm_codagen2.OptionsColumn.AllowEdit = False
    Me.tm_codagen2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codagen2.OptionsColumn.ReadOnly = True
    Me.tm_codagen2.OptionsFilter.AllowFilter = False
    Me.tm_codagen2.Visible = True
    Me.tm_codagen2.VisibleIndex = 19
    '
    'xx_codagen2
    '
    Me.xx_codagen2.AppearanceCell.Options.UseBackColor = True
    Me.xx_codagen2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codagen2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codagen2.Caption = "Descr. agente 2"
    Me.xx_codagen2.Enabled = False
    Me.xx_codagen2.FieldName = "xx_codagen2"
    Me.xx_codagen2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codagen2.Name = "xx_codagen2"
    Me.xx_codagen2.NTSRepositoryComboBox = Nothing
    Me.xx_codagen2.NTSRepositoryItemCheck = Nothing
    Me.xx_codagen2.NTSRepositoryItemMemo = Nothing
    Me.xx_codagen2.NTSRepositoryItemText = Nothing
    Me.xx_codagen2.OptionsColumn.AllowEdit = False
    Me.xx_codagen2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codagen2.OptionsColumn.ReadOnly = True
    Me.xx_codagen2.OptionsFilter.AllowFilter = False
    Me.xx_codagen2.Visible = True
    Me.xx_codagen2.VisibleIndex = 20
    '
    'tm_codese
    '
    Me.tm_codese.AppearanceCell.Options.UseBackColor = True
    Me.tm_codese.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codese.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codese.Caption = "Cod. esenz. IVA"
    Me.tm_codese.Enabled = False
    Me.tm_codese.FieldName = "tm_codese"
    Me.tm_codese.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codese.Name = "tm_codese"
    Me.tm_codese.NTSRepositoryComboBox = Nothing
    Me.tm_codese.NTSRepositoryItemCheck = Nothing
    Me.tm_codese.NTSRepositoryItemMemo = Nothing
    Me.tm_codese.NTSRepositoryItemText = Nothing
    Me.tm_codese.OptionsColumn.AllowEdit = False
    Me.tm_codese.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codese.OptionsColumn.ReadOnly = True
    Me.tm_codese.OptionsFilter.AllowFilter = False
    Me.tm_codese.Visible = True
    Me.tm_codese.VisibleIndex = 21
    '
    'xx_codese
    '
    Me.xx_codese.AppearanceCell.Options.UseBackColor = True
    Me.xx_codese.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codese.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codese.Caption = "Descr. esenz. IVA"
    Me.xx_codese.Enabled = False
    Me.xx_codese.FieldName = "xx_codese"
    Me.xx_codese.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codese.Name = "xx_codese"
    Me.xx_codese.NTSRepositoryComboBox = Nothing
    Me.xx_codese.NTSRepositoryItemCheck = Nothing
    Me.xx_codese.NTSRepositoryItemMemo = Nothing
    Me.xx_codese.NTSRepositoryItemText = Nothing
    Me.xx_codese.OptionsColumn.AllowEdit = False
    Me.xx_codese.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codese.OptionsColumn.ReadOnly = True
    Me.xx_codese.OptionsFilter.AllowFilter = False
    Me.xx_codese.Visible = True
    Me.xx_codese.VisibleIndex = 22
    '
    'tm_scont1
    '
    Me.tm_scont1.AppearanceCell.Options.UseBackColor = True
    Me.tm_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.tm_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_scont1.Caption = "Sc. 1"
    Me.tm_scont1.Enabled = False
    Me.tm_scont1.FieldName = "tm_scont1"
    Me.tm_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_scont1.Name = "tm_scont1"
    Me.tm_scont1.NTSRepositoryComboBox = Nothing
    Me.tm_scont1.NTSRepositoryItemCheck = Nothing
    Me.tm_scont1.NTSRepositoryItemMemo = Nothing
    Me.tm_scont1.NTSRepositoryItemText = Nothing
    Me.tm_scont1.OptionsColumn.AllowEdit = False
    Me.tm_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_scont1.OptionsColumn.ReadOnly = True
    Me.tm_scont1.OptionsFilter.AllowFilter = False
    Me.tm_scont1.Visible = True
    Me.tm_scont1.VisibleIndex = 23
    '
    'tm_scont2
    '
    Me.tm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.tm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.tm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_scont2.Caption = "Sc. 2"
    Me.tm_scont2.Enabled = False
    Me.tm_scont2.FieldName = "tm_scont2"
    Me.tm_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_scont2.Name = "tm_scont2"
    Me.tm_scont2.NTSRepositoryComboBox = Nothing
    Me.tm_scont2.NTSRepositoryItemCheck = Nothing
    Me.tm_scont2.NTSRepositoryItemMemo = Nothing
    Me.tm_scont2.NTSRepositoryItemText = Nothing
    Me.tm_scont2.OptionsColumn.AllowEdit = False
    Me.tm_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_scont2.OptionsColumn.ReadOnly = True
    Me.tm_scont2.OptionsFilter.AllowFilter = False
    Me.tm_scont2.Visible = True
    Me.tm_scont2.VisibleIndex = 24
    '
    'tm_scopag
    '
    Me.tm_scopag.AppearanceCell.Options.UseBackColor = True
    Me.tm_scopag.AppearanceCell.Options.UseTextOptions = True
    Me.tm_scopag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_scopag.Caption = "Sc. pag."
    Me.tm_scopag.Enabled = False
    Me.tm_scopag.FieldName = "tm_scopag"
    Me.tm_scopag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_scopag.Name = "tm_scopag"
    Me.tm_scopag.NTSRepositoryComboBox = Nothing
    Me.tm_scopag.NTSRepositoryItemCheck = Nothing
    Me.tm_scopag.NTSRepositoryItemMemo = Nothing
    Me.tm_scopag.NTSRepositoryItemText = Nothing
    Me.tm_scopag.OptionsColumn.AllowEdit = False
    Me.tm_scopag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_scopag.OptionsColumn.ReadOnly = True
    Me.tm_scopag.OptionsFilter.AllowFilter = False
    Me.tm_scopag.Visible = True
    Me.tm_scopag.VisibleIndex = 25
    '
    'tm_valuta
    '
    Me.tm_valuta.AppearanceCell.Options.UseBackColor = True
    Me.tm_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.tm_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_valuta.Caption = "Valuta"
    Me.tm_valuta.Enabled = False
    Me.tm_valuta.FieldName = "tm_valuta"
    Me.tm_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_valuta.Name = "tm_valuta"
    Me.tm_valuta.NTSRepositoryComboBox = Nothing
    Me.tm_valuta.NTSRepositoryItemCheck = Nothing
    Me.tm_valuta.NTSRepositoryItemMemo = Nothing
    Me.tm_valuta.NTSRepositoryItemText = Nothing
    Me.tm_valuta.OptionsColumn.AllowEdit = False
    Me.tm_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_valuta.OptionsColumn.ReadOnly = True
    Me.tm_valuta.OptionsFilter.AllowFilter = False
    Me.tm_valuta.Visible = True
    Me.tm_valuta.VisibleIndex = 26
    '
    'tm_cambio
    '
    Me.tm_cambio.AppearanceCell.Options.UseBackColor = True
    Me.tm_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.tm_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_cambio.Caption = "Cambio"
    Me.tm_cambio.Enabled = False
    Me.tm_cambio.FieldName = "tm_cambio"
    Me.tm_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_cambio.Name = "tm_cambio"
    Me.tm_cambio.NTSRepositoryComboBox = Nothing
    Me.tm_cambio.NTSRepositoryItemCheck = Nothing
    Me.tm_cambio.NTSRepositoryItemMemo = Nothing
    Me.tm_cambio.NTSRepositoryItemText = Nothing
    Me.tm_cambio.OptionsColumn.AllowEdit = False
    Me.tm_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_cambio.OptionsColumn.ReadOnly = True
    Me.tm_cambio.OptionsFilter.AllowFilter = False
    Me.tm_cambio.Visible = True
    Me.tm_cambio.VisibleIndex = 27
    '
    'tm_abi
    '
    Me.tm_abi.AppearanceCell.Options.UseBackColor = True
    Me.tm_abi.AppearanceCell.Options.UseTextOptions = True
    Me.tm_abi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_abi.Caption = "ABI"
    Me.tm_abi.Enabled = False
    Me.tm_abi.FieldName = "tm_abi"
    Me.tm_abi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_abi.Name = "tm_abi"
    Me.tm_abi.NTSRepositoryComboBox = Nothing
    Me.tm_abi.NTSRepositoryItemCheck = Nothing
    Me.tm_abi.NTSRepositoryItemMemo = Nothing
    Me.tm_abi.NTSRepositoryItemText = Nothing
    Me.tm_abi.OptionsColumn.AllowEdit = False
    Me.tm_abi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_abi.OptionsColumn.ReadOnly = True
    Me.tm_abi.OptionsFilter.AllowFilter = False
    Me.tm_abi.Visible = True
    Me.tm_abi.VisibleIndex = 28
    '
    'tm_cab
    '
    Me.tm_cab.AppearanceCell.Options.UseBackColor = True
    Me.tm_cab.AppearanceCell.Options.UseTextOptions = True
    Me.tm_cab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_cab.Caption = "CAB"
    Me.tm_cab.Enabled = False
    Me.tm_cab.FieldName = "tm_cab"
    Me.tm_cab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_cab.Name = "tm_cab"
    Me.tm_cab.NTSRepositoryComboBox = Nothing
    Me.tm_cab.NTSRepositoryItemCheck = Nothing
    Me.tm_cab.NTSRepositoryItemMemo = Nothing
    Me.tm_cab.NTSRepositoryItemText = Nothing
    Me.tm_cab.OptionsColumn.AllowEdit = False
    Me.tm_cab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_cab.OptionsColumn.ReadOnly = True
    Me.tm_cab.OptionsFilter.AllowFilter = False
    Me.tm_cab.Visible = True
    Me.tm_cab.VisibleIndex = 29
    '
    'tm_banc1
    '
    Me.tm_banc1.AppearanceCell.Options.UseBackColor = True
    Me.tm_banc1.AppearanceCell.Options.UseTextOptions = True
    Me.tm_banc1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_banc1.Caption = "Banca"
    Me.tm_banc1.Enabled = False
    Me.tm_banc1.FieldName = "tm_banc1"
    Me.tm_banc1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_banc1.Name = "tm_banc1"
    Me.tm_banc1.NTSRepositoryComboBox = Nothing
    Me.tm_banc1.NTSRepositoryItemCheck = Nothing
    Me.tm_banc1.NTSRepositoryItemMemo = Nothing
    Me.tm_banc1.NTSRepositoryItemText = Nothing
    Me.tm_banc1.OptionsColumn.AllowEdit = False
    Me.tm_banc1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_banc1.OptionsColumn.ReadOnly = True
    Me.tm_banc1.OptionsFilter.AllowFilter = False
    Me.tm_banc1.Visible = True
    Me.tm_banc1.VisibleIndex = 30
    '
    'tm_banc2
    '
    Me.tm_banc2.AppearanceCell.Options.UseBackColor = True
    Me.tm_banc2.AppearanceCell.Options.UseTextOptions = True
    Me.tm_banc2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_banc2.Caption = "Filiale"
    Me.tm_banc2.Enabled = False
    Me.tm_banc2.FieldName = "tm_banc2"
    Me.tm_banc2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_banc2.Name = "tm_banc2"
    Me.tm_banc2.NTSRepositoryComboBox = Nothing
    Me.tm_banc2.NTSRepositoryItemCheck = Nothing
    Me.tm_banc2.NTSRepositoryItemMemo = Nothing
    Me.tm_banc2.NTSRepositoryItemText = Nothing
    Me.tm_banc2.OptionsColumn.AllowEdit = False
    Me.tm_banc2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_banc2.OptionsColumn.ReadOnly = True
    Me.tm_banc2.OptionsFilter.AllowFilter = False
    Me.tm_banc2.Visible = True
    Me.tm_banc2.VisibleIndex = 31
    '
    'tm_codbanc
    '
    Me.tm_codbanc.AppearanceCell.Options.UseBackColor = True
    Me.tm_codbanc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codbanc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codbanc.Caption = "Cod. ns. banca"
    Me.tm_codbanc.Enabled = False
    Me.tm_codbanc.FieldName = "tm_codbanc"
    Me.tm_codbanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codbanc.Name = "tm_codbanc"
    Me.tm_codbanc.NTSRepositoryComboBox = Nothing
    Me.tm_codbanc.NTSRepositoryItemCheck = Nothing
    Me.tm_codbanc.NTSRepositoryItemMemo = Nothing
    Me.tm_codbanc.NTSRepositoryItemText = Nothing
    Me.tm_codbanc.OptionsColumn.AllowEdit = False
    Me.tm_codbanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codbanc.OptionsColumn.ReadOnly = True
    Me.tm_codbanc.OptionsFilter.AllowFilter = False
    Me.tm_codbanc.Visible = True
    Me.tm_codbanc.VisibleIndex = 32
    '
    'xx_codbanc
    '
    Me.xx_codbanc.AppearanceCell.Options.UseBackColor = True
    Me.xx_codbanc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codbanc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codbanc.Caption = "Descr. ns. banca"
    Me.xx_codbanc.Enabled = False
    Me.xx_codbanc.FieldName = "xx_codbanc"
    Me.xx_codbanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codbanc.Name = "xx_codbanc"
    Me.xx_codbanc.NTSRepositoryComboBox = Nothing
    Me.xx_codbanc.NTSRepositoryItemCheck = Nothing
    Me.xx_codbanc.NTSRepositoryItemMemo = Nothing
    Me.xx_codbanc.NTSRepositoryItemText = Nothing
    Me.xx_codbanc.OptionsColumn.AllowEdit = False
    Me.xx_codbanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codbanc.OptionsColumn.ReadOnly = True
    Me.xx_codbanc.OptionsFilter.AllowFilter = False
    Me.xx_codbanc.Visible = True
    Me.xx_codbanc.VisibleIndex = 33
    '
    'tm_scorpo
    '
    Me.tm_scorpo.AppearanceCell.Options.UseBackColor = True
    Me.tm_scorpo.AppearanceCell.Options.UseTextOptions = True
    Me.tm_scorpo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_scorpo.Caption = "Scorporo"
    Me.tm_scorpo.Enabled = False
    Me.tm_scorpo.FieldName = "tm_scorpo"
    Me.tm_scorpo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_scorpo.Name = "tm_scorpo"
    Me.tm_scorpo.NTSRepositoryComboBox = Nothing
    Me.tm_scorpo.NTSRepositoryItemCheck = Nothing
    Me.tm_scorpo.NTSRepositoryItemMemo = Nothing
    Me.tm_scorpo.NTSRepositoryItemText = Nothing
    Me.tm_scorpo.OptionsColumn.AllowEdit = False
    Me.tm_scorpo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_scorpo.OptionsColumn.ReadOnly = True
    Me.tm_scorpo.OptionsFilter.AllowFilter = False
    Me.tm_scorpo.Visible = True
    Me.tm_scorpo.VisibleIndex = 34
    '
    'tm_flspinc
    '
    Me.tm_flspinc.AppearanceCell.Options.UseBackColor = True
    Me.tm_flspinc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_flspinc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_flspinc.Caption = "Add. spese inc."
    Me.tm_flspinc.Enabled = False
    Me.tm_flspinc.FieldName = "tm_flspinc"
    Me.tm_flspinc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_flspinc.Name = "tm_flspinc"
    Me.tm_flspinc.NTSRepositoryComboBox = Nothing
    Me.tm_flspinc.NTSRepositoryItemCheck = Nothing
    Me.tm_flspinc.NTSRepositoryItemMemo = Nothing
    Me.tm_flspinc.NTSRepositoryItemText = Nothing
    Me.tm_flspinc.OptionsColumn.AllowEdit = False
    Me.tm_flspinc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_flspinc.OptionsColumn.ReadOnly = True
    Me.tm_flspinc.OptionsFilter.AllowFilter = False
    Me.tm_flspinc.Visible = True
    Me.tm_flspinc.VisibleIndex = 35
    '
    'tm_flbolli
    '
    Me.tm_flbolli.AppearanceCell.Options.UseBackColor = True
    Me.tm_flbolli.AppearanceCell.Options.UseTextOptions = True
    Me.tm_flbolli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_flbolli.Caption = "Add. bolli"
    Me.tm_flbolli.Enabled = False
    Me.tm_flbolli.FieldName = "tm_flbolli"
    Me.tm_flbolli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_flbolli.Name = "tm_flbolli"
    Me.tm_flbolli.NTSRepositoryComboBox = Nothing
    Me.tm_flbolli.NTSRepositoryItemCheck = Nothing
    Me.tm_flbolli.NTSRepositoryItemMemo = Nothing
    Me.tm_flbolli.NTSRepositoryItemText = Nothing
    Me.tm_flbolli.OptionsColumn.AllowEdit = False
    Me.tm_flbolli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_flbolli.OptionsColumn.ReadOnly = True
    Me.tm_flbolli.OptionsFilter.AllowFilter = False
    Me.tm_flbolli.Visible = True
    Me.tm_flbolli.VisibleIndex = 36
    '
    'tm_pagato
    '
    Me.tm_pagato.AppearanceCell.Options.UseBackColor = True
    Me.tm_pagato.AppearanceCell.Options.UseTextOptions = True
    Me.tm_pagato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_pagato.Caption = "Pagato"
    Me.tm_pagato.Enabled = False
    Me.tm_pagato.FieldName = "tm_pagato"
    Me.tm_pagato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_pagato.Name = "tm_pagato"
    Me.tm_pagato.NTSRepositoryComboBox = Nothing
    Me.tm_pagato.NTSRepositoryItemCheck = Nothing
    Me.tm_pagato.NTSRepositoryItemMemo = Nothing
    Me.tm_pagato.NTSRepositoryItemText = Nothing
    Me.tm_pagato.OptionsColumn.AllowEdit = False
    Me.tm_pagato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_pagato.OptionsColumn.ReadOnly = True
    Me.tm_pagato.OptionsFilter.AllowFilter = False
    Me.tm_pagato.Visible = True
    Me.tm_pagato.VisibleIndex = 37
    '
    'tm_pagato2
    '
    Me.tm_pagato2.AppearanceCell.Options.UseBackColor = True
    Me.tm_pagato2.AppearanceCell.Options.UseTextOptions = True
    Me.tm_pagato2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_pagato2.Caption = "Pagato 2"
    Me.tm_pagato2.Enabled = False
    Me.tm_pagato2.FieldName = "tm_pagato2"
    Me.tm_pagato2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_pagato2.Name = "tm_pagato2"
    Me.tm_pagato2.NTSRepositoryComboBox = Nothing
    Me.tm_pagato2.NTSRepositoryItemCheck = Nothing
    Me.tm_pagato2.NTSRepositoryItemMemo = Nothing
    Me.tm_pagato2.NTSRepositoryItemText = Nothing
    Me.tm_pagato2.OptionsColumn.AllowEdit = False
    Me.tm_pagato2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_pagato2.OptionsColumn.ReadOnly = True
    Me.tm_pagato2.OptionsFilter.AllowFilter = False
    Me.tm_pagato2.Visible = True
    Me.tm_pagato2.VisibleIndex = 38
    '
    'tm_contfatt
    '
    Me.tm_contfatt.AppearanceCell.Options.UseBackColor = True
    Me.tm_contfatt.AppearanceCell.Options.UseTextOptions = True
    Me.tm_contfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_contfatt.Caption = "Conto fatturaz."
    Me.tm_contfatt.Enabled = False
    Me.tm_contfatt.FieldName = "tm_contfatt"
    Me.tm_contfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_contfatt.Name = "tm_contfatt"
    Me.tm_contfatt.NTSRepositoryComboBox = Nothing
    Me.tm_contfatt.NTSRepositoryItemCheck = Nothing
    Me.tm_contfatt.NTSRepositoryItemMemo = Nothing
    Me.tm_contfatt.NTSRepositoryItemText = Nothing
    Me.tm_contfatt.OptionsColumn.AllowEdit = False
    Me.tm_contfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_contfatt.OptionsColumn.ReadOnly = True
    Me.tm_contfatt.OptionsFilter.AllowFilter = False
    Me.tm_contfatt.Visible = True
    Me.tm_contfatt.VisibleIndex = 39
    '
    'xx_contfatt
    '
    Me.xx_contfatt.AppearanceCell.Options.UseBackColor = True
    Me.xx_contfatt.AppearanceCell.Options.UseTextOptions = True
    Me.xx_contfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_contfatt.Caption = "Descr. conto fatturaz."
    Me.xx_contfatt.Enabled = False
    Me.xx_contfatt.FieldName = "xx_contfatt"
    Me.xx_contfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_contfatt.Name = "xx_contfatt"
    Me.xx_contfatt.NTSRepositoryComboBox = Nothing
    Me.xx_contfatt.NTSRepositoryItemCheck = Nothing
    Me.xx_contfatt.NTSRepositoryItemMemo = Nothing
    Me.xx_contfatt.NTSRepositoryItemText = Nothing
    Me.xx_contfatt.OptionsColumn.AllowEdit = False
    Me.xx_contfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_contfatt.OptionsColumn.ReadOnly = True
    Me.xx_contfatt.OptionsFilter.AllowFilter = False
    Me.xx_contfatt.Visible = True
    Me.xx_contfatt.VisibleIndex = 40
    '
    'tm_fattsep
    '
    Me.tm_fattsep.AppearanceCell.Options.UseBackColor = True
    Me.tm_fattsep.AppearanceCell.Options.UseTextOptions = True
    Me.tm_fattsep.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_fattsep.Caption = "Fattura a parte"
    Me.tm_fattsep.Enabled = False
    Me.tm_fattsep.FieldName = "tm_fattsep"
    Me.tm_fattsep.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_fattsep.Name = "tm_fattsep"
    Me.tm_fattsep.NTSRepositoryComboBox = Nothing
    Me.tm_fattsep.NTSRepositoryItemCheck = Nothing
    Me.tm_fattsep.NTSRepositoryItemMemo = Nothing
    Me.tm_fattsep.NTSRepositoryItemText = Nothing
    Me.tm_fattsep.OptionsColumn.AllowEdit = False
    Me.tm_fattsep.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_fattsep.OptionsColumn.ReadOnly = True
    Me.tm_fattsep.OptionsFilter.AllowFilter = False
    Me.tm_fattsep.Visible = True
    Me.tm_fattsep.VisibleIndex = 41
    '
    'tm_dianno
    '
    Me.tm_dianno.AppearanceCell.Options.UseBackColor = True
    Me.tm_dianno.AppearanceCell.Options.UseTextOptions = True
    Me.tm_dianno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_dianno.Caption = "Anno dich."
    Me.tm_dianno.Enabled = False
    Me.tm_dianno.FieldName = "tm_dianno"
    Me.tm_dianno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_dianno.Name = "tm_dianno"
    Me.tm_dianno.NTSRepositoryComboBox = Nothing
    Me.tm_dianno.NTSRepositoryItemCheck = Nothing
    Me.tm_dianno.NTSRepositoryItemMemo = Nothing
    Me.tm_dianno.NTSRepositoryItemText = Nothing
    Me.tm_dianno.OptionsColumn.AllowEdit = False
    Me.tm_dianno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_dianno.OptionsColumn.ReadOnly = True
    Me.tm_dianno.OptionsFilter.AllowFilter = False
    Me.tm_dianno.Visible = True
    Me.tm_dianno.VisibleIndex = 42
    '
    'tm_dinumero
    '
    Me.tm_dinumero.AppearanceCell.Options.UseBackColor = True
    Me.tm_dinumero.AppearanceCell.Options.UseTextOptions = True
    Me.tm_dinumero.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_dinumero.Caption = "Numero dich."
    Me.tm_dinumero.Enabled = False
    Me.tm_dinumero.FieldName = "tm_dinumero"
    Me.tm_dinumero.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_dinumero.Name = "tm_dinumero"
    Me.tm_dinumero.NTSRepositoryComboBox = Nothing
    Me.tm_dinumero.NTSRepositoryItemCheck = Nothing
    Me.tm_dinumero.NTSRepositoryItemMemo = Nothing
    Me.tm_dinumero.NTSRepositoryItemText = Nothing
    Me.tm_dinumero.OptionsColumn.AllowEdit = False
    Me.tm_dinumero.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_dinumero.OptionsColumn.ReadOnly = True
    Me.tm_dinumero.OptionsFilter.AllowFilter = False
    Me.tm_dinumero.Visible = True
    Me.tm_dinumero.VisibleIndex = 43
    '
    'FRMVEDDTS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(711, 316)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnButton)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMVEDDTS"
    Me.Text = "ELENCO DDT DA FATTURARE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grDdts, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDdts, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub


  Public Overridable Sub FRMVEDDTS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
      GctlApplicaDefaultValue()

      dcDdts.DataSource = oCleFadi.dttDDTs
      grDdts.DataSource = dcDdts

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      bOk = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim i As Integer = 0
    Try
      For i = 0 To grvDdts.RowCount - 1
        grvDdts.SetRowCellValue(i, "xx_seleziona", "S")
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDeseleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeseleziona.Click
    Dim i As Integer = 0
    Try
      For i = 0 To grvDdts.RowCount - 1
        grvDdts.SetRowCellValue(i, "xx_seleziona", "N")
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    oCleFadi.dttDDTs.AcceptChanges()
    Try
      For Each dtrT As DataRow In oCleFadi.dttDDTs.Select("xx_seleziona = 'S'")
        dtrT.Delete()
      Next
      oCleFadi.dttDDTs.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
