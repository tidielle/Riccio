Imports NTSInformatica
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
'Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Data
Imports System.IO
'Imports Microsoft.Win32
Imports System.Globalization
Imports System.Threading
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Text
Imports System.Security.Cryptography
Imports System.Management
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection

'************** RICORDARSI DI COMPILARE SEMPRE CON L'OPZIONE CPU x86, ALTRIMENTI NON GIRA LA CREAZIONE DEGLI ACTIVEX DI BUSINESS SU 64 BIT!!! ***********  compila->opzioni di compilazione avanzate->cpu di destinazione=x86


Public Class CLN__STD
  'variabili settate da be__menu e mai cambiate durante l'esecuzione dell'istanza di busnet
  Public Shared ArrDblTipo As String = ""                     'SETTATO ALL'AVVIO DI BUSNET DA BE__MENU
  Public Shared Dir As String = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)      'SETTATO ALL'AVVIO DA BUSNET.EXE
  Public Shared nDpi As Integer = 0   'impostato da bn__menu: la dimensione standard dei font di windows è di 96 dpi
  Public Shared dMoltiplicatoreInterfaccia As Decimal = 1  'impostato da bn__menu: il moltiplicatore della dimensione dell'interfaccia
  Public Shared CodartMaxLen As Integer = 18  'impostato da be__menu all'avvio di Business ed al cambio del DB
  Public Shared SerieMaxLen As Integer = 1    'impostato da be__menu all'avvio di Business ed al cambio del DB
  Public Shared UsePrinterDialogNet As Boolean = False  'impostato da be__menu all'avvio

  'occhio. se dichiaro delle variabili shared, il valore impostato in una funzione può venir letto in un'altra: NOI NON DOBBIAMO MAI FARLO

  Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As Integer
  Private Declare Function CloseServiceHandle Lib "advapi32.dll" (ByVal hSCObject As Integer) As Integer
  Private Const GENERIC_EXECUTE As Integer = &H20000000
  Private Const GENERIC_READ As Integer = &H80000000
  Private Const GENERIC_WRITE As Integer = &H40000000

  Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
  Private Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
  Private Const SWP_HIDEWINDOW As Integer = &H80
  Private Const SWP_SHOWWINDOW As Integer = &H40

  <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto, EntryPoint:="GetSystemMetrics")> _
  Private Shared Function GetSystemMetrics(ByVal nIndex As Integer) As Integer
  End Function
  Private Shared ReadOnly SM_CONVERTIBLESLATEMODE As Integer = &H2003
  Private Shared ReadOnly SM_TABLETPC As Integer = &H56

#Region "Nome PC in terminal server"
  Enum WTS_INFO_CLASS
    WTSInitialProgram = 0
    WTSApplicationName = 1
    WTSWorkingDirectory = 2
    WTSOEMIdWTSOEMIdNot = 3
    WTSSessionId = 4
    WTSUserName = 5
    WTSWinStationName = 6
    WTSDomainName = 7
    WTSConnectState = 8
    WTSClientBuildNumber = 9
    WTSClientName = 10
    WTSClientDirectory = 11
    WTSClientProductId = 12
    WTSClientHardwareId = 13
    WTSClientAddress = 14
    WTSClientDisplay = 15
    WTSClientProtocolType = 16
    WTSIdleTime = 17
    WTSLogonTime = 18
    WTSIncomingBytes = 19
    WTSOutgoingBytes = 20
    WTSIncomingFrames = 21
    WTSOutgoingFrames = 22
  End Enum
  Private Const WTS_CURRENT_SERVER_HANDLE As Integer = -1

  Private Declare Sub WTSFreeMemory Lib "wtsapi32.dll" (ByVal memory As IntPtr)
  Private Declare Auto Function WTSGetActiveConsoleSessionId Lib "Kernek32.dll" () As Integer
  Private Declare Auto Function WTSQuerySessionInformation Lib "wtsapi32.dll" ( _
          ByVal hServer As Int32, _
          ByVal SessionId As Int32, _
          ByVal InfoClass As WTS_INFO_CLASS, _
          ByRef ppBuffer As IntPtr, _
          ByRef pCount As Int32) As Int32
#End Region

  'Gestione tempi prestazioni
  Public Shared arDate, arDescr As New ArrayList

  Public Shared PrcConn As String = ""                    'stringa di connessione al db ARCPROC utilizzata da GetSettingReg
  Public Shared GetSettingRegPerUser As Integer = 9       'se 0 la GetSettingReg letta da reg. di windows, 1 = per MACCHINA da arcproc, 2 per MACCHINA/UTENTE da arcproc (default 1)

  Private Shared bInError As Boolean = False              'se si verifica un errore di collegamento con SQL server non devo far eseguire la lettura delle opzioni di registro, altrimenti va in loop infinito!!!
  Public Shared WebDir As String = ""           'directory settata da busweb e passata all'STD tramite be__menu per getsettingreg e savesettingreg
  Private Shared arCheckInvokeCustomFunction As New ArrayList
  Public Shared IOS As Boolean = False
  Public Shared ANDROID As Boolean = False
  Public Shared Application_StartupPath As String = ""
  Public Shared FRIENDLY As Boolean = False       'se true è una installazione friendly, non busnet
  Private Shared BUSIS As Boolean = False         'se true è una installazione avviata da BIS (Business Information service) con app client che gira su android/ios/windows (SBC.EXE)

  Public Shared ReadOnly Property strSep() As String
    Get
      Return System.IO.Path.DirectorySeparatorChar
    End Get
  End Property

  Public Shared Function IntSetDate(ByVal strIn As String) As String
    'la data in input deve sempre essere nel formato italiano dd/mm/yyyy
    Try
      Dim strFormat As String = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern
      If strFormat.IndexOf("yyyy") = -1 Then strFormat = strFormat.Replace("yy", "yyyy")
      Return DateTime.Parse(strIn, New CultureInfo("it-IT")).ToString(strFormat)
    Catch
      Throw New Exception("BN__STD - IntSetDate: La data passata (" & strIn & ") non può essere convertita in formato data nazionalizzato in base alle impostazioni del pannello di controllo di windows")
    End Try
  End Function
  Public Shared Function IntSetNum(ByVal strIn As String) As String
    'il numero in input deve sempre essere nel formato italiano nnnn,nnn
    Try
      If Thread.CurrentThread.CurrentCulture.ToString = "it-IT" Then
        Return strIn
      Else
        Return strIn.Replace(".", "").Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString)
      End If
    Catch
      Throw New Exception("BN__STD - IntSetNum: Il numero passato (" & strIn & ") non può essere convertito in formato numero nazionalizzato in base alle impostazioni del pannello di controllo di windows")
    End Try
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function NTSCStr(ByVal objIn As Object) As String
    If objIn Is Nothing Then Return ""
    Return objIn.ToString()
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function NTSCDec(ByVal objIn As Object) As Decimal
    If objIn Is Nothing Then Return 0
    If objIn.ToString = "" Then Return 0
    If Not IsNumeric(objIn) Then Return 0
    Return CDec(objIn)
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function NTSCInt(ByVal objIn As Object) As Integer
    If objIn Is Nothing Then Return 0
    If objIn.ToString = "" Then Return 0
    If Not IsNumeric(objIn) Then Return 0
    Return CInt(objIn)
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function NTSCDate(ByVal objIn As Object) As Date
    If objIn Is Nothing Then Return DateTime.Parse("01/01/1900", New CultureInfo("it-IT"))
    If objIn.ToString = "" Then Return DateTime.Parse("01/01/1900", New CultureInfo("it-IT"))
    If Not IsDate(objIn) Then Return DateTime.Parse("01/01/1900", New CultureInfo("it-IT"))
    Return CDate(objIn)
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function NTSCLng(ByVal objIn As Object) As Long
    If objIn Is Nothing Then Return 0
    If objIn.ToString = "" Then Return 0
    If Not IsNumeric(objIn) Then Return 0
    Return CLng(objIn)
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function NTSCShort(ByVal objIn As Object) As Short
    If objIn Is Nothing Then Return 0
    If objIn.ToString = "" Then Return 0
    If Not IsNumeric(objIn) Then Return 0
    Return CShort(objIn)
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDblSQL(ByVal dIn As Decimal) As String
    'share: cone statit in c#: non c'è bisogno di istanziare la classe prima di usare la funzione, ma basta scrivere CLN__STD.ConvDblSQL
    'per scrittura di un numero con virgola in SQL: converte la virgola in punto
    'Return vIn.Replace(",", ".")
    Return dIn.ToString().Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator.ToString, "").Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString, ".")

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDblSQL(ByVal strIn As String) As String
    'share: cone statit in c#: non c'è bisogno di istanziare la classe prima di usare la funzione, ma basta scrivere CLN__STD.ConvDblSQL
    'per scrittura di un numero con virgola in SQL: converte la virgola in punto
    'Return vIn.Replace(",", ".")
    Return strIn.Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator.ToString, "").Replace(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString, ".")

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CStrSQL(ByVal strIn As String) As String
    'per scrittura di una stringa in SQL: converte l'apice semplice in doppio apice, poi inserisce gli apici iniziali e finali
    'If strIn = "" Then Return "Null"         'NON SERVE (anzi farebbe dare diversi errori): se la stringa è = '' ci pensa SQL Server a mettere null o il valore predefinito!!!
    Return "'" & strIn.Replace("'", "''") & "'"

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CStrSQL(ByVal objIn As Object) As String
    If objIn Is Nothing Then Return "Null"
    If objIn.Equals(DBNull.Value) Then Return "Null"
    Return CStrSQL(objIn.ToString)

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDataSQL(ByVal strIn As String) As String
    'per scrittura di una data in SQL: il formato finale è '1900-1-1' anno-mese-giorno se non è stata passata l'oar, oppure se l'ora è 0.00.00
    Dim dtTmp As Date
    If strIn = "" Then Return "null"
    Try
      dtTmp = DateTime.Parse(strIn)
      'If vIn.IndexOf(" ") = -1 Or vIn.IndexOf(" 0.00.00") > -1 Or vIn.IndexOf(" 0:00:00") > -1 Then
      Return "'" & dtTmp.Year() & "-" & dtTmp.Month & "-" & dtTmp.Day & "'"
      'Else
      ' Return "'" & dtTmp.Year() & "-" & dtTmp.Month & "-" & dtTmp.Day & " " & _
      '      dtTmp.Hour.ToString("00") & ":" & dtTmp.Minute.ToString("00") & ":" & dtTmp.Second.ToString("00") & "'"
      'End If
    Catch
      Throw New Exception("BN__STD - CdataSQL: La data passata (" & strIn & ") non può essere convertita in formato SQL perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDataSQL(ByVal dtIn As DateTime) As String
    'per scrittura di una data in SQL: il formato finale è '1900-1-1' anno-mese-giorno se non è stata passata l'oar, oppure se l'ora è 0.00.00
    If dtIn = Nothing Then Return "null"
    Try
      'If vIn.IndexOf(" ") = -1 Or vIn.IndexOf(" 0.00.00") > -1 Or vIn.IndexOf(" 0:00:00") > -1 Then
      Return "'" & dtIn.Year() & "-" & dtIn.Month & "-" & dtIn.Day & "'"
      'Else
      ' Return "'" & dtTmp.Year() & "-" & dtTmp.Month & "-" & dtTmp.Day & " " & _
      '      dtTmp.Hour.ToString("00") & ":" & dtTmp.Minute.ToString("00") & ":" & dtTmp.Second.ToString("00") & "'"
      'End If
    Catch
      Throw New Exception("BN__STD - CdataSQL: La data passata (" & dtIn.ToShortDateString() & ") non può essere convertita in formato SQL perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDataOraSQL(ByVal strIn As String) As String
    'per scrittura di una data in SQL: il formato finale è '1900-1-1' anno-mese-giorno 
    Dim dtTmp As DateTime
    If strIn = "" Then Return "null"
    Try
      dtTmp = DateTime.Parse(strIn)
      Return "'" & dtTmp.Year() & "-" & dtTmp.Month & "-" & dtTmp.Day & " " & _
                  dtTmp.Hour.ToString("00") & ":" & dtTmp.Minute.ToString("00") & ":" & dtTmp.Second.ToString("00") & "'"
    Catch
      Throw New Exception("BN__STD - CdataSQL: La data passata (" & strIn & ") non può essere convertita in formato SQL perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDataOraSQL(ByVal dtIn As DateTime) As String
    'per scrittura di una data in SQL: il formato finale è '1900-1-1' anno-mese-giorno 
    If dtIn = Nothing Then Return "null"
    Try
      Return "'" & dtIn.Year() & "-" & dtIn.Month & "-" & dtIn.Day & " " & _
                  dtIn.Hour.ToString("00") & ":" & dtIn.Minute.ToString("00") & ":" & dtIn.Second.ToString("00") & "'"
    Catch
      Throw New Exception("BN__STD - CdataSQL: La data passata (" & dtIn.ToString() & ") non può essere convertita in formato SQL perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function CDataSQLAccess(ByVal strIn As String) As String
    'per scrittura di una data in SQL: il formato finale è datevalue("31/01/2010")
    Dim dtTmp As Date
    If strIn = "" Then Return "null"
    Try
      dtTmp = DateTime.Parse(strIn)
      Return "DATEVALUE(""" & NTSCInt(dtTmp.Day).ToString("00") & "/" & NTSCInt(dtTmp.Month).ToString("00") & "/" & dtTmp.Year() & """)"
    Catch
      Throw New Exception("BN__STD - CDataSQLAccess: La data passata (" & strIn & ") non può essere convertita in formato SQL per MSAccess perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function CDataSQLAccess(ByVal dtIn As DateTime) As String
    'per scrittura di una data in SQL: il formato finale è datevalue("31/01/2010")
    If dtIn = Nothing Then Return "null"
    Try
      Return "DATEVALUE(""" & NTSCInt(dtIn.Day).ToString("00") & "/" & NTSCInt(dtIn.Month).ToString("00") & "/" & dtIn.Year() & """)"
    Catch
      Throw New Exception("BN__STD - CDataSQLAccess: La data passata (" & dtIn.ToShortDateString() & ") non può essere convertita in formato SQL per MSAccess perchè non corretta")
    End Try

    '----------------
    'per non far dare il messaggio in testoprecompila
    'CLN__STD.CheckInvokeCustomFunction(CustomClass
    '----------------
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ConvDataRpt(ByVal strDt As String) As String
    Try
      ' converte data per filtri record di Crystal report
      Dim mm As String, yyyy As String, dd As String
      dd = NTSCDate(strDt).Day.ToString.PadLeft(2, CChar("0"))
      mm = NTSCDate(strDt).Month.ToString.PadLeft(2, CChar("0"))
      yyyy = NTSCDate(strDt).Year.ToString
      Return "Date(" & yyyy & "," & mm & "," & dd & ")"
    Catch ex As Exception
      Throw New Exception("BN__STD - ConvDataRpt: La data passata (" & strDt & ") non può essere convertita in formato per Crystal repot perchè non corretta")
    End Try
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function ConvStrRpt(ByVal strIn As String) As String
    '1- Converte gli eventuali apici doppi interni in apici singoli (per il web 2 apici semplci)
    '2- Aggiunge i doppi apici all'inizio e alla fine della stringa (per il web apice semplice)

    Dim strOut As String = ""
    Dim lPos As Integer = 0
    Dim lLen As Integer = 0
    Try
      If IsWebApp() Or IsBis() Then
        strOut = strIn.Replace("""", "'").Replace("§", "_") 'in busweb/business_is § è il separatore tra nome di formula e testo della formula
        While strOut.IndexOf("''") > 0
          strOut = strOut.Replace("''", "'")
        End While
        strOut = strOut.Replace("'", "''")
        If strOut <> "" Then
          If strOut(0) <> "'"c Then
            strOut = "'" & strOut & "'"
          End If
        End If
        Return strOut
      End If

      strOut = ""
      lPos = 1
      lLen = InStr(strIn, """")
      Do While Not lLen = 0
        strOut = strOut & Mid$(strIn, lPos, lLen - lPos) & "'"
        lPos = lLen + 1
        lLen = InStr(lPos, strIn, """")
      Loop
      Return """" & strOut & Mid$(strIn, lPos) & """"

    Catch ex As Exception
      Throw New Exception("BN__STD - ConvStrRpt: La stringa passata (" & strIn & ") non può essere convertita in formato per Crystal repot perchè non corretta")
    End Try
  End Function

  <DebuggerStepThrough()> _
Public Shared Function NTSTraduciLikeValue(ByVal strValueWithoutWildcards As String) As String
    NTSTraduciLikeValue = ""
    Try
      Dim sb As New System.Text.StringBuilder
      For i As Integer = 0 To strValueWithoutWildcards.Length - 1
        Dim c As Char = strValueWithoutWildcards(i)
        If c = "*" Or c = "%" Or c = "[" Or c = "]" Then
          sb.Append("[").Append(c).Append("]")
        Else
          If c = "'" Then
            sb.Append("''")
          Else
            sb.Append(c)
          End If
        End If
      Next
      Return sb.ToString()
    Catch ex As Exception
      Throw New Exception("BN__STD - NTSTraduciLikeValue: La stringa passata (" & strValueWithoutWildcards & ") non è stata convertita")
    End Try
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ConvOra60Ora100(ByVal dValore As Decimal, ByVal nDecSuTempiDiba As Integer) As Decimal
    'Converte l'ora da double base 60 (16,30) a dbl base 100 (16,5)
    Dim i As Integer = 0
    Dim strValore As String = ""
    Dim strOut As String = ""
    Dim lIntero As Decimal = 0
    Dim dMinuti As Decimal = 0
    Dim lInteroMinuti As Decimal = 0
    Dim lSecondi As Decimal = 0
    Dim dAppo As Decimal = 0

    ConvOra60Ora100 = 0

    'Se la variabile di arrotondamente è a 0 o maggiore di 8 la pone al valore
    'di default 4
    If nDecSuTempiDiba <= 0 Then nDecSuTempiDiba = 4
    If nDecSuTempiDiba >= 9 Then nDecSuTempiDiba = 4
    lIntero = Int(dValore)
    dMinuti = NTSCDec((dValore - lIntero + 0.0000000001) * 100)
    'dMinuti = ArrDbl(dMinuti, 4)
    dMinuti = ArrDbl(dMinuti, nDecSuTempiDiba)
    lInteroMinuti = Int(dMinuti)
    lSecondi = NTSCDec((dMinuti - lInteroMinuti + 0.0000000001) * 100)
    lSecondi = ArrDbl(lSecondi, 0)
    ' converte tutta la parte decimale in secondi
    dAppo = lInteroMinuti * 60 + lSecondi
    dAppo = lIntero + dAppo / 3600

    ConvOra60Ora100 = ArrDbl(dAppo, nDecSuTempiDiba)

  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ConvOra100Ora60(ByVal dValore As Decimal) As Decimal
    '------------------------------
    'Converte l'ora da double base 100 (16,50) a dbl base 60 (16,30) (arr. eccesso)
    Dim i As Integer = 0
    Dim strValore As String = ""
    Dim strOut As String = ""
    Dim lIntero As Decimal = 0
    Dim dMinuti As Decimal = 0
    Dim lInteroMinuti As Decimal = 0
    Dim lSecondi As Decimal = 0
    Dim dAppo As Decimal = 0
    Dim dRimasti As Decimal = 0
    Dim dA1 As Decimal = 0
    Dim dA2 As Decimal = 0
    Dim dA0 As Decimal = 0

    ConvOra100Ora60 = 0

    lIntero = Fix(dValore)
    ' trasforma i dec. in secondi
    lSecondi = NTSCDec((dValore - lIntero + 0.00000000001) * 10000)
    lSecondi = NTSCDec(lSecondi * 3600 / 10000 + 0.5)
    ' ora trasforma i secondi in minuti
    dMinuti = Fix(lSecondi / 60)
    dRimasti = Fix(lSecondi - (dMinuti * 60))
    If dRimasti > 59 Then dMinuti = dMinuti + 1 : dRimasti = dRimasti - 60
    If dMinuti > 59 Then lIntero = lIntero + 1 : dMinuti = dMinuti - 60

    dAppo = lIntero + dMinuti / 100 + dRimasti / 10000

    ConvOra100Ora60 = ArrDbl(dAppo, 4)
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ConvOra60Minuti(ByVal dValore As Decimal) As Decimal
    '------------------
    'Converte l'ora a base 60 in 'minuti, centesimi di minuto = secondi a base cento)
    'in vb6 questa funzione è chiamata erroneamente:  ConvOra100Minuti
    Dim dOra As Decimal = 0
    Dim dMinuti As Decimal = 0
    Dim dSecondi As Decimal = 0

    ConvOra60Minuti = 0

    dOra = Fix(dValore) * 60
    dMinuti = ArrDbl((dValore - Fix(dValore)) * 100, 2)
    dMinuti = Fix(dMinuti)
    dSecondi = ArrDbl((dValore - Fix(dValore) - (dMinuti / 100)) * 10000, 0)
    dMinuti = ArrDbl(dMinuti + dOra, 0)
    dSecondi = ArrDbl((dSecondi / 60 * 100), 2)
    ConvOra60Minuti = dMinuti + (dSecondi / 100)
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ConvMinutiOra60(ByVal dValore As Decimal) As Decimal
    '------------------
    'Converte i minuti,secondi in ora a base 60
    Dim dOra As Decimal = 0
    Dim dMinuti As Decimal = 0
    Dim dSecondi As Decimal = 0
    Dim dAppo As Decimal = 0

    ConvMinutiOra60 = 0

    dMinuti = Fix(dValore)
    dSecondi = Fix((dValore - dMinuti) * 100)
    'i secondi sono espressi su base 100: li converto su base 60
    dSecondi = Fix(dSecondi / 100 * 60)
    dOra = Fix(dMinuti / 60)
    dMinuti = dMinuti - (dOra * 60)     'minuti senza ore

    ConvMinutiOra60 = ArrDbl(dOra + (dMinuti / 100) + (dSecondi / 10000), 4)
  End Function

  Public Shared Function GestErrorCallThrow() As Boolean
    'GESTIONE ERRORI: con questa funzione decido se è possibile lanciare una Throw in quanto la routine 
    'è stata chiamata da un'altra routine di Bus, oppure devo scatenare una 'ThrowRemoteEvent'
    Dim bResult As Boolean = True
    Dim strace As New StackTrace()
    Dim count As Integer = 0
    Dim nThrow As Integer = 0
    Dim strTmp As String = ""
    Dim bSial As Boolean = False

    While count < strace.FrameCount 'And nThrow < 3
      If strace.GetFrame(count).GetMethod.ReflectedType.Namespace = "NTSInformatica" Then
        strTmp = strace.GetFrame(count).GetMethod.ReflectedType.Name.ToUpper

        If (strTmp.Substring(0, 3).ToUpper = "FRM" Or strTmp.Substring(0, 3).ToUpper = "FRO" Or _
            strTmp.Substring(0, 3).ToUpper = "FRK") And strTmp.ToUpper.IndexOf("FRM__CHIL") = -1 Then
          'se c'è una form faccio sempre una ThrowRemoteEvent
          Return False
        End If

        'per gestire alert id evento aggiunto da script di tipo email
        'altrimenti viene solo generato ma non invia l'email con business email, con MAPI andava
        '---gestire alert
        If strTmp.ToUpper = "CLE__SIAL" Then bSial = True
        If bSial Then
          If (strTmp.Substring(0, 3).ToUpper = "FRM" Or strTmp.Substring(0, 3).ToUpper = "FRO" Or _
              strTmp.Substring(0, 3).ToUpper = "FRK") Then Return False
        End If
        '---gestire alert

        If strTmp.Substring(0, 3) = "CLE" Or strTmp.Substring(0, 3) = "CLF" Or strTmp.Substring(0, 3) = "CLX" Or _
           strTmp.Substring(0, 3) = "CLD" Or strTmp.Substring(0, 3) = "CLH" Or strTmp.Substring(0, 3) = "CLY" Then nThrow += 1
      End If
      count = count + 1
    End While
    'se sono qui è perchè l'errore è stato scatenato direttamente dentro l'entity da una funzione non chiamada da un UI
    'ad esempio da una 'BeforeColUpdate'
    'se nello stack ho incontrato una sola volta l'oggetto CLE vuol dire che questa à l'ultima porta: 
    'se ora faccio la 'Throw New NTSExcept' si pianta tutto: devo per forza fare la 'ThrowRemoteEvent(New NTSEventA'
    'sperando che un UI sia in ascolto...
    If nThrow < 2 Then bResult = False

    Return bResult
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ValNum(ByRef strValore As String, ByVal strNomeCampo As String, ByVal dMin As Decimal, ByVal dMax As Decimal, ByVal nDecimali As Integer, Optional ByRef strErrorMessage As String = "") As Boolean
    Dim dTmp As Decimal = 0
    Dim strMsg As String = ""

    strMsg = "Nel campo '" & strNomeCampo & "' è possibile inserire solo valori numerici compresi tra " & dMin & " e " & dMax

    If Decimal.TryParse(strValore, dTmp) = False Then
      strErrorMessage = strMsg
      Return False
    End If

    If dTmp < dMin Or dTmp > dMax Then
      strErrorMessage = strMsg
      Return False
    End If

    dTmp = Math.Round(CType(dTmp, Double), nDecimali)

    strValore = dTmp.ToString()
    Return True

  End Function

  Public Shared Function CambiaPrefissoCampiDatatable(ByRef dttIn As DataTable, _
                                                      ByVal strTableNameStd As String, ByVal strNewTableName As String, _
                                                      ByVal strPrefStd As String, ByVal strNewPref As String) As Boolean
    '--------------------------------------
    'data una tabella (movord, movmag) sostituisce il prefisso del campo in modo da poter riutilizzare funzioni come beforecolupdate
    'esempio m_codart -> ee_codart, mm_codart -> ee_codart : entrambe BeforeColUpdateee_codart
    Dim i As Integer = 0
    Dim nLen As Integer = 0
    Try
      If dttIn Is Nothing Then Return True
      If dttIn.TableName = strTableNameStd Then dttIn.TableName = strNewTableName

      nLen = strPrefStd.Length

      For Each oColumn As DataColumn In dttIn.Columns
        If oColumn.ColumnName.StartsWith(strPrefStd) Then
          oColumn.ColumnName = strNewPref & oColumn.ColumnName.Substring(nLen)
        End If
      Next

      Return True
    Catch ex As Exception
      Throw ex
    End Try
  End Function


  Public Shared Function IsWindowsUser() As Boolean
    'restituisce true se l'utente di windows non può scrivere nella HKLM
    Dim hSC As Integer = 0
    Dim lRet As Integer = 0
    Try
      'la lettura del registro restituiva true se UAC attivo e virtualstore attivo !!!!
      'regRootKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\NTS\BUSINESS", True)
      'regRootKey.Close()

      'WinX20000, XP, 2003, Vista, 2008, 7
      IsWindowsUser = True
      hSC = OpenSCManager(vbNullString, vbNullString, GENERIC_READ Or GENERIC_WRITE Or GENERIC_EXECUTE)
      If (hSC <> 0) Then
        IsWindowsUser = False
      Else
        IsWindowsUser = True
      End If
      CloseServiceHandle(hSC)

    Catch ex As Exception
      Return True
    End Try
  End Function

  Public Shared Function IsWin64Bit() As Boolean
    Dim i As Integer = 0
    i = IntPtr.Size
    If i = 4 Then Return False '4 = x86, 8 = x64
    Return True
  End Function

  Public Shared Function IsWindowsTablet() As Boolean
    'per scrupolo su WIN minore di windows 10 restituisco sempre false
    If Environment.OSVersion.Version.Major < 10 Then Return False
    Dim state As Integer = GetSystemMetrics(SM_CONVERTIBLESLATEMODE)
    Return (state = 0) AndAlso (GetSystemMetrics(SM_TABLETPC) <> 0)
  End Function

  Public Shared Function IsWebApp() As Boolean
    If IOS Then Return False
    If ANDROID Then Return False
    If IsBis() Then Return False
    Dim strTmp As String = Application.ExecutablePath
    If strTmp.EndsWith("w3wp.exe", StringComparison.CurrentCultureIgnoreCase) Then
      Return True
    ElseIf strTmp.EndsWith("aspnet_wp.exe", StringComparison.CurrentCultureIgnoreCase) Then
      Return True
    ElseIf strTmp.EndsWith("webserver.exe", StringComparison.CurrentCultureIgnoreCase) Then
      Return True
    ElseIf strTmp.EndsWith("iisexpress.exe", StringComparison.CurrentCultureIgnoreCase) Then
      Return True
    ElseIf strTmp.IndexOf("idws", StringComparison.CurrentCultureIgnoreCase) > -1 Then
      Return True
    ElseIf WebDir.Trim <> "" Then
      Return True
    Else
      Return False
    End If
  End Function
  Public Shared Function IsBis() As Boolean
    If IOS Then Return False
    If ANDROID Then Return False
    If BUSIS Then Return True
    Dim strTmp As String = Application.ExecutablePath.ToLower
    If strTmp.Contains("sbs.exe") OrElse strTmp.Contains("sbs64.exe") OrElse _
       strTmp.Contains("sbs_service.exe") OrElse strTmp.Contains("sbs_service64.exe") OrElse _
       strTmp.Contains("sbs_core.exe") OrElse strTmp.Contains("sbs_core64.exe") Then
      BUSIS = True
      Return True
    End If
    Return False
  End Function

  Public Shared Function IsTerminalServer() As Boolean
    Dim strPCName As String = ""
    Dim lpBuffer As IntPtr = IntPtr.Zero
    Dim lCount As Integer = 0

    Try
      WTSQuerySessionInformation(IntPtr.Zero.ToInt32, -1, WTS_INFO_CLASS.WTSClientName, lpBuffer, lCount)
      strPCName = System.Runtime.InteropServices.Marshal.PtrToStringAuto(lpBuffer)
      'libera la memoria
      WTSFreeMemory(lpBuffer)
      lpBuffer = IntPtr.Zero

      If strPCName.Trim <> "" And strPCName <> Environment.MachineName Then
        'MsgBox("Sessione terminal server") : testato: OK
        Return True
      End If

      Return False
    Catch ex As Exception
      Return False
    End Try
  End Function

  Public Shared ReadOnly Property IsPalmare() As Boolean
    Get
      Try
        Return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height < 480 OrElse _
               System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width < 640
      Catch ex As Exception
        Return False
      End Try
    End Get
  End Property

  Public Shared Function SystemName() As String
    'Dim strValue As String = ""
    'Dim regRootKey As RegistryKey
    ''OK sia x x8 che per x64
    'regRootKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion", False)
    'If Not regRootKey Is Nothing Then
    '  strValue = CStr(regRootKey.GetValue("ProductName"))
    '  regRootKey.Close()
    '  regRootKey = Nothing
    'End If
    Return My.Computer.Info.OSFullName
  End Function

  Public Shared Function GetSettingReg(ByVal strkey1 As String, ByVal strkey2 As String, ByVal strValuename As String, ByVal strDefault As String) As String
    'legge informazioni dal reg di windows

    ' prima legge se gestito o meno installazione per macchina
    Dim strValue As String = "-1"
    Dim strRegRoot As String = ""
    Dim str64 As String = ""
    Dim strOut As String = ""
    Dim strKey3 As String = "*"
    Dim bFound As Boolean = False

    Dim strPc As String = System.Environment.MachineName
    Dim strUser As String = System.Environment.UserName
    Dim strSoftware As String = strkey1
    Dim strProfilo As String = strkey2
    Dim strDetail As String = "*"
    Dim strCfgDir As String = ""

    Dim oleConn As DbConnection = Nothing
    Dim oleDr As DbDataReader = Nothing
    Dim oleCmd As DbCommand = Nothing
    Try
      If Application_StartupPath = "" Then
        Application_StartupPath = Application.StartupPath
      End If
      strCfgDir = Application_StartupPath
      Dim lTmp As Integer = strProfilo.IndexOf("\")
      If lTmp > -1 Then
        strDetail = strProfilo.Substring(lTmp + 1)
        strProfilo = strProfilo.Substring(0, lTmp)
      End If

      If WebDir <> "" Then strCfgDir = WebDir

      'If IsWin64Bit() Then str64 = "\Wow6432Node"
      'per ora forziamo la compilazione busnet.exe come processo a 32 bit, per cui non serve

      '-------------------------------------------------
      'ottengo la stringa di connessione a arcproc dal file 'profilo.cfg'
      If PrcConn = "" Or (BUSIS And strProfilo.ToLower <> "debug" And strValuename <> "NoCheckTSUsers") Then 'la variabile PrcConn è condivisa, per cui se parto con un SBC.EXE con profilo che punta ad un arcproc, poi con un altro SBC.EXE che punta ald una altro arcproc anche il primo SBC.exe utilizzerà l'arcproc del secondo!!!
        If System.IO.File.Exists(strCfgDir & strSep & strProfilo & ".cfg") Then
          Dim r1 As New System.IO.StreamReader(strCfgDir & strSep & strProfilo & ".cfg", System.Text.Encoding.Default)
          PrcConn = r1.ReadToEnd
          r1.Close()
          r1.Dispose()
          If PrcConn.Substring(PrcConn.Length - 2) = vbCrLf Then
            PrcConn = PrcConn.Substring(0, PrcConn.Length - 2)
          End If
          If PrcConn.ToUpper.IndexOf("SERVER") = -1 And PrcConn.ToUpper.IndexOf("DATA SOURCE") = -1 Then Decodifica(PrcConn)
        Else
          If Not IsBis() Then 'Il messaggio su SBC viene gestito diversamente
            MsgBox("Il profilo '" & strCfgDir & strSep & strProfilo & ".cfg' non è stato trovato!" & vbCrLf & _
                   "Impossibile continuare.", MsgBoxStyle.Critical)
          End If
          Return "*-*" 'Busnet.exe se riceve questa stringa come valore di ritorno si chiude
        End If
      End If

      If PrcConn <> "" And GetSettingRegPerUser <> 0 Then
        strOut = strDefault

        If PrcConn.ToUpper.IndexOf("SERVER=") > -1 Then
          oleConn = New SqlConnection(PrcConn)
          oleCmd = New SqlCommand
        Else
          Dim ass As System.Reflection.Assembly = Nothing
          If IOS Or ANDROID Then
            ass = System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll")
            oleConn = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteConnection", True, True)), DbConnection)
            oleCmd = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
          Else
            ass = System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll")
            oleConn = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteConnection", True, True)), DbConnection)
            oleCmd = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
          End If
          oleConn.ConnectionString = PrcConn
        End If

        Try
          oleConn.Open()
        Catch ex As Exception
          If Not IsBis() Then 'Il messaggio su SBC viene gestito diversamente
            MsgBox("Si è verificato un errore in fase di collegamento con SQL SERVER. Il server potrebbe essere non raggiungibile, USERNAME o PWD errati, oppure il firewall del PC server blocca le connessioni SQL Server client. " & _
                   vbCrLf & vbCrLf & "Dettaglio errore: " & vbCrLf & ex.Message, MsgBoxStyle.Critical)
          End If
          Return "*-*" 'Busnet.exe se riceve questa stringa come valore di ritorno si chiude
        End Try
        oleCmd.Connection = oleConn
RIPROVA:
        'determino se installazione per macchina/utente o solo macchina
        If GetSettingRegPerUser = 9 Then
          oleCmd.CommandText = ParseSql("SELECT TOP 1 re_valprop FROM regedit " & _
                                " WHERE re_pc = '" & strPc & "'" & _
                                " AND re_user = '*' " & _
                                " AND re_software = '" & strSoftware & "' " & _
                                " AND re_profil = '*' " & _
                                " AND re_detail = '*' " & _
                                " AND re_nomprop = 'SystemInst'" & _
                                " ORDER BY re_user DESC", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
          oleCmd.Connection = oleConn
          oleDr = oleCmd.ExecuteReader()
          If oleDr.HasRows Then
            oleDr.Read()
            GetSettingRegPerUser = Val(oleDr("re_valprop").ToString())
            If GetSettingRegPerUser <> 0 And GetSettingRegPerUser <> 1 And GetSettingRegPerUser <> 2 Then GetSettingRegPerUser = 1 'default per macchina
          Else
            If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
              WriteMsgBoxToLog("Error reading ARCPROC table REGEDIT: no record found: " & vbCrLf & oleCmd.CommandText & vbCrLf & " Used default value (1)")
              GetSettingRegPerUser = 1 'default per macchina da arcproc
            Else
              'in ARCPROC.REGEDIT non ho nessun record con NOMEPC = al mio e profilo = quello impostato:
              'se posso lo carico al volo da file .xml (funzionalità interna per sviluppatori)
              If System.IO.File.Exists(CLN__STD.Application_StartupPath & strSep & strPc & "_" & strProfilo & ".xml") Then
                If MessageBox.Show("Non è stato trovato nessun profilo con NOME_PC e PROFILO uquale a quello impostato: lo carico dal file '" & Application_StartupPath & strSep & strPc & "_" & strProfilo & ".xml' (actkey compresa)?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                  MsgBox("Error reading ARCPROC table REGEDIT: no record found: " & vbCrLf & oleCmd.CommandText & vbCrLf & " Used default value (1)", MsgBoxStyle.Exclamation, "Business NET")
                  GetSettingRegPerUser = 1 'default per macchina da arcproc
                Else
                  oleDr.Close()
                  Dim dttTmp As New DataTable
                  dttTmp.ReadXml(Application_StartupPath & strSep & strPc & "_" & strProfilo & ".xml")
                  For Each dtrT As DataRow In dttTmp.Rows
                    oleCmd.CommandText = ParseSql("INSERT INTO regedit (re_pc, re_user, re_software, re_profil, re_detail, re_nomprop, re_valprop) " & _
                           "VALUES ('" & dtrT!re_pc.ToString & "', '" & dtrT!re_user.ToString & "', '" & dtrT!re_software.ToString & "', " & _
                           "'" & dtrT!re_profil.ToString & "', '" & dtrT!re_detail.ToString & "', '" & dtrT!re_nomprop.ToString & "', " & _
                           "'" & dtrT!re_valprop.ToString & "')", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
                    oleCmd.ExecuteNonQuery()
                    'se da errore: cavoli del programmatore: è una funzione di uso prettamente interno!!!!
                  Next
                  GoTo RIPROVA
                End If
              Else
                'caso standard: do errore!!!!
                MsgBox("Error reading ARCPROC table REGEDIT: no record found: " & vbCrLf & oleCmd.CommandText & vbCrLf & " Used default value (1)", MsgBoxStyle.Exclamation, "Business NET")
                GetSettingRegPerUser = 1 'default per macchina da arcproc
              End If
            End If
          End If
          oleDr.Close()
        End If    'If GetSettingRegPerUser = 9 Then
        oleCmd.CommandText = ParseSql("SELECT TOP 1 re_valprop FROM regedit " & _
                              " WHERE re_pc = '" & strPc & "'" & _
                              " AND re_user = '" & IIf(GetSettingRegPerUser <> 2, "*", strUser).ToString & "'" & _
                              " AND re_software = '" & strSoftware & "' " & _
                              " AND re_profil = '" & strProfilo & "' " & _
                              " AND re_detail = '" & strDetail & "' " & _
                              " AND re_nomprop = '" & strValuename & "'" & _
                              " ORDER BY re_user DESC", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
        oleCmd.Connection = oleConn
        oleDr = oleCmd.ExecuteReader()
        If oleDr.HasRows Then
          oleDr.Read()
          strOut = oleDr("re_valprop").ToString()
        End If
        oleDr.Close()
        oleConn.Close()
        Return strOut
      End If    'If strPrcConn <> "" Then

      Return strValue

    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error reading Windows registry (System " & IIf(str64 <> "", "64", "32").ToString & " bit)" & vbCrLf & "Error: " & ex.ToString)
      Else
        If IsWebApp() Or IOS Or ANDROID Or IsBis() Then
          Throw ex
        Else
          MsgBox("Error reading Windows registry (System " & IIf(str64 <> "", "64", "32").ToString & " bit)" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
        End If
      End If
      Return strDefault
    End Try

  End Function

  Public Shared Sub SaveSettingReg(ByVal strkey1 As String, ByVal strkey2 As String, ByVal strValueName As String, ByVal strValue As String)
    'legge informazioni dal reg di windows

    ' prima legge se gestito o meno installazione per macchina
    Dim strVal As String = "-1"
    Dim strRegRoot As String = ""
    Dim str64 As String = ""
    Dim strKey3 As String = "*"
    Dim bFound As Boolean = False

    Dim strPc As String = System.Environment.MachineName
    Dim strUser As String = System.Environment.UserName
    Dim strSoftware As String = strkey1
    Dim strProfilo As String = strkey2
    Dim strDetail As String = "*"
    Dim strCfgDir As String = Application_StartupPath

    Dim oleConn As DbConnection = Nothing
    Dim oleDr As DbDataReader = Nothing
    Dim oleCmd As DbCommand = Nothing

    Try
      Dim lTmp As Integer = strProfilo.IndexOf("\")
      If lTmp > -1 Then
        strDetail = strProfilo.Substring(lTmp + 1)
        strProfilo = strProfilo.Substring(0, lTmp)
      End If

      If WebDir <> "" Then strCfgDir = WebDir

      'ottengo la stringa di connessione ad arcproc
      If PrcConn = "" Or (BUSIS And strProfilo.ToLower <> "debug" And strValueName <> "NoCheckTSUsers") Then
        If System.IO.File.Exists(strCfgDir & strSep & strProfilo & ".cfg") Then
          Dim r1 As New System.IO.StreamReader(strCfgDir & strSep & strProfilo & ".cfg", System.Text.Encoding.Default)
          PrcConn = r1.ReadToEnd
          r1.Close()
          r1.Dispose()
          If PrcConn.Substring(PrcConn.Length - 2) = vbCrLf Then
            PrcConn = PrcConn.Substring(0, PrcConn.Length - 2)
          End If
          If PrcConn.ToUpper.IndexOf("SERVER") = -1 And PrcConn.ToUpper.IndexOf("DATA SOURCE") = -1 Then Decodifica(PrcConn)
        Else
          Throw New Exception("BN__STD.SaveSettingReg: File '" & strCfgDir & strSep & strProfilo & ".cfg' non found!")
          Return
        End If
      End If

      If PrcConn <> "" And GetSettingRegPerUser <> 0 Then
        If PrcConn.ToUpper.IndexOf("SERVER=") > -1 Then
          oleConn = New SqlConnection(PrcConn)
          oleCmd = New SqlCommand
        Else
          Dim ass As System.Reflection.Assembly = Nothing
          If IOS Or ANDROID Then
            ass = System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll")
            oleConn = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteConnection", True, True)), DbConnection)
            oleCmd = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
          Else
            ass = System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll")
            oleConn = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteConnection", True, True)), DbConnection)
            oleCmd = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
          End If
          oleConn.ConnectionString = PrcConn
        End If
        Try
          oleConn.Open()
        Catch ex As Exception
          Throw New Exception(vbCrLf & "BN__STD.SaveSettingReg: Si è verificato un errore in fase di collegamento con SQL SERVER. Il server potrebbe essere non raggiungibile, USERNAME o PWD errati, oppure il firewall del PC server blocca le connessioni SQL Server client. " & vbCrLf & vbCrLf & "Dettaglio errore: " & ex.ToString)
        End Try
        oleCmd.Connection = oleConn

        'determino se installazione per macchina/utente o solo macchina
        If GetSettingRegPerUser = 9 Then
          oleCmd.CommandText = ParseSql("SELECT TOP 1 re_valprop FROM regedit " & _
                              " WHERE re_pc = '" & strPc & "'" & _
                              " AND re_user = '*' " & _
                              " AND re_software = '" & strSoftware & "' " & _
                              " AND re_profil = '*' " & _
                              " AND re_detail = '*' " & _
                              " AND re_nomprop = 'SystemInst'" & _
                              " ORDER BY re_user DESC", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
          oleCmd.Connection = oleConn
          oleDr = oleCmd.ExecuteReader()
          If oleDr.HasRows Then
            oleDr.Read()
            GetSettingRegPerUser = Val(oleDr("re_valprop").ToString())
            If GetSettingRegPerUser <> 0 And GetSettingRegPerUser <> 1 And GetSettingRegPerUser <> 2 Then GetSettingRegPerUser = 1 'default per macchina
          Else
            If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
              'se sono stato avviato in modalità batch ...
              WriteMsgBoxToLog("Error reading ARCPROC.REGEDIT: no record found: " & oleCmd.CommandText & " - Used default value (1)")
            Else
              MsgBox("Error reading ARCPROC.REGEDIT: no record found: " & vbCrLf & oleCmd.CommandText & vbCrLf & " Used default value (1)", MsgBoxStyle.Exclamation, "Business NET")
            End If
            GetSettingRegPerUser = 1 'default per macchina da arcproc
          End If
          oleDr.Close()
        End If    'If GetSettingRegPerUser = 9 Then

        oleCmd.CommandText = ParseSql("DELETE FROM regedit " & _
                " WHERE re_pc = '" & System.Environment.MachineName & "'" & _
                " AND re_user = '" & IIf(GetSettingRegPerUser <> 2, "*", strUser).ToString & "'" & _
                " AND re_software = '" & strSoftware & "' " & _
                " AND re_profil = '" & strProfilo & "' " & _
                " AND re_detail = '" & strDetail & "' " & _
                " AND re_nomprop = '" & strValueName & "' ", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
        oleCmd.ExecuteNonQuery()

        oleCmd.CommandText = ParseSql("INSERT INTO regedit (re_pc, re_user, re_software, re_profil, re_detail, re_nomprop, re_valprop) " & _
               "VALUES ('" & System.Environment.MachineName & "', '" & IIf(GetSettingRegPerUser <> 2, "*", strUser).ToString & "', '" & _
               strSoftware & "',  '" & strProfilo & "', " & "'" & strDetail & "', '" & strValueName & "', '" & strValue & "')", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
        oleCmd.ExecuteNonQuery()
        oleConn.Close()
        Return
      End If    'If strPrcConn <> "" Then

    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error writing Windows registry" & vbCrLf & "Error: " & ex.ToString)
      Else
        MsgBox("Error writing Windows registry" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
      Return
    End Try

  End Sub

  Public Shared Function SettingRegChange(ByVal strSoftware As String, ByVal strProfilo As String, ByVal baUtente As Boolean) As Boolean
    'baUtente =true converto da installazione per macchina ad installazione per utente, diversamente il contrario
    Dim strPc As String = System.Environment.MachineName
    Dim strUser As String = System.Environment.UserName

    Dim strCfgDir As String = Application_StartupPath

    Dim oleConn As DbConnection = Nothing
    Dim oleDr As DbDataReader = Nothing
    Dim oleCmd As DbCommand = Nothing
    Dim traDb As DbTransaction = Nothing

    Try
      If WebDir <> "" Then strCfgDir = WebDir

      'ottengo la stringa di connessione ad arcproc
      If PrcConn = "" Then
        If System.IO.File.Exists(strCfgDir & strSep & strProfilo & ".cfg") Then
          Dim r1 As New System.IO.StreamReader(strCfgDir & strSep & strProfilo & ".cfg", System.Text.Encoding.Default)
          PrcConn = r1.ReadToEnd
          r1.Close()
          r1.Dispose()
          If PrcConn.Substring(PrcConn.Length - 2) = vbCrLf Then
            PrcConn = PrcConn.Substring(0, PrcConn.Length - 2)
          End If
          If PrcConn.ToUpper.IndexOf("SERVER") = -1 And PrcConn.ToUpper.IndexOf("DATA SOURCE") = -1 Then Decodifica(PrcConn)
        Else
          Throw New Exception("BN__STD.SaveSettingReg: File '" & strCfgDir & strSep & strProfilo & ".cfg' non found!")
          Return False
        End If
      End If

      If PrcConn.ToUpper.IndexOf("SERVER=") > -1 Then
        oleConn = New SqlConnection(PrcConn)
        oleCmd = New SqlCommand
      Else
        Dim ass As System.Reflection.Assembly = Nothing
        If IOS Or ANDROID Then
          ass = System.Reflection.Assembly.LoadFrom("Mono.Data.Sqlite.dll")
          oleConn = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteConnection", True, True)), DbConnection)
          oleCmd = CType(Activator.CreateInstance(ass.GetType("Mono.Data.Sqlite.SqliteCommand", True, True)), DbCommand)
        Else
          ass = System.Reflection.Assembly.LoadFrom("System.Data.SQLite.dll")
          oleConn = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteConnection", True, True)), DbConnection)
          oleCmd = CType(Activator.CreateInstance(ass.GetType("System.Data.SQLite.SQLiteCommand", True, True)), DbCommand)
        End If
        oleConn.ConnectionString = PrcConn
      End If

      Try
        oleConn.Open()
      Catch ex As Exception
        Throw New Exception(vbCrLf & "BN__STD.SettingRegChange: Si è verificato un errore in fase di collegamento con SQL SERVER. Il server potrebbe essere non raggiungibile, USERNAME o PWD errati, oppure il firewall del PC server blocca le connessioni SQL Server client. " & vbCrLf & vbCrLf & "Dettaglio errore: " & ex.ToString)
      End Try
      oleCmd.Connection = oleConn

      'determino se installazione per macchina/utente o solo macchina
      If GetSettingRegPerUser = 9 Then
        oleCmd.CommandText = ParseSql("SELECT TOP 1 re_valprop FROM regedit " & _
                            " WHERE re_pc = '" & strPc & "'" & _
                            " AND re_user = '*' " & _
                            " AND re_software = '" & strSoftware & "' " & _
                            " AND re_profil = '*' " & _
                            " AND re_detail = '*' " & _
                            " AND re_nomprop = 'SystemInst'" & _
                            " ORDER BY re_user DESC", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
        oleCmd.Connection = oleConn
        oleDr = oleCmd.ExecuteReader()
        If oleDr.HasRows Then
          oleDr.Read()
          GetSettingRegPerUser = Val(oleDr("re_valprop").ToString())
          If GetSettingRegPerUser <> 0 And GetSettingRegPerUser <> 1 And GetSettingRegPerUser <> 2 Then GetSettingRegPerUser = 1 'default per macchina
        Else
          GetSettingRegPerUser = 1 'default per macchina da arcproc
        End If
        oleDr.Close()
      End If    'If GetSettingRegPerUser = 9 Then

      'va già bene così, non occorre fare nulla
      If GetSettingRegPerUser = 2 And baUtente Then Return True
      If GetSettingRegPerUser = 1 And baUtente = False Then Return True
      If GetSettingRegPerUser = 0 Then
        Throw New Exception(vbCrLf & "BN__STD.SettingRegChange: Non è possibile cambiare tipo di installazione di Business perchè il registro non è contenuto in Arcproc")
      End If

      'apro la transazione
      traDb = oleConn.BeginTransaction()
      oleCmd.Transaction = traDb

      oleCmd.CommandText = ParseSql("DELETE FROM regedit " & _
                          " WHERE re_pc = '" & System.Environment.MachineName & "'" & _
                          " AND re_user = '" & IIf(baUtente = False, "*", strUser).ToString & "'" & _
                          " AND re_software = '" & strSoftware & "' " & _
                          " AND re_profil = '" & strProfilo & "'", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
      oleCmd.ExecuteNonQuery()

      oleCmd.CommandText = ParseSql("INSERT INTO regedit (re_pc, re_user, re_software, re_profil, " & _
                          " re_detail, re_nomprop, re_valprop) " & _
                          " SELECT re_pc, '" & IIf(baUtente = False, "*", strUser).ToString & "', re_software, re_profil, " & _
                          " re_detail, re_nomprop, re_valprop" & _
                          " FROM regedit " & _
                          " WHERE re_pc = '" & System.Environment.MachineName & "'" & _
                          " AND re_user = '" & IIf(baUtente, "*", strUser).ToString & "'" & _
                          " AND re_software = '" & strSoftware & "' " & _
                          " AND re_profil = '" & strProfilo & "'", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
      oleCmd.ExecuteNonQuery()

      oleCmd.CommandText = ParseSql("UPDATE regedit SET re_valprop = '" & IIf(baUtente, "2", "1").ToString & "'" & _
                                " WHERE re_pc = '" & System.Environment.MachineName & "'" & _
                                " AND re_user = '*'" & _
                                " AND re_software = '" & strSoftware & "' " & _
                                " AND re_profil = '*'" & _
                                " AND re_nomprop = 'SystemInst'", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
      oleCmd.ExecuteNonQuery()

      oleCmd.CommandText = ParseSql("DELETE FROM regedit " & _
                    " WHERE re_pc = '" & System.Environment.MachineName & "'" & _
                    " AND re_user = '" & IIf(baUtente, "*", strUser).ToString & "'" & _
                    " AND re_software = '" & strSoftware & "' " & _
                    " AND re_profil = '" & strProfilo & "'", IIf(PrcConn.ToUpper.IndexOf("SERVER=") > -1, 0, 1))
      oleCmd.ExecuteNonQuery()

      'chiudo la transazione
      traDb.Commit()
      oleConn.Close()

      If baUtente Then
        GetSettingRegPerUser = 2
      Else
        GetSettingRegPerUser = 1
      End If

      Return True

    Catch ex As Exception
      If oleConn.State = ConnectionState.Open Then
        If Not traDb Is Nothing Then traDb.Rollback() 'annullo la transazione
        oleConn.Close()
      End If

      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error writing Windows registry" & vbCrLf & "Error: " & ex.ToString)
      Else
        MsgBox("Error writing Windows registry" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
      Return False
    End Try
  End Function

  Public Shared Function TraduciMessaggio(ByVal strProgram As String, ByVal strDir As String, ByVal strLingua As String, ByVal lMsgId As Long, ByVal strMsg As String) As String
    'dato un messaggio, lo traduce nella lingua dell'operatore di business
    'strLingua: IT-it, US-bg, ... da oApp.User.
    Dim fI As StreamReader
    Dim fO As StreamWriter
    Dim strNomeFile As String = (strDir + strProgram + "_" + strLingua + ".int")
    Dim strTestoFile As String = ""
    Dim strMessage As String = ""
    Dim i As Integer
    Dim e As Integer

    Dim a As Integer
    Dim b As Integer
    Try
      If File.Exists(strNomeFile) Then

        fI = New StreamReader(strNomeFile)
        strTestoFile = fI.ReadToEnd
        fI.Close()

        i = strTestoFile.IndexOf(lMsgId.ToString + ":")
        If i > -1 Then
          i = strTestoFile.IndexOf(":", i) + 1
          e = strTestoFile.IndexOf("*-*", i)
          If e = -1 Then e = strTestoFile.Length
          strMessage = strTestoFile.Substring(i, e - i).Trim

          '-------------------------------
          'ora devo sostituire il testo variabile
          'strMessage = messaggio tradotto
          'strMsg = messaggio di default
          i = strMsg.IndexOf("|")
          While i > -1
            e = strMsg.IndexOf("|", i + 1)
            If e = -1 Then Exit While
            a = strMessage.IndexOf("|")
            If a > -1 Then
              b = strMessage.IndexOf("|", a + 1)
              If b = strMessage.Length - 1 Then
                strMessage = strMessage.Substring(0, a) & strMsg.Substring(i + 1, e - (i + 1))
              Else
                strMessage = strMessage.Substring(0, a) & strMsg.Substring(i + 1, e - (i + 1)) & strMessage.Substring(b + 1)
              End If
            End If
            i = strMsg.IndexOf("|", e + 1)
          End While

          'strMessage += vbCrLf + vbCrLf + "Translation: Program: " + strProgram + vbCrLf + _
          '                            "                    ID          : " + lMsgId.ToString()

          Return strMessage
        Else
          '------------------------------
          'nel file non c'è il messaggio tradotto: lo aggiungo
          fO = New StreamWriter(strNomeFile, True)
          fO.WriteLine(lMsgId.ToString() + ":" + strMsg + "*-*")
          fO.Flush()
          fO.Close()

          'If strMsg.IndexOf("Translation: Program:") = -1 Then
          '  strMsg += vbCrLf + vbCrLf + "Translation: Program: " + strProgram + vbCrLf + _
          '                              "                    ID          : " + lMsgId.ToString()
          'End If

          Return strMsg.Replace("|", "")
        End If
      Else
        '------------------------------
        'il file non c'è: lo creo e gli aggiungo il messaggio
        If Not Directory.Exists(strDir) Then Directory.CreateDirectory(strDir)
        fO = New StreamWriter(strNomeFile)
        fO.WriteLine(lMsgId.ToString() + ":" + strMsg + "*-*")
        fO.Flush()
        fO.Close()
      End If    'If File.Exists(strNomeFile) Then


      'If strMsg.IndexOf("Translation: Program:") = -1 Then
      '  strMsg += vbCrLf + vbCrLf + "Translation: Program: " + strProgram + vbCrLf + _
      '                              "                    ID          : " + lMsgId.ToString()
      'End If
      Return strMsg.Replace("|", "")

    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error during translate message" & vbCrLf & "Error: " & ex.Message)
      Else
        MsgBox("Error during translate message" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
      Return strMsg.Replace("|", "")
    End Try
  End Function

  Public Shared Function GestError(ByVal Errore As Exception, ByRef objClasse As Object, ByVal strMoreInformation As String, ByVal strAppInformation As String, ByVal strNomeFileLog As String, ByVal bVisMsgBox As Boolean) As String
    'gestione degli errori
    Dim strOut As String = ""
    Dim strTmp() As String
    Dim strNomePrg As String = ""
    Dim strStack As String = ""
    Dim strErrore As String = Errore.Message
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim bMsgboxOlny As Boolean = False
    Dim strTemp As String = ""
    Dim bNoTrace As Boolean = False 'se true non devo accodare al messaggio i reference alle dll collegate e le opzioni di registro

    Try
      Dim strType As String = Errore.ToString()

      'Le eccezioni di ThreadAbort vengono scatenate direttamente dal programma per terminare il Thread senza aspettare la fine.
      'Non vanno segnalate come errori.
      If Errore.GetType.Equals(GetType(ThreadAbortException)) Then bVisMsgBox = False : Return True

      '-----------------------------------------------------------
      'tolgo dal nome del programma/versione il 'culture' e 'PublicToken', se c'è
      strNomePrg = objClasse.GetType.AssemblyQualifiedName.ToString
      i = strNomePrg.IndexOf(",")
      If i = -1 Then i = strNomePrg.Length
      strNomePrg = strNomePrg.Substring(0, i).Replace("NTSInformatica.", "")
      strNomePrg = objClasse.GetType.Module.Name & " - " & strNomePrg
      Try
        strNomePrg += " - File Ver.: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(objClasse.GetType.Module.FullyQualifiedName).FileVersion
      Catch
        'per dll caricate in memoria il file non esiste !!!
        'ignoro l'errore
      End Try

      '-----------------------------------------------------------
      strOut = "Error occured: " & vbCrLf

      '-----------------------------------------------------------
      'aggiungo informazioni relative al database/matricola/...
      If strAppInformation <> "" Then
        strTmp = strAppInformation.Split(CType("|", Char))
        strOut += vbCrLf & "Date: " & DateTime.Now.ToShortDateString & " " & DateTime.Now.ToShortTimeString & vbCrLf & _
                 "Program: " & strNomePrg & vbCrLf & _
                 "OS: " & SystemName() & " (" & Environment.OSVersion.VersionString & ")" & Environment.OSVersion.ServicePack & " - Application runs on " & IIf(IsWin64Bit, " x64 mode", " x86 mode").ToString & vbCrLf & _
                 "Type/Name/Matr/User database: " & strTmp(0) & " - " & strTmp(1) & " - " & strTmp(2) & " - " & strTmp(3)
      End If
      strOut += vbCrLf & "".PadLeft(80, CChar("-")) & vbCrLf & vbCrLf & _
              "Error: " & vbCrLf & vbCrLf

      '-----------------------------------------------------------
      'determino il tipo di errore
      strType = strType.Substring(0, strType.IndexOf(":"))
      For i = strType.Length - 1 To 0 Step -1
        If strType.Substring(i, 1) = "." Then
          strType = strType.Substring(i + 1)
          Exit For
        End If
      Next
      Select Case strType
        Case "NTSException", "TargetInvocationException"
          If strErrore.Length > 12 Then
            If strErrore.Substring(0, 12) = "ATTENZIONE: " Then
              strOut = strErrore
              bMsgboxOlny = True
              Exit Select
            End If
          End If

          If strType = "TargetInvocationException" Then
            'è il caso di un errore non di tipo NTSException scatenato dentro as un 'beforecolupdate' e che si sta processando nella beforecolupdate standard
            If NTSCStr(Errore.InnerException).ToUpper.IndexOf("NTSINFORMATICA") > -1 Then
              strErrore = NTSCStr(Errore.InnerException)
            End If
          End If

          'prendo l'errore e gli sostituisco lo stack/trace, visto che la composizione del messaggio è già stata fatta in precedenza
          i = strErrore.IndexOf("###")
          If i > -1 Then
            'tolgo i reference e li aggiungo dopo
            Dim strReference As String = ""
            i = strErrore.IndexOf("*Reference*:")
            If i > -1 Then
              strReference = strErrore.Substring(i - 89)
              strErrore = strErrore.Substring(0, i - 89)
            End If
            i = strReference.IndexOf(" ***")
            If i > -1 Then strReference = strReference.Substring(0, i + 4)

            i = strErrore.IndexOf("###")
            'se entro qui vuol dire che l'errore mi è stato passato da un throw (es dal dal e ora sono nell'entity):
            'il messaggio di errore è solo quello in Errore.Message, a cui accodo lo stack trace
            strOut = strErrore.Substring(0, i) & vbCrLf
            'dallo stack devo togliere la prima parte in quanto già contenuta nel messaggio precedente con maggior precisione
            i = Errore.StackTrace.IndexOf(" at ")
            If i < 0 Then
              'è la versione di framework in itliano
              i = Errore.StackTrace.IndexOf(" in ")
              i = Errore.StackTrace.IndexOf(" in ", i + 1)
            Else
              i = Errore.StackTrace.IndexOf(" at ", i + 1)
            End If

            strStack = "Call from " & vbCrLf & _
                      Errore.StackTrace.Substring(i + 3).Replace(vbCrLf, " ").Replace(" at ", vbCrLf).Trim() & _
                      "  ###" & vbCrLf

            i = strStack.IndexOf("(")
            While i > -1      'tolgo la firma della funzione che ha scatenato l'errore
              strStack = strStack.Substring(0, i) & strStack.Substring(strStack.IndexOf(")") + 1)
              i = strStack.IndexOf("(")
            End While
            strOut += strStack

            'aggiungo i reference
            If strReference <> "" Then strOut += strReference

            '---------------------------------------------
            'scrivo l'errore nel file di log
            WriteErrorFile(strNomeFileLog, strOut)
            '---------------------- OK: ESCO ---------------
            Return strOut
          Else
            strOut += strErrore
          End If
        Case "SqlException"
          Dim exSQL As SqlException = CType(Errore, SqlException)
          If exSQL.Number = 2627 Then
            'violation of primary key
            strOut = "ATTENZIONE: Impossibile inserire/modificare il record: nella tabella è già presente un record con la stessa chiave primaria" & _
                     vbCrLf & vbCrLf & "Ulteriori dettagli: " & vbCrLf & exSQL.Message & vbCrLf & vbCrLf & vbCrLf & strMoreInformation
            If exSQL.Message.ToLower.Contains("'dbo.conness'") Then strOut = ""
            bNoTrace = True
            bMsgboxOlny = True
          ElseIf exSQL.Number = 547 Then
            'delete /update reference
            If exSQL.Message.ToString.Substring(0, 15).ToUpper().IndexOf("DELETE") > -1 Then
              strOut = "ATTENZIONE: Impossibile cancellare il record: è referenziato in un'altra tabella"
              bNoTrace = True
              i = exSQL.Message.IndexOf("table """)
              If i > -1 Then
                strOut += vbCrLf & "(Tabella: " & exSQL.Message.Substring(i + 7, exSQL.Message.IndexOf("""", i + 10) - (i + 7)) & ")"
              End If
            Else
              If exSQL.Message.ToString.Substring(0, 15).ToUpper().IndexOf("UPDATE") > -1 Then
                strOut = "ATTENZIONE: Impossibile aggiornare il record: "
              Else
                strOut = "ATTENZIONE: Impossibile inserire il record: "
              End If
              bNoTrace = True

              i = exSQL.Message.IndexOf(" REFERENCE constraint ")
              If i > -1 Then
                i = exSQL.Message.IndexOf("table """)
                If i > -1 Then
                  strOut += vbCrLf & " è richiesto lo stesso dato nella tabella: " & exSQL.Message.Substring(i + 7, exSQL.Message.IndexOf("""", i + 10) - (i + 7)) & ")"
                Else
                  '... errore generico
                  strOut += vbCrLf & exSQL.Message
                End If
              Else
                i = exSQL.Message.IndexOf(" CHECK constraint ")         '18
                If i > -1 Then
                  strTemp = ""
                  i += 19
                  l = exSQL.Message.IndexOf("""", i + 1)
                  If l > i Then strTemp = exSQL.Message.Substring(i, l - i)
                  i = exSQL.Message.IndexOf("column '") + 8
                  l = exSQL.Message.IndexOf("'", i + 1)
                  If i > -1 Then
                    strOut += "Nella colonna '*" & exSQL.Message.Substring(i, l - i) & "*' è stato inserito un valore non consentito"
                    bNoTrace = True
                    If strTemp <> "" Then strOut += vbCrLf & "(Vincolo *" & strTemp & "*)"
                  Else
                    '... errore generico
                    strOut += vbCrLf & exSQL.Message
                    If strMoreInformation.Trim <> "" Then strOut += vbCrLf & vbCrLf & vbCrLf & "More information:" & vbCrLf & strMoreInformation
                  End If
                Else
                  '... errore generico
                  strOut += vbCrLf & exSQL.Message
                  If strMoreInformation.Trim <> "" Then strOut += vbCrLf & vbCrLf & vbCrLf & "More information:" & vbCrLf & strMoreInformation
                End If
              End If
            End If

            bMsgboxOlny = True
          Else
            If exSQL.Number = 1326 Or exSQL.Number = -2 Or exSQL.Number = 18456 Then
              strOut += "Si è verificato un errore in fase di collegamento con SQL SERVER. Il server potrebbe essere non raggiungibile, USERNAME o PWD errati, oppure il firewall del PC server blocca le connessioni SQL Server client. " & vbCrLf & vbCrLf & "Dettaglio errore: " & vbCrLf
            End If
            bNoTrace = True
            strOut += "SqlException: " & exSQL.Number & "  -  " & exSQL.Message & "  Error number: " & exSQL.ErrorCode
            If NTSCStr(exSQL.Procedure).Trim <> "" Then strOut += vbCrLf & "Procedure: " & NTSCStr(exSQL.Procedure).Trim
          End If

          'Case "OleDbException"
          '  Dim exOleDb As OleDbException = CType(Errore, OleDbException)
          '  strOut += "OleDbException: " & exOleDb.ErrorCode & "  -  " & exOleDb.Message

        Case "COMException (0x80070006)"
          bNoTrace = True
          strErrore += vbCrLf & " In caso di errore in invio FAX tramite Fax di Windows Xp/2000/2003/Vista l'errore potrebbe essere dovuto ad una non corretta impostazione dell'opzione di registro 'BUSINESS/OPZIONI/FaxServiceMachineXP2003': nel caso cancellare l'opzione e reinserirla. Eventualmente provare ad inviare un fax direttamente dalla stampante/fax che deve essere presente nella cartella delle stampanti "
          strOut += strErrore & "  (error type: " & strType & ")"

        Case Else
          strOut += strErrore & "  (error type: " & strType & ")"

          If strType = "InvalidCastException" AndAlso (strErrore.IndexOf("NTSInformatica.CLH", StringComparison.CurrentCultureIgnoreCase) > -1 OrElse _
                                                       strErrore.IndexOf("NTSInformatica.CLF", StringComparison.CurrentCultureIgnoreCase) > -1) Then
            strOut += vbCrLf & vbCrLf & "L'errore potrebbe essere causato da: " & vbCrLf & _
                                        " - Il file DLLMAP.INI non è aggiornato o non è raggiungibile." & vbCrLf & _
                                        " - La DLL personalizzata non è presente nella cartella di Business."
          End If
      End Select

      If bMsgboxOlny = False Then
        '-----------------------------------------------------------
        'aggiungo le informazioni aggiuntive (as esempio la query passata dal DAL)
        If strMoreInformation <> "" Then
          strOut += vbCrLf & _
                  "".PadLeft(80, CChar("-")) & _
                  vbCrLf & "More informations: " & vbCrLf & strMoreInformation & vbCrLf
        End If

        strOut += vbCrLf & "".PadLeft(80, CChar("-")) & _
                  vbCrLf & "Error occurred in :"
        If Errore.StackTrace.IndexOf("NTSInformatica.") > -1 Then
          strStack = Errore.StackTrace.Substring(Errore.StackTrace.IndexOf("NTSInformatica.")) & "  ###" & vbCrLf
          i = strStack.IndexOf("(")
          While i > -1      'tolgo la firma della funzione che ha scatenato l'errore
            strStack = strStack.Substring(0, i) & strStack.Substring(strStack.IndexOf(")") + 1)
            i = strStack.IndexOf("(")
          End While
          strOut += " " & vbCrLf & strStack

        End If


        '---------------------------------------------
        'aggiungo la versione delle dll coinvolte
        strTemp = ""
        If bInError = False And bNoTrace = False Then
          bInError = True
          Try
            Dim strTmp1 As String = ""
            GetFileVersion(False, objClasse, objClasse.oapp.serverdir, strTemp)

            strTmp1 = vbCrLf & vbCrLf & vbCrLf & _
                      "".PadLeft(80, CChar("-")) & vbCrLf & _
                      "*Reference*:" & vbCrLf & _
                      strTemp

            '---------------------------------------------
            'aggiungo le opzioni di registro settate (... anche le pwd ...)
            'se objClasse è un entity o un dal, non conosco omenu ...
            If IsWebApp() = False And IsBis() = False Then
              Dim dttTmp As New DataTable
              objClasse.oApp.oMenu.GetGregOpzioni(objClasse.GetType.ToString.ToUpper.Replace("NTSINFORMATICA", "").PadRight(4).Substring(4), dttTmp)

              strTmp1 += vbCrLf & vbCrLf & vbCrLf & _
                        "".PadLeft(80, CChar("-")) & vbCrLf & _
                        "*Opzioni di registro impostate:" & vbCrLf & _
                        "Opzione/dip. ute/dip. az/dip.doc/utente/azienda/tipodoc" & vbCrLf

              For Each dtrT As DataRow In dttTmp.Rows
                strTmp1 += NTSCStr(dtrT!rp_liv1) & "." & NTSCStr(dtrT!rp_liv2) & "." & NTSCStr(dtrT!rp_liv3) & " / " & _
                          NTSCStr(dtrT!rd_diput) & "" & NTSCStr(dtrT!rd_dipaz) & "" & NTSCStr(dtrT!rd_diptdoc) & "/" & _
                          "'" & NTSCStr(dtrT!rp_idut) & "' / '" & NTSCStr(dtrT!rp_idaz) & "' / '" & NTSCStr(dtrT!rp_idtdoc) & "' / " & _
                          NTSCStr(dtrT!rp_nomprop) & " = '" & NTSCStr(dtrT!rp_valprop) & "'" & vbCrLf
              Next
              dttTmp.Clear()
            End If

            strTmp1 += " ***"

            'accodo al messaggio di errore
            strOut += strTmp1
          Catch
          End Try
        End If    'If bInError = False Then
        bInError = False

        '---------------------------------------------
        'scrivo l'errore nel file di log
        WriteErrorFile(strNomeFileLog, strOut)
      End If    'If bMsgboxOlny = False Then


    Catch ex As Exception
      strOut += strErrore
    Finally
      If bVisMsgBox Then
        If strNomeFileLog.IndexOf("BusNetBatch_") > -1 Then
          'se sono stato avviato in modalità batch non devo visualizzarlo; l'ho già stampato nel file di log
        Else
          If CLN__STD.IsWebApp Or IOS Or ANDROID Or IsBis() Then
            If IsBis() And Not objClasse Is Nothing Then
              'se bVisMsgBox = TRUE sicuramente objClasse è una form!!!!
              'FUNZIONA, MA SE SCATENO L'ECCEZIONE IL MESSAGGIO VERRA' LOGGATO ANCHE NELLA CONSOLLE DI busnet_is.exe !!!
              'eventualmente modificare in: 
              ' - busnet_is.FRM__CHIL.IS_FORM_LOAD 
              ' - busnet_is.FRM__CHIL.IS_FORM_Shown 
              ' - BN__STD.GestError
              ' - BN__CHIL.GestisciEventiEntity
              If objClasse.GetType.ToString.ToLower = "ntsinformatica.ntsgridview" Then
                objClasse.GridControl.findform.omenu.oBusnet_IS.WriteErrorLog(strOut)  'segnalo l'errore nel log di SBS.EXE
                objClasse.GridControl.findform.oApp.MsgBoxErr(strOut)
              Else
                objClasse.oMenu.oBusnet_IS.WriteErrorLog(strOut)  'segnalo l'errore nel log di SBS.EXE
                objClasse.oApp.MsgBoxErr(strOut)
              End If
              'se sccateno l'eccezione a volte si impalla busnet_is per errore non gestito!!!!!
              'Throw New Exception(strOut)
            Else
              Throw New Exception(strOut)
            End If
          End If
          If strOut.IndexOf("CLE__CZOO.OpenChildGest:") > -1 Then
            i = strOut.IndexOf("*") + 1
            l = strOut.IndexOf("*", i + 1)
            strOut = strOut.Substring(i, l - i)
          End If
          If IOS = False And ANDROID = False And IsBis() = False Then GestError_ShowMessage(strOut)
        End If
      End If
      GestError = strOut
    End Try
  End Function
  Public Shared Sub GestError_ShowMessage(ByVal strOut As String)
    Try
      'OCCHIO: la stessa routine sottostante c'è anceh in FRM__CHIL.GestisciEventiEntity (per gli errori generati dall'entity e passati alla UI)
      Dim frmErr As FRM__ERRO
      If strOut.IndexOf(vbCrLf & "More informations: " & vbCrLf) > -1 Then
        frmErr = New FRM__ERRO
        frmErr.edErrore.Text = strOut.Substring(0, strOut.IndexOf(vbCrLf & "More informations: " & vbCrLf))
        frmErr.edDettagli.Text = strOut.Substring(strOut.IndexOf(vbCrLf & "More informations: " & vbCrLf))
        frmErr.ShowDialog()
        frmErr.Dispose()
      ElseIf strOut.IndexOf(vbCrLf & "Error occurred in :") > -1 Then
        frmErr = New FRM__ERRO
        frmErr.edErrore.Text = strOut.Substring(0, strOut.IndexOf(vbCrLf & "Error occurred in :"))
        frmErr.edDettagli.Text = strOut.Substring(strOut.IndexOf(vbCrLf & "Error occurred in :"))
        frmErr.ShowDialog()
        frmErr.Dispose()
      Else
        MessageBox.Show(strOut, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      End If
    Catch ex As Exception
      MessageBox.Show(strOut, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Public Shared Function GetFileVersion(ByVal bCustom As Boolean, ByVal frmChild As Object, ByVal strServerDir As String, ByRef strMsg As String) As Boolean
    'restituisce la versione dei files UI/ENT/DAL del child
    'ritorna true SOLO se ci sono delle personalizzazioni

    Dim strFileUI As String = ""
    Dim strVerUI As String = ""

    Dim strFileENT As String = ""
    Dim strVerENT As String = ""

    Dim strFileDAL As String = ""
    Dim strVerDAL As String = ""

    Dim i As Integer = 0
    Dim strDir As String = ""
    Dim arList As New ArrayList
    Dim strTmp As String = ""

    Dim strNomeFile As String = ""
    Try
      GetFileVersion = False

      strNomeFile = frmChild.GetType.Assembly.GetName.Name
      If frmChild.GetType.Assembly.Location = "" Then
        'è una dll compilata in memoria: prendo la dll padre (può essere solo una dll di tipo BK di NTS Project - vedi Business File)
        strNomeFile = frmChild.GetType.BaseType.Assembly.GetName.Name
      End If

      '------------------
      'UI
      strFileUI = frmChild.GetType.Assembly.Location

      'se è una dll compilata in memoria (può essere solo Business File di NTS Project) prendo la dir della dll standard
      If strFileUI = "" Then strFileUI = frmChild.GetType.BaseType.Assembly.Location

      If System.IO.File.Exists(strFileUI) Then
        strVerUI = System.Diagnostics.FileVersionInfo.GetVersionInfo(strFileUI).FileVersion()
      Else
        strVerUI = ""
      End If

      strMsg = ""
      Try
        strMsg = frmChild.Text
        If strMsg.IndexOf(" <") > -1 Then strMsg = strMsg.Substring(0, strMsg.IndexOf(" <"))
        strMsg = "Programma: '" & strMsg & "' (Form " & frmChild.ToString.Substring(0, frmChild.ToString.IndexOf(",")).Trim & ")" & vbCrLf & _
                 strFileUI & " - Ver. " & strVerUI & vbCrLf
      Catch
      End Try

      '------------------
      'ENT
      strFileENT = strFileUI.Replace(strNomeFile, "BE" & strNomeFile.Substring(2))
      If System.IO.File.Exists(strFileENT) Then
        strVerENT = System.Diagnostics.FileVersionInfo.GetVersionInfo(strFileENT).FileVersion()
      Else
        strFileENT = ""
      End If

      If strFileENT <> "" Then strMsg += strFileENT & " - Ver. " & strVerENT & vbCrLf

      '------------------
      'DAL
      strFileDAL = strFileUI.Replace(strNomeFile, "BD" & strNomeFile.Substring(2))
      If System.IO.File.Exists(strFileDAL) Then
        strVerDAL = System.Diagnostics.FileVersionInfo.GetVersionInfo(strFileDAL).FileVersion()
      Else
        strFileDAL = strFileUI.Replace(strNomeFile, "BD__BASE")
        If System.IO.File.Exists(strFileDAL) Then
          strVerDAL = System.Diagnostics.FileVersionInfo.GetVersionInfo(strFileDAL).FileVersion()
        Else
          strVerDAL = ""
        End If
      End If

      If strVerDAL <> "" Then strMsg += strFileDAL & " - Ver. " & strVerDAL & vbCrLf & vbCrLf

      '-------------------
      'INFORMAZIONI AGGIUNTIVE
      'ottengo la dir
      strDir = strFileUI
      If strDir <> "" Then
        While strDir.Substring(strDir.Length - 1) <> strSep
          strDir = strDir.Substring(0, strDir.Length - 1)
        End While
      Else
        bCustom = True
      End If
      strFileUI = strFileUI.ToUpper.Substring(strDir.Length).Replace(".DLL", "")
      strFileENT = strFileENT.PadLeft(strDir.Length + 5).ToUpper.Substring(strDir.Length).Replace(".DLL", "").Trim
      strFileDAL = strFileDAL.PadLeft(strDir.Length + 5).ToUpper.Substring(strDir.Length).Replace(".DLL", "").Trim

      '-------------------
      'personalizzazioni
      Try
        If Not frmChild.oScript Is Nothing Then
          strMsg += "PERSONALIZZAZIONE: SCRIPT" & vbCrLf
          GetFileVersion = True
          If bCustom Then Exit Function
        End If
      Catch
      End Try

      If strFileUI.Length > 2 Then
        Select Case strFileUI.Substring(0, 2).ToUpper
          Case "BN", "BE", "BD"
            'prg standard
          Case Else
            'prg personalizzato
            strMsg += "PERSONALIZZAZIONE: " & strFileUI & vbCrLf
            GetFileVersion = True
            If bCustom Then Exit Function
        End Select
      End If
      Try
        Dim r1 As New System.IO.StreamReader(strServerDir & "\script\DLLMAP.INI")
        While Not r1.EndOfStream
          strTmp = r1.ReadLine
          If strTmp.Contains("*") Then
            If strTmp.Contains("|") Then
              If strTmp.Split("|")(1).PadRight(50).Substring(2, strFileUI.Length - 2).ToUpper = strFileUI.Substring(2).ToUpper Then
                'entity custom
                strMsg += "PERSONALIZZAZIONE: " & strTmp & vbCrLf
                GetFileVersion = True
                If bCustom Then Exit Function
              End If
            End If
          Else
            'Ignoro i primi 2 caratteri che potrebbero dipende dalla personalizzazione e non essere standard
            If strTmp.PadRight(50).Substring(2, strFileUI.Length - 2).ToUpper = strFileUI.Substring(2).ToUpper Then
              'entity custom
              strMsg += "PERSONALIZZAZIONE: " & strTmp & vbCrLf
              GetFileVersion = True
              If bCustom Then Exit Function
            End If
          End If
        End While
        r1.Close()
      Catch
      End Try

      Try
        If File.Exists(strServerDir & "\script\DLLMAPEX.INI") Then
          Dim r1 As New System.IO.StreamReader(strServerDir & "\script\DLLMAPEX.INI")
          While Not r1.EndOfStream
            strTmp = r1.ReadLine
            'Ignoro i primi 2 caratteri che potrebbero dipende dalla personalizzazione e non essere standard
            If strTmp.PadRight(50).Substring(2, strFileUI.Length - 2).ToUpper = strFileUI.Substring(2).ToUpper Then
              'entity custom
              strMsg += "PERSONALIZZAZIONE: " & strTmp & vbCrLf
              GetFileVersion = True
              If bCustom Then Exit Function
            End If
          End While
          r1.Close()
        End If
      Catch
      End Try

      'se sono stato chiamato solo per sapere se ci sono personalizzazioni, esco (inutile sprecare tempo ...)
      If bCustom Then Exit Function

      strMsg += vbCrLf & "INFORMAZIONI AGGIUNTIVE - Referenziato a: " & vbCrLf
      ''UI ereditato
      'strMsg += "Ereditato da " & vbCrLf & frmChild.GetType.Assembly.GetCallingAssembly.Location
      'strMsg += " Ver. " & System.Diagnostics.FileVersionInfo.GetVersionInfo(frmChild.GetType.Assembly.GetCallingAssembly.Location).FileVersion() & vbCrLf
      GetReference(strDir, frmChild.GetType.Assembly, arList)
      arList.Sort()
      For i = 0 To arList.Count - 1
        strMsg += arList(i).ToString & vbCrLf
      Next

    Catch ex As Exception
      Throw ex
    End Try
  End Function
  Public Shared Function GetReference(ByVal strDir As String, ByRef asm As Assembly, ByRef arOut As ArrayList) As Boolean
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim strFileVersion As String = ""
    Try
      'ottengo tutti i reference, escludendo quelli di sistema e quelli della devexpress
      For i = 0 To asm.GetReferencedAssemblies.Length - 1
        If asm.GetReferencedAssemblies(i).Name.ToLower.IndexOf("devexpress") = -1 And _
           asm.GetReferencedAssemblies(i).Name.ToLower.IndexOf("icsharpcode") = -1 Then
          If System.IO.File.Exists(strDir & asm.GetReferencedAssemblies(i).Name & ".DLL") Then
            Try
              strFileVersion = "In Memory"
              strFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(strDir & asm.GetReferencedAssemblies(i).Name & ".DLL").FileVersion()
            Catch ex As Exception
              'potrebbe essere un assembly compilato in memoria
            End Try
            strTmp = strDir & asm.GetReferencedAssemblies(i).Name & ".DLL" & _
                             " Ver. " & strFileVersion
            If arOut.Contains(strTmp) = False Then
              arOut.Add(strTmp)
              'ricorsivo: sfoglio i reference del reference
              Dim asm1 As Assembly = Assembly.LoadFrom(strDir & asm.GetReferencedAssemblies(i).Name & ".DLL")
              If Not asm1 Is Nothing Then GetReference(strDir, asm1, arOut)
            End If
          End If
        End If
      Next

      Return True

    Catch ex As Exception
      Throw ex
    End Try
  End Function


  Private Shared Sub WriteErrorFile(ByVal strNomeFileLog As String, ByVal strError As String)
    Dim fErr As StreamWriter = Nothing
    Dim fiErr As FileInfo = Nothing
    Try
      'ho il messaggio di errore: lo scrivo nel file
      'se il file Ã¨ troppo grande lo svuoto
      'If strNomeFileLog = "" Then strNomeFileLog = "C:\BusNet_error.log"
      If strNomeFileLog <> "" Then
        fiErr = New FileInfo(strNomeFileLog)
        If fiErr.Exists Then
          If fiErr.Length > 500000 Then fiErr.Delete()
        End If

        fErr = New StreamWriter(strNomeFileLog, True)
        fErr.Write(DateTime.Now.ToString & vbCrLf & strError & vbCrLf & vbCrLf & _
                  "******************************************************************************" & _
                  vbCrLf & vbCrLf)
        fErr.Flush()
        fErr.Close()
      End If
    Catch ex As Exception

    End Try
  End Sub


  Public Shared Function UserIsAdmin(ByVal strUserGroup As String) As Boolean
    If strUserGroup.Trim = "" Then Return False
    'accomunato comportamento a business VB6: solo gli operatori di gruppo 0 sono amministratori
    If strUserGroup.ToUpper <> "AMMINISTRATORI" Then
      Return False
    Else
      Return True
    End If
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ArrDbl(ByVal dValue As Decimal, ByVal nDec As Integer) As Decimal
    If ArrDblTipo = "-1" Then
      'Modo opzionale (arrotondamento matematico simmetrico)
      'cioè i negativi sono trattati come i positivi
      'ovvero 0.9 * -123.95 = -111.555 -> -111.56
      '       0.9 *  123.95 =  111.555 ->  111.56 
      Return Math.Round(CType(dValue, Double), nDec, MidpointRounding.AwayFromZero)
    Else
      'Modo normale (arrotondamento matematico asimmetrico)
      'ovvero 0.9 * -123.95 = -111.555 -> -111.55
      '       0.9 *  123.95 =  111.555 ->  111.56  
      Return CDec(Int(CDec(dValue * (10 ^ nDec) + 0.5)) / (10 ^ nDec))
    End If
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function ArrDblEcc(ByVal dValore As Decimal, Optional ByVal nDec As Integer = 0) As Decimal
    Dim lValore As Decimal
    lValore = dValore
    lValore = CDec(lValore * 10 ^ nDec)
    lValore = Int(lValore) + Math.Sign(Math.Abs(lValore - Int(lValore)))
    Return CDec(lValore / 10 ^ nDec)
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function FineMese(ByVal strDatprec As String) As String
    Dim strDc As String
    strDc = strDatprec
    Do While True
      strDatprec = DateAdd("d", 1, CDate(strDatprec)).ToShortDateString
      If DatePart("m", strDatprec) <> DatePart("m", strDc) Then
        strDatprec = DateAdd("d", -1, CDate(strDatprec)).ToShortDateString
        Exit Do
      End If
    Loop
    Return strDatprec
  End Function
  <DebuggerStepThrough()> _
  Public Shared Function EstFineMese(ByVal strDatprec As String) As Boolean
    Dim dtDatPrec As Date
    Dim dtTmp As Date
    dtDatPrec = CDate(strDatprec)
    dtTmp = DateAdd("d", 1, dtDatPrec)
    If DatePart("m", dtDatPrec) = DatePart("m", dtTmp) Then
      Return False
    Else
      Return True
    End If
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function CopiaDatarow(ByVal dsIn As DataSet, ByVal strTableName As String, ByVal nRow As Integer, ByRef dtrOut As DataRow) As Boolean
    'dato un datatable ed una riga, restituisce una copia dela riga indicata non collegata al datatable
    Try
      Dim dsTmp As DataSet
      dsTmp = dsIn.Clone()
      dsTmp.Tables(strTableName).ImportRow(dsIn.Tables(strTableName).Rows(nRow))
      dtrOut = dsTmp.Tables(strTableName).Rows(0)
      Return True

    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error during copy of Datarow" & vbCrLf & "Error: " & ex.Message)
      Else
        MsgBox("Error during copy of Datarow" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
      Return False
    End Try
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function ValoriUguali(ByVal strNew As String, ByVal strOld As String) As Boolean
    'verifico se i due valori inseriti sono uguali, sia come stringa che come numero
    'per ora non testo che siano uguali come campi data (per questioni di prestazioni)
    'utilizzato principalmente nella BeforeColUpdate
    Dim dNew, dOld As Decimal
    Dim nPunto As Integer = 0
    Dim nPuntoOld As Integer = 0
    Try
      If strNew = strOld Then
        Return True
      ElseIf Decimal.TryParse(strNew, dNew) And Decimal.TryParse(strOld, dOld) Then
        If strNew.Length < 26 And strOld.Length < 26 Then
          nPunto = strNew.Replace(",", ".").IndexOf(".")
          nPuntoOld = strOld.Replace(",", ".").IndexOf(".")
          If (strNew.Substring(0, 1) = "0" Or strOld.Substring(0, 1) = "0") And nPunto = -1 Then
            'ho degli zeri iniziali e non c'è la virgola: probabilmente è una partita IVA
          Else
            If dNew = dOld Then
              If nPunto <> nPuntoOld Then
                'potrebbe essere un campo stringa che contiene solo un numero che finisce per '.'
              Else
                Return True
              End If
            End If
          End If
        End If
      End If
      Return False
    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error occuring in 'CLN__STD.ValoreCambiato'" & vbCrLf & "Error: " & ex.Message)
      Else
        MsgBox("Error occuring in 'CLN__STD.ValoreCambiato'" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
      Return False
    End Try
  End Function

  Public Shared Function CalcolaMD5(ByVal strTesto As String) As String
    Try
      Dim Ue As New UnicodeEncoding()
      Dim ByteSourceText() As Byte = Ue.GetBytes(strTesto)
      Dim Md5 As New MD5CryptoServiceProvider()
      Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
      Return Convert.ToBase64String(ByteHash)
    Catch ex As Exception
      MsgBox("Error occuring in 'CLN__STD.CalcolaMD5'" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
    End Try
    Return ""
  End Function


  Public Shared Sub WriteMsgBoxToLog(ByVal strMsg As String)
    'se sono stato avviato in modalità batch (tipo busbatch) i messaggi vengono a finire tutti qui (eccetto quelli du busnet.exe)
    Dim lw1 As New StreamWriter(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log", True)
    lw1.WriteLine(DateTime.Now.ToString & " " & strMsg)
    lw1.Flush()
    lw1.Close()
  End Sub

  Public Shared Function TraduciTipoNumerazione(ByVal strIn As String) As String
    TraduciTipoNumerazione = strIn
    Select Case strIn
      Case "!" : Return "Offerte (CRM)"
      Case "#" : Return "Impegni su commessa"
      Case "$" : Return "Ordini Forn.aperti"
      Case "A" : Return "Fatture emesse"
      Case "AA" : Return "Numerazioni articolo"
      Case "AD" : Return "Aree di download"
      Case "AF" : Return "File delle aree di download"
      Case "AL" : Return "AL"
      Case "AP" : Return "Ultima anagrafica percipiente"
      Case "AV" : Return "AV"
      Case "B" : Return "D.D.T. emessi"
      Case "BC" : Return "N.progressivi barcode"
      Case "C" : Return "Corrispettivi emessi"
      Case "C1" : Return "Assist. Problemi/soluzioni"
      Case "C2" : Return "Assist. Elenco ricambi"
      Case "C3" : Return "Assist. Chiamate"
      Case "C4" : Return "Assist. Attività da svolgere"
      Case "C5" : Return "Assist. Attività eseguite"
      Case "C6" : Return "Assist. Contratti di assistenza"
      Case "C7" : Return "CRM Attività"
      Case "C8" : Return "CRM Leads"
      Case "C9" : Return "CRM Opportunità"
      Case "CC" : Return "Clienti"
      Case "CE" : Return "N.progressivi cespiti"
      Case "CL" : Return "Collaudi"
      Case "CO" : Return "N.progressivi commesse C.A."
      Case "CP" : Return "Configurat.per famiglia"
      Case "CP2" : Return "N.configuratore di prodotto"
      Case "CQ" : Return "Configurat.per articolo"
      Case "CQ2" : Return "N.contratti quadro"
      Case "DA" : Return "Distinte"
      Case "DT" : Return "N.documenti associati ai task"
      Case "EA" : Return "Scadenze"
      Case "EL" : Return "N.ultimo elenco intracomunitario presentato"
      Case "F" : Return "Ricevute fiscali"
      Case "F4" : Return "Progressivo F24"
      Case "FF" : Return "Fornitori"
      Case "FL" : Return "N.flussi finanziari"
      Case "FT" : Return "FT"
      Case "G" : Return "Avanz.produzione"
      Case "GP" : Return "Assist. Prepagati e monte-ore"
      Case "H" : Return "Ordini produzione"
      Case "IC" : Return "N.progressivi transazioni intracomunitarie"
      Case "IM" : Return "Progressivo interessi di mora"
      Case "IN" : Return "IN"
      Case "IP" : Return "IP"
      Case "IV" : Return "IV"
      Case "L" : Return "Fatture ricevute"
      Case "LO" : Return "N.progressivi lotti"
      Case "M" : Return "D.D.T. ricevuti"
      Case "MA" : Return "Numerazione attività"
      Case "MF" : Return "N.movimenti finanziari previsti"
      Case "MT" : Return "N.matricole"
      Case "MU" : Return "MU"
      Case "NC" : Return "Rapp.non conformità"
      Case "NS" : Return "NS"
      Case "NT" : Return "NT"
      Case "O" : Return "Ordini fornitori"
      Case "OG" : Return "N.progressivo organigramma"
      Case "OL" : Return "N.progressivi proposte d'ordine"
      Case "OO" : Return "N.progressivi oggetti ole"
      Case "OP" : Return "N.progressivi liste di produzione"
      Case "P" : Return "Protocolli acq/vend"
      Case "P1" : Return "Fatture (parcellazione)"
      Case "P2" : Return "Avv.parc./notula (par.)"
      Case "P3" : Return "Corrispettivi (parc.)"
      Case "P9" : Return "P9"
      Case "PA" : Return "N.progressivi movimenti di C.A."
      Case "PD" : Return "Prot. docum. PM"
      Case "PE" : Return "PE"
      Case "PK" : Return "Lista Picking"
      Case "PR" : Return "Comm. Sempl. Preventivi Commessa semplice"
      Case "PX" : Return "N.progressivi movimenti di NUOVA C.A."
      Case "Q" : Return "Preventivi"
      Case "R" : Return "Impegni clienti"
      Case "RO" : Return "Numer. RDA/RDO"
      Case "RT" : Return "N.progressivi ritenute"
      Case "S" : Return "Fatture ricev.fiscali"
      Case "SA" : Return "SA"
      Case "SG" : Return "SG"
      Case "SM" : Return "Numerazioni RSM"
      Case "SP" : Return "N.schemi di commessa"
      Case "SS" : Return "Sottoconti"
      Case "T" : Return "Carico da produzione"
      Case "TP" : Return "Numerazione attività da svolgere"
      Case "TS" : Return "Timesheet (manodopera)"
      Case "U" : Return "Scarico a produzione"
      Case "UB" : Return "UB"
      Case "UC" : Return "Unità di carico"
      Case "V" : Return "Packing List"
      Case "V1" : Return "Codice pacco"
      Case "VV" : Return "Impegni Clienti aperti"
      Case "W" : Return "Note di prelievo"
      Case "X" : Return "Imp.di trasferimento"
      Case "Y" : Return "Impegni produzione"
      Case "Z" : Return "Bolle di mov. interna"
    End Select
  End Function


#Region "Trace: gestione prestazioni"
  Public Shared Sub TraceStart()
    'Pulisco le vecchie variabili temporanee
    arDate.Clear()
    arDescr.Clear()

    'Mi salvo la data attuale per fare dopo i confronti
    arDescr.Add("Start")
    arDate.Add(Now)
  End Sub

  Public Shared Sub TraceNow(Optional ByVal strDescr As String = "")
    'Memorizzo l'intermezzo con la sua descrizione
    arDescr.Add(strDescr)
    arDate.Add(Now)
  End Sub

  Public Shared Sub TraceStop(Optional ByVal strDescr As String = "", Optional ByVal bRicreaFile As Boolean = True, Optional ByVal bNonAvvisare As Boolean = False)
    Try
      'Chiudo l'ultimo tempo
      arDate.Add(Now)
      arDescr.Add("Stop")

      'Una veloce separazione
      Dim strBuilder As New StringBuilder
      strBuilder.AppendLine("----------------------" & IIf(strDescr <> "", " " & strDescr & " ", "").ToString & "----------------------")
      strBuilder.AppendLine("|- Descrizione -----------------|--- Totale ---|-- Parziale --|")
      strBuilder.AppendLine("|-------------------------------|--------------|--------------|")

      Dim strTmp As String

      ' La prima riga non la conto (è quella di partenza e avrebbe tempi a 0)
      For z As Integer = 1 To arDate.Count - 1
        'Aggiorno la descrizione
        strTmp = NTSCStr(arDescr(z))
        If strTmp.Trim = "" Then strTmp = z.ToString()

        strBuilder.Append("|- " & (strTmp & " ").PadRight(28, "-"c).Substring(0, 28) & "-|")

        'Tempi totali
        CalcolaDifferenzeTempi(NTSCDate(arDate(0)), NTSCDate(arDate(z)), strTmp)

        strBuilder.Append(strTmp.PadLeft(14, " "c) & "|")

        'Tempi parziali
        CalcolaDifferenzeTempi(NTSCDate(arDate(z - 1)), NTSCDate(arDate(z)), strTmp)

        strBuilder.AppendLine(strTmp.PadLeft(14, " "c) & "|")
      Next
      strBuilder.AppendLine("|-------------------------------|--------------|--------------|")

      'per evitare perdite di tempo le variabili le dichiaro dopo aver rilevato l'ultimo tempo
      'genero il file con i tempi
      Dim swFile As New StreamWriter(Dir & "\Trace.txt", Not bRicreaFile)
      swFile.WriteLine(strBuilder.ToString)

      'Lascio un pò di spazi se verrà aggiunto al log un altro tracciato dei tempi
      swFile.Write(vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf)

      swFile.Flush()
      swFile.Close()

      'Riporto il risultato anche nella finestra di output di visual studio
      Console.WriteLine(strBuilder.ToString)

      If Not bNonAvvisare Then MsgBox("Generato il file di monitoraggio prestazioni '" & Dir & "\Trace.txt")
    Catch ex As Exception
      'ignoro l'errore: se c'è lo uac attivo potrei non poter scrivere il file in quella dir...
    End Try
  End Sub

  Public Shared Sub CalcolaDifferenzeTempi(ByVal dtStart As Date, ByVal dtEnd As Date, ByRef strDiff As String)
    Dim timeDiff As TimeSpan

    'Calcolo la differenza con la prima riga
    timeDiff = dtEnd.Subtract(dtStart)
    'Compongo il tempo come ss.mmm (secondi.millisecondi)
    strDiff = timeDiff.Seconds.ToString("00") & "." & timeDiff.Milliseconds.ToString("000")
    If timeDiff.Minutes <> 0 Then strDiff = timeDiff.Minutes.ToString("00") & "." & strDiff 'Se ci sono aggiungo i minuti (mm.ss.mmm)
    If timeDiff.Hours <> 0 Then strDiff = timeDiff.Hours.ToString("00") & ":" & strDiff 'Se ci sono aggiungo le ore (hh:mm.ss.mmm)
  End Sub
#End Region


  Public Shared Function GetNumeroModuliAbilitati(ByVal strModuli As String) As String
    Dim strOut As String = ""
    For i As Integer = 0 To strModuli.Length - 1
      If strModuli(i) = "S" Then strOut += (i + 1).ToString + ", "
    Next
    If strOut.Length > 0 Then strOut = strOut.Substring(0, strOut.Length - 2)

    Return strOut
  End Function

  Public Shared Function GetStrBit(ByVal lModuli As Decimal, ByVal lModuliExt As Decimal) As String
    '-----------------------------------
    'ritorna una stringa di NNNSSS.... in base ai moduli acquistati 
    '(primi 30 caratteri moduli, poi 30 caratteri moduliext)
    Dim strOut As String = ""
    Dim strOutExt As String = ""
    Dim dTemp As Decimal

    While Not CLng(lModuli) < 2
      dTemp = lModuli / 2
      lModuli = Int(lModuli / 2)
      If lModuli <> dTemp Then strOut = strOut & "S" Else strOut = strOut & "N"
    End While
    If CLng(lModuli) = 1 Then strOut = strOut & "S" Else strOut = strOut & "N"
    strOut = Left(strOut.PadRight(30, CType("N", Char)), 30)

    While Not CLng(lModuliExt) < 2
      dTemp = lModuliExt / 2
      lModuliExt = Int(lModuliExt / 2)
      If lModuliExt <> dTemp Then strOutExt = strOutExt & "S" Else strOutExt = strOutExt & "N"
    End While
    If CLng(lModuliExt) = 1 Then strOutExt = strOutExt & "S" Else strOutExt = strOutExt & "N"
    strOutExt = Left(strOutExt.PadRight(30, CType("N", Char)), 30)

    Return strOut & strOutExt
  End Function

  <DebuggerStepThrough()> _
  Public Shared Function CheckCellaShort(ByRef oColumn As DataColumnChangeEventArgs, ByRef strErr As String) As Boolean
    strErr = ""
    If oColumn.Column.DataType.ToString = "System.Int16" Then
      If oColumn.ProposedValue.Equals(DBNull.Value) Then oColumn.ProposedValue = 0
      'If Not IsNumeric(oColumn.ProposedValue) Then
      '  strErr = "Nel campo |" & oColumn.Column.ColumnName & "| è possibile inserire solo numeri compresi tra |-32768| e |32768| (attuale valore: |" & oColumn.ProposedValue.ToString & "|)"
      '  oColumn.ProposedValue = oColumn.Row(oColumn.Column.ColumnName)
      '  Return False
      'End If
      'If CDec(oColumn.ProposedValue) < -32768 Or CDec(oColumn.ProposedValue) > 32768 Then
      '  strErr = "Nel campo |" & oColumn.Column.ColumnName & "| è possibile inserire solo numeri compresi tra |-32768| e |32768| (attuale valore: |" & oColumn.ProposedValue.ToString & "|)"
      '  oColumn.ProposedValue = oColumn.Row(oColumn.Column.ColumnName)
      '  Return False
      'End If
    End If
    Return True
  End Function

  Public Shared Sub AddDirSep(ByRef strPathName As String)
    Try
      If Right(Trim(strPathName), Len("/")) <> "/" And _
         Right(Trim(strPathName), Len(strSep)) <> strSep Then
        strPathName = RTrim$(strPathName) & strSep
      End If
    Catch ex As Exception
      If File.Exists(Dir & "\BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log") Then
        'se sono stato avviato in modalità batch ...
        WriteMsgBoxToLog("Error in AddDirSep" & vbCrLf & "Error: " & ex.Message)
      Else
        MsgBox("Error in AddDirSep" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
      End If
    End Try
  End Sub

#Region "costanti per settare gli NTSSetParam e NTSSetParamZoom"
  Public Const tabartico As Integer = 1
  Public Const tabartfasi As Integer = 2
  Public Const tabanagra As Integer = 3
  Public Const tabanagrac As Integer = 4
  Public Const tabanagraf As Integer = 5
  Public Const tabanagras As Integer = 6
  Public Const tabanagen As Integer = 7
  Public Const tabdestdiv As Integer = 8
  Public Const tabanagpc As Integer = 9
  Public Const tabcommess As Integer = 10
  Public Const tabsubcomm As Integer = 11
  Public Const tabanagca As Integer = 12
  Public Const tabtask As Integer = 13
  Public Const tableads As Integer = 14
  Public Const tabanazul As Integer = 15

  Public Const tababi As Integer = 48
  Public Const tababicab As Integer = 49

  Public Const tabacmi As Integer = 50
  Public Const tabaext As Integer = 51
  Public Const tabaggi As Integer = 52
  Public Const taballa As Integer = 53
  Public Const taballb As Integer = 54
  Public Const tabammi As Integer = 55
  Public Const tabanaz As Integer = 56
  Public Const tabappr As Integer = 57
  Public Const tabarea As Integer = 58
  Public Const tabaspe As Integer = 59
  Public Const tabatec As Integer = 60
  Public Const tabatti As Integer = 61
  Public Const tabbanc As Integer = 62
  Public Const tabbilm As Integer = 63
  Public Const tabbotr As Integer = 64
  Public Const tabcaaf As Integer = 65
  Public Const tabcacc As Integer = 66
  Public Const tabcacd As Integer = 67
  Public Const tabcace As Integer = 68
  Public Const tabcage As Integer = 69
  Public Const tabcalb As Integer = 70
  Public Const tabcama As Integer = 71
  Public Const tabcamc As Integer = 72
  Public Const tabcamf As Integer = 73
  Public Const tabcamp As Integer = 74
  Public Const tabcana As Integer = 75
  Public Const tabcapa As Integer = 76
  Public Const tabcapi As Integer = 77
  Public Const tabcapl As Integer = 78
  Public Const tabcart As Integer = 79
  Public Const tabcatc As Integer = 80
  Public Const tabcate As Integer = 81
  Public Const tabcatf As Integer = 82
  Public Const tabcatp As Integer = 83
  Public Const tabcatq As Integer = 84
  Public Const tabcatt As Integer = 85
  Public Const tabcauc As Integer = 86
  Public Const tabcaum As Integer = 87
  Public Const tabcaus As Integer = 88
  Public Const tabcbol As Integer = 89
  Public Const tabccag As Integer = 90
  Public Const tabccam As Integer = 91
  Public Const tabccat As Integer = 92
  Public Const tabccip As Integer = 93
  Public Const tabccrd As Integer = 94
  Public Const tabcdoc As Integer = 95
  Public Const tabcdur As Integer = 96
  Public Const tabcena As Integer = 97
  Public Const tabcent As Integer = 98
  Public Const tabcfam As Integer = 99
  Public Const tabcfat As Integer = 100
  Public Const tabcfga As Integer = 101
  Public Const tabcfgo As Integer = 102
  Public Const tabcfgs As Integer = 103
  Public Const tabcfoa As Integer = 104
  Public Const tabcgpa As Integer = 105
  Public Const tabcgrp As Integer = 106
  Public Const tabchia As Integer = 107
  Public Const tabchip As Integer = 108
  Public Const tabcint As Integer = 109
  Public Const tabciva As Integer = 110
  Public Const tabclaq As Integer = 111
  Public Const tabclas As Integer = 112
  Public Const tabclca As Integer = 113
  Public Const tabcldn As Integer = 114
  Public Const tabclic As Integer = 115
  Public Const tabcliq As Integer = 116
  Public Const tabcmdp As Integer = 117
  Public Const tabcmer As Integer = 118
  Public Const tabcoce As Integer = 119
  Public Const tabcoga As Integer = 120
  Public Const tabcolo As Integer = 121
  Public Const tabconc As Integer = 122
  Public Const tabconf As Integer = 123
  Public Const tabcont As Integer = 124
  Public Const tabcope As Integer = 125
  Public Const tabcorg As Integer = 126
  Public Const tabcove As Integer = 127
  Public Const tabcovg As Integer = 128
  Public Const tabcovp As Integer = 129
  Public Const tabcpag As Integer = 130
  Public Const tabcpar As Integer = 131
  Public Const tabcpcl As Integer = 132
  Public Const tabcptr As Integer = 133
  Public Const tabcral As Integer = 134
  Public Const tabcred As Integer = 135
  Public Const tabcrgp As Integer = 136
  Public Const tabcsar As Integer = 137
  Public Const tabcscl As Integer = 138
  Public Const tabcset As Integer = 139
  Public Const tabcsub As Integer = 140
  Public Const tabctaf As Integer = 141
  Public Const tabctas As Integer = 142
  Public Const tabctaz As Integer = 143
  Public Const tabctct As Integer = 144
  Public Const tabctpt As Integer = 145
  Public Const tabctqp As Integer = 146
  Public Const tabctqs As Integer = 147
  Public Const tabctri As Integer = 148
  Public Const tabctru As Integer = 149
  Public Const tabctsf As Integer = 150
  Public Const tabctul As Integer = 151
  Public Const tabcurg As Integer = 152
  Public Const tabcval As Integer = 153
  Public Const tabcvuo As Integer = 154
  Public Const tabdcag As Integer = 155
  Public Const tabdccl As Integer = 156
  Public Const tabdcge As Integer = 157
  Public Const tabdfor As Integer = 158
  Public Const tabdich As Integer = 159
  Public Const tabdimd As Integer = 160
  Public Const tabdira As Integer = 161
  Public Const tabdire As Integer = 162
  Public Const tabdpro As Integer = 163
  Public Const tabdsed As Integer = 164
  Public Const tabdtge As Integer = 165
  Public Const tabduri As Integer = 166
  Public Const tabdwim As Integer = 167
  Public Const tabenas As Integer = 168
  Public Const tabenpa As Integer = 169
  Public Const tabesco As Integer = 170
  Public Const tabespr As Integer = 171
  Public Const tabform As Integer = 172
  Public Const tabgmer As Integer = 173
  Public Const tabgrce As Integer = 174
  Public Const tabgrua As Integer = 175
  Public Const tabgruc As Integer = 176
  Public Const tabimba As Integer = 177
  Public Const tabimmd As Integer = 178
  Public Const tabimps As Integer = 179
  Public Const tabinai As Integer = 180
  Public Const tabinps As Integer = 181
  Public Const tabinsg As Integer = 182
  Public Const tabinst As Integer = 183
  Public Const tabiraa As Integer = 184
  Public Const tabirac As Integer = 185
  Public Const tabirar As Integer = 186
  Public Const tabirpe As Integer = 187
  Public Const tabista As Integer = 188
  Public Const tablava As Integer = 189
  Public Const tablavo As Integer = 190
  Public Const tabling As Integer = 191
  Public Const tablipe As Integer = 192
  Public Const tablist As Integer = 193
  Public Const tablivq As Integer = 194
  Public Const tablmup As Integer = 195
  Public Const tablpro As Integer = 196
  Public Const tablsar As Integer = 197
  Public Const tablsel As Integer = 198
  Public Const tabmaca As Integer = 199
  Public Const tabmaga As Integer = 200
  Public Const tabmarc As Integer = 201
  Public Const tabmast As Integer = 202
  Public Const tabmesg As Integer = 203
  Public Const tabmfpr As Integer = 204
  Public Const tabmpor As Integer = 205
  Public Const tabmsos As Integer = 206
  Public Const tabngiu As Integer = 207
  Public Const tabnntc As Integer = 208
  Public Const tabnono As Integer = 209
  Public Const tabnote As Integer = 210
  Public Const tabntra As Integer = 211
  Public Const tabnuma As Integer = 212
  Public Const tabnumg As Integer = 213
  Public Const tabopes As Integer = 214
  Public Const tabpaga As Integer = 215
  Public Const tabpcam As Integer = 216
  Public Const tabpcon As Integer = 217
  Public Const tabpdon As Integer = 218
  Public Const tabpeac As Integer = 219
  Public Const tabpece As Integer = 220
  Public Const tabpecg As Integer = 221
  Public Const tabpepa As Integer = 222
  Public Const tabperv As Integer = 223
  Public Const tabpeve As Integer = 224
  Public Const tabport As Integer = 225
  Public Const tabpqua As Integer = 226
  Public Const tabprcr As Integer = 227
  Public Const tabprea As Integer = 228
  Public Const tabpres As Integer = 229
  Public Const tabprev As Integer = 230
  Public Const tabpriv As Integer = 231
  Public Const tabprog As Integer = 232
  Public Const tabprov As Integer = 233
  Public Const tabprve As Integer = 234
  Public Const tabpuce As Integer = 235
  Public Const tabqass As Integer = 236
  Public Const tabragg As Integer = 237
  Public Const tabrapp As Integer = 238
  Public Const tabrdim As Integer = 239
  Public Const tabregb As Integer = 240
  Public Const tabregi As Integer = 241
  Public Const tabrepa As Integer = 242
  Public Const tabrica As Integer = 243
  Public Const tabriel As Integer = 244
  Public Const tabrivi As Integer = 245
  Public Const tabrpeg As Integer = 246
  Public Const tabrsta As Integer = 247
  Public Const tabrtac As Integer = 248
  Public Const tabruaz As Integer = 249
  Public Const tabsatc As Integer = 250
  Public Const tabscpr As Integer = 251
  Public Const tabseta As Integer = 252
  Public Const tabsett As Integer = 253
  Public Const tabsgme As Integer = 254
  Public Const tabsiso As Integer = 255
  Public Const tabsmel As Integer = 256
  Public Const tabsmer As Integer = 257
  Public Const tabsoca As Integer = 258
  Public Const tabspce As Integer = 259
  Public Const tabspti As Integer = 260
  Public Const tabsstu As Integer = 261
  Public Const tabstac As Integer = 262
  Public Const tabstag As Integer = 263
  Public Const tabstat As Integer = 264
  Public Const tabstes As Integer = 265
  Public Const tabstso As Integer = 266
  Public Const tabstud As Integer = 267
  Public Const tabtaco As Integer = 268
  Public Const tabtacq As Integer = 269
  Public Const tabtagl As Integer = 270
  Public Const tabtcom As Integer = 271
  Public Const tabtcon As Integer = 272
  Public Const tabtdif As Integer = 273
  Public Const tabtest As Integer = 274
  Public Const tabtimp As Integer = 275
  Public Const tabtipa As Integer = 276
  Public Const tabtitp As Integer = 277
  Public Const tabtits As Integer = 278
  Public Const tabtopc As Integer = 279
  Public Const tabtopp As Integer = 280
  Public Const tabtops As Integer = 281
  Public Const tabtpan As Integer = 282
  Public Const tabtpbf As Integer = 283
  Public Const tabtpce As Integer = 284
  Public Const tabtpco As Integer = 285
  Public Const tabtpes As Integer = 286
  Public Const tabtpmo As Integer = 287
  Public Const tabtpro As Integer = 288
  Public Const tabtpse As Integer = 289
  Public Const tabtpsq As Integer = 290
  Public Const tabtpst As Integer = 291
  Public Const tabtrib As Integer = 292
  Public Const tabtric As Integer = 293
  Public Const tabtsca As Integer = 294
  Public Const tabttas As Integer = 295
  Public Const tabultc As Integer = 296
  Public Const tabumis As Integer = 297
  Public Const tabunic As Integer = 298
  Public Const tabusat As Integer = 299
  Public Const tabvalf As Integer = 300
  Public Const tabvalu As Integer = 301
  Public Const tabvarc As Integer = 302
  Public Const tabvari As Integer = 303
  Public Const tabvett As Integer = 304
  Public Const tabvpar As Integer = 305
  Public Const tabzone As Integer = 306
  Public Const tabcomuni As Integer = 307
  Public Const tabqpro As Integer = 308
  Public Const tabanacesp As Integer = 309
  Public Const tabd7percip As Integer = 310
  Public Const tabdistbas As Integer = 311
  Public Const tabopportun As Integer = 312
  Public Const tabalesets As Integer = 313
  Public Const tabproc As Integer = 314
  Public Const tabstab As Integer = 315
  Public Const tabgrlo As Integer = 316
  Public Const tabcract As Integer = 317
  Public Const tabchiam As Integer = 318
  Public Const profil As Integer = 319
  Public Const schtbl As Integer = 320
  Public Const proced As Integer = 321
  Public Const tabelle As Integer = 322
  Public Const tabpcca As Integer = 323
  Public Const tabgru2 As Integer = 324
  Public Const tabclc2 As Integer = 325
  Public Const tabmac2 As Integer = 326
  Public Const tabanagca2 As Integer = 327
  Public Const tabdica As Integer = 328
  Public Const tabtcdc As Integer = 329
  Public Const tabdriv As Integer = 330
  Public Const tabcaca As Integer = 331
  Public Const tabvoca As Integer = 332
  Public Const tabscbg As Integer = 333
  Public Const tabescg As Integer = 334
  Public Const tabdivi As Integer = 335
  Public Const tabdicv As Integer = 336
  Public Const tabscen As Integer = 337
  Public Const tabtcex As Integer = 338
  Public Const tabdime As Integer = 339
  Public Const tabrere As Integer = 340
  Public Const tabreec As Integer = 341
  Public Const tabrepr As Integer = 342
  Public Const tabrecp As Integer = 343
  Public Const tabrett As Integer = 344
  Public Const tabtsla As Integer = 345
  Public Const tabtchi As Integer = 346
  Public Const tabstpg As Integer = 347
  Public Const tabnaut As Integer = 348
  Public Const tabprob As Integer = 349
  Public Const taberog As Integer = 350
  Public Const tabmodi As Integer = 351
  Public Const tabconfproc As Integer = 352
  Public Const tabvfgr As Integer = 353
  Public Const tabvfde As Integer = 354
  Public Const tabretailartico As Integer = 355
  Public Const tabretailanagra As Integer = 356
  Public Const tablotx As Integer = 357
  Public Const tabrepc As Integer = 357
  Public Const tabsezd As Integer = 358
  Public Const tabcalop As Integer = 359
  Public Const tabseat As Integer = 360
  Public Const tabstco As Integer = 361
  Public Const tabcontatti As Integer = 362
  Public Const tabmodrich As Integer = 363
  Public Const tabcpos As Integer = 364
#End Region

  Public Shared Sub NTSSetParam(ByVal nTabe As Integer, ByRef nMaxLen As Integer, _
                                ByRef strFormatNum As String, ByRef dMinNum As Decimal, ByRef dMaxNum As Decimal, _
                                ByRef strPrgZoom As String)
    Try
      Select Case nTabe
        Case tabartico
          nMaxLen = CodartMaxLen
          strPrgZoom = "ZOOMARTICO"
          Exit Select
        Case tabartfasi
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMARTFASI"
          Exit Select
        Case tabanagra
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGRA"
          Exit Select
        Case tabanagrac
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGRAC"
          Exit Select
        Case tabanagraf
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGRAF"
          Exit Select
        Case tabanagras
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGRAS"
          Exit Select
        Case tabanagen
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGEN"
          Exit Select
        Case tabdestdiv
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMDESTDIV"
          Exit Select
        Case tabanazul
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMDESTDIV"
          Exit Select
        Case tabanagpc
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGPC"
          Exit Select
        Case tabcommess
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMCOMMESS"
          Exit Select
        Case tabsubcomm
          nMaxLen = 2
          strPrgZoom = "ZOOMSUBCOMM"
          Exit Select
        Case tabanagca
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGCA"
          Exit Select
        Case tabtask
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTASK"
          Exit Select
        Case tableads
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMLEADS"
          Exit Select
        Case tababi
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMABI"
          Exit Select
        Case tababicab
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMABICAB"
          Exit Select
        Case tabacmi
          nMaxLen = 4
          strPrgZoom = "ZOOMTABACMI"
          Exit Select
        Case tabaext
          nMaxLen = 1
          strPrgZoom = "ZOOMTABAEXT"
          Exit Select
        Case tabaggi
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case taballa
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case taballb
          'genesys 
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabammi
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabanaz
          nMaxLen = 12
          strPrgZoom = "ZOOMTABANAZ"
          Exit Select
        Case tabappr
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABAPPR"
          Exit Select
        Case tabarea
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABAREA"
          Exit Select
        Case tabaspe
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABASPE"
          Exit Select
        Case tabatec
          nMaxLen = 6
          strPrgZoom = "ZOOMTABATEC"
          Exit Select
        Case tabatti
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = ""
          Exit Select
        Case tabbanc
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABBANC"
          Exit Select
        Case tabbilm
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABBILM"
          Exit Select
        Case tabbotr
          strFormatNum = "0"
          nMaxLen = 1
          dMinNum = 0
          dMaxNum = 1
          strPrgZoom = "ZOOMTABBOTR"
          Exit Select
        Case tabcaaf
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcacc
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCACC"
          Exit Select
        Case tabcacd
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = ""
          Exit Select
        Case tabcace
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCACE"
          Exit Select
        Case tabcage
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCAGE"
          Exit Select
        Case tabcalb
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcama
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcamc
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCAMC"
          Exit Select
        Case tabcamf
          nMaxLen = 6
          strPrgZoom = ""
          Exit Select
        Case tabcamp
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCAMP"
          Exit Select
        Case tabcana
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCANA"
          Exit Select
        Case tabcapa
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcapi
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcapl
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcart
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcatc
          'non più usata
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcate
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCATE"
          Exit Select
        Case tabcatf
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcatp
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcatq
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCATQ"
          Exit Select
        Case tabcatt
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcauc
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCAUC"
          Exit Select
        Case tabcaum
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCAUM"
          Exit Select
        Case tabcaus
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCAUS"
          Exit Select
        Case tabcbol
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabccag
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabccam
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabccat
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabccip
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabccrd
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcdoc
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcdur
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcena
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCENA"
          Exit Select
        Case tabcent
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCENT"
          Exit Select
        Case tabcfam
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCFAM"
          Exit Select
        Case tabdriv
          nMaxLen = 9
          strPrgZoom = "ZOOMTABDRIV"
          Exit Select
        Case tabcfat
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcfga
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcfgo
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcfgs
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcfoa
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcgpa
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcgrp
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabchia
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCHIA"
          Exit Select
        Case tabchip
          nMaxLen = 5
          strPrgZoom = "ZOOMTABCHIP"
          Exit Select
        Case tabcint
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabciva
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCIVA"
          Exit Select
        Case tabclaq
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCLAQ"
          Exit Select
        Case tabclas
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCLAS"
          Exit Select
        Case tabclca
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCLCA"
          Exit Select
        Case tabcldn
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCLDN"
          Exit Select
        Case tabclic
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCLIC"
          Exit Select
        Case tabcliq
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcmdp
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCMPD"
          Exit Select
        Case tabcmer
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCMER"
          Exit Select
        Case tabcoce
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCOCE"
          Exit Select
        Case tabcoga
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcolo
          'non più usata
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabconc
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabconf
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCONF"
          Exit Select
        Case tabcont
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABCONT"
          Exit Select
        Case tabcope
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCOPE"
          Exit Select
        Case tabcorg
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCORG"
          Exit Select
        Case tabcove
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCOVE"
          Exit Select
        Case tabcovg
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCOVG"
          Exit Select
        Case tabcovp
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCOVP"
          Exit Select
        Case tabcpag
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCPAG"
          Exit Select
        Case tabcpar
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCPAR"
          Exit Select
        Case tabcpcl
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCPCL"
          Exit Select
        Case tabcptr
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCPTR"
          Exit Select
        Case tabcral
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCRAL"
          Exit Select
        Case tabcred
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcrgp
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcsar
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCSAR"
          Exit Select
        Case tabcscl
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCSCL"
          Exit Select
        Case tabcset
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCSET"
          Exit Select
        Case tabcsub
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCSUB"
          Exit Select
        Case tabctaf
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCTAF"
          Exit Select
        Case tabctas
          nMaxLen = 18
          strPrgZoom = "ZOOMTABCTAS"
          Exit Select
        Case tabctaz
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCTAZ"
          Exit Select
        Case tabctct
          nMaxLen = 4
          strPrgZoom = "ZOOMTABCTCT"
          Exit Select
        Case tabctpt
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctqp : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctqs : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctri : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctru : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctsf : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabctul
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcurg : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcval
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabcvuo
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCVUO"
          Exit Select
        Case tabdcag : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabdccl : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabdcge : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabdfor
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABDFOR"
          Exit Select
        Case tabdich : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabdimd
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABIMD"
          Exit Select
        Case tabdira
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABDIRA"
          Exit Select
        Case tabdire
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABDIRE"
          Exit Select
        Case tabdpro
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABDPRO"
          Exit Select
        Case tabdsed
          strFormatNum = "0"
          nMaxLen = 2
          dMinNum = 0
          dMaxNum = 99
          strPrgZoom = "ZOOMTABDSED"
          Exit Select
        Case tabdtge : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabduri
          'dati generali ultimi reg iva
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabdwim
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 32000
          strPrgZoom = "ZOOMTABDWIM"
          Exit Select
        Case tabenas
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABENAS"
          Exit Select
        Case tabenpa : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabesco
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABESCO"
          Exit Select
        Case tabespr
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABESPR"
          Exit Select
        Case tabform
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABFORM"
          Exit Select
        Case tabgmer
          strFormatNum = "0"
          nMaxLen = 2
          dMinNum = 0
          dMaxNum = 99
          strPrgZoom = "ZOOMTABGMER"
          Exit Select
        Case tabgrce
          strFormatNum = "0"
          nMaxLen = 2
          dMinNum = 0
          dMaxNum = 99
          strPrgZoom = "ZOOMTABGRCE"
          Exit Select
        Case tabgrua
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABGRUA"
          Exit Select
        Case tabgruc
          strFormatNum = "0"
          nMaxLen = 1
          dMinNum = 0
          dMaxNum = 9
          strPrgZoom = "ZOOMTABGRUC"
          Exit Select
        Case tabimba
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABIMBA"
          Exit Select
        Case tabimmd : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabimps : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabinai : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabinps : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabinsg : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabinst : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabiraa : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabirac : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabirar : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabirpe : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabista : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tablava : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tablavo
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLAVO"
          Exit Select
        Case tabling
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABLING"
          Exit Select
        Case tablipe
          'liquidazioni iva
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tablist
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = -9
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLIST"
          Exit Select
        Case tablivq
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABLIVQ"
          Exit Select
        Case tablmup
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABLMUP"
          Exit Select
        Case tablpro
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLPRO"
          Exit Select
        Case tablsar
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLSAR"
          Exit Select
        Case tablsel
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLSEL"
          Exit Select
        Case tabmaca
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMTABMACA"
          Exit Select
        Case tabmaga
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABMAGA"
          Exit Select
        Case tabmarc
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABMARC"
          Exit Select
        Case tabmast
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = ""
          Exit Select
        Case tabmesg : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabmfpr
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABMFPR"
          Exit Select
        Case tabmpor
          nMaxLen = 1
          strPrgZoom = "ZOOMTABMPOR"
          Exit Select
        Case tabmsos
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABMSOS"
          Exit Select
        Case tabngiu
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABNGIU"
          Exit Select
        Case tabnntc
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABNNTC"
          Exit Select
        Case tabnono : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabnote
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABNOTE"
          Exit Select
        Case tabntra
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABNTRA"
          Exit Select
        Case tabnuma
          nMaxLen = 2
          strPrgZoom = "ZOOMTABNUMA"
          Exit Select
        Case tabnumg
          nMaxLen = 2
          strPrgZoom = "ZOOMTABNUMG"
          Exit Select
        Case tabopes
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpaga
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABPAGA"
          Exit Select
        Case tabpcam
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABPCAM"
          Exit Select
        Case tabpcon
          nMaxLen = 12
          strPrgZoom = "ZOOMTABPCON"
          Exit Select
        Case tabpdon
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABPDON"
          Exit Select
        Case tabpeac : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpece : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpecg : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpepa : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabperv
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABPERV"
          Exit Select
        Case tabpeve : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabport
          nMaxLen = 3
          strPrgZoom = "ZOOMTABPORT"
          Exit Select
        Case tabpqua
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABPQUA"
        Case tabprcr : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabprea : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpres : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabprev
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABPREV"
          Exit Select
        Case tabpriv : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabprog
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABPROG"
          Exit Select
        Case tabprov : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabprve : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabpuce
          nMaxLen = 8
          strPrgZoom = "ZOOMTABPUCE"
          Exit Select
        Case tabqass : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabragg
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABRAGG"
          Exit Select
        Case tabrapp : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabrdim : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabregb : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabregi : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabrepa
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABREPA"
          Exit Select
        Case tabrica : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabriel : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabrivi : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabrpeg : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabrsta
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABRSTA"
          Exit Select
        Case tabrtac
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABRTAC"
          Exit Select
        Case tabruaz
          nMaxLen = 4
          strPrgZoom = "ZOOMTABRUAZ"
          Exit Select
        Case tabsatc : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabscpr : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabseta
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMTABSETA"
          Exit Select
        Case tabsett
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABSETT"
          Exit Select
        Case tabsgme
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSGME"
          Exit Select
        Case tabsiso
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSISO"
          Exit Select
        Case tabsmel
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = ""
          Exit Select
        Case tabsmer
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabsoca : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabspce
          nMaxLen = 6
          strPrgZoom = "ZOOMTABSPCE"
          Exit Select
        Case tabspti : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabsstu : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabstac : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabstag
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSTAG"
          Exit Select
        Case tabstat
          nMaxLen = 3
          strPrgZoom = "ZOOMTABSTAT"
          Exit Select
        Case tabstes : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabstso
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSTSO"
          Exit Select
        Case tabstud
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABSTUD"
          Exit Select
        Case tabtaco
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTACO"
          Exit Select
        Case tabtacq
          nMaxLen = 1
          strPrgZoom = "ZOOMTABTACQ"
          Exit Select
        Case tabtagl
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTAGL"
          Exit Select
        Case tabtcom
          nMaxLen = 4
          strPrgZoom = "ZOOMTABTCOM"
          Exit Select
        Case tabtcon
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTCON"
          Exit Select
        Case tabtdif
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTDIF"
          Exit Select
        Case tabtest : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtimp
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTIMP"
          Exit Select
        Case tabtipa
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTIPA"
          Exit Select
        Case tabtitp : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtits
          'genesys
          Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtopc : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtopp
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTOPP"
          Exit Select
        Case tabtops : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtpan : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtpbf
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTPBF"
          Exit Select
        Case tabtpce
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTPCE"
          Exit Select
        Case tabtpco
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTPCO"
          Exit Select
        Case tabtpes : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtpmo
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTPMO"
          Exit Select
        Case tabtpro
          nMaxLen = 4
          strPrgZoom = "ZOOMTABTPRO"
          Exit Select
        Case tabtpse : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtpsq
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTPSQ"
          Exit Select
        Case tabtpst : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabtrib
          nMaxLen = 4
          strPrgZoom = "ZOOMTABTRIB"
          Exit Select
        Case tabtric
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABTRIC"
          Exit Select
        Case tabtsca
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTSCA"
          Exit Select
        Case tabttas
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTTAS"
          Exit Select
        Case tabultc : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabumis
          nMaxLen = 3
          strPrgZoom = "ZOOMTABUMIS"
          Exit Select
        Case tabunic : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabusat
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABUSAT"
          Exit Select
        Case tabvalf
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABVALF"
          Exit Select
        Case tabvalu
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABVALU"
          Exit Select
        Case tabvarc : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
        Case tabvari
          strFormatNum = ""
          nMaxLen = 6
          strPrgZoom = "ZOOMTABVARI"
          Exit Select
        Case tabvett
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABVETT"
          Exit Select
        Case tabvpar
          nMaxLen = 18
          strPrgZoom = "ZOOMTABVPAR"
          Exit Select
        Case tabzone
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABZONE"
          Exit Select
        Case tabcomuni
          nMaxLen = 5
          strPrgZoom = "ZOOMCOMUNI"
          Exit Select
        Case tabqpro
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABQPRO"
          Exit Select
        Case tabanacesp
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANACESP"
          Exit Select
        Case tabd7percip
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMD7PERCIP"
          Exit Select
        Case tabdistbas
          strFormatNum = ""
          nMaxLen = CodartMaxLen
          strPrgZoom = "ZOOMDISTBAS"
          Exit Select
        Case tabopportun
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMOPPORTUN"
          Exit Select

        Case tabalesets
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTIPIALERT"
          Exit Select
        Case tabproc
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABPROC"
          Exit Select
        Case tabstab
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABSTAB"
          Exit Select
        Case tabgrlo
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABGRLO"
          Exit Select
        Case tabcract
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMCRACT"
          Exit Select
        Case tabchiam
          strFormatNum = "0"
          nMaxLen = 10
          dMinNum = 0
          dMaxNum = 9999999999
          strPrgZoom = "ZOOMCHIAMATE"
          Exit Select
        Case profil
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMPROFIL"
          Exit Select
        Case schtbl
          strFormatNum = ""
          nMaxLen = 30
          strPrgZoom = ""
          Exit Select
        Case proced
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMPROCED"
          Exit Select
        Case tabelle
          nMaxLen = 10
          strPrgZoom = "ZOOMTABELLE"
          Exit Select
        Case tabpcca
          nMaxLen = 12
          strPrgZoom = "ZOOMTABPCCA"
          Exit Select
        Case tabgru2
          strFormatNum = "0"
          nMaxLen = 2
          dMinNum = 0
          dMaxNum = 99
          strPrgZoom = "ZOOMTABGRU2"
          Exit Select
        Case tabclc2
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABCLC2"
          Exit Select
        Case tabmac2
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = ""
          Exit Select
        Case tabanagca2
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMANAGCA2"
          Exit Select
        Case tabdica
          nMaxLen = 12
          strPrgZoom = "ZOOMTABDICA"
          Exit Select
        Case tabdicv
          nMaxLen = 12
          strPrgZoom = "ZOOMTABDICV"
          Exit Select
        Case tabtcdc
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTCDC"
          Exit Select
        Case tabcaca
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABCACA"
          Exit Select
        Case tabvoca
          nMaxLen = 9
          strPrgZoom = "ZOOMTABVOCA"
          Exit Select
        Case tabscbg
          strFormatNum = "0"
          nMaxLen = 3
          dMinNum = 0
          dMaxNum = 999
          strPrgZoom = "ZOOMTABSCBG"
          Exit Select
        Case tabescg
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABESCG"
          Exit Select
        Case tabdivi
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABDIVI"
          Exit Select
        Case tabscen
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSCEN"
          Exit Select
        Case tabtcex
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTCEX"
          Exit Select
        Case tabdime
          strFormatNum = "0"
          nMaxLen = 8
          dMinNum = 0
          dMaxNum = 99999999
          strPrgZoom = "ZOOMCAMPIDIMENSIONI"
          Exit Select
        Case tabrere
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABRERE"
          Exit Select
        Case tabrepc
          nMaxLen = 50
          strPrgZoom = "ZOOMTABREPC"
          Exit Select
        Case tabsezd
          strFormatNum = "0"
          nMaxLen = 6
          dMinNum = 0
          dMaxNum = 999999
          strPrgZoom = "ZOOMTABSEZD"
        Case tabreec
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMTABREEC"
          Exit Select
        Case tabrepr
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABREPR"
          Exit Select
        Case tabrecp
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABRECP"
          Exit Select
        Case tabrett
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABRETT"
          Exit Select
        Case tabtsla
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTSLA"
          Exit Select
        Case tabtchi
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABTCHI"
          Exit Select
        Case tabstpg
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSTPG"
          Exit Select
        Case tabnaut
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABNAUT"
          Exit Select
        Case tabprob
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMTABPROB"
          Exit Select
        Case taberog
          nMaxLen = 1
          strPrgZoom = "ZOOMTABEROG"
          Exit Select
        Case tabmodi
          nMaxLen = 1
          strPrgZoom = "ZOOMTABMODI"
          Exit Select
        Case tabconfproc
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMPROCEDCONFIG"
          Exit Select
        Case tabvfgr
          nMaxLen = 2
          strPrgZoom = "ZOOMTABVFGR"
          Exit Select
        Case tabvfde
          nMaxLen = 4
          strPrgZoom = "ZOOMTABVFDE"
          Exit Select
        Case tabretailartico
          nMaxLen = CodartMaxLen
          strPrgZoom = "ZOOMRETAILARTICO"
          Exit Select
        Case tabretailanagra
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMRETAILANAGRA"
          Exit Select
        Case tablotx
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABLOTX"
        Case tabcalop
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMCALOP"
          Exit Select
        Case tabseat
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSEAT"
          Exit Select
        Case tabstco
          strFormatNum = "0"
          nMaxLen = 4
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMTABSTCO"
          Exit Select
        Case tabcontatti
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 999999999
          strPrgZoom = "ZOOMCONTATTI"
          Exit Select
        Case tabmodrich
          strFormatNum = "0"
          nMaxLen = 9
          dMinNum = 0
          dMaxNum = 9999
          strPrgZoom = "ZOOMMODRICH"
          Exit Select
        Case tabcpos
          strFormatNum = "0"
          nMaxLen = 5
          dMinNum = 0
          dMaxNum = 99999
          strPrgZoom = "ZOOMTABCPOS"
          Exit Select
        Case Else : Throw New Exception("CLN__STD.NTSSetParam: i valori non sono stati impostati per la costante '" & nTabe.ToString & "'") : Exit Select
      End Select

    Catch ex As Exception
      Throw New Exception("BN__STD - NTSSetParam: errore " & ex.Message)
    End Try
  End Sub

  Public Shared Function NTSIstanziaDll(ByVal strServerDir As String, ByVal strNetDir As String, _
                                        ByVal strNomeChiamante As String, ByVal strNomeDllStandard As String, _
                                        ByRef oDll As Object, ByRef strErrorOut As String, _
                                        ByVal bRemoting As Boolean, ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    '---------------------------------------------
    'DA UTILIZZARSI SOLO PER ENTITY E DAL
    '---------------------------------------------
    'dato il nome di chi mi ha chiamato ed il nome standard della dll da istanziare,
    'cerco nel file DLLMAP.INI il nome della dll da istanziare: se non trovo il file
    'o non trovo la mappatura istanzio la dll standard
    'restituisco true se tutto ok ,diversamente false + in strErrorout il motivo dell'errore
    Dim s1 As StreamReader
    Dim strLine As String = ""
    Dim strT() As String
    Dim strNomeDll As String = ""
    Dim assem As System.Reflection.Assembly
    Dim childType As System.Type
    Dim strNomeClasse As String = ""
    Dim strNomeClasseStd As String = ""
    Dim bOk As Boolean = False
    Dim bPers As Boolean = False

    Try
      strErrorOut = ""
      Try
        '----------------------------
        'istanzio la dll
        strNomeChiamante = strNomeChiamante.ToUpper
        strNomeDllStandard = strNomeDllStandard.ToUpper
        strNomeDll = strNomeDllStandard
        Dim strSplit() As String = strNomeDll.Split("."c)
        If strSplit.Length > 1 Then
          strNomeDll = strSplit(0)
          strNomeClasse = "NTSInformatica." & strSplit(1)
        Else
          strNomeClasse = "NTSInformatica.CL" & strNomeDll.Substring(1)
        End If
        strNomeClasseStd = strNomeClasse

        If FRIENDLY = False Then
          If File.Exists(strServerDir + "\Script\DLLMAP.INI") Then
            s1 = New StreamReader(strServerDir + "\Script\DLLMAP.INI")
            While Not s1.EndOfStream
              strLine = s1.ReadLine.ToUpper
              If strLine.Trim <> "" Then
                strT = strLine.Split("|"c)
                If strT(1) = strNomeDllStandard AndAlso (strT(0) = strNomeChiamante OrElse strT(0).Trim = "*") Then
                  'ho il nome della dll da istanziare
                  bPers = True
                  strNomeDll = strT(2)
                  strNomeClasse = strT(3)
                  Exit While
                End If
              End If
            End While
            s1.Close()
          End If
        End If
      Catch ex As Exception
        'non ho trovato il file: istanzierò la dll standard
        MsgBox("Vi è verificato un errore durante il caricamento delle personalizzazioni:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Errore nel DLLMAP.INI")
      End Try

      If IOS OrElse ANDROID OrElse FRIENDLY Then
        'non funziona!!!!
        'per friend non abilitata la funzionalità
      Else
        bOk = NTSIstanziaDLL_PJ(strServerDir, strNetDir, strNomeDllStandard, oDll, _
                                strErrorOut, strNomeClasse, strNomeDll)
        If strErrorOut <> "" Then Return False
      End If


      If bOk = False Then
        If bPers AndAlso System.IO.File.Exists(strNetDir & strSep & strNomeDll & ".DLL") Then '''
          'posso proseguire con dll personalizzata
        Else
          If bPers Then
            'il file dllmap.ini è vecchio e contiene una chiamata ad una dll personalizzata che non esiste: ignoro la personalizzazione
            strNomeDll = strNomeDllStandard
            strNomeClasse = strNomeClasseStd
            bPers = False
          End If
        End If

        'attivo la classe sull'oggetto passatomi dalla DLL chiamante
        assem = System.Reflection.Assembly.LoadFrom(strNetDir & strSep & strNomeDll & ".DLL")
        childType = assem.GetType(strNomeClasse, True, True)
        oDll = Activator.CreateInstance(childType)
        Try
          If bPers Then oDll.CustomClass = bPers
        Catch ex As Exception
        End Try
      End If

      Return True
    Catch ex As Exception
      strErrorOut = ex.Message
      oDll = Nothing
      Return False
    End Try
  End Function


  Public Shared Function NTSIstanziaDLL_PJ(ByVal strServerDir As String, ByVal strNetDir As String, _
                                        ByVal strNomeDllStandard As String, _
                                        ByRef oDll As Object, ByRef strErrorOut As String, _
                                        ByVal strNomeClasseDllMap As String, ByVal strNomeDllDllMap As String) As Boolean
    'utilizzata per istanziare dll di nts project (vedi BusinessFile)
    Dim s1 As StreamReader
    Dim strLine As String = ""
    Dim strT() As String
    Dim strNomeDll1 As String = ""
    Dim strNomeClasse2 As String = ""
    Dim bOk As Boolean = False
    Dim bForm_T As Boolean = False
    Try
      If strNomeClasseDllMap.EndsWith("*") And CLN__STD.FRIENDLY = False Then
        'se necessario verifico e faccio partire la from _T (veboll/gsor)
        'nel BK devono esserci sia le classi 
        'Public Class FRKVEBOLL    Inherits ***INHERITS***
        'che
        'Public Class FRKVEBOLL_T    Inherits ***INHERITS***_T
        bForm_T = True
        strNomeClasseDllMap = strNomeClasseDllMap.Substring(0, strNomeClasseDllMap.Length - 1)
      End If

      If IsWebApp() Then Return False
      '---------------
      'esempio di DLLMAP.INI
      'DLL Chiamante|DLL standard|DLL da instanziare|nomeserver.nomeworkspace.nomeclasse dll da istanziare
      '--------|--------|--------|-------------------------------------------------------------------------
      'BNVEBOLL|BEVEBOLL|BFVEBOLL|NtsInformatica.CLFVEBOLL
      'BFVEBOLL|BDVEBOLL|BHVEBOLL|NtsInformatica.CLHVEBOLL

      'esempio di DLLMAPEX.INI
      'DLL standard|DLL da instanziare|nomeclasse dll da istanziare
      '--------|--------|-------------------------------------------------------------------------
      'BNMGARMD.FRMMGARMA|BKMGARMD|FRKMGARMA  (se la dll contiene più form, posso specificare la form da aprire, diversamente viene presa la form con nome = alla dll
      'BNVEBOLL|BKVEBOLL|FRKVEBOLL
      'BEVEBOLL|BXVEBOLL|CLXVEBOLL
      'BDVEBOLL|BYVEBOLL|CLYVEBOLL

      '-------------------------
      'esempio di file BKVEBOLL.NTS (UI)
      '<nts>
      '<use_ assembly="BN__SCTE.DLL"/>
      '<reference assembly="BNVEBOLL.dll" />
      '<reference assembly="BEVEBOLL.dll" />
      '<reference assembly="BEMGDOCU.dll" />
      '<reference assembly="BE__BASN.dll" />
      '<reference assembly="BD__BASE.dll" />
      '
      '<scriptCode><![CDATA[
      '
      'Imports Microsoft.VisualBasic
      'Imports System
      'Imports System.Data
      'Imports NTSInformatica.CLN__STD
      'Imports NTSInformatica
      'Imports NTSInformatica.CLD__BASE
      '
      'Public Class FRKVEBOLL
      '    Inherits ***INHERITS***
      '
      'End Class
      ']]></scriptCode>
      '</nts>

      '-------------------------
      'esempio di file BXVEBOLL.NTS (ENTITY)
      '<nts>
      '<use_ assembly="BN__SCTE.DLL"/>
      '<reference assembly="BEVEBOLL.dll" />
      '<reference assembly="BEMGDOCU.dll" />
      '<reference assembly="BE__BASN.dll" />
      '<reference assembly="BD__BASE.dll" />
      '<reference_ assembly="BEHHBUSF.dll" />
      '
      '<scriptCode><![CDATA[
      '
      'Imports Microsoft.VisualBasic
      'Imports System
      'Imports System.Data
      'Imports NTSInformatica.CLN__STD
      'Imports NTSInformatica
      'Imports NTSInformatica.CLD__BASE
      '
      'Public Class CLXVEBOLL
      '    Inherits ***INHERITS***
      '		
      '	'Public oCore as CLEHHBUSF
      '
      '  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, _
      '                                    ByRef oCleLbmenu As Object, ByVal strTabella As String, _
      '                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
      '                                    ByVal strRemotePort As String) As Boolean
      '		If Not MyBase.Init(App, oScriptEngine,oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort) Then return False
      '		
      '		'oCore = New CLEHHBUSF
      '		
      '		Return True
      '	End Function
      '	
      '	Public Overrides Function SalvaDocumento(ByVal strState As String) As Boolean
      '		If Not MyBase.SalvaDocumento(strState) then Return False 
      '		
      '		'If Not oCore.SalvaDocumento(Me, strState) Then Return False
      '		
      '		Return True
      '	End Function
      'End Class
      '
      ']]></scriptCode>
      '</nts>

      '-------------------------
      'esempio di file BYVEBOLL.DLL (DAL)
      '<nts>
      '<use_ assembly="BN__SCTE.DLL"/>
      '<reference assembly="BDVEBOLL.dll" />
      '<reference assembly="BDMGDOCU.dll" />
      '<reference assembly="BE__BASN.dll" />
      '<reference assembly="BD__BASE.dll" />
      '
      '<scriptCode><![CDATA[
      '
      'Imports Microsoft.VisualBasic
      'Imports System
      'Imports System.Data
      'Imports NTSInformatica.CLN__STD
      'Imports NTSInformatica
      'Imports NTSInformatica.CLD__BASE
      '
      'Public Class CLYVEBOLL
      '    Inherits ***INHERITS***
      '
      'End Class
      '
      ']]></scriptCode>
      '</nts>

      If System.IO.File.Exists(strServerDir + strSep + "Script" + strSep + "DLLMAPEX.INI") = False Then Return False

      s1 = New StreamReader(strServerDir + strSep + "Script" + strSep + "DLLMAPEX.INI")

      While Not s1.EndOfStream
        strLine = s1.ReadLine.ToUpper
        If strLine.Trim <> "" Then
          strT = strLine.Split(CType("|", Char))
          If strT(0) = strNomeDllStandard & "." & strNomeClasseDllMap Then
            'solo per le form: nome dll + "." + nome form (nel caso in cui la dll contenga più form: se omesso viene presa la form con nome = a nome dll
            strNomeDll1 = strT(1)
            strNomeClasse2 = strT(2)
            Exit While
          End If
          If strT(0) = strNomeDllStandard Then
            'ho il nome della dll da istanziare
            strNomeDll1 = strT(1)
            strNomeClasse2 = strT(2)
          End If
        End If
      End While
      s1.Close()

      If strNomeDll1 = "" Then Return False

      s1 = New StreamReader(strServerDir + strSep + "Script" + strSep & strNomeDll1 & ".NTS")
      Dim strTesto As String = s1.ReadToEnd
      s1.Close()

      Dim strError As String = ""
      Dim strReference As String = ""
      Dim strCode As String = ""
      strTesto = strTesto.Replace("***INHERITS***", strNomeClasseDllMap.Replace("NTSINFORMATICA.", ""))

      'se serve, faccio partire la form _T al posto della FRK standard. solo se no friendly e no sbs
      If bForm_T Then
        If strTesto.Contains(strNomeClasse2 & "_T") Then strNomeClasse2 = strNomeClasse2 & "_T"
      End If

      'devo aggiungere tutti i reference della classe da cui eredito, se è divers da BE o BD o FRM
      Dim context As New System.Xml.XmlParserContext(Nothing, Nothing, Nothing, System.Xml.XmlSpace.None)
      Dim xml As System.Xml.XmlTextReader = New System.Xml.XmlTextReader(strTesto, System.Xml.XmlNodeType.Element, context)

      While xml.Read()
        If xml.Name.ToLower = "reference" Then
          strReference += xml.GetAttribute("assembly") + "|"
        End If

        If xml.Name = "scriptCode" Then
          strCode = xml.ReadElementString("scriptCode").Trim()
        End If
      End While
      xml.Close()

      '
      Dim cpar As New System.CodeDom.Compiler.CompilerParameters()
      cpar.GenerateInMemory = True
      cpar.GenerateExecutable = False
      cpar.IncludeDebugInformation = True
      'cpar.CompilerOptions = "/optionexplicit+ /optionstrict+"   'FORZO OPTION STRICT ON E OPTION EXPLICIT
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "mscorlib.dll") = -1 Then cpar.ReferencedAssemblies.Add("mscorlib.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "system.dll") = -1 Then cpar.ReferencedAssemblies.Add("system.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "system.windows.forms.dll") = -1 Then cpar.ReferencedAssemblies.Add("system.windows.forms.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "Microsoft.VisualBasic.dll") = -1 Then cpar.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "System.Data.dll") = -1 Then cpar.ReferencedAssemblies.Add("System.Data.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "System.XML.dll") = -1 Then cpar.ReferencedAssemblies.Add("System.XML.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "System.Drawing.dll") = -1 Then cpar.ReferencedAssemblies.Add("System.Drawing.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "System.Runtime.Remoting.dll") = -1 Then cpar.ReferencedAssemblies.Add("System.Runtime.Remoting.dll")

      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BN__STD.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BN__STD.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BE__FRWK.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BE__FRWK.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BE__MENU.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BE__MENU.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BN__CHIL.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BN__CHIL.dll")

      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BE__CZOO.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BE__CZOO.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BELBMENU.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BELBMENU.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BE__BASN.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BE__BASN.dll")
      If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + "BD__BASE.dll") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + "BD__BASE.dll")

      Dim strTmp() As String = System.IO.Directory.GetFiles(strNetDir, "DevExpress*.dll")
      For i As Integer = 0 To strTmp.Length - 1
        'devo prendere solo i file della devexpres 6.2 (nella 2011 ci saranno anche quelli 9.3 ...)
        If strTmp(i).ToUpper.IndexOf("V6.2.DLL") > -1 Then
          If cpar.ReferencedAssemblies.IndexOf(strTmp(i)) = -1 Then cpar.ReferencedAssemblies.Add(strTmp(i))
        End If
      Next

      'aggiungo ulteriori reference presenti nel file
      If strReference.Length > 0 Then
        strTmp = strReference.Split("|"c)
        For i As Integer = 0 To strTmp.Length - 1
          'If strTmp(i).Substring(0, 2) = "BN" Or strTmp(i).Substring(0, 2) = "BE" Or strTmp(i).Substring(0, 2) = "BD" Then
          If strTmp(i).Trim <> "" Then
            If System.IO.File.Exists(strNetDir + strSep + strTmp(i)) Then
              If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + strTmp(i)) = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + strTmp(i))
            Else
              If cpar.ReferencedAssemblies.IndexOf(strTmp(i)) = -1 Then cpar.ReferencedAssemblies.Add(strTmp(i))
            End If
          End If
        Next
      End If

      '----------------------------------------------
      'devo aggiungere i reference della dll personalizzata
      Dim asm As Assembly = System.Reflection.Assembly.LoadFrom(strNetDir & strSep & strNomeDllDllMap & ".DLL")
      For i As Integer = 0 To asm.GetReferencedAssemblies.Length - 1
        Dim s As String = asm.GetReferencedAssemblies(i).Name
        If System.IO.File.Exists(strNetDir + strSep + asm.GetReferencedAssemblies(i).Name & ".DLL") Then
          If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + asm.GetReferencedAssemblies(i).Name & ".DLL") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + asm.GetReferencedAssemblies(i).Name & ".DLL")
        Else
          If cpar.ReferencedAssemblies.IndexOf(asm.GetReferencedAssemblies(i).Name & ".DLL") = -1 Then cpar.ReferencedAssemblies.Add(asm.GetReferencedAssemblies(i).Name & ".DLL")
        End If
      Next
      asm = Nothing
      If System.IO.File.Exists(strNetDir + strSep + strNomeDllDllMap & ".DLL") Then
        If cpar.ReferencedAssemblies.IndexOf(strNetDir + strSep + strNomeDllDllMap & ".DLL") = -1 Then cpar.ReferencedAssemblies.Add(strNetDir + strSep + strNomeDllDllMap & ".DLL")
      End If

      '----------------------------------------------
      'definisco il compilatore del linguaggio
      Dim cp As New Microsoft.VisualBasic.VBCodeProvider

      '----------------------------------------------
      'preparo l'oggetto che conterrà il risultato della compilazione (con gli eventuali errori)
      Dim cr As System.CodeDom.Compiler.CompilerResults

      '----------------------------------------------
      'eseguo la compilazione
      'Me.GetType.Assembly.GetName.Name
      cr = cp.CompileAssemblyFromSource(cpar, strCode)

      '----------------------------------------------
      'testo gli errori di compilazione
      For Each ce As System.CodeDom.Compiler.CompilerError In cr.Errors
        strError += "Linea " & ce.Line.ToString & " colonna " & ce.Column & ": errore " & ce.ErrorNumber.ToString & " - " & ce.ErrorText + vbCrLf
      Next
      If strError.Length > 0 Then
        Throw New Exception("CLN__STD.NTSIstanziaDLL_PJ: Errori in fase di compilazione script '|" + strServerDir + strSep + "Script" & strSep + strNomeDll1 & ".NTS" + "|': " + vbCrLf + "|" + strError + "|")
      End If

      '----------------------------------------------
      'se tutto ok eseguo il codice
      If cr.Errors.Count = 0 And Not cr.CompiledAssembly Is Nothing Then
        bOk = True
        Try
          If Not cr.CompiledAssembly.GetType(strNomeClasse2) Is Nothing Then
            oDll = Activator.CreateInstance(cr.CompiledAssembly.GetType(strNomeClasse2))
          Else
            Throw New Exception("CLN__STD.NTSIstanziaDLL_PJ: Nel file di script '|" + strServerDir + strSep + "Script" & strSep + strNomeDll1 & ".NTS" + "|' non è stata trovata la classe '|" + strNomeClasseDllMap + "|'")
          End If
        Catch ex As Exception
          Throw New Exception("CLN__STD.NTSIstanziaDLL_PJ: Errori in fase di compilazione script '|" + strServerDir + strSep + "Script" & strSep + strNomeDll1 & ".NTS" + "|': " + vbCrLf + "|" + ex.Message + "|")
        End Try
      End If

      Return bOk

    Catch ex As Exception
      strErrorOut = ex.Message
      oDll = Nothing
      Return False
    End Try
  End Function

  Public Shared Function calcolaPVRCheck(ByVal strIn As String) As Integer
    'funzione per il calcolo del carattere di controllo per i PVR Svizzeri
    Dim i As Integer
    Dim nres As Integer
    Dim arModel(10, 10) As Integer
    arModel(0, 0) = 0 : arModel(0, 1) = 9 : arModel(0, 2) = 4 : arModel(0, 3) = 6 : arModel(0, 4) = 8 : arModel(0, 5) = 2 : arModel(0, 6) = 7 : arModel(0, 7) = 1 : arModel(0, 8) = 3 : arModel(0, 9) = 5
    arModel(1, 0) = 9 : arModel(1, 1) = 4 : arModel(1, 2) = 6 : arModel(1, 3) = 8 : arModel(1, 4) = 2 : arModel(1, 5) = 7 : arModel(1, 6) = 1 : arModel(1, 7) = 3 : arModel(1, 8) = 5 : arModel(1, 9) = 0
    arModel(2, 0) = 4 : arModel(2, 1) = 6 : arModel(2, 2) = 8 : arModel(2, 3) = 2 : arModel(2, 4) = 7 : arModel(2, 5) = 1 : arModel(2, 6) = 3 : arModel(2, 7) = 5 : arModel(2, 8) = 0 : arModel(2, 9) = 9
    arModel(3, 0) = 6 : arModel(3, 1) = 8 : arModel(3, 2) = 2 : arModel(3, 3) = 7 : arModel(3, 4) = 1 : arModel(3, 5) = 3 : arModel(3, 6) = 5 : arModel(3, 7) = 0 : arModel(3, 8) = 9 : arModel(3, 9) = 4
    arModel(4, 0) = 8 : arModel(4, 1) = 2 : arModel(4, 2) = 7 : arModel(4, 3) = 1 : arModel(4, 4) = 3 : arModel(4, 5) = 5 : arModel(4, 6) = 0 : arModel(4, 7) = 9 : arModel(4, 8) = 4 : arModel(4, 9) = 6
    arModel(5, 0) = 2 : arModel(5, 1) = 7 : arModel(5, 2) = 1 : arModel(5, 3) = 3 : arModel(5, 4) = 5 : arModel(5, 5) = 0 : arModel(5, 6) = 9 : arModel(5, 7) = 4 : arModel(5, 8) = 6 : arModel(5, 9) = 8
    arModel(6, 0) = 7 : arModel(6, 1) = 1 : arModel(6, 2) = 3 : arModel(6, 3) = 5 : arModel(6, 4) = 0 : arModel(6, 5) = 9 : arModel(6, 6) = 4 : arModel(6, 7) = 6 : arModel(6, 8) = 8 : arModel(6, 9) = 2
    arModel(7, 0) = 1 : arModel(7, 1) = 3 : arModel(7, 2) = 5 : arModel(7, 3) = 0 : arModel(7, 4) = 9 : arModel(7, 5) = 4 : arModel(7, 6) = 6 : arModel(7, 7) = 8 : arModel(7, 8) = 2 : arModel(7, 9) = 7
    arModel(8, 0) = 3 : arModel(8, 1) = 5 : arModel(8, 2) = 0 : arModel(8, 3) = 9 : arModel(8, 4) = 4 : arModel(8, 5) = 6 : arModel(8, 6) = 8 : arModel(8, 7) = 2 : arModel(8, 8) = 7 : arModel(8, 9) = 1
    arModel(9, 0) = 5 : arModel(9, 1) = 0 : arModel(9, 2) = 9 : arModel(9, 3) = 4 : arModel(9, 4) = 6 : arModel(9, 5) = 8 : arModel(9, 6) = 2 : arModel(9, 7) = 7 : arModel(9, 8) = 1 : arModel(9, 9) = 3

    If Not IsNumeric(strIn) Then
      calcolaPVRCheck = -1
      Exit Function
    End If
    nres = 0
    For i = 1 To Len(strIn)
      nres = arModel(nres, CInt(Mid(strIn, i, 1)))
    Next
    nres = 10 - nres
    If nres = 10 Then nres = 0
    calcolaPVRCheck = nres
  End Function

  Public Shared Function GetNewTempDirName() As String
    'ritorna il nome di una direcotry temporanea sicuramente inesistente (ma non la crea)
    Try
      Return System.IO.Path.GetTempPath() & System.IO.Path.GetRandomFileName()
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  <DebuggerStepThrough()> _
  Public Shared Sub ShowWindowsTaskBar()
    Dim lTaskBarWin As Integer = FindWindow("Shell_TrayWnd", vbNullString)
    SetWindowPos(lTaskBarWin, 0, 0, 0, 0, 0, SWP_SHOWWINDOW)
  End Sub

#Region "Costanti per moduli acquistati"
  'Costanti dei moduli
  Public Const bsModCG As Integer = 1 ' contab. generale
  Public Const bsModOR As Integer = 2 ' ordini
  Public Const bsModVE As Integer = 4 ' vendite/fatturazione
  Public Const bsModPR As Integer = 8 ' provvigioni
  Public Const bsModST As Integer = 16 ' statistiche
  Public Const bsModMG As Integer = 32 ' magazzino
  Public Const bsModDB As Integer = 64 ' distinta base
  Public Const bsModCI As Integer = 128 ' Contab. analitica
  Public Const bsModCE As Integer = 256 ' cespiti
  Public Const bsModORCL As Integer = 512 ' Verticale 1   (ex Oracle Database)
  Public Const bsModGT As Integer = 1024 ' Source Extender
  Public Const bsModCN As Integer = 2048 ' Conai
  Public Const bsModAS As Integer = 4096 'Customer Service (ex assistenza software (ex ass tecnica))
  Public Const bsModPI As Integer = 8192 ' Produzione
  Public Const bsModIE As Integer = 16384 ' Import/Export
  Public Const bsModRA As Integer = 32768 ' richieste d'acquisto RDA-RDO
  Public Const bsModTV As Integer = 65536 ' Tentata vendita
  Public Const bsModPM As Integer = 131072 ' Project Management
  Public Const bsModIC As Integer = 262144 ' Intrastat
  Public Const bsModCT As Integer = 524288 ' Genera PDF da documenti (ex Catalogo webness)
  Public Const bsModPE As Integer = 1048576 ' Percipienti/Rit. acconto
  Public Const bsMod22 As Integer = 2097152 ' a disposizione developers***
  Public Const bsMod23 As Integer = 4194304 ' a disposizione developers***
  Public Const bsModTE As Integer = 8388608 ' tesoreria
  Public Const bsMod25 As Integer = 16777216 ' a disposizione developers***
  Public Const bsModSM As Integer = 33554432 ' San Marino
  Public Const bsModCP As Integer = 67108864 ' Configuratore del prodotto
  Public Const bsModSQ As Integer = 134217728 ' sistema qualit ISO 9000
  Public Const bsMod29 As Integer = 268435456 ' a disposizione developers***
  Public Const bsMod30 As Integer = 536870912 ' a disposizione developers***
  Public Const bsModAll As Integer = 1073741823 'tutti      (tutti 1 in binario 2^60)
  'Costanti dei moduli estesi
  Public Const bsModExtCP2 As Integer = 1 ' Config. su distinta neutra (CP2) (Ex B2B di webness)
  Public Const bsModExtRET As Integer = 2  ' Retail  (Ex E-SFA webness)
  Public Const bsModExtTCO As Integer = 4  ' Taglie e colori (Ex E-Procurement (webness))
  Public Const bsModExtNOT As Integer = 8 ' Bilancio UE e nota integrativa (notagen)
  Public Const bsModExtANB As Integer = 16 ' Analisi di bilancio (notagen)
  Public Const bsModExtCRM As Integer = 32 ' CRM (ex Variaz. AA7/AA9)
  Public Const bsModExtPIK As Integer = 64 ' Picking su Palmare(ex CCIAA)
  Public Const bsModExtPAR As Integer = 128 ' parcellazione
  Public Const bsModExtCON As Integer = 256 ' funzioni comuni Consultant (ed F24 GIC)
  Public Const bsModExtANG As Integer = 512 ' anagrafiche generali
  Public Const bsModExtCGE As Integer = 1024 ' Contab. gen. Easy
  Public Const bsModExtORE As Integer = 2048 ' ordini Easy
  Public Const bsModExtMGE As Integer = 4096 ' magazzino Easy
  Public Const bsModExtCGS As Integer = 8192 ' contabilità professionisti
  Public Const bsModExtF24 As Integer = 16384 ' modulo F24 di Business
  Public Const bsModExtLEX As Integer = 32768 ' logistica extended
  Public Const bsModExtVE2 As Integer = 65536 ' Verticale 2 (ex comunicazione annuale Iva)
  Public Const bsModExtINT As Integer = 131072 ' International
  Public Const bsModExtNSS As Integer = 262144 ' NonSolo-Shop
  Public Const bsModExtSSE As Integer = 524288 ' commesse light
  Public Const bsModExtEIV As Integer = 1048576 ' Elenchi Iva Cli-for
  Public Const bsModExtTCP As Integer = 2097152 ' Produzione Taglie colori
  Public Const bsModExtCAS As Integer = 4194304 '       (ex 730)
  Public Const bsModExt740 As Integer = 8388608 '        ( ex 740,ICI +IRAP)
  Public Const bsModExtBNF As Integer = 16777216 ' BUSNET Frame       ( ex 750,ICI +IRAP)
  Public Const bsModExtXNT As Integer = 33554432 ' Moduli Supplementari NTS        ( ex 760,ICI +IRAP)
  Public Const bsModExtDWH As Integer = 67108864 ' Data-Warehouse (ex Mod. RED e ISEE (era a a disposizione...prima))
  Public Const bsModExt28 As Integer = 134217728 ' a disposizione
  Public Const bsModExt29 As Integer = 268435456 ' a disposizione
  Public Const bsModExt30 As Integer = 536870912 ' Moduli Supplementari Partner  (ex a disposizione)
  Public Const bsModExtAll As Integer = 1073741823 'tutti      (tutti 1 in binario 2^60)
  'Costanti dei moduli suplementari (da 101 a 130)
  Public Const bsModSupIPL As Integer = 1 ' inventari su palmare (101)
  Public Const bsModSupRPL As Integer = 2  ' Controllo ric. merce su palmare (102)
  Public Const bsModSupRME As Integer = 4  ' Soluzione palmare in terminal server
  Public Const bsModSupWCR As Integer = 8 ' Web CRM
  Public Const bsModSupWSS As Integer = 16 ' Web Soft Service
  Public Const bsModSupEMA As Integer = 32 ' E-mail integrata  
  Public Const bsModSupDWE As Integer = 64 ' Data-Warehouse
  Public Const bsModSupCAE As Integer = 128 ' Contabilità Analitica Estesa
  Public Const bsModSupBUD As Integer = 256 ' Budgeting e Controllo di Gestione
  Public Const bsModSupDKO As Integer = 512 ' Desktop Consolle
  Public Const bsModSupTEN As Integer = 1024 'Tesoreria e flussi finanziari
  Public Const bsModSupTEX As Integer = 2048 'Tesoreria Extended
  Public Const bsModSupGPV As Integer = 4096 'Gestione punti vendita (Nuovo retail)
  Public Const bsModSupGPE As Integer = 8192 'Gestione punti vendita Extended (Nuovo retail)
  Public Const bsModSupGPR As Integer = 16384 'Gestione punti vendita Recovery (Nuovo retail)
  Public Const bsModSupGID As Integer = 32768 'Gestione incassi differiti (ABPD)
  Public Const bsModSup17 As Integer = 65536 '
  Public Const bsModSupNTP As Integer = 131072 'Collegamento a NetPro Classic
  Public Const bsModSupBFP As Integer = 262144 'Bus4People Extented
  Public Const bsModSupAFI As Integer = 524288 'Analisi Fattibilità Impegni
  Public Const bsModSupEMP As Integer = 1048576 'PEC-MAIL
  Public Const bsModSupDII As Integer = 2097152 'Dichiarazioni di intento
  Public Const bsModSupCEC As Integer = 4194304 'Collegamento a sistema e-Commerce
  Public Const bsModSupBGE As Integer = 8388608 ' Business for People guest ext.
  Public Const bsModSupMVW As Integer = 16777216 'Customer service Map View
  Public Const bsModSupCCC As Integer = 33554432 'Contratti e Condizioni Commerciali
  Public Const bsModSup27 As Integer = 67108864 '
  Public Const bsModSupC64 As Integer = 134217728 'Crystal Reports 64bit server
  Public Const bsModSupSBF As Integer = 268435456 'Smart Business Framework
  Public Const bsModSup30 As Integer = 536870912 'Friendly
  Public Const bsModSupAll As Integer = 1073741823 '

  'Costanti dei moduli suplementari estesi (da 131 a 160)
  Public Const bsModSupExtJOR As Integer = 1 ' 131 --> Gestione ORTO (pers)
  Public Const bsModSupExt02 As Integer = 2  ' (132) ecc
  Public Const bsModSupExt03 As Integer = 4  '
  Public Const bsModSupExt04 As Integer = 8 '
  Public Const bsModSupExtANB As Integer = 16 'Notax - analisi di bilancio
  Public Const bsModSupExt06 As Integer = 32 '
  Public Const bsModSupExt07 As Integer = 64 '
  Public Const bsModSupExt08 As Integer = 128 '
  Public Const bsModSupExt09 As Integer = 256 '
  Public Const bsModSupExt10 As Integer = 512 '
  Public Const bsModSupExt11 As Integer = 1024 '
  Public Const bsModSupExt12 As Integer = 2048 '
  Public Const bsModSupExt13 As Integer = 4096 '
  Public Const bsModSupExt14 As Integer = 8192 '
  Public Const bsModSupExt15 As Integer = 16384 '
  Public Const bsModSupExt16 As Integer = 32768 '
  Public Const bsModSupExt17 As Integer = 65536 '
  Public Const bsModSupExt18 As Integer = 131072 '
  Public Const bsModSupExt19 As Integer = 262144 '
  Public Const bsModSupExt20 As Integer = 524288 '
  Public Const bsModSupExt21 As Integer = 1048576 '
  Public Const bsModSupExt22 As Integer = 2097152 '
  Public Const bsModSupExt23 As Integer = 4194304 '
  Public Const bsModSupExt24 As Integer = 8388608 '
  Public Const bsModSupExt25 As Integer = 16777216 '
  Public Const bsModSupExt26 As Integer = 33554432 '
  Public Const bsModSupExt27 As Integer = 67108864 '
  Public Const bsModSupExt28 As Integer = 134217728 '
  Public Const bsModSupExt29 As Integer = 268435456 '
  Public Const bsModSupExt30 As Integer = 536870912 '
  Public Const bsModSupExtAll As Integer = 1073741823 '
  'Costanti dei moduli partner
  Public Const bsModPtn01 As Integer = 1 '  (201)
  Public Const bsModPtn02 As Integer = 2  ' (202)
  Public Const bsModPtn03 As Integer = 4  '
  Public Const bsModPtn04 As Integer = 8 '
  Public Const bsModPtn05 As Integer = 16 '
  Public Const bsModPtn06 As Integer = 32 '
  Public Const bsModPtn07 As Integer = 64 '
  Public Const bsModPtn08 As Integer = 128 '
  Public Const bsModPtn09 As Integer = 256 '
  Public Const bsModPtn10 As Integer = 512 '
  Public Const bsModPtn11 As Integer = 1024 '
  Public Const bsModPtn12 As Integer = 2048 '
  Public Const bsModPtn13 As Integer = 4096 '
  Public Const bsModPtn14 As Integer = 8192 '
  Public Const bsModPtn15 As Integer = 16384 '
  Public Const bsModPtn16 As Integer = 32768 '
  Public Const bsModPtn17 As Integer = 65536 '
  Public Const bsModPtn18 As Integer = 131072 '
  Public Const bsModPtn19 As Integer = 262144 '
  Public Const bsModPtn20 As Integer = 524288 '
  Public Const bsModPtn21 As Integer = 1048576 '
  Public Const bsModPtn22 As Integer = 2097152 '
  Public Const bsModPtn23 As Integer = 4194304 '
  Public Const bsModPtn24 As Integer = 8388608 '
  Public Const bsModPtn25 As Integer = 16777216 '
  Public Const bsModPtn26 As Integer = 33554432 '
  Public Const bsModPtn27 As Integer = 67108864 '
  Public Const bsModPtn28 As Integer = 134217728 '
  Public Const bsModPtn29 As Integer = 268435456 '
  Public Const bsModPtn30 As Integer = 536870912 '
  Public Const bsModPtnAll As Integer = 1073741823 '
  'Costanti dei moduli partner estesi (231-260)
  Public Const bsModPtnExt01 As Integer = 1 ' (231)
  Public Const bsModPtnExt02 As Integer = 2  ' (232) ecc
  Public Const bsModPtnExt03 As Integer = 4  '
  Public Const bsModPtnExt04 As Integer = 8 '
  Public Const bsModPtnExt05 As Integer = 16 '
  Public Const bsModPtnExt06 As Integer = 32 '
  Public Const bsModPtnExt07 As Integer = 64 '
  Public Const bsModPtnExt08 As Integer = 128 '
  Public Const bsModPtnExt09 As Integer = 256 '
  Public Const bsModPtnExt10 As Integer = 512 '
  Public Const bsModPtnExt11 As Integer = 1024 '
  Public Const bsModPtnExt12 As Integer = 2048 '
  Public Const bsModPtnExt13 As Integer = 4096 '
  Public Const bsModPtnExt14 As Integer = 8192 '
  Public Const bsModPtnExt15 As Integer = 16384 '
  Public Const bsModPtnExt16 As Integer = 32768 '
  Public Const bsModPtnExt17 As Integer = 65536 '
  Public Const bsModPtnExt18 As Integer = 131072 '
  Public Const bsModPtnExt19 As Integer = 262144 '
  Public Const bsModPtnExt20 As Integer = 524288 '
  Public Const bsModPtnExt21 As Integer = 1048576 '
  Public Const bsModPtnExt22 As Integer = 2097152 '
  Public Const bsModPtnExt23 As Integer = 4194304 '
  Public Const bsModPtnExt24 As Integer = 8388608 '
  Public Const bsModPtnExt25 As Integer = 16777216 '
  Public Const bsModPtnExt26 As Integer = 33554432 '
  Public Const bsModPtnExt27 As Integer = 67108864 '
  Public Const bsModPtnExt28 As Integer = 134217728 '
  Public Const bsModPtnExt29 As Integer = 268435456 '
  Public Const bsModPtnExt30 As Integer = 536870912 '
  Public Const bsModPtnExtAll As Integer = 1073741823 '
#End Region

  Public Shared TTL_MSG As String = "Messaggio da Business NET"
  Public Const FORMAT_LIRE As String = "#,##0"
  Public Const FORMAT_LIREUR As String = "#,##0.00"
  Public Const IMPORTIDEC As Integer = 2
  Public Const IMPORTIDECVAL As Integer = 2

  'Costanti per gestione prezzi e sconti per quantità
  Public Const bsPrNoset As Integer = 0
  Public Const bsPrSetnoqta As Integer = 1
  Public Const bsPrSetsiqta As Integer = 2

  Public Shared Function IsDesignMode() As Boolean
    If Debugger.IsAttached Then
      Return False
    Else
      Return True
    End If
  End Function
  Public Shared Function IsDebugMode() As Boolean
    Return Not IsDesignMode()
  End Function

#Region "PER DETERMINARE LE PROPRIETA' DELLA STAMPANTE"
  Declare Function GetLastError Lib "kernel32.dll" Alias "GetLastErrorA" () As Int32
  Declare Function OpenPrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, ByRef phPrinter As IntPtr, ByRef pDefault As PRINTER_DEFAULTS) As Integer
  Declare Function ClosePrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal hPrinter As IntPtr) As Boolean
  Declare Auto Function DocumentProperties Lib "winspool.drv" Alias "DocumentPropertiesA" (ByVal hwnd As IntPtr, ByVal hPrinter As IntPtr, <MarshalAs(UnmanagedType.LPStr)> ByVal pDeviceNameg As String, ByVal pDevModeOutput As IntPtr, ByRef pDevModeInput As IntPtr, ByVal fMode As Integer) As Integer
  Declare Function GetPrinter Lib "winspool.drv" Alias "GetPrinterA" (ByVal hPrinter As IntPtr, ByVal dwLevel As Int32, ByVal pPrinter As IntPtr, ByVal dwBuf As Int32, ByRef dwNeeded As Int32) As Boolean
  Declare Function SetPrinter Lib "winspool.drv" Alias "SetPrinterA" (ByVal hPrinter As IntPtr, ByVal Level As Integer, ByVal pPrinter As IntPtr, ByVal Command As Integer) As Boolean

  <DllImport("comdlg32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
  Private Shared Function PrintDlg(<[In](), Out()> ByVal lppd As PRINTDLG_TYPE) As Integer
  End Function

  Public Declare Auto Function CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal Dest As IntPtr, ByVal Src As IntPtr, ByVal cb As Integer) As IntPtr
  Public Declare Auto Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Integer, ByVal dwBytes As Integer) As IntPtr
  Public Declare Auto Function GlobalLock Lib "kernel32.dll" (ByVal handle As HandleRef) As IntPtr
  Public Declare Auto Function GlobalUnlock Lib "kernel32.dll" (ByVal handle As HandleRef) As Boolean
  Public Declare Auto Function GlobalFree Lib "kernel32.dll" (ByVal handle As HandleRef) As IntPtr

  <StructLayout(LayoutKind.Sequential)> Public Structure PRINTER_DEFAULTS
    Public pDatatype As IntPtr
    Public pDevMode As IntPtr
    Public DesiredAccess As Integer
  End Structure

  <StructLayout(LayoutKind.Sequential)> Public Structure PRINTER_INFO_2

    <MarshalAs(UnmanagedType.LPStr)> Public pServerName As String
    <MarshalAs(UnmanagedType.LPStr)> Public pPrinterName As String
    <MarshalAs(UnmanagedType.LPStr)> Public pShareName As String
    <MarshalAs(UnmanagedType.LPStr)> Public pPortName As String
    <MarshalAs(UnmanagedType.LPStr)> Public pDriverName As String
    <MarshalAs(UnmanagedType.LPStr)> Public pComment As String
    <MarshalAs(UnmanagedType.LPStr)> Public pLocation As String
    Public pDevMode As IntPtr
    <MarshalAs(UnmanagedType.LPStr)> Public pSepFile As String
    <MarshalAs(UnmanagedType.LPStr)> Public pPrintProcessor As String
    <MarshalAs(UnmanagedType.LPStr)> Public pDatatype As String
    <MarshalAs(UnmanagedType.LPStr)> Public pParameters As String
    Public pSecurityDescriptor As IntPtr
    Public Attributes As Int32
    Public Priority As Int32
    Public DefaultPriority As Int32
    Public StartTime As Int32
    Public UntilTime As Int32
    Public Status As Int32
    Public cJobs As Int32
    Public AveragePPM As Int32
  End Structure

  Public Const ENUM_CURRENT_SETTINGS As Integer = -1&
  Public Const CCDEVICENAME As Integer = 32
  Public Const CCFORMNAME As Integer = 32

  <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> Public Class DEVMODE_TYPEANSI
    <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCDEVICENAME)> Public dmDeviceName As String
    Public dmSpecVersion As Short
    Public dmDriverVersion As Short
    Public dmSize As Short
    Public dmDriverExtra As Short
    Public dmFields As Integer
    Public dmOrientation As Short
    Public dmPaperSize As Short
    Public dmPaperLength As Short
    Public dmPaperWidth As Short
    Public dmScale As Short
    Public dmCopies As Short
    Public dmDefaultSource As Short
    Public dmPrintQuality As Short
    Public dmColor As Short
    Public dmDuplex As Short
    Public dmYResolution As Short
    Public dmTTOption As Short
    Public dmCollate As Short
    <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCFORMNAME)> Public dmFormName As String
    Public dmUnusedPadding As Short
    Public dmBitsPerPel As Short
    Public dmPelsWidth As Integer
    Public dmPelsHeight As Integer
    Public dmDisplayFlags As Integer
    Public dmDisplayFrequency As Integer
  End Class

  <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> Public Class DEVMODE_TYPE
    <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCDEVICENAME)> Public dmDeviceName As String
    Public dmSpecVersion As Short
    Public dmDriverVersion As Short
    Public dmSize As Short
    Public dmDriverExtra As Short
    Public dmFields As Integer
    Public dmOrientation As Short
    Public dmPaperSize As Short
    Public dmPaperLength As Short
    Public dmPaperWidth As Short
    Public dmScale As Short
    Public dmCopies As Short
    Public dmDefaultSource As Short
    Public dmPrintQuality As Short
    Public dmColor As Short
    Public dmDuplex As Short
    Public dmYResolution As Short
    Public dmTTOption As Short
    Public dmCollate As Short
    <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=CCFORMNAME)> Public dmFormName As String
    Public dmUnusedPadding As Short
    Public dmBitsPerPel As Short
    Public dmPelsWidth As Integer
    Public dmPelsHeight As Integer
    Public dmDisplayFlags As Integer
    Public dmDisplayFrequency As Integer

    Public dmICMMethod As Integer
    Public dmICMIntent As Integer
    Public dmMediaType As Integer
    Public dmDitherType As Integer
    Public dmReserved1 As Integer
    Public dmReserved2 As Integer
    Public dmPanningHeight As Integer 'senza questo alcuni driver di stampanti (es Kyocera Mita FS-1020D KX) non prendono da bus l'Orientation. inoltre con win successivo a XP se poi si passa printdlg1.hwndOwner = Me.Handle non prende per niente le impostazioni passate. se non lo si passa fa vedere la dialog vecchia!!!
    Public dmPositionX As Integer
    Public dmPositionY As Integer
  End Class

  <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
  Public Class PRINTDLG_TYPE
    Public lStructSize As Integer
    Public hwndOwner As IntPtr
    Public hDevMode As IntPtr
    Public hDevNames As IntPtr
    Public hDC As IntPtr
    Public Flags As Integer
    Public nFromPage As Short
    Public nToPage As Short
    Public nMinPage As Short
    Public nMaxPage As Short
    Public nCopies As Short
    Public hInstance As IntPtr
    Public lCustData As IntPtr
    Public lpfnPrintHook As IntPtr
    Public lpfnSetupHook As IntPtr
    Public lpPrintTemplateName As String
    Public lpSetupTemplateName As String
    Public hPrintTemplate As IntPtr
    Public hSetupTemplate As IntPtr
  End Class

  '<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
  Public Structure DEVNAMES_TYPE
    Dim wDriverOffset As Short
    Dim wDeviceOffset As Short
    Dim wOutputOffset As Short
    Dim wDefault As Short
    <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=100)> Public extra As String
    '<VBFixedString(100), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=100)> Public extra() As Char
  End Structure

  Public Const PRINTER_ACCESS_ADMINISTER As Integer = &H4
  Public Const PRINTER_ACCESS_USE As Integer = &H8
  Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
  Public Const PRINTER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED Or PRINTER_ACCESS_ADMINISTER Or PRINTER_ACCESS_USE)
  Public Const GMEM_FIXED As Short = &H0S
  Public Const GMEM_MOVEABLE As Short = &H2S
  Public Const GMEM_ZEROINIT As Short = &H40S
  Public Const DM_MODIFY As Integer = 8
  Public Const DM_COPY As Integer = 2
  Public Const DM_COLLATE As Integer = &H8000S
  Public Const DM_IN_BUFFER As Integer = 8
  Public Const DM_OUT_BUFFER As Integer = 2
  Public Const DM_ORIENTATION As Short = &H1S
  Public Const DM_PAPERSIZE As Short = &H2S
  Public Const DM_PAPERLENGTH As Short = &H4S
  Public Const DM_PAPERWIDTH As Short = &H8S
  Public Const DM_SCALE As Short = &H10S
  Public Const DM_COPIES As Short = &H100S
  Public Const DM_DEFAULTSOURCE As Short = &H200S
  Public Const DM_PRINTQUALITY As Short = &H400S
  Public Const DM_COLOR As Short = &H800S
  Public Const DM_DUPLEX As Short = &H1000S
  Public Const DM_YRESOLUTION As Short = &H2000S
  Public Const DM_TTOPTION As Short = &H4000S
  Public Const PD_ALLPAGES As Short = &H0S
  Public Const PD_COLLATE As Short = &H10S
  Public Const PD_DISABLEPRINTTOFILE As Integer = &H80000
  Public Const PD_ENABLEPRINTHOOK As Short = &H1000S
  Public Const PD_ENABLEPRINTTEMPLATE As Short = &H4000S
  Public Const PD_ENABLEPRINTTEMPLATEHANDLE As Integer = &H10000
  Public Const PD_ENABLESETUPHOOK As Short = &H2000S
  Public Const PD_ENABLESETUPTEMPLATE As Short = &H8000S
  Public Const PD_ENABLESETUPTEMPLATEHANDLE As Integer = &H20000
  Public Const PD_HIDEPRINTTOFILE As Integer = &H100000
  Public Const PD_NONETWORKBUTTON As Integer = &H200000
  Public Const PD_NOPAGENUMS As Short = &H8S
  Public Const PD_NOSELECTION As Short = &H4S
  Public Const PD_NOWARNING As Short = &H80S
  Public Const PD_PAGENUMS As Short = &H2S
  Public Const PD_PRINTSETUP As Short = &H40S
  Public Const PD_PRINTTOFILE As Short = &H20S
  Public Const PD_RETURNDC As Short = &H100S
  Public Const PD_RETURNDEFAULT As Short = &H400S
  Public Const PD_RETURNIC As Short = &H200S
  Public Const PD_SELECTION As Short = &H1S
  Public Const PD_SHOWHELP As Short = &H800S
  Public Const PD_USEDEVMODECOPIES As Integer = &H40000
  Public Const PD_USEDEVMODECOPIESANDCOLLATE As Integer = &H40000

  Public Shared Function PrinterExist(ByVal strPrinter As String) As Boolean
    Dim hPrinter As IntPtr
    Dim pd As PRINTER_DEFAULTS
    Dim nRet As Integer = 0
    Try
      If strPrinter.Trim = "" Then Return False

      nRet = OpenPrinter(strPrinter, hPrinter, pd)
      If (nRet = 0) Or (hPrinter.ToInt32 = 0) Then
        'Throw New Exception("BN__STD - PrinterExist: accesso alla stampante '" & strPrinter & "' negato ")
        Return False
      End If
      Return True
    Catch ex As Exception
      'stampante inesistente
      'Throw New Exception("BN__STD - PrinterExist: " & ex.Message)
      Return False
    Finally
      If (hPrinter.ToInt32 <> 0) Then
        Try
          ClosePrinter(hPrinter)
        Catch ex As Exception
        End Try
      End If
    End Try
  End Function

  Public Shared Function PrinterExistNew(ByRef strPrinter As String) As Boolean
    'in terminal server la stampante locale viene rimappata ad ogni sessione accodando la parola " (Rendirizzamento 1)" o latro numero, ca cambia ad ogni sessione
    'in bus, nem reg. di business, viene memorizzato il nome senza la parola " (Rendirizzamento 2)"

    Dim colInstalledPrinters As ManagementObjectSearcher = Nothing
    Try
      If strPrinter.Trim = "" Then Return False

      strPrinter = strPrinter.Replace("\", "\\")
      If CLN__STD.IsTerminalServer Then
        colInstalledPrinters = New ManagementObjectSearcher("Select Name, Copies, Duplex, Color, Orientation from Win32_PrinterConfiguration WHERE Name like " & CStrSQL(strPrinter & "%"))
      Else
        colInstalledPrinters = New ManagementObjectSearcher("Select Name, Copies, Duplex, Color, Orientation from Win32_PrinterConfiguration WHERE Name = " & CStrSQL(strPrinter))
      End If
      Dim results1 As ManagementObjectCollection = colInstalledPrinters.[Get]()

      If results1.Count > 0 Then
        For Each obj As ManagementObject In results1
          'il nome della stampante deve essere uguale a quella passata, oppure, se terminal server, che inizi cin come stampante + " (Rendirizzament
          If NTSCStr(obj("Name")).ToUpper = strPrinter.ToUpper Or NTSCStr(obj("Name")).ToUpper.StartsWith(strPrinter.ToUpper & " (") Then
            strPrinter = NTSCStr(obj("Name"))
          End If
        Next
        Return True
      End If

      Return False
    Catch ex As Exception
      'stampante inesistente
      'Throw New Exception("BN__STD - PrinterExist: " & ex.Message)
      Return False
    End Try
  End Function

  Private Shared Function CreatePRINTDLG() As PRINTDLG_TYPE
    Dim stcPD As New PRINTDLG_TYPE()
    stcPD.lStructSize = Marshal.SizeOf(GetType(PRINTDLG_TYPE))
    stcPD.hwndOwner = IntPtr.Zero
    stcPD.hDevMode = IntPtr.Zero
    stcPD.hDevNames = IntPtr.Zero
    stcPD.Flags = 0
    stcPD.hwndOwner = IntPtr.Zero
    stcPD.hDC = IntPtr.Zero
    stcPD.nFromPage = 1
    stcPD.nToPage = 1
    stcPD.nMinPage = 0
    stcPD.nMaxPage = 9999
    stcPD.nCopies = 1
    stcPD.hInstance = IntPtr.Zero
    stcPD.lCustData = IntPtr.Zero
    stcPD.lpfnPrintHook = IntPtr.Zero
    stcPD.lpfnSetupHook = IntPtr.Zero
    stcPD.lpPrintTemplateName = Nothing
    stcPD.lpSetupTemplateName = Nothing
    stcPD.hPrintTemplate = IntPtr.Zero
    stcPD.hSetupTemplate = IntPtr.Zero
    Return stcPD
  End Function

  Public Shared Function GetPrinterProperties(ByRef strPrinter As String, _
                                              ByRef strDriverName As String, ByRef strPortName As String, _
                                              ByRef nCopies As Short, ByRef nDuplex As Short, _
                                              ByRef nPaperSize As Short, ByRef nPaperBin As Short, _
                                              ByRef nColor As Short, ByRef nOrientation As Short, ByRef hDevmodePrt As IntPtr) As Boolean

    'Dim hPrinter As IntPtr
    'Dim pd As PRINTER_DEFAULTS
    'Dim pinfo As PRINTER_INFO_2 = New PRINTER_INFO_2
    'Dim dm As New DEVMODE_TYPEANSI
    'Dim nRet As Integer = 0
    'Dim nBytesNeeded As Integer
    'Dim ptrPrinterInfo As IntPtr
    'Dim sizeOfDevMode As Integer = 0
    'Dim nJunk As Int32
    'Dim iparg As IntPtr

    Try
      '--------------------------------
      'IMPOSTAZIONI STANDARD:
      nCopies = 1
      nDuplex = 0
      nPaperSize = 9
      nPaperBin = 15
      nColor = 2
      nOrientation = 1
      '--------------------------------
      'da prove fatte ho visto che sia che la stampante sia in rete, sia che sia in locale, sia che sia in stampante TCP/IP 
      'se gli passo come porta sempre LPT1: se la aggiusta lui.
      strDriverName = "winspool" ' VB6 metteva sempre "winspool"
      If (Environment.OSVersion.Version.Major + Environment.OSVersion.Version.Minor) >= 6.2 Then
        'Environment.OSVersion
        'da windows 8 in poi da problemi settato con LPT1
        'con multireport settato e ShowPrinterDialog = 0 non stampa nulla se la porta non è effettivamente la LPT1!!! 
        'stesso problema se si setta printername da reg. di business e si stampa a video 2 report con multireport: da stampa a video a su carta non stampava nulla
        strPortName = ""  '"LPT1:" con LPT1, 
      Else
        strPortName = "LPT1:"
      End If

      ''-----------------------------------------------
      ''con questo sistema per le stampanti in rete può far aspettare anche 1 minuto!!!! se non si usa la funzione 'GetPrinterProperties'
      'Dim printer As New System.Drawing.Printing.PrintDocument
      'strPrinter = printer.PrinterSettings.PrinterName   'ottengo la stampante predefinita
      'nCopies = printer.PrinterSettings.Copies
      'nDuplex = CType(printer.PrinterSettings.Duplex, Short)
      'nPaperSize = CType(printer.DefaultPageSettings.PaperSize.Kind, Short)
      'nPaperBin = CType(printer.DefaultPageSettings.PaperSource.Kind, Short)
      'nColor = CType(IIf(printer.DefaultPageSettings.Color, 2, 1), Short)
      'nOrientation = CType(IIf(printer.DefaultPageSettings.Landscape, 2, 1), Short)
      'strDriverName = "winspool" ' VB6 metteva sempre "winspool"
      'strPortName = ""
      'printer.Dispose()

      ''-----------------------------------------------
      ''OCCHIO: IL SISTEMA SOTTO PER ORA RESTITUISCE SEMPRE LE IMPOSTAZIONI DELLA STAMPANTE PREDEFINITA !!!!!!!
      'ShowPrinterdialog(0, strPrinter, strDriverName, strPortName, 0, nCopies, _
      '                  nOrientation, 0, nDuplex, 0, 0, 0, nColor, _
      '                  nPaperSize, 0, 0, nPaperBin, False, False, True)


      ''-----------------------------------------------
      'con questo sistema ottengo il driver e la porta corretta, 
      'per ora commentato, visto che funziona correttamente anche passando sempre come driver 'winspool' e come porta 'lpt1:'
      'se la stampante è in rete devo duplicare le '\', visto che il altrimenti da il messaggio 'query non valida'
      'Dim searcher As New ManagementObjectSearcher("Select * From Win32_Printer WHERE Name = " & CStrSQL(strPrinter.Replace(strSep, "\\")))
      'Dim results As ManagementObjectCollection = searcher.[Get]()
      'For Each obj As ManagementObject In results
      '  If NTSCStr(obj("Name")) = strPrinter Then
      '    strDriverName = NTSCStr(obj("DriverName"))
      '    strPortName = NTSCStr(obj("PortName"))
      '    Exit For
      '  End If
      'Next
      Dim colInstalledPrinters As New ManagementObjectSearcher("Select Name, Copies, Duplex, Color, Orientation from Win32_PrinterConfiguration WHERE Name = " & CStrSQL(strPrinter.Replace("\", "\\")))
      Dim results1 As ManagementObjectCollection = colInstalledPrinters.[Get]()
      For Each objPrinter As ManagementObject In results1
        If NTSCStr(objPrinter("Name")) = strPrinter Then
          nCopies = CType(objPrinter("Copies"), Short)
          nDuplex = CType(objPrinter("Duplex"), Short)
          'Dim str As String = NTSCStr(objPrinter("PaperSize"))     'non passato come in vb6
          nPaperSize = 9 'CType(objPrinter("PaperSize"), Short)     'non passato come in vb6: default A4
          nPaperBin = 15 'CType(objPrinter(""), Short)    'non passato come in vb6: default automatico
          nColor = CType(objPrinter("Color"), Short)
          nOrientation = CType(objPrinter("Orientation"), Short)
          Exit For
        End If
      Next

      ''-----------------------------------------------
      'VECCHIO SISTEMA CHE A VOLTE DAVA ERRORE IN FASE DI APERTURA STAMPANTE ...
      ''apro la stampante
      'pd.DesiredAccess = PRINTER_ALL_ACCESS
      'nRet = OpenPrinter(strPrinter, hPrinter, pd)
      'If (nRet = 0) Or (hPrinter.ToInt32 = 0) Then
      '  Throw New Exception("BN__STD - GetPrinterProperties: accesso alla stampante '" & strPrinter & "' negato ")
      'End If

      'GetPrinter(hPrinter, 2, IntPtr.Zero, 0, nBytesNeeded)
      'ptrPrinterInfo = Marshal.AllocCoTaskMem(nBytesNeeded + 100)

      'nRet = CType(GetPrinter(hPrinter, 2, ptrPrinterInfo, nBytesNeeded, nJunk), Integer)
      'If (nRet = 0) Then
      '  Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile determinare il driver e la porta della stampante '" & strPrinter & "'")
      'End If

      'pinfo = CType(Marshal.PtrToStructure(ptrPrinterInfo, pinfo.GetType), PRINTER_INFO_2)
      'strDriverName = pinfo.pDriverName ' VB6 metteva sempre "winspool"
      'strPortName = pinfo.pPortName

      'If hDevmodePrt.Equals(IntPtr.Zero) Then
      '  'determino la dimensione del buffer per la lettura del documento
      '  nRet = DocumentProperties(IntPtr.Zero, hPrinter, strPrinter, IntPtr.Zero, IntPtr.Zero, 0)
      '  If (nRet < 0) Then
      '    Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile determinare la struttura del DEVMODE della stampante '" & strPrinter & "'")
      '  End If

      '  'chiedo i dato relativi al documento
      '  iparg = Marshal.AllocCoTaskMem(nRet + 100)

      '  'leggo le impostazioni
      '  nRet = DocumentProperties(IntPtr.Zero, hPrinter, strPrinter, iparg, IntPtr.Zero, DM_OUT_BUFFER)
      '  If (nRet < 0) Then
      '    Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile determinare il DEVMODE della stampante '" & strPrinter & "'")
      '  End If
      'Else
      '  iparg = hDevmodePrt
      'End If

      'dm = CType(Marshal.PtrToStructure(iparg, dm.GetType), DEVMODE_TYPEANSI)

      'nCopies = dm.dmCopies
      'nDuplex = dm.dmDuplex
      'nPaperSize = dm.dmPaperSize
      'nPaperBin = dm.dmDefaultSource
      'nColor = dm.dmColor
      'nOrientation = dm.dmOrientation

      'ClosePrinter(hPrinter)

      'SetPrinterProp(strPrinter)

      Return True

    Catch ex As Exception
      'in alcuni casi 'non si è capito come e perchè' da errore di 'out of memory' e cercando su internet
      'sembra non ci sia soluzione:
      'ignoro l'errore ed imposto le proprietà standard!!!!!
      'Throw New Exception("Errore in BN__STD.GetPrinterProperties su stampante '" & strPrinter & "': " & ex.Message)
      'Throw ex
      'Finally
      '  Try
      '    If (hPrinter.ToInt32 <> 0) Then ClosePrinter(hPrinter)
      '  Catch exq As Exception
      '  End Try
    End Try
  End Function

  'Public Shared Function SetPrinterProp(ByVal strPrinter As String) As Boolean
  '  Dim hPrinter As IntPtr
  '  Dim pd As PRINTER_DEFAULTS
  '  ' Dim pinfo As PRINTER_INFO_2 = New PRINTER_INFO_2
  '  Dim dm As New DEVMODE_TYPE
  '  Dim nRet As Integer = 0
  '  Dim sizeOfDevMode As Integer = 0

  '  Dim iparg As IntPtr

  '  Try
  '    'apro la stampante
  '    pd.DesiredAccess = PRINTER_ALL_ACCESS
  '    nRet = OpenPrinter(strPrinter, hPrinter, pd)
  '    If (nRet = 0) Or (hPrinter.ToInt32 = 0) Then
  '      Throw New Exception("BN__STD - GetPrinterProperties: accesso alla stampante '" & strPrinter & "' negato ")
  '    End If

  '    'determino la dimensione del buffer per la lettura del documento
  '    nRet = DocumentProperties(IntPtr.Zero, hPrinter, strPrinter, IntPtr.Zero, IntPtr.Zero, 0)
  '    If (nRet < 0) Then
  '      Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile determinare la struttura del DEVMODE della stampante '" & strPrinter & "'")
  '    End If

  '    'chiedo i dato relativi al documento
  '    iparg = Marshal.AllocCoTaskMem(nRet)

  '    'leggo le impostazioni
  '    nRet = DocumentProperties(IntPtr.Zero, hPrinter, strPrinter, iparg, IntPtr.Zero, DM_OUT_BUFFER)
  '    If (nRet < 0) Then
  '      Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile determinare il DEVMODE della stampante '" & strPrinter & "'")
  '    End If

  '    dm = CType(Marshal.PtrToStructure(iparg, dm.GetType), DEVMODE_TYPE)

  '    dm.dmCopies = 55
  '    dm.dmFields = DM_COPIES

  '    'setto le impostazioni
  '    'Marshal.StructureToPtr(dm, iparg, False)
  '    nRet = DocumentProperties(IntPtr.Zero, hPrinter, strPrinter, iparg, iparg, DM_IN_BUFFER Or DM_OUT_BUFFER)

  '    If (nRet < 0) Then
  '      Throw New Exception("BN__STD - GetPrinterProperties: non è stato possibile settare il DEVMODE della stampante '" & strPrinter & "'")
  '    End If

  '    ClosePrinter(hPrinter)

  '  Catch ex As Exception
  '    If (hPrinter.ToInt32 <> 0) Then ClosePrinter(hPrinter)
  '    Throw ex
  '  End Try
  'End Function

  Public Shared Function ShowPrinterdialog(ByVal lHandleOwner As Integer, ByRef strRPTPrinterName As String, _
                                            ByRef strRPTDriverName As String, ByRef strRPTPortName As String, _
                                            ByRef lRPTFields As Integer, ByRef nRPTCopies As Short, _
                                            ByRef nRPTOrientation As Short, ByRef nRPTCollate As Short, _
                                            ByRef nRPTDuplex As Short, ByRef nRPTScale As Short, _
                                            ByRef nRPTPrintQuality As Short, ByRef nRPTYResolution As Short, _
                                            ByRef nRPTColor As Short, ByRef nRPTPaperSize As Short, _
                                            ByRef nRPTPaperLength As Short, ByRef nRPTPaperWidth As Short, _
                                            ByRef nRPTPaperBin As Short, ByVal bSetup As Boolean, _
                                            Optional ByVal bRPTForzaCopies As Boolean = False, _
                                            Optional ByVal bGetDefaultPrinter As Boolean = False) As Boolean
    'bSetup = TRUE: leggo la stampante di default all'avvio di Busnet o da crpe se non impostata nessuna stampante all'avvio di bus o da voce di menu 'imposta stampante'
    '               se = false faccio vedere la form di scelta stampante con impostata la stampante settata in business
    'bGetDefaultPrinter = true: se devo far vedere la form di imposta stampante 
    Dim stcPD As PRINTDLG_TYPE
    Dim stcOutDM As New DEVMODE_TYPE
    Dim lRet As Integer
    Dim lpDevNames As IntPtr
    Dim lpDevMode As IntPtr
    Dim lTmpFields As Integer
    Dim lHandleDevMode As IntPtr
    Dim devOffset As Integer = 0

    Try
      'con alcuni diver della stampante il DEVMODE non viene compilato correttamente e qundi quando la CRPE
      'deve visualizzare la stampante non passa tutte le informazioni necessarie al driver per cui la 
      'showprinterdialog non imposta priprietà come orientamento, copie, cassetto, ecc.
      'se settata l'opzione di reg. di business invece di far vedere la showprinterdialog delle api di windows
      'uso il componente nativo NET (sperando che con i service pack abbiano risolto i problemi di lentezza 
      'del'apertura della form se la stampante è in rete!!!!!)
      If UsePrinterDialogNet Then

        If bGetDefaultPrinter Then
          'non devo far apparire la form della stampante e prendo la stampante predefinita di windows
          Try
            Dim prnSetting As New Printing.PrinterSettings
            strRPTPrinterName = prnSetting.PrinterName
            strRPTPortName = prnSetting.PrintFileName 'senza questa riga se si stampava direttamente su carta e, prima di eseguire la stampa, si cambiava stampante con una di rete non stampava nulla e non dava nessun messaggio
            nRPTCopies = prnSetting.Copies
            nRPTCollate = prnSetting.Collate
            nRPTDuplex = prnSetting.Duplex
            nRPTPaperSize = prnSetting.DefaultPageSettings.PaperSize.RawKind
            nRPTPaperBin = prnSetting.DefaultPageSettings.PaperSource.RawKind
            nRPTColor = NTSCInt(IIf(prnSetting.DefaultPageSettings.Color, 2, 1))
            nRPTOrientation = NTSCInt(IIf(prnSetting.DefaultPageSettings.Landscape, 2, 1))
          Catch ex As Exception
            'se da errore è perchè nessuna stampante è stata installata o settata come predefinita
            strRPTPrinterName = ""
            strRPTPortName = ""
            nRPTCopies = 0
            nRPTCollate = 0
            nRPTDuplex = 0
            nRPTPaperSize = 0
            nRPTPaperBin = 0
            nRPTColor = 0
            nRPTOrientation = 0
            Return False
          End Try
        Else
          'faccio apparire la form di selez. della stampante (da menu->imposta stampante)
          Dim oDlg As New PrintDialog
          'oDlg.PrintToFile = False
          oDlg.AllowPrintToFile = False
          oDlg.AllowSelection = False
          oDlg.AllowSomePages = False
          oDlg.ShowNetwork = True
          oDlg.UseEXDialog = True
          oDlg.ShowHelp = False

          If nRPTCopies < 0 Then nRPTCopies = 0
          oDlg.PrinterSettings.PrinterName = strRPTPrinterName
          oDlg.PrinterSettings.Copies = nRPTCopies
          oDlg.PrinterSettings.Collate = nRPTCollate
          oDlg.PrinterSettings.Duplex = nRPTDuplex

          If nRPTPaperSize <> 0 Then
            If oDlg.PrinterSettings.DefaultPageSettings.PaperSize.RawKind <> nRPTPaperSize Then
              For Each PSize As System.Drawing.Printing.PaperSize In oDlg.PrinterSettings.DefaultPageSettings.PrinterSettings.PaperSizes
                If PSize.RawKind = nRPTPaperSize Then
                  oDlg.PrinterSettings.DefaultPageSettings.PaperSize = PSize
                  Exit For
                End If
              Next
            End If
          End If

          If nRPTPaperBin <> 0 Then
            If oDlg.PrinterSettings.DefaultPageSettings.PaperSource.RawKind <> nRPTPaperBin Then
              For Each PSource As System.Drawing.Printing.PaperSource In oDlg.PrinterSettings.DefaultPageSettings.PrinterSettings.PaperSources
                If PSource.RawKind = nRPTPaperBin Then
                  oDlg.PrinterSettings.DefaultPageSettings.PaperSource = PSource
                  Exit For
                End If
              Next
            End If
          End If

          oDlg.PrinterSettings.DefaultPageSettings.Color = CBool(IIf(nRPTColor = 2, True, False))
          oDlg.PrinterSettings.DefaultPageSettings.Landscape = CBool(IIf(nRPTOrientation = 2, True, False))

          'If oDlg.ShowDialog(lHandleOwner) = DialogResult.Cancel Then Return False
          If oDlg.ShowDialog() = DialogResult.Cancel Then Return False

          'dati ritornati dalla scelta stampante
          strRPTPrinterName = oDlg.PrinterSettings.PrinterName

          Try
            'con alcuni driver, tipo il driver EpsonNet Print, qui da errore perchè l'oggetto  
            'oDlg.PrinterSettings.PrintFileName avvisa di 'formato di stringa non corretto'
            'es di porta impostato con EpsonNet Print: '172.20.2.31:WF-5620 SERIES'
            'se da errore cerco di ottenere il driver
            strRPTPortName = oDlg.PrinterSettings.PrintFileName 'senza questa riga se si stampava direttamente su carta e, prima di eseguire la stampa, si cambiava stampante con una di rete non stampava nulla e non dava nessun messaggio
          Catch ex As Exception
            Dim searcher As New ManagementObjectSearcher("SELECT * from Win32_Printer WHERE Name LIKE " & CStrSQL(strRPTPrinterName))
            Dim coll As ManagementObjectCollection = searcher.Get()
            For Each printer As ManagementObject In coll
              strRPTPortName = NTSCStr(printer.Properties("PortName").Value)
              Exit For
            Next
          End Try
          nRPTCopies = oDlg.PrinterSettings.Copies
          nRPTCollate = oDlg.PrinterSettings.Collate
          nRPTDuplex = oDlg.PrinterSettings.Duplex
          nRPTPaperSize = oDlg.PrinterSettings.DefaultPageSettings.PaperSize.RawKind
          nRPTPaperBin = oDlg.PrinterSettings.DefaultPageSettings.PaperSource.RawKind
          nRPTColor = NTSCInt(IIf(oDlg.PrinterSettings.DefaultPageSettings.Color, 2, 1))
          nRPTOrientation = NTSCInt(IIf(oDlg.PrinterSettings.DefaultPageSettings.Landscape, 2, 1))
        End If

        Return True
      End If    'If UsePrinterDialogNet Then

      stcPD = CreatePRINTDLG()
      'Crea l'handle ad DEVMODE
      If bGetDefaultPrinter Then
        stcPD.Flags = PD_RETURNDEFAULT Or PD_ALLPAGES Or PD_USEDEVMODECOPIESANDCOLLATE Or PD_DISABLEPRINTTOFILE Or PD_NOSELECTION Or PD_SHOWHELP Or PD_PRINTSETUP
      Else
        lHandleDevMode = GetHandleDevMode(strRPTPrinterName, lRPTFields, nRPTCopies, nRPTOrientation, nRPTCollate, _
                                          nRPTDuplex, nRPTScale, nRPTPrintQuality, nRPTYResolution, nRPTColor, _
                                          nRPTPaperSize, nRPTPaperLength, nRPTPaperWidth, nRPTPaperBin, bSetup)
        stcPD.hDevMode = lHandleDevMode
        'se non setto la riga sotto fa vedere la dialog vecchia (quella dove le stampanti sono contenute in un combobox)
        'se però la setto e nel devmode non ho dichiarato 'Public dmPanningHeight As Integer' non sente le impostazioni 
        'della stampante passate da Business (e prende sempre la stampante predefinita di windows)
        stcPD.hwndOwner = CType(lHandleOwner, IntPtr)

        If bSetup Then
          stcPD.Flags = PD_PRINTSETUP Or PD_ALLPAGES Or PD_USEDEVMODECOPIESANDCOLLATE Or PD_DISABLEPRINTTOFILE Or PD_NOSELECTION Or PD_SHOWHELP
        Else
          stcPD.Flags = PD_ALLPAGES Or PD_USEDEVMODECOPIESANDCOLLATE Or PD_NOPAGENUMS Or PD_NOSELECTION Or PD_HIDEPRINTTOFILE
          'stcPD.Flags = PD_ALLPAGES Or PD_NOPAGENUMS Or PD_NOSELECTION Or PD_HIDEPRINTTOFILE
          'stcPD.nCopies = 46 '................ se setto stcPD.hDevMode = lHandleDevMode e PD_USEDEVMODECOPIESANDCOLLATE non propone più le copie
        End If

        If Not CBool(lRPTFields And DM_COPIES) Then
          stcPD.nCopies = nRPTCopies
        End If
        If bRPTForzaCopies And nRPTCopies > 0 Then
          stcPD.nCopies = nRPTCopies
        End If
      End If

      'Crea l'handle ad DEVNAMES
      'stcPD.hDevNames = GetHandleDevName(strRPTPrinterName, strRPTDriverName, strRPTPortName)
      lRet = PrintDlg(stcPD)
      '***    handle return from PrintDlg API function
      Select Case lRet
        Case 0
          Return False
        Case 1
          'LEGGE NUOVA STAMPANTE, PORTA E DRIVER
          lpDevNames = GlobalLock(New HandleRef(stcPD, stcPD.hDevNames))

          devOffset = Marshal.SystemDefaultCharSize * Marshal.ReadInt16(CType((CInt(lpDevNames) + 2), IntPtr))
          strRPTPrinterName = Marshal.PtrToStringAuto(CType((CInt(lpDevNames) + devOffset), IntPtr))
          devOffset = Marshal.SystemDefaultCharSize * Marshal.ReadInt16(CType((CInt(lpDevNames) + 0), IntPtr))
          strRPTDriverName = Marshal.PtrToStringAuto(CType((CInt(lpDevNames) + devOffset), IntPtr))
          devOffset = Marshal.SystemDefaultCharSize * Marshal.ReadInt16(CType((CInt(lpDevNames) + 4), IntPtr))
          strRPTPortName = Marshal.PtrToStringAuto(CType((CInt(lpDevNames) + devOffset), IntPtr))

          lpDevMode = GlobalLock(New HandleRef(stcPD, stcPD.hDevMode))
          stcOutDM = CType(Marshal.PtrToStructure(lpDevMode, stcOutDM.GetType), DEVMODE_TYPE)

          GlobalUnlock(New HandleRef(stcPD, lpDevNames))
          GlobalUnlock(New HandleRef(stcPD, lpDevMode))
          GlobalFree(New HandleRef(stcPD, stcPD.hDevNames))
          GlobalFree(New HandleRef(stcPD, lpDevMode))

          'se stcOutDM = nothng vuol dire che ha dato un errore in lettura impostazioni dal driver della stampante. ignoro l'errore come in vb6
          If Not stcOutDM Is Nothing Then
            'Salva le nuove impostazioni nelle variabili locali a CRPE
            '---------------------------------------------------------
            lTmpFields = 0
            'Copie
            If CBool(stcOutDM.dmFields And DM_COPIES) Then
              If CBool(stcPD.Flags And PD_USEDEVMODECOPIESANDCOLLATE) Then
                nRPTCopies = stcOutDM.dmCopies
              Else
                nRPTCopies = stcPD.nCopies
              End If
              lTmpFields = lTmpFields + DM_COPIES
            Else
              'La stampante con gestisce le copie multiple
              'il n. di copie è conservato nella stc PRINTDLG
              nRPTCopies = stcPD.nCopies
            End If
            'Orientamento
            If CBool(stcOutDM.dmFields And DM_ORIENTATION) Then
              nRPTOrientation = stcOutDM.dmOrientation
              lTmpFields = lTmpFields + DM_ORIENTATION
            End If
            'Stampa fronte e retro
            If CBool(stcOutDM.dmFields And DM_DUPLEX) Then
              nRPTDuplex = stcOutDM.dmDuplex
              lTmpFields = lTmpFields + DM_DUPLEX
            End If
            'Scala
            If CBool(stcOutDM.dmFields And DM_SCALE) Then
              nRPTScale = stcOutDM.dmScale
              lTmpFields = lTmpFields + DM_SCALE
            End If
            'Qualità di stampa
            If CBool(stcOutDM.dmFields And DM_PRINTQUALITY) Then
              nRPTPrintQuality = stcOutDM.dmPrintQuality
              lTmpFields = lTmpFields + DM_PRINTQUALITY
            End If
            'Risoluzione verticale
            If CBool(stcOutDM.dmFields And DM_YRESOLUTION) Then
              nRPTYResolution = stcOutDM.dmYResolution
              lTmpFields = lTmpFields + DM_YRESOLUTION
            End If
            'Colore
            If CBool(stcOutDM.dmFields And DM_COLOR) Then
              nRPTColor = stcOutDM.dmColor
              lTmpFields = lTmpFields + DM_COLOR
            End If
            'Foglio
            If CBool(stcOutDM.dmFields And DM_PAPERSIZE) Then
              nRPTPaperSize = stcOutDM.dmPaperSize
              lTmpFields = lTmpFields + DM_PAPERSIZE
            End If
            'Altezza foglio
            If CBool(stcOutDM.dmFields And DM_PAPERLENGTH) Then
              nRPTPaperLength = stcOutDM.dmPaperLength
              lTmpFields = lTmpFields + DM_PAPERLENGTH
            End If
            'Larghezza foglio
            If CBool(stcOutDM.dmFields And DM_PAPERWIDTH) Then
              nRPTPaperWidth = stcOutDM.dmPaperWidth
              lTmpFields = lTmpFields + DM_PAPERWIDTH
            End If
            'Cassetto
            If CBool(stcOutDM.dmFields And DM_DEFAULTSOURCE) Then
              nRPTPaperBin = stcOutDM.dmDefaultSource
              lTmpFields = (lTmpFields + DM_DEFAULTSOURCE)
            End If
          End If    'If Not stcOutDM Is Nothing Then

          'Campi
          lRPTFields = lTmpFields

          Return True
      End Select
      Return False
    Catch ex As Exception
      Throw New Exception(ex.Message & ex.StackTrace)
    End Try
  End Function

  Public Shared Function GetHandleDevMode(ByVal strRPTPrinterName As String, ByVal lRPTFields As Integer, ByVal nRPTCopies As Short, _
                                        ByVal nRPTOrientation As Short, ByVal nRPTCollate As Short, ByVal nRPTDuplex As Short, _
                                        ByVal nRPTScale As Short, ByVal nRPTPrintQuality As Short, ByVal nRPTYResolution As Short, _
                                        ByVal nRPTColor As Short, ByVal nRPTPaperSize As Short, ByVal nRPTPaperLength As Short, _
                                        ByVal nRPTPaperWidth As Short, ByVal nRPTPaperBin As Short, ByVal bSetup As Boolean) As IntPtr
    Dim stcDM As New DEVMODE_TYPE
    Dim hDevMode As IntPtr

    Try
      With stcDM
        .dmBitsPerPel = 0
        .dmCollate = 0
        .dmColor = 0
        .dmCopies = 0
        .dmDefaultSource = 0
        .dmDeviceName = ""
        .dmDitherType = 0
        .dmDisplayFlags = 0
        .dmDisplayFrequency = 0
        .dmDriverExtra = 0
        .dmICMIntent = 0
        .dmICMMethod = 0
        .dmMediaType = 0
        .dmDriverVersion = 0
        .dmDuplex = 0
        .dmFields = lRPTFields
        .dmFormName = ""
        .dmOrientation = 0
        .dmPaperLength = 0
        .dmPaperSize = 0
        .dmPaperWidth = 0
        .dmPelsHeight = 0
        .dmPelsWidth = 0
        .dmPrintQuality = 0
        .dmReserved1 = 0
        .dmReserved2 = 0
        .dmScale = 0
        .dmSize = 0
        .dmSpecVersion = 0
        .dmTTOption = 0
        .dmUnusedPadding = 0
        .dmYResolution = 0
      End With

      stcDM.dmSize = CType(Marshal.SizeOf(stcDM), Short)
      stcDM.dmDeviceName = strRPTPrinterName & Chr(0)

      stcDM.dmFields = lRPTFields
      If CType(lRPTFields And DM_COPIES, Boolean) And nRPTCopies > 0 Then stcDM.dmCopies = nRPTCopies
      If CType(lRPTFields And DM_ORIENTATION, Boolean) Then stcDM.dmOrientation = nRPTOrientation
      If CType(lRPTFields And DM_COLLATE, Boolean) Then stcDM.dmCollate = nRPTCollate
      If CType(lRPTFields And DM_DUPLEX, Boolean) Then stcDM.dmDuplex = nRPTDuplex
      If CType(lRPTFields And DM_SCALE, Boolean) Then stcDM.dmScale = nRPTScale
      If CType(lRPTFields And DM_PRINTQUALITY, Boolean) Then stcDM.dmPrintQuality = nRPTPrintQuality
      If CType(lRPTFields And DM_YRESOLUTION, Boolean) Then stcDM.dmYResolution = nRPTYResolution
      If CType(lRPTFields And DM_COLOR, Boolean) Then stcDM.dmColor = nRPTColor
      If CType(lRPTFields And DM_PAPERSIZE, Boolean) Then stcDM.dmPaperSize = nRPTPaperSize
      If CType(lRPTFields And DM_PAPERLENGTH, Boolean) Then stcDM.dmPaperLength = nRPTPaperLength
      If CType(lRPTFields And DM_PAPERWIDTH, Boolean) Then stcDM.dmPaperWidth = nRPTPaperWidth
      If CType(lRPTFields And DM_DEFAULTSOURCE, Boolean) Then stcDM.dmDefaultSource = nRPTPaperBin

      hDevMode = Marshal.AllocHGlobal(Marshal.SizeOf(stcDM))
      Marshal.StructureToPtr(stcDM, hDevMode, False)
      Return hDevMode

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Shared Function GetHandleDevName(ByVal strRPTPrinterName As String, ByVal strRPTDriverName As String, ByVal strRPTPortName As String) As IntPtr
    Dim stcDN As DEVNAMES_TYPE
    Dim lhDevName As IntPtr
    Dim strTmp As String = ""
    strRPTDriverName = "winspool"

    'Dim cTmp(100) As Char
    'strTmp = strRPTDriverName & strRPTPrinterName & strRPTDriverName
    'cTmp = CType(strRPTDriverName & Chr(0) & strRPTPrinterName & Chr(0) & strRPTPortName & Chr(0) & Space(100 - Len(strTmp)), Char())

    Try
      With stcDN
        .wDriverOffset = 8
        .wDeviceOffset = CType(.wDriverOffset + 1 + Len(strRPTDriverName), Short)
        .wOutputOffset = CType(.wDeviceOffset + 1 + Len(strRPTPortName), Short)
        .wDefault = 0
        .extra = strRPTDriverName & Chr(0) & strRPTPrinterName & Chr(0) & strRPTPortName & Chr(0)
      End With

      lhDevName = Marshal.AllocHGlobal(Marshal.SizeOf(stcDN))
      Marshal.StructureToPtr(stcDN, lhDevName, True)
      Return lhDevName

    Catch ex As Exception
      Throw ex
    End Try
  End Function
#End Region

  Public Class ThMsg
    'costanti per messaggi da throwremoteevent per gestiscieventientity
    Public Const MSG_ERROR As String = ""
    Public Const MSG_INFO As String = "MSG_INFO"
    Public Const MSG_EXCLAMATION As String = "MSG_EXCLAMATION"
    Public Const MSG_OKCANC As String = "MSG_OKCANC"
    Public Const MSG_CANCOK As String = "MSG_CANCOK"
    Public Const MSG_YESNO As String = "MSG_YESNO"
    Public Const MSG_NOYES As String = "MSG_NOYES"
    Public Const MSG_YESNOCANC As String = "MSG_YESNOCANC"
    Public Const MSG_NOYESCANC As String = "MSG_NOYESCANC"
    Public Const MSG_CANCYESNO As String = "MSG_CANCYESNO"
    Public Const INPUTBOX As String = "INPUTBOX"
    Public Const INPUTBOXNUM As String = "INPUTBOXNUM"
    Public Const INPUTBOXDATA As String = "INPUTBOXDATA"
    Public Const INPUTPWD As String = "INPUTPWD"

    Public Const MSG_ABORTRETRYIGNORE As String = "MSG_ABORTIGNORERETRY"
    Public Const MSG_IGNORERETRAYABORT As String = "MSG_IGNORERETRAYABORT"
    Public Const MSG_RETRAYIGNOREABORT As String = "MSG_RETRAYIGNOREABORT"
    Public Const MSG_RETRYCANCEL As String = "MSG_RETRYCANCEL"
    Public Const MSG_CANCELRETRY As String = "MSG_CANCELRETRY"

    Public Const RETVALUE_OK As String = "OK"
    Public Const RETVALUE_YES As String = "YES"
    Public Const RETVALUE_NO As String = "NO"
    Public Const RETVALUE_CANCEL As String = "CANCEL"
    Public Const RETVALUE_RETRAY As String = "RETRAY"
    Public Const RETVALUE_IGNORE As String = "IGNORE"
    Public Const RETVALUE_ABORT As String = "ABORT"

    Public Const SETFOCUS As String = "SETFOCUS"

    Public Const CALL_PROCESSSTART As String = "CALL_PROCESSSTART"
  End Class

#Region "Cripta/decripta"
  Public Enum busCryptoEncryptionType
    busBlockEncryption = 1
    busStreamEncryption = 2
  End Enum
  Public Enum busCryptoErrors
    busErrorAquiringContext = vbObjectError + 1056
    busErrorCreatingHash = vbObjectError + 1057
    busErrorCreatingHashData = vbObjectError + 1058
    busErrorDerivingKey = vbObjectError + 1059
    busErrorEncryptingData = vbObjectError + 1060
    busErrorDecryptingData = vbObjectError + 1061
    busErrorInvalidHexString = vbObjectError + 1062
    busErrorMissingParameter = vbObjectError + 1063
    busErrorBadEncryptionType = vbObjectError + 1064
  End Enum
  Private Const ERROR_AQUIRING_CONTEXT As String = "Could not acquire context"
  Private Const ERROR_CREATING_HASH As String = "Could not create hash"
  Private Const ERROR_CREATING_HASH_DATA As String = "Could not create hash data"
  Private Const ERROR_DERIVING_KEY As String = "Could not derive key"
  Private Const ERROR_ENCRYPTING_DATA As String = "Could not encrypt data"
  Private Const ERROR_DECRYPTING_DATA As String = "Could not decrypt data"
  Private Const ERROR_INVALID_HEX_STRING As String = "Not a valid hex string"
  Private Const ERROR_MISSING_PARAMETER As String = "Both a string and a key are required"
  Private Const ERROR_BAD_ENCRYPTION_TYPE As String = "Invalid encryption type specified"
  Private Const CRYPTO_PROVIDER As String = "Microsoft Base Cryptographic Provider v1.0"
  Private Const CRYPT_VERIFYCONTEXT As Integer = &HF0000000
  Private Const PROV_RSA_FULL As Integer = 1
  Private Const ALG_CLASS_HASH As Integer = 32768 ' (4 << 13)
  Private Const ALG_TYPE_ANY As Integer = 0
  Private Const ALG_SID_MD5 As Integer = 3
  Private Const CALG_MD5 As Integer = ALG_CLASS_HASH Or ALG_TYPE_ANY Or ALG_SID_MD5
  Private Const CRYPT_NO_SALT As Integer = &H10S
  Private Const ALG_CLASS_DATA_ENCRYPT As Integer = 24576 ' (3 << 13)
  Private Const ALG_TYPE_BLOCK As Integer = 1536 ' (3 << 9)
  Private Const ALG_TYPE_STREAM As Integer = 2048 ' (4 << 9)
  Private Const ALG_SID_RC2 As Integer = 2
  Private Const ALG_SID_RC4 As Integer = 1
  Private Const CALG_RC2 As Integer = ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_BLOCK Or ALG_SID_RC2
  Private Const CALG_RC4 As Integer = ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_STREAM Or ALG_SID_RC4

  Private Declare Function CryptAcquireContext Lib "advapi32.dll" Alias "CryptAcquireContextA" (ByRef phProv As Integer, ByVal pszContainer As String, ByVal pszProvider As String, ByVal dwProvType As Integer, ByVal dwFlags As Integer) As Integer
  Private Declare Function CryptCreateHash Lib "advapi32.dll" (ByVal hProv As Integer, ByVal algID As Integer, ByVal hKey As Integer, ByVal dwFlags As Integer, ByRef phHash As Integer) As Integer
  Private Declare Function CryptDeriveKey Lib "advapi32.dll" (ByVal hProv As Integer, ByVal algID As Integer, ByVal hBaseData As Integer, ByVal dwFlags As Integer, ByRef phKey As Integer) As Integer
  Private Declare Function CryptDestroyHash Lib "advapi32.dll" (ByVal hHash As Integer) As Integer
  Private Declare Function CryptDestroyKey Lib "advapi32.dll" (ByVal hKey As Integer) As Integer
  Private Declare Function CryptEncrypt Lib "advapi32.dll" (ByVal hKey As Integer, ByVal hHash As Integer, ByVal Final As Integer, ByVal dwFlags As Integer, ByVal pbData As String, ByRef pdwDataLen As Integer, ByVal dwBufLen As Integer) As Integer
  Private Declare Function CryptDecrypt Lib "advapi32.dll" (ByVal hKey As Integer, ByVal hHash As Integer, ByVal Final As Integer, ByVal dwFlags As Integer, ByVal pbData As String, ByRef pdwDataLen As Integer) As Integer
  Private Declare Function CryptHashData Lib "advapi32.dll" (ByVal hHash As Integer, ByVal pbData As String, ByVal dwDataLen As Integer, ByVal dwFlags As Integer) As Integer
  Private Declare Function CryptReleaseContext Lib "advapi32.dll" (ByVal hProv As Integer, ByVal dwFlags As Integer) As Integer
  Private Shared Function EncryptDecrypt(ByVal sText As String, ByVal sKeyRoot As String, ByVal bEncrypt As Boolean, ByVal enEncryptionType As busCryptoEncryptionType) As String

    Dim lResult As Integer
    Dim lHash As Integer
    Dim lKey As Integer
    Dim lHashPassword As Integer
    Dim lFlags As Integer
    Dim lData As Integer
    Dim lClear As Integer
    Dim lEncryptionType As Integer
    Dim m_lProvider As Integer

    Try
      CryptAcquireContext(m_lProvider, vbNullString, CRYPTO_PROVIDER, PROV_RSA_FULL, CRYPT_VERIFYCONTEXT)

      ' Create a handle to a hash object  using the MD5 algorithm
      lResult = CryptCreateHash(m_lProvider, CALG_MD5, 0, 0, lHash)
      If lResult = 0 Then
        Throw New Exception("CLN__STD.EncryptDecrypt error: " & ERROR_CREATING_HASH)
      End If

      ' Add some data to the hash object for use in generating our key
      ' sKeyRoot is, in effect, our key and should be fairly complex and
      ' not easily guessed
      lHashPassword = Len(sKeyRoot)
      lResult = CryptHashData(lHash, sKeyRoot, lHashPassword, 0)
      If lResult = 0 Then
        Throw New Exception("CLN__STD.EncryptDecrypt error: " & ERROR_CREATING_HASH_DATA)
      End If

      ' Select appropriate encryption method
      If enEncryptionType = busCryptoEncryptionType.busBlockEncryption Then
        lEncryptionType = CALG_RC2
      Else
        lEncryptionType = CALG_RC4
      End If

      ' Generate a session key for use with the cypher
      lFlags = CRYPT_NO_SALT
      lResult = CryptDeriveKey(m_lProvider, lEncryptionType, lHash, lFlags, lKey)
      If lResult = 0 Then
        Throw New Exception("CLN__STD.EncryptDecrypt error: " & ERROR_DERIVING_KEY)
      End If

      ' Encrypt or decrypt data as required
      lData = Len(sText)
      If bEncrypt Then
        lClear = lData
        ' Call with a null first to see how long the string needs to be
        lResult = CryptEncrypt(lKey, 0, 1, 0, vbNullString, lData, lClear)
        sText = sText & New String(" "c, lData - lClear)
        ' Encrypt some text
        lResult = CryptEncrypt(lKey, 0, 1, 0, sText, lClear, lData)
        If lResult = 0 Then
          Throw New Exception("CLN__STD.EncryptDecrypt error: " & ERROR_ENCRYPTING_DATA)
        End If
      Else
        ' You may want to slow down a hacker trying multiple passwords to decrypt
        ' by brut force with a sleep command like this...
        'Sleep 1000

        ' Decrypt the text
        lResult = CryptDecrypt(lKey, 0, 1, 0, sText, lData)
        sText = Left(sText, lData)
        If lResult = 0 Then
          Throw New Exception("CLN__STD.EncryptDecrypt error: " & ERROR_DECRYPTING_DATA)
        End If
      End If

      EncryptDecrypt = sText

      If lHash <> 0 Then
        lResult = CryptDestroyHash(lHash)
      End If
      If lKey <> 0 Then
        lResult = CryptDestroyKey(lKey)
      End If
    Catch ex As Exception
      Throw ex
    Finally
      If m_lProvider <> 0 Then CryptReleaseContext(m_lProvider, 0)
    End Try
  End Function
  Private Shared Function ConvertStringFromHex(ByVal sText As String) As String
    Dim lCount As Integer = 0
    Dim sChar As String = ""
    Dim sResult As String = ""
    Dim lLength As Integer = 0

    ConvertStringFromHex = ""

    lLength = Len(sText)
    If lLength Mod 2 <> 0 Then
      Throw New Exception("CLN__STD.ConvertStringFromHex: Error " & ERROR_INVALID_HEX_STRING)
    End If

    For lCount = 1 To lLength
      sChar = Mid(sText, lCount, 1)
      If sChar < "0" Or sChar > "9" Then
        If sChar < "A" Or sChar > "F" Then
          Throw New Exception("CLN__STD.ConvertStringFromHex: Error " & ERROR_INVALID_HEX_STRING)
        End If
      End If
    Next

    For lCount = 1 To lLength Step 2
      sResult = sResult & Chr(CInt("&H" & Mid(sText, lCount, 2)))
    Next

    Return sResult
  End Function
  Private Shared Function ConvertStringToHex(ByVal sText As String) As String
    Dim lCount As Integer = 0
    Dim sHex As String = ""
    Dim sResult As String = ""

    For lCount = 1 To Len(sText)
      sHex = Hex(Asc(Mid(sText, lCount, 1)))
      If Len(sHex) = 1 Then
        sHex = "0" & sHex
      End If
      sResult = sResult & sHex
    Next
    Return sResult
  End Function
  Public Shared Function PwdCript(ByVal strData As String, ByRef strEncrypted As String) As Boolean
    Try
      If strData = "" Then
        strEncrypted = ""
        Return True
      End If
      strEncrypted = EncryptDecrypt(strData, "acT &%%15?éè\ Ygt<+[", True, busCryptoEncryptionType.busBlockEncryption)
      strEncrypted = ConvertStringToHex(strEncrypted)

      Return True
    Catch ex As Exception
      MsgBox("Error in PwdCript" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
    End Try
  End Function
  Public Shared Function PwdDeCript(ByRef strEncrypted As String, ByRef strDescripted As String) As Boolean
    Try
      If strEncrypted = "" Then
        strDescripted = ""
        Return True
      End If
      strEncrypted = ConvertStringFromHex(strEncrypted)
      strDescripted = EncryptDecrypt(strEncrypted, "acT &%%15?éè\ Ygt<+[", False, busCryptoEncryptionType.busBlockEncryption)
      Return True

    Catch ex As Exception
      MsgBox("Error in PwdDeCript" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
    End Try
  End Function

  'Public Shared Function PwdCriptNet(ByVal strData As String, ByRef strEncrypted As String) As Boolean
  '  Try
  '    Dim arData() As Byte = System.Text.Encoding.ASCII.GetBytes(strData)
  '    Const CSPNAME As String = "Microsoft Base Cryptographic Provider v1.0"
  '    Dim cspParams As CspParameters = New CspParameters(1, CSPNAME)
  '    Dim deriveBytes As PasswordDeriveBytes = New PasswordDeriveBytes("acT &%%15?éè\ Ygt<+[", Nothing, "SHA-1", 1, cspParams)
  '    Dim rgbIV(7) As Byte
  '    Dim key() As Byte = deriveBytes.CryptDeriveKey("RC2", "SHA1", 0, rgbIV)
  '    Dim provider As RC2CryptoServiceProvider = New RC2CryptoServiceProvider
  '    provider.Key = key
  '    provider.IV = rgbIV
  '    '-
  '    Dim rc2CSP As New RC2CryptoServiceProvider()
  '    Dim encryptor As ICryptoTransform = rc2CSP.CreateEncryptor(key, rgbIV)
  '    Dim msEncrypt As New MemoryStream()
  '    Dim csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
  '    csEncrypt.Write(arData, 0, arData.Length)
  '    csEncrypt.FlushFinalBlock()
  '    Dim encrypted() As Byte = msEncrypt.ToArray
  '    strEncrypted = ""
  '    For i As Integer = 0 To encrypted.Length - 1
  '      strEncrypted += encrypted(i).ToString & " "
  '    Next
  '    If strEncrypted.Length > 0 Then strEncrypted = strEncrypted.Substring(0, strEncrypted.Length - 1)

  '    Return True
  '  Catch ex As Exception
  '    MsgBox("Error in PwdCriptNet" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
  '  End Try
  'End Function
  'Public Shared Function PwdDeCriptNet(ByRef strEncrypted As String, ByRef strDescripted As String) As Boolean
  '  Try
  '    Const CSPNAME As String = "Microsoft Base Cryptographic Provider v1.0"
  '    Dim strT() As String = strEncrypted.Split(" "c)
  '    Dim encrypted(strT.Length - 1) As Byte
  '    For i As Integer = 0 To strT.Length - 1
  '      encrypted(i) = CType(CType(strT(i), Integer), Byte)
  '    Next
  '    Dim cspParams As CspParameters = New CspParameters(1, CSPNAME)
  '    Dim deriveBytes As PasswordDeriveBytes = New PasswordDeriveBytes("acT &%%15?éè\ Ygt<+[", Nothing, "SHA-1", 1, cspParams)
  '    Dim rgbIV(7) As Byte
  '    Dim key() As Byte = deriveBytes.CryptDeriveKey("RC2", "SHA1", 0, rgbIV)
  '    Dim provider As RC2CryptoServiceProvider = New RC2CryptoServiceProvider
  '    provider.Key = key
  '    provider.IV = rgbIV
  '    '-
  '    Dim rc2CSP As New RC2CryptoServiceProvider()
  '    Dim decryptor As ICryptoTransform = rc2CSP.CreateDecryptor(key, rgbIV)
  '    Dim msDecrypt As New MemoryStream(encrypted)
  '    Dim csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
  '    Dim fromEncrypt(255) As Byte
  '    csDecrypt.Read(fromEncrypt, 0, 255)
  '    strDescripted = Encoding.ASCII.GetString(fromEncrypt)
  '    Dim l As Integer = strDescripted.IndexOf(Chr(0))
  '    If l > 0 Then strDescripted = strDescripted.Substring(0, l)
  '    Return True

  '  Catch ex As Exception
  '    MsgBox("Error in PwdDeCriptNet" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
  '  End Try
  'End Function

  Public Shared Function Codifica(ByRef strPwd As String) As Boolean
    Dim strTmp As String = ""
    Dim lChar, i As Integer
    Try
      For i = 0 To strPwd.Length - 1
        lChar = Asc(strPwd(i)) Xor strPwd.Length
        lChar += i Mod strPwd.Length
        strTmp &= Chr(lChar)
      Next

      strPwd = strTmp

      Return True
    Catch ex As Exception
      MsgBox("Error in CLN__STD.Codifica" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
    End Try
  End Function
  Public Shared Function Decodifica(ByRef strPwd As String) As Boolean
    Dim strTmp As String = ""
    Dim lChar, i As Integer
    Try
      For i = 0 To strPwd.Length - 1
        lChar = Asc(strPwd(i)) - i Mod strPwd.Length
        lChar = lChar Xor strPwd.Length
        strTmp &= Chr(lChar)
      Next

      strPwd = strTmp

      Return True
    Catch ex As Exception
      MsgBox("Error in CLN__STD.Decodifica" & vbCrLf & "Error: " & ex.ToString, MsgBoxStyle.Exclamation, "Messaggio da " & IIf(CLN__STD.FRIENDLY, "Friendly", "Bus.Net").ToString)
    End Try
  End Function
#End Region

  Public Shared StopCheckInvokeCustomFunction As Boolean = False
  Public Shared Function CheckInvokeCustomFunction(ByVal bCustom As Boolean, ByRef oCurrClass As Object, ByVal oFun As MethodBase, ByRef oIn As ArrayList, ByRef oOut As Object) As Boolean
    'questa funzione, contenuta solo nelle funzioni standard con più firme di Business, verifica se nelle dll 
    'personalizzate è presente la stessa funzione con un minor numero di parametri e, nel caso, la invoca al posto di quella standard

    'questo per fare in modo che se un rive ha personalizzato la BDVEBOLL.aggiornadoc quando aveva 5 parametri, 
    'se nello standard è stato aggiunto un ulteriore parametro venga comunque chiamata la funzione personalizzata
    'del rive

    'per un esempio vedi BDLBBASE.LeggiTabellaSemplice o tutte le funzioni con firme multiple su BDVEBOLL

    'bCustom = true se la dll standard è stata ereditata con un BO, BF, BH
    'oCurrClass = classe standard (BN, BE, BN) che contiene la funzione chiamata
    'oFun = funzione chiamata
    'oIn = elenco di parametri passati alla funzione chiamata (quella standard) 
    'oOut = valore di uscita della funzione custom

    Try
      If Not bCustom OrElse StopCheckInvokeCustomFunction Then Return False

      oOut = Nothing
      Dim strFunName As String = oFun.Name
      Dim strFunFull As String = oFun.ToString
      Dim bOk As Boolean = False

      Dim mods() As System.Reflection.MethodInfo = Nothing
      Dim modsCurr As System.Reflection.MethodInfo = Nothing

      'verifico se nella classe base c'è la stessa funzione con parametri diversi
      If oCurrClass.GetType.ToString.StartsWith("NTSInformatica.FRM") Or _
         oCurrClass.GetType.ToString.StartsWith("NTSInformatica.CLE") Or _
         oCurrClass.GetType.ToString.StartsWith("NTSInformatica.CLD") Then Return False 'la classe non è ereditata: non devo fare nulla

      'se nella funzione ereditata c'è un mybase.funzione entra in un loop infinito.
      'verifico nello stack trace di non essere già in una CheckInvokeCustomFunction
      Dim oTrace As New StackTrace()
      For nI As Integer = 2 To oTrace.FrameCount - 1
        'Dim sss As String = oTrace.GetFrame(nI).GetMethod.DeclaringType.ToString & "." & oTrace.GetFrame(nI).GetMethod.Name

        If oTrace.GetFrame(nI).GetMethod.ReflectedType.Namespace = "NTSInformatica" And _
           oTrace.GetFrame(nI).GetMethod.Name = strFunName And _
           oFun.DeclaringType.ToString = oTrace.GetFrame(nI).GetMethod.DeclaringType.ToString Then

          If nI < 15 Then Return False 'probabilmente sono in un loop dove nella funzione ereditata c'è una mybase.funzione che rimanda alal standard che richiama nuovamente la CheckInvokeCustomFunction

          'arCheckInvokeCustomFunction
        End If
        If nI > 15 Then Exit For
      Next

      'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
      mods = oCurrClass.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                   System.Reflection.BindingFlags.Instance Or _
                                   System.Reflection.BindingFlags.DeclaredOnly)
RESTART:

      If mods Is Nothing Then Return False 'non è una classe ereditata da NTSInformatica secondo i canoni standard di ereditarietà
      If mods.Length = 0 Then Return False

      'se trovo la funzione con la stessa firma ma con parametri in meno nella classe ereditta, eseguo quella
      Dim nItem As Integer = 0
      Dim strTmp As String = ""
      For i As Integer = 0 To mods.Length - 1
        If mods(i).Name = strFunName And mods(i).ToString.Length < strFunFull.Length Then
          'nella classe ereditata c'è la funzione con lo stesso nome di quella padre ma con parametri in meno: la invoco al posto di quella padre
          'SE NELLA CLASSE FIGLIO C'E' LA STESSA FUNZIONE CON PIU' FIRME viene sempre presa la funzione con il maggior numero di parametri
          If modsCurr Is Nothing Then
            'memorizzo la funzione solo se ha la stassa firma di quella standard (almeno per qunato riguarda i parametri presenti in entrambe le funzioni)
            If strFunFull.Substring(0, mods(i).ToString.Length - 1) = mods(i).ToString.Substring(0, mods(i).ToString.Length - 1) Then
              modsCurr = mods(i)
            End If
          Else
            If modsCurr.ToString.Length < mods(i).ToString.Length Then
              'memorizzo la funzione solo se ha la stassa firma di quella standard (almeno per qunato riguarda i parametri presenti in entrambe le funzioni)
              If strFunFull.Substring(0, mods(i).ToString.Length - 1) = mods(i).ToString.Substring(0, mods(i).ToString.Length - 1) Then
                modsCurr = mods(i)
              End If
            End If
          End If
          nItem += 1
        End If
      Next

      If nItem > 0 And Not modsCurr Is Nothing Then
        Dim oParam(modsCurr.GetParameters.Length - 1) As Object
        For n As Integer = 0 To oParam.Length - 1
          oParam(n) = oIn(n)
        Next
        Try
          CheckInvokeCustomFunction = True
          oOut = modsCurr.Invoke(oCurrClass, oParam)
          'riassegno i parametri della funzione per quelli di tipo byref
          For n As Integer = 0 To oParam.Length - 1
            oIn(n) = oParam(n)
          Next
          Return True
        Catch ex As Exception
          Throw ex
        End Try

      End If

      '-----------------------------
      'se la classe ereditata è una verticalizzazione su un verticale standard di un partner, 
      'devo rifare il tutto sulla dll del verticare del partner
      If bOk = False Then
        bOk = True
        If Not (oCurrClass.GetType.BaseType.ToString.StartsWith("NTSInformatica.FRM") Or _
                oCurrClass.GetType.BaseType.ToString.StartsWith("NTSInformatica.CLE") Or _
                oCurrClass.GetType.BaseType.ToString.StartsWith("NTSInformatica.CLD")) Then
          mods = oCurrClass.GetType.BaseType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                System.Reflection.BindingFlags.Instance Or _
                                                System.Reflection.BindingFlags.DeclaredOnly)
          GoTo RESTART
        End If
      End If

      Return False
    Catch ex As Exception
      If CheckInvokeCustomFunction = False Then
        'ignoro l'errore e faccio eseguire la routine sandard NTS
      Else
        'si è verificato un errore nella routine personalizzata. lo visualizzo
        Throw ex
      End If

    End Try

  End Function

#Region "Parser Query SQL Server to SQLite"
  ''' <param name="strIn">Query in formato SQL Server</param>
  ''' <param name="nDBOut"0:Microsoft SQL Server, 1: SQLite
  Public Shared Function ParseSql(ByVal strIn As String, ByVal nDBOut As Integer) As String
    Return ParseSql_P(strIn, nDBOut, False)
  End Function
  Private Shared Function ParseSql_P(ByVal strIn As String, ByVal nDBOut As Integer, ByVal bSubquery As Boolean) As String
    Dim strSubQuery As String = ""
    'subquery da passare ad un'altra istanza di ParseSql_P
    Dim strOut As String = ""
    'query da restituire
    Dim nParOpen As Integer = 0
    'numero di parentesi aperte
    Dim strWord As String()
    'collezione di parole da trattare
    Dim nWord As Integer = 0
    'numero della parola in fase di analisi
    Dim strTop As String = ""
    'stringa da accodare se la select prevede un top ...
    Dim nWordTmp As Integer = 0
    Dim nVirg As Integer = 0
    Dim bApici As Boolean = False
    'true l'apice è aperto, false l'apice non è stato aperto
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim a As Integer = 0
    Dim strTmp As String = ""

    If strIn.Trim() = "" Then
      Return ""
    End If

    If nDBOut = 0 Then
      Return strIn
    End If

    If strIn.ToUpper.StartsWith("*NOPARSE*") Then Return strIn.Substring(9)

    'tolgo l'eventuale ; finale
    If strIn.Substring(strIn.Length - 1, 1) = ";" Then
      strIn = strIn.Substring(0, strIn.Length - 1)
    End If

    Try
      If Not bSubquery Then
        '------------------------------------------------------
        'onde evitare problemi, eccetto il testo tra apici riduco tutto a lowercase, 
        'in modo che nome campi e nomi tabelle siano minuscole
        'inoltre all'interno delle stringhe i caratteri '(', ')', ',' li sostituisco con caratteri particolari
        ''(' -> ╠
        '')' -> ╦
        '',' -> ╬
        bApici = False
        e = 0
        i = strIn.IndexOf("'"c)
        strTmp = ""
        While i > -1
          If Not bApici Then
            strTmp += strIn.Substring(e, i - e).ToLower()
          Else
            strTmp += strIn.Substring(e, i - e).Replace("(", "╠").Replace(")", "╦").Replace(",", "╬")
          End If

          bApici = Not bApici
          e = i
          i = strIn.IndexOf("'"c, i + 1)
        End While
        strTmp += strIn.Substring(e).ToLower()
        strIn = strTmp
        strTmp = ""

        '------------------------------------------------------
        ' normalizzo la query
        strIn = strIn.Replace("++", "||")
        strIn = strIn.Replace(",", " , ")
        strIn = strIn.Replace("(", " ( ")
        strIn = strIn.Replace(")", " ) ")
        'per il carattere = devo aggiungere gli spazi prima e dopo SOLO se non sono dentro ad una stringa
        'conto gli apici prima dell' =: se sono pari non sono dentro ad una stringa!!!!
        i = strIn.IndexOf("=")
        e = 0 'apici contati
        a = 0 'caratteri fino a dove ho contato glli apici
        While i > -1
          For l As Integer = a To i
            If strIn(l) = "'"c Then e += 1
          Next
          If CInt(e / 2) * 2 = e Then
            If strIn.Substring(i - 1, 1) = ">" Or strIn.Substring(i - 1, 1) = "<" Then
              'se è preceduto da < o > (>= o <=) non devo fare niente
            Else
              strIn = strIn.Substring(0, i) & " = " & strIn.Substring(i + 1)
            End If
          Else
            'non devo aggiungere gli spazi: sono all'interno di una stringa
          End If
          a = i
          i = strIn.IndexOf("=", i + 2)
        End While
        strIn = strIn.Replace("+", " + ")
        'non farlo per l'operatore '-', sbaglierebbe l'impostazione dei campi datetime
        strIn = strIn.Replace(vbCr & vbLf, " ")
        strIn = strIn.Replace(vbLf, " ")
        strIn = strIn.Replace(vbCr, " ")
        While strIn.IndexOf("  ") > 0
          strIn = strIn.Replace("  ", " ")
        End While

        ''------------------------------------------------------
        ''se è una update, in mysql devo spostare la from come in access, ovvero tra la parola UPDATE e la SET
        'If strIn.Substring(0, strIn.IndexOf(" "c)) = "update" AndAlso (nDBOut = MyDbType.MySQL OrElse nDBOut = MyDbType.MsACC) Then
        '  i = strIn.IndexOf(" from ") + 1
        '  If i > 0 Then
        '    'cerco la parte di stringa tra from e where (se non c'è la where ... mi adeguo)
        '    e = strIn.IndexOf(" where ") + 1
        '    If e = 0 Then
        '      e = strIn.IndexOf(" group by ") + 1
        '    End If
        '    If e = 0 Then
        '      e = strIn.IndexOf(" order by ") + 1
        '    End If
        '    If e = 0 Then
        '      e = strIn.Length
        '    End If

        '    strTmp = "update " & strIn.Substring(i + 5, e - i - 5)
        '    a = strIn.IndexOf(" set ")
        '    strTmp += strIn.Substring(a, i - a)
        '    strTmp += strIn.Substring(e)
        '    strIn = strTmp
        '    strTmp = ""
        '  End If
        'End If
        '------------------------------------------------------
        'se è una delete, in  modificare la query se c'è più di una tabella in join
        If strIn.Substring(0, strIn.IndexOf(" "c)) = "delete" AndAlso nDBOut = 1 Then
          'delete tt from tt inner join tt1 on .... where ...
          'diventa
          'delete tt where rowid in (select rowid from tt inner join tt1 on .... where ...)
          If strIn.IndexOf(" join ") > -1 Then
            i = strIn.IndexOf(" from ")
            strIn = strIn.Substring(0, i) & " where rowid in ( select rowid " & strIn.Substring(i) & " ) "
          End If
        End If
      End If
      'if (!bSubquery)
      '------------------------------------------------------
      'determino le parole
      strWord = strIn.Split(" "c)

      For nWord = 0 To strWord.Length - 1
        '------------------------------------------------------
        'sostituzione dei termini diversi tra tipi di database
        '------------------------------------------------------
        Select Case strWord(nWord)
          Case "len"
            strWord(nWord) = "length"
            Exit Select
          Case "+"
            'nell'unione di stringhe bisognausare il carattere '||' al posto del '+'
            'visto che non posso sapere se prima e dopo il + ci sono dei campi numerici identificati come campi database o stringhe
            'applico il cambio del carattere solo se nella parola prima o dopo c'è un apice
            If strWord(nWord - 1).Contains("'") Or strWord(nWord + 1).Contains("'") Then strWord(nWord) = "||"
            Exit Select
          Case "isnull"
            strWord(nWord) = "ifnull"
            Exit Select
          Case "substring"
            strWord(nWord) = "substr"
            Exit Select
          Case "getdate"
            strWord(nWord) = "current_timestamp"
            strWord(nWord + 1) = "" 'tolgo le parentesi dopo getdate()'
            strWord(nWord + 2) = ""
            Exit Select
          Case "latin1_general_cs_as"
            strWord(nWord) = "binary"
            Exit Select
        End Select

        If strWord(nWord) <> "" Then
          If strWord(nWord)(0) = "'" Then
            'se è una data impostata come valore fisso (es '1900-1-1') devo aggiungere gli 0 in modo che diventi '1900-01-01'
            'occhio: potrebbe essere anche una data senza l'apice finale, visto che se è una data/ora '1900-1-1 00:00:00' la parte con l'ora è nella parola successiva
            If strWord(nWord).Length >= 9 AndAlso strWord(nWord).Length <= 11 AndAlso strWord(nWord)(5) = "-" Then
              If IsNumeric(strWord(nWord).Replace("-", "0").Replace("'", "")) Then
                i = strWord(nWord).IndexOf("-", 6)
                If i <> 8 Then
                  strWord(nWord) = strWord(nWord).Substring(0, 6) & "0" & strWord(nWord).Substring(6)
                End If
                If strWord(nWord)(strWord(nWord).Length - 1) = "'" Then
                  If strWord(nWord).Length <> 12 Then strWord(nWord) = strWord(nWord).Substring(0, 9) & "0" & strWord(nWord).Substring(9)
                Else
                  If strWord(nWord).Length <> 11 Then strWord(nWord) = strWord(nWord).Substring(0, 9) & "0" & strWord(nWord).Substring(9)
                End If
              End If
            End If
          End If
        End If

        '------------------------------------------------------
        'è una subquery: devo trovare la parentesi di chiusura, quindi elaboro il tutto separatamente
        'attenzione: se prima della 'SELECT' non c'è la '(' non devo trattarla come una subqery, ma come una query normale
        '(può essere una union o una insert into .. select
        If nWord > 0 AndAlso strWord(nWord) = "select" AndAlso strWord(nWord - 1) = "(" Then
          nParOpen = 1
          strSubQuery = "select"
          While nParOpen <> 0
            nWord += 1
            If strWord(nWord) = "'" Then
              bApici = Not bApici
            End If
            If strWord(nWord) = "(" AndAlso Not bApici Then
              nParOpen += 1
            End If
            If strWord(nWord) = ")" AndAlso Not bApici Then
              nParOpen -= 1
            End If
            strSubQuery += " " & strWord(nWord)
          End While
          strSubQuery = strSubQuery.Substring(0, strSubQuery.Length - 1)
          nWord -= 1
          'ho la subquery: la passo al nuovo parser
          strOut += ParseSql_P(strSubQuery, nDBOut, True)
        ElseIf strWord(nWord) = "select" AndAlso (strWord(nWord + 1) = "top" Or strWord(nWord + 2) = "top") Then
          '------------------------------------------------------
          'gestione della TOP ... devo spostarlo alla fine della query
          '------------------------------------------------------
          If strWord(nWord + 1) = "top" Then
            strTop = " limit " & strWord(nWord + 2) & " "
            strWord(nWord + 1) = ""
            strWord(nWord + 2) = ""
          Else
            strTop = " limit " & strWord(nWord + 3) & " "
            strWord(nWord + 2) = ""
            strWord(nWord + 3) = ""
          End If
          strOut += " " & strWord(nWord)
        ElseIf strWord(nWord) = "substring" Or strWord(nWord) = "substr" Then
          '------------------------------------------------------
          'substring: il parametro di partenza deve sempre essere >= 1 
          '------------------------------------------------------
          nParOpen = 1
          nWordTmp = nWord + 2
          nVirg = 0
          While nVirg <> 1
            nWordTmp += 1
            If strWord(nWordTmp) = "'" Then
              bApici = Not bApici
            End If
            If strWord(nWordTmp) = "(" AndAlso Not bApici Then
              nParOpen += 1
            End If
            If strWord(nWordTmp) = ")" AndAlso Not bApici Then
              nParOpen -= 1
            End If
            If strWord(nWordTmp) = "," AndAlso Not bApici AndAlso nParOpen = 1 Then
              nVirg += 1
            End If
          End While
          'ho il secondo parametro della substring: verifico che sia maggiore di 0
          If Integer.TryParse(strWord(nWordTmp + 1), i) Then
            If i < 1 Then
              Throw New Exception("ATTENZIONE: nella query è presente una SUBSTRING con secondo parametro di inizio uguale a 0!")
            End If
          End If

          strOut += " " & strWord(nWord)
        ElseIf strWord(nWord) = "cast" Then
          '------------------------------------------------------
          'non so se devo rimappare alcuni tipi di dati
          '------------------------------------------------------
          strOut += " " & strWord(nWord)
          strTmp = ""
          nParOpen = 1
          nWordTmp = nWord + 1
          nVirg = 0
          While nVirg <> 1
            nWordTmp += 1
            If strWord(nWordTmp) = "'" Then
              bApici = Not bApici
            End If
            If strWord(nWordTmp) = "(" AndAlso Not bApici Then
              nParOpen += 1
            End If
            If strWord(nWordTmp) = ")" AndAlso Not bApici Then
              nParOpen -= 1
            End If
            If strWord(nWordTmp) = "as" AndAlso Not bApici AndAlso nParOpen = 1 Then
              nVirg += 1
            End If
          End While
          'trovo la tipizzazione del cast
          While nParOpen <> 0
            nWordTmp += 1
            If strWord(nWordTmp) = "(" Then
              nParOpen += 1
            End If
            If strWord(nWordTmp) = ")" Then
              nParOpen -= 1
            End If
            If nParOpen <> 0 Then
              strTmp += strWord(nWordTmp)
              strWord(nWordTmp) = ""
            End If
          End While
          Select Case strTmp
            Case "timestamp"
              strWord(nWordTmp) = "blob" & strWord(nWordTmp)
              Exit Select
            Case "int"
              strWord(nWordTmp) = "integer" & strWord(nWordTmp)
              Exit Select
            Case "money"
              strWord(nWordTmp) = "numeric" & strWord(nWordTmp)
              Exit Select
            Case "memo"
              strWord(nWordTmp) = "text" & strWord(nWordTmp)
              Exit Select
            Case "image"
              strWord(nWordTmp) = "blob" & strWord(nWordTmp)
              Exit Select
            Case Else
              strWord(nWordTmp) = strTmp & strWord(nWordTmp)
              Exit Select
          End Select

        ElseIf strWord(nWord) = "convert" Then
          '------------------------------------------------------
          'convert
          '------------------------------------------------------
          Throw New Exception("ATTENZIONE: nella query è presente la funzione CONVERT: sostituirla con la CAST... da finire")
          strOut += " " & strWord(nWord)
        ElseIf strWord(nWord) = "union" Then
          '------------------------------------------------------
          'se c'era un 'Top ...' lo accodo alla sua query
          '------------------------------------------------------
          strOut += strTop & " " & strWord(nWord)
          strTop = ""
        Else
          '------------------------------------------------------
          'caso normale: compongo la stringa da restituire
          '------------------------------------------------------
          strOut += " " & strWord(nWord)
          '------------------------------------------------------
          ' piccolo controllino
          'If nWord > 0 AndAlso strWord(nWord - 1) = "," AndAlso strWord(nWord) = "*" Then
          '  Throw New Exception("ATTENZIONE: nella query è presente un campo prima di un * (es: SELECT campo1, * FROM):" & vbCr & vbLf & "in MySQL verrà generato un errore." & vbCr & vbLf & "Sostituire con 'SELECT *, campo1 FROM'", MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          'End If
        End If
      Next

      'se c'era un 'Top ...' lo accodo
      strOut += strTop

      'aggiungo il ; finale
      If Not bSubquery Then
        While strOut.IndexOf("  ") > -1
          strOut = strOut.Replace("  ", " ")
        End While
        strOut += ";"

        '------------------------------------------
        'piccole correzioni. ad sempio "upper (" da errore in mysql, visto che la sintassi corretta è "upper("
        strOut = strOut.Replace(" ( ", "(").Replace(" ) ", ")").Replace(" ,", ",")

        '------------------------------------------
        'rimetto i caratteri che avevo cambiato ad inizio query
        ''(' -> ╠
        '')' -> ╦
        '',' -> ╬
        strOut = strOut.Replace("╦", ")").Replace("╠", "(").Replace("╬", ",")


        '------------------------------------------
        'tratto la DATEDIFF alla fine (visto che devo invertire la prima con la seconda data ed una data potrebbe essere anche una query
        Dim nStartDatediff As String = strOut.IndexOf(" datediff")
        While nStartDatediff > -1
          'formato sql datediff(ss , datauno , data2) di sql server
          'OCCHIO LE DATE VANNO INVERTITE DI POSIZIONE
          'le date sono sempre in format 'yyyy-mm-dd', ma devono esserci gli 0 se il numero è di una sola cifra ('2012-1-1' -> '2012-01-01')
          'deve diventare per secondi strftime("%s", data2 ) -  strftime('%s', datauno) 
          '   (strftime("%s", data2 ) -  strftime('%s', datauno)) / 60 = minuti / 60 = ore 
          'per giorni
          ' per mesi Select datediff(mm, '2012-1-1', '2010-10-1') as months -> SELECT cast((julianday('2010-10-01') - julianday('2012-01-10')) /30 as int) AS Months             
          ' per anni datediff(yy, datauno , data2) = strftime('%Y',  data2) - strftime('%Y',  datauno)
          'informazioni su http://www.manpagez.com/man/3/strftime/
          '%s    is replaced by the number of seconds since the Epoch
          Dim strPeriodoAnalisi As String = ""
          Dim nFinePrimoParametro As Integer = 0
          Dim nFineDatadiff As Integer = 0
          i = nStartDatediff + 10
          While strOut(i) <> ","
            If strOut(i) <> "(" And strOut(i) <> " " Then strPeriodoAnalisi += strOut(i)
            i += 1
          End While
          nFinePrimoParametro = i

          Select Case strPeriodoAnalisi
            Case "ss", "s"      'secondi
              Dim nParentesiAperte As Integer = 1
              Dim nFineData1 As Integer = 0
              i = nFinePrimoParametro + 1
              While nParentesiAperte > 0
                If strOut(i) = "(" Then nParentesiAperte += 1
                If strOut(i) = ")" Then nParentesiAperte -= 1
                'come trovo la prima ',' e le parentsi aperte sono solo 1 vuol dire che è la fine della prima data
                If strOut(i) = "," And nParentesiAperte = 1 Then nFineData1 = i
                i += 1
              End While
              nFineDatadiff = i - 1
              Dim strData2 As String = "" 'ottengo la seconda data della datadiff
              strData2 = strOut.Substring(nFineData1 + 1, nFineDatadiff - (nFineData1 + 1))

              Dim strData1 As String = "" 'ottengo la prima data della datadiff
              strData1 = strOut.Substring(nFinePrimoParametro + 1, nFineData1 - (nFinePrimoParametro + 1))

              strOut = strOut.Substring(0, nStartDatediff) + _
                       "(strftime('%s', " + strData2 + ") - strftime('%s', " + strData1 + "))" + _
                       strOut.Substring(nFineDatadiff + 1)
            Case "mm", "m"      'mesi
              'da fare
            Case "yy"           'anni
              'da fare
            Case "y", "d", "dd" 'giorni
              'da fare
          End Select

          nStartDatediff = strOut.IndexOf(" datediff")
        End While     'While i > -1
      End If

      Return strOut.Trim()
    Catch err As Exception
      Throw New Exception("Errore in fase di PARSE query: " & vbCr & vbLf & strIn & vbCr & vbLf & "ERRORE:" & err.ToString())
      Return strIn
    End Try
  End Function
#End Region

  Public Shared Function TestSerieMaxLen(ByVal strSerie As String, ByVal bAccettaNull As Boolean) As String
    Try
      If strSerie <> " " Then strSerie = strSerie.Replace(" ", "")
      If strSerie = "" And bAccettaNull = False Then strSerie = " "
      Return strSerie
    Catch ex As Exception
      Return " "
    End Try
  End Function

End Class

Public Class NTSSendKeys
  Private Declare Auto Function SendMessage Lib "user32.dll" Alias "SendMessage" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByRef lParam As IntPtr) As IntPtr
  Private Declare Auto Function PostMessage Lib "user32.dll" Alias "PostMessage" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByRef lParam As IntPtr) As IntPtr
  Private Declare Auto Function SendMessageKeyUpDown Lib "user32" Alias "SendMessage" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr

  Public Shared Sub Send(ByVal hWnd As Integer, ByVal strTesto As String)
    'Const WM_KEYDOWN As Integer = 256 '0X100
    ''Const WM_KEYUP As Integer = 257 '0X101
    ''Const WM_KEYPRESS As Integer = 258 '0X102
    'Const VK_F5 As Integer = 116  '0X74
    'Const VK_ENTER As Integer = 3
    'Const VK_TAB As Integer = 9
    ''Const VK_CONTROL As Integer = 17 '0x11

    'If hWnd = 0 Then
    If CLN__STD.IsBis Then
      'con queste api dovrebbe andare, ma da finire:

      'esempio buono da tenere per eventuali implementazioni
      'Const WM_KEYDOWN As Integer = &H100
      'Const WM_KEYUP As Integer = &H101
      'Const VK_RETURN As Integer = 13
      'SendMessageKeyUpDown(hWnd, WM_KEYDOWN, VK_RETURN, IntPtr.Zero)
      'SendMessageKeyUpDown(hWnd, WM_KEYUP, VK_RETURN, IntPtr.Zero)

      'buono anche, ma valido solo per textbox 
      'CType(oCtrl, NTSTextBox).SendKey(New KeyEventArgs(Keys.Enter))
    Else

      Try
        SendKeys.Send(strTesto)
      Catch ex As Exception
        'se lanciato da WPF da errore!!!!!!
      End Try
    End If
    'Else
    '  Select Case strTesto
    '    Case "{F5}" : PostMessage(hWnd, WM_KEYDOWN, VK_F5, 0)
    '    Case "{ENTER}" : PostMessage(hWnd, WM_KEYDOWN, VK_ENTER, 0)
    '    Case "{TAB}" : PostMessage(hWnd, WM_KEYDOWN, VK_TAB, 0)
    '      'Case "+{TAB}"
    '      '  PostMessage(hWnd, WM_KEYDOWN, VK_CONTROL, 0)
    '      '  PostMessage(hWnd, WM_KEYDOWN, VK_TAB, 0)
    '      '  PostMessage(hWnd, WM_KEYUP, VK_TAB, 0)
    '      '  PostMessage(hWnd, WM_KEYUP, VK_CONTROL, 0)
    '    Case Else : SendKeys.Send(strTesto)
    '  End Select
    'End If
  End Sub
  Public Shared Sub SendWait(ByVal hWnd As Integer, ByVal strTesto As String)
    'Const WM_KEYDOWN As Integer = 256 '0X100
    ''Const WM_KEYUP As Integer = 257 '0X101
    ''Const WM_KEYPRESS As Integer = 258 '0X102
    'Const VK_F5 As Integer = 116  '0X74
    'Const VK_ENTER As Integer = 3
    'Const VK_TAB As Integer = 9
    'Const VK_CONTROL As Integer = 17 '0x11

    'If hWnd = 0 Then
    If CLN__STD.IsBis = False Then
      SendKeys.SendWait(strTesto)
    Else
      Send(hWnd, strTesto)
    End If
    'Else
    '  Select Case strTesto
    '    Case "{F5}" : SendMessage(hWnd, WM_KEYDOWN, VK_F5, 0)
    '    Case "{ENTER}" : SendMessage(hWnd, WM_KEYDOWN, VK_ENTER, 0)
    '    Case "{TAB}" : SendMessage(hWnd, WM_KEYDOWN, VK_TAB, 0)
    '      'Case "+{TAB}" : SendMessage(hWnd, WM_KEYDOWN, VK_TAB, VK_CONTROL)
    '    Case Else : SendKeys.SendWait(strTesto)
    '  End Select
    'End If
  End Sub
End Class

#Region "Enumerare proprietà delle stampanti"
Public Enum PrinterStatus
  Other = 1
  Unknown
  Idle
  Printing
  Warmup
  Stopped
  Offline
End Enum

Public Class Win32_Printer
  Public DriverName As String
  Public Location As String
  Public Name As String
  Public Network As Boolean
  Public PortName As String
  Public ServerName As String
  Public Status As PrinterStatus
  Public WorkOffline As Boolean
  Public DefaultPrinter As Boolean

  Public Shared Function GetList() As List(Of Win32_Printer)
    Dim query As String = "Select * From Win32_Printer"
    Dim searcher As New ManagementObjectSearcher(query)
    Dim results As ManagementObjectCollection = searcher.[Get]()
    Dim list As New List(Of Win32_Printer)(results.Count)
    For Each obj As ManagementObject In results
      Dim entry As New Win32_Printer()

      entry.DefaultPrinter = CBool(obj("Default"))
      For Each field As FieldInfo In GetType(Win32_Printer).GetFields()
        If field.Name <> "DefaultPrinter" Then
          field.SetValue(entry, ConvertValue(obj(field.Name), field.FieldType))
        End If
      Next

      list.Add(entry)
    Next

    'sistema per ottenre altre informazioni
    query = "Select * from Win32_PrinterConfiguration"
    Dim colInstalledPrinters As New ManagementObjectSearcher(query)
    Dim results1 As ManagementObjectCollection = colInstalledPrinters.[Get]()
    For Each objPrinter As ManagementObject In results1
      Dim o As Object = CObj(objPrinter("Name"))
      o = CObj(objPrinter("Collate"))
      o = CObj(objPrinter("Copies"))
      o = CObj(objPrinter("DriverVersion"))
      o = CObj(objPrinter("Duplex"))
      o = CObj(objPrinter("Orientation"))
      'Wscript.Echo("Name: " & objPrinter.Name)
      'Wscript.Echo("Collate: " & objPrinter.Collate)
      'Wscript.Echo("Copies: " & objPrinter.Copies)
      'Wscript.Echo("Driver Version: " & objPrinter.DriverVersion)
      'Wscript.Echo("Duplex: " & objPrinter.Duplex)
      'Wscript.Echo("Horizontal Resolution: " & _
      '    objPrinter.HorizontalResolution)
      'If objPrinter.Orientation = 1 Then
      '  strOrientation = "Portrait"
      'Else
      '  strOrientation = "Landscape"
      'End If
      'Wscript.Echo("Orientation : " & strOrientation)
      'Wscript.Echo("Paper Length: " & objPrinter.PaperLength / 254)
      'Wscript.Echo("Paper Width: " & objPrinter.PaperWidth / 254)
      'Wscript.Echo("Print Quality: " & objPrinter.PrintQuality)
      'Wscript.Echo("Scale: " & objPrinter.Scale)
      'Wscript.Echo("Specification Version: " & _
      '    objPrinter.SpecificationVersion)
      'If objPrinter.TTOption = 1 Then
      '  strTTOption = "Print TrueType fonts as graphics."
      'ElseIf objPrinter.TTOption = 2 Then
      '  strTTOption = "Download TrueType fonts as soft fonts."
      'Else
      '  strTTOption = "Substitute device fonts for TrueType fonts."
      'End If
      'Wscript.Echo("True Type Option: " & strTTOption)
      'Wscript.Echo("Vertical Resolution: " & objPrinter.VerticalResolution)
    Next
    Return list
  End Function

  Private Shared Function ConvertValue(ByVal value As Object, ByVal type As Type) As Object
    If value IsNot Nothing Then
      If type Is GetType(DateTime) Then
        Dim time As String = value.ToString()
        time = time.Substring(0, time.IndexOf("."))
        Return DateTime.ParseExact(time, "yyyyMMddHHmmss", Nothing)
      ElseIf type Is GetType(Long) Then
        Return Convert.ToInt64(value)
      ElseIf type Is GetType(Integer) Then
        Return Convert.ToInt32(value)
      ElseIf type Is GetType(Short) Then
        Return Convert.ToInt16(value)
      ElseIf type Is GetType(String) Then
        Return value.ToString()
      ElseIf type Is GetType(PrinterStatus) Then
        Return CType([Enum].Parse(GetType(PrinterStatus), value.ToString()), PrinterStatus)
      End If
    End If
    Return Nothing
  End Function
End Class
#End Region



