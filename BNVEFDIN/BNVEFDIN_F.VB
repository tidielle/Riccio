Imports System.Data
Imports NTSInformatica.CLN__STD

Imports System
Public Class FRMVEFDIN_F
  Private Moduli_P As Integer = CLN__STD.bsModMG Or CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property


  Public oCallParams As CLE__CLDP
  Public oCleFdin As CLEVEFDIN
  Public dsFdin As DataSet
  Public dcFdin As BindingSource = New BindingSource
  Public dcFdinDDT As BindingSource = New BindingSource
  Public bInLoadDoc As Boolean = False                    'true durante il caricamento di un documento
  Private components As System.ComponentModel.IContainer


  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      oCallParams = Param
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()     'tutto il tempo di attesa nell' avvio del child è in questa routine 
      Me.MinimumSize = Me.Size

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEFDIN", "BEVEFDIN", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleFdin = CType(oTmp, CLEVEFDIN)
      '------------------------------------------------
      bRemoting = Menu.Remoting("BNVEFDIN", strRemoteServer, strRemotePort)
      AddHandler oCleFdin.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleFdin.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim strTmp1() As String
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strT As String = ""
    Dim frmMsgb As FRM__MSG2 = Nothing

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try

      'esco se i messaggi sono dovuto all'apertura del documento
      If bInLoadDoc Then Return

      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Trim.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "VNoteConto"
            frmMsgb = CType(NTSNewFormModal("FRM__MSG2"), FRM__MSG2)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE AL CONTO " & oCleFdin.lContoCF.ToString
            frmMsgb.ShowDialog()

          Case "edDtPrimoP"
            If strTmp(i).Substring(11, 1) = "N" Then
              edTm_datapag.Enabled = False
            Else
              GctlSetVisEnab(edTm_datapag, False)
            End If

          Case "AggTotali."
            'aggiorno le colonne unbound dei totali documento
            strTmp1 = strTmp(i).Substring(11).Split(CType("§", Char))
            edTotSpese.NTSTextDB = strTmp1(0)
            edTotSpesev.NTSTextDB = strTmp1(1)
            edTotImponib.NTSTextDB = strTmp1(2)
            edTotImponibv.NTSTextDB = strTmp1(3)
            edTotIva.NTSTextDB = strTmp1(4)
            edTotIvav.NTSTextDB = strTmp1(5)
            edTotale.NTSTextDB = strTmp1(6)
            edTotalev.NTSTextDB = strTmp1(7)
            edResiduo.NTSTextDB = strTmp1(8)
            edResiduov.NTSTextDB = strTmp1(9)
            If strTmp1.Length > 10 Then edTotQuant.NTSTextDB = strTmp1(10) 'passa di qui anche al ri-salvataggio dei DDT emessi!!!! dove non viene passato questo campo

          Case "ApriDocum."
            'in fase di import ddt se serve apro il documento in elaborazione
            strTmp1 = strTmp(i).Substring(11).Split(CType("§", Char))
            ApriDoc(strTmp1(0), NTSCInt(strTmp1(1)), strTmp1(2), NTSCInt(strTmp1(3)))

        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmMsgb Is Nothing Then frmMsgb.Dispose()
      frmMsgb = Nothing
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEFDIN_F))
    Me.pnTestataTop = New NTSInformatica.NTSPanel
    Me.pnTestataClforn = New NTSInformatica.NTSPanel
    Me.lbTm_datdoc = New NTSInformatica.NTSLabel
    Me.lbXx_coddest = New NTSInformatica.NTSLabel
    Me.edTm_datdoc = New NTSInformatica.NTSTextBoxData
    Me.lbTm_coddest = New NTSInformatica.NTSLabel
    Me.lbXx_conto = New NTSInformatica.NTSLabel
    Me.edTm_coddest = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_conto = New NTSInformatica.NTSLabel
    Me.edTm_conto = New NTSInformatica.NTSTextBoxNum
    Me.fmDocumento = New NTSInformatica.NTSGroupBox
    Me.edAnnoDoc = New NTSInformatica.NTSTextBoxNum
    Me.cbTipoDoc = New NTSInformatica.NTSComboBox
    Me.edSerieDoc = New NTSInformatica.NTSTextBoxStr
    Me.edNumDoc = New NTSInformatica.NTSTextBoxNum
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbFlussoDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager
    Me.TabPage4 = New NTSInformatica.NTSTabPage
    Me.pnNote = New NTSInformatica.NTSPanel
    Me.edTm_note = New NTSInformatica.NTSMemoBox
    Me.TabPage2 = New NTSInformatica.NTSTabPage
    Me.pnVarie = New NTSInformatica.NTSPanel
    Me.pnPage4Fill = New NTSInformatica.NTSPanel
    Me.pnPage4Bottom = New NTSInformatica.NTSPanel
    Me.fmContabilizzato = New NTSInformatica.NTSGroupBox
    Me.edTm_nregiva = New NTSInformatica.NTSTextBoxNum
    Me.edTm_alfpro = New NTSInformatica.NTSTextBoxStr
    Me.lbTm_nregiva = New NTSInformatica.NTSLabel
    Me.edTm_numprot = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_datregef = New NTSInformatica.NTSLabel
    Me.lbTm_numregef = New NTSInformatica.NTSLabel
    Me.edTm_datregef = New NTSInformatica.NTSTextBoxData
    Me.edTm_numregef = New NTSInformatica.NTSTextBoxNum
    Me.ckTm_flcont = New NTSInformatica.NTSCheckBox
    Me.fmIntrastat = New NTSInformatica.NTSGroupBox
    Me.edTm_codport = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codrsta = New NTSInformatica.NTSLabel
    Me.edTm_codrsta = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codrsta = New NTSInformatica.NTSLabel
    Me.lbXx_codport = New NTSInformatica.NTSLabel
    Me.lbTm_codport = New NTSInformatica.NTSLabel
    Me.lbXx_codntra = New NTSInformatica.NTSLabel
    Me.edTm_codntra = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codntra = New NTSInformatica.NTSLabel
    Me.pnDDT = New NTSInformatica.NTSPanel
    Me.cmdDDTDel = New NTSInformatica.NTSButton
    Me.cmdDDTMod = New NTSInformatica.NTSButton
    Me.cmdDDTAdd = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grDDT = New NTSInformatica.NTSGrid
    Me.grvDDT = New NTSInformatica.NTSGridView
    Me.tm_tipork = New NTSInformatica.NTSGridColumn
    Me.tm_anno = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_totdoc = New NTSInformatica.NTSGridColumn
    Me.tm_valuta = New NTSInformatica.NTSGridColumn
    Me.tm_cambio = New NTSInformatica.NTSGridColumn
    Me.tm_annpar = New NTSInformatica.NTSGridColumn
    Me.tm_alfpar = New NTSInformatica.NTSGridColumn
    Me.tm_numpar = New NTSInformatica.NTSGridColumn
    Me.tm_datpar = New NTSInformatica.NTSGridColumn
    Me.tm_coddest = New NTSInformatica.NTSGridColumn
    Me.tm_codagen = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.TabPage3 = New NTSInformatica.NTSTabPage
    Me.pnCmd = New NTSInformatica.NTSPanel
    Me.cmdCastelletti = New NTSInformatica.NTSButton
    Me.cmdModifScad = New NTSInformatica.NTSButton
    Me.fmProvvigioni = New NTSInformatica.NTSGroupBox
    Me.ckTm_flprov = New NTSInformatica.NTSCheckBox
    Me.edTm_totprov = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_totprov = New NTSInformatica.NTSLabel
    Me.edTm_impprov = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_impprov = New NTSInformatica.NTSLabel
    Me.fmPartita = New NTSInformatica.NTSGroupBox
    Me.lbTm_datpar = New NTSInformatica.NTSLabel
    Me.edTm_datpar = New NTSInformatica.NTSTextBoxData
    Me.edTm_numpar = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_numpar = New NTSInformatica.NTSLabel
    Me.edTm_alfpar = New NTSInformatica.NTSTextBoxStr
    Me.lbTm_annpar = New NTSInformatica.NTSLabel
    Me.edTm_annpar = New NTSInformatica.NTSTextBoxNum
    Me.fmTrasporto = New NTSInformatica.NTSGroupBox
    Me.edTotQuant = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_acuradi = New NTSInformatica.NTSLabel
    Me.edTm_aspetto = New NTSInformatica.NTSTextBoxStr
    Me.edTm_pesonetto = New NTSInformatica.NTSTextBoxNum
    Me.lbPesoNettoLordo = New NTSInformatica.NTSLabel
    Me.edTm_peso = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totcoll = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_totcoll = New NTSInformatica.NTSLabel
    Me.lbXx_porto = New NTSInformatica.NTSLabel
    Me.edTm_porto = New NTSInformatica.NTSTextBoxStr
    Me.lbTm_porto = New NTSInformatica.NTSLabel
    Me.cbTm_acuradi = New NTSInformatica.NTSComboBox
    Me.lbXx_vettor2 = New NTSInformatica.NTSLabel
    Me.edTm_vettor2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_vettor2 = New NTSInformatica.NTSLabel
    Me.lbXx_coddest2 = New NTSInformatica.NTSLabel
    Me.edTm_coddest2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_coddest2 = New NTSInformatica.NTSLabel
    Me.edTm_codaspe = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codaspe = New NTSInformatica.NTSLabel
    Me.lbXx_caustra = New NTSInformatica.NTSLabel
    Me.edTm_caustra = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_caustra = New NTSInformatica.NTSLabel
    Me.fmSpese = New NTSInformatica.NTSGroupBox
    Me.lbResto = New NTSInformatica.NTSLabel
    Me.edTm_resto = New NTSInformatica.NTSTextBoxNum
    Me.edTm_pagato2 = New NTSInformatica.NTSTextBoxNum
    Me.lbIncassato2 = New NTSInformatica.NTSLabel
    Me.ckTm_flscdb = New NTSInformatica.NTSCheckBox
    Me.lbRiga2 = New NTSInformatica.NTSLabel
    Me.lbRiga1 = New NTSInformatica.NTSLabel
    Me.edTotalev = New NTSInformatica.NTSTextBoxNum
    Me.edTotale = New NTSInformatica.NTSTextBoxNum
    Me.lbTotale = New NTSInformatica.NTSLabel
    Me.edResiduo = New NTSInformatica.NTSTextBoxNum
    Me.lbRimanenza = New NTSInformatica.NTSLabel
    Me.edResiduov = New NTSInformatica.NTSTextBoxNum
    Me.edTm_abbuono = New NTSInformatica.NTSTextBoxNum
    Me.lbAbbuono = New NTSInformatica.NTSLabel
    Me.edTm_pagato = New NTSInformatica.NTSTextBoxNum
    Me.lbIncassato = New NTSInformatica.NTSLabel
    Me.edTm_abbuonov = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totomag = New NTSInformatica.NTSTextBoxNum
    Me.lbOmaggi = New NTSInformatica.NTSLabel
    Me.lbValuta = New NTSInformatica.NTSLabel
    Me.edTm_speaccv = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totdoc = New NTSInformatica.NTSTextBoxNum
    Me.edTm_pagatov = New NTSInformatica.NTSTextBoxNum
    Me.lbDocumento = New NTSInformatica.NTSLabel
    Me.edTm_speimbv = New NTSInformatica.NTSTextBoxNum
    Me.edTotIva = New NTSInformatica.NTSTextBoxNum
    Me.edTm_speincv = New NTSInformatica.NTSTextBoxNum
    Me.lbImposta = New NTSInformatica.NTSLabel
    Me.edTm_bolliv = New NTSInformatica.NTSTextBoxNum
    Me.edTotImponib = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totomagv = New NTSInformatica.NTSTextBoxNum
    Me.lbImponibile = New NTSInformatica.NTSLabel
    Me.edTm_totmercev = New NTSInformatica.NTSTextBoxNum
    Me.edTotSpese = New NTSInformatica.NTSTextBoxNum
    Me.edTotSpesev = New NTSInformatica.NTSTextBoxNum
    Me.lbSpese = New NTSInformatica.NTSLabel
    Me.edTotImponibv = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totmerce = New NTSInformatica.NTSTextBoxNum
    Me.edTm_totdocv = New NTSInformatica.NTSTextBoxNum
    Me.lbNetto = New NTSInformatica.NTSLabel
    Me.edTotIvav = New NTSInformatica.NTSTextBoxNum
    Me.edTm_bolli = New NTSInformatica.NTSTextBoxNum
    Me.lbBolli = New NTSInformatica.NTSLabel
    Me.edTm_speinc = New NTSInformatica.NTSTextBoxNum
    Me.lbIncasso = New NTSInformatica.NTSLabel
    Me.edTm_speimb = New NTSInformatica.NTSTextBoxNum
    Me.lbImballo = New NTSInformatica.NTSLabel
    Me.lbEur = New NTSInformatica.NTSLabel
    Me.edTm_speacc = New NTSInformatica.NTSTextBoxNum
    Me.lbTrasporto = New NTSInformatica.NTSLabel
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTestataDx = New NTSInformatica.NTSPanel
    Me.lbXx_codpaga2 = New NTSInformatica.NTSLabel
    Me.edTm_codpaga2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codpaga2 = New NTSInformatica.NTSLabel
    Me.lbXx_vettor = New NTSInformatica.NTSLabel
    Me.edTm_cambio = New NTSInformatica.NTSTextBoxNum
    Me.edTm_vettor = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_valuta = New NTSInformatica.NTSLabel
    Me.edTm_valuta = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_valuta = New NTSInformatica.NTSLabel
    Me.lbXx_codpaga = New NTSInformatica.NTSLabel
    Me.edTm_scont2 = New NTSInformatica.NTSTextBoxNum
    Me.edTm_scont1 = New NTSInformatica.NTSTextBoxNum
    Me.edTm_datapag = New NTSInformatica.NTSTextBoxData
    Me.edTm_scopag = New NTSInformatica.NTSTextBoxNum
    Me.edTm_codpaga = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_vettor = New NTSInformatica.NTSLabel
    Me.edTm_listino = New NTSInformatica.NTSTextBoxNum
    Me.fmCa = New NTSInformatica.NTSGroupBox
    Me.edTm_subcommeca = New NTSInformatica.NTSTextBoxStr
    Me.lbTm_subcommeca = New NTSInformatica.NTSLabel
    Me.lbXx_commeca = New NTSInformatica.NTSLabel
    Me.lbXx_codcena = New NTSInformatica.NTSLabel
    Me.edTm_codcena = New NTSInformatica.NTSTextBoxNum
    Me.edTm_commeca = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codcena = New NTSInformatica.NTSLabel
    Me.lbTm_commeca = New NTSInformatica.NTSLabel
    Me.lbScontiTesta = New NTSInformatica.NTSLabel
    Me.fmBanca = New NTSInformatica.NTSGroupBox
    Me.ckTm_autpag = New NTSInformatica.NTSCheckBox
    Me.lbXx_codbanc = New NTSInformatica.NTSLabel
    Me.edTm_codbanc = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_codbanc = New NTSInformatica.NTSLabel
    Me.edTm_banc2 = New NTSInformatica.NTSTextBoxStr
    Me.edTm_banc1 = New NTSInformatica.NTSTextBoxStr
    Me.edTm_cab = New NTSInformatica.NTSTextBoxNum
    Me.edTm_abi = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_cab = New NTSInformatica.NTSLabel
    Me.lbTm_abi = New NTSInformatica.NTSLabel
    Me.lbTm_datapag = New NTSInformatica.NTSLabel
    Me.lbTm_scopag = New NTSInformatica.NTSLabel
    Me.lbTm_codpaga = New NTSInformatica.NTSLabel
    Me.lbTm_listino = New NTSInformatica.NTSLabel
    Me.pnTestataSx = New NTSInformatica.NTSPanel
    Me.lbXx_causcap = New NTSInformatica.NTSLabel
    Me.edTm_causcap = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_causcap = New NTSInformatica.NTSLabel
    Me.lbXx_causale = New NTSInformatica.NTSLabel
    Me.edTm_causale = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_causale = New NTSInformatica.NTSLabel
    Me.lbXx_contfatt = New NTSInformatica.NTSLabel
    Me.edTm_contfatt = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_contfatt = New NTSInformatica.NTSLabel
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbTm_controp = New NTSInformatica.NTSLabel
    Me.edTm_controp = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_magimp = New NTSInformatica.NTSLabel
    Me.lbXx_magaz2 = New NTSInformatica.NTSLabel
    Me.edTm_magimp = New NTSInformatica.NTSTextBoxNum
    Me.edTm_magaz2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_magimp = New NTSInformatica.NTSLabel
    Me.lbTm_magaz2 = New NTSInformatica.NTSLabel
    Me.fmStato = New NTSInformatica.NTSGroupBox
    Me.ckTm_vistato = New NTSInformatica.NTSCheckBox
    Me.ckTm_flevas = New NTSInformatica.NTSCheckBox
    Me.ckTm_flbolli = New NTSInformatica.NTSCheckBox
    Me.ckTm_flspinc = New NTSInformatica.NTSCheckBox
    Me.ckTm_scorpo = New NTSInformatica.NTSCheckBox
    Me.edTm_riferim = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codese = New NTSInformatica.NTSLabel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.edTm_codese = New NTSInformatica.NTSTextBoxNum
    Me.edTm_magaz = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codagen = New NTSInformatica.NTSLabel
    Me.lbXx_tipobf = New NTSInformatica.NTSLabel
    Me.edtm_codagen = New NTSInformatica.NTSTextBoxNum
    Me.edTm_tipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbTm_riferim = New NTSInformatica.NTSLabel
    Me.lbTm_codese = New NTSInformatica.NTSLabel
    Me.lbTm_magaz = New NTSInformatica.NTSLabel
    Me.lbtm_codagen = New NTSInformatica.NTSLabel
    Me.lbTm_tipobf = New NTSInformatica.NTSLabel
    Me.tsFdin = New NTSInformatica.NTSTabControl
    CType(Me.pnTestataTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestataTop.SuspendLayout()
    CType(Me.pnTestataClforn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestataClforn.SuspendLayout()
    CType(Me.edTm_datdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_conto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDocumento.SuspendLayout()
    CType(Me.edAnnoDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerieDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage4.SuspendLayout()
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNote.SuspendLayout()
    CType(Me.edTm_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    CType(Me.pnVarie, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnVarie.SuspendLayout()
    CType(Me.pnPage4Fill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage4Fill.SuspendLayout()
    CType(Me.pnPage4Bottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage4Bottom.SuspendLayout()
    CType(Me.fmContabilizzato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmContabilizzato.SuspendLayout()
    CType(Me.edTm_nregiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_alfpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_numprot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_datregef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_numregef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_flcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIntrastat, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIntrastat.SuspendLayout()
    CType(Me.edTm_codport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codrsta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codntra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDDT, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDDT.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grDDT, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDDT, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    CType(Me.pnCmd, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCmd.SuspendLayout()
    CType(Me.fmProvvigioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmProvvigioni.SuspendLayout()
    CType(Me.ckTm_flprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_impprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPartita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPartita.SuspendLayout()
    CType(Me.edTm_datpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_numpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_alfpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_annpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTrasporto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTrasporto.SuspendLayout()
    CType(Me.edTotQuant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_aspetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_pesonetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_peso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totcoll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_porto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTm_acuradi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_vettor2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_coddest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codaspe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_caustra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSpese, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSpese.SuspendLayout()
    CType(Me.edTm_resto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_pagato2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_flscdb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotalev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edResiduo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edResiduov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_abbuono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_pagato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_abbuonov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totomag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speaccv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_pagatov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speimbv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotIva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speincv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_bolliv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotImponib.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totomagv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totmercev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotSpese.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotSpesev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotImponibv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totmerce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_totdocv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotIvav.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_bolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speinc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speimb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_speacc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnTestataDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestataDx.SuspendLayout()
    CType(Me.edTm_codpaga2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_cambio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_vettor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_valuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scont2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scont1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_datapag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_scopag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codpaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_listino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCa.SuspendLayout()
    CType(Me.edTm_subcommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codcena.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_commeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmBanca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmBanca.SuspendLayout()
    CType(Me.ckTm_autpag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_banc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_banc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_cab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_abi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTestataSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTestataSx.SuspendLayout()
    CType(Me.edTm_causcap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_causale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_contfatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_magimp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_magaz2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStato.SuspendLayout()
    CType(Me.ckTm_vistato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_flevas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_flbolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_flspinc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTm_scorpo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_riferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_codese.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_magaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edtm_codagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTm_tipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsFdin, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsFdin.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'pnTestataTop
    '
    Me.pnTestataTop.AllowDrop = True
    Me.pnTestataTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestataTop.Appearance.Options.UseBackColor = True
    Me.pnTestataTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestataTop.Controls.Add(Me.pnTestataClforn)
    Me.pnTestataTop.Controls.Add(Me.fmDocumento)
    Me.pnTestataTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestataTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTestataTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTestataTop.Name = "pnTestataTop"
    Me.pnTestataTop.NTSActiveTrasparency = True
    Me.pnTestataTop.Size = New System.Drawing.Size(747, 86)
    Me.pnTestataTop.TabIndex = 41
    '
    'pnTestataClforn
    '
    Me.pnTestataClforn.AllowDrop = True
    Me.pnTestataClforn.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestataClforn.Appearance.Options.UseBackColor = True
    Me.pnTestataClforn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestataClforn.Controls.Add(Me.lbTm_datdoc)
    Me.pnTestataClforn.Controls.Add(Me.lbXx_coddest)
    Me.pnTestataClforn.Controls.Add(Me.edTm_datdoc)
    Me.pnTestataClforn.Controls.Add(Me.lbTm_coddest)
    Me.pnTestataClforn.Controls.Add(Me.lbXx_conto)
    Me.pnTestataClforn.Controls.Add(Me.edTm_coddest)
    Me.pnTestataClforn.Controls.Add(Me.lbTm_conto)
    Me.pnTestataClforn.Controls.Add(Me.edTm_conto)
    Me.pnTestataClforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestataClforn.Location = New System.Drawing.Point(221, 1)
    Me.pnTestataClforn.Name = "pnTestataClforn"
    Me.pnTestataClforn.NTSActiveTrasparency = True
    Me.pnTestataClforn.Size = New System.Drawing.Size(526, 85)
    Me.pnTestataClforn.TabIndex = 48
    '
    'lbTm_datdoc
    '
    Me.lbTm_datdoc.AutoSize = True
    Me.lbTm_datdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_datdoc.Location = New System.Drawing.Point(7, 10)
    Me.lbTm_datdoc.Name = "lbTm_datdoc"
    Me.lbTm_datdoc.NTSDbField = ""
    Me.lbTm_datdoc.Size = New System.Drawing.Size(30, 13)
    Me.lbTm_datdoc.TabIndex = 5
    Me.lbTm_datdoc.Text = "Data"
    Me.lbTm_datdoc.Tooltip = ""
    Me.lbTm_datdoc.UseMnemonic = False
    '
    'lbXx_coddest
    '
    Me.lbXx_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddest.Location = New System.Drawing.Point(151, 59)
    Me.lbXx_coddest.Name = "lbXx_coddest"
    Me.lbXx_coddest.NTSDbField = ""
    Me.lbXx_coddest.Size = New System.Drawing.Size(316, 22)
    Me.lbXx_coddest.TabIndex = 47
    Me.lbXx_coddest.Tooltip = ""
    Me.lbXx_coddest.UseMnemonic = False
    '
    'edTm_datdoc
    '
    Me.edTm_datdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_datdoc.EditValue = "01/01/2008"
    Me.edTm_datdoc.Location = New System.Drawing.Point(60, 7)
    Me.edTm_datdoc.Name = "edTm_datdoc"
    Me.edTm_datdoc.NTSDbField = ""
    Me.edTm_datdoc.NTSForzaVisZoom = False
    Me.edTm_datdoc.NTSOldValue = ""
    Me.edTm_datdoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_datdoc.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_datdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_datdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_datdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_datdoc.Properties.MaxLength = 65536
    Me.edTm_datdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_datdoc.Size = New System.Drawing.Size(90, 20)
    Me.edTm_datdoc.TabIndex = 6
    '
    'lbTm_coddest
    '
    Me.lbTm_coddest.AutoSize = True
    Me.lbTm_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_coddest.Location = New System.Drawing.Point(7, 62)
    Me.lbTm_coddest.Name = "lbTm_coddest"
    Me.lbTm_coddest.NTSDbField = ""
    Me.lbTm_coddest.Size = New System.Drawing.Size(52, 13)
    Me.lbTm_coddest.TabIndex = 46
    Me.lbTm_coddest.Text = "Destinaz."
    Me.lbTm_coddest.Tooltip = ""
    Me.lbTm_coddest.UseMnemonic = False
    '
    'lbXx_conto
    '
    Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conto.Location = New System.Drawing.Point(151, 7)
    Me.lbXx_conto.Name = "lbXx_conto"
    Me.lbXx_conto.NTSDbField = ""
    Me.lbXx_conto.Size = New System.Drawing.Size(371, 46)
    Me.lbXx_conto.TabIndex = 7
    Me.lbXx_conto.Tooltip = ""
    Me.lbXx_conto.UseMnemonic = False
    '
    'edTm_coddest
    '
    Me.edTm_coddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_coddest.EditValue = "0"
    Me.edTm_coddest.Location = New System.Drawing.Point(96, 59)
    Me.edTm_coddest.Name = "edTm_coddest"
    Me.edTm_coddest.NTSDbField = ""
    Me.edTm_coddest.NTSFormat = "0"
    Me.edTm_coddest.NTSForzaVisZoom = False
    Me.edTm_coddest.NTSOldValue = ""
    Me.edTm_coddest.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_coddest.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_coddest.Properties.MaxLength = 65536
    Me.edTm_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_coddest.Size = New System.Drawing.Size(54, 20)
    Me.edTm_coddest.TabIndex = 45
    '
    'lbTm_conto
    '
    Me.lbTm_conto.AutoSize = True
    Me.lbTm_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_conto.Location = New System.Drawing.Point(7, 36)
    Me.lbTm_conto.Name = "lbTm_conto"
    Me.lbTm_conto.NTSDbField = ""
    Me.lbTm_conto.Size = New System.Drawing.Size(52, 13)
    Me.lbTm_conto.TabIndex = 43
    Me.lbTm_conto.Text = "Cli./Forn."
    Me.lbTm_conto.Tooltip = ""
    Me.lbTm_conto.UseMnemonic = False
    '
    'edTm_conto
    '
    Me.edTm_conto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_conto.EditValue = "0"
    Me.edTm_conto.Location = New System.Drawing.Point(60, 33)
    Me.edTm_conto.Name = "edTm_conto"
    Me.edTm_conto.NTSDbField = ""
    Me.edTm_conto.NTSFormat = "0"
    Me.edTm_conto.NTSForzaVisZoom = False
    Me.edTm_conto.NTSOldValue = ""
    Me.edTm_conto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_conto.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_conto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_conto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_conto.Properties.MaxLength = 65536
    Me.edTm_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_conto.Size = New System.Drawing.Size(90, 20)
    Me.edTm_conto.TabIndex = 44
    '
    'fmDocumento
    '
    Me.fmDocumento.AllowDrop = True
    Me.fmDocumento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDocumento.Appearance.Options.UseBackColor = True
    Me.fmDocumento.Controls.Add(Me.edAnnoDoc)
    Me.fmDocumento.Controls.Add(Me.cbTipoDoc)
    Me.fmDocumento.Controls.Add(Me.edSerieDoc)
    Me.fmDocumento.Controls.Add(Me.edNumDoc)
    Me.fmDocumento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDocumento.Location = New System.Drawing.Point(3, 3)
    Me.fmDocumento.Name = "fmDocumento"
    Me.fmDocumento.Size = New System.Drawing.Size(211, 79)
    Me.fmDocumento.TabIndex = 42
    Me.fmDocumento.Text = "Documento / anno / serie / numero"
    '
    'edAnnoDoc
    '
    Me.edAnnoDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoDoc.EditValue = "2008"
    Me.edAnnoDoc.Location = New System.Drawing.Point(7, 53)
    Me.edAnnoDoc.Name = "edAnnoDoc"
    Me.edAnnoDoc.NTSDbField = ""
    Me.edAnnoDoc.NTSFormat = "0"
    Me.edAnnoDoc.NTSForzaVisZoom = False
    Me.edAnnoDoc.NTSOldValue = "2008"
    Me.edAnnoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoDoc.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoDoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoDoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoDoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoDoc.Size = New System.Drawing.Size(54, 20)
    Me.edAnnoDoc.TabIndex = 42
    '
    'cbTipoDoc
    '
    Me.cbTipoDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoDoc.DataSource = Nothing
    Me.cbTipoDoc.DisplayMember = ""
    Me.cbTipoDoc.Location = New System.Drawing.Point(7, 22)
    Me.cbTipoDoc.Name = "cbTipoDoc"
    Me.cbTipoDoc.NTSDbField = ""
    Me.cbTipoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipoDoc.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoDoc.Properties.DropDownRows = 30
    Me.cbTipoDoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoDoc.SelectedValue = ""
    Me.cbTipoDoc.Size = New System.Drawing.Size(191, 20)
    Me.cbTipoDoc.TabIndex = 0
    Me.cbTipoDoc.ValueMember = ""
    '
    'edSerieDoc
    '
    Me.edSerieDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerieDoc.EditValue = " "
    Me.edSerieDoc.Location = New System.Drawing.Point(67, 53)
    Me.edSerieDoc.Name = "edSerieDoc"
    Me.edSerieDoc.NTSDbField = ""
    Me.edSerieDoc.NTSForzaVisZoom = False
    Me.edSerieDoc.NTSOldValue = " "
    Me.edSerieDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerieDoc.Properties.Appearance.Options.UseBackColor = True
    Me.edSerieDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerieDoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerieDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerieDoc.Properties.MaxLength = 65536
    Me.edSerieDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerieDoc.Size = New System.Drawing.Size(45, 20)
    Me.edSerieDoc.TabIndex = 2
    '
    'edNumDoc
    '
    Me.edNumDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumDoc.EditValue = "0"
    Me.edNumDoc.Location = New System.Drawing.Point(118, 53)
    Me.edNumDoc.Name = "edNumDoc"
    Me.edNumDoc.NTSDbField = ""
    Me.edNumDoc.NTSFormat = "0"
    Me.edNumDoc.NTSForzaVisZoom = False
    Me.edNumDoc.NTSOldValue = ""
    Me.edNumDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumDoc.Properties.Appearance.Options.UseBackColor = True
    Me.edNumDoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumDoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumDoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumDoc.Properties.MaxLength = 65536
    Me.edNumDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumDoc.Size = New System.Drawing.Size(80, 20)
    Me.edNumDoc.TabIndex = 4
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaPdf, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbFlussoDoc})
    Me.NtsBarManager1.MaxItemId = 89
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 1
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 2
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFlussoDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbFlussoDoc
    '
    Me.tlbFlussoDoc.Caption = "Apri analisi flusso documentale"
    Me.tlbFlussoDoc.Id = 88
    Me.tlbFlussoDoc.Name = "tlbFlussoDoc"
    Me.tlbFlussoDoc.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 9
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 10
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "StampaPdf"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.Id = 12
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'DockManager1
    '
    Me.DockManager1.Form = Me
    Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar"})
    '
    'TabPage4
    '
    Me.TabPage4.AllowDrop = True
    Me.TabPage4.Controls.Add(Me.pnNote)
    Me.TabPage4.Enable = True
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Size = New System.Drawing.Size(738, 373)
    Me.TabPage4.Text = "&4 - Note"
    '
    'pnNote
    '
    Me.pnNote.AllowDrop = True
    Me.pnNote.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNote.Appearance.Options.UseBackColor = True
    Me.pnNote.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNote.Controls.Add(Me.edTm_note)
    Me.pnNote.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnNote.Location = New System.Drawing.Point(0, 0)
    Me.pnNote.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnNote.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnNote.Name = "pnNote"
    Me.pnNote.NTSActiveTrasparency = True
    Me.pnNote.Size = New System.Drawing.Size(738, 373)
    Me.pnNote.TabIndex = 0
    '
    'edTm_note
    '
    Me.edTm_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edTm_note.Location = New System.Drawing.Point(0, 0)
    Me.edTm_note.Name = "edTm_note"
    Me.edTm_note.NTSDbField = ""
    Me.edTm_note.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.edTm_note.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_note.Size = New System.Drawing.Size(738, 373)
    Me.edTm_note.TabIndex = 1
    '
    'TabPage2
    '
    Me.TabPage2.AllowDrop = True
    Me.TabPage2.Controls.Add(Me.pnVarie)
    Me.TabPage2.Enable = True
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Size = New System.Drawing.Size(738, 373)
    Me.TabPage2.Text = "&2- DDT collegati / Varie"
    '
    'pnVarie
    '
    Me.pnVarie.AllowDrop = True
    Me.pnVarie.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnVarie.Appearance.Options.UseBackColor = True
    Me.pnVarie.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnVarie.Controls.Add(Me.pnPage4Fill)
    Me.pnVarie.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnVarie.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnVarie.Location = New System.Drawing.Point(0, 0)
    Me.pnVarie.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnVarie.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnVarie.Name = "pnVarie"
    Me.pnVarie.NTSActiveTrasparency = True
    Me.pnVarie.Size = New System.Drawing.Size(738, 373)
    Me.pnVarie.TabIndex = 0
    Me.pnVarie.Text = "NtsPanel1"
    '
    'pnPage4Fill
    '
    Me.pnPage4Fill.AllowDrop = True
    Me.pnPage4Fill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage4Fill.Appearance.Options.UseBackColor = True
    Me.pnPage4Fill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage4Fill.Controls.Add(Me.pnPage4Bottom)
    Me.pnPage4Fill.Controls.Add(Me.pnDDT)
    Me.pnPage4Fill.Controls.Add(Me.pnGrid)
    Me.pnPage4Fill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage4Fill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage4Fill.Location = New System.Drawing.Point(0, 0)
    Me.pnPage4Fill.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPage4Fill.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPage4Fill.Name = "pnPage4Fill"
    Me.pnPage4Fill.NTSActiveTrasparency = True
    Me.pnPage4Fill.Size = New System.Drawing.Size(738, 373)
    Me.pnPage4Fill.TabIndex = 94
    Me.pnPage4Fill.Text = "NtsPanel1"
    '
    'pnPage4Bottom
    '
    Me.pnPage4Bottom.AllowDrop = True
    Me.pnPage4Bottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage4Bottom.Appearance.Options.UseBackColor = True
    Me.pnPage4Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage4Bottom.Controls.Add(Me.fmContabilizzato)
    Me.pnPage4Bottom.Controls.Add(Me.fmIntrastat)
    Me.pnPage4Bottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage4Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnPage4Bottom.Location = New System.Drawing.Point(0, 264)
    Me.pnPage4Bottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPage4Bottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPage4Bottom.Name = "pnPage4Bottom"
    Me.pnPage4Bottom.NTSActiveTrasparency = True
    Me.pnPage4Bottom.Size = New System.Drawing.Size(738, 109)
    Me.pnPage4Bottom.TabIndex = 107
    Me.pnPage4Bottom.Text = "NtsPanel1"
    '
    'fmContabilizzato
    '
    Me.fmContabilizzato.AllowDrop = True
    Me.fmContabilizzato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmContabilizzato.Appearance.Options.UseBackColor = True
    Me.fmContabilizzato.Controls.Add(Me.edTm_nregiva)
    Me.fmContabilizzato.Controls.Add(Me.edTm_alfpro)
    Me.fmContabilizzato.Controls.Add(Me.lbTm_nregiva)
    Me.fmContabilizzato.Controls.Add(Me.edTm_numprot)
    Me.fmContabilizzato.Controls.Add(Me.lbTm_datregef)
    Me.fmContabilizzato.Controls.Add(Me.lbTm_numregef)
    Me.fmContabilizzato.Controls.Add(Me.edTm_datregef)
    Me.fmContabilizzato.Controls.Add(Me.edTm_numregef)
    Me.fmContabilizzato.Controls.Add(Me.ckTm_flcont)
    Me.fmContabilizzato.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmContabilizzato.Location = New System.Drawing.Point(6, 3)
    Me.fmContabilizzato.Name = "fmContabilizzato"
    Me.fmContabilizzato.Size = New System.Drawing.Size(230, 102)
    Me.fmContabilizzato.TabIndex = 108
    '
    'edTm_nregiva
    '
    Me.edTm_nregiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_nregiva.Enabled = False
    Me.edTm_nregiva.Location = New System.Drawing.Point(110, 76)
    Me.edTm_nregiva.Name = "edTm_nregiva"
    Me.edTm_nregiva.NTSDbField = ""
    Me.edTm_nregiva.NTSFormat = "0"
    Me.edTm_nregiva.NTSForzaVisZoom = False
    Me.edTm_nregiva.NTSOldValue = ""
    Me.edTm_nregiva.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_nregiva.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_nregiva.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_nregiva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_nregiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_nregiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_nregiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_nregiva.Properties.MaxLength = 65536
    Me.edTm_nregiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_nregiva.Size = New System.Drawing.Size(25, 20)
    Me.edTm_nregiva.TabIndex = 105
    '
    'edTm_alfpro
    '
    Me.edTm_alfpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_alfpro.EditValue = " "
    Me.edTm_alfpro.Enabled = False
    Me.edTm_alfpro.Location = New System.Drawing.Point(137, 76)
    Me.edTm_alfpro.Name = "edTm_alfpro"
    Me.edTm_alfpro.NTSDbField = ""
    Me.edTm_alfpro.NTSForzaVisZoom = False
    Me.edTm_alfpro.NTSOldValue = " "
    Me.edTm_alfpro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_alfpro.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_alfpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_alfpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_alfpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_alfpro.Properties.MaxLength = 65536
    Me.edTm_alfpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_alfpro.Size = New System.Drawing.Size(45, 20)
    Me.edTm_alfpro.TabIndex = 109
    '
    'lbTm_nregiva
    '
    Me.lbTm_nregiva.AutoSize = True
    Me.lbTm_nregiva.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_nregiva.Location = New System.Drawing.Point(3, 80)
    Me.lbTm_nregiva.Name = "lbTm_nregiva"
    Me.lbTm_nregiva.NTSDbField = ""
    Me.lbTm_nregiva.Size = New System.Drawing.Size(108, 13)
    Me.lbTm_nregiva.TabIndex = 107
    Me.lbTm_nregiva.Text = "Reg. IVA/S.Prot/Prot"
    Me.lbTm_nregiva.Tooltip = ""
    Me.lbTm_nregiva.UseMnemonic = False
    '
    'edTm_numprot
    '
    Me.edTm_numprot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_numprot.Enabled = False
    Me.edTm_numprot.Location = New System.Drawing.Point(184, 76)
    Me.edTm_numprot.Name = "edTm_numprot"
    Me.edTm_numprot.NTSDbField = ""
    Me.edTm_numprot.NTSFormat = "0"
    Me.edTm_numprot.NTSForzaVisZoom = False
    Me.edTm_numprot.NTSOldValue = ""
    Me.edTm_numprot.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_numprot.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_numprot.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_numprot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_numprot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_numprot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_numprot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_numprot.Properties.MaxLength = 65536
    Me.edTm_numprot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_numprot.Size = New System.Drawing.Size(41, 20)
    Me.edTm_numprot.TabIndex = 106
    '
    'lbTm_datregef
    '
    Me.lbTm_datregef.AutoSize = True
    Me.lbTm_datregef.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_datregef.Location = New System.Drawing.Point(3, 28)
    Me.lbTm_datregef.Name = "lbTm_datregef"
    Me.lbTm_datregef.NTSDbField = ""
    Me.lbTm_datregef.Size = New System.Drawing.Size(30, 13)
    Me.lbTm_datregef.TabIndex = 104
    Me.lbTm_datregef.Text = "Data"
    Me.lbTm_datregef.Tooltip = ""
    Me.lbTm_datregef.UseMnemonic = False
    '
    'lbTm_numregef
    '
    Me.lbTm_numregef.AutoSize = True
    Me.lbTm_numregef.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_numregef.Location = New System.Drawing.Point(3, 53)
    Me.lbTm_numregef.Name = "lbTm_numregef"
    Me.lbTm_numregef.NTSDbField = ""
    Me.lbTm_numregef.Size = New System.Drawing.Size(44, 13)
    Me.lbTm_numregef.TabIndex = 103
    Me.lbTm_numregef.Text = "Numero"
    Me.lbTm_numregef.Tooltip = ""
    Me.lbTm_numregef.UseMnemonic = False
    '
    'edTm_datregef
    '
    Me.edTm_datregef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_datregef.EditValue = "01/01/2008"
    Me.edTm_datregef.Enabled = False
    Me.edTm_datregef.Location = New System.Drawing.Point(110, 25)
    Me.edTm_datregef.Name = "edTm_datregef"
    Me.edTm_datregef.NTSDbField = ""
    Me.edTm_datregef.NTSForzaVisZoom = False
    Me.edTm_datregef.NTSOldValue = ""
    Me.edTm_datregef.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_datregef.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_datregef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_datregef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_datregef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_datregef.Properties.MaxLength = 65536
    Me.edTm_datregef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_datregef.Size = New System.Drawing.Size(115, 20)
    Me.edTm_datregef.TabIndex = 102
    '
    'edTm_numregef
    '
    Me.edTm_numregef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_numregef.Enabled = False
    Me.edTm_numregef.Location = New System.Drawing.Point(110, 50)
    Me.edTm_numregef.Name = "edTm_numregef"
    Me.edTm_numregef.NTSDbField = ""
    Me.edTm_numregef.NTSFormat = "0"
    Me.edTm_numregef.NTSForzaVisZoom = False
    Me.edTm_numregef.NTSOldValue = ""
    Me.edTm_numregef.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_numregef.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_numregef.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_numregef.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_numregef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_numregef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_numregef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_numregef.Properties.MaxLength = 65536
    Me.edTm_numregef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_numregef.Size = New System.Drawing.Size(115, 20)
    Me.edTm_numregef.TabIndex = 99
    '
    'ckTm_flcont
    '
    Me.ckTm_flcont.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckTm_flcont.Enabled = False
    Me.ckTm_flcont.Location = New System.Drawing.Point(0, 0)
    Me.ckTm_flcont.Name = "ckTm_flcont"
    Me.ckTm_flcont.NTSCheckValue = "S"
    Me.ckTm_flcont.NTSUnCheckValue = "N"
    Me.ckTm_flcont.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flcont.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flcont.Properties.Caption = "Fattura contabilizzata"
    Me.ckTm_flcont.Size = New System.Drawing.Size(135, 19)
    Me.ckTm_flcont.TabIndex = 93
    '
    'fmIntrastat
    '
    Me.fmIntrastat.AllowDrop = True
    Me.fmIntrastat.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIntrastat.Appearance.Options.UseBackColor = True
    Me.fmIntrastat.Controls.Add(Me.edTm_codport)
    Me.fmIntrastat.Controls.Add(Me.lbXx_codrsta)
    Me.fmIntrastat.Controls.Add(Me.edTm_codrsta)
    Me.fmIntrastat.Controls.Add(Me.lbTm_codrsta)
    Me.fmIntrastat.Controls.Add(Me.lbXx_codport)
    Me.fmIntrastat.Controls.Add(Me.lbTm_codport)
    Me.fmIntrastat.Controls.Add(Me.lbXx_codntra)
    Me.fmIntrastat.Controls.Add(Me.edTm_codntra)
    Me.fmIntrastat.Controls.Add(Me.lbTm_codntra)
    Me.fmIntrastat.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIntrastat.Location = New System.Drawing.Point(327, 3)
    Me.fmIntrastat.Name = "fmIntrastat"
    Me.fmIntrastat.Size = New System.Drawing.Size(277, 102)
    Me.fmIntrastat.TabIndex = 107
    Me.fmIntrastat.Text = "Intrastat"
    Me.fmIntrastat.Visible = False
    '
    'edTm_codport
    '
    Me.edTm_codport.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codport.EditValue = " "
    Me.edTm_codport.Enabled = False
    Me.edTm_codport.Location = New System.Drawing.Point(99, 50)
    Me.edTm_codport.Name = "edTm_codport"
    Me.edTm_codport.NTSDbField = ""
    Me.edTm_codport.NTSForzaVisZoom = False
    Me.edTm_codport.NTSOldValue = " "
    Me.edTm_codport.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codport.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codport.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codport.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codport.Properties.MaxLength = 65536
    Me.edTm_codport.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codport.Size = New System.Drawing.Size(47, 20)
    Me.edTm_codport.TabIndex = 108
    '
    'lbXx_codrsta
    '
    Me.lbXx_codrsta.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codrsta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codrsta.Location = New System.Drawing.Point(151, 73)
    Me.lbXx_codrsta.Name = "lbXx_codrsta"
    Me.lbXx_codrsta.NTSDbField = ""
    Me.lbXx_codrsta.Size = New System.Drawing.Size(121, 20)
    Me.lbXx_codrsta.TabIndex = 101
    Me.lbXx_codrsta.Tooltip = ""
    Me.lbXx_codrsta.UseMnemonic = False
    '
    'edTm_codrsta
    '
    Me.edTm_codrsta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codrsta.Enabled = False
    Me.edTm_codrsta.Location = New System.Drawing.Point(99, 73)
    Me.edTm_codrsta.Name = "edTm_codrsta"
    Me.edTm_codrsta.NTSDbField = ""
    Me.edTm_codrsta.NTSFormat = "0"
    Me.edTm_codrsta.NTSForzaVisZoom = False
    Me.edTm_codrsta.NTSOldValue = ""
    Me.edTm_codrsta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codrsta.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codrsta.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codrsta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codrsta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codrsta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codrsta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codrsta.Properties.MaxLength = 65536
    Me.edTm_codrsta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codrsta.Size = New System.Drawing.Size(47, 20)
    Me.edTm_codrsta.TabIndex = 100
    '
    'lbTm_codrsta
    '
    Me.lbTm_codrsta.AutoSize = True
    Me.lbTm_codrsta.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codrsta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_codrsta.Location = New System.Drawing.Point(6, 76)
    Me.lbTm_codrsta.Name = "lbTm_codrsta"
    Me.lbTm_codrsta.NTSDbField = ""
    Me.lbTm_codrsta.Size = New System.Drawing.Size(88, 13)
    Me.lbTm_codrsta.TabIndex = 99
    Me.lbTm_codrsta.Text = "Regime statistico"
    Me.lbTm_codrsta.Tooltip = ""
    Me.lbTm_codrsta.UseMnemonic = False
    '
    'lbXx_codport
    '
    Me.lbXx_codport.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codport.Location = New System.Drawing.Point(152, 50)
    Me.lbXx_codport.Name = "lbXx_codport"
    Me.lbXx_codport.NTSDbField = ""
    Me.lbXx_codport.Size = New System.Drawing.Size(120, 20)
    Me.lbXx_codport.TabIndex = 98
    Me.lbXx_codport.Tooltip = ""
    Me.lbXx_codport.UseMnemonic = False
    '
    'lbTm_codport
    '
    Me.lbTm_codport.AutoSize = True
    Me.lbTm_codport.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codport.Location = New System.Drawing.Point(6, 53)
    Me.lbTm_codport.Name = "lbTm_codport"
    Me.lbTm_codport.NTSDbField = ""
    Me.lbTm_codport.Size = New System.Drawing.Size(54, 13)
    Me.lbTm_codport.TabIndex = 97
    Me.lbTm_codport.Text = "Trasporto"
    Me.lbTm_codport.Tooltip = ""
    Me.lbTm_codport.UseMnemonic = False
    '
    'lbXx_codntra
    '
    Me.lbXx_codntra.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codntra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codntra.Location = New System.Drawing.Point(152, 27)
    Me.lbXx_codntra.Name = "lbXx_codntra"
    Me.lbXx_codntra.NTSDbField = ""
    Me.lbXx_codntra.Size = New System.Drawing.Size(120, 20)
    Me.lbXx_codntra.TabIndex = 95
    Me.lbXx_codntra.Tooltip = ""
    Me.lbXx_codntra.UseMnemonic = False
    '
    'edTm_codntra
    '
    Me.edTm_codntra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codntra.Enabled = False
    Me.edTm_codntra.Location = New System.Drawing.Point(99, 27)
    Me.edTm_codntra.Name = "edTm_codntra"
    Me.edTm_codntra.NTSDbField = ""
    Me.edTm_codntra.NTSFormat = "0"
    Me.edTm_codntra.NTSForzaVisZoom = False
    Me.edTm_codntra.NTSOldValue = ""
    Me.edTm_codntra.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codntra.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codntra.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codntra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codntra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codntra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codntra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codntra.Properties.MaxLength = 65536
    Me.edTm_codntra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codntra.Size = New System.Drawing.Size(47, 20)
    Me.edTm_codntra.TabIndex = 94
    '
    'lbTm_codntra
    '
    Me.lbTm_codntra.AutoSize = True
    Me.lbTm_codntra.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codntra.Location = New System.Drawing.Point(6, 30)
    Me.lbTm_codntra.Name = "lbTm_codntra"
    Me.lbTm_codntra.NTSDbField = ""
    Me.lbTm_codntra.Size = New System.Drawing.Size(83, 13)
    Me.lbTm_codntra.TabIndex = 93
    Me.lbTm_codntra.Text = "Natura transaz."
    Me.lbTm_codntra.Tooltip = ""
    Me.lbTm_codntra.UseMnemonic = False
    '
    'pnDDT
    '
    Me.pnDDT.AllowDrop = True
    Me.pnDDT.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDDT.Appearance.Options.UseBackColor = True
    Me.pnDDT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDDT.Controls.Add(Me.cmdDDTDel)
    Me.pnDDT.Controls.Add(Me.cmdDDTMod)
    Me.pnDDT.Controls.Add(Me.cmdDDTAdd)
    Me.pnDDT.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDDT.Location = New System.Drawing.Point(605, 0)
    Me.pnDDT.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnDDT.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnDDT.Name = "pnDDT"
    Me.pnDDT.NTSActiveTrasparency = True
    Me.pnDDT.Size = New System.Drawing.Size(133, 260)
    Me.pnDDT.TabIndex = 8
    Me.pnDDT.Text = "NtsPanel1"
    '
    'cmdDDTDel
    '
    Me.cmdDDTDel.ImageText = ""
    Me.cmdDDTDel.Location = New System.Drawing.Point(7, 71)
    Me.cmdDDTDel.Name = "cmdDDTDel"
    Me.cmdDDTDel.Size = New System.Drawing.Size(120, 28)
    Me.cmdDDTDel.TabIndex = 2
    Me.cmdDDTDel.Text = "&Rimuovi DDT"
    '
    'cmdDDTMod
    '
    Me.cmdDDTMod.ImageText = ""
    Me.cmdDDTMod.Location = New System.Drawing.Point(7, 37)
    Me.cmdDDTMod.Name = "cmdDDTMod"
    Me.cmdDDTMod.Size = New System.Drawing.Size(120, 28)
    Me.cmdDDTMod.TabIndex = 1
    Me.cmdDDTMod.Text = "&Modifica DDT"
    '
    'cmdDDTAdd
    '
    Me.cmdDDTAdd.ImageText = ""
    Me.cmdDDTAdd.Location = New System.Drawing.Point(7, 3)
    Me.cmdDDTAdd.Name = "cmdDDTAdd"
    Me.cmdDDTAdd.Size = New System.Drawing.Size(120, 28)
    Me.cmdDDTAdd.TabIndex = 0
    Me.cmdDDTAdd.Text = "&Aggiungi DDT"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grDDT)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Location = New System.Drawing.Point(6, 0)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(598, 260)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grDDT
    '
    Me.grDDT.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grDDT.EmbeddedNavigator.Name = ""
    Me.grDDT.Location = New System.Drawing.Point(0, 0)
    Me.grDDT.MainView = Me.grvDDT
    Me.grDDT.Name = "grDDT"
    Me.grDDT.Size = New System.Drawing.Size(598, 260)
    Me.grDDT.TabIndex = 6
    Me.grDDT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDDT})
    '
    'grvDDT
    '
    Me.grvDDT.ActiveFilterEnabled = False
    Me.grvDDT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tm_tipork, Me.tm_anno, Me.tm_serie, Me.tm_numdoc, Me.tm_datdoc, Me.tm_totdoc, Me.tm_valuta, Me.tm_cambio, Me.tm_annpar, Me.tm_alfpar, Me.tm_numpar, Me.tm_datpar, Me.tm_coddest, Me.tm_codagen, Me.tm_riferim})
    Me.grvDDT.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvDDT.Enabled = True
    Me.grvDDT.GridControl = Me.grDDT
    Me.grvDDT.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDDT.MinRowHeight = 14
    Me.grvDDT.Name = "grvDDT"
    Me.grvDDT.NTSAllowDelete = True
    Me.grvDDT.NTSAllowInsert = True
    Me.grvDDT.NTSAllowUpdate = True
    Me.grvDDT.NTSMenuContext = Nothing
    Me.grvDDT.OptionsCustomization.AllowRowSizing = True
    Me.grvDDT.OptionsFilter.AllowFilterEditor = False
    Me.grvDDT.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDDT.OptionsNavigation.UseTabKey = False
    Me.grvDDT.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDDT.OptionsView.ColumnAutoWidth = False
    Me.grvDDT.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDDT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDDT.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDDT.OptionsView.ShowGroupPanel = False
    Me.grvDDT.RowHeight = 16
    '
    'tm_tipork
    '
    Me.tm_tipork.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipork.Caption = "Tipo doc"
    Me.tm_tipork.Enabled = True
    Me.tm_tipork.FieldName = "tm_tipork"
    Me.tm_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipork.Name = "tm_tipork"
    Me.tm_tipork.NTSRepositoryComboBox = Nothing
    Me.tm_tipork.NTSRepositoryItemCheck = Nothing
    Me.tm_tipork.NTSRepositoryItemMemo = Nothing
    Me.tm_tipork.NTSRepositoryItemText = Nothing
    Me.tm_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipork.OptionsFilter.AllowFilter = False
    Me.tm_tipork.Visible = True
    Me.tm_tipork.VisibleIndex = 0
    Me.tm_tipork.Width = 70
    '
    'tm_anno
    '
    Me.tm_anno.AppearanceCell.Options.UseBackColor = True
    Me.tm_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tm_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_anno.Caption = "Anno doc"
    Me.tm_anno.Enabled = True
    Me.tm_anno.FieldName = "tm_anno"
    Me.tm_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_anno.Name = "tm_anno"
    Me.tm_anno.NTSRepositoryComboBox = Nothing
    Me.tm_anno.NTSRepositoryItemCheck = Nothing
    Me.tm_anno.NTSRepositoryItemMemo = Nothing
    Me.tm_anno.NTSRepositoryItemText = Nothing
    Me.tm_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_anno.OptionsFilter.AllowFilter = False
    Me.tm_anno.Visible = True
    Me.tm_anno.VisibleIndex = 1
    Me.tm_anno.Width = 70
    '
    'tm_serie
    '
    Me.tm_serie.AppearanceCell.Options.UseBackColor = True
    Me.tm_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tm_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_serie.Caption = "Serie doc"
    Me.tm_serie.Enabled = True
    Me.tm_serie.FieldName = "tm_serie"
    Me.tm_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_serie.Name = "tm_serie"
    Me.tm_serie.NTSRepositoryComboBox = Nothing
    Me.tm_serie.NTSRepositoryItemCheck = Nothing
    Me.tm_serie.NTSRepositoryItemMemo = Nothing
    Me.tm_serie.NTSRepositoryItemText = Nothing
    Me.tm_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_serie.OptionsFilter.AllowFilter = False
    Me.tm_serie.Visible = True
    Me.tm_serie.VisibleIndex = 2
    '
    'tm_numdoc
    '
    Me.tm_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numdoc.Caption = "Numero doc"
    Me.tm_numdoc.Enabled = True
    Me.tm_numdoc.FieldName = "tm_numdoc"
    Me.tm_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numdoc.Name = "tm_numdoc"
    Me.tm_numdoc.NTSRepositoryComboBox = Nothing
    Me.tm_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_numdoc.NTSRepositoryItemText = Nothing
    Me.tm_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numdoc.OptionsFilter.AllowFilter = False
    Me.tm_numdoc.Visible = True
    Me.tm_numdoc.VisibleIndex = 3
    '
    'tm_datdoc
    '
    Me.tm_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datdoc.Caption = "Data doc"
    Me.tm_datdoc.Enabled = True
    Me.tm_datdoc.FieldName = "tm_datdoc"
    Me.tm_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datdoc.Name = "tm_datdoc"
    Me.tm_datdoc.NTSRepositoryComboBox = Nothing
    Me.tm_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_datdoc.NTSRepositoryItemText = Nothing
    Me.tm_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datdoc.OptionsFilter.AllowFilter = False
    Me.tm_datdoc.Visible = True
    Me.tm_datdoc.VisibleIndex = 4
    '
    'tm_totdoc
    '
    Me.tm_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totdoc.Caption = "Totale doc"
    Me.tm_totdoc.Enabled = True
    Me.tm_totdoc.FieldName = "tm_totdoc"
    Me.tm_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totdoc.Name = "tm_totdoc"
    Me.tm_totdoc.NTSRepositoryComboBox = Nothing
    Me.tm_totdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_totdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_totdoc.NTSRepositoryItemText = Nothing
    Me.tm_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totdoc.OptionsFilter.AllowFilter = False
    Me.tm_totdoc.Visible = True
    Me.tm_totdoc.VisibleIndex = 5
    '
    'tm_valuta
    '
    Me.tm_valuta.AppearanceCell.Options.UseBackColor = True
    Me.tm_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.tm_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_valuta.Caption = "Valuta"
    Me.tm_valuta.Enabled = True
    Me.tm_valuta.FieldName = "tm_valuta"
    Me.tm_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_valuta.Name = "tm_valuta"
    Me.tm_valuta.NTSRepositoryComboBox = Nothing
    Me.tm_valuta.NTSRepositoryItemCheck = Nothing
    Me.tm_valuta.NTSRepositoryItemMemo = Nothing
    Me.tm_valuta.NTSRepositoryItemText = Nothing
    Me.tm_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_valuta.OptionsFilter.AllowFilter = False
    Me.tm_valuta.Visible = True
    Me.tm_valuta.VisibleIndex = 6
    '
    'tm_cambio
    '
    Me.tm_cambio.AppearanceCell.Options.UseBackColor = True
    Me.tm_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.tm_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_cambio.Caption = "Cambio"
    Me.tm_cambio.Enabled = True
    Me.tm_cambio.FieldName = "tm_cambio"
    Me.tm_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_cambio.Name = "tm_cambio"
    Me.tm_cambio.NTSRepositoryComboBox = Nothing
    Me.tm_cambio.NTSRepositoryItemCheck = Nothing
    Me.tm_cambio.NTSRepositoryItemMemo = Nothing
    Me.tm_cambio.NTSRepositoryItemText = Nothing
    Me.tm_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_cambio.OptionsFilter.AllowFilter = False
    Me.tm_cambio.Visible = True
    Me.tm_cambio.VisibleIndex = 7
    '
    'tm_annpar
    '
    Me.tm_annpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_annpar.Caption = "Anno Par"
    Me.tm_annpar.Enabled = True
    Me.tm_annpar.FieldName = "tm_annpar"
    Me.tm_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_annpar.Name = "tm_annpar"
    Me.tm_annpar.NTSRepositoryComboBox = Nothing
    Me.tm_annpar.NTSRepositoryItemCheck = Nothing
    Me.tm_annpar.NTSRepositoryItemMemo = Nothing
    Me.tm_annpar.NTSRepositoryItemText = Nothing
    Me.tm_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_annpar.OptionsFilter.AllowFilter = False
    Me.tm_annpar.Visible = True
    Me.tm_annpar.VisibleIndex = 8
    '
    'tm_alfpar
    '
    Me.tm_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_alfpar.Caption = "Serie par"
    Me.tm_alfpar.Enabled = True
    Me.tm_alfpar.FieldName = "tm_alfpar"
    Me.tm_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_alfpar.Name = "tm_alfpar"
    Me.tm_alfpar.NTSRepositoryComboBox = Nothing
    Me.tm_alfpar.NTSRepositoryItemCheck = Nothing
    Me.tm_alfpar.NTSRepositoryItemMemo = Nothing
    Me.tm_alfpar.NTSRepositoryItemText = Nothing
    Me.tm_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_alfpar.OptionsFilter.AllowFilter = False
    Me.tm_alfpar.Visible = True
    Me.tm_alfpar.VisibleIndex = 9
    '
    'tm_numpar
    '
    Me.tm_numpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numpar.Caption = "Numero par"
    Me.tm_numpar.Enabled = True
    Me.tm_numpar.FieldName = "tm_numpar"
    Me.tm_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numpar.Name = "tm_numpar"
    Me.tm_numpar.NTSRepositoryComboBox = Nothing
    Me.tm_numpar.NTSRepositoryItemCheck = Nothing
    Me.tm_numpar.NTSRepositoryItemMemo = Nothing
    Me.tm_numpar.NTSRepositoryItemText = Nothing
    Me.tm_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numpar.OptionsFilter.AllowFilter = False
    Me.tm_numpar.Visible = True
    Me.tm_numpar.VisibleIndex = 10
    '
    'tm_datpar
    '
    Me.tm_datpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datpar.Caption = "Data par"
    Me.tm_datpar.Enabled = True
    Me.tm_datpar.FieldName = "tm_datpar"
    Me.tm_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datpar.Name = "tm_datpar"
    Me.tm_datpar.NTSRepositoryComboBox = Nothing
    Me.tm_datpar.NTSRepositoryItemCheck = Nothing
    Me.tm_datpar.NTSRepositoryItemMemo = Nothing
    Me.tm_datpar.NTSRepositoryItemText = Nothing
    Me.tm_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datpar.OptionsFilter.AllowFilter = False
    Me.tm_datpar.Visible = True
    Me.tm_datpar.VisibleIndex = 11
    '
    'tm_coddest
    '
    Me.tm_coddest.AppearanceCell.Options.UseBackColor = True
    Me.tm_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.tm_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_coddest.Caption = "Destinaz. 1"
    Me.tm_coddest.Enabled = True
    Me.tm_coddest.FieldName = "tm_coddest"
    Me.tm_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_coddest.Name = "tm_coddest"
    Me.tm_coddest.NTSRepositoryComboBox = Nothing
    Me.tm_coddest.NTSRepositoryItemCheck = Nothing
    Me.tm_coddest.NTSRepositoryItemMemo = Nothing
    Me.tm_coddest.NTSRepositoryItemText = Nothing
    Me.tm_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_coddest.OptionsFilter.AllowFilter = False
    Me.tm_coddest.Visible = True
    Me.tm_coddest.VisibleIndex = 12
    '
    'tm_codagen
    '
    Me.tm_codagen.AppearanceCell.Options.UseBackColor = True
    Me.tm_codagen.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codagen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codagen.Caption = "Agente"
    Me.tm_codagen.Enabled = True
    Me.tm_codagen.FieldName = "tm_codagen"
    Me.tm_codagen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codagen.Name = "tm_codagen"
    Me.tm_codagen.NTSRepositoryComboBox = Nothing
    Me.tm_codagen.NTSRepositoryItemCheck = Nothing
    Me.tm_codagen.NTSRepositoryItemMemo = Nothing
    Me.tm_codagen.NTSRepositoryItemText = Nothing
    Me.tm_codagen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codagen.OptionsFilter.AllowFilter = False
    Me.tm_codagen.Visible = True
    Me.tm_codagen.VisibleIndex = 13
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 14
    '
    'TabPage3
    '
    Me.TabPage3.AllowDrop = True
    Me.TabPage3.Controls.Add(Me.pnCmd)
    Me.TabPage3.Controls.Add(Me.fmProvvigioni)
    Me.TabPage3.Controls.Add(Me.fmPartita)
    Me.TabPage3.Controls.Add(Me.fmTrasporto)
    Me.TabPage3.Controls.Add(Me.fmSpese)
    Me.TabPage3.Enable = True
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Size = New System.Drawing.Size(738, 373)
    Me.TabPage3.Text = "&3 - Piede"
    '
    'pnCmd
    '
    Me.pnCmd.AllowDrop = True
    Me.pnCmd.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCmd.Appearance.Options.UseBackColor = True
    Me.pnCmd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCmd.Controls.Add(Me.cmdCastelletti)
    Me.pnCmd.Controls.Add(Me.cmdModifScad)
    Me.pnCmd.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCmd.Location = New System.Drawing.Point(166, 347)
    Me.pnCmd.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCmd.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCmd.Name = "pnCmd"
    Me.pnCmd.NTSActiveTrasparency = True
    Me.pnCmd.Size = New System.Drawing.Size(302, 25)
    Me.pnCmd.TabIndex = 83
    Me.pnCmd.Text = "NtsPanel1"
    '
    'cmdCastelletti
    '
    Me.cmdCastelletti.ImageText = ""
    Me.cmdCastelletti.Location = New System.Drawing.Point(212, 2)
    Me.cmdCastelletti.Name = "cmdCastelletti"
    Me.cmdCastelletti.Size = New System.Drawing.Size(90, 23)
    Me.cmdCastelletti.TabIndex = 83
    Me.cmdCastelletti.Text = "Castelletti"
    '
    'cmdModifScad
    '
    Me.cmdModifScad.ImageText = ""
    Me.cmdModifScad.Location = New System.Drawing.Point(99, 2)
    Me.cmdModifScad.Name = "cmdModifScad"
    Me.cmdModifScad.Size = New System.Drawing.Size(104, 23)
    Me.cmdModifScad.TabIndex = 81
    Me.cmdModifScad.Text = "Modifica scadenze"
    '
    'fmProvvigioni
    '
    Me.fmProvvigioni.AllowDrop = True
    Me.fmProvvigioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmProvvigioni.Appearance.Options.UseBackColor = True
    Me.fmProvvigioni.Controls.Add(Me.ckTm_flprov)
    Me.fmProvvigioni.Controls.Add(Me.edTm_totprov)
    Me.fmProvvigioni.Controls.Add(Me.lbTm_totprov)
    Me.fmProvvigioni.Controls.Add(Me.edTm_impprov)
    Me.fmProvvigioni.Controls.Add(Me.lbTm_impprov)
    Me.fmProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmProvvigioni.Location = New System.Drawing.Point(5, 196)
    Me.fmProvvigioni.Name = "fmProvvigioni"
    Me.fmProvvigioni.Size = New System.Drawing.Size(407, 49)
    Me.fmProvvigioni.TabIndex = 3
    '
    'ckTm_flprov
    '
    Me.ckTm_flprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_flprov.Location = New System.Drawing.Point(3, 0)
    Me.ckTm_flprov.Name = "ckTm_flprov"
    Me.ckTm_flprov.NTSCheckValue = "S"
    Me.ckTm_flprov.NTSUnCheckValue = "N"
    Me.ckTm_flprov.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flprov.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flprov.Properties.Caption = "Provvigioni estratte"
    Me.ckTm_flprov.Size = New System.Drawing.Size(115, 19)
    Me.ckTm_flprov.TabIndex = 94
    '
    'edTm_totprov
    '
    Me.edTm_totprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totprov.Enabled = False
    Me.edTm_totprov.Location = New System.Drawing.Point(325, 22)
    Me.edTm_totprov.Name = "edTm_totprov"
    Me.edTm_totprov.NTSDbField = ""
    Me.edTm_totprov.NTSFormat = "0"
    Me.edTm_totprov.NTSForzaVisZoom = False
    Me.edTm_totprov.NTSOldValue = ""
    Me.edTm_totprov.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totprov.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totprov.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totprov.Properties.MaxLength = 65536
    Me.edTm_totprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totprov.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totprov.TabIndex = 55
    '
    'lbTm_totprov
    '
    Me.lbTm_totprov.AutoSize = True
    Me.lbTm_totprov.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_totprov.Location = New System.Drawing.Point(242, 25)
    Me.lbTm_totprov.Name = "lbTm_totprov"
    Me.lbTm_totprov.NTSDbField = ""
    Me.lbTm_totprov.Size = New System.Drawing.Size(76, 13)
    Me.lbTm_totprov.TabIndex = 54
    Me.lbTm_totprov.Text = "Provv. agente"
    Me.lbTm_totprov.Tooltip = ""
    Me.lbTm_totprov.UseMnemonic = False
    '
    'edTm_impprov
    '
    Me.edTm_impprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_impprov.Enabled = False
    Me.edTm_impprov.Location = New System.Drawing.Point(66, 22)
    Me.edTm_impprov.Name = "edTm_impprov"
    Me.edTm_impprov.NTSDbField = ""
    Me.edTm_impprov.NTSFormat = "0"
    Me.edTm_impprov.NTSForzaVisZoom = False
    Me.edTm_impprov.NTSOldValue = ""
    Me.edTm_impprov.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_impprov.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_impprov.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_impprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_impprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_impprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_impprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_impprov.Properties.MaxLength = 65536
    Me.edTm_impprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_impprov.Size = New System.Drawing.Size(81, 20)
    Me.edTm_impprov.TabIndex = 53
    '
    'lbTm_impprov
    '
    Me.lbTm_impprov.AutoSize = True
    Me.lbTm_impprov.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_impprov.Location = New System.Drawing.Point(4, 25)
    Me.lbTm_impprov.Name = "lbTm_impprov"
    Me.lbTm_impprov.NTSDbField = ""
    Me.lbTm_impprov.Size = New System.Drawing.Size(55, 13)
    Me.lbTm_impprov.TabIndex = 52
    Me.lbTm_impprov.Text = "Imponibile"
    Me.lbTm_impprov.Tooltip = ""
    Me.lbTm_impprov.UseMnemonic = False
    '
    'fmPartita
    '
    Me.fmPartita.AllowDrop = True
    Me.fmPartita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPartita.Appearance.Options.UseBackColor = True
    Me.fmPartita.Controls.Add(Me.lbTm_datpar)
    Me.fmPartita.Controls.Add(Me.edTm_datpar)
    Me.fmPartita.Controls.Add(Me.edTm_numpar)
    Me.fmPartita.Controls.Add(Me.lbTm_numpar)
    Me.fmPartita.Controls.Add(Me.edTm_alfpar)
    Me.fmPartita.Controls.Add(Me.lbTm_annpar)
    Me.fmPartita.Controls.Add(Me.edTm_annpar)
    Me.fmPartita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPartita.Location = New System.Drawing.Point(5, 267)
    Me.fmPartita.Name = "fmPartita"
    Me.fmPartita.Size = New System.Drawing.Size(160, 105)
    Me.fmPartita.TabIndex = 2
    Me.fmPartita.Text = "Partita doc. ricevuto"
    '
    'lbTm_datpar
    '
    Me.lbTm_datpar.AutoSize = True
    Me.lbTm_datpar.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_datpar.Location = New System.Drawing.Point(5, 76)
    Me.lbTm_datpar.Name = "lbTm_datpar"
    Me.lbTm_datpar.NTSDbField = ""
    Me.lbTm_datpar.Size = New System.Drawing.Size(30, 13)
    Me.lbTm_datpar.TabIndex = 74
    Me.lbTm_datpar.Text = "Data"
    Me.lbTm_datpar.Tooltip = ""
    Me.lbTm_datpar.UseMnemonic = False
    '
    'edTm_datpar
    '
    Me.edTm_datpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_datpar.Location = New System.Drawing.Point(67, 73)
    Me.edTm_datpar.Name = "edTm_datpar"
    Me.edTm_datpar.NTSDbField = ""
    Me.edTm_datpar.NTSForzaVisZoom = False
    Me.edTm_datpar.NTSOldValue = ""
    Me.edTm_datpar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_datpar.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_datpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_datpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_datpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_datpar.Properties.MaxLength = 65536
    Me.edTm_datpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_datpar.Size = New System.Drawing.Size(90, 20)
    Me.edTm_datpar.TabIndex = 73
    '
    'edTm_numpar
    '
    Me.edTm_numpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_numpar.EditValue = ""
    Me.edTm_numpar.Location = New System.Drawing.Point(67, 50)
    Me.edTm_numpar.Name = "edTm_numpar"
    Me.edTm_numpar.NTSDbField = ""
    Me.edTm_numpar.NTSFormat = "0"
    Me.edTm_numpar.NTSForzaVisZoom = False
    Me.edTm_numpar.NTSOldValue = ""
    Me.edTm_numpar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_numpar.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_numpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_numpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_numpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_numpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_numpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_numpar.Properties.MaxLength = 65536
    Me.edTm_numpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_numpar.Size = New System.Drawing.Size(90, 20)
    Me.edTm_numpar.TabIndex = 72
    '
    'lbTm_numpar
    '
    Me.lbTm_numpar.AutoSize = True
    Me.lbTm_numpar.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_numpar.Location = New System.Drawing.Point(5, 53)
    Me.lbTm_numpar.Name = "lbTm_numpar"
    Me.lbTm_numpar.NTSDbField = ""
    Me.lbTm_numpar.Size = New System.Drawing.Size(44, 13)
    Me.lbTm_numpar.TabIndex = 71
    Me.lbTm_numpar.Text = "Numero"
    Me.lbTm_numpar.Tooltip = ""
    Me.lbTm_numpar.UseMnemonic = False
    '
    'edTm_alfpar
    '
    Me.edTm_alfpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_alfpar.Location = New System.Drawing.Point(112, 26)
    Me.edTm_alfpar.Name = "edTm_alfpar"
    Me.edTm_alfpar.NTSDbField = ""
    Me.edTm_alfpar.NTSForzaVisZoom = False
    Me.edTm_alfpar.NTSOldValue = ""
    Me.edTm_alfpar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_alfpar.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_alfpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_alfpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_alfpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_alfpar.Properties.MaxLength = 65536
    Me.edTm_alfpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_alfpar.Size = New System.Drawing.Size(45, 20)
    Me.edTm_alfpar.TabIndex = 70
    '
    'lbTm_annpar
    '
    Me.lbTm_annpar.AutoSize = True
    Me.lbTm_annpar.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_annpar.Location = New System.Drawing.Point(5, 29)
    Me.lbTm_annpar.Name = "lbTm_annpar"
    Me.lbTm_annpar.NTSDbField = ""
    Me.lbTm_annpar.Size = New System.Drawing.Size(59, 13)
    Me.lbTm_annpar.TabIndex = 68
    Me.lbTm_annpar.Text = "Anno/serie"
    Me.lbTm_annpar.Tooltip = ""
    Me.lbTm_annpar.UseMnemonic = False
    '
    'edTm_annpar
    '
    Me.edTm_annpar.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTm_annpar.EditValue = ""
    Me.edTm_annpar.Location = New System.Drawing.Point(67, 26)
    Me.edTm_annpar.Name = "edTm_annpar"
    Me.edTm_annpar.NTSDbField = ""
    Me.edTm_annpar.NTSFormat = "0"
    Me.edTm_annpar.NTSForzaVisZoom = False
    Me.edTm_annpar.NTSOldValue = ""
    Me.edTm_annpar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_annpar.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_annpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_annpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_annpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_annpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_annpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_annpar.Properties.MaxLength = 65536
    Me.edTm_annpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_annpar.Size = New System.Drawing.Size(42, 20)
    Me.edTm_annpar.TabIndex = 43
    '
    'fmTrasporto
    '
    Me.fmTrasporto.AllowDrop = True
    Me.fmTrasporto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTrasporto.Appearance.Options.UseBackColor = True
    Me.fmTrasporto.Controls.Add(Me.edTotQuant)
    Me.fmTrasporto.Controls.Add(Me.lbTm_acuradi)
    Me.fmTrasporto.Controls.Add(Me.edTm_aspetto)
    Me.fmTrasporto.Controls.Add(Me.edTm_pesonetto)
    Me.fmTrasporto.Controls.Add(Me.lbPesoNettoLordo)
    Me.fmTrasporto.Controls.Add(Me.edTm_peso)
    Me.fmTrasporto.Controls.Add(Me.edTm_totcoll)
    Me.fmTrasporto.Controls.Add(Me.lbTm_totcoll)
    Me.fmTrasporto.Controls.Add(Me.lbXx_porto)
    Me.fmTrasporto.Controls.Add(Me.edTm_porto)
    Me.fmTrasporto.Controls.Add(Me.lbTm_porto)
    Me.fmTrasporto.Controls.Add(Me.cbTm_acuradi)
    Me.fmTrasporto.Controls.Add(Me.lbXx_vettor2)
    Me.fmTrasporto.Controls.Add(Me.edTm_vettor2)
    Me.fmTrasporto.Controls.Add(Me.lbTm_vettor2)
    Me.fmTrasporto.Controls.Add(Me.lbXx_coddest2)
    Me.fmTrasporto.Controls.Add(Me.edTm_coddest2)
    Me.fmTrasporto.Controls.Add(Me.lbTm_coddest2)
    Me.fmTrasporto.Controls.Add(Me.edTm_codaspe)
    Me.fmTrasporto.Controls.Add(Me.lbTm_codaspe)
    Me.fmTrasporto.Controls.Add(Me.lbXx_caustra)
    Me.fmTrasporto.Controls.Add(Me.edTm_caustra)
    Me.fmTrasporto.Controls.Add(Me.lbTm_caustra)
    Me.fmTrasporto.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTrasporto.Location = New System.Drawing.Point(5, 4)
    Me.fmTrasporto.Name = "fmTrasporto"
    Me.fmTrasporto.Size = New System.Drawing.Size(407, 177)
    Me.fmTrasporto.TabIndex = 1
    Me.fmTrasporto.Text = "Trasporto"
    '
    'edTotQuant
    '
    Me.edTotQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotQuant.Enabled = False
    Me.edTotQuant.Location = New System.Drawing.Point(148, 153)
    Me.edTotQuant.Name = "edTotQuant"
    Me.edTotQuant.NTSDbField = ""
    Me.edTotQuant.NTSFormat = "0"
    Me.edTotQuant.NTSForzaVisZoom = False
    Me.edTotQuant.NTSOldValue = ""
    Me.edTotQuant.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotQuant.Properties.Appearance.Options.UseBackColor = True
    Me.edTotQuant.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotQuant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotQuant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotQuant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotQuant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotQuant.Properties.MaxLength = 65536
    Me.edTotQuant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotQuant.Size = New System.Drawing.Size(72, 20)
    Me.edTotQuant.TabIndex = 75
    '
    'lbTm_acuradi
    '
    Me.lbTm_acuradi.AutoSize = True
    Me.lbTm_acuradi.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_acuradi.Location = New System.Drawing.Point(5, 66)
    Me.lbTm_acuradi.Name = "lbTm_acuradi"
    Me.lbTm_acuradi.NTSDbField = ""
    Me.lbTm_acuradi.Size = New System.Drawing.Size(88, 13)
    Me.lbTm_acuradi.TabIndex = 73
    Me.lbTm_acuradi.Text = "Trasp. a cura del"
    Me.lbTm_acuradi.Tooltip = ""
    Me.lbTm_acuradi.UseMnemonic = False
    '
    'edTm_aspetto
    '
    Me.edTm_aspetto.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTm_aspetto.Location = New System.Drawing.Point(150, 41)
    Me.edTm_aspetto.Name = "edTm_aspetto"
    Me.edTm_aspetto.NTSDbField = ""
    Me.edTm_aspetto.NTSForzaVisZoom = False
    Me.edTm_aspetto.NTSOldValue = ""
    Me.edTm_aspetto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_aspetto.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_aspetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_aspetto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_aspetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_aspetto.Properties.MaxLength = 65536
    Me.edTm_aspetto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_aspetto.Size = New System.Drawing.Size(252, 20)
    Me.edTm_aspetto.TabIndex = 72
    '
    'edTm_pesonetto
    '
    Me.edTm_pesonetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_pesonetto.Enabled = False
    Me.edTm_pesonetto.Location = New System.Drawing.Point(280, 153)
    Me.edTm_pesonetto.Name = "edTm_pesonetto"
    Me.edTm_pesonetto.NTSDbField = ""
    Me.edTm_pesonetto.NTSFormat = "0"
    Me.edTm_pesonetto.NTSForzaVisZoom = False
    Me.edTm_pesonetto.NTSOldValue = ""
    Me.edTm_pesonetto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_pesonetto.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_pesonetto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_pesonetto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_pesonetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_pesonetto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_pesonetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_pesonetto.Properties.MaxLength = 65536
    Me.edTm_pesonetto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_pesonetto.Size = New System.Drawing.Size(60, 20)
    Me.edTm_pesonetto.TabIndex = 71
    '
    'lbPesoNettoLordo
    '
    Me.lbPesoNettoLordo.AutoSize = True
    Me.lbPesoNettoLordo.BackColor = System.Drawing.Color.Transparent
    Me.lbPesoNettoLordo.Location = New System.Drawing.Point(222, 156)
    Me.lbPesoNettoLordo.Name = "lbPesoNettoLordo"
    Me.lbPesoNettoLordo.NTSDbField = ""
    Me.lbPesoNettoLordo.Size = New System.Drawing.Size(59, 13)
    Me.lbPesoNettoLordo.TabIndex = 70
    Me.lbPesoNettoLordo.Text = "Pes net/lor"
    Me.lbPesoNettoLordo.Tooltip = ""
    Me.lbPesoNettoLordo.UseMnemonic = False
    '
    'edTm_peso
    '
    Me.edTm_peso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_peso.Enabled = False
    Me.edTm_peso.Location = New System.Drawing.Point(341, 153)
    Me.edTm_peso.Name = "edTm_peso"
    Me.edTm_peso.NTSDbField = ""
    Me.edTm_peso.NTSFormat = "0"
    Me.edTm_peso.NTSForzaVisZoom = False
    Me.edTm_peso.NTSOldValue = ""
    Me.edTm_peso.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_peso.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_peso.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_peso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_peso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_peso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_peso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_peso.Properties.MaxLength = 65536
    Me.edTm_peso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_peso.Size = New System.Drawing.Size(60, 20)
    Me.edTm_peso.TabIndex = 69
    '
    'edTm_totcoll
    '
    Me.edTm_totcoll.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totcoll.Enabled = False
    Me.edTm_totcoll.Location = New System.Drawing.Point(96, 153)
    Me.edTm_totcoll.Name = "edTm_totcoll"
    Me.edTm_totcoll.NTSDbField = ""
    Me.edTm_totcoll.NTSFormat = "0"
    Me.edTm_totcoll.NTSForzaVisZoom = False
    Me.edTm_totcoll.NTSOldValue = ""
    Me.edTm_totcoll.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totcoll.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totcoll.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totcoll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totcoll.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totcoll.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totcoll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totcoll.Properties.MaxLength = 65536
    Me.edTm_totcoll.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totcoll.Size = New System.Drawing.Size(52, 20)
    Me.edTm_totcoll.TabIndex = 68
    '
    'lbTm_totcoll
    '
    Me.lbTm_totcoll.AutoSize = True
    Me.lbTm_totcoll.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_totcoll.Location = New System.Drawing.Point(5, 156)
    Me.lbTm_totcoll.Name = "lbTm_totcoll"
    Me.lbTm_totcoll.NTSDbField = ""
    Me.lbTm_totcoll.Size = New System.Drawing.Size(63, 13)
    Me.lbTm_totcoll.TabIndex = 67
    Me.lbTm_totcoll.Text = "Tot colli/qtà"
    Me.lbTm_totcoll.Tooltip = ""
    Me.lbTm_totcoll.UseMnemonic = False
    '
    'lbXx_porto
    '
    Me.lbXx_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_porto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_porto.Location = New System.Drawing.Point(149, 132)
    Me.lbXx_porto.Name = "lbXx_porto"
    Me.lbXx_porto.NTSDbField = ""
    Me.lbXx_porto.Size = New System.Drawing.Size(253, 20)
    Me.lbXx_porto.TabIndex = 66
    Me.lbXx_porto.Tooltip = ""
    Me.lbXx_porto.UseMnemonic = False
    '
    'edTm_porto
    '
    Me.edTm_porto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_porto.Location = New System.Drawing.Point(96, 132)
    Me.edTm_porto.Name = "edTm_porto"
    Me.edTm_porto.NTSDbField = ""
    Me.edTm_porto.NTSForzaVisZoom = False
    Me.edTm_porto.NTSOldValue = ""
    Me.edTm_porto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_porto.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_porto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_porto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_porto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_porto.Properties.MaxLength = 65536
    Me.edTm_porto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_porto.Size = New System.Drawing.Size(52, 20)
    Me.edTm_porto.TabIndex = 65
    '
    'lbTm_porto
    '
    Me.lbTm_porto.AutoSize = True
    Me.lbTm_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_porto.Location = New System.Drawing.Point(5, 135)
    Me.lbTm_porto.Name = "lbTm_porto"
    Me.lbTm_porto.NTSDbField = ""
    Me.lbTm_porto.Size = New System.Drawing.Size(33, 13)
    Me.lbTm_porto.TabIndex = 64
    Me.lbTm_porto.Text = "Porto"
    Me.lbTm_porto.Tooltip = ""
    Me.lbTm_porto.UseMnemonic = False
    '
    'cbTm_acuradi
    '
    Me.cbTm_acuradi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTm_acuradi.DataSource = Nothing
    Me.cbTm_acuradi.DisplayMember = ""
    Me.cbTm_acuradi.Location = New System.Drawing.Point(96, 63)
    Me.cbTm_acuradi.Name = "cbTm_acuradi"
    Me.cbTm_acuradi.NTSDbField = ""
    Me.cbTm_acuradi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTm_acuradi.Properties.Appearance.Options.UseBackColor = True
    Me.cbTm_acuradi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTm_acuradi.Properties.DropDownRows = 30
    Me.cbTm_acuradi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTm_acuradi.SelectedValue = ""
    Me.cbTm_acuradi.Size = New System.Drawing.Size(306, 20)
    Me.cbTm_acuradi.TabIndex = 63
    Me.cbTm_acuradi.ValueMember = ""
    '
    'lbXx_vettor2
    '
    Me.lbXx_vettor2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vettor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vettor2.Location = New System.Drawing.Point(149, 110)
    Me.lbXx_vettor2.Name = "lbXx_vettor2"
    Me.lbXx_vettor2.NTSDbField = ""
    Me.lbXx_vettor2.Size = New System.Drawing.Size(253, 20)
    Me.lbXx_vettor2.TabIndex = 61
    Me.lbXx_vettor2.Tooltip = ""
    Me.lbXx_vettor2.UseMnemonic = False
    '
    'edTm_vettor2
    '
    Me.edTm_vettor2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_vettor2.Location = New System.Drawing.Point(96, 110)
    Me.edTm_vettor2.Name = "edTm_vettor2"
    Me.edTm_vettor2.NTSDbField = ""
    Me.edTm_vettor2.NTSFormat = "0"
    Me.edTm_vettor2.NTSForzaVisZoom = False
    Me.edTm_vettor2.NTSOldValue = ""
    Me.edTm_vettor2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_vettor2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_vettor2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_vettor2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_vettor2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_vettor2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_vettor2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_vettor2.Properties.MaxLength = 65536
    Me.edTm_vettor2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_vettor2.Size = New System.Drawing.Size(52, 20)
    Me.edTm_vettor2.TabIndex = 60
    '
    'lbTm_vettor2
    '
    Me.lbTm_vettor2.AutoSize = True
    Me.lbTm_vettor2.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_vettor2.Location = New System.Drawing.Point(5, 113)
    Me.lbTm_vettor2.Name = "lbTm_vettor2"
    Me.lbTm_vettor2.NTSDbField = ""
    Me.lbTm_vettor2.Size = New System.Drawing.Size(52, 13)
    Me.lbTm_vettor2.TabIndex = 59
    Me.lbTm_vettor2.Text = "Vettore 2"
    Me.lbTm_vettor2.Tooltip = ""
    Me.lbTm_vettor2.UseMnemonic = False
    '
    'lbXx_coddest2
    '
    Me.lbXx_coddest2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddest2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddest2.Location = New System.Drawing.Point(149, 87)
    Me.lbXx_coddest2.Name = "lbXx_coddest2"
    Me.lbXx_coddest2.NTSDbField = ""
    Me.lbXx_coddest2.Size = New System.Drawing.Size(253, 20)
    Me.lbXx_coddest2.TabIndex = 58
    Me.lbXx_coddest2.Tooltip = ""
    Me.lbXx_coddest2.UseMnemonic = False
    '
    'edTm_coddest2
    '
    Me.edTm_coddest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_coddest2.Location = New System.Drawing.Point(96, 87)
    Me.edTm_coddest2.Name = "edTm_coddest2"
    Me.edTm_coddest2.NTSDbField = ""
    Me.edTm_coddest2.NTSFormat = "0"
    Me.edTm_coddest2.NTSForzaVisZoom = False
    Me.edTm_coddest2.NTSOldValue = ""
    Me.edTm_coddest2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_coddest2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_coddest2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_coddest2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_coddest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_coddest2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_coddest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_coddest2.Properties.MaxLength = 65536
    Me.edTm_coddest2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_coddest2.Size = New System.Drawing.Size(52, 20)
    Me.edTm_coddest2.TabIndex = 57
    '
    'lbTm_coddest2
    '
    Me.lbTm_coddest2.AutoSize = True
    Me.lbTm_coddest2.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_coddest2.Location = New System.Drawing.Point(5, 90)
    Me.lbTm_coddest2.Name = "lbTm_coddest2"
    Me.lbTm_coddest2.NTSDbField = ""
    Me.lbTm_coddest2.Size = New System.Drawing.Size(88, 13)
    Me.lbTm_coddest2.TabIndex = 56
    Me.lbTm_coddest2.Text = "Destin. diversa 2"
    Me.lbTm_coddest2.Tooltip = ""
    Me.lbTm_coddest2.UseMnemonic = False
    '
    'edTm_codaspe
    '
    Me.edTm_codaspe.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codaspe.Location = New System.Drawing.Point(96, 41)
    Me.edTm_codaspe.Name = "edTm_codaspe"
    Me.edTm_codaspe.NTSDbField = ""
    Me.edTm_codaspe.NTSFormat = "0"
    Me.edTm_codaspe.NTSForzaVisZoom = False
    Me.edTm_codaspe.NTSOldValue = ""
    Me.edTm_codaspe.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codaspe.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codaspe.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codaspe.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codaspe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codaspe.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codaspe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codaspe.Properties.MaxLength = 65536
    Me.edTm_codaspe.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codaspe.Size = New System.Drawing.Size(52, 20)
    Me.edTm_codaspe.TabIndex = 54
    '
    'lbTm_codaspe
    '
    Me.lbTm_codaspe.AutoSize = True
    Me.lbTm_codaspe.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codaspe.Location = New System.Drawing.Point(5, 44)
    Me.lbTm_codaspe.Name = "lbTm_codaspe"
    Me.lbTm_codaspe.NTSDbField = ""
    Me.lbTm_codaspe.Size = New System.Drawing.Size(85, 13)
    Me.lbTm_codaspe.TabIndex = 53
    Me.lbTm_codaspe.Text = "Aspetto dei beni"
    Me.lbTm_codaspe.Tooltip = ""
    Me.lbTm_codaspe.UseMnemonic = False
    '
    'lbXx_caustra
    '
    Me.lbXx_caustra.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_caustra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_caustra.Location = New System.Drawing.Point(149, 19)
    Me.lbXx_caustra.Name = "lbXx_caustra"
    Me.lbXx_caustra.NTSDbField = ""
    Me.lbXx_caustra.Size = New System.Drawing.Size(253, 20)
    Me.lbXx_caustra.TabIndex = 52
    Me.lbXx_caustra.Tooltip = ""
    Me.lbXx_caustra.UseMnemonic = False
    '
    'edTm_caustra
    '
    Me.edTm_caustra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_caustra.Location = New System.Drawing.Point(96, 19)
    Me.edTm_caustra.Name = "edTm_caustra"
    Me.edTm_caustra.NTSDbField = ""
    Me.edTm_caustra.NTSFormat = "0"
    Me.edTm_caustra.NTSForzaVisZoom = False
    Me.edTm_caustra.NTSOldValue = ""
    Me.edTm_caustra.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_caustra.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_caustra.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_caustra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_caustra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_caustra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_caustra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_caustra.Properties.MaxLength = 65536
    Me.edTm_caustra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_caustra.Size = New System.Drawing.Size(52, 20)
    Me.edTm_caustra.TabIndex = 51
    '
    'lbTm_caustra
    '
    Me.lbTm_caustra.AutoSize = True
    Me.lbTm_caustra.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_caustra.Location = New System.Drawing.Point(5, 22)
    Me.lbTm_caustra.Name = "lbTm_caustra"
    Me.lbTm_caustra.NTSDbField = ""
    Me.lbTm_caustra.Size = New System.Drawing.Size(45, 13)
    Me.lbTm_caustra.TabIndex = 50
    Me.lbTm_caustra.Text = "Causale"
    Me.lbTm_caustra.Tooltip = ""
    Me.lbTm_caustra.UseMnemonic = False
    '
    'fmSpese
    '
    Me.fmSpese.AllowDrop = True
    Me.fmSpese.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSpese.Appearance.Options.UseBackColor = True
    Me.fmSpese.Controls.Add(Me.lbResto)
    Me.fmSpese.Controls.Add(Me.edTm_resto)
    Me.fmSpese.Controls.Add(Me.edTm_pagato2)
    Me.fmSpese.Controls.Add(Me.lbIncassato2)
    Me.fmSpese.Controls.Add(Me.ckTm_flscdb)
    Me.fmSpese.Controls.Add(Me.lbRiga2)
    Me.fmSpese.Controls.Add(Me.lbRiga1)
    Me.fmSpese.Controls.Add(Me.edTotalev)
    Me.fmSpese.Controls.Add(Me.edTotale)
    Me.fmSpese.Controls.Add(Me.lbTotale)
    Me.fmSpese.Controls.Add(Me.edResiduo)
    Me.fmSpese.Controls.Add(Me.lbRimanenza)
    Me.fmSpese.Controls.Add(Me.edResiduov)
    Me.fmSpese.Controls.Add(Me.edTm_abbuono)
    Me.fmSpese.Controls.Add(Me.lbAbbuono)
    Me.fmSpese.Controls.Add(Me.edTm_pagato)
    Me.fmSpese.Controls.Add(Me.lbIncassato)
    Me.fmSpese.Controls.Add(Me.edTm_abbuonov)
    Me.fmSpese.Controls.Add(Me.edTm_totomag)
    Me.fmSpese.Controls.Add(Me.lbOmaggi)
    Me.fmSpese.Controls.Add(Me.lbValuta)
    Me.fmSpese.Controls.Add(Me.edTm_speaccv)
    Me.fmSpese.Controls.Add(Me.edTm_totdoc)
    Me.fmSpese.Controls.Add(Me.edTm_pagatov)
    Me.fmSpese.Controls.Add(Me.lbDocumento)
    Me.fmSpese.Controls.Add(Me.edTm_speimbv)
    Me.fmSpese.Controls.Add(Me.edTotIva)
    Me.fmSpese.Controls.Add(Me.edTm_speincv)
    Me.fmSpese.Controls.Add(Me.lbImposta)
    Me.fmSpese.Controls.Add(Me.edTm_bolliv)
    Me.fmSpese.Controls.Add(Me.edTotImponib)
    Me.fmSpese.Controls.Add(Me.edTm_totomagv)
    Me.fmSpese.Controls.Add(Me.lbImponibile)
    Me.fmSpese.Controls.Add(Me.edTm_totmercev)
    Me.fmSpese.Controls.Add(Me.edTotSpese)
    Me.fmSpese.Controls.Add(Me.edTotSpesev)
    Me.fmSpese.Controls.Add(Me.lbSpese)
    Me.fmSpese.Controls.Add(Me.edTotImponibv)
    Me.fmSpese.Controls.Add(Me.edTm_totmerce)
    Me.fmSpese.Controls.Add(Me.edTm_totdocv)
    Me.fmSpese.Controls.Add(Me.lbNetto)
    Me.fmSpese.Controls.Add(Me.edTotIvav)
    Me.fmSpese.Controls.Add(Me.edTm_bolli)
    Me.fmSpese.Controls.Add(Me.lbBolli)
    Me.fmSpese.Controls.Add(Me.edTm_speinc)
    Me.fmSpese.Controls.Add(Me.lbIncasso)
    Me.fmSpese.Controls.Add(Me.edTm_speimb)
    Me.fmSpese.Controls.Add(Me.lbImballo)
    Me.fmSpese.Controls.Add(Me.lbEur)
    Me.fmSpese.Controls.Add(Me.edTm_speacc)
    Me.fmSpese.Controls.Add(Me.lbTrasporto)
    Me.fmSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSpese.Location = New System.Drawing.Point(480, 0)
    Me.fmSpese.Name = "fmSpese"
    Me.fmSpese.Size = New System.Drawing.Size(251, 372)
    Me.fmSpese.TabIndex = 0
    Me.fmSpese.Text = "Spese / totali"
    '
    'lbResto
    '
    Me.lbResto.AutoSize = True
    Me.lbResto.BackColor = System.Drawing.Color.Transparent
    Me.lbResto.Location = New System.Drawing.Point(5, 312)
    Me.lbResto.Name = "lbResto"
    Me.lbResto.NTSDbField = ""
    Me.lbResto.Size = New System.Drawing.Size(35, 13)
    Me.lbResto.TabIndex = 106
    Me.lbResto.Text = "Resto"
    Me.lbResto.Tooltip = ""
    Me.lbResto.UseMnemonic = False
    '
    'edTm_resto
    '
    Me.edTm_resto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_resto.Enabled = False
    Me.edTm_resto.Location = New System.Drawing.Point(159, 309)
    Me.edTm_resto.Name = "edTm_resto"
    Me.edTm_resto.NTSDbField = ""
    Me.edTm_resto.NTSFormat = "0"
    Me.edTm_resto.NTSForzaVisZoom = False
    Me.edTm_resto.NTSOldValue = ""
    Me.edTm_resto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_resto.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_resto.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_resto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_resto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_resto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_resto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_resto.Properties.MaxLength = 65536
    Me.edTm_resto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_resto.Size = New System.Drawing.Size(77, 20)
    Me.edTm_resto.TabIndex = 105
    '
    'edTm_pagato2
    '
    Me.edTm_pagato2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_pagato2.Enabled = False
    Me.edTm_pagato2.Location = New System.Drawing.Point(159, 289)
    Me.edTm_pagato2.Name = "edTm_pagato2"
    Me.edTm_pagato2.NTSDbField = ""
    Me.edTm_pagato2.NTSFormat = "0"
    Me.edTm_pagato2.NTSForzaVisZoom = False
    Me.edTm_pagato2.NTSOldValue = ""
    Me.edTm_pagato2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_pagato2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_pagato2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_pagato2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_pagato2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_pagato2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_pagato2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_pagato2.Properties.MaxLength = 65536
    Me.edTm_pagato2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_pagato2.Size = New System.Drawing.Size(77, 20)
    Me.edTm_pagato2.TabIndex = 104
    '
    'lbIncassato2
    '
    Me.lbIncassato2.AutoSize = True
    Me.lbIncassato2.BackColor = System.Drawing.Color.Transparent
    Me.lbIncassato2.Location = New System.Drawing.Point(5, 292)
    Me.lbIncassato2.Name = "lbIncassato2"
    Me.lbIncassato2.NTSDbField = ""
    Me.lbIncassato2.Size = New System.Drawing.Size(68, 13)
    Me.lbIncassato2.TabIndex = 103
    Me.lbIncassato2.Text = "Inc. contanti"
    Me.lbIncassato2.Tooltip = ""
    Me.lbIncassato2.UseMnemonic = False
    '
    'ckTm_flscdb
    '
    Me.ckTm_flscdb.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_flscdb.Enabled = False
    Me.ckTm_flscdb.Location = New System.Drawing.Point(123, 0)
    Me.ckTm_flscdb.Name = "ckTm_flscdb"
    Me.ckTm_flscdb.NTSCheckValue = "S"
    Me.ckTm_flscdb.NTSUnCheckValue = "N"
    Me.ckTm_flscdb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flscdb.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flscdb.Properties.Caption = "Sblocca castelletti"
    Me.ckTm_flscdb.Size = New System.Drawing.Size(114, 19)
    Me.ckTm_flscdb.TabIndex = 102
    '
    'lbRiga2
    '
    Me.lbRiga2.BackColor = System.Drawing.Color.Transparent
    Me.lbRiga2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbRiga2.Location = New System.Drawing.Point(77, 244)
    Me.lbRiga2.Name = "lbRiga2"
    Me.lbRiga2.NTSDbField = ""
    Me.lbRiga2.Size = New System.Drawing.Size(160, 1)
    Me.lbRiga2.TabIndex = 101
    Me.lbRiga2.Tooltip = ""
    Me.lbRiga2.UseMnemonic = False
    '
    'lbRiga1
    '
    Me.lbRiga1.BackColor = System.Drawing.Color.Transparent
    Me.lbRiga1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbRiga1.Location = New System.Drawing.Point(77, 115)
    Me.lbRiga1.Name = "lbRiga1"
    Me.lbRiga1.NTSDbField = ""
    Me.lbRiga1.Size = New System.Drawing.Size(160, 1)
    Me.lbRiga1.TabIndex = 100
    Me.lbRiga1.Tooltip = ""
    Me.lbRiga1.UseMnemonic = False
    '
    'edTotalev
    '
    Me.edTotalev.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotalev.Enabled = False
    Me.edTotalev.Location = New System.Drawing.Point(77, 249)
    Me.edTotalev.Name = "edTotalev"
    Me.edTotalev.NTSDbField = ""
    Me.edTotalev.NTSFormat = "0"
    Me.edTotalev.NTSForzaVisZoom = False
    Me.edTotalev.NTSOldValue = ""
    Me.edTotalev.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotalev.Properties.Appearance.Options.UseBackColor = True
    Me.edTotalev.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotalev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotalev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotalev.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotalev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotalev.Properties.MaxLength = 65536
    Me.edTotalev.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotalev.Size = New System.Drawing.Size(77, 20)
    Me.edTotalev.TabIndex = 97
    '
    'edTotale
    '
    Me.edTotale.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotale.Enabled = False
    Me.edTotale.Location = New System.Drawing.Point(159, 249)
    Me.edTotale.Name = "edTotale"
    Me.edTotale.NTSDbField = ""
    Me.edTotale.NTSFormat = "0"
    Me.edTotale.NTSForzaVisZoom = False
    Me.edTotale.NTSOldValue = ""
    Me.edTotale.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotale.Properties.Appearance.Options.UseBackColor = True
    Me.edTotale.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotale.Properties.MaxLength = 65536
    Me.edTotale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotale.Size = New System.Drawing.Size(77, 20)
    Me.edTotale.TabIndex = 96
    '
    'lbTotale
    '
    Me.lbTotale.AutoSize = True
    Me.lbTotale.BackColor = System.Drawing.Color.Transparent
    Me.lbTotale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTotale.Location = New System.Drawing.Point(5, 252)
    Me.lbTotale.Name = "lbTotale"
    Me.lbTotale.NTSDbField = ""
    Me.lbTotale.Size = New System.Drawing.Size(43, 13)
    Me.lbTotale.TabIndex = 95
    Me.lbTotale.Text = "Totale"
    Me.lbTotale.Tooltip = ""
    Me.lbTotale.UseMnemonic = False
    '
    'edResiduo
    '
    Me.edResiduo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edResiduo.Enabled = False
    Me.edResiduo.Location = New System.Drawing.Point(159, 349)
    Me.edResiduo.Name = "edResiduo"
    Me.edResiduo.NTSDbField = ""
    Me.edResiduo.NTSFormat = "0"
    Me.edResiduo.NTSForzaVisZoom = False
    Me.edResiduo.NTSOldValue = ""
    Me.edResiduo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edResiduo.Properties.Appearance.Options.UseBackColor = True
    Me.edResiduo.Properties.Appearance.Options.UseTextOptions = True
    Me.edResiduo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edResiduo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edResiduo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edResiduo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edResiduo.Properties.MaxLength = 65536
    Me.edResiduo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edResiduo.Size = New System.Drawing.Size(77, 20)
    Me.edResiduo.TabIndex = 93
    '
    'lbRimanenza
    '
    Me.lbRimanenza.AutoSize = True
    Me.lbRimanenza.BackColor = System.Drawing.Color.Transparent
    Me.lbRimanenza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbRimanenza.Location = New System.Drawing.Point(5, 351)
    Me.lbRimanenza.Name = "lbRimanenza"
    Me.lbRimanenza.NTSDbField = ""
    Me.lbRimanenza.Size = New System.Drawing.Size(69, 13)
    Me.lbRimanenza.TabIndex = 92
    Me.lbRimanenza.Text = "Rimanenza"
    Me.lbRimanenza.Tooltip = ""
    Me.lbRimanenza.UseMnemonic = False
    '
    'edResiduov
    '
    Me.edResiduov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edResiduov.Enabled = False
    Me.edResiduov.Location = New System.Drawing.Point(77, 349)
    Me.edResiduov.Name = "edResiduov"
    Me.edResiduov.NTSDbField = ""
    Me.edResiduov.NTSFormat = "0"
    Me.edResiduov.NTSForzaVisZoom = False
    Me.edResiduov.NTSOldValue = ""
    Me.edResiduov.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edResiduov.Properties.Appearance.Options.UseBackColor = True
    Me.edResiduov.Properties.Appearance.Options.UseTextOptions = True
    Me.edResiduov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edResiduov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edResiduov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edResiduov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edResiduov.Properties.MaxLength = 65536
    Me.edResiduov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edResiduov.Size = New System.Drawing.Size(77, 20)
    Me.edResiduov.TabIndex = 94
    '
    'edTm_abbuono
    '
    Me.edTm_abbuono.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_abbuono.Enabled = False
    Me.edTm_abbuono.Location = New System.Drawing.Point(159, 329)
    Me.edTm_abbuono.Name = "edTm_abbuono"
    Me.edTm_abbuono.NTSDbField = ""
    Me.edTm_abbuono.NTSFormat = "0"
    Me.edTm_abbuono.NTSForzaVisZoom = False
    Me.edTm_abbuono.NTSOldValue = ""
    Me.edTm_abbuono.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_abbuono.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_abbuono.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_abbuono.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_abbuono.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_abbuono.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_abbuono.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_abbuono.Properties.MaxLength = 65536
    Me.edTm_abbuono.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_abbuono.Size = New System.Drawing.Size(77, 20)
    Me.edTm_abbuono.TabIndex = 90
    '
    'lbAbbuono
    '
    Me.lbAbbuono.AutoSize = True
    Me.lbAbbuono.BackColor = System.Drawing.Color.Transparent
    Me.lbAbbuono.Location = New System.Drawing.Point(5, 332)
    Me.lbAbbuono.Name = "lbAbbuono"
    Me.lbAbbuono.NTSDbField = ""
    Me.lbAbbuono.Size = New System.Drawing.Size(50, 13)
    Me.lbAbbuono.TabIndex = 89
    Me.lbAbbuono.Text = "Abbuono"
    Me.lbAbbuono.Tooltip = ""
    Me.lbAbbuono.UseMnemonic = False
    '
    'edTm_pagato
    '
    Me.edTm_pagato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_pagato.Enabled = False
    Me.edTm_pagato.Location = New System.Drawing.Point(159, 269)
    Me.edTm_pagato.Name = "edTm_pagato"
    Me.edTm_pagato.NTSDbField = ""
    Me.edTm_pagato.NTSFormat = "0"
    Me.edTm_pagato.NTSForzaVisZoom = False
    Me.edTm_pagato.NTSOldValue = ""
    Me.edTm_pagato.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_pagato.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_pagato.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_pagato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_pagato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_pagato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_pagato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_pagato.Properties.MaxLength = 65536
    Me.edTm_pagato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_pagato.Size = New System.Drawing.Size(77, 20)
    Me.edTm_pagato.TabIndex = 87
    '
    'lbIncassato
    '
    Me.lbIncassato.AutoSize = True
    Me.lbIncassato.BackColor = System.Drawing.Color.Transparent
    Me.lbIncassato.Location = New System.Drawing.Point(5, 272)
    Me.lbIncassato.Name = "lbIncassato"
    Me.lbIncassato.NTSDbField = ""
    Me.lbIncassato.Size = New System.Drawing.Size(54, 13)
    Me.lbIncassato.TabIndex = 86
    Me.lbIncassato.Text = "Incassato"
    Me.lbIncassato.Tooltip = ""
    Me.lbIncassato.UseMnemonic = False
    '
    'edTm_abbuonov
    '
    Me.edTm_abbuonov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_abbuonov.Enabled = False
    Me.edTm_abbuonov.Location = New System.Drawing.Point(77, 329)
    Me.edTm_abbuonov.Name = "edTm_abbuonov"
    Me.edTm_abbuonov.NTSDbField = ""
    Me.edTm_abbuonov.NTSFormat = "0"
    Me.edTm_abbuonov.NTSForzaVisZoom = False
    Me.edTm_abbuonov.NTSOldValue = ""
    Me.edTm_abbuonov.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_abbuonov.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_abbuonov.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_abbuonov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_abbuonov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_abbuonov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_abbuonov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_abbuonov.Properties.MaxLength = 65536
    Me.edTm_abbuonov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_abbuonov.Size = New System.Drawing.Size(77, 20)
    Me.edTm_abbuonov.TabIndex = 91
    '
    'edTm_totomag
    '
    Me.edTm_totomag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totomag.Enabled = False
    Me.edTm_totomag.Location = New System.Drawing.Point(159, 220)
    Me.edTm_totomag.Name = "edTm_totomag"
    Me.edTm_totomag.NTSDbField = ""
    Me.edTm_totomag.NTSFormat = "0"
    Me.edTm_totomag.NTSForzaVisZoom = False
    Me.edTm_totomag.NTSOldValue = ""
    Me.edTm_totomag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totomag.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totomag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totomag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totomag.Properties.MaxLength = 65536
    Me.edTm_totomag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totomag.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totomag.TabIndex = 84
    '
    'lbOmaggi
    '
    Me.lbOmaggi.AutoSize = True
    Me.lbOmaggi.BackColor = System.Drawing.Color.Transparent
    Me.lbOmaggi.Location = New System.Drawing.Point(5, 223)
    Me.lbOmaggi.Name = "lbOmaggi"
    Me.lbOmaggi.NTSDbField = ""
    Me.lbOmaggi.Size = New System.Drawing.Size(43, 13)
    Me.lbOmaggi.TabIndex = 83
    Me.lbOmaggi.Text = "Omaggi"
    Me.lbOmaggi.Tooltip = ""
    Me.lbOmaggi.UseMnemonic = False
    '
    'lbValuta
    '
    Me.lbValuta.AutoSize = True
    Me.lbValuta.BackColor = System.Drawing.Color.Transparent
    Me.lbValuta.Location = New System.Drawing.Point(101, 19)
    Me.lbValuta.Name = "lbValuta"
    Me.lbValuta.NTSDbField = ""
    Me.lbValuta.Size = New System.Drawing.Size(37, 13)
    Me.lbValuta.TabIndex = 57
    Me.lbValuta.Text = "Valuta"
    Me.lbValuta.Tooltip = ""
    Me.lbValuta.UseMnemonic = False
    '
    'edTm_speaccv
    '
    Me.edTm_speaccv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_speaccv.Enabled = False
    Me.edTm_speaccv.Location = New System.Drawing.Point(77, 32)
    Me.edTm_speaccv.Name = "edTm_speaccv"
    Me.edTm_speaccv.NTSDbField = ""
    Me.edTm_speaccv.NTSFormat = "0"
    Me.edTm_speaccv.NTSForzaVisZoom = False
    Me.edTm_speaccv.NTSOldValue = ""
    Me.edTm_speaccv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speaccv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speaccv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speaccv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speaccv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speaccv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speaccv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speaccv.Properties.MaxLength = 65536
    Me.edTm_speaccv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speaccv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speaccv.TabIndex = 58
    '
    'edTm_totdoc
    '
    Me.edTm_totdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totdoc.Enabled = False
    Me.edTm_totdoc.Location = New System.Drawing.Point(159, 200)
    Me.edTm_totdoc.Name = "edTm_totdoc"
    Me.edTm_totdoc.NTSDbField = ""
    Me.edTm_totdoc.NTSFormat = "0"
    Me.edTm_totdoc.NTSForzaVisZoom = False
    Me.edTm_totdoc.NTSOldValue = ""
    Me.edTm_totdoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totdoc.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totdoc.Properties.MaxLength = 65536
    Me.edTm_totdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totdoc.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totdoc.TabIndex = 81
    '
    'edTm_pagatov
    '
    Me.edTm_pagatov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_pagatov.Enabled = False
    Me.edTm_pagatov.Location = New System.Drawing.Point(77, 269)
    Me.edTm_pagatov.Name = "edTm_pagatov"
    Me.edTm_pagatov.NTSDbField = ""
    Me.edTm_pagatov.NTSFormat = "0"
    Me.edTm_pagatov.NTSForzaVisZoom = False
    Me.edTm_pagatov.NTSOldValue = ""
    Me.edTm_pagatov.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_pagatov.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_pagatov.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_pagatov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_pagatov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_pagatov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_pagatov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_pagatov.Properties.MaxLength = 65536
    Me.edTm_pagatov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_pagatov.Size = New System.Drawing.Size(77, 20)
    Me.edTm_pagatov.TabIndex = 88
    '
    'lbDocumento
    '
    Me.lbDocumento.AutoSize = True
    Me.lbDocumento.BackColor = System.Drawing.Color.Transparent
    Me.lbDocumento.Location = New System.Drawing.Point(5, 203)
    Me.lbDocumento.Name = "lbDocumento"
    Me.lbDocumento.NTSDbField = ""
    Me.lbDocumento.Size = New System.Drawing.Size(61, 13)
    Me.lbDocumento.TabIndex = 80
    Me.lbDocumento.Text = "Documento"
    Me.lbDocumento.Tooltip = ""
    Me.lbDocumento.UseMnemonic = False
    '
    'edTm_speimbv
    '
    Me.edTm_speimbv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_speimbv.Enabled = False
    Me.edTm_speimbv.Location = New System.Drawing.Point(77, 52)
    Me.edTm_speimbv.Name = "edTm_speimbv"
    Me.edTm_speimbv.NTSDbField = ""
    Me.edTm_speimbv.NTSFormat = "0"
    Me.edTm_speimbv.NTSForzaVisZoom = False
    Me.edTm_speimbv.NTSOldValue = ""
    Me.edTm_speimbv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speimbv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speimbv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speimbv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speimbv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speimbv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speimbv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speimbv.Properties.MaxLength = 65536
    Me.edTm_speimbv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speimbv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speimbv.TabIndex = 61
    '
    'edTotIva
    '
    Me.edTotIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotIva.Enabled = False
    Me.edTotIva.Location = New System.Drawing.Point(159, 180)
    Me.edTotIva.Name = "edTotIva"
    Me.edTotIva.NTSDbField = ""
    Me.edTotIva.NTSFormat = "0"
    Me.edTotIva.NTSForzaVisZoom = False
    Me.edTotIva.NTSOldValue = ""
    Me.edTotIva.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotIva.Properties.Appearance.Options.UseBackColor = True
    Me.edTotIva.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotIva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotIva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotIva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotIva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotIva.Properties.MaxLength = 65536
    Me.edTotIva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotIva.Size = New System.Drawing.Size(77, 20)
    Me.edTotIva.TabIndex = 78
    '
    'edTm_speincv
    '
    Me.edTm_speincv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_speincv.Enabled = False
    Me.edTm_speincv.Location = New System.Drawing.Point(77, 72)
    Me.edTm_speincv.Name = "edTm_speincv"
    Me.edTm_speincv.NTSDbField = ""
    Me.edTm_speincv.NTSFormat = "0"
    Me.edTm_speincv.NTSForzaVisZoom = False
    Me.edTm_speincv.NTSOldValue = ""
    Me.edTm_speincv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speincv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speincv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speincv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speincv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speincv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speincv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speincv.Properties.MaxLength = 65536
    Me.edTm_speincv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speincv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speincv.TabIndex = 64
    '
    'lbImposta
    '
    Me.lbImposta.AutoSize = True
    Me.lbImposta.BackColor = System.Drawing.Color.Transparent
    Me.lbImposta.Location = New System.Drawing.Point(5, 183)
    Me.lbImposta.Name = "lbImposta"
    Me.lbImposta.NTSDbField = ""
    Me.lbImposta.Size = New System.Drawing.Size(46, 13)
    Me.lbImposta.TabIndex = 77
    Me.lbImposta.Text = "Imposta"
    Me.lbImposta.Tooltip = ""
    Me.lbImposta.UseMnemonic = False
    '
    'edTm_bolliv
    '
    Me.edTm_bolliv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_bolliv.Enabled = False
    Me.edTm_bolliv.Location = New System.Drawing.Point(77, 92)
    Me.edTm_bolliv.Name = "edTm_bolliv"
    Me.edTm_bolliv.NTSDbField = ""
    Me.edTm_bolliv.NTSFormat = "0"
    Me.edTm_bolliv.NTSForzaVisZoom = False
    Me.edTm_bolliv.NTSOldValue = ""
    Me.edTm_bolliv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_bolliv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_bolliv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_bolliv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_bolliv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_bolliv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_bolliv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_bolliv.Properties.MaxLength = 65536
    Me.edTm_bolliv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_bolliv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_bolliv.TabIndex = 67
    '
    'edTotImponib
    '
    Me.edTotImponib.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotImponib.Enabled = False
    Me.edTotImponib.Location = New System.Drawing.Point(159, 160)
    Me.edTotImponib.Name = "edTotImponib"
    Me.edTotImponib.NTSDbField = ""
    Me.edTotImponib.NTSFormat = "0"
    Me.edTotImponib.NTSForzaVisZoom = False
    Me.edTotImponib.NTSOldValue = ""
    Me.edTotImponib.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotImponib.Properties.Appearance.Options.UseBackColor = True
    Me.edTotImponib.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotImponib.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotImponib.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotImponib.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotImponib.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotImponib.Properties.MaxLength = 65536
    Me.edTotImponib.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotImponib.Size = New System.Drawing.Size(77, 20)
    Me.edTotImponib.TabIndex = 75
    '
    'edTm_totomagv
    '
    Me.edTm_totomagv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totomagv.Enabled = False
    Me.edTm_totomagv.Location = New System.Drawing.Point(77, 220)
    Me.edTm_totomagv.Name = "edTm_totomagv"
    Me.edTm_totomagv.NTSDbField = ""
    Me.edTm_totomagv.NTSFormat = "0"
    Me.edTm_totomagv.NTSForzaVisZoom = False
    Me.edTm_totomagv.NTSOldValue = ""
    Me.edTm_totomagv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totomagv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totomagv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totomagv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totomagv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totomagv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totomagv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totomagv.Properties.MaxLength = 65536
    Me.edTm_totomagv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totomagv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totomagv.TabIndex = 85
    '
    'lbImponibile
    '
    Me.lbImponibile.AutoSize = True
    Me.lbImponibile.BackColor = System.Drawing.Color.Transparent
    Me.lbImponibile.Location = New System.Drawing.Point(5, 163)
    Me.lbImponibile.Name = "lbImponibile"
    Me.lbImponibile.NTSDbField = ""
    Me.lbImponibile.Size = New System.Drawing.Size(55, 13)
    Me.lbImponibile.TabIndex = 74
    Me.lbImponibile.Text = "Imponibile"
    Me.lbImponibile.Tooltip = ""
    Me.lbImponibile.UseMnemonic = False
    '
    'edTm_totmercev
    '
    Me.edTm_totmercev.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totmercev.Enabled = False
    Me.edTm_totmercev.Location = New System.Drawing.Point(77, 120)
    Me.edTm_totmercev.Name = "edTm_totmercev"
    Me.edTm_totmercev.NTSDbField = ""
    Me.edTm_totmercev.NTSFormat = "0"
    Me.edTm_totmercev.NTSForzaVisZoom = False
    Me.edTm_totmercev.NTSOldValue = ""
    Me.edTm_totmercev.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totmercev.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totmercev.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totmercev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totmercev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totmercev.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totmercev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totmercev.Properties.MaxLength = 65536
    Me.edTm_totmercev.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totmercev.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totmercev.TabIndex = 70
    '
    'edTotSpese
    '
    Me.edTotSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotSpese.Enabled = False
    Me.edTotSpese.Location = New System.Drawing.Point(159, 140)
    Me.edTotSpese.Name = "edTotSpese"
    Me.edTotSpese.NTSDbField = ""
    Me.edTotSpese.NTSFormat = "0"
    Me.edTotSpese.NTSForzaVisZoom = False
    Me.edTotSpese.NTSOldValue = ""
    Me.edTotSpese.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotSpese.Properties.Appearance.Options.UseBackColor = True
    Me.edTotSpese.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotSpese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotSpese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotSpese.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotSpese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotSpese.Properties.MaxLength = 65536
    Me.edTotSpese.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotSpese.Size = New System.Drawing.Size(77, 20)
    Me.edTotSpese.TabIndex = 72
    '
    'edTotSpesev
    '
    Me.edTotSpesev.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotSpesev.Enabled = False
    Me.edTotSpesev.Location = New System.Drawing.Point(77, 140)
    Me.edTotSpesev.Name = "edTotSpesev"
    Me.edTotSpesev.NTSDbField = ""
    Me.edTotSpesev.NTSFormat = "0"
    Me.edTotSpesev.NTSForzaVisZoom = False
    Me.edTotSpesev.NTSOldValue = ""
    Me.edTotSpesev.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotSpesev.Properties.Appearance.Options.UseBackColor = True
    Me.edTotSpesev.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotSpesev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotSpesev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotSpesev.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotSpesev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotSpesev.Properties.MaxLength = 65536
    Me.edTotSpesev.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotSpesev.Size = New System.Drawing.Size(77, 20)
    Me.edTotSpesev.TabIndex = 73
    '
    'lbSpese
    '
    Me.lbSpese.AutoSize = True
    Me.lbSpese.BackColor = System.Drawing.Color.Transparent
    Me.lbSpese.Location = New System.Drawing.Point(5, 143)
    Me.lbSpese.Name = "lbSpese"
    Me.lbSpese.NTSDbField = ""
    Me.lbSpese.Size = New System.Drawing.Size(36, 13)
    Me.lbSpese.TabIndex = 71
    Me.lbSpese.Text = "Spese"
    Me.lbSpese.Tooltip = ""
    Me.lbSpese.UseMnemonic = False
    '
    'edTotImponibv
    '
    Me.edTotImponibv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotImponibv.Enabled = False
    Me.edTotImponibv.Location = New System.Drawing.Point(77, 160)
    Me.edTotImponibv.Name = "edTotImponibv"
    Me.edTotImponibv.NTSDbField = ""
    Me.edTotImponibv.NTSFormat = "0"
    Me.edTotImponibv.NTSForzaVisZoom = False
    Me.edTotImponibv.NTSOldValue = ""
    Me.edTotImponibv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotImponibv.Properties.Appearance.Options.UseBackColor = True
    Me.edTotImponibv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotImponibv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotImponibv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotImponibv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotImponibv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotImponibv.Properties.MaxLength = 65536
    Me.edTotImponibv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotImponibv.Size = New System.Drawing.Size(77, 20)
    Me.edTotImponibv.TabIndex = 76
    '
    'edTm_totmerce
    '
    Me.edTm_totmerce.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totmerce.Enabled = False
    Me.edTm_totmerce.Location = New System.Drawing.Point(159, 120)
    Me.edTm_totmerce.Name = "edTm_totmerce"
    Me.edTm_totmerce.NTSDbField = ""
    Me.edTm_totmerce.NTSFormat = "0"
    Me.edTm_totmerce.NTSForzaVisZoom = False
    Me.edTm_totmerce.NTSOldValue = ""
    Me.edTm_totmerce.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totmerce.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totmerce.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totmerce.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totmerce.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totmerce.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totmerce.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totmerce.Properties.MaxLength = 65536
    Me.edTm_totmerce.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totmerce.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totmerce.TabIndex = 69
    '
    'edTm_totdocv
    '
    Me.edTm_totdocv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_totdocv.Enabled = False
    Me.edTm_totdocv.Location = New System.Drawing.Point(77, 200)
    Me.edTm_totdocv.Name = "edTm_totdocv"
    Me.edTm_totdocv.NTSDbField = ""
    Me.edTm_totdocv.NTSFormat = "0"
    Me.edTm_totdocv.NTSForzaVisZoom = False
    Me.edTm_totdocv.NTSOldValue = ""
    Me.edTm_totdocv.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_totdocv.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_totdocv.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_totdocv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_totdocv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_totdocv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_totdocv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_totdocv.Properties.MaxLength = 65536
    Me.edTm_totdocv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_totdocv.Size = New System.Drawing.Size(77, 20)
    Me.edTm_totdocv.TabIndex = 82
    '
    'lbNetto
    '
    Me.lbNetto.AutoSize = True
    Me.lbNetto.BackColor = System.Drawing.Color.Transparent
    Me.lbNetto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNetto.Location = New System.Drawing.Point(5, 123)
    Me.lbNetto.Name = "lbNetto"
    Me.lbNetto.NTSDbField = ""
    Me.lbNetto.Size = New System.Drawing.Size(38, 13)
    Me.lbNetto.TabIndex = 68
    Me.lbNetto.Text = "Netto"
    Me.lbNetto.Tooltip = ""
    Me.lbNetto.UseMnemonic = False
    '
    'edTotIvav
    '
    Me.edTotIvav.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotIvav.Enabled = False
    Me.edTotIvav.Location = New System.Drawing.Point(77, 180)
    Me.edTotIvav.Name = "edTotIvav"
    Me.edTotIvav.NTSDbField = ""
    Me.edTotIvav.NTSFormat = "0"
    Me.edTotIvav.NTSForzaVisZoom = False
    Me.edTotIvav.NTSOldValue = ""
    Me.edTotIvav.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTotIvav.Properties.Appearance.Options.UseBackColor = True
    Me.edTotIvav.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotIvav.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotIvav.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotIvav.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotIvav.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotIvav.Properties.MaxLength = 65536
    Me.edTotIvav.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotIvav.Size = New System.Drawing.Size(77, 20)
    Me.edTotIvav.TabIndex = 79
    '
    'edTm_bolli
    '
    Me.edTm_bolli.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_bolli.Enabled = False
    Me.edTm_bolli.Location = New System.Drawing.Point(159, 92)
    Me.edTm_bolli.Name = "edTm_bolli"
    Me.edTm_bolli.NTSDbField = ""
    Me.edTm_bolli.NTSFormat = "0"
    Me.edTm_bolli.NTSForzaVisZoom = False
    Me.edTm_bolli.NTSOldValue = ""
    Me.edTm_bolli.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_bolli.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_bolli.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_bolli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_bolli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_bolli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_bolli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_bolli.Properties.MaxLength = 65536
    Me.edTm_bolli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_bolli.Size = New System.Drawing.Size(77, 20)
    Me.edTm_bolli.TabIndex = 66
    '
    'lbBolli
    '
    Me.lbBolli.AutoSize = True
    Me.lbBolli.BackColor = System.Drawing.Color.Transparent
    Me.lbBolli.Location = New System.Drawing.Point(5, 95)
    Me.lbBolli.Name = "lbBolli"
    Me.lbBolli.NTSDbField = ""
    Me.lbBolli.Size = New System.Drawing.Size(25, 13)
    Me.lbBolli.TabIndex = 65
    Me.lbBolli.Text = "Bolli"
    Me.lbBolli.Tooltip = ""
    Me.lbBolli.UseMnemonic = False
    '
    'edTm_speinc
    '
    Me.edTm_speinc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_speinc.Enabled = False
    Me.edTm_speinc.Location = New System.Drawing.Point(159, 72)
    Me.edTm_speinc.Name = "edTm_speinc"
    Me.edTm_speinc.NTSDbField = ""
    Me.edTm_speinc.NTSFormat = "0"
    Me.edTm_speinc.NTSForzaVisZoom = False
    Me.edTm_speinc.NTSOldValue = ""
    Me.edTm_speinc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speinc.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speinc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speinc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speinc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speinc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speinc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speinc.Properties.MaxLength = 65536
    Me.edTm_speinc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speinc.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speinc.TabIndex = 63
    '
    'lbIncasso
    '
    Me.lbIncasso.AutoSize = True
    Me.lbIncasso.BackColor = System.Drawing.Color.Transparent
    Me.lbIncasso.Location = New System.Drawing.Point(5, 75)
    Me.lbIncasso.Name = "lbIncasso"
    Me.lbIncasso.NTSDbField = ""
    Me.lbIncasso.Size = New System.Drawing.Size(44, 13)
    Me.lbIncasso.TabIndex = 62
    Me.lbIncasso.Text = "Incasso"
    Me.lbIncasso.Tooltip = ""
    Me.lbIncasso.UseMnemonic = False
    '
    'edTm_speimb
    '
    Me.edTm_speimb.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTm_speimb.Enabled = False
    Me.edTm_speimb.Location = New System.Drawing.Point(159, 52)
    Me.edTm_speimb.Name = "edTm_speimb"
    Me.edTm_speimb.NTSDbField = ""
    Me.edTm_speimb.NTSFormat = "0"
    Me.edTm_speimb.NTSForzaVisZoom = False
    Me.edTm_speimb.NTSOldValue = ""
    Me.edTm_speimb.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speimb.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speimb.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speimb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speimb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speimb.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speimb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speimb.Properties.MaxLength = 65536
    Me.edTm_speimb.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speimb.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speimb.TabIndex = 60
    '
    'lbImballo
    '
    Me.lbImballo.AutoSize = True
    Me.lbImballo.BackColor = System.Drawing.Color.Transparent
    Me.lbImballo.Location = New System.Drawing.Point(5, 55)
    Me.lbImballo.Name = "lbImballo"
    Me.lbImballo.NTSDbField = ""
    Me.lbImballo.Size = New System.Drawing.Size(41, 13)
    Me.lbImballo.TabIndex = 59
    Me.lbImballo.Text = "Imballo"
    Me.lbImballo.Tooltip = ""
    Me.lbImballo.UseMnemonic = False
    '
    'lbEur
    '
    Me.lbEur.AutoSize = True
    Me.lbEur.BackColor = System.Drawing.Color.Transparent
    Me.lbEur.Location = New System.Drawing.Point(185, 19)
    Me.lbEur.Name = "lbEur"
    Me.lbEur.NTSDbField = ""
    Me.lbEur.Size = New System.Drawing.Size(27, 13)
    Me.lbEur.TabIndex = 56
    Me.lbEur.Text = "EUR"
    Me.lbEur.Tooltip = ""
    Me.lbEur.UseMnemonic = False
    '
    'edTm_speacc
    '
    Me.edTm_speacc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_speacc.Enabled = False
    Me.edTm_speacc.Location = New System.Drawing.Point(159, 32)
    Me.edTm_speacc.Name = "edTm_speacc"
    Me.edTm_speacc.NTSDbField = ""
    Me.edTm_speacc.NTSFormat = "0"
    Me.edTm_speacc.NTSForzaVisZoom = False
    Me.edTm_speacc.NTSOldValue = ""
    Me.edTm_speacc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_speacc.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_speacc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_speacc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_speacc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_speacc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_speacc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_speacc.Properties.MaxLength = 65536
    Me.edTm_speacc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_speacc.Size = New System.Drawing.Size(77, 20)
    Me.edTm_speacc.TabIndex = 55
    '
    'lbTrasporto
    '
    Me.lbTrasporto.AutoSize = True
    Me.lbTrasporto.BackColor = System.Drawing.Color.Transparent
    Me.lbTrasporto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTrasporto.Location = New System.Drawing.Point(5, 35)
    Me.lbTrasporto.Name = "lbTrasporto"
    Me.lbTrasporto.NTSDbField = ""
    Me.lbTrasporto.Size = New System.Drawing.Size(52, 13)
    Me.lbTrasporto.TabIndex = 54
    Me.lbTrasporto.Text = "Trasporto"
    Me.lbTrasporto.Tooltip = ""
    Me.lbTrasporto.UseMnemonic = False
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Controls.Add(Me.pnTestataDx)
    Me.TabPage1.Controls.Add(Me.pnTestataSx)
    Me.TabPage1.Enable = True
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Size = New System.Drawing.Size(738, 373)
    Me.TabPage1.Text = "&1 - Testata"
    '
    'pnTestataDx
    '
    Me.pnTestataDx.AllowDrop = True
    Me.pnTestataDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestataDx.Appearance.Options.UseBackColor = True
    Me.pnTestataDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestataDx.Controls.Add(Me.lbXx_codpaga2)
    Me.pnTestataDx.Controls.Add(Me.edTm_codpaga2)
    Me.pnTestataDx.Controls.Add(Me.lbTm_codpaga2)
    Me.pnTestataDx.Controls.Add(Me.lbXx_vettor)
    Me.pnTestataDx.Controls.Add(Me.edTm_cambio)
    Me.pnTestataDx.Controls.Add(Me.edTm_vettor)
    Me.pnTestataDx.Controls.Add(Me.lbXx_valuta)
    Me.pnTestataDx.Controls.Add(Me.edTm_valuta)
    Me.pnTestataDx.Controls.Add(Me.lbTm_valuta)
    Me.pnTestataDx.Controls.Add(Me.lbXx_codpaga)
    Me.pnTestataDx.Controls.Add(Me.edTm_scont2)
    Me.pnTestataDx.Controls.Add(Me.edTm_scont1)
    Me.pnTestataDx.Controls.Add(Me.edTm_datapag)
    Me.pnTestataDx.Controls.Add(Me.edTm_scopag)
    Me.pnTestataDx.Controls.Add(Me.edTm_codpaga)
    Me.pnTestataDx.Controls.Add(Me.lbTm_vettor)
    Me.pnTestataDx.Controls.Add(Me.edTm_listino)
    Me.pnTestataDx.Controls.Add(Me.fmCa)
    Me.pnTestataDx.Controls.Add(Me.lbScontiTesta)
    Me.pnTestataDx.Controls.Add(Me.fmBanca)
    Me.pnTestataDx.Controls.Add(Me.lbTm_datapag)
    Me.pnTestataDx.Controls.Add(Me.lbTm_scopag)
    Me.pnTestataDx.Controls.Add(Me.lbTm_codpaga)
    Me.pnTestataDx.Controls.Add(Me.lbTm_listino)
    Me.pnTestataDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestataDx.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTestataDx.Location = New System.Drawing.Point(367, 0)
    Me.pnTestataDx.Name = "pnTestataDx"
    Me.pnTestataDx.NTSActiveTrasparency = True
    Me.pnTestataDx.Size = New System.Drawing.Size(371, 373)
    Me.pnTestataDx.TabIndex = 1
    '
    'lbXx_codpaga2
    '
    Me.lbXx_codpaga2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpaga2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpaga2.Location = New System.Drawing.Point(150, 81)
    Me.lbXx_codpaga2.Name = "lbXx_codpaga2"
    Me.lbXx_codpaga2.NTSDbField = ""
    Me.lbXx_codpaga2.Size = New System.Drawing.Size(210, 20)
    Me.lbXx_codpaga2.TabIndex = 70
    Me.lbXx_codpaga2.Tooltip = ""
    Me.lbXx_codpaga2.UseMnemonic = False
    '
    'edTm_codpaga2
    '
    Me.edTm_codpaga2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codpaga2.Enabled = False
    Me.edTm_codpaga2.Location = New System.Drawing.Point(94, 81)
    Me.edTm_codpaga2.Name = "edTm_codpaga2"
    Me.edTm_codpaga2.NTSDbField = ""
    Me.edTm_codpaga2.NTSFormat = "0"
    Me.edTm_codpaga2.NTSForzaVisZoom = False
    Me.edTm_codpaga2.NTSOldValue = ""
    Me.edTm_codpaga2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codpaga2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codpaga2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codpaga2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codpaga2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codpaga2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codpaga2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codpaga2.Properties.MaxLength = 65536
    Me.edTm_codpaga2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codpaga2.Size = New System.Drawing.Size(53, 20)
    Me.edTm_codpaga2.TabIndex = 69
    '
    'lbTm_codpaga2
    '
    Me.lbTm_codpaga2.AutoSize = True
    Me.lbTm_codpaga2.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codpaga2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_codpaga2.Location = New System.Drawing.Point(2, 84)
    Me.lbTm_codpaga2.Name = "lbTm_codpaga2"
    Me.lbTm_codpaga2.NTSDbField = ""
    Me.lbTm_codpaga2.Size = New System.Drawing.Size(89, 13)
    Me.lbTm_codpaga2.TabIndex = 68
    Me.lbTm_codpaga2.Text = "Cod pag contanti"
    Me.lbTm_codpaga2.Tooltip = ""
    Me.lbTm_codpaga2.UseMnemonic = False
    '
    'lbXx_vettor
    '
    Me.lbXx_vettor.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vettor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vettor.Location = New System.Drawing.Point(150, 104)
    Me.lbXx_vettor.Name = "lbXx_vettor"
    Me.lbXx_vettor.NTSDbField = ""
    Me.lbXx_vettor.Size = New System.Drawing.Size(210, 20)
    Me.lbXx_vettor.TabIndex = 67
    Me.lbXx_vettor.Tooltip = ""
    Me.lbXx_vettor.UseMnemonic = False
    '
    'edTm_cambio
    '
    Me.edTm_cambio.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_cambio.Enabled = False
    Me.edTm_cambio.Location = New System.Drawing.Point(266, 126)
    Me.edTm_cambio.Name = "edTm_cambio"
    Me.edTm_cambio.NTSDbField = ""
    Me.edTm_cambio.NTSFormat = "0"
    Me.edTm_cambio.NTSForzaVisZoom = False
    Me.edTm_cambio.NTSOldValue = ""
    Me.edTm_cambio.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_cambio.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_cambio.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_cambio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_cambio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_cambio.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_cambio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_cambio.Properties.MaxLength = 65536
    Me.edTm_cambio.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_cambio.Size = New System.Drawing.Size(94, 20)
    Me.edTm_cambio.TabIndex = 57
    '
    'edTm_vettor
    '
    Me.edTm_vettor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_vettor.Location = New System.Drawing.Point(94, 104)
    Me.edTm_vettor.Name = "edTm_vettor"
    Me.edTm_vettor.NTSDbField = ""
    Me.edTm_vettor.NTSFormat = "0"
    Me.edTm_vettor.NTSForzaVisZoom = False
    Me.edTm_vettor.NTSOldValue = ""
    Me.edTm_vettor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_vettor.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_vettor.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_vettor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_vettor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_vettor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_vettor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_vettor.Properties.MaxLength = 65536
    Me.edTm_vettor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_vettor.Size = New System.Drawing.Size(53, 20)
    Me.edTm_vettor.TabIndex = 66
    '
    'lbXx_valuta
    '
    Me.lbXx_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_valuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_valuta.Location = New System.Drawing.Point(150, 126)
    Me.lbXx_valuta.Name = "lbXx_valuta"
    Me.lbXx_valuta.NTSDbField = ""
    Me.lbXx_valuta.Size = New System.Drawing.Size(112, 20)
    Me.lbXx_valuta.TabIndex = 55
    Me.lbXx_valuta.Tooltip = ""
    Me.lbXx_valuta.UseMnemonic = False
    '
    'edTm_valuta
    '
    Me.edTm_valuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_valuta.Enabled = False
    Me.edTm_valuta.Location = New System.Drawing.Point(94, 126)
    Me.edTm_valuta.Name = "edTm_valuta"
    Me.edTm_valuta.NTSDbField = ""
    Me.edTm_valuta.NTSFormat = "0"
    Me.edTm_valuta.NTSForzaVisZoom = False
    Me.edTm_valuta.NTSOldValue = ""
    Me.edTm_valuta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_valuta.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_valuta.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_valuta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_valuta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_valuta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_valuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_valuta.Properties.MaxLength = 65536
    Me.edTm_valuta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_valuta.Size = New System.Drawing.Size(53, 20)
    Me.edTm_valuta.TabIndex = 54
    '
    'lbTm_valuta
    '
    Me.lbTm_valuta.AutoSize = True
    Me.lbTm_valuta.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_valuta.Location = New System.Drawing.Point(2, 128)
    Me.lbTm_valuta.Name = "lbTm_valuta"
    Me.lbTm_valuta.NTSDbField = ""
    Me.lbTm_valuta.Size = New System.Drawing.Size(74, 13)
    Me.lbTm_valuta.TabIndex = 53
    Me.lbTm_valuta.Text = "Valuta/cambio"
    Me.lbTm_valuta.Tooltip = ""
    Me.lbTm_valuta.UseMnemonic = False
    '
    'lbXx_codpaga
    '
    Me.lbXx_codpaga.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpaga.Location = New System.Drawing.Point(150, 34)
    Me.lbXx_codpaga.Name = "lbXx_codpaga"
    Me.lbXx_codpaga.NTSDbField = ""
    Me.lbXx_codpaga.Size = New System.Drawing.Size(210, 20)
    Me.lbXx_codpaga.TabIndex = 52
    Me.lbXx_codpaga.Tooltip = ""
    Me.lbXx_codpaga.UseMnemonic = False
    '
    'edTm_scont2
    '
    Me.edTm_scont2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_scont2.Enabled = False
    Me.edTm_scont2.Location = New System.Drawing.Point(316, 11)
    Me.edTm_scont2.Name = "edTm_scont2"
    Me.edTm_scont2.NTSDbField = ""
    Me.edTm_scont2.NTSFormat = "0"
    Me.edTm_scont2.NTSForzaVisZoom = False
    Me.edTm_scont2.NTSOldValue = ""
    Me.edTm_scont2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_scont2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_scont2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scont2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scont2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scont2.Properties.MaxLength = 65536
    Me.edTm_scont2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scont2.Size = New System.Drawing.Size(44, 20)
    Me.edTm_scont2.TabIndex = 51
    '
    'edTm_scont1
    '
    Me.edTm_scont1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTm_scont1.Enabled = False
    Me.edTm_scont1.Location = New System.Drawing.Point(266, 11)
    Me.edTm_scont1.Name = "edTm_scont1"
    Me.edTm_scont1.NTSDbField = ""
    Me.edTm_scont1.NTSFormat = "0"
    Me.edTm_scont1.NTSForzaVisZoom = False
    Me.edTm_scont1.NTSOldValue = ""
    Me.edTm_scont1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_scont1.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_scont1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scont1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scont1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scont1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scont1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scont1.Properties.MaxLength = 65536
    Me.edTm_scont1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scont1.Size = New System.Drawing.Size(43, 20)
    Me.edTm_scont1.TabIndex = 50
    '
    'edTm_datapag
    '
    Me.edTm_datapag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_datapag.Enabled = False
    Me.edTm_datapag.Location = New System.Drawing.Point(266, 58)
    Me.edTm_datapag.Name = "edTm_datapag"
    Me.edTm_datapag.NTSDbField = ""
    Me.edTm_datapag.NTSForzaVisZoom = False
    Me.edTm_datapag.NTSOldValue = ""
    Me.edTm_datapag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_datapag.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_datapag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_datapag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_datapag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_datapag.Properties.MaxLength = 65536
    Me.edTm_datapag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_datapag.Size = New System.Drawing.Size(94, 20)
    Me.edTm_datapag.TabIndex = 49
    '
    'edTm_scopag
    '
    Me.edTm_scopag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_scopag.Enabled = False
    Me.edTm_scopag.Location = New System.Drawing.Point(94, 58)
    Me.edTm_scopag.Name = "edTm_scopag"
    Me.edTm_scopag.NTSDbField = ""
    Me.edTm_scopag.NTSFormat = "0"
    Me.edTm_scopag.NTSForzaVisZoom = False
    Me.edTm_scopag.NTSOldValue = ""
    Me.edTm_scopag.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_scopag.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_scopag.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_scopag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_scopag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_scopag.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_scopag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_scopag.Properties.MaxLength = 65536
    Me.edTm_scopag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_scopag.Size = New System.Drawing.Size(53, 20)
    Me.edTm_scopag.TabIndex = 48
    '
    'edTm_codpaga
    '
    Me.edTm_codpaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codpaga.Enabled = False
    Me.edTm_codpaga.Location = New System.Drawing.Point(94, 34)
    Me.edTm_codpaga.Name = "edTm_codpaga"
    Me.edTm_codpaga.NTSDbField = ""
    Me.edTm_codpaga.NTSFormat = "0"
    Me.edTm_codpaga.NTSForzaVisZoom = False
    Me.edTm_codpaga.NTSOldValue = ""
    Me.edTm_codpaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codpaga.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codpaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codpaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codpaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codpaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codpaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codpaga.Properties.MaxLength = 65536
    Me.edTm_codpaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codpaga.Size = New System.Drawing.Size(53, 20)
    Me.edTm_codpaga.TabIndex = 47
    '
    'lbTm_vettor
    '
    Me.lbTm_vettor.AutoSize = True
    Me.lbTm_vettor.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_vettor.Location = New System.Drawing.Point(2, 107)
    Me.lbTm_vettor.Name = "lbTm_vettor"
    Me.lbTm_vettor.NTSDbField = ""
    Me.lbTm_vettor.Size = New System.Drawing.Size(43, 13)
    Me.lbTm_vettor.TabIndex = 55
    Me.lbTm_vettor.Text = "Vettore"
    Me.lbTm_vettor.Tooltip = ""
    Me.lbTm_vettor.UseMnemonic = False
    '
    'edTm_listino
    '
    Me.edTm_listino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_listino.Enabled = False
    Me.edTm_listino.Location = New System.Drawing.Point(94, 11)
    Me.edTm_listino.Name = "edTm_listino"
    Me.edTm_listino.NTSDbField = ""
    Me.edTm_listino.NTSFormat = "0"
    Me.edTm_listino.NTSForzaVisZoom = False
    Me.edTm_listino.NTSOldValue = ""
    Me.edTm_listino.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_listino.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_listino.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_listino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_listino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_listino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_listino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_listino.Properties.MaxLength = 65536
    Me.edTm_listino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_listino.Size = New System.Drawing.Size(53, 20)
    Me.edTm_listino.TabIndex = 46
    '
    'fmCa
    '
    Me.fmCa.AllowDrop = True
    Me.fmCa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmCa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCa.Appearance.Options.UseBackColor = True
    Me.fmCa.Controls.Add(Me.edTm_subcommeca)
    Me.fmCa.Controls.Add(Me.lbTm_subcommeca)
    Me.fmCa.Controls.Add(Me.lbXx_commeca)
    Me.fmCa.Controls.Add(Me.lbXx_codcena)
    Me.fmCa.Controls.Add(Me.edTm_codcena)
    Me.fmCa.Controls.Add(Me.edTm_commeca)
    Me.fmCa.Controls.Add(Me.lbTm_codcena)
    Me.fmCa.Controls.Add(Me.lbTm_commeca)
    Me.fmCa.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCa.Location = New System.Drawing.Point(6, 268)
    Me.fmCa.Name = "fmCa"
    Me.fmCa.Size = New System.Drawing.Size(359, 92)
    Me.fmCa.TabIndex = 8
    Me.fmCa.Text = "Contabilità ind."
    '
    'edTm_subcommeca
    '
    Me.edTm_subcommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_subcommeca.Enabled = False
    Me.edTm_subcommeca.Location = New System.Drawing.Point(88, 43)
    Me.edTm_subcommeca.Name = "edTm_subcommeca"
    Me.edTm_subcommeca.NTSDbField = ""
    Me.edTm_subcommeca.NTSForzaVisZoom = False
    Me.edTm_subcommeca.NTSOldValue = ""
    Me.edTm_subcommeca.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_subcommeca.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_subcommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_subcommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_subcommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_subcommeca.Properties.MaxLength = 65536
    Me.edTm_subcommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_subcommeca.Size = New System.Drawing.Size(52, 20)
    Me.edTm_subcommeca.TabIndex = 57
    '
    'lbTm_subcommeca
    '
    Me.lbTm_subcommeca.AutoSize = True
    Me.lbTm_subcommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_subcommeca.Location = New System.Drawing.Point(4, 46)
    Me.lbTm_subcommeca.Name = "lbTm_subcommeca"
    Me.lbTm_subcommeca.NTSDbField = ""
    Me.lbTm_subcommeca.Size = New System.Drawing.Size(59, 13)
    Me.lbTm_subcommeca.TabIndex = 56
    Me.lbTm_subcommeca.Text = "Sub comm."
    Me.lbTm_subcommeca.Tooltip = ""
    Me.lbTm_subcommeca.UseMnemonic = False
    '
    'lbXx_commeca
    '
    Me.lbXx_commeca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_commeca.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_commeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_commeca.Location = New System.Drawing.Point(174, 21)
    Me.lbXx_commeca.Name = "lbXx_commeca"
    Me.lbXx_commeca.NTSDbField = ""
    Me.lbXx_commeca.Size = New System.Drawing.Size(180, 20)
    Me.lbXx_commeca.TabIndex = 55
    Me.lbXx_commeca.Tooltip = ""
    Me.lbXx_commeca.UseMnemonic = False
    '
    'lbXx_codcena
    '
    Me.lbXx_codcena.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_codcena.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcena.Location = New System.Drawing.Point(143, 65)
    Me.lbXx_codcena.Name = "lbXx_codcena"
    Me.lbXx_codcena.NTSDbField = ""
    Me.lbXx_codcena.Size = New System.Drawing.Size(211, 20)
    Me.lbXx_codcena.TabIndex = 53
    Me.lbXx_codcena.Tooltip = ""
    Me.lbXx_codcena.UseMnemonic = False
    '
    'edTm_codcena
    '
    Me.edTm_codcena.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codcena.Location = New System.Drawing.Point(88, 65)
    Me.edTm_codcena.Name = "edTm_codcena"
    Me.edTm_codcena.NTSDbField = ""
    Me.edTm_codcena.NTSFormat = "0"
    Me.edTm_codcena.NTSForzaVisZoom = False
    Me.edTm_codcena.NTSOldValue = ""
    Me.edTm_codcena.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codcena.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codcena.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codcena.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codcena.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codcena.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codcena.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codcena.Properties.MaxLength = 65536
    Me.edTm_codcena.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codcena.Size = New System.Drawing.Size(52, 20)
    Me.edTm_codcena.TabIndex = 51
    '
    'edTm_commeca
    '
    Me.edTm_commeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_commeca.Enabled = False
    Me.edTm_commeca.Location = New System.Drawing.Point(88, 21)
    Me.edTm_commeca.Name = "edTm_commeca"
    Me.edTm_commeca.NTSDbField = ""
    Me.edTm_commeca.NTSFormat = "0"
    Me.edTm_commeca.NTSForzaVisZoom = False
    Me.edTm_commeca.NTSOldValue = ""
    Me.edTm_commeca.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_commeca.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_commeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_commeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_commeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_commeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_commeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_commeca.Properties.MaxLength = 65536
    Me.edTm_commeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_commeca.Size = New System.Drawing.Size(83, 20)
    Me.edTm_commeca.TabIndex = 47
    '
    'lbTm_codcena
    '
    Me.lbTm_codcena.AutoSize = True
    Me.lbTm_codcena.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codcena.Location = New System.Drawing.Point(4, 68)
    Me.lbTm_codcena.Name = "lbTm_codcena"
    Me.lbTm_codcena.NTSDbField = ""
    Me.lbTm_codcena.Size = New System.Drawing.Size(40, 13)
    Me.lbTm_codcena.TabIndex = 7
    Me.lbTm_codcena.Text = "Centro"
    Me.lbTm_codcena.Tooltip = ""
    Me.lbTm_codcena.UseMnemonic = False
    '
    'lbTm_commeca
    '
    Me.lbTm_commeca.AutoSize = True
    Me.lbTm_commeca.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_commeca.Location = New System.Drawing.Point(4, 24)
    Me.lbTm_commeca.Name = "lbTm_commeca"
    Me.lbTm_commeca.NTSDbField = ""
    Me.lbTm_commeca.Size = New System.Drawing.Size(58, 13)
    Me.lbTm_commeca.TabIndex = 6
    Me.lbTm_commeca.Text = "Commessa"
    Me.lbTm_commeca.Tooltip = ""
    Me.lbTm_commeca.UseMnemonic = False
    '
    'lbScontiTesta
    '
    Me.lbScontiTesta.AutoSize = True
    Me.lbScontiTesta.BackColor = System.Drawing.Color.Transparent
    Me.lbScontiTesta.Location = New System.Drawing.Point(184, 14)
    Me.lbScontiTesta.Name = "lbScontiTesta"
    Me.lbScontiTesta.NTSDbField = ""
    Me.lbScontiTesta.Size = New System.Drawing.Size(77, 13)
    Me.lbScontiTesta.TabIndex = 7
    Me.lbScontiTesta.Text = "Sconti generali"
    Me.lbScontiTesta.Tooltip = ""
    Me.lbScontiTesta.UseMnemonic = False
    '
    'fmBanca
    '
    Me.fmBanca.AllowDrop = True
    Me.fmBanca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmBanca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmBanca.Appearance.Options.UseBackColor = True
    Me.fmBanca.Controls.Add(Me.ckTm_autpag)
    Me.fmBanca.Controls.Add(Me.lbXx_codbanc)
    Me.fmBanca.Controls.Add(Me.edTm_codbanc)
    Me.fmBanca.Controls.Add(Me.lbTm_codbanc)
    Me.fmBanca.Controls.Add(Me.edTm_banc2)
    Me.fmBanca.Controls.Add(Me.edTm_banc1)
    Me.fmBanca.Controls.Add(Me.edTm_cab)
    Me.fmBanca.Controls.Add(Me.edTm_abi)
    Me.fmBanca.Controls.Add(Me.lbTm_cab)
    Me.fmBanca.Controls.Add(Me.lbTm_abi)
    Me.fmBanca.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmBanca.Location = New System.Drawing.Point(6, 172)
    Me.fmBanca.Name = "fmBanca"
    Me.fmBanca.Size = New System.Drawing.Size(359, 90)
    Me.fmBanca.TabIndex = 6
    Me.fmBanca.Text = "Banca d'appoggio"
    '
    'ckTm_autpag
    '
    Me.ckTm_autpag.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ckTm_autpag.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_autpag.Location = New System.Drawing.Point(280, 69)
    Me.ckTm_autpag.Name = "ckTm_autpag"
    Me.ckTm_autpag.NTSCheckValue = "S"
    Me.ckTm_autpag.NTSUnCheckValue = "N"
    Me.ckTm_autpag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_autpag.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_autpag.Properties.Caption = "Aut. pag."
    Me.ckTm_autpag.Size = New System.Drawing.Size(74, 19)
    Me.ckTm_autpag.TabIndex = 54
    '
    'lbXx_codbanc
    '
    Me.lbXx_codbanc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codbanc.Location = New System.Drawing.Point(144, 66)
    Me.lbXx_codbanc.Name = "lbXx_codbanc"
    Me.lbXx_codbanc.NTSDbField = ""
    Me.lbXx_codbanc.Size = New System.Drawing.Size(130, 20)
    Me.lbXx_codbanc.TabIndex = 53
    Me.lbXx_codbanc.Tooltip = ""
    Me.lbXx_codbanc.UseMnemonic = False
    '
    'edTm_codbanc
    '
    Me.edTm_codbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codbanc.Location = New System.Drawing.Point(88, 66)
    Me.edTm_codbanc.Name = "edTm_codbanc"
    Me.edTm_codbanc.NTSDbField = ""
    Me.edTm_codbanc.NTSFormat = "0"
    Me.edTm_codbanc.NTSForzaVisZoom = False
    Me.edTm_codbanc.NTSOldValue = ""
    Me.edTm_codbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codbanc.Properties.MaxLength = 65536
    Me.edTm_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codbanc.Size = New System.Drawing.Size(52, 20)
    Me.edTm_codbanc.TabIndex = 52
    '
    'lbTm_codbanc
    '
    Me.lbTm_codbanc.AutoSize = True
    Me.lbTm_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codbanc.Location = New System.Drawing.Point(7, 69)
    Me.lbTm_codbanc.Name = "lbTm_codbanc"
    Me.lbTm_codbanc.NTSDbField = ""
    Me.lbTm_codbanc.Size = New System.Drawing.Size(55, 13)
    Me.lbTm_codbanc.TabIndex = 51
    Me.lbTm_codbanc.Text = "Ns. Banca"
    Me.lbTm_codbanc.Tooltip = ""
    Me.lbTm_codbanc.UseMnemonic = False
    '
    'edTm_banc2
    '
    Me.edTm_banc2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_banc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_banc2.Location = New System.Drawing.Point(144, 44)
    Me.edTm_banc2.Name = "edTm_banc2"
    Me.edTm_banc2.NTSDbField = ""
    Me.edTm_banc2.NTSForzaVisZoom = False
    Me.edTm_banc2.NTSOldValue = ""
    Me.edTm_banc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_banc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_banc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_banc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_banc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_banc2.Properties.MaxLength = 65536
    Me.edTm_banc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_banc2.Size = New System.Drawing.Size(210, 20)
    Me.edTm_banc2.TabIndex = 50
    '
    'edTm_banc1
    '
    Me.edTm_banc1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_banc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_banc1.Location = New System.Drawing.Point(144, 22)
    Me.edTm_banc1.Name = "edTm_banc1"
    Me.edTm_banc1.NTSDbField = ""
    Me.edTm_banc1.NTSForzaVisZoom = False
    Me.edTm_banc1.NTSOldValue = ""
    Me.edTm_banc1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_banc1.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_banc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_banc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_banc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_banc1.Properties.MaxLength = 65536
    Me.edTm_banc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_banc1.Size = New System.Drawing.Size(210, 20)
    Me.edTm_banc1.TabIndex = 49
    '
    'edTm_cab
    '
    Me.edTm_cab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_cab.Location = New System.Drawing.Point(88, 44)
    Me.edTm_cab.Name = "edTm_cab"
    Me.edTm_cab.NTSDbField = ""
    Me.edTm_cab.NTSFormat = "0"
    Me.edTm_cab.NTSForzaVisZoom = False
    Me.edTm_cab.NTSOldValue = ""
    Me.edTm_cab.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_cab.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_cab.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_cab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_cab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_cab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_cab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_cab.Properties.MaxLength = 65536
    Me.edTm_cab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_cab.Size = New System.Drawing.Size(52, 20)
    Me.edTm_cab.TabIndex = 48
    '
    'edTm_abi
    '
    Me.edTm_abi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_abi.Location = New System.Drawing.Point(88, 22)
    Me.edTm_abi.Name = "edTm_abi"
    Me.edTm_abi.NTSDbField = ""
    Me.edTm_abi.NTSFormat = "0"
    Me.edTm_abi.NTSForzaVisZoom = False
    Me.edTm_abi.NTSOldValue = ""
    Me.edTm_abi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_abi.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_abi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_abi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_abi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_abi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_abi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_abi.Properties.MaxLength = 65536
    Me.edTm_abi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_abi.Size = New System.Drawing.Size(52, 20)
    Me.edTm_abi.TabIndex = 47
    '
    'lbTm_cab
    '
    Me.lbTm_cab.AutoSize = True
    Me.lbTm_cab.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_cab.Location = New System.Drawing.Point(7, 47)
    Me.lbTm_cab.Name = "lbTm_cab"
    Me.lbTm_cab.NTSDbField = ""
    Me.lbTm_cab.Size = New System.Drawing.Size(26, 13)
    Me.lbTm_cab.TabIndex = 6
    Me.lbTm_cab.Text = "Cab"
    Me.lbTm_cab.Tooltip = ""
    Me.lbTm_cab.UseMnemonic = False
    '
    'lbTm_abi
    '
    Me.lbTm_abi.AutoSize = True
    Me.lbTm_abi.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_abi.Location = New System.Drawing.Point(7, 25)
    Me.lbTm_abi.Name = "lbTm_abi"
    Me.lbTm_abi.NTSDbField = ""
    Me.lbTm_abi.Size = New System.Drawing.Size(22, 13)
    Me.lbTm_abi.TabIndex = 5
    Me.lbTm_abi.Text = "Abi"
    Me.lbTm_abi.Tooltip = ""
    Me.lbTm_abi.UseMnemonic = False
    '
    'lbTm_datapag
    '
    Me.lbTm_datapag.AutoSize = True
    Me.lbTm_datapag.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_datapag.Location = New System.Drawing.Point(184, 61)
    Me.lbTm_datapag.Name = "lbTm_datapag"
    Me.lbTm_datapag.NTSDbField = ""
    Me.lbTm_datapag.Size = New System.Drawing.Size(64, 13)
    Me.lbTm_datapag.TabIndex = 4
    Me.lbTm_datapag.Text = "Data 1 pag."
    Me.lbTm_datapag.Tooltip = ""
    Me.lbTm_datapag.UseMnemonic = False
    '
    'lbTm_scopag
    '
    Me.lbTm_scopag.AutoSize = True
    Me.lbTm_scopag.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_scopag.Location = New System.Drawing.Point(2, 61)
    Me.lbTm_scopag.Name = "lbTm_scopag"
    Me.lbTm_scopag.NTSDbField = ""
    Me.lbTm_scopag.Size = New System.Drawing.Size(79, 13)
    Me.lbTm_scopag.TabIndex = 3
    Me.lbTm_scopag.Text = "Sconto Pagam."
    Me.lbTm_scopag.Tooltip = ""
    Me.lbTm_scopag.UseMnemonic = False
    '
    'lbTm_codpaga
    '
    Me.lbTm_codpaga.AutoSize = True
    Me.lbTm_codpaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codpaga.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_codpaga.Location = New System.Drawing.Point(2, 37)
    Me.lbTm_codpaga.Name = "lbTm_codpaga"
    Me.lbTm_codpaga.NTSDbField = ""
    Me.lbTm_codpaga.Size = New System.Drawing.Size(69, 13)
    Me.lbTm_codpaga.TabIndex = 2
    Me.lbTm_codpaga.Text = "Cod. Pagam."
    Me.lbTm_codpaga.Tooltip = ""
    Me.lbTm_codpaga.UseMnemonic = False
    '
    'lbTm_listino
    '
    Me.lbTm_listino.AutoSize = True
    Me.lbTm_listino.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_listino.Location = New System.Drawing.Point(2, 14)
    Me.lbTm_listino.Name = "lbTm_listino"
    Me.lbTm_listino.NTSDbField = ""
    Me.lbTm_listino.Size = New System.Drawing.Size(37, 13)
    Me.lbTm_listino.TabIndex = 1
    Me.lbTm_listino.Text = "Listino"
    Me.lbTm_listino.Tooltip = ""
    Me.lbTm_listino.UseMnemonic = False
    '
    'pnTestataSx
    '
    Me.pnTestataSx.AllowDrop = True
    Me.pnTestataSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTestataSx.Appearance.Options.UseBackColor = True
    Me.pnTestataSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTestataSx.Controls.Add(Me.lbXx_causcap)
    Me.pnTestataSx.Controls.Add(Me.edTm_causcap)
    Me.pnTestataSx.Controls.Add(Me.lbTm_causcap)
    Me.pnTestataSx.Controls.Add(Me.lbXx_causale)
    Me.pnTestataSx.Controls.Add(Me.edTm_causale)
    Me.pnTestataSx.Controls.Add(Me.lbTm_causale)
    Me.pnTestataSx.Controls.Add(Me.lbXx_contfatt)
    Me.pnTestataSx.Controls.Add(Me.edTm_contfatt)
    Me.pnTestataSx.Controls.Add(Me.lbTm_contfatt)
    Me.pnTestataSx.Controls.Add(Me.lbXx_controp)
    Me.pnTestataSx.Controls.Add(Me.lbTm_controp)
    Me.pnTestataSx.Controls.Add(Me.edTm_controp)
    Me.pnTestataSx.Controls.Add(Me.lbXx_magimp)
    Me.pnTestataSx.Controls.Add(Me.lbXx_magaz2)
    Me.pnTestataSx.Controls.Add(Me.edTm_magimp)
    Me.pnTestataSx.Controls.Add(Me.edTm_magaz2)
    Me.pnTestataSx.Controls.Add(Me.lbTm_magimp)
    Me.pnTestataSx.Controls.Add(Me.lbTm_magaz2)
    Me.pnTestataSx.Controls.Add(Me.fmStato)
    Me.pnTestataSx.Controls.Add(Me.edTm_riferim)
    Me.pnTestataSx.Controls.Add(Me.lbXx_codese)
    Me.pnTestataSx.Controls.Add(Me.lbXx_magaz)
    Me.pnTestataSx.Controls.Add(Me.edTm_codese)
    Me.pnTestataSx.Controls.Add(Me.edTm_magaz)
    Me.pnTestataSx.Controls.Add(Me.lbXx_codagen)
    Me.pnTestataSx.Controls.Add(Me.lbXx_tipobf)
    Me.pnTestataSx.Controls.Add(Me.edtm_codagen)
    Me.pnTestataSx.Controls.Add(Me.edTm_tipobf)
    Me.pnTestataSx.Controls.Add(Me.lbTm_riferim)
    Me.pnTestataSx.Controls.Add(Me.lbTm_codese)
    Me.pnTestataSx.Controls.Add(Me.lbTm_magaz)
    Me.pnTestataSx.Controls.Add(Me.lbtm_codagen)
    Me.pnTestataSx.Controls.Add(Me.lbTm_tipobf)
    Me.pnTestataSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTestataSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTestataSx.Location = New System.Drawing.Point(0, 0)
    Me.pnTestataSx.Name = "pnTestataSx"
    Me.pnTestataSx.NTSActiveTrasparency = True
    Me.pnTestataSx.Size = New System.Drawing.Size(367, 373)
    Me.pnTestataSx.TabIndex = 0
    '
    'lbXx_causcap
    '
    Me.lbXx_causcap.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_causcap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_causcap.Location = New System.Drawing.Point(151, 194)
    Me.lbXx_causcap.Name = "lbXx_causcap"
    Me.lbXx_causcap.NTSDbField = ""
    Me.lbXx_causcap.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_causcap.TabIndex = 95
    Me.lbXx_causcap.Tooltip = ""
    Me.lbXx_causcap.UseMnemonic = False
    '
    'edTm_causcap
    '
    Me.edTm_causcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_causcap.Enabled = False
    Me.edTm_causcap.Location = New System.Drawing.Point(98, 194)
    Me.edTm_causcap.Name = "edTm_causcap"
    Me.edTm_causcap.NTSDbField = ""
    Me.edTm_causcap.NTSFormat = "0"
    Me.edTm_causcap.NTSForzaVisZoom = False
    Me.edTm_causcap.NTSOldValue = ""
    Me.edTm_causcap.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_causcap.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_causcap.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_causcap.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_causcap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_causcap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_causcap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_causcap.Properties.MaxLength = 65536
    Me.edTm_causcap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_causcap.Size = New System.Drawing.Size(52, 20)
    Me.edTm_causcap.TabIndex = 94
    '
    'lbTm_causcap
    '
    Me.lbTm_causcap.AutoSize = True
    Me.lbTm_causcap.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_causcap.Location = New System.Drawing.Point(5, 197)
    Me.lbTm_causcap.Name = "lbTm_causcap"
    Me.lbTm_causcap.NTSDbField = ""
    Me.lbTm_causcap.Size = New System.Drawing.Size(81, 13)
    Me.lbTm_causcap.TabIndex = 93
    Me.lbTm_causcap.Text = "Causale scarico"
    Me.lbTm_causcap.Tooltip = ""
    Me.lbTm_causcap.UseMnemonic = False
    '
    'lbXx_causale
    '
    Me.lbXx_causale.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_causale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_causale.Location = New System.Drawing.Point(151, 104)
    Me.lbXx_causale.Name = "lbXx_causale"
    Me.lbXx_causale.NTSDbField = ""
    Me.lbXx_causale.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_causale.TabIndex = 92
    Me.lbXx_causale.Tooltip = ""
    Me.lbXx_causale.UseMnemonic = False
    '
    'edTm_causale
    '
    Me.edTm_causale.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_causale.Enabled = False
    Me.edTm_causale.Location = New System.Drawing.Point(98, 104)
    Me.edTm_causale.Name = "edTm_causale"
    Me.edTm_causale.NTSDbField = ""
    Me.edTm_causale.NTSFormat = "0"
    Me.edTm_causale.NTSForzaVisZoom = False
    Me.edTm_causale.NTSOldValue = ""
    Me.edTm_causale.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_causale.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_causale.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_causale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_causale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_causale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_causale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_causale.Properties.MaxLength = 65536
    Me.edTm_causale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_causale.Size = New System.Drawing.Size(52, 20)
    Me.edTm_causale.TabIndex = 91
    '
    'lbTm_causale
    '
    Me.lbTm_causale.AutoSize = True
    Me.lbTm_causale.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_causale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_causale.Location = New System.Drawing.Point(5, 107)
    Me.lbTm_causale.Name = "lbTm_causale"
    Me.lbTm_causale.NTSDbField = ""
    Me.lbTm_causale.Size = New System.Drawing.Size(88, 13)
    Me.lbTm_causale.TabIndex = 90
    Me.lbTm_causale.Text = "Causale magazz."
    Me.lbTm_causale.Tooltip = ""
    Me.lbTm_causale.UseMnemonic = False
    '
    'lbXx_contfatt
    '
    Me.lbXx_contfatt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbXx_contfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_contfatt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_contfatt.Location = New System.Drawing.Point(182, 241)
    Me.lbXx_contfatt.Name = "lbXx_contfatt"
    Me.lbXx_contfatt.NTSDbField = ""
    Me.lbXx_contfatt.Size = New System.Drawing.Size(177, 20)
    Me.lbXx_contfatt.TabIndex = 89
    Me.lbXx_contfatt.Tooltip = ""
    Me.lbXx_contfatt.UseMnemonic = False
    '
    'edTm_contfatt
    '
    Me.edTm_contfatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_contfatt.Enabled = False
    Me.edTm_contfatt.Location = New System.Drawing.Point(98, 241)
    Me.edTm_contfatt.Name = "edTm_contfatt"
    Me.edTm_contfatt.NTSDbField = ""
    Me.edTm_contfatt.NTSFormat = "0"
    Me.edTm_contfatt.NTSForzaVisZoom = False
    Me.edTm_contfatt.NTSOldValue = ""
    Me.edTm_contfatt.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_contfatt.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_contfatt.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_contfatt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_contfatt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_contfatt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_contfatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_contfatt.Properties.MaxLength = 65536
    Me.edTm_contfatt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_contfatt.Size = New System.Drawing.Size(80, 20)
    Me.edTm_contfatt.TabIndex = 88
    '
    'lbTm_contfatt
    '
    Me.lbTm_contfatt.AutoSize = True
    Me.lbTm_contfatt.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_contfatt.Location = New System.Drawing.Point(5, 244)
    Me.lbTm_contfatt.Name = "lbTm_contfatt"
    Me.lbTm_contfatt.NTSDbField = ""
    Me.lbTm_contfatt.Size = New System.Drawing.Size(82, 13)
    Me.lbTm_contfatt.TabIndex = 87
    Me.lbTm_contfatt.Text = "Conto fatturaz."
    Me.lbTm_contfatt.Tooltip = ""
    Me.lbTm_contfatt.UseMnemonic = False
    '
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(151, 81)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(207, 20)
    Me.lbXx_controp.TabIndex = 85
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbTm_controp
    '
    Me.lbTm_controp.AutoSize = True
    Me.lbTm_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_controp.Location = New System.Drawing.Point(5, 84)
    Me.lbTm_controp.Name = "lbTm_controp"
    Me.lbTm_controp.NTSDbField = ""
    Me.lbTm_controp.Size = New System.Drawing.Size(76, 13)
    Me.lbTm_controp.TabIndex = 84
    Me.lbTm_controp.Text = "Cod. Controp."
    Me.lbTm_controp.Tooltip = ""
    Me.lbTm_controp.UseMnemonic = False
    '
    'edTm_controp
    '
    Me.edTm_controp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_controp.Enabled = False
    Me.edTm_controp.Location = New System.Drawing.Point(98, 81)
    Me.edTm_controp.Name = "edTm_controp"
    Me.edTm_controp.NTSDbField = ""
    Me.edTm_controp.NTSFormat = "0"
    Me.edTm_controp.NTSForzaVisZoom = False
    Me.edTm_controp.NTSOldValue = ""
    Me.edTm_controp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_controp.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_controp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_controp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_controp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_controp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_controp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_controp.Properties.MaxLength = 65536
    Me.edTm_controp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_controp.Size = New System.Drawing.Size(51, 20)
    Me.edTm_controp.TabIndex = 83
    '
    'lbXx_magimp
    '
    Me.lbXx_magimp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magimp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magimp.Location = New System.Drawing.Point(151, 172)
    Me.lbXx_magimp.Name = "lbXx_magimp"
    Me.lbXx_magimp.NTSDbField = ""
    Me.lbXx_magimp.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_magimp.TabIndex = 64
    Me.lbXx_magimp.Tooltip = ""
    Me.lbXx_magimp.UseMnemonic = False
    '
    'lbXx_magaz2
    '
    Me.lbXx_magaz2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz2.Location = New System.Drawing.Point(151, 150)
    Me.lbXx_magaz2.Name = "lbXx_magaz2"
    Me.lbXx_magaz2.NTSDbField = ""
    Me.lbXx_magaz2.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_magaz2.TabIndex = 63
    Me.lbXx_magaz2.Tooltip = ""
    Me.lbXx_magaz2.UseMnemonic = False
    '
    'edTm_magimp
    '
    Me.edTm_magimp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_magimp.Enabled = False
    Me.edTm_magimp.Location = New System.Drawing.Point(98, 172)
    Me.edTm_magimp.Name = "edTm_magimp"
    Me.edTm_magimp.NTSDbField = ""
    Me.edTm_magimp.NTSFormat = "0"
    Me.edTm_magimp.NTSForzaVisZoom = False
    Me.edTm_magimp.NTSOldValue = ""
    Me.edTm_magimp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_magimp.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_magimp.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_magimp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_magimp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_magimp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_magimp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_magimp.Properties.MaxLength = 65536
    Me.edTm_magimp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_magimp.Size = New System.Drawing.Size(52, 20)
    Me.edTm_magimp.TabIndex = 62
    '
    'edTm_magaz2
    '
    Me.edTm_magaz2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_magaz2.Enabled = False
    Me.edTm_magaz2.Location = New System.Drawing.Point(98, 150)
    Me.edTm_magaz2.Name = "edTm_magaz2"
    Me.edTm_magaz2.NTSDbField = ""
    Me.edTm_magaz2.NTSFormat = "0"
    Me.edTm_magaz2.NTSForzaVisZoom = False
    Me.edTm_magaz2.NTSOldValue = ""
    Me.edTm_magaz2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_magaz2.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_magaz2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_magaz2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_magaz2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_magaz2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_magaz2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_magaz2.Properties.MaxLength = 65536
    Me.edTm_magaz2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_magaz2.Size = New System.Drawing.Size(52, 20)
    Me.edTm_magaz2.TabIndex = 61
    '
    'lbTm_magimp
    '
    Me.lbTm_magimp.AutoSize = True
    Me.lbTm_magimp.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_magimp.Location = New System.Drawing.Point(5, 175)
    Me.lbTm_magimp.Name = "lbTm_magimp"
    Me.lbTm_magimp.NTSDbField = ""
    Me.lbTm_magimp.Size = New System.Drawing.Size(80, 13)
    Me.lbTm_magimp.TabIndex = 60
    Me.lbTm_magimp.Text = "Magazzino imp."
    Me.lbTm_magimp.Tooltip = ""
    Me.lbTm_magimp.UseMnemonic = False
    '
    'lbTm_magaz2
    '
    Me.lbTm_magaz2.AutoSize = True
    Me.lbTm_magaz2.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_magaz2.Location = New System.Drawing.Point(5, 153)
    Me.lbTm_magaz2.Name = "lbTm_magaz2"
    Me.lbTm_magaz2.NTSDbField = ""
    Me.lbTm_magaz2.Size = New System.Drawing.Size(66, 13)
    Me.lbTm_magaz2.TabIndex = 59
    Me.lbTm_magaz2.Text = "Magazzino 2"
    Me.lbTm_magaz2.Tooltip = ""
    Me.lbTm_magaz2.UseMnemonic = False
    '
    'fmStato
    '
    Me.fmStato.AllowDrop = True
    Me.fmStato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStato.Appearance.Options.UseBackColor = True
    Me.fmStato.Controls.Add(Me.ckTm_vistato)
    Me.fmStato.Controls.Add(Me.ckTm_flevas)
    Me.fmStato.Controls.Add(Me.ckTm_flbolli)
    Me.fmStato.Controls.Add(Me.ckTm_flspinc)
    Me.fmStato.Controls.Add(Me.ckTm_scorpo)
    Me.fmStato.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStato.Location = New System.Drawing.Point(6, 268)
    Me.fmStato.Name = "fmStato"
    Me.fmStato.Size = New System.Drawing.Size(352, 92)
    Me.fmStato.TabIndex = 58
    Me.fmStato.Text = "Stato / opzioni"
    '
    'ckTm_vistato
    '
    Me.ckTm_vistato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_vistato.Location = New System.Drawing.Point(111, 50)
    Me.ckTm_vistato.Name = "ckTm_vistato"
    Me.ckTm_vistato.NTSCheckValue = "S"
    Me.ckTm_vistato.NTSUnCheckValue = "N"
    Me.ckTm_vistato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_vistato.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_vistato.Properties.Caption = "Vistato"
    Me.ckTm_vistato.Size = New System.Drawing.Size(61, 19)
    Me.ckTm_vistato.TabIndex = 6
    '
    'ckTm_flevas
    '
    Me.ckTm_flevas.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_flevas.Enabled = False
    Me.ckTm_flevas.Location = New System.Drawing.Point(10, 50)
    Me.ckTm_flevas.Name = "ckTm_flevas"
    Me.ckTm_flevas.NTSCheckValue = "S"
    Me.ckTm_flevas.NTSUnCheckValue = "N"
    Me.ckTm_flevas.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flevas.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flevas.Properties.Caption = "Nota Evasa"
    Me.ckTm_flevas.Size = New System.Drawing.Size(85, 19)
    Me.ckTm_flevas.TabIndex = 5
    '
    'ckTm_flbolli
    '
    Me.ckTm_flbolli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_flbolli.Enabled = False
    Me.ckTm_flbolli.Location = New System.Drawing.Point(235, 25)
    Me.ckTm_flbolli.Name = "ckTm_flbolli"
    Me.ckTm_flbolli.NTSCheckValue = "S"
    Me.ckTm_flbolli.NTSUnCheckValue = "N"
    Me.ckTm_flbolli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flbolli.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flbolli.Properties.Caption = "&Add. &Bolli"
    Me.ckTm_flbolli.Size = New System.Drawing.Size(70, 19)
    Me.ckTm_flbolli.TabIndex = 2
    '
    'ckTm_flspinc
    '
    Me.ckTm_flspinc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_flspinc.Enabled = False
    Me.ckTm_flspinc.Location = New System.Drawing.Point(111, 25)
    Me.ckTm_flspinc.Name = "ckTm_flspinc"
    Me.ckTm_flspinc.NTSCheckValue = "S"
    Me.ckTm_flspinc.NTSUnCheckValue = "N"
    Me.ckTm_flspinc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_flspinc.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_flspinc.Properties.Caption = "&Add. sp. &Incasso"
    Me.ckTm_flspinc.Size = New System.Drawing.Size(105, 19)
    Me.ckTm_flspinc.TabIndex = 1
    '
    'ckTm_scorpo
    '
    Me.ckTm_scorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTm_scorpo.Enabled = False
    Me.ckTm_scorpo.Location = New System.Drawing.Point(10, 25)
    Me.ckTm_scorpo.Name = "ckTm_scorpo"
    Me.ckTm_scorpo.NTSCheckValue = "S"
    Me.ckTm_scorpo.NTSUnCheckValue = "N"
    Me.ckTm_scorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTm_scorpo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTm_scorpo.Properties.Caption = "Scor&poro"
    Me.ckTm_scorpo.Size = New System.Drawing.Size(69, 19)
    Me.ckTm_scorpo.TabIndex = 0
    '
    'edTm_riferim
    '
    Me.edTm_riferim.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTm_riferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_riferim.Location = New System.Drawing.Point(98, 218)
    Me.edTm_riferim.Name = "edTm_riferim"
    Me.edTm_riferim.NTSDbField = ""
    Me.edTm_riferim.NTSForzaVisZoom = False
    Me.edTm_riferim.NTSOldValue = ""
    Me.edTm_riferim.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_riferim.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_riferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_riferim.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_riferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_riferim.Properties.MaxLength = 65536
    Me.edTm_riferim.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_riferim.Size = New System.Drawing.Size(261, 20)
    Me.edTm_riferim.TabIndex = 57
    '
    'lbXx_codese
    '
    Me.lbXx_codese.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codese.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codese.Location = New System.Drawing.Point(151, 58)
    Me.lbXx_codese.Name = "lbXx_codese"
    Me.lbXx_codese.NTSDbField = ""
    Me.lbXx_codese.Size = New System.Drawing.Size(207, 20)
    Me.lbXx_codese.TabIndex = 54
    Me.lbXx_codese.Tooltip = ""
    Me.lbXx_codese.UseMnemonic = False
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(151, 127)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_magaz.TabIndex = 53
    Me.lbXx_magaz.Tooltip = ""
    Me.lbXx_magaz.UseMnemonic = False
    '
    'edTm_codese
    '
    Me.edTm_codese.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_codese.Enabled = False
    Me.edTm_codese.Location = New System.Drawing.Point(98, 58)
    Me.edTm_codese.Name = "edTm_codese"
    Me.edTm_codese.NTSDbField = ""
    Me.edTm_codese.NTSFormat = "0"
    Me.edTm_codese.NTSForzaVisZoom = False
    Me.edTm_codese.NTSOldValue = ""
    Me.edTm_codese.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_codese.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_codese.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_codese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_codese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_codese.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_codese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_codese.Properties.MaxLength = 65536
    Me.edTm_codese.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_codese.Size = New System.Drawing.Size(52, 20)
    Me.edTm_codese.TabIndex = 52
    '
    'edTm_magaz
    '
    Me.edTm_magaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_magaz.Enabled = False
    Me.edTm_magaz.Location = New System.Drawing.Point(98, 127)
    Me.edTm_magaz.Name = "edTm_magaz"
    Me.edTm_magaz.NTSDbField = ""
    Me.edTm_magaz.NTSFormat = "0"
    Me.edTm_magaz.NTSForzaVisZoom = False
    Me.edTm_magaz.NTSOldValue = ""
    Me.edTm_magaz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_magaz.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_magaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_magaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_magaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_magaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_magaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_magaz.Properties.MaxLength = 65536
    Me.edTm_magaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_magaz.Size = New System.Drawing.Size(52, 20)
    Me.edTm_magaz.TabIndex = 51
    '
    'lbXx_codagen
    '
    Me.lbXx_codagen.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codagen.Location = New System.Drawing.Point(151, 34)
    Me.lbXx_codagen.Name = "lbXx_codagen"
    Me.lbXx_codagen.NTSDbField = ""
    Me.lbXx_codagen.Size = New System.Drawing.Size(207, 20)
    Me.lbXx_codagen.TabIndex = 50
    Me.lbXx_codagen.Tooltip = ""
    Me.lbXx_codagen.UseMnemonic = False
    '
    'lbXx_tipobf
    '
    Me.lbXx_tipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_tipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_tipobf.Location = New System.Drawing.Point(151, 11)
    Me.lbXx_tipobf.Name = "lbXx_tipobf"
    Me.lbXx_tipobf.NTSDbField = ""
    Me.lbXx_tipobf.Size = New System.Drawing.Size(207, 20)
    Me.lbXx_tipobf.TabIndex = 49
    Me.lbXx_tipobf.Tooltip = ""
    Me.lbXx_tipobf.UseMnemonic = False
    '
    'edtm_codagen
    '
    Me.edtm_codagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edtm_codagen.Enabled = False
    Me.edtm_codagen.Location = New System.Drawing.Point(98, 34)
    Me.edtm_codagen.Name = "edtm_codagen"
    Me.edtm_codagen.NTSDbField = ""
    Me.edtm_codagen.NTSFormat = "0"
    Me.edtm_codagen.NTSForzaVisZoom = False
    Me.edtm_codagen.NTSOldValue = ""
    Me.edtm_codagen.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edtm_codagen.Properties.Appearance.Options.UseBackColor = True
    Me.edtm_codagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edtm_codagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edtm_codagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edtm_codagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edtm_codagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edtm_codagen.Properties.MaxLength = 65536
    Me.edtm_codagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edtm_codagen.Size = New System.Drawing.Size(52, 20)
    Me.edtm_codagen.TabIndex = 48
    '
    'edTm_tipobf
    '
    Me.edTm_tipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTm_tipobf.Enabled = False
    Me.edTm_tipobf.Location = New System.Drawing.Point(98, 11)
    Me.edTm_tipobf.Name = "edTm_tipobf"
    Me.edTm_tipobf.NTSDbField = ""
    Me.edTm_tipobf.NTSFormat = "0"
    Me.edTm_tipobf.NTSForzaVisZoom = False
    Me.edTm_tipobf.NTSOldValue = ""
    Me.edTm_tipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTm_tipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edTm_tipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTm_tipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTm_tipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTm_tipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTm_tipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTm_tipobf.Properties.MaxLength = 65536
    Me.edTm_tipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTm_tipobf.Size = New System.Drawing.Size(52, 20)
    Me.edTm_tipobf.TabIndex = 47
    '
    'lbTm_riferim
    '
    Me.lbTm_riferim.AutoSize = True
    Me.lbTm_riferim.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_riferim.Location = New System.Drawing.Point(5, 221)
    Me.lbTm_riferim.Name = "lbTm_riferim"
    Me.lbTm_riferim.NTSDbField = ""
    Me.lbTm_riferim.Size = New System.Drawing.Size(58, 13)
    Me.lbTm_riferim.TabIndex = 4
    Me.lbTm_riferim.Text = "Riferimenti"
    Me.lbTm_riferim.Tooltip = ""
    Me.lbTm_riferim.UseMnemonic = False
    '
    'lbTm_codese
    '
    Me.lbTm_codese.AutoSize = True
    Me.lbTm_codese.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_codese.Location = New System.Drawing.Point(5, 61)
    Me.lbTm_codese.Name = "lbTm_codese"
    Me.lbTm_codese.NTSDbField = ""
    Me.lbTm_codese.Size = New System.Drawing.Size(81, 13)
    Me.lbTm_codese.TabIndex = 3
    Me.lbTm_codese.Text = "Cod. Esenzione"
    Me.lbTm_codese.Tooltip = ""
    Me.lbTm_codese.UseMnemonic = False
    '
    'lbTm_magaz
    '
    Me.lbTm_magaz.AutoSize = True
    Me.lbTm_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_magaz.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_magaz.Location = New System.Drawing.Point(5, 129)
    Me.lbTm_magaz.Name = "lbTm_magaz"
    Me.lbTm_magaz.NTSDbField = ""
    Me.lbTm_magaz.Size = New System.Drawing.Size(66, 13)
    Me.lbTm_magaz.TabIndex = 2
    Me.lbTm_magaz.Text = "Magazzino 1"
    Me.lbTm_magaz.Tooltip = ""
    Me.lbTm_magaz.UseMnemonic = False
    '
    'lbtm_codagen
    '
    Me.lbtm_codagen.AutoSize = True
    Me.lbtm_codagen.BackColor = System.Drawing.Color.Transparent
    Me.lbtm_codagen.Location = New System.Drawing.Point(5, 37)
    Me.lbtm_codagen.Name = "lbtm_codagen"
    Me.lbtm_codagen.NTSDbField = ""
    Me.lbtm_codagen.Size = New System.Drawing.Size(68, 13)
    Me.lbtm_codagen.TabIndex = 1
    Me.lbtm_codagen.Text = "Cod. Agente"
    Me.lbtm_codagen.Tooltip = ""
    Me.lbtm_codagen.UseMnemonic = False
    '
    'lbTm_tipobf
    '
    Me.lbTm_tipobf.AutoSize = True
    Me.lbTm_tipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTm_tipobf.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTm_tipobf.Location = New System.Drawing.Point(5, 14)
    Me.lbTm_tipobf.Name = "lbTm_tipobf"
    Me.lbTm_tipobf.NTSDbField = ""
    Me.lbTm_tipobf.Size = New System.Drawing.Size(78, 13)
    Me.lbTm_tipobf.TabIndex = 0
    Me.lbTm_tipobf.Text = "Tipo bolla/fatt."
    Me.lbTm_tipobf.Tooltip = ""
    Me.lbTm_tipobf.UseMnemonic = False
    '
    'tsFdin
    '
    Me.tsFdin.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsFdin.Location = New System.Drawing.Point(0, 116)
    Me.tsFdin.Name = "tsFdin"
    Me.tsFdin.SelectedTabPage = Me.TabPage1
    Me.tsFdin.Size = New System.Drawing.Size(747, 403)
    Me.tsFdin.TabIndex = 45
    Me.tsFdin.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage1, Me.TabPage2, Me.TabPage3, Me.TabPage4})
    '
    'FRMVEFDIN_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(747, 519)
    Me.Controls.Add(Me.tsFdin)
    Me.Controls.Add(Me.pnTestataTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVEFDIN_F"
    Me.NTSLastControlFocussed = Me.cbTipoDoc
    Me.Text = "FATTURAZIONE DIFFERITA INTERATTIVA"
    CType(Me.pnTestataTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestataTop.ResumeLayout(False)
    CType(Me.pnTestataClforn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestataClforn.ResumeLayout(False)
    Me.pnTestataClforn.PerformLayout()
    CType(Me.edTm_datdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_conto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDocumento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDocumento.ResumeLayout(False)
    CType(Me.edAnnoDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerieDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage4.ResumeLayout(False)
    CType(Me.pnNote, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNote.ResumeLayout(False)
    CType(Me.edTm_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    CType(Me.pnVarie, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnVarie.ResumeLayout(False)
    CType(Me.pnPage4Fill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage4Fill.ResumeLayout(False)
    CType(Me.pnPage4Bottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage4Bottom.ResumeLayout(False)
    CType(Me.fmContabilizzato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmContabilizzato.ResumeLayout(False)
    Me.fmContabilizzato.PerformLayout()
    CType(Me.edTm_nregiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_alfpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_numprot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_datregef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_numregef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_flcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIntrastat, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIntrastat.ResumeLayout(False)
    Me.fmIntrastat.PerformLayout()
    CType(Me.edTm_codport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codrsta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codntra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDDT, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDDT.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grDDT, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDDT, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    CType(Me.pnCmd, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCmd.ResumeLayout(False)
    CType(Me.fmProvvigioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmProvvigioni.ResumeLayout(False)
    Me.fmProvvigioni.PerformLayout()
    CType(Me.ckTm_flprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_impprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPartita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPartita.ResumeLayout(False)
    Me.fmPartita.PerformLayout()
    CType(Me.edTm_datpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_numpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_alfpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_annpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTrasporto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTrasporto.ResumeLayout(False)
    Me.fmTrasporto.PerformLayout()
    CType(Me.edTotQuant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_aspetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_pesonetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_peso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totcoll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_porto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTm_acuradi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_vettor2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_coddest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codaspe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_caustra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSpese, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSpese.ResumeLayout(False)
    Me.fmSpese.PerformLayout()
    CType(Me.edTm_resto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_pagato2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_flscdb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotalev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edResiduo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edResiduov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_abbuono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_pagato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_abbuonov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totomag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speaccv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_pagatov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speimbv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotIva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speincv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_bolliv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotImponib.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totomagv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totmercev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotSpese.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotSpesev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotImponibv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totmerce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_totdocv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotIvav.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_bolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speinc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speimb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_speacc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnTestataDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestataDx.ResumeLayout(False)
    Me.pnTestataDx.PerformLayout()
    CType(Me.edTm_codpaga2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_cambio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_vettor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_valuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scont2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scont1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_datapag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_scopag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codpaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_listino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCa.ResumeLayout(False)
    Me.fmCa.PerformLayout()
    CType(Me.edTm_subcommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codcena.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_commeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmBanca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmBanca.ResumeLayout(False)
    Me.fmBanca.PerformLayout()
    CType(Me.ckTm_autpag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_banc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_banc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_cab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_abi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTestataSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTestataSx.ResumeLayout(False)
    Me.pnTestataSx.PerformLayout()
    CType(Me.edTm_causcap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_causale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_contfatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_magimp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_magaz2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStato.ResumeLayout(False)
    Me.fmStato.PerformLayout()
    CType(Me.ckTm_vistato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_flevas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_flbolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_flspinc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTm_scorpo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_riferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_codese.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_magaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edtm_codagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTm_tipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsFdin, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsFdin.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTipoRk As New DataTable()
      Dim dttTrasporto As New DataTable()
      Dim dttBlocco As New DataTable()
      Dim dttVistato As New DataTable

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"D", "Fattura diff. emessa"})
      dttTipoRk.Rows.Add(New Object() {"K", "Fattura diff. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"P", "Fatt.Ric.Fisc.Differita"})
      dttTipoRk.Rows.Add(New Object() {"£", "Nota accred. diff. emessa"})
      dttTipoRk.Rows.Add(New Object() {"(", "Nota accred. diff. ricevuta"})
      cbTipoDoc.DataSource = dttTipoRk
      cbTipoDoc.ValueMember = "cod"
      cbTipoDoc.DisplayMember = "val"

      dttTrasporto.Columns.Add("cod", GetType(String))
      dttTrasporto.Columns.Add("val", GetType(String))
      dttTrasporto.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttTrasporto.Rows.Add(New Object() {"D", "Destinatario"})
      dttTrasporto.Rows.Add(New Object() {"M", "Mittente"})
      dttTrasporto.Rows.Add(New Object() {"V", "Vettore"})
      cbTm_acuradi.DataSource = dttTrasporto
      cbTm_acuradi.ValueMember = "cod"
      cbTm_acuradi.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim FORMAT_CAMB As String = "#,##0.000000000"

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipoRk1 As New DataTable
      dttTipoRk1.Columns.Add("cod", GetType(String))
      dttTipoRk1.Columns.Add("val", GetType(String))
      dttTipoRk1.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttTipoRk1.Rows.Add(New Object() {"B", "DDT emesso"})
      dttTipoRk1.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipoRk1.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttTipoRk1.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipoRk1.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipoRk1.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttTipoRk1.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttTipoRk1.Rows.Add(New Object() {"M", "DDT ricevuto"})
      dttTipoRk1.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttTipoRk1.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipoRk1.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipoRk1.Rows.Add(New Object() {"U", "Scarico a produzione"})
      dttTipoRk1.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttTipoRk1.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
      dttTipoRk1.AcceptChanges()

      cbTm_acuradi.NTSSetParam(oApp.Tr(Me, 128230023230663757, "Tipo trasporto"))
      cbTipoDoc.NTSSetParam(oApp.Tr(Me, 128230023230819930, "Tipo documento"))

      edTm_cambio.NTSSetParam(oMenu, oApp.Tr(Me, 128230023230976103, "Cambio"), FORMAT_CAMB, 20, 0, 99999999)
      edTm_valuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231132276, "Codice valuta"), tabvalu)
      edTm_scont2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231288449, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      edTm_scont1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231444622, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      edTm_scopag.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231600795, "Sconto pagamento"), oApp.FormatSconti, 6, -100, 100)
      edTm_codpaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231756968, "Pagamento"), tabpaga)
      edTm_listino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231913141, "Numero listino"), tablist)
      edTm_commeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232069314, "Commessa"), tabcommess)
      edTm_subcommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232225487, "Sub commessa"), tabsubcomm, False)
      edTm_codcena.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232537833, "Centro di CA"), tabcena)
      edTm_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232694006, "Codice Ns. Banca"), tabbanc)
      edTm_banc2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023232850179, "Descrizione Filiale"), 50)
      edTm_banc1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023233006352, "Descrizione Banca"), 50)
      edTm_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233162525, "Codice CAB"), tababicab)
      edTm_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233318698, "Codice ABI"), tababi)
      edTm_vettor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233474871, "Codice vettore/spedizioniere"), tabvett)
      edTm_magimp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233631044, "Magazzino Impegni"), tabmaga)
      edTm_magaz2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233787217, "Magazzino 2"), tabmaga)
      ckTm_flevas.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234411909, "Flag Evaso"), "S", "N")
      ckTm_flbolli.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234880428, "Flag Addebito spese per bolli"), "S", "N")
      ckTm_flspinc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235036601, "Flag Addebito spese di incasso"), "S", "N")
      ckTm_scorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235192774, "Flag Scorporo"), "S", "N")
      edTm_riferim.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235348947, "Riferimenti"), 50, True)
      edTm_codese.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235661293, "Esenzione IVA"), tabciva)
      edTm_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235817466, "Magazzino"), tabmaga)
      edtm_codagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235973639, "Agente"), tabcage)
      edTm_tipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023236129812, "Tipo bolla/fattura"), tabtpbf)
      edTm_totprov.NTSSetParam(oMenu, oApp.Tr(Me, 128230023237691542, "Totale provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_impprov.NTSSetParam(oMenu, oApp.Tr(Me, 128230023237847715, "Imponibile provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_datpar.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238003888, "Data partita"), True)
      edTm_numpar.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238160061, "Numero partita"), "#,##0", 9, 0, 999999999)
      edTm_alfpar.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238316234, "Serie partita"), CLN__STD.SerieMaxLen, False)
      edTm_annpar.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238472407, "Anno partita"), "0", 4, 0, 2099)
      edTm_pesonetto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238628580, "Peso netto"), oApp.FormatQta)
      edTm_peso.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238784753, "Peso lordo"), oApp.FormatQta)
      edTm_totcoll.NTSSetParam(oMenu, oApp.Tr(Me, 128230023238940926, "Totale colli"), "#,##0")
      edTm_porto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023239097099, "Porto"), tabport, True)
      edTm_vettor2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023239253272, "Vettore 2"), tabvett)
      edTm_coddest2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023239409445, "Destinazione diversa 2"), tabdestdiv)
      edTm_codaspe.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023239565618, "Codice aspetto esteriore dei beni"), tabaspe)
      edTm_aspetto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023239721791, "Aspetto esteriore dei beni"), 30)
      edTm_caustra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023239877964, "Causale del trasporto"), tabcaum)
      edTotalev.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240034137, "Totale in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotale.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240190310, "Totale"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edResiduov.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240346483, "Residuo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edResiduo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240502656, "Residuo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_abbuonov.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240658829, "Abbuono in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_abbuono.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240815002, "Abbuono"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_pagatov.NTSSetParam(oMenu, oApp.Tr(Me, 128230023240971175, "Incassato in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_pagato.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241127348, "Incassato"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_pagato2.NTSSetParam(oMenu, oApp.Tr(Me, 129587623425439453, "Incassato 2"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_resto.NTSSetParam(oMenu, oApp.Tr(Me, 129587626623144531, "Resto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_totomagv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241283521, "Omaggi in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_totomag.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241439694, "Omaggi"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_totdocv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241595867, "Totale documento in valuta"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_totdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241752040, "Totale documento"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotIvav.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241908213, "Totale IVA in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotIva.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242064386, "Totale IVA"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotImponibv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242220559, "Totale imponibile in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotImponib.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242376732, "Totale imponibile"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotSpesev.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242532905, "Totale spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotSpese.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242689078, "Totale spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_totmercev.NTSSetParam(oMenu, oApp.Tr(Me, 128230023242845251, "Totale netto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_totmerce.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243001424, "Totale netto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_bolliv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243157597, "Bolli in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_bolli.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243313770, "Bolli"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_speincv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243469943, "Spese di incasso in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_speinc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243626116, "Spese di incasso"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_speimbv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243782289, "Spese di imballo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_speimb.NTSSetParam(oMenu, oApp.Tr(Me, 128230023243938462, "Spese di imballo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_speaccv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023244094635, "Spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTm_speacc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023244250808, "Spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTm_note.NTSSetParam(oMenu, oApp.Tr(Me, 128230023244406981, "Note"), 0)
      edTm_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023244563154, "Destinazione diversa"), tabdestdiv)
      edTm_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023244719327, "Codice cliente/fornitore"), tabanagra)
      'per poter inserire piva e/o cod fisc e richiaare hlan passando la piva
      edTm_conto.nMaxLen = 11
      edTm_conto.Properties.MaxLength = 11
      edAnnoDoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023244875500, "Anno documento"), "0", 4, 1900, 2099)
      edSerieDoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023245031673, "Serie documento"), CLN__STD.SerieMaxLen, False)
      edNumDoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023245187846, "Numero documento"), "0", 9, 0, 999999999)
      edTm_datdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023245344019, "Data documento"), False)
      edTm_datapag.NTSSetParam(oMenu, oApp.Tr(Me, 128230023245500192, "Data decorrenza primo pagamento"), False)
      edTm_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023245812538, "Codice contropartita"), tabcove)
      edTm_contfatt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128517034673828000, "Conto fatturazione"), tabanagrac)
      ckTm_autpag.NTSSetParam(oMenu, oApp.Tr(Me, 128653592254843750, "Aut. pag."), "S", "N")
      edTm_causcap.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128653592256875000, "Causale scarico a produzione"), tabcaum)
      edTm_causale.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128653592257343750, "Causale di magazzino"), tabcaum)
      ckTm_flprov.NTSSetParam(oMenu, oApp.Tr(Me, 128653592268906250, "Provvigioni estratte"), "S", "N")
      ckTm_flscdb.NTSSetParam(oMenu, oApp.Tr(Me, 128653592274375000, "Sblocca castelletti"), "S", "N")
      edTm_numprot.NTSSetParam(oMenu, oApp.Tr(Me, 128653592281718750, "Numero protocollo"), "0")
      edTm_alfpro.NTSSetParam(oMenu, oApp.Tr(Me, 129945200901945543, "Serie protocollo"), CLN__STD.SerieMaxLen, True)
      edTm_nregiva.NTSSetParam(oMenu, oApp.Tr(Me, 128653592281875000, "Numereo registro IVA"), "0")
      edTm_datregef.NTSSetParam(oMenu, oApp.Tr(Me, 128653592282343750, "Data registrazione in prima nota"), True)
      edTm_numregef.NTSSetParam(oMenu, oApp.Tr(Me, 128653592282500000, "Numero registrazione in prima nota"), "0")
      ckTm_flcont.NTSSetParam(oMenu, oApp.Tr(Me, 128653592282656250, "Documento contabilizzato"), "S", "N")
      edTm_codrsta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128653592284062500, "Codice regime statistico"), tabrsta)
      edTm_codport.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128653592284687500, "Codice modalità di trasporto"), tabmpor, True)
      edTm_codntra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128653592285000000, "Natura transazione"), tabntra)
      ckTm_vistato.NTSSetParam(oMenu, oApp.Tr(Me, 128715001915468750, "Vistato"), "S", "N")
      edTm_codpaga2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129587613781757812, "Pagamento 2"), tabpaga)

      edNumDoc.NTSForzaVisZoom = True

      grvDDT.Enabled = False
      grvDDT.NTSSetParam(oMenu, oApp.Tr(Me, 128723557193750000, "DDT collegati"))
      tm_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128723560560937500, "Tipo doc"), dttTipoRk1, "val", "cod")
      tm_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560561093750, "Anno doc"), "0", 4, 0, 2099)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128723560561250000, "Serie doc"), CLN__STD.SerieMaxLen, True)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560561406250, "Numero doc"), "0", 9, 0, 999999999)
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128723560561562500, "Data doc"), True)
      tm_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560561718750, "Totale doc"), oApp.FormatImporti, 15)
      tm_valuta.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128723560561875000, "Valuta"), tabvalu)
      tm_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560562031250, "Cambio"), FORMAT_CAMB, 20, 0, 99999999)
      tm_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560562187500, "Anno Par"), "0", 4, 0, 2099)
      tm_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128723560562343750, "Serie par"), CLN__STD.SerieMaxLen, True)
      tm_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128723560562500000, "Numero par"), "0", 9, 0, 999999999)
      tm_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128723560562656250, "Data par"), True)
      tm_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128723560562812500, "Destinaz. 1"), tabdestdiv)
      tm_codagen.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128723560562968750, "Agente"), tabcage)
      edTotQuant.NTSSetParam(oMenu, oApp.Tr(Me, 129606327435107422, "Totale quantità"), oApp.FormatQta)
      grvDDT.NTSAllowDelete = False
      grvDDT.NTSAllowInsert = False
      grvDDT.NTSAllowUpdate = False

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
      Me.Cursor = System.Windows.Forms.Cursors.Default
    Catch ex As Exception
      Me.Cursor = System.Windows.Forms.Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      oCleFdin.bNonValidare = True
      '-------------------------------------------------
      'se i controlli erano gi stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli:
      'ATTENZIONE: devo aver gi preparato ed otttenuto i dati dal database. 
      'se devo rileggere i dati dal database devo rifare il bind!

      ckTm_flevas.NTSText.NTSDbField = "testmag.tm_flevas"
      ckTm_flbolli.NTSText.NTSDbField = "testmag.Tm_flbolli"
      ckTm_flspinc.NTSText.NTSDbField = "testmag.tm_flspinc"
      ckTm_scorpo.NTSText.NTSDbField = "testmag.tm_scorpo"

      edTm_cambio.NTSDbField = "testmag.tm_cambio"
      edTm_valuta.NTSDbField = "testmag.tm_valuta"
      edTm_scont2.NTSDbField = "testmag.tm_scont2"
      edTm_scont1.NTSDbField = "testmag.tm_scont1"
      edTm_scopag.NTSDbField = "testmag.tm_scopag"
      edTm_codpaga.NTSDbField = "testmag.tm_codpaga"
      edTm_codpaga2.NTSDbField = "testmag.tm_codpaga2"
      edTm_datapag.NTSDbField = "testmag.tm_datapag"
      edTm_listino.NTSDbField = "testmag.tm_listino"
      edTm_subcommeca.NTSDbField = "testmag.tm_subcommeca"
      edTm_codcena.NTSDbField = "testmag.tm_codcena"
      edTm_commeca.NTSDbField = "testmag.tm_commeca"
      edTm_codbanc.NTSDbField = "testmag.tm_codbanc"
      edTm_banc2.NTSDbField = "testmag.tm_banc2"
      edTm_banc1.NTSDbField = "testmag.tm_banc1"
      edTm_cab.NTSDbField = "testmag.tm_cab"
      edTm_abi.NTSDbField = "testmag.tm_abi"
      edTm_vettor.NTSDbField = "testmag.tm_vettor"
      edTm_magimp.NTSDbField = "testmag.tm_magimp"
      edTm_magaz2.NTSDbField = "testmag.tm_magaz2"
      edTm_riferim.NTSDbField = "testmag.tm_riferim"
      edTm_codese.NTSDbField = "testmag.tm_codese"
      edTm_magaz.NTSDbField = "testmag.tm_magaz"
      edtm_codagen.NTSDbField = "testmag.tm_codagen"
      edTm_tipobf.NTSDbField = "testmag.tm_tipobf"
      edTm_totprov.NTSDbField = "testmag.tm_totprov"
      edTm_impprov.NTSDbField = "testmag.tm_impprov"
      edTm_datpar.NTSDbField = "testmag.tm_datpar"
      edTm_numpar.NTSDbField = "testmag.tm_numpar"
      edTm_alfpar.NTSDbField = "testmag.tm_alfpar"
      edTm_annpar.NTSDbField = "testmag.tm_annpar"
      edTm_pesonetto.NTSDbField = "testmag.tm_pesonetto"
      edTm_peso.NTSDbField = "testmag.tm_peso"
      edTm_totcoll.NTSDbField = "testmag.tm_totcoll"
      edTm_porto.NTSDbField = "testmag.tm_porto"
      cbTm_acuradi.NTSDbField = "testmag.tm_acuradi"
      edTm_vettor2.NTSDbField = "testmag.tm_vettor2"
      edTm_coddest2.NTSDbField = "testmag.tm_coddest2"
      edTm_codaspe.NTSDbField = "testmag.tm_codaspe"
      edTm_aspetto.NTSDbField = "testmag.tm_aspetto"
      edTm_caustra.NTSDbField = "testmag.tm_caustra"
      'edTotalev.NTSDbField = "testa.******"
      'edTotale.NTSDbField = "testa.******"
      'edResiduov.NTSDbField = "testa.******"
      'edResiduo.NTSDbField = "testa.******"
      edTm_abbuonov.NTSDbField = "testmag.tm_abbuonov"
      edTm_abbuono.NTSDbField = "testmag.tm_abbuono"
      edTm_pagatov.NTSDbField = "testmag.tm_pagatov"
      edTm_pagato.NTSDbField = "testmag.tm_pagato"
      edTm_pagato2.NTSDbField = "testmag.tm_pagato2"
      edTm_resto.NTSDbField = "testmag.tm_resto"
      edTm_totomagv.NTSDbField = "testmag.tm_totomagv"
      edTm_totomag.NTSDbField = "testmag.tm_totomag"
      edTm_totdocv.NTSDbField = "testmag.tm_totdocv"
      edTm_totdoc.NTSDbField = "testmag.tm_totdoc"
      'edTotIvav.NTSDbField = "testa.******"
      'edTotIva.NTSDbField = "testa.******"
      'edTotImponibv.NTSDbField = "testa.******"
      'edTotImponib.NTSDbField = "testa.******"
      'edTotSpesev.NTSDbField = "testa.******"
      'edTotSpese.NTSDbField = "testa.******"
      edTm_totmercev.NTSDbField = "testmag.tm_totmercev"
      edTm_totmerce.NTSDbField = "testmag.tm_totmerce"
      edTm_bolliv.NTSDbField = "testmag.tm_bolliv"
      edTm_bolli.NTSDbField = "testmag.tm_bolli"
      edTm_speincv.NTSDbField = "testmag.tm_speincv"
      edTm_speinc.NTSDbField = "testmag.tm_speinc"
      edTm_speimbv.NTSDbField = "testmag.tm_speimbv"
      edTm_speimb.NTSDbField = "testmag.tm_speimb"
      edTm_speaccv.NTSDbField = "testmag.tm_speaccv"
      edTm_speacc.NTSDbField = "testmag.tm_speacc"
      edTm_note.NTSDbField = "testmag.tm_note"
      edTm_coddest.NTSDbField = "testmag.tm_coddest"
      edTm_conto.NTSDbField = "testmag.tm_conto"
      edTm_datdoc.NTSDbField = "testmag.tm_datdoc"
      edTm_controp.NTSDbField = "testmag.tm_controp"
      'edSconto.NTSDbField = "testa.******"
      'edUltCost.NTSDbField = "testa.******"
      'edPreList.NTSDbField = "testa.******"
      'edDispNetta.NTSDbField = "testa.******"
      'edDispon.NTSDbField = "testa.******"
      ckTm_vistato.NTSText.NTSDbField = "testmag.tm_vistato"

      lbXx_contfatt.NTSDbField = "testmag.xx_contfatt"
      edTm_contfatt.NTSDbField = "testmag.tm_contfatt"
      lbXx_controp.NTSDbField = "testmag.xx_controp"

      '-------------------------------------------------
      'la chiave del documento non la collego: mi serve per poter impostare i dati per creare un nuovo 
      'documento senza bisogno di avere record di testord sottostanti
      'edAnnoDoc.NTSDbField = "testmag.tm_anno"
      'cbTipoDoc.NTSDbField = "testmag.tm_tipork")
      'edSerieDoc.NTSDbField = "testmag.tm_serie"
      'edNumDoc.NTSDbField = "testmag.tm_numdoc"

      '-------------------------------------------------
      lbXx_tipobf.NTSDbField = "testmag.xx_tipobf"
      lbXx_valuta.NTSDbField = "testmag.xx_valuta"
      lbXx_codpaga.NTSDbField = "testmag.xx_codpaga"
      lbXx_codpaga2.NTSDbField = "testmag.xx_codpaga2"
      lbXx_commeca.NTSDbField = "testmag.xx_commeca"
      lbXx_codcena.NTSDbField = "testmag.xx_codcena"
      lbXx_codbanc.NTSDbField = "testmag.xx_codbanc"
      lbXx_vettor.NTSDbField = "testmag.xx_vettor"
      lbXx_magimp.NTSDbField = "testmag.xx_magimp"
      lbXx_magaz2.NTSDbField = "testmag.xx_magaz2"
      lbXx_codese.NTSDbField = "testmag.xx_codese"
      lbXx_magaz.NTSDbField = "testmag.xx_magaz"
      lbXx_codagen.NTSDbField = "testmag.xx_codagen"
      lbXx_porto.NTSDbField = "testmag.xx_porto"
      lbXx_vettor2.NTSDbField = "testmag.xx_vettor2"
      lbXx_coddest2.NTSDbField = "testmag.xx_coddest2"
      lbXx_caustra.NTSDbField = "testmag.xx_caustra"
      lbXx_coddest.NTSDbField = "testmag.xx_coddest"
      lbXx_conto.NTSDbField = "testmag.xx_conto"

      ckTm_autpag.NTSText.NTSDbField = "testmag.tm_autpag"
      lbXx_causcap.NTSDbField = "testmag.xx_causcap"
      edTm_causcap.NTSDbField = "testmag.tm_causcap"
      lbXx_causale.NTSDbField = "testmag.xx_causale"
      edTm_causale.NTSDbField = "testmag.tm_causale"
      ckTm_flbolli.NTSText.NTSDbField = "testmag.Tm_flbolli"
      ckTm_flprov.NTSText.NTSDbField = "testmag.tm_flprov"
      ckTm_flscdb.NTSText.NTSDbField = "testmag.tm_flscdb"
      edTm_numprot.NTSDbField = "testmag.tm_numprot"
      edTm_alfpro.NTSDbField = "testmag.tm_alfpro"
      edTm_nregiva.NTSDbField = "testmag.tm_nregiva"
      edTm_datregef.NTSDbField = "testmag.tm_datregef"
      edTm_numregef.NTSDbField = "testmag.tm_numregef"
      ckTm_flcont.NTSText.NTSDbField = "testmag.tm_flcont"
      lbXx_codrsta.NTSDbField = "testmag.xx_codrsta"
      edTm_codrsta.NTSDbField = "testmag.tm_codrsta"
      lbXx_codport.NTSDbField = "testmag.xx_codport"
      edTm_codport.NTSDbField = "testmag.tm_codport"
      lbXx_codntra.NTSDbField = "testmag.xx_codntra"
      edTm_codntra.NTSDbField = "testmag.tm_codntra"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcFdin, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleFdin.bNonValidare = False
    End Try
  End Sub

#Region "Eventi di FORM"
  Public Overridable Sub FRMVEFDIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bAgenti As Boolean = True
    Dim bNoteprel As Boolean = True
    Try
      Me.Cursor = Cursors.WaitCursor
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not oCleFdin.LeggePevePeac Then
        Me.Close()
        Return
      End If
      If Not oCleFdin.LeggiRegistro Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'leggo dal database i dati
      'NB: se ci saranno dei controlli collegati a datasource, nel datatable sottostante
      '     un record deve esempre esserci, almeno in addnew...
      If Not oCleFdin.ApriDoc(DittaCorrente, True, "B", 0, " ", 0, dsFdin) Then
        Me.Close()
        Return
      End If
      dcFdin.DataSource = dsFdin.Tables("TESTMAG")
      dcFdinDDT.DataSource = dsFdin.Tables("ELENCODDT")
      dsFdin.AcceptChanges()

      '-------------------------------------------------
      'non collego il BindingSource ai vari controlli: lo far solo dopo aver ottenuto il dataset nella nuovo o Apri
      'diversamente da dei messaggi strani sui campi checkbox e radiobutton

      SetStato(0, False)

      GctlTipoDoc = ""
      GctlSetRoules()     'per impostare eventuali blocchi sul tipo documento

      'Se codpaga e scopag sono sbloccati li "apre"
      If oCleFdin.bConsentiModifCodPagaSc Then
        edTm_codpaga.Enabled = True
        edTm_scopag.Enabled = True
      Else
        edTm_codpaga.Enabled = False
        edTm_scopag.Enabled = False
      End If
      If oCleFdin.bConsentiModifFlspinc Then
        ckTm_flspinc.Enabled = True
      Else
        ckTm_flspinc.Enabled = False
      End If

      tlbFlussoDoc.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRMVEFDIN_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim strParam() As String

    Try
      Me.Cursor = Cursors.WaitCursor

      '-------------------------------------------------
      'reimposto l'ultimo tipo documento utilizzato
      'dopo la GCTlsetroules, altrimenti la griglia non viene impostata correttamente ...
      cbTipoDoc.SelectedValue = oMenu.GetSettingBus("BNVEFDIN", "Recent", ".", "LastTipoDoc", " ", " ", "D")
      edAnnoDoc.Text = oMenu.GetSettingBus("BNVEFDIN", "Recent", ".", "LastAnnoDoc", DateTime.Now.Year.ToString, " ", DateTime.Now.Year.ToString)
      edSerieDoc.Text = oMenu.GetSettingBus("BNVEFDIN", "Recent", ".", "LastSerieDoc", " ", " ", " ").PadLeft(1)
      cbTipoDoc_SelectedIndexChanged(cbTipoDoc, Nothing)    'per far applicare la GctlSetRoules in base al tipo documento

      '---------------------------
      'leggo i parametri passati dal child chiamante, se presenti
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          strParam = oCallParams.strParam.Split(";"c)
          Select Case strParam(0)
            Case "NUOD"
              If tlbNuovo.Visible And tlbNuovo.Enabled Then
                'per ora non usato ...
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & strParam(1) & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = strParam(1)
                edAnnoDoc.Text = strParam(2)
                edSerieDoc.Text = strParam(3)
                tlbNuovo_ItemClick(tlbNuovo, Nothing)
                If oCleFdin.dttET.Rows.Count = 0 Then Return
                edTm_conto.NTSTextDB = NTSCInt(strParam(4)).ToString
              End If
            Case Else
              If tlbApri.Visible And tlbApri.Enabled Then
                'es. chiamato da navigazione documentale -> apri documento
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & strParam(1) & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = strParam(1)
                edAnnoDoc.Text = strParam(2)
                edSerieDoc.Text = strParam(3)
                edNumDoc.Text = NTSCInt(strParam(4)).ToString
                tlbApri_ItemClick(tlbApri, Nothing)
              End If
          End Select
        End If
      End If    'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRMVEFDIN_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      '-------------------------------------------------
      'se necessario salvo il documento
      If tsFdin.Visible = True Then
        If tlbSalva.Enabled Then
          If Not Salva(0, True) Then
            e.Cancel = True
            Return
          End If
        Else
          If Not RipristinaDocumento() Then
            e.Cancel = True
            Return
          End If
        End If
      End If

      '-------------------------------------------------
      'salvo l'ultimo tipo documento utilizzato
      If edAnnoDoc.Text <> "1900" Then
        oMenu.SaveSettingBus("BNVEFDIN", "Recent", ".", "LastTipoDoc", cbTipoDoc.SelectedValue.ToString, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNVEFDIN", "Recent", ".", "LastAnnoDoc", edAnnoDoc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNVEFDIN", "Recent", ".", "LastSerieDoc", edSerieDoc.Text, " ", "NS.", "...", "...")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edTm_conto.ContainsFocus Then
        Select Case cbTipoDoc.SelectedValue.ToString
          Case "K", "(" : strTabName = "ANAGRA_FOR"
          Case Else : strTabName = "ANAGRA_CLI"
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

  Public Overridable Sub cbTipoDoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoDoc.SelectedIndexChanged
    Try
      edNumDoc.Text = "0"

      'se devo bloccare la nuovo/apri solo di un determinato tipo documento .... devo farlo qui.
      'solo se non sono nella form_load)
      If edNumDoc.strNomeCampo <> "" Then
        If GctlTipoDoc <> cbTipoDoc.SelectedValue Then
          'devo farlo solo se il tipork è diverso rispetto al contenuto di GctlTipoDoc
          'diversamente se nella configuraz. rendo non visibile una voce di cbTipoDoc
          'entra in un loop infinito e manda in crash Business (es tipork 'O' non visibile per utnete/ditta)
          GctlTipoDoc = cbTipoDoc.SelectedValue
          GctlSetRoules()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edAnnoDoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnnoDoc.ValidatedAndChanged
    Try
      edNumDoc.Text = "0"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAlfdoc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerieDoc.TextChanged
    Try
      If edSerieDoc.Text = "" Then edSerieDoc.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAlfdoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerieDoc.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerieDoc.Text, False)
      If strTmp <> edSerieDoc.Text Then edSerieDoc.Text = strTmp

      If edSerieDoc.Text <> edSerieDoc.Text.ToUpper Then
        edSerieDoc.Text = edSerieDoc.Text.ToUpper
      End If
      edNumDoc.Text = "0"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edNumDoc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edNumDoc.KeyDown
    Try
      If e.KeyCode = Keys.Enter And Val(edNumDoc.Text) > 0 And tlbApri.Enabled = True Then
        tlbApri_ItemClick(Me, Nothing)
        '---------------------------------------------------
        'faccio in modo che la pressione dell'enter non scateni l'emulazione del tasto TAB
        Me.NTSKeyDownEnterNotEmulateTabNow()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edTm_conto_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_conto.ValidatedAndChanged
    Try
      If oCleFdin Is Nothing Then Return

      If NTSCInt(edTm_conto.Text) = 0 Then
        GctlSetVisEnab(edTm_conto, False)
        tsFdin.Enabled = False
      Else
        tsFdin.Enabled = True
        BloccaSbloccaControlli()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edTm_codpaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_codpaga.Validated
    Try
      If oCleFdin Is Nothing Then Return

      If NTSCInt(edTm_codpaga.Text) = 0 Then
        edTm_datapag.Enabled = False
      Else
        If oCleFdin.GetTipoPag(edTm_codpaga.Text) = 4 Then
          GctlSetVisEnab(edTm_datapag, False)
        Else
          edTm_datapag.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTm_flscdb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTm_flscdb.CheckedChanged
    Try
      If ckTm_flscdb.Checked Then
        GctlSetVisEnab(edTm_speinc, False)
        GctlSetVisEnab(edTm_bolli, False)
        GctlSetVisEnab(edTm_totomag, False)
        If NTSCInt(edTm_valuta.Text) <> 0 Then
          GctlSetVisEnab(edTm_speincv, False)
          GctlSetVisEnab(edTm_bolliv, False)
          GctlSetVisEnab(edTm_totomagv, False)
        End If
      Else
        edTm_speinc.Enabled = False
        edTm_bolli.Enabled = False
        edTm_totomag.Enabled = False
        edTm_speincv.Enabled = False
        edTm_bolliv.Enabled = False
        edTm_totomagv.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tsFdin_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsFdin.SelectedPageChanged
    Try
      If oCleFdin.bInUnload Then Return
      Me.Cursor = Cursors.WaitCursor

      If (tsFdin.SelectedTabPageIndex = 2) And (tsFdin.Visible = True) Then
        If NTSCDec(edTotQuant.Text) = 0 Then
          edTotQuant.Text = Format(oCleFdin.CalcolaSommaQtaDTTCollegati(dsFdin.Tables("ELENCODDT")), oApp.FormatQta)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


#Region "Voci di Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim lNumDocTmp As Integer = 0
    Dim strSoloSerie As String = ""
    Dim strTipoProg As String = ""
    Try

      If Not Salva(-1, False) Then Return
      '----------------------------------------------------
      'resetto tutto
      SetStato(0, False)
      edSerieDoc.Text = edSerieDoc.Text.ToUpper
      Me.ValidaLastControl()

      strSoloSerie = oMenu.GetSettingBus("BSVEFDIN", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128733978388277000, "L'utente è abilitato a creare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        edSerieDoc.Text = strSoloSerie.ToUpper
        edNumDoc.Text = "0"
      End If

      '----------------------------------------------------
      'assegno il numero del documento
      lNumDocTmp = NTSCInt(edNumDoc.Text)
      strTipoProg = cbTipoDoc.SelectedValue
      Select Case cbTipoDoc.SelectedValue
        Case "D", "£" : strTipoProg = "A"
        Case "K", "(" : strTipoProg = "L"
        Case "P" : strTipoProg = "S"
      End Select
      lNumDocTmp = oCleFdin.LegNuma(strTipoProg, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text))
      If lNumDocTmp = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115000000, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento"))
        Return
      End If
      If edNumDoc.Text = "0" Then edNumDoc.Text = lNumDocTmp.ToString

      '----------------------------------------------------
      'creo il nuovo record 
      bInLoadDoc = True
      Me.Cursor = Cursors.WaitCursor
      'If cbTipoDoc.SelectedValue.ToString <> oCleFdin.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      oCleFdin.strLastTiporkRegistroDoc = cbTipoDoc.SelectedValue.ToString
      NTSFormClearDataBinding(Me)
      If oCleFdin.NuovoDocumento(DittaCorrente, cbTipoDoc.SelectedValue.ToString, NTSCInt(edAnnoDoc.Text), _
                                 edSerieDoc.Text, NTSCInt(edNumDoc.Text)) = False Then Return
      dsFdin.AcceptChanges()

      dcFdin.DataSource = dsFdin.Tables("TESTMAG")
      Bindcontrols()
      dcFdinDDT.DataSource = dsFdin.Tables("ELENCODDT")
      grDDT.DataSource = dcFdinDDT

      bInLoadDoc = False

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) 'bRicaricaGctl)
      SettaCampiValuta()

      '----------------------------------------------------
      'menu non abilitati quando faccio 'nuovo'
      tlbCancella.Enabled = False

      '----------------------------------------------
      InizProtocollo()

      'di solito si inseriscono documenti nella data odierna ...
      edTm_datdoc.Focus()

      tlbFlussoDoc.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    'Dim bRicaricaGctl As Boolean = False
    Dim strSoloSerie As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try

      If Not Salva(-1, False) Then Return
      '----------------------------------------------------
      'resetto tutto
      SetStato(0, False)
      edSerieDoc.Text = edSerieDoc.Text.ToUpper
      Me.ValidaLastControl()

      strSoloSerie = oMenu.GetSettingBus("BSVEFDIN", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128733978416201000, "L'utente è abilitato a modificare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        Return
      End If

      '----------------------------------------------------
      'testo il numero del documento
      If NTSCInt(edNumDoc.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115312500, "Prima di aprire un nuovo documento è necessario indicare il numero del documento"))
        Return
      End If

      '----------------------------------------------------
      'apro il documento
      Me.Cursor = Cursors.WaitCursor
      bInLoadDoc = True
      'If cbTipoDoc.SelectedValue.ToString <> oCleFdin.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      oCleFdin.strLastTiporkRegistroDoc = cbTipoDoc.SelectedValue.ToString
      NTSFormClearDataBinding(Me)
      If oCleFdin.ApriDoc(DittaCorrente, False, cbTipoDoc.SelectedValue.ToString, _
                                 NTSCInt(edAnnoDoc.Text), edSerieDoc.Text, NTSCInt(edNumDoc.Text), _
                                 dsFdin) = False Then Return
      dsFdin.AcceptChanges()
      If dsFdin.Tables("TESTMAG").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115468750, "Non è stato trovato nessun documento con le caratteristiche richieste"))
        bInLoadDoc = False
        Return
      Else
        dcFdin.DataSource = dsFdin.Tables("TESTMAG")
        Bindcontrols()
        dcFdinDDT.DataSource = dsFdin.Tables("ELENCODDT")
        dcFdin.MoveFirst()
        grDDT.DataSource = dcFdinDDT
      End If

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) ' bRicaricaGctl)
      SettaCampiValuta()

      If oCleFdin.bDocNonModificabile = True Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
      End If

      bInLoadDoc = False

      '----------------------------------------------------
      'ricalcolo i totali del documento (colonne unbound)
      oCleFdin.CalcolaTotali(False)

      oCleFdin.bHasChanges = False


      'se il flag di 'fattura da rielaborare' è settato imposto che il documento è cambiato,
      'diversamente se apro e stampo solo non rielabora ...
      If oCleFdin.dttET.Rows.Count > 0 Then
        If NTSCStr(oCleFdin.dttET.Rows(0)!tm_flagiva_1).ToUpper = "S" Then oCleFdin.bHasChanges = True
      End If

      tlbFlussoDoc.Enabled = True

    Catch ex As Exception
      bInLoadDoc = False
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(-1, False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strMsg As String = ""
    Try

      If OperazioneConsentita() = False Then Return

      '-------------------------------------------------
      'cancello il documento
      strMsg = oApp.Tr(Me, 127791958837031250, "Cancellare il documento?") & vbCrLf
      'se è spuntato il flag di anaditac di lanciare pnfa al salva dei documenti, 
      'devo avvisare che in caso di cancellazione l'aggiornamento delle provvigioni dovrà essere fatto manualmente, 
      'altrimenti ci si può aspettare che come l'archivio viene aggiornato un update di un doc, 
      'venga allineato anche nelle delete
      If oCleFdin.ChiamaPnfa() Then
        If ckTm_flprov.Checked Then
          strMsg += vbCrLf & oApp.Tr(Me, 130415988402832301, "Dovrà essere rieseguita l'estrazione delle provvigioni per rimuovere dall'archivio i dati di questo documento")
        End If
      End If

      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(strMsg)

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleFdin.bInUnload = True
          Me.Cursor = Cursors.WaitCursor

          If Not oCleFdin.SalvaDocumento("D") Then Return
          oCleFdin.bInUnload = False
          SetStato(0, False)
          edNumDoc.Text = "0"
          edNumDoc.SelectAll()
          Return
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleFdin.bInUnload = False
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      RipristinaDocumento()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    'SendKeys.SendWait("{F5}")
    'se faccio la riga sopra va in un loop infinito....
    'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------
      'zoom specifico per le destinazioni diverse (devo passare il conto cliente/fornitore)
      If edTm_conto.Focused Then
        SetFastZoom(edTm_conto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edTm_conto.Text
        oParam.bVisGriglia = True
        oParam.bTipoProposto = True
        oParam.nMastro = 0
        If oCleFdin.IsDocumentoEmesso(dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString()) Then
          oParam.strTipo = "C"
        Else
          oParam.strTipo = "F"
        End If
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTm_conto.Text Then edTm_conto.NTSTextDB = NTSZOOM.strIn

      ElseIf edTm_coddest.Focused Or edTm_coddest2.Focused Then

        If edTm_coddest.Focused Then
          SetFastZoom(edTm_coddest.Text, oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = edTm_coddest.Text
        Else
          SetFastZoom(edTm_coddest2.Text, oParam)
          NTSZOOM.strIn = edTm_coddest2.Text
        End If

        oParam.lContoCF = oCleFdin.lContoCF   'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)

        If edTm_coddest.Focused Then
          If NTSZOOM.strIn <> edTm_coddest.Text Then edTm_coddest.NTSTextDB = NTSZOOM.strIn
        Else
          If NTSZOOM.strIn <> edTm_coddest2.Text Then edTm_coddest2.NTSTextDB = NTSZOOM.strIn
        End If

      ElseIf edNumDoc.Focused Or edSerieDoc.Focused Or edSerieDoc.Focused Or cbTipoDoc.Focused Then
        '------------------------------------
        'zoom documenti 
        If tsFdin.Visible = True Then Return 'esco se sono dentro ad un documento

        oParam.nTipologia = 1   '= bsveboll
        oParam.strTipo = cbTipoDoc.SelectedValue.ToString
        oParam.strAlfpar = edSerieDoc.Text
        oParam.nAnno = NTSCInt(edAnnoDoc.Text)
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMDOCUMENTI", DittaCorrente, oParam)
        If NTSZOOM.strIn = "" Then Return
        edSerieDoc.Text = oParam.strAlfpar
        edNumDoc.Text = oParam.lNumpar.ToString
        tlbApri_ItemClick(Me, Nothing)

      ElseIf edTm_cab.Focused Then
        '------------------------------------
        'zoom cab
        SetFastZoom(edTm_cab.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = edTm_abi.Text   'passo il codice abi
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTm_cab.Text Then edTm_cab.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard
        ctrlTmp = NTSFindControlForZoom()
        If ctrlTmp Is Nothing Then Return

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

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleFdin.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978506595000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        oCleFdin.bHasChanges = True
        If Not Salva(-1, False) Then Return
      End If

      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleFdin.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978564159000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        oCleFdin.bHasChanges = True
        If Not Salva(-1, False) Then Return
      End If

      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaPdf_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaPdf.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryCrw32FileMultipli As String = ""    'query per la stampa per crw32 se un file per ogni documento
    Dim strQueryCrw32FileUnico As String = ""       'query per la stampa per crw32 se un file unico
    Dim strQueryGetDocMultipli As String = ""       'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file per documento)
    Dim strQueryGetDocUnico As String = ""          'query che bepdgenp dovrà  eseguire per ottenere il datatable con l'elenco dei documenti da generare (un file unico)
    Dim dttFormule As New DataTable                 'contiene le formule fisse da passare a crystal report/pdf dal chiamante
    Dim strNumcliPVR As String = ""
    Dim i As Integer = 0

    '------------------------------
    'PER UN ESEMPIO COMPLETO VEDI BNORCONF: questo programma può stampare solo con un PDF per file, non un pdf per tutti i documenti
    '------------------------------

    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleFdin.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978583035000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        oCleFdin.bHasChanges = True
        If Not Salva(-1, False) Then Return
      End If

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleFdin.GetQueryStampaPdf
      strQueryGetDocUnico = strQueryGetDocMultipli
      If strQueryGetDocMultipli = "" Or strQueryGetDocUnico = "" Then Return

      '--------------------------------
      'se devo passare delle formule lo faccio tramite questo datatable (per la 'PeSetFormula'
      'devo compilare o num, o str, o data a seconda del tipo di dato. 'name' deve sempre essere impostata
      dttFormule.Columns.Add("name", GetType(String))
      dttFormule.Columns.Add("num", GetType(Decimal))
      dttFormule.Columns.Add("str", GetType(String))
      dttFormule.Columns.Add("data", GetType(DateTime))
      dttFormule.Columns("name").DefaultValue = Nothing
      dttFormule.Columns("num").DefaultValue = Nothing
      dttFormule.Columns("str").DefaultValue = Nothing
      dttFormule.Columns("data").DefaultValue = Nothing
      dttFormule.AcceptChanges()

      If dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString = "D" Then
        '--------------------------------------------------
        strNumcliPVR = ""
        If oCleFdin.bGestPVR = True Then
          strNumcliPVR = oMenu.GetSettingBus("OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
          For i = 1 To (6 - Len(strNumcliPVR))
            strNumcliPVR = "0" & strNumcliPVR
          Next
          'per un fattore di velocità non testo la tabella valuta:
          'se sono in valuta presuppongo che sia l'unica ammessa
          If NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_valuta) = 0 Then
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
          Else
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
          End If
          strNumcliPVR = strNumcliPVR & CLN__STD.calcolaPVRCheck(strNumcliPVR)
        End If

        dttFormule.Rows.Add(New Object() {"NUMCLIPVR", Nothing, strNumcliPVR, Nothing})
      End If


      'parte aggiuntiva diPeSetSelectionFormula (i marcatori verranno sostituiti con i campi di 'strQueryGetGroupBy'
      strQueryCrw32FileMultipli = "{testmag.codditt} = '" & DittaCorrente & "'" & _
                                 " AND {MOVMAG.mm_stasino}<> 'N' AND {movmag.mm_stasino} <> 'B'" & _
                                 " AND {testmag.tm_anno} = |anno|" & _
                                 " AND {testmag.tm_numdoc} = |numero|" & _
                                 " AND {testmag.tm_serie} = |serie|" & _
                                 " AND {testmag.tm_tipork} = |tipork|"
      If oCleFdin.bUsaKeyMag Then strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"
      strQueryCrw32FileUnico = strQueryCrw32FileMultipli

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BSVEFDIN"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico
      oPar.strParam = "Stampa Fattura differita"
      oPar.ctlPar1 = Me
      oPar.ctlPar2 = dttFormule
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

      oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)

      '-------------------------------
      'da bnpdgenp è stato scelto di stampare anche su carta
      If oPar.bPar4 Then
        Stampa(1)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      If tlbSalva.Enabled Then
        If Not Salva(-1, True) Then Return
      Else
        If Not RipristinaDocumento() Then Return
      End If
      SetStato(0, False)
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbFlussoDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFlussoDoc.ItemClick
    Dim strParam As String = ""
    Try
      strParam = "APRI;" & NTSCStr(cbTipoDoc.SelectedValue).PadLeft(1, " "c) & ";" & NTSCStr(edAnnoDoc.Text).PadLeft(4, "0"c) & ";" & _
                 NTSCStr(edSerieDoc.Text).PadLeft(1, " "c) & ";" & NTSCStr(edNumDoc.Text).PadLeft(9, "0"c) & ";" & _
                 NTSCStr(edTm_conto.Text).PadLeft(9, "0"c) & ";" & NTSCStr(edTm_datdoc.Text).PadLeft(10, " "c) & _
                 ";000000000;0000;0000; ;000000000;0000;4"

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 129451786176573691, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "ALT+F2 e ALT+F3 rimappati"
  Public Overridable Sub edTm_conto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edTm_conto.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If oCleFdin.dttEt_conto.Rows.Count = 0 Then Return
        bNuovo = False
      End If

      oTmp.Text = edTm_conto.Text
      If oCleFdin.dttEt_conto.Rows.Count = 0 Then
        If oCleFdin.IsDocumentoEmesso(cbTipoDoc.SelectedValue) Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      Else
        If oCleFdin.dttEt_conto.Rows(0)!an_tipo.ToString = "C" Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      End If

      edTm_conto.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTm_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edTm_coddest.NTSZoomGest
    e.ZoomHandled = True        'non faccio eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub

  Public Overridable Sub edTm_coddest2_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edTm_coddest2.NTSZoomGest
    e.ZoomHandled = True        'non faccio eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub
#End Region

#Region "CommandButton"
  Public Overridable Sub cmdModifScad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModifScad.Click
    Try
      ApriScadenze()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCastelletti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCastelletti.Click
    Dim dttIva As New DataTable
    Dim dttControp As New DataTable
    Dim i As Integer = 0
    Dim frmCast As FRMVECAS1 = Nothing
    Dim oTestata As New CLELBMENU.OutTestata

    Try
      '----------------------
      'aggiorno i totali
      oCleFdin.CalcolaTotali(True, oTestata)

      '----------------------
      'preparo i datatable
      dttIva.TableName = "IVA"
      dttIva.Columns.Add("xx_idiva", GetType(Integer))
      dttIva.Columns.Add("xx_codiva", GetType(Integer))
      dttIva.Columns.Add("xx_imponib", GetType(Decimal))
      dttIva.Columns.Add("xx_imposta", GetType(Decimal))
      dttIva.Columns.Add("xx_imponibv", GetType(Decimal))
      dttIva.Columns.Add("xx_impostav", GetType(Decimal))

      dttControp.TableName = "CONTROP"
      dttControp.Columns.Add("xx_id", GetType(Integer))
      dttControp.Columns.Add("xx_ccontr", GetType(Integer))
      dttControp.Columns.Add("xx_impcont", GetType(Decimal))
      dttControp.Columns.Add("xx_impcontv", GetType(Decimal))

      '----------------------
      'copio i dati
      For i = 1 To 8
        dttIva.Rows.Add(New Object() {i, _
                                      oCleFdin.dttET.Rows(0)("tm_codiva_" & i.ToString), _
                                      oCleFdin.dttET.Rows(0)("tm_imponib_" & i.ToString), _
                                      oCleFdin.dttET.Rows(0)("tm_imposta_" & i.ToString), _
                                      oCleFdin.dttET.Rows(0)("tm_imponibv_" & i.ToString), _
                                      oCleFdin.dttET.Rows(0)("tm_impostav_" & i.ToString)})
      Next
      For i = 1 To 20
        dttControp.Rows.Add(New Object() {i, _
                                      oCleFdin.dttETC.Rows(0)("tm_ccontr_" & i.ToString), _
                                      oCleFdin.dttETC.Rows(0)("tm_impcont_" & i.ToString), _
                                      oCleFdin.dttETC.Rows(0)("tm_impcontv_" & i.ToString)})
      Next
      dttIva.AcceptChanges()
      dttControp.AcceptChanges()

      '----------------------
      'visualizzo il child
      frmCast = CType(NTSNewFormModal("FRMVECAS1"), FRMVECAS1)
      frmCast.Init(oMenu, Nothing)
      frmCast.InitEntity(oCleFdin)
      frmCast.ds.Tables.Add(dttIva)
      frmCast.ds.Tables.Add(dttControp)
      frmCast.bBloccaGriglie = Not ckTm_flscdb.Checked
      frmCast.dDiffCorpoRighe = ArrDbl(oTestata.dTotimpocontr - oTestata.dTotimporighe, oMenu.TrovaNdec(0)) ' forse il confornto nonnva fatto con totimpocontr..

      frmCast.ShowDialog()
      oCleFdin.bHasChanges = True

      '----------------------
      'riprendo i dati
      oCleFdin.dttET.Rows(0)!tm_dtcomiva = NTSCDate(frmCast.edDtcomiva.Text)
      For i = 1 To 8
        oCleFdin.dttET.Rows(0)("tm_codiva_" & i.ToString) = dttIva.Rows(i - 1)!xx_codiva
        oCleFdin.dttET.Rows(0)("tm_imponib_" & i.ToString) = dttIva.Rows(i - 1)!xx_imponib
        oCleFdin.dttET.Rows(0)("tm_imposta_" & i.ToString) = dttIva.Rows(i - 1)!xx_imposta
        oCleFdin.dttET.Rows(0)("tm_imponibv_" & i.ToString) = dttIva.Rows(i - 1)!xx_imponibv
        oCleFdin.dttET.Rows(0)("tm_impostav_" & i.ToString) = dttIva.Rows(i - 1)!xx_impostav
      Next
      For i = 1 To 20
        oCleFdin.dttETC.Rows(0)("tm_ccontr_" & i.ToString) = dttControp.Rows(i - 1)!xx_ccontr
        oCleFdin.dttETC.Rows(0)("tm_impcont_" & i.ToString) = dttControp.Rows(i - 1)!xx_impcont
        oCleFdin.dttETC.Rows(0)("tm_impcontv_" & i.ToString) = dttControp.Rows(i - 1)!xx_impcontv
      Next

      '----------------------
      'aggiorno i totali
      oCleFdin.CalcolaTotali(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmCast Is Nothing Then frmCast.Dispose()
      frmCast = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdDDTAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDDTAdd.Click
    Dim oPar As New CLE__PATB
    Dim dttTmp As New DataTable

    Try
      '------------------------
      'Chiama lo Zoom sulle testate
      oPar.strDescr = oCleFdin.GetWhereHltmDDT()
      oPar.nTipologia = 1       '0 = posso selez una sola riga, 1 posso fare multiselezione, 2 = acconti
      oPar.strTipo = ""

      NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor
      dttTmp = CType(oPar.oParam, DataTable)
      oCleFdin.DDTAggiungi(dttTmp)

      SettaCampiValuta()

      BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub cmdDDTMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDDTMod.Click
    Dim strParam As String = ""
    Dim dttTmp As New DataTable
    Try
      If dsFdin.Tables("ELENCODDT").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978719105000, "La fattura non ha alcun documento collegato"))
        Return
      End If
      If grvDDT.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726142877343750, "Posizionarsi prima sulla riga di griglia contenente il documento da rimuovere dalla fattura"))
        Return
      End If

      'apro bnveboll, quindi scollego e, se il documento è ancora presente, lo ricollego
      ApriDoc(grvDDT.NTSGetCurrentDataRow!tm_tipork.ToString, NTSCInt(grvDDT.NTSGetCurrentDataRow!tm_anno), grvDDT.NTSGetCurrentDataRow!tm_serie.ToString, NTSCInt(grvDDT.NTSGetCurrentDataRow!tm_numdoc))

      Me.Cursor = Cursors.WaitCursor
      dttTmp.Columns.Add("tm_tipork", GetType(String))
      dttTmp.Columns.Add("tm_anno", GetType(String))
      dttTmp.Columns.Add("tm_serie", GetType(String))
      dttTmp.Columns.Add("tm_numdoc", GetType(String))
      dttTmp.Rows.Add(dttTmp.NewRow())
      dttTmp.Rows(0)!tm_tipork = grvDDT.NTSGetCurrentDataRow!tm_tipork
      dttTmp.Rows(0)!tm_anno = grvDDT.NTSGetCurrentDataRow!tm_anno
      dttTmp.Rows(0)!tm_serie = grvDDT.NTSGetCurrentDataRow!tm_serie
      dttTmp.Rows(0)!tm_numdoc = grvDDT.NTSGetCurrentDataRow!tm_numdoc

      Me.Cursor = Cursors.WaitCursor
      oCleFdin.DDTScollega(grvDDT.NTSGetCurrentDataRow, False)
      Me.Cursor = Cursors.WaitCursor
      oCleFdin.bDaModificaDocumento = True
      oCleFdin.DDTAggiungi(dttTmp)
      oCleFdin.bDaModificaDocumento = False

      BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
      Me.Cursor = Cursors.Default
      oCleFdin.bDaModificaDocumento = False
    End Try
  End Sub
  Public Overridable Sub cmdDDTDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDDTDel.Click
    Try
      If dsFdin.Tables("ELENCODDT").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978696017000, "La fattura non ha alcun documento collegato"))
        Return
      End If
      If grvDDT.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733978838331000, "Posizionarsi prima sulla riga di griglia contenente il documento da rimuovere dalla fattura"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor
      oCleFdin.DDTScollega(grvDDT.NTSGetCurrentDataRow, True)

      BloccaSbloccaControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region


  Public Overridable Function ApriDoc(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                      ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strParam As String = ""
    Try
      strParam = "APRI;" & strTipoDoc & ";" & nAnno.ToString.PadLeft(4, "0"c) & ";" & strSerie & ";" & lNumdoc.ToString.PadLeft(9, "0"c) & ";"

      If oCleFdin.bORTO_ChiamaBsjoboll Then
        oMenu.RunChild("BSJOBOLL", "CLSJOBOLL", oApp.Tr(Me, 128733978949317000, "Gestione Documenti orto"), DittaCorrente, "", "", Nothing, strParam, True, True)
      Else
        If oCleFdin.IsDocRetail(strTipoDoc, nAnno, strSerie, lNumdoc) Then
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 130021109295484106, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
        ElseIf oCleFdin.IsDocRetailNew(strTipoDoc, nAnno, strSerie, lNumdoc) Then
          oMenu.RunChild("BSREGSRE", "CLSREGSRE", oApp.Tr(Me, 129588203437744141, "Gestione retail"), DittaCorrente, "", "", Nothing, strParam, True, True)
        Else
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128726153464687500, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub edTm_note_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_note.Enter
    Try
      Me.NTSDisableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edTm_note_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_note.Leave
    Try
      Me.NTSEnableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  '------------------------------

  Public Overridable Sub SetStato(ByVal nStato As Integer, ByVal bRicaricaGctlInStato1 As Boolean)
    '0 = nessun documento aperto
    '1 = sono dentro ad un documento, nuovo o in modifica
    Dim i As Integer = 0
    Dim strSerieOld As String = ""
    Dim strTipoProg As String = ""
    Try
      Select Case nStato
        Case 0
          edTm_numprot.Enabled = False
          edTm_alfpro.Enabled = False
          edTm_nregiva.Enabled = False

          TabPage1.Enable = True
          TabPage3.Enable = True
          TabPage4.Enable = True
          tsFdin.SelectedTabPageIndex = 0
          tsFdin.Visible = False
          pnTestataClforn.Visible = False

          GctlSetVisEnab(cbTipoDoc, True)
          GctlSetVisEnab(cbTipoDoc, False)
          GctlSetVisEnab(edAnnoDoc, False)
          GctlSetVisEnab(edSerieDoc, False)
          GctlSetVisEnab(edNumDoc, False)

          If edAnnoDoc.Text = "" Then edAnnoDoc.Text = DateTime.Now.Year.ToString
          If edSerieDoc.Text = "" Then edSerieDoc.Text = " "
          If edNumDoc.Text = "" Then edNumDoc.Text = "0"

          GctlSetVisEnab(tlbNuovo, False)
          GctlSetVisEnab(tlbApri, False)
          tlbSalva.Enabled = False
          tlbCancella.Enabled = False
          tlbRipristina.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tlbStampaPdf.Enabled = False
          ckTm_flscdb.Enabled = False
          tlbFlussoDoc.Enabled = False

          GctlSetVisEnab(edTm_note, False)

          SetUnboundFiled(0)
          edNumDoc.SelectAll()
          edNumDoc.Focus()

        Case 1
          pnTestataClforn.Visible = True
          tsFdin.Visible = True

          cbTipoDoc.Enabled = False
          edAnnoDoc.Enabled = False
          edSerieDoc.Enabled = False
          edNumDoc.Enabled = False 'devo poter dare la possibilità di cambiare numero doc quando faccio 'nuovo': no: crea problemi con impegni collegati ... usare la funzione 'cambia numero doc'

          '-------------------------------
          'applico le impostazioni da GCTL ed i valori di default: 
          'la griglia deve gi essere collegata al datatable
          GctlTipoDoc = cbTipoDoc.SelectedValue().ToString
          If bRicaricaGctlInStato1 Then GctlSetRoules()
          If oCleFdin.bNew Then
            'se ho impostato tramite GCTL che in nuovo la serie deve essere una specifica, devo ricalcolare il numero documento
            strSerieOld = edSerieDoc.Text
            GctlApplicaDefaultValue()
            If strSerieOld <> edSerieDoc.Text Then
              strTipoProg = cbTipoDoc.SelectedValue
              Select Case cbTipoDoc.SelectedValue
                Case "D", "£" : strTipoProg = "A"
                Case "K", "(" : strTipoProg = "L"
                Case "P" : strTipoProg = "S"
              End Select
              edNumDoc.Text = oCleFdin.LegNuma(strTipoProg, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text)).ToString
              oCleFdin.CambiaNumdoc(dsFdin, NTSCInt(edNumDoc.Text))
            End If
          End If

          tlbNuovo.Enabled = False
          tlbApri.Enabled = False
          GctlSetVisEnab(tlbSalva, False)
          GctlSetVisEnab(tlbCancella, False)
          GctlSetVisEnab(tlbRipristina, False)

          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          GctlSetVisEnab(tlbStampaPdf, False)

          'Protocollo automatico
          If ckTm_flcont.Checked = False And oCleFdin.strAutoNumProt = "S" And oCleFdin.dttET.Rows(0)!tm_tipork.ToString = "K" Then
            GctlSetVisEnab(edTm_numprot, False)
            GctlSetVisEnab(edTm_alfpro, False)
            GctlSetVisEnab(edTm_nregiva, False)
          End If

          'sblocco castelletti
          If oCleFdin.dttET.Rows(0)!tm_tipork.ToString = "K" Or oCleFdin.dttET.Rows(0)!tm_tipork.ToString = "(" Then
            GctlSetVisEnab(ckTm_flscdb, False)
          End If
          ckTm_flscdb_CheckedChanged(ckTm_flscdb, Nothing)

          If oCleFdin.bNew Then
            tlbCancella.Enabled = False
          Else
            GctlSetVisEnab(tlbCancella, False)
          End If

          If oCleFdin.IsDocumentoEmesso(cbTipoDoc.SelectedValue) Then
            If oCleFdin.bRifBolleInNote Then
              edTm_note.Enabled = False
            End If
          End If

          '----------------------------
          'blocco tutto fino a quando non inserisco il conto CF
          If NTSCInt(edTm_conto.Text) = 0 Then
            tsFdin.Enabled = False
            GctlSetVisEnab(edTm_conto, False)
          Else
            tsFdin.Enabled = True
            edTm_conto.Enabled = False
          End If

      End Select

      BloccaSbloccaControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SettaCampiValuta()
    Try
      If edTm_valuta.Text <> "0" Then
        GctlSetVisEnab(lbValuta, True)
        GctlSetVisEnab(edTm_speaccv, True)
        GctlSetVisEnab(edTm_speimbv, True)
        GctlSetVisEnab(edTm_speincv, True)
        GctlSetVisEnab(edTm_bolliv, True)
        GctlSetVisEnab(edTm_totmercev, True)
        GctlSetVisEnab(edTotSpesev, True)
        GctlSetVisEnab(edTotImponibv, True)
        GctlSetVisEnab(edTotIvav, True)
        GctlSetVisEnab(edTm_totdocv, True)
        GctlSetVisEnab(edTm_totomagv, True)
        GctlSetVisEnab(edTotalev, True)
        GctlSetVisEnab(edTm_pagatov, True)
        GctlSetVisEnab(edTm_abbuonov, True)
        GctlSetVisEnab(edResiduov, True)
      Else
        lbValuta.Visible = False
        edTm_speaccv.Visible = False
        edTm_speimbv.Visible = False
        edTm_speincv.Visible = False
        edTm_bolliv.Visible = False
        edTm_totmercev.Visible = False
        edTotSpesev.Visible = False
        edTotImponibv.Visible = False
        edTotIvav.Visible = False
        edTm_totdocv.Visible = False
        edTm_totomagv.Visible = False
        edTotalev.Visible = False
        edTm_pagatov.Visible = False
        edTm_abbuonov.Visible = False
        edResiduov.Visible = False
      End If

      'forzo la routine 'sblocca castelletti' altrimenti se la valuta era 0, spunto sblocca castelletti poi cambio valuta i campi in valuta rimangono bloccati
      ckTm_flscdb_CheckedChanged(ckTm_flscdb, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ApriScadenze() As Boolean
    Dim i As Integer = 0
    Dim frmDtsc As FRMVEDTS1 = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not oCleFdin.ApriScadenze Then Return False
      Me.Cursor = Cursors.Default

      '-----------------------------
      'visualizzo la form
      'se sono qui ho sicuramente le scadenze calcolate (anche su nuovo docuento ancora non salvato)
      frmDtsc = CType(NTSNewFormModal("FRMVEDTS1"), FRMVEDTS1)
      frmDtsc.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmDtsc.oCleFdin = oCleFdin
      frmDtsc.ShowDialog()

      'ho selezionato annulla
      If frmDtsc.bOk = False Then Return False

      For i = 1 To 8
        oCleFdin.dttET.Rows(0)("tm_tippaga_" & i.ToString) = NTSCInt(CType(frmDtsc.pnSpese.Controls("cbTm_tippaga_" & i.ToString), NTSComboBox).SelectedValue)
        If CType(frmDtsc.pnSpese.Controls("edTm_datsca_" & i.ToString), NTSTextBoxData).Text = "" Then
          oCleFdin.dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
        Else
          oCleFdin.dttET.Rows(0)("tm_datsca_" & i.ToString) = CType(frmDtsc.pnSpese.Controls("edTm_datsca_" & i.ToString), NTSTextBoxData).Text
        End If
        oCleFdin.dttET.Rows(0)("tm_impsca_" & i.ToString) = NTSCDec(CType(frmDtsc.pnSpese.Controls("edTm_impsca_" & i.ToString), NTSTextBoxNum).Text)
        oCleFdin.dttET.Rows(0)("tm_impscav_" & i.ToString) = NTSCDec(CType(frmDtsc.pnSpese.Controls("edTm_impscav_" & i.ToString), NTSTextBoxNum).Text)
      Next

      oCleFdin.bModifCastScad = True

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      If Not frmDtsc Is Nothing Then frmDtsc.Dispose()
      frmDtsc = Nothing
    End Try
  End Function

  Public Overridable Function InizProtocollo() As Boolean
    Try
      If oCleFdin.strAutoNumProt = "S" And _
         (oCleFdin.dttET.Rows(0)!tm_tipork.ToString = "K" Or oCleFdin.dttET.Rows(0)!tm_tipork.ToString = "(") Then
        GctlSetVisEnab(edTm_numprot, False)
        GctlSetVisEnab(edTm_alfpro, False)
        GctlSetVisEnab(edTm_nregiva, False)
      Else
        edTm_numprot.Enabled = False
        edTm_alfpro.Enabled = False
        edTm_nregiva.Enabled = False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub SetUnboundFiled(ByVal nStato As Integer)
    '0= reset per nuovo / reset per chisura documento
    '1= compilo i campi con il documento aperto
    Try
      If nStato = 0 Then
        edTotSpese.NTSTextDB = "0"
        edTotSpesev.NTSTextDB = "0"
        edTotImponib.NTSTextDB = "0"
        edTotImponibv.NTSTextDB = "0"
        edTotIva.NTSTextDB = "0"
        edTotIvav.NTSTextDB = "0"
        edTotale.NTSTextDB = "0"
        edTotalev.NTSTextDB = "0"
        edResiduo.NTSTextDB = "0"
        edResiduov.NTSTextDB = "0"
        edTotQuant.NTSTextDB = "0"
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Ripristina() As Boolean
    Try
      oCleFdin.bInUnload = True

      If Not oCleFdin.RipristinaDocumento() Then Return False

      GctlSaveConfigGrid()
      SetStato(0, False)
      oCleFdin.bInUnload = False
      edNumDoc.Text = "0"
      edNumDoc.SelectAll()
      tlbFlussoDoc.Enabled = False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oCleFdin.bInUnload = False
    End Try
  End Function

  Public Overridable Function Salva(ByVal nStampa As Integer, ByVal bAllowCancel As Boolean) As Boolean
    'nStampa = -1: salva solo
    '           0: stampa video
    '           1: stampa carta
    Dim dRes As DialogResult
    Dim strState As String = "U"

    Try
      If edTm_conto.Visible = False Then Return True
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      Me.Cursor = Cursors.WaitCursor
      'If Not oCleFdin.RecordSalva(dcFdinRighe.Position, False, Nothing) Then Return False
      Me.Cursor = Cursors.Default
      If oCleFdin.bNew Then strState = "N"
      If oCleFdin.RecordIsChanged Then

        If OperazioneConsentita() = False Then Return False

        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        If bAllowCancel Then
          dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128733979059033000, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
          If dRes = System.Windows.Forms.DialogResult.No Then Return True
        Else
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791958202656250, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.No Then Return False
        End If

        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Me.Cursor = Cursors.WaitCursor

          If Not oCleFdin.SalvaDocumento(strState) Then
            'potrebbe non aver salvato perchè il numero doc è già stato utilizzato ...
            edNumDoc.Text = NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_numdoc).ToString
            Return False
          End If
          GctlSaveConfigGrid()

          SetStato(0, False)
          edNumDoc.Text = "0"
          edNumDoc.SelectAll()
        End If
      Else
        'ho aperto un documento ed ora lo stampo senza modficarlo
        GctlSaveConfigGrid()
        SetStato(0, False)
        edNumDoc.Text = "0"
        edNumDoc.SelectAll()
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function RipristinaDocumento() As Boolean
    Try
      If edTm_conto.Visible = False Then Return True

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult

      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791958958281250, "Ripristinare le modifiche apportate al documento?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return False
        Case Windows.Forms.DialogResult.Yes
          Me.Cursor = Cursors.WaitCursor
          Ripristina()
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    'nDestin: 0 = video, 1 = carta
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim strNumcliPVR As String

    Dim strKey2 As String = "Reports1"
    Dim strReportName As String = "BSVEFATD.rpt"

    Try
      If NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_valuta) <> 0 Then
        strKey2 = "Reports3"
      ElseIf dsFdin.Tables("TESTMAG").Rows(0)!tm_scorpo.ToString = "S" Then
        strKey2 = "Reports2"
      Else
        strKey2 = "Reports1"
      End If

      '--------------------------------------------------
      'Preimposta il nome del report da stampare
      Select Case dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString
        Case "D", "K", "£", "("
          If NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_valuta) <> 0 Then
            strReportName = "BSVEFADV.RPT"
          ElseIf dsFdin.Tables("TESTMAG").Rows(0)!tm_scorpo.ToString = "S" Then
            strReportName = "BSVEFADC.RPT"
          Else
            strReportName = "BSVEFATD.RPT"
          End If
        Case "P"
          If NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_valuta) <> 0 Then
            strReportName = "BSVEFRDV.RPT"
          ElseIf dsFdin.Tables("TESTMAG").Rows(0)!tm_scorpo.ToString = "S" Then
            strReportName = "BSVEFRDC.RPT"
          Else
            strReportName = "BSVEFRFD.RPT"
          End If
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128727080808437500, "Tipo documento |" & dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString & "| non consentito."))
          Return
      End Select

      '--------------------------------------------------
      strNumcliPVR = ""
      If dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString = "D" Then
        If oCleFdin.bGestPVR = True Then
          strNumcliPVR = oMenu.GetSettingBus("OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
          For i = 1 To (6 - Len(strNumcliPVR))
            strNumcliPVR = "0" & strNumcliPVR
          Next
          'per un fattore di velocità non testo la tabella valuta:
          'se sono in valuta presuppongo che sia l'unica ammessa
          If NTSCInt(dsFdin.Tables("TESTMAG").Rows(0)!tm_valuta) = 0 Then
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
          Else
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
          End If
          strNumcliPVR = strNumcliPVR & CLN__STD.calcolaPVRCheck(strNumcliPVR)
        End If
      End If

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{testmag.codditt} = '" & oApp.Ditta & "'" & _
                " And {testmag.tm_anno} = " & dsFdin.Tables("TESTMAG").Rows(0)!tm_anno.ToString & _
                " AND {testmag.tm_numdoc} = " & dsFdin.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString & _
                " AND {testmag.tm_serie} = '" & dsFdin.Tables("TESTMAG").Rows(0)!tm_serie.ToString & "'" & _
                " AND {testmag.tm_tipork} = '" & dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString & "'" & _
                " AND {movmag.mm_stasino} <> 'N'" & _
                " AND {movmag.mm_stasino} <> 'B'"
      If oCleFdin.bUsaKeyMag Then strCrpe = strCrpe & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEFDIN", strKey2, dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString, _
                                      0, nDestin, strReportName, False, "Stampa Fattura differita", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        If dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString = "D" Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NUMCLIPVR", "'" & strNumcliPVR & "'")
        End If

        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
        For j = 3 To 12
          If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
          End If
        Next j
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Select Case strIn.Substring(0, 1)
      Case "#"
        'Variabile esposte SPECIFICHE PER PROGRAMMA
        Select Case UCase$(Mid$(strIn, 2))
          Case "TIPORK"
            Return dsFdin.Tables("TESTMAG").Rows(0)!tm_tipork.ToString
          Case "ANNO"
            Return dsFdin.Tables("TESTMAG").Rows(0)!tm_anno.ToString
          Case "SERIE"
            Return dsFdin.Tables("TESTMAG").Rows(0)!tm_serie.ToString
          Case "NUMDOC"
            Return dsFdin.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString
          Case Else
            Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else
        Return MyBase.ResolveField(strIn)
    End Select
    Return ""
  End Function

  Public Overridable Function BloccaSbloccaControlli() As Boolean
    Try
      If dsFdin.Tables("ELENCODDT").Rows.Count = 0 Then
        'Nessuna bolla è aggiunta
        '------------------------
        TabPage1.Enable = False
        TabPage3.Enable = False
        TabPage4.Enable = False
        edTm_coddest.Enabled = False
        cmdDDTMod.Enabled = False
        cmdDDTDel.Enabled = False
        GctlSetVisEnab(edTm_conto, False)
        ckTm_flscdb.Checked = False       'se ho scollegato l'ultimo ddt tolgo anche il flag 'sblocca castelletti' per tornare come in stato 'nuovo'
      Else
        'Una o + bolle sono aggiunte
        TabPage1.Enable = True
        TabPage3.Enable = True
        TabPage4.Enable = True
        GctlSetVisEnab(edTm_coddest, False)
        GctlSetVisEnab(cmdDDTMod, False)
        GctlSetVisEnab(cmdDDTDel, False)

        edTm_conto.Enabled = False
        tsFdin.Enabled = True
      End If    'If dsFdin.Tables("ELENCODDT").Rows.Count = 0 Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function OperazioneConsentita() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      OperazioneConsentita = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione "Friendly", scaduta e la data di registrazione è posteriore alla data di
      '--- scadenza della chiave, ritorna false
      '--------------------------------------------------------------------------------------------------------------
      If (CLN__STD.FRIENDLY = True) And _
         (NTSCDate(Now.ToShortDateString) > NTSCDate(oApp.ActKey.DataScad)) And _
         (NTSCDate(edTm_datdoc.Text) > NTSCDate(oApp.ActKey.DataScad)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130161960628802790, "Attenzione!" & vbCrLf & _
          "Chiave di attivazione scaduta!" & vbCrLf & _
          "Operazione NON consentita su registrazioni posteriori al '|" & oApp.ActKey.DataScad & "|'."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub edTm_alfpar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_alfpar.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edTm_alfpar.Text, False)
      If strTmp <> edTm_alfpar.Text Then edTm_alfpar.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edTm_alfpro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTm_alfpro.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edTm_alfpro.Text, False)
      If strTmp <> edTm_alfpro.Text Then edTm_alfpro.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

End Class