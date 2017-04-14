Imports NTSInformatica.CLN__STD
Imports System.Net
Imports System.IO

Public Class CLE__FTPA
  Private ftpRequest As FtpWebRequest

  Private strServerGLOB As String = ""
  Private strFolderGLOB As String = ""
  Private strUserGLOB As String = ""
  Private strPwdGLOB As String = ""

#Region "Metodi Pubblici"
  Public Overridable Function SetFtpAccess(ByVal strServer As String, ByVal strUser As String, ByVal strPwd As String) As Boolean
    Try
      ftpRequest = DirectCast(System.Net.WebRequest.Create(strServer), System.Net.FtpWebRequest)
      ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails
      ftpRequest.Credentials = New System.Net.NetworkCredential(strUser, strPwd)

      If GetResponse() Then
        strServerGLOB = strServer
        strUserGLOB = strUser
        strPwdGLOB = strPwd
      End If
    Catch ex As Exception
      Throw ex
    End Try
  End Function

#Region "Accesso al File System"
  Public Overridable Function ListFolderContent() As DataTable
    Dim strResponse As String = ""
    Try
      'Invia la richiesta
      SendRequest(WebRequestMethods.Ftp.ListDirectoryDetails)

      'Attende la risposta
      If Not GetResponse(strResponse) Then Return Nothing

      'Inizia l'elaborazione della risposta
      Dim strTmp() As String = strResponse.Trim.Split(Chr(10))

      Dim dttTmp As New DataTable
      dttTmp.Columns.Add("mode", GetType(String))
      dttTmp.Columns.Add("link", GetType(String))
      dttTmp.Columns.Add("owner", GetType(String))
      dttTmp.Columns.Add("group", GetType(String))
      dttTmp.Columns.Add("size", GetType(String))
      dttTmp.Columns.Add("datetime", GetType(String))
      dttTmp.Columns.Add("name", GetType(String))

      'mode	        links	 owner 	group 	         size	datetime	   name
      'drwxr-xr-x    9 19027007   users            4096 Nov 16 23:46 maelstormapp.com_Backup_Giornaliero
      For z As Integer = 0 To strTmp.Length - 1
        dttTmp.Rows.Add()
        With dttTmp.Rows(dttTmp.Rows.Count - 1)
          !mode = strTmp(z).Substring(0, 10).Trim
          !link = strTmp(z).Substring(10, 5).Trim
          !owner = strTmp(z).Substring(15, 9).Trim
          !group = strTmp(z).Substring(24, 8).Trim
          !size = strTmp(z).Substring(32, 16).Trim
          !datetime = strTmp(z).Substring(48, 13).Trim
          !name = strTmp(z).Substring(61).Trim
        End With
      Next

      'Ritorna un datatable con tutti i dati della LIST
      Return dttTmp
    Catch ex As Exception
      Throw ex
      Return Nothing
    End Try
  End Function

  Public Overridable Function CheckFolderFileExists(ByVal strName As String) As Boolean
    Dim dttFolders As DataTable = Nothing
    Try
      dttFolders = ListFolderContent()

      For z As Integer = 0 To dttFolders.Rows.Count - 1
        If CStr(dttFolders.Rows(z)!name) = strName.Trim("/"c) Then Return True
      Next

      Return False
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Public Overridable Function CreateFolder(ByVal strFolder As String) As Boolean
    Try
      'Se la cartella c'è già ritorna true come se l'avesse creata
      If CheckFolderFileExists(strFolder) Then Return True

      SendRequest(WebRequestMethods.Ftp.MakeDirectory, strFolder)

      Return GetResponse()
    Catch ex As Exception
      Throw ex
    End Try
  End Function
  Public Overridable Function CopyFileToFtp(ByVal strFilePath As String) As Boolean
    Dim oStream As Stream
    Dim byteFile() As Byte
    Try
      Dim lIndex As Integer = strFilePath.LastIndexOf("/"c)
      Dim strFileName As String = strFilePath.Substring(lIndex).Trim("/"c)

      SendRequest(WebRequestMethods.Ftp.UploadFile, strFileName)

      ' read in file...
      byteFile = File.ReadAllBytes(strFilePath)

      ' upload file...
      oStream = ftpRequest.GetRequestStream()
      oStream.Write(byteFile, 0, byteFile.Length)
      oStream.Close()
      oStream.Dispose()

      Return GetResponse()
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Public Overridable Function DownloadFile(ByVal strDestPath As String, ByVal strFileToDownload As String) As Boolean
    Dim strFileContent As String = ""
    Dim swWriter As StreamWriter
    Try
      'Controlla che il file esista
      If Not CheckFolderFileExists(strFileToDownload) Then
        MsgBoxFtp("File not found")
        Return False
      End If

      SendRequest(WebRequestMethods.Ftp.DownloadFile)

      'Ritorna il contenuto del file
      If Not GetResponse(strFileContent) Then Return False

      'Salva il file
      swWriter = New StreamWriter(strDestPath & "\" & strFileToDownload)
      swWriter.Write(strFileContent)
      swWriter.Flush()
      swWriter.Close()
    Catch ex As Exception
      Throw ex
    End Try
  End Function
#End Region

#Region "Cambio Cartella Corrente"
  Public Overridable Function GoToSubFolder(ByVal strFolder As String) As Boolean
    Try
      'Se uno passa la stringa vuota o una cartella con solo . ignora (2 punti significa tornare indietro)
      If strFolder.Trim.Trim("."c) = "" Then Return False

      If CheckFolderFileExists(strFolder) Then
        strFolderGLOB &= strFolder & "/"
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Throw ex
    End Try
  End Function
  Public Overridable Sub GoBackSubFolder()
    Try
      'non torna indietro nel caso di cartelle '..'
      Dim strTmp() As String = strFolderGLOB.Split("/"c)

      strFolderGLOB = ""
      For i As Integer = 0 To strTmp.Length - 2
        strFolderGLOB &= strTmp(i) & "/"
      Next
    Catch ex As Exception
      Throw ex
    End Try
  End Sub
  Public Overridable Sub GoToHome()
    Try
      strFolderGLOB = ""
    Catch ex As Exception
      Throw ex
    End Try
  End Sub
#End Region
#End Region

#Region "Metodi Privati"
  Private Function SendRequest(ByVal strRequestType As String, Optional ByVal strParameter As String = "") As Boolean
    Try
      ftpRequest = DirectCast(System.Net.WebRequest.Create(strServerGLOB & "/" & strFolderGLOB & strParameter), System.Net.FtpWebRequest)
      ftpRequest.Method = strRequestType
      ftpRequest.Credentials = New System.Net.NetworkCredential(strUserGLOB, strPwdGLOB)

      Return True
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function GetResponse(Optional ByRef strOut As String = "") As Boolean
    Dim ftpResponse As FtpWebResponse
    Try
      ftpResponse = CType(ftpRequest.GetResponse, FtpWebResponse)

      'Le risposte che iniziano con 1xx, 2xx, 3xx sono sempre positive
      'quelle che iniziano con 4xx, 5xx, sempre negative
      '6xx, 7xx, 8xx, 9xx non esistono
      Select Case ftpResponse.StatusDescription.Substring(0, 1)
        Case "1", "2", "3"
          Dim srReader As StreamReader = New StreamReader(ftpResponse.GetResponseStream())
          strOut = srReader.ReadToEnd

          ftpResponse.Close()

          Return True
        Case Else
          MsgBoxFtp(ftpResponse.StatusDescription)
          ftpResponse.Close()
          Return False
      End Select
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Sub MsgBoxFtp(ByVal strMsg As String)
    Try
      MsgBox(strMsg, MsgBoxStyle.Critical, "FTP Server")
    Catch ex As Exception
      Throw ex
    End Try
  End Sub
#End Region
End Class
