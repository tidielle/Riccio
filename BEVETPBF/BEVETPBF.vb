Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVETPBF
  Inherits CLE__BASE

  Public oCldTpbf As CLDVETPBF

#Region "Moduli"
  Private Moduli_P As Integer = bsModVE + bsModMG + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtPAR + bsModExtCRM
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
  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, _
    ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, _
    ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVETPBF"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldTpbf = CType(MyBase.ocldBase, CLDVETPBF)
    oCldTpbf.Init(oApp)
    Return True
  End Function
#End Region
  
#Region "Eventi di Griglia"
  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      '--------------------------------------------------------------------------------------------------------------
      e.Row.EndEdit()
      e.Row.EndEdit()
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_tb_codtpbf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow

    Try
      dtrTmp = dsShared.Tables("TABTPBF").Select("tb_codtpbf = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128491173732499020, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato")))
        Return
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tcontro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tcontro = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019909863210130, "Contropartita")))
          Return
        Else
          e.Row!xx_tcontro = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tmagazz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tmagazz = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019909899775438, "Magazzino 1")))
          Return
        Else
          e.Row!xx_tmagazz = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tmagazz2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tmagazz2 = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019909984781966, "Magazzino 2")))
          Return
        Else
          e.Row!xx_tmagazz2 = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tcaumag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tcaumag = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUM", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019910026191396, "Causale Magazzino")))
          Return
        Else
          e.Row!xx_tcaumag = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tcautra(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tcautra = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUM", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019910059162678, "Causale Trasporto")))
          Return
        Else
          e.Row!xx_tcautra = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_vcodcen(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vcodcen = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019910101978466, "Centro C/A")))
          Return
        Else
          e.Row!xx_vcodcen = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tcauscap(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tcauscap = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUM", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019910138856298, "Causale scarico produzione")))
          Return
        Else
          e.Row!xx_tcauscap = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tmagimp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tmagimp = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019910174171510, "Magazzino imp.")))
          Return
        Else
          e.Row!xx_tmagimp = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_codcacadd(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcacadd = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCACA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129127790243621250, "Causale di CA per DDT e/o documenti di produzione per le spese di piede")))
          Return
        Else
          e.Row!xx_codcacadd = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_coddivi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddivi = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABDIVI", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270251096456875, "Divisione C/A")))
          Return
        Else
          e.Row!xx_coddivi = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_codcauc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcauc = ""
      Else
        If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUC", "N", strOut, dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129491392545683594, "Causale CG inesistente")))
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" Then
            'se è indicato il piano dei conti, deve essere uguale a quello della ditta del tipobf
            oCldTpbf.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp1)
            If NTSCStr(dttTmp.Rows(0)!tb_codpcon).ToLower <> NTSCStr(dttTmp1.Rows(0)!tb_azcodpcon).ToLower Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129491396901582032, "Causale CG con piano dei conti diverso dal piano dei conti della ditta in uso")))
              Return
            End If
          End If
          e.Row!xx_codcauc = strOut
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_tlistin(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tlistin = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldTpbf.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLIST", "N", strOut) = False Then
        e.Row!xx_tlistin = ""
      Else
        e.Row!xx_tlistin = strOut
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
#End Region

  Public Overrides Function Apri(ByVal strDittaCorrente As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False

    Try
      Me.strDittaCorrente = strDittaCorrente
      '--------------------------------------------------------------------------------------------------------------
      dReturn = oCldTpbf.GetData(strDittaCorrente, ds)
      If dReturn = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      oCldTpbf.SetTableDefaultValueFromDB("TABTPBF", ds)
      '--------------------------------------------------------------------------------------------------------------
      SetDefaultValue(ds)
      '--------------------------------------------------------------------------------------------------------------
      dsShared = ds
      '--------------------------------------------------------------------------------------------------------------
      AddHandler dsShared.Tables("TABTPBF").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABTPBF").ColumnChanged, AddressOf AfterColUpdate
      '--------------------------------------------------------------------------------------------------------------
      bHasChanges = False
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

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '--------------------------------------------------------------------------------------------------------------
      strActLog = ocldBase.GetSettingBus("BSVETPBF", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSVETPBF"
      strActLogNomOggLog = "TABTPBF"
      strActLogDesLog = oApp.Tr(Me, 128491173732343160, "Tipi bolle e fatture")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("TABTPBF").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABTPBF").Columns("tb_new506").DefaultValue = "S"
      ds.Tables("TABTPBF").Columns("tb_tlistin").DefaultValue = "1"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim strOut As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsShared.Tables("TABTPBF").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tb_codtpbf) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128491173732654880, "Inserire un codice tipo bolla fattura compreso tra 0 e 9999")))
          Return False
        End If
        If dtrTmp(i).RowState = DataRowState.Added Then
          If Not oCldTpbf.ValCodiceDb(NTSCStr(dtrTmp(i)!tb_codtpbf), strDittaCorrente, "TABTPBF", "N", strOut) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128448814801861814, "Codice tipo bolla fattura già esistente.")))
            Return False
          End If
        End If
        If NTSCStr(dtrTmp(i)!tb_destpbf).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128448783307640887, "Il campo descrizione è obbligatorio")))
          Return False
        End If
        If NTSCInt(dtrTmp(i)!tb_tlistin) < -2 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128448783201517615, "Il valore del Listino Testata può assumere un valore compreso -2 e 9999")))
          Return False
        End If
        If (NTSCStr(dtrTmp(i)!tb_flresocl) <> "N") And (NTSCStr(dtrTmp(i)!tb_flacconto) = "S") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128491768593585892, "Se nei documenti d'acconto sono indicate Fatt./Ricevute fiscali d'acconto" & vbCrLf & _
            "non possono seguire altri documenti.")))
          Return False
        End If
      Next
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

  Public Overridable Function CheckCod(ByVal strCod As String) As Boolean
    Try
      If strCod = "0" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814774277525, "Codice iva obbligatorio.")))
        Return False
      End If

      If oCldTpbf.ValCodiceDb(strCod, strDittaCorrente, "TABTPBF", "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814796309339, "Codice tipo bolla/fattura '|" & strCod & "|' già esistente.")))
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
  Public Overridable Function Duplica(ByVal strCodNew As String, ByVal strCodOld As String) As Boolean
    Dim i As Integer
    Dim strColname As String = ""
    Dim dtrTmp() As DataRow = Nothing
    Try
      dsShared.Tables("TABTPBF").AcceptChanges()

      If dsShared.Tables("TABTPBF").Rows.Count > 0 Then
        dtrTmp = dsShared.Tables("TABTPBF").Select("tb_codtpbf = " & strCodOld)
        If dtrTmp.Length = 0 Then
          Return False
        End If
      Else
        Return False
      End If

      dsShared.Tables("TABTPBF").Rows.Add()
      dsShared.Tables("TABTPBF").Rows(dsShared.Tables("TABTPBF").Rows.Count - 1)("tb_codtpbf") = strCodNew

      For i = 0 To dsShared.Tables("TABTPBF").Columns.Count - 1
        strColname = dsShared.Tables("TABTPBF").Columns(i).ColumnName
        If strColname = "tb_codtpbf" Or strColname = "ts" Then Continue For

        dsShared.Tables("TABTPBF").Rows(dsShared.Tables("TABTPBF").Rows.Count - 1)(strColname) = dtrTmp(0)(strColname)
      Next

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

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
