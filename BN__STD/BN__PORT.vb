Imports NTSInformatica.CLN__STD
Imports System.Runtime.InteropServices


'esempio di utilizzo (vedi anche BNMGETTE)
'Dim oPort As New CLE__PORT
'oPort.OpenPort("HP LaserJet 4")  '"\\server\hpl"
'oPort.StartDocPort("Prova", vbNullString, vbNullString)
'oPort.StartPagePort
'oPort.WritePort(Chr(15) & Chr(27) & Chr(67) & Chr(66) & "PDFCreator" & Chr(12) & Chr(18))
'oPort.EndDocPort
'oPort.ClosePort

Public Class CLE__PORT
  Dim m_lhPort As IntPtr
  Dim m_diDocInfo As DOCINFO
  Dim m_lpcWritten As Integer = 0

  <StructLayout(LayoutKind.Sequential)> _
  Public Structure DOCINFO
    <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
    <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
    <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
  End Structure

  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function OpenPrinter(ByVal pPrinterName As String, ByRef phPrinter As IntPtr, ByVal pDefault As Integer) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal Level As Integer, ByRef pDocInfo As DOCINFO) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal data As String, ByVal buf As Integer, ByRef pcWritten As Integer) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Integer
  End Function
  <DllImport("winspool.drv", CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Integer
  End Function

  Public Function OpenPort(ByVal strPortName As String) As Integer
    OpenPort = OpenPrinter(strPortName, m_lhPort, 0)
  End Function
  Public Function StartDocPort(ByVal strDocName As String, ByVal strOutputFile As String, ByVal strDatatype As String) As Integer
    Dim myDocInfo As DOCINFO
    myDocInfo.pDocName = strDocName
    myDocInfo.pOutputFile = vbNullString 'strOutputFile
    myDocInfo.pDataType = vbNullString 'strDatatype
    StartDocPort = StartDocPrinter(m_lhPort, 1, myDocInfo)
  End Function
  Public Sub StartPagePort()
    StartPagePrinter(m_lhPort)
  End Sub
  Public Function WritePort(ByVal strData As String) As Integer
    WritePort = WritePrinter(m_lhPort, (strData), Len(strData), m_lpcWritten)
  End Function
  Public Function EndPagePort() As Integer
    EndPagePort = EndPagePrinter(m_lhPort)
  End Function
  Public Function EndDocPort() As Integer
    EndDocPort = EndDocPrinter(m_lhPort)
  End Function
  Public Function ClosePort() As Integer
    ClosePort = ClosePrinter(m_lhPort)
  End Function
End Class
