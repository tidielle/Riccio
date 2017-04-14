Imports System.Data
Imports NTSInformatica.CLN__STD


'PER UN ESEMPIO DI CLASSE EREDITATA CON DAL SPECIFICO VEDI BE__SOTC, BECGPRIV (con cambio ditta), BEVECOVP

Public Class CLEMGMATR
  Inherits CLE__BASE

  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCldMatr As CLDMGMATR
  Public strTipork As String = ""
  Public nAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumdoc As Integer = 0
  Public lRiga As Integer = 0
  Public dQuant As Decimal = 0
  Public nEsist As Decimal = 0      '1 = documento di carico, -1 = documento di scarico
  Public strCodart As String = ""
  Public nFase As Integer = 0
  Public nMagaz As Integer = 0      'magazzino 1: per sapere se è di carico o di scarico si testa nEsis (1 = carico, -1 = scarico)
  Public nMagaz2 As Integer = 0     'magazzino 2: per documenti che muovono 2 magazzini  
  Public dsDoc As DataSet           'dataset contenente il documento in fase di modifica senza le matricole relative alla riga corrente (che sono in dsShared)
  Public bNewdoc As Boolean = False 'true se il documento a cui sono collegate le matricole è in modifica

  'Opzioni di registro
  Public bAutoAggiornaRighe As Boolean = False
  Public strControllaMatricoleInScarico As String = "" 'blank=nessuno, B=blocca,A=solo avviso
  Public strControllaMatricoleInCarico As String = "" 'blank=nessuno, B=blocca,A=solo avviso
  Public bCodartDaBarcode As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                            ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                            ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                            ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGMATR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldMatr = CType(MyBase.ocldBase, CLDMGMATR)
    oCldMatr.Init(oApp)
    Return True
  End Function

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("MOVMATR").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("MOVMATR").Columns("mma_quant").DefaultValue = 1
      ds.Tables("MOVMATR").Columns("mma_matric").DefaultValue = " "

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim nRiga As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      dtrT = dsShared.Tables("MOVMATR").Select("", "mma_rigaa DESC")
      If dtrT.Length > 0 Then nRiga = NTSCInt(dtrT(0)!mma_rigaa)
      nRiga += 1
      e.Row!mma_rigaa = nRiga
      e.Row!mma_tipork = strTipork
      e.Row!mma_anno = nAnno
      e.Row!mma_serie = strSerie
      e.Row!mma_numdoc = lNumdoc
      e.Row!mma_riga = lRiga

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128067822802778673, strErr))

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "mma_rigaa" Then
        If NTSCInt(e.Row!mma_rigaa) = 0 Then OnAddNewRow(sender, e)
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_mma_matric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      'Se la matricola non è cambiata non devo fare alcun controllo
      If NTSCStr(e.ProposedValue).ToUpper = NTSCStr(e.Row!mma_matric).ToUpper Then Return
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("MOVMATR").Select("mma_matric = " & CStrSQL(e.ProposedValue.ToString), Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696805805625000, "La matricola già stata utilizzata. Inserire una matricola diversa.")))
        Return
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("MOVMATR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i)!mma_matric).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696805805781250, "La matricola è un campo obbligatorio. Inserirla prima di salvare la riga")))
          Return False
        End If

        'Controlla che la matricola che si scarica sia esistente
        If strTipork <> "B" And strTipork <> "A" And strTipork <> "C" And strTipork <> "F" And strTipork <> "S" And strTipork <> "U" Then
          If Not CheckEsistMatric(NTSCStr(dtrTmp(i)!mma_matric), NTSCDec(dtrTmp(i)!mma_quant), True) Then
            If strControllaMatricoleInCarico = "B" Then
              Return False
            End If
          End If
        End If
        If strControllaMatricoleInScarico <> " " Then
          If Not CheckEsistMatric(NTSCStr(dtrTmp(i)!mma_matric), NTSCDec(dtrTmp(i)!mma_quant), True) Then
            If strControllaMatricoleInScarico = "B" Then
              Return False
            End If
          End If
        End If

      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      dsShared.Tables("MOVMATR").AcceptChanges()
      bHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function SalvaFinale() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dQta As Decimal = 0
    Dim evnt As NTSEventArgs = Nothing
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY

    Try
      If dsShared.Tables("MOVMATR").Select("mma_matric = ' '").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128697013795312500, "Sono presenti delle righe senza matricola. Impossibile proseguire")))
        Return False
      End If

      '-------------------
      'test su quantità totale = quella del documento
      For i = 0 To dsShared.Tables("MOVMATR").Rows.Count - 1
        dQta += NTSCDec(dsShared.Tables("MOVMATR").Rows(i)!mma_quant)
      Next
      If dQta <> dQuant Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128696839001875000, "La somma delle quantità indicate nel dettaglio matricole, non è pari a quella indicata nel corpo del documento." & vbCrLf & vbCrLf & "Somma quantità matricole: |" & dQta.ToString(oApp.FormatQta) & "|" & vbCrLf & "Quantità riga documento: |" & dQuant.ToString(oApp.FormatQta) & "|" & vbCrLf & vbCrLf & "Continuare ugualmente?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If

      oDttgr.NTSGroupBy(dsShared.Tables("MOVMATR"), dttGr, "Count(mma_matric) As Count, mma_matric", "", "mma_matric")
      If dttGr.Select("Count > 1").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696841042500000, "La matricola '|" & dttGr.Rows(0)!mma_matric.ToString & "|' è stata indicata più di una volta")))
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CheckEsistMatric(ByVal strMatr As String, ByVal dQuan As Decimal, ByVal bShowMsg As Boolean) As Boolean
    Dim bMatrMov As Boolean = False
    Dim dResid As Decimal = 0
    Dim dtrT1() As DataRow = Nothing
    Dim dQta As Decimal = 0

    Dim bMatrMov2 As Boolean = False
    Dim dResid2 As Decimal = 0

    Dim nMagTmp As Integer = 0
    Dim dResTmp As Decimal = 0
    Dim bTrovTmp As Boolean = False

    Try
      If nEsist = 0 Then
        Return True
      ElseIf nEsist < 0 Then         'vendita/scarico
        If strControllaMatricoleInScarico = " " Then
          Return True
        End If
      Else                            'acquisto/carico da prod
        If strControllaMatricoleInCarico = " " And strTipork <> "T" Then
          Return True
        End If
      End If

      '----------------------
      'ottengo il residuo della matricola
      For nGiro As Integer = 0 To 1
        If nGiro = 0 Then
          'primo magazzino (tutti i casi eccetto causali di magaz doppie)
          nMagTmp = nMagaz
        Else
          'per documenti che muovono 2 magazzini trovo l'esistenza anche sul secondo magazzino
          If nMagaz2 = 0 Then Exit For
          nMagTmp = nMagaz2
        End If

        If Not oCldMatr.GetResiduoMatricola(strDittaCorrente, strCodart, nFase, nMagTmp, strMatr, bNewdoc, _
                                                  strTipork, nAnno, strSerie, lNumdoc, dResTmp, bTrovTmp) Then Return False

        '----------------------
        If dsDoc.Tables("MOVMATR").Rows.Count > 0 Then
          'Ora toglie le matricole eventaulemente indicate nel transitorio MMTRANS (esclude le righe con causale doppia)
          dQta = 0
          For Each dtrT As DataRow In dsDoc.Tables("MOVMATR").Select("mma_tipork NOT IN ('U', 'Y') AND mma_matric = " & CStrSQL(strMatr))
            'trovo la riga di movmag collegata a quella della matricola
            dtrT1 = dsDoc.Tables("CORPO").Select("ec_riga = " & dtrT!mma_riga.ToString & _
                                                 " AND ec_codart = " & CStrSQL(strCodart) & _
                                                 " AND ec_causale2 = 0 " & _
                                                 " AND ec_magaz = " & nMagTmp.ToString & _
                                                 " AND ec_fase = " & nFase.ToString)
            If dtrT1.Length > 0 Then dQta += NTSCDec(dtrT!mma_quant) * NTSCInt(dtrT1(0)!xxo_esist)
          Next
          dResTmp += dQta

          'Ora toglie le matricole eventaulemente indicate nel transitorio MMTRASCA (esclude le righe con causale doppia)
          dQta = 0
          For Each dtrT As DataRow In dsDoc.Tables("MOVMATR").Select("mma_tipork IN ('U', 'Y') AND mma_matric = " & CStrSQL(strMatr))
            'trovo la riga di movmag collegata a quella della matricola
            dtrT1 = dsDoc.Tables("CORPOIMP").Select("ec_riga = " & dtrT!mma_riga.ToString & _
                                                 " AND ec_codart = " & CStrSQL(strCodart) & _
                                                 " AND ec_causale2 = 0 " & _
                                                 " AND ec_magaz = " & nMagTmp.ToString & _
                                                 " AND ec_fase = " & nFase.ToString)
            If dtrT1.Length > 0 Then dQta += NTSCDec(dtrT!mma_quant) * NTSCInt(dtrT1(0)!xxo_esist)
          Next
          dResTmp += dQta
        End If

        If nGiro = 0 Then
          'primo magazzino (tutti i casi eccetto causali di magaz doppie)
          dResid = dResTmp
          bMatrMov = bTrovTmp
        Else
          'per documenti che muovono 2 magazzini
          'trovo l'esistenza sul secondo magazzino
          dResid2 = dResTmp
          bMatrMov2 = bTrovTmp
        End If
      Next    'For nGiro As Integer = 0 To 1

      '----------------------
      If nEsist < 0 Then
        'test per matricola in scarico
        If dResid < dQuan And strControllaMatricoleInScarico <> " " Then
          If bShowMsg Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696854743593750, "Matricola '|" & strMatr & "|' non disponibile per l'articolo '|" & strCodart & "|' sul magazzino |" & nMagaz & "|.")))
          Return False
        End If
      Else
        'test per matricola in carico
        If dResid > 0 And strControllaMatricoleInCarico <> " " Then
          If bShowMsg Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696854682343750, "Matricola '|" & strMatr & "|' già presente in magazzino |" & nMagaz & "| con esistenza maggiore di 0 per l'articolo '|" & strCodart & "|'.")))
          Return False
        End If

        If dResid > 0 And bMatrMov And strTipork = "T" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696853320625000, "Attenzione: la Matricola '|" & strMatr & "|' è già stata prodotta per l'articolo '|" & strCodart & "|' sul magazzino |" & nMagaz & "|.")))
        End If
      End If    'If nEsist < 0 Then

      '----------------------
      'stessi test per secondo magazzino
      If nMagaz2 <> 0 Then
        If nEsist > 0 Then  'il segno è invertito rispetto al test sul magaz1, perchè se nMagaz è in carico, nMagaz2 sarà sempre in scarico (e viceversa)
          'test per matricola in scarico
          If dResid2 < dQuan And strControllaMatricoleInScarico <> " " Then
            If bShowMsg Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130705573707857349, "Matricola '|" & strMatr & "|' non disponibile per l'articolo '|" & strCodart & "|' sul magazzino |" & nMagaz2 & "|.")))
            Return False
          End If
        Else
          'test per matricola in carico
          If dResid2 > 0 And strControllaMatricoleInCarico <> " " Then
            If bShowMsg Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130705573691392969, "Matricola '|" & strMatr & "|' già presente in magazzino |" & nMagaz2 & "| con esistenza maggiore di 0 per l'articolo '|" & strCodart & "|'.")))
            Return False
          End If

          If dResid2 > 0 And bMatrMov And strTipork = "T" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130705573673606753, "Attenzione: la Matricola '|" & strMatr & "|' è già stata prodotta per l'articolo '|" & strCodart & "|' sul magazzino |" & nMagaz2 & "|.")))
          End If
        End If    'If nEsist < 0 Then
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function GeneraProgressivo() As Boolean
    Dim lProgr As Integer = 0
    Dim lNewProgr As Integer = 0
    Dim i As Decimal = 0
    Dim dStart As Decimal = 0
    Dim dQuan As Decimal = 0
    Dim dQta As Decimal = 0
    Dim l As Integer = 0
    Dim evnt As NTSEventArgs = Nothing
    Dim strErr As String = ""

    Try
      If bHasChanges Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989432964882230, "Salvare o rispristinare la riga in corso di modifica prima di proseguire.")))
        Return False
      End If

      dStart = 1
      ' Se la quantità passata è = 0 non genera alcun progressivo
      If dQuant < 0 Then Return True
      If dQuant = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989433120975980, "Con la quantità pari a 0 non è possibile generare i progressivi.")))
        Return False
      End If

      ' Controlla se la somma delle matricole esistenti è pari alla quantità passata
      ' Se sì chiede conferma prima di passare alla generazione di ulteriori progressivi
      For l = 0 To dsShared.Tables("MOVMATR").Rows.Count - 1
        dQta += NTSCDec(dsShared.Tables("MOVMATR").Rows(l)!mma_quant)
      Next
      If NTSCDec(dQta) >= NTSCDec(dQuant) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128989433190350980, "Attenzione! La somma delle quantità già indicate nel dettaglio matricole, è pari o superiore a quella indicata nel corpo del documento." & vbCrLf & vbCrLf & "Procedere ugualmente alla generazione di ulteriori numeri di matricola?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      Else
        dStart = (NTSCInt(dQta) + 1)
      End If

      ' Prende la parte intera della quantità non considerando i decimali
      dQuan = NTSCInt(dQuant)
      ' Genera i progressivi
      For i = dStart To dQuan
        lProgr = oCldMatr.LegNuma(strDittaCorrente, "MT", "", 0, True)
        'se la matricola non esiste ...
        If Not oCldMatr.CheckMatricola(strDittaCorrente, strCodart, lProgr) Then
          dsShared.Tables("MOVMATR").Rows.Add(dsShared.Tables("MOVMATR").NewRow)
          With dsShared.Tables("MOVMATR").Rows(dsShared.Tables("MOVMATR").Rows.Count - 1)
            'forzo la OnAddNew
            !codditt = "."
            !codditt = strDittaCorrente
            !mma_matric = Right$("000000000" & lProgr, 9)
            !mma_quant = 1
          End With
        Else
          i = i - 1
        End If    'If Not oCldMatr.CheckMatricola(strDittaCorrente, strCodart, lProgr) Then
        dsShared.Tables("MOVMATR").AcceptChanges()

        lNewProgr = oCldMatr.AggNuma(strDittaCorrente, "MT", "", 0, lProgr, True, True, strErr)
        If strErr <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", strErr))
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GeneraPartendoDa() As Boolean
    Dim strStart As String = ""
    Dim strMax As String = ""
    Dim dQta As Decimal = 0
    Dim l As Integer = 0
    Dim evnt As NTSEventArgs = Nothing
    Dim strErr As String = ""
    Dim dQuan As Decimal = 0
    Dim nC As Integer = 0
    Dim nI As Integer = 0
    Dim dMax As Decimal = 0
    Dim dNum As Decimal = 0
    Dim dCont As Decimal = 0
    Dim lProgr As Integer = 0
    Dim i As Integer = 0
    Dim strMatric As String = ""

    Try
      If bHasChanges Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989432988788480, "Salvare o rispristinare la riga in corso di modifica prima di proseguire.")))
        Return False
      End If

      ' Se la quantità passata è = 0 non genera alcun progressivo
      If dQuant < 0 Then Return True
      If dQuant = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989433099257230, "Con la quantità pari a 0 non è possibile generare i progressivi.")))
        Return False
      End If

      'Prende la parte intera della quantità non considerando i decimali
      dQuan = NTSCInt(dQuant)

      ' Controlla se la somma delle matricole esistenti è pari alla quantità passata
      ' Se sì chiede conferma prima di passare alla generazione di ulteriori progressivi
      For l = 0 To dsShared.Tables("MOVMATR").Rows.Count - 1
        dQta += NTSCDec(dsShared.Tables("MOVMATR").Rows(l)!mma_quant)
      Next
      If NTSCDec(dQta) >= NTSCDec(dQuant) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128989433213788480, "Attenzione! La somma delle quantità già indicate nel dettaglio matricole, è pari o superiore a quella indicata nel corpo del documento." & vbCrLf & vbCrLf & "Procedere ugualmente alla generazione di ulteriori numeri di matricola?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      Else
        dQuan = ArrDbl(NTSCInt(dQuant) - NTSCInt(dQta), 0)
      End If

      '-----------------------------------------------------------------------
      'Chiama una inputbox x il numero di partenza
      evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128696993954687500, "Indicare il numero di matricola iniziale:"))
      ThrowRemoteEvent(evnt)
      strStart = evnt.RetValue
      If strStart = "" Then
        Return False
      End If
      If Len(strStart) > 30 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128697003457500000, "Il numero di matricola deve essere al massimo di 30 caratteri.")))
        Return False
      End If
      '-----------------------------------------------------------------------
      strMax = ""
      For nC = Len(strStart) To 1 Step -1
        If (Mid(strStart, nC, 1) >= "0") And (Mid(strStart, nC, 1) <= "9") Then
          strMax = strMax & "9"
        Else
          Exit For
        End If
      Next nC
      'C'è una lettera come ultimo carattere
      If strMax = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128696997964531250, "Gli ultimi caratteri del numero di matricola iniziale, devono essere numerici.")))
        Return False
      End If
      '-----------------------------------------------------------------------
      'Determina il numero di partenza e massimo possibile
      If Len(strMax) = Len(strStart) Then
        dMax = 999999999
      Else
        dMax = NTSCDec(strMax)
      End If
      dNum = NTSCDec(Right(strStart, Len(strStart) - nC))
      If dMax < dNum Then
        dNum = dMax
      Else
        dNum = dMax - dNum + 1
      End If
      If dNum > dQuant Then
        dNum = dQuant
      End If
      nC = Len(strStart) - nC
      If nC > 4 Then nC = 4
      If nC > 0 Then
        dCont = NTSCDec(Right(strStart, nC))
      Else
        nC = 1
      End If
      'Loppa sulle matricole
      lProgr = 0 : i = 0
      For nI = 0 To NTSCInt(dMax) - 1
        i = i + 1
        'Qui dovrebbe controllare se la matricola esiste a magazzino
        strMatric = Left(strStart, Len(strStart) - nC) & dCont.ToString("".PadLeft(nC, "0"c))
        If CheckEsistMatric(strMatric, 1, False) Then
          dsShared.Tables("MOVMATR").Rows.Add(dsShared.Tables("MOVMATR").NewRow)
          With dsShared.Tables("MOVMATR").Rows(dsShared.Tables("MOVMATR").Rows.Count - 1)
            'forzo la OnAddNew
            !codditt = "."
            !codditt = strDittaCorrente
            !mma_matric = strMatric
            !mma_quant = 1
          End With
          dsShared.Tables("MOVMATR").AcceptChanges()
          lProgr = lProgr + 1
        End If
        dCont = dCont + 1
        If lProgr >= dNum Then
          Exit For
        End If
        'Uscita di sicurezza per evitare loop infoniti x mancanza di matricole caricate
        If nI > dNum + 99 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128697000255000000, "Non sono state trovate matricole in giacenza il cui numero iniziale è '|" & strStart & "|' a copertura delle |" & NTSCDec(dQuant) & "| quantità richieste. Scansione di numeri di matricola terminata al numero '|" & strMatric & "|'.")))
          Exit For
        End If
      Next nI

      If lProgr <> NTSCInt(dQuant) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128697000556093750, "Alcuni numeri di matricola non sono stati generati; righe da generare: |" & NTSCDec(dQuant) & "|, righe generate |" & lProgr & "|")))
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
End Class