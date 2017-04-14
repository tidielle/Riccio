Imports System
Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEBOLL_F

#Region "Dichiarazione variabili"
  Public oCallParams As CLE__CLDP
  Public oCleBoll As CLEVEBOLL
  Public dsBoll As DataSet
  Public dcBoll As BindingSource = New BindingSource      'collegato a testmag/testprb
  Public dcBollc As BindingSource = New BindingSource     'collegato a testmagc/testprbc  
  Public dcBollRighe As BindingSource = New BindingSource 'collegato a movmag/movprb
  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico

  Public bInLoadDoc As Boolean = False                    'true durante il caricamento di un documento

  Private components As System.ComponentModel.IContainer
  Public lRigaCopiata As Integer = 0                      'utilizzata dalle voci di menu copia/incolla riga per memorizzare la riga da copiare
  Public strLastColEdited As String = ""
  Public bInCambioTab As Boolean = False
  Public dttOpens As New DataTable    'datatable contenente l'elenco dei documenti che si vogliono aprire (compilato da zoom documenti per selezionarne uno)
  Public strLastTipork As String = ""
  Public nLastAnno As Integer = 0
  Public strLastSerie As String = ""
  Public lLastNumdoc As Integer = 0

  Public lContoProposto As Integer = 0 'Utilizzato in fase di creazione di un nuovo documento dal menù social

  Public bCa2InCG As Boolean = False      'se TRUE il documento è collegato a registrazioni di CA2 in CG, per cui potrà essere solo parzialmente modificato in campi che non toccano la CA2
  Public dtLastCellKeyPress As DateTime = DateTime.Now

  Public nCarProdInVendita As Integer = 0             'se <> 0 dopo aver creato un nuovo documento di vendita viene creato in automatico il carico da produz per gli articoli appena venduti
  Public bCreaCarProd As Boolean = False
  Public strLastTiporktmp As String = ""
  Public strLastPrezzoInbolla As String = "N"
  Public bInDeleteRow As Boolean = False
  Public bNoResetDttOpens As Boolean = False 'Per eviatere che venga resettato al cambio di tipo documetno se chiamato da DKKONS

  Public bDaTastiNavigazione As Boolean = False
  Public bInAbilitaCellaEnter As Boolean = False
#End Region

#Region "Moduli"
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
#End Region

#Region "Inizializzazione"
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
      Me.Height = 517
      Me.MinimumSize = Me.Size

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEBOLL", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleBoll = CType(oTmp, CLEVEBOLL)
      '------------------------------------------------
      bRemoting = Menu.Remoting("BNVEBOLL", strRemoteServer, strRemotePort)
      AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleBoll.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEBOLL_F))
Me.pnTestataTop = New NTSInformatica.NTSPanel
Me.lbDoc = New NTSInformatica.NTSLabel
Me.cbTipoDoc = New NTSInformatica.NTSComboBox
Me.edAnnoDoc = New NTSInformatica.NTSTextBoxNum
Me.edNumDoc = New NTSInformatica.NTSTextBoxNum
Me.edSerieDoc = New NTSInformatica.NTSTextBoxStr
Me.lbet_datdoc = New NTSInformatica.NTSLabel
Me.lbXx_coddest = New NTSInformatica.NTSLabel
Me.edet_datdoc = New NTSInformatica.NTSTextBoxData
Me.lbEt_coddest = New NTSInformatica.NTSLabel
Me.lbXx_conto = New NTSInformatica.NTSLabel
Me.edEt_coddest = New NTSInformatica.NTSTextBoxNum
Me.lbEt_conto = New NTSInformatica.NTSLabel
Me.edEt_conto = New NTSInformatica.NTSTextBoxNum
Me.TabPage5 = New NTSInformatica.NTSTabPage
Me.pnNote = New NTSInformatica.NTSPanel
Me.edEt_note = New NTSInformatica.NTSMemoBox
Me.TabPage3 = New NTSInformatica.NTSTabPage
Me.pnPage3 = New NTSInformatica.NTSPanel
Me.pnPiedeTrasporto = New NTSInformatica.NTSPanel
Me.lbEt_intra = New NTSInformatica.NTSLabel
Me.cbEt_intra = New NTSInformatica.NTSComboBox
Me.lbXx_codntra = New NTSInformatica.NTSLabel
Me.edEt_codntra = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codntra = New NTSInformatica.NTSLabel
Me.cmdCastelletti = New NTSInformatica.NTSButton
Me.lbXx_vettor = New NTSInformatica.NTSLabel
Me.fmProvvigioni = New NTSInformatica.NTSGroupBox
Me.ckEt_flprov = New NTSInformatica.NTSCheckBox
Me.edEt_totprov = New NTSInformatica.NTSTextBoxNum
Me.lbEt_totprov = New NTSInformatica.NTSLabel
Me.edEt_impprov = New NTSInformatica.NTSTextBoxNum
Me.lbEt_impprov = New NTSInformatica.NTSLabel
Me.cmdModifScad = New NTSInformatica.NTSButton
Me.edEt_vettor = New NTSInformatica.NTSTextBoxNum
Me.cmdUltSpese = New NTSInformatica.NTSButton
Me.lbEt_vettor = New NTSInformatica.NTSLabel
Me.fmPartita = New NTSInformatica.NTSGroupBox
Me.lbEt_datpar = New NTSInformatica.NTSLabel
Me.edEt_datpar = New NTSInformatica.NTSTextBoxData
Me.edEt_numpar = New NTSInformatica.NTSTextBoxNum
Me.lbEt_numpar = New NTSInformatica.NTSLabel
Me.edEt_alfpar = New NTSInformatica.NTSTextBoxStr
Me.lbEt_annpar = New NTSInformatica.NTSLabel
Me.edEt_annpar = New NTSInformatica.NTSTextBoxNum
Me.lbXx_codcfam = New NTSInformatica.NTSLabel
Me.fmInizioTrasporto = New NTSInformatica.NTSGroupBox
Me.edEt_oriniz = New NTSInformatica.NTSTextBoxNum
Me.edEt_dtiniz = New NTSInformatica.NTSTextBoxData
Me.lbEt_oriniz = New NTSInformatica.NTSLabel
Me.lbEt_dtiniz = New NTSInformatica.NTSLabel
Me.lbEt_codcfam = New NTSInformatica.NTSLabel
Me.edEt_codcfam = New NTSInformatica.NTSTextBoxStr
Me.edEt_caustra = New NTSInformatica.NTSTextBoxNum
Me.lbEt_acuradi = New NTSInformatica.NTSLabel
Me.lbEt_caustra = New NTSInformatica.NTSLabel
Me.edEt_aspetto = New NTSInformatica.NTSTextBoxStr
Me.lbXx_caustra = New NTSInformatica.NTSLabel
Me.lbEt_codaspe = New NTSInformatica.NTSLabel
Me.edEt_codaspe = New NTSInformatica.NTSTextBoxNum
Me.lbEt_coddest2 = New NTSInformatica.NTSLabel
Me.edEt_coddest2 = New NTSInformatica.NTSTextBoxNum
Me.lbXx_coddest2 = New NTSInformatica.NTSLabel
Me.lbXx_porto = New NTSInformatica.NTSLabel
Me.lbEt_vettor2 = New NTSInformatica.NTSLabel
Me.edEt_porto = New NTSInformatica.NTSTextBoxStr
Me.edEt_vettor2 = New NTSInformatica.NTSTextBoxNum
Me.lbEt_porto = New NTSInformatica.NTSLabel
Me.lbXx_vettor2 = New NTSInformatica.NTSLabel
Me.cbEt_acuradi = New NTSInformatica.NTSComboBox
Me.pnPiedeTotali = New NTSInformatica.NTSPanel
Me.lbPesoLordo = New NTSInformatica.NTSLabel
Me.lbEur1 = New NTSInformatica.NTSLabel
Me.lbValuta1 = New NTSInformatica.NTSLabel
Me.ckEt_flscdb = New NTSInformatica.NTSCheckBox
Me.lbResto = New NTSInformatica.NTSLabel
Me.lbValuta = New NTSInformatica.NTSLabel
Me.edEt_resto = New NTSInformatica.NTSTextBoxNum
Me.lbTrasporto = New NTSInformatica.NTSLabel
Me.lbEt_pagato2 = New NTSInformatica.NTSLabel
Me.edEt_speacc = New NTSInformatica.NTSTextBoxNum
Me.edEt_pagato2 = New NTSInformatica.NTSTextBoxNum
Me.lbEur = New NTSInformatica.NTSLabel
Me.lbImballo = New NTSInformatica.NTSLabel
Me.lbRiga2 = New NTSInformatica.NTSLabel
Me.edEt_speimb = New NTSInformatica.NTSTextBoxNum
Me.edTotalev = New NTSInformatica.NTSTextBoxNum
Me.lbXx_totquant = New NTSInformatica.NTSLabel
Me.lbRiga1 = New NTSInformatica.NTSLabel
Me.edTotale = New NTSInformatica.NTSTextBoxNum
Me.lbIncasso = New NTSInformatica.NTSLabel
Me.lbTotale = New NTSInformatica.NTSLabel
Me.edEt_speinc = New NTSInformatica.NTSTextBoxNum
Me.edResiduo = New NTSInformatica.NTSTextBoxNum
Me.edEt_pesonetto = New NTSInformatica.NTSTextBoxNum
Me.lbBolli = New NTSInformatica.NTSLabel
Me.lbRimanenza = New NTSInformatica.NTSLabel
Me.lbPesoNetto = New NTSInformatica.NTSLabel
Me.edEt_bolli = New NTSInformatica.NTSTextBoxNum
Me.edResiduov = New NTSInformatica.NTSTextBoxNum
Me.edEt_peso = New NTSInformatica.NTSTextBoxNum
Me.edTotIvav = New NTSInformatica.NTSTextBoxNum
Me.edEt_abbuono = New NTSInformatica.NTSTextBoxNum
Me.edEt_totcoll = New NTSInformatica.NTSTextBoxNum
Me.lbNetto = New NTSInformatica.NTSLabel
Me.lbEt_totcoll = New NTSInformatica.NTSLabel
Me.lbAbbuono = New NTSInformatica.NTSLabel
Me.edEt_totmerce = New NTSInformatica.NTSTextBoxNum
Me.edEt_pagato = New NTSInformatica.NTSTextBoxNum
Me.edTotImponibv = New NTSInformatica.NTSTextBoxNum
Me.lbIncassato = New NTSInformatica.NTSLabel
Me.lbSpese = New NTSInformatica.NTSLabel
Me.edEt_abbuonov = New NTSInformatica.NTSTextBoxNum
Me.edTotSpesev = New NTSInformatica.NTSTextBoxNum
Me.edEt_totomag = New NTSInformatica.NTSTextBoxNum
Me.edTotSpese = New NTSInformatica.NTSTextBoxNum
Me.lbOmaggi = New NTSInformatica.NTSLabel
Me.edEt_totmercev = New NTSInformatica.NTSTextBoxNum
Me.edEt_totdoc = New NTSInformatica.NTSTextBoxNum
Me.lbImponibile = New NTSInformatica.NTSLabel
Me.edEt_pagatov = New NTSInformatica.NTSTextBoxNum
Me.edTotImponib = New NTSInformatica.NTSTextBoxNum
Me.lbDocumento = New NTSInformatica.NTSLabel
Me.edEt_bolliv = New NTSInformatica.NTSTextBoxNum
Me.edEt_totomagv = New NTSInformatica.NTSTextBoxNum
Me.edEt_speaccv = New NTSInformatica.NTSTextBoxNum
Me.edEt_totdocv = New NTSInformatica.NTSTextBoxNum
Me.lbImposta = New NTSInformatica.NTSLabel
Me.edEt_speincv = New NTSInformatica.NTSTextBoxNum
Me.edTotIva = New NTSInformatica.NTSTextBoxNum
Me.edEt_speimbv = New NTSInformatica.NTSTextBoxNum
Me.pnPiedeSx = New NTSInformatica.NTSPanel
Me.cmdPiedeTrasporto = New NTSInformatica.NTSButton
Me.cmdPiedeTotali = New NTSInformatica.NTSButton
Me.TabPage2 = New NTSInformatica.NTSTabPage
Me.pnGriglia = New NTSInformatica.NTSPanel
Me.grRighe = New NTSInformatica.NTSGrid
Me.grvRighe = New NTSInformatica.NTSGridView
Me.xx_sel = New NTSInformatica.NTSGridColumn
Me.ec_riga = New NTSInformatica.NTSGridColumn
Me.ec_matric = New NTSInformatica.NTSGridColumn
Me.ec_codart = New NTSInformatica.NTSGridColumn
Me.ec_descr = New NTSInformatica.NTSGridColumn
Me.ec_desint = New NTSInformatica.NTSGridColumn
Me.ec_unmis = New NTSInformatica.NTSGridColumn
Me.ec_colli = New NTSInformatica.NTSGridColumn
Me.ec_ump = New NTSInformatica.NTSGridColumn
Me.ec_quant = New NTSInformatica.NTSGridColumn
Me.ec_preziva = New NTSInformatica.NTSGridColumn
Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
Me.ec_prezzo = New NTSInformatica.NTSGridColumn
Me.ec_scont1 = New NTSInformatica.NTSGridColumn
Me.ec_scont2 = New NTSInformatica.NTSGridColumn
Me.ec_scont3 = New NTSInformatica.NTSGridColumn
Me.ec_scont4 = New NTSInformatica.NTSGridColumn
Me.ec_scont5 = New NTSInformatica.NTSGridColumn
Me.ec_scont6 = New NTSInformatica.NTSGridColumn
Me.ec_causale = New NTSInformatica.NTSGridColumn
Me.xxo_causale = New NTSInformatica.NTSGridColumn
Me.ec_magaz = New NTSInformatica.NTSGridColumn
Me.xxo_magaz = New NTSInformatica.NTSGridColumn
Me.ec_ricimp = New NTSInformatica.NTSGridColumn
Me.ec_provv = New NTSInformatica.NTSGridColumn
Me.ec_vprovv = New NTSInformatica.NTSGridColumn
Me.ec_provv2 = New NTSInformatica.NTSGridColumn
Me.ec_vprovv2 = New NTSInformatica.NTSGridColumn
Me.ec_controp = New NTSInformatica.NTSGridColumn
Me.xxo_controp = New NTSInformatica.NTSGridColumn
Me.ec_codiva = New NTSInformatica.NTSGridColumn
Me.xxo_codiva = New NTSInformatica.NTSGridColumn
Me.ec_stasino = New NTSInformatica.NTSGridColumn
Me.ec_codcfam = New NTSInformatica.NTSGridColumn
Me.xxo_codcfam = New NTSInformatica.NTSGridColumn
Me.ec_commeca = New NTSInformatica.NTSGridColumn
Me.xxo_commeca = New NTSInformatica.NTSGridColumn
Me.ec_subcommeca = New NTSInformatica.NTSGridColumn
Me.xxo_lottox = New NTSInformatica.NTSGridColumn
Me.ec_codcena = New NTSInformatica.NTSGridColumn
Me.xxo_codcena = New NTSInformatica.NTSGridColumn
Me.ec_note = New NTSInformatica.NTSGridColumn
Me.ec_causale2 = New NTSInformatica.NTSGridColumn
Me.xxo_causale2 = New NTSInformatica.NTSGridColumn
Me.ec_magaz2 = New NTSInformatica.NTSGridColumn
Me.xxo_magaz2 = New NTSInformatica.NTSGridColumn
Me.ec_valore = New NTSInformatica.NTSGridColumn
Me.ec_contocontr = New NTSInformatica.NTSGridColumn
Me.xxo_contocon = New NTSInformatica.NTSGridColumn
Me.ec_codclie = New NTSInformatica.NTSGridColumn
Me.xxo_codclie = New NTSInformatica.NTSGridColumn
Me.ec_misura1 = New NTSInformatica.NTSGridColumn
Me.ec_misura2 = New NTSInformatica.NTSGridColumn
Me.ec_misura3 = New NTSInformatica.NTSGridColumn
Me.xxo_codarfo = New NTSInformatica.NTSGridColumn
Me.ec_perqta = New NTSInformatica.NTSGridColumn
Me.ec_flkit = New NTSInformatica.NTSGridColumn
Me.ec_ktriga = New NTSInformatica.NTSGridColumn
Me.ec_umprz = New NTSInformatica.NTSGridColumn
Me.ec_fase = New NTSInformatica.NTSGridColumn
Me.xxo_fase = New NTSInformatica.NTSGridColumn
Me.ec_pmtaskid = New NTSInformatica.NTSGridColumn
Me.xxo_pmtaskid = New NTSInformatica.NTSGridColumn
Me.xxo_pmqtares = New NTSInformatica.NTSGridColumn
Me.xxo_pmqtarim = New NTSInformatica.NTSGridColumn
Me.xxo_pmvalres = New NTSInformatica.NTSGridColumn
Me.xxo_pmvalrim = New NTSInformatica.NTSGridColumn
Me.ec_pmsalcon = New NTSInformatica.NTSGridColumn
Me.xxo_pmqtadisda = New NTSInformatica.NTSGridColumn
Me.xxo_pmvaldisda = New NTSInformatica.NTSGridColumn
Me.xxo_darave = New NTSInformatica.NTSGridColumn
Me.ec_ubicaz = New NTSInformatica.NTSGridColumn
Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
Me.ec_flprznet = New NTSInformatica.NTSGridColumn
Me.ec_codtpro = New NTSInformatica.NTSGridColumn
Me.xxo_codtpro = New NTSInformatica.NTSGridColumn
Me.ec_datini = New NTSInformatica.NTSGridColumn
Me.ec_datfin = New NTSInformatica.NTSGridColumn
Me.ec_ortipo = New NTSInformatica.NTSGridColumn
Me.ec_oranno = New NTSInformatica.NTSGridColumn
Me.ec_orserie = New NTSInformatica.NTSGridColumn
Me.ec_ornum = New NTSInformatica.NTSGridColumn
Me.ec_orriga = New NTSInformatica.NTSGridColumn
Me.ec_salcon = New NTSInformatica.NTSGridColumn
Me.ec_commen = New NTSInformatica.NTSGridColumn
Me.ec_codnomc = New NTSInformatica.NTSGridColumn
Me.ec_massakg = New NTSInformatica.NTSGridColumn
Me.ec_massaum2 = New NTSInformatica.NTSGridColumn
Me.ec_valstat = New NTSInformatica.NTSGridColumn
Me.ec_proorig = New NTSInformatica.NTSGridColumn
Me.ec_percvst = New NTSInformatica.NTSGridColumn
Me.ec_prtipo = New NTSInformatica.NTSGridColumn
Me.ec_pranno = New NTSInformatica.NTSGridColumn
Me.ec_prserie = New NTSInformatica.NTSGridColumn
Me.ec_prnum = New NTSInformatica.NTSGridColumn
Me.ec_prriga = New NTSInformatica.NTSGridColumn
Me.ec_cltipo = New NTSInformatica.NTSGridColumn
Me.ec_clanno = New NTSInformatica.NTSGridColumn
Me.ec_clserie = New NTSInformatica.NTSGridColumn
Me.ec_clnum = New NTSInformatica.NTSGridColumn
Me.ec_clriga = New NTSInformatica.NTSGridColumn
Me.ec_nptipo = New NTSInformatica.NTSGridColumn
Me.ec_npanno = New NTSInformatica.NTSGridColumn
Me.ec_npserie = New NTSInformatica.NTSGridColumn
Me.ec_npnum = New NTSInformatica.NTSGridColumn
Me.ec_npriga = New NTSInformatica.NTSGridColumn
Me.ec_npqtadis = New NTSInformatica.NTSGridColumn
Me.ec_npcoldis = New NTSInformatica.NTSGridColumn
Me.ec_npvaldis = New NTSInformatica.NTSGridColumn
Me.ec_npsalcon = New NTSInformatica.NTSGridColumn
Me.ec_nprcoleva = New NTSInformatica.NTSGridColumn
Me.ec_nprquaeva = New NTSInformatica.NTSGridColumn
Me.ec_nprflevas = New NTSInformatica.NTSGridColumn
Me.ec_nprvalore = New NTSInformatica.NTSGridColumn
Me.xxo_vallordo = New NTSInformatica.NTSGridColumn
Me.ec_actipo = New NTSInformatica.NTSGridColumn
Me.ec_acanno = New NTSInformatica.NTSGridColumn
Me.ec_acserie = New NTSInformatica.NTSGridColumn
Me.ec_acnum = New NTSInformatica.NTSGridColumn
Me.ec_cctipo = New NTSInformatica.NTSGridColumn
Me.ec_ccanno = New NTSInformatica.NTSGridColumn
Me.ec_ccserie = New NTSInformatica.NTSGridColumn
Me.ec_ccnum = New NTSInformatica.NTSGridColumn
Me.ec_ccriga = New NTSInformatica.NTSGridColumn
Me.xxo_valrigaic = New NTSInformatica.NTSGridColumn
Me.ec_flcom = New NTSInformatica.NTSGridColumn
Me.ec_flelab = New NTSInformatica.NTSGridColumn
Me.ec_valorev = New NTSInformatica.NTSGridColumn
Me.ec_coddivi = New NTSInformatica.NTSGridColumn
Me.xxo_coddivi = New NTSInformatica.NTSGridColumn
Me.ec_cvtipo = New NTSInformatica.NTSGridColumn
Me.ec_cvanno = New NTSInformatica.NTSGridColumn
Me.ec_cvserie = New NTSInformatica.NTSGridColumn
Me.ec_cvnum = New NTSInformatica.NTSGridColumn
Me.ec_cvriga = New NTSInformatica.NTSGridColumn
Me.xxo_codalt = New NTSInformatica.NTSGridColumn
Me.ec_flprzmod = New NTSInformatica.NTSGridColumn
Me.ec_flsc1mod = New NTSInformatica.NTSGridColumn
Me.ec_flsc2mod = New NTSInformatica.NTSGridColumn
Me.ec_flsc3mod = New NTSInformatica.NTSGridColumn
Me.ec_flsc4mod = New NTSInformatica.NTSGridColumn
Me.ec_flsc5mod = New NTSInformatica.NTSGridColumn
Me.ec_flsc6mod = New NTSInformatica.NTSGridColumn
Me.ec_flprov1mod = New NTSInformatica.NTSGridColumn
Me.ec_flprov2mod = New NTSInformatica.NTSGridColumn
Me.xxo_valoreiva = New NTSInformatica.NTSGridColumn
Me.xxo_percricarico = New NTSInformatica.NTSGridColumn
Me.xxo_percmargine = New NTSInformatica.NTSGridColumn
Me.xxo_marginevalore = New NTSInformatica.NTSGridColumn
Me.pnCorpo = New NTSInformatica.NTSPanel
Me.lbOrdinImpegn = New NTSInformatica.NTSLabel
Me.edOrdinato = New NTSInformatica.NTSTextBoxNum
Me.edImpegnato = New NTSInformatica.NTSTextBoxNum
Me.lbSconto = New NTSInformatica.NTSLabel
Me.edSconto = New NTSInformatica.NTSTextBoxNum
Me.edUltCost = New NTSInformatica.NTSTextBoxNum
Me.edPreList = New NTSInformatica.NTSTextBoxNum
Me.edDispNetta = New NTSInformatica.NTSTextBoxNum
Me.edDispon = New NTSInformatica.NTSTextBoxNum
Me.lbDispon = New NTSInformatica.NTSLabel
Me.lbPreList = New NTSInformatica.NTSLabel
Me.TabPage1 = New NTSInformatica.NTSTabPage
Me.pnTestataDx = New NTSInformatica.NTSPanel
Me.pnDatiTesta2 = New NTSInformatica.NTSPanel
Me.fmPa = New NTSInformatica.NTSGroupBox
Me.edEt_riferimpa = New NTSInformatica.NTSTextBoxStr
Me.lbEt_riferimpa = New NTSInformatica.NTSLabel
Me.edEt_cig = New NTSInformatica.NTSTextBoxStr
Me.edEt_cup = New NTSInformatica.NTSTextBoxStr
Me.lbEt_cig = New NTSInformatica.NTSLabel
Me.lbEt_cup = New NTSInformatica.NTSLabel
Me.edEt_valuta = New NTSInformatica.NTSTextBoxNum
Me.lbEt_valuta = New NTSInformatica.NTSLabel
Me.fmBanca = New NTSInformatica.NTSGroupBox
Me.lbXx_codbanc = New NTSInformatica.NTSLabel
Me.edEt_codbanc = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codbanc = New NTSInformatica.NTSLabel
Me.edEt_banc2 = New NTSInformatica.NTSTextBoxStr
Me.edEt_banc1 = New NTSInformatica.NTSTextBoxStr
Me.edEt_cab = New NTSInformatica.NTSTextBoxNum
Me.edEt_abi = New NTSInformatica.NTSTextBoxNum
Me.lbEt_cab = New NTSInformatica.NTSLabel
Me.lbEt_abi = New NTSInformatica.NTSLabel
Me.lbXx_causale = New NTSInformatica.NTSLabel
Me.fmOpzioni = New NTSInformatica.NTSGroupBox
Me.ckXx_przbol = New NTSInformatica.NTSCheckBox
Me.ckEt_scorpo = New NTSInformatica.NTSCheckBox
Me.ckEt_flspinc = New NTSInformatica.NTSCheckBox
Me.ckEt_flboll = New NTSInformatica.NTSCheckBox
Me.ckEt_proforma = New NTSInformatica.NTSCheckBox
Me.edEt_codese = New NTSInformatica.NTSTextBoxNum
Me.edEt_causale = New NTSInformatica.NTSTextBoxNum
Me.edEt_cambio = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codese = New NTSInformatica.NTSLabel
Me.lbEt_causale = New NTSInformatica.NTSLabel
Me.lbXx_magaz2 = New NTSInformatica.NTSLabel
Me.lbXx_codese = New NTSInformatica.NTSLabel
Me.lbXx_valuta = New NTSInformatica.NTSLabel
Me.edEt_controp = New NTSInformatica.NTSTextBoxNum
Me.lbEt_controp = New NTSInformatica.NTSLabel
Me.edEt_magaz2 = New NTSInformatica.NTSTextBoxNum
Me.lbXx_controp = New NTSInformatica.NTSLabel
Me.lbEt_magaz2 = New NTSInformatica.NTSLabel
Me.pnDatiTesta1 = New NTSInformatica.NTSPanel
Me.edEt_listino = New NTSInformatica.NTSTextBoxNum
Me.lbEt_listino = New NTSInformatica.NTSLabel
Me.lbEt_codpaga = New NTSInformatica.NTSLabel
Me.lbEt_scopag = New NTSInformatica.NTSLabel
Me.lbEt_datapag = New NTSInformatica.NTSLabel
Me.lbScontiTesta = New NTSInformatica.NTSLabel
Me.edEt_codpaga = New NTSInformatica.NTSTextBoxNum
Me.edEt_scopag = New NTSInformatica.NTSTextBoxNum
Me.lbXx_tipobf = New NTSInformatica.NTSLabel
Me.lbXx_codpaga = New NTSInformatica.NTSLabel
Me.lbEt_tipobf = New NTSInformatica.NTSLabel
Me.edEt_datapag = New NTSInformatica.NTSTextBoxData
Me.edEt_tipobf = New NTSInformatica.NTSTextBoxNum
Me.edEt_scont2 = New NTSInformatica.NTSTextBoxNum
Me.edEt_riferim = New NTSInformatica.NTSTextBoxStr
Me.edEt_scont1 = New NTSInformatica.NTSTextBoxNum
Me.lbEt_riferim = New NTSInformatica.NTSLabel
Me.lbXx_codagen = New NTSInformatica.NTSLabel
Me.edEt_magaz = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codagen = New NTSInformatica.NTSLabel
Me.edEt_codagen = New NTSInformatica.NTSTextBoxNum
Me.lbEt_magaz = New NTSInformatica.NTSLabel
Me.lbXx_magaz = New NTSInformatica.NTSLabel
Me.pnTestataSx = New NTSInformatica.NTSPanel
Me.cmdDatiTesta2 = New NTSInformatica.NTSButton
Me.cmdDatiTesta1 = New NTSInformatica.NTSButton
Me.tsBoll = New NTSInformatica.NTSTabControl
Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
Me.tlbMain = New NTSInformatica.NTSBar
Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
Me.tlbApri = New NTSInformatica.NTSBarButtonItem
Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
Me.tlbApriDocPrec = New NTSInformatica.NTSBarButtonItem
Me.tlbApriDocSucc = New NTSInformatica.NTSBarButtonItem
Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
Me.tlbSelOrdini = New NTSInformatica.NTSBarButtonItem
Me.tlbSelRigheOrdini = New NTSInformatica.NTSBarButtonItem
Me.tlbMnuFile = New NTSInformatica.NTSBarSubItem
Me.tlbNuovoDocDaPrec = New NTSInformatica.NTSBarButtonItem
Me.tlbDatiContabiliSintetici = New NTSInformatica.NTSBarButtonItem
Me.tlbModifNumDoc = New NTSInformatica.NTSBarMenuItem
Me.tlbRettificaTotdoc = New NTSInformatica.NTSBarMenuItem
Me.tlbRicalcPrzScnPro = New NTSInformatica.NTSBarButtonItem
Me.tlbRicFiscDaRiem = New NTSInformatica.NTSBarMenuItem
Me.tlbAggCorrFF = New NTSInformatica.NTSBarMenuItem
Me.tlbNavigazDoc = New NTSInformatica.NTSBarMenuItem
Me.tlbSchedatrasporto = New NTSInformatica.NTSBarMenuItem
Me.tlbEmail = New NTSInformatica.NTSBarButtonItem
Me.tlbMnuRecord = New NTSInformatica.NTSBarSubItem
Me.tlbPrecMov = New NTSInformatica.NTSBarMenuItem
Me.tlbScarCollegati = New NTSInformatica.NTSBarMenuItem
Me.tlbLavCollegate = New NTSInformatica.NTSBarMenuItem
Me.tlbSeleziona = New NTSInformatica.NTSBarSubItem
Me.tlbRigheOrdBarcode = New NTSInformatica.NTSBarMenuItem
Me.tlbSelezAcconti = New NTSInformatica.NTSBarMenuItem
Me.tlbScaricaMagaz = New NTSInformatica.NTSBarMenuItem
Me.tlbIntegraRighe = New NTSInformatica.NTSBarMenuItem
Me.tlbSelezDaTerm = New NTSInformatica.NTSBarMenuItem
Me.tlbDaLista = New NTSInformatica.NTSBarButtonItem
Me.tlbImport = New NTSInformatica.NTSBarButtonItem
Me.tlbExport = New NTSInformatica.NTSBarButtonItem
Me.tlbSelectDeselect = New NTSInformatica.NTSBarButtonItem
Me.tlbGiub = New NTSInformatica.NTSBarButtonItem
Me.tlbSiub = New NTSInformatica.NTSBarButtonItem
Me.tlbNoteRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbCopiaRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbIncollaRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbInsRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbVisDispVariante = New NTSInformatica.NTSBarMenuItem
Me.tlbRicalcolaColliQta = New NTSInformatica.NTSBarMenuItem
Me.tlbSalvaCondPart = New NTSInformatica.NTSBarMenuItem
Me.tlbCancRigheSel = New NTSInformatica.NTSBarMenuItem
Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
Me.tlbRegCassa = New NTSInformatica.NTSBarMenuItem
Me.tlbStampaEtichetteRiga = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaEtichetteFinale = New NTSInformatica.NTSBarMenuItem
Me.tlbConfRegCassa = New NTSInformatica.NTSBarMenuItem
Me.tlbImpostaFileTerm = New NTSInformatica.NTSBarMenuItem
Me.tlbSalvanuovo = New NTSInformatica.NTSBarMenuItem
Me.tlbRiapriDopoSalva = New NTSInformatica.NTSBarMenuItem
Me.tlbOrdinaRighe = New NTSInformatica.NTSBarSubItem
Me.tlbOrdinaRigheRiga = New NTSInformatica.NTSBarButtonItem
Me.tlbOrdinaRigheCodiceArticolo = New NTSInformatica.NTSBarButtonItem
Me.tlbOrdinaRigheDescrizione = New NTSInformatica.NTSBarButtonItem
Me.tlbRiordinarighe = New NTSInformatica.NTSBarMenuItem
Me.tlbNumerazioni = New NTSInformatica.NTSBarMenuItem
Me.tlbTabBolli = New NTSInformatica.NTSBarMenuItem
Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
Me.tlbApriDB = New NTSInformatica.NTSBarMenuItem
Me.tlbApriOrdineCollegato = New NTSInformatica.NTSBarMenuItem
Me.tlbApriNotaCollegata = New NTSInformatica.NTSBarMenuItem
Me.tlbApriBollaCtoLavcollegata = New NTSInformatica.NTSBarMenuItem
Me.tlbApriCarProdCollegato = New NTSInformatica.NTSBarMenuItem
Me.tlbApriConfiguratore = New NTSInformatica.NTSBarMenuItem
Me.lbBarcodeIn = New NTSInformatica.NTSLabel
Me.edBarcodeIn = New NTSInformatica.NTSTextBoxStr
CType(Me.dttSmartArt,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnTestataTop,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataTop.SuspendLayout
CType(Me.cbTipoDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edAnnoDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edNumDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edSerieDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edet_datdoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_coddest.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_conto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage5.SuspendLayout
CType(Me.pnNote,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnNote.SuspendLayout
CType(Me.edEt_note.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage3.SuspendLayout
CType(Me.pnPage3,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPage3.SuspendLayout
CType(Me.pnPiedeTrasporto,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeTrasporto.SuspendLayout
CType(Me.cbEt_intra.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codntra.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmProvvigioni,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmProvvigioni.SuspendLayout
CType(Me.ckEt_flprov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totprov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_impprov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_vettor.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmPartita,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmPartita.SuspendLayout
CType(Me.edEt_datpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_numpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_alfpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_annpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmInizioTrasporto,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmInizioTrasporto.SuspendLayout
CType(Me.edEt_oriniz.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_dtiniz.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codcfam.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_caustra.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_aspetto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codaspe.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_coddest2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_porto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_vettor2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.cbEt_acuradi.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPiedeTotali,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeTotali.SuspendLayout
CType(Me.ckEt_flscdb.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_resto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speacc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pagato2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speimb.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotalev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotale.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speinc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edResiduo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pesonetto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_bolli.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edResiduov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_peso.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotIvav.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abbuono.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totcoll.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totmerce.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pagato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotImponibv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abbuonov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotSpesev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totomag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotSpese.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totmercev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totdoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pagatov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotImponib.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_bolliv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totomagv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speaccv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totdocv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speincv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotIva.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speimbv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPiedeSx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeSx.SuspendLayout
Me.TabPage2.SuspendLayout
CType(Me.pnGriglia,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnGriglia.SuspendLayout
CType(Me.grRighe,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.grvRighe,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnCorpo,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnCorpo.SuspendLayout
CType(Me.edOrdinato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edImpegnato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edSconto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edUltCost.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edPreList.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edDispNetta.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edDispon.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage1.SuspendLayout
CType(Me.pnTestataDx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataDx.SuspendLayout
CType(Me.pnDatiTesta2,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta2.SuspendLayout
CType(Me.fmPa,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmPa.SuspendLayout
CType(Me.edEt_riferimpa.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cig.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cup.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_valuta.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmBanca,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmBanca.SuspendLayout
CType(Me.edEt_codbanc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_banc2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_banc1.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cab.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abi.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmOpzioni,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmOpzioni.SuspendLayout
CType(Me.ckXx_przbol.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_scorpo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flspinc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flboll.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_proforma.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codese.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_causale.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cambio.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_controp.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_magaz2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnDatiTesta1,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta1.SuspendLayout
CType(Me.edEt_listino.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codpaga.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scopag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_datapag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_tipobf.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scont2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_riferim.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scont1.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_magaz.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codagen.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnTestataSx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataSx.SuspendLayout
CType(Me.tsBoll,System.ComponentModel.ISupportInitialize).BeginInit
Me.tsBoll.SuspendLayout
CType(Me.NtsBarManager1,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edBarcodeIn.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.SuspendLayout
'
'frmPopup
'
Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
Me.frmPopup.Appearance.Options.UseBackColor = true
Me.frmPopup.Appearance.Options.UseImage = true
'
'frmAuto
'
Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
Me.frmAuto.Appearance.Options.UseBackColor = true
Me.frmAuto.Appearance.Options.UseImage = true
'
'pnTestataTop
'
Me.pnTestataTop.AllowDrop = true
Me.pnTestataTop.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTestataTop.Appearance.Options.UseBackColor = true
Me.pnTestataTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTestataTop.Controls.Add(Me.lbDoc)
Me.pnTestataTop.Controls.Add(Me.cbTipoDoc)
Me.pnTestataTop.Controls.Add(Me.edAnnoDoc)
Me.pnTestataTop.Controls.Add(Me.edNumDoc)
Me.pnTestataTop.Controls.Add(Me.edSerieDoc)
Me.pnTestataTop.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataTop.Dock = System.Windows.Forms.DockStyle.Top
Me.pnTestataTop.Location = New System.Drawing.Point(0, 30)
Me.pnTestataTop.Name = "pnTestataTop"
Me.pnTestataTop.NTSActiveTrasparency = true
Me.pnTestataTop.Size = New System.Drawing.Size(732, 32)
Me.pnTestataTop.TabIndex = 41
'
'lbDoc
'
Me.lbDoc.AutoSize = true
Me.lbDoc.BackColor = System.Drawing.Color.Transparent
Me.lbDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbDoc.Location = New System.Drawing.Point(8, 9)
Me.lbDoc.Name = "lbDoc"
Me.lbDoc.NTSDbField = ""
Me.lbDoc.Size = New System.Drawing.Size(235, 13)
Me.lbDoc.TabIndex = 85
Me.lbDoc.Text = "Tipo documento / Anno / serie / numero"
Me.lbDoc.Tooltip = ""
Me.lbDoc.UseMnemonic = false
'
'cbTipoDoc
'
Me.cbTipoDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.cbTipoDoc.DataSource = Nothing
Me.cbTipoDoc.DisplayMember = ""
Me.cbTipoDoc.Location = New System.Drawing.Point(251, 6)
Me.cbTipoDoc.Name = "cbTipoDoc"
Me.cbTipoDoc.NTSDbField = ""
Me.cbTipoDoc.Properties.AutoHeight = false
Me.cbTipoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbTipoDoc.Properties.DropDownRows = 30
Me.cbTipoDoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbTipoDoc.SelectedValue = ""
Me.cbTipoDoc.Size = New System.Drawing.Size(191, 20)
Me.cbTipoDoc.TabIndex = 0
Me.cbTipoDoc.ValueMember = ""
'
'edAnnoDoc
'
Me.edAnnoDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edAnnoDoc.EditValue = "2008"
Me.edAnnoDoc.Location = New System.Drawing.Point(444, 6)
Me.edAnnoDoc.Name = "edAnnoDoc"
Me.edAnnoDoc.NTSDbField = ""
Me.edAnnoDoc.NTSFormat = "0"
Me.edAnnoDoc.NTSForzaVisZoom = false
Me.edAnnoDoc.NTSOldValue = "2008"
Me.edAnnoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edAnnoDoc.Properties.Appearance.Options.UseBackColor = true
Me.edAnnoDoc.Properties.Appearance.Options.UseTextOptions = true
Me.edAnnoDoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edAnnoDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edAnnoDoc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edAnnoDoc.Properties.AutoHeight = false
Me.edAnnoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edAnnoDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edAnnoDoc.Size = New System.Drawing.Size(54, 20)
Me.edAnnoDoc.TabIndex = 42
'
'edNumDoc
'
Me.edNumDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edNumDoc.EditValue = "0"
Me.edNumDoc.Location = New System.Drawing.Point(547, 6)
Me.edNumDoc.Name = "edNumDoc"
Me.edNumDoc.NTSDbField = ""
Me.edNumDoc.NTSFormat = "0"
Me.edNumDoc.NTSForzaVisZoom = false
Me.edNumDoc.NTSOldValue = ""
Me.edNumDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edNumDoc.Properties.Appearance.Options.UseBackColor = true
Me.edNumDoc.Properties.Appearance.Options.UseTextOptions = true
Me.edNumDoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edNumDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edNumDoc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edNumDoc.Properties.AutoHeight = false
Me.edNumDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edNumDoc.Properties.MaxLength = 65536
Me.edNumDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edNumDoc.Size = New System.Drawing.Size(80, 20)
Me.edNumDoc.TabIndex = 4
'
'edSerieDoc
'
Me.edSerieDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edSerieDoc.EditValue = " "
Me.edSerieDoc.Location = New System.Drawing.Point(500, 6)
Me.edSerieDoc.Name = "edSerieDoc"
Me.edSerieDoc.NTSDbField = ""
Me.edSerieDoc.NTSForzaVisZoom = false
Me.edSerieDoc.NTSOldValue = " "
Me.edSerieDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edSerieDoc.Properties.Appearance.Options.UseBackColor = true
Me.edSerieDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edSerieDoc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edSerieDoc.Properties.AutoHeight = false
Me.edSerieDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edSerieDoc.Properties.MaxLength = 65536
Me.edSerieDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edSerieDoc.Size = New System.Drawing.Size(45, 20)
Me.edSerieDoc.TabIndex = 2
'
'lbet_datdoc
'
Me.lbet_datdoc.AutoSize = true
Me.lbet_datdoc.BackColor = System.Drawing.Color.Transparent
Me.lbet_datdoc.Location = New System.Drawing.Point(7, 6)
Me.lbet_datdoc.Name = "lbet_datdoc"
Me.lbet_datdoc.NTSDbField = ""
Me.lbet_datdoc.Size = New System.Drawing.Size(30, 13)
Me.lbet_datdoc.TabIndex = 5
Me.lbet_datdoc.Text = "Data"
Me.lbet_datdoc.Tooltip = ""
Me.lbet_datdoc.UseMnemonic = false
'
'lbXx_coddest
'
Me.lbXx_coddest.BackColor = System.Drawing.Color.Transparent
Me.lbXx_coddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_coddest.Location = New System.Drawing.Point(202, 71)
Me.lbXx_coddest.Name = "lbXx_coddest"
Me.lbXx_coddest.NTSDbField = ""
Me.lbXx_coddest.Size = New System.Drawing.Size(358, 20)
Me.lbXx_coddest.TabIndex = 47
Me.lbXx_coddest.Tooltip = ""
Me.lbXx_coddest.UseMnemonic = false
'
'edet_datdoc
'
Me.edet_datdoc.Cursor = System.Windows.Forms.Cursors.Hand
Me.edet_datdoc.EditValue = "01/01/2008"
Me.edet_datdoc.Location = New System.Drawing.Point(106, 3)
Me.edet_datdoc.Name = "edet_datdoc"
Me.edet_datdoc.NTSDbField = ""
Me.edet_datdoc.NTSForzaVisZoom = false
Me.edet_datdoc.NTSOldValue = ""
Me.edet_datdoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edet_datdoc.Properties.Appearance.Options.UseBackColor = true
Me.edet_datdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edet_datdoc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edet_datdoc.Properties.AutoHeight = false
Me.edet_datdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edet_datdoc.Properties.MaxLength = 65536
Me.edet_datdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edet_datdoc.Size = New System.Drawing.Size(90, 20)
Me.edet_datdoc.TabIndex = 6
'
'lbEt_coddest
'
Me.lbEt_coddest.AutoSize = true
Me.lbEt_coddest.BackColor = System.Drawing.Color.Transparent
Me.lbEt_coddest.Location = New System.Drawing.Point(7, 74)
Me.lbEt_coddest.Name = "lbEt_coddest"
Me.lbEt_coddest.NTSDbField = ""
Me.lbEt_coddest.Size = New System.Drawing.Size(68, 13)
Me.lbEt_coddest.TabIndex = 46
Me.lbEt_coddest.Text = "Destinazione"
Me.lbEt_coddest.Tooltip = ""
Me.lbEt_coddest.UseMnemonic = false
'
'lbXx_conto
'
Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_conto.Location = New System.Drawing.Point(202, 3)
Me.lbXx_conto.Name = "lbXx_conto"
Me.lbXx_conto.NTSDbField = ""
Me.lbXx_conto.Size = New System.Drawing.Size(358, 55)
Me.lbXx_conto.TabIndex = 7
Me.lbXx_conto.Tooltip = ""
Me.lbXx_conto.UseMnemonic = false
'
'edEt_coddest
'
Me.edEt_coddest.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_coddest.EditValue = "0"
Me.edEt_coddest.Location = New System.Drawing.Point(106, 71)
Me.edEt_coddest.Name = "edEt_coddest"
Me.edEt_coddest.NTSDbField = ""
Me.edEt_coddest.NTSFormat = "0"
Me.edEt_coddest.NTSForzaVisZoom = false
Me.edEt_coddest.NTSOldValue = ""
Me.edEt_coddest.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_coddest.Properties.Appearance.Options.UseBackColor = true
Me.edEt_coddest.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_coddest.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_coddest.Properties.AutoHeight = false
Me.edEt_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_coddest.Properties.MaxLength = 65536
Me.edEt_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_coddest.Size = New System.Drawing.Size(90, 20)
Me.edEt_coddest.TabIndex = 45
'
'lbEt_conto
'
Me.lbEt_conto.AutoSize = true
Me.lbEt_conto.BackColor = System.Drawing.Color.Transparent
Me.lbEt_conto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
Me.lbEt_conto.Location = New System.Drawing.Point(7, 32)
Me.lbEt_conto.Name = "lbEt_conto"
Me.lbEt_conto.NTSDbField = ""
Me.lbEt_conto.Size = New System.Drawing.Size(80, 13)
Me.lbEt_conto.TabIndex = 43
Me.lbEt_conto.Text = "Cliente/Forn."
Me.lbEt_conto.Tooltip = ""
Me.lbEt_conto.UseMnemonic = false
'
'edEt_conto
'
Me.edEt_conto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_conto.EditValue = "0"
Me.edEt_conto.Location = New System.Drawing.Point(106, 29)
Me.edEt_conto.Name = "edEt_conto"
Me.edEt_conto.NTSDbField = ""
Me.edEt_conto.NTSFormat = "0"
Me.edEt_conto.NTSForzaVisZoom = false
Me.edEt_conto.NTSOldValue = ""
Me.edEt_conto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_conto.Properties.Appearance.Options.UseBackColor = true
Me.edEt_conto.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_conto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_conto.Properties.AutoHeight = false
Me.edEt_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_conto.Properties.MaxLength = 65536
Me.edEt_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_conto.Size = New System.Drawing.Size(90, 20)
Me.edEt_conto.TabIndex = 44
'
'TabPage5
'
Me.TabPage5.AllowDrop = true
Me.TabPage5.Controls.Add(Me.pnNote)
Me.TabPage5.Enable = true
Me.TabPage5.Name = "TabPage5"
Me.TabPage5.Size = New System.Drawing.Size(723, 649)
Me.TabPage5.Text = "&4 - Note"
'
'pnNote
'
Me.pnNote.AllowDrop = true
Me.pnNote.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnNote.Appearance.Options.UseBackColor = true
Me.pnNote.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnNote.Controls.Add(Me.edEt_note)
Me.pnNote.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnNote.Location = New System.Drawing.Point(0, 0)
Me.pnNote.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnNote.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnNote.Name = "pnNote"
Me.pnNote.NTSActiveTrasparency = true
Me.pnNote.Size = New System.Drawing.Size(723, 649)
Me.pnNote.TabIndex = 0
'
'edEt_note
'
Me.edEt_note.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_note.Dock = System.Windows.Forms.DockStyle.Fill
Me.edEt_note.Location = New System.Drawing.Point(0, 0)
Me.edEt_note.Name = "edEt_note"
Me.edEt_note.NTSDbField = ""
Me.edEt_note.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
Me.edEt_note.Properties.Appearance.Options.UseBackColor = true
Me.edEt_note.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_note.Size = New System.Drawing.Size(723, 649)
Me.edEt_note.TabIndex = 1
'
'TabPage3
'
Me.TabPage3.AllowDrop = true
Me.TabPage3.Controls.Add(Me.pnPage3)
Me.TabPage3.Controls.Add(Me.pnPiedeSx)
Me.TabPage3.Enable = true
Me.TabPage3.Name = "TabPage3"
Me.TabPage3.Size = New System.Drawing.Size(723, 649)
Me.TabPage3.Text = "&3 - Piede"
'
'pnPage3
'
Me.pnPage3.AllowDrop = true
Me.pnPage3.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPage3.Appearance.Options.UseBackColor = true
Me.pnPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPage3.Controls.Add(Me.pnPiedeTrasporto)
Me.pnPage3.Controls.Add(Me.pnPiedeTotali)
Me.pnPage3.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPage3.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnPage3.Location = New System.Drawing.Point(152, 0)
Me.pnPage3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPage3.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPage3.Name = "pnPage3"
Me.pnPage3.NTSActiveTrasparency = true
Me.pnPage3.Size = New System.Drawing.Size(571, 649)
Me.pnPage3.TabIndex = 84
Me.pnPage3.Text = "NtsPanel1"
'
'pnPiedeTrasporto
'
Me.pnPiedeTrasporto.AllowDrop = true
Me.pnPiedeTrasporto.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeTrasporto.Appearance.Options.UseBackColor = true
Me.pnPiedeTrasporto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_intra)
Me.pnPiedeTrasporto.Controls.Add(Me.cbEt_intra)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_codntra)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_codntra)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_codntra)
Me.pnPiedeTrasporto.Controls.Add(Me.cmdCastelletti)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_vettor)
Me.pnPiedeTrasporto.Controls.Add(Me.fmProvvigioni)
Me.pnPiedeTrasporto.Controls.Add(Me.cmdModifScad)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_vettor)
Me.pnPiedeTrasporto.Controls.Add(Me.cmdUltSpese)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_vettor)
Me.pnPiedeTrasporto.Controls.Add(Me.fmPartita)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_codcfam)
Me.pnPiedeTrasporto.Controls.Add(Me.fmInizioTrasporto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_codcfam)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_codcfam)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_acuradi)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_aspetto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_codaspe)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_codaspe)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.cbEt_acuradi)
Me.pnPiedeTrasporto.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeTrasporto.Location = New System.Drawing.Point(0, 271)
Me.pnPiedeTrasporto.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeTrasporto.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeTrasporto.Name = "pnPiedeTrasporto"
Me.pnPiedeTrasporto.NTSActiveTrasparency = true
Me.pnPiedeTrasporto.Size = New System.Drawing.Size(567, 306)
Me.pnPiedeTrasporto.TabIndex = 134
Me.pnPiedeTrasporto.Text = "NtsPanel2"
'
'lbEt_intra
'
Me.lbEt_intra.AutoSize = true
Me.lbEt_intra.BackColor = System.Drawing.Color.Transparent
Me.lbEt_intra.Location = New System.Drawing.Point(11, 166)
Me.lbEt_intra.Name = "lbEt_intra"
Me.lbEt_intra.NTSDbField = ""
Me.lbEt_intra.Size = New System.Drawing.Size(82, 13)
Me.lbEt_intra.TabIndex = 119
Me.lbEt_intra.Text = "Tipo movimento"
Me.lbEt_intra.Tooltip = ""
Me.lbEt_intra.UseMnemonic = false
'
'cbEt_intra
'
Me.cbEt_intra.Cursor = System.Windows.Forms.Cursors.Default
Me.cbEt_intra.DataSource = Nothing
Me.cbEt_intra.DisplayMember = ""
Me.cbEt_intra.Location = New System.Drawing.Point(133, 163)
Me.cbEt_intra.Name = "cbEt_intra"
Me.cbEt_intra.NTSDbField = ""
Me.cbEt_intra.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.cbEt_intra.Properties.Appearance.Options.UseBackColor = true
Me.cbEt_intra.Properties.AutoHeight = false
Me.cbEt_intra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbEt_intra.Properties.DropDownRows = 30
Me.cbEt_intra.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbEt_intra.SelectedValue = ""
Me.cbEt_intra.Size = New System.Drawing.Size(170, 20)
Me.cbEt_intra.TabIndex = 118
Me.cbEt_intra.ValueMember = ""
'
'lbXx_codntra
'
Me.lbXx_codntra.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codntra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codntra.Location = New System.Drawing.Point(186, 183)
Me.lbXx_codntra.Name = "lbXx_codntra"
Me.lbXx_codntra.NTSDbField = ""
Me.lbXx_codntra.Size = New System.Drawing.Size(319, 20)
Me.lbXx_codntra.TabIndex = 117
Me.lbXx_codntra.Tooltip = ""
Me.lbXx_codntra.UseMnemonic = false
'
'edEt_codntra
'
Me.edEt_codntra.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codntra.EditValue = "0"
Me.edEt_codntra.Location = New System.Drawing.Point(133, 183)
Me.edEt_codntra.Name = "edEt_codntra"
Me.edEt_codntra.NTSDbField = ""
Me.edEt_codntra.NTSFormat = "0"
Me.edEt_codntra.NTSForzaVisZoom = false
Me.edEt_codntra.NTSOldValue = ""
Me.edEt_codntra.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codntra.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codntra.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codntra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codntra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codntra.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codntra.Properties.AutoHeight = false
Me.edEt_codntra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codntra.Properties.MaxLength = 65536
Me.edEt_codntra.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codntra.Size = New System.Drawing.Size(52, 20)
Me.edEt_codntra.TabIndex = 116
'
'lbEt_codntra
'
Me.lbEt_codntra.AutoSize = true
Me.lbEt_codntra.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codntra.Location = New System.Drawing.Point(11, 186)
Me.lbEt_codntra.Name = "lbEt_codntra"
Me.lbEt_codntra.NTSDbField = ""
Me.lbEt_codntra.Size = New System.Drawing.Size(118, 13)
Me.lbEt_codntra.TabIndex = 115
Me.lbEt_codntra.Text = "Natura transaz. (Intra)"
Me.lbEt_codntra.Tooltip = ""
Me.lbEt_codntra.UseMnemonic = false
'
'cmdCastelletti
'
Me.cmdCastelletti.ImagePath = ""
Me.cmdCastelletti.ImageText = ""
Me.cmdCastelletti.Location = New System.Drawing.Point(451, 43)
Me.cmdCastelletti.Name = "cmdCastelletti"
Me.cmdCastelletti.NTSContextMenu = Nothing
Me.cmdCastelletti.Size = New System.Drawing.Size(113, 20)
Me.cmdCastelletti.TabIndex = 83
Me.cmdCastelletti.Text = "Castelletti"
'
'lbXx_vettor
'
Me.lbXx_vettor.BackColor = System.Drawing.Color.Transparent
Me.lbXx_vettor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_vettor.Location = New System.Drawing.Point(186, 83)
Me.lbXx_vettor.Name = "lbXx_vettor"
Me.lbXx_vettor.NTSDbField = ""
Me.lbXx_vettor.Size = New System.Drawing.Size(318, 20)
Me.lbXx_vettor.TabIndex = 114
Me.lbXx_vettor.Tooltip = ""
Me.lbXx_vettor.UseMnemonic = false
'
'fmProvvigioni
'
Me.fmProvvigioni.AllowDrop = true
Me.fmProvvigioni.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmProvvigioni.Appearance.Options.UseBackColor = true
Me.fmProvvigioni.Controls.Add(Me.ckEt_flprov)
Me.fmProvvigioni.Controls.Add(Me.edEt_totprov)
Me.fmProvvigioni.Controls.Add(Me.lbEt_totprov)
Me.fmProvvigioni.Controls.Add(Me.edEt_impprov)
Me.fmProvvigioni.Controls.Add(Me.lbEt_impprov)
Me.fmProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
Me.fmProvvigioni.Location = New System.Drawing.Point(195, 232)
Me.fmProvvigioni.Name = "fmProvvigioni"
Me.fmProvvigioni.Size = New System.Drawing.Size(210, 69)
Me.fmProvvigioni.TabIndex = 3
'
'ckEt_flprov
'
Me.ckEt_flprov.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flprov.Location = New System.Drawing.Point(3, 0)
Me.ckEt_flprov.Name = "ckEt_flprov"
Me.ckEt_flprov.NTSCheckValue = "S"
Me.ckEt_flprov.NTSUnCheckValue = "N"
Me.ckEt_flprov.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flprov.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flprov.Properties.AutoHeight = false
Me.ckEt_flprov.Properties.Caption = "Provvigioni estratte"
Me.ckEt_flprov.Size = New System.Drawing.Size(115, 19)
Me.ckEt_flprov.TabIndex = 94
'
'edEt_totprov
'
Me.edEt_totprov.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_totprov.EditValue = "0"
Me.edEt_totprov.Enabled = false
Me.edEt_totprov.Location = New System.Drawing.Point(122, 45)
Me.edEt_totprov.Name = "edEt_totprov"
Me.edEt_totprov.NTSDbField = ""
Me.edEt_totprov.NTSFormat = "0"
Me.edEt_totprov.NTSForzaVisZoom = false
Me.edEt_totprov.NTSOldValue = ""
Me.edEt_totprov.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totprov.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totprov.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totprov.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totprov.Properties.AutoHeight = false
Me.edEt_totprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totprov.Properties.MaxLength = 65536
Me.edEt_totprov.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totprov.Size = New System.Drawing.Size(84, 20)
Me.edEt_totprov.TabIndex = 55
'
'lbEt_totprov
'
Me.lbEt_totprov.AutoSize = true
Me.lbEt_totprov.BackColor = System.Drawing.Color.Transparent
Me.lbEt_totprov.Location = New System.Drawing.Point(4, 48)
Me.lbEt_totprov.Name = "lbEt_totprov"
Me.lbEt_totprov.NTSDbField = ""
Me.lbEt_totprov.Size = New System.Drawing.Size(100, 13)
Me.lbEt_totprov.TabIndex = 54
Me.lbEt_totprov.Text = "Provvigione agente"
Me.lbEt_totprov.Tooltip = ""
Me.lbEt_totprov.UseMnemonic = false
'
'edEt_impprov
'
Me.edEt_impprov.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_impprov.EditValue = "0"
Me.edEt_impprov.Enabled = false
Me.edEt_impprov.Location = New System.Drawing.Point(122, 22)
Me.edEt_impprov.Name = "edEt_impprov"
Me.edEt_impprov.NTSDbField = ""
Me.edEt_impprov.NTSFormat = "0"
Me.edEt_impprov.NTSForzaVisZoom = false
Me.edEt_impprov.NTSOldValue = ""
Me.edEt_impprov.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_impprov.Properties.Appearance.Options.UseBackColor = true
Me.edEt_impprov.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_impprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_impprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_impprov.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_impprov.Properties.AutoHeight = false
Me.edEt_impprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_impprov.Properties.MaxLength = 65536
Me.edEt_impprov.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_impprov.Size = New System.Drawing.Size(84, 20)
Me.edEt_impprov.TabIndex = 53
'
'lbEt_impprov
'
Me.lbEt_impprov.AutoSize = true
Me.lbEt_impprov.BackColor = System.Drawing.Color.Transparent
Me.lbEt_impprov.Location = New System.Drawing.Point(4, 25)
Me.lbEt_impprov.Name = "lbEt_impprov"
Me.lbEt_impprov.NTSDbField = ""
Me.lbEt_impprov.Size = New System.Drawing.Size(55, 13)
Me.lbEt_impprov.TabIndex = 52
Me.lbEt_impprov.Text = "Imponibile"
Me.lbEt_impprov.Tooltip = ""
Me.lbEt_impprov.UseMnemonic = false
'
'cmdModifScad
'
Me.cmdModifScad.ImagePath = ""
Me.cmdModifScad.ImageText = ""
Me.cmdModifScad.Location = New System.Drawing.Point(451, 23)
Me.cmdModifScad.Name = "cmdModifScad"
Me.cmdModifScad.NTSContextMenu = Nothing
Me.cmdModifScad.Size = New System.Drawing.Size(113, 20)
Me.cmdModifScad.TabIndex = 81
Me.cmdModifScad.Text = "Modifica scad."
'
'edEt_vettor
'
Me.edEt_vettor.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_vettor.EditValue = "0"
Me.edEt_vettor.Location = New System.Drawing.Point(133, 83)
Me.edEt_vettor.Name = "edEt_vettor"
Me.edEt_vettor.NTSDbField = ""
Me.edEt_vettor.NTSFormat = "0"
Me.edEt_vettor.NTSForzaVisZoom = false
Me.edEt_vettor.NTSOldValue = ""
Me.edEt_vettor.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_vettor.Properties.Appearance.Options.UseBackColor = true
Me.edEt_vettor.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_vettor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_vettor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_vettor.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_vettor.Properties.AutoHeight = false
Me.edEt_vettor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_vettor.Properties.MaxLength = 65536
Me.edEt_vettor.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_vettor.Size = New System.Drawing.Size(53, 20)
Me.edEt_vettor.TabIndex = 113
'
'cmdUltSpese
'
Me.cmdUltSpese.ImagePath = ""
Me.cmdUltSpese.ImageText = ""
Me.cmdUltSpese.Location = New System.Drawing.Point(451, 2)
Me.cmdUltSpese.Name = "cmdUltSpese"
Me.cmdUltSpese.NTSContextMenu = Nothing
Me.cmdUltSpese.Size = New System.Drawing.Size(113, 20)
Me.cmdUltSpese.TabIndex = 82
Me.cmdUltSpese.Text = "Ulteriori spese"
'
'lbEt_vettor
'
Me.lbEt_vettor.AutoSize = true
Me.lbEt_vettor.BackColor = System.Drawing.Color.Transparent
Me.lbEt_vettor.Location = New System.Drawing.Point(11, 86)
Me.lbEt_vettor.Name = "lbEt_vettor"
Me.lbEt_vettor.NTSDbField = ""
Me.lbEt_vettor.Size = New System.Drawing.Size(43, 13)
Me.lbEt_vettor.TabIndex = 112
Me.lbEt_vettor.Text = "Vettore"
Me.lbEt_vettor.Tooltip = ""
Me.lbEt_vettor.UseMnemonic = false
'
'fmPartita
'
Me.fmPartita.AllowDrop = true
Me.fmPartita.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmPartita.Appearance.Options.UseBackColor = true
Me.fmPartita.Controls.Add(Me.lbEt_datpar)
Me.fmPartita.Controls.Add(Me.edEt_datpar)
Me.fmPartita.Controls.Add(Me.edEt_numpar)
Me.fmPartita.Controls.Add(Me.lbEt_numpar)
Me.fmPartita.Controls.Add(Me.edEt_alfpar)
Me.fmPartita.Controls.Add(Me.lbEt_annpar)
Me.fmPartita.Controls.Add(Me.edEt_annpar)
Me.fmPartita.Cursor = System.Windows.Forms.Cursors.Default
Me.fmPartita.Location = New System.Drawing.Point(6, 208)
Me.fmPartita.Name = "fmPartita"
Me.fmPartita.Size = New System.Drawing.Size(184, 93)
Me.fmPartita.TabIndex = 2
Me.fmPartita.Text = "Partita documento ricevuto"
'
'lbEt_datpar
'
Me.lbEt_datpar.AutoSize = true
Me.lbEt_datpar.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datpar.Location = New System.Drawing.Point(5, 70)
Me.lbEt_datpar.Name = "lbEt_datpar"
Me.lbEt_datpar.NTSDbField = ""
Me.lbEt_datpar.Size = New System.Drawing.Size(30, 13)
Me.lbEt_datpar.TabIndex = 74
Me.lbEt_datpar.Text = "Data"
Me.lbEt_datpar.Tooltip = ""
Me.lbEt_datpar.UseMnemonic = false
'
'edEt_datpar
'
Me.edEt_datpar.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_datpar.EditValue = "01/01/1900"
Me.edEt_datpar.Location = New System.Drawing.Point(90, 67)
Me.edEt_datpar.Name = "edEt_datpar"
Me.edEt_datpar.NTSDbField = ""
Me.edEt_datpar.NTSForzaVisZoom = false
Me.edEt_datpar.NTSOldValue = ""
Me.edEt_datpar.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_datpar.Properties.Appearance.Options.UseBackColor = true
Me.edEt_datpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_datpar.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_datpar.Properties.AutoHeight = false
Me.edEt_datpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_datpar.Properties.MaxLength = 65536
Me.edEt_datpar.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_datpar.Size = New System.Drawing.Size(89, 20)
Me.edEt_datpar.TabIndex = 73
'
'edEt_numpar
'
Me.edEt_numpar.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_numpar.EditValue = "0"
Me.edEt_numpar.Location = New System.Drawing.Point(90, 45)
Me.edEt_numpar.Name = "edEt_numpar"
Me.edEt_numpar.NTSDbField = ""
Me.edEt_numpar.NTSFormat = "0"
Me.edEt_numpar.NTSForzaVisZoom = false
Me.edEt_numpar.NTSOldValue = ""
Me.edEt_numpar.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_numpar.Properties.Appearance.Options.UseBackColor = true
Me.edEt_numpar.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_numpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_numpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_numpar.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_numpar.Properties.AutoHeight = false
Me.edEt_numpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_numpar.Properties.MaxLength = 65536
Me.edEt_numpar.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_numpar.Size = New System.Drawing.Size(89, 20)
Me.edEt_numpar.TabIndex = 72
'
'lbEt_numpar
'
Me.lbEt_numpar.AutoSize = true
Me.lbEt_numpar.BackColor = System.Drawing.Color.Transparent
Me.lbEt_numpar.Location = New System.Drawing.Point(5, 48)
Me.lbEt_numpar.Name = "lbEt_numpar"
Me.lbEt_numpar.NTSDbField = ""
Me.lbEt_numpar.Size = New System.Drawing.Size(44, 13)
Me.lbEt_numpar.TabIndex = 71
Me.lbEt_numpar.Text = "Numero"
Me.lbEt_numpar.Tooltip = ""
Me.lbEt_numpar.UseMnemonic = false
'
'edEt_alfpar
'
Me.edEt_alfpar.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_alfpar.EditValue = ""
Me.edEt_alfpar.Location = New System.Drawing.Point(134, 22)
Me.edEt_alfpar.Name = "edEt_alfpar"
Me.edEt_alfpar.NTSDbField = ""
Me.edEt_alfpar.NTSForzaVisZoom = false
Me.edEt_alfpar.NTSOldValue = ""
Me.edEt_alfpar.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_alfpar.Properties.Appearance.Options.UseBackColor = true
Me.edEt_alfpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_alfpar.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_alfpar.Properties.AutoHeight = false
Me.edEt_alfpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_alfpar.Properties.MaxLength = 65536
Me.edEt_alfpar.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_alfpar.Size = New System.Drawing.Size(45, 20)
Me.edEt_alfpar.TabIndex = 70
'
'lbEt_annpar
'
Me.lbEt_annpar.AutoSize = true
Me.lbEt_annpar.BackColor = System.Drawing.Color.Transparent
Me.lbEt_annpar.Location = New System.Drawing.Point(5, 25)
Me.lbEt_annpar.Name = "lbEt_annpar"
Me.lbEt_annpar.NTSDbField = ""
Me.lbEt_annpar.Size = New System.Drawing.Size(59, 13)
Me.lbEt_annpar.TabIndex = 68
Me.lbEt_annpar.Text = "Anno/serie"
Me.lbEt_annpar.Tooltip = ""
Me.lbEt_annpar.UseMnemonic = false
'
'edEt_annpar
'
Me.edEt_annpar.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_annpar.EditValue = "0"
Me.edEt_annpar.Location = New System.Drawing.Point(90, 22)
Me.edEt_annpar.Name = "edEt_annpar"
Me.edEt_annpar.NTSDbField = ""
Me.edEt_annpar.NTSFormat = "0"
Me.edEt_annpar.NTSForzaVisZoom = false
Me.edEt_annpar.NTSOldValue = ""
Me.edEt_annpar.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_annpar.Properties.Appearance.Options.UseBackColor = true
Me.edEt_annpar.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_annpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_annpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_annpar.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_annpar.Properties.AutoHeight = false
Me.edEt_annpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_annpar.Properties.MaxLength = 65536
Me.edEt_annpar.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_annpar.Size = New System.Drawing.Size(42, 20)
Me.edEt_annpar.TabIndex = 43
'
'lbXx_codcfam
'
Me.lbXx_codcfam.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codcfam.Location = New System.Drawing.Point(186, 143)
Me.lbXx_codcfam.Name = "lbXx_codcfam"
Me.lbXx_codcfam.NTSDbField = ""
Me.lbXx_codcfam.Size = New System.Drawing.Size(318, 20)
Me.lbXx_codcfam.TabIndex = 82
Me.lbXx_codcfam.Tooltip = ""
Me.lbXx_codcfam.UseMnemonic = false
'
'fmInizioTrasporto
'
Me.fmInizioTrasporto.AllowDrop = true
Me.fmInizioTrasporto.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmInizioTrasporto.Appearance.Options.UseBackColor = true
Me.fmInizioTrasporto.Controls.Add(Me.edEt_oriniz)
Me.fmInizioTrasporto.Controls.Add(Me.edEt_dtiniz)
Me.fmInizioTrasporto.Controls.Add(Me.lbEt_oriniz)
Me.fmInizioTrasporto.Controls.Add(Me.lbEt_dtiniz)
Me.fmInizioTrasporto.Cursor = System.Windows.Forms.Cursors.Default
Me.fmInizioTrasporto.Location = New System.Drawing.Point(411, 232)
Me.fmInizioTrasporto.Name = "fmInizioTrasporto"
Me.fmInizioTrasporto.Size = New System.Drawing.Size(153, 69)
Me.fmInizioTrasporto.TabIndex = 111
Me.fmInizioTrasporto.Text = "Inizio trasporto"
'
'edEt_oriniz
'
Me.edEt_oriniz.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_oriniz.EditValue = "0"
Me.edEt_oriniz.Location = New System.Drawing.Point(66, 44)
Me.edEt_oriniz.Name = "edEt_oriniz"
Me.edEt_oriniz.NTSDbField = ""
Me.edEt_oriniz.NTSFormat = "0"
Me.edEt_oriniz.NTSForzaVisZoom = false
Me.edEt_oriniz.NTSOldValue = ""
Me.edEt_oriniz.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_oriniz.Properties.Appearance.Options.UseBackColor = true
Me.edEt_oriniz.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_oriniz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_oriniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_oriniz.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_oriniz.Properties.AutoHeight = false
Me.edEt_oriniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_oriniz.Properties.MaxLength = 65536
Me.edEt_oriniz.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_oriniz.Size = New System.Drawing.Size(51, 20)
Me.edEt_oriniz.TabIndex = 91
'
'edEt_dtiniz
'
Me.edEt_dtiniz.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_dtiniz.EditValue = "01/01/2008"
Me.edEt_dtiniz.Location = New System.Drawing.Point(66, 22)
Me.edEt_dtiniz.Name = "edEt_dtiniz"
Me.edEt_dtiniz.NTSDbField = ""
Me.edEt_dtiniz.NTSForzaVisZoom = false
Me.edEt_dtiniz.NTSOldValue = ""
Me.edEt_dtiniz.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_dtiniz.Properties.Appearance.Options.UseBackColor = true
Me.edEt_dtiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_dtiniz.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_dtiniz.Properties.AutoHeight = false
Me.edEt_dtiniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_dtiniz.Properties.MaxLength = 65536
Me.edEt_dtiniz.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_dtiniz.Size = New System.Drawing.Size(80, 20)
Me.edEt_dtiniz.TabIndex = 90
'
'lbEt_oriniz
'
Me.lbEt_oriniz.AutoSize = true
Me.lbEt_oriniz.BackColor = System.Drawing.Color.Transparent
Me.lbEt_oriniz.Location = New System.Drawing.Point(9, 48)
Me.lbEt_oriniz.Name = "lbEt_oriniz"
Me.lbEt_oriniz.NTSDbField = ""
Me.lbEt_oriniz.Size = New System.Drawing.Size(25, 13)
Me.lbEt_oriniz.TabIndex = 89
Me.lbEt_oriniz.Text = "Ora"
Me.lbEt_oriniz.Tooltip = ""
Me.lbEt_oriniz.UseMnemonic = false
'
'lbEt_dtiniz
'
Me.lbEt_dtiniz.AutoSize = true
Me.lbEt_dtiniz.BackColor = System.Drawing.Color.Transparent
Me.lbEt_dtiniz.Location = New System.Drawing.Point(7, 25)
Me.lbEt_dtiniz.Name = "lbEt_dtiniz"
Me.lbEt_dtiniz.NTSDbField = ""
Me.lbEt_dtiniz.Size = New System.Drawing.Size(52, 13)
Me.lbEt_dtiniz.TabIndex = 88
Me.lbEt_dtiniz.Text = "Data/Ora"
Me.lbEt_dtiniz.Tooltip = ""
Me.lbEt_dtiniz.UseMnemonic = false
'
'lbEt_codcfam
'
Me.lbEt_codcfam.AutoSize = true
Me.lbEt_codcfam.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codcfam.Location = New System.Drawing.Point(11, 146)
Me.lbEt_codcfam.Name = "lbEt_codcfam"
Me.lbEt_codcfam.NTSDbField = ""
Me.lbEt_codcfam.Size = New System.Drawing.Size(45, 13)
Me.lbEt_codcfam.TabIndex = 80
Me.lbEt_codcfam.Text = "Famiglia"
Me.lbEt_codcfam.Tooltip = ""
Me.lbEt_codcfam.UseMnemonic = false
'
'edEt_codcfam
'
Me.edEt_codcfam.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codcfam.EditValue = " "
Me.edEt_codcfam.Location = New System.Drawing.Point(133, 143)
Me.edEt_codcfam.Name = "edEt_codcfam"
Me.edEt_codcfam.NTSDbField = ""
Me.edEt_codcfam.NTSForzaVisZoom = false
Me.edEt_codcfam.NTSOldValue = " "
Me.edEt_codcfam.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codcfam.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codcfam.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codcfam.Properties.AutoHeight = false
Me.edEt_codcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codcfam.Properties.MaxLength = 65536
Me.edEt_codcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codcfam.Size = New System.Drawing.Size(52, 20)
Me.edEt_codcfam.TabIndex = 81
'
'edEt_caustra
'
Me.edEt_caustra.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_caustra.EditValue = "0"
Me.edEt_caustra.Location = New System.Drawing.Point(133, 3)
Me.edEt_caustra.Name = "edEt_caustra"
Me.edEt_caustra.NTSDbField = ""
Me.edEt_caustra.NTSFormat = "0"
Me.edEt_caustra.NTSForzaVisZoom = false
Me.edEt_caustra.NTSOldValue = ""
Me.edEt_caustra.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_caustra.Properties.Appearance.Options.UseBackColor = true
Me.edEt_caustra.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_caustra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_caustra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_caustra.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_caustra.Properties.AutoHeight = false
Me.edEt_caustra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_caustra.Properties.MaxLength = 65536
Me.edEt_caustra.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_caustra.Size = New System.Drawing.Size(52, 20)
Me.edEt_caustra.TabIndex = 51
'
'lbEt_acuradi
'
Me.lbEt_acuradi.AutoSize = true
Me.lbEt_acuradi.BackColor = System.Drawing.Color.Transparent
Me.lbEt_acuradi.Location = New System.Drawing.Point(11, 46)
Me.lbEt_acuradi.Name = "lbEt_acuradi"
Me.lbEt_acuradi.NTSDbField = ""
Me.lbEt_acuradi.Size = New System.Drawing.Size(104, 13)
Me.lbEt_acuradi.TabIndex = 73
Me.lbEt_acuradi.Text = "Trasporto a cura del"
Me.lbEt_acuradi.Tooltip = ""
Me.lbEt_acuradi.UseMnemonic = false
'
'lbEt_caustra
'
Me.lbEt_caustra.AutoSize = true
Me.lbEt_caustra.BackColor = System.Drawing.Color.Transparent
Me.lbEt_caustra.Location = New System.Drawing.Point(13, 6)
Me.lbEt_caustra.Name = "lbEt_caustra"
Me.lbEt_caustra.NTSDbField = ""
Me.lbEt_caustra.Size = New System.Drawing.Size(45, 13)
Me.lbEt_caustra.TabIndex = 50
Me.lbEt_caustra.Text = "Causale"
Me.lbEt_caustra.Tooltip = ""
Me.lbEt_caustra.UseMnemonic = false
'
'edEt_aspetto
'
Me.edEt_aspetto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_aspetto.EditValue = ""
Me.edEt_aspetto.Location = New System.Drawing.Point(187, 23)
Me.edEt_aspetto.Name = "edEt_aspetto"
Me.edEt_aspetto.NTSDbField = ""
Me.edEt_aspetto.NTSForzaVisZoom = false
Me.edEt_aspetto.NTSOldValue = ""
Me.edEt_aspetto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_aspetto.Properties.Appearance.Options.UseBackColor = true
Me.edEt_aspetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_aspetto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_aspetto.Properties.AutoHeight = false
Me.edEt_aspetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_aspetto.Properties.MaxLength = 65536
Me.edEt_aspetto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_aspetto.Size = New System.Drawing.Size(263, 20)
Me.edEt_aspetto.TabIndex = 72
'
'lbXx_caustra
'
Me.lbXx_caustra.BackColor = System.Drawing.Color.Transparent
Me.lbXx_caustra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_caustra.Location = New System.Drawing.Point(186, 3)
Me.lbXx_caustra.Name = "lbXx_caustra"
Me.lbXx_caustra.NTSDbField = ""
Me.lbXx_caustra.Size = New System.Drawing.Size(264, 20)
Me.lbXx_caustra.TabIndex = 52
Me.lbXx_caustra.Tooltip = ""
Me.lbXx_caustra.UseMnemonic = false
'
'lbEt_codaspe
'
Me.lbEt_codaspe.AutoSize = true
Me.lbEt_codaspe.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codaspe.Location = New System.Drawing.Point(11, 26)
Me.lbEt_codaspe.Name = "lbEt_codaspe"
Me.lbEt_codaspe.NTSDbField = ""
Me.lbEt_codaspe.Size = New System.Drawing.Size(85, 13)
Me.lbEt_codaspe.TabIndex = 53
Me.lbEt_codaspe.Text = "Aspetto dei beni"
Me.lbEt_codaspe.Tooltip = ""
Me.lbEt_codaspe.UseMnemonic = false
'
'edEt_codaspe
'
Me.edEt_codaspe.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codaspe.EditValue = "0"
Me.edEt_codaspe.Location = New System.Drawing.Point(133, 23)
Me.edEt_codaspe.Name = "edEt_codaspe"
Me.edEt_codaspe.NTSDbField = ""
Me.edEt_codaspe.NTSFormat = "0"
Me.edEt_codaspe.NTSForzaVisZoom = false
Me.edEt_codaspe.NTSOldValue = ""
Me.edEt_codaspe.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codaspe.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codaspe.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codaspe.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codaspe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codaspe.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codaspe.Properties.AutoHeight = false
Me.edEt_codaspe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codaspe.Properties.MaxLength = 65536
Me.edEt_codaspe.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codaspe.Size = New System.Drawing.Size(52, 20)
Me.edEt_codaspe.TabIndex = 54
'
'lbEt_coddest2
'
Me.lbEt_coddest2.AutoSize = true
Me.lbEt_coddest2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_coddest2.Location = New System.Drawing.Point(11, 66)
Me.lbEt_coddest2.Name = "lbEt_coddest2"
Me.lbEt_coddest2.NTSDbField = ""
Me.lbEt_coddest2.Size = New System.Drawing.Size(115, 13)
Me.lbEt_coddest2.TabIndex = 56
Me.lbEt_coddest2.Text = "Destinazione diversa 2"
Me.lbEt_coddest2.Tooltip = ""
Me.lbEt_coddest2.UseMnemonic = false
'
'edEt_coddest2
'
Me.edEt_coddest2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_coddest2.EditValue = "0"
Me.edEt_coddest2.Location = New System.Drawing.Point(133, 63)
Me.edEt_coddest2.Name = "edEt_coddest2"
Me.edEt_coddest2.NTSDbField = ""
Me.edEt_coddest2.NTSFormat = "0"
Me.edEt_coddest2.NTSForzaVisZoom = false
Me.edEt_coddest2.NTSOldValue = ""
Me.edEt_coddest2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_coddest2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_coddest2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_coddest2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_coddest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_coddest2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_coddest2.Properties.AutoHeight = false
Me.edEt_coddest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_coddest2.Properties.MaxLength = 65536
Me.edEt_coddest2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_coddest2.Size = New System.Drawing.Size(52, 20)
Me.edEt_coddest2.TabIndex = 57
'
'lbXx_coddest2
'
Me.lbXx_coddest2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_coddest2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_coddest2.Location = New System.Drawing.Point(186, 63)
Me.lbXx_coddest2.Name = "lbXx_coddest2"
Me.lbXx_coddest2.NTSDbField = ""
Me.lbXx_coddest2.Size = New System.Drawing.Size(319, 20)
Me.lbXx_coddest2.TabIndex = 58
Me.lbXx_coddest2.Tooltip = ""
Me.lbXx_coddest2.UseMnemonic = false
'
'lbXx_porto
'
Me.lbXx_porto.BackColor = System.Drawing.Color.Transparent
Me.lbXx_porto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_porto.Location = New System.Drawing.Point(186, 123)
Me.lbXx_porto.Name = "lbXx_porto"
Me.lbXx_porto.NTSDbField = ""
Me.lbXx_porto.Size = New System.Drawing.Size(319, 20)
Me.lbXx_porto.TabIndex = 66
Me.lbXx_porto.Tooltip = ""
Me.lbXx_porto.UseMnemonic = false
'
'lbEt_vettor2
'
Me.lbEt_vettor2.AutoSize = true
Me.lbEt_vettor2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_vettor2.Location = New System.Drawing.Point(11, 106)
Me.lbEt_vettor2.Name = "lbEt_vettor2"
Me.lbEt_vettor2.NTSDbField = ""
Me.lbEt_vettor2.Size = New System.Drawing.Size(52, 13)
Me.lbEt_vettor2.TabIndex = 59
Me.lbEt_vettor2.Text = "Vettore 2"
Me.lbEt_vettor2.Tooltip = ""
Me.lbEt_vettor2.UseMnemonic = false
'
'edEt_porto
'
Me.edEt_porto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_porto.EditValue = ""
Me.edEt_porto.Location = New System.Drawing.Point(133, 123)
Me.edEt_porto.Name = "edEt_porto"
Me.edEt_porto.NTSDbField = ""
Me.edEt_porto.NTSForzaVisZoom = false
Me.edEt_porto.NTSOldValue = ""
Me.edEt_porto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_porto.Properties.Appearance.Options.UseBackColor = true
Me.edEt_porto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_porto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_porto.Properties.AutoHeight = false
Me.edEt_porto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_porto.Properties.MaxLength = 65536
Me.edEt_porto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_porto.Size = New System.Drawing.Size(52, 20)
Me.edEt_porto.TabIndex = 65
'
'edEt_vettor2
'
Me.edEt_vettor2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_vettor2.EditValue = "0"
Me.edEt_vettor2.Location = New System.Drawing.Point(133, 103)
Me.edEt_vettor2.Name = "edEt_vettor2"
Me.edEt_vettor2.NTSDbField = ""
Me.edEt_vettor2.NTSFormat = "0"
Me.edEt_vettor2.NTSForzaVisZoom = false
Me.edEt_vettor2.NTSOldValue = ""
Me.edEt_vettor2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_vettor2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_vettor2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_vettor2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_vettor2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_vettor2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_vettor2.Properties.AutoHeight = false
Me.edEt_vettor2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_vettor2.Properties.MaxLength = 65536
Me.edEt_vettor2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_vettor2.Size = New System.Drawing.Size(52, 20)
Me.edEt_vettor2.TabIndex = 60
'
'lbEt_porto
'
Me.lbEt_porto.AutoSize = true
Me.lbEt_porto.BackColor = System.Drawing.Color.Transparent
Me.lbEt_porto.Location = New System.Drawing.Point(11, 126)
Me.lbEt_porto.Name = "lbEt_porto"
Me.lbEt_porto.NTSDbField = ""
Me.lbEt_porto.Size = New System.Drawing.Size(33, 13)
Me.lbEt_porto.TabIndex = 64
Me.lbEt_porto.Text = "Porto"
Me.lbEt_porto.Tooltip = ""
Me.lbEt_porto.UseMnemonic = false
'
'lbXx_vettor2
'
Me.lbXx_vettor2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_vettor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_vettor2.Location = New System.Drawing.Point(186, 103)
Me.lbXx_vettor2.Name = "lbXx_vettor2"
Me.lbXx_vettor2.NTSDbField = ""
Me.lbXx_vettor2.Size = New System.Drawing.Size(318, 20)
Me.lbXx_vettor2.TabIndex = 61
Me.lbXx_vettor2.Tooltip = ""
Me.lbXx_vettor2.UseMnemonic = false
'
'cbEt_acuradi
'
Me.cbEt_acuradi.Cursor = System.Windows.Forms.Cursors.Default
Me.cbEt_acuradi.DataSource = Nothing
Me.cbEt_acuradi.DisplayMember = ""
Me.cbEt_acuradi.Location = New System.Drawing.Point(133, 43)
Me.cbEt_acuradi.Name = "cbEt_acuradi"
Me.cbEt_acuradi.NTSDbField = ""
Me.cbEt_acuradi.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.cbEt_acuradi.Properties.Appearance.Options.UseBackColor = true
Me.cbEt_acuradi.Properties.AutoHeight = false
Me.cbEt_acuradi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbEt_acuradi.Properties.DropDownRows = 30
Me.cbEt_acuradi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbEt_acuradi.SelectedValue = ""
Me.cbEt_acuradi.Size = New System.Drawing.Size(213, 20)
Me.cbEt_acuradi.TabIndex = 63
Me.cbEt_acuradi.ValueMember = ""
'
'pnPiedeTotali
'
Me.pnPiedeTotali.AllowDrop = true
Me.pnPiedeTotali.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeTotali.Appearance.Options.UseBackColor = true
Me.pnPiedeTotali.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeTotali.Controls.Add(Me.lbPesoLordo)
Me.pnPiedeTotali.Controls.Add(Me.lbEur1)
Me.pnPiedeTotali.Controls.Add(Me.lbValuta1)
Me.pnPiedeTotali.Controls.Add(Me.ckEt_flscdb)
Me.pnPiedeTotali.Controls.Add(Me.lbResto)
Me.pnPiedeTotali.Controls.Add(Me.lbValuta)
Me.pnPiedeTotali.Controls.Add(Me.edEt_resto)
Me.pnPiedeTotali.Controls.Add(Me.lbTrasporto)
Me.pnPiedeTotali.Controls.Add(Me.lbEt_pagato2)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speacc)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pagato2)
Me.pnPiedeTotali.Controls.Add(Me.lbEur)
Me.pnPiedeTotali.Controls.Add(Me.lbImballo)
Me.pnPiedeTotali.Controls.Add(Me.lbRiga2)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speimb)
Me.pnPiedeTotali.Controls.Add(Me.edTotalev)
Me.pnPiedeTotali.Controls.Add(Me.lbXx_totquant)
Me.pnPiedeTotali.Controls.Add(Me.lbRiga1)
Me.pnPiedeTotali.Controls.Add(Me.edTotale)
Me.pnPiedeTotali.Controls.Add(Me.lbIncasso)
Me.pnPiedeTotali.Controls.Add(Me.lbTotale)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speinc)
Me.pnPiedeTotali.Controls.Add(Me.edResiduo)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pesonetto)
Me.pnPiedeTotali.Controls.Add(Me.lbBolli)
Me.pnPiedeTotali.Controls.Add(Me.lbRimanenza)
Me.pnPiedeTotali.Controls.Add(Me.lbPesoNetto)
Me.pnPiedeTotali.Controls.Add(Me.edEt_bolli)
Me.pnPiedeTotali.Controls.Add(Me.edResiduov)
Me.pnPiedeTotali.Controls.Add(Me.edEt_peso)
Me.pnPiedeTotali.Controls.Add(Me.edTotIvav)
Me.pnPiedeTotali.Controls.Add(Me.edEt_abbuono)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totcoll)
Me.pnPiedeTotali.Controls.Add(Me.lbNetto)
Me.pnPiedeTotali.Controls.Add(Me.lbEt_totcoll)
Me.pnPiedeTotali.Controls.Add(Me.lbAbbuono)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totmerce)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pagato)
Me.pnPiedeTotali.Controls.Add(Me.edTotImponibv)
Me.pnPiedeTotali.Controls.Add(Me.lbIncassato)
Me.pnPiedeTotali.Controls.Add(Me.lbSpese)
Me.pnPiedeTotali.Controls.Add(Me.edEt_abbuonov)
Me.pnPiedeTotali.Controls.Add(Me.edTotSpesev)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totomag)
Me.pnPiedeTotali.Controls.Add(Me.edTotSpese)
Me.pnPiedeTotali.Controls.Add(Me.lbOmaggi)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totmercev)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totdoc)
Me.pnPiedeTotali.Controls.Add(Me.lbImponibile)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pagatov)
Me.pnPiedeTotali.Controls.Add(Me.edTotImponib)
Me.pnPiedeTotali.Controls.Add(Me.lbDocumento)
Me.pnPiedeTotali.Controls.Add(Me.edEt_bolliv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totomagv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speaccv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totdocv)
Me.pnPiedeTotali.Controls.Add(Me.lbImposta)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speincv)
Me.pnPiedeTotali.Controls.Add(Me.edTotIva)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speimbv)
Me.pnPiedeTotali.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeTotali.Location = New System.Drawing.Point(0, 0)
Me.pnPiedeTotali.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeTotali.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeTotali.Name = "pnPiedeTotali"
Me.pnPiedeTotali.NTSActiveTrasparency = true
Me.pnPiedeTotali.Size = New System.Drawing.Size(567, 269)
Me.pnPiedeTotali.TabIndex = 131
Me.pnPiedeTotali.Text = "NtsPanel1"
'
'lbPesoLordo
'
Me.lbPesoLordo.AutoSize = true
Me.lbPesoLordo.BackColor = System.Drawing.Color.Transparent
Me.lbPesoLordo.Location = New System.Drawing.Point(6, 246)
Me.lbPesoLordo.Name = "lbPesoLordo"
Me.lbPesoLordo.NTSDbField = ""
Me.lbPesoLordo.Size = New System.Drawing.Size(57, 13)
Me.lbPesoLordo.TabIndex = 109
Me.lbPesoLordo.Text = "Peso lordo"
Me.lbPesoLordo.Tooltip = ""
Me.lbPesoLordo.UseMnemonic = false
'
'lbEur1
'
Me.lbEur1.AutoSize = true
Me.lbEur1.BackColor = System.Drawing.Color.Transparent
Me.lbEur1.Location = New System.Drawing.Point(448, 7)
Me.lbEur1.Name = "lbEur1"
Me.lbEur1.NTSDbField = ""
Me.lbEur1.Size = New System.Drawing.Size(27, 13)
Me.lbEur1.TabIndex = 108
Me.lbEur1.Text = "EUR"
Me.lbEur1.Tooltip = ""
Me.lbEur1.UseMnemonic = false
'
'lbValuta1
'
Me.lbValuta1.AutoSize = true
Me.lbValuta1.BackColor = System.Drawing.Color.Transparent
Me.lbValuta1.Location = New System.Drawing.Point(359, 7)
Me.lbValuta1.Name = "lbValuta1"
Me.lbValuta1.NTSDbField = ""
Me.lbValuta1.Size = New System.Drawing.Size(37, 13)
Me.lbValuta1.TabIndex = 107
Me.lbValuta1.Text = "Valuta"
Me.lbValuta1.Tooltip = ""
Me.lbValuta1.UseMnemonic = false
'
'ckEt_flscdb
'
Me.ckEt_flscdb.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flscdb.Enabled = false
Me.ckEt_flscdb.Location = New System.Drawing.Point(340, 240)
Me.ckEt_flscdb.Name = "ckEt_flscdb"
Me.ckEt_flscdb.NTSCheckValue = "S"
Me.ckEt_flscdb.NTSUnCheckValue = "N"
Me.ckEt_flscdb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flscdb.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flscdb.Properties.AutoHeight = false
Me.ckEt_flscdb.Properties.Caption = "Sblocca castelletti"
Me.ckEt_flscdb.Size = New System.Drawing.Size(133, 19)
Me.ckEt_flscdb.TabIndex = 102
'
'lbResto
'
Me.lbResto.AutoSize = true
Me.lbResto.BackColor = System.Drawing.Color.Transparent
Me.lbResto.Location = New System.Drawing.Point(268, 130)
Me.lbResto.Name = "lbResto"
Me.lbResto.NTSDbField = ""
Me.lbResto.Size = New System.Drawing.Size(35, 13)
Me.lbResto.TabIndex = 106
Me.lbResto.Text = "Resto"
Me.lbResto.Tooltip = ""
Me.lbResto.UseMnemonic = false
'
'lbValuta
'
Me.lbValuta.AutoSize = true
Me.lbValuta.BackColor = System.Drawing.Color.Transparent
Me.lbValuta.Location = New System.Drawing.Point(102, 7)
Me.lbValuta.Name = "lbValuta"
Me.lbValuta.NTSDbField = ""
Me.lbValuta.Size = New System.Drawing.Size(37, 13)
Me.lbValuta.TabIndex = 57
Me.lbValuta.Text = "Valuta"
Me.lbValuta.Tooltip = ""
Me.lbValuta.UseMnemonic = false
'
'edEt_resto
'
Me.edEt_resto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_resto.EditValue = "0"
Me.edEt_resto.Enabled = false
Me.edEt_resto.Location = New System.Drawing.Point(422, 127)
Me.edEt_resto.Name = "edEt_resto"
Me.edEt_resto.NTSDbField = ""
Me.edEt_resto.NTSFormat = "0"
Me.edEt_resto.NTSForzaVisZoom = false
Me.edEt_resto.NTSOldValue = ""
Me.edEt_resto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_resto.Properties.Appearance.Options.UseBackColor = true
Me.edEt_resto.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_resto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_resto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_resto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_resto.Properties.AutoHeight = false
Me.edEt_resto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_resto.Properties.MaxLength = 65536
Me.edEt_resto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_resto.Size = New System.Drawing.Size(77, 20)
Me.edEt_resto.TabIndex = 105
'
'lbTrasporto
'
Me.lbTrasporto.AutoSize = true
Me.lbTrasporto.BackColor = System.Drawing.Color.Transparent
Me.lbTrasporto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbTrasporto.Location = New System.Drawing.Point(6, 23)
Me.lbTrasporto.Name = "lbTrasporto"
Me.lbTrasporto.NTSDbField = ""
Me.lbTrasporto.Size = New System.Drawing.Size(52, 13)
Me.lbTrasporto.TabIndex = 54
Me.lbTrasporto.Text = "Trasporto"
Me.lbTrasporto.Tooltip = ""
Me.lbTrasporto.UseMnemonic = false
'
'lbEt_pagato2
'
Me.lbEt_pagato2.AutoSize = true
Me.lbEt_pagato2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_pagato2.Location = New System.Drawing.Point(268, 110)
Me.lbEt_pagato2.Name = "lbEt_pagato2"
Me.lbEt_pagato2.NTSDbField = ""
Me.lbEt_pagato2.Size = New System.Drawing.Size(68, 13)
Me.lbEt_pagato2.TabIndex = 104
Me.lbEt_pagato2.Text = "Inc. contanti"
Me.lbEt_pagato2.Tooltip = ""
Me.lbEt_pagato2.UseMnemonic = false
'
'edEt_speacc
'
Me.edEt_speacc.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speacc.EditValue = "0"
Me.edEt_speacc.Location = New System.Drawing.Point(160, 20)
Me.edEt_speacc.Name = "edEt_speacc"
Me.edEt_speacc.NTSDbField = ""
Me.edEt_speacc.NTSFormat = "0"
Me.edEt_speacc.NTSForzaVisZoom = false
Me.edEt_speacc.NTSOldValue = ""
Me.edEt_speacc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speacc.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speacc.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speacc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speacc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speacc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speacc.Properties.AutoHeight = false
Me.edEt_speacc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speacc.Properties.MaxLength = 65536
Me.edEt_speacc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speacc.Size = New System.Drawing.Size(77, 20)
Me.edEt_speacc.TabIndex = 55
'
'edEt_pagato2
'
Me.edEt_pagato2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pagato2.EditValue = "0"
Me.edEt_pagato2.Enabled = false
Me.edEt_pagato2.Location = New System.Drawing.Point(422, 107)
Me.edEt_pagato2.Name = "edEt_pagato2"
Me.edEt_pagato2.NTSDbField = ""
Me.edEt_pagato2.NTSFormat = "0"
Me.edEt_pagato2.NTSForzaVisZoom = false
Me.edEt_pagato2.NTSOldValue = ""
Me.edEt_pagato2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_pagato2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_pagato2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_pagato2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_pagato2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_pagato2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_pagato2.Properties.AutoHeight = false
Me.edEt_pagato2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_pagato2.Properties.MaxLength = 65536
Me.edEt_pagato2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_pagato2.Size = New System.Drawing.Size(77, 20)
Me.edEt_pagato2.TabIndex = 103
'
'lbEur
'
Me.lbEur.AutoSize = true
Me.lbEur.BackColor = System.Drawing.Color.Transparent
Me.lbEur.Location = New System.Drawing.Point(186, 7)
Me.lbEur.Name = "lbEur"
Me.lbEur.NTSDbField = ""
Me.lbEur.Size = New System.Drawing.Size(27, 13)
Me.lbEur.TabIndex = 56
Me.lbEur.Text = "EUR"
Me.lbEur.Tooltip = ""
Me.lbEur.UseMnemonic = false
'
'lbImballo
'
Me.lbImballo.AutoSize = true
Me.lbImballo.BackColor = System.Drawing.Color.Transparent
Me.lbImballo.Location = New System.Drawing.Point(6, 43)
Me.lbImballo.Name = "lbImballo"
Me.lbImballo.NTSDbField = ""
Me.lbImballo.Size = New System.Drawing.Size(41, 13)
Me.lbImballo.TabIndex = 59
Me.lbImballo.Text = "Imballo"
Me.lbImballo.Tooltip = ""
Me.lbImballo.UseMnemonic = false
'
'lbRiga2
'
Me.lbRiga2.BackColor = System.Drawing.Color.Transparent
Me.lbRiga2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.lbRiga2.Location = New System.Drawing.Point(340, 63)
Me.lbRiga2.Name = "lbRiga2"
Me.lbRiga2.NTSDbField = ""
Me.lbRiga2.Size = New System.Drawing.Size(160, 1)
Me.lbRiga2.TabIndex = 101
Me.lbRiga2.Tooltip = ""
Me.lbRiga2.UseMnemonic = false
'
'edEt_speimb
'
Me.edEt_speimb.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speimb.EditValue = "0"
Me.edEt_speimb.Location = New System.Drawing.Point(160, 40)
Me.edEt_speimb.Name = "edEt_speimb"
Me.edEt_speimb.NTSDbField = ""
Me.edEt_speimb.NTSFormat = "0"
Me.edEt_speimb.NTSForzaVisZoom = false
Me.edEt_speimb.NTSOldValue = ""
Me.edEt_speimb.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speimb.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speimb.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speimb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speimb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speimb.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speimb.Properties.AutoHeight = false
Me.edEt_speimb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speimb.Properties.MaxLength = 65536
Me.edEt_speimb.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speimb.Size = New System.Drawing.Size(77, 20)
Me.edEt_speimb.TabIndex = 60
'
'edTotalev
'
Me.edTotalev.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotalev.EditValue = "0"
Me.edTotalev.Enabled = false
Me.edTotalev.Location = New System.Drawing.Point(340, 67)
Me.edTotalev.Name = "edTotalev"
Me.edTotalev.NTSDbField = ""
Me.edTotalev.NTSFormat = "0"
Me.edTotalev.NTSForzaVisZoom = false
Me.edTotalev.NTSOldValue = ""
Me.edTotalev.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotalev.Properties.Appearance.Options.UseBackColor = true
Me.edTotalev.Properties.Appearance.Options.UseTextOptions = true
Me.edTotalev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotalev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotalev.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotalev.Properties.AutoHeight = false
Me.edTotalev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotalev.Properties.MaxLength = 65536
Me.edTotalev.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotalev.Size = New System.Drawing.Size(77, 20)
Me.edTotalev.TabIndex = 97
'
'lbXx_totquant
'
Me.lbXx_totquant.BackColor = System.Drawing.Color.Transparent
Me.lbXx_totquant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_totquant.Location = New System.Drawing.Point(160, 203)
Me.lbXx_totquant.Name = "lbXx_totquant"
Me.lbXx_totquant.NTSDbField = ""
Me.lbXx_totquant.Size = New System.Drawing.Size(78, 19)
Me.lbXx_totquant.TabIndex = 74
Me.lbXx_totquant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
Me.lbXx_totquant.Tooltip = ""
Me.lbXx_totquant.UseMnemonic = false
'
'lbRiga1
'
Me.lbRiga1.BackColor = System.Drawing.Color.Transparent
Me.lbRiga1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.lbRiga1.Location = New System.Drawing.Point(78, 103)
Me.lbRiga1.Name = "lbRiga1"
Me.lbRiga1.NTSDbField = ""
Me.lbRiga1.Size = New System.Drawing.Size(160, 1)
Me.lbRiga1.TabIndex = 100
Me.lbRiga1.Tooltip = ""
Me.lbRiga1.UseMnemonic = false
'
'edTotale
'
Me.edTotale.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotale.EditValue = "0"
Me.edTotale.Enabled = false
Me.edTotale.Location = New System.Drawing.Point(422, 67)
Me.edTotale.Name = "edTotale"
Me.edTotale.NTSDbField = ""
Me.edTotale.NTSFormat = "0"
Me.edTotale.NTSForzaVisZoom = false
Me.edTotale.NTSOldValue = ""
Me.edTotale.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotale.Properties.Appearance.Options.UseBackColor = true
Me.edTotale.Properties.Appearance.Options.UseTextOptions = true
Me.edTotale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotale.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotale.Properties.AutoHeight = false
Me.edTotale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotale.Properties.MaxLength = 65536
Me.edTotale.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotale.Size = New System.Drawing.Size(77, 20)
Me.edTotale.TabIndex = 96
'
'lbIncasso
'
Me.lbIncasso.AutoSize = true
Me.lbIncasso.BackColor = System.Drawing.Color.Transparent
Me.lbIncasso.Location = New System.Drawing.Point(6, 63)
Me.lbIncasso.Name = "lbIncasso"
Me.lbIncasso.NTSDbField = ""
Me.lbIncasso.Size = New System.Drawing.Size(44, 13)
Me.lbIncasso.TabIndex = 62
Me.lbIncasso.Text = "Incasso"
Me.lbIncasso.Tooltip = ""
Me.lbIncasso.UseMnemonic = false
'
'lbTotale
'
Me.lbTotale.AutoSize = true
Me.lbTotale.BackColor = System.Drawing.Color.Transparent
Me.lbTotale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbTotale.Location = New System.Drawing.Point(268, 70)
Me.lbTotale.Name = "lbTotale"
Me.lbTotale.NTSDbField = ""
Me.lbTotale.Size = New System.Drawing.Size(43, 13)
Me.lbTotale.TabIndex = 95
Me.lbTotale.Text = "Totale"
Me.lbTotale.Tooltip = ""
Me.lbTotale.UseMnemonic = false
'
'edEt_speinc
'
Me.edEt_speinc.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speinc.EditValue = "0"
Me.edEt_speinc.Enabled = false
Me.edEt_speinc.Location = New System.Drawing.Point(160, 60)
Me.edEt_speinc.Name = "edEt_speinc"
Me.edEt_speinc.NTSDbField = ""
Me.edEt_speinc.NTSFormat = "0"
Me.edEt_speinc.NTSForzaVisZoom = false
Me.edEt_speinc.NTSOldValue = ""
Me.edEt_speinc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speinc.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speinc.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speinc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speinc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speinc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speinc.Properties.AutoHeight = false
Me.edEt_speinc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speinc.Properties.MaxLength = 65536
Me.edEt_speinc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speinc.Size = New System.Drawing.Size(77, 20)
Me.edEt_speinc.TabIndex = 63
'
'edResiduo
'
Me.edResiduo.Cursor = System.Windows.Forms.Cursors.Default
Me.edResiduo.EditValue = "0"
Me.edResiduo.Enabled = false
Me.edResiduo.Location = New System.Drawing.Point(422, 167)
Me.edResiduo.Name = "edResiduo"
Me.edResiduo.NTSDbField = ""
Me.edResiduo.NTSFormat = "0"
Me.edResiduo.NTSForzaVisZoom = false
Me.edResiduo.NTSOldValue = ""
Me.edResiduo.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edResiduo.Properties.Appearance.Options.UseBackColor = true
Me.edResiduo.Properties.Appearance.Options.UseTextOptions = true
Me.edResiduo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edResiduo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edResiduo.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edResiduo.Properties.AutoHeight = false
Me.edResiduo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edResiduo.Properties.MaxLength = 65536
Me.edResiduo.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edResiduo.Size = New System.Drawing.Size(77, 20)
Me.edResiduo.TabIndex = 93
'
'edEt_pesonetto
'
Me.edEt_pesonetto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pesonetto.EditValue = "0"
Me.edEt_pesonetto.Location = New System.Drawing.Point(78, 223)
Me.edEt_pesonetto.Name = "edEt_pesonetto"
Me.edEt_pesonetto.NTSDbField = ""
Me.edEt_pesonetto.NTSFormat = "0"
Me.edEt_pesonetto.NTSForzaVisZoom = false
Me.edEt_pesonetto.NTSOldValue = ""
Me.edEt_pesonetto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_pesonetto.Properties.Appearance.Options.UseBackColor = true
Me.edEt_pesonetto.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_pesonetto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_pesonetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_pesonetto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_pesonetto.Properties.AutoHeight = false
Me.edEt_pesonetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_pesonetto.Properties.MaxLength = 65536
Me.edEt_pesonetto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_pesonetto.Size = New System.Drawing.Size(60, 20)
Me.edEt_pesonetto.TabIndex = 71
'
'lbBolli
'
Me.lbBolli.AutoSize = true
Me.lbBolli.BackColor = System.Drawing.Color.Transparent
Me.lbBolli.Location = New System.Drawing.Point(6, 83)
Me.lbBolli.Name = "lbBolli"
Me.lbBolli.NTSDbField = ""
Me.lbBolli.Size = New System.Drawing.Size(25, 13)
Me.lbBolli.TabIndex = 65
Me.lbBolli.Text = "Bolli"
Me.lbBolli.Tooltip = ""
Me.lbBolli.UseMnemonic = false
'
'lbRimanenza
'
Me.lbRimanenza.AutoSize = true
Me.lbRimanenza.BackColor = System.Drawing.Color.Transparent
Me.lbRimanenza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbRimanenza.Location = New System.Drawing.Point(268, 170)
Me.lbRimanenza.Name = "lbRimanenza"
Me.lbRimanenza.NTSDbField = ""
Me.lbRimanenza.Size = New System.Drawing.Size(69, 13)
Me.lbRimanenza.TabIndex = 92
Me.lbRimanenza.Text = "Rimanenza"
Me.lbRimanenza.Tooltip = ""
Me.lbRimanenza.UseMnemonic = false
'
'lbPesoNetto
'
Me.lbPesoNetto.AutoSize = true
Me.lbPesoNetto.BackColor = System.Drawing.Color.Transparent
Me.lbPesoNetto.Location = New System.Drawing.Point(6, 226)
Me.lbPesoNetto.Name = "lbPesoNetto"
Me.lbPesoNetto.NTSDbField = ""
Me.lbPesoNetto.Size = New System.Drawing.Size(59, 13)
Me.lbPesoNetto.TabIndex = 70
Me.lbPesoNetto.Text = "Peso netto"
Me.lbPesoNetto.Tooltip = ""
Me.lbPesoNetto.UseMnemonic = false
'
'edEt_bolli
'
Me.edEt_bolli.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_bolli.EditValue = "0"
Me.edEt_bolli.Enabled = false
Me.edEt_bolli.Location = New System.Drawing.Point(160, 80)
Me.edEt_bolli.Name = "edEt_bolli"
Me.edEt_bolli.NTSDbField = ""
Me.edEt_bolli.NTSFormat = "0"
Me.edEt_bolli.NTSForzaVisZoom = false
Me.edEt_bolli.NTSOldValue = ""
Me.edEt_bolli.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_bolli.Properties.Appearance.Options.UseBackColor = true
Me.edEt_bolli.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_bolli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_bolli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_bolli.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_bolli.Properties.AutoHeight = false
Me.edEt_bolli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_bolli.Properties.MaxLength = 65536
Me.edEt_bolli.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_bolli.Size = New System.Drawing.Size(77, 20)
Me.edEt_bolli.TabIndex = 66
'
'edResiduov
'
Me.edResiduov.Cursor = System.Windows.Forms.Cursors.Default
Me.edResiduov.EditValue = "0"
Me.edResiduov.Enabled = false
Me.edResiduov.Location = New System.Drawing.Point(340, 167)
Me.edResiduov.Name = "edResiduov"
Me.edResiduov.NTSDbField = ""
Me.edResiduov.NTSFormat = "0"
Me.edResiduov.NTSForzaVisZoom = false
Me.edResiduov.NTSOldValue = ""
Me.edResiduov.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edResiduov.Properties.Appearance.Options.UseBackColor = true
Me.edResiduov.Properties.Appearance.Options.UseTextOptions = true
Me.edResiduov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edResiduov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edResiduov.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edResiduov.Properties.AutoHeight = false
Me.edResiduov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edResiduov.Properties.MaxLength = 65536
Me.edResiduov.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edResiduov.Size = New System.Drawing.Size(77, 20)
Me.edResiduov.TabIndex = 94
'
'edEt_peso
'
Me.edEt_peso.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_peso.EditValue = "0"
Me.edEt_peso.Location = New System.Drawing.Point(78, 243)
Me.edEt_peso.Name = "edEt_peso"
Me.edEt_peso.NTSDbField = ""
Me.edEt_peso.NTSFormat = "0"
Me.edEt_peso.NTSForzaVisZoom = false
Me.edEt_peso.NTSOldValue = ""
Me.edEt_peso.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_peso.Properties.Appearance.Options.UseBackColor = true
Me.edEt_peso.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_peso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_peso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_peso.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_peso.Properties.AutoHeight = false
Me.edEt_peso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_peso.Properties.MaxLength = 65536
Me.edEt_peso.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_peso.Size = New System.Drawing.Size(60, 20)
Me.edEt_peso.TabIndex = 69
'
'edTotIvav
'
Me.edTotIvav.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotIvav.EditValue = "0"
Me.edTotIvav.Enabled = false
Me.edTotIvav.Location = New System.Drawing.Point(78, 167)
Me.edTotIvav.Name = "edTotIvav"
Me.edTotIvav.NTSDbField = ""
Me.edTotIvav.NTSFormat = "0"
Me.edTotIvav.NTSForzaVisZoom = false
Me.edTotIvav.NTSOldValue = ""
Me.edTotIvav.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotIvav.Properties.Appearance.Options.UseBackColor = true
Me.edTotIvav.Properties.Appearance.Options.UseTextOptions = true
Me.edTotIvav.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotIvav.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotIvav.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotIvav.Properties.AutoHeight = false
Me.edTotIvav.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotIvav.Properties.MaxLength = 65536
Me.edTotIvav.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotIvav.Size = New System.Drawing.Size(77, 20)
Me.edTotIvav.TabIndex = 79
'
'edEt_abbuono
'
Me.edEt_abbuono.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_abbuono.EditValue = "0"
Me.edEt_abbuono.Location = New System.Drawing.Point(422, 147)
Me.edEt_abbuono.Name = "edEt_abbuono"
Me.edEt_abbuono.NTSDbField = ""
Me.edEt_abbuono.NTSFormat = "0"
Me.edEt_abbuono.NTSForzaVisZoom = false
Me.edEt_abbuono.NTSOldValue = ""
Me.edEt_abbuono.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_abbuono.Properties.Appearance.Options.UseBackColor = true
Me.edEt_abbuono.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_abbuono.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_abbuono.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_abbuono.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_abbuono.Properties.AutoHeight = false
Me.edEt_abbuono.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_abbuono.Properties.MaxLength = 65536
Me.edEt_abbuono.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_abbuono.Size = New System.Drawing.Size(77, 20)
Me.edEt_abbuono.TabIndex = 90
'
'edEt_totcoll
'
Me.edEt_totcoll.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totcoll.EditValue = "0"
Me.edEt_totcoll.Location = New System.Drawing.Point(78, 203)
Me.edEt_totcoll.Name = "edEt_totcoll"
Me.edEt_totcoll.NTSDbField = ""
Me.edEt_totcoll.NTSFormat = "0"
Me.edEt_totcoll.NTSForzaVisZoom = false
Me.edEt_totcoll.NTSOldValue = ""
Me.edEt_totcoll.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totcoll.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totcoll.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totcoll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totcoll.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totcoll.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totcoll.Properties.AutoHeight = false
Me.edEt_totcoll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totcoll.Properties.MaxLength = 65536
Me.edEt_totcoll.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totcoll.Size = New System.Drawing.Size(76, 20)
Me.edEt_totcoll.TabIndex = 68
'
'lbNetto
'
Me.lbNetto.AutoSize = true
Me.lbNetto.BackColor = System.Drawing.Color.Transparent
Me.lbNetto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbNetto.Location = New System.Drawing.Point(6, 110)
Me.lbNetto.Name = "lbNetto"
Me.lbNetto.NTSDbField = ""
Me.lbNetto.Size = New System.Drawing.Size(38, 13)
Me.lbNetto.TabIndex = 68
Me.lbNetto.Text = "Netto"
Me.lbNetto.Tooltip = ""
Me.lbNetto.UseMnemonic = false
'
'lbEt_totcoll
'
Me.lbEt_totcoll.AutoSize = true
Me.lbEt_totcoll.BackColor = System.Drawing.Color.Transparent
Me.lbEt_totcoll.Location = New System.Drawing.Point(6, 206)
Me.lbEt_totcoll.Name = "lbEt_totcoll"
Me.lbEt_totcoll.NTSDbField = ""
Me.lbEt_totcoll.Size = New System.Drawing.Size(67, 13)
Me.lbEt_totcoll.TabIndex = 67
Me.lbEt_totcoll.Text = "TOT colli\qtà"
Me.lbEt_totcoll.Tooltip = ""
Me.lbEt_totcoll.UseMnemonic = false
'
'lbAbbuono
'
Me.lbAbbuono.AutoSize = true
Me.lbAbbuono.BackColor = System.Drawing.Color.Transparent
Me.lbAbbuono.Location = New System.Drawing.Point(268, 150)
Me.lbAbbuono.Name = "lbAbbuono"
Me.lbAbbuono.NTSDbField = ""
Me.lbAbbuono.Size = New System.Drawing.Size(50, 13)
Me.lbAbbuono.TabIndex = 89
Me.lbAbbuono.Text = "Abbuono"
Me.lbAbbuono.Tooltip = ""
Me.lbAbbuono.UseMnemonic = false
'
'edEt_totmerce
'
Me.edEt_totmerce.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totmerce.EditValue = "0"
Me.edEt_totmerce.Enabled = false
Me.edEt_totmerce.Location = New System.Drawing.Point(160, 107)
Me.edEt_totmerce.Name = "edEt_totmerce"
Me.edEt_totmerce.NTSDbField = ""
Me.edEt_totmerce.NTSFormat = "0"
Me.edEt_totmerce.NTSForzaVisZoom = false
Me.edEt_totmerce.NTSOldValue = ""
Me.edEt_totmerce.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totmerce.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totmerce.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totmerce.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totmerce.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totmerce.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totmerce.Properties.AutoHeight = false
Me.edEt_totmerce.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totmerce.Properties.MaxLength = 65536
Me.edEt_totmerce.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totmerce.Size = New System.Drawing.Size(77, 20)
Me.edEt_totmerce.TabIndex = 69
'
'edEt_pagato
'
Me.edEt_pagato.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pagato.EditValue = "0"
Me.edEt_pagato.Location = New System.Drawing.Point(422, 87)
Me.edEt_pagato.Name = "edEt_pagato"
Me.edEt_pagato.NTSDbField = ""
Me.edEt_pagato.NTSFormat = "0"
Me.edEt_pagato.NTSForzaVisZoom = false
Me.edEt_pagato.NTSOldValue = ""
Me.edEt_pagato.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_pagato.Properties.Appearance.Options.UseBackColor = true
Me.edEt_pagato.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_pagato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_pagato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_pagato.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_pagato.Properties.AutoHeight = false
Me.edEt_pagato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_pagato.Properties.MaxLength = 65536
Me.edEt_pagato.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_pagato.Size = New System.Drawing.Size(77, 20)
Me.edEt_pagato.TabIndex = 87
'
'edTotImponibv
'
Me.edTotImponibv.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotImponibv.EditValue = "0"
Me.edTotImponibv.Enabled = false
Me.edTotImponibv.Location = New System.Drawing.Point(78, 147)
Me.edTotImponibv.Name = "edTotImponibv"
Me.edTotImponibv.NTSDbField = ""
Me.edTotImponibv.NTSFormat = "0"
Me.edTotImponibv.NTSForzaVisZoom = false
Me.edTotImponibv.NTSOldValue = ""
Me.edTotImponibv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotImponibv.Properties.Appearance.Options.UseBackColor = true
Me.edTotImponibv.Properties.Appearance.Options.UseTextOptions = true
Me.edTotImponibv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotImponibv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotImponibv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotImponibv.Properties.AutoHeight = false
Me.edTotImponibv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotImponibv.Properties.MaxLength = 65536
Me.edTotImponibv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotImponibv.Size = New System.Drawing.Size(77, 20)
Me.edTotImponibv.TabIndex = 76
'
'lbIncassato
'
Me.lbIncassato.AutoSize = true
Me.lbIncassato.BackColor = System.Drawing.Color.Transparent
Me.lbIncassato.Location = New System.Drawing.Point(268, 90)
Me.lbIncassato.Name = "lbIncassato"
Me.lbIncassato.NTSDbField = ""
Me.lbIncassato.Size = New System.Drawing.Size(54, 13)
Me.lbIncassato.TabIndex = 86
Me.lbIncassato.Text = "Incassato"
Me.lbIncassato.Tooltip = ""
Me.lbIncassato.UseMnemonic = false
'
'lbSpese
'
Me.lbSpese.AutoSize = true
Me.lbSpese.BackColor = System.Drawing.Color.Transparent
Me.lbSpese.Location = New System.Drawing.Point(6, 130)
Me.lbSpese.Name = "lbSpese"
Me.lbSpese.NTSDbField = ""
Me.lbSpese.Size = New System.Drawing.Size(68, 13)
Me.lbSpese.TabIndex = 71
Me.lbSpese.Text = "Totale spese"
Me.lbSpese.Tooltip = ""
Me.lbSpese.UseMnemonic = false
'
'edEt_abbuonov
'
Me.edEt_abbuonov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_abbuonov.EditValue = "0"
Me.edEt_abbuonov.Location = New System.Drawing.Point(340, 147)
Me.edEt_abbuonov.Name = "edEt_abbuonov"
Me.edEt_abbuonov.NTSDbField = ""
Me.edEt_abbuonov.NTSFormat = "0"
Me.edEt_abbuonov.NTSForzaVisZoom = false
Me.edEt_abbuonov.NTSOldValue = ""
Me.edEt_abbuonov.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_abbuonov.Properties.Appearance.Options.UseBackColor = true
Me.edEt_abbuonov.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_abbuonov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_abbuonov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_abbuonov.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_abbuonov.Properties.AutoHeight = false
Me.edEt_abbuonov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_abbuonov.Properties.MaxLength = 65536
Me.edEt_abbuonov.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_abbuonov.Size = New System.Drawing.Size(77, 20)
Me.edEt_abbuonov.TabIndex = 91
'
'edTotSpesev
'
Me.edTotSpesev.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotSpesev.EditValue = "0"
Me.edTotSpesev.Enabled = false
Me.edTotSpesev.Location = New System.Drawing.Point(78, 127)
Me.edTotSpesev.Name = "edTotSpesev"
Me.edTotSpesev.NTSDbField = ""
Me.edTotSpesev.NTSFormat = "0"
Me.edTotSpesev.NTSForzaVisZoom = false
Me.edTotSpesev.NTSOldValue = ""
Me.edTotSpesev.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotSpesev.Properties.Appearance.Options.UseBackColor = true
Me.edTotSpesev.Properties.Appearance.Options.UseTextOptions = true
Me.edTotSpesev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotSpesev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotSpesev.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotSpesev.Properties.AutoHeight = false
Me.edTotSpesev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotSpesev.Properties.MaxLength = 65536
Me.edTotSpesev.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotSpesev.Size = New System.Drawing.Size(77, 20)
Me.edTotSpesev.TabIndex = 73
'
'edEt_totomag
'
Me.edEt_totomag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totomag.EditValue = "0"
Me.edEt_totomag.Enabled = false
Me.edEt_totomag.Location = New System.Drawing.Point(422, 40)
Me.edEt_totomag.Name = "edEt_totomag"
Me.edEt_totomag.NTSDbField = ""
Me.edEt_totomag.NTSFormat = "0"
Me.edEt_totomag.NTSForzaVisZoom = false
Me.edEt_totomag.NTSOldValue = ""
Me.edEt_totomag.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totomag.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totomag.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totomag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totomag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totomag.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totomag.Properties.AutoHeight = false
Me.edEt_totomag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totomag.Properties.MaxLength = 65536
Me.edEt_totomag.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totomag.Size = New System.Drawing.Size(77, 20)
Me.edEt_totomag.TabIndex = 84
'
'edTotSpese
'
Me.edTotSpese.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotSpese.EditValue = "0"
Me.edTotSpese.Enabled = false
Me.edTotSpese.Location = New System.Drawing.Point(160, 127)
Me.edTotSpese.Name = "edTotSpese"
Me.edTotSpese.NTSDbField = ""
Me.edTotSpese.NTSFormat = "0"
Me.edTotSpese.NTSForzaVisZoom = false
Me.edTotSpese.NTSOldValue = ""
Me.edTotSpese.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotSpese.Properties.Appearance.Options.UseBackColor = true
Me.edTotSpese.Properties.Appearance.Options.UseTextOptions = true
Me.edTotSpese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotSpese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotSpese.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotSpese.Properties.AutoHeight = false
Me.edTotSpese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotSpese.Properties.MaxLength = 65536
Me.edTotSpese.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotSpese.Size = New System.Drawing.Size(77, 20)
Me.edTotSpese.TabIndex = 72
'
'lbOmaggi
'
Me.lbOmaggi.AutoSize = true
Me.lbOmaggi.BackColor = System.Drawing.Color.Transparent
Me.lbOmaggi.Location = New System.Drawing.Point(268, 43)
Me.lbOmaggi.Name = "lbOmaggi"
Me.lbOmaggi.NTSDbField = ""
Me.lbOmaggi.Size = New System.Drawing.Size(43, 13)
Me.lbOmaggi.TabIndex = 83
Me.lbOmaggi.Text = "Omaggi"
Me.lbOmaggi.Tooltip = ""
Me.lbOmaggi.UseMnemonic = false
'
'edEt_totmercev
'
Me.edEt_totmercev.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totmercev.EditValue = "0"
Me.edEt_totmercev.Enabled = false
Me.edEt_totmercev.Location = New System.Drawing.Point(78, 107)
Me.edEt_totmercev.Name = "edEt_totmercev"
Me.edEt_totmercev.NTSDbField = ""
Me.edEt_totmercev.NTSFormat = "0"
Me.edEt_totmercev.NTSForzaVisZoom = false
Me.edEt_totmercev.NTSOldValue = ""
Me.edEt_totmercev.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totmercev.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totmercev.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totmercev.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totmercev.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totmercev.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totmercev.Properties.AutoHeight = false
Me.edEt_totmercev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totmercev.Properties.MaxLength = 65536
Me.edEt_totmercev.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totmercev.Size = New System.Drawing.Size(77, 20)
Me.edEt_totmercev.TabIndex = 70
'
'edEt_totdoc
'
Me.edEt_totdoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totdoc.EditValue = "0"
Me.edEt_totdoc.Enabled = false
Me.edEt_totdoc.Location = New System.Drawing.Point(422, 20)
Me.edEt_totdoc.Name = "edEt_totdoc"
Me.edEt_totdoc.NTSDbField = ""
Me.edEt_totdoc.NTSFormat = "0"
Me.edEt_totdoc.NTSForzaVisZoom = false
Me.edEt_totdoc.NTSOldValue = ""
Me.edEt_totdoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totdoc.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totdoc.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totdoc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totdoc.Properties.AutoHeight = false
Me.edEt_totdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totdoc.Properties.MaxLength = 65536
Me.edEt_totdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totdoc.Size = New System.Drawing.Size(77, 20)
Me.edEt_totdoc.TabIndex = 81
'
'lbImponibile
'
Me.lbImponibile.AutoSize = true
Me.lbImponibile.BackColor = System.Drawing.Color.Transparent
Me.lbImponibile.Location = New System.Drawing.Point(6, 150)
Me.lbImponibile.Name = "lbImponibile"
Me.lbImponibile.NTSDbField = ""
Me.lbImponibile.Size = New System.Drawing.Size(55, 13)
Me.lbImponibile.TabIndex = 74
Me.lbImponibile.Text = "Imponibile"
Me.lbImponibile.Tooltip = ""
Me.lbImponibile.UseMnemonic = false
'
'edEt_pagatov
'
Me.edEt_pagatov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pagatov.EditValue = "0"
Me.edEt_pagatov.Location = New System.Drawing.Point(340, 87)
Me.edEt_pagatov.Name = "edEt_pagatov"
Me.edEt_pagatov.NTSDbField = ""
Me.edEt_pagatov.NTSFormat = "0"
Me.edEt_pagatov.NTSForzaVisZoom = false
Me.edEt_pagatov.NTSOldValue = ""
Me.edEt_pagatov.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_pagatov.Properties.Appearance.Options.UseBackColor = true
Me.edEt_pagatov.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_pagatov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_pagatov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_pagatov.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_pagatov.Properties.AutoHeight = false
Me.edEt_pagatov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_pagatov.Properties.MaxLength = 65536
Me.edEt_pagatov.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_pagatov.Size = New System.Drawing.Size(77, 20)
Me.edEt_pagatov.TabIndex = 88
'
'edTotImponib
'
Me.edTotImponib.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotImponib.EditValue = "0"
Me.edTotImponib.Enabled = false
Me.edTotImponib.Location = New System.Drawing.Point(160, 147)
Me.edTotImponib.Name = "edTotImponib"
Me.edTotImponib.NTSDbField = ""
Me.edTotImponib.NTSFormat = "0"
Me.edTotImponib.NTSForzaVisZoom = false
Me.edTotImponib.NTSOldValue = ""
Me.edTotImponib.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotImponib.Properties.Appearance.Options.UseBackColor = true
Me.edTotImponib.Properties.Appearance.Options.UseTextOptions = true
Me.edTotImponib.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotImponib.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotImponib.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotImponib.Properties.AutoHeight = false
Me.edTotImponib.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotImponib.Properties.MaxLength = 65536
Me.edTotImponib.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotImponib.Size = New System.Drawing.Size(77, 20)
Me.edTotImponib.TabIndex = 75
'
'lbDocumento
'
Me.lbDocumento.AutoSize = true
Me.lbDocumento.BackColor = System.Drawing.Color.Transparent
Me.lbDocumento.Location = New System.Drawing.Point(268, 23)
Me.lbDocumento.Name = "lbDocumento"
Me.lbDocumento.NTSDbField = ""
Me.lbDocumento.Size = New System.Drawing.Size(61, 13)
Me.lbDocumento.TabIndex = 80
Me.lbDocumento.Text = "Documento"
Me.lbDocumento.Tooltip = ""
Me.lbDocumento.UseMnemonic = false
'
'edEt_bolliv
'
Me.edEt_bolliv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_bolliv.EditValue = "0"
Me.edEt_bolliv.Enabled = false
Me.edEt_bolliv.Location = New System.Drawing.Point(78, 80)
Me.edEt_bolliv.Name = "edEt_bolliv"
Me.edEt_bolliv.NTSDbField = ""
Me.edEt_bolliv.NTSFormat = "0"
Me.edEt_bolliv.NTSForzaVisZoom = false
Me.edEt_bolliv.NTSOldValue = ""
Me.edEt_bolliv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_bolliv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_bolliv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_bolliv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_bolliv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_bolliv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_bolliv.Properties.AutoHeight = false
Me.edEt_bolliv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_bolliv.Properties.MaxLength = 65536
Me.edEt_bolliv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_bolliv.Size = New System.Drawing.Size(77, 20)
Me.edEt_bolliv.TabIndex = 67
'
'edEt_totomagv
'
Me.edEt_totomagv.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_totomagv.EditValue = "0"
Me.edEt_totomagv.Enabled = false
Me.edEt_totomagv.Location = New System.Drawing.Point(340, 40)
Me.edEt_totomagv.Name = "edEt_totomagv"
Me.edEt_totomagv.NTSDbField = ""
Me.edEt_totomagv.NTSFormat = "0"
Me.edEt_totomagv.NTSForzaVisZoom = false
Me.edEt_totomagv.NTSOldValue = ""
Me.edEt_totomagv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totomagv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totomagv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totomagv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totomagv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totomagv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totomagv.Properties.AutoHeight = false
Me.edEt_totomagv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totomagv.Properties.MaxLength = 65536
Me.edEt_totomagv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totomagv.Size = New System.Drawing.Size(77, 20)
Me.edEt_totomagv.TabIndex = 85
'
'edEt_speaccv
'
Me.edEt_speaccv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speaccv.EditValue = "0"
Me.edEt_speaccv.Location = New System.Drawing.Point(78, 20)
Me.edEt_speaccv.Name = "edEt_speaccv"
Me.edEt_speaccv.NTSDbField = ""
Me.edEt_speaccv.NTSFormat = "0"
Me.edEt_speaccv.NTSForzaVisZoom = false
Me.edEt_speaccv.NTSOldValue = ""
Me.edEt_speaccv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speaccv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speaccv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speaccv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speaccv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speaccv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speaccv.Properties.AutoHeight = false
Me.edEt_speaccv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speaccv.Properties.MaxLength = 65536
Me.edEt_speaccv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speaccv.Size = New System.Drawing.Size(77, 20)
Me.edEt_speaccv.TabIndex = 58
'
'edEt_totdocv
'
Me.edEt_totdocv.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_totdocv.EditValue = "0"
Me.edEt_totdocv.Enabled = false
Me.edEt_totdocv.Location = New System.Drawing.Point(340, 20)
Me.edEt_totdocv.Name = "edEt_totdocv"
Me.edEt_totdocv.NTSDbField = ""
Me.edEt_totdocv.NTSFormat = "0"
Me.edEt_totdocv.NTSForzaVisZoom = false
Me.edEt_totdocv.NTSOldValue = ""
Me.edEt_totdocv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totdocv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totdocv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totdocv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totdocv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totdocv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totdocv.Properties.AutoHeight = false
Me.edEt_totdocv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totdocv.Properties.MaxLength = 65536
Me.edEt_totdocv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totdocv.Size = New System.Drawing.Size(77, 20)
Me.edEt_totdocv.TabIndex = 82
'
'lbImposta
'
Me.lbImposta.AutoSize = true
Me.lbImposta.BackColor = System.Drawing.Color.Transparent
Me.lbImposta.Location = New System.Drawing.Point(6, 170)
Me.lbImposta.Name = "lbImposta"
Me.lbImposta.NTSDbField = ""
Me.lbImposta.Size = New System.Drawing.Size(46, 13)
Me.lbImposta.TabIndex = 77
Me.lbImposta.Text = "Imposta"
Me.lbImposta.Tooltip = ""
Me.lbImposta.UseMnemonic = false
'
'edEt_speincv
'
Me.edEt_speincv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speincv.EditValue = "0"
Me.edEt_speincv.Enabled = false
Me.edEt_speincv.Location = New System.Drawing.Point(78, 60)
Me.edEt_speincv.Name = "edEt_speincv"
Me.edEt_speincv.NTSDbField = ""
Me.edEt_speincv.NTSFormat = "0"
Me.edEt_speincv.NTSForzaVisZoom = false
Me.edEt_speincv.NTSOldValue = ""
Me.edEt_speincv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speincv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speincv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speincv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speincv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speincv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speincv.Properties.AutoHeight = false
Me.edEt_speincv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speincv.Properties.MaxLength = 65536
Me.edEt_speincv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speincv.Size = New System.Drawing.Size(77, 20)
Me.edEt_speincv.TabIndex = 64
'
'edTotIva
'
Me.edTotIva.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotIva.EditValue = "0"
Me.edTotIva.Enabled = false
Me.edTotIva.Location = New System.Drawing.Point(160, 167)
Me.edTotIva.Name = "edTotIva"
Me.edTotIva.NTSDbField = ""
Me.edTotIva.NTSFormat = "0"
Me.edTotIva.NTSForzaVisZoom = false
Me.edTotIva.NTSOldValue = ""
Me.edTotIva.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edTotIva.Properties.Appearance.Options.UseBackColor = true
Me.edTotIva.Properties.Appearance.Options.UseTextOptions = true
Me.edTotIva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edTotIva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edTotIva.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edTotIva.Properties.AutoHeight = false
Me.edTotIva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edTotIva.Properties.MaxLength = 65536
Me.edTotIva.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edTotIva.Size = New System.Drawing.Size(77, 20)
Me.edTotIva.TabIndex = 78
'
'edEt_speimbv
'
Me.edEt_speimbv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speimbv.EditValue = "0"
Me.edEt_speimbv.Location = New System.Drawing.Point(78, 40)
Me.edEt_speimbv.Name = "edEt_speimbv"
Me.edEt_speimbv.NTSDbField = ""
Me.edEt_speimbv.NTSFormat = "0"
Me.edEt_speimbv.NTSForzaVisZoom = false
Me.edEt_speimbv.NTSOldValue = ""
Me.edEt_speimbv.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_speimbv.Properties.Appearance.Options.UseBackColor = true
Me.edEt_speimbv.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_speimbv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_speimbv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_speimbv.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_speimbv.Properties.AutoHeight = false
Me.edEt_speimbv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_speimbv.Properties.MaxLength = 65536
Me.edEt_speimbv.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_speimbv.Size = New System.Drawing.Size(77, 20)
Me.edEt_speimbv.TabIndex = 61
'
'pnPiedeSx
'
Me.pnPiedeSx.AllowDrop = true
Me.pnPiedeSx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeSx.Appearance.Options.UseBackColor = true
Me.pnPiedeSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeSx.Controls.Add(Me.cmdPiedeTrasporto)
Me.pnPiedeSx.Controls.Add(Me.cmdPiedeTotali)
Me.pnPiedeSx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeSx.Dock = System.Windows.Forms.DockStyle.Left
Me.pnPiedeSx.Location = New System.Drawing.Point(0, 0)
Me.pnPiedeSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeSx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeSx.Name = "pnPiedeSx"
Me.pnPiedeSx.NTSActiveTrasparency = true
Me.pnPiedeSx.Size = New System.Drawing.Size(152, 649)
Me.pnPiedeSx.TabIndex = 130
'
'cmdPiedeTrasporto
'
Me.cmdPiedeTrasporto.ImagePath = ""
Me.cmdPiedeTrasporto.ImageText = ""
Me.cmdPiedeTrasporto.Location = New System.Drawing.Point(3, 36)
Me.cmdPiedeTrasporto.Name = "cmdPiedeTrasporto"
Me.cmdPiedeTrasporto.NTSContextMenu = Nothing
Me.cmdPiedeTrasporto.Size = New System.Drawing.Size(140, 28)
Me.cmdPiedeTrasporto.TabIndex = 120
Me.cmdPiedeTrasporto.Text = "Altri dati Piede"
'
'cmdPiedeTotali
'
Me.cmdPiedeTotali.ImagePath = ""
Me.cmdPiedeTotali.ImageText = ""
Me.cmdPiedeTotali.ImeMode = System.Windows.Forms.ImeMode.[On]
Me.cmdPiedeTotali.Location = New System.Drawing.Point(3, 6)
Me.cmdPiedeTotali.Name = "cmdPiedeTotali"
Me.cmdPiedeTotali.NTSContextMenu = Nothing
Me.cmdPiedeTotali.Size = New System.Drawing.Size(140, 28)
Me.cmdPiedeTotali.TabIndex = 118
Me.cmdPiedeTotali.Text = "Totali"
'
'TabPage2
'
Me.TabPage2.AllowDrop = true
Me.TabPage2.Controls.Add(Me.pnGriglia)
Me.TabPage2.Controls.Add(Me.pnCorpo)
Me.TabPage2.Enable = true
Me.TabPage2.Name = "TabPage2"
Me.TabPage2.Size = New System.Drawing.Size(723, 649)
Me.TabPage2.Text = "&2 - Corpo"
'
'pnGriglia
'
Me.pnGriglia.AllowDrop = true
Me.pnGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnGriglia.Appearance.Options.UseBackColor = true
Me.pnGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnGriglia.Controls.Add(Me.grRighe)
Me.pnGriglia.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnGriglia.Location = New System.Drawing.Point(0, 43)
Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnGriglia.Name = "pnGriglia"
Me.pnGriglia.NTSActiveTrasparency = true
Me.pnGriglia.Size = New System.Drawing.Size(723, 606)
Me.pnGriglia.TabIndex = 2
'
'grRighe
'
Me.grRighe.Dock = System.Windows.Forms.DockStyle.Fill
Me.grRighe.EmbeddedNavigator.Name = ""
Me.grRighe.Location = New System.Drawing.Point(0, 0)
Me.grRighe.MainView = Me.grvRighe
Me.grRighe.Name = "grRighe"
Me.grRighe.Size = New System.Drawing.Size(723, 606)
Me.grRighe.TabIndex = 1
Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
'
'grvRighe
'
Me.grvRighe.ActiveFilterEnabled = false
Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.ec_riga, Me.ec_matric, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_ump, Me.ec_quant, Me.ec_preziva, Me.ec_prezvalc, Me.ec_prezzo, Me.ec_scont1, Me.ec_scont2, Me.ec_scont3, Me.ec_scont4, Me.ec_scont5, Me.ec_scont6, Me.ec_causale, Me.xxo_causale, Me.ec_magaz, Me.xxo_magaz, Me.ec_ricimp, Me.ec_provv, Me.ec_vprovv, Me.ec_provv2, Me.ec_vprovv2, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_stasino, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.xxo_lottox, Me.ec_codcena, Me.xxo_codcena, Me.ec_note, Me.ec_causale2, Me.xxo_causale2, Me.ec_magaz2, Me.xxo_magaz2, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.xxo_codarfo, Me.ec_perqta, Me.ec_flkit, Me.ec_ktriga, Me.ec_umprz, Me.ec_fase, Me.xxo_fase, Me.ec_pmtaskid, Me.xxo_pmtaskid, Me.xxo_pmqtares, Me.xxo_pmqtarim, Me.xxo_pmvalres, Me.xxo_pmvalrim, Me.ec_pmsalcon, Me.xxo_pmqtadisda, Me.xxo_pmvaldisda, Me.xxo_darave, Me.ec_ubicaz, Me.xxo_codtagl, Me.ec_flprznet, Me.ec_codtpro, Me.xxo_codtpro, Me.ec_datini, Me.ec_datfin, Me.ec_ortipo, Me.ec_oranno, Me.ec_orserie, Me.ec_ornum, Me.ec_orriga, Me.ec_salcon, Me.ec_commen, Me.ec_codnomc, Me.ec_massakg, Me.ec_massaum2, Me.ec_valstat, Me.ec_proorig, Me.ec_percvst, Me.ec_prtipo, Me.ec_pranno, Me.ec_prserie, Me.ec_prnum, Me.ec_prriga, Me.ec_cltipo, Me.ec_clanno, Me.ec_clserie, Me.ec_clnum, Me.ec_clriga, Me.ec_nptipo, Me.ec_npanno, Me.ec_npserie, Me.ec_npnum, Me.ec_npriga, Me.ec_npqtadis, Me.ec_npcoldis, Me.ec_npvaldis, Me.ec_npsalcon, Me.ec_nprcoleva, Me.ec_nprquaeva, Me.ec_nprflevas, Me.ec_nprvalore, Me.xxo_vallordo, Me.ec_actipo, Me.ec_acanno, Me.ec_acserie, Me.ec_acnum, Me.ec_cctipo, Me.ec_ccanno, Me.ec_ccserie, Me.ec_ccnum, Me.ec_ccriga, Me.xxo_valrigaic, Me.ec_flcom, Me.ec_flelab, Me.ec_valorev, Me.ec_coddivi, Me.xxo_coddivi, Me.ec_cvtipo, Me.ec_cvanno, Me.ec_cvserie, Me.ec_cvnum, Me.ec_cvriga, Me.xxo_codalt, Me.ec_flprzmod, Me.ec_flsc1mod, Me.ec_flsc2mod, Me.ec_flsc3mod, Me.ec_flsc4mod, Me.ec_flsc5mod, Me.ec_flsc6mod, Me.ec_flprov1mod, Me.ec_flprov2mod, Me.xxo_valoreiva, Me.xxo_percricarico, Me.xxo_percmargine, Me.xxo_marginevalore})
Me.grvRighe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
Me.grvRighe.Enabled = true
Me.grvRighe.GridControl = Me.grRighe
Me.grvRighe.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
Me.grvRighe.MinRowHeight = 14
Me.grvRighe.Name = "grvRighe"
Me.grvRighe.NTSAllowDelete = true
Me.grvRighe.NTSAllowInsert = true
Me.grvRighe.NTSAllowUpdate = true
Me.grvRighe.NTSMenuContext = Nothing
Me.grvRighe.OptionsCustomization.AllowRowSizing = true
Me.grvRighe.OptionsFilter.AllowFilterEditor = false
Me.grvRighe.OptionsNavigation.EnterMoveNextColumn = true
Me.grvRighe.OptionsNavigation.UseTabKey = false
Me.grvRighe.OptionsSelection.EnableAppearanceFocusedRow = false
Me.grvRighe.OptionsView.ColumnAutoWidth = false
Me.grvRighe.OptionsView.EnableAppearanceEvenRow = true
Me.grvRighe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
Me.grvRighe.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
Me.grvRighe.OptionsView.ShowGroupPanel = false
Me.grvRighe.RowHeight = 16
'
'xx_sel
'
Me.xx_sel.AppearanceCell.Options.UseBackColor = true
Me.xx_sel.AppearanceCell.Options.UseTextOptions = true
Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xx_sel.Caption = "Sel."
Me.xx_sel.Enabled = true
Me.xx_sel.FieldName = "xx_sel"
Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xx_sel.Name = "xx_sel"
Me.xx_sel.NTSRepositoryComboBox = Nothing
Me.xx_sel.NTSRepositoryItemCheck = Nothing
Me.xx_sel.NTSRepositoryItemMemo = Nothing
Me.xx_sel.NTSRepositoryItemText = Nothing
Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xx_sel.OptionsFilter.AllowFilter = false
Me.xx_sel.Visible = true
Me.xx_sel.VisibleIndex = 0
'
'ec_riga
'
Me.ec_riga.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
Me.ec_riga.AppearanceCell.Options.UseBackColor = true
Me.ec_riga.AppearanceCell.Options.UseTextOptions = true
Me.ec_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_riga.Caption = "Riga"
Me.ec_riga.Enabled = false
Me.ec_riga.FieldName = "ec_riga"
Me.ec_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_riga.Name = "ec_riga"
Me.ec_riga.NTSRepositoryComboBox = Nothing
Me.ec_riga.NTSRepositoryItemCheck = Nothing
Me.ec_riga.NTSRepositoryItemMemo = Nothing
Me.ec_riga.NTSRepositoryItemText = Nothing
Me.ec_riga.OptionsColumn.AllowEdit = false
Me.ec_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_riga.OptionsColumn.ReadOnly = true
Me.ec_riga.OptionsFilter.AllowFilter = false
Me.ec_riga.Visible = true
Me.ec_riga.VisibleIndex = 1
Me.ec_riga.Width = 32
'
'ec_matric
'
Me.ec_matric.AppearanceCell.Options.UseBackColor = true
Me.ec_matric.AppearanceCell.Options.UseTextOptions = true
Me.ec_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_matric.Caption = "Barcode"
Me.ec_matric.Enabled = true
Me.ec_matric.FieldName = "ec_matric"
Me.ec_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_matric.Name = "ec_matric"
Me.ec_matric.NTSRepositoryComboBox = Nothing
Me.ec_matric.NTSRepositoryItemCheck = Nothing
Me.ec_matric.NTSRepositoryItemMemo = Nothing
Me.ec_matric.NTSRepositoryItemText = Nothing
Me.ec_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_matric.OptionsFilter.AllowFilter = false
Me.ec_matric.Width = 73
'
'ec_codart
'
Me.ec_codart.AppearanceCell.Options.UseBackColor = true
Me.ec_codart.AppearanceCell.Options.UseTextOptions = true
Me.ec_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codart.Caption = "Cod. Art."
Me.ec_codart.Enabled = true
Me.ec_codart.FieldName = "ec_codart"
Me.ec_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codart.Name = "ec_codart"
Me.ec_codart.NTSRepositoryComboBox = Nothing
Me.ec_codart.NTSRepositoryItemCheck = Nothing
Me.ec_codart.NTSRepositoryItemMemo = Nothing
Me.ec_codart.NTSRepositoryItemText = Nothing
Me.ec_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codart.OptionsFilter.AllowFilter = false
Me.ec_codart.Visible = true
Me.ec_codart.VisibleIndex = 2
Me.ec_codart.Width = 64
'
'ec_descr
'
Me.ec_descr.AppearanceCell.Options.UseBackColor = true
Me.ec_descr.AppearanceCell.Options.UseTextOptions = true
Me.ec_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_descr.Caption = "Descrizione"
Me.ec_descr.Enabled = true
Me.ec_descr.FieldName = "ec_descr"
Me.ec_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_descr.Name = "ec_descr"
Me.ec_descr.NTSRepositoryComboBox = Nothing
Me.ec_descr.NTSRepositoryItemCheck = Nothing
Me.ec_descr.NTSRepositoryItemMemo = Nothing
Me.ec_descr.NTSRepositoryItemText = Nothing
Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_descr.OptionsFilter.AllowFilter = false
Me.ec_descr.Visible = true
Me.ec_descr.VisibleIndex = 3
Me.ec_descr.Width = 139
'
'ec_desint
'
Me.ec_desint.AppearanceCell.Options.UseBackColor = true
Me.ec_desint.AppearanceCell.Options.UseTextOptions = true
Me.ec_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_desint.Caption = "Descr.interna"
Me.ec_desint.Enabled = true
Me.ec_desint.FieldName = "ec_desint"
Me.ec_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_desint.Name = "ec_desint"
Me.ec_desint.NTSRepositoryComboBox = Nothing
Me.ec_desint.NTSRepositoryItemCheck = Nothing
Me.ec_desint.NTSRepositoryItemMemo = Nothing
Me.ec_desint.NTSRepositoryItemText = Nothing
Me.ec_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_desint.OptionsFilter.AllowFilter = false
Me.ec_desint.Width = 126
'
'ec_unmis
'
Me.ec_unmis.AppearanceCell.Options.UseBackColor = true
Me.ec_unmis.AppearanceCell.Options.UseTextOptions = true
Me.ec_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_unmis.Caption = "U.M."
Me.ec_unmis.Enabled = true
Me.ec_unmis.FieldName = "ec_unmis"
Me.ec_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_unmis.Name = "ec_unmis"
Me.ec_unmis.NTSRepositoryComboBox = Nothing
Me.ec_unmis.NTSRepositoryItemCheck = Nothing
Me.ec_unmis.NTSRepositoryItemMemo = Nothing
Me.ec_unmis.NTSRepositoryItemText = Nothing
Me.ec_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_unmis.OptionsFilter.AllowFilter = false
Me.ec_unmis.Visible = true
Me.ec_unmis.VisibleIndex = 4
Me.ec_unmis.Width = 39
'
'ec_colli
'
Me.ec_colli.AppearanceCell.Options.UseBackColor = true
Me.ec_colli.AppearanceCell.Options.UseTextOptions = true
Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_colli.Caption = "QTA UM"
Me.ec_colli.Enabled = true
Me.ec_colli.FieldName = "ec_colli"
Me.ec_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_colli.Name = "ec_colli"
Me.ec_colli.NTSRepositoryComboBox = Nothing
Me.ec_colli.NTSRepositoryItemCheck = Nothing
Me.ec_colli.NTSRepositoryItemMemo = Nothing
Me.ec_colli.NTSRepositoryItemText = Nothing
Me.ec_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_colli.OptionsFilter.AllowFilter = false
Me.ec_colli.Visible = true
Me.ec_colli.VisibleIndex = 5
Me.ec_colli.Width = 54
'
'ec_ump
'
Me.ec_ump.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
Me.ec_ump.AppearanceCell.Options.UseBackColor = true
Me.ec_ump.AppearanceCell.Options.UseTextOptions = true
Me.ec_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ump.Caption = "UMP"
Me.ec_ump.Enabled = false
Me.ec_ump.FieldName = "ec_ump"
Me.ec_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ump.Name = "ec_ump"
Me.ec_ump.NTSRepositoryComboBox = Nothing
Me.ec_ump.NTSRepositoryItemCheck = Nothing
Me.ec_ump.NTSRepositoryItemMemo = Nothing
Me.ec_ump.NTSRepositoryItemText = Nothing
Me.ec_ump.OptionsColumn.AllowEdit = false
Me.ec_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ump.OptionsColumn.ReadOnly = true
Me.ec_ump.OptionsFilter.AllowFilter = false
Me.ec_ump.Visible = true
Me.ec_ump.VisibleIndex = 6
'
'ec_quant
'
Me.ec_quant.AppearanceCell.Options.UseBackColor = true
Me.ec_quant.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant.Caption = "Q.tà"
Me.ec_quant.Enabled = true
Me.ec_quant.FieldName = "ec_quant"
Me.ec_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant.Name = "ec_quant"
Me.ec_quant.NTSRepositoryComboBox = Nothing
Me.ec_quant.NTSRepositoryItemCheck = Nothing
Me.ec_quant.NTSRepositoryItemMemo = Nothing
Me.ec_quant.NTSRepositoryItemText = Nothing
Me.ec_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant.OptionsFilter.AllowFilter = false
Me.ec_quant.Visible = true
Me.ec_quant.VisibleIndex = 7
'
'ec_preziva
'
Me.ec_preziva.AppearanceCell.Options.UseBackColor = true
Me.ec_preziva.AppearanceCell.Options.UseTextOptions = true
Me.ec_preziva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_preziva.Caption = "Prezzo IVA inc."
Me.ec_preziva.Enabled = true
Me.ec_preziva.FieldName = "ec_preziva"
Me.ec_preziva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_preziva.Name = "ec_preziva"
Me.ec_preziva.NTSRepositoryComboBox = Nothing
Me.ec_preziva.NTSRepositoryItemCheck = Nothing
Me.ec_preziva.NTSRepositoryItemMemo = Nothing
Me.ec_preziva.NTSRepositoryItemText = Nothing
Me.ec_preziva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_preziva.OptionsFilter.AllowFilter = false
'
'ec_prezvalc
'
Me.ec_prezvalc.AppearanceCell.Options.UseBackColor = true
Me.ec_prezvalc.AppearanceCell.Options.UseTextOptions = true
Me.ec_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prezvalc.Caption = "Prezzo valuta"
Me.ec_prezvalc.Enabled = true
Me.ec_prezvalc.FieldName = "ec_prezvalc"
Me.ec_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prezvalc.Name = "ec_prezvalc"
Me.ec_prezvalc.NTSRepositoryComboBox = Nothing
Me.ec_prezvalc.NTSRepositoryItemCheck = Nothing
Me.ec_prezvalc.NTSRepositoryItemMemo = Nothing
Me.ec_prezvalc.NTSRepositoryItemText = Nothing
Me.ec_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prezvalc.OptionsFilter.AllowFilter = false
'
'ec_prezzo
'
Me.ec_prezzo.AppearanceCell.Options.UseBackColor = true
Me.ec_prezzo.AppearanceCell.Options.UseTextOptions = true
Me.ec_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prezzo.Caption = "Prezzo"
Me.ec_prezzo.Enabled = true
Me.ec_prezzo.FieldName = "ec_prezzo"
Me.ec_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prezzo.Name = "ec_prezzo"
Me.ec_prezzo.NTSRepositoryComboBox = Nothing
Me.ec_prezzo.NTSRepositoryItemCheck = Nothing
Me.ec_prezzo.NTSRepositoryItemMemo = Nothing
Me.ec_prezzo.NTSRepositoryItemText = Nothing
Me.ec_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prezzo.OptionsFilter.AllowFilter = false
Me.ec_prezzo.Visible = true
Me.ec_prezzo.VisibleIndex = 8
'
'ec_scont1
'
Me.ec_scont1.AppearanceCell.Options.UseBackColor = true
Me.ec_scont1.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont1.Caption = "Sconto 1"
Me.ec_scont1.Enabled = true
Me.ec_scont1.FieldName = "ec_scont1"
Me.ec_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont1.Name = "ec_scont1"
Me.ec_scont1.NTSRepositoryComboBox = Nothing
Me.ec_scont1.NTSRepositoryItemCheck = Nothing
Me.ec_scont1.NTSRepositoryItemMemo = Nothing
Me.ec_scont1.NTSRepositoryItemText = Nothing
Me.ec_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont1.OptionsFilter.AllowFilter = false
Me.ec_scont1.Visible = true
Me.ec_scont1.VisibleIndex = 9
'
'ec_scont2
'
Me.ec_scont2.AppearanceCell.Options.UseBackColor = true
Me.ec_scont2.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont2.Caption = "Sconto 2"
Me.ec_scont2.Enabled = true
Me.ec_scont2.FieldName = "ec_scont2"
Me.ec_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont2.Name = "ec_scont2"
Me.ec_scont2.NTSRepositoryComboBox = Nothing
Me.ec_scont2.NTSRepositoryItemCheck = Nothing
Me.ec_scont2.NTSRepositoryItemMemo = Nothing
Me.ec_scont2.NTSRepositoryItemText = Nothing
Me.ec_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont2.OptionsFilter.AllowFilter = false
Me.ec_scont2.Visible = true
Me.ec_scont2.VisibleIndex = 10
'
'ec_scont3
'
Me.ec_scont3.AppearanceCell.Options.UseBackColor = true
Me.ec_scont3.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont3.Caption = "Sconto 3"
Me.ec_scont3.Enabled = true
Me.ec_scont3.FieldName = "ec_scont3"
Me.ec_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont3.Name = "ec_scont3"
Me.ec_scont3.NTSRepositoryComboBox = Nothing
Me.ec_scont3.NTSRepositoryItemCheck = Nothing
Me.ec_scont3.NTSRepositoryItemMemo = Nothing
Me.ec_scont3.NTSRepositoryItemText = Nothing
Me.ec_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont3.OptionsFilter.AllowFilter = false
'
'ec_scont4
'
Me.ec_scont4.AppearanceCell.Options.UseBackColor = true
Me.ec_scont4.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont4.Caption = "Sconto 4"
Me.ec_scont4.Enabled = true
Me.ec_scont4.FieldName = "ec_scont4"
Me.ec_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont4.Name = "ec_scont4"
Me.ec_scont4.NTSRepositoryComboBox = Nothing
Me.ec_scont4.NTSRepositoryItemCheck = Nothing
Me.ec_scont4.NTSRepositoryItemMemo = Nothing
Me.ec_scont4.NTSRepositoryItemText = Nothing
Me.ec_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont4.OptionsFilter.AllowFilter = false
'
'ec_scont5
'
Me.ec_scont5.AppearanceCell.Options.UseBackColor = true
Me.ec_scont5.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont5.Caption = "Sconto 5"
Me.ec_scont5.Enabled = true
Me.ec_scont5.FieldName = "ec_scont5"
Me.ec_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont5.Name = "ec_scont5"
Me.ec_scont5.NTSRepositoryComboBox = Nothing
Me.ec_scont5.NTSRepositoryItemCheck = Nothing
Me.ec_scont5.NTSRepositoryItemMemo = Nothing
Me.ec_scont5.NTSRepositoryItemText = Nothing
Me.ec_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont5.OptionsFilter.AllowFilter = false
'
'ec_scont6
'
Me.ec_scont6.AppearanceCell.Options.UseBackColor = true
Me.ec_scont6.AppearanceCell.Options.UseTextOptions = true
Me.ec_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scont6.Caption = "Sconto 6"
Me.ec_scont6.Enabled = true
Me.ec_scont6.FieldName = "ec_scont6"
Me.ec_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scont6.Name = "ec_scont6"
Me.ec_scont6.NTSRepositoryComboBox = Nothing
Me.ec_scont6.NTSRepositoryItemCheck = Nothing
Me.ec_scont6.NTSRepositoryItemMemo = Nothing
Me.ec_scont6.NTSRepositoryItemText = Nothing
Me.ec_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scont6.OptionsFilter.AllowFilter = false
'
'ec_causale
'
Me.ec_causale.AppearanceCell.Options.UseBackColor = true
Me.ec_causale.AppearanceCell.Options.UseTextOptions = true
Me.ec_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_causale.Caption = "Causale"
Me.ec_causale.Enabled = true
Me.ec_causale.FieldName = "ec_causale"
Me.ec_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_causale.Name = "ec_causale"
Me.ec_causale.NTSRepositoryComboBox = Nothing
Me.ec_causale.NTSRepositoryItemCheck = Nothing
Me.ec_causale.NTSRepositoryItemMemo = Nothing
Me.ec_causale.NTSRepositoryItemText = Nothing
Me.ec_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_causale.OptionsFilter.AllowFilter = false
'
'xxo_causale
'
Me.xxo_causale.AppearanceCell.Options.UseBackColor = true
Me.xxo_causale.AppearanceCell.Options.UseTextOptions = true
Me.xxo_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_causale.Caption = "Descr. causale"
Me.xxo_causale.Enabled = false
Me.xxo_causale.FieldName = "xxo_causale"
Me.xxo_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_causale.Name = "xxo_causale"
Me.xxo_causale.NTSRepositoryComboBox = Nothing
Me.xxo_causale.NTSRepositoryItemCheck = Nothing
Me.xxo_causale.NTSRepositoryItemMemo = Nothing
Me.xxo_causale.NTSRepositoryItemText = Nothing
Me.xxo_causale.OptionsColumn.AllowEdit = false
Me.xxo_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_causale.OptionsColumn.ReadOnly = true
Me.xxo_causale.OptionsFilter.AllowFilter = false
'
'ec_magaz
'
Me.ec_magaz.AppearanceCell.Options.UseBackColor = true
Me.ec_magaz.AppearanceCell.Options.UseTextOptions = true
Me.ec_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_magaz.Caption = "Magazzino"
Me.ec_magaz.Enabled = true
Me.ec_magaz.FieldName = "ec_magaz"
Me.ec_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_magaz.Name = "ec_magaz"
Me.ec_magaz.NTSRepositoryComboBox = Nothing
Me.ec_magaz.NTSRepositoryItemCheck = Nothing
Me.ec_magaz.NTSRepositoryItemMemo = Nothing
Me.ec_magaz.NTSRepositoryItemText = Nothing
Me.ec_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_magaz.OptionsFilter.AllowFilter = false
'
'xxo_magaz
'
Me.xxo_magaz.AppearanceCell.Options.UseBackColor = true
Me.xxo_magaz.AppearanceCell.Options.UseTextOptions = true
Me.xxo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_magaz.Caption = "Descr. magazzino"
Me.xxo_magaz.Enabled = false
Me.xxo_magaz.FieldName = "xxo_magaz"
Me.xxo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_magaz.Name = "xxo_magaz"
Me.xxo_magaz.NTSRepositoryComboBox = Nothing
Me.xxo_magaz.NTSRepositoryItemCheck = Nothing
Me.xxo_magaz.NTSRepositoryItemMemo = Nothing
Me.xxo_magaz.NTSRepositoryItemText = Nothing
Me.xxo_magaz.OptionsColumn.AllowEdit = false
Me.xxo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_magaz.OptionsColumn.ReadOnly = true
Me.xxo_magaz.OptionsFilter.AllowFilter = false
'
'ec_ricimp
'
Me.ec_ricimp.AppearanceCell.Options.UseBackColor = true
Me.ec_ricimp.AppearanceCell.Options.UseTextOptions = true
Me.ec_ricimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ricimp.Caption = "Provv. a val."
Me.ec_ricimp.Enabled = true
Me.ec_ricimp.FieldName = "ec_ricimp"
Me.ec_ricimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ricimp.Name = "ec_ricimp"
Me.ec_ricimp.NTSRepositoryComboBox = Nothing
Me.ec_ricimp.NTSRepositoryItemCheck = Nothing
Me.ec_ricimp.NTSRepositoryItemMemo = Nothing
Me.ec_ricimp.NTSRepositoryItemText = Nothing
Me.ec_ricimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ricimp.OptionsFilter.AllowFilter = false
Me.ec_ricimp.UnboundType = DevExpress.Data.UnboundColumnType.[Integer]
'
'ec_provv
'
Me.ec_provv.AppearanceCell.Options.UseBackColor = true
Me.ec_provv.AppearanceCell.Options.UseTextOptions = true
Me.ec_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_provv.Caption = "Provv. 1"
Me.ec_provv.Enabled = true
Me.ec_provv.FieldName = "ec_provv"
Me.ec_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_provv.Name = "ec_provv"
Me.ec_provv.NTSRepositoryComboBox = Nothing
Me.ec_provv.NTSRepositoryItemCheck = Nothing
Me.ec_provv.NTSRepositoryItemMemo = Nothing
Me.ec_provv.NTSRepositoryItemText = Nothing
Me.ec_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_provv.OptionsFilter.AllowFilter = false
Me.ec_provv.Visible = true
Me.ec_provv.VisibleIndex = 11
'
'ec_vprovv
'
Me.ec_vprovv.AppearanceCell.Options.UseBackColor = true
Me.ec_vprovv.AppearanceCell.Options.UseTextOptions = true
Me.ec_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_vprovv.Caption = "Importo provv. 1"
Me.ec_vprovv.Enabled = false
Me.ec_vprovv.FieldName = "ec_vprovv"
Me.ec_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_vprovv.Name = "ec_vprovv"
Me.ec_vprovv.NTSRepositoryComboBox = Nothing
Me.ec_vprovv.NTSRepositoryItemCheck = Nothing
Me.ec_vprovv.NTSRepositoryItemMemo = Nothing
Me.ec_vprovv.NTSRepositoryItemText = Nothing
Me.ec_vprovv.OptionsColumn.AllowEdit = false
Me.ec_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_vprovv.OptionsColumn.ReadOnly = true
Me.ec_vprovv.OptionsFilter.AllowFilter = false
'
'ec_provv2
'
Me.ec_provv2.AppearanceCell.Options.UseBackColor = true
Me.ec_provv2.AppearanceCell.Options.UseTextOptions = true
Me.ec_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_provv2.Caption = "Provv. 2"
Me.ec_provv2.Enabled = true
Me.ec_provv2.FieldName = "ec_provv2"
Me.ec_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_provv2.Name = "ec_provv2"
Me.ec_provv2.NTSRepositoryComboBox = Nothing
Me.ec_provv2.NTSRepositoryItemCheck = Nothing
Me.ec_provv2.NTSRepositoryItemMemo = Nothing
Me.ec_provv2.NTSRepositoryItemText = Nothing
Me.ec_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_provv2.OptionsFilter.AllowFilter = false
'
'ec_vprovv2
'
Me.ec_vprovv2.AppearanceCell.Options.UseBackColor = true
Me.ec_vprovv2.AppearanceCell.Options.UseTextOptions = true
Me.ec_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_vprovv2.Caption = "Importo provv. 2"
Me.ec_vprovv2.Enabled = false
Me.ec_vprovv2.FieldName = "ec_vprovv2"
Me.ec_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_vprovv2.Name = "ec_vprovv2"
Me.ec_vprovv2.NTSRepositoryComboBox = Nothing
Me.ec_vprovv2.NTSRepositoryItemCheck = Nothing
Me.ec_vprovv2.NTSRepositoryItemMemo = Nothing
Me.ec_vprovv2.NTSRepositoryItemText = Nothing
Me.ec_vprovv2.OptionsColumn.AllowEdit = false
Me.ec_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_vprovv2.OptionsColumn.ReadOnly = true
Me.ec_vprovv2.OptionsFilter.AllowFilter = false
'
'ec_controp
'
Me.ec_controp.AppearanceCell.Options.UseBackColor = true
Me.ec_controp.AppearanceCell.Options.UseTextOptions = true
Me.ec_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_controp.Caption = "Controp."
Me.ec_controp.Enabled = true
Me.ec_controp.FieldName = "ec_controp"
Me.ec_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_controp.Name = "ec_controp"
Me.ec_controp.NTSRepositoryComboBox = Nothing
Me.ec_controp.NTSRepositoryItemCheck = Nothing
Me.ec_controp.NTSRepositoryItemMemo = Nothing
Me.ec_controp.NTSRepositoryItemText = Nothing
Me.ec_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_controp.OptionsFilter.AllowFilter = false
Me.ec_controp.Visible = true
Me.ec_controp.VisibleIndex = 12
'
'xxo_controp
'
Me.xxo_controp.AppearanceCell.Options.UseBackColor = true
Me.xxo_controp.AppearanceCell.Options.UseTextOptions = true
Me.xxo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_controp.Caption = "Descr. controp."
Me.xxo_controp.Enabled = false
Me.xxo_controp.FieldName = "xxo_controp"
Me.xxo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_controp.Name = "xxo_controp"
Me.xxo_controp.NTSRepositoryComboBox = Nothing
Me.xxo_controp.NTSRepositoryItemCheck = Nothing
Me.xxo_controp.NTSRepositoryItemMemo = Nothing
Me.xxo_controp.NTSRepositoryItemText = Nothing
Me.xxo_controp.OptionsColumn.AllowEdit = false
Me.xxo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_controp.OptionsColumn.ReadOnly = true
Me.xxo_controp.OptionsFilter.AllowFilter = false
Me.xxo_controp.Visible = true
Me.xxo_controp.VisibleIndex = 13
'
'ec_codiva
'
Me.ec_codiva.AppearanceCell.Options.UseBackColor = true
Me.ec_codiva.AppearanceCell.Options.UseTextOptions = true
Me.ec_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codiva.Caption = "Cod. IVA"
Me.ec_codiva.Enabled = true
Me.ec_codiva.FieldName = "ec_codiva"
Me.ec_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codiva.Name = "ec_codiva"
Me.ec_codiva.NTSRepositoryComboBox = Nothing
Me.ec_codiva.NTSRepositoryItemCheck = Nothing
Me.ec_codiva.NTSRepositoryItemMemo = Nothing
Me.ec_codiva.NTSRepositoryItemText = Nothing
Me.ec_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codiva.OptionsFilter.AllowFilter = false
Me.ec_codiva.Visible = true
Me.ec_codiva.VisibleIndex = 14
'
'xxo_codiva
'
Me.xxo_codiva.AppearanceCell.Options.UseBackColor = true
Me.xxo_codiva.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codiva.Caption = "Descr. IVA"
Me.xxo_codiva.Enabled = false
Me.xxo_codiva.FieldName = "xxo_codiva"
Me.xxo_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codiva.Name = "xxo_codiva"
Me.xxo_codiva.NTSRepositoryComboBox = Nothing
Me.xxo_codiva.NTSRepositoryItemCheck = Nothing
Me.xxo_codiva.NTSRepositoryItemMemo = Nothing
Me.xxo_codiva.NTSRepositoryItemText = Nothing
Me.xxo_codiva.OptionsColumn.AllowEdit = false
Me.xxo_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codiva.OptionsColumn.ReadOnly = true
Me.xxo_codiva.OptionsFilter.AllowFilter = false
Me.xxo_codiva.Visible = true
Me.xxo_codiva.VisibleIndex = 15
'
'ec_stasino
'
Me.ec_stasino.AppearanceCell.Options.UseBackColor = true
Me.ec_stasino.AppearanceCell.Options.UseTextOptions = true
Me.ec_stasino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_stasino.Caption = "Stampa riga"
Me.ec_stasino.Enabled = true
Me.ec_stasino.FieldName = "ec_stasino"
Me.ec_stasino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_stasino.Name = "ec_stasino"
Me.ec_stasino.NTSRepositoryComboBox = Nothing
Me.ec_stasino.NTSRepositoryItemCheck = Nothing
Me.ec_stasino.NTSRepositoryItemMemo = Nothing
Me.ec_stasino.NTSRepositoryItemText = Nothing
Me.ec_stasino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_stasino.OptionsFilter.AllowFilter = false
Me.ec_stasino.Visible = true
Me.ec_stasino.VisibleIndex = 16
'
'ec_codcfam
'
Me.ec_codcfam.AppearanceCell.Options.UseBackColor = true
Me.ec_codcfam.AppearanceCell.Options.UseTextOptions = true
Me.ec_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codcfam.Caption = "Linea/Fam."
Me.ec_codcfam.Enabled = true
Me.ec_codcfam.FieldName = "ec_codcfam"
Me.ec_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codcfam.Name = "ec_codcfam"
Me.ec_codcfam.NTSRepositoryComboBox = Nothing
Me.ec_codcfam.NTSRepositoryItemCheck = Nothing
Me.ec_codcfam.NTSRepositoryItemMemo = Nothing
Me.ec_codcfam.NTSRepositoryItemText = Nothing
Me.ec_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codcfam.OptionsFilter.AllowFilter = false
'
'xxo_codcfam
'
Me.xxo_codcfam.AppearanceCell.Options.UseBackColor = true
Me.xxo_codcfam.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codcfam.Caption = "Descr. linea/fam"
Me.xxo_codcfam.Enabled = false
Me.xxo_codcfam.FieldName = "xxo_codcfam"
Me.xxo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codcfam.Name = "xxo_codcfam"
Me.xxo_codcfam.NTSRepositoryComboBox = Nothing
Me.xxo_codcfam.NTSRepositoryItemCheck = Nothing
Me.xxo_codcfam.NTSRepositoryItemMemo = Nothing
Me.xxo_codcfam.NTSRepositoryItemText = Nothing
Me.xxo_codcfam.OptionsColumn.AllowEdit = false
Me.xxo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codcfam.OptionsColumn.ReadOnly = true
Me.xxo_codcfam.OptionsFilter.AllowFilter = false
'
'ec_commeca
'
Me.ec_commeca.AppearanceCell.Options.UseBackColor = true
Me.ec_commeca.AppearanceCell.Options.UseTextOptions = true
Me.ec_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_commeca.Caption = "Comm. C.A."
Me.ec_commeca.Enabled = true
Me.ec_commeca.FieldName = "ec_commeca"
Me.ec_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_commeca.Name = "ec_commeca"
Me.ec_commeca.NTSRepositoryComboBox = Nothing
Me.ec_commeca.NTSRepositoryItemCheck = Nothing
Me.ec_commeca.NTSRepositoryItemMemo = Nothing
Me.ec_commeca.NTSRepositoryItemText = Nothing
Me.ec_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_commeca.OptionsFilter.AllowFilter = false
'
'xxo_commeca
'
Me.xxo_commeca.AppearanceCell.Options.UseBackColor = true
Me.xxo_commeca.AppearanceCell.Options.UseTextOptions = true
Me.xxo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_commeca.Caption = "Descr. commessa"
Me.xxo_commeca.Enabled = false
Me.xxo_commeca.FieldName = "xxo_commeca"
Me.xxo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_commeca.Name = "xxo_commeca"
Me.xxo_commeca.NTSRepositoryComboBox = Nothing
Me.xxo_commeca.NTSRepositoryItemCheck = Nothing
Me.xxo_commeca.NTSRepositoryItemMemo = Nothing
Me.xxo_commeca.NTSRepositoryItemText = Nothing
Me.xxo_commeca.OptionsColumn.AllowEdit = false
Me.xxo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_commeca.OptionsColumn.ReadOnly = true
Me.xxo_commeca.OptionsFilter.AllowFilter = false
'
'ec_subcommeca
'
Me.ec_subcommeca.AppearanceCell.Options.UseBackColor = true
Me.ec_subcommeca.AppearanceCell.Options.UseTextOptions = true
Me.ec_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_subcommeca.Caption = "Sub C."
Me.ec_subcommeca.Enabled = true
Me.ec_subcommeca.FieldName = "ec_subcommeca"
Me.ec_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_subcommeca.Name = "ec_subcommeca"
Me.ec_subcommeca.NTSRepositoryComboBox = Nothing
Me.ec_subcommeca.NTSRepositoryItemCheck = Nothing
Me.ec_subcommeca.NTSRepositoryItemMemo = Nothing
Me.ec_subcommeca.NTSRepositoryItemText = Nothing
Me.ec_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_subcommeca.OptionsFilter.AllowFilter = false
'
'xxo_lottox
'
Me.xxo_lottox.AppearanceCell.Options.UseBackColor = true
Me.xxo_lottox.AppearanceCell.Options.UseTextOptions = true
Me.xxo_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_lottox.Caption = "Lotto"
Me.xxo_lottox.Enabled = true
Me.xxo_lottox.FieldName = "xxo_lottox"
Me.xxo_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_lottox.Name = "xxo_lottox"
Me.xxo_lottox.NTSRepositoryComboBox = Nothing
Me.xxo_lottox.NTSRepositoryItemCheck = Nothing
Me.xxo_lottox.NTSRepositoryItemMemo = Nothing
Me.xxo_lottox.NTSRepositoryItemText = Nothing
Me.xxo_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_lottox.OptionsFilter.AllowFilter = false
'
'ec_codcena
'
Me.ec_codcena.AppearanceCell.Options.UseBackColor = true
Me.ec_codcena.AppearanceCell.Options.UseTextOptions = true
Me.ec_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codcena.Caption = "Centro C.A."
Me.ec_codcena.Enabled = true
Me.ec_codcena.FieldName = "ec_codcena"
Me.ec_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codcena.Name = "ec_codcena"
Me.ec_codcena.NTSRepositoryComboBox = Nothing
Me.ec_codcena.NTSRepositoryItemCheck = Nothing
Me.ec_codcena.NTSRepositoryItemMemo = Nothing
Me.ec_codcena.NTSRepositoryItemText = Nothing
Me.ec_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codcena.OptionsFilter.AllowFilter = false
'
'xxo_codcena
'
Me.xxo_codcena.AppearanceCell.Options.UseBackColor = true
Me.xxo_codcena.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codcena.Caption = "Descr. centro"
Me.xxo_codcena.Enabled = false
Me.xxo_codcena.FieldName = "xxo_codcena"
Me.xxo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codcena.Name = "xxo_codcena"
Me.xxo_codcena.NTSRepositoryComboBox = Nothing
Me.xxo_codcena.NTSRepositoryItemCheck = Nothing
Me.xxo_codcena.NTSRepositoryItemMemo = Nothing
Me.xxo_codcena.NTSRepositoryItemText = Nothing
Me.xxo_codcena.OptionsColumn.AllowEdit = false
Me.xxo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codcena.OptionsColumn.ReadOnly = true
Me.xxo_codcena.OptionsFilter.AllowFilter = false
'
'ec_note
'
Me.ec_note.AppearanceCell.Options.UseBackColor = true
Me.ec_note.AppearanceCell.Options.UseTextOptions = true
Me.ec_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_note.Caption = "Note"
Me.ec_note.Enabled = true
Me.ec_note.FieldName = "ec_note"
Me.ec_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_note.Name = "ec_note"
Me.ec_note.NTSRepositoryComboBox = Nothing
Me.ec_note.NTSRepositoryItemCheck = Nothing
Me.ec_note.NTSRepositoryItemMemo = Nothing
Me.ec_note.NTSRepositoryItemText = Nothing
Me.ec_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_note.OptionsFilter.AllowFilter = false
Me.ec_note.Visible = true
Me.ec_note.VisibleIndex = 17
'
'ec_causale2
'
Me.ec_causale2.AppearanceCell.Options.UseBackColor = true
Me.ec_causale2.AppearanceCell.Options.UseTextOptions = true
Me.ec_causale2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_causale2.Caption = "Causale 2"
Me.ec_causale2.Enabled = true
Me.ec_causale2.FieldName = "ec_causale2"
Me.ec_causale2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_causale2.Name = "ec_causale2"
Me.ec_causale2.NTSRepositoryComboBox = Nothing
Me.ec_causale2.NTSRepositoryItemCheck = Nothing
Me.ec_causale2.NTSRepositoryItemMemo = Nothing
Me.ec_causale2.NTSRepositoryItemText = Nothing
Me.ec_causale2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_causale2.OptionsFilter.AllowFilter = false
'
'xxo_causale2
'
Me.xxo_causale2.AppearanceCell.Options.UseBackColor = true
Me.xxo_causale2.AppearanceCell.Options.UseTextOptions = true
Me.xxo_causale2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_causale2.Caption = "Descr. causale 2"
Me.xxo_causale2.Enabled = false
Me.xxo_causale2.FieldName = "xxo_causale2"
Me.xxo_causale2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_causale2.Name = "xxo_causale2"
Me.xxo_causale2.NTSRepositoryComboBox = Nothing
Me.xxo_causale2.NTSRepositoryItemCheck = Nothing
Me.xxo_causale2.NTSRepositoryItemMemo = Nothing
Me.xxo_causale2.NTSRepositoryItemText = Nothing
Me.xxo_causale2.OptionsColumn.AllowEdit = false
Me.xxo_causale2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_causale2.OptionsColumn.ReadOnly = true
Me.xxo_causale2.OptionsFilter.AllowFilter = false
'
'ec_magaz2
'
Me.ec_magaz2.AppearanceCell.Options.UseBackColor = true
Me.ec_magaz2.AppearanceCell.Options.UseTextOptions = true
Me.ec_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_magaz2.Caption = "Magaz 2"
Me.ec_magaz2.Enabled = true
Me.ec_magaz2.FieldName = "ec_magaz2"
Me.ec_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_magaz2.Name = "ec_magaz2"
Me.ec_magaz2.NTSRepositoryComboBox = Nothing
Me.ec_magaz2.NTSRepositoryItemCheck = Nothing
Me.ec_magaz2.NTSRepositoryItemMemo = Nothing
Me.ec_magaz2.NTSRepositoryItemText = Nothing
Me.ec_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_magaz2.OptionsFilter.AllowFilter = false
'
'xxo_magaz2
'
Me.xxo_magaz2.AppearanceCell.Options.UseBackColor = true
Me.xxo_magaz2.AppearanceCell.Options.UseTextOptions = true
Me.xxo_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_magaz2.Caption = "Descr. magaz. 2"
Me.xxo_magaz2.Enabled = false
Me.xxo_magaz2.FieldName = "xxo_magaz2"
Me.xxo_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_magaz2.Name = "xxo_magaz2"
Me.xxo_magaz2.NTSRepositoryComboBox = Nothing
Me.xxo_magaz2.NTSRepositoryItemCheck = Nothing
Me.xxo_magaz2.NTSRepositoryItemMemo = Nothing
Me.xxo_magaz2.NTSRepositoryItemText = Nothing
Me.xxo_magaz2.OptionsColumn.AllowEdit = false
Me.xxo_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_magaz2.OptionsColumn.ReadOnly = true
Me.xxo_magaz2.OptionsFilter.AllowFilter = false
'
'ec_valore
'
Me.ec_valore.AppearanceCell.Options.UseBackColor = true
Me.ec_valore.AppearanceCell.Options.UseTextOptions = true
Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_valore.Caption = "Valore riga"
Me.ec_valore.Enabled = true
Me.ec_valore.FieldName = "ec_valore"
Me.ec_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_valore.Name = "ec_valore"
Me.ec_valore.NTSRepositoryComboBox = Nothing
Me.ec_valore.NTSRepositoryItemCheck = Nothing
Me.ec_valore.NTSRepositoryItemMemo = Nothing
Me.ec_valore.NTSRepositoryItemText = Nothing
Me.ec_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_valore.OptionsFilter.AllowFilter = false
Me.ec_valore.Visible = true
Me.ec_valore.VisibleIndex = 18
'
'ec_contocontr
'
Me.ec_contocontr.AppearanceCell.Options.UseBackColor = true
Me.ec_contocontr.AppearanceCell.Options.UseTextOptions = true
Me.ec_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_contocontr.Caption = "Conto controp."
Me.ec_contocontr.Enabled = true
Me.ec_contocontr.FieldName = "ec_contocontr"
Me.ec_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_contocontr.Name = "ec_contocontr"
Me.ec_contocontr.NTSRepositoryComboBox = Nothing
Me.ec_contocontr.NTSRepositoryItemCheck = Nothing
Me.ec_contocontr.NTSRepositoryItemMemo = Nothing
Me.ec_contocontr.NTSRepositoryItemText = Nothing
Me.ec_contocontr.OptionsColumn.AllowEdit = false
Me.ec_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_contocontr.OptionsFilter.AllowFilter = false
'
'xxo_contocon
'
Me.xxo_contocon.AppearanceCell.Options.UseBackColor = true
Me.xxo_contocon.AppearanceCell.Options.UseTextOptions = true
Me.xxo_contocon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_contocon.Caption = "Descr. conto controp."
Me.xxo_contocon.Enabled = false
Me.xxo_contocon.FieldName = "xxo_contocon"
Me.xxo_contocon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_contocon.Name = "xxo_contocon"
Me.xxo_contocon.NTSRepositoryComboBox = Nothing
Me.xxo_contocon.NTSRepositoryItemCheck = Nothing
Me.xxo_contocon.NTSRepositoryItemMemo = Nothing
Me.xxo_contocon.NTSRepositoryItemText = Nothing
Me.xxo_contocon.OptionsColumn.AllowEdit = false
Me.xxo_contocon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_contocon.OptionsColumn.ReadOnly = true
Me.xxo_contocon.OptionsFilter.AllowFilter = false
'
'ec_codclie
'
Me.ec_codclie.AppearanceCell.Options.UseBackColor = true
Me.ec_codclie.AppearanceCell.Options.UseTextOptions = true
Me.ec_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codclie.Caption = "Cod. cliente"
Me.ec_codclie.Enabled = true
Me.ec_codclie.FieldName = "ec_codclie"
Me.ec_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codclie.Name = "ec_codclie"
Me.ec_codclie.NTSRepositoryComboBox = Nothing
Me.ec_codclie.NTSRepositoryItemCheck = Nothing
Me.ec_codclie.NTSRepositoryItemMemo = Nothing
Me.ec_codclie.NTSRepositoryItemText = Nothing
Me.ec_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codclie.OptionsFilter.AllowFilter = false
'
'xxo_codclie
'
Me.xxo_codclie.AppearanceCell.Options.UseBackColor = true
Me.xxo_codclie.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codclie.Caption = "Descr. cliente"
Me.xxo_codclie.Enabled = false
Me.xxo_codclie.FieldName = "xxo_codclie"
Me.xxo_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codclie.Name = "xxo_codclie"
Me.xxo_codclie.NTSRepositoryComboBox = Nothing
Me.xxo_codclie.NTSRepositoryItemCheck = Nothing
Me.xxo_codclie.NTSRepositoryItemMemo = Nothing
Me.xxo_codclie.NTSRepositoryItemText = Nothing
Me.xxo_codclie.OptionsColumn.AllowEdit = false
Me.xxo_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codclie.OptionsColumn.ReadOnly = true
Me.xxo_codclie.OptionsFilter.AllowFilter = false
'
'ec_misura1
'
Me.ec_misura1.AppearanceCell.Options.UseBackColor = true
Me.ec_misura1.AppearanceCell.Options.UseTextOptions = true
Me.ec_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_misura1.Caption = "Misura 1"
Me.ec_misura1.Enabled = true
Me.ec_misura1.FieldName = "ec_misura1"
Me.ec_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_misura1.Name = "ec_misura1"
Me.ec_misura1.NTSRepositoryComboBox = Nothing
Me.ec_misura1.NTSRepositoryItemCheck = Nothing
Me.ec_misura1.NTSRepositoryItemMemo = Nothing
Me.ec_misura1.NTSRepositoryItemText = Nothing
Me.ec_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_misura1.OptionsFilter.AllowFilter = false
'
'ec_misura2
'
Me.ec_misura2.AppearanceCell.Options.UseBackColor = true
Me.ec_misura2.AppearanceCell.Options.UseTextOptions = true
Me.ec_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_misura2.Caption = "Misura 2"
Me.ec_misura2.Enabled = true
Me.ec_misura2.FieldName = "ec_misura2"
Me.ec_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_misura2.Name = "ec_misura2"
Me.ec_misura2.NTSRepositoryComboBox = Nothing
Me.ec_misura2.NTSRepositoryItemCheck = Nothing
Me.ec_misura2.NTSRepositoryItemMemo = Nothing
Me.ec_misura2.NTSRepositoryItemText = Nothing
Me.ec_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_misura2.OptionsFilter.AllowFilter = false
'
'ec_misura3
'
Me.ec_misura3.AppearanceCell.Options.UseBackColor = true
Me.ec_misura3.AppearanceCell.Options.UseTextOptions = true
Me.ec_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_misura3.Caption = "Misura 3"
Me.ec_misura3.Enabled = true
Me.ec_misura3.FieldName = "ec_misura3"
Me.ec_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_misura3.Name = "ec_misura3"
Me.ec_misura3.NTSRepositoryComboBox = Nothing
Me.ec_misura3.NTSRepositoryItemCheck = Nothing
Me.ec_misura3.NTSRepositoryItemMemo = Nothing
Me.ec_misura3.NTSRepositoryItemText = Nothing
Me.ec_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_misura3.OptionsFilter.AllowFilter = false
'
'xxo_codarfo
'
Me.xxo_codarfo.AppearanceCell.Options.UseBackColor = true
Me.xxo_codarfo.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codarfo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codarfo.Caption = "Cod.Art.cli/forn"
Me.xxo_codarfo.Enabled = true
Me.xxo_codarfo.FieldName = "xxo_codarfo"
Me.xxo_codarfo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codarfo.Name = "xxo_codarfo"
Me.xxo_codarfo.NTSRepositoryComboBox = Nothing
Me.xxo_codarfo.NTSRepositoryItemCheck = Nothing
Me.xxo_codarfo.NTSRepositoryItemMemo = Nothing
Me.xxo_codarfo.NTSRepositoryItemText = Nothing
Me.xxo_codarfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codarfo.OptionsFilter.AllowFilter = false
'
'ec_perqta
'
Me.ec_perqta.AppearanceCell.Options.UseBackColor = true
Me.ec_perqta.AppearanceCell.Options.UseTextOptions = true
Me.ec_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_perqta.Caption = "Prz/Qta"
Me.ec_perqta.Enabled = false
Me.ec_perqta.FieldName = "ec_perqta"
Me.ec_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_perqta.Name = "ec_perqta"
Me.ec_perqta.NTSRepositoryComboBox = Nothing
Me.ec_perqta.NTSRepositoryItemCheck = Nothing
Me.ec_perqta.NTSRepositoryItemMemo = Nothing
Me.ec_perqta.NTSRepositoryItemText = Nothing
Me.ec_perqta.OptionsColumn.AllowEdit = false
Me.ec_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_perqta.OptionsColumn.ReadOnly = true
Me.ec_perqta.OptionsFilter.AllowFilter = false
'
'ec_flkit
'
Me.ec_flkit.AppearanceCell.Options.UseBackColor = true
Me.ec_flkit.AppearanceCell.Options.UseTextOptions = true
Me.ec_flkit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flkit.Caption = "Tipo Kit"
Me.ec_flkit.Enabled = true
Me.ec_flkit.FieldName = "ec_flkit"
Me.ec_flkit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flkit.Name = "ec_flkit"
Me.ec_flkit.NTSRepositoryComboBox = Nothing
Me.ec_flkit.NTSRepositoryItemCheck = Nothing
Me.ec_flkit.NTSRepositoryItemMemo = Nothing
Me.ec_flkit.NTSRepositoryItemText = Nothing
Me.ec_flkit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flkit.OptionsFilter.AllowFilter = false
Me.ec_flkit.Visible = true
Me.ec_flkit.VisibleIndex = 19
'
'ec_ktriga
'
Me.ec_ktriga.AppearanceCell.Options.UseBackColor = true
Me.ec_ktriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_ktriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ktriga.Caption = "Rif. riga kit"
Me.ec_ktriga.Enabled = true
Me.ec_ktriga.FieldName = "ec_ktriga"
Me.ec_ktriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ktriga.Name = "ec_ktriga"
Me.ec_ktriga.NTSRepositoryComboBox = Nothing
Me.ec_ktriga.NTSRepositoryItemCheck = Nothing
Me.ec_ktriga.NTSRepositoryItemMemo = Nothing
Me.ec_ktriga.NTSRepositoryItemText = Nothing
Me.ec_ktriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ktriga.OptionsFilter.AllowFilter = false
'
'ec_umprz
'
Me.ec_umprz.AppearanceCell.Options.UseBackColor = true
Me.ec_umprz.AppearanceCell.Options.UseTextOptions = true
Me.ec_umprz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_umprz.Caption = "Prezzi x u.d.m."
Me.ec_umprz.Enabled = false
Me.ec_umprz.FieldName = "ec_umprz"
Me.ec_umprz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_umprz.Name = "ec_umprz"
Me.ec_umprz.NTSRepositoryComboBox = Nothing
Me.ec_umprz.NTSRepositoryItemCheck = Nothing
Me.ec_umprz.NTSRepositoryItemMemo = Nothing
Me.ec_umprz.NTSRepositoryItemText = Nothing
Me.ec_umprz.OptionsColumn.AllowEdit = false
Me.ec_umprz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_umprz.OptionsColumn.ReadOnly = true
Me.ec_umprz.OptionsFilter.AllowFilter = false
'
'ec_fase
'
Me.ec_fase.AppearanceCell.Options.UseBackColor = true
Me.ec_fase.AppearanceCell.Options.UseTextOptions = true
Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_fase.Caption = "Fase"
Me.ec_fase.Enabled = true
Me.ec_fase.FieldName = "ec_fase"
Me.ec_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_fase.Name = "ec_fase"
Me.ec_fase.NTSRepositoryComboBox = Nothing
Me.ec_fase.NTSRepositoryItemCheck = Nothing
Me.ec_fase.NTSRepositoryItemMemo = Nothing
Me.ec_fase.NTSRepositoryItemText = Nothing
Me.ec_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_fase.OptionsFilter.AllowFilter = false
'
'xxo_fase
'
Me.xxo_fase.AppearanceCell.Options.UseBackColor = true
Me.xxo_fase.AppearanceCell.Options.UseTextOptions = true
Me.xxo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_fase.Caption = "Descr. fase"
Me.xxo_fase.Enabled = false
Me.xxo_fase.FieldName = "xxo_fase"
Me.xxo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_fase.Name = "xxo_fase"
Me.xxo_fase.NTSRepositoryComboBox = Nothing
Me.xxo_fase.NTSRepositoryItemCheck = Nothing
Me.xxo_fase.NTSRepositoryItemMemo = Nothing
Me.xxo_fase.NTSRepositoryItemText = Nothing
Me.xxo_fase.OptionsColumn.AllowEdit = false
Me.xxo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_fase.OptionsColumn.ReadOnly = true
Me.xxo_fase.OptionsFilter.AllowFilter = false
'
'ec_pmtaskid
'
Me.ec_pmtaskid.AppearanceCell.Options.UseBackColor = true
Me.ec_pmtaskid.AppearanceCell.Options.UseTextOptions = true
Me.ec_pmtaskid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_pmtaskid.Caption = "Id Task"
Me.ec_pmtaskid.Enabled = true
Me.ec_pmtaskid.FieldName = "ec_pmtaskid"
Me.ec_pmtaskid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_pmtaskid.Name = "ec_pmtaskid"
Me.ec_pmtaskid.NTSRepositoryComboBox = Nothing
Me.ec_pmtaskid.NTSRepositoryItemCheck = Nothing
Me.ec_pmtaskid.NTSRepositoryItemMemo = Nothing
Me.ec_pmtaskid.NTSRepositoryItemText = Nothing
Me.ec_pmtaskid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_pmtaskid.OptionsFilter.AllowFilter = false
'
'xxo_pmtaskid
'
Me.xxo_pmtaskid.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmtaskid.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmtaskid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmtaskid.Caption = "Descr. Task"
Me.xxo_pmtaskid.Enabled = false
Me.xxo_pmtaskid.FieldName = "xxo_pmtaskid"
Me.xxo_pmtaskid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmtaskid.Name = "xxo_pmtaskid"
Me.xxo_pmtaskid.NTSRepositoryComboBox = Nothing
Me.xxo_pmtaskid.NTSRepositoryItemCheck = Nothing
Me.xxo_pmtaskid.NTSRepositoryItemMemo = Nothing
Me.xxo_pmtaskid.NTSRepositoryItemText = Nothing
Me.xxo_pmtaskid.OptionsColumn.AllowEdit = false
Me.xxo_pmtaskid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmtaskid.OptionsColumn.ReadOnly = true
Me.xxo_pmtaskid.OptionsFilter.AllowFilter = false
'
'xxo_pmqtares
'
Me.xxo_pmqtares.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmqtares.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmqtares.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmqtares.Caption = "Qta res.Task"
Me.xxo_pmqtares.Enabled = false
Me.xxo_pmqtares.FieldName = "xxo_pmqtares"
Me.xxo_pmqtares.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmqtares.Name = "xxo_pmqtares"
Me.xxo_pmqtares.NTSRepositoryComboBox = Nothing
Me.xxo_pmqtares.NTSRepositoryItemCheck = Nothing
Me.xxo_pmqtares.NTSRepositoryItemMemo = Nothing
Me.xxo_pmqtares.NTSRepositoryItemText = Nothing
Me.xxo_pmqtares.OptionsColumn.AllowEdit = false
Me.xxo_pmqtares.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmqtares.OptionsColumn.ReadOnly = true
Me.xxo_pmqtares.OptionsFilter.AllowFilter = false
'
'xxo_pmqtarim
'
Me.xxo_pmqtarim.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmqtarim.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmqtarim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmqtarim.Caption = "Qta riman. Task"
Me.xxo_pmqtarim.Enabled = true
Me.xxo_pmqtarim.FieldName = "xxo_pmqtarim"
Me.xxo_pmqtarim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmqtarim.Name = "xxo_pmqtarim"
Me.xxo_pmqtarim.NTSRepositoryComboBox = Nothing
Me.xxo_pmqtarim.NTSRepositoryItemCheck = Nothing
Me.xxo_pmqtarim.NTSRepositoryItemMemo = Nothing
Me.xxo_pmqtarim.NTSRepositoryItemText = Nothing
Me.xxo_pmqtarim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmqtarim.OptionsFilter.AllowFilter = false
'
'xxo_pmvalres
'
Me.xxo_pmvalres.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmvalres.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmvalres.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmvalres.Caption = "Val. res. Task"
Me.xxo_pmvalres.Enabled = false
Me.xxo_pmvalres.FieldName = "xxo_pmvalres"
Me.xxo_pmvalres.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmvalres.Name = "xxo_pmvalres"
Me.xxo_pmvalres.NTSRepositoryComboBox = Nothing
Me.xxo_pmvalres.NTSRepositoryItemCheck = Nothing
Me.xxo_pmvalres.NTSRepositoryItemMemo = Nothing
Me.xxo_pmvalres.NTSRepositoryItemText = Nothing
Me.xxo_pmvalres.OptionsColumn.AllowEdit = false
Me.xxo_pmvalres.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmvalres.OptionsColumn.ReadOnly = true
Me.xxo_pmvalres.OptionsFilter.AllowFilter = false
'
'xxo_pmvalrim
'
Me.xxo_pmvalrim.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmvalrim.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmvalrim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmvalrim.Caption = "Val. rim. Task"
Me.xxo_pmvalrim.Enabled = true
Me.xxo_pmvalrim.FieldName = "xxo_pmvalrim"
Me.xxo_pmvalrim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmvalrim.Name = "xxo_pmvalrim"
Me.xxo_pmvalrim.NTSRepositoryComboBox = Nothing
Me.xxo_pmvalrim.NTSRepositoryItemCheck = Nothing
Me.xxo_pmvalrim.NTSRepositoryItemMemo = Nothing
Me.xxo_pmvalrim.NTSRepositoryItemText = Nothing
Me.xxo_pmvalrim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmvalrim.OptionsFilter.AllowFilter = false
'
'ec_pmsalcon
'
Me.ec_pmsalcon.AppearanceCell.Options.UseBackColor = true
Me.ec_pmsalcon.AppearanceCell.Options.UseTextOptions = true
Me.ec_pmsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_pmsalcon.Caption = "Sal. Task"
Me.ec_pmsalcon.Enabled = true
Me.ec_pmsalcon.FieldName = "ec_pmsalcon"
Me.ec_pmsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_pmsalcon.Name = "ec_pmsalcon"
Me.ec_pmsalcon.NTSRepositoryComboBox = Nothing
Me.ec_pmsalcon.NTSRepositoryItemCheck = Nothing
Me.ec_pmsalcon.NTSRepositoryItemMemo = Nothing
Me.ec_pmsalcon.NTSRepositoryItemText = Nothing
Me.ec_pmsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_pmsalcon.OptionsFilter.AllowFilter = false
'
'xxo_pmqtadisda
'
Me.xxo_pmqtadisda.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmqtadisda.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmqtadisda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmqtadisda.Caption = "Qta dis. Task"
Me.xxo_pmqtadisda.Enabled = false
Me.xxo_pmqtadisda.FieldName = "xxo_pmqtadisda"
Me.xxo_pmqtadisda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmqtadisda.Name = "xxo_pmqtadisda"
Me.xxo_pmqtadisda.NTSRepositoryComboBox = Nothing
Me.xxo_pmqtadisda.NTSRepositoryItemCheck = Nothing
Me.xxo_pmqtadisda.NTSRepositoryItemMemo = Nothing
Me.xxo_pmqtadisda.NTSRepositoryItemText = Nothing
Me.xxo_pmqtadisda.OptionsColumn.AllowEdit = false
Me.xxo_pmqtadisda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmqtadisda.OptionsColumn.ReadOnly = true
Me.xxo_pmqtadisda.OptionsFilter.AllowFilter = false
'
'xxo_pmvaldisda
'
Me.xxo_pmvaldisda.AppearanceCell.Options.UseBackColor = true
Me.xxo_pmvaldisda.AppearanceCell.Options.UseTextOptions = true
Me.xxo_pmvaldisda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_pmvaldisda.Caption = "Val. dis. Task"
Me.xxo_pmvaldisda.Enabled = false
Me.xxo_pmvaldisda.FieldName = "xxo_pmvaldisda"
Me.xxo_pmvaldisda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_pmvaldisda.Name = "xxo_pmvaldisda"
Me.xxo_pmvaldisda.NTSRepositoryComboBox = Nothing
Me.xxo_pmvaldisda.NTSRepositoryItemCheck = Nothing
Me.xxo_pmvaldisda.NTSRepositoryItemMemo = Nothing
Me.xxo_pmvaldisda.NTSRepositoryItemText = Nothing
Me.xxo_pmvaldisda.OptionsColumn.AllowEdit = false
Me.xxo_pmvaldisda.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_pmvaldisda.OptionsColumn.ReadOnly = true
Me.xxo_pmvaldisda.OptionsFilter.AllowFilter = false
'
'xxo_darave
'
Me.xxo_darave.AppearanceCell.Options.UseBackColor = true
Me.xxo_darave.AppearanceCell.Options.UseTextOptions = true
Me.xxo_darave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_darave.Caption = "Dare-Avere"
Me.xxo_darave.Enabled = false
Me.xxo_darave.FieldName = "xxo_darave"
Me.xxo_darave.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_darave.Name = "xxo_darave"
Me.xxo_darave.NTSRepositoryComboBox = Nothing
Me.xxo_darave.NTSRepositoryItemCheck = Nothing
Me.xxo_darave.NTSRepositoryItemMemo = Nothing
Me.xxo_darave.NTSRepositoryItemText = Nothing
Me.xxo_darave.OptionsColumn.AllowEdit = false
Me.xxo_darave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_darave.OptionsColumn.ReadOnly = true
Me.xxo_darave.OptionsFilter.AllowFilter = false
'
'ec_ubicaz
'
Me.ec_ubicaz.AppearanceCell.Options.UseBackColor = true
Me.ec_ubicaz.AppearanceCell.Options.UseTextOptions = true
Me.ec_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ubicaz.Caption = "Ubicazione"
Me.ec_ubicaz.Enabled = true
Me.ec_ubicaz.FieldName = "ec_ubicaz"
Me.ec_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ubicaz.Name = "ec_ubicaz"
Me.ec_ubicaz.NTSRepositoryComboBox = Nothing
Me.ec_ubicaz.NTSRepositoryItemCheck = Nothing
Me.ec_ubicaz.NTSRepositoryItemMemo = Nothing
Me.ec_ubicaz.NTSRepositoryItemText = Nothing
Me.ec_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ubicaz.OptionsFilter.AllowFilter = false
'
'xxo_codtagl
'
Me.xxo_codtagl.AppearanceCell.Options.UseBackColor = true
Me.xxo_codtagl.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codtagl.Enabled = false
Me.xxo_codtagl.FieldName = "xxo_codtagl"
Me.xxo_codtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codtagl.Name = "xxo_codtagl"
Me.xxo_codtagl.NTSRepositoryComboBox = Nothing
Me.xxo_codtagl.NTSRepositoryItemCheck = Nothing
Me.xxo_codtagl.NTSRepositoryItemMemo = Nothing
Me.xxo_codtagl.NTSRepositoryItemText = Nothing
Me.xxo_codtagl.OptionsColumn.AllowEdit = false
Me.xxo_codtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codtagl.OptionsColumn.ReadOnly = true
Me.xxo_codtagl.OptionsFilter.AllowFilter = false
'
'ec_flprznet
'
Me.ec_flprznet.AppearanceCell.Options.UseBackColor = true
Me.ec_flprznet.AppearanceCell.Options.UseTextOptions = true
Me.ec_flprznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flprznet.Caption = "Prezzo netto"
Me.ec_flprznet.Enabled = true
Me.ec_flprznet.FieldName = "ec_flprznet"
Me.ec_flprznet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flprznet.Name = "ec_flprznet"
Me.ec_flprznet.NTSRepositoryComboBox = Nothing
Me.ec_flprznet.NTSRepositoryItemCheck = Nothing
Me.ec_flprznet.NTSRepositoryItemMemo = Nothing
Me.ec_flprznet.NTSRepositoryItemText = Nothing
Me.ec_flprznet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flprznet.OptionsFilter.AllowFilter = false
'
'ec_codtpro
'
Me.ec_codtpro.AppearanceCell.Options.UseBackColor = true
Me.ec_codtpro.AppearanceCell.Options.UseTextOptions = true
Me.ec_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codtpro.Caption = "Cod. promoz."
Me.ec_codtpro.Enabled = true
Me.ec_codtpro.FieldName = "ec_codtpro"
Me.ec_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codtpro.Name = "ec_codtpro"
Me.ec_codtpro.NTSRepositoryComboBox = Nothing
Me.ec_codtpro.NTSRepositoryItemCheck = Nothing
Me.ec_codtpro.NTSRepositoryItemMemo = Nothing
Me.ec_codtpro.NTSRepositoryItemText = Nothing
Me.ec_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codtpro.OptionsFilter.AllowFilter = false
'
'xxo_codtpro
'
Me.xxo_codtpro.AppearanceCell.Options.UseBackColor = true
Me.xxo_codtpro.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codtpro.Caption = "Descr. promoz."
Me.xxo_codtpro.Enabled = false
Me.xxo_codtpro.FieldName = "xxo_codtpro"
Me.xxo_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codtpro.Name = "xxo_codtpro"
Me.xxo_codtpro.NTSRepositoryComboBox = Nothing
Me.xxo_codtpro.NTSRepositoryItemCheck = Nothing
Me.xxo_codtpro.NTSRepositoryItemMemo = Nothing
Me.xxo_codtpro.NTSRepositoryItemText = Nothing
Me.xxo_codtpro.OptionsColumn.AllowEdit = false
Me.xxo_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codtpro.OptionsColumn.ReadOnly = true
Me.xxo_codtpro.OptionsFilter.AllowFilter = false
'
'ec_datini
'
Me.ec_datini.AppearanceCell.Options.UseBackColor = true
Me.ec_datini.AppearanceCell.Options.UseTextOptions = true
Me.ec_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_datini.Caption = "Dt iniz. comp. econ."
Me.ec_datini.Enabled = true
Me.ec_datini.FieldName = "ec_datini"
Me.ec_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_datini.Name = "ec_datini"
Me.ec_datini.NTSRepositoryComboBox = Nothing
Me.ec_datini.NTSRepositoryItemCheck = Nothing
Me.ec_datini.NTSRepositoryItemMemo = Nothing
Me.ec_datini.NTSRepositoryItemText = Nothing
Me.ec_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_datini.OptionsFilter.AllowFilter = false
'
'ec_datfin
'
Me.ec_datfin.AppearanceCell.Options.UseBackColor = true
Me.ec_datfin.AppearanceCell.Options.UseTextOptions = true
Me.ec_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_datfin.Caption = "Dt fin. comp. econ."
Me.ec_datfin.Enabled = true
Me.ec_datfin.FieldName = "ec_datfin"
Me.ec_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_datfin.Name = "ec_datfin"
Me.ec_datfin.NTSRepositoryComboBox = Nothing
Me.ec_datfin.NTSRepositoryItemCheck = Nothing
Me.ec_datfin.NTSRepositoryItemMemo = Nothing
Me.ec_datfin.NTSRepositoryItemText = Nothing
Me.ec_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_datfin.OptionsFilter.AllowFilter = false
'
'ec_ortipo
'
Me.ec_ortipo.AppearanceCell.Options.UseBackColor = true
Me.ec_ortipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_ortipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ortipo.Caption = "Ord. tipo"
Me.ec_ortipo.Enabled = false
Me.ec_ortipo.FieldName = "ec_ortipo"
Me.ec_ortipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ortipo.Name = "ec_ortipo"
Me.ec_ortipo.NTSRepositoryComboBox = Nothing
Me.ec_ortipo.NTSRepositoryItemCheck = Nothing
Me.ec_ortipo.NTSRepositoryItemMemo = Nothing
Me.ec_ortipo.NTSRepositoryItemText = Nothing
Me.ec_ortipo.OptionsColumn.AllowEdit = false
Me.ec_ortipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ortipo.OptionsColumn.ReadOnly = true
Me.ec_ortipo.OptionsFilter.AllowFilter = false
'
'ec_oranno
'
Me.ec_oranno.AppearanceCell.Options.UseBackColor = true
Me.ec_oranno.AppearanceCell.Options.UseTextOptions = true
Me.ec_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oranno.Caption = "Ord. anno"
Me.ec_oranno.Enabled = false
Me.ec_oranno.FieldName = "ec_oranno"
Me.ec_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oranno.Name = "ec_oranno"
Me.ec_oranno.NTSRepositoryComboBox = Nothing
Me.ec_oranno.NTSRepositoryItemCheck = Nothing
Me.ec_oranno.NTSRepositoryItemMemo = Nothing
Me.ec_oranno.NTSRepositoryItemText = Nothing
Me.ec_oranno.OptionsColumn.AllowEdit = false
Me.ec_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oranno.OptionsColumn.ReadOnly = true
Me.ec_oranno.OptionsFilter.AllowFilter = false
'
'ec_orserie
'
Me.ec_orserie.AppearanceCell.Options.UseBackColor = true
Me.ec_orserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_orserie.Caption = "Ord. serie"
Me.ec_orserie.Enabled = false
Me.ec_orserie.FieldName = "ec_orserie"
Me.ec_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_orserie.Name = "ec_orserie"
Me.ec_orserie.NTSRepositoryComboBox = Nothing
Me.ec_orserie.NTSRepositoryItemCheck = Nothing
Me.ec_orserie.NTSRepositoryItemMemo = Nothing
Me.ec_orserie.NTSRepositoryItemText = Nothing
Me.ec_orserie.OptionsColumn.AllowEdit = false
Me.ec_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_orserie.OptionsColumn.ReadOnly = true
Me.ec_orserie.OptionsFilter.AllowFilter = false
'
'ec_ornum
'
Me.ec_ornum.AppearanceCell.Options.UseBackColor = true
Me.ec_ornum.AppearanceCell.Options.UseTextOptions = true
Me.ec_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ornum.Caption = "Ord. num"
Me.ec_ornum.Enabled = false
Me.ec_ornum.FieldName = "ec_ornum"
Me.ec_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ornum.Name = "ec_ornum"
Me.ec_ornum.NTSRepositoryComboBox = Nothing
Me.ec_ornum.NTSRepositoryItemCheck = Nothing
Me.ec_ornum.NTSRepositoryItemMemo = Nothing
Me.ec_ornum.NTSRepositoryItemText = Nothing
Me.ec_ornum.OptionsColumn.AllowEdit = false
Me.ec_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ornum.OptionsColumn.ReadOnly = true
Me.ec_ornum.OptionsFilter.AllowFilter = false
'
'ec_orriga
'
Me.ec_orriga.AppearanceCell.Options.UseBackColor = true
Me.ec_orriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_orriga.Caption = "Ord. riga"
Me.ec_orriga.Enabled = false
Me.ec_orriga.FieldName = "ec_orriga"
Me.ec_orriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_orriga.Name = "ec_orriga"
Me.ec_orriga.NTSRepositoryComboBox = Nothing
Me.ec_orriga.NTSRepositoryItemCheck = Nothing
Me.ec_orriga.NTSRepositoryItemMemo = Nothing
Me.ec_orriga.NTSRepositoryItemText = Nothing
Me.ec_orriga.OptionsColumn.AllowEdit = false
Me.ec_orriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_orriga.OptionsColumn.ReadOnly = true
Me.ec_orriga.OptionsFilter.AllowFilter = false
'
'ec_salcon
'
Me.ec_salcon.AppearanceCell.Options.UseBackColor = true
Me.ec_salcon.AppearanceCell.Options.UseTextOptions = true
Me.ec_salcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_salcon.Caption = "Saldo ordine"
Me.ec_salcon.Enabled = true
Me.ec_salcon.FieldName = "ec_salcon"
Me.ec_salcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_salcon.Name = "ec_salcon"
Me.ec_salcon.NTSRepositoryComboBox = Nothing
Me.ec_salcon.NTSRepositoryItemCheck = Nothing
Me.ec_salcon.NTSRepositoryItemMemo = Nothing
Me.ec_salcon.NTSRepositoryItemText = Nothing
Me.ec_salcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_salcon.OptionsFilter.AllowFilter = false
'
'ec_commen
'
Me.ec_commen.AppearanceCell.Options.UseBackColor = true
Me.ec_commen.AppearanceCell.Options.UseTextOptions = true
Me.ec_commen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_commen.Caption = "Num ordine (fat. proforma)"
Me.ec_commen.Enabled = false
Me.ec_commen.FieldName = "ec_commen"
Me.ec_commen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_commen.Name = "ec_commen"
Me.ec_commen.NTSRepositoryComboBox = Nothing
Me.ec_commen.NTSRepositoryItemCheck = Nothing
Me.ec_commen.NTSRepositoryItemMemo = Nothing
Me.ec_commen.NTSRepositoryItemText = Nothing
Me.ec_commen.OptionsColumn.AllowEdit = false
Me.ec_commen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_commen.OptionsColumn.ReadOnly = true
Me.ec_commen.OptionsFilter.AllowFilter = false
'
'ec_codnomc
'
Me.ec_codnomc.AppearanceCell.Options.UseBackColor = true
Me.ec_codnomc.AppearanceCell.Options.UseTextOptions = true
Me.ec_codnomc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codnomc.Caption = "Cod. nomenclatura comb."
Me.ec_codnomc.Enabled = true
Me.ec_codnomc.FieldName = "ec_codnomc"
Me.ec_codnomc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codnomc.Name = "ec_codnomc"
Me.ec_codnomc.NTSRepositoryComboBox = Nothing
Me.ec_codnomc.NTSRepositoryItemCheck = Nothing
Me.ec_codnomc.NTSRepositoryItemMemo = Nothing
Me.ec_codnomc.NTSRepositoryItemText = Nothing
Me.ec_codnomc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codnomc.OptionsFilter.AllowFilter = false
'
'ec_massakg
'
Me.ec_massakg.AppearanceCell.Options.UseBackColor = true
Me.ec_massakg.AppearanceCell.Options.UseTextOptions = true
Me.ec_massakg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_massakg.Caption = "Massa Kg"
Me.ec_massakg.Enabled = true
Me.ec_massakg.FieldName = "ec_massakg"
Me.ec_massakg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_massakg.Name = "ec_massakg"
Me.ec_massakg.NTSRepositoryComboBox = Nothing
Me.ec_massakg.NTSRepositoryItemCheck = Nothing
Me.ec_massakg.NTSRepositoryItemMemo = Nothing
Me.ec_massakg.NTSRepositoryItemText = Nothing
Me.ec_massakg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_massakg.OptionsFilter.AllowFilter = false
'
'ec_massaum2
'
Me.ec_massaum2.AppearanceCell.Options.UseBackColor = true
Me.ec_massaum2.AppearanceCell.Options.UseTextOptions = true
Me.ec_massaum2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_massaum2.Caption = "Massa Um 2"
Me.ec_massaum2.Enabled = true
Me.ec_massaum2.FieldName = "ec_massaum2"
Me.ec_massaum2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_massaum2.Name = "ec_massaum2"
Me.ec_massaum2.NTSRepositoryComboBox = Nothing
Me.ec_massaum2.NTSRepositoryItemCheck = Nothing
Me.ec_massaum2.NTSRepositoryItemMemo = Nothing
Me.ec_massaum2.NTSRepositoryItemText = Nothing
Me.ec_massaum2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_massaum2.OptionsFilter.AllowFilter = false
'
'ec_valstat
'
Me.ec_valstat.AppearanceCell.Options.UseBackColor = true
Me.ec_valstat.AppearanceCell.Options.UseTextOptions = true
Me.ec_valstat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_valstat.Caption = "Valore statistico"
Me.ec_valstat.Enabled = true
Me.ec_valstat.FieldName = "ec_valstat"
Me.ec_valstat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_valstat.Name = "ec_valstat"
Me.ec_valstat.NTSRepositoryComboBox = Nothing
Me.ec_valstat.NTSRepositoryItemCheck = Nothing
Me.ec_valstat.NTSRepositoryItemMemo = Nothing
Me.ec_valstat.NTSRepositoryItemText = Nothing
Me.ec_valstat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_valstat.OptionsFilter.AllowFilter = false
'
'ec_proorig
'
Me.ec_proorig.AppearanceCell.Options.UseBackColor = true
Me.ec_proorig.AppearanceCell.Options.UseTextOptions = true
Me.ec_proorig.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_proorig.Caption = "Prov. orig./paese pagam."
Me.ec_proorig.Enabled = true
Me.ec_proorig.FieldName = "ec_proorig"
Me.ec_proorig.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_proorig.Name = "ec_proorig"
Me.ec_proorig.NTSRepositoryComboBox = Nothing
Me.ec_proorig.NTSRepositoryItemCheck = Nothing
Me.ec_proorig.NTSRepositoryItemMemo = Nothing
Me.ec_proorig.NTSRepositoryItemText = Nothing
Me.ec_proorig.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_proorig.OptionsFilter.AllowFilter = false
'
'ec_percvst
'
Me.ec_percvst.AppearanceCell.Options.UseBackColor = true
Me.ec_percvst.AppearanceCell.Options.UseTextOptions = true
Me.ec_percvst.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_percvst.Caption = "Perc. valore stat."
Me.ec_percvst.Enabled = true
Me.ec_percvst.FieldName = "ec_percvst"
Me.ec_percvst.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_percvst.Name = "ec_percvst"
Me.ec_percvst.NTSRepositoryComboBox = Nothing
Me.ec_percvst.NTSRepositoryItemCheck = Nothing
Me.ec_percvst.NTSRepositoryItemMemo = Nothing
Me.ec_percvst.NTSRepositoryItemText = Nothing
Me.ec_percvst.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_percvst.OptionsFilter.AllowFilter = false
'
'ec_prtipo
'
Me.ec_prtipo.AppearanceCell.Options.UseBackColor = true
Me.ec_prtipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_prtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prtipo.Caption = "Imp. tipo"
Me.ec_prtipo.Enabled = false
Me.ec_prtipo.FieldName = "ec_prtipo"
Me.ec_prtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prtipo.Name = "ec_prtipo"
Me.ec_prtipo.NTSRepositoryComboBox = Nothing
Me.ec_prtipo.NTSRepositoryItemCheck = Nothing
Me.ec_prtipo.NTSRepositoryItemMemo = Nothing
Me.ec_prtipo.NTSRepositoryItemText = Nothing
Me.ec_prtipo.OptionsColumn.AllowEdit = false
Me.ec_prtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prtipo.OptionsColumn.ReadOnly = true
Me.ec_prtipo.OptionsFilter.AllowFilter = false
'
'ec_pranno
'
Me.ec_pranno.AppearanceCell.Options.UseBackColor = true
Me.ec_pranno.AppearanceCell.Options.UseTextOptions = true
Me.ec_pranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_pranno.Caption = "Imp anno"
Me.ec_pranno.Enabled = false
Me.ec_pranno.FieldName = "ec_pranno"
Me.ec_pranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_pranno.Name = "ec_pranno"
Me.ec_pranno.NTSRepositoryComboBox = Nothing
Me.ec_pranno.NTSRepositoryItemCheck = Nothing
Me.ec_pranno.NTSRepositoryItemMemo = Nothing
Me.ec_pranno.NTSRepositoryItemText = Nothing
Me.ec_pranno.OptionsColumn.AllowEdit = false
Me.ec_pranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_pranno.OptionsColumn.ReadOnly = true
Me.ec_pranno.OptionsFilter.AllowFilter = false
'
'ec_prserie
'
Me.ec_prserie.AppearanceCell.Options.UseBackColor = true
Me.ec_prserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_prserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prserie.Caption = "Imp. serie"
Me.ec_prserie.Enabled = false
Me.ec_prserie.FieldName = "ec_prserie"
Me.ec_prserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prserie.Name = "ec_prserie"
Me.ec_prserie.NTSRepositoryComboBox = Nothing
Me.ec_prserie.NTSRepositoryItemCheck = Nothing
Me.ec_prserie.NTSRepositoryItemMemo = Nothing
Me.ec_prserie.NTSRepositoryItemText = Nothing
Me.ec_prserie.OptionsColumn.AllowEdit = false
Me.ec_prserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prserie.OptionsColumn.ReadOnly = true
Me.ec_prserie.OptionsFilter.AllowFilter = false
'
'ec_prnum
'
Me.ec_prnum.AppearanceCell.Options.UseBackColor = true
Me.ec_prnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_prnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prnum.Caption = "Imp. num"
Me.ec_prnum.Enabled = false
Me.ec_prnum.FieldName = "ec_prnum"
Me.ec_prnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prnum.Name = "ec_prnum"
Me.ec_prnum.NTSRepositoryComboBox = Nothing
Me.ec_prnum.NTSRepositoryItemCheck = Nothing
Me.ec_prnum.NTSRepositoryItemMemo = Nothing
Me.ec_prnum.NTSRepositoryItemText = Nothing
Me.ec_prnum.OptionsColumn.AllowEdit = false
Me.ec_prnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prnum.OptionsColumn.ReadOnly = true
Me.ec_prnum.OptionsFilter.AllowFilter = false
'
'ec_prriga
'
Me.ec_prriga.AppearanceCell.Options.UseBackColor = true
Me.ec_prriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_prriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prriga.Caption = "Imp. riga"
Me.ec_prriga.Enabled = false
Me.ec_prriga.FieldName = "ec_prriga"
Me.ec_prriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prriga.Name = "ec_prriga"
Me.ec_prriga.NTSRepositoryComboBox = Nothing
Me.ec_prriga.NTSRepositoryItemCheck = Nothing
Me.ec_prriga.NTSRepositoryItemMemo = Nothing
Me.ec_prriga.NTSRepositoryItemText = Nothing
Me.ec_prriga.OptionsColumn.AllowEdit = false
Me.ec_prriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prriga.OptionsColumn.ReadOnly = true
Me.ec_prriga.OptionsFilter.AllowFilter = false
'
'ec_cltipo
'
Me.ec_cltipo.AppearanceCell.Options.UseBackColor = true
Me.ec_cltipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_cltipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cltipo.Caption = "C/lav. tipo"
Me.ec_cltipo.Enabled = true
Me.ec_cltipo.FieldName = "ec_cltipo"
Me.ec_cltipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cltipo.Name = "ec_cltipo"
Me.ec_cltipo.NTSRepositoryComboBox = Nothing
Me.ec_cltipo.NTSRepositoryItemCheck = Nothing
Me.ec_cltipo.NTSRepositoryItemMemo = Nothing
Me.ec_cltipo.NTSRepositoryItemText = Nothing
Me.ec_cltipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cltipo.OptionsFilter.AllowFilter = false
'
'ec_clanno
'
Me.ec_clanno.AppearanceCell.Options.UseBackColor = true
Me.ec_clanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_clanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_clanno.Caption = "C/lav. anno"
Me.ec_clanno.Enabled = true
Me.ec_clanno.FieldName = "ec_clanno"
Me.ec_clanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_clanno.Name = "ec_clanno"
Me.ec_clanno.NTSRepositoryComboBox = Nothing
Me.ec_clanno.NTSRepositoryItemCheck = Nothing
Me.ec_clanno.NTSRepositoryItemMemo = Nothing
Me.ec_clanno.NTSRepositoryItemText = Nothing
Me.ec_clanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_clanno.OptionsFilter.AllowFilter = false
'
'ec_clserie
'
Me.ec_clserie.AppearanceCell.Options.UseBackColor = true
Me.ec_clserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_clserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_clserie.Caption = "C/lav. serie"
Me.ec_clserie.Enabled = true
Me.ec_clserie.FieldName = "ec_clserie"
Me.ec_clserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_clserie.Name = "ec_clserie"
Me.ec_clserie.NTSRepositoryComboBox = Nothing
Me.ec_clserie.NTSRepositoryItemCheck = Nothing
Me.ec_clserie.NTSRepositoryItemMemo = Nothing
Me.ec_clserie.NTSRepositoryItemText = Nothing
Me.ec_clserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_clserie.OptionsFilter.AllowFilter = false
'
'ec_clnum
'
Me.ec_clnum.AppearanceCell.Options.UseBackColor = true
Me.ec_clnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_clnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_clnum.Caption = "C/lav. num."
Me.ec_clnum.Enabled = true
Me.ec_clnum.FieldName = "ec_clnum"
Me.ec_clnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_clnum.Name = "ec_clnum"
Me.ec_clnum.NTSRepositoryComboBox = Nothing
Me.ec_clnum.NTSRepositoryItemCheck = Nothing
Me.ec_clnum.NTSRepositoryItemMemo = Nothing
Me.ec_clnum.NTSRepositoryItemText = Nothing
Me.ec_clnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_clnum.OptionsFilter.AllowFilter = false
'
'ec_clriga
'
Me.ec_clriga.AppearanceCell.Options.UseBackColor = true
Me.ec_clriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_clriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_clriga.Caption = "C/lav. riga"
Me.ec_clriga.Enabled = true
Me.ec_clriga.FieldName = "ec_clriga"
Me.ec_clriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_clriga.Name = "ec_clriga"
Me.ec_clriga.NTSRepositoryComboBox = Nothing
Me.ec_clriga.NTSRepositoryItemCheck = Nothing
Me.ec_clriga.NTSRepositoryItemMemo = Nothing
Me.ec_clriga.NTSRepositoryItemText = Nothing
Me.ec_clriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_clriga.OptionsFilter.AllowFilter = false
'
'ec_nptipo
'
Me.ec_nptipo.AppearanceCell.Options.UseBackColor = true
Me.ec_nptipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_nptipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_nptipo.Caption = "N.pr. tipo"
Me.ec_nptipo.Enabled = false
Me.ec_nptipo.FieldName = "ec_nptipo"
Me.ec_nptipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_nptipo.Name = "ec_nptipo"
Me.ec_nptipo.NTSRepositoryComboBox = Nothing
Me.ec_nptipo.NTSRepositoryItemCheck = Nothing
Me.ec_nptipo.NTSRepositoryItemMemo = Nothing
Me.ec_nptipo.NTSRepositoryItemText = Nothing
Me.ec_nptipo.OptionsColumn.AllowEdit = false
Me.ec_nptipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_nptipo.OptionsColumn.ReadOnly = true
Me.ec_nptipo.OptionsFilter.AllowFilter = false
'
'ec_npanno
'
Me.ec_npanno.AppearanceCell.Options.UseBackColor = true
Me.ec_npanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_npanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npanno.Caption = "N.pr. anno"
Me.ec_npanno.Enabled = false
Me.ec_npanno.FieldName = "ec_npanno"
Me.ec_npanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npanno.Name = "ec_npanno"
Me.ec_npanno.NTSRepositoryComboBox = Nothing
Me.ec_npanno.NTSRepositoryItemCheck = Nothing
Me.ec_npanno.NTSRepositoryItemMemo = Nothing
Me.ec_npanno.NTSRepositoryItemText = Nothing
Me.ec_npanno.OptionsColumn.AllowEdit = false
Me.ec_npanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npanno.OptionsColumn.ReadOnly = true
Me.ec_npanno.OptionsFilter.AllowFilter = false
'
'ec_npserie
'
Me.ec_npserie.AppearanceCell.Options.UseBackColor = true
Me.ec_npserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_npserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npserie.Caption = "N.pr. serie"
Me.ec_npserie.Enabled = false
Me.ec_npserie.FieldName = "ec_npserie"
Me.ec_npserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npserie.Name = "ec_npserie"
Me.ec_npserie.NTSRepositoryComboBox = Nothing
Me.ec_npserie.NTSRepositoryItemCheck = Nothing
Me.ec_npserie.NTSRepositoryItemMemo = Nothing
Me.ec_npserie.NTSRepositoryItemText = Nothing
Me.ec_npserie.OptionsColumn.AllowEdit = false
Me.ec_npserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npserie.OptionsColumn.ReadOnly = true
Me.ec_npserie.OptionsFilter.AllowFilter = false
'
'ec_npnum
'
Me.ec_npnum.AppearanceCell.Options.UseBackColor = true
Me.ec_npnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_npnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npnum.Caption = "N.pr. num"
Me.ec_npnum.Enabled = false
Me.ec_npnum.FieldName = "ec_npnum"
Me.ec_npnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npnum.Name = "ec_npnum"
Me.ec_npnum.NTSRepositoryComboBox = Nothing
Me.ec_npnum.NTSRepositoryItemCheck = Nothing
Me.ec_npnum.NTSRepositoryItemMemo = Nothing
Me.ec_npnum.NTSRepositoryItemText = Nothing
Me.ec_npnum.OptionsColumn.AllowEdit = false
Me.ec_npnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npnum.OptionsColumn.ReadOnly = true
Me.ec_npnum.OptionsFilter.AllowFilter = false
'
'ec_npriga
'
Me.ec_npriga.AppearanceCell.Options.UseBackColor = true
Me.ec_npriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_npriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npriga.Caption = "N.pr. riga"
Me.ec_npriga.Enabled = false
Me.ec_npriga.FieldName = "ec_npriga"
Me.ec_npriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npriga.Name = "ec_npriga"
Me.ec_npriga.NTSRepositoryComboBox = Nothing
Me.ec_npriga.NTSRepositoryItemCheck = Nothing
Me.ec_npriga.NTSRepositoryItemMemo = Nothing
Me.ec_npriga.NTSRepositoryItemText = Nothing
Me.ec_npriga.OptionsColumn.AllowEdit = false
Me.ec_npriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npriga.OptionsColumn.ReadOnly = true
Me.ec_npriga.OptionsFilter.AllowFilter = false
'
'ec_npqtadis
'
Me.ec_npqtadis.AppearanceCell.Options.UseBackColor = true
Me.ec_npqtadis.AppearanceCell.Options.UseTextOptions = true
Me.ec_npqtadis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npqtadis.Caption = "N.pr. qta disimp."
Me.ec_npqtadis.Enabled = false
Me.ec_npqtadis.FieldName = "ec_npqtadis"
Me.ec_npqtadis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npqtadis.Name = "ec_npqtadis"
Me.ec_npqtadis.NTSRepositoryComboBox = Nothing
Me.ec_npqtadis.NTSRepositoryItemCheck = Nothing
Me.ec_npqtadis.NTSRepositoryItemMemo = Nothing
Me.ec_npqtadis.NTSRepositoryItemText = Nothing
Me.ec_npqtadis.OptionsColumn.AllowEdit = false
Me.ec_npqtadis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npqtadis.OptionsColumn.ReadOnly = true
Me.ec_npqtadis.OptionsFilter.AllowFilter = false
'
'ec_npcoldis
'
Me.ec_npcoldis.AppearanceCell.Options.UseBackColor = true
Me.ec_npcoldis.AppearanceCell.Options.UseTextOptions = true
Me.ec_npcoldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npcoldis.Caption = "N.pr. colli disimp."
Me.ec_npcoldis.Enabled = false
Me.ec_npcoldis.FieldName = "ec_npcoldis"
Me.ec_npcoldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npcoldis.Name = "ec_npcoldis"
Me.ec_npcoldis.NTSRepositoryComboBox = Nothing
Me.ec_npcoldis.NTSRepositoryItemCheck = Nothing
Me.ec_npcoldis.NTSRepositoryItemMemo = Nothing
Me.ec_npcoldis.NTSRepositoryItemText = Nothing
Me.ec_npcoldis.OptionsColumn.AllowEdit = false
Me.ec_npcoldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npcoldis.OptionsColumn.ReadOnly = true
Me.ec_npcoldis.OptionsFilter.AllowFilter = false
'
'ec_npvaldis
'
Me.ec_npvaldis.AppearanceCell.Options.UseBackColor = true
Me.ec_npvaldis.AppearanceCell.Options.UseTextOptions = true
Me.ec_npvaldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npvaldis.Caption = "N.pr. val. disimp."
Me.ec_npvaldis.Enabled = false
Me.ec_npvaldis.FieldName = "ec_npvaldis"
Me.ec_npvaldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npvaldis.Name = "ec_npvaldis"
Me.ec_npvaldis.NTSRepositoryComboBox = Nothing
Me.ec_npvaldis.NTSRepositoryItemCheck = Nothing
Me.ec_npvaldis.NTSRepositoryItemMemo = Nothing
Me.ec_npvaldis.NTSRepositoryItemText = Nothing
Me.ec_npvaldis.OptionsColumn.AllowEdit = false
Me.ec_npvaldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npvaldis.OptionsColumn.ReadOnly = true
Me.ec_npvaldis.OptionsFilter.AllowFilter = false
'
'ec_npsalcon
'
Me.ec_npsalcon.AppearanceCell.Options.UseBackColor = true
Me.ec_npsalcon.AppearanceCell.Options.UseTextOptions = true
Me.ec_npsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_npsalcon.Caption = "N.pr. evasa"
Me.ec_npsalcon.Enabled = true
Me.ec_npsalcon.FieldName = "ec_npsalcon"
Me.ec_npsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_npsalcon.Name = "ec_npsalcon"
Me.ec_npsalcon.NTSRepositoryComboBox = Nothing
Me.ec_npsalcon.NTSRepositoryItemCheck = Nothing
Me.ec_npsalcon.NTSRepositoryItemMemo = Nothing
Me.ec_npsalcon.NTSRepositoryItemText = Nothing
Me.ec_npsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_npsalcon.OptionsFilter.AllowFilter = false
'
'ec_nprcoleva
'
Me.ec_nprcoleva.AppearanceCell.Options.UseBackColor = true
Me.ec_nprcoleva.AppearanceCell.Options.UseTextOptions = true
Me.ec_nprcoleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_nprcoleva.Caption = "N.Pr.Colli evasi"
Me.ec_nprcoleva.Enabled = false
Me.ec_nprcoleva.FieldName = "ec_nprcoleva"
Me.ec_nprcoleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_nprcoleva.Name = "ec_nprcoleva"
Me.ec_nprcoleva.NTSRepositoryComboBox = Nothing
Me.ec_nprcoleva.NTSRepositoryItemCheck = Nothing
Me.ec_nprcoleva.NTSRepositoryItemMemo = Nothing
Me.ec_nprcoleva.NTSRepositoryItemText = Nothing
Me.ec_nprcoleva.OptionsColumn.AllowEdit = false
Me.ec_nprcoleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_nprcoleva.OptionsColumn.ReadOnly = true
Me.ec_nprcoleva.OptionsFilter.AllowFilter = false
'
'ec_nprquaeva
'
Me.ec_nprquaeva.AppearanceCell.Options.UseBackColor = true
Me.ec_nprquaeva.AppearanceCell.Options.UseTextOptions = true
Me.ec_nprquaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_nprquaeva.Caption = "N.Pr.Q.tà evasa"
Me.ec_nprquaeva.Enabled = false
Me.ec_nprquaeva.FieldName = "ec_nprquaeva"
Me.ec_nprquaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_nprquaeva.Name = "ec_nprquaeva"
Me.ec_nprquaeva.NTSRepositoryComboBox = Nothing
Me.ec_nprquaeva.NTSRepositoryItemCheck = Nothing
Me.ec_nprquaeva.NTSRepositoryItemMemo = Nothing
Me.ec_nprquaeva.NTSRepositoryItemText = Nothing
Me.ec_nprquaeva.OptionsColumn.AllowEdit = false
Me.ec_nprquaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_nprquaeva.OptionsColumn.ReadOnly = true
Me.ec_nprquaeva.OptionsFilter.AllowFilter = false
'
'ec_nprflevas
'
Me.ec_nprflevas.AppearanceCell.Options.UseBackColor = true
Me.ec_nprflevas.AppearanceCell.Options.UseTextOptions = true
Me.ec_nprflevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_nprflevas.Caption = "N.Pr.Evaso"
Me.ec_nprflevas.Enabled = true
Me.ec_nprflevas.FieldName = "ec_nprflevas"
Me.ec_nprflevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_nprflevas.Name = "ec_nprflevas"
Me.ec_nprflevas.NTSRepositoryComboBox = Nothing
Me.ec_nprflevas.NTSRepositoryItemCheck = Nothing
Me.ec_nprflevas.NTSRepositoryItemMemo = Nothing
Me.ec_nprflevas.NTSRepositoryItemText = Nothing
Me.ec_nprflevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_nprflevas.OptionsFilter.AllowFilter = false
'
'ec_nprvalore
'
Me.ec_nprvalore.AppearanceCell.Options.UseBackColor = true
Me.ec_nprvalore.AppearanceCell.Options.UseTextOptions = true
Me.ec_nprvalore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_nprvalore.Caption = "Valore nota prel."
Me.ec_nprvalore.Enabled = false
Me.ec_nprvalore.FieldName = "ec_nprvalore"
Me.ec_nprvalore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_nprvalore.Name = "ec_nprvalore"
Me.ec_nprvalore.NTSRepositoryComboBox = Nothing
Me.ec_nprvalore.NTSRepositoryItemCheck = Nothing
Me.ec_nprvalore.NTSRepositoryItemMemo = Nothing
Me.ec_nprvalore.NTSRepositoryItemText = Nothing
Me.ec_nprvalore.OptionsColumn.AllowEdit = false
Me.ec_nprvalore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_nprvalore.OptionsColumn.ReadOnly = true
Me.ec_nprvalore.OptionsFilter.AllowFilter = false
'
'xxo_vallordo
'
Me.xxo_vallordo.AppearanceCell.Options.UseBackColor = true
Me.xxo_vallordo.AppearanceCell.Options.UseTextOptions = true
Me.xxo_vallordo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_vallordo.Caption = "Valore riga lordo"
Me.xxo_vallordo.CustomizationCaption = "xxo_vallordo"
Me.xxo_vallordo.Enabled = true
Me.xxo_vallordo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_vallordo.Name = "xxo_vallordo"
Me.xxo_vallordo.NTSRepositoryComboBox = Nothing
Me.xxo_vallordo.NTSRepositoryItemCheck = Nothing
Me.xxo_vallordo.NTSRepositoryItemMemo = Nothing
Me.xxo_vallordo.NTSRepositoryItemText = Nothing
Me.xxo_vallordo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_vallordo.OptionsFilter.AllowFilter = false
'
'ec_actipo
'
Me.ec_actipo.AppearanceCell.Options.UseBackColor = true
Me.ec_actipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_actipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_actipo.Caption = "Acconto tipo"
Me.ec_actipo.Enabled = false
Me.ec_actipo.FieldName = "ec_actipo"
Me.ec_actipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_actipo.Name = "ec_actipo"
Me.ec_actipo.NTSRepositoryComboBox = Nothing
Me.ec_actipo.NTSRepositoryItemCheck = Nothing
Me.ec_actipo.NTSRepositoryItemMemo = Nothing
Me.ec_actipo.NTSRepositoryItemText = Nothing
Me.ec_actipo.OptionsColumn.AllowEdit = false
Me.ec_actipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_actipo.OptionsColumn.ReadOnly = true
Me.ec_actipo.OptionsFilter.AllowFilter = false
'
'ec_acanno
'
Me.ec_acanno.AppearanceCell.Options.UseBackColor = true
Me.ec_acanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_acanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_acanno.Caption = "Acconto anno"
Me.ec_acanno.Enabled = false
Me.ec_acanno.FieldName = "ec_acanno"
Me.ec_acanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_acanno.Name = "ec_acanno"
Me.ec_acanno.NTSRepositoryComboBox = Nothing
Me.ec_acanno.NTSRepositoryItemCheck = Nothing
Me.ec_acanno.NTSRepositoryItemMemo = Nothing
Me.ec_acanno.NTSRepositoryItemText = Nothing
Me.ec_acanno.OptionsColumn.AllowEdit = false
Me.ec_acanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_acanno.OptionsColumn.ReadOnly = true
Me.ec_acanno.OptionsFilter.AllowFilter = false
'
'ec_acserie
'
Me.ec_acserie.AppearanceCell.Options.UseBackColor = true
Me.ec_acserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_acserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_acserie.Caption = "Acconto serie"
Me.ec_acserie.Enabled = false
Me.ec_acserie.FieldName = "ec_acserie"
Me.ec_acserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_acserie.Name = "ec_acserie"
Me.ec_acserie.NTSRepositoryComboBox = Nothing
Me.ec_acserie.NTSRepositoryItemCheck = Nothing
Me.ec_acserie.NTSRepositoryItemMemo = Nothing
Me.ec_acserie.NTSRepositoryItemText = Nothing
Me.ec_acserie.OptionsColumn.AllowEdit = false
Me.ec_acserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_acserie.OptionsColumn.ReadOnly = true
Me.ec_acserie.OptionsFilter.AllowFilter = false
'
'ec_acnum
'
Me.ec_acnum.AppearanceCell.Options.UseBackColor = true
Me.ec_acnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_acnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_acnum.Caption = "Acconto num"
Me.ec_acnum.Enabled = false
Me.ec_acnum.FieldName = "ec_acnum"
Me.ec_acnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_acnum.Name = "ec_acnum"
Me.ec_acnum.NTSRepositoryComboBox = Nothing
Me.ec_acnum.NTSRepositoryItemCheck = Nothing
Me.ec_acnum.NTSRepositoryItemMemo = Nothing
Me.ec_acnum.NTSRepositoryItemText = Nothing
Me.ec_acnum.OptionsColumn.AllowEdit = false
Me.ec_acnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_acnum.OptionsColumn.ReadOnly = true
Me.ec_acnum.OptionsFilter.AllowFilter = false
'
'ec_cctipo
'
Me.ec_cctipo.AppearanceCell.Options.UseBackColor = true
Me.ec_cctipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_cctipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cctipo.Caption = "Car.prod. tipo"
Me.ec_cctipo.Enabled = true
Me.ec_cctipo.FieldName = "ec_cctipo"
Me.ec_cctipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cctipo.Name = "ec_cctipo"
Me.ec_cctipo.NTSRepositoryComboBox = Nothing
Me.ec_cctipo.NTSRepositoryItemCheck = Nothing
Me.ec_cctipo.NTSRepositoryItemMemo = Nothing
Me.ec_cctipo.NTSRepositoryItemText = Nothing
Me.ec_cctipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cctipo.OptionsFilter.AllowFilter = false
'
'ec_ccanno
'
Me.ec_ccanno.AppearanceCell.Options.UseBackColor = true
Me.ec_ccanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_ccanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ccanno.Caption = "Car.prod. anno"
Me.ec_ccanno.Enabled = true
Me.ec_ccanno.FieldName = "ec_ccanno"
Me.ec_ccanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ccanno.Name = "ec_ccanno"
Me.ec_ccanno.NTSRepositoryComboBox = Nothing
Me.ec_ccanno.NTSRepositoryItemCheck = Nothing
Me.ec_ccanno.NTSRepositoryItemMemo = Nothing
Me.ec_ccanno.NTSRepositoryItemText = Nothing
Me.ec_ccanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ccanno.OptionsFilter.AllowFilter = false
'
'ec_ccserie
'
Me.ec_ccserie.AppearanceCell.Options.UseBackColor = true
Me.ec_ccserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_ccserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ccserie.Caption = "Car.prod. serie"
Me.ec_ccserie.Enabled = true
Me.ec_ccserie.FieldName = "ec_ccserie"
Me.ec_ccserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ccserie.Name = "ec_ccserie"
Me.ec_ccserie.NTSRepositoryComboBox = Nothing
Me.ec_ccserie.NTSRepositoryItemCheck = Nothing
Me.ec_ccserie.NTSRepositoryItemMemo = Nothing
Me.ec_ccserie.NTSRepositoryItemText = Nothing
Me.ec_ccserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ccserie.OptionsFilter.AllowFilter = false
'
'ec_ccnum
'
Me.ec_ccnum.AppearanceCell.Options.UseBackColor = true
Me.ec_ccnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_ccnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ccnum.Caption = "Car.prod. num"
Me.ec_ccnum.Enabled = true
Me.ec_ccnum.FieldName = "ec_ccnum"
Me.ec_ccnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ccnum.Name = "ec_ccnum"
Me.ec_ccnum.NTSRepositoryComboBox = Nothing
Me.ec_ccnum.NTSRepositoryItemCheck = Nothing
Me.ec_ccnum.NTSRepositoryItemMemo = Nothing
Me.ec_ccnum.NTSRepositoryItemText = Nothing
Me.ec_ccnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ccnum.OptionsFilter.AllowFilter = false
'
'ec_ccriga
'
Me.ec_ccriga.AppearanceCell.Options.UseBackColor = true
Me.ec_ccriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_ccriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_ccriga.Caption = "Car.prod. riga"
Me.ec_ccriga.Enabled = true
Me.ec_ccriga.FieldName = "ec_ccriga"
Me.ec_ccriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_ccriga.Name = "ec_ccriga"
Me.ec_ccriga.NTSRepositoryComboBox = Nothing
Me.ec_ccriga.NTSRepositoryItemCheck = Nothing
Me.ec_ccriga.NTSRepositoryItemMemo = Nothing
Me.ec_ccriga.NTSRepositoryItemText = Nothing
Me.ec_ccriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_ccriga.OptionsFilter.AllowFilter = false
'
'xxo_valrigaic
'
Me.xxo_valrigaic.AppearanceCell.Options.UseBackColor = true
Me.xxo_valrigaic.AppearanceCell.Options.UseTextOptions = true
Me.xxo_valrigaic.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_valrigaic.Caption = "Val. riga Iva comp."
Me.xxo_valrigaic.Enabled = false
Me.xxo_valrigaic.FieldName = "xxo_valrigaic"
Me.xxo_valrigaic.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_valrigaic.Name = "xxo_valrigaic"
Me.xxo_valrigaic.NTSRepositoryComboBox = Nothing
Me.xxo_valrigaic.NTSRepositoryItemCheck = Nothing
Me.xxo_valrigaic.NTSRepositoryItemMemo = Nothing
Me.xxo_valrigaic.NTSRepositoryItemText = Nothing
Me.xxo_valrigaic.OptionsColumn.AllowEdit = false
Me.xxo_valrigaic.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_valrigaic.OptionsColumn.ReadOnly = true
Me.xxo_valrigaic.OptionsFilter.AllowFilter = false
'
'ec_flcom
'
Me.ec_flcom.AppearanceCell.Options.UseBackColor = true
Me.ec_flcom.AppearanceCell.Options.UseTextOptions = true
Me.ec_flcom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flcom.Caption = "Da controllare"
Me.ec_flcom.Enabled = true
Me.ec_flcom.FieldName = "ec_flcom"
Me.ec_flcom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flcom.Name = "ec_flcom"
Me.ec_flcom.NTSRepositoryComboBox = Nothing
Me.ec_flcom.NTSRepositoryItemCheck = Nothing
Me.ec_flcom.NTSRepositoryItemMemo = Nothing
Me.ec_flcom.NTSRepositoryItemText = Nothing
Me.ec_flcom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flcom.OptionsFilter.AllowFilter = false
'
'ec_flelab
'
Me.ec_flelab.AppearanceCell.Options.UseBackColor = true
Me.ec_flelab.AppearanceCell.Options.UseTextOptions = true
Me.ec_flelab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flelab.Caption = "Controllato"
Me.ec_flelab.Enabled = true
Me.ec_flelab.FieldName = "ec_flelab"
Me.ec_flelab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flelab.Name = "ec_flelab"
Me.ec_flelab.NTSRepositoryComboBox = Nothing
Me.ec_flelab.NTSRepositoryItemCheck = Nothing
Me.ec_flelab.NTSRepositoryItemMemo = Nothing
Me.ec_flelab.NTSRepositoryItemText = Nothing
Me.ec_flelab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flelab.OptionsFilter.AllowFilter = false
'
'ec_valorev
'
Me.ec_valorev.AppearanceCell.Options.UseBackColor = true
Me.ec_valorev.AppearanceCell.Options.UseTextOptions = true
Me.ec_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_valorev.Caption = "Valore in val."
Me.ec_valorev.Enabled = false
Me.ec_valorev.FieldName = "ec_valorev"
Me.ec_valorev.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_valorev.Name = "ec_valorev"
Me.ec_valorev.NTSRepositoryComboBox = Nothing
Me.ec_valorev.NTSRepositoryItemCheck = Nothing
Me.ec_valorev.NTSRepositoryItemMemo = Nothing
Me.ec_valorev.NTSRepositoryItemText = Nothing
Me.ec_valorev.OptionsColumn.AllowEdit = false
Me.ec_valorev.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_valorev.OptionsColumn.ReadOnly = true
Me.ec_valorev.OptionsFilter.AllowFilter = false
'
'ec_coddivi
'
Me.ec_coddivi.AppearanceCell.Options.UseBackColor = true
Me.ec_coddivi.AppearanceCell.Options.UseTextOptions = true
Me.ec_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_coddivi.Caption = "Divisione CA"
Me.ec_coddivi.Enabled = true
Me.ec_coddivi.FieldName = "ec_coddivi"
Me.ec_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_coddivi.Name = "ec_coddivi"
Me.ec_coddivi.NTSRepositoryComboBox = Nothing
Me.ec_coddivi.NTSRepositoryItemCheck = Nothing
Me.ec_coddivi.NTSRepositoryItemMemo = Nothing
Me.ec_coddivi.NTSRepositoryItemText = Nothing
Me.ec_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_coddivi.OptionsFilter.AllowFilter = false
'
'xxo_coddivi
'
Me.xxo_coddivi.AppearanceCell.Options.UseBackColor = true
Me.xxo_coddivi.AppearanceCell.Options.UseTextOptions = true
Me.xxo_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_coddivi.Caption = "Descr. divis. CA"
Me.xxo_coddivi.Enabled = false
Me.xxo_coddivi.FieldName = "xxo_coddivi"
Me.xxo_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_coddivi.Name = "xxo_coddivi"
Me.xxo_coddivi.NTSRepositoryComboBox = Nothing
Me.xxo_coddivi.NTSRepositoryItemCheck = Nothing
Me.xxo_coddivi.NTSRepositoryItemMemo = Nothing
Me.xxo_coddivi.NTSRepositoryItemText = Nothing
Me.xxo_coddivi.OptionsColumn.AllowEdit = false
Me.xxo_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_coddivi.OptionsColumn.ReadOnly = true
Me.xxo_coddivi.OptionsFilter.AllowFilter = false
'
'ec_cvtipo
'
Me.ec_cvtipo.AppearanceCell.Options.UseBackColor = true
Me.ec_cvtipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_cvtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cvtipo.Caption = "C/vis. tipo"
Me.ec_cvtipo.Enabled = false
Me.ec_cvtipo.FieldName = "ec_cvtipo"
Me.ec_cvtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cvtipo.Name = "ec_cvtipo"
Me.ec_cvtipo.NTSRepositoryComboBox = Nothing
Me.ec_cvtipo.NTSRepositoryItemCheck = Nothing
Me.ec_cvtipo.NTSRepositoryItemMemo = Nothing
Me.ec_cvtipo.NTSRepositoryItemText = Nothing
Me.ec_cvtipo.OptionsColumn.AllowEdit = false
Me.ec_cvtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cvtipo.OptionsColumn.ReadOnly = true
Me.ec_cvtipo.OptionsFilter.AllowFilter = false
'
'ec_cvanno
'
Me.ec_cvanno.AppearanceCell.Options.UseBackColor = true
Me.ec_cvanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_cvanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cvanno.Caption = "C/vis. anno"
Me.ec_cvanno.Enabled = false
Me.ec_cvanno.FieldName = "ec_cvanno"
Me.ec_cvanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cvanno.Name = "ec_cvanno"
Me.ec_cvanno.NTSRepositoryComboBox = Nothing
Me.ec_cvanno.NTSRepositoryItemCheck = Nothing
Me.ec_cvanno.NTSRepositoryItemMemo = Nothing
Me.ec_cvanno.NTSRepositoryItemText = Nothing
Me.ec_cvanno.OptionsColumn.AllowEdit = false
Me.ec_cvanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cvanno.OptionsColumn.ReadOnly = true
Me.ec_cvanno.OptionsFilter.AllowFilter = false
'
'ec_cvserie
'
Me.ec_cvserie.AppearanceCell.Options.UseBackColor = true
Me.ec_cvserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_cvserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cvserie.Caption = "C/Vis. serie"
Me.ec_cvserie.Enabled = false
Me.ec_cvserie.FieldName = "ec_cvserie"
Me.ec_cvserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cvserie.Name = "ec_cvserie"
Me.ec_cvserie.NTSRepositoryComboBox = Nothing
Me.ec_cvserie.NTSRepositoryItemCheck = Nothing
Me.ec_cvserie.NTSRepositoryItemMemo = Nothing
Me.ec_cvserie.NTSRepositoryItemText = Nothing
Me.ec_cvserie.OptionsColumn.AllowEdit = false
Me.ec_cvserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cvserie.OptionsColumn.ReadOnly = true
Me.ec_cvserie.OptionsFilter.AllowFilter = false
'
'ec_cvnum
'
Me.ec_cvnum.AppearanceCell.Options.UseBackColor = true
Me.ec_cvnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_cvnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cvnum.Caption = "C/Vis. num."
Me.ec_cvnum.Enabled = false
Me.ec_cvnum.FieldName = "ec_cvnum"
Me.ec_cvnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cvnum.Name = "ec_cvnum"
Me.ec_cvnum.NTSRepositoryComboBox = Nothing
Me.ec_cvnum.NTSRepositoryItemCheck = Nothing
Me.ec_cvnum.NTSRepositoryItemMemo = Nothing
Me.ec_cvnum.NTSRepositoryItemText = Nothing
Me.ec_cvnum.OptionsColumn.AllowEdit = false
Me.ec_cvnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cvnum.OptionsColumn.ReadOnly = true
Me.ec_cvnum.OptionsFilter.AllowFilter = false
'
'ec_cvriga
'
Me.ec_cvriga.AppearanceCell.Options.UseBackColor = true
Me.ec_cvriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_cvriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_cvriga.Caption = "C/Vis. riga"
Me.ec_cvriga.Enabled = false
Me.ec_cvriga.FieldName = "ec_cvriga"
Me.ec_cvriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_cvriga.Name = "ec_cvriga"
Me.ec_cvriga.NTSRepositoryComboBox = Nothing
Me.ec_cvriga.NTSRepositoryItemCheck = Nothing
Me.ec_cvriga.NTSRepositoryItemMemo = Nothing
Me.ec_cvriga.NTSRepositoryItemText = Nothing
Me.ec_cvriga.OptionsColumn.AllowEdit = false
Me.ec_cvriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_cvriga.OptionsColumn.ReadOnly = true
Me.ec_cvriga.OptionsFilter.AllowFilter = false
'
'xxo_codalt
'
Me.xxo_codalt.AppearanceCell.Options.UseBackColor = true
Me.xxo_codalt.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codalt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codalt.Caption = "Cod. art. alternativo"
Me.xxo_codalt.Enabled = false
Me.xxo_codalt.FieldName = "xxo_codalt"
Me.xxo_codalt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codalt.Name = "xxo_codalt"
Me.xxo_codalt.NTSRepositoryComboBox = Nothing
Me.xxo_codalt.NTSRepositoryItemCheck = Nothing
Me.xxo_codalt.NTSRepositoryItemMemo = Nothing
Me.xxo_codalt.NTSRepositoryItemText = Nothing
Me.xxo_codalt.OptionsColumn.AllowEdit = false
Me.xxo_codalt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codalt.OptionsColumn.ReadOnly = true
Me.xxo_codalt.OptionsFilter.AllowFilter = false
'
'ec_flprzmod
'
Me.ec_flprzmod.AppearanceCell.Options.UseBackColor = true
Me.ec_flprzmod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flprzmod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flprzmod.Caption = "Prezzo modificato"
Me.ec_flprzmod.Enabled = false
Me.ec_flprzmod.FieldName = "ec_flprzmod"
Me.ec_flprzmod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flprzmod.Name = "ec_flprzmod"
Me.ec_flprzmod.NTSRepositoryComboBox = Nothing
Me.ec_flprzmod.NTSRepositoryItemCheck = Nothing
Me.ec_flprzmod.NTSRepositoryItemMemo = Nothing
Me.ec_flprzmod.NTSRepositoryItemText = Nothing
Me.ec_flprzmod.OptionsColumn.AllowEdit = false
Me.ec_flprzmod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flprzmod.OptionsColumn.ReadOnly = true
Me.ec_flprzmod.OptionsFilter.AllowFilter = false
'
'ec_flsc1mod
'
Me.ec_flsc1mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc1mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc1mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc1mod.Caption = "Sconto 1 modificato"
Me.ec_flsc1mod.Enabled = false
Me.ec_flsc1mod.FieldName = "ec_flsc1mod"
Me.ec_flsc1mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc1mod.Name = "ec_flsc1mod"
Me.ec_flsc1mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc1mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc1mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc1mod.NTSRepositoryItemText = Nothing
Me.ec_flsc1mod.OptionsColumn.AllowEdit = false
Me.ec_flsc1mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc1mod.OptionsColumn.ReadOnly = true
Me.ec_flsc1mod.OptionsFilter.AllowFilter = false
'
'ec_flsc2mod
'
Me.ec_flsc2mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc2mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc2mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc2mod.Caption = "Sconto 2 modificato"
Me.ec_flsc2mod.Enabled = false
Me.ec_flsc2mod.FieldName = "ec_flsc2mod"
Me.ec_flsc2mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc2mod.Name = "ec_flsc2mod"
Me.ec_flsc2mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc2mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc2mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc2mod.NTSRepositoryItemText = Nothing
Me.ec_flsc2mod.OptionsColumn.AllowEdit = false
Me.ec_flsc2mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc2mod.OptionsColumn.ReadOnly = true
Me.ec_flsc2mod.OptionsFilter.AllowFilter = false
'
'ec_flsc3mod
'
Me.ec_flsc3mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc3mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc3mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc3mod.Caption = "Sconto 3 modificato"
Me.ec_flsc3mod.Enabled = false
Me.ec_flsc3mod.FieldName = "ec_flsc3mod"
Me.ec_flsc3mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc3mod.Name = "ec_flsc3mod"
Me.ec_flsc3mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc3mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc3mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc3mod.NTSRepositoryItemText = Nothing
Me.ec_flsc3mod.OptionsColumn.AllowEdit = false
Me.ec_flsc3mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc3mod.OptionsColumn.ReadOnly = true
Me.ec_flsc3mod.OptionsFilter.AllowFilter = false
'
'ec_flsc4mod
'
Me.ec_flsc4mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc4mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc4mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc4mod.Caption = "Sconto 4 modificato"
Me.ec_flsc4mod.Enabled = false
Me.ec_flsc4mod.FieldName = "ec_flsc4mod"
Me.ec_flsc4mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc4mod.Name = "ec_flsc4mod"
Me.ec_flsc4mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc4mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc4mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc4mod.NTSRepositoryItemText = Nothing
Me.ec_flsc4mod.OptionsColumn.AllowEdit = false
Me.ec_flsc4mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc4mod.OptionsColumn.ReadOnly = true
Me.ec_flsc4mod.OptionsFilter.AllowFilter = false
'
'ec_flsc5mod
'
Me.ec_flsc5mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc5mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc5mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc5mod.Caption = "Sconto 5 modificato"
Me.ec_flsc5mod.Enabled = false
Me.ec_flsc5mod.FieldName = "ec_flsc5mod"
Me.ec_flsc5mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc5mod.Name = "ec_flsc5mod"
Me.ec_flsc5mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc5mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc5mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc5mod.NTSRepositoryItemText = Nothing
Me.ec_flsc5mod.OptionsColumn.AllowEdit = false
Me.ec_flsc5mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc5mod.OptionsColumn.ReadOnly = true
Me.ec_flsc5mod.OptionsFilter.AllowFilter = false
'
'ec_flsc6mod
'
Me.ec_flsc6mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flsc6mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flsc6mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flsc6mod.Caption = "Sconto 6 modificato"
Me.ec_flsc6mod.Enabled = false
Me.ec_flsc6mod.FieldName = "ec_flsc6mod"
Me.ec_flsc6mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flsc6mod.Name = "ec_flsc6mod"
Me.ec_flsc6mod.NTSRepositoryComboBox = Nothing
Me.ec_flsc6mod.NTSRepositoryItemCheck = Nothing
Me.ec_flsc6mod.NTSRepositoryItemMemo = Nothing
Me.ec_flsc6mod.NTSRepositoryItemText = Nothing
Me.ec_flsc6mod.OptionsColumn.AllowEdit = false
Me.ec_flsc6mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flsc6mod.OptionsColumn.ReadOnly = true
Me.ec_flsc6mod.OptionsFilter.AllowFilter = false
'
'ec_flprov1mod
'
Me.ec_flprov1mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flprov1mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flprov1mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flprov1mod.Caption = "Provvigione agente 1 modificata"
Me.ec_flprov1mod.Enabled = false
Me.ec_flprov1mod.FieldName = "ec_flprov1mod"
Me.ec_flprov1mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flprov1mod.Name = "ec_flprov1mod"
Me.ec_flprov1mod.NTSRepositoryComboBox = Nothing
Me.ec_flprov1mod.NTSRepositoryItemCheck = Nothing
Me.ec_flprov1mod.NTSRepositoryItemMemo = Nothing
Me.ec_flprov1mod.NTSRepositoryItemText = Nothing
Me.ec_flprov1mod.OptionsColumn.AllowEdit = false
Me.ec_flprov1mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flprov1mod.OptionsColumn.ReadOnly = true
Me.ec_flprov1mod.OptionsFilter.AllowFilter = false
'
'ec_flprov2mod
'
Me.ec_flprov2mod.AppearanceCell.Options.UseBackColor = true
Me.ec_flprov2mod.AppearanceCell.Options.UseTextOptions = true
Me.ec_flprov2mod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flprov2mod.Caption = "Provvigione agente 2 modificata"
Me.ec_flprov2mod.Enabled = false
Me.ec_flprov2mod.FieldName = "ec_flprov2mod"
Me.ec_flprov2mod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flprov2mod.Name = "ec_flprov2mod"
Me.ec_flprov2mod.NTSRepositoryComboBox = Nothing
Me.ec_flprov2mod.NTSRepositoryItemCheck = Nothing
Me.ec_flprov2mod.NTSRepositoryItemMemo = Nothing
Me.ec_flprov2mod.NTSRepositoryItemText = Nothing
Me.ec_flprov2mod.OptionsColumn.AllowEdit = false
Me.ec_flprov2mod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flprov2mod.OptionsColumn.ReadOnly = true
Me.ec_flprov2mod.OptionsFilter.AllowFilter = false
'
'xxo_valoreiva
'
Me.xxo_valoreiva.AppearanceCell.Options.UseBackColor = true
Me.xxo_valoreiva.AppearanceCell.Options.UseTextOptions = true
Me.xxo_valoreiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_valoreiva.Caption = "Valore Ivato"
Me.xxo_valoreiva.Enabled = false
Me.xxo_valoreiva.FieldName = "xxo_valoreiva"
Me.xxo_valoreiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_valoreiva.Name = "xxo_valoreiva"
Me.xxo_valoreiva.NTSRepositoryComboBox = Nothing
Me.xxo_valoreiva.NTSRepositoryItemCheck = Nothing
Me.xxo_valoreiva.NTSRepositoryItemMemo = Nothing
Me.xxo_valoreiva.NTSRepositoryItemText = Nothing
Me.xxo_valoreiva.OptionsColumn.AllowEdit = false
Me.xxo_valoreiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_valoreiva.OptionsColumn.ReadOnly = true
Me.xxo_valoreiva.OptionsFilter.AllowFilter = false
'
'xxo_percricarico
'
Me.xxo_percricarico.AppearanceCell.Options.UseBackColor = true
Me.xxo_percricarico.AppearanceCell.Options.UseTextOptions = true
Me.xxo_percricarico.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_percricarico.Caption = "% Ricarico"
Me.xxo_percricarico.Enabled = false
Me.xxo_percricarico.FieldName = "xxo_percricarico"
Me.xxo_percricarico.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_percricarico.Name = "xxo_percricarico"
Me.xxo_percricarico.NTSRepositoryComboBox = Nothing
Me.xxo_percricarico.NTSRepositoryItemCheck = Nothing
Me.xxo_percricarico.NTSRepositoryItemMemo = Nothing
Me.xxo_percricarico.NTSRepositoryItemText = Nothing
Me.xxo_percricarico.OptionsColumn.AllowEdit = false
Me.xxo_percricarico.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_percricarico.OptionsColumn.ReadOnly = true
Me.xxo_percricarico.OptionsFilter.AllowFilter = false
'
'xxo_percmargine
'
Me.xxo_percmargine.AppearanceCell.Options.UseBackColor = true
Me.xxo_percmargine.AppearanceCell.Options.UseTextOptions = true
Me.xxo_percmargine.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_percmargine.Caption = "% Margine"
Me.xxo_percmargine.Enabled = false
Me.xxo_percmargine.FieldName = "xxo_percmargine"
Me.xxo_percmargine.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_percmargine.Name = "xxo_percmargine"
Me.xxo_percmargine.NTSRepositoryComboBox = Nothing
Me.xxo_percmargine.NTSRepositoryItemCheck = Nothing
Me.xxo_percmargine.NTSRepositoryItemMemo = Nothing
Me.xxo_percmargine.NTSRepositoryItemText = Nothing
Me.xxo_percmargine.OptionsColumn.AllowEdit = false
Me.xxo_percmargine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_percmargine.OptionsColumn.ReadOnly = true
Me.xxo_percmargine.OptionsFilter.AllowFilter = false
'
'xxo_marginevalore
'
Me.xxo_marginevalore.AppearanceCell.Options.UseBackColor = true
Me.xxo_marginevalore.AppearanceCell.Options.UseTextOptions = true
Me.xxo_marginevalore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_marginevalore.Caption = "Margine valore"
Me.xxo_marginevalore.Enabled = false
Me.xxo_marginevalore.FieldName = "xxo_marginevalore"
Me.xxo_marginevalore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_marginevalore.Name = "xxo_marginevalore"
Me.xxo_marginevalore.NTSRepositoryComboBox = Nothing
Me.xxo_marginevalore.NTSRepositoryItemCheck = Nothing
Me.xxo_marginevalore.NTSRepositoryItemMemo = Nothing
Me.xxo_marginevalore.NTSRepositoryItemText = Nothing
Me.xxo_marginevalore.OptionsColumn.AllowEdit = false
Me.xxo_marginevalore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_marginevalore.OptionsColumn.ReadOnly = true
Me.xxo_marginevalore.OptionsFilter.AllowFilter = false
'
'pnCorpo
'
Me.pnCorpo.AllowDrop = true
Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnCorpo.Appearance.Options.UseBackColor = true
Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnCorpo.Controls.Add(Me.lbBarcodeIn)
Me.pnCorpo.Controls.Add(Me.edBarcodeIn)
Me.pnCorpo.Controls.Add(Me.lbOrdinImpegn)
Me.pnCorpo.Controls.Add(Me.edOrdinato)
Me.pnCorpo.Controls.Add(Me.edImpegnato)
Me.pnCorpo.Controls.Add(Me.lbSconto)
Me.pnCorpo.Controls.Add(Me.edSconto)
Me.pnCorpo.Controls.Add(Me.edUltCost)
Me.pnCorpo.Controls.Add(Me.edPreList)
Me.pnCorpo.Controls.Add(Me.edDispNetta)
Me.pnCorpo.Controls.Add(Me.edDispon)
Me.pnCorpo.Controls.Add(Me.lbDispon)
Me.pnCorpo.Controls.Add(Me.lbPreList)
Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
Me.pnCorpo.Dock = System.Windows.Forms.DockStyle.Top
Me.pnCorpo.Location = New System.Drawing.Point(0, 0)
Me.pnCorpo.Name = "pnCorpo"
Me.pnCorpo.NTSActiveTrasparency = true
Me.pnCorpo.Size = New System.Drawing.Size(723, 43)
Me.pnCorpo.TabIndex = 1
'
'lbOrdinImpegn
'
Me.lbOrdinImpegn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbOrdinImpegn.AutoSize = true
Me.lbOrdinImpegn.BackColor = System.Drawing.Color.Transparent
Me.lbOrdinImpegn.Location = New System.Drawing.Point(392, 24)
Me.lbOrdinImpegn.Name = "lbOrdinImpegn"
Me.lbOrdinImpegn.NTSDbField = ""
Me.lbOrdinImpegn.Size = New System.Drawing.Size(109, 13)
Me.lbOrdinImpegn.TabIndex = 53
Me.lbOrdinImpegn.Text = "Impegnato / ordinato"
Me.lbOrdinImpegn.Tooltip = ""
Me.lbOrdinImpegn.UseMnemonic = false
'
'edOrdinato
'
Me.edOrdinato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edOrdinato.Cursor = System.Windows.Forms.Cursors.Default
Me.edOrdinato.EditValue = "0"
Me.edOrdinato.Enabled = false
Me.edOrdinato.Location = New System.Drawing.Point(618, 21)
Me.edOrdinato.Name = "edOrdinato"
Me.edOrdinato.NTSDbField = ""
Me.edOrdinato.NTSFormat = "0"
Me.edOrdinato.NTSForzaVisZoom = false
Me.edOrdinato.NTSOldValue = ""
Me.edOrdinato.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edOrdinato.Properties.Appearance.Options.UseBackColor = true
Me.edOrdinato.Properties.Appearance.Options.UseTextOptions = true
Me.edOrdinato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edOrdinato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edOrdinato.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edOrdinato.Properties.AutoHeight = false
Me.edOrdinato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edOrdinato.Properties.MaxLength = 65536
Me.edOrdinato.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edOrdinato.Size = New System.Drawing.Size(99, 20)
Me.edOrdinato.TabIndex = 52
'
'edImpegnato
'
Me.edImpegnato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edImpegnato.Cursor = System.Windows.Forms.Cursors.Default
Me.edImpegnato.EditValue = "0"
Me.edImpegnato.Enabled = false
Me.edImpegnato.Location = New System.Drawing.Point(508, 21)
Me.edImpegnato.Name = "edImpegnato"
Me.edImpegnato.NTSDbField = ""
Me.edImpegnato.NTSFormat = "0"
Me.edImpegnato.NTSForzaVisZoom = false
Me.edImpegnato.NTSOldValue = ""
Me.edImpegnato.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edImpegnato.Properties.Appearance.Options.UseBackColor = true
Me.edImpegnato.Properties.Appearance.Options.UseTextOptions = true
Me.edImpegnato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edImpegnato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edImpegnato.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edImpegnato.Properties.AutoHeight = false
Me.edImpegnato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edImpegnato.Properties.MaxLength = 65536
Me.edImpegnato.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edImpegnato.Size = New System.Drawing.Size(104, 20)
Me.edImpegnato.TabIndex = 51
'
'lbSconto
'
Me.lbSconto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbSconto.AutoSize = true
Me.lbSconto.BackColor = System.Drawing.Color.Transparent
Me.lbSconto.Location = New System.Drawing.Point(264, 24)
Me.lbSconto.Name = "lbSconto"
Me.lbSconto.NTSDbField = ""
Me.lbSconto.Size = New System.Drawing.Size(64, 13)
Me.lbSconto.TabIndex = 50
Me.lbSconto.Text = "Sc.Effettivo"
Me.lbSconto.Tooltip = ""
Me.lbSconto.UseMnemonic = false
'
'edSconto
'
Me.edSconto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edSconto.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edSconto.EditValue = "0"
Me.edSconto.Enabled = false
Me.edSconto.Location = New System.Drawing.Point(329, 21)
Me.edSconto.Name = "edSconto"
Me.edSconto.NTSDbField = ""
Me.edSconto.NTSFormat = "0"
Me.edSconto.NTSForzaVisZoom = false
Me.edSconto.NTSOldValue = ""
Me.edSconto.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edSconto.Properties.Appearance.Options.UseBackColor = true
Me.edSconto.Properties.Appearance.Options.UseTextOptions = true
Me.edSconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edSconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edSconto.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edSconto.Properties.AutoHeight = false
Me.edSconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edSconto.Properties.MaxLength = 65536
Me.edSconto.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edSconto.Size = New System.Drawing.Size(57, 20)
Me.edSconto.TabIndex = 49
'
'edUltCost
'
Me.edUltCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edUltCost.Cursor = System.Windows.Forms.Cursors.Default
Me.edUltCost.EditValue = "0"
Me.edUltCost.Enabled = false
Me.edUltCost.Location = New System.Drawing.Point(178, 21)
Me.edUltCost.Name = "edUltCost"
Me.edUltCost.NTSDbField = ""
Me.edUltCost.NTSFormat = "0"
Me.edUltCost.NTSForzaVisZoom = false
Me.edUltCost.NTSOldValue = ""
Me.edUltCost.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edUltCost.Properties.Appearance.Options.UseBackColor = true
Me.edUltCost.Properties.Appearance.Options.UseTextOptions = true
Me.edUltCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edUltCost.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edUltCost.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edUltCost.Properties.AutoHeight = false
Me.edUltCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edUltCost.Properties.MaxLength = 65536
Me.edUltCost.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edUltCost.Size = New System.Drawing.Size(80, 20)
Me.edUltCost.TabIndex = 48
'
'edPreList
'
Me.edPreList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edPreList.Cursor = System.Windows.Forms.Cursors.Default
Me.edPreList.EditValue = "0"
Me.edPreList.Enabled = false
Me.edPreList.Location = New System.Drawing.Point(96, 21)
Me.edPreList.Name = "edPreList"
Me.edPreList.NTSDbField = ""
Me.edPreList.NTSFormat = "0"
Me.edPreList.NTSForzaVisZoom = false
Me.edPreList.NTSOldValue = ""
Me.edPreList.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edPreList.Properties.Appearance.Options.UseBackColor = true
Me.edPreList.Properties.Appearance.Options.UseTextOptions = true
Me.edPreList.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edPreList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edPreList.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edPreList.Properties.AutoHeight = false
Me.edPreList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edPreList.Properties.MaxLength = 65536
Me.edPreList.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edPreList.Size = New System.Drawing.Size(76, 20)
Me.edPreList.TabIndex = 47
'
'edDispNetta
'
Me.edDispNetta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edDispNetta.Cursor = System.Windows.Forms.Cursors.Default
Me.edDispNetta.EditValue = "0"
Me.edDispNetta.Enabled = false
Me.edDispNetta.Location = New System.Drawing.Point(618, 1)
Me.edDispNetta.Name = "edDispNetta"
Me.edDispNetta.NTSDbField = ""
Me.edDispNetta.NTSFormat = "0"
Me.edDispNetta.NTSForzaVisZoom = false
Me.edDispNetta.NTSOldValue = ""
Me.edDispNetta.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edDispNetta.Properties.Appearance.Options.UseBackColor = true
Me.edDispNetta.Properties.Appearance.Options.UseTextOptions = true
Me.edDispNetta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edDispNetta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edDispNetta.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edDispNetta.Properties.AutoHeight = false
Me.edDispNetta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edDispNetta.Properties.MaxLength = 65536
Me.edDispNetta.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edDispNetta.Size = New System.Drawing.Size(99, 20)
Me.edDispNetta.TabIndex = 46
'
'edDispon
'
Me.edDispon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edDispon.Cursor = System.Windows.Forms.Cursors.Default
Me.edDispon.EditValue = "0"
Me.edDispon.Enabled = false
Me.edDispon.Location = New System.Drawing.Point(508, 1)
Me.edDispon.Name = "edDispon"
Me.edDispon.NTSDbField = ""
Me.edDispon.NTSFormat = "0"
Me.edDispon.NTSForzaVisZoom = false
Me.edDispon.NTSOldValue = ""
Me.edDispon.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edDispon.Properties.Appearance.Options.UseBackColor = true
Me.edDispon.Properties.Appearance.Options.UseTextOptions = true
Me.edDispon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edDispon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edDispon.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edDispon.Properties.AutoHeight = false
Me.edDispon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edDispon.Properties.MaxLength = 65536
Me.edDispon.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edDispon.Size = New System.Drawing.Size(104, 20)
Me.edDispon.TabIndex = 45
'
'lbDispon
'
Me.lbDispon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbDispon.AutoSize = true
Me.lbDispon.BackColor = System.Drawing.Color.Transparent
Me.lbDispon.Location = New System.Drawing.Point(392, 3)
Me.lbDispon.Name = "lbDispon"
Me.lbDispon.NTSDbField = ""
Me.lbDispon.Size = New System.Drawing.Size(110, 13)
Me.lbDispon.TabIndex = 5
Me.lbDispon.Text = "Disponib./ disp. netta"
Me.lbDispon.Tooltip = ""
Me.lbDispon.UseMnemonic = false
'
'lbPreList
'
Me.lbPreList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbPreList.AutoSize = true
Me.lbPreList.BackColor = System.Drawing.Color.Transparent
Me.lbPreList.Location = New System.Drawing.Point(4, 24)
Me.lbPreList.Name = "lbPreList"
Me.lbPreList.NTSDbField = ""
Me.lbPreList.Size = New System.Drawing.Size(86, 13)
Me.lbPreList.TabIndex = 3
Me.lbPreList.Text = "Listino/ult. costo"
Me.lbPreList.Tooltip = ""
Me.lbPreList.UseMnemonic = false
'
'TabPage1
'
Me.TabPage1.AllowDrop = true
Me.TabPage1.Controls.Add(Me.pnTestataDx)
Me.TabPage1.Controls.Add(Me.pnTestataSx)
Me.TabPage1.Enable = true
Me.TabPage1.Name = "TabPage1"
Me.TabPage1.Size = New System.Drawing.Size(723, 649)
Me.TabPage1.Text = "&1 - Testata"
'
'pnTestataDx
'
Me.pnTestataDx.AllowDrop = true
Me.pnTestataDx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTestataDx.Appearance.Options.UseBackColor = true
Me.pnTestataDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta2)
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta1)
Me.pnTestataDx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataDx.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnTestataDx.Location = New System.Drawing.Point(149, 0)
Me.pnTestataDx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnTestataDx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnTestataDx.Name = "pnTestataDx"
Me.pnTestataDx.NTSActiveTrasparency = true
Me.pnTestataDx.Size = New System.Drawing.Size(574, 649)
Me.pnTestataDx.TabIndex = 1
'
'pnDatiTesta2
'
Me.pnDatiTesta2.AllowDrop = true
Me.pnDatiTesta2.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta2.Appearance.Options.UseBackColor = true
Me.pnDatiTesta2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta2.Controls.Add(Me.fmPa)
Me.pnDatiTesta2.Controls.Add(Me.edEt_valuta)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_valuta)
Me.pnDatiTesta2.Controls.Add(Me.fmBanca)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_causale)
Me.pnDatiTesta2.Controls.Add(Me.fmOpzioni)
Me.pnDatiTesta2.Controls.Add(Me.edEt_codese)
Me.pnDatiTesta2.Controls.Add(Me.edEt_causale)
Me.pnDatiTesta2.Controls.Add(Me.edEt_cambio)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_codese)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_causale)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_magaz2)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_codese)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_valuta)
Me.pnDatiTesta2.Controls.Add(Me.edEt_controp)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_controp)
Me.pnDatiTesta2.Controls.Add(Me.edEt_magaz2)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_controp)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_magaz2)
Me.pnDatiTesta2.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta2.Location = New System.Drawing.Point(0, 252)
Me.pnDatiTesta2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta2.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta2.Name = "pnDatiTesta2"
Me.pnDatiTesta2.NTSActiveTrasparency = true
Me.pnDatiTesta2.Size = New System.Drawing.Size(573, 312)
Me.pnDatiTesta2.TabIndex = 69
Me.pnDatiTesta2.Text = "NtsPanel1"
'
'fmPa
'
Me.fmPa.AllowDrop = true
Me.fmPa.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmPa.Appearance.Options.UseBackColor = true
Me.fmPa.Controls.Add(Me.edEt_riferimpa)
Me.fmPa.Controls.Add(Me.lbEt_riferimpa)
Me.fmPa.Controls.Add(Me.edEt_cig)
Me.fmPa.Controls.Add(Me.edEt_cup)
Me.fmPa.Controls.Add(Me.lbEt_cig)
Me.fmPa.Controls.Add(Me.lbEt_cup)
Me.fmPa.Cursor = System.Windows.Forms.Cursors.Default
Me.fmPa.Location = New System.Drawing.Point(9, 208)
Me.fmPa.Name = "fmPa"
Me.fmPa.Size = New System.Drawing.Size(279, 100)
Me.fmPa.TabIndex = 112
Me.fmPa.Text = "Pubblica amministrazione"
'
'edEt_riferimpa
'
Me.edEt_riferimpa.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_riferimpa.EditValue = ""
Me.edEt_riferimpa.Location = New System.Drawing.Point(111, 66)
Me.edEt_riferimpa.Name = "edEt_riferimpa"
Me.edEt_riferimpa.NTSDbField = ""
Me.edEt_riferimpa.NTSForzaVisZoom = false
Me.edEt_riferimpa.NTSOldValue = ""
Me.edEt_riferimpa.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_riferimpa.Properties.Appearance.Options.UseBackColor = true
Me.edEt_riferimpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_riferimpa.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_riferimpa.Properties.AutoHeight = false
Me.edEt_riferimpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_riferimpa.Properties.MaxLength = 65536
Me.edEt_riferimpa.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_riferimpa.Size = New System.Drawing.Size(159, 20)
Me.edEt_riferimpa.TabIndex = 52
'
'lbEt_riferimpa
'
Me.lbEt_riferimpa.AutoSize = true
Me.lbEt_riferimpa.BackColor = System.Drawing.Color.Transparent
Me.lbEt_riferimpa.Location = New System.Drawing.Point(7, 69)
Me.lbEt_riferimpa.Name = "lbEt_riferimpa"
Me.lbEt_riferimpa.NTSDbField = ""
Me.lbEt_riferimpa.Size = New System.Drawing.Size(95, 13)
Me.lbEt_riferimpa.TabIndex = 51
Me.lbEt_riferimpa.Text = "Riferimento ordine"
Me.lbEt_riferimpa.Tooltip = ""
Me.lbEt_riferimpa.UseMnemonic = false
'
'edEt_cig
'
Me.edEt_cig.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_cig.EditValue = ""
Me.edEt_cig.Location = New System.Drawing.Point(111, 44)
Me.edEt_cig.Name = "edEt_cig"
Me.edEt_cig.NTSDbField = ""
Me.edEt_cig.NTSForzaVisZoom = false
Me.edEt_cig.NTSOldValue = ""
Me.edEt_cig.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_cig.Properties.Appearance.Options.UseBackColor = true
Me.edEt_cig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_cig.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_cig.Properties.AutoHeight = false
Me.edEt_cig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_cig.Properties.MaxLength = 65536
Me.edEt_cig.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_cig.Size = New System.Drawing.Size(159, 20)
Me.edEt_cig.TabIndex = 50
'
'edEt_cup
'
Me.edEt_cup.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_cup.EditValue = ""
Me.edEt_cup.Location = New System.Drawing.Point(111, 22)
Me.edEt_cup.Name = "edEt_cup"
Me.edEt_cup.NTSDbField = ""
Me.edEt_cup.NTSForzaVisZoom = false
Me.edEt_cup.NTSOldValue = ""
Me.edEt_cup.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_cup.Properties.Appearance.Options.UseBackColor = true
Me.edEt_cup.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_cup.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_cup.Properties.AutoHeight = false
Me.edEt_cup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_cup.Properties.MaxLength = 65536
Me.edEt_cup.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_cup.Size = New System.Drawing.Size(159, 20)
Me.edEt_cup.TabIndex = 49
'
'lbEt_cig
'
Me.lbEt_cig.BackColor = System.Drawing.Color.Transparent
Me.lbEt_cig.Location = New System.Drawing.Point(7, 47)
Me.lbEt_cig.Name = "lbEt_cig"
Me.lbEt_cig.NTSDbField = ""
Me.lbEt_cig.Size = New System.Drawing.Size(37, 13)
Me.lbEt_cig.TabIndex = 6
Me.lbEt_cig.Text = "Cig"
Me.lbEt_cig.Tooltip = ""
Me.lbEt_cig.UseMnemonic = false
'
'lbEt_cup
'
Me.lbEt_cup.BackColor = System.Drawing.Color.Transparent
Me.lbEt_cup.Location = New System.Drawing.Point(7, 25)
Me.lbEt_cup.Name = "lbEt_cup"
Me.lbEt_cup.NTSDbField = ""
Me.lbEt_cup.Size = New System.Drawing.Size(47, 13)
Me.lbEt_cup.TabIndex = 5
Me.lbEt_cup.Text = "Cup"
Me.lbEt_cup.Tooltip = ""
Me.lbEt_cup.UseMnemonic = false
'
'edEt_valuta
'
Me.edEt_valuta.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_valuta.EditValue = "0"
Me.edEt_valuta.Location = New System.Drawing.Point(97, 119)
Me.edEt_valuta.Name = "edEt_valuta"
Me.edEt_valuta.NTSDbField = ""
Me.edEt_valuta.NTSFormat = "0"
Me.edEt_valuta.NTSForzaVisZoom = false
Me.edEt_valuta.NTSOldValue = ""
Me.edEt_valuta.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_valuta.Properties.Appearance.Options.UseBackColor = true
Me.edEt_valuta.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_valuta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_valuta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_valuta.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_valuta.Properties.AutoHeight = false
Me.edEt_valuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_valuta.Properties.MaxLength = 65536
Me.edEt_valuta.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_valuta.Size = New System.Drawing.Size(53, 20)
Me.edEt_valuta.TabIndex = 54
'
'lbEt_valuta
'
Me.lbEt_valuta.AutoSize = true
Me.lbEt_valuta.BackColor = System.Drawing.Color.Transparent
Me.lbEt_valuta.Location = New System.Drawing.Point(6, 121)
Me.lbEt_valuta.Name = "lbEt_valuta"
Me.lbEt_valuta.NTSDbField = ""
Me.lbEt_valuta.Size = New System.Drawing.Size(74, 13)
Me.lbEt_valuta.TabIndex = 53
Me.lbEt_valuta.Text = "Valuta/cambio"
Me.lbEt_valuta.Tooltip = ""
Me.lbEt_valuta.UseMnemonic = false
'
'fmBanca
'
Me.fmBanca.AllowDrop = true
Me.fmBanca.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmBanca.Appearance.Options.UseBackColor = true
Me.fmBanca.Controls.Add(Me.lbXx_codbanc)
Me.fmBanca.Controls.Add(Me.edEt_codbanc)
Me.fmBanca.Controls.Add(Me.lbEt_codbanc)
Me.fmBanca.Controls.Add(Me.edEt_banc2)
Me.fmBanca.Controls.Add(Me.edEt_banc1)
Me.fmBanca.Controls.Add(Me.edEt_cab)
Me.fmBanca.Controls.Add(Me.edEt_abi)
Me.fmBanca.Controls.Add(Me.lbEt_cab)
Me.fmBanca.Controls.Add(Me.lbEt_abi)
Me.fmBanca.Cursor = System.Windows.Forms.Cursors.Default
Me.fmBanca.Location = New System.Drawing.Point(9, 26)
Me.fmBanca.Name = "fmBanca"
Me.fmBanca.Size = New System.Drawing.Size(550, 90)
Me.fmBanca.TabIndex = 6
Me.fmBanca.Text = "Banca d'appoggio"
'
'lbXx_codbanc
'
Me.lbXx_codbanc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbXx_codbanc.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codbanc.Location = New System.Drawing.Point(144, 66)
Me.lbXx_codbanc.Name = "lbXx_codbanc"
Me.lbXx_codbanc.NTSDbField = ""
Me.lbXx_codbanc.Size = New System.Drawing.Size(321, 20)
Me.lbXx_codbanc.TabIndex = 53
Me.lbXx_codbanc.Tooltip = ""
Me.lbXx_codbanc.UseMnemonic = false
'
'edEt_codbanc
'
Me.edEt_codbanc.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codbanc.EditValue = "0"
Me.edEt_codbanc.Location = New System.Drawing.Point(88, 66)
Me.edEt_codbanc.Name = "edEt_codbanc"
Me.edEt_codbanc.NTSDbField = ""
Me.edEt_codbanc.NTSFormat = "0"
Me.edEt_codbanc.NTSForzaVisZoom = false
Me.edEt_codbanc.NTSOldValue = ""
Me.edEt_codbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codbanc.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codbanc.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codbanc.Properties.AutoHeight = false
Me.edEt_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codbanc.Properties.MaxLength = 65536
Me.edEt_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codbanc.Size = New System.Drawing.Size(52, 20)
Me.edEt_codbanc.TabIndex = 52
'
'lbEt_codbanc
'
Me.lbEt_codbanc.AutoSize = true
Me.lbEt_codbanc.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codbanc.Location = New System.Drawing.Point(7, 69)
Me.lbEt_codbanc.Name = "lbEt_codbanc"
Me.lbEt_codbanc.NTSDbField = ""
Me.lbEt_codbanc.Size = New System.Drawing.Size(71, 13)
Me.lbEt_codbanc.TabIndex = 51
Me.lbEt_codbanc.Text = "Nostra Banca"
Me.lbEt_codbanc.Tooltip = ""
Me.lbEt_codbanc.UseMnemonic = false
'
'edEt_banc2
'
Me.edEt_banc2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edEt_banc2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_banc2.EditValue = ""
Me.edEt_banc2.Location = New System.Drawing.Point(144, 44)
Me.edEt_banc2.Name = "edEt_banc2"
Me.edEt_banc2.NTSDbField = ""
Me.edEt_banc2.NTSForzaVisZoom = false
Me.edEt_banc2.NTSOldValue = ""
Me.edEt_banc2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_banc2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_banc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_banc2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_banc2.Properties.AutoHeight = false
Me.edEt_banc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_banc2.Properties.MaxLength = 65536
Me.edEt_banc2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_banc2.Size = New System.Drawing.Size(401, 20)
Me.edEt_banc2.TabIndex = 50
'
'edEt_banc1
'
Me.edEt_banc1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edEt_banc1.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_banc1.EditValue = ""
Me.edEt_banc1.Location = New System.Drawing.Point(144, 22)
Me.edEt_banc1.Name = "edEt_banc1"
Me.edEt_banc1.NTSDbField = ""
Me.edEt_banc1.NTSForzaVisZoom = false
Me.edEt_banc1.NTSOldValue = ""
Me.edEt_banc1.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_banc1.Properties.Appearance.Options.UseBackColor = true
Me.edEt_banc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_banc1.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_banc1.Properties.AutoHeight = false
Me.edEt_banc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_banc1.Properties.MaxLength = 65536
Me.edEt_banc1.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_banc1.Size = New System.Drawing.Size(401, 20)
Me.edEt_banc1.TabIndex = 49
'
'edEt_cab
'
Me.edEt_cab.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_cab.EditValue = "0"
Me.edEt_cab.Location = New System.Drawing.Point(88, 44)
Me.edEt_cab.Name = "edEt_cab"
Me.edEt_cab.NTSDbField = ""
Me.edEt_cab.NTSFormat = "0"
Me.edEt_cab.NTSForzaVisZoom = false
Me.edEt_cab.NTSOldValue = ""
Me.edEt_cab.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_cab.Properties.Appearance.Options.UseBackColor = true
Me.edEt_cab.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_cab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_cab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_cab.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_cab.Properties.AutoHeight = false
Me.edEt_cab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_cab.Properties.MaxLength = 65536
Me.edEt_cab.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_cab.Size = New System.Drawing.Size(52, 20)
Me.edEt_cab.TabIndex = 48
'
'edEt_abi
'
Me.edEt_abi.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_abi.EditValue = "0"
Me.edEt_abi.Location = New System.Drawing.Point(88, 22)
Me.edEt_abi.Name = "edEt_abi"
Me.edEt_abi.NTSDbField = ""
Me.edEt_abi.NTSFormat = "0"
Me.edEt_abi.NTSForzaVisZoom = false
Me.edEt_abi.NTSOldValue = ""
Me.edEt_abi.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_abi.Properties.Appearance.Options.UseBackColor = true
Me.edEt_abi.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_abi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_abi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_abi.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_abi.Properties.AutoHeight = false
Me.edEt_abi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_abi.Properties.MaxLength = 65536
Me.edEt_abi.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_abi.Size = New System.Drawing.Size(52, 20)
Me.edEt_abi.TabIndex = 47
'
'lbEt_cab
'
Me.lbEt_cab.AutoSize = true
Me.lbEt_cab.BackColor = System.Drawing.Color.Transparent
Me.lbEt_cab.Location = New System.Drawing.Point(7, 47)
Me.lbEt_cab.Name = "lbEt_cab"
Me.lbEt_cab.NTSDbField = ""
Me.lbEt_cab.Size = New System.Drawing.Size(27, 13)
Me.lbEt_cab.TabIndex = 6
Me.lbEt_cab.Text = "CAB"
Me.lbEt_cab.Tooltip = ""
Me.lbEt_cab.UseMnemonic = false
'
'lbEt_abi
'
Me.lbEt_abi.AutoSize = true
Me.lbEt_abi.BackColor = System.Drawing.Color.Transparent
Me.lbEt_abi.Location = New System.Drawing.Point(7, 25)
Me.lbEt_abi.Name = "lbEt_abi"
Me.lbEt_abi.NTSDbField = ""
Me.lbEt_abi.Size = New System.Drawing.Size(24, 13)
Me.lbEt_abi.TabIndex = 5
Me.lbEt_abi.Text = "ABI"
Me.lbEt_abi.Tooltip = ""
Me.lbEt_abi.UseMnemonic = false
'
'lbXx_causale
'
Me.lbXx_causale.BackColor = System.Drawing.Color.Transparent
Me.lbXx_causale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_causale.Location = New System.Drawing.Point(151, 185)
Me.lbXx_causale.Name = "lbXx_causale"
Me.lbXx_causale.NTSDbField = ""
Me.lbXx_causale.Size = New System.Drawing.Size(404, 20)
Me.lbXx_causale.TabIndex = 92
Me.lbXx_causale.Tooltip = ""
Me.lbXx_causale.UseMnemonic = false
'
'fmOpzioni
'
Me.fmOpzioni.AllowDrop = true
Me.fmOpzioni.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmOpzioni.Appearance.Options.UseBackColor = true
Me.fmOpzioni.Controls.Add(Me.ckXx_przbol)
Me.fmOpzioni.Controls.Add(Me.ckEt_scorpo)
Me.fmOpzioni.Controls.Add(Me.ckEt_flspinc)
Me.fmOpzioni.Controls.Add(Me.ckEt_flboll)
Me.fmOpzioni.Controls.Add(Me.ckEt_proforma)
Me.fmOpzioni.Cursor = System.Windows.Forms.Cursors.Default
Me.fmOpzioni.Location = New System.Drawing.Point(340, 208)
Me.fmOpzioni.Name = "fmOpzioni"
Me.fmOpzioni.ShowCaption = false
Me.fmOpzioni.Size = New System.Drawing.Size(220, 100)
Me.fmOpzioni.TabIndex = 111
'
'ckXx_przbol
'
Me.ckXx_przbol.Cursor = System.Windows.Forms.Cursors.Default
Me.ckXx_przbol.Location = New System.Drawing.Point(10, 77)
Me.ckXx_przbol.Name = "ckXx_przbol"
Me.ckXx_przbol.NTSCheckValue = "S"
Me.ckXx_przbol.NTSUnCheckValue = "N"
Me.ckXx_przbol.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckXx_przbol.Properties.Appearance.Options.UseBackColor = true
Me.ckXx_przbol.Properties.AutoHeight = false
Me.ckXx_przbol.Properties.Caption = "Stampa prezzi in bolla"
Me.ckXx_przbol.Size = New System.Drawing.Size(135, 19)
Me.ckXx_przbol.TabIndex = 91
'
'ckEt_scorpo
'
Me.ckEt_scorpo.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_scorpo.Location = New System.Drawing.Point(10, 59)
Me.ckEt_scorpo.Name = "ckEt_scorpo"
Me.ckEt_scorpo.NTSCheckValue = "S"
Me.ckEt_scorpo.NTSUnCheckValue = "N"
Me.ckEt_scorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_scorpo.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_scorpo.Properties.AutoHeight = false
Me.ckEt_scorpo.Properties.Caption = "Scor&poro"
Me.ckEt_scorpo.Size = New System.Drawing.Size(79, 19)
Me.ckEt_scorpo.TabIndex = 0
'
'ckEt_flspinc
'
Me.ckEt_flspinc.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flspinc.Location = New System.Drawing.Point(10, 5)
Me.ckEt_flspinc.Name = "ckEt_flspinc"
Me.ckEt_flspinc.NTSCheckValue = "S"
Me.ckEt_flspinc.NTSUnCheckValue = "N"
Me.ckEt_flspinc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flspinc.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flspinc.Properties.AutoHeight = false
Me.ckEt_flspinc.Properties.Caption = "&Addebito spese &Incasso"
Me.ckEt_flspinc.Size = New System.Drawing.Size(148, 19)
Me.ckEt_flspinc.TabIndex = 1
'
'ckEt_flboll
'
Me.ckEt_flboll.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flboll.Location = New System.Drawing.Point(10, 23)
Me.ckEt_flboll.Name = "ckEt_flboll"
Me.ckEt_flboll.NTSCheckValue = "S"
Me.ckEt_flboll.NTSUnCheckValue = "N"
Me.ckEt_flboll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flboll.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flboll.Properties.AutoHeight = false
Me.ckEt_flboll.Properties.Caption = "&Addebito &Bolli"
Me.ckEt_flboll.Size = New System.Drawing.Size(98, 19)
Me.ckEt_flboll.TabIndex = 2
'
'ckEt_proforma
'
Me.ckEt_proforma.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_proforma.Location = New System.Drawing.Point(10, 41)
Me.ckEt_proforma.Name = "ckEt_proforma"
Me.ckEt_proforma.NTSCheckValue = "S"
Me.ckEt_proforma.NTSUnCheckValue = "N"
Me.ckEt_proforma.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_proforma.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_proforma.Properties.AutoHeight = false
Me.ckEt_proforma.Properties.Caption = "Proforma"
Me.ckEt_proforma.Size = New System.Drawing.Size(79, 19)
Me.ckEt_proforma.TabIndex = 3
'
'edEt_codese
'
Me.edEt_codese.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codese.EditValue = "0"
Me.edEt_codese.Location = New System.Drawing.Point(97, 141)
Me.edEt_codese.Name = "edEt_codese"
Me.edEt_codese.NTSDbField = ""
Me.edEt_codese.NTSFormat = "0"
Me.edEt_codese.NTSForzaVisZoom = false
Me.edEt_codese.NTSOldValue = ""
Me.edEt_codese.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codese.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codese.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codese.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codese.Properties.AutoHeight = false
Me.edEt_codese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codese.Properties.MaxLength = 65536
Me.edEt_codese.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codese.Size = New System.Drawing.Size(52, 20)
Me.edEt_codese.TabIndex = 52
'
'edEt_causale
'
Me.edEt_causale.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_causale.EditValue = "0"
Me.edEt_causale.Location = New System.Drawing.Point(97, 185)
Me.edEt_causale.Name = "edEt_causale"
Me.edEt_causale.NTSDbField = ""
Me.edEt_causale.NTSFormat = "0"
Me.edEt_causale.NTSForzaVisZoom = false
Me.edEt_causale.NTSOldValue = ""
Me.edEt_causale.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_causale.Properties.Appearance.Options.UseBackColor = true
Me.edEt_causale.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_causale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_causale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_causale.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_causale.Properties.AutoHeight = false
Me.edEt_causale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_causale.Properties.MaxLength = 65536
Me.edEt_causale.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_causale.Size = New System.Drawing.Size(52, 20)
Me.edEt_causale.TabIndex = 91
'
'edEt_cambio
'
Me.edEt_cambio.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_cambio.EditValue = "0"
Me.edEt_cambio.Location = New System.Drawing.Point(296, 118)
Me.edEt_cambio.Name = "edEt_cambio"
Me.edEt_cambio.NTSDbField = ""
Me.edEt_cambio.NTSFormat = "0"
Me.edEt_cambio.NTSForzaVisZoom = false
Me.edEt_cambio.NTSOldValue = ""
Me.edEt_cambio.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_cambio.Properties.Appearance.Options.UseBackColor = true
Me.edEt_cambio.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_cambio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_cambio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_cambio.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_cambio.Properties.AutoHeight = false
Me.edEt_cambio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_cambio.Properties.MaxLength = 65536
Me.edEt_cambio.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_cambio.Size = New System.Drawing.Size(94, 20)
Me.edEt_cambio.TabIndex = 57
'
'lbEt_codese
'
Me.lbEt_codese.AutoSize = true
Me.lbEt_codese.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codese.Location = New System.Drawing.Point(7, 142)
Me.lbEt_codese.Name = "lbEt_codese"
Me.lbEt_codese.NTSDbField = ""
Me.lbEt_codese.Size = New System.Drawing.Size(55, 13)
Me.lbEt_codese.TabIndex = 3
Me.lbEt_codese.Text = "Esenzione"
Me.lbEt_codese.Tooltip = ""
Me.lbEt_codese.UseMnemonic = false
'
'lbEt_causale
'
Me.lbEt_causale.AutoSize = true
Me.lbEt_causale.BackColor = System.Drawing.Color.Transparent
Me.lbEt_causale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_causale.Location = New System.Drawing.Point(7, 188)
Me.lbEt_causale.Name = "lbEt_causale"
Me.lbEt_causale.NTSDbField = ""
Me.lbEt_causale.Size = New System.Drawing.Size(88, 13)
Me.lbEt_causale.TabIndex = 90
Me.lbEt_causale.Text = "Causale magazz."
Me.lbEt_causale.Tooltip = ""
Me.lbEt_causale.UseMnemonic = false
'
'lbXx_magaz2
'
Me.lbXx_magaz2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_magaz2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_magaz2.Location = New System.Drawing.Point(153, 3)
Me.lbXx_magaz2.Name = "lbXx_magaz2"
Me.lbXx_magaz2.NTSDbField = ""
Me.lbXx_magaz2.Size = New System.Drawing.Size(401, 20)
Me.lbXx_magaz2.TabIndex = 63
Me.lbXx_magaz2.Tooltip = ""
Me.lbXx_magaz2.UseMnemonic = false
'
'lbXx_codese
'
Me.lbXx_codese.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codese.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codese.Location = New System.Drawing.Point(152, 141)
Me.lbXx_codese.Name = "lbXx_codese"
Me.lbXx_codese.NTSDbField = ""
Me.lbXx_codese.Size = New System.Drawing.Size(403, 20)
Me.lbXx_codese.TabIndex = 54
Me.lbXx_codese.Tooltip = ""
Me.lbXx_codese.UseMnemonic = false
'
'lbXx_valuta
'
Me.lbXx_valuta.BackColor = System.Drawing.Color.Transparent
Me.lbXx_valuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_valuta.Location = New System.Drawing.Point(153, 118)
Me.lbXx_valuta.Name = "lbXx_valuta"
Me.lbXx_valuta.NTSDbField = ""
Me.lbXx_valuta.Size = New System.Drawing.Size(137, 20)
Me.lbXx_valuta.TabIndex = 55
Me.lbXx_valuta.Tooltip = ""
Me.lbXx_valuta.UseMnemonic = false
'
'edEt_controp
'
Me.edEt_controp.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_controp.EditValue = "0"
Me.edEt_controp.Location = New System.Drawing.Point(97, 163)
Me.edEt_controp.Name = "edEt_controp"
Me.edEt_controp.NTSDbField = ""
Me.edEt_controp.NTSFormat = "0"
Me.edEt_controp.NTSForzaVisZoom = false
Me.edEt_controp.NTSOldValue = ""
Me.edEt_controp.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_controp.Properties.Appearance.Options.UseBackColor = true
Me.edEt_controp.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_controp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_controp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_controp.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_controp.Properties.AutoHeight = false
Me.edEt_controp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_controp.Properties.MaxLength = 65536
Me.edEt_controp.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_controp.Size = New System.Drawing.Size(51, 20)
Me.edEt_controp.TabIndex = 83
'
'lbEt_controp
'
Me.lbEt_controp.AutoSize = true
Me.lbEt_controp.BackColor = System.Drawing.Color.Transparent
Me.lbEt_controp.Location = New System.Drawing.Point(6, 166)
Me.lbEt_controp.Name = "lbEt_controp"
Me.lbEt_controp.NTSDbField = ""
Me.lbEt_controp.Size = New System.Drawing.Size(72, 13)
Me.lbEt_controp.TabIndex = 84
Me.lbEt_controp.Text = "Contropartita"
Me.lbEt_controp.Tooltip = ""
Me.lbEt_controp.UseMnemonic = false
'
'edEt_magaz2
'
Me.edEt_magaz2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_magaz2.EditValue = "0"
Me.edEt_magaz2.Location = New System.Drawing.Point(97, 3)
Me.edEt_magaz2.Name = "edEt_magaz2"
Me.edEt_magaz2.NTSDbField = ""
Me.edEt_magaz2.NTSFormat = "0"
Me.edEt_magaz2.NTSForzaVisZoom = false
Me.edEt_magaz2.NTSOldValue = ""
Me.edEt_magaz2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_magaz2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_magaz2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_magaz2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_magaz2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_magaz2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_magaz2.Properties.AutoHeight = false
Me.edEt_magaz2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_magaz2.Properties.MaxLength = 65536
Me.edEt_magaz2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_magaz2.Size = New System.Drawing.Size(53, 20)
Me.edEt_magaz2.TabIndex = 61
'
'lbXx_controp
'
Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_controp.Location = New System.Drawing.Point(151, 163)
Me.lbXx_controp.Name = "lbXx_controp"
Me.lbXx_controp.NTSDbField = ""
Me.lbXx_controp.Size = New System.Drawing.Size(403, 20)
Me.lbXx_controp.TabIndex = 85
Me.lbXx_controp.Tooltip = ""
Me.lbXx_controp.UseMnemonic = false
'
'lbEt_magaz2
'
Me.lbEt_magaz2.AutoSize = true
Me.lbEt_magaz2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_magaz2.Location = New System.Drawing.Point(6, 6)
Me.lbEt_magaz2.Name = "lbEt_magaz2"
Me.lbEt_magaz2.NTSDbField = ""
Me.lbEt_magaz2.Size = New System.Drawing.Size(66, 13)
Me.lbEt_magaz2.TabIndex = 59
Me.lbEt_magaz2.Text = "Magazzino 2"
Me.lbEt_magaz2.Tooltip = ""
Me.lbEt_magaz2.UseMnemonic = false
'
'pnDatiTesta1
'
Me.pnDatiTesta1.AllowDrop = true
Me.pnDatiTesta1.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta1.Appearance.Options.UseBackColor = true
Me.pnDatiTesta1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta1.Controls.Add(Me.lbet_datdoc)
Me.pnDatiTesta1.Controls.Add(Me.edet_datdoc)
Me.pnDatiTesta1.Controls.Add(Me.edEt_listino)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_listino)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_coddest)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_codpaga)
Me.pnDatiTesta1.Controls.Add(Me.edEt_conto)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_scopag)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_conto)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_datapag)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_coddest)
Me.pnDatiTesta1.Controls.Add(Me.lbScontiTesta)
Me.pnDatiTesta1.Controls.Add(Me.edEt_coddest)
Me.pnDatiTesta1.Controls.Add(Me.edEt_codpaga)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_conto)
Me.pnDatiTesta1.Controls.Add(Me.edEt_scopag)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_codpaga)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.edEt_datapag)
Me.pnDatiTesta1.Controls.Add(Me.edEt_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.edEt_scont2)
Me.pnDatiTesta1.Controls.Add(Me.edEt_riferim)
Me.pnDatiTesta1.Controls.Add(Me.edEt_scont1)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_riferim)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_codagen)
Me.pnDatiTesta1.Controls.Add(Me.edEt_magaz)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_codagen)
Me.pnDatiTesta1.Controls.Add(Me.edEt_codagen)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_magaz)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_magaz)
Me.pnDatiTesta1.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta1.Location = New System.Drawing.Point(0, 0)
Me.pnDatiTesta1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta1.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta1.Name = "pnDatiTesta1"
Me.pnDatiTesta1.NTSActiveTrasparency = true
Me.pnDatiTesta1.Size = New System.Drawing.Size(570, 248)
Me.pnDatiTesta1.TabIndex = 68
Me.pnDatiTesta1.Text = "NtsPanel1"
'
'edEt_listino
'
Me.edEt_listino.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_listino.EditValue = "0"
Me.edEt_listino.Location = New System.Drawing.Point(106, 159)
Me.edEt_listino.Name = "edEt_listino"
Me.edEt_listino.NTSDbField = ""
Me.edEt_listino.NTSFormat = "0"
Me.edEt_listino.NTSForzaVisZoom = false
Me.edEt_listino.NTSOldValue = ""
Me.edEt_listino.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_listino.Properties.Appearance.Options.UseBackColor = true
Me.edEt_listino.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_listino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_listino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_listino.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_listino.Properties.AutoHeight = false
Me.edEt_listino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_listino.Properties.MaxLength = 65536
Me.edEt_listino.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_listino.Size = New System.Drawing.Size(53, 20)
Me.edEt_listino.TabIndex = 46
'
'lbEt_listino
'
Me.lbEt_listino.AutoSize = true
Me.lbEt_listino.BackColor = System.Drawing.Color.Transparent
Me.lbEt_listino.Location = New System.Drawing.Point(7, 162)
Me.lbEt_listino.Name = "lbEt_listino"
Me.lbEt_listino.NTSDbField = ""
Me.lbEt_listino.Size = New System.Drawing.Size(37, 13)
Me.lbEt_listino.TabIndex = 1
Me.lbEt_listino.Text = "Listino"
Me.lbEt_listino.Tooltip = ""
Me.lbEt_listino.UseMnemonic = false
'
'lbEt_codpaga
'
Me.lbEt_codpaga.AutoSize = true
Me.lbEt_codpaga.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codpaga.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_codpaga.Location = New System.Drawing.Point(7, 184)
Me.lbEt_codpaga.Name = "lbEt_codpaga"
Me.lbEt_codpaga.NTSDbField = ""
Me.lbEt_codpaga.Size = New System.Drawing.Size(72, 13)
Me.lbEt_codpaga.TabIndex = 2
Me.lbEt_codpaga.Text = "Pagamento"
Me.lbEt_codpaga.Tooltip = ""
Me.lbEt_codpaga.UseMnemonic = false
'
'lbEt_scopag
'
Me.lbEt_scopag.AutoSize = true
Me.lbEt_scopag.BackColor = System.Drawing.Color.Transparent
Me.lbEt_scopag.Location = New System.Drawing.Point(7, 206)
Me.lbEt_scopag.Name = "lbEt_scopag"
Me.lbEt_scopag.NTSDbField = ""
Me.lbEt_scopag.Size = New System.Drawing.Size(97, 13)
Me.lbEt_scopag.TabIndex = 3
Me.lbEt_scopag.Text = "Sconto Pagamento"
Me.lbEt_scopag.Tooltip = ""
Me.lbEt_scopag.UseMnemonic = false
'
'lbEt_datapag
'
Me.lbEt_datapag.AutoSize = true
Me.lbEt_datapag.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datapag.Location = New System.Drawing.Point(196, 206)
Me.lbEt_datapag.Name = "lbEt_datapag"
Me.lbEt_datapag.NTSDbField = ""
Me.lbEt_datapag.Size = New System.Drawing.Size(101, 13)
Me.lbEt_datapag.TabIndex = 4
Me.lbEt_datapag.Text = "Data 1° pagamento"
Me.lbEt_datapag.Tooltip = ""
Me.lbEt_datapag.UseMnemonic = false
'
'lbScontiTesta
'
Me.lbScontiTesta.AutoSize = true
Me.lbScontiTesta.BackColor = System.Drawing.Color.Transparent
Me.lbScontiTesta.Location = New System.Drawing.Point(196, 162)
Me.lbScontiTesta.Name = "lbScontiTesta"
Me.lbScontiTesta.NTSDbField = ""
Me.lbScontiTesta.Size = New System.Drawing.Size(77, 13)
Me.lbScontiTesta.TabIndex = 7
Me.lbScontiTesta.Text = "Sconti generali"
Me.lbScontiTesta.Tooltip = ""
Me.lbScontiTesta.UseMnemonic = false
'
'edEt_codpaga
'
Me.edEt_codpaga.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codpaga.EditValue = "0"
Me.edEt_codpaga.Location = New System.Drawing.Point(106, 181)
Me.edEt_codpaga.Name = "edEt_codpaga"
Me.edEt_codpaga.NTSDbField = ""
Me.edEt_codpaga.NTSFormat = "0"
Me.edEt_codpaga.NTSForzaVisZoom = false
Me.edEt_codpaga.NTSOldValue = ""
Me.edEt_codpaga.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codpaga.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codpaga.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codpaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codpaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codpaga.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codpaga.Properties.AutoHeight = false
Me.edEt_codpaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codpaga.Properties.MaxLength = 65536
Me.edEt_codpaga.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codpaga.Size = New System.Drawing.Size(53, 20)
Me.edEt_codpaga.TabIndex = 47
'
'edEt_scopag
'
Me.edEt_scopag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scopag.EditValue = "0"
Me.edEt_scopag.Location = New System.Drawing.Point(106, 203)
Me.edEt_scopag.Name = "edEt_scopag"
Me.edEt_scopag.NTSDbField = ""
Me.edEt_scopag.NTSFormat = "0"
Me.edEt_scopag.NTSForzaVisZoom = false
Me.edEt_scopag.NTSOldValue = ""
Me.edEt_scopag.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_scopag.Properties.Appearance.Options.UseBackColor = true
Me.edEt_scopag.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_scopag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_scopag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_scopag.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_scopag.Properties.AutoHeight = false
Me.edEt_scopag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_scopag.Properties.MaxLength = 65536
Me.edEt_scopag.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_scopag.Size = New System.Drawing.Size(53, 20)
Me.edEt_scopag.TabIndex = 48
'
'lbXx_tipobf
'
Me.lbXx_tipobf.BackColor = System.Drawing.Color.Transparent
Me.lbXx_tipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_tipobf.Location = New System.Drawing.Point(165, 93)
Me.lbXx_tipobf.Name = "lbXx_tipobf"
Me.lbXx_tipobf.NTSDbField = ""
Me.lbXx_tipobf.Size = New System.Drawing.Size(395, 20)
Me.lbXx_tipobf.TabIndex = 49
Me.lbXx_tipobf.Tooltip = ""
Me.lbXx_tipobf.UseMnemonic = false
'
'lbXx_codpaga
'
Me.lbXx_codpaga.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codpaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codpaga.Location = New System.Drawing.Point(162, 181)
Me.lbXx_codpaga.Name = "lbXx_codpaga"
Me.lbXx_codpaga.NTSDbField = ""
Me.lbXx_codpaga.Size = New System.Drawing.Size(398, 20)
Me.lbXx_codpaga.TabIndex = 52
Me.lbXx_codpaga.Tooltip = ""
Me.lbXx_codpaga.UseMnemonic = false
'
'lbEt_tipobf
'
Me.lbEt_tipobf.AutoSize = true
Me.lbEt_tipobf.BackColor = System.Drawing.Color.Transparent
Me.lbEt_tipobf.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_tipobf.Location = New System.Drawing.Point(7, 96)
Me.lbEt_tipobf.Name = "lbEt_tipobf"
Me.lbEt_tipobf.NTSDbField = ""
Me.lbEt_tipobf.Size = New System.Drawing.Size(82, 13)
Me.lbEt_tipobf.TabIndex = 0
Me.lbEt_tipobf.Text = "Bolla/Fattura"
Me.lbEt_tipobf.Tooltip = ""
Me.lbEt_tipobf.UseMnemonic = false
'
'edEt_datapag
'
Me.edEt_datapag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_datapag.EditValue = "01/01/1900"
Me.edEt_datapag.Location = New System.Drawing.Point(303, 203)
Me.edEt_datapag.Name = "edEt_datapag"
Me.edEt_datapag.NTSDbField = ""
Me.edEt_datapag.NTSForzaVisZoom = false
Me.edEt_datapag.NTSOldValue = ""
Me.edEt_datapag.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_datapag.Properties.Appearance.Options.UseBackColor = true
Me.edEt_datapag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_datapag.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_datapag.Properties.AutoHeight = false
Me.edEt_datapag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_datapag.Properties.MaxLength = 65536
Me.edEt_datapag.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_datapag.Size = New System.Drawing.Size(94, 20)
Me.edEt_datapag.TabIndex = 49
'
'edEt_tipobf
'
Me.edEt_tipobf.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_tipobf.EditValue = "0"
Me.edEt_tipobf.Location = New System.Drawing.Point(106, 93)
Me.edEt_tipobf.Name = "edEt_tipobf"
Me.edEt_tipobf.NTSDbField = ""
Me.edEt_tipobf.NTSFormat = "0"
Me.edEt_tipobf.NTSForzaVisZoom = false
Me.edEt_tipobf.NTSOldValue = ""
Me.edEt_tipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_tipobf.Properties.Appearance.Options.UseBackColor = true
Me.edEt_tipobf.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_tipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_tipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_tipobf.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_tipobf.Properties.AutoHeight = false
Me.edEt_tipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_tipobf.Properties.MaxLength = 65536
Me.edEt_tipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_tipobf.Size = New System.Drawing.Size(53, 20)
Me.edEt_tipobf.TabIndex = 47
'
'edEt_scont2
'
Me.edEt_scont2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scont2.EditValue = "0"
Me.edEt_scont2.Location = New System.Drawing.Point(352, 159)
Me.edEt_scont2.Name = "edEt_scont2"
Me.edEt_scont2.NTSDbField = ""
Me.edEt_scont2.NTSFormat = "0"
Me.edEt_scont2.NTSForzaVisZoom = false
Me.edEt_scont2.NTSOldValue = ""
Me.edEt_scont2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_scont2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_scont2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_scont2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_scont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_scont2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_scont2.Properties.AutoHeight = false
Me.edEt_scont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_scont2.Properties.MaxLength = 65536
Me.edEt_scont2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_scont2.Size = New System.Drawing.Size(44, 20)
Me.edEt_scont2.TabIndex = 51
'
'edEt_riferim
'
Me.edEt_riferim.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_riferim.EditValue = ""
Me.edEt_riferim.Location = New System.Drawing.Point(106, 115)
Me.edEt_riferim.Name = "edEt_riferim"
Me.edEt_riferim.NTSDbField = ""
Me.edEt_riferim.NTSForzaVisZoom = false
Me.edEt_riferim.NTSOldValue = ""
Me.edEt_riferim.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_riferim.Properties.Appearance.Options.UseBackColor = true
Me.edEt_riferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_riferim.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_riferim.Properties.AutoHeight = false
Me.edEt_riferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_riferim.Properties.MaxLength = 65536
Me.edEt_riferim.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_riferim.Size = New System.Drawing.Size(454, 20)
Me.edEt_riferim.TabIndex = 57
'
'edEt_scont1
'
Me.edEt_scont1.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scont1.EditValue = "0"
Me.edEt_scont1.Location = New System.Drawing.Point(303, 159)
Me.edEt_scont1.Name = "edEt_scont1"
Me.edEt_scont1.NTSDbField = ""
Me.edEt_scont1.NTSFormat = "0"
Me.edEt_scont1.NTSForzaVisZoom = false
Me.edEt_scont1.NTSOldValue = ""
Me.edEt_scont1.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_scont1.Properties.Appearance.Options.UseBackColor = true
Me.edEt_scont1.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_scont1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_scont1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_scont1.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_scont1.Properties.AutoHeight = false
Me.edEt_scont1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_scont1.Properties.MaxLength = 65536
Me.edEt_scont1.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_scont1.Size = New System.Drawing.Size(44, 20)
Me.edEt_scont1.TabIndex = 50
'
'lbEt_riferim
'
Me.lbEt_riferim.AutoSize = true
Me.lbEt_riferim.BackColor = System.Drawing.Color.Transparent
Me.lbEt_riferim.Location = New System.Drawing.Point(7, 118)
Me.lbEt_riferim.Name = "lbEt_riferim"
Me.lbEt_riferim.NTSDbField = ""
Me.lbEt_riferim.Size = New System.Drawing.Size(58, 13)
Me.lbEt_riferim.TabIndex = 4
Me.lbEt_riferim.Text = "Riferimenti"
Me.lbEt_riferim.Tooltip = ""
Me.lbEt_riferim.UseMnemonic = false
'
'lbXx_codagen
'
Me.lbXx_codagen.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codagen.Location = New System.Drawing.Point(165, 137)
Me.lbXx_codagen.Name = "lbXx_codagen"
Me.lbXx_codagen.NTSDbField = ""
Me.lbXx_codagen.Size = New System.Drawing.Size(395, 20)
Me.lbXx_codagen.TabIndex = 50
Me.lbXx_codagen.Tooltip = ""
Me.lbXx_codagen.UseMnemonic = false
'
'edEt_magaz
'
Me.edEt_magaz.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_magaz.EditValue = "0"
Me.edEt_magaz.Location = New System.Drawing.Point(106, 225)
Me.edEt_magaz.Name = "edEt_magaz"
Me.edEt_magaz.NTSDbField = ""
Me.edEt_magaz.NTSFormat = "0"
Me.edEt_magaz.NTSForzaVisZoom = false
Me.edEt_magaz.NTSOldValue = ""
Me.edEt_magaz.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_magaz.Properties.Appearance.Options.UseBackColor = true
Me.edEt_magaz.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_magaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_magaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_magaz.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_magaz.Properties.AutoHeight = false
Me.edEt_magaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_magaz.Properties.MaxLength = 65536
Me.edEt_magaz.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_magaz.Size = New System.Drawing.Size(53, 20)
Me.edEt_magaz.TabIndex = 51
'
'lbEt_codagen
'
Me.lbEt_codagen.AutoSize = true
Me.lbEt_codagen.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codagen.Location = New System.Drawing.Point(7, 140)
Me.lbEt_codagen.Name = "lbEt_codagen"
Me.lbEt_codagen.NTSDbField = ""
Me.lbEt_codagen.Size = New System.Drawing.Size(42, 13)
Me.lbEt_codagen.TabIndex = 1
Me.lbEt_codagen.Text = "Agente"
Me.lbEt_codagen.Tooltip = ""
Me.lbEt_codagen.UseMnemonic = false
'
'edEt_codagen
'
Me.edEt_codagen.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codagen.EditValue = "0"
Me.edEt_codagen.Location = New System.Drawing.Point(106, 137)
Me.edEt_codagen.Name = "edEt_codagen"
Me.edEt_codagen.NTSDbField = ""
Me.edEt_codagen.NTSFormat = "0"
Me.edEt_codagen.NTSForzaVisZoom = false
Me.edEt_codagen.NTSOldValue = ""
Me.edEt_codagen.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codagen.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codagen.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codagen.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codagen.Properties.AutoHeight = false
Me.edEt_codagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codagen.Properties.MaxLength = 65536
Me.edEt_codagen.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codagen.Size = New System.Drawing.Size(52, 20)
Me.edEt_codagen.TabIndex = 48
'
'lbEt_magaz
'
Me.lbEt_magaz.AutoSize = true
Me.lbEt_magaz.BackColor = System.Drawing.Color.Transparent
Me.lbEt_magaz.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_magaz.Location = New System.Drawing.Point(7, 228)
Me.lbEt_magaz.Name = "lbEt_magaz"
Me.lbEt_magaz.NTSDbField = ""
Me.lbEt_magaz.Size = New System.Drawing.Size(77, 13)
Me.lbEt_magaz.TabIndex = 2
Me.lbEt_magaz.Text = "Magazzino 1"
Me.lbEt_magaz.Tooltip = ""
Me.lbEt_magaz.UseMnemonic = false
'
'lbXx_magaz
'
Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_magaz.Location = New System.Drawing.Point(162, 225)
Me.lbXx_magaz.Name = "lbXx_magaz"
Me.lbXx_magaz.NTSDbField = ""
Me.lbXx_magaz.Size = New System.Drawing.Size(398, 20)
Me.lbXx_magaz.TabIndex = 53
Me.lbXx_magaz.Tooltip = ""
Me.lbXx_magaz.UseMnemonic = false
'
'pnTestataSx
'
Me.pnTestataSx.AllowDrop = true
Me.pnTestataSx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTestataSx.Appearance.Options.UseBackColor = true
Me.pnTestataSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta2)
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta1)
Me.pnTestataSx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataSx.Dock = System.Windows.Forms.DockStyle.Left
Me.pnTestataSx.Location = New System.Drawing.Point(0, 0)
Me.pnTestataSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnTestataSx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnTestataSx.Name = "pnTestataSx"
Me.pnTestataSx.NTSActiveTrasparency = true
Me.pnTestataSx.Size = New System.Drawing.Size(149, 649)
Me.pnTestataSx.TabIndex = 0
'
'cmdDatiTesta2
'
Me.cmdDatiTesta2.ImagePath = ""
Me.cmdDatiTesta2.ImageText = ""
Me.cmdDatiTesta2.Location = New System.Drawing.Point(3, 36)
Me.cmdDatiTesta2.Name = "cmdDatiTesta2"
Me.cmdDatiTesta2.NTSContextMenu = Nothing
Me.cmdDatiTesta2.Size = New System.Drawing.Size(140, 28)
Me.cmdDatiTesta2.TabIndex = 127
Me.cmdDatiTesta2.Text = "Altri dati Testata"
'
'cmdDatiTesta1
'
Me.cmdDatiTesta1.ImagePath = ""
Me.cmdDatiTesta1.ImageText = ""
Me.cmdDatiTesta1.Location = New System.Drawing.Point(3, 6)
Me.cmdDatiTesta1.Name = "cmdDatiTesta1"
Me.cmdDatiTesta1.NTSContextMenu = Nothing
Me.cmdDatiTesta1.Size = New System.Drawing.Size(140, 28)
Me.cmdDatiTesta1.TabIndex = 126
Me.cmdDatiTesta1.Text = "Dati principali"
'
'tsBoll
'
Me.tsBoll.Dock = System.Windows.Forms.DockStyle.Fill
Me.tsBoll.Location = New System.Drawing.Point(0, 62)
Me.tsBoll.Name = "tsBoll"
Me.tsBoll.SelectedTabPage = Me.TabPage2
Me.tsBoll.Size = New System.Drawing.Size(732, 679)
Me.tsBoll.TabIndex = 45
Me.tsBoll.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage1, Me.TabPage2, Me.TabPage3, Me.TabPage5})
'
'NtsBarManager1
'
Me.NtsBarManager1.AllowCustomization = false
Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
Me.NtsBarManager1.Form = Me
Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRecordCancella, Me.tlbRecordRipristina, Me.tlbMnuRecord, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaPdf, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbNuovoDocDaPrec, Me.tlbStampaEtichetteRiga, Me.tlbStampaEtichetteFinale, Me.tlbDatiContabiliSintetici, Me.tlbMnuFile, Me.tlbNoteRiga, Me.tlbSelRigheOrdini, Me.tlbSelOrdini, Me.tlbModifNumDoc, Me.tlbImpostaFileTerm, Me.tlbNumerazioni, Me.tlbTabBolli, Me.tlbRicalcolaColliQta, Me.tlbCopiaRiga, Me.tlbIncollaRiga, Me.tlbSeleziona, Me.tlbSelezDaTerm, Me.tlbScarCollegati, Me.tlbLavCollegate, Me.tlbApriDB, Me.tlbPrecMov, Me.tlbSalvaCondPart, Me.tlbVisDispVariante, Me.tlbInsRiga, Me.tlbNavigazDoc, Me.tlbSelezAcconti, Me.tlbRegCassa, Me.tlbRettificaTotdoc, Me.tlbRicFiscDaRiem, Me.tlbScaricaMagaz, Me.tlbIntegraRighe, Me.tlbRigheOrdBarcode, Me.tlbApriOrdineCollegato, Me.tlbApriNotaCollegata, Me.tlbApriBollaCtoLavcollegata, Me.tlbApriCarProdCollegato, Me.tlbApriConfiguratore, Me.tlbGiub, Me.tlbSiub, Me.tlbSchedatrasporto, Me.tlbOrdinaRighe, Me.tlbOrdinaRigheRiga, Me.tlbOrdinaRigheCodiceArticolo, Me.tlbOrdinaRigheDescrizione, Me.tlbEmail, Me.tlbSalvanuovo, Me.tlbRiapriDopoSalva, Me.tlbExport, Me.tlbImport, Me.tlbSelectDeselect, Me.tlbDaLista, Me.tlbRiordinarighe, Me.tlbApriDocPrec, Me.tlbApriDocSucc, Me.tlbConfRegCassa, Me.tlbCancRigheSel, Me.tlbRicalcPrzScnPro, Me.tlbAggCorrFF})
Me.NtsBarManager1.MaxItemId = 126
'
'tlbMain
'
Me.tlbMain.BarName = "tlbMain"
Me.tlbMain.DockCol = 0
Me.tlbMain.DockRow = 0
Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDocPrec, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDocSucc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelOrdini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelRigheOrdini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMnuFile, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMnuRecord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
Me.tlbMain.OptionsBar.AllowQuickCustomization = false
Me.tlbMain.OptionsBar.DisableClose = true
Me.tlbMain.OptionsBar.DrawDragBorder = false
Me.tlbMain.OptionsBar.UseWholeRow = true
Me.tlbMain.Text = "tlbMain"
'
'tlbNuovo
'
Me.tlbNuovo.Caption = "Nuovo"
Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"),System.Drawing.Image)
Me.tlbNuovo.GlyphPath = ""
Me.tlbNuovo.Id = 0
Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
Me.tlbNuovo.Name = "tlbNuovo"
Me.tlbNuovo.Visible = true
'
'tlbApri
'
Me.tlbApri.Caption = "Apri"
Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"),System.Drawing.Image)
Me.tlbApri.GlyphPath = ""
Me.tlbApri.Id = 1
Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
Me.tlbApri.Name = "tlbApri"
Me.tlbApri.Visible = true
'
'tlbSalva
'
Me.tlbSalva.Caption = "Salva"
Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"),System.Drawing.Image)
Me.tlbSalva.GlyphPath = ""
Me.tlbSalva.Id = 2
Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
Me.tlbSalva.Name = "tlbSalva"
Me.tlbSalva.Visible = true
'
'tlbRipristina
'
Me.tlbRipristina.Caption = "Ripristina"
Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"),System.Drawing.Image)
Me.tlbRipristina.GlyphPath = ""
Me.tlbRipristina.Id = 4
Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
Me.tlbRipristina.Name = "tlbRipristina"
Me.tlbRipristina.Visible = true
'
'tlbCancella
'
Me.tlbCancella.Caption = "Cancella"
Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"),System.Drawing.Image)
Me.tlbCancella.GlyphPath = ""
Me.tlbCancella.Id = 3
Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
Me.tlbCancella.Name = "tlbCancella"
Me.tlbCancella.Visible = true
'
'tlbZoom
'
Me.tlbZoom.Caption = "Zoom"
Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"),System.Drawing.Image)
Me.tlbZoom.GlyphPath = ""
Me.tlbZoom.Id = 5
Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
Me.tlbZoom.Name = "tlbZoom"
Me.tlbZoom.Visible = true
'
'tlbApriDocPrec
'
Me.tlbApriDocPrec.Caption = "Passa a documento precedente"
Me.tlbApriDocPrec.Glyph = CType(resources.GetObject("tlbApriDocPrec.Glyph"),System.Drawing.Image)
Me.tlbApriDocPrec.GlyphPath = ""
Me.tlbApriDocPrec.Id = 120
Me.tlbApriDocPrec.Name = "tlbApriDocPrec"
Me.tlbApriDocPrec.Visible = true
'
'tlbApriDocSucc
'
Me.tlbApriDocSucc.Caption = "Passa a documento successivo"
Me.tlbApriDocSucc.Glyph = CType(resources.GetObject("tlbApriDocSucc.Glyph"),System.Drawing.Image)
Me.tlbApriDocSucc.GlyphPath = ""
Me.tlbApriDocSucc.Id = 121
Me.tlbApriDocSucc.Name = "tlbApriDocSucc"
Me.tlbApriDocSucc.Visible = true
'
'tlbRecordCancella
'
Me.tlbRecordCancella.Caption = "Cancella riga"
Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"),System.Drawing.Image)
Me.tlbRecordCancella.GlyphPath = ""
Me.tlbRecordCancella.Id = 6
Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
Me.tlbRecordCancella.Name = "tlbRecordCancella"
Me.tlbRecordCancella.Visible = true
'
'tlbRecordRipristina
'
Me.tlbRecordRipristina.Caption = "Ripristina riga"
Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"),System.Drawing.Image)
Me.tlbRecordRipristina.GlyphPath = ""
Me.tlbRecordRipristina.Id = 7
Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
Me.tlbRecordRipristina.Visible = true
'
'tlbSelOrdini
'
Me.tlbSelOrdini.Caption = "Seleziona Ordini"
Me.tlbSelOrdini.Glyph = CType(resources.GetObject("tlbSelOrdini.Glyph"),System.Drawing.Image)
Me.tlbSelOrdini.GlyphPath = ""
Me.tlbSelOrdini.Id = 30
Me.tlbSelOrdini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F3))
Me.tlbSelOrdini.Name = "tlbSelOrdini"
Me.tlbSelOrdini.Visible = true
'
'tlbSelRigheOrdini
'
Me.tlbSelRigheOrdini.Caption = "Seleziona righe d'ordine"
Me.tlbSelRigheOrdini.Glyph = CType(resources.GetObject("tlbSelRigheOrdini.Glyph"),System.Drawing.Image)
Me.tlbSelRigheOrdini.GlyphPath = ""
Me.tlbSelRigheOrdini.Id = 29
Me.tlbSelRigheOrdini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
Me.tlbSelRigheOrdini.Name = "tlbSelRigheOrdini"
Me.tlbSelRigheOrdini.Visible = true
'
'tlbMnuFile
'
Me.tlbMnuFile.Caption = "&Testata"
Me.tlbMnuFile.GlyphPath = ""
Me.tlbMnuFile.Id = 24
Me.tlbMnuFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovoDocDaPrec), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDatiContabiliSintetici), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbModifNumDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRettificaTotdoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcPrzScnPro), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicFiscDaRiem, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggCorrFF), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSchedatrasporto, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEmail)})
Me.tlbMnuFile.Name = "tlbMnuFile"
Me.tlbMnuFile.Visible = true
'
'tlbNuovoDocDaPrec
'
Me.tlbNuovoDocDaPrec.Caption = "Nuovo documento da precedente"
Me.tlbNuovoDocDaPrec.GlyphPath = ""
Me.tlbNuovoDocDaPrec.Id = 17
Me.tlbNuovoDocDaPrec.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
Me.tlbNuovoDocDaPrec.Name = "tlbNuovoDocDaPrec"
Me.tlbNuovoDocDaPrec.Visible = true
'
'tlbDatiContabiliSintetici
'
Me.tlbDatiContabiliSintetici.Caption = "Dati contabili sintetici"
Me.tlbDatiContabiliSintetici.GlyphPath = ""
Me.tlbDatiContabiliSintetici.Id = 23
Me.tlbDatiContabiliSintetici.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
Me.tlbDatiContabiliSintetici.Name = "tlbDatiContabiliSintetici"
Me.tlbDatiContabiliSintetici.Visible = true
'
'tlbModifNumDoc
'
Me.tlbModifNumDoc.Caption = "Modifica tipo/numero documento"
Me.tlbModifNumDoc.GlyphPath = ""
Me.tlbModifNumDoc.Id = 33
Me.tlbModifNumDoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
Me.tlbModifNumDoc.Name = "tlbModifNumDoc"
Me.tlbModifNumDoc.NTSIsCheckBox = false
Me.tlbModifNumDoc.Visible = true
'
'tlbRettificaTotdoc
'
Me.tlbRettificaTotdoc.Caption = "Rettifica totale documento"
Me.tlbRettificaTotdoc.GlyphPath = ""
Me.tlbRettificaTotdoc.Id = 76
Me.tlbRettificaTotdoc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.J))
Me.tlbRettificaTotdoc.Name = "tlbRettificaTotdoc"
Me.tlbRettificaTotdoc.NTSIsCheckBox = false
Me.tlbRettificaTotdoc.Visible = true
'
'tlbRicalcPrzScnPro
'
Me.tlbRicalcPrzScnPro.Caption = "Ricalcola prezzi/sconti/provvigioni"
Me.tlbRicalcPrzScnPro.GlyphPath = ""
Me.tlbRicalcPrzScnPro.Id = 124
Me.tlbRicalcPrzScnPro.Name = "tlbRicalcPrzScnPro"
Me.tlbRicalcPrzScnPro.Visible = true
'
'tlbRicFiscDaRiem
'
Me.tlbRicFiscDaRiem.Caption = "Selez. ricevute fisc. da riemettere"
Me.tlbRicFiscDaRiem.GlyphPath = ""
Me.tlbRicFiscDaRiem.Id = 81
Me.tlbRicFiscDaRiem.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y))
Me.tlbRicFiscDaRiem.Name = "tlbRicFiscDaRiem"
Me.tlbRicFiscDaRiem.NTSIsCheckBox = false
Me.tlbRicFiscDaRiem.Visible = true
'
'tlbAggCorrFF
'
Me.tlbAggCorrFF.Caption = "Aggiungi Corrispettivo a Fattura Figurativa"
Me.tlbAggCorrFF.GlyphPath = ""
Me.tlbAggCorrFF.Id = 125
Me.tlbAggCorrFF.Name = "tlbAggCorrFF"
Me.tlbAggCorrFF.NTSIsCheckBox = false
Me.tlbAggCorrFF.Visible = true
'
'tlbNavigazDoc
'
Me.tlbNavigazDoc.Caption = "Navigazione documenti"
Me.tlbNavigazDoc.GlyphPath = ""
Me.tlbNavigazDoc.Id = 66
Me.tlbNavigazDoc.Name = "tlbNavigazDoc"
Me.tlbNavigazDoc.NTSIsCheckBox = false
Me.tlbNavigazDoc.Visible = true
'
'tlbSchedatrasporto
'
Me.tlbSchedatrasporto.Caption = "Scheda di trasporto"
Me.tlbSchedatrasporto.GlyphPath = ""
Me.tlbSchedatrasporto.Id = 97
Me.tlbSchedatrasporto.Name = "tlbSchedatrasporto"
Me.tlbSchedatrasporto.NTSIsCheckBox = false
Me.tlbSchedatrasporto.Visible = true
'
'tlbEmail
'
Me.tlbEmail.Caption = "Visualizza e-mail scambiate"
Me.tlbEmail.GlyphPath = ""
Me.tlbEmail.Id = 103
Me.tlbEmail.Name = "tlbEmail"
Me.tlbEmail.Visible = true
'
'tlbMnuRecord
'
Me.tlbMnuRecord.Caption = "&Menu Riga"
Me.tlbMnuRecord.GlyphPath = ""
Me.tlbMnuRecord.Id = 8
Me.tlbMnuRecord.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecMov), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbScarCollegati, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLavCollegate), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoteRiga, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaRiga, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInsRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDispVariante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaColliQta, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalvaCondPart), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancRigheSel)})
Me.tlbMnuRecord.Name = "tlbMnuRecord"
Me.tlbMnuRecord.Visible = true
'
'tlbPrecMov
'
Me.tlbPrecMov.Caption = "Mostra precedenti movimenti"
Me.tlbPrecMov.GlyphPath = ""
Me.tlbPrecMov.Id = 57
Me.tlbPrecMov.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5))
Me.tlbPrecMov.Name = "tlbPrecMov"
Me.tlbPrecMov.NTSIsCheckBox = false
Me.tlbPrecMov.Visible = true
'
'tlbScarCollegati
'
Me.tlbScarCollegati.Caption = "Scarichi collegati"
Me.tlbScarCollegati.GlyphPath = ""
Me.tlbScarCollegati.Id = 53
Me.tlbScarCollegati.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
Me.tlbScarCollegati.Name = "tlbScarCollegati"
Me.tlbScarCollegati.NTSIsCheckBox = false
Me.tlbScarCollegati.Visible = true
'
'tlbLavCollegate
'
Me.tlbLavCollegate.Caption = "Lavorazioni collegate"
Me.tlbLavCollegate.GlyphPath = ""
Me.tlbLavCollegate.Id = 54
Me.tlbLavCollegate.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F9))
Me.tlbLavCollegate.Name = "tlbLavCollegate"
Me.tlbLavCollegate.NTSIsCheckBox = false
Me.tlbLavCollegate.Visible = true
'
'tlbSeleziona
'
Me.tlbSeleziona.Caption = "SELEZIONA"
Me.tlbSeleziona.GlyphPath = ""
Me.tlbSeleziona.Id = 47
Me.tlbSeleziona.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRigheOrdBarcode), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezAcconti, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbScaricaMagaz), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIntegraRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezDaTerm, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaLista), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImport), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbExport), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelectDeselect), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGiub, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSiub)})
Me.tlbSeleziona.Name = "tlbSeleziona"
Me.tlbSeleziona.Visible = true
'
'tlbRigheOrdBarcode
'
Me.tlbRigheOrdBarcode.Caption = "Righe d'ordine con barcode"
Me.tlbRigheOrdBarcode.GlyphPath = ""
Me.tlbRigheOrdBarcode.Id = 85
Me.tlbRigheOrdBarcode.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Insert))
Me.tlbRigheOrdBarcode.Name = "tlbRigheOrdBarcode"
Me.tlbRigheOrdBarcode.NTSIsCheckBox = false
Me.tlbRigheOrdBarcode.Visible = true
'
'tlbSelezAcconti
'
Me.tlbSelezAcconti.Caption = "Precedenti acconti"
Me.tlbSelezAcconti.GlyphPath = ""
Me.tlbSelezAcconti.Id = 73
Me.tlbSelezAcconti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W))
Me.tlbSelezAcconti.Name = "tlbSelezAcconti"
Me.tlbSelezAcconti.NTSIsCheckBox = false
Me.tlbSelezAcconti.Visible = true
'
'tlbScaricaMagaz
'
Me.tlbScaricaMagaz.Caption = "Scarica giacenze di magazzino"
Me.tlbScaricaMagaz.GlyphPath = ""
Me.tlbScaricaMagaz.Id = 83
Me.tlbScaricaMagaz.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
Me.tlbScaricaMagaz.Name = "tlbScaricaMagaz"
Me.tlbScaricaMagaz.NTSIsCheckBox = false
Me.tlbScaricaMagaz.Visible = true
'
'tlbIntegraRighe
'
Me.tlbIntegraRighe.Caption = "Integra righe"
Me.tlbIntegraRighe.GlyphPath = ""
Me.tlbIntegraRighe.Id = 84
Me.tlbIntegraRighe.Name = "tlbIntegraRighe"
Me.tlbIntegraRighe.NTSIsCheckBox = false
Me.tlbIntegraRighe.Visible = true
'
'tlbSelezDaTerm
'
Me.tlbSelezDaTerm.Caption = "Da terminale"
Me.tlbSelezDaTerm.GlyphPath = ""
Me.tlbSelezDaTerm.Id = 50
Me.tlbSelezDaTerm.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K))
Me.tlbSelezDaTerm.Name = "tlbSelezDaTerm"
Me.tlbSelezDaTerm.NTSIsCheckBox = false
Me.tlbSelezDaTerm.Visible = true
'
'tlbDaLista
'
Me.tlbDaLista.Caption = "Da lista selezionata"
Me.tlbDaLista.GlyphPath = ""
Me.tlbDaLista.Id = 110
Me.tlbDaLista.Name = "tlbDaLista"
Me.tlbDaLista.Visible = true
'
'tlbImport
'
Me.tlbImport.Caption = "Importa righe"
Me.tlbImport.GlyphPath = ""
Me.tlbImport.Id = 108
Me.tlbImport.Name = "tlbImport"
Me.tlbImport.Visible = true
'
'tlbExport
'
Me.tlbExport.Caption = "Esporta righe selezionate"
Me.tlbExport.GlyphPath = ""
Me.tlbExport.Id = 107
Me.tlbExport.Name = "tlbExport"
Me.tlbExport.Visible = true
'
'tlbSelectDeselect
'
Me.tlbSelectDeselect.Caption = "Seleziona\deseleziona le righe da esportare"
Me.tlbSelectDeselect.GlyphPath = ""
Me.tlbSelectDeselect.Id = 109
Me.tlbSelectDeselect.Name = "tlbSelectDeselect"
Me.tlbSelectDeselect.Visible = true
'
'tlbGiub
'
Me.tlbGiub.Caption = "Giacenza ubicazioni"
Me.tlbGiub.GlyphPath = ""
Me.tlbGiub.Id = 95
Me.tlbGiub.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F5))
Me.tlbGiub.Name = "tlbGiub"
Me.tlbGiub.Visible = true
'
'tlbSiub
'
Me.tlbSiub.Caption = "Situazione ubicazioni"
Me.tlbSiub.GlyphPath = ""
Me.tlbSiub.Id = 96
Me.tlbSiub.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F6))
Me.tlbSiub.Name = "tlbSiub"
Me.tlbSiub.Visible = true
'
'tlbNoteRiga
'
Me.tlbNoteRiga.Caption = "&Note Riga"
Me.tlbNoteRiga.GlyphPath = ""
Me.tlbNoteRiga.Id = 25
Me.tlbNoteRiga.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z))
Me.tlbNoteRiga.Name = "tlbNoteRiga"
Me.tlbNoteRiga.NTSIsCheckBox = false
Me.tlbNoteRiga.Visible = true
'
'tlbCopiaRiga
'
Me.tlbCopiaRiga.Caption = "Copia riga"
Me.tlbCopiaRiga.GlyphPath = ""
Me.tlbCopiaRiga.Id = 45
Me.tlbCopiaRiga.Name = "tlbCopiaRiga"
Me.tlbCopiaRiga.NTSIsCheckBox = false
Me.tlbCopiaRiga.Visible = true
'
'tlbIncollaRiga
'
Me.tlbIncollaRiga.Caption = "Incolla riga"
Me.tlbIncollaRiga.GlyphPath = ""
Me.tlbIncollaRiga.Id = 46
Me.tlbIncollaRiga.Name = "tlbIncollaRiga"
Me.tlbIncollaRiga.NTSIsCheckBox = false
Me.tlbIncollaRiga.Visible = true
'
'tlbInsRiga
'
Me.tlbInsRiga.Caption = "Inserisci riga"
Me.tlbInsRiga.GlyphPath = ""
Me.tlbInsRiga.Id = 65
Me.tlbInsRiga.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F12))
Me.tlbInsRiga.Name = "tlbInsRiga"
Me.tlbInsRiga.NTSIsCheckBox = false
Me.tlbInsRiga.Visible = true
'
'tlbVisDispVariante
'
Me.tlbVisDispVariante.GlyphPath = ""
Me.tlbVisDispVariante.Id = 119
Me.tlbVisDispVariante.Name = "tlbVisDispVariante"
Me.tlbVisDispVariante.NTSIsCheckBox = false
Me.tlbVisDispVariante.Visible = true
'
'tlbRicalcolaColliQta
'
Me.tlbRicalcolaColliQta.Caption = "Ricalcola colli da quantità"
Me.tlbRicalcolaColliQta.GlyphPath = ""
Me.tlbRicalcolaColliQta.Id = 42
Me.tlbRicalcolaColliQta.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
Me.tlbRicalcolaColliQta.Name = "tlbRicalcolaColliQta"
Me.tlbRicalcolaColliQta.NTSIsCheckBox = false
Me.tlbRicalcolaColliQta.Visible = true
'
'tlbSalvaCondPart
'
Me.tlbSalvaCondPart.Caption = "Salva condizioni particolari"
Me.tlbSalvaCondPart.GlyphPath = ""
Me.tlbSalvaCondPart.Id = 61
Me.tlbSalvaCondPart.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F6))
Me.tlbSalvaCondPart.Name = "tlbSalvaCondPart"
Me.tlbSalvaCondPart.NTSIsCheckBox = false
Me.tlbSalvaCondPart.Visible = true
'
'tlbCancRigheSel
'
Me.tlbCancRigheSel.Caption = "Cancella righe selezionate"
Me.tlbCancRigheSel.GlyphPath = ""
Me.tlbCancRigheSel.Id = 123
Me.tlbCancRigheSel.Name = "tlbCancRigheSel"
Me.tlbCancRigheSel.NTSIsCheckBox = false
Me.tlbCancRigheSel.Visible = true
'
'tlbStrumenti
'
Me.tlbStrumenti.Caption = "Strumenti"
Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"),System.Drawing.Image)
Me.tlbStrumenti.GlyphPath = ""
Me.tlbStrumenti.Id = 15
Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRegCassa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichetteRiga, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichetteFinale), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbConfRegCassa, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaFileTerm), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalvanuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiapriDopoSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRighe, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNumerazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTabBolli), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
Me.tlbStrumenti.Name = "tlbStrumenti"
Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
Me.tlbStrumenti.Visible = true
'
'tlbRegCassa
'
Me.tlbRegCassa.Caption = "Registratore di cassa"
Me.tlbRegCassa.GlyphPath = ""
Me.tlbRegCassa.Id = 74
Me.tlbRegCassa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F6))
Me.tlbRegCassa.Name = "tlbRegCassa"
Me.tlbRegCassa.NTSIsCheckBox = false
Me.tlbRegCassa.Visible = true
'
'tlbStampaEtichetteRiga
'
Me.tlbStampaEtichetteRiga.Caption = "Stampa etichette per riga"
Me.tlbStampaEtichetteRiga.GlyphPath = ""
Me.tlbStampaEtichetteRiga.Id = 18
Me.tlbStampaEtichetteRiga.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
Me.tlbStampaEtichetteRiga.Name = "tlbStampaEtichetteRiga"
Me.tlbStampaEtichetteRiga.Visible = true
'
'tlbStampaEtichetteFinale
'
Me.tlbStampaEtichetteFinale.Caption = "Stampa etichette finale"
Me.tlbStampaEtichetteFinale.GlyphPath = ""
Me.tlbStampaEtichetteFinale.Id = 21
Me.tlbStampaEtichetteFinale.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
Me.tlbStampaEtichetteFinale.Name = "tlbStampaEtichetteFinale"
Me.tlbStampaEtichetteFinale.NTSIsCheckBox = true
Me.tlbStampaEtichetteFinale.Visible = true
'
'tlbConfRegCassa
'
Me.tlbConfRegCassa.Caption = "Configura Registratore di Cassa"
Me.tlbConfRegCassa.GlyphPath = ""
Me.tlbConfRegCassa.Id = 122
Me.tlbConfRegCassa.Name = "tlbConfRegCassa"
Me.tlbConfRegCassa.NTSIsCheckBox = false
Me.tlbConfRegCassa.Visible = true
'
'tlbImpostaFileTerm
'
Me.tlbImpostaFileTerm.Caption = "Imposta file terminale"
Me.tlbImpostaFileTerm.GlyphPath = ""
Me.tlbImpostaFileTerm.Id = 34
Me.tlbImpostaFileTerm.Name = "tlbImpostaFileTerm"
Me.tlbImpostaFileTerm.NTSIsCheckBox = false
Me.tlbImpostaFileTerm.Visible = true
'
'tlbSalvanuovo
'
Me.tlbSalvanuovo.Caption = "Dopo salva -> nuovo"
Me.tlbSalvanuovo.GlyphPath = ""
Me.tlbSalvanuovo.Id = 105
Me.tlbSalvanuovo.Name = "tlbSalvanuovo"
Me.tlbSalvanuovo.NTSIsCheckBox = true
Me.tlbSalvanuovo.Visible = true
'
'tlbRiapriDopoSalva
'
Me.tlbRiapriDopoSalva.Caption = "Riapri doc. dopo salva"
Me.tlbRiapriDopoSalva.GlyphPath = ""
Me.tlbRiapriDopoSalva.Id = 106
Me.tlbRiapriDopoSalva.Name = "tlbRiapriDopoSalva"
Me.tlbRiapriDopoSalva.NTSIsCheckBox = true
Me.tlbRiapriDopoSalva.Visible = true
'
'tlbOrdinaRighe
'
Me.tlbOrdinaRighe.Caption = "Ordina righe per ..."
Me.tlbOrdinaRighe.GlyphPath = ""
Me.tlbOrdinaRighe.Id = 98
Me.tlbOrdinaRighe.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheRiga, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheCodiceArticolo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheDescrizione), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiordinarighe, true)})
Me.tlbOrdinaRighe.Name = "tlbOrdinaRighe"
Me.tlbOrdinaRighe.Visible = true
'
'tlbOrdinaRigheRiga
'
Me.tlbOrdinaRigheRiga.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheRiga.Caption = "Riga"
Me.tlbOrdinaRigheRiga.Down = true
Me.tlbOrdinaRigheRiga.GlyphPath = ""
Me.tlbOrdinaRigheRiga.Id = 99
Me.tlbOrdinaRigheRiga.Name = "tlbOrdinaRigheRiga"
Me.tlbOrdinaRigheRiga.Visible = true
'
'tlbOrdinaRigheCodiceArticolo
'
Me.tlbOrdinaRigheCodiceArticolo.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheCodiceArticolo.Caption = "Codice articolo"
Me.tlbOrdinaRigheCodiceArticolo.GlyphPath = ""
Me.tlbOrdinaRigheCodiceArticolo.Id = 100
Me.tlbOrdinaRigheCodiceArticolo.Name = "tlbOrdinaRigheCodiceArticolo"
Me.tlbOrdinaRigheCodiceArticolo.Visible = true
'
'tlbOrdinaRigheDescrizione
'
Me.tlbOrdinaRigheDescrizione.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheDescrizione.Caption = "Descrizione"
Me.tlbOrdinaRigheDescrizione.GlyphPath = ""
Me.tlbOrdinaRigheDescrizione.Id = 101
Me.tlbOrdinaRigheDescrizione.Name = "tlbOrdinaRigheDescrizione"
Me.tlbOrdinaRigheDescrizione.Visible = true
'
'tlbRiordinarighe
'
Me.tlbRiordinarighe.Caption = "Riordina righe"
Me.tlbRiordinarighe.GlyphPath = ""
Me.tlbRiordinarighe.Id = 111
Me.tlbRiordinarighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F12))
Me.tlbRiordinarighe.Name = "tlbRiordinarighe"
Me.tlbRiordinarighe.NTSIsCheckBox = false
Me.tlbRiordinarighe.Visible = true
'
'tlbNumerazioni
'
Me.tlbNumerazioni.Caption = "Numerazioni"
Me.tlbNumerazioni.GlyphPath = ""
Me.tlbNumerazioni.Id = 40
Me.tlbNumerazioni.Name = "tlbNumerazioni"
Me.tlbNumerazioni.NTSIsCheckBox = false
Me.tlbNumerazioni.Visible = true
'
'tlbTabBolli
'
Me.tlbTabBolli.Caption = "Tabella bolli"
Me.tlbTabBolli.GlyphPath = ""
Me.tlbTabBolli.Id = 41
Me.tlbTabBolli.Name = "tlbTabBolli"
Me.tlbTabBolli.NTSIsCheckBox = false
Me.tlbTabBolli.Visible = true
'
'tlbStampaPdf
'
Me.tlbStampaPdf.Caption = "StampaPdf"
Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"),System.Drawing.Image)
Me.tlbStampaPdf.GlyphPath = ""
Me.tlbStampaPdf.Id = 12
Me.tlbStampaPdf.Name = "tlbStampaPdf"
Me.tlbStampaPdf.Visible = true
'
'tlbImpostaStampante
'
Me.tlbImpostaStampante.Caption = "Imposta Stampante"
Me.tlbImpostaStampante.GlyphPath = ""
Me.tlbImpostaStampante.Id = 16
Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
Me.tlbImpostaStampante.Visible = true
'
'tlbStampa
'
Me.tlbStampa.Caption = "Stampa"
Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"),System.Drawing.Image)
Me.tlbStampa.GlyphPath = ""
Me.tlbStampa.Id = 9
Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
Me.tlbStampa.Name = "tlbStampa"
Me.tlbStampa.Visible = true
'
'tlbStampaVideo
'
Me.tlbStampaVideo.Caption = "StampaVideo"
Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"),System.Drawing.Image)
Me.tlbStampaVideo.GlyphPath = ""
Me.tlbStampaVideo.Id = 10
Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
Me.tlbStampaVideo.Name = "tlbStampaVideo"
Me.tlbStampaVideo.Visible = true
'
'tlbGuida
'
Me.tlbGuida.Caption = "Guida"
Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"),System.Drawing.Image)
Me.tlbGuida.GlyphPath = ""
Me.tlbGuida.Id = 13
Me.tlbGuida.Name = "tlbGuida"
Me.tlbGuida.Visible = true
'
'tlbEsci
'
Me.tlbEsci.Caption = "Esci"
Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"),System.Drawing.Image)
Me.tlbEsci.GlyphPath = ""
Me.tlbEsci.Id = 14
Me.tlbEsci.Name = "tlbEsci"
Me.tlbEsci.Visible = true
'
'tlbApriDB
'
Me.tlbApriDB.Caption = "Apri distinta base collegata"
Me.tlbApriDB.GlyphPath = ""
Me.tlbApriDB.Id = 55
Me.tlbApriDB.Name = "tlbApriDB"
Me.tlbApriDB.NTSIsCheckBox = false
Me.tlbApriDB.Visible = true
'
'tlbApriOrdineCollegato
'
Me.tlbApriOrdineCollegato.Caption = "Ordine collegato"
Me.tlbApriOrdineCollegato.GlyphPath = ""
Me.tlbApriOrdineCollegato.Id = 87
Me.tlbApriOrdineCollegato.Name = "tlbApriOrdineCollegato"
Me.tlbApriOrdineCollegato.NTSIsCheckBox = false
Me.tlbApriOrdineCollegato.Visible = true
'
'tlbApriNotaCollegata
'
Me.tlbApriNotaCollegata.Caption = "Nota di prelievo collegata"
Me.tlbApriNotaCollegata.GlyphPath = ""
Me.tlbApriNotaCollegata.Id = 88
Me.tlbApriNotaCollegata.Name = "tlbApriNotaCollegata"
Me.tlbApriNotaCollegata.NTSIsCheckBox = false
Me.tlbApriNotaCollegata.Visible = true
'
'tlbApriBollaCtoLavcollegata
'
Me.tlbApriBollaCtoLavcollegata.Caption = "Bolle c/to lavoro collegato"
Me.tlbApriBollaCtoLavcollegata.GlyphPath = ""
Me.tlbApriBollaCtoLavcollegata.Id = 89
Me.tlbApriBollaCtoLavcollegata.Name = "tlbApriBollaCtoLavcollegata"
Me.tlbApriBollaCtoLavcollegata.NTSIsCheckBox = false
Me.tlbApriBollaCtoLavcollegata.Visible = true
'
'tlbApriCarProdCollegato
'
Me.tlbApriCarProdCollegato.Caption = "Carico prod. collegato (c.to lav. NO FASI)"
Me.tlbApriCarProdCollegato.GlyphPath = ""
Me.tlbApriCarProdCollegato.Id = 90
Me.tlbApriCarProdCollegato.Name = "tlbApriCarProdCollegato"
Me.tlbApriCarProdCollegato.NTSIsCheckBox = false
Me.tlbApriCarProdCollegato.Visible = true
'
'tlbApriConfiguratore
'
Me.tlbApriConfiguratore.Caption = "Apri configuratore"
Me.tlbApriConfiguratore.GlyphPath = ""
Me.tlbApriConfiguratore.Id = 94
Me.tlbApriConfiguratore.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
Me.tlbApriConfiguratore.Name = "tlbApriConfiguratore"
Me.tlbApriConfiguratore.NTSIsCheckBox = false
Me.tlbApriConfiguratore.Visible = true
'
'lbBarcodeIn
'
Me.lbBarcodeIn.AutoSize = true
Me.lbBarcodeIn.BackColor = System.Drawing.Color.Transparent
Me.lbBarcodeIn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbBarcodeIn.Location = New System.Drawing.Point(10, 4)
Me.lbBarcodeIn.Name = "lbBarcodeIn"
Me.lbBarcodeIn.NTSDbField = ""
Me.lbBarcodeIn.Size = New System.Drawing.Size(59, 13)
Me.lbBarcodeIn.TabIndex = 57
Me.lbBarcodeIn.Text = "BARCODE"
Me.lbBarcodeIn.Tooltip = ""
Me.lbBarcodeIn.UseMnemonic = false
Me.lbBarcodeIn.Visible = false
'
'edBarcodeIn
'
Me.edBarcodeIn.Cursor = System.Windows.Forms.Cursors.Default
Me.edBarcodeIn.EditValue = " "
Me.edBarcodeIn.Location = New System.Drawing.Point(94, 1)
Me.edBarcodeIn.Name = "edBarcodeIn"
Me.edBarcodeIn.NTSDbField = ""
Me.edBarcodeIn.NTSForzaVisZoom = false
Me.edBarcodeIn.NTSOldValue = " "
Me.edBarcodeIn.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edBarcodeIn.Properties.Appearance.Options.UseBackColor = true
Me.edBarcodeIn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edBarcodeIn.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edBarcodeIn.Properties.AutoHeight = false
Me.edBarcodeIn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edBarcodeIn.Properties.MaxLength = 65536
Me.edBarcodeIn.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edBarcodeIn.Size = New System.Drawing.Size(164, 20)
Me.edBarcodeIn.TabIndex = 56
Me.edBarcodeIn.Visible = false
'
'FRMVEBOLL_F
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.ClientSize = New System.Drawing.Size(732, 741)
Me.Controls.Add(Me.tsBoll)
Me.Controls.Add(Me.pnTestataTop)
Me.Controls.Add(Me.barDockControlLeft)
Me.Controls.Add(Me.barDockControlRight)
Me.Controls.Add(Me.barDockControlBottom)
Me.Controls.Add(Me.barDockControlTop)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Name = "FRMVEBOLL_F"
Me.NTSLastControlFocussed = Me.grRighe
Me.Text = "GESTIONE DOCUMENTI DI MAGAZZINO"
CType(Me.dttSmartArt,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnTestataTop,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataTop.ResumeLayout(false)
Me.pnTestataTop.PerformLayout
CType(Me.cbTipoDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edAnnoDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edNumDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edSerieDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edet_datdoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_coddest.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_conto.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage5.ResumeLayout(false)
CType(Me.pnNote,System.ComponentModel.ISupportInitialize).EndInit
Me.pnNote.ResumeLayout(false)
CType(Me.edEt_note.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage3.ResumeLayout(false)
CType(Me.pnPage3,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPage3.ResumeLayout(false)
CType(Me.pnPiedeTrasporto,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeTrasporto.ResumeLayout(false)
Me.pnPiedeTrasporto.PerformLayout
CType(Me.cbEt_intra.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codntra.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmProvvigioni,System.ComponentModel.ISupportInitialize).EndInit
Me.fmProvvigioni.ResumeLayout(false)
Me.fmProvvigioni.PerformLayout
CType(Me.ckEt_flprov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totprov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_impprov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_vettor.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmPartita,System.ComponentModel.ISupportInitialize).EndInit
Me.fmPartita.ResumeLayout(false)
Me.fmPartita.PerformLayout
CType(Me.edEt_datpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_numpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_alfpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_annpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmInizioTrasporto,System.ComponentModel.ISupportInitialize).EndInit
Me.fmInizioTrasporto.ResumeLayout(false)
Me.fmInizioTrasporto.PerformLayout
CType(Me.edEt_oriniz.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_dtiniz.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codcfam.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_caustra.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_aspetto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codaspe.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_coddest2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_porto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_vettor2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.cbEt_acuradi.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPiedeTotali,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeTotali.ResumeLayout(false)
Me.pnPiedeTotali.PerformLayout
CType(Me.ckEt_flscdb.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_resto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speacc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pagato2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speimb.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotalev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotale.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speinc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edResiduo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pesonetto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_bolli.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edResiduov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_peso.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotIvav.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abbuono.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totcoll.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totmerce.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pagato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotImponibv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abbuonov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotSpesev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totomag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotSpese.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totmercev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totdoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pagatov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotImponib.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_bolliv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totomagv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speaccv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totdocv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speincv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotIva.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speimbv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPiedeSx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeSx.ResumeLayout(false)
Me.TabPage2.ResumeLayout(false)
CType(Me.pnGriglia,System.ComponentModel.ISupportInitialize).EndInit
Me.pnGriglia.ResumeLayout(false)
CType(Me.grRighe,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.grvRighe,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnCorpo,System.ComponentModel.ISupportInitialize).EndInit
Me.pnCorpo.ResumeLayout(false)
Me.pnCorpo.PerformLayout
CType(Me.edOrdinato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edImpegnato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edSconto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edUltCost.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edPreList.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edDispNetta.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edDispon.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage1.ResumeLayout(false)
CType(Me.pnTestataDx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataDx.ResumeLayout(false)
CType(Me.pnDatiTesta2,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta2.ResumeLayout(false)
Me.pnDatiTesta2.PerformLayout
CType(Me.fmPa,System.ComponentModel.ISupportInitialize).EndInit
Me.fmPa.ResumeLayout(false)
Me.fmPa.PerformLayout
CType(Me.edEt_riferimpa.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cig.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cup.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_valuta.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmBanca,System.ComponentModel.ISupportInitialize).EndInit
Me.fmBanca.ResumeLayout(false)
Me.fmBanca.PerformLayout
CType(Me.edEt_codbanc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_banc2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_banc1.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cab.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abi.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmOpzioni,System.ComponentModel.ISupportInitialize).EndInit
Me.fmOpzioni.ResumeLayout(false)
Me.fmOpzioni.PerformLayout
CType(Me.ckXx_przbol.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_scorpo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flspinc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flboll.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_proforma.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codese.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_causale.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cambio.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_controp.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_magaz2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnDatiTesta1,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta1.ResumeLayout(false)
Me.pnDatiTesta1.PerformLayout
CType(Me.edEt_listino.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codpaga.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scopag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_datapag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_tipobf.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scont2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_riferim.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scont1.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_magaz.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codagen.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnTestataSx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataSx.ResumeLayout(false)
CType(Me.tsBoll,System.ComponentModel.ISupportInitialize).EndInit
Me.tsBoll.ResumeLayout(false)
CType(Me.NtsBarManager1,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edBarcodeIn.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.ResumeLayout(false)

End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttStasino As New DataTable()
    Dim dttKit As New DataTable()
    Dim dttOrtipo As New DataTable()
    Dim dttTask As New DataTable()
    Dim dttNptipo As New DataTable()

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
        tlbApriDocPrec.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbApriDocSucc.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbSelRigheOrdini.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbSelOrdini.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
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

      '-------------------------------------------------
      grvRighe.NTSMenuContext = tlbMnuRecord

      cbEt_acuradi.NTSSetParam(oApp.Tr(Me, 129055173884392804, "Tipo trasporto"))
      cbTipoDoc.NTSSetParam(oApp.Tr(Me, 129055174083067898, "Tipo documento"))

      edEt_cambio.NTSSetParam(oMenu, oApp.Tr(Me, 129055174113392814, "Cambio"), "#,##0.000000000", 20, 0, 99999999)
      edEt_valuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174136996228, "Codice valuta"), tabvalu)
      edEt_scont2.NTSSetParam(oMenu, oApp.Tr(Me, 129055174163413294, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      edEt_scont1.NTSSetParam(oMenu, oApp.Tr(Me, 129055174185604572, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      edEt_scopag.NTSSetParam(oMenu, oApp.Tr(Me, 129055174234047962, "Sconto pagamento"), oApp.FormatSconti, 6, -100, 100)
      edEt_codpaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174259207271, "Pagamento"), tabpaga)
      edEt_listino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174284835387, "Numero listino"), tablist)
      edEt_codcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174454543521, "Famiglia"), tabcfam, False)
      edEt_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174518301273, "Codice Ns. Banca"), tabbanc)
      edEt_banc2.NTSSetParam(oMenu, oApp.Tr(Me, 129055174553149260, "Descrizione Filiale"), 50)
      edEt_banc1.NTSSetParam(oMenu, oApp.Tr(Me, 129055174574245575, "Descrizione Banca"), 50)
      edEt_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174602217726, "Codice CAB"), tababicab)
      edEt_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174646910660, "Codice ABI"), tababi)
      edEt_vettor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174676289232, "Codice vettore/spedizioniere"), tabvett)
      edEt_magaz2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174762862258, "Magazzino 2"), tabmaga)
      ckEt_proforma.NTSSetParam(oMenu, oApp.Tr(Me, 129055174833964653, "Fattura proforma"), "S", "N")
      ckEt_flboll.NTSSetParam(oMenu, oApp.Tr(Me, 129055174860374114, "Flag Addebito spese per bolli"), "S", "N")
      ckEt_flspinc.NTSSetParam(oMenu, oApp.Tr(Me, 129055174884283271, "Flag Addebito spese di incasso"), "S", "N")
      ckEt_scorpo.NTSSetParam(oMenu, oApp.Tr(Me, 129055174913505574, "Flag Scorporo"), "S", "N")
      edEt_riferim.NTSSetParam(oMenu, oApp.Tr(Me, 129055174941790263, "Riferimenti"), 50, True)
      edEt_codese.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174971637642, "Esenzione IVA"), tabciva)
      edEt_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055174999609793, "Magazzino"), tabmaga)
      edEt_codagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175034457780, "Agente"), tabcage)
      edEt_tipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175078056831, "Tipo bolla/fattura"), tabtpbf)
      edSconto.NTSSetParam(oMenu, oApp.Tr(Me, 129055175099309415, "Sconto effettivo"), oApp.FormatSconti, 10, -999999999, 9999999999)
      edUltCost.NTSSetParam(oMenu, oApp.Tr(Me, 129055175139783086, "Ultimo costo"), oApp.FormatPrzUn)
      edPreList.NTSSetParam(oMenu, oApp.Tr(Me, 129055175164473588, "Prezzo di listino"), oApp.FormatPrzUn)
      edBarcodeIn.NTSSetParam(oMenu, oApp.Tr(Me, 129055175164473589, "Barcode"), 0, True)
      FormatQtaEsistCorpo()
      edEt_totprov.NTSSetParam(oMenu, oApp.Tr(Me, 129055175407784421, "Totale provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_impprov.NTSSetParam(oMenu, oApp.Tr(Me, 129055175430287157, "Imponibile provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_datpar.NTSSetParam(oMenu, oApp.Tr(Me, 129055175458728115, "Data partita"), True)
      edEt_numpar.NTSSetParam(oMenu, oApp.Tr(Me, 129055175480136968, "Numero partita"), "0", 9, 0, 999999999)
      edEt_alfpar.NTSSetParam(oMenu, oApp.Tr(Me, 129055175530924393, "Serie partita"), CLN__STD.SerieMaxLen, False)
      edEt_annpar.NTSSetParam(oMenu, oApp.Tr(Me, 129055175553895936, "Anno partita"), "0", 4, 0, 2099)
      edEt_pesonetto.NTSSetParam(oMenu, oApp.Tr(Me, 129055175581399280, "Peso netto"), oApp.FormatQta)
      edEt_peso.NTSSetParam(oMenu, oApp.Tr(Me, 129055175607339934, "Peso lordo"), oApp.FormatQta)
      edEt_totcoll.NTSSetParam(oMenu, oApp.Tr(Me, 129055175641562845, "Totale colli"), "#,##0")
      edEt_porto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175666722154, "Porto"), tabport, True)
      edEt_vettor2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175693131615, "Vettore 2"), tabvett)
      edEt_coddest2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175719541076, "Destinazione diversa 2"), tabdestdiv)
      edEt_coddest2.CliePerDestdiv = edEt_conto
      edEt_codaspe.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175757514443, "Codice aspetto esteriore dei beni"), tabaspe)
      edEt_aspetto.NTSSetParam(oMenu, oApp.Tr(Me, 129055175785174056, "Aspetto esteriore dei beni"), 30)
      edEt_caustra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055175809239482, "Causale del trasporto"), tabcaum)
      edTotalev.NTSSetParam(oMenu, oApp.Tr(Me, 129055175834086253, "Totale in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotale.NTSSetParam(oMenu, oApp.Tr(Me, 129055175856901527, "Totale"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edResiduov.NTSSetParam(oMenu, oApp.Tr(Me, 129055175878779187, "Residuo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edResiduo.NTSSetParam(oMenu, oApp.Tr(Me, 129055175902063268, "Residuo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_abbuonov.NTSSetParam(oMenu, oApp.Tr(Me, 129055175924566004, "Abbuono in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_abbuono.NTSSetParam(oMenu, oApp.Tr(Me, 129055175950975465, "Abbuono"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_pagatov.NTSSetParam(oMenu, oApp.Tr(Me, 129055175978166271, "Incassato in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_pagato.NTSSetParam(oMenu, oApp.Tr(Me, 129055176014576948, "Incassato"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totomagv.NTSSetParam(oMenu, oApp.Tr(Me, 128230023241283521, "Omaggi in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_pagato2.NTSSetParam(oMenu, oApp.Tr(Me, 129588447722373047, "Incassato contanti"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_resto.NTSSetParam(oMenu, oApp.Tr(Me, 129588447852929688, "Resto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totomag.NTSSetParam(oMenu, oApp.Tr(Me, 129055176038642374, "Omaggi"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totdocv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176058332268, "Totale documento in valuta"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176079741121, "Totale documento"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotIvav.NTSSetParam(oMenu, oApp.Tr(Me, 129055176101149974, "Totale IVA in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotIva.NTSSetParam(oMenu, oApp.Tr(Me, 129055176120527330, "Totale IVA"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotImponibv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176140373493, "Totale imponibile in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotImponib.NTSSetParam(oMenu, oApp.Tr(Me, 129055176163188767, "Totale imponibile"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotSpesev.NTSSetParam(oMenu, oApp.Tr(Me, 129055176197724216, "Totale spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotSpese.NTSSetParam(oMenu, oApp.Tr(Me, 129055176232415934, "Totale spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totmercev.NTSSetParam(oMenu, oApp.Tr(Me, 129055176268514073, "Totale netto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_totmerce.NTSSetParam(oMenu, oApp.Tr(Me, 129055176296955031, "Totale netto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_bolliv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176328052562, "Bolli in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_bolli.NTSSetParam(oMenu, oApp.Tr(Me, 129055176360712783, "Bolli"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speincv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176385559554, "Spese di incasso in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speinc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176405718255, "Spese di incasso"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speimbv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176424470535, "Spese di imballo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speimb.NTSSetParam(oMenu, oApp.Tr(Me, 129055176443066546, "Spese di imballo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speaccv.NTSSetParam(oMenu, oApp.Tr(Me, 129055176464475399, "Spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speacc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176483227679, "Spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_note.NTSSetParam(oMenu, oApp.Tr(Me, 129055176523545081, "Note"), 0)
      edEt_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055176549798273, "Destinazione diversa"), tabdestdiv)
      edEt_coddest.CliePerDestdiv = edEt_conto
      edEt_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055176575113851, "Codice cliente/fornitore"), tabanagra)
      'per poter inserire piva e/o cod fisc e richiaare hlan passando la piva
      edEt_conto.nMaxLen = 11
      edEt_conto.Properties.MaxLength = 11
      edAnnoDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176609336762, "Anno documento"), "0", 4, 1900, 2099)
      edSerieDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176628714118, "Serie documento"), CLN__STD.SerieMaxLen, False)
      edNumDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176648091474, "Numero documento"), "0", 9, 0, 999999999)
      edet_datdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055176673094514, "Data documento"), False)
      edEt_datapag.NTSSetParam(oMenu, oApp.Tr(Me, 129055176702629355, "Data decorrenza primo pagamento"), False)
      edEt_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055176722319249, "Codice contropartita"), tabcove)
      edEt_causale.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055176834520391, "Causale di magazzino"), tabcaum)
      ckEt_flprov.NTSSetParam(oMenu, oApp.Tr(Me, 129055176881088553, "Provvigioni estratte"), "S", "N")
      ckEt_flscdb.NTSSetParam(oMenu, oApp.Tr(Me, 129055176917968037, "Sblocca castelletti"), "S", "N")
      ckXx_przbol.NTSSetParam(oMenu, oApp.Tr(Me, 129055177423626658, "Stampa prezzi in bolla"), "S", "N")
      edEt_oriniz.NTSSetParam(oMenu, oApp.Tr(Me, 129055177539721894, "Ora inizio trasporto"), "00.00", 5, 0, NTSCDec(24.59))
      edEt_dtiniz.NTSSetParam(oMenu, oApp.Tr(Me, 129055177598785150, "Data inizio trasporto"), True)
      edEt_codntra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055177240499314, "Natura transazione"), tabntra)
      cbEt_intra.NTSSetParam(oApp.Tr(Me, 129457950913369765, "tipo movimento intrastat"))
      edEt_cup.NTSSetParam(oMenu, oApp.Tr(Me, 130471173732093137, "Codice CUP"), 15, True)
      edEt_cig.NTSSetParam(oMenu, oApp.Tr(Me, 130471174249952302, "Codice CIG"), 15, True)
      edEt_riferimpa.NTSSetParam(oMenu, oApp.Tr(Me, 130471174273813753, "Riferimento ordine"), 100, True)

      '-------------------------------------------------
      'la griglia
      dttStasino.Columns.Add("cod", GetType(String))
      dttStasino.Columns.Add("val", GetType(String))
      dttStasino.Rows.Add(New Object() {"S", "Si"})
      dttStasino.Rows.Add(New Object() {"N", "No"})
      dttStasino.Rows.Add(New Object() {"B", "Solo in bolla"})
      dttStasino.Rows.Add(New Object() {"D", "Solo in fattura"})
      dttStasino.Rows.Add(New Object() {"O", "Omaggi (imponibile)"})
      dttStasino.Rows.Add(New Object() {"M", "Sconto merce"})
      dttStasino.Rows.Add(New Object() {"X", "Sconto merce NC"})
      dttStasino.Rows.Add(New Object() {"P", "Omaggi (imp. + IVA)"})
      dttStasino.AcceptChanges()

      dttKit.Columns.Add("cod", GetType(String))
      dttKit.Columns.Add("val", GetType(String))
      dttKit.Rows.Add(New Object() {" ", "Nessuno"})
      dttKit.Rows.Add(New Object() {"A", "Analitico"})
      dttKit.Rows.Add(New Object() {"S", "Sintetico"})
      dttKit.Rows.Add(New Object() {"B", "Comp. analitica"})
      dttKit.Rows.Add(New Object() {"T", "Comp. sintetica"})
      dttKit.AcceptChanges()

      dttOrtipo.Columns.Add("cod", GetType(String))
      dttOrtipo.Columns.Add("val", GetType(String))
      dttOrtipo.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttOrtipo.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttOrtipo.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttOrtipo.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttOrtipo.Rows.Add(New Object() {"Q", "Preventivo"})
      dttOrtipo.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttOrtipo.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttOrtipo.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttOrtipo.Rows.Add(New Object() {"Y", "Impegno di produzione"})

      dttTask.Columns.Add("cod", GetType(String))
      dttTask.Columns.Add("val", GetType(String))
      dttTask.Rows.Add(New Object() {"S", "Saldato"})
      dttTask.Rows.Add(New Object() {"C", "Aperto"})
      dttTask.Rows.Add(New Object() {"Q", "Q"})
      dttTask.Rows.Add(New Object() {"V", "B"})
      dttTask.AcceptChanges()

      dttNptipo.Columns.Add("cod", GetType(String))
      dttNptipo.Columns.Add("val", GetType(String))
      dttNptipo.Rows.Add(New Object() {"", ""})
      dttNptipo.Rows.Add(New Object() {"W", "Nota di Prelievo"})
      dttNptipo.AcceptChanges()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleBoll.CaricaUnMis()
      dttUm.AcceptChanges()

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 129055178754528313, "Griglia righe documento"))
      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055178806442821, "Riga"), "0", 9, 0, 999999999)
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055178842251322, "Barcode"), 255, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129055178873994229, "Cod. Art."), tabartico, False)
      ec_codart.nMaxLen = 255 'imposto qui anche per SBC: per poter consentire l'inserimento di EAN 128 di tipo UCC (ovvero nel barcode, oltre al cod articolo, c'è anche quantità, lotto, matricola, ...)
      xxo_codalt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130002093223803441, "Cod. Art. alternativo"), 0, True)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055178895416782, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055178918715763, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055178939669209, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055178960309917, "QTA UM"), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055178980950625, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179002529547, "Q.tà  ordin."), oApp.FormatQta, 13)
      ec_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179024108469, "Prezzo IVA inc."), oApp.FormatPrzUn, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179045061915, "Prezzo valuta"), oApp.FormatPrzUnVal, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179066484468, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179088376128, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      ec_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179124340998, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      ec_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179162025927, "Sconto 3"), oApp.FormatSconti, 6, -100, 100)
      ec_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179183135742, "Sconto 4"), oApp.FormatSconti, 6, -100, 100)
      ec_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179222697099, "Sconto 5"), oApp.FormatSconti, 6, -100, 100)
      ec_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179244119652, "Sconto 6"), oApp.FormatSconti, 6, -100, 100)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179263040301, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179284150116, "Descr. magazzino"), 0, True)
      ec_ricimp.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055179307449097, "Provv. a val."), "S", "N")
      ec_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179329653495, "Provv. 1"), oApp.FormatSconti, 6)
      ec_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179351701524, "Importo provv. 1"), oApp.FormatImporti, 13)
      ec_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179373124077, "Provv. 2"), oApp.FormatSconti, 6)
      ec_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179394077523, "Importo provv. 2"), oApp.FormatImporti, 13)
      ec_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179418158349, "Controp."), tabcove)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179438642688, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179460065241, "Cod. IVA"), tabciva)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179479924104, "Descr. IVA"), 0, True)
      ec_stasino.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055179500095705, "Stampa riga"), dttStasino, "val", "cod")
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129055179518703616, "Linea/Fam."), tabcfam, False)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179540908014, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179565301578, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179594386212, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129055179611899540, "Sub C."), tabsubcomm, True)
      If oCleBoll.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420303526138037, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420303821139925, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179653493694, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179691960468, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179715728556, "Note"), 0, True)
      ec_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179736525633, "Magaz 2"), tabmaga)
      xxo_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179760293721, "Descr. magaz. 2"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179783279964, "Valore riga"), oApp.FormatImporti, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055179804546148, "Conto controp."), tabanagca)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179825812332, "Descr. conto controp."), 0, True)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055182579393270, "Cod. cliente"), tabanagra)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055179847703992, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179893207371, "Misura 1"), oApp.FormatQta, 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179925263016, "Misura 2"), oApp.FormatQta, 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179950594794, "Misura 3"), oApp.FormatQta, 13)
      xxo_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055182749912206, "Cod.Art.cli/forn"), 0, True)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055179980930380, "Prz/Qta"), "#,##0.00", 13)
      ec_flkit.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055180021273582, "Tipo Kit"), dttKit, "val", "cod")
      ec_ktriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180066307854, "Rif. riga kit"), "0", 9, 0, 999999999)
      ec_umprz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180096330702, "Prezzi x u.d.m."), 3, False)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055180119629683, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180142146819, "Descr. fase"), 0, True)
      ec_pmtaskid.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180173889726, "Id Task"), "0", 9, 0, 999999999)
      xxo_pmtaskid.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180192966744, "Descr. Task"), 0, True)
      xxo_pmqtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180222207747, "Qta res.Task"), oApp.FormatQta, 13)
      xxo_pmqtarim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180245037621, "Qta riman. Task"), oApp.FormatQta, 13)
      xxo_pmvalres.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180264896484, "Val. res. Task"), oApp.FormatImporti, 13)
      xxo_pmvalrim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180285068085, "Val. rim. Task"), oApp.FormatImporti, 13)
      ec_pmsalcon.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055180308367066, "Sal. Task"), dttTask, "val", "cod")
      xxo_pmqtadisda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180341360925, "Qta dis. Task"), oApp.FormatQta, 13)
      xxo_pmvaldisda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180367474548, "Val. dis. Task"), oApp.FormatQta, 13)
      xxo_darave.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180394995492, "Dare-Avere"), 1, True)
      ec_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180416105307, "Ubicazione"), 18, False)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180437058753, "."), "0", 4, 0, 9999)
      ec_flprznet.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055180456917616, "Prezzo netto"), "S", "N")
      ec_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055180527127297, "Cod. promoz."), tabtpro)
      xxo_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180551520861, "Descr. promoz."), 0, True)
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129055180571848831, "Dt iniz. comp. econ."), False)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129055180591551325, "Dt fin. comp. econ."), False)
      ec_causale.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055180612192033, "Causale"), tabcaum)
      xxo_causale.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180643934940, "Descr. causale"), 0)
      ec_causale2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129055180663793803, "Causale 2"), tabcaum)
      xxo_causale2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180684747249, "Descr. causale 2"), 0)
      ec_ortipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055180706951647, "Ord. tipo"), dttOrtipo, "val", "cod")
      ec_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180728374200, "Ord. anno"), "0", 4, 0, 9999)
      ec_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180754487823, "Ord. serie"), CLN__STD.SerieMaxLen, True)
      ec_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180779663232, "Ord. num"), "0", 9, 0, 999999999)
      ec_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180799052988, "Ord. riga"), "0", 9, 0, 999999999)
      ec_salcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055180832515954, "Saldo ordine"), "S", "C")
      ec_commen.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180850029282, "Num ordine (fat. proforma)"), "0", 9, 0, 999999999)
      ec_codnomc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180874891953, "Cod. nomenclatura comb."), 10)
      ec_massakg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180893812602, "Massa Kg"), oApp.FormatQta, 15)
      ec_massaum2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180932279376, "Massa Um 2"), oApp.FormatQta, 15)
      ec_valstat.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180953701929, "Valore statistico"), oApp.FormatImporti, 15)
      ec_proorig.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055180977313648, "Prov. orig./paese pagam."), 2, True)
      ec_percvst.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055180996077928, "Perc. valore stat."), oApp.FormatSconti, 6, 0, 1000)
      ec_prtipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181017813219, "Imp. tipo"), 1, True)
      ec_pranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181058156421, "Imp anno"), "0", 4, 0, 9999)
      ec_prserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181081611771, "Imp. serie"), CLN__STD.SerieMaxLen, True)
      ec_prnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181103347062, "Imp. num"), "0", 9, 0, 999999999)
      ec_prriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181124300508, "Imp. riga"), "0", 9, 0, 999999999)
      ec_cltipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181146035799, "C/lav. tipo"), 1, True)
      ec_clanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181164956448, "C/lav. anno"), "0", 4, 0, 9999)
      ec_clserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181206550602, "C/lav. serie"), CLN__STD.SerieMaxLen, True)
      ec_clnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181229536845, "C/lav. num."), "0", 9, 0, 999999999)
      ec_clriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181250959398, "C/lav. riga"), "0", 9, 0, 999999999)
      ec_nptipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129055181298651943, "N.pr. tipo"), dttNptipo, "val", "cod")
      ec_npanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181322420031, "N.pr. anno"), "0", 4, 0, 9999)
      ec_npserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181341340680, "N.pr. serie"), CLN__STD.SerieMaxLen, True)
      ec_npnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181367141565, "N.pr. num"), "0", 9, 0, 999999999)
      ec_npriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181389658701, "N.pr. riga"), "0", 9, 0, 999999999)
      ec_npqtadis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181420771022, "N.pr. qta disimp."), oApp.FormatQta, 15)
      ec_npcoldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181447497638, "N.pr. colli disimp."), oApp.FormatQta, 15)
      ec_npvaldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181471567222, "N.pr. val. disimp."), oApp.FormatImporti, 15)
      ec_npsalcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055181499075318, "N.pr. evasa"), "S", "C")
      ec_nprcoleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181520018982, "N.Pr.Colli evasi"), oApp.FormatQta, 15)
      ec_nprquaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181552372254, "N.Pr.Q.tà evasa"), oApp.FormatQta, 15)
      ec_nprflevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055181573784806, "N.Pr.Evaso"), "S", "C")
      ec_nprvalore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181595509950, "Valore nota prel."), oApp.FormatImporti, 15)
      xxo_vallordo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181615359542, "Valore riga lordo"), oApp.FormatImporti, 15)
      ec_actipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181637866166, "Acconto tipo"), 1, True)
      ec_acanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181662717230, "Acconto anno"), "0", 4, 0, 9999)
      ec_acserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181690537918, "Acconto serie"), CLN__STD.SerieMaxLen, True)
      ec_acnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181713513430, "Acconto num"), "0", 9, 0, 999999999)
      ec_cctipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181742897078, "Car.prod. tipo"), 1, True)
      ec_ccanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181763215558, "Car.prod. anno"), "0", 4, 0, 9999)
      ec_ccserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129055181780251822, "Car.prod. serie"), CLN__STD.SerieMaxLen, True)
      ec_ccnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181806353254, "Car.prod. num"), "0", 9, 0, 999999999)
      ec_ccriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181825421366, "Car.prod. riga"), "0", 9, 0, 999999999)
      xxo_valrigaic.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181857462046, "Val. riga Iva comp."), oApp.FormatImporti, 15)
      ec_flcom.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055181883563478, "Da controllare"), "S", "N")
      ec_flelab.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129055181902631590, "Controllato"), "S", "N")
      ec_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129055181930764870, "Valore in val."), oApp.FormatImporti, 15)
      ec_coddivi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130420304090204147, "Divisione CA"), tabdivi)
      xxo_coddivi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129270307473302578, "Descr. Divisione CA"), 0, True)
      ec_cvtipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129605685711201172, "C/vis. tipo"), 1, True)
      ec_cvanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129605685697763672, "C/vis. anno"), "0", 4, 0, 9999)
      ec_cvserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129605685683515625, "C/vis. serie"), CLN__STD.SerieMaxLen, True)
      ec_cvnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129605685667841797, "C/vis. num."), "0", 9, 0, 999999999)
      ec_cvriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129605685639355469, "C/vis. riga"), "0", 9, 0, 999999999)
      ec_flprzmod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975273805954, "Prezzo modificato"), "S", "N")
      ec_flsc1mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975273962207, "Sconto 1 modificato"), "S", "N")
      ec_flsc2mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274118457, "Sconto 2 modificato"), "S", "N")
      ec_flsc3mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274274710, "Sconto 3 modificato"), "S", "N")
      ec_flsc4mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274430968, "Sconto 4 modificato"), "S", "N")
      ec_flsc5mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274587217, "Sconto 5 modificato"), "S", "N")
      ec_flsc6mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274743479, "Sconto 6 modificato"), "S", "N")
      ec_flprov1mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274899728, "Provvigione agente 1 modificata"), "S", "N")
      ec_flprov2mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975275055973, "Provvigione agente 2 modificata"), "S", "N")
      xxo_valoreiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130795171558587211, "Valore ivato"), oApp.FormatImporti, 15)

      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129248603853818813, "Sel."), "S", "N")
      xxo_percricarico.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804010780708623, "% Ricarico"), oApp.FormatSconti, 6)
      xxo_percmargine.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804011016258064, "% Margine"), oApp.FormatSconti, 6)
      xxo_marginevalore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804011166848848, "Margine valore"), oApp.FormatImporti, 13)
      '-------------------------------------------------
      'gli zoom aggiuntivi/particolari di movord
      xxo_lottox.NTSSetParamZoom("ZOOMANALOTTI")
      ec_prezzo.NTSSetParamZoom("ZOOMPREZZO")
      ec_prezvalc.NTSSetParamZoom("ZOOMPREZZO")
      ec_preziva.NTSSetParamZoom("ZOOMPREZZO")
      ec_pmtaskid.NTSSetParamZoom("ZOOMPMTASK")
      ec_ubicaz.NTSSetParamZoom("ZOOMUBICAZ")
      ec_codnomc.NTSSetParamZoom("ZOOMTARIC")

      edNumDoc.NTSForzaVisZoom = True

      ec_preziva.NTSForzaVisZoom = True
      ec_prezvalc.NTSForzaVisZoom = True
      ec_prezzo.NTSForzaVisZoom = True

      ec_codart.NTSSetRichiesto()
      ec_quant.NTSSetRichiesto()
      ec_prezzo.NTSSetRichiesto()
      ec_codiva.NTSSetRichiesto()
      ec_controp.NTSSetRichiesto()
      ec_causale.NTSSetRichiesto()
      ec_magaz.NTSSetRichiesto()

      grvRighe.AddColumnBackColor("backcolor_row") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_prezzo") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_preziva") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_prezvalc") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont1") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont2") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont3") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont4") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont5") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_scont6") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_provv") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_vprovv") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_provv2") 'sempre nella InitControls
      grvRighe.AddColumnBackColor("backcolor_ec_vprovv2") 'sempre nella InitControls

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
  Public Overridable Sub FormatQtaEsistCorpo()
    Try
      edDispNetta.NTSSetParam(oMenu, oApp.Tr(Me, 129048471154378121, "Disponibilità netta"), oCleBoll.strFormatQtaEsistCorpo)
      edDispon.NTSSetParam(oMenu, oApp.Tr(Me, 129048471204535655, "Disponibilità"), oCleBoll.strFormatQtaEsistCorpo)
      edOrdinato.NTSSetParam(oMenu, oApp.Tr(Me, 129048471246099219, "Ordinato"), oCleBoll.strFormatQtaEsistCorpo)
      edImpegnato.NTSSetParam(oMenu, oApp.Tr(Me, 129048471282350147, "Impegnato"), oCleBoll.strFormatQtaEsistCorpo)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi di FORM"
  Public Overridable Sub FRMVEBOLL_T_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      If Me.Modal = True Then Me.MinimizeBox = False

      'nascondo tutte le colonne che, perdefault, non dovrebbero essere visibili in una gestione ordinaria.
      'poi l'utente renderà visibili quelle che vuole
      For Each col As NTSGridColumn In grvRighe.Columns
        Select Case col.Name
          Case "ec_riga", "ec_codart", "ec_descr", "ec_unmis", "ec_colli", "ec_ump", "ec_quant", "ec_prezzo", "ec_scont1", "ec_scont2", _
               "ec_stasino", "ec_valore", "ec_note", "ec_oranno", "ec_orserie", "ec_ornum", "ec_orriga", "ec_salcon", _
               "ec_npanno", "ec_npserie", "ec_npnum", "ec_npriga", "ec_npsalcon"
            'colonne visibili
          Case Else
            col.Visible = False
        End Select
      Next

      If bPreload Then Return

      Me.Cursor = Cursors.WaitCursor
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      oCleBoll.LeggePevePeac()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      tsBoll.SelectedTabPageIndex = 0

      ''-------------------------------------------------
      ''leggo dal database i dati
      ''NB: se ci saranno dei controlli collegati a datasource, nel datatable sottostante
      ''     un record deve esempre esserci, almeno in addnew...
      Me.NTSFormClearDataBinding(Me)
      dsBoll = New DataSet
      dsBoll.Tables.Add("TESTA")
      dsBoll.Tables.Add("TESTAC")
      dsBoll.Tables.Add("CORPO")
      dcBoll.DataSource = dsBoll.Tables("TESTA")
      dcBollc.DataSource = dsBoll.Tables("TESTAC")
      dsBoll.AcceptChanges()
      dcBollRighe.DataSource = dsBoll.Tables("CORPO")

      '-------------------------------------------------
      'non collego il BindingSource ai vari controlli: lo far solo dopo aver ottenuto il dataset nella nuovo o Apri
      'diversamente da dei messaggi strani sui campi checkbox e radiobutton

      SetStato(0, False)

      '-------------------------------------------------
      'imposto l'ordinamento settato per le righe del corpo
      Dim strOrdrig As String
      strOrdrig = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "RECENT", ".", "RecordOrdinaModo", "0", " ", "0")
      Select Case strOrdrig
        Case "1"
          tlbOrdinaRigheCodiceArticolo.Down = True
          tlbOrdinaRigheCodiceArticolo_ItemClick(Nothing, Nothing)
        Case "2"
          tlbOrdinaRigheDescrizione.Down = True
          tlbOrdinaRigheDescrizione_ItemClick(Nothing, Nothing)
        Case Else
          tlbOrdinaRigheRiga.Down = True
          tlbOrdinaRigheRiga_ItemClick(Nothing, Nothing)
      End Select

      edEt_datapag.Enabled = False

      tlbStampaEtichetteFinale.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "RECENT", ".", "ReportStampaEtiFin", "0", " ", "0"))
      tlbRiapriDopoSalva.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "RECENT", ".", "RiapriDopoSalva", "0", " ", "0"))
      tlbSalvanuovo.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "RECENT", ".", "Salvanuovo", "0", " ", "0"))

      oCleBoll.bModSupRme = CBool(oCleBoll.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME)
      If Not oCleBoll.bModSupRme Then
        tlbSiub.Visible = False
        tlbSiub.ItemShortcut = Nothing
        tlbGiub.Visible = False
        tlbGiub.ItemShortcut = Nothing
      End If

      GctlTipoDoc = ""
      GctlSetRoules()     'per impostare eventuali blocchi sul tipo documento
      GestisciBarcodeIn()

      tlbSalvanuovo_ItemClick(tlbSalvanuovo, Nothing)

      xx_sel.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRMVEBOLL_T_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim strParam() As String

    Try
      Me.Cursor = Cursors.WaitCursor

      If Not LeggiDatiDitta() Then Return

      '-------------------------------------------------
      'reimposto l'ultimo tipo documento utilizzato
      'dopo la GCTlsetroules, altrimenti la griglia non viene impostata correttamente ...
      cbTipoDoc.SelectedValue = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "Recent", ".", "LastTipoDoc", " ", " ", "A")
      If cbTipoDoc.SelectedIndex = -1 Then cbTipoDoc.SelectedIndex = 0
      edAnnoDoc.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "Recent", ".", "LastAnnoDoc", DateTime.Now.Year.ToString, " ", DateTime.Now.Year.ToString)
      edSerieDoc.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "Recent", ".", "LastSerieDoc", " ", " ", " ").PadLeft(1)

      cbTipoDoc_SelectedIndexChanged(cbTipoDoc, Nothing)    'per far applicare la GctlSetRoules in base al tipo documento

      '---------------------------
      'leggo i parametri passati dal child chiamante, se presenti
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          strParam = oCallParams.strParam.Split(";"c)
          Select Case strParam(0)
            Case "NUOV"
              If tlbNuovo.Visible And tlbNuovo.Enabled Then
                'es. chamato da ...... ANCHE CON RICERCHE AVANZATE NON HO TROVATO NESSUN DOCUMENTO CHE CHIAMASSE BSVEBOLL IN QUESTO MODO IN BUS 13!!!!!
                'viene passato anche l'instid di motrans ... devo caricarlo nel datatable 
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & IIf(strParam(1) = "U", "T", strParam(1)).ToString & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = IIf(strParam(1) = "U", "T", strParam(1)).ToString
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edSerieDoc.Text = strParam(2)
                  tlbNuovo_ItemClick(tlbNuovo, Nothing)
                  If oCleBoll.dttET.Rows.Count = 0 Then Return
                  edEt_conto.Text = NTSCInt(strParam(3)).ToString
                  edEt_tipobf.Text = NTSCInt(strParam(4)).ToString
                  'm_fmForm.DagestIImotrans = CLng(Trim$(Mid(strParam, 34, 9)))
                  edEt_codpaga.Text = NTSCInt(strParam(7)).ToString
                  edEt_riferim.Text = strParam(8)
                  oApp.MsgBoxErr("Messaggio per programmatore: caricare/proporre righe documento da motrans ...")
                End If
              End If
            Case "NUOD"
              If tlbNuovo.Visible And tlbNuovo.Enabled Then
                'es.chiamato da consolle clienti/fornitori/articoli -> nuovo documento
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & IIf(strParam(1) = "U", "T", strParam(1)).ToString & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = IIf(strParam(1) = "U", "T", strParam(1)).ToString
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edAnnoDoc.Text = NTSCInt(strParam(2)).ToString
                  tlbNuovo_ItemClick(tlbNuovo, Nothing)
                  If oCleBoll.dttET.Rows.Count = 0 Then Return
                  If bPreload Then
                    'non so perchè, ma senza la riga qui sotto (che tra l'altro non azzera il campo et_caustra ...) in inserimento codice conto da il messaggio che la causale del trasporto deve essere un numero diverso da 0 ...
                    'forse con la riga sotto forzo il refresh del datasource ... 
                    edEt_caustra.Text = "0"
                  End If
                  edEt_conto.NTSTextDB = NTSCInt(strParam(3)).ToString
                End If
              End If
            Case Else
              If tlbApri.Visible And tlbApri.Enabled Then
                'es. chiamato da navigazione documentale -> apri documento
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & IIf(strParam(1) = "U", "T", strParam(1)).ToString & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = IIf(strParam(1) = "U", "T", strParam(1)).ToString
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edAnnoDoc.Text = NTSCInt(strParam(2)).ToString
                  edSerieDoc.Text = strParam(3)
                  edNumDoc.Text = NTSCInt(strParam(4)).ToString
                  tlbApri_ItemClick(tlbApri, Nothing)
                End If
              End If
          End Select
        ElseIf oCallParams.dPar1 <> 0 Then
          lContoProposto = NTSCInt(oCallParams.dPar1)
        ElseIf Not oCallParams.ctlPar1 Is Nothing Then
          bNoResetDttOpens = True
          dttOpens = CType(oCallParams.ctlPar1, DataTable)
          Dim strParams() As String = oCallParams.strPar5.Split(";"c)
          Dim bTrovato As Boolean = False
          For z As Integer = 0 To dttOpens.Rows.Count - 1
            If strParams(0) = dttOpens.Rows(z)!tm_tipork.ToString And strParams(1) = dttOpens.Rows(z)!tm_anno.ToString And _
               strParams(2) = dttOpens.Rows(z)!tm_serie.ToString And strParams(3) = dttOpens.Rows(z)!tm_numdoc.ToString Then
              cbTipoDoc.SelectedValue = dttOpens.Rows(z)!tm_tipork.ToString
              edAnnoDoc.Text = dttOpens.Rows(z)!tm_anno.ToString
              edSerieDoc.Text = dttOpens.Rows(z)!tm_serie.ToString
              edNumDoc.Text = dttOpens.Rows(z)!tm_numdoc.ToString

              tlbApri_ItemClick(tlbApri, Nothing)
              bTrovato = True
              Exit For
            End If
          Next
          If Not bTrovato Then
            cbTipoDoc.SelectedValue = dttOpens.Rows(0)!tm_tipork.ToString
            edAnnoDoc.Text = dttOpens.Rows(0)!tm_anno.ToString
            edSerieDoc.Text = dttOpens.Rows(0)!tm_serie.ToString
            edNumDoc.Text = dttOpens.Rows(0)!tm_numdoc.ToString

            tlbApri_ItemClick(tlbApri, Nothing)
          End If
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
  Public Overridable Sub FRMVEBOLL_T_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try

      oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "RecordOrdinaModo", NTSCStr(oCleBoll.nOrdrig), " ", "NS.", "...", "...")
      '-------------------------------------------------
      'se necessario salvo il documento
      If tsBoll.Visible = True Then
        If tlbSalva.Enabled Then
          If Not Salva(-1, True) Then
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
        oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "LastTipoDoc", cbTipoDoc.SelectedValue.ToString, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "LastAnnoDoc", edAnnoDoc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "LastSerieDoc", edSerieDoc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BSVEBOLL", "RECENT", ".", "ReportStampaEtiFin", NTSCStr(IIf(tlbStampaEtichetteFinale.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
        oMenu.SaveSettingBus("BSVEBOLL", "RECENT", ".", "Salvanuovo", NTSCStr(IIf(tlbSalvanuovo.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
        oMenu.SaveSettingBus("BSVEBOLL", "RECENT", ".", "RiapriDopoSalva", NTSCStr(IIf(tlbRiapriDopoSalva.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
      End If

      If oCleBoll.bModPM Then
        oMenu.ResetTblInstId("TTPROESEC", False, oCleBoll.lIIttproesebappo)
        oMenu.ResetTblInstId("TTTASKS", False, oCleBoll.lIItttasks)
      End If
      oCleBoll.RemoveHandlerEntity()
      oCleBoll.DocumentLockRemove()

      If bCreaCarProd Then CreaCaricoProduzione()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEBOLL_T_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim dPrezzo As Decimal = 0
    Try
      'If grvRighe.FocusedColumn.Name = "ec_codart" Then
      If NTSCDec(DateTime.Now.Subtract(dtLastCellKeyPress).TotalMilliseconds) < 50 Then
        Me.tlbRipristina.ItemShortcut = Nothing 'Sono stati trovati barcode EAN128 con terminatore del lotto = F8
      Else
        Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
      End If
      'Else
      'ec_codart.nMaxLen = CLN__STD.CodartMaxLen
      'Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
      'End If
      dtLastCellKeyPress = DateTime.Now

      If e.KeyCode = Keys.Q And e.Shift And Not e.Alt And Not e.Control And _
        Not grvRighe.NTSGetCurrentDataRow Is Nothing And grvRighe.FocusedColumn.Name = "ec_prezzo" And ec_prezzo.Enabled Then
        e.Handled = True
        If oCleBoll.ScorporaPrezzo(grvRighe.NTSGetCurrentDataRow) = True Then
          e.SuppressKeyPress = True
          grvRighe.NTSMoveNextColunn()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRMVEBOLL_T_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    'se da BNDKKONS trascino delle righe d'ordine da trasformare in documento ....
    Dim strData As String = ""
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing
    Dim nStrT As Integer = 0
    Try
      strData = NTSCStr(e.Data.GetData(DataFormats.Text))
      If strData.Trim = "" Then Return
      strT = strData.Split("|"c)
      If strT(0) <> "BNDKKONS:Ordini" Then Return 'non sono stato chiamato da BNDKKONS pannello ordini

      Me.Cursor = Cursors.WaitCursor

      For nStrT = 1 To strT.Length - 1
        strT1 = strT(nStrT).Split(";"c)

        '------------------------
        'solo la prima volta verifico che il tipork di destinazione sia coerente con l'ordine da evadere
        If nStrT = 1 Then

          Select Case strT1(0)
            Case "R"
              If cbTipoDoc.SelectedValue <> "A" And cbTipoDoc.SelectedValue <> "B" And _
                 cbTipoDoc.SelectedValue <> "C" And cbTipoDoc.SelectedValue <> "F" And _
                 cbTipoDoc.SelectedValue <> "S" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 129684164561060173, "Per evadere degli 'Impegni cliente' è necessario che il tipo documento sia 'DDT emesso o Fattura immediata emessa o Corrispettivo o Ric.Fiscale Emessa o Fatt.Ric.Fisc. Emessa'"))
                Return
              End If
            Case "O"
              If cbTipoDoc.SelectedValue <> "M" And cbTipoDoc.SelectedValue <> "L" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 129684148420877812, "Per evadere degli 'Ordini fornitore' è necessario che il tipo documento sia 'DDT ricevuto o Fattura immediata ricevuta'"))
                Return
              End If
            Case "H"
              If cbTipoDoc.SelectedValue <> "T" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 129684147930936943, "Per evadere degli 'Ordini di produzione' è necessario che il tipo documento sia 'Carico da produzione'"))
                Return
              End If
            Case "X"
              If cbTipoDoc.SelectedValue <> "B" And cbTipoDoc.SelectedValue <> "Z" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 129684157440145655, "Per evadere degli 'Impegni di trasferimento' è necessario che il tipo documento sia 'DDT emesso o Bolla di movim. interna'"))
                Return
              End If
          End Select    'Select Case strT1(0)

          If pnCorpo.Visible Then
            'documento già inizializzato
            If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_conto) <> NTSCInt(strT1(1)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129684160866751675, "L'intestatario del documento e' diverso da quello delle righe da evadere"))
              Return
            End If
          Else
            'documento non ancora inizializzato
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            'dati di testata
            dsBoll.Tables("TESTA").Rows(0)!et_conto = NTSCInt(strT1(1))
          End If

          If oCleBoll.OkTestata() Then
            tsBoll.SelectedTabPageIndex = 1
            grRighe.Focus()
            grvRighe.FocusedColumn = ec_codart
          End If
        End If    'If nStrT = 1 Then

        '------------------------
        'ora aggiungo le righe del corpo
        oCleBoll.EvadiRigaOrdineDkkons(strT1(0), NTSCInt(strT1(2)), strT1(3), NTSCInt(strT1(4)), NTSCInt(strT1(5)), NTSCDec(strT1(6)))

      Next    'For nStrT = 1 To strT.Length - 1


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
                                                      ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    'modifico la funzione standard dell'autocompletamento per fare in modo che da edEt_conto 
    'vengano visti solo clienti o fornitori a seconda che il documento sia emesso o ricevuto
    Try
      If edEt_conto.ContainsFocus Then
        If oCleBoll.bDocEmesso Then
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
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edAnnoDoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnnoDoc.ValidatedAndChanged
    Try
      edNumDoc.Text = "0"
      dttOpens.Clear()
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
      If edSerieDoc.Text <> edSerieDoc.Text.ToUpper Then
        edSerieDoc.Text = edSerieDoc.Text.ToUpper
      End If

      strTmp = TestSerieMaxLen(edSerieDoc.Text, False)
      If strTmp <> edSerieDoc.Text Then edSerieDoc.Text = strTmp

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

  Public Overridable Function GestisciBarcodeIn() As Boolean
    Try
      'su android/ios non riesco a gestire il posizionamento del focus sulla riga nuova.
      'raddoppia le righe a causa di eventi strani di ANDROID

      If CLN__STD.IsBis And oApp.SO_TYPE.StartsWith("WIN") = False And _
         CBool(Val(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Opzioni", ".", "ConfermaRigaBarcode", "0", " ", "0"))) Then
        ec_matric.Enabled = False
        edBarcodeIn.Visible = True
        lbBarcodeIn.Visible = True
      Else
        GctlSetVisEnab(ec_matric, False)
        edBarcodeIn.Visible = False
        lbBarcodeIn.Visible = False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub edBarcodeIn_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edBarcodeIn.Enter
    Try
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then
        grRighe.Focus()
        oApp.MsgBoxErr(oApp.Tr(Me, 131109742495586922, "Salvare prima la riga di griglia in modifica"))
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edBarcodeIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edBarcodeIn.KeyDown
    Try
      If edBarcodeIn.Visible = False Then Return
      If e.KeyCode = Keys.Enter And edBarcodeIn.Text.Trim <> "" Then
        If grRighe.Enabled And grvRighe.Enabled And grvRighe.NTSAllowInsert Then
          oCleBoll.dttEC.Rows.Add(oCleBoll.dttEC.NewRow)
          oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_matric = edBarcodeIn.Text
          If NTSCStr(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_codart).Trim = "" Then
            'barcode non collegato a nessun articolo
            oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
          End If
          grvRighe.NTSMoveLastRow()
          edBarcodeIn.Text = ""
          Me.ValidaLastControl()
          edBarcodeIn.Focus()
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 131109742495586921, "Non è possibile aggiungere righe nella griglia"))
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edEt_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edEt_coddest.NTSZoomGest
    'limite superato     e.ZoomHandled = True        'non faccio eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub
  Public Overridable Sub edEt_coddest2_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edEt_coddest2.NTSZoomGest
    'limite superato     e.ZoomHandled = True        'non faccio eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub

  Public Overridable Sub edEt_conto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_conto.Validated
    Try
      If oCleBoll Is Nothing Then Return
      If bInLoadDoc = True Then Return
      '---------------------
      'CRM: testo se posso agire con il conto selezionato
      If NTSCInt(edEt_conto.Text) = NTSCInt(edEt_conto.NTSOldValue) And dsBoll.Tables("TESTA").Rows.Count > 0 And NTSCInt(edEt_conto.Text) <> 0 Then
        If oCleBoll.bModuloCRM And oCleBoll.bIsCRMUser And NTSCStr(dsBoll.Tables("TESTA").Rows(0)!xx_tipo).Trim <> "" Then
          If NTSCStr(dsBoll.Tables("TESTA").Rows(0)!xx_tipo) = "C" Then
            oCleBoll.bAccvis = oMenu.CercaAccessiCrmDaLead(DittaCorrente, oCleBoll.lLead, oCleBoll.bAccmod)
            If oCleBoll.bAccvis = False Then
              oCleBoll.bNonValidare = False
              edEt_conto.Text = "0"
              lbXx_conto.Text = ""
              oApp.MsgBoxErr(oApp.Tr(Me, 128776114797006000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi al Cliente di questo documento."))
            End If
          Else
            If oCleBoll.bAmm = False Then
              oCleBoll.bNonValidare = False
              edEt_conto.Text = "0"
              lbXx_conto.Text = ""
              oApp.MsgBoxErr(oApp.Tr(Me, 128520466482044000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi ai fornitori."))
            End If
          End If
        End If      ' If oCleBoll.bModuloCRM And oCleBoll.bIsCRMUser Then

      End If

      '-----------------------------------------------------------------------------------
      'Descrizione testata e C/F
      If NTSCInt(edEt_conto.Text) = oCleBoll.lConclpriv And Not (cbTipoDoc.SelectedValue <> "C" And cbTipoDoc.SelectedValue <> "F" And cbTipoDoc.SelectedValue <> "I") Then
        'Richiede, quando possibile, l'anagrafica privata
        ApriAnagraficaPrivata(oCleBoll.bApriAnagraficaPrivata)
      End If

      If NTSCInt(edEt_valuta.Text) = 0 Then
        edEt_cambio.Enabled = False
      Else
        GctlSetVisEnab(edEt_cambio, False)
      End If

      SettaCampiValuta()
      
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edEt_codese_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_codese.Validated
    'se faccio in questo modo, se indico un codice iva non corretto l'entity da il messaggio, 
    'rimette il codice iva originale ma poi quando arrivo qui da 
    'comunque il messaggio di codice iva da variare su tutte le righe del corpo
    'If NTSCInt(edEt_codese.Text) <> 0 And dsBoll.Tables("CORPO").Rows.Count > 0 Then
    '  If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791955669687500, "Modificare il Codice IVA su tutte le righe di questo documento?")) = Windows.Forms.DialogResult.Yes Then
    '    oCleBoll.CambioDatiTestataInGriglia(NTSCInt(edEt_codese.Text), 0, 0, 0, 0, 0, "", "")
    '  End If
    'End If
  End Sub

  Public Overridable Sub edEt_codpaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_codpaga.Validated
    Try
      If oCleBoll Is Nothing Then Return

      If NTSCInt(edEt_codpaga.Text) = 0 Then
        edEt_datapag.Enabled = False
      Else
        If oCleBoll.GetTipoPag(edEt_codpaga.Text) = 4 Then
          GctlSetVisEnab(edEt_datapag, False)
        Else
          edEt_datapag.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edEt_valuta_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_valuta.Validated
    Try
      If oCleBoll Is Nothing Then Return
      SettaCampiValuta()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edEt_cambio_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_cambio.Leave
    Try
      '--------------------
      'se serve aggiorno il cambio nella tabella
      If NTSCDec(edEt_cambio.Text) <> 0 And NTSCDec(edEt_cambio.Text) <> NTSCDec(edEt_cambio.NTSOldValue) And NTSCInt(edEt_valuta.Text) <> 0 Then
        If dsBoll.Tables("CORPO").Rows.Count > 0 Then
          'se sono sulla cella 'valore riga' non aggiorna il valore di quella riga ...
          grvRighe.NTSMoveFirstColunn()
        End If
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128521436577992000, "Inserire il cambio indicato nella tabella dei CAMBI per la valuta '|" & _
                            edEt_valuta.Text & "|'")) = Windows.Forms.DialogResult.Yes Then
          oCleBoll.AggiornaCambio(NTSCInt(edEt_valuta.Text), NTSCDate(edet_datdoc.Text).ToShortDateString, NTSCDec(edEt_cambio.Text), True)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edEt_totcoll_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_totcoll.KeyPress
    If e.KeyChar <> Chr(13) Then oCleBoll.bCalcolaColli = False
  End Sub

  Public Overridable Sub edEt_pesonetto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_pesonetto.KeyPress
    If e.KeyChar <> Chr(13) Then oCleBoll.bCalcolaPesoNetto = False
  End Sub

  Public Overridable Sub edEt_peso_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_peso.KeyPress
    If e.KeyChar <> Chr(13) Then oCleBoll.bCalcolaPesoLordo = False
  End Sub

  Public Overridable Sub edEt_note_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_note.Enter
    Try
      Me.NTSDisableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edEt_note_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_note.Leave
    Try
      Me.NTSEnableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edEt_tipobf_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_tipobf.Validated
    Try
      MagazzinoSecondario()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edEt_causale_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_causale.Validated
    Try
      MagazzinoSecondario()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edEt_magaz_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_magaz.Validated
    Try
      MagazzinoSecondario()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckEt_scorpo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckEt_scorpo.CheckedChanged
    If ckEt_scorpo.Checked = True Then
      xxo_valoreiva.Visible = True
      GctlSetVisEnab(ec_preziva, False)
      ec_preziva.Visible = True
      ec_prezzo.Enabled = False
    Else
      ec_preziva.Visible = False
      ec_preziva.Enabled = False
      xxo_valoreiva.Visible = False
      GctlSetVisEnab(ec_prezzo, False)
    End If
  End Sub

  Public Overridable Sub ckEt_flscdb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckEt_flscdb.CheckedChanged
    Try
      If ckEt_flscdb.Checked Then
        GctlSetVisEnab(edEt_speinc, False)
        GctlSetVisEnab(edEt_bolli, False)
        GctlSetVisEnab(edEt_totomag, False)
        If NTSCInt(edEt_valuta.Text) <> 0 Then
          GctlSetVisEnab(edEt_speincv, False)
          GctlSetVisEnab(edEt_bolliv, False)
          GctlSetVisEnab(edEt_totomagv, False)
        End If
      Else
        edEt_speinc.Enabled = False
        edEt_bolli.Enabled = False
        edEt_totomag.Enabled = False
        edEt_speincv.Enabled = False
        edEt_bolliv.Enabled = False
        edEt_totomagv.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbTipoDoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoDoc.SelectedIndexChanged
    Dim strSoloSerie As String
    Try
      edNumDoc.Text = "0"
      dttOpens.Clear()
      If cbTipoDoc.SelectedIndex = -1 Then cbTipoDoc.SelectedIndex = 0
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        edSerieDoc.Text = strSoloSerie.ToUpper
        edSerieDoc.Enabled = False
      Else
        edSerieDoc.Enabled = True
      End If

      'se devo bloccare la nuovo/apri solo di un determinato tipo documento .... devo farlo qui.
      'solo se non sono nella form_load)
      If edNumDoc.strNomeCampo <> "" Then
        If GctlTipoDoc <> cbTipoDoc.SelectedValue Then
          'devo farlo solo se il tipork è diverso rispetto al contenuto di GctlTipoDoc
          'diversamente se nella configuraz. rendo non visibile una voce di cbTipoDoc
          'entra in un loop infinito e manda in crash Business (es tipork 'O' non visibile per utnete/ditta)
          GctlTipoDoc = cbTipoDoc.SelectedValue
          GctlSetRoules()
          GestisciBarcodeIn()
        End If
      End If
      NTSGetDataAutocompletamentoTipork = cbTipoDoc.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdModifScad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModifScad.Click
    Try
      ApriScadenze()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdUltSpese_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUltSpese.Click
    Try
      If oCleBoll.bDocEmesso Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128669121521875000, "Spese ulteriori inseribili solo su documenti ricevuti."))
        Return
      End If

      '-----------------------------
      'visualizzo la form
      Dim frmSpex As FRMVESPEX = Nothing
      frmSpex = CType(NTSNewFormModal("FRMVESPEX"), FRMVESPEX)
      frmSpex.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmSpex.oCleBoll = oCleBoll
      frmSpex.ShowDialog()
      frmSpex.Dispose()
      frmSpex = Nothing

      'ho selezionato annulla
      'If frmSpex.bOk = False Then Return
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
    Dim frmCast As FRMVECAST = Nothing
    Dim oTestata As CLELBMENU.OutTestata = Nothing

    Try
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      '----------------------
      'aggiorno i totali
      oCleBoll.CalcolaTotali(oTestata)

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
                                      oCleBoll.dttET.Rows(0)("et_codiva_" & i.ToString), _
                                      oCleBoll.dttET.Rows(0)("et_imponib_" & i.ToString), _
                                      oCleBoll.dttET.Rows(0)("et_imposta_" & i.ToString), _
                                      oCleBoll.dttET.Rows(0)("et_imponibv_" & i.ToString), _
                                      oCleBoll.dttET.Rows(0)("et_impostav_" & i.ToString)})
      Next
      For i = 1 To 20
        dttControp.Rows.Add(New Object() {i, _
                                      oCleBoll.dttETC.Rows(0)("tm_ccontr_" & i.ToString), _
                                      oCleBoll.dttETC.Rows(0)("tm_impcont_" & i.ToString), _
                                      oCleBoll.dttETC.Rows(0)("tm_impcontv_" & i.ToString)})
      Next
      dttIva.AcceptChanges()
      dttControp.AcceptChanges()

      '----------------------
      'visualizzo il child
      frmCast = CType(NTSNewFormModal("FRMVECAST"), FRMVECAST)
      frmCast.Init(oMenu, Nothing)
      frmCast.InitEntity(oCleBoll)
      frmCast.ds.Tables.Add(dttIva)
      frmCast.ds.Tables.Add(dttControp)
      frmCast.bBloccaGriglie = Not ckEt_flscdb.Checked
      frmCast.dDiffCorpoRighe = ArrDbl(oTestata.dTotimpocontr - oTestata.dTotimporighe, oMenu.TrovaNdec(0)) ' forse il confornto nonnva fatto con totimpocontr..

      frmCast.ShowDialog()
      oCleBoll.bHasChangesET = True

      '----------------------
      'riprendo i dati
      oCleBoll.dttET.Rows(0)!et_dtcomiva = NTSCDate(frmCast.edDtcomiva.Text)

      For i = 1 To 8
        oCleBoll.dttET.Rows(0)("et_codiva_" & i.ToString) = dttIva.Rows(i - 1)!xx_codiva
        oCleBoll.dttET.Rows(0)("et_imponib_" & i.ToString) = dttIva.Rows(i - 1)!xx_imponib
        oCleBoll.dttET.Rows(0)("et_imposta_" & i.ToString) = dttIva.Rows(i - 1)!xx_imposta
        oCleBoll.dttET.Rows(0)("et_imponibv_" & i.ToString) = dttIva.Rows(i - 1)!xx_imponibv
        oCleBoll.dttET.Rows(0)("et_impostav_" & i.ToString) = dttIva.Rows(i - 1)!xx_impostav
      Next
      For i = 1 To 20
        oCleBoll.dttETC.Rows(0)("tm_ccontr_" & i.ToString) = dttControp.Rows(i - 1)!xx_ccontr
        oCleBoll.dttETC.Rows(0)("tm_impcont_" & i.ToString) = dttControp.Rows(i - 1)!xx_impcont
        oCleBoll.dttETC.Rows(0)("tm_impcontv_" & i.ToString) = dttControp.Rows(i - 1)!xx_impcontv
      Next

      '----------------------
      'aggiorno i totali
      oCleBoll.CalcolaTotali()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oTestata = Nothing
      If Not frmCast Is Nothing Then frmCast.Dispose()
      frmCast = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdDatiTesta1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiTesta1.Click
    Try
      cmdDatiTesta1.Font = New Font(cmdDatiTesta1.Font, FontStyle.Bold)
      If cmdDatiTesta2.Font.Bold Then cmdDatiTesta2.Font = New Font(cmdDatiTesta2.Font, FontStyle.Regular)

      pnDatiTesta2.Visible = False

      pnDatiTesta2.Dock = DockStyle.None

      pnDatiTesta1.Visible = True
      pnDatiTesta1.Dock = DockStyle.Fill

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDatiTesta2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiTesta2.Click
    Try
      If cmdDatiTesta1.Font.Bold Then cmdDatiTesta1.Font = New Font(cmdDatiTesta1.Font, FontStyle.Regular)
      cmdDatiTesta2.Font = New Font(cmdDatiTesta2.Font, FontStyle.Bold)

      pnDatiTesta1.Visible = False

      pnDatiTesta1.Dock = DockStyle.None

      pnDatiTesta2.Visible = True
      pnDatiTesta2.Dock = DockStyle.Fill

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdPiedeTotali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPiedeTotali.Click
    Try
      cmdPiedeTotali.Font = New Font(cmdPiedeTotali.Font, FontStyle.Bold)
      If cmdPiedeTrasporto.Font.Bold Then cmdPiedeTrasporto.Font = New Font(cmdPiedeTrasporto.Font, FontStyle.Regular)
      
      pnPiedeTrasporto.Visible = False

      pnPiedeTrasporto.Dock = DockStyle.None

      pnPiedeTotali.Visible = True
      pnPiedeTotali.Dock = DockStyle.Fill

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdPiedeAltro1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPiedeTrasporto.Click, cmdPiedeTrasporto.Click
    Try
      If cmdPiedeTotali.Font.Bold Then cmdPiedeTotali.Font = New Font(cmdPiedeTotali.Font, FontStyle.Regular)
      cmdPiedeTrasporto.Font = New Font(cmdPiedeTrasporto.Font, FontStyle.Bold)

      pnPiedeTotali.Visible = False

      pnPiedeTotali.Dock = DockStyle.None

      pnPiedeTrasporto.Visible = True
      pnPiedeTrasporto.Dock = DockStyle.Fill

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi di Griglia"

  Public Overridable Sub grRighe_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grRighe.Enter
    Try
      'spostato nella tsBoll_SelectedPageChanged
      'If oCleBoll.OkTestata() Then
      'If grvRighe.FocusedRowHandle = -999999 And grvRighe.RowCount > 0 Then
      '  'mi devo posizionare sulla prima cella in alto a SX
      'End If
      'Else
      '  tsBoll.Focus()
      'End If

      If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) = "S" Or bCa2InCG Then
        ec_codart.Enabled = False
        ec_fase.Enabled = False
        ec_unmis.Enabled = False
        ec_colli.Enabled = False
        ec_quant.Enabled = False
        ec_prezzo.Enabled = False
        ec_preziva.Enabled = False
        ec_prezvalc.Enabled = False
        ec_scont1.Enabled = False
        ec_scont2.Enabled = False
        ec_scont3.Enabled = False
        ec_scont4.Enabled = False
        ec_scont5.Enabled = False
        ec_scont6.Enabled = False
        ec_codiva.Enabled = False
        ec_stasino.Enabled = False
        If bCa2InCG Then
          ec_datini.Enabled = False
          ec_datfin.Enabled = False
          ec_codcena.Enabled = False
          ec_codcfam.Enabled = False
          ec_commeca.Enabled = False
          ec_subcommeca.Enabled = False
          ec_coddivi.Enabled = False
        End If
      End If

      '----------------------------
      'devo bloccare la colomma ec_valore se necessario
      '"Impossibile immettere un valore globale per la riga per i CARICHI DI PRODUZIONE."
      '"Impossibile immettere un valore globale per la riga per i documenti in valuta o IVA compresa (scorporo)."

      '"Impossibile immettere un valore globale lordo per la riga per i CARICHI DI PRODUZIONE."
      '"Impossibile immettere un valore lordo globale per la riga per i documenti in valuta o IVA compresa (scorporo)."
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Or ckEt_scorpo.Checked Or NTSCInt(edEt_valuta.Text) <> 0 Then
        ec_valore.Enabled = False
        xxo_vallordo.Enabled = False
      Else
        'se è un documento RETAIL non posso cambiare i valori
        If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) <> "S" And bCa2InCG = False Then
          GctlSetVisEnab(ec_valore, False)
          GctlSetVisEnab(xxo_vallordo, False)
        End If
      End If

      AbilitaCellaCodart()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grRighe_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grRighe.Leave
    Try
      Me.Cursor = Cursors.WaitCursor
      If Me.GctlControllaOutNotEqual("grvRighe") = False Then
        grRighe.Focus()
        Return
      End If

      ProponiEvasioneOrdini()

      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then
        grRighe.Focus()
        Return
      End If
      dsBoll.Tables("CORPO").AcceptChanges()
      SettaCampiValuta()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grRighe_EditorKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grRighe.EditorKeyPress
    Try
      'memorizzo il nome della colonna che ha fatt ogenerare la nuova riga, per riproporla nel momento in cui dovrò inserirne un'altra
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If grvRighe.FocusedColumn.Name = "ec_codart" Or grvRighe.FocusedColumn.Name = "ec_matric" Then
          strLastColEdited = grvRighe.FocusedColumn.Name
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grRighe_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grRighe.EditorKeyDown
    Try
      If grvRighe.FocusedColumn.Name = "ec_codart" Then
        'memorizzo i caratteri effettivamente digitati nel cod. articolo: se ho inserito l'articolo con barcode 
        'di tipo EAN128 possono esserci caratteri di separatore di campo non presenti nella beforecolupdate e.proposedvalue
        oCleBoll.strEAN128Barcode += e.KeyCode.ToString & "§"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvRighe.NTSCellValueChanged
    Dim strColName As String = ""
    Dim strUbicaz As String = ""
    Dim dPrezzo As Decimal = 0
    Dim dScontV As Decimal = 0
    Try
      strColName = grvRighe.FocusedColumn.Name

      Select Case strColName.ToUpper
        Case "EC_CODART"
          CalcolaSceffAppl(NTSCInt(grvRighe.GetFocusedRowCellValue(ec_riga).ToString))

        Case "EC_SCONT1", "EC_SCONT2", "EC_SCONT3", "EC_SCONT4", "EC_SCONT5", "EC_SCONT6", _
             "EC_PREZZO", "EC_PREZVALC", "EC_PREZIVA", "EC_STASINO", "EC_CODIVA"
          CalcolaSceffAppl(NTSCInt(grvRighe.GetFocusedRowCellValue(ec_riga).ToString))

        Case "EC_VALORE"
          'ho cambiato il valore a mano: devo ricalcolare il prezzo all'indietro
          With grvRighe.NTSGetCurrentDataRow
            If NTSCDec(!ec_scontv) <> 0 And NTSCStr(oCleBoll.dttET.Rows(0)!et_scorpo) = "S" Then
              oCleBoll.Scorporo(NTSCDec(!ec_scontv), NTSCInt(!ec_codiva), dScontV, 0)
            Else
              dScontV = NTSCDec(!ec_scontv)
            End If

            If !ec_umprz.ToString <> "S" Then
              If NTSCDec(!ec_quant) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl(((NTSCDec(!ec_valore) / NTSCDec(!ec_quant) * NTSCDec(!ec_perqta)) / ((100 - NTSCDec(!ec_scont1)) / 100) / ((100 - NTSCDec(!ec_scont2)) / 100) / ((100 - NTSCDec(!ec_scont3)) / 100) / ((100 - NTSCDec(!ec_scont4)) / 100) / ((100 - NTSCDec(!ec_scont5)) / 100) / ((100 - NTSCDec(!ec_scont6)) / 100) / ((100 - NTSCDec(!ec_scontp)) / 100) - dScontV) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scont1)) / 100) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scont2)) / 100) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scopag)) / 100), oMenu.TrovaNdecSuPrzUn(0))
              End If
            Else
              If NTSCDec(!ec_colli) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl(((NTSCDec(!ec_valore) / NTSCDec(!ec_colli) * NTSCDec(!ec_perqta)) / ((100 - NTSCDec(!ec_scont1)) / 100) / ((100 - NTSCDec(!ec_scont2)) / 100) / ((100 - NTSCDec(!ec_scont3)) / 100) / ((100 - NTSCDec(!ec_scont4)) / 100) / ((100 - NTSCDec(!ec_scont5)) / 100) / ((100 - NTSCDec(!ec_scont6)) / 100) / ((100 - NTSCDec(!ec_scontp)) / 100) - dScontV) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scont1)) / 100) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scont2)) / 100) / ((100 - NTSCDec(oCleBoll.dttET.Rows(0)!et_scopag)) / 100), oMenu.TrovaNdecSuPrzUn(0))
              End If
            End If
            If NTSCDec(!ec_prezzo) <> dPrezzo Then !ec_prezzo = dPrezzo
            oCleBoll.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
          End With

        Case "XXO_VALLORDO"
          'ho cambiato il valore a mano: devo ricalcolare il prezzo all'indietro
          With grvRighe.NTSGetCurrentDataRow
            If !ec_umprz.ToString <> "S" Then
              If NTSCDec(!ec_quant) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl((NTSCDec(!xxo_vallordo) / NTSCDec(!ec_quant) * NTSCDec(!ec_perqta)), oMenu.TrovaNdecSuPrzUn(0))
              End If
            Else
              If NTSCDec(!ec_colli) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl((NTSCDec(!xxo_vallordo) / NTSCDec(!ec_colli) * NTSCDec(!ec_perqta)), oMenu.TrovaNdecSuPrzUn(0))
              End If
            End If
            If NTSCDec(!ec_prezzo) <> dPrezzo Then !ec_prezzo = dPrezzo
            oCleBoll.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
          End With
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvRighe.NTSBeforeRowUpdate
    Try
      '---------------------------------------
      'avviso l'entity di eseguire i test pre-cambio riga
      If tsBoll.Visible = False Then Return
      If bInDeleteRow Then Return 'passo di qui erroneamente se cancello una riga quando sono in abilita/disabilita sort/filter: non devo fare la recordsalva!!!!

      ProponiEvasioneOrdini()

      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then e.Allow = False 'rimango sulla riga su cui sono

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRighe.NTSFocusedRowChanged
    '-------------------------------------------------
    'carico le unità di misura al cambio di riga
    Dim strTmp As String = ""

    Try
      If dsBoll.Tables("CORPO") Is Nothing Then Return
      If dsBoll.Tables("CORPO").Rows.Count = 0 Then Return
      If grvRighe.GetFocusedRowCellValue(ec_riga) Is Nothing Then Return

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      If NTSCStr(grvRighe.GetFocusedRowCellValue(ec_riga)) = "" And grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grvRighe.LeftCoord = 0
        If ec_matric.Visible Then
          If strLastColEdited <> "" Then
            'propongo come cella quella che ha fatto creare l'ultima nuova riga, tra codart e barcode
            grvRighe.FocusedColumn = grvRighe.Columns(strLastColEdited)
          Else
            grvRighe.FocusedColumn = ec_matric
          End If
        Else
          grvRighe.FocusedColumn = ec_codart
        End If
        'For n = grvRighe.FocusedColumn.VisibleIndex To 1 Step -1
        '  SendKeys.Send("{LEFT}")
        'Next
        'If ec_riga.Visible Then SendKeys.Send("{RIGHT}")
      End If    'If grvRighe.GetFocusedRowCellValue(ec_riga").FormattedValue.ToString = "" Then

      If grvRighe.GetFocusedRowCellValue(ec_codart).ToString.Trim = "" Then
        edPreList.Text = "0"
        edUltCost.Text = "0"
        edSconto.Text = "0"
        edDispon.Text = "0"
        edDispNetta.Text = "0"
        edImpegnato.Text = "0"
        edOrdinato.Text = "0"
        GoTo FineFunzione
      End If

      '-------------------------------------------------
      'se l'articolo  gestito a taglia e colore blocco la colonna quantità
      'la blocco anche se è un carico da prod in modifica interno ed è setttata l'opzione di carichi prod da avmg
      If NTSCInt(grvRighe.GetFocusedRowCellValue(xxo_codtagl).ToString.Trim) <> 0 Then
        ec_quant.Enabled = False
      Else
        If oCleBoll.bNew = False And oCleBoll.bCarProdInAvlavp And _
           oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And oCleBoll.bTerzista = False Then
          ec_quant.Enabled = False
        Else
          If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) = "S" Or bCa2InCG Then
            ec_quant.Enabled = False
          Else
            GctlSetVisEnab(ec_quant, False)
          End If
        End If
      End If

      '-------------------------------------------------
      'rileggo la disponibilità e l'ultimo costo
      LeggiDisponibilitaArticolo(grvRighe.GetFocusedRowCellValue(ec_codart).ToString, _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_magaz).ToString), _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_fase).ToString), _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_commeca).ToString))

      '-------------------------------------------------
      'calcolo lo sconto effettivamente applicato e visualizzo il prezzo di listino
      CalcolaSceffAppl(NTSCInt(grvRighe.GetFocusedRowCellValue(ec_riga)))

      '-------------------------------------------------
      GrvRighe_RowColChange()

FineFunzione:

      AbilitaCellaCodart()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvRighe.NTSFocusedColumnChanged
    Try
      GrvRighe_RowColChange()
      If NTSCInt(edNumDoc.Text) = 0 Then Return 'da errore nella form_load ...

      '--------------------------------------
      'Se è impostata l'opzione di registro \BSVEBOLL\OPZIONI\ConfermaRigaBarcode a -1 (True)
      'dopo l'inserimento del BARCODE nella colonna relativa il programma scatena una
      'FRECCIA-GIU' e si posiziona sulla colonna BARCODE della nuova riga
      If Not e Is Nothing And oCleBoll.bConfermaRigaBarcode = True Then
        If e.PrevFocusedColumn.Name = "ec_matric" And oCleBoll.bConfermaRigaBarcode = True And _
           e.PrevFocusedColumn.AbsoluteIndex = e.FocusedColumn.AbsoluteIndex - 1 Then
          If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, "ec_matric").ToString.Trim <> "" And grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
              'altrimenti non va su SBC
              'NTSSendKeys.SendWait(0, "{Down}")
              'grvRighe.FocusedColumn = ec_matric
              grvRighe.NTSNuovo("ec_matric")
            End If
          End If
        End If
      End If

      AbilitaCellaCodart()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function AbilitaCellaCodart() As Boolean
    Try
      '--------------------------------------
      'bug di lettura barcode: se sono su una nuova riga ed il cursore non è dentro alla cella 
      '(quindi cella con focus ma fursore non lampeggiante)
      'se sparo sul barcode, a volte inferte il primo con secondo carattere del barcode
      'SU CUBE NON SERVE: IL PROBLEMA NON SI VERIFICA
      If oCleBoll Is Nothing Then Return True
      If bInAbilitaCellaEnter Then Return True
      If oCleBoll.bCodartDaBarcode Then
        If Not grvRighe.FocusedColumn Is Nothing AndAlso grvRighe.FocusedColumn.Name = "ec_codart" _
           AndAlso ec_codart.Enabled AndAlso grvRighe.Enabled Then
          If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, "ec_codart")) = "" Then
            bInAbilitaCellaEnter = True
            grvRighe.ShowEditorByMouse()
            If Not grvRighe.ActiveEditor Is Nothing Then
              'grvRighe.ActiveEditor.SelectAll() 'non va!!!!
              NTSSendKeys.Send(0, "{Down}") 'faccio apparire il cursore del mouse
            End If
          End If
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bInAbilitaCellaEnter = False
    End Try
  End Function

  Public Overridable Function GrvRighe_RowColChange() As Boolean
    Try

      If oCleBoll Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        ec_unmis.Enabled = False
      Else
        GctlSetVisEnab(ec_unmis, False)
      End If

      '--------------------------------------
      'se posso rendo editabile il campo 'numero di riga'
      AbilitaDisabilitaRigaNum()

      '--------------------------------------
      'Disabilita la modifica dell'unità di misura se riga proveniente da un ordine
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        ec_salcon.Enabled = False
        ec_npsalcon.Enabled = False
      Else
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum) > 0 Or NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum) > 0 Then
          If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
          ec_unmis.Enabled = False
        Else
          GctlSetVisEnab(ec_unmis, False)
        End If
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum) = 0 Then
          ec_salcon.Enabled = False
        Else
          GctlSetVisEnab(ec_salcon, False)
        End If
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum) = 0 Then
          ec_npsalcon.Enabled = False
        Else
          GctlSetVisEnab(ec_npsalcon, False)
        End If
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleBoll.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
          'sarà BN__CHIL ha gestire ec_unmis.NTSComboValueOk
        End If
      End If    'If Not ec_unmis.ColumnEdit Is Nothing Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub xxo_lottox_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles xxo_lottox.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183074695294, "Posizionarsi prima su una riga con codice articolo impostato"))
        Return
      End If
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776115002254000, "Indicare prima il codice articolo"))
        Return
      End If

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_lotto).PadLeft(9, "0"c) & ";"
      Else
        oPar.strParam = "NUOV;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & "000000000" & ";"
      End If

      oTmp.Text = NTSCStr(grvRighe.NTSGetCurrentDataRow!xxo_lottox)
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, bNuovo, oPar)

      If oPar.strParam.Trim <> "" Then
        grvRighe.NTSGetCurrentDataRow!xxo_lottox = oPar.strParam
        grvRighe.RefreshEditor(True)
      End If

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Eventi TabStrip"
  Public Overridable Sub tsBoll_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsBoll.SelectedPageChanged
    Try
      If oCleBoll.bInUnload Then Return
      If bInCambioTab Then Return
      Me.Cursor = Cursors.WaitCursor

      '------------------------------
      'spostato dalla gririghe_enter perchè se con delle righe nel corpo tornavo in testata ed azzeravo il magazzino
      '(azzerandolo ahce sulle righe) poi passavo al corpo si entrava in un loop infinito
      'se aggiungo il controllo che la pagina uscente è la prima, in alcuni casi non funziona
      'stessa cosa se impsto come pagina su cui tornare quella che ho appena lasciato!!!!
      If Not e.PrevPage.Equals(tsBoll.TabPages(1)) Then
        If pnCorpo.Visible Then
          If Not oCleBoll.OkTestata Then
            bInCambioTab = True
            tsBoll.SelectedTabPageIndex = 0
            bInCambioTab = False
            Return
          End If
        End If
      End If

      '------------------------------
      'ricalcolo i totali del documento quando esco dal corpo
      If e.PrevPage.Equals(tsBoll.TabPages(1)) Then
        If NTSCInt(Me.edNumDoc.Text) <> 0 Then
          If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then
            bInCambioTab = True
            tsBoll.SelectedTabPageIndex = 1
            bInCambioTab = False
            Return
          End If
          Me.NTSFormClearDataBinding(Me)
          oCleBoll.CalcolaTotali()
          Bindcontrols()
        End If
      End If

      If e.Page.Equals(tsBoll.TabPages(1)) Then
        GctlSetVisEnab(tlbStampaEtichetteRiga, False)
        If cbTipoDoc.SelectedValue.ToString <> "I" Then
          GctlSetVisEnab(tlbMnuRecord, False)
          GctlSetVisEnab(tlbRecordRipristina, False)
        End If
        If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) <> "S" And bCa2InCG = False AndAlso Not oCleBoll.bInEvasioneDaFattFigurativa Then
          GctlSetVisEnab(tlbRecordCancella, False)
        End If

        If oCleBoll.bCodartDaBarcode Then
          If edBarcodeIn.Visible Then
            edBarcodeIn.Focus()
          Else
            Application.DoEvents()
            grvRighe.ShowEditor()
            grvRighe.ShowEditorByMouse()
          End If
        End If
      Else
        tlbStampaEtichetteRiga.Enabled = False
        tlbMnuRecord.Enabled = False
        tlbRecordCancella.Enabled = False
        tlbRecordRipristina.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Voci di Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    'Dim bRicaricaGctl As Boolean = False
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

      '----------------------------------------------------
      'se avevo dei recent tra i documenti da aprire, li svuoto
      bNoResetDttOpens = False
      dttOpens.Clear()

      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776115070010000, "L'utente è abilitato a creare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        edSerieDoc.Text = strSoloSerie.ToUpper
        edNumDoc.Text = "0"
      End If

      '----------------------------------------------------
      'assegno il numero del documento
      lNumDocTmp = NTSCInt(edNumDoc.Text)
      strTipoProg = cbTipoDoc.SelectedValue
      Select Case cbTipoDoc.SelectedValue
        Case "A", "D", "E", "N", "£" : strTipoProg = "A"
        Case "B" : strTipoProg = "B"
        Case "C" : strTipoProg = "C"
        Case "M" : strTipoProg = "M"
        Case "Z" : strTipoProg = "Z"
        Case "L", "K", "J", "(" : strTipoProg = "L"
        Case "W" : strTipoProg = "W"
        Case "T" : strTipoProg = "T"
        Case "U" : strTipoProg = "U"
        Case "F", "I" : strTipoProg = "F"
        Case "S" : strTipoProg = "S"
      End Select
      lNumDocTmp = oCleBoll.LegNuma(strTipoProg, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text))
      If lNumDocTmp = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115000000, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento"))
        Return
      End If
      If edNumDoc.Text = "0" Then edNumDoc.Text = lNumDocTmp.ToString

      '----------------------------------------------------
      'creo il nuovo record 
      bInLoadDoc = True
      Me.Cursor = Cursors.WaitCursor
      'If cbTipoDoc.SelectedValue.ToString <> oCleBoll.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      NTSFormClearDataBinding(Me)

      '----------------------------------------
      'se l'ambiente non era stato caricato, lo carico ora
      If dsBoll.Tables.Count < 4 Then
        If Not oCleBoll.ApriDoc(DittaCorrente, True, "B", 0, " ", 0, dsBoll) Then Return
      End If

      oCleBoll.dtDataNewDoc = CDate(Now.ToShortDateString)

      If oCleBoll.NuovoDocumento(DittaCorrente, cbTipoDoc.SelectedValue.ToString, NTSCInt(edAnnoDoc.Text), _
                                 edSerieDoc.Text, NTSCInt(edNumDoc.Text)) = False Then Return
      dsBoll.Tables("TESTA").AcceptChanges()

      dcBoll.DataSource = dsBoll.Tables("TESTA")
      dcBollc.DataSource = dsBoll.Tables("TESTAC")
      Bindcontrols()

      '----------------------------------------------------
      'ricollego la griglia al datatable di movord
      dcBollRighe.DataSource = dsBoll.Tables("CORPO")
      grRighe.DataSource = dcBollRighe
      bInLoadDoc = False

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) 'bRicaricaGctl)

      '----------------------------------------------------
      'menu non abilitati quando faccio 'nuovo'
      tlbNuovoDocDaPrec.Enabled = False
      tlbCancella.Enabled = False

      '----------------------------------------------------
      'disabilito i campi in valuta e correggo la visibilit della colonna PREZZO CON IVA
      SettaCampiValuta()
      ckEt_scorpo_CheckedChanged(Me, Nothing)

      '----------------------------------------------
      'Il CheckBox "Proforma" è abilitato solo se si è in creazione di un documento
      'di tipo "Fattura immediata emessa"
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "A" Then
        GctlSetVisEnab(ckEt_proforma, False)
      Else
        ckEt_proforma.Enabled = False
        ckEt_proforma.Checked = False
      End If

      'di solito si inseriscono documenti nella data odierna ...
      edEt_conto.Focus()

      If lContoProposto <> 0 Then edEt_conto.NTSTextDB = NTSCStr(lContoProposto)

      '----------------------------------------------
      'Se il conto lo ha validato cerca di andare sul corpo
      If NTSCInt(edEt_conto.Text) > 0 AndAlso cbTipoDoc.SelectedValue <> "T" Then
        tsBoll.SelectedTabPageIndex = 1
        grRighe.Focus()
      End If

      '----------------------------------------------
      'con gestione di avmg, se avevo aperto un carico da prod la colona colli era stata bloccata:
      'su nuovi documenti non lo deve essere, ma la personalizzaz. della griglia non viene fatta se non cambio il tipo documento
      If cbTipoDoc.SelectedValue = "T" Then
        GctlSetVisEnab(ec_colli, False)
        GctlSetVisEnab(ec_quant, False)
      End If

      ColoraCampoAbiCab()
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
    Dim strPwd As String = ""
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

      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776115093254000, "L'utente è abilitato a modificare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        Return
      End If

      '----------------------------------------------------
      'testo il numero del documento
      If NTSCInt(edNumDoc.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115312500, "Prima di aprire un nuovo documento è necessario indicare il numero del documento"))
        Return
      End If

      '----------------------------------------------------
      'se avevo dei recent tra i documenti da aprire, verifico se il documento che sto aprendo è tra quelli: se non lo è pulisco il recent
      If bDaTastiNavigazione = False Then
        tlbApriDocPrec.Enabled = False
        tlbApriDocSucc.Enabled = False
      End If
      If dttOpens.Rows.Count > 0 Then
        If dttOpens.Select("tm_tipork = " & CStrSQL(cbTipoDoc.SelectedValue) & _
                          " AND tm_anno = " & NTSCInt(edAnnoDoc.Text).ToString & _
                          " AND tm_serie = " & CStrSQL(edSerieDoc.Text) & _
                          " AND tm_numdoc = " & NTSCInt(edNumDoc.Text).ToString).Length = 0 Then
          dttOpens.Clear()
        Else
          If dttOpens.Rows.Count > 1 Then
            If bDaTastiNavigazione = False Then GctlSetVisEnab(tlbApriDocSucc, False)
          End If
        End If
      End If

      '----------------------------------------------------
      'apro il documento
      Me.Cursor = Cursors.WaitCursor
      bInLoadDoc = True
      'If cbTipoDoc.SelectedValue.ToString <> oCleBoll.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      NTSFormClearDataBinding(Me)
      If oCleBoll.ApriDoc(DittaCorrente, False, cbTipoDoc.SelectedValue.ToString, _
                                 NTSCInt(edAnnoDoc.Text), edSerieDoc.Text, NTSCInt(edNumDoc.Text), _
                                 dsBoll) = False Then Return
      dsBoll.Tables("TESTA").AcceptChanges()
      If dsBoll.Tables("TESTA").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115468750, "Non è stato trovato nessun documento con le caratteristiche richieste"))
        bInLoadDoc = False
        Return
      Else
        dcBoll.DataSource = dsBoll.Tables("TESTA")
        dcBoll.MoveFirst()
        dcBollc.DataSource = dsBoll.Tables("TESTAC")
        dcBollc.MoveFirst()
        Bindcontrols()

        dcBollRighe.DataSource = dsBoll.Tables("CORPO")
        grRighe.DataSource = dcBollRighe
      End If

      '----------------------------------------------------
      'CRM
      If oCleBoll.bRipristinaDocumento = True Then
        Ripristina()
        Return
      End If

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) 'bRicaricaGctl)
      '--------------------------------------------------------------------------------------------------------------
      '--- Dopo la chiamata alla ApriDoc del BE, testo i flag che, eventualmente4, disabilitano alcune funzionalità
      '--------------------------------------------------------------------------------------------------------------
      If oCleBoll.bDocNonModificabile = True Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
      End If
      If oCleBoll.bNuovoDocDaPrec = False Then tlbNuovoDocDaPrec.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      'Se è attivo il modulo CRM, il conto di testata è un Cliente e non è permessa la
      'modifica in CRMOP, disabilita i pulsanti e le voci di menu relative a:
      'Aggiorna
      'Cancella
      If oCleBoll.bModuloCRM And oCleBoll.bIsCRMUser And oCleBoll.bAccmod = False Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
      End If

      '----------------------------------------------------
      'Avverte che sono già state estratte le provvigioni agli agenti
      If oCleBoll.dttET.Rows(0)!et_flprov.ToString <> "N" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128655248081093750, "Attenzione! Sono già state estratte le provvigioni agli agenti per il documento aperto."))
      End If

      '----------------------------------------------------
      'abilito/disabilito i campi in valuta
      SettaCampiValuta()
      If dsBoll.Tables("CORPO").Rows.Count > 0 Then  'altrimenti non disabilita i campi in testata
        ckEt_scorpo.Enabled = False
        edEt_valuta.Enabled = False
      Else
        If oCleBoll.bModPM And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
          edEt_valuta.Enabled = False
        End If
      End If
      ckEt_scorpo_CheckedChanged(Me, Nothing)  'altrimenti non abilita la colonna 'prezzo con iva' in griglia

      bInLoadDoc = False
      '--------------------------------------------------------------------------------------------------------------
      'ricalcolo i totali del documento
      Me.NTSFormClearDataBinding(Me)
      oCleBoll.CalcolaTotali()
      Bindcontrols()

      '-----------------------------------------------
      If oCleBoll.bCarProdInAvlavp And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
        'se i carichi da prod sono stati fatti da bsdbavmg o bsdbavex non posso cambiare le quantità
        'per non creare differenze tra veboll e avmg
        ec_colli.Enabled = False
        ec_quant.Enabled = False
        ec_unmis.Enabled = False
      Else
        GctlSetVisEnab(ec_colli, False)
        GctlSetVisEnab(ec_quant, False)
        GctlSetVisEnab(ec_unmis, False)
      End If

      '----------------------------------------------------
      If oCleBoll.strVisNoteContoApertura = "S" Then
        Dim frmMsgb As FRM__MSG1 = Nothing
        frmMsgb = CType(NTSNewFormModal("FRM__MSG1"), FRM__MSG1)
        frmMsgb.Init(oMenu, Nothing, DittaCorrente)
        frmMsgb.cmdZoom.Visible = False
        frmMsgb.cmdAnnulla.Visible = False
        If NTSCStr(oCleBoll.dttEt_conto.Rows(0)!an_note2).Trim <> "" Then

          frmMsgb.edMess.Text = NTSCStr(oCleBoll.dttEt_conto.Rows(0)!an_note2).Trim
          frmMsgb.Text = oApp.Tr(Me, 128655279197500000, "NOTE RELATIVE AL CONTO ") & oCleBoll.lContoCF.ToString
          frmMsgb.ShowDialog()
          frmMsgb.Dispose()
          frmMsgb = Nothing
        End If

        If NTSCInt(oCleBoll.dttET.Rows(0)!et_coddest) <> 0 Then
          oMenu.ValCodiceDb(NTSCInt(oCleBoll.dttET.Rows(0)!et_coddest).ToString, DittaCorrente, "DESTDIV", "N", "", dttTmp, oCleBoll.lContoCF.ToString)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!dd_note).Trim <> "" Then
              frmMsgb = CType(NTSNewFormModal("FRM__MSG1"), FRM__MSG1)
              frmMsgb.Init(oMenu, Nothing, DittaCorrente)
              frmMsgb.cmdZoom.Visible = False
              frmMsgb.cmdAnnulla.Visible = False
              frmMsgb.edMess.Text = NTSCStr(dttTmp.Rows(0)!dd_note).Trim
              frmMsgb.Text = oApp.Tr(Me, 128655279326562500, "NOTE RELATIVE ALLA DESTINAZIONE |" & NTSCInt(oCleBoll.dttET.Rows(0)!et_coddest).ToString & "| DEL CONTO ") & oCleBoll.lContoCF.ToString
              frmMsgb.ShowDialog()
              frmMsgb.Dispose()
              frmMsgb = Nothing
            End If
          End If
        End If
      End If

      '----------------------------------------------------
      'Il CheckBox "Proforma" è abilitato solo se si è in inserimento di un documento
      'di tipo "Fattura immediata emessa"
      ckEt_proforma.Enabled = False

      '----------------------------------------------------
      'blocchi se ho aperto un documento proveniente da retail nuovo o CA DC
      If oCleBoll.bModGPV Or CBool(oCleBoll.lModuliSupDittaDitt And bsModSupCAE) Then SetBlocchiRetail(False)

      grvRighe.LeftCoord = 0
      If ec_matric.Visible Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      oCleBoll.bHasChangesET = False

      ColoraCampoAbiCab()
    Catch ex As Exception
      bInLoadDoc = False
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbApriDocPrec_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriDocPrec.ItemClick
    'se quando ho fatto zoom per selezionare il documento da aprire ho selez più documenti, ora posso passare a quello successivo in ordine di tipork, anno, serie, numdoc
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(-1, False) Then Return
      RipristinaInterfaccia()
      '--------------------------------------------------------------------------------------------------------------
      'dai recent posso selezionare solo serie e numero documento. se cambio anno o tipork i recent si svuotano

      If dttOpens.Rows.Count > 0 Then
        For i = 0 To dttOpens.Rows.Count - 1
          If NTSCStr(dttOpens.Rows(i)!tm_tipork) = strLastTipork And _
             NTSCInt(dttOpens.Rows(i)!tm_anno) = nLastAnno And _
             NTSCStr(dttOpens.Rows(i)!tm_serie).ToUpper = strLastSerie.ToUpper And _
             NTSCInt(dttOpens.Rows(i)!tm_numdoc) = lLastNumdoc Then
            bOk = True
            Exit For
          End If
        Next

        If bOk = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129496853554558202, "Tra i recent dell'ultima ricerca non è stato trovato l'ultimo documento aperto. I recent verranno svuotati"))
          dttOpens.Clear()
          Return
        Else
          If i = 0 Then
            '
          Else
            i -= 1
          End If
        End If

        GctlSetVisEnab(tlbApriDocSucc, False)
        If i = 0 Then
          tlbApriDocPrec.Enabled = False
        Else
          GctlSetVisEnab(tlbApriDocPrec, False)
        End If

        cbTipoDoc.SelectedValue = dttOpens.Rows(i)!tm_tipork.ToString
        edAnnoDoc.Text = dttOpens.Rows(i)!tm_anno.ToString
        edSerieDoc.Text = dttOpens.Rows(i)!tm_serie.ToString
        edNumDoc.Text = dttOpens.Rows(i)!tm_numdoc.ToString
        bDaTastiNavigazione = True
        tlbApri_ItemClick(tlbApri, Nothing)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 129496853585437109, "Non sono presenti documenti da aprire tra i recent dell'ultima ricerca"))
        Return
      End If

      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bDaTastiNavigazione = False
    End Try
  End Sub
  Public Overridable Sub tlbApriDocSucc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriDocSucc.ItemClick
    'se quando ho fatto zoom per selezionare il documento da aprire ho selez più documenti, ora posso passare a quello precedente in ordine di tipork, anno, serie, numdoc
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(-1, False) Then Return
      RipristinaInterfaccia()
      '--------------------------------------------------------------------------------------------------------------
      'dai recent posso selezionare solo serie e numero documento. se cambio anno o tipork i recent si svuotano

      If dttOpens.Rows.Count > 0 Then
        For i = 0 To dttOpens.Rows.Count - 1
          If NTSCStr(dttOpens.Rows(i)!tm_tipork) = strLastTipork And _
             NTSCInt(dttOpens.Rows(i)!tm_anno) = nLastAnno And _
             NTSCStr(dttOpens.Rows(i)!tm_serie).ToUpper = strLastSerie.ToUpper And _
             NTSCInt(dttOpens.Rows(i)!tm_numdoc) = lLastNumdoc Then
            bOk = True
            Exit For
          End If
        Next

        If bOk = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129496847590505468, "Tra i recent dell'ultima ricerca non è stato trovato l'ultimo documento aperto. I recent verranno svuotati"))
          dttOpens.Clear()
          Return
        Else
          If i = dttOpens.Rows.Count - 1 Then
            '
          Else
            i += 1
          End If
        End If

        GctlSetVisEnab(tlbApriDocPrec, False)
        If i = dttOpens.Rows.Count - 1 Then
          tlbApriDocSucc.Enabled = False
        Else
          GctlSetVisEnab(tlbApriDocSucc, False)
        End If

        cbTipoDoc.SelectedValue = dttOpens.Rows(i)!tm_tipork.ToString
        edAnnoDoc.Text = dttOpens.Rows(i)!tm_anno.ToString
        edSerieDoc.Text = dttOpens.Rows(i)!tm_serie.ToString
        edNumDoc.Text = dttOpens.Rows(i)!tm_numdoc.ToString
        bDaTastiNavigazione = True
        tlbApri_ItemClick(tlbApri, Nothing)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 129496850188542577, "Non sono presenti documenti da aprire tra i recent dell'ultima ricerca"))
        Return
      End If

      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bDaTastiNavigazione = False
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Dim bNewTmp As Boolean = False
    Try
      bNewTmp = oCleBoll.bNew
      If Not Salva(-1, False) Then Return

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bCreaCarProd Then
        CreaCaricoProduzione()
      Else
        If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      End If

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
      If oCleBoll.ChiamaPnfa Then
        If ckEt_flprov.Checked Then
          strMsg += vbCrLf & oApp.Tr(Me, 130415988402832301, "Dovrà essere rieseguita l'estrazione delle provvigioni per rimuovere dall'archivio i dati di questo documento")
        End If
      End If

      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(strMsg)
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleBoll.bInUnload = True
          Me.Cursor = Cursors.WaitCursor

          'annullo la riga in corso di inserimento
          If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
              If Not oCleBoll.RecordRipristina(dcBollRighe.Position, dcBollRighe.Filter) Then Return
            End If
          End If

          If Not oCleBoll.SalvaDocumento("D") Then Return
          '----------------------------------------------------------------------------------------------------------
          If dttOpens.Rows.Count > 1 Then
            Dim bOk As Boolean = False
            Dim bRiapriDocSuSalvaTmp As Integer = oCleBoll.bRiapriDocSuSalva
            Dim i As Integer = 0
            Dim dtrRow() As DataRow = Nothing
            For i = 0 To (dttOpens.Rows.Count - 1)
              With dttOpens.Rows(i)
                If NTSCStr(!tm_tipork) = NTSCStr(cbTipoDoc.SelectedValue) And _
                   NTSCInt(!tm_anno) = NTSCInt(edAnnoDoc.Text) And _
                   NTSCStr(!tm_serie).ToUpper = edSerieDoc.Text And _
                   NTSCInt(!tm_numdoc) = NTSCInt(edNumDoc.Text) Then
                  bOk = True
                  Exit For
                End If
              End With
            Next
            If i > 0 Then i -= 1
            dtrRow = dttOpens.Select("tm_tipork = " & CStrSQL(cbTipoDoc.SelectedValue) & _
                                     " AND tm_anno = " & NTSCInt(edAnnoDoc.Text) & _
                                     " AND tm_serie = " & CStrSQL(edSerieDoc.Text) & _
                                     " AND tm_numdoc = " & NTSCInt(edNumDoc.Text))
            If dtrRow.Length > 0 Then dtrRow(0).Delete()
            dttOpens.AcceptChanges()
            oCleBoll.bInUnload = False
            oCleBoll.bRiapriDocSuSalva = -1
            cbTipoDoc.SelectedValue = dttOpens.Rows(i)!tm_tipork.ToString
            edAnnoDoc.Text = dttOpens.Rows(i)!tm_anno.ToString
            edSerieDoc.Text = dttOpens.Rows(i)!tm_serie.ToString
            edNumDoc.Text = dttOpens.Rows(i)!tm_numdoc.ToString
            tlbApri_ItemClick(tlbApri, Nothing)
            oCleBoll.bRiapriDocSuSalva = bRiapriDocSuSalvaTmp
            oCleBoll.RipristinaDocumento()
            Return
          End If
          '----------------------------------------------------------------------------------------------------------
          oCleBoll.bInUnload = False
          RipristinaInterfaccia()
          Return
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleBoll.bInUnload = False
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
      If edEt_conto.Focused Then
        SetFastZoom(edEt_conto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edEt_conto.Text
        oParam.bVisGriglia = True
        oParam.bTipoProposto = True
        oParam.nMastro = 0
        If oCleBoll.IsDocumentoEmesso(dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString(), False) Then
          oParam.strTipo = "C"
        Else
          oParam.strTipo = "F"
        End If
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edEt_conto.Text Then edEt_conto.NTSTextDB = NTSZOOM.strIn

      ElseIf edEt_coddest.Focused Or edEt_coddest2.Focused Then

        If edEt_coddest.Focused Then
          SetFastZoom(edEt_coddest.Text, oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = edEt_coddest.Text
        Else
          SetFastZoom(edEt_coddest2.Text, oParam)
          NTSZOOM.strIn = edEt_coddest2.Text
        End If

        oParam.lContoCF = oCleBoll.lContoCF   'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)

        If edEt_coddest.Focused Then
          If NTSZOOM.strIn <> edEt_coddest.Text Then edEt_coddest.NTSTextDB = NTSZOOM.strIn
        Else
          If NTSZOOM.strIn <> edEt_coddest2.Text Then edEt_coddest2.NTSTextDB = NTSZOOM.strIn
        End If

      ElseIf edNumDoc.Focused Or cbTipoDoc.Focused Then
        '------------------------------------
        'zoom documenti 
        If tsBoll.Visible = True Then Return 'esco se sono dentro ad un documento

        oParam.nTipologia = 1   '= bsveboll
        oParam.strTipo = cbTipoDoc.SelectedValue.ToString
        oParam.strAlfpar = edSerieDoc.Text
        oParam.nAnno = NTSCInt(edAnnoDoc.Text)
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMDOCUMENTI", DittaCorrente, oParam)
        If NTSZOOM.strIn = "" Then Return

        If Not oParam.oParam Is Nothing Then
          'la selezione documenti mi ha passato un elenco di documenti da aprire: memorizzo l'elenco
          dttOpens = CType(oParam.oParam, DataTable).Copy
          CType(oParam.oParam, DataTable).Clear()
          oParam.oParam = Nothing
        End If
        If dttOpens.Rows.Count > 1 Then
          GctlSetVisEnab(tlbApriDocPrec, False)
          GctlSetVisEnab(tlbApriDocSucc, False)

          cbTipoDoc.SelectedValue = dttOpens.Rows(0)!tm_tipork.ToString
          edAnnoDoc.Text = dttOpens.Rows(0)!tm_anno.ToString
          edSerieDoc.Text = dttOpens.Rows(0)!tm_serie.ToString
          edNumDoc.Text = dttOpens.Rows(0)!tm_numdoc.ToString
        Else
          dttOpens.Clear()
          tlbApriDocPrec.Enabled = False
          tlbApriDocSucc.Enabled = False

          edSerieDoc.Text = oParam.strAlfpar
          edNumDoc.Text = oParam.lNumpar.ToString
        End If

        tlbApri_ItemClick(Me, Nothing)

      ElseIf edEt_cab.Focused Then
        '------------------------------------
        'zoom cab
        SetFastZoom(edEt_cab.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = edEt_abi.Text   'passo il codice abi
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edEt_cab.Text Then edEt_cab.NTSTextDB = NTSZOOM.strIn
      ElseIf edSerieDoc.Focused Then
        '------------------------------------
        'zoom serie
        If NTSCInt(edAnnoDoc.Text) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130512728416683717, "Indicare prima l'anno"))
          Return
        End If
        SetFastZoom(edSerieDoc.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strDescr = NTSCStr(cbTipoDoc.SelectedValue)
        oParam.lContoCF = NTSCInt(edAnnoDoc.Text)
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMSERIE", DittaCorrente, oParam)
        If oParam.strOut <> "" Then edSerieDoc.NTSTextDB = oParam.strOut
      Else
        '------------------------------------
        'zoom standard
        ctrlTmp = NTSFindControlForZoom()
        If ctrlTmp Is Nothing Then Return
        If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
          SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)    'abilito la gestione dello zoom veloce

          If grvRighe.FocusedColumn.Equals(ec_codart) Then
            '------------------------------------
            'colonna particolare: codice articolo
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.nMagaz = NTSCInt(edEt_magaz.Text)
            oParam.nListino = NTSCInt(edEt_listino.Text)
            oParam.lContoCF = oCleBoll.lContoCF
            oParam.bTipoProposto = False       'abilito la possibilità di selezionare + articoli
            NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
            If NTSZOOM.strIn = "*" Then
              'zoom multiselezione
              If Not oParam.oParam Is Nothing Then
                If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
                  'il primo articolo selezionato lo metto nella riga su cui sono, gli altri li accodo
                  If NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart) <> NTSCStr(grvRighe.GetFocusedValue) Then
                    grvRighe.SetFocusedValue(NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart))
                    If CType(oParam.oParam, DataTable).Rows.Count > 1 Then
                      If NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) = 0 Then
                        grvRighe.NTSGetCurrentDataRow!ec_quant = 1
                      End If
                    End If
                  End If
                  CType(oParam.oParam, DataTable).Rows(0).Delete()
                  CType(oParam.oParam, DataTable).AcceptChanges()
                  If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
                    oCleBoll.bInInsertArticoDaZoom = True
                    If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
                    For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                      oCleBoll.dttEC.Rows.Add(oCleBoll.dttEC.NewRow)
                      With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
                        'forzo la MovordOnAddNewRow
                        !codditt = "."
                        !codditt = DittaCorrente
                        !ec_codart = dtrT!codart.ToString
                        'imposto sempre la qta = 1 (altrimenti in carichi da prod e/o articoli conai quando in seguito al cambio della qta dovrò riproporzionalizzare gli scarichi/dettaglio conai non lavorerebbe correttamente)
                        'solo se non è un artoclo TCO, altrimenti non compilerebbe correttamente il dettaglio TCO
                        If NTSCInt(!xxo_codtagl) = 0 Then
                          !ec_quant = 1
                        End If
                      End With
                      If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
                        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
                      End If
                    Next
                    oCleBoll.bInInsertArticoDaZoom = False
                  End If
                  CType(oParam.oParam, DataTable).Clear()

                  grvRighe_NTSFocusedRowChanged(Me, Nothing)
                End If
              End If
            Else
              If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
            End If

          ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
            '------------------------------------
            'zoom fasi articoli
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
            NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(ec_subcommeca) Then
            '------------------------------------
            'zoom subcommessa
            If NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca)) = 0 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128776115180102000, "Indicare prima il codice commessa"))
              Return
            End If
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.lCommessa = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca))   'passo il codice della commessa
            NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(xxo_lottox) Then
            '------------------------------------
            'zoom lotti
            If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128776114956390000, "Indicare prima il codice articolo"))
              Return
            End If
            'oCleBoll.GetMemDttArti(NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)))
            'If NTSCStr(oCleBoll.dttArti.Rows(0)!ar_geslotti) = "N" Then
            '  oApp.MsgBoxInfo(oApp.Tr(Me, 130652685584033091, "Zoom disponibile solo se l'articolo è gestito a lotti"))
            '  Return
            'End If
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
            'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
            oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
            'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
            NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(ec_ubicaz) Then
            '------------------------------------
            'zoom ubicazioni
            If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128776114971210000, "Indicare prima il codice articolo"))
              Return
            End If
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
            oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
            oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
            'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
            NTSZOOM.ZoomStrIn("ZOOMUBICAZ", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(ec_codclie) Then
            '------------------------------------
            'zoom cliente di griglia
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.bVisGriglia = True
            oParam.bTipoProposto = True
            oParam.nMastro = 0
            oParam.strTipo = "C"
            NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(ec_prezzo) Then
            '------------------------------------
            'zoom listini
            If ckEt_scorpo.Checked = False And NTSCInt(edEt_valuta.Text) = 0 Then ApriVisualizzaListini(0)

          ElseIf grvRighe.FocusedColumn.Equals(ec_preziva) Then
            '------------------------------------
            'zoom listini
            If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And Not oCleBoll.bTerzista Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128569600217187500, "Prezzo IVA inclusa non modificabile per le righe di caricho da produzione interno."))
              Return
            Else
              If ckEt_scorpo.Checked Then ApriVisualizzaListini(1)
            End If

          ElseIf grvRighe.FocusedColumn.Equals(ec_prezvalc) Then
            '------------------------------------
            'zoom listini
            If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And Not oCleBoll.bTerzista Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129055183188166190, "Prezzo in valuta non modificabile per le righe di caricho da produzione interno."))
              Return
            Else
              If NTSCInt(edEt_valuta.Text) <> 0 Then ApriVisualizzaListini(2)
            End If

          ElseIf grvRighe.FocusedColumn.Equals(ec_pmtaskid) Then
            '------------------------------------
            'zoom Project management task ID
            SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)

            'parametri per lo zoom
            Dim lInstid As Integer = 0
            Dim lCodCommessa As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_commeca)
            Dim bAttNonCompletate As Boolean = True
            Dim bSoloProgDate As Boolean = False
            '
            Dim bSoloUltimoLiv As Boolean = True
            Dim strTipoTask As String = "1345678BC"
            Dim bSoloRilasciati As Boolean = oCleBoll.bPMSoloTaskRilasciatiMG
            Dim strCodart As String = IIf(NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) = "D", "", NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)).ToString
            Dim lCodConto As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_contocontr)
            Dim lOperaio As Integer = 0
            Dim nCodlavo As Integer = 0
            Dim nCodCentroLav As Integer = 0
            Dim nCodCentroCa As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codcena)
            Dim lCodForni As Integer = NTSCInt(edEt_conto.Text)
            Dim nNLayout As Integer = 5
            Dim lInstidPrevent As Integer = 0

            oParam.nTipologia = lInstid
            oParam.lCommessa = lCodCommessa
            oParam.bStanziamenti = bAttNonCompletate
            oParam.bLiv2 = bSoloProgDate
            If oCleBoll.bModPM Then
              oParam.strTipo = NTSCStr(bSoloUltimoLiv) & ";" & strTipoTask & ";" & NTSCStr(bSoloRilasciati) & ";" & _
                               strCodart & ";" & NTSCStr(lCodConto) & ";" & NTSCStr(lOperaio) & ";" & NTSCStr(nCodlavo) & ";" & _
                               NTSCStr(nCodCentroLav) & ";" & NTSCStr(nCodCentroCa) & ";" & NTSCStr(lCodForni) & ";" & _
                               NTSCStr(nNLayout) & ";" & NTSCStr(lInstidPrevent)
            Else
              oParam.strTipo = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_subcommeca)
            End If
            NTSZOOM.ZoomStrIn("ZOOMTASK", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
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
      End If

      If edEt_codese.Focused Or edEt_controp.Focused Or edEt_magaz.Focused Or edEt_magaz2.Focused Or _
        (grRighe.ContainsFocus And (grvRighe.FocusedColumn.Equals(ec_magaz) Or grvRighe.FocusedColumn.Equals(ec_magaz2) Or grvRighe.FocusedColumn.Equals(ec_controp) Or grvRighe.FocusedColumn.Equals(ec_codiva))) Then
        oCleBoll.CaricaTabelleCache()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleBoll.bInInsertArticoDaZoom = False
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim dtrDeleted As DataRow = Nothing
    Dim nPos As Integer = 0
    Try
      bInDeleteRow = True
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183334459246, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.CorpoTestPreCancella(dcBollRighe.Position) Then Return
      nPos = dcBollRighe.Position
      If Not grvRighe.NTSDeleteRigaCorrente(dcBollRighe, True, dtrDeleted) Then Return
      oCleBoll.RecordSalva(nPos, True, dtrDeleted)

      'riabilito, se necessario, il codice valuta
      If dsBoll.Tables("CORPO").Rows.Count = 0 Then
        If ckEt_scorpo.Checked = False Then
          If oCleBoll.bModPM And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
            'no docu di prod in valuta se PM
          Else
            GctlSetVisEnab(edEt_valuta, False)
          End If
        End If
        If edEt_valuta.Text = "0" Then GctlSetVisEnab(ckEt_scorpo, False)
      End If

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      grvRighe.LeftCoord = 0
      If ec_matric.Visible Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bInDeleteRow = False
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115937500, "Posizionarsi prima nella griglia e selezionare la riga da ripristinare"))
        Return
      End If
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcBollRighe, True) Then Return
      If oCleBoll.RecordRipristina(dcBollRighe.Position, dcBollRighe.Filter) Then
        grvRighe.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelOrdini.ItemClick
    Try
      SelezionaOrdini("T")

      '-------------------------------------------------
      'Rifaccio la lettura delle disponbilità perchè l'evento viene scatenato anche dalle righe degli scarichi collegati.
      If NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)) <> "" Then
        LeggiDisponibilitaArticolo(grvRighe.GetFocusedRowCellValue(ec_codart).ToString, _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_magaz).ToString), _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_fase).ToString), _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_commeca).ToString))
      Else ' Se sono su una riga vuota leggo i dati della penultima riga inserita
        If grvRighe.RowCount >= 2 Then
          LeggiDisponibilitaArticolo(NTSCStr(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_codart)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_magaz)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_fase)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_commeca)))
        End If
      End If
      'si posiziona sull'ultima riga
      grvRighe.NTSNuovo()

      If ec_matric.Visible And oCleBoll.bDisabilitaDecodificaBarcode = False Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelRigheOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelRigheOrdini.ItemClick
    Try
      SelezionaOrdini("R")

      '-------------------------------------------------
      'Rifaccio la lettura delle disponbilità perchè l'evento viene scatenato anche dalle righe degli scarichi collegati.
      If NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)) <> "" Then
        LeggiDisponibilitaArticolo(grvRighe.GetFocusedRowCellValue(ec_codart).ToString, _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_magaz).ToString), _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_fase).ToString), _
                                   NTSCInt(grvRighe.GetFocusedRowCellValue(ec_commeca).ToString))
      Else ' Se sono su una riga vuota leggo i dati della penultima riga inserita
        If grvRighe.RowCount >= 2 Then
          LeggiDisponibilitaArticolo(NTSCStr(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_codart)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_magaz)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_fase)), _
                                     NTSCInt(grvRighe.GetRowCellValue(grvRighe.RowCount - 2, ec_commeca)))
        End If
      End If
      'si posiziona sull'ultima riga
      grvRighe.NTSNuovo()

      If ec_matric.Visible And oCleBoll.bDisabilitaDecodificaBarcode = False Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Dim bNewTmp As Boolean = False
    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleBoll.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115307634000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleBoll.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "F" And oCleBoll.bNew And Not oCleBoll.IsRFsalvabile Then
          'non posso salvare
        Else
          If Not Salva(-1, False) Then Return
        End If
      End If

      Stampa(1)

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bCreaCarProd Then
        CreaCaricoProduzione()
      Else
        If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Dim bNewTmp As Boolean = False
    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleBoll.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115324170000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleBoll.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "F" And oCleBoll.bNew And Not oCleBoll.IsRFsalvabile Then
          'non posso salvare
        Else
          If Not Salva(-1, False) Then Return
        End If
      End If

      Stampa(0)

      If CheckRiapriDocSuSalva() = False Then
        RipristinaInterfaccia()
        If oMenu.lFormLastReport > 0 Then SetForegroundWindow(CType(oMenu.lFormLastReport, IntPtr))
      End If

      If bCreaCarProd Then
        CreaCaricoProduzione()
      Else
        If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      End If

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
    Dim bNewTmp As Boolean = False

    '------------------------------
    'PER UN ESEMPIO COMPLETO VEDI BNORCONF: questo programma può stampare solo con un PDF per file, non un pdf per tutti i documenti
    '------------------------------

    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleBoll.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115344762000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleBoll.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        '1) Operazioni preliminari - vedi FileStampa
        'Non salva il documento se non salvabile
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "F" And oCleBoll.bNew And Not oCleBoll.IsRFsalvabile Then
          'non posso salvare
        Else
          If Not Salva(-1, False) Then Return
        End If
      End If

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleBoll.GetQueryStampaPdf
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

      If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "B" Then
        dttFormule.Rows.Add(New Object() {"PREZZOINBOLLA", Nothing, strLastPrezzoInbolla, Nothing})
      End If

      If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
        '--------------------------------------------------
        strNumcliPVR = ""
        If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
          If oCleBoll.bGestPVR = True Then
            strNumcliPVR = oMenu.GetSettingBusDitt(DittaCorrente, "OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
            For i = 1 To (6 - Len(strNumcliPVR))
              strNumcliPVR = "0" & strNumcliPVR
            Next
            'per un fattore di velocità non testo la tabella valuta:
            'se sono in valuta presuppongo che sia l'unica ammessa
            If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) = 0 Then
              strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
            Else
              strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
            End If
            strNumcliPVR = strNumcliPVR & CLN__STD.calcolaPVRCheck(strNumcliPVR)
          End If
        End If
        dttFormule.Rows.Add(New Object() {"NUMCLIPVR", Nothing, strNumcliPVR, Nothing})
      End If


      'parte aggiuntiva diPeSetSelectionFormula (i marcatori verranno sostituiti con i campi di 'strQueryGetGroupBy'
      strQueryCrw32FileMultipli = "{MOVMAG.codditt} = '" & DittaCorrente & "'" & _
                                 " AND {MOVMAG.mm_stasino} <> 'N'" & _
                                 " AND {MOVMAG.mm_anno} = |anno|" & _
                                 " AND {MOVMAG.mm_numdoc} = |numero|" & _
                                 " AND {MOVMAG.mm_serie} = |serie|" & _
                                 " AND {MOVMAG.mm_tipork} = |tipork|"
      If oCleBoll.bUsaKeyMag Then strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "B" Then strQueryCrw32FileMultipli = strQueryCrw32FileMultipli & " AND {MOVMAG.mm_stasino}<>'D'"

      strQueryCrw32FileUnico = strQueryCrw32FileMultipli

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BSVEBOLL"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico
      oPar.strParam = "Stampa documenti"
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

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bCreaCarProd Then
        CreaCaricoProduzione()
      Else
        If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
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
        RipristinaInterfaccia()
      Else
        If Not RipristinaDocumento() Then Return
      End If

      If bCreaCarProd Then CreaCaricoProduzione()

      SetStato(0, False)
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbEmail_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEmail.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.strPar2 = NTSCStr(dsBoll.Tables("TESTA").Rows(0)!et_tipork)
      oPar.strPar3 = NTSCStr(dsBoll.Tables("TESTA").Rows(0)!et_serie)
      oPar.dPar1 = NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_anno)
      oPar.dPar2 = NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_numdoc)

      oMenu.RunChild("NTSINFORMATICA", "FRMEMCMAI", oApp.Tr(Me, 129048215400020161, "Console e-mail"), DittaCorrente, "", "BNEMCMAI", oPar)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSalvanuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalvanuovo.ItemClick
    Try
      'se dopo salva devo proporre nuovo, non ha senso mantenere aperto il doc salvato ...
      If tlbSalvanuovo.Checked Then
        tlbRiapriDopoSalva.Checked = False
        tlbRiapriDopoSalva.Enabled = False
      Else
        GctlSetVisEnab(tlbRiapriDopoSalva, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbConfRegCassa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbConfRegCassa.ItemClick
    Dim frmReca As FRMVERECA = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      frmReca = CType(NTSNewFormModal("FRMVERECA"), FRMVERECA)
      frmReca.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmReca.oCleBoll = oCleBoll
      frmReca.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      Select Case frmReca.bRecaAnnullato
        Case True
          oApp.MsgBoxInfo(oApp.Tr(Me, 130196653374871212, "Configurazione Registratore di Cassa annullata."))
        Case False
          oApp.MsgBoxInfo(oApp.Tr(Me, 130196653221320394, "Configurazione Registratore di Cassa effettuata."))
      End Select
      '--------------------------------------------------------------------------------------------------------------
      frmReca.Dispose()
      frmReca = Nothing
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmReca Is Nothing Then frmReca.Dispose()
      frmReca = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbAggCorrFF_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggCorrFF.ItemClick
    Dim oPar As New CLE__PATB
    Try
      If dsBoll.Tables("CORPO").Rows.Count > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130628666260755831, "Per utilizzare questa funzionalità non devono essere presenti righe nel corpo."))
        Return
      End If

      If edEt_conto.TextInt = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130628667279030573, "Prima indicare il conto."))
        Return
      Else
        If Not oCleBoll.OkTestata() Then
          tsBoll.Focus()
          Return
        End If
      End If

      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      '------------------------
      'Chiama lo Zoom sulle testate
      oPar.strDescr = oCleBoll.GetWhereHltmFatturaFigurativa()
      oPar.nTipologia = 1       '0 = posso selez una sola riga, 1 posso fare multiselezione, 2 = acconti
      oPar.strTipo = "C"

      NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      '------------------------
      'ho selezionato qualche cosa
      If Not oCleBoll.ImportaCorrispettivoInFatFigurativa(CType(oPar.oParam, DataTable)) Then Return

      'Blocco il documento, non posso fare più nulla sul corpo
      grvRighe.NTSAllowInsert = False
      grvRighe.Enabled = False
      tlbRecordCancella.Enabled = False
      oCleBoll.bInEvasioneDaFattFigurativa = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Voci Toolbar STRUMENTI"
  Public Overridable Sub tlbRegCassa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRegCassa.ItemClick
    Dim bNewTmp As Boolean = False
    Try
      bNewTmp = oCleBoll.bNew

      FileRegcassa(False)

      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbStampaEtichetteRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaEtichetteRiga.ItemClick
    Try
      ApriEtichette(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImpostaFileTerm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaFileTerm.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Try
      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSVEBOLL"

      oMenu.RunChild("NTSInformatica", "FRMMGTERM", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbNumerazioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNumerazioni.ItemClick
    Try
      oMenu.RunChild("BS__NUME", "CLS__NUME", oApp.Tr(Me, 128521252906680000, "Numerazioni"), DittaCorrente, "", "", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbTabBolli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTabBolli.ItemClick
    Try
      oMenu.RunChild("BSVEBOTR", "CLSVEBOTR", oApp.Tr(Me, 128521252892016000, "Tabella bolli"), DittaCorrente, "", "", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRiordinarighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRiordinarighe.ItemClick
    Dim dttTmp As New DataTable
    Dim strSort As String = ""
    Try
      'se non sono dentro ad un documento non faccio nulla
      If tsBoll.Visible = False Then Return

      'prima di riordinare le righe devono essere salvate
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      'se le righe sono filtrate devo rimuovere il filtro
      grvRighe.DisableColumnSortFilter()

      'non posso applicare la sort: devo clonare il datatable, svuotarlo e riempirlo nuovamente
      If tlbOrdinaRigheRiga.Down Then strSort = "ec_riga"
      If tlbOrdinaRigheCodiceArticolo.Down Then strSort = "ec_codart"
      If tlbOrdinaRigheDescrizione.Down Then strSort = "ec_descr"

      dttTmp = dsBoll.Tables("CORPO").Copy
      dsBoll.Tables("CORPO").Clear()
      For Each dtrT As DataRow In dttTmp.Select("", strSort)
        dsBoll.Tables("CORPO").ImportRow(dtrT)
      Next
      dsBoll.Tables("CORPO").AcceptChanges()

      'mi posiziono sulla prima riga
      grvRighe.NTSMoveFirstRowColunn()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  'ordinamento righe corpo
  Public Overridable Sub tlbOrdinaRigheRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheRiga.ItemClick, tlbOrdinaRigheRiga.ItemClick
    oCleBoll.nOrdrig = 0
    tlbOrdinaRigheCodiceArticolo.Down = False
    tlbOrdinaRigheDescrizione.Down = False
  End Sub
  Public Overridable Sub tlbOrdinaRigheCodiceArticolo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheCodiceArticolo.ItemClick
    oCleBoll.nOrdrig = 1
    tlbOrdinaRigheRiga.Down = False
    tlbOrdinaRigheDescrizione.Down = False
  End Sub
  Public Overridable Sub tlbOrdinaRigheDescrizione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheDescrizione.ItemClick
    oCleBoll.nOrdrig = 2
    tlbOrdinaRigheCodiceArticolo.Down = False
    tlbOrdinaRigheRiga.Down = False
  End Sub
#End Region

#Region "Voci Toolbar TESTA"
  Public Overridable Sub tlbNuovoDocDaPrec_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovoDocDaPrec.ItemClick
    Dim lNewProgr As Integer = 0
    Dim nNewAnno As Integer = 0
    Dim strNewSerie As String = ""
    Dim strNewTipork As String = ""
    Dim strSoloSerie As String = ""
    Dim dttTmp As New DataTable
    Dim frmOpen As FRMVESEBO = Nothing
    Try
      If oCleBoll.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128602555183593750, "Il comando può essere utilizzato solo su documenti già salvati"))
        Return
      End If

      If oCleBoll.bHasChangesET Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128602622670156250, "Il documento ha subito delle modifiche dopo la sua apertura. Duplicazione non possibile"))
        Return
      End If

      'Esce se è in corso una modifica di un record
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      Me.Cursor = Cursors.Default

      '-----------------------------
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "

      '-----------------------------
      'visualizzo la form
      frmOpen = CType(NTSNewFormModal("FRMVESEBO"), FRMVESEBO)
      frmOpen.Init(oMenu, Nothing, DittaCorrente, Nothing)
      AddHandler oCleBoll.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity
      frmOpen.oCleBoll = oCleBoll
      frmOpen.Text = oApp.Tr(Me, 128776115422052000, "Estremi nuovo documento")
      dttTmp = cbTipoDoc.DataSource.Clone
      For Each dtrT As DataRow In CType(cbTipoDoc.DataSource, DataTable).Rows
        dttTmp.ImportRow(dtrT)
      Next
      frmOpen.cbTipo.DataSource = dttTmp
      frmOpen.cbTipo.ValueMember = cbTipoDoc.ValueMember
      frmOpen.cbTipo.DisplayMember = cbTipoDoc.DisplayMember
      frmOpen.cbTipo.SelectedValue = cbTipoDoc.SelectedValue
      frmOpen.cbTipo.Enabled = True
      frmOpen.edAnno.Text = edAnnoDoc.Text
      If strSoloSerie <> "?" Then
        frmOpen.edSerie.Text = strSoloSerie
        frmOpen.edSerie.Enabled = False
      Else
        frmOpen.edSerie.Text = edSerieDoc.Text
      End If
      frmOpen.strTiporkOrig = cbTipoDoc.SelectedValue
      '--------------------------------------------
      'propongo anno e serie dell'ultimo documento creato
      frmOpen.edAnno.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "Recent", ".", "LastDocNewAnno", frmOpen.edAnno.Text, " ", frmOpen.edAnno.Text)
      'frmOpen.edSerie.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "Recent", ".", "LastDocNewSerie", frmOpen.edSerie.Text, " ", frmOpen.edSerie.Text).PadLeft(1)

      frmOpen.ShowDialog()
      RemoveHandler oCleBoll.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity

      'ho selezionato annulla
      If frmOpen.bOk = False Then Return

      strNewTipork = frmOpen.cbTipo.SelectedValue
      nNewAnno = NTSCInt(frmOpen.edAnno.Text)
      strNewSerie = frmOpen.edSerie.Text.ToUpper
      lNewProgr = NTSCInt(frmOpen.edNumero.Text)

      If nNewAnno = 0 Then Return
      If strNewSerie = "" Then Return
      If lNewProgr = 0 Then Return

      If strNewTipork = cbTipoDoc.SelectedValue And _
         lNewProgr = NTSCInt(oCleBoll.dttET.Rows(0)!et_numdoc) And _
         strNewSerie = NTSCStr(oCleBoll.dttET.Rows(0)!et_serie) And _
         nNewAnno = NTSCInt(oCleBoll.dttET.Rows(0)!et_anno) Then Return

      Me.Cursor = Cursors.WaitCursor

      'inoltre se cambio tipork devo ricaricare anche la griglia ...
      SetStato(0, False)
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString <> strNewTipork Then
        GctlTipoDoc = strNewTipork
        GctlSetRoules()
        GestisciBarcodeIn()
      End If
      NTSGetDataAutocompletamentoTipork = cbTipoDoc.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

      If oCleBoll.DuplicaDoc(strNewTipork, nNewAnno, strNewSerie, lNewProgr, 0, 0) Then
        cbTipoDoc.SelectedValue = strNewTipork
        edAnnoDoc.Text = nNewAnno.ToString
        edSerieDoc.Text = strNewSerie
        edNumDoc.Text = lNewProgr.ToString

        grvRighe.NTSMoveFirstRowColunn()
        tsBoll.SelectedTabPageIndex = 0
        If strNewTipork = "A" Then
          GctlSetVisEnab(ckEt_proforma, False)
        Else
          ckEt_proforma.Enabled = False
        End If

        GctlSetVisEnab(tlbSalva, False)
        oCleBoll.bDocNonModificabile = False
        oCleBoll.bNonModificabileperPicking = False
        SetStato(1, False)
        oApp.MsgBoxInfo(oApp.Tr(Me, 128602591253906250, "Duplicazione documento terminata"))

        'pulisco i recent dei documenti da aprire
        dttOpens.Clear()
        tlbApriDocPrec.Enabled = False
        tlbApriDocSucc.Enabled = False
        tlbCancella.Enabled = False
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128602620241093750, "Duplicazione documento non eseguita"))
        Ripristina()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Ripristina()
    Finally
      If Not frmOpen Is Nothing Then frmOpen.Dispose()
      frmOpen = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbDatiContabiliSintetici_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDatiContabiliSintetici.ItemClick
    Dim oPar As New CLE__CLDP
    Dim oDcst As New CLE__DCST
    Dim nEscomp As Integer
    Try
      If NTSCInt(edEt_conto.Text) = 0 Then Return
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then
        Return
      End If
      If Not oCleBoll.AggiornaDisimpegno Then Return
      If Not oCleBoll.CalcolaTotali() Then Return
      'struttura per passare i parametri a BNCGDCST e ricevere gli stessi compilati con i valori calcolati in BECGDCST
      oMenu.EscompFromDate(DittaCorrente, NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc), nEscomp, Nothing)
      With oDcst
        .lConto = NTSCInt(oCleBoll.dttET.Rows(0)!et_conto)
        .nEscomp = nEscomp
        .strData = NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc).ToShortDateString
        .bControllaFido = True
        .bControllaInsoluti = True
        .bStatPagamenti = True
        .dttMmtrans = oCleBoll.dttEC
        .dImpOdierno = NTSCDec(edEt_totdoc.Text)
        .bOpendoc = Not oCleBoll.bNew
        .strTipork = oCleBoll.dttET.Rows(0)!et_tipork.ToString
        .nAnno = NTSCInt(oCleBoll.dttET.Rows(0)!et_anno)
        .strSerie = oCleBoll.dttET.Rows(0)!et_serie.ToString
        .lNumdoc = NTSCInt(oCleBoll.dttET.Rows(0)!et_numdoc)
        .bBlocca = False
        .bVisForm = True
        .bVismess = True
      End With

      oPar.ctlPar1 = oDcst
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"

      oMenu.RunChild("NTSInformatica", "FRMCGDCST", "", DittaCorrente, "", "BNCGDCST", oPar, "", True, True)

      '-----------------------------------------
      'prova per inserire 400 record
      'Dim dtr As DataRow = Nothing
      'Dim nStart As Integer = dsBoll.Tables("CORPO").Rows.Count
      'CLN__STD.CopiaDatarow(dsBoll, "MOVORD", 0, dtr)
      'For i As Integer = 0 To 400
      '  dsBoll.Tables("CORPO").ImportRow(dtr)
      '  dsBoll.Tables("CORPO").Rows(dsBoll.Tables("CORPO").Rows.Count - 1)!ec_riga = nStart + i
      'Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbModifNumDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbModifNumDoc.ItemClick
    Dim lNewProgr As Integer = 0
    Dim nNewAnno As Integer = 0
    Dim strNewSerie As String = ""
    Dim strNewTipork As String = ""
    Dim strSoloSerie As String = ""
    Dim dttTmp As New DataTable
    Dim strTipoProg As String = ""
    Dim frmOpen As FRMVESEBO = Nothing
    Try
      If oCleBoll.bNew = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128534121291752000, "Numero documento modificabile solo sui nuovi documenti"))
        Return
      End If

      'Esce se è in corso una modifica di un record
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      Me.Cursor = Cursors.Default

      '-----------------------------
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "

      '-----------------------------
      'visualizzo la form
      frmOpen = CType(NTSNewFormModal("FRMVESEBO"), FRMVESEBO)
      frmOpen.Init(oMenu, Nothing, DittaCorrente, Nothing)
      AddHandler oCleBoll.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity
      frmOpen.oCleBoll = oCleBoll
      frmOpen.Text = oApp.Tr(Me, 128776115436716000, "Cambia tipo/numero documento")
      dttTmp = cbTipoDoc.DataSource.Clone
      For Each dtrT As DataRow In CType(cbTipoDoc.DataSource, DataTable).Rows
        dttTmp.ImportRow(dtrT)
      Next
      frmOpen.cbTipo.DataSource = dttTmp
      frmOpen.cbTipo.ValueMember = cbTipoDoc.ValueMember
      frmOpen.cbTipo.DisplayMember = cbTipoDoc.DisplayMember
      frmOpen.cbTipo.SelectedValue = cbTipoDoc.SelectedValue
      frmOpen.edAnno.Text = edAnnoDoc.Text
      If strSoloSerie <> "?" Then
        frmOpen.edSerie.Text = strSoloSerie
        frmOpen.edSerie.Enabled = False
      Else
        frmOpen.edSerie.Text = edSerieDoc.Text
      End If

      If cbTipoDoc.SelectedValue <> "B" And cbTipoDoc.SelectedValue <> "A" And cbTipoDoc.SelectedValue <> "C" And _
         cbTipoDoc.SelectedValue <> "F" And cbTipoDoc.SelectedValue <> "S" Then
        'non posso cambiare il tipo documento
        frmOpen.cbTipo.Enabled = False
      End If

      frmOpen.ShowDialog()
      RemoveHandler oCleBoll.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity

      'ho selezionato annulla
      If frmOpen.bOk = False Then Return

      nNewAnno = NTSCInt(frmOpen.edAnno.Text)
      strNewSerie = frmOpen.edSerie.Text.ToUpper
      lNewProgr = NTSCInt(frmOpen.edNumero.Text)
      strNewTipork = frmOpen.cbTipo.SelectedValue

      If strNewTipork = "" Then Return
      If nNewAnno = 0 Then Return
      If strNewSerie = "" Then Return
      If lNewProgr = 0 Then Return

      'Controlla conguenza tipo/numero
      If cbTipoDoc.SelectedValue <> "B" And cbTipoDoc.SelectedValue <> "A" And cbTipoDoc.SelectedValue <> "C" And _
         cbTipoDoc.SelectedValue <> "F" And cbTipoDoc.SelectedValue <> "S" Then
        'in questo caso NON si può cambiare tipo ma solo serie/numero
        If strNewTipork <> cbTipoDoc.SelectedValue Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128660293527343750, "Per questo documento è possibile solo modificare anno/serie/numero, ma non il tipo."))
          Return
        End If
      Else
        If strNewTipork <> "B" And strNewTipork <> "A" And strNewTipork <> "C" And strNewTipork <> "F" And strNewTipork <> "S" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128660293967968750, "Il tipo di documento di destinazione deve essere uno dei seguenti: 'DDT emesso', 'Fattura immed. emessa', 'Corrispettivo', 'Ricevuta fiscale' o 'Fatt. ricevuta fiscale'."))
          Return
        End If
      End If

      If strNewTipork = "" And lNewProgr = 0 And strNewSerie = "" And nNewAnno = 0 Then Return
      If strNewTipork = cbTipoDoc.SelectedValue And _
         lNewProgr = NTSCInt(oCleBoll.dttET.Rows(0)!et_numdoc) And _
         strNewSerie = NTSCStr(oCleBoll.dttET.Rows(0)!et_serie) And _
         nNewAnno = NTSCInt(oCleBoll.dttET.Rows(0)!et_anno) Then Return

      'cambio il num doc su tutte le tabella
      If Not oCleBoll.CambiaNumdoc(dsBoll, strNewTipork, lNewProgr, strNewSerie, nNewAnno, True) Then Return
      If strNewTipork <> "" And strNewTipork <> cbTipoDoc.SelectedValue Then
        'inoltre se cambio tipork devo ricaricare anche la griglia ...
        cbTipoDoc.SelectedValue = strNewTipork
        GctlTipoDoc = strNewTipork
        GctlSetRoules()
        GestisciBarcodeIn()
      End If

      edAnnoDoc.Text = nNewAnno.ToString
      edSerieDoc.Text = strNewSerie
      edNumDoc.Text = lNewProgr.ToString

      'se ho forzato il num doc non devo salvarlo con aggnuma
      strTipoProg = cbTipoDoc.SelectedValue
      Select Case cbTipoDoc.SelectedValue
        Case "A", "D", "E", "N", "£" : strTipoProg = "A"
        Case "B" : strTipoProg = "B"
        Case "C" : strTipoProg = "C"
        Case "M" : strTipoProg = "M"
        Case "Z" : strTipoProg = "Z"
        Case "L", "K", "J", "(" : strTipoProg = "L"
        Case "W" : strTipoProg = "W"
        Case "T" : strTipoProg = "T"
        Case "U" : strTipoProg = "U"
        Case "F", "I" : strTipoProg = "F"
        Case "S" : strTipoProg = "S"
      End Select
      If lNewProgr <> oCleBoll.LegNuma(strTipoProg, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text)) Then
        oCleBoll.bProgrCambiato = True
      Else
        oCleBoll.bProgrCambiato = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If cbTipoDoc.SelectedValue = "B" Then
        GctlSetVisEnab(ckXx_przbol, False)
      Else
        ckXx_przbol.Enabled = False
        ckXx_przbol.Checked = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmOpen Is Nothing Then frmOpen.Dispose()
      frmOpen = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbNavigazDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazDoc.ItemClick
    Dim strParam As String = ""
    Try
      If oCleBoll.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128554237315694937, "Funzionalità non abilitata sui nuovi documenti"))
        Return
      End If

      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "F" Or oCleBoll.dttET.Rows(0)!et_tipork.ToString = "I" Or _
         oCleBoll.dttET.Rows(0)!et_tipork.ToString = "P" Or oCleBoll.dttET.Rows(0)!et_tipork.ToString = "S" Or _
         oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Or oCleBoll.dttET.Rows(0)!et_tipork.ToString = "U" Or _
         oCleBoll.dttET.Rows(0)!et_tipork.ToString = "Z" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776115501182000, "Funzione non disponibile per questo tipo di documento"))
        Return
      End If

      strParam = "APRI;" & oCleBoll.dttET.Rows(0)!et_tipork.ToString & ";" & _
                edAnnoDoc.Text & ";" & _
                edSerieDoc.Text.PadRight(1) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(edNumDoc.Text).ToString.PadLeft(9, "0"c), 9) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(oCleBoll.dttET.Rows(0)!et_conto).ToString.PadLeft(9, "0"c), 9) & ";" & _
                Microsoft.VisualBasic.Right("          " & NTSCDate(edet_datdoc.Text).ToString("dd/MM/yyyy"), 10) & _
                ";000000000;0000;0000; ;000000000;0000;" & _
                IIf(oCleBoll.dttET.Rows(0)!et_tipork.ToString = "D" Or _
                    oCleBoll.dttET.Rows(0)!et_tipork.ToString = "K" Or _
                    oCleBoll.dttET.Rows(0)!et_tipork.ToString = "£" Or _
                    oCleBoll.dttET.Rows(0)!et_tipork.ToString = "(", "4", "2").ToString
      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 128154847220982619, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRettificaTotdoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRettificaTotdoc.ItemClick
    Dim nTipoElab As Integer = 0
    Dim dNewTotDoc As Decimal = 0
    Dim frmRttd As FRMVERTTD = Nothing
    Try
      'Esce con alcuni tipi di documento
      If cbTipoDoc.SelectedValue = "T" Or cbTipoDoc.SelectedValue = "I" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128687225445937500, "Funzionalità non disponibile su documenti di tipo 'Carico di produzione' e 'Riemissione Ricevute fiscali'."))
        Return
      End If

      'Esce se è in corso una modifica
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      '----------------------
      'aggiorno i totali e mi sposto nel piede 
      tsBoll.SelectedTabPageIndex = 2

      '----------------------
      'visualizzo la form
      frmRttd = CType(NTSNewFormModal("FRMVERTTD"), FRMVERTTD)
      frmRttd.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmRttd.oCleBoll = oCleBoll
      frmRttd.lbAttuale.Text = oApp.Tr(Me, 128687238887656250, "Totale documento |" & IIf(NTSCInt(edEt_valuta.Text) = 0, edEt_totdoc.Text, edEt_totdocv.Text).ToString & "| - valuta |" & edEt_valuta.Text & "|")
      frmRttd.ShowDialog()


      If frmRttd.bOk = False Then Return

      '----------------------
      'Tipo di rettifica da fare: determino il nuovo totale documento
      If frmRttd.opDiventi.Checked Then
        nTipoElab = 0
        dNewTotDoc = NTSCDec(frmRttd.edDiventi.Text)
      End If
      If frmRttd.opDiminuiscaDel.Checked Then
        nTipoElab = 1
        If NTSCInt(edEt_valuta.Text) <> 0 Then
          dNewTotDoc = ArrDbl(NTSCDec(edEt_totdocv.Text) - (NTSCDec(edEt_totdocv.Text) * (NTSCDec(frmRttd.edDiminuiscaDel.Text) / 100)), oMenu.TrovaNdec(NTSCInt(edEt_valuta.Text)))
        Else
          dNewTotDoc = ArrDbl(NTSCDec(edEt_totdoc.Text) - (NTSCDec(edEt_totdoc.Text) * (NTSCDec(frmRttd.edDiminuiscaDel.Text) / 100)), oMenu.TrovaNdec(0))
        End If
      End If
      If frmRttd.opDiminuiscaDi.Checked Then
        nTipoElab = 2
        If NTSCInt(edEt_valuta.Text) <> 0 Then
          dNewTotDoc = NTSCDec(edEt_totdocv.Text) - NTSCDec(frmRttd.edDiminuiscaDel.Text)
        Else
          dNewTotDoc = NTSCDec(edEt_totdoc.Text) - NTSCDec(frmRttd.edDiminuiscaDel.Text)
        End If
      End If
      If dNewTotDoc = 0 Then Return

      '----------------------
      'Parte
      oCleBoll.RettificaTotdoc(dNewTotDoc)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmRttd Is Nothing Then frmRttd.Dispose()
      frmRttd = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbRicalcPrzScnPro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcPrzScnPro.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.strNomProg = "BSVEBOLL"
      oMenu.RunChild("NTSInformatica", "FRMMGRICA", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.bPar1 Then
        Cursor.Current = Cursors.WaitCursor
        oCleBoll.RicalcolaCondizioniDocumento(grvRighe.NTSGetCurrentDataRow)
        Cursor.Current = Cursors.Default
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSchedatrasporto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSchedatrasporto.ItemClick
    Try
      If Not oCleBoll.CheckAperturaSchetrasp(False, False) Then Return
      ApriSchedaTrasp(False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Voci Toolbar RIGHE"
  Public Overridable Sub tlbPrecMov_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecMov.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strT() As String = Nothing
    Dim dTmp1 As Decimal = 0
    Dim dTmp2 As Decimal = 0
    Try
      '----------------------
      If Not RigaConArticoloOk(False) Then Return

      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115657224000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If

      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And (Not oCleBoll.bTerzista) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569717214375000, "Funzione abilitata solo su Carichi da produzione a terzisti e non interni."))
        Return
      End If

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar2 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz)
      oPar.dPar3 = NTSCInt(edEt_conto.Text)
      oPar.strPar2 = ""                   'VALORI DI RITORNO

      oMenu.RunChild("NTSInformatica", "FRMMGGRSC", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.strPar2 = "" Then Return

      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569727909531250, "Non è possibile riportare le precedenti condizioni in ' Carichi da produzione'."))
        Return
      End If

      strT = oPar.strPar2.Split(";"c)

      '----------------------
      'Riporta i prezzi praticati in precedenza
      If NTSCInt(edEt_valuta.Text) <> 0 Then
        If NTSCInt(edEt_valuta.Text) <> NTSCInt(strT(0)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128570370248281250, "Non è possibile riportare le precedenti condizioni commerciali (prezzi/sconti) in quanto il documento selezionato riporta una valuta (|" & strT(0) & "|) diversa da quella del documento corrente (|" & edEt_valuta.Text & "|)."))
          Return
        Else
          If NTSCInt(edEt_valuta.Text) <> 0 Then
            grvRighe.NTSGetCurrentDataRow!ec_prezvalc = NTSCDec(strT(2))
          End If
          grvRighe.NTSGetCurrentDataRow!ec_prezzo = NTSCDec(strT(4))
        End If
      Else
        If ckEt_scorpo.Checked Then
          If strT(1).ToUpper = "S" Then
            'Sia l'origine che la destinazione sono in SCORPORO
            If NTSCInt(strT(11)) <> NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codiva) Then
              'Se il codice IVA della riga è diverso lo segnala
              oApp.MsgBoxErr(oApp.Tr(Me, 128570375300937500, "Non è possibile riportare le precedenti condizioni commerciali (prezzi/sconti) in quanto nel documento selezionato, di tipo IVA compresa, la riga riporta un codive IVA (|" & strT(11) & "|) diverso da quella corrente (|" & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codiva).ToString & "|)."))
              Return
            Else
              grvRighe.NTSGetCurrentDataRow!ec_preziva = NTSCDec(strT(3))
              'oCleBoll.Scorporo(NTSCDec(strT(3)), NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codiva), dTmp1, dTmp2, oApp.NDecImporti)
              grvRighe.NTSGetCurrentDataRow!ec_prezzo = NTSCDec(strT(4))
            End If
          Else
            'Il documento selezionato è normale, quello corrente è di scorporo
            oApp.MsgBoxErr(oApp.Tr(Me, 128776115829156000, "Non è possibile riportare le precedenti condizioni commerciali (prezzi/sconti) in quanto il documento selezionato è di tipo IVA compresa (scorporo) al contrario di quello corrente."))
            Return
          End If
        Else
          If strT(1).ToUpper = "S" Then
            'Il documento selezionato è di scorporo, quello corrente no
            oApp.MsgBoxErr(oApp.Tr(Me, 128570374672187500, "Non è possibile riportare le precedenti condizioni commerciali (prezzi/sconti) in quanto il documento selezionato è di tipo IVA compresa (scorporo) al contrario di quello corrente."))
            Return
          Else
            grvRighe.NTSGetCurrentDataRow!ec_prezzo = NTSCDec(strT(4))
          End If
        End If
      End If    'If NTSCInt(edEt_valuta.Text) <> 0 Then

      '----------------------
      'riporta gli sconti praticati in precedenza
      grvRighe.NTSGetCurrentDataRow!ec_scont1 = NTSCDec(strT(5))
      grvRighe.NTSGetCurrentDataRow!ec_scont2 = NTSCDec(strT(6))
      grvRighe.NTSGetCurrentDataRow!ec_scont3 = NTSCDec(strT(7))
      grvRighe.NTSGetCurrentDataRow!ec_scont4 = NTSCDec(strT(8))
      grvRighe.NTSGetCurrentDataRow!ec_scont5 = NTSCDec(strT(9))
      grvRighe.NTSGetCurrentDataRow!ec_scont6 = NTSCDec(strT(10))

      '----------------------
      'se sono su una delle colonne che ho modificato, devo forzare il valore appena scritto nel datatable, 
      'diversamente non viene visualizzato fino a quando non esco dalla cella...
      Select Case grvRighe.FocusedColumn.Name
        Case "ec_prezzo", "ec_preziva", "ec_prezvalc", "ec_scont1", "ec_scont2", "ec_scont3", "ec_scont4", "ec_scont5", "ec_scont6"
          grvRighe.SetFocusedValue(grvRighe.NTSGetCurrentDataRow(grvRighe.FocusedColumn.Name))
      End Select


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbScarCollegati_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbScarCollegati.ItemClick
    Dim frmScal As FRMVESCAL = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim strColSort As String = "ec_riga"

    Try
      frmScal = CType(NTSNewFormModal("FRMVESCAL"), FRMVESCAL)
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115878920000, "Funzione disponibile solo per 'Scarichi a produzione'"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116039982000, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116216456000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116230808000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleBoll.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere gli altri impegni collegati
      ds.Tables.Add(dsBoll.Tables("CORPOIMP").Clone())
      Select Case oCleBoll.nOrdrig
        Case 0 : strColSort = "ec_riga"
        Case 1 : strColSort = "ec_codart"
        Case 2 : strColSort = "ec_descr"
        Case 3 : strColSort = "ec_commeca"
      End Select

      For Each dtrT As DataRow In dsBoll.Tables("CORPOIMP").Select("ec_prriga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, strColSort & " ASC, ec_riga ASC")
        ds.Tables("CORPOIMP").ImportRow(dtrT)
        dtrT.Delete()
      Next
      dsBoll.Tables("CORPOIMP").AcceptChanges()

      frmScal.oCleBoll = oCleBoll
      frmScal.dttUm = dttUm
      frmScal.bEt_Scorpo = ckEt_scorpo.Checked
      frmScal.nEt_valuta = NTSCInt(edEt_valuta.Text)
      frmScal.nEt_listino = NTSCInt(edEt_listino.Text)
      frmScal.dtEt_datdoc = NTSCDate(edet_datdoc.Text)
      frmScal.nAnnoDoc = NTSCInt(edAnnoDoc.Text)
      frmScal.strSerieDoc = edSerieDoc.Text
      frmScal.lNumdoc = NTSCInt(edNumDoc.Text)
      frmScal.nEt_magaz = NTSCInt(edEt_magaz.Text)
      frmScal.lEt_conto = NTSCInt(edEt_conto.Text)
      frmScal.InitEntity(oMenu, Nothing, ds)
      frmScal.ShowDialog()
      grvRighe.Focus()

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        If ds.Tables("CORPOIMP").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("CORPOIMP").Rows(i)!ec_riga) > 0 Then
            dsBoll.Tables("CORPOIMP").ImportRow(ds.Tables("CORPOIMP").Rows(i))
          Else
            ds.Tables("CORPOIMP").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsBoll.Tables("CORPOIMP").AcceptChanges()
      oCleBoll.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleBoll.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleBoll.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        If oCleBoll.bModPM And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_pmtaskid) > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128776116291998000, "ATTENZIONE:" & vbCrLf & _
                 "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                 "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management."))
        End If
        oCleBoll.SistemaPMSalconDaQta(grvRighe.NTSGetCurrentDataRow)
        If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      End If
      AbilitaDisabilitaRigaNum()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmScal Is Nothing Then frmScal.Dispose()
      frmScal = Nothing
      Me.Cursor = Cursors.Default
      oCleBoll.dtrHT = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbLavCollegate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLavCollegate.ItemClick
    Dim frmLavc As FRMVELAVC = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dtrLavcent() As DataRow = Nothing

    Try
      frmLavc = CType(NTSNewFormModal("FRMVELAVC"), FRMVELAVC)
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115897640000, "Funzione disponibile solo per 'Carichi da produzione'"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116059014000, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116117240000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116131280000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.ValidaLastControl()

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleBoll.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere le altre lavorazioni collegate
      'aggiungo alla tabella anche le colonne di assris, per fare in modo che le due tabelle vengano gestite come se fossero una
      dsBoll.Tables("LAVCENT").AcceptChanges()

      ds.Tables.Add(dsBoll.Tables("LAVCENT").Clone())

      'importo i dati dalle due tabelle
      dtrLavcent = dsBoll.Tables("LAVCENT").Select("lce_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "lce_rigaa")
      For i = 0 To dtrLavcent.Length - 1
        'carico attivit
        ds.Tables("LAVCENT").ImportRow(dtrLavcent(i))
        dtrLavcent(i).Delete()
      Next
      dsBoll.Tables("LAVCENT").AcceptChanges()
      ds.Tables("LAVCENT").AcceptChanges()

      frmLavc.oCleBoll = oCleBoll
      frmLavc.InitEntity(oMenu, Nothing, ds)
      frmLavc.ShowDialog()
      grvRighe.Focus()

      '-------------------------------
      'riacquisisco le lavorazioni aggiornate
      For i = 0 To ds.Tables("LAVCENT").Rows.Count - 1
        If ds.Tables("LAVCENT").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("LAVCENT").Rows(i)!lce_rigaa) > 0 Then
            dsBoll.Tables("LAVCENT").ImportRow(ds.Tables("LAVCENT").Rows(i))
          Else
            ds.Tables("LAVCENT").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsBoll.Tables("LAVCENT").AcceptChanges()
      oCleBoll.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleBoll.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleBoll.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        If oCleBoll.bModPM And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_pmtaskid) > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128776116308378000, "ATTENZIONE:" & vbCrLf & _
                 "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                 "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management."))
        End If
        oCleBoll.SistemaPMSalconDaQta(grvRighe.NTSGetCurrentDataRow)
        If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      End If
      AbilitaDisabilitaRigaNum()

    Catch ex As Exception
      dsBoll.Tables("LAVCENT").RejectChanges()
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmLavc Is Nothing Then frmLavc.Dispose()
      frmLavc = Nothing
      Me.Cursor = Cursors.Default
      oCleBoll.dtrHT = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApriDB_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriDB.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & "," & _
                  NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc).ToString("dd/MM/yyyy") & "," & _
                  Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_commeca).ToString.PadLeft(9, "0"c), 9) & "," & _
                  "000000000"
      oMenu.RunChild("BSDBDIBA", "CLSDBDIBA", oApp.Tr(Me, 128557642245856788, "Distinta Base"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSelezDaTerm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezDaTerm.ItemClick
    Dim i As Integer = 0
    Dim strPath As String = ""
    Dim strFile As String = ""
    Try
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128571282388281250, "Funzionalità non disponibile sui 'Carichi da produzione'."))
        Return
      End If

      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.TerminaleLeggiCheck("BSVEBOLL") Then Return

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleBoll.SeparatePathAndFileName(oCleBoll.strTermFileName, strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        'strFile = oApp.InputBoxNew(oApp.Tr(Me, 128571298019531250, "Inserire il nome del file da acquisire (che deve risiedere nella cartella '|" & strPath & "|'):"), Mid(strFile, 2))
        'oCleBoll.strTermFileName = (strPath & "\").Replace("\\", "\") & strFile
        Dim oDlg As New NTSOpenFileDialog
        oDlg.Multiselect = False
        oDlg.ShowHelp = False
        oDlg.Title = oApp.Tr(Me, 128571256152187500, "File da acquisire")
        oDlg.DefaultExt = "TXT"
        oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
        oDlg.InitialDirectory = strPath
        oDlg.FileName = Mid(strFile, 2)
        oDlg.oMenu = oMenu
        oDlg.ShowDialog()
        If oDlg.FileName <> "" Then
          oCleBoll.strTermFileName = oDlg.FileName
        End If
        oDlg.Dispose()
        oDlg = Nothing
      End If

      'Lancia la shell
      If oCleBoll.bTermExecute Then
        'Prima di tutto cancella il file se esiste
        Try
          System.IO.File.Delete(oCleBoll.strTermFileName)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 128571303239218750, "Impossibile cancellare il file '|" & oCleBoll.strTermFileName & "|' prima della acquisizione. Errore: ") & vbCrLf & ex.Message)
          Return
        End Try

        If oCleBoll.strTermCommand.Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If oCleBoll.bTermSilent Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = oCleBoll.strTermCommand
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            oApp.MsgBoxErr(oApp.Tr(Me, 128571304353281250, "Errore durante l'esecuzione della Shell '|" & oCleBoll.strTermCommand & "|'. Errore: ") & vbCrLf & ex.Message)
            Return
          End Try
        End If
      End If    'If oCleBoll.bTermExecute Then

      '---------------------------
      'inizio l'import delle righe del file
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.TerminaleLeggiImportaFile() Then Return

      '---------------------------
      'mi sposto sull'ultima riga 
      grvRighe.NTSMoveLastRow()

      '---------------------------
      'Visualizza il log
      If oCleBoll.bTermShowLogErr Or oCleBoll.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleBoll.LogFileName)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbSelezAcconti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezAcconti.ItemClick
    Dim oPar As New CLE__PATB
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable

    Try
      '------------------------------
      'controlli preliminari
      If cbTipoDoc.SelectedValue = "W" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128704550640312500, "Funzione non disponibile su note di prelievo."))
        Return
      End If

      oMenu.ValCodiceDb(NTSCInt(edEt_tipobf.Text).ToString, DittaCorrente, "TABTPBF", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128704551490625000, "Il codice Tipo Bolla/Fattura inserito non è valido."))
        Return
      End If

      If dttTmp.Rows(0)!tb_flacconto.ToString <> "E" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128704551861875000, "Il codice Tipo Bolla/Fattura non indica che questo documento evade precedenti documenti di acconto; non è possibile selezionarli."))
        Return
      End If

      If Not oCleBoll.OkTestata() Then
        tsBoll.Focus()
        Return
      End If
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      '------------------------
      'Chiama lo Zoom sulle testate
      oPar.strDescr = oCleBoll.GetWhereHltmAcconti()
      oPar.nTipologia = 2       '0 = posso selez una sola riga, 1 posso fare multiselezione, 2 = acconti
      oPar.strTipo = ""

      NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      '------------------------
      'ho selezionato qualche cosa
      oCleBoll.bAccontiVerificati = True
      If Not oCleBoll.ImportaAcconti(CType(oPar.oParam, DataTable)) Then Return

      'si posiziona sull'ultima riga
      grvRighe.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbNoteRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNoteRiga.ItemClick
    Try
      If grvRighe.Columns("ec_note").Visible = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557689742844788, "La colonna 'NOTE' non è visibile; impossibile visualizzarne il contenuto"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557689491840788, "Posizionarsi su una riga contenente un articolo"))
        Return
      End If

      Me.ValidaLastControl()
      Dim frmMsgb As FRM__MSG1 = Nothing
      frmMsgb = CType(NTSNewFormModal("FRM__MSG1"), FRM__MSG1)
      frmMsgb.Init(oMenu, Nothing, DittaCorrente)
      frmMsgb.edMess.Text = grvRighe.GetFocusedRowCellValue(ec_note).ToString
      frmMsgb.Text = "NOTE RIGA"
      frmMsgb.lConto = NTSCInt(edEt_conto.Text)
      frmMsgb.ShowDialog()
      If frmMsgb.bOk Then grvRighe.SetFocusedRowCellValue(ec_note, frmMsgb.edMess.Text)
      frmMsgb.Dispose()
      frmMsgb = Nothing

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbCopiaRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaRiga.ItemClick
    Try
      If grRighe.ContainsFocus = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183611415758, "Posizionarsi nella griglia del corpo del documento sulla riga da copiare"))
        Return
      End If

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183715040006, "Posizionarsi nella griglia del corpo del documento sulla riga da copiare"))
        Return
      End If

      Me.ValidaLastControl()

      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      lRigaCopiata = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbIncollaRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIncollaRiga.ItemClick
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557650827742788, "Copia/Incolla di righe di CARICHI DA PRODUZIONE non consentito."))
        Return
      End If

      If lRigaCopiata = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183823665726, "Utilizzare prima la funzione 'Copia riga'"))
        Return
      End If

      If grRighe.ContainsFocus = False Or Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055183940731430, "Posizionarsi sull'ultima riga della griglia del corpo del documento"))
        Return
      End If

      dtrT = oCleBoll.dttEC.Select("ec_riga = " & lRigaCopiata.ToString)
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055184034040142, "Impossibile incollare la riga: non è stata trovata la riga n° |" & lRigaCopiata.ToString & "| copiata."))
        Return
      End If

      oCleBoll.bInIncollaRiga = True
      dsBoll.Tables("CORPO").Rows.Add(dsBoll.Tables("CORPO").NewRow)
      With dsBoll.Tables("CORPO").Rows(dsBoll.Tables("CORPO").Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = DittaCorrente
        !ec_causale = dtrT(0)!ec_causale
        !ec_causale2 = dtrT(0)!ec_causale2
        !ec_magaz = dtrT(0)!ec_magaz    'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
        !ec_magaz2 = dtrT(0)!ec_magaz2
        !ec_codart = dtrT(0)!ec_codart
        !ec_fase = dtrT(0)!ec_fase
        !ec_unmis = dtrT(0)!ec_unmis
        !ec_descr = dtrT(0)!ec_descr
        !ec_colli = dtrT(0)!ec_colli
        !ec_quant = dtrT(0)!ec_quant
        !ec_preziva = dtrT(0)!ec_preziva
        !ec_prezvalc = dtrT(0)!ec_prezvalc
        !ec_prezzo = dtrT(0)!ec_prezzo
        !ec_scont1 = dtrT(0)!ec_scont1
        !ec_scont2 = dtrT(0)!ec_scont2
        !ec_scont3 = dtrT(0)!ec_scont3
        !ec_scont4 = dtrT(0)!ec_scont4
        !ec_scont5 = dtrT(0)!ec_scont5
        !ec_scont6 = dtrT(0)!ec_scont6
        !ec_codiva = dtrT(0)!ec_codiva
        !ec_commen = dtrT(0)!ec_commen
        !ec_note = dtrT(0)!ec_note
        !ec_controp = dtrT(0)!ec_controp
        !ec_contocontr = dtrT(0)!ec_contocontr
        !ec_stasino = dtrT(0)!ec_stasino
        !ec_provv = dtrT(0)!ec_provv
        !ec_provv2 = dtrT(0)!ec_provv2
        !ec_prelist = dtrT(0)!ec_prelist
        !ec_codcfam = dtrT(0)!ec_codcfam
        !ec_commeca = dtrT(0)!ec_commeca
        !ec_subcommeca = dtrT(0)!ec_subcommeca
        !ec_codcena = dtrT(0)!ec_codcena
        !ec_coddivi = dtrT(0)!ec_coddivi
        !ec_desint = dtrT(0)!ec_desint
        !ec_codvuo = dtrT(0)!ec_codvuo
        !ec_lotto = dtrT(0)!ec_lotto
        !ec_matric = dtrT(0)!ec_matric
        !ec_ricimp = dtrT(0)!ec_ricimp
        !ec_misura1 = dtrT(0)!ec_misura1
        !ec_misura2 = dtrT(0)!ec_misura2
        !ec_misura3 = dtrT(0)!ec_misura3
        !ec_umprz = dtrT(0)!ec_umprz
        !ec_ubicaz = dtrT(0)!ec_ubicaz
        !ec_codvuo = dtrT(0)!ec_codvuo
        !ec_codnomc = dtrT(0)!ec_codnomc
        !ec_massakg = dtrT(0)!ec_massakg
        !ec_massaum2 = dtrT(0)!ec_massaum2
        !ec_valstat = dtrT(0)!ec_valstat
        !ec_proorig = dtrT(0)!ec_proorig
        !ec_percvst = dtrT(0)!ec_percvst
        !ec_numpac = dtrT(0)!ec_numpac
        !ec_codclie = dtrT(0)!ec_codclie
        !ec_ortipo = dtrT(0)!ec_ortipo
        !ec_oranno = dtrT(0)!ec_oranno
        !ec_orserie = dtrT(0)!ec_orserie
        !ec_ornum = dtrT(0)!ec_ornum
        !ec_orriga = dtrT(0)!ec_orriga
        !ec_salcon = dtrT(0)!ec_salcon
        !ec_prtipo = dtrT(0)!ec_prtipo
        !ec_pranno = dtrT(0)!ec_pranno
        !ec_prserie = dtrT(0)!ec_prserie
        !ec_prnum = dtrT(0)!ec_prnum
        !ec_prriga = dtrT(0)!ec_prriga
        !ec_cltipo = dtrT(0)!ec_cltipo
        !ec_clanno = dtrT(0)!ec_clanno
        !ec_clserie = dtrT(0)!ec_clserie
        !ec_clnum = dtrT(0)!ec_clnum
        !ec_clriga = dtrT(0)!ec_clriga
        !ec_nptipo = dtrT(0)!ec_nptipo
        !ec_npanno = dtrT(0)!ec_npanno
        !ec_npserie = dtrT(0)!ec_npserie
        !ec_npnum = dtrT(0)!ec_npnum
        !ec_npriga = dtrT(0)!ec_npriga
        !xxo_npultagg = dtrT(0)!xxo_npultagg
        !xxo_orultagg = dtrT(0)!xxo_orultagg
        !ec_ricimp = dtrT(0)!ec_ricimp          'mm_flstat : rinominata per standardizzare il nome delle colonne con gsor
        !ec_qtadisimp = dtrT(0)!ec_qtadisimp
        !ec_coldisimp = dtrT(0)!ec_coldisimp
        !ec_valdisimp = dtrT(0)!ec_valdisimp
        !ec_npqtadis = dtrT(0)!ec_npqtadis
        !ec_npcoldis = dtrT(0)!ec_npcoldis
        !ec_npvaldis = dtrT(0)!ec_npvaldis
        !ec_datini = dtrT(0)!ec_datini
        !ec_datfin = dtrT(0)!ec_datfin
      End With

      'tco: riverso i dati del tco: la nuova riga tco è stata creata in automatico alla creazione della nuova riga dttEC
      If Not oCleBoll.dttECTC Is Nothing Then
        dtrT = oCleBoll.dttECTC.Select("ec_riga = " & lRigaCopiata.ToString)
        If dtrT.Length > 0 Then
          For i = 1 To 24
            oCleBoll.dttECTC.Rows(oCleBoll.dttECTC.Rows.Count - 1)("ec_quant" & i.ToString("00")) = dtrT(0)("ec_quant" & i.ToString("00"))
          Next
        End If
      End If

      oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing)

      'mi posiziono sulla nuova riga
      grvRighe.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleBoll.bInIncollaRiga = False
    End Try
  End Sub

  Public Overridable Sub tlbInsRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInsRiga.ItemClick
    Dim lPrevRiga As Integer = 0
    Dim lNewRiga As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If dsBoll.Tables("CORPO").Rows.Count = 0 Then Return
      If grvRighe.FocusedRowHandle < 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055184125629598, "Posizionarsi prima su una riga già compilata"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      dtrT = dsBoll.Tables("CORPO").Select("ec_riga < " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      If dtrT.Length > 0 Then lPrevRiga = NTSCInt(dtrT(0)!ec_riga)
      lNewRiga = NTSCInt(Fix((NTSCDec((NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) - lPrevRiga) / 2) + lPrevRiga)))

      If dsBoll.Tables("CORPO").Select("ec_riga = " & lNewRiga.ToString).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055184230816806, "Impossibile inserire una riga intermedia; riga |" & lNewRiga.ToString & "| già presente nel corpo del documento"))
        Return
      End If

      dsBoll.Tables("CORPO").Rows.Add(dsBoll.Tables("CORPO").NewRow)
      'forzo la MovordOnAddNewRow
      dsBoll.Tables("CORPO").Rows(dsBoll.Tables("CORPO").Rows.Count - 1)!codditt = "."
      dsBoll.Tables("CORPO").Rows(dsBoll.Tables("CORPO").Rows.Count - 1)!codditt = DittaCorrente

      dsBoll.Tables("CORPO").Rows(dsBoll.Tables("CORPO").Rows.Count - 1)!ec_riga = lNewRiga
      oCleBoll.lCrighe -= oCleBoll.nIncremContatoreRiga

      'ora mi ci devo posizionare sopra ...
      For i = 0 To grvRighe.RowCount - 1
        If NTSCInt(grvRighe.GetRowCellValue(i, "ec_riga")) = lNewRiga Then
          grvRighe.FocusedRowHandle = i
          grvRighe.NTSGetCurrentDataRow.SetAdded()    'altrimenti al salvataggio della riga non esplode i kit e la db
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbVisDispVariante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisDispVariante.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      'la chiamata è identica a quella di frmDqta.tlbRecordDispVariante_ItemClick

      '------------------------------
      'controlli preliminari
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128577303104218750, "Articolo non gestito a Taglie e colori"))
        Return
      End If
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055184370701726, "Magazzino sulla riga non impostato"))
        Return
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BS--HLAP"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) & "|" & _
               NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) & "|" & _
               NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz).ToString & "|" & _
               NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz).ToString & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "ARTPROTC" & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", True, True)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRicalcolaColliQta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcolaColliQta.ItemClick
    Dim strErrore As String = ""
    Dim dQuant As Decimal = 0
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then Return

      '-----------------------------
      With grvRighe.NTSGetCurrentDataRow
        If NTSCStr(!ec_codart) = "D" Or NTSCStr(!ec_codart) = "M" Then
          oCleBoll.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          grvRighe.NTSGetCurrentDataRow!ec_colli = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
          oCleBoll.bInValidazQuant = False
          Return
        End If

        If CType(oCleBoll.oCleComm, CLELBMENU).ConvQuantUM(DittaCorrente, !ec_codart.ToString, !ec_ump.ToString, _
                  NTSCDec(!ec_quant), NTSCDec(!ec_misura1), NTSCDec(!ec_misura2), _
                  NTSCDec(!ec_misura3), !ec_unmis.ToString, dQuant, strErrore, oApp.NDecQta) Then
          oCleBoll.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          !ec_colli = dQuant
          oCleBoll.bInValidazQuant = False
          If strErrore <> "" Then oApp.MsgBoxErr(strErrore)
        End If

        If !ec_umprz.ToString = "S" Then     'ricalcolo il prezzo se unità di misura su colli
          'devo farlo perchè altrimenti il prezzo viene ricalcolato solo al cambio della quantità
          'ma in questo caso la qta non cambia perchè 'bInValidazQuant = True'
          oCleBoll.SettaCondCommerciali(True, False, False, grvRighe.NTSGetCurrentDataRow, 0, 0, False)
          oCleBoll.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
        End If
      End With

    Catch ex As Exception
      oCleBoll.bInValidazQuant = False
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSalvaCondPart_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalvaCondPart.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dttCopa As New DataTable
    Dim nCodlavo As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dPrezzo As Decimal = 0
    Try
      '------------------------------
      'controlli preliminari
      If Not RigaConArticoloOk(False) Then Return
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115682184000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And Not oCleBoll.bTerzista Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128576533131562500, "Funzione abilitata solo su carichi da produzione a terzisti e non interni."))
        Return
      End If

      Me.ValidaLastControl()

      '-------------------------------
      'preparo i parametri da passare a vecopa
      dttCopa.Columns.Add("conto", GetType(Integer))
      dttCopa.Columns.Add("coddest", GetType(Integer))
      dttCopa.Columns.Add("valuta", GetType(Integer))
      dttCopa.Columns.Add("agente", GetType(Integer))
      dttCopa.Columns.Add("agente2", GetType(Integer))
      dttCopa.Columns.Add("datdoc", GetType(String))
      dttCopa.Columns.Add("codart", GetType(String))
      dttCopa.Columns.Add("fase", GetType(Integer))
      dttCopa.Columns.Add("quant", GetType(Decimal))
      dttCopa.Columns.Add("prezzo", GetType(Decimal))
      dttCopa.Columns.Add("prezvalc", GetType(Decimal))
      dttCopa.Columns.Add("prznet", GetType(String))
      dttCopa.Columns.Add("perqta", GetType(Integer))
      dttCopa.Columns.Add("ump", GetType(String))
      dttCopa.Columns.Add("scont1", GetType(Decimal))
      dttCopa.Columns.Add("scont2", GetType(Decimal))
      dttCopa.Columns.Add("scont3", GetType(Decimal))
      dttCopa.Columns.Add("scont4", GetType(Decimal))
      dttCopa.Columns.Add("scont5", GetType(Decimal))
      dttCopa.Columns.Add("scont6", GetType(Decimal))
      dttCopa.Columns.Add("provv", GetType(Decimal))
      dttCopa.Columns.Add("provv2", GetType(Decimal))
      dttCopa.Columns.Add("codlavo", GetType(Integer))
      dttCopa.Columns.Add("codtpro", GetType(Integer))

      'Rileva il codice lavorazione terzista
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
        nCodlavo = 0
        dtrT = dsBoll.Tables("LAVCENT").Select("lce_riga = " & grvRighe.NTSGetCurrentDataRow!ec_riga.ToString, "lce_riga, lce_rigaa")
        If dtrT.Length > 0 Then nCodlavo = NTSCInt(dtrT(0)!lce_codlavo)
      End If

      If ckEt_scorpo.Checked Then
        dPrezzo = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_preziva)
      Else
        dPrezzo = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prezzo)
      End If

      dttCopa.Rows.Add(dttCopa.NewRow)
      With dttCopa.Rows(0)
        !conto = NTSCInt(edEt_conto.Text)
        !coddest = NTSCInt(edEt_coddest.Text)
        !valuta = NTSCInt(edEt_valuta.Text)
        !agente = NTSCInt(edEt_codagen.Text)
        !agente2 = 0
        !datdoc = NTSCDate(edet_datdoc.Text).ToShortDateString
        !codart = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
        !fase = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
        !quant = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
        !prezzo = dPrezzo
        !prezvalc = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prezvalc)
        !prznet = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_flprznet)
        !perqta = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_perqta)
        !ump = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_ump)
        !scont1 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont1)
        !scont2 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont2)
        !scont3 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont3)
        !scont4 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont4)
        !scont5 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont5)
        !scont6 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont6)
        !provv = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_provv)
        !provv2 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_provv2)
        !codlavo = nCodlavo
        !codtpro = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codtpro)
      End With
      dttCopa.AcceptChanges()

      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.ctlPar1 = dttCopa
      oPar.bPar1 = False             'visualizza la form di FRMVECOPA
      oMenu.RunChild("NTSInformatica", "FRMVECOPA", "", DittaCorrente, "", "BNVECOPA", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRigheOrdBarcode_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRigheOrdBarcode.ItemClick
    Try
      SelezionaOrdini("B")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRicFiscDaRiem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicFiscDaRiem.ItemClick
    Dim oPar As New CLE__PATB
    Try
      If cbTipoDoc.SelectedValue <> "I" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128708966090781250, "Questo tipo di documento non consente la selezione delle Ricevute fiscali da riemettere."))
        Return
      End If

      If Not oCleBoll.OkTestata() Then
        tsBoll.Focus()
        Return
      End If

      '------------------------
      'Chiama lo Zoom sulle testate delle ricevute fiscali da riemettere
      oPar.strDescr = oCleBoll.GetWhereHlri()

      NTSZOOM.ZoomStrIn("ZOOMTESTMAGRI", DittaCorrente, oPar)
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      '------------------------
      'ho selezionato qualche cosa
      If Not oCleBoll.ImportaRicevuteFiscali(CType(oPar.oParam, DataTable)) Then Return

      tsBoll.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbIntegraRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIntegraRighe.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Try
      If Not CBool(oCleBoll.lModuliDittaDitt And bsModTV) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128705251194531250, "Funzionalità non disponibile quando non è attivo il modulo 'Tentata vendita'."))
        Return
      End If

      If cbTipoDoc.SelectedValue = "T" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128705207108906250, "Funzionalità non disponibile sui documenti di tipo 'Carico di produzione'."))
        Return
      End If

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      If Not oCleBoll.OkTestata Then Return
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSVEBOLL"
      oPar.ctlPar1 = Nothing

      oMenu.RunChild("NTSInformatica", "FRMVEADRG", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.ctlPar1 Is Nothing Then Return
      If CType(oPar.ctlPar1, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor
      oCleBoll.IntegraRighe(CType(oPar.ctlPar1, DataTable))
      CType(oPar.ctlPar1, DataTable).Clear()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbScaricaMagaz_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbScaricaMagaz.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      If Not oCleBoll.OkTestata Then Return
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      oCleBoll.ScaricaMagaz()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbApriConfiguratore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriConfiguratore.ItemClick
    Dim strParam As String = ""
    Dim nRowHandle As Integer = 0
    Try
      nRowHandle = grvRighe.FocusedRowHandle
      Me.ValidaLastControl()
      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return

      If Not grRighe.Focused Or grvRighe.FocusedColumn.Name <> "ec_codart" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128554264182124967, _
                         "Per poter aprire il 'Configuratore di Prodotto'" & vbCrLf & _
                         "è necessario posizionarsi sul corpo dell'ordine" & vbCrLf & _
                         "in corrispondenza del campo 'Cod. Articolo'."))
        Return
      End If

      'con la Recordsalva si psota sull'ultima riga da salvare ... mi riposizoino sulla riga su cui era, se è cambiata
      If grvRighe.FocusedRowHandle <> nRowHandle Then
        grvRighe.FocusedRowHandle = nRowHandle
        grvRighe.FocusedColumn = ec_codart
      End If

      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCP2) Then
        strParam = NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).ToString.PadLeft(CLN__STD.CodartMaxLen)
        If strParam.Trim = "" Then
          'Se non c'è nulla nel campo codice articolo...
          '...significa che voglio creare una nuova configurazione e riportarla
          strParam = "NUOVO;" & strParam
          oMenu.RunChild("BSCPCONP", "CLSCPCONP", oApp.Tr(Me, 128776134919278000, "Gestione Configuratore di prodotto"), DittaCorrente, "", "", Nothing, strParam, True, True)
          If strParam <> "" Then
            'Se ho effettivamente un articolo da riportare...
            grvRighe.SetFocusedValue(strParam)
          Else
            '...non ha senso fare qualcosa
          End If
        Else
          'Se nel campo codice articolo c'è qualcosa...
          '...significa che voglio aprire la configurazione dell'articolo per guardarla
          'NON DEVO COMUNQUE RIPORTARE NULLA
          oMenu.RunChild("BSCPCONP", "CLSCPCONP", oApp.Tr(Me, 128776134930666000, "Gestione Configuratore di prodotto"), DittaCorrente, "", "", Nothing, "APRI ;" & strParam, True, True)
        End If

      Else
        'Parte vecchia: CP
        'Viene richiamata la configurazione associata all'articolo di riga
        strParam = "APRI;" & _
                   NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).PadRight(CLN__STD.CodartMaxLen) & "," & _
                   Microsoft.VisualBasic.Right("          " & NTSCDate(edet_datdoc.Text).ToString("dd/MM/yyyy"), 10)
        oMenu.RunChild("BSCPCOPR", "CLSCPCOPR", oApp.Tr(Me, 128776134942678000, "Configuratore di prodotto"), DittaCorrente, "", "", Nothing, strParam, True, True)

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbApriOrdineCollegato_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriOrdineCollegato.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128717309928750000, "La riga su cui si è posizionati non evade ordini"))
        Return
      End If

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_ortipo.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oranno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_orserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128589367205937500, "Gestione ordini/impegni"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbApriNotaCollegata_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriNotaCollegata.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128717311327187500, "La riga su cui si è posizionati non evade note di prelievo"))
        Return
      End If

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_nptipo.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npanno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_npserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128776118081826000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbApriBollaCtoLavcollegata_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriBollaCtoLavcollegata.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clnum) = 0 Or _
          (NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_cltipo) = cbTipoDoc.SelectedValue And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clanno) = NTSCInt(edAnnoDoc.Text) And _
          NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_clserie) = edSerieDoc.Text And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clnum) = NTSCInt(edNumDoc.Text)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128717316590781250, "La riga su cui si è posizionati non evade documenti di c.to lavoro attivo"))
        Return
      End If

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_cltipo.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clanno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_clserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clnum).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128776118095866000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbApriCarProdCollegato_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriCarProdCollegato.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ccnum) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128717316519687500, "La riga su cui si è posizionati non evade carichi di produzione"))
        Return
      End If

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_cctipo.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ccanno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_ccserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ccnum).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128776118113026000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGiub_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGiub.ItemClick
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return

      oCallParams = New CLE__CLDP
      oCallParams.strPar1 = ""
      oCallParams.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz)
      oCallParams.bPar1 = False

      'Chiamo la dll
      oMenu.RunChild("NTSInformatica", "FRMRMGIUB", oApp.Tr(Me, 128956010155061981, "Giacenza"), DittaCorrente, "", "BNRMGIUB", oCallParams, Nothing, True, True)

      If oCallParams.strPar3.Trim("-"c).Trim <> "" Then grvRighe.NTSGetCurrentDataRow!ec_ubicaz = oCallParams.strPar3.PadRight(12, "-"c)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSiub_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSiub.ItemClick
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return

      oCallParams = New CLE__CLDP
      oCallParams.strPar1 = ""
      oCallParams.strPar2 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
      oCallParams.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz)
      oCallParams.bPar1 = False

      'Chiamo la dll
      oMenu.RunChild("NTSInformatica", "FRMRMSIUB", oApp.Tr(Me, 128885197022015890, "Situazione"), DittaCorrente, "", "BNRMSIUB", oCallParams, Nothing, True, True)

      If oCallParams.strPar3.Trim("-"c).Trim <> "" Then grvRighe.NTSGetCurrentDataRow!ec_ubicaz = oCallParams.strPar3.PadRight(12, "-"c)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbExport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbExport.ItemClick
    Dim dtrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim strExport() As String
    Dim strPath As String = ""
    Dim strFile As String = ""
    Try
      dtrRow = dsBoll.Tables("CORPO").Select("xx_sel = 'S'")
      If dtrRow.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129248818662149899, "Selezionare almeno una riga da esportare."))
        Return
      End If

      strExport = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", oCleBoll.strTermKey, ".", "Export01", "", " ", "").Split("|"c)
      'Se non esiste la configurazione do errore
      If strExport.Length <= 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129248818703401483, "Tracciato di esportazione non configurato. Impossibile continuare."))
        Return
      End If

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleBoll.SeparatePathAndFileName(strExport(3), strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        Dim oDlg As New NTSSaveFileDialog
        oDlg.ShowHelp = False
        oDlg.Title = oApp.Tr(Me, 129249503638910508, "File da esportare")
        oDlg.DefaultExt = "TXT"
        oDlg.InitialDirectory = strPath
        oDlg.FileName = Mid(strFile, 2)
        oDlg.oMenu = oMenu
        oDlg.ShowDialog()
        If oDlg.FileName <> "" Then
          strExport(3) = oDlg.FileName
        End If
        oDlg.Dispose()
        oDlg = Nothing
      End If

      If oCleBoll.EsportaRighe(strExport, dtrRow, "BSVEBOLL") Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129248624479620804, "Esportazione completata correttamente."))
      End If

      '---------------------------
      'Visualizza il log
      If CBool(strExport(5)) Or oCleBoll.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleBoll.LogFileName)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImport.ItemClick
    Dim dttTmp As New DataTable
    Dim strImport() As String
    Dim strPath As String = ""
    Dim strFile As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Obbliga che sia presente l'articolo D
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ValCodiceDb("D", DittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129248817765705749, "E' obbligatorio che sia codificato l'articolo 'D' (descrittivo) in anagrafica articoli."))
        dttTmp.Clear()
        Return
      End If
      dttTmp.Clear()

      strImport = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", oCleBoll.strTermKey, ".", "Import01", "", " ", "").Split("|"c)

      If strImport.Length <= 1 Then strImport = "0||0|C:\?|0|0|0|0|0|0|S|§|,|0|1|1|0|0|1|0|NOCFG".Split("|"c) 'così verifica cmq se c'è il tracciato.

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleBoll.SeparatePathAndFileName(strImport(3), strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        Dim oDlg As New NTSOpenFileDialog
        oDlg.Multiselect = False
        oDlg.ShowHelp = False
        oDlg.Title = oApp.Tr(Me, 129249516494700060, "File da acquisire")
        oDlg.DefaultExt = "TXT"
        oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
        oDlg.InitialDirectory = strNomeFile
        oDlg.FileName = Mid(strFile, 2)
        oDlg.oMenu = oMenu
        oDlg.ShowDialog()
        If oDlg.FileName <> "" Then
          strImport(3) = oDlg.FileName
        End If
        oDlg.Dispose()
        oDlg = Nothing
      End If

      oCleBoll.ImportaRighe(strImport, "BSVEBOLL")

      '---------------------------
      'Visualizza il log
      If CBool(strImport(9)) Or oCleBoll.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleBoll.LogFileName)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSelectDeselect_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelectDeselect.ItemClick
    Try
      If dsBoll.Tables("CORPO").Select("xx_sel = 'N'").Length = 0 Then
        For z As Integer = 0 To dsBoll.Tables("CORPO").Rows.Count - 1
          dsBoll.Tables("CORPO").Rows(z)!xx_sel = "N"
        Next
      Else
        For z As Integer = 0 To dsBoll.Tables("CORPO").Rows.Count - 1
          dsBoll.Tables("CORPO").Rows(z)!xx_sel = "S"
        Next
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbDaLista_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaLista.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.dPar1 = NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_magaz)
      oPar.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGSELI", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.dPar2 <> 0 Then
        oCleBoll.ImportaDaLista(NTSCInt(oPar.dPar2), oPar.bPar1)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbCancRigheSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancRigheSel.ItemClick
    Dim dtrDeleted As DataRow = Nothing
    Dim nPos As Integer = 0
    Dim i As Integer
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130390078404355233, "Confermi la cancellazione delle righe selezionate?")) = Windows.Forms.DialogResult.No Then Return
      bInDeleteRow = True
      Me.Cursor = Cursors.WaitCursor

      For i = dsBoll.Tables("CORPO").Rows.Count - 1 To 0 Step -1
        If NTSCStr(dsBoll.Tables("CORPO").Rows(i)!xx_sel) = "S" Then
          dtrDeleted = dsBoll.Tables("CORPO").Rows(i)
          If Not oCleBoll.CorpoTestPreCancella(i) Then Continue For
          oCleBoll.RecordSalva(i, True, dtrDeleted)
          dsBoll.Tables("CORPO").Rows(i).Delete()
          dsBoll.Tables("CORPO").AcceptChanges()
        End If
      Next

      'riabilito, se necessario, il codice valuta
      If dsBoll.Tables("CORPO").Rows.Count = 0 Then
        If ckEt_scorpo.Checked = False Then
          If oCleBoll.bModPM And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
            'no docu di prod in valuta se PM
          Else
            GctlSetVisEnab(edEt_valuta, False)
          End If
        End If
        If edEt_valuta.Text = "0" Then GctlSetVisEnab(ckEt_scorpo, False)
      End If

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      grvRighe.LeftCoord = 0
      If ec_matric.Visible Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bInDeleteRow = False
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "ALT+F2 e ALT+F3 rimappati"
  Public Overridable Sub edEt_conto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edEt_conto.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If oCleBoll.dttEt_conto.Rows.Count = 0 Then Return
        bNuovo = False
      End If

      oTmp.Text = edEt_conto.Text
      If oCleBoll.dttEt_conto.Rows.Count = 0 Then
        If oCleBoll.bDocEmesso Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      Else
        If oCleBoll.dttEt_conto.Rows(0)!an_tipo.ToString = "C" Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      End If

      edEt_conto.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function AbilitaDisabilitaRigaNum() As Boolean
    Try
      ec_riga.Enabled = False
      If oCleBoll.AbilitaDisabilitaRigaNum(grvRighe.NTSGetCurrentDataRow) Then
        GctlSetVisEnab(ec_riga, False)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ApriAnagraficaPrivata(ByVal bAutoOpen As Boolean) As Boolean
    Dim oPar As CLE__CLDP = Nothing
    Try
      If bAutoOpen = False Then Return True

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSVEBOLL"
      With oCleBoll.dttET.Rows(0)
        oPar.bPar1 = False
        oPar.strPar1 = NTSCStr(!et_andescr1)
        oPar.strPar2 = NTSCStr(!et_andescr2)
        oPar.strPar3 = NTSCStr(!et_anindir)
        oPar.strPar4 = NTSCStr(!et_ancitta)
        oPar.strPar5 = NTSCStr(!et_ancap)
        oPar.strParam = NTSCStr(!et_anprov)
        oPar.strOpen = NTSCStr(!et_ancodfis)
      End With

      oMenu.RunChild("NTSInformatica", "FRMMGANPR", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.bPar1 Then    'non ho annullato
        With oCleBoll.dttET.Rows(0)
          oPar.bPar1 = False
          !et_andescr1 = oPar.strPar1
          !et_andescr2 = oPar.strPar2
          !et_anindir = oPar.strPar3
          !et_ancitta = oPar.strPar4
          !et_ancap = oPar.strPar5
          !et_anprov = oPar.strParam
          !et_ancodfis = oPar.strOpen
        End With

        dsBoll.Tables("TESTA").Rows(0)!xx_conto = dsBoll.Tables("TESTA").Rows(0)!et_andescr1.ToString & " " & _
                                         dsBoll.Tables("TESTA").Rows(0)!et_andescr2.ToString & vbCrLf & _
                                         dsBoll.Tables("TESTA").Rows(0)!et_anindir.ToString & vbCrLf & _
                                         dsBoll.Tables("TESTA").Rows(0)!et_ancap.ToString & " " & _
                                         dsBoll.Tables("TESTA").Rows(0)!et_ancitta.ToString & " (" & _
                                         dsBoll.Tables("TESTA").Rows(0)!et_anprov.ToString & ")"
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ApriEtichette(ByVal nCome As Integer) As Boolean
    Dim strNumero As String = ""
    Dim strCodart As String = ""
    Dim strQuant As String = ""
    Dim strParam As String = ""
    Dim dttTmp As New DataTable
    Dim bTco As Boolean
    Dim i As Integer
    Dim dttTmp2 As New DataTable
    Dim dtrTmp() As DataRow
    Try
      If nCome = 0 Then
        'Riga x riga
        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          strCodart = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
          If strCodart.Trim <> "" Then
            strCodart = strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
            If oMenu.ValCodiceDb(strCodart, DittaCorrente, "ARTICO", "S", , dttTmp) Then
              If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
                If oMenu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!ar_codtagl), DittaCorrente, "TABTAGL", "N", , dttTmp2) Then
                  bTco = True
                End If
              End If
            End If
            If bTco Then
              dtrTmp = dsBoll.Tables("CORPOTC").Select("codditt = " & CStrSQL(DittaCorrente) & _
                                         " AND ec_tipork = " & CStrSQL(grvRighe.NTSGetCurrentDataRow!ec_tipork) & _
                                         " AND ec_anno = " & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_anno) & _
                                         " AND ec_serie = " & CStrSQL(grvRighe.NTSGetCurrentDataRow!ec_serie) & _
                                         " AND ec_numdoc = " & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_numdoc) & _
                                         " AND ec_riga = " & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_riga))

              strParam = "ATC;" & strCodart & ";"
              For i = 1 To 24
                strParam = strParam & NTSCStr(dttTmp2.Rows(0)("tb_dest" & NTSCStr(IIf(i < 10, "0" & i, i)))) & ";"
                strParam = strParam & NTSCStr(dtrTmp(0)("ec_quant" & NTSCStr(IIf(i < 10, "0" & i, i)))) & ";"
              Next
              oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 130414428662140856, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
            Else
              Dim nQuant As Integer = Math.Abs(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_quant))
              If nQuant > 0 Then
                strQuant = nQuant.ToString.PadLeft(9)
                strQuant = Microsoft.VisualBasic.Right(strQuant, 9)
                strParam = "ART;" & strCodart & ";" & strQuant & ";"
                oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128776118486324000, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
              End If
            End If
          End If
        End If
      Else
        'Alla fine del documento
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "W" Then Return True 'note di prelievo non gestite da bsmgette 
        strNumero = NTSCInt(oCleBoll.dttET.Rows(0)!et_numdoc).ToString.PadLeft(9)
        strNumero = Microsoft.VisualBasic.Right(strNumero, 9)
        strParam = "DOC;" & oCleBoll.dttET.Rows(0)!et_tipork.ToString & ";" & NTSCInt(oCleBoll.dttET.Rows(0)!et_anno).ToString & ";" & oCleBoll.dttET.Rows(0)!et_serie.ToString & ";" & strNumero & ";        0;"
        oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128776118503328000, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ApriScadenze() As Boolean
    Dim i As Integer = 0
    Dim frmDtsc As FRMVEDTSC = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.ApriScadenze Then Return False
      Me.Cursor = Cursors.Default

      '-----------------------------
      'visualizzo la form
      'se sono qui ho sicuramente le scadenze calcolate (anche su nuovo docuento ancora non salvato)
      frmDtsc = CType(NTSNewFormModal("FRMVEDTSC"), FRMVEDTSC)
      frmDtsc.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmDtsc.ocleDocu = oCleBoll
      frmDtsc.ShowDialog()

      'ho selezionato annulla
      If frmDtsc.bOk = False Then
        Return False
      End If


      For i = 1 To 8
        oCleBoll.dttET.Rows(0)("et_tippaga_" & i.ToString) = NTSCInt(CType(frmDtsc.pnSpese.Controls("cbEt_tippaga_" & i.ToString), NTSComboBox).SelectedValue)
        If CType(frmDtsc.pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = "" Then
          oCleBoll.dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
        Else
          oCleBoll.dttET.Rows(0)("et_datsca_" & i.ToString) = CType(frmDtsc.pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text
        End If
        oCleBoll.dttET.Rows(0)("et_impsca_" & i.ToString) = NTSCDec(CType(frmDtsc.pnSpese.Controls("edEt_impsca_" & i.ToString), NTSTextBoxNum).Text)
        oCleBoll.dttET.Rows(0)("et_impscav_" & i.ToString) = NTSCDec(CType(frmDtsc.pnSpese.Controls("edEt_impscav_" & i.ToString), NTSTextBoxNum).Text)
      Next

      oCleBoll.bModifCastScad = True

      frmDtsc.Dispose()
      frmDtsc = Nothing

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDtsc Is Nothing Then frmDtsc.Dispose()
      frmDtsc = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function ApriVisualizzaListini(ByVal nTipoCol As Integer) As Boolean
    'nTipoCol = 0: listino normale, 1 = prezzi iva compresa, 2 = prezzi in valuta
    Dim oPar As CLE__CLDP = Nothing
    Dim strT() As String = Nothing
    Dim dtrRow As DataRow
    Dim i As Integer = 0
    Try
      '----------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776117915766000, "Posizionarsi prima su una riga di griglia in una colonna prezzo"))
        Return False
      End If

      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776117974304000, "Inserire prima il codice articolo"))
        Return False
      End If

      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115698876000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return False
      End If

      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And (Not oCleBoll.bTerzista) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115762544000, "Funzione abilitata solo su carichi da produzione a terzisti e non interni."))
        Return False
      End If

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.strPar2 = edet_datdoc.Text
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar3 = NTSCInt(edEt_conto.Text)
      oPar.dPar4 = 0        'ritorna il prezzo
      oPar.dPar5 = NTSCInt(edEt_valuta.Text)        'passa e ritorna la valuta
      oPar.ctlPar1 = Nothing

      oMenu.RunChild("NTSInformatica", "FRMMGLIST", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.dPar4 = 0 Then Return False

      '----------------------
      'Riporta i prezzi praticati in precedenza
      If NTSCInt(edEt_valuta.Text) <> NTSCInt(oPar.dPar5) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055185012661690, "Non è possibile riportare il prezzo in quanto il prezzo selezionato riporta una valuta (|" & oPar.dPar5.ToString & "|) diversa da quella del documento corrente (|" & edEt_valuta.Text & "|)."))
        Return False
      Else
        'Modifica anche il flag di prezzo netto e le promozioni
        If Not oPar.ctlPar1 Is Nothing Then
          dtrRow = CType(oPar.ctlPar1, DataRow)
          grvRighe.NTSGetCurrentDataRow!ec_codtpro = dtrRow!lc_codtpro

          grvRighe.NTSGetCurrentDataRow!ec_flprznet = dtrRow!lc_netto


          If oApp.oGvar.strSconClCliDaList = "S" Then
            grvRighe.NTSGetCurrentDataRow!ec_scont1 = dtrRow!so_scont1
            grvRighe.NTSGetCurrentDataRow!ec_scont2 = dtrRow!so_scont2
            grvRighe.NTSGetCurrentDataRow!ec_scont3 = dtrRow!so_scont3
            grvRighe.NTSGetCurrentDataRow!ec_scont4 = dtrRow!so_scont4
            grvRighe.NTSGetCurrentDataRow!ec_scont5 = dtrRow!so_scont5
            grvRighe.NTSGetCurrentDataRow!ec_scont6 = dtrRow!so_scont6
          End If
        End If

        If NTSCInt(edEt_valuta.Text) <> 0 Then
          grvRighe.NTSGetCurrentDataRow!ec_prezvalc = oPar.dPar4
        Else
          If nTipoCol = 0 Then
            grvRighe.NTSGetCurrentDataRow!ec_prezzo = oPar.dPar4
          Else
            grvRighe.NTSGetCurrentDataRow!ec_preziva = oPar.dPar4
          End If
        End If

        grvRighe.NTSMoveNextColunn()
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub Bindcontrols()
    Try
      oCleBoll.bNonValidare = True
      '-------------------------------------------------
      'se i controlli erano gi stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli:
      'ATTENZIONE: devo aver gi preparato ed otttenuto i dati dal database. 
      'se devo rileggere i dati dal database devo rifare il bind!

      ckEt_proforma.NTSText.NTSDbField = "testa.et_soloasa"
      ckEt_flboll.NTSText.NTSDbField = "testa.et_flboll"
      ckEt_flspinc.NTSText.NTSDbField = "testa.et_flspinc"
      ckEt_scorpo.NTSText.NTSDbField = "testa.et_scorpo"

      edEt_cambio.NTSDbField = "testa.et_cambio"
      edEt_valuta.NTSDbField = "testa.et_valuta"
      edEt_scont2.NTSDbField = "testa.et_scont2"
      edEt_scont1.NTSDbField = "testa.et_scont1"
      edEt_scopag.NTSDbField = "testa.et_scopag"
      edEt_codpaga.NTSDbField = "testa.et_codpaga"
      edEt_datapag.NTSDbField = "testa.et_datapag"
      edEt_listino.NTSDbField = "testa.et_listino"
      edEt_codcfam.NTSDbField = "testa.et_codcfam"
      edEt_codbanc.NTSDbField = "testa.et_codbanc"
      edEt_banc2.NTSDbField = "testa.et_banc2"
      edEt_banc1.NTSDbField = "testa.et_banc1"
      edEt_cab.NTSDbField = "testa.et_cab"
      edEt_abi.NTSDbField = "testa.et_abi"
      edEt_vettor.NTSDbField = "testa.et_vettor"
      edEt_magaz2.NTSDbField = "testa.et_magaz2"
      edEt_riferim.NTSDbField = "testa.et_riferim"
      edEt_codese.NTSDbField = "testa.et_codese"
      edEt_magaz.NTSDbField = "testa.et_magaz"
      edEt_codagen.NTSDbField = "testa.et_codagen"
      edEt_tipobf.NTSDbField = "testa.et_tipobf"
      edEt_totprov.NTSDbField = "testa.et_totprov"
      edEt_impprov.NTSDbField = "testa.et_impprov"
      edEt_datpar.NTSDbField = "testa.et_datpar"
      edEt_numpar.NTSDbField = "testa.et_numpar"
      edEt_alfpar.NTSDbField = "testa.et_alfpar"
      edEt_annpar.NTSDbField = "testa.et_annpar"
      edEt_pesonetto.NTSDbField = "testa.et_pesonetto"
      edEt_peso.NTSDbField = "testa.et_peso"
      edEt_totcoll.NTSDbField = "testa.et_totcoll"
      edEt_porto.NTSDbField = "testa.et_porto"
      cbEt_acuradi.NTSDbField = "testa.et_acuradi"
      edEt_vettor2.NTSDbField = "testa.et_vettor2"
      edEt_coddest2.NTSDbField = "testa.et_coddest2"
      edEt_codaspe.NTSDbField = "testa.et_codaspe"
      edEt_aspetto.NTSDbField = "testa.et_aspetto"
      edEt_caustra.NTSDbField = "testa.et_caustra"
      edEt_abbuonov.NTSDbField = "testa.et_abbuonov"
      edEt_abbuono.NTSDbField = "testa.et_abbuono"
      edEt_pagatov.NTSDbField = "testa.et_pagatov"
      edEt_pagato.NTSDbField = "testa.et_pagato"
      edEt_pagato2.NTSDbField = "testa.et_pagato2"
      edEt_resto.NTSDbField = "testa.et_resto"
      edEt_totomagv.NTSDbField = "testa.et_totomagv"
      edEt_totomag.NTSDbField = "testa.et_totomag"
      edEt_totdocv.NTSDbField = "testa.et_totdocv"
      edEt_totdoc.NTSDbField = "testa.et_totdoc"
      edEt_totmercev.NTSDbField = "testa.et_totmercev"
      edEt_totmerce.NTSDbField = "testa.et_totmerce"
      edEt_bolliv.NTSDbField = "testa.et_bolliv"
      edEt_bolli.NTSDbField = "testa.et_bolli"
      edEt_speincv.NTSDbField = "testa.et_speincv"
      edEt_speinc.NTSDbField = "testa.et_speinc"
      edEt_speimbv.NTSDbField = "testa.et_speimbv"
      edEt_speimb.NTSDbField = "testa.et_speimb"
      edEt_speaccv.NTSDbField = "testa.et_speaccv"
      edEt_speacc.NTSDbField = "testa.et_speacc"
      edEt_note.NTSDbField = "testa.et_note"
      edEt_cup.NTSDbField = "testa.et_cup"
      edEt_cig.NTSDbField = "testa.et_cig"
      edEt_riferimpa.NTSDbField = "testa.et_riferimpa"
      edEt_coddest.NTSDbField = "testa.et_coddest"
      edEt_conto.NTSDbField = "testa.et_conto"
      edet_datdoc.NTSDbField = "testa.et_datdoc"
      edEt_controp.NTSDbField = "testa.et_controp"

      lbXx_controp.NTSDbField = "testa.xx_controp"

      '-------------------------------------------------
      'la chiave del documento non la collego: mi serve per poter impostare i dati per creare un nuovo 
      'documento senza bisogno di avere record di testord sottostanti
      'edAnnoDoc.NTSDbField = "testa.et_anno"
      'cbTipoDoc.NTSDbField = "testa.et_tipork")
      'edSerieDoc.NTSDbField = "testa.et_serie"
      'edNumDoc.NTSDbField = "testa.et_numdoc"

      '-------------------------------------------------
      lbXx_tipobf.NTSDbField = "testa.xx_tipobf"
      lbXx_valuta.NTSDbField = "testa.xx_valuta"
      lbXx_codpaga.NTSDbField = "testa.xx_codpaga"
      lbXx_codcfam.NTSDbField = "testa.xx_codcfam"
      lbXx_codbanc.NTSDbField = "testa.xx_codbanc"
      lbXx_vettor.NTSDbField = "testa.xx_vettor"
      lbXx_magaz2.NTSDbField = "testa.xx_magaz2"
      lbXx_codese.NTSDbField = "testa.xx_codese"
      lbXx_magaz.NTSDbField = "testa.xx_magaz"
      lbXx_codagen.NTSDbField = "testa.xx_codagen"
      lbXx_porto.NTSDbField = "testa.xx_porto"
      lbXx_vettor2.NTSDbField = "testa.xx_vettor2"
      lbXx_coddest2.NTSDbField = "testa.xx_coddest2"
      lbXx_caustra.NTSDbField = "testa.xx_caustra"
      lbXx_coddest.NTSDbField = "testa.xx_coddest"
      lbXx_conto.NTSDbField = "testa.xx_conto"

      lbXx_causale.NTSDbField = "testa.xx_causale"
      edEt_causale.NTSDbField = "testa.et_causale"
      ckEt_proforma.NTSText.NTSDbField = "testa.et_proforma"
      ckEt_flboll.NTSText.NTSDbField = "testa.et_flboll"
      ckEt_flprov.NTSText.NTSDbField = "testa.et_flprov"
      ckEt_flscdb.NTSText.NTSDbField = "testa.et_flscdb"
      ckXx_przbol.NTSText.NTSDbField = "testa.xx_przbol"
      edEt_oriniz.NTSDbField = "testa.et_oriniz"
      edEt_dtiniz.NTSDbField = "testa.et_dtiniz"
      ckEt_flboll.NTSText.NTSDbField = "testa.et_flboll"
      lbXx_totquant.NTSDbField = "testa.xx_totquant"
      edEt_codntra.NTSDbField = "testa.et_codntra"
      lbXx_codntra.NTSDbField = "testa.xx_codntra"
      cbEt_intra.NTSDbField = "testa.et_intra"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcBoll, Me)
      NTSFormAddDataBinding(dcBollc, Me)
      NTSFormAddDataBinding(dcBollRighe, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleBoll.bNonValidare = False
    End Try
  End Sub

  Public Overridable Sub CalcolaSceffAppl(ByVal nRiga As Integer)
    Dim dPrezzo As Decimal = 0
    Dim dPrznet As Decimal = 0
    Dim dPrzl As Decimal = 0
    Dim dtrMo() As DataRow
    Try
      dtrMo = dsBoll.Tables("CORPO").Select("ec_riga = " & nRiga.ToString)
      If dtrMo.Length = 0 Then Return

      If dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" And oCleBoll.bPrelistIvato Then
        dPrezzo = NTSCDec(dtrMo(0)!ec_preziva)
      Else
        dPrezzo = NTSCDec(dtrMo(0)!ec_prezzo)
      End If

      dPrznet = dPrezzo * (100 - NTSCDec(dtrMo(0)!ec_scont1)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scont2)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scont3)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scont4)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scont5)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scont6)) / 100 * _
                    (100 - NTSCDec(dtrMo(0)!ec_scontp)) / 100 - NTSCDec(dtrMo(0)!ec_scontv)
      dPrzl = NTSCDec(dtrMo(0)!ec_prelist)

      edPreList.Text = dPrzl.ToString
      If dPrznet <> 0 And dPrzl <> 0 Then
        edSconto.Text = ArrDbl(((dPrzl - dPrznet) / dPrzl) * 100, 2).ToString
      Else
        edSconto.Text = "0"
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTipoRk As New DataTable()
      Dim dttTrasporto As New DataTable()
      Dim dttBlocco As New DataTable()
      Dim dttIntra As New DataTable

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttTipoRk.Rows.Add(New Object() {"B", "DDT emesso"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipoRk.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipoRk.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttTipoRk.Rows.Add(New Object() {"M", "DDT ricevuto"})
      dttTipoRk.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
      cbTipoDoc.DataSource = dttTipoRk
      cbTipoDoc.ValueMember = "cod"
      cbTipoDoc.DisplayMember = "val"

      dttTrasporto.Columns.Add("cod", GetType(String))
      dttTrasporto.Columns.Add("val", GetType(String))
      dttTrasporto.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttTrasporto.Rows.Add(New Object() {"D", "Destinatario"})
      dttTrasporto.Rows.Add(New Object() {"M", "Mittente"})
      dttTrasporto.Rows.Add(New Object() {"V", "Vettore"})
      cbEt_acuradi.DataSource = dttTrasporto
      cbEt_acuradi.ValueMember = "cod"
      cbEt_acuradi.DisplayMember = "val"

      dttIntra.Columns.Add("cod", GetType(String))
      dttIntra.Columns.Add("val", GetType(String))
      dttIntra.Rows.Add(New Object() {"N", "No intrastat"})
      dttIntra.Rows.Add(New Object() {"C", "Cessione"})
      dttIntra.Rows.Add(New Object() {"A", "Acquisto"})
      dttIntra.AcceptChanges()
      cbEt_intra.DataSource = dttIntra
      cbEt_intra.ValueMember = "cod"
      cbEt_intra.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CreaCaricoProduzione() As Boolean
    'è stata settata l'opzione di registro per creare un carico da prod al salva del doc evemsso, 
    'contenente gli articoli appena venduti
    Dim dttTesta, dttMM, dttMatr, dttTc As New DataTable
    Try
      bCreaCarProd = False

      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.GetMovmagPerAutoCarprod(strLastTipork, nLastAnno, strLastSerie, lLastNumdoc, _
                                              CBool(IIf(nCarProdInVendita = 1, True, False)), dttMM, _
                                              dttMatr, dttTc) Then Return False
      If dttMM.Rows.Count = 0 Then Return True

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129948680777947365, "Creare il carico da produzione per gli articoli appena scaricati?")) = Windows.Forms.DialogResult.No Then Return True

      strLastTiporktmp = cbTipoDoc.SelectedValue
      cbTipoDoc.SelectedValue = "T"
      edAnnoDoc.Text = nLastAnno.ToString
      edSerieDoc.Text = " "
      dttTesta = dsBoll.Tables("TESTA").Copy

      tlbNuovo_ItemClick(tlbNuovo, Nothing)
      Me.Cursor = Cursors.WaitCursor

      If Not oCleBoll.CreaCarprodDaDocEmesso(strLastTipork, nLastAnno, strLastSerie, lLastNumdoc, nCarProdInVendita, _
                                             dttTesta, dttMM, dttMatr, dttTc) Then Return False

      If CBool(Val(oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "CarProdInVenditaSalva ", "0", " ", "0"))) Then
        If Salva(-1, False) Then
          'chiudo tutto
          SetStato(0, False)
        Else
          'rimango con il documento aperto per consentire di correggere quanto necessario per salvare
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttMM.Clear()
      dttMatr.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function FileRegcassa(ByVal bForzaStampa As Boolean) As Boolean
    Try
      If Not oCleBoll.bDisableCheckRegCassa Then
        If cbTipoDoc.SelectedValue <> "C" And cbTipoDoc.SelectedValue <> "F" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128692635919687500, "La stampa su Registratore di cassa è possibile solo su documenti di tipo 'Corrispettivo', 'Ricev. fiscale'."))
          Return False
        End If
        'Solo su documenti con scoporo
        If ckEt_scorpo.Checked = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128692636362031250, "La stampa su Registratore di cassa è possibile solo su documenti IVA Inclusa (con scorporo)."))
          Return False
        End If
      End If

      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleBoll.bNew Then
        If Not bForzaStampa Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128776115360830000, "Impossibile stampare un nuovo documento non possedendo i privilegi di salvataggio."))
          Return False
        End If
      End If

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "F" And oCleBoll.bNew And Not oCleBoll.IsRFsalvabile Then
          'non posso salvare
        Else
          If Not Salva(-1, False) Then Return False
          If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()
        End If
      End If

      '--------------------------------
      'genero il file 
      If Not oCleBoll.FileRegcassa Then Return False

      If bCreaCarProd Then CreaCaricoProduzione()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    Dim frmMsgb As FRM__MSG1
    Dim strT As String = ""
    Dim oPar As CLE__CLDP = Nothing

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity
      Select Case e.TipoEvento
        Case "AggiornaColoreAbiCab" : ColoraCampoAbiCab()
        Case "FormatQtaEsistCorpo" : FormatQtaEsistCorpo()
      End Select

      'esco se i messaggi sono dovuto all'apertura del documento
      If bInLoadDoc Then Return

      If e.TipoEvento.Trim.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)

          Case "PRINTWORD:"
            Me.Text = oApp.Tr(Me, 127826697267656250, "Elaborazione documento ") + e.TipoEvento.Substring(10)

          Case "edDtPrimoP"
            If strTmp(i).Substring(11, 1) = "N" Then
              edEt_datapag.Enabled = False
            Else
              GctlSetVisEnab(edEt_datapag, False)
            End If

          Case "ChiediDest"
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791955341718750, _
                               "Il cliente/fornitore indicato non possiede alcuna destinazione diversa " & _
                               "predefinita; provvedere alla selezione?")) = Windows.Forms.DialogResult.Yes Then
              edEt_coddest.Focus()
              tlbZoom_ItemClick(Me, Nothing)
            End If

          Case "VNoteConto"
            frmMsgb = CType(NTSNewFormModal("FRM__MSG1"), FRM__MSG1)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE AL CONTO " & oCleBoll.lContoCF.ToString
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing
          Case "VNoteContD"
            frmMsgb = CType(NTSNewFormModal("FRM__MSG1"), FRM__MSG1)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE ALLA DESTINAZIONE DIVERSA DEL CONTO " & oCleBoll.lContoCF.ToString
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing
          Case "CambioDtPg"
            If edEt_datapag.Enabled = False Then edEt_datapag.Text = edet_datdoc.Text

          Case "GestScoVal"
            If dsBoll.Tables("CORPO").Rows.Count = 0 Then
              If ckEt_scorpo.Checked = False Then
                If oCleBoll.bModPM And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
                  'no docu in valuta 
                Else
                  GctlSetVisEnab(edEt_valuta, False)
                End If
              End If
              If edEt_valuta.Text = "0" Then GctlSetVisEnab(ckEt_scorpo, False)
            Else
              ckEt_scorpo.Enabled = False
              edEt_valuta.Enabled = False
            End If

          Case "ArticoloTC"
            'blocco la colonna QTA se articolo TC
            'la blocco anche se è un carico da prod in modifica interno ed è setttata l'opzione di carichi prod da avmg
            If strTmp(i).Substring(11, 1) = "S" Then
              ec_quant.Enabled = False
            Else
              If oCleBoll.bNew = False And oCleBoll.bCarProdInAvlavp And _
                 oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And oCleBoll.bTerzista = False Then
                ec_quant.Enabled = False
              Else
                If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) = "S" Then
                  ec_quant.Enabled = False
                Else
                  GctlSetVisEnab(ec_quant, False)
                End If
              End If
            End If

          Case "LeggiDispo"
            'rileggo la disponibilità e l'ultimo costo
            strTmp1 = strTmp(i).Substring(11).Split(CType("§", Char))
            LeggiDisponibilitaArticolo(strTmp1(0), _
                                       NTSCInt(strTmp1(1)), _
                                       NTSCInt(strTmp1(2)), _
                                       NTSCInt(strTmp1(3)))

          Case "LeggiPromo"
            'leggo le promozioni articolo per proporle in uscita dal cod articolo
            e.RetValue = LeggiPromozioni(strTmp(i).Substring(11), NTSCDate(edet_datdoc.Text).ToShortDateString).ToString

          Case "ApriAccont"
            'apro la form per la selezione degli acconti
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128668859551718750, "Esistono precedenti documenti di acconto da evadere su questo cliente. Procedere alla selezione?")) = Windows.Forms.DialogResult.Yes Then
              tsBoll.SelectedTabPageIndex = 1
              tlbSelezAcconti_ItemClick(tlbSelezAcconti, Nothing)
              e.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              e.RetValue = CLN__STD.ThMsg.RETVALUE_NO
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

          Case "DataValDB."
Riparti:
            strT = oApp.InputBoxNew(oApp.Tr(Me, 128599797686718750, "Inserire la data di validità della distinta base:"), edet_datdoc.Text)
            If strT.Trim = "" Then
              'premuto annulla o stringa vuota
              oApp.MsgBoxInfo(oApp.Tr(Me, 130168136837609317, "Attenzione" & vbCrLf & _
                "Non è stata indicata una data valida." & vbCrLf & _
                "L'eventuale esplosione di materiali nella Distinta Base NON sarà effettuata."))
              e.RetValue = ""
            Else
              Dim edTmp As New NTSInformatica.NTSTextBoxData
              edTmp.Text = ""
              edTmp.Name = "edTmp1"
              edTmp.Visible = False
              edTmp.Parent = Me
              edTmp.NTSSetParam(oMenu, oApp.Tr(Me, 128599798701250000, "Data di validità della distinta base."), True)
              edTmp.Text = strT
              If edTmp.Text = "" Then GoTo Riparti
              e.RetValue = edTmp.Text
              edTmp.Dispose()
            End If

          Case "SCHEDATRAS"
            If strTmp(i).Length > 11 Then
              If strTmp(i).Substring(11, 1) = "S" Then
                ApriSchedaTrasp(False)
              Else
                ApriSchedaTrasp(True)
              End If
            Else
              ApriSchedaTrasp(True)
            End If

          Case "DOAUTS___:"
            'PM: visualizzo la form per consentire il superamento del budget 
            strTmp1 = e.Message.Split(";"c)
            Dim oPar1 As New CLE__CLDP
            oPar1.dPar1 = NTSCInt(strTmp1(0))  'commessa
            oPar1.dPar2 = NTSCInt(strTmp1(1))  'task
            oPar1.dPar3 = NTSCDec(strTmp1(2))  'importo 
            oPar1.strPar1 = strTmp1(3)         'xx_gestcost
            oMenu.RunChild("NTSInformatica", "FRMPMAUTS", "", DittaCorrente, "", "BNPMAUTS", oPar1, "", True, True)
            If oPar1.bPar1 = True Then e.RetValue = CLN__STD.ThMsg.RETVALUE_OK

          Case "APRIANALO:"
            'dopo la creazione automatica di una anagrafica lotti, la apro
            strTmp1 = e.Message.Split("§"c)
            Dim oPar1 As New CLE__CLDP
            Dim oTmp As New Control
            oPar1.strParam = "APRI;" & strTmp1(0).PadRight(CLN__STD.CodartMaxLen) & ";" & strTmp1(1).PadLeft(9, "0"c) & ";"

            oTmp.Text = NTSCStr(grvRighe.NTSGetCurrentDataRow!xxo_lottox)
            NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, False, oPar1)
          Case "SelCatForn" : SelezionaCatalogoFornitori(strTmp(i))
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiDatiDitta() As Boolean
    Dim bAgenti As Boolean = True
    Dim bNoteprel As Boolean = True

    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleBoll.strDittaCorrente = oApp.Ditta
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleBoll.InitExt() Then
        Me.Close()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleBoll.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129670382103398438, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129670382120283204, "Lotto"), 50, True)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non è settata a 1 (ovvero usa le impostazioni della voce di menu) non ha senso visualizzare la voce di menu
      '--------------------------------------------------------------------------------------------------------------
      oCleBoll.bRiapriDocSuSalva = NTSCInt(Val(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Opzioni", ".", "RiapriDocSuSalva", "0", " ", "0"))) 'se abilitata a -1 al salvataggio del documento lo riapre (lo stesso) posizionandosi in testata, se impsotata ad 1 utilizza l'impostazione della voce di menu 'Riapri doc dopo salva'
      nCarProdInVendita = NTSCInt(Val(oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "CarProdInVendita", "0", " ", "0")))
      If oCleBoll.bRiapriDocSuSalva = 1 Then
        GctlSetVisEnab(tlbRiapriDopoSalva, True)
      Else
        tlbRiapriDopoSalva.Visible = False
        If oCleBoll.bRiapriDocSuSalva = -1 Then
          tlbSalvanuovo.Checked = False
          tlbSalvanuovo.Visible = False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CBool(oCleBoll.lModuliDittaDitt And bsModTV) Then tlbIntegraRighe.Visible = False
        '--------------------------------------------------------------------------------------------------------------
        If Not CBool(oCleBoll.lModuliSupDittaDitt And bsModSupEMA) Then tlbEmail.Visible = False
        '--------------------------------------------------------------------------------------------------------------
        '--- Nascondo le voci di menu non gestite
        '--------------------------------------------------------------------------------------------------------------
        If CBool(oCleBoll.lModuliExtDittaDitt And bsModExtTCP) = False And _
           CBool(oCleBoll.lModuliExtDittaDitt And bsModExtTCO) = False Then tlbVisDispVariante.Visible = False
        If CBool(oCleBoll.lModuliDittaDitt And bsModCP) = False And _
           CBool(oCleBoll.lModuliExtDittaDitt And bsModExtCP2) = False Then tlbApriConfiguratore.Visible = False
        '--------------------------------------------------------------------------------------------------------------
        '--- Se non ho agenti nascondo
        '--------------------------------------------------------------------------------------------------------------
        oCleBoll.TabEmpty(DittaCorrente, bAgenti, bNoteprel)
        If bAgenti Then
          lbEt_codagen.Visible = False
          edEt_codagen.Visible = False
          lbXx_codagen.Visible = False
          fmProvvigioni.Visible = False
          ec_provv.Visible = False
          ec_provv2.Visible = False
          ec_vprovv.Visible = False
          ec_vprovv2.Visible = False
          ec_ricimp.Visible = False
        End If
        '--------------------------------------------------------------------------------------------------------------
        '---Forzo la GCTLSETROULES, per liniti impostati per ditta
        '--- prima per impostare eventuali blocchi sul tipo documento
        '--------------------------------------------------------------------------------------------------------------
        GctlTipoDoc = ""
      GctlSetRoules()
      GestisciBarcodeIn()
        '--------------------------------------------------------------------------------------------------------------
        Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)
        '--------------------------------------------------------------------------------------------------------------
        Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub LeggiDisponibilitaArticolo(ByVal strCodart As String, ByVal nMagaz As Integer, _
                                  ByVal nFase As Integer, ByVal lCommeca As Integer, _
                                  Optional ByVal strGescomm As String = "?")
    Dim dDisponibilita As Decimal = 999999999
    Dim dDisponibilitaNetta As Decimal = 999999999
    Dim dOrdinato As Decimal = 999999999
    Dim dImpegnato As Decimal = 999999999
    Dim dUltCost As Decimal = 999999999

    Try
      oCleBoll.Leggidisponibilita(strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, dOrdinato, dImpegnato, 0, strGescomm)
      If dDisponibilita = 999999999 And dDisponibilitaNetta = 999999999 Then
        edDispon.Text = "999999999"
        edDispNetta.Text = "999999999"
        edOrdinato.Text = "999999999"
        edImpegnato.Text = "999999999"
      Else
        edDispon.Text = dDisponibilita.ToString
        edDispNetta.Text = dDisponibilitaNetta.ToString
        edOrdinato.Text = dOrdinato.ToString
        edImpegnato.Text = dImpegnato.ToString
      End If
      If dUltCost = 999999999 Then
        edUltCost.Text = "999999999"
      Else
        edUltCost.Text = dUltCost.ToString
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiPromozioni(ByVal strCodart As String, ByVal strDatdoc As String) As Integer
    Dim oPar As CLE__CLDP
    Try
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.strPar1 = strCodart
      oPar.strPar2 = strDatdoc
      oPar.dPar1 = 0                        'valore di ritorno
      oMenu.RunChild("NTSInformatica", "FRMMGPROA", "", DittaCorrente, "", "BNMGPROA", oPar, "", True, True)
      Return NTSCInt(oPar.dPar1)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function MagazzinoSecondario() As Boolean
    'abilito/disabilito il magaz. 2
    Dim dttTmp As New DataTable
    Try
      lbEt_magaz.Text = oApp.Tr(Me, 128657082841250000, "Magazzino 1")
      lbEt_magaz2.Text = oApp.Tr(Me, 128657082900156250, "Magazzino 2")
      edEt_magaz2.Enabled = False
      If NTSCInt(edEt_causale.Text) <> 0 And cbTipoDoc.SelectedValue <> "T" Then
        If oMenu.ValCodiceDb(NTSCInt(edEt_causale.Text).ToString, DittaCorrente, "TABCAUM", "N", "", dttTmp) Then
          If NTSCInt(dttTmp.Rows(0)!tb_causec) > 0 Then
            GctlSetVisEnab(edEt_magaz2, False)
            If NTSCInt(dttTmp.Rows(0)!tb_esist) = 1 Then
              lbEt_magaz.Text = oApp.Tr(Me, 128776118168912000, "Magaz. carico")
              lbEt_magaz2.Text = oApp.Tr(Me, 128776118237590000, "Magaz. scarico")
            Else
              lbEt_magaz2.Text = oApp.Tr(Me, 128776118182328000, "Magaz. carico")
              lbEt_magaz.Text = oApp.Tr(Me, 128776118249914000, "Magaz. scarico")
            End If
          End If
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ProponiEvasioneOrdini() As Boolean
    Dim dttOr As New DataTable
    Try

      If Not grvRighe.NTSGetCurrentDataRow Is Nothing And oCleBoll.bProponiEvasioneOrdini Then
        If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
          With grvRighe.NTSGetCurrentDataRow
            If NTSCInt(!ec_ornum) = 0 And NTSCStr(!ec_codart) <> "D" And NTSCStr(!ec_codart) <> "M" And _
               NTSCDec(!ec_quant) <> 0 Then
              If oCleBoll.GetOrdiniPerAutoevasione(NTSCStr(!ec_codart), NTSCInt(!ec_fase), _
                                                  NTSCInt(oCleBoll.dttET.Rows(0)!et_conto), _
                                                  NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta), _
                                                  IIf(ckEt_scorpo.Checked, "S", "N").ToString, dttOr) Then
                If dttOr.Rows.Count > 0 Then
                  'chiamo la 'seleziona righe d'ordine
                  grvRighe.Focus()    'altrimenti non passa l'articolo e la qta!!!
                  SelezionaOrdini("R")
                End If
              End If
            End If
          End With
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttOr.Clear()
    End Try
  End Function

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Select Case strIn.Substring(0, 1)
      Case "#"
        'Variabile esposte SPECIFICHE PER PROGRAMMA
        Select Case UCase$(Mid$(strIn, 2))
          Case "TIPORK"
            Return dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString
          Case "ANNO"
            Return dsBoll.Tables("TESTA").Rows(0)!et_anno.ToString
          Case "SERIE"
            Return dsBoll.Tables("TESTA").Rows(0)!et_serie.ToString
          Case "NUMDOC"
            Return dsBoll.Tables("TESTA").Rows(0)!et_numdoc.ToString
          Case Else
            Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else
        Return MyBase.ResolveField(strIn)
    End Select
    Return ""
  End Function

  Public Overridable Function RigaConArticoloOk(ByVal bSuCodart As Boolean) As Boolean
    Try
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116006754000, "Posizionarsi prima su una riga di griglia contenente il 'codice articolo'"))
        Return False
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776116025474000, "Posizionarsi prima su una riga di griglia contenente il 'codice articolo'"))
        Return False
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128689606437812500, "Posizionarsi prima su una riga di griglia con 'codice articolo' impostato"))
        Return False
      End If
      If grvRighe.FocusedColumn.Name <> "ec_codart" And bSuCodart Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128578169100937500, "Posizionarsi prima sulla colonna 'codice articolo'"))
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Ripristina() As Boolean
    Try
      oCleBoll.bInUnload = True

      'annullo la riga in corso di inserimento
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
          oCleBoll.RecordRipristina(dcBollRighe.Position, dcBollRighe.Filter)
        End If
      End If

      If Not oCleBoll.RipristinaDocumento() Then Return False

      GctlSaveConfigGrid()
      SetStato(0, False)
      oCleBoll.bInUnload = False
      edNumDoc.Text = "0"
      edNumDoc.SelectAll()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oCleBoll.bInUnload = False
    End Try
  End Function

  Public Overridable Function RipristinaDocumento() As Boolean
    Try
      If edNumDoc.Enabled Then Return True

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

  Public Overridable Function Salva(ByVal nStampa As Integer, ByVal bAllowCancel As Boolean) As Boolean
    'nStampa = -1: salva solo
    '           0: stampa video
    '           1: stampa carta
    Dim dRes As DialogResult
    Dim strState As String = "U"
    Dim bRiapridoc As Boolean = CheckRiapriDocSuSalva()

    Try
      bCreaCarProd = False
      If edNumDoc.Enabled Then Return True
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      Me.Cursor = Cursors.WaitCursor

      If oCleBoll.bSalvaAncheSeNonModificato And oCleBoll.bNew = False Then oCleBoll.bHasChangesET = True
      If oCleBoll.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False
      End If

      If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return False
      Me.Cursor = Cursors.Default
      If oCleBoll.bNew Then strState = "N"

      strLastPrezzoInbolla = IIf(ckXx_przbol.Checked, "S", "N").ToString

      If oCleBoll.RecordIsChanged Then

        If OperazioneConsentita() = False Then Return False

        If bAllowCancel Then
          dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128776118412186000, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
          If dRes = System.Windows.Forms.DialogResult.No Then Return True
        Else
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128776118430906000, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.No Then Return False
        End If

        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Me.Cursor = Cursors.WaitCursor

          If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return False
          oCleBoll.dttEC.AcceptChanges()
          If Not oCleBoll.SalvaDocumento(strState) Then
            'potrebbe non aver salvato perchè il numero doc è già stato utilizzato ...
            edNumDoc.Text = NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_numdoc).ToString
            Return False
          End If
          GctlSaveConfigGrid()

          '-----------------------------
          'salvo le condizioni particolari LISTINI/SCONTI/PROVV
          If oCleBoll.bSalvaCondizioniFinali Then
            '--------------------------------------------------------------------------------------------------------
            '--- Valuta le nuove opzioni di registro
            '--------------------------------------------------------------------------------------------------------
            Select Case oCleBoll.dttEt_conto.Rows(0)!an_tipo.ToString
              Case "C"
                Select Case oCleBoll.nSalvaCondizioniFinaliClientiDove
                  Case 0 : GoTo SaltaSalvataggioCondizioniFinali
                  Case 1 : If oCleBoll.bDocEmesso = False Then GoTo SaltaSalvataggioCondizioniFinali
                End Select
              Case "F"
                Select Case oCleBoll.nSalvaCondizioniFinaliFornitoriDove
                  Case 0 : GoTo SaltaSalvataggioCondizioniFinali
                  Case 1 : If oCleBoll.bDocEmesso = True Then GoTo SaltaSalvataggioCondizioniFinali
                End Select
            End Select
            '--------------------------------------------------------------------------------------------------------
            'Funzione non abilitata per documenti di produzione interni
            If Not (dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" And oCleBoll.bTerzista = False) Then
              If Not SalvaCondizioniFinali() Then Return False
            Else
              If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 128673202256562500, "Attenzione!" & vbCrLf & vbCrLf & _
                                                "La funzionalità di salvataggio delle condizioni particolari è " & _
                                                "abilitata solo sui carichi di produzioni da terzisti e non interni."))
              End If
            End If
SaltaSalvataggioCondizioniFinali:
          End If


          'se è stata settata l'opzione per creare il carico da prod al salva del documento emesso, predispongo il tutto
          If nCarProdInVendita <> 0 And oCleBoll.bDocEmesso And strState = "N" And _
             NTSCInt(oCleBoll.dttET.Rows(0)!et_magaz2) = 0 And cbTipoDoc.SelectedValue <> "W" Then
            bRiapridoc = False
            bCreaCarProd = True
          End If

        End If
      Else
        'ho aperto un documento ed ora lo stampo senza modficarlo
        If bRiapridoc = False Then
          oCleBoll.DocumentLockRemove()
          GctlSaveConfigGrid()
        End If
      End If

      If bRiapridoc Then
        'non chiudo ma mi posiziono sul corpo sull'ultima riga
        SetStato(1, False)
        GctlSetVisEnab(tlbNuovoDocDaPrec, False)
        NTSFormClearDataBinding(Me)
        oCleBoll.bInRiapriDopoSalva = True
        If oCleBoll.ApriDoc(DittaCorrente, False, cbTipoDoc.SelectedValue.ToString, _
                                        NTSCInt(edAnnoDoc.Text), edSerieDoc.Text, NTSCInt(edNumDoc.Text), _
                                        dsBoll) = False Then Return False
        oCleBoll.bInRiapriDopoSalva = False

        dcBoll.DataSource = dsBoll.Tables("TESTA")
        dcBoll.MoveFirst()
        dcBollc.DataSource = dsBoll.Tables("TESTAC")
        dcBollc.MoveFirst()

        dcBollRighe.DataSource = dsBoll.Tables("CORPO")
        grRighe.DataSource = dcBollRighe

        Bindcontrols()

        dsBoll.AcceptChanges()

        oCleBoll.bNew = False
        tsBoll.SelectedTabPageIndex = 1
        grRighe.Focus()
        grvRighe.MoveLast()
      End If    'If bRiapridoc Then

      If strState = "N" Then
        'salvo gli estremi dell'ultimo documento fatto. mi serviranno nel caso du 'nuovo da altro' per proporre anno e serie del nuovo documento
        oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "LastDocNewAnno", edAnnoDoc.Text, " ", "NS.", "...", "...")
        'oMenu.SaveSettingBus("BNVEBOLL", "Recent", ".", "LastDocNewSerie", edSerieDoc.Text, " ", "NS.", "...", "...")
      End If

      If strState = "N" And CBool(oCleBoll.lModuliDittaDitt And bsModSQ) Then
        If oCleBoll.dttEC.Select("ec_flcom = 'S' AND ec_flelab = 'N'").Length > 0 Then
          'dovrò dare il messaggio se entrare nelle schede collaudo
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129394953846376954, "Nel documento sono presenti righe da collaudare: aprire la gestione collaudi?")) = Windows.Forms.DialogResult.Yes Then
            Dim strParam As String = ""
            'Apro la gestione collaudi
            strParam = "VEBOLLAPRI;" & oCleBoll.dttET.Rows(0)!et_tipork.ToString & ";" & _
                       oCleBoll.dttET.Rows(0)!et_anno.ToString & ";" & _
                       oCleBoll.dttET.Rows(0)!et_serie.ToString & ";" & _
                       NTSCInt(oCleBoll.dttET.Rows(0)!et_numdoc).ToString.PadLeft(9)
            'chiamo volutamente solo il programma in NET, visto che quello in VB6 lavora diversamente ...
            oMenu.RunChild("NTSInformatica", "FRMSQCOLL", "", DittaCorrente, "", "BNSQCOLL", Nothing, strParam, True, True)
            Return True
          End If
        End If
      End If    'If CBool(oCleBoll.lModuliDittaDitt And bsModSQ) Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleBoll.bInRiapriDopoSalva = False
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function CheckRiapriDocSuSalva() As Boolean
    CheckRiapriDocSuSalva = False
    Try
      If bCreaCarProd Then Return False
      If oCleBoll.bRiapriDocSuSalva = -1 Then
        Return True
      ElseIf oCleBoll.bRiapriDocSuSalva = 1 And tlbRiapriDopoSalva.Checked = True Then
        Return True
      End If

      Return False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub SelezionaOrdini(ByVal strTipo As String)
    'strTipo: T = testate, R = righe, B = righe con barcode
    Dim lCodConto As Integer = 0
    Dim oPar As New CLE__PATB
    Dim dQuant As Decimal = 0
    Dim strCodart As String = ""
    Dim nFase As Integer = 0

    Try
      NTSZOOM.strIn = ""

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If NTSCInt(edEt_conto.Text) = 0 And strTipo = "T" Then
        'non ho indicato il cliente / fornitore: il controllo sui dati di testata lo eseguirò dopo aver impostato il conto preso dal primo ordine
      Else
        If Not oCleBoll.OkTestata() Then
          tsBoll.Focus()
          Return
        End If
      End If

      If (oCleBoll.lConclpriv = NTSCInt(oCleBoll.dttET.Rows(0)!et_conto)) And (oCleBoll.lConclpriv <> 0) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776115238328000, "Impossibile acquisire ordini per un cliente generico privato."))
        Return
      End If

      '-----------------------
      'se è una nuova riga con o senza codice articolo proseguo, se è una riga già precedentemente salvata prima la aggiorno
      If strTipo = "T" Then
        If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
      Else

        '--------------------------------
        'seleziona ordini con barcode
        If strTipo = "B" Then
          If cbTipoDoc.SelectedValue = "T" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128709715844375000, "Funzionalità non disponibile sui documenti di tipo 'Carico di produzione'."))
            Return
          End If
          If Not oCleBoll.bNew Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128709719253906250, "Questa funzione è disponibile solo sui nuovi documenti."))
            Return
          End If
          If oCleBoll.dttEC.Rows.Count > 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128709719617187500, "Questa funzione è disponibile solo quando non ci sono righe già inserite sul corpo dei documenti."))
            Return
          End If
        End If    'If strTipo = "B" Then

        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          If oCleBoll.dttEC.Rows(dcBollRighe.Position).RowState = DataRowState.Added Then
            'non devo fare nulla
          ElseIf oCleBoll.dttEC.Rows(dcBollRighe.Position).RowState = DataRowState.Unchanged Then
            'non devo fare nulla
          Else
            If Not oCleBoll.RecordSalva(dcBollRighe.Position, False, Nothing) Then Return
          End If
        End If
      End If

      '------------------------
      'chiedo il cliente/fornitore
      If oCleBoll.bChiediContoOrdini And strTipo <> "B" Then
        oPar.bVisGriglia = True
        oPar.bTipoProposto = True
        oPar.nMastro = 0
        If oCleBoll.IsDocumentoEmesso(dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString(), False) Then
          oPar.strTipo = "C"
        Else
          oPar.strTipo = "F"
        End If
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oPar)
        If NTSZOOM.strIn <> "" Then
          lCodConto = NTSCInt(NTSZOOM.strIn)
        Else
          Return
        End If
      Else
        lCodConto = NTSCInt(edEt_conto.Text)
      End If


      Select Case strTipo
        Case "T"
          '------------------------
          'Chiama lo zoom testate ordini
          oPar.strDescr = oCleBoll.GetWhereHltd(lCodConto)
          oPar.nTipologia = 1       '0 = posso selez una sola riga, 1 posso fare multiselezione
          NTSZOOM.ZoomStrIn("ZOOMTESTORD", DittaCorrente, oPar)

        Case "R"
          '------------------------
          'Chiama lo zoom righe ordini

          'Codice articolo e q.ta da passare
          If grRighe.Focused Then
            If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
              If grvRighe.FocusedColumn.Name = "ec_codart" Then grvRighe.NTSMoveNextColunn()
              strCodart = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
              nFase = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
              dQuant = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
            End If
          End If

          '------------------------
          'Chiama lo zoom ordini aperti
          oPar.strDescr = oCleBoll.GetWhereHlmo(lCodConto)
          oPar.strTipo = oCleBoll.dttET.Rows(0)!et_tipork.ToString
          oPar.lContoCF = lCodConto
          oPar.strCodart = strCodart
          oPar.nFase = nFase
          oPar.dImporto = dQuant
          oPar.lCommessa = 0
          oPar.bFlag1 = oCleBoll.bNew
          oPar.nAnno = NTSCInt(edAnnoDoc.Text)
          oPar.strAlfpar = edSerieDoc.Text
          oPar.lNumreg = NTSCInt(edNumDoc.Text)
          oPar.nTipologia = 2                     '0 solo visualizzaz, 2 = possibilità di selez le righe
          If oCleBoll.bConsMultiEvasRigheOrdNote = False Then
            'Chiama lo zoom righe ordini "statisco"
            oPar.oParam = Nothing
          Else
            'Chiama lo zoom righe ordini "dinamico"
            oPar.oParam = oCleBoll.dttEC            'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
          End If
          oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
          NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo
          If oPar Is Nothing Then Return
          If oPar.oParam Is Nothing Then Return
          CType(oPar.oParam, DataTable).AcceptChanges()

        Case "B"
          '------------------------
          'Chiama lo zoom ordini aperti
          oPar.strDescr = oCleBoll.GetWhereHlmo(lCodConto)
          oPar.nAnno = NTSCInt(edEt_codpaga.Text)
          oPar.nEscomp = NTSCInt(edEt_coddest.Text)
          oPar.lContoCF = lCodConto
          oPar.strAlfpar = oCleBoll.dttET.Rows(0)!et_scorpo.ToString
          oPar.nValuta = NTSCInt(edEt_valuta.Text)
          oPar.strTipo = cbTipoDoc.SelectedValue
          NTSZOOM.ZoomStrIn("ZOOMMOVORDBARCODE", DittaCorrente, oPar)        'in vb6 chiama bsvebolx
          If oPar.oParam Is Nothing Then Return

      End Select

      'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      If strTipo <> "T" Then
        '-----------------------
        'se è una nuova riga la annullo, verrà reinserita nella ImportaOrdini
        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          If oCleBoll.dttEC.Rows(dcBollRighe.Position).RowState = DataRowState.Added Then
            oCleBoll.RecordRipristina(dcBollRighe.Position, dcBollRighe.Filter)
          End If
        End If
      End If

      '------------------------
      'ho selezionato qualche cosa: ESEGUO L'IMPORT DELLE RIGHE
      oCleBoll.ImportaOrdini(strTipo, CType(oPar.oParam, DataTable))

      'si posiziona sull'ultima riga
      grvRighe.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function SetBlocchiRetail(ByVal bReset As Boolean) As Boolean
    'se ho aperto un documento creato con il retail nuovo, non posso modificare 
    'in alcun modo i valori di riga e totali documento, visto che influirebbero
    'sulla gestione delle promozioni
    Dim bBlocca As Boolean = False
    Try
      If bReset Then
        GctlSetConfigGrid()
        bCa2InCG = False
        GctlSetVisEnab(tlbRettificaTotdoc, False)
        'GctlSetVisEnab(tlbNuovoDocDaPrec, False)
        GctlSetVisEnab(tlbRicalcolaColliQta, False)
        GctlSetVisEnab(tlbInsRiga, False)
        GctlSetVisEnab(tlbSelOrdini, False)
        GctlSetVisEnab(tlbSelRigheOrdini, False)

        GctlSetVisEnab(tlbRigheOrdBarcode, False)
        GctlSetVisEnab(tlbSelezAcconti, False)
        GctlSetVisEnab(tlbScaricaMagaz, False)
        GctlSetVisEnab(tlbIntegraRighe, False)
        GctlSetVisEnab(tlbSelezDaTerm, False)
        GctlSetVisEnab(tlbDaLista, False)
        GctlSetVisEnab(tlbImport, False)
        GctlSetVisEnab(tlbSelectDeselect, False)
        GctlSetVisEnab(tlbGiub, False)
        GctlSetVisEnab(tlbSiub, False)

        GctlSetVisEnab(edet_datdoc, False)
        GctlSetVisEnab(edEt_conto, False)
        GctlSetVisEnab(edEt_codpaga, False)
        GctlSetVisEnab(edEt_scopag, False)
        GctlSetVisEnab(edEt_datapag, False)
        GctlSetVisEnab(edEt_scopag, False)
        GctlSetVisEnab(edEt_scont1, False)
        GctlSetVisEnab(edEt_scont2, False)
        GctlSetVisEnab(ckEt_scorpo, False)
        GctlSetVisEnab(ckEt_flspinc, False)
        GctlSetVisEnab(ckEt_flboll, False)
        GctlSetVisEnab(edEt_codese, False)
        GctlSetVisEnab(edEt_listino, False)
        GctlSetVisEnab(edEt_speacc, False)
        GctlSetVisEnab(edEt_speaccv, False)
        GctlSetVisEnab(edEt_speimb, False)
        GctlSetVisEnab(edEt_speimbv, False)
        GctlSetVisEnab(edEt_abbuono, False)
        GctlSetVisEnab(edEt_abbuonov, False)
        GctlSetVisEnab(edEt_pagato, False)
        GctlSetVisEnab(edEt_pagatov, False)
        grvRighe.NTSAllowInsert = True
        GctlSetVisEnab(edEt_tipobf, False)
        GctlSetVisEnab(edEt_causale, False)
        GctlSetVisEnab(edEt_magaz, False)
        GctlSetVisEnab(edEt_magaz2, False)
        Return True
      End If

      If oCleBoll.dttETC.Rows.Count = 0 Then Return True

      '----------------------------------
      'controllo ed eventualmente avviso se il documento deve essere bloccato 
      'perchè contiene collegamenti di CA2 e documento già contabilizzato
      'oppure perchè la CA2 è stata congelata a data successiva a quella del documento
      'lo stesso test verrà fatto anche in fase di salvataggio documento!!!!
      If CBool(oCleBoll.lModuliSupDittaDitt And bsModSupCAE) Then
        oCleBoll.TestPreCancellaTestmag_CheckCa2(True, True, bCa2InCG)
        If bCa2InCG Then bBlocca = True
      End If

      '----------------------------------
      'controlli per documento realizzato con retail
      If oCleBoll.dttETC IsNot Nothing AndAlso oCleBoll.dttETC.Rows.Count > 0 AndAlso NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) = "S" Then

        'test per documento fatto con nuovo retail (direttamente in testmagc)
        oApp.MsgBoxInfo(oApp.Tr(Me, 129588241428593750, "Attenzione!" & vbCrLf & _
                            "Il documento selezionato è stato creato con il programma" & vbCrLf & _
                            "'Gestione unificata documenti'." & vbCrLf & _
                            "Non sarà possibile modificare tutto ciò che potrebbe influire " & vbCrLf & _
                            "sulla determinazione dei valori del documento." & vbCrLf & _
                            "Alcune informazione specifiche del retail non saranno visualizzate"))
        bBlocca = True
      End If    'If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) <> "S" Then
PASSA:

      If bBlocca Then
        tlbRettificaTotdoc.Enabled = False
        'If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) = "S" Then tlbNuovoDocDaPrec.Enabled = False
        tlbRicalcolaColliQta.Enabled = False
        tlbInsRiga.Enabled = False
        tlbSelOrdini.Enabled = False
        tlbSelRigheOrdini.Enabled = False

        tlbRigheOrdBarcode.Enabled = False
        tlbSelezAcconti.Enabled = False
        tlbScaricaMagaz.Enabled = False
        tlbIntegraRighe.Enabled = False
        tlbSelezDaTerm.Enabled = False
        tlbDaLista.Enabled = False
        tlbImport.Enabled = False
        'tlbSelectDeselect.Enabled = False
        tlbGiub.Enabled = False
        tlbSiub.Enabled = False

        edet_datdoc.Enabled = False 'se cambio la data dopo che un corrispettivo è stato portato in CG raggruppato succede un macello nella ricontabilizzazione !!!!
        edEt_conto.Enabled = False
        edEt_codpaga.Enabled = False
        edEt_scopag.Enabled = False
        edEt_datapag.Enabled = False
        edEt_scopag.Enabled = False
        edEt_scont1.Enabled = False
        edEt_scont2.Enabled = False
        ckEt_scorpo.Enabled = False
        ckEt_flspinc.Enabled = False
        ckEt_flboll.Enabled = False
        edEt_codese.Enabled = False
        edEt_listino.Enabled = False
        edEt_speacc.Enabled = False
        edEt_speaccv.Enabled = False
        edEt_speimb.Enabled = False
        edEt_speimbv.Enabled = False
        edEt_abbuono.Enabled = False
        edEt_abbuonov.Enabled = False
        edEt_pagato.Enabled = False
        edEt_pagatov.Enabled = False
        grvRighe.NTSAllowInsert = False
        'spostati in ingresso di griglia
        'ec_codart.Enabled = False
        'ec_fase.Enabled = False
        'ec_unmis.Enabled = False
        'ec_colli.Enabled = False
        'ec_quant.Enabled = False
        'ec_prezzo.Enabled = False
        'ec_prezvalc.Enabled = False
        'ec_scont1.Enabled = False
        'ec_scont2.Enabled = False
        'ec_scont3.Enabled = False
        'ec_scont4.Enabled = False
        'ec_scont5.Enabled = False
        'ec_scont6.Enabled = False
        'ec_codiva.Enabled = False
        'ec_stasino.Enabled = False

        'ulteriori blocchi per registrazione di CA già riportata in CG: non devo poter modificare
        'le dimensioni di CA, quantità, importi, valori, date competenza
        If bCa2InCG Then
          edEt_tipobf.Enabled = False
          edEt_causale.Enabled = False
          edEt_magaz.Enabled = False
          edEt_magaz2.Enabled = False
          'spostati in ingresso di griglia
          'ec_datini.Enabled = False
          'ec_datfin.Enabled = False
          'commessa, centro, linea, divisione
        End If
      End If    'If bBlocca Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nStato As Integer, ByVal bRicaricaGctlInStato1 As Boolean)
    '0 = nessun documento aperto
    '1 = sono dentro ad un documento, nuovo o in modifica
    Dim i As Integer = 0
    Dim strSerieOld As String = ""
    Dim strTipoProg As String = ""
    Try
      Select Case nStato
        Case 0
          'memorizzo l'ultimo documento trattato
          If NTSCInt(edNumDoc.Text) > 0 Then
            If strLastTiporktmp <> "" Then
              'ho creto un CP al salva di un documento emesso: ripropongo il tipork del documento emesso
              cbTipoDoc.SelectedValue = strLastTiporktmp
              strLastTiporktmp = ""
            Else
              strLastTipork = cbTipoDoc.SelectedValue.ToUpper
              nLastAnno = NTSCInt(edAnnoDoc.Text)
              strLastSerie = edSerieDoc.Text
              lLastNumdoc = NTSCInt(edNumDoc.Text)
            End If
          End If

          If tsBoll.ContainsFocus Then edEt_conto.Focus() 'diversamente quando salvo impostando come tabpageindex = 0 viene simulata la pressione del TAB per entrare nel primo campo da bn__chil ed il risultato è che con multireport il focus va su ANNULLA invece che rimanere su OK
          tsBoll.SelectedTabPageIndex = 0
          tsBoll.Visible = False

          'blocchi se ho aperto un documento proveniente da retail nuovo o CA DC
          If oCleBoll.bModGPV Or CBool(oCleBoll.lModuliSupDittaDitt And bsModSupCAE) Then SetBlocchiRetail(True)

          GctlSetVisEnab(cbTipoDoc, True)
          GctlSetVisEnab(cbTipoDoc, False)
          GctlSetVisEnab(edAnnoDoc, False)
          GctlSetVisEnab(edSerieDoc, False)
          GctlSetVisEnab(edNumDoc, False)

          If edAnnoDoc.Text = "" Then edAnnoDoc.Text = DateTime.Now.Year.ToString
          If edSerieDoc.Text = "" Then edSerieDoc.Text = " "
          If edNumDoc.Text = "" Then edNumDoc.Text = "0"
          strLastColEdited = ""

          '----------------------------------------------
          'menu e toolbar
          tlbNuovoDocDaPrec.Enabled = False
          tlbDatiContabiliSintetici.Enabled = False
          tlbModifNumDoc.Enabled = False
          tlbNavigazDoc.Enabled = False
          tlbMnuFile.Enabled = False
          tlbMnuRecord.Enabled = False
          tlbSalva.Enabled = False
          tlbCancella.Enabled = False
          tlbRipristina.Enabled = False
          tlbRecordCancella.Enabled = False
          tlbRecordRipristina.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tlbRegCassa.Enabled = False
          tlbStampaPdf.Enabled = False
          tlbStampaEtichetteRiga.Enabled = False
          tlbSelRigheOrdini.Enabled = False
          tlbSelOrdini.Enabled = False
          If bDaTastiNavigazione = False Then
            tlbApriDocPrec.Enabled = False
            tlbApriDocSucc.Enabled = False
          End If

          If Not CBool(oCleBoll.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
            tlbEmail.Visible = False
          End If
          tlbEmail.Enabled = False

          GctlSetVisEnab(tlbNuovo, False)
          GctlSetVisEnab(tlbApri, False)

          oCleBoll.bNew = False
          tsBoll.SelectedTabPageIndex = 0

          GctlSetVisEnab(edEt_valuta, False)
          GctlSetVisEnab(ckEt_scorpo, False)

          SetUnboundFiled(0)
          edNumDoc.SelectAll()
          If Me.ContainsFocus Then
            edNumDoc.Focus()
          Else
            'non devo fare nulla, per gestire il seguente caso:
            'stampa etichette finale, apro un doc e stampo a video: anteprima sopra a veboll
            'si apre stampa etichette sopra l'antepria. faccio anteprima di etichette, chiudo anteprima rimante etichette sopra a anteprima doc
            'chiudo etichette: in questo caso se faccio fare la edNumdoc.Focus appare veboll sopra a anteprima, senza òlafocus rimane sopra l'anteprima
          End If

          grvRighe.NTSAllowInsert = True
          grvRighe.NTSAllowUpdate = True

          ckEt_flscdb.Enabled = False

          GctlSetVisEnab(edEt_codagen, False)
          GctlSetVisEnab(fmPa, False)

          cmdDatiTesta1_Click(cmdDatiTesta1, Nothing)
          cmdPiedeTotali_Click(cmdPiedeTotali, Nothing)

          tlbAggCorrFF.Enabled = False
        Case 1
          tsBoll.Visible = True

          cbTipoDoc.Enabled = False
          edAnnoDoc.Enabled = False
          edSerieDoc.Enabled = False
          edNumDoc.Enabled = False 'devo poter dare la possibilità di cambiare numero doc quando faccio 'nuovo': no: crea problemi con impegni collegati ... usare la funzione 'cambia numero doc'

          '-------------------------------
          'applico le impostazioni da GCTL ed i valori di default: 
          'la griglia deve gi essere collegata al datatable
          'GIA' FATTO IN PRECEDENZA!!!!!!! bRicaricaGctlInStato1 sempre = false
          'GctlTipoDoc = cbTipoDoc.SelectedValue().ToString
          'If bRicaricaGctlInStato1 Then GctlSetRoules()
          If oCleBoll.bNew Then
            'se ho impostato tramite GCTL che in nuovo la serie deve essere una specifica, devo ricalcolare il numero documento
            strSerieOld = edSerieDoc.Text
            GctlApplicaDefaultValue()
            If strSerieOld <> edSerieDoc.Text Then
              Select Case cbTipoDoc.SelectedValue
                Case "A", "D", "E", "N", "£" : strTipoProg = "A"
                Case "B" : strTipoProg = "B"
                Case "C" : strTipoProg = "C"
                Case "M" : strTipoProg = "M"
                Case "Z" : strTipoProg = "Z"
                Case "L", "K", "J", "(" : strTipoProg = "L"
                Case "W" : strTipoProg = "W"
                Case "T" : strTipoProg = "T"
                Case "U" : strTipoProg = "U"
                Case "F", "I" : strTipoProg = "F"
                Case "S" : strTipoProg = "S"
              End Select
              edNumDoc.Text = oCleBoll.LegNuma(strTipoProg, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text)).ToString
              oCleBoll.CambiaNumdoc(dsBoll, cbTipoDoc.SelectedValue, NTSCInt(edNumDoc.Text), edSerieDoc.Text)
            End If
          End If

          '----------------------------------------------
          'menu e toolbar
          GctlSetVisEnab(tlbMnuFile, False)
          GctlSetVisEnab(tlbSalva, False)
          GctlSetVisEnab(tlbCancella, False)
          GctlSetVisEnab(tlbRipristina, False)
          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          GctlSetVisEnab(tlbRegCassa, False)
          GctlSetVisEnab(tlbStampaPdf, False)
          GctlSetVisEnab(tlbDatiContabiliSintetici, False)
          If Not oCleBoll.bNew Then GctlSetVisEnab(tlbNuovoDocDaPrec, False)
          If oCleBoll.bNew Then GctlSetVisEnab(tlbModifNumDoc, False)
          GctlSetVisEnab(tlbNavigazDoc, False)
          GctlSetVisEnab(tlbSelRigheOrdini, False)
          GctlSetVisEnab(tlbSelOrdini, False)

          If CBool(oCleBoll.ModuliSupDittaDitt(DittaCorrente) And bsModSupEMA) Then
            GctlSetVisEnab(tlbEmail, False)
            GctlSetVisEnab(tlbEmail, True)
          End If


          '-------------------------------------------------
          'nascondo le voci di menu non gestite
          If cbTipoDoc.SelectedValue.ToString <> "T" Then
            tlbScarCollegati.Visible = False
            tlbLavCollegate.Visible = False
            tlbApriDB.Visible = False
          Else
            GctlSetVisEnab(tlbScarCollegati, True)
            GctlSetVisEnab(tlbLavCollegate, True)
            GctlSetVisEnab(tlbApriDB, True)
          End If

          tlbNuovo.Enabled = False
          tlbApri.Enabled = False

          '----------------------------------------------
          'Le riemissioni sono particolari
          If cbTipoDoc.SelectedValue.ToString = "I" Then
            GctlSetVisEnab(tlbRicFiscDaRiem, False)
            tlbMnuRecord.Enabled = False
            GctlSetVisEnab(tlbRicFiscDaRiem, False)
            tlbRecordRipristina.Enabled = False
            tlbRettificaTotdoc.Enabled = False
            tlbSelOrdini.Enabled = False
            tlbSelRigheOrdini.Enabled = False

            grvRighe.NTSAllowInsert = False
            grvRighe.NTSAllowUpdate = False
          Else
            tlbRicFiscDaRiem.Enabled = False
          End If

          If oCleBoll.bAbilOrdiniBarcode Then
            GctlSetVisEnab(tlbRigheOrdBarcode, False)
          Else
            tlbRigheOrdBarcode.Enabled = False
          End If

          MagazzinoSecondario()
          grvRighe.FocusedRowHandle = 0

          If oCleBoll.bModPM And oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
            edEt_valuta.Enabled = False
            edEt_cambio.Enabled = False
            edEt_valuta.Text = "0"
          End If

          'sblocco castelletti
          If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "L" Or oCleBoll.dttET.Rows(0)!et_tipork.ToString = "J" Then
            GctlSetVisEnab(ckEt_flscdb, False)
          End If
          ckEt_flscdb_CheckedChanged(ckEt_flscdb, Nothing)

          If oCleBoll.bNew = False And oCleBoll.bBloccaAgente1 Then edEt_codagen.Enabled = False

          '----------------------------------------------------
          'Se il documento NON è di tipo D.D.T emesso ('B')
          'il CheckBox 'Stampa prezzi in bolla' è sempre disabilitato
          If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "B" Then
            GctlSetVisEnab(ckXx_przbol, False)
          Else
            ckXx_przbol.Enabled = False
            ckXx_przbol.Checked = False
          End If

          If oCleBoll.bModCP2 Then
            GctlSetVisEnab(tlbApriConfiguratore, True)
          Else
            tlbApriConfiguratore.Visible = False
          End If

          'disabilito la gestione di CUP e CIG per i tipi documento non gestiti
          If cbTipoDoc.SelectedValue.ToString = "Z" Or cbTipoDoc.SelectedValue.ToString = "T" Or cbTipoDoc.SelectedValue.ToString = "I" Then
            fmPa.Enabled = False
          Else
            GctlSetVisEnab(fmPa, False)
          End If

          VisualizzaColonne()

          'Se è una fattura figurativa viene mostrato il pulsante per selezionare i corrispettivi
          If oCleBoll.IsFatturaFigurativa(cbTipoDoc.SelectedValue, edAnnoDoc.TextInt, edSerieDoc.Text) Then
            GctlSetVisEnab(tlbAggCorrFF, True)
            GctlSetVisEnab(tlbAggCorrFF, False)
            If oCleBoll.bInEvasioneDaFattFigurativa Then
              grvRighe.Enabled = False
              tlbRecordCancella.Enabled = False
              grvRighe.NTSAllowInsert = False
            End If
          Else
            tlbAggCorrFF.Enabled = False
          End If
      End Select

      grvRighe.LeftCoord = 0
      If ec_matric.Visible And oCleBoll.bDisabilitaDecodificaBarcode = False Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SettaCampiValuta()
    If edEt_valuta.Text <> "0" Then
      GctlSetVisEnab(edEt_cambio, False)
      GctlSetVisEnab(edEt_speaccv, False)
      GctlSetVisEnab(edEt_speimbv, False)
      GctlSetVisEnab(edEt_pagatov, False)
      GctlSetVisEnab(edEt_abbuonov, False)
      ec_prezvalc.Visible = True

      ckEt_scorpo.Enabled = False
      ckEt_scorpo.Checked = False

      GctlSetVisEnab(lbValuta, True)
      GctlSetVisEnab(lbValuta1, True)
      GctlSetVisEnab(edEt_speaccv, True)
      GctlSetVisEnab(edEt_speimbv, True)
      GctlSetVisEnab(edEt_speincv, True)
      GctlSetVisEnab(edEt_bolliv, True)
      GctlSetVisEnab(edEt_totmercev, True)
      GctlSetVisEnab(edTotSpesev, True)
      GctlSetVisEnab(edTotImponibv, True)
      GctlSetVisEnab(edTotIvav, True)
      GctlSetVisEnab(edEt_totdocv, True)
      GctlSetVisEnab(edEt_totomagv, True)
      GctlSetVisEnab(edTotalev, True)
      GctlSetVisEnab(edEt_pagatov, True)
      GctlSetVisEnab(edEt_abbuonov, True)
      GctlSetVisEnab(edResiduov, True)
      ec_prezzo.Enabled = False
      ec_preziva.Enabled = False
    Else
      edEt_cambio.Enabled = False
      edEt_speaccv.Enabled = False
      edEt_speimbv.Enabled = False
      edEt_pagatov.Enabled = False
      edEt_abbuonov.Enabled = False
      ec_prezvalc.Visible = False

      lbValuta.Visible = False
      lbValuta1.Visible = False
      edEt_speaccv.Visible = False
      edEt_speimbv.Visible = False
      edEt_speincv.Visible = False
      edEt_bolliv.Visible = False
      edEt_totmercev.Visible = False
      edTotSpesev.Visible = False
      edTotImponibv.Visible = False
      edTotIvav.Visible = False
      edEt_totdocv.Visible = False
      edEt_totomagv.Visible = False
      edTotalev.Visible = False
      edEt_pagatov.Visible = False
      edEt_abbuonov.Visible = False
      edResiduov.Visible = False
      If ckEt_scorpo.Checked Then
        GctlSetVisEnab(ec_preziva, False)
      Else
        GctlSetVisEnab(ec_prezzo, False)
      End If
      If ckEt_scorpo.Checked = False And dsBoll.Tables("CORPO").Rows.Count = 0 Then
        If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And oCleBoll.bModPM Then
          'no docu in valuta 
        Else
          GctlSetVisEnab(edEt_valuta, False)
        End If
      End If
      If dsBoll.Tables("CORPO").Rows.Count = 0 Then
        GctlSetVisEnab(ckEt_scorpo, False)
      End If
    End If

    'forzo la routine 'sblocca castelletti' altrimenti se la valuta era 0, spunto sblocca castelletti poi cambio valuta i campi in valuta rimangono bloccati
    ckEt_flscdb_CheckedChanged(ckEt_flscdb, Nothing)
  End Sub

  Public Overridable Sub SetUnboundFiled(ByVal nStato As Integer)
    '0= reset per nuovo / reset per chisura documento
    '1= compilo i campi con il documento aperto
    Try
      If nStato = 0 Then
        edSconto.NTSTextDB = "0"
        edPreList.NTSTextDB = "0"
        edUltCost.NTSTextDB = "0"
        edDispon.NTSTextDB = "0"
        edDispNetta.NTSTextDB = "0"
        edOrdinato.NTSTextDB = "0"
        edImpegnato.NTSTextDB = "0"
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
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    'nDestin: 0 = video, 1 = carta
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim strNumcliPVR As String

    Dim strKey2 As String = ""
    Dim strReportName As String = "BSVEBOLL.rpt"

    Try
      '--------------------------------------------------
      'Non si possono stampare gli impegni di produzione
      If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "U" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128659597701562500, "Non si possono stampare documenti di tipo 'Scarichi a produzione'"))
        Return
      End If

      If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
        strKey2 = "Reports3"
      ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
        strKey2 = "Reports2"
      Else
        strKey2 = "Reports1"
      End If

      '--------------------------------------------------
      'Preimposta il nome del report da stampare
      Select Case dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString
        Case "A", "N", "E", "C", "J", "L"
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
            strReportName = "BSVEFATV.RPT"
          ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
            strReportName = "BSVEFATC.RPT"
          Else
            strReportName = "BSVEFATI.RPT"
          End If
        Case "B", "M", "Z", "T"
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
            strReportName = "BSVEBOLV.RPT"
          ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
            strReportName = "BSVEBOLC.RPT"
          Else
            strReportName = "BSVEBOLL.RPT"
          End If
        Case "W"
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
            strReportName = "BSVEPRBV.RPT"
          ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
            strReportName = "BSVEPRBC.RPT"
          Else
            strReportName = "BSVEPRBN.RPT"
          End If
        Case "F", "I"
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
            strReportName = "BSVERIFV.RPT"
          ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
            strReportName = "BSVERIFC.RPT"
          Else
            strReportName = "BSVERIFI.RPT"
          End If
        Case "S"
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
            strReportName = "BSVEFRFV.RPT"
          ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
            strReportName = "BSVEFRFC.RPT"
          Else
            strReportName = "BSVEFRFI.RPT"
          End If
      End Select

      '--------------------------------------------------
      strNumcliPVR = ""
      If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
        If oCleBoll.bGestPVR = True Then
          strNumcliPVR = oMenu.GetSettingBusDitt(DittaCorrente, "OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
          For i = 1 To (6 - Len(strNumcliPVR))
            strNumcliPVR = "0" & strNumcliPVR
          Next
          'per un fattore di velocità non testo la tabella valuta:
          'se sono in valuta presuppongo che sia l'unica ammessa
          If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) = 0 Then
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
          Else
            strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
          End If
          strNumcliPVR = strNumcliPVR & CLN__STD.calcolaPVRCheck(strNumcliPVR)
        End If
      End If

      '--------------------------------------------------
      'eseguo delle query libere prima della stampa (query memorizzate in regprop di arcproc)
      oCleBoll.RunQueryBeforePrint("BSVEBOLL")

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{MOVMAG.codditt} = '" & DittaCorrente & "'" & _
                " And {MOVMAG.mm_anno} = " & dsBoll.Tables("TESTA").Rows(0)!et_anno.ToString & _
                " AND {MOVMAG.mm_numdoc} = " & dsBoll.Tables("TESTA").Rows(0)!et_numdoc.ToString & _
                " AND {MOVMAG.mm_serie} = '" & dsBoll.Tables("TESTA").Rows(0)!et_serie.ToString & "'" & _
                " AND {MOVMAG.mm_tipork} = '" & dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString & "'" & _
                " AND {MOVMAG.mm_stasino} <> 'N'"
      If oCleBoll.bUsaKeyMag Then strCrpe = strCrpe & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "B" Then strCrpe = strCrpe & " AND {MOVMAG.mm_stasino}<>'D'"

      nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBOLL", strKey2, dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                                      0, nDestin, strReportName, False, "Stampa documento", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "B" Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZOINBOLLA", "'" & strLastPrezzoInbolla & "'")
        End If
        If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
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

      '--------------------------------------------------
      'stampo su reg. di cassa se è stata settata l'opzione di registro in BSVEBOLL/opzioni o BSVEBOLL/opzionidoc
      If oCleBoll.bDopoStpChiamaStpRegCassa Then FileRegcassa(True)

      If tlbStampaEtichetteFinale.Checked Then
        ApriEtichette(1)
      End If

      'stampo la scheda di trasporto se presente
      If oCleBoll.dttSCHETRASP.Rows.Count > 0 Then
        strKey2 = "Reports4"
        strReportName = "BSVESCTR.RPT"
        strCrpe = "{SCHETRASP.codditt} = '" & DittaCorrente & "'" & _
          " And {SCHETRASP.sct_anno} = " & dsBoll.Tables("TESTA").Rows(0)!et_anno.ToString & _
          " AND {SCHETRASP.sct_numdoc} = " & dsBoll.Tables("TESTA").Rows(0)!et_numdoc.ToString & _
          " AND {SCHETRASP.sct_serie} = '" & dsBoll.Tables("TESTA").Rows(0)!et_serie.ToString & "'" & _
          " AND {SCHETRASP.sct_tipork} = '" & dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString & "'"

        nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBOLL", strKey2, dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                                        0, nDestin, strReportName, False, "Stampa scheda di trasporto", False)
        If nPjob Is Nothing Then Return

        '--------------------------------------------------
        'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
        For i = 1 To UBound(CType(nPjob, Array), 2)
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
          For j = 3 To 12
            If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
              nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
            End If
          Next j
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Next

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SalvaCondizioniFinali() As Boolean
    '---------------------------------
    'salvo le condizioni particolari LISTINI/SCONTI/PROVV
    'Dim oPar As New CLE__CLDP
    Dim dttCopa As New DataTable
    Dim nCodlavo As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dPrezzo As Decimal = 0
    Dim i As Integer = 0

    Dim oCleCopa As CLEVECOPA = Nothing
    Try
      '------------------------------
      'controlli preliminari
      If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And Not oCleBoll.bTerzista Then
        'oApp.MsgBoxErr(oApp.Tr(Me, 128576533131562500, "Funzione abilitata solo su carichi da produzione a terzisti e non interni."))
        Return True
      End If


      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEBOLL", "BEVECOPA", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776114517034000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleCopa = CType(oTmp, CLEVECOPA)
      '------------------------------------------------
      AddHandler oCleCopa.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleCopa.Init(oApp, oScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

      '--------------------------------------------------------------------------------------------------------------
      '--- Legge l'opzione di registro per chiedere o meno conferma
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "Conf_salv_cond_part", "S", " ", "S").ToUpper = "S" Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128673206501093750, _
          "Procedere con il salvataggio delle condizioni particolari" & vbCrLf & _
          "(prezzi, sconti, provvigioni)" & vbCrLf & _
          "su tutte le righe del documento?")) = Windows.Forms.DialogResult.No Then Return True
      End If
      '--------------------------------------------------------------------------------------------------------------

      '-------------------------------
      'preparo i parametri da passare a vecopa
      dttCopa.Columns.Add("conto", GetType(Integer))
      dttCopa.Columns.Add("coddest", GetType(Integer))
      dttCopa.Columns.Add("valuta", GetType(Integer))
      dttCopa.Columns.Add("agente", GetType(Integer))
      dttCopa.Columns.Add("agente2", GetType(Integer))
      dttCopa.Columns.Add("datdoc", GetType(String))
      dttCopa.Columns.Add("codart", GetType(String))
      dttCopa.Columns.Add("fase", GetType(Integer))
      dttCopa.Columns.Add("quant", GetType(Decimal))
      dttCopa.Columns.Add("prezzo", GetType(Decimal))
      dttCopa.Columns.Add("prezvalc", GetType(Decimal))
      dttCopa.Columns.Add("prznet", GetType(String))
      dttCopa.Columns.Add("perqta", GetType(Integer))
      dttCopa.Columns.Add("ump", GetType(String))
      dttCopa.Columns.Add("scont1", GetType(Decimal))
      dttCopa.Columns.Add("scont2", GetType(Decimal))
      dttCopa.Columns.Add("scont3", GetType(Decimal))
      dttCopa.Columns.Add("scont4", GetType(Decimal))
      dttCopa.Columns.Add("scont5", GetType(Decimal))
      dttCopa.Columns.Add("scont6", GetType(Decimal))
      dttCopa.Columns.Add("provv", GetType(Decimal))
      dttCopa.Columns.Add("provv2", GetType(Decimal))
      dttCopa.Columns.Add("codlavo", GetType(Integer))
      dttCopa.Columns.Add("codtpro", GetType(Integer))

      For i = 0 To oCleBoll.dttEC.Rows.Count - 1
        If NTSCStr(oCleBoll.dttEC.Rows(i)!ec_umprz).Trim = "N" Then

          dttCopa.Clear()
          dttCopa.AcceptChanges()

          'Rileva il codice lavorazione terzista
          If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" Then
            nCodlavo = 0
            dtrT = dsBoll.Tables("LAVCENT").Select("lce_riga = " & oCleBoll.dttEC.Rows(i)!ec_riga.ToString, "lce_riga, lce_rigaa")
            If dtrT.Length > 0 Then nCodlavo = NTSCInt(dtrT(0)!lce_codlavo)
          End If

          If ckEt_scorpo.Checked Then
            dPrezzo = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_preziva)
          Else
            dPrezzo = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_prezzo)
          End If

          dttCopa.Rows.Add(dttCopa.NewRow)
          With dttCopa.Rows(0)
            !conto = NTSCInt(edEt_conto.Text)
            !coddest = NTSCInt(edEt_coddest.Text)
            !valuta = NTSCInt(edEt_valuta.Text)
            !agente = NTSCInt(edEt_codagen.Text)
            !agente2 = 0
            !datdoc = NTSCDate(edet_datdoc.Text).ToShortDateString
            !codart = NTSCStr(oCleBoll.dttEC.Rows(i)!ec_codart)
            !fase = NTSCInt(oCleBoll.dttEC.Rows(i)!ec_fase)
            !quant = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_quant)
            !prezzo = dPrezzo
            !prezvalc = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_prezvalc)
            !prznet = NTSCStr(oCleBoll.dttEC.Rows(i)!ec_flprznet)
            !perqta = NTSCInt(oCleBoll.dttEC.Rows(i)!ec_perqta)
            !ump = NTSCStr(oCleBoll.dttEC.Rows(i)!ec_ump)
            !scont1 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont1)
            !scont2 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont2)
            !scont3 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont3)
            !scont4 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont4)
            !scont5 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont5)
            !scont6 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_scont6)
            !provv = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_provv)
            !provv2 = NTSCDec(oCleBoll.dttEC.Rows(i)!ec_provv2)
            !codlavo = nCodlavo
            !codtpro = NTSCInt(oCleBoll.dttEC.Rows(i)!ec_codtpro)
          End With
          dttCopa.AcceptChanges()

          'oPar.Ditta = DittaCorrente
          'oPar.strNomProg = "BNVEBOLL"
          'oPar.ctlPar1 = dttCopa
          'oPar.bPar1 = True             'non visualizza la form di FRMVECOPA
          'oMenu.RunChild("NTSInformatica", "FRMVECOPA", "", DittaCorrente, "", "BNVECOPA", oPar, "", True, True)

          oCleCopa.InitEx(dttCopa)
          oCleCopa.nNoMsgConfermaAggCond = 1  'aggiorna senza chiedere conferma ad ogni articolo
          oCleCopa.Elabora()
        End If    ' If NTSCStr(oCleBoll.dttEC.Rows(i)!ec_umprz).Trim = "N" Then
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      RemoveHandler oCleCopa.RemoteEvent, AddressOf GestisciEventiEntity
    End Try
  End Function

  Public Overridable Sub VisualizzaColonne()
    Try
      '--------------------------------------------------------------------------------------------------------------
      GctlSetVisEnab(ec_riga, True)
      GctlSetVisEnab(ec_codart, True)
      GctlSetVisEnab(ec_descr, True)
      GctlSetVisEnab(ec_unmis, True)
      GctlSetVisEnab(ec_ump, True)
      GctlSetVisEnab(ec_quant, True)
      GctlSetVisEnab(ec_prezzo, True)
      GctlSetVisEnab(ec_scont1, True)
      GctlSetVisEnab(ec_scont2, True)
      GctlSetVisEnab(ec_provv, True)
      GctlSetVisEnab(ec_controp, True)
      GctlSetVisEnab(xxo_controp, True)
      GctlSetVisEnab(ec_codiva, True)
      GctlSetVisEnab(xxo_codiva, True)
      GctlSetVisEnab(ec_stasino, True)
      GctlSetVisEnab(ec_note, True)
      GctlSetVisEnab(ec_flkit, True)
      GctlSetVisEnab(ec_valore, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function ApriSchedaTrasp(ByVal bInSalva As Boolean) As Boolean
    Dim oPar As CLE__CLDP = Nothing
    Dim dColli As Decimal
    Dim dQuat As Decimal
    Dim dPeso As Decimal
    Dim i As Integer
    Dim dttTmp As New DataTable
    Dim bArtConai As Boolean
    Try
      If bInSalva = False Then
        oCleBoll.CalcolaTotali()
      End If
      If oCleBoll.bConai Then
        For i = 0 To dsBoll.Tables("CORPO").Rows.Count - 1
          With dsBoll.Tables("CORPO").Rows(i)
            If Microsoft.VisualBasic.Left(NTSCStr(!ec_codart), 5).ToUpper <> "CONAI" Then
              dColli = dColli + NTSCDec(!ec_colli)
              dQuat = dQuat + NTSCDec(!ec_quant)
              If oCleBoll.strCalcPesi = "S" Then
                If oMenu.ValCodiceDb(NTSCStr(!ec_codart), DittaCorrente, "ARTICO", "S", , dttTmp) Then
                  dPeso = dPeso + (NTSCDec(dttTmp.Rows(0)!ar_pesolor) * NTSCDec(!ec_quant))
                End If
              End If
            Else
              bArtConai = True
            End If
          End With
        Next
      End If

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSVEBOLL"
      oPar.ctlPar1 = dsBoll.Tables("SCHETRASP")
      oPar.ctlPar2 = dsBoll.Tables("TESTA")
      oPar.dPar1 = dColli
      oPar.dPar2 = dQuat
      oPar.dPar3 = dPeso
      oPar.bPar1 = bArtConai

      oMenu.RunChild("NTSInformatica", "FRMMGSCTR", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If CType(oPar.ctlPar1, DataTable).Rows.Count > 0 Then
        oCleBoll.dsShared.Tables("SCHETRASP").Clear()
        If CType(oPar.ctlPar1, DataTable).Rows.Count > 0 Then
          oCleBoll.dsShared.Tables("SCHETRASP").ImportRow(CType(oPar.ctlPar1, DataTable).Rows(0))
        End If
        oCleBoll.bHasChangesET = True
      Else
        oCleBoll.dsShared.Tables("SCHETRASP").Clear()
        oCleBoll.bHasChangesET = True
      End If

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
         (NTSCDate(edet_datdoc.Text) > NTSCDate(oApp.ActKey.DataScad)) Then
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

  Public Overridable Sub ColoraCampoAbiCab()
    Dim dttTmp As New DataTable
    Try
      Dim lAbi As Integer = NTSCInt(oCleBoll.dttET.Rows(dcBoll.Position)!et_abi)
      Dim lCab As Integer = NTSCInt(oCleBoll.dttET.Rows(dcBoll.Position)!et_cab)

      If lAbi = 0 Then
        'Abi non settato, non uso il colore di avviso
        edEt_abi.BackColor = Color.Transparent
        edEt_banc1.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(lAbi.ToString, DittaCorrente, "ABI", "N", , dttTmp)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abiabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edEt_abi.BackColor = Color.Salmon
          edEt_banc1.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edEt_abi.BackColor = Color.Transparent
          edEt_banc1.BackColor = Color.Transparent
        End If
      End If

      If lCab = 0 Then
        'Abi non settato, non uso il colore di avviso
        edEt_cab.BackColor = Color.Transparent
        edEt_banc2.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(lCab.ToString, DittaCorrente, "CAB", "N", , dttTmp, lAbi.ToString)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abcabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edEt_cab.BackColor = Color.Salmon
          edEt_banc2.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edEt_cab.BackColor = Color.Transparent
          edEt_banc2.BackColor = Color.Transparent
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub SelezionaCatalogoFornitori(ByVal strCodartf As String)
    Dim oPar As CLE__CLDP = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = Mid(strCodartf, 11)
      '--------------------------------------------------------------------------------------------------------------
      oMenu.RunChild("NTSInformatica", "FRMMGCACL", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)
      oCleBoll.bCaclAnnullato = oPar.bPar1
      oCleBoll.lCaclForn = NTSCInt(oPar.dPar1)
      oCleBoll.nCaclCodmarc = NTSCInt(oPar.dPar2)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub RipristinaInterfaccia()
    Try
      SetStato(0, False)
      edNumDoc.TextInt = 0
      edNumDoc.SelectAll()
    Catch ex As Exception
      '--------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------
    End Try
  End Sub

End Class