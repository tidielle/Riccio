Imports System.Windows.Forms
Imports NTSInformatica
Imports NTSInformatica.CLN__STD
Public Class CLFDBDIBA

    Inherits CLEDBDIBA
    'Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, strTabella As String, bRemoting As Boolean, strRemoteServer As String, strRemotePort As String) As Boolean
    '    Return MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    'End Function
    Dim frmParent As FRMDBDIBA
    'Public Overrides Function Apri(strDitta As String, stredFilCodArtPad As String, ByRef strTesto As String, ByRef strKey As String, stredDescr As String, ByRef ds As DataSet) As Boolean
    '    'Return MyBase.Apri(strDitta, stredFilCodArtPad, strTesto, strKey, stredDescr, ds)
    '    'oCldDiba.SetTableDefaultValueFromDB("TABHHLCAM", ds)
    '    'SetDefaultValue(ds)

    '    'dsShared = ds

    'End Function

    Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, strTabella As String, bRemoting As Boolean, strRemoteServer As String, strRemotePort As String) As Boolean
        Return MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
        Dim ed_hhvaluso As New NTSTextBoxNum
        'ed_hhvaluso = CType(CType(frmParent, FRODBDIBA).NTSFindControlByName(CType(frmParent, FRODBDIBA), "ed_hhvaluso"), NTSTextBoxNum)
        'ed_hhvaluso.Text = CType(10, String)
    End Function
    Public row As Integer
    Public Overridable Function MovdisAfterColUpdate_md_hhcocad(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        '  MsgBox(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row).ToString)
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant = Math.Ceiling(Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsCicliShared.Tables(0).Rows(0)("dd_perqta")))
    End Function
    Public Overridable Function MovdisAfterColUpdate_md_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvaluso = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhcostol)
    End Function
    Public Overridable Function MovdisAfterColUpdate_md_hhmtbonus(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvalbonus = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhcostol)
    End Function
    Public Overridable Function MovdisAfterColUpdate_md_hhcostol(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvaluso = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant)
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvalbonus = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhmtbonus)
    End Function
    Public Overridable Function MovdisAfterColUpdate_md_hhvaluso(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhcoston = (Convert.ToDouble(e.ProposedValue) - Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvalbonus)) / Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant)
        'dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvaluso = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant)
    End Function
    Public Overridable Function MovdisAfterColUpdate_md_hhvalbonus(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhcoston = (Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvaluso) - Convert.ToDouble(e.ProposedValue)) / Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant)
        'dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_hhvaluso = Convert.ToDouble(e.ProposedValue) * Convert.ToDouble(dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_quant)
    End Function

    Public Overridable Sub MovdisBeforeColUpdate_md_hhlc(sender As Object, e As DataColumnChangeEventArgs)
        Try
            Dim nveicolo As Integer = 0
            nveicolo = NTSCInt(e.ProposedValue)
            If oCldDiba.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "TABHHLC", "N") = False Then
                ThrowRemoteEvent(New NTSEventArgs("", "Codice inesistente"))
                e.ProposedValue = e.Row(e.Column.ColumnName)
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

    Public Overridable Sub MovdisAfterColUpdate_md_hhlc(sender As Object, e As DataColumnChangeEventArgs)
        Try
            Dim nveicolo As Integer = 0
            nveicolo = NTSCInt(e.ProposedValue)
            Dim dttTmpVei As New DataTable
            If oCldDiba.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "TABHHLC", "N", "", dttTmpVei) Then
                e.Row.Item("md_hhcconf") = dttTmpVei.Rows(0).Item("tb_prezzo")
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


    Public Overridable Sub MovdisBeforeColUpdate_md_hhconto(sender As Object, e As DataColumnChangeEventArgs)
        Try
            Dim nveicolo As Integer = 0
            nveicolo = NTSCInt(e.ProposedValue)
            If oCldDiba.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "ANAGRA", "N") = False Then
                ThrowRemoteEvent(New NTSEventArgs("", "Codice inesistente"))
                e.ProposedValue = e.Row(e.Column.ColumnName)
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
    Public Overridable Sub MovdisAfterColUpdate_md_hhconto(sender As Object, e As DataColumnChangeEventArgs)
        Try
            Dim nveicolo As Integer = 0
            nveicolo = NTSCInt(e.ProposedValue)
            Dim dttTmpVei As New DataTable
            If oCldDiba.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmpVei) And nveicolo <> 0 Then
                e.Row.Item("md_hhforn") = dttTmpVei.Rows(0).Item("an_descr1")
            End If
            If dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_codfigli.ToString = " " Then
                dsMovdisShared.Tables(0).Rows(dsMovdisShared.Tables(0).Rows.IndexOf(e.Row))!md_codfigli = "T"
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
    Public Overridable Function CalcolaTotali(strRiga As String) As ArrayList
        Dim totalSum As New ArrayList
        Dim Sum As Double = 0
        Dim Sum1 As Double = 0
        Dim Sum2 As Double = 0
        Dim Sum3 As Double = 0
        Dim tsum As Double = 0
        Dim tsum1 As Double = 0
        Dim tsum2 As Double = 0
        Dim tsum3 As Double = 0
        Try

            For i As Integer = 0 To dsMovdisShared.Tables(0).Rows.Count - 1
                If IsDBNull(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvaluso")) Then
                    Sum = 0
                Else
                    Sum = Convert.ToDouble(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvaluso"))
                End If
                tsum += Sum

                If IsDBNull(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvalbonus")) Then
                    Sum1 = 0
                Else
                    Sum1 = Convert.ToDouble(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvalbonus"))
                End If
                tsum1 += Sum1


                If IsDBNull(dsMovdisShared.Tables(0).Rows(i).Item("md_hhcdors")) Then
                    Sum2 = 0
                Else
                    Sum2 = Convert.ToDouble(dsMovdisShared.Tables(0).Rows(i).Item("md_hhcdors"))
                End If
                tsum2 += Sum2

                If IsDBNull(dsMovdisShared.Tables(0).Rows(i).Item("md_hhcconf")) Then
                    Sum3 = 0
                Else
                    Sum3 = Convert.ToDouble(dsMovdisShared.Tables(0).Rows(i).Item("md_hhcconf"))
                End If
                tsum3 += Sum3

            Next
            totalSum.Add(tsum)
            totalSum.Add(tsum1)
            totalSum.Add(tsum2 * Convert.ToDouble(dsCicliShared.Tables(0).Rows(0)("dd_perqta")))
            totalSum.Add(tsum3 * Convert.ToDouble(dsCicliShared.Tables(0).Rows(0)("dd_perqta")))

            ' totalSum.Add(CalcolaSconti(strRiga))

            'Return totalSum
        Catch ex As Exception
            For i As Integer = 1 To 4
                totalSum.Add(0)
            Next
        End Try
        Return totalSum

    End Function
    Public Overridable Function CalcolaSconti(strRiga As String) As Double
        Dim sc As Double = 1
        Dim result As String() = strRiga.Split(New String() {"+"}, StringSplitOptions.None)
        Try

            For Each s As String In result
                If s = "" Then s = "0"
                sc = sc * (1 - (CType(s, Double) / 100))
            Next
            sc = 1 - sc
            Return sc
        Catch ex As Exception
            Return 1
        End Try
    End Function

    'Public Overrides Sub MovdisAfterColUpdate(sender As Object, e As DataColumnChangeEventArgs)
    '    MyBase.MovdisAfterColUpdate(sender, e)
    '    'Dim sum As String = ""

    '    'For Each drData As DataRow In dsMovdisShared.Tables(0).Rows
    '    '    sum = drData("md_hhvalusu").ToString()
    '    '    'code to do something with the 'sum' value

    '    'Next
    '    'Dim totalSum As Double
    '    'Dim Sum As Double = 0
    '    '''Dim oControl As New NTSTextBoxNum
    '    '''oControl = CType(CType(frmparent, FRODBDIBA).NTSFindControlByName(frmparent, "ed_hhvaluso"), NTSTextBoxNum) '.NTSFindControlByName(frmparent, "edAn_prefiban")
    '    ''''oControl = Me.
    '    ''Dim oform As Form = CType(FRODBDIBA, FRODBDIBA)
    '    ''MsgBox(oform.Name)
    '    ''Dim oControl As Control
    '    ''oControl = CType(FRODBDIBA.NTSFindControlByName(FRODBDIBA, "ed_hhvaluso"), Control)

    '    'For i As Integer = 0 To dsMovdisShared.Tables(0).Rows.Count - 1
    '    '    If IsDBNull(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvaluso")) Then
    '    '        Sum = 0
    '    '    Else
    '    '        Sum = Convert.ToDouble(dsMovdisShared.Tables(0).Rows(i).Item("md_hhvaluso"))
    '    '    End If
    '    '    totalSum += Sum
    '    'Next
    '    ''Dim ed_hhvaluso As NTSTextBoxNum = CType(Me.FRODBDIBA.NTSFindControlByName(Me.FRODBDIBA, "ed_hhvaluso"), NTSTextBoxNum)
    '    ''CType(oControl, NTSTextBoxNum).Text
    '    ''CType(frmparent, FRODBDIBA).
    '    ''CType(frmParent, FRODBDIBA).ed_hhvaluso.Text =

    '    'dsShared.Tables("DISTBAS").Rows(0)!db_hhvaluso = NTSCDec(totalSum.ToString)
    '    ''oControl.Text = totalSum.ToString
    '    ''MsgBox(totalSum.ToString)
    'End Sub
    'Public Overridable Sub DuplicaItem_Click(sender As Object, e As EventArgs)
    'Dim newRow As DataRow = dsMovdisShared.Tables(0).NewRow
    '    newRow.Item(row) = dsMovdisShared.Tables(0).Rows(row - 1)

    '    dsMovdisShared.Tables(0).Rows.Add(newRow)
    'End Sub
End Class
