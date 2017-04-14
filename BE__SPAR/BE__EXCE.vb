Option Strict Off

Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__EXCE
  Inherits CLE__BASN

  Public oExcel As Excel.Application = Nothing
  Public oCartella As Excel.Workbook = Nothing
  Public oSheet As Excel.Worksheet = Nothing

  Public Overridable Function Init2(ByRef App As CLE__APP) As Boolean
    oApp = App
    Return True
  End Function

  Public Overridable Function ApriExcel(ByVal strFile As String, ByVal strFoglio As String) As Boolean
    Dim strNomeXls As String = ""
    Dim i As Integer = 0
    Try
      '-------------------------
      'tolgo la dir al nome del file
      strNomeXls = strFile
      i = strNomeXls.IndexOf("\")
      Do While i <> -1
        strNomeXls = strNomeXls.Substring(i + 1)
        i = strNomeXls.IndexOf("\")
      Loop

      '-------------------------
      'apro Excel
      Try
        oExcel = New Excel.Application
        oExcel.WindowState = Excel.XlWindowState.xlMinimized
        oExcel.Visible = True
      Catch ex As Exception
        Throw New NTSException(oApp.Tr(Me, 128287314843556000, "Microsoft Excel non trovato o non installato correttamente nel sistema."))
        Return False
      End Try

      Try
        oExcel.Workbooks.Open(strFile, , False)
      Catch ex As Exception
        Throw New NTSException(oApp.Tr(Me, 128846933400302000, "Impossibile aprire il file '|" & strFile & "|'."))
        Return False
      End Try

      oCartella = oExcel.Workbooks(strNomeXls)
      If oCartella.ReadOnly Then
        Throw New NTSException(oApp.Tr(Me, 128287308967556000, "Un altro utente o processo ha aperto in lettura/scrittura la cartella di Excel '|" & strNomeXls & "|' (oppure il file è protetto da scrittura). Impossibile continuare."))
        Return False
      End If

      Try
        oSheet = CType(oCartella.Worksheets(strFoglio), Excel.Worksheet)
      Catch ex As Exception
        Throw New NTSException(oApp.Tr(Me, 128287819799816000, "Impossibile accedere al foglio '" & strFoglio & "' nella cartella di lavoro di Excel |" & strNomeXls & "|."))
        Return False
      End Try

      Return True
    Catch ex As Exception
      If Not oExcel Is Nothing Then oExcel.Quit()
      '--------------------------------------------------------------
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ChiudiExcel() As Boolean
    Try
      oCartella.Save()
      oExcel.Quit()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function PreparaFile() As Boolean
    Dim nColtmp As Integer = 0
    Dim i As Integer = 0
    Try
      '--------------------
      'Attiva il foglio contoeconomico
      oSheet.Activate()

      '--------------------
      'svuoto il foglio da precedenti elaborazioni
      oSheet.Cells.Select()
      oExcel.Selection.ClearContents()

      Return True
    Catch ex As Exception
      If Not oExcel Is Nothing Then oExcel.Quit()
      '--------------------------------------------------------------
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function InserisciValore(ByVal nRiga As Integer, ByVal nColonna As Integer, ByVal strTipoDato As String, ByVal oValore As Object) As Boolean
    '-------------------------------
    'inserisce il valore nella cella
    Try
      Select Case strTipoDato
        Case "S" : oSheet.Cells(nRiga, nColonna).value = NTSCStr(oValore)
        Case "D" : oSheet.Cells(nRiga, nColonna).value = NTSCDate(oValore)
        Case Else : oSheet.Cells(nRiga, nColonna).value = NTSCDec(oValore)
      End Select

      Return True
    Catch ex As Exception
      If Not oExcel Is Nothing Then oExcel.Quit()
      '--------------------------------------------------------------
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      '--------------------------------------------------------------	
    End Try
  End Function
End Class