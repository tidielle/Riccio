Imports NTSInformatica
Imports NTSInformatica.CLN__STD

'Classe di supporto per il passaggio dei riferimenti di un DOCUMENTO 
Public Class NTSDoc
  Public strDitta As String = ""
  Public strTipoRk As String = ""
  Public lAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumero As Integer = 0
  Public lRiga As Integer = 0

  'Inizializzazione di una testata
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer)
    Me.New(strDitta, strTipoRk, lAnno, strSerie, lNumero, 0)
  End Sub
  'Inizializzazione di una riga
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                 ByVal lRiga As Integer)
    Me.strDitta = strDitta
    Me.strTipoRk = strTipoRk
    Me.lAnno = lAnno
    Me.strSerie = strSerie
    Me.lNumero = lNumero
    Me.lRiga = lRiga
  End Sub

  'Generazione della Where da utilizzare nelle query
  Public Function GeneraWhere() As String
    Dim strTabella As String
    If lRiga = 0 Then
      strTabella = "test" & RitornaSuffissoTabella()
    Else
      strTabella = "mov" & RitornaSuffissoTabella()
    End If

    Dim strPrefissoCampo As String = RitornaPrefissoColonna()

    Return " " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
           " AND " & strTabella & "." & strPrefissoCampo & "tipork = " & CStrSQL(strTipoRk) & _
           " AND " & strTabella & "." & strPrefissoCampo & "anno = " & lAnno & _
           " AND " & strTabella & "." & strPrefissoCampo & "serie = " & CStrSQL(strSerie) & _
           " AND " & strTabella & "." & strPrefissoCampo & "numdoc = " & lNumero & _
           IIf(lRiga <> 0, " AND " & strTabella & "." & strPrefissoCampo & "riga = " & lRiga, "")
  End Function

  'Ritorna la JOIN tra le righe e la testata del documento
  Public Function GeneraJoinTestataRighe(ByVal bLeftJoin As Boolean) As String
    Dim strSuffissoTabella As String = RitornaSuffissoTabella()
    Dim strTabella As String
    If lRiga <> 0 Then
      strTabella = "test" & RitornaSuffissoTabella()
    Else
      strTabella = "mov" & RitornaSuffissoTabella()
    End If

    Return IIf(bLeftJoin, " LEFT ", " INNER ") & " JOIN " & strTabella & " ON " & _
           " test" & strSuffissoTabella & ".codditt = mov" & strSuffissoTabella & ".codditt AND " & _
           " test" & strSuffissoTabella & ".tm_tipork = mov" & strSuffissoTabella & ".mm_tipork AND " & _
           " test" & strSuffissoTabella & ".tm_anno = mov" & strSuffissoTabella & ".mm_anno AND " & _
           " test" & strSuffissoTabella & ".tm_serie = mov" & strSuffissoTabella & ".mm_serie AND " & _
           " test" & strSuffissoTabella & ".tm_numdoc = mov" & strSuffissoTabella & ".mm_numdoc "
  End Function

  'Ritorna il suffisso del nome delle tabelle in base a se è una nota di prelievo o un altro documento di magazzino.
  Private Function RitornaSuffissoTabella() As String
    If strTipoRk = "W" Then Return "prb"
    Return "mag"
  End Function
  'Ritorna il prefisso delle colonne, in base a se deve essere di Testata o di Riga
  Private Function RitornaPrefissoColonna() As String
    If lRiga = 0 Then Return "tm_"
    Return "mm_"
  End Function
End Class

'Classe di supporto per il passaggio dei riferimenti di un ORDINE
Public Class NTSOrd
  Public strDitta As String = ""
  Public strTipoRk As String = ""
  Public lAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumero As Integer = 0
  Public lRiga As Integer = 0

  'Inizializzazione di una testata
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer)
    Me.New(strDitta, strTipoRk, lAnno, strSerie, lNumero, 0)
  End Sub
  'Inizializzazione di una riga
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                 ByVal lRiga As Integer)
    Me.strDitta = strDitta
    Me.strTipoRk = strTipoRk
    Me.lAnno = lAnno
    Me.strSerie = strSerie
    Me.lNumero = lNumero
    Me.lRiga = lRiga
  End Sub

  'Generazione della Where da utilizzare nelle query
  Public Function GeneraWhere() As String
    Dim strTabella As String
    If lRiga = 0 Then
      strTabella = "testord"
    Else
      strTabella = "movord"
    End If

    Dim strPrefissoCampo As String = RitornaPrefissoColonna()

    Return " " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
           " AND " & strTabella & "." & strPrefissoCampo & "tipork = " & CStrSQL(strTipoRk) & _
           " AND " & strTabella & "." & strPrefissoCampo & "anno = " & lAnno & _
           " AND " & strTabella & "." & strPrefissoCampo & "serie = " & CStrSQL(strSerie) & _
           " AND " & strTabella & "." & strPrefissoCampo & "numord = " & lNumero & _
           IIf(lRiga <> 0, " AND " & strTabella & "." & strPrefissoCampo & "riga = " & lRiga, "")
  End Function

  'Ritorna la JOIN con le righe e la testata del documento
  Public Function GeneraJoinTestataRighe(ByVal bLeftJoin As Boolean) As String
    Dim strTabella As String
    If lRiga <> 0 Then
      strTabella = "testord"
    Else
      strTabella = "movord"
    End If

    Return IIf(bLeftJoin, " LEFT ", " INNER ") & " JOIN " & strTabella & " ON " & _
           " testord.codditt = movord.codditt AND " & _
           " testord.td_tipork = movord.mo_tipork AND " & _
           " testord.td_anno = movord.mo_anno AND " & _
           " testord.td_serie = movord.mo_serie AND " & _
           " testord.td_numord = movord.mo_numord "
  End Function

  'Ritorna il prefisso delle colonne, in base a se deve essere di Testata o di Riga
  Private Function RitornaPrefissoColonna() As String
    If lRiga = 0 Then Return "td_"
    Return "mo_"
  End Function
End Class

'Classe di supporto per il passaggio dei riferimenti di una OFFERTA
Public Class NTSOff
  Public strDitta As String = ""
  Public strTipoRk As String = ""
  Public lAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumero As Integer = 0
  Public lVersione As Integer = 0
  Public lRiga As Integer = 0


  'Inizializzazione di una testata
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                 ByVal lVersione As Integer)
    Me.New(strDitta, strTipoRk, lAnno, strSerie, lNumero, lVersione, 0)
  End Sub
  'Inizializzazione di una riga
  Public Sub New(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                 ByVal lVersione As Integer, ByVal lRiga As Integer)
    Me.strDitta = strDitta
    Me.strTipoRk = strTipoRk
    Me.lAnno = lAnno
    Me.strSerie = strSerie
    Me.lNumero = lNumero
    Me.lVersione = lVersione
    Me.lRiga = lRiga
  End Sub

  'Generazione della Where da utilizzare nelle query
  Public Function GeneraWhere() As String
    Dim strTabella As String
    If lRiga = 0 Then
      strTabella = "testoff"
    Else
      strTabella = "movoff"
    End If

    Dim strPrefissoCampo As String = RitornaPrefissoColonna()

    Return " " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
           " AND " & strTabella & "." & strPrefissoCampo & "tipork = " & CStrSQL(strTipoRk) & _
           " AND " & strTabella & "." & strPrefissoCampo & "anno = " & lAnno & _
           " AND " & strTabella & "." & strPrefissoCampo & "serie = " & CStrSQL(strSerie) & _
           " AND " & strTabella & "." & strPrefissoCampo & "numord = " & lNumero & _
           " AND " & strTabella & "." & strPrefissoCampo & "vers = " & lVersione & _
           IIf(lRiga <> 0, " AND " & strTabella & "." & strPrefissoCampo & "riga = " & lRiga, "")
  End Function

  'Ritorna la JOIN con le righe del documento
  Public Function GeneraJoinTestataRighe(ByVal bLeftJoin As Boolean) As String
    Dim strTabella As String
    If lRiga <> 0 Then
      strTabella = "testoff"
    Else
      strTabella = "movoff"
    End If

    Return IIf(bLeftJoin, " LEFT ", " INNER ") & " JOIN " & strTabella & " ON " & _
           " testoff.codditt = movoff.codditt AND " & _
           " testoff.td_tipork = movoff.mo_tipork AND " & _
           " testoff.td_anno = movoff.mo_anno AND " & _
           " testoff.td_serie = movoff.mo_serie AND " & _
           " testoff.td_numord = movoff.mo_numord AND " & _
           " testoff.td_vers = movoff.mo_vers "
  End Function

  'Ritorna il prefisso delle colonne, in base a se deve essere di Testata o di Riga
  Private Function RitornaPrefissoColonna() As String
    If lRiga = 0 Then Return "td_"
    Return "mo_"
  End Function
End Class

