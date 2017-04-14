Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORHLMO
  Public oCleHlmo As CLEORHLMO
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsHlmo As New DataSet
  Public dcHlmo As BindingSource = New BindingSource()
  Public nTipoZoom As Integer = 0
  '0 = solo visualizzazione (bs--clie, bsmghlap, bsmghlar, bsmghvar)
  '1 = non più utilizzato
  '2 = con possibilità di selezione righe (bsorgsor, bsveboll, bsorgsof, bsregdoc, bsdblprd, Bsvebolx)
  Public bModTco As Boolean = False
  Public bAbilitaStart As Boolean = False
  Public bCancel As Boolean = True

  Public bOnLoading As Boolean = False

  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grHlmo As NTSInformatica.NTSGrid
  Public WithEvents grvHlmo As NTSInformatica.NTSGridView
  Public WithEvents NtsGridView1 As NTSInformatica.NTSGridView
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdDeseleziona As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edAppro As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckConfermato As NTSInformatica.NTSCheckBox
  Public WithEvents ckArticolo As NTSInformatica.NTSCheckBox
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mo_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents mo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents mo_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents mo_anno As NTSInformatica.NTSGridColumn
  Public WithEvents mo_serie As NTSInformatica.NTSGridColumn
  Public WithEvents mo_numord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_riga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_flevasdaeva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_qtares As NTSInformatica.NTSGridColumn
  Public WithEvents mo_valore As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant01 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant02 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant03 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant04 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant05 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant06 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant07 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant08 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant09 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant10 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant11 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant12 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant13 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant14 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant15 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant16 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant17 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant18 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant19 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant20 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant21 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant22 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant23 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant24 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva01 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva02 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva03 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva04 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva05 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva06 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva07 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva08 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva09 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva10 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva11 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva12 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva13 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva14 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva15 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva16 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva17 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva18 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva19 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva20 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva21 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva22 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva23 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva24 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre01 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre02 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre03 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre04 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre05 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre06 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre07 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre08 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre09 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre10 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre11 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre12 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre13 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre14 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre15 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre16 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre17 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre18 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre19 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre20 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre21 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre22 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre23 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre24 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva01 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva02 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva03 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva04 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva05 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva06 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva07 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva08 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva09 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva10 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva11 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva12 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva13 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva14 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva15 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva16 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva17 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva18 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva19 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva20 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva21 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva22 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva23 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva24 As NTSInformatica.NTSGridColumn
  Public WithEvents lbAppro As NTSInformatica.NTSLabel
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents ckAppro As NTSInformatica.NTSCheckBox
  Public WithEvents ckCommeca As NTSInformatica.NTSCheckBox
  Public WithEvents cmdNavigazione As NTSInformatica.NTSButton
  Public WithEvents cmdRipristina As NTSInformatica.NTSButton
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents mo_note As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prezzonetto As NTSInformatica.NTSGridColumn
  Public WithEvents mo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mo_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_articopadre As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desarticopadre As NTSInformatica.NTSGridColumn
  Public WithEvents xx_agente As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desagente As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codappr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desappr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desconto As NTSInformatica.NTSGridColumn
  Public WithEvents mo_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mo_desint As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codalt As NTSInformatica.NTSGridColumn
  Public WithEvents cmdApriOrdine As NTSInformatica.NTSButton
  Public WithEvents frmEstremiDoc As NTSInformatica.NTSGroupBox
  Public WithEvents lbTipoRk As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipoRk As NTSInformatica.NTSComboBox
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents ckNoFigliKit As NTSInformatica.NTSCheckBox
  Public WithEvents mo_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quadaeva As NTSInformatica.NTSGridColumn


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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORHLMO", "BEORHLMO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128581693271641409, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlmo = CType(oTmp, CLEORHLMO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORHLMO", strRemoteServer, strRemotePort)
    oCleHlmo.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlmo.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      Dim dttTipoRk As New DataTable
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      ckArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 128582589738125000, "Filtro articolo"), "S", "N")
      ckConfermato.NTSSetParam(oMenu, oApp.Tr(Me, 128582589848750000, "Solo righe confermate"), "S", "N")
      ckCommeca.NTSSetParam(oMenu, oApp.Tr(Me, 128583591414375000, "Filtro Commessa"), "S", "N")
      ckAppro.NTSSetParam(oMenu, oApp.Tr(Me, 128583591400468750, "Filtro Approvvigionatore"), "S", "N")
      edCommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128582590327500000, "Commessa"), tabcommess)
      edAppro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128582590320312500, "Approvvigionatore"), tabappr)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 130409913201007699, "Serie"), CLN__STD.SerieMaxLen, True)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 130409913201017704, "Anno"), "0", 4, 1900, 2099)
      cbTipoRk.NTSSetParam(oApp.Tr(Me, 130409913201027712, "Tipo Documento"))
      grvHlmo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia righe d'ordine"))
      grvHlmo.NTSAllowDelete = False
      grvHlmo.NTSAllowUpdate = True
      grvHlmo.NTSAllowInsert = False
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128582558793906250, "Seleziona"), "S", "N")
      xx_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548496142080079, "Conto"), tabanagra)
      mo_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129548496152724610, "Articolo"), tabartico, True)
      mo_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548496167675782, "Magazzino"), tabmaga)
      mo_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 131026028104774753, "Magazzino 2"), tabmaga)
      xx_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131026028104930981, "Descr. magazzino 2"), 0, True)
      mo_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548496179921875, "Fase"), tabartfasi)
      mo_fase.ArtiPerFase = mo_codart
      mo_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129548496191123047, "tipo ord."), dttTipoRk, "val", "cod")
      mo_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496206162110, "Anno ord."), "0", 4, 0, 9999)
      mo_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548496217919922, "Serie ord."), CLN__STD.SerieMaxLen, True)
      mo_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496229521485, "Num. ord."), "0", 9, 0, 999999999)
      mo_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496241035157, "Riga ord."), "0", 9, 0, 999999999)
      xx_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129548496252236329, "Data ord."), True)
      mo_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129548496263515625, "Data cons."), True)
      mo_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496275439454, "Qta ordin."), oApp.FormatQta, 15)
      mo_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496287685547, "Qta pren."), oApp.FormatQta, 15)
      mo_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496298476563, "Qta evasa"), oApp.FormatQta, 15)
      xx_quadaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496309521485, "Qta da evadere"), oApp.FormatQta, 15, 0, 999999999999999)
      xx_flevasdaeva.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129548496321298829, "Evaso C/S"), "S", "C")
      xx_qtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496333037110, "Qta resid."), oApp.FormatQta, 15)
      mo_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496343515625, "Valore resid."), oApp.FormatQta, 15)
      xx_desconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495548251954, "Descr. Conto"), 0, True)
      mo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548496072089844, "Descr. Articolo"), 0, True)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495563291016, "Descr. magazzino"), 0, True)
      xx_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495588085938, "Anno P."), "0", 4, 0, 9999)
      xx_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495599531250, "Serie P."), CLN__STD.SerieMaxLen, True)
      xx_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495611464844, "Num. P."), "0", 9, 0, 999999999)
      xx_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128584227240937500, "Riferimenti"), 0, True)
      mo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495640576172, "Note riga"), 0, True)
      mo_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495656572266, "Prezzo unitario"), oApp.FormatPrzUn, 15)
      mo_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495668408204, "Sc. 1"), oApp.FormatSconti, 6)
      mo_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495679609375, "Sc. 2"), oApp.FormatSconti, 6)
      mo_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495735849610, "Sc. 3"), oApp.FormatSconti, 6)
      mo_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495711533204, "Sc. 4"), oApp.FormatSconti, 6)
      mo_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495750742188, "Sc. 5"), oApp.FormatSconti, 6)
      mo_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548495761445313, "Sc. 6"), oApp.FormatSconti, 6)
      xx_prezzonetto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496474873047, "Prezzo netto"), oApp.FormatPrzUn, 15)
      mo_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548495772724610, "Commessa"), tabcommess)
      mo_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495796972657, "S. comm."), 0, True)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495808486329, "Descr. commessa"), 0, True)
      xx_articopadre.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129548495819365235, "Articolo padre"), tabartico, True)
      xx_desarticopadre.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495837529297, "Descr. articolo padre"), 0, True)
      xx_agente.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548495849609375, "Agente"), tabcage)
      xx_desagente.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495860566407, "Descr. agente"), 0, True)
      xx_codappr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129548495872001954, "Approvvig."), tabappr)
      xx_desappr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495883125000, "Descr. approvvig."), 0, True)
      mo_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129548495893925782, "Descr. interna art"), 0, True)
      xx_codalt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129403329411312354, "Cod. alternativo articolo"), 0, True)

      For i = 1 To 24
        CType(grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).NTSSetParamNUM(oMenu, oApp.Tr(Me, 129548496354394532, "Q.O.") & i.ToString.PadLeft(2, "0"c), oApp.FormatQta, 15)
        CType(grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).NTSSetParamNUM(oMenu, oApp.Tr(Me, 128583575696250000, "Q.E.") & i.ToString.PadLeft(2, "0"c), oApp.FormatQta, 15)
        CType(grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).NTSSetParamNUM(oMenu, oApp.Tr(Me, 128583575707656250, "Q.P.") & i.ToString.PadLeft(2, "0"c), oApp.FormatQta, 15)
        CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).NTSSetParamNUM(oMenu, oApp.Tr(Me, 128583575719531250, "Q.D.") & i.ToString.PadLeft(2, "0"c), oApp.FormatQta, 15, 0, 999999999999999)
      Next

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
    Me.lbAppro = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.ckNoFigliKit = New NTSInformatica.NTSCheckBox
    Me.ckArticolo = New NTSInformatica.NTSCheckBox
    Me.ckAppro = New NTSInformatica.NTSCheckBox
    Me.ckCommeca = New NTSInformatica.NTSCheckBox
    Me.ckConfermato = New NTSInformatica.NTSCheckBox
    Me.edAppro = New NTSInformatica.NTSTextBoxNum
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.cmdApriOrdine = New NTSInformatica.NTSButton
    Me.cmdNavigazione = New NTSInformatica.NTSButton
    Me.cmdRipristina = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.cmdDeseleziona = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grHlmo = New NTSInformatica.NTSGrid
    Me.grvHlmo = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva = New NTSInformatica.NTSGridColumn
    Me.xx_flevasdaeva = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.xx_desconto = New NTSInformatica.NTSGridColumn
    Me.mo_codart = New NTSInformatica.NTSGridColumn
    Me.mo_descr = New NTSInformatica.NTSGridColumn
    Me.mo_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.mo_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xx_magaz2 = New NTSInformatica.NTSGridColumn
    Me.mo_fase = New NTSInformatica.NTSGridColumn
    Me.mo_tipork = New NTSInformatica.NTSGridColumn
    Me.mo_anno = New NTSInformatica.NTSGridColumn
    Me.mo_serie = New NTSInformatica.NTSGridColumn
    Me.mo_numord = New NTSInformatica.NTSGridColumn
    Me.mo_riga = New NTSInformatica.NTSGridColumn
    Me.xx_datord = New NTSInformatica.NTSGridColumn
    Me.mo_datcons = New NTSInformatica.NTSGridColumn
    Me.mo_quant = New NTSInformatica.NTSGridColumn
    Me.mo_quapre = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva = New NTSInformatica.NTSGridColumn
    Me.xx_qtares = New NTSInformatica.NTSGridColumn
    Me.mo_valore = New NTSInformatica.NTSGridColumn
    Me.xx_annpar = New NTSInformatica.NTSGridColumn
    Me.xx_alfpar = New NTSInformatica.NTSGridColumn
    Me.xx_numpar = New NTSInformatica.NTSGridColumn
    Me.xx_riferim = New NTSInformatica.NTSGridColumn
    Me.mo_note = New NTSInformatica.NTSGridColumn
    Me.mo_prezzo = New NTSInformatica.NTSGridColumn
    Me.mo_scont1 = New NTSInformatica.NTSGridColumn
    Me.mo_scont2 = New NTSInformatica.NTSGridColumn
    Me.mo_scont3 = New NTSInformatica.NTSGridColumn
    Me.mo_scont4 = New NTSInformatica.NTSGridColumn
    Me.mo_scont5 = New NTSInformatica.NTSGridColumn
    Me.mo_scont6 = New NTSInformatica.NTSGridColumn
    Me.xx_prezzonetto = New NTSInformatica.NTSGridColumn
    Me.mo_commeca = New NTSInformatica.NTSGridColumn
    Me.mo_subcommeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_articopadre = New NTSInformatica.NTSGridColumn
    Me.xx_desarticopadre = New NTSInformatica.NTSGridColumn
    Me.xx_agente = New NTSInformatica.NTSGridColumn
    Me.xx_desagente = New NTSInformatica.NTSGridColumn
    Me.xx_codappr = New NTSInformatica.NTSGridColumn
    Me.xx_desappr = New NTSInformatica.NTSGridColumn
    Me.mo_desint = New NTSInformatica.NTSGridColumn
    Me.xx_codalt = New NTSInformatica.NTSGridColumn
    Me.mo_quant01 = New NTSInformatica.NTSGridColumn
    Me.mo_quant02 = New NTSInformatica.NTSGridColumn
    Me.mo_quant03 = New NTSInformatica.NTSGridColumn
    Me.mo_quant04 = New NTSInformatica.NTSGridColumn
    Me.mo_quant05 = New NTSInformatica.NTSGridColumn
    Me.mo_quant06 = New NTSInformatica.NTSGridColumn
    Me.mo_quant07 = New NTSInformatica.NTSGridColumn
    Me.mo_quant08 = New NTSInformatica.NTSGridColumn
    Me.mo_quant09 = New NTSInformatica.NTSGridColumn
    Me.mo_quant10 = New NTSInformatica.NTSGridColumn
    Me.mo_quant11 = New NTSInformatica.NTSGridColumn
    Me.mo_quant12 = New NTSInformatica.NTSGridColumn
    Me.mo_quant13 = New NTSInformatica.NTSGridColumn
    Me.mo_quant14 = New NTSInformatica.NTSGridColumn
    Me.mo_quant15 = New NTSInformatica.NTSGridColumn
    Me.mo_quant16 = New NTSInformatica.NTSGridColumn
    Me.mo_quant17 = New NTSInformatica.NTSGridColumn
    Me.mo_quant18 = New NTSInformatica.NTSGridColumn
    Me.mo_quant19 = New NTSInformatica.NTSGridColumn
    Me.mo_quant20 = New NTSInformatica.NTSGridColumn
    Me.mo_quant21 = New NTSInformatica.NTSGridColumn
    Me.mo_quant22 = New NTSInformatica.NTSGridColumn
    Me.mo_quant23 = New NTSInformatica.NTSGridColumn
    Me.mo_quant24 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva01 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva02 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva03 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva04 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva05 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva06 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva07 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva08 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva09 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva10 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva11 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva12 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva13 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva14 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva15 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva16 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva17 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva18 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva19 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva20 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva21 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva22 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva23 = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva24 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre01 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre02 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre03 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre04 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre05 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre06 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre07 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre08 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre09 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre10 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre11 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre12 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre13 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre14 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre15 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre16 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre17 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre18 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre19 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre20 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre21 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre22 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre23 = New NTSInformatica.NTSGridColumn
    Me.mo_quapre24 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva01 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva02 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva03 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva04 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva05 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva06 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva07 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva08 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva09 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva10 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva11 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva12 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva13 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva14 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva15 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva16 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva17 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva18 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva19 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva20 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva21 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva22 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva23 = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva24 = New NTSInformatica.NTSGridColumn
    Me.NtsGridView1 = New NTSInformatica.NTSGridView
    Me.frmEstremiDoc = New NTSInformatica.NTSGroupBox
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.cbTipoRk = New NTSInformatica.NTSComboBox
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbTipoRk = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.ckNoFigliKit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAppro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConfermato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAppro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grHlmo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlmo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmEstremiDoc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmEstremiDoc.SuspendLayout()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoRk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.lbAppro)
    Me.pnBottom.Controls.Add(Me.lbCommeca)
    Me.pnBottom.Controls.Add(Me.ckNoFigliKit)
    Me.pnBottom.Controls.Add(Me.ckArticolo)
    Me.pnBottom.Controls.Add(Me.ckAppro)
    Me.pnBottom.Controls.Add(Me.ckCommeca)
    Me.pnBottom.Controls.Add(Me.ckConfermato)
    Me.pnBottom.Controls.Add(Me.edAppro)
    Me.pnBottom.Controls.Add(Me.edCommeca)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 253)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(711, 63)
    Me.pnBottom.TabIndex = 1
    Me.pnBottom.Text = "NtsPanel1"
    '
    'lbAppro
    '
    Me.lbAppro.BackColor = System.Drawing.Color.Transparent
    Me.lbAppro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAppro.Location = New System.Drawing.Point(265, 32)
    Me.lbAppro.Name = "lbAppro"
    Me.lbAppro.NTSDbField = ""
    Me.lbAppro.Size = New System.Drawing.Size(232, 20)
    Me.lbAppro.TabIndex = 18
    Me.lbAppro.Tooltip = ""
    Me.lbAppro.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommeca.Location = New System.Drawing.Point(265, 6)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(232, 20)
    Me.lbCommeca.TabIndex = 17
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'ckNoFigliKit
    '
    Me.ckNoFigliKit.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNoFigliKit.Location = New System.Drawing.Point(504, 33)
    Me.ckNoFigliKit.Name = "ckNoFigliKit"
    Me.ckNoFigliKit.NTSCheckValue = "S"
    Me.ckNoFigliKit.NTSUnCheckValue = "N"
    Me.ckNoFigliKit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNoFigliKit.Properties.Appearance.Options.UseBackColor = True
    Me.ckNoFigliKit.Properties.AutoHeight = False
    Me.ckNoFigliKit.Properties.Caption = "&Nascondi i figli delle righe kit"
    Me.ckNoFigliKit.Size = New System.Drawing.Size(199, 19)
    Me.ckNoFigliKit.TabIndex = 14
    '
    'ckArticolo
    '
    Me.ckArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArticolo.Location = New System.Drawing.Point(504, 7)
    Me.ckArticolo.Name = "ckArticolo"
    Me.ckArticolo.NTSCheckValue = "S"
    Me.ckArticolo.NTSUnCheckValue = "N"
    Me.ckArticolo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArticolo.Properties.Appearance.Options.UseBackColor = True
    Me.ckArticolo.Properties.AutoHeight = False
    Me.ckArticolo.Properties.Caption = "&Filtro articolo"
    Me.ckArticolo.Size = New System.Drawing.Size(91, 19)
    Me.ckArticolo.TabIndex = 14
    '
    'ckAppro
    '
    Me.ckAppro.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAppro.Location = New System.Drawing.Point(12, 33)
    Me.ckAppro.Name = "ckAppro"
    Me.ckAppro.NTSCheckValue = "S"
    Me.ckAppro.NTSUnCheckValue = "N"
    Me.ckAppro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAppro.Properties.Appearance.Options.UseBackColor = True
    Me.ckAppro.Properties.AutoHeight = False
    Me.ckAppro.Properties.Caption = "Filtro &Approvvigionatore"
    Me.ckAppro.Size = New System.Drawing.Size(145, 19)
    Me.ckAppro.TabIndex = 16
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
    Me.ckCommeca.Properties.AutoHeight = False
    Me.ckCommeca.Properties.Caption = "Filtro &Commessa"
    Me.ckCommeca.Size = New System.Drawing.Size(105, 19)
    Me.ckCommeca.TabIndex = 15
    '
    'ckConfermato
    '
    Me.ckConfermato.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConfermato.Location = New System.Drawing.Point(602, 7)
    Me.ckConfermato.Name = "ckConfermato"
    Me.ckConfermato.NTSCheckValue = "S"
    Me.ckConfermato.NTSUnCheckValue = "N"
    Me.ckConfermato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConfermato.Properties.Appearance.Options.UseBackColor = True
    Me.ckConfermato.Properties.AutoHeight = False
    Me.ckConfermato.Properties.Caption = "Solo righe conf."
    Me.ckConfermato.Size = New System.Drawing.Size(105, 19)
    Me.ckConfermato.TabIndex = 0
    '
    'edAppro
    '
    Me.edAppro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAppro.EditValue = "0"
    Me.edAppro.Location = New System.Drawing.Point(162, 32)
    Me.edAppro.Name = "edAppro"
    Me.edAppro.NTSDbField = ""
    Me.edAppro.NTSFormat = "0"
    Me.edAppro.NTSForzaVisZoom = False
    Me.edAppro.NTSOldValue = ""
    Me.edAppro.Properties.Appearance.Options.UseTextOptions = True
    Me.edAppro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAppro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAppro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAppro.Properties.AutoHeight = False
    Me.edAppro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAppro.Properties.MaxLength = 65536
    Me.edAppro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAppro.Size = New System.Drawing.Size(100, 20)
    Me.edAppro.TabIndex = 2
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.EditValue = "0"
    Me.edCommeca.Location = New System.Drawing.Point(162, 6)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.AutoHeight = False
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
    Me.pnButton.Controls.Add(Me.cmdApriOrdine)
    Me.pnButton.Controls.Add(Me.cmdNavigazione)
    Me.pnButton.Controls.Add(Me.cmdRipristina)
    Me.pnButton.Controls.Add(Me.cmdRicerca)
    Me.pnButton.Controls.Add(Me.cmdDeseleziona)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdSeleziona)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(598, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.NTSActiveTrasparency = True
    Me.pnButton.Size = New System.Drawing.Size(113, 253)
    Me.pnButton.TabIndex = 2
    Me.pnButton.Text = "NtsPanel1"
    '
    'cmdApriOrdine
    '
    Me.cmdApriOrdine.ImagePath = ""
    Me.cmdApriOrdine.ImageText = ""
    Me.cmdApriOrdine.Location = New System.Drawing.Point(6, 225)
    Me.cmdApriOrdine.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdApriOrdine.Name = "cmdApriOrdine"
    Me.cmdApriOrdine.NTSContextMenu = Nothing
    Me.cmdApriOrdine.Size = New System.Drawing.Size(98, 26)
    Me.cmdApriOrdine.TabIndex = 18
    Me.cmdApriOrdine.Text = "Apri &Ordine"
    '
    'cmdNavigazione
    '
    Me.cmdNavigazione.ImagePath = ""
    Me.cmdNavigazione.ImageText = ""
    Me.cmdNavigazione.Location = New System.Drawing.Point(6, 186)
    Me.cmdNavigazione.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdNavigazione.Name = "cmdNavigazione"
    Me.cmdNavigazione.NTSContextMenu = Nothing
    Me.cmdNavigazione.Size = New System.Drawing.Size(98, 26)
    Me.cmdNavigazione.TabIndex = 16
    Me.cmdNavigazione.Text = "Navigazione MRP"
    '
    'cmdRipristina
    '
    Me.cmdRipristina.ImagePath = ""
    Me.cmdRipristina.ImageText = ""
    Me.cmdRipristina.Location = New System.Drawing.Point(6, 158)
    Me.cmdRipristina.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdRipristina.Name = "cmdRipristina"
    Me.cmdRipristina.NTSContextMenu = Nothing
    Me.cmdRipristina.Size = New System.Drawing.Size(98, 26)
    Me.cmdRipristina.TabIndex = 15
    Me.cmdRipristina.Text = "Ripristina"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImagePath = ""
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 10)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(98, 26)
    Me.cmdRicerca.TabIndex = 13
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'cmdDeseleziona
    '
    Me.cmdDeseleziona.ImagePath = ""
    Me.cmdDeseleziona.ImageText = ""
    Me.cmdDeseleziona.Location = New System.Drawing.Point(6, 124)
    Me.cmdDeseleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdDeseleziona.Name = "cmdDeseleziona"
    Me.cmdDeseleziona.NTSContextMenu = Nothing
    Me.cmdDeseleziona.Size = New System.Drawing.Size(98, 26)
    Me.cmdDeseleziona.TabIndex = 12
    Me.cmdDeseleziona.Text = "&Deseleziona tutto"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 62)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(98, 26)
    Me.cmdConferma.TabIndex = 11
    Me.cmdConferma.Text = "&Conferma (F9)"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 36)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 26)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 98)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(98, 26)
    Me.cmdSeleziona.TabIndex = 8
    Me.cmdSeleziona.Text = "&Seleziona tutto"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grHlmo)
    Me.pnGrid.Controls.Add(Me.frmEstremiDoc)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(598, 253)
    Me.pnGrid.TabIndex = 3
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grHlmo
    '
    Me.grHlmo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHlmo.EmbeddedNavigator.Name = ""
    Me.grHlmo.Location = New System.Drawing.Point(0, 0)
    Me.grHlmo.MainView = Me.grvHlmo
    Me.grHlmo.Name = "grHlmo"
    Me.grHlmo.Size = New System.Drawing.Size(598, 209)
    Me.grHlmo.TabIndex = 0
    Me.grHlmo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlmo, Me.NtsGridView1})
    '
    'grvHlmo
    '
    Me.grvHlmo.ActiveFilterEnabled = False
    Me.grvHlmo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.xx_quadaeva, Me.xx_flevasdaeva, Me.xx_conto, Me.xx_desconto, Me.mo_codart, Me.mo_descr, Me.mo_magaz, Me.xx_magaz, Me.mo_magaz2, Me.xx_magaz2, Me.mo_fase, Me.mo_tipork, Me.mo_anno, Me.mo_serie, Me.mo_numord, Me.mo_riga, Me.xx_datord, Me.mo_datcons, Me.mo_quant, Me.mo_quapre, Me.mo_quaeva, Me.xx_qtares, Me.mo_valore, Me.xx_annpar, Me.xx_alfpar, Me.xx_numpar, Me.xx_riferim, Me.mo_note, Me.mo_prezzo, Me.mo_scont1, Me.mo_scont2, Me.mo_scont3, Me.mo_scont4, Me.mo_scont5, Me.mo_scont6, Me.xx_prezzonetto, Me.mo_commeca, Me.mo_subcommeca, Me.xx_commeca, Me.xx_articopadre, Me.xx_desarticopadre, Me.xx_agente, Me.xx_desagente, Me.xx_codappr, Me.xx_desappr, Me.mo_desint, Me.xx_codalt, Me.mo_quant01, Me.mo_quant02, Me.mo_quant03, Me.mo_quant04, Me.mo_quant05, Me.mo_quant06, Me.mo_quant07, Me.mo_quant08, Me.mo_quant09, Me.mo_quant10, Me.mo_quant11, Me.mo_quant12, Me.mo_quant13, Me.mo_quant14, Me.mo_quant15, Me.mo_quant16, Me.mo_quant17, Me.mo_quant18, Me.mo_quant19, Me.mo_quant20, Me.mo_quant21, Me.mo_quant22, Me.mo_quant23, Me.mo_quant24, Me.mo_quaeva01, Me.mo_quaeva02, Me.mo_quaeva03, Me.mo_quaeva04, Me.mo_quaeva05, Me.mo_quaeva06, Me.mo_quaeva07, Me.mo_quaeva08, Me.mo_quaeva09, Me.mo_quaeva10, Me.mo_quaeva11, Me.mo_quaeva12, Me.mo_quaeva13, Me.mo_quaeva14, Me.mo_quaeva15, Me.mo_quaeva16, Me.mo_quaeva17, Me.mo_quaeva18, Me.mo_quaeva19, Me.mo_quaeva20, Me.mo_quaeva21, Me.mo_quaeva22, Me.mo_quaeva23, Me.mo_quaeva24, Me.mo_quapre01, Me.mo_quapre02, Me.mo_quapre03, Me.mo_quapre04, Me.mo_quapre05, Me.mo_quapre06, Me.mo_quapre07, Me.mo_quapre08, Me.mo_quapre09, Me.mo_quapre10, Me.mo_quapre11, Me.mo_quapre12, Me.mo_quapre13, Me.mo_quapre14, Me.mo_quapre15, Me.mo_quapre16, Me.mo_quapre17, Me.mo_quapre18, Me.mo_quapre19, Me.mo_quapre20, Me.mo_quapre21, Me.mo_quapre22, Me.mo_quapre23, Me.mo_quapre24, Me.xx_quadaeva01, Me.xx_quadaeva02, Me.xx_quadaeva03, Me.xx_quadaeva04, Me.xx_quadaeva05, Me.xx_quadaeva06, Me.xx_quadaeva07, Me.xx_quadaeva08, Me.xx_quadaeva09, Me.xx_quadaeva10, Me.xx_quadaeva11, Me.xx_quadaeva12, Me.xx_quadaeva13, Me.xx_quadaeva14, Me.xx_quadaeva15, Me.xx_quadaeva16, Me.xx_quadaeva17, Me.xx_quadaeva18, Me.xx_quadaeva19, Me.xx_quadaeva20, Me.xx_quadaeva21, Me.xx_quadaeva22, Me.xx_quadaeva23, Me.xx_quadaeva24})
    Me.grvHlmo.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvHlmo.Enabled = True
    Me.grvHlmo.GridControl = Me.grHlmo
    Me.grvHlmo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlmo.MinRowHeight = 14
    Me.grvHlmo.Name = "grvHlmo"
    Me.grvHlmo.NTSAllowDelete = True
    Me.grvHlmo.NTSAllowInsert = True
    Me.grvHlmo.NTSAllowUpdate = True
    Me.grvHlmo.NTSMenuContext = Nothing
    Me.grvHlmo.OptionsCustomization.AllowRowSizing = True
    Me.grvHlmo.OptionsFilter.AllowFilterEditor = False
    Me.grvHlmo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlmo.OptionsNavigation.UseTabKey = False
    Me.grvHlmo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlmo.OptionsView.ColumnAutoWidth = False
    Me.grvHlmo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlmo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlmo.OptionsView.ShowGroupPanel = False
    Me.grvHlmo.RowHeight = 14
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
    'xx_quadaeva
    '
    Me.xx_quadaeva.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva.Caption = "Qta da evadere"
    Me.xx_quadaeva.Enabled = True
    Me.xx_quadaeva.FieldName = "xx_quadaeva"
    Me.xx_quadaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva.Name = "xx_quadaeva"
    Me.xx_quadaeva.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva.Visible = True
    Me.xx_quadaeva.VisibleIndex = 1
    '
    'xx_flevasdaeva
    '
    Me.xx_flevasdaeva.AppearanceCell.Options.UseBackColor = True
    Me.xx_flevasdaeva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_flevasdaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_flevasdaeva.Caption = "Evaso C/S"
    Me.xx_flevasdaeva.Enabled = True
    Me.xx_flevasdaeva.FieldName = "xx_flevasdaeva"
    Me.xx_flevasdaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_flevasdaeva.Name = "xx_flevasdaeva"
    Me.xx_flevasdaeva.NTSRepositoryComboBox = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemCheck = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemMemo = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemText = Nothing
    Me.xx_flevasdaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_flevasdaeva.OptionsFilter.AllowFilter = False
    Me.xx_flevasdaeva.Visible = True
    Me.xx_flevasdaeva.VisibleIndex = 2
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Conto"
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
    Me.xx_conto.VisibleIndex = 3
    '
    'xx_desconto
    '
    Me.xx_desconto.AppearanceCell.Options.UseBackColor = True
    Me.xx_desconto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desconto.Caption = "Descr. Conto"
    Me.xx_desconto.Enabled = False
    Me.xx_desconto.FieldName = "xx_desconto"
    Me.xx_desconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desconto.Name = "xx_desconto"
    Me.xx_desconto.NTSRepositoryComboBox = Nothing
    Me.xx_desconto.NTSRepositoryItemCheck = Nothing
    Me.xx_desconto.NTSRepositoryItemMemo = Nothing
    Me.xx_desconto.NTSRepositoryItemText = Nothing
    Me.xx_desconto.OptionsColumn.AllowEdit = False
    Me.xx_desconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desconto.OptionsColumn.ReadOnly = True
    Me.xx_desconto.OptionsFilter.AllowFilter = False
    Me.xx_desconto.Visible = True
    Me.xx_desconto.VisibleIndex = 4
    '
    'mo_codart
    '
    Me.mo_codart.AppearanceCell.Options.UseBackColor = True
    Me.mo_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codart.Caption = "Articolo"
    Me.mo_codart.Enabled = False
    Me.mo_codart.FieldName = "mo_codart"
    Me.mo_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codart.Name = "mo_codart"
    Me.mo_codart.NTSRepositoryComboBox = Nothing
    Me.mo_codart.NTSRepositoryItemCheck = Nothing
    Me.mo_codart.NTSRepositoryItemMemo = Nothing
    Me.mo_codart.NTSRepositoryItemText = Nothing
    Me.mo_codart.OptionsColumn.AllowEdit = False
    Me.mo_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codart.OptionsColumn.ReadOnly = True
    Me.mo_codart.OptionsFilter.AllowFilter = False
    Me.mo_codart.Visible = True
    Me.mo_codart.VisibleIndex = 5
    '
    'mo_descr
    '
    Me.mo_descr.AppearanceCell.Options.UseBackColor = True
    Me.mo_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_descr.Caption = "Descr. Articolo"
    Me.mo_descr.Enabled = False
    Me.mo_descr.FieldName = "mo_descr"
    Me.mo_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_descr.Name = "mo_descr"
    Me.mo_descr.NTSRepositoryComboBox = Nothing
    Me.mo_descr.NTSRepositoryItemCheck = Nothing
    Me.mo_descr.NTSRepositoryItemMemo = Nothing
    Me.mo_descr.NTSRepositoryItemText = Nothing
    Me.mo_descr.OptionsColumn.AllowEdit = False
    Me.mo_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_descr.OptionsColumn.ReadOnly = True
    Me.mo_descr.OptionsFilter.AllowFilter = False
    Me.mo_descr.Visible = True
    Me.mo_descr.VisibleIndex = 6
    '
    'mo_magaz
    '
    Me.mo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_magaz.Caption = "Magazzino"
    Me.mo_magaz.Enabled = False
    Me.mo_magaz.FieldName = "mo_magaz"
    Me.mo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_magaz.Name = "mo_magaz"
    Me.mo_magaz.NTSRepositoryComboBox = Nothing
    Me.mo_magaz.NTSRepositoryItemCheck = Nothing
    Me.mo_magaz.NTSRepositoryItemMemo = Nothing
    Me.mo_magaz.NTSRepositoryItemText = Nothing
    Me.mo_magaz.OptionsColumn.AllowEdit = False
    Me.mo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_magaz.OptionsColumn.ReadOnly = True
    Me.mo_magaz.OptionsFilter.AllowFilter = False
    Me.mo_magaz.Visible = True
    Me.mo_magaz.VisibleIndex = 7
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
    Me.xx_magaz.VisibleIndex = 8
    '
    'mo_magaz2
    '
    Me.mo_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.mo_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_magaz2.Caption = "Magazzino 2"
    Me.mo_magaz2.Enabled = False
    Me.mo_magaz2.FieldName = "mo_magaz2"
    Me.mo_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_magaz2.Name = "mo_magaz2"
    Me.mo_magaz2.NTSRepositoryComboBox = Nothing
    Me.mo_magaz2.NTSRepositoryItemCheck = Nothing
    Me.mo_magaz2.NTSRepositoryItemMemo = Nothing
    Me.mo_magaz2.NTSRepositoryItemText = Nothing
    Me.mo_magaz2.OptionsColumn.AllowEdit = False
    Me.mo_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_magaz2.OptionsColumn.ReadOnly = True
    Me.mo_magaz2.OptionsFilter.AllowFilter = False
    '
    'xx_magaz2
    '
    Me.xx_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz2.Caption = "Descr. magazzino 2"
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
    '
    'mo_fase
    '
    Me.mo_fase.AppearanceCell.Options.UseBackColor = True
    Me.mo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.mo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_fase.Caption = "Fase"
    Me.mo_fase.Enabled = False
    Me.mo_fase.FieldName = "mo_fase"
    Me.mo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_fase.Name = "mo_fase"
    Me.mo_fase.NTSRepositoryComboBox = Nothing
    Me.mo_fase.NTSRepositoryItemCheck = Nothing
    Me.mo_fase.NTSRepositoryItemMemo = Nothing
    Me.mo_fase.NTSRepositoryItemText = Nothing
    Me.mo_fase.OptionsColumn.AllowEdit = False
    Me.mo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_fase.OptionsColumn.ReadOnly = True
    Me.mo_fase.OptionsFilter.AllowFilter = False
    Me.mo_fase.Visible = True
    Me.mo_fase.VisibleIndex = 9
    '
    'mo_tipork
    '
    Me.mo_tipork.AppearanceCell.Options.UseBackColor = True
    Me.mo_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.mo_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_tipork.Caption = "tipo ord."
    Me.mo_tipork.Enabled = False
    Me.mo_tipork.FieldName = "mo_tipork"
    Me.mo_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_tipork.Name = "mo_tipork"
    Me.mo_tipork.NTSRepositoryComboBox = Nothing
    Me.mo_tipork.NTSRepositoryItemCheck = Nothing
    Me.mo_tipork.NTSRepositoryItemMemo = Nothing
    Me.mo_tipork.NTSRepositoryItemText = Nothing
    Me.mo_tipork.OptionsColumn.AllowEdit = False
    Me.mo_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_tipork.OptionsColumn.ReadOnly = True
    Me.mo_tipork.OptionsFilter.AllowFilter = False
    Me.mo_tipork.Visible = True
    Me.mo_tipork.VisibleIndex = 10
    '
    'mo_anno
    '
    Me.mo_anno.AppearanceCell.Options.UseBackColor = True
    Me.mo_anno.AppearanceCell.Options.UseTextOptions = True
    Me.mo_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_anno.Caption = "Anno ord."
    Me.mo_anno.Enabled = False
    Me.mo_anno.FieldName = "mo_anno"
    Me.mo_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_anno.Name = "mo_anno"
    Me.mo_anno.NTSRepositoryComboBox = Nothing
    Me.mo_anno.NTSRepositoryItemCheck = Nothing
    Me.mo_anno.NTSRepositoryItemMemo = Nothing
    Me.mo_anno.NTSRepositoryItemText = Nothing
    Me.mo_anno.OptionsColumn.AllowEdit = False
    Me.mo_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_anno.OptionsColumn.ReadOnly = True
    Me.mo_anno.OptionsFilter.AllowFilter = False
    Me.mo_anno.Visible = True
    Me.mo_anno.VisibleIndex = 11
    '
    'mo_serie
    '
    Me.mo_serie.AppearanceCell.Options.UseBackColor = True
    Me.mo_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mo_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_serie.Caption = "Serie ord."
    Me.mo_serie.Enabled = False
    Me.mo_serie.FieldName = "mo_serie"
    Me.mo_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_serie.Name = "mo_serie"
    Me.mo_serie.NTSRepositoryComboBox = Nothing
    Me.mo_serie.NTSRepositoryItemCheck = Nothing
    Me.mo_serie.NTSRepositoryItemMemo = Nothing
    Me.mo_serie.NTSRepositoryItemText = Nothing
    Me.mo_serie.OptionsColumn.AllowEdit = False
    Me.mo_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_serie.OptionsColumn.ReadOnly = True
    Me.mo_serie.OptionsFilter.AllowFilter = False
    Me.mo_serie.Visible = True
    Me.mo_serie.VisibleIndex = 12
    '
    'mo_numord
    '
    Me.mo_numord.AppearanceCell.Options.UseBackColor = True
    Me.mo_numord.AppearanceCell.Options.UseTextOptions = True
    Me.mo_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_numord.Caption = "Num. ord."
    Me.mo_numord.Enabled = False
    Me.mo_numord.FieldName = "mo_numord"
    Me.mo_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_numord.Name = "mo_numord"
    Me.mo_numord.NTSRepositoryComboBox = Nothing
    Me.mo_numord.NTSRepositoryItemCheck = Nothing
    Me.mo_numord.NTSRepositoryItemMemo = Nothing
    Me.mo_numord.NTSRepositoryItemText = Nothing
    Me.mo_numord.OptionsColumn.AllowEdit = False
    Me.mo_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_numord.OptionsColumn.ReadOnly = True
    Me.mo_numord.OptionsFilter.AllowFilter = False
    Me.mo_numord.Visible = True
    Me.mo_numord.VisibleIndex = 13
    '
    'mo_riga
    '
    Me.mo_riga.AppearanceCell.Options.UseBackColor = True
    Me.mo_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mo_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_riga.Caption = "Riga ord."
    Me.mo_riga.Enabled = False
    Me.mo_riga.FieldName = "mo_riga"
    Me.mo_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_riga.Name = "mo_riga"
    Me.mo_riga.NTSRepositoryComboBox = Nothing
    Me.mo_riga.NTSRepositoryItemCheck = Nothing
    Me.mo_riga.NTSRepositoryItemMemo = Nothing
    Me.mo_riga.NTSRepositoryItemText = Nothing
    Me.mo_riga.OptionsColumn.AllowEdit = False
    Me.mo_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_riga.OptionsColumn.ReadOnly = True
    Me.mo_riga.OptionsFilter.AllowFilter = False
    Me.mo_riga.Visible = True
    Me.mo_riga.VisibleIndex = 14
    '
    'xx_datord
    '
    Me.xx_datord.AppearanceCell.Options.UseBackColor = True
    Me.xx_datord.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datord.Caption = "Data ord."
    Me.xx_datord.Enabled = False
    Me.xx_datord.FieldName = "xx_datord"
    Me.xx_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datord.Name = "xx_datord"
    Me.xx_datord.NTSRepositoryComboBox = Nothing
    Me.xx_datord.NTSRepositoryItemCheck = Nothing
    Me.xx_datord.NTSRepositoryItemMemo = Nothing
    Me.xx_datord.NTSRepositoryItemText = Nothing
    Me.xx_datord.OptionsColumn.AllowEdit = False
    Me.xx_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datord.OptionsColumn.ReadOnly = True
    Me.xx_datord.OptionsFilter.AllowFilter = False
    Me.xx_datord.Visible = True
    Me.xx_datord.VisibleIndex = 15
    '
    'mo_datcons
    '
    Me.mo_datcons.AppearanceCell.Options.UseBackColor = True
    Me.mo_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datcons.Caption = "Data cons."
    Me.mo_datcons.Enabled = False
    Me.mo_datcons.FieldName = "mo_datcons"
    Me.mo_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datcons.Name = "mo_datcons"
    Me.mo_datcons.NTSRepositoryComboBox = Nothing
    Me.mo_datcons.NTSRepositoryItemCheck = Nothing
    Me.mo_datcons.NTSRepositoryItemMemo = Nothing
    Me.mo_datcons.NTSRepositoryItemText = Nothing
    Me.mo_datcons.OptionsColumn.AllowEdit = False
    Me.mo_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datcons.OptionsColumn.ReadOnly = True
    Me.mo_datcons.OptionsFilter.AllowFilter = False
    Me.mo_datcons.Visible = True
    Me.mo_datcons.VisibleIndex = 16
    '
    'mo_quant
    '
    Me.mo_quant.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant.Caption = "Qta ordin."
    Me.mo_quant.Enabled = False
    Me.mo_quant.FieldName = "mo_quant"
    Me.mo_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant.Name = "mo_quant"
    Me.mo_quant.NTSRepositoryComboBox = Nothing
    Me.mo_quant.NTSRepositoryItemCheck = Nothing
    Me.mo_quant.NTSRepositoryItemMemo = Nothing
    Me.mo_quant.NTSRepositoryItemText = Nothing
    Me.mo_quant.OptionsColumn.AllowEdit = False
    Me.mo_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant.OptionsColumn.ReadOnly = True
    Me.mo_quant.OptionsFilter.AllowFilter = False
    Me.mo_quant.Visible = True
    Me.mo_quant.VisibleIndex = 17
    '
    'mo_quapre
    '
    Me.mo_quapre.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre.Caption = "Qta pren."
    Me.mo_quapre.Enabled = False
    Me.mo_quapre.FieldName = "mo_quapre"
    Me.mo_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre.Name = "mo_quapre"
    Me.mo_quapre.NTSRepositoryComboBox = Nothing
    Me.mo_quapre.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre.NTSRepositoryItemText = Nothing
    Me.mo_quapre.OptionsColumn.AllowEdit = False
    Me.mo_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre.OptionsColumn.ReadOnly = True
    Me.mo_quapre.OptionsFilter.AllowFilter = False
    Me.mo_quapre.Visible = True
    Me.mo_quapre.VisibleIndex = 18
    '
    'mo_quaeva
    '
    Me.mo_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva.Caption = "Qta evasa"
    Me.mo_quaeva.Enabled = False
    Me.mo_quaeva.FieldName = "mo_quaeva"
    Me.mo_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva.Name = "mo_quaeva"
    Me.mo_quaeva.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva.NTSRepositoryItemText = Nothing
    Me.mo_quaeva.OptionsColumn.AllowEdit = False
    Me.mo_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva.OptionsColumn.ReadOnly = True
    Me.mo_quaeva.OptionsFilter.AllowFilter = False
    Me.mo_quaeva.Visible = True
    Me.mo_quaeva.VisibleIndex = 19
    '
    'xx_qtares
    '
    Me.xx_qtares.AppearanceCell.Options.UseBackColor = True
    Me.xx_qtares.AppearanceCell.Options.UseTextOptions = True
    Me.xx_qtares.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_qtares.Caption = "Qta resid."
    Me.xx_qtares.Enabled = False
    Me.xx_qtares.FieldName = "xx_qtares"
    Me.xx_qtares.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_qtares.Name = "xx_qtares"
    Me.xx_qtares.NTSRepositoryComboBox = Nothing
    Me.xx_qtares.NTSRepositoryItemCheck = Nothing
    Me.xx_qtares.NTSRepositoryItemMemo = Nothing
    Me.xx_qtares.NTSRepositoryItemText = Nothing
    Me.xx_qtares.OptionsColumn.AllowEdit = False
    Me.xx_qtares.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_qtares.OptionsColumn.ReadOnly = True
    Me.xx_qtares.OptionsFilter.AllowFilter = False
    Me.xx_qtares.Visible = True
    Me.xx_qtares.VisibleIndex = 20
    '
    'mo_valore
    '
    Me.mo_valore.AppearanceCell.Options.UseBackColor = True
    Me.mo_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mo_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_valore.Caption = "Valore resid."
    Me.mo_valore.Enabled = False
    Me.mo_valore.FieldName = "mo_valore"
    Me.mo_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_valore.Name = "mo_valore"
    Me.mo_valore.NTSRepositoryComboBox = Nothing
    Me.mo_valore.NTSRepositoryItemCheck = Nothing
    Me.mo_valore.NTSRepositoryItemMemo = Nothing
    Me.mo_valore.NTSRepositoryItemText = Nothing
    Me.mo_valore.OptionsColumn.AllowEdit = False
    Me.mo_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_valore.OptionsColumn.ReadOnly = True
    Me.mo_valore.OptionsFilter.AllowFilter = False
    Me.mo_valore.Visible = True
    Me.mo_valore.VisibleIndex = 21
    '
    'xx_annpar
    '
    Me.xx_annpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_annpar.Caption = "Anno P."
    Me.xx_annpar.Enabled = False
    Me.xx_annpar.FieldName = "xx_annpar"
    Me.xx_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_annpar.Name = "xx_annpar"
    Me.xx_annpar.NTSRepositoryComboBox = Nothing
    Me.xx_annpar.NTSRepositoryItemCheck = Nothing
    Me.xx_annpar.NTSRepositoryItemMemo = Nothing
    Me.xx_annpar.NTSRepositoryItemText = Nothing
    Me.xx_annpar.OptionsColumn.AllowEdit = False
    Me.xx_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_annpar.OptionsColumn.ReadOnly = True
    Me.xx_annpar.OptionsFilter.AllowFilter = False
    Me.xx_annpar.Visible = True
    Me.xx_annpar.VisibleIndex = 22
    '
    'xx_alfpar
    '
    Me.xx_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_alfpar.Caption = "Serie P."
    Me.xx_alfpar.Enabled = False
    Me.xx_alfpar.FieldName = "xx_alfpar"
    Me.xx_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_alfpar.Name = "xx_alfpar"
    Me.xx_alfpar.NTSRepositoryComboBox = Nothing
    Me.xx_alfpar.NTSRepositoryItemCheck = Nothing
    Me.xx_alfpar.NTSRepositoryItemMemo = Nothing
    Me.xx_alfpar.NTSRepositoryItemText = Nothing
    Me.xx_alfpar.OptionsColumn.AllowEdit = False
    Me.xx_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_alfpar.OptionsColumn.ReadOnly = True
    Me.xx_alfpar.OptionsFilter.AllowFilter = False
    Me.xx_alfpar.Visible = True
    Me.xx_alfpar.VisibleIndex = 23
    '
    'xx_numpar
    '
    Me.xx_numpar.AppearanceCell.Options.UseBackColor = True
    Me.xx_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numpar.Caption = "Num. P."
    Me.xx_numpar.Enabled = False
    Me.xx_numpar.FieldName = "xx_numpar"
    Me.xx_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numpar.Name = "xx_numpar"
    Me.xx_numpar.NTSRepositoryComboBox = Nothing
    Me.xx_numpar.NTSRepositoryItemCheck = Nothing
    Me.xx_numpar.NTSRepositoryItemMemo = Nothing
    Me.xx_numpar.NTSRepositoryItemText = Nothing
    Me.xx_numpar.OptionsColumn.AllowEdit = False
    Me.xx_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numpar.OptionsColumn.ReadOnly = True
    Me.xx_numpar.OptionsFilter.AllowFilter = False
    Me.xx_numpar.Visible = True
    Me.xx_numpar.VisibleIndex = 24
    '
    'xx_riferim
    '
    Me.xx_riferim.AppearanceCell.Options.UseBackColor = True
    Me.xx_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.xx_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_riferim.Caption = "Riferimenti"
    Me.xx_riferim.Enabled = False
    Me.xx_riferim.FieldName = "xx_riferim"
    Me.xx_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_riferim.Name = "xx_riferim"
    Me.xx_riferim.NTSRepositoryComboBox = Nothing
    Me.xx_riferim.NTSRepositoryItemCheck = Nothing
    Me.xx_riferim.NTSRepositoryItemMemo = Nothing
    Me.xx_riferim.NTSRepositoryItemText = Nothing
    Me.xx_riferim.OptionsColumn.AllowEdit = False
    Me.xx_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_riferim.OptionsColumn.ReadOnly = True
    Me.xx_riferim.OptionsFilter.AllowFilter = False
    Me.xx_riferim.Visible = True
    Me.xx_riferim.VisibleIndex = 25
    '
    'mo_note
    '
    Me.mo_note.AppearanceCell.Options.UseBackColor = True
    Me.mo_note.AppearanceCell.Options.UseTextOptions = True
    Me.mo_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_note.Caption = "Note riga"
    Me.mo_note.Enabled = False
    Me.mo_note.FieldName = "mo_note"
    Me.mo_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_note.Name = "mo_note"
    Me.mo_note.NTSRepositoryComboBox = Nothing
    Me.mo_note.NTSRepositoryItemCheck = Nothing
    Me.mo_note.NTSRepositoryItemMemo = Nothing
    Me.mo_note.NTSRepositoryItemText = Nothing
    Me.mo_note.OptionsColumn.AllowEdit = False
    Me.mo_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_note.OptionsColumn.ReadOnly = True
    Me.mo_note.OptionsFilter.AllowFilter = False
    Me.mo_note.Visible = True
    Me.mo_note.VisibleIndex = 26
    '
    'mo_prezzo
    '
    Me.mo_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezzo.Caption = "Prezzo unitario"
    Me.mo_prezzo.Enabled = False
    Me.mo_prezzo.FieldName = "mo_prezzo"
    Me.mo_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezzo.Name = "mo_prezzo"
    Me.mo_prezzo.NTSRepositoryComboBox = Nothing
    Me.mo_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mo_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mo_prezzo.NTSRepositoryItemText = Nothing
    Me.mo_prezzo.OptionsColumn.AllowEdit = False
    Me.mo_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezzo.OptionsColumn.ReadOnly = True
    Me.mo_prezzo.OptionsFilter.AllowFilter = False
    Me.mo_prezzo.Visible = True
    Me.mo_prezzo.VisibleIndex = 27
    '
    'mo_scont1
    '
    Me.mo_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont1.Caption = "Sc. 1"
    Me.mo_scont1.Enabled = False
    Me.mo_scont1.FieldName = "mo_scont1"
    Me.mo_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont1.Name = "mo_scont1"
    Me.mo_scont1.NTSRepositoryComboBox = Nothing
    Me.mo_scont1.NTSRepositoryItemCheck = Nothing
    Me.mo_scont1.NTSRepositoryItemMemo = Nothing
    Me.mo_scont1.NTSRepositoryItemText = Nothing
    Me.mo_scont1.OptionsColumn.AllowEdit = False
    Me.mo_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont1.OptionsColumn.ReadOnly = True
    Me.mo_scont1.OptionsFilter.AllowFilter = False
    Me.mo_scont1.Visible = True
    Me.mo_scont1.VisibleIndex = 28
    '
    'mo_scont2
    '
    Me.mo_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont2.Caption = "Sc. 2"
    Me.mo_scont2.Enabled = False
    Me.mo_scont2.FieldName = "mo_scont2"
    Me.mo_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont2.Name = "mo_scont2"
    Me.mo_scont2.NTSRepositoryComboBox = Nothing
    Me.mo_scont2.NTSRepositoryItemCheck = Nothing
    Me.mo_scont2.NTSRepositoryItemMemo = Nothing
    Me.mo_scont2.NTSRepositoryItemText = Nothing
    Me.mo_scont2.OptionsColumn.AllowEdit = False
    Me.mo_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont2.OptionsColumn.ReadOnly = True
    Me.mo_scont2.OptionsFilter.AllowFilter = False
    Me.mo_scont2.Visible = True
    Me.mo_scont2.VisibleIndex = 29
    '
    'mo_scont3
    '
    Me.mo_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont3.Caption = "Sc. 3"
    Me.mo_scont3.Enabled = False
    Me.mo_scont3.FieldName = "mo_scont3"
    Me.mo_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont3.Name = "mo_scont3"
    Me.mo_scont3.NTSRepositoryComboBox = Nothing
    Me.mo_scont3.NTSRepositoryItemCheck = Nothing
    Me.mo_scont3.NTSRepositoryItemMemo = Nothing
    Me.mo_scont3.NTSRepositoryItemText = Nothing
    Me.mo_scont3.OptionsColumn.AllowEdit = False
    Me.mo_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont3.OptionsColumn.ReadOnly = True
    Me.mo_scont3.OptionsFilter.AllowFilter = False
    Me.mo_scont3.Visible = True
    Me.mo_scont3.VisibleIndex = 30
    '
    'mo_scont4
    '
    Me.mo_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont4.Caption = "Sc. 4"
    Me.mo_scont4.Enabled = False
    Me.mo_scont4.FieldName = "mo_scont4"
    Me.mo_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont4.Name = "mo_scont4"
    Me.mo_scont4.NTSRepositoryComboBox = Nothing
    Me.mo_scont4.NTSRepositoryItemCheck = Nothing
    Me.mo_scont4.NTSRepositoryItemMemo = Nothing
    Me.mo_scont4.NTSRepositoryItemText = Nothing
    Me.mo_scont4.OptionsColumn.AllowEdit = False
    Me.mo_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont4.OptionsColumn.ReadOnly = True
    Me.mo_scont4.OptionsFilter.AllowFilter = False
    Me.mo_scont4.Visible = True
    Me.mo_scont4.VisibleIndex = 31
    '
    'mo_scont5
    '
    Me.mo_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont5.Caption = "Sc. 5"
    Me.mo_scont5.Enabled = False
    Me.mo_scont5.FieldName = "mo_scont5"
    Me.mo_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont5.Name = "mo_scont5"
    Me.mo_scont5.NTSRepositoryComboBox = Nothing
    Me.mo_scont5.NTSRepositoryItemCheck = Nothing
    Me.mo_scont5.NTSRepositoryItemMemo = Nothing
    Me.mo_scont5.NTSRepositoryItemText = Nothing
    Me.mo_scont5.OptionsColumn.AllowEdit = False
    Me.mo_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont5.OptionsColumn.ReadOnly = True
    Me.mo_scont5.OptionsFilter.AllowFilter = False
    Me.mo_scont5.Visible = True
    Me.mo_scont5.VisibleIndex = 32
    '
    'mo_scont6
    '
    Me.mo_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont6.Caption = "Sc. 6"
    Me.mo_scont6.Enabled = False
    Me.mo_scont6.FieldName = "mo_scont6"
    Me.mo_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont6.Name = "mo_scont6"
    Me.mo_scont6.NTSRepositoryComboBox = Nothing
    Me.mo_scont6.NTSRepositoryItemCheck = Nothing
    Me.mo_scont6.NTSRepositoryItemMemo = Nothing
    Me.mo_scont6.NTSRepositoryItemText = Nothing
    Me.mo_scont6.OptionsColumn.AllowEdit = False
    Me.mo_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont6.OptionsColumn.ReadOnly = True
    Me.mo_scont6.OptionsFilter.AllowFilter = False
    Me.mo_scont6.Visible = True
    Me.mo_scont6.VisibleIndex = 33
    '
    'xx_prezzonetto
    '
    Me.xx_prezzonetto.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzonetto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzonetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzonetto.Caption = "Prezzo netto"
    Me.xx_prezzonetto.Enabled = False
    Me.xx_prezzonetto.FieldName = "xx_prezzonetto"
    Me.xx_prezzonetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzonetto.Name = "xx_prezzonetto"
    Me.xx_prezzonetto.NTSRepositoryComboBox = Nothing
    Me.xx_prezzonetto.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzonetto.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzonetto.NTSRepositoryItemText = Nothing
    Me.xx_prezzonetto.OptionsColumn.AllowEdit = False
    Me.xx_prezzonetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzonetto.OptionsColumn.ReadOnly = True
    Me.xx_prezzonetto.OptionsFilter.AllowFilter = False
    Me.xx_prezzonetto.Visible = True
    Me.xx_prezzonetto.VisibleIndex = 34
    '
    'mo_commeca
    '
    Me.mo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_commeca.Caption = "Commessa"
    Me.mo_commeca.Enabled = False
    Me.mo_commeca.FieldName = "mo_commeca"
    Me.mo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_commeca.Name = "mo_commeca"
    Me.mo_commeca.NTSRepositoryComboBox = Nothing
    Me.mo_commeca.NTSRepositoryItemCheck = Nothing
    Me.mo_commeca.NTSRepositoryItemMemo = Nothing
    Me.mo_commeca.NTSRepositoryItemText = Nothing
    Me.mo_commeca.OptionsColumn.AllowEdit = False
    Me.mo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_commeca.OptionsColumn.ReadOnly = True
    Me.mo_commeca.OptionsFilter.AllowFilter = False
    Me.mo_commeca.Visible = True
    Me.mo_commeca.VisibleIndex = 35
    '
    'mo_subcommeca
    '
    Me.mo_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_subcommeca.Caption = "S. comm."
    Me.mo_subcommeca.Enabled = False
    Me.mo_subcommeca.FieldName = "mo_subcommeca"
    Me.mo_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_subcommeca.Name = "mo_subcommeca"
    Me.mo_subcommeca.NTSRepositoryComboBox = Nothing
    Me.mo_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.mo_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.mo_subcommeca.NTSRepositoryItemText = Nothing
    Me.mo_subcommeca.OptionsColumn.AllowEdit = False
    Me.mo_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_subcommeca.OptionsColumn.ReadOnly = True
    Me.mo_subcommeca.OptionsFilter.AllowFilter = False
    Me.mo_subcommeca.Visible = True
    Me.mo_subcommeca.VisibleIndex = 36
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
    Me.xx_commeca.VisibleIndex = 37
    '
    'xx_articopadre
    '
    Me.xx_articopadre.AppearanceCell.Options.UseBackColor = True
    Me.xx_articopadre.AppearanceCell.Options.UseTextOptions = True
    Me.xx_articopadre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_articopadre.Caption = "Articolo padre"
    Me.xx_articopadre.Enabled = False
    Me.xx_articopadre.FieldName = "xx_articopadre"
    Me.xx_articopadre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_articopadre.Name = "xx_articopadre"
    Me.xx_articopadre.NTSRepositoryComboBox = Nothing
    Me.xx_articopadre.NTSRepositoryItemCheck = Nothing
    Me.xx_articopadre.NTSRepositoryItemMemo = Nothing
    Me.xx_articopadre.NTSRepositoryItemText = Nothing
    Me.xx_articopadre.OptionsColumn.AllowEdit = False
    Me.xx_articopadre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_articopadre.OptionsColumn.ReadOnly = True
    Me.xx_articopadre.OptionsFilter.AllowFilter = False
    Me.xx_articopadre.Visible = True
    Me.xx_articopadre.VisibleIndex = 38
    '
    'xx_desarticopadre
    '
    Me.xx_desarticopadre.AppearanceCell.Options.UseBackColor = True
    Me.xx_desarticopadre.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desarticopadre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desarticopadre.Caption = "Descr. articolo padre"
    Me.xx_desarticopadre.Enabled = False
    Me.xx_desarticopadre.FieldName = "xx_desarticopadre"
    Me.xx_desarticopadre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desarticopadre.Name = "xx_desarticopadre"
    Me.xx_desarticopadre.NTSRepositoryComboBox = Nothing
    Me.xx_desarticopadre.NTSRepositoryItemCheck = Nothing
    Me.xx_desarticopadre.NTSRepositoryItemMemo = Nothing
    Me.xx_desarticopadre.NTSRepositoryItemText = Nothing
    Me.xx_desarticopadre.OptionsColumn.AllowEdit = False
    Me.xx_desarticopadre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desarticopadre.OptionsColumn.ReadOnly = True
    Me.xx_desarticopadre.OptionsFilter.AllowFilter = False
    Me.xx_desarticopadre.Visible = True
    Me.xx_desarticopadre.VisibleIndex = 39
    '
    'xx_agente
    '
    Me.xx_agente.AppearanceCell.Options.UseBackColor = True
    Me.xx_agente.AppearanceCell.Options.UseTextOptions = True
    Me.xx_agente.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_agente.Caption = "Agente"
    Me.xx_agente.Enabled = False
    Me.xx_agente.FieldName = "xx_agente"
    Me.xx_agente.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_agente.Name = "xx_agente"
    Me.xx_agente.NTSRepositoryComboBox = Nothing
    Me.xx_agente.NTSRepositoryItemCheck = Nothing
    Me.xx_agente.NTSRepositoryItemMemo = Nothing
    Me.xx_agente.NTSRepositoryItemText = Nothing
    Me.xx_agente.OptionsColumn.AllowEdit = False
    Me.xx_agente.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_agente.OptionsColumn.ReadOnly = True
    Me.xx_agente.OptionsFilter.AllowFilter = False
    Me.xx_agente.Visible = True
    Me.xx_agente.VisibleIndex = 40
    '
    'xx_desagente
    '
    Me.xx_desagente.AppearanceCell.Options.UseBackColor = True
    Me.xx_desagente.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desagente.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desagente.Caption = "Descr. agente"
    Me.xx_desagente.Enabled = False
    Me.xx_desagente.FieldName = "xx_desagente"
    Me.xx_desagente.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desagente.Name = "xx_desagente"
    Me.xx_desagente.NTSRepositoryComboBox = Nothing
    Me.xx_desagente.NTSRepositoryItemCheck = Nothing
    Me.xx_desagente.NTSRepositoryItemMemo = Nothing
    Me.xx_desagente.NTSRepositoryItemText = Nothing
    Me.xx_desagente.OptionsColumn.AllowEdit = False
    Me.xx_desagente.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desagente.OptionsColumn.ReadOnly = True
    Me.xx_desagente.OptionsFilter.AllowFilter = False
    Me.xx_desagente.Visible = True
    Me.xx_desagente.VisibleIndex = 41
    '
    'xx_codappr
    '
    Me.xx_codappr.AppearanceCell.Options.UseBackColor = True
    Me.xx_codappr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codappr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codappr.Caption = "Approvvig."
    Me.xx_codappr.Enabled = False
    Me.xx_codappr.FieldName = "xx_codappr"
    Me.xx_codappr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codappr.Name = "xx_codappr"
    Me.xx_codappr.NTSRepositoryComboBox = Nothing
    Me.xx_codappr.NTSRepositoryItemCheck = Nothing
    Me.xx_codappr.NTSRepositoryItemMemo = Nothing
    Me.xx_codappr.NTSRepositoryItemText = Nothing
    Me.xx_codappr.OptionsColumn.AllowEdit = False
    Me.xx_codappr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codappr.OptionsColumn.ReadOnly = True
    Me.xx_codappr.OptionsFilter.AllowFilter = False
    Me.xx_codappr.Visible = True
    Me.xx_codappr.VisibleIndex = 42
    '
    'xx_desappr
    '
    Me.xx_desappr.AppearanceCell.Options.UseBackColor = True
    Me.xx_desappr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desappr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desappr.Caption = "Descr. approvvig."
    Me.xx_desappr.Enabled = False
    Me.xx_desappr.FieldName = "xx_desappr"
    Me.xx_desappr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desappr.Name = "xx_desappr"
    Me.xx_desappr.NTSRepositoryComboBox = Nothing
    Me.xx_desappr.NTSRepositoryItemCheck = Nothing
    Me.xx_desappr.NTSRepositoryItemMemo = Nothing
    Me.xx_desappr.NTSRepositoryItemText = Nothing
    Me.xx_desappr.OptionsColumn.AllowEdit = False
    Me.xx_desappr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desappr.OptionsColumn.ReadOnly = True
    Me.xx_desappr.OptionsFilter.AllowFilter = False
    Me.xx_desappr.Visible = True
    Me.xx_desappr.VisibleIndex = 43
    '
    'mo_desint
    '
    Me.mo_desint.AppearanceCell.Options.UseBackColor = True
    Me.mo_desint.AppearanceCell.Options.UseTextOptions = True
    Me.mo_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_desint.Caption = "Descr. interna art"
    Me.mo_desint.Enabled = False
    Me.mo_desint.FieldName = "mo_desint"
    Me.mo_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_desint.Name = "mo_desint"
    Me.mo_desint.NTSRepositoryComboBox = Nothing
    Me.mo_desint.NTSRepositoryItemCheck = Nothing
    Me.mo_desint.NTSRepositoryItemMemo = Nothing
    Me.mo_desint.NTSRepositoryItemText = Nothing
    Me.mo_desint.OptionsColumn.AllowEdit = False
    Me.mo_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_desint.OptionsColumn.ReadOnly = True
    Me.mo_desint.OptionsFilter.AllowFilter = False
    Me.mo_desint.Visible = True
    Me.mo_desint.VisibleIndex = 44
    '
    'xx_codalt
    '
    Me.xx_codalt.AppearanceCell.Options.UseBackColor = True
    Me.xx_codalt.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codalt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codalt.Caption = "Cod. alternativo articolo"
    Me.xx_codalt.Enabled = False
    Me.xx_codalt.FieldName = "xx_codalt"
    Me.xx_codalt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codalt.Name = "xx_codalt"
    Me.xx_codalt.NTSRepositoryComboBox = Nothing
    Me.xx_codalt.NTSRepositoryItemCheck = Nothing
    Me.xx_codalt.NTSRepositoryItemMemo = Nothing
    Me.xx_codalt.NTSRepositoryItemText = Nothing
    Me.xx_codalt.OptionsColumn.AllowEdit = False
    Me.xx_codalt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codalt.OptionsColumn.ReadOnly = True
    Me.xx_codalt.OptionsFilter.AllowFilter = False
    Me.xx_codalt.Visible = True
    Me.xx_codalt.VisibleIndex = 45
    '
    'mo_quant01
    '
    Me.mo_quant01.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant01.Caption = "Q.O.01"
    Me.mo_quant01.Enabled = False
    Me.mo_quant01.FieldName = "mo_quant01"
    Me.mo_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant01.Name = "mo_quant01"
    Me.mo_quant01.NTSRepositoryComboBox = Nothing
    Me.mo_quant01.NTSRepositoryItemCheck = Nothing
    Me.mo_quant01.NTSRepositoryItemMemo = Nothing
    Me.mo_quant01.NTSRepositoryItemText = Nothing
    Me.mo_quant01.OptionsColumn.AllowEdit = False
    Me.mo_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant01.OptionsColumn.ReadOnly = True
    Me.mo_quant01.OptionsFilter.AllowFilter = False
    Me.mo_quant01.Visible = True
    Me.mo_quant01.VisibleIndex = 46
    '
    'mo_quant02
    '
    Me.mo_quant02.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant02.Caption = "Q.O.02"
    Me.mo_quant02.Enabled = False
    Me.mo_quant02.FieldName = "mo_quant02"
    Me.mo_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant02.Name = "mo_quant02"
    Me.mo_quant02.NTSRepositoryComboBox = Nothing
    Me.mo_quant02.NTSRepositoryItemCheck = Nothing
    Me.mo_quant02.NTSRepositoryItemMemo = Nothing
    Me.mo_quant02.NTSRepositoryItemText = Nothing
    Me.mo_quant02.OptionsColumn.AllowEdit = False
    Me.mo_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant02.OptionsColumn.ReadOnly = True
    Me.mo_quant02.OptionsFilter.AllowFilter = False
    Me.mo_quant02.Visible = True
    Me.mo_quant02.VisibleIndex = 47
    '
    'mo_quant03
    '
    Me.mo_quant03.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant03.Caption = "Q.O.03"
    Me.mo_quant03.Enabled = False
    Me.mo_quant03.FieldName = "mo_quant03"
    Me.mo_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant03.Name = "mo_quant03"
    Me.mo_quant03.NTSRepositoryComboBox = Nothing
    Me.mo_quant03.NTSRepositoryItemCheck = Nothing
    Me.mo_quant03.NTSRepositoryItemMemo = Nothing
    Me.mo_quant03.NTSRepositoryItemText = Nothing
    Me.mo_quant03.OptionsColumn.AllowEdit = False
    Me.mo_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant03.OptionsColumn.ReadOnly = True
    Me.mo_quant03.OptionsFilter.AllowFilter = False
    Me.mo_quant03.Visible = True
    Me.mo_quant03.VisibleIndex = 48
    '
    'mo_quant04
    '
    Me.mo_quant04.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant04.Caption = "Q.O.04"
    Me.mo_quant04.Enabled = False
    Me.mo_quant04.FieldName = "mo_quant04"
    Me.mo_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant04.Name = "mo_quant04"
    Me.mo_quant04.NTSRepositoryComboBox = Nothing
    Me.mo_quant04.NTSRepositoryItemCheck = Nothing
    Me.mo_quant04.NTSRepositoryItemMemo = Nothing
    Me.mo_quant04.NTSRepositoryItemText = Nothing
    Me.mo_quant04.OptionsColumn.AllowEdit = False
    Me.mo_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant04.OptionsColumn.ReadOnly = True
    Me.mo_quant04.OptionsFilter.AllowFilter = False
    Me.mo_quant04.Visible = True
    Me.mo_quant04.VisibleIndex = 49
    '
    'mo_quant05
    '
    Me.mo_quant05.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant05.Caption = "Q.O.05"
    Me.mo_quant05.Enabled = False
    Me.mo_quant05.FieldName = "mo_quant05"
    Me.mo_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant05.Name = "mo_quant05"
    Me.mo_quant05.NTSRepositoryComboBox = Nothing
    Me.mo_quant05.NTSRepositoryItemCheck = Nothing
    Me.mo_quant05.NTSRepositoryItemMemo = Nothing
    Me.mo_quant05.NTSRepositoryItemText = Nothing
    Me.mo_quant05.OptionsColumn.AllowEdit = False
    Me.mo_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant05.OptionsColumn.ReadOnly = True
    Me.mo_quant05.OptionsFilter.AllowFilter = False
    Me.mo_quant05.Visible = True
    Me.mo_quant05.VisibleIndex = 50
    '
    'mo_quant06
    '
    Me.mo_quant06.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant06.Caption = "Q.O.06"
    Me.mo_quant06.Enabled = False
    Me.mo_quant06.FieldName = "mo_quant06"
    Me.mo_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant06.Name = "mo_quant06"
    Me.mo_quant06.NTSRepositoryComboBox = Nothing
    Me.mo_quant06.NTSRepositoryItemCheck = Nothing
    Me.mo_quant06.NTSRepositoryItemMemo = Nothing
    Me.mo_quant06.NTSRepositoryItemText = Nothing
    Me.mo_quant06.OptionsColumn.AllowEdit = False
    Me.mo_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant06.OptionsColumn.ReadOnly = True
    Me.mo_quant06.OptionsFilter.AllowFilter = False
    Me.mo_quant06.Visible = True
    Me.mo_quant06.VisibleIndex = 51
    '
    'mo_quant07
    '
    Me.mo_quant07.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant07.Caption = "Q.O.07"
    Me.mo_quant07.Enabled = False
    Me.mo_quant07.FieldName = "mo_quant07"
    Me.mo_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant07.Name = "mo_quant07"
    Me.mo_quant07.NTSRepositoryComboBox = Nothing
    Me.mo_quant07.NTSRepositoryItemCheck = Nothing
    Me.mo_quant07.NTSRepositoryItemMemo = Nothing
    Me.mo_quant07.NTSRepositoryItemText = Nothing
    Me.mo_quant07.OptionsColumn.AllowEdit = False
    Me.mo_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant07.OptionsColumn.ReadOnly = True
    Me.mo_quant07.OptionsFilter.AllowFilter = False
    Me.mo_quant07.Visible = True
    Me.mo_quant07.VisibleIndex = 52
    '
    'mo_quant08
    '
    Me.mo_quant08.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant08.Caption = "Q.O.08"
    Me.mo_quant08.Enabled = False
    Me.mo_quant08.FieldName = "mo_quant08"
    Me.mo_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant08.Name = "mo_quant08"
    Me.mo_quant08.NTSRepositoryComboBox = Nothing
    Me.mo_quant08.NTSRepositoryItemCheck = Nothing
    Me.mo_quant08.NTSRepositoryItemMemo = Nothing
    Me.mo_quant08.NTSRepositoryItemText = Nothing
    Me.mo_quant08.OptionsColumn.AllowEdit = False
    Me.mo_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant08.OptionsColumn.ReadOnly = True
    Me.mo_quant08.OptionsFilter.AllowFilter = False
    Me.mo_quant08.Visible = True
    Me.mo_quant08.VisibleIndex = 53
    '
    'mo_quant09
    '
    Me.mo_quant09.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant09.Caption = "Q.O.09"
    Me.mo_quant09.Enabled = False
    Me.mo_quant09.FieldName = "mo_quant09"
    Me.mo_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant09.Name = "mo_quant09"
    Me.mo_quant09.NTSRepositoryComboBox = Nothing
    Me.mo_quant09.NTSRepositoryItemCheck = Nothing
    Me.mo_quant09.NTSRepositoryItemMemo = Nothing
    Me.mo_quant09.NTSRepositoryItemText = Nothing
    Me.mo_quant09.OptionsColumn.AllowEdit = False
    Me.mo_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant09.OptionsColumn.ReadOnly = True
    Me.mo_quant09.OptionsFilter.AllowFilter = False
    Me.mo_quant09.Visible = True
    Me.mo_quant09.VisibleIndex = 54
    '
    'mo_quant10
    '
    Me.mo_quant10.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant10.Caption = "Q.O.10"
    Me.mo_quant10.Enabled = False
    Me.mo_quant10.FieldName = "mo_quant10"
    Me.mo_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant10.Name = "mo_quant10"
    Me.mo_quant10.NTSRepositoryComboBox = Nothing
    Me.mo_quant10.NTSRepositoryItemCheck = Nothing
    Me.mo_quant10.NTSRepositoryItemMemo = Nothing
    Me.mo_quant10.NTSRepositoryItemText = Nothing
    Me.mo_quant10.OptionsColumn.AllowEdit = False
    Me.mo_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant10.OptionsColumn.ReadOnly = True
    Me.mo_quant10.OptionsFilter.AllowFilter = False
    Me.mo_quant10.Visible = True
    Me.mo_quant10.VisibleIndex = 55
    '
    'mo_quant11
    '
    Me.mo_quant11.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant11.Caption = "Q.O.11"
    Me.mo_quant11.Enabled = False
    Me.mo_quant11.FieldName = "mo_quant11"
    Me.mo_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant11.Name = "mo_quant11"
    Me.mo_quant11.NTSRepositoryComboBox = Nothing
    Me.mo_quant11.NTSRepositoryItemCheck = Nothing
    Me.mo_quant11.NTSRepositoryItemMemo = Nothing
    Me.mo_quant11.NTSRepositoryItemText = Nothing
    Me.mo_quant11.OptionsColumn.AllowEdit = False
    Me.mo_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant11.OptionsColumn.ReadOnly = True
    Me.mo_quant11.OptionsFilter.AllowFilter = False
    Me.mo_quant11.Visible = True
    Me.mo_quant11.VisibleIndex = 56
    '
    'mo_quant12
    '
    Me.mo_quant12.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant12.Caption = "Q.O.12"
    Me.mo_quant12.Enabled = False
    Me.mo_quant12.FieldName = "mo_quant12"
    Me.mo_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant12.Name = "mo_quant12"
    Me.mo_quant12.NTSRepositoryComboBox = Nothing
    Me.mo_quant12.NTSRepositoryItemCheck = Nothing
    Me.mo_quant12.NTSRepositoryItemMemo = Nothing
    Me.mo_quant12.NTSRepositoryItemText = Nothing
    Me.mo_quant12.OptionsColumn.AllowEdit = False
    Me.mo_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant12.OptionsColumn.ReadOnly = True
    Me.mo_quant12.OptionsFilter.AllowFilter = False
    Me.mo_quant12.Visible = True
    Me.mo_quant12.VisibleIndex = 57
    '
    'mo_quant13
    '
    Me.mo_quant13.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant13.Caption = "Q.O.13"
    Me.mo_quant13.Enabled = False
    Me.mo_quant13.FieldName = "mo_quant13"
    Me.mo_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant13.Name = "mo_quant13"
    Me.mo_quant13.NTSRepositoryComboBox = Nothing
    Me.mo_quant13.NTSRepositoryItemCheck = Nothing
    Me.mo_quant13.NTSRepositoryItemMemo = Nothing
    Me.mo_quant13.NTSRepositoryItemText = Nothing
    Me.mo_quant13.OptionsColumn.AllowEdit = False
    Me.mo_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant13.OptionsColumn.ReadOnly = True
    Me.mo_quant13.OptionsFilter.AllowFilter = False
    Me.mo_quant13.Visible = True
    Me.mo_quant13.VisibleIndex = 58
    '
    'mo_quant14
    '
    Me.mo_quant14.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant14.Caption = "Q.O.14"
    Me.mo_quant14.Enabled = False
    Me.mo_quant14.FieldName = "mo_quant14"
    Me.mo_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant14.Name = "mo_quant14"
    Me.mo_quant14.NTSRepositoryComboBox = Nothing
    Me.mo_quant14.NTSRepositoryItemCheck = Nothing
    Me.mo_quant14.NTSRepositoryItemMemo = Nothing
    Me.mo_quant14.NTSRepositoryItemText = Nothing
    Me.mo_quant14.OptionsColumn.AllowEdit = False
    Me.mo_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant14.OptionsColumn.ReadOnly = True
    Me.mo_quant14.OptionsFilter.AllowFilter = False
    Me.mo_quant14.Visible = True
    Me.mo_quant14.VisibleIndex = 59
    '
    'mo_quant15
    '
    Me.mo_quant15.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant15.Caption = "Q.O.15"
    Me.mo_quant15.Enabled = False
    Me.mo_quant15.FieldName = "mo_quant15"
    Me.mo_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant15.Name = "mo_quant15"
    Me.mo_quant15.NTSRepositoryComboBox = Nothing
    Me.mo_quant15.NTSRepositoryItemCheck = Nothing
    Me.mo_quant15.NTSRepositoryItemMemo = Nothing
    Me.mo_quant15.NTSRepositoryItemText = Nothing
    Me.mo_quant15.OptionsColumn.AllowEdit = False
    Me.mo_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant15.OptionsColumn.ReadOnly = True
    Me.mo_quant15.OptionsFilter.AllowFilter = False
    Me.mo_quant15.Visible = True
    Me.mo_quant15.VisibleIndex = 60
    '
    'mo_quant16
    '
    Me.mo_quant16.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant16.Caption = "Q.O.16"
    Me.mo_quant16.Enabled = False
    Me.mo_quant16.FieldName = "mo_quant16"
    Me.mo_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant16.Name = "mo_quant16"
    Me.mo_quant16.NTSRepositoryComboBox = Nothing
    Me.mo_quant16.NTSRepositoryItemCheck = Nothing
    Me.mo_quant16.NTSRepositoryItemMemo = Nothing
    Me.mo_quant16.NTSRepositoryItemText = Nothing
    Me.mo_quant16.OptionsColumn.AllowEdit = False
    Me.mo_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant16.OptionsColumn.ReadOnly = True
    Me.mo_quant16.OptionsFilter.AllowFilter = False
    Me.mo_quant16.Visible = True
    Me.mo_quant16.VisibleIndex = 61
    '
    'mo_quant17
    '
    Me.mo_quant17.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant17.Caption = "Q.O.17"
    Me.mo_quant17.Enabled = False
    Me.mo_quant17.FieldName = "mo_quant17"
    Me.mo_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant17.Name = "mo_quant17"
    Me.mo_quant17.NTSRepositoryComboBox = Nothing
    Me.mo_quant17.NTSRepositoryItemCheck = Nothing
    Me.mo_quant17.NTSRepositoryItemMemo = Nothing
    Me.mo_quant17.NTSRepositoryItemText = Nothing
    Me.mo_quant17.OptionsColumn.AllowEdit = False
    Me.mo_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant17.OptionsColumn.ReadOnly = True
    Me.mo_quant17.OptionsFilter.AllowFilter = False
    Me.mo_quant17.Visible = True
    Me.mo_quant17.VisibleIndex = 62
    '
    'mo_quant18
    '
    Me.mo_quant18.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant18.Caption = "Q.O.18"
    Me.mo_quant18.Enabled = False
    Me.mo_quant18.FieldName = "mo_quant18"
    Me.mo_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant18.Name = "mo_quant18"
    Me.mo_quant18.NTSRepositoryComboBox = Nothing
    Me.mo_quant18.NTSRepositoryItemCheck = Nothing
    Me.mo_quant18.NTSRepositoryItemMemo = Nothing
    Me.mo_quant18.NTSRepositoryItemText = Nothing
    Me.mo_quant18.OptionsColumn.AllowEdit = False
    Me.mo_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant18.OptionsColumn.ReadOnly = True
    Me.mo_quant18.OptionsFilter.AllowFilter = False
    Me.mo_quant18.Visible = True
    Me.mo_quant18.VisibleIndex = 63
    '
    'mo_quant19
    '
    Me.mo_quant19.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant19.Caption = "Q.O.19"
    Me.mo_quant19.Enabled = False
    Me.mo_quant19.FieldName = "mo_quant19"
    Me.mo_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant19.Name = "mo_quant19"
    Me.mo_quant19.NTSRepositoryComboBox = Nothing
    Me.mo_quant19.NTSRepositoryItemCheck = Nothing
    Me.mo_quant19.NTSRepositoryItemMemo = Nothing
    Me.mo_quant19.NTSRepositoryItemText = Nothing
    Me.mo_quant19.OptionsColumn.AllowEdit = False
    Me.mo_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant19.OptionsColumn.ReadOnly = True
    Me.mo_quant19.OptionsFilter.AllowFilter = False
    Me.mo_quant19.Visible = True
    Me.mo_quant19.VisibleIndex = 64
    '
    'mo_quant20
    '
    Me.mo_quant20.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant20.Caption = "Q.O.20"
    Me.mo_quant20.Enabled = False
    Me.mo_quant20.FieldName = "mo_quant20"
    Me.mo_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant20.Name = "mo_quant20"
    Me.mo_quant20.NTSRepositoryComboBox = Nothing
    Me.mo_quant20.NTSRepositoryItemCheck = Nothing
    Me.mo_quant20.NTSRepositoryItemMemo = Nothing
    Me.mo_quant20.NTSRepositoryItemText = Nothing
    Me.mo_quant20.OptionsColumn.AllowEdit = False
    Me.mo_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant20.OptionsColumn.ReadOnly = True
    Me.mo_quant20.OptionsFilter.AllowFilter = False
    Me.mo_quant20.Visible = True
    Me.mo_quant20.VisibleIndex = 65
    '
    'mo_quant21
    '
    Me.mo_quant21.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant21.Caption = "Q.O.21"
    Me.mo_quant21.Enabled = False
    Me.mo_quant21.FieldName = "mo_quant21"
    Me.mo_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant21.Name = "mo_quant21"
    Me.mo_quant21.NTSRepositoryComboBox = Nothing
    Me.mo_quant21.NTSRepositoryItemCheck = Nothing
    Me.mo_quant21.NTSRepositoryItemMemo = Nothing
    Me.mo_quant21.NTSRepositoryItemText = Nothing
    Me.mo_quant21.OptionsColumn.AllowEdit = False
    Me.mo_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant21.OptionsColumn.ReadOnly = True
    Me.mo_quant21.OptionsFilter.AllowFilter = False
    Me.mo_quant21.Visible = True
    Me.mo_quant21.VisibleIndex = 66
    '
    'mo_quant22
    '
    Me.mo_quant22.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant22.Caption = "Q.O.22"
    Me.mo_quant22.Enabled = False
    Me.mo_quant22.FieldName = "mo_quant22"
    Me.mo_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant22.Name = "mo_quant22"
    Me.mo_quant22.NTSRepositoryComboBox = Nothing
    Me.mo_quant22.NTSRepositoryItemCheck = Nothing
    Me.mo_quant22.NTSRepositoryItemMemo = Nothing
    Me.mo_quant22.NTSRepositoryItemText = Nothing
    Me.mo_quant22.OptionsColumn.AllowEdit = False
    Me.mo_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant22.OptionsColumn.ReadOnly = True
    Me.mo_quant22.OptionsFilter.AllowFilter = False
    Me.mo_quant22.Visible = True
    Me.mo_quant22.VisibleIndex = 67
    '
    'mo_quant23
    '
    Me.mo_quant23.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant23.Caption = "Q.O.23"
    Me.mo_quant23.Enabled = False
    Me.mo_quant23.FieldName = "mo_quant23"
    Me.mo_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant23.Name = "mo_quant23"
    Me.mo_quant23.NTSRepositoryComboBox = Nothing
    Me.mo_quant23.NTSRepositoryItemCheck = Nothing
    Me.mo_quant23.NTSRepositoryItemMemo = Nothing
    Me.mo_quant23.NTSRepositoryItemText = Nothing
    Me.mo_quant23.OptionsColumn.AllowEdit = False
    Me.mo_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant23.OptionsColumn.ReadOnly = True
    Me.mo_quant23.OptionsFilter.AllowFilter = False
    Me.mo_quant23.Visible = True
    Me.mo_quant23.VisibleIndex = 68
    '
    'mo_quant24
    '
    Me.mo_quant24.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant24.Caption = "Q.O.24"
    Me.mo_quant24.Enabled = False
    Me.mo_quant24.FieldName = "mo_quant24"
    Me.mo_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant24.Name = "mo_quant24"
    Me.mo_quant24.NTSRepositoryComboBox = Nothing
    Me.mo_quant24.NTSRepositoryItemCheck = Nothing
    Me.mo_quant24.NTSRepositoryItemMemo = Nothing
    Me.mo_quant24.NTSRepositoryItemText = Nothing
    Me.mo_quant24.OptionsColumn.AllowEdit = False
    Me.mo_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant24.OptionsColumn.ReadOnly = True
    Me.mo_quant24.OptionsFilter.AllowFilter = False
    Me.mo_quant24.Visible = True
    Me.mo_quant24.VisibleIndex = 69
    '
    'mo_quaeva01
    '
    Me.mo_quaeva01.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva01.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva01.Caption = "Q.E.01"
    Me.mo_quaeva01.Enabled = False
    Me.mo_quaeva01.FieldName = "mo_quaeva01"
    Me.mo_quaeva01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva01.Name = "mo_quaeva01"
    Me.mo_quaeva01.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva01.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva01.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva01.NTSRepositoryItemText = Nothing
    Me.mo_quaeva01.OptionsColumn.AllowEdit = False
    Me.mo_quaeva01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva01.OptionsColumn.ReadOnly = True
    Me.mo_quaeva01.OptionsFilter.AllowFilter = False
    Me.mo_quaeva01.Visible = True
    Me.mo_quaeva01.VisibleIndex = 70
    '
    'mo_quaeva02
    '
    Me.mo_quaeva02.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva02.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva02.Caption = "Q.E.02"
    Me.mo_quaeva02.Enabled = False
    Me.mo_quaeva02.FieldName = "mo_quaeva02"
    Me.mo_quaeva02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva02.Name = "mo_quaeva02"
    Me.mo_quaeva02.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva02.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva02.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva02.NTSRepositoryItemText = Nothing
    Me.mo_quaeva02.OptionsColumn.AllowEdit = False
    Me.mo_quaeva02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva02.OptionsColumn.ReadOnly = True
    Me.mo_quaeva02.OptionsFilter.AllowFilter = False
    Me.mo_quaeva02.Visible = True
    Me.mo_quaeva02.VisibleIndex = 71
    '
    'mo_quaeva03
    '
    Me.mo_quaeva03.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva03.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva03.Caption = "Q.E.03"
    Me.mo_quaeva03.Enabled = False
    Me.mo_quaeva03.FieldName = "mo_quaeva03"
    Me.mo_quaeva03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva03.Name = "mo_quaeva03"
    Me.mo_quaeva03.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva03.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva03.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva03.NTSRepositoryItemText = Nothing
    Me.mo_quaeva03.OptionsColumn.AllowEdit = False
    Me.mo_quaeva03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva03.OptionsColumn.ReadOnly = True
    Me.mo_quaeva03.OptionsFilter.AllowFilter = False
    Me.mo_quaeva03.Visible = True
    Me.mo_quaeva03.VisibleIndex = 72
    '
    'mo_quaeva04
    '
    Me.mo_quaeva04.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva04.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva04.Caption = "Q.E.04"
    Me.mo_quaeva04.Enabled = False
    Me.mo_quaeva04.FieldName = "mo_quaeva04"
    Me.mo_quaeva04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva04.Name = "mo_quaeva04"
    Me.mo_quaeva04.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva04.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva04.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva04.NTSRepositoryItemText = Nothing
    Me.mo_quaeva04.OptionsColumn.AllowEdit = False
    Me.mo_quaeva04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva04.OptionsColumn.ReadOnly = True
    Me.mo_quaeva04.OptionsFilter.AllowFilter = False
    Me.mo_quaeva04.Visible = True
    Me.mo_quaeva04.VisibleIndex = 73
    '
    'mo_quaeva05
    '
    Me.mo_quaeva05.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva05.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva05.Caption = "Q.E.05"
    Me.mo_quaeva05.Enabled = False
    Me.mo_quaeva05.FieldName = "mo_quaeva05"
    Me.mo_quaeva05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva05.Name = "mo_quaeva05"
    Me.mo_quaeva05.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva05.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva05.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva05.NTSRepositoryItemText = Nothing
    Me.mo_quaeva05.OptionsColumn.AllowEdit = False
    Me.mo_quaeva05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva05.OptionsColumn.ReadOnly = True
    Me.mo_quaeva05.OptionsFilter.AllowFilter = False
    Me.mo_quaeva05.Visible = True
    Me.mo_quaeva05.VisibleIndex = 74
    '
    'mo_quaeva06
    '
    Me.mo_quaeva06.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva06.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva06.Caption = "Q.E.06"
    Me.mo_quaeva06.Enabled = False
    Me.mo_quaeva06.FieldName = "mo_quaeva06"
    Me.mo_quaeva06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva06.Name = "mo_quaeva06"
    Me.mo_quaeva06.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva06.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva06.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva06.NTSRepositoryItemText = Nothing
    Me.mo_quaeva06.OptionsColumn.AllowEdit = False
    Me.mo_quaeva06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva06.OptionsColumn.ReadOnly = True
    Me.mo_quaeva06.OptionsFilter.AllowFilter = False
    Me.mo_quaeva06.Visible = True
    Me.mo_quaeva06.VisibleIndex = 75
    '
    'mo_quaeva07
    '
    Me.mo_quaeva07.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva07.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva07.Caption = "Q.E.07"
    Me.mo_quaeva07.Enabled = False
    Me.mo_quaeva07.FieldName = "mo_quaeva07"
    Me.mo_quaeva07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva07.Name = "mo_quaeva07"
    Me.mo_quaeva07.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva07.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva07.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva07.NTSRepositoryItemText = Nothing
    Me.mo_quaeva07.OptionsColumn.AllowEdit = False
    Me.mo_quaeva07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva07.OptionsColumn.ReadOnly = True
    Me.mo_quaeva07.OptionsFilter.AllowFilter = False
    Me.mo_quaeva07.Visible = True
    Me.mo_quaeva07.VisibleIndex = 76
    '
    'mo_quaeva08
    '
    Me.mo_quaeva08.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva08.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva08.Caption = "Q.E.08"
    Me.mo_quaeva08.Enabled = False
    Me.mo_quaeva08.FieldName = "mo_quaeva08"
    Me.mo_quaeva08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva08.Name = "mo_quaeva08"
    Me.mo_quaeva08.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva08.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva08.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva08.NTSRepositoryItemText = Nothing
    Me.mo_quaeva08.OptionsColumn.AllowEdit = False
    Me.mo_quaeva08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva08.OptionsColumn.ReadOnly = True
    Me.mo_quaeva08.OptionsFilter.AllowFilter = False
    Me.mo_quaeva08.Visible = True
    Me.mo_quaeva08.VisibleIndex = 77
    '
    'mo_quaeva09
    '
    Me.mo_quaeva09.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva09.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva09.Caption = "Q.E.09"
    Me.mo_quaeva09.Enabled = False
    Me.mo_quaeva09.FieldName = "mo_quaeva09"
    Me.mo_quaeva09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva09.Name = "mo_quaeva09"
    Me.mo_quaeva09.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva09.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva09.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva09.NTSRepositoryItemText = Nothing
    Me.mo_quaeva09.OptionsColumn.AllowEdit = False
    Me.mo_quaeva09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva09.OptionsColumn.ReadOnly = True
    Me.mo_quaeva09.OptionsFilter.AllowFilter = False
    Me.mo_quaeva09.Visible = True
    Me.mo_quaeva09.VisibleIndex = 78
    '
    'mo_quaeva10
    '
    Me.mo_quaeva10.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva10.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva10.Caption = "Q.E.10"
    Me.mo_quaeva10.Enabled = False
    Me.mo_quaeva10.FieldName = "mo_quaeva10"
    Me.mo_quaeva10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva10.Name = "mo_quaeva10"
    Me.mo_quaeva10.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva10.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva10.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva10.NTSRepositoryItemText = Nothing
    Me.mo_quaeva10.OptionsColumn.AllowEdit = False
    Me.mo_quaeva10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva10.OptionsColumn.ReadOnly = True
    Me.mo_quaeva10.OptionsFilter.AllowFilter = False
    Me.mo_quaeva10.Visible = True
    Me.mo_quaeva10.VisibleIndex = 79
    '
    'mo_quaeva11
    '
    Me.mo_quaeva11.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva11.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva11.Caption = "Q.E.11"
    Me.mo_quaeva11.Enabled = False
    Me.mo_quaeva11.FieldName = "mo_quaeva11"
    Me.mo_quaeva11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva11.Name = "mo_quaeva11"
    Me.mo_quaeva11.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva11.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva11.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva11.NTSRepositoryItemText = Nothing
    Me.mo_quaeva11.OptionsColumn.AllowEdit = False
    Me.mo_quaeva11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva11.OptionsColumn.ReadOnly = True
    Me.mo_quaeva11.OptionsFilter.AllowFilter = False
    Me.mo_quaeva11.Visible = True
    Me.mo_quaeva11.VisibleIndex = 80
    '
    'mo_quaeva12
    '
    Me.mo_quaeva12.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva12.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva12.Caption = "Q.E.12"
    Me.mo_quaeva12.Enabled = False
    Me.mo_quaeva12.FieldName = "mo_quaeva12"
    Me.mo_quaeva12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva12.Name = "mo_quaeva12"
    Me.mo_quaeva12.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva12.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva12.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva12.NTSRepositoryItemText = Nothing
    Me.mo_quaeva12.OptionsColumn.AllowEdit = False
    Me.mo_quaeva12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva12.OptionsColumn.ReadOnly = True
    Me.mo_quaeva12.OptionsFilter.AllowFilter = False
    Me.mo_quaeva12.Visible = True
    Me.mo_quaeva12.VisibleIndex = 81
    '
    'mo_quaeva13
    '
    Me.mo_quaeva13.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva13.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva13.Caption = "Q.E.13"
    Me.mo_quaeva13.Enabled = False
    Me.mo_quaeva13.FieldName = "mo_quaeva13"
    Me.mo_quaeva13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva13.Name = "mo_quaeva13"
    Me.mo_quaeva13.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva13.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva13.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva13.NTSRepositoryItemText = Nothing
    Me.mo_quaeva13.OptionsColumn.AllowEdit = False
    Me.mo_quaeva13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva13.OptionsColumn.ReadOnly = True
    Me.mo_quaeva13.OptionsFilter.AllowFilter = False
    Me.mo_quaeva13.Visible = True
    Me.mo_quaeva13.VisibleIndex = 82
    '
    'mo_quaeva14
    '
    Me.mo_quaeva14.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva14.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva14.Caption = "Q.E.14"
    Me.mo_quaeva14.Enabled = False
    Me.mo_quaeva14.FieldName = "mo_quaeva14"
    Me.mo_quaeva14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva14.Name = "mo_quaeva14"
    Me.mo_quaeva14.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva14.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva14.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva14.NTSRepositoryItemText = Nothing
    Me.mo_quaeva14.OptionsColumn.AllowEdit = False
    Me.mo_quaeva14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva14.OptionsColumn.ReadOnly = True
    Me.mo_quaeva14.OptionsFilter.AllowFilter = False
    Me.mo_quaeva14.Visible = True
    Me.mo_quaeva14.VisibleIndex = 83
    '
    'mo_quaeva15
    '
    Me.mo_quaeva15.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva15.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva15.Caption = "Q.E.15"
    Me.mo_quaeva15.Enabled = False
    Me.mo_quaeva15.FieldName = "mo_quaeva15"
    Me.mo_quaeva15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva15.Name = "mo_quaeva15"
    Me.mo_quaeva15.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva15.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva15.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva15.NTSRepositoryItemText = Nothing
    Me.mo_quaeva15.OptionsColumn.AllowEdit = False
    Me.mo_quaeva15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva15.OptionsColumn.ReadOnly = True
    Me.mo_quaeva15.OptionsFilter.AllowFilter = False
    Me.mo_quaeva15.Visible = True
    Me.mo_quaeva15.VisibleIndex = 84
    '
    'mo_quaeva16
    '
    Me.mo_quaeva16.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva16.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva16.Caption = "Q.E.16"
    Me.mo_quaeva16.Enabled = False
    Me.mo_quaeva16.FieldName = "mo_quaeva16"
    Me.mo_quaeva16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva16.Name = "mo_quaeva16"
    Me.mo_quaeva16.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva16.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva16.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva16.NTSRepositoryItemText = Nothing
    Me.mo_quaeva16.OptionsColumn.AllowEdit = False
    Me.mo_quaeva16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva16.OptionsColumn.ReadOnly = True
    Me.mo_quaeva16.OptionsFilter.AllowFilter = False
    Me.mo_quaeva16.Visible = True
    Me.mo_quaeva16.VisibleIndex = 85
    '
    'mo_quaeva17
    '
    Me.mo_quaeva17.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva17.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva17.Caption = "Q.E.17"
    Me.mo_quaeva17.Enabled = False
    Me.mo_quaeva17.FieldName = "mo_quaeva17"
    Me.mo_quaeva17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva17.Name = "mo_quaeva17"
    Me.mo_quaeva17.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva17.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva17.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva17.NTSRepositoryItemText = Nothing
    Me.mo_quaeva17.OptionsColumn.AllowEdit = False
    Me.mo_quaeva17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva17.OptionsColumn.ReadOnly = True
    Me.mo_quaeva17.OptionsFilter.AllowFilter = False
    Me.mo_quaeva17.Visible = True
    Me.mo_quaeva17.VisibleIndex = 86
    '
    'mo_quaeva18
    '
    Me.mo_quaeva18.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva18.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva18.Caption = "Q.E.18"
    Me.mo_quaeva18.Enabled = False
    Me.mo_quaeva18.FieldName = "mo_quaeva18"
    Me.mo_quaeva18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva18.Name = "mo_quaeva18"
    Me.mo_quaeva18.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva18.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva18.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva18.NTSRepositoryItemText = Nothing
    Me.mo_quaeva18.OptionsColumn.AllowEdit = False
    Me.mo_quaeva18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva18.OptionsColumn.ReadOnly = True
    Me.mo_quaeva18.OptionsFilter.AllowFilter = False
    Me.mo_quaeva18.Visible = True
    Me.mo_quaeva18.VisibleIndex = 87
    '
    'mo_quaeva19
    '
    Me.mo_quaeva19.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva19.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva19.Caption = "Q.E.19"
    Me.mo_quaeva19.Enabled = False
    Me.mo_quaeva19.FieldName = "mo_quaeva19"
    Me.mo_quaeva19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva19.Name = "mo_quaeva19"
    Me.mo_quaeva19.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva19.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva19.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva19.NTSRepositoryItemText = Nothing
    Me.mo_quaeva19.OptionsColumn.AllowEdit = False
    Me.mo_quaeva19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva19.OptionsColumn.ReadOnly = True
    Me.mo_quaeva19.OptionsFilter.AllowFilter = False
    Me.mo_quaeva19.Visible = True
    Me.mo_quaeva19.VisibleIndex = 88
    '
    'mo_quaeva20
    '
    Me.mo_quaeva20.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva20.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva20.Caption = "Q.E.20"
    Me.mo_quaeva20.Enabled = False
    Me.mo_quaeva20.FieldName = "mo_quaeva20"
    Me.mo_quaeva20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva20.Name = "mo_quaeva20"
    Me.mo_quaeva20.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva20.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva20.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva20.NTSRepositoryItemText = Nothing
    Me.mo_quaeva20.OptionsColumn.AllowEdit = False
    Me.mo_quaeva20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva20.OptionsColumn.ReadOnly = True
    Me.mo_quaeva20.OptionsFilter.AllowFilter = False
    Me.mo_quaeva20.Visible = True
    Me.mo_quaeva20.VisibleIndex = 89
    '
    'mo_quaeva21
    '
    Me.mo_quaeva21.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva21.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva21.Caption = "Q.E.21"
    Me.mo_quaeva21.Enabled = False
    Me.mo_quaeva21.FieldName = "mo_quaeva21"
    Me.mo_quaeva21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva21.Name = "mo_quaeva21"
    Me.mo_quaeva21.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva21.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva21.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva21.NTSRepositoryItemText = Nothing
    Me.mo_quaeva21.OptionsColumn.AllowEdit = False
    Me.mo_quaeva21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva21.OptionsColumn.ReadOnly = True
    Me.mo_quaeva21.OptionsFilter.AllowFilter = False
    Me.mo_quaeva21.Visible = True
    Me.mo_quaeva21.VisibleIndex = 90
    '
    'mo_quaeva22
    '
    Me.mo_quaeva22.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva22.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva22.Caption = "Q.E.22"
    Me.mo_quaeva22.Enabled = False
    Me.mo_quaeva22.FieldName = "mo_quaeva22"
    Me.mo_quaeva22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva22.Name = "mo_quaeva22"
    Me.mo_quaeva22.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva22.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva22.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva22.NTSRepositoryItemText = Nothing
    Me.mo_quaeva22.OptionsColumn.AllowEdit = False
    Me.mo_quaeva22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva22.OptionsColumn.ReadOnly = True
    Me.mo_quaeva22.OptionsFilter.AllowFilter = False
    Me.mo_quaeva22.Visible = True
    Me.mo_quaeva22.VisibleIndex = 91
    '
    'mo_quaeva23
    '
    Me.mo_quaeva23.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva23.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva23.Caption = "Q.E.23"
    Me.mo_quaeva23.Enabled = False
    Me.mo_quaeva23.FieldName = "mo_quaeva23"
    Me.mo_quaeva23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva23.Name = "mo_quaeva23"
    Me.mo_quaeva23.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva23.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva23.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva23.NTSRepositoryItemText = Nothing
    Me.mo_quaeva23.OptionsColumn.AllowEdit = False
    Me.mo_quaeva23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva23.OptionsColumn.ReadOnly = True
    Me.mo_quaeva23.OptionsFilter.AllowFilter = False
    Me.mo_quaeva23.Visible = True
    Me.mo_quaeva23.VisibleIndex = 92
    '
    'mo_quaeva24
    '
    Me.mo_quaeva24.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva24.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva24.Caption = "Q.E.24"
    Me.mo_quaeva24.Enabled = False
    Me.mo_quaeva24.FieldName = "mo_quaeva24"
    Me.mo_quaeva24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva24.Name = "mo_quaeva24"
    Me.mo_quaeva24.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva24.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva24.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva24.NTSRepositoryItemText = Nothing
    Me.mo_quaeva24.OptionsColumn.AllowEdit = False
    Me.mo_quaeva24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva24.OptionsColumn.ReadOnly = True
    Me.mo_quaeva24.OptionsFilter.AllowFilter = False
    Me.mo_quaeva24.Visible = True
    Me.mo_quaeva24.VisibleIndex = 93
    '
    'mo_quapre01
    '
    Me.mo_quapre01.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre01.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre01.Caption = "Q.P.01"
    Me.mo_quapre01.Enabled = False
    Me.mo_quapre01.FieldName = "mo_quapre01"
    Me.mo_quapre01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre01.Name = "mo_quapre01"
    Me.mo_quapre01.NTSRepositoryComboBox = Nothing
    Me.mo_quapre01.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre01.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre01.NTSRepositoryItemText = Nothing
    Me.mo_quapre01.OptionsColumn.AllowEdit = False
    Me.mo_quapre01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre01.OptionsColumn.ReadOnly = True
    Me.mo_quapre01.OptionsFilter.AllowFilter = False
    Me.mo_quapre01.Visible = True
    Me.mo_quapre01.VisibleIndex = 94
    '
    'mo_quapre02
    '
    Me.mo_quapre02.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre02.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre02.Caption = "Q.P.02"
    Me.mo_quapre02.Enabled = False
    Me.mo_quapre02.FieldName = "mo_quapre02"
    Me.mo_quapre02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre02.Name = "mo_quapre02"
    Me.mo_quapre02.NTSRepositoryComboBox = Nothing
    Me.mo_quapre02.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre02.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre02.NTSRepositoryItemText = Nothing
    Me.mo_quapre02.OptionsColumn.AllowEdit = False
    Me.mo_quapre02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre02.OptionsColumn.ReadOnly = True
    Me.mo_quapre02.OptionsFilter.AllowFilter = False
    Me.mo_quapre02.Visible = True
    Me.mo_quapre02.VisibleIndex = 95
    '
    'mo_quapre03
    '
    Me.mo_quapre03.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre03.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre03.Caption = "Q.P.03"
    Me.mo_quapre03.Enabled = False
    Me.mo_quapre03.FieldName = "mo_quapre03"
    Me.mo_quapre03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre03.Name = "mo_quapre03"
    Me.mo_quapre03.NTSRepositoryComboBox = Nothing
    Me.mo_quapre03.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre03.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre03.NTSRepositoryItemText = Nothing
    Me.mo_quapre03.OptionsColumn.AllowEdit = False
    Me.mo_quapre03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre03.OptionsColumn.ReadOnly = True
    Me.mo_quapre03.OptionsFilter.AllowFilter = False
    Me.mo_quapre03.Visible = True
    Me.mo_quapre03.VisibleIndex = 96
    '
    'mo_quapre04
    '
    Me.mo_quapre04.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre04.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre04.Caption = "Q.P.04"
    Me.mo_quapre04.Enabled = False
    Me.mo_quapre04.FieldName = "mo_quapre04"
    Me.mo_quapre04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre04.Name = "mo_quapre04"
    Me.mo_quapre04.NTSRepositoryComboBox = Nothing
    Me.mo_quapre04.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre04.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre04.NTSRepositoryItemText = Nothing
    Me.mo_quapre04.OptionsColumn.AllowEdit = False
    Me.mo_quapre04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre04.OptionsColumn.ReadOnly = True
    Me.mo_quapre04.OptionsFilter.AllowFilter = False
    Me.mo_quapre04.Visible = True
    Me.mo_quapre04.VisibleIndex = 97
    '
    'mo_quapre05
    '
    Me.mo_quapre05.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre05.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre05.Caption = "Q.P.05"
    Me.mo_quapre05.Enabled = False
    Me.mo_quapre05.FieldName = "mo_quapre05"
    Me.mo_quapre05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre05.Name = "mo_quapre05"
    Me.mo_quapre05.NTSRepositoryComboBox = Nothing
    Me.mo_quapre05.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre05.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre05.NTSRepositoryItemText = Nothing
    Me.mo_quapre05.OptionsColumn.AllowEdit = False
    Me.mo_quapre05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre05.OptionsColumn.ReadOnly = True
    Me.mo_quapre05.OptionsFilter.AllowFilter = False
    Me.mo_quapre05.Visible = True
    Me.mo_quapre05.VisibleIndex = 98
    '
    'mo_quapre06
    '
    Me.mo_quapre06.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre06.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre06.Caption = "Q.P.06"
    Me.mo_quapre06.Enabled = False
    Me.mo_quapre06.FieldName = "mo_quapre06"
    Me.mo_quapre06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre06.Name = "mo_quapre06"
    Me.mo_quapre06.NTSRepositoryComboBox = Nothing
    Me.mo_quapre06.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre06.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre06.NTSRepositoryItemText = Nothing
    Me.mo_quapre06.OptionsColumn.AllowEdit = False
    Me.mo_quapre06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre06.OptionsColumn.ReadOnly = True
    Me.mo_quapre06.OptionsFilter.AllowFilter = False
    Me.mo_quapre06.Visible = True
    Me.mo_quapre06.VisibleIndex = 99
    '
    'mo_quapre07
    '
    Me.mo_quapre07.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre07.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre07.Caption = "Q.P.07"
    Me.mo_quapre07.Enabled = False
    Me.mo_quapre07.FieldName = "mo_quapre07"
    Me.mo_quapre07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre07.Name = "mo_quapre07"
    Me.mo_quapre07.NTSRepositoryComboBox = Nothing
    Me.mo_quapre07.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre07.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre07.NTSRepositoryItemText = Nothing
    Me.mo_quapre07.OptionsColumn.AllowEdit = False
    Me.mo_quapre07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre07.OptionsColumn.ReadOnly = True
    Me.mo_quapre07.OptionsFilter.AllowFilter = False
    Me.mo_quapre07.Visible = True
    Me.mo_quapre07.VisibleIndex = 100
    '
    'mo_quapre08
    '
    Me.mo_quapre08.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre08.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre08.Caption = "Q.P.08"
    Me.mo_quapre08.Enabled = False
    Me.mo_quapre08.FieldName = "mo_quapre08"
    Me.mo_quapre08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre08.Name = "mo_quapre08"
    Me.mo_quapre08.NTSRepositoryComboBox = Nothing
    Me.mo_quapre08.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre08.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre08.NTSRepositoryItemText = Nothing
    Me.mo_quapre08.OptionsColumn.AllowEdit = False
    Me.mo_quapre08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre08.OptionsColumn.ReadOnly = True
    Me.mo_quapre08.OptionsFilter.AllowFilter = False
    Me.mo_quapre08.Visible = True
    Me.mo_quapre08.VisibleIndex = 101
    '
    'mo_quapre09
    '
    Me.mo_quapre09.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre09.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre09.Caption = "Q.P.09"
    Me.mo_quapre09.Enabled = False
    Me.mo_quapre09.FieldName = "mo_quapre09"
    Me.mo_quapre09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre09.Name = "mo_quapre09"
    Me.mo_quapre09.NTSRepositoryComboBox = Nothing
    Me.mo_quapre09.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre09.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre09.NTSRepositoryItemText = Nothing
    Me.mo_quapre09.OptionsColumn.AllowEdit = False
    Me.mo_quapre09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre09.OptionsColumn.ReadOnly = True
    Me.mo_quapre09.OptionsFilter.AllowFilter = False
    Me.mo_quapre09.Visible = True
    Me.mo_quapre09.VisibleIndex = 102
    '
    'mo_quapre10
    '
    Me.mo_quapre10.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre10.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre10.Caption = "Q.P.10"
    Me.mo_quapre10.Enabled = False
    Me.mo_quapre10.FieldName = "mo_quapre10"
    Me.mo_quapre10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre10.Name = "mo_quapre10"
    Me.mo_quapre10.NTSRepositoryComboBox = Nothing
    Me.mo_quapre10.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre10.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre10.NTSRepositoryItemText = Nothing
    Me.mo_quapre10.OptionsColumn.AllowEdit = False
    Me.mo_quapre10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre10.OptionsColumn.ReadOnly = True
    Me.mo_quapre10.OptionsFilter.AllowFilter = False
    Me.mo_quapre10.Visible = True
    Me.mo_quapre10.VisibleIndex = 103
    '
    'mo_quapre11
    '
    Me.mo_quapre11.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre11.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre11.Caption = "Q.P.11"
    Me.mo_quapre11.Enabled = False
    Me.mo_quapre11.FieldName = "mo_quapre11"
    Me.mo_quapre11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre11.Name = "mo_quapre11"
    Me.mo_quapre11.NTSRepositoryComboBox = Nothing
    Me.mo_quapre11.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre11.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre11.NTSRepositoryItemText = Nothing
    Me.mo_quapre11.OptionsColumn.AllowEdit = False
    Me.mo_quapre11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre11.OptionsColumn.ReadOnly = True
    Me.mo_quapre11.OptionsFilter.AllowFilter = False
    Me.mo_quapre11.Visible = True
    Me.mo_quapre11.VisibleIndex = 104
    '
    'mo_quapre12
    '
    Me.mo_quapre12.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre12.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre12.Caption = "Q.P.12"
    Me.mo_quapre12.Enabled = False
    Me.mo_quapre12.FieldName = "mo_quapre12"
    Me.mo_quapre12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre12.Name = "mo_quapre12"
    Me.mo_quapre12.NTSRepositoryComboBox = Nothing
    Me.mo_quapre12.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre12.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre12.NTSRepositoryItemText = Nothing
    Me.mo_quapre12.OptionsColumn.AllowEdit = False
    Me.mo_quapre12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre12.OptionsColumn.ReadOnly = True
    Me.mo_quapre12.OptionsFilter.AllowFilter = False
    Me.mo_quapre12.Visible = True
    Me.mo_quapre12.VisibleIndex = 105
    '
    'mo_quapre13
    '
    Me.mo_quapre13.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre13.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre13.Caption = "Q.P.13"
    Me.mo_quapre13.Enabled = False
    Me.mo_quapre13.FieldName = "mo_quapre13"
    Me.mo_quapre13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre13.Name = "mo_quapre13"
    Me.mo_quapre13.NTSRepositoryComboBox = Nothing
    Me.mo_quapre13.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre13.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre13.NTSRepositoryItemText = Nothing
    Me.mo_quapre13.OptionsColumn.AllowEdit = False
    Me.mo_quapre13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre13.OptionsColumn.ReadOnly = True
    Me.mo_quapre13.OptionsFilter.AllowFilter = False
    Me.mo_quapre13.Visible = True
    Me.mo_quapre13.VisibleIndex = 106
    '
    'mo_quapre14
    '
    Me.mo_quapre14.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre14.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre14.Caption = "Q.P.14"
    Me.mo_quapre14.Enabled = False
    Me.mo_quapre14.FieldName = "mo_quapre14"
    Me.mo_quapre14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre14.Name = "mo_quapre14"
    Me.mo_quapre14.NTSRepositoryComboBox = Nothing
    Me.mo_quapre14.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre14.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre14.NTSRepositoryItemText = Nothing
    Me.mo_quapre14.OptionsColumn.AllowEdit = False
    Me.mo_quapre14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre14.OptionsColumn.ReadOnly = True
    Me.mo_quapre14.OptionsFilter.AllowFilter = False
    Me.mo_quapre14.Visible = True
    Me.mo_quapre14.VisibleIndex = 107
    '
    'mo_quapre15
    '
    Me.mo_quapre15.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre15.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre15.Caption = "Q.P.15"
    Me.mo_quapre15.Enabled = False
    Me.mo_quapre15.FieldName = "mo_quapre15"
    Me.mo_quapre15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre15.Name = "mo_quapre15"
    Me.mo_quapre15.NTSRepositoryComboBox = Nothing
    Me.mo_quapre15.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre15.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre15.NTSRepositoryItemText = Nothing
    Me.mo_quapre15.OptionsColumn.AllowEdit = False
    Me.mo_quapre15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre15.OptionsColumn.ReadOnly = True
    Me.mo_quapre15.OptionsFilter.AllowFilter = False
    Me.mo_quapre15.Visible = True
    Me.mo_quapre15.VisibleIndex = 108
    '
    'mo_quapre16
    '
    Me.mo_quapre16.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre16.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre16.Caption = "Q.P.16"
    Me.mo_quapre16.Enabled = False
    Me.mo_quapre16.FieldName = "mo_quapre16"
    Me.mo_quapre16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre16.Name = "mo_quapre16"
    Me.mo_quapre16.NTSRepositoryComboBox = Nothing
    Me.mo_quapre16.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre16.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre16.NTSRepositoryItemText = Nothing
    Me.mo_quapre16.OptionsColumn.AllowEdit = False
    Me.mo_quapre16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre16.OptionsColumn.ReadOnly = True
    Me.mo_quapre16.OptionsFilter.AllowFilter = False
    Me.mo_quapre16.Visible = True
    Me.mo_quapre16.VisibleIndex = 109
    '
    'mo_quapre17
    '
    Me.mo_quapre17.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre17.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre17.Caption = "Q.P.17"
    Me.mo_quapre17.Enabled = False
    Me.mo_quapre17.FieldName = "mo_quapre17"
    Me.mo_quapre17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre17.Name = "mo_quapre17"
    Me.mo_quapre17.NTSRepositoryComboBox = Nothing
    Me.mo_quapre17.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre17.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre17.NTSRepositoryItemText = Nothing
    Me.mo_quapre17.OptionsColumn.AllowEdit = False
    Me.mo_quapre17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre17.OptionsColumn.ReadOnly = True
    Me.mo_quapre17.OptionsFilter.AllowFilter = False
    Me.mo_quapre17.Visible = True
    Me.mo_quapre17.VisibleIndex = 110
    '
    'mo_quapre18
    '
    Me.mo_quapre18.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre18.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre18.Caption = "Q.P.18"
    Me.mo_quapre18.Enabled = False
    Me.mo_quapre18.FieldName = "mo_quapre18"
    Me.mo_quapre18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre18.Name = "mo_quapre18"
    Me.mo_quapre18.NTSRepositoryComboBox = Nothing
    Me.mo_quapre18.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre18.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre18.NTSRepositoryItemText = Nothing
    Me.mo_quapre18.OptionsColumn.AllowEdit = False
    Me.mo_quapre18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre18.OptionsColumn.ReadOnly = True
    Me.mo_quapre18.OptionsFilter.AllowFilter = False
    Me.mo_quapre18.Visible = True
    Me.mo_quapre18.VisibleIndex = 111
    '
    'mo_quapre19
    '
    Me.mo_quapre19.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre19.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre19.Caption = "Q.P.19"
    Me.mo_quapre19.Enabled = False
    Me.mo_quapre19.FieldName = "mo_quapre19"
    Me.mo_quapre19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre19.Name = "mo_quapre19"
    Me.mo_quapre19.NTSRepositoryComboBox = Nothing
    Me.mo_quapre19.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre19.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre19.NTSRepositoryItemText = Nothing
    Me.mo_quapre19.OptionsColumn.AllowEdit = False
    Me.mo_quapre19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre19.OptionsColumn.ReadOnly = True
    Me.mo_quapre19.OptionsFilter.AllowFilter = False
    Me.mo_quapre19.Visible = True
    Me.mo_quapre19.VisibleIndex = 112
    '
    'mo_quapre20
    '
    Me.mo_quapre20.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre20.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre20.Caption = "Q.P.20"
    Me.mo_quapre20.Enabled = False
    Me.mo_quapre20.FieldName = "mo_quapre20"
    Me.mo_quapre20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre20.Name = "mo_quapre20"
    Me.mo_quapre20.NTSRepositoryComboBox = Nothing
    Me.mo_quapre20.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre20.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre20.NTSRepositoryItemText = Nothing
    Me.mo_quapre20.OptionsColumn.AllowEdit = False
    Me.mo_quapre20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre20.OptionsColumn.ReadOnly = True
    Me.mo_quapre20.OptionsFilter.AllowFilter = False
    Me.mo_quapre20.Visible = True
    Me.mo_quapre20.VisibleIndex = 113
    '
    'mo_quapre21
    '
    Me.mo_quapre21.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre21.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre21.Caption = "Q.P.21"
    Me.mo_quapre21.Enabled = False
    Me.mo_quapre21.FieldName = "mo_quapre21"
    Me.mo_quapre21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre21.Name = "mo_quapre21"
    Me.mo_quapre21.NTSRepositoryComboBox = Nothing
    Me.mo_quapre21.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre21.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre21.NTSRepositoryItemText = Nothing
    Me.mo_quapre21.OptionsColumn.AllowEdit = False
    Me.mo_quapre21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre21.OptionsColumn.ReadOnly = True
    Me.mo_quapre21.OptionsFilter.AllowFilter = False
    Me.mo_quapre21.Visible = True
    Me.mo_quapre21.VisibleIndex = 114
    '
    'mo_quapre22
    '
    Me.mo_quapre22.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre22.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre22.Caption = "Q.P.22"
    Me.mo_quapre22.Enabled = False
    Me.mo_quapre22.FieldName = "mo_quapre22"
    Me.mo_quapre22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre22.Name = "mo_quapre22"
    Me.mo_quapre22.NTSRepositoryComboBox = Nothing
    Me.mo_quapre22.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre22.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre22.NTSRepositoryItemText = Nothing
    Me.mo_quapre22.OptionsColumn.AllowEdit = False
    Me.mo_quapre22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre22.OptionsColumn.ReadOnly = True
    Me.mo_quapre22.OptionsFilter.AllowFilter = False
    Me.mo_quapre22.Visible = True
    Me.mo_quapre22.VisibleIndex = 115
    '
    'mo_quapre23
    '
    Me.mo_quapre23.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre23.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre23.Caption = "Q.P.23"
    Me.mo_quapre23.Enabled = False
    Me.mo_quapre23.FieldName = "mo_quapre23"
    Me.mo_quapre23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre23.Name = "mo_quapre23"
    Me.mo_quapre23.NTSRepositoryComboBox = Nothing
    Me.mo_quapre23.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre23.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre23.NTSRepositoryItemText = Nothing
    Me.mo_quapre23.OptionsColumn.AllowEdit = False
    Me.mo_quapre23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre23.OptionsColumn.ReadOnly = True
    Me.mo_quapre23.OptionsFilter.AllowFilter = False
    Me.mo_quapre23.Visible = True
    Me.mo_quapre23.VisibleIndex = 116
    '
    'mo_quapre24
    '
    Me.mo_quapre24.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre24.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre24.Caption = "Q.P.24"
    Me.mo_quapre24.Enabled = False
    Me.mo_quapre24.FieldName = "mo_quapre24"
    Me.mo_quapre24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre24.Name = "mo_quapre24"
    Me.mo_quapre24.NTSRepositoryComboBox = Nothing
    Me.mo_quapre24.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre24.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre24.NTSRepositoryItemText = Nothing
    Me.mo_quapre24.OptionsColumn.AllowEdit = False
    Me.mo_quapre24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre24.OptionsColumn.ReadOnly = True
    Me.mo_quapre24.OptionsFilter.AllowFilter = False
    Me.mo_quapre24.Visible = True
    Me.mo_quapre24.VisibleIndex = 119
    '
    'xx_quadaeva01
    '
    Me.xx_quadaeva01.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva01.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva01.Caption = "Q.D.01"
    Me.xx_quadaeva01.Enabled = False
    Me.xx_quadaeva01.FieldName = "xx_quadaeva01"
    Me.xx_quadaeva01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva01.Name = "xx_quadaeva01"
    Me.xx_quadaeva01.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva01.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva01.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva01.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva01.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva01.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva01.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva01.Visible = True
    Me.xx_quadaeva01.VisibleIndex = 117
    '
    'xx_quadaeva02
    '
    Me.xx_quadaeva02.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva02.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva02.Caption = "Q.D.02"
    Me.xx_quadaeva02.Enabled = False
    Me.xx_quadaeva02.FieldName = "xx_quadaeva02"
    Me.xx_quadaeva02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva02.Name = "xx_quadaeva02"
    Me.xx_quadaeva02.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva02.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva02.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva02.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva02.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva02.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva02.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva02.Visible = True
    Me.xx_quadaeva02.VisibleIndex = 118
    '
    'xx_quadaeva03
    '
    Me.xx_quadaeva03.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva03.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva03.Caption = "Q.D.03"
    Me.xx_quadaeva03.Enabled = False
    Me.xx_quadaeva03.FieldName = "xx_quadaeva03"
    Me.xx_quadaeva03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva03.Name = "xx_quadaeva03"
    Me.xx_quadaeva03.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva03.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva03.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva03.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva03.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva03.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva03.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva03.Visible = True
    Me.xx_quadaeva03.VisibleIndex = 120
    '
    'xx_quadaeva04
    '
    Me.xx_quadaeva04.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva04.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva04.Caption = "Q.D.04"
    Me.xx_quadaeva04.Enabled = False
    Me.xx_quadaeva04.FieldName = "xx_quadaeva04"
    Me.xx_quadaeva04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva04.Name = "xx_quadaeva04"
    Me.xx_quadaeva04.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva04.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva04.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva04.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva04.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva04.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva04.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva04.Visible = True
    Me.xx_quadaeva04.VisibleIndex = 121
    '
    'xx_quadaeva05
    '
    Me.xx_quadaeva05.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva05.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva05.Caption = "Q.D.05"
    Me.xx_quadaeva05.Enabled = False
    Me.xx_quadaeva05.FieldName = "xx_quadaeva05"
    Me.xx_quadaeva05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva05.Name = "xx_quadaeva05"
    Me.xx_quadaeva05.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva05.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva05.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva05.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva05.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva05.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva05.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva05.Visible = True
    Me.xx_quadaeva05.VisibleIndex = 122
    '
    'xx_quadaeva06
    '
    Me.xx_quadaeva06.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva06.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva06.Caption = "Q.D.06"
    Me.xx_quadaeva06.Enabled = False
    Me.xx_quadaeva06.FieldName = "xx_quadaeva06"
    Me.xx_quadaeva06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva06.Name = "xx_quadaeva06"
    Me.xx_quadaeva06.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva06.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva06.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva06.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva06.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva06.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva06.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva06.Visible = True
    Me.xx_quadaeva06.VisibleIndex = 123
    '
    'xx_quadaeva07
    '
    Me.xx_quadaeva07.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva07.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva07.Caption = "Q.D.07"
    Me.xx_quadaeva07.Enabled = False
    Me.xx_quadaeva07.FieldName = "xx_quadaeva07"
    Me.xx_quadaeva07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva07.Name = "xx_quadaeva07"
    Me.xx_quadaeva07.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva07.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva07.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva07.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva07.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva07.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva07.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva07.Visible = True
    Me.xx_quadaeva07.VisibleIndex = 124
    '
    'xx_quadaeva08
    '
    Me.xx_quadaeva08.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva08.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva08.Caption = "Q.D.08"
    Me.xx_quadaeva08.Enabled = False
    Me.xx_quadaeva08.FieldName = "xx_quadaeva08"
    Me.xx_quadaeva08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva08.Name = "xx_quadaeva08"
    Me.xx_quadaeva08.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva08.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva08.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva08.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva08.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva08.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva08.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva08.Visible = True
    Me.xx_quadaeva08.VisibleIndex = 125
    '
    'xx_quadaeva09
    '
    Me.xx_quadaeva09.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva09.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva09.Caption = "Q.D.09"
    Me.xx_quadaeva09.Enabled = False
    Me.xx_quadaeva09.FieldName = "xx_quadaeva09"
    Me.xx_quadaeva09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva09.Name = "xx_quadaeva09"
    Me.xx_quadaeva09.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva09.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva09.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva09.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva09.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva09.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva09.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva09.Visible = True
    Me.xx_quadaeva09.VisibleIndex = 126
    '
    'xx_quadaeva10
    '
    Me.xx_quadaeva10.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva10.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva10.Caption = "Q.D.10"
    Me.xx_quadaeva10.Enabled = False
    Me.xx_quadaeva10.FieldName = "xx_quadaeva10"
    Me.xx_quadaeva10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva10.Name = "xx_quadaeva10"
    Me.xx_quadaeva10.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva10.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva10.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva10.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva10.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva10.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva10.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva10.Visible = True
    Me.xx_quadaeva10.VisibleIndex = 127
    '
    'xx_quadaeva11
    '
    Me.xx_quadaeva11.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva11.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva11.Caption = "Q.D.11"
    Me.xx_quadaeva11.Enabled = False
    Me.xx_quadaeva11.FieldName = "xx_quadaeva11"
    Me.xx_quadaeva11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva11.Name = "xx_quadaeva11"
    Me.xx_quadaeva11.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva11.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva11.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva11.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva11.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva11.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva11.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva11.Visible = True
    Me.xx_quadaeva11.VisibleIndex = 128
    '
    'xx_quadaeva12
    '
    Me.xx_quadaeva12.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva12.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva12.Caption = "Q.D.12"
    Me.xx_quadaeva12.Enabled = False
    Me.xx_quadaeva12.FieldName = "xx_quadaeva12"
    Me.xx_quadaeva12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva12.Name = "xx_quadaeva12"
    Me.xx_quadaeva12.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva12.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva12.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva12.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva12.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva12.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva12.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva12.Visible = True
    Me.xx_quadaeva12.VisibleIndex = 129
    '
    'xx_quadaeva13
    '
    Me.xx_quadaeva13.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva13.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva13.Caption = "Q.D.13"
    Me.xx_quadaeva13.Enabled = False
    Me.xx_quadaeva13.FieldName = "xx_quadaeva13"
    Me.xx_quadaeva13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva13.Name = "xx_quadaeva13"
    Me.xx_quadaeva13.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva13.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva13.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva13.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva13.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva13.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva13.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva13.Visible = True
    Me.xx_quadaeva13.VisibleIndex = 130
    '
    'xx_quadaeva14
    '
    Me.xx_quadaeva14.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva14.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva14.Caption = "Q.D.14"
    Me.xx_quadaeva14.Enabled = False
    Me.xx_quadaeva14.FieldName = "xx_quadaeva14"
    Me.xx_quadaeva14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva14.Name = "xx_quadaeva14"
    Me.xx_quadaeva14.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva14.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva14.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva14.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva14.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva14.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva14.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva14.Visible = True
    Me.xx_quadaeva14.VisibleIndex = 131
    '
    'xx_quadaeva15
    '
    Me.xx_quadaeva15.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva15.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva15.Caption = "Q.D.15"
    Me.xx_quadaeva15.Enabled = False
    Me.xx_quadaeva15.FieldName = "xx_quadaeva15"
    Me.xx_quadaeva15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva15.Name = "xx_quadaeva15"
    Me.xx_quadaeva15.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva15.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva15.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva15.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva15.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva15.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva15.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva15.Visible = True
    Me.xx_quadaeva15.VisibleIndex = 132
    '
    'xx_quadaeva16
    '
    Me.xx_quadaeva16.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva16.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva16.Caption = "Q.D.16"
    Me.xx_quadaeva16.Enabled = False
    Me.xx_quadaeva16.FieldName = "xx_quadaeva16"
    Me.xx_quadaeva16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva16.Name = "xx_quadaeva16"
    Me.xx_quadaeva16.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva16.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva16.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva16.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva16.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva16.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva16.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva16.Visible = True
    Me.xx_quadaeva16.VisibleIndex = 133
    '
    'xx_quadaeva17
    '
    Me.xx_quadaeva17.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva17.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva17.Caption = "Q.D.17"
    Me.xx_quadaeva17.Enabled = False
    Me.xx_quadaeva17.FieldName = "xx_quadaeva17"
    Me.xx_quadaeva17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva17.Name = "xx_quadaeva17"
    Me.xx_quadaeva17.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva17.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva17.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva17.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva17.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva17.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva17.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva17.Visible = True
    Me.xx_quadaeva17.VisibleIndex = 134
    '
    'xx_quadaeva18
    '
    Me.xx_quadaeva18.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva18.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva18.Caption = "Q.D.18"
    Me.xx_quadaeva18.Enabled = False
    Me.xx_quadaeva18.FieldName = "xx_quadaeva18"
    Me.xx_quadaeva18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva18.Name = "xx_quadaeva18"
    Me.xx_quadaeva18.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva18.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva18.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva18.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva18.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva18.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva18.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva18.Visible = True
    Me.xx_quadaeva18.VisibleIndex = 135
    '
    'xx_quadaeva19
    '
    Me.xx_quadaeva19.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva19.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva19.Caption = "Q.D.19"
    Me.xx_quadaeva19.Enabled = False
    Me.xx_quadaeva19.FieldName = "xx_quadaeva19"
    Me.xx_quadaeva19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva19.Name = "xx_quadaeva19"
    Me.xx_quadaeva19.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva19.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva19.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva19.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva19.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva19.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva19.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva19.Visible = True
    Me.xx_quadaeva19.VisibleIndex = 136
    '
    'xx_quadaeva20
    '
    Me.xx_quadaeva20.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva20.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva20.Caption = "Q.D.20"
    Me.xx_quadaeva20.Enabled = False
    Me.xx_quadaeva20.FieldName = "xx_quadaeva20"
    Me.xx_quadaeva20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva20.Name = "xx_quadaeva20"
    Me.xx_quadaeva20.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva20.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva20.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva20.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva20.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva20.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva20.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva20.Visible = True
    Me.xx_quadaeva20.VisibleIndex = 137
    '
    'xx_quadaeva21
    '
    Me.xx_quadaeva21.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva21.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva21.Caption = "Q.D.21"
    Me.xx_quadaeva21.Enabled = False
    Me.xx_quadaeva21.FieldName = "xx_quadaeva21"
    Me.xx_quadaeva21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva21.Name = "xx_quadaeva21"
    Me.xx_quadaeva21.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva21.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva21.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva21.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva21.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva21.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva21.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva21.Visible = True
    Me.xx_quadaeva21.VisibleIndex = 138
    '
    'xx_quadaeva22
    '
    Me.xx_quadaeva22.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva22.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva22.Caption = "Q.D.22"
    Me.xx_quadaeva22.Enabled = False
    Me.xx_quadaeva22.FieldName = "xx_quadaeva22"
    Me.xx_quadaeva22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva22.Name = "xx_quadaeva22"
    Me.xx_quadaeva22.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva22.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva22.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva22.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva22.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva22.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva22.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva22.Visible = True
    Me.xx_quadaeva22.VisibleIndex = 139
    '
    'xx_quadaeva23
    '
    Me.xx_quadaeva23.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva23.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva23.Caption = "Q.D.23"
    Me.xx_quadaeva23.Enabled = False
    Me.xx_quadaeva23.FieldName = "xx_quadaeva23"
    Me.xx_quadaeva23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva23.Name = "xx_quadaeva23"
    Me.xx_quadaeva23.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva23.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva23.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva23.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva23.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva23.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva23.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva23.Visible = True
    Me.xx_quadaeva23.VisibleIndex = 140
    '
    'xx_quadaeva24
    '
    Me.xx_quadaeva24.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva24.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva24.Caption = "Q.D.24"
    Me.xx_quadaeva24.Enabled = False
    Me.xx_quadaeva24.FieldName = "xx_quadaeva24"
    Me.xx_quadaeva24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva24.Name = "xx_quadaeva24"
    Me.xx_quadaeva24.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva24.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva24.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva24.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva24.OptionsColumn.AllowEdit = False
    Me.xx_quadaeva24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva24.OptionsColumn.ReadOnly = True
    Me.xx_quadaeva24.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva24.Visible = True
    Me.xx_quadaeva24.VisibleIndex = 141
    '
    'NtsGridView1
    '
    Me.NtsGridView1.ActiveFilterEnabled = False
    Me.NtsGridView1.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.NtsGridView1.Enabled = True
    Me.NtsGridView1.GridControl = Me.grHlmo
    Me.NtsGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.NtsGridView1.MinRowHeight = 14
    Me.NtsGridView1.Name = "NtsGridView1"
    Me.NtsGridView1.NTSAllowDelete = True
    Me.NtsGridView1.NTSAllowInsert = True
    Me.NtsGridView1.NTSAllowUpdate = True
    Me.NtsGridView1.NTSMenuContext = Nothing
    Me.NtsGridView1.OptionsCustomization.AllowRowSizing = True
    Me.NtsGridView1.OptionsFilter.AllowFilterEditor = False
    Me.NtsGridView1.OptionsNavigation.EnterMoveNextColumn = True
    Me.NtsGridView1.OptionsNavigation.UseTabKey = False
    Me.NtsGridView1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.NtsGridView1.OptionsView.ColumnAutoWidth = False
    Me.NtsGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.NtsGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.NtsGridView1.OptionsView.ShowGroupPanel = False
    Me.NtsGridView1.RowHeight = 14
    '
    'frmEstremiDoc
    '
    Me.frmEstremiDoc.AllowDrop = True
    Me.frmEstremiDoc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmEstremiDoc.Appearance.Options.UseBackColor = True
    Me.frmEstremiDoc.Controls.Add(Me.edSerie)
    Me.frmEstremiDoc.Controls.Add(Me.edAnno)
    Me.frmEstremiDoc.Controls.Add(Me.cbTipoRk)
    Me.frmEstremiDoc.Controls.Add(Me.lbSerie)
    Me.frmEstremiDoc.Controls.Add(Me.lbAnno)
    Me.frmEstremiDoc.Controls.Add(Me.lbTipoRk)
    Me.frmEstremiDoc.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.frmEstremiDoc.Location = New System.Drawing.Point(0, 209)
    Me.frmEstremiDoc.Name = "frmEstremiDoc"
    Me.frmEstremiDoc.Size = New System.Drawing.Size(598, 44)
    Me.frmEstremiDoc.TabIndex = 1
    Me.frmEstremiDoc.Text = "Estremi nuovo documento"
    Me.frmEstremiDoc.Visible = False
    '
    'edSerie
    '
    Me.edSerie.EditValue = ""
    Me.edSerie.Location = New System.Drawing.Point(398, 21)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(48, 20)
    Me.edSerie.TabIndex = 3
    '
    'edAnno
    '
    Me.edAnno.EditValue = "2014"
    Me.edAnno.Location = New System.Drawing.Point(307, 21)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "2014"
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(48, 20)
    Me.edAnno.TabIndex = 2
    '
    'cbTipoRk
    '
    Me.cbTipoRk.DataSource = Nothing
    Me.cbTipoRk.DisplayMember = ""
    Me.cbTipoRk.Location = New System.Drawing.Point(39, 21)
    Me.cbTipoRk.Name = "cbTipoRk"
    Me.cbTipoRk.NTSDbField = ""
    Me.cbTipoRk.Properties.AutoHeight = False
    Me.cbTipoRk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoRk.Properties.DropDownRows = 30
    Me.cbTipoRk.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoRk.SelectedValue = ""
    Me.cbTipoRk.Size = New System.Drawing.Size(223, 20)
    Me.cbTipoRk.TabIndex = 1
    Me.cbTipoRk.ValueMember = ""
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(361, 24)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 0
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(269, 24)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 0
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbTipoRk
    '
    Me.lbTipoRk.AutoSize = True
    Me.lbTipoRk.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoRk.Location = New System.Drawing.Point(6, 24)
    Me.lbTipoRk.Name = "lbTipoRk"
    Me.lbTipoRk.NTSDbField = ""
    Me.lbTipoRk.Size = New System.Drawing.Size(27, 13)
    Me.lbTipoRk.TabIndex = 0
    Me.lbTipoRk.Text = "Tipo"
    Me.lbTipoRk.Tooltip = ""
    Me.lbTipoRk.UseMnemonic = False
    '
    'FRMORHLMO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(711, 316)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnBottom)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMORHLMO"
    Me.Text = "SELEZIONE RIGHE D'ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.ckNoFigliKit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAppro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConfermato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAppro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grHlmo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlmo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmEstremiDoc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmEstremiDoc.ResumeLayout(False)
    Me.frmEstremiDoc.PerformLayout()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoRk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMORHLMO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      bOnLoading = True
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      'per default non sono visibili, altrimenti la query di selezione è più lenta ....
      xx_articopadre.Visible = False
      xx_desarticopadre.Visible = False

      nTipoZoom = oParam.nTipologia

      oCleHlmo.strDittaCorrente = DittaCorrente

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      GctlTipoDoc = oParam.nMastro.ToString
      If GctlTipoDoc = " " Then GctlTipoDoc = "999"
      GctlSetRoules()

      ckConfermato.Checked = (oMenu.GetSettingBus("BSORHLMO", "RECENT", ".", "SoloRigheConfermate", "N", " ", "N") = "S")
      ckNoFigliKit.Checked = (oMenu.GetSettingBus("BSORHLMO", "RECENT", ".", "ckNoFigliKit", "0", " ", "0") = "-1")



      '--------------------------
      'se non è stato passato il cod articolo blocco
      If oParam.strCodart <> "" Then
        ckArticolo.Checked = True
      Else
        ckArticolo.Checked = False
        ckArticolo.Enabled = False
      End If

      bAbilitaStart = True
      If oParam.lCommessa <> 0 Then
        edCommeca.Text = oParam.lCommessa.ToString
        ckCommeca.Checked = True
      End If

      cmdRicerca_Click(Me, Nothing)

      ValutaTipoDocumento()
      '--------------------------
      'se posso preseleziono le quantità
      If nTipoZoom <> 0 And oParam.strCodart <> "" Then
        If oParam.dImporto <> 0 Then PreSelQuantita(oParam.dImporto)
      End If


      GctlApplicaDefaultValue()

      '-------------------------------------------------
      bOnLoading = False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        cmdNavigazione.Visible = False
        ckCommeca.Visible = False
        edCommeca.Visible = False
        lbCommeca.Visible = False
        ckAppro.Visible = False
        edAppro.Visible = False
        lbAppro.Visible = False
        mo_fase.Visible = False
        mo_quapre.Visible = False
        mo_scont3.Visible = False
        mo_scont4.Visible = False
        mo_scont5.Visible = False
        mo_scont6.Visible = False
        mo_commeca.Visible = False
        xx_commeca.Visible = False
        mo_subcommeca.Visible = False
        xx_articopadre.Visible = False
        xx_desarticopadre.Visible = False
        xx_codappr.Visible = False
        xx_desappr.Visible = False
        mo_quant01.Visible = False : mo_quant02.Visible = False : mo_quant03.Visible = False : mo_quant04.Visible = False
        mo_quant05.Visible = False : mo_quant06.Visible = False : mo_quant07.Visible = False : mo_quant08.Visible = False
        mo_quant09.Visible = False : mo_quant10.Visible = False : mo_quant11.Visible = False : mo_quant12.Visible = False
        mo_quant13.Visible = False : mo_quant14.Visible = False : mo_quant15.Visible = False : mo_quant16.Visible = False
        mo_quant17.Visible = False : mo_quant18.Visible = False : mo_quant19.Visible = False : mo_quant20.Visible = False
        mo_quant21.Visible = False : mo_quant22.Visible = False : mo_quant23.Visible = False : mo_quant24.Visible = False
        mo_quaeva01.Visible = False : mo_quaeva02.Visible = False : mo_quaeva03.Visible = False : mo_quaeva04.Visible = False
        mo_quaeva05.Visible = False : mo_quaeva06.Visible = False : mo_quaeva07.Visible = False : mo_quaeva08.Visible = False
        mo_quaeva09.Visible = False : mo_quaeva10.Visible = False : mo_quaeva11.Visible = False : mo_quaeva12.Visible = False
        mo_quaeva13.Visible = False : mo_quaeva14.Visible = False : mo_quaeva15.Visible = False : mo_quaeva16.Visible = False
        mo_quaeva17.Visible = False : mo_quaeva18.Visible = False : mo_quaeva19.Visible = False : mo_quaeva20.Visible = False
        mo_quaeva21.Visible = False : mo_quaeva22.Visible = False : mo_quaeva23.Visible = False : mo_quaeva24.Visible = False
        mo_quapre01.Visible = False : mo_quapre02.Visible = False : mo_quapre03.Visible = False : mo_quapre04.Visible = False
        mo_quapre05.Visible = False : mo_quapre06.Visible = False : mo_quapre07.Visible = False : mo_quapre08.Visible = False
        mo_quapre09.Visible = False : mo_quapre10.Visible = False : mo_quapre11.Visible = False : mo_quapre12.Visible = False
        mo_quapre13.Visible = False : mo_quapre14.Visible = False : mo_quapre15.Visible = False : mo_quapre16.Visible = False
        mo_quapre17.Visible = False : mo_quapre18.Visible = False : mo_quapre19.Visible = False : mo_quapre20.Visible = False
        mo_quapre21.Visible = False : mo_quapre22.Visible = False : mo_quapre23.Visible = False : mo_quapre24.Visible = False
        xx_quadaeva01.Visible = False : xx_quadaeva02.Visible = False : xx_quadaeva03.Visible = False
        xx_quadaeva04.Visible = False : xx_quadaeva05.Visible = False : xx_quadaeva06.Visible = False
        xx_quadaeva07.Visible = False : xx_quadaeva08.Visible = False : xx_quadaeva09.Visible = False
        xx_quadaeva10.Visible = False : xx_quadaeva11.Visible = False : xx_quadaeva12.Visible = False
        xx_quadaeva13.Visible = False : xx_quadaeva14.Visible = False : xx_quadaeva15.Visible = False
        xx_quadaeva16.Visible = False : xx_quadaeva17.Visible = False : xx_quadaeva18.Visible = False
        xx_quadaeva19.Visible = False : xx_quadaeva20.Visible = False : xx_quadaeva21.Visible = False
        xx_quadaeva22.Visible = False : xx_quadaeva23.Visible = False : xx_quadaeva24.Visible = False
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
  Public Overridable Sub FRMORHLMO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try

      If e.KeyCode = Keys.F9 And e.Control = False And e.Alt = False And e.Shift = False Then
        cmdConferma_Click(cmdConferma, Nothing)
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMORHLMO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      If bCancel Then oParam.oParam = Nothing
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      If bAbilitaStart = False Then Return

      dsHlmo.Clear()

      'parametri passati dal chiamante
      'oPar.strDescr = oCleGsor.GetWhereHlmo()
      'oPar.strTipo = oCleGsor.dttET.Rows(0)!et_tipork.ToString
      'oPar.lContoCF = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto)
      'oPar.strCodart = strCodart
      'oPar.nFase = nFase
      'oPar.dImporto = dQuant
      'oPar.lCommessa = NTSCInt(IIf(oCleBoll.bPassaCommessaTestataHLMO, edEt_commeca.Text, 0))
      'oPar.nTipologia = 2                     '0 solo visualizzaz, 2 = possibilità di selez le righe
      'oPar.oParam = Nothing                   'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      'oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsorhlmo da visualizzare (in vb6 oPar.nMastro)
      'oPar.b
      'oPar.bFlag1 = ocleBoll.bNew
      'oPar.nAnno = NTSCInt(edAnnoDoc.Text)
      'oPar.strAlfpar = edSerieDoc.Text
      'oPar.lNumreg = NTSCInt(edNumDoc.Text)
      Me.Cursor = Cursors.WaitCursor
      If Not oCleHlmo.GetData(CType(oParam.oParam, DataTable), oParam.strTipo, oParam.nAnno, oParam.strAlfpar, _
                              oParam.lNumreg, bModTco, NTSCInt(IIf(ckCommeca.Checked, edCommeca.Text, 0)), _
                              NTSCInt(IIf(ckAppro.Checked, edAppro.Text, 0)), ckConfermato.Checked, _
                              oParam.strDescr.Replace("?", "_").Replace("*", "%"), _
                              IIf(ckArticolo.Checked, oParam.strCodart, "").ToString, oParam.nFase, _
                              xx_articopadre.Visible, oParam.bFlag1, dsHlmo, ckNoFigliKit.Checked) Then
        Me.Close()
        Return
      End If
      grHlmo.DataSource = Nothing
      dcHlmo.DataSource = dsHlmo.Tables("MOVORD")
      dsHlmo.AcceptChanges()
      grHlmo.DataSource = dcHlmo

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvHlmo.RowCount > 0 Then grHlmo.Focus()

      'per bloccare eventualmente la colonna qtadaeva se tco
      grvHlmo_NTSFocusedRowChanged(grvHlmo, Nothing)

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
    Dim dttTmp As New DataTable
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not Salva() Then Return

      dtrT = dsHlmo.Tables("MOVORD").Select("xx_seleziona = 'S'", "mo_anno, mo_serie, mo_numord, mo_riga")

      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129137215554062684, "Selezionare almeno una riga prima di confermare."))
        Return
      End If

      dttTmp = dsHlmo.Tables("MOVORD").Clone
      For Each dtrRiga As DataRow In dtrT
        dttTmp.ImportRow(dtrRiga)

        'Per ogni riga da importare, se è presente la spunta di non mostrare le righe dei figli di articoli kit,
        '    devo importare le righe dei figli in maniera proporzionalizzata.
        If ckNoFigliKit.Checked AndAlso NTSCStr(dtrRiga!mo_flkit).Trim <> "" Then
          'è una riga kit, devo trovare i figli e proporzionalizzare le quantità da evadere.
          For Each dtrRow As DataRow In oCleHlmo.TrovaFigliKit(dtrRiga).Rows
            dttTmp.ImportRow(dtrRow)
          Next
        End If
      Next
      dsHlmo.Tables("MOVORD").Clear()
      dsHlmo.Tables("MOVORD").AcceptChanges()
      dttTmp.AcceptChanges()
      oParam.oParam = dttTmp      'ritorno le righe selezionate

      If frmEstremiDoc.Visible Then
        'solo per bndkkons->evadi ordine: fa impostare il tipo documento
        oParam.strTipo = cbTipoRk.SelectedValue
        oParam.nAnno = NTSCInt(edAnno.Text)
        oParam.strAlfpar = edSerie.Text
      End If

      bCancel = False

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.oParam = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      For i As Integer = 0 To grvHlmo.RowCount - 1
        grvHlmo.GetDataRow(i)!xx_seleziona = "S"
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdDeseleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeseleziona.Click
    Try
      For Each dtrRow As DataRow In dsHlmo.Tables("MOVORD").Rows
        dtrRow!xx_seleziona = "N"
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdRipristina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRipristina.Click
    Try
      If Not grvHlmo.NTSRipristinaRigaCorrenteBefore(dcHlmo, True) Then Return
      oCleHlmo.Ripristina(dcHlmo.Position, dcHlmo.Filter)
      grvHlmo.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdNavigazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNavigazione.Click
    Dim strParam As String = ""
    Try
      If grvHlmo.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      With grvHlmo.NTSGetCurrentDataRow
        If !mo_tipork.ToString <> "$" And _
          !mo_tipork.ToString <> "O" And _
          !mo_tipork.ToString <> "H" And _
          !mo_tipork.ToString <> "X" And _
          !mo_tipork.ToString <> "R" And _
          !mo_tipork.ToString <> "V" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128583594683593750, "Funzione non disponibile per questo tipo di ordine/impegno."))
          Return
        End If

        strParam = "O;" & !mo_tipork.ToString & ";" & _
                   NTSCInt(!mo_anno).ToString & ";" & _
                   !mo_tipork.ToString & ";" & _
                   NTSCInt(!mo_numord).ToString.PadLeft(9, "0"c) & ";" & _
                   NTSCInt(!mo_riga).ToString.PadLeft(9, "0"c) & ";" & _
                   !mo_codart.ToString.PadRight(CLN__STD.CodartMaxLen) & ";" & _
                   NTSCInt(!mo_fase).ToString.PadLeft(4, "0"c)
        oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", oApp.Tr(Me, 128583595124687500, "Navigazione MRP"), DittaCorrente, "", "", Nothing, strParam, True, True)

      End With

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdApriOrdine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriOrdine.Click
    Dim strTipork As String = ""
    Dim nAnno As Integer = 0
    Dim strSerie As String = ""
    Dim lNumord As Integer = 0
    Dim lRiga As Integer = 0
    Dim lRigaOut As Integer = 0
    Dim strParam As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono righe, esce
      '--------------------------------------------------------------------------------------------------------------
      If grvHlmo.NTSGetCurrentDataRow() Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      strTipork = NTSCStr(grvHlmo.NTSGetCurrentDataRow()!mo_tipork)
      If strTipork = "Y" Then strTipork = "H"
      nAnno = NTSCInt(grvHlmo.NTSGetCurrentDataRow()!mo_anno)
      strSerie = NTSCStr(grvHlmo.NTSGetCurrentDataRow()!mo_serie)
      lNumord = NTSCInt(grvHlmo.NTSGetCurrentDataRow()!mo_numord)
      lRiga = NTSCInt(grvHlmo.NTSGetCurrentDataRow()!mo_riga)
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiama 'Gesitone Ordini/Impegni' in modale
      '--------------------------------------------------------------------------------------------------------------
      If oCleHlmo.IsDocRetail(DittaCorrente, strTipork, nAnno, strSerie, lNumord) = True Then
        strParam = "APRI;" & strTipork & ";" & _
                   nAnno.ToString.PadLeft(4, "0"c) & ";" & _
                   strSerie & ";" & _
                   lNumord.ToString.PadLeft(9, "0"c) & ";"
        oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 129665133528349610, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, True, True)
      ElseIf oCleHlmo.IsDocRetailNew(DittaCorrente, strTipork, nAnno, strSerie, lNumord) = True Then
        strParam = "APRI;" & strTipork & ";" & _
                   nAnno.ToString.PadLeft(4, "0"c) & ";" & _
                   strSerie & ";" & _
                   lNumord.ToString.PadLeft(9, "0"c) & ";"
        oMenu.RunChild("NTSInformatica", "FRMREGSRE", "Gestione retail", DittaCorrente, "", "BNREGSRE", Nothing, strParam, True, True)
      Else
        strParam = "APRI;" & strTipork & ";" & _
                   nAnno.ToString.PadLeft(4, "0"c) & ";" & _
                   strSerie & ";" & _
                   lNumord.ToString.PadLeft(9, "0"c) & ";"
        oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 130574249678170428, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If      '--------------------------------------------------------------------------------------------------------------
      '--- Al ritorno di 'Gestione Ordini/Impegni', fa il refresh dei dati visualizzati in griglia
      '--------------------------------------------------------------------------------------------------------------
      cmdRicerca_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono righe, esce
      '--------------------------------------------------------------------------------------------------------------
      If dcHlmo.List.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se, dopo il refresh di griglia, la riga esiste ancora, ci si posiziona
      '--- se è stata eliminata ma esiste ancora l'Ordine/Impegno, si posizioa sul primo che trova,
      '--- altrimenti si posiziona sulla prima riga
      '--------------------------------------------------------------------------------------------------------------
      If oCleHlmo.EsisteOrdineImpegno(strTipork, nAnno, strSerie, lNumord, lRiga, lRigaOut) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If lRigaOut <> 0 Then
        For i As Integer = 0 To (dcHlmo.List.Count - 1)
          If (CType(dcHlmo.Item(i), DataRowView)!mo_tipork.ToString = strTipork) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_anno.ToString = nAnno.ToString) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_serie.ToString = strSerie) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_numord.ToString = lNumord.ToString) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_riga.ToString = lRiga.ToString) Then
            dcHlmo.Position = i
            Exit For
          End If
        Next
      Else
        For i As Integer = 0 To (dcHlmo.List.Count - 1)
          If (CType(dcHlmo.Item(i), DataRowView)!mo_tipork.ToString = strTipork) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_anno.ToString = nAnno.ToString) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_serie.ToString = strSerie) And _
             (CType(dcHlmo.Item(i), DataRowView)!mo_numord.ToString = lNumord.ToString) Then
            dcHlmo.Position = i
            Exit For
          End If
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi di griglia"
  Public Overridable Sub grvHlmo_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHlmo.NTSBeforeRowUpdate
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

  Public Overridable Sub grvHlmo_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHlmo.NTSFocusedRowChanged
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      If grvHlmo.NTSGetCurrentDataRow Is Nothing Then Return

      '-------------------------
      'se l'articolo è gestito a TCO non posso cambiare la quantità da evadere, ma quelle specifiche per taglia
      If bModTco Then
        If NTSCInt(grvHlmo.NTSGetCurrentDataRow!xx_codtagl) <> 0 Then
          xx_quadaeva.Enabled = False

          '-------------------------
          'compilo l'intestaz. delle colonne come da TCO
          If bModTco Then
            oMenu.ValCodiceDb(NTSCInt(grvHlmo.NTSGetCurrentDataRow!xx_codtagl).ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
            For i = 1 To 24
              If NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).Trim = "" Then
                CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Enabled = False

                CType(grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
                CType(grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
                CType(grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
                CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
              Else
                GctlSetVisEnab(CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn), False)

                If nTipoZoom <> 0 Then
                  'visualizzo la colonna solo se posso ...
                  CType(grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = True
                  CType(grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = True
                  CType(grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = True
                  CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = True
                End If

                grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)).Caption = oApp.Tr(Me, 129548495409541016, "Q.O. ") & dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c)).ToString
                grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)).Caption = oApp.Tr(Me, 129548495428251954, "Q.E. ") & dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c)).ToString
                grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)).Caption = oApp.Tr(Me, 129548495441289063, "Q.P. ") & dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c)).ToString
                grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)).Caption = oApp.Tr(Me, 129548495464814454, "Q.D. ") & dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c)).ToString
              End If
            Next
            dttTmp.Clear()
          End If

        Else
          GctlSetVisEnab(xx_quadaeva, False)
          For i = 1 To 24
            CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Enabled = False
            grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)).Visible = False
            grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)).Visible = False
            grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)).Visible = False
            grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)).Visible = False
          Next
        End If    'If NTSCInt(grvHlmo.NTSGetCurrentDataRow!xx_codtagl) <> 0 Then
      End If    'If bModTco Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overrides Sub GctlSetConfigGrid()
    Dim i As Integer = 0
    Try
      '--------------------------
      'dev oereditare perchè dopo aver impostato la griglia come più mi piace, 
      'devo impostare le limitazioni da programma
      MyBase.GctlSetConfigGrid()

      '--------------------------
      'se posso solo visualizzare blocco quello che non serve
      If nTipoZoom = 0 Then
        grvHlmo.Enabled = False
        xx_seleziona.Visible = False
        xx_quadaeva.Visible = False
        xx_flevasdaeva.Visible = False
        cmdConferma.Visible = False
        cmdSeleziona.Visible = False
        cmdDeseleziona.Visible = False
        For i = 1 To 24
          CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
        Next
      Else
        GctlSetVisEnab(xx_seleziona, True)
        GctlSetVisEnab(xx_quadaeva, True)
        GctlSetVisEnab(xx_flevasdaeva, True)
        For i = 1 To 24
          GctlSetVisEnab(CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn), True)
        Next
      End If

      '-------------------------
      'in base al tipo nasconde le colonne che non servono
      If CBool(oParam.nMastro And 2) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Al solo caricamento della form, applica la configurazione delle colonne Articolo/Descrizione articolo.
        '--- Mentre, in uscita dal Ctrl + Click, deve mantenere ciò che è stato impostato
        '------------------------------------------------------------------------------------------------------------
        If bOnLoading = True Then
          mo_codart.Visible = False
          mo_descr.Visible = False
        End If
        '------------------------------------------------------------------------------------------------------------
      End If

      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtTCO)) Then bModTco = True
      'Colonne articolo
      If oParam.strCodart <> "" Then
        '------------------------------------------------------------------------------------------------------------
        '--- Al solo caricamento della form, applica la configurazione delle colonne Articolo/Descrizione articolo.
        '--- Mentre, in uscita dal Ctrl + Click, deve mantenere ciò che è stato impostato
        '------------------------------------------------------------------------------------------------------------ 
        If bOnLoading = True Then
          mo_codart.Visible = False
          mo_descr.Visible = False
        End If
        If CBool(oMenu.GetSettingBus("BSORHLMO", "Opzioni", ".", "NonApplicareFiltroFaseArticolo", "0", " ", "0")) Then
          Me.Text += oApp.Tr(Me, 128666564140312500, " - Articolo: |" & oParam.strCodart & "|")
          nTipoZoom = 0 'non può operare con la preselezione delle quantità
        Else
          'standard
          Me.Text += oApp.Tr(Me, 128583645004218750, " - Articolo: |" & oParam.strCodart & "| Fase: |" & oParam.nFase.ToString & "|")
        End If
      Else
        GctlSetVisEnab(mo_codart, True)
        GctlSetVisEnab(mo_descr, True)
      End If
      'Colonne conto
      If CBool(oParam.nMastro And 1) Then
        If CBool(oMenu.GetSettingBus("Bsorgsor", "Opzioni", ".", "SelezionaOAContoDiv", "0", " ", "0")) Then
          'mantengo le colonne visibili se settata l'opzione di registro per selez righe d'ordine aperto intestete ad altro conto
          GctlSetVisEnab(xx_conto, True)
          GctlSetVisEnab(xx_desconto, True)
        Else
          xx_conto.Visible = False
          xx_desconto.Visible = False
        End If
      Else
        GctlSetVisEnab(xx_conto, True)
        GctlSetVisEnab(xx_desconto, True)
      End If
      'Colonne ordine
      If CBool(oParam.nMastro And 4) Then
        mo_tipork.Visible = False
        mo_anno.Visible = False
        mo_serie.Visible = False
        mo_numord.Visible = False
        mo_riga.Visible = False
      End If
      'Colonne magazzino
      If CBool(oParam.nMastro And 8) Then
        mo_magaz.Visible = False
        xx_magaz.Visible = False
      End If

      '--------------------------
      'gli ordini aperti non gestiscono il TCO. nascondo le colonne
      If bModTco = False Then
        For i = 1 To 24
          CType(grvHlmo.Columns("mo_quant" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
          CType(grvHlmo.Columns("mo_quaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
          CType(grvHlmo.Columns("mo_quapre" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
          CType(grvHlmo.Columns("xx_quadaeva" & i.ToString.PadLeft(2, "0"c)), NTSGridColumn).Visible = False
        Next
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckArticolo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckArticolo.CheckedChanged
    Try
      If ckArticolo.Checked = False Or oParam.strCodart.Trim = "" Then
        GctlSetVisEnab(mo_codart, True)
        GctlSetVisEnab(mo_descr, True)
      Else
        'mo_codart.Visible = False
        'mo_descr.Visible = False
      End If
      cmdRicerca_Click(cmdRicerca, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckConfermato_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckConfermato.CheckedChanged
    Try
      cmdRicerca_Click(cmdRicerca, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckNoFigliKit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNoFigliKit.CheckedChanged
    Try
      oMenu.SaveSettingBus("BSORHLMO", "RECENT", ".", "ckNoFigliKit", NTSCStr(IIf(ckNoFigliKit.Checked, "-1", "0")), ".", ".S.", "...", "...")

      cmdRicerca_Click(cmdRicerca, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCommeca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommeca.Validated
    Dim strTmp As String = ""
    Try

      If Not oCleHlmo.edCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
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
  Public Overridable Sub edAppro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAppro.Validated
    Dim strTmp As String = ""
    Try

      If Not oCleHlmo.edAppro_Validated(NTSCInt(edAppro.Text), strTmp) Then
        edAppro.Text = NTSCStr(edAppro.OldEditValue)
      Else
        lbAppro.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function PreSelQuantita(ByVal dQtaDaPresel As Decimal) As Boolean
    Dim dResid As Decimal = 0
    Dim dQta As Decimal = 0
    Dim i As Integer = 0
    Try

      dResid = dQtaDaPresel
      '--------------------
      For i = 0 To dsHlmo.Tables("MOVORD").Rows.Count - 1
        If oParam.strTipo = "W" Then
          dQta = ArrDbl(NTSCDec(grvHlmo.GetRowCellValue(i, mo_quant)) - NTSCDec(grvHlmo.GetRowCellValue(i, mo_quaeva)) - NTSCDec(grvHlmo.GetRowCellValue(i, mo_quapre)), 3)
        Else
          dQta = ArrDbl(NTSCDec(grvHlmo.GetRowCellValue(i, mo_quant)) - NTSCDec(grvHlmo.GetRowCellValue(i, mo_quaeva)), 3)
        End If

        If dQta < 0 Then dQta = 0
        If dQta <= dResid Then
          grvHlmo.SetRowCellValue(i, "xx_quadaeva", dQta)
        Else
          grvHlmo.SetRowCellValue(i, "xx_quadaeva", dResid)
        End If
        grvHlmo.SetRowCellValue(i, "xx_seleziona", "S")
        dResid = ArrDbl(dResid - dQta, 3)
        If dResid <= 0 Then Exit For
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleHlmo.Salva() Then
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Sub ValutaTipoDocumento()
    Dim dttDoc As New DataTable
    Try
      If oParam.strTipoBil.ToUpper <> "BNDKKONS" Then Return

      dttDoc.Columns.Add("cod", GetType(String))
      dttDoc.Columns.Add("val", GetType(String))

      Select Case oParam.strTipo
        Case "R", "X", "#"
          dttDoc.Rows.Add(New Object() {"A", "Fatt. Imm. Emessa"})
          dttDoc.Rows.Add(New Object() {"B", "D.D.T. Emesso"})
          dttDoc.Rows.Add(New Object() {"C", "Corrispettivo"})
          dttDoc.Rows.Add(New Object() {"F", "Ric. Fiscale Emessa"})
          dttDoc.Rows.Add(New Object() {"I", "Riemissione Ric. Fiscale"})
          dttDoc.Rows.Add(New Object() {"S", "Fatt. Ricevuta Fiscale Emessa"})
          dttDoc.Rows.Add(New Object() {"W", "Nota di prelievo"})
          dttDoc.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
        Case "Y"
          dttDoc.Rows.Add(New Object() {"W", "Nota di Prelievo"})
          dttDoc.Rows.Add(New Object() {"T", "Carico da Produz."})
          dttDoc.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
        Case "O"
          dttDoc.Rows.Add(New Object() {"L", "Fatt. Imm. Ricevuta"})
          dttDoc.Rows.Add(New Object() {"M", "D.D.T. Ricevuto"})
        Case "H"
          dttDoc.Rows.Add(New Object() {"T", "Carico da Produz."})
        Case Else : Return ' è un documento, non un ordine
      End Select

      dttDoc.AcceptChanges()
      frmEstremiDoc.Visible = True

      cbTipoRk.DataSource = dttDoc
      cbTipoRk.ValueMember = "cod"
      cbTipoRk.DisplayMember = "val"

      edAnno.Text = Now.Year.ToString
      edSerie.Text = " "

      cmdSeleziona_Click(Me, Nothing)

      cmdRicerca.Visible = False
      'cmdApriOrdine.Visible = False
      cmdNavigazione.Visible = False
      cmdRipristina.Visible = False
      pnBottom.Visible = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


End Class

