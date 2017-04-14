Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLDVEASPE
  Inherits CLD__BASE

  Public Overridable Function LeggiTabAspe(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT tabaspe.*" & _
               " FROM tabaspe WHERE codditt = " & CStrSQL(strDitta) & _
               " ORDER BY tabaspe.tb_codaspe  "
      'Chiedo i dati al database
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABASPE")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
