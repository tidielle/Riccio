Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGUMIS
  Inherits CLE__BASE

  Public Overloads Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""

    Try
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

  Public Overloads Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_tb_codumis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      e.ProposedValue = e.ProposedValue.ToString.Trim
      '--------------------------------------------------------------------------------------------------------------
      If dsShared.Tables("TABUMIS").Select("tb_codumis = '" & e.ProposedValue.ToString & "'", Nothing, DataViewRowState.CurrentRows).Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127930179092201112, "Attenzione!" & vbCrLf & _
          "L'unità di misura inserita è già stato utilizzata." & vbCrLf & _
          "Inserire un'unità di misura non utilizzata.")))
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

  Public Overloads Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsShared.Tables("TABUMIS").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i As Integer = 0 To dtrTmp.Length - 1
        If dtrTmp(i)!tb_codumis.ToString.Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127930179812201112, "Attenzione!" & vbCrLf & _
            "Inserire un'unità di misura valida.")))
          Return False
        End If
        If dtrTmp(i)!tb_desumis.ToString = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128389217144923210, "Attenzione!" & vbCrLf & _
            "La descrizione dell'unità di misura non può essere vuota.")))
          Return False
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      strActLog = ocldBase.GetSettingBus("BSMGUMIS", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGUMIS"
      strActLogNomOggLog = "tabumis"
      strActLogDesLog = oApp.Tr(Me, 128058229647869287, "Unità di misura")
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

End Class