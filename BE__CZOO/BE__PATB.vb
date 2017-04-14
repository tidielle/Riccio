Public Class CLE__PATB
  Public CANCELZOOM As Boolean = False                    'se TRUE lo zoom non verrà visualizzato (è il caso di zoom richiamato da celle di griglia bloccate o zoom complessi richiamati da toolbar)
  Public strDescr As String = ""
  Public nAnno As Integer = 0                             'usato da tabcaus
  Public strCodPdc As String = ""                         'usato da taucauc, tabmast, tabgruc, tabclas
  Public nEscomp As Integer = 0                           'usato da tabcauc
  Public lContoCF As Integer = 0                          'usato da destdiv
  Public strCodart As String = ""
  Public nFase As Integer = 0
  Public strIn As String = ""                             'valore contenuto nel campo prima dello zoom
  Public strOut As String = ""                            'valore restituito dallo zoom
  Public oParam As Object = Nothing                       'oggetto per passaggio valori complessi sia in IN che in OUT

  Public bFlag1 As Boolean = False                        'genrico ...         

  'specifici per zoom anagra
  Public bVisGriglia As Boolean = True
  Public bTipoProposto As Boolean = True
  Public nMastro As Integer
  'Viene usato nel caso di reimpostazione filtri 
  '(per esempio da 'Stampe predefinite' - 
  ' in questo casa la struttura della riga deve corrispondere a quella di ARTICO.PARSTAF)
  Public rFiltriAnagra As Data.DataRow = Nothing

  'specifico per zoom articoli
  Public nMagaz As Integer = 0
  Public nListino As Integer = 1
  Public nTipologia As Integer = 1
  Public strTipoArticolo As String = ""
  Public strCodartAcc As String = ""
  Public bLiv2 As Boolean = False
  'Viene usato nel caso di reimpostazione filtri 
  '(per esempio da 'Stampe predefinite' - 
  ' in questo casa la struttura della riga deve corrispondere a quella di ARTICO.PARSTAF)
  Public rFiltriArtico As Data.DataRow = Nothing

  'specifico per zoom bice
  Public strTipoBil As String = ""
  Public nCodTipoRicl As Integer = 0
  Public strTipoSezione As String = ""

  'specifico per zoom sottocommesse
  Public lCommessa As Integer = 0

  'specifico per zoom banche
  Public nAbi As Integer = 0
  Public nCab As Integer = 0
  Public strBanc1 As String = ""
  Public strBanc2 As String = ""
  Public strRifriba As String = ""
  Public strCin As String = ""
  Public strPrefIBAN As String = ""
  Public strIBAN As String = ""
  Public strSwift As String = ""

  'specifico per zoom partitari
  Public nValuta As Integer = 0
  Public dCambio As Decimal = 0
  Public strIntegr As String = "N"
  Public strDatreg As String = ""
  Public lNumreg As Integer = 0
  Public nAnnpar As Integer = 0
  Public strAlfpar As String = ""
  Public lNumpar As Integer = 0
  Public dImporto As Decimal = 0
  Public dImportoval As Decimal = 0
  Public bStanziamenti As Boolean = False

  'per il passaggio di valori zoom via web
  Public oWebCtrlZoom As Object = Nothing
  Public oWebChild As Object = Nothing
  Public strWebNomeZoom As String = ""

  'usato da zoom anagra
  'usato da tabdica, tabtcdc
  Public strTipo As String = ""

  'specifico per zoom preventivi (Project Management)
  Public bEscludiAnnullati As Boolean = False    '(INGRESSO)
  Public lInstid As Integer = 0                  '(INGRESSO)
  'Public lCommessa As Integer = 0               '(INGRESSO) (già dichiarato)
  Public lPreNum As Integer = 0                  '(INGRESSO/USCITA)
  Public nPreRev As Integer = 0                  '(INGRESSO/USCITA)
  Public nVarId As Integer = 0                   '(USCITA)

  'codici primari tipo String    '
  'usato da tabdicv
  Public Property strCodice() As String
    Get
      Return strCodPdc
    End Get
    Set(ByVal strCodice As String)
      strCodPdc = strCodice
    End Set
  End Property
End Class
