Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGHLAR
  Inherits CLE__BASN

  Public oCldHlar As CLDMGHLAR

  Public dsShared As DataSet

  Public strImageDir As String

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGHLAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldHlar = CType(MyBase.ocldBase, CLDMGHLAR)
    oCldHlar.Init(oApp)

    Return True
  End Function

  Public Overridable Function GetTabtipa(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Try
      strDittaCorrente = strDitta
      Return oCldHlar.GetTabtipa(strDittaCorrente, dttOut)
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

  Public Overridable Function Ricerca(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Ricerca(dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, strCodarfo, lContoCodarfo, _
                     bTestmag, nMagaz, strSuccedanei, strCodartacc, bPrezzi, nListino, strDtlistino, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function Ricerca(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String, ByVal bAbituali As Boolean) As Boolean
    Dim dReturn As Boolean = False

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, _
                                             strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                                             strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlar = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      Return Ricerca(dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                     strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali, False)
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
  Public Overridable Function Ricerca(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String, ByVal bAbituali As Boolean, ByVal bCodiciRoot As Boolean) As Boolean
    Dim dReturn As Boolean = False

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, _
                                             strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                                             strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali, bCodiciRoot})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlar = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      oCldHlar.Init(oApp)

      dReturn = oCldHlar.GetData(dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, _
                                 strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                                 strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali, bCodiciRoot)
      dsShared = dsHlar

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

  Public Overridable Function GetWhereHlmo(ByVal nMagaz As Integer) As String
    Try

      Return oCldHlar.GetWhereHlmo(strDittaCorrente, nMagaz)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function SetImagesDir() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Prelevo la dir delle imaggini sul server
      '--------------------------------------------------------------------------------------------------------------
      Dim i As Integer = oApp.RptDir.Length - 1
      Do While oApp.RptDir.Substring(i, 1) <> "\"
        i -= 1
      Loop
      '--------------------------------------------------------------------------------------------------------------
      strImageDir = oApp.RptDir.Substring(0, i) & "\Images\"
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function GetQueryListe(ByVal strTabella As String, ByVal nCod As Integer) As String
    GetQueryListe = ""
    Try
      Return oCldHlar.GetQueryListe(strDittaCorrente, strTabella, nCod)
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

  Public Overridable Function GetArtclasDescr(ByVal strClas1 As String, _
                                              ByVal strClas2 As String, ByVal strClas3 As String, _
                                              ByVal strClas4 As String, ByVal strClas5 As String) As String
    GetArtclasDescr = ""
    Try
      Return oCldHlar.GetArtclasDescr(strDittaCorrente, strClas1, strClas2, strClas3, strClas4, strClas5)
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

  Public Overridable Function ListaClassificazioni(ByRef dsClassificazioni As DataSet) As Boolean
    Try
      Return oCldHlar.ListaClassificazioni(strDittaCorrente, dsClassificazioni)
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

  Public Overridable Function GetTabAext(ByVal strDitta As String, ByRef dsTabaext As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldHlar.GetTabAext(strDitta, dsTabaext)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
End Class
