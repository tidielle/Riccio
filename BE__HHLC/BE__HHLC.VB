Imports System.Data
Imports NTSInformatica
Imports NTSInformatica.CLN__STD

Public Class CLE__HHLC
    Inherits CLE__BASE

#Region "Moduli"
    Private Moduli_P As Integer = CLN__STD.bsModAll
    Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Variabili"
    Public oCldHHLC As CLD__HHLC  'oggetto DAL
#End Region

    Public Overrides Function Init(ByRef App As CLE__APP,
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String,
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String,
                              ByVal strRemotePort As String) As Boolean
        If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__HHLC"
        MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
        oCldHHLC = CType(MyBase.ocldBase, CLD__HHLC)
        oCldHHLC.Init(oApp)

        Return True
    End Function

    Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
        Dim dReturn As Boolean = False
        Try
            '--------------------------------------
            'Tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
            strDittaCorrente = strDitta
            dReturn = oCldHHLC.LeggiTabHHLC(strDittaCorrente, ds)
            If dReturn = False Then Return False

            '--------------------------------------------------------------
            'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
            oCldHHLC.SetTableDefaultValueFromDB(strNomeTabella, ds)

            SetDefaultValue(ds)

            dsShared = ds

            '--------------------------------------
            'Creo gli eventi per la gestione del Datatable dentro l'Entity
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
            'Imposto i valori di default diversi da quelli impostati nel database
            ds.Tables(strNomeTabella).Columns("codditt").DefaultValue = strDittaCorrente
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
                strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
                Return
            End If
            '-------------------------------------------------------------
            'controllo che in una cella short non venga inserito un numero troppo grande
            If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
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

    Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
        Dim bResult As Boolean = False
        Try
            '----------------------------------------
            'controlli pre-salvataggio (solo se non è una delete)
            If Not bDelete Then
                If Not TestPreSalva() Then Return False
            End If

            '----------------------------------------
            'chiamo il dal per salvare
            bResult = oCldHHLC.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")

            If bResult Then
                bHasChanges = False
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
    Public Overrides Function TestPreSalva() As Boolean
        'Eseguo i controlli necessari prima di fare il salvataggio della riga
        Dim dtrTmp As DataRow() = dsShared.Tables(strNomeTabella).Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
        Dim i As Integer = 0

        Try
            'For i = 0 To dtrTmp.Length - 1
            '    If NTSCInt(dtrTmp(i)!tb_codciva) = 0 Then
            '        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182577065937500, "Il Codice IVA deve essere un valore compreso tra 1 e 9999.")))
            '        Return False
            '    End If

            '    If NTSCStr(dtrTmp(i)!tb_desciva).Trim = "" Then
            '        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182577117500000, "Descrizione IVA obbligatoria.")))
            '        Return False
            '    End If
            'Next

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

End Class
