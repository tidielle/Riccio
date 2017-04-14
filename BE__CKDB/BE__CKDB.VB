Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.IO.File

Public Class CLE__CKDB
  Inherits CLE__BASN


  Public oCldCkdb As CLD__CKDB 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__CKDB"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCkdb = CType(MyBase.ocldBase, CLD__CKDB)
    oCldCkdb.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      CaricaDataset(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABELLA").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABELLA").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      Return True
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
  Public Overridable Sub CaricaDataset(ByRef ds As DataSet)
    ' -------------------------------------------------------------------------------------
    ' Viene caricato in un vettore l'elenco dei controlli da svolgere, dove sono sctitte le
    ' query hard-coded
    '
    ' Descrizione dell'array dttCheck:
    '
    ' col1 = aree d'interesse (es. CG, MG, VE, PR, ..., cioè i moduli)
    ' col2 = tabelle interessate (informativo)
    ' col3 = descrizione del controllo
    ' col4 = tipo database (1=azienda/ditta, 2=arcproc)
    ' col5 = gravità (0=errore, 1=warning, 2=informazione)
    ' col6 = query comune o jet/funzione VBscript del controlli
    ' col7 = query specializzata SQL Server/stored procedures
    ' col8 = tipo di controllo (1=query/sp, 2=funzione VBScript)
    ' col9 = da eseguire quando (0=sempre) non più supportati 1=solo con database access, 2=solo in SQL/MSDE
    ' col10 = loggare se la query/script:
    '          0=restituisce un numero = di record da 'valore test' di col11
    '          1=restituisce un numero <> di record da 'valore test' di col11
    '          2=restituisce un numero < di record da 'valore test' di col11
    '          3=restituisce un numero > di record da 'valore test' di col11
    '          4=restituisce un numero <= di record da 'valore test' di col11
    '          5=restituisce un numero => di record da 'valore test' di col11
    '          6=il 1° campo della query è = a 'valore test' di col11
    '          7=il 1° campo della query è <> a 'valore test' di col11
    '          8=il 1° campo della query è < a 'valore test' di col11
    '          9=il 1° campo della query è > a 'valore test' di col11
    '          10=il 1° campo della query è <= a 'valore test' di col11
    '          11=il 1° campo della query è => a 'valore test' di col11
    '          12=il risultato della funzione VBSCRIPT è False
    '          13=il risultato della funzione VBSCRIPT è = a 'valore test' di col11
    '          14=il risultato della funzione VBSCRIPT è <> a 'valore test' di col11
    '          15=il risultato della funzione VBSCRIPT è < a 'valore test' di col11
    '          16=il risultato della funzione VBSCRIPT è > a 'valore test' di col11
    '          17=il risultato della funzione VBSCRIPT è <= a 'valore test' di col11
    '          18=il risultato della funzione VBSCRIPT è => a 'valore test' di col11
    ' col11 = valore test (vedi col10)
    ' col12 = numero massimo di righe di log per ciascun controllo (0=nessun limite)
    '           (valido per i controlli da 0 a 11)
    ' col13 = massaggio da scrivere nel log con marcatori (|0|, |1|, |2|, |n| per campi select,
    '            |return|, |<nomevariabile>| per le funzioni
    ' col14 = numero del controllo
    ' col15 = seleziona il lancio del controllo della colonna in griglia
    ' col16 = descrizione della colonna in griglia
    ' -------------------------------------------------------------------------------------
    Dim i As Integer
    Dim nIndex As Integer = 0
    Dim strSQL As String = ""
    Dim strListaCampi As String = ""

    Try
      ds.Tables.Add("TABELLA")

      With ds.Tables("TABELLA")
        .Columns.Add("aree", GetType(String))
        .Columns.Add("tabelle", GetType(String))
        .Columns.Add("descr", GetType(String))
        .Columns.Add("tipodb", GetType(String))
        .Columns.Add("gravita", GetType(String))
        .Columns.Add("query", GetType(String))
        .Columns.Add("querysql", GetType(String))
        .Columns.Add("tipoquery", GetType(String))
        .Columns.Add("daeseguirequando", GetType(String))
        .Columns.Add("tipolog", GetType(String))
        .Columns.Add("valoretest", GetType(String))
        .Columns.Add("maxrighelog", GetType(String))
        .Columns.Add("msg", GetType(String))
        .Columns.Add("numcheck", GetType(String))
        .Columns.Add("xx_seleziona", GetType(String))
        .Columns.Add("xx_descrizione", GetType(String))

        'Controllo 1: conguenza ordini di produzione, padre evaso con figlo inevaso
        .Rows.Add(New Object() {"PR,OR,DB", "movord", "Presenza di righe di ordini di produzioni (padri) evase, con righe di articoli figli non evase.", _
          "1", "1", "SELECT movordY.codditt, movordY.mo_tipork, movordY.mo_anno, movordY.mo_serie, movordY.mo_numord, movordY.mo_riga, movordH.mo_riga, movordY.mo_codart FROM movord AS movordY INNER JOIN movord AS movordH ON (movordY.mo_rigaor = movordH.mo_riga) AND (movordY.mo_numordor = movordH.mo_numord) AND (movordY.mo_serieor = movordH.mo_serie) AND (movordY.mo_annoor = movordH.mo_anno) AND (movordY.mo_tiporkor = movordH.mo_tipork) AND (movordY.codditt = movordH.codditt) WHERE movordY.mo_flevas='C' AND movordH.mo_flevas ='S' ORDER BY movordY.codditt, movordY.mo_tipork, movordY.mo_anno, movordY.mo_serie, movordY.mo_numord, movordY.mo_riga", _
          "", "1", "0", "1", "0", "0", "Errore nell'ordine di produzione, ditta |0|, anno |2|, n. |4|/|3|, riga |6|: tale riga risulta evasa mentre la riga del figlio |5| (articolo |7|) è inevasa.", _
          "1"})

        'Controllo 2: conguenza ordini di produzione, padre evaso con lavorazioni inevaso
        .Rows.Add(New Object() {"PR,OR,DB", "movord, attivit", "Presenza di righe di ordini di produzioni evase, con attività non evase.", _
          "1", "1", "SELECT attivit.codditt, attivit.at_anno, attivit.at_serie, attivit.at_numord, attivit.at_riga, attivit.at_fase FROM attivit INNER JOIN movord ON (attivit.at_riga = movord.mo_riga) AND (attivit.at_numord = movord.mo_numord) AND (attivit.at_serie = movord.mo_serie) AND (attivit.at_anno = movord.mo_anno) AND (attivit.at_tipork = movord.mo_tipork) AND (attivit.codditt = movord.codditt) WHERE attivit.at_flevas='C' and movord.mo_flevas='S' ORDER BY attivit.codditt, attivit.at_anno, attivit.at_serie, attivit.at_numord, attivit.at_riga, attivit.at_fase", _
          "", "1", "0", "1", "0", "0", "Errore nell'ordine di produzione, ditta |0|, anno |1|, n. |3|/|2|, riga |4|: la fase |5| è inevasa mentre la righe dell'ordine di produzione risulta già evasa.", _
          "2"})

        'Controllo 3: conguenza ordini di produzione, padre inevaso con figlo evaso
        .Rows.Add(New Object() {"PR,OR,DB", "movord", "Presenza di righe di ordini di produzioni (padri) non evase, con righe di articoli figli evase.", _
          "1", "1", "SELECT movordY.codditt, movordY.mo_tipork, movordY.mo_anno, movordY.mo_serie, movordY.mo_numord, movordY.mo_riga, movordH.mo_riga, movordY.mo_codart FROM movord AS movordY INNER JOIN movord AS movordH ON (movordY.mo_rigaor = movordH.mo_riga) AND (movordY.mo_numordor = movordH.mo_numord) AND (movordY.mo_serieor = movordH.mo_serie) AND (movordY.mo_annoor = movordH.mo_anno) AND (movordY.mo_tiporkor = movordH.mo_tipork) AND (movordY.codditt = movordH.codditt) WHERE movordY.mo_flevas='S' AND movordH.mo_flevas ='C' ORDER BY movordY.codditt, movordY.mo_tipork, movordY.mo_anno, movordY.mo_serie, movordY.mo_numord, movordY.mo_riga", _
          "", "1", "0", "1", "0", "0", "Errore nell'ordine di produzione, ditta |0|, anno |2|, n. |4|/|3|, riga |6|: tale riga risulta NON evasa mentre la riga del figlio |5| (articolo |7|) è evasa.", _
          "3"})

        'Controllo 4: conguenza ordini/documenti, attività non evasa, qtà lav+scart sup. all'ordinato
        .Rows.Add(New Object() {"PR,OR,DB", "attivitt, lavcent", "Presenza di attività inevase, sugli ordini di produzione, con lavorazioni già svolte dove la quantità (buona+scartata) supera o è uguale alla q.tà ordinata.", _
          "1", "1", "SELECT attivit.codditt, attivit.at_anno, attivit.at_serie, attivit.at_numord, attivit.at_riga, attivit.at_fase, Min(attivit.at_qtapr) AS MinDiat_qtapr, (Sum(lce_qtaes)+Sum(lce_qtascart)) AS Espr1 FROM lavcent INNER JOIN attivit ON (lavcent.lce_orfase = attivit.at_fase) AND (lavcent.lce_orriga = attivit.at_riga) AND (lavcent.lce_ornum = attivit.at_numord) AND (lavcent.lce_orserie = attivit.at_serie) AND (lavcent.lce_oranno = attivit.at_anno) AND (lavcent.lce_ortipo = attivit.at_tipork) AND (lavcent.codditt = attivit.codditt) WHERE (((attivit.at_flevas)='C') AND ((lavcent.lce_gnumdoc)=0)) GROUP BY attivit.codditt, attivit.at_anno, attivit.at_serie, attivit.at_numord, attivit.at_riga, attivit.at_fase, attivit.at_tipork HAVING (Sum(lce_qtaes)+Sum(lce_qtascart))>=Min(at_qtapr) ORDER BY attivit.codditt, attivit.at_tipork, attivit.at_anno, attivit.at_serie, attivit.at_numord, attivit.at_riga, attivit.at_fase", _
          "", "1", "0", "1", "0", "0", "Errore nell'ordine di produzione, ditta |0|, anno |1|, n. |3|/|2|, riga |4|: la fase |5| è inevasa pur in presenza di lavorazioni le cui quantità (buona+scarto) è superiore o uguale alla q.tà prevista (prevista |6|, lavorata |7|).", _
          "4"})

        'Controllo 5: conguenza documenti, dettaglio matricole con matricola indicata più di una volta
        .Rows.Add(New Object() {"VE", "movmatr", "Presenza dello stesso codice matricola nel dettaglio matricole delle righe documento.", _
          "1", "1", "SELECT codditt, mma_tipork, mma_anno, mma_numdoc, mma_serie, mma_riga, mma_matric, Count(codditt) As NumDuplicati FROM movmatr GROUP BY codditt, mma_tipork, mma_anno, mma_serie, mma_numdoc, mma_riga, mma_matric HAVING Count(codditt) > 1", _
          "", "1", "0", "1", "0", "0", "Errore nel documento, ditta |0|, tipo |1|, anno |2|, n. |3|/|4|: la matricola |6| è presente |7| volte nel dettaglio matricole relativo alla riga |5|.", _
          "5"})

        'Controllo 6:
        .Rows.Add(New Object() {"VE, OR", "movord, movmag", "Presenza di righe d'ordine dove la quantità evasa differisce dai movimenti di mag. di evasione.", _
          "1", "1", "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, Min(movord.mo_codart), Min(movord.mo_quant), Min(movord.mo_quaeva), Sum(movmag.mm_quant) AS Tot_Qta_Movmag " & _
                              "FROM movmag INNER JOIN movord ON (movmag.codditt = movord.codditt) AND (movmag.mm_ortipo = movord.mo_tipork) AND (movmag.mm_oranno = movord.mo_anno) AND (movmag.mm_orserie = movord.mo_serie) AND (movmag.mm_ornum = movord.mo_numord) AND (movmag.mm_orriga = movord.mo_riga) " & _
                              "GROUP BY movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga " & _
                              "HAVING (((Sum(movmag.mm_quant)) <> Min(movord.mo_quaeva))) " & _
                              "ORDER BY movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga", _
          "", "1", "0", "1", "0", "0", "Differenze sull'ordine, ditta |0|, tipo |1|, anno |2|, numero |4|/|3|, riga |5|, articolo |6|: quantità ordinata |7|, quantità evasa in ordine |8|, quantità evasa a magazzino |9|.", _
          "6"})

        'Controllo 8: articoli: presenza di fasi su articoli non a fase
        .Rows.Add(New Object() {"VE, OR, MG", "artico", "Articoli: presenza di fasi articoli su articoli NON a fase.", _
          "1", "0", "SELECT af_codart, af_fase FROM artico INNER JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart WHERE ar_gesfasi = 'N' ORDER BY af_codart, af_fase", _
          "", "1", "0", "1", "0", "0", "Presenza di fase |1| su articolo |0| NON gestito a fasi.", _
          "8"})

        'Controllo 9: articoli: controllo articoli anomali 
        .Rows.Add(New Object() {"VE, OR, MG", "artico", "Articoli: controllo articoli anomali.", _
          "1", "0", "SELECT Count(ar_codart) as conta FROM artico WHERE ar_codart IS NULL  or ar_codart='' or ar_codart=' '", _
          "", "1", "0", "1", "0", "0", "La query ha individuato la presenza di |0| articoli anomali nella anagrafica con codice articolo NULL, stringa vuota '' o blank ' '.", _
          "9"})

        'Controllo 10: listini: controllo chiave primaria logica
        .Rows.Add(New Object() {"VE", "listini", "Listini: violazione della chiave primaria logica.", _
          "1", "0", "SELECT codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase, Count(codditt) AS NumDuplicati, lc_codcas FROM listini GROUP BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase, lc_codcas HAVING Count(codditt)>1", _
          "", "1", "0", "1", "0", "0", "Sono presenti |11| record di listini con la stessa chiave: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|, casella |12|.", _
          "10"})

        'Controllo 11: listini: presenza di record su cod. root di articoli a varianti con prezzi sui dettagli variante o sulla 1. var.
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di record su cod. root di articoli a varianti con prezzi sui dettagli variante o sulla 1. variante", _
          "1", "1", "SELECT listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart WHERE artico.ar_gesvar = 'S' AND artico.ar_codroot IS NULL AND (artico.ar_prevar = 'S' OR artico.ar_prevar = '1')  ORDER BY listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase ", _
          "", "1", "0", "1", "0", "0", "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "11"})

        'Controllo 12: listini: presenza di record su dettaglio variante per articoli a varianti con prezzi comuni
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di record su dettaglio variante per articoli a varianti con prezzi comuni", _
          "1", "1", "SELECT listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart WHERE artico.ar_gesvar = 'S' AND artico.ar_codroot IS NOT NULL AND artico.ar_prevar = 'N'  ORDER BY listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase ", _
          "", "1", "0", "1", "0", "0", "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "12"})

        'Controllo 13: listini: presenza di record su dettaglio variante per articoli a varianti con prezzi sulla 1. variante
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di record su dettaglio variante per articoli a varianti con prezzi sulla 1. variante", _
          "1", "1", "SELECT listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart WHERE artico.ar_gesvar = 'S' AND   artico.ar_prevar = '1' AND lc_codart <> ar_codroot + ar_codvar1 ORDER BY listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase ", _
          "", "1", "0", "1", "0", "0", "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "13"})

        'Controllo 14: listini: presenza di record su unità di misura non ammesse in anagrafica articoli
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di record su unità di misura non ammesse in anagrafica articoli", _
          "1", "1", "SELECT DISTINCT listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND ((artico.ar_codart = listini.lc_codart) OR (artico.ar_codroot = listini.lc_codart) OR (artico.ar_codroot + artico.ar_codvar1 = listini.lc_codart)) WHERE lc_unmis not in( ar_unmis,ar_unmis2, ar_confez2 , ar_um4) ORDER BY listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase ", _
          "SELECT DISTINCT listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND ((artico.ar_codart = listini.lc_codart) OR (artico.ar_codroot = listini.lc_codart) OR (artico.ar_codroot + artico.ar_codvar1 = listini.lc_codart)) WHERE lc_unmis not in( COALESCE(ar_unmis,'?'),coalesce(ar_unmis2,'?'), COALESCE(ar_confez2,'?') , COALESCE(ar_um4,'?')) ORDER BY listini.codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_daquant, lc_unmis, lc_fase", _
          "1", "0", "1", "0", "0", "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "14"})

        'Controllo 15: Articoli: presenza di articoli gestiti a fase con il campo ultima fase a zero
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli a fase con ultima fase a zero", _
          "1", "1", "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_gesfasi = 'S' AND ar_ultfase = 0", _
          "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_gesfasi = 'S' AND ar_ultfase = 0", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "15"})

        'Controllo 16: Articoli: presenza di articoli gestiti a fase con il campo ultima fase non ammessa
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli a fase con ultima fase non ammessa", _
          "1", "1", "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart WHERE ar_gesfasi = 'S' AND ar_ultfase > 0 AND af_codart IS NULL", _
          "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart WHERE ar_gesfasi = 'S' AND ar_ultfase > 0 AND af_codart IS NULL", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "16"})

        'Controllo 17: Attività: presenza di attività (ZZATTIVIT) su proposte d'ordine di produzione inesistenti
        .Rows.Add(New Object() {"OR", "zzattivit", "Attività: presenza di attività (ZZATTIVIT) su proposte d'ordine di produzione inesistenti", _
          "1", "1", "SELECT DISTINCT zzattivit.codditt, at_progr, at_fase FROM zzattivit LEFT JOIN ordlist ON zzattivit.codditt = ordlist.codditt AND zzattivit.at_progr = ordlist.ol_progr WHERE ol_progr IS NULL", _
          "SELECT DISTINCT zzattivit.codditt, at_progr, at_fase FROM zzattivit LEFT JOIN ordlist ON zzattivit.codditt = ordlist.codditt AND zzattivit.at_progr = ordlist.ol_progr WHERE ol_progr IS NULL", _
          "1", "0", "1", "0", "0", "Attività anomala: ditta |0|, progressivo |1|, fase |2|.", _
          "17"})

        'Controllo 18: Attività: presenza di attività (ATTIVIT) su ordini di produzione inesistenti
        .Rows.Add(New Object() {"OR", "attivit", "Attività: presenza di attività (ATTIVIT) su ordini di produzione inesistenti", _
          "1", "1", "SELECT DISTINCT attivit.codditt, at_tipork, at_anno, at_serie, at_numord, at_riga, at_fase FROM attivit LEFT JOIN movord ON attivit.codditt = movord.codditt AND attivit.at_tipork = movord.mo_tipork AND attivit.at_anno = movord.mo_anno AND attivit.at_serie = movord.mo_serie AND attivit.at_numord = movord.mo_numord AND attivit.at_riga = movord.mo_riga WHERE mo_tipork IS NULL", _
          "SELECT DISTINCT attivit.codditt, at_tipork, at_anno, at_serie, at_numord, at_riga, at_fase FROM attivit LEFT JOIN movord ON attivit.codditt = movord.codditt AND attivit.at_tipork = movord.mo_tipork AND attivit.at_anno = movord.mo_anno AND attivit.at_serie = movord.mo_serie AND attivit.at_numord = movord.mo_numord AND attivit.at_riga = movord.mo_riga WHERE mo_tipork IS NULL", _
          "1", "0", "1", "0", "0", "Attività anomala: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, fase |6|.", _
          "18"})

        'Controllo 19: Progressivi corr. articoli: progressivi articoli (ARTPROX) disallineati con progressivi articoli/magazzino (ARTTPRO)
        .Rows.Add(New Object() {"MG", "artpro,artprox", "Progressivi corr. articoli: progressivi articoli (ARTPROX) disallineati con progressivi articoli/magazzino (ARTPRO)", _
          "1", "1", "SELECT artprox.codditt, artprox.apx_codart, artprox.apx_fase, Min(artprox.apx_esist) AS MinDiapx_esist, Sum(artpro.ap_esist) AS SommaDiap_esist " & _
                              "FROM artpro INNER JOIN artprox ON (artpro.codditt = artprox.codditt) AND (artpro.ap_codart = artprox.apx_codart) AND (artpro.ap_fase = artprox.apx_fase) " & _
                              "GROUP BY artprox.codditt, artprox.apx_codart, artprox.apx_fase " & _
                              "HAVING Abs(Min(artprox.apx_esist) - Sum(artpro.ap_esist)) >= 0.001 " & _
                              "ORDER BY artprox.codditt, artprox.apx_codart, artprox.apx_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, esistenza da ARTPROX |3|, somma esistenze da ARTPRO |4|.", _
          "19"})

        'Controllo 20: Progressivi corr. articoli: progressivi articoli/magazzino (ARTPRO) disallineati con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCPRO)
        .Rows.Add(New Object() {"MG", "artpro,lotcpro", "Progressivi corr. articoli: progressivi articoli/magazzino (ARTPRO) disallineati con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCPRO)", _
          "1", "1", "SELECT artpro.codditt, artpro.ap_codart, artpro.ap_fase, artpro.ap_magaz, Min(artpro.ap_esist) AS MinDiapx_esist, Sum(lotcpro.lp_esist) AS SommaDiap_esist " & _
                              "FROM artpro INNER JOIN lotcpro ON (artpro.codditt = lotcpro.codditt) AND (artpro.ap_codart = lotcpro.lp_codart) AND (artpro.ap_fase = lotcpro.lp_fase) AND (artpro.ap_magaz = lotcpro.lp_magaz) " & _
                              "GROUP BY artpro.codditt, artpro.ap_codart, artpro.ap_fase, artpro.ap_magaz " & _
                              "HAVING Abs(Min(artpro.ap_esist) - Sum(lotcpro.lp_esist)) >= 0.001 " & _
                              "ORDER BY artpro.codditt, artpro.ap_codart, artpro.ap_fase, artpro.ap_magaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|, esistenza da ARTPRO |4|, somma esistenze da LOTCPRO |5|.", _
          "20"})

        'Controllo 21: Progressivi def. articoli: record progressivi articoli (ARTDEFX) presenti con progressivi articoli/magazzino (ARTDEF) inesistenti
        .Rows.Add(New Object() {"MG", "artdef,artdefx", "Progressivi def. articoli: record progressivi articoli (ARTDEFX) presenti con progressivi articoli/magazzino (ARTDEF) inesistenti", _
          "1", "1", "SELECT artdefx.codditt, artdefx.adx_codart, artdefx.adx_fase " & _
                              "FROM artdefx LEFT JOIN artdef ON (artdef.codditt = artdefx.codditt) AND (artdef.ad_codart = artdefx.adx_codart) AND (artdef.ad_fase = artdefx.adx_fase) " & _
                              "WHERE artdef.ad_fase IS Null " & _
                              "ORDER BY artdefx.codditt, artdefx.adx_codart, artdefx.adx_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "21"})

        'Controllo 22: Progressivi def. articoli: record progressivi articoli/magazzino (ARTDEF) presenti con progressivi articoli (ARTDEFX) inesistenti
        .Rows.Add(New Object() {"MG", "artdef,artdefx", "Progressivi def. articoli: record progressivi articoli/magazzino (ARTDEF) presenti con progressivi articoli (ARTDEFX) inesistenti", _
          "1", "1", "SELECT artdef.codditt, artdef.ad_codart, artdef.ad_fase " & _
                              "FROM artdef LEFT JOIN artdefx ON (artdef.codditt = artdefx.codditt) AND (artdef.ad_codart = artdefx.adx_codart) AND (artdef.ad_fase = artdefx.adx_fase) " & _
                              "WHERE artdefx.adx_fase IS Null " & _
                              "GROUP BY artdef.codditt, artdef.ad_codart, artdef.ad_fase " & _
                              "ORDER BY artdef.codditt, artdef.ad_codart, artdef.ad_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "22"})

        'Controllo 23: Progressivi def. articoli: progressivi articoli (ARTDEFX) disallineati con progressivi articoli/magazzino (ARTDEF)
        .Rows.Add(New Object() {"MG", "artdef,artdefx", "Progressivi def. articoli: progressivi articoli (ARTDEFX) disallineati con progressivi articoli/magazzino (ARTDEF)", _
          "1", "1", "SELECT artdefx.codditt, artdefx.adx_codart, artdefx.adx_fase, Min(artdefx.adx_esist), Sum(artdef.ad_esist) " & _
                              "FROM artdef INNER JOIN artdefx ON (artdef.codditt = artdefx.codditt) AND (artdef.ad_codart = artdefx.adx_codart) AND (artdef.ad_fase = artdefx.adx_fase) " & _
                              "GROUP BY artdefx.codditt, artdefx.adx_codart, artdefx.adx_fase " & _
                              "HAVING Abs(Min(artdefx.adx_esist) - Sum(artdef.ad_esist)) >= 0.001 " & _
                              "ORDER BY artdefx.codditt, artdefx.adx_codart, artdefx.adx_fase ", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, esistenza da ARTDEFX |3|, somma esistenze da ARTDEF |4|.", _
          "23"})

        'Controllo 24: Progressivi def. articoli: record progressivi articoli/magazzino (ARTDEF) presenti con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) inesistenti
        .Rows.Add(New Object() {"MG", "artdef,lotcdef", "Progressivi def. articoli: record progressivi articoli/magazzino (ARTDEF) presenti con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) inesistenti", _
          "1", "1", "SELECT artdef.codditt, artdef.ad_codart, artdef.ad_fase, artdef.ad_magaz " & _
                              "FROM artdef LEFT JOIN lotcdef ON (artdef.codditt = lotcdef.codditt) AND (artdef.ad_magaz = lotcdef.ld_magaz) AND (artdef.ad_codart = lotcdef.ld_codart) AND (artdef.ad_fase = lotcdef.ld_fase) " & _
                              "WHERE lotcdef.ld_fase IS Null " & _
                              "ORDER BY artdef.codditt, artdef.ad_codart, artdef.ad_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|.", _
          "24"})

        'Controllo 25: Progressivi def. articoli: record progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) presenti con progressivi articoli/magazzino (ARTDEF) inesistenti
        .Rows.Add(New Object() {"MG", "artdef,lotcdef", "Progressivi def. articoli: record progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) presenti con progressivi articoli/magazzino (ARTDEF) inesistenti", _
          "1", "1", "SELECT lotcdef.codditt, lotcdef.ld_codart, lotcdef.ld_fase, lotcdef.ld_magaz" & _
          " FROM lotcdef LEFT JOIN artdef ON (artdef.codditt = lotcdef.codditt) AND (artdef.ad_magaz = lotcdef.ld_magaz) AND (artdef.ad_codart = lotcdef.ld_codart) AND (artdef.ad_fase = lotcdef.ld_fase)" & _
          " WHERE artdef.ad_fase IS Null" & _
          " GROUP BY lotcdef.codditt, lotcdef.ld_codart, lotcdef.ld_fase, lotcdef.ld_magaz" & _
          " ORDER BY lotcdef.codditt, lotcdef.ld_codart, lotcdef.ld_fase, lotcdef.ld_magaz", _
          "", "1", "0", "1", "0", "0", _
          "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|.", _
          "25"})

        'Controllo 26: Progressivi def. articoli: progressivi articoli/magazzino (ARTDEF) disallineati con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF)
        .Rows.Add(New Object() {"MG", "artdef,lotcdef", "Progressivi def. articoli: progressivi articoli/magazzino (ARTDEF) disallineati con progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF)", _
          "1", "1", "SELECT artdef.codditt, artdef.ad_codart, artdef.ad_fase, artdef.ad_magaz, Min(artdef.ad_esist) AS MinDiapx_esist, Sum(lotcdef.ld_quant) AS SommaDiap_esist " & _
                              "FROM artdef INNER JOIN lotcdef ON (artdef.codditt = lotcdef.codditt) AND (artdef.ad_codart = lotcdef.ld_codart) AND (artdef.ad_fase = lotcdef.ld_fase) AND (artdef.ad_magaz = lotcdef.ld_magaz) " & _
                              "GROUP BY artdef.codditt, artdef.ad_codart, artdef.ad_fase, artdef.ad_magaz " & _
                              "HAVING Abs(Min(artdef.ad_esist) - Sum(lotcdef.ld_quant)) >= 0.001 " & _
                              "ORDER BY artdef.codditt, artdef.ad_codart, artdef.ad_fase, artdef.ad_magaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|, esistenza da ARTDEF |4|, somma esistenze da LOTCDEF |5|.", _
          "26"})

        'Controllo 27: Progressivi corr. articolo/magazzino: ordinato e/o impegnato (ARTPRO) disallineato con le righe d'ordine inevase (MOVORD)
        .Rows.Add(New Object() {"OR", "artpro", "Progressivi corr. articolo/magazzino: ordinato e/o impegnato (ARTPRO) disallineato con le righe d'ordine inevase (MOVORD)", _
          "1", "1", "SELECT keyord.codditt, keyord.ko_codart, keyord.ko_fase, keyord.ko_magaz, Sum((mo_quant-mo_quaeva)*ko_ordin) AS OrdinatoMovord, Min(ap_ordin) AS OrdinatoArtpro, Sum((mo_quant-mo_quaeva)*ko_impeg) AS ImpegnatoMovord, Min(ap_impeg) AS ImpegnatoArtpro " & _
                              "FROM (movord INNER JOIN keyord ON (movord.codditt = keyord.codditt) AND (movord.mo_riga = keyord.ko_riga) AND (movord.mo_tipork = keyord.ko_tipork) AND (movord.mo_serie = keyord.ko_serie) AND (movord.mo_anno = keyord.ko_anno) AND (movord.mo_numord = keyord.ko_numord)) LEFT JOIN artpro ON (keyord.ko_fase = artpro.ap_fase) AND (keyord.ko_magaz = artpro.ap_magaz) AND (keyord.ko_codart = artpro.ap_codart) AND (keyord.codditt = artpro.codditt) " & _
                              "WHERE (((movord.mo_flevas)='C')) " & _
                              "GROUP BY keyord.codditt, keyord.ko_codart, keyord.ko_fase, keyord.ko_magaz " & _
                              "HAVING Round(Sum((mo_quant - mo_quaeva) * ko_ordin), 3) <> Round(Min(ap_ordin), 3) Or Round(Sum((mo_quant - mo_quaeva) * ko_impeg), 3) <> Round(Min(ap_impeg), 3) " & _
                              "ORDER BY keyord.codditt, keyord.ko_codart, keyord.ko_fase, keyord.ko_magaz ", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|, ordinato MOVORD |4|, ordinato ARTPRO |5|, impegnato MOVORD |6|, impegnato ARTPRO |7|.", _
          "27"})

        'Controllo 28: Progressivi def. articoli T&C: progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) su articoli a 'Taglie e Colori' senza dettaglio i LOTCDEFTC
        .Rows.Add(New Object() {"MG", "lotcdef,lotcdeftc", "Progressivi def. articoli T&C: record progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) su articoli a 'Taglie e Colori' senza dettaglio i LOTCDEFTC", _
          "1", "1", "SELECT lotcdef.codditt, ld_codart, ld_magaz, ld_commeca," & _
                              " ld_lotto, ld_fase, ld_ubicaz" & _
                              " FROM (artico INNER JOIN lotcdef ON artico.codditt = lotcdef.codditt AND artico.ar_codart = lotcdef.ld_codart)" & _
                              " LEFT JOIN lotcdeftc ON lotcdef.codditt = lotcdeftc.codditt AND lotcdef.ld_codart = lotcdeftc.adt_codart AND lotcdef.ld_magaz = lotcdeftc.adt_magaz AND lotcdef.ld_commeca = lotcdeftc.adt_commeca AND lotcdef.ld_lotto = lotcdeftc.adt_lotto AND lotcdef.ld_fase = lotcdeftc.adt_fase AND lotcdef.ld_ubicaz = lotcdeftc.adt_ubicaz" & _
                              " WHERE ar_codtagl <> 0" & _
                              " AND lotcdeftc.adt_codart IS NULL" & _
                              " ORDER BY lotcdef.codditt, ld_codart, ld_magaz, ld_commeca, ld_lotto, ld_fase, ld_ubicaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, commessa |3|, lotto |4|, fase |5|, ubicazione |6|.", _
          "28"})

        'Controllo 29: Progressivi def. articoli T&C: dettaglio progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEFTC) su articoli a 'Taglie e Colori' senza LOTCDEF
        .Rows.Add(New Object() {"MG", "lotcdef,lotcdeftc", "Progressivi def. articoli T&C: record dettaglio progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEFTC) su articoli a 'Taglie e Colori' senza LOTCDEF", _
          "1", "1", "SELECT lotcdeftc.codditt, adt_codart, adt_magaz, adt_commeca," & _
                              " adt_lotto, adt_fase, adt_ubicaz" & _
                              " FROM (artico INNER JOIN lotcdeftc ON artico.codditt = lotcdeftc.codditt AND artico.ar_codart = lotcdeftc.adt_codart)" & _
                              " LEFT JOIN lotcdef ON lotcdeftc.codditt = lotcdef.codditt AND lotcdeftc.adt_codart = lotcdef.ld_codart AND lotcdeftc.adt_magaz = lotcdef.ld_magaz AND lotcdeftc.adt_commeca = lotcdef.ld_commeca AND lotcdeftc.adt_lotto = lotcdef.ld_lotto AND lotcdeftc.adt_fase = lotcdef.ld_fase AND lotcdeftc.adt_ubicaz = lotcdef.ld_ubicaz" & _
                              " WHERE ar_codtagl <> 0" & _
                              " AND lotcdef.ld_codart IS NULL" & _
                              " ORDER BY lotcdeftc.codditt, adt_codart, adt_magaz, adt_commeca, adt_lotto," & _
                              " adt_fase, adt_ubicaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, commessa |3|, lotto |4|, fase |5|, ubicazione |6|.", _
          "29"})

        'Controllo 30: Progressivi def. articoli T&C: progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) su articoli a 'Taglie e Colori' disallineati con il dettaglio in LOTCDEFTC
        strSQL = "SELECT lotcdef.codditt, ld_codart, ld_magaz, ld_commeca, ld_lotto, ld_fase, ld_ubicaz" & _
          " FROM lotcdef INNER JOIN lotcdeftc ON lotcdef.codditt = lotcdeftc.codditt AND lotcdef.ld_codart = lotcdeftc.adt_codart AND lotcdef.ld_magaz = lotcdeftc.adt_magaz AND lotcdef.ld_commeca = lotcdeftc.adt_commeca AND lotcdef.ld_lotto = lotcdeftc.adt_lotto AND lotcdef.ld_fase = lotcdeftc.adt_fase AND lotcdef.ld_ubicaz = lotcdeftc.adt_ubicaz" & _
          " WHERE ld_quant <> ("
        For i = 1 To 24
          strSQL = strSQL & "adt_esist" & Right("0" & NTSCStr(i), 2) & NTSCStr(IIf(i < 24, " + ", ")"))
        Next i
        strSQL = strSQL & " ORDER BY lotcdef.codditt, ld_codart, ld_magaz, ld_commeca, ld_lotto, ld_fase, ld_ubicaz"

        .Rows.Add(New Object() {"MG", "lotcdef,lotcdeftc", "Progressivi def. articoli T&C: record progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEF) su articoli a 'Taglie e Colori' disallineati con il dettaglio in LOTCDEFTC", _
          "1", "1", strSQL, _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, commessa |3|, lotto |4|, fase |5|, ubicazione |6|.", _
          "30"})

        'Controllo 31: Progressivi def. articoli T&C: progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEFTC) su articoli NON a 'Taglie e Colori'
        .Rows.Add(New Object() {"MG", "lotcdef,artico", "Progressivi def. articoli T&C: record progressivi articoli/magazzino/lotto/commessa/ubicazione (LOTCDEFTC) su articoli NON a 'Taglie e Colori'", _
          "1", "1", "SELECT lotcdeftc.codditt, adt_codart, adt_magaz, adt_commeca," & _
                              " adt_lotto, adt_fase, adt_ubicaz" & _
                              " FROM artico INNER JOIN lotcdeftc ON artico.codditt = lotcdeftc.codditt AND artico.ar_codart = lotcdeftc.adt_codart" & _
                              " WHERE ar_codtagl = 0" & _
                              " ORDER BY lotcdeftc.codditt, adt_codart, adt_magaz, adt_commeca, adt_lotto, adt_fase, adt_ubicaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, commessa |3|, lotto |4|, fase |5|, ubicazione |6|.", _
          "31"})

        'Controllo 32: Progressivi corr. articolo/magazzino: ordinato (ARTPRO) diverso da zero, in mancanza di righe d'ordine da evadere (MOVORD)
        .Rows.Add(New Object() {"OR", "artpro, movord", "Progressivi corr. articolo/magazzino: ordinato (ARTPRO) diverso da zero, in mancanza di righe d'ordine da evadere (MOVORD)", _
          "1", "1", "SELECT codditt, ap_codart, ap_fase, ap_magaz, ap_ordin " & _
                                    "FROM artpro " & _
                                    "WHERE Round(ap_ordin, 3) <> 0 AND NOT EXISTS " & _
                                    "(SELECT movord.mo_numord" & _
                                    " FROM (movord INNER JOIN keyord ON (movord.codditt = keyord.codditt) AND (movord.mo_riga = keyord.ko_riga) " & _
                                    " AND (movord.mo_tipork = keyord.ko_tipork) AND (movord.mo_serie = keyord.ko_serie) " & _
                                    " AND (movord.mo_anno = keyord.ko_anno) AND (movord.mo_numord = keyord.ko_numord)) " & _
                                    " WHERE movord.mo_flevas='C' AND keyord.ko_ordin = 1 AND (keyord.ko_fase = artpro.ap_fase) " & _
                                    " AND (keyord.ko_magaz = artpro.ap_magaz) " & _
                                    " AND (keyord.ko_codart = artpro.ap_codart) AND (keyord.codditt = artpro.codditt)) " & _
                                    "ORDER BY codditt, ap_codart, ap_fase, ap_magaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|, ordinato ARTPRO |4|", _
          "32"})

        'Controllo 33: Progressivi corr. articolo/magazzino: impegnato (ARTPRO) diverso da zero, in mancanza di righe d'ordine da evadere (MOVORD)
        .Rows.Add(New Object() {"OR", "artpro, movord", "Progressivi corr. articolo/magazzino: impegnato (ARTPRO) diverso da zero, in mancanza di righe d'ordine da evadere (MOVORD)", _
          "1", "1", "SELECT codditt, ap_codart, ap_fase, ap_magaz, ap_impeg " & _
                                    "FROM artpro " & _
                                    "WHERE Round(ap_impeg, 3) <> 0 AND NOT EXISTS " & _
                                    "(SELECT movord.mo_numord" & _
                                    " FROM (movord INNER JOIN keyord ON (movord.codditt = keyord.codditt) AND (movord.mo_riga = keyord.ko_riga) " & _
                                    " AND (movord.mo_tipork = keyord.ko_tipork) AND (movord.mo_serie = keyord.ko_serie) " & _
                                    " AND (movord.mo_anno = keyord.ko_anno) AND (movord.mo_numord = keyord.ko_numord)) " & _
                                    " WHERE movord.mo_flevas='C' AND keyord.ko_impeg = 1 AND (keyord.ko_fase = artpro.ap_fase) " & _
                                    " AND (keyord.ko_magaz = artpro.ap_magaz) " & _
                                    " AND (keyord.ko_codart = artpro.ap_codart) AND (keyord.codditt = artpro.codditt)) " & _
                                    "ORDER BY codditt, ap_codart, ap_fase, ap_magaz", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|, magazzino |3|, impegnato ARTPRO |4|", _
          "33"})

        'Controllo 34: Progressivi def. articoli T&C: progressivi articoli/magazzino (ARTDEF) su articoli a 'Taglie e Colori' senza dettaglio i ARTDEFTC
        .Rows.Add(New Object() {"MG", "artdef,artdeftc,artico", "Progressivi def. articoli T&C: record progressivi articoli/magazzino (ARTDEF) su articoli a 'Taglie e Colori' senza dettaglio i ARTDEFTC", _
          "1", "1", "SELECT artdef.codditt, ad_codart, ad_magaz, ad_fase" & _
                    " FROM (artico INNER JOIN artdef ON artico.codditt = artdef.codditt AND artico.ar_codart = artdef.ad_codart)" & _
                    " LEFT JOIN artdeftc ON artdef.codditt = artdeftc.codditt AND artdef.ad_codart = artdeftc.adt_codart AND artdef.ad_magaz = artdeftc.adt_magaz AND artdef.ad_fase = artdeftc.adt_fase" & _
                    " WHERE ar_codtagl <> 0" & _
                    " AND artdeftc.adt_codart IS NULL" & _
                    " ORDER BY artdef.codditt, ad_codart, ad_magaz, ad_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, fase |3|.", _
          "34"})

        'Controllo 35: Progressivi def. articoli T&C: dettaglio progressivi articoli/magazzino (ARTDEFTC) su articoli a 'Taglie e Colori' senza ARTDEF
        .Rows.Add(New Object() {"MG", "artdef,artdeftc,artico", "Progressivi def. articoli T&C: record dettaglio progressivi articoli/magazzino (ARTDEFTC) su articoli a 'Taglie e Colori' senza ARTDEF", _
          "1", "1", "SELECT artdeftc.codditt, adt_codart, adt_magaz, adt_fase" & _
                    " FROM (artico INNER JOIN artdeftc ON artico.codditt = artdeftc.codditt AND artico.ar_codart = artdeftc.adt_codart)" & _
                    " LEFT JOIN artdef ON artdef.codditt = artdeftc.codditt AND artdef.ad_codart = artdeftc.adt_codart AND artdef.ad_magaz = artdeftc.adt_magaz AND artdef.ad_fase = artdeftc.adt_fase" & _
                    " WHERE ar_codtagl <> 0" & _
                    " AND artdef.ad_codart IS NULL" & _
                    " ORDER BY artdeftc.codditt, adt_codart, adt_magaz, adt_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, fase |3|.", _
          "35"})

        'Controllo 36: Progressivi def. articoli T&C: progressivi articoli/magazzino (ARTDEF) su articoli a 'Taglie e Colori' disallineati con il dettaglio in ARTDEFTC
        strSQL = "SELECT artdef.codditt, ad_codart, ad_magaz, ad_fase" & _
                 " FROM artdef INNER JOIN artdeftc ON artdef.codditt = artdeftc.codditt AND artdef.ad_codart = artdeftc.adt_codart AND artdef.ad_magaz = artdeftc.adt_magaz AND artdef.ad_fase = artdeftc.adt_fase" & _
                 " WHERE ad_esist <> ("
        For i = 1 To 24
          strSQL = strSQL & "adt_esist" & Right("0" & NTSCStr(i), 2) & NTSCStr(IIf(i < 24, " + ", ")"))
        Next i
        strSQL = strSQL & " ORDER BY artdef.codditt, ad_codart, ad_magaz, ad_fase"
        .Rows.Add(New Object() {"MG", "artdef,artdeftc", "Progressivi def. articoli T&C: record progressivi articoli/magazzino (ARTDEF) su articoli a 'Taglie e Colori' disallineati con il dettaglio in ARTDEFTC", _
          "1", "1", strSQL, _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, fase |3|.", _
          "36"})

        'Controllo 37: Progressivi def. articoli T&C: progressivi articoli/magazzino (ARTDEFTC) su articoli NON a 'Taglie e Colori'
        .Rows.Add(New Object() {"MG", "artdeftc,artico", "Progressivi def. articoli T&C: record progressivi articoli/magazzino (ARTDEFTC) su articoli NON a 'Taglie e Colori'", _
          "1", "1", "SELECT artdeftc.codditt, adt_codart, adt_magaz, adt_fase" & _
                    " FROM artico INNER JOIN artdeftc ON artico.codditt = artdeftc.codditt AND artico.ar_codart = artdeftc.adt_codart" & _
                    " WHERE ar_codtagl = 0" & _
                    " ORDER BY artdeftc.codditt, adt_codart, adt_magaz, adt_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, fase |3|.", _
          "37"})

        'Controllo 38: Progressivi def. articoli T&C: progressivi articoli (ARTDEFX) su articoli a 'Taglie e Colori' senza dettaglio i ARTDEFXTC
        .Rows.Add(New Object() {"MG", "artdefx,artdeftcx,artico", "Progressivi def. articoli T&C: record progressivi articoli (ARTDEFX) su articoli a 'Taglie e Colori' senza dettaglio i ARTDEFXTC", _
          "1", "1", "SELECT artdefx.codditt, adx_codart, adx_fase" & _
                    " FROM (artico INNER JOIN artdefx ON artico.codditt = artdefx.codditt AND artico.ar_codart = artdefx.adx_codart)" & _
                    " LEFT JOIN artdefxtc ON artdefx.codditt = artdefxtc.codditt AND artdefx.adx_codart = artdefxtc.adt_codart AND artdefx.adx_fase = artdefxtc.adt_fase" & _
                    " WHERE ar_codtagl <> 0" & _
                    " AND artdefxtc.adt_codart IS NULL" & _
                    " ORDER BY artdefx.codditt, adx_codart, adx_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "38"})

        'Controllo 39: Progressivi def. articoli T&C: dettaglio progressivi articoli (ARTDEFXTC) su articoli a 'Taglie e Colori' senza ARTDEFX
        .Rows.Add(New Object() {"MG", "artdefx,artdefxtc,artico", "Progressivi def. articoli T&C: record dettaglio progressivi articoli (ARTDEFXTC) su articoli a 'Taglie e Colori' senza ARTDEFX", _
          "1", "1", "SELECT artdefxtc.codditt, adt_codart, adt_fase" & _
                    " FROM (artico INNER JOIN artdefxtc ON artico.codditt = artdefxtc.codditt AND artico.ar_codart = artdefxtc.adt_codart)" & _
                    " LEFT JOIN artdefx ON artdefx.codditt = artdefxtc.codditt AND artdefx.adx_codart = artdefxtc.adt_codart AND artdefx.adx_fase = artdefxtc.adt_fase" & _
                    " WHERE ar_codtagl <> 0" & _
                    " AND artdefx.adx_codart IS NULL" & _
                    " ORDER BY artdefxtc.codditt, adt_codart, adt_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "39"})

        'Controllo 40: Progressivi def. articoli T&C: progressivi articoli (ARTDEFX) su articoli a 'Taglie e Colori' disallineati con il dettaglio in ARTDEFXTC
        strSQL = "SELECT artdefx.codditt, adx_codart, adx_fase" & _
                 " FROM artdefx INNER JOIN artdefxtc ON artdefx.codditt = artdefxtc.codditt AND artdefx.adx_codart = artdefxtc.adt_codart AND artdefx.adx_fase = artdefxtc.adt_fase" & _
                 " WHERE adx_esist <> ("
        For i = 1 To 24
          strSQL = strSQL & "adt_esist" & Right("0" & NTSCStr(i), 2) & NTSCStr(IIf(i < 24, " + ", ")"))
        Next i
        strSQL = strSQL & " ORDER BY artdefx.codditt, adx_codart, adx_fase"

        .Rows.Add(New Object() {"MG", "artdefx,artdefxtc", "Progressivi def. articoli T&C: record progressivi articoli (ARTDEFX) su articoli a 'Taglie e Colori' disallineati con il dettaglio in ARTDEFXTC", _
          "1", "1", strSQL, _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "40"})

        'Controllo 41: Progressivi def. articoli: progressivi articoli (ARTDEFX) Presenza di valore esistenza iniziale <> 0 con quantità a zero
        strSQL = "SELECT codditt, adx_codart, adx_fase FROM artdefx" & _
          " WHERE (adx_esist = 0 AND adx_valesi <> 0)" & _
          " OR (adx_lesist = 0 AND adx_lvalesi <> 0)" & _
          " OR (adx_giaini = 0 AND adx_vgiaini <> 0)" & _
          " OR (adx_lgiaini = 0 AND adx_lvgiaini <> 0)" & _
          " ORDER BY codditt, adx_codart, adx_fase"
        .Rows.Add(New Object() {"MG", "artdefx", _
          "Progressivi def. articoli: record progressivi articoli (ARTDEFX) su articoli con valore esistenza iniziale <> 0 e quantità a zero", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", "41"})

        'Controllo 42: Progressivi def. articoli: progressivi articoli (ARTDEF) Presenza di valore esistenza iniziale <> 0 con quantità a zero
        strSQL = "SELECT codditt, ad_codart, ad_magaz, ad_fase FROM artdef" & _
          " WHERE (ad_esist = 0 AND ad_valesi <> 0)" & _
          " OR (ad_giaini = 0 AND ad_vgiaini <> 0)" & _
          " ORDER BY codditt, ad_codart, ad_magaz, ad_fase"
        .Rows.Add(New Object() {"MG", "artdef", _
          "Progressivi def. articoli: record progressivi articoli (ARTDEF) su articoli con valore esistenza iniziale <> 0 e quantità a zero", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, fase |3|.", "42"})

        'Controllo 43: Progressivi def. articoli: progressivi articoli (LOTCDEF) Presenza di valore esistenza iniziale <> 0 con quantità a zero
        strSQL = "SELECT codditt, ld_codart, ld_magaz, ld_commeca, ld_lotto, ld_fase, ld_ubicaz FROM lotcdef" & _
          " WHERE ld_giaini = 0" & _
          " AND ld_vgiaini <> 0" & _
          " ORDER BY codditt, ld_codart, ld_magaz, ld_commeca, ld_lotto, ld_fase, ld_ubicaz"
        .Rows.Add(New Object() {"MG", "artdef", _
          "Progressivi def. articoli: record progressivi articoli (LOTCDEF) su articoli con valore esistenza iniziale <> 0 e quantità a zero", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Progres. articolo anomalo: ditta |0|, articolo |1|, magazzino |2|, commessa |3|, lotto |4|, fase |5|, ubicazione |6|.", "43"})

        'Controllo 47: Progressivi def. articoli T&C: progressivi articoli (ARTDEFXTC) su articoli NON a 'Taglie e Colori'
        .Rows.Add(New Object() {"MG", "artdefxtc,artico", "Progressivi def. articoli T&C: record progressivi articoli (ARTDEFXTC) su articoli NON a 'Taglie e Colori'", _
          "1", "1", "SELECT artdefxtc.codditt, adt_codart, adt_fase" & _
                    " FROM artico INNER JOIN artdefxtc ON artico.codditt = artdefxtc.codditt AND artico.ar_codart = artdefxtc.adt_codart" & _
                    " WHERE ar_codtagl = 0" & _
                    " ORDER BY artdefxtc.codditt, adt_codart, adt_fase", _
          "", "1", "0", "1", "0", "0", "Progres. articolo anomalo: ditta |0|, articolo |1|, fase |2|.", _
          "47"})

        'Controllo 48: Movimentazione articoli T&C: righe di MOVMAG su articoli a 'Taglie e Colori' con dettaglio quantità MOVMAGTC diverso
        .Rows.Add(New Object() {"MG", "movmag,movmagtc", "Movimentazione articoli T&C: righe di MOVMAG su articoli a 'Taglie e Colori' con dettaglio quantità MOVMAGTC diverso", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga," & _
                    " movmag.mm_quant," & _
                    " (movmag.mm_quant-(movmagtc.mm_quant01+movmagtc.mm_quant02+movmagtc.mm_quant03+movmagtc.mm_quant04+movmagtc.mm_quant05+movmagtc.mm_quant06+movmagtc.mm_quant07+movmagtc.mm_quant08+movmagtc.mm_quant09+movmagtc.mm_quant10+movmagtc.mm_quant11+movmagtc.mm_quant12+movmagtc.mm_quant13+movmagtc.mm_quant14+movmagtc.mm_quant15+movmagtc.mm_quant16+movmagtc.mm_quant17+movmagtc.mm_quant18+movmagtc.mm_quant19+movmagtc.mm_quant20+movmagtc.mm_quant21+movmagtc.mm_quant22+movmagtc.mm_quant23+movmagtc.mm_quant24)) AS diff" & _
                    " FROM movmag INNER JOIN movmagtc ON (movmag.codditt = movmagtc.codditt) AND (movmag.mm_tipork = movmagtc.mm_tipork) AND (movmag.mm_anno = movmagtc.mm_anno) AND (movmag.mm_serie = movmagtc.mm_serie) AND (movmag.mm_numdoc = movmagtc.mm_numdoc) AND (movmag.mm_riga = movmagtc.mm_riga)" & _
                    " WHERE(movmag.mm_quant <> (movmagtc.mm_quant01 + movmagtc.mm_quant02 + movmagtc.mm_quant03 + movmagtc.mm_quant04 + movmagtc.mm_quant05 + movmagtc.mm_quant06 + movmagtc.mm_quant07 + movmagtc.mm_quant08 + movmagtc.mm_quant09 + movmagtc.mm_quant10 + movmagtc.mm_quant11 + movmagtc.mm_quant12 + movmagtc.mm_quant13 + movmagtc.mm_quant14 + movmagtc.mm_quant15 + movmagtc.mm_quant16 + movmagtc.mm_quant17 + movmagtc.mm_quant18 + movmagtc.mm_quant19 + movmagtc.mm_quant20 + movmagtc.mm_quant21 + movmagtc.mm_quant22 + movmagtc.mm_quant23 + movmagtc.mm_quant24))" & _
                    " ORDER BY movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimentazione articolo T&C anomala: ditta |0|, tipo doc. |1|, anno |2|, serie |3|, numero |4|, riga |5|, quantità movmag |6|, differenza rilevata |7|.", _
          "48"})

        'Controllo 49: Movimentazione articoli T&C: righe di MOVMAG su articoli a 'Taglie e Colori' SENZA record di dettaglio MOVMAGTC 
        .Rows.Add(New Object() {"MG", "artdefxtc,artico", "Movimentazione articoli T&C: righe di MOVMAG su articoli a 'Taglie e Colori' SENZA record di dettaglio MOVMAGTC", _
          "1", "1", "SELECT movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga" & _
                    " FROM movmag LEFT JOIN movmagtc ON (movmag.codditt = movmagtc.codditt) AND (movmag.mm_tipork = movmagtc.mm_tipork) AND (movmag.mm_anno = movmagtc.mm_anno) AND (movmag.mm_serie = movmagtc.mm_serie) AND (movmag.mm_numdoc = movmagtc.mm_numdoc) AND (movmag.mm_riga = movmagtc.mm_riga)" & _
                    " INNER JOIN artico on artico.codditt = movmag.codditt and artico.ar_codart = movmag.mm_codart" & _
                    " WHERE(movmagtc.codditt Is null) and artico.ar_codtagl <> 0" & _
                    " ORDER BY movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimentazione articolo T&C anomala: ditta |0|, tipo doc. |1|, anno |2|, serie |3|, numero |4|, riga |5|.", _
          "49"})

        'Controllo 50: Articoli: presenza di articoli con 'Gestione a fasi' senza dati nella tabella relativa 'Fasi articolo'
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Gestione a fasi' senza dati nella tabella relativa 'Fasi articolo'", _
          "1", "1", "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart WHERE ar_gesfasi = 'S' AND af_codart IS NULL", _
          "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart WHERE ar_gesfasi = 'S' AND af_codart IS NULL", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "50"})

        'Controllo 51: Articoli: presenza di articoli con 'Politica di riordino' --> 'A punto di riordino con lotto' ('M') e con 'Scorta minima' e/o 'Quantità lotto standard di prduzione o acquisto' a zero
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino' --> 'A punto di riordino con lotto' e con 'Scorta minima' e/o 'Quantità lotto standard di prduzione o acquisto' a zero", _
          "1", "1", "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_polriord = 'M' AND (ar_scomin <= 0 OR ar_minord <= 0) ORDER BY codditt, ar_codart", _
          "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_polriord = 'M' AND (ar_scomin <= 0 OR ar_minord <= 0) ORDER BY codditt, ar_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "51"})

        'Controllo 52: Articoli: presenza di articoli con 'Politica di riordino' --> 'A punto di riordino a ricostituzione scorta' con 'Scorta minima' <= 0 e/o 'Scorta massima' <= 0 e/o 'Scorta minima' >= 'Scorta massima'
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino' --> 'A punto di riordino a ricostituzione scorta' con 'Scorta minima' <= 0 e/o 'Scorta massima' <= 0 e/o 'Scorta minima' >= 'Scorta massima'", _
          "1", "1", "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_polriord = 'N' AND (ar_scomin <= 0 OR ar_scomax <= 0 OR ar_scomin >= ar_scomax) ORDER BY codditt, ar_codart", _
          "SELECT codditt, ar_codart, ar_descr FROM artico WHERE ar_polriord = 'N' AND (ar_scomin <= 0 OR ar_scomax <= 0 OR ar_scomin >= ar_scomax) ORDER BY codditt, ar_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "52"})

        'Controllo 53: Articoli: presenza di articoli con 'Politica di riordino' --> 'Su fabbisogno con lotto' o 'Su fabbisogno con lotto minimo' con 'Quantità Lotto standard' <= 0
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino' --> 'Su fabbisogno con lotto' o 'Su fabbisogno con lotto minimo' con 'Quantità Lotto standard' <= 0", _
          "1", "1", "SELECT codditt, ar_codart, ar_descr FROM artico WHERE (ar_polriord = 'F' OR ar_polriord = 'O') AND ar_minord <= 0 ORDER BY codditt, ar_codart", _
          "SELECT codditt, ar_codart, ar_descr FROM artico WHERE (ar_polriord = 'F' OR ar_polriord = 'O') AND ar_minord <= 0 ORDER BY codditt, ar_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "53"})

        'Controllo 54: Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'A punto di riordino con lotto' con 'Quantità Lotto standard' <= 0 e/o 'Scorta minima' <= 0
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'A punto di riordino con lotto' con 'Quantità Lotto standard' <= 0 e/o 'Scorta minima' <= 0", _
          "1", "1", "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE am_polriord = 'M' AND (am_scomin <= 0 OR am_minord <= 0) ORDER BY artmaga.codditt, am_codart", _
          "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE am_polriord = 'M' AND (am_scomin <= 0 OR am_minord <= 0) ORDER BY artmaga.codditt, am_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "54"})

        'Controllo 55: Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'A punto di riordino ricostituzione scorta' con 'Scorta minima' <= 0 e/o 'Scorta minima' <= 0 e/o 'Scorta minima' >= 'Scorta massima'
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'A punto di riordino ricostituzione scorta' con 'Scorta minima' <= 0 e/o 'Scorta minima' <= 0 e/o 'Scorta minima' >= 'Scorta massima'", _
          "1", "1", "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE am_polriord = 'N' AND (am_scomin <= 0 OR am_scomax <= 0 OR am_scomin >= am_scomax) ORDER BY artmaga.codditt, am_codart", _
          "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE am_polriord = 'N' AND (am_scomin <= 0 OR am_scomax <= 0 OR am_scomin >= am_scomax) ORDER BY artmaga.codditt, am_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "55"})

        'Controllo 56: Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'Su fabbisogno con lotto' o 'Su fabbisogno con lotto minimo' con 'Quantità lotto standard <= 0'
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli con 'Politica di riordino per magazzino' --> 'Su fabbisogno con lotto' o 'Su fabbisogno con lotto minimo' con 'Quantità lotto standard <= 0'", _
          "1", "1", "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE (am_polriord = 'F' OR am_polriord = 'O') AND am_minord <= 0 ORDER BY artmaga.codditt, am_codart", _
          "SELECT artmaga.codditt, am_codart, ar_descr, am_codmaga FROM artmaga INNER JOIN artico ON artmaga.codditt = artico.codditt AND artmaga.am_codart = artico.ar_codart WHERE (am_polriord = 'F' OR am_polriord = 'O') AND am_minord <= 0 ORDER BY artmaga.codditt, am_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "56"})

        'Controllo 57: Articoli: presenza di articoli gestiti a varianti 'Taglie & Colori' che non posseggono il record relativo in tabella ARTICOTC
        .Rows.Add(New Object() {"MG", "artico", "Articoli: presenza di articoli gestiti a varianti 'Taglie & Colori' che non posseggono il record relativo in tabella ARTICOTC", _
          "1", "1", "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN articotc ON artico.codditt = articotc.codditt AND artico.ar_codart = articotc.art_codart WHERE ar_gesvar = 'S' AND ar_codtagl <> 0 AND ar_anno <> 0 AND ar_codstag <> 0 AND art_codart IS NULL ORDER BY artico.codditt, ar_codart", _
          "SELECT artico.codditt, ar_codart, ar_descr FROM artico LEFT JOIN articotc ON artico.codditt = articotc.codditt AND artico.ar_codart = articotc.art_codart WHERE ar_gesvar = 'S' AND ar_codtagl <> 0 AND ar_anno <> 0 AND ar_codstag <> 0 AND art_codart IS NULL ORDER BY artico.codditt, ar_codart", _
          "1", "0", "1", "0", "0", "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|.", _
          "57"})

        'Controllo 58: Articoli: la somma dei movimenti di magazzino per taglia (MOVMAGTC) è diversa dal corrispondente progressivo per magazzino/taglia (ARTPROTC)
        strSQL = "SELECT movmag.codditt, mm_codart, MIN(mm_descr), km_magaz,"
        For i = 1 To 24
          strSQL += " apt_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & _
            " - SUM(mm_quant" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & _
            " * km_carscar) as 'DIFF_TG" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & "'" & _
            IIf(i < 24, ",", "").ToString
        Next
        strSQL += " FROM movmag INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
          " INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
          " INNER JOIN artprotc ON movmag.codditt = artprotc.codditt AND movmag.mm_codart = artprotc.apt_codart AND keymag.km_magaz = artprotc.apt_magaz" & _
          " GROUP BY movmag.codditt, mm_codart, km_magaz,"
        For i = 1 To 24
          strSQL += " apt_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & IIf(i < 24, ",", "").ToString
        Next
        strSQL += " HAVING ("
        For i = 1 To 24
          strSQL += "SUM(mm_quant" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & _
            " * km_carscar) <> apt_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2) & _
            IIf(i < 24, " OR ", ")").ToString
        Next
        strSQL += " ORDER BY movmag.codditt, mm_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Documenti: la somma dei movimenti di magazzino per taglia (MOVMAGTC) è diversa dal corrispondente progressivo per magazzino/taglia (ARTPROTC) (da utilizzare solo prima della prima chiusura di magazzino oppure se non sono stati cancellati/modificati i documenti di magazzino compresi nelle chiusure di magazzino e non sono stati modificati i progressivi definitivi)", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, magazzino |3|.", "58"})

        'Controllo 59: Documenti: Articoli T&C: progressivi correnti articoli/magazzino (ARTPROTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN artprotc ON artico.codditt = artprotc.codditt" & _
                            " AND artico.ar_codart = artprotc.apt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_prenot" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_ordin" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_impeg" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi correnti articoli/magazzino (ARTPROTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "59"})

        'Controllo 60: Documenti: Articoli T&C: movimenti di magazzino e note di prelievo (MOVMAGTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT movmag.codditt, movmag.mm_anno, movmag.mm_tipork, movmag.mm_serie, movmag.mm_numdoc," & _
          " movmag.mm_riga, movmag.mm_codart, tabtagl.tb_codtagl" & _
          " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt" & _
                                         " AND movmag.mm_tipork = movmagtc.mm_tipork" & _
                                         " AND movmag.mm_anno = movmagtc.mm_anno" & _
                                         " AND movmag.mm_serie = movmagtc.mm_serie" & _
                                         " AND movmag.mm_numdoc = movmagtc.mm_numdoc" & _
                                         " AND movmag.mm_riga = movmagtc.mm_riga" & _
           " INNER JOIN artico ON movmag.codditt = artico.codditt" & _
                           " AND movmag.mm_codart = artico.ar_codart" & _
           " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                             " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
           " WHERE ar_codtagl <> 0" & _
           " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND mm_quant" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY movmag.mm_anno, movmag.mm_tipork, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: movimenti di magazzino e note di prelievo (MOVMAGTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, anno |1|, tipo |2|, serie |3|, numero |4|, riga |5|, articolo |6|, taglia |7|.", "60"})

        'Controllo 61: Documenti: Articoli T&C: progressivi correnti articoli (ARTPROXTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN artproxtc ON artico.codditt = artproxtc.codditt" & _
                            " AND artico.ar_codart = artproxtc.apt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_prenot" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_ordin" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_impeg" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi correnti articoli (ARTPROXTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "61"})

        'Controllo 62: Documenti: Articoli T&C: progressivi correnti lotti/commesse/ubicazioni (LOTCPROTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN lotcprotc ON artico.codditt = lotcprotc.codditt" & _
                            " AND artico.ar_codart = lotcprotc.apt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_prenot" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_ordin" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_impeg" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi correnti lotti/commesse/ubicazioni (LOTCPROTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "62"})

        'Controllo 63: Documenti: Articoli T&C: progressivi definitivi articoli (ARTDEFXTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN artdefxtc ON artico.codditt = artdefxtc.codditt" & _
                            " AND artico.ar_codart = artdefxtc.adt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_lesist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi definitivi articoli (ARTDEFXTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "63"})

        'Controllo 64: Documenti: Articoli T&C: progressivi definitivi articoli/magazzino (ARTDEFTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN artdeftc ON artico.codditt = artdeftc.codditt" & _
                            " AND artico.ar_codart = artdeftc.adt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi definitivi articoli/magazzino (ARTDEFTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "64"})

        'Controllo 65: Documenti: Articoli T&C: progressivi definitivi lotti/commesse/ubicazioni (LOTCDEFTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT artico.codditt, ar_codart, ar_descr, tb_codtagl, tb_destagl" & _
          " FROM artico INNER JOIN lotcdeftc ON artico.codditt = lotcdeftc.codditt" & _
                            " AND artico.ar_codart = lotcdeftc.adt_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: progressivi definitivi lotti/commesse/ubicazioni (LOTCDEFTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|.", "65"})

        'Controllo 66: Articoli T&C: righe d'ordine (MOVORDTC) su taglie non presenti in tabella scala taglie
        strSQL = "SELECT movord.codditt, mo_codart, mo_descr, tb_codtagl, tb_destagl," & _
          " movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga" & _
          " FROM movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt" & _
                                         " AND movord.mo_tipork = movordtc.mo_tipork" & _
                                         " AND movord.mo_anno = movordtc.mo_anno" & _
                                         " AND movord.mo_serie = movordtc.mo_serie" & _
                                         " AND movord.mo_numord = movordtc.mo_numord" & _
                                         " AND movord.mo_riga = movordtc.mo_riga" & _
          " INNER JOIN artico ON movord.codditt = artico.codditt" & _
                           " AND movord.mo_codart = artico.ar_codart" & _
          " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                            " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
          " WHERE ar_codtagl <> 0" & _
          " AND	("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND mo_quant" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND mo_quaeva" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " OR ("
        For i = 1 To 24
          strSQL += IIf(i > 1, " OR ", "").ToString & _
            "(tb_dest" & Right("0" & i, 2) & " = ' ' AND mo_quapre" & Right("0" & i, 2) & " <> 0)" & _
            IIf(i = 24, ")", "").ToString
        Next
        strSQL += " ORDER BY ar_codart"
        .Rows.Add(New Object() {"MG", "artico", _
          "Articoli T&C: righe d'ordine (MOVORDTC) su taglie non presenti in tabella scala taglie", _
          "1", "1", strSQL, strSQL, "1", "0", "1", "0", "0", _
          "Articolo anomalo: ditta |0|, articolo |1|, descrizione |2|, taglia |3|, descr. taglia |4|, tipo |5|, anno |6|, serie |7|, numero |8|, riga |9|.", "66"})

        '------------------------------------------------------------------------------------
        'DISTINTA BASE
        '------------------------------------------------------------------------------------
        'Controllo 100: Distinta base: presenza di più di una fase di lavorazione terzista per gli articoli non gestiti a fase
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: presenza di più di una fase di lavorazione terzista per gli articoli non gestiti a fase", _
          "1", "1", "SELECT codditt, dd_coddb, Count(dd_coddb) FROM cicli WHERE dd_terzista = 'S' GROUP BY codditt, dd_coddb HAVING Count(dd_coddb) > 1 ORDER BY codditt, dd_coddb", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|: sono presenti |2| fasi di lavorazione terziste. E' consentita al massimo una fase.", _
          "100"})

        'Controllo 101: Distinta base: presenza di lavorazioni interne senza indicazione del tempo di esecuzione
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: presenza di lavorazioni interne senza indicazione del tempo di esecuzione", _
          "1", "2", "SELECT DISTINCT codditt, dd_coddb, dd_riga FROM cicli WHERE (dd_terzista = 'N' OR dd_terzista = 'I') AND dd_tempese = 0 ORDER BY codditt, dd_coddb", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|, riga lavorazione |2|: su questa lavorazione interna non è indicato alcun tempo di esecuzione.", _
          "101"})

        'Controllo 102: Distinta base: centro di lavoro esterno su fasi interne o viceversa
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: centro di lavoro esterno su fasi interne o viceversa", _
          "1", "1", "SELECT DISTINCT cicli.codditt, dd_coddb, dd_riga, dd_codcent" & _
                              " FROM (cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent)" & _
                              " INNER JOIN tabmaga ON tabcent.codditt = tabmaga.codditt AND tabcent.tb_magterz = tabmaga.tb_codmaga" & _
                              " WHERE  ((dd_terzista = 'S' OR dd_terzista = 'E') AND (tb_flclavo = ' ' OR tb_flclavo = 'C'))" & _
                              " OR ((dd_terzista = 'N' OR dd_terzista = 'I') AND (tb_flclavo = 'F' OR tb_flclavo = 'X'))" & _
                              " ORDER BY cicli.codditt, dd_coddb, dd_riga", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|, riga lavorazione |2|: centro di lavoro |3| esterno su fasi interne o viceversa.", _
          "102"})

        'Controllo 103: Distinta base: presenza di fasi svolte da centri esterni diversi dal fornitore abituale dell'articolo
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: presenza di fasi svolte da centri esterni diversi dal fornitore abituale dell'articolo", _
          "1", "2", "SELECT DISTINCT cicli.codditt, dd_coddb, dd_riga, dd_codcent FROM (artico" & _
                              " INNER JOIN (cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent)" & _
                              " ON artico.codditt = cicli.codditt AND artico.ar_coddb = cicli.dd_coddb) INNER JOIN tabmaga" & _
                              " ON tabcent.codditt = tabmaga.codditt AND tabcent.tb_magterz = tabmaga.tb_codmaga" & _
                              " WHERE ar_forn <> tb_magconto AND ar_forn <> 0 AND dd_terzista = 'S' " & _
                              " ORDER BY cicli.codditt, dd_coddb, dd_riga", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|, riga lavorazione |2|: centro di lavoro |3| associato ad un fornitore diverso dal fornitore abituale dell'articolo.", _
          "103"})

        'Controllo 104: Distinta base: distinte con fasi interne dove il fornitore abituale è associati a centri esterni
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: distinte con fasi interne dove il fornitore abituale è associati a centri esterni", _
          "1", "2", "SELECT cicli.codditt, dd_coddb, dd_riga, ar_forn FROM artico" & _
                              " INNER JOIN cicli ON artico.ar_coddb = cicli.dd_coddb AND artico.codditt = cicli.codditt" & _
                              " WHERE ar_forn IN (SELECT tb_magconto FROM tabmaga" & _
                                                 " WHERE tabmaga.codditt = cicli.codditt AND (tb_flclavo = 'F' OR tb_flclavo = 'X'))" & _
                              " AND (dd_terzista = 'N') AND ar_forn <> 0" & _
                              " ORDER BY cicli.codditt, dd_coddb", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|, riga lavorazione |2|: fornitore abituale |3| associato ad un centro esterno mentre la fase di lavorazione è interna.", _
          "104"})

        'Controllo 105: Distinta base: lavorazioni interne associate a centri di lavoro senza costo orario macchina/manodopera
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: lavorazioni interne associate a centri di lavoro senza costo orario macchina/manodopera", _
          "1", "2", "SELECT DISTINCT cicli.codditt, dd_coddb, dd_riga, dd_codcent FROM cicli INNER JOIN tabcent" & _
                              " ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent" & _
                              " WHERE (dd_terzista = 'N' OR dd_terzista = 'I') " & _
                              " AND (tb_pagaora + tb_cmacora) = 0" & _
                              " ORDER BY cicli.codditt, dd_coddb", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, codice distinta base |1|, riga lavorazione |2|: sul centro di lavoro |3| non sono indicati costo orario manodopera e macchina.", _
          "105"})

        'Controllo 106: Distinta base: presenza di articoli a fase con Distinta Base con fasi non presenti nei cicli di lavorazione della Distinta Base
        .Rows.Add(New Object() {"DB", "cicli", "Distinta base: presenza di articoli a fase con Distinta Base con fasi non presenti nei cicli di lavorazione della Distinta Base", _
          "1", "2", "SELECT artico.codditt, ar_codart, ar_descr, af_fase" & _
                    " FROM (artico INNER JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart)" & _
                    " LEFT JOIN cicli ON artfasi.codditt = cicli.codditt AND artfasi.af_codart = cicli.dd_coddb AND artfasi.af_fase = cicli.dd_fase" & _
                    " WHERE ar_gesfasi = 'S'" & _
                    " AND ar_coddb IS NOT NULL" & _
                    " AND dd_coddb IS NULL" & _
                    " AND ar_coddb = ar_codart" & _
                    " ORDER BY artico.codditt, ar_codart, ar_descr, af_fase", _
                    "SELECT artico.codditt, ar_codart, ar_descr, af_fase" & _
                    " FROM (artfasi INNER JOIN artico ON artfasi.codditt = artico.codditt AND artfasi.af_codart = artico.ar_codart)" & _
                    " LEFT JOIN cicli ON artico.codditt = cicli.codditt AND artico.ar_coddb = cicli.dd_coddb AND artfasi.af_fase = cicli.dd_fase" & _
                    " WHERE ar_gesfasi = 'S'" & _
                    " AND ar_coddb IS NOT NULL" & _
                    " AND dd_coddb IS NULL" & _
                    " ORDER BY artico.codditt, ar_codart, ar_descr, af_fase", _
          "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|, descrizione |2|: la fase |3| non è indicata in Distinta Base.", _
          "106"})

        'Controllo 107: Distinta base: presenza di Distinte Basi senza Materiali
        .Rows.Add(New Object() {"DB", "distbas", "Distinta base: presenza di Distinte Basi senza Materiali", _
          "1", "2", "SELECT distbas.codditt, db_coddb, ar_descr" & _
                    " FROM (distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_codart)" & _
                    " LEFT JOIN movdis ON distbas.codditt = movdis.codditt AND distbas.db_coddb = movdis.md_coddb" & _
                    " WHERE md_codfigli IS NULL" & _
                    " ORDER BY distbas.codditt, db_coddb", _
                    "SELECT distbas.codditt, db_coddb, ar_descr" & _
                    " FROM (distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_codart)" & _
                    " LEFT JOIN movdis ON distbas.codditt = movdis.codditt AND distbas.db_coddb = movdis.md_coddb" & _
                    " WHERE md_codfigli IS NULL" & _
                    " ORDER BY distbas.codditt, db_coddb", _
          "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|, descrizione |2|: non possiede Materiali.", _
          "107"})
        'Controllo 108: Distinta base: presenza di materiali senza indicazione degli abbinamenti taglia
        .Rows.Add(New Object() {"DB, TCP", "movdis", "Distinta base: presenza di materiali senza indicazione degli abbinamenti taglia", _
           "1", "2", "SELECT movdis.codditt, md_coddb, md_codfigli, md_riga" & _
                     " FROM movdis LEFT JOIN movdistt ON movdis.codditt = movdistt.codditt AND movdis.md_coddb = movdistt.mdt_coddb AND movdis.md_riga = movdistt.mdt_riga" & _
                     " WHERE md_tctipo IN ('C','G') and mdt_rigaa is null", _
           "", "1", "0", "1", "0", "0", "Ditta |0|, Cod. Padre |1|, Cod. Figlio |2|, Riga |3|: mancano abbinamenti taglia.", _
           "108"})
        'Controllo 109: Distinta base: presenza di materiali senza indicazione degli abbinamenti colore
        .Rows.Add(New Object() {"DB, TCP", "movdis", "Distinta base: presenza di materiali senza indicazione degli abbinamenti colore", _
           "1", "2", "SELECT movdis.codditt, md_coddb, md_codfigli, md_riga" & _
                     " FROM movdis LEFT OUTER JOIN movdistc ON movdis.codditt = movdistc.codditt AND movdis.md_coddb = movdistc.mdc_coddb AND movdis.md_riga = movdistc.mdc_riga" & _
                     " WHERE md_tctipo IN ('D','E','G') and mdc_rigaa is null", _
           "", "1", "0", "1", "0", "0", "Ditta |0|, Cod. Padre |1|, Cod. Figlio |2|, Riga |3|: mancano abbinamenti colore.", _
           "109"})


        'Controllo 700 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico con riferimenti in/out non uguali
        .Rows.Add(New Object() {"DB, MG", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico con riferimenti in/out non uguali", _
          "1", "1", "SELECT codditt, mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga, mc_outipo, mc_ouanno, mc_ouserie, mc_ounum, mc_ouriga FROM movcla WHERE mc_carscar = 1 AND (movcla.mc_intipo <> movcla.mc_outipo OR movcla.mc_inanno <> movcla.mc_ouanno OR movcla.mc_inserie <> movcla.mc_ouserie OR movcla.mc_innum <> movcla.mc_ounum OR movcla.mc_inriga <> movcla.mc_ouriga) ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, righe anomale: ditta |0|, tipo IN |1|, anno IN |2|, serie IN |3|, numero IN |4|, riga IN |5|, tipo OUT |6|, anno OUT |7|, serie OUT |8|, numero OUT |9|, riga OUT |10|.", _
          "700"})

        'Controllo 701 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico con flag di scarico (mc_carscar = -1)
        .Rows.Add(New Object() {"DB, MG", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico con flag di scarico (mc_carscar = -1)", _
          "1", "1", "SELECT codditt, mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga FROM movcla WHERE mc_carscar = -1 AND movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, righe anomale: ditta |0|, tipo IN |1|, anno IN |2|, serie IN |3|, numero IN |4|, riga IN |5|.", _
          "701"})

        'Controllo 702 C/LAV. ATTIVO: presenza in MOVCLA di carichi di cui non esiste documento di magazzino
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di carichi di cui non esiste documento di magazzino", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga FROM movcla LEFT JOIN movmag ON (movcla.mc_inriga = movmag.mm_riga) AND (movcla.mc_innum = movmag.mm_numdoc) AND (movcla.mc_inserie = movmag.mm_serie) AND (movcla.mc_inanno = movmag.mm_anno) AND (movcla.mc_intipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND movmag.mm_tipork IS NULL ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, documento di carico inesistente: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|.", _
          "702"})

        'Controllo 703 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui causale di magazzino non è di 'Carico materiali in c/lavoro attivo'
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui causale di magazzino non è di 'Carico materiali in c/lavoro attivo'", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, keymag.km_causale FROM tabcaum INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_intipo = keymag.km_tipork) AND (movcla.mc_inanno = keymag.km_anno) AND (movcla.mc_inserie = keymag.km_serie) AND (movcla.mc_innum = keymag.km_numdoc) AND (movcla.mc_inriga = keymag.km_riga)) ON tabcaum.tb_codcaum = keymag.km_causale WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND (((tabcaum.tb_tipcaum)<>'K')) ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con causale errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, causale |6|.", _
          "703"})

        'Controllo 704 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui causale di magazzino non è di carico (Esistenza +)
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui causale di magazzino non è di carico (Esistenza +)", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, keymag.km_causale FROM tabcaum INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_intipo = keymag.km_tipork) AND (movcla.mc_inanno = keymag.km_anno) AND (movcla.mc_inserie = keymag.km_serie) AND (movcla.mc_innum = keymag.km_numdoc) AND (movcla.mc_inriga = keymag.km_riga)) ON tabcaum.tb_codcaum = keymag.km_causale WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND (((tabcaum.tb_esist) <> 1)) ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con causale errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, causale |6|.", _
          "704"})

        'Controllo 705 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico il cui tipo di magazzino non è 'Merce Altrui'
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico il cui tipo di magazzino non è 'Merce Altrui'", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, keymag.km_magaz FROM tabmaga INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_intipo = keymag.km_tipork) AND (movcla.mc_inanno = keymag.km_anno) AND (movcla.mc_inserie = keymag.km_serie) AND (movcla.mc_innum = keymag.km_numdoc) AND (movcla.mc_inriga = keymag.km_riga)) ON (tabmaga.codditt = keymag.codditt) AND (tabmaga.tb_codmaga = keymag.km_magaz) WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND (((tabmaga.tb_flclavo)<>'C' And (tabmaga.tb_flclavo)<>'X')) ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con magazzino errato: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, magazzino |6|.", _
          "705"})

        'Controllo 706 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico il cui cod. articolo non corrisponde al movimento di magazzino
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico il cui cod. articolo non corrisponde al movimento di magazzino", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, movcla.mc_incodart, movcla.mc_infase, movmag.mm_codart, movmag.mm_fase FROM movcla INNER JOIN movmag ON (movcla.mc_inriga = movmag.mm_riga) AND (movcla.mc_innum = movmag.mm_numdoc) AND (movcla.mc_inserie = movmag.mm_serie) AND (movcla.mc_inanno = movmag.mm_anno) AND (movcla.mc_intipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND (((movcla.mc_incodart)<>movmag.mm_codart) ) ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, cod. articolo errato: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo MOVCLA |6|, fase MOVCLA |7|, articolo MOVMAG |8|, fase MOVMAG |9|.", _
          "706"})

        'Controllo 707 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui quantità non corrisponde al movimento di magazzino
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui quantità non corrisponde al movimento di magazzino", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, movcla.mc_inquant, movmag.mm_quant FROM movcla INNER JOIN movmag ON (movcla.mc_inriga = movmag.mm_riga) AND (movcla.mc_innum = movmag.mm_numdoc) AND (movcla.mc_inserie = movmag.mm_serie) AND (movcla.mc_inanno = movmag.mm_anno) AND (movcla.mc_intipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND Abs(movcla.mc_inquant - movmag.mm_quant) > 0.001 ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, quantità errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, quantità MOVCLA |6|, quantità MOVMAG |7|.", _
          "707"})

        'Controllo 708 C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui quantità nel movimento di magazzino è negativa
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di carico la cui quantità nel movimento di magazzino è negativa", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga, movmag.mm_quant FROM movcla INNER JOIN movmag ON (movcla.mc_inriga = movmag.mm_riga) AND (movcla.mc_innum = movmag.mm_numdoc) AND (movcla.mc_inserie = movmag.mm_serie) AND (movcla.mc_inanno = movmag.mm_anno) AND (movcla.mc_intipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga AND movmag.mm_quant <= 0.001 ORDER BY movcla.codditt, movcla.mc_intipo, movcla.mc_inanno, movcla.mc_inserie, movcla.mc_innum, movcla.mc_inriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, quantità errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, quantità MOVMAG |6|.", _
          "708"})

        'Controllo 709 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico con riferimenti in/out uguali
        .Rows.Add(New Object() {"DB, MG", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso con riferimenti in/out uguali", _
          "1", "1", "SELECT codditt, mc_outipo, mc_ouanno, mc_ouserie, mc_ounum, mc_ouriga, mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga FROM movcla WHERE mc_carscar = -1 AND (movcla.mc_intipo = movcla.mc_outipo AND movcla.mc_inanno = movcla.mc_ouanno AND movcla.mc_inserie = movcla.mc_ouserie AND movcla.mc_innum = movcla.mc_ounum AND movcla.mc_inriga = movcla.mc_ouriga) ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, righe anomale: ditta |0|, tipo OUT |1|, anno OUT |2|, serie OUT |3|, numero OUT |4|, riga OUT |5|, tipo IN |6|, anno IN |7|, serie IN |8|, numero IN |9|, riga IN |10|.", _
          "709"})

        'Controllo 710 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico relative a documenti di carico inesistenti
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso relative a documenti di carico inesistenti", _
          "1", "1", "SELECT scarico.codditt, scarico.mc_outipo, scarico.mc_ouanno, scarico.mc_ouserie, scarico.mc_ounum, scarico.mc_ouriga, scarico.mc_intipo, scarico.mc_inanno, scarico.mc_inserie, scarico.mc_innum, scarico.mc_inriga FROM movcla AS scarico LEFT JOIN movcla AS carico ON (scarico.mc_inriga = carico.mc_inriga) AND (scarico.mc_innum = carico.mc_innum) AND (scarico.mc_inserie = carico.mc_inserie) AND (scarico.mc_inanno = carico.mc_inanno) AND (scarico.mc_intipo = carico.mc_intipo) AND (scarico.codditt = carico.codditt) WHERE (((scarico.mc_carscar)=-1) AND ((carico.mc_intipo) Is Null)) ORDER BY scarico.codditt, scarico.mc_outipo, scarico.mc_ouanno, scarico.mc_ouserie, scarico.mc_ounum, scarico.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di scarico senza carico: ditta OUT |0|, tipo OUT |1|, anno OUT |2|, serie OUT |3|, numero OUT |4|, riga OUT |5|, tipo IN |6|, anno IN |7|, serie IN |8|, numero IN |9|, riga IN |10|.", _
          "710"})

        'Controllo 711 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico di cui non esiste documento di magazzino
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso di cui non esiste documento di magazzino", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga FROM movcla LEFT JOIN movmag ON (movcla.mc_ouriga = movmag.mm_riga) AND (movcla.mc_ounum = movmag.mm_numdoc) AND (movcla.mc_ouserie = movmag.mm_serie) AND (movcla.mc_ouanno = movmag.mm_anno) AND (movcla.mc_outipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_carscar = -1 AND movmag.mm_tipork IS NULL ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, documento di scarico inesistente: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|.", _
          "711"})

        'Controllo 712 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico la cui causale di magazzino non è di 'Reso Lavorato con trasform. c/lav.(att)' o 'Reso Lav. senza trasform. c/Lav.(att)'
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso la cui causale di magazzino non è di 'Reso Lavorato con trasform. c/lav.(att)' o 'Reso Lav. senza trasform. c/Lav.(att)'", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga, keymag.km_causale FROM tabcaum INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_outipo = keymag.km_tipork) AND (movcla.mc_ouanno = keymag.km_anno) AND (movcla.mc_ouserie = keymag.km_serie) AND (movcla.mc_ounum = keymag.km_numdoc) AND (movcla.mc_ouriga = keymag.km_riga)) ON tabcaum.tb_codcaum = keymag.km_causale WHERE movcla.mc_carscar = -1 AND keymag.km_carscar = -1 AND tabcaum.tb_tipcaum <> 'E' AND tabcaum.tb_tipcaum <> 'F' ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con causale errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, causale |6|.", _
          "712"})

        'Controllo 713 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico la cui causale di magazzino non è di scarico (Esistenza -)
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso la cui causale di magazzino non è di scarico (Esistenza -)", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga, keymag.km_causale FROM tabcaum INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_outipo = keymag.km_tipork) AND (movcla.mc_ouanno = keymag.km_anno) AND (movcla.mc_ouserie = keymag.km_serie) AND (movcla.mc_ounum = keymag.km_numdoc) AND (movcla.mc_ouriga = keymag.km_riga)) ON tabcaum.tb_codcaum = keymag.km_causale WHERE movcla.mc_carscar = -1 AND keymag.km_carscar = -1 AND ((tabcaum.tb_esist) <> -1) ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con causale errata: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, causale |6|.", _
          "713"})

        'Controllo 714 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico il cui tipo di magazzino non è 'Merce Altrui'
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso il cui tipo di magazzino non è 'Merce Altrui'", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga, keymag.km_magaz FROM tabmaga INNER JOIN (movcla INNER JOIN keymag ON (movcla.codditt = keymag.codditt) AND (movcla.mc_outipo = keymag.km_tipork) AND (movcla.mc_ouanno = keymag.km_anno) AND (movcla.mc_ouserie = keymag.km_serie) AND (movcla.mc_ounum = keymag.km_numdoc) AND (movcla.mc_ouriga = keymag.km_riga)) ON (tabmaga.codditt = keymag.codditt) AND (tabmaga.tb_codmaga = keymag.km_magaz) WHERE movcla.mc_carscar = -1 AND keymag.km_carscar = -1 AND ((tabmaga.tb_flclavo)<>'C' And (tabmaga.tb_flclavo)<>'X') ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, riga di documento con magazzino errato: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, magazzino |6|.", _
          "714"})

        'Controllo 715 C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico il cui cod. articolo non corrisponde al movimento di magazzino (non tiene conto della FASE)
        .Rows.Add(New Object() {"DB", "movcla", "C/LAV. ATTIVO: presenza in MOVCLA di righe di scarico/reso il cui cod. articolo non corrisponde al movimento di magazzino", _
          "1", "1", "SELECT movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga, movcla.mc_oucodart, movcla.mc_oufase, movmag.mm_codart, movmag.mm_fase FROM movcla INNER JOIN movmag ON (movcla.mc_ouriga = movmag.mm_riga) AND (movcla.mc_ounum = movmag.mm_numdoc) AND (movcla.mc_ouserie = movmag.mm_serie) AND (movcla.mc_ouanno = movmag.mm_anno) AND (movcla.mc_outipo = movmag.mm_tipork) AND (movcla.codditt = movmag.codditt) WHERE movcla.mc_carscar = -1 AND (((movcla.mc_oucodart)<>movmag.mm_codart)) ORDER BY movcla.codditt, movcla.mc_outipo, movcla.mc_ouanno, movcla.mc_ouserie, movcla.mc_ounum, movcla.mc_ouriga", _
          "", "1", "0", "1", "0", "0", "MOVCLA, cod. articolo errato: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo MOVCLA |6|, fase MOVCLA |7|, articolo MOVMAG |8|, fase MOVMAG |9|.", _
          "715"})

        'controllo delle quantità sui movimenti di scarico
        ' da fare

        'controllo che le quantità non siano negative
        ' da fare

        'controllo che i saldi (delle righe referenziate non siano negative) , un po complesso.
        'da fare

        '--- Controllo 721 C/LAV. ATTIVO: presenza di movimenti di carico MP con riferimenti (campi mm_cl*) non coincidenti
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO: presenza di movimenti di carico MP con riferimenti (campi mm_cl*) non coincidenti", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, movmag.mm_cltipo, movmag.mm_clanno, movmag.mm_clserie, movmag.mm_clnum, movmag.mm_clriga " & _
                              "FROM (movmag INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum  " & _
                              "WHERE (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'K' AND ((movmag.mm_clriga <> movmag.mm_riga OR movmag.mm_clnum <> movmag.mm_numdoc OR movmag.mm_clserie <>movmag.mm_serie OR movmag.mm_clanno <> movmag.mm_anno OR movmag.mm_cltipo <> movmag.mm_tipork)) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, tipo CARICO |6|, anno CARICO |7|, serie CARICO |8|, numero CARICO |9|, riga CARICO |10|.", _
          "721"})

        'Controllo 722 C/LAV. ATTIVO: presenza di movimenti di carico MP con riferimenti (campi mm_cl*) compilati ma il tipo di magazzino/causale non lo prevedono
        'non fatto

        'Controllo 723 C/LAV. ATTIVO: presenza di movimenti di scarico MP con riferimenti ai carichi (campi mm_cl*) dove il tipo di magazzino/causale non lo prevede
        'non fatto

        'Controllo 724 C/LAV. ATTIVO: presenza di movimenti di scarico MP SENZA riferimenti ai carichi (campi mm_cl*) dove il tipo di magazzino/causale lo prevede
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO: presenza nei movimenti di scarico MP SENZA riferimenti ai carichi (campi mm_cl*) dove il tipo di magazzino/causale lo prevede", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga,  movmag.mm_codart, movmag.mm_quant, movmag.mm_magaz, movmag.mm_causale " & _
                              "FROM (((movmag LEFT JOIN movmag AS Carprod ON (movmag.mm_clriga = Carprod.mm_riga) AND (movmag.mm_clnum = Carprod.mm_numdoc) AND (movmag.mm_clserie = Carprod.mm_serie) AND (movmag.mm_clanno = Carprod.mm_anno) AND (movmag.mm_cltipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt)) INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum ) " & _
                              "WHERE (Carprod.mm_tipork IS NULL) AND (movmag.mm_codart <> 'D' AND movmag.mm_codart <> 'M' AND tabmaga.tb_flclavo = 'C' AND (tabcaum.tb_tipcaum = 'S' OR  tabcaum.tb_tipcaum = 'F')) AND (movmag.mm_clriga <> movmag.mm_riga OR movmag.mm_clnum <> movmag.mm_numdoc OR movmag.mm_clserie <>movmag.mm_serie OR movmag.mm_clanno <> movmag.mm_anno OR movmag.mm_cltipo <> movmag.mm_tipork) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo |6|, quantità |7|, magazzino |8|, causale |9|.", _
          "724"})

        'Controllo 725 C/LAV. ATTIVO: presenza nei movimenti di scarico MP che si riferiscono a carichi (campi mm_cl*) dove il codice articolo non coincide
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO: presenza nei movimenti di scarico MP che si riferiscono a carichi (campi mm_cl*) dove il codice articolo non coincide", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, movmag.mm_codart, Carprod.mm_codart " & _
                              "FROM movmag INNER JOIN movmag AS Carprod ON (movmag.mm_clriga = Carprod.mm_riga) AND (movmag.mm_clnum = Carprod.mm_numdoc) AND (movmag.mm_clserie = Carprod.mm_serie) AND (movmag.mm_clanno = Carprod.mm_anno) AND (movmag.mm_cltipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt) " & _
                              "WHERE movmag.mm_codart <> Carprod.mm_codart " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo SCARICO |1|, anno SCARICO |2|, serie SCARICO |3|, numero SCARICO |4|, riga SCARICO |5|, tipo CARICO |6|, anno CARICO |7|, serie CARICO |8|, numero CARICO |9|, riga CARICO |10|, articolo SCARICO |11|, articolo CARICO |12|.", _
          "725"})

        'Controllo 726 C/LAV. ATTIVO: presenza di carichi MP da cliente utilizzati in documento di scarico dove la quantità scaricata supera la quantità caricata dal cliente
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO: presenza di carichi MP da cliente utilizzati in documento di scarico dove la quantità scaricata supera la quantità caricata dal cliente", _
          "1", "1", "SELECT Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, Min(Carprod.mm_codart) AS Codart, Min(Carprod.mm_quant) AS QuantCarico, Sum(movmag.mm_quant) AS QuantReso, Min(Carprod.mm_magaz) AS Magaz " & _
                              "FROM movmag INNER JOIN movmag AS Carprod ON (movmag.mm_clriga = Carprod.mm_riga) AND (movmag.mm_clnum = Carprod.mm_numdoc) AND (movmag.mm_clserie = Carprod.mm_serie) AND (movmag.mm_clanno = Carprod.mm_anno) AND (movmag.mm_cltipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt) " & _
                              "WHERE (movmag.mm_clriga <> movmag.mm_riga Or movmag.mm_clnum <> movmag.mm_numdoc Or movmag.mm_clserie <> movmag.mm_serie Or movmag.mm_clanno <> movmag.mm_anno Or movmag.mm_cltipo <> movmag.mm_tipork) " & _
                              "GROUP BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga " & _
                              "HAVING (Sum(movmag.mm_quant) - Min(Carprod.mm_quant)) > 0.001 " & _
                              "ORDER BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga", _
          "", "1", "0", "1", "0", "0", "Carico: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo |6|, quantità CARICO |7|, quantità SCARICO |8|, magazzino |9|.", _
          "726"})

        'Controllo 731 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di prod. con riferimenti (campi mm_cc*) non coincidenti
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di prod. con riferimenti (campi mm_cc*) non coincidenti", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, movmag.mm_cctipo, movmag.mm_ccanno, movmag.mm_ccserie, movmag.mm_ccnum, movmag.mm_ccriga " & _
                              "FROM (movmag INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum " & _
                              "WHERE mm_fase = 0 AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'P' AND ((movmag.mm_ccriga <> movmag.mm_riga OR movmag.mm_ccnum <> movmag.mm_numdoc OR movmag.mm_ccserie <>movmag.mm_serie OR movmag.mm_ccanno <> movmag.mm_anno OR movmag.mm_cctipo <> movmag.mm_tipork)) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, tipo CAR.PROD |6|, anno CAR.PROD |7|, serie CAR.PROD |8|, numero CAR.PROD |9|, riga CAR.PROD |10|.", _
          "731"})

        'Controllo 732 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di prod. con riferimenti (campi mm_cc*) compilati ma il tipo di magazzino/causale non lo prevedono
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di prod. con riferimenti (campi mm_cc*) compilati ma il tipo di magazzino/causale non lo prevedono", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, movmag.mm_magaz, movmag.mm_causale " & _
                              "FROM (movmag INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum " & _
                              "WHERE NOT( mm_fase = 0 AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'P') AND ((movmag.mm_ccriga = movmag.mm_riga AND movmag.mm_ccnum = movmag.mm_numdoc AND movmag.mm_ccserie = movmag.mm_serie AND movmag.mm_ccanno = movmag.mm_anno AND movmag.mm_cctipo = movmag.mm_tipork)) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, magazzino |6|, causale |7|.", _
          "732"})

        'Controllo 733 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente che si riferiscono a carichi di produzione (campi mm_cc*) dove il tipo di magazzino/causale non lo prevede
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente che si riferiscono a carichi di produzione (campi mm_cc*) dove il tipo di magazzino/causale non lo prevede", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, movmag.mm_magaz, movmag.mm_causale " & _
                              "FROM (((movmag INNER JOIN movmag AS Carprod ON (movmag.mm_ccriga = Carprod.mm_riga) AND (movmag.mm_ccnum = Carprod.mm_numdoc) AND (movmag.mm_ccserie = Carprod.mm_serie) AND (movmag.mm_ccanno = Carprod.mm_anno) AND (movmag.mm_cctipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt)) INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum ) " & _
                              "WHERE NOT( movmag.mm_fase = 0 AND tabmaga.tb_flclavo = 'C' AND tabcaum.tb_tipcaum = 'E') AND (movmag.mm_ccriga <> movmag.mm_riga OR movmag.mm_ccnum <> movmag.mm_numdoc OR movmag.mm_ccserie <>movmag.mm_serie OR movmag.mm_ccanno <> movmag.mm_anno OR movmag.mm_cctipo <> movmag.mm_tipork) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo RESO |1|, anno RESO |2|, serie RESO |3|, numero RESO |4|, riga RESO |5|, tipo CAR.PROD |6|, anno CAR.PROD |7|, serie CAR.PROD |8|, numero CAR.PROD |9|, riga CAR.PROD |10|, magazzino RESO |11|, causale RESO |12|.", _
          "733"})

        'Controllo 734 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente SENZA riferimenti validi a carichi di produzione (campi mm_cc*) anche se il tipo di magazzino/causale lo prevede
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente SENZA riferimenti validi a carichi di produzione (campi mm_cc*) anche se il tipo di magazzino/causale lo prevede", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga,  movmag.mm_codart, movmag.mm_magaz, movmag.mm_causale " & _
                              "FROM (((movmag LEFT JOIN movmag AS Carprod ON (movmag.mm_ccriga = Carprod.mm_riga) AND (movmag.mm_ccnum = Carprod.mm_numdoc) AND (movmag.mm_ccserie = Carprod.mm_serie) AND (movmag.mm_ccanno = Carprod.mm_anno) AND (movmag.mm_cctipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt)) INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON movmag.mm_causale = tabcaum.tb_codcaum ) " & _
                              "WHERE (Carprod.mm_tipork IS NULL) AND ( movmag.mm_fase = 0 AND movmag.mm_codart <> 'D' AND movmag.mm_codart <> 'M' AND tabmaga.tb_flclavo = 'C' AND tabcaum.tb_tipcaum = 'E') AND (movmag.mm_ccriga <> movmag.mm_riga OR movmag.mm_ccnum <> movmag.mm_numdoc OR movmag.mm_ccserie <>movmag.mm_serie OR movmag.mm_ccanno <> movmag.mm_anno OR movmag.mm_cctipo <> movmag.mm_tipork) " & _
                              "ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo RESO |1|, anno RESO |2|, serie RESO |3|, numero RESO |4|, riga RESO |5|, articolo RESO |6|, magazzino RESO |7|, causale RESO |8|.", _
          "734"})

        'Controllo 735 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente che si riferiscono a carichi di produzione (campi mm_cc*) dove il codice articolo non coincide
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza nei movimenti di reso cliente che si riferiscono a carichi di produzione (campi mm_cc*) dove il codice articolo non coincide", _
          "1", "1", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, movmag.mm_codart, Carprod.mm_codart FROM movmag INNER JOIN movmag AS Carprod ON (movmag.mm_ccriga = Carprod.mm_riga) AND (movmag.mm_ccnum = Carprod.mm_numdoc) AND (movmag.mm_ccserie = Carprod.mm_serie) AND (movmag.mm_ccanno = Carprod.mm_anno) AND (movmag.mm_cctipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt) WHERE movmag.mm_codart <> Carprod.mm_codart ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Movimento anomalo: ditta |0|, tipo RESO |1|, anno RESO |2|, serie RESO |3|, numero RESO |4|, riga RESO |5|, tipo CAR.PROD |6|, anno CAR.PROD |7|, serie CAR.PROD |8|, numero CAR.PROD |9|, riga CAR.PROD |10|, articolo RESO |11|, articolo CAR.PROD |12|.", _
          "735"})

        'Controllo 736 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di produzione utilizzati in documento di reso dove la quantità resa al cliente supera la quantità prodotta
        .Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di produzione utilizzati in documento di reso dove la quantità resa al cliente supera la quantità prodotta", _
          "1", "1", "SELECT Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, Min(Carprod.mm_codart) AS Codart, Min(Carprod.mm_quant) AS QuantCarico, Sum(movmag.mm_quant) AS QuantReso " & _
                              "FROM movmag INNER JOIN movmag AS Carprod ON (movmag.mm_ccriga = Carprod.mm_riga) AND (movmag.mm_ccnum = Carprod.mm_numdoc) AND (movmag.mm_ccserie = Carprod.mm_serie) AND (movmag.mm_ccanno = Carprod.mm_anno) AND (movmag.mm_cctipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt) " & _
                              "WHERE (movmag.mm_ccriga <> movmag.mm_riga OR movmag.mm_ccnum <> movmag.mm_numdoc OR movmag.mm_ccserie <>movmag.mm_serie OR movmag.mm_ccanno <> movmag.mm_anno OR movmag.mm_cctipo <> movmag.mm_tipork) " & _
                              "GROUP BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga " & _
                              "HAVING Sum(movmag.mm_quant) > Min(Carprod.mm_quant) " & _
                              "ORDER BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga", _
          "", "1", "0", "1", "0", "0", "Carico di produzione: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo |6|, quantità CAR.PROD |7|, quantità RESA |8| .", _
          "736"})

        ''--- Controllo 737 C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di produzione utilizzati in documento di reso dove la quantità resa al cliente supera la quantità prodotta
        '.Rows.Add(New Object() {"MG,DB", "movmag", "C/LAV. ATTIVO, ARTICOLI NO FASE (con dist. base): presenza di carichi di produzione utilizzati in documento di reso dove la quantità resa al cliente supera la quantità prodotta", _
        '  "1", "1", "SELECT Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga, Min(Carprod.mm_codart) AS Codart, Min(Carprod.mm_quant) AS QuantCarico, Sum(movmag.mm_quant) AS QuantReso " & _
        '                      "FROM movmag INNER JOIN movmag AS Carprod ON (movmag.mm_ccriga = Carprod.mm_riga) AND (movmag.mm_ccnum = Carprod.mm_numdoc) AND (movmag.mm_ccserie = Carprod.mm_serie) AND (movmag.mm_ccanno = Carprod.mm_anno) AND (movmag.mm_cctipo = Carprod.mm_tipork) AND (movmag.codditt = Carprod.codditt) " & _
        '                      "WHERE (movmag.mm_ccriga <> movmag.mm_riga OR movmag.mm_ccnum <> movmag.mm_numdoc OR movmag.mm_ccserie <>movmag.mm_serie OR movmag.mm_ccanno <> movmag.mm_anno OR movmag.mm_cctipo <> movmag.mm_tipork) " & _
        '                      "GROUP BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga " & _
        '                      "HAVING Sum(movmag.mm_quant) > Min(Carprod.mm_quant) " & _
        '                      "ORDER BY Carprod.codditt, Carprod.mm_tipork, Carprod.mm_anno, Carprod.mm_serie, Carprod.mm_numdoc, Carprod.mm_riga", _
        '  "", "1", "0", "1", "0", "0", "Carico di produzione: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, articolo |6|, quantità CAR.PROD |7|, quantità RESA |8| .", _
        '  "737"})

        'Controllo 740: NOTE DI PRELIEVO legate ad una riga d'ORDINE inesistente
        .Rows.Add(New Object() {"VE", "movprb", "Presenza di righe di Note di Prelievo legate ad una riga d'ordine inesistente.", _
          "1", "0", "SELECT movprb.codditt, movprb.mm_anno, movprb.mm_serie, movprb.mm_numdoc, movprb.mm_riga" & _
                    " FROM movprb LEFT JOIN movord ON movprb.codditt = movord.codditt AND movprb.mm_ortipo = movord.mo_tipork AND movprb.mm_oranno = movord.mo_anno AND movprb.mm_orserie = movord.mo_serie AND movprb.mm_ornum = movord.mo_numord AND movprb.mm_orriga = movord.mo_riga" & _
                    " WHERE movprb.mm_ornum <> 0" & _
                    " AND movord.mo_tipork IS NULL" & _
                    " ORDER BY movprb.codditt, movprb.mm_anno, movprb.mm_serie, movprb.mm_numdoc, movprb.mm_riga", _
          "", "1", "0", "1", "0", "0", "Errore nella Nota di Prelievo, ditta |0|, anno |1|, n. |3|/|2|, riga |4|: La riga risulta essere legata ad una riga d'ordine/impegno inesistente.", _
          "740"})

        'Controllo 741: DOCUMENTI DI MAGAZZINO legati ad una riga d'ORDINE inesistente
        .Rows.Add(New Object() {"VE", "movmag", "Presenza di righe di Documenti di Magazzino legate ad una riga d'ordine inesistente.", _
          "1", "0", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga" & _
                    " FROM movmag LEFT JOIN movord ON movmag.codditt = movord.codditt AND movmag.mm_ortipo = movord.mo_tipork AND movmag.mm_oranno = movord.mo_anno AND movmag.mm_orserie = movord.mo_serie AND movmag.mm_ornum = movord.mo_numord AND movmag.mm_orriga = movord.mo_riga" & _
                    " WHERE movmag.mm_ornum <> 0" & _
                    " AND movord.mo_tipork IS NULL" & _
                    " ORDER BY movmag.codditt, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
          "", "1", "0", "1", "0", "0", "Errore nel Documento, ditta |0|, tipo |1| anno |2|, n. |4|/|3|, riga |5|: La riga risulta essere legata ad una riga d'ordine/impegno inesistente.", _
          "741"})

        'Controllo 742: DOCUMENTI DI MAGAZZINO legati ad una riga di NOTA DI PRELIEVO inesistente
        .Rows.Add(New Object() {"VE", "movmag", "Presenza di righe di Documenti di Magazzino legate ad una riga di Nota di Prelievo inesistente.", _
        "1", "0", "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga" & _
                  " FROM movmag LEFT JOIN movprb ON movmag.codditt = movprb.codditt AND movmag.mm_nptipo = movprb.mm_tipork AND movmag.mm_npanno = movprb.mm_anno AND movmag.mm_npserie = movprb.mm_serie AND movmag.mm_npnum = movprb.mm_numdoc AND movmag.mm_npriga = movprb.mm_riga" & _
                  " WHERE movmag.mm_npnum <> 0" & _
                  " AND movprb.mm_tipork IS NULL" & _
                  " ORDER BY movmag.codditt, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga", _
        "", "1", "0", "1", "0", "0", "Errore nel Documento, ditta |0|, tipo |1| anno |2|, n. |4|/|3|, riga |5|: La riga risulta essere legata ad una riga di Nota di Prelievo inesistente.", _
        "742"})

        'Controllo 743: Righe di ORDINI EVASE collegate a righe di Note di Prelievo NON EVASE
        .Rows.Add(New Object() {"OR", "movord", "Presenza di righe di Ordini/Impegni evase legate ad una riga di Nota di Prelievo non evasa.", _
        "1", "1", "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga" & _
                  " FROM movprb INNER JOIN movord ON movprb.codditt = movord.codditt AND movprb.mm_ortipo = movord.mo_tipork AND movprb.mm_oranno = movord.mo_anno AND movprb.mm_orserie = movord.mo_serie AND movprb.mm_ornum = movord.mo_numord AND movprb.mm_orriga = movord.mo_riga" & _
                  " WHERE movprb.mm_ornum <> 0" & _
                  " AND movprb.mm_salcon = 'C'" & _
                  " AND movord.mo_flevas = 'S'" & _
                  " ORDER BY movord.codditt, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga", _
        "", "1", "0", "1", "0", "0", "Nell'Ordine/Impegno, ditta |0|, tipo |1| anno |2|, n. |4|/|3|, riga |5|: La riga risulta essere evasa e legata ad una riga di Nota di Prelievo NON evasa.", _
        "743"})

        'Controllo 744: Righe di ORDINI collegate a righe di Documenti di Magazzino dove la quantità evasa dell'ordine/impegno differisce dalla quantità del Documento
        .Rows.Add(New Object() {"OR", "movord", "Presenza di righe di Ordini/Impegni legate ad una riga di Documento di Magazzino dove la quantità evasa dell'Ordine/Impegno differisce dalla quantità del documento.", _
        "1", "1", "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga" & _
                  " FROM movmag INNER JOIN movord ON movmag.codditt = movord.codditt AND movmag.mm_ortipo = movord.mo_tipork AND movmag.mm_oranno = movord.mo_anno AND movmag.mm_orserie = movord.mo_serie AND movmag.mm_ornum = movord.mo_numord AND movmag.mm_orriga = movord.mo_riga" & _
                  " WHERE movmag.mm_ornum <> 0" & _
                  " AND movmag.mm_quant <> movord.mo_quaeva" & _
                  " ORDER BY movord.codditt, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga", _
        "", "1", "0", "1", "0", "0", "Nell'Ordine/Impegno, ditta |0|, tipo |1| anno |2|, n. |4|/|3|, riga |5|: La riga risulta possedere una quantità evasa diversa da quella della riga del documento collegata.", _
        "744"})

        'Controllo 745: Righe di ORDINI collegate a righe di Note di Prelievo dove la quantità evasa dell'ordine/impegno differisce dalla quantità della Nota di Prelievo
        .Rows.Add(New Object() {"OR", "movord", "Presenza di righe di Ordini/Impegni legate ad una riga di Note di Prelievo dove la quantità evasa dell'Ordine/Impegno differisce dalla quantità della Nota di Prelievo.", _
        "1", "1", "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga" & _
                  " FROM movprb INNER JOIN movord ON movprb.codditt = movord.codditt AND movprb.mm_ortipo = movord.mo_tipork AND movprb.mm_oranno = movord.mo_anno AND movprb.mm_orserie = movord.mo_serie AND movprb.mm_ornum = movord.mo_numord AND movprb.mm_orriga = movord.mo_riga" & _
                  " WHERE movprb.mm_ornum <> 0" & _
                  " AND movprb.mm_quant <> movord.mo_quapre" & _
                  " ORDER BY movord.codditt, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga", _
        "", "1", "0", "1", "0", "0", "Nell'Ordine/Impegno, ditta |0|, tipo |1| anno |2|, n. |4|/|3|, riga |5|: La riga risulta possedere una quantità evasa diversa da quella della riga della Note di Prelievo collegata.", _
        "745"})

        'Controllo 750: Righe di ORDLIST con articoli T&C che NON possiedono dettaglio in ORDLISTTC
        .Rows.Add(New Object() {"OR", "ordlist", _
          "Presenza di righe in Proposte d'Ordine con articoli gestiti per Taglie & Colori che non possiedono il dettaglio relativo.", _
          "1", "1", _
          "SELECT ordlist.codditt, ol_tipork, ordlist.ol_progr, ol_codart, ol_conto, ol_commeca," & _
          " ol_magaz, ol_datcons, ol_datord" & _
          " FROM (ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart)" & _
          " LEFT JOIN ordlisttc ON ordlist.codditt = ordlisttc.codditt AND ordlist.ol_progr = ordlisttc.ol_progr" & _
          " WHERE ar_codtagl <> 0" & _
          " AND ordlisttc.ol_progr IS NULL" & _
          " ORDER BY ordlist.codditt, ordlist.ol_progr, ol_tipork, ol_codart, ol_conto, ol_commeca, ol_magaz," & _
          " ol_datcons, ol_datord", _
          "", "1", "0", "1", "0", "0", _
          "Nella Proposta d'Ordine, ditta |0|, tipo |1| riga |2|, articolo |3|, fornitore |4|, commessa |5|," & _
          " magazzino |6|, data consegna |7|, data ordine |8|:" & _
          " l'articolo risulta essere gestito a Taglie e Colori, ma non possiede il dettaglio relativo.", _
          "750"})

        'Controllo 751: Righe di MOVORD con articoli T&C che NON possiedono dettaglio in MOVORDTC
        .Rows.Add(New Object() {"OR", "movord", _
          "Presenza di righe in Gestioni Ordini con articoli gestiti per Taglie & Colori che non possiedono il dettaglio relativo.", _
          "1", "1", _
          "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord," & _
          " movord.mo_riga, movord.mo_codart" & _
          " FROM (movord INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart)" & _
          " LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
          " WHERE ar_codtagl <> 0" & _
          " AND movordtc.mo_tipork IS NULL" & _
          " ORDER BY movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord," & _
          " movord.mo_riga, movord.mo_codart", _
          "", "1", "0", "1", "0", "0", _
          "Nell'Ordine/Impegno, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|, riga|5|, articolo |6|:" & _
          " l'articolo risulta essere gestito a Taglie e Colori, ma non possiede il dettaglio relativo.", _
          "751"})

        'Controllo 752: Righe di MOVMAG con articoli T&C che NON possiedono dettaglio in MOVMAGTC
        .Rows.Add(New Object() {"MG", "movmag", _
          "Presenza di righe in Gestioni Documenti di Magazzino con articoli gestiti per Taglie & Colori che non possiedono il dettaglio relativo.", _
          "1", "1", _
          "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc," & _
          " movmag.mm_riga, movmag.mm_codart" & _
          " FROM (movmag INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart)" & _
          " LEFT JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
          " WHERE ar_codtagl <> 0" & _
          " AND movmagtc.mm_tipork IS NULL" & _
          " ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc," & _
          " movmag.mm_riga, movmag.mm_codart", _
          "", "1", "0", "1", "0", "0", _
          "Nel Documento di Magazzino, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|, riga|5|, articolo |6|:" & _
          " l'articolo risulta essere gestito a Taglie e Colori, ma non possiede il dettaglio relativo.", _
          "752"})

        'Controllo 753: Righe di ORDLIST con articoli T&C con quantità diversa da somma delle 24 quantità in ORDLISTTC
        strSQL = "SELECT ordlist.codditt, ol_tipork, ordlist.ol_progr, ol_codart, ol_conto, ol_commeca, ol_magaz," & _
          " ol_datcons, ol_datord, ordlist.ol_quant, "
        strListaCampi = "("
        nIndex = 0
        For nIndex = 1 To 24
          strListaCampi += "ol_quant" & Microsoft.VisualBasic.Right("0" & CStr(nIndex), 2) & IIf(nIndex < 24, " + ", ")").ToString
        Next nIndex
        strSQL += strListaCampi & " As Somma" & _
          " FROM ordlist INNER JOIN ordlisttc ON ordlist.codditt = ordlisttc.codditt AND ordlist.ol_progr = ordlisttc.ol_progr" & _
          " WHERE ol_quant <> " & strListaCampi & _
          " ORDER BY ordlist.codditt, ordlist.ol_progr, ol_tipork, ol_codart, ol_conto, ol_commeca, ol_magaz," & _
          " ol_datcons, ol_datord"
        .Rows.Add(New Object() {"OR", "ordlist", _
          "Presenza di righe in Proposte d'Ordine con articoli gestiti per Taglie & Colori che possiedono la quantità di riga diversa dalla somma delle quantità nel dettaglio relativo.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nella Proposta d'Ordine, ditta |0|, tipo |1| riga |2|, articolo |3|, fornitore |4|, commessa |5|," & _
          " magazzino |6|, data consegna |7|, data ordine |8|:" & _
          " l'articolo, gestito a Taglie e Colori, possiede quantità (|9|) diversa da somma quantità dettaglio (|10|).", _
          "753"})

        'Controllo 754: Righe di MOVORD con articoli T&C con quantità diversa da somma delle 24 quantità in MOVORDTC
        strSQL = "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord," & _
          " movord.mo_riga, movord.mo_codart, movord.mo_quant, "
        strListaCampi = "("
        nIndex = 0
        For nIndex = 1 To 24
          strListaCampi += "mo_quant" & Microsoft.VisualBasic.Right("0" & CStr(nIndex), 2) & IIf(nIndex < 24, " + ", ")").ToString
        Next nIndex
        strSQL += strListaCampi & " As Somma" & _
          " FROM movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
          " WHERE mo_quant <> " & strListaCampi & _
          " ORDER BY movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord," & _
          " movord.mo_riga, movord.mo_codart"
        .Rows.Add(New Object() {"OR", "movord", _
          "Presenza di righe in Gestioni Ordini con articoli gestiti per Taglie & Colori che possiedono la quantità di riga diversa dalla somma delle quantità nel dettaglio relativo.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nell'Ordine/Impegno, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|, riga|5|, articolo |6|:" & _
          " l'articolo, gestito a Taglie e Colori, possiede quantità (|7|) diversa da somma quantità dettaglio (|8|).", _
          "754"})

        'Controllo 755: Righe di MOVMAG con articoli T&C con quantità diversa da somma delle 24 quantità in MOVMAGTC
        strSQL = "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc," & _
          " movmag.mm_riga, movmag.mm_codart, movmag.mm_quant, "
        strListaCampi = "("
        nIndex = 0
        For nIndex = 1 To 24
          strListaCampi += "mm_quant" & Microsoft.VisualBasic.Right("0" & CStr(nIndex), 2) & IIf(nIndex < 24, " + ", ")").ToString
        Next nIndex
        strSQL += strListaCampi & " As Somma" & _
          " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
          " WHERE mm_quant <> " & strListaCampi & _
          " ORDER BY movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc," & _
          " movmag.mm_riga, movmag.mm_codart"
        .Rows.Add(New Object() {"MG", "movmag", _
          "Presenza di righe in Gestioni Documenti di Magazzino con articoli gestiti per Taglie & Colori che possiedono la quantità di riga diversa dalla somma delle quantità nel dettaglio relativo.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nel Documento di Magazzino, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|, riga|5|, articolo |6|:" & _
          " l'articolo, gestito a Taglie e Colori, possiede quantità (|7|) diversa da somma quantità dettaglio (|8|).", _
          "755"})

        'Controllo 756: Righe di impegni senza la riga di ordine (MOVMAG)
        strSQL = "SELECT movmag.codditt, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc " & _
                 "FROM movmag LEFT JOIN movmag AS movmagH ON movmag.codditt = movmagH.codditt AND " & _
                 "movmagH.mm_tipork = movmag.mm_prtipo AND movmagH.mm_anno = movmag.mm_pranno AND movmagH.mm_serie = movmag.mm_prserie AND " & _
                 "movmagH.mm_numdoc = movmag.mm_prnum AND movmagH.mm_riga = movmag.mm_prriga " & _
                 " WHERE movmag.mm_tipork = 'U' AND movmagH.mm_tipork is null"
        .Rows.Add(New Object() {"MG", "movmag", _
          "Presenza di righe di Scarico a produzione in Gestioni Documenti di Magazzino senza la relativa riga di Carico da produzione.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nel Documento di Magazzino, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|:" & _
          " Manca la relativa riga di Carico da produzione.", _
          "756"})

        'Controllo 757: Righe di impegni senza la riga di ordine (MOVORD)
        strSQL = "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord " & _
                 "FROM movord LEFT JOIN movord AS movordH ON movord.codditt = movordH.codditt AND " & _
                 "movordH.mo_tipork = movord.mo_tiporkor AND movordH.mo_anno = movord.mo_annoor AND movordH.mo_serie = movord.mo_serieor AND " & _
                 "movordH.mo_numord = movord.mo_numordor AND movordH.mo_riga = movord.mo_rigaor " & _
                 " WHERE movord.mo_tipork = 'Y' AND movordH.mo_tipork is null"
        .Rows.Add(New Object() {"OR", "movord", _
          "Presenza di righe di Impegno di produzione in Gestioni Ordini senza la relativa riga di Ordine di produzione.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nell'Impegno di produzione, ditta |0|, tipo |1| anno |2|, serie |3|, numero |4|:" & _
          " Manca la relativa riga di Ordine di produzione.", _
          "757"})

        'Controllo 758: Righe di impegni senza la riga di ordine (ORDLIST)
        strSQL = "SELECT ordlist.codditt, ordlist.ol_progr " & _
                 "FROM ordlist LEFT JOIN ordlist AS ordlistH ON ordlist.codditt = ordlistH.codditt AND " & _
                 "ordlistH.ol_progr = ordlist.ol_olprogr " & _
                 " WHERE ordlist.ol_tipork = 'Y' AND ordlistH.ol_olprogr is null"
        .Rows.Add(New Object() {"OR", "ordlist", _
          "Presenza di righe di Impegno di produzione in Proposte d'ordine senza la relativa riga di Proposte d'ordine di produzione.", _
          "1", "1", strSQL, "", "1", "0", "1", "0", "0", _
          "Nell'Impegno di produzione, ditta |0|, progressivo |1|:" & _
          " Manca la relativa riga di Proposta d'ordine di produzione.", _
          "758"})

        'Controllo 800: Operatori non CRM presenti su acclead in quanto proprietari di lead[Solo dove ARCPROC è sulla stessa istanza del server del DB azienda, e ARCPROC non è stato rinominato]
        .Rows.Add(New Object() {"CR", "acclead", "Operatori non CRM presenti su acclead in quanto proprietari di lead.[Solo dove ARCPROC è sulla stessa istanza del server del DB azienda, e ARCPROC non è stato rinominato]", _
        "1", "1", "select distinct opcr_opnome from acclead as ac inner join ARCPROC.dbo.OPERAT as op on ac.opcr_opnome=op.OpNome" & _
                  " where op.OpIscrmus <>'S' order by opcr_opnome", _
        "", "1", "0", "1", "0", "0", "L'operatore |0| non è di tipo CRM ma risulta proprietario di uno o più lead.", _
        "800"})
        .Rows.Add(New Object() {"CR", "anagra", "Anagrafiche clienti senza lead collegato", _
        "1", "1", "SELECT anagra.codditt, an_conto, an_descr1 FROM anagra LEFT JOIN (select * from leads where le_coddest = 0) as leads2 ON (leads2.codditt = anagra.codditt) AND (leads2.le_conto = anagra.an_conto) where an_tipo = 'C' and leads2.le_codlead is null order by anagra.codditt, an_conto", _
        "", "1", "0", "1", "0", "0", "Sulla ditta |0| il cliente |1| - |2| non ha un lead collegato.", _
        "801"})
        .Rows.Add(New Object() {"CR", "destdiv", "Destinazioni clienti senza lead collegato", _
        "1", "1", "SELECT anagra.codditt, an_conto, an_descr1, dd_coddest, dd_nomdest FROM (destdiv left JOIN leads ON (destdiv.codditt = leads.codditt) AND (destdiv.dd_conto = leads.le_conto) AND (destdiv.dd_coddest = leads.le_coddest)) INNER JOIN anagra ON (destdiv.dd_conto = anagra.an_conto) AND (destdiv.codditt = anagra.codditt) where an_tipo = 'C' and leads.le_codlead is null", _
        "", "1", "0", "1", "0", "0", "Sulla ditta |0| il cliente |1| - |2| non ha un lead collegato sulla destinazione |3| - |4|.", _
        "802"})
        .Rows.Add(New Object() {"CR", "acclead", "Lead che non hanno record sulla tabella acclead degli accessi CRM", _
        "1", "1", "SELECT leads.codditt, le_codlead, le_descr1 FROM leads LEFT JOIN acclead ON (acclead.codditt = leads.codditt) AND (acclead.opcr_codlead = leads.le_codlead) where opcr_opnome is null", _
        "", "1", "0", "1", "0", "0", "Sulla ditta |0| il lead |1| - |2| non ha nessun record in acclead.", _
        "803"})

        'Controllo 820: Budget di contabilità analitica: presenza codici aggregazione budget su commesse se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "commess", "Budget di contabilità analitica: presenza di commesse senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, co_comme FROM commess WHERE co_coddica = ' ' OR co_coddicv = ' ' ORDER BY codditt, co_comme", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, commessa |1|: su questa commessa non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "820"})

        'Controllo 821: Budget di contabilità analitica: presenza codici aggregazione budget su centri se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabcena", "Budget di contabilità analitica: presenza di centri senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, tb_codcena FROM tabcena WHERE tb_coddica = ' ' OR tb_coddicv = ' ' ORDER BY codditt, tb_codcena", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, Centro |1|: su questo centro non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "821"})

        'Controllo 822: Budget di contabilità analitica: presenza codici aggregazione budget sulle linee\prodotti se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabcfam", "Budget di contabilità analitica: presenza di linee\prodotti senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, tb_codcfam FROM tabcfam WHERE tb_coddica = ' ' OR tb_coddicv = ' ' ORDER BY codditt, tb_codcfam", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, linea\prodotto |1|: su questa linea\prodotto non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "822"})

        'Controllo 823: Budget di contabilità analitica: presenza codici aggregazione budget sulle divisioni se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabdivi", "Budget di contabilità analitica: presenza di divisioni senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, tb_coddivi FROM tabdivi WHERE tb_coddica = ' ' OR tb_coddicv = ' ' ORDER BY codditt, tb_coddivi", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, divisione |1|: su questa divisione non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "823"})

        'Controllo 824: Budget di contabilità analitica: presenza codici aggregazione budget sugli stabilimenti se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabstab", "Budget di contabilità analitica: presenza di stabilimenti\filiali\negozi senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, tb_codstab FROM tabstab WHERE tb_coddica = ' ' OR tb_coddicv = ' ' ORDER BY codditt, tb_codstab", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, stabilimento\filiale\negozio |1|: su questo stabilimento\filiale\negozio non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "824"})

        'Controllo 825: Budget di contabilità analitica: presenza codici aggregazione budget sugli articoli se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "artico", "Budget di contabilità analitica: presenza di articoli senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, ar_codart FROM artico WHERE ar_gesfasi = 'N' AND ar_coddica = ' ' OR ar_coddicv = ' ' ORDER BY codditt, ar_codart", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|: su questo articolo non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "825"})

        'Controllo 826: Budget di contabilità analitica: presenza codici aggregazione budget sulle fasi se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "artfasi", "Budget di contabilità analitica: presenza di fasi senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, af_codart, af_fase FROM artfasi WHERE af_coddica = ' ' OR af_coddicv = ' ' ORDER BY codditt, af_codart, af_fase", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|, fase |2|: su questa fase non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "826"})

        'Controllo 827: Budget di contabilità analitica: mancanza codici 'tipo entità' su commesse se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "commess", "Budget di contabilità analitica: presenza di commesse senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, co_comme FROM commess WHERE co_codtcdc = 0 ORDER BY codditt, co_comme", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, commessa |1|: su questa commessa non è indicato il codice 'Tipo entità'.", _
          "827"})

        'Controllo 828: Budget di contabilità analitica: mancanza codici 'tipo entità' su centri se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabcena", "Budget di contabilità analitica: presenza di centri senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, tb_codcena FROM tabcena WHERE tb_codtcdc = 0 ORDER BY codditt, tb_codcena", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, Centro |1|: su questo centro non è indicato il codice 'Tipo entità'.", _
          "828"})

        'Controllo 829: Budget di contabilità analitica: mancanza codici 'tipo entità' sulle linee\prodotti se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabcfam", "Budget di contabilità analitica: presenza di linee\prodotti senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, tb_codcfam FROM tabcfam WHERE tb_codtcdc = 0 ORDER BY codditt, tb_codcfam", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, linea\prodotto |1|: su questa linea\prodotto non è indicato il codice 'Tipo entità'.", _
          "829"})

        'Controllo 830: Budget di contabilità analitica: mancanza codici 'tipo entità' sulle divisioni se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabdivi", "Budget di contabilità analitica: presenza di divisioni senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, tb_coddivi FROM tabdivi WHERE tb_codtcdc = 0 ORDER BY codditt, tb_coddivi", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, divisione |1|: su questa divisione non è indicato il codice 'Tipo entità'.", _
          "830"})

        'Controllo 831: Budget di contabilità analitica: mancanza codici 'tipo entità' sugli stabilimenti se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "tabstab", "Budget di contabilità analitica: presenza di stabilimenti\filiali\negozi senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, tb_codstab FROM tabstab WHERE tb_codtcdc = 0 ORDER BY codditt, tb_codstab", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, stabilimento\filiale\negozio |1|: su questo stabilimento\filiale\negozio non è indicato il codice 'Tipo entità'.", _
          "831"})

        'Controllo 832: Budget di contabilità analitica: mancanza codici 'tipo entità' sugli articoli se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "artico", "Budget di contabilità analitica: presenza di articoli senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, ar_codart FROM artico WHERE ar_gesfasi = 'N' AND ar_codtcdc = 0 ORDER BY codditt, ar_codart", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|: su questo articolo non è indicato il codice 'Tipo entità'.", _
          "832"})

        'Controllo 833: Budget di contabilità analitica: mancanza codici 'tipo entità' sulle fasi se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "artfasi", "Budget di contabilità analitica: presenza di fasi senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, af_codart, af_fase FROM artfasi WHERE af_codtcdc = 0 ORDER BY codditt, af_codart, af_fase", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, articolo |1|, fase |2|: su questa fase non è indicato il codice 'Tipo entità'.", _
          "833"})

        'Controllo 834: Budget di contabilità analitica: presenza codici aggregazione budget sugli articoli se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "anagra", "Budget di contabilità analitica: presenza di clienti senza codice di Aggregazione Budget", _
          "1", "1", "SELECT DISTINCT codditt, an_conto FROM anagra WHERE an_tipo <> 'S' AND an_coddica = ' ' OR an_coddicv = ' ' ORDER BY codditt, an_conto", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, cliente |1|: su questo cliente non è indicato il codice di aggregazione budget o il valore di aggregazione budget.", _
          "834"})

        'Controllo 835: Budget di contabilità analitica: mancanza codici 'tipo entità' sulle fasi se attivo modulo BUD
        .Rows.Add(New Object() {"CX", "anagra", "Budget di contabilità analitica: presenza di clienti senza codice 'Tipo entità'", _
          "1", "1", "SELECT DISTINCT codditt, an_conto FROM anagra WHERE an_tipo <> 'S' AND an_codtcdc = 0 ORDER BY codditt, an_conto", _
          "", "1", "0", "1", "0", "0", "Ditta |0|, cliente |1|: su questa cliente non è indicato il codice 'Tipo entità'.", _
          "835"})

        'Controllo 851: Controllo quadrature ca-dc/cg: differenze tra imputazione contabilità generale e contabilità analitica
        .Rows.Add(New Object() {"CA", "priana2,prinot,anagra", "Controllo quadrature ca-dc/cg: differenze tra imputazione contabilità generale e contabilità analitica", _
          "1", "1", "select pn_datreg as DataRegCG, pn_numreg as NumRegCG, pn_escomp as AnnoCompCG, pn_causale as CausaleCG, pn_integr as Integrativa, pn_conto as ContoCG, ((pn_importo) * (-1)) as ImportoCG, pa2_contocg as ContoCA, sum(pa2_importo) as ImportoCA, pa2_numreg as NumRegCA, pa2_datreg as DataRegCA, pa2_escomp as AnnoCompCA" & _
                    " from (priana2 inner join prinot on priana2.codditt = prinot.codditt" & _
                    " and pa2_numreg = pn_numreg and pa2_escomp = pn_escomp" & _
                    " and pa2_riga = pn_riga" & _
                    " and pa2_datreg = pn_datreg and pn_conto = pa2_contocg)" & _
                    " inner join anagra on prinot.codditt = anagra.codditt and pn_conto = an_conto" & _
                    " where(an_flci = 1)" & _
                    " group by pn_datreg, pn_numreg, pn_escomp, pn_causale, pa2_contocg, pn_conto," & _
                    " pa2_numreg, pa2_datreg, pa2_escomp, pa2_riga, pn_importo, pn_integr" & _
                    " having(SUM(pa2_importo) <> -(pn_importo))", _
          "", "1", "0", "1", "0", "0", "La registrazione di CG in data |0| num. |1| (anno comp. |2| e integrativa=|4|) sul conto |5| ha un importo di |6|, mentre in CA è impostato il conto |7| con importo |8| (estremi reg. di CA data |10| num. |9| anno comp. |11|).", _
          "851"})

        'Controllo 852: Controllo quadrature ca-dc/cg: imputazione totale in contabilità generale e nulla in contabilità analitica
        .Rows.Add(New Object() {"CA", "priana2,prinot,anagra", "Controllo quadrature ca-dc/cg: imputazione totale in contabilità generale e nulla in contabilità analitica", _
          "1", "1", "select pn_datreg as DataRegCG, pn_numreg as NumRegCG, pn_escomp as AnnoCompCG, pn_causale as CausaleCG, pn_integr as Integrativa, pn_conto as ContoCG, ((pn_importo) * (-1)) as ImportoCG, pa2_contocg as ContoCA, sum(pa2_importo) as ImportoCA, pa2_numreg as NumRegCA, pa2_datreg as DataRegCA, pa2_escomp as AnnoCompCA" & _
                    " from (prinot inner join anagra on prinot.codditt = anagra.codditt and pn_conto = an_conto)" & _
                    " left join priana2 on priana2.codditt = prinot.codditt and pa2_numreg = pn_numreg" & _
                    " and pa2_riga = pn_riga" & _
                    " and pa2_escomp = pn_escomp and pa2_datreg = pn_datreg and pn_conto = pa2_contocg" & _
                    " where(an_flci = 1)" & _
                    " group by pn_datreg, pn_numreg, pn_escomp, pn_causale, pa2_contocg, pn_conto," & _
                    " pa2_numreg, pa2_datreg, pa2_escomp, pa2_riga, pn_importo, pn_integr" & _
                    " having(SUM(pa2_importo) Is null)", _
          "", "1", "0", "1", "0", "0", "La registrazione di CG in data |0| num. |1| (anno comp. |2| e integrativa=|4|) sul conto |5| ha un importo di |6| non ha una registrazione di CA collegata.", _
          "852"})

        'Controllo 853: Controllo quadrature ca-dc/cg: imputazione nulla in contabilità generale e totale in contabilità analitica
        .Rows.Add(New Object() {"CA", "priana2,prinot,anagra", "Controllo quadrature ca-dc/cg: imputazione nulla in contabilità generale e totale in contabilità analitica", _
          "1", "1", "select pn_datreg as DataRegCG, pn_numreg as NumRegCG, pn_escomp as AnnoCompCG, pn_causale as CausaleCG, pn_conto as ContoCG, ((pn_importo) * (-1)) as ImportoCG, pa2_contocg as ContoCA, sum(pa2_importo) as ImportoCA, pa2_numreg as NumRegCA, pa2_datreg as DataRegCA, pa2_escomp as AnnoCompCA" & _
                    " from (priana2 inner join anagra on priana2.codditt = anagra.codditt and pa2_contocg = an_conto)" & _
                    " left join prinot on priana2.codditt = prinot.codditt and pa2_numreg = pn_numreg" & _
                    " and pa2_riga = pn_riga" & _
                    " and pa2_escomp = pn_escomp and pa2_datreg = pn_datreg and pn_conto = pa2_contocg" & _
                    " where(an_flci = 1)" & _
                    " group by pn_datreg, pn_numreg, pn_escomp, pn_causale, pa2_contocg, pn_conto," & _
                    " pa2_numreg, pa2_datreg, pa2_escomp, pa2_riga, pn_importo" & _
                    " having(SUM(pa2_importo) Is Not null And pn_importo Is null)", _
          "", "1", "0", "1", "0", "0", "La registrazione di CA in data |9| num. |8| (anno comp. |10|) sul conto |6| con un importo di |7| non ha una registrazione di CG collegata.", _
          "853"})

        'Controllo 900: Listini: presenza di listini 'Speciali' con 'Tipo' errato
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di listini 'Speciali' con 'Tipo' errato.", _
          "1", "0", _
          "SELECT DISTINCT codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino," & _
          " lc_datagg, lc_daquant, lc_unmis, lc_fase" & _
          " FROM listini" & _
          " WHERE lc_conto <> 0" & _
          " AND lc_tipo = ' '" & _
          " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg," & _
          " lc_daquant, lc_unmis, lc_fase", _
          "", "1", "0", "1", "0", "0", _
          "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "900"})

        'Controllo 901: Listini: presenza di listini 'Speciali Clienti/Fornitori' con 'Conto' a zero
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di listini 'Speciali Clienti/Fornitori' con 'Conto' a zero.", _
          "1", "0", _
          "SELECT DISTINCT codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino," & _
          " lc_datagg, lc_daquant, lc_unmis, lc_fase" & _
          " FROM listini" & _
          " WHERE lc_conto = 0" & _
          " AND lc_tipo IN ('C', 'F')" & _
          " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg," & _
          " lc_daquant, lc_unmis, lc_fase", _
          "", "1", "0", "1", "0", "0", _
          "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "901"})

        'Controllo 902: Listini: presenza di listini 'Speciali Clienti/Fornitori' con 'Listino' diverso da zero
        .Rows.Add(New Object() {"VE", "listini", "Listini: presenza di listini 'Speciali Clienti/Fornitori' con 'Listino' diverso da zero.", _
          "1", "0", _
          "SELECT DISTINCT codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino," & _
          " lc_datagg, lc_daquant, lc_unmis, lc_fase" & _
          " FROM listini" & _
          " WHERE lc_conto <> 0" & _
          " AND lc_listino <> 0" & _
          " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg," & _
          " lc_daquant, lc_unmis, lc_fase", _
          "", "1", "0", "1", "0", "0", _
          "Listino anomalo: ditta |0|, articolo |1|, lavorazione |2|, conto |3|, valuta |4|, promozione |5|, listino |6|, data aggiornamento |7|, da quantità |8|, unità di misura |9|, fase |10|.", _
          "902"})

        'Controllo 903: Leads cancellati logicamente con offerta collegata
        .Rows.Add(New Object() {"CR", "leads", "Leads cancellati logicamente con offerta collegata.", _
          "1", "0", _
          "SELECT leads.codditt, leads.le_codlead, testoff.td_tipork, testoff.td_anno, testoff.td_serie, testoff.td_numord, testoff.td_vers" & _
          " FROM leads INNER JOIN testoff ON testoff.codditt = leads.codditt AND testoff.td_codlead = leads.le_codlead WHERE le_servdeleted = 'S'", _
          "", "1", "0", "1", "0", "0", _
          "Leads Offerta anomalo: ditta |0|, lead |1|, tipodoc |2|, anno |3|, serie |4|, numero |5|, versione |6|.", _
          "903"})

        'Controllo 950: Righe d'Ordine/Impegno con valore da evadere disallineato (in Euro, prezzo UMP)
        .Rows.Add(New Object() {"OR", "movord", "Righe d'Ordine/Impegno con valore da evadere disallineato (in Euro, prezzo UMP)", _
          "1", "0", _
          "SELECT movord.codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_valore, ROUND(ROUND(mo_prezzo * (mo_quant - mo_quaeva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)" & _
          " FROM movord INNER JOIN testord ON movord.codditt = testord.codditt AND movord.mo_tipork = testord.td_tipork AND movord.mo_anno = testord.td_anno AND movord.mo_serie = testord.td_serie AND movord.mo_numord = testord.td_numord" & _
          " WHERE td_scorpo = 'N'" & _
          " AND td_tipork NOT IN ('H', 'Q')" & _
          " AND td_valuta = 0" & _
          " AND mo_flevas <> 'S'" & _
          " AND (mo_umprz <> 'S' OR mo_tipork = 'Y' OR mo_unmis = mo_ump)" & _
          " AND mo_pmtaskid = 0" & _
          " AND ABS(mo_valore - ROUND(ROUND(mo_prezzo * (mo_quant - mo_quaeva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)) > 0.02" & _
          " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga", _
          "", "1", "0", "1", "0", "0", _
          "Valore disallineato in: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, valore da evadere |6|, valore ad evadere calcolato |7|.", _
          "950"})

        'Controllo 951: Righe d'Ordine/Impegno con valore da evadere disallineato (in Euro, prezzo UM sec.)
        .Rows.Add(New Object() {"OR", "movord", "Righe d'Ordine/Impegno con valore da evadere disallineato (in Euro, prezzo UM sec.)", _
          "1", "0", _
          "SELECT movord.codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_valore, ROUND(ROUND(mo_prezzo * (mo_colli - mo_coleva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)" & _
          " FROM movord INNER JOIN testord ON movord.codditt = testord.codditt AND movord.mo_tipork = testord.td_tipork AND movord.mo_anno = testord.td_anno AND movord.mo_serie = testord.td_serie AND movord.mo_numord = testord.td_numord" & _
          " WHERE td_scorpo = 'N'" & _
          " AND td_tipork NOT IN ('H', 'Q')" & _
          " AND td_valuta = 0" & _
          " AND mo_flevas <> 'S'" & _
          " AND not (mo_umprz <> 'S' OR mo_tipork = 'Y' OR mo_unmis = mo_ump)" & _
          " AND mo_pmtaskid = 0" & _
          " AND ABS(mo_valore - ROUND(ROUND(mo_prezzo * (mo_colli - mo_coleva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)) > 0.02" & _
          " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga", _
          "", "1", "0", "1", "0", "0", _
          "Valore disallineato in: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, valore da evadere |6|, valore ad evadere calcolato |7|.", _
          "951"})

        'Controllo 952: Righe d'Ordine/Impegno con valore da evadere disallineato (in valuta, prezzo UMP)
        .Rows.Add(New Object() {"OR", "movord", "Righe d'Ordine/Impegno con valore da evadere disallineato (in valuta, prezzo UMP)", _
          "1", "0", _
          "SELECT movord.codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_valorev, ROUND(ROUND(mo_prezvalc * (mo_quant - mo_quaeva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)" & _
          " FROM movord INNER JOIN testord ON movord.codditt = testord.codditt AND movord.mo_tipork = testord.td_tipork AND movord.mo_anno = testord.td_anno AND movord.mo_serie = testord.td_serie AND movord.mo_numord = testord.td_numord" & _
          " WHERE td_scorpo = 'N'" & _
          " AND td_tipork NOT IN ('H', 'Q')" & _
          " AND td_valuta <> 0" & _
          " AND mo_flevas <> 'S'" & _
          " AND (mo_umprz <> 'S' OR mo_tipork = 'Y' OR mo_unmis = mo_ump)" & _
          " AND mo_pmtaskid = 0" & _
          " AND ABS(mo_valorev - ROUND(ROUND(mo_prezvalc * (mo_quant - mo_quaeva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)) > 0.02" & _
          " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga", _
          "", "1", "0", "1", "0", "0", _
          "Valore disallineato in: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, valore da evadere |6|, valore ad evadere calcolato |7|.", _
          "952"})

        'Controllo 953: Righe d'Ordine/Impegno con valore da evadere disallineato (in valuta, prezzo UM sec.)
        .Rows.Add(New Object() {"OR", "movord", "Righe d'Ordine/Impegno con valore da evadere disallineato (in valuta, prezzo UM sec.)", _
          "1", "0", _
          "SELECT movord.codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_valorev, ROUND(ROUND(mo_prezvalc * (mo_colli - mo_coleva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)" & _
          " FROM movord INNER JOIN testord ON movord.codditt = testord.codditt AND movord.mo_tipork = testord.td_tipork AND movord.mo_anno = testord.td_anno AND movord.mo_serie = testord.td_serie AND movord.mo_numord = testord.td_numord" & _
          " WHERE td_scorpo = 'N'" & _
          " AND td_tipork NOT IN ('H', 'Q')" & _
          " AND td_valuta <> 0" & _
          " AND mo_flevas <> 'S'" & _
          " AND NOT (mo_umprz <> 'S' OR mo_tipork = 'Y' OR mo_unmis = mo_ump)" & _
          " AND mo_pmtaskid = 0" & _
          " AND ABS(mo_valorev - ROUND(ROUND(mo_prezvalc * (mo_colli - mo_coleva) / mo_perqta * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100 * (100 - mo_scontp) / 100 - mo_scontv, 2) * (100 - td_scont1) / 100 * (100 - td_scont2) / 100 * (100 - td_scopag) / 100, 2)) > 0.02" & _
          " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga", _
          "", "1", "0", "1", "0", "0", _
          "Valore disallineato in: ditta |0|, tipo |1|, anno |2|, serie |3|, numero |4|, riga |5|, valore da evadere |6|, valore ad evadere calcolato |7|.", _
          "953"})

        .AcceptChanges()

        For i = 0 To .Rows.Count - 1
          .Rows(i)!xx_seleziona = "N"
          .Rows(i)!xx_descrizione = "[" & Format(NTSCInt(.Rows(i)!numcheck), "00000") & "] " & NTSCStr(.Rows(i)!descr)
        Next

        .AcceptChanges()
      End With

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function EseguiControlli(ByVal bckMostraQuery As Boolean) As Boolean
    Dim i As Integer
    Dim bOK As Boolean = False
    Dim evt As NTSEventArgs = Nothing
    Try
      'Controlla se c'è qualche controllo selezionato
      bOK = False
      For i = 0 To dsShared.Tables("TABELLA").Rows.Count - 1
        If NTSCStr(dsShared.Tables("TABELLA").Rows(i)!xx_seleziona) = "S" Then
          bOK = True
          Exit For
        End If
      Next
      If Not bOK Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381460686165898, "Nessun controllo selezionato.")))
        Exit Function
      End If

      'Messaggio di conferma
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128381483542780694, "Procedere con i controlli di coerenza selezionati?"))
      ThrowRemoteEvent(evt)
      If evt.RetValue = "NO" Then Exit Function

      'Per ogni controllo selezionato
      For i = 0 To dsShared.Tables("TABELLA").Rows.Count - 1
        If NTSCStr(dsShared.Tables("TABELLA").Rows(i)!xx_seleziona) = "S" Then
          LanciaControllo(NTSCInt(dsShared.Tables("TABELLA").Rows(i)!numcheck), bckMostraQuery)
        End If
      Next

      Return True

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
  Public Overridable Function LanciaControllo(ByVal nCheck As Integer, ByVal bckMostraQuery As Boolean) As Boolean
    Dim strDescr As String
    Dim strTipoDb As String
    Dim strGrav As String
    Dim strQuery As String
    Dim strQuerySQL As String
    Dim strTipoQuery As String
    Dim strQuando As String
    Dim strLogIf As String
    Dim strValoreTest As String
    Dim strLogMax As String
    Dim strMsg As String
    Dim strEXEC As String
    Dim strErr As String = ""
    Dim lCount As Integer
    Dim lRow As Integer
    Dim bLog As Boolean
    Dim dsQuery As DataSet = Nothing
    Try
      bLog = False

      'Legge il controllo dall'array
      Dim dtrTmp As DataRow() = dsShared.Tables("TABELLA").Select("numcheck = '" & nCheck & "'", Nothing)
      If dtrTmp.Length > 0 Then
        strDescr = NTSCStr(dtrTmp(0)!descr)
        strTipoDb = NTSCStr(dtrTmp(0)!tipodb)
        strGrav = NTSCStr(dtrTmp(0)!gravita)
        strQuery = NTSCStr(dtrTmp(0)!query)
        strQuerySQL = NTSCStr(dtrTmp(0)!querysql)
        strTipoQuery = NTSCStr(dtrTmp(0)!tipoquery)
        strQuando = NTSCStr(dtrTmp(0)!daeseguirequando)
        strLogIf = NTSCStr(dtrTmp(0)!tipolog)
        strValoreTest = NTSCStr(dtrTmp(0)!valoretest)
        strLogMax = NTSCStr(dtrTmp(0)!maxrighelog)
        strMsg = NTSCStr(dtrTmp(0)!msg)
      Else
        LogWrite("Controllo [" & Format(nCheck, "00000") & "] non trovato", False)
        Exit Function
      End If

      'Scrive nel log
      LogWrite("Esecuzione controllo [" & Format(nCheck, "00000") & "]: " & strDescr, False)

      'Non esegue il controllo se riguarda ARCPROC
      If strTipoDb = "2" Then
        LogWrite("Controllo [" & Format(nCheck, "00000") & "] non eseguibile in quanto riguarda l'archivio procedura ARCPROC", False)
        Exit Function
      End If

      'Non esegue se trattasi di VBSCRIPT (non ancora implementato)
      If strTipoQuery <> "1" Then
        LogWrite("Controllo [" & Format(nCheck, "00000") & "] non eseguibile in quanto la tipologia di controllo non abilitato, sono abilitati solo i controlli QUERY (1)", False)
        Exit Function
      End If

      'Determina la query da lanciare
      'SQL/MSDE
      If strQuerySQL <> "" Then strEXEC = strQuerySQL Else strEXEC = strQuery

      'Non esegue se non insicata alcuna query/sp
      If strEXEC = "" Then
        LogWrite("Controllo [" & Format(nCheck, "00000") & "] non eseguibile in quanto manca il comando SQL", False)
        Exit Function
      End If

      'Scrive la query nel log se richiesto
      If bckMostraQuery Then
        LogWrite("Controllo [" & Format(nCheck, "00000") & "]: Query eseguita:" & strEXEC, False)
      End If

      ''Apre il recordset
      oCldCkdb.EseguiQuery(strEXEC, dsQuery, strErr)
      If Len(strErr) <> 0 Then
        'Lo scrive nel log ed esce
        LogWrite("Controllo [" & Format(nCheck, "00000") & "] errore durante l'eseguzione: " & strErr, False)
        Exit Function
      End If

      'Conta le righe
      If dsQuery.Tables("QUERY").Rows.Count = 0 Then
        lRow = 0
      Else
        lRow = dsQuery.Tables("QUERY").Rows.Count
      End If

      'Loppa sulle righe e logga
      lCount = 0
      With dsQuery.Tables("QUERY").Rows
        Select Case strLogIf
          '--------------------------------------------------------------------------------
          Case "0"  'numero di record = a strValoreTest
            If lRow = NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount > NTSCInt(strLogMax) And NTSCInt(strLogMax) > 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "1" 'numero di record <> da strValoreTest
            If lRow <> NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "2" 'numero di record < da strValoreTest
            If lRow < NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "3" 'numero di record > da strValoreTest
            If lRow > NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "4" 'numero di record <= da strValoreTest
            If lRow <= NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "5" 'numero di record >= da strValoreTest
            If lRow >= NTSCInt(strValoreTest) Then
              'situazione da loggare
              If lRow = 0 Then
                LogWrite(strMsg, False)
                bLog = True
              Else
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "6" '1 campo = a strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) = NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "7" '1 campo <> da strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) <> NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "8" '1 campo < da strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) < NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "9" '1 campo > da strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) > NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "10" '1 campo <= da strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) <= NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            '--------------------------------------------------------------------------------
          Case "11" '1 campo => da strValoreTest
            If lRow > 0 Then
              If NTSCInt(dsQuery.Tables("QUERY").Rows(0)(0).ToString()) >= NTSCInt(strValoreTest) Then
                'situazione da loggare
                Do While Not lCount > .Count - 1
                  lCount = lCount + 1
                  If lCount <= NTSCInt(strLogMax) Or NTSCInt(strLogMax) = 0 Then
                    LogWrite(ResolveMsg(strMsg, dsQuery.Tables("QUERY").Rows(lCount - 1)), False)
                    bLog = True
                  Else
                    Exit Do
                  End If
                Loop
              End If
            End If
            'non gistito
          Case Else
            LogWrite("Controllo [" & Format(nCheck, "00000") & "] tipo di controllo " & strLogIf & " non gestito", False)
            bLog = True
        End Select
      End With

      If Not bLog Then
        LogWrite("Esecuzione controllo [" & Format(nCheck, "00000") & "] terminata con successo.", False)
      Else
        LogWrite("Esecuzione controllo [" & Format(nCheck, "00000") & "] terminata con i " & lCount & " messaggi sopra indicati.", False)
      End If
      LogWrite("--------------------------------------------------------------------", False)

      Return True

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

  Public Overridable Function ResolveMsg(ByVal strMsgIn As String, ByVal dtr As DataRow) As String
    Dim strTmp As String
    Dim i As Integer
    Dim strValue As String
    Try
      ResolveMsg = strMsgIn
      strTmp = strMsgIn

      'Effettua la sostituzione (assume che il recordset posizione su un record corrente)
      For i = 0 To dtr.Table.Columns.Count - 1
        strValue = NTSCStr(dtr(i))
        strTmp = strTmp.Replace("|" & i & "|", strValue)
      Next

      Return strTmp

    Catch ex As Exception
      Return strMsgIn
    End Try
  End Function


  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try

      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128381234472230345, strErr))
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub




End Class
