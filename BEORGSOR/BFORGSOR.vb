Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Globalization
Imports System
Public Class CLFORGSOR
    Inherits CLEORGSOR
    Public Overrides Function AfterColUpdate_CORPO_ec_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
        Dim dttTmp As New DataTable
        Try
            ''--------------------------------------------------------------------------------------------------------------
            'If bNew = True Then e.Row!ec_datconsor = e.ProposedValue
            ''--------------------------------------------------------------------------------------------------------------
            If MyBase.AfterColUpdate_CORPO_ec_codart(sender, e) Then

                If NTSCStr(e.ProposedValue).Trim <> "" Then
                    oCldGsor.ValCodiceDb(dttEt_conto.Rows(0)!an_conto.ToString,
                            strDittaCorrente, "ANAGRA", "N", "", dttTmp)
                    If dttTmp.Rows.Count = 0 Then
                        'ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, "articolo inesistente"))
                        'e.ProposedValue = e.Row!ec_hhprova2
                    Else
                        If dttTmp.Rows(0)!an_omocodice.ToString = "S" Then
                            e.Row!ec_magaz = 2
                        End If


                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            If GestErrorCallThrow() Then
                Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
            Else
                ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
            End If
        End Try
    End Function
End Class
