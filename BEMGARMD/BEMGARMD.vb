Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGARMD
  Inherits CLE__BASN
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE + bsModExtCRM
  Private ModuliSup_P As Integer = bsModSupWCR
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


  Public oCldArmd As CLDMGARMD
  Public strPrgParent As String = ""         'nome del programma che mi ha chiamato

  'log
  Public strActLog As String = ""
  Public strActLogProg As String = ""
  Public strActLogNomOggLog As String = ""
  Public strActLogDesLog As String = ""

  'mgarma
  Public dsArmaShared As DataSet
  Public bArmaHasChanges As Boolean = False
  Public bArmaGesfasi As Boolean
  Public strArmaUltfase As String
  Public strArmaCodart As String
  Public nArmaCodtagl As Integer

  'mgckit
  Public dsCkitShared As DataSet
  Public bCkitHasChanges As Boolean = False
  Public strCkitCodart As String
  Public dttAk_unmis As New DataTable()
  Public bConsentiModificaArticoliKit As Boolean = False

  'mgfasi
  Public dsFasiShared As DataSet
  Public bFasiHasChanges As Boolean = False
  Public dFasiPerqta As Decimal
  Public strFasiCodart As String
  Public strFasiColore As String

  'mghlap
  Public dsHlapShared As DataSet
  Public bHlapHasChanges As Boolean = False
  Public dHlapFase As Decimal
  Public strHlapCodart As String
  Public strElencoCodici As String
  Public bHlapTCO As Boolean

  'mghlat
  Public dsHlatShared As DataSet
  Public bHlatHasChanges As Boolean = False
  Public dHlatPerqta As Decimal
  Public strHlatCodart As String
  Public dHlatFase As Decimal
  Public bHlatTCO As Boolean

  'mgbarc
  Public dsBarcShared As DataSet
  Public bBarcHasChanges As Boolean = False
  Public strBarcCodart As String
  Public strBarcUnmis As String
  Public strBarcConfez2 As String
  Public strBarcunmis2 As String
  Public dBarcQtacon2 As Decimal
  Public dBarcConver As Decimal
  Public bBarcGesfasi As Boolean
  Public dBarcFase As Decimal
  Public nBarcCodtagl As Integer
  Public strBarcPrimaTaglia As String
  Public dttBc_tagl As New DataTable
  Public bCodartDaBarcode As Boolean = False

  Public bIndicod As Boolean
  Public strPrefixEAN13 As String
  Public strPrefixEAN13GiftCard As String
  Public bBarcodeDerogaQtaBOLL As Boolean
  Public bBarcodeDerogaQtaGSOR As Boolean
  Public bAbilBarcodeVariabili As Boolean

  'Primo digit di EAN8 ed EAN13
  Public Const PRIMODGT As String = "2"
  Public Const EAN8 As Integer = 1
  Public Const EAN13 As Integer = 2

  'mghlav
  Public dsHlavShared As DataSet
  Public bHlavHasChanges As Boolean = False
  Public strHlavCodart As String
  Public bHlavIsRoot As Boolean = False
  Public strHlavCartellaColore As String = ""

  'mgcacf
  Public dsCacfShared As DataSet
  Public bCacfHasChanges As Boolean = False
  Public strCacfCodart As String
  Public bBDPKExt As Boolean

  'mgvgif
  Public dVGifQuale As Decimal
  Public strVGifPath As String
  Public nAltezzaMaxImg As Integer

  'mgscor
  Public bScorRoot As Boolean
  Public strScorCodart As String
  Public nScorCodtagl As Decimal
  Public nScorCodmaga As Integer
  Public nScorFase As Integer
  Public strScorChiamante As String
  Public bScorHasChanges As Boolean

  'mggift
  Public dttGiftShared As DataTable
  Public bGiftHasChanges As Boolean

  Public bNoMsgCongruenzaPolSconte As Boolean = False

  'mgsimu
  Public strSimuCodart As String

  'mgdepr
  Public dsDeprShared As DataSet
  Public strDeprCodart As String = ""
  Public dDeprMagaz As Integer = 0
  Public strDeprDesMagaz As String = ""
  Public dDeprFase As Integer = 0

  Public dsLotcShared As DataSet
  Public strLotcCodart As String = ""
  Public strLotcDesCodart As String = ""
  Public strLotcMagaz As String = ""
  Public strLotcDesMagaz As String = ""
  Public strLotcFase As String = ""
  Public strLotcDesFase As String = ""

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGARMD"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldArmd = CType(MyBase.ocldBase, CLDMGARMD)
    oCldArmd.Init(oApp)
    Return True
  End Function

  'stesse funzioni in mgdocu per caricare le unità di misura
  Public Overridable Function GetArticoUnMis(ByVal strCodart As String) As String
    '---------------------------------------
    'ritorna le unità di misura dell'articolo passato in input
    Try
      Return CType(oCleComm, CLELBMENU).TrovaArticoUnMis(strDittaCorrente, strCodart)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
        Return ""
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaUnMis() As DataTable
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim dttTmp As New DataTable
    Try
      oCldArmd.CaricaUmMis(strDittaCorrente, dttTmp)
      Return dttTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function

#Region "funzioni specifiche per BNMGARTI.BNMGARMA.VB"
  Public Overridable Function ArmaApri(ByVal strDitta As String, ByVal strArmaCodart As String, ByRef dsArma As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strTmp As String = ""
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataArma(strDitta, strArmaCodart, dsArma)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTMAGA", dsArma)

      dsArmaShared = dsArma

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsArmaShared.Tables("ARTMAGA").ColumnChanging, AddressOf ArmaBeforeColUpdate
      AddHandler dsArmaShared.Tables("ARTMAGA").ColumnChanged, AddressOf ArmaAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsArmaShared.Tables("ARTMAGA").Columns("codditt").DefaultValue = strDittaCorrente
      dsArmaShared.Tables("ARTMAGA").Columns("am_codart").DefaultValue = strArmaCodart
      dsArmaShared.Tables("ARTMAGA").Columns("am_ultagg").DefaultValue = Now.ToShortDateString
      dsArmaShared.Tables("ARTMAGA").Columns("am_opnome").DefaultValue = oApp.User.Nome
      dsArmaShared.Tables("ARTMAGA").Columns("am_polriord").DefaultValue = "G"
      dsArmaShared.Tables("ARTMAGA").Columns("am_ggragg").DefaultValue = "1"

      If bArmaGesfasi Then
        dsArmaShared.Tables("ARTMAGA").Columns("am_fase").DefaultValue = strArmaUltfase
        If oCldArmd.ValCodiceDb(strArmaUltfase, strDittaCorrente, "ARTFASI", "N", strTmp, , strArmaCodart) Then
          dsArmaShared.Tables("ARTMAGA").Columns("xx_fase").DefaultValue = strTmp
        End If
      End If

      dsArmaShared.Tables("ARTMAGA").AcceptChanges()

      bArmaHasChanges = False

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
  Public Overridable Function ArmaSalva(ByVal bDelete As Boolean, ByVal strCodart As String, _
                                        ByVal strCodmaga As String, ByVal strFase As String) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not ArmaTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artmaga"
        strActLogDesLog = oApp.Tr(Me, 128574795274678240, "Articoli per magazzino")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "ARTMAGA", dsArmaShared.Tables("ARTMAGA"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        If UCase(strPrgParent) = "BNTCARTV" Then
          oCldArmd.AggiornaArtmatc(strDittaCorrente, strCodart, strCodmaga, strFase, bDelete)
        End If
        bArmaHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property ArmaRecordIsChanged() As Boolean
    Get
      Return bArmaHasChanges
    End Get
  End Property
  Public Overridable Function ArmaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dValmin As Decimal

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsArmaShared.Tables("ARTMAGA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!am_codmaga) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562811152841708, "Codice magazzino obbligatorio.")))
          Return False
        End If

        Select Case NTSCStr(dtrCurrRow(i)!am_polriord)
          Case "M", "N"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrCurrRow(i)!am_scomin) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666403086471, "Attenzione!" & vbCrLf & _
            "La 'Scorta minima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If

        Select Case NTSCStr(dtrCurrRow(i)!am_polriord)
          Case "N"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrCurrRow(i)!am_scomax) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666439805691, "Attenzione!" & vbCrLf & _
            "La 'Scorta massima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If

        Select Case NTSCStr(dtrCurrRow(i)!am_polriord)
          Case "F", "M", "O"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select

        If bNoMsgCongruenzaPolSconte = False Then
          If (NTSCStr(dtrCurrRow(i)!am_polriord) = "N") And (NTSCDec(dtrCurrRow(i)!am_scomin) >= NTSCDec(dtrCurrRow(i)!am_scomax)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130510148519073345, "La scorta massima deve essere superiore a quella minima.")))
            Return False
          End If
        End If

        If NTSCDec(dtrCurrRow(i)!am_minord) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666474649887, "Attenzione!" & vbCrLf & _
            "La 'Quantità lotto standard' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto minimo")))
          Return False
        End If

        Select Case NTSCStr(dtrCurrRow(i)!am_perragg)
          Case "G"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 999) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377646247523, "Se il Periodo di Raggruppamento indicato è 'Giorno', i giorni di raggruppamento possono essere maggiori di '1'")))
              Return False
            End If
          Case "S"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 7) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377619100298, "Se il Periodo di Raggruppamento indicato è 'Settimana', i giorni di raggruppamento possono valere da '1' a '7'.")))
              Return False
            End If
          Case "D"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 10) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377598933788, "Se il Periodo di Raggruppamento indicato è 'Decade', i giorni di raggruppamento possono valere da '1' a '10'.")))
              Return False
            End If
          Case "Q"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 15) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377570235293, "Se il Periodo di Raggruppamento indicato è 'Quindicina', i giorni di raggruppamento possono valere da '1' a '15'.")))
              Return False
            End If
          Case "M"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 31) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377544639338, "Se il Periodo di Raggruppamento indicato è 'Mese', i giorni di raggruppamento possono valere da '1' a '31'.")))
              Return False
            End If
          Case "B"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 62) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377511442160, "Se il Periodo di Raggruppamento indicato è 'Bimestre', i giorni di raggruppamento possono valere da '1' a '62'.")))
              Return False
            End If
          Case "T"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 93) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377490655142, "Se il Periodo di Raggruppamento indicato è 'Trimestre', i giorni di raggruppamento possono valere da '1' a '93'.")))
              Return False
            End If
          Case "R"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 124) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377470954013, "Se il Periodo di Raggruppamento indicato è 'Quadrimestre', i giorni di raggruppamento possono valere da '1' a '124'.")))
              Return False
            End If
          Case "U"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 186) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377445823439, "Se il Periodo di Raggruppamento indicato è 'Semestre', i giorni di raggruppamento possono valere da '1' a '186'.")))
              Return False
            End If
          Case "A"
            If Not (NTSCInt(dtrCurrRow(i)!am_ggragg) >= 0 And NTSCInt(dtrCurrRow(i)!am_ggragg) <= 366) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377423019770, "Se il Periodo di Raggruppamento indicato è 'Anno', i giorni di raggruppamento possono valere da '1' a '366'.")))
              Return False
            End If
        End Select
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsArmaShared.Tables("ARTMAGA").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND am_codart = " & CStrSQL(dtrCurrRow(0)!am_codart.ToString) & " AND am_codmaga = " & dtrCurrRow(0)!am_codmaga.ToString & _
      " AND am_fase = " & dtrCurrRow(0)!am_fase.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666616370451, "Esiste gia una riga con le stesse caratteristiche")))
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
      Return False
    End Try
  End Function
  Public Overridable Sub ArmaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsArmaShared.Tables("ARTMAGA").Rows.Add(dsArmaShared.Tables("ARTMAGA").NewRow)
      bArmaHasChanges = True

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
  Public Overridable Function ArmaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsArmaShared.Tables("ARTMAGA").Select(strFilter)(nRow).RejectChanges()
      bArmaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub ArmaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ArmaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ArmaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bArmaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ArmaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub ArmaBeforeColUpdate_am_codmaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codmaga = ""
        Return
      End If

      If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670400481387, "Codice magazzino non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        If Not bArmaGesfasi Then
          If oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTMAGA", "N", , , NTSCStr(e.Row!am_codart), NTSCStr(e.Row!am_fase)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128267311197734610, "Magazzino già esistente per l'articolo.")))
            e.ProposedValue = e.Row(e.Column.ColumnName)
          Else
            e.Row!xx_codmaga = strTmp
          End If
        Else
          e.Row!xx_codmaga = strTmp
        End If
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
  Public Overridable Sub ArmaBeforeColUpdate_am_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_fase = ""
        Return
      End If

      If bArmaGesfasi Then
        If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTFASI", "N", strTmp, , NTSCStr(e.Row!am_codart)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670446575727, "Codice fase non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
        Else
          e.Row!xx_fase = strTmp
        End If
      Else
        If Not NTSCInt(e.ProposedValue.ToString) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128564542305376215, "Codice fase deve essere 0")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
        End If
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
  Public Overridable Sub ArmaBeforeColUpdate_am_ggragg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.Row!am_perragg)
        Case "G"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 999) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "S"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 7) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "D"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 10) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "Q"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 15) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "M"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 31) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "B"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 62) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "T"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 93) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "R"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 124) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "U"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 186) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "A"
          If Not (NTSCInt(e.ProposedValue) > 0 And NTSCInt(e.ProposedValue) <= 366) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
      End Select

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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGCKIT.VB"
  Public Overridable Sub CkitOnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dReturn As Boolean = False
    Dim nNcampo As Integer
    Dim dsMaxArtkit As DataSet = Nothing
    Try
      If e.Row!ak_riga Is Nothing Then Exit Sub
      dReturn = oCldArmd.GetMaxArtkit(strDittaCorrente, NTSCStr(e.Row!ak_codart), dsMaxArtkit)

      If dsMaxArtkit.Tables("ARTKIT").Rows.Count > 0 Then
        nNcampo = NTSCInt(dsMaxArtkit.Tables("ARTKIT").Rows(0)!Riga)
      Else
        nNcampo = 0
      End If

      e.Row!ak_riga = NTSCStr(nNcampo + 1)

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
  Public Overridable Function CkitApri(ByVal strDitta As String, ByVal strCkitCodart As String, ByRef dsCkit As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataCkit(strDitta, strCkitCodart, dsCkit)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTKIT", dsCkit)

      dsCkitShared = dsCkit

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsCkitShared.Tables("ARTKIT").ColumnChanging, AddressOf CkitBeforeColUpdate
      AddHandler dsCkitShared.Tables("ARTKIT").ColumnChanged, AddressOf CkitAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsCkitShared.Tables("ARTKIT").Columns("codditt").DefaultValue = strDittaCorrente
      dsCkitShared.Tables("ARTKIT").Columns("ak_codart").DefaultValue = strCkitCodart

      dsCkitShared.Tables("ARTKIT").AcceptChanges()

      bCkitHasChanges = False

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
  Public Overridable Function CkitSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not CkitTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artkit"
        strActLogDesLog = oApp.Tr(Me, 128574804095153803, "Composizione Kit")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "ARTKIT", dsCkitShared.Tables("ARTKIT"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bCkitHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property CkitRecordIsChanged() As Boolean
    Get
      Return bCkitHasChanges
    End Get
  End Property
  Public Overridable Function CkitTestPreSalva() As Boolean
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim evt As NTSEventArgs = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrCurrRow = dsCkitShared.Tables("ARTKIT").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dtrCurrRow(i)!ak_codfigli) = " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128565382651564028, "Codice articolo obbligatorio.")))
          Return False
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If bConsentiModificaArticoliKit = False Then
        With dtrCurrRow(0)
          dtrTmp = dsCkitShared.Tables("ARTKIT").Select("codditt = " & CStrSQL(!codditt) & _
                                                        " AND ak_codart = " & CStrSQL(!ak_codart) & _
                                                        " AND ak_codfigli = " & CStrSQL(!ak_codfigli))
        End With
        If dtrTmp.Length > 1 Then
          evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128565382279562606, "Attenzione!" & vbCrLf & _
            "Articolo " & NTSCStr(dtrTmp(0)!ak_codfigli) & " già presente nella composizione kit." & vbCrLf & _
            "Reinserirlo comunque?"))
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Devo verificase se  presente un'altra riga con la stessa chiave
        '--- tenendo in considerazione chce la riga che devo salvare sempre una, potrei trovarmi nella situazione di 
        '--- nuovo record
        '--- record modificato
        '--- in entrambi i casi le righe trovate sono sempre 2,
        '--- quella precedente non modificata e quella nuova o in modifica
        '------------------------------------------------------------------------------------------------------------
        With dtrCurrRow(0)
          dtrTmp = dsCkitShared.Tables("ARTKIT").Select("codditt = " & CStrSQL(!codditt) & _
                                                        " AND ak_codart = " & CStrSQL(!ak_codart) & _
                                                        " AND ak_riga = " & !ak_riga.ToString)
        End With
        If dtrTmp.Length > 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666652933419, "Attenzione!" & vbCrLf & _
            "Esiste gia una riga con le stesse caratteristiche.")))
          Return False
        End If
      Else
        With dtrCurrRow(0)
          dtrTmp = dsCkitShared.Tables("ARTKIT").Select("codditt = " & CStrSQL(!codditt) & _
                                                        " AND ak_codart = " & CStrSQL(!ak_codart) & _
                                                        " AND ak_codfigli = " & CStrSQL(!ak_codfigli) & _
                                                        " AND ak_riga <> " & !ak_riga.ToString)
        End With
        If dtrTmp.Length > 0 Then
          evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 130302088504391529, "Attenzione!" & vbCrLf & _
            "Articolo " & NTSCStr(dtrTmp(0)!ak_codfigli) & " già presente nella composizione kit." & vbCrLf & _
            "Reinserirlo comunque?"))
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then
            Return False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Sub CkitNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsCkitShared.Tables("ARTKIT").Rows.Add(dsCkitShared.Tables("ARTKIT").NewRow)
      bCkitHasChanges = True

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
  Public Overridable Function CkitRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsCkitShared.Tables("ARTKIT").Select(strFilter)(nRow).RejectChanges()
      bCkitHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub CkitBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "ak_riga" Then
        If e.Row!ak_riga.ToString = "0" Then CkitOnAddNew(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CkitBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub CkitAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bCkitHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CkitAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub CkitBeforeColUpdate_ak_codfigli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim evt As NTSEventArgs = Nothing
    Dim strUmdapr As String
    Dim strCart As String
    Try
      If NTSCStr(e.ProposedValue) = " " Then
        e.Row!xx_codfigli = ""
        Return
      End If

      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128565303882688253, "Codice articolo non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        If NTSCStr(dttTmp.Rows(0)!ar_tipokit) <> " " Then
          evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128565307615881047, "L'articolo componente inserito |" & UCase(e.ProposedValue.ToString) & "| è di tipo kit; è da tenere presente comunque che non avviene alcuna esplosione multilivello dei kit sui docuemnti/ordini. Inserirlo comunque?"))
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        End If

        'se cambio il codice articolo modifico i campi proposti
        If NTSCStr(e.ProposedValue) <> NTSCStr(e.Row!ak_codfigli) Then

          '----------------------------------
          'Setta l'unità di misura principale
          strCart = UCase(e.ProposedValue.ToString)
          strUmdapr = UCase(NTSCStr(dttTmp.Rows(0)!ar_umdapr))
          If (Trim(NTSCStr(dttTmp.Rows(0)!ar_unmis)) = "" And Not (strCart = "D" Or strCart = "M")) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128565388267035369, "L'articolo inserito non ha l'unità di misura principale. Inserirla prima utilizzare l'articolo.")))
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          Else
            e.Row!ak_ump = NTSCStr(dttTmp.Rows(0)!ar_unmis)
            'Unità di misura da proporre
            Select Case strUmdapr
              Case "P"
                e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
              Case "S"
                If Trim(NTSCStr(dttTmp.Rows(0)!ar_unmis2)) <> "" Then
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_unmis2)
                Else
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
                End If
              Case "C"
                If Trim(NTSCStr(dttTmp.Rows(0)!ar_confez2)) <> "" Then
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_confez2)
                Else
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
                End If
              Case "Q"
                If Trim(NTSCStr(dttTmp.Rows(0)!ar_um4)) <> "" Then
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_um4)
                Else
                  e.Row!ak_unmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
                End If
            End Select
          End If
          '------------------------------------
          'Ricerca la descrizione e le note dell'articolo
          e.Row!ak_desfigli = NTSCStr(dttTmp.Rows(0)!ar_descr)
        End If

        e.Row!xx_codfigli = strTmp
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
  Public Overridable Sub CkitBeforeColUpdate_ak_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strCart As String
    Dim dQuant As Decimal
    Dim bDaMisure As Boolean
    Dim strErr As String = ""
    Try
      If Trim(NTSCStr(e.Row!ak_codfigli)) <> "" Then
        strCart = UCase(NTSCStr(e.Row!ak_codfigli))
        If (strCart = "D" Or strCart = "M") Then
          e.Row!ak_quant = NTSCStr(e.Row!ak_colli)
          e.Row!ak_ump = NTSCStr(e.ProposedValue)
        Else
          If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, strCart, NTSCStr(e.ProposedValue), NTSCDec(e.Row!ak_colli), 0, 0, 0, dQuant, bDaMisure, strErr, 3) Then
            e.Row!ak_quant = dQuant
          Else
            e.Row!ak_quant = 0
          End If
          If strErr <> "" Then
            ThrowRemoteEvent(New NTSEventArgs("", strErr))
          End If
        End If
      Else
        e.Row!ak_unmis = " "
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
  Public Overridable Sub CkitBeforeColUpdate_ak_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strCart As String
    Dim dQuant As Decimal
    Dim bDaMisure As Boolean
    Dim strErr As String = ""
    Try
      If NTSCStr(e.Row!ak_codfigli) <> "" And NTSCStr(e.Row!ak_unmis) <> "" Then
        If oCldArmd.ValCodiceDb(NTSCStr(e.Row!ak_codfigli), strDittaCorrente, "ARTICO", "S") Then
          strCart = UCase(NTSCStr(e.Row!ak_codfigli))
          If (strCart = "D" Or strCart = "M") Then
            e.Row!ak_quant = NTSCStr(e.ProposedValue)
          Else
            If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, strCart, NTSCStr(e.Row!ak_unmis), NTSCDec(e.ProposedValue), 0, 0, 0, dQuant, bDaMisure, strErr, 3) Then
              e.Row!ak_quant = dQuant
            Else
              e.Row!ak_quant = 0
            End If
            If strErr <> "" Then
              ThrowRemoteEvent(New NTSEventArgs("", strErr))
            End If
          End If
        End If
      Else
        e.Row!ak_colli = 0
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGFASI.VB"
  Public Overridable Function FasiApri(ByVal strDitta As String, ByVal strFasiCodart As String, ByRef dsFasi As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataFasi(strDitta, strFasiCodart, dsFasi)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTFASI", dsFasi)

      dsFasiShared = dsFasi

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsFasiShared.Tables("ARTFASI").ColumnChanging, AddressOf FasiBeforeColUpdate
      AddHandler dsFasiShared.Tables("ARTFASI").ColumnChanged, AddressOf FasiAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsFasiShared.Tables("ARTFASI").Columns("codditt").DefaultValue = strDittaCorrente
      dsFasiShared.Tables("ARTFASI").Columns("af_codart").DefaultValue = strFasiCodart
      dsFasiShared.Tables("ARTFASI").Columns("af_ultagg").DefaultValue = Now.ToShortDateString
      dsFasiShared.Tables("ARTFASI").Columns("af_opnome").DefaultValue = oApp.User.Nome

      dsFasiShared.Tables("ARTFASI").AcceptChanges()

      bFasiHasChanges = False

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
  Public Overridable Function FasiSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not FasiTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artfasi"
        strActLogDesLog = oApp.Tr(Me, 128574805528703708, "Fasi articolo")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "ARTFASI", dsFasiShared.Tables("ARTFASI"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bFasiHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property FasiRecordIsChanged() As Boolean
    Get
      Return bFasiHasChanges
    End Get
  End Property
  Public Overridable Function FasiTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsFasiShared.Tables("ARTFASI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1

      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsFasiShared.Tables("ARTFASI").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND af_codart = " & CStrSQL(dtrCurrRow(0)!af_codart.ToString) & " AND af_fase = " & dtrCurrRow(0)!af_fase.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666687465111, "Esiste gia una riga con le stesse caratteristiche")))
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
      Return False
    End Try
  End Function
  Public Overridable Sub FasiNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsFasiShared.Tables("ARTFASI").Rows.Add(dsFasiShared.Tables("ARTFASI").NewRow)
      bFasiHasChanges = True

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
  Public Overridable Function FasiRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsFasiShared.Tables("ARTFASI").Select(strFilter)(nRow).RejectChanges()
      bFasiHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub FasiBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "FasiBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub FasiAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bFasiHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "FasiAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub FasiBeforeColUpdate_af_codlavo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codlavo = ""
        Return
      End If

      If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLAVO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128566088712698199, "Codice lavorazione non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codlavo = strTmp
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
  Public Overridable Sub FasiBeforeColUpdate_af_codtcdc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_codtcdc = "" : Return

      If oCldArmd.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABTCDC", "N", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_tipork) = "A" Then
          e.Row!xx_codtcdc = dttTmp.Rows(0)!tb_destcdc
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701344910501, "La Tipologia entità deve essere di tipo 'Articolo\Fase'")))
          e.ProposedValue = e.Row!af_codtcdc
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701332254575, "Tipologia entità inesistente")))
        e.ProposedValue = e.Row!af_codtcdc
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
  Public Overridable Sub FasiBeforeColUpdate_af_coddica(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.ProposedValue = " " : e.Row!xx_coddica = "" : e.Row!af_coddicv = "" : Return

      If oCldArmd.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICA", "S", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_applicaa) = "A" Then
          If NTSCInt(dttTmp.Rows(0)!tb_liv) <> 1 Then
            e.Row!xx_coddica = dttTmp.Rows(0)!tb_desdica
            e.Row!af_coddicv = ""
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701312880071, "Il codice di aggregazione budget non deve essere di primo livello")))
            e.ProposedValue = e.Row!af_coddica
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701302567835, "Il codice di aggregazione budget deve essere per 'Articolo\Fase'")))
          e.ProposedValue = e.Row!af_coddica
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701289911909, "Aggregazione budget inesistente")))
        e.ProposedValue = e.Row!af_coddica
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
  Public Overridable Sub FasiBeforeColUpdate_af_coddicv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.Row!xx_coddicv = "" : e.ProposedValue = " " : Return

      If NTSCStr(e.Row!af_coddica).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701049449315, "Prima inserire un codice di aggregazione budget.")))
        e.ProposedValue = " "
        Return
      End If

      If oCldArmd.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICV", "S", , dttTmp, NTSCStr(e.Row!af_coddica)) Then
        e.Row!xx_coddicv = dttTmp.Rows(0)!tb_desdicv
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701272568603, "Valore aggregazione budget inesistente")))
        e.ProposedValue = e.Row!af_coddicv
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

  Public Overridable Function TestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Dim i As Integer = 0
    Dim bFasiGesvar As Boolean
    Dim strOut As String = ""
    Try
      If strPrgParent = "BNMGARTV" Then bFasiGesvar = True Else bFasiGesvar = False
      '----------------------------------------------------------------------------------------
      '--- All'ultimo parametro, bRoot, viene passato il flag "gbFasiGesvar", che se "True",
      '--- vuol dire che è stato chiamato da BSTCARTV altrimenti da BSMGARTI
      '----------------------------------------------------------------------------------------
      If Not oCldArmd.IsArtFasiDeletable(strDittaCorrente, strFasiCodart, NTSCInt(dtrTmp!af_fase), bFasiGesvar, strOut) Then
        ThrowRemoteEvent(New NTSEventArgs("", strOut))
        Return False
      End If
      '----------------------------------------------------------------------------------------
      '--- Se l'articolo/fase è movimentato (se esiste in ARTPROX)
      '--- avvisa e non permette la cancellazione
      '----------------------------------------------------------------------------------------
      If oCldArmd.ValCodiceDb(NTSCStr(dtrTmp!af_codart), strDittaCorrente, "ARTPROX", "S", , , NTSCStr(dtrTmp!af_fase)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128566122416076246, "L'articolo/fase che si tenta di eliminare è stato movimentato." & vbCrLf & _
            "Cancellazione non possibile.")))
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
      Return False
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGHLAP.VB"
  Public Overridable Function HlapApri(ByVal strDitta As String, ByVal strHlapCodart As String, ByRef dsHlap As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è presente il modulo 'Taglie e colori'
      '--- oppure se l'articolo non è stato creato con 'Anagrafica articoli - Taglie e colori -'
      '--- disabilita il pulsante nella toolbar per la chiamata al dettaglio quantità per taglie
      '-----------------------------------------------------------------------------------------
      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtTCO) Then bHlapTCO = True Else bHlapTCO = False
      If oCldArmd.ValCodiceDb(strHlapCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        If NTSCInt(dttTmp.Rows(0)!ar_codtagl) = 0 Then bHlapTCO = False
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataHlap(strDitta, strHlapCodart, dHlapFase, strElencoCodici, dsHlap)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTPRO", dsHlap)

      dsHlapShared = dsHlap

      HlapCaricaColonneUnbound()

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsHlapShared.Tables("ARTPRO").ColumnChanging, AddressOf HlapBeforeColUpdate
      AddHandler dsHlapShared.Tables("ARTPRO").ColumnChanged, AddressOf HlapAfterColUpdate

      bHlapHasChanges = False

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

  Public Overridable Function HlapCaricaColonneUnbound() As Boolean
    Dim i As Integer
    Dim dOrdin As Decimal
    Dim dImpeg As Decimal
    Dim dsTmp As DataSet = Nothing
    Try
      For i = 0 To dsHlapShared.Tables("ARTPRO").Rows.Count - 1
        With dsHlapShared.Tables("ARTPRO").Rows(i)

          !xx_disp = NTSCDec(!ap_esist) + NTSCDec(!ap_ordin) - NTSCDec(!ap_impeg)
          !xx_dispnet = NTSCDec(!ap_esist) - NTSCDec(!ap_prenot)
          !xx_totcarichi = (NTSCDec(!ap_carfor) + NTSCDec(!ap_carpro) + NTSCDec(!ap_carvar) + NTSCDec(!ap_rescli))
          !xx_totscarichi = (NTSCDec(!ap_scacli) + NTSCDec(!ap_scapro) + NTSCDec(!ap_scavar) + NTSCDec(!ap_resfor))

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "$", NTSCStr(!ap_magaz), dsTmp)
          dOrdin = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dOrdin = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          !xx_ordes = (NTSCDec(!ap_ordin) - dOrdin)

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "$", NTSCStr(!ap_magaz), dsTmp)
          dOrdin = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dOrdin = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          !xx_ordap = dOrdin

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "V", NTSCStr(!ap_magaz), dsTmp)
          dImpeg = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dImpeg = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          !xx_impeges = (NTSCDec(!ap_impeg) - dImpeg)

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "V", NTSCStr(!ap_magaz), dsTmp)
          dImpeg = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dImpeg = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          !xx_impegap = dImpeg

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "$", NTSCStr(!ap_magaz), dsTmp)
          dOrdin = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dOrdin = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          dOrdin = (NTSCDec(!ap_ordin) - dOrdin)

          oCldArmd.HlapGetQta(strDittaCorrente, strHlapCodart, "C", "V", NTSCStr(!ap_magaz), dsTmp)
          dImpeg = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dImpeg = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
          End If
          dImpeg = (NTSCDec(!ap_impeg) - dImpeg)
          !xx_dispones = NTSCDec(!ap_esist) + dOrdin - dImpeg

        End With
      Next

      dsHlapShared.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub HlapBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlapBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlapAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHlapHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlapAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function ArticoloGestitoAMatricole() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArmd.ArticoloGestitoAMatricole(strDittaCorrente, strHlapCodart)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGHLAT.VB"
  Public Overridable Function HlatApri(ByVal strDitta As String, ByVal strHlatCodart As String, ByRef dsHlat As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è presente il modulo 'Taglie e colori'
      '--- oppure se l'articolo non è stato creato con 'Anagrafica articoli - Taglie e colori -'
      '--- disabilita il pulsante nella toolbar per la chiamata al dettaglio quantità per taglie
      '-----------------------------------------------------------------------------------------
      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtTCO) Then bHlatTCO = True Else bHlatTCO = False
      If oCldArmd.ValCodiceDb(strHlatCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        If NTSCInt(dttTmp.Rows(0)!ar_codtagl) = 0 Then bHlatTCO = False
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataHlat(strDitta, strHlatCodart, dHlatFase, dsHlat)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTPROX", dsHlat)

      dsHlatShared = dsHlat

      HlatCaricaColonneUnbound()

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsHlatShared.Tables("ARTPROX").ColumnChanging, AddressOf HlatBeforeColUpdate
      AddHandler dsHlatShared.Tables("ARTPROX").ColumnChanged, AddressOf HlatAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsHlatShared.Tables("ARTPROX").Columns("codditt").DefaultValue = strDittaCorrente
      dsHlatShared.Tables("ARTPROX").Columns("apx_codart").DefaultValue = strHlatCodart
      dsHlatShared.Tables("ARTPROX").Columns("apx_dtulcar").DefaultValue = IntSetDate("01/01/1900")
      dsHlatShared.Tables("ARTPROX").Columns("apx_dtulsca").DefaultValue = IntSetDate("01/01/1900")
      dsHlatShared.Tables("ARTPROX").Columns("apx_fase").DefaultValue = dHlatFase

      dsHlatShared.Tables("ARTPROX").AcceptChanges()

      bHlatHasChanges = False

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
  Public Overridable Function HlatSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not HlatTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artprox"
        strActLogDesLog = oApp.Tr(Me, 128574806683191100, "Progressivi Totali")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "ARTPROX", dsHlatShared.Tables("ARTPROX"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bHlatHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property HlatRecordIsChanged() As Boolean
    Get
      Return bHlatHasChanges
    End Get
  End Property
  Public Overridable Function HlatTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsHlatShared.Tables("ARTPROX").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1

      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsHlatShared.Tables("ARTPROX").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND apx_codart = " & CStrSQL(dtrCurrRow(0)!apx_codart.ToString) & " AND apx_fase = " & dtrCurrRow(0)!apx_fase.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666717621747, "Esiste gia una riga con le stesse caratteristiche")))
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
      Return False
    End Try
  End Function
  Public Overridable Sub HlatNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsHlatShared.Tables("ARTPROX").Rows.Add(dsHlatShared.Tables("ARTPROX").NewRow)
      bHlatHasChanges = True

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
  Public Overridable Function HlatRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsHlatShared.Tables("ARTPROX").Select(strFilter)(nRow).RejectChanges()
      bHlatHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function HlatCaricaColonneUnbound() As Boolean
    Dim i As Integer
    Dim dOrdin As Decimal
    Dim dImpeg As Decimal
    Dim dEsist As Decimal
    Dim dPrenot As Decimal
    Dim dValore As Decimal
    Dim dsTmp As DataSet = Nothing
    Try
      For i = 0 To dsHlatShared.Tables("ARTPROX").Rows.Count - 1
        With dsHlatShared.Tables("ARTPROX").Rows(i)
          dEsist = NTSCDec(!apx_esist)
          dOrdin = NTSCDec(!apx_ordin)
          dImpeg = NTSCDec(!apx_impeg)
          dPrenot = NTSCDec(!apx_prenot)
          !xx_disp = (dEsist + dOrdin - dImpeg)
          !xx_dispnet = (dEsist - dPrenot)
          '--- Calcola ordinato/ordinato aperto da MOVORD
          oCldArmd.HlatGetQta(strDittaCorrente, strHlatCodart, "C", "$", dsTmp)
          dOrdin = 0
          dValore = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dOrdin = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Valore) = 0 Then dValore = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Valore)
          End If
          !xx_ordin = (NTSCDec(!apx_ordin) - dOrdin)
          !xx_ordinap = dOrdin
          !xx_vordinap = dValore
          !xx_vordin = (NTSCDec(!apx_vordin) - NTSCDec(!xx_vordinap))

          '--- Calcola impegnato/impegnato aperto da MOVORD
          oCldArmd.HlatGetQta(strDittaCorrente, strHlatCodart, "C", "V", dsTmp)
          dImpeg = 0
          dValore = 0
          If dsTmp.Tables("MOVORD").Rows.Count > 0 Then
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita) = 0 Then dImpeg = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Quantita)
            If Not NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Valore) = 0 Then dValore = NTSCDec(dsTmp.Tables("MOVORD").Rows(0)!Valore)
          End If
          !xx_impeg = (NTSCDec(!apx_impeg) - dImpeg)
          !xx_impegap = dImpeg
          !xx_vimpegap = dValore
          !xx_vimpeg = (NTSCDec(!apx_vimpeg) - NTSCDec(!xx_vimpegap))
          !xx_dispesec = (NTSCDec(!apx_esist) + NTSCDec(!xx_ordin) - NTSCDec(!xx_impeg))

          If NTSCDec(!apx_qtalif) <> 0 Then
            !xx_costmed = NTSCDec(!apx_vqtalif) / NTSCDec(!apx_qtalif) * dHlatPerqta
          Else
            If NTSCDec(!apx_giaini) <> 0 Then
              !xx_costmed = NTSCDec(!apx_vgiaini) / NTSCDec(!apx_giaini) * dHlatPerqta
            Else
              !xx_costmed = 0
            End If
          End If
        End With
      Next

      dsHlatShared.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub HlatBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlatBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlatAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHlatHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlatAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGBARC.VB"
  Public Overridable Function BarcApri(ByVal strDitta As String, ByVal strBarcCodart As String, ByRef dsBarc As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strTmp As String = ""
    Dim lOrins As Integer
    Dim strOra As String
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataBarc(strDitta, strBarcCodart, dsBarc)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("BARCODE", dsBarc)

      dsBarcShared = dsBarc

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsBarcShared.Tables("BARCODE").ColumnChanging, AddressOf BarcBeforeColUpdate
      AddHandler dsBarcShared.Tables("BARCODE").ColumnChanged, AddressOf BarcAfterColUpdate

      strOra = Format(Now.ToShortTimeString, "hh:mm:ss")
      lOrins = NTSCInt(Mid(strOra, 1, 2)) * 10000
      lOrins = lOrins + NTSCInt(Mid$(strOra, 4, 2)) * 100
      lOrins = lOrins + NTSCInt(Mid$(strOra, 7, 2))

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsBarcShared.Tables("BARCODE").Columns("codditt").DefaultValue = strDittaCorrente
      dsBarcShared.Tables("BARCODE").Columns("bc_codart").DefaultValue = strBarcCodart
      dsBarcShared.Tables("BARCODE").Columns("bc_quant").DefaultValue = "1"
      dsBarcShared.Tables("BARCODE").Columns("bc_unmis").DefaultValue = strBarcUnmis
      dsBarcShared.Tables("BARCODE").Columns("bc_datins").DefaultValue = Now.ToShortDateString
      dsBarcShared.Tables("BARCODE").Columns("bc_orins").DefaultValue = lOrins
      dsBarcShared.Tables("BARCODE").Columns("bc_datagg").DefaultValue = Now.ToShortDateString
      dsBarcShared.Tables("BARCODE").Columns("bc_oragg").DefaultValue = lOrins

      If (UCase(strPrgParent) = "BNTCARTV") And (strBarcPrimaTaglia <> "") Then
        dsBarcShared.Tables("BARCODE").Columns("bc_tagl").DefaultValue = strBarcPrimaTaglia
      End If
      dsBarcShared.Tables("BARCODE").Columns("bc_fase").DefaultValue = dBarcFase
      If oCldArmd.ValCodiceDb(NTSCStr(dBarcFase), strDittaCorrente, "ARTFASI", "N", strTmp, , strBarcCodart) Then
        dsBarcShared.Tables("BARCODE").Columns("xx_fase").DefaultValue = strTmp
      End If

      dsBarcShared.Tables("BARCODE").AcceptChanges()

      bBarcHasChanges = False

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
  Public Overridable Function BarcSalva(ByVal bDelete As Boolean) As Boolean
    Dim lOra, i As Integer
    Dim dtrCurrRow() As DataRow
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not BarcTestPreSalva() Then Return False

        'Aggiorna la l'ora di inserimento\aggiornamento
        lOra = Now.Hour * 10000
        lOra = lOra + Now.Minute * 100
        lOra = lOra + Now.Second

        dtrCurrRow = dsBarcShared.Tables("BARCODE").Select(Nothing, Nothing, DataViewRowState.Added)
        For i = 0 To dtrCurrRow.Length - 1
          dtrCurrRow(i)!bc_orins = lOra
          dtrCurrRow(i)!bc_oragg = lOra
        Next
        dtrCurrRow = dsBarcShared.Tables("BARCODE").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
        For i = 0 To dtrCurrRow.Length - 1
          dtrCurrRow(i)!bc_oragg = lOra
          dtrCurrRow(i)!bc_datagg = Now.ToShortDateString
        Next
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "barcode"
        strActLogDesLog = oApp.Tr(Me, 128574808063815534, "Codici a barre")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "BARCODE", dsBarcShared.Tables("BARCODE"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bBarcHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property BarcRecordIsChanged() As Boolean
    Get
      Return bBarcHasChanges
    End Get
  End Property
  Public Overridable Function BarcTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim dttBc As New DataTable
    Dim i As Integer = 0
    Dim dQta As Decimal

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsBarcShared.Tables("BARCODE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dtrCurrRow(i)!bc_code) = " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571431322035088, "Codice barcode obbligatorio.")))
          Return False
        End If

        If Not InStr(1, NTSCStr(dtrCurrRow(i)!bc_code), "'") = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571444123213804, "Il Barcode non può contenere apici semplici.")))
          Return False
        End If

        If (bBarcGesfasi = True) And (NTSCInt(dtrCurrRow(i)!bc_fase) = 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571437799061992, "Indicare una fase valida.")))
          Return False
        End If

        dQta = NTSCDec(dtrCurrRow(i)!bc_quant)

        If Not (bBarcodeDerogaQtaBOLL OrElse bBarcodeDerogaQtaGSOR OrElse NTSCStr(dtrCurrRow(i)!bc_tipo) = "U") Then
          If Not ((dQta = 1 AndAlso NTSCStr(dtrCurrRow(i)!bc_unmis) = strBarcUnmis) OrElse _
                  (dQta = dBarcQtacon2 AndAlso dBarcQtacon2 <> 0 AndAlso NTSCStr(dtrCurrRow(i)!bc_unmis) = strBarcConfez2) OrElse _
                  (dQta = dBarcConver AndAlso dBarcConver <> 0 AndAlso NTSCStr(dtrCurrRow(i)!bc_unmis) = strBarcunmis2)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670698610203, "Quantità non prevista.")))
            Return False
          End If
        End If

        'Verifica se lo stesso barcode è in uso da un altro articolo
        oCldArmd.VerificaBarcodeAltriArticoli(strDittaCorrente, NTSCStr(dtrCurrRow(i)!bc_code), dttBc)
        If dttBc.Rows.Count > 0 Then
          If dttBc.Rows.Count = 1 And NTSCStr(dttBc.Rows(0)!bc_codart).ToUpper = strBarcCodart.ToUpper And dtrCurrRow(i).RowState = DataRowState.Modified Then
            'è una modifica all'unico barcode esistente sull'articolo corrente: posso proseguire
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129418076984311079, "Il barcode è già in uso sull'articolo '|" & NTSCStr(dttBc.Rows(0)!bc_codart) & "|'")))
            Return False
          End If
        End If
      Next

      dtrTmp = dsBarcShared.Tables("BARCODE").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
                                                     " AND bc_code = " & CStrSQL(dtrCurrRow(0)!bc_code.ToString) & _
                                                     " AND bc_codart = " & CStrSQL(dtrCurrRow(0)!bc_codart.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita per l'articolo
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571436040373178, "Esiste gia un barcode per questo articolo")))
        Return False
      End If

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsBarcShared.Tables("BARCODE").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND bc_code = " & CStrSQL(dtrCurrRow(0)!bc_code.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868666744653343, "Esiste gia una riga con le stesse caratteristiche")))
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
      Return False
    End Try
  End Function
  Public Overridable Sub BarcNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsBarcShared.Tables("BARCODE").Rows.Add(dsBarcShared.Tables("BARCODE").NewRow)
      bBarcHasChanges = True

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
  Public Overridable Function BarcRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsBarcShared.Tables("BARCODE").Select(strFilter)(nRow).RejectChanges()
      bBarcHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function GetTabTagl(ByRef dttTmp As DataTable) As Boolean
    Try
      If Not oCldArmd.ValCodiceDb(NTSCStr(nBarcCodtagl), strDittaCorrente, "TABTAGL", "N", , dttTmp) Then
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

  Public Overridable Function CheckDigit(ByVal strCodice As String) As String
    Dim nI As Integer
    Dim nCurNum As Integer
    Dim nSumDisp As Integer
    Dim nSumPari As Integer
    Dim nSumTot As Integer
    Try
      CheckDigit = ""
      nSumPari = 0
      nSumDisp = 0
      If Len(strCodice) <> 8 And Len(strCodice) <> 13 Then
        Exit Function
      End If
      'Per adesso il calcolo dell'ultima cifra fa riferimento a codici che comprendono
      'solo numeri
      For nI = 1 To Len(strCodice)
        If Not ((Asc(Mid(strCodice, nI, 1)) >= 48) And (Asc(Mid(strCodice, nI, 1)) <= 57)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571232711073609, "I codici EAN8 ed EAN13 non possono contenere caratteri")))
          Return ""
        End If
      Next
      If Len(strCodice) = 8 Then
        strCodice = "00000" & strCodice
      End If

      For nI = 1 To Len(strCodice) - 1
        nCurNum = NTSCInt(Mid(strCodice, nI, 1))
        'Verifico che il numero corrente sia pari o dispari
        If (nI Mod 2) = 0 Then
          nSumPari = nSumPari + nCurNum
        Else
          nSumDisp = nSumDisp + nCurNum
        End If
      Next
      nSumPari = nSumPari * 3
      nSumTot = nSumPari + nSumDisp
      Return Right(NTSCStr(10 - NTSCInt(Right(NTSCStr(nSumTot), 1))), 1)

    Catch ex As Exception
      CheckDigit = ""
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GeneraEAN(ByRef strCode As String, ByRef strTipo As String, ByVal nQuale As Integer) As Boolean
    Dim strDummy As String
    Dim lProgr As Integer
    Dim lProgrTmp As Integer
    Dim strLastDigit As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se è già stato indicato un barcode nella riga salta la creazione del codice
      '-----------------------------------------------------------------------------------------
      If Not (strCode = "" Or strCode = " ") Then Return False
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = oCldArmd.LegNuma(strDittaCorrente, "BC", " ", 0, True)
      lProgrTmp = lProgr
      '-----------------------------------------------------------------------------------------
      If nQuale <> EAN8 Then
        If bIndicod = False Then
          'If lProgr > 99999 Then
          'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129179715365733730, "Attenzione!" & vbCrLf & _
          '  "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni', supera i 99999." & vbCrLf & _
          '  "Generazione EAN13 non possibile.")))
          'Return False
          'End If
        Else
          If (lProgr.ToString & strPrefixEAN13).Length > 12 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129183139072752460, "Attenzione!" & vbCrLf & _
              "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni' " & _
              "insieme al valore indicato in 'BSMGARTI/OPZIONI/PrefixEAN13', nel Registro di Business, " & _
              "supera i 12 caratteri." & vbCrLf & _
              "Generazione EAN13 non possibile.")))
            Return False
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla quale tipo di EAN è stato scelto di creare (EAN8 o EAN13)
      '-----------------------------------------------------------------------------------------
      strDummy = GeneraBarcode(lProgr, nQuale, strPrefixEAN13)
      '-----------------------------------------------------------------------------------------
      '--- Determina il CheckDigit finale
      '-----------------------------------------------------------------------------------------
      strLastDigit = CheckDigit(strDummy & "0")
      '-----------------------------------------------------------------------------------------
      '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      If strLastDigit <> "" Then
        strDummy = strDummy & strLastDigit
        oCldArmd.AggNuma(strDittaCorrente, "BC", " ", 0, lProgr, True, True, "")
      End If
      '-----------------------------------------------------------------------------------------
      '--- Copia il codice creato nella colonna relativa
      '-----------------------------------------------------------------------------------------
      strCode = strDummy
      '-----------------------------------------------------------------------------------------
      '--- Determina il tipo di barcode secondo quello appena creato
      '-----------------------------------------------------------------------------------------
      If nQuale = EAN8 Then
        strTipo = "8"
      Else
        strTipo = "E"
      End If
      '-----------------------------------------------------------------------------------------

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
  Public Overridable Function ControllaEAN(ByRef strCode As String, ByRef strTipo As String, ByVal nQuale As Integer) As Boolean
    Dim strLastDigit As String
    Dim strQuale As String
    Dim nI As Integer
    Dim evt As NTSEventArgs = Nothing
    Try
      For nI = 1 To Len(strCode)
        If Not ((Asc(Mid(strCode, nI, 1)) >= 48) And (Asc(Mid(strCode, nI, 1)) <= 57)) Then
          'Controlla se è un codice a PESO o VALORE
          If Len(strCode) = 13 And (Mid(UCase(strCode), 7, 6) = "QQQQQQ" Or Mid(UCase(strCode), 8, 5) = "QQQQQ" Or Mid(UCase(strCode), 7, 6) = "VVVVVV" Or Mid(UCase(strCode), 8, 5) = "VVVVV") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571217313845412, "Codice EAN13 a peso o valore.")))
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571216045596864, "I codici EAN8 ed EAN13 non possono contenere caratteri")))
          End If
          Exit Function
        End If
      Next
      If nQuale = 1 Then
        strQuale = "EAN8"
        If Len(strCode) <> 8 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571216267767224, "Il codice |" & strCode & "| non è di tipo EAN8")))
          Exit Function
        End If
      Else
        strQuale = "EAN13"
        If Len(strCode) <> 13 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571216486808424, "Il codice |" & strCode & "| non è di tipo EAN13")))
          Exit Function
        End If
      End If
      strLastDigit = CheckDigit(strCode)
      If (strLastDigit = " ") Or (strLastDigit = "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571216697244434, "Il codice da controllare non si riferisce all'|" & strQuale & "|")))
        Exit Function
      End If
      If strLastDigit = Right(strCode, 1) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571216922856870, "Il codice |" & strQuale & "| è corretto")))
        Exit Function
      Else
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128299074407774121, "Il codice |" & strQuale & "| non è corretto. L'ultima cifra dovrebbe essere '|" & strLastDigit & "|', sostituire?"))
        ThrowRemoteEvent(evt)
        If evt.RetValue = "YES" Then
          strCode = Left(strCode, Len(strCode) - 1) & strLastDigit
        Else
          Exit Function
        End If
      End If
      If nQuale = 1 Then
        strTipo = "8"
      Else
        strTipo = "E"
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
  Public Overridable Function RecordGeneraEANPerTaglia(ByVal nQuale As Integer) As Boolean
    Dim bRigheInserite As Boolean
    Dim i As Integer
    Dim lProgr As Integer
    Dim lOrins As Integer
    Dim strCampo As String
    Dim strDummy As String
    Dim strLastDigit As String
    Dim strOra As String
    Dim dsTmp As DataSet = Nothing
    Dim nRec As Integer
    Try
      '-----------------------------------------------------------------------------------------
      If UCase(strPrgParent) <> "BNTCARTV" Then Return False
      '-----------------------------------------------------------------------------------------
      If strBarcPrimaTaglia = "" Then Return False
      '-----------------------------------------------------------------------------------------
      bRigheInserite = False
      '-----------------------------------------------------------------------------------------
      '--- Determina l'ora da inserire nelle nuove righe generate
      '-----------------------------------------------------------------------------------------
      strOra = Format(Now.ToShortTimeString, "hh:mm:ss")
      lOrins = NTSCInt(Mid(strOra, 1, 2)) * 10000
      lOrins = lOrins + NTSCInt(Mid$(strOra, 4, 2)) * 100
      lOrins = lOrins + NTSCInt(Mid$(strOra, 7, 2))
      '-----------------------------------------------------------------------------------------
      oCldArmd.GetTabtagl(strDittaCorrente, nBarcCodtagl, dsTmp)

      If dsTmp.Tables("TABTAGL").Rows.Count > 0 Then
        For i = 1 To 24
          strCampo = "tb_dest" & Right("00" & NTSCStr(i), 2)
          If Trim(NTSCStr(dsTmp.Tables("TABTAGL").Rows(0)(strCampo))) <> "" Then
            '-----------------------------------------------------------------------------------
            '--- Legge l'ultimo progressivo da TABNUMA
            '-----------------------------------------------------------------------------------
            lProgr = oCldArmd.LegNuma(strDittaCorrente, "BC", " ", 0, True)
            '-----------------------------------------------------------------------------------
            strDummy = GeneraBarcode(lProgr, nQuale, strPrefixEAN13)
            '-----------------------------------------------------------------------------------
            '--- Determina il CheckDigit finale
            '-----------------------------------------------------------------------------------
            strLastDigit = CheckDigit(strDummy & "0")
            '-----------------------------------------------------------------------------------
            '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
            '-----------------------------------------------------------------------------------
            If strLastDigit <> "" Then
              strDummy = strDummy & strLastDigit
              oCldArmd.AggNuma(strDittaCorrente, "BC", " ", 0, lProgr, True, True, "")
            End If
            '-----------------------------------------------------------------------------------
            '--- Inserisce la riga in BARCODE
            '-----------------------------------------------------------------------------------
            oCldArmd.InsertBarcode(strDittaCorrente, strBarcCodart, strDummy, strBarcUnmis, _
                                   lOrins, NTSCStr(IIf(nQuale = EAN8, "8", "E")), _
                                   NTSCStr(dsTmp.Tables("TABTAGL").Rows(0)(strCampo)), nRec)
            '-----------------------------------------------------------------------------------
            If (nRec <> 0) And (bRigheInserite = False) Then bRigheInserite = True
            '-----------------------------------------------------------------------------------
          End If
        Next
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se sono state inserite delle righe, chiude e riapre il recordset su BARCODE
      '-----------------------------------------------------------------------------------------
      If Not bRigheInserite = True Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571223393073195, "Non sono state generate righe di tipo EAN" & NTSCStr(IIf(nQuale = EAN8, "8.", "13.")))))
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
  Public Overridable Function GeneraEAN13Variabili(ByRef strCode As String, ByRef strTipo As String, ByVal nTipo As Integer, ByVal rs As DataRowState) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      Return GeneraEAN13Variabili(strCode, strTipo, nTipo)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function GeneraEAN13Variabili(ByRef strCode As String, ByRef strTipo As String, ByVal nTipo As Integer) As Boolean
    'dove nTipo=1 se a peso, nTipo=2 se valore
    Dim strDummy As String
    Dim lProgr As Integer
    Dim lProgrTmp As Integer
    Dim j As Integer
    Dim strFull As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se è già stato indicato un barcode nella riga salta la creazione del codice
      '-----------------------------------------------------------------------------------------
      If strCode.Trim <> "" Then
        'Se quanto è già indicato  nel campo è < a 6 caratteri lo accoda
        strDummy = strCode
        If Len(strDummy) <= 6 And IsNumeric(strDummy) Then
          strFull = ""
          For j = 1 To 6 - strDummy.Length 'carica la lista degli 0
            strFull += "0"
          Next
          If nTipo = 1 Then
            strDummy += strFull & "QQQQQQ0"
          Else
            strDummy += strFull & "VVVVVV0"
          End If
          strCode = strDummy
          strTipo = "E"
          Return True
        Else
          If strCode <> "" Then Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = oCldArmd.LegNuma(strDittaCorrente, "BC", " ", 0, True)
      lProgrTmp = lProgr
      '-----------------------------------------------------------------------------------------
      '--- Lo genera a seconda se è a peso o valore
      '-----------------------------------------------------------------------------------------
      If nTipo = 1 Then
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000")) & "QQQQQQ0"
      Else
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000")) & "VVVVVV0"
      End If
      '-----------------------------------------------------------------------------------------
      '--- aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      oCldArmd.AggNuma(strDittaCorrente, "BC", " ", 0, lProgr, True, True, "")
      '-----------------------------------------------------------------------------------------
      '--- Copia il codice creato nella colonna relativa
      '-----------------------------------------------------------------------------------------
      strCode = strDummy
      '-----------------------------------------------------------------------------------------
      '--- imposta il tipo di barcode secondo quello appena creato
      '-----------------------------------------------------------------------------------------
      strTipo = "E"
      '-----------------------------------------------------------------------------------------

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

  Public Overridable Function GeneraEANGiftCard(ByVal nBarcode As Integer, ByVal nQuale As Integer) As Boolean
    Dim strDummy As String
    Dim lProgr, i As Integer
    Dim lProgrTmp As Integer
    Dim strLastDigit As String
    Dim strUnMis As String = ""
    Dim strPrefixGiftCardTmp As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      For i = 0 To nBarcode - 1 'Più lento, ma così viene controllato ogni singolo codice 
        strPrefixGiftCardTmp = strPrefixEAN13GiftCard
        lProgr = oCldArmd.LegNuma(strDittaCorrente, "BG", " ", 0, True) 'Numeratore specifico Gift Card
        lProgrTmp = lProgr
        '-----------------------------------------------------------------------------------------
        If nQuale <> EAN8 Then
          If bIndicod = False Then
            If lProgr > 99999 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129992581538622907, "Attenzione!" & vbCrLf & _
                "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni', supera i 99999." & vbCrLf & _
                "Generazione EAN13 non possibile.")))
              Return False
            End If
          Else
            If (lProgr.ToString & strPrefixGiftCardTmp).Length > 12 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129992581938545983, "Attenzione!" & vbCrLf & _
                "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni' " & _
                "insieme al valore indicato in 'BSMGARTI/OPZIONI/PrefixEAN13GiftCard', nel Registro di Business, " & _
                "supera i 12 caratteri." & vbCrLf & _
                "Generazione EAN13 non possibile.")))
              Return False
            End If
          End If
        End If

        strDummy = GeneraBarcode(lProgr, nQuale, strPrefixGiftCardTmp)
        '-----------------------------------------------------------------------------------------
        '--- Determina il CheckDigit finale
        '-----------------------------------------------------------------------------------------
        strLastDigit = CheckDigit(strDummy & "0")
        '-----------------------------------------------------------------------------------------
        '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
        '-----------------------------------------------------------------------------------------
        If i = 0 Then
          Dim dttTmp As New DataTable
          oCldArmd.ValCodiceDb(strBarcCodart, strDittaCorrente, "ARTICO", "S", , dttTmp)
          strUnMis = NTSCStr(dttTmp.Rows(0)!ar_unmis)

          Dim dttRange As New DataTable
          If Not oCldArmd.CheckRangeBarcode(strDittaCorrente, strDummy, NTSCStr(NTSCDec(strDummy) + nBarcode), dttRange) Then Return False

          If dttRange.Rows.Count > 0 Then
            Dim strMsg As String = ""
            For k As Integer = 0 To dttRange.Rows.Count - 1
              strMsg &= oApp.Tr(Me, 129992604088242945, " - Barcode |" & NTSCStr(dttRange.Rows(k)!bc_code) & "|, Articolo |" & NTSCStr(dttRange.Rows(k)!bc_codart) & "|") & vbCrLf
            Next

            'Se sono meno di 5 faccio apparire il messaggio, altrimenti indico il tutto nel file di log
            If dttRange.Rows.Count < 5 Then
              Dim evnt As New NTSEventArgs(ThMsg.MSG_NOYES, oApp.Tr(Me, 129992604799897925, "I seguenti barcode non saranno generati perchè già presenti:") & vbCrLf & strMsg & _
                                                            oApp.Tr(Me, 129992606285546049, "Procedere comunque con la generazione?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue <> ThMsg.RETVALUE_YES Then Return False
            Else
              Dim swWriter As New System.IO.StreamWriter(oApp.AscDir & "\BarcodeGiftCard.txt")
              swWriter.WriteLine(oApp.Tr(Me, 129992617866401864, "I seguenti barcode non saranno generati perchè già presenti:"))
              swWriter.Write(strMsg)
              swWriter.Flush()
              swWriter.Close()
              Shell("notepad.exe " & oApp.AscDir & "\BarcodeGiftCard.txt")
              Dim evnt As New NTSEventArgs(ThMsg.MSG_NOYES, oApp.Tr(Me, 129992606540366965, "Sono stati rilevati dei barcode già in uso da altri articoli, controllare il file di log prima di scegliere se procedere comunque con la generazione."))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue <> ThMsg.RETVALUE_YES Then Return False
              Try
                System.IO.File.Delete(oApp.AscDir & "\BarcodeGiftCard.txt")
              Catch ex As Exception
                'In caso di fallimento non do comunque errore (magari il file di log è rimasto aperto)
              End Try
            End If
          End If
        End If

        If strLastDigit <> "" Then
          strDummy = strDummy & strLastDigit

          oCldArmd.SalvaBarcode(strDittaCorrente, strBarcCodart, strUnMis, strDummy)

          oCldArmd.AggNuma(strDittaCorrente, "BG", " ", 0, lProgr, True, True, "")
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

  Public Overridable Sub BarcBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BarcBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub BarcAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bBarcHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BarcAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BarcBeforeColUpdate_bc_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!bc_codart) = "D" Or NTSCStr(e.Row!bc_codart) = "M" Then Return
      If Not NTSCStr(e.ProposedValue) = strBarcUnmis Then
        If NTSCStr(e.ProposedValue) = strBarcConfez2 Then
          e.Row!bc_quant = dBarcQtacon2
        Else
          e.Row!bc_quant = dBarcConver
        End If
      Else
        e.Row!bc_quant = "1"
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
  Public Overridable Sub BarcBeforeColUpdate_bc_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_fase = ""
        Return
      End If

      If bBarcGesfasi Then
        If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTFASI", "N", strTmp, , NTSCStr(e.Row!bc_codart)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571442179449214, "Codice fase non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
        Else
          e.Row!xx_fase = strTmp
        End If
      Else
        If Not NTSCInt(e.ProposedValue.ToString) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571442201333174, "Codice fase deve essere 0")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
        End If
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

  Public Overridable Function BarcCaricaUnitaMisura() As DataTable
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim dttTmp As New DataTable
    Try
      oCldArmd.BarcCaricaUnitaMisura(strDittaCorrente, strBarcCodart, dttTmp)
      Return dttTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function


  Public Overridable Function GeneraBarcode(ByVal lProgr As Integer, ByVal nQuale As Integer, ByVal strPrefix As String) As String
    Dim lProgrTmp As Integer = lProgr
    Dim strDummy As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla quale tipo di EAN è stato scelto di creare (EAN8 o EAN13)
      '-----------------------------------------------------------------------------------------
      If nQuale = EAN8 Then
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "000000"))
      Else
        '--- Se non c'è l'ipostazione di registro per la creazione da Indicod
        '--- funziona come prima
        If bIndicod = False Then
          strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000000000"))
        Else
          If Len(strPrefix) < 12 Then
            If Len(strPrefix & NTSCStr(lProgrTmp)) < 12 Then
              Do
                strPrefix = strPrefix & "0"
              Loop Until Len(strPrefix & NTSCStr(lProgrTmp)) = 12
              strDummy = strPrefix & NTSCStr(lProgrTmp)
            Else
              If Len(strPrefix & NTSCStr(lProgrTmp)) > 12 Then
                If Len(NTSCStr(lProgrTmp)) > 1 Then
                  Do
                    lProgrTmp = NTSCInt(Right(NTSCStr(lProgrTmp), (Len(NTSCStr(lProgrTmp)) - 1)))
                  Loop Until Len(strPrefix & NTSCStr(lProgrTmp)) = 12
                  strDummy = strPrefix & NTSCStr(lProgrTmp)
                Else
                  strDummy = Left(strPrefix, 12)
                End If
              Else
                strDummy = strPrefix & NTSCStr(lProgrTmp)
              End If
            End If
          Else
            If Len(strPrefix) > 12 Then
              strDummy = Left(strPrefix, 12)
            Else
              strDummy = strPrefix
            End If
          End If
        End If
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
    Return strDummy
  End Function

#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGHLAV.VB"
  Public Overridable Function HlavApri(ByVal strDitta As String, ByVal strHlavCodart As String, ByRef dsHlav As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataHlav(strDitta, strHlavCodart, dsHlav)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("ARTVAL", dsHlav)

      dsHlavShared = dsHlav

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsHlavShared.Tables("ARTVAL").ColumnChanging, AddressOf HlavBeforeColUpdate
      AddHandler dsHlavShared.Tables("ARTVAL").ColumnChanged, AddressOf HlavAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsHlavShared.Tables("ARTVAL").Columns("codditt").DefaultValue = strDittaCorrente
      dsHlavShared.Tables("ARTVAL").Columns("ax_codart").DefaultValue = strHlavCodart
      dsHlavShared.Tables("ARTVAL").Columns("ax_ultagg").DefaultValue = Now.ToShortDateString

      dsHlavShared.Tables("ARTVAL").AcceptChanges()

      bHlavHasChanges = False

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
  Public Overridable Function HlavSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim evt As NTSEventArgs = Nothing
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not HlavTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artval"
        strActLogDesLog = oApp.Tr(Me, 128574809339401726, "Descrizione articolo in lingua straniera")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "ARTVAL", dsHlavShared.Tables("ARTVAL"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bHlavHasChanges = False
      End If

      If bDelete Then
        'TCO
        If (strPrgParent = "BNTCARTV" Or strPrgParent = "BNMGARTV") And strHlavCartellaColore <> "" And bHlavIsRoot = True Then
          'Cancellazione
          evt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 129966027339124698, "Cancello la descrizione in lingua su tutte le varianti?"))
          ThrowRemoteEvent(evt)
          If evt.RetValue = "NO" Then
            'Procedo... 
          Else
            'Aggiorno le descrizioni in lingua
            oCldArmd.AggiornaDescrizioniInLingua(strDittaCorrente, strHlavCodart, strHlavCartellaColore, 0, "", True)
          End If
        End If
      End If

      Return bResult
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
  Public ReadOnly Property HlavRecordIsChanged() As Boolean
    Get
      Return bHlavHasChanges
    End Get
  End Property
  Public Overridable Function HlavTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim evt As NTSEventArgs = Nothing
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsHlavShared.Tables("ARTVAL").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!ax_codvalu) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571557152966644, "Codice lingua obbligatorio.")))
          Return False
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsHlavShared.Tables("ARTVAL").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND ax_codart = " & CStrSQL(dtrCurrRow(0)!ax_codart.ToString) & " AND ax_codvalu = " & dtrCurrRow(0)!ax_codvalu.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670231260471, "Esiste già una riga con le stesse caratteristiche.")))
        Return False
      End If

      'TCO
      If (strPrgParent = "BNTCARTV" Or strPrgParent = "BNMGARTV") And strHlavCartellaColore <> "" And bHlavIsRoot = True Then
        dtrCurrRow = dsHlavShared.Tables("ARTVAL").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
        If dtrCurrRow.Length > 0 Then
          If dtrCurrRow(0).RowState = DataRowState.Added Then
            'Nuovo
            evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 129966027339124700, "Genero la descrizione in lingua su tutte le varianti?"))
            ThrowRemoteEvent(evt)
          Else
            'Modifica
            If UCase(NTSCStr(dtrCurrRow(0)!ax_descr)) <> UCase(NTSCStr(dtrCurrRow(0)("ax_descr", DataRowVersion.Original))) Then
              evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 129966027339124699, "Modifico la descrizione in lingua su tutte le varianti?"))
              ThrowRemoteEvent(evt)
            Else
              Return True
            End If
          End If
          If evt.RetValue = "NO" Then
            Return True
          End If
          'Aggiorna descrizioni in lingua
          oCldArmd.AggiornaDescrizioniInLingua(strDittaCorrente, strHlavCodart, strHlavCartellaColore, NTSCInt(dtrCurrRow(0)!ax_codvalu), NTSCStr(dtrCurrRow(0)!ax_descr), False)
        End If
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
      Return False
    End Try
  End Function
  Public Overridable Sub HlavNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsHlavShared.Tables("ARTVAL").Rows.Add(dsHlavShared.Tables("ARTVAL").NewRow)
      bHlavHasChanges = True

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
  Public Overridable Function HlavRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsHlavShared.Tables("ARTVAL").Select(strFilter)(nRow).RejectChanges()
      bHlavHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub HlavBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlavBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlavAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHlavHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlavAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub HlavBeforeColUpdate_ax_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codvalu = ""
        Return
      End If

      If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLING", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670482357435, "Codice lingua non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codvalu = strTmp
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGCACF.VB"
  Public Overridable Function CacfApri(ByVal strDitta As String, ByVal strCacfCodart As String, ByRef dsCacf As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataCacf(strDitta, strCacfCodart, dsCacf)
      If dReturn = False Then Return False

      oCldArmd.SetTableDefaultValueFromDB("CODARFO", dsCacf)

      dsCacfShared = dsCacf

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldArmd.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsCacfShared.Tables("CODARFO").ColumnChanging, AddressOf CacfBeforeColUpdate
      AddHandler dsCacfShared.Tables("CODARFO").ColumnChanged, AddressOf CacfAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsCacfShared.Tables("CODARFO").Columns("codditt").DefaultValue = strDittaCorrente
      dsCacfShared.Tables("CODARFO").Columns("caf_codart").DefaultValue = strCacfCodart
      dsCacfShared.Tables("CODARFO").Columns("caf_ultagg").DefaultValue = Now.ToShortDateString
      dsCacfShared.Tables("CODARFO").Columns("caf_opnome").DefaultValue = oApp.User.Nome

      dsCacfShared.Tables("CODARFO").AcceptChanges()

      bCacfHasChanges = False

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
  Public Overridable Function CacfSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not CacfTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "codarfo"
        strActLogDesLog = oApp.Tr(Me, 128574810668652856, "Corrispondenza codici articoli C/F")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScriviTabellaSemplice(strDittaCorrente, "CODARFO", dsCacfShared.Tables("CODARFO"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bCacfHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property CacfRecordIsChanged() As Boolean
    Get
      Return bCacfHasChanges
    End Get
  End Property
  Public Overridable Function CacfTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsCacfShared.Tables("CODARFO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!caf_conto) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572312974757449, "Codice conto obbligatorio.")))
          Return False
        End If
        If Trim(NTSCStr(dtrCurrRow(i)!caf_codarfo)) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128576662809210797, "Codice articolo obbligatorio.")))
          Return False
        End If
      Next

      For i = 0 To dtrCurrRow.Length - 1
        'Aggiorna il timestamp sul cliente/fornitore corrente
        If NTSCInt(dtrCurrRow(i)!caf_conto) > 0 Then
          oCldArmd.UpdateTimeStampConto(strDittaCorrente, NTSCInt(dtrCurrRow(i)!caf_conto))
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsCacfShared.Tables("CODARFO").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
        " AND caf_conto = " & dtrCurrRow(0)!caf_conto.ToString & _
        " AND caf_codart = " & CStrSQL(dtrCurrRow(0)!caf_codart.ToString) & _
        IIf(bBDPKExt = True, " AND caf_codarfo = " & CStrSQL(dtrCurrRow(0)!caf_codarfo.ToString), "").ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572313020075579, "Esiste gia una riga con le stesse caratteristiche")))
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
      Return False
    End Try
  End Function
  Public Overridable Function CacfTestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Try
      'Aggiorna il timestamp sul cliente/fornitore corrente
      If NTSCInt(dtrTmp!caf_conto) > 0 Then
        oCldArmd.UpdateTimeStampConto(strDittaCorrente, NTSCInt(dtrTmp!caf_conto))
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
      Return False
    End Try
  End Function
  Public Overridable Sub CacfNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsCacfShared.Tables("CODARFO").Rows.Add(dsCacfShared.Tables("CODARFO").NewRow)
      bCacfHasChanges = True

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
  Public Overridable Function CacfRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsCacfShared.Tables("CODARFO").Select(strFilter)(nRow).RejectChanges()
      bCacfHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function CheckEAN13(ByRef strCode As String) As Boolean
    Dim strLastDigit As String
    Dim i As Integer
    Dim evt As NTSEventArgs = Nothing
    Try
      'Non fa nulla se non è attiva l'opzione
      If Not bBDPKExt Then
        Return False
      End If
      'Se il codice non è di 12 o 13 caratteri esce
      If Len(strCode) <> 13 And Len(strCode) <> 12 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572357460027692, "Il codice a barre indicato deve essere di 12 o 13 caratteri.")))
        Return False
      End If
      'Se il codice è di 12 ne genera il check digit
      If Len(strCode) = 12 Then
        strLastDigit = CheckDigit(strCode & "0")
        If strLastDigit <> "" Then
          strCode = strCode & strLastDigit
        End If
      End If
      'Se il codice è di 13 lo controlla
      If Len(strCode) = 13 Then
        For i = 1 To Len(strCode)
          If Not ((Asc(Mid(strCode, i, 1)) >= 48) And (Asc(Mid(strCode, i, 1)) <= 57)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572357435151665, "I codici EAN13 non possono contenere caratteri")))
            Return False
          End If
        Next
        strLastDigit = CheckDigit(strCode)
        If (strLastDigit = " ") Or (strLastDigit = "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572357707692791, "Codice a barre Ean13 non corretto.")))
          Return False
        End If
        If strLastDigit <> Right(strCode, 1) Then
          evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128868670806736587, "Codice a barre Ean13 non corretto. L'ultima cifra dovrebbe essere '|" & strLastDigit & "|', sostituire?"))
          ThrowRemoteEvent(evt)
          If evt.RetValue = "YES" Then
            strCode = Left(strCode, Len(strCode) - 1) & strLastDigit
          Else
            Return False
          End If
        End If
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
  Public Overridable Function CacfInsertBarcode(ByVal strDesnote As String) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try
      'Ora lo copia in BARCODE
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128572360591121581, "Il codice a barre |" & strDesnote & "| risulta essere formalmente corretto." & vbCrLf & vbCrLf & "Copiarlo nei codici a barre dell'articolo?"))
      ThrowRemoteEvent(evt)
      If evt.RetValue = "YES" Then
        If Not oCldArmd.CacfInsertBarcode(strDittaCorrente, strCacfCodart, strDesnote) Then
          Return False
        End If
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

  Public Overridable Sub CacfBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CacfBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub CacfAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bCacfHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CacfAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub CacfBeforeColUpdate_caf_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_conto = ""
        Return
      End If

      If Not oCldArmd.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128450577437473574, "Cliente/Fornitore inesistente.")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        If NTSCStr(dttTmp.Rows(0)!an_tipo) = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128572326891025599, "Il conto non può far parte dei sottoconti.")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
        Else
          e.Row!xx_conto = strTmp
        End If
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
  Public Overridable Sub CacfBeforeColUpdate_caf_codarfo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.ProposedValue = UCase(e.ProposedValue.ToString)
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGVGIF.VB"

#End Region

#Region "funzioni specifiche per BNMGARTI.BNTCSCOR.VB"
  Public Overridable Function ScorApri(ByVal strDitta As String, ByRef dsScor As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArmd.GetDataScor(strDitta, strScorCodart, strScorChiamante, nScorCodmaga, _
                                    nScorFase, dsScor)
      If dReturn = False Then Return False

      bScorHasChanges = False

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
  Public Overridable Function ScorSalva(ByRef dsScor As DataSet) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArmd.ScorSalva(strDittaCorrente, strScorCodart, strScorChiamante, nScorCodmaga, _
                                    nScorFase, dsScor)

      If bResult Then
        bBarcHasChanges = False
      End If

      Return bResult
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGGIFT.VB"
  Public Overridable Function GiftApri(ByVal strDitta As String, ByVal strGiftCodart As String, ByRef dttGift As DataTable) As Boolean
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      oCldArmd.ValCodiceDb(strGiftCodart, strDitta, "ARTICOGIFT", "S", , dttGift)

      dttGift.TableName = "ARTICOGIFT"
      oCldArmd.SetTableDefaultValueFromDB("ARTICOGIFT", dttGift.DataSet)

      dttGiftShared = dttGift

      If dttGift.Rows.Count = 0 Then
        '--------------------------------------
        'imposto i valori di default per i nuovi record
        dttGift.Columns("codditt").DefaultValue = strDittaCorrente
        dttGift.Columns("ag_codart").DefaultValue = strGiftCodart
        dttGift.Columns("ag_dtscad").DefaultValue = IntSetDate("31/12/2099")
        dttGift.Columns("ag_valore").DefaultValue = 0
        dttGift.Columns("ag_rimbors").DefaultValue = "N"
        dttGift.Columns("ag_asaldo").DefaultValue = "N"
        dttGift.Columns("ag_durata").DefaultValue = 0
        dttGift.AcceptChanges()
        dttGift.Rows.Add()
      End If

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttGift.ColumnChanging, AddressOf GiftBeforeColUpdate
      AddHandler dttGift.ColumnChanged, AddressOf GiftAfterColUpdate

      bGiftHasChanges = False

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
  Public Overridable Function GiftSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then If Not GiftTestPreSalva() Then Return False
      '----------------------------------------
      'chiamo il dal per salvare
      If Not oCldArmd.GiftSalva(strDittaCorrente, dttGiftShared) Then Return False

      bGiftHasChanges = False

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
  Public ReadOnly Property GiftRecordIsChanged() As Boolean
    Get
      Return bGiftHasChanges
    End Get
  End Property
  Public Overridable Function GiftTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim i As Integer = 0
    Try
      If oCldArmd.CheckGiftMovimentata(strDittaCorrente, NTSCStr(dttGiftShared.Rows(0)!ag_codart)) Then
        Dim evnt As New NTSEventArgs(ThMsg.MSG_NOYES, oApp.Tr(Me, 129996283299449695, "Attenzione! Si sta per modificare le condizioni di una Gift Card che risulta essere già stata venduta." & vbCrLf & _
                                                                                     "Procedere ugualmente con la modifica?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue <> ThMsg.RETVALUE_YES Then Return False
      End If
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dttGiftShared.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCDec(dtrCurrRow(i)!ag_valore) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129991986172556087, "Indicare il valore della gift card")))
          Return False
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
  Public Overridable Function GiftRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttGiftShared.Select(strFilter)(nRow).RejectChanges()
      bGiftHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub GiftBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GiftBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub GiftAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bGiftHasChanges = True

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GiftAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "Funzioni specifiche per FRM_SIMU"
  Public Overridable Function PrimoMagazzino() As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArmd.PrimoMagazzino(strDittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ProgressiviMagazzino(ByVal strCodart As String, ByVal nMagaz As Integer, _
    ByVal nFase As Integer, ByRef dttTmp As DataTable) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArmd.ProgressiviMagazzino(strDittaCorrente, strCodart, nMagaz, nFase, dttTmp)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function SimulaVendita(ByVal lConto As Integer, ByVal strCodart As String, _
    ByVal nListino As Integer, ByVal dQuant As Decimal, _
    ByRef dPrezzo As Decimal, _
    ByRef dSconto1 As Decimal, ByRef dSconto2 As Decimal, ByRef dSconto3 As Decimal, _
    ByRef dSconto4 As Decimal, ByRef dSconto5 As Decimal, ByRef dSconto6 As Decimal, _
    ByRef strTipovalListOut As String, ByRef strTipovalScontOut As String) As Boolean
    Dim bGestionePrezzi As Boolean = CBool(oCldArmd.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))
    Dim bModLEX As Boolean = False
    Dim lModuliExtDittaDitt As Integer = ModuliExtDittaDitt(strDittaCorrente)
    Dim nFase As Integer = 0
    Dim nPromo As Integer = 0
    Dim nPrperqta As Integer = 0
    Dim nArClascon As Integer = 0
    Dim nAnClascon As Integer
    Dim dPrelist As Decimal = 0
    Dim dDaQuantOut As Decimal = 0
    Dim dAQuantOut As Decimal = 0
    Dim dPerqtaOut As Decimal = 0
    Dim strUmprz As String = "S"
    Dim strPrzNet As String = ""
    Dim strUnmisOut As String = ""
    Dim strTipoConto As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCldArmd.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = False Then Return False
      With dttTmp.Rows(0)
        strTipoConto = NTSCStr(!an_tipo)
        nAnClascon = NTSCInt(!an_clascon)
      End With
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If oCldArmd.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp) = False Then Return False
      nArClascon = NTSCInt(dttTmp.Rows(0)!ar_clascon)
      '--------------------------------------------------------------------------------------------------------------
      If CType(oCleComm, CLELBMENU).GestioneUMPrezzo(bGestionePrezzi, "BSVEBOLL", _
        IIf(strTipoConto = "C", "B", "M").ToString, False, strCodart, NTSCStr(dttTmp.Rows(0)!ar_unmis), _
        strDittaCorrente) = True Then strUmprz = "S" Else strUmprz = "N"
      '--------------------------------------------------------------------------------------------------------------
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtLEX)) Or _
         CBool((lModuliExtDittaDitt And CLN__STD.bsModExtTCP)) Then bModLEX = True
      '--------------------------------------------------------------------------------------------------------------
      If (dttTmp.Rows(0)!ar_gesfasi.ToString = "N") Or (bModLEX = False) Then
        nFase = 0
      Else
        nFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
      End If
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        If strUmprz <> "S" Then
          If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, 0, lConto, nListino, _
            NTSCStr(IIf(bGestionePrezzi, NTSCStr(!ar_unmis), "")), nFase, "P", True, 0, 0, _
            NTSCDate(Now.ToShortDateString), 0, dQuant, dPrezzo, dPrelist, nPromo, strPrzNet, nPrperqta, _
            dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalListOut) Then dPrezzo = 0
        Else
          If dPrezzo = 0 Then
            If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, 0, lConto, nListino, _
              NTSCStr(!ar_unmis), nFase, "P", True, 0, 0, NTSCDate(Now.ToShortDateString), 0, dQuant, dPrezzo, _
              dPrelist, nPromo, strPrzNet, nPrperqta, dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, _
              strTipovalListOut) Then
              dPrezzo = 0
            End If
            If dPrelist <> 0 Then
              CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, NTSCStr(!ar_unmis), _
                dPrelist, 0)
            End If
            If dPrezzo <> 0 Then
              CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, NTSCStr(!ar_unmis), _
                dPrezzo, 0)
            End If
          End If
        End If
      End With
      '--------------------------------------------------------------------------------------------------------------
      CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, strCodart, lConto, nArClascon, nAnClascon, "P", _
        True, 0, NTSCDate(Now.ToShortDateString), dQuant, dSconto1, dSconto2, dSconto3, dSconto4, dSconto5, dSconto6, _
        0, strPrzNet, Nothing, Nothing, 1, 0, 9999999999, "", strTipovalScontOut, nListino)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGHLAP.BNMGDEPR.VB"
  Public Overridable Function DecodificaFaseArticolo() As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArmd.DecodificaFaseArticolo(strDittaCorrente, strDeprCodart, dDeprFase)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetDataDepr(ByVal bCommeca As Boolean, ByVal bLotto As Boolean, _
    ByVal bUbicaz As Boolean, ByRef dsDepr As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCldArmd.GetDataDepr(strDittaCorrente, strDeprCodart, dDeprMagaz, dDeprFase, _
        bCommeca, bLotto, bUbicaz, dsDepr) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      dsDeprShared = dsDepr
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGHLAP.BNMGLOTC.VB"
  Public Overridable Function GetDataLotc(ByVal bCommeca As Boolean, ByVal bLotto As Boolean, _
    ByVal bUbicaz As Boolean, ByRef dsLotc As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCldArmd.GetDataLotc(strDittaCorrente, strDeprCodart, dDeprMagaz, dDeprFase, bCommeca, bLotto, bUbicaz, _
        dsLotc) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      dsLotcShared = dsLotc
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function RitornaTagliaArticolo(ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArmd.RitornaTagliaArticolo(strDittaCorrente, strLotcCodart, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

End Class
