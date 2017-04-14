Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Threading

Namespace NTSInformatica
    Public Class CLFDBDIBA
        Inherits CLEDBDIBA
        ' Methods


        'Public Overridable Sub CicliAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
        '    Dim args As NTSEventArgs
        '    Try
        '        If (MyBase.strPrevCelValue.IndexOf((e.Column.ColumnName.ToUpper & ";")) > -1) Then
        '            MyBase.strPrevCelValue = MyBase.strPrevCelValue.Remove(MyBase.strPrevCelValue.IndexOf((e.Column.ColumnName.ToUpper & ";")), (e.Column.ColumnName.ToUpper.Length + 1))
        '        Else
        '            Me.bCicliHasChanges = True
        '            args = New NTSEventArgs("GRIAGG", (e.Column.Table.TableName & "§" & e.Column.ColumnName))
        '            Me.ThrowRemoteEvent(args)
        '            e.Row.EndEdit()
        '            e.Row.EndEdit()
        '            Dim name As String = ("CicliAfterColUpdate_" & e.Column.ColumnName.ToLower)
        '            Dim method As MethodInfo = Me.GetType.GetMethod(name)
        '            If (Not method Is Nothing) Then
        '                method.Invoke(Me, New Object() {RuntimeHelpers.GetObjectValue(sender), e})
        '            End If
        '        End If
        '    Catch exception1 As Exception
        '        Dim obj2 As Object
        '        ProjectData.SetProjectError(exception1)
        '        Dim errore As Exception = exception1
        '        If CLN__STD.GestErrorCallThrow Then
        '            obj2 = Me
        '            Throw New NTSException(CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        End If
        '        obj2 = Me
        '        args = New NTSEventArgs("", CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        Me.ThrowRemoteEvent(args)
        '        ProjectData.ClearProjectError()
        '    End Try
        'End Sub

        'Public Overridable Function CicliApri(ByVal strSelNodekey As String, ByVal stredFilCodArtPad As String, ByVal stredDb_coddb As String, ByVal stredFilDataValid As String, ByVal stredFilLotto As String, ByVal stredFilCodClienti As String, ByVal stredFilComm As String, ByRef ds As DataSet) As Boolean
        '    Dim flag As Boolean
        '    Dim set As DataSet = Nothing
        '    Try
        '        Dim str As String
        '        Dim str3 As String = stredFilCodArtPad
        '        Dim str2 As String = Me.EstraiDaNome(Me.EstraiDaKey(strSelNodekey, 0, True), 3)
        '        If (Strings.Len(Me.EstraiDaKey(strSelNodekey, 1, True)) = 0) Then
        '            If (Strings.Len(stredFilCodArtPad) > 0) Then
        '                str = Strings.UCase(Strings.Left((stredFilCodArtPad & "".PadLeft(CLN__STD.CodartMaxLen, " "c)), CLN__STD.CodartMaxLen))
        '            Else
        '                str = Strings.UCase(Strings.Left((stredDb_coddb & "".PadLeft(CLN__STD.CodartMaxLen, " "c)), CLN__STD.CodartMaxLen))
        '            End If
        '        Else
        '            str = Strings.UCase(Strings.Left((Me.EstraiDaNome(Me.EstraiDaKey(strSelNodekey, 0, True), 3) & "".PadLeft(CLN__STD.CodartMaxLen, " "c)), CLN__STD.CodartMaxLen))
        '        End If
        '        If (Strings.Left(str, 1) = "!") Then
        '            str = (Strings.Mid(str, 1) & " ")
        '        End If
        '        Me.strGesvar = Me.TrovaGesvar(str2)
        '        Me.oCldDiba.CicliApri(MyBase.strDittaCorrente, str2, stredFilCodArtPad, stredFilDataValid, Me.strGesvar, Me.bUsaFiltroPadre, Me.strTipoOpz, str, stredFilLotto, stredFilCodClienti, Me.bUsaFiltroCommessa, stredFilComm, Me.TrovaCoddb(str2), ds)
        '        Me.oCldDiba.SetTableDefaultValueFromDB("CICLI", ds)
        '        Me.CicliSetDefaultValue(ds)
        '        Me.CicliRiempiColonneUnbound(ds)
        '        Me.dsCicliShared = ds
        '        AddHandler Me.dsCicliShared.Tables.Item("CICLI").ColumnChanging, New DataColumnChangeEventHandler(AddressOf Me.CicliBeforeColUpdate)
        '        AddHandler Me.dsCicliShared.Tables.Item("CICLI").ColumnChanged, New DataColumnChangeEventHandler(AddressOf Me.CicliAfterColUpdate)
        '        Me.dsCicliShared.Tables.Item("CICLI").AcceptChanges()
        '        Me.bCicliHasChanges = False
        '        If (Strings.Len(Me.strDibaCoddb) > 0) Then
        '            Me.oCldDiba.GetMaxCicli(MyBase.strDittaCorrente, Me.strDibaCoddb, [set])
        '            If ([set].Tables.Item("CICLI").Rows.Count > 0) Then
        '                Me.dCicliMaxRiga = CLN__STD.NTSCDec(RuntimeHelpers.GetObjectValue([set].Tables.Item("CICLI").Rows.Item(0).Item("xx_maxriga")))
        '                If (Decimal.Compare(New Decimal(CLN__STD.NTSCInt(Me.dCicliMaxRiga)), Me.dCicliMaxRiga) < 0) Then
        '                    Me.dCicliMaxRiga = New Decimal((CLN__STD.NTSCInt(Me.dCicliMaxRiga) + 1))
        '                End If
        '            Else
        '                Me.dCicliMaxRiga = New Decimal
        '            End If
        '        Else
        '            Me.dCicliMaxRiga = New Decimal
        '        End If
        '        flag = True
        '    Catch exception1 As Exception
        '        Dim obj2 As Object
        '        ProjectData.SetProjectError(exception1)
        '        Dim errore As Exception = exception1
        '        If CLN__STD.GestErrorCallThrow Then
        '            obj2 = Me
        '            Throw New NTSException(CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        End If
        '        obj2 = Me
        '        Dim e As New NTSEventArgs("", CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        Me.ThrowRemoteEvent(e)
        '        ProjectData.ClearProjectError()
        '    End Try
        '    Return flag
        'End Function



        'Public Overridable Sub CicliOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
        '    Dim ds As DataSet = Nothing
        '    Dim args As NTSEventArgs
        '    Dim dttTable As New DataTable
        '    Try
        '        args = New NTSEventArgs("NUOVARIGA:", "")
        '        Me.ThrowRemoteEvent(args)
        '        If (Strings.Len(Me.strDibaCoddb) > 0) Then
        '            Me.oCldDiba.GetMaxCicli(MyBase.strDittaCorrente, Me.strDibaCoddb, ds)
        '            If (ds.Tables.Item("CICLI").Rows.Count > 0) Then
        '                Me.dCicliMaxRiga = CLN__STD.NTSCDec(RuntimeHelpers.GetObjectValue(ds.Tables.Item("CICLI").Rows.Item(0).Item("xx_maxriga")))
        '                If (Decimal.Compare(New Decimal(CLN__STD.NTSCInt(Me.dCicliMaxRiga)), Me.dCicliMaxRiga) < 0) Then
        '                    Me.dCicliMaxRiga = New Decimal((CLN__STD.NTSCInt(Me.dCicliMaxRiga) + Me.nIncrementaContatoreRiga))
        '                End If
        '            Else
        '                Me.dCicliMaxRiga = New Decimal
        '            End If
        '        Else
        '            Me.dCicliMaxRiga = New Decimal
        '        End If
        '        Dim strDescampo As String = ""
        '        If (Me.oCldDiba.ValCodiceDb(Me.strDibaCoddb, MyBase.strDittaCorrente, "ARTICO", "S", strDescampo, dttTable, "", "", "") AndAlso (CLN__STD.NTSCStr(RuntimeHelpers.GetObjectValue(dttTable.Rows.Item(0).Item("ar_gesfasi"))) = "S")) Then
        '        End If
        '        e.Row.Item("dd_coddb") = Me.strDibaCoddb
        '        If ((e.Column.ColumnName <> "dd_riga") AndAlso (Decimal.Compare(CLN__STD.NTSCDec(RuntimeHelpers.GetObjectValue(e.Row.Item("dd_riga"))), Decimal.Zero) = 0)) Then
        '            e.Row.Item("dd_riga") = Decimal.Add(Me.dCicliMaxRiga, New Decimal(Me.nIncrementaContatoreRiga))
        '        End If
        '        e.Row.Item("dd_dtinval") = Me.dInizioValLavorazioni
        '        If (CLN__STD.NTSCInt(Me.stredFilCodClientiForm) = 0) Then
        '            e.Row.Item("dd_coclie") = 0
        '        Else
        '            e.Row.Item("dd_coclie") = CLN__STD.NTSCInt(Me.stredFilCodClientiForm)
        '        End If
        '        If (CLN__STD.NTSCInt(Me.stredFilCommForm) = 0) Then
        '            e.Row.Item("dd_commeca") = 0
        '        Else
        '            e.Row.Item("dd_commeca") = CLN__STD.NTSCInt(Me.stredFilCommForm)
        '        End If
        '    Catch exception1 As Exception
        '        Dim obj2 As Object
        '        ProjectData.SetProjectError(exception1)
        '        Dim errore As Exception = exception1
        '        If CLN__STD.GestErrorCallThrow Then
        '            obj2 = Me
        '            Throw New NTSException(CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        End If
        '        obj2 = Me
        '        args = New NTSEventArgs("", CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        Me.ThrowRemoteEvent(args)
        '        ProjectData.ClearProjectError()
        '    End Try
        'End Sub


        'Public Overridable Sub CicliSetDefaultValue(ByRef ds As DataSet)
        '    Dim dttTable As New DataTable
        '    Try
        '        Dim flag As Boolean
        '        ds.Tables.Item("CICLI").Columns.Item("codditt").DefaultValue = MyBase.strDittaCorrente
        '        Dim strDescampo As String = ""
        '        If (Me.oCldDiba.ValCodiceDb(Me.strDibaCoddb, MyBase.strDittaCorrente, "ARTICO", "S", strDescampo, dttTable, "", "", "") AndAlso (CLN__STD.NTSCStr(RuntimeHelpers.GetObjectValue(dttTable.Rows.Item(0).Item("ar_gesfasi"))) = "S")) Then
        '            flag = True
        '        End If
        '        If Not flag Then
        '            ds.Tables.Item("CICLI").Columns.Item("dd_terzista").DefaultValue = "N"
        '        Else
        '            ds.Tables.Item("CICLI").Columns.Item("dd_terzista").DefaultValue = "I"
        '        End If
        '        ds.Tables.Item("CICLI").Columns.Item("dd_filtart").DefaultValue = "".PadLeft(CLN__STD.CodartMaxLen, "_"c)
        '        ds.Tables.Item("CICLI").Columns.Item("dd_filtart2").DefaultValue = "".PadLeft(CLN__STD.CodartMaxLen, "_"c)
        '        ds.Tables.Item("CICLI").Columns.Item("xx_ttrans").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tattrez").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tesec").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tattmo").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tesecmo").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tattrezmin").DefaultValue = 0
        '        ds.Tables.Item("CICLI").Columns.Item("xx_tesecmin").DefaultValue = 0
        '    Catch exception1 As Exception
        '        Dim obj2 As Object
        '        ProjectData.SetProjectError(exception1)
        '        Dim errore As Exception = exception1
        '        If CLN__STD.GestErrorCallThrow Then
        '            obj2 = Me
        '            Throw New NTSException(CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        End If
        '        obj2 = Me
        '        Dim e As New NTSEventArgs("", CLN__STD.GestError(errore, obj2, "", MyBase.oApp.InfoError, "", False))
        '        Me.ThrowRemoteEvent(e)
        '        ProjectData.ClearProjectError()
        '    End Try
        'End Sub




        Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
            If (MyBase.strNomeDal = "BD__BASE") Then
                MyBase.strNomeDal = "BDDBDIBA"
            End If
            MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
            Me.oCldDiba = DirectCast(MyBase.ocldBase, CLDDBDIBA)
            Me.oCldDiba.Init(MyBase.oApp)
            Return True
        End Function


    End Class
End Namespace

