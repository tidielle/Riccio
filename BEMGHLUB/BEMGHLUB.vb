Imports System.Data
Imports NTSInformatica.CLN__STD


'PER UN ESEMPIO DI CLASSE EREDITATA CON DAL SPECIFICO VEDI BE__SOTC, BECGPRIV (con cambio ditta), BEVECOVP

Public Class CLEMGHLUB
  Inherits CLE__BASN


  Public oCldHlub As CLDMGHLUB

  Public dsShared As DataSet

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGHLUB"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldHlub = CType(MyBase.ocldBase, CLDMGHLUB)
    oCldHlub.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByRef dsHlub As DataSet, ByVal strDitta As String, ByVal strCodart As String, _
                                   ByVal nFase As Integer, ByVal nMagaz As Integer, ByVal strDatreg As String) As Boolean
    Try
      Return Apri(dsHlub, strDitta, strCodart, nFase, nMagaz, strDatreg, False)
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

  Public Overridable Function Apri(ByRef dsHlub As DataSet, ByVal strDitta As String, ByVal strCodart As String, _
                                   ByVal nFase As Integer, ByVal nMagaz As Integer, ByVal strDatreg As String, ByVal bAnaubic As Boolean) As Boolean
    Dim dReturn As Boolean = False
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlub, strDitta, strCodart, nFase, nMagaz, strDatreg, bAnaubic})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlub = CType(oIn(0), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      oCldHlub.Init(oApp)

      dReturn = oCldHlub.GetData(strDitta, strCodart, nFase, nMagaz, strDatreg, dsHlub, bAnaubic)
      dsShared = dsHlub


      dsShared.Tables("UBICAZ").Columns.Add("backcolor_tt_ubicaz", GetType(Integer))
      For Each dtrRow As DataRow In dsShared.Tables("UBICAZ").Rows
        If NTSCStr(dtrRow!au_bloccata) = "S" Then dtrRow!backcolor_tt_ubicaz = System.Drawing.Color.Silver.ToArgb
      Next

      Return dReturn
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
