Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLTM
  Public oCleHltm As CLEmghltm
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsHltm As New DataSet
  Public dcHltm As BindingSource = New BindingSource()
  Public bAbilitaStart As Boolean = False

  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grHltm As NTSInformatica.NTSGrid
  Public WithEvents grvHltm As NTSInformatica.NTSGridView
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdDeseleziona As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents tm_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tm_magaz As NTSInformatica.NTSGridColumn
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
  Public WithEvents tm_annotco As NTSInformatica.NTSGridColumn
  Public WithEvents tm_codstag As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codstag As NTSInformatica.NTSGridColumn
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents ckCommeca As NTSInformatica.NTSCheckBox
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents tm_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tm_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents tm_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents tm_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magimp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totacceva As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totaccevav As NTSInformatica.NTSGridColumn
  Public WithEvents xx_accresiduo As NTSInformatica.NTSGridColumn
  Public WithEvents lbNoteP As NTSInformatica.NTSLabel
  Public WithEvents tm_magimp As NTSInformatica.NTSGridColumn


  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGHLTM", "BEMGHLTM", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128581693271641409, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHltm = CType(oTmp, CLEMGHLTM)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGHLTM", strRemoteServer, strRemotePort)
    oCleHltm.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHltm.RemoteEvent, AddressOf GestisciEventiEntity

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
      ckCommeca.NTSSetParam(oMenu, oApp.Tr(Me, 128583591414375000, "Filtro Commessa"), "S", "N")
      edCommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128582590327500000, "Commessa"), tabcommess)
      grvHltm.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia testata documenti"))
      grvHltm.NTSAllowDelete = False
      grvHltm.NTSAllowUpdate = True
      grvHltm.NTSAllowInsert = False
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589568045625000, "Seleziona"), "S", "N")
      tm_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568058750000, "Conto"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568070625000, "Descr. Conto"), 0, True)
      tm_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568084375000, "Magazzino"), tabmaga)
      tm_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128589568097812500, "Tipo "), dttTipoRk, "val", "cod")
      tm_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568110312500, "Anno"), "0", 4, 0, 9999)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568122656250, "Serie"), CLN__STD.SerieMaxLen, True)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568138437500, "Numero"), "0", 9, 0, 999999999)
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589568149531250, "Data doc."), True)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568160625000, "Descr. magazzino"), 0, True)
      tm_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568172656250, "Anno P."), "0", 4, 0, 9999)
      tm_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568184062500, "Serie P."), CLN__STD.SerieMaxLen, True)
      tm_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568194531250, "Num. P."), "0", 9, 0, 999999999)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568204375000, "Riferimenti"), 0, True)
      tm_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589568215937500, "Data partita"), True)
      tm_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568228750000, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568240000000, "Descr. commessa"), 0, True)
      tm_tipobf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568249218750, "Tipo bolla/fattura"), tabtpbf)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568261718750, "Descr. tipo bolla/fattura"), 0, True)
      tm_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568271406250, "Magazzino 2"), tabmaga)
      xx_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568282500000, "Descr. magazzino 2"), 0, True)
      tm_magimp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568292500000, "Magazzino impegni"), tabmaga)
      xx_magimp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568300937500, "Descr. magazzino impegni"), 0, True)
      tm_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568309375000, "Totale documento"), oApp.FormatImporti, 0)
      tm_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568337968750, "Cod. destinaz."), "0", 9, 0, 999999999)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568358125000, "Descr. destinaz."), 0, True)
      tm_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568410937500, "Cod. pagamento"), tabpaga)
      xx_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568428281250, "Descr. pagamento"), 0, True)
      tm_vistato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589568439375000, "Vistato"), "S", "N")
      tm_annotco.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589568448437500, "Anno TC"), "0", 4, 0, 9999)
      tm_codstag.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589568457656250, "Stagione"), tabstag)
      xx_codstag.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589568466718750, "Descr. stagione"), 0, True)
      tm_totacceva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589567909062500, "Acconto"), oApp.FormatImporti, 0)
      tm_totaccevav.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589567896718750, "Acconto valuta"), oApp.FormatImporti, 0)
      xx_accresiduo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589567884062500, "Residuo"), oApp.FormatImporti, 0)

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
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.ckCommeca = New NTSInformatica.NTSCheckBox
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.lbNoteP = New NTSInformatica.NTSLabel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.cmdDeseleziona = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grHltm = New NTSInformatica.NTSGrid
    Me.grvHltm = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.tm_tipork = New NTSInformatica.NTSGridColumn
    Me.tm_anno = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tm_totdoc = New NTSInformatica.NTSGridColumn
    Me.tm_totacceva = New NTSInformatica.NTSGridColumn
    Me.tm_totaccevav = New NTSInformatica.NTSGridColumn
    Me.xx_accresiduo = New NTSInformatica.NTSGridColumn
    Me.tm_annpar = New NTSInformatica.NTSGridColumn
    Me.tm_alfpar = New NTSInformatica.NTSGridColumn
    Me.tm_numpar = New NTSInformatica.NTSGridColumn
    Me.tm_datpar = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.tm_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.tm_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.tm_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.tm_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xx_magaz2 = New NTSInformatica.NTSGridColumn
    Me.tm_magimp = New NTSInformatica.NTSGridColumn
    Me.xx_magimp = New NTSInformatica.NTSGridColumn
    Me.tm_coddest = New NTSInformatica.NTSGridColumn
    Me.tm_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.tm_vistato = New NTSInformatica.NTSGridColumn
    Me.tm_annotco = New NTSInformatica.NTSGridColumn
    Me.tm_codstag = New NTSInformatica.NTSGridColumn
    Me.xx_codstag = New NTSInformatica.NTSGridColumn
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grHltm, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHltm, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.lbCommeca)
    Me.pnBottom.Controls.Add(Me.ckCommeca)
    Me.pnBottom.Controls.Add(Me.edCommeca)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 280)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(711, 36)
    Me.pnBottom.TabIndex = 1
    Me.pnBottom.Text = "NtsPanel1"
    '
    'lbCommeca
    '
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommeca.Location = New System.Drawing.Point(269, 6)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(329, 20)
    Me.lbCommeca.TabIndex = 17
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'ckCommeca
    '
    Me.ckCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCommeca.Location = New System.Drawing.Point(12, 7)
    Me.ckCommeca.Name = "ckCommeca"
    Me.ckCommeca.NTSCheckValue = "S"
    Me.ckCommeca.NTSUnCheckValue = "N"
    Me.ckCommeca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCommeca.Properties.Appearance.Options.UseBackColor = True
    Me.ckCommeca.Properties.Caption = "Filtro &Commessa"
    Me.ckCommeca.Size = New System.Drawing.Size(105, 19)
    Me.ckCommeca.TabIndex = 15
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.EditValue = "0"
    Me.edCommeca.Location = New System.Drawing.Point(163, 6)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(100, 20)
    Me.edCommeca.TabIndex = 1
    '
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.lbNoteP)
    Me.pnButton.Controls.Add(Me.cmdRicerca)
    Me.pnButton.Controls.Add(Me.cmdDeseleziona)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdSeleziona)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(598, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.Size = New System.Drawing.Size(113, 280)
    Me.pnButton.TabIndex = 2
    Me.pnButton.Text = "NtsPanel1"
    '
    'lbNoteP
    '
    Me.lbNoteP.BackColor = System.Drawing.Color.Transparent
    Me.lbNoteP.Font = New System.Drawing.Font("Tahoma", 8.25!)
    Me.lbNoteP.Location = New System.Drawing.Point(3, 211)
    Me.lbNoteP.Name = "lbNoteP"
    Me.lbNoteP.NTSDbField = ""
    Me.lbNoteP.Size = New System.Drawing.Size(109, 69)
    Me.lbNoteP.TabIndex = 15
    Me.lbNoteP.Text = "N.B. In griglia sono visualizzate solo le note di prelievo in stato ""Non Vistato""" & _
        " o ""Vistato"""
    Me.lbNoteP.Tooltip = ""
    Me.lbNoteP.UseMnemonic = False
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 10)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(98, 24)
    Me.cmdRicerca.TabIndex = 13
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'cmdDeseleziona
    '
    Me.cmdDeseleziona.ImageText = ""
    Me.cmdDeseleziona.Location = New System.Drawing.Point(6, 123)
    Me.cmdDeseleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdDeseleziona.Name = "cmdDeseleziona"
    Me.cmdDeseleziona.Size = New System.Drawing.Size(98, 24)
    Me.cmdDeseleziona.TabIndex = 12
    Me.cmdDeseleziona.Text = "&Deseleziona tutto"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 62)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(98, 24)
    Me.cmdConferma.TabIndex = 11
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 36)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 24)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 98)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
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
    Me.pnGrid.Controls.Add(Me.grHltm)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(598, 280)
    Me.pnGrid.TabIndex = 3
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grHltm
    '
    Me.grHltm.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHltm.EmbeddedNavigator.Name = ""
    Me.grHltm.Location = New System.Drawing.Point(0, 0)
    Me.grHltm.MainView = Me.grvHltm
    Me.grHltm.Name = "grHltm"
    Me.grHltm.Size = New System.Drawing.Size(598, 280)
    Me.grHltm.TabIndex = 0
    Me.grHltm.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHltm})
    '
    'grvHltm
    '
    Me.grvHltm.ActiveFilterEnabled = False
    Me.grvHltm.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.tm_tipork, Me.tm_anno, Me.tm_serie, Me.tm_numdoc, Me.tm_datdoc, Me.tm_conto, Me.xx_conto, Me.tm_totdoc, Me.tm_totacceva, Me.tm_totaccevav, Me.xx_accresiduo, Me.tm_annpar, Me.tm_alfpar, Me.tm_numpar, Me.tm_datpar, Me.tm_riferim, Me.tm_commeca, Me.xx_commeca, Me.tm_tipobf, Me.xx_tipobf, Me.tm_magaz, Me.xx_magaz, Me.tm_magaz2, Me.xx_magaz2, Me.tm_magimp, Me.xx_magimp, Me.tm_coddest, Me.tm_codpaga, Me.xx_codpaga, Me.xx_coddest, Me.tm_vistato, Me.tm_annotco, Me.tm_codstag, Me.xx_codstag})
    Me.grvHltm.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvHltm.Enabled = True
    Me.grvHltm.GridControl = Me.grHltm
    Me.grvHltm.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHltm.Name = "grvHltm"
    Me.grvHltm.NTSAllowDelete = True
    Me.grvHltm.NTSAllowInsert = True
    Me.grvHltm.NTSAllowUpdate = True
    Me.grvHltm.NTSMenuContext = Nothing
    Me.grvHltm.OptionsCustomization.AllowRowSizing = True
    Me.grvHltm.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHltm.OptionsNavigation.UseTabKey = False
    Me.grvHltm.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHltm.OptionsView.ColumnAutoWidth = False
    Me.grvHltm.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHltm.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHltm.OptionsView.ShowGroupPanel = False
    Me.grvHltm.RowHeight = 14
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
    'tm_totacceva
    '
    Me.tm_totacceva.AppearanceCell.Options.UseBackColor = True
    Me.tm_totacceva.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totacceva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totacceva.Caption = "Acconto evaso"
    Me.tm_totacceva.Enabled = False
    Me.tm_totacceva.FieldName = "tm_totacceva"
    Me.tm_totacceva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totacceva.Name = "tm_totacceva"
    Me.tm_totacceva.NTSRepositoryComboBox = Nothing
    Me.tm_totacceva.NTSRepositoryItemCheck = Nothing
    Me.tm_totacceva.NTSRepositoryItemMemo = Nothing
    Me.tm_totacceva.NTSRepositoryItemText = Nothing
    Me.tm_totacceva.OptionsColumn.AllowEdit = False
    Me.tm_totacceva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totacceva.OptionsColumn.ReadOnly = True
    Me.tm_totacceva.OptionsFilter.AllowFilter = False
    Me.tm_totacceva.Visible = True
    Me.tm_totacceva.VisibleIndex = 9
    '
    'tm_totaccevav
    '
    Me.tm_totaccevav.AppearanceCell.Options.UseBackColor = True
    Me.tm_totaccevav.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totaccevav.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totaccevav.Caption = "Acconto evaso val"
    Me.tm_totaccevav.Enabled = False
    Me.tm_totaccevav.FieldName = "tm_totaccevav"
    Me.tm_totaccevav.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totaccevav.Name = "tm_totaccevav"
    Me.tm_totaccevav.NTSRepositoryComboBox = Nothing
    Me.tm_totaccevav.NTSRepositoryItemCheck = Nothing
    Me.tm_totaccevav.NTSRepositoryItemMemo = Nothing
    Me.tm_totaccevav.NTSRepositoryItemText = Nothing
    Me.tm_totaccevav.OptionsColumn.AllowEdit = False
    Me.tm_totaccevav.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totaccevav.OptionsColumn.ReadOnly = True
    Me.tm_totaccevav.OptionsFilter.AllowFilter = False
    Me.tm_totaccevav.Visible = True
    Me.tm_totaccevav.VisibleIndex = 10
    '
    'xx_accresiduo
    '
    Me.xx_accresiduo.AppearanceCell.Options.UseBackColor = True
    Me.xx_accresiduo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_accresiduo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_accresiduo.Caption = "Residuo"
    Me.xx_accresiduo.Enabled = False
    Me.xx_accresiduo.FieldName = "xx_accresiduo"
    Me.xx_accresiduo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_accresiduo.Name = "xx_accresiduo"
    Me.xx_accresiduo.NTSRepositoryComboBox = Nothing
    Me.xx_accresiduo.NTSRepositoryItemCheck = Nothing
    Me.xx_accresiduo.NTSRepositoryItemMemo = Nothing
    Me.xx_accresiduo.NTSRepositoryItemText = Nothing
    Me.xx_accresiduo.OptionsColumn.AllowEdit = False
    Me.xx_accresiduo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_accresiduo.OptionsColumn.ReadOnly = True
    Me.xx_accresiduo.OptionsFilter.AllowFilter = False
    Me.xx_accresiduo.Visible = True
    Me.xx_accresiduo.VisibleIndex = 11
    '
    'tm_annpar
    '
    Me.tm_annpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_annpar.Caption = "Anno P."
    Me.tm_annpar.Enabled = False
    Me.tm_annpar.FieldName = "tm_annpar"
    Me.tm_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_annpar.Name = "tm_annpar"
    Me.tm_annpar.NTSRepositoryComboBox = Nothing
    Me.tm_annpar.NTSRepositoryItemCheck = Nothing
    Me.tm_annpar.NTSRepositoryItemMemo = Nothing
    Me.tm_annpar.NTSRepositoryItemText = Nothing
    Me.tm_annpar.OptionsColumn.AllowEdit = False
    Me.tm_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_annpar.OptionsColumn.ReadOnly = True
    Me.tm_annpar.OptionsFilter.AllowFilter = False
    Me.tm_annpar.Visible = True
    Me.tm_annpar.VisibleIndex = 12
    '
    'tm_alfpar
    '
    Me.tm_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_alfpar.Caption = "Serie P."
    Me.tm_alfpar.Enabled = False
    Me.tm_alfpar.FieldName = "tm_alfpar"
    Me.tm_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_alfpar.Name = "tm_alfpar"
    Me.tm_alfpar.NTSRepositoryComboBox = Nothing
    Me.tm_alfpar.NTSRepositoryItemCheck = Nothing
    Me.tm_alfpar.NTSRepositoryItemMemo = Nothing
    Me.tm_alfpar.NTSRepositoryItemText = Nothing
    Me.tm_alfpar.OptionsColumn.AllowEdit = False
    Me.tm_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_alfpar.OptionsColumn.ReadOnly = True
    Me.tm_alfpar.OptionsFilter.AllowFilter = False
    Me.tm_alfpar.Visible = True
    Me.tm_alfpar.VisibleIndex = 13
    '
    'tm_numpar
    '
    Me.tm_numpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numpar.Caption = "Num. P."
    Me.tm_numpar.Enabled = False
    Me.tm_numpar.FieldName = "tm_numpar"
    Me.tm_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numpar.Name = "tm_numpar"
    Me.tm_numpar.NTSRepositoryComboBox = Nothing
    Me.tm_numpar.NTSRepositoryItemCheck = Nothing
    Me.tm_numpar.NTSRepositoryItemMemo = Nothing
    Me.tm_numpar.NTSRepositoryItemText = Nothing
    Me.tm_numpar.OptionsColumn.AllowEdit = False
    Me.tm_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numpar.OptionsColumn.ReadOnly = True
    Me.tm_numpar.OptionsFilter.AllowFilter = False
    Me.tm_numpar.Visible = True
    Me.tm_numpar.VisibleIndex = 14
    '
    'tm_datpar
    '
    Me.tm_datpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datpar.Caption = "Data P."
    Me.tm_datpar.Enabled = False
    Me.tm_datpar.FieldName = "tm_datpar"
    Me.tm_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datpar.Name = "tm_datpar"
    Me.tm_datpar.NTSRepositoryComboBox = Nothing
    Me.tm_datpar.NTSRepositoryItemCheck = Nothing
    Me.tm_datpar.NTSRepositoryItemMemo = Nothing
    Me.tm_datpar.NTSRepositoryItemText = Nothing
    Me.tm_datpar.OptionsColumn.AllowEdit = False
    Me.tm_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datpar.OptionsColumn.ReadOnly = True
    Me.tm_datpar.OptionsFilter.AllowFilter = False
    Me.tm_datpar.Visible = True
    Me.tm_datpar.VisibleIndex = 15
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
    Me.tm_riferim.VisibleIndex = 16
    '
    'tm_commeca
    '
    Me.tm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.tm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.tm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_commeca.Caption = "Commessa"
    Me.tm_commeca.Enabled = False
    Me.tm_commeca.FieldName = "tm_commeca"
    Me.tm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_commeca.Name = "tm_commeca"
    Me.tm_commeca.NTSRepositoryComboBox = Nothing
    Me.tm_commeca.NTSRepositoryItemCheck = Nothing
    Me.tm_commeca.NTSRepositoryItemMemo = Nothing
    Me.tm_commeca.NTSRepositoryItemText = Nothing
    Me.tm_commeca.OptionsColumn.AllowEdit = False
    Me.tm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_commeca.OptionsColumn.ReadOnly = True
    Me.tm_commeca.OptionsFilter.AllowFilter = False
    Me.tm_commeca.Visible = True
    Me.tm_commeca.VisibleIndex = 17
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = False
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 18
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
    Me.tm_tipobf.VisibleIndex = 19
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descr. tipo BF"
    Me.xx_tipobf.Enabled = False
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowEdit = False
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipobf.OptionsColumn.ReadOnly = True
    Me.xx_tipobf.OptionsFilter.AllowFilter = False
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 20
    '
    'tm_magaz
    '
    Me.tm_magaz.AppearanceCell.Options.UseBackColor = True
    Me.tm_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.tm_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_magaz.Caption = "Magazzino"
    Me.tm_magaz.Enabled = False
    Me.tm_magaz.FieldName = "tm_magaz"
    Me.tm_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_magaz.Name = "tm_magaz"
    Me.tm_magaz.NTSRepositoryComboBox = Nothing
    Me.tm_magaz.NTSRepositoryItemCheck = Nothing
    Me.tm_magaz.NTSRepositoryItemMemo = Nothing
    Me.tm_magaz.NTSRepositoryItemText = Nothing
    Me.tm_magaz.OptionsColumn.AllowEdit = False
    Me.tm_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_magaz.OptionsColumn.ReadOnly = True
    Me.tm_magaz.OptionsFilter.AllowFilter = False
    Me.tm_magaz.Visible = True
    Me.tm_magaz.VisibleIndex = 21
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Descr. magazzino"
    Me.xx_magaz.Enabled = False
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowEdit = False
    Me.xx_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz.OptionsColumn.ReadOnly = True
    Me.xx_magaz.OptionsFilter.AllowFilter = False
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 22
    '
    'tm_magaz2
    '
    Me.tm_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.tm_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.tm_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_magaz2.Caption = "Magaz. 2"
    Me.tm_magaz2.Enabled = False
    Me.tm_magaz2.FieldName = "tm_magaz2"
    Me.tm_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_magaz2.Name = "tm_magaz2"
    Me.tm_magaz2.NTSRepositoryComboBox = Nothing
    Me.tm_magaz2.NTSRepositoryItemCheck = Nothing
    Me.tm_magaz2.NTSRepositoryItemMemo = Nothing
    Me.tm_magaz2.NTSRepositoryItemText = Nothing
    Me.tm_magaz2.OptionsColumn.AllowEdit = False
    Me.tm_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_magaz2.OptionsColumn.ReadOnly = True
    Me.tm_magaz2.OptionsFilter.AllowFilter = False
    Me.tm_magaz2.Visible = True
    Me.tm_magaz2.VisibleIndex = 23
    '
    'xx_magaz2
    '
    Me.xx_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz2.Caption = "Descr. magaz. 2"
    Me.xx_magaz2.Enabled = False
    Me.xx_magaz2.FieldName = "xx_magaz2"
    Me.xx_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz2.Name = "xx_magaz2"
    Me.xx_magaz2.NTSRepositoryComboBox = Nothing
    Me.xx_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz2.NTSRepositoryItemText = Nothing
    Me.xx_magaz2.OptionsColumn.AllowEdit = False
    Me.xx_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz2.OptionsColumn.ReadOnly = True
    Me.xx_magaz2.OptionsFilter.AllowFilter = False
    Me.xx_magaz2.Visible = True
    Me.xx_magaz2.VisibleIndex = 24
    '
    'tm_magimp
    '
    Me.tm_magimp.AppearanceCell.Options.UseBackColor = True
    Me.tm_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.tm_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_magimp.Caption = "Magaz. impegni"
    Me.tm_magimp.Enabled = False
    Me.tm_magimp.FieldName = "tm_magimp"
    Me.tm_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_magimp.Name = "tm_magimp"
    Me.tm_magimp.NTSRepositoryComboBox = Nothing
    Me.tm_magimp.NTSRepositoryItemCheck = Nothing
    Me.tm_magimp.NTSRepositoryItemMemo = Nothing
    Me.tm_magimp.NTSRepositoryItemText = Nothing
    Me.tm_magimp.OptionsColumn.AllowEdit = False
    Me.tm_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_magimp.OptionsColumn.ReadOnly = True
    Me.tm_magimp.OptionsFilter.AllowFilter = False
    Me.tm_magimp.Visible = True
    Me.tm_magimp.VisibleIndex = 25
    '
    'xx_magimp
    '
    Me.xx_magimp.AppearanceCell.Options.UseBackColor = True
    Me.xx_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magimp.Caption = "Descr. magaz. imp."
    Me.xx_magimp.Enabled = False
    Me.xx_magimp.FieldName = "xx_magimp"
    Me.xx_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magimp.Name = "xx_magimp"
    Me.xx_magimp.NTSRepositoryComboBox = Nothing
    Me.xx_magimp.NTSRepositoryItemCheck = Nothing
    Me.xx_magimp.NTSRepositoryItemMemo = Nothing
    Me.xx_magimp.NTSRepositoryItemText = Nothing
    Me.xx_magimp.OptionsColumn.AllowEdit = False
    Me.xx_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magimp.OptionsColumn.ReadOnly = True
    Me.xx_magimp.OptionsFilter.AllowFilter = False
    Me.xx_magimp.Visible = True
    Me.xx_magimp.VisibleIndex = 26
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
    Me.tm_coddest.VisibleIndex = 27
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
    Me.tm_codpaga.VisibleIndex = 28
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
    Me.xx_codpaga.VisibleIndex = 29
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
    Me.xx_coddest.VisibleIndex = 30
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
    Me.tm_vistato.VisibleIndex = 31
    '
    'tm_annotco
    '
    Me.tm_annotco.AppearanceCell.Options.UseBackColor = True
    Me.tm_annotco.AppearanceCell.Options.UseTextOptions = True
    Me.tm_annotco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_annotco.Caption = "Anno TC"
    Me.tm_annotco.Enabled = False
    Me.tm_annotco.FieldName = "tm_annotco"
    Me.tm_annotco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_annotco.Name = "tm_annotco"
    Me.tm_annotco.NTSRepositoryComboBox = Nothing
    Me.tm_annotco.NTSRepositoryItemCheck = Nothing
    Me.tm_annotco.NTSRepositoryItemMemo = Nothing
    Me.tm_annotco.NTSRepositoryItemText = Nothing
    Me.tm_annotco.OptionsColumn.AllowEdit = False
    Me.tm_annotco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_annotco.OptionsColumn.ReadOnly = True
    Me.tm_annotco.OptionsFilter.AllowFilter = False
    Me.tm_annotco.Visible = True
    Me.tm_annotco.VisibleIndex = 32
    '
    'tm_codstag
    '
    Me.tm_codstag.AppearanceCell.Options.UseBackColor = True
    Me.tm_codstag.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codstag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codstag.Caption = "Stagione"
    Me.tm_codstag.Enabled = False
    Me.tm_codstag.FieldName = "tm_codstag"
    Me.tm_codstag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codstag.Name = "tm_codstag"
    Me.tm_codstag.NTSRepositoryComboBox = Nothing
    Me.tm_codstag.NTSRepositoryItemCheck = Nothing
    Me.tm_codstag.NTSRepositoryItemMemo = Nothing
    Me.tm_codstag.NTSRepositoryItemText = Nothing
    Me.tm_codstag.OptionsColumn.AllowEdit = False
    Me.tm_codstag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codstag.OptionsColumn.ReadOnly = True
    Me.tm_codstag.OptionsFilter.AllowFilter = False
    Me.tm_codstag.Visible = True
    Me.tm_codstag.VisibleIndex = 33
    '
    'xx_codstag
    '
    Me.xx_codstag.AppearanceCell.Options.UseBackColor = True
    Me.xx_codstag.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codstag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codstag.Caption = "Descr. stagione"
    Me.xx_codstag.Enabled = False
    Me.xx_codstag.FieldName = "xx_codstag"
    Me.xx_codstag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codstag.Name = "xx_codstag"
    Me.xx_codstag.NTSRepositoryComboBox = Nothing
    Me.xx_codstag.NTSRepositoryItemCheck = Nothing
    Me.xx_codstag.NTSRepositoryItemMemo = Nothing
    Me.xx_codstag.NTSRepositoryItemText = Nothing
    Me.xx_codstag.OptionsColumn.AllowEdit = False
    Me.xx_codstag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codstag.OptionsColumn.ReadOnly = True
    Me.xx_codstag.OptionsFilter.AllowFilter = False
    Me.xx_codstag.Visible = True
    Me.xx_codstag.VisibleIndex = 34
    '
    'FRMMGHLTM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(711, 316)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnBottom)
    Me.MinimizeBox = False
    Me.Name = "FRMMGHLTM"
    Me.Text = "SELEZIONE TESTATE DOCUMENTI"
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grHltm, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHltm, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub


  Public Overridable Sub FRMMGHLTM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
      GctlApplicaDefaultValue()

      oCleHltm.strDittaCorrente = DittaCorrente

      bAbilitaStart = True

      If oParam.nTipologia = -2 Then
        'acconti di parcellazione (testpar): nascondo le colonne non gestite
        tm_magaz.Visible = False
        xx_magaz.Visible = False
        tm_magaz2.Visible = False
        xx_magaz2.Visible = False
        tm_magimp.Visible = False
        xx_magimp.Visible = False
        tm_totaccevav.Visible = False
        tm_annotco.Visible = False
        tm_codstag.Visible = False
        xx_codstag.Visible = False
      End If

      oCleHltm.nTipologia = oParam.nTipologia
      oCleHltm.bTestPrb = CBool(IIf(oParam.strTipo = "W", True, False))
      If Not oCleHltm.bTestPrb Then lbNoteP.Visible = False
      If oParam.bFlag1 Then
        cmdConferma.Text = oApp.Tr(Me, 128753958312704000, "Modifica") 'invece di visualizzare il comando 'conferma' visualizza 'modifica'
      End If
      cmdRicerca_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tm_commeca.Visible = False
        xx_commeca.Visible = False
        tm_vistato.Visible = False
        tm_annotco.Visible = False
        tm_codstag.Visible = False
        xx_codstag.Visible = False
        pnBottom.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      If bAbilitaStart = False Then Return

      dsHltm.Clear()

      'parametri passati dal chiamante
      'oPar.strDescr = oCle__Cons.GetWhereHltd()
      'oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione, 2 = 'Acconti, -2 acconti parcellazione
      'oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsORHLTD da visualizzare (in vb6 oPar.nMastro)

      If oParam.nTipologia <> 1 Then
        cmdSeleziona.Visible = False
        cmdDeseleziona.Visible = False
      Else
        GctlSetVisEnab(cmdSeleziona, True)
        GctlSetVisEnab(cmdDeseleziona, True)
      End If

      If oParam.nTipologia = 2 Or oParam.nTipologia = -2 Then
        GctlSetVisEnab(tm_totacceva, True)
        GctlSetVisEnab(tm_totaccevav, True)
        GctlSetVisEnab(xx_accresiduo, True)
        xx_seleziona.Visible = False
      Else
        tm_totacceva.Visible = False
        tm_totaccevav.Visible = False
        xx_accresiduo.Visible = False
        GctlSetVisEnab(xx_seleziona, True)
      End If

      Me.Cursor = Cursors.WaitCursor
      If Not oCleHltm.GetData(NTSCInt(IIf(ckCommeca.Checked, edCommeca.Text, 0)), oParam.strDescr.Replace("?", "_").Replace("*", "%"), dsHltm) Then
        Me.Close()
        Return
      End If
      dcHltm.DataSource = dsHltm.Tables("TESTMAG")
      dsHltm.AcceptChanges()
      grHltm.DataSource = dcHltm

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvHltm.RowCount > 0 Then grHltm.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è chiamato da seleziona Acconti e la colonna relativa alla selezione di rige è nascosta,
      '--- ritorna la riga corrente
      '--------------------------------------------------------------------------------------------------------------
      If (oCleHltm.nTipologia = 2 Or oCleHltm.nTipologia = -2) And (xx_seleziona.Visible = False) Then
        If Not grvHltm.NTSGetCurrentDataRow() Is Nothing Then
          grvHltm.NTSGetCurrentDataRow!xx_seleziona = "S"
          grvHltm.NTSGetCurrentDataRow.AcceptChanges()
        End If
      End If
      If Not Salva() Then Return

      dsHltm.Tables("TESTMAG").AcceptChanges()

      dtrT = dsHltm.Tables("TESTMAG").Select("xx_seleziona = 'N'")
      For i = 0 To dtrT.Length - 1
        dtrT(i).Delete()
      Next
      dsHltm.Tables("TESTMAG").AcceptChanges()
      oParam.oParam = dsHltm.Tables("TESTMAG")      'ritorno le righe selezionate

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.oParam = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdDeseleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeseleziona.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub grHltm_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grHltm.MouseDoubleClick
    Try
      If (oCleHltm.nTipologia = 2) And (xx_seleziona.Visible = False) Then
        cmdConferma_Click(Me, Nothing)
      Else
        If Not grvHltm.NTSGetCurrentDataRow Is Nothing Then
          If grvHltm.NTSGetCurrentDataRow!xx_seleziona.ToString = "S" Then
            grvHltm.NTSGetCurrentDataRow!xx_seleziona = "N"
          Else
            grvHltm.NTSGetCurrentDataRow!xx_seleziona = "S"
          End If
          grvHltm.NTSGetCurrentDataRow.AcceptChanges()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvHltm_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHltm.NTSBeforeRowUpdate
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

  Public Overridable Sub edCommeca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommeca.Validated
    Dim strTmp As String = ""
    Try

      If Not oCleHltm.edCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
        edCommeca.Text = NTSCStr(edCommeca.OldEditValue)
      Else
        lbCommeca.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleHltm.Salva() Then
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SelezionaDeselezionaRighe(ByVal bSel As Boolean)
    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsHltm.Tables("TESTMAG").Rows.Count - 1)
        With dsHltm.Tables("TESTMAG").Rows(i)
          !xx_seleziona = IIf(bSel = True, "S", "N").ToString
        End With
      Next
      dsHltm.Tables("TESTMAG").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
