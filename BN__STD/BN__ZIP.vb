'il codice commentato era perfettamente funzionante, ma richiedeva come libreria (quindi add tra i reference)
'la vjslib (C:\Windows\Microsoft.NET\Framework\v2.0.50727\vjslib.dll), 
'che viene installata con il J# framework (in installato per default da Business e non presente sui pc)

Imports NTSInformatica.CLN__STD
'Imports java.util.zip
'Imports java.io
Imports ICSharpCode.SharpZipLib.Zip

Public Class CLE__ZIP
  Public Function UnZip(ByVal stFileIn As String, ByVal strDirOut As String) As Boolean
    'Dim Buffer(1023) As SByte
    'Dim nCount As Integer = 0
    Try
      If stFileIn = "" Then Return True
      Dim oZip As New FastZip()
      oZip.ExtractZip(stFileIn, strDirOut, "")

      'If strDirOut.Substring(strDirOut.Length - 1) <> "\" Then
      '  strDirOut = strDirOut & "\"
      'End If
      'Dim fsIn As New ZipInputStream(New java.io.FileInputStream(stFileIn))
      'Dim entry As ZipEntry = Nothing
      'System.IO.Directory.CreateDirectory(strDirOut)
      'entry = fsIn.getNextEntry()
      'Do
      '  Dim fsOu As java.io.FileOutputStream = Nothing
      '  If entry.isDirectory() = False Then
      '    If entry.getName().Replace("/", "\").IndexOf("\") > -1 Then
      '      Dim strF As String = entry.getName().Replace("/", "\")
      '      While strF.Substring(strF.Length - 1) <> "\"
      '        strF = strF.Substring(0, strF.Length - 1)
      '      End While
      '      System.IO.Directory.CreateDirectory(strDirOut & strF)
      '    End If
      '    fsOu = New FileOutputStream(System.IO.Path.Combine(strDirOut, entry.getName().Replace("/", "\")), False)
      '    'Dim c As Integer = fsIn.read()
      '    'While c <> -1
      '    '  fsOu.write(c)
      '    '  c = fsIn.read()
      '    'End While
      '    nCount = fsIn.read(Buffer, 0, Buffer.Length)
      '    While nCount > 0
      '      fsOu.write(Buffer, 0, nCount)
      '      nCount = fsIn.read(Buffer, 0, Buffer.Length)
      '    End While
      '    fsOu.close()
      '  Else
      '    System.IO.Directory.CreateDirectory(strDirOut & entry.getName().Replace("/", "\"))
      '  End If
      '  entry = fsIn.getNextEntry()
      'Loop While entry IsNot Nothing
      'fsIn.close()

      Return True
    Catch ex As Exception
      Throw ex
    End Try
  End Function
  Public Function Zip(ByVal strFileOut As String, ByVal strDirIn As String) As Boolean
    Try
      If strFileOut = "" Then Return True

      Dim oZip As New FastZip()
      oZip.CreateZip(strFileOut, strDirIn, True, "")

      'Dim fsOut As New ZipOutputStream(New java.io.FileOutputStream(strFileOut))
      'If Not Zip_AddDirs(fsOut, strDirIn, strDirIn) Then
      '  Return False
      'End If
      'If Not Zip_AddFiles(fsOut, strDirIn, strDirIn) Then
      '  Return False
      'End If
      'fsOut.close()
      Return True
    Catch ex As Exception
      Throw ex
    End Try
  End Function
  'Private Function Zip_AddDirs(ByRef fsOut As ZipOutputStream, ByVal strDirInStart As String, ByVal strDirIn As String) As Boolean
  '  Dim MyZipEntry As ZipEntry = Nothing
  '  Try
  '    Dim strD As String() = System.IO.Directory.GetDirectories(strDirIn)
  '    For i As Integer = 0 To strD.Length - 1
  '      MyZipEntry = New ZipEntry(strD(i).Substring(strDirInStart.Length + 1).Replace("\", "/") & "/") 'la '/' finale indica che è una subdir
  '      MyZipEntry.setMethod(ZipEntry.DEFLATED)
  '      fsOut.putNextEntry(MyZipEntry)
  '      fsOut.closeEntry()
  '      If Not Zip_AddFiles(fsOut, strDirInStart, strD(i)) Then
  '        Return False
  '      End If
  '      If System.IO.Directory.GetDirectories(strD(i)).Length > 0 Then
  '        If Not Zip_AddDirs(fsOut, strDirInStart, strD(i)) Then
  '          Return False
  '        End If
  '      End If
  '    Next
  '    Return True
  '  Catch ex As Exception
  '    Throw ex
  '  End Try
  'End Function
  'Private Function Zip_AddFiles(ByRef fsOut As ZipOutputStream, ByVal strDirInStart As String, ByVal strDirIn As String) As Boolean
  '  Dim MyZipEntry As ZipEntry = Nothing
  '  Dim fsIn As java.io.FileInputStream = Nothing
  '  Dim Buffer(1023) As SByte
  '  Dim nCount As Integer = 0
  '  Try
  '    Dim strF As String() = System.IO.Directory.GetFiles(strDirIn)
  '    For i As Integer = 0 To strF.Length - 1
  '      MyZipEntry = New ZipEntry(strF(i).Substring(strDirInStart.Length + 1).Replace("\", "/"))
  '      MyZipEntry.setMethod(ZipEntry.DEFLATED)
  '      fsOut.putNextEntry(MyZipEntry)
  '      fsIn = New java.io.FileInputStream(strF(i))
  '      'Dim c As Integer = fsIn.read()
  '      'While c <> -1
  '      '  fsOut.write(c)
  '      '  c = fsIn.read()
  '      'End While
  '      nCount = fsIn.read(Buffer, 0, Buffer.Length)
  '      While nCount > 0
  '        fsOut.write(Buffer, 0, nCount)
  '        nCount = fsIn.read(Buffer, 0, Buffer.Length)
  '      End While
  '      fsIn.close()
  '      fsOut.closeEntry()
  '    Next
  '    Return True
  '  Catch ex As Exception
  '    Throw ex
  '  End Try
  'End Function
End Class
