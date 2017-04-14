Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGARTV
  Inherits CLD__BASE

  Public Overridable Function GetDataApri(ByVal strDitta As String, ByVal strCodArt As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""
    Try
      strSQL = "SELECT artico.*, tabmarc.tb_desmarc as xx_codmarc, tabpdon.tb_despdon as xx_codpdon," & _
        " tabappr.tb_desappr as xx_codappr, tabcena.tb_descena as xx_numecr," & _
        " anagra.an_descr1 as xx_forn, anagra2.an_descr1 as xx_forn2," & _
        " tabciva.tb_desciva as xx_codiva, tabgmer.tb_desgmer as xx_gruppo," & _
        " tabsgme.tb_dessgme as xx_sotgru, tabcove.tb_descove as xx_controp," & _
        " tabcove2.tb_descove as xx_controa, tabcove3.tb_descove as xx_contros," & _
        " tabcfam.tb_descfam as xx_famprod, tabcvuo.tb_descvuo as xx_codvuo," & _
        " tabmaga.tb_desmaga as xx_magstock, tabmaga2.tb_desmaga as xx_magprod," & _
        " tabimba.tb_desimba as xx_codimba, artico2.ar_descr as xx_cartric," & _
        " artico3.ar_descr as xx_cartcanas, tabvari.tb_desvari as xx_flmod," & _
        " tabcsar.tb_descsar as xx_clascon, tabcpar.tb_descpar as xx_claprov," & _
        " tabtcdc.tb_destcdc AS xx_codtcdc, tabdica.tb_desdica AS xx_coddica, " & _
        " tabdicv.tb_desdicv AS xx_coddicv, tablotx.tb_deslotx AS xx_codtlox, tabrear.tb_desrear AS xx_reparto," & _
        " tabseat.tb_desseat as xx_codseat" & _
        " FROM ((((((((((((((((((((((artico" & _
        " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc)" & _
        " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon)" & _
        " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr)" & _
        " LEFT JOIN tabcena ON artico.codditt = tabcena.codditt AND artico.ar_numecr = tabcena.tb_codcena)" & _
        " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto)" & _
        " LEFT JOIN anagra as anagra2 ON artico.codditt = anagra2.codditt AND artico.ar_forn2 = anagra2.an_conto)" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva)" & _
        " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer)" & _
        " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme)" & _
        " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controp = tabcove.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove2 ON artico.codditt = tabcove2.codditt AND artico.ar_controa = tabcove2.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove3 ON artico.codditt = tabcove3.codditt AND artico.ar_contros = tabcove3.tb_codcove)" & _
        " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam)" & _
        " LEFT JOIN tabcvuo ON artico.codditt = tabcvuo.codditt AND artico.ar_codvuo = tabcvuo.tb_codcvuo)" & _
        " LEFT JOIN tabmaga ON artico.codditt = tabmaga.codditt AND artico.ar_magstock = tabmaga.tb_codmaga)" & _
        " LEFT JOIN tabmaga as tabmaga2 ON artico.codditt = tabmaga2.codditt AND artico.ar_magprod = tabmaga2.tb_codmaga)" & _
        " LEFT JOIN tabimba ON artico.codditt = tabimba.codditt AND artico.ar_codimba = tabimba.tb_codimba)" & _
        " LEFT JOIN artico as artico2 ON artico.codditt = artico2.codditt AND artico.ar_cartric = artico2.ar_codart)" & _
        " LEFT JOIN artico as artico3 ON artico.codditt = artico3.codditt AND artico.ar_cartcanas = artico3.ar_codart)" & _
        " LEFT JOIN tabvari ON artico.codditt = tabvari.codditt AND artico.ar_flmod = tabvari.tb_codvari)" & _
        " LEFT JOIN tabcsar ON artico.codditt = tabcsar.codditt AND artico.ar_clascon = tabcsar.tb_codcsar)" & _
        " LEFT JOIN tabcpar ON artico.codditt = tabcpar.codditt AND artico.ar_claprov = tabcpar.tb_codcpar)" & _
        " LEFT JOIN tablotx ON artico.codditt = tablotx.codditt AND artico.ar_codtlox = tablotx.tb_codlotx" & _
        " LEFT JOIN tabtcdc ON artico.ar_codtcdc = tabtcdc.tb_codtcdc" & _
        " LEFT JOIN tabdica ON artico.ar_coddica = tabdica.tb_coddica" & _
        " LEFT JOIN tabdicv ON artico.codditt = tabdicv.codditt AND artico.ar_coddica = tabdicv.tb_coddica AND artico.ar_coddicv = tabdicv.tb_coddicv " & _
        " LEFT JOIN tabrear ON artico.codditt = tabrear.codditt AND artico.ar_reparto = tabrear.tb_codrear " & _
        " LEFT JOIN tabseat ON artico.codditt = tabseat.codditt AND artico.ar_codseat = tabseat.tb_codseat "


      strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codart = " & CStrSQL(strCodArt) & _
        " ORDER BY artico.codditt, artico.ar_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataNuovo(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artico.*, tabmarc.tb_desmarc as xx_codmarc, tabpdon.tb_despdon as xx_codpdon," & _
        " tabappr.tb_desappr as xx_codappr, tabcena.tb_descena as xx_numecr," & _
        " anagra.an_descr1 as xx_forn, anagra2.an_descr1 as xx_forn2," & _
        " tabciva.tb_desciva as xx_codiva, tabgmer.tb_desgmer as xx_gruppo," & _
        " tabsgme.tb_dessgme as xx_sotgru, tabcove.tb_descove as xx_controp," & _
        " tabcove2.tb_descove as xx_controa, tabcove3.tb_descove as xx_contros," & _
        " tabcfam.tb_descfam as xx_famprod, tabcvuo.tb_descvuo as xx_codvuo," & _
        " tabmaga.tb_desmaga as xx_magstock, tabmaga2.tb_desmaga as xx_magprod," & _
        " tabimba.tb_desimba as xx_codimba, artico2.ar_descr as xx_cartric," & _
        " artico3.ar_descr as xx_cartcanas, tabvari.tb_desvari as xx_flmod," & _
        " tabcsar.tb_descsar as xx_clascon, tabcpar.tb_descpar as xx_claprov," & _
        " tabtcdc.tb_destcdc AS xx_codtcdc, tabdica.tb_desdica AS xx_coddica, tabdicv.tb_desdicv AS xx_coddicv, " & _
        " tablotx.tb_deslotx AS xx_codtlox, tabrear.tb_desrear AS xx_reparto," & _
        " tabseat.tb_desseat as xx_codseat" & _
        " FROM ((((((((((((((((((((((artico" & _
        " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc)" & _
        " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon)" & _
        " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr)" & _
        " LEFT JOIN tabcena ON artico.codditt = tabcena.codditt AND artico.ar_numecr = tabcena.tb_codcena)" & _
        " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto)" & _
        " LEFT JOIN anagra as anagra2 ON artico.codditt = anagra2.codditt AND artico.ar_forn2 = anagra2.an_conto)" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva)" & _
        " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer)" & _
        " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme)" & _
        " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controp = tabcove.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove2 ON artico.codditt = tabcove2.codditt AND artico.ar_controa = tabcove2.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove3 ON artico.codditt = tabcove3.codditt AND artico.ar_contros = tabcove3.tb_codcove)" & _
        " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam)" & _
        " LEFT JOIN tabcvuo ON artico.codditt = tabcvuo.codditt AND artico.ar_codvuo = tabcvuo.tb_codcvuo)" & _
        " LEFT JOIN tabmaga ON artico.codditt = tabmaga.codditt AND artico.ar_magstock = tabmaga.tb_codmaga)" & _
        " LEFT JOIN tabmaga as tabmaga2 ON artico.codditt = tabmaga2.codditt AND artico.ar_magprod = tabmaga2.tb_codmaga)" & _
        " LEFT JOIN tabimba ON artico.codditt = tabimba.codditt AND artico.ar_codimba = tabimba.tb_codimba)" & _
        " LEFT JOIN artico as artico2 ON artico.codditt = artico2.codditt AND artico.ar_cartric = artico2.ar_codart)" & _
        " LEFT JOIN artico as artico3 ON artico.codditt = artico3.codditt AND artico.ar_cartcanas = artico3.ar_codart)" & _
        " LEFT JOIN tabvari ON artico.codditt = tabvari.codditt AND artico.ar_flmod = tabvari.tb_codvari)" & _
        " LEFT JOIN tabcsar ON artico.codditt = tabcsar.codditt AND artico.ar_clascon = tabcsar.tb_codcsar)" & _
        " LEFT JOIN tabcpar ON artico.codditt = tabcpar.codditt AND artico.ar_claprov = tabcpar.tb_codcpar" & _
        " LEFT JOIN tabtcdc ON artico.ar_codtcdc = tabtcdc.tb_codtcdc" & _
        " LEFT JOIN tabdica ON artico.ar_coddica = tabdica.tb_coddica" & _
        " LEFT JOIN tabdicv ON artico.codditt = tabdicv.codditt AND artico.ar_coddica = tabdicv.tb_coddica AND artico.ar_coddicv = tabdicv.tb_coddicv)" & _
        " LEFT JOIN tablotx ON artico.codditt = tablotx.codditt AND artico.ar_codtlox = tablotx.tb_codlotx " & _
        " LEFT JOIN tabrear ON artico.codditt = tabrear.codditt AND artico.ar_reparto = tabrear.tb_codrear " & _
        " LEFT JOIN tabseat ON artico.codditt = tabseat.codditt AND artico.ar_codseat = tabseat.tb_codseat "

      strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codart = 'zzzzzzzzzzzzzzzzzz'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetTabNuma(ByVal strDitta As String, ByVal strCodart As String, _
                                         ByVal nLungRoot As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_numprog FROM tabnuma" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo = 'AA'" & _
        " AND tb_numserie = '" & Left(strCodart, nLungRoot) & "'" & _
        " AND tb_numcodl = 0" & _
        " ORDER BY codditt, tb_numtipo, tb_numserie, tb_numcodl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtico(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, ar_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoRoot(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artico.* FROM artico" & _
      " WHERE codditt = " & CStrSQL(strDitta) & _
      " AND ar_codroot = " & CStrSQL(strCodart)
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ControllaArticolo(ByVal strDitta As String, ByVal strCodart As String, _
                                  ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT db_coddb FROM distbas" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND db_coddb = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, db_coddb"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DISTBAS")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lConto As Integer, _
                                                   ByVal dbConn As DbConnection) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim strSQL As String = ""
    Try
      'Aggiorna il timestamp di anagra (an_ultagg) con la data di sistema
      'Opera su gdb o gcn a seconda della connessione
      strSQL = "UPDATE anagra SET an_ultagg = GETDATE() " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function VarGetDataNuovo(ByVal strDitta As String, ByVal strCodvari As String, _
                                              ByVal strLivello As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Try
      strSQL = " SELECT " & CStrSQL(strDitta) & " as xx_codditt" & strLivello & "," & _
        " vv_valvari as xx_codvar" & strLivello & "," & _
        " LEFT(vv_desvvar, 40) as xx_descr" & strLivello & _
        " FROM valvari" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND vv_codvari = " & CStrSQL(strCodvari)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR")

      'carico la colonna di selezione
      If strLivello = "1" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona1", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona1 = "N"
        Next
      ElseIf strLivello = "2" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona2", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona2 = "N"
        Next
      ElseIf strLivello = "3" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona3", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona3 = "N"
        Next
      End If

      ds.Tables("ARTVAR").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VarGetDataApri(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                        ByVal strLivello As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Try
      strSQL = "SELECT codditt as xx_codditt" & strLivello & "," & _
        " arv_codvar as xx_codvar" & strLivello & ", arv_descr as xx_descr" & strLivello & _
        " FROM artvar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND arv_livello = " & strLivello

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR")

      'carico la colonna di selezione
      If strLivello = "1" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona1", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona1 = "N"
        Next
      ElseIf strLivello = "2" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona2", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona2 = "N"
        Next
      ElseIf strLivello = "3" Then
        ds.Tables("ARTVAR").Columns.Add(New DataColumn("xx_seleziona3", GetType(String)))

        For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
          ds.Tables("ARTVAR").Rows(i)!xx_seleziona3 = "N"
        Next
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ArticoGetDataApri(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                                ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT artico.*" & _
        " FROM artico" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codroot = " & CStrSQL(strArtvCodroot)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArtpro(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ap_codart FROM artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, ap_codart, ap_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArtprox(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT apx_codart FROM artprox" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apx_codart = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, apx_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetValvari(ByVal strDitta As String, ByVal strCodvari As String, _
                                         ByVal strValvari As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT LEFT(vv_desvvar, 40) AS vv_desvvar FROM valvari" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND vv_codvari = " & CStrSQL(strCodvari) & _
        " AND vv_valvari = " & CStrSQL(strValvari) & _
        " ORDER BY codditt, vv_codvari, vv_valvari"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "VALVARI")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ArticoGetDataNuovo(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                                 ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT artico.*" & _
        " FROM artico" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codroot = " & CStrSQL(strArtvCodroot)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VarSalva(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                      ByVal strLivello As String, ByVal nNumliv As Integer, _
                                      ByVal strPrevar As String, ByVal nArtvLungroot As Integer, _
                                      ByVal nLungvar1 As Integer, ByVal nLungvar2 As Integer, _
                                      ByVal nLungvar3 As Integer, ByVal strAr_descr As String, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim dtrChange() As DataRow = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim strCodart As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim j As Integer
    Try
      'FUNZIONE PER IL SALVATAGGIO/CANCELLAZIONE DELLE VARIANTI COLLEGATE ALLA GRIGLIA ELENCO VARIANTI

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'prima se non presente creo un record di artroot
      strSQL = "SELECT arr_codroot FROM artroot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arr_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTROOT", , dbConn)

      If dsTmp.Tables("ARTROOT").Rows.Count = 0 Then
        strSQL = "INSERT INTO artroot (codditt, arr_codroot, arr_descr, arr_numliv," & _
          " arr_lungroot, arr_lungvar1, arr_lungvar2, arr_lungvar3, arr_prevar1)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strArtvCodroot) & ", " & _
          CStrSQL(strAr_descr) & ", " & nNumliv & ", " & nArtvLungroot & ", " & _
          nLungvar1 & ", " & nLungvar2 & ", " & nLungvar3 & ", '" & strPrevar & "')"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'prima le delete
      dtrChange = ds.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        'ricerca gli articoli collegati alla variante da cancellare
        strSQL = "SELECT ar_codvar1, ar_codvar2, ar_codvar3 FROM artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codvar" & strLivello & " = " & CStrSQL(dtrChange(i)("xx_codvar" & strLivello, DataRowVersion.Original)) & _
        " AND ar_codroot = " & CStrSQL(strArtvCodroot)

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)

        'elimina le tabelle collegate agli articoli 
        For j = 0 To dsTmp.Tables("ARTICO").Rows.Count - 1

          Select Case nNumliv
            Case 1 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar1))
            Case 2 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar1)) & _
                                 Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar2))
            Case 3 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar1)) & _
                                 Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar2)) & _
                                 Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar3))
          End Select

          strSQL = "DELETE FROM barcode" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND bc_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM codarfo" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND caf_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM artkit" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ak_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM artval" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ax_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '--- Se prezzi diversi su 3 varianti cancella LISTINI, SCONTI, PROVVIGIONI
          If strPrevar = "S" Then
            strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar1)) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar2)) & Trim(NTSCStr(dsTmp.Tables("ARTICO").Rows(j)!ar_codvar3))
            strSQL = "DELETE FROM listini" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lc_codart = " & CStrSQL(strCodart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM sconti" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND so_codart = " & CStrSQL(strCodart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM perprov" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND per_codart = " & CStrSQL(strCodart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        Next

        If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
          strSQL = "DELETE FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codroot = " & CStrSQL(strArtvCodroot) & _
            " AND ar_codvar" & strLivello & " = " & CStrSQL(dtrChange(i)("xx_codvar" & strLivello, DataRowVersion.Original))
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strSQL = "DELETE FROM artvar" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND arv_codroot = " & CStrSQL(strArtvCodroot) & _
          " AND arv_livello = " & strLivello & _
          " AND arv_codvar = " & CStrSQL(dtrChange(i)("xx_codvar" & strLivello, DataRowVersion.Original))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      'poi insert
      dtrChange = ds.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrChange.Length - 1
        strSQL = "INSERT INTO artvar (codditt, arv_codroot, arv_livello, arv_codvar, arv_descr)" & _
          " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(strArtvCodroot) & ", " & strLivello & ", " & _
          CStrSQL(dtrChange(i)("xx_codvar" & strLivello)) & ", " & _
          CStrSQL(dtrChange(i)("xx_descr" & strLivello)) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      'poi update
      dtrChange = ds.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE artvar SET arv_descr = " & CStrSQL(dtrChange(i)("xx_descr" & strLivello)) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND arv_codroot = " & CStrSQL(strArtvCodroot) & _
          " AND arv_livello = " & strLivello & _
          " AND arv_codvar = " & CStrSQL(dtrChange(i)("xx_codvar" & strLivello))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VarGetartdefx(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                            ByVal strLivello As String, ByVal dtrTmp As DataRow, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 ar_codart FROM ARTICO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codvar" & strLivello & " = " & CStrSQL(dtrTmp("xx_codvar" & strLivello)) & _
        " AND ar_codart IN (SELECT adx_codart FROM artico INNER JOIN artdefx ON artico.codditt = artdefx.codditt AND artico.ar_codart = artdefx.adx_codart" & _
                          " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                          " AND artico.ar_codroot = " & CStrSQL(strArtvCodroot) & ")"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ArticoGetartdefx(ByVal strDitta As String, ByVal strCodart As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT adx_codart FROM artdefx" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND adx_codart = " & CStrSQL(strCodart)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ArticoSalva(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                    ByVal nNumliv As Integer, ByVal strPrevar As String, _
                                    ByRef strError As String, ByVal nArtvLungroot As Integer, _
                                    ByVal nLungvar1 As Integer, ByVal nLungvar2 As Integer, _
                                    ByVal nLungvar3 As Integer, ByVal strAr_descr As String, _
                                    ByRef ds As DataSet, ByRef dsShared As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim dtrChange() As DataRow = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim strCodart As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      'FUNZIONE PER IL SALVATAGGIO/CANCELLAZIONE DEGLI ARTICOLI CON VARIANTI COLLEGATI ALLA GRIGLIA ANALITICO VARIANTI

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'prima se non presente creo un record di artroot
      strSQL = "SELECT arr_codroot FROM artroot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arr_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTROOT", , dbConn)

      If dsTmp.Tables("ARTROOT").Rows.Count = 0 Then
        strSQL = "INSERT INTO artroot (codditt, arr_codroot, arr_descr, arr_numliv," & _
          " arr_lungroot, arr_lungvar1, arr_lungvar2, arr_lungvar3, arr_prevar1)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strArtvCodroot) & ", " & _
          CStrSQL(strAr_descr) & ", " & nNumliv & ", " & nArtvLungroot & ", " & _
          nLungvar1 & ", " & nLungvar2 & ", " & nLungvar3 & ", '" & strPrevar & "')"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'prima le delete
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        Select Case nNumliv
          Case 1 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)("ar_codvar1", DataRowVersion.Original)))
          Case 2 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)("ar_codvar1", DataRowVersion.Original))) & _
                               Trim(NTSCStr(dtrChange(i)("ar_codvar2", DataRowVersion.Original)))
          Case 3 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)("ar_codvar1", DataRowVersion.Original))) & _
                               Trim(NTSCStr(dtrChange(i)("ar_codvar2", DataRowVersion.Original))) & _
                               Trim(NTSCStr(dtrChange(i)("ar_codvar3", DataRowVersion.Original)))
        End Select

        If Not CancellaListini(strDitta, strCodart, strPrevar, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaArticoli(strDitta, strCodart, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaCodarfo(strDitta, strCodart, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaArtmaga(strDitta, strCodart, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaKit(strDitta, strCodart, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      Next
      'poi insert
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrChange.Length - 1

        Select Case nNumliv
          Case 1 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1))
          Case 2 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar2))
          Case 3 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar2)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar3))
        End Select

        'inserisce il artico.ar_codart = codice root + varianti
        dtrChange(i)!ar_codroot = strArtvCodroot
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
        " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), dtrChange(i), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        AggiornaArticoliDaArticoloRoot(NTSCStr(dtrChange(i)!ar_codart), dsShared, dbConn)
      Next
      'poi update
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        Select Case nNumliv
          Case 1 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1))
          Case 2 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar2))
          Case 3 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrChange(i)!ar_codvar1)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar2)) & _
                               Trim(NTSCStr(dtrChange(i)!ar_codvar3))
        End Select

        'aggiorna il artico.ar_codart = codice root + varianti
        dtrChange(i)!ar_codroot = strArtvCodroot
        strSQL = "UPDATE artico SET " & GetQueryUpdate(ds.Tables("ARTICO"), dtrChange(i), "ar_") & _
          " WHERE codditt = " & CStrSQL(dtrChange(i)!codditt.ToString) & _
          " AND ar_codart = " & CStrSQL(strCodart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        AggiornaArticoliDaArticoloRoot(NTSCStr(dtrChange(i)!ar_codart), dsShared, dbConn)
      Next

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function Salva(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                  ByVal nNumliv As Integer, ByVal strPrevar As String, _
                                  ByRef strError As String, ByRef ds As DataSet, _
                                  ByRef dsArtico As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim dtrChange() As DataRow = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim strCodart As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim j As Integer
    Dim dttFieldAlias As New DataTable
    Try
      'FUNZIONE PER IL SALVATAGGIO/CANCELLAZIONE DI TUTTI GLI ARTICOLI ROOT + VARIANTI

      GetColonneSpecificheArticolo(dttFieldAlias)

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'prima le delete
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        If Not CancellaAllListini(strDitta, strArtvCodroot, strPrevar, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllCodarfo(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllArtmaga(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllKit(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllArticoli(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllArtvar(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not CancellaAllArtroot(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      Next
      'poi insert
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrChange.Length - 1
        'controlla e inserisce il artico.ar_codart = codice root
        strSQL = "SELECT artico.ar_codart FROM artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strArtvCodroot)
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)

        If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
          strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), dtrChange(i), "ar_", "")
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          'aggiorna il artico.ar_codart = codice root
          strSQL = "UPDATE artico SET " & GetQueryUpdate(ds.Tables("ARTICO"), dtrChange(i), "ar_") & _
            " WHERE codditt = " & CStrSQL(dtrChange(i)!codditt.ToString) & _
            " AND ar_codart = " & CStrSQL(strArtvCodroot)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        'aggiorna i listini provvigioni sconti
        If Not AggiornaListini(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not AggiornaProvvigioni(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
        If Not AggiornaSconti(strDitta, strArtvCodroot, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If

        'aggiorna il artico.ar_codart = codice root + varianti
        For j = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
          'I campi presenti nella griglia analitico varianti variano a seconda del articolo root + varianti
          'I campi non presenti nella griglia analitico varianti sono identici per articolo root e root + varianti

          'l'update per i campi di root + varianti
          strSQL = "UPDATE artico SET " & GetQueryUpdate(dsArtico.Tables("ARTICO"), dsArtico.Tables("ARTICO").Rows(j), "ar_") & _
            " WHERE codditt = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!codditt.ToString) & _
            " AND ar_codart = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'l'update per i campi non in comune tra root e root + varianti
          strSQL = "UPDATE artico SET " & GetQueryUpdate(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "", "", dttFieldAlias) & _
            " WHERE codditt = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!codditt.ToString) & _
            " AND ar_codart = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        'aggiorno i campi fissi del root
        strSQL = "UPDATE artico SET ar_tipoopz = 'G'" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strArtvCodroot)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      'poi update
      dtrChange = ds.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        'aggiorna artroot
        strSQL = "UPDATE artroot SET arr_descr = " & CStrSQL(dtrChange(i)!ar_descr) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND arr_codroot = " & CStrSQL(strArtvCodroot)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'aggiorna il artico.ar_codart = codice root
        strSQL = "UPDATE artico SET " & GetQueryUpdate(ds.Tables("ARTICO"), dtrChange(i), "ar_") & _
          " WHERE codditt = " & CStrSQL(dtrChange(i)!codditt.ToString) & _
          " AND ar_codart = " & CStrSQL(strArtvCodroot)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'aggiorna il artico.ar_codart = codice root + varianti
        For j = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
          'I campi presenti nella griglia analitico varianti variano a seconda del articolo root + varianti
          'I campi non presenti nella griglia analitico varianti sono identici per articolo root e root + varianti

          'l'update per i campi di root + varianti
          strSQL = "UPDATE artico SET " & GetQueryUpdate(dsArtico.Tables("ARTICO"), dsArtico.Tables("ARTICO").Rows(j), "ar_") & _
            " WHERE codditt = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!codditt.ToString) & _
            " AND ar_codart = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'l'update per i campi non in comune tra root e root + varianti
          strSQL = "UPDATE artico SET " & GetQueryUpdate(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "", "", dttFieldAlias) & _
            " WHERE codditt = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!codditt.ToString) & _
            " AND ar_codart = " & CStrSQL(dsArtico.Tables("ARTICO").Rows(j)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        'aggiorno i campi fissi del root
        strSQL = "UPDATE artico SET ar_tipoopz = 'G'" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strArtvCodroot)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AggiornaListini(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu, lc_codtpro," & _
        " lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant, lc_aquant, lc_perqta, lc_unmis," & _
        " lc_note, lc_netto, lc_fase, lc_ultagg, lc_codcas, lc_coddest)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", lc_codlavo, lc_conto, lc_codvalu, lc_codtpro," & _
        " lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant, lc_aquant, lc_perqta, lc_unmis," & _
        " lc_note, lc_netto, lc_fase, " & CDataOraSQL(Now) & ", ' ', lc_coddest" & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodartOld) & _
        " AND lc_codcas = ' '" & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_listino, lc_codvalu, lc_codtpro, lc_datagg DESC," & _
        " lc_daquant, lc_fase"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT lc_conto " & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodartOld) & _
        " AND lc_codcas = ' '" & _
        " AND lc_conto > 0 " & _
        " ORDER BY lc_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsTmp.Tables("LISTINI").Rows.Count - 1)
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto))
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dsTmp.Clear()
      dsTmp.Dispose()
    End Try
  End Function

  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiorna il timestamp di anagra (an_ultagg) con la data di sistema
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anagra" & _
        " SET an_ultagg = GETDATE()" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  'funzioni per la cancellazione singolo articolo root + varianti
  Public Overridable Function CancellaListini(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByVal strPrevar As String, ByRef strError As String, _
                                            ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      If strPrevar = "S" Then
        '-------------------------------------------------------------------------------------
        '--- Per i listini (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
        '-------------------------------------------------------------------------------------
        strSQL = "SELECT DISTINCT lc_conto" & _
          " FROM listini" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lc_codart = " & CStrSQL(strCodart) & _
          " AND lc_codcas = ' '" & _
          " AND lc_conto > 0" & _
          " ORDER BY lc_conto"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI", , dbConn)

        For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
          UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto), dbConn)
        Next
        '-------------------------------------------------------------------------------------
        strSQL = "DELETE FROM listini" & _
          " WHERE listini.codditt = " & CStrSQL(strDitta) & _
          " AND listini.lc_codart = " & CStrSQL(strCodart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '-------------------------------------------------------------------------------------
        '--- Per le provvigioni (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
        '-------------------------------------------------------------------------------------
        strSQL = "SELECT DISTINCT per_conto" & _
          " FROM perprov" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND per_codart = " & CStrSQL(strCodart) & _
          " AND per_conto > 0" & _
          " ORDER BY per_conto"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV", , dbConn)
        For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
          UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto), dbConn)
        Next
        '-------------------------------------------------------------------------------------
        strSQL = "DELETE FROM perprov" & _
          " WHERE perprov.codditt = " & CStrSQL(strDitta) & _
          " AND perprov.per_codart = " & CStrSQL(strCodart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '-------------------------------------------------------------------------------------
        '--- Per gli sconti (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
        '-------------------------------------------------------------------------------------
        strSQL = "SELECT DISTINCT so_conto" & _
          " FROM sconti" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND so_codart = " & CStrSQL(strCodart) & _
          " AND so_clscar = 0" & _
          " AND so_conto > 0" & _
          " ORDER BY so_conto"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI", , dbConn)
        For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
          UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto), dbConn)
        Next
        '-------------------------------------------------------------------------------------
        strSQL = "DELETE FROM sconti" & _
          " WHERE sconti.codditt = " & CStrSQL(strDitta) & _
          " AND sconti.so_codart = " & CStrSQL(strCodart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaArticoli(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim lRec As Integer
    Try
      '-------------------------------------------------------------------------------------
      '--- Cancella i records in LOTCPRO
      '-------------------------------------------------------------------------------------
      strSQL = "DELETE FROM lotcpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lp_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '-------------------------------------------------------------------------------------
      '--- Cancella i records in LOTCDEF
      '-------------------------------------------------------------------------------------
      strSQL = "DELETE FROM lotcdef" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ld_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '-------------------------------------------------------------------------
      '--- Se si lavora in sqlserver occorre cancellare manualmente
      '--- i record relativi in ARTPRO, ARTPROX, BARCODE, CODARFO, ARTVAL
      '--- prima di cancellare i record di artico
      strSQL = "DELETE artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE artprox" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apx_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE barcode" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND bc_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE artval" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ax_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '-------------------------------------------------------------------------
      Try
        strSQL = "DELETE artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strCodart)
        lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Catch ex As Exception
        strError = "L'articolo: '|" & NTSCStr(strCodart) & "|' è movimentato," & vbCrLf & "pertanto non può essere cancellato."
        Return False
      End Try

      If Not lRec = 1 Then
        strError = "L'articolo: '|" & NTSCStr(strCodart) & "|' è movimentato," & vbCrLf & "pertanto non può essere cancellato."
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaCodarfo(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Per i codarfo (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT caf_conto" & _
        " FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCodart) & _
        " AND caf_conto > 0" & _
        " ORDER BY caf_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO", , dbConn)
      For i = 0 To dsTmp.Tables("CODARFO").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("CODARFO").Rows(i)!caf_conto), dbConn)
      Next
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaArtmaga(ByVal strDitta As String, ByVal strCodart As String, _
                                          ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artmaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND am_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaKit(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artkit" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  'funzioni per la cancellazione root e tutti gli articoli collegati al root
  Public Overridable Function CancellaAllListini(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                            ByVal strPrevar As String, ByRef strError As String, _
                                            ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      Select Case strPrevar
        Case "N"
          '-------------------------------------------------------------------------------------
          '--- Per i listini (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT lc_conto" & _
            " FROM listini" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND lc_codart = " & CStrSQL(strArtvCodroot) & _
            " AND lc_codcas = ' '" & _
            " AND lc_conto > 0" & _
            " ORDER BY lc_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI", , dbConn)

          For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          strSQL = "DELETE FROM listini" & _
            " WHERE listini.codditt = " & CStrSQL(strDitta) & _
            " AND listini.lc_codart = " & CStrSQL(strArtvCodroot)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '-------------------------------------------------------------------------------------
          '--- Per le provvigioni (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT per_conto" & _
            " FROM perprov" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND per_codart = " & CStrSQL(strArtvCodroot) & _
            " AND per_conto > 0" & _
            " ORDER BY per_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV", , dbConn)
          For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          strSQL = "DELETE FROM perprov" & _
            " WHERE perprov.codditt = " & CStrSQL(strDitta) & _
            " AND perprov.per_codart = " & CStrSQL(strArtvCodroot)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '-------------------------------------------------------------------------------------
          '--- Per gli sconti (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT so_conto" & _
            " FROM sconti" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND so_codart = " & CStrSQL(strArtvCodroot) & _
            " AND so_clscar = 0" & _
            " AND so_conto > 0" & _
            " ORDER BY so_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI", , dbConn)
          For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          strSQL = "DELETE FROM sconti" & _
            " WHERE sconti.codditt = " & CStrSQL(strDitta) & _
            " AND sconti.so_codart = " & CStrSQL(strArtvCodroot)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Case Else
          '-------------------------------------------------------------------------------------
          '--- Per i listini (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT lc_conto" & _
            " FROM listini" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND lc_codcas = ' '" & _
            " AND lc_conto > 0"
          Select Case strPrevar
            Case "1"
              strSQL = strSQL & " AND lc_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1" & _
                                                  " FROM artico" & _
                                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = strSQL & " AND lc_codart IN (SELECT ar_codart FROM artico" & _
                                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          strSQL = strSQL & " ORDER BY lc_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI", , dbConn)
          For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          Select Case strPrevar
            Case "1"
              strSQL = "DELETE FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1 FROM artico" & _
                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = "DELETE FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart IN (SELECT ar_codart FROM artico" & _
                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '-------------------------------------------------------------------------------------
          '--- Per le provvigioni (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT per_conto" & _
            " FROM perprov" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND per_conto > 0"
          Select Case strPrevar
            Case "1"
              strSQL = strSQL & " AND per_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1" & _
                                                   " FROM artico" & _
                                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                                   " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = strSQL & " AND per_codart IN (SELECT ar_codart FROM artico" & _
                                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                                   " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          strSQL = strSQL & " ORDER BY per_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV", , dbConn)
          For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          Select Case strPrevar
            Case "1"
              strSQL = "DELETE FROM perprov" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND per_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1 FROM artico" & _
                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                   " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = "DELETE FROM perprov" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND per_codart IN (SELECT ar_codart FROM artico" & _
                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                   " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '-------------------------------------------------------------------------------------
          '--- Per gli sconti (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
          '-------------------------------------------------------------------------------------
          strSQL = "SELECT DISTINCT so_conto" & _
            " FROM sconti" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND so_clscar = 0" & _
            " AND so_conto > 0"
          Select Case strPrevar
            Case "1"
              strSQL = strSQL & " AND so_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1" & _
                                                  " FROM artico" & _
                                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = strSQL & " AND so_codart IN (SELECT ar_codart FROM artico" & _
                                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          strSQL = strSQL & " ORDER BY so_conto"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI", , dbConn)
          For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
            UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto), dbConn)
          Next
          '-------------------------------------------------------------------------------------
          Select Case strPrevar
            Case "1"
              strSQL = "DELETE FROM sconti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart IN (SELECT DISTINCT ar_codroot + ar_codvar1 FROM artico" & _
                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
            Case "S"
              strSQL = "DELETE FROM sconti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart IN (SELECT ar_codart FROM artico" & _
                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                  " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
          End Select
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllArticoli(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                              ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim lRec As Integer
    Try
      strSQL = "SELECT ar_codart, ar_codvar1, ar_codvar2, ar_codvar3 FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codroot = " & CStrSQL(strArtvCodroot) & _
        " ORDER BY codditt, ar_codart"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      For i = 0 To dsTmp.Tables("ARTICO").Rows.Count - 1
        '-------------------------------------------------------------------------------------
        '--- Cancella i records in LOTCPRO
        '-------------------------------------------------------------------------------------
        strSQL = "DELETE FROM lotcpro" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lp_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '-------------------------------------------------------------------------------------
        '--- Cancella i records in LOTCDEF
        '-------------------------------------------------------------------------------------
        strSQL = "DELETE FROM lotcdef" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ld_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '-------------------------------------------------------------------------
        '--- Se si lavora in sqlserver occorre cancellare manualmente
        '--- i record relativi in ARTPRO, ARTPROX, BARCODE, CODARFO, ARTVAL
        '--- prima di cancellare i record di artico
        strSQL = "DELETE artpro" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ap_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE artprox" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND apx_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE barcode" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND bc_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE codarfo" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND caf_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE artval" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ax_codart = " & CStrSQL(dsTmp.Tables("ARTICO").Rows(i)!ar_codart)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '-------------------------------------------------------------------------
        Try
          strSQL = "DELETE artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codroot = " & CStrSQL(strArtvCodroot) & _
            " AND ar_codvar1 = " & CStrSQL(NTSCStr(dsTmp.Tables("ARTICO").Rows(i)!ar_codvar1)) & _
            " AND ar_codvar2 = " & CStrSQL(NTSCStr(dsTmp.Tables("ARTICO").Rows(i)!ar_codvar2)) & _
            " AND ar_codvar3 = " & CStrSQL(NTSCStr(dsTmp.Tables("ARTICO").Rows(i)!ar_codvar3))
          lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Catch ex As Exception
          strError = "L'articolo: '|" & NTSCStr(dsTmp.Tables("ARTICO").Rows(i)!ar_codart) & "|' è movimentato," & vbCrLf & "pertanto non può essere cancellato."
          Return False
        End Try

        If Not lRec = 1 Then
          strError = "L'articolo: '|" & NTSCStr(dsTmp.Tables("ARTICO").Rows(i)!ar_codart) & "|' è movimentato," & vbCrLf & "pertanto non può essere cancellato."
          Return False
        End If
      Next
      strSQL = "DELETE artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strArtvCodroot)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      Return False
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllArtvar(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                               ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE artvar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arv_codroot = " & CStrSQL(strArtvCodroot)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllArtroot(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                             ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE artroot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arr_codroot = " & CStrSQL(strArtvCodroot)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllCodarfo(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                            ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Per i codarfo (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT caf_conto" & _
        " FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart IN (SELECT caf_codart " & _
                           " FROM codarfo INNER JOIN artico ON codarfo.codditt = artico.codditt AND codarfo.caf_codart = ar_codart" & _
                           " WHERE codarfo.codditt = " & CStrSQL(strDitta) & _
                           " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")" & _
        " AND caf_conto > 0" & _
        " ORDER BY caf_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO", , dbConn)
      For i = 0 To dsTmp.Tables("CODARFO").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("CODARFO").Rows(i)!caf_conto), dbConn)
      Next
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart IN (SELECT ar_codart FROM artico" & _
                           " WHERE codditt = " & CStrSQL(strDitta) & _
                           " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllArtmaga(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                          ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artmaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND am_codart IN (SELECT ar_codart FROM artico" & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CancellaAllKit(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                        ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artkit" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart IN (SELECT ar_codart FROM artico" & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND ar_codroot = " & CStrSQL(strArtvCodroot) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AggiornaListini(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                  ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT lc_conto" & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strArtvCodroot) & _
        " AND lc_codcas = ' '" & _
        " AND lc_conto > 0" & _
        " ORDER BY lc_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI", , dbConn)
      For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto), dbConn)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function AggiornaProvvigioni(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT per_conto" & _
        " FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND per_codart = " & CStrSQL(strArtvCodroot) & _
        " AND per_clprar = 0" & _
        " AND per_conto > 0" & _
        " ORDER BY per_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV", , dbConn)
      For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto), dbConn)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function AggiornaProvvigioni(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "INSERT INTO perprov (codditt, per_codcage, per_codart, per_conto, per_coddest, per_clpran," & _
       " per_clprar, per_provv, per_datagg, per_datscad, per_codtpro, per_vprovv, per_unmis," & _
       " per_ultagg)" & _
       " SELECT codditt, per_codcage, " & CStrSQL(strCodartNew) & ", per_conto, per_coddest," & _
       " per_clpran, per_clprar, per_provv, per_datagg, per_datscad, per_codtpro, per_vprovv," & _
       " per_unmis, " & CDataOraSQL(Now) & _
       " FROM perprov" & _
       " WHERE codditt = " & CStrSQL(strDitta) & _
       " AND per_codart = " & CStrSQL(strCodartOld) & _
       " AND per_clprar = 0" & _
       " ORDER BY codditt, per_codcage, per_codart, per_conto, per_coddest, per_clpran, per_clprar," & _
       " per_datagg, per_codtpro"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT per_conto " & _
        " FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND per_codart = " & CStrSQL(strCodartOld) & _
        " AND per_clprar = 0" & _
        " AND per_conto > 0 " & _
        " ORDER BY per_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV")

      For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto))
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiornaSconti(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                              ByRef strError As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT so_conto" & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strArtvCodroot) & _
        " AND so_clscar = 0" & _
        " AND so_conto > 0" & _
        " ORDER BY so_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI", , dbConn)
      For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto), dbConn)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function AggiornaSconti(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "INSERT INTO sconti (codditt, so_codart, so_conto, so_coddest, so_clscan, so_clscar," & _
        " so_scont1, so_scont2, so_scont3, so_datagg, so_datscad, so_codtpro, so_daquant," & _
        " so_aquant, so_scont4, so_scont5, so_scont6, so_unmis, so_fase, so_ultagg)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", so_conto, so_coddest, so_clscan," & _
        " so_clscar, so_scont1, so_scont2, so_scont3, so_datagg, so_datscad, so_codtpro," & _
        " so_daquant, so_aquant, so_scont4, so_scont5, so_scont6, so_unmis, so_fase, " & _
        CDataOraSQL(Now) & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodartOld) & _
        " AND so_clscar = 0" & _
        " ORDER BY codditt, so_codart, so_conto, so_coddest, so_clscan, so_clscar, so_datagg," & _
        " so_codtpro, so_daquant"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT so_conto " & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodartOld) & _
        " AND so_clscar = 0" & _
        " AND so_conto > 0 " & _
        " ORDER BY so_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI")

      For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto))
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Var1Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal ds As DataSet) As Boolean
    Try
      Var1Aggiungi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVar1Tmp, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var1Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVar1Tmp, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artvar.* FROM artvar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arv_livello = 1" & _
        " AND arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND arv_codvar = " & CStrSQL(dtrVar1Tmp!xx_codvar1)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Ccontrollo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!arv_codvar) & ", ' ', ' '," & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar))) & "," & CStrSQL(dtrTmp!ar_formula) & "," & _
              CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If (bCreaBarcodeE13 = True) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar))) & vbCrLf
              End If
            End If
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function Var2Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal nVar As Integer, ByVal ds As DataSet) As Boolean
    Try
      Return Var2Aggiungi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, _
        dtrVarTmp, nVar, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var2Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal nVar As Integer, ByVal ds As DataSet, _
    ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, nVar, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      '--------------------------------------------------------------------------------------------------------------
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1, artvar2.arv_codvar as codvar2," & _
        " artvar2.arv_descr as descr2" & _
        " FROM artvar, artvar as artvar2" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot)
      If nVar = 1 Then 'controllo in che griglia sono posizionato
        strSQL += " AND artvar.arv_codvar = " & CStrSQL(dtrVarTmp!xx_codvar1)
      Else
        strSQL += " AND artvar2.arv_codvar = " & CStrSQL(dtrVarTmp!xx_codvar2)
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          '----------------------------------------------------------------------------------------------------------
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", ' '," & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2))) & "," & CStrSQL(dtrTmp!ar_formula) & "," & _
              CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If (bCreaBarcodeE13 = True) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2))) & vbCrLf
              End If
            End If
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), ds, dbConn)
          End If
          '----------------------------------------------------------------------------------------------------------
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal nVar As Integer, ByVal ds As DataSet) As Boolean
    Try
      Var3Aggiungi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVarTmp, _
        nVar, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var3Aggiungi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal nVar As Integer, ByVal ds As DataSet, _
    ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, nVar, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1, artvar2.arv_codvar as codvar2," & _
        " artvar2.arv_descr as descr2, artvar3.arv_codvar as codvar3, artvar3.arv_descr as descr3" & _
        " FROM artvar, artvar as artvar2, artvar as artvar3" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar3.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar3.arv_livello = 3" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar3.arv_codroot = " & CStrSQL(strArtvCodroot)
      If nVar = 1 Then 'controllo in che griglia sono posizionato
        strSQL += " AND artvar.arv_codvar = " & CStrSQL(dtrVarTmp!xx_codvar1)
      ElseIf nVar = 2 Then
        strSQL += " AND artvar2.arv_codvar = " & CStrSQL(dtrVarTmp!xx_codvar2)
      ElseIf nVar = 3 Then
        strSQL += " AND artvar3.arv_codvar = " & CStrSQL(dtrVarTmp!xx_codvar3)
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & _
              CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", " & CStrSQL(!codvar3) & ", " & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & _
              NTSCStr(!descr3), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & NTSCStr(!descr3), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & _
                NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & NTSCStr(!descr3), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3))) & _
              ", " & CStrSQL(dtrTmp!ar_formula) & "," & CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function Var1Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal ds As DataSet) As Boolean
    Try
      Var1Esplodi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVar1Tmp, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var1Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVar1Tmp, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.* FROM artvar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arv_livello = 1" & _
        " AND arv_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!arv_codvar) & ", ' ', ' '," & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar))) & ", " & _
              CStrSQL(dtrTmp!ar_formula) & ", " & CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)) & vbCrLf
              End If
            End If
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal ds As DataSet) As Boolean
    Try
      Var2Esplodi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVarTmp, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var2Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1," & _
        " artvar2.arv_codvar as codvar2, artvar2.arv_descr as descr2" & _
        " FROM artvar, artvar as artvar2" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '---------------------------------------------------------------------------------------------------------- 
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", ' '," & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 40))) & ", "
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2))) & ", " & _
              CStrSQL(dtrTmp!ar_formula) & ", " & CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal ds As DataSet) As Boolean
    Try
      Var3Esplodi(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVarTmp, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var3Esplodi(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1," & _
        " artvar2.arv_codvar as codvar2, artvar2.arv_descr as descr2," & _
        " artvar3.arv_codvar as codvar3, artvar3.arv_descr as descr3" & _
        " FROM artvar, artvar as artvar2, artvar as artvar3" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar3.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar3.arv_livello = 3" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar3.arv_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", " & _
              CStrSQL(!codvar3) & ", " & CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & _
              NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & NTSCStr(!descr3), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & NTSCStr(!descr3), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & _
                NTSCStr(!descr2) & " " & NTSCStr(!descr3), 41, 40)))
            End If
            strSQL += "," & _
              CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3))) & _
              ", " & CStrSQL(dtrTmp!ar_formula) & ", " & CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Var1EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal dttVar1 As DataTable, ByVal ds As DataSet) As Boolean
    Try
      Var1EspSel(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVar1Tmp, _
        dttVar1, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var1EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVar1Tmp As DataRow, ByVal dttVar1 As DataTable, ByVal ds As DataSet, _
    ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVar1Tmp, dttVar1, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.* FROM artvar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arv_livello = 1" & _
        " AND arv_codroot = " & CStrSQL(strArtvCodroot)
      strSQL += " AND artvar.arv_codvar IN ( "
      For i = 0 To dttVar1.Rows.Count - 1
        If NTSCStr(dttVar1.Rows(i)!xx_seleziona1) = "S" Then strSQL = strSQL & CStrSQL(dttVar1.Rows(i)!xx_codvar1) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!arv_codvar) & ", ' ', ' '," & _
              CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!arv_descr), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar))) & "," & CStrSQL(dtrTmp!ar_formula) & "," & _
              CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!arv_codvar)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal dttVar1 As DataTable, ByVal dttVar2 As DataTable, _
    ByVal ds As DataSet) As Boolean
    Try
      Var2EspSel(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVarTmp, _
        dttVar1, dttVar2, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var2EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal dttVar1 As DataTable, ByVal dttVar2 As DataTable, _
    ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, dttVar1, dttVar2, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(10))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1," & _
        " artvar2.arv_codvar as codvar2, artvar2.arv_descr as descr2" & _
        " FROM artvar, artvar as artvar2" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot)
      strSQL += " AND artvar.arv_codvar IN ( "
      For i = 0 To dttVar1.Rows.Count - 1
        If NTSCStr(dttVar1.Rows(i)!xx_seleziona1) = "S" Then strSQL = strSQL & CStrSQL(dttVar1.Rows(i)!xx_codvar1) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      strSQL += " AND artvar2.arv_codvar IN ( "
      For i = 0 To dttVar2.Rows.Count - 1
        If NTSCStr(dttVar2.Rows(i)!xx_seleziona2) = "S" Then strSQL = strSQL & CStrSQL(dttVar2.Rows(i)!xx_codvar2) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & _
              CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", ' '," & CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & _
              NTSCStr(!descr1) & " " & NTSCStr(!descr2), 40))) & ", "
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2))) & "," & CStrSQL(dtrTmp!ar_formula) & "," & _
              CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal dttVar1 As DataTable, ByVal dttVar2 As DataTable, _
    ByVal dttVar3 As DataTable, ByVal ds As DataSet) As Boolean
    Try
      Var3EspSel(strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, dtrTmp, dtrVarTmp, _
        dttVar1, dttVar2, dttVar3, ds, "")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Var3EspSel(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
    ByVal bIndicod As Boolean, ByVal strArtvCodroot As String, ByVal bCreaBarcodeE13 As Boolean, _
    ByVal dtrTmp As DataRow, ByVal dtrVarTmp As DataRow, ByVal dttVar1 As DataTable, ByVal dttVar2 As DataTable, _
    ByVal dttVar3 As DataTable, ByVal ds As DataSet, ByRef strMsg As String) As Boolean
    Dim i As Integer = 0
    Dim lRec As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
                                             dtrTmp, dtrVarTmp, dttVar1, dttVar2, dttVar3, ds, strMsg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strMsg = NTSCStr(oIn(11))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strMsg = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--- factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla e inserisce il artico.ar_codart = codice root
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        strSQL = "INSERT INTO artico " & GetQueryInsertField(ds.Tables("ARTICO"), "ar_", "") & _
          " VALUES " & GetQueryInsertValue(ds.Tables("ARTICO"), ds.Tables("ARTICO").Rows(0), "ar_", "")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      strSQL = "SELECT artvar.arv_codvar as codvar1, artvar.arv_descr as descr1, artvar2.arv_codvar as codvar2," & _
        " artvar2.arv_descr as descr2, artvar3.arv_codvar as codvar3, artvar3.arv_descr as descr3" & _
        " FROM artvar, artvar as artvar2, artvar as artvar3" & _
        " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
        " AND artvar2.codditt = " & CStrSQL(strDitta) & _
        " AND artvar3.codditt = " & CStrSQL(strDitta) & _
        " AND artvar.arv_livello = 1" & _
        " AND artvar2.arv_livello = 2" & _
        " AND artvar3.arv_livello = 3" & _
        " AND artvar.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar2.arv_codroot = " & CStrSQL(strArtvCodroot) & _
        " AND artvar3.arv_codroot = " & CStrSQL(strArtvCodroot)
      strSQL += " AND artvar.arv_codvar IN ( "
      For i = 0 To dttVar1.Rows.Count - 1
        If NTSCStr(dttVar1.Rows(i)!xx_seleziona1) = "S" Then strSQL = strSQL & CStrSQL(dttVar1.Rows(i)!xx_codvar1) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      strSQL += " AND artvar2.arv_codvar IN ( "
      For i = 0 To dttVar2.Rows.Count - 1
        If NTSCStr(dttVar2.Rows(i)!xx_seleziona2) = "S" Then strSQL = strSQL & CStrSQL(dttVar2.Rows(i)!xx_codvar2) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      strSQL += " AND artvar3.arv_codvar IN ( "
      For i = 0 To dttVar3.Rows.Count - 1
        If NTSCStr(dttVar3.Rows(i)!xx_seleziona3) = "S" Then strSQL = strSQL & CStrSQL(dttVar3.Rows(i)!xx_codvar3) & ","
      Next
      strSQL = Mid(strSQL, 1, (Len(strSQL) - 1)) & ")"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAR", , dbConn)
      For i = 0 To dsTmp.Tables("ARTVAR").Rows.Count - 1
        With dsTmp.Tables("ARTVAR").Rows(i)
          '----------------------------------------------------------------------------------------------------------
          '--- Controllo che l'articolo (root + varianti) non sia gia stato generato
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT artico.ar_codart FROM artico" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)))
          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO", , dbConn)
          If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
            strSQL = "INSERT INTO artico (codditt, ar_codvar1, ar_codvar2, ar_codvar3, ar_descr, ar_desint," & _
              " ar_scomin, ar_scomax, ar_minord, ar_inesaur, ar_note, ar_codart, ar_formula, ar_codroot)" & _
              " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(!codvar1) & ", " & CStrSQL(!codvar2) & ", " & _
              CStrSQL(!codvar3) & ", " & CStrSQL(Trim(Left(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & _
              NTSCStr(!descr2) & " " & NTSCStr(!descr3), 40))) & ","
            If (Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & NTSCStr(!descr3), 41, 40) = "") Then
              strSQL += "NULL"
            Else
              strSQL += CStrSQL(Trim(Mid(NTSCStr(dtrTmp!ar_descr) & " " & NTSCStr(!descr1) & " " & NTSCStr(!descr2) & " " & _
                NTSCStr(!descr3), 41, 40)))
            End If
            strSQL += "," & CDblSQL(NTSCDec(dtrTmp!ar_scomin)) & ", " & CDblSQL(NTSCDec(dtrTmp!ar_scomax)) & ", " & _
              CDblSQL(NTSCDec(dtrTmp!ar_minord)) & ", '" & NTSCStr(dtrTmp!ar_inesaur) & "', " & _
              CStrSQL(NTSCStr(dtrTmp!ar_note)) & ", " & _
              CStrSQL(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3))) & _
              "," & CStrSQL(dtrTmp!ar_formula) & "," & CStrSQL(Trim(strArtvCodroot)) & ")"
            lRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            '--------------------------------------------------------------------------------------------------------
            If (bCreaBarcodeE13) And (lRec > 0) Then
              If GeneraBarcode(strDitta, Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), _
                NTSCStr(dtrTmp!ar_unmis), strPrefixEAN13, bIndicod, dbConn) = False Then
                strMsg += " --> " & Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)) & vbCrLf
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            AggiornaArticoliDaArticoloRoot(Trim(strArtvCodroot) & Trim(NTSCStr(!codvar1)) & Trim(NTSCStr(!codvar2)) & Trim(NTSCStr(!codvar3)), ds, dbConn)
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GeneraBarcode(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByVal strUnmis As String, ByVal strPrefixEAN13 As String, _
                                            ByVal bIndicod As Boolean, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim ds As DataSet = Nothing
    Dim lOrins As Integer
    Dim strOra As String
    Dim strCodeTmp As String = ""

    Try
      '--- Se il progressivo è al limite massimo non crea il barcode, ma avvisa dopo che non ha potuto inserire il dato
      If LegNuma(strDitta, "BC", " ", 0, True, dbConn) <= 99999 Then
        '--- Se non esiste un articolo relativo in TTBARCODE lo inserisce
        strSQL = "SELECT bc_codart FROM barcode" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND bc_codart = " & CStrSQL(strCodart) & _
          " ORDER BY codditt, bc_code"
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE", , dbConn)

        If ds.Tables("BARCODE").Rows.Count = 0 Then
          strOra = Format(Now.ToShortTimeString, "hh:mm:ss")
          lOrins = NTSCInt(Mid(strOra, 1, 2)) * 10000
          lOrins = lOrins + NTSCInt(Mid$(strOra, 4, 2)) * 100
          lOrins = lOrins + NTSCInt(Mid$(strOra, 7, 2))
          '----------------------------------------------------------------------------------------------------------
          strCodeTmp = GeneraEAN13(strDitta, strPrefixEAN13, bIndicod, dbConn)
          If strCodeTmp = "" Then Return False
          '----------------------------------------------------------------------------------------------------------
          strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins," & _
            " bc_code, bc_unmis, bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
            CDataSQL(Now.ToShortDateString) & ", " & CStrSQL(strCodeTmp) & ", " & CStrSQL(strUnmis) & ", 1, " & _
            lOrins & ", " & CDataSQL(Now.ToShortDateString) & ", " & lOrins & ", 'E')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GeneraEAN13(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
                                          ByVal bIndicod As Boolean, ByVal dbConn As DbConnection) As String
    Dim strSQL As String = ""
    Dim strDummy As String
    Dim lProgr As Integer
    Dim lProgrTmp As Integer
    Dim strLastDigit As String
    Dim strPrefissoEAN13 As String
    Try
      '-----------------------------------------------------------------------------------------
      GeneraEAN13 = ""
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = LegNuma(strDitta, "BC", " ", 0, True, dbConn)
      lProgrTmp = lProgr
      '-----------------------------------------------------------------------------------------
      strPrefissoEAN13 = strPrefixEAN13
      '-----------------------------------------------------------------------------------------
      '--- Se non c'è l'ipostazione di registro per la creazione da Indicod funziona come prima
      '-----------------------------------------------------------------------------------------
      If bIndicod = False Then
        strDummy = "2" & CStr(Format(lProgr, "00000")) & "000000"
      Else
        If Len(strPrefissoEAN13) < 12 Then
          If Len(strPrefissoEAN13 & CStr(lProgrTmp)) < 12 Then
            Do
              strPrefissoEAN13 = strPrefissoEAN13 & "0"
            Loop Until Len(strPrefissoEAN13 & CStr(lProgrTmp)) = 12
            strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
          Else
            If Len(strPrefissoEAN13 & CStr(lProgrTmp)) > 12 Then
              If Len(CStr(lProgrTmp)) > 1 Then
                Do
                  lProgrTmp = NTSCInt(Right(CStr(lProgrTmp), (Len(CStr(lProgrTmp)) - 1)))
                  If lProgrTmp = 0 Then Return ""
                Loop Until Len(strPrefissoEAN13 & CStr(lProgrTmp)) = 12
                strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
              Else
                strDummy = Left(strPrefissoEAN13, 12)
              End If
            Else
              strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
            End If
          End If
        Else
          If Len(strPrefissoEAN13) > 12 Then
            strDummy = Left(strPrefissoEAN13, 12)
          Else
            strDummy = strPrefissoEAN13
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina il CheckDigit finale
      '-----------------------------------------------------------------------------------------
      strLastDigit = CheckDigit(strDummy & "0")
      '-----------------------------------------------------------------------------------------
      '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      If strLastDigit <> "" Then
        strDummy = strDummy & strLastDigit
        GeneraEAN13 = strDummy
        AggNuma(strDitta, "BC", " ", 0, lProgr, True, True, "", dbConn)
      End If
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckDigit(ByVal strCodice As String) As String
    Dim strSQL As String = ""
    Dim nI As Integer
    Dim nCurNum As Integer
    Dim nSumDisp As Integer
    Dim nSumPari As Integer
    Dim nSumTot As Integer
    Try
      CheckDigit = ""
      nSumPari = 0 : nSumDisp = 0
      For nI = 1 To Len(strCodice) - 1
        nCurNum = NTSCInt(Mid(strCodice, nI, 1))
        '--- Verifico che il numero corrente sia pari o dispari
        If (nI Mod 2) = 0 Then nSumPari = (nSumPari + nCurNum) Else nSumDisp = (nSumDisp + nCurNum)
      Next
      nSumPari = (nSumPari * 3) : nSumTot = (nSumPari + nSumDisp)
      CheckDigit = Right(NTSCStr(10 - NTSCInt(Right(NTSCStr(nSumTot), 1))), 1)

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetColonneSpecificheArticolo(ByRef dttFieldAlias As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0

    Try
      '---------------------------------
      'serve per rinominare le colonne precedentemenre standardizzate
      dttFieldAlias.Columns.Add("datatable", GetType(String))
      dttFieldAlias.Columns.Add("database", GetType(String))
      dttFieldAlias.Rows.Add(New Object() {"ar_codvar1", ""})              'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_codvar2", ""})            'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_codvar3", ""})              'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_descr", ""})             'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_desint", ""})            'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_scomin", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_scomax", ""})            'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_minord", ""})           'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_inesaur", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_note", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_codart", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_formula", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"codditt", ""})          'colonna da escludere dalla query
      dttFieldAlias.Rows.Add(New Object() {"ar_codroot", ""})          'colonna da escludere dalla query
      For i = 1 To 10
        dttFieldAlias.Rows.Add(New Object() {"ar_xtipo" & i.ToString, ""})
      Next
      For i = 1 To 20
        dttFieldAlias.Rows.Add(New Object() {"ar_xdescr" & i.ToString, ""})
      Next
      For i = 1 To 10
        dttFieldAlias.Rows.Add(New Object() {"ar_xdesext" & i.ToString, ""})
      Next
      For i = 1 To 10
        dttFieldAlias.Rows.Add(New Object() {"ar_xmemo" & i.ToString, ""})
      Next
      For i = 1 To 20
        dttFieldAlias.Rows.Add(New Object() {"ar_xdata" & i.ToString, ""})
      Next
      For i = 1 To 20
        dttFieldAlias.Rows.Add(New Object() {"ar_xnum" & i.ToString, ""})
      Next
      For i = 1 To 20
        dttFieldAlias.Rows.Add(New Object() {"ar_xcheck" & i.ToString, ""})
      Next
      For i = 1 To 20
        dttFieldAlias.Rows.Add(New Object() {"ar_xcombo" & i.ToString, ""})
      Next
      dttFieldAlias.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ArticoRipristinaCancella(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                                       ByVal dsVar1 As DataSet, ByVal dsVar2 As DataSet, ByVal dsVar3 As DataSet, _
                                                       ByVal dsArtico As DataSet, ByVal ds As DataSet, ByRef strError As String) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      'NEL CASO DI NUOVO CANCELLA TUTTI I DATI GIA INSERITI

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If Not CancellaAllArticoli(strDitta, strArtvCodroot, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If
      If Not CancellaAllArtvar(strDitta, strArtvCodroot, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If
      If Not CancellaAllArtroot(strDitta, strArtvCodroot, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VarNuovoSalva(ByVal strDitta As String, ByVal strArtvCodroot As String, _
                                      ByVal strLivello As String, ByVal nNumliv As Integer, _
                                      ByVal strPrevar As String, ByVal nArtvLungroot As Integer, _
                                      ByVal nLungvar1 As Integer, ByVal nLungvar2 As Integer, _
                                      ByVal nLungvar3 As Integer, ByVal strAr_descr As String, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim dbConn As DbConnection = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      'FUNZIONE PER IL INSERIMENTO DELLE VARIANTI NEL CASO NUOVO PASSANDO IL CODICE VARIANTE

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'prima se non presente creo un record di artroot
      strSQL = "SELECT arr_codroot FROM artroot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND arr_codroot = " & CStrSQL(strArtvCodroot)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTROOT", , dbConn)

      If dsTmp.Tables("ARTROOT").Rows.Count = 0 Then
        strSQL = "INSERT INTO artroot (codditt, arr_codroot, arr_descr, arr_numliv," & _
          " arr_lungroot, arr_lungvar1, arr_lungvar2, arr_lungvar3, arr_prevar1)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strArtvCodroot) & ", " & _
          CStrSQL(strAr_descr) & ", " & nNumliv & ", " & nArtvLungroot & ", " & _
          nLungvar1 & ", " & nLungvar2 & ", " & nLungvar3 & ", '" & strPrevar & "')"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      For i = 0 To ds.Tables("ARTVAR").Rows.Count - 1
        strSQL = "INSERT INTO artvar (codditt, arv_codroot, arv_livello, arv_codvar, arv_descr)" & _
          " VALUES ( " & CStrSQL(strDitta) & ", " & CStrSQL(strArtvCodroot) & ", " & strLivello & ", " & _
          CStrSQL(ds.Tables("ARTVAR").Rows(i)("xx_codvar" & strLivello)) & ", " & _
          CStrSQL(Microsoft.VisualBasic.Left(ds.Tables("ARTVAR").Rows(i)("xx_descr" & strLivello).ToString.Trim, 40)) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AggiornaArticoliDaArticoloRoot(ByVal strCodart As String, ByRef dsShared As DataSet, _
                                                           ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttFieldAlias As New DataTable
    Try
      'nel caso se inserisco una riga in analitico varianti non modifico niente negli altri tab
      GetColonneSpecificheArticolo(dttFieldAlias)

      'l'update per i campi non in comune tra root e root + varianti
      strSQL = "UPDATE artico SET " & GetQueryUpdate(dsShared.Tables("ARTICO"), dsShared.Tables("ARTICO").Rows(0), "ar_", "", "", dttFieldAlias) & _
        " WHERE codditt = " & CStrSQL(dsShared.Tables("ARTICO").Rows(0)!codditt.ToString) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArticoTipoopz(ByVal strDitta As String, ByVal strCodart As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 ar_codart, ar_tipoopz FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codroot = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, ar_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtRootFromArtico(ByVal strDitta As String, ByVal strCodroot As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codroot = " & CStrSQL(strCodroot) & _
        " ORDER BY codditt, ar_codart"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function AggiornaArtval(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr, ax_desint, ax_ultagg, ax_note)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", ax_codvalu, ax_descr, ax_desint, ax_ultagg, ax_note" & _
        " FROM artval" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ax_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, ax_codart, ax_codvalu"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaArtmaga(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artmaga (codditt, am_codart, am_codmaga, am_ubicaz, am_scomin, am_scomax, am_minord," & _
        " am_polriord, am_ultagg, am_opnome, am_sublotto, am_maxlotto, am_ggragg, am_perragg, am_scosic, am_fase)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", am_codmaga, am_ubicaz, am_scomin, am_scomax, am_minord," & _
        " am_polriord, am_ultagg, am_opnome, am_sublotto, am_maxlotto, am_ggragg, am_perragg, am_scosic, am_fase" & _
        " FROM artmaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND am_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, am_codart, am_codmaga, am_fase"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaArtfasi(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strSQLInsert As String = ""
    Try
      'prima se non presente creo un record di artfasi
      strSQL = "SELECT * FROM artfasi" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND af_codart = " & CStrSQL(strCodartNew)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTFASI")

      If dsTmp.Tables("ARTFASI").Rows.Count = 0 Then
        For Each oCol As DataColumn In dsTmp.Tables("ARTFASI").Columns
          If oCol.ColumnName <> "ts" Then
            strSQLInsert += oCol.ColumnName.ToLower & ", "
          End If
        Next
        strSQLInsert = strSQLInsert.Substring(0, strSQLInsert.Length - 2)
        strSQL = "INSERT INTO artfasi (" & strSQLInsert & ") " & _
                 "SELECT " & strSQLInsert.Replace("af_codart", CStrSQL(strCodartNew)).Replace("af_ultagg", CDataOraSQL(Now)) & _
                 " FROM artfasi" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND af_codart = " & CStrSQL(strCodartOld)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaTabelleVarianti(ByVal strDitta As String, ByVal strCodartNew As String, _
                                                       ByVal strCodartOld As String, ByVal bListini As Boolean, _
                                                       ByVal bSconti As Boolean, ByVal bProvv As Boolean, _
                                                       ByVal bArtfasi As Boolean, ByVal nArtvLungrootOld As Integer) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Dim strCodRootNew As String = ""
    Try
      'duplico le tabelle collegate con codice root se spuntate nella form
      If bListini = True Then AggiornaListini(strDitta, strCodartNew, strCodartOld)
      If bSconti = True Then AggiornaSconti(strDitta, strCodartNew, strCodartOld)
      If bProvv = True Then AggiornaProvvigioni(strDitta, strCodartNew, strCodartOld)
      AggiornaArtval(strDitta, strCodartNew, strCodartOld)
      AggiornaArtmaga(strDitta, strCodartNew, strCodartOld)
      If bArtfasi = True Then AggiornaArtfasi(strDitta, strCodartNew, strCodartOld)

      strSQL = "SELECT ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codroot = " & CStrSQL(strCodartOld)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      strCodRootNew = strCodartNew

      For i = 0 To ds.Tables("ARTICO").Rows.Count - 1
        strCodartOld = NTSCStr(ds.Tables("ARTICO").Rows(i)!ar_codart)
        strCodartNew = strCodRootNew & Right(strCodartOld, Len(strCodartOld) - nArtvLungrootOld) 'estraggo il vecchio root dalla lista degli articoli

        'duplico le tabelle collegate con codice root + varianti se spuntate nella form
        If bListini = True Then AggiornaListini(strDitta, strCodartNew, strCodartOld)
        If bSconti = True Then AggiornaSconti(strDitta, strCodartNew, strCodartOld)
        If bProvv = True Then AggiornaProvvigioni(strDitta, strCodartNew, strCodartOld)
        AggiornaArtval(strDitta, strCodartNew, strCodartOld)
        AggiornaArtmaga(strDitta, strCodartNew, strCodartOld)
        If bArtfasi = True Then AggiornaArtfasi(strDitta, strCodartNew, strCodartOld)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtclasDescr(ByVal strDitta As String, ByVal strClas1 As String, _
                                            ByVal strClas2 As String, ByVal strClas3 As String, _
                                            ByVal strClas4 As String, ByVal strClas5 As String) As String
    GetArtclasDescr = ""
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strDescr As String = ""
    Try
      If strClas1 <> "" Then
        strSQL = "SELECT acl_descla1 FROM artclas1 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr = NTSCStr(dttTmp.Rows(0)!acl_descla1)
        dttTmp.Clear()
      End If

      If strClas2 <> "" Then
        strSQL = "SELECT acl_descla2 FROM artclas2 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla2)
        dttTmp.Clear()
      End If

      If strClas3 <> "" Then
        strSQL = "SELECT acl_descla3 FROM artclas3 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla3)
        dttTmp.Clear()
      End If

      If strClas4 <> "" Then
        strSQL = "SELECT acl_descla4 FROM artclas4 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3) & " AND acl_codcla4 = " & CStrSQL(strClas4)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla4)
        dttTmp.Clear()
      End If

      If strClas5 <> "" Then
        strSQL = "SELECT acl_descla5 FROM artclas5 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3) & " AND acl_codcla4 = " & CStrSQL(strClas4) & _
                 " AND acl_codcla5 = " & CStrSQL(strClas5)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla5)
        dttTmp.Clear()
      End If

      Return strDescr

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ArticoliDeteriorabili(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 rp_liv1 FROM REGPROP " & _
               " WHERE rp_liv1 = 'BSVEBOLL' " & _
               "   AND rp_liv2 IN ('OPZIONI', 'OPZIONIDOC')  " & _
               "   AND rp_idaz IN ('', " & CStrSQL(strDitta) & ") " & _
               "   AND rp_nomprop = 'SpezzaDocArt62Deterior' " & _
               "   AND rp_valprop <> '0' "

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC).Rows.Count > 0
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#Region "Articoli Accessori/succedanei"
  Public Overridable Function CancellaArtacce(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artacce" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM artacce" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart <> " & CStrSQL(strCodart) & _
        " AND apa_codartas = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataArta(ByVal strDitta As String, ByVal strArtaCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artacce.*, artico.ar_descr as xx_codartas" & _
        " FROM artacce " & _
        " LEFT JOIN artico ON artacce.codditt = artico.codditt AND artacce.apa_codartas = artico.ar_codart" & _
        " WHERE artacce.codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart = " & CStrSQL(strArtaCodart) & _
        " ORDER BY artacce.codditt, apa_codart, apa_codartas"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTACCE")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RicreaSuccedaneiAccessori(ByVal strDitta As String, ByVal strCodart As String, ByVal bSucc As Boolean, ByVal bAcc As Boolean, _
                                                          ByVal dttIn As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dtrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim strQuery As String = ""
    Dim i As Integer = 0
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM artacce WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codartas = " & CStrSQL(strCodart)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For z As Integer = 0 To dttIn.Rows.Count - 1
        If (NTSCStr(dttIn.Rows(z)!apa_tipo) = "A" And bAcc) Or (NTSCStr(dttIn.Rows(z)!apa_tipo) = "S" And bSucc) Then
          strSQL = "DELETE FROM artacce " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codart = " & CStrSQL(dttIn.Rows(z)!apa_codartas) & _
                   " AND apa_tipo = " & CStrSQL(dttIn.Rows(z)!apa_tipo)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          dtrRow = dttIn.Select("apa_tipo = '" & NTSCStr(dttIn.Rows(z)!apa_tipo) & "'")

          For i = 0 To dtrRow.Length - 1
            If NTSCStr(dtrRow(i)!apa_codartas) <> NTSCStr(dttIn.Rows(z)!apa_codartas) Then
              strSQL = "INSERT INTO artacce (codditt, apa_codart, apa_codartas, apa_tipo, apa_note, apa_ultagg, apa_opnome) VALUES " & _
                       "(" & CStrSQL(dttIn.Rows(z)!codditt) & ", " & CStrSQL(dttIn.Rows(z)!apa_codartas) & ", " & CStrSQL(dtrRow(i)!apa_codartas) & ", " & _
                       CStrSQL(dttIn.Rows(z)!apa_tipo) & ", " & CStrSQL(dttIn.Rows(z)!apa_note) & ", " & CDataSQL(Now) & ", " & CStrSQL(oApp.User.Nome) & ")"

              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          Next

          strSQL = "INSERT INTO artacce (codditt, apa_codart, apa_codartas, apa_tipo, apa_note, apa_ultagg, apa_opnome) VALUES " & _
                "(" & CStrSQL(dttIn.Rows(z)!codditt) & ", " & CStrSQL(dttIn.Rows(z)!apa_codartas) & ", " & CStrSQL(strCodart) & ", " & _
                CStrSQL(dttIn.Rows(z)!apa_tipo) & ", " & CStrSQL(dttIn.Rows(z)!apa_note) & ", " & CDataSQL(Now) & ", " & CStrSQL(oApp.User.Nome) & ")"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strQuery &= CStrSQL(dttIn.Rows(z)!apa_codartas) & ", "
      Next

      strQuery &= CStrSQL(strCodart)

      strSQL = "DELETE FROM artacce " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codart NOT IN (" & strQuery & ") AND apa_codartas IN (" & strQuery & ")"

      If Not (bAcc And bSucc) Then
        If bAcc Then
          strSQL &= " AND apa_tipo = 'A'"
        ElseIf bSucc Then
          strSQL &= " AND apa_tipo = 'S'"
        End If
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()

      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CompletaSuccedaneiAccessori(ByVal strDitta As String, ByVal strQuery As String, ByVal bAcc As Boolean, ByVal bSucc As Boolean, _
                                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT codditt, apa_codart, apa_codartas, apa_tipo FROM artacce " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codartas IN (" & strQuery & ")"

      If Not (bAcc And bSucc) Then
        If bAcc Then
          strSQL &= " AND apa_tipo = 'A'"
        ElseIf bSucc Then
          strSQL &= " AND apa_tipo = 'S'"
        End If
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "BNMGANEX.VB"
  Public Overridable Function GetTabaext(ByVal strDitta As String, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_tipork = 'A'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        dttTmp.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

End Class
