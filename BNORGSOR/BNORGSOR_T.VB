Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGSOR_T
  Private Moduli_P As Integer = CLN__STD.bsModOR
  Private ModuliExt_P As Integer = CLN__STD.bsModExtORE
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
  Public oCleGsor As CLEORGSOR
  Public dsGsor As DataSet
  Public dcGsor As New BindingSource
  Public dcGsorRighe As New BindingSource
  Public dcPromozioni As New BindingSource
  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico
  Public dsGrvTCO As New DataSet                         'conterrà il dattable per la visualizzaz delle qta per taglia 
  Public dcGsorTCO As New BindingSource

  Public bInLoadOrd As Boolean = False                    'true durante il caricamento di un ordine

  Private components As System.ComponentModel.IContainer
  Public lRigaCopiata As Integer = 0                      'utilizzata dalle voci di menu copia/incolla riga per memorizzare la riga da copiare
  Public strLastColEdited As String = ""
  Public dttGnno As DataTable
  Public dttOpens As New DataTable    'datatable contenente l'elenco dei documenti che si vogliono aprire (compilato da zoom documenti per selezionarne uno)
  Public strLastTipork As String = ""
  Public nLastAnno As Integer = 0
  Public strLastSerie As String = ""
  Public lLastNumdoc As Integer = 0

  Public lContoProposto As Integer = 0 'Utilizzato in fase di creazione di un nuovo documento dal menù social
  Public bInDeleteRow As Boolean = False
  Public bNoResetDttOpens As Boolean = False 'Per eviatere che venga resettato al cambio di tipo documetno se chiamato da DKKONS
  Public dtLastCellKeyPress As Date = Now

  Public bDaTastiNavigazione As Boolean = False
  Public bInAbilitaCellaEnter As Boolean = False

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
      Me.Height = 513

      Me.MinimumSize = Me.Size

      pnDatiTesta1.Top = 0
      pnDatiTesta2.Top = 0
      pnDatiTesta3.Top = 0
      pnDatiTesta4.Top = 0

      pnPiedeAltro.Top = 0
      pnPiedeTotali.Top = 0
      pnPiedeTrasporto.Top = 0

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOR", "BEORGSOR", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleGsor = CType(oTmp, CLEORGSOR)
      '------------------------------------------------
      bRemoting = Menu.Remoting("BNORGSOR", strRemoteServer, strRemotePort)
      AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleGsor.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
    Dim frmMsgb As FRM__MSGB
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
        Case "CaricaGrigliaPromozioni" : CaricaGrigliaPromozioni() : Return
        Case "MostraPropostaOmaggi" : MostraPropostaOmaggi() : Return
        Case "FormatQtaEsistCorpo" : FormatQtaEsistCorpo()
      End Select

      'esco se i messaggi sono dovuto all'apertura dell'ordine
      If bInLoadOrd Then Return

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
            frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE AL CONTO " & oCleGsor.lContoCF.ToString
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing
          Case "VNoteContD"
            frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE ALLA DESTINAZIONE DIVERSA DEL CONTO " & oCleGsor.lContoCF.ToString
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing
          Case "VNoteArtic"
            frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE ALL'ARTICOLO " & oCleGsor.dttArti.Rows(0)!ar_codart.ToString
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing
          Case "CambioDtPg"
            If edEt_datapag.Enabled = False Then edEt_datapag.Text = edet_datdoc.Text

          Case "GestScoVal"
            If dsGsor.Tables("CORPO").Rows.Count = 0 Then
              If ckEt_scorpo.Checked = False Then
                If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
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
            If strTmp(i).Substring(11, 1) = "S" Then
              ec_quant.Enabled = False
            Else
              If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) = "S" Then
                ec_quant.Enabled = False
              Else
                GctlSetVisEnab(ec_quant, False)
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

          Case "VNoteArtic:"
            frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
            frmMsgb.Init(oMenu, Nothing, DittaCorrente)
            frmMsgb.cmdZoom.Visible = False
            frmMsgb.cmdAnnulla.Visible = False
            frmMsgb.edMess.Text = strTmp(i).Substring(11)
            frmMsgb.Text = "NOTE RELATIVE ALL'ARTICOLO"
            frmMsgb.ShowDialog()
            frmMsgb.Dispose()
            frmMsgb = Nothing

        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGSOR_T))
Me.pnTestataTop = New NTSInformatica.NTSPanel
Me.ceColl = New NTSInformatica.NTSXXCOLB
Me.lbOfferta = New NTSInformatica.NTSLabel
Me.edNumDoc = New NTSInformatica.NTSTextBoxNum
Me.edSerieDoc = New NTSInformatica.NTSTextBoxStr
Me.edAnnoDoc = New NTSInformatica.NTSTextBoxNum
Me.cbTipoDoc = New NTSInformatica.NTSComboBox
Me.lbet_datdoc = New NTSInformatica.NTSLabel
Me.lbXx_coddest = New NTSInformatica.NTSLabel
Me.edet_datdoc = New NTSInformatica.NTSTextBoxData
Me.lbEt_coddest = New NTSInformatica.NTSLabel
Me.lbXx_conto = New NTSInformatica.NTSLabel
Me.edEt_coddest = New NTSInformatica.NTSTextBoxNum
Me.lbEt_conto = New NTSInformatica.NTSLabel
Me.edEt_conto = New NTSInformatica.NTSTextBoxNum
Me.TabPage4 = New NTSInformatica.NTSTabPage
Me.pnPage4 = New NTSInformatica.NTSPanel
Me.edEt_note = New NTSInformatica.NTSMemoBox
Me.TabPage3 = New NTSInformatica.NTSTabPage
Me.pnPage3 = New NTSInformatica.NTSPanel
Me.pnPiedeTotali = New NTSInformatica.NTSPanel
Me.lbEur1 = New NTSInformatica.NTSLabel
Me.lbValuta1 = New NTSInformatica.NTSLabel
Me.lbRiga2 = New NTSInformatica.NTSLabel
Me.edEt_speaccv = New NTSInformatica.NTSTextBoxNum
Me.edTotalev = New NTSInformatica.NTSTextBoxNum
Me.lbRiga1 = New NTSInformatica.NTSLabel
Me.edTotale = New NTSInformatica.NTSTextBoxNum
Me.lbTrasporto = New NTSInformatica.NTSLabel
Me.lbTotale = New NTSInformatica.NTSLabel
Me.edEt_speacc = New NTSInformatica.NTSTextBoxNum
Me.edResiduo = New NTSInformatica.NTSTextBoxNum
Me.lbRimanenza = New NTSInformatica.NTSLabel
Me.lbImballo = New NTSInformatica.NTSLabel
Me.edResiduov = New NTSInformatica.NTSTextBoxNum
Me.edEt_speimb = New NTSInformatica.NTSTextBoxNum
Me.edEt_abbuono = New NTSInformatica.NTSTextBoxNum
Me.lbIncasso = New NTSInformatica.NTSLabel
Me.lbAbbuono = New NTSInformatica.NTSLabel
Me.edEt_speinc = New NTSInformatica.NTSTextBoxNum
Me.edEt_pagato = New NTSInformatica.NTSTextBoxNum
Me.lbBolli = New NTSInformatica.NTSLabel
Me.lbIncassato = New NTSInformatica.NTSLabel
Me.edEt_bolli = New NTSInformatica.NTSTextBoxNum
Me.edEt_abbuonov = New NTSInformatica.NTSTextBoxNum
Me.lbNetto = New NTSInformatica.NTSLabel
Me.edEt_totomag = New NTSInformatica.NTSTextBoxNum
Me.edEt_totmerce = New NTSInformatica.NTSTextBoxNum
Me.lbOmaggi = New NTSInformatica.NTSLabel
Me.edEt_totmercev = New NTSInformatica.NTSTextBoxNum
Me.edEt_totdoc = New NTSInformatica.NTSTextBoxNum
Me.edEt_bolliv = New NTSInformatica.NTSTextBoxNum
Me.edEt_pagatov = New NTSInformatica.NTSTextBoxNum
Me.lbValuta = New NTSInformatica.NTSLabel
Me.lbDocumento = New NTSInformatica.NTSLabel
Me.edEt_speincv = New NTSInformatica.NTSTextBoxNum
Me.edEt_totomagv = New NTSInformatica.NTSTextBoxNum
Me.edEt_speimbv = New NTSInformatica.NTSTextBoxNum
Me.edEt_totdocv = New NTSInformatica.NTSTextBoxNum
Me.edTotIva = New NTSInformatica.NTSTextBoxNum
Me.lbImposta = New NTSInformatica.NTSLabel
Me.lbEur = New NTSInformatica.NTSLabel
Me.edTotImponib = New NTSInformatica.NTSTextBoxNum
Me.edTotIvav = New NTSInformatica.NTSTextBoxNum
Me.edTotImponibv = New NTSInformatica.NTSTextBoxNum
Me.lbImponibile = New NTSInformatica.NTSLabel
Me.lbSpese = New NTSInformatica.NTSLabel
Me.edTotSpese = New NTSInformatica.NTSTextBoxNum
Me.edTotSpesev = New NTSInformatica.NTSTextBoxNum
Me.pnPiedeAltro = New NTSInformatica.NTSPanel
Me.fmPartita = New NTSInformatica.NTSGroupBox
Me.lbEt_datpar = New NTSInformatica.NTSLabel
Me.edEt_datpar = New NTSInformatica.NTSTextBoxData
Me.edEt_numpar = New NTSInformatica.NTSTextBoxNum
Me.lbEt_numpar = New NTSInformatica.NTSLabel
Me.edEt_alfpar = New NTSInformatica.NTSTextBoxStr
Me.lbEt_annpar = New NTSInformatica.NTSLabel
Me.edEt_annpar = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codchia = New NTSInformatica.NTSLabel
Me.fmTco = New NTSInformatica.NTSGroupBox
Me.lbXx_codstag = New NTSInformatica.NTSLabel
Me.lbEt_codstag = New NTSInformatica.NTSLabel
Me.edEt_annotco = New NTSInformatica.NTSTextBoxNum
Me.edEt_codstag = New NTSInformatica.NTSTextBoxNum
Me.lbEt_annotco = New NTSInformatica.NTSLabel
Me.fmProvvigioni = New NTSInformatica.NTSGroupBox
Me.lbXx_codagen2 = New NTSInformatica.NTSLabel
Me.edEt_codagen2 = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codagen2 = New NTSInformatica.NTSLabel
Me.edEt_totprov2 = New NTSInformatica.NTSTextBoxNum
Me.edEt_totprov = New NTSInformatica.NTSTextBoxNum
Me.lbEt_totprov = New NTSInformatica.NTSLabel
Me.edEt_impprov = New NTSInformatica.NTSTextBoxNum
Me.lbEt_impprov = New NTSInformatica.NTSLabel
Me.lbEt_codchiaLabel = New NTSInformatica.NTSLabel
Me.lbXx_codcfam = New NTSInformatica.NTSLabel
Me.lbEt_codattpLabel = New NTSInformatica.NTSLabel
Me.lbEt_codattp = New NTSInformatica.NTSLabel
Me.lbEt_codcfam = New NTSInformatica.NTSLabel
Me.edEt_codcfam = New NTSInformatica.NTSTextBoxStr
Me.pnPiedeTrasporto = New NTSInformatica.NTSPanel
Me.lbXx_totquant = New NTSInformatica.NTSLabel
Me.edEt_caustra = New NTSInformatica.NTSTextBoxNum
Me.lbEt_acuradi = New NTSInformatica.NTSLabel
Me.lbEt_caustra = New NTSInformatica.NTSLabel
Me.edEt_aspetto = New NTSInformatica.NTSTextBoxStr
Me.lbXx_caustra = New NTSInformatica.NTSLabel
Me.edEt_pesonetto = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codaspe = New NTSInformatica.NTSLabel
Me.lbPesoNettoLordo = New NTSInformatica.NTSLabel
Me.edEt_codaspe = New NTSInformatica.NTSTextBoxNum
Me.edEt_peso = New NTSInformatica.NTSTextBoxNum
Me.lbEt_coddest2 = New NTSInformatica.NTSLabel
Me.edEt_totcoll = New NTSInformatica.NTSTextBoxNum
Me.edEt_coddest2 = New NTSInformatica.NTSTextBoxNum
Me.lbEt_totcoll = New NTSInformatica.NTSLabel
Me.lbXx_coddest2 = New NTSInformatica.NTSLabel
Me.lbXx_porto = New NTSInformatica.NTSLabel
Me.lbEt_vettor2 = New NTSInformatica.NTSLabel
Me.edEt_porto = New NTSInformatica.NTSTextBoxStr
Me.edEt_vettor2 = New NTSInformatica.NTSTextBoxNum
Me.lbEt_porto = New NTSInformatica.NTSLabel
Me.lbXx_vettor2 = New NTSInformatica.NTSLabel
Me.cbEt_acuradi = New NTSInformatica.NTSComboBox
Me.pnPiedeSx = New NTSInformatica.NTSPanel
Me.cmdPiedeAltro = New NTSInformatica.NTSButton
Me.cmdPiedeTrasporto = New NTSInformatica.NTSButton
Me.cmdPiedeTotali = New NTSInformatica.NTSButton
Me.TabPage2 = New NTSInformatica.NTSTabPage
Me.pnGriglia = New NTSInformatica.NTSPanel
Me.grRighe = New NTSInformatica.NTSGrid
Me.grvRighe = New NTSInformatica.NTSGridView
Me.xxo_prznet = New NTSInformatica.NTSGridColumn
Me.xx_sel = New NTSInformatica.NTSGridColumn
Me.ec_riga = New NTSInformatica.NTSGridColumn
Me.ec_matric = New NTSInformatica.NTSGridColumn
Me.ec_codart = New NTSInformatica.NTSGridColumn
Me.ec_descr = New NTSInformatica.NTSGridColumn
Me.ec_desint = New NTSInformatica.NTSGridColumn
Me.ec_unmis = New NTSInformatica.NTSGridColumn
Me.ec_colli = New NTSInformatica.NTSGridColumn
Me.ec_colpre = New NTSInformatica.NTSGridColumn
Me.ec_coleva = New NTSInformatica.NTSGridColumn
Me.ec_ump = New NTSInformatica.NTSGridColumn
Me.ec_quant = New NTSInformatica.NTSGridColumn
Me.xxo_qtadaass = New NTSInformatica.NTSGridColumn
Me.xxo_flevasass = New NTSInformatica.NTSGridColumn
Me.ec_quapre = New NTSInformatica.NTSGridColumn
Me.ec_quaeva = New NTSInformatica.NTSGridColumn
Me.ec_flevapre = New NTSInformatica.NTSGridColumn
Me.ec_flevas = New NTSInformatica.NTSGridColumn
Me.ec_preziva = New NTSInformatica.NTSGridColumn
Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
Me.ec_prezzo = New NTSInformatica.NTSGridColumn
Me.ec_scont1 = New NTSInformatica.NTSGridColumn
Me.ec_scont2 = New NTSInformatica.NTSGridColumn
Me.ec_scont3 = New NTSInformatica.NTSGridColumn
Me.ec_scont4 = New NTSInformatica.NTSGridColumn
Me.ec_scont5 = New NTSInformatica.NTSGridColumn
Me.ec_scont6 = New NTSInformatica.NTSGridColumn
Me.ec_scontp = New NTSInformatica.NTSGridColumn
Me.ec_magaz = New NTSInformatica.NTSGridColumn
Me.xxo_magaz = New NTSInformatica.NTSGridColumn
Me.ec_datcons = New NTSInformatica.NTSGridColumn
Me.ec_confermato = New NTSInformatica.NTSGridColumn
Me.ec_rilasciato = New NTSInformatica.NTSGridColumn
Me.ec_aperto = New NTSInformatica.NTSGridColumn
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
Me.ec_magaz2 = New NTSInformatica.NTSGridColumn
Me.xxo_magaz2 = New NTSInformatica.NTSGridColumn
Me.ec_valore = New NTSInformatica.NTSGridColumn
Me.ec_contocontr = New NTSInformatica.NTSGridColumn
Me.xxo_contocon = New NTSInformatica.NTSGridColumn
Me.ec_datconsor = New NTSInformatica.NTSGridColumn
Me.ec_codclie = New NTSInformatica.NTSGridColumn
Me.xxo_codclie = New NTSInformatica.NTSGridColumn
Me.ec_misura1 = New NTSInformatica.NTSGridColumn
Me.ec_misura2 = New NTSInformatica.NTSGridColumn
Me.ec_misura3 = New NTSInformatica.NTSGridColumn
Me.xxo_codarfo = New NTSInformatica.NTSGridColumn
Me.ec_perqta = New NTSInformatica.NTSGridColumn
Me.ec_valoremm = New NTSInformatica.NTSGridColumn
Me.ec_flkit = New NTSInformatica.NTSGridColumn
Me.ec_ktriga = New NTSInformatica.NTSGridColumn
Me.ec_oatipo = New NTSInformatica.NTSGridColumn
Me.ec_oaanno = New NTSInformatica.NTSGridColumn
Me.ec_oaserie = New NTSInformatica.NTSGridColumn
Me.ec_oanum = New NTSInformatica.NTSGridColumn
Me.ec_oariga = New NTSInformatica.NTSGridColumn
Me.ec_oaqtadis = New NTSInformatica.NTSGridColumn
Me.ec_oacoldis = New NTSInformatica.NTSGridColumn
Me.ec_oavaldis = New NTSInformatica.NTSGridColumn
Me.ec_oasalcon = New NTSInformatica.NTSGridColumn
Me.ec_umprz = New NTSInformatica.NTSGridColumn
Me.ec_fase = New NTSInformatica.NTSGridColumn
Me.xxo_fase = New NTSInformatica.NTSGridColumn
Me.ec_codlavo = New NTSInformatica.NTSGridColumn
Me.xxo_codlavo = New NTSInformatica.NTSGridColumn
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
Me.ec_oqtipo = New NTSInformatica.NTSGridColumn
Me.ec_oqanno = New NTSInformatica.NTSGridColumn
Me.ec_oqserie = New NTSInformatica.NTSGridColumn
Me.ec_oqnum = New NTSInformatica.NTSGridColumn
Me.ec_oqvers = New NTSInformatica.NTSGridColumn
Me.ec_oqriga = New NTSInformatica.NTSGridColumn
Me.ec_oqsalcon = New NTSInformatica.NTSGridColumn
Me.ec_codtpro = New NTSInformatica.NTSGridColumn
Me.xxo_codtpro = New NTSInformatica.NTSGridColumn
Me.ec_datini = New NTSInformatica.NTSGridColumn
Me.ec_datfin = New NTSInformatica.NTSGridColumn
Me.ec_coddivi = New NTSInformatica.NTSGridColumn
Me.xxo_coddivi = New NTSInformatica.NTSGridColumn
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
Me.ec_codrepr1 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr2 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr3 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr4 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr5 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr6 = New NTSInformatica.NTSGridColumn
Me.ec_codrepr1out = New NTSInformatica.NTSGridColumn
Me.ec_codrepr2out = New NTSInformatica.NTSGridColumn
Me.ec_codrepr3out = New NTSInformatica.NTSGridColumn
Me.ec_codrepr4out = New NTSInformatica.NTSGridColumn
Me.ec_codrepr5out = New NTSInformatica.NTSGridColumn
Me.ec_codrepr6out = New NTSInformatica.NTSGridColumn
Me.ec_tiporiga = New NTSInformatica.NTSGridColumn
Me.ec_scpercdiff = New NTSInformatica.NTSGridColumn
Me.ec_prov1percdiff = New NTSInformatica.NTSGridColumn
Me.ec_prov2percdiff = New NTSInformatica.NTSGridColumn
Me.xxo_valoreiva = New NTSInformatica.NTSGridColumn
Me.xxo_percricarico = New NTSInformatica.NTSGridColumn
Me.xxo_percmargine = New NTSInformatica.NTSGridColumn
Me.xxo_marginevalore = New NTSInformatica.NTSGridColumn
Me.pnTCO = New NTSInformatica.NTSPanel
Me.grTco = New NTSInformatica.NTSGrid
Me.grvTco = New NTSInformatica.NTSGridView
Me.ec_quant01 = New NTSInformatica.NTSGridColumn
Me.ec_quant02 = New NTSInformatica.NTSGridColumn
Me.ec_quant03 = New NTSInformatica.NTSGridColumn
Me.ec_quant04 = New NTSInformatica.NTSGridColumn
Me.ec_quant05 = New NTSInformatica.NTSGridColumn
Me.ec_quant06 = New NTSInformatica.NTSGridColumn
Me.ec_quant07 = New NTSInformatica.NTSGridColumn
Me.ec_quant08 = New NTSInformatica.NTSGridColumn
Me.ec_quant09 = New NTSInformatica.NTSGridColumn
Me.ec_quant10 = New NTSInformatica.NTSGridColumn
Me.ec_quant11 = New NTSInformatica.NTSGridColumn
Me.ec_quant12 = New NTSInformatica.NTSGridColumn
Me.ec_quant13 = New NTSInformatica.NTSGridColumn
Me.ec_quant14 = New NTSInformatica.NTSGridColumn
Me.ec_quant15 = New NTSInformatica.NTSGridColumn
Me.ec_quant16 = New NTSInformatica.NTSGridColumn
Me.ec_quant17 = New NTSInformatica.NTSGridColumn
Me.ec_quant18 = New NTSInformatica.NTSGridColumn
Me.ec_quant19 = New NTSInformatica.NTSGridColumn
Me.ec_quant20 = New NTSInformatica.NTSGridColumn
Me.ec_quant21 = New NTSInformatica.NTSGridColumn
Me.ec_quant22 = New NTSInformatica.NTSGridColumn
Me.ec_quant23 = New NTSInformatica.NTSGridColumn
Me.ec_quant24 = New NTSInformatica.NTSGridColumn
Me.pnCorpo = New NTSInformatica.NTSPanel
Me.lbSconto = New NTSInformatica.NTSLabel
Me.edSconto = New NTSInformatica.NTSTextBoxNum
Me.edUltCost = New NTSInformatica.NTSTextBoxNum
Me.edPreList = New NTSInformatica.NTSTextBoxNum
Me.edOrdinato = New NTSInformatica.NTSTextBoxNum
Me.edDispNetta = New NTSInformatica.NTSTextBoxNum
Me.edImpegnato = New NTSInformatica.NTSTextBoxNum
Me.lbOrdinImpegn = New NTSInformatica.NTSLabel
Me.edDispon = New NTSInformatica.NTSTextBoxNum
Me.lbDispon = New NTSInformatica.NTSLabel
Me.lbPreList = New NTSInformatica.NTSLabel
Me.TabPage1 = New NTSInformatica.NTSTabPage
Me.pnTestataDx = New NTSInformatica.NTSPanel
Me.pnDatiTesta2 = New NTSInformatica.NTSPanel
Me.ckEt_rilasciato = New NTSInformatica.NTSCheckBox
Me.ckEt_aperto = New NTSInformatica.NTSCheckBox
Me.ckEt_scorpo = New NTSInformatica.NTSCheckBox
Me.ckEt_flspinc = New NTSInformatica.NTSCheckBox
Me.ckEt_flboll = New NTSInformatica.NTSCheckBox
Me.ckEt_soloasa = New NTSInformatica.NTSCheckBox
Me.edEt_datcons = New NTSInformatica.NTSTextBoxData
Me.lbEt_datcons = New NTSInformatica.NTSLabel
Me.edEt_magaz = New NTSInformatica.NTSTextBoxNum
Me.lbEt_magaz = New NTSInformatica.NTSLabel
Me.lbXx_magaz = New NTSInformatica.NTSLabel
Me.lbEt_magaz2 = New NTSInformatica.NTSLabel
Me.lbEt_magimp = New NTSInformatica.NTSLabel
Me.lbXx_codpaga = New NTSInformatica.NTSLabel
Me.edEt_magaz2 = New NTSInformatica.NTSTextBoxNum
Me.edEt_scont2 = New NTSInformatica.NTSTextBoxNum
Me.edEt_magimp = New NTSInformatica.NTSTextBoxNum
Me.edEt_scont1 = New NTSInformatica.NTSTextBoxNum
Me.lbXx_magaz2 = New NTSInformatica.NTSLabel
Me.edEt_datapag = New NTSInformatica.NTSTextBoxData
Me.lbXx_magimp = New NTSInformatica.NTSLabel
Me.edEt_scopag = New NTSInformatica.NTSTextBoxNum
Me.edEt_listino = New NTSInformatica.NTSTextBoxNum
Me.edEt_codpaga = New NTSInformatica.NTSTextBoxNum
Me.lbEt_listino = New NTSInformatica.NTSLabel
Me.lbEt_codpaga = New NTSInformatica.NTSLabel
Me.lbEt_scopag = New NTSInformatica.NTSLabel
Me.lbEt_datapag = New NTSInformatica.NTSLabel
Me.lbScontiTesta = New NTSInformatica.NTSLabel
Me.pnDatiTesta4 = New NTSInformatica.NTSPanel
Me.fmPa = New NTSInformatica.NTSGroupBox
Me.edEt_riferimpa = New NTSInformatica.NTSTextBoxStr
Me.lbEt_riferimpa = New NTSInformatica.NTSLabel
Me.edEt_cig = New NTSInformatica.NTSTextBoxStr
Me.edEt_cup = New NTSInformatica.NTSTextBoxStr
Me.lbEt_cig = New NTSInformatica.NTSLabel
Me.lbEt_cup = New NTSInformatica.NTSLabel
Me.edEt_valuta = New NTSInformatica.NTSTextBoxNum
Me.edEt_cambio = New NTSInformatica.NTSTextBoxNum
Me.lbXx_valuta = New NTSInformatica.NTSLabel
Me.lbEt_valuta = New NTSInformatica.NTSLabel
Me.lbXx_vettor = New NTSInformatica.NTSLabel
Me.lbXx_contfatt = New NTSInformatica.NTSLabel
Me.lbEt_vettor = New NTSInformatica.NTSLabel
Me.edEt_contfatt = New NTSInformatica.NTSTextBoxNum
Me.lbEt_contfatt = New NTSInformatica.NTSLabel
Me.edEt_vettor = New NTSInformatica.NTSTextBoxNum
Me.lbXx_controp = New NTSInformatica.NTSLabel
Me.lbXx_codese = New NTSInformatica.NTSLabel
Me.lbEt_controp = New NTSInformatica.NTSLabel
Me.lbEt_codese = New NTSInformatica.NTSLabel
Me.edEt_controp = New NTSInformatica.NTSTextBoxNum
Me.edEt_codese = New NTSInformatica.NTSTextBoxNum
Me.pnDatiTesta1 = New NTSInformatica.NTSPanel
Me.lbXx_organig = New NTSInformatica.NTSLabel
Me.edEt_organig = New NTSInformatica.NTSTextBoxNum
Me.lbReferente = New NTSInformatica.NTSLabel
Me.ckEt_sospeso = New NTSInformatica.NTSCheckBox
Me.ckEt_confermato = New NTSInformatica.NTSCheckBox
Me.ckEt_flevas = New NTSInformatica.NTSCheckBox
Me.lbXx_tipobf = New NTSInformatica.NTSLabel
Me.ckEt_flstam = New NTSInformatica.NTSCheckBox
Me.lbEt_tipobf = New NTSInformatica.NTSLabel
Me.edEt_tipobf = New NTSInformatica.NTSTextBoxNum
Me.cbEt_blocco = New NTSInformatica.NTSComboBox
Me.edEt_riferim = New NTSInformatica.NTSTextBoxStr
Me.lbEt_riferim = New NTSInformatica.NTSLabel
Me.lbXx_codagen = New NTSInformatica.NTSLabel
Me.lbEt_codagen = New NTSInformatica.NTSLabel
Me.edEt_codagen = New NTSInformatica.NTSTextBoxNum
Me.pnDatiTesta3 = New NTSInformatica.NTSPanel
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
Me.fmCa = New NTSInformatica.NTSGroupBox
Me.lbXx_codcli = New NTSInformatica.NTSLabel
Me.edEt_codcli = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codcli = New NTSInformatica.NTSLabel
Me.lbXx_coddivi = New NTSInformatica.NTSLabel
Me.edEt_coddivi = New NTSInformatica.NTSTextBoxNum
Me.lbEt_coddivi = New NTSInformatica.NTSLabel
Me.edEt_subcommeca = New NTSInformatica.NTSTextBoxStr
Me.lbXx_commeca = New NTSInformatica.NTSLabel
Me.lbXx_codcena = New NTSInformatica.NTSLabel
Me.edEt_codcena = New NTSInformatica.NTSTextBoxNum
Me.edEt_commeca = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codcena = New NTSInformatica.NTSLabel
Me.lbEt_commeca = New NTSInformatica.NTSLabel
Me.pnTestataSx = New NTSInformatica.NTSPanel
Me.cmdSitcli = New NTSInformatica.NTSButton
Me.cmdDatiTesta4 = New NTSInformatica.NTSButton
Me.cmdDatiTesta3 = New NTSInformatica.NTSButton
Me.cmdDatiTesta2 = New NTSInformatica.NTSButton
Me.cmdDatiTesta1 = New NTSInformatica.NTSButton
Me.tsGsor = New NTSInformatica.NTSTabControl
Me.tpContratti = New NTSInformatica.NTSTabPage
Me.pnContratti = New NTSInformatica.NTSPanel
Me.fmContratti = New NTSInformatica.NTSGroupBox
Me.edEt_codtes = New NTSInformatica.NTSTextBoxNum
Me.lbEt_codtes = New NTSInformatica.NTSLabel
Me.pnPromoData = New NTSInformatica.NTSPanel
Me.lbEt_datpromo = New NTSInformatica.NTSLabel
Me.edEt_datpromo = New NTSInformatica.NTSTextBoxData
Me.lbEt_orprom = New NTSInformatica.NTSLabel
Me.edEt_orpromo = New NTSInformatica.NTSTextBoxNum
Me.cmdCaricaContratti = New NTSInformatica.NTSButton
Me.cmdCancContratto6 = New NTSInformatica.NTSButton
Me.lbEt_codtes2 = New NTSInformatica.NTSLabel
Me.cmdCancContratto5 = New NTSInformatica.NTSButton
Me.lbEt_codtes3 = New NTSInformatica.NTSLabel
Me.cmdCancContratto4 = New NTSInformatica.NTSButton
Me.lbEt_codtes4 = New NTSInformatica.NTSLabel
Me.cmdCancContratto3 = New NTSInformatica.NTSButton
Me.lbEt_codtes5 = New NTSInformatica.NTSLabel
Me.cmdCancContratto2 = New NTSInformatica.NTSButton
Me.lbEt_codtes6 = New NTSInformatica.NTSLabel
Me.cmdCancContratto1 = New NTSInformatica.NTSButton
Me.edEt_codtes2 = New NTSInformatica.NTSTextBoxNum
Me.edEt_codtes3 = New NTSInformatica.NTSTextBoxNum
Me.edEt_codtes4 = New NTSInformatica.NTSTextBoxNum
Me.edEt_codtes5 = New NTSInformatica.NTSTextBoxNum
Me.lbXx_codtes6 = New NTSInformatica.NTSLabel
Me.edEt_codtes6 = New NTSInformatica.NTSTextBoxNum
Me.lbXx_codtes5 = New NTSInformatica.NTSLabel
Me.lbXx_codtes = New NTSInformatica.NTSLabel
Me.lbXx_codtes4 = New NTSInformatica.NTSLabel
Me.lbXx_codtes2 = New NTSInformatica.NTSLabel
Me.lbXx_codtes3 = New NTSInformatica.NTSLabel
Me.fmPromozioni = New NTSInformatica.NTSGroupBox
Me.grPromo = New NTSInformatica.NTSGrid
Me.grvPromo = New NTSInformatica.NTSGridView
Me.tb_codrepr = New NTSInformatica.NTSGridColumn
Me.tb_desrepr = New NTSInformatica.NTSGridColumn
Me.xx_contratto = New NTSInformatica.NTSGridColumn
Me.xx_prior = New NTSInformatica.NTSGridColumn
Me.pnPromoPulsanti = New NTSInformatica.NTSPanel
Me.cmdAnnullaPromo = New NTSInformatica.NTSButton
Me.cmdApplicaPromo = New NTSInformatica.NTSButton
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
Me.tlbSelOfferta = New NTSInformatica.NTSBarButtonItem
Me.tlbDettaglioTCO = New NTSInformatica.NTSBarButtonItem
Me.tlbMnuFile = New NTSInformatica.NTSBarSubItem
Me.tlbNuovoOrdineDaPrec = New NTSInformatica.NTSBarButtonItem
Me.tlbDatiContabiliSintetici = New NTSInformatica.NTSBarButtonItem
Me.tlbModifNumOrd = New NTSInformatica.NTSBarMenuItem
Me.tlbRicalcPrzScnPro = New NTSInformatica.NTSBarButtonItem
Me.tlbZoomBanche = New NTSInformatica.NTSBarMenuItem
Me.tlbVisDettTCO = New NTSInformatica.NTSBarMenuItem
Me.tlbGenNumComm = New NTSInformatica.NTSBarMenuItem
Me.tlbApriChiamata = New NTSInformatica.NTSBarMenuItem
Me.tlbOggettiAssociati = New NTSInformatica.NTSBarMenuItem
Me.tlbNavigazDoc = New NTSInformatica.NTSBarMenuItem
Me.tlbSchedatrasporto = New NTSInformatica.NTSBarMenuItem
Me.tlbEmail = New NTSInformatica.NTSBarButtonItem
Me.tlbSkype = New NTSInformatica.NTSBarButtonItem
Me.tlbMnuRecord = New NTSInformatica.NTSBarSubItem
Me.tlbPrecMov = New NTSInformatica.NTSBarMenuItem
Me.tlbPrecOrd = New NTSInformatica.NTSBarMenuItem
Me.tlbImpCollegati = New NTSInformatica.NTSBarMenuItem
Me.tlbLavCollegate = New NTSInformatica.NTSBarMenuItem
Me.tlbSeleziona = New NTSInformatica.NTSBarSubItem
Me.tlbSelOFA = New NTSInformatica.NTSBarButtonItem
Me.tlbSelezDaTerm = New NTSInformatica.NTSBarMenuItem
Me.tlbDaLista = New NTSInformatica.NTSBarButtonItem
Me.tlbInsRigheDaFigliDB = New NTSInformatica.NTSBarMenuItem
Me.tlbImport = New NTSInformatica.NTSBarButtonItem
Me.tlbExport = New NTSInformatica.NTSBarButtonItem
Me.tlbSelectDeselect = New NTSInformatica.NTSBarButtonItem
Me.tlbSelezArtAcc = New NTSInformatica.NTSBarMenuItem
Me.tlbSelezArtSucc = New NTSInformatica.NTSBarMenuItem
Me.tlbSelezLottoUbicaz = New NTSInformatica.NTSBarMenuItem
Me.tlbApriDocCollegati = New NTSInformatica.NTSBarSubItem
Me.tlbApriOrdineCollegato = New NTSInformatica.NTSBarMenuItem
Me.tlbApriOffertaCollegata = New NTSInformatica.NTSBarMenuItem
Me.tlbApriDB = New NTSInformatica.NTSBarMenuItem
Me.tlbApriConfiguratore = New NTSInformatica.NTSBarMenuItem
Me.tlbNoteRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbZoomRigheOff = New NTSInformatica.NTSBarMenuItem
Me.tlbCopiaRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbIncollaRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbInsRiga = New NTSInformatica.NTSBarMenuItem
Me.tlbDettQtaDate = New NTSInformatica.NTSBarMenuItem
Me.tlbVisDispComm = New NTSInformatica.NTSBarMenuItem
Me.tlbVisDispVariante = New NTSInformatica.NTSBarMenuItem
Me.tlbVisDettEvasione = New NTSInformatica.NTSBarMenuItem
Me.tlbNavigazMrp = New NTSInformatica.NTSBarMenuItem
Me.tlbRicalcolaColliQta = New NTSInformatica.NTSBarMenuItem
Me.tlbRicalcolaListini = New NTSInformatica.NTSBarMenuItem
Me.tlbSalvaCondPart = New NTSInformatica.NTSBarMenuItem
Me.tlbCancRigheSel = New NTSInformatica.NTSBarMenuItem
Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
Me.tlbCambioDitta = New NTSInformatica.NTSBarMenuItem
Me.tlbStampaEtichetteRiga = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaEtichetteFinale = New NTSInformatica.NTSBarMenuItem
Me.tlbImpostaFileTerm = New NTSInformatica.NTSBarMenuItem
Me.tlbRicaricaGreg = New NTSInformatica.NTSBarMenuItem
Me.tlbSalvanuovo = New NTSInformatica.NTSBarMenuItem
Me.tlbRiapriDopoSalva = New NTSInformatica.NTSBarMenuItem
Me.tlbOrdinaRighe = New NTSInformatica.NTSBarSubItem
Me.tlbOrdinaRigheRiga = New NTSInformatica.NTSBarButtonItem
Me.tlbOrdinaRigheCodiceArticolo = New NTSInformatica.NTSBarButtonItem
Me.tlbOrdinaRigheDescrizione = New NTSInformatica.NTSBarButtonItem
Me.tlbOrdinaRigheCommessa = New NTSInformatica.NTSBarButtonItem
Me.tlbRiordinarighe = New NTSInformatica.NTSBarMenuItem
Me.tlbPersVend = New NTSInformatica.NTSBarMenuItem
Me.tlbPersAcq = New NTSInformatica.NTSBarMenuItem
Me.tlbNumerazioni = New NTSInformatica.NTSBarMenuItem
Me.tlbTabBolli = New NTSInformatica.NTSBarMenuItem
Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem
Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
CType(Me.dttSmartArt,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnTestataTop,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataTop.SuspendLayout
CType(Me.edNumDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edSerieDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edAnnoDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.cbTipoDoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edet_datdoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_coddest.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_conto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage4.SuspendLayout
CType(Me.pnPage4,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPage4.SuspendLayout
CType(Me.edEt_note.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage3.SuspendLayout
CType(Me.pnPage3,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPage3.SuspendLayout
CType(Me.pnPiedeTotali,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeTotali.SuspendLayout
CType(Me.edEt_speaccv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotalev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotale.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speacc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edResiduo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edResiduov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speimb.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abbuono.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speinc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pagato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_bolli.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abbuonov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totomag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totmerce.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totmercev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totdoc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_bolliv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pagatov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speincv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totomagv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_speimbv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totdocv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotIva.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotImponib.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotIvav.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotImponibv.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotSpese.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edTotSpesev.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPiedeAltro,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeAltro.SuspendLayout
CType(Me.fmPartita,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmPartita.SuspendLayout
CType(Me.edEt_datpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_numpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_alfpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_annpar.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmTco,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmTco.SuspendLayout
CType(Me.edEt_annotco.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codstag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmProvvigioni,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmProvvigioni.SuspendLayout
CType(Me.edEt_codagen2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totprov2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totprov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_impprov.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codcfam.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPiedeTrasporto,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeTrasporto.SuspendLayout
CType(Me.edEt_caustra.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_aspetto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_pesonetto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codaspe.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_peso.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_totcoll.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_coddest2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_porto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_vettor2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.cbEt_acuradi.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPiedeSx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPiedeSx.SuspendLayout
Me.TabPage2.SuspendLayout
CType(Me.pnGriglia,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnGriglia.SuspendLayout
CType(Me.grRighe,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.grvRighe,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnTCO,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTCO.SuspendLayout
CType(Me.grTco,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.grvTco,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnCorpo,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnCorpo.SuspendLayout
CType(Me.edSconto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edUltCost.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edPreList.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edOrdinato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edDispNetta.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edImpegnato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edDispon.Properties,System.ComponentModel.ISupportInitialize).BeginInit
Me.TabPage1.SuspendLayout
CType(Me.pnTestataDx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataDx.SuspendLayout
CType(Me.pnDatiTesta2,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta2.SuspendLayout
CType(Me.ckEt_rilasciato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_aperto.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_scorpo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flspinc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flboll.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_soloasa.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_datcons.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_magaz.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_magaz2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scont2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_magimp.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scont1.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_datapag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_scopag.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_listino.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codpaga.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnDatiTesta4,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta4.SuspendLayout
CType(Me.fmPa,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmPa.SuspendLayout
CType(Me.edEt_riferimpa.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cig.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cup.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_valuta.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cambio.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_contfatt.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_vettor.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_controp.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codese.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnDatiTesta1,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta1.SuspendLayout
CType(Me.edEt_organig.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_sospeso.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_confermato.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flevas.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ckEt_flstam.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_tipobf.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.cbEt_blocco.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_riferim.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codagen.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnDatiTesta3,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnDatiTesta3.SuspendLayout
CType(Me.fmBanca,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmBanca.SuspendLayout
CType(Me.edEt_codbanc.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_banc2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_banc1.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_cab.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_abi.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmCa,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmCa.SuspendLayout
CType(Me.edEt_codcli.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_coddivi.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_subcommeca.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codcena.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_commeca.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnTestataSx,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnTestataSx.SuspendLayout
CType(Me.tsGsor,System.ComponentModel.ISupportInitialize).BeginInit
Me.tsGsor.SuspendLayout
Me.tpContratti.SuspendLayout
CType(Me.pnContratti,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnContratti.SuspendLayout
CType(Me.fmContratti,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmContratti.SuspendLayout
CType(Me.edEt_codtes.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPromoData,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPromoData.SuspendLayout
CType(Me.edEt_datpromo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_orpromo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codtes2.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codtes3.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codtes4.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codtes5.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.edEt_codtes6.Properties,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.fmPromozioni,System.ComponentModel.ISupportInitialize).BeginInit
Me.fmPromozioni.SuspendLayout
CType(Me.grPromo,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.grvPromo,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.pnPromoPulsanti,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnPromoPulsanti.SuspendLayout
CType(Me.NtsBarManager1,System.ComponentModel.ISupportInitialize).BeginInit
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
Me.pnTestataTop.Controls.Add(Me.ceColl)
Me.pnTestataTop.Controls.Add(Me.lbOfferta)
Me.pnTestataTop.Controls.Add(Me.edNumDoc)
Me.pnTestataTop.Controls.Add(Me.edSerieDoc)
Me.pnTestataTop.Controls.Add(Me.edAnnoDoc)
Me.pnTestataTop.Controls.Add(Me.cbTipoDoc)
Me.pnTestataTop.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataTop.Dock = System.Windows.Forms.DockStyle.Top
Me.pnTestataTop.Location = New System.Drawing.Point(0, 30)
Me.pnTestataTop.Name = "pnTestataTop"
Me.pnTestataTop.NTSActiveTrasparency = true
Me.pnTestataTop.Size = New System.Drawing.Size(731, 31)
Me.pnTestataTop.TabIndex = 41
'
'ceColl
'
Me.ceColl.cmdApri = Nothing
Me.ceColl.cmdNuovo = Nothing
Me.ceColl.lbCounter = Nothing
Me.ceColl.lbPost = Nothing
Me.ceColl.Location = New System.Drawing.Point(614, 3)
Me.ceColl.Name = "ceColl"
Me.ceColl.pnAll = Nothing
Me.ceColl.Size = New System.Drawing.Size(117, 28)
Me.ceColl.strNomeCampo = ""
Me.ceColl.TabIndex = 85
Me.ceColl.Visible = false
'
'lbOfferta
'
Me.lbOfferta.AutoSize = true
Me.lbOfferta.BackColor = System.Drawing.Color.Transparent
Me.lbOfferta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbOfferta.Location = New System.Drawing.Point(8, 9)
Me.lbOfferta.Name = "lbOfferta"
Me.lbOfferta.NTSDbField = ""
Me.lbOfferta.Size = New System.Drawing.Size(191, 13)
Me.lbOfferta.TabIndex = 84
Me.lbOfferta.Text = "Tipo ordine / Anno / serie / num."
Me.lbOfferta.Tooltip = ""
Me.lbOfferta.UseMnemonic = false
'
'edNumDoc
'
Me.edNumDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edNumDoc.EditValue = "0"
Me.edNumDoc.Location = New System.Drawing.Point(525, 6)
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
Me.edSerieDoc.Cursor = System.Windows.Forms.Cursors.Hand
Me.edSerieDoc.EditValue = " "
Me.edSerieDoc.Location = New System.Drawing.Point(474, 6)
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
'edAnnoDoc
'
Me.edAnnoDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edAnnoDoc.EditValue = "2008"
Me.edAnnoDoc.Location = New System.Drawing.Point(413, 6)
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
'cbTipoDoc
'
Me.cbTipoDoc.Cursor = System.Windows.Forms.Cursors.Default
Me.cbTipoDoc.DataSource = Nothing
Me.cbTipoDoc.DisplayMember = ""
Me.cbTipoDoc.Location = New System.Drawing.Point(214, 6)
Me.cbTipoDoc.Name = "cbTipoDoc"
Me.cbTipoDoc.NTSDbField = ""
Me.cbTipoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.cbTipoDoc.Properties.Appearance.Options.UseBackColor = true
Me.cbTipoDoc.Properties.AutoHeight = false
Me.cbTipoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbTipoDoc.Properties.DropDownRows = 30
Me.cbTipoDoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbTipoDoc.SelectedValue = ""
Me.cbTipoDoc.Size = New System.Drawing.Size(191, 20)
Me.cbTipoDoc.TabIndex = 0
Me.cbTipoDoc.ValueMember = ""
'
'lbet_datdoc
'
Me.lbet_datdoc.AutoSize = true
Me.lbet_datdoc.BackColor = System.Drawing.Color.Transparent
Me.lbet_datdoc.Location = New System.Drawing.Point(5, 8)
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
Me.lbXx_coddest.Location = New System.Drawing.Point(149, 57)
Me.lbXx_coddest.Name = "lbXx_coddest"
Me.lbXx_coddest.NTSDbField = ""
Me.lbXx_coddest.Size = New System.Drawing.Size(403, 22)
Me.lbXx_coddest.TabIndex = 47
Me.lbXx_coddest.Tooltip = ""
Me.lbXx_coddest.UseMnemonic = false
'
'edet_datdoc
'
Me.edet_datdoc.Cursor = System.Windows.Forms.Cursors.Default
Me.edet_datdoc.EditValue = "01/01/2008"
Me.edet_datdoc.Location = New System.Drawing.Point(58, 5)
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
Me.lbEt_coddest.Location = New System.Drawing.Point(5, 60)
Me.lbEt_coddest.Name = "lbEt_coddest"
Me.lbEt_coddest.NTSDbField = ""
Me.lbEt_coddest.Size = New System.Drawing.Size(52, 13)
Me.lbEt_coddest.TabIndex = 46
Me.lbEt_coddest.Text = "Destinaz."
Me.lbEt_coddest.Tooltip = ""
Me.lbEt_coddest.UseMnemonic = false
'
'lbXx_conto
'
Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_conto.Location = New System.Drawing.Point(149, 5)
Me.lbXx_conto.Name = "lbXx_conto"
Me.lbXx_conto.NTSDbField = ""
Me.lbXx_conto.Size = New System.Drawing.Size(403, 46)
Me.lbXx_conto.TabIndex = 7
Me.lbXx_conto.Tooltip = ""
Me.lbXx_conto.UseMnemonic = false
'
'edEt_coddest
'
Me.edEt_coddest.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_coddest.EditValue = "0"
Me.edEt_coddest.Location = New System.Drawing.Point(96, 57)
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
Me.edEt_coddest.Size = New System.Drawing.Size(52, 20)
Me.edEt_coddest.TabIndex = 45
'
'lbEt_conto
'
Me.lbEt_conto.AutoSize = true
Me.lbEt_conto.BackColor = System.Drawing.Color.Transparent
Me.lbEt_conto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_conto.Location = New System.Drawing.Point(5, 34)
Me.lbEt_conto.Name = "lbEt_conto"
Me.lbEt_conto.NTSDbField = ""
Me.lbEt_conto.Size = New System.Drawing.Size(57, 13)
Me.lbEt_conto.TabIndex = 43
Me.lbEt_conto.Text = "Cli./Forn."
Me.lbEt_conto.Tooltip = ""
Me.lbEt_conto.UseMnemonic = false
'
'edEt_conto
'
Me.edEt_conto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_conto.EditValue = "0"
Me.edEt_conto.Location = New System.Drawing.Point(58, 31)
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
'TabPage4
'
Me.TabPage4.AllowDrop = true
Me.TabPage4.Controls.Add(Me.pnPage4)
Me.TabPage4.Enable = true
Me.TabPage4.Name = "TabPage4"
Me.TabPage4.Size = New System.Drawing.Size(722, 384)
Me.TabPage4.Text = "&4 - Note"
'
'pnPage4
'
Me.pnPage4.AllowDrop = true
Me.pnPage4.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPage4.Appearance.Options.UseBackColor = true
Me.pnPage4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPage4.Controls.Add(Me.edEt_note)
Me.pnPage4.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnPage4.Location = New System.Drawing.Point(0, 0)
Me.pnPage4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPage4.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPage4.Name = "pnPage4"
Me.pnPage4.NTSActiveTrasparency = true
Me.pnPage4.Size = New System.Drawing.Size(722, 384)
Me.pnPage4.TabIndex = 0
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
Me.edEt_note.Size = New System.Drawing.Size(722, 384)
Me.edEt_note.TabIndex = 1
'
'TabPage3
'
Me.TabPage3.AllowDrop = true
Me.TabPage3.Controls.Add(Me.pnPage3)
Me.TabPage3.Controls.Add(Me.pnPiedeSx)
Me.TabPage3.Enable = true
Me.TabPage3.Name = "TabPage3"
Me.TabPage3.Size = New System.Drawing.Size(722, 384)
Me.TabPage3.Text = "&3 - Piede"
'
'pnPage3
'
Me.pnPage3.AllowDrop = true
Me.pnPage3.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPage3.Appearance.Options.UseBackColor = true
Me.pnPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPage3.Controls.Add(Me.pnPiedeTotali)
Me.pnPage3.Controls.Add(Me.pnPiedeAltro)
Me.pnPage3.Controls.Add(Me.pnPiedeTrasporto)
Me.pnPage3.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPage3.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnPage3.Location = New System.Drawing.Point(152, 0)
Me.pnPage3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPage3.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPage3.Name = "pnPage3"
Me.pnPage3.NTSActiveTrasparency = true
Me.pnPage3.Size = New System.Drawing.Size(570, 384)
Me.pnPage3.TabIndex = 86
Me.pnPage3.Text = "NtsPanel1"
'
'pnPiedeTotali
'
Me.pnPiedeTotali.AllowDrop = true
Me.pnPiedeTotali.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeTotali.Appearance.Options.UseBackColor = true
Me.pnPiedeTotali.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeTotali.Controls.Add(Me.lbEur1)
Me.pnPiedeTotali.Controls.Add(Me.lbValuta1)
Me.pnPiedeTotali.Controls.Add(Me.lbRiga2)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speaccv)
Me.pnPiedeTotali.Controls.Add(Me.edTotalev)
Me.pnPiedeTotali.Controls.Add(Me.lbRiga1)
Me.pnPiedeTotali.Controls.Add(Me.edTotale)
Me.pnPiedeTotali.Controls.Add(Me.lbTrasporto)
Me.pnPiedeTotali.Controls.Add(Me.lbTotale)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speacc)
Me.pnPiedeTotali.Controls.Add(Me.edResiduo)
Me.pnPiedeTotali.Controls.Add(Me.lbRimanenza)
Me.pnPiedeTotali.Controls.Add(Me.lbImballo)
Me.pnPiedeTotali.Controls.Add(Me.edResiduov)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speimb)
Me.pnPiedeTotali.Controls.Add(Me.edEt_abbuono)
Me.pnPiedeTotali.Controls.Add(Me.lbIncasso)
Me.pnPiedeTotali.Controls.Add(Me.lbAbbuono)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speinc)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pagato)
Me.pnPiedeTotali.Controls.Add(Me.lbBolli)
Me.pnPiedeTotali.Controls.Add(Me.lbIncassato)
Me.pnPiedeTotali.Controls.Add(Me.edEt_bolli)
Me.pnPiedeTotali.Controls.Add(Me.edEt_abbuonov)
Me.pnPiedeTotali.Controls.Add(Me.lbNetto)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totomag)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totmerce)
Me.pnPiedeTotali.Controls.Add(Me.lbOmaggi)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totmercev)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totdoc)
Me.pnPiedeTotali.Controls.Add(Me.edEt_bolliv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_pagatov)
Me.pnPiedeTotali.Controls.Add(Me.lbValuta)
Me.pnPiedeTotali.Controls.Add(Me.lbDocumento)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speincv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totomagv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_speimbv)
Me.pnPiedeTotali.Controls.Add(Me.edEt_totdocv)
Me.pnPiedeTotali.Controls.Add(Me.edTotIva)
Me.pnPiedeTotali.Controls.Add(Me.lbImposta)
Me.pnPiedeTotali.Controls.Add(Me.lbEur)
Me.pnPiedeTotali.Controls.Add(Me.edTotImponib)
Me.pnPiedeTotali.Controls.Add(Me.edTotIvav)
Me.pnPiedeTotali.Controls.Add(Me.edTotImponibv)
Me.pnPiedeTotali.Controls.Add(Me.lbImponibile)
Me.pnPiedeTotali.Controls.Add(Me.lbSpese)
Me.pnPiedeTotali.Controls.Add(Me.edTotSpese)
Me.pnPiedeTotali.Controls.Add(Me.edTotSpesev)
Me.pnPiedeTotali.Cursor = System.Windows.Forms.Cursors.Hand
Me.pnPiedeTotali.Location = New System.Drawing.Point(0, 0)
Me.pnPiedeTotali.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeTotali.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeTotali.Name = "pnPiedeTotali"
Me.pnPiedeTotali.NTSActiveTrasparency = true
Me.pnPiedeTotali.Size = New System.Drawing.Size(570, 212)
Me.pnPiedeTotali.TabIndex = 86
Me.pnPiedeTotali.Text = "NtsPanel1"
'
'lbEur1
'
Me.lbEur1.AutoSize = true
Me.lbEur1.BackColor = System.Drawing.Color.Transparent
Me.lbEur1.Location = New System.Drawing.Point(428, 0)
Me.lbEur1.Name = "lbEur1"
Me.lbEur1.NTSDbField = ""
Me.lbEur1.Size = New System.Drawing.Size(27, 13)
Me.lbEur1.TabIndex = 105
Me.lbEur1.Text = "EUR"
Me.lbEur1.Tooltip = ""
Me.lbEur1.UseMnemonic = false
'
'lbValuta1
'
Me.lbValuta1.AutoSize = true
Me.lbValuta1.BackColor = System.Drawing.Color.Transparent
Me.lbValuta1.Location = New System.Drawing.Point(344, 0)
Me.lbValuta1.Name = "lbValuta1"
Me.lbValuta1.NTSDbField = ""
Me.lbValuta1.Size = New System.Drawing.Size(37, 13)
Me.lbValuta1.TabIndex = 104
Me.lbValuta1.Text = "Valuta"
Me.lbValuta1.Tooltip = ""
Me.lbValuta1.UseMnemonic = false
'
'lbRiga2
'
Me.lbRiga2.BackColor = System.Drawing.Color.Transparent
Me.lbRiga2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.lbRiga2.Location = New System.Drawing.Point(328, 64)
Me.lbRiga2.Name = "lbRiga2"
Me.lbRiga2.NTSDbField = ""
Me.lbRiga2.Size = New System.Drawing.Size(160, 1)
Me.lbRiga2.TabIndex = 101
Me.lbRiga2.Tooltip = ""
Me.lbRiga2.UseMnemonic = false
'
'edEt_speaccv
'
Me.edEt_speaccv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speaccv.EditValue = "0"
Me.edEt_speaccv.Location = New System.Drawing.Point(81, 16)
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
'edTotalev
'
Me.edTotalev.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotalev.EditValue = "0"
Me.edTotalev.Enabled = false
Me.edTotalev.Location = New System.Drawing.Point(328, 68)
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
'lbRiga1
'
Me.lbRiga1.BackColor = System.Drawing.Color.Transparent
Me.lbRiga1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.lbRiga1.Location = New System.Drawing.Point(81, 105)
Me.lbRiga1.Name = "lbRiga1"
Me.lbRiga1.NTSDbField = ""
Me.lbRiga1.Size = New System.Drawing.Size(160, 1)
Me.lbRiga1.TabIndex = 100
Me.lbRiga1.Tooltip = ""
Me.lbRiga1.UseMnemonic = false
'
'edTotale
'
Me.edTotale.Cursor = System.Windows.Forms.Cursors.Hand
Me.edTotale.EditValue = "0"
Me.edTotale.Enabled = false
Me.edTotale.Location = New System.Drawing.Point(410, 68)
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
'lbTrasporto
'
Me.lbTrasporto.AutoSize = true
Me.lbTrasporto.BackColor = System.Drawing.Color.Transparent
Me.lbTrasporto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbTrasporto.Location = New System.Drawing.Point(9, 19)
Me.lbTrasporto.Name = "lbTrasporto"
Me.lbTrasporto.NTSDbField = ""
Me.lbTrasporto.Size = New System.Drawing.Size(52, 13)
Me.lbTrasporto.TabIndex = 54
Me.lbTrasporto.Text = "Trasporto"
Me.lbTrasporto.Tooltip = ""
Me.lbTrasporto.UseMnemonic = false
'
'lbTotale
'
Me.lbTotale.AutoSize = true
Me.lbTotale.BackColor = System.Drawing.Color.Transparent
Me.lbTotale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbTotale.Location = New System.Drawing.Point(256, 71)
Me.lbTotale.Name = "lbTotale"
Me.lbTotale.NTSDbField = ""
Me.lbTotale.Size = New System.Drawing.Size(43, 13)
Me.lbTotale.TabIndex = 95
Me.lbTotale.Text = "Totale"
Me.lbTotale.Tooltip = ""
Me.lbTotale.UseMnemonic = false
'
'edEt_speacc
'
Me.edEt_speacc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_speacc.EditValue = "0"
Me.edEt_speacc.Location = New System.Drawing.Point(163, 16)
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
'edResiduo
'
Me.edResiduo.Cursor = System.Windows.Forms.Cursors.Default
Me.edResiduo.EditValue = "0"
Me.edResiduo.Enabled = false
Me.edResiduo.Location = New System.Drawing.Point(410, 140)
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
'lbRimanenza
'
Me.lbRimanenza.AutoSize = true
Me.lbRimanenza.BackColor = System.Drawing.Color.Transparent
Me.lbRimanenza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbRimanenza.Location = New System.Drawing.Point(256, 142)
Me.lbRimanenza.Name = "lbRimanenza"
Me.lbRimanenza.NTSDbField = ""
Me.lbRimanenza.Size = New System.Drawing.Size(69, 13)
Me.lbRimanenza.TabIndex = 92
Me.lbRimanenza.Text = "Rimanenza"
Me.lbRimanenza.Tooltip = ""
Me.lbRimanenza.UseMnemonic = false
'
'lbImballo
'
Me.lbImballo.AutoSize = true
Me.lbImballo.BackColor = System.Drawing.Color.Transparent
Me.lbImballo.Location = New System.Drawing.Point(9, 41)
Me.lbImballo.Name = "lbImballo"
Me.lbImballo.NTSDbField = ""
Me.lbImballo.Size = New System.Drawing.Size(41, 13)
Me.lbImballo.TabIndex = 59
Me.lbImballo.Text = "Imballo"
Me.lbImballo.Tooltip = ""
Me.lbImballo.UseMnemonic = false
'
'edResiduov
'
Me.edResiduov.Cursor = System.Windows.Forms.Cursors.Default
Me.edResiduov.EditValue = "0"
Me.edResiduov.Enabled = false
Me.edResiduov.Location = New System.Drawing.Point(328, 140)
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
'edEt_speimb
'
Me.edEt_speimb.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speimb.EditValue = "0"
Me.edEt_speimb.Location = New System.Drawing.Point(163, 38)
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
'edEt_abbuono
'
Me.edEt_abbuono.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_abbuono.EditValue = "0"
Me.edEt_abbuono.Location = New System.Drawing.Point(410, 115)
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
'lbIncasso
'
Me.lbIncasso.AutoSize = true
Me.lbIncasso.BackColor = System.Drawing.Color.Transparent
Me.lbIncasso.Location = New System.Drawing.Point(9, 62)
Me.lbIncasso.Name = "lbIncasso"
Me.lbIncasso.NTSDbField = ""
Me.lbIncasso.Size = New System.Drawing.Size(44, 13)
Me.lbIncasso.TabIndex = 62
Me.lbIncasso.Text = "Incasso"
Me.lbIncasso.Tooltip = ""
Me.lbIncasso.UseMnemonic = false
'
'lbAbbuono
'
Me.lbAbbuono.AutoSize = true
Me.lbAbbuono.BackColor = System.Drawing.Color.Transparent
Me.lbAbbuono.Location = New System.Drawing.Point(256, 118)
Me.lbAbbuono.Name = "lbAbbuono"
Me.lbAbbuono.NTSDbField = ""
Me.lbAbbuono.Size = New System.Drawing.Size(50, 13)
Me.lbAbbuono.TabIndex = 89
Me.lbAbbuono.Text = "Abbuono"
Me.lbAbbuono.Tooltip = ""
Me.lbAbbuono.UseMnemonic = false
'
'edEt_speinc
'
Me.edEt_speinc.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speinc.EditValue = "0"
Me.edEt_speinc.Enabled = false
Me.edEt_speinc.Location = New System.Drawing.Point(163, 59)
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
'edEt_pagato
'
Me.edEt_pagato.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pagato.EditValue = "0"
Me.edEt_pagato.Location = New System.Drawing.Point(410, 92)
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
'lbBolli
'
Me.lbBolli.AutoSize = true
Me.lbBolli.BackColor = System.Drawing.Color.Transparent
Me.lbBolli.Location = New System.Drawing.Point(10, 85)
Me.lbBolli.Name = "lbBolli"
Me.lbBolli.NTSDbField = ""
Me.lbBolli.Size = New System.Drawing.Size(25, 13)
Me.lbBolli.TabIndex = 65
Me.lbBolli.Text = "Bolli"
Me.lbBolli.Tooltip = ""
Me.lbBolli.UseMnemonic = false
'
'lbIncassato
'
Me.lbIncassato.AutoSize = true
Me.lbIncassato.BackColor = System.Drawing.Color.Transparent
Me.lbIncassato.Location = New System.Drawing.Point(256, 95)
Me.lbIncassato.Name = "lbIncassato"
Me.lbIncassato.NTSDbField = ""
Me.lbIncassato.Size = New System.Drawing.Size(54, 13)
Me.lbIncassato.TabIndex = 86
Me.lbIncassato.Text = "Incassato"
Me.lbIncassato.Tooltip = ""
Me.lbIncassato.UseMnemonic = false
'
'edEt_bolli
'
Me.edEt_bolli.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_bolli.EditValue = "0"
Me.edEt_bolli.Enabled = false
Me.edEt_bolli.Location = New System.Drawing.Point(163, 82)
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
'edEt_abbuonov
'
Me.edEt_abbuonov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_abbuonov.EditValue = "0"
Me.edEt_abbuonov.Location = New System.Drawing.Point(328, 115)
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
'lbNetto
'
Me.lbNetto.AutoSize = true
Me.lbNetto.BackColor = System.Drawing.Color.Transparent
Me.lbNetto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbNetto.Location = New System.Drawing.Point(9, 117)
Me.lbNetto.Name = "lbNetto"
Me.lbNetto.NTSDbField = ""
Me.lbNetto.Size = New System.Drawing.Size(38, 13)
Me.lbNetto.TabIndex = 68
Me.lbNetto.Text = "Netto"
Me.lbNetto.Tooltip = ""
Me.lbNetto.UseMnemonic = false
'
'edEt_totomag
'
Me.edEt_totomag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totomag.EditValue = "0"
Me.edEt_totomag.Enabled = false
Me.edEt_totomag.Location = New System.Drawing.Point(410, 41)
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
'edEt_totmerce
'
Me.edEt_totmerce.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totmerce.EditValue = "0"
Me.edEt_totmerce.Enabled = false
Me.edEt_totmerce.Location = New System.Drawing.Point(163, 112)
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
'lbOmaggi
'
Me.lbOmaggi.AutoSize = true
Me.lbOmaggi.BackColor = System.Drawing.Color.Transparent
Me.lbOmaggi.Location = New System.Drawing.Point(256, 44)
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
Me.edEt_totmercev.Location = New System.Drawing.Point(81, 112)
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
Me.edEt_totdoc.Location = New System.Drawing.Point(410, 16)
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
'edEt_bolliv
'
Me.edEt_bolliv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_bolliv.EditValue = "0"
Me.edEt_bolliv.Enabled = false
Me.edEt_bolliv.Location = New System.Drawing.Point(81, 82)
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
'edEt_pagatov
'
Me.edEt_pagatov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pagatov.EditValue = "0"
Me.edEt_pagatov.Location = New System.Drawing.Point(328, 92)
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
'lbValuta
'
Me.lbValuta.AutoSize = true
Me.lbValuta.BackColor = System.Drawing.Color.Transparent
Me.lbValuta.Location = New System.Drawing.Point(107, 0)
Me.lbValuta.Name = "lbValuta"
Me.lbValuta.NTSDbField = ""
Me.lbValuta.Size = New System.Drawing.Size(37, 13)
Me.lbValuta.TabIndex = 57
Me.lbValuta.Text = "Valuta"
Me.lbValuta.Tooltip = ""
Me.lbValuta.UseMnemonic = false
'
'lbDocumento
'
Me.lbDocumento.AutoSize = true
Me.lbDocumento.BackColor = System.Drawing.Color.Transparent
Me.lbDocumento.Location = New System.Drawing.Point(256, 19)
Me.lbDocumento.Name = "lbDocumento"
Me.lbDocumento.NTSDbField = ""
Me.lbDocumento.Size = New System.Drawing.Size(61, 13)
Me.lbDocumento.TabIndex = 80
Me.lbDocumento.Text = "Documento"
Me.lbDocumento.Tooltip = ""
Me.lbDocumento.UseMnemonic = false
'
'edEt_speincv
'
Me.edEt_speincv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speincv.EditValue = "0"
Me.edEt_speincv.Enabled = false
Me.edEt_speincv.Location = New System.Drawing.Point(81, 59)
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
'edEt_totomagv
'
Me.edEt_totomagv.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_totomagv.EditValue = "0"
Me.edEt_totomagv.Enabled = false
Me.edEt_totomagv.Location = New System.Drawing.Point(328, 41)
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
'edEt_speimbv
'
Me.edEt_speimbv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_speimbv.EditValue = "0"
Me.edEt_speimbv.Location = New System.Drawing.Point(81, 38)
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
'edEt_totdocv
'
Me.edEt_totdocv.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totdocv.EditValue = "0"
Me.edEt_totdocv.Enabled = false
Me.edEt_totdocv.Location = New System.Drawing.Point(328, 16)
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
'edTotIva
'
Me.edTotIva.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotIva.EditValue = "0"
Me.edTotIva.Enabled = false
Me.edTotIva.Location = New System.Drawing.Point(163, 180)
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
'lbImposta
'
Me.lbImposta.AutoSize = true
Me.lbImposta.BackColor = System.Drawing.Color.Transparent
Me.lbImposta.Location = New System.Drawing.Point(9, 183)
Me.lbImposta.Name = "lbImposta"
Me.lbImposta.NTSDbField = ""
Me.lbImposta.Size = New System.Drawing.Size(46, 13)
Me.lbImposta.TabIndex = 77
Me.lbImposta.Text = "Imposta"
Me.lbImposta.Tooltip = ""
Me.lbImposta.UseMnemonic = false
'
'lbEur
'
Me.lbEur.AutoSize = true
Me.lbEur.BackColor = System.Drawing.Color.Transparent
Me.lbEur.Location = New System.Drawing.Point(191, 0)
Me.lbEur.Name = "lbEur"
Me.lbEur.NTSDbField = ""
Me.lbEur.Size = New System.Drawing.Size(27, 13)
Me.lbEur.TabIndex = 56
Me.lbEur.Text = "EUR"
Me.lbEur.Tooltip = ""
Me.lbEur.UseMnemonic = false
'
'edTotImponib
'
Me.edTotImponib.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotImponib.EditValue = "0"
Me.edTotImponib.Enabled = false
Me.edTotImponib.Location = New System.Drawing.Point(163, 158)
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
'edTotIvav
'
Me.edTotIvav.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotIvav.EditValue = "0"
Me.edTotIvav.Enabled = false
Me.edTotIvav.Location = New System.Drawing.Point(81, 180)
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
'edTotImponibv
'
Me.edTotImponibv.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotImponibv.EditValue = "0"
Me.edTotImponibv.Enabled = false
Me.edTotImponibv.Location = New System.Drawing.Point(81, 158)
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
'lbImponibile
'
Me.lbImponibile.AutoSize = true
Me.lbImponibile.BackColor = System.Drawing.Color.Transparent
Me.lbImponibile.Location = New System.Drawing.Point(9, 161)
Me.lbImponibile.Name = "lbImponibile"
Me.lbImponibile.NTSDbField = ""
Me.lbImponibile.Size = New System.Drawing.Size(55, 13)
Me.lbImponibile.TabIndex = 74
Me.lbImponibile.Text = "Imponibile"
Me.lbImponibile.Tooltip = ""
Me.lbImponibile.UseMnemonic = false
'
'lbSpese
'
Me.lbSpese.AutoSize = true
Me.lbSpese.BackColor = System.Drawing.Color.Transparent
Me.lbSpese.Location = New System.Drawing.Point(9, 139)
Me.lbSpese.Name = "lbSpese"
Me.lbSpese.NTSDbField = ""
Me.lbSpese.Size = New System.Drawing.Size(68, 13)
Me.lbSpese.TabIndex = 71
Me.lbSpese.Text = "Totale spese"
Me.lbSpese.Tooltip = ""
Me.lbSpese.UseMnemonic = false
'
'edTotSpese
'
Me.edTotSpese.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotSpese.EditValue = "0"
Me.edTotSpese.Enabled = false
Me.edTotSpese.Location = New System.Drawing.Point(163, 136)
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
'edTotSpesev
'
Me.edTotSpesev.Cursor = System.Windows.Forms.Cursors.Default
Me.edTotSpesev.EditValue = "0"
Me.edTotSpesev.Enabled = false
Me.edTotSpesev.Location = New System.Drawing.Point(81, 136)
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
'pnPiedeAltro
'
Me.pnPiedeAltro.AllowDrop = true
Me.pnPiedeAltro.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeAltro.Appearance.Options.UseBackColor = true
Me.pnPiedeAltro.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeAltro.Controls.Add(Me.fmPartita)
Me.pnPiedeAltro.Controls.Add(Me.lbEt_codchia)
Me.pnPiedeAltro.Controls.Add(Me.fmTco)
Me.pnPiedeAltro.Controls.Add(Me.fmProvvigioni)
Me.pnPiedeAltro.Controls.Add(Me.lbEt_codchiaLabel)
Me.pnPiedeAltro.Controls.Add(Me.lbXx_codcfam)
Me.pnPiedeAltro.Controls.Add(Me.lbEt_codattpLabel)
Me.pnPiedeAltro.Controls.Add(Me.lbEt_codattp)
Me.pnPiedeAltro.Controls.Add(Me.lbEt_codcfam)
Me.pnPiedeAltro.Controls.Add(Me.edEt_codcfam)
Me.pnPiedeAltro.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeAltro.Location = New System.Drawing.Point(0, 399)
Me.pnPiedeAltro.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeAltro.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeAltro.Name = "pnPiedeAltro"
Me.pnPiedeAltro.NTSActiveTrasparency = true
Me.pnPiedeAltro.Size = New System.Drawing.Size(567, 185)
Me.pnPiedeAltro.TabIndex = 88
Me.pnPiedeAltro.Text = "NtsPanel3"
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
Me.fmPartita.Location = New System.Drawing.Point(3, 75)
Me.fmPartita.Name = "fmPartita"
Me.fmPartita.Size = New System.Drawing.Size(160, 101)
Me.fmPartita.TabIndex = 2
Me.fmPartita.Text = "Partita"
'
'lbEt_datpar
'
Me.lbEt_datpar.AutoSize = true
Me.lbEt_datpar.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datpar.Location = New System.Drawing.Point(5, 76)
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
Me.edEt_datpar.Location = New System.Drawing.Point(67, 73)
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
Me.edEt_numpar.Location = New System.Drawing.Point(67, 50)
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
Me.lbEt_numpar.Location = New System.Drawing.Point(5, 53)
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
Me.edEt_alfpar.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_alfpar.EditValue = ""
Me.edEt_alfpar.Location = New System.Drawing.Point(111, 26)
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
Me.lbEt_annpar.Location = New System.Drawing.Point(5, 29)
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
Me.edEt_annpar.Location = New System.Drawing.Point(67, 26)
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
'lbEt_codchia
'
Me.lbEt_codchia.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codchia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbEt_codchia.Location = New System.Drawing.Point(375, 77)
Me.lbEt_codchia.Name = "lbEt_codchia"
Me.lbEt_codchia.NTSDbField = ""
Me.lbEt_codchia.Size = New System.Drawing.Size(91, 20)
Me.lbEt_codchia.TabIndex = 85
Me.lbEt_codchia.TextAlign = System.Drawing.ContentAlignment.TopRight
Me.lbEt_codchia.Tooltip = ""
Me.lbEt_codchia.UseMnemonic = false
'
'fmTco
'
Me.fmTco.AllowDrop = true
Me.fmTco.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmTco.Appearance.Options.UseBackColor = true
Me.fmTco.Controls.Add(Me.lbXx_codstag)
Me.fmTco.Controls.Add(Me.lbEt_codstag)
Me.fmTco.Controls.Add(Me.edEt_annotco)
Me.fmTco.Controls.Add(Me.edEt_codstag)
Me.fmTco.Controls.Add(Me.lbEt_annotco)
Me.fmTco.Cursor = System.Windows.Forms.Cursors.Default
Me.fmTco.Location = New System.Drawing.Point(166, 104)
Me.fmTco.Name = "fmTco"
Me.fmTco.Size = New System.Drawing.Size(308, 47)
Me.fmTco.TabIndex = 80
Me.fmTco.Text = "Taglie e colori"
'
'lbXx_codstag
'
Me.lbXx_codstag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbXx_codstag.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codstag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codstag.Location = New System.Drawing.Point(209, 21)
Me.lbXx_codstag.Name = "lbXx_codstag"
Me.lbXx_codstag.NTSDbField = ""
Me.lbXx_codstag.Size = New System.Drawing.Size(91, 20)
Me.lbXx_codstag.TabIndex = 83
Me.lbXx_codstag.Tooltip = ""
Me.lbXx_codstag.UseMnemonic = false
'
'lbEt_codstag
'
Me.lbEt_codstag.AutoSize = true
Me.lbEt_codstag.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codstag.Location = New System.Drawing.Point(115, 25)
Me.lbEt_codstag.Name = "lbEt_codstag"
Me.lbEt_codstag.NTSDbField = ""
Me.lbEt_codstag.Size = New System.Drawing.Size(49, 13)
Me.lbEt_codstag.TabIndex = 79
Me.lbEt_codstag.Text = "Stagione"
Me.lbEt_codstag.Tooltip = ""
Me.lbEt_codstag.UseMnemonic = false
'
'edEt_annotco
'
Me.edEt_annotco.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_annotco.EditValue = "0"
Me.edEt_annotco.Location = New System.Drawing.Point(57, 21)
Me.edEt_annotco.Name = "edEt_annotco"
Me.edEt_annotco.NTSDbField = ""
Me.edEt_annotco.NTSFormat = "0"
Me.edEt_annotco.NTSForzaVisZoom = false
Me.edEt_annotco.NTSOldValue = ""
Me.edEt_annotco.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_annotco.Properties.Appearance.Options.UseBackColor = true
Me.edEt_annotco.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_annotco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_annotco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_annotco.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_annotco.Properties.AutoHeight = false
Me.edEt_annotco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_annotco.Properties.MaxLength = 65536
Me.edEt_annotco.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_annotco.Size = New System.Drawing.Size(53, 20)
Me.edEt_annotco.TabIndex = 76
'
'edEt_codstag
'
Me.edEt_codstag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codstag.EditValue = "0"
Me.edEt_codstag.Location = New System.Drawing.Point(165, 21)
Me.edEt_codstag.Name = "edEt_codstag"
Me.edEt_codstag.NTSDbField = ""
Me.edEt_codstag.NTSFormat = "0"
Me.edEt_codstag.NTSForzaVisZoom = false
Me.edEt_codstag.NTSOldValue = ""
Me.edEt_codstag.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codstag.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codstag.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codstag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codstag.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codstag.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codstag.Properties.AutoHeight = false
Me.edEt_codstag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codstag.Properties.MaxLength = 65536
Me.edEt_codstag.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codstag.Size = New System.Drawing.Size(42, 20)
Me.edEt_codstag.TabIndex = 77
'
'lbEt_annotco
'
Me.lbEt_annotco.AutoSize = true
Me.lbEt_annotco.BackColor = System.Drawing.Color.Transparent
Me.lbEt_annotco.Location = New System.Drawing.Point(5, 24)
Me.lbEt_annotco.Name = "lbEt_annotco"
Me.lbEt_annotco.NTSDbField = ""
Me.lbEt_annotco.Size = New System.Drawing.Size(32, 13)
Me.lbEt_annotco.TabIndex = 78
Me.lbEt_annotco.Text = "Anno"
Me.lbEt_annotco.Tooltip = ""
Me.lbEt_annotco.UseMnemonic = false
'
'fmProvvigioni
'
Me.fmProvvigioni.AllowDrop = true
Me.fmProvvigioni.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmProvvigioni.Appearance.Options.UseBackColor = true
Me.fmProvvigioni.Controls.Add(Me.lbXx_codagen2)
Me.fmProvvigioni.Controls.Add(Me.edEt_codagen2)
Me.fmProvvigioni.Controls.Add(Me.lbEt_codagen2)
Me.fmProvvigioni.Controls.Add(Me.edEt_totprov2)
Me.fmProvvigioni.Controls.Add(Me.edEt_totprov)
Me.fmProvvigioni.Controls.Add(Me.lbEt_totprov)
Me.fmProvvigioni.Controls.Add(Me.edEt_impprov)
Me.fmProvvigioni.Controls.Add(Me.lbEt_impprov)
Me.fmProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
Me.fmProvvigioni.Location = New System.Drawing.Point(3, 3)
Me.fmProvvigioni.Name = "fmProvvigioni"
Me.fmProvvigioni.Size = New System.Drawing.Size(419, 68)
Me.fmProvvigioni.TabIndex = 3
Me.fmProvvigioni.Text = "Provvigioni"
'
'lbXx_codagen2
'
Me.lbXx_codagen2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codagen2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codagen2.Location = New System.Drawing.Point(149, 44)
Me.lbXx_codagen2.Name = "lbXx_codagen2"
Me.lbXx_codagen2.NTSDbField = ""
Me.lbXx_codagen2.Size = New System.Drawing.Size(182, 20)
Me.lbXx_codagen2.TabIndex = 60
Me.lbXx_codagen2.Tooltip = ""
Me.lbXx_codagen2.UseMnemonic = false
'
'edEt_codagen2
'
Me.edEt_codagen2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codagen2.EditValue = "0"
Me.edEt_codagen2.Location = New System.Drawing.Point(66, 44)
Me.edEt_codagen2.Name = "edEt_codagen2"
Me.edEt_codagen2.NTSDbField = ""
Me.edEt_codagen2.NTSFormat = "0"
Me.edEt_codagen2.NTSForzaVisZoom = false
Me.edEt_codagen2.NTSOldValue = ""
Me.edEt_codagen2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codagen2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codagen2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codagen2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codagen2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codagen2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codagen2.Properties.AutoHeight = false
Me.edEt_codagen2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codagen2.Properties.MaxLength = 65536
Me.edEt_codagen2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codagen2.Size = New System.Drawing.Size(81, 20)
Me.edEt_codagen2.TabIndex = 59
'
'lbEt_codagen2
'
Me.lbEt_codagen2.AutoSize = true
Me.lbEt_codagen2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codagen2.Location = New System.Drawing.Point(4, 47)
Me.lbEt_codagen2.Name = "lbEt_codagen2"
Me.lbEt_codagen2.NTSDbField = ""
Me.lbEt_codagen2.Size = New System.Drawing.Size(51, 13)
Me.lbEt_codagen2.TabIndex = 57
Me.lbEt_codagen2.Text = "Agente 2"
Me.lbEt_codagen2.Tooltip = ""
Me.lbEt_codagen2.UseMnemonic = false
'
'edEt_totprov2
'
Me.edEt_totprov2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totprov2.EditValue = "0"
Me.edEt_totprov2.Enabled = false
Me.edEt_totprov2.Location = New System.Drawing.Point(337, 44)
Me.edEt_totprov2.Name = "edEt_totprov2"
Me.edEt_totprov2.NTSDbField = ""
Me.edEt_totprov2.NTSFormat = "0"
Me.edEt_totprov2.NTSForzaVisZoom = false
Me.edEt_totprov2.NTSOldValue = ""
Me.edEt_totprov2.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_totprov2.Properties.Appearance.Options.UseBackColor = true
Me.edEt_totprov2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_totprov2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_totprov2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_totprov2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_totprov2.Properties.AutoHeight = false
Me.edEt_totprov2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_totprov2.Properties.MaxLength = 65536
Me.edEt_totprov2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_totprov2.Size = New System.Drawing.Size(77, 20)
Me.edEt_totprov2.TabIndex = 56
'
'edEt_totprov
'
Me.edEt_totprov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totprov.EditValue = "0"
Me.edEt_totprov.Enabled = false
Me.edEt_totprov.Location = New System.Drawing.Point(337, 22)
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
Me.edEt_totprov.Size = New System.Drawing.Size(77, 20)
Me.edEt_totprov.TabIndex = 55
'
'lbEt_totprov
'
Me.lbEt_totprov.AutoSize = true
Me.lbEt_totprov.BackColor = System.Drawing.Color.Transparent
Me.lbEt_totprov.Location = New System.Drawing.Point(255, 25)
Me.lbEt_totprov.Name = "lbEt_totprov"
Me.lbEt_totprov.NTSDbField = ""
Me.lbEt_totprov.Size = New System.Drawing.Size(76, 13)
Me.lbEt_totprov.TabIndex = 54
Me.lbEt_totprov.Text = "Provv. agente"
Me.lbEt_totprov.Tooltip = ""
Me.lbEt_totprov.UseMnemonic = false
'
'edEt_impprov
'
Me.edEt_impprov.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_impprov.EditValue = "0"
Me.edEt_impprov.Enabled = false
Me.edEt_impprov.Location = New System.Drawing.Point(66, 22)
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
Me.edEt_impprov.Size = New System.Drawing.Size(81, 20)
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
'lbEt_codchiaLabel
'
Me.lbEt_codchiaLabel.AutoSize = true
Me.lbEt_codchiaLabel.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codchiaLabel.Location = New System.Drawing.Point(317, 80)
Me.lbEt_codchiaLabel.Name = "lbEt_codchiaLabel"
Me.lbEt_codchiaLabel.NTSDbField = ""
Me.lbEt_codchiaLabel.Size = New System.Drawing.Size(52, 13)
Me.lbEt_codchiaLabel.TabIndex = 81
Me.lbEt_codchiaLabel.Text = "Chiamata"
Me.lbEt_codchiaLabel.Tooltip = ""
Me.lbEt_codchiaLabel.UseMnemonic = false
'
'lbXx_codcfam
'
Me.lbXx_codcfam.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codcfam.Location = New System.Drawing.Point(283, 156)
Me.lbXx_codcfam.Name = "lbXx_codcfam"
Me.lbXx_codcfam.NTSDbField = ""
Me.lbXx_codcfam.Size = New System.Drawing.Size(184, 20)
Me.lbXx_codcfam.TabIndex = 82
Me.lbXx_codcfam.Tooltip = ""
Me.lbXx_codcfam.UseMnemonic = false
'
'lbEt_codattpLabel
'
Me.lbEt_codattpLabel.AutoSize = true
Me.lbEt_codattpLabel.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codattpLabel.Location = New System.Drawing.Point(166, 80)
Me.lbEt_codattpLabel.Name = "lbEt_codattpLabel"
Me.lbEt_codattpLabel.NTSDbField = ""
Me.lbEt_codattpLabel.Size = New System.Drawing.Size(69, 13)
Me.lbEt_codattpLabel.TabIndex = 82
Me.lbEt_codattpLabel.Text = "Attivit. prev."
Me.lbEt_codattpLabel.Tooltip = ""
Me.lbEt_codattpLabel.UseMnemonic = false
'
'lbEt_codattp
'
Me.lbEt_codattp.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codattp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbEt_codattp.Location = New System.Drawing.Point(240, 77)
Me.lbEt_codattp.Name = "lbEt_codattp"
Me.lbEt_codattp.NTSDbField = ""
Me.lbEt_codattp.Size = New System.Drawing.Size(70, 20)
Me.lbEt_codattp.TabIndex = 84
Me.lbEt_codattp.TextAlign = System.Drawing.ContentAlignment.TopRight
Me.lbEt_codattp.Tooltip = ""
Me.lbEt_codattp.UseMnemonic = false
'
'lbEt_codcfam
'
Me.lbEt_codcfam.AutoSize = true
Me.lbEt_codcfam.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codcfam.Location = New System.Drawing.Point(166, 159)
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
Me.edEt_codcfam.Location = New System.Drawing.Point(223, 156)
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
Me.edEt_codcfam.Size = New System.Drawing.Size(53, 20)
Me.edEt_codcfam.TabIndex = 81
'
'pnPiedeTrasporto
'
Me.pnPiedeTrasporto.AllowDrop = true
Me.pnPiedeTrasporto.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeTrasporto.Appearance.Options.UseBackColor = true
Me.pnPiedeTrasporto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_totquant)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_acuradi)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_aspetto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_caustra)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_pesonetto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_codaspe)
Me.pnPiedeTrasporto.Controls.Add(Me.lbPesoNettoLordo)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_codaspe)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_peso)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_totcoll)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_totcoll)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_coddest2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.edEt_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.lbEt_porto)
Me.pnPiedeTrasporto.Controls.Add(Me.lbXx_vettor2)
Me.pnPiedeTrasporto.Controls.Add(Me.cbEt_acuradi)
Me.pnPiedeTrasporto.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeTrasporto.Location = New System.Drawing.Point(0, 215)
Me.pnPiedeTrasporto.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeTrasporto.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeTrasporto.Name = "pnPiedeTrasporto"
Me.pnPiedeTrasporto.NTSActiveTrasparency = true
Me.pnPiedeTrasporto.Size = New System.Drawing.Size(570, 178)
Me.pnPiedeTrasporto.TabIndex = 87
Me.pnPiedeTrasporto.Text = "NtsPanel2"
'
'lbXx_totquant
'
Me.lbXx_totquant.BackColor = System.Drawing.Color.Transparent
Me.lbXx_totquant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_totquant.Location = New System.Drawing.Point(155, 137)
Me.lbXx_totquant.Name = "lbXx_totquant"
Me.lbXx_totquant.NTSDbField = ""
Me.lbXx_totquant.Size = New System.Drawing.Size(57, 19)
Me.lbXx_totquant.TabIndex = 74
Me.lbXx_totquant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
Me.lbXx_totquant.Tooltip = ""
Me.lbXx_totquant.UseMnemonic = false
'
'edEt_caustra
'
Me.edEt_caustra.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_caustra.EditValue = "0"
Me.edEt_caustra.Location = New System.Drawing.Point(101, 3)
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
Me.lbEt_acuradi.Location = New System.Drawing.Point(10, 50)
Me.lbEt_acuradi.Name = "lbEt_acuradi"
Me.lbEt_acuradi.NTSDbField = ""
Me.lbEt_acuradi.Size = New System.Drawing.Size(88, 13)
Me.lbEt_acuradi.TabIndex = 73
Me.lbEt_acuradi.Text = "Trasp. a cura del"
Me.lbEt_acuradi.Tooltip = ""
Me.lbEt_acuradi.UseMnemonic = false
'
'lbEt_caustra
'
Me.lbEt_caustra.AutoSize = true
Me.lbEt_caustra.BackColor = System.Drawing.Color.Transparent
Me.lbEt_caustra.Location = New System.Drawing.Point(10, 6)
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
Me.edEt_aspetto.Location = New System.Drawing.Point(155, 25)
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
Me.edEt_aspetto.Size = New System.Drawing.Size(265, 20)
Me.edEt_aspetto.TabIndex = 72
'
'lbXx_caustra
'
Me.lbXx_caustra.BackColor = System.Drawing.Color.Transparent
Me.lbXx_caustra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_caustra.Location = New System.Drawing.Point(154, 3)
Me.lbXx_caustra.Name = "lbXx_caustra"
Me.lbXx_caustra.NTSDbField = ""
Me.lbXx_caustra.Size = New System.Drawing.Size(266, 20)
Me.lbXx_caustra.TabIndex = 52
Me.lbXx_caustra.Tooltip = ""
Me.lbXx_caustra.UseMnemonic = false
'
'edEt_pesonetto
'
Me.edEt_pesonetto.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_pesonetto.EditValue = "0"
Me.edEt_pesonetto.Location = New System.Drawing.Point(299, 137)
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
'lbEt_codaspe
'
Me.lbEt_codaspe.AutoSize = true
Me.lbEt_codaspe.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codaspe.Location = New System.Drawing.Point(10, 28)
Me.lbEt_codaspe.Name = "lbEt_codaspe"
Me.lbEt_codaspe.NTSDbField = ""
Me.lbEt_codaspe.Size = New System.Drawing.Size(85, 13)
Me.lbEt_codaspe.TabIndex = 53
Me.lbEt_codaspe.Text = "Aspetto dei beni"
Me.lbEt_codaspe.Tooltip = ""
Me.lbEt_codaspe.UseMnemonic = false
'
'lbPesoNettoLordo
'
Me.lbPesoNettoLordo.AutoSize = true
Me.lbPesoNettoLordo.BackColor = System.Drawing.Color.Transparent
Me.lbPesoNettoLordo.Location = New System.Drawing.Point(212, 140)
Me.lbPesoNettoLordo.Name = "lbPesoNettoLordo"
Me.lbPesoNettoLordo.NTSDbField = ""
Me.lbPesoNettoLordo.Size = New System.Drawing.Size(87, 13)
Me.lbPesoNettoLordo.TabIndex = 70
Me.lbPesoNettoLordo.Text = "Peso netto/lordo"
Me.lbPesoNettoLordo.Tooltip = ""
Me.lbPesoNettoLordo.UseMnemonic = false
'
'edEt_codaspe
'
Me.edEt_codaspe.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codaspe.EditValue = "0"
Me.edEt_codaspe.Location = New System.Drawing.Point(101, 25)
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
'edEt_peso
'
Me.edEt_peso.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_peso.EditValue = "0"
Me.edEt_peso.Location = New System.Drawing.Point(360, 137)
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
'lbEt_coddest2
'
Me.lbEt_coddest2.AutoSize = true
Me.lbEt_coddest2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_coddest2.Location = New System.Drawing.Point(10, 74)
Me.lbEt_coddest2.Name = "lbEt_coddest2"
Me.lbEt_coddest2.NTSDbField = ""
Me.lbEt_coddest2.Size = New System.Drawing.Size(88, 13)
Me.lbEt_coddest2.TabIndex = 56
Me.lbEt_coddest2.Text = "Destin. diversa 2"
Me.lbEt_coddest2.Tooltip = ""
Me.lbEt_coddest2.UseMnemonic = false
'
'edEt_totcoll
'
Me.edEt_totcoll.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_totcoll.EditValue = "0"
Me.edEt_totcoll.Location = New System.Drawing.Point(101, 137)
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
Me.edEt_totcoll.Size = New System.Drawing.Size(52, 20)
Me.edEt_totcoll.TabIndex = 68
'
'edEt_coddest2
'
Me.edEt_coddest2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_coddest2.EditValue = "0"
Me.edEt_coddest2.Location = New System.Drawing.Point(101, 71)
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
'lbEt_totcoll
'
Me.lbEt_totcoll.AutoSize = true
Me.lbEt_totcoll.BackColor = System.Drawing.Color.Transparent
Me.lbEt_totcoll.Location = New System.Drawing.Point(10, 140)
Me.lbEt_totcoll.Name = "lbEt_totcoll"
Me.lbEt_totcoll.NTSDbField = ""
Me.lbEt_totcoll.Size = New System.Drawing.Size(57, 13)
Me.lbEt_totcoll.TabIndex = 67
Me.lbEt_totcoll.Text = "Totale colli"
Me.lbEt_totcoll.Tooltip = ""
Me.lbEt_totcoll.UseMnemonic = false
'
'lbXx_coddest2
'
Me.lbXx_coddest2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_coddest2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_coddest2.Location = New System.Drawing.Point(154, 71)
Me.lbXx_coddest2.Name = "lbXx_coddest2"
Me.lbXx_coddest2.NTSDbField = ""
Me.lbXx_coddest2.Size = New System.Drawing.Size(266, 20)
Me.lbXx_coddest2.TabIndex = 58
Me.lbXx_coddest2.Tooltip = ""
Me.lbXx_coddest2.UseMnemonic = false
'
'lbXx_porto
'
Me.lbXx_porto.BackColor = System.Drawing.Color.Transparent
Me.lbXx_porto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_porto.Location = New System.Drawing.Point(154, 116)
Me.lbXx_porto.Name = "lbXx_porto"
Me.lbXx_porto.NTSDbField = ""
Me.lbXx_porto.Size = New System.Drawing.Size(266, 20)
Me.lbXx_porto.TabIndex = 66
Me.lbXx_porto.Tooltip = ""
Me.lbXx_porto.UseMnemonic = false
'
'lbEt_vettor2
'
Me.lbEt_vettor2.AutoSize = true
Me.lbEt_vettor2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_vettor2.Location = New System.Drawing.Point(10, 97)
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
Me.edEt_porto.Location = New System.Drawing.Point(101, 116)
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
Me.edEt_vettor2.Location = New System.Drawing.Point(101, 94)
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
Me.lbEt_porto.Location = New System.Drawing.Point(10, 119)
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
Me.lbXx_vettor2.Location = New System.Drawing.Point(154, 94)
Me.lbXx_vettor2.Name = "lbXx_vettor2"
Me.lbXx_vettor2.NTSDbField = ""
Me.lbXx_vettor2.Size = New System.Drawing.Size(266, 20)
Me.lbXx_vettor2.TabIndex = 61
Me.lbXx_vettor2.Tooltip = ""
Me.lbXx_vettor2.UseMnemonic = false
'
'cbEt_acuradi
'
Me.cbEt_acuradi.Cursor = System.Windows.Forms.Cursors.Default
Me.cbEt_acuradi.DataSource = Nothing
Me.cbEt_acuradi.DisplayMember = ""
Me.cbEt_acuradi.Location = New System.Drawing.Point(101, 47)
Me.cbEt_acuradi.Name = "cbEt_acuradi"
Me.cbEt_acuradi.NTSDbField = ""
Me.cbEt_acuradi.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.cbEt_acuradi.Properties.Appearance.Options.UseBackColor = true
Me.cbEt_acuradi.Properties.AutoHeight = false
Me.cbEt_acuradi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbEt_acuradi.Properties.DropDownRows = 30
Me.cbEt_acuradi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbEt_acuradi.SelectedValue = ""
Me.cbEt_acuradi.Size = New System.Drawing.Size(319, 20)
Me.cbEt_acuradi.TabIndex = 63
Me.cbEt_acuradi.ValueMember = ""
'
'pnPiedeSx
'
Me.pnPiedeSx.AllowDrop = true
Me.pnPiedeSx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPiedeSx.Appearance.Options.UseBackColor = true
Me.pnPiedeSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPiedeSx.Controls.Add(Me.cmdPiedeAltro)
Me.pnPiedeSx.Controls.Add(Me.cmdPiedeTrasporto)
Me.pnPiedeSx.Controls.Add(Me.cmdPiedeTotali)
Me.pnPiedeSx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnPiedeSx.Dock = System.Windows.Forms.DockStyle.Left
Me.pnPiedeSx.Location = New System.Drawing.Point(0, 0)
Me.pnPiedeSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPiedeSx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPiedeSx.Name = "pnPiedeSx"
Me.pnPiedeSx.NTSActiveTrasparency = true
Me.pnPiedeSx.Size = New System.Drawing.Size(152, 384)
Me.pnPiedeSx.TabIndex = 129
'
'cmdPiedeAltro
'
Me.cmdPiedeAltro.ImagePath = ""
Me.cmdPiedeAltro.ImageText = ""
Me.cmdPiedeAltro.Location = New System.Drawing.Point(9, 77)
Me.cmdPiedeAltro.Name = "cmdPiedeAltro"
Me.cmdPiedeAltro.NTSContextMenu = Nothing
Me.cmdPiedeAltro.Size = New System.Drawing.Size(131, 28)
Me.cmdPiedeAltro.TabIndex = 121
Me.cmdPiedeAltro.Text = "Altri dati"
'
'cmdPiedeTrasporto
'
Me.cmdPiedeTrasporto.ImagePath = ""
Me.cmdPiedeTrasporto.ImageText = ""
Me.cmdPiedeTrasporto.Location = New System.Drawing.Point(9, 43)
Me.cmdPiedeTrasporto.Name = "cmdPiedeTrasporto"
Me.cmdPiedeTrasporto.NTSContextMenu = Nothing
Me.cmdPiedeTrasporto.Size = New System.Drawing.Size(131, 28)
Me.cmdPiedeTrasporto.TabIndex = 120
Me.cmdPiedeTrasporto.Text = "Trasporto"
'
'cmdPiedeTotali
'
Me.cmdPiedeTotali.ImagePath = ""
Me.cmdPiedeTotali.ImageText = ""
Me.cmdPiedeTotali.Location = New System.Drawing.Point(9, 9)
Me.cmdPiedeTotali.Name = "cmdPiedeTotali"
Me.cmdPiedeTotali.NTSContextMenu = Nothing
Me.cmdPiedeTotali.Size = New System.Drawing.Size(131, 28)
Me.cmdPiedeTotali.TabIndex = 118
Me.cmdPiedeTotali.Text = "Totali"
'
'TabPage2
'
Me.TabPage2.AllowDrop = true
Me.TabPage2.Controls.Add(Me.pnGriglia)
Me.TabPage2.Controls.Add(Me.pnTCO)
Me.TabPage2.Controls.Add(Me.pnCorpo)
Me.TabPage2.Enable = true
Me.TabPage2.Name = "TabPage2"
Me.TabPage2.Size = New System.Drawing.Size(722, 384)
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
Me.pnGriglia.Location = New System.Drawing.Point(0, 41)
Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnGriglia.Name = "pnGriglia"
Me.pnGriglia.NTSActiveTrasparency = true
Me.pnGriglia.Size = New System.Drawing.Size(722, 268)
Me.pnGriglia.TabIndex = 2
'
'grRighe
'
Me.grRighe.Dock = System.Windows.Forms.DockStyle.Fill
Me.grRighe.EmbeddedNavigator.Name = ""
Me.grRighe.Location = New System.Drawing.Point(0, 0)
Me.grRighe.MainView = Me.grvRighe
Me.grRighe.Name = "grRighe"
Me.grRighe.Size = New System.Drawing.Size(722, 268)
Me.grRighe.TabIndex = 1
Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
'
'grvRighe
'
Me.grvRighe.ActiveFilterEnabled = false
'
'xxo_prznet
'
Me.xxo_prznet.AppearanceCell.Options.UseBackColor = true
Me.xxo_prznet.AppearanceCell.Options.UseTextOptions = true
Me.xxo_prznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_prznet.Caption = "Prezzo netto"
Me.xxo_prznet.Enabled = false
Me.xxo_prznet.FieldName = "xxo_prznet"
Me.xxo_prznet.Name = "xxo_prznet"
Me.xxo_prznet.NTSRepositoryComboBox = Nothing
Me.xxo_prznet.NTSRepositoryItemCheck = Nothing
Me.xxo_prznet.NTSRepositoryItemMemo = Nothing
Me.xxo_prznet.NTSRepositoryItemText = Nothing
Me.xxo_prznet.OptionsColumn.AllowEdit = false
Me.xxo_prznet.OptionsColumn.ReadOnly = true
Me.xxo_prznet.Visible = true
Me.xxo_prznet.VisibleIndex = 45
Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.ec_riga, Me.ec_matric, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_colpre, Me.ec_coleva, Me.ec_ump, Me.ec_quant, Me.xxo_qtadaass, Me.xxo_flevasass, Me.ec_quapre, Me.ec_quaeva, Me.ec_flevapre, Me.ec_flevas, Me.ec_preziva, Me.ec_prezvalc, Me.ec_prezzo, Me.ec_scont1, Me.ec_scont2, Me.ec_scont3, Me.ec_scont4, Me.ec_scont5, Me.ec_scont6, Me.ec_scontp, Me.ec_magaz, Me.xxo_magaz, Me.ec_datcons, Me.ec_confermato, Me.ec_rilasciato, Me.ec_aperto, Me.ec_ricimp, Me.ec_provv, Me.ec_vprovv, Me.ec_provv2, Me.ec_vprovv2, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_stasino, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.xxo_lottox, Me.ec_codcena, Me.xxo_codcena, Me.ec_note, Me.ec_magaz2, Me.xxo_magaz2, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_datconsor, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.xxo_codarfo, Me.ec_perqta, Me.ec_valoremm, Me.ec_flkit, Me.ec_ktriga, Me.ec_oatipo, Me.ec_oaanno, Me.ec_oaserie, Me.ec_oanum, Me.ec_oariga, Me.ec_oaqtadis, Me.ec_oacoldis, Me.ec_oavaldis, Me.ec_oasalcon, Me.ec_umprz, Me.ec_fase, Me.xxo_fase, Me.ec_codlavo, Me.xxo_codlavo, Me.ec_pmtaskid, Me.xxo_pmtaskid, Me.xxo_pmqtares, Me.xxo_pmqtarim, Me.xxo_pmvalres, Me.xxo_pmvalrim, Me.ec_pmsalcon, Me.xxo_pmqtadisda, Me.xxo_pmvaldisda, Me.xxo_darave, Me.ec_ubicaz, Me.xxo_codtagl, Me.ec_flprznet, Me.ec_oqtipo, Me.ec_oqanno, Me.ec_oqserie, Me.ec_oqnum, Me.ec_oqvers, Me.ec_oqriga, Me.ec_oqsalcon, Me.ec_codtpro, Me.xxo_codtpro, Me.ec_datini, Me.ec_datfin, Me.ec_coddivi, Me.xxo_coddivi, Me.xxo_codalt, Me.ec_flprzmod, Me.ec_flsc1mod, Me.ec_flsc2mod, Me.ec_flsc3mod, Me.ec_flsc4mod, Me.ec_flsc5mod, Me.ec_flsc6mod, Me.ec_flprov1mod, Me.ec_flprov2mod, Me.ec_codrepr1, Me.ec_codrepr2, Me.ec_codrepr3, Me.ec_codrepr4, Me.ec_codrepr5, Me.ec_codrepr6, Me.ec_codrepr1out, Me.ec_codrepr2out, Me.ec_codrepr3out, Me.ec_codrepr4out, Me.ec_codrepr5out, Me.ec_codrepr6out, Me.ec_tiporiga, Me.ec_scpercdiff, Me.ec_prov1percdiff, Me.ec_prov2percdiff, Me.xxo_valoreiva, Me.xxo_percricarico, Me.xxo_percmargine, Me.xxo_marginevalore, Me.xxo_prznet})
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
Me.xx_sel.Width = 32
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
Me.ec_riga.VisibleIndex = 0
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
Me.ec_codart.VisibleIndex = 1
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
Me.ec_descr.VisibleIndex = 2
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
Me.ec_unmis.VisibleIndex = 3
Me.ec_unmis.Width = 39
'
'ec_colli
'
Me.ec_colli.AppearanceCell.Options.UseBackColor = true
Me.ec_colli.AppearanceCell.Options.UseTextOptions = true
Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_colli.Caption = "Colli ord."
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
Me.ec_colli.VisibleIndex = 4
Me.ec_colli.Width = 54
'
'ec_colpre
'
Me.ec_colpre.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
Me.ec_colpre.AppearanceCell.Options.UseBackColor = true
Me.ec_colpre.AppearanceCell.Options.UseTextOptions = true
Me.ec_colpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_colpre.Caption = "Colli pren."
Me.ec_colpre.Enabled = false
Me.ec_colpre.FieldName = "ec_colpre"
Me.ec_colpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_colpre.Name = "ec_colpre"
Me.ec_colpre.NTSRepositoryComboBox = Nothing
Me.ec_colpre.NTSRepositoryItemCheck = Nothing
Me.ec_colpre.NTSRepositoryItemMemo = Nothing
Me.ec_colpre.NTSRepositoryItemText = Nothing
Me.ec_colpre.OptionsColumn.AllowEdit = false
Me.ec_colpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_colpre.OptionsColumn.ReadOnly = true
Me.ec_colpre.OptionsFilter.AllowFilter = false
Me.ec_colpre.Visible = true
Me.ec_colpre.VisibleIndex = 5
Me.ec_colpre.Width = 54
'
'ec_coleva
'
Me.ec_coleva.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
Me.ec_coleva.AppearanceCell.Options.UseBackColor = true
Me.ec_coleva.AppearanceCell.Options.UseTextOptions = true
Me.ec_coleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_coleva.Caption = "Colli evasi"
Me.ec_coleva.Enabled = false
Me.ec_coleva.FieldName = "ec_coleva"
Me.ec_coleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_coleva.Name = "ec_coleva"
Me.ec_coleva.NTSRepositoryComboBox = Nothing
Me.ec_coleva.NTSRepositoryItemCheck = Nothing
Me.ec_coleva.NTSRepositoryItemMemo = Nothing
Me.ec_coleva.NTSRepositoryItemText = Nothing
Me.ec_coleva.OptionsColumn.AllowEdit = false
Me.ec_coleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_coleva.OptionsColumn.ReadOnly = true
Me.ec_coleva.OptionsFilter.AllowFilter = false
Me.ec_coleva.Visible = true
Me.ec_coleva.VisibleIndex = 6
Me.ec_coleva.Width = 51
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
Me.ec_ump.VisibleIndex = 7
'
'ec_quant
'
Me.ec_quant.AppearanceCell.Options.UseBackColor = true
Me.ec_quant.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant.Caption = "Q.tà ordin."
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
Me.ec_quant.VisibleIndex = 8
'
'xxo_qtadaass
'
Me.xxo_qtadaass.AppearanceCell.Options.UseBackColor = true
Me.xxo_qtadaass.AppearanceCell.Options.UseTextOptions = true
Me.xxo_qtadaass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_qtadaass.Caption = "Q.tà da assegnare"
Me.xxo_qtadaass.Enabled = true
Me.xxo_qtadaass.FieldName = "xxo_qtadaass"
Me.xxo_qtadaass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_qtadaass.Name = "xxo_qtadaass"
Me.xxo_qtadaass.NTSRepositoryComboBox = Nothing
Me.xxo_qtadaass.NTSRepositoryItemCheck = Nothing
Me.xxo_qtadaass.NTSRepositoryItemMemo = Nothing
Me.xxo_qtadaass.NTSRepositoryItemText = Nothing
Me.xxo_qtadaass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_qtadaass.OptionsFilter.AllowFilter = false
'
'xxo_flevasass
'
Me.xxo_flevasass.AppearanceCell.Options.UseBackColor = true
Me.xxo_flevasass.AppearanceCell.Options.UseTextOptions = true
Me.xxo_flevasass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_flevasass.Caption = "Ass. totale"
Me.xxo_flevasass.Enabled = true
Me.xxo_flevasass.FieldName = "xxo_flevasass"
Me.xxo_flevasass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_flevasass.Name = "xxo_flevasass"
Me.xxo_flevasass.NTSRepositoryComboBox = Nothing
Me.xxo_flevasass.NTSRepositoryItemCheck = Nothing
Me.xxo_flevasass.NTSRepositoryItemMemo = Nothing
Me.xxo_flevasass.NTSRepositoryItemText = Nothing
Me.xxo_flevasass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_flevasass.OptionsFilter.AllowFilter = false
'
'ec_quapre
'
Me.ec_quapre.AppearanceCell.Options.UseBackColor = true
Me.ec_quapre.AppearanceCell.Options.UseTextOptions = true
Me.ec_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quapre.Caption = "Q.tà pren."
Me.ec_quapre.Enabled = false
Me.ec_quapre.FieldName = "ec_quapre"
Me.ec_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quapre.Name = "ec_quapre"
Me.ec_quapre.NTSRepositoryComboBox = Nothing
Me.ec_quapre.NTSRepositoryItemCheck = Nothing
Me.ec_quapre.NTSRepositoryItemMemo = Nothing
Me.ec_quapre.NTSRepositoryItemText = Nothing
Me.ec_quapre.OptionsColumn.AllowEdit = false
Me.ec_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quapre.OptionsColumn.ReadOnly = true
Me.ec_quapre.OptionsFilter.AllowFilter = false
Me.ec_quapre.Visible = true
Me.ec_quapre.VisibleIndex = 9
'
'ec_quaeva
'
Me.ec_quaeva.AppearanceCell.Options.UseBackColor = true
Me.ec_quaeva.AppearanceCell.Options.UseTextOptions = true
Me.ec_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quaeva.Caption = "Q.tà evasa"
Me.ec_quaeva.Enabled = false
Me.ec_quaeva.FieldName = "ec_quaeva"
Me.ec_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quaeva.Name = "ec_quaeva"
Me.ec_quaeva.NTSRepositoryComboBox = Nothing
Me.ec_quaeva.NTSRepositoryItemCheck = Nothing
Me.ec_quaeva.NTSRepositoryItemMemo = Nothing
Me.ec_quaeva.NTSRepositoryItemText = Nothing
Me.ec_quaeva.OptionsColumn.AllowEdit = false
Me.ec_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quaeva.OptionsColumn.ReadOnly = true
Me.ec_quaeva.OptionsFilter.AllowFilter = false
Me.ec_quaeva.Visible = true
Me.ec_quaeva.VisibleIndex = 10
'
'ec_flevapre
'
Me.ec_flevapre.AppearanceCell.Options.UseBackColor = true
Me.ec_flevapre.AppearanceCell.Options.UseTextOptions = true
Me.ec_flevapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flevapre.Caption = "Pr. totale"
Me.ec_flevapre.Enabled = true
Me.ec_flevapre.FieldName = "ec_flevapre"
Me.ec_flevapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flevapre.Name = "ec_flevapre"
Me.ec_flevapre.NTSRepositoryComboBox = Nothing
Me.ec_flevapre.NTSRepositoryItemCheck = Nothing
Me.ec_flevapre.NTSRepositoryItemMemo = Nothing
Me.ec_flevapre.NTSRepositoryItemText = Nothing
Me.ec_flevapre.OptionsColumn.AllowEdit = false
Me.ec_flevapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flevapre.OptionsFilter.AllowFilter = false
Me.ec_flevapre.Visible = true
Me.ec_flevapre.VisibleIndex = 11
'
'ec_flevas
'
Me.ec_flevas.AppearanceCell.Options.UseBackColor = true
Me.ec_flevas.AppearanceCell.Options.UseTextOptions = true
Me.ec_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_flevas.Caption = "Evas. totale"
Me.ec_flevas.Enabled = true
Me.ec_flevas.FieldName = "ec_flevas"
Me.ec_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_flevas.Name = "ec_flevas"
Me.ec_flevas.NTSRepositoryComboBox = Nothing
Me.ec_flevas.NTSRepositoryItemCheck = Nothing
Me.ec_flevas.NTSRepositoryItemMemo = Nothing
Me.ec_flevas.NTSRepositoryItemText = Nothing
Me.ec_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_flevas.OptionsFilter.AllowFilter = false
Me.ec_flevas.Visible = true
Me.ec_flevas.VisibleIndex = 12
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
Me.ec_prezzo.VisibleIndex = 13
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
Me.ec_scont1.VisibleIndex = 14
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
Me.ec_scont2.VisibleIndex = 15
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
'ec_scontp
'
Me.ec_scontp.AppearanceCell.Options.UseBackColor = true
Me.ec_scontp.AppearanceCell.Options.UseTextOptions = true
Me.ec_scontp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scontp.Caption = "Sconto Promozione"
Me.ec_scontp.Enabled = false
Me.ec_scontp.FieldName = "ec_scontp"
Me.ec_scontp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scontp.Name = "ec_scontp"
Me.ec_scontp.NTSRepositoryComboBox = Nothing
Me.ec_scontp.NTSRepositoryItemCheck = Nothing
Me.ec_scontp.NTSRepositoryItemMemo = Nothing
Me.ec_scontp.NTSRepositoryItemText = Nothing
Me.ec_scontp.OptionsColumn.AllowEdit = false
Me.ec_scontp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scontp.OptionsColumn.ReadOnly = true
Me.ec_scontp.OptionsFilter.AllowFilter = false
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
Me.ec_magaz.Visible = true
Me.ec_magaz.VisibleIndex = 16
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
Me.xxo_magaz.Visible = true
Me.xxo_magaz.VisibleIndex = 17
'
'ec_datcons
'
Me.ec_datcons.AppearanceCell.Options.UseBackColor = true
Me.ec_datcons.AppearanceCell.Options.UseTextOptions = true
Me.ec_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_datcons.Caption = "Data cons."
Me.ec_datcons.Enabled = true
Me.ec_datcons.FieldName = "ec_datcons"
Me.ec_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_datcons.Name = "ec_datcons"
Me.ec_datcons.NTSRepositoryComboBox = Nothing
Me.ec_datcons.NTSRepositoryItemCheck = Nothing
Me.ec_datcons.NTSRepositoryItemMemo = Nothing
Me.ec_datcons.NTSRepositoryItemText = Nothing
Me.ec_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_datcons.OptionsFilter.AllowFilter = false
Me.ec_datcons.Visible = true
Me.ec_datcons.VisibleIndex = 18
'
'ec_confermato
'
Me.ec_confermato.AppearanceCell.Options.UseBackColor = true
Me.ec_confermato.AppearanceCell.Options.UseTextOptions = true
Me.ec_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_confermato.Caption = "Confermato"
Me.ec_confermato.Enabled = true
Me.ec_confermato.FieldName = "ec_confermato"
Me.ec_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_confermato.Name = "ec_confermato"
Me.ec_confermato.NTSRepositoryComboBox = Nothing
Me.ec_confermato.NTSRepositoryItemCheck = Nothing
Me.ec_confermato.NTSRepositoryItemMemo = Nothing
Me.ec_confermato.NTSRepositoryItemText = Nothing
Me.ec_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_confermato.OptionsFilter.AllowFilter = false
Me.ec_confermato.Visible = true
Me.ec_confermato.VisibleIndex = 19
'
'ec_rilasciato
'
Me.ec_rilasciato.AppearanceCell.Options.UseBackColor = true
Me.ec_rilasciato.AppearanceCell.Options.UseTextOptions = true
Me.ec_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_rilasciato.Caption = "Rilasciato"
Me.ec_rilasciato.Enabled = true
Me.ec_rilasciato.FieldName = "ec_rilasciato"
Me.ec_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_rilasciato.Name = "ec_rilasciato"
Me.ec_rilasciato.NTSRepositoryComboBox = Nothing
Me.ec_rilasciato.NTSRepositoryItemCheck = Nothing
Me.ec_rilasciato.NTSRepositoryItemMemo = Nothing
Me.ec_rilasciato.NTSRepositoryItemText = Nothing
Me.ec_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_rilasciato.OptionsFilter.AllowFilter = false
Me.ec_rilasciato.Visible = true
Me.ec_rilasciato.VisibleIndex = 20
'
'ec_aperto
'
Me.ec_aperto.AppearanceCell.Options.UseBackColor = true
Me.ec_aperto.AppearanceCell.Options.UseTextOptions = true
Me.ec_aperto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_aperto.Caption = "Aperto"
Me.ec_aperto.Enabled = true
Me.ec_aperto.FieldName = "ec_aperto"
Me.ec_aperto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_aperto.Name = "ec_aperto"
Me.ec_aperto.NTSRepositoryComboBox = Nothing
Me.ec_aperto.NTSRepositoryItemCheck = Nothing
Me.ec_aperto.NTSRepositoryItemMemo = Nothing
Me.ec_aperto.NTSRepositoryItemText = Nothing
Me.ec_aperto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_aperto.OptionsFilter.AllowFilter = false
Me.ec_aperto.Visible = true
Me.ec_aperto.VisibleIndex = 21
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
Me.ec_provv.VisibleIndex = 22
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
Me.ec_provv2.Visible = true
Me.ec_provv2.VisibleIndex = 23
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
Me.ec_controp.VisibleIndex = 24
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
Me.xxo_controp.VisibleIndex = 25
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
Me.ec_codiva.VisibleIndex = 26
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
Me.xxo_codiva.VisibleIndex = 27
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
Me.ec_stasino.VisibleIndex = 28
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
Me.ec_codcfam.Visible = true
Me.ec_codcfam.VisibleIndex = 29
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
Me.xxo_codcfam.Visible = true
Me.xxo_codcfam.VisibleIndex = 30
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
Me.ec_commeca.Visible = true
Me.ec_commeca.VisibleIndex = 31
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
Me.xxo_commeca.Visible = true
Me.xxo_commeca.VisibleIndex = 32
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
Me.ec_subcommeca.Visible = true
Me.ec_subcommeca.VisibleIndex = 33
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
Me.xxo_lottox.Visible = true
Me.xxo_lottox.VisibleIndex = 34
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
Me.ec_codcena.Visible = true
Me.ec_codcena.VisibleIndex = 35
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
Me.xxo_codcena.Visible = true
Me.xxo_codcena.VisibleIndex = 36
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
Me.ec_note.VisibleIndex = 37
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
Me.ec_valore.Caption = "Valore da evadere"
Me.ec_valore.Enabled = false
Me.ec_valore.FieldName = "ec_valore"
Me.ec_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_valore.Name = "ec_valore"
Me.ec_valore.NTSRepositoryComboBox = Nothing
Me.ec_valore.NTSRepositoryItemCheck = Nothing
Me.ec_valore.NTSRepositoryItemMemo = Nothing
Me.ec_valore.NTSRepositoryItemText = Nothing
Me.ec_valore.OptionsColumn.AllowEdit = false
Me.ec_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_valore.OptionsColumn.ReadOnly = true
Me.ec_valore.OptionsFilter.AllowFilter = false
Me.ec_valore.Visible = true
Me.ec_valore.VisibleIndex = 38
'
'ec_contocontr
'
Me.ec_contocontr.AppearanceCell.Options.UseBackColor = true
Me.ec_contocontr.AppearanceCell.Options.UseTextOptions = true
Me.ec_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_contocontr.Caption = "Conto controp."
Me.ec_contocontr.Enabled = false
Me.ec_contocontr.FieldName = "ec_contocontr"
Me.ec_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_contocontr.Name = "ec_contocontr"
Me.ec_contocontr.NTSRepositoryComboBox = Nothing
Me.ec_contocontr.NTSRepositoryItemCheck = Nothing
Me.ec_contocontr.NTSRepositoryItemMemo = Nothing
Me.ec_contocontr.NTSRepositoryItemText = Nothing
Me.ec_contocontr.OptionsColumn.AllowEdit = false
Me.ec_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_contocontr.OptionsColumn.ReadOnly = true
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
'ec_datconsor
'
Me.ec_datconsor.AppearanceCell.Options.UseBackColor = true
Me.ec_datconsor.AppearanceCell.Options.UseTextOptions = true
Me.ec_datconsor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_datconsor.Caption = "Dt. consegna originaria"
Me.ec_datconsor.Enabled = true
Me.ec_datconsor.FieldName = "ec_datconsor"
Me.ec_datconsor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_datconsor.Name = "ec_datconsor"
Me.ec_datconsor.NTSRepositoryComboBox = Nothing
Me.ec_datconsor.NTSRepositoryItemCheck = Nothing
Me.ec_datconsor.NTSRepositoryItemMemo = Nothing
Me.ec_datconsor.NTSRepositoryItemText = Nothing
Me.ec_datconsor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_datconsor.OptionsFilter.AllowFilter = false
Me.ec_datconsor.Visible = true
Me.ec_datconsor.VisibleIndex = 39
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
Me.ec_perqta.Visible = true
Me.ec_perqta.VisibleIndex = 40
'
'ec_valoremm
'
Me.ec_valoremm.AppearanceCell.Options.UseBackColor = true
Me.ec_valoremm.AppearanceCell.Options.UseTextOptions = true
Me.ec_valoremm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_valoremm.Caption = "Valore riga totale"
Me.ec_valoremm.Enabled = false
Me.ec_valoremm.FieldName = "ec_valoremm"
Me.ec_valoremm.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_valoremm.Name = "ec_valoremm"
Me.ec_valoremm.NTSRepositoryComboBox = Nothing
Me.ec_valoremm.NTSRepositoryItemCheck = Nothing
Me.ec_valoremm.NTSRepositoryItemMemo = Nothing
Me.ec_valoremm.NTSRepositoryItemText = Nothing
Me.ec_valoremm.OptionsColumn.AllowEdit = false
Me.ec_valoremm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_valoremm.OptionsColumn.ReadOnly = true
Me.ec_valoremm.OptionsFilter.AllowFilter = false
Me.ec_valoremm.Visible = true
Me.ec_valoremm.VisibleIndex = 41
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
Me.ec_flkit.VisibleIndex = 42
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
Me.ec_ktriga.Visible = true
Me.ec_ktriga.VisibleIndex = 43
'
'ec_oatipo
'
Me.ec_oatipo.AppearanceCell.Options.UseBackColor = true
Me.ec_oatipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_oatipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oatipo.Caption = "Tipo ordine ap."
Me.ec_oatipo.Enabled = false
Me.ec_oatipo.FieldName = "ec_oatipo"
Me.ec_oatipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oatipo.Name = "ec_oatipo"
Me.ec_oatipo.NTSRepositoryComboBox = Nothing
Me.ec_oatipo.NTSRepositoryItemCheck = Nothing
Me.ec_oatipo.NTSRepositoryItemMemo = Nothing
Me.ec_oatipo.NTSRepositoryItemText = Nothing
Me.ec_oatipo.OptionsColumn.AllowEdit = false
Me.ec_oatipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oatipo.OptionsColumn.ReadOnly = true
Me.ec_oatipo.OptionsFilter.AllowFilter = false
'
'ec_oaanno
'
Me.ec_oaanno.AppearanceCell.Options.UseBackColor = true
Me.ec_oaanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_oaanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oaanno.Caption = "Anno ordine ap."
Me.ec_oaanno.Enabled = false
Me.ec_oaanno.FieldName = "ec_oaanno"
Me.ec_oaanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oaanno.Name = "ec_oaanno"
Me.ec_oaanno.NTSRepositoryComboBox = Nothing
Me.ec_oaanno.NTSRepositoryItemCheck = Nothing
Me.ec_oaanno.NTSRepositoryItemMemo = Nothing
Me.ec_oaanno.NTSRepositoryItemText = Nothing
Me.ec_oaanno.OptionsColumn.AllowEdit = false
Me.ec_oaanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oaanno.OptionsColumn.ReadOnly = true
Me.ec_oaanno.OptionsFilter.AllowFilter = false
'
'ec_oaserie
'
Me.ec_oaserie.AppearanceCell.Options.UseBackColor = true
Me.ec_oaserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_oaserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oaserie.Caption = "Serie ordine ap."
Me.ec_oaserie.Enabled = false
Me.ec_oaserie.FieldName = "ec_oaserie"
Me.ec_oaserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oaserie.Name = "ec_oaserie"
Me.ec_oaserie.NTSRepositoryComboBox = Nothing
Me.ec_oaserie.NTSRepositoryItemCheck = Nothing
Me.ec_oaserie.NTSRepositoryItemMemo = Nothing
Me.ec_oaserie.NTSRepositoryItemText = Nothing
Me.ec_oaserie.OptionsColumn.AllowEdit = false
Me.ec_oaserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oaserie.OptionsColumn.ReadOnly = true
Me.ec_oaserie.OptionsFilter.AllowFilter = false
'
'ec_oanum
'
Me.ec_oanum.AppearanceCell.Options.UseBackColor = true
Me.ec_oanum.AppearanceCell.Options.UseTextOptions = true
Me.ec_oanum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oanum.Caption = "Numero ordine ap."
Me.ec_oanum.Enabled = false
Me.ec_oanum.FieldName = "ec_oanum"
Me.ec_oanum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oanum.Name = "ec_oanum"
Me.ec_oanum.NTSRepositoryComboBox = Nothing
Me.ec_oanum.NTSRepositoryItemCheck = Nothing
Me.ec_oanum.NTSRepositoryItemMemo = Nothing
Me.ec_oanum.NTSRepositoryItemText = Nothing
Me.ec_oanum.OptionsColumn.AllowEdit = false
Me.ec_oanum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oanum.OptionsColumn.ReadOnly = true
Me.ec_oanum.OptionsFilter.AllowFilter = false
Me.ec_oanum.Visible = true
Me.ec_oanum.VisibleIndex = 44
'
'ec_oariga
'
Me.ec_oariga.AppearanceCell.Options.UseBackColor = true
Me.ec_oariga.AppearanceCell.Options.UseTextOptions = true
Me.ec_oariga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oariga.Caption = "Riga ordine ap."
Me.ec_oariga.Enabled = false
Me.ec_oariga.FieldName = "ec_oariga"
Me.ec_oariga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oariga.Name = "ec_oariga"
Me.ec_oariga.NTSRepositoryComboBox = Nothing
Me.ec_oariga.NTSRepositoryItemCheck = Nothing
Me.ec_oariga.NTSRepositoryItemMemo = Nothing
Me.ec_oariga.NTSRepositoryItemText = Nothing
Me.ec_oariga.OptionsColumn.AllowEdit = false
Me.ec_oariga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oariga.OptionsColumn.ReadOnly = true
Me.ec_oariga.OptionsFilter.AllowFilter = false
'
'ec_oaqtadis
'
Me.ec_oaqtadis.AppearanceCell.Options.UseBackColor = true
Me.ec_oaqtadis.AppearanceCell.Options.UseTextOptions = true
Me.ec_oaqtadis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oaqtadis.Caption = "Qta disimp. ord. ap."
Me.ec_oaqtadis.Enabled = false
Me.ec_oaqtadis.FieldName = "ec_oaqtadis"
Me.ec_oaqtadis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oaqtadis.Name = "ec_oaqtadis"
Me.ec_oaqtadis.NTSRepositoryComboBox = Nothing
Me.ec_oaqtadis.NTSRepositoryItemCheck = Nothing
Me.ec_oaqtadis.NTSRepositoryItemMemo = Nothing
Me.ec_oaqtadis.NTSRepositoryItemText = Nothing
Me.ec_oaqtadis.OptionsColumn.AllowEdit = false
Me.ec_oaqtadis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oaqtadis.OptionsColumn.ReadOnly = true
Me.ec_oaqtadis.OptionsFilter.AllowFilter = false
'
'ec_oacoldis
'
Me.ec_oacoldis.AppearanceCell.Options.UseBackColor = true
Me.ec_oacoldis.AppearanceCell.Options.UseTextOptions = true
Me.ec_oacoldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oacoldis.Caption = "Colli disimp. ord. ap."
Me.ec_oacoldis.Enabled = false
Me.ec_oacoldis.FieldName = "ec_oacoldis"
Me.ec_oacoldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oacoldis.Name = "ec_oacoldis"
Me.ec_oacoldis.NTSRepositoryComboBox = Nothing
Me.ec_oacoldis.NTSRepositoryItemCheck = Nothing
Me.ec_oacoldis.NTSRepositoryItemMemo = Nothing
Me.ec_oacoldis.NTSRepositoryItemText = Nothing
Me.ec_oacoldis.OptionsColumn.AllowEdit = false
Me.ec_oacoldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oacoldis.OptionsColumn.ReadOnly = true
Me.ec_oacoldis.OptionsFilter.AllowFilter = false
'
'ec_oavaldis
'
Me.ec_oavaldis.AppearanceCell.Options.UseBackColor = true
Me.ec_oavaldis.AppearanceCell.Options.UseTextOptions = true
Me.ec_oavaldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oavaldis.Caption = "Valore disimp. ord. ap."
Me.ec_oavaldis.Enabled = false
Me.ec_oavaldis.FieldName = "ec_oavaldis"
Me.ec_oavaldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oavaldis.Name = "ec_oavaldis"
Me.ec_oavaldis.NTSRepositoryComboBox = Nothing
Me.ec_oavaldis.NTSRepositoryItemCheck = Nothing
Me.ec_oavaldis.NTSRepositoryItemMemo = Nothing
Me.ec_oavaldis.NTSRepositoryItemText = Nothing
Me.ec_oavaldis.OptionsColumn.AllowEdit = false
Me.ec_oavaldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oavaldis.OptionsColumn.ReadOnly = true
Me.ec_oavaldis.OptionsFilter.AllowFilter = false
'
'ec_oasalcon
'
Me.ec_oasalcon.AppearanceCell.Options.UseBackColor = true
Me.ec_oasalcon.AppearanceCell.Options.UseTextOptions = true
Me.ec_oasalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oasalcon.Caption = "Saldo ordine ap."
Me.ec_oasalcon.Enabled = true
Me.ec_oasalcon.FieldName = "ec_oasalcon"
Me.ec_oasalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oasalcon.Name = "ec_oasalcon"
Me.ec_oasalcon.NTSRepositoryComboBox = Nothing
Me.ec_oasalcon.NTSRepositoryItemCheck = Nothing
Me.ec_oasalcon.NTSRepositoryItemMemo = Nothing
Me.ec_oasalcon.NTSRepositoryItemText = Nothing
Me.ec_oasalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oasalcon.OptionsFilter.AllowFilter = false
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
'ec_codlavo
'
Me.ec_codlavo.AppearanceCell.Options.UseBackColor = true
Me.ec_codlavo.AppearanceCell.Options.UseTextOptions = true
Me.ec_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codlavo.Caption = "Codice lavoraz."
Me.ec_codlavo.Enabled = false
Me.ec_codlavo.FieldName = "ec_codlavo"
Me.ec_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codlavo.Name = "ec_codlavo"
Me.ec_codlavo.NTSRepositoryComboBox = Nothing
Me.ec_codlavo.NTSRepositoryItemCheck = Nothing
Me.ec_codlavo.NTSRepositoryItemMemo = Nothing
Me.ec_codlavo.NTSRepositoryItemText = Nothing
Me.ec_codlavo.OptionsColumn.AllowEdit = false
Me.ec_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codlavo.OptionsColumn.ReadOnly = true
Me.ec_codlavo.OptionsFilter.AllowFilter = false
'
'xxo_codlavo
'
Me.xxo_codlavo.AppearanceCell.Options.UseBackColor = true
Me.xxo_codlavo.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codlavo.Caption = "Descr. lavoraz."
Me.xxo_codlavo.Enabled = false
Me.xxo_codlavo.FieldName = "xxo_codlavo"
Me.xxo_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xxo_codlavo.Name = "xxo_codlavo"
Me.xxo_codlavo.NTSRepositoryComboBox = Nothing
Me.xxo_codlavo.NTSRepositoryItemCheck = Nothing
Me.xxo_codlavo.NTSRepositoryItemMemo = Nothing
Me.xxo_codlavo.NTSRepositoryItemText = Nothing
Me.xxo_codlavo.OptionsColumn.AllowEdit = false
Me.xxo_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xxo_codlavo.OptionsColumn.ReadOnly = true
Me.xxo_codlavo.OptionsFilter.AllowFilter = false
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
'ec_oqtipo
'
Me.ec_oqtipo.AppearanceCell.Options.UseBackColor = true
Me.ec_oqtipo.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqtipo.Caption = "Tipo offerta"
Me.ec_oqtipo.Enabled = false
Me.ec_oqtipo.FieldName = "ec_oqtipo"
Me.ec_oqtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqtipo.Name = "ec_oqtipo"
Me.ec_oqtipo.NTSRepositoryComboBox = Nothing
Me.ec_oqtipo.NTSRepositoryItemCheck = Nothing
Me.ec_oqtipo.NTSRepositoryItemMemo = Nothing
Me.ec_oqtipo.NTSRepositoryItemText = Nothing
Me.ec_oqtipo.OptionsColumn.AllowEdit = false
Me.ec_oqtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqtipo.OptionsColumn.ReadOnly = true
Me.ec_oqtipo.OptionsFilter.AllowFilter = false
'
'ec_oqanno
'
Me.ec_oqanno.AppearanceCell.Options.UseBackColor = true
Me.ec_oqanno.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqanno.Caption = "Anno offerta"
Me.ec_oqanno.Enabled = false
Me.ec_oqanno.FieldName = "ec_oqanno"
Me.ec_oqanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqanno.Name = "ec_oqanno"
Me.ec_oqanno.NTSRepositoryComboBox = Nothing
Me.ec_oqanno.NTSRepositoryItemCheck = Nothing
Me.ec_oqanno.NTSRepositoryItemMemo = Nothing
Me.ec_oqanno.NTSRepositoryItemText = Nothing
Me.ec_oqanno.OptionsColumn.AllowEdit = false
Me.ec_oqanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqanno.OptionsColumn.ReadOnly = true
Me.ec_oqanno.OptionsFilter.AllowFilter = false
'
'ec_oqserie
'
Me.ec_oqserie.AppearanceCell.Options.UseBackColor = true
Me.ec_oqserie.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqserie.Caption = "Serie offerta"
Me.ec_oqserie.Enabled = false
Me.ec_oqserie.FieldName = "ec_oqserie"
Me.ec_oqserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqserie.Name = "ec_oqserie"
Me.ec_oqserie.NTSRepositoryComboBox = Nothing
Me.ec_oqserie.NTSRepositoryItemCheck = Nothing
Me.ec_oqserie.NTSRepositoryItemMemo = Nothing
Me.ec_oqserie.NTSRepositoryItemText = Nothing
Me.ec_oqserie.OptionsColumn.AllowEdit = false
Me.ec_oqserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqserie.OptionsColumn.ReadOnly = true
Me.ec_oqserie.OptionsFilter.AllowFilter = false
'
'ec_oqnum
'
Me.ec_oqnum.AppearanceCell.Options.UseBackColor = true
Me.ec_oqnum.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqnum.Caption = "Num. offerta"
Me.ec_oqnum.Enabled = false
Me.ec_oqnum.FieldName = "ec_oqnum"
Me.ec_oqnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqnum.Name = "ec_oqnum"
Me.ec_oqnum.NTSRepositoryComboBox = Nothing
Me.ec_oqnum.NTSRepositoryItemCheck = Nothing
Me.ec_oqnum.NTSRepositoryItemMemo = Nothing
Me.ec_oqnum.NTSRepositoryItemText = Nothing
Me.ec_oqnum.OptionsColumn.AllowEdit = false
Me.ec_oqnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqnum.OptionsColumn.ReadOnly = true
Me.ec_oqnum.OptionsFilter.AllowFilter = false
'
'ec_oqvers
'
Me.ec_oqvers.AppearanceCell.Options.UseBackColor = true
Me.ec_oqvers.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqvers.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqvers.Caption = "Vers. offerta"
Me.ec_oqvers.Enabled = false
Me.ec_oqvers.FieldName = "ec_oqvers"
Me.ec_oqvers.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqvers.Name = "ec_oqvers"
Me.ec_oqvers.NTSRepositoryComboBox = Nothing
Me.ec_oqvers.NTSRepositoryItemCheck = Nothing
Me.ec_oqvers.NTSRepositoryItemMemo = Nothing
Me.ec_oqvers.NTSRepositoryItemText = Nothing
Me.ec_oqvers.OptionsColumn.AllowEdit = false
Me.ec_oqvers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqvers.OptionsColumn.ReadOnly = true
Me.ec_oqvers.OptionsFilter.AllowFilter = false
'
'ec_oqriga
'
Me.ec_oqriga.AppearanceCell.Options.UseBackColor = true
Me.ec_oqriga.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqriga.Caption = "Riga offerta"
Me.ec_oqriga.Enabled = false
Me.ec_oqriga.FieldName = "ec_oqriga"
Me.ec_oqriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqriga.Name = "ec_oqriga"
Me.ec_oqriga.NTSRepositoryComboBox = Nothing
Me.ec_oqriga.NTSRepositoryItemCheck = Nothing
Me.ec_oqriga.NTSRepositoryItemMemo = Nothing
Me.ec_oqriga.NTSRepositoryItemText = Nothing
Me.ec_oqriga.OptionsColumn.AllowEdit = false
Me.ec_oqriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqriga.OptionsColumn.ReadOnly = true
Me.ec_oqriga.OptionsFilter.AllowFilter = false
'
'ec_oqsalcon
'
Me.ec_oqsalcon.AppearanceCell.Options.UseBackColor = true
Me.ec_oqsalcon.AppearanceCell.Options.UseTextOptions = true
Me.ec_oqsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_oqsalcon.Caption = "Riga off. evasa"
Me.ec_oqsalcon.Enabled = false
Me.ec_oqsalcon.FieldName = "ec_oqsalcon"
Me.ec_oqsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_oqsalcon.Name = "ec_oqsalcon"
Me.ec_oqsalcon.NTSRepositoryComboBox = Nothing
Me.ec_oqsalcon.NTSRepositoryItemCheck = Nothing
Me.ec_oqsalcon.NTSRepositoryItemMemo = Nothing
Me.ec_oqsalcon.NTSRepositoryItemText = Nothing
Me.ec_oqsalcon.OptionsColumn.AllowEdit = false
Me.ec_oqsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_oqsalcon.OptionsColumn.ReadOnly = true
Me.ec_oqsalcon.OptionsFilter.AllowFilter = false
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
Me.xxo_coddivi.Caption = "Descr. Divisione CA"
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
'xxo_codalt
'
Me.xxo_codalt.AppearanceCell.Options.UseBackColor = true
Me.xxo_codalt.AppearanceCell.Options.UseTextOptions = true
Me.xxo_codalt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xxo_codalt.Caption = "Cod. altern. articolo"
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
'ec_codrepr1
'
Me.ec_codrepr1.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr1.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr1.Caption = "Codice promozione 1"
Me.ec_codrepr1.Enabled = false
Me.ec_codrepr1.FieldName = "ec_codrepr1"
Me.ec_codrepr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr1.Name = "ec_codrepr1"
Me.ec_codrepr1.NTSRepositoryComboBox = Nothing
Me.ec_codrepr1.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr1.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr1.NTSRepositoryItemText = Nothing
Me.ec_codrepr1.OptionsColumn.AllowEdit = false
Me.ec_codrepr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr1.OptionsColumn.ReadOnly = true
Me.ec_codrepr1.OptionsFilter.AllowFilter = false
'
'ec_codrepr2
'
Me.ec_codrepr2.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr2.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr2.Caption = "Codice promozione 2"
Me.ec_codrepr2.Enabled = false
Me.ec_codrepr2.FieldName = "ec_codrepr2"
Me.ec_codrepr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr2.Name = "ec_codrepr2"
Me.ec_codrepr2.NTSRepositoryComboBox = Nothing
Me.ec_codrepr2.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr2.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr2.NTSRepositoryItemText = Nothing
Me.ec_codrepr2.OptionsColumn.AllowEdit = false
Me.ec_codrepr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr2.OptionsColumn.ReadOnly = true
Me.ec_codrepr2.OptionsFilter.AllowFilter = false
'
'ec_codrepr3
'
Me.ec_codrepr3.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr3.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr3.Caption = "Codice promozione 3"
Me.ec_codrepr3.Enabled = false
Me.ec_codrepr3.FieldName = "ec_codrepr3"
Me.ec_codrepr3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr3.Name = "ec_codrepr3"
Me.ec_codrepr3.NTSRepositoryComboBox = Nothing
Me.ec_codrepr3.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr3.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr3.NTSRepositoryItemText = Nothing
Me.ec_codrepr3.OptionsColumn.AllowEdit = false
Me.ec_codrepr3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr3.OptionsColumn.ReadOnly = true
Me.ec_codrepr3.OptionsFilter.AllowFilter = false
'
'ec_codrepr4
'
Me.ec_codrepr4.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr4.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr4.Caption = "Codice promozione 4"
Me.ec_codrepr4.Enabled = false
Me.ec_codrepr4.FieldName = "ec_codrepr4"
Me.ec_codrepr4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr4.Name = "ec_codrepr4"
Me.ec_codrepr4.NTSRepositoryComboBox = Nothing
Me.ec_codrepr4.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr4.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr4.NTSRepositoryItemText = Nothing
Me.ec_codrepr4.OptionsColumn.AllowEdit = false
Me.ec_codrepr4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr4.OptionsColumn.ReadOnly = true
Me.ec_codrepr4.OptionsFilter.AllowFilter = false
'
'ec_codrepr5
'
Me.ec_codrepr5.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr5.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr5.Caption = "Codice promozione 5"
Me.ec_codrepr5.Enabled = false
Me.ec_codrepr5.FieldName = "ec_codrepr5"
Me.ec_codrepr5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr5.Name = "ec_codrepr5"
Me.ec_codrepr5.NTSRepositoryComboBox = Nothing
Me.ec_codrepr5.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr5.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr5.NTSRepositoryItemText = Nothing
Me.ec_codrepr5.OptionsColumn.AllowEdit = false
Me.ec_codrepr5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr5.OptionsColumn.ReadOnly = true
Me.ec_codrepr5.OptionsFilter.AllowFilter = false
'
'ec_codrepr6
'
Me.ec_codrepr6.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr6.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr6.Caption = "Codice promozione 6"
Me.ec_codrepr6.Enabled = false
Me.ec_codrepr6.FieldName = "ec_codrepr6"
Me.ec_codrepr6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr6.Name = "ec_codrepr6"
Me.ec_codrepr6.NTSRepositoryComboBox = Nothing
Me.ec_codrepr6.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr6.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr6.NTSRepositoryItemText = Nothing
Me.ec_codrepr6.OptionsColumn.AllowEdit = false
Me.ec_codrepr6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr6.OptionsColumn.ReadOnly = true
Me.ec_codrepr6.OptionsFilter.AllowFilter = false
'
'ec_codrepr1out
'
Me.ec_codrepr1out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr1out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr1out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr1out.Caption = "1° promozione applicata"
Me.ec_codrepr1out.Enabled = false
Me.ec_codrepr1out.FieldName = "ec_codrepr1out"
Me.ec_codrepr1out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr1out.Name = "ec_codrepr1out"
Me.ec_codrepr1out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr1out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr1out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr1out.NTSRepositoryItemText = Nothing
Me.ec_codrepr1out.OptionsColumn.AllowEdit = false
Me.ec_codrepr1out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr1out.OptionsColumn.ReadOnly = true
Me.ec_codrepr1out.OptionsFilter.AllowFilter = false
'
'ec_codrepr2out
'
Me.ec_codrepr2out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr2out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr2out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr2out.Caption = "2° promozione applicata"
Me.ec_codrepr2out.Enabled = false
Me.ec_codrepr2out.FieldName = "ec_codrepr2out"
Me.ec_codrepr2out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr2out.Name = "ec_codrepr2out"
Me.ec_codrepr2out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr2out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr2out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr2out.NTSRepositoryItemText = Nothing
Me.ec_codrepr2out.OptionsColumn.AllowEdit = false
Me.ec_codrepr2out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr2out.OptionsColumn.ReadOnly = true
Me.ec_codrepr2out.OptionsFilter.AllowFilter = false
'
'ec_codrepr3out
'
Me.ec_codrepr3out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr3out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr3out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr3out.Caption = "3° promozione applicata"
Me.ec_codrepr3out.Enabled = false
Me.ec_codrepr3out.FieldName = "ec_codrepr3out"
Me.ec_codrepr3out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr3out.Name = "ec_codrepr3out"
Me.ec_codrepr3out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr3out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr3out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr3out.NTSRepositoryItemText = Nothing
Me.ec_codrepr3out.OptionsColumn.AllowEdit = false
Me.ec_codrepr3out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr3out.OptionsColumn.ReadOnly = true
Me.ec_codrepr3out.OptionsFilter.AllowFilter = false
'
'ec_codrepr4out
'
Me.ec_codrepr4out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr4out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr4out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr4out.Caption = "4° promozione applicata"
Me.ec_codrepr4out.Enabled = false
Me.ec_codrepr4out.FieldName = "ec_codrepr4out"
Me.ec_codrepr4out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr4out.Name = "ec_codrepr4out"
Me.ec_codrepr4out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr4out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr4out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr4out.NTSRepositoryItemText = Nothing
Me.ec_codrepr4out.OptionsColumn.AllowEdit = false
Me.ec_codrepr4out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr4out.OptionsColumn.ReadOnly = true
Me.ec_codrepr4out.OptionsFilter.AllowFilter = false
'
'ec_codrepr5out
'
Me.ec_codrepr5out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr5out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr5out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr5out.Caption = "5° promozione applicata"
Me.ec_codrepr5out.Enabled = false
Me.ec_codrepr5out.FieldName = "ec_codrepr5out"
Me.ec_codrepr5out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr5out.Name = "ec_codrepr5out"
Me.ec_codrepr5out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr5out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr5out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr5out.NTSRepositoryItemText = Nothing
Me.ec_codrepr5out.OptionsColumn.AllowEdit = false
Me.ec_codrepr5out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr5out.OptionsColumn.ReadOnly = true
Me.ec_codrepr5out.OptionsFilter.AllowFilter = false
'
'ec_codrepr6out
'
Me.ec_codrepr6out.AppearanceCell.Options.UseBackColor = true
Me.ec_codrepr6out.AppearanceCell.Options.UseTextOptions = true
Me.ec_codrepr6out.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_codrepr6out.Caption = "6° promozione applicata"
Me.ec_codrepr6out.Enabled = false
Me.ec_codrepr6out.FieldName = "ec_codrepr6out"
Me.ec_codrepr6out.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_codrepr6out.Name = "ec_codrepr6out"
Me.ec_codrepr6out.NTSRepositoryComboBox = Nothing
Me.ec_codrepr6out.NTSRepositoryItemCheck = Nothing
Me.ec_codrepr6out.NTSRepositoryItemMemo = Nothing
Me.ec_codrepr6out.NTSRepositoryItemText = Nothing
Me.ec_codrepr6out.OptionsColumn.AllowEdit = false
Me.ec_codrepr6out.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_codrepr6out.OptionsColumn.ReadOnly = true
Me.ec_codrepr6out.OptionsFilter.AllowFilter = false
'
'ec_tiporiga
'
Me.ec_tiporiga.AppearanceCell.Options.UseBackColor = true
Me.ec_tiporiga.AppearanceCell.Options.UseTextOptions = true
Me.ec_tiporiga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_tiporiga.Caption = "Tipo riga"
Me.ec_tiporiga.Enabled = false
Me.ec_tiporiga.FieldName = "ec_tiporiga"
Me.ec_tiporiga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_tiporiga.Name = "ec_tiporiga"
Me.ec_tiporiga.NTSRepositoryComboBox = Nothing
Me.ec_tiporiga.NTSRepositoryItemCheck = Nothing
Me.ec_tiporiga.NTSRepositoryItemMemo = Nothing
Me.ec_tiporiga.NTSRepositoryItemText = Nothing
Me.ec_tiporiga.OptionsColumn.AllowEdit = false
Me.ec_tiporiga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_tiporiga.OptionsColumn.ReadOnly = true
Me.ec_tiporiga.OptionsFilter.AllowFilter = false
'
'ec_scpercdiff
'
Me.ec_scpercdiff.AppearanceCell.Options.UseBackColor = true
Me.ec_scpercdiff.AppearanceCell.Options.UseTextOptions = true
Me.ec_scpercdiff.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_scpercdiff.Caption = "Valore sconto % Differito"
Me.ec_scpercdiff.Enabled = false
Me.ec_scpercdiff.FieldName = "ec_scpercdiff"
Me.ec_scpercdiff.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_scpercdiff.Name = "ec_scpercdiff"
Me.ec_scpercdiff.NTSRepositoryComboBox = Nothing
Me.ec_scpercdiff.NTSRepositoryItemCheck = Nothing
Me.ec_scpercdiff.NTSRepositoryItemMemo = Nothing
Me.ec_scpercdiff.NTSRepositoryItemText = Nothing
Me.ec_scpercdiff.OptionsColumn.AllowEdit = false
Me.ec_scpercdiff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_scpercdiff.OptionsColumn.ReadOnly = true
Me.ec_scpercdiff.OptionsFilter.AllowFilter = false
'
'ec_prov1percdiff
'
Me.ec_prov1percdiff.AppearanceCell.Options.UseBackColor = true
Me.ec_prov1percdiff.AppearanceCell.Options.UseTextOptions = true
Me.ec_prov1percdiff.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prov1percdiff.Caption = "Valore provv. % Differita (agente 1)"
Me.ec_prov1percdiff.Enabled = false
Me.ec_prov1percdiff.FieldName = "ec_prov1percdiff"
Me.ec_prov1percdiff.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prov1percdiff.Name = "ec_prov1percdiff"
Me.ec_prov1percdiff.NTSRepositoryComboBox = Nothing
Me.ec_prov1percdiff.NTSRepositoryItemCheck = Nothing
Me.ec_prov1percdiff.NTSRepositoryItemMemo = Nothing
Me.ec_prov1percdiff.NTSRepositoryItemText = Nothing
Me.ec_prov1percdiff.OptionsColumn.AllowEdit = false
Me.ec_prov1percdiff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prov1percdiff.OptionsColumn.ReadOnly = true
Me.ec_prov1percdiff.OptionsFilter.AllowFilter = false
'
'ec_prov2percdiff
'
Me.ec_prov2percdiff.AppearanceCell.Options.UseBackColor = true
Me.ec_prov2percdiff.AppearanceCell.Options.UseTextOptions = true
Me.ec_prov2percdiff.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_prov2percdiff.Caption = "Valore provv. % Differita (agente 2)"
Me.ec_prov2percdiff.Enabled = false
Me.ec_prov2percdiff.FieldName = "ec_prov2percdiff"
Me.ec_prov2percdiff.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_prov2percdiff.Name = "ec_prov2percdiff"
Me.ec_prov2percdiff.NTSRepositoryComboBox = Nothing
Me.ec_prov2percdiff.NTSRepositoryItemCheck = Nothing
Me.ec_prov2percdiff.NTSRepositoryItemMemo = Nothing
Me.ec_prov2percdiff.NTSRepositoryItemText = Nothing
Me.ec_prov2percdiff.OptionsColumn.AllowEdit = false
Me.ec_prov2percdiff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_prov2percdiff.OptionsColumn.ReadOnly = true
Me.ec_prov2percdiff.OptionsFilter.AllowFilter = false
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
'pnTCO
'
Me.pnTCO.AllowDrop = true
Me.pnTCO.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTCO.Appearance.Options.UseBackColor = true
Me.pnTCO.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTCO.Controls.Add(Me.grTco)
Me.pnTCO.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTCO.Dock = System.Windows.Forms.DockStyle.Bottom
Me.pnTCO.Location = New System.Drawing.Point(0, 309)
Me.pnTCO.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnTCO.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnTCO.Name = "pnTCO"
Me.pnTCO.NTSActiveTrasparency = true
Me.pnTCO.Size = New System.Drawing.Size(722, 75)
Me.pnTCO.TabIndex = 3
Me.pnTCO.Text = "pnTCO"
'
'grTco
'
Me.grTco.Dock = System.Windows.Forms.DockStyle.Fill
Me.grTco.EmbeddedNavigator.Name = ""
Me.grTco.Location = New System.Drawing.Point(0, 0)
Me.grTco.MainView = Me.grvTco
Me.grTco.Name = "grTco"
Me.grTco.Size = New System.Drawing.Size(722, 75)
Me.grTco.TabIndex = 0
Me.grTco.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTco})
'
'grvTco
'
Me.grvTco.ActiveFilterEnabled = false
Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_quant01, Me.ec_quant02, Me.ec_quant03, Me.ec_quant04, Me.ec_quant05, Me.ec_quant06, Me.ec_quant07, Me.ec_quant08, Me.ec_quant09, Me.ec_quant10, Me.ec_quant11, Me.ec_quant12, Me.ec_quant13, Me.ec_quant14, Me.ec_quant15, Me.ec_quant16, Me.ec_quant17, Me.ec_quant18, Me.ec_quant19, Me.ec_quant20, Me.ec_quant21, Me.ec_quant22, Me.ec_quant23, Me.ec_quant24})
Me.grvTco.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
Me.grvTco.Enabled = true
Me.grvTco.GridControl = Me.grTco
Me.grvTco.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
Me.grvTco.MinRowHeight = 14
Me.grvTco.Name = "grvTco"
Me.grvTco.NTSAllowDelete = true
Me.grvTco.NTSAllowInsert = true
Me.grvTco.NTSAllowUpdate = true
Me.grvTco.NTSMenuContext = Nothing
Me.grvTco.OptionsCustomization.AllowRowSizing = true
Me.grvTco.OptionsFilter.AllowFilterEditor = false
Me.grvTco.OptionsNavigation.EnterMoveNextColumn = true
Me.grvTco.OptionsNavigation.UseTabKey = false
Me.grvTco.OptionsSelection.EnableAppearanceFocusedRow = false
Me.grvTco.OptionsView.ColumnAutoWidth = false
Me.grvTco.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
Me.grvTco.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
Me.grvTco.OptionsView.ShowGroupPanel = false
Me.grvTco.RowHeight = 14
'
'ec_quant01
'
Me.ec_quant01.AppearanceCell.Options.UseBackColor = true
Me.ec_quant01.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant01.Caption = "QTA1"
Me.ec_quant01.Enabled = true
Me.ec_quant01.FieldName = "ec_quant01"
Me.ec_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant01.Name = "ec_quant01"
Me.ec_quant01.NTSRepositoryComboBox = Nothing
Me.ec_quant01.NTSRepositoryItemCheck = Nothing
Me.ec_quant01.NTSRepositoryItemMemo = Nothing
Me.ec_quant01.NTSRepositoryItemText = Nothing
Me.ec_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant01.OptionsFilter.AllowFilter = false
Me.ec_quant01.Visible = true
Me.ec_quant01.VisibleIndex = 0
'
'ec_quant02
'
Me.ec_quant02.AppearanceCell.Options.UseBackColor = true
Me.ec_quant02.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant02.Caption = "QTA2"
Me.ec_quant02.Enabled = true
Me.ec_quant02.FieldName = "ec_quant02"
Me.ec_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant02.Name = "ec_quant02"
Me.ec_quant02.NTSRepositoryComboBox = Nothing
Me.ec_quant02.NTSRepositoryItemCheck = Nothing
Me.ec_quant02.NTSRepositoryItemMemo = Nothing
Me.ec_quant02.NTSRepositoryItemText = Nothing
Me.ec_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant02.OptionsFilter.AllowFilter = false
Me.ec_quant02.Visible = true
Me.ec_quant02.VisibleIndex = 1
'
'ec_quant03
'
Me.ec_quant03.AppearanceCell.Options.UseBackColor = true
Me.ec_quant03.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant03.Caption = "QTA3"
Me.ec_quant03.Enabled = true
Me.ec_quant03.FieldName = "ec_quant03"
Me.ec_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant03.Name = "ec_quant03"
Me.ec_quant03.NTSRepositoryComboBox = Nothing
Me.ec_quant03.NTSRepositoryItemCheck = Nothing
Me.ec_quant03.NTSRepositoryItemMemo = Nothing
Me.ec_quant03.NTSRepositoryItemText = Nothing
Me.ec_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant03.OptionsFilter.AllowFilter = false
Me.ec_quant03.Visible = true
Me.ec_quant03.VisibleIndex = 2
'
'ec_quant04
'
Me.ec_quant04.AppearanceCell.Options.UseBackColor = true
Me.ec_quant04.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant04.Caption = "QTA4"
Me.ec_quant04.Enabled = true
Me.ec_quant04.FieldName = "ec_quant04"
Me.ec_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant04.Name = "ec_quant04"
Me.ec_quant04.NTSRepositoryComboBox = Nothing
Me.ec_quant04.NTSRepositoryItemCheck = Nothing
Me.ec_quant04.NTSRepositoryItemMemo = Nothing
Me.ec_quant04.NTSRepositoryItemText = Nothing
Me.ec_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant04.OptionsFilter.AllowFilter = false
Me.ec_quant04.Visible = true
Me.ec_quant04.VisibleIndex = 3
'
'ec_quant05
'
Me.ec_quant05.AppearanceCell.Options.UseBackColor = true
Me.ec_quant05.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant05.Caption = "QTA5"
Me.ec_quant05.Enabled = true
Me.ec_quant05.FieldName = "ec_quant05"
Me.ec_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant05.Name = "ec_quant05"
Me.ec_quant05.NTSRepositoryComboBox = Nothing
Me.ec_quant05.NTSRepositoryItemCheck = Nothing
Me.ec_quant05.NTSRepositoryItemMemo = Nothing
Me.ec_quant05.NTSRepositoryItemText = Nothing
Me.ec_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant05.OptionsFilter.AllowFilter = false
Me.ec_quant05.Visible = true
Me.ec_quant05.VisibleIndex = 4
'
'ec_quant06
'
Me.ec_quant06.AppearanceCell.Options.UseBackColor = true
Me.ec_quant06.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant06.Caption = "QTA6"
Me.ec_quant06.Enabled = true
Me.ec_quant06.FieldName = "ec_quant06"
Me.ec_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant06.Name = "ec_quant06"
Me.ec_quant06.NTSRepositoryComboBox = Nothing
Me.ec_quant06.NTSRepositoryItemCheck = Nothing
Me.ec_quant06.NTSRepositoryItemMemo = Nothing
Me.ec_quant06.NTSRepositoryItemText = Nothing
Me.ec_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant06.OptionsFilter.AllowFilter = false
Me.ec_quant06.Visible = true
Me.ec_quant06.VisibleIndex = 5
'
'ec_quant07
'
Me.ec_quant07.AppearanceCell.Options.UseBackColor = true
Me.ec_quant07.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant07.Caption = "QTA7"
Me.ec_quant07.Enabled = true
Me.ec_quant07.FieldName = "ec_quant07"
Me.ec_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant07.Name = "ec_quant07"
Me.ec_quant07.NTSRepositoryComboBox = Nothing
Me.ec_quant07.NTSRepositoryItemCheck = Nothing
Me.ec_quant07.NTSRepositoryItemMemo = Nothing
Me.ec_quant07.NTSRepositoryItemText = Nothing
Me.ec_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant07.OptionsFilter.AllowFilter = false
Me.ec_quant07.Visible = true
Me.ec_quant07.VisibleIndex = 6
'
'ec_quant08
'
Me.ec_quant08.AppearanceCell.Options.UseBackColor = true
Me.ec_quant08.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant08.Caption = "QTA8"
Me.ec_quant08.Enabled = true
Me.ec_quant08.FieldName = "ec_quant08"
Me.ec_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant08.Name = "ec_quant08"
Me.ec_quant08.NTSRepositoryComboBox = Nothing
Me.ec_quant08.NTSRepositoryItemCheck = Nothing
Me.ec_quant08.NTSRepositoryItemMemo = Nothing
Me.ec_quant08.NTSRepositoryItemText = Nothing
Me.ec_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant08.OptionsFilter.AllowFilter = false
Me.ec_quant08.Visible = true
Me.ec_quant08.VisibleIndex = 7
'
'ec_quant09
'
Me.ec_quant09.AppearanceCell.Options.UseBackColor = true
Me.ec_quant09.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant09.Caption = "QTA9"
Me.ec_quant09.Enabled = true
Me.ec_quant09.FieldName = "ec_quant09"
Me.ec_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant09.Name = "ec_quant09"
Me.ec_quant09.NTSRepositoryComboBox = Nothing
Me.ec_quant09.NTSRepositoryItemCheck = Nothing
Me.ec_quant09.NTSRepositoryItemMemo = Nothing
Me.ec_quant09.NTSRepositoryItemText = Nothing
Me.ec_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant09.OptionsFilter.AllowFilter = false
Me.ec_quant09.Visible = true
Me.ec_quant09.VisibleIndex = 8
'
'ec_quant10
'
Me.ec_quant10.AppearanceCell.Options.UseBackColor = true
Me.ec_quant10.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant10.Caption = "QTA10"
Me.ec_quant10.Enabled = true
Me.ec_quant10.FieldName = "ec_quant10"
Me.ec_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant10.Name = "ec_quant10"
Me.ec_quant10.NTSRepositoryComboBox = Nothing
Me.ec_quant10.NTSRepositoryItemCheck = Nothing
Me.ec_quant10.NTSRepositoryItemMemo = Nothing
Me.ec_quant10.NTSRepositoryItemText = Nothing
Me.ec_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant10.OptionsFilter.AllowFilter = false
Me.ec_quant10.Visible = true
Me.ec_quant10.VisibleIndex = 9
'
'ec_quant11
'
Me.ec_quant11.AppearanceCell.Options.UseBackColor = true
Me.ec_quant11.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant11.Caption = "QTA11"
Me.ec_quant11.Enabled = true
Me.ec_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant11.Name = "ec_quant11"
Me.ec_quant11.NTSRepositoryComboBox = Nothing
Me.ec_quant11.NTSRepositoryItemCheck = Nothing
Me.ec_quant11.NTSRepositoryItemMemo = Nothing
Me.ec_quant11.NTSRepositoryItemText = Nothing
Me.ec_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant11.OptionsFilter.AllowFilter = false
Me.ec_quant11.Visible = true
Me.ec_quant11.VisibleIndex = 10
'
'ec_quant12
'
Me.ec_quant12.AppearanceCell.Options.UseBackColor = true
Me.ec_quant12.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant12.Caption = "QTA12"
Me.ec_quant12.Enabled = true
Me.ec_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant12.Name = "ec_quant12"
Me.ec_quant12.NTSRepositoryComboBox = Nothing
Me.ec_quant12.NTSRepositoryItemCheck = Nothing
Me.ec_quant12.NTSRepositoryItemMemo = Nothing
Me.ec_quant12.NTSRepositoryItemText = Nothing
Me.ec_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant12.OptionsFilter.AllowFilter = false
Me.ec_quant12.Visible = true
Me.ec_quant12.VisibleIndex = 11
'
'ec_quant13
'
Me.ec_quant13.AppearanceCell.Options.UseBackColor = true
Me.ec_quant13.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant13.Caption = "QTA13"
Me.ec_quant13.Enabled = true
Me.ec_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant13.Name = "ec_quant13"
Me.ec_quant13.NTSRepositoryComboBox = Nothing
Me.ec_quant13.NTSRepositoryItemCheck = Nothing
Me.ec_quant13.NTSRepositoryItemMemo = Nothing
Me.ec_quant13.NTSRepositoryItemText = Nothing
Me.ec_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant13.OptionsFilter.AllowFilter = false
Me.ec_quant13.Visible = true
Me.ec_quant13.VisibleIndex = 12
'
'ec_quant14
'
Me.ec_quant14.AppearanceCell.Options.UseBackColor = true
Me.ec_quant14.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant14.Caption = "QTA14"
Me.ec_quant14.Enabled = true
Me.ec_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant14.Name = "ec_quant14"
Me.ec_quant14.NTSRepositoryComboBox = Nothing
Me.ec_quant14.NTSRepositoryItemCheck = Nothing
Me.ec_quant14.NTSRepositoryItemMemo = Nothing
Me.ec_quant14.NTSRepositoryItemText = Nothing
Me.ec_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant14.OptionsFilter.AllowFilter = false
Me.ec_quant14.Visible = true
Me.ec_quant14.VisibleIndex = 13
'
'ec_quant15
'
Me.ec_quant15.AppearanceCell.Options.UseBackColor = true
Me.ec_quant15.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant15.Caption = "QTA15"
Me.ec_quant15.Enabled = true
Me.ec_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant15.Name = "ec_quant15"
Me.ec_quant15.NTSRepositoryComboBox = Nothing
Me.ec_quant15.NTSRepositoryItemCheck = Nothing
Me.ec_quant15.NTSRepositoryItemMemo = Nothing
Me.ec_quant15.NTSRepositoryItemText = Nothing
Me.ec_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant15.OptionsFilter.AllowFilter = false
Me.ec_quant15.Visible = true
Me.ec_quant15.VisibleIndex = 14
'
'ec_quant16
'
Me.ec_quant16.AppearanceCell.Options.UseBackColor = true
Me.ec_quant16.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant16.Caption = "QTA16"
Me.ec_quant16.Enabled = true
Me.ec_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant16.Name = "ec_quant16"
Me.ec_quant16.NTSRepositoryComboBox = Nothing
Me.ec_quant16.NTSRepositoryItemCheck = Nothing
Me.ec_quant16.NTSRepositoryItemMemo = Nothing
Me.ec_quant16.NTSRepositoryItemText = Nothing
Me.ec_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant16.OptionsFilter.AllowFilter = false
Me.ec_quant16.Visible = true
Me.ec_quant16.VisibleIndex = 15
'
'ec_quant17
'
Me.ec_quant17.AppearanceCell.Options.UseBackColor = true
Me.ec_quant17.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant17.Caption = "QTA17"
Me.ec_quant17.Enabled = true
Me.ec_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant17.Name = "ec_quant17"
Me.ec_quant17.NTSRepositoryComboBox = Nothing
Me.ec_quant17.NTSRepositoryItemCheck = Nothing
Me.ec_quant17.NTSRepositoryItemMemo = Nothing
Me.ec_quant17.NTSRepositoryItemText = Nothing
Me.ec_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant17.OptionsFilter.AllowFilter = false
Me.ec_quant17.Visible = true
Me.ec_quant17.VisibleIndex = 16
'
'ec_quant18
'
Me.ec_quant18.AppearanceCell.Options.UseBackColor = true
Me.ec_quant18.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant18.Caption = "QTA18"
Me.ec_quant18.Enabled = true
Me.ec_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant18.Name = "ec_quant18"
Me.ec_quant18.NTSRepositoryComboBox = Nothing
Me.ec_quant18.NTSRepositoryItemCheck = Nothing
Me.ec_quant18.NTSRepositoryItemMemo = Nothing
Me.ec_quant18.NTSRepositoryItemText = Nothing
Me.ec_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant18.OptionsFilter.AllowFilter = false
Me.ec_quant18.Visible = true
Me.ec_quant18.VisibleIndex = 17
'
'ec_quant19
'
Me.ec_quant19.AppearanceCell.Options.UseBackColor = true
Me.ec_quant19.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant19.Caption = "QTA19"
Me.ec_quant19.Enabled = true
Me.ec_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant19.Name = "ec_quant19"
Me.ec_quant19.NTSRepositoryComboBox = Nothing
Me.ec_quant19.NTSRepositoryItemCheck = Nothing
Me.ec_quant19.NTSRepositoryItemMemo = Nothing
Me.ec_quant19.NTSRepositoryItemText = Nothing
Me.ec_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant19.OptionsFilter.AllowFilter = false
Me.ec_quant19.Visible = true
Me.ec_quant19.VisibleIndex = 18
'
'ec_quant20
'
Me.ec_quant20.AppearanceCell.Options.UseBackColor = true
Me.ec_quant20.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant20.Caption = "QTA20"
Me.ec_quant20.Enabled = true
Me.ec_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant20.Name = "ec_quant20"
Me.ec_quant20.NTSRepositoryComboBox = Nothing
Me.ec_quant20.NTSRepositoryItemCheck = Nothing
Me.ec_quant20.NTSRepositoryItemMemo = Nothing
Me.ec_quant20.NTSRepositoryItemText = Nothing
Me.ec_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant20.OptionsFilter.AllowFilter = false
Me.ec_quant20.Visible = true
Me.ec_quant20.VisibleIndex = 19
'
'ec_quant21
'
Me.ec_quant21.AppearanceCell.Options.UseBackColor = true
Me.ec_quant21.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant21.Caption = "QTA21"
Me.ec_quant21.Enabled = true
Me.ec_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant21.Name = "ec_quant21"
Me.ec_quant21.NTSRepositoryComboBox = Nothing
Me.ec_quant21.NTSRepositoryItemCheck = Nothing
Me.ec_quant21.NTSRepositoryItemMemo = Nothing
Me.ec_quant21.NTSRepositoryItemText = Nothing
Me.ec_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant21.OptionsFilter.AllowFilter = false
Me.ec_quant21.Visible = true
Me.ec_quant21.VisibleIndex = 20
'
'ec_quant22
'
Me.ec_quant22.AppearanceCell.Options.UseBackColor = true
Me.ec_quant22.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant22.Caption = "QTA22"
Me.ec_quant22.Enabled = true
Me.ec_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant22.Name = "ec_quant22"
Me.ec_quant22.NTSRepositoryComboBox = Nothing
Me.ec_quant22.NTSRepositoryItemCheck = Nothing
Me.ec_quant22.NTSRepositoryItemMemo = Nothing
Me.ec_quant22.NTSRepositoryItemText = Nothing
Me.ec_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant22.OptionsFilter.AllowFilter = false
Me.ec_quant22.Visible = true
Me.ec_quant22.VisibleIndex = 21
'
'ec_quant23
'
Me.ec_quant23.AppearanceCell.Options.UseBackColor = true
Me.ec_quant23.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant23.Caption = "QTA23"
Me.ec_quant23.Enabled = true
Me.ec_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant23.Name = "ec_quant23"
Me.ec_quant23.NTSRepositoryComboBox = Nothing
Me.ec_quant23.NTSRepositoryItemCheck = Nothing
Me.ec_quant23.NTSRepositoryItemMemo = Nothing
Me.ec_quant23.NTSRepositoryItemText = Nothing
Me.ec_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant23.OptionsFilter.AllowFilter = false
Me.ec_quant23.Visible = true
Me.ec_quant23.VisibleIndex = 22
'
'ec_quant24
'
Me.ec_quant24.AppearanceCell.Options.UseBackColor = true
Me.ec_quant24.AppearanceCell.Options.UseTextOptions = true
Me.ec_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.ec_quant24.Caption = "QTA24"
Me.ec_quant24.Enabled = true
Me.ec_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.ec_quant24.Name = "ec_quant24"
Me.ec_quant24.NTSRepositoryComboBox = Nothing
Me.ec_quant24.NTSRepositoryItemCheck = Nothing
Me.ec_quant24.NTSRepositoryItemMemo = Nothing
Me.ec_quant24.NTSRepositoryItemText = Nothing
Me.ec_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.ec_quant24.OptionsFilter.AllowFilter = false
Me.ec_quant24.Visible = true
Me.ec_quant24.VisibleIndex = 23
'
'pnCorpo
'
Me.pnCorpo.AllowDrop = true
Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnCorpo.Appearance.Options.UseBackColor = true
Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnCorpo.Controls.Add(Me.lbSconto)
Me.pnCorpo.Controls.Add(Me.edSconto)
Me.pnCorpo.Controls.Add(Me.edUltCost)
Me.pnCorpo.Controls.Add(Me.edPreList)
Me.pnCorpo.Controls.Add(Me.edOrdinato)
Me.pnCorpo.Controls.Add(Me.edDispNetta)
Me.pnCorpo.Controls.Add(Me.edImpegnato)
Me.pnCorpo.Controls.Add(Me.lbOrdinImpegn)
Me.pnCorpo.Controls.Add(Me.edDispon)
Me.pnCorpo.Controls.Add(Me.lbDispon)
Me.pnCorpo.Controls.Add(Me.lbPreList)
Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
Me.pnCorpo.Dock = System.Windows.Forms.DockStyle.Top
Me.pnCorpo.Location = New System.Drawing.Point(0, 0)
Me.pnCorpo.Name = "pnCorpo"
Me.pnCorpo.NTSActiveTrasparency = true
Me.pnCorpo.Size = New System.Drawing.Size(722, 41)
Me.pnCorpo.TabIndex = 1
'
'lbSconto
'
Me.lbSconto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbSconto.AutoSize = true
Me.lbSconto.BackColor = System.Drawing.Color.Transparent
Me.lbSconto.Location = New System.Drawing.Point(263, 14)
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
Me.edSconto.Cursor = System.Windows.Forms.Cursors.Default
Me.edSconto.EditValue = "0"
Me.edSconto.Enabled = false
Me.edSconto.Location = New System.Drawing.Point(328, 11)
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
Me.edUltCost.Location = New System.Drawing.Point(177, 11)
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
Me.edPreList.Location = New System.Drawing.Point(91, 11)
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
Me.edPreList.Size = New System.Drawing.Size(80, 20)
Me.edPreList.TabIndex = 47
'
'edOrdinato
'
Me.edOrdinato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edOrdinato.Cursor = System.Windows.Forms.Cursors.Default
Me.edOrdinato.EditValue = "0"
Me.edOrdinato.Enabled = false
Me.edOrdinato.Location = New System.Drawing.Point(617, 20)
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
Me.edOrdinato.TabIndex = 46
'
'edDispNetta
'
Me.edDispNetta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edDispNetta.Cursor = System.Windows.Forms.Cursors.Default
Me.edDispNetta.EditValue = "0"
Me.edDispNetta.Enabled = false
Me.edDispNetta.Location = New System.Drawing.Point(617, 0)
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
'edImpegnato
'
Me.edImpegnato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edImpegnato.Cursor = System.Windows.Forms.Cursors.Default
Me.edImpegnato.EditValue = "0"
Me.edImpegnato.Enabled = false
Me.edImpegnato.Location = New System.Drawing.Point(507, 20)
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
Me.edImpegnato.TabIndex = 45
'
'lbOrdinImpegn
'
Me.lbOrdinImpegn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbOrdinImpegn.AutoSize = true
Me.lbOrdinImpegn.BackColor = System.Drawing.Color.Transparent
Me.lbOrdinImpegn.Location = New System.Drawing.Point(391, 23)
Me.lbOrdinImpegn.Name = "lbOrdinImpegn"
Me.lbOrdinImpegn.NTSDbField = ""
Me.lbOrdinImpegn.Size = New System.Drawing.Size(109, 13)
Me.lbOrdinImpegn.TabIndex = 5
Me.lbOrdinImpegn.Text = "Impegnato / ordinato"
Me.lbOrdinImpegn.Tooltip = ""
Me.lbOrdinImpegn.UseMnemonic = false
'
'edDispon
'
Me.edDispon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edDispon.Cursor = System.Windows.Forms.Cursors.Default
Me.edDispon.EditValue = "0"
Me.edDispon.Enabled = false
Me.edDispon.Location = New System.Drawing.Point(507, 0)
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
Me.lbDispon.Location = New System.Drawing.Point(391, 3)
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
Me.lbPreList.Location = New System.Drawing.Point(-7, 14)
Me.lbPreList.Name = "lbPreList"
Me.lbPreList.NTSDbField = ""
Me.lbPreList.Size = New System.Drawing.Size(92, 13)
Me.lbPreList.TabIndex = 3
Me.lbPreList.Text = "Listino / ult. costo"
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
Me.TabPage1.Size = New System.Drawing.Size(722, 384)
Me.TabPage1.Text = "&1 - Testata"
'
'pnTestataDx
'
Me.pnTestataDx.AllowDrop = true
Me.pnTestataDx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTestataDx.Appearance.Options.UseBackColor = true
Me.pnTestataDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta2)
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta4)
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta1)
Me.pnTestataDx.Controls.Add(Me.pnDatiTesta3)
Me.pnTestataDx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataDx.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnTestataDx.Location = New System.Drawing.Point(153, 0)
Me.pnTestataDx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnTestataDx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnTestataDx.Name = "pnTestataDx"
Me.pnTestataDx.NTSActiveTrasparency = true
Me.pnTestataDx.Size = New System.Drawing.Size(569, 384)
Me.pnTestataDx.TabIndex = 1
'
'pnDatiTesta2
'
Me.pnDatiTesta2.AllowDrop = true
Me.pnDatiTesta2.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta2.Appearance.Options.UseBackColor = true
Me.pnDatiTesta2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta2.Controls.Add(Me.ckEt_rilasciato)
Me.pnDatiTesta2.Controls.Add(Me.ckEt_aperto)
Me.pnDatiTesta2.Controls.Add(Me.ckEt_scorpo)
Me.pnDatiTesta2.Controls.Add(Me.ckEt_flspinc)
Me.pnDatiTesta2.Controls.Add(Me.ckEt_flboll)
Me.pnDatiTesta2.Controls.Add(Me.ckEt_soloasa)
Me.pnDatiTesta2.Controls.Add(Me.edEt_datcons)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_datcons)
Me.pnDatiTesta2.Controls.Add(Me.edEt_magaz)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_magaz)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_magaz)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_magaz2)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_magimp)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_codpaga)
Me.pnDatiTesta2.Controls.Add(Me.edEt_magaz2)
Me.pnDatiTesta2.Controls.Add(Me.edEt_scont2)
Me.pnDatiTesta2.Controls.Add(Me.edEt_magimp)
Me.pnDatiTesta2.Controls.Add(Me.edEt_scont1)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_magaz2)
Me.pnDatiTesta2.Controls.Add(Me.edEt_datapag)
Me.pnDatiTesta2.Controls.Add(Me.lbXx_magimp)
Me.pnDatiTesta2.Controls.Add(Me.edEt_scopag)
Me.pnDatiTesta2.Controls.Add(Me.edEt_listino)
Me.pnDatiTesta2.Controls.Add(Me.edEt_codpaga)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_listino)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_codpaga)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_scopag)
Me.pnDatiTesta2.Controls.Add(Me.lbEt_datapag)
Me.pnDatiTesta2.Controls.Add(Me.lbScontiTesta)
Me.pnDatiTesta2.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta2.Location = New System.Drawing.Point(0, 178)
Me.pnDatiTesta2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta2.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta2.Name = "pnDatiTesta2"
Me.pnDatiTesta2.NTSActiveTrasparency = true
Me.pnDatiTesta2.Size = New System.Drawing.Size(569, 178)
Me.pnDatiTesta2.TabIndex = 69
'
'ckEt_rilasciato
'
Me.ckEt_rilasciato.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_rilasciato.Location = New System.Drawing.Point(459, 131)
Me.ckEt_rilasciato.Name = "ckEt_rilasciato"
Me.ckEt_rilasciato.NTSCheckValue = "S"
Me.ckEt_rilasciato.NTSUnCheckValue = "N"
Me.ckEt_rilasciato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_rilasciato.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_rilasciato.Properties.AutoHeight = false
Me.ckEt_rilasciato.Properties.Caption = "&Rilasciato"
Me.ckEt_rilasciato.Size = New System.Drawing.Size(84, 19)
Me.ckEt_rilasciato.TabIndex = 6
'
'ckEt_aperto
'
Me.ckEt_aperto.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_aperto.Location = New System.Drawing.Point(459, 109)
Me.ckEt_aperto.Name = "ckEt_aperto"
Me.ckEt_aperto.NTSCheckValue = "S"
Me.ckEt_aperto.NTSUnCheckValue = "N"
Me.ckEt_aperto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_aperto.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_aperto.Properties.AutoHeight = false
Me.ckEt_aperto.Properties.Caption = "&Aperto"
Me.ckEt_aperto.Size = New System.Drawing.Size(68, 19)
Me.ckEt_aperto.TabIndex = 7
'
'ckEt_scorpo
'
Me.ckEt_scorpo.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_scorpo.Location = New System.Drawing.Point(459, 9)
Me.ckEt_scorpo.Name = "ckEt_scorpo"
Me.ckEt_scorpo.NTSCheckValue = "S"
Me.ckEt_scorpo.NTSUnCheckValue = "N"
Me.ckEt_scorpo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_scorpo.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_scorpo.Properties.AutoHeight = false
Me.ckEt_scorpo.Properties.Caption = "Scor&poro"
Me.ckEt_scorpo.Size = New System.Drawing.Size(84, 19)
Me.ckEt_scorpo.TabIndex = 0
'
'ckEt_flspinc
'
Me.ckEt_flspinc.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flspinc.Location = New System.Drawing.Point(459, 30)
Me.ckEt_flspinc.Name = "ckEt_flspinc"
Me.ckEt_flspinc.NTSCheckValue = "S"
Me.ckEt_flspinc.NTSUnCheckValue = "N"
Me.ckEt_flspinc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flspinc.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flspinc.Properties.AutoHeight = false
Me.ckEt_flspinc.Properties.Caption = "&Add. sp. &Incasso"
Me.ckEt_flspinc.Size = New System.Drawing.Size(106, 19)
Me.ckEt_flspinc.TabIndex = 1
'
'ckEt_flboll
'
Me.ckEt_flboll.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flboll.Location = New System.Drawing.Point(459, 52)
Me.ckEt_flboll.Name = "ckEt_flboll"
Me.ckEt_flboll.NTSCheckValue = "S"
Me.ckEt_flboll.NTSUnCheckValue = "N"
Me.ckEt_flboll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flboll.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flboll.Properties.AutoHeight = false
Me.ckEt_flboll.Properties.Caption = "&Add. &Bolli"
Me.ckEt_flboll.Size = New System.Drawing.Size(84, 19)
Me.ckEt_flboll.TabIndex = 2
'
'ckEt_soloasa
'
Me.ckEt_soloasa.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_soloasa.Location = New System.Drawing.Point(459, 86)
Me.ckEt_soloasa.Name = "ckEt_soloasa"
Me.ckEt_soloasa.NTSCheckValue = "S"
Me.ckEt_soloasa.NTSUnCheckValue = "N"
Me.ckEt_soloasa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_soloasa.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_soloasa.Properties.AutoHeight = false
Me.ckEt_soloasa.Properties.Caption = "&Solo a saldo"
Me.ckEt_soloasa.Size = New System.Drawing.Size(91, 19)
Me.ckEt_soloasa.TabIndex = 3
'
'edEt_datcons
'
Me.edEt_datcons.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_datcons.EditValue = "01/01/1900"
Me.edEt_datcons.Location = New System.Drawing.Point(96, 6)
Me.edEt_datcons.Name = "edEt_datcons"
Me.edEt_datcons.NTSDbField = ""
Me.edEt_datcons.NTSForzaVisZoom = false
Me.edEt_datcons.NTSOldValue = ""
Me.edEt_datcons.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_datcons.Properties.Appearance.Options.UseBackColor = true
Me.edEt_datcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_datcons.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_datcons.Properties.AutoHeight = false
Me.edEt_datcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_datcons.Properties.MaxLength = 65536
Me.edEt_datcons.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_datcons.Size = New System.Drawing.Size(94, 20)
Me.edEt_datcons.TabIndex = 56
'
'lbEt_datcons
'
Me.lbEt_datcons.AutoSize = true
Me.lbEt_datcons.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datcons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_datcons.Location = New System.Drawing.Point(5, 9)
Me.lbEt_datcons.Name = "lbEt_datcons"
Me.lbEt_datcons.NTSDbField = ""
Me.lbEt_datcons.Size = New System.Drawing.Size(92, 13)
Me.lbEt_datcons.TabIndex = 65
Me.lbEt_datcons.Text = "Data Consegna"
Me.lbEt_datcons.Tooltip = ""
Me.lbEt_datcons.UseMnemonic = false
'
'edEt_magaz
'
Me.edEt_magaz.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_magaz.EditValue = "0"
Me.edEt_magaz.Location = New System.Drawing.Point(96, 108)
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
Me.edEt_magaz.Size = New System.Drawing.Size(52, 20)
Me.edEt_magaz.TabIndex = 51
'
'lbEt_magaz
'
Me.lbEt_magaz.AutoSize = true
Me.lbEt_magaz.BackColor = System.Drawing.Color.Transparent
Me.lbEt_magaz.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_magaz.Location = New System.Drawing.Point(5, 111)
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
Me.lbXx_magaz.Location = New System.Drawing.Point(149, 108)
Me.lbXx_magaz.Name = "lbXx_magaz"
Me.lbXx_magaz.NTSDbField = ""
Me.lbXx_magaz.Size = New System.Drawing.Size(208, 20)
Me.lbXx_magaz.TabIndex = 53
Me.lbXx_magaz.Tooltip = ""
Me.lbXx_magaz.UseMnemonic = false
'
'lbEt_magaz2
'
Me.lbEt_magaz2.AutoSize = true
Me.lbEt_magaz2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_magaz2.Location = New System.Drawing.Point(5, 133)
Me.lbEt_magaz2.Name = "lbEt_magaz2"
Me.lbEt_magaz2.NTSDbField = ""
Me.lbEt_magaz2.Size = New System.Drawing.Size(66, 13)
Me.lbEt_magaz2.TabIndex = 59
Me.lbEt_magaz2.Text = "Magazzino 2"
Me.lbEt_magaz2.Tooltip = ""
Me.lbEt_magaz2.UseMnemonic = false
'
'lbEt_magimp
'
Me.lbEt_magimp.AutoSize = true
Me.lbEt_magimp.BackColor = System.Drawing.Color.Transparent
Me.lbEt_magimp.Location = New System.Drawing.Point(5, 155)
Me.lbEt_magimp.Name = "lbEt_magimp"
Me.lbEt_magimp.NTSDbField = ""
Me.lbEt_magimp.Size = New System.Drawing.Size(80, 13)
Me.lbEt_magimp.TabIndex = 60
Me.lbEt_magimp.Text = "Magazzino imp."
Me.lbEt_magimp.Tooltip = ""
Me.lbEt_magimp.UseMnemonic = false
'
'lbXx_codpaga
'
Me.lbXx_codpaga.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codpaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codpaga.Location = New System.Drawing.Point(152, 55)
Me.lbXx_codpaga.Name = "lbXx_codpaga"
Me.lbXx_codpaga.NTSDbField = ""
Me.lbXx_codpaga.Size = New System.Drawing.Size(210, 20)
Me.lbXx_codpaga.TabIndex = 52
Me.lbXx_codpaga.Tooltip = ""
Me.lbXx_codpaga.UseMnemonic = false
'
'edEt_magaz2
'
Me.edEt_magaz2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_magaz2.EditValue = "0"
Me.edEt_magaz2.Location = New System.Drawing.Point(96, 130)
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
Me.edEt_magaz2.Size = New System.Drawing.Size(52, 20)
Me.edEt_magaz2.TabIndex = 61
'
'edEt_scont2
'
Me.edEt_scont2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scont2.EditValue = "0"
Me.edEt_scont2.Location = New System.Drawing.Point(318, 32)
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
'edEt_magimp
'
Me.edEt_magimp.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_magimp.EditValue = "0"
Me.edEt_magimp.Location = New System.Drawing.Point(96, 152)
Me.edEt_magimp.Name = "edEt_magimp"
Me.edEt_magimp.NTSDbField = ""
Me.edEt_magimp.NTSFormat = "0"
Me.edEt_magimp.NTSForzaVisZoom = false
Me.edEt_magimp.NTSOldValue = ""
Me.edEt_magimp.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_magimp.Properties.Appearance.Options.UseBackColor = true
Me.edEt_magimp.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_magimp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_magimp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_magimp.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_magimp.Properties.AutoHeight = false
Me.edEt_magimp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_magimp.Properties.MaxLength = 65536
Me.edEt_magimp.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_magimp.Size = New System.Drawing.Size(52, 20)
Me.edEt_magimp.TabIndex = 62
'
'edEt_scont1
'
Me.edEt_scont1.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scont1.EditValue = "0"
Me.edEt_scont1.Location = New System.Drawing.Point(268, 32)
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
Me.edEt_scont1.Size = New System.Drawing.Size(43, 20)
Me.edEt_scont1.TabIndex = 50
'
'lbXx_magaz2
'
Me.lbXx_magaz2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_magaz2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_magaz2.Location = New System.Drawing.Point(149, 130)
Me.lbXx_magaz2.Name = "lbXx_magaz2"
Me.lbXx_magaz2.NTSDbField = ""
Me.lbXx_magaz2.Size = New System.Drawing.Size(208, 20)
Me.lbXx_magaz2.TabIndex = 63
Me.lbXx_magaz2.Tooltip = ""
Me.lbXx_magaz2.UseMnemonic = false
'
'edEt_datapag
'
Me.edEt_datapag.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_datapag.EditValue = "01/01/1900"
Me.edEt_datapag.Location = New System.Drawing.Point(268, 79)
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
'lbXx_magimp
'
Me.lbXx_magimp.BackColor = System.Drawing.Color.Transparent
Me.lbXx_magimp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_magimp.Location = New System.Drawing.Point(149, 152)
Me.lbXx_magimp.Name = "lbXx_magimp"
Me.lbXx_magimp.NTSDbField = ""
Me.lbXx_magimp.Size = New System.Drawing.Size(208, 20)
Me.lbXx_magimp.TabIndex = 64
Me.lbXx_magimp.Tooltip = ""
Me.lbXx_magimp.UseMnemonic = false
'
'edEt_scopag
'
Me.edEt_scopag.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_scopag.EditValue = "0"
Me.edEt_scopag.Location = New System.Drawing.Point(96, 79)
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
'edEt_listino
'
Me.edEt_listino.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_listino.EditValue = "0"
Me.edEt_listino.Location = New System.Drawing.Point(96, 32)
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
'edEt_codpaga
'
Me.edEt_codpaga.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codpaga.EditValue = "0"
Me.edEt_codpaga.Location = New System.Drawing.Point(96, 55)
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
'lbEt_listino
'
Me.lbEt_listino.AutoSize = true
Me.lbEt_listino.BackColor = System.Drawing.Color.Transparent
Me.lbEt_listino.Location = New System.Drawing.Point(5, 35)
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
Me.lbEt_codpaga.Location = New System.Drawing.Point(5, 58)
Me.lbEt_codpaga.Name = "lbEt_codpaga"
Me.lbEt_codpaga.NTSDbField = ""
Me.lbEt_codpaga.Size = New System.Drawing.Size(87, 13)
Me.lbEt_codpaga.TabIndex = 2
Me.lbEt_codpaga.Text = "Cod. Pagamento"
Me.lbEt_codpaga.Tooltip = ""
Me.lbEt_codpaga.UseMnemonic = false
'
'lbEt_scopag
'
Me.lbEt_scopag.AutoSize = true
Me.lbEt_scopag.BackColor = System.Drawing.Color.Transparent
Me.lbEt_scopag.Location = New System.Drawing.Point(5, 82)
Me.lbEt_scopag.Name = "lbEt_scopag"
Me.lbEt_scopag.NTSDbField = ""
Me.lbEt_scopag.Size = New System.Drawing.Size(79, 13)
Me.lbEt_scopag.TabIndex = 3
Me.lbEt_scopag.Text = "Sconto Pagam."
Me.lbEt_scopag.Tooltip = ""
Me.lbEt_scopag.UseMnemonic = false
'
'lbEt_datapag
'
Me.lbEt_datapag.AutoSize = true
Me.lbEt_datapag.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datapag.Location = New System.Drawing.Point(186, 82)
Me.lbEt_datapag.Name = "lbEt_datapag"
Me.lbEt_datapag.NTSDbField = ""
Me.lbEt_datapag.Size = New System.Drawing.Size(64, 13)
Me.lbEt_datapag.TabIndex = 4
Me.lbEt_datapag.Text = "Data 1 pag."
Me.lbEt_datapag.Tooltip = ""
Me.lbEt_datapag.UseMnemonic = false
'
'lbScontiTesta
'
Me.lbScontiTesta.AutoSize = true
Me.lbScontiTesta.BackColor = System.Drawing.Color.Transparent
Me.lbScontiTesta.Location = New System.Drawing.Point(186, 35)
Me.lbScontiTesta.Name = "lbScontiTesta"
Me.lbScontiTesta.NTSDbField = ""
Me.lbScontiTesta.Size = New System.Drawing.Size(77, 13)
Me.lbScontiTesta.TabIndex = 7
Me.lbScontiTesta.Text = "Sconti generali"
Me.lbScontiTesta.Tooltip = ""
Me.lbScontiTesta.UseMnemonic = false
'
'pnDatiTesta4
'
Me.pnDatiTesta4.AllowDrop = true
Me.pnDatiTesta4.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta4.Appearance.Options.UseBackColor = true
Me.pnDatiTesta4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta4.Controls.Add(Me.fmPa)
Me.pnDatiTesta4.Controls.Add(Me.edEt_valuta)
Me.pnDatiTesta4.Controls.Add(Me.edEt_cambio)
Me.pnDatiTesta4.Controls.Add(Me.lbXx_valuta)
Me.pnDatiTesta4.Controls.Add(Me.lbEt_valuta)
Me.pnDatiTesta4.Controls.Add(Me.lbXx_vettor)
Me.pnDatiTesta4.Controls.Add(Me.lbXx_contfatt)
Me.pnDatiTesta4.Controls.Add(Me.lbEt_vettor)
Me.pnDatiTesta4.Controls.Add(Me.edEt_contfatt)
Me.pnDatiTesta4.Controls.Add(Me.lbEt_contfatt)
Me.pnDatiTesta4.Controls.Add(Me.edEt_vettor)
Me.pnDatiTesta4.Controls.Add(Me.lbXx_controp)
Me.pnDatiTesta4.Controls.Add(Me.lbXx_codese)
Me.pnDatiTesta4.Controls.Add(Me.lbEt_controp)
Me.pnDatiTesta4.Controls.Add(Me.lbEt_codese)
Me.pnDatiTesta4.Controls.Add(Me.edEt_controp)
Me.pnDatiTesta4.Controls.Add(Me.edEt_codese)
Me.pnDatiTesta4.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta4.Location = New System.Drawing.Point(0, 581)
Me.pnDatiTesta4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta4.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta4.Name = "pnDatiTesta4"
Me.pnDatiTesta4.NTSActiveTrasparency = true
Me.pnDatiTesta4.Size = New System.Drawing.Size(566, 190)
Me.pnDatiTesta4.TabIndex = 71
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
Me.fmPa.Location = New System.Drawing.Point(6, 99)
Me.fmPa.Name = "fmPa"
Me.fmPa.Size = New System.Drawing.Size(279, 90)
Me.fmPa.TabIndex = 90
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
Me.lbEt_cig.Size = New System.Drawing.Size(43, 13)
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
Me.edEt_valuta.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_valuta.EditValue = "0"
Me.edEt_valuta.Location = New System.Drawing.Point(95, 6)
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
'edEt_cambio
'
Me.edEt_cambio.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_cambio.EditValue = "0"
Me.edEt_cambio.Location = New System.Drawing.Point(269, 6)
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
'lbXx_valuta
'
Me.lbXx_valuta.BackColor = System.Drawing.Color.Transparent
Me.lbXx_valuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_valuta.Location = New System.Drawing.Point(149, 6)
Me.lbXx_valuta.Name = "lbXx_valuta"
Me.lbXx_valuta.NTSDbField = ""
Me.lbXx_valuta.Size = New System.Drawing.Size(112, 20)
Me.lbXx_valuta.TabIndex = 55
Me.lbXx_valuta.Tooltip = ""
Me.lbXx_valuta.UseMnemonic = false
'
'lbEt_valuta
'
Me.lbEt_valuta.AutoSize = true
Me.lbEt_valuta.BackColor = System.Drawing.Color.Transparent
Me.lbEt_valuta.Location = New System.Drawing.Point(3, 9)
Me.lbEt_valuta.Name = "lbEt_valuta"
Me.lbEt_valuta.NTSDbField = ""
Me.lbEt_valuta.Size = New System.Drawing.Size(74, 13)
Me.lbEt_valuta.TabIndex = 53
Me.lbEt_valuta.Text = "Valuta/cambio"
Me.lbEt_valuta.Tooltip = ""
Me.lbEt_valuta.UseMnemonic = false
'
'lbXx_vettor
'
Me.lbXx_vettor.BackColor = System.Drawing.Color.Transparent
Me.lbXx_vettor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_vettor.Location = New System.Drawing.Point(149, 30)
Me.lbXx_vettor.Name = "lbXx_vettor"
Me.lbXx_vettor.NTSDbField = ""
Me.lbXx_vettor.Size = New System.Drawing.Size(212, 20)
Me.lbXx_vettor.TabIndex = 67
Me.lbXx_vettor.Tooltip = ""
Me.lbXx_vettor.UseMnemonic = false
'
'lbXx_contfatt
'
Me.lbXx_contfatt.BackColor = System.Drawing.Color.Transparent
Me.lbXx_contfatt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_contfatt.Location = New System.Drawing.Point(181, 77)
Me.lbXx_contfatt.Name = "lbXx_contfatt"
Me.lbXx_contfatt.NTSDbField = ""
Me.lbXx_contfatt.Size = New System.Drawing.Size(371, 20)
Me.lbXx_contfatt.TabIndex = 89
Me.lbXx_contfatt.Tooltip = ""
Me.lbXx_contfatt.UseMnemonic = false
'
'lbEt_vettor
'
Me.lbEt_vettor.AutoSize = true
Me.lbEt_vettor.BackColor = System.Drawing.Color.Transparent
Me.lbEt_vettor.Location = New System.Drawing.Point(3, 33)
Me.lbEt_vettor.Name = "lbEt_vettor"
Me.lbEt_vettor.NTSDbField = ""
Me.lbEt_vettor.Size = New System.Drawing.Size(43, 13)
Me.lbEt_vettor.TabIndex = 55
Me.lbEt_vettor.Text = "Vettore"
Me.lbEt_vettor.Tooltip = ""
Me.lbEt_vettor.UseMnemonic = false
'
'edEt_contfatt
'
Me.edEt_contfatt.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.edEt_contfatt.EditValue = "0"
Me.edEt_contfatt.Location = New System.Drawing.Point(95, 77)
Me.edEt_contfatt.Name = "edEt_contfatt"
Me.edEt_contfatt.NTSDbField = ""
Me.edEt_contfatt.NTSFormat = "0"
Me.edEt_contfatt.NTSForzaVisZoom = false
Me.edEt_contfatt.NTSOldValue = ""
Me.edEt_contfatt.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_contfatt.Properties.Appearance.Options.UseBackColor = true
Me.edEt_contfatt.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_contfatt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_contfatt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_contfatt.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_contfatt.Properties.AutoHeight = false
Me.edEt_contfatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_contfatt.Properties.MaxLength = 65536
Me.edEt_contfatt.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_contfatt.Size = New System.Drawing.Size(80, 20)
Me.edEt_contfatt.TabIndex = 88
'
'lbEt_contfatt
'
Me.lbEt_contfatt.AutoSize = true
Me.lbEt_contfatt.BackColor = System.Drawing.Color.Transparent
Me.lbEt_contfatt.Location = New System.Drawing.Point(3, 80)
Me.lbEt_contfatt.Name = "lbEt_contfatt"
Me.lbEt_contfatt.NTSDbField = ""
Me.lbEt_contfatt.Size = New System.Drawing.Size(82, 13)
Me.lbEt_contfatt.TabIndex = 87
Me.lbEt_contfatt.Text = "Conto fatturaz."
Me.lbEt_contfatt.Tooltip = ""
Me.lbEt_contfatt.UseMnemonic = false
'
'edEt_vettor
'
Me.edEt_vettor.Cursor = System.Windows.Forms.Cursors.Hand
Me.edEt_vettor.EditValue = "0"
Me.edEt_vettor.Location = New System.Drawing.Point(95, 30)
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
Me.edEt_vettor.TabIndex = 66
'
'lbXx_controp
'
Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_controp.Location = New System.Drawing.Point(412, 53)
Me.lbXx_controp.Name = "lbXx_controp"
Me.lbXx_controp.NTSDbField = ""
Me.lbXx_controp.Size = New System.Drawing.Size(140, 20)
Me.lbXx_controp.TabIndex = 85
Me.lbXx_controp.Tooltip = ""
Me.lbXx_controp.UseMnemonic = false
'
'lbXx_codese
'
Me.lbXx_codese.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codese.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codese.Location = New System.Drawing.Point(149, 53)
Me.lbXx_codese.Name = "lbXx_codese"
Me.lbXx_codese.NTSDbField = ""
Me.lbXx_codese.Size = New System.Drawing.Size(114, 20)
Me.lbXx_codese.TabIndex = 54
Me.lbXx_codese.Tooltip = ""
Me.lbXx_codese.UseMnemonic = false
'
'lbEt_controp
'
Me.lbEt_controp.AutoSize = true
Me.lbEt_controp.BackColor = System.Drawing.Color.Transparent
Me.lbEt_controp.Location = New System.Drawing.Point(269, 56)
Me.lbEt_controp.Name = "lbEt_controp"
Me.lbEt_controp.NTSDbField = ""
Me.lbEt_controp.Size = New System.Drawing.Size(76, 13)
Me.lbEt_controp.TabIndex = 84
Me.lbEt_controp.Text = "Cod. Controp."
Me.lbEt_controp.Tooltip = ""
Me.lbEt_controp.UseMnemonic = false
'
'lbEt_codese
'
Me.lbEt_codese.AutoSize = true
Me.lbEt_codese.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codese.Location = New System.Drawing.Point(3, 56)
Me.lbEt_codese.Name = "lbEt_codese"
Me.lbEt_codese.NTSDbField = ""
Me.lbEt_codese.Size = New System.Drawing.Size(81, 13)
Me.lbEt_codese.TabIndex = 3
Me.lbEt_codese.Text = "Cod. Esenzione"
Me.lbEt_codese.Tooltip = ""
Me.lbEt_codese.UseMnemonic = false
'
'edEt_controp
'
Me.edEt_controp.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_controp.EditValue = "0"
Me.edEt_controp.Location = New System.Drawing.Point(358, 53)
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
'edEt_codese
'
Me.edEt_codese.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codese.EditValue = "0"
Me.edEt_codese.Location = New System.Drawing.Point(95, 53)
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
'pnDatiTesta1
'
Me.pnDatiTesta1.AllowDrop = true
Me.pnDatiTesta1.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta1.Appearance.Options.UseBackColor = true
Me.pnDatiTesta1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta1.Controls.Add(Me.lbXx_organig)
Me.pnDatiTesta1.Controls.Add(Me.edEt_organig)
Me.pnDatiTesta1.Controls.Add(Me.lbReferente)
Me.pnDatiTesta1.Controls.Add(Me.lbet_datdoc)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_coddest)
Me.pnDatiTesta1.Controls.Add(Me.ckEt_sospeso)
Me.pnDatiTesta1.Controls.Add(Me.edet_datdoc)
Me.pnDatiTesta1.Controls.Add(Me.edEt_conto)
Me.pnDatiTesta1.Controls.Add(Me.ckEt_confermato)
Me.pnDatiTesta1.Controls.Add(Me.ckEt_flevas)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_coddest)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_conto)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_conto)
Me.pnDatiTesta1.Controls.Add(Me.edEt_coddest)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.ckEt_flstam)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.edEt_tipobf)
Me.pnDatiTesta1.Controls.Add(Me.cbEt_blocco)
Me.pnDatiTesta1.Controls.Add(Me.edEt_riferim)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_riferim)
Me.pnDatiTesta1.Controls.Add(Me.lbXx_codagen)
Me.pnDatiTesta1.Controls.Add(Me.lbEt_codagen)
Me.pnDatiTesta1.Controls.Add(Me.edEt_codagen)
Me.pnDatiTesta1.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta1.Location = New System.Drawing.Point(0, 0)
Me.pnDatiTesta1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta1.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta1.Name = "pnDatiTesta1"
Me.pnDatiTesta1.NTSActiveTrasparency = true
Me.pnDatiTesta1.Size = New System.Drawing.Size(569, 178)
Me.pnDatiTesta1.TabIndex = 68
'
'lbXx_organig
'
Me.lbXx_organig.BackColor = System.Drawing.Color.Transparent
Me.lbXx_organig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_organig.Location = New System.Drawing.Point(371, 105)
Me.lbXx_organig.Name = "lbXx_organig"
Me.lbXx_organig.NTSDbField = ""
Me.lbXx_organig.Size = New System.Drawing.Size(82, 20)
Me.lbXx_organig.TabIndex = 95
Me.lbXx_organig.Tooltip = ""
Me.lbXx_organig.UseMnemonic = false
'
'edEt_organig
'
Me.edEt_organig.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_organig.EditValue = "0"
Me.edEt_organig.Location = New System.Drawing.Point(318, 105)
Me.edEt_organig.Name = "edEt_organig"
Me.edEt_organig.NTSDbField = ""
Me.edEt_organig.NTSFormat = "0"
Me.edEt_organig.NTSForzaVisZoom = false
Me.edEt_organig.NTSOldValue = ""
Me.edEt_organig.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_organig.Properties.Appearance.Options.UseBackColor = true
Me.edEt_organig.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_organig.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_organig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_organig.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_organig.Properties.AutoHeight = false
Me.edEt_organig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_organig.Properties.MaxLength = 65536
Me.edEt_organig.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_organig.Size = New System.Drawing.Size(52, 20)
Me.edEt_organig.TabIndex = 94
'
'lbReferente
'
Me.lbReferente.AutoSize = true
Me.lbReferente.BackColor = System.Drawing.Color.Transparent
Me.lbReferente.Location = New System.Drawing.Point(264, 108)
Me.lbReferente.Name = "lbReferente"
Me.lbReferente.NTSDbField = ""
Me.lbReferente.Size = New System.Drawing.Size(56, 13)
Me.lbReferente.TabIndex = 93
Me.lbReferente.Text = "Referente"
Me.lbReferente.Tooltip = ""
Me.lbReferente.UseMnemonic = false
'
'ckEt_sospeso
'
Me.ckEt_sospeso.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_sospeso.Location = New System.Drawing.Point(459, 108)
Me.ckEt_sospeso.Name = "ckEt_sospeso"
Me.ckEt_sospeso.NTSCheckValue = "S"
Me.ckEt_sospeso.NTSUnCheckValue = "N"
Me.ckEt_sospeso.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_sospeso.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_sospeso.Properties.AutoHeight = false
Me.ckEt_sospeso.Properties.Caption = "S&ospeso"
Me.ckEt_sospeso.Size = New System.Drawing.Size(84, 19)
Me.ckEt_sospeso.TabIndex = 8
'
'ckEt_confermato
'
Me.ckEt_confermato.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_confermato.Location = New System.Drawing.Point(459, 83)
Me.ckEt_confermato.Name = "ckEt_confermato"
Me.ckEt_confermato.NTSCheckValue = "S"
Me.ckEt_confermato.NTSUnCheckValue = "N"
Me.ckEt_confermato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_confermato.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_confermato.Properties.AutoHeight = false
Me.ckEt_confermato.Properties.Caption = "Conf&ermato"
Me.ckEt_confermato.Size = New System.Drawing.Size(104, 19)
Me.ckEt_confermato.TabIndex = 4
'
'ckEt_flevas
'
Me.ckEt_flevas.Cursor = System.Windows.Forms.Cursors.SizeNWSE
Me.ckEt_flevas.Location = New System.Drawing.Point(459, 155)
Me.ckEt_flevas.Name = "ckEt_flevas"
Me.ckEt_flevas.NTSCheckValue = "S"
Me.ckEt_flevas.NTSUnCheckValue = "N"
Me.ckEt_flevas.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flevas.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flevas.Properties.AutoHeight = false
Me.ckEt_flevas.Properties.Caption = "Evaso"
Me.ckEt_flevas.Size = New System.Drawing.Size(68, 19)
Me.ckEt_flevas.TabIndex = 5
'
'lbXx_tipobf
'
Me.lbXx_tipobf.BackColor = System.Drawing.Color.Transparent
Me.lbXx_tipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_tipobf.Location = New System.Drawing.Point(149, 81)
Me.lbXx_tipobf.Name = "lbXx_tipobf"
Me.lbXx_tipobf.NTSDbField = ""
Me.lbXx_tipobf.Size = New System.Drawing.Size(304, 20)
Me.lbXx_tipobf.TabIndex = 49
Me.lbXx_tipobf.Tooltip = ""
Me.lbXx_tipobf.UseMnemonic = false
'
'ckEt_flstam
'
Me.ckEt_flstam.Cursor = System.Windows.Forms.Cursors.Default
Me.ckEt_flstam.Location = New System.Drawing.Point(459, 131)
Me.ckEt_flstam.Name = "ckEt_flstam"
Me.ckEt_flstam.NTSCheckValue = "S"
Me.ckEt_flstam.NTSUnCheckValue = "N"
Me.ckEt_flstam.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
Me.ckEt_flstam.Properties.Appearance.Options.UseBackColor = true
Me.ckEt_flstam.Properties.AutoHeight = false
Me.ckEt_flstam.Properties.Caption = "Stampato"
Me.ckEt_flstam.Size = New System.Drawing.Size(84, 19)
Me.ckEt_flstam.TabIndex = 82
'
'lbEt_tipobf
'
Me.lbEt_tipobf.AutoSize = true
Me.lbEt_tipobf.BackColor = System.Drawing.Color.Transparent
Me.lbEt_tipobf.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.lbEt_tipobf.Location = New System.Drawing.Point(3, 84)
Me.lbEt_tipobf.Name = "lbEt_tipobf"
Me.lbEt_tipobf.NTSDbField = ""
Me.lbEt_tipobf.Size = New System.Drawing.Size(91, 13)
Me.lbEt_tipobf.TabIndex = 0
Me.lbEt_tipobf.Text = "Tipo bolla/fatt."
Me.lbEt_tipobf.Tooltip = ""
Me.lbEt_tipobf.UseMnemonic = false
'
'edEt_tipobf
'
Me.edEt_tipobf.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_tipobf.EditValue = "0"
Me.edEt_tipobf.Location = New System.Drawing.Point(96, 81)
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
Me.edEt_tipobf.Size = New System.Drawing.Size(52, 20)
Me.edEt_tipobf.TabIndex = 47
'
'cbEt_blocco
'
Me.cbEt_blocco.Cursor = System.Windows.Forms.Cursors.Default
Me.cbEt_blocco.DataSource = Nothing
Me.cbEt_blocco.DisplayMember = ""
Me.cbEt_blocco.Location = New System.Drawing.Point(96, 129)
Me.cbEt_blocco.Name = "cbEt_blocco"
Me.cbEt_blocco.NTSDbField = ""
Me.cbEt_blocco.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.cbEt_blocco.Properties.Appearance.Options.UseBackColor = true
Me.cbEt_blocco.Properties.AutoHeight = false
Me.cbEt_blocco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
Me.cbEt_blocco.Properties.DropDownRows = 30
Me.cbEt_blocco.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
Me.cbEt_blocco.SelectedValue = ""
Me.cbEt_blocco.Size = New System.Drawing.Size(136, 20)
Me.cbEt_blocco.TabIndex = 68
Me.cbEt_blocco.ValueMember = ""
'
'edEt_riferim
'
Me.edEt_riferim.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_riferim.EditValue = ""
Me.edEt_riferim.Location = New System.Drawing.Point(96, 105)
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
Me.edEt_riferim.Size = New System.Drawing.Size(167, 20)
Me.edEt_riferim.TabIndex = 57
'
'lbEt_riferim
'
Me.lbEt_riferim.AutoSize = true
Me.lbEt_riferim.BackColor = System.Drawing.Color.Transparent
Me.lbEt_riferim.Location = New System.Drawing.Point(5, 108)
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
Me.lbXx_codagen.Location = New System.Drawing.Point(149, 155)
Me.lbXx_codagen.Name = "lbXx_codagen"
Me.lbXx_codagen.NTSDbField = ""
Me.lbXx_codagen.Size = New System.Drawing.Size(304, 20)
Me.lbXx_codagen.TabIndex = 50
Me.lbXx_codagen.Tooltip = ""
Me.lbXx_codagen.UseMnemonic = false
'
'lbEt_codagen
'
Me.lbEt_codagen.AutoSize = true
Me.lbEt_codagen.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codagen.Location = New System.Drawing.Point(5, 158)
Me.lbEt_codagen.Name = "lbEt_codagen"
Me.lbEt_codagen.NTSDbField = ""
Me.lbEt_codagen.Size = New System.Drawing.Size(68, 13)
Me.lbEt_codagen.TabIndex = 1
Me.lbEt_codagen.Text = "Cod. Agente"
Me.lbEt_codagen.Tooltip = ""
Me.lbEt_codagen.UseMnemonic = false
'
'edEt_codagen
'
Me.edEt_codagen.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codagen.EditValue = "0"
Me.edEt_codagen.Location = New System.Drawing.Point(96, 155)
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
'pnDatiTesta3
'
Me.pnDatiTesta3.AllowDrop = true
Me.pnDatiTesta3.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnDatiTesta3.Appearance.Options.UseBackColor = true
Me.pnDatiTesta3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnDatiTesta3.Controls.Add(Me.fmBanca)
Me.pnDatiTesta3.Controls.Add(Me.fmCa)
Me.pnDatiTesta3.Cursor = System.Windows.Forms.Cursors.Default
Me.pnDatiTesta3.Location = New System.Drawing.Point(0, 356)
Me.pnDatiTesta3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnDatiTesta3.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnDatiTesta3.Name = "pnDatiTesta3"
Me.pnDatiTesta3.NTSActiveTrasparency = true
Me.pnDatiTesta3.Size = New System.Drawing.Size(569, 219)
Me.pnDatiTesta3.TabIndex = 70
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
Me.fmBanca.Location = New System.Drawing.Point(3, 3)
Me.fmBanca.Name = "fmBanca"
Me.fmBanca.Size = New System.Drawing.Size(560, 92)
Me.fmBanca.TabIndex = 6
Me.fmBanca.Text = "Banca d'appoggio"
'
'lbXx_codbanc
'
Me.lbXx_codbanc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.lbXx_codbanc.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codbanc.Location = New System.Drawing.Point(178, 66)
Me.lbXx_codbanc.Name = "lbXx_codbanc"
Me.lbXx_codbanc.NTSDbField = ""
Me.lbXx_codbanc.Size = New System.Drawing.Size(371, 20)
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
Me.edEt_codbanc.Size = New System.Drawing.Size(84, 20)
Me.edEt_codbanc.TabIndex = 52
'
'lbEt_codbanc
'
Me.lbEt_codbanc.AutoSize = true
Me.lbEt_codbanc.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codbanc.Location = New System.Drawing.Point(4, 69)
Me.lbEt_codbanc.Name = "lbEt_codbanc"
Me.lbEt_codbanc.NTSDbField = ""
Me.lbEt_codbanc.Size = New System.Drawing.Size(55, 13)
Me.lbEt_codbanc.TabIndex = 51
Me.lbEt_codbanc.Text = "Ns. Banca"
Me.lbEt_codbanc.Tooltip = ""
Me.lbEt_codbanc.UseMnemonic = false
'
'edEt_banc2
'
Me.edEt_banc2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edEt_banc2.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_banc2.EditValue = ""
Me.edEt_banc2.Location = New System.Drawing.Point(178, 44)
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
Me.edEt_banc2.Size = New System.Drawing.Size(371, 20)
Me.edEt_banc2.TabIndex = 50
'
'edEt_banc1
'
Me.edEt_banc1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.edEt_banc1.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_banc1.EditValue = ""
Me.edEt_banc1.Location = New System.Drawing.Point(177, 22)
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
Me.edEt_banc1.Size = New System.Drawing.Size(372, 20)
Me.edEt_banc1.TabIndex = 49
'
'edEt_cab
'
Me.edEt_cab.Cursor = System.Windows.Forms.Cursors.SizeNWSE
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
Me.edEt_cab.Size = New System.Drawing.Size(83, 20)
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
Me.edEt_abi.Size = New System.Drawing.Size(83, 20)
Me.edEt_abi.TabIndex = 47
'
'lbEt_cab
'
Me.lbEt_cab.AutoSize = true
Me.lbEt_cab.BackColor = System.Drawing.Color.Transparent
Me.lbEt_cab.Location = New System.Drawing.Point(4, 47)
Me.lbEt_cab.Name = "lbEt_cab"
Me.lbEt_cab.NTSDbField = ""
Me.lbEt_cab.Size = New System.Drawing.Size(26, 13)
Me.lbEt_cab.TabIndex = 6
Me.lbEt_cab.Text = "Cab"
Me.lbEt_cab.Tooltip = ""
Me.lbEt_cab.UseMnemonic = false
'
'lbEt_abi
'
Me.lbEt_abi.AutoSize = true
Me.lbEt_abi.BackColor = System.Drawing.Color.Transparent
Me.lbEt_abi.Location = New System.Drawing.Point(4, 25)
Me.lbEt_abi.Name = "lbEt_abi"
Me.lbEt_abi.NTSDbField = ""
Me.lbEt_abi.Size = New System.Drawing.Size(22, 13)
Me.lbEt_abi.TabIndex = 5
Me.lbEt_abi.Text = "Abi"
Me.lbEt_abi.Tooltip = ""
Me.lbEt_abi.UseMnemonic = false
'
'fmCa
'
Me.fmCa.AllowDrop = true
Me.fmCa.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmCa.Appearance.Options.UseBackColor = true
Me.fmCa.Controls.Add(Me.lbXx_codcli)
Me.fmCa.Controls.Add(Me.edEt_codcli)
Me.fmCa.Controls.Add(Me.lbEt_codcli)
Me.fmCa.Controls.Add(Me.lbXx_coddivi)
Me.fmCa.Controls.Add(Me.edEt_coddivi)
Me.fmCa.Controls.Add(Me.lbEt_coddivi)
Me.fmCa.Controls.Add(Me.edEt_subcommeca)
Me.fmCa.Controls.Add(Me.lbXx_commeca)
Me.fmCa.Controls.Add(Me.lbXx_codcena)
Me.fmCa.Controls.Add(Me.edEt_codcena)
Me.fmCa.Controls.Add(Me.edEt_commeca)
Me.fmCa.Controls.Add(Me.lbEt_codcena)
Me.fmCa.Controls.Add(Me.lbEt_commeca)
Me.fmCa.Cursor = System.Windows.Forms.Cursors.Default
Me.fmCa.Location = New System.Drawing.Point(3, 99)
Me.fmCa.Name = "fmCa"
Me.fmCa.Size = New System.Drawing.Size(560, 116)
Me.fmCa.TabIndex = 8
Me.fmCa.Text = "Contabilità industriale"
'
'lbXx_codcli
'
Me.lbXx_codcli.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codcli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codcli.Location = New System.Drawing.Point(177, 91)
Me.lbXx_codcli.Name = "lbXx_codcli"
Me.lbXx_codcli.NTSDbField = ""
Me.lbXx_codcli.Size = New System.Drawing.Size(367, 20)
Me.lbXx_codcli.TabIndex = 69
Me.lbXx_codcli.Tooltip = ""
Me.lbXx_codcli.UseMnemonic = false
'
'edEt_codcli
'
Me.edEt_codcli.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codcli.EditValue = "0"
Me.edEt_codcli.Location = New System.Drawing.Point(88, 91)
Me.edEt_codcli.Name = "edEt_codcli"
Me.edEt_codcli.NTSDbField = ""
Me.edEt_codcli.NTSFormat = "0"
Me.edEt_codcli.NTSForzaVisZoom = false
Me.edEt_codcli.NTSOldValue = ""
Me.edEt_codcli.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codcli.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codcli.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codcli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codcli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codcli.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codcli.Properties.AutoHeight = false
Me.edEt_codcli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codcli.Properties.MaxLength = 65536
Me.edEt_codcli.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codcli.Size = New System.Drawing.Size(83, 20)
Me.edEt_codcli.TabIndex = 68
'
'lbEt_codcli
'
Me.lbEt_codcli.AutoSize = true
Me.lbEt_codcli.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codcli.Location = New System.Drawing.Point(4, 92)
Me.lbEt_codcli.Name = "lbEt_codcli"
Me.lbEt_codcli.NTSDbField = ""
Me.lbEt_codcli.Size = New System.Drawing.Size(61, 13)
Me.lbEt_codcli.TabIndex = 67
Me.lbEt_codcli.Text = "Cliente C/A"
Me.lbEt_codcli.Tooltip = ""
Me.lbEt_codcli.UseMnemonic = false
'
'lbXx_coddivi
'
Me.lbXx_coddivi.BackColor = System.Drawing.Color.Transparent
Me.lbXx_coddivi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_coddivi.Location = New System.Drawing.Point(143, 67)
Me.lbXx_coddivi.Name = "lbXx_coddivi"
Me.lbXx_coddivi.NTSDbField = ""
Me.lbXx_coddivi.Size = New System.Drawing.Size(401, 20)
Me.lbXx_coddivi.TabIndex = 66
Me.lbXx_coddivi.Tooltip = ""
Me.lbXx_coddivi.UseMnemonic = false
'
'edEt_coddivi
'
Me.edEt_coddivi.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_coddivi.EditValue = "0"
Me.edEt_coddivi.Location = New System.Drawing.Point(88, 67)
Me.edEt_coddivi.Name = "edEt_coddivi"
Me.edEt_coddivi.NTSDbField = ""
Me.edEt_coddivi.NTSFormat = "0"
Me.edEt_coddivi.NTSForzaVisZoom = false
Me.edEt_coddivi.NTSOldValue = ""
Me.edEt_coddivi.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_coddivi.Properties.Appearance.Options.UseBackColor = true
Me.edEt_coddivi.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_coddivi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_coddivi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_coddivi.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_coddivi.Properties.AutoHeight = false
Me.edEt_coddivi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_coddivi.Properties.MaxLength = 65536
Me.edEt_coddivi.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_coddivi.Size = New System.Drawing.Size(53, 20)
Me.edEt_coddivi.TabIndex = 65
'
'lbEt_coddivi
'
Me.lbEt_coddivi.AutoSize = true
Me.lbEt_coddivi.BackColor = System.Drawing.Color.Transparent
Me.lbEt_coddivi.Location = New System.Drawing.Point(4, 70)
Me.lbEt_coddivi.Name = "lbEt_coddivi"
Me.lbEt_coddivi.NTSDbField = ""
Me.lbEt_coddivi.Size = New System.Drawing.Size(49, 13)
Me.lbEt_coddivi.TabIndex = 64
Me.lbEt_coddivi.Text = "Divisione"
Me.lbEt_coddivi.Tooltip = ""
Me.lbEt_coddivi.UseMnemonic = false
'
'edEt_subcommeca
'
Me.edEt_subcommeca.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_subcommeca.EditValue = ""
Me.edEt_subcommeca.Location = New System.Drawing.Point(177, 21)
Me.edEt_subcommeca.Name = "edEt_subcommeca"
Me.edEt_subcommeca.NTSDbField = ""
Me.edEt_subcommeca.NTSForzaVisZoom = false
Me.edEt_subcommeca.NTSOldValue = ""
Me.edEt_subcommeca.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_subcommeca.Properties.Appearance.Options.UseBackColor = true
Me.edEt_subcommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_subcommeca.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_subcommeca.Properties.AutoHeight = false
Me.edEt_subcommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_subcommeca.Properties.MaxLength = 65536
Me.edEt_subcommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_subcommeca.Size = New System.Drawing.Size(35, 20)
Me.edEt_subcommeca.TabIndex = 57
'
'lbXx_commeca
'
Me.lbXx_commeca.BackColor = System.Drawing.Color.Transparent
Me.lbXx_commeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_commeca.Location = New System.Drawing.Point(218, 21)
Me.lbXx_commeca.Name = "lbXx_commeca"
Me.lbXx_commeca.NTSDbField = ""
Me.lbXx_commeca.Size = New System.Drawing.Size(327, 20)
Me.lbXx_commeca.TabIndex = 55
Me.lbXx_commeca.Tooltip = ""
Me.lbXx_commeca.UseMnemonic = false
'
'lbXx_codcena
'
Me.lbXx_codcena.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codcena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codcena.Location = New System.Drawing.Point(143, 43)
Me.lbXx_codcena.Name = "lbXx_codcena"
Me.lbXx_codcena.NTSDbField = ""
Me.lbXx_codcena.Size = New System.Drawing.Size(403, 20)
Me.lbXx_codcena.TabIndex = 53
Me.lbXx_codcena.Tooltip = ""
Me.lbXx_codcena.UseMnemonic = false
'
'edEt_codcena
'
Me.edEt_codcena.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_codcena.EditValue = "0"
Me.edEt_codcena.Location = New System.Drawing.Point(88, 43)
Me.edEt_codcena.Name = "edEt_codcena"
Me.edEt_codcena.NTSDbField = ""
Me.edEt_codcena.NTSFormat = "0"
Me.edEt_codcena.NTSForzaVisZoom = false
Me.edEt_codcena.NTSOldValue = ""
Me.edEt_codcena.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_codcena.Properties.Appearance.Options.UseBackColor = true
Me.edEt_codcena.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codcena.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codcena.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codcena.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codcena.Properties.AutoHeight = false
Me.edEt_codcena.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codcena.Properties.MaxLength = 65536
Me.edEt_codcena.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codcena.Size = New System.Drawing.Size(52, 20)
Me.edEt_codcena.TabIndex = 51
'
'edEt_commeca
'
Me.edEt_commeca.Cursor = System.Windows.Forms.Cursors.Default
Me.edEt_commeca.EditValue = "0"
Me.edEt_commeca.Location = New System.Drawing.Point(88, 21)
Me.edEt_commeca.Name = "edEt_commeca"
Me.edEt_commeca.NTSDbField = ""
Me.edEt_commeca.NTSFormat = "0"
Me.edEt_commeca.NTSForzaVisZoom = false
Me.edEt_commeca.NTSOldValue = ""
Me.edEt_commeca.Properties.Appearance.BackColor = System.Drawing.Color.White
Me.edEt_commeca.Properties.Appearance.Options.UseBackColor = true
Me.edEt_commeca.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_commeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_commeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_commeca.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_commeca.Properties.AutoHeight = false
Me.edEt_commeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_commeca.Properties.MaxLength = 65536
Me.edEt_commeca.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_commeca.Size = New System.Drawing.Size(83, 20)
Me.edEt_commeca.TabIndex = 47
'
'lbEt_codcena
'
Me.lbEt_codcena.AutoSize = true
Me.lbEt_codcena.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codcena.Location = New System.Drawing.Point(4, 46)
Me.lbEt_codcena.Name = "lbEt_codcena"
Me.lbEt_codcena.NTSDbField = ""
Me.lbEt_codcena.Size = New System.Drawing.Size(40, 13)
Me.lbEt_codcena.TabIndex = 7
Me.lbEt_codcena.Text = "Centro"
Me.lbEt_codcena.Tooltip = ""
Me.lbEt_codcena.UseMnemonic = false
'
'lbEt_commeca
'
Me.lbEt_commeca.AutoSize = true
Me.lbEt_commeca.BackColor = System.Drawing.Color.Transparent
Me.lbEt_commeca.Location = New System.Drawing.Point(4, 24)
Me.lbEt_commeca.Name = "lbEt_commeca"
Me.lbEt_commeca.NTSDbField = ""
Me.lbEt_commeca.Size = New System.Drawing.Size(88, 13)
Me.lbEt_commeca.TabIndex = 6
Me.lbEt_commeca.Text = "Commessa/subc."
Me.lbEt_commeca.Tooltip = ""
Me.lbEt_commeca.UseMnemonic = false
'
'pnTestataSx
'
Me.pnTestataSx.AllowDrop = true
Me.pnTestataSx.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnTestataSx.Appearance.Options.UseBackColor = true
Me.pnTestataSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnTestataSx.Controls.Add(Me.cmdSitcli)
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta4)
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta3)
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta2)
Me.pnTestataSx.Controls.Add(Me.cmdDatiTesta1)
Me.pnTestataSx.Cursor = System.Windows.Forms.Cursors.Default
Me.pnTestataSx.Dock = System.Windows.Forms.DockStyle.Left
Me.pnTestataSx.Location = New System.Drawing.Point(0, 0)
Me.pnTestataSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnTestataSx.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnTestataSx.Name = "pnTestataSx"
Me.pnTestataSx.NTSActiveTrasparency = true
Me.pnTestataSx.Size = New System.Drawing.Size(153, 384)
Me.pnTestataSx.TabIndex = 0
'
'cmdSitcli
'
Me.cmdSitcli.ImagePath = ""
Me.cmdSitcli.ImageText = ""
Me.cmdSitcli.Location = New System.Drawing.Point(8, 141)
Me.cmdSitcli.Name = "cmdSitcli"
Me.cmdSitcli.NTSContextMenu = Nothing
Me.cmdSitcli.Size = New System.Drawing.Size(131, 28)
Me.cmdSitcli.TabIndex = 126
Me.cmdSitcli.Text = "Situaz. cliente/fornitore"
'
'cmdDatiTesta4
'
Me.cmdDatiTesta4.ImagePath = ""
Me.cmdDatiTesta4.ImageText = ""
Me.cmdDatiTesta4.Location = New System.Drawing.Point(8, 107)
Me.cmdDatiTesta4.Name = "cmdDatiTesta4"
Me.cmdDatiTesta4.NTSContextMenu = Nothing
Me.cmdDatiTesta4.Size = New System.Drawing.Size(131, 28)
Me.cmdDatiTesta4.TabIndex = 125
Me.cmdDatiTesta4.Text = "Altri dati"
'
'cmdDatiTesta3
'
Me.cmdDatiTesta3.ImagePath = ""
Me.cmdDatiTesta3.ImageText = ""
Me.cmdDatiTesta3.Location = New System.Drawing.Point(8, 73)
Me.cmdDatiTesta3.Name = "cmdDatiTesta3"
Me.cmdDatiTesta3.NTSContextMenu = Nothing
Me.cmdDatiTesta3.Size = New System.Drawing.Size(131, 28)
Me.cmdDatiTesta3.TabIndex = 124
Me.cmdDatiTesta3.Text = "Banca / Cont. ind."
'
'cmdDatiTesta2
'
Me.cmdDatiTesta2.ImagePath = ""
Me.cmdDatiTesta2.ImageText = ""
Me.cmdDatiTesta2.Location = New System.Drawing.Point(8, 39)
Me.cmdDatiTesta2.Name = "cmdDatiTesta2"
Me.cmdDatiTesta2.NTSContextMenu = Nothing
Me.cmdDatiTesta2.Size = New System.Drawing.Size(131, 28)
Me.cmdDatiTesta2.TabIndex = 123
Me.cmdDatiTesta2.Text = "Cond. pag./ magaz."
'
'cmdDatiTesta1
'
Me.cmdDatiTesta1.ImagePath = ""
Me.cmdDatiTesta1.ImageText = ""
Me.cmdDatiTesta1.Location = New System.Drawing.Point(8, 5)
Me.cmdDatiTesta1.Name = "cmdDatiTesta1"
Me.cmdDatiTesta1.NTSContextMenu = Nothing
Me.cmdDatiTesta1.Size = New System.Drawing.Size(131, 28)
Me.cmdDatiTesta1.TabIndex = 122
Me.cmdDatiTesta1.Text = "Dati principali"
'
'tsGsor
'
Me.tsGsor.Dock = System.Windows.Forms.DockStyle.Fill
Me.tsGsor.Location = New System.Drawing.Point(0, 61)
Me.tsGsor.Name = "tsGsor"
Me.tsGsor.SelectedTabPage = Me.TabPage2
Me.tsGsor.Size = New System.Drawing.Size(731, 414)
Me.tsGsor.TabIndex = 45
Me.tsGsor.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage1, Me.TabPage2, Me.TabPage3, Me.TabPage4, Me.tpContratti})
'
'tpContratti
'
Me.tpContratti.AllowDrop = true
Me.tpContratti.Controls.Add(Me.pnContratti)
Me.tpContratti.Enable = true
Me.tpContratti.Name = "tpContratti"
Me.tpContratti.Size = New System.Drawing.Size(722, 384)
Me.tpContratti.Text = "&5 - Contratti"
'
'pnContratti
'
Me.pnContratti.AllowDrop = true
Me.pnContratti.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnContratti.Appearance.Options.UseBackColor = true
Me.pnContratti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnContratti.Controls.Add(Me.fmContratti)
Me.pnContratti.Controls.Add(Me.fmPromozioni)
Me.pnContratti.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnContratti.Location = New System.Drawing.Point(0, 0)
Me.pnContratti.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnContratti.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnContratti.Name = "pnContratti"
Me.pnContratti.NTSActiveTrasparency = true
Me.pnContratti.Size = New System.Drawing.Size(722, 384)
Me.pnContratti.TabIndex = 1
Me.pnContratti.Text = "NtsPanel1"
'
'fmContratti
'
Me.fmContratti.AllowDrop = true
Me.fmContratti.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmContratti.Appearance.Options.UseBackColor = true
Me.fmContratti.Controls.Add(Me.edEt_codtes)
Me.fmContratti.Controls.Add(Me.lbEt_codtes)
Me.fmContratti.Controls.Add(Me.pnPromoData)
Me.fmContratti.Controls.Add(Me.cmdCaricaContratti)
Me.fmContratti.Controls.Add(Me.cmdCancContratto6)
Me.fmContratti.Controls.Add(Me.lbEt_codtes2)
Me.fmContratti.Controls.Add(Me.cmdCancContratto5)
Me.fmContratti.Controls.Add(Me.lbEt_codtes3)
Me.fmContratti.Controls.Add(Me.cmdCancContratto4)
Me.fmContratti.Controls.Add(Me.lbEt_codtes4)
Me.fmContratti.Controls.Add(Me.cmdCancContratto3)
Me.fmContratti.Controls.Add(Me.lbEt_codtes5)
Me.fmContratti.Controls.Add(Me.cmdCancContratto2)
Me.fmContratti.Controls.Add(Me.lbEt_codtes6)
Me.fmContratti.Controls.Add(Me.cmdCancContratto1)
Me.fmContratti.Controls.Add(Me.edEt_codtes2)
Me.fmContratti.Controls.Add(Me.edEt_codtes3)
Me.fmContratti.Controls.Add(Me.edEt_codtes4)
Me.fmContratti.Controls.Add(Me.edEt_codtes5)
Me.fmContratti.Controls.Add(Me.lbXx_codtes6)
Me.fmContratti.Controls.Add(Me.edEt_codtes6)
Me.fmContratti.Controls.Add(Me.lbXx_codtes5)
Me.fmContratti.Controls.Add(Me.lbXx_codtes)
Me.fmContratti.Controls.Add(Me.lbXx_codtes4)
Me.fmContratti.Controls.Add(Me.lbXx_codtes2)
Me.fmContratti.Controls.Add(Me.lbXx_codtes3)
Me.fmContratti.Location = New System.Drawing.Point(3, 3)
Me.fmContratti.Name = "fmContratti"
Me.fmContratti.Size = New System.Drawing.Size(363, 377)
Me.fmContratti.TabIndex = 5
Me.fmContratti.Text = "Contratti applicati all'ordine"
'
'edEt_codtes
'
Me.edEt_codtes.EditValue = "0"
Me.edEt_codtes.Enabled = false
Me.edEt_codtes.Location = New System.Drawing.Point(77, 52)
Me.edEt_codtes.Name = "edEt_codtes"
Me.edEt_codtes.NTSDbField = ""
Me.edEt_codtes.NTSFormat = "0"
Me.edEt_codtes.NTSForzaVisZoom = true
Me.edEt_codtes.NTSOldValue = ""
Me.edEt_codtes.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes.Properties.AutoHeight = false
Me.edEt_codtes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes.Properties.MaxLength = 65536
Me.edEt_codtes.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes.TabIndex = 2
'
'lbEt_codtes
'
Me.lbEt_codtes.AutoSize = true
Me.lbEt_codtes.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes.Location = New System.Drawing.Point(6, 55)
Me.lbEt_codtes.Name = "lbEt_codtes"
Me.lbEt_codtes.NTSDbField = ""
Me.lbEt_codtes.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes.TabIndex = 0
Me.lbEt_codtes.Text = "Contratto 1"
Me.lbEt_codtes.Tooltip = ""
Me.lbEt_codtes.UseMnemonic = false
'
'pnPromoData
'
Me.pnPromoData.AllowDrop = true
Me.pnPromoData.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPromoData.Appearance.Options.UseBackColor = true
Me.pnPromoData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPromoData.Controls.Add(Me.lbEt_datpromo)
Me.pnPromoData.Controls.Add(Me.edEt_datpromo)
Me.pnPromoData.Controls.Add(Me.lbEt_orprom)
Me.pnPromoData.Controls.Add(Me.edEt_orpromo)
Me.pnPromoData.Dock = System.Windows.Forms.DockStyle.Top
Me.pnPromoData.Location = New System.Drawing.Point(2, 20)
Me.pnPromoData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPromoData.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPromoData.Name = "pnPromoData"
Me.pnPromoData.NTSActiveTrasparency = true
Me.pnPromoData.Size = New System.Drawing.Size(359, 27)
Me.pnPromoData.TabIndex = 5
Me.pnPromoData.Text = "NtsPanel1"
'
'lbEt_datpromo
'
Me.lbEt_datpromo.AutoSize = true
Me.lbEt_datpromo.BackColor = System.Drawing.Color.Transparent
Me.lbEt_datpromo.Location = New System.Drawing.Point(3, 7)
Me.lbEt_datpromo.Name = "lbEt_datpromo"
Me.lbEt_datpromo.NTSDbField = ""
Me.lbEt_datpromo.Size = New System.Drawing.Size(121, 13)
Me.lbEt_datpromo.TabIndex = 0
Me.lbEt_datpromo.Text = "Contratti validi alla data"
Me.lbEt_datpromo.Tooltip = ""
Me.lbEt_datpromo.UseMnemonic = false
'
'edEt_datpromo
'
Me.edEt_datpromo.EditValue = "01/01/1900"
Me.edEt_datpromo.Location = New System.Drawing.Point(134, 4)
Me.edEt_datpromo.Name = "edEt_datpromo"
Me.edEt_datpromo.NTSDbField = ""
Me.edEt_datpromo.NTSForzaVisZoom = true
Me.edEt_datpromo.NTSOldValue = ""
Me.edEt_datpromo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_datpromo.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_datpromo.Properties.AutoHeight = false
Me.edEt_datpromo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_datpromo.Properties.MaxLength = 65536
Me.edEt_datpromo.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_datpromo.Size = New System.Drawing.Size(81, 20)
Me.edEt_datpromo.TabIndex = 1
'
'lbEt_orprom
'
Me.lbEt_orprom.AutoSize = true
Me.lbEt_orprom.BackColor = System.Drawing.Color.Transparent
Me.lbEt_orprom.Location = New System.Drawing.Point(221, 7)
Me.lbEt_orprom.Name = "lbEt_orprom"
Me.lbEt_orprom.NTSDbField = ""
Me.lbEt_orprom.Size = New System.Drawing.Size(23, 13)
Me.lbEt_orprom.TabIndex = 0
Me.lbEt_orprom.Text = "ora"
Me.lbEt_orprom.Tooltip = ""
Me.lbEt_orprom.UseMnemonic = false
'
'edEt_orpromo
'
Me.edEt_orpromo.EditValue = "0"
Me.edEt_orpromo.Location = New System.Drawing.Point(245, 4)
Me.edEt_orpromo.Name = "edEt_orpromo"
Me.edEt_orpromo.NTSDbField = ""
Me.edEt_orpromo.NTSFormat = "0"
Me.edEt_orpromo.NTSForzaVisZoom = true
Me.edEt_orpromo.NTSOldValue = ""
Me.edEt_orpromo.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_orpromo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_orpromo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_orpromo.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_orpromo.Properties.AutoHeight = false
Me.edEt_orpromo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_orpromo.Properties.MaxLength = 65536
Me.edEt_orpromo.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_orpromo.Size = New System.Drawing.Size(67, 20)
Me.edEt_orpromo.TabIndex = 2
'
'cmdCaricaContratti
'
Me.cmdCaricaContratti.ImagePath = ""
Me.cmdCaricaContratti.ImageText = ""
Me.cmdCaricaContratti.Location = New System.Drawing.Point(77, 211)
Me.cmdCaricaContratti.Name = "cmdCaricaContratti"
Me.cmdCaricaContratti.NTSContextMenu = Nothing
Me.cmdCaricaContratti.Size = New System.Drawing.Size(197, 25)
Me.cmdCaricaContratti.TabIndex = 4
Me.cmdCaricaContratti.Text = "Ricarica Contratti"
'
'cmdCancContratto6
'
Me.cmdCancContratto6.Image = CType(resources.GetObject("cmdCancContratto6.Image"),System.Drawing.Image)
Me.cmdCancContratto6.ImagePath = ""
Me.cmdCancContratto6.ImageText = ""
Me.cmdCancContratto6.Location = New System.Drawing.Point(333, 180)
Me.cmdCancContratto6.Name = "cmdCancContratto6"
Me.cmdCancContratto6.NTSContextMenu = Nothing
Me.cmdCancContratto6.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto6.TabIndex = 4
'
'lbEt_codtes2
'
Me.lbEt_codtes2.AutoSize = true
Me.lbEt_codtes2.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes2.Location = New System.Drawing.Point(6, 81)
Me.lbEt_codtes2.Name = "lbEt_codtes2"
Me.lbEt_codtes2.NTSDbField = ""
Me.lbEt_codtes2.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes2.TabIndex = 0
Me.lbEt_codtes2.Text = "Contratto 2"
Me.lbEt_codtes2.Tooltip = ""
Me.lbEt_codtes2.UseMnemonic = false
'
'cmdCancContratto5
'
Me.cmdCancContratto5.Image = CType(resources.GetObject("cmdCancContratto5.Image"),System.Drawing.Image)
Me.cmdCancContratto5.ImagePath = ""
Me.cmdCancContratto5.ImageText = ""
Me.cmdCancContratto5.Location = New System.Drawing.Point(333, 154)
Me.cmdCancContratto5.Name = "cmdCancContratto5"
Me.cmdCancContratto5.NTSContextMenu = Nothing
Me.cmdCancContratto5.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto5.TabIndex = 4
'
'lbEt_codtes3
'
Me.lbEt_codtes3.AutoSize = true
Me.lbEt_codtes3.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes3.Location = New System.Drawing.Point(6, 107)
Me.lbEt_codtes3.Name = "lbEt_codtes3"
Me.lbEt_codtes3.NTSDbField = ""
Me.lbEt_codtes3.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes3.TabIndex = 0
Me.lbEt_codtes3.Text = "Contratto 3"
Me.lbEt_codtes3.Tooltip = ""
Me.lbEt_codtes3.UseMnemonic = false
'
'cmdCancContratto4
'
Me.cmdCancContratto4.Image = CType(resources.GetObject("cmdCancContratto4.Image"),System.Drawing.Image)
Me.cmdCancContratto4.ImagePath = ""
Me.cmdCancContratto4.ImageText = ""
Me.cmdCancContratto4.Location = New System.Drawing.Point(333, 128)
Me.cmdCancContratto4.Name = "cmdCancContratto4"
Me.cmdCancContratto4.NTSContextMenu = Nothing
Me.cmdCancContratto4.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto4.TabIndex = 4
'
'lbEt_codtes4
'
Me.lbEt_codtes4.AutoSize = true
Me.lbEt_codtes4.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes4.Location = New System.Drawing.Point(6, 133)
Me.lbEt_codtes4.Name = "lbEt_codtes4"
Me.lbEt_codtes4.NTSDbField = ""
Me.lbEt_codtes4.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes4.TabIndex = 0
Me.lbEt_codtes4.Text = "Contratto 4"
Me.lbEt_codtes4.Tooltip = ""
Me.lbEt_codtes4.UseMnemonic = false
'
'cmdCancContratto3
'
Me.cmdCancContratto3.Image = CType(resources.GetObject("cmdCancContratto3.Image"),System.Drawing.Image)
Me.cmdCancContratto3.ImagePath = ""
Me.cmdCancContratto3.ImageText = ""
Me.cmdCancContratto3.Location = New System.Drawing.Point(333, 102)
Me.cmdCancContratto3.Name = "cmdCancContratto3"
Me.cmdCancContratto3.NTSContextMenu = Nothing
Me.cmdCancContratto3.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto3.TabIndex = 4
'
'lbEt_codtes5
'
Me.lbEt_codtes5.AutoSize = true
Me.lbEt_codtes5.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes5.Location = New System.Drawing.Point(6, 159)
Me.lbEt_codtes5.Name = "lbEt_codtes5"
Me.lbEt_codtes5.NTSDbField = ""
Me.lbEt_codtes5.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes5.TabIndex = 0
Me.lbEt_codtes5.Text = "Contratto 5"
Me.lbEt_codtes5.Tooltip = ""
Me.lbEt_codtes5.UseMnemonic = false
'
'cmdCancContratto2
'
Me.cmdCancContratto2.Image = CType(resources.GetObject("cmdCancContratto2.Image"),System.Drawing.Image)
Me.cmdCancContratto2.ImagePath = ""
Me.cmdCancContratto2.ImageText = ""
Me.cmdCancContratto2.Location = New System.Drawing.Point(333, 76)
Me.cmdCancContratto2.Name = "cmdCancContratto2"
Me.cmdCancContratto2.NTSContextMenu = Nothing
Me.cmdCancContratto2.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto2.TabIndex = 4
'
'lbEt_codtes6
'
Me.lbEt_codtes6.AutoSize = true
Me.lbEt_codtes6.BackColor = System.Drawing.Color.Transparent
Me.lbEt_codtes6.Location = New System.Drawing.Point(6, 185)
Me.lbEt_codtes6.Name = "lbEt_codtes6"
Me.lbEt_codtes6.NTSDbField = ""
Me.lbEt_codtes6.Size = New System.Drawing.Size(63, 13)
Me.lbEt_codtes6.TabIndex = 0
Me.lbEt_codtes6.Text = "Contratto 6"
Me.lbEt_codtes6.Tooltip = ""
Me.lbEt_codtes6.UseMnemonic = false
'
'cmdCancContratto1
'
Me.cmdCancContratto1.Image = CType(resources.GetObject("cmdCancContratto1.Image"),System.Drawing.Image)
Me.cmdCancContratto1.ImagePath = ""
Me.cmdCancContratto1.ImageText = ""
Me.cmdCancContratto1.Location = New System.Drawing.Point(333, 50)
Me.cmdCancContratto1.Name = "cmdCancContratto1"
Me.cmdCancContratto1.NTSContextMenu = Nothing
Me.cmdCancContratto1.Size = New System.Drawing.Size(24, 24)
Me.cmdCancContratto1.TabIndex = 4
'
'edEt_codtes2
'
Me.edEt_codtes2.EditValue = "0"
Me.edEt_codtes2.Enabled = false
Me.edEt_codtes2.Location = New System.Drawing.Point(77, 78)
Me.edEt_codtes2.Name = "edEt_codtes2"
Me.edEt_codtes2.NTSDbField = ""
Me.edEt_codtes2.NTSFormat = "0"
Me.edEt_codtes2.NTSForzaVisZoom = true
Me.edEt_codtes2.NTSOldValue = ""
Me.edEt_codtes2.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes2.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes2.Properties.AutoHeight = false
Me.edEt_codtes2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes2.Properties.MaxLength = 65536
Me.edEt_codtes2.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes2.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes2.TabIndex = 2
'
'edEt_codtes3
'
Me.edEt_codtes3.EditValue = "0"
Me.edEt_codtes3.Enabled = false
Me.edEt_codtes3.Location = New System.Drawing.Point(77, 104)
Me.edEt_codtes3.Name = "edEt_codtes3"
Me.edEt_codtes3.NTSDbField = ""
Me.edEt_codtes3.NTSFormat = "0"
Me.edEt_codtes3.NTSForzaVisZoom = true
Me.edEt_codtes3.NTSOldValue = ""
Me.edEt_codtes3.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes3.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes3.Properties.AutoHeight = false
Me.edEt_codtes3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes3.Properties.MaxLength = 65536
Me.edEt_codtes3.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes3.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes3.TabIndex = 2
'
'edEt_codtes4
'
Me.edEt_codtes4.EditValue = "0"
Me.edEt_codtes4.Enabled = false
Me.edEt_codtes4.Location = New System.Drawing.Point(77, 130)
Me.edEt_codtes4.Name = "edEt_codtes4"
Me.edEt_codtes4.NTSDbField = ""
Me.edEt_codtes4.NTSFormat = "0"
Me.edEt_codtes4.NTSForzaVisZoom = true
Me.edEt_codtes4.NTSOldValue = ""
Me.edEt_codtes4.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes4.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes4.Properties.AutoHeight = false
Me.edEt_codtes4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes4.Properties.MaxLength = 65536
Me.edEt_codtes4.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes4.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes4.TabIndex = 2
'
'edEt_codtes5
'
Me.edEt_codtes5.EditValue = "0"
Me.edEt_codtes5.Enabled = false
Me.edEt_codtes5.Location = New System.Drawing.Point(77, 156)
Me.edEt_codtes5.Name = "edEt_codtes5"
Me.edEt_codtes5.NTSDbField = ""
Me.edEt_codtes5.NTSFormat = "0"
Me.edEt_codtes5.NTSForzaVisZoom = true
Me.edEt_codtes5.NTSOldValue = ""
Me.edEt_codtes5.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes5.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes5.Properties.AutoHeight = false
Me.edEt_codtes5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes5.Properties.MaxLength = 65536
Me.edEt_codtes5.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes5.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes5.TabIndex = 2
'
'lbXx_codtes6
'
Me.lbXx_codtes6.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes6.Location = New System.Drawing.Point(136, 182)
Me.lbXx_codtes6.Name = "lbXx_codtes6"
Me.lbXx_codtes6.NTSDbField = ""
Me.lbXx_codtes6.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes6.TabIndex = 3
Me.lbXx_codtes6.Tooltip = ""
Me.lbXx_codtes6.UseMnemonic = false
'
'edEt_codtes6
'
Me.edEt_codtes6.EditValue = "0"
Me.edEt_codtes6.Enabled = false
Me.edEt_codtes6.Location = New System.Drawing.Point(77, 182)
Me.edEt_codtes6.Name = "edEt_codtes6"
Me.edEt_codtes6.NTSDbField = ""
Me.edEt_codtes6.NTSFormat = "0"
Me.edEt_codtes6.NTSForzaVisZoom = true
Me.edEt_codtes6.NTSOldValue = ""
Me.edEt_codtes6.Properties.Appearance.Options.UseTextOptions = true
Me.edEt_codtes6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
Me.edEt_codtes6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
Me.edEt_codtes6.Properties.AppearanceDisabled.Options.UseForeColor = true
Me.edEt_codtes6.Properties.AutoHeight = false
Me.edEt_codtes6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
Me.edEt_codtes6.Properties.MaxLength = 65536
Me.edEt_codtes6.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.edEt_codtes6.Size = New System.Drawing.Size(56, 20)
Me.edEt_codtes6.TabIndex = 2
'
'lbXx_codtes5
'
Me.lbXx_codtes5.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes5.Location = New System.Drawing.Point(136, 156)
Me.lbXx_codtes5.Name = "lbXx_codtes5"
Me.lbXx_codtes5.NTSDbField = ""
Me.lbXx_codtes5.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes5.TabIndex = 3
Me.lbXx_codtes5.Tooltip = ""
Me.lbXx_codtes5.UseMnemonic = false
'
'lbXx_codtes
'
Me.lbXx_codtes.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes.Location = New System.Drawing.Point(136, 52)
Me.lbXx_codtes.Name = "lbXx_codtes"
Me.lbXx_codtes.NTSDbField = ""
Me.lbXx_codtes.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes.TabIndex = 3
Me.lbXx_codtes.Tooltip = ""
Me.lbXx_codtes.UseMnemonic = false
'
'lbXx_codtes4
'
Me.lbXx_codtes4.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes4.Location = New System.Drawing.Point(136, 130)
Me.lbXx_codtes4.Name = "lbXx_codtes4"
Me.lbXx_codtes4.NTSDbField = ""
Me.lbXx_codtes4.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes4.TabIndex = 3
Me.lbXx_codtes4.Tooltip = ""
Me.lbXx_codtes4.UseMnemonic = false
'
'lbXx_codtes2
'
Me.lbXx_codtes2.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes2.Location = New System.Drawing.Point(136, 78)
Me.lbXx_codtes2.Name = "lbXx_codtes2"
Me.lbXx_codtes2.NTSDbField = ""
Me.lbXx_codtes2.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes2.TabIndex = 3
Me.lbXx_codtes2.Tooltip = ""
Me.lbXx_codtes2.UseMnemonic = false
'
'lbXx_codtes3
'
Me.lbXx_codtes3.BackColor = System.Drawing.Color.Transparent
Me.lbXx_codtes3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.lbXx_codtes3.Location = New System.Drawing.Point(136, 104)
Me.lbXx_codtes3.Name = "lbXx_codtes3"
Me.lbXx_codtes3.NTSDbField = ""
Me.lbXx_codtes3.Size = New System.Drawing.Size(190, 20)
Me.lbXx_codtes3.TabIndex = 3
Me.lbXx_codtes3.Tooltip = ""
Me.lbXx_codtes3.UseMnemonic = false
'
'fmPromozioni
'
Me.fmPromozioni.AllowDrop = true
Me.fmPromozioni.Appearance.BackColor = System.Drawing.Color.Transparent
Me.fmPromozioni.Appearance.Options.UseBackColor = true
Me.fmPromozioni.Controls.Add(Me.grPromo)
Me.fmPromozioni.Controls.Add(Me.pnPromoPulsanti)
Me.fmPromozioni.Location = New System.Drawing.Point(369, 3)
Me.fmPromozioni.Name = "fmPromozioni"
Me.fmPromozioni.Size = New System.Drawing.Size(350, 377)
Me.fmPromozioni.TabIndex = 5
Me.fmPromozioni.Text = "Promozioni derivate dai contratti applicati"
'
'grPromo
'
Me.grPromo.Dock = System.Windows.Forms.DockStyle.Fill
Me.grPromo.EmbeddedNavigator.Name = ""
Me.grPromo.Location = New System.Drawing.Point(2, 20)
Me.grPromo.MainView = Me.grvPromo
Me.grPromo.Name = "grPromo"
Me.grPromo.Size = New System.Drawing.Size(346, 323)
Me.grPromo.TabIndex = 5
Me.grPromo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPromo})
'
'grvPromo
'
Me.grvPromo.ActiveFilterEnabled = false
Me.grvPromo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codrepr, Me.tb_desrepr, Me.xx_contratto, Me.xx_prior})
Me.grvPromo.Enabled = true
Me.grvPromo.GridControl = Me.grPromo
Me.grvPromo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
Me.grvPromo.MinRowHeight = 14
Me.grvPromo.Name = "grvPromo"
Me.grvPromo.NTSAllowDelete = true
Me.grvPromo.NTSAllowInsert = true
Me.grvPromo.NTSAllowUpdate = true
Me.grvPromo.NTSMenuContext = Nothing
Me.grvPromo.OptionsCustomization.AllowRowSizing = true
Me.grvPromo.OptionsFilter.AllowFilterEditor = false
Me.grvPromo.OptionsNavigation.EnterMoveNextColumn = true
Me.grvPromo.OptionsNavigation.UseTabKey = false
Me.grvPromo.OptionsSelection.EnableAppearanceFocusedRow = false
Me.grvPromo.OptionsView.ColumnAutoWidth = false
Me.grvPromo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
Me.grvPromo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
Me.grvPromo.OptionsView.ShowGroupPanel = false
Me.grvPromo.RowHeight = 14
'
'tb_codrepr
'
Me.tb_codrepr.AppearanceCell.Options.UseBackColor = true
Me.tb_codrepr.AppearanceCell.Options.UseTextOptions = true
Me.tb_codrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.tb_codrepr.Caption = "Promozione"
Me.tb_codrepr.Enabled = true
Me.tb_codrepr.FieldName = "tb_codrepr"
Me.tb_codrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.tb_codrepr.Name = "tb_codrepr"
Me.tb_codrepr.NTSRepositoryComboBox = Nothing
Me.tb_codrepr.NTSRepositoryItemCheck = Nothing
Me.tb_codrepr.NTSRepositoryItemMemo = Nothing
Me.tb_codrepr.NTSRepositoryItemText = Nothing
Me.tb_codrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.tb_codrepr.OptionsFilter.AllowFilter = false
Me.tb_codrepr.Visible = true
Me.tb_codrepr.VisibleIndex = 0
'
'tb_desrepr
'
Me.tb_desrepr.AppearanceCell.Options.UseBackColor = true
Me.tb_desrepr.AppearanceCell.Options.UseTextOptions = true
Me.tb_desrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.tb_desrepr.Caption = "Descr. Promozione"
Me.tb_desrepr.Enabled = true
Me.tb_desrepr.FieldName = "tb_desrepr"
Me.tb_desrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.tb_desrepr.Name = "tb_desrepr"
Me.tb_desrepr.NTSRepositoryComboBox = Nothing
Me.tb_desrepr.NTSRepositoryItemCheck = Nothing
Me.tb_desrepr.NTSRepositoryItemMemo = Nothing
Me.tb_desrepr.NTSRepositoryItemText = Nothing
Me.tb_desrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.tb_desrepr.OptionsFilter.AllowFilter = false
Me.tb_desrepr.Visible = true
Me.tb_desrepr.VisibleIndex = 1
'
'xx_contratto
'
Me.xx_contratto.AppearanceCell.Options.UseBackColor = true
Me.xx_contratto.AppearanceCell.Options.UseTextOptions = true
Me.xx_contratto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xx_contratto.Caption = "Contratto"
Me.xx_contratto.Enabled = true
Me.xx_contratto.FieldName = "xx_contratto"
Me.xx_contratto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xx_contratto.Name = "xx_contratto"
Me.xx_contratto.NTSRepositoryComboBox = Nothing
Me.xx_contratto.NTSRepositoryItemCheck = Nothing
Me.xx_contratto.NTSRepositoryItemMemo = Nothing
Me.xx_contratto.NTSRepositoryItemText = Nothing
Me.xx_contratto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xx_contratto.OptionsFilter.AllowFilter = false
Me.xx_contratto.Visible = true
Me.xx_contratto.VisibleIndex = 2
'
'xx_prior
'
Me.xx_prior.AppearanceCell.Options.UseBackColor = true
Me.xx_prior.AppearanceCell.Options.UseTextOptions = true
Me.xx_prior.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
Me.xx_prior.Caption = "Priorità"
Me.xx_prior.Enabled = true
Me.xx_prior.FieldName = "xx_prior"
Me.xx_prior.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
Me.xx_prior.Name = "xx_prior"
Me.xx_prior.NTSRepositoryComboBox = Nothing
Me.xx_prior.NTSRepositoryItemCheck = Nothing
Me.xx_prior.NTSRepositoryItemMemo = Nothing
Me.xx_prior.NTSRepositoryItemText = Nothing
Me.xx_prior.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
Me.xx_prior.OptionsFilter.AllowFilter = false
Me.xx_prior.Visible = true
Me.xx_prior.VisibleIndex = 3
'
'pnPromoPulsanti
'
Me.pnPromoPulsanti.AllowDrop = true
Me.pnPromoPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
Me.pnPromoPulsanti.Appearance.Options.UseBackColor = true
Me.pnPromoPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
Me.pnPromoPulsanti.Controls.Add(Me.cmdAnnullaPromo)
Me.pnPromoPulsanti.Controls.Add(Me.cmdApplicaPromo)
Me.pnPromoPulsanti.Dock = System.Windows.Forms.DockStyle.Bottom
Me.pnPromoPulsanti.Location = New System.Drawing.Point(2, 343)
Me.pnPromoPulsanti.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
Me.pnPromoPulsanti.LookAndFeel.UseDefaultLookAndFeel = false
Me.pnPromoPulsanti.Name = "pnPromoPulsanti"
Me.pnPromoPulsanti.NTSActiveTrasparency = true
Me.pnPromoPulsanti.Size = New System.Drawing.Size(346, 32)
Me.pnPromoPulsanti.TabIndex = 5
Me.pnPromoPulsanti.Text = "NtsPanel1"
'
'cmdAnnullaPromo
'
Me.cmdAnnullaPromo.ImagePath = ""
Me.cmdAnnullaPromo.ImageText = ""
Me.cmdAnnullaPromo.Location = New System.Drawing.Point(3, 4)
Me.cmdAnnullaPromo.Name = "cmdAnnullaPromo"
Me.cmdAnnullaPromo.NTSContextMenu = Nothing
Me.cmdAnnullaPromo.Size = New System.Drawing.Size(148, 25)
Me.cmdAnnullaPromo.TabIndex = 4
Me.cmdAnnullaPromo.Text = "Annulla promozioni"
'
'cmdApplicaPromo
'
Me.cmdApplicaPromo.ImagePath = ""
Me.cmdApplicaPromo.ImageText = ""
Me.cmdApplicaPromo.Location = New System.Drawing.Point(195, 4)
Me.cmdApplicaPromo.Name = "cmdApplicaPromo"
Me.cmdApplicaPromo.NTSContextMenu = Nothing
Me.cmdApplicaPromo.Size = New System.Drawing.Size(148, 25)
Me.cmdApplicaPromo.TabIndex = 4
Me.cmdApplicaPromo.Text = "Applica promozioni"
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
Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRecordCancella, Me.tlbRecordRipristina, Me.tlbMnuRecord, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaWord, Me.tlbStampaPdf, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbNuovoOrdineDaPrec, Me.tlbStampaEtichetteRiga, Me.tlbStampaEtichetteFinale, Me.tlbDatiContabiliSintetici, Me.tlbMnuFile, Me.tlbNoteRiga, Me.tlbSelezLottoUbicaz, Me.tlbDettaglioTCO, Me.tlbSelOFA, Me.tlbSelOfferta, Me.tlbApriConfiguratore, Me.tlbRicaricaGreg, Me.tlbModifNumOrd, Me.tlbImpostaFileTerm, Me.tlbApriChiamata, Me.tlbOggettiAssociati, Me.tlbNavigazMrp, Me.tlbPersVend, Me.tlbPersAcq, Me.tlbNumerazioni, Me.tlbTabBolli, Me.tlbRicalcolaColliQta, Me.tlbZoomBanche, Me.tlbZoomRigheOff, Me.tlbCopiaRiga, Me.tlbIncollaRiga, Me.tlbSeleziona, Me.tlbSelezDaTerm, Me.tlbSelezArtAcc, Me.tlbSelezArtSucc, Me.tlbImpCollegati, Me.tlbLavCollegate, Me.tlbApriDB, Me.tlbDettQtaDate, Me.tlbPrecMov, Me.tlbPrecOrd, Me.tlbGenNumComm, Me.tlbRicalcolaListini, Me.tlbSalvaCondPart, Me.tlbVisDispComm, Me.tlbVisDispVariante, Me.tlbVisDettTCO, Me.tlbInsRiga, Me.tlbNavigazDoc, Me.tlbVisDettEvasione, Me.tlbApriDocCollegati, Me.tlbApriOrdineCollegato, Me.tlbApriOffertaCollegata, Me.tlbSchedatrasporto, Me.tlbOrdinaRighe, Me.tlbOrdinaRigheRiga, Me.tlbOrdinaRigheCodiceArticolo, Me.tlbOrdinaRigheDescrizione, Me.tlbOrdinaRigheCommessa, Me.tlbEmail, Me.tlbCambioDitta, Me.tlbSalvanuovo, Me.tlbRiapriDopoSalva, Me.tlbImport, Me.tlbExport, Me.tlbSelectDeselect, Me.tlbDaLista, Me.tlbRiordinarighe, Me.tlbInsRigheDaFigliDB, Me.tlbApriDocPrec, Me.tlbApriDocSucc, Me.tlbCancRigheSel, Me.tlbSkype, Me.tlbRicalcPrzScnPro})
Me.NtsBarManager1.MaxItemId = 98
'
'tlbMain
'
Me.tlbMain.BarName = "tlbMain"
Me.tlbMain.DockCol = 0
Me.tlbMain.DockRow = 0
Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDocPrec, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDocSucc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelOfferta, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettaglioTCO), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMnuFile, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMnuRecord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
Me.tlbApriDocPrec.Id = 92
Me.tlbApriDocPrec.Name = "tlbApriDocPrec"
Me.tlbApriDocPrec.Visible = true
'
'tlbApriDocSucc
'
Me.tlbApriDocSucc.Caption = "Passa a documento successivo"
Me.tlbApriDocSucc.Glyph = CType(resources.GetObject("tlbApriDocSucc.Glyph"),System.Drawing.Image)
Me.tlbApriDocSucc.GlyphPath = ""
Me.tlbApriDocSucc.Id = 93
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
'tlbSelOfferta
'
Me.tlbSelOfferta.Caption = "Seleziona offerte"
Me.tlbSelOfferta.Glyph = CType(resources.GetObject("tlbSelOfferta.Glyph"),System.Drawing.Image)
Me.tlbSelOfferta.GlyphPath = ""
Me.tlbSelOfferta.Id = 30
Me.tlbSelOfferta.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F4))
Me.tlbSelOfferta.Name = "tlbSelOfferta"
Me.tlbSelOfferta.Visible = true
'
'tlbDettaglioTCO
'
Me.tlbDettaglioTCO.Caption = "Dettaglio quantità 'Taglie e colori'"
Me.tlbDettaglioTCO.Glyph = CType(resources.GetObject("tlbDettaglioTCO.Glyph"),System.Drawing.Image)
Me.tlbDettaglioTCO.GlyphPath = ""
Me.tlbDettaglioTCO.Id = 28
Me.tlbDettaglioTCO.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
Me.tlbDettaglioTCO.Name = "tlbDettaglioTCO"
Me.tlbDettaglioTCO.Visible = true
'
'tlbMnuFile
'
Me.tlbMnuFile.Caption = "&Testata"
Me.tlbMnuFile.GlyphPath = ""
Me.tlbMnuFile.Id = 24
Me.tlbMnuFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovoOrdineDaPrec), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDatiContabiliSintetici), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbModifNumOrd), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcPrzScnPro), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoomBanche, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDettTCO), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGenNumComm, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriChiamata, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOggettiAssociati), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSchedatrasporto, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEmail), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSkype)})
Me.tlbMnuFile.Name = "tlbMnuFile"
Me.tlbMnuFile.Visible = true
'
'tlbNuovoOrdineDaPrec
'
Me.tlbNuovoOrdineDaPrec.Caption = "Nuovo Ordine da precedente"
Me.tlbNuovoOrdineDaPrec.GlyphPath = ""
Me.tlbNuovoOrdineDaPrec.Id = 17
Me.tlbNuovoOrdineDaPrec.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
Me.tlbNuovoOrdineDaPrec.Name = "tlbNuovoOrdineDaPrec"
Me.tlbNuovoOrdineDaPrec.Visible = true
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
'tlbModifNumOrd
'
Me.tlbModifNumOrd.Caption = "Modifica numero ordine"
Me.tlbModifNumOrd.GlyphPath = ""
Me.tlbModifNumOrd.Id = 33
Me.tlbModifNumOrd.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
Me.tlbModifNumOrd.Name = "tlbModifNumOrd"
Me.tlbModifNumOrd.NTSIsCheckBox = false
Me.tlbModifNumOrd.Visible = true
'
'tlbRicalcPrzScnPro
'
Me.tlbRicalcPrzScnPro.Caption = "Ricalcola prezzi/sconti/provvigioni"
Me.tlbRicalcPrzScnPro.GlyphPath = ""
Me.tlbRicalcPrzScnPro.Id = 97
Me.tlbRicalcPrzScnPro.Name = "tlbRicalcPrzScnPro"
Me.tlbRicalcPrzScnPro.Visible = true
'
'tlbZoomBanche
'
Me.tlbZoomBanche.Caption = "Zoom banche"
Me.tlbZoomBanche.GlyphPath = ""
Me.tlbZoomBanche.Id = 43
Me.tlbZoomBanche.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5))
Me.tlbZoomBanche.Name = "tlbZoomBanche"
Me.tlbZoomBanche.NTSIsCheckBox = false
Me.tlbZoomBanche.Visible = true
'
'tlbVisDettTCO
'
Me.tlbVisDettTCO.Caption = "Visualizza dettagli per taglia"
Me.tlbVisDettTCO.GlyphPath = ""
Me.tlbVisDettTCO.Id = 64
Me.tlbVisDettTCO.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
Me.tlbVisDettTCO.Name = "tlbVisDettTCO"
Me.tlbVisDettTCO.NTSIsCheckBox = false
Me.tlbVisDettTCO.Visible = true
'
'tlbGenNumComm
'
Me.tlbGenNumComm.Caption = "Genera numero di commessa"
Me.tlbGenNumComm.GlyphPath = ""
Me.tlbGenNumComm.Id = 59
Me.tlbGenNumComm.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F6))
Me.tlbGenNumComm.Name = "tlbGenNumComm"
Me.tlbGenNumComm.NTSIsCheckBox = false
Me.tlbGenNumComm.Visible = true
'
'tlbApriChiamata
'
Me.tlbApriChiamata.Caption = "Apri chiamata"
Me.tlbApriChiamata.GlyphPath = ""
Me.tlbApriChiamata.Id = 35
Me.tlbApriChiamata.Name = "tlbApriChiamata"
Me.tlbApriChiamata.NTSIsCheckBox = false
Me.tlbApriChiamata.Visible = true
'
'tlbOggettiAssociati
'
Me.tlbOggettiAssociati.Caption = "Oggetti associati"
Me.tlbOggettiAssociati.GlyphPath = ""
Me.tlbOggettiAssociati.Id = 36
Me.tlbOggettiAssociati.Name = "tlbOggettiAssociati"
Me.tlbOggettiAssociati.NTSIsCheckBox = false
Me.tlbOggettiAssociati.Visible = true
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
Me.tlbSchedatrasporto.Id = 75
Me.tlbSchedatrasporto.Name = "tlbSchedatrasporto"
Me.tlbSchedatrasporto.NTSIsCheckBox = false
Me.tlbSchedatrasporto.Visible = true
'
'tlbEmail
'
Me.tlbEmail.Caption = "Visualizza e-mail scambiate"
Me.tlbEmail.GlyphPath = ""
Me.tlbEmail.Id = 81
Me.tlbEmail.Name = "tlbEmail"
Me.tlbEmail.Visible = true
'
'tlbSkype
'
Me.tlbSkype.Caption = "Chiama con Skype"
Me.tlbSkype.GlyphPath = ""
Me.tlbSkype.Id = 95
Me.tlbSkype.Name = "tlbSkype"
Me.tlbSkype.Visible = true
'
'tlbMnuRecord
'
Me.tlbMnuRecord.Caption = "&Menu Riga"
Me.tlbMnuRecord.GlyphPath = ""
Me.tlbMnuRecord.Id = 8
Me.tlbMnuRecord.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecMov), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecOrd), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpCollegati, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLavCollegate), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDocCollegati), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNoteRiga, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoomRigheOff), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInsRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettQtaDate, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDispComm), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDispVariante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDettEvasione), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazMrp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaColliQta, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaListini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalvaCondPart), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancRigheSel)})
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
'tlbPrecOrd
'
Me.tlbPrecOrd.Caption = "Mostra precedenti ordini"
Me.tlbPrecOrd.GlyphPath = ""
Me.tlbPrecOrd.Id = 58
Me.tlbPrecOrd.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
Me.tlbPrecOrd.Name = "tlbPrecOrd"
Me.tlbPrecOrd.NTSIsCheckBox = false
Me.tlbPrecOrd.Visible = true
'
'tlbImpCollegati
'
Me.tlbImpCollegati.Caption = "Impegni collegati"
Me.tlbImpCollegati.GlyphPath = ""
Me.tlbImpCollegati.Id = 53
Me.tlbImpCollegati.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
Me.tlbImpCollegati.Name = "tlbImpCollegati"
Me.tlbImpCollegati.NTSIsCheckBox = false
Me.tlbImpCollegati.Visible = true
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
Me.tlbSeleziona.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelOFA), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezDaTerm), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaLista), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInsRigheDaFigliDB), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImport), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbExport), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelectDeselect), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezArtAcc, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezArtSucc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezLottoUbicaz, true)})
Me.tlbSeleziona.Name = "tlbSeleziona"
Me.tlbSeleziona.Visible = true
'
'tlbSelOFA
'
Me.tlbSelOFA.Caption = "Seleziona righe d'ordine aperto"
Me.tlbSelOFA.GlyphPath = ""
Me.tlbSelOFA.Id = 29
Me.tlbSelOFA.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F3))
Me.tlbSelOFA.Name = "tlbSelOFA"
Me.tlbSelOFA.Visible = true
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
Me.tlbDaLista.Id = 88
Me.tlbDaLista.Name = "tlbDaLista"
Me.tlbDaLista.Visible = true
'
'tlbInsRigheDaFigliDB
'
Me.tlbInsRigheDaFigliDB.Caption = "Inserisci righe da figli di DB"
Me.tlbInsRigheDaFigliDB.GlyphPath = ""
Me.tlbInsRigheDaFigliDB.Id = 91
Me.tlbInsRigheDaFigliDB.Name = "tlbInsRigheDaFigliDB"
Me.tlbInsRigheDaFigliDB.NTSIsCheckBox = false
Me.tlbInsRigheDaFigliDB.Visible = true
'
'tlbImport
'
Me.tlbImport.Caption = "Importa righe"
Me.tlbImport.GlyphPath = ""
Me.tlbImport.Id = 85
Me.tlbImport.Name = "tlbImport"
Me.tlbImport.Visible = true
'
'tlbExport
'
Me.tlbExport.Caption = "Esporta righe selezionate"
Me.tlbExport.GlyphPath = ""
Me.tlbExport.Id = 86
Me.tlbExport.Name = "tlbExport"
Me.tlbExport.Visible = true
'
'tlbSelectDeselect
'
Me.tlbSelectDeselect.Caption = "Seleziona\deseleziona le righe da esportare"
Me.tlbSelectDeselect.GlyphPath = ""
Me.tlbSelectDeselect.Id = 87
Me.tlbSelectDeselect.Name = "tlbSelectDeselect"
Me.tlbSelectDeselect.Visible = true
'
'tlbSelezArtAcc
'
Me.tlbSelezArtAcc.Caption = "Articolo accessorio"
Me.tlbSelezArtAcc.GlyphPath = ""
Me.tlbSelezArtAcc.Id = 51
Me.tlbSelezArtAcc.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F8))
Me.tlbSelezArtAcc.Name = "tlbSelezArtAcc"
Me.tlbSelezArtAcc.NTSIsCheckBox = false
Me.tlbSelezArtAcc.Visible = true
'
'tlbSelezArtSucc
'
Me.tlbSelezArtSucc.Caption = "Articolo succedaneo"
Me.tlbSelezArtSucc.GlyphPath = ""
Me.tlbSelezArtSucc.Id = 52
Me.tlbSelezArtSucc.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F9))
Me.tlbSelezArtSucc.Name = "tlbSelezArtSucc"
Me.tlbSelezArtSucc.NTSIsCheckBox = false
Me.tlbSelezArtSucc.Visible = true
'
'tlbSelezLottoUbicaz
'
Me.tlbSelezLottoUbicaz.Caption = "Lotti / ubicazioni aperti"
Me.tlbSelezLottoUbicaz.GlyphPath = ""
Me.tlbSelezLottoUbicaz.Id = 26
Me.tlbSelezLottoUbicaz.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
Me.tlbSelezLottoUbicaz.Name = "tlbSelezLottoUbicaz"
Me.tlbSelezLottoUbicaz.NTSIsCheckBox = false
Me.tlbSelezLottoUbicaz.Visible = true
'
'tlbApriDocCollegati
'
Me.tlbApriDocCollegati.Caption = "APRI"
Me.tlbApriDocCollegati.GlyphPath = ""
Me.tlbApriDocCollegati.Id = 72
Me.tlbApriDocCollegati.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriOrdineCollegato), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriOffertaCollegata), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriDB, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriConfiguratore)})
Me.tlbApriDocCollegati.Name = "tlbApriDocCollegati"
Me.tlbApriDocCollegati.Visible = true
'
'tlbApriOrdineCollegato
'
Me.tlbApriOrdineCollegato.Caption = "Ordine aperto collegato"
Me.tlbApriOrdineCollegato.GlyphPath = ""
Me.tlbApriOrdineCollegato.Id = 73
Me.tlbApriOrdineCollegato.Name = "tlbApriOrdineCollegato"
Me.tlbApriOrdineCollegato.NTSIsCheckBox = false
Me.tlbApriOrdineCollegato.Visible = true
'
'tlbApriOffertaCollegata
'
Me.tlbApriOffertaCollegata.Caption = "Offerta collegata"
Me.tlbApriOffertaCollegata.GlyphPath = ""
Me.tlbApriOffertaCollegata.Id = 74
Me.tlbApriOffertaCollegata.Name = "tlbApriOffertaCollegata"
Me.tlbApriOffertaCollegata.NTSIsCheckBox = false
Me.tlbApriOffertaCollegata.Visible = true
'
'tlbApriDB
'
Me.tlbApriDB.Caption = "Distinta base collegata"
Me.tlbApriDB.GlyphPath = ""
Me.tlbApriDB.Id = 55
Me.tlbApriDB.Name = "tlbApriDB"
Me.tlbApriDB.NTSIsCheckBox = false
Me.tlbApriDB.Visible = true
'
'tlbApriConfiguratore
'
Me.tlbApriConfiguratore.Caption = "Apri configuratore"
Me.tlbApriConfiguratore.GlyphPath = ""
Me.tlbApriConfiguratore.Id = 31
Me.tlbApriConfiguratore.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
Me.tlbApriConfiguratore.Name = "tlbApriConfiguratore"
Me.tlbApriConfiguratore.NTSIsCheckBox = false
Me.tlbApriConfiguratore.Visible = true
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
'tlbZoomRigheOff
'
Me.tlbZoomRigheOff.Caption = "Zoom righe di offerte per articolo"
Me.tlbZoomRigheOff.GlyphPath = ""
Me.tlbZoomRigheOff.Id = 44
Me.tlbZoomRigheOff.Name = "tlbZoomRigheOff"
Me.tlbZoomRigheOff.NTSIsCheckBox = false
Me.tlbZoomRigheOff.Visible = true
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
'tlbDettQtaDate
'
Me.tlbDettQtaDate.Caption = "Dettaglio quantità/date"
Me.tlbDettQtaDate.GlyphPath = ""
Me.tlbDettQtaDate.Id = 56
Me.tlbDettQtaDate.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X))
Me.tlbDettQtaDate.Name = "tlbDettQtaDate"
Me.tlbDettQtaDate.NTSIsCheckBox = false
Me.tlbDettQtaDate.Visible = true
'
'tlbVisDispComm
'
Me.tlbVisDispComm.Caption = "Visualizza disponibilità commessa"
Me.tlbVisDispComm.GlyphPath = ""
Me.tlbVisDispComm.Id = 62
Me.tlbVisDispComm.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
Me.tlbVisDispComm.Name = "tlbVisDispComm"
Me.tlbVisDispComm.NTSIsCheckBox = false
Me.tlbVisDispComm.Visible = true
'
'tlbVisDispVariante
'
Me.tlbVisDispVariante.Caption = "Visualizza disponibilità variante/art. TCO"
Me.tlbVisDispVariante.GlyphPath = ""
Me.tlbVisDispVariante.Id = 63
Me.tlbVisDispVariante.Name = "tlbVisDispVariante"
Me.tlbVisDispVariante.NTSIsCheckBox = false
Me.tlbVisDispVariante.Visible = true
'
'tlbVisDettEvasione
'
Me.tlbVisDettEvasione.Caption = "Visualizza dettaglio evasione"
Me.tlbVisDettEvasione.GlyphPath = ""
Me.tlbVisDettEvasione.Id = 68
Me.tlbVisDettEvasione.Name = "tlbVisDettEvasione"
Me.tlbVisDettEvasione.NTSIsCheckBox = false
Me.tlbVisDettEvasione.Visible = true
'
'tlbNavigazMrp
'
Me.tlbNavigazMrp.Caption = "Navigazione MRP"
Me.tlbNavigazMrp.GlyphPath = ""
Me.tlbNavigazMrp.Id = 37
Me.tlbNavigazMrp.Name = "tlbNavigazMrp"
Me.tlbNavigazMrp.NTSIsCheckBox = false
Me.tlbNavigazMrp.Visible = true
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
'tlbRicalcolaListini
'
Me.tlbRicalcolaListini.Caption = "Ricalcola listini"
Me.tlbRicalcolaListini.GlyphPath = ""
Me.tlbRicalcolaListini.Id = 60
Me.tlbRicalcolaListini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F12))
Me.tlbRicalcolaListini.Name = "tlbRicalcolaListini"
Me.tlbRicalcolaListini.NTSIsCheckBox = false
Me.tlbRicalcolaListini.Visible = true
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
Me.tlbCancRigheSel.Id = 94
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
Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichetteRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichetteFinale), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaFileTerm, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicaricaGreg), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalvanuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiapriDopoSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRighe, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPersVend, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPersAcq), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNumerazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTabBolli), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, true), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
Me.tlbStrumenti.Name = "tlbStrumenti"
Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
Me.tlbStrumenti.Visible = true
'
'tlbCambioDitta
'
Me.tlbCambioDitta.Caption = "Cambio ditta"
Me.tlbCambioDitta.GlyphPath = ""
Me.tlbCambioDitta.Id = 82
Me.tlbCambioDitta.Name = "tlbCambioDitta"
Me.tlbCambioDitta.NTSIsCheckBox = false
Me.tlbCambioDitta.Visible = true
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
'tlbImpostaFileTerm
'
Me.tlbImpostaFileTerm.Caption = "Imposta file terminale"
Me.tlbImpostaFileTerm.GlyphPath = ""
Me.tlbImpostaFileTerm.Id = 34
Me.tlbImpostaFileTerm.Name = "tlbImpostaFileTerm"
Me.tlbImpostaFileTerm.NTSIsCheckBox = false
Me.tlbImpostaFileTerm.Visible = true
'
'tlbRicaricaGreg
'
Me.tlbRicaricaGreg.Caption = "Ricarica configurazioni"
Me.tlbRicaricaGreg.GlyphPath = ""
Me.tlbRicaricaGreg.Id = 32
Me.tlbRicaricaGreg.Name = "tlbRicaricaGreg"
Me.tlbRicaricaGreg.NTSIsCheckBox = false
Me.tlbRicaricaGreg.Visible = true
'
'tlbSalvanuovo
'
Me.tlbSalvanuovo.Caption = "Dopo salva -> nuovo"
Me.tlbSalvanuovo.GlyphPath = ""
Me.tlbSalvanuovo.Id = 83
Me.tlbSalvanuovo.Name = "tlbSalvanuovo"
Me.tlbSalvanuovo.NTSIsCheckBox = true
Me.tlbSalvanuovo.Visible = true
'
'tlbRiapriDopoSalva
'
Me.tlbRiapriDopoSalva.Caption = "Riapri doc. dopo salva"
Me.tlbRiapriDopoSalva.GlyphPath = ""
Me.tlbRiapriDopoSalva.Id = 84
Me.tlbRiapriDopoSalva.Name = "tlbRiapriDopoSalva"
Me.tlbRiapriDopoSalva.NTSIsCheckBox = true
Me.tlbRiapriDopoSalva.Visible = true
'
'tlbOrdinaRighe
'
Me.tlbOrdinaRighe.Caption = "Ordina righe per ..."
Me.tlbOrdinaRighe.GlyphPath = ""
Me.tlbOrdinaRighe.Id = 76
Me.tlbOrdinaRighe.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheCodiceArticolo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheDescrizione), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaRigheCommessa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiordinarighe, true)})
Me.tlbOrdinaRighe.Name = "tlbOrdinaRighe"
Me.tlbOrdinaRighe.Visible = true
'
'tlbOrdinaRigheRiga
'
Me.tlbOrdinaRigheRiga.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheRiga.Caption = "Riga"
Me.tlbOrdinaRigheRiga.Down = true
Me.tlbOrdinaRigheRiga.GlyphPath = ""
Me.tlbOrdinaRigheRiga.Id = 77
Me.tlbOrdinaRigheRiga.Name = "tlbOrdinaRigheRiga"
Me.tlbOrdinaRigheRiga.Visible = true
'
'tlbOrdinaRigheCodiceArticolo
'
Me.tlbOrdinaRigheCodiceArticolo.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheCodiceArticolo.Caption = "Codice articolo"
Me.tlbOrdinaRigheCodiceArticolo.GlyphPath = ""
Me.tlbOrdinaRigheCodiceArticolo.Id = 78
Me.tlbOrdinaRigheCodiceArticolo.Name = "tlbOrdinaRigheCodiceArticolo"
Me.tlbOrdinaRigheCodiceArticolo.Visible = true
'
'tlbOrdinaRigheDescrizione
'
Me.tlbOrdinaRigheDescrizione.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheDescrizione.Caption = "Descrizione"
Me.tlbOrdinaRigheDescrizione.GlyphPath = ""
Me.tlbOrdinaRigheDescrizione.Id = 79
Me.tlbOrdinaRigheDescrizione.Name = "tlbOrdinaRigheDescrizione"
Me.tlbOrdinaRigheDescrizione.Visible = true
'
'tlbOrdinaRigheCommessa
'
Me.tlbOrdinaRigheCommessa.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
Me.tlbOrdinaRigheCommessa.Caption = "Commessa"
Me.tlbOrdinaRigheCommessa.GlyphPath = ""
Me.tlbOrdinaRigheCommessa.Id = 80
Me.tlbOrdinaRigheCommessa.Name = "tlbOrdinaRigheCommessa"
Me.tlbOrdinaRigheCommessa.Visible = true
'
'tlbRiordinarighe
'
Me.tlbRiordinarighe.Caption = "Riordina righe"
Me.tlbRiordinarighe.GlyphPath = ""
Me.tlbRiordinarighe.Id = 89
Me.tlbRiordinarighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
                Or System.Windows.Forms.Keys.F12))
Me.tlbRiordinarighe.Name = "tlbRiordinarighe"
Me.tlbRiordinarighe.NTSIsCheckBox = false
Me.tlbRiordinarighe.Visible = true
'
'tlbPersVend
'
Me.tlbPersVend.Caption = "Personalizzaz. vendite"
Me.tlbPersVend.GlyphPath = ""
Me.tlbPersVend.Id = 38
Me.tlbPersVend.Name = "tlbPersVend"
Me.tlbPersVend.NTSIsCheckBox = false
Me.tlbPersVend.Visible = true
'
'tlbPersAcq
'
Me.tlbPersAcq.Caption = "Personalizzaz. acquisti"
Me.tlbPersAcq.GlyphPath = ""
Me.tlbPersAcq.Id = 39
Me.tlbPersAcq.Name = "tlbPersAcq"
Me.tlbPersAcq.NTSIsCheckBox = false
Me.tlbPersAcq.Visible = true
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
'tlbImpostaStampante
'
Me.tlbImpostaStampante.Caption = "Imposta Stampante"
Me.tlbImpostaStampante.GlyphPath = ""
Me.tlbImpostaStampante.Id = 16
Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
Me.tlbImpostaStampante.Visible = true
'
'tlbStampaWord
'
Me.tlbStampaWord.Caption = "StampaWord"
Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"),System.Drawing.Image)
Me.tlbStampaWord.GlyphPath = ""
Me.tlbStampaWord.Id = 11
Me.tlbStampaWord.Name = "tlbStampaWord"
Me.tlbStampaWord.Visible = true
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
'FRMORGSOR_T
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.ClientSize = New System.Drawing.Size(731, 475)
Me.Controls.Add(Me.tsGsor)
Me.Controls.Add(Me.pnTestataTop)
Me.Controls.Add(Me.barDockControlLeft)
Me.Controls.Add(Me.barDockControlRight)
Me.Controls.Add(Me.barDockControlBottom)
Me.Controls.Add(Me.barDockControlTop)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Name = "FRMORGSOR_T"
Me.NTSLastControlFocussed = Me.grRighe
Me.Text = "GESTIONE ORDINI"
CType(Me.dttSmartArt,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnTestataTop,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataTop.ResumeLayout(false)
Me.pnTestataTop.PerformLayout
CType(Me.edNumDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edSerieDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edAnnoDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.cbTipoDoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edet_datdoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_coddest.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_conto.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage4.ResumeLayout(false)
CType(Me.pnPage4,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPage4.ResumeLayout(false)
CType(Me.edEt_note.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage3.ResumeLayout(false)
CType(Me.pnPage3,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPage3.ResumeLayout(false)
CType(Me.pnPiedeTotali,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeTotali.ResumeLayout(false)
Me.pnPiedeTotali.PerformLayout
CType(Me.edEt_speaccv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotalev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotale.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speacc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edResiduo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edResiduov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speimb.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abbuono.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speinc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pagato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_bolli.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abbuonov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totomag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totmerce.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totmercev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totdoc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_bolliv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pagatov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speincv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totomagv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_speimbv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totdocv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotIva.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotImponib.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotIvav.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotImponibv.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotSpese.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edTotSpesev.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPiedeAltro,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeAltro.ResumeLayout(false)
Me.pnPiedeAltro.PerformLayout
CType(Me.fmPartita,System.ComponentModel.ISupportInitialize).EndInit
Me.fmPartita.ResumeLayout(false)
Me.fmPartita.PerformLayout
CType(Me.edEt_datpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_numpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_alfpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_annpar.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmTco,System.ComponentModel.ISupportInitialize).EndInit
Me.fmTco.ResumeLayout(false)
Me.fmTco.PerformLayout
CType(Me.edEt_annotco.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codstag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmProvvigioni,System.ComponentModel.ISupportInitialize).EndInit
Me.fmProvvigioni.ResumeLayout(false)
Me.fmProvvigioni.PerformLayout
CType(Me.edEt_codagen2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totprov2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totprov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_impprov.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codcfam.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPiedeTrasporto,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeTrasporto.ResumeLayout(false)
Me.pnPiedeTrasporto.PerformLayout
CType(Me.edEt_caustra.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_aspetto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_pesonetto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codaspe.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_peso.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_totcoll.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_coddest2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_porto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_vettor2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.cbEt_acuradi.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPiedeSx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPiedeSx.ResumeLayout(false)
Me.TabPage2.ResumeLayout(false)
CType(Me.pnGriglia,System.ComponentModel.ISupportInitialize).EndInit
Me.pnGriglia.ResumeLayout(false)
CType(Me.grRighe,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.grvRighe,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnTCO,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTCO.ResumeLayout(false)
CType(Me.grTco,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.grvTco,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnCorpo,System.ComponentModel.ISupportInitialize).EndInit
Me.pnCorpo.ResumeLayout(false)
Me.pnCorpo.PerformLayout
CType(Me.edSconto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edUltCost.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edPreList.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edOrdinato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edDispNetta.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edImpegnato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edDispon.Properties,System.ComponentModel.ISupportInitialize).EndInit
Me.TabPage1.ResumeLayout(false)
CType(Me.pnTestataDx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataDx.ResumeLayout(false)
CType(Me.pnDatiTesta2,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta2.ResumeLayout(false)
Me.pnDatiTesta2.PerformLayout
CType(Me.ckEt_rilasciato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_aperto.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_scorpo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flspinc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flboll.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_soloasa.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_datcons.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_magaz.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_magaz2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scont2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_magimp.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scont1.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_datapag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_scopag.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_listino.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codpaga.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnDatiTesta4,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta4.ResumeLayout(false)
Me.pnDatiTesta4.PerformLayout
CType(Me.fmPa,System.ComponentModel.ISupportInitialize).EndInit
Me.fmPa.ResumeLayout(false)
Me.fmPa.PerformLayout
CType(Me.edEt_riferimpa.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cig.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cup.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_valuta.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cambio.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_contfatt.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_vettor.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_controp.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codese.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnDatiTesta1,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta1.ResumeLayout(false)
Me.pnDatiTesta1.PerformLayout
CType(Me.edEt_organig.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_sospeso.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_confermato.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flevas.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ckEt_flstam.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_tipobf.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.cbEt_blocco.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_riferim.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codagen.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnDatiTesta3,System.ComponentModel.ISupportInitialize).EndInit
Me.pnDatiTesta3.ResumeLayout(false)
CType(Me.fmBanca,System.ComponentModel.ISupportInitialize).EndInit
Me.fmBanca.ResumeLayout(false)
Me.fmBanca.PerformLayout
CType(Me.edEt_codbanc.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_banc2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_banc1.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_cab.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_abi.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmCa,System.ComponentModel.ISupportInitialize).EndInit
Me.fmCa.ResumeLayout(false)
Me.fmCa.PerformLayout
CType(Me.edEt_codcli.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_coddivi.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_subcommeca.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codcena.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_commeca.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnTestataSx,System.ComponentModel.ISupportInitialize).EndInit
Me.pnTestataSx.ResumeLayout(false)
CType(Me.tsGsor,System.ComponentModel.ISupportInitialize).EndInit
Me.tsGsor.ResumeLayout(false)
Me.tpContratti.ResumeLayout(false)
CType(Me.pnContratti,System.ComponentModel.ISupportInitialize).EndInit
Me.pnContratti.ResumeLayout(false)
CType(Me.fmContratti,System.ComponentModel.ISupportInitialize).EndInit
Me.fmContratti.ResumeLayout(false)
Me.fmContratti.PerformLayout
CType(Me.edEt_codtes.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPromoData,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPromoData.ResumeLayout(false)
Me.pnPromoData.PerformLayout
CType(Me.edEt_datpromo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_orpromo.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codtes2.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codtes3.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codtes4.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codtes5.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.edEt_codtes6.Properties,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.fmPromozioni,System.ComponentModel.ISupportInitialize).EndInit
Me.fmPromozioni.ResumeLayout(false)
CType(Me.grPromo,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.grvPromo,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.pnPromoPulsanti,System.ComponentModel.ISupportInitialize).EndInit
Me.pnPromoPulsanti.ResumeLayout(false)
CType(Me.NtsBarManager1,System.ComponentModel.ISupportInitialize).EndInit
Me.ResumeLayout(false)

End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTipoRk As New DataTable()
      Dim dttTrasporto As New DataTable()
      Dim dttBlocco As New DataTable()

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
      'dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})
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


      dttBlocco.Columns.Add("cod", GetType(String))
      dttBlocco.Columns.Add("val", GetType(String))
      dttBlocco.Rows.Add(New Object() {"N", "Nessun blocco"})
      dttBlocco.Rows.Add(New Object() {"B", "Blocco fisso"})
      dttBlocco.Rows.Add(New Object() {"F", "Blocco fuori Fido"})
      dttBlocco.Rows.Add(New Object() {"I", "Blocco insoluti"})
      dttBlocco.Rows.Add(New Object() {"R", "Blocco RD Scadute"})
      cbEt_blocco.DataSource = dttBlocco
      cbEt_blocco.ValueMember = "cod"
      cbEt_blocco.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim dttStasino As New DataTable()
    Dim dttKit As New DataTable()
    Dim dttOatipo As New DataTable()
    Dim dttTask As New DataTable()

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
        tlbSelOfferta.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbDettaglioTCO.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      grvRighe.NTSMenuContext = tlbMnuRecord

      cbEt_blocco.NTSSetParam(oApp.Tr(Me, 128230023230507584, "Blocco cliente"))
      cbEt_acuradi.NTSSetParam(oApp.Tr(Me, 128230023230663757, "Tipo trasporto"))
      cbTipoDoc.NTSSetParam(oApp.Tr(Me, 128230023230819930, "Tipo documento"))

      edEt_cambio.NTSSetParam(oMenu, oApp.Tr(Me, 128230023230976103, "Cambio"), "#,##0.000000000", 20, 0, 99999999)
      edEt_valuta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231132276, "Codice valuta"), tabvalu)
      edEt_scont2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231288449, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      edEt_scont1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231444622, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      edEt_scopag.NTSSetParam(oMenu, oApp.Tr(Me, 128230023231600795, "Sconto pagamento"), oApp.FormatSconti, 6, -100, 100)
      edEt_codpaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231756968, "Pagamento"), tabpaga)
      edEt_listino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023231913141, "Numero listino"), tablist)
      edEt_commeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232069314, "Commessa"), tabcommess)
      edEt_subcommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232225487, "Sub commessa"), tabsubcomm, False)
      edEt_codcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232381660, "Famiglia"), tabcfam, False)
      edEt_codcena.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232537833, "Centro di CA"), tabcena)
      edEt_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023232694006, "Codice Ns. Banca"), tabbanc)
      edEt_banc2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023232850179, "Descrizione Filiale"), 50)
      edEt_banc1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023233006352, "Descrizione Banca"), 50)
      edEt_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233162525, "Codice CAB"), tababicab)
      edEt_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233318698, "Codice ABI"), tababi)
      edEt_vettor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233474871, "Codice vettore/spedizioniere"), tabvett)
      edEt_magimp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233631044, "Magazzino Impegni"), tabmaga)
      edEt_magaz2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023233787217, "Magazzino 2"), tabmaga)
      ckEt_sospeso.NTSSetParam(oMenu, oApp.Tr(Me, 128230023233943390, "Flag Sospeso"), "S", "N")
      ckEt_aperto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234099563, "Flag Aperto"), "S", "N")
      ckEt_rilasciato.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234255736, "Flag Rilasciato"), "S", "N")
      ckEt_flevas.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234411909, "Flag Evaso"), "S", "N")
      ckEt_confermato.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234568082, "Flag Confermato"), "S", "N")
      ckEt_soloasa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234724255, "Flag Solo a saldo"), "S", "N")
      ckEt_flboll.NTSSetParam(oMenu, oApp.Tr(Me, 128230023234880428, "Flag Addebito spese per bolli"), "S", "N")
      ckEt_flspinc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235036601, "Flag Addebito spese di incasso"), "S", "N")
      ckEt_scorpo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235192774, "Flag Scorporo"), "S", "N")
      edEt_riferim.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235348947, "Riferimenti"), 50, True)
      edEt_datcons.NTSSetParam(oMenu, oApp.Tr(Me, 128230023235505120, "Data generale di consegna"), False)
      edEt_codese.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235661293, "Esenzione IVA"), tabciva)
      edEt_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235817466, "Magazzino"), tabmaga)
      edEt_codagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023235973639, "Agente"), tabcage)
      edEt_tipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023236129812, "Tipo bolla/fattura"), tabtpbf)
      edSconto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236285985, "Sconto effettivo"), oApp.FormatSconti, 10, -999999999, 9999999999)
      edUltCost.NTSSetParam(oMenu, oApp.Tr(Me, 129048470726242161, "Ultimo costo"), oApp.FormatPrzUn)
      edPreList.NTSSetParam(oMenu, oApp.Tr(Me, 129048470909059341, "Prezzo di listino"), oApp.FormatPrzUn)
      FormatQtaEsistCorpo()
      edEt_annotco.NTSSetParam(oMenu, oApp.Tr(Me, 129048471312194661, "Anno taglie e colori"), "0", 4, 0, 2099)
      edEt_codstag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471343757969, "Stagione taglie e colori"), tabstag)
      edEt_codagen2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471381102675, "Agente 2"), tabcage)
      edEt_totprov2.NTSSetParam(oMenu, oApp.Tr(Me, 129048471421103699, "Totale provvigioni agente 2"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totprov.NTSSetParam(oMenu, oApp.Tr(Me, 129048471445166815, "Totale provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_impprov.NTSSetParam(oMenu, oApp.Tr(Me, 129048471466417359, "Imponibile provvigioni"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_datpar.NTSSetParam(oMenu, oApp.Tr(Me, 129048471506887145, "Data partita"), True)
      edEt_numpar.NTSSetParam(oMenu, oApp.Tr(Me, 129048471533606579, "Numero partita"), "0", 9, 0, 999999999)
      edEt_alfpar.NTSSetParam(oMenu, oApp.Tr(Me, 129048471564857379, "Serie partita"), CLN__STD.SerieMaxLen, False)
      edEt_annpar.NTSSetParam(oMenu, oApp.Tr(Me, 129048471585795415, "Anno partita"), "0", 4, 0, 2099)
      edEt_pesonetto.NTSSetParam(oMenu, oApp.Tr(Me, 129048471621421327, "Peso netto"), oApp.FormatQta)
      edEt_peso.NTSSetParam(oMenu, oApp.Tr(Me, 129048471648609523, "Peso lordo"), oApp.FormatQta)
      edEt_totcoll.NTSSetParam(oMenu, oApp.Tr(Me, 129048471689079309, "Totale colli"), "#,##0")
      edEt_porto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471737205541, "Porto"), tabport, True)
      edEt_vettor2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471766737547, "Vettore 2"), tabvett)
      edEt_coddest2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471799394633, "Destinazione diversa 2"), tabdestdiv)
      edEt_coddest2.CliePerDestdiv = edEt_conto
      edEt_codaspe.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471829707909, "Codice aspetto esteriore dei beni"), tabaspe)
      edEt_aspetto.NTSSetParam(oMenu, oApp.Tr(Me, 129048471865958837, "Aspetto esteriore dei beni"), 30)
      edEt_caustra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048471890021953, "Causale del trasporto"), tabcaum)
      edTotalev.NTSSetParam(oMenu, oApp.Tr(Me, 129048471913616307, "Totale in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotale.NTSSetParam(oMenu, oApp.Tr(Me, 129048471937366915, "Totale"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edResiduov.NTSSetParam(oMenu, oApp.Tr(Me, 129048471987211941, "Residuo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edResiduo.NTSSetParam(oMenu, oApp.Tr(Me, 129048472011275057, "Residuo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_abbuonov.NTSSetParam(oMenu, oApp.Tr(Me, 129048472036900713, "Abbuono in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_abbuono.NTSSetParam(oMenu, oApp.Tr(Me, 129048472062526369, "Abbuono"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_pagatov.NTSSetParam(oMenu, oApp.Tr(Me, 129048472122684159, "Incassato in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_pagato.NTSSetParam(oMenu, oApp.Tr(Me, 129048472148622323, "Incassato"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totomagv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472184091981, "Omaggi in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_totomag.NTSSetParam(oMenu, oApp.Tr(Me, 129048472203936239, "Omaggi"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totdocv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472231593197, "Totale documento in valuta"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472252531233, "Totale documento"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotIvav.NTSSetParam(oMenu, oApp.Tr(Me, 129048472282375747, "Totale IVA in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotIva.NTSSetParam(oMenu, oApp.Tr(Me, 129048472307688895, "Totale IVA"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotImponibv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472358471445, "Totale imponibile in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotImponib.NTSSetParam(oMenu, oApp.Tr(Me, 129048472382534561, "Totale imponibile"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edTotSpesev.NTSSetParam(oMenu, oApp.Tr(Me, 129048472408160217, "Totale spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edTotSpese.NTSSetParam(oMenu, oApp.Tr(Me, 129048472430348285, "Totale spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_totmercev.NTSSetParam(oMenu, oApp.Tr(Me, 129048472458786513, "Totale netto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_totmerce.NTSSetParam(oMenu, oApp.Tr(Me, 129048472481755851, "Totale netto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_bolliv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472522538145, "Bolli in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_bolli.NTSSetParam(oMenu, oApp.Tr(Me, 129048472545351229, "Bolli"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speincv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472568320567, "Spese di incasso in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speinc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472591133651, "Spese di incasso"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speimbv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472615196767, "Spese di imballo in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speimb.NTSSetParam(oMenu, oApp.Tr(Me, 129048472642384963, "Spese di imballo"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_speaccv.NTSSetParam(oMenu, oApp.Tr(Me, 129048472682854749, "Spese di trasporto in valuta"), oApp.FormatImpVal, 13, -9999999999, 99999999999)
      edEt_speacc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472712386755, "Spese di trasporto"), oApp.FormatImporti, 13, -9999999999, 99999999999)
      edEt_note.NTSSetParam(oMenu, oApp.Tr(Me, 129048472758169177, "Note"), 0)
      edEt_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), tabdestdiv)
      edEt_coddest.CliePerDestdiv = edEt_conto
      edEt_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048472823483349, "Codice cliente/fornitore"), tabanagra)
      'per poter inserire piva e/o cod fisc e richiaare hlan passando la piva
      edEt_conto.nMaxLen = 11
      edEt_conto.Properties.MaxLength = 11
      edAnnoDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472846765195, "Anno documento"), "0", 4, 1900, 2099)
      edSerieDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472887078727, "Serie documento"), CLN__STD.SerieMaxLen, False)
      edNumDoc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472926767243, "Numero documento"), "0", 9, 0, 999999999)
      edet_datdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129048472952392899, "Data documento"), False)
      edEt_datapag.NTSSetParam(oMenu, oApp.Tr(Me, 129048472975049729, "Data decorrenza primo pagamento"), False)
      ckEt_flstam.NTSSetParam(oMenu, oApp.Tr(Me, 129048473004894243, "Stampato"), "S", "N")
      edEt_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048473040988917, "Codice contropartita"), tabcove)
      edEt_contfatt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048473064739525, "Conto fatturazione"), tabanagrac)
      edEt_coddivi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129270276426701015, "Divisione C/A"), tabdivi)
      edEt_codcli.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129270276691652187, "Cliente C/A"), tabanagrac)
      edEt_cup.NTSSetParam(oMenu, oApp.Tr(Me, 130471173732093137, "Codice CUP"), 15, True)
      edEt_cig.NTSSetParam(oMenu, oApp.Tr(Me, 130471174249952302, "Codice CIG"), 10, True)
      edEt_riferimpa.NTSSetParam(oMenu, oApp.Tr(Me, 130471174273813753, "Riferimento ordine"), 100, True)
      edEt_organig.NTSSetParam(oMenu, oApp.Tr(Me, 130474633945436509, "Referente"), "0", 9, 0, 999999999)
      edEt_organig.NTSSetParamZoom("ZOOMORGANIG")
      edEt_codtes6.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260022096, "Contratto 6"), "0", 9, 0, 999999999)
      edEt_codtes5.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260178335, "Contratto 5"), "0", 9, 0, 999999999)
      edEt_codtes4.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260334571, "Contratto 4"), "0", 9, 0, 999999999)
      edEt_codtes3.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260490810, "Contratto 3"), "0", 9, 0, 999999999)
      edEt_codtes2.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260647046, "Contratto 2"), "0", 9, 0, 999999999)
      edEt_codtes.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260803285, "Contratto 1"), "0", 9, 0, 999999999)
      edEt_orpromo.NTSSetParam(oMenu, oApp.Tr(Me, 130596687260959520, "Ora promozione"), "00.00", 5, 0, 24)
      edEt_datpromo.NTSSetParam(oMenu, oApp.Tr(Me, 130596687261115760, "Data promozione"), True)

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

      dttOatipo.Columns.Add("cod", GetType(String))
      dttOatipo.Columns.Add("val", GetType(String))
      dttOatipo.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttOatipo.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttOatipo.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttOatipo.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttOatipo.AcceptChanges()

      dttTask.Columns.Add("cod", GetType(String))
      dttTask.Columns.Add("val", GetType(String))
      dttTask.Rows.Add(New Object() {"S", "Saldato"})
      dttTask.Rows.Add(New Object() {"C", "Aperto"})
      dttTask.Rows.Add(New Object() {"Q", "Q"})
      dttTask.Rows.Add(New Object() {"V", "B"})
      dttTask.AcceptChanges()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleGsor.CaricaUnMis()
      dttUm.AcceptChanges()

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 129048474485557147, "Griglia righe ordine"))
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129248603853818813, "Sel."), "S", "N")
      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474518995503, "Riga"), "0", 9, 0, 999999999)
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048474544621159, "Barcode"), 255, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048474573059387, "Cod. Art."), tabartico, False)
      ec_codart.nMaxLen = 255 'imposto qui anche per SBC: per poter consentire l'inserimento di EAN 128 di tipo UCC (ovvero nel barcode, oltre al cod articolo, c'è anche quantità, lotto, matricola, ...)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048474606341489, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048474633998447, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048474657592801, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474681343409, "Colli ord."), oApp.FormatQta, 13)
      ec_colpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474706969065, "Colli pren."), oApp.FormatQta, 13)
      ec_coleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474733688499, "Colli evasi"), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048474762595489, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474798221401, "Q.tà  ordin."), oApp.FormatQta, 13)
      xxo_qtadaass.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474827753407, "Q.tà  da assegnare"), oApp.FormatQta, 13)
      xxo_flevasass.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048474857441667, "Ass. totale"), "S", "C")
      ec_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474905567899, "Q.tà  pren."), oApp.FormatQta, 13)
      ec_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474926193427, "Q.tà  evasa"), oApp.FormatQta, 13)
      ec_flevapre.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048474942912605, "Pr. totale"), "S", "C")
      ec_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048474966194451, "Evas. totale"), "S", "C")
      ec_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048474996038965, "Prezzo IVA inc."), oApp.FormatPrzUn, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475017758271, "Prezzo valuta"), oApp.FormatPrzUnVal, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475038383799, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475065571995, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      ec_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475116354545, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      ec_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475147449091, "Sconto 3"), oApp.FormatSconti, 6, -100, 100)
      ec_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475169637159, "Sconto 4"), oApp.FormatSconti, 6, -100, 100)
      ec_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475196356593, "Sconto 5"), oApp.FormatSconti, 6, -100, 100)
      ec_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475222294757, "Sconto 6"), oApp.FormatSconti, 6, -100, 100)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048475741058037, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048475766683693, "Descr. magazzino"), 0, True)
      ec_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048475795278175, "Data cons."), False)
      ec_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048475836841739, "Confermato"), "S", "N")
      ec_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048475869811333, "Rilasciato"), "S", "N")
      ec_aperto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048475904655975, "Aperto"), "S", "N")
      ec_ricimp.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048475924500233, "Provv. a val."), "S", "N")
      ec_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475949032111, "Provv. 1"), oApp.FormatSconti, 6)
      ec_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475972626465, "Importo provv. 1"), oApp.FormatImporti, 13)
      ec_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048475995595803, "Provv. 2"), oApp.FormatSconti, 6)
      ec_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048476022002729, "Importo provv. 2"), oApp.FormatImporti, 13)
      ec_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476067160135, "Controp."), tabcove)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476112473795, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476141693293, "Cod. IVA"), tabciva)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476271227859, "Descr. IVA"), 0, True)
      ec_stasino.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048476327166791, "Stampa riga"), dttStasino, "val", "cod")
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048476362167687, "Linea/Fam."), tabcfam, True)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476385449533, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476411543951, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476443263513, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048476471701741, "Sub C."), tabsubcomm, True)
      If oCleGsor.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130385091394829203, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130385093547954203, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476536547151, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476565766649, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476613736627, "Note"), 0, True)
      ec_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476638581013, "Magaz 2"), tabmaga)
      xxo_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476660612827, "Descr. magaz. 2"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048476688113531, "Valore da evadere"), oApp.FormatImporti, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048476711864139, "Conto controp."), tabanagra)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048476756084021, "Descr. conto controp."), 0, True)
      ec_datconsor.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048476781709677, "Dt. consegna originaria"), False)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048482309194927, "Cod. cliente"), tabanagra)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048482360758747, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482393884595, "Misura 1"), oApp.FormatQta, 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482422947839, "Misura 2"), oApp.FormatQta, 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482445135907, "Misura 3"), oApp.FormatQta, 13)
      xxo_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048482476542961, "Cod.Art.cli/forn"), 0, True)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482506074967, "Prz/Qta"), "#,##0.00", 13)
      ec_valoremm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482525606717, "Valore riga totale"), "#,##0.00", 13)
      ec_flkit.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048482547638531, "Tipo Kit"), dttKit, "val", "cod")
      ec_ktriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482580295617, "Rif. riga kit"), "0", 9, 0, 999999999)
      ec_oatipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048482608733845, "Tipo ordine ap."), dttOatipo, "val", "cod")
      ec_oaanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482637328327, "Anno ordine ap."), "0", 4, 0, 9999)
      ec_oaserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048482671547953, "Serie ordine ap."), CLN__STD.SerieMaxLen, True)
      ec_oanum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482691860973, "Numero ordine ap."), "0", 9, 0, 999999999)
      ec_oariga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482734830823, "Riga ordine ap."), "0", 9, 0, 999999999)
      ec_oaqtadis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482769362957, "Qta disimp. ord. ap."), oApp.FormatQta, 13)
      ec_oacoldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482802801313, "Colli disimp. ord. ap."), oApp.FormatQta, 13)
      ec_oavaldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048482830302017, "Valore disimp. ord. ap."), oApp.FormatImporti, 13)
      ec_oasalcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048482851708815, "Saldo ordine ap."), "S", "C")
      ec_umprz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048482877021963, "Prezzi x u.d.m."), 3, False)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048482896866221, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048477308441911, "Descr. fase"), 0, True)
      ec_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048477364380843, "Codice lavoraz."), tablavo)
      xxo_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048477400788025, "Descr. lavoraz."), 0, True)
      ec_pmtaskid.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477430320031, "Id Task"), "0", 9, 0, 999999999)
      xxo_pmtaskid.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048477459852037, "Descr. Task"), 0, True)
      xxo_pmqtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477480790073, "Qta res.Task"), oApp.FormatQta, 13)
      xxo_pmqtarim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477509228301, "Qta riman. Task"), oApp.FormatQta, 13)
      xxo_pmvalres.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477538604053, "Val. res. Task"), oApp.FormatImporti, 13)
      xxo_pmvalrim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477600793145, "Val. rim. Task"), oApp.FormatImporti, 13)
      ec_pmsalcon.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048477649075631, "Sal. Task"), dttTask, "val", "cod")
      xxo_pmqtadisda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477673451255, "Qta dis. Task"), oApp.FormatQta, 13)
      xxo_pmvaldisda.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477697983133, "Val. dis. Task"), oApp.FormatQta, 13)
      xxo_darave.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048477728921425, "Dare-Avere"), 1, True)
      ec_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048477763609813, "Ubicazione"), 18, False)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048477788297945, "."), "0", 4, 0, 9999)

      ec_flprznet.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128776134198790000, "Prezzo netto"), "S", "N")
      ec_oqtipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128776134212674000, "Tipo offerta"), 1, True)
      ec_oqanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128776134225934000, "Anno offerta"), "0", 4)
      ec_oqserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128776134238570000, "Serie offerta"), CLN__STD.SerieMaxLen, True)
      ec_oqnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128776134251986000, "Num. offerta"), "0", 9, 0, 999999999)
      ec_oqvers.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128776134263998000, "Vers. offerta"), "0", 9, 0, 999999999)
      ec_oqriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128776134270550000, "Riga offerta"), "0", 9, 0, 999999999)
      ec_oqsalcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128776134279442000, "Riga off. evasa"), "S", "C")
      ec_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128776134287242000, "Cod. promoz."), tabtpro)
      xxo_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128776134295354000, "Descr. promoz."), 0, True)
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128776134303622000, "Dt iniz. comp. econ."), False)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128776134313606000, "Dt fin. comp. econ."), False)
      ec_coddivi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130385094871547953, "Divisione CA"), tabdivi)
      xxo_coddivi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130385096489360453, "Descr. Divisione CA"), 0, True)
      xxo_codalt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130002093223803441, "Cod. Art. alternativo"), 0, True)
      ec_flprzmod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975273805954, "Prezzo modificato"), "S", "N")
      ec_flsc1mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975273962207, "Sconto 1 modificato"), "S", "N")
      ec_flsc2mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274118457, "Sconto 2 modificato"), "S", "N")
      ec_flsc3mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274274710, "Sconto 3 modificato"), "S", "N")
      ec_flsc4mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274430968, "Sconto 4 modificato"), "S", "N")
      ec_flsc5mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274587217, "Sconto 5 modificato"), "S", "N")
      ec_flsc6mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274743479, "Sconto 6 modificato"), "S", "N")
      ec_flprov1mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975274899728, "Provvigione agente 1 modificata"), "S", "N")
      ec_flprov2mod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130594975275055973, "Provvigione agente 2 modificata"), "S", "N")
      ec_scontp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130610418959579025, "Sconto Promozione"), oApp.FormatSconti, 6, -100, 100)
      ec_scpercdiff.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130644261186214732, "Valore sconto % Differito"), oApp.FormatImporti, 15)
      ec_prov1percdiff.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130716887874214567, "Valore provv. % Differita (agente 1)"), oApp.FormatImporti, 15)
      ec_prov2percdiff.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130716887895012203, "Valore provv. % Differita (agente 2)"), oApp.FormatImporti, 15)
      xxo_valoreiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130795171558587211, "Valore ivato"), oApp.FormatImporti, 15)
      xxo_percricarico.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804053979149389, "% Ricarico"), oApp.FormatSconti, 6)
      xxo_percmargine.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804053998517197, "% Margine"), oApp.FormatSconti, 6)
      xxo_marginevalore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130804054021028369, "Margine valore"), oApp.FormatImporti, 13)
      xxo_prznet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131007777049600135, "Prezzo netto"), oApp.FormatPrzUn, 15)

      '-------------------------------------------------
      'griglia TCO
      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 129048478280654299, "Griglia dettaglio taglie"))
      ec_quant01.NTSSetParamNUM(oMenu, "Quantità 1^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant02.NTSSetParamNUM(oMenu, "Quantità 2^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant03.NTSSetParamNUM(oMenu, "Quantità 3^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant04.NTSSetParamNUM(oMenu, "Quantità 4^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant05.NTSSetParamNUM(oMenu, "Quantità 5^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant06.NTSSetParamNUM(oMenu, "Quantità 6^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant07.NTSSetParamNUM(oMenu, "Quantità 7^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant08.NTSSetParamNUM(oMenu, "Quantità 8^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant09.NTSSetParamNUM(oMenu, "Quantità 9^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant10.NTSSetParamNUM(oMenu, "Quantità 10^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant11.NTSSetParamNUM(oMenu, "Quantità 11^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant12.NTSSetParamNUM(oMenu, "Quantità 12^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant13.NTSSetParamNUM(oMenu, "Quantità 13^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant14.NTSSetParamNUM(oMenu, "Quantità 14^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant15.NTSSetParamNUM(oMenu, "Quantità 15^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant16.NTSSetParamNUM(oMenu, "Quantità 16^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant17.NTSSetParamNUM(oMenu, "Quantità 17^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant18.NTSSetParamNUM(oMenu, "Quantità 18^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant19.NTSSetParamNUM(oMenu, "Quantità 19^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant20.NTSSetParamNUM(oMenu, "Quantità 20^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant21.NTSSetParamNUM(oMenu, "Quantità 21^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant22.NTSSetParamNUM(oMenu, "Quantità 22^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant23.NTSSetParamNUM(oMenu, "Quantità 23^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant24.NTSSetParamNUM(oMenu, "Quantità 24^ taglia", "#,##0", 6, -99999, 99999)

      grvTco.NTSAllowDelete = False
      grvTco.NTSAllowUpdate = False
      grvTco.NTSAllowInsert = False
      grvTco.Enabled = False

      '-------------------------------------------------
      'gli zoom aggiuntivi/particolari di movord
      xxo_lottox.NTSSetParamZoom("ZOOMANALOTTI")
      ec_prezzo.NTSSetParamZoom("ZOOMPREZZO")
      ec_prezvalc.NTSSetParamZoom("ZOOMPREZZO")
      ec_preziva.NTSSetParamZoom("ZOOMPREZZO")
      ec_pmtaskid.NTSSetParamZoom("ZOOMPMTASK")
      ec_ubicaz.NTSSetParamZoom("ZOOMUBICAZ")

      edNumDoc.NTSForzaVisZoom = True

      ec_preziva.NTSForzaVisZoom = True
      ec_prezvalc.NTSForzaVisZoom = True
      ec_prezzo.NTSForzaVisZoom = True

      ec_codart.NTSSetRichiesto()
      ec_quant.NTSSetRichiesto()
      ec_prezzo.NTSSetRichiesto()
      ec_codiva.NTSSetRichiesto()
      ec_controp.NTSSetRichiesto()
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

      grvPromo.NTSSetParam(oMenu, oApp.Tr(Me, 130596793633372742, "Promozioni"))
      tb_codrepr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130596799814020605, "Promozione"), "0", 9, 0, 999999999)
      tb_desrepr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130596799814176876, "Descr. Promozione"), 0, True)
      xx_contratto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130596799814333130, "Contratto"), "0", 9, 0, 999999999)
      xx_prior.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130621647260883454, "Contratto"), "0", 9, 0, 999999999)
      grvPromo.AddColumnBackColor("backcolor_row")

      grvPromo.Enabled = False
      grvPromo.NTSAllowDelete = False
      grvPromo.NTSAllowInsert = False
      grvPromo.NTSAllowUpdate = False

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
      edDispNetta.NTSSetParam(oMenu, oApp.Tr(Me, 129048471154378121, "Disponibilità netta"), oCleGsor.strFormatQtaEsistCorpo)
      edDispon.NTSSetParam(oMenu, oApp.Tr(Me, 129048471204535655, "Disponibilità"), oCleGsor.strFormatQtaEsistCorpo)
      edOrdinato.NTSSetParam(oMenu, oApp.Tr(Me, 129048471246099219, "Ordinato"), oCleGsor.strFormatQtaEsistCorpo)
      edImpegnato.NTSSetParam(oMenu, oApp.Tr(Me, 129048471282350147, "Impegnato"), oCleGsor.strFormatQtaEsistCorpo)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano gi stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli:
      'ATTENZIONE: devo aver gi preparato ed otttenuto i dati dal database. 
      'se devo rileggere i dati dal database devo rifare il bind!

      ckEt_sospeso.NTSText.NTSDbField = "testa.et_sospeso"
      ckEt_aperto.NTSText.NTSDbField = "testa.et_aperto"
      ckEt_rilasciato.NTSText.NTSDbField = "testa.et_rilasciato"
      ckEt_flevas.NTSText.NTSDbField = "testa.et_flevas"
      ckEt_confermato.NTSText.NTSDbField = "testa.et_confermato"
      ckEt_soloasa.NTSText.NTSDbField = "testa.et_soloasa"
      ckEt_flboll.NTSText.NTSDbField = "testa.et_flboll"
      ckEt_flspinc.NTSText.NTSDbField = "testa.et_flspinc"
      ckEt_scorpo.NTSText.NTSDbField = "testa.et_scorpo"
      ckEt_flstam.NTSText.NTSDbField = "testa.et_flstam"

      edEt_cambio.NTSDbField = "testa.et_cambio"
      edEt_valuta.NTSDbField = "testa.et_valuta"
      edEt_scont2.NTSDbField = "testa.et_scont2"
      edEt_scont1.NTSDbField = "testa.et_scont1"
      edEt_scopag.NTSDbField = "testa.et_scopag"
      edEt_codpaga.NTSDbField = "testa.et_codpaga"
      edEt_datapag.NTSDbField = "testa.et_datapag"
      edEt_listino.NTSDbField = "testa.et_listino"
      edEt_subcommeca.NTSDbField = "testa.et_subcommeca"
      edEt_codcfam.NTSDbField = "testa.et_codcfam"
      edEt_codcena.NTSDbField = "testa.et_codcena"
      edEt_commeca.NTSDbField = "testa.et_commeca"
      edEt_codbanc.NTSDbField = "testa.et_codbanc"
      edEt_banc2.NTSDbField = "testa.et_banc2"
      edEt_banc1.NTSDbField = "testa.et_banc1"
      edEt_cab.NTSDbField = "testa.et_cab"
      edEt_abi.NTSDbField = "testa.et_abi"
      edEt_vettor.NTSDbField = "testa.et_vettor"
      edEt_magimp.NTSDbField = "testa.et_magimp"
      edEt_magaz2.NTSDbField = "testa.et_magaz2"
      edEt_riferim.NTSDbField = "testa.et_riferim"
      edEt_datcons.NTSDbField = "testa.et_datcons"
      edEt_codese.NTSDbField = "testa.et_codese"
      edEt_magaz.NTSDbField = "testa.et_magaz"
      edEt_codagen.NTSDbField = "testa.et_codagen"
      edEt_tipobf.NTSDbField = "testa.et_tipobf"
      edEt_annotco.NTSDbField = "testa.et_annotco"
      edEt_codstag.NTSDbField = "testa.et_codstag"
      cbEt_blocco.NTSDbField = "testa.et_blocco"
      edEt_codagen2.NTSDbField = "testa.et_codagen2"
      edEt_totprov2.NTSDbField = "testa.et_totprov2"
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
      'edTotalev.NTSDbField = "testa.******"
      'edTotale.NTSDbField = "testa.******"
      'edResiduov.NTSDbField = "testa.******"
      'edResiduo.NTSDbField = "testa.******"
      edEt_abbuonov.NTSDbField = "testa.et_abbuonov"
      edEt_abbuono.NTSDbField = "testa.et_abbuono"
      edEt_pagatov.NTSDbField = "testa.et_pagatov"
      edEt_pagato.NTSDbField = "testa.et_pagato"
      edEt_totomagv.NTSDbField = "testa.et_totomagv"
      edEt_totomag.NTSDbField = "testa.et_totomag"
      edEt_totdocv.NTSDbField = "testa.et_totdocv"
      edEt_totdoc.NTSDbField = "testa.et_totdoc"
      'edTotIvav.NTSDbField = "testa.******"
      'edTotIva.NTSDbField = "testa.******"
      'edTotImponibv.NTSDbField = "testa.******"
      'edTotImponib.NTSDbField = "testa.******"
      'edTotSpesev.NTSDbField = "testa.******"
      'edTotSpese.NTSDbField = "testa.******"
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
      'edSconto.NTSDbField = "testa.******"
      'edUltCost.NTSDbField = "testa.******"
      'edPreList.NTSDbField = "testa.******"
      'edDispNetta.NTSDbField = "testa.******"
      'edDispon.NTSDbField = "testa.******"

      lbXx_contfatt.NTSDbField = "testa.xx_contfatt"
      edEt_contfatt.NTSDbField = "testa.et_contfatt"
      lbXx_controp.NTSDbField = "testa.xx_controp"
      lbEt_codchia.NTSDbField = "testa.et_codchia"
      lbEt_codattp.NTSDbField = "testa.et_codattp"
      lbXx_codstag.NTSDbField = "testa.xx_codstag"

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
      lbXx_commeca.NTSDbField = "testa.xx_commeca"
      lbXx_codcfam.NTSDbField = "testa.xx_codcfam"
      lbXx_codcena.NTSDbField = "testa.xx_codcena"
      lbXx_codbanc.NTSDbField = "testa.xx_codbanc"
      lbXx_vettor.NTSDbField = "testa.xx_vettor"
      lbXx_magimp.NTSDbField = "testa.xx_magimp"
      lbXx_magaz2.NTSDbField = "testa.xx_magaz2"
      lbXx_codese.NTSDbField = "testa.xx_codese"
      lbXx_magaz.NTSDbField = "testa.xx_magaz"
      lbXx_codagen.NTSDbField = "testa.xx_codagen"
      lbXx_codagen2.NTSDbField = "testa.xx_codagen2"
      lbXx_porto.NTSDbField = "testa.xx_porto"
      lbXx_vettor2.NTSDbField = "testa.xx_vettor2"
      lbXx_coddest2.NTSDbField = "testa.xx_coddest2"
      lbXx_caustra.NTSDbField = "testa.xx_caustra"
      lbXx_coddest.NTSDbField = "testa.xx_coddest"
      lbXx_conto.NTSDbField = "testa.xx_conto"
      lbXx_totquant.NTSDbField = "testa.xx_totquant"
      edEt_coddivi.NTSDbField = "testa.et_coddivi"
      lbXx_coddivi.NTSDbField = "testa.xx_coddivi"
      edEt_codcli.NTSDbField = "testa.et_codcli"
      lbXx_codcli.NTSDbField = "testa.xx_codcli"
      edEt_organig.NTSDbField = "testa.et_organig"
      lbXx_organig.NTSDbField = "testa.xx_organig"

      lbXx_codtes6.NTSDbField = "testa.xx_codtes6"
      lbXx_codtes5.NTSDbField = "testa.xx_codtes5"
      lbXx_codtes4.NTSDbField = "testa.xx_codtes4"
      lbXx_codtes3.NTSDbField = "testa.xx_codtes3"
      lbXx_codtes2.NTSDbField = "testa.xx_codtes2"
      lbXx_codtes.NTSDbField = "testa.xx_codtes"
      edEt_codtes6.NTSDbField = "testa.et_codtes6"
      edEt_codtes5.NTSDbField = "testa.et_codtes5"
      edEt_codtes4.NTSDbField = "testa.et_codtes4"
      edEt_codtes3.NTSDbField = "testa.et_codtes3"
      edEt_codtes2.NTSDbField = "testa.et_codtes2"
      edEt_codtes.NTSDbField = "testa.et_codtes"
      edEt_orpromo.NTSDbField = "testa.et_orpromo"
      edEt_datpromo.NTSDbField = "testa.et_datpromo"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGsor, Me)
      NTSFormAddDataBinding(dcGsorRighe, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiDatiDitta() As Boolean
    Dim bAnagen As Boolean = False
    Dim bDbMultiDitta As Boolean = False
    Dim bAgenti As Boolean = True
    Dim bNoteprel As Boolean = True
    Try
Riprova:
      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'opzione di registro "BSORGSOR/OPZIONI/LeggiDatiDitta" è attiva
      '--- non permette il cammbio ditta, impostandola fissa a oApp.Ditta
      '--- e nasconde la voce di menu relativa al Cambio Ditta
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", "OPZIONI", ".", "DisabilitaCambioDitta", "0", " ", "0")) = True Then
        tlbCambioDitta.Enabled = False
        tlbCambioDitta.Visible = False
        DittaCorrente = oApp.Ditta
        GoTo SaltaSceltaDitta
      End If
      '--------------------------------------------------------------------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      'se oCallParams.strOpen <> "" non visualizzera lo zoom ditte e prednerà la ditta passata dal child padre
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BNORGSOR", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then
        Me.Close()
        Return False
      End If

SaltaSceltaDitta:

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      oCleGsor.strDittaCorrente = DittaCorrente

      If Not oCleGsor.InitExt() Then
        Me.Close()
        Return False
      End If
      oCleGsor.strLastTiporkRegistroDoc = ""    'per far rileggere le ozioni di registro all'apertura del documento o in creazione di nuovo doc
      If oCleGsor.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129670382283955079, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129670382297402344, "Lotto"), 50, True)
      End If

      If Not CBool(oCleGsor.lModuliSupDittaDitt And bsModSupEMA) Then
        tlbEmail.Visible = False
      Else
        GctlSetVisEnab(tlbEmail, True)
      End If

      If (oCleGsor.nRiportaNoteDaAnaext < 0) Or (oCleGsor.nRiportaNoteDaAnaext > 15) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128896408885857104, "Attenzione" & vbCrLf & _
          "L'opzione di registro \Bsorgsor\Opzioni\RiportaNoteDaAnaext può assumere solo valori compresi tra 0 e 15." & vbCrLf & _
          "Pertanto l'opzione sarà impostata al valore predefinito 0."))
        oCleGsor.nRiportaNoteDaAnaext = 0
      End If

      '-------------------------------------------------
      'nascondo le voci di menu non gestite
      If CBool(oCleGsor.lModuliExtDittaDitt And bsModExtTCP) = False And _
         CBool(oCleGsor.lModuliExtDittaDitt And bsModExtTCO) = False Then
        tlbVisDettTCO.Visible = False
        tlbDettaglioTCO.Visible = False
        pnTCO.Visible = False
        fmTco.Visible = False
        edEt_codstag.Enabled = False
        edEt_annotco.Enabled = False
        tlbVisDispVariante.Visible = False
      End If
      If CBool(oCleGsor.lModuliExtDittaDitt And bsModExtCRM) = False And _
         CBool(oCleGsor.lModuliSupDittaDitt And bsModSupWCR) = False And _
         CBool(oCleGsor.lModuliDittaDitt And bsModAS) = False Then
        tlbApriChiamata.Visible = False
        lbEt_codchiaLabel.Visible = False
        lbEt_codchia.Visible = False
        lbEt_codattpLabel.Visible = False
        lbEt_codattp.Visible = False
      End If
      If CBool(oCleGsor.lModuliDittaDitt And bsModCP) = False And _
         CBool(oCleGsor.lModuliExtDittaDitt And bsModExtCP2) = False Then
        tlbApriConfiguratore.Visible = False
      End If
      If CBool(oCleGsor.lModuliSupDittaDitt And bsModSupCCC) = False Then
        tpContratti.Visible = False
      End If


      '----------------------------------
      'se non ho agenti nascondo
      oCleGsor.TabEmpty(DittaCorrente, bAgenti, bNoteprel)
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

      If bNoteprel Then
        ec_flevapre.Visible = False
        ec_colpre.Visible = False
        ec_quapre.Visible = False
      End If

      'se non è settata a 1 (ovvero usa le impostazioni della voce di menu) non ha senso visualizzare la voce di menu
      oCleGsor.bRiapriDocSuSalva = NTSCInt(Val(oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "Opzioni", ".", "RiapriDocSuSalva", "0", " ", "0"))) 'se abilitata a -1 al salvataggio del documento lo riapre (lo stesso) posizionandosi in testata, se impsotata ad 1 utilizza l'impostazione della voce di menu 'Riapri doc dopo salva'
      If oCleGsor.bRiapriDocSuSalva = 1 Then
        GctlSetVisEnab(tlbRiapriDopoSalva, True)
      Else
        tlbRiapriDopoSalva.Visible = False
        If oCleGsor.bRiapriDocSuSalva = -1 Then
          tlbSalvanuovo.Checked = False
          tlbSalvanuovo.Visible = False
        End If
      End If

      oCleGsor.bModSupRme = CBool(oCleGsor.lModuliSupDittaDitt And bsModSupRME)

      'forzo la GCTLSETROULES, per liniti impostati per ditta
      'prima per impostare eventuali blocchi sul tipo documento
      GctlTipoDoc = ""
      GctlSetRoules()

      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Eventi di FORM"
  Public Overridable Sub FRMORGSOR_T_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strMsg As String = ""
    Try

      If Me.Modal = True Then Me.MinimizeBox = False

      'nascondo tutte le colonne che, perdefault, non dovrebbero essere visibili in una gestione ordinaria.
      'poi l'utente renderà visibili quelle che vuole
      For Each col As NTSGridColumn In grvRighe.Columns
        Select Case col.Name
          Case "ec_riga", "ec_codart", "ec_descr", "ec_unmis", "ec_colli", "ec_coleva", _
               "ec_flevas", "ec_prezzo", "ec_scont1", "ec_scont2", "ec_datcons", _
               "ec_confermato", "ec_stasino", "ec_valore", "ec_valoremm", "ec_note"
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

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati
      'NB: se ci saranno dei controlli collegati a datasource, nel datatable sottostante
      '     un record deve esempre esserci, almeno in addnew...
      Me.NTSFormClearDataBinding(Me)
      'If Not oCleGsor.ApriOrdine(DittaCorrente, True, "R", 0, " ", 0, dsGsor) Then
      '  Me.Close()
      '  Return
      'End If
      dsGsor = New DataSet
      dsGsor.Tables.Add("TESTA")
      dsGsor.Tables.Add("CORPO")
      dcGsor.DataSource = dsGsor.Tables("TESTA")
      dsGsor.AcceptChanges()
      dcGsorRighe.DataSource = dsGsor.Tables("CORPO")

      '-------------------------------------------------
      'non collego il BindingSource ai vari controlli: lo far solo dopo aver ottenuto il dataset nella nuovo o Apri
      'diversamente da dei messaggi strani sui campi checkbox e radiobutton

      SetStato(0, False)

      '-------------------------------------------------
      'imposto l'ordinamento settato per le righe del corpo
      Dim strOrdrig As String
      strOrdrig = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "RECENT", ".", "RecordOrdinaModo", "0", " ", "0")
      Select Case strOrdrig
        Case "1"
          tlbOrdinaRigheCodiceArticolo.Down = True
          tlbOrdinaRigheCodiceArticolo_ItemClick(Nothing, Nothing)
        Case "2"
          tlbOrdinaRigheDescrizione.Down = True
          tlbOrdinaRigheDescrizione_ItemClick(Nothing, Nothing)
        Case "3"
          tlbOrdinaRigheCommessa.Down = True
          tlbOrdinaRigheCommessa_ItemClick(Nothing, Nothing)
        Case Else
          tlbOrdinaRigheRiga.Down = True
          tlbOrdinaRigheRiga_ItemClick(Nothing, Nothing)
      End Select

      edEt_datapag.Enabled = False

      tlbStampaEtichetteFinale.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", "RECENT", ".", "ReportStampaEtiFin", "0", " ", "0"))
      tlbRiapriDopoSalva.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", "RECENT", ".", "RiapriDopoSalva", "0", " ", "0"))
      tlbSalvanuovo.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", "RECENT", ".", "Salvanuovo", "0", " ", "0"))

      tlbSalvanuovo_ItemClick(tlbSalvanuovo, Nothing)

      'Serve per caricare i controlli personalizzati!
      ' Se i controlli personalizzati vengono caricati nella ActivedFirst non vengono ridimensionati quando si utilizza il /MODE=BigChar
      GctlTipoDoc = "XYZ"
      GctlSetRoules()
      GctlTipoDoc = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRMORGSOR_T_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim dttTmp As New DataTable
    Dim strParam() As String

    Try
      Me.Cursor = Cursors.WaitCursor

      If Not LeggiDatiDitta() Then Return

      '-------------------------------------------------
      'reimposto l'ultimo tipo documento utilizzato
      'dopo la GCTlsetroules, altrimenti la griglia non viene impostata correttamente ...
      cbTipoDoc.SelectedValue = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "Recent", ".", "LastTipoDoc", " ", " ", "R")
      If cbTipoDoc.SelectedIndex = -1 Then cbTipoDoc.SelectedIndex = 0
      edAnnoDoc.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "Recent", ".", "LastAnnoDoc", DateTime.Now.Year.ToString, " ", DateTime.Now.Year.ToString)
      edSerieDoc.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "Recent", ".", "LastSerieDoc", " ", " ", " ").PadLeft(1)

      cbTipoDoc_SelectedIndexChanged(cbTipoDoc, Nothing)    'per far applicare la GctlSetRoules in base al tipo documento

      '---------------------------
      'leggo i parametri passati dal child chiamante, se presenti
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          strParam = oCallParams.strParam.Split(";"c)
          Select Case strParam(0).Trim.ToUpper
            Case "NUOV"
              'chiamato solo da bsasgmatr di NTS
              If tlbNuovo.Visible And tlbNuovo.Enabled Then
                'es. chamato da ...... ANCHE CON RICERCHE AVANZATE NON HO TROVATO NESSUN DOCUMENTO CHE CHIAMASSE BSORGSOR IN QUESTO MODO IN BUS 13!!!!!
                'viene passato anche l'instid di motrans ... devo caricarlo nel datatable 
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & strParam(1) & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = strParam(1)
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edSerieDoc.Text = strParam(2)
                  tlbNuovo_ItemClick(tlbNuovo, Nothing)
                  If oCleGsor.dttET.Rows.Count = 0 Then Return
                  edEt_conto.NTSTextDB = NTSCInt(strParam(3)).ToString
                  edEt_tipobf.NTSTextDB = NTSCInt(strParam(4)).ToString
                  edEt_commeca.NTSTextDB = NTSCInt(strParam(5)).ToString
                  'm_fmForm.DagestIImotrans = CLng(Trim$(Mid(strParam, 34, 9)))
                  edEt_codpaga.NTSTextDB = NTSCInt(strParam(7)).ToString
                  edEt_riferim.NTSTextDB = strParam(8)
                  If Not oCleGsor.OkTestata() Then Return
                  oCleGsor.bDisabilitaDecodificaBarcode = True
                  If oCleGsor.GetMotransDaGestNuov(NTSCInt(strParam(6)), dttTmp) Then
                    For Each dtrT As DataRow In dttTmp.Rows
                      dsGsor.Tables("CORPO").Rows.Add(dsGsor.Tables("CORPO").NewRow)
                      With dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)
                        'forzo la MovordOnAddNewRow
                        !codditt = "."
                        !codditt = DittaCorrente
                        !ec_magaz = dtrT!mo_magaz    'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
                        !ec_magaz2 = dtrT!mo_magaz2
                        !ec_codart = dtrT!mo_codart
                        !ec_fase = dtrT!mo_fase
                        !ec_datcons = dtrT!mo_datcons
                        !ec_unmis = dtrT!mo_unmis
                        !ec_descr = dtrT!mo_descr
                        !ec_colli = dtrT!mo_colli
                        !ec_quant = dtrT!mo_quant
                        !ec_preziva = dtrT!mo_preziva
                        !ec_prezvalc = dtrT!mo_prezvalc
                        !ec_prezzo = dtrT!mo_prezzo
                        !ec_scont1 = dtrT!mo_scont1
                        !ec_scont2 = dtrT!mo_scont2
                        !ec_scont3 = dtrT!mo_scont3
                        !ec_scont4 = dtrT!mo_scont4
                        !ec_scont5 = dtrT!mo_scont5
                        !ec_scont6 = dtrT!mo_scont6
                        !ec_codiva = dtrT!mo_codiva
                        !ec_commen = dtrT!mo_commen
                        !ec_note = dtrT!mo_note
                        !ec_controp = dtrT!mo_controp
                        !ec_stasino = dtrT!mo_stasino
                        !ec_provv = dtrT!mo_provv
                        !ec_provv2 = dtrT!mo_provv2
                        !ec_prelist = dtrT!mo_prelist
                        !ec_codcfam = dtrT!mo_codcfam
                        !ec_commeca = dtrT!mo_commeca
                        !ec_subcommeca = dtrT!mo_subcommeca
                        !ec_codcena = dtrT!mo_codcena
                        !ec_coddivi = dtrT!mo_coddivi
                        !ec_desint = dtrT!mo_desint
                        !ec_codvuo = dtrT!mo_codvuo
                        !ec_confermato = dtrT!mo_confermato
                        !ec_lotto = dtrT!mo_lotto
                        !ec_ricimp = dtrT!mo_ricimp
                        !ec_misura1 = dtrT!mo_misura1
                        !ec_misura2 = dtrT!mo_misura2
                        !ec_misura3 = dtrT!mo_misura3
                        !ec_umprz = dtrT!mo_umprz
                        !ec_matric = dtrT!mo_matric
                        !ec_datini = dtrT!mo_datini
                        !ec_datfin = dtrT!mo_datfin
                      End With
                      If Not oCleGsor.RecordSalva(oCleGsor.dttEC.Rows.Count - 1, False, Nothing) Then
                        oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1).Delete()
                      End If
                    Next    'For Each dtrT As DataRow In dttTmp.Rows

                  End If    'If oCleGsor.GetMotransDaGestNuov(NTSCInt(Trim$(Mid(strParam, 34, 9))), dtttmp) Then
                End If
              End If
            Case "NUOD"
              If tlbNuovo.Visible And tlbNuovo.Enabled Then
                'es.chiamato da consolle clienti/fornitori/articoli -> nuovo impegno cliente
                'se il comando è stato inibito con CTRL+ALT+CLICK e da BN__FLDO cerco di aprirlo, esco
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & strParam(1) & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = strParam(1)
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edAnnoDoc.Text = NTSCInt(strParam(2)).ToString
                  tlbNuovo_ItemClick(tlbNuovo, Nothing)
                  If oCleGsor.dttET.Rows.Count = 0 Then Return
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
                If CType(cbTipoDoc.DataSource, DataTable).Select("cod = '" & strParam(1) & "'").Length = 0 Then Return
                cbTipoDoc.SelectedValue = strParam(1)
                If cbTipoDoc.SelectedIndex = -1 Then
                  cbTipoDoc.SelectedIndex = 0
                Else
                  edAnnoDoc.Text = NTSCInt(strParam(2)).ToString
                  edSerieDoc.Text = strParam(3)
                  edNumDoc.Text = NTSCInt(strParam(4)).ToString
                  tlbApri_ItemClick(tlbApri, Nothing)
                End If
                'se è stato passato anche il numero di riga (numerico di 9 caratteri) ...
                If oCallParams.strParam.Length > 24 Then
                  tsGsor.SelectedTabPageIndex = 1
                  grRighe.Focus()
                  For i As Integer = 0 To dcGsorRighe.List.Count - 1
                    If NTSCInt(CType(dcGsorRighe.Item(i), DataRowView)!ec_riga) = NTSCInt(strParam(5)) Then
                      dcGsorRighe.Position = i
                      Exit For
                    End If
                  Next
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
            If strParams(0) = dttOpens.Rows(z)!td_tipork.ToString And strParams(1) = dttOpens.Rows(z)!td_anno.ToString And _
               strParams(2) = dttOpens.Rows(z)!td_serie.ToString And strParams(3) = dttOpens.Rows(z)!td_numord.ToString Then
              cbTipoDoc.SelectedValue = dttOpens.Rows(z)!td_tipork.ToString
              edAnnoDoc.Text = dttOpens.Rows(z)!td_anno.ToString
              edSerieDoc.Text = dttOpens.Rows(z)!td_serie.ToString
              edNumDoc.Text = dttOpens.Rows(z)!td_numord.ToString

              tlbApri_ItemClick(tlbApri, Nothing)
              bTrovato = True
              Exit For
            End If
          Next
          If Not bTrovato Then
            cbTipoDoc.SelectedValue = dttOpens.Rows(0)!td_tipork.ToString
            edAnnoDoc.Text = dttOpens.Rows(0)!td_anno.ToString
            edSerieDoc.Text = dttOpens.Rows(0)!td_serie.ToString
            edNumDoc.Text = dttOpens.Rows(0)!td_numord.ToString

            tlbApri_ItemClick(tlbApri, Nothing)
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRMORGSOR_T_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "RecordOrdinaModo", NTSCStr(oCleGsor.nOrdrig), " ", "NS.", "...", "...")
      '-------------------------------------------------
      'se necessario salvo il documento
      If tsGsor.Visible = True Then
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
        oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "LastTipoDoc", cbTipoDoc.SelectedValue.ToString, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "LastAnnoDoc", edAnnoDoc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "LastSerieDoc", edSerieDoc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "ReportStampaEtiFin", NTSCStr(IIf(tlbStampaEtichetteFinale.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
        oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "Salvanuovo", NTSCStr(IIf(tlbSalvanuovo.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
        oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "RiapriDopoSalva", NTSCStr(IIf(tlbRiapriDopoSalva.Checked, "-1", "0")), " ", ".S.", ".N.", ".N.")
      End If

      If oCleGsor.bModPM Then
        oMenu.ResetTblInstId("TTPROESEC", False, oCleGsor.lIIttproesebappo)
        oMenu.ResetTblInstId("TTTASKS", False, oCleGsor.lIItttasks)
      End If
      oCleGsor.RemoveHandlerEntity()
      oCleGsor.DocumentLockRemove()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGSOR_T_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRMORGSOR_T_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    'se da BNDKKONS trascino delle righe d'ordine da trasformare in documento ....
    Dim strData As String = ""
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing
    Dim nStrT As Integer = 0
    Try
      strData = NTSCStr(e.Data.GetData(DataFormats.Text))
      If strData.Trim = "" Then Return
      strT = strData.Split("|"c)
      If strT(0) <> "BNDKKONS:Articoli" Then Return 'non sono stato chiamato da BNDKKONS pannello ordini

      If grvRighe.NTSAllowInsert = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129684328520294352, "La griglia è impostata per non accettare nuove righe"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      For nStrT = 1 To strT.Length - 1
        strT1 = strT(nStrT).Split(";"c)

        If nStrT = 1 Then
          If pnCorpo.Visible Then
            'documento già inizializzato
            If NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_conto) <> NTSCInt(strT1(2)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129684160866751675, "L'intestatario del documento e' diverso da quello delle righe da inserire"))
              Return
            End If
          Else
            'documento non ancora inizializzato
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            'dati di testata
            dsGsor.Tables("TESTA").Rows(0)!et_conto = NTSCInt(strT1(2))
          End If

          If oCleGsor.OkTestata() Then
            tsGsor.SelectedTabPageIndex = 1
            grRighe.Focus()
            grvRighe.FocusedColumn = ec_codart
          End If
        End If    'If nStrT = 1 Then

        '------------------------
        'ora aggiungo le righe del corpo
        'attivo la nuova riga
        grvRighe.NTSNuovo()
        grvRighe.Focus()
        grvRighe.FocusedColumn = ec_codart
        NTSSendKeys.SendWait(0, "0")
        grvRighe.SetFocusedValue(strT1(0))
        'inserisco le altre informazioni
        grvRighe.NTSGetCurrentDataRow!ec_fase = NTSCInt(strT1(1))
        grvRighe.NTSGetCurrentDataRow!ec_magaz = NTSCInt(strT1(4))
        grvRighe.NTSGetCurrentDataRow!ec_datcons = NTSCDate(strT1(3))
        grvRighe.NTSGetCurrentDataRow!ec_quant = NTSCDec(strT1(5))
        tlbRicalcolaColliQta_ItemClick(tlbRicalcolaColliQta, Nothing)
        If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then
          tlbRecordRipristina_ItemClick(tlbRecordRipristina, Nothing)

        End If

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
      If edEt_conto.ContainsFocus Or edEt_contfatt.ContainsFocus Then
        If oCleGsor.bDocEmesso Then
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

  Public Overridable Sub cbTipoDoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoDoc.SelectedIndexChanged
    Dim strSoloSerie As String
    Try
      edNumDoc.Text = "0"
      If Not bNoResetDttOpens Then dttOpens.Clear()
      If cbTipoDoc.SelectedIndex = -1 Then cbTipoDoc.SelectedIndex = 0
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
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
        End If
      End If
      NTSGetDataAutocompletamentoTipork = cbTipoDoc.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edAnnoDoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnnoDoc.ValidatedAndChanged
    Try
      edNumDoc.Text = "0"
      If Not bNoResetDttOpens Then dttOpens.Clear()
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

  Public Overridable Sub edEt_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edEt_coddest.NTSZoomGest
    'limite superato e.ZoomHandled = True        'non facci eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub

  Public Overridable Sub edEt_coddest2_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edEt_coddest2.NTSZoomGest
    'limite superato e.ZoomHandled = True        'non facci eseguire la routine standard del ALT+F2 o ALT+F3
  End Sub

  Public Overridable Sub edEt_conto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_conto.Validated
    Try
      If oCleGsor Is Nothing Then Return
      If bInLoadOrd = True Then Return
      If oCleGsor.bInUnload Then Return

      '---------------------
      'CRM: testo se posso agire con il conto selezionato
      If NTSCInt(edEt_conto.Text) = NTSCInt(edEt_conto.NTSOldValue) And dsGsor.Tables("TESTA").Rows.Count > 0 And NTSCInt(edEt_conto.Text) <> 0 Then
        If oCleGsor.bModuloCRM And oCleGsor.bIsCRMUser And NTSCStr(dsGsor.Tables("TESTA").Rows(0)!xx_tipo).Trim <> "" Then
          If NTSCStr(dsGsor.Tables("TESTA").Rows(0)!xx_tipo) = "C" Then
            oCleGsor.bAccvis = oMenu.CercaAccessiCrmDaLead(DittaCorrente, oCleGsor.lLead, oCleGsor.bAccmod)
            If oCleGsor.bAccvis And oCleGsor.bNew = False Then
              oCleGsor.bAccvis = oMenu.CercaAccessiCrmDaTestord(DittaCorrente, cbTipoDoc.SelectedValue, NTSCInt(edAnnoDoc.Text), NTSCStr(edSerieDoc.Text), NTSCInt(edNumDoc.Text), oCleGsor.bAccmod)
            End If
            If oCleGsor.bAccvis = False Then
              oCleGsor.bNonValidare = False
              edEt_conto.Text = "0"
              lbXx_conto.Text = ""
              oApp.MsgBoxErr(oApp.Tr(Me, 128776134372020000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi al Cliente di questo Ordine/Impegno."))
            End If
          Else
            If oCleGsor.bAmm = False Then
              oCleGsor.bNonValidare = False
              edEt_conto.Text = "0"
              lbXx_conto.Text = ""
              oApp.MsgBoxErr(oApp.Tr(Me, 128776134421316000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi ai fornitori."))
            End If
          End If
        End If      ' If oCleGsor.bModuloCRM And oCleGsor.bIsCRMUser Then
      End If


      If NTSCInt(edEt_valuta.Text) = 0 Then
        edEt_cambio.Enabled = False
      Else
        GctlSetVisEnab(edEt_cambio, False)
      End If
      SettaCampiValuta()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edEt_codese_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_codese.Validated
    'se faccio in questo modo, se indico un codice iva non corretto l'entity da il messaggio, 
    'rimette il codice iva originale ma poi quando arrivo qui da 
    'comunque il messaggio di codice iva da variare su tutte le righe del corpo
    'If NTSCInt(edEt_codese.Text) <> 0 And dsGsor.Tables("CORPO").Rows.Count > 0 Then
    '  If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791955669687500, "Modificare il Codice IVA su tutte le righe di questo ordine?")) = Windows.Forms.DialogResult.Yes Then
    '    oCleGsor.CambioDatiTestataInGriglia(NTSCInt(edEt_codese.Text), 0, 0, 0, 0, 0, "", "")
    '  End If
    'End If
  End Sub

  Public Overridable Sub edEt_codpaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEt_codpaga.Validated
    Try
      If oCleGsor Is Nothing Then Return

      If NTSCInt(edEt_codpaga.Text) = 0 Then
        edEt_datapag.Enabled = False
      Else
        If oCleGsor.GetTipoPag(edEt_codpaga.Text) = 4 Then
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
      If oCleGsor Is Nothing Then Return
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
        If dsGsor.Tables("CORPO").Rows.Count > 0 Then
          'se sono sulla cella 'valore riga' non aggiorna il valore di quella riga ...
          grvRighe.NTSMoveFirstColunn()
        End If
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128521436577992000, "Inserire il cambio indicato nella tabella dei CAMBI per la valuta '|" & _
                            edEt_valuta.Text & "|'")) = Windows.Forms.DialogResult.Yes Then
          oCleGsor.AggiornaCambio(NTSCInt(edEt_valuta.Text), NTSCDate(edet_datdoc.Text).ToShortDateString, NTSCDec(edEt_cambio.Text), True)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckEt_scorpo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckEt_scorpo.CheckedChanged
    If ckEt_scorpo.Checked = True Then
      ec_preziva.Visible = True
      xxo_valoreiva.Visible = True
      GctlSetVisEnab(ec_preziva, False)
      ec_prezzo.Enabled = False
    Else
      ec_preziva.Visible = False
      ec_preziva.Enabled = False
      xxo_valoreiva.Visible = False
      If bInLoadOrd = False Then GctlSetVisEnab(ec_prezzo, False)
    End If
  End Sub

#Region "Eventi di griglia"

  Public Overridable Sub grRighe_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grRighe.Enter
    Try
      If oCleGsor.OkTestata() Then
        'If grvRighe.FocusedRowHandle = -999999 And grvRighe.RowCount > 0 Then
        '  'mi devo posizionare sulla prima cella in alto a SX
        'End If
      Else
        tsGsor.Focus()
      End If

      If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) = "S" Then
        'documento retail: non posso modificare i valori di riga
        ec_codart.Enabled = False
        ec_fase.Enabled = False
        ec_unmis.Enabled = False
        ec_colli.Enabled = False
        ec_quant.Enabled = False
        ec_prezzo.Enabled = False
        ec_prezvalc.Enabled = False
        ec_scont1.Enabled = False
        ec_scont2.Enabled = False
        ec_scont3.Enabled = False
        ec_scont4.Enabled = False
        ec_scont5.Enabled = False
        ec_scont6.Enabled = False
        ec_codiva.Enabled = False
        ec_stasino.Enabled = False
      End If


      '----------------------------
      'devo bloccare la colomma ec_valoremm se necessario
      '"Impossibile immettere un valore globale per la riga per gli ORDINI DI PRODUZIONE."
      '"Impossibile immettere un valore globale per la riga per i documenti in valuta o IVA compresa (scorporo)."
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Or ckEt_scorpo.Checked Or NTSCInt(edEt_valuta.Text) <> 0 Then
        ec_valoremm.Enabled = False
      Else
        If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) <> "S" Then
          GctlSetVisEnab(ec_valoremm, False)
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
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then
        grRighe.Focus()
        Return
      End If
      dsGsor.Tables("CORPO").AcceptChanges()
      SettaCampiValuta()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grRighe_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grRighe.KeyPress
    Try
      ''memorizzo il nome della colonna che ha fatt ogenerare la nuova riga, per riproporla nel momento in cui dovrò inserirne un'altra
      'If grvRighe.NTSGetCurrentDataRow Is Nothing Then
      '  If grvRighe.FocusedColumn.Name = "ec_codart" Or grvRighe.FocusedColumn.Name = "ec_matric" Then
      '    strLastColEdited = grvRighe.FocusedColumn.Name
      '  End If
      'End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
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

        Case "EC_VALOREMM"
          'ho cambiato il valore a mano: devo ricalcolare il prezzo all'indietro
          With grvRighe.NTSGetCurrentDataRow
            If NTSCDec(!ec_scontv) <> 0 And NTSCStr(oCleGsor.dttET.Rows(0)!et_scorpo) = "S" Then
              oCleGsor.Scorporo(NTSCDec(!ec_scontv), NTSCInt(!ec_codiva), dScontV, 0)
            Else
              dScontV = NTSCDec(!ec_scontv)
            End If

            If !ec_umprz.ToString <> "S" Then
              If NTSCDec(!ec_quant) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl(((NTSCDec(!ec_valoremm) / NTSCDec(!ec_quant) * NTSCDec(!ec_perqta)) / ((100 - NTSCDec(!ec_scont1)) / 100) / ((100 - NTSCDec(!ec_scont2)) / 100) / ((100 - NTSCDec(!ec_scont3)) / 100) / ((100 - NTSCDec(!ec_scont4)) / 100) / ((100 - NTSCDec(!ec_scont5)) / 100) / ((100 - NTSCDec(!ec_scont6)) / 100) / ((100 - NTSCDec(!ec_scontp)) / 100) - dScontV) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scont1)) / 100) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scont2)) / 100) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scopag)) / 100), oMenu.TrovaNdecSuPrzUn(0))
              End If
            Else
              If NTSCDec(!ec_colli) = 0 Then
                dPrezzo = 0
              Else
                dPrezzo = ArrDbl(((NTSCDec(!ec_valoremm) / NTSCDec(!ec_colli) * NTSCDec(!ec_perqta)) / ((100 - NTSCDec(!ec_scont1)) / 100) / ((100 - NTSCDec(!ec_scont2)) / 100) / ((100 - NTSCDec(!ec_scont3)) / 100) / ((100 - NTSCDec(!ec_scont4)) / 100) / ((100 - NTSCDec(!ec_scont5)) / 100) / ((100 - NTSCDec(!ec_scont6)) / 100) / ((100 - NTSCDec(!ec_scontp)) / 100) - dScontV) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scont1)) / 100) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scont2)) / 100) / ((100 - NTSCDec(oCleGsor.dttET.Rows(0)!et_scopag)) / 100), oMenu.TrovaNdecSuPrzUn(0))
              End If
            End If
            If NTSCDec(!ec_prezzo) <> dPrezzo Then !ec_prezzo = dPrezzo
            oCleGsor.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
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
      If tsGsor.Visible = False Then Return
      If bInDeleteRow Then Return 'passo di qui erroneamente se cancello una riga quando sono in abilita/disabilita sort/filter: non devo fare la recordsalva!!!!
      If oCleGsor.bInDuplicadoc Then Return

      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then e.Allow = False 'rimango sulla riga su cui sono

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
      If dsGsor.Tables("CORPO") Is Nothing Then Return
      If dsGsor.Tables("CORPO").Rows.Count = 0 Then Return
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

      '-------------------------------------------------
      'visualizzo o meno la griglia TCO
      GestisciGrigliaTCO()

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
      If NTSCInt(grvRighe.GetFocusedRowCellValue(xxo_codtagl).ToString.Trim) <> 0 Then
        ec_quant.Enabled = False
      Else
        If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) = "S" Then
          ec_quant.Enabled = False
        Else
          GctlSetVisEnab(ec_quant, False)
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

      '--------------------------------------
      'visualizzo la form modale per il dettaglio TCO (solo su nuove righe e solo su articoli TCO)
      If oCleGsor.bModTCO Then
        If grvRighe.FocusedColumn.Name = "ec_quant" Then
          If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
              If NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) <> 0 Then tlbDettaglioTCO_ItemClick(tlbDettaglioTCO, Nothing)
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
      If oCleGsor Is Nothing Then Return True
      If bInAbilitaCellaEnter Then Return True
      If NTSCInt(edEt_conto.Text) = 0 Then Return True
      If oCleGsor.bCodartDaBarcode Then
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

      If oCleGsor Is Nothing Then Return True
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
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleGsor.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
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
        oApp.MsgBoxErr(oApp.Tr(Me, 129048478401282387, "Posizionarsi prima su una riga con codice articolo impostato"))
        Return
      End If
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776135447176000, "Indicare prima il codice articolo"))
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

  Public Overridable Sub grRighe_EditorKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grRighe.EditorKeyDown
    Try
      If grvRighe.FocusedColumn.Name = "ec_codart" Then
        'memorizzo i caratteri effettivamente digitati nel cod. articolo: se ho inserito l'articolo con barcode 
        'di tipo EAN128 possono esserci caratteri di separatore di campo non presenti nella beforecolupdate e.proposedvalue
        oCleGsor.strEAN128Barcode += e.KeyCode.ToString & "§"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub edEt_totcoll_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_totcoll.KeyPress
    If e.KeyChar <> Chr(13) Then oCleGsor.bCalcolaColli = False
  End Sub

  Public Overridable Sub edEt_pesonetto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_pesonetto.KeyPress
    If e.KeyChar <> Chr(13) Then oCleGsor.bCalcolaPesoNetto = False
  End Sub

  Public Overridable Sub edEt_peso_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles edEt_peso.KeyPress
    If e.KeyChar <> Chr(13) Then oCleGsor.bCalcolaPesoLordo = False
  End Sub


  Public Overridable Sub tsGsor_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsGsor.SelectedPageChanged
    Try
      If oCleGsor.bInUnload Then Return
      Me.Cursor = Cursors.WaitCursor
      '------------------------------
      'ricalcolo i totali del documento quando esco dal corpo
      If e.PrevPage.Equals(tsGsor.TabPages(1)) Then
        If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then
          tsGsor.SelectedTabPageIndex = 1
          Return
        End If
        Me.NTSFormClearDataBinding(Me)
        oCleGsor.CalcolaTotali()
        Bindcontrols()
      End If

      If e.Page.Equals(tsGsor.TabPages(1)) Then
        GctlSetVisEnab(tlbStampaEtichetteRiga, False)
        GctlSetVisEnab(tlbMnuRecord, False)
        GctlSetVisEnab(tlbRecordRipristina, False)
        If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) <> "S" Then
          GctlSetVisEnab(tlbRecordCancella, False)
          GctlSetVisEnab(tlbDettaglioTCO, False)
        End If

        If oCleGsor.bCodartDaBarcode Then
          Application.DoEvents()
          grvRighe.ShowEditor()
          grvRighe.ShowEditorByMouse()
        End If
      Else
        tlbStampaEtichetteRiga.Enabled = False
        tlbMnuRecord.Enabled = False
        tlbRecordCancella.Enabled = False
        tlbRecordRipristina.Enabled = False
        tlbDettaglioTCO.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function AbilitaDisabilitaRigaNum() As Boolean
    Try
      ec_riga.Enabled = False
      If oCleGsor.AbilitaDisabilitaRigaNum(grvRighe.NTSGetCurrentDataRow) Then
        GctlSetVisEnab(ec_riga, False)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Voci di Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim lNumDocTmp As Integer = 0
    Dim strSoloSerie As String = ""
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

      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776135528764000, "L'utente è abilitato a creare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        edSerieDoc.Text = strSoloSerie.ToUpper
        edNumDoc.Text = "0"
      End If

      '----------------------------------------------------
      'assegno il numero del documento
      lNumDocTmp = NTSCInt(edNumDoc.Text)
      lNumDocTmp = oCleGsor.LegNuma(IIf(cbTipoDoc.SelectedValue.ToString() = "V", "VV", cbTipoDoc.SelectedValue.ToString()).ToString, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text))
      If lNumDocTmp = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115000000, "Prima di creare un nuovo ordine è necessario attivare la numerazione del documento"))
        Return
      End If
      If edNumDoc.Text = "0" Then edNumDoc.Text = lNumDocTmp.ToString

      '----------------------------------------------------
      'creo il nuovo record 
      bInLoadOrd = True
      Me.Cursor = Cursors.WaitCursor
      'If cbTipoDoc.SelectedValue.ToString <> oCleGsor.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      NTSFormClearDataBinding(Me)

      '----------------------------------------
      'se l'ambiente non era stato caricato, lo carico ora
      If dsGsor.Tables.Count < 3 Then
        If Not oCleGsor.ApriOrdine(DittaCorrente, True, "R", 0, " ", 0, dsGsor) Then Return
      End If

      If oCleGsor.NuovoOrdine(DittaCorrente, cbTipoDoc.SelectedValue.ToString, NTSCInt(edAnnoDoc.Text), _
                                 edSerieDoc.Text, NTSCInt(edNumDoc.Text)) = False Then Return
      dsGsor.Tables("TESTA").AcceptChanges()

      dcGsor.DataSource = dsGsor.Tables("TESTA")
      Bindcontrols()

      '----------------------------------------------------
      'ricollego la griglia al datatable di movord
      dcGsorRighe.DataSource = dsGsor.Tables("CORPO")
      grRighe.DataSource = dcGsorRighe
      bInLoadOrd = False

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      NumeroPost()
      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) 'bRicaricaGctl)

      '----------------------------------------------------
      'menu non abilitati quando faccio 'nuovo'
      tlbNuovoOrdineDaPrec.Enabled = False
      tlbCancella.Enabled = False

      '----------------------------------------------------
      'disabilito i campi in valuta e correggo la visibilit della colonna PREZZO CON IVA
      SettaCampiValuta()
      ckEt_scorpo_CheckedChanged(Me, Nothing)

      'di solito si inseriscono documenti nella data odierna ...
      edEt_conto.Focus()

      If lContoProposto <> 0 Then edEt_conto.NTSTextDB = NTSCStr(lContoProposto)

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
    Try
      If Not Salva(-1, False) Then Return
      '----------------------------------------------------
      'resetto tutto
      SetStato(0, False)
      edSerieDoc.Text = edSerieDoc.Text.ToUpper
      Me.ValidaLastControl()

      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" And strSoloSerie.ToUpper <> edSerieDoc.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128534164320642000, "L'utente è abilitato a modificare solo documenti di serie '|" & strSoloSerie.ToUpper & "|'"))
        Return
      End If

      '----------------------------------------------------
      'testo il numero del documento
      If NTSCInt(edNumDoc.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115312500, "Prima di aprire un nuovo ordine è necessario indicare il numero del documento"))
        Return
      End If

      '----------------------------------------------------
      'se avevo dei recent tra i documenti da aprire, verifico se il documento che sto aprendo è tra quelli: se non lo è pulisco il recent
      If bDaTastiNavigazione = False Then
        tlbApriDocPrec.Enabled = False
        tlbApriDocSucc.Enabled = False
      End If
      If dttOpens.Rows.Count > 0 Then
        If dttOpens.Select("td_tipork = " & CStrSQL(cbTipoDoc.SelectedValue) & _
                          " AND td_anno = " & NTSCInt(edAnnoDoc.Text).ToString & _
                          " AND td_serie = " & CStrSQL(edSerieDoc.Text) & _
                          " AND td_numord = " & NTSCInt(edNumDoc.Text).ToString).Length = 0 Then
          dttOpens.Clear()
        Else
          If dttOpens.Rows.Count > 1 Then
            If bDaTastiNavigazione = False Then GctlSetVisEnab(tlbApriDocSucc, False)
          End If
        End If
      End If

      '----------------------------------------------------
      'apro l'ordine
      Me.Cursor = Cursors.WaitCursor
      bInLoadOrd = True
      'If cbTipoDoc.SelectedValue.ToString <> oCleGsor.strLastTiporkRegistroDoc Then bRicaricaGctl = True
      NTSFormClearDataBinding(Me)
      If oCleGsor.ApriOrdine(DittaCorrente, False, cbTipoDoc.SelectedValue.ToString, _
                                 NTSCInt(edAnnoDoc.Text), edSerieDoc.Text, NTSCInt(edNumDoc.Text), _
                                 dsGsor) = False Then Return
      dsGsor.Tables("TESTA").AcceptChanges()
      If dsGsor.Tables("TESTA").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115468750, "Non è stato trovato nessun documento con le caratteristiche richieste"))
        bInLoadOrd = False
        Return
      Else
        dcGsor.DataSource = dsGsor.Tables("TESTA")
        dcGsor.MoveFirst()
        Bindcontrols()
        NumeroPost()

        dcGsorRighe.DataSource = dsGsor.Tables("CORPO")
        grRighe.DataSource = dcGsorRighe
      End If

      '----------------------------------------------------
      'CRM
      If oCleGsor.bRipristinaDocumento = True Then
        Ripristina()
        Return
      End If

      '----------------------------------------------------
      'setto le variabili non collegate al datatable
      SetUnboundFiled(0)

      '----------------------------------------------------
      'attivo i controlli
      SetStato(1, False) ' bRicaricaGctl)

      '-----------------------------------------------------------------------------------------
      'Se è attivo il modulo CRM, il conto di testata è un Cliente e non è permessa la
      'modifica in CRMOP, disabilita i pulsanti e le voci di menu relative a:
      'Aggiorna
      'Cancella
      If oCleGsor.bModuloCRM And oCleGsor.bIsCRMUser And oCleGsor.bAccmod = False Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
      End If

      '----------------------------------------------------
      'abilito/disabilito i campi in valuta
      SettaCampiValuta()
      If dsGsor.Tables("CORPO").Rows.Count > 0 Then  'altrimenti non disabilita i campi in testata
        ckEt_scorpo.Enabled = False
        edEt_valuta.Enabled = False
      Else
        If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
          edEt_valuta.Enabled = False
        End If
      End If
      ckEt_scorpo_CheckedChanged(Me, Nothing)  'altrimenti non abilita la colonna 'prezzo con iva' in griglia

      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "Q" Then GctlSetVisEnab(ckEt_flevas, False)

      bInLoadOrd = False
      '----------------------------------------------------
      'ricalcolo i totali del documento
      Me.NTSFormClearDataBinding(Me)
      oCleGsor.CalcolaTotali()
      Bindcontrols()

      '----------------------------------------------------
      If oCleGsor.strVisNoteContoApertura = "S" And NTSCStr(oCleGsor.dttEt_conto.Rows(0)!an_note2).Trim <> "" Then
        Dim frmMsgb As FRM__MSGB = Nothing
        frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
        frmMsgb.Init(oMenu, Nothing, DittaCorrente)
        frmMsgb.cmdZoom.Visible = False
        frmMsgb.cmdAnnulla.Visible = False
        frmMsgb.edMess.Text = NTSCStr(oCleGsor.dttEt_conto.Rows(0)!an_note2).Trim
        frmMsgb.Text = "NOTE RELATIVE AL CONTO " & oCleGsor.lContoCF.ToString
        frmMsgb.ShowDialog()
        frmMsgb.Dispose()
        frmMsgb = Nothing
      End If

      '----------------------------------------------------
      'blocchi se ho aperto un documento proveniente da retail nuovo
      If oCleGsor.bModGPV Then SetBlocchiRetail(False)

      grvRighe.LeftCoord = 0
      If ec_matric.Visible Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      ColoraCampoAbiCab()
    Catch ex As Exception
      bInLoadOrd = False
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
          If NTSCStr(dttOpens.Rows(i)!td_tipork) = strLastTipork And _
             NTSCInt(dttOpens.Rows(i)!td_anno) = nLastAnno And _
             NTSCStr(dttOpens.Rows(i)!td_serie).ToUpper = strLastSerie.ToUpper And _
             NTSCInt(dttOpens.Rows(i)!td_numord) = lLastNumdoc Then
            bOk = True
            Exit For
          End If
        Next

        If bOk = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129496853554558202, "Tra i recent dell'ultima ricerca non è stato trovato l'ultimo documento aperto. I recent verranno svuotati"))
          dttOpens.Clear()
          tlbApriDocPrec.Enabled = False
          tlbApriDocSucc.Enabled = False
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

        cbTipoDoc.SelectedValue = dttOpens.Rows(i)!td_tipork.ToString
        edAnnoDoc.Text = dttOpens.Rows(i)!td_anno.ToString
        edSerieDoc.Text = dttOpens.Rows(i)!td_serie.ToString
        edNumDoc.Text = dttOpens.Rows(i)!td_numord.ToString
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
          If NTSCStr(dttOpens.Rows(i)!td_tipork) = strLastTipork And _
             NTSCInt(dttOpens.Rows(i)!td_anno) = nLastAnno And _
             NTSCStr(dttOpens.Rows(i)!td_serie).ToUpper = strLastSerie.ToUpper And _
             NTSCInt(dttOpens.Rows(i)!td_numord) = lLastNumdoc Then
            bOk = True
            Exit For
          End If
        Next

        If bOk = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129496847590505468, "Tra i recent dell'ultima ricerca non è stato trovato l'ultimo documento aperto. I recent verranno svuotati"))
          dttOpens.Clear()
          tlbApriDocPrec.Enabled = False
          tlbApriDocSucc.Enabled = False
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

        cbTipoDoc.SelectedValue = dttOpens.Rows(i)!td_tipork.ToString
        edAnnoDoc.Text = dttOpens.Rows(i)!td_anno.ToString
        edSerieDoc.Text = dttOpens.Rows(i)!td_serie.ToString
        edNumDoc.Text = dttOpens.Rows(i)!td_numord.ToString
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
      bNewTmp = oCleGsor.bNew
      If Not Salva(-1, False) Then Return

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      '-------------------------------------------------
      'cancello il documento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791958837031250, "Cancellare il documento?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleGsor.bInUnload = True
          Me.Cursor = Cursors.WaitCursor

          'annullo la riga in corso di inserimento
          If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
              If Not oCleGsor.RecordRipristina(dcGsorRighe.Position, dcGsorRighe.Filter) Then Return
            End If
          End If

          If Not oCleGsor.SalvaOrdine("D") Then Return
          '----------------------------------------------------------------------------------------------------------
          If dttOpens.Rows.Count > 1 Then
            Dim bOk As Boolean = False
            Dim bRiapriDocSuSalvaTmp As Integer = oCleGsor.bRiapriDocSuSalva
            Dim i As Integer = 0
            Dim dtrRow() As DataRow = Nothing
            For i = 0 To (dttOpens.Rows.Count - 1)
              With dttOpens.Rows(i)
                If NTSCStr(!td_tipork) = NTSCStr(cbTipoDoc.SelectedValue) And _
                   NTSCInt(!td_anno) = NTSCInt(edAnnoDoc.Text) And _
                   NTSCStr(!td_serie).ToUpper = edSerieDoc.Text And _
                   NTSCInt(!td_numord) = NTSCInt(edNumDoc.Text) Then
                  bOk = True
                  Exit For
                End If
              End With
            Next
            If i > 0 Then i -= 1
            dtrRow = dttOpens.Select("td_tipork = " & CStrSQL(cbTipoDoc.SelectedValue) & _
                                     " AND td_anno = " & NTSCInt(edAnnoDoc.Text) & _
                                     " AND td_serie = " & CStrSQL(edSerieDoc.Text) & _
                                     " AND td_numord = " & NTSCInt(edNumDoc.Text))
            If dtrRow.Length > 0 Then dtrRow(0).Delete()
            dttOpens.AcceptChanges()
            oCleGsor.bInUnload = False
            oCleGsor.bRiapriDocSuSalva = -1
            cbTipoDoc.SelectedValue = dttOpens.Rows(i)!td_tipork.ToString
            edAnnoDoc.Text = dttOpens.Rows(i)!td_anno.ToString
            edSerieDoc.Text = dttOpens.Rows(i)!td_serie.ToString
            edNumDoc.Text = dttOpens.Rows(i)!td_numord.ToString
            tlbApri_ItemClick(tlbApri, Nothing)
            oCleGsor.bRiapriDocSuSalva = bRiapriDocSuSalvaTmp
            oCleGsor.RipristinaDocumento()
            Return
          End If
          '----------------------------------------------------------------------------------------------------------
          SetStato(0, False)
          oCleGsor.bInUnload = False 'Dopo SetStato o da errore se il focus è sul campo Conto e il cliente è anche un lead
          edNumDoc.Text = "0"
          edNumDoc.SelectAll()
          Return
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleGsor.bInUnload = False
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
    Dim frmSedo As FRMORSEDO = Nothing
    Dim oPar As CLE__CLDP

    Try
      '------------------------------------
      'zoom specifico per le destinazioni diverse (devo passare il conto cliente/fornitore)
      If edEt_conto.Focused Then
        SetFastZoom(edEt_conto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edEt_conto.Text
        oParam.bVisGriglia = True
        oParam.bTipoProposto = True
        oParam.nMastro = 0
        Select Case dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString()
          Case "R", "Q", "V"
            oParam.strTipo = "C"
          Case Else
            oParam.strTipo = "F"
        End Select
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

        oParam.lContoCF = oCleGsor.lContoCF   'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)

        If edEt_coddest.Focused Then
          If NTSZOOM.strIn <> edEt_coddest.Text Then edEt_coddest.NTSTextDB = NTSZOOM.strIn
        Else
          If NTSZOOM.strIn <> edEt_coddest2.Text Then edEt_coddest2.NTSTextDB = NTSZOOM.strIn
        End If

      ElseIf edNumDoc.Focused Or cbTipoDoc.Focused Then
        '------------------------------------
        'zoom ordini 
        If tsGsor.Visible = True Then Return 'esco se sono dentro ad un documento

        frmSedo = CType(NTSNewFormModal("FRMORSEDO"), FRMORSEDO)
        oPar = New CLE__CLDP
        oPar.strPar1 = cbTipoDoc.SelectedValue.ToString
        oPar.strPar2 = edSerieDoc.Text
        oPar.strPar3 = edAnnoDoc.Text
        frmSedo.Init(oMenu, oPar, DittaCorrente)
        frmSedo.InitEntity(oCleGsor)
        frmSedo.ShowDialog()

        If oPar.bPar1 = False Then Return

        If Not frmSedo.dttOut Is Nothing Then
          'la selezione documenti mi ha passato un elenco di documenti da aprire: memorizzo l'elenco
          dttOpens = frmSedo.dttOut.Copy
          frmSedo.dttOut.Clear()
          frmSedo.dttOut = Nothing
        End If
        If dttOpens.Rows.Count > 1 Then
          GctlSetVisEnab(tlbApriDocPrec, False)
          GctlSetVisEnab(tlbApriDocSucc, False)
          cbTipoDoc.SelectedValue = dttOpens.Rows(0)!td_tipork.ToString
          edAnnoDoc.Text = dttOpens.Rows(0)!td_anno.ToString
          edSerieDoc.Text = dttOpens.Rows(0)!td_serie.ToString
          edNumDoc.Text = dttOpens.Rows(0)!td_numord.ToString
        Else
          dttOpens.Clear()
          tlbApriDocPrec.Enabled = False
          tlbApriDocSucc.Enabled = False
          edSerieDoc.Text = oPar.strPar2
          edNumDoc.Text = oPar.strPar4
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
      ElseIf edEt_subcommeca.Focused Then
        '------------------------------------
        'zoom sottocommesse
        If NTSCInt(edEt_commeca.Text) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128776135620336000, "Indicare prima il codice commessa"))
          Return
        End If
        SetFastZoom(edEt_subcommeca.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.lCommessa = NTSCInt(edEt_commeca.Text)   'passo il codice commessa
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edEt_subcommeca.Text And NTSZOOM.strIn <> "" Then edEt_subcommeca.NTSTextDB = NTSZOOM.strIn
      ElseIf edEt_contfatt.Focused Then
        '------------------------------------
        'zoom conto fatturazione
        SetFastZoom(edEt_contfatt.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edEt_contfatt.Text
        oParam.bVisGriglia = True
        oParam.bTipoProposto = True
        oParam.nMastro = 0
        Select Case dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString()
          Case "R", "Q", "V"
            oParam.strTipo = "C"
          Case Else
            oParam.strTipo = "F"
        End Select
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edEt_contfatt.Text Then edEt_contfatt.NTSTextDB = NTSZOOM.strIn
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
            oParam.lContoCF = oCleGsor.lContoCF
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
                    oCleGsor.bInInsertArticoDaZoom = True
                    If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
                    For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                      oCleGsor.dttEC.Rows.Add(oCleGsor.dttEC.NewRow)
                      With oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
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
                      If Not oCleGsor.RecordSalva(oCleGsor.dttEC.Rows.Count - 1, False, Nothing) Then
                        oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1).Delete()
                      End If
                    Next
                    oCleGsor.bInInsertArticoDaZoom = False
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
              oApp.MsgBoxInfo(oApp.Tr(Me, 129048478529410667, "Indicare prima il codice commessa"))
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
              oApp.MsgBoxInfo(oApp.Tr(Me, 128776135459188000, "Indicare prima il codice articolo"))
              Return
            End If
            'oCleGsor.GetMemDttArti(NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)))
            'If NTSCStr(oCleGsor.dttArti.Rows(0)!ar_geslotti) = "N" Then
            '  oApp.MsgBoxInfo(oApp.Tr(Me, 130652685584033091, "Zoom disponibile solo se l'articolo è gestito a lotti"))
            '  Return
            'End If
            NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
            oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
            oParam.nFase = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))
            'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
            oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
            'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
            NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

          ElseIf grvRighe.FocusedColumn.Equals(ec_ubicaz) Then
            '------------------------------------
            'zoom ubicazioni
            If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128776135470888000, "Indicare prima il codice articolo"))
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

          ElseIf edEt_organig.Focused Then
            SetFastZoom(edEt_organig.Text, oParam)
            oParam.lContoCF = NTSCInt(edEt_conto.Text)
            oParam.strTipo = "I"
            NTSZOOM.ZoomStrIn("ZOOMORGANIG", DittaCorrente, oParam)

            If NTSCInt(NTSZOOM.strIn) <> 0 AndAlso NTSCInt(NTSZOOM.strIn) <> NTSCInt(edEt_organig.Text) Then edEt_organig.NTSTextDB = NTSZOOM.strIn

          ElseIf grvRighe.FocusedColumn.Equals(ec_prezzo) Then
            '------------------------------------
            'zoom listini
            If ckEt_scorpo.Checked = False And NTSCInt(edEt_valuta.Text) = 0 Then ApriVisualizzaListini(0)

          ElseIf grvRighe.FocusedColumn.Equals(ec_preziva) Then
            '------------------------------------
            'zoom listini
            If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And Not oCleGsor.bTerzista Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128569600217187500, "Prezzo IVA inclusa non modificabile per le righe di ordine di produzione interno."))
              Return
            Else
              If ckEt_scorpo.Checked Then ApriVisualizzaListini(1)
            End If

          ElseIf grvRighe.FocusedColumn.Equals(ec_prezvalc) Then
            '------------------------------------
            'zoom listini
            If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And Not oCleGsor.bTerzista Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129048478653007581, "Prezzo in valuta non modificabile per le righe di ordine di lavorazioni interne."))
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
            Dim strTipoTask As String = "13578"
            Dim bSoloRilasciati As Boolean = oCleGsor.bPMSoloTaskRilasciatiOR
            Dim strCodart As String = IIf(NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) = "D", "", NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)).ToString
            Dim lCodConto As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_contocontr)
            Dim lOperaio As Integer = 0
            Dim nCodlavo As Integer = 0
            Dim nCodCentroLav As Integer = 0
            Dim nCodCentroCa As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codcena)
            Dim lCodForni As Integer = NTSCInt(edEt_conto.Text)
            Dim nNLayout As Integer = 4
            Dim lInstidPrevent As Integer = 0

            oParam.nTipologia = lInstid
            oParam.lCommessa = lCodCommessa
            oParam.bStanziamenti = bAttNonCompletate
            oParam.bLiv2 = bSoloProgDate
            If oCleGsor.bModPM Then
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

      If edEt_codese.Focused Or edEt_controp.Focused Or edEt_magaz.Focused Or edEt_magaz2.Focused Or edEt_magimp.Focused Or _
        (grRighe.ContainsFocus And (grvRighe.FocusedColumn.Equals(ec_magaz) Or grvRighe.FocusedColumn.Equals(ec_magaz2) Or grvRighe.FocusedColumn.Equals(ec_controp) Or grvRighe.FocusedColumn.Equals(ec_codiva))) Then
        oCleGsor.CaricaTabelleCache()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSedo Is Nothing Then frmSedo.Dispose()
      frmSedo = Nothing
      oCleGsor.bInInsertArticoDaZoom = False
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim dtrDeleted As DataRow = Nothing
    Dim nPos As Integer = 0
    Try
      bInDeleteRow = True
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048478756291475, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.CorpoTestPreCancella(dcGsorRighe.Position) Then Return
      nPos = dcGsorRighe.Position
      If Not grvRighe.NTSDeleteRigaCorrente(dcGsorRighe, True, dtrDeleted) Then Return
      oCleGsor.RecordSalva(nPos, True, dtrDeleted)

      'riabilito, se necessario, il codice valuta
      If oCleGsor.dttEC.Rows.Count = 0 Then
        If ckEt_scorpo.Checked = False Then
          If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
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
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcGsorRighe, CBool(IIf(e Is Nothing, False, True))) Then Return
      If oCleGsor.RecordRipristina(dcGsorRighe.Position, dcGsorRighe.Filter) Then
        grvRighe.NTSRipristinaRigaCorrenteAfter()
      End If
      GestisciGrigliaTCO()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelOFA_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelOFA.ItemClick
    Dim oPar As New CLE__PATB
    Dim dQuant As Decimal = 0
    Dim strCodart As String = ""
    Dim nFase As Integer = 0

    Try
      '---------------------------
      'controlli preliminari
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "R" And oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "O" And oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "#" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128582456087343750, "Funzione utilizzabile solo con Impegni clienti, Impegno di commessa e Ordini fornitore"))
        Return
      End If

      If Not oCleGsor.OkTestata() Then
        tsGsor.Focus()
        Return
      End If

      '-----------------------
      'se è una nuova riga con o senza codice articolo proseguo, se è una riga già precedentemente salvata prima la aggiorno
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If oCleGsor.dttEC.Rows(dcGsorRighe.Position).RowState = DataRowState.Added Then
          'non devo fare nulla
        ElseIf oCleGsor.dttEC.Rows(dcGsorRighe.Position).RowState = DataRowState.Unchanged Then
          'non devo fare nulla
        Else
          If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
        End If
      End If

      If oCleGsor.lConclpriv = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128582475572812500, "Impossibile acquisire ordini/impegni per un cliente generico privato."))
        Return
      End If

      '---------------------------
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
      oPar.strDescr = oCleGsor.GetWhereHlmoOfa(False)
      oPar.strTipo = oCleGsor.dttET.Rows(0)!et_tipork.ToString
      oPar.lContoCF = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto)
      oPar.strCodart = strCodart
      oPar.nFase = nFase
      oPar.dImporto = dQuant
      'oPar.lCommessa = NTSCInt(IIf(oCleBoll.bPassaCommessaTestataHLMO, edEt_commeca.Text, 0))
      oPar.bFlag1 = False 'oCleBoll.bNew
      'oPar.nAnno = NTSCInt(edAnnoDoc.Text)
      'oPar.strAlfpar = edSerieDoc.ToString
      'oPar.lNumreg = NTSCInt(edNumDoc.Text)
      oPar.nTipologia = 2                     '0 solo visualizzaz, 2 = possibilità di selez le righe
      oPar.oParam = Nothing                   'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
      NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo
      If oPar.oParam Is Nothing Then Return
      'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      '------------------------
      'ho selezionato qualche cosa

      '-----------------------
      'se è una nuova riga la annullo, verrà reinserita nella ImportaOrdini
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If oCleGsor.dttEC.Rows(dcGsorRighe.Position).RowState = DataRowState.Added Then
          oCleGsor.RecordRipristina(dcGsorRighe.Position, dcGsorRighe.Filter)
        End If
      End If

      If Not oCleGsor.ImportaOrdini(CType(oPar.oParam, DataTable), "") Then Return

      If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        '...
      End If

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

  Public Overridable Sub tlbSelOfferta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelOfferta.ItemClick
    Dim lContoLead As Integer = 0
    Dim oPar As New CLE__PATB

    Try
      If Not (oCleGsor.bModuloCRM Or oCleGsor.bModuloAS) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135669788000, "Impossibile utilizzare questa funzionalità:" & vbCrLf & _
               "nè il modulo C.R.M., nè il modulo Customer Service," & vbCrLf & _
               "sono abilitati per la ditta corrente," & vbCrLf & _
               "oppure non sono presenti nella chiave di attivazione" & vbCrLf & _
               "fra i moduli acquistati."))
        Return
      End If

      If oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "R" And oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "#" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128580996931875000, "Funzione utilizzabile solo con Impegni clienti e Impegno di commessa"))
        Return
      End If

      If Not oCleGsor.OkTestata() Then
        tsGsor.Focus()
        Return
      End If
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

      If oCleGsor.lConclpriv = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128580982002656250, "Impossibile acquisire offerte per un cliente generico privato."))
        Return
      End If

      '------------------------
      'Chiama lo zoom testate offerte
      lContoLead = oCleGsor.CercaLeadDaConto(NTSCInt(oCleGsor.dttET.Rows(0)!et_conto), NTSCInt(edEt_coddest.Text))
      oPar.strDescr = oCleGsor.GetWhereHlof(lContoLead, NTSCInt(edEt_valuta.Text))
      oPar.lContoCF = lContoLead
      oPar.strIntegr = oCleGsor.strSoloConfermate

      NTSZOOM.ZoomStrIn("ZOOMTESTOFF", DittaCorrente, oPar)
      If oPar.oParam Is Nothing Then Return
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      '------------------------
      'ho selezionato qualche cosa
      If Not oCleGsor.ImportaOfferte(CType(oPar.oParam, DataTable)) Then Return

      If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        '...
      End If

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

  Public Overridable Sub tlbDettaglioTCO_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettaglioTCO.ItemClick
    Dim dTmp As Decimal = 0
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmDqta As FRMORDQTA = Nothing
    Try
      If oCleGsor.bModTCO = False Then Return
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      oCleGsor.GetMemDttArti(grvRighe.NTSGetCurrentDataRow!ec_codart.ToString)
      If oCleGsor.dttArti.Rows.Count = 0 Then Return

      If NTSCInt(oCleGsor.dttArti.Rows(0)!ar_codtagl) <> 0 Then
        If grvRighe.FocusedColumn.Name = "ec_quant" Then
          'mi sposto, altrimenti all'uscita da frmDqta il focus ritorna sulla colonna ec_quant e si riapre la form
          grvRighe.NTSMoveNextColunn()
          'grvRighe.FocusedColumn = ec_ump
        End If
        frmDqta = CType(NTSNewFormModal("FRMORDQTA"), FRMORDQTA)
        frmDqta.Init(oMenu, Nothing, DittaCorrente, Nothing)
        frmDqta.InitEntity(oCleGsor, grvRighe.NTSGetCurrentDataRow)
        frmDqta.dsGsor = dsGsor
        frmDqta.bVisDoc = False
        frmDqta.ShowDialog()

        '-----------------------
        'aggiorno il totale quantità
        dtrT = dsGsor.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString)
        For i = 1 To 24
          dTmp += NTSCDec(dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
        grvRighe.NTSGetCurrentDataRow!ec_quant = dTmp

        GestisciGrigliaTCO()
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128716713542669829, "Articolo non gestito a Taglie e Colori"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDqta Is Nothing Then frmDqta.Dispose()
      frmDqta = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Dim bNewTmp As Boolean = False
    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleGsor.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135727040000, "Impossibile stampare un nuovo ordine non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleGsor.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If Not Salva(1, False) Then Return
      End If

      Stampa(1)

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

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
      If tlbSalva.Enabled = False And oCleGsor.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135739052000, "Impossibile stampare un nuovo ordine non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleGsor.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If Not Salva(0, False) Then Return
      End If

      Stampa(0)

      If CheckRiapriDocSuSalva() = False Then
        RipristinaInterfaccia()
        For Each lForm As Integer In oMenu.lReportsForm
          SetForegroundWindow(CType(lForm, IntPtr))
        Next
      End If

      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Dim bNewTmp As Boolean = False

    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleGsor.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135749972000, "Impossibile stampare un nuovo ordine non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleGsor.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If Not Salva(-1, False) Then Return
      End If

      '--------------------------------
      'chiamo la stampa su word passandogli la query
      strQueryWord = oCleGsor.GetQueryStampaWord()
      If strQueryWord = "" Then Return

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORGSOR"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = cbTipoDoc.SelectedValue
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar1 = False

      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

      '-------------------------------
      'aggiorno il flag di 'Stampato' su testord
      If oPar.bPar5 Then
        oCleGsor.AggiornaFlagStampato()
      End If

      '--------------------------------
      'Crea/sovrascrive l'eventuale record di tipork = 'F' in ALLOLE
      'con il percorso del file documento appena creato
      'già fatto in BN__STWO
      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

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
    Dim bNewTmp As Boolean = False

    '------------------------------
    'PER UN ESEMPIO COMPLETO VEDI BNORCONF: questo programma può stampare solo con un PDF per file, non un pdf per tutti i documenti
    '------------------------------

    Try
      '--------------------------------
      'se non posso salvare non posso stampare i nuovi documenti
      If tlbSalva.Enabled = False And oCleGsor.bNew Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222116250000, "Impossibile stampare un nuovo ordine non possedendo i privilegi di salvataggio."))
        Return
      End If

      bNewTmp = oCleGsor.bNew

      '--------------------------------
      'salva il documento
      If tlbSalva.Enabled = True Then
        If Not Salva(-1, False) Then Return
      End If

      '--------------------------------
      'chiamo la stampa su PDF passandogli le query
      strQueryGetDocMultipli = oCleGsor.GetQueryStampaPdf
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

      'parte aggiuntiva diPeSetSelectionFormula (i marcatori verranno sostituiti con i campi di 'strQueryGetGroupBy'
      strQueryCrw32FileMultipli = "{MOVORD.codditt} = '" & DittaCorrente & "'" & _
                                 " AND {MOVORD.mo_stasino} <> 'N'" & _
                                 " AND {TESTORD.td_magaz2} <> {KEYORD.ko_magaz}" & _
                                 " AND {MOVORD.mo_anno} = |anno|" & _
                                 " AND {MOVORD.mo_numord} = |numero|" & _
                                 " AND {MOVORD.mo_serie} = |serie|" & _
                                 " AND {MOVORD.mo_tipork} = |tipork|"
      strQueryCrw32FileUnico = strQueryCrw32FileMultipli

      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BSORGSOR"
      oPar.strPar2 = strQueryCrw32FileMultipli
      oPar.strPar3 = strQueryCrw32FileUnico
      oPar.strPar4 = strQueryGetDocMultipli
      oPar.strPar5 = strQueryGetDocUnico
      oPar.strParam = "Stampa ordini"
      oPar.ctlPar1 = Me
      oPar.ctlPar2 = dttFormule
      oPar.bPar5 = False    'se al ritorno da BN__STWO = true vuol dire che il documento è stato stampato
      oPar.bPar4 = False    'al ritorno se true devo eseguire anche la stampa su carta

      oMenu.RunChild("NTSInformatica", "FRMPDGENP", "", DittaCorrente, "", "BNPDGENP", oPar, "", True, True)

      '-------------------------------
      'aggiorno il flag di 'Stampato' su testord
      If oPar.bPar5 Then
        oCleGsor.AggiornaFlagStampato()
      End If

      '-------------------------------
      'da bnpdgenp è stato scelto di stampare anche su carta
      If oPar.bPar4 Then
        Stampa(1)
      End If

      If CheckRiapriDocSuSalva() = False Then RipristinaInterfaccia()

      If bNewTmp And tlbSalvanuovo.Checked Then tlbNuovo_ItemClick(tlbNuovo, Nothing)

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
      oPar.strPar2 = NTSCStr(dsGsor.Tables("TESTA").Rows(0)!et_tipork)
      oPar.strPar3 = NTSCStr(dsGsor.Tables("TESTA").Rows(0)!et_serie)
      oPar.dPar1 = NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_anno)
      oPar.dPar2 = NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_numdoc)

      oMenu.RunChild("NTSInformatica", "FRMEMCMAI", oApp.Tr(Me, 129048283640958247, "Console e-mail"), DittaCorrente, "", "BNEMCMAI", oPar)
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

#End Region

#Region "Voci Toolbar STRUMENTI"
  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      LeggiDatiDitta()
      'poi eventuali blocci sulla toolbar nuovo/apri per tipo documento
      cbTipoDoc_SelectedIndexChanged(cbTipoDoc, Nothing)
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
      oPar.strNomProg = "BSORGSOR"

      oMenu.RunChild("NTSInformatica", "FRMMGTERM", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRicaricaGreg_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicaricaGreg.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor

      If oCleGsor.LeggePevePeac Then
        oCleGsor.LeggiRegistro()
        oCleGsor.strLastTiporkRegistroDoc = "."
        If tsGsor.Visible Then
          oCleGsor.LeggiRegistroDoc(cbTipoDoc.SelectedValue)
        End If
      End If
      oApp.MsgBoxInfo(oApp.Tr(Me, 128521248721816000, "Configurazioni delle Personalizzazioni vendite\acquisti e Registro di Business ricaricate"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbPersVend_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPersVend.ItemClick
    Try
      oMenu.RunChild("BSVEPEVE", "CLSVEPEVE", oApp.Tr(Me, 128154847057714954, "Personalizzazione Vendite"), DittaCorrente, "", "", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbPersAcq_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPersAcq.ItemClick
    Try
      oMenu.RunChild("BSMGPEAC", "CLSMGPEAC", oApp.Tr(Me, 128521251321846000, "Personalizzazione Acquisti"), DittaCorrente, "", "", Nothing, "", True, True)

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
      If tsGsor.Visible = False Then Return

      'prima di riordinare le righe devono essere salvate
      Me.ValidaLastControl()
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

      'se le righe sono filtrate devo rimuovere il filtro
      grvRighe.DisableColumnSortFilter()

      'non posso applicare la sort: devo clonare il datatable, svuotarlo e riempirlo nuovamente
      If tlbOrdinaRigheRiga.Down Then strSort = "ec_riga"
      If tlbOrdinaRigheCodiceArticolo.Down Then strSort = "ec_codart"
      If tlbOrdinaRigheDescrizione.Down Then strSort = "ec_descr"
      If tlbOrdinaRigheCommessa.Down Then strSort = "ec_commeca"

      dttTmp = dsGsor.Tables("CORPO").Copy
      dsGsor.Tables("CORPO").Clear()
      For Each dtrT As DataRow In dttTmp.Select("", strSort)
        dsGsor.Tables("CORPO").ImportRow(dtrT)
      Next
      dsGsor.Tables("CORPO").AcceptChanges()

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
    oCleGsor.nOrdrig = 0
    tlbOrdinaRigheCodiceArticolo.Down = False
    tlbOrdinaRigheCommessa.Down = False
    tlbOrdinaRigheDescrizione.Down = False
  End Sub
  Public Overridable Sub tlbOrdinaRigheCodiceArticolo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheCodiceArticolo.ItemClick
    oCleGsor.nOrdrig = 1
    tlbOrdinaRigheRiga.Down = False
    tlbOrdinaRigheCommessa.Down = False
    tlbOrdinaRigheDescrizione.Down = False
  End Sub
  Public Overridable Sub tlbOrdinaRigheDescrizione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheDescrizione.ItemClick
    oCleGsor.nOrdrig = 2
    tlbOrdinaRigheCodiceArticolo.Down = False
    tlbOrdinaRigheCommessa.Down = False
    tlbOrdinaRigheRiga.Down = False
  End Sub
  Public Overridable Sub tlbOrdinaRigheCommessa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaRigheCommessa.ItemClick
    oCleGsor.nOrdrig = 3
    tlbOrdinaRigheCodiceArticolo.Down = False
    tlbOrdinaRigheRiga.Down = False
    tlbOrdinaRigheDescrizione.Down = False
  End Sub
#End Region

#Region "Voci Toolbar TESTA"

  Public Overridable Sub tlbNuovoOrdineDaPrec_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovoOrdineDaPrec.ItemClick
    Dim lNewProgr As Integer = 0
    Dim nNewAnno As Integer = 0
    Dim strNewSerie As String = ""
    Dim strNewTipork As String = ""
    Dim strSoloSerie As String = ""
    Dim dttTmp As New DataTable
    Dim frmOpen As FRMORSEOR = Nothing
    Try
      If oCleGsor.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128602555183593750, "Il comando può essere utilizzato solo su documenti già salvati"))
        Return
      End If

      If oCleGsor.bHasChangesET Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128602622670156250, "Il documento ha subito delle modifiche dopo la sua apertura. Duplicazione non possibile"))
        Return
      End If

      'Esce se è in corso una modifica di un record
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      Me.Cursor = Cursors.Default

      '-----------------------------
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "

      '-----------------------------
      'visualizzo la form
      frmOpen = CType(NTSNewFormModal("FRMORSEOR"), FRMORSEOR)
      frmOpen.Init(oMenu, Nothing, DittaCorrente, Nothing)
      AddHandler oCleGsor.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity
      frmOpen.oCleGsor = oCleGsor
      frmOpen.Text = oApp.Tr(Me, 128776135797240000, "Estremi nuovo documento")
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
      frmOpen.edAnno.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "Recent", ".", "LastDocNewAnno", frmOpen.edAnno.Text, " ", frmOpen.edAnno.Text)
      'frmOpen.edSerie.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BNORGSOR", "Recent", ".", "LastDocNewSerie", frmOpen.edSerie.Text, " ", frmOpen.edSerie.Text).PadLeft(1)

      frmOpen.ShowDialog()
      RemoveHandler oCleGsor.RemoteEvent, AddressOf frmOpen.GestisciEventiEntity

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
         lNewProgr = NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc) And _
         strNewSerie = NTSCStr(oCleGsor.dttET.Rows(0)!et_serie) And _
         nNewAnno = NTSCInt(oCleGsor.dttET.Rows(0)!et_anno) Then Return

      Me.Cursor = Cursors.WaitCursor

      'inoltre se cambio tipork devo ricaricare anche la griglia ...
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString <> strNewTipork Then
        GctlTipoDoc = strNewTipork
        GctlSetRoules()
        GestisciGrigliaTCO()
      End If
      NTSGetDataAutocompletamentoTipork = cbTipoDoc.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

      If oCleGsor.DuplicaDoc(strNewTipork, nNewAnno, strNewSerie, lNewProgr, NTSCInt(frmOpen.edConto.Text), NTSCInt(frmOpen.edTipoBf.Text)) Then
        cbTipoDoc.SelectedValue = strNewTipork
        edAnnoDoc.Text = nNewAnno.ToString
        edSerieDoc.Text = strNewSerie
        edNumDoc.Text = lNewProgr.ToString
        If cbTipoDoc.SelectedValue.ToString = "Q" Then
          If ckEt_flevas.Checked = False Then
            GctlSetVisEnab(ckEt_flevas, False)
          Else
            ckEt_flevas.Enabled = False
          End If
        Else
          ckEt_flevas.Enabled = False
        End If
        grvRighe.NTSMoveFirstRowColunn()
        tsGsor.SelectedTabPageIndex = 0
        GctlSetVisEnab(tlbSalva, False)
        oCleGsor.bDocNonModificabile = False

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
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then
        Return
      End If
      oCleGsor.CalcolaTotali()
      'struttura per passare i parametri a BNCGDCST e ricevere gli stessi compilati con i valori calcolati in BECGDCST
      oMenu.EscompFromDate(DittaCorrente, NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc), nEscomp, Nothing)
      With oDcst
        .lConto = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto)
        .nEscomp = nEscomp
        .strData = NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc).ToShortDateString
        .bControllaFido = True
        .bControllaInsoluti = True
        .bStatPagamenti = True
        .dttMotrans = oCleGsor.dttEC
        .dImpOdierno = NTSCDec(edEt_totdoc.Text)
        .bOpendoc = Not oCleGsor.bNew
        .strTipork = oCleGsor.dttET.Rows(0)!et_tipork.ToString
        .nAnno = NTSCInt(oCleGsor.dttET.Rows(0)!et_anno)
        .strSerie = oCleGsor.dttET.Rows(0)!et_serie.ToString
        .lNumdoc = NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc)
        .bBlocca = False
        .bVisForm = True
        .bVismess = False
      End With

      oPar.ctlPar1 = oDcst
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNORGSOR"

      oMenu.RunChild("NTSInformatica", "FRMCGDCST", "", DittaCorrente, "", "BNCGDCST", oPar, "", True, True)

      '-----------------------------------------
      'prova per inserire 400 record
      'Dim dtr As DataRow = Nothing
      'Dim nStart As Integer = dsGsor.Tables("CORPO").Rows.Count
      'CLN__STD.CopiaDatarow(dsGsor, "MOVORD", 0, dtr)
      'For i As Integer = 0 To 400
      '  dsGsor.Tables("CORPO").ImportRow(dtr)
      '  dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)!ec_riga = nStart + i
      'Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbModifNumOrd_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbModifNumOrd.ItemClick
    Dim lNewProgr As Integer = 0
    Dim nNewAnno As Integer = 0
    Dim strNewSerie As String = ""
    Dim strSoloSerie As String = ""
    Dim dttTmp As New DataTable
    Dim frmOpen As FRMORSEOR = Nothing
    Try
      If oCleGsor.bNew = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128534121291752000, "Numero ordine modificabile solo sui nuovi documenti"))
        Return
      End If

      'Esce se è in corso una modifica di un record
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      Me.Cursor = Cursors.Default

      '-----------------------------
      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipoDoc.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "

      '-----------------------------
      'visualizzo la form
      frmOpen = CType(NTSNewFormModal("FRMORSEOR"), FRMORSEOR)
      frmOpen.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmOpen.oCleGsor = oCleGsor
      frmOpen.Text = oApp.Tr(Me, 129048478871450673, "Cambia numero documento")
      frmOpen.MinimumSize = New Size(0, 0)
      frmOpen.Height = frmOpen.edConto.Top + frmOpen.edConto.Height
      frmOpen.MinimumSize = frmOpen.Size
      dttTmp = cbTipoDoc.DataSource.Clone
      For Each dtrT As DataRow In CType(cbTipoDoc.DataSource, DataTable).Rows
        dttTmp.ImportRow(dtrT)
      Next
      frmOpen.cbTipo.DataSource = dttTmp
      frmOpen.cbTipo.ValueMember = cbTipoDoc.ValueMember
      frmOpen.cbTipo.DisplayMember = cbTipoDoc.DisplayMember
      frmOpen.cbTipo.SelectedValue = cbTipoDoc.SelectedValue
      frmOpen.cbTipo.Enabled = False            'non posso cambiare il tipo documento
      frmOpen.edAnno.Text = edAnnoDoc.Text
      If strSoloSerie <> "?" Then
        frmOpen.edSerie.Text = strSoloSerie
        frmOpen.edSerie.Enabled = False
      Else
        frmOpen.edSerie.Text = edSerieDoc.Text
      End If
      frmOpen.edConto.Enabled = False
      frmOpen.edTipoBf.Enabled = False
      frmOpen.edConto.Visible = False
      frmOpen.edTipoBf.Visible = False
      frmOpen.ShowDialog()

      'ho selezionato annulla
      If frmOpen.bOk = False Then Return

      nNewAnno = NTSCInt(frmOpen.edAnno.Text)
      strNewSerie = frmOpen.edSerie.Text.ToUpper
      lNewProgr = NTSCInt(frmOpen.edNumero.Text)

      If nNewAnno = 0 Then Return
      If strNewSerie = "" Then Return
      If lNewProgr = 0 Then Return

      If lNewProgr = 0 And strNewSerie = "" And nNewAnno = 0 Then Return
      If lNewProgr = NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc) And _
         strNewSerie = NTSCStr(oCleGsor.dttET.Rows(0)!et_serie) And _
         nNewAnno = NTSCInt(oCleGsor.dttET.Rows(0)!et_anno) Then Return

      'cambio il num doc su tutte le tabella
      If Not oCleGsor.CambiaNumdoc(dsGsor, lNewProgr, strNewSerie, nNewAnno, True) Then Return
      edAnnoDoc.Text = nNewAnno.ToString
      edSerieDoc.Text = strNewSerie
      edNumDoc.Text = lNewProgr.ToString

      'se ho forzato il num doc non devo salvarlo con aggnuma
      If lNewProgr <> oCleGsor.LegNuma(IIf(cbTipoDoc.SelectedValue.ToString() = "V", "VV", cbTipoDoc.SelectedValue.ToString()).ToString, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text)) Then
        oCleGsor.bProgrCambiato = True
      Else
        oCleGsor.bProgrCambiato = False
      End If

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

  Public Overridable Sub tlbRicalcPrzScnPro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcPrzScnPro.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.strNomProg = "BSORGSOR"
      oMenu.RunChild("NTSInformatica", "FRMMGRICA", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.bPar1 Then
        Cursor.Current = Cursors.WaitCursor
        oCleGsor.RicalcolaCondizioniDocumento(grvRighe.NTSGetCurrentDataRow)
        Cursor.Current = Cursors.Default
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbVisDettTCO_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisDettTCO.ItemClick
    Try
      edEt_conto.Focus()
      If oCleGsor.dttEC.Select("xxo_codtagl <> 0").Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128619912560937500, "Nella griglia non sono presenti articoli di tipo 'Taglia e colore'"))
        Return
      End If

      Dim frmDqta As FRMORDQTA = Nothing
      frmDqta = CType(NTSNewFormModal("FRMORDQTA"), FRMORDQTA)
      frmDqta.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmDqta.InitEntity(oCleGsor, Nothing)
      frmDqta.dsGsor = dsGsor
      frmDqta.bVisDoc = True
      frmDqta.ShowDialog()
      frmDqta.Dispose()
      frmDqta = Nothing

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbApriChiamata_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriChiamata.ItemClick
    Dim lLead As Integer = 0
    Dim strParam As String = ""
    Dim lCodChia As Integer = 0
    Try
      '------------------------------
      'Se non è attivo il modulo Customer Service, esce senza avvisare
      '(non dovrebbe succedere perché la voce di menu è NON visibile
      If oCleGsor.bModuloAS = False Then Return

      '------------------------------
      'Se non si tratta di 'Impegno cliente', avvisa ed esce
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString <> "R" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569578649687500, "Attenzione!" & vbCrLf & _
                        "La creazione di una Chiamata può essere fatta solo sugli 'Impegni clienti'." & vbCrLf & _
                        "Creazione nuova Chiamata non possibile."))
        Return
      End If
      If NTSCInt(edEt_conto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569579047500000, "Indicare un conto valido prima di passare alla creazione di una nuova Chiamata."))
        Return
      End If

      '------------------------------
      'Se esiste già una Chiamata agganciata, avvisa ed esce
      If NTSCInt(lbEt_codchia.Text) <> 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569579622187500, "Attenzione!" & vbCrLf & _
                        "Esiste già una Chiamata (|" & lbEt_codchia.Text & "|) collegata all'ordine/impegno corrente." & vbCrLf & _
                        "Creazione nuova Chiamata non possibile."))
        Return
      End If
      '------------------------------
      'Ricava il codice Lead da conto/destinazione di testata
      lLead = oCleGsor.CercaLeadDaConto(NTSCInt(oCleGsor.dttET.Rows(0)!et_conto), NTSCInt(edEt_coddest.Text))

      '------------------------------
      'Se non esiste un Lead associato al conto, avvisa ed esce
      If lLead = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569584327343750, "Attenzione!" & vbCrLf & _
                        "Non esiste un Lead valido associato a conto/destinazione di testata." & vbCrLf & _
                        "Creazione nuova Chiamata non possibile."))
        Return
      End If
      '------------------------------
      strParam = "NUOV;" & lLead.ToString.PadLeft(9, "0"c) & ";" & "000000000" & ";" & "000000000" & ";" & "000000000" & ";" & lCodChia

      oMenu.RunChild("BSCSGCHI", "CLSCSGCHI", "Gestione Chiamate", DittaCorrente, "", "", Nothing, strParam, True, True)

      lCodChia = NTSCInt(strParam)    'valore di ritorno da BSCGGCHI
      '-------------------------------
      oCleGsor.dttET.Rows(0)!et_codchia = lCodChia.ToString


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbOggettiAssociati_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOggettiAssociati.ItemClick
    Dim strParam As String = ""
    Try
      strParam = "APRI§O§0§" & NTSCStr(oCleGsor.dttET.Rows(0)!et_tipork) & "§" & _
                    edAnnoDoc.Text & "§" & _
                    edSerieDoc.Text & "§" & _
                    edNumDoc.Text & "§" & _
                    NTSCStr(oCleGsor.dttET.Rows(0)!et_conto)

      oMenu.RunChild("BS__AOLE", "CLS__AOLE", "", DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbZoomBanche_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoomBanche.ItemClick
    Dim oParam As New CLE__PATB

    Try
      If edEt_conto.Focused Or edEt_abi.Focused Or edEt_cab.Focused Or edEt_banc1.Focused Or edEt_banc2.Focused Then tsGsor.Focus()

      '--------------------------------------------
      'zoom specifico per le banche del cliente
      NTSZOOM.strIn = NTSCInt(edEt_conto.Text).ToString
      NTSZOOM.ZoomStrIn("ZOOMCLIBANC", DittaCorrente, oParam)
      If oParam.nAbi <> 0 Then
        oCleGsor.dttET.Rows(0)!et_abi = NTSCInt(oParam.nAbi).ToString
        oCleGsor.dttET.Rows(0)!et_cab = NTSCInt(oParam.nCab).ToString
        oCleGsor.dttET.Rows(0)!et_banc1 = oParam.strBanc1
        oCleGsor.dttET.Rows(0)!et_banc2 = oParam.strBanc2
        'oParam.strRifriba
        'oParam.strCin
        'oParam.strPrefIBAN
        'oParam.strIBAN
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGenNumComm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGenNumComm.ItemClick
    Dim strRetSubCommeca As String = " "
    Dim strDescomm As String = ""
    Dim strError As String = ""
    Try

      If grRighe.Focused And Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        Dim dtrEC As DataRow = grvRighe.NTSGetCurrentDataRow
        grvRighe.NTSGetCurrentDataRow!ec_commeca = CType(oCleGsor.oCleComm, CLELBMENU).GeneraNumCommeca(DittaCorrente, _
                              oCleGsor.dttET.Rows(0)!et_tipork.ToString, NTSCInt(oCleGsor.dttET.Rows(0)!et_anno), _
                              oCleGsor.dttET.Rows(0)!et_serie.ToString, NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc), NTSCInt(dtrEC!ec_riga), _
                              NTSCInt(oCleGsor.dttET.Rows(0)!et_tipobf), NTSCInt(oCleGsor.dttET.Rows(0)!et_conto), NTSCStr(dtrEC!ec_codart), _
                              NTSCStr(dtrEC!ec_descr), NTSCStr(dtrEC!ec_desint), NTSCStr(dtrEC!ec_note), _
                              NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc).ToShortDateString, strRetSubCommeca, "O", "", strError)
        grvRighe.NTSGetCurrentDataRow!ec_subcommeca = strRetSubCommeca
        If strError <> "" Then oApp.MsgBoxErr(strError)

      ElseIf edEt_commeca.Focused Then
        edEt_commeca.Text = CType(oCleGsor.oCleComm, CLELBMENU).GeneraNumCommeca(DittaCorrente, _
                              oCleGsor.dttET.Rows(0)!et_tipork.ToString, NTSCInt(oCleGsor.dttET.Rows(0)!et_anno), _
                              oCleGsor.dttET.Rows(0)!et_serie.ToString, NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc), 0, _
                              NTSCInt(oCleGsor.dttET.Rows(0)!et_tipobf), NTSCInt(oCleGsor.dttET.Rows(0)!et_conto), " ", _
                              " ", " ", " ", _
                              NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc).ToShortDateString, strRetSubCommeca, "O", strDescomm, strError).ToString
        lbXx_commeca.Text = strDescomm
        edEt_subcommeca.Text = strRetSubCommeca
        If strError <> "" Then oApp.MsgBoxErr(strError)

      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128552466709324500, "Posizionarsi su una riga di griglia con articolo impostato o nel campo 'Commessa' della testata del documento"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbNavigazDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazDoc.ItemClick
    Dim strParam As String = ""
    Try
      If oCleGsor.bNew Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128554237315694937, "Funzionalità non abilitata sui nuovi documenti"))
        Return
      End If
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "$" Or oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Or _
         oCleGsor.dttET.Rows(0)!et_tipork.ToString = "Q" Or oCleGsor.dttET.Rows(0)!et_tipork.ToString = "V" Or _
         oCleGsor.dttET.Rows(0)!et_tipork.ToString = "Y" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776135843572000, "Funzione non disponibile per questo tipo di documento"))
        Return
      End If

      strParam = "APRI;" & oCleGsor.dttET.Rows(0)!et_tipork.ToString & ";" & _
                edAnnoDoc.Text & ";" & _
                edSerieDoc.Text & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(edNumDoc.Text).ToString.PadLeft(9, "0"c), 9) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(oCleGsor.dttET.Rows(0)!et_conto).ToString.PadLeft(9, "0"c), 9) & ";" & _
                Microsoft.VisualBasic.Right("          " & NTSCDate(edet_datdoc.Text).ToString("dd/MM/yyyy"), 10) & _
                ";000000000;0000;0000; ;000000000;0000;1"
      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 128154847220982619, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSchedatrasporto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSchedatrasporto.ItemClick
    Try
      If Not oCleGsor.CheckAperturaSchetrasp(False, False) Then Return
      ApriSchedaTrasp(False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSkype_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSkype.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If NTSCInt(edEt_conto.Text) = 0 Then Return

      oPar.strPar1 = "C"
      oPar.dPar1 = NTSCInt(edEt_conto.Text)

      oMenu.RunChild("NTSInformatica", "FRM__SKYP", "", DittaCorrente, "", "BN__SKYP", oPar, "", False, True)
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
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135891464000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If

      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And (Not oCleGsor.bTerzista) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569717214375000, "Funzione abilitata solo su Ordini di produzione a terzisti e non interni."))
        Return
      End If

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNORGSOR"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar2 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz)
      oPar.dPar3 = NTSCInt(edEt_conto.Text)
      oPar.strPar2 = ""                   'VALORI DI RITORNO

      oMenu.RunChild("NTSInformatica", "FRMMGGRSC", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.strPar2 = "" Then Return

      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569727909531250, "Non è possibile riportare le precedenti condizioni in 'Ordini di produzione'."))
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
              'oCleGsor.Scorporo(NTSCDec(strT(3)), NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codiva), dTmp1, dTmp2, oApp.NDecImporti)
              grvRighe.NTSGetCurrentDataRow!ec_prezzo = NTSCDec(strT(4))
            End If
          Else
            'Il documento selezionato è normale, quello corrente è di scorporo
            oApp.MsgBoxErr(oApp.Tr(Me, 128776136050584000, "Non è possibile riportare le precedenti condizioni commerciali (prezzi/sconti) in quanto il documento selezionato è di tipo IVA compresa (scorporo) al contrario di quello corrente."))
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

  Public Overridable Sub tlbPrecOrd_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecOrd.ItemClick
    'Dim oPar As CLE__CLDP = Nothing
    Dim oPar As New CLE__PATB
    Try
      '----------------------
      'oPar = New CLE__CLDP
      'oPar.Ditta = DittaCorrente
      'oPar.strNomProg = "BNORGSOR"
      'If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
      '  oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      '  oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      'End If
      'oPar.dPar3 = NTSCInt(edEt_conto.Text)

      'oMenu.RunChild("NTSInformatica", "FRMMGORSC", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '------------------------
      'Chiama lo zoom ordini
      oPar.strDescr = oCleGsor.GetWhereHlmoOfa(True)
      oPar.strTipo = oCleGsor.dttET.Rows(0)!et_tipork.ToString
      oPar.lContoCF = NTSCInt(oCleGsor.dttET.Rows(0)!et_conto)
      oPar.strCodart = ""
      oPar.nFase = 0
      If grvRighe.Focused Then
        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          oPar.strCodart = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
          oPar.nFase = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
        End If
      End If
      oPar.dImporto = 0
      'oPar.lCommessa = NTSCInt(IIf(oCleBoll.bPassaCommessaTestataHLMO, edEt_commeca.Text, 0))
      oPar.bFlag1 = False 'oCleBoll.bNew
      'oPar.nAnno = NTSCInt(edAnnoDoc.Text)
      'oPar.strAlfpar = edSerieDoc.ToString
      'oPar.lNumreg = NTSCInt(edNumDoc.Text)
      oPar.nTipologia = 0                     '0 solo visualizzaz, 2 = possibilità di selez le righe
      oPar.oParam = Nothing                   'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      oPar.nMastro = 1                        'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
      NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbImpCollegati_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpCollegati.ItemClick
    Dim frmImpe As FRMORIMPE = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim strColSort As String = "ec_riga"

    Try
      frmImpe = CType(NTSNewFormModal("FRMORIMPE"), FRMORIMPE)
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776136099724000, "Funzione disponibile solo per 'Ordini di produzione'"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134505556000, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134599226000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134611862000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleGsor.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere gli altri impegni collegati
      ds.Tables.Add(dsGsor.Tables("CORPOIMP").Clone())
      Select Case oCleGsor.nOrdrig
        Case 0 : strColSort = "ec_riga"
        Case 1 : strColSort = "ec_codart"
        Case 2 : strColSort = "ec_descr"
        Case 3 : strColSort = "ec_commeca"
      End Select
      For Each dtrT As DataRow In dsGsor.Tables("CORPOIMP").Select("ec_rigaor = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, strColSort & " ASC, ec_riga ASC")
        ds.Tables("CORPOIMP").ImportRow(dtrT)
        dtrT.Delete()
      Next
      dsGsor.Tables("CORPOIMP").AcceptChanges()

      '--------------
      frmImpe.oCleGsor = oCleGsor
      frmImpe.dttUm = dttUm
      frmImpe.bEt_Scorpo = ckEt_scorpo.Checked
      frmImpe.nEt_valuta = NTSCInt(edEt_valuta.Text)
      frmImpe.nEt_listino = NTSCInt(edEt_listino.Text)
      frmImpe.dtEt_datdoc = NTSCDate(edet_datdoc.Text)
      frmImpe.nAnnoDoc = NTSCInt(edAnnoDoc.Text)
      frmImpe.strSerieDoc = edSerieDoc.Text
      frmImpe.lNumdoc = NTSCInt(edNumDoc.Text)
      frmImpe.nEt_magaz = NTSCInt(edEt_magaz.Text)
      frmImpe.lEt_conto = NTSCInt(edEt_conto.Text)
      frmImpe.InitEntity(oMenu, Nothing, ds)
      frmImpe.ShowDialog()
      grvRighe.Focus()

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        If ds.Tables("CORPOIMP").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("CORPOIMP").Rows(i)!ec_riga) > 0 Then
            dsGsor.Tables("CORPOIMP").ImportRow(ds.Tables("CORPOIMP").Rows(i))
          Else
            ds.Tables("CORPOIMP").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsGsor.Tables("CORPOIMP").AcceptChanges()
      oCleGsor.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleGsor.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleGsor.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        If oCleGsor.bModPM And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_pmtaskid) > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128776134707248000, "ATTENZIONE:" & vbCrLf & _
                 "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                 "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management."))
        End If
        oCleGsor.SistemaPMSalconDaQta(grvRighe.NTSGetCurrentDataRow)
        If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      End If
      AbilitaDisabilitaRigaNum()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmImpe Is Nothing Then frmImpe.Dispose()
      frmImpe = Nothing
      Me.Cursor = Cursors.Default
      oCleGsor.dtrHT = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbLavCollegate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLavCollegate.ItemClick
    Dim frmTtat As FRMORTTAT = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dtrAttivit() As DataRow = Nothing

    Try
      frmTtat = CType(NTSNewFormModal("FRMORTTAT"), FRMORTTAT)
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586969528593750, "Funzione disponibile solo per 'Ordini di produzione'"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134518192000, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134624498000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134636822000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleGsor.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere le altre lavorazioni collegate
      'aggiungo alla tabella anche le colonne di assris, per fare in modo che le due tabelle vengano gestite come se fossero una
      dsGsor.Tables("ATTIVIT").AcceptChanges()
      dsGsor.Tables("ASSRIS").AcceptChanges()
      ds.Tables.Add(dsGsor.Tables("ATTIVIT").Clone())
      For i = 0 To dsGsor.Tables("ASSRIS").Columns.Count - 1
        If dsGsor.Tables("ASSRIS").Columns(i).ColumnName.ToLower <> "codditt" And dsGsor.Tables("ASSRIS").Columns(i).ColumnName.ToLower <> "ts" Then
          ds.Tables("ATTIVIT").Columns.Add(dsGsor.Tables("ASSRIS").Columns(i).ColumnName, dsGsor.Tables("ASSRIS").Columns(i).DataType)
          ds.Tables("ATTIVIT").Columns(dsGsor.Tables("ASSRIS").Columns(i).ColumnName).DefaultValue = dsGsor.Tables("ASSRIS").Columns(i).DefaultValue
        End If
      Next
      'importo i dati dalle due tabelle
      dtrAttivit = dsGsor.Tables("ATTIVIT").Select("at_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "at_fase")
      For i = 0 To dtrAttivit.Length - 1
        'carico attivit
        ds.Tables("ATTIVIT").ImportRow(dtrAttivit(i))
        dtrT1 = ds.Tables("ATTIVIT").Select("at_riga = " & dtrAttivit(i)!at_riga.ToString & _
                                            " AND at_fase = " & dtrAttivit(i)!at_fase.ToString)
        'devo aggiungere i valori di assris
        dtrT = dsGsor.Tables("ASSRIS").Select("as_riga = " & dtrT1(0)!at_riga.ToString & _
                                              " AND as_fase = " & dtrT1(0)!at_fase.ToString)
        If dtrT.Length > 0 Then
          For l = 0 To dsGsor.Tables("ASSRIS").Columns.Count - 1
            If dsGsor.Tables("ASSRIS").Columns(l).ColumnName.ToLower <> "codditt" And dsGsor.Tables("ASSRIS").Columns(l).ColumnName.ToLower <> "ts" Then
              dtrT1(0)(dsGsor.Tables("ASSRIS").Columns(l).ColumnName) = dtrT(0)(dsGsor.Tables("ASSRIS").Columns(l).ColumnName)
            End If
          Next
          dtrT(0).Delete()
        End If
        dtrAttivit(i).Delete()
      Next
      dsGsor.Tables("ATTIVIT").AcceptChanges()
      dsGsor.Tables("ASSRIS").AcceptChanges()
      ds.Tables("ATTIVIT").AcceptChanges()

      frmTtat.oCleGsor = oCleGsor
      frmTtat.InitEntity(oMenu, Nothing, ds)
      frmTtat.ShowDialog()
      grvRighe.Focus()

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("ATTIVIT").Rows.Count - 1
        If ds.Tables("ATTIVIT").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("ATTIVIT").Rows(i)!at_riga) > 0 Then
            dsGsor.Tables("ATTIVIT").ImportRow(ds.Tables("ATTIVIT").Rows(i))
            'devo importare anche assris, se settata
            If NTSCInt(ds.Tables("ATTIVIT").Rows(i)!as_codcent) <> 0 Then
              dsGsor.Tables("ASSRIS").ImportRow(ds.Tables("ATTIVIT").Rows(i))
            End If
          Else
            ds.Tables("ATTIVIT").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsGsor.Tables("ATTIVIT").AcceptChanges()
      dsGsor.Tables("ASSRIS").AcceptChanges()
      oCleGsor.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleGsor.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleGsor.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        If oCleGsor.bModPM And NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_pmtaskid) > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128776134690712000, "ATTENZIONE:" & vbCrLf & _
                 "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                 "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management."))
        End If
        oCleGsor.SistemaPMSalconDaQta(grvRighe.NTSGetCurrentDataRow)
        If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      End If
      AbilitaDisabilitaRigaNum()

    Catch ex As Exception
      dsGsor.Tables("ATTIVIT").RejectChanges()
      dsGsor.Tables("ASSRIS").RejectChanges()
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmTtat Is Nothing Then frmTtat.Dispose()
      frmTtat = Nothing
      Me.Cursor = Cursors.Default
      oCleGsor.dtrHT = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApriDB_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriDB.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return

      If oMenu.ValCodiceDb(grvRighe.NTSGetCurrentDataRow!ec_codart.ToString, DittaCorrente, "DISTBAS", "S") Then
        'La distinta base esistente, accedo in apertura
        strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & "," & _
                          NTSCDate(oCleGsor.dttET.Rows(0)!et_datdoc).ToString("dd/MM/yyyy") & "," & _
                          Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_commeca).ToString.PadLeft(9, "0"c), 9) & "," & _
                          "000000000"
      Else
        'La distinta base non esisto. chiedo all'utente cosa vuole fare.
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130954191811948062, "Non esiste la distinta base per l'articolo selezionato." & vbCrLf & _
                                                                      "Creare una nuova distinta?")) = DialogResult.Yes Then
          strParam = "NUOV;" & grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
        Else
          Return
        End If
      End If

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
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128571282388281250, "Funzionalità non disponibile sugli 'Ordini di produzione'."))
        Return
      End If

      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.TerminaleLeggiCheck("BSORGSOR") Then Return

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleGsor.SeparatePathAndFileName(oCleGsor.strTermFileName, strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        'strFile = oApp.InputBoxNew(oApp.Tr(Me, 128571298019531250, "Inserire il nome del file da acquisire (che deve risiedere nella cartella '|" & strPath & "|'):"), Mid(strFile, 2))
        'oCleGsor.strTermFileName = (strPath & "\").Replace("\\", "\") & strFile
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
          oCleGsor.strTermFileName = oDlg.FileName
        End If
        oDlg.Dispose()
        oDlg = Nothing
      End If

      'Lancia la shell
      If oCleGsor.bTermExecute Then
        'Prima di tutto cancella il file se esiste
        Try
          System.IO.File.Delete(oCleGsor.strTermFileName)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 128571303239218750, "Impossibile cancellare il file '|" & oCleGsor.strTermFileName & "|' prima della acquisizione. Errore: ") & vbCrLf & ex.Message)
          Return
        End Try

        If oCleGsor.strTermCommand.Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If oCleGsor.bTermSilent Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = oCleGsor.strTermCommand
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            oApp.MsgBoxErr(oApp.Tr(Me, 128571304353281250, "Errore durante l'esecuzione della Shell '|" & oCleGsor.strTermCommand & "|'. Errore: ") & vbCrLf & ex.Message)
            Return
          End Try
        End If
      End If    'If oCleGsor.bTermExecute Then

      '---------------------------
      'inizio l'import delle righe del file
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.TerminaleLeggiImportaFile() Then Return

      '---------------------------
      'mi sposto sull'ultima riga 
      grvRighe.NTSMoveLastRow()

      '---------------------------
      'Visualizza il log
      If oCleGsor.bTermShowLogErr Or oCleGsor.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleGsor.LogFileName)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbSelezArtAcc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezArtAcc.ItemClick
    Dim oParam As New CLE__PATB
    Dim dtrT() As DataRow = Nothing
    Try
      '------------------------------
      'controlli preliminari
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134529892000, "Posizionarsi prima su una riga di griglia nella colonna 'codice articolo'"))
        Return
      End If
      If grvRighe.FocusedColumn.Name <> "ec_codart" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134783106000, "Posizionarsi prima sulla colonna 'codice articolo'"))
        Return
      End If

      '------------------------------
      'chiamo lo zoom articoli passando l'articolo come filtro per 'articoli accessori'
      NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
      oParam.nMagaz = NTSCInt(edEt_magaz.Text)
      oParam.nListino = NTSCInt(edEt_listino.Text)
      oParam.lContoCF = oCleGsor.lContoCF
      'per ricerca art. accessorio
      'la differenza tra succedaneo ed accessorio:
      '- accessorio passa come filtro aggiuntivo sempre l'articolo della riga precedente
      '- succedaneo passa come filtro aggiuntivo l'articolo della riga corrente, solo se è una nuova riga passa l'articolo della riga precedente
      oParam.strTipoArticolo = "A"
      If grvRighe.FocusedRowHandle < 0 Then
        dtrT = dsGsor.Tables("CORPO").Select("", "ec_riga DESC")
      Else
        dtrT = dsGsor.Tables("CORPO").Select("ec_riga < " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      End If
      If dtrT.Length > 0 Then oParam.strCodartAcc = NTSCStr(dtrT(0)!ec_codart)
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSelezArtSucc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezArtSucc.ItemClick
    Dim oParam As New CLE__PATB
    Dim dtrT() As DataRow = Nothing
    Try
      '------------------------------
      'controlli preliminari
      If Not RigaConArticoloOk(True) Then Return


      '------------------------------
      'chiamo lo zoom articoli passando l'articolo come filtro per 'articoli accessori'
      NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
      oParam.nMagaz = NTSCInt(edEt_magaz.Text)
      oParam.nListino = NTSCInt(edEt_listino.Text)
      oParam.lContoCF = oCleGsor.lContoCF
      'per ricerca art. accessorio
      'la differenza tra succedaneo ed accessorio:
      '- accessorio passa come filtro aggiuntivo sempre l'articolo della riga precedente
      '- succedaneo passa come filtro aggiuntivo l'articolo della riga corrente, solo se è una nuova riga passa l'articolo della riga precedente
      oParam.strTipoArticolo = "S"
      If grvRighe.FocusedRowHandle < 0 Then
        dtrT = dsGsor.Tables("CORPO").Select("", "ec_riga DESC")
      Else
        dtrT = dsGsor.Tables("CORPO").Select("ec_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      End If
      If dtrT.Length > 0 Then oParam.strCodartAcc = NTSCStr(dtrT(0)!ec_codart)
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSelezLottoUbicaz_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezLottoUbicaz.ItemClick
    Dim oParam As New CLE__PATB
    Dim dDaAss As Decimal = 0
    Try
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128569627222968750, "Indicare prima il codice articolo"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_umprz.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129048479057705441, "Selezione non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_flevas.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129048479197396517, "Selezione non consentita sulle righe già evase a saldo."))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_flevapre.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129048479324118511, "Selezione non consentita sulle righe già prenotate a saldo."))
        Return
      End If

      NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
      oParam.bLiv2 = True 'Abilitata multiselezione
      oParam.dImporto = NTSCDec(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_quant))
      oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
      oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
      oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
      oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
      NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
      If oParam.bFlag1 = False Then Return

      If oParam.oParam Is Nothing Then
        If grvRighe.NTSGetCurrentDataRow!xxo_geslotti.ToString = "S" Then
          grvRighe.NTSGetCurrentDataRow!xxo_lottox = oParam.strOut
        End If
        If oCleGsor.GetMemDttArti(NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))).Rows(0)!ar_gesubic.ToString = "S" Then
          grvRighe.NTSGetCurrentDataRow!ec_ubicaz = oParam.strDescr
        End If

        If oParam.dImporto > 0 Then         'disponbilità netta come da zoom
          If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant) = 0 Then
            'Riporta la q.ta ordinata
            grvRighe.NTSGetCurrentDataRow!ec_unmis = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_ump)
            grvRighe.NTSGetCurrentDataRow!ec_colli = oParam.dImporto

            'Se visibile chiede se assegnare
            If xxo_qtadaass.Visible Then
              If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128776134854156000, "Assegnare la quantità residua del lotto/ubicazione selezionato?")) = Windows.Forms.DialogResult.Yes Then
                grvRighe.NTSGetCurrentDataRow!xxo_qtadaass = oParam.dImporto
                grvRighe.NTSGetCurrentDataRow!xxo_flevasass = "S"
              End If
            End If
          Else
            If xxo_qtadaass.Visible Then
              dDaAss = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant) - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quaeva) - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quapre)
              If dDaAss < 0 Then dDaAss = 0
              If dDaAss = 0 Then Return
              'Se il saldo del lotto è inferiore alla q.ta da assegnare  ->
              If dDaAss > oParam.dImporto Then dDaAss = oParam.dImporto
              If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128776134868040000, "Assegnare la quantità residua del lotto/ubicazione selezionato?")) = Windows.Forms.DialogResult.Yes Then
                grvRighe.NTSGetCurrentDataRow!xxo_qtadaass = dDaAss
                grvRighe.NTSGetCurrentDataRow!xxo_flevasass = "S"
              End If
            End If
          End If
        End If
      Else
        'Se ho passato il datatable devo spezzare le righe
        oCleGsor.SpezzaRigheDaZoomLotti(CType(oParam.oParam, DataTable), grvRighe.NTSGetCurrentDataRow)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbNoteRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNoteRiga.ItemClick
    Dim frmMsgb As FRM__MSGB = Nothing
    Try
      frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)
      If grvRighe.Columns("ec_note").Visible = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557689742844788, "La colonna 'NOTE' non è visibile; impossibile visualizzarne il contenuto"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557689491840788, "Posizionarsi su una riga contenente un articolo"))
        Return
      End If
      Me.ValidaLastControl()
      frmMsgb.Init(oMenu, Nothing, DittaCorrente)
      frmMsgb.edMess.Text = grvRighe.NTSGetCurrentDataRow!ec_note.ToString
      frmMsgb.Text = "NOTE RIGA"
      frmMsgb.lConto = NTSCInt(edEt_conto.Text)
      frmMsgb.ShowDialog()
      If frmMsgb.bOk Then grvRighe.SetFocusedRowCellValue(ec_note, frmMsgb.edMess.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmMsgb Is Nothing Then frmMsgb.Dispose()
      frmMsgb = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbZoomRigheOff_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoomRigheOff.ItemClick
    Dim opar As New CLE__PATB
    Try
      If Not RigaConArticoloOk(False) Then Return

      opar.strDescr = "BNORGSOR"
      opar.strCodartAcc = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      opar.lCommessa = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      opar.lContoCF = oCleGsor.CercaLeadDaConto(NTSCInt(oCleGsor.dttET.Rows(0)!et_conto), NTSCInt(edEt_coddest.Text))
      NTSZOOM.ZoomStrIn("ZOOMARTRIGHEOFF", DittaCorrente, opar)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbCopiaRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaRiga.ItemClick
    Try
      If grRighe.ContainsFocus = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048479550218049, "Posizionarsi nella griglia del corpo del documento sulla riga da copiare"))
        Return
      End If

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048479609594569, "Posizionarsi nella griglia del corpo del documento sulla riga da copiare"))
        Return
      End If

      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

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
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557650827742788, "Copia/Incolla di righe di ORDINI DI PRODUZIONE non consentito."))
        Return
      End If

      If lRigaCopiata = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048479729753895, "Utilizzare prima la funzione 'Copia riga'"))
        Return
      End If

      If grRighe.ContainsFocus = False Or Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048479816631119, "Posizionarsi sull'ultima riga della griglia del corpo del documento"))
        Return
      End If

      dtrT = oCleGsor.dttEC.Select("ec_riga = " & lRigaCopiata.ToString)
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048479867882431, "Impossibile incollare la riga: non è stata trovata la riga n° |" & lRigaCopiata.ToString & "| copiata."))
        Return
      End If

      oCleGsor.bInIncollaRiga = True
      dsGsor.Tables("CORPO").Rows.Add(dsGsor.Tables("CORPO").NewRow)
      With dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = DittaCorrente
        !ec_magaz = dtrT(0)!ec_magaz    'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
        !ec_magaz2 = dtrT(0)!ec_magaz2
        !ec_codart = dtrT(0)!ec_codart
        !ec_fase = dtrT(0)!ec_fase
        !ec_datcons = dtrT(0)!ec_datcons
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
        !ec_confermato = dtrT(0)!ec_confermato
        !ec_lotto = dtrT(0)!ec_lotto
        !ec_ricimp = dtrT(0)!ec_ricimp
        !ec_misura1 = dtrT(0)!ec_misura1
        !ec_misura2 = dtrT(0)!ec_misura2
        !ec_misura3 = dtrT(0)!ec_misura3
        !ec_umprz = dtrT(0)!ec_umprz
        !ec_datini = dtrT(0)!ec_datini
        !ec_datfin = dtrT(0)!ec_datfin
      End With

      'tco: riverso i dati del tco: la nuova riga tco è stata creata in automatico alla creazione della nuova riga dttEC
      If Not oCleGsor.dttECTC Is Nothing Then
        dtrT = oCleGsor.dttECTC.Select("ec_riga = " & lRigaCopiata.ToString)
        If dtrT.Length > 0 Then
          For i = 1 To 24
            oCleGsor.dttECTC.Rows(oCleGsor.dttECTC.Rows.Count - 1)("ec_quant" & i.ToString("00")) = dtrT(0)("ec_quant" & i.ToString("00"))
          Next
        End If
      End If

      oCleGsor.RecordSalva(oCleGsor.dttEC.Rows.Count - 1, False, Nothing)

      'mi posiziono sulla nuova riga
      grvRighe.MoveLast()
      'dcGsor.Find("ec_riga = " & )

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oCleGsor.bInIncollaRiga = False
    End Try
  End Sub

  Public Overridable Sub tlbInsRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInsRiga.ItemClick
    Dim lPrevRiga As Integer = 0
    Dim lNewRiga As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If dsGsor.Tables("CORPO").Rows.Count = 0 Then Return
      If grvRighe.FocusedRowHandle < 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048480017261255, "Posizionarsi prima su una riga già compilata"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

      dtrT = dsGsor.Tables("CORPO").Select("ec_riga < " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      If dtrT.Length > 0 Then lPrevRiga = NTSCInt(dtrT(0)!ec_riga)
      lNewRiga = NTSCInt(Fix((NTSCDec((NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) - lPrevRiga) / 2) + lPrevRiga)))

      If dsGsor.Tables("CORPO").Select("ec_riga = " & lNewRiga.ToString).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048480148983377, "Impossibile inserire una riga intermedia; riga |" & lNewRiga.ToString & "| già presente nel corpo del documento"))
        Return
      End If

      dsGsor.Tables("CORPO").Rows.Add(dsGsor.Tables("CORPO").NewRow)
      'forzo la MovordOnAddNewRow
      dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)!codditt = "."
      dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)!codditt = DittaCorrente

      dsGsor.Tables("CORPO").Rows(dsGsor.Tables("CORPO").Rows.Count - 1)!ec_riga = lNewRiga
      oCleGsor.lCrighe -= oCleGsor.nIncremContatoreRiga

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

  Public Overridable Sub tlbInsRigheDaFigliDB_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInsRigheDaFigliDB.ItemClick
    Dim frmSear As FRMORSEAR = Nothing
    Try
      Me.ValidaLastControl()
      If Not oCleGsor.OkTestata Then Return
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

      frmSear = CType(NTSNewFormModal("FRMORSEAR"), FRMORSEAR)
      frmSear.Init(oMenu, Nothing, DittaCorrente)
      frmSear.InitEntity(oCleGsor)
      frmSear.ShowDialog()

      Me.Cursor = Cursors.WaitCursor

      oCleGsor.CaricaCorpoDaDB(frmSear.edCodart.Text, NTSCInt(frmSear.edFase.Text), _
                               NTSCStr(frmSear.cbUnmis.SelectedValue), NTSCDec(frmSear.edColli.Text), _
                               NTSCDate(frmSear.edDatavalDB.Text).ToShortDateString)
      grvRighe.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      If Not frmSear Is Nothing Then frmSear.Dispose()
      frmSear = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApriConfiguratore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriConfiguratore.ItemClick
    Dim strParam As String = ""
    Dim nRowHandle As Integer = 0
    Try
      nRowHandle = grvRighe.FocusedRowHandle
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return

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

  Public Overridable Sub tlbNavigazMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazMrp.ItemClick
    Dim strParam As String = ""
    Try
      Select Case oCleGsor.dttET.Rows(0)!et_tipork.ToString
        Case "$", "O", "H", "X", "R", "#", "V"
          'posso continuare
        Case Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 129048480278674197, "Funzione non disponibile per questo tipo di documento"))
      End Select

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129048480318675221, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga nel corpo di un ordine/impegno."))
        Return
      End If

      strParam = "O;" & oCleGsor.dttET.Rows(0)!et_tipork.ToString & ";" & _
                edAnnoDoc.Text & ";" & _
                edSerieDoc.Text.PadRight(1).Substring(0, 1) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(edNumDoc.Text).ToString.PadLeft(9, "0"c), 9) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString.PadLeft(9, "0"c), 9) & ";" & _
                grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase).ToString.PadLeft(4, "0"c), 4)
      oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", oApp.Tr(Me, 129048480465553981, "Navigazione MRP"), DittaCorrente, "", "", Nothing, strParam, True, True)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbDettQtaDate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettQtaDate.ItemClick
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Dim dtrT As DataRow = Nothing
    Dim dttTmp1 As New DataTable
    Dim frmDemo As FRMORDEMO = Nothing
    Try
      '------------------------------
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558561833976000, "Funzione non abilitata per ORDINI DI PRODUZIONE"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135002932000, "Indicare prima il codice articolo"))
        Return
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558562241448000, "Indicare prima il codice articolo"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Added Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558562735968000, "Funzione abilitata solo su nuove righe con codice articolo impostato"))
        Return
      End If
      If grvRighe.FocusedColumn.Name = "ec_quant" Then
        grvRighe.FocusedColumn = ec_descr     'valido l'articolo spostandomi di colonna, se serve
      End If
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codiva) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558570795954000, "Codice IVA non impostato"))
        Return
      End If
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_controp) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558570991890000, "Codice CONTROPARTITA non impostato"))
        Return
      End If
      oCleGsor.GetMemDttArti(NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart))
      If NTSCInt(oCleGsor.dttArti.Rows(0)!ar_codtagl) <> 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128558568177296000, "Funzione non abilitata per articoli a Taglie e colori"))
        Return
      End If

      '------------------------------
      'preparo la tabella temporanea
      dttTmp.TableName = "DEMO"
      dttTmp.Columns.Add("ec_colli", GetType(Decimal))
      dttTmp.Columns.Add("ec_quant", GetType(Decimal))
      dttTmp.Columns.Add("ec_datcons", GetType(DateTime))
      dttTmp.Columns.Add("ec_codart", GetType(String))
      dttTmp.Columns.Add("ec_unmis", GetType(String))
      dttTmp.Columns.Add("ec_ump", GetType(String))
      dttTmp.Columns.Add("ec_misura1", GetType(Decimal))
      dttTmp.Columns.Add("ec_misura2", GetType(Decimal))
      dttTmp.Columns.Add("ec_misura3", GetType(Decimal))

      dttTmp.Columns("ec_colli").DefaultValue = 0
      dttTmp.Columns("ec_quant").DefaultValue = 0
      dttTmp.Columns("ec_datcons").DefaultValue = NTSCDate(DateTime.Now.ToShortDateString)
      dttTmp.Columns("ec_codart").DefaultValue = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      dttTmp.Columns("ec_unmis").DefaultValue = grvRighe.NTSGetCurrentDataRow!ec_unmis.ToString
      dttTmp.Columns("ec_ump").DefaultValue = grvRighe.NTSGetCurrentDataRow!ec_ump.ToString
      dttTmp.Columns("ec_misura1").DefaultValue = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_misura1)
      dttTmp.Columns("ec_misura2").DefaultValue = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_misura2)
      dttTmp.Columns("ec_misura3").DefaultValue = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_misura3)
      dttTmp.AcceptChanges()

      '------------------------------
      'visualizzo la form
      frmDemo = CType(NTSNewFormModal("FRMORDEMO"), FRMORDEMO)
      frmDemo.Init(oMenu, Nothing, DittaCorrente)
      frmDemo.InitEntity(oCleGsor)
      frmDemo.dsDemo.Tables.Add(dttTmp)
      frmDemo.ShowDialog()

      '------------------------------
      'se non ho aggiunto righe esco
      If dttTmp.Rows.Count = 0 Then Return

      Me.Cursor = Cursors.WaitCursor
      'prim aaggiorno la riga corrente
      grvRighe.NTSGetCurrentDataRow!ec_colli = NTSCDec(dttTmp.Rows(0)!ec_colli)
      grvRighe.NTSGetCurrentDataRow!ec_quant = NTSCDec(dttTmp.Rows(0)!ec_quant)
      grvRighe.NTSGetCurrentDataRow!ec_datcons = NTSCDate(dttTmp.Rows(0)!ec_datcons)
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return
      oCleGsor.dttEC.AcceptChanges()
      dtrT = grvRighe.NTSGetCurrentDataRow

      'ora aggiungo le righe successive
      dttTmp1 = oCleGsor.dttEC.Clone
      dttTmp1.Clear()
      For i = 1 To dttTmp.Rows.Count - 1
        dttTmp1.ImportRow(grvRighe.NTSGetCurrentDataRow)
        oCleGsor.lCrighe = oCleGsor.lCrighe + oCleGsor.nIncremContatoreRiga
        dttTmp1.Rows(dttTmp1.Rows.Count - 1)!ec_riga = oCleGsor.lCrighe
        dttTmp1.Rows(dttTmp1.Rows.Count - 1)!ec_colli = NTSCDec(dttTmp.Rows(i)!ec_colli)
        dttTmp1.Rows(dttTmp1.Rows.Count - 1)!ec_quant = NTSCDec(dttTmp.Rows(i)!ec_quant)
        dttTmp1.Rows(dttTmp1.Rows.Count - 1)!ec_datcons = NTSCDate(dttTmp.Rows(i)!ec_datcons)
        oCleGsor.dttEC.ImportRow(dttTmp1.Rows(dttTmp1.Rows.Count - 1))
        oCleGsor.SettaValoriRiga(oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1))    'ricalcolo il valore della riga
        oCleGsor.dttEC.AcceptChanges()
      Next
      dttTmp1.Clear()
      oApp.MsgBoxInfo(oApp.Tr(Me, 128558576881560000, "Inserimento righe completato: inserite |" & dttTmp.Rows.Count.ToString & "| righe"))
      AbilitaDisabilitaRigaNum()
    Catch ex As Exception
      dsGsor.Tables("CORPO").RejectChanges()
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDemo Is Nothing Then frmDemo.Dispose()
      frmDemo = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbVisDispComm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisDispComm.ItemClick
    Try
      '------------------------------
      'controlli preliminari
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135046456000, "Magazzino sulla riga non impostato"))
        Return
      End If

      '------------------------------
      If Not oCleGsor.GetDisponibilitaCommessa(NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart), _
                                               NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase), _
                                               NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz), _
                                               oCleGsor.lRilevaDisponibCodCommessa) Then Return

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbVisDettEvasione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisDettEvasione.ItemClick
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Dim strTmp1 As String = ""
    Try
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "Q" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128613552802187500, "Funzione non abilitata per documenti di tipo 'Preventivo'"))
        Return
      End If
      '------------------------------
      'controlli preliminari
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128613043068906250, "Posizionarsi su una riga con codice articolo impostato"))
        Return
      End If
      If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quaeva) = 0 And NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quapre) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135108700000, "La riga non è stata evasa con nessun documento"))
        Return
      End If

      '------------------------------
      If Not oCleGsor.GetDettaglioEvasione(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga), dttTmp) Then Return
      If dttTmp.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128613038545781250, "La riga non è stata evasa con nessun documento"))
        Return
      End If

      strTmp = oApp.Tr(Me, 128613039360937500, "Dettaglio evasione riga: " & vbCrLf & "Tipo doc/anno/serie/numero/riga/quant." & vbCrLf & vbCrLf)
      For Each dtrT As DataRow In dttTmp.Rows
        Select Case dtrT!mm_tipork.ToString
          Case "R" : strTmp1 = oApp.Tr(Me, 128613565975312500, "Impegno cliente")
          Case "O" : strTmp1 = oApp.Tr(Me, 128613565998437500, "Ordine fornitore")
          Case "H" : strTmp1 = oApp.Tr(Me, 128613566100312500, "Ordine di produzione")
          Case "X" : strTmp1 = oApp.Tr(Me, 128613566120468750, "Impegno Trasferimento")
          Case "Q" : strTmp1 = oApp.Tr(Me, 128613566135312500, "Preventivo")
          Case "#" : strTmp1 = oApp.Tr(Me, 128613566162656250, "Impegno di commessa")
          Case "V" : strTmp1 = oApp.Tr(Me, 128613566178437500, "Impegno cliente aperto")
          Case "$" : strTmp1 = oApp.Tr(Me, 128613566203437500, "Ordine fornitore aperto")
          Case "Y" : strTmp1 = oApp.Tr(Me, 128613566225156250, "Impegno di produzione")
          Case "A" : strTmp1 = oApp.Tr(Me, 128613566240781250, "Fatture Imm. emesse")
          Case "B" : strTmp1 = oApp.Tr(Me, 128613566254062500, "D.D.T. emessi")
          Case "C" : strTmp1 = oApp.Tr(Me, 128613566273281250, "Corrispettivi emessi")
          Case "D" : strTmp1 = oApp.Tr(Me, 128613566297968750, "Fatture Diff. emesse")
          Case "E" : strTmp1 = oApp.Tr(Me, 128613566317656250, "Note di Addebito emesse")
          Case "F" : strTmp1 = oApp.Tr(Me, 128613566333125000, "Ric.Fiscale Emessa")
          Case "I" : strTmp1 = oApp.Tr(Me, 128613566347812500, "Riemissione Ric.Fiscali")
          Case "J" : strTmp1 = oApp.Tr(Me, 128613566367343750, "Note Accr. ricevute")
          Case "K" : strTmp1 = oApp.Tr(Me, 128613566384375000, "Fatture Diff. ricevute")
          Case "L" : strTmp1 = oApp.Tr(Me, 128613566446875000, "Fatture Imm. ricevute")
          Case "M" : strTmp1 = oApp.Tr(Me, 128613566464218750, "D.D.T. ricevuti")
          Case "N" : strTmp1 = oApp.Tr(Me, 128613566478906250, "Note Accr. emesse")
          Case "P" : strTmp1 = oApp.Tr(Me, 128613566494375000, "Fatt.Ric.Fisc.Differita")
          Case "S" : strTmp1 = oApp.Tr(Me, 128613566508906250, "Fatt.Ric.Fisc. Emessa")
          Case "T" : strTmp1 = oApp.Tr(Me, 128613566526718750, "Carico da produz.")
          Case "U" : strTmp1 = oApp.Tr(Me, 128613566560625000, "Scarico a produz.")
          Case "Z" : strTmp1 = oApp.Tr(Me, 128613566587187500, "Bolle di mov. interna")
          Case "W" : strTmp1 = oApp.Tr(Me, 128717324936093750, "Note di prelievo")

          Case Else : strTmp1 = dtrT!mm_tipork.ToString
        End Select
        strTmp += strTmp1 & "/" & dtrT!mm_anno.ToString & "/" & dtrT!mm_serie.ToString & _
                   "/" & dtrT!mm_numdoc.ToString & "/" & dtrT!mm_riga.ToString & _
                   ":   " & NTSCDec(dtrT!mm_qtadisimp).ToString(oApp.FormatQta) & vbCrLf
      Next
      oApp.MsgBoxInfo(strTmp)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
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
        oApp.MsgBoxErr(oApp.Tr(Me, 129048480603213755, "Magazzino sulla riga non impostato"))
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
          oCleGsor.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          grvRighe.NTSGetCurrentDataRow!ec_colli = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
          oCleGsor.bInValidazQuant = False
          Return
        End If

        If CType(oCleGsor.oCleComm, CLELBMENU).ConvQuantUM(DittaCorrente, !ec_codart.ToString, !ec_ump.ToString, _
                  NTSCDec(!ec_quant), NTSCDec(!ec_misura1), NTSCDec(!ec_misura2), _
                  NTSCDec(!ec_misura3), !ec_unmis.ToString, dQuant, strErrore, oApp.NDecQta) Then
          oCleGsor.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          !ec_colli = dQuant
          oCleGsor.bInValidazQuant = False
          If strErrore <> "" Then oApp.MsgBoxErr(strErrore)
        End If

        If !ec_umprz.ToString = "S" Then     'ricalcolo il prezzo se unità di misura su colli
          'devo farlo perchè altrimenti il prezzo viene ricalcolato solo al cambio della quantità
          'ma in questo caso la qta non cambia perchè 'bInValidazQuant = True'
          oCleGsor.SettaCondCommerciali(True, False, False, grvRighe.NTSGetCurrentDataRow, 0, 0, False)
          oCleGsor.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
        End If
      End With

    Catch ex As Exception
      oCleGsor.bInValidazQuant = False
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRicalcolaListini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcolaListini.ItemClick
    Dim dPrezzoBase As Decimal = 0
    Dim oPar As New CLE__CLDP
    Try
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129177151258005645, "Attenzione!" & vbCrLf & _
          "Funzionalità non consentita in presenza di documenti di tipo:" & vbCrLf & _
          " --> |'" & cbTipoDoc.Text & "'|."))
        Return
      End If
      '------------------------------
      'controlli preliminari
      If Not RigaConArticoloOk(False) Then Return
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776136210596000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If

      If NTSCInt(edEt_valuta.Text) = 0 Then
        dPrezzoBase = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prezzo)
      Else
        dPrezzoBase = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prezvalc)
      End If

      If CBool(oMenu.GetSettingBus("BSMGRCPR", "OPZIONI", ".", "PrezzoBaseLordo", "0", " ", "0")) Then
        'il prezzo di partenza è al lordo degli sconti
      Else
        dPrezzoBase = (dPrezzoBase * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont1)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont2)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont3)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont4)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont5)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont6)) / 100 * (100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scontp)) / 100 - NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scontv)) * (100 - NTSCDec(edEt_scont1.Text)) / 100 * (100 - NTSCDec(edEt_scont2.Text)) / 100 * (100 - NTSCDec(edEt_scopag.Text)) / 100
        dPrezzoBase = ArrDbl(dPrezzoBase, oMenu.TrovaNdecSuPrzUn(NTSCInt(edEt_valuta.Text)))
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSORGSOR"
      oPar.dPar1 = NTSCInt(edEt_conto.Text)
      oPar.dPar2 = NTSCInt(edEt_valuta.Text)
      oPar.dPar3 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar4 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
      oPar.dPar5 = dPrezzoBase
      oPar.strPar1 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
      oPar.strPar2 = NTSCDate(edet_datdoc.Text).ToShortDateString
      oPar.strPar3 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_ump)
      oPar.strPar4 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_flprznet)
      oMenu.RunChild("NTSInformatica", "FRMMGRCPR", "", DittaCorrente, "", "BNMGRCPR", oPar, "", True, True)

    Catch ex As Exception
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
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135934832000, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And Not oCleGsor.bTerzista Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128576533131562500, "Funzione abilitata solo su Ordini di produzione a terzisti e non interni."))
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
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
        nCodlavo = 0
        dtrT = dsGsor.Tables("ATTIVIT").Select("at_riga = " & grvRighe.NTSGetCurrentDataRow!ec_riga.ToString, "at_riga, at_fase")
        If dtrT.Length > 0 Then nCodlavo = NTSCInt(dtrT(0)!at_codlavo)
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
        !agente2 = NTSCInt(edEt_codagen2.Text)
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
      oPar.strNomProg = "BNORGSOR"
      oPar.ctlPar1 = dttCopa
      oPar.bPar1 = False             'visualizza la form di FRMVECOPA
      oMenu.RunChild("NTSInformatica", "FRMVECOPA", "", DittaCorrente, "", "BNVECOPA", oPar, "", True, True)

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
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oanum) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135182956000, "La riga su cui si è posizionati non evade ordini aperti"))
        Return
      End If

      strParam = "APRI;" & grvRighe.NTSGetCurrentDataRow!ec_oatipo.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oaanno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_oaserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oanum).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128589367205937500, "Gestione ordini/impegni"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbApriOffertaCollegata_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriOffertaCollegata.ItemClick
    Dim strParam As String = ""
    Try
      If Not RigaConArticoloOk(False) Then Return
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oqnum) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128717309928750000, "La riga su cui si è posizionati non evade offerte"))
        Return
      End If

      strParam = "APRI;" & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oqanno).ToString.PadLeft(4, "0"c) & ";" & _
           grvRighe.NTSGetCurrentDataRow!ec_oqserie.ToString & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oqnum).ToString.PadLeft(9, "0"c) & ";" & _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_oqvers).ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 128728091749375000, "Gestione offerte"), DittaCorrente, "", "", Nothing, strParam, True, True)

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
    Dim oParam As New CLE__CLDP
    Dim frmLayo As FRMMGLAYO = Nothing
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

      oCleGsor.strTermKey = "Terminale"
      If oCleGsor.GetImpostazioniFile("BSORGSOR", dttTmp) = True Then
        If dttTmp.Rows.Count > 0 Then
          frmLayo = CType(NTSNewFormModal("FRMMGLAYO"), FRMMGLAYO)
          frmLayo.Init(oMenu, oParam, DittaCorrente)
          frmLayo.InitEntity(oCleGsor)
          frmLayo.ShowDialog()
          If oCleGsor.bLayoAnnullato = True Then Return
        End If
      End If
      strImport = oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", oCleGsor.strTermKey, ".", "Import01", "", " ", "").Split("|"c)

      If strImport.Length <= 1 Then strImport = "0||0|C:\?|0|0|0|0|0|0|S|§|,|0|1|1|0|0|1|0|NOCFG".Split("|"c) 'così verifica cmq se c'è il tracciato.

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleGsor.SeparatePathAndFileName(strImport(3), strPath, strFile)
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

      oCleGsor.ImportaRighe(strImport, "BSORGSOR")

      '---------------------------
      'Visualizza il log
      If CBool(strImport(9)) Or oCleGsor.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleGsor.LogFileName)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If frmLayo IsNot Nothing Then frmLayo.Dispose()
      frmLayo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbExport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbExport.ItemClick
    Dim dtrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim strExport() As String
    Dim strPath As String = ""
    Dim strFile As String = ""
    Dim oParam As New CLE__CLDP
    Dim frmLayo As FRMMGLAYO = Nothing
    Try
      dtrRow = dsGsor.Tables("CORPO").Select("xx_sel = 'S'")
      If dtrRow.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129248818662149899, "Selezionare almeno una riga da esportare."))
        Return
      End If

      oCleGsor.strTermKey = "Terminale"
      If oCleGsor.GetImpostazioniFile("BSORGSOR", dttTmp) = True Then
        If dttTmp.Rows.Count > 0 Then
          frmLayo = CType(NTSNewFormModal("FRMMGLAYO"), FRMMGLAYO)
          frmLayo.Init(oMenu, oParam, DittaCorrente)
          frmLayo.InitEntity(oCleGsor)
          frmLayo.ShowDialog()
          If oCleGsor.bLayoAnnullato = True Then Return
        End If
      End If
      strExport = oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", oCleGsor.strTermKey, ".", "Export01", "", " ", "").Split("|"c)
      'Se non esiste la configurazione do errore
      If strExport.Length <= 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129248818703401483, "Tracciato di esportazione non configurato. Impossibile continuare."))
        Return
      End If

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleGsor.SeparatePathAndFileName(strExport(3), strPath, strFile)
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

      If oCleGsor.EsportaRighe(strExport, dtrRow, "BSORGSOR") Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129248624479620804, "Esportazione completata correttamente."))
      End If

      '---------------------------
      'Visualizza il log
      If CBool(strExport(5)) Or oCleGsor.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleGsor.LogFileName)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If frmLayo IsNot Nothing Then frmLayo.Dispose()
      frmLayo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSelectDeselect_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelectDeselect.ItemClick
    Try
      If dsGsor.Tables("CORPO").Select("xx_sel = 'N'").Length = 0 Then
        For z As Integer = 0 To dsGsor.Tables("CORPO").Rows.Count - 1
          dsGsor.Tables("CORPO").Rows(z)!xx_sel = "N"
        Next
      Else
        For z As Integer = 0 To dsGsor.Tables("CORPO").Rows.Count - 1
          dsGsor.Tables("CORPO").Rows(z)!xx_sel = "S"
        Next
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

      For i = dsGsor.Tables("CORPO").Rows.Count - 1 To 0 Step -1
        If NTSCStr(dsGsor.Tables("CORPO").Rows(i)!xx_sel) = "S" Then
          dtrDeleted = dsGsor.Tables("CORPO").Rows(i)
          If Not oCleGsor.CorpoTestPreCancella(i) Then Continue For
          oCleGsor.RecordSalva(i, True, dtrDeleted)
          dsGsor.Tables("CORPO").Rows(i).Delete()
          dsGsor.Tables("CORPO").AcceptChanges()
        End If
      Next

      'riabilito, se necessario, il codice valuta
      If dsGsor.Tables("CORPO").Rows.Count = 0 Then
        If ckEt_scorpo.Checked = False Then
          If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
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
        If oCleGsor.dttEt_conto.Rows.Count = 0 Then Return
        bNuovo = False
      End If

      oTmp.Text = edEt_conto.Text
      If oCleGsor.dttEt_conto.Rows.Count = 0 Then
        If oCleGsor.bDocEmesso Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      Else
        If oCleGsor.dttEt_conto.Rows(0)!an_tipo.ToString = "C" Then
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

  '------------------------------

  Public Overridable Sub SetStato(ByVal nStato As Integer, ByVal bRicaricaGctlInStato1 As Boolean)
    '0 = nessun documento aperto
    '1 = sono dentro ad un documento, nuovo o in modifica
    Dim i As Integer = 0
    Dim strSerieOld As String = ""
    Try
      Select Case nStato
        Case 0
          'memorizzo l'ultimo documento trattato
          If NTSCInt(edNumDoc.Text) > 0 Then
            strLastTipork = cbTipoDoc.SelectedValue.ToUpper
            nLastAnno = NTSCInt(edAnnoDoc.Text)
            strLastSerie = edSerieDoc.Text
            lLastNumdoc = NTSCInt(edNumDoc.Text)
          End If

          tsGsor.Visible = False
          If tsGsor.ContainsFocus Then edEt_conto.Focus() 'diversamente quando salvo impostando come tabpageindex = 0 viene simulata la pressione del TAB per entrare nel primo campo da bn__chil ed il risultato è che con multireport il focus va su ANNULLA invece che rimanere su OK
          tsGsor.SelectedTabPageIndex = 0

          If oCleGsor.bModGPV Then SetBlocchiRetail(True)

          GctlSetVisEnab(cbTipoDoc, True)
          GctlSetVisEnab(cbTipoDoc, False)
          GctlSetVisEnab(edAnnoDoc, False)
          GctlSetVisEnab(edSerieDoc, False)
          GctlSetVisEnab(edNumDoc, False)

          If edAnnoDoc.Text = "" Then edAnnoDoc.Text = DateTime.Now.Year.ToString
          If edSerieDoc.Text = "" Then edSerieDoc.Text = " "
          If edNumDoc.Text = "" Then edNumDoc.Text = "0"
          strLastColEdited = ""

          GctlSetVisEnab(tlbCambioDitta, False)

          '----------------------------------------------
          'menu e toolbar
          tlbNuovoOrdineDaPrec.Enabled = False
          tlbDatiContabiliSintetici.Enabled = False
          tlbModifNumOrd.Enabled = False
          tlbZoomBanche.Enabled = False
          tlbGenNumComm.Enabled = False
          tlbNavigazDoc.Enabled = False
          tlbVisDettTCO.Enabled = False
          tlbApriChiamata.Enabled = False
          tlbOggettiAssociati.Enabled = False
          tlbMnuFile.Enabled = False
          tlbMnuRecord.Enabled = False
          tlbSalva.Enabled = False
          tlbCancella.Enabled = False
          tlbRipristina.Enabled = False
          tlbRecordCancella.Enabled = False
          tlbRecordRipristina.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tlbStampaPdf.Enabled = False
          tlbStampaWord.Enabled = False
          tlbStampaEtichetteRiga.Enabled = False
          tlbSelOFA.Enabled = False
          tlbSelOfferta.Enabled = False
          tlbDettaglioTCO.Enabled = False
          If bDaTastiNavigazione = False Then
            tlbApriDocPrec.Enabled = False
            tlbApriDocSucc.Enabled = False
          End If

          GctlSetVisEnab(tlbNuovo, False)
          GctlSetVisEnab(tlbApri, False)

          oCleGsor.bNew = False
          tsGsor.SelectedTabPageIndex = 0

          GctlSetVisEnab(edEt_valuta, False)
          GctlSetVisEnab(ckEt_scorpo, False)
          'GctlSetVisEnab(ckEt_flevas, False)
          GctlSetVisEnab(fmPa, False)

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

          cmdDatiTesta1_Click(cmdDatiTesta1, Nothing)
          cmdPiedeTotali_Click(cmdPiedeTotali, Nothing)
        Case 1
          tsGsor.Visible = True

          cbTipoDoc.Enabled = False
          edAnnoDoc.Enabled = False
          edSerieDoc.Enabled = False
          edNumDoc.Enabled = False 'devo poter dare la possibilità di cambiare numero doc quando faccio 'nuovo': no: crea problemi con impegni collegati ... usare la funzione 'cambia numero doc'

          tlbCambioDitta.Enabled = False

          '-------------------------------
          'applico le impostazioni da GCTL ed i valori di default: 
          'la griglia deve gi essere collegata al datatable
          'GIA' FATTO IN PRECEDENZA!!!!!!! bRicaricaGctlInStato1 sempre = false
          'GctlTipoDoc = cbTipoDoc.SelectedValue().ToString
          'If bRicaricaGctlInStato1 Then GctlSetRoules()
          If oCleGsor.bNew Then
            'se ho impostato tramite GCTL che in nuovo la serie deve essere una specifica, devo ricalcolare il numero documento
            strSerieOld = edSerieDoc.Text
            GctlApplicaDefaultValue()
            If strSerieOld <> edSerieDoc.Text Then
              edNumDoc.Text = oCleGsor.LegNuma(IIf(cbTipoDoc.SelectedValue.ToString() = "V", "VV", cbTipoDoc.SelectedValue.ToString()).ToString, edSerieDoc.Text, NTSCInt(edAnnoDoc.Text)).ToString
              oCleGsor.CambiaNumdoc(dsGsor, NTSCInt(edNumDoc.Text), edSerieDoc.Text)
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
          GctlSetVisEnab(tlbStampaPdf, False)
          GctlSetVisEnab(tlbStampaWord, False)
          GctlSetVisEnab(tlbDatiContabiliSintetici, False)
          If Not oCleGsor.bNew Then GctlSetVisEnab(tlbNuovoOrdineDaPrec, False)
          If oCleGsor.bNew Then GctlSetVisEnab(tlbModifNumOrd, False)
          GctlSetVisEnab(tlbZoomBanche, False)
          GctlSetVisEnab(tlbGenNumComm, False)
          GctlSetVisEnab(tlbNavigazDoc, False)
          GctlSetVisEnab(tlbVisDettTCO, False)
          GctlSetVisEnab(tlbApriChiamata, False)
          GctlSetVisEnab(tlbOggettiAssociati, False)
          'GctlSetVisEnab(tlbStampaEtichetteRiga, False)      'gestito in tsGsor_SelectedPageChanged
          'GctlSetVisEnab(tlbMnuRecord, False)                'gestito in tsGsor_SelectedPageChanged
          'GctlSetVisEnab(tlbRecordRipristina, False)         'gestito in tsGsor_SelectedPageChanged
          'GctlSetVisEnab(tlbRecordCancella, False)           'gestito in tsGsor_SelectedPageChanged
          GctlSetVisEnab(tlbSelOFA, False)
          GctlSetVisEnab(tlbSelOfferta, False)
          'GctlSetVisEnab(tlbDettaglioTCO, False)             'gestito in tsGsor_SelectedPageChanged

          '-------------------------------------------------
          'nascondo le voci di menu non gestite
          If cbTipoDoc.SelectedValue.ToString <> "H" Then
            tlbImpCollegati.Visible = False
            tlbLavCollegate.Visible = False
          Else
            GctlSetVisEnab(tlbImpCollegati, True)
            GctlSetVisEnab(tlbLavCollegate, True)
          End If

          If cbTipoDoc.SelectedValue.ToString <> "$" And _
             cbTipoDoc.SelectedValue.ToString <> "O" And _
             cbTipoDoc.SelectedValue.ToString <> "H" And _
             cbTipoDoc.SelectedValue.ToString <> "X" And _
             cbTipoDoc.SelectedValue.ToString <> "R" And _
             cbTipoDoc.SelectedValue.ToString <> "#" And _
             cbTipoDoc.SelectedValue.ToString <> "V" Then
            tlbNavigazMrp.Enabled = False
          Else
            GctlSetVisEnab(tlbNavigazMrp, False)
          End If

          If oCleGsor.bModCP2 Then
            GctlSetVisEnab(tlbApriConfiguratore, True)
          Else
            tlbApriConfiguratore.Visible = False
          End If

          tlbNuovo.Enabled = False
          tlbApri.Enabled = False

          '----------------------------------------------
          'disabilito i magazzini non impostabili
          If cbTipoDoc.SelectedValue.ToString <> "H" Then
            edEt_magimp.Enabled = False
          Else
            GctlSetVisEnab(edEt_magimp, False)
          End If

          If cbTipoDoc.SelectedValue.ToString = "Q" Then
            If ckEt_flevas.Checked = False Then
              GctlSetVisEnab(ckEt_flevas, False)
            Else
              ckEt_flevas.Enabled = False
            End If
          Else
            ckEt_flevas.Enabled = False
          End If

          If cbTipoDoc.SelectedValue.ToString <> "X" Then
            edEt_magaz2.Enabled = False
            ec_magaz2.Visible = False
            xxo_magaz2.Visible = False
          Else
            GctlSetVisEnab(edEt_magaz2, False)
          End If
          grvRighe.FocusedRowHandle = 0

          If oCleGsor.bModPM And oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
            edEt_valuta.Enabled = False
            edEt_cambio.Enabled = False
            edEt_valuta.Text = "0"
          End If

          If tlbVisDispComm.Caption.IndexOf("(") > -1 Then
            tlbVisDispComm.Caption = tlbVisDispComm.Caption.Substring(0, tlbVisDispComm.Caption.IndexOf("(")).Trim
          End If
          tlbVisDispComm.Caption += " (" & oCleGsor.lRilevaDisponibCodCommessa & ")"

          'disabilito la gestione di CUP e CIG per i tipi documento non gestiti
          If cbTipoDoc.SelectedValue.ToString = "#" Or cbTipoDoc.SelectedValue.ToString = "H" Or _
             cbTipoDoc.SelectedValue.ToString = "Q" Or cbTipoDoc.SelectedValue.ToString = "X" Then
            fmPa.Enabled = False
          Else
            GctlSetVisEnab(fmPa, False)
          End If
      End Select

      grvRighe.LeftCoord = 0
      If ec_matric.Visible And oCleGsor.bDisabilitaDecodificaBarcode = False Then
        grvRighe.FocusedColumn = ec_matric
      Else
        grvRighe.FocusedColumn = ec_codart
      End If

      'visualizzo o meno la griglia TCO: se lo statoè 0 nasconde, negli altri casi opera in base alla riga su cui sono (se gestita a tco o no)
      GestisciGrigliaTCO()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SetBlocchiRetail(ByVal bReset As Boolean) As Boolean
    'se ho aperto un documento creato con il retail nuovo, non posso modificare 
    'in alcun modo i valori di riga e totali documento, visto che influirebbero
    'sulla gestione delle promozioni
    Try
      If oCleGsor.dttET Is Nothing Then Return True

      If bReset Then
        GctlSetConfigGrid()
        GctlSetVisEnab(tlbNuovoOrdineDaPrec, False)
        GctlSetVisEnab(tlbRicalcolaColliQta, False)
        GctlSetVisEnab(tlbInsRiga, False)
        GctlSetVisEnab(tlbSelOFA, False)
        GctlSetVisEnab(tlbSelOfferta, False)
        GctlSetVisEnab(tlbDaLista, False)
        GctlSetVisEnab(tlbInsRigheDaFigliDB, False)
        GctlSetVisEnab(tlbImport, False)
        GctlSetVisEnab(tlbSelezArtAcc, False)
        GctlSetVisEnab(tlbSelezArtSucc, False)
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
        GctlSetConfigGrid()

        GctlSetEnable(pnContratti)

        If oCleGsor.dttET.Rows.Count > 0 AndAlso NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) = "S" Then
          'documento retail: non posso modificare i valori di riga
          GctlSetEnable(ec_codart)
          GctlSetEnable(ec_fase)
          GctlSetEnable(ec_unmis)
          GctlSetEnable(ec_colli)
          GctlSetEnable(ec_quant)
          GctlSetEnable(ec_prezzo)
          GctlSetEnable(ec_prezvalc)
          GctlSetEnable(ec_scont1)
          GctlSetEnable(ec_scont2)
          GctlSetEnable(ec_scont3)
          GctlSetEnable(ec_scont4)
          GctlSetEnable(ec_scont5)
          GctlSetEnable(ec_scont6)
          GctlSetEnable(ec_codiva)
          GctlSetEnable(ec_stasino)
        End If

        Return True
      End If

      If oCleGsor.dttET.Rows.Count = 0 Then Return True
      If NTSCStr(oCleGsor.dttET.Rows(0)!et_retail) <> "S" Then
        Return True
      Else
        'test per documento fatto con nuovo retail (direttamente in testmagc)
        oApp.MsgBoxInfo(oApp.Tr(Me, 129588454981318359, "Attenzione!" & vbCrLf & _
                            "Il documento selezionato è stato creato con il programma" & vbCrLf & _
                            "'Gestione unificata documenti'." & vbCrLf & _
                            "Non sarà possibile modificare tutto ciò che potrebbe influire " & vbCrLf & _
                            "sulla determinazione dei valori del documento." & vbCrLf & _
                            "Alcune informazione specifiche del retail non saranno visualizzate"))

        tlbNuovoOrdineDaPrec.Enabled = False
        tlbRicalcolaColliQta.Enabled = False
        tlbInsRiga.Enabled = False
        tlbSelOFA.Enabled = False
        tlbSelOfferta.Enabled = False
        tlbDaLista.Enabled = False
        tlbInsRigheDaFigliDB.Enabled = False
        tlbImport.Enabled = False
        tlbSelezArtAcc.Enabled = False
        tlbSelezArtSucc.Enabled = False
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
        pnContratti.Enabled = False
        'spostati in ingresso della griglia
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
      End If    'If NTSCStr(oCleBoll.dttETC.Rows(0)!tm_retail) <> "S" Then

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

  Public Overridable Function Ripristina() As Boolean
    Try
      oCleGsor.bInUnload = True

      'annullo la riga in corso di inserimento
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
          oCleGsor.RecordRipristina(dcGsorRighe.Position, dcGsorRighe.Filter)
        End If
      End If

      oCleGsor.RipristinaDocumento()
      GctlSaveConfigGrid()
      SetStato(0, False)
      oCleGsor.bInUnload = False
      edNumDoc.Text = "0"
      edNumDoc.SelectAll()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oCleGsor.bInUnload = False
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
      If edNumDoc.Enabled Then Return True
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      Me.Cursor = Cursors.WaitCursor

      If oCleGsor.bSalvaAncheSeNonModificato And oCleGsor.bNew = False Then oCleGsor.bHasChangesET = True
      If oCleGsor.RecordIsChanged Then
        '--------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False
      End If
      If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return False
      Me.Cursor = Cursors.Default
      If oCleGsor.bNew Then strState = "N"

      If oCleGsor.bSalvaAncheSeNonModificato And oCleGsor.bNew = False Then oCleGsor.bHasChangesET = True
      If oCleGsor.RecordIsChanged Then

        If bAllowCancel Then
          dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128776135234280000, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
          If dRes = System.Windows.Forms.DialogResult.No Then Return True
        Else
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791958202656250, "Salvo il documento?"))
          If dRes = System.Windows.Forms.DialogResult.No Then Return False
        End If

        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not RicalcoloPromozioni() Then Return False

          Me.Cursor = Cursors.WaitCursor
          If oCleGsor.bModTCO And oCleGsor.bAttivaRecentTCO And oCleGsor.bNew Then
            oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "AnnoTCO", edEt_annotco.Text, " ", "NS.", "...", "...")
            oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "CodstagTCO", edEt_codstag.Text, " ", "NS.", "...", "...")
          End If

          '-----------------------------
          'Se è attivo il modulo del Customer Service ed esiste il riferimento alla chiamata...
          If oCleGsor.bModuloAS And NTSCInt(lbEt_codchia.Text) <> 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 127791222092187500, _
                        "Qualora siano state modificate quantità e/o prezzi di riga" & vbCrLf & _
                        "ricordarsi di modificare opportunamente la parte 'Ricambi'" & vbCrLf & _
                        "della Chiamata n.|'" & lbEt_codchia.Text & "'|."))
          End If

          If Not oCleGsor.RecordSalva(dcGsorRighe.Position, False, Nothing) Then Return False
          oCleGsor.dttEC.AcceptChanges()
          If Not oCleGsor.SalvaOrdine(strState) Then
            'potrebbe non aver salvato perchè il numero doc è già stato utilizzato ...
            edNumDoc.Text = NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_numdoc).ToString
            Return False
          End If
          GctlSaveConfigGrid()

          '-----------------------------
          'salvo le condizioni particolari LISTINI/SCONTI/PROVV
          If oCleGsor.bSalvaCondizioniFinali Then
            'Funzione non abilitata per Ordine di produzione interni
            If Not (dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" And oCleGsor.bTerzista = False) Then
              If Not SalvaCondizioniFinali() Then Return False
            Else
              If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 128673202256562500, "Attenzione!" & vbCrLf & vbCrLf & _
                                                "La funzionalità di salvataggio delle condizioni particolari è " & _
                                                "abilitata solo sui carichi di produzioni da terzisti e non interni."))
              End If
            End If
          End If

          '----------------------------------------
          'Controlla se deve generare automaticamente note di prelievo
          If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" Or dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "#" Then
            If oCleGsor.dttEC.Select("xxo_qtadaass > 0").Length > 0 Then
              'Crea la nota di prelievo
              If Not CreaNota(nStampa) Then
                oApp.MsgBoxErr(oApp.Tr(Me, 128534304950636216, "Si è verificato un errore nella generazione automatica della Nota di prelievo dall'Impegno cliente. Riaprire l'Impegno cliente, inserire i dati eventualmente mancanti, riassegnare le quantità quindi salvare l'Impegno."))
              End If
            End If
          End If    'If strTipork = "R" Or strTipork = "#" Then
        End If
      Else
        'ho aperto un documento ed ora lo stampo senza modficarlo
        If bRiapridoc = False Then
          oCleGsor.DocumentLockRemove()
          GctlSaveConfigGrid()
        End If
      End If

      If bRiapridoc Then
        'non chiudo ma mi posiziono sul corpo sull'ultima riga
        SetStato(1, False)
        GctlSetVisEnab(tlbNuovoOrdineDaPrec, False)
        NTSFormClearDataBinding(Me)
        If oCleGsor.ApriOrdine(DittaCorrente, False, cbTipoDoc.SelectedValue.ToString, _
                              NTSCInt(edAnnoDoc.Text), edSerieDoc.Text, NTSCInt(edNumDoc.Text), _
                              dsGsor) = False Then Return False

        dcGsor.DataSource = dsGsor.Tables("TESTA")
        dcGsor.MoveFirst()

        dcGsorRighe.DataSource = dsGsor.Tables("CORPO")
        grRighe.DataSource = dcGsorRighe

        Bindcontrols()
        NumeroPost()
        dsGsor.AcceptChanges()

        oCleGsor.bNew = False
        tsGsor.SelectedTabPageIndex = 1
        grRighe.Focus()
        grvRighe.MoveLast()
      End If    'If bRiapridoc Then

      If strState = "N" Then
        'salvo gli estremi dell'ultimo documento fatto. mi serviranno nel caso du 'nuovo da altro' per proporre anno e serie del nuovo documento
        oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "LastDocNewAnno", edAnnoDoc.Text, " ", "NS.", "...", "...")
        'oMenu.SaveSettingBus("BNORGSOR", "Recent", ".", "LastDocNewSerie", edSerieDoc.Text, " ", "NS.", "...", "...")
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

  Public Overridable Function CreaNota(ByVal nStampa As Integer) As Boolean
    Dim oCleGnno As CLEORGNNO = Nothing
    'nStampa = -1: salva solo
    '           0: stampa video
    '           1: stampa carta
    Dim lIIMotrans As Integer = 0
    Try
      dttGnno = New DataTable
      'ottengo i dati
      lIIMotrans = oMenu.GetTblInstId("MOTRANS", False)
      If Not oCleGsor.ScriviMotransPerCreaNota(lIIMotrans) Then Return False

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOR", "BEORGNNO", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128697675491029481, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleGnno = CType(oTmp, CLEORGNNO)
      '------------------------------------------------
      AddHandler oCleGnno.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleGnno.Init(oApp, oScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

      'carico i parametri
      dttGnno.Columns.Add("conto", GetType(Integer))
      dttGnno.Columns.Add("tipork", GetType(String))
      dttGnno.Columns.Add("anno", GetType(String))
      dttGnno.Columns.Add("serie", GetType(String))
      dttGnno.Columns.Add("numord", GetType(Integer))
      dttGnno.Columns.Add("datord", GetType(Date))
      dttGnno.Columns.Add("riferim", GetType(String))
      dttGnno.Columns.Add("tipobf", GetType(Integer))
      dttGnno.Columns.Add("datcons", GetType(Date))
      dttGnno.Columns.Add("codpaga", GetType(Integer))
      dttGnno.Columns.Add("datapag", GetType(Date))
      dttGnno.Columns.Add("codagen", GetType(Integer))
      dttGnno.Columns.Add("listino", GetType(Integer))
      dttGnno.Columns.Add("controp", GetType(Integer))
      dttGnno.Columns.Add("scont1", GetType(Decimal))
      dttGnno.Columns.Add("scont2", GetType(Decimal))
      dttGnno.Columns.Add("scopag", GetType(Decimal))
      dttGnno.Columns.Add("magaz", GetType(Integer))
      dttGnno.Columns.Add("magaz2", GetType(Integer))
      dttGnno.Columns.Add("bolli", GetType(Decimal))
      dttGnno.Columns.Add("speinc", GetType(Decimal))
      dttGnno.Columns.Add("speacc", GetType(Decimal))
      dttGnno.Columns.Add("speaccv", GetType(Decimal))
      dttGnno.Columns.Add("valuta", GetType(Integer))
      dttGnno.Columns.Add("cambio", GetType(Decimal))
      dttGnno.Columns.Add("scorpo", GetType(String))
      dttGnno.Columns.Add("acuradi", GetType(String))
      dttGnno.Columns.Add("vettor", GetType(Integer))
      dttGnno.Columns.Add("caustra", GetType(Integer))
      dttGnno.Columns.Add("note", GetType(String))
      dttGnno.Columns.Add("porto", GetType(String))
      dttGnno.Columns.Add("coddest", GetType(Integer))
      dttGnno.Columns.Add("coddest2", GetType(Integer))
      dttGnno.Columns.Add("codese", GetType(Integer))
      dttGnno.Columns.Add("codagen2", GetType(Integer))
      dttGnno.Columns.Add("codaspe", GetType(Integer))
      dttGnno.Columns.Add("aspetto", GetType(String))
      dttGnno.Columns.Add("flspinc", GetType(String))
      dttGnno.Columns.Add("flboll", GetType(String))
      dttGnno.Columns.Add("pagato", GetType(Decimal))
      dttGnno.Columns.Add("abbuono", GetType(Decimal))
      dttGnno.Columns.Add("commeca", GetType(Integer))
      dttGnno.Columns.Add("subcommeca", GetType(String))
      dttGnno.Columns.Add("codcena", GetType(Integer))
      dttGnno.Columns.Add("coddivi", GetType(Integer))
      dttGnno.Columns.Add("codcli", GetType(Integer))
      dttGnno.Columns.Add("abi", GetType(Integer))
      dttGnno.Columns.Add("cab", GetType(Integer))
      dttGnno.Columns.Add("banc1", GetType(String))
      dttGnno.Columns.Add("banc2", GetType(String))
      dttGnno.Columns.Add("stampala", GetType(Boolean))
      dttGnno.Columns.Add("peveivainc", GetType(Integer))
      dttGnno.Columns.Add("tipostampa", GetType(Integer))
      dttGnno.Columns.Add("bolliv", GetType(Decimal))
      dttGnno.Columns.Add("speincv", GetType(Decimal))
      dttGnno.Columns.Add("pagatov", GetType(Decimal))
      dttGnno.Columns.Add("abbuonov", GetType(Decimal))
      dttGnno.Columns.Add("totomagv", GetType(Decimal))
      dttGnno.Columns.Add("speimb", GetType(Decimal))
      dttGnno.Columns.Add("speimbv", GetType(Decimal))
      dttGnno.Columns.Add("annotco", GetType(Decimal))
      dttGnno.Columns.Add("codstag", GetType(String))
      dttGnno.Columns.Add("liimotrans", GetType(String))
      dttGnno.Columns.Add("nota_tipork", GetType(String))
      dttGnno.Columns.Add("nota_anno", GetType(String))
      dttGnno.Columns.Add("nota_serie", GetType(String))
      dttGnno.Columns.Add("nota_numdoc", GetType(Integer))
      dttGnno.Columns.Add("cup", GetType(String))
      dttGnno.Columns.Add("cig", GetType(String))
      dttGnno.Columns.Add("riferimpa", GetType(String))

      dttGnno.Clear()
      dttGnno.AcceptChanges()

      dttGnno.Rows.Add(dttGnno.NewRow)
      With dttGnno.Rows(0)
        !conto = NTSCInt(edEt_conto.Text)
        !tipork = "R"
        !anno = edAnnoDoc.Text
        !serie = edSerieDoc.Text
        !numord = NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_numdoc)
        !datord = NTSCDate(edet_datdoc.Text)
        !riferim = edEt_riferim.Text
        !tipobf = NTSCInt(edEt_tipobf.Text)
        !datcons = NTSCDate(edEt_datcons.Text)
        !codpaga = NTSCInt(edEt_codpaga.Text)
        !datapag = IIf(IsDate(edEt_datapag.Text), NTSCDate(edEt_datapag).ToShortDateString, "").ToString
        !codagen = NTSCInt(edEt_codagen.Text)
        !listino = NTSCInt(edEt_listino.Text)
        !controp = NTSCInt(edEt_controp.Text)
        !scont1 = NTSCDec(edEt_scont1.Text)
        !scont2 = NTSCDec(edEt_scont2.Text)
        !scopag = NTSCDec(edEt_scopag.Text)
        !magaz = NTSCInt(edEt_magaz.Text)
        !magaz2 = NTSCInt(edEt_magaz2.Text)
        !bolli = NTSCDec(edEt_bolli.Text)
        !speinc = NTSCDec(edEt_speinc.Text)
        !speacc = NTSCDec(edEt_speacc.Text)
        !speaccv = NTSCDec(edEt_speaccv.Text)
        !valuta = NTSCInt(edEt_valuta.Text)
        !cambio = NTSCDec(edEt_cambio.Text)
        !scorpo = IIf(ckEt_scorpo.Checked, "S", "N")
        !acuradi = cbEt_acuradi.SelectedValue.ToString
        !vettor = NTSCInt(edEt_vettor.Text)
        !caustra = NTSCInt(edEt_caustra.Text)
        !note = NTSCStr(edEt_note.Text)
        !porto = NTSCStr(edEt_porto.Text)
        !coddest = NTSCInt(edEt_coddest.Text)
        !coddest2 = NTSCInt(edEt_coddest2.Text)
        !codese = NTSCInt(edEt_codese.Text)
        !codagen2 = NTSCInt(edEt_codagen2.Text)
        !codaspe = NTSCInt(edEt_codaspe.Text)
        !aspetto = edEt_aspetto.Text
        !flspinc = IIf(ckEt_flspinc.Checked, "S", "N").ToString
        !flboll = IIf(ckEt_flboll.Checked, "S", "N").ToString
        !pagato = NTSCDec(edEt_pagato.Text)
        !abbuono = NTSCDec(edEt_abbuono.Text)
        !commeca = NTSCInt(edEt_commeca.Text)
        !subcommeca = edEt_subcommeca.Text
        !codcena = NTSCInt(edEt_codcena.Text)
        !coddivi = NTSCInt(edEt_coddivi.Text)
        !codcli = NTSCInt(edEt_codcli.Text)
        !abi = NTSCInt(edEt_abi.Text)
        !cab = NTSCInt(edEt_cab.Text)
        !banc1 = edEt_banc1.Text
        !banc2 = edEt_banc2.Text
        !stampala = CBool(IIf(nStampa = -1, False, True))
        !peveivainc = oCleGsor.nPeveIvaInc
        !tipostampa = nStampa
        !bolliv = NTSCDec(edEt_bolliv.Text)
        !speincv = NTSCDec(edEt_speincv.Text)
        !pagatov = NTSCDec(edEt_pagatov.Text)
        !abbuonov = NTSCDec(edEt_abbuonov.Text)
        !totomagv = NTSCDec(edEt_totomagv.Text)
        !speimb = NTSCDec(edEt_speimb.Text)
        !speimbv = NTSCDec(edEt_speimbv.Text)
        !annotco = NTSCDec(edEt_annotco.Text)
        !codstag = NTSCDec(edEt_codstag.Text)
        !liimotrans = lIIMotrans.ToString
        !cup = NTSCStr(edEt_cup.Text)
        !cig = NTSCStr(edEt_cig.Text)
        !riferimpa = NTSCStr(edEt_riferimpa.Text)
      End With

      oCleGnno.CreaNotaDaImpCli(dttGnno)

      If CBool(dttGnno.Rows(0)!stampala) Then
        StampaNotaGnno(nStampa)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oMenu.ResetTblInstId("MOTRANS", False, lIIMotrans)
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    'nDestin: 0 = video, 1 = carta
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer

    Dim strKey2 As String = ""
    Dim strReportName As String = "Bsorgsor.rpt"

    Try
      '--------------------------------------------------
      'Non si possono stampare gli impegni di produzione
      If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "Y" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222114843750, "Si possono stampare solo documenti :" & vbCrLf & " - Ordini di produzione;" & vbCrLf & " - Ordini fornitori;" & vbCrLf & " - Preventivi;" & vbCrLf & " - Impegni clienti;" & vbCrLf & " - Impegni di trasferimento;"))
        Return
      End If

      If dsGsor.Tables("TESTA").Rows(0)!et_valuta.ToString <> "0" Then
        strKey2 = "Reports3"
      ElseIf dsGsor.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
        strKey2 = "Reports2"
      Else
        strKey2 = "Reports1"
      End If

      '--------------------------------------------------
      'eseguo delle query libere prima della stampa (query memorizzate in regprop di arcproc)
      oCleGsor.RunQueryBeforePrint("BSORGSOR")

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{MOVORD.codditt} = '" & DittaCorrente & "'" & _
                " And {MOVORD.mo_anno} = " & dsGsor.Tables("TESTA").Rows(0)!et_anno.ToString & _
                " AND {MOVORD.mo_numord} = " & dsGsor.Tables("TESTA").Rows(0)!et_numdoc.ToString & _
                " AND {MOVORD.mo_serie} = '" & dsGsor.Tables("TESTA").Rows(0)!et_serie.ToString & "'" & _
                " AND {MOVORD.mo_tipork} = '" & dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString & "'" & _
                " AND {MOVORD.mo_stasino} <> 'N'" & _
                " AND {TESTORD.td_magaz2} <> {KEYORD.ko_magaz}"
      nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "Bsorgsor", strKey2, dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                                      0, nDestin, strReportName, False, "Stampa Ordine", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in bsveboll, bsorgsor e pochi altri programmi
        For j = 3 To 12
          If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
          End If
        Next j
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      '--------------------------------------------------
      'aggiorno il flag di 'Stampato' su testord
      oCleGsor.AggiornaFlagStampato()

      If tlbStampaEtichetteFinale.Checked Then
        ApriEtichette(1)
      End If

      'stampo la scheda di trasporto se presente
      If oCleGsor.dttSCHETRASP.Rows.Count > 0 Then
        strKey2 = "Reports4"
        strReportName = "BSVESCTR.RPT"
        strCrpe = "{SCHETRASP.codditt} = '" & DittaCorrente & "'" & _
          " And {SCHETRASP.sct_anno} = " & dsGsor.Tables("TESTA").Rows(0)!et_anno.ToString & _
          " AND {SCHETRASP.sct_numdoc} = " & dsGsor.Tables("TESTA").Rows(0)!et_numdoc.ToString & _
          " AND {SCHETRASP.sct_serie} = '" & dsGsor.Tables("TESTA").Rows(0)!et_serie.ToString & "'" & _
          " AND {SCHETRASP.sct_tipork} = '" & dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString & "'"

        nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSORGSOR", strKey2, dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString, _
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
  Public Overridable Sub StampaNotaGnno(ByVal nDestin As Integer)
    'nDestin: 0 = video, 1 = carta
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer

    Dim strKey2 As String = ""
    Dim strReportName As String = ""

    Try
      If dttGnno.Rows(0)!valuta.ToString <> "0" Then
        strKey2 = "Reports3"
      ElseIf dttGnno.Rows(0)!scorpo.ToString = "S" Then
        strKey2 = "Reports2"
      Else
        strKey2 = "Reports1"
      End If
      If dttGnno.Rows(0)!valuta.ToString <> "0" Then
        strReportName = "BSVEPRBV.RPT"
      ElseIf dttGnno.Rows(0)!scorpo.ToString = "S" Then
        strReportName = "BSVEPRBC.RPT"
      Else
        strReportName = "BSVEPRBN.RPT"
      End If

      '--------------------------------------------------
      'eseguo delle query libere prima della stampa (query memorizzate in regprop di arcproc)
      oCleGsor.RunQueryBeforePrint("BSVEBOLL")

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{MOVMAG.codditt} = '" & DittaCorrente & "'" & _
                " And {MOVMAG.mm_anno} = " & dttGnno.Rows(0)!nota_anno.ToString & _
                " AND {MOVMAG.mm_numdoc} = " & dttGnno.Rows(0)!nota_numdoc.ToString & _
                " AND {MOVMAG.mm_serie} = '" & dttGnno.Rows(0)!nota_serie.ToString & "'" & _
                " AND {MOVMAG.mm_tipork} = '" & dttGnno.Rows(0)!nota_tipork.ToString & "'" & _
                " AND {MOVMAG.mm_stasino} <> 'N'"
      nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "Bsveboll", strKey2, dttGnno.Rows(0)!nota_tipork.ToString, _
                                      0, nDestin, strReportName, False, "Stampa Documento", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in bsveboll, bsorgsor e pochi altri programmi
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
      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And Not oCleGsor.bTerzista Then
        'oApp.MsgBoxErr(oApp.Tr(Me, 128576533131562500, "Funzione abilitata solo su Ordini di produzione a terzisti e non interni."))
        Return True
      End If

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOR", "BEVECOPA", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134148246000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleCopa = CType(oTmp, CLEVECOPA)
      '------------------------------------------------
      AddHandler oCleCopa.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleCopa.Init(oApp, oScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128673206501093750, _
                  "Procedere con il salvataggio delle condizioni particolari" & vbCrLf & _
                  "(prezzi, sconti, provvigioni)" & vbCrLf & _
                  "su tutte le righe del documento?")) = Windows.Forms.DialogResult.No Then Return True

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

      For i = 0 To oCleGsor.dttEC.Rows.Count - 1
        If NTSCStr(oCleGsor.dttEC.Rows(i)!ec_umprz).Trim = "N" Then

          dttCopa.Clear()
          dttCopa.AcceptChanges()

          'Rileva il codice lavorazione terzista
          If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" Then
            nCodlavo = 0
            dtrT = dsGsor.Tables("ATTIVIT").Select("at_riga = " & oCleGsor.dttEC.Rows(i)!ec_riga.ToString, "at_riga, at_fase")
            If dtrT.Length > 0 Then nCodlavo = NTSCInt(dtrT(0)!at_codlavo)
          End If

          If ckEt_scorpo.Checked Then
            dPrezzo = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_preziva)
          Else
            dPrezzo = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_prezzo)
          End If

          dttCopa.Rows.Add(dttCopa.NewRow)
          With dttCopa.Rows(0)
            !conto = NTSCInt(edEt_conto.Text)
            !coddest = NTSCInt(edEt_coddest.Text)
            !valuta = NTSCInt(edEt_valuta.Text)
            !agente = NTSCInt(edEt_codagen.Text)
            !agente2 = NTSCInt(edEt_codagen2.Text)
            !datdoc = NTSCDate(edet_datdoc.Text).ToShortDateString
            !codart = NTSCStr(oCleGsor.dttEC.Rows(i)!ec_codart)
            !fase = NTSCInt(oCleGsor.dttEC.Rows(i)!ec_fase)
            !quant = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_quant)
            !prezzo = dPrezzo
            !prezvalc = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_prezvalc)
            !prznet = NTSCStr(oCleGsor.dttEC.Rows(i)!ec_flprznet)
            !perqta = NTSCInt(oCleGsor.dttEC.Rows(i)!ec_perqta)
            !ump = NTSCStr(oCleGsor.dttEC.Rows(i)!ec_ump)
            !scont1 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont1)
            !scont2 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont2)
            !scont3 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont3)
            !scont4 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont4)
            !scont5 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont5)
            !scont6 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_scont6)
            !provv = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_provv)
            !provv2 = NTSCDec(oCleGsor.dttEC.Rows(i)!ec_provv2)
            !codlavo = nCodlavo
            !codtpro = NTSCInt(oCleGsor.dttEC.Rows(i)!ec_codtpro)
          End With
          dttCopa.AcceptChanges()

          'oPar.Ditta = DittaCorrente
          'oPar.strNomProg = "BNORGSOR"
          'oPar.ctlPar1 = dttCopa
          'oPar.bPar1 = True             'non visualizza la form di FRMVECOPA
          'oMenu.RunChild("NTSInformatica", "FRMVECOPA", "", DittaCorrente, "", "BNVECOPA", oPar, "", True, True)

          oCleCopa.InitEx(dttCopa)
          oCleCopa.nNoMsgConfermaAggCond = 1  'aggiorna senza chiedere conferma ad ogni articolo
          oCleCopa.Elabora()
        End If    ' If NTSCStr(oCleGsor.dttEC.Rows(i)!ec_umprz).Trim = "N" Then
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
              dtrTmp = dsGsor.Tables("CORPOTC").Select("codditt = " & CStrSQL(DittaCorrente) & _
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
              If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_quant) > 0 Then
                strQuant = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_quant).ToString.PadLeft(9)
                strQuant = Microsoft.VisualBasic.Right(strQuant, 9)
                strParam = "ART;" & strCodart & ";" & strQuant & ";"
                oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128776135292156000, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
              End If
            End If
          End If
        End If
      Else
        'Alla fine del documento
        strNumero = NTSCInt(oCleGsor.dttET.Rows(0)!et_numdoc).ToString.PadLeft(9)
        strNumero = Microsoft.VisualBasic.Right(strNumero, 9)
        strParam = "ORD;" & oCleGsor.dttET.Rows(0)!et_tipork.ToString & ";" & NTSCInt(oCleGsor.dttET.Rows(0)!et_anno).ToString & ";" & oCleGsor.dttET.Rows(0)!et_serie.ToString & ";" & strNumero & ";        0;"
        oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128553214063728000, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
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
        oApp.MsgBoxErr(oApp.Tr(Me, 128680972342812500, "Posizionarsi prima su una riga di griglia in una colonna prezzo"))
        Return False
      End If

      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128680973032031250, "Inserire prima il codice articolo"))
        Return False
      End If

      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048480725404383, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return False
      End If

      If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And (Not oCleGsor.bTerzista) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776135985532000, "Funzione abilitata solo su Ordini di produzione a terzisti e non interni."))
        Return False
      End If

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNORGSOR"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.strPar2 = edet_datdoc.Text
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar3 = NTSCInt(edEt_conto.Text)
      oPar.dPar4 = 0        'ritorna il prezzo
      oPar.dPar5 = 0        'ritorna la valuta
      oPar.ctlPar1 = Nothing

      oMenu.RunChild("NTSInformatica", "FRMMGLIST", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.dPar4 = 0 Then Return False

      '----------------------
      'Riporta i prezzi praticati in precedenza
      If NTSCInt(edEt_valuta.Text) <> NTSCInt(oPar.dPar5) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129048480855563965, "Non è possibile riportare il prezzo in quanto il prezzo selezionato riporta una valuta (|" & oPar.dPar5.ToString & "|) diversa da quella del documento corrente (|" & edEt_valuta.Text & "|)."))
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

  '------------------------------

  Public Overridable Function GestisciGrigliaTCO() As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      If oCleGsor.bInUnload = True Then Return True
      If oCleGsor.bModTCO = False Then
        grTco.Visible = False
        Return True
      End If

      '----------------------------
      'visualizzo o meno la griglia TCO ed imposto la caption delle colonne
      If grvRighe.FocusedRowHandle < 0 Then
        grTco.Visible = False
      ElseIf grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grTco.Visible = False
        'ElseIf grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
        '  grTco.Visible = False
      ElseIf NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) = 0 Then
        grTco.Visible = False
      Else
        GctlSetVisEnab(grTco, True)
        '-------------------------
        'carico l'intestazione delle colonne e nascondo quelle che non servono
        oMenu.ValCodiceDb(NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl).ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          For i = 0 To grvTco.Columns.Count - 1
            l = NTSCInt(grvTco.Columns(i).Name.Substring(grvTco.Columns(i).Name.Length - 2))
            If l <> 0 Then
              grvTco.Columns(i).Caption = NTSCStr(dttTmp.Rows(0)("tb_dest" & l.ToString.PadLeft(2, "0"c)))
              If NTSCStr(grvTco.Columns(i).Caption).Trim = "" Then
                grvTco.Columns(i).Visible = False
              Else
                GctlSetVisEnab(grvTco.Columns(i), True)
              End If
            End If
          Next
        End If
        dttTmp.Clear()

        '-------------------------
        'collego i dati: se non presente creo la tab temporanea per la visualizzaz delle qta
        If dsGrvTCO.Tables.Count = 0 Then
          dsGrvTCO.Tables.Add(dsGsor.Tables("CORPOTC").Clone())
          dsGrvTCO.Tables(0).TableName = "TEMPTCO"
          dcGsorTCO.DataSource = dsGrvTCO.Tables("TEMPTCO")
          grTco.DataSource = dcGsorTCO
        End If

        dsGrvTCO.Tables("TEMPTCO").Clear()
        dtrT = dsGsor.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString)
        If dtrT.Length > 0 Then
          'prima riga: quantità ordinata
          dsGrvTCO.Tables("TEMPTCO").ImportRow(dtrT(0))
          'seconda riga: quantità evasa
          dsGrvTCO.Tables("TEMPTCO").ImportRow(dtrT(0))
          For i = 1 To 24
            dsGrvTCO.Tables("TEMPTCO").Rows(1)("ec_quant" & i.ToString("00")) = NTSCDec(dsGrvTCO.Tables("TEMPTCO").Rows(1)("ec_quaeva" & i.ToString("00")))
          Next
        End If
        dsGrvTCO.Tables("TEMPTCO").AcceptChanges()
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
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
      oCleGsor.Leggidisponibilita(strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, dOrdinato, dImpegnato, 0, strGescomm)
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

  Public Overrides Function ResolveField(ByVal strIn As String) As String
    Select Case strIn.Substring(0, 1)
      Case "#"
        'Variabile esposte SPECIFICHE PER PROGRAMMA
        Select Case UCase$(Mid$(strIn, 2))
          Case "TIPORK"
            Return dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString
          Case "ANNO"
            Return dsGsor.Tables("TESTA").Rows(0)!et_anno.ToString
          Case "SERIE"
            Return dsGsor.Tables("TESTA").Rows(0)!et_serie.ToString
          Case "NUMDOC"
            Return dsGsor.Tables("TESTA").Rows(0)!et_numdoc.ToString
          Case Else
            Return "{Variabile '" & Mid$(strIn, 2) & "' sconosciuta}"
        End Select
      Case Else
        Return MyBase.ResolveField(strIn)
    End Select
    Return ""
  End Function

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
      If ckEt_scorpo.Checked = False And dsGsor.Tables("CORPO").Rows.Count = 0 Then
        If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And oCleGsor.bModPM Then
          'no docu in valuta 
        Else
          GctlSetVisEnab(edEt_valuta, False)
        End If
      End If
      If dsGsor.Tables("CORPO").Rows.Count = 0 Then
        GctlSetVisEnab(ckEt_scorpo, False)
      End If
    End If
  End Sub

  Public Overridable Sub CalcolaSceffAppl(ByVal nRiga As Integer)
    Dim dPrezzo As Decimal = 0
    Dim dPrznet As Decimal = 0
    Dim dPrzl As Decimal = 0
    Dim dtrMo() As DataRow
    Try
      dtrMo = dsGsor.Tables("CORPO").Select("ec_riga = " & nRiga.ToString)
      If dtrMo.Length = 0 Then Return

      If dsGsor.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" And oCleGsor.bPrelistIvato Then
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

  Public Overridable Function LeggiPromozioni(ByVal strCodart As String, ByVal strDatdoc As String) As Integer
    Dim oPar As CLE__CLDP
    Try
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNORGSOR"
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


  Public Overridable Function RigaConArticoloOk(ByVal bSuCodart As Boolean) As Boolean
    Try
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134541124000, "Posizionarsi prima su una riga di griglia contenente il 'codice articolo'"))
        Return False
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134554228000, "Posizionarsi prima su una riga di griglia contenente il 'codice articolo'"))
        Return False
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128689606437812500, "Posizionarsi prima su una riga di griglia con 'codice articolo' impostato"))
        Return False
      End If
      If grvRighe.FocusedColumn.Name <> "ec_codart" And bSuCodart Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776134799954000, "Posizionarsi prima sulla colonna 'codice articolo'"))
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub tlbDaLista_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaLista.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.dPar1 = NTSCInt(dsGsor.Tables("TESTA").Rows(0)!et_magaz)
      oPar.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGSELI", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If oPar.dPar2 <> 0 Then
        oCleGsor.ImportaDaLista(NTSCInt(oPar.dPar2), oPar.bPar1)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub cmdDatiTesta1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiTesta1.Click
    Try
      cmdDatiTesta1.Font = New Font(cmdDatiTesta1.Font, FontStyle.Bold)
      If cmdDatiTesta2.Font.Bold Then cmdDatiTesta2.Font = New Font(cmdDatiTesta2.Font, FontStyle.Regular)
      If cmdDatiTesta3.Font.Bold Then cmdDatiTesta3.Font = New Font(cmdDatiTesta3.Font, FontStyle.Regular)
      If cmdDatiTesta4.Font.Bold Then cmdDatiTesta4.Font = New Font(cmdDatiTesta4.Font, FontStyle.Regular)

      pnDatiTesta2.Visible = False
      pnDatiTesta3.Visible = False
      pnDatiTesta4.Visible = False
      pnDatiTesta1.Visible = True

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
      If cmdDatiTesta3.Font.Bold Then cmdDatiTesta3.Font = New Font(cmdDatiTesta3.Font, FontStyle.Regular)
      If cmdDatiTesta4.Font.Bold Then cmdDatiTesta4.Font = New Font(cmdDatiTesta4.Font, FontStyle.Regular)

      pnDatiTesta1.Visible = False
      pnDatiTesta3.Visible = False
      pnDatiTesta4.Visible = False
      pnDatiTesta2.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDatiTesta3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiTesta3.Click
    Try
      If cmdDatiTesta1.Font.Bold Then cmdDatiTesta1.Font = New Font(cmdDatiTesta1.Font, FontStyle.Regular)
      If cmdDatiTesta2.Font.Bold Then cmdDatiTesta2.Font = New Font(cmdDatiTesta2.Font, FontStyle.Regular)
      cmdDatiTesta3.Font = New Font(cmdDatiTesta3.Font, FontStyle.Bold)
      If cmdDatiTesta4.Font.Bold Then cmdDatiTesta4.Font = New Font(cmdDatiTesta4.Font, FontStyle.Regular)

      pnDatiTesta1.Visible = False
      pnDatiTesta2.Visible = False
      pnDatiTesta4.Visible = False
      pnDatiTesta3.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDatiTesta4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiTesta4.Click
    Try
      If cmdDatiTesta1.Font.Bold Then cmdDatiTesta1.Font = New Font(cmdDatiTesta1.Font, FontStyle.Regular)
      If cmdDatiTesta2.Font.Bold Then cmdDatiTesta2.Font = New Font(cmdDatiTesta2.Font, FontStyle.Regular)
      If cmdDatiTesta3.Font.Bold Then cmdDatiTesta3.Font = New Font(cmdDatiTesta3.Font, FontStyle.Regular)
      cmdDatiTesta4.Font = New Font(cmdDatiTesta4.Font, FontStyle.Bold)

      pnDatiTesta1.Visible = False
      pnDatiTesta2.Visible = False
      pnDatiTesta3.Visible = False
      pnDatiTesta4.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSitcli_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSitcli.Click
    Dim strParam As String = ""
    Try
      If NTSCInt(edEt_conto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130293538887050790, "Impostare prima il cliente/fornitore"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      strParam = "SITUAZ:" & edEt_conto.Text
      oMenu.RunChild("NTSInformatica", "FRMDKKONS", "", DittaCorrente, "", "BNDKKONS", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdPiedeTotali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPiedeTotali.Click
    Try
      cmdPiedeTotali.Font = New Font(cmdPiedeTotali.Font, FontStyle.Bold)
      If cmdPiedeTrasporto.Font.Bold Then cmdPiedeTrasporto.Font = New Font(cmdPiedeTrasporto.Font, FontStyle.Regular)
      If cmdPiedeAltro.Font.Bold Then cmdPiedeAltro.Font = New Font(cmdPiedeAltro.Font, FontStyle.Regular)

      pnPiedeTrasporto.Visible = False
      pnPiedeAltro.Visible = False
      pnPiedeTotali.Visible = True

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
      If cmdPiedeAltro.Font.Bold Then cmdPiedeAltro.Font = New Font(cmdPiedeAltro.Font, FontStyle.Regular)

      pnPiedeTotali.Visible = False
      pnPiedeAltro.Visible = False
      pnPiedeTrasporto.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdPiedeAltro2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPiedeAltro.Click, cmdPiedeAltro.Click
    Try
      If cmdPiedeTotali.Font.Bold Then cmdPiedeTotali.Font = New Font(cmdPiedeTotali.Font, FontStyle.Regular)
      If cmdPiedeTrasporto.Font.Bold Then cmdPiedeTrasporto.Font = New Font(cmdPiedeTrasporto.Font, FontStyle.Regular)
      cmdPiedeAltro.Font = New Font(cmdPiedeAltro.Font, FontStyle.Bold)

      pnPiedeTotali.Visible = False
      pnPiedeTrasporto.Visible = False
      pnPiedeAltro.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub NumeroPost()
    Try
      ceColl.Visible = False 'Sempre invisibile, se è un utente social ci penserà lui ad apparire completato l'aggiornamento dei dati

      If oCleGsor.bNew Then Return

      If oApp.User.SocialUser = "I" Then
        Dim oPar As New CLE__CLDP
        Dim dttDati As New DataTable
        dttDati.Columns.Add("codditt")
        dttDati.Columns.Add("spd_tipo")
        dttDati.Columns.Add("spd_tipork")
        dttDati.Columns.Add("spd_anno")
        dttDati.Columns.Add("spd_serie")
        dttDati.Columns.Add("spd_numdoc")
        oPar.bPar1 = True
        With dsGsor.Tables("TESTA").Rows(dcGsor.Position)
          dttDati.Rows.Add(New Object() {DittaCorrente, "O", NTSCStr(!et_tipork), NTSCStr(!et_anno), NTSCStr(!et_serie), NTSCInt(!et_numdoc)})
        End With
        oPar.ctlPar1 = dttDati

        ceColl.NTSSetParam(oMenu, "", "BNORGSOR", oPar)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
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
        oCleGsor.CalcolaTotali()
      End If
      If oCleGsor.bConai Then
        For i = 0 To dsGsor.Tables("CORPO").Rows.Count - 1
          With dsGsor.Tables("CORPO").Rows(i)
            If Microsoft.VisualBasic.Left(NTSCStr(!ec_codart), 5).ToUpper <> "CONAI" Then
              dColli = dColli + NTSCDec(!ec_colli)
              dQuat = dQuat + NTSCDec(!ec_quant)
              If oCleGsor.strCalcPesi = "S" Then
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
      oPar.strNomProg = "BSORGSOR"
      oPar.ctlPar1 = dsGsor.Tables("SCHETRASP")
      oPar.ctlPar2 = dsGsor.Tables("TESTA")
      oPar.dPar1 = dColli
      oPar.dPar2 = dQuat
      oPar.dPar3 = dPeso
      oPar.bPar1 = bArtConai

      oMenu.RunChild("NTSInformatica", "FRMMGSCTR", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If CType(oPar.ctlPar1, DataTable).Rows.Count > 0 Then
        oCleGsor.dsShared.Tables("SCHETRASP").Clear()
        If CType(oPar.ctlPar1, DataTable).Rows.Count > 0 Then
          oCleGsor.dsShared.Tables("SCHETRASP").ImportRow(CType(oPar.ctlPar1, DataTable).Rows(0))
        End If
        oCleGsor.bHasChangesET = True
      Else
        oCleGsor.dsShared.Tables("SCHETRASP").Clear()
        oCleGsor.bHasChangesET = True
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub ColoraCampoAbiCab()
    Dim dttTmp As New DataTable
    Try
      Dim lAbi As Integer = NTSCInt(oCleGsor.dttET.Rows(dcGsor.Position)!et_abi)
      Dim lCab As Integer = NTSCInt(oCleGsor.dttET.Rows(dcGsor.Position)!et_cab)

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

#Region "Panello Promozioni"
  Public Overridable Sub cmdCaricaContratti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCaricaContratti.Click
    Try
      oCleGsor.CaricaContrattiCliente()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub CaricaGrigliaPromozioni()
    Try
      dcPromozioni = New BindingSource
      dcPromozioni.DataSource = oCleGsor.dttPromozioni
      grPromo.DataSource = dcPromozioni
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdAnnullaPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnullaPromo.Click
    Try
      Me.Cursor = Cursors.WaitCursor

      oCleGsor.AnnullaPromozioni()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub cmdApplicaPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplicaPromo.Click
    Try
      Me.Cursor = Cursors.WaitCursor

      oCleGsor.ApplicaPromozioni()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdCancContratto1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto1.Click
    Try
      oCleGsor.CancellaContratto("")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancContratto2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto2.Click
    Try
      oCleGsor.CancellaContratto("2")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancContratto3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto3.Click
    Try
      oCleGsor.CancellaContratto("3")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancContratto4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto4.Click
    Try
      oCleGsor.CancellaContratto("4")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancContratto5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto5.Click
    Try
      oCleGsor.CancellaContratto("5")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancContratto6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancContratto6.Click
    Try
      oCleGsor.CancellaContratto("6")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub MostraPropostaOmaggi()
    Try
      Dim oPar As New CLE__CLDP
      oPar.ctlPar1 = oCleGsor.dttOmaggi

      oMenu.RunChild("NTSInformatica", "FRMMGOMGP", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      If Not oPar.bPar1 Then Return

      oCleGsor.AggiungiArticoliOmaggio()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function RicalcoloPromozioni() As Boolean
    Dim strMessaggio As String = ""
    Try
      'Le promozioni vengono ricalcolate solo se ci sono delle promozioni da applicare
      If oCleGsor.dttPromozioni Is Nothing OrElse oCleGsor.dttPromozioni.Rows.Count = 0 OrElse oCleGsor.dttEC.Rows.Count = 0 Then Return True

      If oCleGsor.bSalvaNuovoDocApplicaPromo Then 'se impostata l'opzione di registro ricalcola sempre le promozioni al salvataggio
        oCleGsor.ApplicaPromozioni() 'Applica\Riapplica le promozioni
      Else
        If oCleGsor.dttPromozioni.Select("backcolor_row = -1").Length = oCleGsor.dttPromozioni.Rows.Count Then
          'Non sono mai state applicate le promozioni
          strMessaggio = oApp.Tr(Me, 130602744612769343, "Non sono state applicate le promozioni associate ai contratti presenti." & vbCrLf & _
                                                         "Procedere ora l'applicazione delle promozioni?")
        ElseIf oCleGsor.dttPromozioni.Select("backcolor_row = " & Color.MistyRose.ToArgb).Length > 0 Then
          'Entra qui se è variata una riga rispetto a quando è stata applicata la promozione
          strMessaggio = oApp.Tr(Me, 130602744700534334, "Sono state apportate delle modifiche al documento dopo aver calcolato le promozioni." & vbCrLf & _
                                                         "Procedere con il ricalcolo delle promozioni?" & vbCrLf & _
                                                         "NOTA: Eventuali rettifiche alle righe generate da una promozione saranno perse.")
        Else
          'Le promozioni sono già state applicate e non sono necessari ulteriori ricalcoli
          Return True
        End If

        Select Case oApp.MsgBoxInfoYesNoCancel_DefYes(strMessaggio)
          Case Windows.Forms.DialogResult.Cancel : Return False
          Case Windows.Forms.DialogResult.Yes : oCleGsor.ApplicaPromozioni() 'Applica\Riapplica le promozioni
        End Select
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

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

  Public Overridable Function CheckRiapriDocSuSalva() As Boolean
    Try
      If oCleGsor.bRiapriDocSuSalva = -1 Then
        Return True
      ElseIf oCleGsor.bRiapriDocSuSalva = 1 And tlbRiapriDopoSalva.Checked = True Then
        Return True
      End If

      Return False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
