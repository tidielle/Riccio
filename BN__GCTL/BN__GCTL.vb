Imports System.Windows.Forms
Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GCTL
  Public ctlGest As Object  'controllo da trattare
  Public oCleGctl As CLE__GCTL
  Public dsUi As DataSet
  Public dtcUi As BindingSource = New BindingSource()
  Public strChild As String = ""
  Public strForm As String = ""
  Public strTipoDoc As String = ""
  Public strParent As String = ""    'controllo contenitore del controllo in analisi
  Public fmParent As FRM__CHIL      'form contenitore del controllo
  Public strControls() As String    'collezione di tutti gli NTScontrol presenti in form

  Public bInit As Boolean = False      'serve solo per evitare che vengano eseguite inutilmente delle routine al cambio del controllo (tanto vengono eseguite successivamente lo stesso)
  Public arXXparent As New ArrayList

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", _
                                  Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Dim strTmp As String = ""
    Dim oCtl As Control = Nothing
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

      '------------------------------------------------
      'creo e attivo l'entity
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__GCTL", "BE__GCTL", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222141875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleGctl = CType(oTmp, CLE__GCTL)
      '------------------------------------------------
      bRemoting = Menu.Remoting("BN__GCTL", strRemoteServer, strRemotePort)
      oCleGctl.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

      '---------------------------------
      'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
      AddHandler oCleGctl.RemoteEvent, AddressOf GestisciEventiEntity

      '---------------------------------
      'memorizzo i parametri passati dal child
      ctlGest = Param.ctlPar1
      If ctlGest.GetType.ToString.IndexOf("NTSBar") > -1 Then
        fmParent = CType(CType(ctlGest, DevExpress.XtraBars.BarItem).Manager.Form.FindForm, FRM__CHIL)
      ElseIf ctlGest.GetType.ToString.IndexOf("NTSGridView") > -1 Then
        fmParent = CType(CType(ctlGest, NTSGridView).GridControl.FindForm, FRM__CHIL)
      Else
        fmParent = CType(CType(ctlGest, Control).FindForm, FRM__CHIL)
      End If

      strForm = fmParent.Name
      lbTipoDoc.Text = oApp.Tr(Me, 130421150622006152, "Tipo doc.:") & "'" & Param.strPar2 & "'"  'mirto: lasciare così, non aggiungere "|"

      strChild = Param.strPar1
      strTipoDoc = Param.strPar2

      '---------------------------------
      'memorizzo tutti i controlli di tipo NTS presenti in form
      strTmp = MemorizzaNTSControls(fmParent)
      While (strTmp.IndexOf("||") > -1)
        strTmp = strTmp.Replace("||", "|")
      End While
      If strTmp.Length > 0 Then
        strTmp = strTmp.Substring(0, strTmp.Length - 1)
        strControls = strTmp.Split(CType("|", Char))
        cbControl.Properties.Items.Clear()
        cbControl.Properties.Items.AddRange(strControls)

        If ctlGest.GetType.ToString.IndexOf("NTSBarButtonItem") > -1 Then
          strTmp = CType(ctlGest, NTSBarButtonItem).Name
        ElseIf ctlGest.GetType.ToString.IndexOf("NTSBarMenuItem") > -1 Then
          strTmp = CType(ctlGest, NTSBarMenuItem).Name
        ElseIf ctlGest.GetType.ToString.IndexOf("NTSBarSubItem") > -1 Then
          strTmp = CType(ctlGest, NTSBarSubItem).Name
        ElseIf ctlGest.GetType.ToString.IndexOf("NTSGridView") > -1 Then
          strTmp = CType(ctlGest, NTSGridView).Name
          oCtl = CType(ctlGest, NTSGridView).GridControl.Parent
        Else
          strTmp = CType(ctlGest, Control).Name
          oCtl = CType(ctlGest, Control).Parent
        End If
        cbControl.Text = strTmp

        'se dopo al nome del controllo Ã¨ indicato l'oggetto NTSXX, devo posizionarmi sulla voce corretta
        If cbControl.SelectedIndex = -1 And Not oCtl Is Nothing Then
          While Not oCtl.Parent Is Nothing AndAlso oCtl.Parent.GetType.ToString.ToUpper.IndexOf("NTSXX") = -1
            oCtl = oCtl.Parent
          End While
          If Not oCtl Is Nothing Then
            strTmp += "(" & oCtl.Parent.Name & ")"
            cbControl.Text = strTmp
          End If
        End If

      End If

      '-----------------------------------------
      'blocco i controlli non gestiti se non viene passato il tipo documento
      If strTipoDoc = "" Then
        optPrior1.Enabled = False
        optPrior2.Enabled = False
        optPrior3.Enabled = False
        optPrior4.Enabled = False
        optPrior5.Enabled = False
        optPrior6.Enabled = False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Eventi di form"
  Public Overridable Sub FRM__GCTL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      If lbTipoControllo.Text.Trim = "Tipo:" Then   'la form: devo bloccare tutto
        optVisible.Enabled = False
        optEnable.Enabled = False
        optBold.Enabled = False
        optDefault.Enabled = False
        optFormatnumber.Enabled = False
        optOutnotequal.Enabled = False
        optChecked.Enabled = False
        optText.Enabled = False
        optErrorText.Enabled = False
        optInsert.Enabled = False
        optUpdate.Enabled = False
        optDelete.Enabled = False
        grvUI.Enabled = False
        grUI.Visible = False
      End If

      If strChild.ToUpper.IndexOf("CGPRIN") > -1 Then optOutnotequal.Visible = False 'quel programma è troppo particolare ... fa scatgenare errori vari
      If strChild.ToUpper.IndexOf("DWQVDA") > -1 Then optFormatnumber.Visible = False 'quel programma è troppo particolare ... fa scatgenare errori vari

      InitControls()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GCTL_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      cbControl_SelectedIndexChanged(cbControl, Nothing)

      '-------------------------------------------------
      'cerco di proporre la colonna su cui era il mouse quando ho cliccato per arrivare qui
      If ctlGest.GetType.Equals(GetType(NTSGridView)) Then
        If Not CType(ctlGest, NTSGridView).FocusedColumn Is Nothing Then
          cbGriglia.SelectedValue = CType(ctlGest, NTSGridView).FocusedColumn.Name
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__GCTL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva(False) Then
      e.Cancel = True
    Else
      '.Show("Riavviare il programma Child per applicare le modifiche apportate", oApp.MSGTTL, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End If
  End Sub
#End Region

  Public Overridable Sub cbControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbControl.SelectedIndexChanged
    Try
      ctlGest = NTSFindControlByName(fmParent, cbControl.Text)
      InitImpostazioniForm(ctlGest)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function InitImpostazioniForm(ByVal ctrl As Object) As Boolean
    Try
      bInit = True
      'lbControl.Text = "Controllo: " & ctlGest.Name
      'lbTipoControllo.Text = "Tipo: |" & ctlGest.GetType.ToString & "|"
      lbTipoControllo.Text = oApp.Tr(Me, 130421149394789536, "Tipo: ") & ctlGest.GetType.ToString    'mirto: lasciare così, non aggiungere "|"

      '-----------------------------------------
      'cerco il contenitore del controllo per ottenere il nome
      strParent = ""
      If ctlGest.GetType.ToString.IndexOf("NTSBarButtonItem") > -1 Then
        If Not CType(ctlGest, NTSBarButtonItem).Manager.Form Is Nothing Then strParent = CType(ctlGest, NTSBarButtonItem).Manager.Form.Name.ToString
      ElseIf ctlGest.GetType.ToString.IndexOf("NTSBarMenuItem") > -1 Then
        If Not CType(ctlGest, NTSBarMenuItem).Manager.Form Is Nothing Then strParent = CType(ctlGest, NTSBarMenuItem).Manager.Form.Name.ToString
      ElseIf ctlGest.GetType.ToString.IndexOf("NTSBarSubItem") > -1 Then
        If Not CType(ctlGest, NTSBarSubItem).Manager.Form Is Nothing Then strParent = CType(ctlGest, NTSBarSubItem).Manager.Form.Name.ToString
      ElseIf ctlGest.GetType.ToString.IndexOf("NTSGridView") > -1 Then
        If Not CType(ctlGest, NTSGridView).GridControl.Parent Is Nothing Then strParent = CType(ctlGest, NTSGridView).GridControl.Parent.Name.ToString
      Else
        If Not CType(ctlGest, Control).Parent Is Nothing Then strParent = CType(ctlGest, Control).Parent.Name.ToString
      End If

      '-----------------------------------------
      'carico il combo della griglia
      cbGriglia.Enabled = True
      CaricaCombo(1, cbGriglia)
      If ctlGest.GetType.Equals(GetType(NTSGridView)) Then
        cbGriglia_SelectedIndexChanged(Me, Nothing)
      Else
        cbGriglia.Enabled = False
      End If

      '-----------------------------------------
      'carico il combo degli item: possono essere combobox, listbox o combobox in una cella della griglia, menu o toolbar
      cbCombo.Enabled = True
      CaricaCombo(2, cbCombo)
      If ctlGest.GetType.Equals(GetType(NTSComboBox)) Then
      ElseIf ctlGest.GetType.Equals(GetType(NTSListBox)) Then
      ElseIf ctlGest.GetType.Equals(GetType(NTSTabControl)) Then
      Else
        cbCombo.Enabled = False
      End If

      '-----------------------------------------
      'carico il combo della lingua
      CaricaCombo(0, cbLingua)


      Abilitaproprietà()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGctl.Apri(strChild, strForm, strTipoDoc, lbTipoControllo.Text.Substring(6), cbControl.Text, dsUi) Then Me.Close()
      dtcUi.DataSource = dsUi.Tables("UICONF")
      dsUi.AcceptChanges()
      grUI.DataSource = dtcUi

      Evidenziacontrolli()

      Me.Cursor = Cursors.Default
      bInit = False

      Return True

    Catch ex As Exception
      bInit = False
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function MemorizzaNTSControls(ByVal ctrlStart As Control) As String
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim k As Integer = 0
    Dim j As Integer = 0
    Dim n As Integer = 0
    Dim strOut As String = ""
    Dim bToolbar As Boolean = False
    Dim bAddXxparent As Boolean = False

    'non vengono rilevati i controlli con proprietà 'TAG' = "V": 
    'questa proprietà viene impostata dalla GCTLInitFormDefautl
    'per impostare le limitazioni di Bus EASY e/o TipoInstalla = T/A/C di opzione di registro

    Try
      If ctrlStart.GetType.ToString.ToUpper.IndexOf("NTSXX") > -1 Then
        arXXparent.Add(ctrlStart.Name)
        bAddXxparent = True
      End If

      If ctrlStart.Parent Is Nothing Then strOut += ctrlStart.Name & "|" 'la form: solo la prima volta, altrimenti raddoppia 
      For i = 0 To ctrlStart.Controls.Count - 1
        If ctrlStart.Controls(i).GetType.ToString.IndexOf("NTSInformatica.NTS") > -1 Then
          If ctrlStart.Controls(i).Name.Trim <> "" And ctrlStart.Controls(i).Name <> "pnXXExt" Then
            If ctrlStart.Controls(i).GetType.ToString.IndexOf("NTSGrid") > -1 Then
              strOut += CType(ctrlStart.Controls(i), NTSGrid).DefaultView.Name
              If arXXparent.Count > 0 Then strOut += "(" & arXXparent.Item(arXXparent.Count - 1).ToString & ")"
              strOut += "|"
            Else
              If ctrlStart.Controls(i).Name <> "NTSText" And ctrlStart.Controls(i).GetType.ToString.IndexOf("NTSInformatica.NTSXX") = -1 Then
                If ctrlStart.Controls(i).Tag Is Nothing Then
                  strOut += ctrlStart.Controls(i).Name
                ElseIf ctrlStart.Controls(i).Tag.ToString <> "V" Then
                  strOut += ctrlStart.Controls(i).Name
                End If
                If arXXparent.Count > 0 Then strOut += "(" & arXXparent.Item(arXXparent.Count - 1).ToString & ")"
                strOut += "|"
              End If
            End If
          End If
          If ctrlStart.Controls(i).Controls.Count > 0 And ctrlStart.Controls(i).GetType.ToString.IndexOf("ComboBox") = -1 Then
            If ctrlStart.Controls(i).Controls(0).Name <> "NTSText" Then strOut += MemorizzaNTSControls(ctrlStart.Controls(i))
          End If
        Else
          'gestione toolbar e menu
          'scendo fino ad un massimo di 3 livelli .... tanto oltre sarebbe ingestibile il menu dall'utente
          If ctrlStart.Controls(i).GetType.ToString = "DevExpress.XtraBars.BarDockControl" Then
            If bToolbar = False Then
              bToolbar = True

              Dim oBarm As NTSBarManager = CType(CType(ctrlStart.Controls(i), DevExpress.XtraBars.BarDockControl).Manager, NTSBarManager)
              For l = 0 To oBarm.Bars.Count - 1
                For k = 0 To oBarm.Bars(l).ItemLinks.Count - 1
                  If NTSCStr(oBarm.Bars(l).ItemLinks(k).Item.Tag) <> "V" Then
                    strOut += oBarm.Bars(l).ItemLinks(k).Item.Name & "|"
                  End If
                  If oBarm.Bars(l).ItemLinks(k).GetType.ToString.IndexOf("BarSubItem") > -1 Then
                    For j = 0 To CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks.Count - 1
                      If NTSCStr(CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).Item.Tag) <> "V" Then
                        strOut += CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).Item.Name
                        If arXXparent.Count > 0 Then strOut += "(" & arXXparent.Item(arXXparent.Count - 1).ToString & ")"
                        strOut += "|"
                      End If
                      If CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).GetType.ToString.IndexOf("BarSubItem") > -1 Then
                        For n = 0 To CType(CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).Item, NTSBarSubItem).ItemLinks.Count - 1
                          If NTSCStr(CType(CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).Item, NTSBarSubItem).ItemLinks(n).Item.Tag) <> "V" Then
                            strOut += CType(CType(oBarm.Bars(l).ItemLinks(k).Item, NTSBarSubItem).ItemLinks(j).Item, NTSBarSubItem).ItemLinks(n).Item.Name
                            If arXXparent.Count > 0 Then strOut += "(" & arXXparent.Item(arXXparent.Count - 1).ToString & ")"
                            strOut += "|"
                          End If
                        Next
                      End If
                    Next
                  End If
                Next
              Next
            End If
          End If
        End If
      Next

      If bAddXxparent Then
        arXXparent.RemoveAt(arXXparent.Count - 1)
      End If

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei controlli
      grvUI.NTSSetParam(oMenu, oApp.Tr(Me, 128230023396519483, "Griglia Personalizzazioni"))
      ui_db.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023396675656, "Azienda"), 25)
      ui_ditta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023396831829, "Ditta"), 12)
      ui_tipodoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023396988002, "Tipo documento"), 3)
      ui_ruolo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023397144175, "Gruppo operatore"), 20)
      ui_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023397300348, "Operatore"), 20)
      ui_nomprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023397456521, "Nome proprietà"), 0)
      ui_valprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023397612694, "Valore proprietà"), 0, True, True)
      ui_usascript.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023397768867, "Usa Script"), "S", "N")
      ui_script.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023397925040, "Script"), 0)
      ui_gridcol.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023398081213, "Colonna della griglia"), 20)
      ui_comboitem.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023398237386, "Elemento del combobox"), 20)
      ui_codling.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023398393559, "Codice lingua"), "0", 4)
      ui_ctrltype.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023398549732, "Tipo di controllo"), 0)
      ui_child.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023398705905, "Nome del child"), 0)
      ui_form.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023398862078, "Nome della form"), 0)
      ui_ctrlname.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023399018251, "Nome del controllo"), 0)

      cbControl.NTSSetParam(oApp.Tr(Me, 128230023399174424, "Controlli"))
      optPrior13.NTSSetParam(oMenu, oApp.Tr(Me, 128230023399330597, ""), "S")
      optPrior12.NTSSetParam(oMenu, oApp.Tr(Me, 128230023399486770, ""), "S")
      optPrior11.NTSSetParam(oMenu, oApp.Tr(Me, 128230023399642943, ""), "S")
      optPrior10.NTSSetParam(oMenu, oApp.Tr(Me, 128230023399799116, ""), "S")
      optPrior9.NTSSetParam(oMenu, oApp.Tr(Me, 128230023399955289, ""), "S")
      optPrior8.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400111462, ""), "S")
      optPrior7.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400267635, ""), "S")
      optPrior6.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400423808, ""), "S")
      optPrior5.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400579981, ""), "S")
      optPrior4.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400736154, ""), "S")
      optPrior3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023400892327, ""), "S")
      optPrior2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401048500, ""), "S")
      optPrior1.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401204673, ""), "S")
      optDelete.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401360846, ""), "S")
      optUpdate.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401517019, ""), "S")
      optInsert.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401673192, ""), "S")
      optChecked.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401829365, ""), "S")
      optOutnotequal.NTSSetParam(oMenu, oApp.Tr(Me, 128230023401985538, ""), "S")
      optDefault.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402141711, ""), "S")
      optFormatnumber.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402297884, ""), "S")
      optEnable.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402454057, ""), "S")
      optBold.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402610230, ""), "S")
      optVisible.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402766403, ""), "S")
      optText.NTSSetParam(oMenu, oApp.Tr(Me, 128230023402922576, ""), "S")
      optErrorText.NTSSetParam(oMenu, oApp.Tr(Me, 128394227438122000, ""), "S")
      cbLingua.NTSSetParam(oApp.Tr(Me, 128230023403078749, "Lingua"))
      cbGriglia.NTSSetParam(oApp.Tr(Me, 128230023403234922, "Griglia"))
      cbCombo.NTSSetParam(oApp.Tr(Me, 128230023403391095, "ComboBox"))

      ui_db.NTSSetParamZoom("ZOOMAZIENDE")
      ui_ditta.NTSSetParamZoom("ZOOMTABANAZ")
      ui_ruolo.NTSSetParamZoom("ZOOMRUOLI")
      ui_opnome.NTSSetParamZoom("ZOOMOPERAT")
      ui_codling.NTSSetParamZoom("ZOOMTABLINGP")

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
    Me.lbControl = New NTSInformatica.NTSLabel
    Me.lbTipoControllo = New NTSInformatica.NTSLabel
    Me.cbCombo = New NTSInformatica.NTSComboBox
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cbGriglia = New NTSInformatica.NTSComboBox
    Me.cbLingua = New NTSInformatica.NTSComboBox
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.optErrorText = New NTSInformatica.NTSRadioButton
    Me.optDelete = New NTSInformatica.NTSRadioButton
    Me.optUpdate = New NTSInformatica.NTSRadioButton
    Me.optInsert = New NTSInformatica.NTSRadioButton
    Me.optChecked = New NTSInformatica.NTSRadioButton
    Me.optOutnotequal = New NTSInformatica.NTSRadioButton
    Me.optDefault = New NTSInformatica.NTSRadioButton
    Me.optFormatnumber = New NTSInformatica.NTSRadioButton
    Me.optEnable = New NTSInformatica.NTSRadioButton
    Me.optBold = New NTSInformatica.NTSRadioButton
    Me.optVisible = New NTSInformatica.NTSRadioButton
    Me.optText = New NTSInformatica.NTSRadioButton
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox
    Me.optPrior13 = New NTSInformatica.NTSRadioButton
    Me.optPrior12 = New NTSInformatica.NTSRadioButton
    Me.optPrior11 = New NTSInformatica.NTSRadioButton
    Me.optPrior10 = New NTSInformatica.NTSRadioButton
    Me.optPrior9 = New NTSInformatica.NTSRadioButton
    Me.optPrior8 = New NTSInformatica.NTSRadioButton
    Me.optPrior7 = New NTSInformatica.NTSRadioButton
    Me.optPrior6 = New NTSInformatica.NTSRadioButton
    Me.optPrior5 = New NTSInformatica.NTSRadioButton
    Me.optPrior4 = New NTSInformatica.NTSRadioButton
    Me.optPrior3 = New NTSInformatica.NTSRadioButton
    Me.optPrior2 = New NTSInformatica.NTSRadioButton
    Me.optPrior1 = New NTSInformatica.NTSRadioButton
    Me.NtsGroupBox3 = New NTSInformatica.NTSGroupBox
    Me.grUI = New NTSInformatica.NTSGrid
    Me.grvUI = New NTSInformatica.NTSGridView
    Me.ui_db = New NTSInformatica.NTSGridColumn
    Me.ui_ditta = New NTSInformatica.NTSGridColumn
    Me.ui_tipodoc = New NTSInformatica.NTSGridColumn
    Me.ui_ruolo = New NTSInformatica.NTSGridColumn
    Me.ui_opnome = New NTSInformatica.NTSGridColumn
    Me.ui_nomprop = New NTSInformatica.NTSGridColumn
    Me.ui_valprop = New NTSInformatica.NTSGridColumn
    Me.ui_usascript = New NTSInformatica.NTSGridColumn
    Me.ui_script = New NTSInformatica.NTSGridColumn
    Me.ui_gridcol = New NTSInformatica.NTSGridColumn
    Me.ui_comboitem = New NTSInformatica.NTSGridColumn
    Me.ui_codling = New NTSInformatica.NTSGridColumn
    Me.ui_ctrltype = New NTSInformatica.NTSGridColumn
    Me.ui_child = New NTSInformatica.NTSGridColumn
    Me.ui_form = New NTSInformatica.NTSGridColumn
    Me.ui_ctrlname = New NTSInformatica.NTSGridColumn
    Me.cmdRipristina = New NTSInformatica.NTSButton
    Me.cmdCancRow = New NTSInformatica.NTSButton
    Me.lbTipoDoc = New NTSInformatica.NTSLabel
    Me.lbNota = New NTSInformatica.NTSLabel
    Me.cmdRemoting = New NTSInformatica.NTSButton
    Me.cbControl = New NTSInformatica.NTSComboBox
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.lbLingua = New NTSInformatica.NTSLabel
    Me.lbGriglia = New NTSInformatica.NTSLabel
    Me.lbCombo = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.cmdFastVisible = New NTSInformatica.NTSButton
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    CType(Me.cbCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbGriglia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbLingua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.optErrorText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optInsert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optChecked.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optOutnotequal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDefault.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optFormatnumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optEnable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optBold.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optVisible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.optPrior13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox3.SuspendLayout()
    CType(Me.grUI, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvUI, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbControl
    '
    Me.lbControl.BackColor = System.Drawing.Color.Transparent
    Me.lbControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbControl.Location = New System.Drawing.Point(8, 9)
    Me.lbControl.Name = "lbControl"
    Me.lbControl.NTSDbField = ""
    Me.lbControl.Size = New System.Drawing.Size(76, 25)
    Me.lbControl.TabIndex = 0
    Me.lbControl.Text = "Controllo: "
    Me.lbControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbControl.Tooltip = ""
    Me.lbControl.UseMnemonic = False
    '
    'lbTipoControllo
    '
    Me.lbTipoControllo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoControllo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbTipoControllo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTipoControllo.Location = New System.Drawing.Point(352, 9)
    Me.lbTipoControllo.Name = "lbTipoControllo"
    Me.lbTipoControllo.NTSDbField = ""
    Me.lbTipoControllo.Size = New System.Drawing.Size(284, 25)
    Me.lbTipoControllo.TabIndex = 2
    Me.lbTipoControllo.Text = "Tipo: "
    Me.lbTipoControllo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbTipoControllo.Tooltip = ""
    Me.lbTipoControllo.UseMnemonic = False
    '
    'cbCombo
    '
    Me.cbCombo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCombo.DataSource = Nothing
    Me.cbCombo.DisplayMember = ""
    Me.cbCombo.Location = New System.Drawing.Point(90, 68)
    Me.cbCombo.Name = "cbCombo"
    Me.cbCombo.NTSDbField = ""
    Me.cbCombo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbCombo.Properties.Appearance.Options.UseBackColor = True
    Me.cbCombo.Properties.AutoHeight = False
    Me.cbCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCombo.Properties.DropDownRows = 30
    Me.cbCombo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCombo.SelectedValue = ""
    Me.cbCombo.Size = New System.Drawing.Size(181, 20)
    Me.cbCombo.TabIndex = 1
    Me.cbCombo.ValueMember = ""
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(560, 42)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(76, 24)
    Me.cmdConferma.TabIndex = 6
    Me.cmdConferma.Text = "&Conferma"
    '
    'cbGriglia
    '
    Me.cbGriglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbGriglia.DataSource = Nothing
    Me.cbGriglia.DisplayMember = ""
    Me.cbGriglia.Location = New System.Drawing.Point(90, 42)
    Me.cbGriglia.Name = "cbGriglia"
    Me.cbGriglia.NTSDbField = ""
    Me.cbGriglia.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbGriglia.Properties.Appearance.Options.UseBackColor = True
    Me.cbGriglia.Properties.AutoHeight = False
    Me.cbGriglia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbGriglia.Properties.DropDownRows = 30
    Me.cbGriglia.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbGriglia.SelectedValue = ""
    Me.cbGriglia.Size = New System.Drawing.Size(181, 20)
    Me.cbGriglia.TabIndex = 0
    Me.cbGriglia.ValueMember = ""
    '
    'cbLingua
    '
    Me.cbLingua.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbLingua.DataSource = Nothing
    Me.cbLingua.DisplayMember = ""
    Me.cbLingua.Location = New System.Drawing.Point(425, 42)
    Me.cbLingua.Name = "cbLingua"
    Me.cbLingua.NTSDbField = ""
    Me.cbLingua.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbLingua.Properties.Appearance.Options.UseBackColor = True
    Me.cbLingua.Properties.AutoHeight = False
    Me.cbLingua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbLingua.Properties.DropDownRows = 30
    Me.cbLingua.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbLingua.SelectedValue = ""
    Me.cbLingua.Size = New System.Drawing.Size(111, 20)
    Me.cbLingua.TabIndex = 5
    Me.cbLingua.ValueMember = ""
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.optErrorText)
    Me.NtsGroupBox1.Controls.Add(Me.optDelete)
    Me.NtsGroupBox1.Controls.Add(Me.optUpdate)
    Me.NtsGroupBox1.Controls.Add(Me.optInsert)
    Me.NtsGroupBox1.Controls.Add(Me.optChecked)
    Me.NtsGroupBox1.Controls.Add(Me.optOutnotequal)
    Me.NtsGroupBox1.Controls.Add(Me.optDefault)
    Me.NtsGroupBox1.Controls.Add(Me.optFormatnumber)
    Me.NtsGroupBox1.Controls.Add(Me.optEnable)
    Me.NtsGroupBox1.Controls.Add(Me.optBold)
    Me.NtsGroupBox1.Controls.Add(Me.optVisible)
    Me.NtsGroupBox1.Controls.Add(Me.optText)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(12, 108)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(222, 192)
    Me.NtsGroupBox1.TabIndex = 2
    Me.NtsGroupBox1.Text = "(4) proprietà da settare"
    '
    'optErrorText
    '
    Me.optErrorText.Cursor = System.Windows.Forms.Cursors.Default
    Me.optErrorText.Location = New System.Drawing.Point(126, 48)
    Me.optErrorText.Name = "optErrorText"
    Me.optErrorText.NTSCheckValue = "S"
    Me.optErrorText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optErrorText.Properties.Appearance.Options.UseBackColor = True
    Me.optErrorText.Properties.AutoHeight = False
    Me.optErrorText.Properties.Caption = "Error Text"
    Me.optErrorText.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optErrorText.Size = New System.Drawing.Size(91, 19)
    Me.optErrorText.TabIndex = 8
    '
    'optDelete
    '
    Me.optDelete.Cursor = System.Windows.Forms.Cursors.Default
    Me.optDelete.Location = New System.Drawing.Point(126, 143)
    Me.optDelete.Name = "optDelete"
    Me.optDelete.NTSCheckValue = "S"
    Me.optDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDelete.Properties.Appearance.Options.UseBackColor = True
    Me.optDelete.Properties.AutoHeight = False
    Me.optDelete.Properties.Caption = "Delete  (0/-1)"
    Me.optDelete.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDelete.Size = New System.Drawing.Size(91, 19)
    Me.optDelete.TabIndex = 11
    '
    'optUpdate
    '
    Me.optUpdate.Cursor = System.Windows.Forms.Cursors.Default
    Me.optUpdate.Location = New System.Drawing.Point(126, 119)
    Me.optUpdate.Name = "optUpdate"
    Me.optUpdate.NTSCheckValue = "S"
    Me.optUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optUpdate.Properties.Appearance.Options.UseBackColor = True
    Me.optUpdate.Properties.AutoHeight = False
    Me.optUpdate.Properties.Caption = "Update (0/-1)"
    Me.optUpdate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optUpdate.Size = New System.Drawing.Size(91, 19)
    Me.optUpdate.TabIndex = 10
    '
    'optInsert
    '
    Me.optInsert.Cursor = System.Windows.Forms.Cursors.Default
    Me.optInsert.Location = New System.Drawing.Point(126, 95)
    Me.optInsert.Name = "optInsert"
    Me.optInsert.NTSCheckValue = "S"
    Me.optInsert.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optInsert.Properties.Appearance.Options.UseBackColor = True
    Me.optInsert.Properties.AutoHeight = False
    Me.optInsert.Properties.Caption = "Insert   (0/-1)"
    Me.optInsert.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optInsert.Size = New System.Drawing.Size(96, 19)
    Me.optInsert.TabIndex = 9
    '
    'optChecked
    '
    Me.optChecked.Cursor = System.Windows.Forms.Cursors.Default
    Me.optChecked.Location = New System.Drawing.Point(15, 167)
    Me.optChecked.Name = "optChecked"
    Me.optChecked.NTSCheckValue = "S"
    Me.optChecked.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optChecked.Properties.Appearance.Options.UseBackColor = True
    Me.optChecked.Properties.AutoHeight = False
    Me.optChecked.Properties.Caption = "Checked (N/S)"
    Me.optChecked.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optChecked.Size = New System.Drawing.Size(105, 19)
    Me.optChecked.TabIndex = 6
    '
    'optOutnotequal
    '
    Me.optOutnotequal.Cursor = System.Windows.Forms.Cursors.Default
    Me.optOutnotequal.Location = New System.Drawing.Point(15, 143)
    Me.optOutnotequal.Name = "optOutnotequal"
    Me.optOutnotequal.NTSCheckValue = "S"
    Me.optOutnotequal.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optOutnotequal.Properties.Appearance.Options.UseBackColor = True
    Me.optOutnotequal.Properties.AutoHeight = False
    Me.optOutnotequal.Properties.Caption = "Out not equal"
    Me.optOutnotequal.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optOutnotequal.Size = New System.Drawing.Size(105, 19)
    Me.optOutnotequal.TabIndex = 5
    '
    'optDefault
    '
    Me.optDefault.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.optDefault.Location = New System.Drawing.Point(15, 95)
    Me.optDefault.Name = "optDefault"
    Me.optDefault.NTSCheckValue = "S"
    Me.optDefault.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDefault.Properties.Appearance.Options.UseBackColor = True
    Me.optDefault.Properties.AutoHeight = False
    Me.optDefault.Properties.Caption = "Default"
    Me.optDefault.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDefault.Size = New System.Drawing.Size(79, 19)
    Me.optDefault.TabIndex = 3
    '
    'optFormatnumber
    '
    Me.optFormatnumber.Cursor = System.Windows.Forms.Cursors.Default
    Me.optFormatnumber.Location = New System.Drawing.Point(15, 119)
    Me.optFormatnumber.Name = "optFormatnumber"
    Me.optFormatnumber.NTSCheckValue = "S"
    Me.optFormatnumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optFormatnumber.Properties.Appearance.Options.UseBackColor = True
    Me.optFormatnumber.Properties.AutoHeight = False
    Me.optFormatnumber.Properties.Caption = "Format number"
    Me.optFormatnumber.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optFormatnumber.Size = New System.Drawing.Size(105, 19)
    Me.optFormatnumber.TabIndex = 4
    '
    'optEnable
    '
    Me.optEnable.Cursor = System.Windows.Forms.Cursors.Default
    Me.optEnable.Location = New System.Drawing.Point(15, 47)
    Me.optEnable.Name = "optEnable"
    Me.optEnable.NTSCheckValue = "S"
    Me.optEnable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optEnable.Properties.Appearance.Options.UseBackColor = True
    Me.optEnable.Properties.AutoHeight = False
    Me.optEnable.Properties.Caption = "Enable  (0/-1)"
    Me.optEnable.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optEnable.Size = New System.Drawing.Size(105, 19)
    Me.optEnable.TabIndex = 1
    '
    'optBold
    '
    Me.optBold.Cursor = System.Windows.Forms.Cursors.Default
    Me.optBold.Location = New System.Drawing.Point(15, 71)
    Me.optBold.Name = "optBold"
    Me.optBold.NTSCheckValue = "S"
    Me.optBold.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optBold.Properties.Appearance.Options.UseBackColor = True
    Me.optBold.Properties.AutoHeight = False
    Me.optBold.Properties.Caption = "Bold      (0/-1)"
    Me.optBold.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optBold.Size = New System.Drawing.Size(105, 19)
    Me.optBold.TabIndex = 2
    '
    'optVisible
    '
    Me.optVisible.Cursor = System.Windows.Forms.Cursors.Default
    Me.optVisible.Location = New System.Drawing.Point(15, 23)
    Me.optVisible.Name = "optVisible"
    Me.optVisible.NTSCheckValue = "S"
    Me.optVisible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optVisible.Properties.Appearance.Options.UseBackColor = True
    Me.optVisible.Properties.AutoHeight = False
    Me.optVisible.Properties.Caption = "Visible   (0/-1)"
    Me.optVisible.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optVisible.Size = New System.Drawing.Size(105, 19)
    Me.optVisible.TabIndex = 0
    '
    'optText
    '
    Me.optText.Cursor = System.Windows.Forms.Cursors.Default
    Me.optText.Location = New System.Drawing.Point(126, 23)
    Me.optText.Name = "optText"
    Me.optText.NTSCheckValue = "S"
    Me.optText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optText.Properties.Appearance.Options.UseBackColor = True
    Me.optText.Properties.AutoHeight = False
    Me.optText.Properties.Caption = "Text (*)"
    Me.optText.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optText.Size = New System.Drawing.Size(91, 19)
    Me.optText.TabIndex = 7
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.optPrior13)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior12)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior11)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior10)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior9)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior8)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior7)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior6)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior5)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior4)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior3)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior2)
    Me.NtsGroupBox2.Controls.Add(Me.optPrior1)
    Me.NtsGroupBox2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox2.Location = New System.Drawing.Point(254, 108)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(382, 192)
    Me.NtsGroupBox2.TabIndex = 3
    Me.NtsGroupBox2.Text = "(5) dipendenze gestite in ordine di priorità"
    '
    'optPrior13
    '
    Me.optPrior13.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior13.Location = New System.Drawing.Point(222, 167)
    Me.optPrior13.Name = "optPrior13"
    Me.optPrior13.NTSCheckValue = "S"
    Me.optPrior13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior13.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior13.Properties.AutoHeight = False
    Me.optPrior13.Properties.Caption = "13- generale"
    Me.optPrior13.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior13.Size = New System.Drawing.Size(109, 19)
    Me.optPrior13.TabIndex = 12
    '
    'optPrior12
    '
    Me.optPrior12.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior12.Location = New System.Drawing.Point(222, 143)
    Me.optPrior12.Name = "optPrior12"
    Me.optPrior12.NTSCheckValue = "S"
    Me.optPrior12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior12.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior12.Properties.AutoHeight = False
    Me.optPrior12.Properties.Caption = "12- azienda"
    Me.optPrior12.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior12.Size = New System.Drawing.Size(109, 19)
    Me.optPrior12.TabIndex = 11
    '
    'optPrior11
    '
    Me.optPrior11.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior11.Location = New System.Drawing.Point(222, 119)
    Me.optPrior11.Name = "optPrior11"
    Me.optPrior11.NTSCheckValue = "S"
    Me.optPrior11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior11.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior11.Properties.AutoHeight = False
    Me.optPrior11.Properties.Caption = "11- ditta"
    Me.optPrior11.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior11.Size = New System.Drawing.Size(109, 19)
    Me.optPrior11.TabIndex = 10
    '
    'optPrior10
    '
    Me.optPrior10.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior10.Location = New System.Drawing.Point(222, 95)
    Me.optPrior10.Name = "optPrior10"
    Me.optPrior10.NTSCheckValue = "S"
    Me.optPrior10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior10.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior10.Properties.AutoHeight = False
    Me.optPrior10.Properties.Caption = "10- gruppo operat, azienda"
    Me.optPrior10.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior10.Size = New System.Drawing.Size(160, 19)
    Me.optPrior10.TabIndex = 9
    '
    'optPrior9
    '
    Me.optPrior9.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior9.Location = New System.Drawing.Point(222, 71)
    Me.optPrior9.Name = "optPrior9"
    Me.optPrior9.NTSCheckValue = "S"
    Me.optPrior9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior9.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior9.Properties.AutoHeight = False
    Me.optPrior9.Properties.Caption = " 9- gruppo operat, ditta"
    Me.optPrior9.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior9.Size = New System.Drawing.Size(152, 19)
    Me.optPrior9.TabIndex = 8
    '
    'optPrior8
    '
    Me.optPrior8.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior8.Location = New System.Drawing.Point(222, 47)
    Me.optPrior8.Name = "optPrior8"
    Me.optPrior8.NTSCheckValue = "S"
    Me.optPrior8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior8.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior8.Properties.AutoHeight = False
    Me.optPrior8.Properties.Caption = " 8- operatore, azienda"
    Me.optPrior8.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior8.Size = New System.Drawing.Size(152, 19)
    Me.optPrior8.TabIndex = 7
    '
    'optPrior7
    '
    Me.optPrior7.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior7.Location = New System.Drawing.Point(222, 22)
    Me.optPrior7.Name = "optPrior7"
    Me.optPrior7.NTSCheckValue = "S"
    Me.optPrior7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior7.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior7.Properties.AutoHeight = False
    Me.optPrior7.Properties.Caption = "7- operatore, ditta"
    Me.optPrior7.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior7.Size = New System.Drawing.Size(132, 19)
    Me.optPrior7.TabIndex = 6
    '
    'optPrior6
    '
    Me.optPrior6.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior6.Location = New System.Drawing.Point(9, 143)
    Me.optPrior6.Name = "optPrior6"
    Me.optPrior6.NTSCheckValue = "S"
    Me.optPrior6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior6.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior6.Properties.AutoHeight = False
    Me.optPrior6.Properties.Caption = "6- tipo doc, azienda"
    Me.optPrior6.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior6.Size = New System.Drawing.Size(145, 19)
    Me.optPrior6.TabIndex = 5
    '
    'optPrior5
    '
    Me.optPrior5.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior5.Location = New System.Drawing.Point(9, 119)
    Me.optPrior5.Name = "optPrior5"
    Me.optPrior5.NTSCheckValue = "S"
    Me.optPrior5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior5.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior5.Properties.AutoHeight = False
    Me.optPrior5.Properties.Caption = "5- tipo doc, ditta"
    Me.optPrior5.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior5.Size = New System.Drawing.Size(126, 19)
    Me.optPrior5.TabIndex = 4
    '
    'optPrior4
    '
    Me.optPrior4.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior4.Location = New System.Drawing.Point(9, 95)
    Me.optPrior4.Name = "optPrior4"
    Me.optPrior4.NTSCheckValue = "S"
    Me.optPrior4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior4.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior4.Properties.AutoHeight = False
    Me.optPrior4.Properties.Caption = "4- tipo doc, gruppo operat, azienda"
    Me.optPrior4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior4.Size = New System.Drawing.Size(207, 19)
    Me.optPrior4.TabIndex = 3
    '
    'optPrior3
    '
    Me.optPrior3.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior3.Location = New System.Drawing.Point(9, 71)
    Me.optPrior3.Name = "optPrior3"
    Me.optPrior3.NTSCheckValue = "S"
    Me.optPrior3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior3.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior3.Properties.AutoHeight = False
    Me.optPrior3.Properties.Caption = "3- tipo doc, gruppo operat, ditta"
    Me.optPrior3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior3.Size = New System.Drawing.Size(207, 19)
    Me.optPrior3.TabIndex = 2
    '
    'optPrior2
    '
    Me.optPrior2.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior2.Location = New System.Drawing.Point(9, 47)
    Me.optPrior2.Name = "optPrior2"
    Me.optPrior2.NTSCheckValue = "S"
    Me.optPrior2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior2.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior2.Properties.AutoHeight = False
    Me.optPrior2.Properties.Caption = "2- tipo doc, operatore, azienda"
    Me.optPrior2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior2.Size = New System.Drawing.Size(196, 19)
    Me.optPrior2.TabIndex = 1
    '
    'optPrior1
    '
    Me.optPrior1.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior1.Location = New System.Drawing.Point(9, 23)
    Me.optPrior1.Name = "optPrior1"
    Me.optPrior1.NTSCheckValue = "S"
    Me.optPrior1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior1.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior1.Properties.AutoHeight = False
    Me.optPrior1.Properties.Caption = "1- tipo doc, operatore, ditta"
    Me.optPrior1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior1.Size = New System.Drawing.Size(196, 19)
    Me.optPrior1.TabIndex = 0
    '
    'NtsGroupBox3
    '
    Me.NtsGroupBox3.AllowDrop = True
    Me.NtsGroupBox3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox3.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox3.Controls.Add(Me.grUI)
    Me.NtsGroupBox3.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox3.Location = New System.Drawing.Point(12, 307)
    Me.NtsGroupBox3.Name = "NtsGroupBox3"
    Me.NtsGroupBox3.Size = New System.Drawing.Size(624, 197)
    Me.NtsGroupBox3.TabIndex = 4
    Me.NtsGroupBox3.Text = "(6) Valori impostati"
    '
    'grUI
    '
    Me.grUI.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grUI.EmbeddedNavigator.Name = ""
    Me.grUI.Location = New System.Drawing.Point(2, 20)
    Me.grUI.MainView = Me.grvUI
    Me.grUI.Name = "grUI"
    Me.grUI.Size = New System.Drawing.Size(620, 175)
    Me.grUI.TabIndex = 1
    Me.grUI.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvUI})
    '
    'grvUI
    '
    Me.grvUI.ActiveFilterEnabled = False
    Me.grvUI.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ui_db, Me.ui_ditta, Me.ui_tipodoc, Me.ui_ruolo, Me.ui_opnome, Me.ui_nomprop, Me.ui_valprop, Me.ui_usascript, Me.ui_script, Me.ui_gridcol, Me.ui_comboitem, Me.ui_codling, Me.ui_ctrltype, Me.ui_child, Me.ui_form, Me.ui_ctrlname})
    Me.grvUI.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvUI.Enabled = True
    Me.grvUI.GridControl = Me.grUI
    Me.grvUI.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvUI.MinRowHeight = 14
    Me.grvUI.Name = "grvUI"
    Me.grvUI.NTSAllowDelete = True
    Me.grvUI.NTSAllowInsert = True
    Me.grvUI.NTSAllowUpdate = True
    Me.grvUI.NTSMenuContext = Nothing
    Me.grvUI.OptionsCustomization.AllowRowSizing = True
    Me.grvUI.OptionsFilter.AllowFilterEditor = False
    Me.grvUI.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvUI.OptionsNavigation.UseTabKey = False
    Me.grvUI.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvUI.OptionsView.ColumnAutoWidth = False
    Me.grvUI.OptionsView.EnableAppearanceEvenRow = True
    Me.grvUI.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvUI.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvUI.OptionsView.ShowGroupPanel = False
    Me.grvUI.RowHeight = 16
    '
    'ui_db
    '
    Me.ui_db.AppearanceCell.Options.UseBackColor = True
    Me.ui_db.AppearanceCell.Options.UseTextOptions = True
    Me.ui_db.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_db.Caption = "Azienda"
    Me.ui_db.Enabled = True
    Me.ui_db.FieldName = "ui_db"
    Me.ui_db.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_db.Name = "ui_db"
    Me.ui_db.NTSRepositoryComboBox = Nothing
    Me.ui_db.NTSRepositoryItemCheck = Nothing
    Me.ui_db.NTSRepositoryItemMemo = Nothing
    Me.ui_db.NTSRepositoryItemText = Nothing
    Me.ui_db.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_db.OptionsFilter.AllowFilter = False
    Me.ui_db.Visible = True
    Me.ui_db.VisibleIndex = 0
    Me.ui_db.Width = 91
    '
    'ui_ditta
    '
    Me.ui_ditta.AppearanceCell.Options.UseBackColor = True
    Me.ui_ditta.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ditta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ditta.Caption = "Ditta"
    Me.ui_ditta.Enabled = True
    Me.ui_ditta.FieldName = "ui_ditta"
    Me.ui_ditta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ditta.Name = "ui_ditta"
    Me.ui_ditta.NTSRepositoryComboBox = Nothing
    Me.ui_ditta.NTSRepositoryItemCheck = Nothing
    Me.ui_ditta.NTSRepositoryItemMemo = Nothing
    Me.ui_ditta.NTSRepositoryItemText = Nothing
    Me.ui_ditta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ditta.OptionsFilter.AllowFilter = False
    Me.ui_ditta.Visible = True
    Me.ui_ditta.VisibleIndex = 1
    '
    'ui_tipodoc
    '
    Me.ui_tipodoc.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.ui_tipodoc.AppearanceCell.Options.UseBackColor = True
    Me.ui_tipodoc.AppearanceCell.Options.UseTextOptions = True
    Me.ui_tipodoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_tipodoc.Caption = "Tipo documento"
    Me.ui_tipodoc.Enabled = False
    Me.ui_tipodoc.FieldName = "ui_tipodoc"
    Me.ui_tipodoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_tipodoc.Name = "ui_tipodoc"
    Me.ui_tipodoc.NTSRepositoryComboBox = Nothing
    Me.ui_tipodoc.NTSRepositoryItemCheck = Nothing
    Me.ui_tipodoc.NTSRepositoryItemMemo = Nothing
    Me.ui_tipodoc.NTSRepositoryItemText = Nothing
    Me.ui_tipodoc.OptionsColumn.AllowEdit = False
    Me.ui_tipodoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_tipodoc.OptionsColumn.ReadOnly = True
    Me.ui_tipodoc.OptionsFilter.AllowFilter = False
    Me.ui_tipodoc.Visible = True
    Me.ui_tipodoc.VisibleIndex = 2
    Me.ui_tipodoc.Width = 54
    '
    'ui_ruolo
    '
    Me.ui_ruolo.AppearanceCell.Options.UseBackColor = True
    Me.ui_ruolo.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ruolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ruolo.Caption = "Gruppo"
    Me.ui_ruolo.Enabled = True
    Me.ui_ruolo.FieldName = "ui_ruolo"
    Me.ui_ruolo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ruolo.Name = "ui_ruolo"
    Me.ui_ruolo.NTSRepositoryComboBox = Nothing
    Me.ui_ruolo.NTSRepositoryItemCheck = Nothing
    Me.ui_ruolo.NTSRepositoryItemMemo = Nothing
    Me.ui_ruolo.NTSRepositoryItemText = Nothing
    Me.ui_ruolo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ruolo.OptionsFilter.AllowFilter = False
    Me.ui_ruolo.Visible = True
    Me.ui_ruolo.VisibleIndex = 3
    Me.ui_ruolo.Width = 99
    '
    'ui_opnome
    '
    Me.ui_opnome.AppearanceCell.Options.UseBackColor = True
    Me.ui_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.ui_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_opnome.Caption = "Operatore"
    Me.ui_opnome.Enabled = True
    Me.ui_opnome.FieldName = "ui_opnome"
    Me.ui_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_opnome.Name = "ui_opnome"
    Me.ui_opnome.NTSRepositoryComboBox = Nothing
    Me.ui_opnome.NTSRepositoryItemCheck = Nothing
    Me.ui_opnome.NTSRepositoryItemMemo = Nothing
    Me.ui_opnome.NTSRepositoryItemText = Nothing
    Me.ui_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_opnome.OptionsFilter.AllowFilter = False
    Me.ui_opnome.Visible = True
    Me.ui_opnome.VisibleIndex = 4
    Me.ui_opnome.Width = 101
    '
    'ui_nomprop
    '
    Me.ui_nomprop.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.ui_nomprop.AppearanceCell.Options.UseBackColor = True
    Me.ui_nomprop.AppearanceCell.Options.UseTextOptions = True
    Me.ui_nomprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_nomprop.Caption = "Nome proprietà"
    Me.ui_nomprop.Enabled = False
    Me.ui_nomprop.FieldName = "ui_nomprop"
    Me.ui_nomprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_nomprop.Name = "ui_nomprop"
    Me.ui_nomprop.NTSRepositoryComboBox = Nothing
    Me.ui_nomprop.NTSRepositoryItemCheck = Nothing
    Me.ui_nomprop.NTSRepositoryItemMemo = Nothing
    Me.ui_nomprop.NTSRepositoryItemText = Nothing
    Me.ui_nomprop.OptionsColumn.AllowEdit = False
    Me.ui_nomprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_nomprop.OptionsColumn.ReadOnly = True
    Me.ui_nomprop.OptionsFilter.AllowFilter = False
    Me.ui_nomprop.Visible = True
    Me.ui_nomprop.VisibleIndex = 5
    Me.ui_nomprop.Width = 101
    '
    'ui_valprop
    '
    Me.ui_valprop.AppearanceCell.Options.UseBackColor = True
    Me.ui_valprop.AppearanceCell.Options.UseTextOptions = True
    Me.ui_valprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_valprop.Caption = "Valore"
    Me.ui_valprop.Enabled = True
    Me.ui_valprop.FieldName = "ui_valprop"
    Me.ui_valprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_valprop.Name = "ui_valprop"
    Me.ui_valprop.NTSRepositoryComboBox = Nothing
    Me.ui_valprop.NTSRepositoryItemCheck = Nothing
    Me.ui_valprop.NTSRepositoryItemMemo = Nothing
    Me.ui_valprop.NTSRepositoryItemText = Nothing
    Me.ui_valprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_valprop.OptionsFilter.AllowFilter = False
    Me.ui_valprop.Visible = True
    Me.ui_valprop.VisibleIndex = 6
    '
    'ui_usascript
    '
    Me.ui_usascript.AppearanceCell.Options.UseBackColor = True
    Me.ui_usascript.AppearanceCell.Options.UseTextOptions = True
    Me.ui_usascript.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_usascript.Caption = "Usa Script"
    Me.ui_usascript.Enabled = True
    Me.ui_usascript.FieldName = "ui_usascript"
    Me.ui_usascript.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_usascript.Name = "ui_usascript"
    Me.ui_usascript.NTSRepositoryComboBox = Nothing
    Me.ui_usascript.NTSRepositoryItemCheck = Nothing
    Me.ui_usascript.NTSRepositoryItemMemo = Nothing
    Me.ui_usascript.NTSRepositoryItemText = Nothing
    Me.ui_usascript.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_usascript.OptionsFilter.AllowFilter = False
    Me.ui_usascript.Visible = True
    Me.ui_usascript.VisibleIndex = 7
    Me.ui_usascript.Width = 50
    '
    'ui_script
    '
    Me.ui_script.AppearanceCell.Options.UseBackColor = True
    Me.ui_script.AppearanceCell.Options.UseTextOptions = True
    Me.ui_script.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_script.Caption = "Script"
    Me.ui_script.Enabled = True
    Me.ui_script.FieldName = "ui_script"
    Me.ui_script.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_script.Name = "ui_script"
    Me.ui_script.NTSRepositoryComboBox = Nothing
    Me.ui_script.NTSRepositoryItemCheck = Nothing
    Me.ui_script.NTSRepositoryItemMemo = Nothing
    Me.ui_script.NTSRepositoryItemText = Nothing
    Me.ui_script.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_script.OptionsFilter.AllowFilter = False
    Me.ui_script.Visible = True
    Me.ui_script.VisibleIndex = 8
    Me.ui_script.Width = 80
    '
    'ui_gridcol
    '
    Me.ui_gridcol.AppearanceCell.Options.UseBackColor = True
    Me.ui_gridcol.AppearanceCell.Options.UseTextOptions = True
    Me.ui_gridcol.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_gridcol.Caption = "Colonna griglia"
    Me.ui_gridcol.Enabled = False
    Me.ui_gridcol.FieldName = "ui_gridcol"
    Me.ui_gridcol.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_gridcol.Name = "ui_gridcol"
    Me.ui_gridcol.NTSRepositoryComboBox = Nothing
    Me.ui_gridcol.NTSRepositoryItemCheck = Nothing
    Me.ui_gridcol.NTSRepositoryItemMemo = Nothing
    Me.ui_gridcol.NTSRepositoryItemText = Nothing
    Me.ui_gridcol.OptionsColumn.AllowEdit = False
    Me.ui_gridcol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_gridcol.OptionsColumn.ReadOnly = True
    Me.ui_gridcol.OptionsFilter.AllowFilter = False
    '
    'ui_comboitem
    '
    Me.ui_comboitem.AppearanceCell.Options.UseBackColor = True
    Me.ui_comboitem.AppearanceCell.Options.UseTextOptions = True
    Me.ui_comboitem.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_comboitem.Caption = "Combo Item"
    Me.ui_comboitem.Enabled = False
    Me.ui_comboitem.FieldName = "ui_comboitem"
    Me.ui_comboitem.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_comboitem.Name = "ui_comboitem"
    Me.ui_comboitem.NTSRepositoryComboBox = Nothing
    Me.ui_comboitem.NTSRepositoryItemCheck = Nothing
    Me.ui_comboitem.NTSRepositoryItemMemo = Nothing
    Me.ui_comboitem.NTSRepositoryItemText = Nothing
    Me.ui_comboitem.OptionsColumn.AllowEdit = False
    Me.ui_comboitem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_comboitem.OptionsColumn.ReadOnly = True
    Me.ui_comboitem.OptionsFilter.AllowFilter = False
    '
    'ui_codling
    '
    Me.ui_codling.AppearanceCell.Options.UseBackColor = True
    Me.ui_codling.AppearanceCell.Options.UseTextOptions = True
    Me.ui_codling.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_codling.Caption = "Lingua"
    Me.ui_codling.Enabled = False
    Me.ui_codling.FieldName = "ui_codling"
    Me.ui_codling.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_codling.Name = "ui_codling"
    Me.ui_codling.NTSRepositoryComboBox = Nothing
    Me.ui_codling.NTSRepositoryItemCheck = Nothing
    Me.ui_codling.NTSRepositoryItemMemo = Nothing
    Me.ui_codling.NTSRepositoryItemText = Nothing
    Me.ui_codling.OptionsColumn.AllowEdit = False
    Me.ui_codling.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_codling.OptionsColumn.ReadOnly = True
    Me.ui_codling.OptionsFilter.AllowFilter = False
    '
    'ui_ctrltype
    '
    Me.ui_ctrltype.AppearanceCell.Options.UseBackColor = True
    Me.ui_ctrltype.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ctrltype.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ctrltype.Caption = "Tipo controllo"
    Me.ui_ctrltype.Enabled = False
    Me.ui_ctrltype.FieldName = "ui_ctrltype"
    Me.ui_ctrltype.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ctrltype.Name = "ui_ctrltype"
    Me.ui_ctrltype.NTSRepositoryComboBox = Nothing
    Me.ui_ctrltype.NTSRepositoryItemCheck = Nothing
    Me.ui_ctrltype.NTSRepositoryItemMemo = Nothing
    Me.ui_ctrltype.NTSRepositoryItemText = Nothing
    Me.ui_ctrltype.OptionsColumn.AllowEdit = False
    Me.ui_ctrltype.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ctrltype.OptionsColumn.ReadOnly = True
    Me.ui_ctrltype.OptionsFilter.AllowFilter = False
    '
    'ui_child
    '
    Me.ui_child.AppearanceCell.Options.UseBackColor = True
    Me.ui_child.AppearanceCell.Options.UseTextOptions = True
    Me.ui_child.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_child.Caption = "Child"
    Me.ui_child.Enabled = False
    Me.ui_child.FieldName = "ui_child"
    Me.ui_child.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_child.Name = "ui_child"
    Me.ui_child.NTSRepositoryComboBox = Nothing
    Me.ui_child.NTSRepositoryItemCheck = Nothing
    Me.ui_child.NTSRepositoryItemMemo = Nothing
    Me.ui_child.NTSRepositoryItemText = Nothing
    Me.ui_child.OptionsColumn.AllowEdit = False
    Me.ui_child.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_child.OptionsColumn.ReadOnly = True
    Me.ui_child.OptionsFilter.AllowFilter = False
    '
    'ui_form
    '
    Me.ui_form.AppearanceCell.Options.UseBackColor = True
    Me.ui_form.AppearanceCell.Options.UseTextOptions = True
    Me.ui_form.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_form.Caption = "Form"
    Me.ui_form.Enabled = False
    Me.ui_form.FieldName = "ui_form"
    Me.ui_form.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_form.Name = "ui_form"
    Me.ui_form.NTSRepositoryComboBox = Nothing
    Me.ui_form.NTSRepositoryItemCheck = Nothing
    Me.ui_form.NTSRepositoryItemMemo = Nothing
    Me.ui_form.NTSRepositoryItemText = Nothing
    Me.ui_form.OptionsColumn.AllowEdit = False
    Me.ui_form.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_form.OptionsColumn.ReadOnly = True
    Me.ui_form.OptionsFilter.AllowFilter = False
    '
    'ui_ctrlname
    '
    Me.ui_ctrlname.AppearanceCell.Options.UseBackColor = True
    Me.ui_ctrlname.AppearanceCell.Options.UseTextOptions = True
    Me.ui_ctrlname.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ui_ctrlname.Caption = "Nome controllo"
    Me.ui_ctrlname.Enabled = False
    Me.ui_ctrlname.FieldName = "ui_ctrlname"
    Me.ui_ctrlname.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ui_ctrlname.Name = "ui_ctrlname"
    Me.ui_ctrlname.NTSRepositoryComboBox = Nothing
    Me.ui_ctrlname.NTSRepositoryItemCheck = Nothing
    Me.ui_ctrlname.NTSRepositoryItemMemo = Nothing
    Me.ui_ctrlname.NTSRepositoryItemText = Nothing
    Me.ui_ctrlname.OptionsColumn.AllowEdit = False
    Me.ui_ctrlname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ui_ctrlname.OptionsColumn.ReadOnly = True
    Me.ui_ctrlname.OptionsFilter.AllowFilter = False
    '
    'cmdRipristina
    '
    Me.cmdRipristina.ImageText = ""
    Me.cmdRipristina.Location = New System.Drawing.Point(534, 70)
    Me.cmdRipristina.Name = "cmdRipristina"
    Me.cmdRipristina.Size = New System.Drawing.Size(20, 19)
    Me.cmdRipristina.TabIndex = 7
    Me.cmdRipristina.TabStop = False
    Me.cmdRipristina.Text = "Ripristina riga"
    Me.cmdRipristina.Visible = False
    '
    'cmdCancRow
    '
    Me.cmdCancRow.ImageText = ""
    Me.cmdCancRow.Location = New System.Drawing.Point(560, 68)
    Me.cmdCancRow.Name = "cmdCancRow"
    Me.cmdCancRow.Size = New System.Drawing.Size(76, 24)
    Me.cmdCancRow.TabIndex = 8
    Me.cmdCancRow.TabStop = False
    Me.cmdCancRow.Text = "Cancella riga"
    '
    'lbTipoDoc
    '
    Me.lbTipoDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbTipoDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTipoDoc.Location = New System.Drawing.Point(252, 9)
    Me.lbTipoDoc.Name = "lbTipoDoc"
    Me.lbTipoDoc.NTSDbField = ""
    Me.lbTipoDoc.Size = New System.Drawing.Size(93, 25)
    Me.lbTipoDoc.TabIndex = 1
    Me.lbTipoDoc.Text = "Tipo doc.: "
    Me.lbTipoDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbTipoDoc.Tooltip = ""
    Me.lbTipoDoc.UseMnemonic = False
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbNota.Location = New System.Drawing.Point(10, 506)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(351, 12)
    Me.lbNota.TabIndex = 9
    Me.lbNota.Text = "* Si ricorda che per LABEL collegate a tabelle non è possibile settare la proprie" & _
        "tà 'Text'"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'cmdRemoting
    '
    Me.cmdRemoting.ImageText = ""
    Me.cmdRemoting.Location = New System.Drawing.Point(462, 68)
    Me.cmdRemoting.Name = "cmdRemoting"
    Me.cmdRemoting.Size = New System.Drawing.Size(74, 20)
    Me.cmdRemoting.TabIndex = 12
    Me.cmdRemoting.Text = "&Remoting"
    Me.cmdRemoting.Visible = False
    '
    'cbControl
    '
    Me.cbControl.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbControl.DataSource = Nothing
    Me.cbControl.DisplayMember = ""
    Me.cbControl.Location = New System.Drawing.Point(90, 9)
    Me.cbControl.Name = "cbControl"
    Me.cbControl.NTSDbField = ""
    Me.cbControl.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbControl.Properties.Appearance.Options.UseBackColor = True
    Me.cbControl.Properties.AutoHeight = False
    Me.cbControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbControl.Properties.DropDownRows = 30
    Me.cbControl.Properties.Sorted = True
    Me.cbControl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbControl.SelectedValue = ""
    Me.cbControl.Size = New System.Drawing.Size(153, 20)
    Me.cbControl.TabIndex = 13
    Me.cbControl.TabStop = False
    Me.cbControl.ValueMember = ""
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel1.Location = New System.Drawing.Point(10, 518)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(472, 12)
    Me.NtsLabel1.TabIndex = 14
    Me.NtsLabel1.Text = "Le proprietà VISIBLE/ENABLE non operano se 'da codice' gli oggetti sono stati dic" & _
        "hiarati 'non visibili' o 'non editabili'"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'lbLingua
    '
    Me.lbLingua.AutoSize = True
    Me.lbLingua.BackColor = System.Drawing.Color.Transparent
    Me.lbLingua.Location = New System.Drawing.Point(353, 45)
    Me.lbLingua.Name = "lbLingua"
    Me.lbLingua.NTSDbField = ""
    Me.lbLingua.Size = New System.Drawing.Size(55, 13)
    Me.lbLingua.TabIndex = 15
    Me.lbLingua.Text = "(3) Lingua"
    Me.lbLingua.Tooltip = ""
    Me.lbLingua.UseMnemonic = False
    '
    'lbGriglia
    '
    Me.lbGriglia.AutoSize = True
    Me.lbGriglia.BackColor = System.Drawing.Color.Transparent
    Me.lbGriglia.Location = New System.Drawing.Point(9, 45)
    Me.lbGriglia.Name = "lbGriglia"
    Me.lbGriglia.NTSDbField = ""
    Me.lbGriglia.Size = New System.Drawing.Size(53, 13)
    Me.lbGriglia.TabIndex = 16
    Me.lbGriglia.Text = "(1) Griglia"
    Me.lbGriglia.Tooltip = ""
    Me.lbGriglia.UseMnemonic = False
    '
    'lbCombo
    '
    Me.lbCombo.AutoSize = True
    Me.lbCombo.BackColor = System.Drawing.Color.Transparent
    Me.lbCombo.Location = New System.Drawing.Point(9, 71)
    Me.lbCombo.Name = "lbCombo"
    Me.lbCombo.NTSDbField = ""
    Me.lbCombo.Size = New System.Drawing.Size(68, 13)
    Me.lbCombo.TabIndex = 17
    Me.lbCombo.Text = "(2) Elemento"
    Me.lbCombo.Tooltip = ""
    Me.lbCombo.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel2.Location = New System.Drawing.Point(10, 530)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(516, 12)
    Me.NtsLabel2.TabIndex = 18
    Me.NtsLabel2.Text = "OUT NOT EQUAL: per campi di tipo STRINGA o DATA se il valore deve essere diverso " & _
        "da '' indicare nella colonna valore 'null'"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'cmdFastVisible
    '
    Me.cmdFastVisible.ImageText = ""
    Me.cmdFastVisible.Location = New System.Drawing.Point(534, 524)
    Me.cmdFastVisible.Name = "cmdFastVisible"
    Me.cmdFastVisible.Size = New System.Drawing.Size(102, 26)
    Me.cmdFastVisible.TabIndex = 19
    Me.cmdFastVisible.Text = "Nascondi massivo"
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel3.Location = New System.Drawing.Point(10, 542)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(485, 12)
    Me.NtsLabel3.TabIndex = 20
    Me.NtsLabel3.Text = "TEXT settata su operatori con lingua diversa da Italiano: il contenuto del file ." & _
        "INT ha priorità rispetto al valore settato qui"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'FRM__GCTL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 563)
    Me.Controls.Add(Me.NtsLabel3)
    Me.Controls.Add(Me.cmdFastVisible)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.lbCombo)
    Me.Controls.Add(Me.lbGriglia)
    Me.Controls.Add(Me.lbLingua)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Controls.Add(Me.cbControl)
    Me.Controls.Add(Me.lbControl)
    Me.Controls.Add(Me.cmdRemoting)
    Me.Controls.Add(Me.lbNota)
    Me.Controls.Add(Me.cmdRipristina)
    Me.Controls.Add(Me.lbTipoDoc)
    Me.Controls.Add(Me.cmdCancRow)
    Me.Controls.Add(Me.NtsGroupBox3)
    Me.Controls.Add(Me.NtsGroupBox2)
    Me.Controls.Add(Me.NtsGroupBox1)
    Me.Controls.Add(Me.cbLingua)
    Me.Controls.Add(Me.cbGriglia)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.cbCombo)
    Me.Controls.Add(Me.lbTipoControllo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "FRM__GCTL"
    Me.NTSLastControlFocussed = Me.grUI
    Me.Text = "GESTIONE SICUREZZA / CONFIGURAZIONE ACCESSI"
    CType(Me.cbCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbGriglia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbLingua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.optErrorText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optInsert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optChecked.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optOutnotequal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDefault.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optFormatnumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optEnable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optBold.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optVisible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    Me.NtsGroupBox2.PerformLayout()
    CType(Me.optPrior13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox3.ResumeLayout(False)
    CType(Me.grUI, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvUI, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub CaricaCombo(ByVal nCombo As Integer, ByVal ctlCombo As NTSComboBox)
    '0 = combo della lingua
    '1 = combo della griglia
    '2 = combo degli item di un combobox o di un listbox, di un tabcontrol
    Dim dttTable As New DataTable()
    Dim dttTmp As DataTable
    Dim i As Integer = 0
    Dim dsLingue As New DataSet

    Try
      dttTable.Columns.Add("cod", GetType(String))
      dttTable.Columns.Add("val", GetType(String))

      '0 = combo della lingua
      If nCombo = 0 Then
        oCleGctl.GetLingue(dsLingue)
        If dsLingue.Tables("TABLING").Select("tb_codling = 0").Length = 0 Then
          dsLingue.Tables("TABLING").Rows.Add(dsLingue.Tables("TABLING").NewRow())
          dsLingue.Tables("TABLING").Rows(dsLingue.Tables("TABLING").Rows.Count - 1)!tb_codling = 0
          dsLingue.Tables("TABLING").Rows(dsLingue.Tables("TABLING").Rows.Count - 1)!tb_desling = "Italiano"
        End If
        dsLingue.Tables("TABLING").AcceptChanges()
        If Not ctlCombo.DataSource Is Nothing Then ctlCombo.DataSource = Nothing
        ctlCombo.Properties.Items.Clear()
        ctlCombo.DataSource = dsLingue.Tables("TABLING")
        ctlCombo.DisplayMember = "tb_desling"
        ctlCombo.ValueMember = "tb_codling"
        ctlCombo.SelectedValue = "0"
      End If

      '1 = combo della griglia
      If nCombo = 1 Then
        dttTable.Rows.Add(New Object() {".", "Generale"})
        If ctlGest.GetType.Equals(GetType(NTSGridView)) Then
          For i = 0 To CType(ctlGest, NTSGridView).Columns.Count - 1
            If CType(ctlGest, NTSGridView).Columns(i).Caption <> "" Then
              dttTable.Rows.Add(New Object() {CType(ctlGest, NTSGridView).Columns(i).Name, CType(ctlGest, NTSGridView).Columns(i).Caption})
            End If
          Next
        End If
        dttTable.AcceptChanges()
        If Not ctlCombo.DataSource Is Nothing Then ctlCombo.DataSource = Nothing
        ctlCombo.Properties.Items.Clear()
        ctlCombo.DataSource = dttTable
        ctlCombo.DisplayMember = "val"
        ctlCombo.ValueMember = "cod"
        ctlCombo.SelectedValue = "."
      End If

      '2 = combo degli item di un combobox/listbox, oppure voci di menu, oppure bottoni della toolbar
      If nCombo = 2 Then

        If ctlGest.GetType.Equals(GetType(NTSComboBox)) Then
          Dim cbTmp As NTSComboBox = CType(ctlGest, NTSComboBox)
          dttTmp = CType(cbTmp.DataSource, DataTable).Copy
          dttTmp.RejectChanges()  'nel combo poteri avere delle voci cancellate a causa di una precedente impostazione di non visibilità: così prendo sua anche le righe non visibili nel combo
          Dim dtrT() As DataRow
          dtrT = dttTmp.Select(Nothing, Nothing, DataViewRowState.OriginalRows)

          dttTable.Rows.Add(dttTable.NewRow())
          dttTable.Rows(dttTable.Rows.Count - 1)!cod = "."
          dttTable.Rows(dttTable.Rows.Count - 1)!val = "Generale"
          For i = 0 To dtrT.Length - 1
            dttTable.Rows.Add(dttTable.NewRow())
            dttTable.Rows(dttTable.Rows.Count - 1)!cod = IIf(dtrT(i)!cod.ToString = " ", "§", dtrT(i)!cod)
            dttTable.Rows(dttTable.Rows.Count - 1)!val = dtrT(i)!val.ToString & " ('" & dtrT(i)!cod.ToString & "')"
          Next

        ElseIf ctlGest.GetType.Equals(GetType(NTSListBox)) Then
          Dim liTmp As NTSListBox = CType(ctlGest, NTSListBox)
          dttTmp = CType(liTmp.DataSource, DataTable).Copy
          dttTmp.RejectChanges()  'nel combo poteri avere delle voci cancellate a causa di una precedente impostazione di non visibilità: così prendo sua anche le righe non visibili nel combo
          Dim dtrT() As DataRow
          dtrT = dttTmp.Select(Nothing, Nothing, DataViewRowState.OriginalRows)

          dttTable.Rows.Add(dttTable.NewRow())
          dttTable.Rows(dttTable.Rows.Count - 1)!cod = "."
          dttTable.Rows(dttTable.Rows.Count - 1)!val = "Generale"
          For i = 0 To dtrT.Length - 1
            dttTable.Rows.Add(dttTable.NewRow())
            dttTable.Rows(dttTable.Rows.Count - 1)!cod = IIf(dtrT(i)!cod.ToString = " ", "§", dtrT(i)!cod)
            dttTable.Rows(dttTable.Rows.Count - 1)!val = dtrT(i)!val.ToString & " ('" & dtrT(i)!cod.ToString & "')"
          Next

        ElseIf ctlGest.GetType.Equals(GetType(NTSTabControl)) Then
          dttTable.Rows.Add(dttTable.NewRow())
          dttTable.Rows(dttTable.Rows.Count - 1)!cod = "."
          dttTable.Rows(dttTable.Rows.Count - 1)!val = "Generale"
          Dim oTmp As NTSTabControl = CType(ctlGest, NTSTabControl)
          For i = 0 To oTmp.TabPages.Count - 1
            If NTSCStr(oTmp.TabPages(i).Tag) <> "V" Then
              dttTable.Rows.Add(dttTable.NewRow())
              dttTable.Rows(dttTable.Rows.Count - 1)!cod = oTmp.NTSTabPages(i).Name
              dttTable.Rows(dttTable.Rows.Count - 1)!val = oTmp.NTSTabPages(i).Text
            End If
          Next

        Else
          dttTable.Rows.Add(dttTable.NewRow())
          dttTable.Rows(dttTable.Rows.Count - 1)!cod = "."
          dttTable.Rows(dttTable.Rows.Count - 1)!val = "Generale"
        End If

        dttTable.AcceptChanges()
        If Not ctlCombo.DataSource Is Nothing Then ctlCombo.DataSource = Nothing
        ctlCombo.DataSource = dttTable
        ctlCombo.DisplayMember = "val"
        ctlCombo.ValueMember = "cod"
        ctlCombo.SelectedValue = "."
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiMenu(ByRef oBar As NTSBar, ByRef oSubMenu As DevExpress.XtraBars.BarSubItemLink) As String
    'dato un menu restituisce tutti i nomi dei sottomenu separati da ','
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim strOut As String = ""
    Try
      If Not oBar Is Nothing Then
        For i = 0 To oBar.ItemLinks.Count - 1
          strOut = strOut & oBar.ItemLinks(i).Item.Name.Trim & ","
          If oBar.ItemLinks(i).GetType.ToString.IndexOf("BarSubItemLink") > -1 Then
            strOut = strOut & LeggiMenu(Nothing, CType(oBar.ItemLinks(i), DevExpress.XtraBars.BarSubItemLink))
          End If
        Next
      Else
        For i = 0 To oSubMenu.Links.Count - 1
          If oSubMenu.Links(i).GetType.ToString.IndexOf("BarSubItemLink") > -1 Then
            strOut = strOut & LeggiMenu(Nothing, CType(oSubMenu.Links(i), DevExpress.XtraBars.BarSubItemLink))
          Else
            strOut = strOut & oSubMenu.Links(i).Item.Name.Trim & ","
          End If
        Next
      End If
      Return strOut
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub cbLingua_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLingua.SelectedIndexChanged
    Try
      If bInit Then Return

      Abilitaproprietà()
      Evidenziacontrolli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbGriglia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGriglia.SelectedIndexChanged
    If bInit Then Return

    Dim strGricol As String = "."
    Dim dttTable As New DataTable
    Dim i As Integer = 0
    Try
      dttTable.Columns.Add("cod", GetType(String))
      dttTable.Columns.Add("val", GetType(String))
      dttTable.Rows.Add(dttTable.NewRow())
      dttTable.Rows(dttTable.Rows.Count - 1)!cod = "."
      dttTable.Rows(dttTable.Rows.Count - 1)!val = "Generale"

      'se la colonna è di tipo combobox devo compilare anche il combo, altrimenti devo vuotarlo
      If ctlGest.GetType.Equals(GetType(NTSGridView)) Then
        If CType(ctlGest, NTSGridView).Columns.Count <> 0 Then
          If cbGriglia.SelectedValue Is Nothing Then
            strGricol = "."
          Else
            strGricol = cbGriglia.SelectedValue.ToString.ToLower
          End If

          If Not CType(ctlGest, NTSGridView).Columns(strGricol) Is Nothing Then
            If CType(CType(ctlGest, NTSGridView).Columns(strGricol), NTSGridColumn).NTSTipoColonna = "B" Then
              'colonna COMBO
              cbCombo.DataSource = Nothing
              cbCombo.Properties.Items.Clear()
              cbCombo.Enabled = True
              Dim dttTmp As New DataTable()
              dttTable.AcceptChanges()

              'se la colonna non è collegata ad un datatable non faccio vedere nulla (è il cso, ad esempio, di BNORGSOR, colonna UM)
              dttTmp.Clear()
              Try
                dttTmp = CType(CType(CType(CType(ctlGest, NTSGridView).Columns(strGricol), NTSGridColumn).ColumnEdit, NTSRepositoryItemComboBox).DataSource, DataTable).Copy
                dttTmp.RejectChanges()  'nel combo poteri avere delle voci cancellate a causa di una precedente impostazione di non visibilità: così prendo sua anche le righe non visibili nel combo
              Catch ex As Exception
              End Try
              Dim dtrT() As DataRow
              If dttTmp.Columns.Contains("cod") And dttTmp.Columns.Contains("val") Then
                'dava errore sulle colonne unmis di veboll/gsor
                dtrT = dttTmp.Select(Nothing, Nothing, DataViewRowState.OriginalRows)
                For i = 0 To dtrT.Length - 1
                  dttTable.Rows.Add(dttTable.NewRow())
                  dttTable.Rows(dttTable.Rows.Count - 1)!cod = dtrT(i)!cod
                  dttTable.Rows(dttTable.Rows.Count - 1)!val = dtrT(i)!val.ToString & " ('" & dtrT(i)!cod.ToString & "')"
                Next
              End If
              cbCombo.Properties.Items.Clear()
              cbCombo.DataSource = dttTable
              cbCombo.DisplayMember = "val"
              cbCombo.ValueMember = "cod"
              cbCombo.SelectedValue = "."
            Else
              'solo 'GENERALE'
              cbCombo.DataSource = Nothing
              cbCombo.Properties.Items.Clear()
              cbCombo.DataSource = dttTable
              cbCombo.DisplayMember = "val"
              cbCombo.ValueMember = "cod"
              cbCombo.Enabled = False
            End If
          Else
            'solo 'GENERALE'
            cbCombo.DataSource = Nothing
            cbCombo.Properties.Items.Clear()
            cbCombo.DataSource = dttTable
            cbCombo.DisplayMember = "val"
            cbCombo.ValueMember = "cod"
            cbCombo.Enabled = False
          End If

        End If    'If CType(ctlGest, NTSGridView).Columns.Count <> 0 Then
      End If    'If ctlGest.GetType.Equals(GetType(NTSGridView)) Then

      Abilitaproprietà()
      Evidenziacontrolli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCombo.SelectedIndexChanged
    Try
      If bInit Then Return

      Abilitaproprietà()
      Evidenziacontrolli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di griglia"
  Public Overridable Sub grUi_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grUI.Enter
    Try
      If optVisible.Checked = False And optEnable.Checked = False And optBold.Checked = False And _
         optDefault.Checked = False And optFormatnumber.Checked = False And optOutnotequal.Checked = False And _
         optChecked.Checked = False And optText.Checked = False And optErrorText.Checked = False And _
         optInsert.Checked = False And optUpdate.Checked = False And optDelete.Checked = False Then
        If True Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129019105329794922, "Prima di aggiungere righe nella griglia impostare la PROPRIETA'"))
          cmdConferma.Focus()
          Return
        End If
      End If

      If optPrior1.Checked = False And optPrior2.Checked = False And optPrior3.Checked = False And _
         optPrior4.Checked = False And optPrior5.Checked = False And optPrior6.Checked = False And _
         optPrior7.Checked = False And optPrior8.Checked = False And optPrior9.Checked = False And _
         optPrior10.Checked = False And optPrior11.Checked = False And optPrior12.Checked = False And _
         optPrior13.Checked = False Then
        If True Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129019103025566407, "Prima di aggiungere righe nella griglia impostare la DIPENDENZA"))
          cmdConferma.Focus()
          Return
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvUI_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvUI.NTSBeforeRowUpdate
    Try
      If Not Salva(False) Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Abilitaproprietà()
    Try
      '-------------------------------------------------
      'in base al tipo controllo, lingua, colonna, item abilita le proprietà gestite per quel caso
      optVisible.Enabled = True
      optEnable.Enabled = True
      optBold.Enabled = True
      optDefault.Enabled = True
      optFormatnumber.Enabled = True
      optOutnotequal.Enabled = True
      optChecked.Enabled = True
      optText.Enabled = True
      optErrorText.Enabled = True
      optInsert.Enabled = False
      optUpdate.Enabled = False
      optDelete.Enabled = False

      optVisible.ForeColor = lbTipoControllo.ForeColor
      optEnable.ForeColor = lbTipoControllo.ForeColor
      optBold.ForeColor = lbTipoControllo.ForeColor
      optDefault.ForeColor = lbTipoControllo.ForeColor
      optFormatnumber.ForeColor = lbTipoControllo.ForeColor
      optOutnotequal.ForeColor = lbTipoControllo.ForeColor
      optChecked.ForeColor = lbTipoControllo.ForeColor
      optText.ForeColor = lbTipoControllo.ForeColor
      optErrorText.ForeColor = lbTipoControllo.ForeColor

      TogliCheck(False)
      GestisciGriglia(True, 0)

      Select Case lbTipoControllo.Text.Substring(6)
        Case "NTSInformatica.NTSTextBoxStr"
          optFormatnumber.Enabled = False
          optChecked.Enabled = False
          optText.Enabled = False
        Case "NTSInformatica.NTSTextBoxNum"
          optChecked.Enabled = False
          optText.Enabled = False
        Case "NTSInformatica.NTSTextBoxData"
          optFormatnumber.Enabled = False
          optChecked.Enabled = False
          optText.Enabled = False
        Case "NTSInformatica.NTSMemoBox"
          optFormatnumber.Enabled = False
          optChecked.Enabled = False
          optText.Enabled = False
        Case "NTSInformatica.NTSRadioButton"
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSComboBox"
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          If cbCombo.Properties.Items.Count > 1 Then
            If cbCombo.SelectedValue.ToString = "." Then
              optText.Enabled = False
              optOutnotequal.Enabled = True
            Else
              optEnable.Enabled = False
              optBold.Enabled = False
              optDefault.Enabled = False
              optErrorText.Enabled = False
            End If
          Else
            optEnable.Enabled = False
            optBold.Enabled = False
            optDefault.Enabled = False
            optErrorText.Enabled = False
          End If
        Case "NTSInformatica.NTSListBox"
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = True
          optChecked.Enabled = False
          optDefault.Enabled = False
          If cbCombo.Properties.Items.Count > 1 Then
            If cbCombo.SelectedValue.ToString = "." Then
              optText.Enabled = False
              optDefault.Enabled = True
            Else
              optEnable.Enabled = False
              optBold.Enabled = False
              optErrorText.Enabled = False
            End If
          Else
            optEnable.Enabled = False
            optBold.Enabled = False
            optText.Enabled = False
            optErrorText.Enabled = False
          End If
        Case "NTSInformatica.NTSCheckBox"
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = True
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSLabel"
          optEnable.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
          '---------------------------------
          'label collegate a database: non posso settare la text
          If CType(ctlGest, Control).DataBindings.Count > 0 Then
            '.Show("Si ricorda che per le 'NTSLabel' collegate a tabelle non è possibile settare la proprietà 'Text'", oApp.MSGTTL, MessageBoxButtons.OK, MessageBoxIcon.Information)
            lbNota.ForeColor = Color.Red
            optText.Enabled = False
          End If
        Case "NTSInformatica.NTSButton"
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSGroupBox"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSPanel"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optText.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSBarButtonItem"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSBarMenuItem"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSBarSubItem"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSPictureBox"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optText.Enabled = False
          optErrorText.Enabled = False
        Case "NTSInformatica.NTSTabControl"
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
          optErrorText.Enabled = False
          If cbCombo.Properties.Items.Count > 1 Then
            If cbCombo.SelectedValue.ToString = "." Then
              optText.Enabled = False
              optEnable.Enabled = False
            End If
          Else
            optText.Enabled = False
            optEnable.Enabled = False
          End If
        Case "NTSInformatica.NTSGridView"
          optErrorText.Enabled = False
          If cbGriglia.SelectedValue.ToString = "." Then
            optBold.Enabled = False
            optDefault.Enabled = False
            optFormatnumber.Enabled = False
            optOutnotequal.Enabled = False
            optChecked.Enabled = False
            optText.Enabled = False

            optInsert.Enabled = True
            optUpdate.Enabled = True
            optDelete.Enabled = True
          Else

            If cbCombo.SelectedValue.ToString = "." Then
              optEnable.Enabled = True
              optBold.Enabled = True
              optDefault.Enabled = True
              optOutnotequal.Enabled = True
            Else
              optEnable.Enabled = False
              optBold.Enabled = False
              optDefault.Enabled = False
              optOutnotequal.Enabled = False
            End If

            optFormatnumber.Enabled = False
            optChecked.Enabled = False
            Select Case CType(CType(ctlGest, NTSGridView).Columns(cbGriglia.SelectedValue.ToString.ToLower), NTSGridColumn).NTSTipoColonna
              Case "N"    'colonna numerica
                optFormatnumber.Enabled = True
              Case "S"    'colonna stringa
              Case "M"    'colonna stringa memo
              Case "D"    'colonna data
              Case "C"    'colonna checkbox
                optDefault.Enabled = False
                optChecked.Enabled = True
              Case "B"    'colonna combobobx
            End Select
          End If
        Case Else
          If lbTipoControllo.Text.Substring(6).IndexOf("NTSInformatica.FRM") >= 0 Then
            optVisible.Enabled = False
            optEnable.Enabled = False
            optBold.Enabled = False
            optDefault.Enabled = False
            optFormatnumber.Enabled = False
            optOutnotequal.Enabled = False
            optChecked.Enabled = False
            optErrorText.Enabled = False
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142031250, "BN__GCTL -> Abilitaproprietà: Tipo di controllo '|" & lbTipoControllo.Text.Substring(6) & "|' non attualmente gestito"))
          End If
      End Select

      '-------------------------------------------------
      'SE HO IMPOSTATO UNA LINGUA DIVERSA DA ITALIANO DISABILITO TUTTI I CONTROLLI ECCETTO Text e ErrorText
      If Not cbLingua.SelectedValue Is Nothing Then
        If cbLingua.SelectedValue.ToString <> "0" Then
          optVisible.Enabled = False
          optEnable.Enabled = False
          optBold.Enabled = False
          optDefault.Enabled = False
          optFormatnumber.Enabled = False
          optOutnotequal.Enabled = False
          optChecked.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Evidenziacontrolli()
    Dim dtrTmp() As DataRow = Nothing
    Dim strFilter As String = ""
    Dim strGricol As String = "."
    Dim strComboCol As String = "."

    Try
      If dsUi Is Nothing Then Return
      If dsUi.Tables.Count = 0 Then Return
      If dsUi.Tables("UICONF").Rows.Count = 0 Then Return

      If Not cbGriglia.SelectedValue Is Nothing Then strGricol = cbGriglia.SelectedValue.ToString
      If Not cbCombo.SelectedValue Is Nothing Then strComboCol = cbCombo.SelectedValue.ToString

      strFilter = "ui_gridcol = '" & strGricol & _
                  "' AND ui_comboitem = '" & strComboCol & "'" & _
                  " AND ui_codling = " & cbLingua.SelectedValue.ToString & " AND "

      '--------------------------------------
      'evidenzio il controllo se ho delle impostazioni per lui settate
      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'VISIBLE'")
      If dtrTmp.Length > 0 Then optVisible.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'ENABLE'")
      If dtrTmp.Length > 0 Then optEnable.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'BOLD'")
      If dtrTmp.Length > 0 Then optBold.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'DEFAULT'")
      If dtrTmp.Length > 0 Then optDefault.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'FORMAT'")
      If dtrTmp.Length > 0 Then optFormatnumber.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'OUTNOTEQUAL'")
      If dtrTmp.Length > 0 Then optOutnotequal.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'CHECKED'")
      If dtrTmp.Length > 0 Then optChecked.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'TEXT'")
      If dtrTmp.Length > 0 Then optText.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'ERRORTEXT'")
      If dtrTmp.Length > 0 Then optErrorText.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'ALLOWNEW'")
      If dtrTmp.Length > 0 Then optInsert.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'ALLOUPDATE'")
      If dtrTmp.Length > 0 Then optUpdate.ForeColor = Color.Red

      dtrTmp = dsUi.Tables("UICONF").Select(strFilter & "ui_nomprop = 'ALLOWDELETE'")
      If dtrTmp.Length > 0 Then optDelete.ForeColor = Color.Red

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub EvidenziaVincoli(ByVal strFilter As String)
    Dim dtrTmp() As DataRow = Nothing
    Dim i As Integer
    Dim bDb As Boolean = False
    Dim bDitta As Boolean = False
    Dim bGruppo As Boolean = False
    Dim bUtente As Boolean = False
    Dim bTipoDoc As Boolean = False
    Dim strGricol As String = "."
    Dim strComboCol As String = "."

    Try
      optPrior1.ForeColor = lbTipoControllo.ForeColor
      optPrior2.ForeColor = lbTipoControllo.ForeColor
      optPrior3.ForeColor = lbTipoControllo.ForeColor
      optPrior4.ForeColor = lbTipoControllo.ForeColor
      optPrior5.ForeColor = lbTipoControllo.ForeColor
      optPrior6.ForeColor = lbTipoControllo.ForeColor
      optPrior7.ForeColor = lbTipoControllo.ForeColor
      optPrior8.ForeColor = lbTipoControllo.ForeColor
      optPrior9.ForeColor = lbTipoControllo.ForeColor
      optPrior10.ForeColor = lbTipoControllo.ForeColor
      optPrior11.ForeColor = lbTipoControllo.ForeColor
      optPrior12.ForeColor = lbTipoControllo.ForeColor
      optPrior13.ForeColor = lbTipoControllo.ForeColor

      If dsUi.Tables.Count = 0 Then Return
      If dsUi.Tables("UICONF").Rows.Count = 0 Then Return


      If Not cbGriglia.SelectedValue Is Nothing Then strGricol = cbGriglia.SelectedValue.ToString
      If Not cbCombo.SelectedValue Is Nothing Then strComboCol = cbCombo.SelectedValue.ToString

      strFilter = strFilter & " AND ui_gridcol = '" & strGricol & _
                              "' AND ui_comboitem = '" & strComboCol & _
                              "' AND ui_codling = " & cbLingua.SelectedValue.ToString
      dtrTmp = dsUi.Tables("UICONF").Select(strFilter)
      For i = 0 To dtrTmp.Length - 1
        bDb = False
        bDitta = False
        bGruppo = False
        bUtente = False
        bTipoDoc = False
        If dtrTmp(i)!ui_db.ToString().Trim <> "" Then bDb = True
        If dtrTmp(i)!ui_ditta.ToString().Trim <> "" Then bDitta = True
        If dtrTmp(i)!ui_ruolo.ToString().Trim <> "" Then bGruppo = True
        If dtrTmp(i)!ui_opnome.ToString().Trim <> "" Then bUtente = True
        If dtrTmp(i)!ui_tipodoc.ToString().Trim <> "" Then bTipoDoc = True

        If bTipoDoc Then
          If bDitta And bUtente Then optPrior1.ForeColor = Color.Red
          If bDb And bDitta = False And bUtente Then optPrior2.ForeColor = Color.Red
          If bDitta And bGruppo Then optPrior3.ForeColor = Color.Red
          If bDb And bDitta = False And bGruppo Then optPrior4.ForeColor = Color.Red
          If bDitta And bUtente = False And bGruppo = False Then optPrior5.ForeColor = Color.Red
          If bDb And bDitta = False And bUtente = False And bGruppo = False Then optPrior6.ForeColor = Color.Red
        Else
          If bDitta And bUtente Then optPrior7.ForeColor = Color.Red
          If bDb And bDitta = False And bUtente Then optPrior8.ForeColor = Color.Red
          If bDitta And bGruppo Then optPrior9.ForeColor = Color.Red
          If bDb And bDitta = False And bGruppo Then optPrior10.ForeColor = Color.Red
          If bDitta And bGruppo = False And bUtente = False Then optPrior11.ForeColor = Color.Red
          If bDb And bDitta = False And bGruppo = False And bUtente = False Then optPrior12.ForeColor = Color.Red
          If bDb = False And bDitta = False And bGruppo = False And bUtente = False Then optPrior13.ForeColor = Color.Red
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub TogliCheck(ByVal bSoloPrior As Boolean)
    '-------------------------------------------
    'tolgo tutti i check
    If Not bSoloPrior Then
      optVisible.Checked = False
      optEnable.Checked = False
      optBold.Checked = False
      optDefault.Checked = False
      optFormatnumber.Checked = False
      optOutnotequal.Checked = False
      optChecked.Checked = False
      optText.Checked = False
      optErrorText.Checked = False
      optInsert.Checked = False
      optUpdate.Checked = False
      optDelete.Checked = False
    End If

    optPrior1.Checked = False
    optPrior2.Checked = False
    optPrior3.Checked = False
    optPrior4.Checked = False
    optPrior5.Checked = False
    optPrior6.Checked = False
    optPrior7.Checked = False
    optPrior8.Checked = False
    optPrior9.Checked = False
    optPrior10.Checked = False
    optPrior11.Checked = False
    optPrior12.Checked = False
    optPrior13.Checked = False

    GestisciGriglia(True, 0)

  End Sub

  Public Overridable Sub GestisciGriglia(ByVal bLock As Boolean, ByVal nPrior As Integer)

    Dim strNomProp As String = ""
    Dim strFilter As String = ""
    Dim strGricol As String = "."
    Dim strComboCol As String = "."
    Dim strLingua As String = "0"
    Dim strValProp As String = ""
    Dim strTipoDocTmp As String = ""

    Try
      If oCleGctl.RecordIsChanged Then Salva(False)

      'se aggiungo le sue righe che seguono la riga proposta è gia completa e se non digito almeno un dato non permette di salvarla
      'If optVisible.Checked Or optEnable.Checked Then strValProp = "0"
      'If optBold.Checked Then strValProp = "-1"

      If optVisible.Checked Then strNomProp = "VISIBLE"
      If optEnable.Checked Then strNomProp = "ENABLE"
      If optBold.Checked Then strNomProp = "BOLD"
      If optDefault.Checked Then strNomProp = "DEFAULT"
      If optFormatnumber.Checked Then strNomProp = "FORMAT"
      If optOutnotequal.Checked Then strNomProp = "OUTNOTEQUAL"
      If optChecked.Checked Then strNomProp = "CHECKED"
      If optText.Checked Then strNomProp = "TEXT"
      If optErrorText.Checked Then strNomProp = "ERRORTEXT"
      If optInsert.Checked Then strNomProp = "ALLOWNEW"
      If optUpdate.Checked Then strNomProp = "ALLOUPDATE"
      If optDelete.Checked Then strNomProp = "ALLOWDELETE"

      If Not cbGriglia.SelectedValue Is Nothing Then strGricol = cbGriglia.SelectedValue.ToString
      If Not cbCombo.SelectedValue Is Nothing Then strComboCol = cbCombo.SelectedValue.ToString
      If Not cbLingua.SelectedValue Is Nothing Then strLingua = cbLingua.SelectedValue.ToString()

      strFilter = "ui_nomprop = '" & strNomProp & "' AND " & _
                  "ui_gridcol = '" & strGricol & "' AND " & _
                  "ui_comboitem = '" & strComboCol & "' AND " & _
                  "ui_codling = " & strLingua

      '------------------------------------
      'comunico all'entity la priorità impostata in modo che proponga correttamente i valori di default
      strTipoDocTmp = strTipoDoc
      If nPrior > 6 Then strTipoDocTmp = " "
      Select Case nPrior
        Case 1, 7
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, DittaCorrente, "", oApp.User.Nome, _
                          CType(cbLingua.SelectedValue, Integer), strGricol, _
                          strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 2, 8
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, "", "", oApp.User.Nome, _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 3, 9
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, DittaCorrente, oApp.User.Gruppo, "", _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 4, 10
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, "", oApp.User.Gruppo, "", _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 5, 11
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, DittaCorrente, "", "", _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 6, 12
          oCleGctl.SetCurrentPriority(nPrior, oApp.Db.Nome, "", "", "", _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
        Case 13
          oCleGctl.SetCurrentPriority(nPrior, "", "", "", "", _
                CType(cbLingua.SelectedValue, Integer), strGricol, _
                strComboCol, strNomProp, strTipoDocTmp, strParent, strValProp)
      End Select

      '------------------------------------
      'prima riabilito tutto
      ui_db.Enabled = True
      ui_ditta.Enabled = True
      ui_ruolo.Enabled = True
      ui_opnome.Enabled = True
      ui_tipodoc.Enabled = True
      ui_db.Visible = True
      ui_ditta.Visible = True
      ui_ruolo.Visible = True
      ui_opnome.Visible = True
      ui_tipodoc.Visible = True

      '------------------------------------
      'abilito tutte le colonne della griglia e blocco la griglia
      'quando imposterò un check riabiliterò la griglia
      If bLock Or (optVisible.Checked = False And optEnable.Checked = False And optBold.Checked = False And _
        optDefault.Checked = False And optFormatnumber.Checked = False And optOutnotequal.Checked = False And _
        optChecked.Enabled = False And optText.Checked = False And optErrorText.Checked = False And _
        optInsert.Checked = False And optUpdate.Checked = False And optDelete.Checked = False) Then
        grvUI.Enabled = False
        grUI.Visible = False
        cmdRipristina.Enabled = False
        cmdCancRow.Enabled = False
        Return
      End If

      '------------------------------------
      'adesso devo bloccare le colonne che non posso gestire
      'e far vedere solo le righe interessate dal vincolo
      If optVisible.Checked = False And optEnable.Checked = False And optBold.Checked = False And _
         optDefault.Checked = False And optFormatnumber.Checked = False And optOutnotequal.Checked = False And _
         optChecked.Checked = False And optText.Checked = False And optErrorText.Checked = False And _
         optInsert.Checked = False And optUpdate.Checked = False And optDelete.Checked = False Then
        'non ho selezionato la proprietà
      Else
        grvUI.Enabled = True
        grUI.Visible = True
        cmdRipristina.Enabled = True
        cmdCancRow.Enabled = True
      End If

      If nPrior > 6 Then
        ui_tipodoc.Enabled = False
        ui_tipodoc.Visible = False
        'strFilter = strFilter & " AND ui_tipodoc = ' '"
      Else
        'strFilter = strFilter & " AND ui_tipodoc = '" & strTipoDoc & "'"
      End If

      Select Case nPrior
        Case 1, 7
          ui_ruolo.Enabled = False
          ui_ruolo.Visible = False
          strFilter = strFilter & " AND ui_db <> ' 'AND ui_ditta <> ' ' AND ui_ruolo = ' ' AND ui_opnome <> ' '"
        Case 2, 8
          ui_ditta.Enabled = False
          ui_ruolo.Enabled = False
          ui_ditta.Visible = False
          ui_ruolo.Visible = False
          strFilter = strFilter & " AND ui_db <> ' 'AND ui_ditta = ' ' AND ui_ruolo = ' ' AND ui_opnome <> ' '"
        Case 3, 9
          ui_opnome.Enabled = False
          ui_opnome.Visible = False
          strFilter = strFilter & " AND ui_db <> ' 'AND ui_ditta <> ' ' AND ui_ruolo <> ' ' AND ui_opnome = ' '"
        Case 4, 10
          ui_ditta.Enabled = False
          ui_opnome.Enabled = False
          ui_ditta.Visible = False
          ui_opnome.Visible = False
          strFilter = strFilter & " AND ui_db <> ' 'AND ui_ditta = ' ' AND ui_ruolo <> ' ' AND ui_opnome = ' '"
        Case 5, 11
          ui_ruolo.Enabled = False
          ui_opnome.Enabled = False
          ui_ruolo.Visible = False
          ui_opnome.Visible = False
          strFilter = strFilter & " AND ui_db <> ' ' AND ui_ditta <> ' '  AND ui_ruolo = ' ' AND ui_opnome = ' '"
        Case 6, 12
          ui_ditta.Enabled = False
          ui_ruolo.Enabled = False
          ui_opnome.Enabled = False
          ui_ditta.Visible = False
          ui_ruolo.Visible = False
          ui_opnome.Visible = False
          strFilter = strFilter & " AND ui_db <> ' ' AND ui_ditta = ' ' AND ui_ruolo = ' ' AND ui_opnome = ' '"
        Case 13
          ui_db.Enabled = False
          ui_ditta.Enabled = False
          ui_ruolo.Enabled = False
          ui_opnome.Enabled = False
          ui_db.Visible = False
          ui_ditta.Visible = False
          ui_ruolo.Visible = False
          ui_opnome.Visible = False
          strFilter = strFilter & " AND ui_db = ' ' AND ui_ditta = ' ' AND ui_ruolo = ' ' AND ui_opnome = ' '"
      End Select

      dtcUi.Filter = strFilter

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva(ByVal bSaveposition As Boolean) As Boolean
    Dim dRes As DialogResult
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      dRes = grvUI.NTSSalvaRigaCorrente(dtcUi, oCleGctl.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          If Not oCleGctl.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGctl.Ripristina(dtcUi.Position, dtcUi.Filter)
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

#Region "Command Button"
  Public Overridable Sub cmdCancRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancRow.Click
    Try
      If Not grvUI.NTSDeleteRigaCorrente(dtcUi, True) Then Return
      oCleGctl.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdRipristina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRipristina.Click
    Try
      If Not grvUI.NTSRipristinaRigaCorrenteBefore(dtcUi, True) Then Return
      oCleGctl.Ripristina(dtcUi.Position, dtcUi.Filter)
      grvUI.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    If Not Salva(False) Then Return
    Me.Close()
  End Sub
  Public Overridable Sub cmdRemoting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoting.Click
    Dim frmRemoting As FRM__GCRE
    Try
      '-------------------------------------------------
      'chiamo la form per gestire il remoting del DAL
      frmRemoting = CType(NTSNewFormModal("FRM__GCRE"), FRM__GCRE)
      frmRemoting.Init(oMenu)
      frmRemoting.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      frmRemoting = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdFastVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFastVisible.Click
    Try
      If fmParent.Name = "FRM__GCTL" Then Return

      oApp.MsgBoxInfo(oApp.Tr(Me, 129500036843007812, _
                      "Con questa funzionalità sarà possibile rendere NON VISIBILI per TUTTI gli utenti" & vbCrLf & _
                      "i controlli con il puntatore del mouse sopra e successiva pressione del tasto CTRL." & vbCrLf & _
                      "(Per le colonne di griglia prima bisogna posizionarsi sulla colonna da nascondere. " & vbCrLf & _
                      "Stessa cosa per le pagine del TabControl)" & vbCrLf & _
                      "Per terminare la procedura e salvare le impostazioni (o annullare l'ultimo comando)" & vbCrLf & _
                      "vedi nuovi comandi in altro a sinistra." & vbCrLf & _
                      "(NB: non è possibile utilizzare questa procedura con i comandi della toolbar)"))

      Dim pnFastVis As New NTSPanel
      pnFastVis.Name = "pnFastVis"
      pnFastVis.BackColor = Color.Transparent
      pnFastVis.Width = 150
      pnFastVis.Height = 17
      pnFastVis.Top = 0
      pnFastVis.Left = 0
      fmParent.Controls.Add(pnFastVis)
      pnFastVis.BringToFront()
      fmParent.Controls.Add(pnFastVis)

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub optPrior1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior1.CheckedChanged
    GestisciGriglia(False, 1)
  End Sub
  Public Overridable Sub optPrior2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior2.CheckedChanged
    GestisciGriglia(False, 2)
  End Sub
  Public Overridable Sub optPrior3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior3.CheckedChanged
    GestisciGriglia(False, 3)
  End Sub
  Public Overridable Sub optPrior4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior4.CheckedChanged
    GestisciGriglia(False, 4)
  End Sub
  Public Overridable Sub optPrior5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior5.CheckedChanged
    GestisciGriglia(False, 5)
  End Sub
  Public Overridable Sub optPrior6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior6.CheckedChanged
    GestisciGriglia(False, 6)
  End Sub
  Public Overridable Sub optPrior7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior7.CheckedChanged
    GestisciGriglia(False, 7)
  End Sub
  Public Overridable Sub optPrior8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior8.CheckedChanged
    GestisciGriglia(False, 8)
  End Sub
  Public Overridable Sub optPrior9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior9.CheckedChanged
    GestisciGriglia(False, 9)
  End Sub
  Public Overridable Sub optPrior10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior10.CheckedChanged
    GestisciGriglia(False, 10)
  End Sub
  Public Overridable Sub optPrior11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior11.CheckedChanged
    GestisciGriglia(False, 11)
  End Sub
  Public Overridable Sub optPrior12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior12.CheckedChanged
    GestisciGriglia(False, 12)
  End Sub
  Public Overridable Sub optPrior13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior13.CheckedChanged
    GestisciGriglia(False, 13)
  End Sub


  Public Overridable Sub optVisible_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVisible.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'VISIBLE'")
  End Sub
  Public Overridable Sub optEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEnable.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'ENABLE'")
  End Sub
  Public Overridable Sub optBold_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBold.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'BOLD'")
  End Sub
  Public Overridable Sub optDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDefault.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'DEFAULT'")
  End Sub
  Public Overridable Sub optFormatnumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormatnumber.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'FORMAT'")
  End Sub
  Public Overridable Sub optOutnotequal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOutnotequal.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'OUTNOTEQUAL'")
  End Sub
  Public Overridable Sub optChecked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optChecked.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'CHECKED'")
  End Sub
  Public Overridable Sub optText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optText.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'TEXT'")
  End Sub
  Public Overridable Sub optErrorText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optErrorText.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'ERRORTEXT'")
  End Sub
  Public Overridable Sub optInsert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optInsert.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'ALLOWNEW'")
  End Sub
  Public Overridable Sub optUpdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optUpdate.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'ALLOUPDATE'")
  End Sub
  Public Overridable Sub optDelete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDelete.CheckedChanged
    TogliCheck(True)
    EvidenziaVincoli("ui_nomprop = 'ALLOWDELETE'")
  End Sub

End Class
