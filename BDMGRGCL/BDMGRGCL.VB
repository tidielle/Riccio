#Region "Imports"
Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
#End Region

Public Class CLDMGRGCL
  Inherits CLD__BASE

  Public Overridable Function CheckReports(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
                                          ByVal nMagaz As Integer, ByVal nProgressivo As Integer, _
                                          ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                                          ByVal bDefinitiva As Boolean, ByVal lIITTStrcl As Integer, _
                                          ByVal strTTStrcl As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT1() As DataRow = Nothing
    Try
      '------------------------------------------------------------------------------
      ' Riempie la tabella temporanea con i filtri imputati
      '------------------------------------------------------------------------------
      strSQL = "INSERT INTO " & strTTStrcl & " (codditt, instid, kk_tipork, kk_anno," & _
        " kk_serie, kk_numdoc, kk_riga, kk_kmmagaz, kk_kmcausale, kk_codart, kk_unmis," & _
        " kk_descr, kk_colli, kk_quant, kk_desint, kk_ump, kk_note, kk_prtipo, kk_pranno," & _
        " kk_prserie, kk_prnum, kk_prriga, kk_cltipo, kk_clanno, kk_clserie, kk_clnum," & _
        " kk_clriga, kk_kmaammgg, kk_tmriferim, kk_descaum, kk_destpbf, kk_codartcar," & _
        " kk_desartcar, kk_qtaartcar, kk_contosaldo)"
      strSQL += " SELECT " & CStrSQL(strDitta) & ", " & lIITTStrcl & ", movmag.mm_tipork, movmag.mm_anno," & _
        " movmag.mm_serie, movmag.mm_numdoc, movmag.mm_riga, km_magaz, km_causale," & _
        " movmag.mm_codart, movmag.mm_unmis, artico.ar_descr, movmag.mm_colli," & _
        " (movmag.mm_quant * keymag.km_carscar), movmag.mm_descr, movmag.mm_ump, movmag.mm_note," & _
        " movmag.mm_prtipo, movmag.mm_pranno, movmag.mm_prserie, movmag.mm_prnum," & _
        " movmag.mm_prriga, movmag.mm_cltipo, movmag.mm_clanno, movmag.mm_clserie," & _
        " movmag.mm_clnum, movmag.mm_clriga, keymag.km_aammgg, testmag.tm_riferim," & _
        " tabcaum.tb_descaum, tabtpbf.tb_destpbf, movmag_1.mm_codart, movmag_1.mm_descr," & _
        " movmag_1.mm_quant, 'C'"
      strSQL += " FROM ((((movmag INNER JOIN ((keymag INNER JOIN artico ON keymag.codditt = artico.codditt AND keymag.km_codart = artico.ar_codart)" & _
        " INNER JOIN tabmaga ON keymag.codditt = tabmaga.codditt AND keymag.km_magaz = tabmaga.tb_codmaga) ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork))" & _
        " INNER JOIN testmag ON (movmag.codditt = testmag.codditt) AND (movmag.mm_numdoc = testmag.tm_numdoc) AND (movmag.mm_serie = testmag.tm_serie) AND (movmag.mm_anno = testmag.tm_anno) AND (movmag.mm_tipork = testmag.tm_tipork))" & _
        " INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf)" & _
        " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum)" & _
        " LEFT JOIN movmag AS movmag_1 ON (movmag.codditt = movmag_1.codditt) AND (movmag.mm_prriga = movmag_1.mm_riga) AND (movmag.mm_prnum = movmag_1.mm_numdoc) AND (movmag.mm_prserie = movmag_1.mm_serie) AND (movmag.mm_pranno = movmag_1.mm_anno) AND (movmag.mm_prtipo = movmag_1.mm_tipork)"
      strSQL += " WHERE keymag.codditt = " & CStrSQL(strDitta) & " AND keymag.km_magaz = " & nMagaz
      If bDefinitiva = True Then
        strSQL += " AND keymag.km_aammgg BETWEEN " & CDataSQL(DateAdd("d", 1, dateDatini)) & " AND " & CDataSQL(dateDatfin)
      Else
        strSQL += " AND keymag.km_aammgg BETWEEN " & CDataSQL(dateDatini) & " AND " & CDataSQL(dateDatfin)
      End If
      strSQL += " AND movmag.mm_clnum <> 0" & _
        " ORDER BY movmag.mm_anno, movmag.mm_serie, testmag.tm_datdoc, movmag.mm_numdoc, movmag.mm_riga, (movmag.mm_quant * keymag.km_carscar) DESC"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '------------------------------------------------------------------------------
      'Apre un dynaset sul temporaneo,
      'se non esistono record lo chiude e non fa partire neanche il report
      '------------------------------------------------------------------------------
      strSQL = "SELECT * FROM " & strTTStrcl & _
        " WHERE codditt = " & CStrSQL(strDitta) & " AND instid = " & lIITTStrcl & _
        " ORDER BY kk_kmmagaz, kk_kmaammgg, kk_tipork, kk_anno, kk_serie, kk_numdoc, kk_riga"
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabella, ds)
      If ds.Tables(strTabella).Rows.Count = 0 Then Return False

      '------------------------------------------------------------------------------
      'Verifica di eventuali rettitifiche sulla tabella temporanea
      'ottengo i record dei documenti con c/to lavoro chiuso
      'NB: in questo caso gli carichi di materiale (i consumi) NON DEVONO superare la quantità caricata!!!!
      '------------------------------------------------------------------------------
      strSQL = "SELECT mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga, " & _
        " Sum(movmag.mm_quant * keymag.km_carscar) as xx_saldo" & _
        " FROM movmag INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_numdoc = keymag.km_numdoc)" & _
        " AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & " AND keymag.km_magaz = " & nMagaz & _
        " AND keymag.km_aammgg <= " & CDataSQL(dateDatfin) & _
        " AND movmag.mm_clnum <> 0" & _
        " GROUP BY mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga" & _
        " HAVING Abs(Sum(movmag.mm_quant * keymag.km_carscar)) < 0.001" & _
        " ORDER BY mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return True


      'ora devo aggioranre il flag 'kk_contosaldo' con 'S' = a saldo
      For Each dtrT As DataRow In dttTmp.Rows
        dtrT1 = ds.Tables(strTabella).Select("kk_cltipo = " & CStrSQL(dtrT!mm_cltipo) & _
                                             " AND kk_clanno = " & dtrT!mm_clanno.ToString & _
                                             " AND kk_clserie = " & CStrSQL(dtrT!mm_clserie) & _
                                             " AND kk_clnum = " & dtrT!mm_clnum.ToString & _
                                             " AND kk_clriga = " & dtrT!mm_clriga.ToString & _
                                             " AND kk_quant < 0", _
                                             "kk_tipork DESC, kk_anno DESC, kk_serie DESC, kk_numdoc DESC, kk_riga DESC")
        If dtrT1.Length > 0 Then
          'metto il flag
          dtrT1(0)!kk_contosaldo = "S"
          dtrT1(0).AcceptChanges()
        End If
      Next    'For Each dtrT As DataRow In dttTmp.Rows
      dttTmp.Clear()

      ScriviTabellaTemporanea(strDitta, strTabella, ds.Tables(strTabella), lIITTStrcl)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function LeggiMagazzino(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dsOut As System.Data.DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 tb_codmaga FROM " & strNomeTabella & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_flclavo <> ' '" & _
               " ORDER BY tb_codmaga"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SvuotaTabellaDaDataset(ByRef dsDataset As DataSet, ByVal strTabella As String) As Boolean
    Try
      If Not dsDataset Is Nothing Then
        If Not dsDataset.Tables(strTabella) Is Nothing Then
          dsDataset.Tables.Remove(strTabella)
          dsDataset.AcceptChanges()
        End If
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "Errore durante la rimozione della tabella " & strTabella & " nel dataset.", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabellaTemporanea(ByVal strDitta As String, ByVal strTabella As String, ByRef dttTable As System.Data.DataTable, ByVal lIITTStrcl As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      For Each dtrT As DataRow In dttTable.Select("kk_contosaldo = 'S'")
        strSQL = "UPDATE ttstrcl SET kk_contosaldo = 'S'" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTStrcl & _
                 " AND kk_tipork = " & CStrSQL(dtrT!kk_tipork) & _
                 " AND kk_anno = " & dtrT!kk_anno.ToString & _
                 " AND kk_serie = " & CStrSQL(dtrT!kk_serie) & _
                 " AND kk_numdoc = " & dtrT!kk_numdoc.ToString & _
                 " AND kk_riga = " & dtrT!kk_riga.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaDataProgr(ByVal strDitta As String, ByVal strNomeTabella As String, ByVal nMagaz As Integer, ByVal nProgressivo As Integer, _
                                           ByVal dateDatfin As Date, ByVal lRecordCount As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE " & strNomeTabella & _
               " SET tb_datacs = " & CDataSQL(dateDatfin) & "," & _
               " tb_progr = " & (nProgressivo + lRecordCount) & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_codmaga = " & nMagaz
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
